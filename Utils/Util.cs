using System;
using System.Dynamic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.IO;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;
using System.Web.SessionState;

using System.Text;
using System.Web.Mvc.Html;

public static class Util {

    //==================================================
    //URL SEÇİCİ
    //==================================================
    public static List<SelectListItem> getUrlList() {
        var db = new Db();
        var pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
        var items = new List<SelectListItem>();
        foreach (var i in pages) {
            var pageURL = Util.urlYap(i.Title) + "-" + i.Id;
            items.Add(
                new SelectListItem() {
                    Text = i.Title,
                    Value = pageURL
                }
            );
            foreach (var j in i.Section) {
                var selectURL = pageURL + "#section-" + j.Id;
                items.Add(
                    new SelectListItem() {
                        Text = "-" + j.Title,
                        Value = selectURL
                    }
                );
            }
        }
        return items;
    }


    //public static IHtmlString selectURL(this HtmlHelper html, string name, string value) {
    //    var db = new Db();
    //    var pages = db.Page.Include("Section").OrderBy(i => i.Title).ToList();
    //    var items = new List<SelectListItem>();
    //    foreach (var i in pages) {
    //        var pageURL = Util.urlYap(i.Title) + "-" + i.ID;
    //        items.Add(
    //            new SelectListItem() {
    //                Text = i.Title,
    //                Value = pageURL
    //            }
    //        );
    //        foreach (var j in i.Section) {
    //            var selectURL = Util.urlYap(i.Title) + "-" + i.ID;
    //            new SelectListItem() {
    //                Text = i.Title,
    //                Value = pageURL + "#" + j.ID
    //            };
    //        }
    //    }
    //    return html.ListBox(name, items);
    //}

    //==================================================
    //ALERT
    //==================================================
    public static IHtmlString validationState(this HtmlHelper html, string model) {
        if (html.ViewContext.HttpContext.Request.HttpMethod == "POST") {
            if (html.ViewData.ModelState.IsValidField(model)) {
                return new HtmlString("has-success");
            }
            else {
                return new HtmlString("has-warning");
            }
        }
        return new HtmlString("");
    }

    public static IHtmlString renderAlert(this HtmlHelper html) {

        var tempData = html.ViewContext.TempData;
        var alertType = tempData["alertType"];
        var alertIcon = tempData["alertIcon"];
        var alertTitle = (string)tempData["alertTitle"];
        var alertMessage = tempData["alertMessage"];

        //alert yoksa çızıktırma
        if (string.IsNullOrEmpty(alertTitle)) {
            return null;
        }

        //çızıktır
        return new HtmlString(string.Format(@"
            <div class=""alert alert-{0} alert-dismissable"">
                <button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">×</button>
                <h4><i class=""fa {1}""></i> {2}</h4> {3}
            </div>",
            alertType,
            alertIcon,
            alertTitle,
            alertMessage
        ));
    }

    ///==================================================
    ///KULLANICI
    ///==================================================
    public static bool oturumAcikmi() {
        return session["oturum"] != null;
    }

    public static void oturumAc(object data) {
        session["oturum"] = data;
    }

    public static void oturumKapat() {
        session["oturum"] = null;
    }

    //==================================================
    //UPLOAD
    //==================================================
    public static string thumbnailLink(string file, int width = 100, int height = 100) {
        return "/thumbnail?w=" + width + "&h=" + height + "&f=" + file;
    }

    public static HtmlString thumbnail(string file, int width = 100, int height = 100) {
        return new HtmlString(@"<img src=""/thumbnail?w=" + width + "&h=" + height + "&f=" + file + @""" />");
    }

    public static string resimYukle(string alanadi, string eskiResim, int maxWidth = 2048, int maxHeight = 2048) {
        return resimYukle(HttpContext.Current.Request.Files[alanadi], eskiResim, maxWidth, maxHeight);
    }

    public static string resimYukle(HttpPostedFile yuklenenResim, string eskiResim, int maxWidth = 2048, int maxHeight = 2048) {

        //yeni resim yok eskisini kullanmaya devam
        if (yuklenenResim == null || yuklenenResim.ContentLength == 0) {
            return eskiResim;
        }

        //yükle
        var image = new WebImage(yuklenenResim.InputStream);

        //boyutlandır
        if (image.Width > maxWidth || image.Height > maxHeight) {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            image.Resize(newWidth, newHeight);
        }

        //kaydet
        var savePath = context.Server.MapPath("~/Assets/uploads/");
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(yuklenenResim.FileName);
        image.Save(savePath + fileName);

        //eski resmi sil
        if (File.Exists(savePath + eskiResim)) {
            File.Delete(savePath + eskiResim);
        }

        //dosya ismini döndür
        return fileName;
    }

    public static string dosyaYukle(HttpPostedFileBase yuklenen, string eski) {

        //yeni dosya yok eskisini kullanmaya devam
        if (yuklenen == null || yuklenen.ContentLength == 0) {
            return eski;
        }

        //kaydet
        var savePath = context.Server.MapPath("~/Assets/uploads/");
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(yuklenen.FileName);
        yuklenen.SaveAs(savePath + fileName);

        //eski resmi sil
        if (File.Exists(savePath + eski)) {
            File.Delete(savePath + eski);
        }

        //dosya ismini döndür
        return fileName;
    }

    public static void dosyaSil(string adi) {
        var savePath = context.Server.MapPath("~/Assets/uploads/" + adi);
        if (File.Exists(savePath)) {
            File.Delete(savePath);
        }
    }

    //==================================================
    //DATE HELPER
    //==================================================

    public static DateTime FirstDayOfMonth(this DateTime value) {
        return new DateTime(value.Year, value.Month, 1);
    }

    public static int DaysInMonth(this DateTime value) {
        return DateTime.DaysInMonth(value.Year, value.Month);
    }

    public static DateTime LastDayOfMonth(this DateTime value) {
        return new DateTime(value.Year, value.Month, value.DaysInMonth());
    }

    //==================================================
    //FORMAT
    //==================================================

    public static string Format(object value, string format) {
        return value == null ? "" : string.Format("{0:" + format + "}", value);
    }

    public static string ToShortDateString(this DateTime? value) {
        if (value.HasValue) {
            return value.Value.ToShortDateString();
        }
        else {
            return "";
        }
    }

    public static string ToShortDateString(this DateTime value) {
        if (value == default(DateTime)) {
            return "";
        }
        else {
            return value.ToShortDateString();
        }
    }

    public static string ToShortTimeString(this TimeSpan? value) {
        if (value.HasValue) {
            return value.Value.ToString(@"hh\:mm");
        }
        else {
            return "";
        }
    }

    public static string ToShortTimeString(this TimeSpan value) {
        return value.ToString(@"hh\:mm");
    }

    public static string ToDateTimeString(this DateTime value) {
        if (value == default(DateTime)) {
            return "";
        }
        else {
            return value.ToString();
        }
    }

    public static HtmlString ToImage(string value, int width = 0, int height = 0) {
        var style = "";
        if (width > 0) style += "width:" + width + "px;";
        if (height > 0) style += "height:" + height + "px;";
        if (style != "") style = " style=\"" + style + "\"";
        if (!string.IsNullOrEmpty(value)) {
            return new HtmlString(
                "<img src=\"/Assets/uploads/" + value + "\"" + style + "/>"
            );
        }
        else {
            return new HtmlString("");
        }
    }

    //==================================================
    //IVIR
    //==================================================

    public static void bidirim(string konu, string mesaj, string sayfa) {
    }

    //public static bool smsGonder(string mesaj, params string[] kime) {
    //    return true;

    //    // Phone Number
    //    var SMSUser = "5454549929";
    //    var SMSPass = "8336324";
    //    var SMSOriginator = "5372727262";//TODO:

    //    // Create Sms Object
    //    var sms = new IletiMerkezi.SMS.SmsSender(SMSUser, SMSPass, SMSOriginator);
    //    return sms.SendSMS(kime, mesaj);

    //}

    public static bool ePostaGonder(string konu, string mesaj, string kime) {
       try {

            var db = new Db();
            var settings = db.Setting.FirstOrDefault();

            var mail = new MailMessage();
            mail.To.Add(kime);
            mail.From = new MailAddress(settings.SmtpEmail);
            mail.Subject = konu;
            mail.Body = mesaj;
            mail.IsBodyHtml = true;

            var smtp = new SmtpClient();
            smtp.Host = settings.SmtpServer;
            smtp.Credentials = new NetworkCredential(settings.SmtpEmail, settings.SmtpPassword);
            smtp.EnableSsl = settings.SmtpSsl;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Port = settings.SmtpPort;
            smtp.Timeout = 5000;
            smtp.Send(mail);

        }
        catch {
            return false;
        }
        return true;
    }

    //public static string[] ePostaGonder(string konu, string mesaj, params string[] kimlere) {
    //    var fail = new List<string>();
    //    foreach (var i in kimlere) {
    //        try {

    //            var mail = new MailMessage();
    //            mail.To.Add(i);
    //            mail.From = new MailAddress("asdm@gmail.com");
    //            mail.Subject = konu;
    //            mail.Body = mesaj;
    //            mail.IsBodyHtml = true;

    //            var smtp = new SmtpClient();
    //            smtp.Host = "smtp.gmail.com";
    //            smtp.Credentials = new NetworkCredential("asdm@gmail.com", "asd");//TODO: ayarlardan al
    //            smtp.EnableSsl = true;
    //            smtp.Send(mail);

    //        }

    //        catch {
    //            fail.Add(i);
    //        }

    //    }
    //    return fail.ToArray();
    //}

    //==================================================
    //ZIVIR
    //==================================================

    public static string DescriptionAttr<T>(this T source) {
        FieldInfo fi = source.GetType().GetField(source.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attributes != null && attributes.Length > 0) return attributes[0].Description;
        else return source.ToString();
    }

    public static SelectList GetSelectList(object value) {
        var type = value.GetType();
        var underlyingType = Enum.GetUnderlyingType(type);
        var values = from object i in Enum.GetValues(type)
                     select new {
                         value = Convert.ChangeType(i, underlyingType),
                         name = i.DescriptionAttr()
                     };
        return new SelectList(values, "value", "name", Convert.ChangeType(value, underlyingType));
    }

    public static TSource[] ToArray<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) {
        return source.Where(predicate).ToArray();
    }

    public static ExpandoObject[] ToExpando<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector) {
        return source.Select(selector).ToExpando();
    }

    public static ExpandoObject[] ToExpando<TSource>(this IEnumerable<TSource> source) {
        var ret = new List<ExpandoObject>();
        var props = typeof(TSource).GetProperties();
        foreach (var item in source.ToArray()) {
            IDictionary<string, object> dic = new ExpandoObject();
            foreach (var prop in props) {
                dic.Add(prop.Name, prop.GetValue(item));
            }
            ret.Add((ExpandoObject)dic);
        }
        return ret.ToArray();
    }


    //public static dynamic ToExpando(this object source) {
    //    if (source == null) {
    //        return null;
    //    }
    //    else if (source is IEnumerable && !(source is string)) {
    //        var ret = new List<dynamic>();
    //        foreach (var item in ((IEnumerable)source)) {
    //            ret.Add(item.ToExpando());
    //        }
    //        return ret;
    //    }

    //    else if (source.GetType().Name.Contains("AnonymousType")) {
    //        IDictionary<string, object> ret = new ExpandoObject();
    //        foreach (var prop in source.GetType().GetProperties()) {
    //            ret.Add(prop.Name, prop.GetValue(source).ToExpando());
    //        }
    //        return (ExpandoObject)ret;
    //    }

    //    return source;
    //}

    //==================================================
    //CONTEXT
    //==================================================

    public static HttpContext context {
        get {
            return HttpContext.Current;
        }
    }

    public static HttpRequest request {
        get {
            return context.Request;
        }
    }

    public static HttpResponse response {
        get {
            return context.Response;
        }
    }

    public static HttpSessionState session {
        get {
            return context.Session;
        }
    }

    public static string urlYap(string Başlık) {
        return Unichar.UnicodeStrings.LatinToAscii(Başlık).ToLower().Replace("-", "").Replace("  ", " ").Replace(" ", "-");
    }

}