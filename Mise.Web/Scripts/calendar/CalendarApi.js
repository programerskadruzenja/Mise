var CalendarApi = function (addresses, mjestoID) {
    var self = this;
    self.Addresses = addresses;
    self.MjestoID = mjestoID;

    function getVM(item) {
        var vm = new CalendarEventVM(item.EventID, item.EventTitle, item.EventStartTimeStamp, item.EventEndTimeStamp);

        vm.OsobaPunoIme = item.OsobaPunoIme;
        vm.OsobaID = item.OsobaID;
        vm.MjestoNaziv = item.MjestoNaziv;
        vm.MjestoID = item.MjestoID;
        vm.backgroundColor = item.EventColorValue;
        vm.zaPokojne = item.ZaPokojne;

        return vm;
    }

    self.Load = function (start, end, timezone, callback) {
        //var result = [];

        //result.push(getVM({
        //    EventID: 1, EventTitle: 'Misa za Anu',
        //    EventStartTimeStamp: '2017-09-08T12:00:00', EventEndTimeStamp: '2017-09-08T12:30:00',
        //    OsobaPuniNaziv : 'Ivan Horvat', MjestoNaziv: 'Tisno'
        //}));
        //result.push(getVM({
        //    EventID: 2, EventTitle: 'Misa za Ivu',
        //    EventStartTimeStamp: '2017-09-08T13:00:00', EventEndTimeStamp: '2017-09-08T14:30:00',
        //    OsobaPuniNaziv: 'Marko Marković', MjestoNaziv: 'Tisno'
        //}));
        //var vm = getVM({
        //    EventID: 3, EventTitle: 'Dan državnosti',
        //    EventStartTimeStamp: '2017-09-08T11:00:00', EventEndTimeStamp: '2017-09-08T12:00:00',
        //    OsobaPuniNaziv: '', MjestoNaziv: 'Tisno'
        //});
        //vm.backgroundColor = "#105D2A";
        //result.push(vm);

        //vm = getVM({
        //    EventID: 3,
        //    EventTitle: 'Misa za Petra',
        //    EventStartTimeStamp: '2017-09-08T13:00:00',
        //    EventEndTimeStamp: '2017-09-08T13:30:00',
        //    OsobaPuniNaziv: 'Ana Anić',
        //    MjestoNaziv: 'Dubrava'
        //});
        //vm.backgroundColor = "#403075";
        //result.push(vm);
        //callback(result);



        return $.ajax({
                url: self.Addresses.GetCalendarEventsUrl,
                method: "POST",
                data: { odDatuma: start.format(), doDatuma: end.format(), mjestoID: self.MjestoID }
            })
            .done(function (data) {
                var result = [];
                $(data).each(function (index, item) {
                    result.push(getVM(item));
                });

                callback(result);

            })
            .fail(function (jqXHR, textProcedure, errorThrown) {
                alert('Pogreška kod dohvaćanja podataka')
                    //self.Notification.showError("Pogreška kod dohvaćanja podataka.", "Pogreška");
                }
            );
    }

    self.Create = function (start, end) {
        var url = self.Addresses.CreateUrl;
        url += "?odDatumaTekst=" + moment(start).format('YYYY-MM-DD HH:mm:ss') + "&doDatumaTekst=" + moment(end).format('YYYY-MM-DD HH:mm:ss');
        document.location = url;
    }


    self.Update = function (event) {
        var url = self.Addresses.UpdateUrl;
        url += "?id=" + event.id;
        document.location = url;
    }
}