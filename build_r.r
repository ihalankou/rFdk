quickRequire <- function(packageName){
	if(!require(packageName)){
		install.packages(packageName, repos="http://cran.us.r-project.org")
		require(packageName)
	}
  
}

quickRequire("roxygen2")
quickRequire("devtools")
quickRequire("data.table")

installBinaryHttr <- function(fdkRLibPackage){
  basicUrl = "https://github.com/SoftFx/FdkRLib/raw/master/dist/"
  fullUrl = paste(basicUrl, fdkRLibPackage, sep = "")
  download.file(fullUrl,destfile = fdkRLibPackage, method = "libcurl")
  
  install.packages(fdkRLibPackage, repos = NULL, type = "source", dependencies = TRUE)
  file.remove(fdkRLibPackage)
}
installBinaryHttr("rClr_0.7-4.zip")
require(rClr)

setwd("RPackage")
require(devtools)

devtools::document(roclets=c('rd', 'collate', 'namespace'))
packPath <- devtools::build(binary = TRUE, args = c('--preclean'))
