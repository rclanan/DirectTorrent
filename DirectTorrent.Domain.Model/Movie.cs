using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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
        public uint SizeByte { get; set; }
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

        public static List<DirectTorrent.Domain.Models.Movie> ParseDataModel()
        {
            List<Data.Models.Movie> filmovi = Data.Models.Movie.PopulateModel();
            List<Movie> filmoviParsovani = new List<Movie>();
            Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
            Mapper.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            Mapper.CreateMap<string, Uri>().ConvertUsing(new UriTypeConverter());
            Mapper.CreateMap<string, Image>().ConvertUsing(new ImageTypeConverter());
            Mapper.CreateMap<string, Quality>().ConvertUsing(new QualityTypeConverter());
            Mapper.CreateMap<DirectTorrent.Data.Models.Movie, Movie>();
            filmovi.ForEach(x => filmoviParsovani.Add(Mapper.Map<Movie>(x)));
            return filmoviParsovani;
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
                return Image.FromStream(WebRequest.Create((string)context.SourceValue).GetResponse().GetResponseStream());
            }
        }

        public class QualityTypeConverter : ITypeConverter<string, Quality>
        {
            public Quality Convert(ResolutionContext context)
            {
                string kvalitet = context.SourceValue.ToString();
                if (kvalitet == "720p")
                    return Quality.HD;
                else if (kvalitet == "1080p")
                    return Quality.FHD;
                else if (kvalitet == "3D")
                    return Quality.ThreeD;
                else
                    throw new Exception();
            }
        }
        #endregion
    }
}
