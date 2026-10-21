CREATE TEMP TABLE projects(
       project_id int PRIMARY KEY,
       name varchar(255) NOT NULL
);
       
CREATE TEMP TABLE work_items(
       item_id int PRIMARY KEY,
       title varchar(255) NOT NULL,
       project_id int REFERENCES projects(project_id) NOT NULL
);

INSERT INTO projects (project_id, name)
VALUES (1, 'Elso projekt'),
       (2, 'Masodik projekt');

INSERT INTO work_items (item_id, title, project_id)
VALUES (101, 'Elso feladat', 1),
       (102, 'Masodik feladat', 1),
       (103, 'Harmadik feladat', 2);

SELECT projects.name AS project_name, work_items.title AS workitem_title
FROM projects
JOIN work_items ON work_items.project_id = projects.project_id
ORDER BY work_items.item_id;

INSERT INTO work_items (item_id, title, project_id)
VALUES (104, 'Elso feladat', 999);
