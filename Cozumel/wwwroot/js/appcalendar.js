/*$(document).ready(function () {
    let today = new Date();
    let dayNumber = today.getDate();
    let dayNameNum = today.getDay();
    let month = today.getMonth();
    let dayName;
    let weekDays = new Array("Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");
    for (let i = 0; i < 14; i++) {
        console.log(dayNameNum);
        console.log(dayNumber);
        $(`#${i}`).append(`<span class="day">${weekDays[dayNameNum]}</span>
            <span class="number">${dayNumber}</span>`);
        dayNumber++;
        dayNameNum++;
        if ((month == 0 || month == 2 || month == 4 || month == 6 || month == 7 || month == 9 || month == 11) && (dayNumber > 31)) {
            dayNumber = 1;
        }
        if ((month == 3 || month == 5 || month == 8 || month == 10) && (dayNumber > 30)) {
            dayNumber = 1;
        }
        if ((month == 1) && (dayNumber > 28)) {
            dayNumber = 1;
        }
        dayNameNum == 7 ? dayNameNum = 0 : dayNameNum = dayNameNum;

    }
});*/

