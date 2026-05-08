using Soul_Talk.Model;
using System.Collections.Generic;

namespace Soul_Talk.Persistence__Repositories_
{
    // UC2: Planlæg podcast-episode.
    public interface IPodcastEpisodeRepository : IRepository<PodcastEpisode>
    {
        int AddGuestToPodcastEpisode(int podcastEpisodeId, int guestId);
        List<Guest> GetGuestsForPodcastEpisode(int podcastEpisodeId);
    }
}
