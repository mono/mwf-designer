
This repository is very old Google Summer of Code project work, which is not maintened nor relevant nowadays. Most of the "Design-Time" code was at the time merged into the Mono class library.





=== On Mono ===

make && make run

=== On Microsoft .NET with Visual Studio ===

1) Install Python 3 - http://python.org/download/
2) Run "Prepare Visual Studio Build.bat"
3) Open mwf-designer.sln with Visual Studio and you are done!

What is happening automatically behind the scenes is the generation of a 
Mono.Design assembly - a subset of Mono's System.Design. 

The python script will:
 - Download a subset of Mono System.Design assembly's source code from SVN
 - Apply a set of patches to make it run against Microsoft .NET
 - Change namespaces to "Mono.Design".

=== On Mono with Mono.Design ===

make mono-design && make run

=== Status and Documentation ===

http://www.mono-project.com/WinForms_Designer
