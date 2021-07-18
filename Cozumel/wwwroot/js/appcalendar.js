let today = new Date();
let dayNumber = today.getDate();
let dayNameNum = today.getDay();
let dayName;
let weekDays = new Array("Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");
for (let i = 0; i < 14; i++) {
    console.log(dayNameNum);
    $(`#${i}`).append(`<span class="day">${weekDays[dayNameNum]}</span>
            <span class="number">${dayNumber}</span>`);
    dayNumber++;
    dayNameNum++;
    dayNameNum == 7 ? dayNameNum = 0 : dayNameNum = dayNameNum;

}
