using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lisovskii_20331.UI.TagHelpers
{
    [HtmlTargetElement("img", Attributes = "img-action, img-controller")]
    public class ImageTagHelper(LinkGenerator linkGenerator) : TagHelper
    {
        public string ImgController {  get; set; }
        public string ImgAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
			output.Attributes.Add("src", linkGenerator.GetPathByAction(ImgAction, ImgController));
            output.Attributes.Add("style", "margin-right: 5px;");
			output.Attributes.Add("width", "50");
		}
    }
}
