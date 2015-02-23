using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectTorrent.Logic.Models
{
    /// <summary>
    /// Represents the data associated to the upcoming movie.
    /// </summary>
    public class UpcomingMovie
    {
        /// <summary>
        /// Gets the title of movie.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Gets the release year of the movie.
        /// </summary>
        public int Year { get; private set; }
        /// <summary>
        /// Gets the imdb code of the movie.
        /// </summary>
        public string ImdbCode { get; private set; }
        public string MediumCoverImage { get; private set; }
        /// <summary>
        /// Gets the date when the movie was added.
        /// </summary>
        public string DateAdded { get; private set; }
        /// <summary>
        /// Gets the date when the movie was added as a unix timestamp.
        /// </summary>
        public int DateAddedUnix { get; private set; }

        public UpcomingMovie(DirectTorrent.Data.Yify.Models.UpcomingMovie source)
        {
            UpcomingMovie temp = null;
            AutoMapper.Mapper.CreateMap<Data.Yify.Models.UpcomingMovie, UpcomingMovie>();
            temp = AutoMapper.Mapper.Map<UpcomingMovie>(source);

            this.Title = temp.Title;
            this.Year = temp.Year;
            this.ImdbCode = temp.ImdbCode;
            this.MediumCoverImage = temp.MediumCoverImage;
            this.DateAdded = temp.DateAdded;
            this.DateAddedUnix = temp.DateAddedUnix;
        }

        private UpcomingMovie(){}
    }
}
