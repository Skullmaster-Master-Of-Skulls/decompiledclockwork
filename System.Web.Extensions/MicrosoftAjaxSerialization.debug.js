//-----------------------------------------------------------------------
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------
// MicrosoftAjaxSerialization.js
// Microsoft AJAX Framework Serialization.
/// <reference name="MicrosoftAjaxCore.js" />
Type._registerScript("MicrosoftAjaxSerialization.js", ["MicrosoftAjaxCore.js"]);
Type.registerNamespace('Sys.Serialization');
Sys.Serialization.JavaScriptSerializer = function Sys$Serialization$JavaScriptSerializer() {
    /// <summary locid="M:J#Sys.Serialization.JavaScriptSerializer.#ctor" />
    if (arguments.length !== 0) throw Error.parameterCount();
}
Sys.Serialization.JavaScriptSerializer.registerClass('Sys.Serialization.JavaScriptSerializer');
Sys.Serialization.JavaScriptSerializer._charsToEscapeRegExs = [];
Sys.Serialization.JavaScriptSerializer._charsToEscape = [];
Sys.Serialization.JavaScriptSerializer._dateRegEx = new RegExp('(^|[^\\\\])\\"\\\\/Date\\((-?[0-9]+)(?:[a-zA-Z]|(?:\\+|-)[0-9]{4})?\\)\\\\/\\"', 'g');
Sys.Serialization.JavaScriptSerializer._escapeChars = {};
Sys.Serialization.JavaScriptSerializer._escapeRegEx = new RegExp('["\\\\\\x00-\\x1F]', 'i');
Sys.Serialization.JavaScriptSerializer._escapeRegExGlobal = new RegExp('["\\\\\\x00-\\x1F]', 'g');
Sys.Serialization.JavaScriptSerializer._jsonRegEx = new RegExp('[^,:{}\\[\\]0-9.\\-+Eaeflnr-u \\n\\r\\t]', 'g');
Sys.Serialization.JavaScriptSerializer._jsonStringRegEx = new RegExp('"(\\\\.|[^"\\\\])*"', 'g');
Sys.Serialization.JavaScriptSerializer._serverTypeFieldName = '__type';
Sys.Serialization.JavaScriptSerializer._init = function Sys$Serialization$JavaScriptSerializer$_init() {
    var replaceChars = ['\\u0000','\\u0001','\\u0002','\\u0003','\\u0004','\\u0005','\\u0006','\\u0007',
                        '\\b','\\t','\\n','\\u000b','\\f','\\r','\\u000e','\\u000f','\\u0010','\\u0011',
                        '\\u0012','\\u0013','\\u0014','\\u0015','\\u0016','\\u0017','\\u0018','\\u0019',
                        '\\u001a','\\u001b','\\u001c','\\u001d','\\u001e','\\u001f'];
    Sys.Serialization.JavaScriptSerializer._charsToEscape[0] = '\\';
    Sys.Serialization.JavaScriptSerializer._charsToEscapeRegExs['\\'] = new RegExp('\\\\', 'g');
    Sys.Serialization.JavaScriptSerializer._escapeChars['\\'] = '\\\\';
    Sys.Serialization.JavaScriptSerializer._charsToEscape[1] = '"';
    Sys.Serialization.JavaScriptSerializer._charsToEscapeRegExs['"'] = new RegExp('"', 'g');
    Sys.Serialization.JavaScriptSerializer._escapeChars['"'] = '\\"';
    for (var i = 0; i < 32; i++) {
        var c = String.fromCharCode(i);
        Sys.Serialization.JavaScriptSerializer._charsToEscape[i+2] = c;
        Sys.Serialization.JavaScriptSerializer._charsToEscapeRegExs[c] = new RegExp(c, 'g');
        Sys.Serialization.JavaScriptSerializer._escapeChars[c] = replaceChars[i];
    }
}
Sys.Serialization.JavaScriptSerializer._serializeBooleanWithBuilder = function Sys$Serialization$JavaScriptSerializer$_serializeBooleanWithBuilder(object, stringBuilder) {
    stringBuilder.append(object.toString());
}
Sys.Serialization.JavaScriptSerializer._serializeNumberWithBuilder = function Sys$Serialization$JavaScriptSerializer$_serializeNumberWithBuilder(object, stringBuilder) {
    if (isFinite(object)) {
        stringBuilder.append(String(object));
    }
    else {
        throw Error.invalidOperation(Sys.Res.cannotSerializeNonFiniteNumbers);
    }
}
Sys.Serialization.JavaScriptSerializer._serializeStringWithBuilder = function Sys$Serialization$JavaScriptSerializer$_serializeStringWithBuilder(string, stringBuilder) {
    stringBuilder.append('"');
    if (Sys.Serialization.JavaScriptSerializer._escapeRegEx.test(string)) {
        if (Sys.Serialization.JavaScriptSerializer._charsToEscape.length === 0) {
            Sys.Serialization.JavaScriptSerializer._init();
        }
        if (string.length < 128) {
            string = string.replace(Sys.Serialization.JavaScriptSerializer._escapeRegExGlobal,
                function(x) { return Sys.Serialization.JavaScriptSerializer._escapeChars[x]; });
        }
        else {
            for (var i = 0; i < 34; i++) {
                var c = Sys.Serialization.JavaScriptSerializer._charsToEscape[i];
                if (string.indexOf(c) !== -1) {
                    if (Sys.Browser.agent === Sys.Browser.Opera || Sys.Browser.agent === Sys.Browser.FireFox) {
                        string = string.split(c).join(Sys.Serialization.JavaScriptSerializer._escapeChars[c]);
                    }
                    else {
                        string = string.replace(Sys.Serialization.JavaScriptSerializer._charsToEscapeRegExs[c],
                            Sys.Serialization.JavaScriptSerializer._escapeChars[c]);
                    }
                }
            }
       }
    }
    stringBuilder.append(string);
    stringBuilder.append('"');
}
Sys.Serialization.JavaScriptSerializer._serializeWithBuilder = function Sys$Serialization$JavaScriptSerializer$_serializeWithBuilder(object, stringBuilder, sort, prevObjects) {
    var i;
    switch (typeof object) {
    case 'object':
        if (object) {
            if (prevObjects){
                for( var j = 0; j < prevObjects.length; j++) {
                    if (prevObjects[j] === object) {
                        throw Error.invalidOperation(Sys.Res.cannotSerializeObjectWithCycle);
                    }
                }
            }
            else {
                prevObjects = new Array();
            }
            try {
                Array.add(prevObjects, object);
                
                if (Number.isInstanceOfType(object)){
                    Sys.Serialization.JavaScriptSerializer._serializeNumberWithBuilder(object, stringBuilder);
                }
                else if (Boolean.isInstanceOfType(object)){
                    Sys.Serialization.JavaScriptSerializer._serializeBooleanWithBuilder(object, stringBuilder);
                }
                else if (String.isInstanceOfType(object)){
                    Sys.Serialization.JavaScriptSerializer._serializeStringWithBuilder(object, stringBuilder);
                }
            
                else if (Array.isInstanceOfType(object)) {
                    stringBuilder.append('[');
                   
                    for (i = 0; i < object.length; ++i) {
                        if (i > 0) {
                            stringBuilder.append(',');
                        }
                        Sys.Serialization.JavaScriptSerializer._serializeWithBuilder(object[i], stringBuilder,false,prevObjects);
                    }
                    stringBuilder.append(']');
                }
                else {
                    if (Date.isInstanceOfType(object)) {
                        stringBuilder.append('"\\/Date(');
                        stringBuilder.append(object.getTime());
                        stringBuilder.append(')\\/"');
                        break;
                    }
                    var properties = [];
                    var propertyCount = 0;
                    for (var name in object) {
                        if (name.startsWith('$')) {
                            continue;
                        }
                        if (name === Sys.Serialization.JavaScriptSerializer._serverTypeFieldName && propertyCount !== 0){
                            properties[propertyCount++] = properties[0];
                            properties[0] = name;
                        }
                        else{
                            properties[propertyCount++] = name;
                        }
                    }
                    if (sort) properties.sort();
                    stringBuilder.append('{');
                    var needComma = false;
                     
                    for (i=0; i<propertyCount; i++) {
                        var value = object[properties[i]];
                        if (typeof value !== 'undefined' && typeof value !== 'function') {
                            if (needComma) {
                                stringBuilder.append(',');
                            }
                            else {
                                needComma = true;
                            }
                           
                            Sys.Serialization.JavaScriptSerializer._serializeWithBuilder(properties[i], stringBuilder, sort, prevObjects);
                            stringBuilder.append(':');
                            Sys.Serialization.JavaScriptSerializer._serializeWithBuilder(value, stringBuilder, sort, prevObjects);
                          
                        }
                    }
                stringBuilder.append('}');
                }
            }
            finally {
                Array.removeAt(prevObjects, prevObjects.length - 1);
            }
        }
        else {
            stringBuilder.append('null');
        }
        break;
    case 'number':
        Sys.Serialization.JavaScriptSerializer._serializeNumberWithBuilder(object, stringBuilder);
        break;
    case 'string':
        Sys.Serialization.JavaScriptSerializer._serializeStringWithBuilder(object, stringBuilder);
        break;
    case 'boolean':
        Sys.Serialization.JavaScriptSerializer._serializeBooleanWithBuilder(object, stringBuilder);
        break;
    default:
        stringBuilder.append('null');
        break;
    }
}
Sys.Serialization.JavaScriptSerializer.serialize = function Sys$Serialization$JavaScriptSerializer$serialize(object) {
    /// <summary locid="M:J#Sys.Serialization.JavaScriptSerializer.serialize" />
    /// <param name="object" mayBeNull="true"></param>
    /// <returns type="String"></returns>
    var e = Function._validateParams(arguments, [
        {name: "object", mayBeNull: true}
    ]);
    if (e) throw e;
    var stringBuilder = new Sys.StringBuilder();
    Sys.Serialization.JavaScriptSerializer._serializeWithBuilder(object, stringBuilder, false);
    return stringBuilder.toString();
}
Sys.Serialization.JavaScriptSerializer.deserialize = function Sys$Serialization$JavaScriptSerializer$deserialize(data, secure) {
    /// <summary locid="M:J#Sys.Serialization.JavaScriptSerializer.deserialize" />
    /// <param name="data" type="String"></param>
    /// <param name="secure" type="Boolean" optional="true"></param>
    /// <returns></returns>
    var e = Function._validateParams(arguments, [
        {name: "data", type: String},
        {name: "secure", type: Boolean, optional: true}
    ]);
    if (e) throw e;
    
    if (data.length === 0) throw Error.argument('data', Sys.Res.cannotDeserializeEmptyString);
    try {    
        var exp = data.replace(Sys.Serialization.JavaScriptSerializer._dateRegEx, "$1new Date($2)");
        
        if (secure && Sys.Serialization.JavaScriptSerializer._jsonRegEx.test(
             exp.replace(Sys.Serialization.JavaScriptSerializer._jsonStringRegEx, ''))) throw null;
        return eval('(' + exp + ')');
    }
    catch (e) {
         throw Error.argument('data', Sys.Res.cannotDeserializeInvalidJson);
    }
}
