using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectTorrent.Data.ApiWrappers;
using Newtonsoft.Json;

namespace DirectTorrent.Data.Models
{
    public class Actor : ICloneable
    {
        public string ActorName { get; set; }
        public string CharacterName { get; set; }
        public string ActorImdbCode { get; set; }
        public string ActorImdbLink { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Director : ICloneable
    {
        public string DirectorName { get; set; }
        public string DirectorImdbCode { get; set; }
        public string DirectorImdbLink { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class MovieDetails
    {
        #region Properties

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
        public List<Actor> CastList { get; set; }
        public List<Director> DirectorList { get; set; }
        public string Downloaded { get; set; }
        public string TorrentUrl { get; set; }
        public string TorrentHash { get; set; }
        public string TorrentMagnetUrl { get; set; }
        public string TorrentSeeds { get; set; }
        public string TorrentPeers { get; set; }
        public string Size { get; set; }
        public string SizeByte { get; set; }

        #endregion

        public MovieDetails(int movId)
        {
            var temp = JsonConvert.DeserializeObject<MovieDetails>(YifyWrapper.GetMovieDetails(movId));
            this.AgeRating = temp.AgeRating;
            this.CastList = temp.CastList.Select(x => (Actor) x.Clone()).ToList();
            this.DateUploaded = temp.DateUploaded;
            this.DateUploadedEpoch = temp.DateUploadedEpoch;
            this.DirectorList = temp.DirectorList.Select(x => (Director) x.Clone()).ToList();
            this.Downloaded = temp.Downloaded;
            this.FrameRate = temp.FrameRate;
            this.Genre1 = temp.Genre1;
            this.Genre2 = temp.Genre2;
            this.ImdbCode = temp.ImdbCode;
            this.ImdbLink = temp.ImdbLink;
            this.Language = temp.Language;
            this.LargeCover = temp.LargeCover;
            this.LargeScreenshot1 = temp.LargeScreenshot1;
            this.LargeScreenshot2 = temp.LargeScreenshot2;
            this.LargeScreenshot3 = temp.LargeScreenshot3;
            this.LongDescription = temp.LongDescription;
            this.MediumCover = temp.MediumCover;
            this.MediumScreenshot1 = temp.MediumScreenshot1;
            this.MediumScreenshot2 = temp.MediumScreenshot2;
            this.MediumScreenshot3 = temp.MediumScreenshot3;
            this.MovieID = temp.MovieID;
            this.MovieRating = temp.MovieRating;
            this.MovieRuntime = temp.MovieRuntime;
            this.MovieTitle = temp.MovieTitle;
            this.MovieTitleClean = temp.MovieTitleClean;
            this.MovieUrl = temp.MovieUrl;
            this.MovieYear = temp.MovieYear;
            this.Quality = temp.Quality;
            this.Resolution = temp.Resolution;
            this.ShortDescription = temp.ShortDescription;
            this.Size = temp.Size;
            this.SizeByte = temp.SizeByte;
            this.Subtitles = temp.Subtitles;
            this.TorrentHash = temp.TorrentHash;
            this.TorrentMagnetUrl = temp.TorrentMagnetUrl;
            this.TorrentPeers = temp.TorrentPeers;
            this.TorrentSeeds = temp.TorrentSeeds;
            this.TorrentUrl = temp.TorrentUrl;
            this.Uploader = temp.Uploader;
            this.UploaderNotes = temp.UploaderNotes;
            this.UploaderUID = temp.UploaderUID;
            this.YoutubeTrailerID = temp.YoutubeTrailerID;
            this.YoutubeTrailerUrl = temp.YoutubeTrailerUrl;
        }
    }
}