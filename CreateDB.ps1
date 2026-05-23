$dbPath = "C:\Users\Victus\.gemini\antigravity\scratch\Hw4NewsProject\App_Data\NewsDB.mdb"

if (Test-Path $dbPath) {
    Remove-Item $dbPath
}

$providers = @(
    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=$dbPath",
    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$dbPath"
)

$success = $false
foreach ($connStr in $providers) {
    try {
        $catalog = New-Object -ComObject ADOX.Catalog
        $catalog.Create($connStr)
        
        $conn = New-Object -ComObject ADODB.Connection
        $conn.Open($connStr)
        
        $cmd = "CREATE TABLE News (Id AUTOINCREMENT PRIMARY KEY, Title VARCHAR(255), Description MEMO, Link VARCHAR(255), Category VARCHAR(50), PubDate DATETIME)"
        $conn.Execute($cmd)
        $conn.Close()
        
        Write-Host "Database created with provider $connStr"
        $success = $true
        break
    } catch {
        Write-Host "Failed with provider $connStr : $($_.Exception.Message)"
    }
}

if (-not $success) {
    Write-Host "Could not create database."
}
