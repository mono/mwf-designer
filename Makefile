SOURCES=src/*.cs src/*/*.cs src/*/*/*.cs
BUILD_DIR=build
MD_BUILD_DIR=../build
DEPS_DIR = deps
DEPS = ${DEPS_DIR}/*
DIST_DIR = ${BUILD_DIR}/mwf-designer-dist
MD_DIST_DIR = ${MD_BUILD_DIR}/mwf-designer
ASSEMBLY=mwf-designer.exe
REFERENCES=System.Design,System.Windows.Forms,System.Drawing,System.Data,${DEPS_DIR}/ICSharpCode.NRefactory.dll
MD_REFERENCES=System.Design,System.Windows.Forms,System.Drawing,System.Data,${MD_BUILD_DIR}/ICSharpCode.NRefactory.dll

all:
	mkdir -p ${BUILD_DIR}
	cp ${DEPS_DIR}/ICSharpCode.NRefactory.dll ${BUILD_DIR}
	export MCS_COLORS=disable;gmcs -debug -r:${REFERENCES} -out:${BUILD_DIR}/${ASSEMBLY} ${SOURCES}

run:
	cd ${BUILD_DIR};mono --debug mwf-designer.exe

msnet:
	csc -debug -d:NET_2_0 -t:library -r:System.Design.dll,System.Windows.Forms.dll,System.Drawing.dll,System.Data.dll,..\build\Mono.Design.dll,..\build\ICSharpCode.NRefactory.dll -out:build\mwf-designer.exe src\\*.cs src\\*\\*.cs

dist:
	mkdir -p ${DIST_DIR}
	cp ${BUILD_DIR}/Mono.Design.* ${DIST_DIR}
	cp ${BUILD_DIR}/ICSharpCode.NRefactory.* ${DIST_DIR}
	cp ${BUILD_DIR}/mwf-designer.* ${DIST_DIR}
	cd ${BUILD_DIR};tar -c mwf-designer | bzip2 -c > mwf-designer.tar.bz2

mono-design:
	mkdir -p ${MD_BUILD_DIR}
	cp ${DEPS} ${MD_BUILD_DIR}
	export MCS_COLORS=disable;gmcs -d:WITH_MONO_DESIGN -debug -r:${MD_REFERENCES} -r:${MD_BUILD_DIR}/Mono.Design.dll -out:${MD_BUILD_DIR}/${ASSEMBLY} ${SOURCES}

mono-design-run:
	cd ${MD_BUILD_DIR};mono --debug mwf-designer.exe

mono-design-msnet:
	csc -debug -d:NET_2_0 -t:library -r:System.Design.dll,System.Windows.Forms.dll,System.Drawing.dll,System.Data.dll,..\build\Mono.Design.dll,..\build\ICSharpCode.NRefactory.dll -out:..\build\mwf-designer.exe src\*.cs src\*\*.cs



