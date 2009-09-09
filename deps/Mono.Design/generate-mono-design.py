#!/usr/bin/python
import os
import os.path
import re
import sys
import subprocess
import urllib.request, urllib.error

def main ():
    try:
        print ("--> Step 1: Fetching source code...")
        files = get_files ();
        fetch_source_code (files)
        print ("--> Step 2: Applying patches...")
        apply_patches (get_patches ())
        print ("--> Step 3: Replacing namespaces...")
        replace_namespaces (files)
        print ("Done!")
    except Exception as exc:
        print ("Unexpected Error: ")
        print (exc)

def fetch_source_code (files):
    for file in files:
        directory = os.path.dirname (file)
        if directory != "": # quick hack to only download assembly files and not the ones we already have
            if not os.path.exists (directory):
                os.makedirs (directory)
            for i in range (1, 4): # retry 3 times
                try:
                    webFile = urllib.request.urlopen (file_to_url (file))
                    localFile = open (file, 'wb+')
                    localFile.write (webFile.read())
                    webFile.close ()
                    localFile.close ()
                    print ("A " + file)
                    break
                except Exception as exc:
                    print ("E " + file)
                    print (exc)

def file_to_url (file):
    return "http://anonsvn.mono-project.com/viewvc/trunk/mcs/" + file + "?view=co"

def get_files ():
    try:
        file = open ("Mono.Design.sources")
        filesList = []
        for fileEntry in file:
            filesList.append (fileEntry.strip ())
        file.close ()
        return filesList
    except Exception:
        print ("Unable to open file: Mono.Design.sources")
        return None

def replace_namespaces (filesList):
    if filesList == None:
        return

    regexp = re.compile (r"namespace (.*?)( ?\{?)$", re.M)
    for currentFile in filesList:
        try:
            inputFile = open (currentFile)
            input = inputFile.read ()
            inputFile.close ()
            if input.find ("namespace Mono.Design") == -1:
                output = regexp.sub (r"using \1;" + os.linesep + r"namespace Mono.Design\2", input, 1)
                outputFile = open (currentFile, "w")
                outputFile.write (output)
                outputFile.flush ()
                outputFile.close ()
        except Exception as exc:
            print (exc)

def get_patches ():
    patches = []
    patchesDir = os.path.join (os.getcwd (), "patches")
    if os.path.exists (patchesDir):
        for item in os.listdir (patchesDir):
            patchFile = os.path.join (patchesDir, item)
            if os.path.isfile (patchFile) and patchFile.endswith (".patch"):
                patches.append (patchFile)
                patches.sort ()
    return patches

def file_lf_to_clrf (fileName):
        inputFile = open (fileName, "rb")
        input = inputFile.read ()
        inputFile.close ()
        output = re.sub (bytes ("\r?\n", "utf8"), bytes ("\r\n", "utf8"), input)
        outputFile = open (fileName, "wb")
        outputFile.write (output)
        outputFile.flush ()
        outputFile.close ()

def apply_patches (patches):
    for patch in patches:
        returnCode = 0
        if os.name == "nt":
            file_lf_to_clrf (patch); # fix line endings just in case
            returnCode = subprocess.call ("patches\\patch.exe -p0 -i \"" + patch + "\"")
        else:
            returnCode = subprocess.call ("patch -p0 -i \"" + patch + "\"")

        if returnCode < 0:
            print (os.path.basename (patch) + ": " + error)

if __name__ == "__main__":
        main ()
