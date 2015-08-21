
if(!require(roxygen2)){
  install.packages("roxygen2", repos="http://cran.us.r-project.org")
  require(roxygen2)
}

if(!require(devtools)){
  install.packages("devtools", repos="http://cran.us.r-project.org")
  require(devtools)
}

if(!require(data.table)){
  install.packages("data.table", repos="http://cran.us.r-project.org")
  require(data.table)
}

installBinaryHttr <- function(fdkRLibPackage){
  basicUrl = "https://github.com/SoftFx/FdkRLib/raw/dev/dist/"
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
