using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000617 RID: 1559
	public class XlsName : XlsObject, INamedRange, spr\u1AE6, spr\u1D46, spr\u1A8B, ICloneParent, ICombinedRange, IDisposable
	{
		// Token: 0x06005DC1 RID: 24001 RVA: 0x003AD3DC File Offset: 0x003AC3DC
		internal XlsName(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜄ = (sprῚ)spr\u175E.ᜀ(TBIFFRecord.Name);
			this.ᜀ();
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x003AD410 File Offset: 0x003AC410
		internal XlsName(spr\u1DF5 A_0, object A_1, sprῚ A_2, int A_3) : base(A_0, A_1)
		{
			this.ᜇ = A_3;
			this.ᜀ();
			this.ᜀ(A_2);
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x003AD444 File Offset: 0x003AC444
		internal XlsName(spr\u1DF5 A_0, object A_1, sprῚ A_2) : this(A_0, A_1, A_2, -1)
		{
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x003AD45C File Offset: 0x003AC45C
		internal XlsName(spr\u1DF5 A_0, object A_1, string A_2, IXLSRange A_3, int A_4) : this(A_0, A_1, A_2, A_3, A_4, false)
		{
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x003AD478 File Offset: 0x003AC478
		internal XlsName(spr\u1DF5 A_0, object A_1, string A_2, int A_3) : this(A_0, A_1, A_2, A_3, false)
		{
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x003AD494 File Offset: 0x003AC494
		internal XlsName(spr\u1DF5 A_0, object A_1, string A_2, int A_3, bool A_4) : this(A_0, A_1)
		{
			this.ᜇ = A_3;
			this.Name = A_2;
			this.ᜀ(A_4);
		}

		// Token: 0x06005DC7 RID: 24007 RVA: 0x003AD4C0 File Offset: 0x003AC4C0
		internal XlsName(spr\u1DF5 A_0, object A_1, string A_2, IXLSRange A_3, int A_4, bool A_5) : this(A_0, A_1)
		{
			this.ᜇ = A_4;
			this.ᜄ = (sprῚ)spr\u175E.ᜀ(TBIFFRecord.Name);
			this.ᜀ(A_5);
			this.ᜀ();
			this.Name = A_2;
			this.RefersToRange = A_3;
		}

		// Token: 0x06005DC8 RID: 24008 RVA: 0x003AD50C File Offset: 0x003AC50C
		private void ᜀ(bool A_0)
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
			this.ᜄ.ᜀ(A_0 ? ((ushort)(this.ᜆ.RealIndex + 1)) : 0);
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06005DC9 RID: 24009 RVA: 0x003AD56C File Offset: 0x003AC56C
		public int Index
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
				return this.ᜇ;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06005DCA RID: 24010 RVA: 0x003AD5B0 File Offset: 0x003AC5B0
		// (set) Token: 0x06005DCB RID: 24011 RVA: 0x003AD5F8 File Offset: 0x003AC5F8
		public string Name
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
				return this.ᜄ.ᜊ();
			}
			set
			{
				int a_ = 14;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_40;
					case 2:
						return;
					case 3:
					{
						string a_2 = this.ᜄ.ᜊ();
						this.ᜄ.ᜃ(sprῚ.ᜁ(value));
						this.ᜄ.ᜆ(value);
						this.ᜅ.InnerNamesColection.ᜀ(true);
						num = 6;
						continue;
					}
					case 4:
					{
						if (true)
						{
						}
						sprᤗ sprᤗ = this.Worksheet.InnerNames;
						string a_2;
						sprᤗ.ᜀ(this, a_2);
						num = 2;
						continue;
					}
					case 5:
						if (value != this.ᜄ.ᜊ())
						{
							num = 3;
							continue;
						}
						return;
					case 6:
						if (this.ᜆ != null)
						{
							num = 4;
							continue;
						}
						return;
					}
					if (value == null)
					{
						num = 0;
					}
					else
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 5;
							break;
						}
					}
				}
				IL_40:
				throw new ArgumentNullException(RecordTableEnumerator.b("㉃❅⑇㽉⥋", a_));
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06005DCC RID: 24012 RVA: 0x003AD738 File Offset: 0x003AC738
		// (set) Token: 0x06005DCD RID: 24013 RVA: 0x003AD780 File Offset: 0x003AC780
		public string NameLocal
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
				return this.ᜄ.ᜊ();
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
				this.ᜄ.ᜆ(value);
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06005DCE RID: 24014 RVA: 0x003AD7C8 File Offset: 0x003AC7C8
		// (set) Token: 0x06005DCF RID: 24015 RVA: 0x003AD978 File Offset: 0x003AC978
		public IXLSRange RefersToRange
		{
			get
			{
				switch (0)
				{
				default:
				{
					IXLSRange result;
					for (;;)
					{
						IL_4F:
						string text;
						XlsWorksheet xlsWorksheet;
						string value;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_A5:
							xlsWorksheet = (XlsWorksheet)this.ᜅ.Worksheets[text];
							goto IL_137;
						default:
							if (false)
							{
							}
							result = null;
							value = this.Value;
							num = 4;
							break;
						}
						XlsWorksheet xlsWorksheet2;
						for (;;)
						{
							IL_10:
							switch (num)
							{
							case 0:
								goto IL_D3;
							case 1:
								num = 2;
								continue;
							case 2:
								if (value.Length != 0)
								{
									num = 8;
									continue;
								}
								return result;
							case 3:
								return result;
							case 4:
								if (value != null)
								{
									num = 1;
									continue;
								}
								return result;
							case 5:
								result = this.ᜆ.GetRangeByString(value);
								num = 6;
								continue;
							case 6:
								return result;
							case 7:
							{
								string rangeValue;
								result = xlsWorksheet2.GetRangeByString(rangeValue);
								num = 3;
								continue;
							}
							case 8:
								num = 12;
								continue;
							case 9:
								if (xlsWorksheet2 != null)
								{
									num = 7;
									continue;
								}
								return result;
							case 10:
								if (text == null)
								{
									num = 13;
									continue;
								}
								if (true)
								{
								}
								num = 11;
								continue;
							case 11:
								goto IL_A5;
							case 12:
							{
								if (this.ᜆ != null)
								{
									num = 5;
									continue;
								}
								string rangeValue = value;
								text = sprṔ.ᜀ(ref rangeValue);
								num = 10;
								continue;
							}
							case 13:
								num = 0;
								continue;
							}
							goto IL_4F;
						}
						IL_D3:
						xlsWorksheet = null;
						IL_137:
						xlsWorksheet2 = xlsWorksheet;
						num = 9;
						goto IL_10;
					}
					return result;
				}
				}
			}
			set
			{
				int a_ = 15;
				while (value == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						throw new ArgumentNullException(RecordTableEnumerator.b("㍄♆╈㹊⡌", a_));
					}
				}
				this.Value = value.RangeGlobalAddress;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06005DD0 RID: 24016 RVA: 0x003AD9E4 File Offset: 0x003AC9E4
		// (set) Token: 0x06005DD1 RID: 24017 RVA: 0x003ADA58 File Offset: 0x003ACA58
		public string Value
		{
			get
			{
				string result;
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
					result = null;
					try
					{
						result = this.ᜅ.FormulaUtil.ᜁ(this.ᜄ.ᜈ());
					}
					catch (spr\u2313)
					{
					}
					break;
				}
				return result;
			}
			set
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
				this.ᜀ(value, false);
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x003ADA9C File Offset: 0x003ACA9C
		public string EnvalutedValue
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							if (((IWorksheet)base.Parent).FormulaEngine != null)
							{
								num = 3;
								continue;
							}
							goto IL_CA;
						case 1:
							goto IL_63;
						case 2:
							if (true)
							{
							}
							break;
						case 3:
							goto IL_C8;
						}
						if (base.Parent is IWorksheet)
						{
							num = 1;
							continue;
						}
						goto IL_CA;
					}
					IL_63:
					num = 0;
				}
				IL_C8:
				string a_ = sprḅ.ᜀ(this.Column) + this.Row.ToString();
				return ((IWorksheet)base.Parent).FormulaEngine.ᜀ.\u17C4(a_);
				IL_CA:
				return null;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x003ADB74 File Offset: 0x003ACB74
		public string ValueR1C1
		{
			get
			{
				string result;
				try
				{
					switch (1 == 1)
					{
					}
					if (false)
					{
					}
					result = this.ᜅ.FormulaUtil.ᜀ(this.ᜄ.ᜈ(), 0, 0, true, false);
				}
				catch
				{
					result = null;
				}
				if (true)
				{
				}
				return result;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x003ADBE8 File Offset: 0x003ACBE8
		// (set) Token: 0x06005DD5 RID: 24021 RVA: 0x003ADC34 File Offset: 0x003ACC34
		public bool Visible
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
				return !this.ᜄ.\u170D();
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
				this.ᜄ.ᜂ(!value);
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06005DD6 RID: 24022 RVA: 0x003ADC80 File Offset: 0x003ACC80
		public bool IsLocal
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
				return this.ᜄ.ᜃ() != 0;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x003ADCCC File Offset: 0x003ACCCC
		IWorksheet INamedRange.Worksheet
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
				return this.ᜆ;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x003ADD10 File Offset: 0x003ACD10
		public string Scope
		{
			get
			{
				int a_ = 3;
				while (!this.IsLocal)
				{
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
						return RecordTableEnumerator.b("游吺似吾⍀ⱂ⩄ⱆ", a_);
					}
				}
				return this.ᜆ.Name;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06005DD9 RID: 24025 RVA: 0x003ADD78 File Offset: 0x003ACD78
		public string RangeAddress
		{
			get
			{
				int a_ = 12;
				while (this.ᜆ == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return this.Name;
					}
				}
				if (true)
				{
				}
				return string.Format(RecordTableEnumerator.b("敁㽃癅㕇浉测㕍慏⽑", a_), this.ᜆ.Name, this.Name);
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06005DDA RID: 24026 RVA: 0x003ADDF4 File Offset: 0x003ACDF4
		public string RangeAddressLocal
		{
			get
			{
				int a_ = 14;
				while (this.ᜆ == null)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return this.Name;
					}
				}
				return string.Format(RecordTableEnumerator.b("捃㵅硇㝉歋潍⭏捑⥓", a_), this.ᜆ.Name, this.Name);
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06005DDB RID: 24027 RVA: 0x003ADE70 File Offset: 0x003ACE70
		public string RangeGlobalAddress
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
				return this.RefersToRange.RangeGlobalAddress;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06005DDC RID: 24028 RVA: 0x003ADEB8 File Offset: 0x003ACEB8
		public string RangeR1C1Address
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
				return this.RefersToRange.RangeR1C1Address;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06005DDD RID: 24029 RVA: 0x003ADF00 File Offset: 0x003ACF00
		public string RangeR1C1AddressLocal
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
				return this.RefersToRange.RangeR1C1AddressLocal;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06005DDE RID: 24030 RVA: 0x003ADF48 File Offset: 0x003ACF48
		// (set) Token: 0x06005DDF RID: 24031 RVA: 0x003ADF90 File Offset: 0x003ACF90
		public bool BooleanValue
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
				return this.RefersToRange.BooleanValue;
			}
			set
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
				this.RefersToRange.BooleanValue = value;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x003ADFD8 File Offset: 0x003ACFD8
		public IBorders Borders
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
				return this.RefersToRange.Borders;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06005DE1 RID: 24033 RVA: 0x003AE020 File Offset: 0x003AD020
		public CellRange[] Cells
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
				return this.RefersToRange.Cells;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06005DE2 RID: 24034 RVA: 0x003AE068 File Offset: 0x003AD068
		public int Column
		{
			get
			{
				IXLSRange refersToRange = this.RefersToRange;
				if (refersToRange != null)
				{
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
						return refersToRange.LastColumn;
					}
				}
				return -1;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x003AE0B8 File Offset: 0x003AD0B8
		public int ColumnGroupLevel
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
				return this.RefersToRange.ColumnGroupLevel;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06005DE4 RID: 24036 RVA: 0x003AE100 File Offset: 0x003AD100
		// (set) Token: 0x06005DE5 RID: 24037 RVA: 0x003AE148 File Offset: 0x003AD148
		public double ColumnWidth
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
				return this.RefersToRange.ColumnWidth;
			}
			set
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
				this.RefersToRange.ColumnWidth = value;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06005DE6 RID: 24038 RVA: 0x003AE190 File Offset: 0x003AD190
		public int Count
		{
			get
			{
				IXLSRange refersToRange = this.RefersToRange;
				if (refersToRange != null)
				{
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
						return refersToRange.Count;
					}
				}
				return 1;
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x003AE1E0 File Offset: 0x003AD1E0
		// (set) Token: 0x06005DE8 RID: 24040 RVA: 0x003AE228 File Offset: 0x003AD228
		public DateTime DateTimeValue
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
				return this.RefersToRange.DateTimeValue;
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
				this.RefersToRange.DateTimeValue = value;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x003AE270 File Offset: 0x003AD270
		public string NumberText
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
				return this.RefersToRange.NumberText;
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x003AE2B8 File Offset: 0x003AD2B8
		public IXLSRange EndCell
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
				return this.RefersToRange.EndCell;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06005DEB RID: 24043 RVA: 0x003AE300 File Offset: 0x003AD300
		public IXLSRange EntireColumn
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
				return this.RefersToRange.EntireColumn;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06005DEC RID: 24044 RVA: 0x003AE348 File Offset: 0x003AD348
		public IXLSRange EntireRow
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
				return this.RefersToRange.EntireRow;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06005DED RID: 24045 RVA: 0x003AE390 File Offset: 0x003AD390
		// (set) Token: 0x06005DEE RID: 24046 RVA: 0x003AE3D8 File Offset: 0x003AD3D8
		public string ErrorValue
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
				return this.RefersToRange.ErrorValue;
			}
			set
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
				this.RefersToRange.ErrorValue = value;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06005DEF RID: 24047 RVA: 0x003AE420 File Offset: 0x003AD420
		// (set) Token: 0x06005DF0 RID: 24048 RVA: 0x003AE468 File Offset: 0x003AD468
		public string Formula
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
				return this.RefersToRange.Formula;
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
				this.RefersToRange.Formula = value;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06005DF1 RID: 24049 RVA: 0x003AE4B0 File Offset: 0x003AD4B0
		// (set) Token: 0x06005DF2 RID: 24050 RVA: 0x003AE4F8 File Offset: 0x003AD4F8
		public string FormulaArray
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
				return this.RefersToRange.FormulaArray;
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
				this.RefersToRange.FormulaArray = value;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06005DF3 RID: 24051 RVA: 0x003AE540 File Offset: 0x003AD540
		// (set) Token: 0x06005DF4 RID: 24052 RVA: 0x003AE588 File Offset: 0x003AD588
		public string FormulaArrayR1C1
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
				return this.RefersToRange.FormulaArrayR1C1;
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
				this.RefersToRange.FormulaArrayR1C1 = value;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06005DF5 RID: 24053 RVA: 0x003AE5D0 File Offset: 0x003AD5D0
		// (set) Token: 0x06005DF6 RID: 24054 RVA: 0x003AE618 File Offset: 0x003AD618
		public bool IsFormulaHidden
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
				return this.RefersToRange.IsFormulaHidden;
			}
			set
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
				this.RefersToRange.IsFormulaHidden = value;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06005DF7 RID: 24055 RVA: 0x003AE660 File Offset: 0x003AD660
		// (set) Token: 0x06005DF8 RID: 24056 RVA: 0x003AE6A8 File Offset: 0x003AD6A8
		public DateTime FormulaDateTime
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
				return this.RefersToRange.FormulaDateTime;
			}
			set
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
				this.RefersToRange.FormulaDateTime = value;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06005DF9 RID: 24057 RVA: 0x003AE6F0 File Offset: 0x003AD6F0
		// (set) Token: 0x06005DFA RID: 24058 RVA: 0x003AE738 File Offset: 0x003AD738
		public string FormulaR1C1
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
				return this.RefersToRange.FormulaR1C1;
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
				this.RefersToRange.FormulaR1C1 = value;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06005DFB RID: 24059 RVA: 0x003AE780 File Offset: 0x003AD780
		public bool HasDataValidation
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
				return this.RefersToRange.HasDataValidation;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06005DFC RID: 24060 RVA: 0x003AE7C8 File Offset: 0x003AD7C8
		public bool HasDateTime
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
				return this.RefersToRange.HasDateTime;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06005DFD RID: 24061 RVA: 0x003AE810 File Offset: 0x003AD810
		public bool HasFormulaBoolValue
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
				return this.RefersToRange.HasFormulaBoolValue;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06005DFE RID: 24062 RVA: 0x003AE858 File Offset: 0x003AD858
		public bool HasFormulaErrorValue
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
				return this.RefersToRange.HasFormulaErrorValue;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x003AE8A0 File Offset: 0x003AD8A0
		public bool HasFormulaDateTime
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
				return this.RefersToRange.HasFormulaDateTime;
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06005E00 RID: 24064 RVA: 0x003AE8E8 File Offset: 0x003AD8E8
		public bool HasFormulaNumberValue
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
				return this.RefersToRange.HasFormulaNumberValue;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06005E01 RID: 24065 RVA: 0x003AE930 File Offset: 0x003AD930
		public bool HasFormulaStringValue
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
				return this.RefersToRange.HasFormulaStringValue;
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005E02 RID: 24066 RVA: 0x003AE978 File Offset: 0x003AD978
		public bool HasFormula
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
				return this.RefersToRange.HasFormula;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06005E03 RID: 24067 RVA: 0x003AE9C0 File Offset: 0x003AD9C0
		public bool HasFormulaArray
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
				return this.RefersToRange.HasFormulaArray;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06005E04 RID: 24068 RVA: 0x003AEA08 File Offset: 0x003ADA08
		public bool HasNumber
		{
			get
			{
				if (this.RefersToRange != null)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return this.RefersToRange.HasNumber;
					}
				}
				double num;
				return double.TryParse(this.Value, out num);
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06005E05 RID: 24069 RVA: 0x003AEA68 File Offset: 0x003ADA68
		public bool HasRichText
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
				return this.RefersToRange.HasRichText;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06005E06 RID: 24070 RVA: 0x003AEAB0 File Offset: 0x003ADAB0
		public bool HasString
		{
			get
			{
				if (true)
				{
				}
				if (this.RefersToRange != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return this.RefersToRange.HasString;
					}
				}
				return false;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x003AEB04 File Offset: 0x003ADB04
		public bool HasStyle
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
				return this.RefersToRange.HasStyle;
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06005E08 RID: 24072 RVA: 0x003AEB4C File Offset: 0x003ADB4C
		public IHyperLinks Hyperlinks
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
				return this.RefersToRange.Hyperlinks;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06005E09 RID: 24073 RVA: 0x003AEB94 File Offset: 0x003ADB94
		// (set) Token: 0x06005E0A RID: 24074 RVA: 0x003AEBDC File Offset: 0x003ADBDC
		public HorizontalAlignType HorizontalAlignment
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
				return this.RefersToRange.HorizontalAlignment;
			}
			set
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
				this.RefersToRange.HorizontalAlignment = value;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06005E0B RID: 24075 RVA: 0x003AEC24 File Offset: 0x003ADC24
		// (set) Token: 0x06005E0C RID: 24076 RVA: 0x003AEC6C File Offset: 0x003ADC6C
		public int IndentLevel
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
				return this.RefersToRange.IndentLevel;
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
				this.RefersToRange.IndentLevel = value;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x003AECB4 File Offset: 0x003ADCB4
		public bool IsBlank
		{
			get
			{
				if (this.RefersToRange != null)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return this.RefersToRange.IsBlank;
					}
				}
				return !string.IsNullOrEmpty(this.Value);
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06005E0E RID: 24078 RVA: 0x003AED14 File Offset: 0x003ADD14
		public bool HasBoolean
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
				return this.RefersToRange.HasBoolean;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x003AED5C File Offset: 0x003ADD5C
		public bool HasError
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
				return this.RefersToRange.HasError;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06005E10 RID: 24080 RVA: 0x003AEDA4 File Offset: 0x003ADDA4
		public bool IsGroupedByColumn
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
				return this.RefersToRange.IsGroupedByColumn;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x003AEDEC File Offset: 0x003ADDEC
		public bool IsGroupedByRow
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
				return this.RefersToRange.IsGroupedByRow;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06005E12 RID: 24082 RVA: 0x003AEE34 File Offset: 0x003ADE34
		public bool IsInitialized
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
				return this.RefersToRange.IsInitialized;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x003AEE7C File Offset: 0x003ADE7C
		// (set) Token: 0x06005E14 RID: 24084 RVA: 0x003AEECC File Offset: 0x003ADECC
		public int LastColumn
		{
			get
			{
				IXLSRange refersToRange = this.RefersToRange;
				if (refersToRange != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return refersToRange.LastColumn;
					}
				}
				if (true)
				{
				}
				return -1;
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
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06005E15 RID: 24085 RVA: 0x003AEF08 File Offset: 0x003ADF08
		// (set) Token: 0x06005E16 RID: 24086 RVA: 0x003AEF58 File Offset: 0x003ADF58
		public int LastRow
		{
			get
			{
				IXLSRange refersToRange = this.RefersToRange;
				if (refersToRange != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return refersToRange.LastRow;
					}
				}
				if (true)
				{
				}
				return -1;
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
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06005E17 RID: 24087 RVA: 0x003AEF94 File Offset: 0x003ADF94
		// (set) Token: 0x06005E18 RID: 24088 RVA: 0x003AEFDC File Offset: 0x003ADFDC
		public double NumberValue
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
				return this.RefersToRange.NumberValue;
			}
			set
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
				this.RefersToRange.NumberValue = value;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06005E19 RID: 24089 RVA: 0x003AF024 File Offset: 0x003AE024
		// (set) Token: 0x06005E1A RID: 24090 RVA: 0x003AF06C File Offset: 0x003AE06C
		public string NumberFormat
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
				return this.RefersToRange.NumberFormat;
			}
			set
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
				this.RefersToRange.NumberFormat = value;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06005E1B RID: 24091 RVA: 0x003AF0B4 File Offset: 0x003AE0B4
		public int Row
		{
			get
			{
				IXLSRange refersToRange = this.RefersToRange;
				if (refersToRange != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return refersToRange.LastRow;
					}
				}
				if (true)
				{
				}
				return -1;
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06005E1C RID: 24092 RVA: 0x003AF104 File Offset: 0x003AE104
		public int RowGroupLevel
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
				return this.RefersToRange.RowGroupLevel;
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06005E1D RID: 24093 RVA: 0x003AF14C File Offset: 0x003AE14C
		// (set) Token: 0x06005E1E RID: 24094 RVA: 0x003AF194 File Offset: 0x003AE194
		public double RowHeight
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
				return this.RefersToRange.RowHeight;
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
				this.RefersToRange.RowHeight = value;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x003AF1DC File Offset: 0x003AE1DC
		public IXLSRange[] Rows
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
				return this.RefersToRange.Rows;
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06005E20 RID: 24096 RVA: 0x003AF224 File Offset: 0x003AE224
		public IXLSRange[] Columns
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
				return this.RefersToRange.Columns;
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06005E21 RID: 24097 RVA: 0x003AF26C File Offset: 0x003AE26C
		// (set) Token: 0x06005E22 RID: 24098 RVA: 0x003AF2B4 File Offset: 0x003AE2B4
		public IStyle Style
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
				return this.RefersToRange.Style;
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
				this.RefersToRange.Style = value;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06005E23 RID: 24099 RVA: 0x003AF2FC File Offset: 0x003AE2FC
		// (set) Token: 0x06005E24 RID: 24100 RVA: 0x003AF344 File Offset: 0x003AE344
		public string CellStyleName
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
				return this.RefersToRange.CellStyleName;
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
				this.RefersToRange.CellStyleName = value;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x003AF38C File Offset: 0x003AE38C
		// (set) Token: 0x06005E26 RID: 24102 RVA: 0x003AF3D4 File Offset: 0x003AE3D4
		public string Text
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
				return this.RefersToRange.Text;
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
				this.RefersToRange.Text = value;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005E27 RID: 24103 RVA: 0x003AF41C File Offset: 0x003AE41C
		// (set) Token: 0x06005E28 RID: 24104 RVA: 0x003AF464 File Offset: 0x003AE464
		public TimeSpan TimeSpanValue
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
				return this.RefersToRange.TimeSpanValue;
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
				this.RefersToRange.TimeSpanValue = value;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x003AF4AC File Offset: 0x003AE4AC
		// (set) Token: 0x06005E2A RID: 24106 RVA: 0x003AF4F4 File Offset: 0x003AE4F4
		string IXLSRange.Value
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
				return this.RefersToRange.Value;
			}
			set
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
				this.RefersToRange.Value = value;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x003AF53C File Offset: 0x003AE53C
		// (set) Token: 0x06005E2C RID: 24108 RVA: 0x003AF584 File Offset: 0x003AE584
		public object Value2
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
				return this.RefersToRange.Value2;
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
				this.RefersToRange.Value2 = value;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06005E2D RID: 24109 RVA: 0x003AF5CC File Offset: 0x003AE5CC
		// (set) Token: 0x06005E2E RID: 24110 RVA: 0x003AF614 File Offset: 0x003AE614
		public VerticalAlignType VerticalAlignment
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
				return this.RefersToRange.VerticalAlignment;
			}
			set
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
				this.RefersToRange.VerticalAlignment = value;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x003AF65C File Offset: 0x003AE65C
		public ConditionalFormats ConditionalFormats
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
				return this.RefersToRange.ConditionalFormats;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06005E30 RID: 24112 RVA: 0x003AF6A4 File Offset: 0x003AE6A4
		public Validation DataValidation
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
				return this.RefersToRange.DataValidation;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06005E31 RID: 24113 RVA: 0x003AF6EC File Offset: 0x003AE6EC
		// (set) Token: 0x06005E32 RID: 24114 RVA: 0x003AF734 File Offset: 0x003AE734
		public string FormulaStringValue
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
				return this.RefersToRange.FormulaStringValue;
			}
			set
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
				this.RefersToRange.FormulaStringValue = value;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x003AF77C File Offset: 0x003AE77C
		// (set) Token: 0x06005E34 RID: 24116 RVA: 0x003AF7C4 File Offset: 0x003AE7C4
		public double FormulaNumberValue
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
				return this.RefersToRange.FormulaNumberValue;
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
				this.RefersToRange.FormulaNumberValue = value;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x003AF80C File Offset: 0x003AE80C
		// (set) Token: 0x06005E36 RID: 24118 RVA: 0x003AF854 File Offset: 0x003AE854
		public bool FormulaBoolValue
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
				return this.RefersToRange.FormulaBoolValue;
			}
			set
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
				this.RefersToRange.FormulaBoolValue = value;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x003AF89C File Offset: 0x003AE89C
		// (set) Token: 0x06005E38 RID: 24120 RVA: 0x003AF8E4 File Offset: 0x003AE8E4
		public string FormulaErrorValue
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
				return this.RefersToRange.FormulaErrorValue;
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
				this.RefersToRange.FormulaErrorValue = value;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06005E39 RID: 24121 RVA: 0x003AF92C File Offset: 0x003AE92C
		public ICommentShape Comment
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
				return this.RefersToRange.Comment;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06005E3A RID: 24122 RVA: 0x003AF974 File Offset: 0x003AE974
		public IRichTextString RichText
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
				return this.RefersToRange.RichText;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06005E3B RID: 24123 RVA: 0x003AF9BC File Offset: 0x003AE9BC
		public bool HasMerged
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
				return this.RefersToRange.HasMerged;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06005E3C RID: 24124 RVA: 0x003AFA04 File Offset: 0x003AEA04
		public IXLSRange MergeArea
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
				return this.RefersToRange.MergeArea;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x003AFA4C File Offset: 0x003AEA4C
		// (set) Token: 0x06005E3E RID: 24126 RVA: 0x003AFA94 File Offset: 0x003AEA94
		public bool IsWrapText
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
				return this.RefersToRange.IsWrapText;
			}
			set
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
				this.RefersToRange.IsWrapText = value;
			}
		}

		// Token: 0x17000F58 RID: 3928
		public IXLSRange this[int row, int column]
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
				return this.RefersToRange[row, column];
			}
			set
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
				this.RefersToRange[row, column] = value;
			}
		}

		// Token: 0x17000F59 RID: 3929
		public IXLSRange this[int row, int column, int lastRow, int lastColumn]
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
				return this.RefersToRange[row, column, lastRow, lastColumn];
			}
		}

		// Token: 0x17000F5A RID: 3930
		public IXLSRange this[string name]
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
				return this[name, false];
			}
		}

		// Token: 0x17000F5B RID: 3931
		public IXLSRange this[string name, bool IsR1C1Notation]
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
				return this.RefersToRange[name, IsR1C1Notation];
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x003AFC48 File Offset: 0x003AEC48
		public bool HasExternalFormula
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
				return this.RefersToRange.HasExternalFormula;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x003AFC90 File Offset: 0x003AEC90
		// (set) Token: 0x06005E46 RID: 24134 RVA: 0x003AFCD8 File Offset: 0x003AECD8
		public IgnoreErrorType IgnoreErrorOptions
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
				return this.RefersToRange.IgnoreErrorOptions;
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
				this.RefersToRange.IgnoreErrorOptions = value;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x003AFD20 File Offset: 0x003AED20
		// (set) Token: 0x06005E48 RID: 24136 RVA: 0x003AFD84 File Offset: 0x003AED84
		public bool? IsStringsPreserved
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_49;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				ICombinedRange combinedRange = this.RefersToRange as ICombinedRange;
				if (combinedRange == null)
				{
					return null;
				}
				IL_49:
				return this.ᜆ.ᜀ(combinedRange);
			}
			set
			{
				for (;;)
				{
					ICombinedRange combinedRange = this.RefersToRange as ICombinedRange;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (combinedRange != null)
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								this.ᜆ.ᜀ(combinedRange, value);
								num = 1;
								continue;
							}
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06005E49 RID: 24137 RVA: 0x003AFE0C File Offset: 0x003AEE0C
		// (set) Token: 0x06005E4A RID: 24138 RVA: 0x003AFE54 File Offset: 0x003AEE54
		public BuiltInStyles? BuiltInStyle
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
				return this.RefersToRange.BuiltInStyle;
			}
			set
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
				this.RefersToRange.BuiltInStyle = value;
			}
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x003AFE9C File Offset: 0x003AEE9C
		public CellRange[] FindAll(TimeSpan findValue)
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
			return ((XlsRange)this.RefersToRange).FindAll(findValue);
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x003AFEE8 File Offset: 0x003AEEE8
		public CellRange[] FindAll(DateTime findValue)
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
			return ((XlsRange)this.RefersToRange).FindAll(findValue);
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x003AFF34 File Offset: 0x003AEF34
		public CellRange[] FindAll(bool findValue)
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
			return ((XlsRange)this.RefersToRange).FindAll(findValue);
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x003AFF80 File Offset: 0x003AEF80
		public CellRange[] FindAll(double findValue, FindType flags)
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
			return ((XlsRange)this.RefersToRange).FindAll(findValue, flags);
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x003AFFD0 File Offset: 0x003AEFD0
		public List<CellRange> FindAll(string findValue, FindType flags)
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
			return ((XlsRange)this.RefersToRange).FindAll(findValue, flags);
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x003B0020 File Offset: 0x003AF020
		public IXLSRange FindFirst(TimeSpan findValue)
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
			return ((XlsRange)this.RefersToRange).FindFirst(findValue);
		}

		// Token: 0x06005E51 RID: 24145 RVA: 0x003B006C File Offset: 0x003AF06C
		public IXLSRange FindFirst(DateTime findValue)
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
			return ((XlsRange)this.RefersToRange).FindFirst(findValue);
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x003B00B8 File Offset: 0x003AF0B8
		public IXLSRange FindFirst(bool findValue)
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
			return ((XlsRange)this.RefersToRange).FindFirst(findValue);
		}

		// Token: 0x06005E53 RID: 24147 RVA: 0x003B0104 File Offset: 0x003AF104
		public IXLSRange FindFirst(double findValue, FindType flags)
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
			return ((XlsRange)this.RefersToRange).FindFirst(findValue, flags);
		}

		// Token: 0x06005E54 RID: 24148 RVA: 0x003B0154 File Offset: 0x003AF154
		public IXLSRange FindFirst(string findValue, FindType flags)
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
			return ((XlsRange)this.RefersToRange).FindFirst(findValue, flags);
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x003B01A4 File Offset: 0x003AF1A4
		public ICommentShape AddComment()
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
			return this.RefersToRange.AddComment();
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x003B01EC File Offset: 0x003AF1EC
		public void AutoFitColumns()
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
			this.RefersToRange.AutoFitColumns();
		}

		// Token: 0x06005E57 RID: 24151 RVA: 0x003B0234 File Offset: 0x003AF234
		public void AutoFitRows()
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
			this.RefersToRange.AutoFitRows();
		}

		// Token: 0x06005E58 RID: 24152 RVA: 0x003B027C File Offset: 0x003AF27C
		public void Merge(bool clearCells)
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
			this.RefersToRange.Merge(clearCells);
		}

		// Token: 0x06005E59 RID: 24153 RVA: 0x003B02C4 File Offset: 0x003AF2C4
		public IXLSRange Merge(IXLSRange range)
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
			return this.RefersToRange.Merge(range);
		}

		// Token: 0x06005E5A RID: 24154 RVA: 0x003B030C File Offset: 0x003AF30C
		public IXLSRange Intersect(IXLSRange range)
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
			return this.RefersToRange.Intersect(range);
		}

		// Token: 0x06005E5B RID: 24155 RVA: 0x003B0354 File Offset: 0x003AF354
		internal IXLSRange ᜀ(IXLSRange A_0, CopyRangeOptions A_1)
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
			return ((XlsRange)this.RefersToRange).ᜁ(A_0, A_1);
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x003B03A4 File Offset: 0x003AF3A4
		public IXLSRange CopyTo(IXLSRange destination)
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
			return ((XlsRange)this.RefersToRange).CopyTo(destination);
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x003B03F0 File Offset: 0x003AF3F0
		public void MoveTo(IXLSRange destination)
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
			((XlsRange)this.RefersToRange).MoveTo(destination);
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x003B043C File Offset: 0x003AF43C
		internal void ᜀ(MoveDirectionType A_0, CopyRangeOptions A_1)
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
			((XlsRange)this.RefersToRange).ᜀ(A_0, A_1);
		}

		// Token: 0x06005E5F RID: 24159 RVA: 0x003B048C File Offset: 0x003AF48C
		public void Clear(MoveDirectionType direction)
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
			((XlsRange)this.RefersToRange).Clear(direction);
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x003B04D8 File Offset: 0x003AF4D8
		public void Clear(ExcelClearOptions option)
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
			this.RefersToRange.Clear(option);
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x003B0520 File Offset: 0x003AF520
		public void Clear(bool isClearFormat)
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
			((XlsRange)this.RefersToRange).Clear(isClearFormat);
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x003B056C File Offset: 0x003AF56C
		public void ClearContents()
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
			this.RefersToRange.ClearContents();
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x003B05B4 File Offset: 0x003AF5B4
		public void FreezePanes()
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
			this.RefersToRange.FreezePanes();
		}

		// Token: 0x06005E64 RID: 24164 RVA: 0x003B05FC File Offset: 0x003AF5FC
		public void UnMerge()
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
			this.RefersToRange.UnMerge();
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x003B0644 File Offset: 0x003AF644
		protected internal IXLSRange Ungroup(GroupByType groupBy)
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
			return ((XlsRange)this.RefersToRange).Ungroup(groupBy);
		}

		// Token: 0x06005E66 RID: 24166 RVA: 0x003B0690 File Offset: 0x003AF690
		public void Merge()
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
			this.RefersToRange.Merge();
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x003B06D8 File Offset: 0x003AF6D8
		protected internal IXLSRange Group(GroupByType groupBy, bool bCollapsed)
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
			return ((XlsRange)this.RefersToRange).Group(groupBy, bCollapsed);
		}

		// Token: 0x06005E68 RID: 24168 RVA: 0x003B0728 File Offset: 0x003AF728
		protected internal IXLSRange Group(GroupByType groupBy)
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
			return ((XlsRange)this.RefersToRange).Group(groupBy);
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x003B0774 File Offset: 0x003AF774
		public IXLSRange Activate()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_46;
			}
			if (false)
			{
			}
			if (this.RefersToRange is CellRange)
			{
				return ((XlsRange)this.RefersToRange).Activate();
			}
			IL_46:
			if (true)
			{
			}
			return null;
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x003B07D0 File Offset: 0x003AF7D0
		public IXLSRange Activate(bool scroll)
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
			return this.RefersToRange.Activate(scroll);
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x003B0818 File Offset: 0x003AF818
		public void BorderAround()
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
			this.BorderAround(LineStyleType.Thin);
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x003B085C File Offset: 0x003AF85C
		public void BorderAround(LineStyleType borderLine)
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
			this.BorderAround(borderLine, ExcelColors.Black);
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x003B08A0 File Offset: 0x003AF8A0
		public void BorderAround(LineStyleType borderLine, Color borderColor)
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
			ExcelColors nearestColor = this.ᜅ.GetNearestColor(borderColor);
			this.BorderAround(borderLine, nearestColor);
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x003B08F0 File Offset: 0x003AF8F0
		public void BorderAround(LineStyleType borderLine, ExcelColors borderColor)
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
			this.RefersToRange.BorderAround(borderLine, borderColor);
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x003B0938 File Offset: 0x003AF938
		public void BorderInside()
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
			this.BorderInside(LineStyleType.Thin);
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x003B097C File Offset: 0x003AF97C
		public void BorderInside(LineStyleType borderLine)
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
			this.BorderInside(borderLine, ExcelColors.Black);
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x003B09C0 File Offset: 0x003AF9C0
		public void BorderInside(LineStyleType borderLine, Color borderColor)
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
			ExcelColors nearestColor = this.ᜅ.GetNearestColor(borderColor);
			this.BorderInside(borderLine, nearestColor);
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x003B0A10 File Offset: 0x003AFA10
		public void BorderInside(LineStyleType borderLine, ExcelColors borderColor)
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
			this.RefersToRange.BorderInside(borderLine, borderColor);
		}

		// Token: 0x06005E73 RID: 24179 RVA: 0x003B0A58 File Offset: 0x003AFA58
		public void BorderNone()
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
			this.RefersToRange.BorderNone();
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x003B0AA0 File Offset: 0x003AFAA0
		public void CollapseGroup(GroupByType groupBy)
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
			(this.RefersToRange as XlsRange).CollapseGroup(groupBy);
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x003B0AEC File Offset: 0x003AFAEC
		public void ExpandGroup(GroupByType groupBy)
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
			this.RefersToRange.ExpandGroup(groupBy);
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x003B0B34 File Offset: 0x003AFB34
		public void ExpandGroup(GroupByType groupBy, ExpandCollapseFlags flags)
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
			(this.RefersToRange as XlsRange).ExpandGroup(groupBy, flags);
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06005E77 RID: 24183 RVA: 0x003B0B84 File Offset: 0x003AFB84
		internal sprῚ Record
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
				return this.ᜄ;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06005E78 RID: 24184 RVA: 0x003B0BC8 File Offset: 0x003AFBC8
		IWorksheet IXLSRange.Worksheet
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3C;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				IXLSRange refersToRange = this.RefersToRange;
				if (refersToRange == null)
				{
					return null;
				}
				IL_3C:
				return refersToRange.Worksheet;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06005E79 RID: 24185 RVA: 0x003B0C18 File Offset: 0x003AFC18
		public XlsWorksheet Worksheet
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
				return this.ᜆ;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06005E7A RID: 24186 RVA: 0x003B0C5C File Offset: 0x003AFC5C
		internal XlsWorkbook Workbook
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
				return this.ᜅ;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06005E7B RID: 24187 RVA: 0x003B0CA0 File Offset: 0x003AFCA0
		public bool IsExternName
		{
			get
			{
				int num = 3;
				for (;;)
				{
					int num2;
					int reference;
					switch (num)
					{
					case 0:
						goto IL_9B;
					case 1:
						return false;
					case 2:
						goto IL_9B;
					case 4:
					{
						if (true)
						{
						}
						int num3;
						if (num2 >= num3)
						{
							num = 1;
							continue;
						}
						num = 6;
						continue;
					}
					case 5:
						if (this.ᜄ.ᜈ() != null)
						{
							num2 = 0;
							int num3 = this.ᜄ.ᜈ().Length;
							num = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_54;
						default:
							if (false)
							{
							}
							num = 10;
							continue;
						}
						break;
					case 6:
						if (this.ᜄ.ᜈ()[num2] is sprẄ)
						{
							num = 7;
							continue;
						}
						goto IL_103;
					case 7:
						goto IL_54;
					case 8:
						return true;
					case 9:
						if (this.ᜅ.IsExternalReference(reference))
						{
							num = 8;
							continue;
						}
						goto IL_103;
					case 10:
						goto IL_101;
					case 11:
						num = 5;
						continue;
					}
					if (this.ᜄ != null)
					{
						num = 11;
						continue;
					}
					return false;
					IL_54:
					reference = (int)(this.ᜄ.ᜈ()[num2] as sprẄ).ᜁ();
					num = 9;
					continue;
					IL_9B:
					num = 4;
					continue;
					IL_103:
					num2++;
					num = 0;
				}
				return true;
				IL_101:
				return false;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06005E7C RID: 24188 RVA: 0x003B0E14 File Offset: 0x003AFE14
		// (set) Token: 0x06005E7D RID: 24189 RVA: 0x003B0FB0 File Offset: 0x003AFFB0
		internal spr\u25A6.ᜀ Region
		{
			get
			{
				switch (0)
				{
				default:
				{
					long num2;
					long a_;
					for (;;)
					{
						string rangeAddressLocal = this.RangeAddressLocal;
						int num = 2;
						for (;;)
						{
							string[] array;
							switch (num)
							{
							case 0:
								goto IL_CD;
							case 1:
								if (array.Length == 2)
								{
									num = 7;
									continue;
								}
								goto IL_F7;
							case 2:
								if (rangeAddressLocal == null)
								{
									num = 4;
									continue;
								}
								goto IL_76;
							case 3:
							{
								if (true)
								{
								}
								spr\u25A6.ᜀ result;
								try
								{
									num2 = sprṔ.ᜁ(array[0]);
									a_ = num2;
									goto IL_D4;
								}
								catch (ArgumentException)
								{
									result = null;
								}
								return result;
							}
							case 4:
								goto IL_5D;
							case 5:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_CD;
								default:
									if (false)
									{
									}
									if (array.Length > 2)
									{
										num = 0;
										continue;
									}
									num = 6;
									continue;
								}
								break;
							case 6:
								if (array.Length >= 1)
								{
									num = 3;
									continue;
								}
								goto IL_D4;
							case 7:
								try
								{
									a_ = sprṔ.ᜁ(array[1]);
									goto IL_F7;
								}
								catch (ArgumentException)
								{
									return null;
								}
								goto IL_76;
							}
							break;
							IL_76:
							array = rangeAddressLocal.Split(new char[]
							{
								':'
							});
							num2 = 0L;
							a_ = 0L;
							num = 5;
							continue;
							IL_D4:
							num = 1;
						}
					}
					IL_5D:
					return null;
					IL_CD:
					return null;
					IL_F7:
					ushort a_2 = (ushort)(sprṔ.ᜁ(num2) - 1);
					ushort a_3 = (ushort)(sprṔ.ᜀ(num2) - 1);
					ushort a_4 = (ushort)(sprṔ.ᜁ(a_) - 1);
					ushort a_5 = (ushort)(sprṔ.ᜀ(a_) - 1);
					return new spr\u25A6.ᜀ((int)a_2, (int)a_4, (int)a_3, (int)a_5);
				}
				}
			}
			set
			{
				int a_ = 6;
				switch (0)
				{
				default:
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_F9:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						num = 4;
						break;
					}
					string text;
					string text2;
					string text3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_105;
						case 1:
							goto IL_198;
						case 2:
						{
							if (this.Region == value)
							{
								num = 5;
								continue;
							}
							int a_2 = value.ᜂ() + 1;
							int a_3 = value.ᜅ() + 1;
							value.ᜇ();
							value.ᜃ();
							text = sprṔ.ᜀ(a_3, a_2, false, true);
							text2 = sprṔ.ᜀ(a_3, a_2, false, true);
							string value2 = this.Value;
							int num2 = value2.IndexOf(RecordTableEnumerator.b("ᴻ", a_));
							num = 3;
							continue;
						}
						case 3:
						{
							int num2;
							if (num2 < 1)
							{
								goto IL_F9;
							}
							string value2;
							text3 = value2.Substring(0, num2);
							num = 6;
							continue;
						}
						case 5:
							return;
						case 6:
							if (text == text2)
							{
								num = 1;
								continue;
							}
							goto IL_19B;
						case 7:
							goto IL_7C;
						}
						if (true)
						{
						}
						if (value == null)
						{
							num = 7;
						}
						else
						{
							num = 2;
						}
					}
					IL_7C:
					throw new ArgumentNullException(RecordTableEnumerator.b("渻嬽✿⭁⭃⡅", a_));
					IL_105:
					throw new NotSupportedException(RecordTableEnumerator.b("缻弽⸿ⱁ⭃㉅桇ⱉ╋⁍㑏牑❓㹕㵗㽙⡛繝๟͡ॣͥ䡧ᥩ५ṭᅯqᕳɵᵷࡹ剻", a_));
					IL_198:
					this.Value = text3 + RecordTableEnumerator.b("ᴻ", a_) + text;
					return;
					IL_19B:
					this.Value = string.Format(RecordTableEnumerator.b("䜻ఽ㴿捁㽃癅㕇灉㝋罍ⵏ", a_), text, text2, text3);
					return;
				}
				}
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06005E7E RID: 24190 RVA: 0x003B1178 File Offset: 0x003B0178
		// (set) Token: 0x06005E7F RID: 24191 RVA: 0x003B11C0 File Offset: 0x003B01C0
		public bool IsBuiltIn
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
				return this.ᜄ.\u1716();
			}
			set
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
				this.ᜄ.ᜃ(value);
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06005E80 RID: 24192 RVA: 0x003B1208 File Offset: 0x003B0208
		public int NameIndexChangedHandlersCount
		{
			get
			{
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
					if (this.ᜋ == null)
					{
						return 0;
					}
					break;
				}
				return this.ᜋ.GetInvocationList().Length;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06005E81 RID: 24193 RVA: 0x003B125C File Offset: 0x003B025C
		// (set) Token: 0x06005E82 RID: 24194 RVA: 0x003B12A4 File Offset: 0x003B02A4
		public bool IsFunction
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
				return this.ᜄ.ᜀ();
			}
			set
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
				this.ᜄ.ᜆ(value);
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06005E83 RID: 24195 RVA: 0x003B12EC File Offset: 0x003B02EC
		// (set) Token: 0x06005E84 RID: 24196 RVA: 0x003B1330 File Offset: 0x003B0330
		internal bool IsNumReference
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
				return this.ᜈ;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06005E85 RID: 24197 RVA: 0x003B1374 File Offset: 0x003B0374
		// (set) Token: 0x06005E86 RID: 24198 RVA: 0x003B13B8 File Offset: 0x003B03B8
		internal bool IsStringReference
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
				return this.ᜊ;
			}
			set
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06005E87 RID: 24199 RVA: 0x003B13FC File Offset: 0x003B03FC
		// (set) Token: 0x06005E88 RID: 24200 RVA: 0x003B1440 File Offset: 0x003B0440
		internal bool IsMultiReference
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06005E89 RID: 24201 RVA: 0x003B1484 File Offset: 0x003B0484
		// (remove) Token: 0x06005E8A RID: 24202 RVA: 0x003B1518 File Offset: 0x003B0518
		public event XlsName.NameIndexChangedEventHandler NameIndexChanged
		{
			add
			{
				for (;;)
				{
					IL_00:
					for (;;)
					{
						IL_3A:
						XlsName.NameIndexChangedEventHandler nameIndexChangedEventHandler = this.ᜋ;
						int num = 1;
						for (;;)
						{
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
								XlsName.NameIndexChangedEventHandler nameIndexChangedEventHandler2;
								switch (num)
								{
								case 0:
									return;
								case 1:
									goto IL_4B;
								case 2:
									if (nameIndexChangedEventHandler == nameIndexChangedEventHandler2)
									{
										if (true)
										{
										}
										num = 0;
										continue;
									}
									goto IL_4B;
								}
								goto IL_3A;
								IL_4B:
								nameIndexChangedEventHandler2 = nameIndexChangedEventHandler;
								XlsName.NameIndexChangedEventHandler value2 = (XlsName.NameIndexChangedEventHandler)Delegate.Combine(nameIndexChangedEventHandler2, value);
								nameIndexChangedEventHandler = Interlocked.CompareExchange<XlsName.NameIndexChangedEventHandler>(ref this.ᜋ, value2, nameIndexChangedEventHandler2);
								num = 2;
								break;
							}
							}
						}
					}
				}
			}
			remove
			{
				for (;;)
				{
					IL_00:
					if (true)
					{
					}
					for (;;)
					{
						IL_42:
						XlsName.NameIndexChangedEventHandler nameIndexChangedEventHandler = this.ᜋ;
						int num = 1;
						for (;;)
						{
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
								XlsName.NameIndexChangedEventHandler nameIndexChangedEventHandler2;
								switch (num)
								{
								case 0:
									if (nameIndexChangedEventHandler == nameIndexChangedEventHandler2)
									{
										num = 2;
										continue;
									}
									goto IL_53;
								case 1:
									goto IL_53;
								case 2:
									return;
								}
								goto IL_42;
								IL_53:
								nameIndexChangedEventHandler2 = nameIndexChangedEventHandler;
								XlsName.NameIndexChangedEventHandler value2 = (XlsName.NameIndexChangedEventHandler)Delegate.Remove(nameIndexChangedEventHandler2, value);
								nameIndexChangedEventHandler = Interlocked.CompareExchange<XlsName.NameIndexChangedEventHandler>(ref this.ᜋ, value2, nameIndexChangedEventHandler2);
								num = 0;
								break;
							}
							}
						}
					}
				}
			}
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x003B15AC File Offset: 0x003B05AC
		public void Delete()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (this.ᜆ == null)
				{
					this.ᜅ.Names.RemoveAt(this.Index);
					return;
				}
				break;
			}
			this.ᜆ.Names.Remove(this.Name);
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x003B1620 File Offset: 0x003B0620
		private void ᜀ()
		{
			int a_ = 2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				for (;;)
				{
					this.ᜆ = (base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet);
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜅ == null)
							{
								num = 3;
								continue;
							}
							return;
						case 1:
							goto IL_70;
						case 2:
							if (this.ᜆ != null)
							{
								num = 1;
								continue;
							}
							this.ᜅ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
							if (true)
							{
							}
							num = 0;
							continue;
						case 3:
							goto IL_D1;
						}
						break;
					}
				}
				IL_70:
				this.ᜅ = (this.ᜆ.Workbook as XlsWorkbook);
				return;
			}
			IL_D1:
			throw new ArgumentNullException(RecordTableEnumerator.b("样嬹主嬽⸿㙁摃⥅⩇⁉⥋ⵍ⑏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥๧թᥫmᑯ山", a_));
		}

		// Token: 0x06005E8D RID: 24205 RVA: 0x003B1718 File Offset: 0x003B0718
		internal void ᜀ(sprῚ A_0)
		{
			int a_ = 18;
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (A_0 != null)
				{
					this.ᜄ = (sprῚ)A_0.Clone();
					return;
				}
				break;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("♇⭉⅋⭍", a_));
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x003B1788 File Offset: 0x003B0788
		private void ᜀ(string A_0, string A_1, bool A_2)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				ParseFormulaOptions parseFormulaOptions;
				Dictionary<Type, sprᨳ> dictionary;
				switch (num)
				{
				case 1:
					return;
				case 2:
					parseFormulaOptions |= ParseFormulaOptions.UseR1C1;
					num = 3;
					continue;
				case 3:
					goto IL_62;
				case 4:
					if (A_2)
					{
						num = 2;
						continue;
					}
					goto IL_62;
				case 5:
					if (true)
					{
					}
					dictionary = new Dictionary<Type, sprᨳ>();
					dictionary.Add(typeof(spr\u1BFD), new sprᨳ(1));
					dictionary.Add(typeof(sprᣋ), new sprᨳ(1));
					parseFormulaOptions = (ParseFormulaOptions.RootLevel | ParseFormulaOptions.InName);
					num = 4;
					continue;
				}
				if (A_0 != A_1)
				{
					num = 5;
					continue;
				}
				break;
				IL_62:
				this.ᜄ.ᜀ(this.ᜅ.FormulaUtil.ᜁ(A_1, this.ᜆ, dictionary, 0, null, parseFormulaOptions, 0, 0));
				this.ᜀ(new NameIndexChangedEventArgs(this.Index, this.Index));
				num = 1;
			}
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x003B18B4 File Offset: 0x003B08B4
		private void ᜀ(NameIndexChangedEventArgs A_0)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6D:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_52;
				case 2:
					goto IL_75;
				}
				if (this.ᜋ == null)
				{
					goto IL_77;
				}
				num = 1;
			}
			IL_52:
			this.ᜋ.GetInvocationList();
			this.ᜋ(this, A_0);
			goto IL_6D;
			IL_75:
			IL_77:
			if (true)
			{
			}
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x003B1940 File Offset: 0x003B0940
		private bool ᜀ(string A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						char c;
						if (!char.IsLetterOrDigit(c))
						{
							num = 6;
							continue;
						}
						goto IL_BA;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CC;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					case 2:
					{
						int length;
						if (num2 >= length)
						{
							num = 12;
							continue;
						}
						char c = A_0[num2];
						num = 0;
						continue;
					}
					case 3:
						goto IL_11A;
					case 4:
					{
						int num3;
						if (num3 > sprῚ.ᜂ.Length)
						{
							num = 8;
							continue;
						}
						goto IL_BA;
					}
					case 5:
						goto IL_11A;
					case 6:
						num = 7;
						continue;
					case 7:
					{
						char c;
						if (Array.IndexOf<char>(XlsName.ᜃ, c) == -1)
						{
							num = 9;
							continue;
						}
						goto IL_BA;
					}
					case 8:
						return false;
					case 9:
					{
						char c;
						int num3 = (int)c;
						num = 4;
						continue;
					}
					case 10:
						goto IL_CC;
					case 11:
						goto IL_EF;
					case 12:
						return true;
					case 13:
					{
						if (A_0.Length == 0)
						{
							num = 11;
							continue;
						}
						num2 = 0;
						int length = A_0.Length;
						num = 3;
						continue;
					}
					}
					if (A_0 != null)
					{
						num = 10;
						continue;
					}
					break;
					IL_BA:
					num2++;
					num = 5;
					continue;
					IL_CC:
					num = 13;
					continue;
					IL_11A:
					num = 2;
				}
				return false;
				IL_EF:
				return false;
			}
			}
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x003B1AD0 File Offset: 0x003B0AD0
		private void ᜀ(string A_0, bool A_1)
		{
			int num = 3;
			for (;;)
			{
				string value;
				switch (num)
				{
				case 0:
					if (A_0.Length > 0)
					{
						num = 5;
						continue;
					}
					goto IL_4F;
				case 1:
					if (value != A_0)
					{
						num = 6;
						continue;
					}
					return;
				case 2:
					A_0 = A_0.Substring(1);
					num = 7;
					continue;
				case 4:
					if (A_0[0] == '=')
					{
						num = 2;
						continue;
					}
					goto IL_4F;
				case 5:
					num = 4;
					continue;
				case 6:
					this.ᜀ(value, A_0, A_1);
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
						num = 9;
						continue;
					}
					break;
				case 7:
					goto IL_4F;
				case 8:
					num = 0;
					continue;
				case 9:
					return;
				}
				IL_42:
				if (A_0 != null)
				{
					num = 8;
					continue;
				}
				goto IL_4F;
				goto IL_42;
				IL_4F:
				value = this.Value;
				num = 1;
			}
		}

		// Token: 0x06005E92 RID: 24210 RVA: 0x003B1BE8 File Offset: 0x003B0BE8
		public void ConvertFullRowColumnName(ExcelVersion version)
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
			spr᱒.ᜀ(this.ᜄ.ᜈ(), version != ExcelVersion.Version2007 && version != ExcelVersion.Version2010);
		}

		// Token: 0x06005E93 RID: 24211 RVA: 0x003B1C48 File Offset: 0x003B0C48
		public string GetValue(FormulaUtil formulaUtil)
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
			return formulaUtil.ᜁ(this.ᜄ.ᜈ());
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x003B1C94 File Offset: 0x003B0C94
		public void SetIndex(int index)
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
			this.SetIndex(index, true);
		}

		// Token: 0x06005E95 RID: 24213 RVA: 0x003B1CD8 File Offset: 0x003B0CD8
		public void SetIndex(int index, bool bRaiseEvent)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6C;
				case 1:
				{
					int oldIndex = this.ᜇ;
					this.ᜇ = index;
					num = 0;
					continue;
				}
				case 2:
					return;
				case 3:
					if (true)
					{
					}
					break;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6C;
					default:
					{
						if (false)
						{
						}
						int oldIndex;
						this.ᜀ(new NameIndexChangedEventArgs(oldIndex, index));
						num = 2;
						continue;
					}
					}
					break;
				}
				if (index != this.ᜇ)
				{
					num = 1;
					continue;
				}
				break;
				IL_6C:
				if (!bRaiseEvent)
				{
					break;
				}
				num = 4;
			}
		}

		// Token: 0x06005E96 RID: 24214 RVA: 0x003B1D84 File Offset: 0x003B0D84
		internal void ᜀ(RecordArrayList A_0)
		{
			int a_ = 12;
			if (true)
			{
			}
			if (A_0 != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					A_0.ᜀ(this.ᜄ);
					return;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃╅❇㡉⡋㵍", a_));
		}

		// Token: 0x06005E97 RID: 24215 RVA: 0x003B1DF0 File Offset: 0x003B0DF0
		public void SetSheetIndex(int iSheetIndex)
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
			this.ᜄ.ᜀ((ushort)(iSheetIndex + 1));
		}

		// Token: 0x06005E98 RID: 24216 RVA: 0x003B1E3C File Offset: 0x003B0E3C
		internal void ᜀ(Ptg[] A_0)
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
			this.ᜄ.ᜀ(A_0);
			this.ᜀ(new NameIndexChangedEventArgs(this.Index, this.Index));
		}

		// Token: 0x06005E99 RID: 24217 RVA: 0x003B1E9C File Offset: 0x003B0E9C
		void spr\u1D46.Parse()
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
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x003B1ED8 File Offset: 0x003B0ED8
		public Ptg[] GetNativePtg()
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
			Ptg[] array = new Ptg[1];
			int supIndex = this.ᜅ.ExternWorkbooks.InsertSelfSupbook();
			int num = this.ᜅ.AddSheetReference(supIndex, 65534, 65534);
			array[0] = FormulaUtil.ᜀ(FormulaToken.tNameX1, new object[]
			{
				num,
				this.Index
			});
			return array;
		}

		// Token: 0x06005E9B RID: 24219 RVA: 0x003B1F6C File Offset: 0x003B0F6C
		public object Clone(object parent)
		{
			XlsName xlsName;
			for (;;)
			{
				for (;;)
				{
					xlsName = (XlsName)base.MemberwiseClone();
					xlsName.SetParent(parent);
					xlsName.ᜀ();
					xlsName.ᜄ = (sprῚ)spr\u1CD3.ᜀ(this.ᜄ);
					int num = (int)this.ᜄ.ᜃ();
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							num--;
							XlsWorksheet xlsWorksheet = (XlsWorksheet)xlsName.ᜅ.Objects[num];
							xlsWorksheet.InnerNames.ᜀ(xlsName);
							xlsName.ᜆ = xlsWorksheet;
							num2 = 2;
							continue;
						}
						case 1:
							if (num != 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_BC;
						case 2:
							goto IL_9E;
						}
						break;
					}
				}
				IL_9E:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_B4;
				}
			}
			IL_B4:
			if (false)
			{
			}
			IL_BC:
			if (true)
			{
			}
			return xlsName;
		}

		// Token: 0x06005E9C RID: 24220 RVA: 0x003B2048 File Offset: 0x003B1048
		public IEnumerator GetEnumerator()
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
			return this.RefersToRange.GetEnumerator();
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x003B2090 File Offset: 0x003B1090
		public string GetNewRangeLocation(Dictionary<string, string> names, out string strSheetName)
		{
			int a_ = 17;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ፆⅈ⹊浌≎㑐❒㵔㡖㵘筚㉜ⵞ䅠ౢᕤɦ᭨੪ᥬٮṰᵲ啴Ṷ੸孺፼ၾꎂ麗ﶒﶘ떚", a_));
		}

		// Token: 0x06005E9E RID: 24222 RVA: 0x003B20E8 File Offset: 0x003B10E8
		public IXLSRange Clone(object parent, Dictionary<string, string> hashNewNames, XlsWorkbook book)
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
		}

		// Token: 0x06005E9F RID: 24223 RVA: 0x003B2140 File Offset: 0x003B1140
		public void ClearConditionalFormats()
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x003B2198 File Offset: 0x003B1198
		public Rectangle[] GetRectangles()
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x003B21F0 File Offset: 0x003B11F0
		public int GetRectanglesCount()
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᵈ⍊⡌潎㱐㙒⅔㽖㙘㽚絜ぞ፠䍢੤ᝦ౨ᥪ౬᭮ᡰᱲ᭴坶ၸࡺ嵼ᅾꖄﮊﲐﮔﲘﾚ뎜", a_));
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06005EA2 RID: 24226 RVA: 0x003B2248 File Offset: 0x003B1248
		public int CellsCount
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
				return 0;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06005EA3 RID: 24227 RVA: 0x003B2284 File Offset: 0x003B1284
		public string RangeGlobalAddress2007
		{
			get
			{
				int a_ = 4;
				if (this.ᜆ != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return string.Format(RecordTableEnumerator.b("ᴹ䜻฽㴿敁敃㵅祇㝉", a_), this.ᜆ.Name, this.Name);
					}
				}
				if (true)
				{
				}
				return RecordTableEnumerator.b("愹఻挽愿", a_) + this.Name;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06005EA4 RID: 24228 RVA: 0x003B2310 File Offset: 0x003B1310
		public string WorksheetName
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
				return this.Worksheet.Name;
			}
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x003B2358 File Offset: 0x003B1358
		internal void ᜃ()
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
			this.ᜄ.ClearData();
			this.ᜄ = null;
			this.Dispose();
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x003B23AC File Offset: 0x003B13AC
		void IDisposable.Dispose()
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
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005EA7 RID: 24231 RVA: 0x003B23F0 File Offset: 0x003B13F0
		// Note: this type is marked as 'beforefieldinit'.
		static XlsName()
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
			XlsName.ᜃ = new char[]
			{
				'_',
				'?',
				'\\',
				'№',
				'.',
				'#'
			};
		}

		// Token: 0x04002D6C RID: 11628
		private const string ᜀ = "!";

		// Token: 0x04002D6D RID: 11629
		private const string ᜁ = "{2}!{0}:{1}";

		// Token: 0x04002D6E RID: 11630
		public const int DEF_NAME_SHEET_INDEX = 65534;

		// Token: 0x04002D6F RID: 11631
		private const string ᜂ = "Workbook";

		// Token: 0x04002D70 RID: 11632
		private static readonly char[] ᜃ;

		// Token: 0x04002D71 RID: 11633
		private sprῚ ᜄ;

		// Token: 0x04002D72 RID: 11634
		private XlsWorkbook ᜅ;

		// Token: 0x04002D73 RID: 11635
		private XlsWorksheet ᜆ;

		// Token: 0x04002D74 RID: 11636
		private int ᜇ = -1;

		// Token: 0x04002D75 RID: 11637
		private bool ᜈ;

		// Token: 0x04002D76 RID: 11638
		private bool ᜉ;

		// Token: 0x04002D77 RID: 11639
		private bool ᜊ;

		// Token: 0x04002D78 RID: 11640
		private XlsName.NameIndexChangedEventHandler ᜋ;

		// Token: 0x02000618 RID: 1560
		// (Invoke) Token: 0x06005EA9 RID: 24233
		public delegate void NameIndexChangedEventHandler(object sender, NameIndexChangedEventArgs data);
	}
}
