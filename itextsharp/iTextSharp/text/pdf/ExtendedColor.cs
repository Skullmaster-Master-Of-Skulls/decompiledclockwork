using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020001CC RID: 460
	public abstract class ExtendedColor : BaseColor
	{
		// Token: 0x06001201 RID: 4609 RVA: 0x00067B59 File Offset: 0x00066B59
		public ExtendedColor(int type) : base(0, 0, 0)
		{
			this.type = type;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00067B6B File Offset: 0x00066B6B
		public ExtendedColor(int type, float red, float green, float blue) : base(ExtendedColor.Normalize(red), ExtendedColor.Normalize(green), ExtendedColor.Normalize(blue))
		{
			this.type = type;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00067B90 File Offset: 0x00066B90
		public ExtendedColor(int type, int red, int green, int blue, int alpha) : base(ExtendedColor.Normalize((float)red / 255f), ExtendedColor.Normalize((float)green / 255f), ExtendedColor.Normalize((float)blue / 255f), ExtendedColor.Normalize((float)alpha / 255f))
		{
			this.type = type;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00067BE0 File Offset: 0x00066BE0
		public int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00067BE8 File Offset: 0x00066BE8
		public static int GetType(object color)
		{
			if (color is ExtendedColor)
			{
				return ((ExtendedColor)color).Type;
			}
			return 0;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00067BFF File Offset: 0x00066BFF
		internal static float Normalize(float value)
		{
			if (value < 0f)
			{
				return 0f;
			}
			if (value > 1f)
			{
				return 1f;
			}
			return value;
		}

		// Token: 0x04000CA8 RID: 3240
		internal const int TYPE_RGB = 0;

		// Token: 0x04000CA9 RID: 3241
		internal const int TYPE_GRAY = 1;

		// Token: 0x04000CAA RID: 3242
		internal const int TYPE_CMYK = 2;

		// Token: 0x04000CAB RID: 3243
		internal const int TYPE_SEPARATION = 3;

		// Token: 0x04000CAC RID: 3244
		internal const int TYPE_PATTERN = 4;

		// Token: 0x04000CAD RID: 3245
		internal const int TYPE_SHADING = 5;

		// Token: 0x04000CAE RID: 3246
		protected int type;
	}
}
