var slider = document.querySelector("#slider");

document.body.addEventListener("mousemove", MouseMove);
slider.addEventListener("mousedown", MouseDown);
document.body.addEventListener("mouseup", MouseUp);

var deltaX = 0;
var currentX = 0;
var prevX = 0;

var mouseReleased = true;
var offsetX = 0;
var newOffset = 0;
var sliderWidth = 0;
var currentSlide = 0;
var timer;
var slideTime = 3000;
var buttons = document.querySelectorAll("#slider_pagination>.button");

window.onload = () => {
	SetTimer(true);
	buttons.forEach(v => {
		v.addEventListener("click", (e) => {
			SetSlide(+v.getAttribute("data-item"));
		});
	});
}
function MouseMove (e) {
	sliderWidth = slider.getBoundingClientRect().width;
	prevX = e.clientX;
	deltaX = prevX - currentX;
	currentX = e.clientX;

	if (!mouseReleased) {
		if (deltaX > 0 && currentSlide == 0) {
			newOffset += deltaX / 4;
		} else
		if (deltaX < 0 && currentSlide == 2) {
			newOffset += deltaX / 4;
		} else {
			newOffset += deltaX;
		}
		var part = Math.floor((Math.abs(offsetX)) / (sliderWidth / 9));
		currentSlide = Math.round(part / 3);
	}
	
}
function MouseDown(e) {
	mouseReleased = false;
	SetTimer(false);
}
function MouseUp (e) {
	SetTimer(true);
	mouseReleased = true;
	SetSlide(currentSlide);
}
function SetSlide(e) {
	newOffset = (sliderWidth / 3 * -e);
	var currentButton = document.querySelectorAll(`#slider_pagination>.button[data-item="${e}"]`);
	buttons.forEach( function(v, i) {
		v.setAttribute('data-active', 
			+v.getAttribute("data-item") == e);
	});
}
function SetTimer(e) {
	if (e) {
		clearInterval(timer);
		timer = setInterval(() => {
			if (currentSlide == 2) {
				currentSlide = 0;
			} else {
				currentSlide += 1;
			}
			SetSlide(currentSlide);
		}, slideTime);
	} else {
		clearInterval(timer);
	}
}
UpdateSlider();
function UpdateSlider() {
	requestAnimationFrame(UpdateSlider);
	offsetX = lerp(offsetX, newOffset, 0.15);
	slider.style.left = offsetX + 'px';
}
function lerp(start, end, t) {
    return start * (1 - t) + end * t;
}