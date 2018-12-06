using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylistAnalyzer
{
    class Program
    {

        static List<Song> ReadSongs(string filepath)
        {
            List<Song> result = new List<Song>();
            FileStream fs = new FileStream(filepath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            var line = sr.ReadLine();
            line = sr.ReadLine();
            while (line != null && line.Trim()!="")
            {
                var parts = line.Split('\t');
                if (parts.Length == 8)
                {
                    try
                    {
                        result.Add(new Song
                        {
                            Name = parts[0],
                            Artist = parts[1],
                            Album = parts[2],
                            Genre = parts[3],
                            Size = Convert.ToInt32(parts[4]),
                            Time = Convert.ToInt32(parts[5]),
                            Year = Convert.ToInt32(parts[6]),
                            Plays = Convert.ToInt32(parts[7])
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Record contains data that is not of the right type.");
                        throw new Exception();
                    }
                }else
                {
                    Console.WriteLine("Record doesn't contain the correct number of data elements.");
                    throw new Exception();
                }
                line = sr.ReadLine();
            }
            fs.Close();
            return result;
        }

        static void Main(string[] args)
        {
            if(args.Length==0)
            {
                Console.WriteLine("Please enter 2 command line parameters!");
                return;
            }
            string infile = args[0];
            string outfile = args[1];

            List<Song> songs = null;
            try
            {
                songs = ReadSongs(infile);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Playlist data file can't be opened.");
                return;
            }
            catch (IOException e)
            {
                Console.WriteLine("Error occurs reading lines from playlist data file.");
                return;
            }
            catch (Exception e)
            {
                return;
            }

            try {
                FileStream fout = new FileStream(outfile, FileMode.Create);
                StreamWriter sw = new StreamWriter(fout);
                Q1(songs, sw);
                Q2(songs, sw);
                Q3(songs, sw);
                Q4(songs, sw);
                Q5(songs, sw);
                Q6(songs, sw);
                Q7(songs, sw);
                sw.Flush();
                fout.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Report file can t be opened or written to.");
            }
        }

        static void Q1(List<Song> songs, StreamWriter sw)
        {
            var result = from song in songs where song.Plays >= 200 select song;
            sw.WriteLine("Songs that received 200 or more plays:");
            foreach (var song in result)
            {
                sw.WriteLine(song.ToString());
            }
            sw.WriteLine();
        }

        static void Q2(List<Song> songs, StreamWriter sw)
        {
            var result = from song in songs where song.Genre == "Alternative" select song;
            sw.WriteLine(string.Format("Number of Alterative songs: {0}", result.Count()));
            sw.WriteLine();

        }

        static void Q3(List<Song> songs, StreamWriter sw)
        {
            var result = from song in songs where song.Genre.Trim().ToLower() == "Hip-Hop/Rap".ToLower() select song;
            sw.WriteLine(string.Format("Number of Hip-Hop/Rap songs: {0}", result.Count()));
            sw.WriteLine();
        }

        static void Q4(List<Song> songs, StreamWriter sw)
        {
            var result = from song in songs where song.Album == "Welcome to the Fishbowl" select song;
            sw.WriteLine("Songs from the album Welcome to the fishbowl:");
            foreach (var song in result)
            {
                sw.WriteLine(song.ToString());
            }
            sw.WriteLine();
        }

        static void Q5(List<Song> songs, StreamWriter sw)
        {
            sw.WriteLine("Songs from before 1970: ");
            var result = from song in songs where song.Year<1970 select song;
            foreach (var song in result)
            {
                sw.WriteLine(song.ToString());
            }
            sw.WriteLine();
        }

        static void Q6(List<Song> songs, StreamWriter sw)
        {
            sw.WriteLine("Song names longer than 85 characters: ");
            var result = from song in songs where song.Name.Length > 85 select song;
            foreach (var song in result)
            {
                sw.WriteLine(song.ToString());
            }
            sw.WriteLine();
        }

        static void Q7(List<Song> songs, StreamWriter sw)
        {
            var r = from song in songs orderby song.Time descending select song;
            var longestSong = r.First();
            sw.WriteLine("Longest song: "+longestSong);
            sw.WriteLine();
        }

    }
}
