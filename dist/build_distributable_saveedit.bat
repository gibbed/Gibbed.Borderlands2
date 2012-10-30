@echo off
call "%VS100COMNTOOLS%vsvars32.bat"
rmdir /s /q "saveedit"
mkdir "saveedit"
msbuild "..\Borderlands 2.sln" /p:Configuration="SaveEdit Packaging" /p:OutputPath="%cd%\saveedit"
copy /y "Gibbed.Borderlands2.SaveEdit.exe.config" "saveedit\Gibbed.Borderlands2.SaveEdit.exe.config"
copy /y "..\license.txt" "saveedit\license.txt"
copy /y "..\readme.txt" "saveedit\readme.txt"
svn log ".." > "saveedit\revisions.txt"
cd "saveedit"
rem mkdir pdbs
rem move *.pdb pdbs\
del *.pdb
mkdir assemblies
move *.dll assemblies\
del *.xml
7z a -r -tzip -mx=9 ..\saveedit.zip .
cd ".."
pause