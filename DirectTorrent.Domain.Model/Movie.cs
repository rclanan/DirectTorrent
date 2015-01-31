using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using AutoMapper;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace DirectTorrent.Domain.Models
{
    public enum State
    {
        OK,
        Disabled,
        DMCARemoved
    }

    public enum Quality
    {
        HD,
        FHD,
        ThreeD
    }

    public class Movie
    {
        #region Properties

        public ushort MovieID { get; private set; }
        public State State { get; private set; }
        public Uri MovieUrl { get; private set; }
        public string MovieTitle { get; private set; }
        public string MovieTitleClean { get; private set; }
        public ushort MovieYear { get; private set; }
        public string AgeRating { get; private set; }
        public DateTime DateUploaded { get; private set; }
        public uint DateUploadedEpoch { get; private set; }
        public Quality Quality { get; private set; }
        public Image CoverImage { get; private set; }
        public string ImdbCode { get; private set; }
        public Uri ImdbLink { get; private set; }
        public string Size { get; private set; }
        public uint SizeByte { get; private set; }
        public float MovieRating { get; private set; }
        public string Genre { get; private set; }
        public string Uploader { get; private set; }
        public uint UploaderUID { get; private set; }
        public uint Downloaded { get; private set; }
        public ushort TorrentSeeds { get; private set; }
        public ushort TorrentPeers { get; private set; }
        public Uri TorrentUrl { get; private set; }
        public string TorrentHash { get; private set; }
        public string TorrentMagnetUrl { get; private set; }

        #endregion

        public static List<DirectTorrent.Domain.Models.Movie> ParseDataModel()
        {
            //List<Data.Models.Movie> filmovi = Data.Models.Movie.PopulateModel();
            //List<Movie> filmoviParsovani = new List<Movie>();
            //Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
            //Mapper.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            //Mapper.CreateMap<string, Uri>().ConvertUsing(new UriTypeConverter());
            //Mapper.CreateMap<string, Image>().ConvertUsing(new ImageTypeConverter());
            //Mapper.CreateMap<string, Quality>().ConvertUsing(new QualityTypeConverter());
            //Mapper.CreateMap<DirectTorrent.Data.Models.Movie, Movie>();
            //filmovi.ForEach(x => filmoviParsovani.Add(Mapper.Map<Movie>(x)));
            //return filmoviParsovani;
            Stopwatch timer = Stopwatch.StartNew();
            Data.Models.MovieSet filmovi = new Data.Models.MovieSet(limit: 5);
            List<Movie> filmoviParsirani = new List<Movie>();
            Mapper.CreateMap<string, int>().ConvertUsing(Convert.ToInt32);
            Mapper.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            Mapper.CreateMap<string, Uri>().ConvertUsing(new UriTypeConverter());
            Mapper.CreateMap<string, Image>().ConvertUsing(new ImageTypeConverter());
            Mapper.CreateMap<string, Quality>().ConvertUsing(new QualityTypeConverter());
            Mapper.CreateMap<DirectTorrent.Data.Models.Movie, Domain.Models.Movie>();
            filmovi.MovieList.ForEach(x => filmoviParsirani.Add(Mapper.Map<Movie>(x)));

            timer.Stop();
            // TODO: 24 FUCKING SECONDS!!! GOTTTA SPEED THIS SHIT UP
            Debug.WriteLine(timer.Elapsed);
            return filmoviParsirani;
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
                return new Uri((string) context.SourceValue);
            }
        }

        public class ImageTypeConverter : ITypeConverter<string, Image>
        {
            public Image Convert(ResolutionContext context)
            {
                return
                    Image.FromStream(WebRequest.Create((string) context.SourceValue).GetResponse().GetResponseStream());
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