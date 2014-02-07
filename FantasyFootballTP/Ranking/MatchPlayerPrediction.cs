using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;

namespace FantasyFootballTP.Ranking
{
    public class MatchPlayerPrediction
    {
        public MatchPlayerPrediction(Fixture fixture, PlayerRank playerRank, RankController rankController)
        {
            Fixture = fixture;
            Player = playerRank.Player;

            var teamRank = rankController.TeamRanks.FirstOrDefault(x => x.Team == (fixture.HomeTeam == player.Team ? fixture.AwayTeam : fixture.HomeTeam));
            if (playerRank.WillPlay > 0.35)
            {
                if (fixture.AwayTeam == teamRank.Team)
                {
                    /*
                     the deviation is the opposition teams average points against them from the standard deviation of all teams. 
                     extra points are a proportion of that deviation from the standard deviation. multiplied by the sd of the players form.
                     */

                    if (player.Position == "GKP")
                    {
                        var deviation = teamRank.AvgAwayGKPPoints - rankController.TeamGKAVGAway;
                        var extrapoints = (deviation / rankController.TeamGKSDAway) * playerRank.HomePointsSD;
                        Points = RemoveNegatives(extrapoints) + playerRank.HomePointsPerGame;
                    }
                    else if (player.Position == "DEF")
                    {
                        var deviation = teamRank.AvgAwayDefPoints - rankController.TeamDEFAVGAway;
                        var extrapoints = (deviation / rankController.TeamDEFSDAway) * playerRank.HomePointsSD;
                        Points = RemoveNegatives(extrapoints + playerRank.HomePointsPerGame);
                    }
                    else if (player.Position == "MID")
                    {
                        var deviation = teamRank.AvgAwayMidPoints - rankController.TeamMIDAVGAway;
                        var extrapoints = (deviation / rankController.TeamMIDSDAway) * playerRank.HomePointsSD;
                        Points = RemoveNegatives(extrapoints + playerRank.HomePointsPerGame);
                    }
                    else if (player.Position == "FWD")
                    {
                        var deviation = teamRank.AvgAwayFWDPoints - rankController.TeamFWDAVGAway;
                        var extrapoints = (deviation / rankController.TeamFWDSDAway) * playerRank.HomePointsSD;
                        Points = RemoveNegatives(extrapoints + playerRank.HomePointsPerGame);
                    }
                }
                else
                {
                    if (player.Position == "GKP")
                    {
                        var deviation = teamRank.AvghomeGKPPoints - rankController.TeamGKAVGHome;
                        var extrapoints = (deviation / rankController.TeamGKSDHome) * playerRank.AwayPointsSD;
                        Points = RemoveNegatives(extrapoints + playerRank.AwayPointsPerGame);
                    }
                    else if (player.Position == "DEF")
                    {
                        var deviation = teamRank.AvgHomeDefPoints - rankController.TeamDEFAVGHome;
                        var extrapoints = (deviation / rankController.TeamDEFSDHome) * playerRank.AwayPointsSD;
                        Points = RemoveNegatives(extrapoints + playerRank.AwayPointsPerGame);
                    }
                    else if (player.Position == "MID")
                    {
                        var deviation = teamRank.AvgHomeMidPoints - rankController.TeamMIDAVGHome;
                        var extrapoints = (deviation / rankController.TeamMIDSDHome) * playerRank.AwayPointsSD;
                        Points = RemoveNegatives(extrapoints + playerRank.AwayPointsPerGame);
                    }
                    else if (player.Position == "FWD")
                    {
                        var deviation = teamRank.AvgHomeFWDPoints - rankController.TeamFWDAVGHome;
                        var extrapoints = (deviation / rankController.TeamFWDSDHome) * playerRank.AwayPointsSD;
                        Points = RemoveNegatives(extrapoints + playerRank.AwayPointsPerGame);
                    }
                }
            }
        }

        private Fixture fixture;
        public Fixture Fixture
        {
            get { return fixture; }
            set { fixture = value; }
        }

        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        private double points;
        public double Points
        {
            get { return points; }
            set { points = value; }
        }

        private double RemoveNegatives(double d)
        {
            if (d < 0)
                return 0;
            else
                return d;
        }
    }
}
