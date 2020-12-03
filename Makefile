SOURCES=src/*.cs src/*/*.cs src/*/*/*.cs
BUILD_DIR=build
DEPS_DIR = deps
DEPS = ${DEPS_DIR}/*.dll
DIST_DIR = ${BUILD_DIR}/mwf-designer-dist
MD_DIST_DIR = ${MD_BUILD_DIR}/mwf-designer
ASSEMBLY=mwf-designer.exe
REFERENCES=System.Design,System.Windows.Forms,System.Drawing,System.Data,${DEPS_DIR}/ICSharpCode.NRefactory.dll

all: ${BUILD_DIR}/${ASSEMBLY}

${BUILD_DIR}/${ASSEMBLY}: ${SOURCES}
	mkdir -p ${BUILD_DIR}
	MCS_COLORS=disable mcs -debug -r:${REFERENCES} -out:${BUILD_DIR}/${ASSEMBLY} ${SOURCES}

run: all
	cp ${DEPS_DIR}/*.dll ${BUILD_DIR}
	cp ${DEPS_DIR}/*.mdb ${BUILD_DIR} || true
	mono --debug ${BUILD_DIR}/mwf-designer.exe

mono-design: all
	cd ${DEPS_DIR}/Mono.Design && make
	export MCS_COLORS=disable;mcs -debug -r:${REFERENCES},${DEPS_DIR}/Mono.Design.dll -out:${BUILD_DIR}/${ASSEMBLY} ${SOURCES}

mono-design-update:
	cd ${DEPS_DIR}/Mono.Design && make update

clean:
	rm -rf build/ deps/Mono.Design/.generated deps/Mono.Design/class/
