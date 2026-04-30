using Soul_Talk.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface IPodcastEpisodeRepository
    {
        List<PodcastEpisode> GetAllPodcastEpisodes();
        PodcastEpisode GetPodcastEpisodeById(int id);
        int AddPodcastEpisode(PodcastEpisode episode);
        void UpdatePodcastEpisode(PodcastEpisode episode);
        void DeletePodcastEpisode(int id);
        int AddGuestToPodcastEpisode(int episodeId, int guestId);
    }
}
