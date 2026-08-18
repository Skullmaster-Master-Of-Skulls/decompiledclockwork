using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.Membership.LoginMethods;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005C5 RID: 1477
	public static class MembershipLoginMethodsAdapter
	{
		// Token: 0x06002F83 RID: 12163 RVA: 0x00036894 File Offset: 0x00034A94
		public static LoginMethodActiveDirectorySettings ActiveDirectorySettingsFromXml(this string xml)
		{
			try
			{
				XDocument xdocument = XDocument.Parse(xml ?? "");
				return (from x in xdocument.Descendants("activedirectoryloginmethod")
				let domain = x.Element("domain")
				let fallback = x.Element("noclockworkfallback")
				let fallbackStr = (fallback == null) ? "" : (fallback.Value ?? "").Trim()
				select new LoginMethodActiveDirectorySettings
				{
					Domain = ((domain == null) ? "" : (domain.Value ?? "")),
					DontAllowFallbackToClockWorkUsernamePasswordCheck = (fallbackStr == "1")
				}).FirstOrDefault<LoginMethodActiveDirectorySettings>();
			}
			catch (Exception ex)
			{
			}
			return new LoginMethodActiveDirectorySettings();
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x0003697C File Offset: 0x00034B7C
		public static string XmlFromActiveDirectorySettings(this LoginMethodActiveDirectorySettings activeDirectorySettings)
		{
			bool flag = activeDirectorySettings == null;
			if (flag)
			{
				activeDirectorySettings = new LoginMethodActiveDirectorySettings();
			}
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
			{
				new XElement("activedirectoryloginmethod", new object[]
				{
					new XElement("domain", activeDirectorySettings.Domain ?? ""),
					new XElement("noclockworkfallback", activeDirectorySettings.DontAllowFallbackToClockWorkUsernamePasswordCheck ? "1" : "0")
				})
			});
			return xdocument.ToString();
		}
	}
}
