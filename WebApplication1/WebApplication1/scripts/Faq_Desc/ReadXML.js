
function loadDoc(ID_DEF,AF,UF) {
   
    if (ID_DEF) {

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                myFunction(this);
            }
        };
        xhttp.open("GET", AF, true);
        xhttp.send();
    }
    else {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                myFunction(this);
            }
        };
        xhttp.open("GET", UF, true);
        xhttp.send();
    }
}
function myFunction(xml) {
    var i;
    var xmlDoc = xml.responseXML;
    var mpanel = '';
    var x = xmlDoc.getElementsByTagName("P");
    for (i = 0; i < x.length; i++) {
        mpanel+= '<div class="panel panel-default "><div class="panel-heading accordion-toggle question-toggle collapsed" data-toggle="collapse" data-parent="#faqAccordion" data-target="#question'+ i+'"><h4 class="panel-title"><a href="#" class="ing">';
        mpanel += x[i].getElementsByTagName("Q")[0].childNodes[0].nodeValue;
        mpanel += '</a></h4></div><div id="question'+i+'" class="panel-collapse collapse" style="height: 0px;"><div class="panel-body"><h5><span class="label label-primary">תשובה</span></h5><p>';
        mpanel += x[i].getElementsByTagName("A")[0].childNodes[0].nodeValue;
        mpanel += '</p></div></div></div>';
    }
    document.getElementById("faqAccordion").innerHTML = mpanel;
}

