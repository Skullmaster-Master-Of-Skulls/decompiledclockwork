using System;
using System.Drawing;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000606 RID: 1542
	public class XlsFill
	{
		// Token: 0x06005B4B RID: 23371 RVA: 0x0038EEC8 File Offset: 0x0038DEC8
		public XlsFill()
		{
			this.ᜀ = new OColor(ExcelColors.BlackCustom);
			this.ᜁ = new OColor((ExcelColors)65);
			base..ctor();
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x0038EEF8 File Offset: 0x0038DEF8
		internal XlsFill(spr\u192F A_0)
		{
			int a_ = 8;
			this.ᜀ = new OColor(ExcelColors.BlackCustom);
			this.ᜁ = new OColor((ExcelColors)65);
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("堽⼿ぁ⥃❅㱇", a_));
			}
			IGradient gradient = A_0.ᝐ();
			if (gradient != null)
			{
				this.ᜃ = gradient.GradientStyle;
				this.ᜄ = gradient.GradientVariant;
				this.ᜀ = gradient.BackColorObject;
				this.ᜁ = gradient.ForeColorObject;
			}
			else
			{
				this.ᜀ = A_0.ᝄ();
				this.ᜁ = A_0.\u1754();
			}
			this.ᜂ = A_0.ᜤ();
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x0038EFAC File Offset: 0x0038DFAC
		internal XlsFill(ExcelPatternType A_0, Color A_1, Color A_2)
		{
			this.ᜀ = new OColor(ExcelColors.BlackCustom);
			this.ᜁ = new OColor((ExcelColors)65);
			base..ctor();
			this.ᜂ = A_0;
			if (A_0 != ExcelPatternType.None)
			{
				this.ᜀ.ᜀ(A_1);
			}
			if (A_0 != ExcelPatternType.Solid)
			{
				this.ᜁ.ᜀ(A_2);
			}
			this.ᜅ = ((A_0 == ExcelPatternType.Solid) ? ShapeFillType.SolidColor : ShapeFillType.Pattern);
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x0038F01C File Offset: 0x0038E01C
		internal XlsFill(ExcelPatternType A_0, OColor A_1, OColor A_2)
		{
			this.ᜀ = new OColor(ExcelColors.BlackCustom);
			this.ᜁ = new OColor((ExcelColors)65);
			base..ctor();
			this.ᜂ = A_0;
			if (A_0 != ExcelPatternType.None)
			{
				this.ᜀ = A_1;
			}
			if (A_0 != ExcelPatternType.Solid)
			{
				this.ᜁ = A_2;
			}
			this.ᜅ = ((A_0 == ExcelPatternType.Solid) ? ShapeFillType.SolidColor : ShapeFillType.Pattern);
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x0038F080 File Offset: 0x0038E080
		public OColor OColor
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
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06005B50 RID: 23376 RVA: 0x0038F0C4 File Offset: 0x0038E0C4
		public OColor PatternColorObject
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
				return this.ᜁ;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06005B51 RID: 23377 RVA: 0x0038F108 File Offset: 0x0038E108
		// (set) Token: 0x06005B52 RID: 23378 RVA: 0x0038F14C File Offset: 0x0038E14C
		public ExcelPatternType Pattern
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x0038F190 File Offset: 0x0038E190
		// (set) Token: 0x06005B54 RID: 23380 RVA: 0x0038F1D4 File Offset: 0x0038E1D4
		public GradientStyleType GradientStyle
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06005B55 RID: 23381 RVA: 0x0038F218 File Offset: 0x0038E218
		// (set) Token: 0x06005B56 RID: 23382 RVA: 0x0038F25C File Offset: 0x0038E25C
		public GradientVariantsType GradientVariant
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x0038F2A0 File Offset: 0x0038E2A0
		// (set) Token: 0x06005B58 RID: 23384 RVA: 0x0038F2E4 File Offset: 0x0038E2E4
		public ShapeFillType FillType
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x0038F328 File Offset: 0x0038E328
		public override bool Equals(object obj)
		{
			XlsFill xlsFill;
			for (;;)
			{
				if (true)
				{
				}
				xlsFill = (obj as XlsFill);
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.GradientVariant == xlsFill.GradientVariant)
						{
							num = 1;
							continue;
						}
						return false;
					case 1:
						goto IL_12A;
					case 2:
						num = 4;
						continue;
					case 3:
						num = 9;
						continue;
					case 4:
						if (this.Pattern == xlsFill.Pattern)
						{
							num = 10;
							continue;
						}
						return false;
					case 5:
						num = 0;
						continue;
					case 6:
						if (this.GradientStyle == xlsFill.GradientStyle)
						{
							num = 5;
							continue;
						}
						return false;
					case 7:
						if (this.OColor == xlsFill.OColor)
						{
							num = 3;
							continue;
						}
						return false;
					case 8:
						return false;
					case 9:
						if (this.PatternColorObject == xlsFill.PatternColorObject)
						{
							goto IL_14A;
						}
						return false;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14A;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 11:
						if (xlsFill == null)
						{
							num = 8;
							continue;
						}
						num = 7;
						continue;
					}
					break;
					IL_14A:
					num = 2;
				}
			}
			return false;
			IL_12A:
			return this.FillType == xlsFill.FillType;
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x0038F49C File Offset: 0x0038E49C
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
			return this.OColor.GetHashCode() ^ this.PatternColorObject.GetHashCode() ^ this.Pattern.GetHashCode() ^ this.GradientStyle.GetHashCode() ^ this.GradientVariant.GetHashCode() ^ this.FillType.GetHashCode();
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x0038F534 File Offset: 0x0038E534
		public XlsFill Clone()
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
			return (XlsFill)base.MemberwiseClone();
		}

		// Token: 0x04002C8D RID: 11405
		private string \u25D8\u0098\u0092\u0088;

		// Token: 0x04002C8E RID: 11406
		private long \u2593\u008B\u0097\u0081;

		// Token: 0x04002C8F RID: 11407
		private OColor ᜀ;

		// Token: 0x04002C90 RID: 11408
		private OColor ᜁ;

		// Token: 0x04002C91 RID: 11409
		private ExcelPatternType ᜂ;

		// Token: 0x04002C92 RID: 11410
		private GradientStyleType ᜃ;

		// Token: 0x04002C93 RID: 11411
		private GradientVariantsType ᜄ;

		// Token: 0x04002C94 RID: 11412
		private float \u2609\u009C\u008E\u00B0;

		// Token: 0x04002C95 RID: 11413
		private ShapeFillType ᜅ;
	}
}
