using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.Common
{
	// Token: 0x020019DA RID: 6618
	internal class BaseClass
	{
		// Token: 0x06010043 RID: 65603 RVA: 0x0039779E File Offset: 0x0039599E
		internal static void RenderAjaxCssReferences(ISkinnableControl control, HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(control.AjaxCssRegistrations))
			{
				writer.Write(control.AjaxCssRegistrations);
			}
		}

		// Token: 0x06010044 RID: 65604 RVA: 0x003977BC File Offset: 0x003959BC
		internal static void RenderVersionStamp(HtmlTextWriter writer)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				return;
			}
			if (httpContext.Items["_!TelerikVersionStampRendered"] == null)
			{
				if (!ScriptManagerConfigurationSettings.GetConfiguration().EnableHandlerEncryption)
				{
					writer.Write(string.Format("<!-- {0} -->", Assembly.GetExecutingAssembly().GetName().Version));
				}
				httpContext.Items["_!TelerikVersionStampRendered"] = true;
			}
		}

		// Token: 0x06010045 RID: 65605 RVA: 0x00397828 File Offset: 0x00395A28
		internal static string GetShortControlName(Control control)
		{
			IList<EmbeddedSkinAttribute> allEmbeddedSkinAttributes = SkinRegistrar.GetAllEmbeddedSkinAttributes(control.GetType(), control.Page);
			if (allEmbeddedSkinAttributes != null && allEmbeddedSkinAttributes.Count < 1)
			{
				return control.GetType().Name.Replace("Rad", "");
			}
			return allEmbeddedSkinAttributes[0].ShortControlName;
		}

		// Token: 0x06010046 RID: 65606 RVA: 0x0039787C File Offset: 0x00395A7C
		private static string GetValueFromConfig(string keyFormat, Control control)
		{
			string text = ConfigurationManager.AppSettings[string.Format(keyFormat, BaseClass.GetShortControlName(control))];
			if (text != null)
			{
				return text;
			}
			return ConfigurationManager.AppSettings[string.Format(keyFormat, control.GetType().Name)];
		}

		// Token: 0x06010047 RID: 65607 RVA: 0x003978C0 File Offset: 0x00395AC0
		internal static bool GetGlobalEnableEmbeddedScripts(Control control)
		{
			string text = BaseClass.GetValueFromConfig("Telerik.{0}.EnableEmbeddedScripts", control);
			if (text == null)
			{
				text = ConfigurationManager.AppSettings["Telerik.EnableEmbeddedScripts"];
				if (text == null)
				{
					return true;
				}
			}
			bool result = true;
			bool.TryParse(text, out result);
			return result;
		}

		// Token: 0x06010048 RID: 65608 RVA: 0x00397900 File Offset: 0x00395B00
		internal static bool GetGlobalEnableEmbeddedBaseStylesheet(Control control)
		{
			string text = BaseClass.GetValueFromConfig("Telerik.{0}.EnableEmbeddedBaseStylesheet", control);
			if (text == null)
			{
				text = ConfigurationManager.AppSettings["Telerik.EnableEmbeddedBaseStylesheet"];
				if (text == null)
				{
					return true;
				}
			}
			bool result = true;
			bool.TryParse(text, out result);
			return result;
		}

		// Token: 0x06010049 RID: 65609 RVA: 0x00397940 File Offset: 0x00395B40
		internal static bool GetGlobalEnableEmbeddedSkins(Control control)
		{
			string text = BaseClass.GetValueFromConfig("Telerik.{0}.EnableEmbeddedSkins", control);
			if (text == null)
			{
				text = ConfigurationManager.AppSettings["Telerik.EnableEmbeddedSkins"];
				if (text == null)
				{
					return true;
				}
			}
			bool result = true;
			bool.TryParse(text, out result);
			return result;
		}

		// Token: 0x0601004A RID: 65610 RVA: 0x00397980 File Offset: 0x00395B80
		internal static string SerializeToString(object[] objToSerialize)
		{
			if (objToSerialize == null || objToSerialize.Length == 0)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in objToSerialize)
			{
				if (obj != null)
				{
					stringBuilder.Append(obj.ToString());
				}
				stringBuilder.Append("\n");
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0601004B RID: 65611 RVA: 0x003979F0 File Offset: 0x00395BF0
		internal static string[] DeserializeFromString(string serialized)
		{
			if (string.IsNullOrEmpty(serialized))
			{
				return new string[0];
			}
			return serialized.Split(new char[]
			{
				'\n'
			});
		}
	}
}
