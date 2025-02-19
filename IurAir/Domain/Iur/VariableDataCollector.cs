using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class VariableDataCollector : IurLine
    {
        public int LinesNumber { get; set; }
        public bool IsVoid { get; set; }
        public bool PhoneIndicator { get; set; }
        public int LastIndex = 0;
        public string LastPax = "";
        public Dictionary<string, string> M0VarData = new Dictionary<string, string>();
        public Dictionary<string, string> M1VarData = new Dictionary<string, string>();
        public VariableDataCollector(string[] lines) : base("", "", 0, new List<LineSection>())
        {

            LinesNumber = lines.Length;
            var type = lines[0][13];
            if (type == '5' || type == 'C')
            {
                this.IsVoid = true;
                return;
            }
            try
            {
                var Phone = lines[0][70];
                PhoneIndicator = Phone == '1';
            }
            catch (Exception ex)
            {

            }
        }

        private Dictionary<string, string> ParseM0Var(string[] lines)
        {
            var m0VarData = new Dictionary<string, string>();
            m0VarData.Add("TravelPolicyNumber", isInIndexAndNotEmpty(lines, 1) ? lines[1] : "");
            m0VarData.Add("Address11", isInIndexAndNotEmpty(lines, 2) ? lines[2].Trim() : "");
            m0VarData.Add("Address12", isInIndexAndNotEmpty(lines, 3) ? lines[3].Trim() : "");
            m0VarData.Add("Address13", isInIndexAndNotEmpty(lines, 4) ? lines[4].Trim() : "");
            m0VarData.Add("ddress14", isInIndexAndNotEmpty(lines, 5) ? lines[5].Trim() : "");
            m0VarData.Add("Address15", isInIndexAndNotEmpty(lines, 6) ? lines[6].Trim() : "");
            m0VarData.Add("Address21", isInIndexAndNotEmpty(lines, 7) ? lines[7].Trim() : "");
            m0VarData.Add("Address22", isInIndexAndNotEmpty(lines, 8) ? lines[8].Trim() : "");
            m0VarData.Add("Address23", isInIndexAndNotEmpty(lines, 9) ? lines[9].Trim() : "");
            m0VarData.Add("Address24", isInIndexAndNotEmpty(lines, 10) ? lines[10].Trim() : "");
            m0VarData.Add("Address25", isInIndexAndNotEmpty(lines, 11) ? lines[11].Trim() : "");
            m0VarData.Add("Phone1", isInIndexAndNotEmpty(lines, 12) ? lines[12].Trim() : "");
            m0VarData.Add("Phone2", isInIndexAndNotEmpty(lines, 13) ? lines[13].Trim() : "");
            m0VarData.Add("Phone3", isInIndexAndNotEmpty(lines, 14) ? lines[14].Trim() : "");
            m0VarData.Add("Received", isInIndexAndNotEmpty(lines, 15) ? lines[15].Trim() : "");
            return m0VarData;
        }

        private Dictionary<string, string> parseM1Var(string[] lines)
        {
            List<string> LabelMap = new List<string>() { "M3L", "M5L", "M7L", "M8L", "M9L" };
            var m1VarData = new Dictionary<string, string>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("M1"))
                {
                    LastIndex = i;
                    this.LastPax = lines[i].Substring(2, 4);
                    int label = 0;
                    for (int k = LastIndex + 1; k < LastIndex + 6; k++)
                    {
                        m1VarData.Add($"{this.LastPax}${LabelMap[label]}", lines[i]);
                        label += 1;
                    }
                }
            }
            return m1VarData;
        }

        private bool isInIndexAndNotEmpty(string[] lines, int index)
        {
            if (index < 0)
            {
                return false;
            }
            if (index >= lines.Length)
            {
                return false;
            }
            if (String.IsNullOrEmpty(lines[index]))
            {
                return false;
            }
            return true;
        }

        public override IurObject getObject()
        {
            return new VariableDataObject(this);
        }
    }

    public class VariableDataObject : IurObject
    {
        public bool isVoid { get; set; }
        public string travelPolicyNumber { get; set; }
        public string address11 { get; set; }
        public string address12 { get; set; }
        public string address13 { get; set; }
        public string address14 { get; set; }
        public string address15 { get; set; }
        public string address21 { get; set; }
        public string address22 { get; set; }
        public string address23 { get; set; }
        public string address24 { get; set; }
        public string address25 { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string phone3 { get; set; }
        public string received { get; set; }
        public Dictionary<string, string> m1VarData = new Dictionary<string, string>();

        public VariableDataObject(VariableDataCollector data)
        {
            this.isVoid = data.IsVoid;
            this.travelPolicyNumber = data.M0VarData.ContainsKey("travelPolicyNumber") ? data.M0VarData["travelPolicyNumber"] : "";
            this.address11 = data.M0VarData.ContainsKey("address11") ? data.M0VarData["address11"].Trim() : "";
            this.address12 = data.M0VarData.ContainsKey("address12") ? data.M0VarData["address12"].Trim() : "";
            this.address13 = data.M0VarData.ContainsKey("address13") ? data.M0VarData["address13"].Trim() : "";
            this.address14 = data.M0VarData.ContainsKey("address14") ? data.M0VarData["address14"].Trim() : "";
            this.address15 = data.M0VarData.ContainsKey("address15") ? data.M0VarData["address15"].Trim() : "";
            this.address21 = data.M0VarData.ContainsKey("address21") ? data.M0VarData["address21"].Trim() : "";
            this.address22 = data.M0VarData.ContainsKey("address22") ? data.M0VarData["address22"].Trim() : "";
            this.address23 = data.M0VarData.ContainsKey("address23") ? data.M0VarData["address23"].Trim() : "";
            this.address24 = data.M0VarData.ContainsKey("address24") ? data.M0VarData["address24"].Trim() : "";
            this.address25 = data.M0VarData.ContainsKey("address25") ? data.M0VarData["address25"].Trim() : "";
            this.phone1 = data.M0VarData.ContainsKey("phone1") ? data.M0VarData["phone1"].Trim() : "";
            this.phone2 = data.M0VarData.ContainsKey("phone2") ? data.M0VarData["phone2"].Trim() : "";
            this.phone3 = data.M0VarData.ContainsKey("phone3") ? data.M0VarData["phone3"].Trim() : "";
            this.received = data.M0VarData.ContainsKey("received") ? data.M0VarData["received"].Trim() : "";
            this.m1VarData = data.M1VarData;
        }
    }

}
