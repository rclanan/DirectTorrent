using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectTorrent.Logic.Models
{
    /// <summary>
    /// Represents the quality of the video.
    /// </summary>

    #region Enums
    /// <summary>
    /// Represents the quality of the video.
    /// </summary>
    public enum Quality
    {
        HD,
        FHD,
        ThreeD,
        ALL
    }

    /// <summary>
    /// Represents the sorting criteria by which the results will be sorted.
    /// </summary>
    public enum Sort
    {
        Title,
        Year,
        Rating,
        Peers,
        Seeds,
        DownloadCount,
        LikeCount,
        DateAdded
    }

    /// <summary>
    /// Represents the order in which the results will be sorted.
    /// </summary>
    public enum Order
    {
        Ascending,
        Descending
    }
    #endregion enums

    /// <summary>
    /// Represents the data associated to a torrent.
    /// </summary>
    public class Torrent
    {
        /// <summary>
        /// Gets the url of the torrent.
        /// </summary>
        public Uri Url { get; private set; }
        /// <summary>
        /// Gets the hash of the torrent.
        /// </summary>
        public string Hash { get; private set; }
        /// <summary>
        /// Gets the quality of the movie torrent.
        /// </summary>
        public Quality Quality { get; private set; }
        /// <summary>
        /// Gets the amount of seeds.
        /// </summary>
        public int Seeds { get; private set; }
        /// <summary>
        /// Gets the amount of peers.
        /// </summary>
        public int Peers { get; private set; }
        /// <summary>
        /// Gets the size of the movie.
        /// </summary>
        public string Size { get; private set; }
        /// <summary>
        /// Gets the size of the movie represented in bytes.
        /// </summary>
        public long SizeBytes { get; private set; }
        /// <summary>
        /// Gets the date when the torrent was uploaded.
        /// </summary>
        public DateTime DateUploaded { get; private set; }
        /// <summary>
        /// Gets the date when the torrent was uploaded as a unix timestamp.
        /// </summary>
        public int DateUploadedUnix { get; private set; }
    }

    /// <summary>
    /// Represents the data associated to the director.
    /// </summary>
    public class Director
    {
        /// <summary>
        /// Gets the director's name.
        /// </summary>
        public string Name { get; private set; }
        public string SmallImage { get; private set; }
        public string MediumImage { get; private set; }
    }

    /// <summary>
    /// Represents the the data associated to the actor.
    /// </summary>
    public class Actor
    {
        /// <summary>
        /// Gets the actor's name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the actor's character name.
        /// </summary>
        public string CharacterName { get; private set; }
        public string SmallImage { get; private set; }
        public string MediumImage { get; private set; }
    }

    /// <summary>
    /// Represents the set of images associated to the movie.
    /// </summary>
    public class Images
    {
        public string BackgroundImage { get; private set; }
        public string SmallCoverImage { get; private set; }
        public string MediumCoverImage { get; private set; }
        public string LargeCoverImage { get; private set; }
        public string MediumScreenshotImage1 { get; private set; }
        public string MediumScreenshotImage2 { get; private set; }
        public string MediumScreenshotImage3 { get; private set; }
        public string LargeScreenshotImage1 { get; private set; }
        public string LargeScreenshotImage2 { get; private set; }
        public string LargeScreenshotImage3 { get; private set; }
    }
}
