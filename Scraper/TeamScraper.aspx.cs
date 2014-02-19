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

public partial class TeamScraper : System.Web.UI.Page
{
    private ModelController ModelController;

    protected void Page_Load(object sender, EventArgs e)
    {
        ModelController = new ModelController();

        string url = "http://fantasy.premierleague.com/transfers/";

            // Display results to a webpage
            var webpage = GetWebPage(url);

            var table = webpage.Split(new string[] {"Teams selected by %"}, StringSplitOptions.None);
            
        

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