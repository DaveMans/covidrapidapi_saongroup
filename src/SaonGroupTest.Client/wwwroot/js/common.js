function jsonToXml(obj) {
  var xml = "";
  for (var prop in obj) {
    xml += obj[prop] instanceof Array ? "" : "<" + prop + ">";
    if (obj[prop] instanceof Array) {
      for (var array in obj[prop]) {
        xml += "<" + prop + ">";
        xml += jsonToXml(new Object(obj[prop][array]));
        xml += "</" + prop + ">";
      }
    } else if (typeof obj[prop] == "object") {
      xml += jsonToXml(new Object(obj[prop]));
    } else {
      xml += obj[prop];
    }
    xml += obj[prop] instanceof Array ? "" : "</" + prop + ">";
  }
  var xml = xml.replace(/<\/?[0-9]{1,}>/g, "");
  return xml;
}

function jsonToCsv(objArray) {
  const array = typeof objArray != "object" ? JSON.parse(objArray) : objArray;
  let str = "";
  for (let i = 0; i < array.length; i++) {
    let line = "";
    for (let index in array[i]) {
      if (line != "") line += ",";
      line += array[i][index];
    }
    str += line + "\r\n";
  }
  return str;
}

function downloadFile(filename, text) {
  var element = document.createElement("a");
  element.setAttribute(
    "href",
    "data:text/plain;charset=utf-8," + encodeURIComponent(text)
  );
  element.setAttribute("download", filename);
  element.style.display = "none";
  document.body.appendChild(element);
  element.click();
  document.body.removeChild(element);
}

function jsonToXls(jsonData, fileName) {
  var xls = json2xls(jsonData);
  fs.writeFileSync(fileName, xls, "binary", (err) => {
    if (err) {
      console.log("writeFileSync :", err);
    }
    console.log(filename + " file is saved!");
  });
}
