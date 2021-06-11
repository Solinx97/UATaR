let teacher = document.querySelector("#DataTextField");

teacher.addEventListener("change", () => {
    let dataDictionary = {
        teacherId: +teacher.value,
        viewName: "ShowLoadsByTeacherIdForReport"
    };

    ajaxQuery("GET", "/Load/ShowLoadsByTeacherId", undefined, dataDictionary, "#result");

    let reportBuildBtn = document.querySelector("#report-build-btn");
    reportBuildBtn.removeAttribute("hidden");
});

document.addEventListener("click", (e) => {
    let startPeriod = document.querySelector("#StartPeriod");
    let finishPeriod = document.querySelector("#FinishPeriod");
    let hours = document.querySelectorAll(".hours");
    let dataContent = e.target.getAttribute("data-content");
    let allHours = 0;
    let dataDictionary = {};

    for (var i = 0; i < hours.length; i++) {
        allHours += +hours[i].innerText;
    }

    switch (dataContent) {
        case "report-build":
            dataDictionary = {
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

            let documentCreateBtn = document.querySelector("#document-create-btn");
            documentCreateBtn.removeAttribute("hidden");
            break;
        case "document-create":
            let errorMessagePeriod = document.querySelector("#not-set-period");
            let incorrectErrorMessagePeriod = document.querySelector("#incorrect-period");
            incorrectErrorMessagePeriod.setAttribute("hidden", true);
            errorMessagePeriod.setAttribute("hidden", true);

            if ((startPeriod.value.length > 0 && finishPeriod.value.length > 0)
                && startPeriod.value < finishPeriod.value) {
                documentCreate(startPeriod, finishPeriod);
            }
            else {
                if (startPeriod.value > finishPeriod.value) {
                    incorrectErrorMessagePeriod.removeAttribute("hidden");
                }
                else {
                    errorMessagePeriod.removeAttribute("hidden");
                }
            }
            break;
        default:
            break;
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

function documentCreate(startPeriod, finishPeriod) {
    let fullName = document.querySelector("#FullName");
    let education = document.querySelector("#Education");
    let hours = document.querySelectorAll("#report-table-result .hours");
    let loadTypeHours = document.querySelectorAll("#report-table-result #LoadTypeHours");
    let loadTypes = document.querySelectorAll("#report-table-result .loadType");
    let hoursesForLoadType = {};
    allHours = 0;

    for (var i = 0; i < hours.length; i++) {
        allHours += +hours[i].innerHTML;
    }

    for (var j = 0; j < loadTypes.length; j++) {
        let property = loadTypes[j].innerHTML;
        hoursesForLoadType[property] = "";
    }

    let count = 0;
    for (var i = 0; i < loadTypeHours.length; i++) {
        for (var j = 0; j < loadTypes.length; j++) {
            if (i + count < loadTypeHours.length) {
                let property = loadTypes[j].innerHTML;
                hoursesForLoadType[property] += loadTypeHours[i + count].value + ';';
                count++;
            }
        }
        i += count - 1;
        count = 0;
    }

    dataDictionary = {
        report: {
            Id: 0,
            ExecutorFullName: fullName.value,
            Education: education.value,
            Hours: allHours,
            StartPeriod: startPeriod.value,
            FinishPeriod: finishPeriod.value
        },
        hoursesForLoadType: hoursesForLoadType,
    };

    ajaxQuery("POST", "/Report/CreateDocument", undefined, dataDictionary, "#document-create");
}