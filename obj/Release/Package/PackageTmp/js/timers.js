
function dateFormat() {
    var now = new Date();
    var month = new Array(12);
    month[0] = "January";
    month[1] = "February";
    month[2] = "March";
    month[3] = "April";
    month[4] = "May";
    month[5] = "June";
    month[6] = "July";
    month[7] = "August";
    month[8] = "September";
    month[9] = "October";
    month[10] = "November";
    month[11] = "December";
    //November 1, 2013 1:1:1
    return month[now.getMonth()] + " " + now.getDay() + ", " + now.getFullYear() + " " + now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds();
   // return now.format("mmmm dS, yyyy, h:MM:ss");
}


/**********************************************************************************************
* CountUnits script by Praveen Lobo (http://PraveenLobo.com/techblog/javascript-countup-timer/)
* This notice MUST stay intact(in both JS file and SCRIPT tag) for legal use.
* http://praveenlobo.com/blog/disclaimer/
**********************************************************************************************/
function CountUnits(initDate, id) {
    this.counterDate = new Date(initDate);
    this.secDiff = 0, this.seconds = 0, this.minutes = 0, this.hours = 0;
    this.days = 0, this.weeks = 0, this.months = 0, this.years = 0;
    this.countainer = document.getElementById(id);
    this.countainer.title = initDate;
    this.countainer.onclick = function () { window.location = "http://praveenlobo.com/techblog/javascript-countup-timer/"; };
    this.updateCounter();
}

CountUnits.prototype.calculateUnits = function (valuePerUnit) {
    var tmp = this.secDiff / valuePerUnit;
    tmp = Math.abs(tmp) < 1 ? 0 : tmp;
    return (tmp < 0 ? Math.ceil(tmp) : Math.floor(tmp));
}

CountUnits.prototype.calculate = function () {
    this.secDiff = Math.round(((new Date()) - this.counterDate) / 1000);
    this.seconds = Math.round(this.secDiff);
    this.minutes = this.calculateUnits(60);
    this.hours = this.calculateUnits(3600);
    this.days = this.calculateUnits(86400);
    this.weeks = this.calculateUnits(604800);
    this.months = this.calculateUnits(2629744);
    this.years = this.calculateUnits(31556928);
}

CountUnits.prototype.updateCounter = function () {
    this.calculate();
    this.countainer.innerHTML = "<strong>" + this.years + "</strong> <small>" + (this.years == 1 ? "year" : "years") + "</small><br/>" +
			"<strong>" + this.months + "</strong> <small>" + (this.months == 1 ? "month" : "months") + "</small><br/>" +
			"<strong>" + this.days + "</strong> <small>" + (this.days == 1 ? "day" : "days") + "</small><br/>" +
			"<strong>" + this.hours + "</strong> <small>" + (this.hours == 1 ? "hour" : "hours") + "</small><br/>" +
			"<strong>" + this.minutes + "</strong> <small>" + (this.minutes == 1 ? "minute" : "minutes") + "</small><br/>" +
			"<strong>" + this.seconds + "</strong> <small>" + (this.seconds == 1 ? "second" : "seconds") + "</small><br/>";
    var self = this;
    setTimeout(function () { self.updateCounter(); }, 1000);
}


/**********************************************************************************************
* Day Counter script by Praveen Lobo (http://PraveenLobo.com/techblog/javascript-counter-count-days/)
* This notice MUST stay intact(in both JS file and SCRIPT tag) for legal use.
* http://praveenlobo.com/blog/disclaimer/
**********************************************************************************************/
function DayCounter(initDate, id) {
    this.counterDate = new Date(initDate);
    this.container = document.getElementById(id);
    this.calculate();
    this.container.title = initDate;
    this.container.onclick = function () { window.location = "http://praveenlobo.com/techblog/javascript-counter-count-days/"; };
}

DayCounter.prototype.calculate = function () {
    var secDiff = Math.round(((new Date()) - this.counterDate) / 1000);
    var nextUpdate = (nextUpdate = (secDiff % 86400)) < 0 ? (nextUpdate * -1) : (86400 - nextUpdate);
    var tmp = Math.abs((tmp = secDiff / 86400)) < 1 ? 0 : tmp;
    var days = (tmp < 0 ? Math.ceil(tmp) : Math.floor(tmp));
    this.container.innerHTML =
        "<strong>" + days + "</strong> " + (Math.abs(days) == 1 ? "day" : "days");
    var self = this;
    setTimeout(function () { self.calculate(); }, (++nextUpdate * 1000));
}

/**********************************************************************************************
* Day Counter script by Praveen Lobo (http://PraveenLobo.com/techblog/javascript-counter-count-days/)
* This notice MUST stay intact(in both JS file and SCRIPT tag) for legal use.
* http://praveenlobo.com/blog/disclaimer/
**********************************************************************************************/
function DayCounterFraction(initDate, id) {
    this.counterDate = new Date(initDate);
    this.container = document.getElementById(id);
    this.calculate();
    this.container.title = initDate;
    this.container.onclick = function () { window.location = "http://praveenlobo.com/techblog/javascript-counter-count-days/"; };
}

DayCounterFraction.prototype.calculate = function () {
    var secDiff = ((new Date()) - this.counterDate) / 1000;
    var days = (secDiff / 86400).toFixed(2)
    this.container.innerHTML =
        " <strong>" + days + "</strong> " + (Math.abs(days) == 1 ? "day" : "days");
    var self = this;
    var nextUpdate = (nextUpdate = (secDiff % 864).toFixed(0)) < 0 ? (nextUpdate * -1) : (864 - nextUpdate);
    setTimeout(function () { self.calculate(); }, (++nextUpdate * 1000));
}

/**********************************************************************************************
* Days-Hours-Minutes-Seconds Counter script by Praveen Lobo 
* (http://PraveenLobo.com/techblog/javascript-counter-count-days-hours-minutes-seconds/)
* This notice MUST stay intact(in both JS file and SCRIPT tag) for legal use.
* http://praveenlobo.com/blog/disclaimer/
**********************************************************************************************/
function DaysHMSCounter(initDate, id) {
    this.counterDate = new Date(initDate);
    this.container = document.getElementById(id);
    this.update();
    this.container.title = initDate;
    this.container.onclick = function () { window.location = "http://praveenlobo.com/techblog/javascript-counter-count-days-hours-minutes-seconds/"; };
}

DaysHMSCounter.prototype.calculateUnit = function (secDiff, unitSeconds) {
    var tmp = Math.abs((tmp = secDiff / unitSeconds)) < 1 ? 0 : tmp;
    return Math.abs(tmp < 0 ? Math.ceil(tmp) : Math.floor(tmp));
}

DaysHMSCounter.prototype.calculate = function () {
    var secDiff = Math.abs(Math.round(((new Date()) - this.counterDate) / 1000));
    this.days = this.calculateUnit(secDiff, 86400);
    this.hours = this.calculateUnit((secDiff - (this.days * 86400)), 3600);
    this.mins = this.calculateUnit((secDiff - (this.days * 86400) - (this.hours * 3600)), 60);
    this.secs = this.calculateUnit((secDiff - (this.days * 86400) - (this.hours * 3600) - (this.mins * 60)), 1);
}

DaysHMSCounter.prototype.update = function () {
    this.calculate();
    this.container.innerHTML =
        " <strong>" + this.days + "</strong> " + (this.days == 1 ? "day" : "days") +
        " <strong>" + this.hours + "</strong> " + (this.hours == 1 ? "hour" : "hours") +
        " <strong>" + this.mins + "</strong> " + (this.mins == 1 ? "min" : "mins") +
        " <strong>" + this.secs + "</strong> " + (this.secs == 1 ? "sec" : "secs");
    var self = this;
    setTimeout(function () { self.update(); }, (1000));
}

/**********************************************************************************************
* CountUp script by Praveen Lobo (http://PraveenLobo.com/techblog/javascript-countup-timer/)
* This notice MUST stay intact(in both JS file and SCRIPT tag) for legal use.
* http://praveenlobo.com/blog/disclaimer/
**********************************************************************************************/
function CountUp(initDate, id, msg) {
    this.beginDate = new Date(initDate);
    this.msg = msg;
    this.countainer = document.getElementById(id);
    this.numOfDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    this.borrowed = 0, this.years = 0, this.months = 0, this.days = 0;
    this.hours = 0, this.minutes = 0, this.seconds = 0;
    this.updateNumOfDays();
    this.updateCounter();
    this.countainer.onclick = function () { window.location = "http://praveenlobo.com/techblog/javascript-countup-timer/"; };
}

CountUp.prototype.updateNumOfDays = function () {
    var dateNow = new Date();
    var currYear = dateNow.getFullYear();
    if ((currYear % 4 == 0 && currYear % 100 != 0) || currYear % 400 == 0) {
        this.numOfDays[1] = 29;
    }
    var self = this;
    setTimeout(function () { self.updateNumOfDays(); }, (new Date((currYear + 1), 1, 2) - dateNow));
}

CountUp.prototype.datePartDiff = function (then, now, MAX) {
    var diff = now - then - this.borrowed;
    this.borrowed = 0;
    if (diff > -1) return diff;
    this.borrowed = 1;
    return (MAX + diff);
}

CountUp.prototype.calculate = function () {
    var currDate = new Date();
    var prevDate = this.beginDate;
    this.seconds = this.datePartDiff(prevDate.getSeconds(), currDate.getSeconds(), 60);
    this.minutes = this.datePartDiff(prevDate.getMinutes(), currDate.getMinutes(), 60);
    this.hours = this.datePartDiff(prevDate.getHours(), currDate.getHours(), 24);
    this.days = this.datePartDiff(prevDate.getDate(), currDate.getDate(), this.numOfDays[currDate.getMonth()]);
    this.months = this.datePartDiff(prevDate.getMonth(), currDate.getMonth(), 12);
    this.years = this.datePartDiff(prevDate.getFullYear(), currDate.getFullYear(), 0);
}

CountUp.prototype.addLeadingZero = function (value) {
    return value < 10 ? ("0" + value) : value;
}

CountUp.prototype.formatTime = function () {
    this.seconds = this.addLeadingZero(this.seconds);
    this.minutes = this.addLeadingZero(this.minutes);
    this.hours = this.addLeadingZero(this.hours);
}

CountUp.prototype.updateCounter = function () {
    this.calculate();
    this.formatTime();
    //this.countainer.innerHTML = "<strong>" + this.years + "</strong> <small>" + (this.years == 1 ? "year" : "years") + "</small>" +
//    " <strong>" + this.months + "</strong> <small>" + (this.months == 1 ? "month" : "months") + "</small>" +
//    " <strong>" + this.days + "</strong> <small>" + (this.days == 1 ? "day" : "days") + "</small>" +
//    " <strong>" + this.hours + "</strong> <small>" + (this.hours == 1 ? "hour" : "hours") + "</small>" +
//    " <strong>" + this.minutes + "</strong> <small>" + (this.minutes == 1 ? "minute" : "minutes") + "</small>" +
//    " <strong>" + this.seconds + "</strong> <small>" + (this.seconds == 1 ? "second" : "seconds") + "</small>" +
//    " <strong> " + this.msg + " </strong>";

    this.countainer.innerHTML = " <strong>" + this.hours + "</strong> <small>" + (this.hours == 1 ? "hour" : "hours") + "</small>" + 
        " <strong>" + this.minutes + "</strong> <small>" + (this.minutes == 1 ? "minute" : "minutes") + "</small>" +
        " <strong>" + this.seconds + "</strong> <small>" + (this.seconds == 1 ? "second" : "seconds") + "</small>" +
        " <strong> " + this.msg + " </strong>";
    var self = this;
    setTimeout(function () { self.updateCounter(); }, 1000);
}

/**********************************************************************************************
* CountDown Timer script by Praveen Lobo (http://PraveenLobo.com/techblog/javascript-CountDown-timer/)
* This notice MUST stay intact(in both JS file and SCRIPT tag) for legal use.
* http://praveenlobo.com/blog/disclaimer/
**********************************************************************************************/
function CountDown(initDate, id, msg) {
    this.endDate = new Date(initDate);
    this.msg = msg;
    this.countainer = document.getElementById(id);
    this.numOfDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    this.borrowed = 0, this.years = 0, this.months = 0, this.days = 0;
    this.hours = 0, this.minutes = 0, this.seconds = 0;
    this.updateNumOfDays();
    this.updateCounter();
    this.countainer.onclick = function () { window.location = "http://praveenlobo.com/techblog/javascript-countdown-timer/"; };
}

CountDown.prototype.updateNumOfDays = function () {
    var dateNow = new Date();
    var currYear = dateNow.getFullYear();
    if ((currYear % 4 == 0 && currYear % 100 != 0) || currYear % 400 == 0) {
        this.numOfDays[1] = 29;
    }
    var self = this;
    setTimeout(function () { self.updateNumOfDays(); }, (new Date((currYear + 1), 1, 2) - dateNow));
}

CountDown.prototype.datePartDiff = function (then, now, MAX) {
    var diff = now - then - this.borrowed;
    this.borrowed = 0;
    if (diff > -1) return diff;
    this.borrowed = 1;
    return (MAX + diff);
}

CountDown.prototype.calculate = function () {
    var futureDate = this.endDate;
    var currDate = new Date();
    this.seconds = this.datePartDiff(currDate.getSeconds(), futureDate.getSeconds(), 60);
    this.minutes = this.datePartDiff(currDate.getMinutes(), futureDate.getMinutes(), 60);
    this.hours = this.datePartDiff(currDate.getHours(), futureDate.getHours(), 24);
    this.days = this.datePartDiff(currDate.getDate(), futureDate.getDate(), this.numOfDays[futureDate.getMonth()]);
    this.months = this.datePartDiff(currDate.getMonth(), futureDate.getMonth(), 12);
    this.years = this.datePartDiff(currDate.getFullYear(), futureDate.getFullYear(), 0);
}

CountDown.prototype.addLeadingZero = function (value) {
    return value < 10 ? ("0" + value) : value;
}

CountDown.prototype.formatTime = function () {
    this.seconds = this.addLeadingZero(this.seconds);
    this.minutes = this.addLeadingZero(this.minutes);
    this.hours = this.addLeadingZero(this.hours);
}

CountDown.prototype.updateCounter = function () {
    this.calculate();
    this.formatTime();
    this.countainer.innerHTML = "<strong>" + this.years + "</strong> <small>" + (this.years == 1 ? "year" : "years") + "</small>" +
       " <strong>" + this.months + "</strong> <small>" + (this.months == 1 ? "month" : "months") + "</small>" +
       " <strong>" + this.days + "</strong> <small>" + (this.days == 1 ? "day" : "days") + "</small>" +
       " <strong>" + this.hours + "</strong> <small>" + (this.hours == 1 ? "hour" : "hours") + "</small>" +
       " <strong>" + this.minutes + "</strong> <small>" + (this.minutes == 1 ? "minute" : "minutes") + "</small>" +
       " <strong>" + this.seconds + "</strong> <small>" + (this.seconds == 1 ? "second" : "seconds") + "</small>" +
       " <strong> " + this.msg + " </strong>";
    if (this.endDate > (new Date())) {
        var self = this;
        setTimeout(function () { self.updateCounter(); }, 1000);
    }
}

/**********************************************************************************************
* CountUp/CountDown Timer script by Praveen Lobo 
* (http://PraveenLobo.com/techblog/javascript-countup-countdown-timer/)
* This notice MUST stay intact(in both JS file and SCRIPT tag) for legal use.
* http://praveenlobo.com/blog/disclaimer/
**********************************************************************************************/
function Counter(initDate, id) {
    this.counterDate = new Date(initDate);
    this.countainer = document.getElementById(id);
    this.numOfDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    this.borrowed = 0, this.years = 0, this.months = 0, this.days = 0;
    this.hours = 0, this.minutes = 0, this.seconds = 0;
    this.updateNumOfDays();
    this.updateCounter();

    // following lines are added just for display on the blog
    this.countainer.title = new Date(initDate);
    this.countainer.onclick = function () { window.location = "http://praveenlobo.com/techblog/javascript-countup-countdown-timer"; };
}

Counter.prototype.updateNumOfDays = function () {
    var dateNow = new Date();
    var currYear = dateNow.getFullYear();
    if ((currYear % 4 == 0 && currYear % 100 != 0) || currYear % 400 == 0) {
        this.numOfDays[1] = 29;
    }
    var self = this;
    setTimeout(function () { self.updateNumOfDays(); }, (new Date((currYear + 1), 1, 2) - dateNow));
}

Counter.prototype.datePartDiff = function (then, now, MAX) {
    var diff = now - then - this.borrowed;
    this.borrowed = 0;
    if (diff > -1) return diff;
    this.borrowed = 1;
    return (MAX + diff);
}

Counter.prototype.calculate = function () {
    var futureDate = this.counterDate > new Date() ? this.counterDate : new Date();
    var pastDate = this.counterDate == futureDate ? new Date() : this.counterDate;
    this.seconds = this.datePartDiff(pastDate.getSeconds(), futureDate.getSeconds(), 60);
    this.minutes = this.datePartDiff(pastDate.getMinutes(), futureDate.getMinutes(), 60);
    this.hours = this.datePartDiff(pastDate.getHours(), futureDate.getHours(), 24);
    this.days = this.datePartDiff(pastDate.getDate(), futureDate.getDate(), this.numOfDays[futureDate.getMonth()]);
    this.months = this.datePartDiff(pastDate.getMonth(), futureDate.getMonth(), 12);
    this.years = this.datePartDiff(pastDate.getFullYear(), futureDate.getFullYear(), 0);
}

Counter.prototype.addLeadingZero = function (value) {
    return value < 10 ? ("0" + value) : value;
}

Counter.prototype.formatTime = function () {
    this.seconds = this.addLeadingZero(this.seconds);
    this.minutes = this.addLeadingZero(this.minutes);
    this.hours = this.addLeadingZero(this.hours);
}

Counter.prototype.updateCounter = function () {
    this.calculate();
    this.formatTime();
    this.countainer.innerHTML = "<strong>" + this.years + "</strong> " + (this.years == 1 ? "year" : "years") +
        " <strong>" + this.months + "</strong> " + (this.months == 1 ? "month" : "months") +
        " <strong>" + this.days + "</strong> " + (this.days == 1 ? "day" : "days") +
        " <strong>" + this.hours + "</strong> " + (this.hours == 1 ? "hour" : "hours") +
        " <strong>" + this.minutes + "</strong> " + (this.minutes == 1 ? "minute" : "minutes") +
        " <strong>" + this.seconds + "</strong> " + (this.seconds == 1 ? "second" : "seconds");
    var self = this;
    setTimeout(function () { self.updateCounter(); }, 1000);
}