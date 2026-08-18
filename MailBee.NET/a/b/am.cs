using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace a.b
{
	// Token: 0x02000345 RID: 837
	internal class am : e6
	{
		// Token: 0x06001E49 RID: 7753 RVA: 0x00081D02 File Offset: 0x00080D02
		public am(string A_0, ImageFormat A_1, Size A_2)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00081D2D File Offset: 0x00080D2D
		public string bf()
		{
			return this.a;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x00081D35 File Offset: 0x00080D35
		public ImageFormat bg()
		{
			return this.b;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00081D3D File Offset: 0x00080D3D
		public Size bh()
		{
			return this.c;
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x00081D48 File Offset: 0x00080D48
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.a,
				" ",
				this.b,
				" ",
				this.c.Width,
				"x",
				this.c.Height
			});
		}

		// Token: 0x040013D1 RID: 5073
		private readonly string a;

		// Token: 0x040013D2 RID: 5074
		private readonly ImageFormat b;

		// Token: 0x040013D3 RID: 5075
		private readonly Size c;
	}
}
