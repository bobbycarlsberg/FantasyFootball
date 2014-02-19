using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootball.Model
{
    public class FFTeam
    {
        public FFTeam()
        {
            squad = new ObservableCollection<IPlayerRank>();
            team = new List<IPlayerRank>();
            substitutes = new List<IPlayerRank>();
            goalies = new List<IPlayerRank>();
            defenders = new List<IPlayerRank>();
            midfielders = new List<IPlayerRank>();
            forwards = new List<IPlayerRank>();

            squad.CollectionChanged += squad_CollectionChanged;
        }

        void squad_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var z in e.NewItems)
                {
                    var playerRank = (IPlayerRank)z;
                    if (playerRank != null)
                    {
                        var gCount = team.Count(x => x.Player.IsGoalKeeper);
                        var dCount = team.Count(x => x.Player.IsDefender);
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
                        else if (playerRank.Player.IsDefender)
                        {
                            if (defenders.Count < 5)
                            {
                                defenders.Add(playerRank);

                                if (team.Count < 11 - spaceNeeded)
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefender), playerRank);
                                else if (dCount < 3)
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefender), playerRank);
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
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefender) + team.Count(x => x.Player.IsMidfield), playerRank);
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
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefender) + team.Count(x => x.Player.IsMidfield) + team.Count(x => x.Player.IsAttack), playerRank);
                                else if (fCount == 0)
                                    team.Insert(team.Count(x => x.Player.IsGoalKeeper) + team.Count(x => x.Player.IsDefender) + team.Count(x => x.Player.IsMidfield) + team.Count(x => x.Player.IsAttack), playerRank);
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
                    var playerRank = (IPlayerRank)x;

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

            if (squad.Count == 1)
            {
                captain = squad[0];
            }
            else if (squad.Count == 2)
            {
                viceCaptain = squad[1];
            }

            Cost = squad.Sum(x => x.Player.Price);
        }

        private ObservableCollection<IPlayerRank> squad;
        public ObservableCollection<IPlayerRank> Squad
        {
            get { return squad; }
            set { squad = value; }
        }

        private List<IPlayerRank> team;
        public List<IPlayerRank> Team
        {
            get { return team; }
            set { team = value; }
        }

        private List<IPlayerRank> substitutes;
        public List<IPlayerRank> Substitutes
        {
            get { return substitutes; }
            set { substitutes = value; }
        }

        public List<IPlayerRank> goalies;
        public List<IPlayerRank> defenders;
        public List<IPlayerRank> midfielders;
        public List<IPlayerRank> forwards;

        public IPlayerRank captain;
        public IPlayerRank viceCaptain;

        private double cost;
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }
    }
}
