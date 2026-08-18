using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Drawing.Printing
{
	// Token: 0x02000055 RID: 85
	[TypeConverter(typeof(MarginsConverter))]
	[Serializable]
	public class Margins : ICloneable
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0001CA88 File Offset: 0x0001AC88
		public Margins() : this(100, 100, 100, 100)
		{
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001CA98 File Offset: 0x0001AC98
		public Margins(int left, int right, int top, int bottom)
		{
			this.CheckMargin(left, "left");
			this.CheckMargin(right, "right");
			this.CheckMargin(top, "top");
			this.CheckMargin(bottom, "bottom");
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
			this.doubleLeft = (double)left;
			this.doubleRight = (double)right;
			this.doubleTop = (double)top;
			this.doubleBottom = (double)bottom;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001CB1A File Offset: 0x0001AD1A
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0001CB22 File Offset: 0x0001AD22
		public int Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.CheckMargin(value, "Left");
				this.left = value;
				this.doubleLeft = (double)value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001CB3F File Offset: 0x0001AD3F
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001CB47 File Offset: 0x0001AD47
		public int Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.CheckMargin(value, "Right");
				this.right = value;
				this.doubleRight = (double)value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001CB64 File Offset: 0x0001AD64
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0001CB6C File Offset: 0x0001AD6C
		public int Top
		{
			get
			{
				return this.top;
			}
			set
			{
				this.CheckMargin(value, "Top");
				this.top = value;
				this.doubleTop = (double)value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001CB89 File Offset: 0x0001AD89
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0001CB91 File Offset: 0x0001AD91
		public int Bottom
		{
			get
			{
				return this.bottom;
			}
			set
			{
				this.CheckMargin(value, "Bottom");
				this.bottom = value;
				this.doubleBottom = (double)value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001CBAE File Offset: 0x0001ADAE
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001CBB6 File Offset: 0x0001ADB6
		internal double DoubleLeft
		{
			get
			{
				return this.doubleLeft;
			}
			set
			{
				this.Left = (int)Math.Round(value);
				this.doubleLeft = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		internal double DoubleRight
		{
			get
			{
				return this.doubleRight;
			}
			set
			{
				this.Right = (int)Math.Round(value);
				this.doubleRight = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001CBEA File Offset: 0x0001ADEA
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001CBF2 File Offset: 0x0001ADF2
		internal double DoubleTop
		{
			get
			{
				return this.doubleTop;
			}
			set
			{
				this.Top = (int)Math.Round(value);
				this.doubleTop = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001CC08 File Offset: 0x0001AE08
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001CC10 File Offset: 0x0001AE10
		internal double DoubleBottom
		{
			get
			{
				return this.doubleBottom;
			}
			set
			{
				this.Bottom = (int)Math.Round(value);
				this.doubleBottom = value;
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001CC28 File Offset: 0x0001AE28
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this.doubleLeft == 0.0 && this.left != 0)
			{
				this.doubleLeft = (double)this.left;
			}
			if (this.doubleRight == 0.0 && this.right != 0)
			{
				this.doubleRight = (double)this.right;
			}
			if (this.doubleTop == 0.0 && this.top != 0)
			{
				this.doubleTop = (double)this.top;
			}
			if (this.doubleBottom == 0.0 && this.bottom != 0)
			{
				this.doubleBottom = (double)this.bottom;
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001CCCD File Offset: 0x0001AECD
		private void CheckMargin(int margin, string name)
		{
			if (margin < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					name,
					margin,
					"0"
				}));
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001CCFE File Offset: 0x0001AEFE
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001CD08 File Offset: 0x0001AF08
		public override bool Equals(object obj)
		{
			Margins margins = obj as Margins;
			return margins == this || (!(margins == null) && (margins.Left == this.Left && margins.Right == this.Right && margins.Top == this.Top) && margins.Bottom == this.Bottom);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		public override int GetHashCode()
		{
			uint num = (uint)this.Left;
			uint num2 = (uint)this.Right;
			uint num3 = (uint)this.Top;
			uint num4 = (uint)this.Bottom;
			return (int)(num ^ (num2 << 13 | num2 >> 19) ^ (num3 << 26 | num3 >> 6) ^ (num4 << 7 | num4 >> 25));
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001CDB8 File Offset: 0x0001AFB8
		public static bool operator ==(Margins m1, Margins m2)
		{
			return m1 == null == (m2 == null) && (m1 == null || (m1.Left == m2.Left && m1.Top == m2.Top && m1.Right == m2.Right && m1.Bottom == m2.Bottom));
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001CE10 File Offset: 0x0001B010
		public static bool operator !=(Margins m1, Margins m2)
		{
			return !(m1 == m2);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001CE1C File Offset: 0x0001B01C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[Margins Left=",
				this.Left.ToString(CultureInfo.InvariantCulture),
				" Right=",
				this.Right.ToString(CultureInfo.InvariantCulture),
				" Top=",
				this.Top.ToString(CultureInfo.InvariantCulture),
				" Bottom=",
				this.Bottom.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x0400060D RID: 1549
		private int left;

		// Token: 0x0400060E RID: 1550
		private int right;

		// Token: 0x0400060F RID: 1551
		private int top;

		// Token: 0x04000610 RID: 1552
		private int bottom;

		// Token: 0x04000611 RID: 1553
		[OptionalField]
		private double doubleLeft;

		// Token: 0x04000612 RID: 1554
		[OptionalField]
		private double doubleRight;

		// Token: 0x04000613 RID: 1555
		[OptionalField]
		private double doubleTop;

		// Token: 0x04000614 RID: 1556
		[OptionalField]
		private double doubleBottom;
	}
}
