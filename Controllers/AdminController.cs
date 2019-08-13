using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers { 
    public class AdminController : BaseController {

        static string[] blocks = {
            "Title",
            "Gallery",
            "Image",
            "Article",
            "Service",
            "Form",
            "Text",
            "Html",
            "Slide"
        };

        //===============================================================
        //GİRİŞ ÇIKIŞ
        //===============================================================

        [Route("Admin")]
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        [Route("Admin")]
        public ActionResult Index(string AdminKullaniciAdi, string AdminSifre) {
            if (db.Setting.Any(i => i.AdminKullaniciAdi == AdminKullaniciAdi && i.AdminSifre == AdminSifre)) {
                Util.oturumAc(AdminKullaniciAdi);
                return RedirectToAction("Anasayfa");
            }
            ViewBag.hata = "geçersiz kullanıcı bilgisi";
            return View();
        }

        [Oturum]
        [Route("Admin/Cikis")]
        public ActionResult Cikis() {
            Util.oturumKapat();
            return RedirectToAction("Index", "Default");
        }

        //===============================================================
        //SİTE
        //===============================================================

        [Route("~/")]
        public ActionResult _Site() {

            var menus = (
                db.Menu
                .Include("MenuItem")
                .ToList()
            );

            foreach (var i in menus) {
                ViewData[i.Name] = i.MenuItem.OrderBy(j => j.No);
            }

            var page = db.Page.FirstOrDefault();
            ViewBag.page = page;
            ViewBag.sections = (
                page.Section
                .OrderBy(i => i.No)
                .ToList()
            );

            var settings = db.Setting.FirstOrDefault();
            ViewBag.title = page.Title ?? settings.Title;
            ViewBag.description = page.Description ?? settings.Description;
            ViewBag.keywords = page.Keywords ?? settings.Keywords;
            return View();
        }

        [Route("~/{title}-{id:int}")]
        public ActionResult _Site(string title, int id) {

            var menus = (
                db.Menu
                .Include("MenuItem")
                .ToList()
            );

            foreach (var i in menus) {
                ViewData[i.Name] = i.MenuItem.OrderBy(j => j.No);
            }

            var page = db.Page.Find(id);
            ViewBag.page = page;
            ViewBag.sections = (
                page.Section
                .OrderBy(i => i.No)
                .ToList()
            );

            var settings = db.Setting.FirstOrDefault();
            ViewBag.title = page.Title ?? settings.Title;
            ViewBag.description = page.Description ?? settings.Description;
            ViewBag.keywords = page.Keywords ?? settings.Keywords;
            return View();

        }

        //===============================================================
        //ANASAYFA
        //===============================================================

        [Oturum]
        [Route("Admin/Anasayfa")]
        public ActionResult Anasayfa() {
            return View();
        }

        //===============================================================
        //PAGE
        //===============================================================

        [ChildActionOnly]
        public ActionResult Pages() {
            return PartialView(
                db.Page.OrderBy(i => i.Title).ToArray()
            );
        }

        [Route("Admin/Page/Add")]
        public ActionResult PageAdd() {
            ViewBag.model = new Page() {
                Active = true
            };
            return View("PageEdit");
        }

        [HttpPost]
        [Route("Admin/Page/Add")]
        public ActionResult PageAdd(Page model) {

            //hatalar hatalar
            if (!ModelState.IsValid) {
                ViewBag.model = model;
                return View("PageEdit");
            }

            //yeni
            db.Insert(model);

            //section listele
            return RedirectToAction("Section", new {
                PageID = model.Id
            });
        }

        [Route("Admin/Page/Edit/{ID:int}")]
        public ActionResult PageEdit(int ID) {
            ViewBag.model = db.Page.Find(ID);
            return View();
        }

        [HttpPost]
        [Route("Admin/Page/Edit/{ID:int}")]
        public ActionResult PageEdit(Page model) {

            //hatalar hatalar
            if (!ModelState.IsValid) {
                ViewBag.model = model;
                return View();
            }

            //yeni
            db.Update(model);

            //section listele
            return RedirectToAction("Section", new {
                PageID = model.Id
            });

        }

        [Route("Admin/Page/Delete/{ID:int}")]
        public ActionResult PageDelete(int ID) {

            //silinecek model
            var model = db.Page.Find(ID);

            //sil 
            db.Delete(model);

            //anasayfaya yönlendir
            return RedirectToAction("Index");
        }

        //===============================================================
        //SECTION 
        //===============================================================
        [Route("Admin/Section/{PageID:int}")]
        public ActionResult Section(int PageID) {
            var page = db.Page.Find(PageID);
            ViewBag.page = page;
            ViewBag.sections = page.Section.OrderBy(i => i.No).ToList();
            ViewBag.types = blocks;
            return View();
        }

        [Route("Admin/Section/Add/{PageID:int}")]
        public ActionResult SectionAdd(int PageID) {
            ViewBag.model = new Section() {
                PageId = PageID
            };
            return View("SectionEdit");
        }

        [HttpPost]
        [Route("Admin/Section/Add/{PageID:int}")]
        public ActionResult SectionAdd(Section model) {

            //hatalar hatalar
            if (!ModelState.IsValid) {
                ViewBag.model = model;
                return View("SectionEdit");
            }

            //yeni
            model.No = db.Section.Any() ? db.Section.Max(i => i.No) + 1 : 1;
            db.Insert(model);

            //tamamdır
            return RedirectToAction("Section", new {
                PageID = model.PageId
            });
        }

        [Route("Admin/Section/Edit/{ID:int}")]
        public ActionResult SectionEdit(int ID) {
            ViewBag.model = db.Section.Find(ID);
            return View();
        }

        [HttpPost]
        [Route("Admin/Section/Edit/{ID:int}")]
        public ActionResult SectionEdit(Section model) {

            //hatalar hatalar
            if (!ModelState.IsValid) {
                ViewBag.model = model;
                return View();
            }

            //yeni
            db.Update(model, "No");

            //tamamdır
            return RedirectToAction("Section", new {
                PageID = model.PageId
            });

        }

        [Route("Admin/Section/Delete/{ID:int}")]
        public ActionResult SectionDelete(int ID) {

            //silinecek model
            var model = db.Section.Find(ID);

            //sil 
            db.Delete(model);

            //geri git
            return RedirectToAction("Section", new {
                PageID = model.PageId
            });
        }

        [Route("Admin/Section/{Direction}/{ID:int}")]
        public ActionResult SectionMove(string Direction, int ID) {

            //şuanki kayıt
            var current = db.Section.Find(ID);

            //tüm kayıtlar
            var all = current.Page.Section.OrderBy(i => i.No).ToList();

            //en üstte değilse
            var index = all.FindIndex(i => i.Id == ID);

            //yukarı
            var previousIndex = index;
            if (Direction == "up") {
                if (index > 0) {
                    previousIndex--;
                }
            }

            //aşağı
            else if (Direction == "down") {
                if (index < all.Count - 1) {
                    previousIndex++;
                }
            }

            //kaydet
            var previous = all[previousIndex];
            var temp = previous.No;
            previous.No = current.No;
            current.No = temp;
            db.Update(previous);
            db.Update(current);

            //yeniden listele
            var section = db.Section.Find(current.Id);
            return RedirectToAction("Section", new {
                PageID = section.PageId
            });

        }

        //===============================================================
        //BLOCK 
        //===============================================================
        [Route("Admin/Block/Delete/{ID:int}")]
        public ActionResult BlockDelete(int ID) {

            //silinecek model
            var model = db.Block.Find(ID);

            //sil 
            db.Delete(model);

            //yeniden listele
            var section = db.Section.Find(model.SectionId);
            return RedirectToAction("Section", new {
                PageID = section.PageId
            });
        }

        [Route("Admin/Block/{Direction}/{ID:int}")]
        public ActionResult BlockMove(string Direction, int ID) {

            //şuanki kayıt
            var current = db.Block.Find(ID);

            //tüm kayıtlar
            var all = current.Section.Block.OrderBy(i => i.No).ToList();

            //sırasını bul
            var index = all.FindIndex(i => i.Id == ID);

            //yukarı
            var previousIndex = index;
            if (Direction == "up") {
                if (index > 0) {
                    previousIndex--;
                }
            }

            //aşağı
            else if (Direction == "down") {
                if (index < all.Count - 1) {
                    previousIndex++;
                }
            }

            //kaydet
            var previous = all[previousIndex];
            var temp = previous.No;
            previous.No = current.No;
            current.No = temp;
            db.Update(previous);
            db.Update(current);

            //yeniden listele
            var section = db.Section.Find(current.SectionId);
            return RedirectToAction("Section", new {
                PageID = section.PageId
            });

        }

        //===============================================================
        //MENÜ 
        //===============================================================

        [ChildActionOnly]
        public ActionResult Menus() {
            return PartialView(
                db.Menu.Include("MenuItem").OrderBy(i => i.Name).ToArray()
            );
        }

        [Route("Admin/Menu/{MenuID:int}")]
        public ActionResult Menu(int MenuID) {
            var menu = db.Menu.Find(MenuID);
            ViewBag.menu = menu;
            ViewBag.model = menu.MenuItem.OrderBy(i => i.No).ToList();
            return View();
        }

        [Route("Admin/Menu/Add/{MenuID:int}")]
        public ActionResult MenuAdd(int MenuID) {
            ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
            ViewBag.model = new MenuItem() {
                MenuId = MenuID
            };
            return View("MenuEdit");
        }

        [HttpPost]
        [Route("Admin/Menu/Add/{MenuID:int}")]
        public ActionResult MenuAdd(MenuItem model) {

            //hatalar hatalar
            if (!ModelState.IsValid) {
                ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
                ViewBag.model = model;
                return View("MenuEdit");
            }

            //yeni
            model.No = db.MenuItem.Any() ? db.MenuItem.Max(i => i.No) + 1 : 1;
            db.Insert(model);

            //tamamdır
            return RedirectToAction("Menu", new {
                MenuID = model.MenuId
            });
        }

        [Route("Admin/Menu/Edit/{ID:int}")]
        public ActionResult MenuEdit(int ID) {
            var model = db.MenuItem.Find(ID);
            ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
            ViewBag.model = model;
            ViewBag.ExternalUrl = model.Url;
            return View();
        }

        [HttpPost]
        [Route("Admin/Menu/Edit/{ID:int}")]
        public ActionResult MenuEdit(MenuItem model, string ExternalUrl) {

            //hatalar hatalar
            if (!ModelState.IsValid) {
                ViewBag.pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
                ViewBag.model = model;
                ViewBag.externalUrl = model.Url;
                return View();
            }

            //yeni
            model.Url = string.IsNullOrEmpty(model.Url) ? ExternalUrl : model.Url;
            db.Update(model, "No");

            //tamamdır
            return RedirectToAction("Menu", new {
                MenuID = model.MenuId
            });

        }

        [Route("Admin/Menu/Delete/{ID:int}")]
        public ActionResult MenuDelete(int ID) {

            //silinecek model
            var model = db.MenuItem.Find(ID);

            //sil 
            db.Delete(model);

            //geri git
            return RedirectToAction("Menu", new {
                MenuID = model.MenuId
            });
        }

        [Route("Admin/Menu/{Direction}/{ID:int}")]
        public ActionResult MenuMove(string Direction, int ID) {

            //şuanki kayıt
            var current = db.MenuItem.Find(ID);

            //tüm kayıtlar
            var all = current.Menu.MenuItem.OrderBy(i => i.No).ToList();

            //sırasını bul
            var index = all.FindIndex(i => i.Id == ID);

            //yukarı
            var previousIndex = index;
            if (Direction == "up") {
                if (index > 0) {
                    previousIndex--;
                }
            }

            //aşağı
            else if (Direction == "down") {
                if (index < all.Count - 1) {
                    previousIndex++;
                }
            }

            //kaydet
            var previous = all[previousIndex];
            var temp = previous.No;
            previous.No = current.No;
            current.No = temp;
            db.Update(previous);
            db.Update(current);

            //yeniden listele
            return RedirectToAction("Menu", new {
                MenuID = current.MenuId
            });

        }

        //===============================================================
        //SETTINGS 
        //===============================================================

        [Route("Admin/Settings")]
        public ActionResult Settings() {
            ViewBag.model = db.Setting.FirstOrDefault();
            return View();
        }

        [HttpPost]
        [Route("Admin/Settings")]
        public ActionResult Settings(Setting model, string AdminSifreTekrar) {

            //hatalar hatalar
            if (!ModelState.IsValid) {
                ViewBag.model = model;
                return View();
            }

            //şifre değiştirilmeyecek
            if (string.IsNullOrEmpty(model.AdminSifre)) {
                db.Update(model, "AdminKullaniciAdi", "AdminSifre");
                alertSuccess("Başarılı", "İşlem tamamlandı");
            }

            //parola depiştir
            else if (model.AdminSifre == AdminSifreTekrar) {
                db.Update(model, "AdminKullaniciAdi");
                alertSuccess("Başarılı", "İşlem tamamlandı");
            }

            //hata
            else {
                alertDanger("Hata", "Parola ve tekrarı farklı");
            }

            //tamamdır
            return RedirectToAction("Settings");

        }

        //===============================================================
        //THUMBNAIL
        //===============================================================
        [OutputCache(Duration = 30000)]
        [Route("Thumbnail")]
        public ActionResult Thumbnail(int w, int h, string f) {

            if (string.IsNullOrEmpty(f)) {
                f = "no-photo.png";
            }

            //dosya aç
            var dir = Server.MapPath("~/Assets/uploads");
            var path = Path.Combine(dir, f);
            var img = System.Drawing.Image.FromFile(path);

            //boyutlandır
            var _maxWidth = w > 0 ? w : img.Width;
            var _maxHeight = h > 0 ? h : img.Height;
            var _scaleWidth = (float)_maxWidth / img.Width;
            var _scaleHeight = (float)_maxHeight / img.Height;
            var _scale = Math.Min(1f, Math.Min(_scaleWidth, _scaleHeight));
            var _newWidth = (int)(_scale * img.Width);
            var _newHeight = (int)(_scale * img.Height);
            var bmp = new Bitmap(img, _newWidth, _newHeight);

            //kaydet
            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            var data = ms.ToArray();

            ms.Close();
            bmp.Dispose();
            img.Dispose();

            return File(data, "image/png");
        }

    }
}