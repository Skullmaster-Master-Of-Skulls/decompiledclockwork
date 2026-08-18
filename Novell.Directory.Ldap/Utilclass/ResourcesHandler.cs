using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000F5 RID: 245
	public class ResourcesHandler
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x0001D19C File Offset: 0x0001C19C
		private ResourcesHandler()
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001D1B4 File Offset: 0x0001C1B4
		public static string getMessage(string messageOrKey, object[] arguments)
		{
			return ResourcesHandler.getMessage(messageOrKey, arguments, null);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001D1D0 File Offset: 0x0001C1D0
		public static string getMessage(string messageOrKey, object[] arguments, CultureInfo locale)
		{
			if (ResourcesHandler.defaultMessages == null)
			{
				ResourcesHandler.defaultMessages = new ResourceManager("Ldap2._1._2.ExceptionMessages", Assembly.GetExecutingAssembly());
			}
			if (ResourcesHandler.defaultLocale == null)
			{
				ResourcesHandler.defaultLocale = Thread.CurrentThread.CurrentUICulture;
			}
			if (locale == null)
			{
				locale = ResourcesHandler.defaultLocale;
			}
			if (messageOrKey == null)
			{
				messageOrKey = "";
			}
			string text;
			try
			{
				text = ResourcesHandler.defaultMessages.GetString(messageOrKey, locale);
			}
			catch (MissingManifestResourceException ex)
			{
				text = messageOrKey;
			}
			if (arguments != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat(text, arguments);
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001D270 File Offset: 0x0001C270
		public static string getResultString(int code)
		{
			return ResourcesHandler.getResultString(code, null);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001D288 File Offset: 0x0001C288
		public static string getResultString(int code, CultureInfo locale)
		{
			if (ResourcesHandler.defaultResultCodes == null)
			{
				ResourcesHandler.defaultResultCodes = new ResourceManager("ResultCodeMessages", Assembly.GetExecutingAssembly());
			}
			if (ResourcesHandler.defaultLocale == null)
			{
				ResourcesHandler.defaultLocale = Thread.CurrentThread.CurrentUICulture;
			}
			if (locale == null)
			{
				locale = ResourcesHandler.defaultLocale;
			}
			string result;
			try
			{
				result = ResourcesHandler.defaultResultCodes.GetString(Convert.ToString(code), ResourcesHandler.defaultLocale);
			}
			catch (ArgumentNullException ex)
			{
				result = ResourcesHandler.getMessage("UNKNOWN_RESULT", new object[]
				{
					code
				}, locale);
			}
			return result;
		}

		// Token: 0x0400048C RID: 1164
		private static ResourceManager defaultResultCodes = null;

		// Token: 0x0400048D RID: 1165
		private static ResourceManager defaultMessages = null;

		// Token: 0x0400048E RID: 1166
		private static string pkg = "Novell.Directory.Ldap.Utilclass.";

		// Token: 0x0400048F RID: 1167
		private static CultureInfo defaultLocale = Thread.CurrentThread.CurrentUICulture;
	}
}
