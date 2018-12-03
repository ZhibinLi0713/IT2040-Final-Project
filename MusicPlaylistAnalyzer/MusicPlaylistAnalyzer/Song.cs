using System;
namespace MusicPlaylistAnalyzer
{
    public class Song
    {
        public String Name { get; set; }
        public String Artist { get; set; }
        public String Album { get; set; }
        public String Genre { get; set; }
        public long Size { get; set; }
        public int Time { get; set; }
        public int Year { get; set; }
        public int Plays { get; set; }

        override public string ToString()
        {
            return String.Format("Name: {0}, Artist: {1}, Album: {2}, Genre: {3}, Size: {4}, Time: {5}, Year: {6}, Plays: {7}", Name, Artist, Album, Genre, Size, Time, Year, Plays);
        }

    }
}
