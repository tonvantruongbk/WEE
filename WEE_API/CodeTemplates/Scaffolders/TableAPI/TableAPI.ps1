[T4Scaffolding.Scaffolder(Description = "Enter a description of CMT.JTableGrid here")][CmdletBinding()]
param(    
	[Parameter(Mandatory=$true, ValueFromPipeLineByPropertyName=$true)]$ModelTypes,    
	[string]$controllerName ,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $true,
	[switch]$Delete= $false
) 

foreach($ModelType in $ModelTypes)
{ 

	if($controllerName) {
	$ControlerName =  $controllerName
	}
	else {
		$ControlerName =  $ModelType
	}
	$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
	$Project = $namespace 
	$envdte = [System.Runtime.InteropServices.Marshal]::GetActiveObject("VisualStudio.DTE.14.0")
	$solution = $envdte.Solution;
	$ProjectInstance = ($solution.Projects | where ProjectName -eq $Project);

	$foundModelType = Get-ProjectType $ModelType -Project $namespace 

	if($Delete)
	{
		$ServiceFile =  $Namespace +".Service\Services\" + $foundModelType.Name +"Service.cs"
		$ControllerFile =  $Project + "\Controllers\" + $foundModelType.Name + "Controller.cs" 
		$IndexFile = $Project + "\Views\" + $foundModelType.Name +"\index.cshtml" 
		if($ControlerName)
		{
			$ControllerFile =  $Project + "\Controllers\" + $ControlerName + "Controller.cs" 
			$IndexFile = $Project + "\Views\" +$ControlerName +"\index.cshtml" 
	} 
		If (Test-Path $IndexFile ){
			$solution.findprojectitem($IndexFile).delete()
				Write-Host "Deleted: " + $IndexFile
			if($ControlerName){
				$IndexFolder = $ProjectInstance.ProjectItems.Item("Views").ProjectItems.Item($ControlerName)
			}
			else {
				$IndexFolder = $ProjectInstance.ProjectItems.Item("Views").ProjectItems.Item($foundModelType.Name)
			} 
			$IndexFolder.delete()
				Write-Host "Deleted IndexFolder " $ControlerName
		}
		if (test-path $ServiceFile ){
			$solution.findprojectitem($ServiceFile).delete()
				Write-Host "Deleted: " + $ServiceFile
		}
		if (test-path $ControllerFile ){
			$solution.findprojectitem($ControllerFile ).delete()
			Write-Host "Deleted: " + $ControllerFile
		} 
		Write-Host " =>ALL CLEAN!<= "
	}
	else
	{
		$primaryKey = Get-PrimaryKey $ModelType -Project $Project
		$outputPathController = Join-Path Controllers/API ( $foundModelType.Name + "Controller" )
		$outputPathIndex = Join-Path Views ( $foundModelType.Name + "\Index" ) 
		if($ControlerName)
		{
			$outputPathController = Join-Path Controllers/API ( $ControlerName + "Controller" )
			$outputPathIndex = Join-Path Views ( $ControlerName + "\Index" ) 
		}
		
		Add-ProjectItemViaTemplate $outputPathController -Template APIController `
			-Model @{  ModelType = $foundModelType ; RelatedEntities = $relatedEntities; PrimaryKey = $primaryKey; Namespace = $namespace ; ControllerName = $ControlerName; } `
			-SuccessMessage "    Đã Gen Xong file {0}" `
			-TemplateFolders $TemplateFolders -Project $Project  -CodeLanguage $CodeLanguage -Force:$Force -Verbose 
	}
}