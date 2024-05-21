using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Lernen
{
    internal class Logging
    {
        private string logFile;
        public Logging(string PathAndFilename) { 
        logFile = PathAndFilename;
        }
        public void Log(string text,LogType logType)
        {
            StreamWriter logwriter = new StreamWriter(logFile,true);
            logwriter.WriteLine(DateTime.Now + "[" + Enum.GetName(logType) + "] " + text);
            logwriter.Close();
        }
        public string DGVtoString(DataGridView dgv, char delimiter)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                sb.Append(Environment.NewLine);
                foreach (DataGridViewCell cell in row.Cells)
                {
                    sb.Append(cell.Value);
                    sb.Append(delimiter);
                   
                }
                sb.Remove(sb.Length - 1, 1); // Removes the last delimiter 
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }


    enum LogType
    {
        Input,
        Output,
        Error,
        System
    }
}
