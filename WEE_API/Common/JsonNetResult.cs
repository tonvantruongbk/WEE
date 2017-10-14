using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WEE_WEB_API.Common
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings SerializerSettings { get; set; }
        public Formatting Formatting { get; set; }

        public JsonNetResult()
        {
            SerializerSettings = new JsonSerializerSettings
            {
               // DateFormatString = "dd'/'MM'/'yyyy HH':'mm':'ss",
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Formatting = Formatting.Indented,
                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            Formatting = Formatting.Indented;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null) response.ContentEncoding = ContentEncoding;
            JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };
            JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
            serializer.Serialize(writer, Data);
            writer.Flush();
        }
    }

    public class ShortDateTimeConverter : IsoDateTimeConverter
    {
        public ShortDateTimeConverter()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }
    }
}