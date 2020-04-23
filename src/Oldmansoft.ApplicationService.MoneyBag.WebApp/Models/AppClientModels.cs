using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oldmansoft.ApplicationService.MoneyBag.WebApp.Models
{
    public class AppClientListModel
    {
        public Guid Id { get; set; }

        [Display(Name = "序号")]
        public Guid IdDisplay { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "注释")]
        public string Description { get; set; }

        [Display(Name = "创建时间")]
        public DateTime Created { get; set; }
    }

    public class AppClientCreateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "序号")]
        public Guid? IdDisplay { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "注释")]
        public string Description { get; set; }
    }

    public class AppClientEditModel
    {
        public Guid Id { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "注释")]
        public string Description { get; set; }
    }
}