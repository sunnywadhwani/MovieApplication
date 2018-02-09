$(document).ready(function ()
{
   

    $('#producerbuttonid').click(function() {


        var p = {
            Name: $("#producernameid").val(),
            DOB: $("#producerbdayid").val(),
            BIO: $("#producerbioid").val(),
            SEX: $("input:radio[name=gen]:checked").val()
        };

        if ("#producernameid" == null) {
            $("#myModalproducer").modal({ "backdrop": "static" });
        } else {
            $.ajax({
                type: "POST",
                data: JSON.stringify(p),
                contentType: "application/json",
                dataType: "json",
                url: "/Producer/Save",
                success: function(response) {

                    var opt = $("<option></option>").text(response.producer.Name).val(response.producer.Id);


                    $("#producerddl").append(opt);
                    $("#producerddl").trigger("chosen:updated");
                    $("#myModalproducer").modal("toggle");

                },
                error: function(errorResponse) {
                    console.log("failed");
                }

            });
    }

    });

});