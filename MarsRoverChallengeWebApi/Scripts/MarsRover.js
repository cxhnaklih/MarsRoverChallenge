var uriPlateau = 'api/marsrover/plateau';
var uriPlateauNameValidation = 'api/marsrover/plateauNameAvailable/';
var uriRoverAddition = '/rover';

function getPlateauUri(plateauId) {
    return uriPlateau + '/' + plateauId

}

function getRoverUri(plateauId, roverId) {
    return getPlateauUri(plateauId) + uriRoverAddition + '/' + roverId

}

function getSelectedPlateauId() {
    return $('#plateauItems option:selected').val();
}

function getSelectedRoverId() {
    return $('#roverItems option:selected').val();
}

function executeRoverCommands(plateauid, roverid, commands) {
    makeAlertVisible(false);

    if (commands.length > 0 && (!isNaN(roverid)) && (!isNaN(plateauid))) {
        var uri = getRoverUri(plateauid, roverid) + '/' + commands;

        $.ajax({
            url: uri,
            type: 'get',
            error: function (err) {
                executeCommandError(err.responseJSON);
            }
        }
            )
           .done(function (data) {
               updateFinalDestination(data.X, data.Y, data.Direction);

           });
    }
}

function executeCommandError(err) {

    $('#ErrorMessage').html(err.Message);
    makeAlertVisible(true);
}

function updateFinalDestination(x, y, direction) {
    makeAlertVisible(false);


    $('#FinalRoverX').html("End X :" + x);

    $('#FinalRoverY').html("End Y :" + y);

    $('#FinalRoverDir').html("End Direction :" + direction);
    locateRover(x, y, '#FFD7FF')
}

function createRover(plateauid) {
    if (!isNaN(plateauid)) {
        var width = $('#plateauItems option:selected').attr('xwidth');
        var height = $('#plateauItems option:selected').attr('xheight');

        name = uniqueNameInputBox("What is the rover name", $('#roverItems'));
        x = boundedNumericInputBox("What is the starting X Coord", width, 0);
        y = boundedNumericInputBox("What is the starting Y Coord", height, 0);
        dir = NSEWInputBox("What is the starting Direction (N/S/E/W)");


        var rover = { Direction: dir, InitialX: x, InitialY: y, Name: name, PlateauId: plateauid };


        $.ajax({
            type: "POST",
            data: JSON.stringify(rover),
            url: getRoverUri(plateauid, ""),
            contentType: "application/json"

        }).done(function () { getAllRovers(plateauid) });


    }

}

function makeAlertVisible(visible) {
    if (visible == true) {
        $('#bs-alert').css('display', 'block');
    }
    else {
        $('#bs-alert').css('display', 'none');
    }
}

function updateWidthAndHeight() {

    var width = $('#plateauItems option:selected').attr('xwidth');
    var height = $('#plateauItems option:selected').attr('xheight');
    var plateauid = $('#plateauItems option:selected').val();

    if (width)
        $('#selectedPlateauWidth').html("Width :" + width);
    if (height)
        $('#selectedPlateauHeight').html("Height :" + height);
    if(width && height)
        drawCanvas(width, height);
    getAllRovers(plateauid);
}

function updateStartingPoint() {
    var x = $('#roverItems option:selected').attr('xCoord');
    var y = $('#roverItems option:selected').attr('yCoord');
    var direction = $('#roverItems option:selected').attr('direction');


    if (x)
        $('#selectedRoverX').html("Start X :" + x);
    if (y)
        $('#selectedRoverY').html("Start Y :" + y);
    if (direction)
        $('#selectedRoverDir').html("Start Direction :" + direction);
    locateRover(x,y,'#FFD700' )
}

$(document).ready(function () {
    getAllPlateaus()


});

function getAllPlateaus() {
    $.getJSON(uriPlateau)
        .done(function (data) {

            populatePlateaus(data);
        });


}

function boundedNumericInputBox(message, uboundary, lboundary) {
    result = numericInputBox(message);
    message = message + "\n CAN'T BE OUTSIDE OF THE BOUNDARY";
    while ((result > uboundary) || (result < lboundary)) {
        result = boundedNumericInputBox(message, uboundary, lboundary);
    }
    return result;
}

function numericInputBox(message) {
    result = prompt(message, "");
    message = message + '\n PLEASE NOTE MUST BE NUMERIC!'
    while (isNaN(result)) {
        result = prompt(message, "")
    }
    return parseInt(result);
}

function NSEWInputBox(message) {
    console.group("NSEWInputBox")
    result = prompt(message, "");
    console.log(result);
    message = message + '\n PLEASE NOTE MUST BE N/S/E/W!'

    var patt = new RegExp('[^NSEW]+')

    if (!result)
        result = "";


    while (patt.test(result.toUpperCase()) == true || result.length > 1) {
        result = prompt(message, "")
    }
    console.groupEnd();
    return result;
}

function uniqueNameInputBox(message, mySelect) {
    result = prompt(message, "");

    if (mySelect.children().length > 0) {
        for (i = 0; i < mySelect.children().length; i++) {

            if (mySelect.children()[i].innerHTML.toUpperCase() == result.toUpperCase()) {
                message = message + '\n PLEASE NOTE MUST BE A UNIQUE NAME!'
                return uniqueNameInputBox(message, mySelect)
            }

        }
    }
    return result;
}


function getAllRovers(plateauId) {
    if (!isNaN(plateauId)) {
        $.getJSON(getRoverUri(plateauId, ""))
            .done(function (data) {
                populateRovers(data);
            });
    }
}

function deleteRover(roverId) {
    var plateauid = getSelectedPlateauId();
    var rover = { PlateauId: plateauid, MarsRoverId: roverId };


    $.ajax({
        type: "DELETE",
        data: JSON.stringify(rover),
        url: getRoverUri(plateauid, roverId),
        contentType: "application/json"
    });
    $('#roverItems option:selected').remove();
}

function deletePlateau(plateauId) {
    var plateau = { PlateauId: plateauId };
    $.ajax({
        type: "DELETE",
        data: JSON.stringify(plateau),
        url: getPlateauUri(plateauId),
        contentType: "application/json"
    });
    $('#plateauItems option:selected').remove();
    getAllRovers(getSelectedPlateauId());
}

function populatePlateaus(data) {
    var mySelect = $('#plateauItems');
    if (mySelect.children().length > 0)
        mySelect.find('option').remove().end();
    $.each(data, function (i, item) {
        var optionItem = $('<option></option>').val(data[i].PlateauId).html(data[i].Name);
        optionItem.attr("xwidth", data[i].Width);
        optionItem.attr("xheight", data[i].Length);
        mySelect.append(optionItem);
    })
    mySelect.ready(updateWidthAndHeight);

}

function populateRovers(data) {
    var mySelect = $('#roverItems');
    if (mySelect.children().length > 0)
        mySelect.find('option').remove().end();
    $.each(data, function (i, item) {
        var optionItem = $('<option></option>').val(data[i].MarsRoverId).html(data[i].Name);
        optionItem.attr("xCoord", data[i].InitialX);
        optionItem.attr("yCoord", data[i].InitialY);
        optionItem.attr("direction", data[i].Direction);

        mySelect.append(optionItem);
    })
    window.setTimeout(updateStartingPoint, 100);
}




function createPlateau() {

    name = uniqueNameInputBox("What is the plateau name", $('#plateauItems'));
    width = numericInputBox("What is the width of the plateau");
    length = numericInputBox("What is the length of the plateau");

    /*var validationResult;
    $.getJSON(uriPlateauNameValidation + name, function (data) {
        JSON.stringify(data);
    });*/


    var plateau = { Width: width, Length: length, Name: name };


    $.ajax({
        type: "POST",
        data: JSON.stringify(plateau),
        url: uriPlateau,
        contentType: "application/json"
    }).done(function () {
        getAllPlateaus();
    });

}


function drawCanvas(width, height) {
    var canvas = $("#canvas")[0];
    canvas.width = width*10;
    canvas.height = height*10;
    var context = canvas.getContext("2d");
    context.clearRect(0, 0, canvas.width, canvas.height)
    var x=0;
    var y=0;
    for (x = 0; x<=width; x++)
    {
        for (y = 0; y <= height; y++)
        {
            drawsq(context, x*10, y*10, 10)
        }
    }
    context.stroke();
    
    

}

function locateRover(x,y,fillStyle)
{
    var canvas = $("#canvas")[0];
    var context = canvas.getContext("2d");
    context.moveTo(x * 10, y * 10)
    context.fillStyle = fillStyle;
    context.fillRect((x-1 ) * 10, canvas.height -((y ) * 10), 10, 10)

}

function drawsq(context, x, y, size) {
    context.moveTo(x, y)
    context.lineTo(x + size, y);
    context.lineTo(x + size, y + size);
    context.lineTo(x, y + size);
    context.lineTo(x , y );
    
}