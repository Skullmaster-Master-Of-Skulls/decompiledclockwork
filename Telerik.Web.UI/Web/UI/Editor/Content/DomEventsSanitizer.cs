using System;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.Editor.Content
{
	// Token: 0x02000279 RID: 633
	public class DomEventsSanitizer : IContentSanitizer
	{
		// Token: 0x060016DD RID: 5853 RVA: 0x0004D3B4 File Offset: 0x0004B5B4
		public string Sanitize(string input)
		{
			return HtmlTagSanitizer.SanitizeTags(input, new MatchEvaluator(this.SanitizeEventAttributes));
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0004D3C8 File Offset: 0x0004B5C8
		private string SanitizeEventAttributes(Match m)
		{
			return DomEventsSanitizer.attributePattern.Replace(m.Value, "");
		}

		// Token: 0x040005FD RID: 1533
		public static string[] DomEvents = new string[]
		{
			"abort",
			"activate",
			"afterprint",
			"beforeactivate",
			"beforecopy",
			"beforecut",
			"beforedeactivate",
			"beforepaste",
			"beforeprint",
			"beforeunload",
			"blur",
			"canplay",
			"canplaythrough",
			"change",
			"click",
			"contextmenu",
			"copy",
			"cuechange",
			"cut",
			"dblclick",
			"deactivate",
			"drag",
			"dragend",
			"dragenter",
			"dragleave",
			"dragover",
			"dragstart",
			"drop",
			"durationchange",
			"emptied",
			"ended",
			"error",
			"focus",
			"focusin",
			"focusout",
			"gotpointercapture",
			"hashchange",
			"help",
			"input",
			"keydown",
			"keypress",
			"keyup",
			"load",
			"loadeddata",
			"loadedmetadata",
			"loadstart",
			"lostpointercapture",
			"message",
			"mousedown",
			"mouseenter",
			"mouseleave",
			"mousemove",
			"mouseout",
			"mouseover",
			"mouseup",
			"mousewheel",
			"mscontentzoom",
			"msgesturechange",
			"msgesturedoubletap",
			"msgestureend",
			"msgesturehold",
			"msgesturestart",
			"msgesturetap",
			"msgotpointercapture",
			"msinertiastart",
			"mslostpointercapture",
			"msmanipulationstatechanged",
			"mspointercancel",
			"mspointerdown",
			"mspointerenter",
			"mspointerleave",
			"mspointermove",
			"mspointerout",
			"mspointerover",
			"mspointerup",
			"offline",
			"online",
			"pagehide",
			"pageshow",
			"paste",
			"pause",
			"play",
			"playing",
			"pointercancel",
			"pointerdown",
			"pointerenter",
			"pointerleave",
			"pointermove",
			"pointerout",
			"pointerover",
			"pointerup",
			"popstate",
			"progress",
			"ratechange",
			"reset",
			"resize",
			"scroll",
			"seeked",
			"seeking",
			"select",
			"selectstart",
			"stalled",
			"storage",
			"submit",
			"suspend",
			"timeupdate",
			"toggle",
			"unload",
			"volumechange",
			"waiting"
		};

		// Token: 0x040005FE RID: 1534
		private static readonly Regex attributePattern = new Regex("\\son(?:" + string.Join("|", DomEventsSanitizer.DomEvents) + ")=(?:\"(?:\\\\\"|[^\"])*\"|'(?:\\\\'|[^'])*'|[^\\s]*?)(?=\\s|>)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
	}
}
