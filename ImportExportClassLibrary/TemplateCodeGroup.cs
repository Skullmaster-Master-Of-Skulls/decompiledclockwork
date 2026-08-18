using System;
using System.Collections;

namespace ImportExportClassLibrary
{
	// Token: 0x0200003E RID: 62
	public class TemplateCodeGroup
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000178D8 File Offset: 0x000168D8
		// (set) Token: 0x06000233 RID: 563 RVA: 0x000178E0 File Offset: 0x000168E0
		public long StartIndex
		{
			get
			{
				return this.startIndex;
			}
			set
			{
				this.startIndex = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000178E9 File Offset: 0x000168E9
		// (set) Token: 0x06000235 RID: 565 RVA: 0x000178F1 File Offset: 0x000168F1
		public long EndIndex
		{
			get
			{
				return this.endIndex;
			}
			set
			{
				this.endIndex = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000178FA File Offset: 0x000168FA
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00017902 File Offset: 0x00016902
		public TemplateCodeCollection SubCodes
		{
			get
			{
				return this.subCodes;
			}
			set
			{
				this.subCodes = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0001790B File Offset: 0x0001690B
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00017913 File Offset: 0x00016913
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0001791C File Offset: 0x0001691C
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00017924 File Offset: 0x00016924
		public string Suffix
		{
			get
			{
				return this.suffix;
			}
			set
			{
				this.suffix = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0001792D File Offset: 0x0001692D
		public char Char1a
		{
			get
			{
				return this.char1a;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00017935 File Offset: 0x00016935
		public char Char1b
		{
			get
			{
				return this.char1b;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0001793D File Offset: 0x0001693D
		public char Char2a
		{
			get
			{
				return this.char2a;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00017945 File Offset: 0x00016945
		public char Char2b
		{
			get
			{
				return this.char2b;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0001794D File Offset: 0x0001694D
		public string Char1ab
		{
			get
			{
				return this.char1a + this.char1b;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0001796A File Offset: 0x0001696A
		public string Char2ab
		{
			get
			{
				return this.char2a + this.char2b;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00017987 File Offset: 0x00016987
		public string RawCode
		{
			get
			{
				return this.rawCode;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0001798F File Offset: 0x0001698F
		public string PrefixSuffixStartCode
		{
			get
			{
				return this.prefixSuffixStartCode;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00017997 File Offset: 0x00016997
		public string PrefixSuffixEndCode
		{
			get
			{
				return this.prefixSuffixEndCode;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0001799F File Offset: 0x0001699F
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000179A7 File Offset: 0x000169A7
		public string GroupPrefix
		{
			get
			{
				return this.groupPrefix;
			}
			set
			{
				this.groupPrefix = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000179B0 File Offset: 0x000169B0
		// (set) Token: 0x06000248 RID: 584 RVA: 0x000179B8 File Offset: 0x000169B8
		public string GroupSuffix
		{
			get
			{
				return this.groupSuffix;
			}
			set
			{
				this.groupSuffix = value;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000179C4 File Offset: 0x000169C4
		public TemplateCodeGroup(TemplateCodeGroup tcg)
		{
			this.subCodes = new TemplateCodeCollection();
			this.prefix = tcg.Prefix;
			this.suffix = tcg.Suffix;
			this.groupPrefix = tcg.GroupPrefix;
			this.groupSuffix = tcg.GroupSuffix;
			this.startIndex = tcg.StartIndex;
			this.endIndex = tcg.EndIndex;
			this.rawCode = tcg.RawCode;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00017A6C File Offset: 0x00016A6C
		public TemplateCodeGroup()
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00017AAC File Offset: 0x00016AAC
		public TemplateCodeGroup(string RawCode, long startIndex, long endIndex)
		{
			this.rawCode = RawCode;
			this.startIndex = startIndex;
			this.endIndex = endIndex;
			this.ParseRawCode(RawCode);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00017B14 File Offset: 0x00016B14
		public TemplateCodeGroup(string RawCode, object codeValue, Type codeValueType)
		{
			this.rawCode = RawCode;
			this.startIndex = -1L;
			this.endIndex = -1L;
			this.ParseRawCode(RawCode);
			foreach (object obj in this.subCodes)
			{
				TemplateCode templateCode = (TemplateCode)obj;
				templateCode.CodeValue = codeValue;
				templateCode.CodeDataType = codeValueType;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00017BD0 File Offset: 0x00016BD0
		private void ParseRawCode(string RawCode)
		{
			this.prefix = "";
			this.suffix = "";
			this.rawCode = RawCode;
			string text = RawCode;
			this.groupPrefix = "";
			this.groupSuffix = "";
			if (text.Length > 2 && text.IndexOf(this.prefixSuffixStartCode) == 0)
			{
				int num = text.IndexOf(this.prefixSuffixEndCode, 2);
				int num2 = text.IndexOf(this.prefixSuffixEndCode + this.prefixSuffixEndCode, 2);
				if (num >= 2 && num != num2)
				{
					this.prefix = text.Substring(1, num - 1).Replace("\\n", TemplateCodeGroup.NewLine);
					text = text.Substring(num + 1);
					if (this.prefix.Length > 0 && this.prefix[0] == '{')
					{
						num = this.prefix.IndexOf("}");
						if (num > 1)
						{
							this.groupPrefix = this.prefix.Substring(1, num - 1);
							this.prefix = this.prefix.Substring(num + 1);
						}
					}
				}
			}
			if (text.Length > 2 && text.LastIndexOf(this.prefixSuffixEndCode) == text.Length - 1)
			{
				int num3 = text.LastIndexOf(this.prefixSuffixStartCode);
				int num4 = text.LastIndexOf(this.prefixSuffixStartCode + this.prefixSuffixStartCode);
				if (num3 >= 0 && num3 != num4)
				{
					this.suffix = text.Substring(num3 + 1, text.Length - num3 - 2).Replace("\\n", TemplateCodeGroup.NewLine);
					text = text.Substring(0, num3);
					if (this.suffix.Length > 0 && this.suffix[this.suffix.Length - 1] == '}')
					{
						num3 = this.suffix.IndexOf("{");
						if (num3 >= 0)
						{
							this.groupSuffix = this.suffix.Substring(num3 + 1, this.suffix.Length - num3 - 2);
							this.suffix = this.suffix.Substring(0, num3);
						}
					}
				}
			}
			this.subCodes = new TemplateCodeCollection();
			text = text.Replace(",,", TemplateCode.PlaceHolder);
			string[] array = text.Split(new char[]
			{
				','
			});
			foreach (string text2 in array)
			{
				TemplateCode templateCode = new TemplateCode(text2.Replace(TemplateCode.PlaceHolder, ",").Trim());
				this.subCodes.Add(templateCode);
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00017E68 File Offset: 0x00016E68
		public TemplateCodeGroup Copy()
		{
			TemplateCodeGroup templateCodeGroup = new TemplateCodeGroup(this);
			foreach (object obj in this.subCodes)
			{
				TemplateCode templateCode = (TemplateCode)obj;
				TemplateCode templateCode2 = templateCode.Copy();
				templateCodeGroup.SubCodes.Add(templateCode2);
			}
			return templateCodeGroup;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00017EDC File Offset: 0x00016EDC
		public string GetCodeValue(TemplateCodeGroupCollection codes, int indexOfThisTemplateCodeGroup)
		{
			string str = (this.prefix == null) ? "" : this.prefix;
			string text = (this.suffix == null) ? "" : this.suffix;
			string text2 = "";
			int num = 0;
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.subCodes.Count; i++)
			{
				TemplateCode templateCode = this.subCodes[i];
				string codeName = templateCode.CodeName;
				bool flag = true;
				if (codeName.Length > 1 && codeName[0] == '?')
				{
					string s = codeName.Substring(1);
					try
					{
						int num2 = int.Parse(s);
						int num3 = indexOfThisTemplateCodeGroup + 1;
						int num4 = indexOfThisTemplateCodeGroup + num2;
						if (num3 >= 0 && num4 < codes.Count)
						{
							bool flag2 = false;
							for (int j = num3; j <= num4; j++)
							{
								TemplateCodeGroup templateCodeGroup = codes[j];
								if (templateCodeGroup.GetCodeValue(codes, j).Length > 0)
								{
									flag2 = true;
									break;
								}
							}
							if (flag2)
							{
								arrayList.Add(text);
								flag = false;
							}
						}
					}
					catch
					{
					}
				}
				if (flag)
				{
					string codeValueString = templateCode.GetCodeValueString();
					if (codeValueString.Trim().Length > 0)
					{
						string codeValueString2 = templateCode.GetCodeValueString();
						if (!arrayList.Contains(codeValueString2))
						{
							arrayList.Add(codeValueString2);
							num++;
						}
					}
				}
			}
			for (int k = 0; k < arrayList.Count; k++)
			{
				text2 = text2 + str + (string)arrayList[k] + ((k < arrayList.Count - 1) ? text : "");
			}
			if (text2.Length > 0)
			{
				text2 = this.groupPrefix + text2 + this.groupSuffix;
			}
			return text2;
		}

		// Token: 0x04000122 RID: 290
		private long startIndex;

		// Token: 0x04000123 RID: 291
		private long endIndex;

		// Token: 0x04000124 RID: 292
		private TemplateCodeCollection subCodes;

		// Token: 0x04000125 RID: 293
		private string prefix;

		// Token: 0x04000126 RID: 294
		private string suffix;

		// Token: 0x04000127 RID: 295
		private string groupPrefix;

		// Token: 0x04000128 RID: 296
		private string groupSuffix;

		// Token: 0x04000129 RID: 297
		private char char1a = '#';

		// Token: 0x0400012A RID: 298
		private char char1b = '<';

		// Token: 0x0400012B RID: 299
		private char char2a = '>';

		// Token: 0x0400012C RID: 300
		private char char2b = '#';

		// Token: 0x0400012D RID: 301
		private string rawCode;

		// Token: 0x0400012E RID: 302
		private string prefixSuffixStartCode = "[";

		// Token: 0x0400012F RID: 303
		private string prefixSuffixEndCode = "]";

		// Token: 0x04000130 RID: 304
		private static string NewLine = '\r'.ToString();
	}
}
