using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Model
{
    public class PodcastEpisode
    {
        public int PodcastEpisodeID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; }
        public string MeetingPlace { get; set; }
        public string Note { get; set; }
        public int CaseOfficerId { get; set; }
        public string CaseOfficerName { get; set; }

        public PodcastEpisode() { }

        public PodcastEpisode(int podcastEpisodeID, string title, DateTime date, TimeSpan duration, string status, string meetingPlace, string note)
        {
            PodcastEpisodeID = podcastEpisodeID;
            Title = title;
            Date = date;
            Duration = duration;
            Status = status;
            MeetingPlace = meetingPlace;
            Note = note;
        }
    }
}
