let teacher = document.querySelector("#DataTextField");

teacher.addEventListener("change", () => {
    let dataDictionary = {
        teacherId: +teacher.value,
        viewName: "ShowLoadsByTeacherIdForReport"
    };

    ajaxQuery("GET", "/Load/ShowLoadsByTeacherId", undefined, dataDictionary, "#result");
});

document.addEventListener("click", (e) => {
    let startPeriod = document.querySelector("#StartPeriod");
    let finishPeriod = document.querySelector("#FinishPeriod");
    let hours = document.querySelectorAll(".hours");
    let dataContent = e.target.getAttribute("data-content");
    let allHours = 0;
    for (var i = 0; i < hours.length; i++) {
        allHours += +hours[i].innerText;
    }

    if (dataContent == "report-build") {
        let dataDictionary = {
            teacherId: +teacher.value,
            startPeriod: startPeriod.value,
            finishPeriod: finishPeriod.value,
            hours: allHours
        };

        ajaxQuery("GET", "/Report/GetReportData", undefined, dataDictionary, "#report-result");

        dataDictionary = {
            teacherId: +teacher.value,
            viewName: "ShowLoadsByTeacherIdForReport"
        };

        ajaxQuery("GET", "/Load/ShowLoadsByTeacherId", undefined, dataDictionary, "#report-table-result");
    }
});

function ajaxQuery(ajaxType, ajaxUrl, loadElement, dataDictionary, resultId) {
    $.ajax({
        type: ajaxType,
        url: ajaxUrl,
        beforeSend: () => {
            $(loadElement).show();
        },
        complete: () => {
            $(loadElement).hide();
        },
        onBegin: "ajaxBegin",
        data: dataDictionary,
        success: (data) => {
            $(resultId).html(data);
        }
    });
}