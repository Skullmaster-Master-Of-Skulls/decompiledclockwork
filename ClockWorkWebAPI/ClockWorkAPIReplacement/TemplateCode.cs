using System;
using System.Collections;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200006D RID: 109
	public class TemplateCode
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00023478 File Offset: 0x00021678
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00023490 File Offset: 0x00021690
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

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0002349C File Offset: 0x0002169C
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x000234B4 File Offset: 0x000216B4
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

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x000234C0 File Offset: 0x000216C0
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x000234D8 File Offset: 0x000216D8
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

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000234E4 File Offset: 0x000216E4
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00023511 File Offset: 0x00021711
		public object CodeValue
		{
			get
			{
				bool flag = this.codeValue != null;
				object result;
				if (flag)
				{
					result = this.codeValue;
				}
				else
				{
					result = "";
				}
				return result;
			}
			set
			{
				this.codeValue = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0002351C File Offset: 0x0002171C
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00023534 File Offset: 0x00021734
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

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00023540 File Offset: 0x00021740
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00023558 File Offset: 0x00021758
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

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00023564 File Offset: 0x00021764
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0002357C File Offset: 0x0002177C
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

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00023588 File Offset: 0x00021788
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x000235A0 File Offset: 0x000217A0
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

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x000235AC File Offset: 0x000217AC
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x000235C4 File Offset: 0x000217C4
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

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000235D0 File Offset: 0x000217D0
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x000235E8 File Offset: 0x000217E8
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

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000235F4 File Offset: 0x000217F4
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0002360C File Offset: 0x0002180C
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

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00023618 File Offset: 0x00021818
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00023630 File Offset: 0x00021830
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

		// Token: 0x06000569 RID: 1385 RVA: 0x0002363C File Offset: 0x0002183C
		public TemplateCode Copy()
		{
			return new TemplateCode(this.codeName_lcase, this.codeName, this.rawCode, this.codeValue, this.codeDataType, this.yesRule, this.noRule);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00023680 File Offset: 0x00021880
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

		// Token: 0x0600056B RID: 1387 RVA: 0x00023710 File Offset: 0x00021910
		public TemplateCode(string codeName, object codeValue, Type codeDataType)
		{
			this.codeName = codeName;
			this.codeName_lcase = codeName.ToLower();
			this.codeValue = codeValue;
			this.codeDataType = codeDataType;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0002378A File Offset: 0x0002198A
		public void AddAlias(string alias)
		{
			this.aliases.Add(alias.ToLower());
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000237A0 File Offset: 0x000219A0
		public bool Matches(string codeNameLCase)
		{
			bool flag = this.codeName_lcase.CompareTo(codeNameLCase) == 0;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				foreach (object obj in this.aliases)
				{
					string text = (string)obj;
					bool flag2 = text.CompareTo(codeNameLCase) == 0;
					if (flag2)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0002382C File Offset: 0x00021A2C
		public TemplateCode(string RawCode)
		{
			this.rawCode = RawCode;
			this.codeValue = null;
			this.codeDataType = null;
			this.codeName = RawCode;
			this.yesRule = null;
			this.noRule = null;
			int num = this.codeName.LastIndexOf(">");
			bool flag = num >= 0 && num == this.codeName.Length - 1;
			if (flag)
			{
				int num2 = this.codeName.LastIndexOf("<");
				int num3 = num - num2 - 1;
				bool flag2 = num3 > 0 && num2 >= 0;
				if (flag2)
				{
					string text = this.codeName.Substring(num2 + 1, num - num2 - 1);
					this.codeName = this.codeName.Remove(num2, num - num2 + 1);
					text = text.Replace("||", TemplateCode.PlaceHolder);
					string[] array = text.Split(new char[]
					{
						'|'
					});
					bool flag3 = array.Length >= 1;
					if (flag3)
					{
						this.yesRule = array[0].Replace(TemplateCode.PlaceHolder, "|");
					}
					bool flag4 = array.Length >= 2;
					if (flag4)
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

		// Token: 0x0600056F RID: 1391 RVA: 0x00023A08 File Offset: 0x00021C08
		public string GetCodeValueString()
		{
			bool flag = this.codeValueString != null;
			string result;
			if (flag)
			{
				result = this.codeValueString;
			}
			else
			{
				bool flag2 = this.codeDataType == typeof(DateTime);
				if (flag2)
				{
					bool flag3 = this.codeValue == null;
					if (flag3)
					{
						result = TemplateCode.GetCodeValueString("", this.noRule, this.codeName);
					}
					else
					{
						DateTime dateTime = (DateTime)this.codeValue;
						string text = dateTime.ToString("yyyy-MM-dd");
						bool flag4 = this.yesRule != null && this.yesRule.Length > 0;
						if (flag4)
						{
							try
							{
								text = dateTime.ToString(this.yesRule);
							}
							catch
							{
							}
						}
						result = text;
					}
				}
				else
				{
					bool flag5 = this.codeDataType == typeof(bool);
					if (flag5)
					{
						bool flag6 = (bool)this.codeValue;
						string rule = flag6 ? this.yesRule : this.noRule;
						result = TemplateCode.GetCodeValueString(flag6 ? "YES" : "NO", rule, this.codeName);
					}
					else
					{
						bool flag7 = this.codeDataType == typeof(string);
						if (flag7)
						{
							string text2 = (this.codeValue == null) ? "" : ((string)this.codeValue);
							string rule = (text2.Trim().Length > 0) ? this.yesRule : this.noRule;
							result = TemplateCode.GetCodeValueString(text2, rule, this.codeName);
						}
						else
						{
							string text3 = (this.codeValue == null) ? "" : this.codeValue.ToString();
							string rule = (text3.Trim().Length > 0) ? this.yesRule : this.noRule;
							result = TemplateCode.GetCodeValueString(text3, rule, this.codeName);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00023BF8 File Offset: 0x00021DF8
		private static string GetCodeValueString(string defaultText, string rule, string codeName)
		{
			bool flag = rule == null;
			string result;
			if (flag)
			{
				result = defaultText;
			}
			else
			{
				bool flag2 = rule.Length < 1;
				if (flag2)
				{
					result = "";
				}
				else
				{
					string text = rule.Replace("**", TemplateCode.PlaceHolder);
					text = text.Replace("*", codeName);
					text = text.Replace(TemplateCode.PlaceHolder, "*");
					text = text.Replace("..", TemplateCode.PlaceHolder);
					text = text.Replace(".", defaultText);
					text = text.Replace(TemplateCode.PlaceHolder, ".");
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00023C8C File Offset: 0x00021E8C
		public static ArrayList FindIndices(string s, string lookFor, string ignore)
		{
			int num = 0;
			ArrayList arrayList = new ArrayList();
			for (;;)
			{
				num = s.IndexOf(lookFor, num);
				bool flag = num >= 0;
				if (!flag)
				{
					break;
				}
				int num2 = s.IndexOf(ignore, num);
				bool flag2 = num2 == num;
				if (flag2)
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

		// Token: 0x040002D9 RID: 729
		private string codeName_lcase;

		// Token: 0x040002DA RID: 730
		private string codeName;

		// Token: 0x040002DB RID: 731
		private string rawCode;

		// Token: 0x040002DC RID: 732
		private object codeValue;

		// Token: 0x040002DD RID: 733
		private Type codeDataType;

		// Token: 0x040002DE RID: 734
		private string yesRule;

		// Token: 0x040002DF RID: 735
		private string noRule;

		// Token: 0x040002E0 RID: 736
		private string yesNoRuleCodeStart = "<";

		// Token: 0x040002E1 RID: 737
		private string yesNoRuleCodeEnd = "<";

		// Token: 0x040002E2 RID: 738
		private bool shouldntBeEmpty = false;

		// Token: 0x040002E3 RID: 739
		private bool ignoreForFillCodes = false;

		// Token: 0x040002E4 RID: 740
		private int controlCode = -1;

		// Token: 0x040002E5 RID: 741
		private bool isDataHolding = true;

		// Token: 0x040002E6 RID: 742
		private string codeValueString = null;

		// Token: 0x040002E7 RID: 743
		private ArrayList aliases = new ArrayList();

		// Token: 0x040002E8 RID: 744
		public static string PlaceHolder = '\u001b'.ToString();
	}
}
