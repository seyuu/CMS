using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using CMS.Models;

namespace CMS.Controllers {

    public class BaseBlockMultiController<BlockT, ItemT> : 
        BaseBlockController<BlockT>
        where BlockT : Block, new()
        where ItemT : BlockItem, new() {

        public ActionResult Items(int BlockId) {
            ViewBag.BlockId = BlockId;
            ViewBag.model = db.BlockItem.Where(i => i.BlockId == BlockId).ToList();
            return View();
        }

        public ActionResult ItemAdd(int BlockId) {
            ViewBag.sectionID = db.Block.Find(BlockId).SectionId;
            ViewBag.model = new ItemT() {
                BlockId = BlockId
            };
            return View("ItemEdit");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ItemAdd(ItemT model) {

            //hata
            if (!ModelState.IsValid) {
                ViewBag.sectionID = db.Block.Find(model.Block).SectionId;
                ViewBag.model = model;
                return View("ItemEdit");
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
            db.Insert(model);

            //başarılı
            return RedirectToAction("Items", new {
                BlockId = model.BlockId
            });
        }

        public ActionResult ItemEdit(int ID) {
            ViewBag.model = db.BlockItem.Find(ID);
            ViewBag.sectionId = ViewBag.model.Block.SectionId;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ItemEdit(ItemT model) {

            //hata
            if (!ModelState.IsValid) {
                ViewBag.model = model;
                ViewBag.sectionId = db.Block.Find(model.BlockId).SectionId;
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
                    var oldModel = db.BlockItem.FirstOrDefault(i => i.BlockId == model.BlockId);

                    //modelin database ile olan ilişkisini kes yoksa hata verecek
                    var entry = db.Entry((ItemT)oldModel);
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
            db.Update(model);

            //başarılı
            return RedirectToAction("Items", new {
                BlockId = model.BlockId
            });

        }

        public ActionResult ItemDelete(int ID) {

            //silinecek model
            var model = db.BlockItem.Find(ID);
            var BlockId = model.Block.Id;

            //sil
            db.Delete(model);

            //başarılı
            return RedirectToAction("Items", new {
                BlockId = BlockId
            });

        }
    }

}