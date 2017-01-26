using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using EloBuddy;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Warwirk.zBeta
{
    class CheckVersion
    {
        public static readonly string LocalVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string GitHubVersion;

        public static void CheckUpdate()
        {
            WebRequest Request_Ver = WebRequest.Create("https://github.com/InvitingEnemy/ScriptEloBuddy");
            Request_Ver.Credentials = CredentialCache.DefaultCredentials;
            WebResponse Response_Ver = Request_Ver.GetResponse();
            Stream Stream_Ver = Response_Ver.GetResponseStream();
            StreamReader Reader_Ver = new StreamReader(Stream_Ver);
            GitHubVersion = Reader_Ver.ReadToEnd();
            Reader_Ver.Close();
            Response_Ver.Close();

            GitHubVersion = Regex.Split(Regex.Split(GitHubVersion, "type-text\">")[1], "</table>")[0];
            GitHubVersion = Regex.Replace(GitHubVersion, @"[<][a-z|A-Z|/](.|)*?[>]", "");

            GitHubVersion = GitHubVersion.Trim().Replace("\t", "").Replace("\r", "").Replace("\n", "");
            GitHubVersion = (new Regex(" +")).Replace(GitHubVersion, " ");

            string[] WordList = { "," };
            string[] NoticeList = GitHubVersion.Split(WordList, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("Local Version : " + LocalVersion + "  /  GitHub Version : " + NoticeList[0]);

        }
    }
}
