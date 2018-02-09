
$('#producerform').on('submit', function(e) {
    var Name = $('#producernameid');

        // Check if there is an entered value
        if(!Name.val()) {
            // Add errors highlight
            Name.closest('.form-group').removeClass('has-success').addClass('has-error');

            // Stop submission of the form
            e.preventDefault();
        } else {
            // Remove the errors highlight
            Name.closest('.form-group').removeClass('has-error').addClass('has-success');
        }
    });
