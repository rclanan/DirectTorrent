using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using DirectTorrent.Data;
using AutoMapper;
using System.Net;
using System.IO;

namespace DirectTorrent.Domain.Models
{
    public enum State { OK, Disabled, DMCARemoved }
    public enum Quality { HD, FHD, ThreeD }
    public class Movie
    {
        #region Properties
        public int MovieID { get; set; }
        public State State { get; set; }
        public Uri MovieUrl { get; set; }
        public string MovieTitle { get; set; }
        public string MovieTitleClean { get; set; }
        public int MovieYear { get; set; }
        public string AgeRating { get; set; }
        public DateTime DateUploaded { get; set; }
        public int DateUploadedEpoch { get; set; }
        public Quality Quality { get; set; }
        public Image CoverImage { get; set; }
        public string ImdbCode { get; set; }
        public Uri ImdbLink { get; set; }
        public string Size { get; set; }
        public int SizeByte { get; set; }
        public float MovieRating { get; set; }
        public string Genre { get; set; }
        public string Uploader { get; set; }
        public int UploaderUID { get; set; }
        public int Downloaded { get; set; }
        public int TorrentSeeds { get; set; }
        public int TorrentPeers { get; set; }
        public Uri TorrentUrl { get; set; }
        public string TorrentHash { get; set; }
        public string TorrentMagnetUrl { get; set; }
        #endregion

        public Movie()
        {

        }

        public DirectTorrent.Domain.Models.Movie ParseDataModel()
        {
            Data.Models.Movie film = new Data.Models.Movie();
            var temp = film.PopulateModel();
            film = temp;
            Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
            Mapper.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            Mapper.CreateMap<string, Uri>().ConvertUsing(new UriTypeConverter());
            Mapper.CreateMap<string, Image>().ConvertUsing(new ImageTypeConverter());
            Mapper.CreateMap<string, Quality>().ConvertUsing(new QualityTypeConverter());
            Mapper.CreateMap<DirectTorrent.Data.Models.Movie, Movie>();
            Movie model = Mapper.Map<Movie>(film);
            return model;
        }

        #region TypeConverters
        public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(ResolutionContext context)
            {
                return System.Convert.ToDateTime(context.SourceValue);
            }
        }

        public class UriTypeConverter : ITypeConverter<string, Uri>
        {
            public Uri Convert(ResolutionContext context)
            {
                return new Uri((string)context.SourceValue);
            }
        }

        public class ImageTypeConverter : ITypeConverter<string, Image>
        {
            public Image Convert(ResolutionContext context)
            {
                Image tmpimg = null;
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create((string)context.SourceValue);
                HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebReponse.GetResponseStream();
                return Image.FromStream(stream);
            }
        }

        public class QualityTypeConverter : ITypeConverter<string, Quality>
        {
            public Quality Convert(ResolutionContext context)
            {
                if (context.SourceValue == "720p")
                    return Quality.HD;
                else if (context.SourceValue == "1080p")
                    return Quality.FHD;
                else
                    return Quality.ThreeD;
            }
        }
        #endregion
    }
}
