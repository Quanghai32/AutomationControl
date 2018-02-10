using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineModel
{
    public class KeyenceMemory
    {
        public static string[] ListFormatName { get; set; } = new string[] {"Unsigned 16-bit DEC (.U)", "Signed 16-bit DEC (.S)", "Unsigned 32-bit DEC (.D)",
            "Signed 32-bit DEC (.L)", "16-bit HEX (.H)"};
        public static string[] ListFormatSymbol { get; set; } = new string[] { ".U", ".S", ".D", ".L", ".H" };
        public static string[] ListMemoryTypeName { get; set; } = new string[] {"Relay (R)", "Link relay (B)", "Internal auxiliary relay (MR)", "Latch relay (LR)",
            "Control relay (CR)", "Virtual relay (VB)", "Data memory (DM)", "Extended data memory (EM)",
            "File register (FM)", "File register (ZF)", "Link register (W)", "Temporary data memory (TM)",
            "Index register (Z)", "Timer (T)", "Timer (current value) (TC)", "Timer (setting value) (TS)",
            "Counter (C)", "Counter (current value) (CC)", "Counter (setting value) (CS)",
            "High-speed counter (CTH)", "High-speed counter comparator (setting value) (CTC)",
            "Digital trimmer (AT)", "Control memory (CM)", "Virtual memory (VM)"};
        public static string[] ListMemoryTypeSymbol { get; set; } = new string[] {"R", "B", "MR", "LR", "CR", "VB", "DM", "EM", "FM", "ZF", "W", "TM", "Z",
            "T", "TC", "TS", "C", "CC", "CS", "CTH", "CTC", "AT", "CM", "VM"};

        public KeyenceMemory()
        {
            MemoryAddress = 0;
            TypeIndex = 0;
            FormatIndex = 0;
        }

        #region "Memory Type"
        private int typeIndex;
        public int TypeIndex
        {
            get
            {
                return typeIndex;
            }
            set
            {
                typeIndex = value;
                TypeDisplay = ListMemoryTypeName[value];
                TypeSymbol = ListMemoryTypeSymbol[value];
                UpdateDiplayValue();
            }
        }

        private string TypeDisplay;

        public string TypeSymbol { get; set; }
        #endregion

        #region "Format"
        private int formatIndex;

        public int FormatIndex
        {
            get
            {
                return formatIndex;
            }
            set
            {
                formatIndex = value;
                FormatDisplay = ListFormatName[formatIndex];
                FormatSymbol = ListFormatSymbol[formatIndex];
                UpdateDiplayValue();
            }
        }

        private string FormatDisplay;

        public string FormatSymbol { get; set; }
        #endregion

        private int memoryAddress;
        public int MemoryAddress
        {
            get { return memoryAddress; }
            set
            {
                memoryAddress = value;
                UpdateDiplayValue();
            }
        }

        private void UpdateDiplayValue()
        {
            DisplayValue = TypeSymbol + MemoryAddress.ToString() + " (" + FormatSymbol + ")";
        }

        public string DisplayValue { get; set; }
    }
}
