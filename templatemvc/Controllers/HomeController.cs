using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using templatemvc.dbfolder;
using templatemvc.Models;
using System.Web.Security;
using System.Web;

namespace templatemvc.Controllers
{
    public class HomeController : Controller
    {
        //login page
        [HttpGet]
        public ActionResult Index()
        {


            return View();
        }
        //login page
        [HttpPost]
        public ActionResult Index(usermodel umobj)
        {
            mvcdatabaseEntities1 eobj = new mvcdatabaseEntities1();
            var useres = eobj.logtables.Where(m => m.email == umobj.email).FirstOrDefault();
            if(useres==null)
            {
                TempData["invalid"] = "email not found";
            }
            else
            {
                if (useres.email==umobj.email&&useres.pass==umobj.pass) 
                {
                   FormsAuthentication.SetAuthCookie(useres.email, false);

                    Session["name"] = useres.name;
                    return RedirectToAction("dashboard");


                }
                else {
                    TempData["wr pass"] = "wrong email id or password";
                    return View();
                
                }
            }


            return View();
        }
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult dashboard()
        {


            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult showtable()
        {
            mvcdatabaseEntities1 eobj = new mvcdatabaseEntities1();
            var res=eobj.logtables.ToList();

            List<mymodelClass1> lobj = new List<mymodelClass1>();
            foreach (var item in res)
            {
                lobj.Add(new mymodelClass1
                {
                    id=item.id,
                    username = item.username,
                    email = item.email,
                    pass = item.pass,
                    name = item.name,

                });
            }
            return View(lobj);
        }[HttpGet]
        [Authorize]
        public ActionResult addata()
        {
           

            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult addata(mymodelClass1 mobj)
        {
            mvcdatabaseEntities1 eobj = new mvcdatabaseEntities1();
            logtable tobj = new logtable();
            tobj.id = mobj.id;
            tobj.email = mobj.email;
            tobj.name = mobj.name;
            tobj.username = mobj.username;
            tobj.pass = mobj.pass;
            eobj.logtables.Add(tobj);
            if(mobj.id==0)
            {
                eobj.logtables.Add(tobj);
                eobj.SaveChanges();

            }
            else
            {
                eobj.Entry(tobj).State = System.Data.Entity.EntityState.Modified;
                eobj.SaveChanges();
            }
           
            return RedirectToAction("showtable");
        }
        [Authorize]
        public ActionResult edit(int id)
        {
            mvcdatabaseEntities1 eobj = new mvcdatabaseEntities1();
            mymodelClass1 mobj = new mymodelClass1();          
            var edititem = eobj.logtables.Where(m => m.id == id).First();
            mobj.id = edititem.id;
            mobj.username = edititem.username;
            mobj.pass = edititem.pass;
            mobj.email = edititem.email;
            mobj.name = edititem.name;

            return View("addata",mobj);
        }
        [Authorize]
        public ActionResult delete(int id)
        {
            mvcdatabaseEntities1 eobj = new mvcdatabaseEntities1();
            var deleteitem = eobj.logtables.Where(m => m.id == id).First();
            eobj.logtables.Remove(deleteitem);
            eobj.SaveChanges();


            return RedirectToAction("showtable");
        }[Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}