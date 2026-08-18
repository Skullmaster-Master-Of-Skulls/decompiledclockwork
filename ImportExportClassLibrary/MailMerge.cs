using System;
using System.Collections;
using System.Collections.Generic;
using MailMerging;

namespace ImportExportClassLibrary
{
	// Token: 0x02000023 RID: 35
	public class MailMerge
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000063E0 File Offset: 0x000053E0
		public static List<MailMergeCode> FromTemplateCodeGroupCollection(TemplateCodeGroupCollection codesMultiple)
		{
			List<MailMergeCode> list = new List<MailMergeCode>();
			foreach (object obj in codesMultiple)
			{
				TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj;
				foreach (object obj2 in templateCodeGroup.SubCodes)
				{
					TemplateCode templateCode = (TemplateCode)obj2;
					MailMergeCode item = new MailMergeCode(templateCode.CodeName_lcase);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006498 File Offset: 0x00005498
		public static ArrayList ToTemplateCodeGroupCollections(List<List<MailMergeCodeValue>> pages)
		{
			ArrayList arrayList = new ArrayList(pages.Count);
			foreach (List<MailMergeCodeValue> list in pages)
			{
				TemplateCodeGroupCollection templateCodeGroupCollection = new TemplateCodeGroupCollection();
				foreach (MailMergeCodeValue mailMergeCodeValue in list)
				{
					TemplateCode templateCode = new TemplateCode(mailMergeCodeValue.Code.Name, mailMergeCodeValue.Code.Name, "", null, typeof(string), "", "");
					templateCode.CodeValueString = mailMergeCodeValue.Value.ValueToString;
					templateCodeGroupCollection.Add(new TemplateCodeGroup(mailMergeCodeValue.Code.Name, 0L, 0L)
					{
						SubCodes = new TemplateCodeCollection(),
						SubCodes = 
						{
							templateCode
						}
					});
				}
				arrayList.Add(templateCodeGroupCollection);
			}
			return arrayList;
		}
	}
}
