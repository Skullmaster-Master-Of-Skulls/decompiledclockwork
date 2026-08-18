using System;
using System.Globalization;
using System.Text;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Render.Pdf
{
	// Token: 0x0200169C RID: 5788
	internal sealed class PdfColor
	{
		// Token: 0x0600DF6D RID: 57197 RVA: 0x0031A094 File Offset: 0x00318294
		public PdfColor(ColorType color)
		{
			this.red = (double)color.Red;
			this.green = (double)color.Green;
			this.blue = (double)color.Blue;
		}

		// Token: 0x0600DF6E RID: 57198 RVA: 0x0031A0FC File Offset: 0x003182FC
		public PdfColor(double red, double green, double blue)
		{
			this.red = red;
			this.green = green;
			this.blue = blue;
		}

		// Token: 0x0600DF6F RID: 57199 RVA: 0x0031A151 File Offset: 0x00318351
		public PdfColor(int red, int green, int blue) : this((double)red / 255.0, (double)green / 255.0, (double)blue / 255.0)
		{
		}

		// Token: 0x0600DF70 RID: 57200 RVA: 0x0031A17D File Offset: 0x0031837D
		public double getRed()
		{
			return this.red;
		}

		// Token: 0x0600DF71 RID: 57201 RVA: 0x0031A185 File Offset: 0x00318385
		public double getGreen()
		{
			return this.green;
		}

		// Token: 0x0600DF72 RID: 57202 RVA: 0x0031A18D File Offset: 0x0031838D
		public double getBlue()
		{
			return this.blue;
		}

		// Token: 0x0600DF73 RID: 57203 RVA: 0x0031A198 File Offset: 0x00318398
		public string getColorSpaceOut(bool fillNotStroke)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			if (this.red == this.green && this.red == this.blue)
			{
				flag = true;
			}
			if (fillNotStroke)
			{
				if (flag)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:0.0####} g\n", new object[]
					{
						this.red
					});
				}
				else
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:0.0####} {1:0.0####} {2:0.0####} rg\n", new object[]
					{
						this.red,
						this.green,
						this.blue
					});
				}
			}
			else if (flag)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:0.0####} G\n", new object[]
				{
					this.red
				});
			}
			else
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:0.0####} {1:0.0####} {2:0.0####} RG\n", new object[]
				{
					this.red,
					this.green,
					this.blue
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04004071 RID: 16497
		private double red = -1.0;

		// Token: 0x04004072 RID: 16498
		private double green = -1.0;

		// Token: 0x04004073 RID: 16499
		private double blue = -1.0;
	}
}
