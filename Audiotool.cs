using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Drawing;

namespace ATLib
{
    public class Artist
    {
        public Artist(string url) { URL = url; }
        public Image FullAvatar { get; set; }
        public Image LowResAvatar { get; set; }

        public string Name { get; set; }
        public string OriginalName { get; set; }

        public string URL { get; set; }
        public Music[] Musics { get;set; }
    }

    public class BaseComment
    {
        public int ID { get; set; }
        public string CreationDate { get; set; }

        public int Rating { get; set; }

        public Artist Artist { get; set; }
        public string Comment { get; set; }
    }

    public class Comment : BaseComment { public BaseComment[] Replies { get; set; } }

    public class Music
    {
        private static string GetArtistURL(ItemProp[] props) { foreach (ItemProp prop in props) { if (prop.Name == "audiotool:artist") { return prop.Content; } } throw new Exception("Failed to find Artist URL!"); }

        public Music(string trackURL)
        {
            _Props = PageReader.GetProps(trackURL);
            Metadata = new Utils.MusicMetadata();
            Artist = new Artist(GetArtistURL(_Props));
            URL = trackURL;
            Initialize(_Props, this);
        }

        private void Initialize(ItemProp[] props, Music tempMusic)
        {
            foreach (ItemProp prop in props)
            {
                //if (prop.Content == null || prop.Content == "") { continue; }
                switch (prop.Name)
                {
                    case "byArtist": tempMusic.Artist.Name = prop.Content; break;
                    case "description": tempMusic.Metadata.TrackDescription = prop.Content; break;
                    case "thumbnailURL": tempMusic.Metadata.TrackImageURL = prop.Content; tempMusic.Image = Image.FromStream(new MemoryStream(new WebClient().DownloadData(prop.Content))); break;
                    case "name": tempMusic.Title = prop.Content; break;
                    case "uploadDate": tempMusic.Metadata.UploadDate = prop.Content; break;
                    case "dateCreated": tempMusic.Metadata.CreationDate = prop.Content; break;
                    case "dateModified": tempMusic.Metadata.ModifiedDate = prop.Content; break;
                    case "datePublished": tempMusic.Metadata.PublishedDate = prop.Content; break;
                    case "isFamilyFriendly": tempMusic.Metadata.FamilyFriendly = ("true" == prop.Content ? true : false); break;
                    case "interactionCount":
                        switch (prop.Content.Split(':')[0])
                        {
                            case "UserDownloads": tempMusic.Metadata.Downloads = tempMusic.Downloads = Convert.ToInt32(prop.Content.Split(':')[1]); break;
                            case "UserPlays": tempMusic.Metadata.Plays = tempMusic.Plays = Convert.ToInt32(prop.Content.Split(':')[1]); break;
                            case "UserLikes": tempMusic.Metadata.Likes = tempMusic.Likes = Convert.ToInt32(prop.Content.Split(':')[1]); break;
                            case "UserComments": tempMusic.Metadata.Comments = tempMusic.Comments = Convert.ToInt32(prop.Content.Split(':')[1]); break;
                        }
                        break;
                    case "keywords": tempMusic.Metadata.Keywords = tempMusic.Keywords = prop.Content; break;
                    case "version": tempMusic.Metadata.Version = Convert.ToInt32(prop.Content); break;
                    case "duration": tempMusic.Metadata.TrackDuration = prop.Content.Replace("PT", null); tempMusic.Duration = (Convert.ToInt32(prop.Content.Replace("PT", null).Split('M')[0]) * 60) + Convert.ToInt32(prop.Content.Split('M')[1].Replace("S", null)); break;
                    case "bpm": tempMusic.Metadata.TrackBPM = tempMusic.BPM = Convert.ToDouble(prop.Content.Replace(".", ",")); break;
                    case "bitrate": tempMusic.Metadata.Bitrate = Convert.ToInt32(prop.Content); break;
                    case "audio": tempMusic.Metadata.TrackAPIURL = tempMusic.apiURL = prop.Content; break;
                    case "contentURL": tempMusic.Metadata.TrackContentURL = prop.Content; break;
                    case "embedURL": tempMusic.Metadata.EmbededSWFURL = prop.Content; break;
                    case "audiotool:artist": tempMusic.Artist.URL = tempMusic.Metadata.ArtistURL = prop.Content; break;
                }
            }
        }
        private ItemProp[] _Props { get; set; }

        public Utils.MusicMetadata Metadata { get; set; }
        public Artist Artist { get; set; }

        public string Title { get; set; }
        public string URL { get; set; }
        public string apiURL { get; set; }
        public Image Image { get; set; }
        //public Image ProjectImage { get; set; } - will be added

        public string Keywords { get; set; }

        public double BPM { get; set; }
        public int Duration { get; set; }
        public int Likes { get; set; }
        public int Plays { get; set; }
        public int Comments { get; set; }
        public int Downloads { get; set; }
    }

    public class Listeners
    {
        public string ID { get; set; }
        public Artist Artist { get; set; }
    }
}
