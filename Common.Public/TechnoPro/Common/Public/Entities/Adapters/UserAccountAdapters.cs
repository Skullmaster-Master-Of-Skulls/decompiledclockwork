using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.UserAccount;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005CF RID: 1487
	public static class UserAccountAdapters
	{
		// Token: 0x06002FD8 RID: 12248 RVA: 0x0003AB34 File Offset: 0x00038D34
		public static string ConvertToSummaryString(this PasswordPolicy policy)
		{
			bool flag = policy == null || !policy.EnforcePasswordPolicy;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag2 = policy.MinimumLengthTotal > 0;
				if (flag2)
				{
					stringBuilder.AppendFormat("MinimumLengthTotal={0}, ", policy.MinimumLengthTotal.ToString());
				}
				bool flag3 = policy.MinimumLengthLowercase > 0;
				if (flag3)
				{
					stringBuilder.AppendFormat("MinimumLengthLowercase={0}, ", policy.MinimumLengthLowercase.ToString());
				}
				bool flag4 = policy.MinimumLengthUppercase > 0;
				if (flag4)
				{
					stringBuilder.AppendFormat("MinimumLengthUppercase={0}, ", policy.MinimumLengthUppercase.ToString());
				}
				bool flag5 = policy.MinimumLengthNumeric > 0;
				if (flag5)
				{
					stringBuilder.AppendFormat("MinimumLengthNumeric={0}, ", policy.MinimumLengthNumeric.ToString());
				}
				bool flag6 = policy.MinimumLengthSpecialCharacter > 0;
				if (flag6)
				{
					stringBuilder.AppendFormat("MinimumLengthSpecialCharacter={0}, ", policy.MinimumLengthSpecialCharacter.ToString());
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x0003AC40 File Offset: 0x00038E40
		public static PasswordPolicy ParsePasswordPolicy(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			PasswordPolicy result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument = XDocument.Parse(xml);
				IList<PasswordPolicy> list = (from g in xdocument.Root.Elements("passwordpolicies").Elements("passwordpolicy")
				select new PasswordPolicy
				{
					MinimumLengthTotal = UserAccountAdapters.GetIntFromAttribute(g, "MinimumLengthTotal", 0),
					MinimumLengthLowercase = UserAccountAdapters.GetIntFromAttribute(g, "MinimumLengthLowercase", 0),
					MinimumLengthUppercase = UserAccountAdapters.GetIntFromAttribute(g, "MinimumLengthUppercase", 0),
					MinimumLengthSpecialCharacter = UserAccountAdapters.GetIntFromAttribute(g, "MinimumLengthSpecialCharacter", 0),
					MinimumLengthNumeric = UserAccountAdapters.GetIntFromAttribute(g, "MinimumLengthNumeric", 0),
					NumPreviousPasswordsCantUse = UserAccountAdapters.GetIntFromAttribute(g, "NumPreviousPasswordsCantUse", 0),
					AutoPasswordExpiryNumDays = UserAccountAdapters.GetIntFromAttribute(g, "AutoPasswordExpiryNumDays", 0),
					MaxFailedAttempts = UserAccountAdapters.GetIntFromAttribute(g, "MaxFailedAttempts", 0),
					LockoutDurationMinutes = UserAccountAdapters.GetIntFromAttribute(g, "LockoutDurationMinutes", 0),
					EnforcePasswordPolicy = (UserAccountAdapters.GetIntFromAttribute(g, "EnforcePasswordPolicy", 0) == 1)
				}).ToList<PasswordPolicy>();
				result = ((list.Count > 0) ? list[0] : null);
			}
			return result;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x0003ACC8 File Offset: 0x00038EC8
		public static string ConvertToXml(this PasswordPolicy policy)
		{
			bool flag = policy == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
				{
					new XElement("passwordpolicies", new XElement("passwordpolicy", new object[]
					{
						new XAttribute("MinimumLengthTotal", policy.MinimumLengthTotal),
						new XAttribute("MinimumLengthLowercase", policy.MinimumLengthLowercase),
						new XAttribute("MinimumLengthUppercase", policy.MinimumLengthUppercase),
						new XAttribute("MinimumLengthSpecialCharacter", policy.MinimumLengthSpecialCharacter),
						new XAttribute("MinimumLengthNumeric", policy.MinimumLengthNumeric),
						new XAttribute("NumPreviousPasswordsCantUse", policy.NumPreviousPasswordsCantUse),
						new XAttribute("AutoPasswordExpiryNumDays", policy.AutoPasswordExpiryNumDays),
						new XAttribute("MaxFailedAttempts", policy.MaxFailedAttempts),
						new XAttribute("LockoutDurationMinutes", policy.LockoutDurationMinutes),
						new XAttribute("EnforcePasswordPolicy", policy.EnforcePasswordPolicy ? 1 : 0)
					}))
				});
				result = xdocument.Declaration.ToString() + xdocument.ToString();
			}
			return result;
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x0003AE74 File Offset: 0x00039074
		private static int GetIntFromAttribute(XElement element, string attributeName, int defaultValue = 0)
		{
			bool flag = element == null;
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					int num;
					bool flag3 = !int.TryParse(xattribute.Value, out num);
					if (flag3)
					{
						result = defaultValue;
					}
					else
					{
						result = num;
					}
				}
			}
			return result;
		}
	}
}
