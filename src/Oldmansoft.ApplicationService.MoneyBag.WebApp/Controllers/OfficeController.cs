using Oldmansoft.ClassicDomain;
using Oldmansoft.Html.Element;
using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oldmansoft.ApplicationService.MoneyBag.WebApp.Controllers
{
    public class OfficeController : Controller
    {
        public ActionResult Index()
        {
            var document = new ManageDocument(Url.Location(Manage));
            document.Logo = "钱包配置";
            document.Title = string.Format("{0} {1}", document.Logo, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
            document.Resources.AddScript(new ScriptResource(Url.Content("~/Scripts/oldmansoft-webman.cn.js")));
            document.Menu.Add(new TreeListItem(Url.Location(Manage)));
            return new HtmlResult(document);
        }

        [Location("应用配置", Icon = FontAwesome.Laptop)]
        public ActionResult Manage()
        {
            var table = DataTable.Define<Models.AppClientListModel>(o => o.Id).Create(Url.Location(new Func<DataTable.Request, JsonResult>(IndexSource)));
            table.AddActionTable(Url.Location(Create));
            table.AddActionItem(Url.Location(Edit));
            table.AddActionItem(Url.Location(Remove)).Confirm("是否删除");

            var panel = new Panel();
            panel.ConfigLocation();
            panel.Append(table);
            return new HtmlResult(panel.CreateGrid());
        }

        public JsonResult IndexSource(DataTable.Request request)
        {
            var totalCount = 0;
            var list = new List<Models.AppClientListModel>();
            foreach (var item in new Application.AppClient().Paging(request.PageIndex, request.PageSize, out totalCount))
            {
                var model = item.MapTo(new Models.AppClientListModel());
                model.IdDisplay = item.Id;
                list.Add(model);
            }
            return Json(DataTable.Source(list, request, totalCount));
        }

        [Location("创建")]
        public ActionResult Create()
        {
            var form = FormHorizontal.Create(new Models.AppClientCreateModel(), Url.Location(new Func<Models.AppClientCreateModel, JsonResult>(CreateResult)));

            var panel = new Panel();
            panel.ConfigLocation();
            panel.Append(form);
            return new HtmlResult(panel.CreateGrid());
        }

        public JsonResult CreateResult(Models.AppClientCreateModel model)
        {
            if (model.IdDisplay.HasValue)
            {
                new Application.AppClient().Create(model.IdDisplay.Value, model.Name, model.Description);
            }
            else
            {
                new Application.AppClient().Create(model.Name, model.Description);
            }

            return Json(DealResult.Refresh());
        }

        [Location("修改")]
        public ActionResult Edit(Guid selectedId)
        {
            var data = new Application.AppClient().Get(selectedId);
            if (data == null) return HttpNotFound();
            var form = FormHorizontal.Create(data.MapTo(new Models.AppClientEditModel()), Url.Location(new Func<Models.AppClientEditModel, JsonResult>(EditResult)));

            var panel = new Panel();
            panel.ConfigLocation();
            panel.Append(form);
            return new HtmlResult(panel.CreateGrid());
        }

        public JsonResult EditResult(Models.AppClientEditModel model)
        {
            new Application.AppClient().Change(model.Id, model.Name, model.Description);
            return Json(DealResult.Refresh());
        }

        [Location("移除")]
        public JsonResult Remove(Guid[] selectedId)
        {
            if (selectedId == null || selectedId.Length == 0) return Json(DealResult.Refresh());
            var appClients = new Application.AppClient();
            var actionResult = true;
            foreach (var item in selectedId)
            {
                if (!appClients.Remove(item))
                {
                    actionResult = false;
                }
            }
            if (actionResult)
            {
                return Json(DealResult.Refresh());
            }
            else
            {
                return Json(DealResult.WrongRefresh("有帐单在引用，不能删除。"));
            }
        }
    }
}
