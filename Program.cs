namespace taglist_csv_er
{
    internal class Program
    {
        const int max_char_count = 64;
        static void Main(string[] args)
        {
            string filepath = "C:\\Users\\Joe bingle\\Downloads\\tagnames.txt";
            StreamReader sR = new StreamReader(filepath);
            StreamWriter sW = new StreamWriter(filepath + "_filtered.csv");

            string header = "tagID";
            for (int i = 0; i < max_char_count; i++)
                header += "," + i;
            sW.WriteLine(header);

            while (true){
                string curr_line = sR.ReadLine();
                if (string.IsNullOrEmpty(curr_line)) break; // no string found, file ended

                string tagid = curr_line.Substring(0, 8);
                string tagname = curr_line.Substring(11);
                if (tagname.Length > max_char_count) continue; // we cant process this string

                // convert hex value to unsigned decimal
                string csv_columns = Convert.ToUInt32(tagid, 16).ToString();
                // convert tag name to a buncha chars
                for (int i = 0; i < max_char_count; i++)
                    if (tagname.Length > i)
                        csv_columns += "," + (byte)tagname[i];
                    else csv_columns += ",0"; // empty column

                sW.WriteLine(csv_columns);
            }
            sR.Close();
            sW.Close();
        }
    }
}