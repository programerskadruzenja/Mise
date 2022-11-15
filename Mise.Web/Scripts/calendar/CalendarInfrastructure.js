
var CalendarAddresses = function (createUrl, updateUrl, getCalendarEventsUrl) {
    var self = this;
    self.GetCalendarEventsUrl = getCalendarEventsUrl;
    self.UpdateUrl = updateUrl;
    self.CreateUrl = createUrl;
}

var CalendarEventVM = function (id, title, start, end, backgroundColor) {
    var self = this;
    self.id = id;
    self.title = title == "" ? " " : title;
    self.start = start;
    self.end = end;
    self.backgroundColor = backgroundColor;
    self.textColor = null;
    self.deleteColor = null;
    self.zaPokojne = null;

    self.SetForegroundColor = function (color) {
        self.textColor = color;
        self.deleteColor = color;
    }

    self.SetBorderColor = function (color) {
        self.borderColor = color;
    }

    self.OsobaPunoIme = null;
    self.OsobaID = null;
    self.MjestoNaziv = null;
    self.MjestoID = null;
    
    self.GetLongTitle = function () {
        var title = "";

        if (self.zaPokojne)
            title = "+ ";

        if (self.MjestoNaziv != undefined && self.MjestoNaziv != "" && self.MjestoNaziv != " ") {
            title += "<span style='margin-left:60px;'><span class='glyphicon glyphicon-asterisk'></span>&nbsp;" + self.MjestoNaziv + "</span>";
        }

        if (self.OsobaPunoIme != undefined && self.OsobaPunoIme != "" && self.OsobaPunoIme != " ") {
            title += "<span style='margin-left:40px;'>&nbsp;" + self.OsobaPunoIme + "</span>";
        }



        if (self.title != undefined && self.title != "" && self.title != " ") {
            title += "<span style='margin-left:40px; font-style:italic'>&nbsp;" + self.title + "</span>";
        }
        return title;
    }

    self.GetShortTitle = function () {
        var title = "";

        //if (self.zaPokojne)
        //    title = "+ ";

        
        
        if (self.OsobaPunoIme != undefined && self.OsobaPunoIme != "" && self.OsobaPunoIme != " ") {
            title += "<span style='margin-left:0px;'>" + self.OsobaPunoIme + "</span>";
        }

        else {
            title += "<span style='margin-left:0px;'>" + self.title + "</span>";
        }

    if (self.MjestoNaziv != undefined && self.MjestoNaziv != "" && self.MjestoNaziv != " ") {
            title += "<span style='margin-left:20x;'>&nbsp;</br>" + self.MjestoNaziv + "</span>";
        }

        return title;
    }
}