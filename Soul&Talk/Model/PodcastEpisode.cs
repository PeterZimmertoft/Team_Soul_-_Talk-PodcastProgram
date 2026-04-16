using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class PodcastEpisode
    {
        public int PodcastEpisodeID {  get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        private string status { get; set; }
        private string meetingPlace { get; set; }
        private string note { get; set; }

    }
}
