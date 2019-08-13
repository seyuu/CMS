using System.Linq;
using System.Text;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers {

    public class FormController : BaseBlockMultiController<Form, FormItem> {

        [HttpPost]
        [ChildActionOnly]
        public PartialViewResult View(int id, FormCollection formCollection) {
            var state = 2;
            var form = (Form)db.Block.Find(id);
            var sb = new StringBuilder();
            foreach (FormItem i in form.BlockItem) {
                var inputName = "field-" + i.Id;
                var inputValue = formCollection[inputName];
                if (i.Required && string.IsNullOrEmpty(inputValue)) {
                    state = 1;
                }
                sb.AppendFormat("{0}:{1}<br/>", i.Title, inputValue);
            }

            if (state == 2) {
                if (!Util.ePostaGonder(form.Subject, sb.ToString(), form.Email)) {
                    state = 3;
                };
            }

            ViewBag.state = state;
            ViewBag.model = db.Block.Find(id);
            return PartialView();
        }

    }

}