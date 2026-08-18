using System;
using System.Drawing;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Parser.Biff_Records
{
	// Token: 0x020005EB RID: 1515
	[CLSCompliant(false)]
	public struct TAddr
	{
		// Token: 0x060059CC RID: 22988 RVA: 0x003855E8 File Offset: 0x003845E8
		public TAddr(int iFirstRow, int iFirstCol, int iLastRow, int iLastCol)
		{
			this.ᜀ = iFirstRow;
			this.ᜂ = iFirstCol;
			this.ᜁ = iLastRow;
			this.ᜃ = iLastCol;
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x00385614 File Offset: 0x00384614
		public TAddr(int iTopLeftIndex, int iBottomRightIndex)
		{
			this.ᜀ = sprṔ.ᜁ((long)iTopLeftIndex);
			this.ᜂ = sprṔ.ᜀ((long)iTopLeftIndex);
			this.ᜁ = sprṔ.ᜁ((long)iBottomRightIndex);
			this.ᜃ = sprṔ.ᜀ((long)iBottomRightIndex);
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x00385658 File Offset: 0x00384658
		public TAddr(Rectangle rect)
		{
			this.ᜂ = rect.X;
			this.ᜀ = rect.Y;
			this.ᜃ = rect.Right;
			this.ᜁ = rect.Bottom;
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060059CF RID: 22991 RVA: 0x0038569C File Offset: 0x0038469C
		// (set) Token: 0x060059D0 RID: 22992 RVA: 0x003856E0 File Offset: 0x003846E0
		public int FirstCol
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
				return this.ᜂ;
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
				this.ᜂ = value;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060059D1 RID: 22993 RVA: 0x00385724 File Offset: 0x00384724
		// (set) Token: 0x060059D2 RID: 22994 RVA: 0x00385768 File Offset: 0x00384768
		public int FirstRow
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
				return this.ᜀ;
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060059D3 RID: 22995 RVA: 0x003857AC File Offset: 0x003847AC
		// (set) Token: 0x060059D4 RID: 22996 RVA: 0x003857F0 File Offset: 0x003847F0
		public int LastCol
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

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060059D5 RID: 22997 RVA: 0x00385834 File Offset: 0x00384834
		// (set) Token: 0x060059D6 RID: 22998 RVA: 0x00385878 File Offset: 0x00384878
		public int LastRow
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
				this.ᜁ = value;
			}
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x003858BC File Offset: 0x003848BC
		public override string ToString()
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return string.Concat(new string[]
			{
				base.ToString(),
				RecordTableEnumerator.b("敄潆楈", a_),
				this.ᜀ.ToString(),
				RecordTableEnumerator.b("楄杆", a_),
				this.ᜂ.ToString(),
				RecordTableEnumerator.b("敄湆楈晊浌李煐", a_),
				this.ᜁ.ToString(),
				RecordTableEnumerator.b("楄杆", a_),
				this.ᜃ.ToString(),
				RecordTableEnumerator.b("敄湆", a_)
			});
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x003859B0 File Offset: 0x003849B0
		public Rectangle GetRectangle()
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
			return Rectangle.FromLTRB(this.FirstCol, this.FirstRow, this.LastCol, this.LastRow);
		}

		// Token: 0x04002BF7 RID: 11255
		private string \u25D8\u0091\u00A6\u0094;

		// Token: 0x04002BF8 RID: 11256
		private int ᜀ;

		// Token: 0x04002BF9 RID: 11257
		private float[] \u25D8\u0083ª\u009B;

		// Token: 0x04002BFA RID: 11258
		private float \u25D8\u00A8\u0094\u009E;

		// Token: 0x04002BFB RID: 11259
		private int ᜁ;

		// Token: 0x04002BFC RID: 11260
		private float \u2593\u0099\u00A4\u0093;

		// Token: 0x04002BFD RID: 11261
		private int ᜂ;

		// Token: 0x04002BFE RID: 11262
		private string[] \u25D8\u0094\u0089\u009E;

		// Token: 0x04002BFF RID: 11263
		private byte \u2609\u008C\u008A\u00A1;

		// Token: 0x04002C00 RID: 11264
		private int ᜃ;
	}
}
