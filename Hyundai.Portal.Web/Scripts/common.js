var arrayToJSON = function (serializedForm) {
    var data = {};
    for (var cnt in serializedForm) {
        if (data[serializedForm[cnt].name] == undefined)
            data[serializedForm[cnt].name] = serializedForm[cnt].value;
    }
    return data;
}

var selectFromJSON = function (jsonobj, field, value) {
    for (var obj in jsonobj) {
        if (jsonobj[obj][field] == value)
            return jsonobj[obj];
    }
    return null;
}

String.format = function (text) {
    //check if there are two arguments in the arguments list
    if (arguments.length <= 1) {
        //if there are not 2 or more arguments there's nothing to replace
        //just return the original text
        return text;
    }
    //decrement to move to the second argument in the array
    var tokenCount = arguments.length - 2;
    for (var token = 0; token <= tokenCount; token++) {
        //iterate through the tokens and replace their
        // placeholders from the original text in order
        text = text.replace(new RegExp("\\{" + token + "\\}", "gi"),
                                                arguments[token + 1]);
    }
    return text;
};