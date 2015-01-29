using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectTorrent.Data.Models
{
    public class CastList
    {
        public string ActorName { get; set; }
        public string CharacterName { get; set; }
        public string ActorImdbCode { get; set; }
        public string ActorImdbLink { get; set; }
    }

    public class DirectorList
    {
        public string DirectorName { get; set; }
        public string DirectorImdbCode { get; set; }
        public string DirectorImdbLink { get; set; }
    }

    public class MovieDetails
    {
        public string MovieID { get; set; }
        public string MovieUrl { get; set; }
        public string DateUploaded { get; set; }
        public int DateUploadedEpoch { get; set; }
        public string Uploader { get; set; }
        public string UploaderUID { get; set; }
        public object UploaderNotes { get; set; }
        public string Quality { get; set; }
        public string Resolution { get; set; }
        public string FrameRate { get; set; }
        public string Language { get; set; }
        public string Subtitles { get; set; }
        public string LargeCover { get; set; }
        public string MediumCover { get; set; }
        public string LargeScreenshot1 { get; set; }
        public string LargeScreenshot2 { get; set; }
        public string LargeScreenshot3 { get; set; }
        public string MediumScreenshot1 { get; set; }
        public string MediumScreenshot2 { get; set; }
        public string MediumScreenshot3 { get; set; }
        public string ImdbCode { get; set; }
        public string ImdbLink { get; set; }
        public string MovieTitle { get; set; }
        public string MovieTitleClean { get; set; }
        public string MovieYear { get; set; }
        public string MovieRating { get; set; }
        public string MovieRuntime { get; set; }
        public string YoutubeTrailerID { get; set; }
        public string YoutubeTrailerUrl { get; set; }
        public string AgeRating { get; set; }
        public string Genre1 { get; set; }
        public string Genre2 { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<CastList> CastList { get; set; }
        public List<DirectorList> DirectorList { get; set; }
        public string Downloaded { get; set; }
        public string TorrentUrl { get; set; }
        public string TorrentHash { get; set; }
        public string TorrentMagnetUrl { get; set; }
        public string TorrentSeeds { get; set; }
        public string TorrentPeers { get; set; }
        public string Size { get; set; }
        public string SizeByte { get; set; }
    }
}
