using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddAlert
{
    public class RaceEvent
    {
        public string ID;
        public DateTime Date;
        public string EventCode;
        public string StartTime_Au;
        public DateTime StartTime;
        public string Type;
        public string Venue;
        public string EventNo;
        public string Status;
        public string State;

        public string Name;
        public string Distance;
        public string Track;
        public string TrackRtg;
        public string Starters;
        public string Class;
        public string Prizemoney;
        public string WeatherTC;

        public string Rail;
        public string CodeAAP;
        public string CodeBF;

        public WeatherConditions WeatherCon;

        public List<Runner> Runners;
        public OddOverview OddView = null;
        public override string ToString()
        {
            return $"{StartTime_Au} {Type}{EventNo} {Venue}";
        }
    }

    public class Runner
    {
        public int Number;
        public string Name;
        public string Jockey;
        public string Trainer;
        public string Bar;
        public string Hcp;
        public string Scr;
        public string Emergency;
    }

    public class WeatherConditions
    {
        public string TempC;
        public string Humidity;
        public string WindSpeed;
        public string WindDir;
    }

    public class OddOverview
    {
        public string EveID; // 4349 -> <Event ID>
        public List<string> RNo; // Selection ID
        public List<string> RName; // Selection Name
        public List<OddOneBookie> Odds; // 1 OddOneBookie -> Bookie = "Beteasy" OddName = "Win" Odds <13>

        public decimal get_odd_for_bookie(string bookie, string runner)
        {
            var runner_idx = RName.IndexOf(runner);
            var search = Odds.Where(x => x.Bookie == bookie).ToList();
            if (search.Count > 0 && runner_idx >= 0)
                return search[0].Odds[runner_idx];
            return 0;
        }
    }

    public class OddOneBookie
    {
        public string Bookie;
        public string OddName;
        public List<decimal> Odds;
    }

    //thanks for your communication.it was nice talking to you.
    public enum Bookies
    {
        TAB_NSW = 0,
        BetEasy,
        Williamhill,
        Sportsbet,
        UBET,
        LuxBet,
        TopSport,
        Bet365,
        BestBookies,
        SportsBetting,
        UNIBET,
        NZ_TAB,
        TopBetta,
        ClassicBet,
    }
    public class OddBookieMap
    {
        // Colby wants monitor the following bookies
        // Sportsbet, ladbrokes, Sportsbetting, beteasy, bet365, best bookies, classicbet, topbetta, topsport
        // But the comparison will be done for
        // Sportsbet, Beteasy, Tab nsw
        public static Dictionary<string, string> map = new Dictionary<string, string>
        {
            //{"OddsSB", "Williamhill"},
            {"OddsSB2", "Sportsbet"},
            //{"OddsBF_B1", "Betfair(Back 1)"},
            //{"OddsBF_B2", "Betfair(Back 2)"},
            //{"OddsBF_B3", "Betfair(Back 3)"},
            //{"OddsBF_L1", "Betfair(Lay 1)"},
            //{"OddsBF_L2", "Betfair(Lay 2)"},
            //{"OddsBF_L3", "Betfair(Lay 3)"},
            //{"OddsBF_B1_p", "Betfair(Back 1p)"},
            //{"OddsBF_B2_p", "Betfair(Back 2p)"},
            //{"OddsBF_B3_p", "Betfair(Back 3p)"},
            //{"OddsBF_L1_p", "Betfair(Lay 1p)"},
            //{"OddsBF_L2_p", "Betfair(Lay 2p)"},
            //{"OddsBF_L3_p", "Betfair(Lay 3p)"},
            //{"OddsBF_WAP", "Betfair(WAP)"},
            //{"OddsLB", "LuxBet"},
            //{"OddsLB_p", "LuxBet (p)"},
            {"OddsLB2", "Ladbrokes"},
            {"OddsSB5", "SportsBetting"},
            //{"OddsQ", "UBET (Tote - Win)" },
            //{"OddsQ_p", "UBET (Tote - Place)" },
            //{"OddsQ_FX", "UBET" },
            //{"OddsQ_FX_p", "UBET(Fixed - Place)" },
            {"OddsN", "TAB NSW" },
            //{"OddsN_P", "TAB NSW(Place Fixed)" },
            {"OddsBE", "BetEasy" },
            //{"OddsBE_p", "BetEasy (Fixed - Place)" },
            {"OddsBT", "Bet365" },
            {"OddsBB2", "BestBookies" },
            {"OddsCB2", "ClassicBet" },
            {"OddsTB2", "TopBetta" },
            {"OddsTS2", "TopSport" },
            //{"OddsUB", "UNIBET" },
            //{"OddsNZ_FX", "NZ TAB" },
        };
    }
}
