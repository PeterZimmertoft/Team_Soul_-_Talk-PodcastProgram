using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class PodcastEpisode : Guest
    {
        public int PodcastEpisodeID {  get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public string MeetingPlace { get; set; }
        public string Note { get; set; }
        public int CaseOfficerId { get; set; }
        public string CaseOfficerName { get; set; }

        public PodcastEpisode() : base() { }

        public PodcastEpisode(int podcastEpisodeID, string title, DateTime date, int duration, string status, string meetingPlace, string note)
        {
            this.PodcastEpisodeID = podcastEpisodeID;
            this.Title = title;
            this.Date = date;
            this.Duration = duration;
            this.Status = status;
            this.MeetingPlace = meetingPlace;
            this.Note = note;
        }
    }
}