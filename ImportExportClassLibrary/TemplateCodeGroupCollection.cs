using System;
using System.Collections;

namespace ImportExportClassLibrary
{
	// Token: 0x0200003F RID: 63
	public class TemplateCodeGroupCollection : CollectionBase
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000180B8 File Offset: 0x000170B8
		// (set) Token: 0x06000252 RID: 594 RVA: 0x000180C0 File Offset: 0x000170C0
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

		// Token: 0x1700003C RID: 60
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

		// Token: 0x06000255 RID: 597 RVA: 0x000180EB File Offset: 0x000170EB
		public int Add(TemplateCodeGroup templateCodeGroup)
		{
			return base.List.Add(templateCodeGroup);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000180F9 File Offset: 0x000170F9
		public void Insert(int index, TemplateCodeGroup templateCodeGroup)
		{
			base.List.Insert(index, templateCodeGroup);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00018108 File Offset: 0x00017108
		public void Remove(TemplateCodeGroup templateCodeGroup)
		{
			base.List.Remove(templateCodeGroup);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00018116 File Offset: 0x00017116
		public bool Contains(TemplateCodeGroup templateCodeGroup)
		{
			return base.List.Contains(templateCodeGroup);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00018124 File Offset: 0x00017124
		public TemplateCode FindTemplateCode(string codeName)
		{
			string text = codeName.ToLower();
			foreach (object obj in base.List)
			{
				TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj;
				foreach (object obj2 in templateCodeGroup.SubCodes)
				{
					TemplateCode templateCode = (TemplateCode)obj2;
					if (text.CompareTo(templateCode.CodeName_lcase) == 0)
					{
						return templateCode;
					}
				}
			}
			return null;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000181E4 File Offset: 0x000171E4
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

		// Token: 0x0600025B RID: 603 RVA: 0x00018290 File Offset: 0x00017290
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
					if (templateCode.CodeName_lcase.IndexOf(value) == 0)
					{
						arrayList.Add(templateCode);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00018358 File Offset: 0x00017358
		public TemplateCodeGroupCollection Copy()
		{
			if (base.Count > 0)
			{
				TemplateCodeGroupCollection templateCodeGroupCollection = new TemplateCodeGroupCollection();
				templateCodeGroupCollection.NewPageSeparator = this.newPageSeparator;
				foreach (object obj in base.List)
				{
					TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj;
					TemplateCodeGroup templateCodeGroup2 = templateCodeGroup.Copy();
					templateCodeGroupCollection.Add(templateCodeGroup2);
				}
				return templateCodeGroupCollection;
			}
			return new TemplateCodeGroupCollection();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000183E0 File Offset: 0x000173E0
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

		// Token: 0x04000131 RID: 305
		private string newPageSeparator = "\\p";
	}
}
