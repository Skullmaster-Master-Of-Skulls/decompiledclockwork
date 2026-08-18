using System;
using System.Drawing;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000292 RID: 658
	public class XlsBorder : XlsObject, IBorder, IDisposable
	{
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x0016146C File Offset: 0x0016046C
		// (set) Token: 0x060026E2 RID: 9954 RVA: 0x001614C0 File Offset: 0x001604C0
		public ExcelColors KnownColor
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
				return this.OColor.ᜂ(this.ᜃ.Workbook);
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
				value = XlsBorder.ColorToExcelColor(value);
				this.ᜃ.BeginUpdate();
				this.OColor.SetKnownColor(value);
				this.ᜃ.EndUpdate();
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x00161528 File Offset: 0x00160528
		public OColor OColor
		{
			get
			{
				int a_ = 11;
				for (;;)
				{
					BordersLineType bordersLineType = this.ᜂ;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_B7;
						case 2:
							switch (bordersLineType)
							{
							case BordersLineType.DiagonalDown:
								goto IL_C1;
							case BordersLineType.DiagonalUp:
								goto IL_A0;
							case BordersLineType.EdgeLeft:
								goto IL_88;
							case BordersLineType.EdgeTop:
								goto IL_94;
							case BordersLineType.EdgeBottom:
								goto IL_CD;
							case BordersLineType.EdgeRight:
								goto IL_7C;
							default:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_CD;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							}
							break;
						}
						break;
					}
				}
				IL_7C:
				return this.ᜃ.RightBorderColor;
				IL_88:
				return this.ᜃ.LeftBorderColor;
				IL_94:
				return this.ᜃ.TopBorderColor;
				IL_A0:
				return this.ᜃ.DiagonalBorderColor;
				IL_B7:
				if (true)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("̀ⱂ㝄⍆ⱈ㥊浌♎㽐㝒ご⽖", a_));
				IL_C1:
				return this.ᜃ.DiagonalBorderColor;
				IL_CD:
				return this.ᜃ.BottomBorderColor;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x00161624 File Offset: 0x00160624
		// (set) Token: 0x060026E5 RID: 9957 RVA: 0x00161670 File Offset: 0x00160670
		public Color Color
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
				return this.OColor.ᜁ(this.Workbook);
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
				this.ᜃ.BeginUpdate();
				this.OColor.ᜀ(value, this.Workbook);
				this.ᜃ.EndUpdate();
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x001616D4 File Offset: 0x001606D4
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x001617D0 File Offset: 0x001607D0
		public LineStyleType LineStyle
		{
			get
			{
				int a_ = 10;
				for (;;)
				{
					BordersLineType bordersLineType = this.ᜂ;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch (bordersLineType)
							{
							case BordersLineType.DiagonalDown:
								goto IL_C1;
							case BordersLineType.DiagonalUp:
								goto IL_A8;
							case BordersLineType.EdgeLeft:
								goto IL_90;
							case BordersLineType.EdgeTop:
								goto IL_9C;
							case BordersLineType.EdgeBottom:
								goto IL_CD;
							case BordersLineType.EdgeRight:
								goto IL_84;
							default:
								if (true)
								{
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_CD;
								default:
									if (false)
									{
									}
									num = 2;
									continue;
								}
								break;
							}
							break;
						case 1:
							goto IL_BF;
						case 2:
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_84:
				return this.ᜃ.RightBorderLineStyle;
				IL_90:
				return this.ᜃ.LeftBorderLineStyle;
				IL_9C:
				return this.ᜃ.TopBorderLineStyle;
				IL_A8:
				return this.ᜃ.DiagonalUpBorderLineStyle;
				IL_BF:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᔿⱁ⽃⥅㽇⑉汋ⱍ㽏⁑こ㍕⩗穙⡛❝ၟݡ䩣", a_));
				IL_C1:
				return this.ᜃ.DiagonalDownBorderLineStyle;
				IL_CD:
				return this.ᜃ.BottomBorderLineStyle;
			}
			set
			{
				int a_ = 13;
				for (;;)
				{
					BordersLineType bordersLineType = this.ᜂ;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_C3;
						case 1:
							if (true)
							{
							}
							switch (bordersLineType)
							{
							case BordersLineType.DiagonalDown:
								goto IL_C5;
							case BordersLineType.DiagonalUp:
								goto IL_AB;
							case BordersLineType.EdgeLeft:
								goto IL_91;
							case BordersLineType.EdgeTop:
								goto IL_9E;
							case BordersLineType.EdgeBottom:
								goto IL_D2;
							case BordersLineType.EdgeRight:
								goto IL_84;
							default:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_D2;
								default:
									if (false)
									{
									}
									num = 2;
									continue;
								}
								break;
							}
							break;
						case 2:
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_84:
				this.ᜃ.RightBorderLineStyle = value;
				return;
				IL_91:
				this.ᜃ.LeftBorderLineStyle = value;
				return;
				IL_9E:
				this.ᜃ.TopBorderLineStyle = value;
				return;
				IL_AB:
				this.ᜃ.DiagonalUpBorderLineStyle = value;
				return;
				IL_C3:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᙂ⭄ⱆ♈㱊⍌潎㍐㱒❔㍖㱘⥚絜⭞ᡠ።d䥦", a_));
				IL_C5:
				this.ᜃ.DiagonalDownBorderLineStyle = value;
				return;
				IL_D2:
				this.ᜃ.BottomBorderLineStyle = value;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x001618D0 File Offset: 0x001608D0
		// (set) Token: 0x060026E9 RID: 9961 RVA: 0x00161970 File Offset: 0x00160970
		public bool ShowDiagonalLine
		{
			get
			{
				for (;;)
				{
					IL_30:
					BordersLineType bordersLineType = this.ᜂ;
					int num = 0;
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
								switch (bordersLineType)
								{
								case BordersLineType.DiagonalDown:
									goto IL_77;
								case BordersLineType.DiagonalUp:
									goto IL_6B;
								default:
									if (true)
									{
									}
									num = 1;
									continue;
								}
								break;
							case 1:
								goto IL_69;
							case 2:
								return false;
							}
							goto IL_30;
						}
						IL_69:
						num = 2;
					}
				}
				IL_6B:
				return this.ᜃ.DiagonalUpVisible;
				IL_77:
				return this.ᜃ.DiagonalDownVisible;
			}
			set
			{
				switch (this.ᜂ)
				{
				case BordersLineType.DiagonalDown:
					break;
				case BordersLineType.DiagonalUp:
					this.ᜃ.DiagonalUpVisible = value;
					return;
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return;
					}
					break;
				}
				if (true)
				{
				}
				this.ᜃ.DiagonalDownVisible = value;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x001619E0 File Offset: 0x001609E0
		// (set) Token: 0x060026EB RID: 9963 RVA: 0x00161A24 File Offset: 0x00160A24
		internal BordersLineType BorderIndex
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
				return this.ᜂ;
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
				this.ᜂ = value;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x00161A68 File Offset: 0x00160A68
		private XlsWorkbook Workbook
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
				return this.ᜃ.Workbook;
			}
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x00161AB0 File Offset: 0x00160AB0
		private XlsBorder(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x00161AC8 File Offset: 0x00160AC8
		private XlsBorder(spr\u1DF5 A_0, object A_1, BordersLineType A_2) : this(A_0, A_1)
		{
			this.ᜂ = A_2;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x00161AE4 File Offset: 0x00160AE4
		internal XlsBorder(spr\u1DF5 A_0, object A_1, IInternalAddtionalFormat A_2, BordersLineType A_3) : this(A_0, A_1, A_3)
		{
			this.ᜃ = A_2;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00161B04 File Offset: 0x00160B04
		public override bool Equals(object obj)
		{
			XlsBorder xlsBorder;
			for (;;)
			{
				xlsBorder = (obj as XlsBorder);
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
						if (xlsBorder.ShowDiagonalLine != this.ShowDiagonalLine)
						{
							return false;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7C;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_7C;
					case 3:
						goto IL_45;
					case 4:
						if (xlsBorder.ᜂ == this.ᜂ)
						{
							num = 2;
							continue;
						}
						return false;
					case 5:
						if (xlsBorder == null)
						{
							num = 3;
							continue;
						}
						num = 4;
						continue;
					case 6:
						if (xlsBorder.LineStyle == this.LineStyle)
						{
							num = 7;
							continue;
						}
						return false;
					case 7:
						goto IL_7A;
					}
					break;
					IL_7C:
					num = 1;
				}
			}
			IL_45:
			if (true)
			{
			}
			return false;
			IL_7A:
			return xlsBorder.OColor == this.OColor;
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00161C0C File Offset: 0x00160C0C
		public override int GetHashCode()
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
			return this.ᜂ.GetHashCode() ^ this.ShowDiagonalLine.GetHashCode() ^ this.LineStyle.GetHashCode() ^ this.OColor.GetHashCode();
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x00161C84 File Offset: 0x00160C84
		public void CopyFrom(IBorder baseBorder)
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
			this.OColor.ᜀ(baseBorder.OColor, true);
			this.LineStyle = baseBorder.LineStyle;
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x00161CE0 File Offset: 0x00160CE0
		private void ᜀ()
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
						return;
					case 1:
						if (this.OColor.ColorType == ColorType.Known)
						{
							num = 3;
							continue;
						}
						return;
					case 2:
						if (true)
						{
						}
						break;
					case 3:
						goto IL_A1;
					case 4:
						num = 1;
						continue;
					}
					if (this.LineStyle != LineStyleType.None)
					{
						num = 4;
						continue;
					}
					return;
				}
				IL_A1:
				OColor ocolor = this.OColor;
				ExcelColors excelColors = ocolor.ᜂ(null);
				excelColors = XlsBorder.ColorToExcelColor(excelColors);
				ocolor.SetKnownColor(excelColors);
				num = 0;
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x00161D9C File Offset: 0x00160D9C
		public static ExcelColors ColorToExcelColor(ExcelColors color)
		{
			int num;
			for (;;)
			{
				num = (int)color;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return (ExcelColors)num;
					case 1:
						if (num == 0)
						{
							num2 = 2;
							continue;
						}
						return (ExcelColors)num;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num += 64;
							color = (ExcelColors)num;
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					}
					break;
				}
			}
			return (ExcelColors)num;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x00161E18 File Offset: 0x00160E18
		public XlsBorder Clone(XlsStyle newFormat)
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
			XlsBorder xlsBorder = base.MemberwiseClone() as XlsBorder;
			xlsBorder.ᜃ = newFormat;
			return xlsBorder;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x00161E68 File Offset: 0x00160E68
		void IDisposable.Dispose()
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
			GC.SuppressFinalize(this);
		}

		// Token: 0x04001329 RID: 4905
		internal const int ᜀ = 8;

		// Token: 0x0400132A RID: 4906
		private byte[] \u25D8\u009C\u00AC\u0092;

		// Token: 0x0400132B RID: 4907
		internal const int ᜁ = 64;

		// Token: 0x0400132C RID: 4908
		private BordersLineType ᜂ;

		// Token: 0x0400132D RID: 4909
		private IInternalAddtionalFormat ᜃ;
	}
}
