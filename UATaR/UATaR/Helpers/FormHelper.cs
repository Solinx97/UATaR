﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace UATaR.Helpers
{
    public static class FormHelper
    {
        public static HtmlString CreateForm(this IHtmlHelper html, string buttonClass, string controllerName, string actionName, string inputName, string id)
        {
            var form = new TagBuilder("form");
            form.Attributes.Add("action", $"/{controllerName}/{actionName}/");
            form.Attributes.Add("method", "get");

            var input = new TagBuilder("input");
            input.Attributes.Add("type", "hidden");
            input.Attributes.Add("name", "id");
            input.Attributes.Add("value", id);
            form.InnerHtml.AppendHtml(input);

            input = new TagBuilder("input");
            input.Attributes.Add("type", "submit");
            input.Attributes.Add("class", buttonClass);
            input.Attributes.Add("value", inputName);
            form.InnerHtml.AppendHtml(input);

            using var writer = new System.IO.StringWriter();
            form.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
    }
}
