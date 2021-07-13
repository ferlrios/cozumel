let today = new Date();
let dayNumber = today.getDate();
let dayNameNum = today.getDay();
let dayName;
let weekDays = new Array("Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");
for (let i = 1; i < 15; i++) {
    console.log(dayNameNum);
    /*switch (dayNameNum) {
        case 1, 8:
            dayName = "Lunes";
            console.log("1");
            break;
        case 2, 9, 16:
            dayName = "Martes";
            console.log("2");
            break;
        case 3, 10, 17:
            dayName = "Miércoles";
            console.log("3");
            break;
        case 4, 11, 18:
            dayName = "Jueves";
            break;
        case 5, 12, 19:
            dayName = "Viernes";
            break;
        case 6, 13, 20:
            dayName = "Sábado";
            break;
        case 7, 14, 20:
            dayName = "Domingo";
            break;
    }
    console.log(dayName);*/
    $(`.grid-item:nth-child(${i})`).append(`<span class="day">${weekDays[dayNameNum]}</span>
            <span class="number">${dayNumber}</span>`);
    dayNumber++;
    dayNameNum++;
    dayNameNum == 7 ? dayNameNum = 0 : dayNameNum = dayNameNum;

}
