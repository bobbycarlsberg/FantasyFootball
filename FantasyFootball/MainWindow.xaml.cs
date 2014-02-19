using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FantasyFootball.Model;
using FantasyFootball;
using FantasyFootball.Model.Interfaces;
using FantasyFootball.Ranking;
using FantasyFootball.Ranking.MatchDetailsForm;

namespace FantasyFootball
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ModelController ModelController;

        private RankController rankController;
        public RankController RankController
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

        private MatchDetailName selectedAttribute = MatchDetailName.TP;
        public MatchDetailName SelectedAttribute
        {
            get { return selectedAttribute; }
            set
            {
                selectedAttribute = value;
                TeamRanks = RankController.RankTeams(ModelController.Teams, formLength, currentGameWeek, FutureFixtures,
                                         selectedAttribute);
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedAttribute"));
                }
            }
        }

        private MatchDetailForm selectedMatchDetailForm;
        public MatchDetailForm SelectedMatchDetailForm
        {
            get { return selectedMatchDetailForm; }
            set
            {
                selectedMatchDetailForm = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedMatchDetailForm"));
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

        private MatchPlayerPrediction selectedPrediction;
        public MatchPlayerPrediction SelectedPrediction
        {
            get { return selectedPrediction; }
            set
            {
                selectedPrediction = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedPrediction"));
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

        private TeamRank teamRank;
        public TeamRank TeamRank
        {
            get { return teamRank; }
            set
            {
                teamRank = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("TeamRank"));
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
            RankController = new RankController();
            ModelController.LoadPlayers();
            ModelController.LoadPlayerDetails();
            CurrentGameWeek = GameWeek.GetGameWeek(ModelController.GameWeeks, DateTime.Today).No;


            TeamRanks = RankController.RankTeams(ModelController.Teams, formLength, currentGameWeek, futureFixtures, selectedAttribute).ToList();
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
            TeamRanks = RankController.RankTeams(ModelController.Teams, formLength, currentGameWeek, futureFixtures, selectedAttribute).ToList();
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

            var team = new FFTeam();
           // team.Squad.Add(ModelController.Players.FirstOrDefault(x => x.Name == "Szezesny"));
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            TeamAttributeComboBox.ItemsSource = Enum.GetValues(typeof (MatchDetailName)).Cast<MatchDetailName>();
        }

        private void MyTeam_OnClick(object sender, RoutedEventArgs e)
        {
            List<Player> team = new List<Player>();


            team.Add(ModelController.Players.First(
                x => x.IsGoalKeeper && x.Team.LongName == "Arsenal" && x.Name == "Szczesny"));
            team.Add(ModelController.Players.First(
                x => x.IsGoalKeeper && x.Team.LongName == "Crystal Palace" && x.Name == "Speroni"));


            team.Add(ModelController.Players.First(
                x => x.IsDefender && x.Team.LongName == "Southampton" && x.Name == "Shaw"));
            team.Add(ModelController.Players.First(
                x => x.IsDefender && x.Team.LongName == "Southampton" && x.Name == "Fonte"));
            team.Add(ModelController.Players.First(
                x => x.IsDefender && x.Team.LongName == "Sunderland" && x.Name == "Bardsley"));
            team.Add(ModelController.Players.First(
                x => x.IsDefender && x.Team.LongName == "Everton" && x.Name == "Coleman"));
            team.Add(ModelController.Players.First(
                x => x.IsDefender && x.Team.LongName == "Chelsea" && x.Name == "Cahill"));


            team.Add(ModelController.Players.First(
                x => x.IsMidfield && x.Team.LongName == "Cardiff City" && x.Name == "Mutch"));
            team.Add(ModelController.Players.First(
                x => x.IsMidfield && x.Team.LongName == "Everton" && x.Name == "Mirallas"));
            team.Add(ModelController.Players.First(
                x => x.IsMidfield && x.Team.LongName == "Sunderland" && x.Name == "Johnson"));
            team.Add(ModelController.Players.First(
                x => x.IsMidfield && x.Team.LongName == "Manchester City" && x.Name == "Yaya Touré"));
            team.Add(ModelController.Players.First(
                x => x.IsMidfield && x.Team.LongName == "Chelsea" && x.Name == "Hazard"));


            team.Add(ModelController.Players.First(
                x => x.IsAttack && x.Team.LongName == "Liverpool" && x.Name == "Suárez"));
            team.Add(ModelController.Players.First(
                x => x.IsAttack && x.Team.LongName == "Liverpool" && x.Name == "Sturridge"));
            team.Add(ModelController.Players.First(
                x => x.IsAttack && x.Team.LongName == "Southampton" && x.Name == "Rodriguez"));


            

            //team.Add(ModelController.Players.First(
            //    x => x.IsGoalKeeper && x.Team.LongName == "Sunderland" && x.Name == "Mannone"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsGoalKeeper && x.Team.LongName == "Southampton" && x.Name == "Kelvin Davis"));


            //team.Add(ModelController.Players.First(
            //    x => x.IsDefender && x.Team.LongName == "Everton" && x.Name == "Baines"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsDefender && x.Team.LongName == "Norwich City" && x.Name == "Ryan Bennett"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsDefender && x.Team.LongName == "Hull City" && x.Name == "Faye"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsDefender && x.Team.LongName == "Everton" && x.Name == "Coleman"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsDefender && x.Team.LongName == "Chelsea" && x.Name == "Cahill"));


            //team.Add(ModelController.Players.First(
            //    x => x.IsMidfield && x.Team.LongName == "Cardiff City" && x.Name == "Mutch"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsMidfield && x.Team.LongName == "Southampton" && x.Name == "Lallana"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsMidfield && x.Team.LongName == "Manchester City" && x.Name == "Silva"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsMidfield && x.Team.LongName == "Manchester City" && x.Name == "Yaya Touré"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsMidfield && x.Team.LongName == "Liverpool" && x.Name == "Ibe"));


            //team.Add(ModelController.Players.First(
            //    x => x.IsAttack && x.Team.LongName == "Liverpool" && x.Name == "Suárez"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsAttack && x.Team.LongName == "Southampton" && x.Name == "Rodriguez"));
            //team.Add(ModelController.Players.First(
            //    x => x.IsAttack && x.Team.LongName == "Tottenham Hotspur" && x.Name == "Adebayor"));


            rankController.RankPositions(ModelController.Players, formLength, currentGameWeek, FutureFixtures, "");

            var ffteam = rankController.PickAndSuggest(team, formLength, currentGameWeek, 1.3);
            Squad = ffteam;
            PlayerRanks = Squad.Team;
        }
    }
}
