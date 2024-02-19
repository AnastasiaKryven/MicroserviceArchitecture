
try {
    Write-Host "Building kubectl..."
	docker build -t anastasiyakryven1313/platformservice ./src/PlatformService
	docker build -t anastasiyakryven1313/commandservice ./src/CommandsService
	
	kubectl apply -f ./k8s/platforms-depl.yaml
	kubectl apply -f ./k8s/platforms-np-srv.yaml
	kubectl apply -f ./k8s/commands-depl.yaml
	kubectl apply -f ./k8s/local-pvc.yaml.yaml
	kubectl apply -f ./k8s/mssql-plat-depl.yaml
	kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
	kubectl apply -f ./k8s/rabbitmq-depl.yaml
	kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/cloud/deploy.yaml
	kubectl apply -f ./k8s/ingress-srv.yaml
	
	kubectl rollout restart deployment ./k8s/platforms-depl
	kubectl rollout restart deployment ./k8s/commands-depl
	
	Write-Host -Object ('The key that was pressed was: {0}' -f [System.Console]::ReadKey().Key.ToString());
} finally {
    Write-Host "Stopping..."
}
