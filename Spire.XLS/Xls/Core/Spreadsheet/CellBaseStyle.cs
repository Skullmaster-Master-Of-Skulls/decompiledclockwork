using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x020003AE RID: 942
	public class CellBaseStyle : AddtionalFormatWrapper, IStyle
	{
		// Token: 0x06003900 RID: 14592 RVA: 0x001FC418 File Offset: 0x001FB418
		public CellBaseStyle(XlsRange range) : base(range.Workbook)
		{
			this.ᜀ = range;
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x001FC438 File Offset: 0x001FB438
		public CellBaseStyle(XlsRange range, int iXFIndex) : base(range.Workbook, iXFIndex)
		{
			this.ᜀ = range;
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x001FC45C File Offset: 0x001FB45C
		public override void BeginUpdate()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
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
						break;
					}
					break;
				case 1:
					this.BeforeRead();
					this.ᜀ = this.m_book.ᜀ(this.ᜀ);
					num = 2;
					continue;
				case 2:
					goto IL_81;
				}
				if (base.BeginCallsCount != 0)
				{
					break;
				}
				num = 1;
			}
			IL_81:
			base.BeginUpdate();
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x001FC4F4 File Offset: 0x001FB4F4
		public override void EndUpdate()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_60:
				this.ᜀ = this.m_book.ᜀ(this.ᜀ);
				this.ᜀ.ExtendedFormatIndex = (ushort)this.ᜀ.ᜠ();
				num = 2;
				break;
			default:
				if (false)
				{
				}
				goto IL_42;
			}
			for (;;)
			{
				IL_28:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (base.BeginCallsCount == 0)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					goto IL_60;
				case 2:
					return;
				}
				goto IL_42;
			}
			return;
			IL_42:
			base.EndUpdate();
			num = 0;
			goto IL_28;
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x001FC59C File Offset: 0x001FB59C
		protected override void SetParents(object parent)
		{
			int a_ = 1;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				this.ᜀ = (XlsObject.FindParent(parent, typeof(XlsRange)) as XlsRange);
				if (this.ᜀ != null)
				{
					this.m_book = this.ᜀ.Workbook;
					return;
				}
				break;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䜶堸䤺堼儾㕀", a_), RecordTableEnumerator.b("朶堸䤺堼儾㕀捂⩄╆⍈⹊⹌㭎煐げ㑔㥖㝘㑚⥜罞͠٢䕤Ŧ٨Ṫͬ୮彰", a_));
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x001FC638 File Offset: 0x001FB638
		protected override void BeforeRead()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					base.BeforeRead();
					base.SetFormatIndex((int)this.ᜀ.ExtendedFormatIndex);
					if (true)
					{
					}
					num = 0;
					continue;
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
						break;
					}
					break;
				}
				if (base.BeginCallsCount != 0)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x001FC6C4 File Offset: 0x001FB6C4
		public override OColor LeftBorderColor
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
				return base.LeftBorderColor;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x001FC708 File Offset: 0x001FB708
		public override OColor RightBorderColor
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
				return base.RightBorderColor;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06003908 RID: 14600 RVA: 0x001FC74C File Offset: 0x001FB74C
		// (set) Token: 0x06003909 RID: 14601 RVA: 0x001FC790 File Offset: 0x001FB790
		public override LineStyleType LeftBorderLineStyle
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
				return this.GetLeftLineStyle(true);
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
				base.LeftBorderLineStyle = value;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x001FC7D4 File Offset: 0x001FB7D4
		// (set) Token: 0x0600390B RID: 14603 RVA: 0x001FC818 File Offset: 0x001FB818
		public override LineStyleType RightBorderLineStyle
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
				return this.GetRightLineStyle(true);
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
				base.RightBorderLineStyle = value;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x001FC85C File Offset: 0x001FB85C
		// (set) Token: 0x0600390D RID: 14605 RVA: 0x001FC8A0 File Offset: 0x001FB8A0
		public override LineStyleType TopBorderLineStyle
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
				return this.GetTopLineStyle(true);
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
				base.TopBorderLineStyle = value;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x001FC8E4 File Offset: 0x001FB8E4
		// (set) Token: 0x0600390F RID: 14607 RVA: 0x001FC928 File Offset: 0x001FB928
		public override LineStyleType BottomBorderLineStyle
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
				return this.GetBottomLineStyle(true);
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
				base.BottomBorderLineStyle = value;
			}
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x001FC96C File Offset: 0x001FB96C
		protected LineStyleType GetLeftLineStyle(bool askAdjecent)
		{
			IXLSRange ixlsrange;
			LineStyleType lineStyleType;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A9:
				if (ixlsrange == null)
				{
					return lineStyleType;
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				goto IL_40;
			}
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					lineStyleType = ((ixlsrange.Style as CellStyle).Wrapped as CellBaseStyle).GetRightLineStyle(false);
					num = 4;
					continue;
				case 1:
					ixlsrange = this.ᜃ();
					num = 6;
					continue;
				case 2:
					if (lineStyleType == LineStyleType.None)
					{
						num = 3;
						continue;
					}
					return lineStyleType;
				case 3:
					num = 5;
					continue;
				case 4:
					goto IL_95;
				case 5:
					if (askAdjecent)
					{
						num = 1;
						continue;
					}
					return lineStyleType;
				case 6:
					goto IL_A9;
				}
				goto IL_40;
			}
			IL_95:
			return lineStyleType;
			IL_40:
			lineStyleType = base.LeftBorderLineStyle;
			num = 2;
			goto IL_1E;
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x001FCA50 File Offset: 0x001FBA50
		protected LineStyleType GetRightLineStyle(bool askAdjecent)
		{
			IXLSRange ixlsrange;
			LineStyleType lineStyleType;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A3:
				if (ixlsrange == null)
				{
					return lineStyleType;
				}
				num = 1;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_48;
			}
			for (;;)
			{
				IL_26:
				switch (num)
				{
				case 0:
					goto IL_A3;
				case 1:
					lineStyleType = ((ixlsrange.Style as CellStyle).Wrapped as CellBaseStyle).GetLeftLineStyle(false);
					num = 3;
					continue;
				case 2:
					num = 5;
					continue;
				case 3:
					goto IL_92;
				case 4:
					ixlsrange = this.ᜂ();
					num = 0;
					continue;
				case 5:
					if (askAdjecent)
					{
						num = 4;
						continue;
					}
					return lineStyleType;
				case 6:
					if (lineStyleType == LineStyleType.None)
					{
						num = 2;
						continue;
					}
					return lineStyleType;
				}
				goto IL_48;
			}
			IL_92:
			return lineStyleType;
			IL_48:
			lineStyleType = base.RightBorderLineStyle;
			num = 6;
			goto IL_26;
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x001FCB2C File Offset: 0x001FBB2C
		protected LineStyleType GetTopLineStyle(bool askAdjecent)
		{
			IXLSRange ixlsrange;
			LineStyleType lineStyleType;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_9B:
				if (ixlsrange == null)
				{
					return lineStyleType;
				}
				num = 3;
				break;
			default:
				if (false)
				{
				}
				goto IL_40;
			}
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					if (lineStyleType == LineStyleType.None)
					{
						num = 6;
						continue;
					}
					return lineStyleType;
				case 1:
					goto IL_9B;
				case 2:
					if (askAdjecent)
					{
						num = 5;
						continue;
					}
					return lineStyleType;
				case 3:
					lineStyleType = ((ixlsrange.Style as CellStyle).Wrapped as CellBaseStyle).GetBottomLineStyle(false);
					num = 4;
					continue;
				case 4:
					goto IL_8A;
				case 5:
					ixlsrange = this.ᜁ();
					num = 1;
					continue;
				case 6:
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_40;
			}
			IL_8A:
			return lineStyleType;
			IL_40:
			lineStyleType = base.TopBorderLineStyle;
			num = 0;
			goto IL_1E;
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x001FCC08 File Offset: 0x001FBC08
		protected LineStyleType GetBottomLineStyle(bool askAdjecent)
		{
			IXLSRange ixlsrange;
			LineStyleType lineStyleType;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A6:
				if (ixlsrange == null)
				{
					return lineStyleType;
				}
				num = 6;
				break;
			default:
				if (false)
				{
				}
				goto IL_40;
			}
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					ixlsrange = this.ᜀ();
					num = 3;
					continue;
				case 1:
					if (askAdjecent)
					{
						num = 0;
						continue;
					}
					return lineStyleType;
				case 2:
					goto IL_92;
				case 3:
					goto IL_A6;
				case 4:
					if (true)
					{
					}
					if (lineStyleType == LineStyleType.None)
					{
						num = 5;
						continue;
					}
					return lineStyleType;
				case 5:
					num = 1;
					continue;
				case 6:
					lineStyleType = ((ixlsrange.Style as CellStyle).Wrapped as CellBaseStyle).GetTopLineStyle(false);
					num = 2;
					continue;
				}
				goto IL_40;
			}
			IL_92:
			return lineStyleType;
			IL_40:
			lineStyleType = base.BottomBorderLineStyle;
			num = 4;
			goto IL_1E;
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x001FCCE8 File Offset: 0x001FBCE8
		private IXLSRange ᜃ()
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
			return this.ᜀ(0, -1);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x001FCD2C File Offset: 0x001FBD2C
		private IXLSRange ᜂ()
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
			return this.ᜀ(0, 1);
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x001FCD70 File Offset: 0x001FBD70
		private IXLSRange ᜁ()
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
			return this.ᜀ(-1, 0);
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x001FCDB4 File Offset: 0x001FBDB4
		private new IXLSRange ᜀ()
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
			return this.ᜀ(1, 0);
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x001FCDF8 File Offset: 0x001FBDF8
		private new IXLSRange ᜀ(int A_0, int A_1)
		{
			IXLSRange result;
			for (;;)
			{
				IL_50:
				int num = this.ᜀ.Row + A_0;
				int num2 = this.ᜀ.Column + A_1;
				result = null;
				int num3 = 7;
				for (;;)
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
						switch (num3)
						{
						case 0:
							if (num <= this.m_book.MaxRowCount)
							{
								num3 = 2;
								continue;
							}
							return result;
						case 1:
							num3 = 0;
							continue;
						case 2:
							num3 = 8;
							continue;
						case 3:
							goto IL_AE;
						case 4:
							return result;
						case 5:
							if (num2 <= this.m_book.MaxColumnCount)
							{
								num3 = 3;
								continue;
							}
							return result;
						case 6:
							num3 = 5;
							continue;
						case 7:
							if (num > 0)
							{
								num3 = 1;
								continue;
							}
							return result;
						case 8:
							if (num2 > 0)
							{
								num3 = 6;
								continue;
							}
							return result;
						}
						goto IL_50;
					}
					IL_AE:
					result = this.ᜀ[num, num2];
					num3 = 4;
				}
			}
			return result;
		}

		// Token: 0x04001910 RID: 6416
		private long \u2460\u0091\u0097\u00A8;

		// Token: 0x04001911 RID: 6417
		private bool[] \u2460\u0088\u009D\u008A;

		// Token: 0x04001912 RID: 6418
		private long \u2593\u00AE\u0088\u009A;

		// Token: 0x04001913 RID: 6419
		private new XlsRange ᜀ;
	}
}
