using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.UI.Web.Entity.WebLogin;

namespace TechnoPro.Common.UI.Web.Entity.Adapters
{
	// Token: 0x02000057 RID: 87
	public static class LoginPageUrlRuleAdapter
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00005C08 File Offset: 0x00003E08
		public static string ToLoginPageUrlRuleXml(this LoginPageUrlRule rule)
		{
			bool flag = ((rule != null) ? rule.LoginUrls : null) == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				List<XAttribute> list = (from g in rule.LoginUrls
				select new XAttribute(g.Key.ToString(), g.Value)).ToList<XAttribute>();
				IDictionary<eWebPageTargetAudience, string> logoutUrls = rule.LogoutUrls;
				IEnumerable<XAttribute> enumerable;
				if (logoutUrls == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = from g in logoutUrls
					select new XAttribute(g.Key.ToString(), g.Value);
				}
				IEnumerable<XAttribute> collection = enumerable ?? new List<XAttribute>().ToList<XAttribute>();
				list.AddRange(collection);
				XDocument xdocument = new XDocument(new object[]
				{
					new XElement("loginurl", list)
				});
				result = xdocument.ToString();
			}
			return result;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00005CD8 File Offset: 0x00003ED8
		public static LoginPageUrlRule LoginPageUrlRuleFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			LoginPageUrlRule result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					XDocument xdocument = XDocument.Parse(xml);
					XElement xelement = xdocument.Element("loginurl");
					IEnumerable<XAttribute> enumerable = (xelement != null) ? xelement.Attributes() : null;
					bool flag2 = enumerable == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						Dictionary<string, eWebPageTargetAudience> dictionary = ((eWebPageTargetAudience[])Enum.GetValues(typeof(eWebPageTargetAudience))).ToDictionary((eWebPageTargetAudience g) => g.ToString().ToLower(), (eWebPageTargetAudience g) => g);
						Dictionary<eWebPageTargetAudience, string> dictionary2 = new Dictionary<eWebPageTargetAudience, string>();
						Dictionary<eWebPageTargetAudience, string> dictionary3 = new Dictionary<eWebPageTargetAudience, string>();
						foreach (XAttribute xattribute in enumerable)
						{
							string text = (xattribute.Value ?? "").Trim();
							bool flag3 = text.Length < 1;
							if (!flag3)
							{
								string text2 = xattribute.Name.ToString().ToLower().Trim();
								bool flag4 = text2.EndsWith("logout");
								bool flag5 = flag4;
								if (flag5)
								{
									text2 = text2.Substring(0, text2.Length - 6);
								}
								eWebPageTargetAudience eWebPageTargetAudience = dictionary.ContainsKey(text2) ? dictionary[text2] : eWebPageTargetAudience.Unknown;
								bool flag6 = eWebPageTargetAudience == eWebPageTargetAudience.Unknown;
								if (!flag6)
								{
									bool flag7 = flag4;
									if (flag7)
									{
										bool flag8 = !dictionary3.ContainsKey(eWebPageTargetAudience);
										if (flag8)
										{
											dictionary3.Add(eWebPageTargetAudience, text);
										}
									}
									else
									{
										bool flag9 = !dictionary2.ContainsKey(eWebPageTargetAudience);
										if (flag9)
										{
											dictionary2.Add(eWebPageTargetAudience, text);
										}
									}
								}
							}
						}
						result = new LoginPageUrlRule
						{
							LoginUrls = dictionary2,
							LogoutUrls = dictionary3
						};
					}
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}
	}
}
