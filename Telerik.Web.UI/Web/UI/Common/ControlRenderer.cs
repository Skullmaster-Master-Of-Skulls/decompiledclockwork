using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Common
{
	// Token: 0x02001828 RID: 6184
	public class ControlRenderer
	{
		// Token: 0x0600F072 RID: 61554 RVA: 0x0036A6A4 File Offset: 0x003688A4
		internal static void EnsureChildControlsAreNotRegistered(Control parentControl)
		{
			foreach (object obj in parentControl.Controls)
			{
				Control control = (Control)obj;
				IControl control2 = control as IControl;
				if (control2 != null)
				{
					control2.RegisterWithScriptManager = false;
				}
				else
				{
					ControlRenderer.EnsureChildControlsAreNotRegistered(control);
				}
			}
		}

		// Token: 0x0600F073 RID: 61555 RVA: 0x0036A710 File Offset: 0x00368910
		public static List<string> GetControlScriptsCollection(Control controlRef, bool isRecursive)
		{
			List<string> controlScriptsCollection = ControlRenderer.GetControlScriptsCollection(controlRef);
			if (isRecursive)
			{
				IControl control = controlRef as IControl;
				if (control != null)
				{
					control.EnsureChildControlsCreated();
				}
				foreach (object obj in controlRef.Controls)
				{
					Control controlRef2 = (Control)obj;
					controlScriptsCollection.AddRange(ControlRenderer.GetControlScriptsCollection(controlRef2, isRecursive));
				}
			}
			return controlScriptsCollection;
		}

		// Token: 0x0600F074 RID: 61556 RVA: 0x0036A790 File Offset: 0x00368990
		public static List<string> GetControlScriptsCollection(Control controlRef)
		{
			List<string> list = new List<string>();
			IScriptControl scriptControl = controlRef as IScriptControl;
			if (scriptControl != null)
			{
				list = ControlRenderer.GetControlScriptsUrls(new RadControlRenderHelper
				{
					ScriptReferences = new List<ScriptReference>(scriptControl.GetScriptReferences())
				});
				List<string> scriptManagerUrlsInternal = ControlRenderer.GetScriptManagerUrlsInternal();
				if (scriptManagerUrlsInternal != null)
				{
					foreach (string item in scriptManagerUrlsInternal)
					{
						if (list.Contains(item))
						{
							list.Remove(item);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600F075 RID: 61557 RVA: 0x0036A824 File Offset: 0x00368A24
		private static string RenderControlInPage(Control newControl)
		{
			Page page = new Page();
			HtmlForm htmlForm = new HtmlForm();
			ScriptManager child = new ScriptManager();
			StringWriter stringWriter = new StringWriter();
			page.Controls.Add(htmlForm);
			htmlForm.Controls.Add(child);
			htmlForm.Controls.Add(newControl);
			if (HttpContext.Current != null && HttpContext.Current.Server != null)
			{
				HttpContext.Current.Server.Execute(page, stringWriter, false);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600F076 RID: 61558 RVA: 0x0036A898 File Offset: 0x00368A98
		private static List<string> GetControlScriptsUrls(Control newControl)
		{
			List<string> list = new List<string>();
			string input = ControlRenderer.RenderControlInPage(newControl);
			Regex regex = new Regex("<script src=\"(?<scriptUrl>[^\"]+ScriptResource.axd[^\"]+)\"", RegexOptions.Compiled);
			Match match = regex.Match(input);
			while (match != null && match.Success)
			{
				string value = match.Groups["scriptUrl"].Value;
				if (!list.Contains(value))
				{
					list.Add(value);
				}
				match = match.NextMatch();
			}
			return list;
		}

		// Token: 0x0600F077 RID: 61559 RVA: 0x0036A904 File Offset: 0x00368B04
		private static List<string> GetScriptManagerUrlsInternal()
		{
			string text = "TelerikScriptManagerUrls";
			if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Headers != null)
			{
				string text2 = HttpContext.Current.Request.Headers["Accept-encoding"];
				if (text2 != null)
				{
					text += text2;
				}
				XhtmlConformanceMode? xhtmlConformanceMode = ControlRenderer.GetXhtmlConformanceMode();
				if (xhtmlConformanceMode != null)
				{
					text += xhtmlConformanceMode.ToString();
				}
			}
			List<string> list = null;
			if (HttpContext.Current != null && HttpContext.Current.Cache != null)
			{
				list = (HttpContext.Current.Cache.Get(text) as List<string>);
			}
			if (list == null)
			{
				UpdatePanel updatePanel = new UpdatePanel();
				Button child = new Button();
				updatePanel.ContentTemplateContainer.Controls.Add(child);
				list = ControlRenderer.GetControlScriptsUrls(updatePanel);
				if (HttpContext.Current != null && HttpContext.Current.Cache != null)
				{
					HttpContext.Current.Cache.Add(text, list, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0), CacheItemPriority.Low, null);
				}
			}
			return list;
		}

		// Token: 0x0600F078 RID: 61560 RVA: 0x0036AA14 File Offset: 0x00368C14
		public static string GetControlScripts(Control controlRef)
		{
			IScriptControl scriptControl = controlRef as IScriptControl;
			StringBuilder stringBuilder = new StringBuilder();
			if (scriptControl != null && controlRef.Visible)
			{
				List<string> controlScriptsCollection = ControlRenderer.GetControlScriptsCollection(controlRef);
				IControl control = controlRef as IControl;
				bool flag = control != null && !control.RegisterWithScriptManager;
				foreach (string text in controlScriptsCollection)
				{
					if ((HttpContext.Current != null && HttpContext.Current.Items[text] == null) || flag)
					{
						HttpContext.Current.Items[text] = true;
						stringBuilder.Append(string.Format("\r\n<script src=\"{0}\" type=\"text/javascript\"></script>", text));
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600F079 RID: 61561 RVA: 0x0036AAE8 File Offset: 0x00368CE8
		public static string GetControlCssReferences(ISkinnableControl control)
		{
			SkinRegistrar.RegisterCssReferences(control);
			return control.AjaxCssRegistrations;
		}

		// Token: 0x0600F07A RID: 61562 RVA: 0x0036AAF8 File Offset: 0x00368CF8
		public static string GetControlHtml(Control control)
		{
			StringWriter stringWriter = new StringWriter();
			control.RenderControl(new HtmlTextWriter(stringWriter));
			return stringWriter.ToString();
		}

		// Token: 0x0600F07B RID: 61563 RVA: 0x0036AB20 File Offset: 0x00368D20
		public static string GetControlDescriptors(Control controlRef)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IScriptControl scriptControl = controlRef as IScriptControl;
			if (scriptControl != null && controlRef.Visible)
			{
				IEnumerable<ScriptDescriptor> scriptDescriptors = scriptControl.GetScriptDescriptors();
				if (scriptDescriptors != null)
				{
					foreach (ScriptDescriptor scriptDescriptor in scriptDescriptors)
					{
						RadControlScriptDescriptor radControlScriptDescriptor = scriptDescriptor as RadControlScriptDescriptor;
						if (radControlScriptDescriptor != null)
						{
							stringBuilder.Append(radControlScriptDescriptor.Script);
						}
					}
				}
			}
			string arg = stringBuilder.ToString();
			return string.Format("<script type=\"text/javascript\">\r\n//<![CDATA[ \r\n    {0}\r\n//]]>\r\n</script>", string.Format("Sys.Application.add_init(function(){{{0}}});", arg));
		}

		// Token: 0x0600F07C RID: 61564 RVA: 0x0036ABC4 File Offset: 0x00368DC4
		private static XhtmlConformanceMode? GetXhtmlConformanceMode()
		{
			XhtmlConformanceSection xhtmlConformanceSection = ConfigurationManager.GetSection("system.web/xhtmlConformance") as XhtmlConformanceSection;
			if (xhtmlConformanceSection != null)
			{
				return new XhtmlConformanceMode?(xhtmlConformanceSection.Mode);
			}
			return null;
		}

		// Token: 0x04004546 RID: 17734
		private const string JavaScriptBlockFormat = "<script type=\"text/javascript\">\r\n//<![CDATA[ \r\n    {0}\r\n//]]>\r\n</script>";

		// Token: 0x04004547 RID: 17735
		private const string AddInitStatementFormat = "Sys.Application.add_init(function(){{{0}}});";

		// Token: 0x04004548 RID: 17736
		private const string ScriptReferenceFormat = "\r\n<script src=\"{0}\" type=\"text/javascript\"></script>";

		// Token: 0x04004549 RID: 17737
		private const string ScriptManagerScriptsCacheKey = "TelerikScriptManagerUrls";
	}
}
