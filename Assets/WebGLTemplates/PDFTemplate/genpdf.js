
var nom = "Résultats";
function namePDF(arg)
{
	nom = arg;
}

function PDF(arg) {
	var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
	if (isSafari)
	{
		var button = document.createElement('button');
		button.innerHTML = 'Résultats';
		button.setAttribute("class", "btn btn-sample btn-sm pos-button");
		button.onclick = function(){
			window.open("data:application/pdf," + escape(arg));
			return false;
		};
		document.getElementById('PDFButton').appendChild(button);
	} else 
	{
		var href = document.createElement('a');
		href.innerHTML = "Résultats";
		href.setAttribute("href", "data:application/pdf," + escape(arg));
		href.setAttribute("download", "Resultats - " + nom + ".pdf");
		href.setAttribute("class", "btn btn-sample btn-sm pos-button");
		href.setAttribute("role", "button");
		document.getElementById('PDFButton').appendChild(href);
	}
}

function PDFFinal(arg) {
	var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
	if (isSafari)
	{
		var button = document.createElement('button');
		button.innerHTML = 'Résultats complet';
		button.setAttribute("class", "btn btn-sample btn-sm pos-button");
		button.onclick = function(){
			window.open("data:application/pdf," + escape(arg));
			return false;
		};
		document.getElementById('PDFButton').appendChild(button);
	} else 
	{
		var href = document.createElement('a');
		href.innerHTML = "Résultats";
		href.setAttribute("href", "data:application/pdf," + escape(arg));
		href.setAttribute("download", "Résultats.pdf");
		href.setAttribute("class", "btn btn-sample btn-sm pos-button");
		href.setAttribute("role", "button");
		document.getElementById('PDFButton').appendChild(href);
	}
}

function Log(str) {
	console.log(str.toString());
}
