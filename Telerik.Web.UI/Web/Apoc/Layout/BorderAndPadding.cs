using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015E1 RID: 5601
	internal class BorderAndPadding : ICloneable
	{
		// Token: 0x0600DA55 RID: 55893 RVA: 0x002FD6C4 File Offset: 0x002FB8C4
		public object Clone()
		{
			BorderAndPadding borderAndPadding = new BorderAndPadding();
			borderAndPadding.padding = (BorderAndPadding.ResolvedCondLength[])this.padding.Clone();
			borderAndPadding.borderInfo = (BorderAndPadding.BorderInfo[])this.borderInfo.Clone();
			for (int i = 0; i < this.padding.Length; i++)
			{
				if (this.padding[i] != null)
				{
					borderAndPadding.padding[i] = (BorderAndPadding.ResolvedCondLength)this.padding[i].Clone();
				}
				if (this.borderInfo[i] != null)
				{
					borderAndPadding.borderInfo[i] = (BorderAndPadding.BorderInfo)this.borderInfo[i].Clone();
				}
			}
			return borderAndPadding;
		}

		// Token: 0x0600DA57 RID: 55895 RVA: 0x002FD77F File Offset: 0x002FB97F
		public void setBorder(int side, int style, CondLength width, ColorType color)
		{
			this.borderInfo[side] = new BorderAndPadding.BorderInfo(style, width, color);
		}

		// Token: 0x0600DA58 RID: 55896 RVA: 0x002FD792 File Offset: 0x002FB992
		public void setPadding(int side, CondLength width)
		{
			this.padding[side] = new BorderAndPadding.ResolvedCondLength(width);
		}

		// Token: 0x0600DA59 RID: 55897 RVA: 0x002FD7A2 File Offset: 0x002FB9A2
		public void setPaddingLength(int side, int iLength)
		{
			this.padding[side].iLength = iLength;
		}

		// Token: 0x0600DA5A RID: 55898 RVA: 0x002FD7B2 File Offset: 0x002FB9B2
		public void setBorderLength(int side, int iLength)
		{
			this.borderInfo[side].mWidth.iLength = iLength;
		}

		// Token: 0x0600DA5B RID: 55899 RVA: 0x002FD7C7 File Offset: 0x002FB9C7
		public int getBorderLeftWidth(bool bDiscard)
		{
			return this.getBorderWidth(3, bDiscard);
		}

		// Token: 0x0600DA5C RID: 55900 RVA: 0x002FD7D1 File Offset: 0x002FB9D1
		public int getBorderRightWidth(bool bDiscard)
		{
			return this.getBorderWidth(1, bDiscard);
		}

		// Token: 0x0600DA5D RID: 55901 RVA: 0x002FD7DB File Offset: 0x002FB9DB
		public int getBorderTopWidth(bool bDiscard)
		{
			return this.getBorderWidth(0, bDiscard);
		}

		// Token: 0x0600DA5E RID: 55902 RVA: 0x002FD7E5 File Offset: 0x002FB9E5
		public int getBorderBottomWidth(bool bDiscard)
		{
			return this.getBorderWidth(2, bDiscard);
		}

		// Token: 0x0600DA5F RID: 55903 RVA: 0x002FD7EF File Offset: 0x002FB9EF
		public int getPaddingLeft(bool bDiscard)
		{
			return this.getPadding(3, bDiscard);
		}

		// Token: 0x0600DA60 RID: 55904 RVA: 0x002FD7F9 File Offset: 0x002FB9F9
		public int getPaddingRight(bool bDiscard)
		{
			return this.getPadding(1, bDiscard);
		}

		// Token: 0x0600DA61 RID: 55905 RVA: 0x002FD803 File Offset: 0x002FBA03
		public int getPaddingBottom(bool bDiscard)
		{
			return this.getPadding(2, bDiscard);
		}

		// Token: 0x0600DA62 RID: 55906 RVA: 0x002FD80D File Offset: 0x002FBA0D
		public int getPaddingTop(bool bDiscard)
		{
			return this.getPadding(0, bDiscard);
		}

		// Token: 0x0600DA63 RID: 55907 RVA: 0x002FD817 File Offset: 0x002FBA17
		private int getBorderWidth(int side, bool bDiscard)
		{
			if (this.borderInfo[side] == null || (bDiscard && this.borderInfo[side].mWidth.bDiscard))
			{
				return 0;
			}
			return this.borderInfo[side].mWidth.iLength;
		}

		// Token: 0x0600DA64 RID: 55908 RVA: 0x002FD84E File Offset: 0x002FBA4E
		public ColorType getBorderColor(int side)
		{
			if (this.borderInfo[side] != null)
			{
				return this.borderInfo[side].mColor;
			}
			return null;
		}

		// Token: 0x0600DA65 RID: 55909 RVA: 0x002FD869 File Offset: 0x002FBA69
		public int getBorderStyle(int side)
		{
			if (this.borderInfo[side] != null)
			{
				return this.borderInfo[side].mStyle;
			}
			return 0;
		}

		// Token: 0x0600DA66 RID: 55910 RVA: 0x002FD884 File Offset: 0x002FBA84
		private int getPadding(int side, bool bDiscard)
		{
			if (this.padding[side] == null || (bDiscard && this.padding[side].bDiscard))
			{
				return 0;
			}
			return this.padding[side].iLength;
		}

		// Token: 0x04003CC1 RID: 15553
		public const int TOP = 0;

		// Token: 0x04003CC2 RID: 15554
		public const int RIGHT = 1;

		// Token: 0x04003CC3 RID: 15555
		public const int BOTTOM = 2;

		// Token: 0x04003CC4 RID: 15556
		public const int LEFT = 3;

		// Token: 0x04003CC5 RID: 15557
		private BorderAndPadding.BorderInfo[] borderInfo = new BorderAndPadding.BorderInfo[4];

		// Token: 0x04003CC6 RID: 15558
		private BorderAndPadding.ResolvedCondLength[] padding = new BorderAndPadding.ResolvedCondLength[4];

		// Token: 0x020015E2 RID: 5602
		internal class ResolvedCondLength : ICloneable
		{
			// Token: 0x0600DA67 RID: 55911 RVA: 0x002FD8B1 File Offset: 0x002FBAB1
			private ResolvedCondLength(int iLength, bool bDiscard)
			{
				this.iLength = iLength;
				this.bDiscard = bDiscard;
			}

			// Token: 0x0600DA68 RID: 55912 RVA: 0x002FD8C7 File Offset: 0x002FBAC7
			internal ResolvedCondLength(CondLength length)
			{
				this.bDiscard = length.IsDiscard();
				this.iLength = length.MValue();
			}

			// Token: 0x0600DA69 RID: 55913 RVA: 0x002FD8E7 File Offset: 0x002FBAE7
			public object Clone()
			{
				return new BorderAndPadding.ResolvedCondLength(this.iLength, this.bDiscard);
			}

			// Token: 0x04003CC7 RID: 15559
			internal int iLength;

			// Token: 0x04003CC8 RID: 15560
			internal bool bDiscard;
		}

		// Token: 0x020015E3 RID: 5603
		internal class BorderInfo : ICloneable
		{
			// Token: 0x0600DA6A RID: 55914 RVA: 0x002FD8FA File Offset: 0x002FBAFA
			internal BorderInfo(int style, CondLength width, ColorType color)
			{
				this.mStyle = style;
				this.mWidth = new BorderAndPadding.ResolvedCondLength(width);
				this.mColor = color;
			}

			// Token: 0x0600DA6B RID: 55915 RVA: 0x002FD91C File Offset: 0x002FBB1C
			private BorderInfo(int style, BorderAndPadding.ResolvedCondLength width, ColorType color)
			{
				this.mStyle = style;
				this.mWidth = width;
				this.mColor = color;
			}

			// Token: 0x0600DA6C RID: 55916 RVA: 0x002FD939 File Offset: 0x002FBB39
			public object Clone()
			{
				return new BorderAndPadding.BorderInfo(this.mStyle, (BorderAndPadding.ResolvedCondLength)this.mWidth.Clone(), (ColorType)this.mColor.Clone());
			}

			// Token: 0x04003CC9 RID: 15561
			internal int mStyle;

			// Token: 0x04003CCA RID: 15562
			internal ColorType mColor;

			// Token: 0x04003CCB RID: 15563
			internal BorderAndPadding.ResolvedCondLength mWidth;
		}
	}
}
