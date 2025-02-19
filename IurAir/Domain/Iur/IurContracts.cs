using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public abstract class IurLine : IReificable
    {
        public string rawLine { get; }
        public string lineIdentifier { get; }
        public int totLineLength { get; }
        public List<LineSection> sections { get; }

        public IurLine(string rawLine, string lineIdentifier, int totLineLength, List<LineSection> sections)
        {
            this.rawLine = rawLine;
            this.lineIdentifier = lineIdentifier;
            this.totLineLength = totLineLength;
            this.sections = sections;
        }

        public virtual Dictionary<string, string> parseLine()
        {
            Dictionary<string, string> resDictionary = new Dictionary<string, string>();
            foreach (var section in sections)
            {
                string label = section.iurLabel;
                int ssStart = section.start - 1;
                int ssEnd = section.length;
                string value = rawLine.Substring(ssStart, ssEnd);
                resDictionary.Add(label, value);
            }
            return resDictionary;
        }

        public string toJson()
        {
            return JsonSerializer.Serialize(this.getObject());
        }
        public abstract IurObject getObject();
    }

    public interface IReificable
    {
        string toJson();
        IurObject getObject();
    }

    public interface IurObject
    {

    }
    public class LineSection
    {

        public int start { get; set; }
        public int length { get; set; }
        public string iurLabel { get; set; }
        public string hrLabel { get; set; }
    }
}
