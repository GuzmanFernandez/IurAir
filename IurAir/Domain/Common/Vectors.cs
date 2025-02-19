using CsvHelper;
using CsvHelper.Configuration;
using IurAir.Domain.Air.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Common
{
    public class Vectors
    {
        public string Name { get; set; }
        public string Designator { get; set; }
        public string Code { get; set; }

        public Vectors normalizeCode()
        {
            if (Code.Length < 3)
            {
                Code = $"0{Code}";
            }
            return this;
        }

       
        public static Vectors defaultVector()
        {
            return new Vectors()
            {
                Name = "Default",
                Designator = "Default",
                Code = "Default",
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Vectors vectors &&
                   Name == vectors.Name &&
                   Designator == vectors.Designator &&
                   Code == vectors.Code;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Designator, Code);
        }
    }

    public static class VectorReader
    {
        private static CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            Encoding = Encoding.UTF8,
        };

        public static List<Vectors> GetVectors()
        {
            using (var reader = new StringReader(Properties.Resources.Vettori))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<VectorsClassMap>();
                return csv.GetRecords<Vectors>().ToList();
            }
        }

        public static Vectors GetVectorByCode(string code)
        {
            using (var reader = new StringReader(Properties.Resources.Vettori))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<VectorsClassMap>();
                var vects = csv.GetRecords<Vectors>().Where((v) => (v.Code == code) || (v.normalizeCode().Code == code));
                List<Vectors> vecList = new List<Vectors>();
                var retVect = Vectors.defaultVector();
                foreach (var vect in vects)
                {
                    vecList.Add(vect.normalizeCode());
                }
                if (vecList.Count > 0) { retVect = vecList[0]; }
                return retVect;
            }
        }

        public static Vectors GetVectorByIATA(string iata)
        {
            using (var reader = new StringReader(Properties.Resources.Vettori))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<VectorsClassMap>();
                var vects = csv.GetRecords<Vectors>().Where((v) => v.Designator == iata);
                List<Vectors> vecList = new List<Vectors>();
                var retVect = Vectors.defaultVector();
                foreach (var vect in vects)
                {
                    vecList.Add(vect.normalizeCode());
                }
                if (vecList.Count > 0) { retVect = vecList[0]; }
                return retVect;
            }
        }
    }

    public class VectorsClassMap : ClassMap<Vectors>
    {
        public VectorsClassMap()
        {
            Map(m => m.Name).Name("AIRLINE_NAME");
            Map(m => m.Designator).Name("IATA_DESIGNATOR");
            Map(m => m.Code).Name("DIGIT_CODE");
        }
    }

}
