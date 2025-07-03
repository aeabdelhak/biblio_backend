
@ECHO OFF

git pull

docker build -t aeabdelhak/biblio_backend:latest .

docker push aeabdelhak/biblio_backend:latest


ECHO. (Press any key to close the window...)
PAUSE