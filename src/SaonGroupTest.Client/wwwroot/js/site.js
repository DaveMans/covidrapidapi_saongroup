$(document).ready(function () {
  $("#regionsReport").kendoButton();
  $("#regionsXml").kendoButton();
  $("#regionsJson").kendoButton();
  $("#regionsCsv").kendoButton();
  var gridGlobalData = null;

  function renderComboBox(id, data) {
    $("#" + id).kendoComboBox({
      dataTextField: "name",
      dataValueField: "iso",
      dataSource: data,
      height: 300,
      filter: "contains",
      suggest: true,
    });
  }

  function loadRegions() {
    var url = $("#getRegions").val();
    getData(url, {}).then(function (data) {
      renderComboBox("regionsComboBox", data);
    });
  }

  function getData(url, parameters) {
    $("#overlay").attr("style", "display:flex");
    return new Promise(function (resolve, reject) {
      var result = { data: null, success: false };
      $.ajax({
        url: url,
        data: parameters,
        type: "POST",
        dataType: "json",
        cache: false,
        success: function (result) {
          gridGlobalData = result;
          resolve(result);
          $("#overlay").attr("style", "display:none");
        },
        error: function (xhr, textStatus, error) {
          console.log(xhr.statusText);
          console.log(textStatus);
          console.log(error);
          swal("Error", "There was an error, please try again!", "error");
          reject();
          $("#overlay").attr("style", "display:none");
        },
      });
      return result;
    });
  }

  function renderGrid(id, data, areAllRegions) {
    $("#" + id)
      .empty()
      .kendoGrid({
        dataSource: {
          data: data,
        },
        sortable: true,
        filterable: true,
        columns: [
          {
            field: "location",
            title: areAllRegions ? "Region" : "Provinces",
            headerAttributes: {
              class: "color-red",
              style: "text-align: center",
            },
          },
          {
            field: "confirmed",
            title: "Cases",
            format: "{0:n0}",
            headerAttributes: {
              class: "color-red",
              style: "text-align: center",
            },
          },
          {
            field: "deaths",
            title: "Deaths",
            format: "{0:n0}",
            headerAttributes: {
              class: "color-red",
              style: "text-align: center",
            },
          },
        ],
      });
  }

  $("#regionsReport").click(function () {
    var regionIso = $("#regionsComboBox").data("kendoComboBox").value();

    if (regionIso === "") {
      swal("Error", "Please select a region first!", "error");
      return;
    }

    var reportUrl = $("#getReportByRegion").val();
    var areAllRegions = regionIso === "ALL";
    getData(reportUrl, { iso: regionIso }).then(function (data) {
      renderGrid("regionsGrid", data, areAllRegions);
    });
  });

  loadRegions();

  $("#regionsXml").click(function () {
    if (gridGlobalData === null) {
      swal(
        "Error",
        "Please select a region data first to be exported!",
        "error"
      );
      return;
    }

    var xmlData = jsonToXml(gridGlobalData);
    downloadFile("regions_top10_covid_cases.xml", xmlData);
  });
  $("#regionsJson").click(function () {
    if (gridGlobalData === null) {
      swal(
        "Error",
        "Please select a region data first to be exported!",
        "error"
      );
      return;
    }
    var data = JSON.stringify(gridGlobalData);
    downloadFile("regions_top10_covid_cases.json", data);
  });
  $("#regionsCsv").click(function () {
    if (gridGlobalData === null) {
      swal(
        "Error",
        "Please select a region data first to be exported!",
        "error"
      );
      return;
    }
    var data = JSON.stringify(gridGlobalData);
    var csvData = jsonToCsv(data);
    downloadFile("regions_top10_covid_cases.csv", csvData);
  });
});
