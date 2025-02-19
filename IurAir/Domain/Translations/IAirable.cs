using IurAir.Domain.Air;
using IurAir.Domain.Air.Lines;
using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Translations
{
    public interface IAirable
    {
        string getCommonParts();
        Dictionary<string, string> getPerPaxParts(bool paxSplit = false);
        List<AirRender> getRenders(bool paxSplit = false);
        int getPassengerTypeCount();
        DocumentParse getOriginal();
    }
}
