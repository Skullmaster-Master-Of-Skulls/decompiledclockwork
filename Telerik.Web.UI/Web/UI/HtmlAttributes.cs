using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AF8 RID: 6904
	internal class HtmlAttributes
	{
		// Token: 0x06010B45 RID: 68421 RVA: 0x003B810E File Offset: 0x003B630E
		public static bool IsHtmlAttribute(string attribute)
		{
			return Array.IndexOf<string>(HtmlAttributes.Attributes, attribute.ToLower()) > -1;
		}

		// Token: 0x04004A95 RID: 19093
		private static readonly string[] Attributes = new string[]
		{
			"accesskey",
			"align",
			"class",
			"dir",
			"disabled",
			"id",
			"href",
			"hreflang",
			"lang",
			"language",
			"rel",
			"rev",
			"style",
			"tabindex",
			"target",
			"title",
			"type",
			"urn",
			"onactivate",
			"onafterupdate",
			"onbeforeactivate",
			"onbeforecopy",
			"onbeforecut",
			"onbeforedeactivate",
			"onbeforeeditfocus",
			"onbeforepaste",
			"onbeforeupdate",
			"onblur",
			"onclick",
			"oncontextmenu",
			"oncontrolselect",
			"oncopy",
			"oncut",
			"ondblclick",
			"ondeactivate",
			"ondrag",
			"ondragend",
			"ondragenter",
			"ondragleave",
			"ondragover",
			"ondragstart",
			"ondrop",
			"onerrorupdate",
			"onfilterchange",
			"onfocus",
			"onfocusin",
			"onfocusout",
			"onhelp",
			"onkeydown",
			"onkeypress",
			"onkeyup",
			"onlayoutcomplete",
			"onlosecapture",
			"onmousedown",
			"onmouseenter",
			"onmouseleave",
			"onmousemove",
			"onmouseout",
			"onmouseover",
			"onmouseup",
			"onmousewheel",
			"onmove",
			"onmoveend",
			"onmovestart",
			"onpaste",
			"onpropertychange",
			"onreadystatechange",
			"onresize",
			"onresizeend",
			"onresizestart",
			"onscroll",
			"onselectstart",
			"ontimeerror",
			"_designerregion"
		};
	}
}
