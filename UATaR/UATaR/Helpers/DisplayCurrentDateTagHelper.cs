﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace UATaR.Helpers
{
    public class DisplayCurrentDateTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.SetContent($"Current date: {DateTime.Now:dd.MM.yyyy}");
        }
    }
}
