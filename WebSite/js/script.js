var d = new Date();
d.getHours();

var hours = new Date().getHours(); 
if (hours >= 5 && hours < 11) {
	console.log("morning");
	changeBackground("#2d1a73");
}
else if (hours >= 11 && hours < 19){
	console.log("day");
	changeBackground("#2db9ca");
} 
else if (hours >= 19 && hours < 23) {
	console.log("evening");
	changeBackground("#2d1a73");
}
else {
	console.log("night");
	changeBackground("#531a75");
}

