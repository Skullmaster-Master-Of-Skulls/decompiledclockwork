using System;
using System.Drawing;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AA0 RID: 2720
	internal class ExcelConverter
	{
		// Token: 0x17002222 RID: 8738
		// (get) Token: 0x060067C7 RID: 26567 RVA: 0x001844DE File Offset: 0x001826DE
		// (set) Token: 0x060067C8 RID: 26568 RVA: 0x001844E6 File Offset: 0x001826E6
		public double DotsPerInch
		{
			get
			{
				return this.dpi;
			}
			set
			{
				this.dpi = value;
			}
		}

		// Token: 0x17002223 RID: 8739
		// (get) Token: 0x060067C9 RID: 26569 RVA: 0x001844EF File Offset: 0x001826EF
		private double NumberWidth
		{
			get
			{
				if (this.numberWidth == 0.0)
				{
					this.numberWidth = this.CalculateNumberWidth();
				}
				return this.numberWidth;
			}
		}

		// Token: 0x060067CA RID: 26570 RVA: 0x00184514 File Offset: 0x00182714
		private double CalculateNumberWidth()
		{
			double num = double.MinValue;
			for (int i = 0; i < "0123456789".Length; i++)
			{
				using (Bitmap bitmap = new Bitmap(1, 1))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						CharacterRange[] measurableCharacterRanges = new CharacterRange[]
						{
							new CharacterRange(i, 1)
						};
						RectangleF layoutRect = new RectangleF(0f, 0f, 100000f, 100000f);
						using (StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic))
						{
							stringFormat.SetMeasurableCharacterRanges(measurableCharacterRanges);
							Region[] array = graphics.MeasureCharacterRanges(this.measureString, this.defaultFont, layoutRect, stringFormat);
							RectangleF bounds = array[0].GetBounds(graphics);
							if ((double)bounds.Width > num)
							{
								num = (double)bounds.Width;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060067CB RID: 26571 RVA: 0x0018462C File Offset: 0x0018282C
		public ExcelConverter(Font defaultFont)
		{
			this.defaultFont = defaultFont;
		}

		// Token: 0x060067CC RID: 26572 RVA: 0x00184655 File Offset: 0x00182855
		public double CharactersToPixels(double characters)
		{
			if (characters < 1.0)
			{
				return characters * 12.0;
			}
			return characters * this.NumberWidth + 5.0;
		}

		// Token: 0x060067CD RID: 26573 RVA: 0x00184681 File Offset: 0x00182881
		public double CharactersToPoints(double characters)
		{
			return this.PixelsToPoints(this.CharactersToPixels(characters));
		}

		// Token: 0x060067CE RID: 26574 RVA: 0x00184690 File Offset: 0x00182890
		public double PixelsToCharacters(double pixels)
		{
			if (pixels > 12.0)
			{
				return (pixels - 5.0) / this.NumberWidth;
			}
			return pixels / 12.0;
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x001846BE File Offset: 0x001828BE
		public double PointsToCharacters(double points)
		{
			return this.PixelsToCharacters(this.PointsToPixels(points));
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x001846CD File Offset: 0x001828CD
		public double PointsToPixels(double points)
		{
			return this.DotsPerInch * points / 72.0;
		}

		// Token: 0x060067D1 RID: 26577 RVA: 0x001846E1 File Offset: 0x001828E1
		public double PixelsToPoints(double pixels)
		{
			return pixels * 72.0 / this.DotsPerInch;
		}

		// Token: 0x04001AC5 RID: 6853
		private readonly string measureString = "0123456789";

		// Token: 0x04001AC6 RID: 6854
		private double numberWidth;

		// Token: 0x04001AC7 RID: 6855
		private readonly Font defaultFont;

		// Token: 0x04001AC8 RID: 6856
		private double dpi = 96.0;
	}
}
