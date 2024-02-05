import requests,sys

if len(sys.argv) > 1:
        format = sys.argv[1]
        pathToContent= sys.argv[2]

print(format)
print(pathToContent)

#Lade die komplette Datei in den Zwischenspeicher:
res = requests.get(pathToContent)
#Prüfe, ob alles okay. Ansonsten werfe Exeption
res.raise_for_status()

#Öffne neue Datei im Filesystem als "Binary Writer"
playFile = open('Download.' +format, 'wb')
# Schreibe Daten vom res Objekt in die Datei:
# iteriere durch die Daten vom res Objekt(Einhunderttausend bytes je chunk).
for chunk in res.iter_content(100000):
        playFile.write(chunk)

#Dannach schließe den Writer
playFile.close()

