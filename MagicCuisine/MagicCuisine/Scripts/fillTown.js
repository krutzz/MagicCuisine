function FillTown() {
    var countryId = $('#Country').val();
    $.ajax({
        url: '/Account/FillTown',
        type: "GET",
        dataType: "JSON",
        data: { countryId: countryId },
        success: function (towns) {
            $("#Town").html(""); // clear before appending new list
            $.each(towns, function (i, town) {
                $("#Town").append(
                    $('<option></option>').val(town.ID).html(town.Name));
            });
        }
    });
}