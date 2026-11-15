-- Additional data for filtering, counting, ordering and pagination practice.
-- Run against the local data_access_lab database after InitialSchema.
-- Existing rows are preserved. Re-running does not duplicate these demo rows.
-- Uses the current endpoint's "dgdfg" prefix and the earlier "proba" prefix.
BEGIN;
SET LOCAL lock_timeout = '5s';
LOCK TABLE public."Projects", public."WorkItems" IN SHARE ROW EXCLUSIVE MODE;

CREATE TEMP TABLE query_practice_seed (
    project_name text PRIMARY KEY,
    total_count integer NOT NULL,
    dgdfg_count integer NOT NULL,
    proba_count integer NOT NULL,
    CHECK (total_count >= 0 AND dgdfg_count >= 0 AND proba_count >= 0),
    CHECK (dgdfg_count + proba_count <= total_count)
) ON COMMIT DROP;

INSERT INTO query_practice_seed VALUES
    ('[Demo] Inventory API',        10, 5, 3),
    ('[Demo] Book Tracker',          8, 5, 1),
    ('[Demo] Admin Portal',          7, 4, 2),
    ('[Demo] Recipe Collection',     6, 4, 0),
    ('[Demo] Event Planner',         9, 3, 4),
    ('[Demo] Expense Tracker',       5, 3, 1),
    ('[Demo] Learning Dashboard',    8, 2, 4),
    ('[Demo] Support Desk',          4, 2, 1),
    ('[Demo] Fitness Log',           6, 1, 3),
    ('[Demo] Music Library',         3, 1, 1),
    ('[Demo] Weather Monitor',       5, 0, 3),
    ('[Demo] Travel Journal',        3, 0, 2),
    ('[Demo] Shopping List',         2, 0, 0),
    ('[Demo] Notes API',             1, 0, 1),
    ('[Demo] Photo Gallery',         4, 0, 0),
    ('[Demo] Empty Sandbox',         0, 0, 0),
    ('[Demo] Future Project',        0, 0, 0),
    ('[Demo] Archived Idea',         0, 0, 0);

-- Earlier exercises inserted IDs manually. Advance the identity sequences
-- past existing IDs without moving them backwards.
SELECT setval(
    pg_get_serial_sequence('public."Projects"', 'ProjectId'),
    GREATEST(
        (SELECT COALESCE(MAX("ProjectId"), 0) FROM public."Projects"),
        (SELECT last_value FROM public."Projects_ProjectId_seq")
    ),
    true
);

SELECT setval(
    pg_get_serial_sequence('public."WorkItems"', 'WorkItemId'),
    GREATEST(
        (SELECT COALESCE(MAX("WorkItemId"), 0) FROM public."WorkItems"),
        (SELECT last_value FROM public."WorkItems_WorkItemId_seq")
    ),
    true
);

INSERT INTO public."Projects" ("Name")
SELECT s.project_name
FROM query_practice_seed AS s
WHERE NOT EXISTS (
    SELECT 1 FROM public."Projects" AS p WHERE p."Name" = s.project_name
)
ORDER BY s.project_name;

WITH demo_items AS (
    SELECT p."ProjectId",
           CASE
               WHEN n <= s.dgdfg_count THEN 'dgdfg'
               WHEN n <= s.dgdfg_count + s.proba_count THEN 'proba'
               ELSE 'task'
           END || ' - ' || s.project_name || ' - ' || lpad(n::text, 2, '0') AS title
    FROM query_practice_seed AS s
    JOIN public."Projects" AS p ON p."Name" = s.project_name
    CROSS JOIN LATERAL generate_series(1, s.total_count) AS series(n)
)
INSERT INTO public."WorkItems" ("Title", "ProjectId")
SELECT d.title, d."ProjectId"
FROM demo_items AS d
WHERE NOT EXISTS (
    SELECT 1 FROM public."WorkItems" AS w
    WHERE w."ProjectId" = d."ProjectId" AND w."Title" = d.title
)
ORDER BY d."ProjectId", d.title;

-- Verify every demo project's total and both prefix counts before committing.
DO $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM query_practice_seed AS s
        JOIN public."Projects" AS p ON p."Name" = s.project_name
        LEFT JOIN public."WorkItems" AS w ON w."ProjectId" = p."ProjectId"
        GROUP BY s.project_name, s.total_count, s.dgdfg_count, s.proba_count
        HAVING count(w."WorkItemId") <> s.total_count
            OR count(*) FILTER (WHERE w."Title" LIKE 'dgdfg%') <> s.dgdfg_count
            OR count(*) FILTER (WHERE w."Title" LIKE 'proba%') <> s.proba_count
    ) THEN
        RAISE EXCEPTION 'Demo counts differ from the seed specification';
    END IF;
END $$;

COMMIT;

SELECT p."ProjectId", p."Name",
       count(w."WorkItemId") AS "TotalWorkItems",
       count(*) FILTER (WHERE w."Title" LIKE 'dgdfg%') AS "DgdfgCount",
       count(*) FILTER (WHERE w."Title" LIKE 'proba%') AS "ProbaCount"
FROM public."Projects" AS p
LEFT JOIN public."WorkItems" AS w ON w."ProjectId" = p."ProjectId"
GROUP BY p."ProjectId", p."Name"
ORDER BY "DgdfgCount" DESC, p."ProjectId";
