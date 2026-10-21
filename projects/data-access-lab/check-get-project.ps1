# A futo helyi API-t es az M3-hoz felvett tesztadatokat ellenorzi.
$baseUrl = 'http://localhost:5267'

$projectResponse = Invoke-WebRequest -Uri "$baseUrl/projects/1" -UseBasicParsing -ErrorAction Stop
if ([int]$projectResponse.StatusCode -ne 200) {
    throw "A /projects/1 valasza nem 200, hanem $($projectResponse.StatusCode)."
}

$project = $projectResponse.Content | ConvertFrom-Json
if ($project.projectId -ne 1 -or $project.name -ne 'Project 1') {
    throw 'A /projects/1 valaszaban nem a vart projekt szerepel.'
}

$workItemIds = @($project.workItems | ForEach-Object { $_.workItemId })
if (1 -notin $workItemIds -or 2 -notin $workItemIds) {
    throw 'A /projects/1 valaszabol hianyzik valamelyik eredeti munkatetel.'
}

Write-Host 'OK: /projects/1 -> 200, a projekt es a ket eredeti munkatetel megerkezett.'

$missingStatus = $null
try {
    $missingResponse = Invoke-WebRequest -Uri "$baseUrl/projects/99" -UseBasicParsing -ErrorAction Stop
    $missingStatus = [int]$missingResponse.StatusCode
}
catch {
    if ($null -eq $_.Exception.Response) {
        throw
    }

    $missingStatus = [int]$_.Exception.Response.StatusCode
}

if ($missingStatus -ne 404) {
    throw "A /projects/99 valasza nem 404, hanem $missingStatus."
}

Write-Host 'OK: /projects/99 -> 404.'
