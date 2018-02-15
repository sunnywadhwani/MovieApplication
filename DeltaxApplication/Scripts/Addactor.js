
$(document).ready(function () {
   
    $('#actid').click(function () {
        $('#myModal').modal();

    });

    $('#prodid').click(function () {
        $('#myModalproducer').modal();

    });


    $('#actorbuttonid').click(function () {
        var p = { Name: $("#actornameid").val(), DOB: $("#actorbdayid").val(), BIO: $("#actorbioid").val(), SEX: $("input:radio[name=gender]:checked").val() };


        var name = $("#actornameid").val();
        var valid = true;
       
        if (name === undefined || name === null || name === "") {
            valid = false;
            $('#actorlabel').html("Name field is required");
        }

        


        if (valid)
        {
            $.ajax({
                type: "POST",
                data: JSON.stringify(p),
                contentType: "application/json",
                dataType: "json",
                url: "/Actor/Save",
                success: function(response) {

                    var opt = $("<option></option>").text(response.actor.Name).val(response.actor.Id);


                    $("#actorddl").append(opt);
                    $("#actorddl").trigger("chosen:updated");
                    $("#myModal").modal("toggle");

                },
                error: function(errorResponse) {
                    console.log("failed");
                }


            });

        }
    });


    $('#producerbuttonid').click(function () {


        var p = {
            Name: $("#producernameid").val(),
            DOB: $("#producerbdayid").val(),
            BIO: $("#producerbioid").val(),
            SEX: $("input:radio[name=gen]:checked").val()
        };

        /*   if ($("#producernameid").val()=="")
           {
   
   
   
               $('#myModalproducer').modal({
                   backdrop: 'static',
                   keyboard: false
               })
               
           }
           else*/

        $.ajax({
            type: "POST",
            data: JSON.stringify(p),
            contentType: "application/json",
            dataType: "json",
            url: "/Producer/Save",
            success: function (response) {

                var opt = $("<option></option>").text(response.producer.Name).val(response.producer.Id);


                $("#producerddl").append(opt);
                $("#producerddl").trigger("chosen:updated");
                $("#myModalproducer").modal("toggle");

            },
            error: function (errorResponse) {
                console.log("failed");
            }

        });




    });































});