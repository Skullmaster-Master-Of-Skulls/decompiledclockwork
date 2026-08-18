using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x0200008C RID: 140
	[StructLayout(LayoutKind.Sequential)]
	public sealed class BitmapData
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x000224B7 File Offset: 0x000206B7
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x000224BF File Offset: 0x000206BF
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

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x000224C8 File Offset: 0x000206C8
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x000224D0 File Offset: 0x000206D0
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

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x000224D9 File Offset: 0x000206D9
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x000224E1 File Offset: 0x000206E1
		public int Stride
		{
			get
			{
				return this.stride;
			}
			set
			{
				this.stride = value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x000224EA File Offset: 0x000206EA
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x000224F4 File Offset: 0x000206F4
		public PixelFormat PixelFormat
		{
			get
			{
				return (PixelFormat)this.pixelFormat;
			}
			set
			{
				if (value <= PixelFormat.Format8bppIndexed)
				{
					if (value <= PixelFormat.Format16bppRgb565)
					{
						if (value <= PixelFormat.Max)
						{
							if (value == PixelFormat.Undefined || value == PixelFormat.Max)
							{
								goto IL_125;
							}
						}
						else if (value == PixelFormat.Indexed || value == PixelFormat.Gdi || value - PixelFormat.Format16bppRgb555 <= 1)
						{
							goto IL_125;
						}
					}
					else if (value <= PixelFormat.Format32bppRgb)
					{
						if (value == PixelFormat.Format24bppRgb || value == PixelFormat.Format32bppRgb)
						{
							goto IL_125;
						}
					}
					else if (value == PixelFormat.Format1bppIndexed || value == PixelFormat.Format4bppIndexed || value == PixelFormat.Format8bppIndexed)
					{
						goto IL_125;
					}
				}
				else if (value <= PixelFormat.Extended)
				{
					if (value <= PixelFormat.Format16bppArgb1555)
					{
						if (value == PixelFormat.Alpha || value == PixelFormat.Format16bppArgb1555)
						{
							goto IL_125;
						}
					}
					else if (value == PixelFormat.PAlpha || value == PixelFormat.Format32bppPArgb || value == PixelFormat.Extended)
					{
						goto IL_125;
					}
				}
				else if (value <= PixelFormat.Format64bppPArgb)
				{
					if (value == PixelFormat.Format16bppGrayScale || value == PixelFormat.Format48bppRgb || value == PixelFormat.Format64bppPArgb)
					{
						goto IL_125;
					}
				}
				else if (value == PixelFormat.Canonical || value == PixelFormat.Format32bppArgb || value == PixelFormat.Format64bppArgb)
				{
					goto IL_125;
				}
				throw new InvalidEnumArgumentException("value", (int)value, typeof(PixelFormat));
				IL_125:
				this.pixelFormat = (int)value;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0002262D File Offset: 0x0002082D
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x00022635 File Offset: 0x00020835
		public IntPtr Scan0
		{
			get
			{
				return this.scan0;
			}
			set
			{
				this.scan0 = value;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0002263E File Offset: 0x0002083E
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00022646 File Offset: 0x00020846
		public int Reserved
		{
			get
			{
				return this.reserved;
			}
			set
			{
				this.reserved = value;
			}
		}

		// Token: 0x04000735 RID: 1845
		private int width;

		// Token: 0x04000736 RID: 1846
		private int height;

		// Token: 0x04000737 RID: 1847
		private int stride;

		// Token: 0x04000738 RID: 1848
		private int pixelFormat;

		// Token: 0x04000739 RID: 1849
		private IntPtr scan0;

		// Token: 0x0400073A RID: 1850
		private int reserved;
	}
}
