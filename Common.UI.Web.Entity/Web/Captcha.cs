using System;
using System.Drawing;

namespace TechnoPro.Common.UI.Web.Entity.Web
{
	// Token: 0x0200000E RID: 14
	public class Captcha
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000229C File Offset: 0x0000049C
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000022B4 File Offset: 0x000004B4
		public string FontFamily
		{
			get
			{
				return this.fontFamily;
			}
			set
			{
				this.fontFamily = ((!string.IsNullOrEmpty(value)) ? value : "Arial");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000022D0 File Offset: 0x000004D0
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000022E8 File Offset: 0x000004E8
		public double FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				try
				{
					bool flag = value <= 10.0 || value >= 24.0;
					if (flag)
					{
						this.fontSize = 16.0;
					}
					else
					{
						this.fontSize = value;
					}
				}
				catch (Exception ex)
				{
					this.fontSize = 16.0;
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002358 File Offset: 0x00000558
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002370 File Offset: 0x00000570
		public string TextColor
		{
			get
			{
				return this.textColor;
			}
			set
			{
				this.textColor = ((value == string.Empty || value == null) ? "Black" : value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002394 File Offset: 0x00000594
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000023AC File Offset: 0x000005AC
		public string BackgroundImagePath
		{
			get
			{
				return this.backgroundImagePath;
			}
			set
			{
				this.backgroundImagePath = value;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000023B8 File Offset: 0x000005B8
		public Font GetFont()
		{
			return new Font(this.FontFamily, (float)this.FontSize);
		}

		// Token: 0x0400005B RID: 91
		private double fontSize;

		// Token: 0x0400005C RID: 92
		private string fontFamily;

		// Token: 0x0400005D RID: 93
		private string backgroundImagePath;

		// Token: 0x0400005E RID: 94
		private string textColor;
	}
}
