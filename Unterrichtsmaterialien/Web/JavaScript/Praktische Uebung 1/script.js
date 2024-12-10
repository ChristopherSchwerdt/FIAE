// 1. Variablen
function startExercise() {
    let message = "Willkommen bei JavaScript!";
    let number = 10;

    // 2. Bedingung
    if (number > 5) {
        message += " Die Zahl ist größer als 5.";
    } else {
        message += " Die Zahl ist 5 oder kleiner.";
    }

    // 3. Schleife
    let sum = 0;
    for (let i = 1; i <= number; i++) {
        sum += i;
    }

    // 4. Funktion
    function calculateSquare(num) {
        return num * num;
    }

    let square = calculateSquare(number);

    // 5. Ausgabe in die HTML-Seite
    document.getElementById("output").innerHTML = `
        <p>${message}</p>
        <p>Die Summe von 1 bis ${number} ist: ${sum}</p>
        <p>Das Quadrat von ${number} ist: ${square}</p>
    `;
}
