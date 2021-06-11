let teacher = document.querySelector("#DataTextField");
let saveExecuteLoad = document.querySelector("#SaveExecuteLoad");

let addAntiForgeryToken = function (data) {
    data.__RequestVerificationToken = $('#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
    return data;
}

teacher.addEventListener("change", () => {
    let dataDictionary = {
        teacherId: +teacher.value,
        viewName: "ShowLoadsByTeacherIdForExecute"
    };

    ajaxQuery("GET", "/Load/ShowLoadsByTeacherId", undefined, dataDictionary, "#result");
});

document.addEventListener("click", (event) => {
    let dataContent = event.target.getAttribute("data-content");
    if (dataContent == "saveExecuteLoad") {
        let executeLoadId = document.querySelectorAll("#ExecuteLoadId")[0];
        if (executeLoadId.value > 0) {
            updateExecuteLoad();
        }
        else {
            createExecuteLoad();
        }
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
        },
        error: (jqXHR, exception) => {
            console.log(jqXHR);
        }
    });
}

function createExecuteLoad() {
    let loadId = document.querySelectorAll("#LoadId");
    let hours = document.querySelectorAll("#Hours");
    for (var i = 0; i < loadId.length; i++) {
        let dataDictionary = {
            loadId: +loadId[i].value,
            hours: +hours[i].value
        };

        ajaxQuery("POST", "/ExecuteLoad/CreateExecuteLoad", undefined, dataDictionary);
        if (+hours[i].value < 0) break;
    }
}

function updateExecuteLoad() {
    let excecuteLoadId = document.querySelectorAll("#ExecuteLoadId");
    let loadId = document.querySelectorAll("#LoadId");
    let hours = document.querySelectorAll("#Hours");
    for (var i = 0; i < loadId.length; i++) {
        let dataDictionary = {
            id: +excecuteLoadId[i].value,
            loadId: +loadId[i].value,
            hours: +hours[i].value
        };

        ajaxQuery("POST", "/ExecuteLoad/UpdateExecuteLoad", undefined, dataDictionary, "#update-execute-load");
        if (+hours[i].value < 0) break;
    }
}

function renderErrorMessage(message) {
    let error = document.querySelector("#error span");
    console.log(error);
    error.textContent = message;
}   