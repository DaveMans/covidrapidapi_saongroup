using System;
using System.Collections.Generic;

namespace SaonGroupTest.Client.Models
{
    public class ReportDtoModel
    {
        public long confirmed { get; set; }
        public long deaths { get; set; }
        public CountryDtoModel region { get; set; }
    }
}
