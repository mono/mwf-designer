SOURCES=src/*.cs src/*/*.cs src/*/*/*.cs
BUILD_DIR=build
DEPS_DIR = deps
DEPS = ${DEPS_DIR}/*.dll
DIST_DIR = ${BUILD_DIR}/mwf-designer-dist
MD_DIST_DIR = ${MD_BUILD_DIR}/mwf-designer
ASSEMBLY=mwf-designer.exe
REFERENCES=System.Design,System.Windows.Forms,System.Drawing,System.Data,${DEPS_DIR}/ICSharpCode.NRefactory.dll

all: prepare
	export MCS_COLORS=disable && gmcs -debug -r:${REFERENCES} -out:${BUILD_DIR}/${ASSEMBLY} ${SOURCES}

prepare:
	mkdir -p ${BUILD_DIR}

run: prepare
	cp ${DEPS_DIR}/*.dll ${BUILD_DIR}
	cp ${DEPS_DIR}/*.mdb ${BUILD_DIR}
	cd ${BUILD_DIR} && mono --debug mwf-designer.exe

mono-design: prepare
	cd ${DEPS_DIR}/Mono.Design && make
	export MCS_COLORS=disable;gmcs -debug -r:${REFERENCES},${DEPS_DIR}/Mono.Design.dll -out:${BUILD_DIR}/${ASSEMBLY} ${SOURCES}

mono-design-update:
	cd ${DEPS_DIR}/Mono.Design && make update
	

