using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Public.Entities.Settings.Adapters
{
	// Token: 0x020001DD RID: 477
	public static class TestBookingSettingsAdapters
	{
		// Token: 0x06000DB8 RID: 3512 RVA: 0x000159F8 File Offset: 0x00013BF8
		public static IDictionary<string, int> GetCampusesWithStudentEmailTemplateIdsFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			IDictionary<string, int> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument = XDocument.Parse(xml);
				var enumerable = from g in xdocument.Root.Elements("item")
				let gCampus = g.Attribute("campus")
				let gtid = g.Attribute("templateid")
				select new
				{
					Campus = (gCampus.GetStringFromAttribute() ?? "").Trim().ToLower(),
					TemplateId = gtid.GetIntFromAttribute(0)
				};
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (var <>f__AnonymousType in enumerable)
				{
					bool flag2 = <>f__AnonymousType.Campus.Length < 1 || <>f__AnonymousType.TemplateId < 1 || dictionary.ContainsKey(<>f__AnonymousType.Campus);
					if (!flag2)
					{
						dictionary.Add(<>f__AnonymousType.Campus, <>f__AnonymousType.TemplateId);
					}
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00015B34 File Offset: 0x00013D34
		public static string GetXmlFromCampusesWithStudentEmailTemplateIds(this IDictionary<string, int> campusesWithStudentEmailTemplateIds)
		{
			bool flag = campusesWithStudentEmailTemplateIds == null || campusesWithStudentEmailTemplateIds.Count < 1;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
				object[] array = new object[1];
				array[0] = new XElement("CampusesWithStudentEmailTemplateIds", campusesWithStudentEmailTemplateIds.Select(delegate(KeyValuePair<string, int> kvp)
				{
					XName name = "item";
					object[] array2 = new object[2];
					int num = 0;
					XName name2 = "campus";
					KeyValuePair<string, int> keyValuePair = kvp;
					array2[num] = new XAttribute(name2, (keyValuePair.Key ?? "").Trim().ToLower());
					int num2 = 1;
					XName name3 = "templateid";
					keyValuePair = kvp;
					array2[num2] = new XAttribute(name3, keyValuePair.Value.ToString());
					return new XElement(name, array2);
				}));
				XDocument xdocument = new XDocument(declaration, array);
				result = xdocument.ToString();
			}
			return result;
		}
	}
}
