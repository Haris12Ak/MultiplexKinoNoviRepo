using System.ComponentModel;

namespace MultiplexKino.Models.Film
{
    public class FilmPrikazVM
    {
        public int Id { get; set; }

        public string Naslov { get; set; }

        public int? Trajanje { get; set; }

        [DisplayName("Godina snimanja")]
        public int? GodinaSnimanja { get; set; }

        public string Zanr { get; set; }

        public string Reditelj { get; set; }

        public string Glumci { get; set; }

        [DisplayName("Sadržaj")]
        public string Sadrzaj { get; set; }

        [DisplayName("Video link")]
        public string VideoLink { get; set; }

        [DisplayName("Imdb link")]

        public string Plakat { get; set; }

        public string YouTubeId
        {
            get
            {
                string id = null;

                if (!string.IsNullOrEmpty(VideoLink) &&
                   (VideoLink.Contains("youtube.com") || VideoLink.Contains("youtu.be")))
                {
                    int lastIndexOf = VideoLink.LastIndexOf("v=");
                    if (lastIndexOf == 0)
                    {
                        lastIndexOf = VideoLink.LastIndexOf("/") + 1;
                    }
                    else
                    {
                        lastIndexOf += 2;
                    }

                    id = VideoLink.Substring(lastIndexOf);
                }

                return id;
            }
        }
    }
}
