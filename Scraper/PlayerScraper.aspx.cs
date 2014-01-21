using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FantasyFootball.Model;
using FantasyFootball;

public partial class PlayerScraper : System.Web.UI.Page
{
    private ModelController ModelController;

    protected void Page_Load(object sender, EventArgs e)
    {
        ModelController = new ModelController();
        
        string url = "http://fantasy.premierleague.com/stats/elements/?page=";

        for (int i = 1; i < 21; i++)
        {
            // Display results to a webpage
            var webpage = GetWebPage(url + i.ToString());

            var table = webpage.Split(new string[] {"Teams selected by %"}, StringSplitOptions.None);

            var tbody = table[2].Split(new string[] {"tbody"}, StringSplitOptions.None);
            var trs = tbody[1].Split(new string[] {"<tr>"}, StringSplitOptions.None);

            foreach (var tr in trs)
            {
                var tds = tr.Split(new string[] {"<td>", "</td>"}, StringSplitOptions.None);
                if (tds.Length > 10)
                {
                    var team = Team.GetTeam(ModelController.Teams, tds[7]);
                    var Player = new Player(tds[5], team);
                    Player.PriceString = tds[13];
                    Player.Position = tds[9];

                    if (tds[3].Contains("info.png"))
                    {
                        Player.Availability = 100;
                    }
                    else if (tds[3].Contains("infoposs_25.png"))
                    {
                        Player.Availability = 25;
                    }
                    else if (tds[3].Contains("infoposs_50.png"))
                    {
                        Player.Availability = 50;
                    }
                    else if (tds[3].Contains("infoposs_75.png"))
                    {
                        Player.Availability = 75;
                    }
                    else if (tds[3].Contains("infowarn.png"))
                    {
                        Player.Availability = 0;
                    }

                    team.Players.Add(Player);
                    ModelController.Players.Add(Player);
                }
            }
        }

        ModelController.CreatePlayerFile();
    }

    private string GetWebPage(string url)
    {
        string strResult = "";
        WebResponse objResponse;
        WebRequest objRequest = System.Net.HttpWebRequest.Create(url);

        objResponse = objRequest.GetResponse();

        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
        {
            strResult = sr.ReadToEnd();
            // Close and clean up the StreamReader
            sr.Close();
        }

        // Display results to a webpage
        return strResult;
    }
}