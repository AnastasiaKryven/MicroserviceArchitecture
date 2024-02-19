
try {
    Write-Host "Building images..."
    docker-compose build > compose.log
    [Console]::ResetColor()
    Write-Host "Starting services..."
    docker-compose up
    Write-Host "Writings logs to compose.log, press Ctrl+C to stop services."
    docker-compose logs -f --no-color >> compose.log
 	
} finally {
    [Console]::ResetColor()
    Write-Host "Stopping services..."
    docker-compose down
}
