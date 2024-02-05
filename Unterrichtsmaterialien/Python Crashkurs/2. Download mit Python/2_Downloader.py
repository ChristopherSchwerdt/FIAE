import requests

#Lade die komplette Datei in den Zwischenspeicher:
res = requests.get('http://christopher-schwerdt.de/sherlock.txt')
#Prüfe, ob alles okay. Ansonsten werfe Exeption
res.raise_for_status()

#Öffne neue Datei im Filesystem als "Binary Writer"
playFile = open('Sherlock.txt', 'wb')
# Schreibe Daten vom res Objekt in die Datei:
# iteriere durch die Daten vom res Objekt(Einhunderttausend bytes je chunk).
for chunk in res.iter_content(100000):
        playFile.write(chunk)

#Dannach schließe den Writer
playFile.close()

