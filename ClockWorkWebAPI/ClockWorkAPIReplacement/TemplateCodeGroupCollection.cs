using System;
using System.Collections;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000070 RID: 112
	public class TemplateCodeGroupCollection : CollectionBase
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00024790 File Offset: 0x00022990
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x000247A8 File Offset: 0x000229A8
		public string NewPageSeparator
		{
			get
			{
				return this.newPageSeparator;
			}
			set
			{
				this.newPageSeparator = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		public TemplateCodeGroup this[int index]
		{
			get
			{
				return (TemplateCodeGroup)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000247D8 File Offset: 0x000229D8
		public int Add(TemplateCodeGroup templateCodeGroup)
		{
			return base.List.Add(templateCodeGroup);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000365E File Offset: 0x0000185E
		public void Insert(int index, TemplateCodeGroup templateCodeGroup)
		{
			base.List.Insert(index, templateCodeGroup);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000366F File Offset: 0x0000186F
		public void Remove(TemplateCodeGroup templateCodeGroup)
		{
			base.List.Remove(templateCodeGroup);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000247F8 File Offset: 0x000229F8
		public bool Contains(TemplateCodeGroup templateCodeGroup)
		{
			return base.List.Contains(templateCodeGroup);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00024818 File Offset: 0x00022A18
		public TemplateCode FindTemplateCode(string codeName)
		{
			string text = codeName.ToLower();
			foreach (object obj in base.List)
			{
				TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj;
				foreach (object obj2 in templateCodeGroup.SubCodes)
				{
					TemplateCode templateCode = (TemplateCode)obj2;
					bool flag = text.CompareTo(templateCode.CodeName_lcase) == 0;
					if (flag)
					{
						return templateCode;
					}
				}
			}
			return null;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000248E8 File Offset: 0x00022AE8
		public ArrayList GetAllTemplateCodes()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in base.List)
			{
				TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj;
				foreach (object obj2 in templateCodeGroup.SubCodes)
				{
					TemplateCode value = (TemplateCode)obj2;
					arrayList.Add(value);
				}
			}
			return arrayList;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000249A4 File Offset: 0x00022BA4
		public ArrayList FindTemplateCodesStartingWith(string codeNamePrefix)
		{
			ArrayList arrayList = new ArrayList();
			string value = codeNamePrefix.ToLower();
			foreach (object obj in base.List)
			{
				TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj;
				foreach (object obj2 in templateCodeGroup.SubCodes)
				{
					TemplateCode templateCode = (TemplateCode)obj2;
					bool flag = templateCode.CodeName_lcase.IndexOf(value) == 0;
					if (flag)
					{
						arrayList.Add(templateCode);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00024A80 File Offset: 0x00022C80
		public TemplateCodeGroupCollection Copy()
		{
			bool flag = base.Count > 0;
			TemplateCodeGroupCollection result;
			if (flag)
			{
				TemplateCodeGroupCollection templateCodeGroupCollection = new TemplateCodeGroupCollection();
				templateCodeGroupCollection.NewPageSeparator = this.newPageSeparator;
				foreach (object obj in base.List)
				{
					TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj;
					TemplateCodeGroup templateCodeGroup2 = templateCodeGroup.Copy();
					templateCodeGroupCollection.Add(templateCodeGroup2);
				}
				result = templateCodeGroupCollection;
			}
			else
			{
				result = new TemplateCodeGroupCollection();
			}
			return result;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00024B1C File Offset: 0x00022D1C
		public override string ToString()
		{
			string text = "";
			foreach (object obj in base.List)
			{
				TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj;
				text = text + "GROUP" + Environment.NewLine;
				string str = new string(' ', 5);
				foreach (object obj2 in templateCodeGroup.SubCodes)
				{
					TemplateCode templateCode = (TemplateCode)obj2;
					text = text + str + templateCode.CodeName + Environment.NewLine;
				}
			}
			return text;
		}

		// Token: 0x040002F8 RID: 760
		private string newPageSeparator = "\\p";
	}
}
