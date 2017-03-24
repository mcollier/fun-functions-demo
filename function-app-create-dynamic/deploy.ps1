$subscriptionId = "{your-azure-subscription-id-here}"
$resourceGroupName = "{your-resource-group-name-here}"


<#
Login-AzureRmAccount -SubscriptionId $subscriptionId


Test-AzureRmResourceGroupDeployment -ResourceGroupName $resourceGroupName `
                                    -TemplateFile "azuredeploy.json" `
                                    -TemplateParameterFile "azuredeploy.parameters.json" -Verbose
#>
$currentDate = get-date -Format yyyyMMdd.HHmmss
$deploymentLabel = "funfunc-$currentDate"

New-AzureRmResourceGroupDeployment  -Name $deploymentLabel `
                                    -ResourceGroupName $resourceGroupName `
                                    -TemplateFile "azuredeploy.json" `
                                    -TemplateParameterFile "azuredeploy.parameters.json" `
                                    -Verbose
