using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineModel
{
    public class JsonDB
    {
        private static JsonDB _Instance;
        public static JsonDB Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new JsonDB();
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }

        public T Read<T>(string path)
        {
            T data;
            using (StreamReader file = new StreamReader(path))
            {
                string dataJson = string.Empty;
                dataJson = file.ReadToEnd();
                data = JsonConvert.DeserializeObject<T>(dataJson);
            }
            return data;
        }

        public void Write(string path, object data)
        {
            string jsonFile = string.Empty;
            jsonFile = JsonConvert.SerializeObject((data), Formatting.Indented);
            File.WriteAllText(path, jsonFile);
        }
    }
}
