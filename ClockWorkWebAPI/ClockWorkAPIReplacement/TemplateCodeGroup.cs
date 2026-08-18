using System;
using System.Collections;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200006F RID: 111
	public class TemplateCodeGroup
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00023DC4 File Offset: 0x00021FC4
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00023DDC File Offset: 0x00021FDC
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

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00023DE8 File Offset: 0x00021FE8
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00023E00 File Offset: 0x00022000
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

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00023E0C File Offset: 0x0002200C
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00023E24 File Offset: 0x00022024
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

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00023E30 File Offset: 0x00022030
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00023E48 File Offset: 0x00022048
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

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00023E54 File Offset: 0x00022054
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x00023E6C File Offset: 0x0002206C
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

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00023E78 File Offset: 0x00022078
		public char Char1a
		{
			get
			{
				return this.char1a;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00023E90 File Offset: 0x00022090
		public char Char1b
		{
			get
			{
				return this.char1b;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00023EA8 File Offset: 0x000220A8
		public char Char2a
		{
			get
			{
				return this.char2a;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00023EC0 File Offset: 0x000220C0
		public char Char2b
		{
			get
			{
				return this.char2b;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00023ED8 File Offset: 0x000220D8
		public string Char1ab
		{
			get
			{
				return this.char1a.ToString() + this.char1b.ToString();
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00023F08 File Offset: 0x00022108
		public string Char2ab
		{
			get
			{
				return this.char2a.ToString() + this.char2b.ToString();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00023F38 File Offset: 0x00022138
		public string RawCode
		{
			get
			{
				return this.rawCode;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00023F50 File Offset: 0x00022150
		public string PrefixSuffixStartCode
		{
			get
			{
				return this.prefixSuffixStartCode;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00023F68 File Offset: 0x00022168
		public string PrefixSuffixEndCode
		{
			get
			{
				return this.prefixSuffixEndCode;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00023F80 File Offset: 0x00022180
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00023F98 File Offset: 0x00022198
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

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00023FA4 File Offset: 0x000221A4
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00023FBC File Offset: 0x000221BC
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

		// Token: 0x06000592 RID: 1426 RVA: 0x00023FC8 File Offset: 0x000221C8
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

		// Token: 0x06000593 RID: 1427 RVA: 0x00024072 File Offset: 0x00022272
		public TemplateCodeGroup()
		{
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000240B4 File Offset: 0x000222B4
		public TemplateCodeGroup(string RawCode, long startIndex, long endIndex)
		{
			this.rawCode = RawCode;
			this.startIndex = startIndex;
			this.endIndex = endIndex;
			this.ParseRawCode(RawCode);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002411C File Offset: 0x0002231C
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

		// Token: 0x06000596 RID: 1430 RVA: 0x000241E0 File Offset: 0x000223E0
		private void ParseRawCode(string RawCode)
		{
			this.prefix = "";
			this.suffix = "";
			this.rawCode = RawCode;
			string text = RawCode;
			this.groupPrefix = "";
			this.groupSuffix = "";
			bool flag = text.Length > 2 && text.IndexOf(this.prefixSuffixStartCode) == 0;
			if (flag)
			{
				int num = text.IndexOf(this.prefixSuffixEndCode, 2);
				int num2 = text.IndexOf(this.prefixSuffixEndCode + this.prefixSuffixEndCode, 2);
				bool flag2 = num >= 2 && num != num2;
				if (flag2)
				{
					this.prefix = text.Substring(1, num - 1).Replace("\\n", TemplateCodeGroup.NewLine);
					text = text.Substring(num + 1);
					bool flag3 = this.prefix.Length > 0 && this.prefix[0] == '{';
					if (flag3)
					{
						num = this.prefix.IndexOf("}");
						bool flag4 = num > 1;
						if (flag4)
						{
							this.groupPrefix = this.prefix.Substring(1, num - 1);
							this.prefix = this.prefix.Substring(num + 1);
						}
					}
				}
			}
			bool flag5 = text.Length > 2 && text.LastIndexOf(this.prefixSuffixEndCode) == text.Length - 1;
			if (flag5)
			{
				int num3 = text.LastIndexOf(this.prefixSuffixStartCode);
				int num4 = text.LastIndexOf(this.prefixSuffixStartCode + this.prefixSuffixStartCode);
				bool flag6 = num3 >= 0 && num3 != num4;
				if (flag6)
				{
					this.suffix = text.Substring(num3 + 1, text.Length - num3 - 2).Replace("\\n", TemplateCodeGroup.NewLine);
					text = text.Substring(0, num3);
					bool flag7 = this.suffix.Length > 0 && this.suffix[this.suffix.Length - 1] == '}';
					if (flag7)
					{
						num3 = this.suffix.IndexOf("{");
						bool flag8 = num3 >= 0;
						if (flag8)
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

		// Token: 0x06000597 RID: 1431 RVA: 0x000244D0 File Offset: 0x000226D0
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

		// Token: 0x06000598 RID: 1432 RVA: 0x0002454C File Offset: 0x0002274C
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
				bool flag2 = codeName.Length > 1 && codeName[0] == '?';
				if (flag2)
				{
					string s = codeName.Substring(1);
					try
					{
						int num2 = int.Parse(s);
						int num3 = indexOfThisTemplateCodeGroup + 1;
						int num4 = indexOfThisTemplateCodeGroup + num2;
						bool flag3 = num3 >= 0 && num4 < codes.Count;
						if (flag3)
						{
							bool flag4 = false;
							for (int j = num3; j <= num4; j++)
							{
								TemplateCodeGroup templateCodeGroup = codes[j];
								bool flag5 = templateCodeGroup.GetCodeValue(codes, j).Length > 0;
								if (flag5)
								{
									flag4 = true;
									break;
								}
							}
							bool flag6 = flag4;
							if (flag6)
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
				bool flag7 = flag;
				if (flag7)
				{
					string codeValueString = templateCode.GetCodeValueString();
					bool flag8 = codeValueString.Trim().Length > 0;
					if (flag8)
					{
						string codeValueString2 = templateCode.GetCodeValueString();
						bool flag9 = !arrayList.Contains(codeValueString2);
						if (flag9)
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
			bool flag10 = text2.Length > 0;
			if (flag10)
			{
				text2 = this.groupPrefix + text2 + this.groupSuffix;
			}
			return text2;
		}

		// Token: 0x040002E9 RID: 745
		private long startIndex;

		// Token: 0x040002EA RID: 746
		private long endIndex;

		// Token: 0x040002EB RID: 747
		private TemplateCodeCollection subCodes;

		// Token: 0x040002EC RID: 748
		private string prefix;

		// Token: 0x040002ED RID: 749
		private string suffix;

		// Token: 0x040002EE RID: 750
		private string groupPrefix;

		// Token: 0x040002EF RID: 751
		private string groupSuffix;

		// Token: 0x040002F0 RID: 752
		private char char1a = '#';

		// Token: 0x040002F1 RID: 753
		private char char1b = '<';

		// Token: 0x040002F2 RID: 754
		private char char2a = '>';

		// Token: 0x040002F3 RID: 755
		private char char2b = '#';

		// Token: 0x040002F4 RID: 756
		private string rawCode;

		// Token: 0x040002F5 RID: 757
		private string prefixSuffixStartCode = "[";

		// Token: 0x040002F6 RID: 758
		private string prefixSuffixEndCode = "]";

		// Token: 0x040002F7 RID: 759
		private static string NewLine = '\r'.ToString();
	}
}
