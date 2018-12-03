using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlaylistAnalyzer
{
    class Program
    {
         static void Main(string[] args)
        {
            Console.WriteLine($"the arguments are {args[0]} and {args[1]}");
            if (args.Length == 2)
            {
                List<Song> songList = new List<Song>();
                if (Utilities.ReadFromFile(args[0], songList))
                {
                    String result = GenerateReport(songList);
                    if(Utilities.WriteToFile(args[1], result))
                    {
                        Console.WriteLine("Report created successfull");
                    }

                }
            }
            else
            {
                Console.WriteLine("Unable to run without 2 arguments, Correct Systax:- MusicPlaylistAnalyzer <music_playlist_file_path> <report_file_path>");
            }


        }

       static String GenerateReport(List<Song> songs)
        {
            String result = "";
            result += "Music Playlist Report\n\n";
            var moreThen200Plays = from song in songs where song.Plays >= 200 select song;
            result = AddSongsToResult(result, moreThen200Plays, "Songs That received 200 or more plays:");
           
            var alernativeCount = (from song in songs where song.Genre == "Alternative" select song).Count();
            result=AddCountsToResult(result, alernativeCount, "Number of Altnerative songs:");

            var hipHopCount = (from song in songs where song.Genre == "Hip-Hop/Rap" select song).Count();
            result=AddCountsToResult(result, hipHopCount, "Number of Hip-Hop/Rap songs:");

            var selectAlbumFishbowl = from song in songs where song.Album == "Welcome to the Fishbowl" select song;
            result = AddSongsToResult(result, selectAlbumFishbowl, "Songs from the album Welcome to the Fishbowl:");

            var songsBefore1970 = from song in songs where song.Year < 1970 select song;
            result = AddSongsToResult(result, songsBefore1970, "Songs from before 1970:");

            var songNamesLongerThen85Char = from song in songs where song.Name.Length > 85 select song;
            result = AddSongsToResult(result, songNamesLongerThen85Char, "Song names longer than 85 characters:");

            var longestSong = (from song in songs orderby song.Time descending select song).First();
            result += "longest Song:\n";
            result += longestSong;

            return result;
        }

        static String AddSongsToResult(String result,System.Collections.Generic.IEnumerable<Song> songs,String title)
        {
            result += title + "\n";
            foreach(Song song in songs)
            {
                result += song+"\n";
            }
            result += "\n\n";
            return result;
           
        }
        static String AddCountsToResult(String result,int count, String title)
        {
            result += title + "\n";
            result += count;
            result += "\n\n";
            return result;
        }

    }
}
