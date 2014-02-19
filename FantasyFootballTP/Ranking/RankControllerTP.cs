using System;
using System.Collections.Generic;
using System.Linq;
using FantasyFootball;
using FantasyFootball.Model;
using FantasyFootball.Model.Interfaces;

namespace FantasyFootballTP.Ranking
{
    public class RankControllerTP : RankControllerBase
    {
        public List<ITeamRank> TeamRanks { get; set; }

        private double teamGKSDHome;
        public double TeamGKSDHome
        {
            get { return teamGKSDHome; }
            set { teamGKSDHome = value; }
        }

        private double teamDEFSDHome;
        public double TeamDEFSDHome
        {
            get { return teamDEFSDHome; }
            set { teamDEFSDHome = value; }
        }

        private double teamMIDSDHome;
        public double TeamMIDSDHome
        {
            get { return teamMIDSDHome; }
            set { teamMIDSDHome = value; }
        }

        private double teamFWDSDHome;
        public double TeamFWDSDHome
        {
            get { return teamFWDSDHome; }
            set { teamFWDSDHome = value; }
        }

        private double teamGKSDAway;
        public double TeamGKSDAway
        {
            get { return teamGKSDAway; }
            set { teamGKSDAway = value; }
        }

        private double teamDEFSDAway;
        public double TeamDEFSDAway
        {
            get { return teamDEFSDAway; }
            set { teamDEFSDAway = value; }
        }

        private double teamMIDSDAway;
        public double TeamMIDSDAway
        {
            get { return teamMIDSDAway; }
            set { teamMIDSDAway = value; }
        }

        private double teamFWDSDAway;
        public double TeamFWDSDAway
        {
            get { return teamFWDSDAway; }
            set { teamFWDSDAway = value; }
        }

        private double teamGKAVGHome;
        public double TeamGKAVGHome
        {
            get { return teamGKAVGHome; }
            set { teamGKAVGHome = value; }
        }

        private double teamDEFAVGHome;
        public double TeamDEFAVGHome
        {
            get { return teamDEFAVGHome; }
            set { teamDEFAVGHome = value; }
        }

        private double teamMIDAVGHome;
        public double TeamMIDAVGHome
        {
            get { return teamMIDAVGHome; }
            set { teamMIDAVGHome = value; }
        }

        private double teamFWDAVGHome;
        public double TeamFWDAVGHome
        {
            get { return teamFWDAVGHome; }
            set { teamFWDAVGHome = value; }
        }

        private double teamGKAVGAway;
        public double TeamGKAVGAway
        {
            get { return teamGKAVGAway; }
            set { teamGKAVGAway = value; }
        }

        private double teamDEFAVGAway;
        public double TeamDEFAVGAway
        {
            get { return teamDEFAVGAway; }
            set { teamDEFAVGAway = value; }
        }

        private double teamMIDAVGAway;
        public double TeamMIDAVGAway
        {
            get { return teamMIDAVGAway; }
            set { teamMIDAVGAway = value; }
        }

        private double teamFWDAVGAway;
        public double TeamFWDAVGAway
        {
            get { return teamFWDAVGAway; }
            set { teamFWDAVGAway = value; }
        }

        public override List<ITeamRank> RankTeams(List<Team> Teams, int NoFormWeeks, int currentGameweek, int Gameweeks, MatchDetailName matchDetailName)
        {
            TeamRanks = new List<ITeamRank>();

            Teams.ForEach(x =>
                {
                    var tr = new TeamRankTP(x);
                    TeamRanks.Add(tr);
                    tr.SetPoints(currentGameweek, NoFormWeeks);
                });

            teamGKSDHome = CalculateStdDev(TeamRanks.Select(x => ((TeamRankTP)x).AvghomeGKPPoints));
            teamDEFSDHome = CalculateStdDev(TeamRanks.Select(x => ((TeamRankTP)x).AvgHomeDefPoints));
            teamMIDSDHome = CalculateStdDev(TeamRanks.Select(x => ((TeamRankTP)x).AvgHomeMidPoints));
            teamFWDSDHome = CalculateStdDev(TeamRanks.Select(x => ((TeamRankTP)x).AvgHomeFWDPoints));
            teamGKSDAway = CalculateStdDev(TeamRanks.Select(x => ((TeamRankTP)x).AvgAwayGKPPoints));
            teamDEFSDAway = CalculateStdDev(TeamRanks.Select(x => ((TeamRankTP)x).AvgAwayDefPoints));
            teamMIDSDAway = CalculateStdDev(TeamRanks.Select(x => ((TeamRankTP)x).AvgAwayMidPoints));
            TeamFWDSDAway = CalculateStdDev(TeamRanks.Select(x => ((TeamRankTP)x).AvgAwayFWDPoints));

            teamGKAVGAway = TeamRanks.Sum(x => ((TeamRankTP)x).AvghomeGKPPoints) / TeamRanks.Count;
            teamDEFAVGAway = TeamRanks.Sum(x => ((TeamRankTP)x).AvgHomeDefPoints) / TeamRanks.Count;
            teamMIDAVGAway = TeamRanks.Sum(x => ((TeamRankTP)x).AvgHomeMidPoints) / TeamRanks.Count;
            TeamFWDAVGAway = TeamRanks.Sum(x => ((TeamRankTP)x).AvgHomeFWDPoints) / TeamRanks.Count;
            teamGKAVGHome = TeamRanks.Sum(x => ((TeamRankTP)x).AvgAwayGKPPoints) / TeamRanks.Count;
            teamDEFAVGHome = TeamRanks.Sum(x => ((TeamRankTP)x).AvgAwayDefPoints) / TeamRanks.Count;
            teamMIDAVGHome = TeamRanks.Sum(x => ((TeamRankTP)x).AvgAwayMidPoints) / TeamRanks.Count;
            teamFWDAVGHome = TeamRanks.Sum(x => ((TeamRankTP)x).AvgAwayFWDPoints) / TeamRanks.Count;

            return TeamRanks.OrderByDescending(x => ((TeamRankTP)x).AvgHomeFWDPoints + ((TeamRankTP)x).AvgAwayFWDPoints).ToList();
        }

        public override List<IPlayerRank> RankPlayers(List<Player> players, int NoFormWeeks, int currentGameweek,
                                                          int Gameweeks, string Position)
        {
            var rankedPlayers = new List<IPlayerRank>();

            players.Where(x => x.Position != null && x.Position.Contains(Position) && x.Availability >= 0).ToList().ForEach(x =>
            {
                var tr = new PlayerRankTP(x);
                rankedPlayers.Add(tr);
                tr.SetPoints(currentGameweek, NoFormWeeks, Gameweeks);
                tr.Predict(currentGameweek, Gameweeks, this);
            });

            return rankedPlayers.OrderByDescending(x => x.FuturePoints).ToList();
        }
    }
}