using System;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000162 RID: 354
	public class XlsValidationWrapper : CommonWrapper, spr\u20B0
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x000A151C File Offset: 0x000A051C
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x000A1560 File Offset: 0x000A0560
		internal XlsValidation Wrapped
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_37;
						default:
							goto IL_5C;
						}
						break;
					case 2:
						goto IL_37;
					}
					if (true)
					{
					}
					if (value != this.ᜀ)
					{
						num = 2;
						continue;
					}
					return;
					IL_37:
					this.ᜀ = value;
					num = 1;
				}
				IL_5C:
				if (false)
				{
				}
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x000A15DC File Offset: 0x000A05DC
		public XlsValidationWrapper(XlsRange range, XlsValidation wrap)
		{
			if (wrap == null)
			{
				XlsWorksheet innerWorksheet = range.InnerWorksheet;
				spr\u22CB a_ = (spr\u22CB)spr\u175E.ᜀ(TBIFFRecord.DVal);
				XlsDataValidationCollection xlsDataValidationCollection = innerWorksheet.DVTable.ᜀ(a_);
				sprᡣ a_2 = (sprᡣ)spr\u175E.ᜀ(TBIFFRecord.DV);
				wrap = xlsDataValidationCollection.ᜁ(a_2);
				wrap.AddRange(range);
			}
			this.ᜀ = wrap;
			this.ᜁ = range;
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000A1648 File Offset: 0x000A0648
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x000A1690 File Offset: 0x000A0690
		public string InputTitle
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.InputTitle;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_34;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						default:
							goto IL_6A;
						}
						break;
					}
					if (this.InputTitle != value)
					{
						num = 0;
						continue;
					}
					return;
					IL_34:
					this.BeginUpdate();
					this.ᜀ.InputTitle = value;
					this.EndUpdate();
					num = 2;
				}
				IL_6A:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x000A1724 File Offset: 0x000A0724
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x000A176C File Offset: 0x000A076C
		public string InputMessage
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.InputMessage;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3C;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3C;
						default:
							goto IL_72;
						}
						break;
					}
					if (true)
					{
					}
					if (this.InputMessage != value)
					{
						num = 0;
						continue;
					}
					return;
					IL_3C:
					this.BeginUpdate();
					this.ᜀ.InputMessage = value;
					this.EndUpdate();
					num = 2;
				}
				IL_72:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x000A1800 File Offset: 0x000A0800
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x000A1848 File Offset: 0x000A0848
		public string ErrorTitle
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.ErrorTitle;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						default:
							goto IL_72;
						}
						break;
					case 2:
						goto IL_34;
					}
					if (this.ErrorTitle != value)
					{
						num = 2;
						continue;
					}
					return;
					IL_34:
					this.BeginUpdate();
					this.ᜀ.ErrorTitle = value;
					this.EndUpdate();
					if (true)
					{
					}
					num = 1;
				}
				IL_72:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x000A18DC File Offset: 0x000A08DC
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x000A1924 File Offset: 0x000A0924
		public string ErrorMessage
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.ErrorMessage;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_34;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						default:
							goto IL_6A;
						}
						break;
					}
					if (this.ErrorMessage != value)
					{
						num = 0;
						continue;
					}
					return;
					IL_34:
					this.BeginUpdate();
					this.ᜀ.ErrorMessage = value;
					this.EndUpdate();
					num = 1;
				}
				IL_6A:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x000A19B8 File Offset: 0x000A09B8
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x000A1A2C File Offset: 0x000A0A2C
		public string Formula1
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				XlsWorkbook xlsWorkbook = this.ᜀ.Workbook;
				FormulaUtil formulaUtil = xlsWorkbook.FormulaUtil;
				return formulaUtil.ᜀ(this.FirstFormulaTokens, this.ᜁ.Row, this.ᜁ.Column, false, false);
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3F;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3F;
						default:
							goto IL_BC;
						}
						break;
					}
					if (true)
					{
					}
					if (this.Formula1 != value)
					{
						num = 0;
						continue;
					}
					return;
					IL_3F:
					this.BeginUpdate();
					XlsWorksheet a_ = this.ᜁ.Worksheet as XlsWorksheet;
					this.ᜀ.DVRecord.ᜁ(XlsValidation.ᜀ(ref value, null, a_, this.ᜁ.Row - 1, this.ᜁ.Column - 1));
					this.ᜀ.Formula1 = value;
					this.EndUpdate();
					num = 2;
				}
				IL_BC:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x000A1B08 File Offset: 0x000A0B08
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x000A1B50 File Offset: 0x000A0B50
		public DateTime DateTime1
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.DateTime1;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (this.DateTime1 == DateTime.MinValue)
						{
							num = 2;
							continue;
						}
						goto IL_97;
					case 2:
						goto IL_4E;
					case 3:
						goto IL_6E;
					}
					if (!(this.DateTime1 != value))
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					IL_4E:
					this.BeginUpdate();
					this.ᜀ.DateTime1 = value;
					this.EndUpdate();
					num = 3;
				}
				IL_6E:
				IL_97:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6E;
				default:
					if (false)
					{
					}
					return;
				}
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x000A1C10 File Offset: 0x000A0C10
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x000A1C58 File Offset: 0x000A0C58
		public string Formula2
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Formula2;
			}
			set
			{
				for (;;)
				{
					IL_00:
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
							{
								if (false)
								{
								}
								this.BeginUpdate();
								XlsWorksheet a_ = this.ᜁ.Worksheet as XlsWorksheet;
								this.ᜀ.DVRecord.ᜀ(XlsValidation.ᜀ(ref value, null, a_, this.ᜁ.Row - 1, this.ᜁ.Column - 1));
								this.ᜀ.Formula2 = value;
								this.EndUpdate();
								num = 0;
								continue;
							}
							}
							break;
						}
						if (!(this.Formula2 != value))
						{
							return;
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x000A1D34 File Offset: 0x000A0D34
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x000A1D7C File Offset: 0x000A0D7C
		public DateTime DateTime2
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.DateTime2;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_60;
					case 1:
						num = 4;
						continue;
					case 2:
						return;
					case 4:
						if (this.DateTime1 == DateTime.MinValue)
						{
							num = 0;
							continue;
						}
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (this.DateTime2 != value)
						{
							goto IL_60;
						}
						break;
					}
					num = 1;
					continue;
					IL_60:
					this.BeginUpdate();
					this.ᜀ.DateTime2 = value;
					this.EndUpdate();
					num = 2;
				}
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x000A1E40 File Offset: 0x000A0E40
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x000A1E88 File Offset: 0x000A0E88
		public CellDataType AllowType
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.AllowType;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜀ.IsListInFormula)
						{
							num = 2;
							continue;
						}
						goto IL_BB;
					case 2:
						num = 6;
						continue;
					case 3:
						this.BeginUpdate();
						this.ᜀ.AllowType = value;
						num = 0;
						continue;
					case 4:
						this.ᜀ.IsListInFormula = false;
						num = 7;
						continue;
					case 5:
						goto IL_CE;
					case 6:
						if (value != CellDataType.User)
						{
							num = 4;
							continue;
						}
						goto IL_BB;
					case 7:
						goto IL_BB;
					}
					if (true)
					{
					}
					if (this.AllowType != value)
					{
						num = 3;
						continue;
					}
					goto IL_CE;
					IL_BB:
					this.EndUpdate();
					num = 5;
					continue;
					IL_CE:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BB;
					default:
						goto IL_E4;
					}
				}
				IL_E4:
				if (false)
				{
				}
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x000A1F80 File Offset: 0x000A0F80
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x000A1FC8 File Offset: 0x000A0FC8
		public ValidationComparisonOperator CompareOperator
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.CompareOperator;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.CompareOperator = value;
								this.EndUpdate();
								if (true)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						if (this.CompareOperator == value)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x000A2054 File Offset: 0x000A1054
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x000A209C File Offset: 0x000A109C
		public bool IsListInFormula
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IsListInFormula;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								this.BeginUpdate();
								this.ᜀ.IsListInFormula = value;
								this.EndUpdate();
								num = 0;
								continue;
							}
							break;
						}
						if (this.IsListInFormula == value)
						{
							return;
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x000A2128 File Offset: 0x000A1128
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x000A2170 File Offset: 0x000A1170
		public bool IgnoreBlank
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.IgnoreBlank;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_75;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.IgnoreBlank = value;
								this.EndUpdate();
								num = 0;
								continue;
							}
							break;
						}
						if (this.IgnoreBlank == value)
						{
							return;
						}
						num = 2;
					}
				}
				IL_75:
				if (true)
				{
				}
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x000A21FC File Offset: 0x000A11FC
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x000A2244 File Offset: 0x000A1244
		public bool IsSuppressDropDownArrow
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IsSuppressDropDownArrow;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								this.BeginUpdate();
								this.ᜀ.IsSuppressDropDownArrow = value;
								this.EndUpdate();
								num = 0;
								continue;
							}
							break;
						}
						if (this.IsSuppressDropDownArrow == value)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x000A22D0 File Offset: 0x000A12D0
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x000A2318 File Offset: 0x000A1318
		public bool ShowInput
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.ShowInput;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 1:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.ShowInput = value;
								this.EndUpdate();
								num = 1;
								continue;
							}
							break;
						}
						if (this.ShowInput == value)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x000A23A4 File Offset: 0x000A13A4
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x000A23EC File Offset: 0x000A13EC
		public bool ShowError
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.ShowError;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.ShowError = value;
								this.EndUpdate();
								num = 0;
								continue;
							}
							break;
						}
						if (this.ShowError == value)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x000A2478 File Offset: 0x000A1478
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x000A24C0 File Offset: 0x000A14C0
		public int PromptBoxHPosition
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.PromptBoxHPosition;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜁ();
								this.ᜀ.PromptBoxHPosition = value;
								this.ᜀ();
								num = 1;
								continue;
							}
							break;
						case 1:
							return;
						}
						if (this.PromptBoxHPosition == value)
						{
							return;
						}
						num = 0;
					}
				}
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x000A254C File Offset: 0x000A154C
		// (set) Token: 0x0600102C RID: 4140 RVA: 0x000A2594 File Offset: 0x000A1594
		public int PromptBoxVPosition
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.PromptBoxVPosition;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜁ();
								this.ᜀ.PromptBoxVPosition = value;
								this.ᜀ();
								num = 0;
								continue;
							}
							break;
						}
						if (this.PromptBoxVPosition == value)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x000A2620 File Offset: 0x000A1620
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x000A2668 File Offset: 0x000A1668
		public bool IsInputVisible
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IsInputVisible;
			}
			set
			{
				for (;;)
				{
					IL_00:
					if (true)
					{
					}
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜁ();
								this.ᜀ.IsInputVisible = value;
								this.ᜀ();
								num = 2;
								continue;
							}
							break;
						case 2:
							return;
						}
						if (this.IsInputVisible == value)
						{
							return;
						}
						num = 0;
					}
				}
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x000A26F4 File Offset: 0x000A16F4
		// (set) Token: 0x06001030 RID: 4144 RVA: 0x000A273C File Offset: 0x000A173C
		public bool IsInputPositionFixed
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.IsInputPositionFixed;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_75;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜁ();
								this.ᜀ.IsInputPositionFixed = value;
								this.ᜀ();
								num = 1;
								continue;
							}
							break;
						}
						if (this.IsInputPositionFixed == value)
						{
							return;
						}
						num = 2;
					}
				}
				IL_75:
				if (true)
				{
				}
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x000A27C8 File Offset: 0x000A17C8
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x000A2810 File Offset: 0x000A1810
		public AlertStyleType AlertStyle
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.AlertStyle;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.AlertStyle = value;
								this.EndUpdate();
								num = 1;
								continue;
							}
							break;
						case 1:
							return;
						}
						if (this.AlertStyle == value)
						{
							return;
						}
						num = 0;
					}
				}
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x000A289C File Offset: 0x000A189C
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x000A28E4 File Offset: 0x000A18E4
		public string[] Values
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Values;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.BeginUpdate();
				this.ᜀ.Values = value;
				this.EndUpdate();
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x000A2938 File Offset: 0x000A1938
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x000A2980 File Offset: 0x000A1980
		public IXLSRange DataRange
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.DataRange;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.DataRange = value;
								this.EndUpdate();
								num = 0;
								continue;
							}
							break;
						}
						if (this.DataRange == value)
						{
							return;
						}
						if (true)
						{
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x000A2A0C File Offset: 0x000A1A0C
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x000A2A54 File Offset: 0x000A1A54
		public Ptg[] FirstFormulaTokens
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.FirstFormulaTokens;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.FirstFormulaTokens = value;
								this.EndUpdate();
								num = 2;
								continue;
							}
							break;
						case 2:
							return;
						}
						if (this.FirstFormulaTokens == value)
						{
							return;
						}
						num = 0;
					}
				}
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000A2AE0 File Offset: 0x000A1AE0
		// (set) Token: 0x0600103A RID: 4154 RVA: 0x000A2B28 File Offset: 0x000A1B28
		public Ptg[] SecondFormulaTokens
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.SecondFormulaTokens;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								this.BeginUpdate();
								this.ᜀ.SecondFormulaTokens = value;
								this.EndUpdate();
								num = 2;
								continue;
							}
							break;
						case 2:
							return;
						}
						if (this.SecondFormulaTokens == value)
						{
							return;
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x000A2BB4 File Offset: 0x000A1BB4
		internal spr\u1DF5 Application
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ.Application;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x000A2BFC File Offset: 0x000A1BFC
		public object Parent
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Parent;
			}
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000A2C44 File Offset: 0x000A1C44
		public override void BeginUpdate()
		{
			for (;;)
			{
				IL_00:
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							XlsDataValidationCollection parentCollection = this.ᜀ.ParentCollection;
							XlsValidation xlsValidation = new XlsValidation(parentCollection, this.ᜀ.DVRecord);
							xlsValidation.AddRange(this.ᜁ);
							this.ᜂ = this.ᜀ;
							this.ᜀ = xlsValidation;
							num = 1;
							continue;
						}
						}
						break;
					case 1:
						goto IL_A4;
					}
					if (base.BeginCallsCount != 0)
					{
						goto IL_A6;
					}
					num = 0;
				}
			}
			IL_A4:
			IL_A6:
			base.BeginUpdate();
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000A2D00 File Offset: 0x000A1D00
		public override void EndUpdate()
		{
			for (;;)
			{
				base.EndUpdate();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
						if (base.BeginCallsCount == 0)
						{
							num = 4;
							continue;
						}
						goto IL_F8;
					case 2:
						if (this.ᜂ != null)
						{
							num = 0;
							continue;
						}
						goto IL_E4;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E4;
						default:
							if (false)
							{
							}
							goto IL_E4;
						}
						break;
					case 4:
						this.ᜀ = this.ᜀ.ParentCollection.Add(this.ᜀ);
						num = 2;
						continue;
					case 5:
						goto IL_F6;
					case 6:
						if (this.ᜂ != this.ᜀ)
						{
							num = 7;
							continue;
						}
						goto IL_E4;
					case 7:
						this.ᜂ.RemoveRange(this.ᜁ);
						num = 3;
						continue;
					}
					break;
					IL_E4:
					this.ᜂ = null;
					num = 5;
				}
			}
			IL_F6:
			IL_F8:
			if (true)
			{
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000A2E18 File Offset: 0x000A1E18
		private void ᜁ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x000A2E54 File Offset: 0x000A1E54
		private void ᜀ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x04000DD9 RID: 3545
		private XlsValidation ᜀ;

		// Token: 0x04000DDA RID: 3546
		private XlsRange ᜁ;

		// Token: 0x04000DDB RID: 3547
		private long \u25D9\u0098\u009D\u0090;

		// Token: 0x04000DDC RID: 3548
		private int[] \u25D8\u00A0\u009B\u00AC;

		// Token: 0x04000DDD RID: 3549
		private int[] \u25D9\u0086ª\u008C;

		// Token: 0x04000DDE RID: 3550
		private XlsValidation ᜂ;
	}
}
