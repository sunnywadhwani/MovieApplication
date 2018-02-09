
$(document).ready(function () {
   



    $('#actorbuttonid').click(function () {
        var p = { Name: $("#actornameid").val(), DOB: $("#actorbdayid").val(), BIO: $("#actorbioid").val(), SEX: $("input:radio[name=gender]:checked").val() };

        $.ajax({
            type: "POST",
            data: JSON.stringify(p),
            contentType: "application/json",
            dataType: "json",
            url: "/Actor/Save",
            success: function (response) {

                var opt = $("<option></option>").text(response.actor.Name).val(response.actor.Id);


                $("#actorddl").append(opt);
                $("#actorddl").trigger("chosen:updated");
                $("#myModal").modal("toggle");

            },
            error: function (errorResponse) {
                console.log("failed");
            }

          
        });
        

    });
});