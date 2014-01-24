using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Ranking;

namespace FantasyFootball.Model
{
    public class FFTeam
    {
        public FFTeam()
        {
            squad = new ObservableCollection<PlayerRank>();
            team = new List<PlayerRank>();
            substitutes = new List<PlayerRank>();
            goalies = new List<PlayerRank>();
            defenders = new List<PlayerRank>();
            midfielders = new List<PlayerRank>();
            forwards = new List<PlayerRank>();

            squad.CollectionChanged += squad_CollectionChanged;
        }

        void squad_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var z in e.NewItems)
                {
                    var playerRank = (PlayerRank)z;
                    if (playerRank != null)
                    {
                        var gCount = team.Count(x => x.Player.IsGoalKeeper);
                        var dCount = team.Count(x => x.Player.IsDefensive);
                        var mCount = team.Count(x => x.Player.IsMidfield);
                        var fCount = team.Count(x => x.Player.IsAttack);

                        var spaceNeeded = 5 - gCount - (fCount > 0 ? 1 : 0) - (dCount > 2 ? 3 : dCount);

                        if (playerRank.Player.IsGoalKeeper)
                        {
                            if (goalies.Count < 2)
                            {
                                goalies.Add(playerRank);
                                if (team.Count < 11 && gCount == 0)
                                    team.Insert(0, playerRank);
                                else
                                    substitutes.Add(playerRank);
                            }
                        }
                        else if (playerRank.Player.IsDefensive)
                        {
                            if (defenders.Count < 5)
                            {
                                defenders.Add(playerRank);

                                if (team.Count < 11 - spaceNeeded)
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefensive), playerRank);
                                else if (dCount < 3)
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefensive), playerRank);
                                else
                                    substitutes.Add(playerRank);
                            }
                        }
                        else if (playerRank.Player.IsMidfield)
                        {
                            if (midfielders.Count < 5)
                            {
                                midfielders.Add(playerRank);
                                if (team.Count < 11 - spaceNeeded)
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefensive) + team.Count(x => x.Player.IsMidfield), playerRank);
                                else
                                    substitutes.Add(playerRank);
                            }
                        }
                        else if (playerRank.Player.IsAttack)
                        {
                            if (forwards.Count < 3)
                            {
                                forwards.Add(playerRank);
                                if (team.Count < 11 - spaceNeeded)
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefensive) + team.Count(x => x.Player.IsMidfield) + team.Count(x => x.Player.IsAttack), playerRank);
                                else if (fCount == 0)
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefensive) + team.Count(x => x.Player.IsMidfield) + team.Count(x => x.Player.IsAttack), playerRank);
                                else
                                    substitutes.Add(playerRank);
                            }
                        }
                    }
                    if (!goalies.Contains(playerRank) && !defenders.Contains(playerRank) &&
                        !midfielders.Contains(playerRank) && !forwards.Contains(playerRank))
                        squad.Remove(playerRank);
                }
            }
            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var x in e.OldItems)
                {
                    var playerRank = (PlayerRank)x;

                    if (playerRank != null)
                    {
                        if (team.Contains(playerRank))
                            team.Remove(playerRank);
                        else if (substitutes.Contains(playerRank))
                            substitutes.Remove(playerRank);

                        if (goalies.Contains(playerRank))
                            goalies.Remove(playerRank);
                        else if (defenders.Contains(playerRank))
                            defenders.Remove(playerRank);
                        else if (midfielders.Contains(playerRank))
                            midfielders.Remove(playerRank);
                        else if (forwards.Contains(playerRank))
                            forwards.Remove(playerRank);
                    }
                }
            }

            Cost = squad.Sum(x => x.Player.Price);
        }

        private ObservableCollection<PlayerRank> squad;
        public ObservableCollection<PlayerRank> Squad
        {
            get { return squad; }
            set { squad = value; }
        }

        private List<PlayerRank> team;
        public List<PlayerRank> Team
        {
            get { return team; }
            set { team = value; }
        }

        private List<PlayerRank> substitutes;
        public List<PlayerRank> Substitutes
        {
            get { return substitutes; }
            set { substitutes = value; }
        }

        public List<PlayerRank> goalies;
        public List<PlayerRank> defenders;
        public List<PlayerRank> midfielders;
        public List<PlayerRank> forwards;

        private double cost;
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }
    }
}
