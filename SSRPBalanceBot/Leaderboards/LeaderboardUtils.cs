using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SSRPBalanceBot.Leaderboards
{
    class LeaderboardUtils
    {
        public static List<Boards> boards = SSRPItems.FillList<Boards>("Leaderboards/boards.json");

        public async static Task<string> GetPage(string url)
        {
            using (WebClient wc = new WebClient())
            {
                return await wc.DownloadStringTaskAsync(url);
            }
        }

        public static async Task<GangInfo> GetGangInfo(string gangName)
        {
            using (WebClient wc = new WebClient())
            {
                string page = await wc.DownloadStringTaskAsync(new Uri($"https://zarpgaming.com/index.php/leaderboards/darkrp/gangs?search={gangName}"));

                Regex regex = new Regex("specialrow\">(?s)(.*)<\\/tr>|krow(?s)(.*)<\\/tr>");
                Regex infoX = new Regex("(?<=<td class=\"kcol-mid\">)(.*)(?=<\\/td>)");
                Regex gangIcon = new Regex("(?<=\" src=\")(.*?)(?=\")");
                Regex gangOwner = new Regex("(?<=rel=\"nofollow\">)(.*)(?=<\\/a>)");

                string gangRow = regex.Match(page).ToString();

                //mc[0] = gang icon
                //mc[1] = gang name
                //mc[2] = gang owner
                //mc[3] = member count
                //mc[4] = gang cash
                //mc[5] = gang loot

                MatchCollection mc = infoX.Matches(gangRow);

                string gIcon = gangIcon.Match(mc[0].ToString()).ToString();
                string gName = mc[1].ToString();

                string gOwner;
                if (mc[2].ToString().Contains("nofollow"))
                {
                    gOwner = gangOwner.Match(mc[2].ToString()).ToString();
                }
                else
                {
                    gOwner = mc[2].ToString();
                }

                string memberCount = mc[3].ToString();
                string gangCash = mc[4].ToString();
                string gangLoot = mc[5].ToString();

                GangInfo gInfo = new GangInfo { gangIcon = gIcon, gangName = gName, gangCash = gangCash, gangLoot = gangLoot, memberCount = memberCount, gangOwner = gOwner };

                return gInfo;
            }

        }

        public static async Task<string> GetBoard(string board, string player)
        {
            foreach(Boards b in boards)
            {
                foreach(string alias in b.aliases)
                {
                    if(alias == board)
                    {
                        return await GetPage(b.url + $"?search={player}");
                    }
                }
            }
            return null;
        }

        public static MatchCollection GetBoardRows(string boardData)
        {
            Regex row = new Regex("specialrow\">(?s)(.*?)<\\/tr>|krow(?s)(.*?)<\\/tr>");
            MatchCollection rows = row.Matches(boardData);

            return rows;
        } 

        public static List<BoardInfo> GetList(string boardData, string player)
        {
            List<BoardInfo> bInfo = new List<BoardInfo> { };
            if(player == null)
            {
                int count = 0;
                foreach (Match m in GetBoardRows(boardData))
                {
                    count++;
                    if (count > 3) { break; }

                    Regex pos = new Regex("(?<=<td class=\"kcol-first\">)(.*)(?=<\\/td>)");
                    Regex infoX = new Regex("(?<=<td class=\"kcol-mid\">)(.*)(?=<\\/td>)");
                    Regex name = new Regex("(?<=rel=\"nofollow\">)(.*)(?=<\\/a>)");

                    bInfo.Add(new BoardInfo
                    {
                        position = pos.Match(m.ToString()).ToString(),
                        name = name.Match(infoX.Matches(m.ToString())[1].ToString()).ToString(),
                        stat = infoX.Matches(m.ToString())[2].ToString()
                    });
                }
            }
            else
            {
                Regex pos = new Regex("(?<=<td class=\"kcol-first\">)(.*)(?=<\\/td>)");
                Regex infoX = new Regex("(?<=<td class=\"kcol-mid\">)(.*)(?=<\\/td>)");
                Regex name = new Regex("(?<=rel=\"nofollow\">)(.*)(?=<\\/a>)");

                Match m = GetBoardRows(boardData)[0];

                bInfo.Add(new BoardInfo
                {
                    position = pos.Match(m.ToString()).ToString(),
                    name = name.Match(infoX.Matches(m.ToString())[1].ToString()).ToString(),
                    stat = infoX.Matches(m.ToString())[2].ToString()
                });
            }

            return bInfo;
        }

        public class GangInfo
        {
            public string gangIcon { get; set; }
            public string gangName { get; set; }
            public string gangOwner { get; set; }
            public string memberCount { get; set; }
            public string gangCash { get; set; }
            public string gangLoot { get; set; }
        }

        public class BoardInfo
        {
            public string position { get; set; }
            public string name { get; set; }
            public string stat { get; set; }
        }

        public class Boards
        {
            public string name { get; set; }
            public string url { get; set; }
            public string[] aliases { get; set; }
        }

    }
}
