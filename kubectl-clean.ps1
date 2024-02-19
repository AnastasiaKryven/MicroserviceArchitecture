
try {
    Write-Host "Building kubectl..."
	kubectl delete deployment platforms-depl
	kubectl delete deployment commands-depl
	kubectl delete deployment mssql-depl
	kubectl delete deployment rabbitmq-depl
	kubectl delete all  --all -n ingress-nginx

	kubectl delete service commands-clusterip-srv
	kubectl delete service mssql-clusterip-srv
	kubectl delete service mssql-loadbalancer
	kubectl delete service platformnpservice-srv
	kubectl delete service platforms-clusterip-srv
	kubectl delete service rabbitmq-clusterip-srv
	kubectl delete service rabbitmq-loadbalancer
} finally {
    Write-Host "Stopping..."
}
