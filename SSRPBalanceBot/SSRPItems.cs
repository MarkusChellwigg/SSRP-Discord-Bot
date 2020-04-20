using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSRPBalanceBot
{
    class SSRPItems
    {
        static List<Item> itemList = new List<Item> { };
        static List<Printer> printerList = new List<Printer> { };
        public static async Task AddItems()
        {
            //Weapons Added Here
            itemList.Add(new Item { bind = "zarp_equipitem m9k_psg1", aliases = new string[] { "psg", "psg1", "psg-1", "m9k_psg1" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_thunderhammer", aliases = new string[] { "thunderhammer", "thunder hammer", "weapon_thunderhammer" } });
            itemList.Add(new Item { bind = "zarp_equipitem m9k_m202", aliases = new string[] { "m202", "m9k_m202" } });
            itemList.Add(new Item { bind = "zarp_equipitem m9k_milkormgl", aliases = new string[] { "milkor", "m9k_milkormgl", "milkormgl" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_hoff_thundergun", aliases = new string[] { "tgun", "thundergun", "weapon_hoff_thundergun"} });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_gluongun_golden", aliases = new string[] { "ggluon", "goldengluon", "goldgluon", "weapon_gluongun_golden", "golden gluon", "golden gluon gun" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_gluongun", aliases = new string[] { "gluon", "weapon_gluongun", "gluongun", "gluon gun" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_minigun2", aliases = new string[] { "mini2", "minigun2", "weapon_minigun2", "minigun 2.0" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_hand_cannon", aliases = new string[] { "hc", "handcannon", "weapon_hand_cannon", "hand cannon" } });
            itemList.Add(new Item { bind = "zarp_equipitem m9k_svu", aliases = new string[] { "svu"} });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_portableemp", aliases = new string[] { "portemp", "port emp", "portable emp", "gencorp emp" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_gaussrifle", aliases = new string[] { "gauss", "gaussrifle", "gauss rifle" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_gauss_rifle", aliases = new string[] { "gauss", "gaussrifle", "gauss rifle" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_gamma_rifle", aliases = new string[] { "gamma", "gammarifle", "gamma rifle" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_scanmaster5000", aliases = new string[] { "scanmaster", "scan master", "scan master 5000", "scanmaster5000" } });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_scanmaster5000_golden", aliases = new string[] { "gscan", "goldenscanmaster", "golden scan master", "golden scan master 5000", "goldenscanmaster5000", "goldscanmaster", "gold scan master", "gold scan master 5000", "goldscanmaster5000"} });
            itemList.Add(new Item { bind = "zarp_equipitem weapon_defib", aliases = new string[] { "defib", "defibrilator", "weapon_defib" } });
            itemList.Add(new Item { bind = "zarp_equipitem m9k_nerve_gas", aliases = new string[] { "nervegas", "nerve gas", "nerve_gas", "m9k_nerve_gas"} });
            itemList.Add(new Item { bind = "zarp_equipitem m9k_suicide_bomb", aliases = new string[] { "c4", "C4" } });


            //Suits Added Here
            itemList.Add(new Item { bind = "zarp_equipitem advanced nano suit", aliases = new string[] { "nano", "nano suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem advanced combat suit", aliases = new string[] { "acs", "advanced combat suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem gencorp juggernaut suit", aliases = new string[] { "jugg", "jugg suit", "jug suit", "juggernaut suit", "gencorp juggernaut suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem hyper suit", aliases = new string[] { "hyper", "hyper suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem evo suit", aliases = new string[] { "evo", "evo suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem mummy suit", aliases = new string[] { "mummy", "mummy suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem visor suit", aliases = new string[] { "visor", "visor suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem portal suit", aliases = new string[] { "portal", "portal suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem knight demon suit", aliases = new string[] { "demon", "demon suit", "knight demon", "knight demon suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem holo pilot suit", aliases = new string[] { "holo", "holo suit", "holo pilot", "holo pilot suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem jet suit", aliases = new string[] { "jet", "jet suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem golden juggernaut suit", aliases = new string[] { "gjugg", "gold jugg", "golden jugg", "golden juggernaut suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem golden edf", aliases = new string[] { "gedf", "gold edf", "golden edf" } });
            itemList.Add(new Item { bind = "zarp_equipitem golden nano suit", aliases = new string[] { "gnano", "gold nano", "gold nano suit", "golden nano suit" } });
            itemList.Add(new Item { bind = "zarp_equipitem golden advanced combat suit", aliases = new string[] { "gacs", "gold acs", "gold advanced combat suit", "golden acs", "golden advanced combat suit" } });

            //Printers Added Here
            printerList.Add(new Printer { printerName = "Topaz", perSecond = 5.00, aliases = new string[] { "topaz", "topaz printer", "topas"} });
            printerList.Add(new Printer { printerName = "Amethyst", perSecond = 7.50, aliases = new string[] { "amethyst", "amethyst printer"} });
            printerList.Add(new Printer { printerName = "Emerald", perSecond = 12.50, aliases = new string[] { "emerald", "emerald printer"} });
            printerList.Add(new Printer { printerName = "Sapphire", perSecond = 37.50, aliases = new string[] { "sapphire", "sapphire printer", "saphire", "saphire printer" } });
            printerList.Add(new Printer { printerName = "Money", perSecond = 10.00, aliases = new string[] { "money", "money printer", "normal", "normal printer" } });
            printerList.Add(new Printer { printerName = "Ruby", perSecond = 20.00, aliases = new string[] { "ruby", "ruby printer" } });
            printerList.Add(new Printer { printerName = "Gold", perSecond = 40.00, aliases = new string[] { "gold", "gold printer"} });
            printerList.Add(new Printer { printerName = "Nuclear", perSecond = 90.00, aliases = new string[] { "nuclear", "nuclear printer", "nuke", "nuke printer" } });
            printerList.Add(new Printer { printerName = "Diamond", perSecond = 200.00, aliases = new string[] { "diamond", "diamond printer"} });
            printerList.Add(new Printer { printerName = "Black Diamond", perSecond = 333.33, aliases = new string[] { "black diamond", "black diamond printer", "bdp" } });
            printerList.Add(new Printer { printerName = "Gencorp", perSecond = 500.00, aliases = new string[] { "gencorp", "gencorp printer", "gencorp orange core printer", "gencorp orange core" } });
            printerList.Add(new Printer { printerName = "Festive", perSecond = 500.00, aliases = new string[] { "festive", "festive printer"} });
            printerList.Add(new Printer { printerName = "Iridium", perSecond = 625.0, aliases = new string[] { "iridium", "iridium printer", "irid", "irid printer" } });
            printerList.Add(new Printer { printerName = "Magik", perSecond = 666.67, aliases = new string[] { "magik", "magik printer"} });
            printerList.Add(new Printer { printerName = "Golden Plated", perSecond = 750.00, aliases = new string[] { "gpp", "golden plated printer", "gold plated printer" } });
            printerList.Add(new Printer { printerName = "Ice", perSecond = 750.00, aliases = new string[] { "ice", "ice printer"} });
            printerList.Add(new Printer { printerName = "Skull", perSecond = 750.00, aliases = new string[] { "skull", "skull printer"} });
            printerList.Add(new Printer { printerName = "XMAS", perSecond = 770.00, aliases = new string[] { "xmas", "xmas printer", "christmas", "christmas printer" } });
            printerList.Add(new Printer { printerName = "Summer", perSecond = 800.00, aliases = new string[] { "summer", "summer printer"} });
            printerList.Add(new Printer { printerName = "Hell", perSecond = 802.50, aliases = new string[] { "hell", "hell printer" } });
            printerList.Add(new Printer { printerName = "Santa", perSecond = 700.00, aliases = new string[] { "santa", "santa printer"} });
            printerList.Add(new Printer { printerName = "Easter", perSecond = 775.00, aliases = new string[] { "easter", "easter printer"} });
        }


        public static async Task<string> GetBind(string input)
        {
            foreach (Item item in itemList)
            {
                foreach (string alias in item.aliases)
                {
                    if (input == alias)
                    {
                        return item.bind;
                    }
                }
            }
            return "Not Found";
        }

        public static async Task<Printer> GetPrinter(string input)
        {
            foreach (Printer printer in printerList)
            {
                foreach (string alias in printer.aliases)
                {
                    if (input.ToLower() == alias)
                    {
                        return printer;
                    }
                }
            }
            return null;
        }

        class Item
        {
            public string bind { get; set; }
            public string[] aliases { get; set; }
        }

        public class Printer
        {
            public string printerName { get; set; }
            public double perSecond { get; set; }
            public string[] aliases { get; set; }
        }
    }
}
