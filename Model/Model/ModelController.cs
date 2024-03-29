﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FantasyFootball.Model;

namespace FantasyFootball
{
    public class ModelController
    {
        public List<Team> Teams;
        public List<GameWeek> GameWeeks;
        public List<Player> Players;
        public List<Fixture> Matches;
        public List<Fixture> Fixtures;

        private string Folder = @"C:\Users\nicholas\Documents\GitHub\FantasyFootball\Model\Data\";

        public ModelController()
        {
            LoadTeams();
            LoadGameWeeks();
            LoadFixtures();
            Players = new List<Player>();
        }

        public void LoadTeams()
        {
            var file = new LinqToExcel.ExcelQueryFactory(Folder + @"Teams.xlsx");

            var query1 = from row in file.Worksheet("Sheet1")
                         let item = new Team()
                         {
                             LongName = row[0].Cast<string>(),
                             ShortName = row[1].Cast<string>(),
                             Abbreviation = row[2].Cast<string>()
                         }

                         select item;

            Teams = query1.ToList();
        }

        public void LoadGameWeeks()
        {
            var file = new LinqToExcel.ExcelQueryFactory(Folder + @"GameWeeks.xlsx");

            var query2 = from row in file.Worksheet("Sheet1")
                         let item = new GameWeek()
                         {
                             No = row[0].Cast<int>(),
                             StartDate = row[1].Cast<DateTime>(),
                             EndDate = row[2].Cast<DateTime>(),
                         }
                         select item;

            GameWeeks = query2.ToList();
        }

        public void LoadPlayers()
        {
            var di = new DirectoryInfo(Folder + @"Players\");
            var filename = di.GetFiles().OrderByDescending(x => x.Name).FirstOrDefault().FullName;

            var file = new LinqToExcel.ExcelQueryFactory(filename);

            var query2 = from row in file.Worksheet("DataTable")
                         let item = new Player(row[0].Cast<string>(), Team.GetTeam(Teams, row[3].Cast<string>()))
                         {
                             Position = row[2].Cast<string>(),
                             PriceString = row[1].Cast<string>(),
                             Price = double.Parse(row[1].Cast<string>().Substring(1)),
                             Availability = row[4].Cast<int>()
                         }
                         select item;

            Players = query2.ToList();
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].ID = i;
            }
        }

        public void LoadFixtures()
        {
            var file = new LinqToExcel.ExcelQueryFactory(Folder + @"Fixtures.xlsx");

            var query2 = from row in file.Worksheet("Sheet1")
                         let item = new Fixture()
                         {
                             HomeTeam = Team.GetTeam(Teams, row["HomeTeam"].Cast<string>()),
                             AwayTeam = Team.GetTeam(Teams, row["AwayTeam"].Cast<string>()),
                             Date = ParseDate(row["Date"].Cast<string>()),
                             GameWeek = GameWeek.GetGameWeek(GameWeeks, ParseDate(row["Date"].Cast<string>()))
                         }
                         select item;

            Fixtures = query2.ToList();

            Teams.ForEach(x => x.Fixtures = x.Fixtures.OrderByDescending(y => y.GameWeek.No).ToList());
        }

        public void LoadPlayerDetails()
        {
            var fixtures = new LinqToExcel.ExcelQueryFactory(Folder + @"MatchesPlayed.xlsx");

            var query =
                from row in fixtures.Worksheet("Sheet1")
                let item = new List<MatchDetail>
                    {
                        new MatchDetail((MatchDetailName)0, row[0].Cast<object>()),
                        new MatchDetail((MatchDetailName)1, row[1].Cast<object>()),
                        new MatchDetail((MatchDetailName)2, row[2].Cast<object>()),
                        new MatchDetail((MatchDetailName)3, row[3].Cast<object>()),
                        new MatchDetail((MatchDetailName)4, row[4].Cast<object>()),
                        new MatchDetail((MatchDetailName)5, row[5].Cast<object>()),
                        new MatchDetail((MatchDetailName)6, row[6].Cast<object>()),
                        new MatchDetail((MatchDetailName)7, row[7].Cast<object>()),
                        new MatchDetail((MatchDetailName)8, row[8].Cast<object>()),
                        new MatchDetail((MatchDetailName)9, row[9].Cast<object>()),
                        new MatchDetail((MatchDetailName)10, row[10].Cast<object>()),
                        new MatchDetail((MatchDetailName)11, row[11].Cast<object>()),
                        new MatchDetail((MatchDetailName)12, row[12].Cast<object>()),
                        new MatchDetail((MatchDetailName)13, row[13].Cast<object>()),
                        new MatchDetail((MatchDetailName)14, row[14].Cast<object>()),
                        new MatchDetail((MatchDetailName)15, row[15].Cast<object>())
                }
                select item;

            var list = query.ToList();

            Matches = new List<Fixture>();

            Fixture currentMatch = new Fixture();
            Team currentTeam = new Team();

            for (int i = 0; i < list.Count; i++)
            {
                var team = Team.GetTeam(Teams, list[i][0].Value.ToString());
                if (team != null)
                {
                    currentTeam = team;
                    if (currentMatch.HomeTeam == null)
                        currentMatch.HomeTeam = currentTeam;
                    else
                    {
                        currentMatch.AwayTeam = currentTeam;
                    }
                }
                else if (list[i][1].Value == null)
                {
                    if (list[i][0].Value.ToString() != "")
                    {
                        currentMatch = new Fixture();
                        var str = list[i][0].Value.ToString();
                        str = str.Substring(str.Length - 18, 13);
                        currentMatch.Date = ParseDate(str);
                        currentMatch.GameWeek =
                            GameWeeks.FirstOrDefault(
                                x => x.EndDate > currentMatch.Date && x.StartDate <= currentMatch.Date);
                        Matches.Add(currentMatch);
                    }
                }
                else if (list[i][0].Value.ToString() != "")
                {
                    var player = Players.FirstOrDefault(x => x.Name == list[i][0].Value.ToString() && x.Team == currentTeam);
                    if (player == null)
                    {
                        player = new Player(list[i][0].Value.ToString(), currentTeam);
                        Players.Add(player);
                        currentTeam.Players.Add(player);
                    }

                    var minutes = 0;
                    if (list[i][1].Value != null)
                        int.TryParse(list[i][1].Value.ToString(), out minutes);

                    if (minutes >= 0)
                    {
                        var mpd = new MatchPlayerDetails(player, currentMatch);

                        for (var j = 1; j < 16; j++)
                        {
                            mpd.MatchDetails.Add(list[i][j]);
                        }
                        player.MatchPlayerDetails.Add(mpd);
                    }
                }
            }

            Players.ForEach(x => x.MatchPlayerDetails = x.MatchPlayerDetails.OrderByDescending(y => y.Match.GameWeek.No).ToList());
        }

        private static DateTime ParseDate(string s)
        {
            try
            {
                DateTime result;
                result = DateTime.ParseExact(s.Trim(), "dd MMM H:mm", System.Globalization.CultureInfo.InvariantCulture);
                result = result.AddDays(1);

                if (result.Month > 6)
                    result = result.AddYears(-1);

                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CreatePlayerFile()
        {
            var query = from p in Players
                        let player = new
                            {
                                Name = p.Name,
                                Price = p.PriceString,
                                Position = p.Position,
                                Team = p.Team.LongName,
                                Availability = p.Availability
                            }
                        select player;

            var ds = new DataSet();
            ds.Tables.Add(ToDataTable(query));

            var fileName = @"Players\Players" + DateTime.Today.Year;
            var month = DateTime.Today.Month.ToString();
            if (month.Length == 1)
                month = "0" + month;
            var day = DateTime.Today.Day.ToString();
            if (day.Length == 1)
                day = "0" + day;

            fileName += month + day + ".xls";
            
            ExcelLibrary.DataSetHelper.CreateWorkbook(Folder + fileName, ds);
        }

        private static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            DataTable dt = new DataTable("DataTable");
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();

            //Inspect the properties and create the columns in the DataTable
            foreach (PropertyInfo pi in pia)
            {
                Type ColumnType = pi.PropertyType;
                if ((ColumnType.IsGenericType))
                {
                    ColumnType = ColumnType.GetGenericArguments()[0];
                }
                dt.Columns.Add(pi.Name, ColumnType);
            }

            //Populate the data table
            foreach (T item in collection)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();
                foreach (PropertyInfo pi in pia)
                {
                    if (pi.GetValue(item, null) != null)
                    {
                        dr[pi.Name] = pi.GetValue(item, null);
                    }
                }
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }

    public enum MatchDetailName
    {
        Name = 0,
        MP = 1,
        GS = 2,
        A = 3,
        CS = 4,
        GC = 5,
        OG = 6,
        PS = 7,
        PM = 8,
        YC = 9,
        RC = 10,
        S = 11,
        B = 12,
        ESP = 13,
        BPS = 14,
        TP = 15
    }
}
