using Soul_Talk.Model;
using System.Collections.Generic;

namespace Soul_Talk.Persistence__Repositories_
{
    public interface IPodcastEpisodeRepository
    {
        List<PodcastEpisode> GetAll();
        PodcastEpisode GetById(int id);

        int Add(PodcastEpisode model);
        void Update(PodcastEpisode model);
        void Delete(int id);

        int AddGuestToPodcastEpisode(int podcastEpisodeId, int guestId);
        List<Guest> GetGuestsForPodcastEpisode(int podcastEpisodeId);
    }
}
