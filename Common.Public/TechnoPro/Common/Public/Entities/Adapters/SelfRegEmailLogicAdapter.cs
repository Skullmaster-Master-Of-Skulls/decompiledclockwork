using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegEmail;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005CC RID: 1484
	public static class SelfRegEmailLogicAdapter
	{
		// Token: 0x06002FCB RID: 12235 RVA: 0x0003A1DC File Offset: 0x000383DC
		public static string SelfRegEmailLogicRulesToXml(this SelfRegEmailLogicRule[] rules)
		{
			bool flag = rules == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
				object[] array = new object[1];
				array[0] = new XElement("rules", rules.Select(delegate(SelfRegEmailLogicRule rule)
				{
					XName name = "rule";
					object[] array2 = new object[9];
					array2[0] = new XAttribute("type", ((int)rule.LogicType).ToString());
					array2[1] = new XAttribute("gid", rule.AuthorizedGroupId.ToString());
					array2[2] = new XAttribute("isdisabled", rule.IsDisabled.ToString());
					array2[3] = new XAttribute("cancelprofemail", rule.CancelProfEmail.ToString());
					array2[4] = new XAttribute("emailtemplateid", rule.EmailTemplateId.ToString());
					array2[5] = new XAttribute("lettertemplateid", rule.LetterTemplateId.ToString());
					array2[6] = new XAttribute("title", rule.Title ?? "");
					int num = 7;
					XName name2 = "datamatchings";
					IList<SelfRegDataFieldMatchingRule> dataMatchingRules = rule.DataMatchingRules;
					object content;
					if (dataMatchingRules == null)
					{
						content = null;
					}
					else
					{
						content = from g in dataMatchingRules
						select new XElement("datamatching", new object[]
						{
							new XAttribute("cid", g.ControlId.ToString()),
							new XAttribute("match", g.MatchingString ?? "")
						});
					}
					array2[num] = new XElement(name2, content);
					int num2 = 8;
					XName name3 = "notifications";
					IList<string> notificationEmails = rule.NotificationEmails;
					object content2;
					if (notificationEmails == null)
					{
						content2 = null;
					}
					else
					{
						content2 = from g in notificationEmails
						select new XElement("notification", new XAttribute("email", g ?? ""));
					}
					array2[num2] = new XElement(name3, content2);
					return new XElement(name, array2);
				}));
				XDocument xdocument = new XDocument(declaration, array);
				result = xdocument.Declaration.ToString() + xdocument.ToString();
			}
			return result;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x0003A26C File Offset: 0x0003846C
		public static SelfRegEmailLogicRule[] XmlToSelfRegEmailLogicRules(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			SelfRegEmailLogicRule[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					return XDocument.Parse(xml).Descendants("rule").Select(delegate(XElement el)
					{
						SelfRegEmailLogicRule selfRegEmailLogicRule = new SelfRegEmailLogicRule();
						XAttribute xattribute = el.Attribute("type");
						selfRegEmailLogicRule.LogicType = ((xattribute != null) ? xattribute.GetEnumFromAttributeInt(eSelfRegEmailLogicType.Unknown) : eSelfRegEmailLogicType.Unknown);
						XAttribute xattribute2 = el.Attribute("isdisabled");
						selfRegEmailLogicRule.IsDisabled = (xattribute2 != null && xattribute2.GetBoolFromAttribute(false));
						XAttribute xattribute3 = el.Attribute("cancelprofemail");
						selfRegEmailLogicRule.CancelProfEmail = (xattribute3 != null && xattribute3.GetBoolFromAttribute(false));
						XAttribute xattribute4 = el.Attribute("emailtemplateid");
						selfRegEmailLogicRule.EmailTemplateId = ((xattribute4 != null) ? xattribute4.GetIntFromAttribute(0) : 0);
						XAttribute xattribute5 = el.Attribute("lettertemplateid");
						selfRegEmailLogicRule.LetterTemplateId = ((xattribute5 != null) ? xattribute5.GetIntFromAttribute(0) : 0);
						XAttribute xattribute6 = el.Attribute("gid");
						selfRegEmailLogicRule.AuthorizedGroupId = ((xattribute6 != null) ? xattribute6.GetIntFromAttribute(0) : 0);
						XAttribute xattribute7 = el.Attribute("title");
						selfRegEmailLogicRule.Title = ((xattribute7 != null) ? xattribute7.GetStringFromAttribute() : null);
						selfRegEmailLogicRule.DataMatchingRules = el.Descendants("datamatching").Select(delegate(XElement mr)
						{
							SelfRegDataFieldMatchingRule selfRegDataFieldMatchingRule = new SelfRegDataFieldMatchingRule();
							XAttribute xattribute8 = mr.Attribute("cid");
							selfRegDataFieldMatchingRule.ControlId = ((xattribute8 != null) ? xattribute8.GetIntFromAttribute(0) : 0);
							XAttribute xattribute9 = mr.Attribute("match");
							selfRegDataFieldMatchingRule.MatchingString = ((xattribute9 != null) ? xattribute9.GetStringFromAttribute() : null);
							return selfRegDataFieldMatchingRule;
						}).ToList<SelfRegDataFieldMatchingRule>();
						selfRegEmailLogicRule.NotificationEmails = (from ne in el.Descendants("notification")
						select ne.Attribute("email").GetStringFromAttribute()).ToList<string>();
						return selfRegEmailLogicRule;
					}).ToArray<SelfRegEmailLogicRule>();
				}
				catch
				{
				}
				result = null;
			}
			return result;
		}
	}
}
