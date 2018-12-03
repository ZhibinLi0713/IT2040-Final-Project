using System;
using System.Collections.Generic;

namespace MusicPlaylistAnalyzer
{
    public class Utilities
    {
        public Utilities()
        {
        }

        public static bool MapToSongObject(String line, List<Song> songList,int row)
        {
            int NumItemsInRow = 8;
            bool result = true;
            String[] values = line.Split('\t');
            if (values.Length == NumItemsInRow)
            {
                Song song = new Song
                {
                    Name = values[0],
                    Artist = values[1],
                    Album = values[2],
                    Genre = values[3],
                    Size = long.Parse(values[4]),
                    Time = int.Parse(values[5]),
                    Year = int.Parse(values[6]),
                    Plays = int.Parse(values[7])
                };
                songList.Add(song);
            }
            else
            {
                Console.WriteLine($"Row {row} contains {values.Length} values. It should contain {NumItemsInRow}");
                result = false;
            }
            return result;
        }
       public static bool ReadFromFile(String fileLoc, List<Song> songList)
        {
            Boolean result = true;
            string line;
            System.IO.StreamReader file = null;
            int i = 2;
            try
            {
              file = new System.IO.StreamReader(@fileLoc);
                String headerLine = file.ReadLine(); //ignore header line for this assignment
                while ((line = file.ReadLine()) != null)
                {
                    if(!Utilities.MapToSongObject(line, songList,i))
                    {
                        result = false;
                        break;
                    }
                    i++;

                }
            }catch(System.IO.FileNotFoundException fe)
            {
                Console.WriteLine("error: {0}", fe.Message);
                result = false;
                return result;
            }
            catch(FormatException forEx)
            {
                Console.WriteLine($"Row {i} contains invalid types for either (Size,Time,Year,Plays). They should contain an integer.Details: {forEx.Message}");
                result = false;
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine("Unable to read the file: {0}", ex.Message);
                result = false;
                return result;
            }
            file.Close();
            return result;
        }

        public static bool WriteToFile(String fileLoc,String result)
        {
            bool output=true;
            try
            {
                System.IO.File.WriteAllText(@fileLoc, result);
            }catch(System.IO.IOException ex)
            {
                output=false;
                Console.WriteLine("Unable to read the file: {0}", ex.Message);

            }
            return output;
        }



    }
}
