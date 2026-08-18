using System;
using System.Collections;

namespace ImportExportClassLibrary
{
	// Token: 0x02000043 RID: 67
	public class TemplateCode
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0001C009 File Offset: 0x0001B009
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0001C011 File Offset: 0x0001B011
		public string CodeValueString
		{
			get
			{
				return this.codeValueString;
			}
			set
			{
				this.codeValueString = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0001C01A File Offset: 0x0001B01A
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0001C022 File Offset: 0x0001B022
		public string CodeName_lcase
		{
			get
			{
				return this.codeName_lcase;
			}
			set
			{
				this.codeName_lcase = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0001C02B File Offset: 0x0001B02B
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0001C033 File Offset: 0x0001B033
		public string CodeName
		{
			get
			{
				return this.codeName;
			}
			set
			{
				this.codeName = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0001C03C File Offset: 0x0001B03C
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0001C052 File Offset: 0x0001B052
		public object CodeValue
		{
			get
			{
				if (this.codeValue != null)
				{
					return this.codeValue;
				}
				return "";
			}
			set
			{
				this.codeValue = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0001C05B File Offset: 0x0001B05B
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0001C063 File Offset: 0x0001B063
		public Type CodeDataType
		{
			get
			{
				return this.codeDataType;
			}
			set
			{
				this.codeDataType = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0001C06C File Offset: 0x0001B06C
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0001C074 File Offset: 0x0001B074
		public string YesRule
		{
			get
			{
				return this.yesRule;
			}
			set
			{
				this.yesRule = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0001C07D File Offset: 0x0001B07D
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0001C085 File Offset: 0x0001B085
		public string NoRule
		{
			get
			{
				return this.noRule;
			}
			set
			{
				this.noRule = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0001C08E File Offset: 0x0001B08E
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0001C096 File Offset: 0x0001B096
		public string RawCode
		{
			get
			{
				return this.rawCode;
			}
			set
			{
				this.rawCode = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0001C09F File Offset: 0x0001B09F
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0001C0A7 File Offset: 0x0001B0A7
		public bool ShouldntBeEmpty
		{
			get
			{
				return this.shouldntBeEmpty;
			}
			set
			{
				this.shouldntBeEmpty = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0001C0B0 File Offset: 0x0001B0B0
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0001C0B8 File Offset: 0x0001B0B8
		public bool IgnoreForFillCodes
		{
			get
			{
				return this.ignoreForFillCodes;
			}
			set
			{
				this.ignoreForFillCodes = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0001C0C1 File Offset: 0x0001B0C1
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0001C0C9 File Offset: 0x0001B0C9
		public int ControlCode
		{
			get
			{
				return this.controlCode;
			}
			set
			{
				this.controlCode = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0001C0D2 File Offset: 0x0001B0D2
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0001C0DA File Offset: 0x0001B0DA
		public bool IsDataHolding
		{
			get
			{
				return this.isDataHolding;
			}
			set
			{
				this.isDataHolding = value;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0001C0E3 File Offset: 0x0001B0E3
		public TemplateCode Copy()
		{
			return new TemplateCode(this.codeName_lcase, this.codeName, this.rawCode, this.codeValue, this.codeDataType, this.yesRule, this.noRule);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0001C114 File Offset: 0x0001B114
		public TemplateCode(string codeName_lcase, string codeName, string rawCode, object codeValue, Type codeDataType, string yesRule, string noRule)
		{
			this.codeName_lcase = codeName_lcase;
			this.codeName = codeName;
			this.rawCode = rawCode;
			this.codeValue = codeValue;
			this.codeDataType = codeDataType;
			this.yesRule = yesRule;
			this.noRule = noRule;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0001C18C File Offset: 0x0001B18C
		public TemplateCode(string codeName, object codeValue, Type codeDataType)
		{
			this.codeName = codeName;
			this.codeName_lcase = codeName.ToLower();
			this.codeValue = codeValue;
			this.codeDataType = codeDataType;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001C1EF File Offset: 0x0001B1EF
		public void AddAlias(string alias)
		{
			this.aliases.Add(alias.ToLower());
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0001C204 File Offset: 0x0001B204
		public bool Matches(string codeNameLCase)
		{
			if (this.codeName_lcase.CompareTo(codeNameLCase) == 0)
			{
				return true;
			}
			foreach (object obj in this.aliases)
			{
				string text = (string)obj;
				if (text.CompareTo(codeNameLCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001C278 File Offset: 0x0001B278
		public TemplateCode(string RawCode)
		{
			this.rawCode = RawCode;
			this.codeValue = null;
			this.codeDataType = null;
			this.codeName = RawCode;
			this.yesRule = null;
			this.noRule = null;
			int num = this.codeName.LastIndexOf(">");
			if (num >= 0 && num == this.codeName.Length - 1)
			{
				int num2 = this.codeName.LastIndexOf("<");
				int num3 = num - num2 - 1;
				if (num3 > 0 && num2 >= 0)
				{
					string text = this.codeName.Substring(num2 + 1, num - num2 - 1);
					this.codeName = this.codeName.Remove(num2, num - num2 + 1);
					text = text.Replace("||", TemplateCode.PlaceHolder);
					string[] array = text.Split(new char[]
					{
						'|'
					});
					if (array.Length >= 1)
					{
						this.yesRule = array[0].Replace(TemplateCode.PlaceHolder, "|");
					}
					if (array.Length >= 2)
					{
						this.noRule = array[1].Replace(TemplateCode.PlaceHolder, "|");
						for (int i = 2; i < array.Length; i++)
						{
							this.noRule += array[i].Replace(TemplateCode.PlaceHolder, "|");
						}
					}
				}
			}
			this.codeName_lcase = this.codeName.ToLower();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001C414 File Offset: 0x0001B414
		public string GetCodeValueString()
		{
			if (this.codeValueString != null)
			{
				return this.codeValueString;
			}
			if (this.codeDataType == typeof(DateTime))
			{
				if (this.codeValue == null)
				{
					return TemplateCode.GetCodeValueString("", this.noRule, this.codeName);
				}
				DateTime dateTime = (DateTime)this.codeValue;
				string result = dateTime.ToString("yyyy-MM-dd");
				if (this.yesRule != null && this.yesRule.Length > 0)
				{
					try
					{
						result = dateTime.ToString(this.yesRule);
					}
					catch
					{
					}
				}
				return result;
			}
			else
			{
				string rule;
				if (this.codeDataType == typeof(bool))
				{
					bool flag = (bool)this.codeValue;
					rule = (flag ? this.yesRule : this.noRule);
					return TemplateCode.GetCodeValueString(flag ? "YES" : "NO", rule, this.codeName);
				}
				if (this.codeDataType == typeof(string))
				{
					string text = (this.codeValue == null) ? "" : ((string)this.codeValue);
					rule = ((text.Trim().Length > 0) ? this.yesRule : this.noRule);
					return TemplateCode.GetCodeValueString(text, rule, this.codeName);
				}
				string text2 = (this.codeValue == null) ? "" : this.codeValue.ToString();
				rule = ((text2.Trim().Length > 0) ? this.yesRule : this.noRule);
				return TemplateCode.GetCodeValueString(text2, rule, this.codeName);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001C5A4 File Offset: 0x0001B5A4
		private static string GetCodeValueString(string defaultText, string rule, string codeName)
		{
			if (rule == null)
			{
				return defaultText;
			}
			if (rule.Length < 1)
			{
				return "";
			}
			string text = rule.Replace("**", TemplateCode.PlaceHolder);
			text = text.Replace("*", codeName);
			text = text.Replace(TemplateCode.PlaceHolder, "*");
			text = text.Replace("..", TemplateCode.PlaceHolder);
			text = text.Replace(".", defaultText);
			return text.Replace(TemplateCode.PlaceHolder, ".");
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001C624 File Offset: 0x0001B624
		public static ArrayList FindIndices(string s, string lookFor, string ignore)
		{
			int num = 0;
			ArrayList arrayList = new ArrayList();
			for (;;)
			{
				num = s.IndexOf(lookFor, num);
				if (num < 0)
				{
					break;
				}
				int num2 = s.IndexOf(ignore, num);
				if (num2 == num)
				{
					num += ((ignore.Length > lookFor.Length) ? ignore.Length : lookFor.Length) + 1;
				}
				else
				{
					arrayList.Add(num);
					num += lookFor.Length;
				}
			}
			return arrayList;
		}

		// Token: 0x0400016F RID: 367
		private string codeName_lcase;

		// Token: 0x04000170 RID: 368
		private string codeName;

		// Token: 0x04000171 RID: 369
		private string rawCode;

		// Token: 0x04000172 RID: 370
		private object codeValue;

		// Token: 0x04000173 RID: 371
		private Type codeDataType;

		// Token: 0x04000174 RID: 372
		private string yesRule;

		// Token: 0x04000175 RID: 373
		private string noRule;

		// Token: 0x04000176 RID: 374
		private string yesNoRuleCodeStart = "<";

		// Token: 0x04000177 RID: 375
		private string yesNoRuleCodeEnd = "<";

		// Token: 0x04000178 RID: 376
		private bool shouldntBeEmpty;

		// Token: 0x04000179 RID: 377
		private bool ignoreForFillCodes;

		// Token: 0x0400017A RID: 378
		private int controlCode = -1;

		// Token: 0x0400017B RID: 379
		private bool isDataHolding = true;

		// Token: 0x0400017C RID: 380
		private string codeValueString;

		// Token: 0x0400017D RID: 381
		private ArrayList aliases = new ArrayList();

		// Token: 0x0400017E RID: 382
		public static string PlaceHolder = '\u001b'.ToString();
	}
}
