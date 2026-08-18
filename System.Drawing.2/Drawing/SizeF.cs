using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x02000042 RID: 66
	[ComVisible(true)]
	[TypeConverter(typeof(SizeFConverter))]
	[Serializable]
	public struct SizeF
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x0001B20D File Offset: 0x0001940D
		public SizeF(SizeF size)
		{
			this.width = size.width;
			this.height = size.height;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001B227 File Offset: 0x00019427
		public SizeF(PointF pt)
		{
			this.width = pt.X;
			this.height = pt.Y;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001B243 File Offset: 0x00019443
		public SizeF(float width, float height)
		{
			this.width = width;
			this.height = height;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001B253 File Offset: 0x00019453
		public static SizeF operator +(SizeF sz1, SizeF sz2)
		{
			return SizeF.Add(sz1, sz2);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001B25C File Offset: 0x0001945C
		public static SizeF operator -(SizeF sz1, SizeF sz2)
		{
			return SizeF.Subtract(sz1, sz2);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001B265 File Offset: 0x00019465
		public static bool operator ==(SizeF sz1, SizeF sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001B289 File Offset: 0x00019489
		public static bool operator !=(SizeF sz1, SizeF sz2)
		{
			return !(sz1 == sz2);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001B295 File Offset: 0x00019495
		public static explicit operator PointF(SizeF size)
		{
			return new PointF(size.Width, size.Height);
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001B2AA File Offset: 0x000194AA
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.width == 0f && this.height == 0f;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001B2C8 File Offset: 0x000194C8
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001B2D0 File Offset: 0x000194D0
		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001B2D9 File Offset: 0x000194D9
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0001B2E1 File Offset: 0x000194E1
		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001B2EA File Offset: 0x000194EA
		public static SizeF Add(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001B30F File Offset: 0x0001950F
		public static SizeF Subtract(SizeF sz1, SizeF sz2)
		{
			return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001B334 File Offset: 0x00019534
		public override bool Equals(object obj)
		{
			if (!(obj is SizeF))
			{
				return false;
			}
			SizeF sizeF = (SizeF)obj;
			return sizeF.Width == this.Width && sizeF.Height == this.Height && sizeF.GetType().Equals(base.GetType());
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001B392 File Offset: 0x00019592
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001B3A4 File Offset: 0x000195A4
		public PointF ToPointF()
		{
			return (PointF)this;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001B3B1 File Offset: 0x000195B1
		public Size ToSize()
		{
			return Size.Truncate(this);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001B3C0 File Offset: 0x000195C0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Width=",
				this.width.ToString(CultureInfo.CurrentCulture),
				", Height=",
				this.height.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		// Token: 0x04000576 RID: 1398
		public static readonly SizeF Empty;

		// Token: 0x04000577 RID: 1399
		private float width;

		// Token: 0x04000578 RID: 1400
		private float height;
	}
}
