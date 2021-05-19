let teacher = document.querySelector("#DataTextField");

teacher.addEventListener("change", () => {
    let dataDictionary = {
        teacherId: +teacher.value,
        viewName: "ShowLoadsByTeacherIdForReport"
    };

    ajaxQuery("GET", "/Load/ShowLoadsByTeacherId", undefined, dataDictionary, "#result");
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