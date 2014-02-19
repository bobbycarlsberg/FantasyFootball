using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;
using FantasyFootball.Model.Interfaces;

namespace FantasyFootball.Model
{
    public interface IRankController
    {
        FFTeam DreamTeam(List<Player> players, int form, int gameweek, int predictions);

        FFTeam PickAndSuggest(List<Player> players, int form, int currentGameweek,
                              double budget);

        List<IPlayerRank> PlayerRanks { get; set; }

        List<IPlayerRank> goalies { get; }
        List<IPlayerRank> defenders { get; }
        List<IPlayerRank> midfielders { get; }
        List<IPlayerRank> forwards { get; }

        List<IPlayerRank> RankPlayers(List<Player> players, int NoFormWeeks, int currentGameweek,
                                      int Gameweeks, string Position);

        List<ITeamRank> RankTeams(List<Team> Teams, int NoFormWeeks, int currentGameweek, int Gameweeks,
                                 MatchDetailName matchDetailName);

        void RankPositions(List<Player> players, int NoFormWeeks, int currentGameweek,
                                      int Gameweeks, string Position);
    }
}