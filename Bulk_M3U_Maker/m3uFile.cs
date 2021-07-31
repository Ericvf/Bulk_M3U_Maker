using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bulk_M3U_Maker
{
    public class m3uFile
    {
        public List<String> mp3Files;
        public string Path;
        public string Name;

        public m3uFile(string sPath)
        {
            this.mp3Files = new List<String>();
            this.Path = sPath;

            this.SetNameFromPath(sPath);
        }

        public void SetNameFromPath(string sPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(sPath);
            this.Name = dirInfo.Name;
        }

        public void Save()
        {
            string fileName = String.Format(@"{0}/{1}.m3u", this.Path, this.Name);
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            if (!fileStream.CanWrite)
            {
                fileStream.Close();
                return;
            }

            StreamWriter streamWriter = new StreamWriter(fileStream);
            try
            {
                streamWriter.WriteLine("#EXTM3U");



                foreach (string sMp3File in this.mp3Files)
                {
                    fileName = new FileInfo(sMp3File).Name;
                    streamWriter.WriteLine(String.Format("#EXTINF:0,{0}", fileName));
                    streamWriter.WriteLine(fileName);
                    streamWriter.WriteLine();
                }
            }
            finally
            {
                streamWriter.Flush();
                streamWriter.Close();
                fileStream.Close();
            }

        }
    }
}
