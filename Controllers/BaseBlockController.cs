using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using CMS.Models;

namespace CMS.Controllers {

    public class BaseBlockController<T> : BaseController where T : Block, new() {

        [ChildActionOnly]
        public PartialViewResult View(int ID) {
            ViewBag.model = db.Block.Find(ID);
            return PartialView();
        }

        [Oturum]
        [ValidateInput(false)]
        public ActionResult Add(int SectionID) {
            var model = new T();
            ViewBag.model = new T() {
                SectionId = SectionID,
                Width = 12
            };
            return View("Edit");
        }

        [Oturum]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(T model) {

            //hata
            if (!ModelState.IsValid) {
                ViewBag.model = model;
                return View("Edit");
            }

            //resim yükle
            foreach (string name in Request.Files.Keys) {
                var type = model.GetType();
                var prop = type.GetProperty(name);
                if (prop.PropertyType == typeof(string)) {
                    prop.SetValue(model, Util.resimYukle(name, (string)prop.GetValue(model)));
                }
            }

            //kaydet
            model.No = db.Block.Any() ? db.Block.Max(i => i.No) + 1 : 1;
            db.Insert(model);

            //başarılı
            var section = db.Section.Find(model.SectionId);
            return RedirectToAction("Section", "Admin", new {
                PageID = section.PageId
            });

        }

        [Oturum]
        [ValidateInput(false)]
        public ActionResult Edit(int ID) {
            ViewBag.model = db.Block.Find(ID);
            return View();
        }

        [Oturum]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(T model) {

            //hata
            if (!ModelState.IsValid) {
                ViewBag.model = model;
                return View();
            }


            //resim yükle

            //model tipi ne
            var type = model.GetType();

            //formdan gelen dosyaları şey yap
            foreach (string name in Request.Files.Keys) {

                //file input isminde bir property var mı ki ne?
                var prop = type.GetProperty(name);

                //var sa kesin stringdir yoksa sie
                if (prop.PropertyType != typeof(string)) {
                    continue;
                }

                //dosyayı al
                var value = "";
                var httpFile = Request.Files[name];

                //dosya yoksa eski dosyayı ata
                if (httpFile == null || httpFile.ContentLength == 0) {

                    //şuanki kayıt
                    var oldModel = db.Block.Find(model.Id);

                    //modelin database ile olan ilişkisini kes yoksa hata verecek
                    var entry = db.Entry(oldModel);
                    entry.State = EntityState.Detached;

                    //eski resmi oku
                    value = (string)prop.GetValue(oldModel);
                }

                //yeni resmi oku
                else {
                    value = (string)prop.GetValue(model);
                }

                //okunan resmi modele yaz
                prop.SetValue(model, Util.resimYukle(name, value));

            }

            //kaydet
            db.Update(model, "No");

            //başarılı
            var section = db.Section.Find(model.SectionId);
            return RedirectToAction("Section", "Admin", new {
                PageID = section.PageId
            });

        }
    }

}
