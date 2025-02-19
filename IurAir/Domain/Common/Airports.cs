using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Common
{
    public class Airports
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }

		public static Airports getDefault()
		{
			return new Airports()
			{
				Id = "NA",
				Country = "NA",
				Code = "NA"
			};
		}
    }

    public static class AirportReader
    {
		private static CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			Delimiter = ";",
			Encoding = Encoding.UTF8,
		};

        public static List<Airports> ReadAirports()
        {
			using (var reader = new StringReader(Properties.Resources.Airports))
			using (var csv = new CsvReader(reader, config))
			{
				csv.Context.RegisterClassMap<AirportsClassMap>();
				return csv.GetRecords<Airports>().ToList();
			}
		}

		public static Airports GetAirportByCode(string code)
		{
			using (var reader = new StringReader(Properties.Resources.Airports))
			using (var csv = new CsvReader(reader, config))
			{
				csv.Context.RegisterClassMap<AirportsClassMap>();
				var airs = csv.GetRecords<Airports>().Where((v) => v.Code == code);
				List<Airports> airList = new List<Airports>();
				var retAirport = Airports.getDefault();
				foreach (var air in airs)
				{
					airList.Add(air);
				}
				if (airList.Count > 0) { retAirport = airList[0]; }
				return retAirport;
			}
		}
	}

    public class AirportsClassMap : ClassMap<Airports>
    {
        public AirportsClassMap()
        {
			//ident;iso_country;iata_code
			Map(m => m.Id).Name("ident");
			Map(m => m.Country).Name("iso_country");
			Map(m => m.Code).Name("iata_code");

		}
    }
}
