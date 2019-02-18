using DevExpress.Web.Mvc;
using ProjectAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectAnalysis.Controllers
{
    public class ManageController : Controller
    {
         ProjectAnalysis.Models.DataModelContainer db = new ProjectAnalysis.Models.DataModelContainer();
        // GET: Manage
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Loginin(string account,string password)
        //{
        //    var ishave = db.T_CustomerUser.Where(m => m.CustomerUserName == account && m.UserPwd == password).FirstOrDefault();
        //    if (ishave != null)
        //    {
        //        Session["CustomerID"] = ishave.CustomerID;//市场id
        //        return RedirectToAction("Main", "Home");

        //    }
        //    else if (account == "admin" && password == "123456")
        //    {
        //        Session["CustomerID"] = 0;
        //        return RedirectToAction("Main", "Home");
        //    }

        //    else
        //    {

        //        return Content("<script>alert('用户名或密码错误');history.go(-1);</script>");
        //    }
        //    //return View();
        //}

        #region 市场管理
        /// <summary>
        /// 市场列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Customer()
        {
            return View();
        }

       

        [ValidateInput(false)]
        public ActionResult CustomerGridViewPartial()
        {
            var cid = Convert.ToInt32(Session["CustomerID"]);
            List<T_Customer> model = new List<T_Customer>();

            if (cid == 0)
            {
                //ViewData["Customer"] = db.T_Customer.ToList();
                model = db.T_Customer.ToList();
            }
            else
                model = db.T_Customer.Where(m => m.CustomerID == cid).ToList();
                 ViewData["Customer"] = db.T_Customer.Where(m=>m.CustomerID==cid).ToList();
            return PartialView("_CustomerGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CustomerGridViewPartialAddNew(ProjectAnalysis.Models.T_Customer item)
        {
            List<T_Customer> model = new List<T_Customer>();
            var cid = Convert.ToInt32(Session["CustomerID"]);
            if (cid == 0)
            {
                model = db.T_Customer.ToList();
            }
            else
            { 
                model = db.T_Customer.Where(m => m.CustomerID == cid).ToList();
            
                }
            if (ModelState.IsValid)
            {
                try
                {
                    ObjectParameter output = new ObjectParameter("CurrentSequenceStr", typeof(string));
                  db.p_sys_GenSequence("T_Customer_CustomerID", output);
                    var result = output.Value;
                    item.CustomerID =long.Parse(result.ToString());//主键
                    item.CustomerSysID = "sysDyl" + DateTime.Today.ToString("yyyyMMdd");
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "请检查录入数据是否正确.";
            return PartialView("_CustomerGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CustomerGridViewPartialUpdate(ProjectAnalysis.Models.T_Customer item)
        {
            List<T_Customer> model = new List<T_Customer>();
            var cid = Convert.ToInt32(Session["CustomerID"]);
            if (cid == 0)
            {
                model = db.T_Customer.ToList();
            }
            else
            {
                model = db.T_Customer.Where(m => m.CustomerID == cid).ToList();

            }
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.CustomerID == item.CustomerID);
                   

                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_CustomerGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CustomerGridViewPartialDelete(System.Int64 CustomerID)
        {
            List<T_Customer> model = new List<T_Customer>();
            var cid = Convert.ToInt32(Session["CustomerID"]);
            if (cid == 0)
            {
                model = db.T_Customer.ToList();
            }
            else
            {
                model = db.T_Customer.Where(m => m.CustomerID == cid).ToList();

            }
            if (CustomerID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.CustomerID == CustomerID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_CustomerGridViewPartial", model.ToList());
        }
        #endregion

        #region 市场部门管理
         

        //市场部门列表
        public ActionResult CustomerDepartment()
        {
            var cid = Convert.ToInt32(Session["CustomerID"]);
            if (cid == 0)
            {
                ViewData["Customer"] = db.T_Customer.ToList();
            }
            return View();
        }



        [ValidateInput(false)]
        public ActionResult TreeListPartial()
        {
              var cid = Convert.ToInt32(Session["CustomerID"]);
            List<T_CustomerDepartment> model = new List<T_CustomerDepartment>();
            if (cid==0)
            {
               // ViewData["Customer"] = db.T_Customer.ToList();
                model = db.T_CustomerDepartment.ToList();
            }
            else
             model = db.T_CustomerDepartment.Where(m=>m.CustomerID==cid).ToList();
           // ViewData["CustomerDepName"] = model;
            return PartialView("_TreeListPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialAddNew(ProjectAnalysis.Models.T_CustomerDepartment item,string  ParentDeps)
        {
            //var model = db.T_CustomerDepartment;
            List<T_CustomerDepartment> model = new List<T_CustomerDepartment>();
             var cid = Convert.ToInt32(Session["CustomerID"]);
            if (cid == 0)
            {
                model = db.T_CustomerDepartment.ToList();
            }
            else
            {
                model = db.T_CustomerDepartment.Where(m=>m.CustomerID==cid).ToList();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    ObjectParameter output = new ObjectParameter("CurrentSequenceStr", typeof(string));
                   
                    string[] str = ParentDeps.Split(',');
                    foreach (var p in str)
                    {
                        db.p_sys_GenSequence("T_CustomerDepartment_CustomerDepID", output);
                        var result = output.Value;
                        item.CustomerDepID = long.Parse(result.ToString());//主键
                         item.ParentDep =long.Parse(p);
                        if (cid != 0)
                        {
                           
                        
                         item.CustomerID =long.Parse(cid.ToString());
                        }
                    
                        db.T_CustomerDepartment.Add(item);
                        db.SaveChanges();
                    }
                  //
                  
                    //item.CustomerID = cid;
                   
                    return Content("<script>alert('保存成功');location.href='/Manage/CustomerDepartment';</script>");
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    return Content("<script>alert('"+e.Message+"');history.go(-1);</script>");
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialUpdate(ProjectAnalysis.Models.T_CustomerDepartment item)
        {
            List<T_CustomerDepartment> model = new List<T_CustomerDepartment>();
            var cid = Convert.ToInt32(Session["CustomerID"]);
            if (cid == 0)
            {
                model = db.T_CustomerDepartment.ToList();
            }
            else
            {
                model = db.T_CustomerDepartment.Where(m => m.CustomerID == cid).ToList();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.CustomerDepID == item.CustomerDepID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_TreeListPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialDelete(System.Int64 CustomerDepID)
        {
            List<T_CustomerDepartment> model = new List<T_CustomerDepartment>();
            var cid = Convert.ToInt32(Session["CustomerID"]);
            if (cid == 0)
            {
                model = db.T_CustomerDepartment.ToList();
            }
            else
            {
                model = db.T_CustomerDepartment.Where(m => m.CustomerID == cid).ToList();
            }
            if (CustomerDepID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.CustomerDepID == CustomerDepID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_TreeListPartial", model.ToList());
        }
        #endregion

        #region 市场用户管理
        public ActionResult CustomerUser()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult CustomerUserGridView1Partial()
        {
            var cid = Convert.ToInt32(Session["CustomerID"]);
             List<T_CustomerUser> model=new List<T_CustomerUser>();
            if (cid == 0)
            {
                model = db.T_CustomerUser.ToList();
                 ViewData["Customer"] = db.T_Customer.ToList();
                ViewData["T_CustomerDepartment"] = db.T_CustomerDepartment.ToList();
               
            }
            else
            {
                model = db.T_CustomerUser.Where(m => m.CustomerID == cid).ToList(); ;
                 ViewData["Customer"] = db.T_Customer.Where(m=>m.CustomerID==cid).ToList();
                ViewData["T_CustomerDepartment"] = db.T_CustomerDepartment.Where(m=>m.CustomerID==cid).ToList();
                
            }
             ViewData["T_CustomerUserType"] = db.T_CustomerUserType.ToList();
           
            return PartialView("_CustomerUserGridView1Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CustomerUserGridView1PartialAddNew(ProjectAnalysis.Models.T_CustomerUser item)
        {
            var model = db.T_CustomerUser;
            if (ModelState.IsValid)
            {
                try
                {
                    ObjectParameter output = new ObjectParameter("CurrentSequenceStr", typeof(string));
                    db.p_sys_GenSequence("T_CustomerUser_CustomerUserID", output);
                    var result = output.Value;
                    item.CustomerUserID = long.Parse(result.ToString());
                    item.CustomerUserBH=result.ToString();
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_CustomerUserGridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CustomerUserGridView1PartialUpdate(ProjectAnalysis.Models.T_CustomerUser item)
        {
            var model = db.T_CustomerUser;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.CustomerUserID == item.CustomerUserID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_CustomerUserGridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CustomerUserGridView1PartialDelete(System.Int64 CustomerUserID)
        {
            var model = db.T_CustomerUser;
            if (CustomerUserID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.CustomerUserID == CustomerUserID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_CustomerUserGridView1Partial", model.ToList());
        }
        #endregion
        #region 数据管理
        public ActionResult DataManage()
        {
            return View();
        }
        
        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var cid = Convert.ToInt32(Session["CustomerID"]);
            List<T_ProductTradeDaySummary> model = new List<T_ProductTradeDaySummary>();
            if (cid == 0)
            {
                ViewData["T_Product"] = db.T_Product.ToList();
                ViewData["T_Customer"] = db.T_Customer.ToList();
                 model = db.T_ProductTradeDaySummary.ToList();
            }
            else
            {
                ViewData["T_Product"] = db.T_Product.Where(m=>m.CustomerID==cid).ToList();
                ViewData["T_Customer"] = db.T_Customer.Where(m=>m.CustomerID==cid).ToList();
                model = db.T_ProductTradeDaySummary.Where(m=>m.CustomerID==cid).ToList();
            }
            return PartialView("_GridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(ProjectAnalysis.Models.T_ProductTradeDaySummary item)
        {
            var cid = Convert.ToInt32(Session["CustomerID"]);
            List<T_ProductTradeDaySummary> model;
            NewMethod(cid, out model);
            if (ModelState.IsValid)
            {
                try
                {
                    ObjectParameter output = new ObjectParameter("CurrentSequenceStr", typeof(string));
                    db.p_sys_GenSequence("T_ProductTradeDaySummary_TradeID", output);
                    var result = output.Value;
                    item.TradeID =long.Parse(result.ToString());
                    item.TradeMonth = item.TradeDate.Month;
                    item.TradeYear = item.TradeDate.Year;
                    item.TradeYearMon = item.TradeDate.ToString("yyyy-MM");
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(ProjectAnalysis.Models.T_ProductTradeDaySummary item)
        {
            // var model = db.T_ProductTradeDaySummary;
            int cid = Convert.ToInt32(Session["CustomerID"]);
            List<T_ProductTradeDaySummary> model;
            NewMethod(cid, out model);
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.TradeID == item.TradeID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            
            return PartialView("_GridViewPartial", model.ToList());
        }

        private void NewMethod(int cid, out List<T_ProductTradeDaySummary> model)
        {
            
            ViewData["ProductCategory"] = db.T_ProductCategory.ToList();
            model = new List<Models.T_ProductTradeDaySummary>();
            if (cid == 0)
            {
                ViewData["T_Product"] = db.T_Product.ToList();
                ViewData["T_Customer"] = db.T_Customer.ToList();
                model = db.T_ProductTradeDaySummary.ToList();
            }
            else
            {
                ViewData["T_Product"] = db.T_Product.Where(m => m.CustomerID == cid).ToList();
                ViewData["T_Customer"] = db.T_Customer.Where(m => m.CustomerID == cid).ToList();
                model = db.T_ProductTradeDaySummary.Where(m => m.CustomerID == cid).ToList();
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int64 TradeID)
        {
            var cid = Convert.ToInt32(Session["CustomerID"]);
            List<T_ProductTradeDaySummary> model;
            NewMethod(cid, out model);
            if (TradeID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.TradeID == TradeID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", model.ToList());
        }
        #endregion

        #region 职位管理
        public ActionResult CustomerUserType()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult UserTypeGridViewPartial()
        {
            var model = db.T_CustomerUserType;
            return PartialView("_UserTypeGridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UserTypeGridViewPartialAddNew(ProjectAnalysis.Models.T_CustomerUserType item)
        {
            var model = db.T_CustomerUserType;
            if (ModelState.IsValid)
            {
                try
                {

                    ObjectParameter output = new ObjectParameter("CurrentSequenceStr", typeof(string));
                    db.p_sys_GenSequence("T_CustomerUserType_UserTypeID", output);
                    var result = output.Value;
                    item.UserTypeID =Convert.ToInt32(result);
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_UserTypeGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UserTypeGridViewPartialUpdate(ProjectAnalysis.Models.T_CustomerUserType item)
        {
            var model = db.T_CustomerUserType;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.UserTypeID == item.UserTypeID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_UserTypeGridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UserTypeGridViewPartialDelete(System.Int32 UserTypeID)
        {
            var model = db.T_CustomerUserType;
            if (UserTypeID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.UserTypeID == UserTypeID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_UserTypeGridViewPartial", model.ToList());
        }
        #endregion
        #region 商品管理
        public ActionResult T_Product()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult GridView2Partial()
        {
             var cid = Convert.ToInt32(Session["CustomerID"]);
            List<T_Product> model = new List<Models.T_Product>();
             ViewData["ProductCategory"] = db.T_ProductCategory.ToList();
            if (cid == 0)
            {
                 ViewData["Customer"] = db.T_Customer.ToList();
                model = db.T_Product.ToList();
            }
            else
               
                 ViewData["Customer"] = db.T_Customer.Where(m=>m.CustomerID==cid).ToList();
                model = db.T_Product.Where(m => m.CustomerID == cid).ToList();
            
            return PartialView("_GridView2Partial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialAddNew(ProjectAnalysis.Models.T_Product item)
        {
            var cid = Convert.ToInt32(Session["CustomerID"]);
            ViewData["ProductCategory"] = db.T_ProductCategory.ToList();
            List<T_Product> model = new List<Models.T_Product>();
            if (cid == 0)
            {
                ViewData["Customer"] = db.T_Customer.ToList();
                var smodel = db.T_Product.ToList();
            }
            else
            {
                ViewData["Customer"] = db.T_Customer.Where(m => m.CustomerID == cid).ToList();
                model = db.T_Product.Where(m => m.CustomerID == cid).ToList();
            }
            if (ModelState.IsValid)
            {
                try
                {
                     ObjectParameter output = new ObjectParameter("CurrentSequenceStr", typeof(string));
                    db.p_sys_GenSequence("T_Product_ProductID", output);
                    var result = output.Value;
                    item.ProductID = long.Parse(result.ToString());
                    item.ProductAliasName = item.ProductName;
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView2Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialUpdate(ProjectAnalysis.Models.T_Product item)
        {
            var cid = Convert.ToInt32(Session["CustomerID"]);
            ViewData["ProductCategory"] = db.T_ProductCategory.ToList();
            List<T_Product> model = new List<Models.T_Product>();
            if (cid == 0)
            {
                 ViewData["Customer"] = db.T_Customer.ToList();
                var smodel = db.T_Product.ToList();
            }
            else
            {
                 ViewData["Customer"] = db.T_Customer.Where(m=>m.CustomerID==cid).ToList();
                 model = db.T_Product.Where(m=>m.CustomerID==cid).ToList();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.ProductID == item.ProductID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridView2Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView2PartialDelete(System.Int64 ProductID)
        {
            var cid = Convert.ToInt32(Session["CustomerID"]);
            ViewData["ProductCategory"] = db.T_ProductCategory.ToList();
            List<T_Product> model = new List<Models.T_Product>();
            if (cid == 0)
            {
                ViewData["Customer"] = db.T_Customer.ToList();
                var smodel = db.T_Product.ToList();
            }
            else
            {
                ViewData["Customer"] = db.T_Customer.Where(m => m.CustomerID == cid).ToList();
                model = db.T_Product.Where(m => m.CustomerID == cid).ToList();
            }
            if (ProductID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.ProductID == ProductID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridView2Partial", model.ToList());
        }
        #endregion

        //商品类别管理
        public ActionResult ProductCategory()
        {
            return View();
        }
        
        [ValidateInput(false)]
        public ActionResult PcategoryTreeListPartial()
        {
            var model = db.T_ProductCategory;
            return PartialView("_PcategoryTreeListPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PcategoryTreeListPartialAddNew(string PdCategoryName,string  partid)
        {
            var model = db.T_ProductCategory;
            if (ModelState.IsValid)
            {
                try
                {
                    ObjectParameter output = new ObjectParameter("CurrentSequenceStr", typeof(string));

                    if (partid != null&&partid!="")
                    {

                        string[] st = partid.Split(',');
                        foreach (var p in st)
                        {
                            if (p!="")
                            {
                                ProjectAnalysis.Models.T_ProductCategory item = new T_ProductCategory();
                                db.p_sys_GenSequence("T_ProductCategory_pdCatetoryID", output);
                                var result1 = output.Value;
                                item.pdCatetoryID = long.Parse(result1.ToString());
                                item.ParentCatgoryID = long.Parse(p);
                                model.Add(item);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        ProjectAnalysis.Models.T_ProductCategory item = new T_ProductCategory();
                        db.p_sys_GenSequence("T_ProductCategory_pdCatetoryID", output);
                        var result = output.Value;
                        item.pdCatetoryID = long.Parse(result.ToString());
                        item.ParentCatgoryID = 0;
                        model.Add(item);
                        db.SaveChanges();
                    }
                    return Content("<script>alert('录入成功');location.href='/Manage/ProductCategory'</script>");
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    return Content("<script>alert('"+e.Message+"');location.href='/Manage/ProductCategory'</script>");
                }
            }
            else
            {
                //ViewData["EditError"] = "Please, correct all errors.";
                return Content("<script>alert('Please, correct all errors.');location.href='/Manage/ProductCategory'</script>");
            }
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PcategoryTreeListPartialUpdate(ProjectAnalysis.Models.T_ProductCategory item)
        {
            var model = db.T_ProductCategory;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.pdCatetoryID == item.pdCatetoryID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_PcategoryTreeListPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PcategoryTreeListPartialDelete(System.Int64 pdCatetoryID)
        {
            var model = db.T_ProductCategory;
            if (pdCatetoryID >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.pdCatetoryID == pdCatetoryID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_PcategoryTreeListPartial", model.ToList());
        }
    }
}
