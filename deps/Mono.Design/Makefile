SOURCES=`cat Mono.Design.sources`
BUILD_DIR=..
ASSEMBLY=${BUILD_DIR}/Mono.Design.dll
REFERENCES=System.Design,System.Windows.Forms,System.Drawing

all: generate-md net-2

net-2:
	export MCS_COLORS=disable;gmcs -d:NET_2_0,DEBUG -target:library -debug -r:${REFERENCES} -out:${ASSEMBLY} ${SOURCES}

net-1:
	export MCS_COLORS=disable;mcs -d:DEBUG,NET_1_1 -t:library -debug -r:${REFERENCES} -out:${ASSEMBLY} ${SOURCES}

clean:
	rm -f .generated

update: clean
	make generate-md

generate-md:
ifeq (,$(findstring .generated,$(wildcard .*)))
	python generate-mono-design.py
	touch .generated
endif
