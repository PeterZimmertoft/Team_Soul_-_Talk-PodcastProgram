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
        private string _status { get; set; }
        private string _meetingPlace { get; set; }
        private string _note { get; set; }

        public PodcastEpisode(int podcastEpisodeID, string title, DateTime date, int duration, string status, string meetingPlace, string note)
        {
            this.PodcastEpisodeID = podcastEpisodeID;
            this.Title = title;
            this.Date = date;
            this.Duration = duration;
            this._status = status;
            this._meetingPlace = meetingPlace;
            this._note = note;
        }
    }
}