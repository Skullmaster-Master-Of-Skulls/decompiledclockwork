using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x02000030 RID: 48
	[TypeConverter(typeof(SizeConverter))]
	[ComVisible(true)]
	[Serializable]
	public struct Size
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x00016D58 File Offset: 0x00014F58
		public Size(Point pt)
		{
			this.width = pt.X;
			this.height = pt.Y;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00016D74 File Offset: 0x00014F74
		public Size(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00016D84 File Offset: 0x00014F84
		public static implicit operator SizeF(Size p)
		{
			return new SizeF((float)p.Width, (float)p.Height);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00016D9B File Offset: 0x00014F9B
		public static Size operator +(Size sz1, Size sz2)
		{
			return Size.Add(sz1, sz2);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00016DA4 File Offset: 0x00014FA4
		public static Size operator -(Size sz1, Size sz2)
		{
			return Size.Subtract(sz1, sz2);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00016DAD File Offset: 0x00014FAD
		public static bool operator ==(Size sz1, Size sz2)
		{
			return sz1.Width == sz2.Width && sz1.Height == sz2.Height;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00016DD1 File Offset: 0x00014FD1
		public static bool operator !=(Size sz1, Size sz2)
		{
			return !(sz1 == sz2);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00016DDD File Offset: 0x00014FDD
		public static explicit operator Point(Size size)
		{
			return new Point(size.Width, size.Height);
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00016DF2 File Offset: 0x00014FF2
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return this.width == 0 && this.height == 0;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00016E07 File Offset: 0x00015007
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00016E0F File Offset: 0x0001500F
		public int Width
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

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00016E18 File Offset: 0x00015018
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00016E20 File Offset: 0x00015020
		public int Height
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

		// Token: 0x060004EA RID: 1258 RVA: 0x00016E29 File Offset: 0x00015029
		public static Size Add(Size sz1, Size sz2)
		{
			return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00016E4E File Offset: 0x0001504E
		public static Size Ceiling(SizeF value)
		{
			return new Size((int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00016E71 File Offset: 0x00015071
		public static Size Subtract(Size sz1, Size sz2)
		{
			return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00016E96 File Offset: 0x00015096
		public static Size Truncate(SizeF value)
		{
			return new Size((int)value.Width, (int)value.Height);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00016EAD File Offset: 0x000150AD
		public static Size Round(SizeF value)
		{
			return new Size((int)Math.Round((double)value.Width), (int)Math.Round((double)value.Height));
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00016ED0 File Offset: 0x000150D0
		public override bool Equals(object obj)
		{
			if (!(obj is Size))
			{
				return false;
			}
			Size size = (Size)obj;
			return size.width == this.width && size.height == this.height;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00016F0C File Offset: 0x0001510C
		public override int GetHashCode()
		{
			return this.width ^ this.height;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00016F1C File Offset: 0x0001511C
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

		// Token: 0x04000317 RID: 791
		public static readonly Size Empty;

		// Token: 0x04000318 RID: 792
		private int width;

		// Token: 0x04000319 RID: 793
		private int height;
	}
}
