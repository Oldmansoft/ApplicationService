using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oldmansoft.ApplicationService.WebApp.Areas.Sweepstake.Models
{
    public class ThemeAddModel
    {
        public string Name;

        public DateTime Start;

        public DateTime Finish;

        public int Floor;

        public int Ceiling;

        public List<int> Definition;
    }

    public class ThemePutModel
    {
        public string Name;

        public DateTime Start;

        public DateTime Finish;

        public int Floor;

        public int Ceiling;

        public List<int> Definition;
    }
}