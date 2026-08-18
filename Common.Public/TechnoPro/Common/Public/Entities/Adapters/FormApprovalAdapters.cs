using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005C1 RID: 1473
	public static class FormApprovalAdapters
	{
		// Token: 0x06002F79 RID: 12153 RVA: 0x000361D8 File Offset: 0x000343D8
		public static string FormApprovalOptionsToXml(this IList<FormApprovalOptions> formApprovalOptions)
		{
			bool flag = formApprovalOptions == null || formApprovalOptions.Count < 1;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
				object[] array = new object[1];
				array[0] = new XElement("formapprovaloptions", formApprovalOptions.Select(delegate(FormApprovalOptions options)
				{
					XName name = "option";
					object[] array2 = new object[4];
					array2[0] = new XAttribute("isenabled", options.IsEnabled ? "1" : "0");
					array2[1] = new XAttribute("screennum", options.ScreenNum.ToString());
					int num = 2;
					XName name2 = "supervisorgids";
					string separator = ",";
					int[] supervisorGroupIds = options.SupervisorGroupIds;
					string[] array3;
					if (supervisorGroupIds == null)
					{
						array3 = null;
					}
					else
					{
						array3 = (from g in supervisorGroupIds
						select g.ToString()).ToArray<string>();
					}
					array2[num] = new XAttribute(name2, string.Join(separator, array3 ?? new string[0]));
					int num2 = 3;
					XName name3 = "exemptgids";
					string separator2 = ",";
					int[] exemptGroupIds = options.ExemptGroupIds;
					string[] array4;
					if (exemptGroupIds == null)
					{
						array4 = null;
					}
					else
					{
						array4 = (from g in exemptGroupIds
						select g.ToString()).ToArray<string>();
					}
					array2[num2] = new XAttribute(name3, string.Join(separator2, array4 ?? new string[0]));
					return new XElement(name, array2);
				}));
				XDocument xdocument = new XDocument(declaration, array);
				result = xdocument.ToString();
			}
			return result;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x00036264 File Offset: 0x00034464
		private static int ParseIntSafe(string s)
		{
			int num;
			bool flag = string.IsNullOrEmpty(s) || !int.TryParse(s, out num);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x00036298 File Offset: 0x00034498
		public static IList<FormApprovalOptions> XmlToFormApprovalOptions(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			IList<FormApprovalOptions> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					XDocument xdocument = XDocument.Parse(xml);
					result = (from el in xdocument.Descendants("option")
					let xEnabled = el.Attribute("isenabled")
					let xScreenNum = el.Attribute("screennum")
					let xSup = el.Attribute("supervisorgids")
					select new
					{
						<>h__TransparentIdentifier2 = <>h__TransparentIdentifier2,
						xEx = el.Attribute("exemptgids")
					}).Select(delegate(<>h__TransparentIdentifier3)
					{
						FormApprovalOptions formApprovalOptions = new FormApprovalOptions();
						FormApprovalOptions formApprovalOptions2 = formApprovalOptions;
						XAttribute xEnabled = <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.xEnabled;
						formApprovalOptions2.IsEnabled = (((xEnabled != null) ? xEnabled.Value : null) == "1");
						FormApprovalOptions formApprovalOptions3 = formApprovalOptions;
						XAttribute xScreenNum = <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.xScreenNum;
						formApprovalOptions3.ScreenNum = FormApprovalAdapters.ParseIntSafe(((xScreenNum != null) ? xScreenNum.Value : null) ?? "");
						FormApprovalOptions formApprovalOptions4 = formApprovalOptions;
						XAttribute xSup = <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xSup;
						formApprovalOptions4.SupervisorGroupIds = (((xSup != null) ? xSup.Value : null) ?? "").Split(new char[]
						{
							','
						}).Select(new Func<string, int>(FormApprovalAdapters.ParseIntSafe)).ToArray<int>();
						FormApprovalOptions formApprovalOptions5 = formApprovalOptions;
						XAttribute xEx = <>h__TransparentIdentifier3.xEx;
						formApprovalOptions5.ExemptGroupIds = (((xEx != null) ? xEx.Value : null) ?? "").Split(new char[]
						{
							','
						}).Select(new Func<string, int>(FormApprovalAdapters.ParseIntSafe)).ToArray<int>();
						return formApprovalOptions;
					}).ToList<FormApprovalOptions>();
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
