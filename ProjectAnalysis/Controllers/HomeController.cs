using DevExpress.XtraEditors.Filtering.Templates;
using ProjectAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectAnalysis.Controllers {
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Main()
        {
            return View();
        }
        public ActionResult PriceLine()
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;
            var day = "1";
            ViewData["ST"] = year + "/" + month + "/" + day;
            return View();
        }
       

        public ActionResult Line()
        {
            return View();
        }
        //月 交易量分析
        public ActionResult TransactionAnalysis()
        {
            NewMethod();
            return View();
        }

        private void NewMethod()
        {
            DataModelContainer db = new DataModelContainer();
            var minyear = db.T_ProductTradeDaySummary.Min(m => m.TradeYear);//获取数据表中最早的一年
            ViewBag.minyear = minyear;
        }

        //交易同比分析
        public ActionResult TransactionVolume()
        {
            NewMethod();
            return View();
        }

        //交易额占比
        public ActionResult TransactionVolumeRate()
        {
            NewMethod();
            return View();
        }
        /// <summary>
        /// 交易费分析
        /// </summary>
        /// <returns></returns>
        public ActionResult TransactionFee()
        {
           
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            ViewData["ST"] = d1.ToString("yyyy-MM-dd");
             ViewData["ET"] = d2.ToString("yyyy-MM-dd");
            Session["ST"] = d1.ToString("yyyy-MM-dd");
            Session["ET"] = d2.ToString("yyyy-MM-dd");
            return View();
        }

        //public int  Datashow(DateTime? stime,DateTime? etime,DateTime? datetime, DateTime? datetime2)
        //{
        //    if ((Convert.ToDateTime(Session["ST"].ToString()) == stime &&Convert.ToDateTime(Session["ET"].ToString()) == etime)&&(datetime==datetime))
        //    {
        //        return 1;
        //    }
        //    else
        //        Session["ST"] = stime;
        //    Session["ET"] = etime;
        //    return 0;
        //}

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Loginin(string account, string password)
        {
            ProjectAnalysis.Models.DataModelContainer db = new ProjectAnalysis.Models.DataModelContainer();
            var ishave = db.T_CustomerUser.Where(m => m.CustomerUserName == account && m.UserPwd == password).FirstOrDefault();
            if (ishave != null)
            {
                Session["CustomerID"] = ishave.CustomerID;//市场id
                return RedirectToAction("Main", "Home");

            }
            else if (account == "admin" && password == "123456")
            {
                Session["CustomerID"] = 0;
                return RedirectToAction("Main", "Home");
            }

            else
            {

                return Content("<script>alert('用户名或密码错误');history.go(-1);</script>");
            }
            //return View();
        }
    }
}