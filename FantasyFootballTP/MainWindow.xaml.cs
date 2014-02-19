using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using FantasyFootball;
using FantasyFootball.Model;
using FantasyFootball.Model.Interfaces;
using FantasyFootballTP.Ranking;

namespace FantasyFootballTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ModelController ModelController;
        private RankControllerTP rankController;
        public RankControllerTP RankController
        {
            get { return rankController; }
            set
            {
                rankController = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("RankController"));
                }
            }
        }

        private double maxPrice = 100;
        public double MaxPrice
        {
            get { return maxPrice; }
            set
            {
                maxPrice = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("MaxPrice"));
                }
            }
        }

        private int currentGameWeek;
        public int CurrentGameWeek
        {
            get { return currentGameWeek; }
            set
            {
                currentGameWeek = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("CurrentGameWeek"));
                }
            }
        }

        private int formLength = 38;
        public int FormLength
        {
            get { return formLength; }
            set
            {
                formLength = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("FormLength"));
                }
            }
        }

        private int futureFixtures = 10;
        public int FutureFixtures
        {
            get { return futureFixtures; }
            set
            {
                futureFixtures = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("FutureFixtures"));
                }
            }
        }

        private List<ITeamRank> teamRanks;
        public List<ITeamRank> TeamRanks
        {
            get { return teamRanks; }
            set
            {
                teamRanks = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("TeamRanks"));
                }
            }
        }

        private List<IPlayerRank> playerRanks;
        public List<IPlayerRank> PlayerRanks
        {
            get { return playerRanks; }
            set
            {
                playerRanks = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("PlayerRanks"));
                }
            }
        }

        private IPlayerRank playerRank;
        public IPlayerRank PlayerRank
        {
            get { return playerRank; }
            set
            {
                playerRank = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("PlayerRank"));
                }
            }
        }

        private FFTeam squad;
        public FFTeam Squad
        {
            get { return squad; }
            set
            {
                squad = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Squad"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            ModelController = new ModelController();
            RankController = new RankControllerTP();
            ModelController.LoadPlayers();
            ModelController.LoadPlayerDetails();
            CurrentGameWeek = GameWeek.GetGameWeek(ModelController.GameWeeks, DateTime.Today).No;


            TeamRanks = RankController.RankTeams(ModelController.Teams, formLength, currentGameWeek, futureFixtures, MatchDetailName.TP).ToList();
            PlayerRanks = RankController.RankPlayers(ModelController.Players, formLength, currentGameWeek, futureFixtures, "").ToList();
        }

        private void Goalies_OnClick(object sender, RoutedEventArgs e)
        {
            PlayerRanks = RankController.RankPlayers(ModelController.Players, formLength, currentGameWeek, futureFixtures, "GKP").ToList();
        }

        private void Defenders_OnClick(object sender, RoutedEventArgs e)
        {
            PlayerRanks = RankController.RankPlayers(ModelController.Players, formLength, currentGameWeek, futureFixtures, "DEF").ToList();
        }

        private void mids_OnClick(object sender, RoutedEventArgs e)
        {
            PlayerRanks = RankController.RankPlayers(ModelController.Players, formLength, currentGameWeek, futureFixtures, "MID").ToList();
        }

        private void fwds_OnClick(object sender, RoutedEventArgs e)
        {
            PlayerRanks = RankController.RankPlayers(ModelController.Players, formLength, currentGameWeek, futureFixtures, "FWD").ToList();
        }

        private void teams_OnClick(object sender, RoutedEventArgs e)
        {
            TeamRanks = RankController.RankTeams(ModelController.Teams, formLength, currentGameWeek, futureFixtures, MatchDetailName.TP).ToList();
        }

        private void Price_OnClick(object sender, RoutedEventArgs e)
        {
            PlayerRanks = RankController.RankPlayers(ModelController.Players, formLength, currentGameWeek, futureFixtures, "").Where(x => x.Player.Price <= MaxPrice).OrderByDescending(x => x.FuturePoints).ToList();
        }

        private void TeamOfWeek_OnClick(object sender, RoutedEventArgs e)
        {
            if (maxPrice > 60)
            {
                Squad =
                    RankController.BudgetDreamTeam(ModelController.Players, formLength, currentGameWeek, futureFixtures,
                                                   MaxPrice);

                PlayerRanks = Squad.Team;
            }
        }
    }
}
