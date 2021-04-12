// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
  function renderDropDown(id, data, isRegion) {
    $("#" + id).kendoDropDownList({
      dataTextField: "name",
      dataValueField: "iso",
      dataSource: data,
      height: 100,
      dataBound: function (e) {
        this.select(0);
        isRegion
          ? $("#regionsReport").trigger("click")
          : $("#provincesReport").trigger("click");
      },
    });
  }

  function getData(url, parameters) {
    return new Promise(function (resolve, reject) {
      var result = { data: null, success: false };
      $.ajax({
        url: url,
        data: parameters,
        type: "POST",
        dataType: "json",
        cache: false,
        success: function (result) {
          console.log(result);
          resolve(result.data);
        },
        error: function () {
          reject();
        },
      });
      return result;
    });
  }

  function renderGrid(id, data, isRegion) {
    $("#" + id).kendoGrid({
      dataSource: {
        data: data,
      },
      columns: [
        {
          field: "location",
          title: isRegion ? "Region" : "Provinces",
        },
        { field: "cases", title: "Cases" },
        { field: "deaths", title: "Deaths" },
      ],
    });
  }

  $("#provincesReport").click(function () {
    var provincesIso = $("#provincesDropDown")
      .data("kendoDropDownList")
      .value();
    getData(provincesUrl, { iso: provincesIso }).then(function (data) {
      renderGrid("provincesGrid", data);
    });
  });

  $("#regionReport").click(function () {
    var regionIso = $("#regionsDropDown").data("kendoDropDownList").value();
    getData(regionsUrl, { iso: regionIso }).then(function (data) {
      renderGrid("provincesGrid", data);
    });
  });

  function loadInformation() {
    var regionsUrl = $("#getRegions").val();

    getData(regionsUrl, {})
      .then(function (data) {
        renderDropDown("regionsDropDown", data);
      })
      .then(function () {
        var firstRegion = regionData[0];
        var provincesUrl = $("#getCountriesDataByRegion").val();
        getData(provincesUrl, { iso: firstRegion.iso }).then(function (data) {
          renderDropDown("provincesDropDown", data);
        });
      });
  }

  loadInformation();
});
