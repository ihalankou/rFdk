if(!require("data.table"))  
 install.packages("data.table") 
 library("data.table") 
if!(require("stringi")) 
 install.packages("stringi") 
 library("stringi") 
installBinaryHttr <- function(fdkRLibPackage){
  basicUrl = "https://github.com/SoftFx/FdkRLib/raw/master/dist/"
  fullUrl = paste(basicUrl, fdkRLibPackage, sep = "")
  download.file(fullUrl,destfile = fdkRLibPackage, method = "libcurl")
  
  install.packages(fdkRLibPackage, repos = NULL, type = "source", dependencies = TRUE)
  file.remove(fdkRLibPackage)
}
installBinaryHttr("rClr_0.7-4.zip")
installBinaryHttr("rFdk_1.0.20160315.zip") 
