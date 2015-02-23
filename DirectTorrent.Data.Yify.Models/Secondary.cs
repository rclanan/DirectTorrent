using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace DirectTorrent.Data.Yify.Models
{
    /// <summary>
    /// Represents the data associated to the director.
    /// </summary>
    public class Director : ICloneable
    {
        /// <summary>
        /// Gets the director's name.
        /// </summary>
        public string Name { get; private set; }

        public string SmallImage { get; private set; }
        public string MediumImage { get; private set; }

        [JsonConstructor]
        internal Director(string name, string small_image, string medium_image)
        {
            this.Name = name;
            // TODO: Convert to images?
            this.SmallImage = small_image;
            this.MediumImage = medium_image;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// Represents the the data associated to the actor.
    /// </summary>
    public class Actor : ICloneable
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

        [JsonConstructor]
        internal Actor(string name, string character_name, string small_image, string medium_image)
        {
            this.Name = name;
            this.CharacterName = character_name;
            // TODO: Convert to images?
            this.SmallImage = small_image;
            this.MediumImage = medium_image;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
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

        [JsonConstructor]
        internal Images(string background_image, string small_cover_image, string medium_cover_image,
            string large_cover_image, string medium_screenshot_image1, string medium_screenshot_image2,
            string medium_screenshot_image3, string large_screenshot_image1, string large_screenshot_image2,
            string large_screenshot_image3)
        {
            // TODO: Convert to images?
            this.BackgroundImage = background_image;
            this.SmallCoverImage = small_cover_image;
            this.LargeCoverImage = large_cover_image;
            this.MediumCoverImage = medium_cover_image;
            this.MediumScreenshotImage1 = medium_screenshot_image1;
            this.MediumScreenshotImage2 = medium_screenshot_image2;
            this.MediumScreenshotImage3 = medium_screenshot_image3;
            this.LargeScreenshotImage1 = large_screenshot_image1;
            this.LargeScreenshotImage2 = large_screenshot_image2;
            this.LargeScreenshotImage3 = large_screenshot_image3;
        }
    }
}
