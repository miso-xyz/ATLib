using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ATLib
{
    public class Utils
    {
        public class MusicMetadata
        {
            // General
            public string UploadDate { get; set; }
            public string ModifiedDate { get; set; }
            public string CreationDate { get; set; }
            public string PublishedDate { get; set; }

            public string TrackID { get; set; }
            public string TrackDescription { get; set; }

            public string TrackName { get; set; }
            public string AuthorName { get; set; }
            public string SiteName { get; set; } // "audiotool"

            public bool FamilyFriendly { get; set; }
            public string Keywords { get; set; }
            public int Version { get; set; }

            public int ChartsPlacement { get; set; }
            public string ChartsDate { get; set; }
            public string ChartsURL { get; set; }
            public string ChartsGenre { get; set; }

            public int Downloads { get; set; }
            public int Plays { get; set; }
            public int Likes { get; set; }
            public int Comments { get; set; }

            public string TrackDuration { get; set; }
            public double TrackBPM { get; set; }
            public int Bitrate { get; set; }
            public string EncodingFormat { get; set; } // "mp3"

            public string ArtistURL { get; set; }
            public string ThumbnailURL { get; set; }
            public string TrackContentURL { get; set; }
            public string TrackAPIURL { get; set; }
            public string TrackKeyURL { get; set; }
            public string TrackImageURL { get; set; }
            public string EmbededSWFURL { get; set; }

            // OG (SWF)

            public string VideoURLSWF { get; set; }
            public string TrackURLSWF { get; set; }
            public string TrackImageURLSWF { get; set; }

            public string SiteNameSWF { get; set; }
            public string TrackNameSWF { get; set; }

            public string ContentTypeSWF { get; set; } // "audiotool:track"
            public string VideoTypeSWF { get; set; }
            public int VideHeightSWF { get; set; }

            // Twitter
            public string TrackURLTwitter { get; set; }
            public string DomainURLTwitter { get; set; }
            public string StreamURLTwitter { get; set; }
            public string PlayerURLTwitter { get; set; }

            public int PlayerWidthTwitter { get; set; } // 512
            public int PlayerHeightTwitter { get; set; } // 512
            public string PlayerImageTwitter { get; set; }

            public string TrackDescriptionTwitter { get; set; }
            public string TrackTitleTwitter { get; set; }
            public string TrackDomainTwitter { get; set; }

            public string CardTypeTwitter { get; set; } // "card"
            public string ContentTypeTwitter { get; set; } // "audio/mpeg"

        }
    }
}
