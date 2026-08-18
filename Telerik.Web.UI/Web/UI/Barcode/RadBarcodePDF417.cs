using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Web.UI;
using Telerik.Web.UI.Barcode.PDF417ClassLibrary;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020000A3 RID: 163
	internal class RadBarcodePDF417 : Control
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x0001A3B8 File Offset: 0x000185B8
		public RadBarcodePDF417(PDF417Settings settings, string text)
		{
			this.EncodingMode = settings.EncodingMode;
			this.ErrorCorrectionLevel = settings.ErrorCorrectionLevel;
			this.Text = text;
			this.CheckDefaultValues();
			this.encoder = new PDF417Encoder();
			this.encoder.PopulateMatrix(this.Text, this.ErrorCorrectionLevel, this.EncodingMode, 1, 1);
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001A41A File Offset: 0x0001861A
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0001A422 File Offset: 0x00018622
		public string Text { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001A42B File Offset: 0x0001862B
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0001A433 File Offset: 0x00018633
		public int Columns { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001A43C File Offset: 0x0001863C
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0001A444 File Offset: 0x00018644
		public int Rows { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001A44D File Offset: 0x0001864D
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0001A455 File Offset: 0x00018655
		public EncodingMode EncodingMode { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001A45E File Offset: 0x0001865E
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0001A466 File Offset: 0x00018666
		public int ErrorCorrectionLevel
		{
			get
			{
				return this.ErrorCorrectionLevelProperty;
			}
			set
			{
				if (value < 0 || value > 8)
				{
					this.ErrorCorrectionLevelProperty = 0;
					return;
				}
				this.ErrorCorrectionLevelProperty = value;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001A47F File Offset: 0x0001867F
		private void CheckDefaultValues()
		{
			if (this.Text == null)
			{
				this.Text = "PDF417";
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001A494 File Offset: 0x00018694
		public Image GetBitmap(int dotSize, int aspectRatio)
		{
			int num = dotSize * aspectRatio;
			int height = num * this.encoder.TotalRows;
			int width = dotSize * (this.encoder.TotalColumns + 4);
			int num2 = dotSize * 2;
			Bitmap bitmap = new Bitmap(width, height);
			for (int i = 0; i < this.encoder.TotalRows; i++)
			{
				for (int j = 0; j < this.encoder.TotalColumns; j++)
				{
					if (this.encoder.DataMatrix[i, j])
					{
						int num3 = j * dotSize + num2;
						int num4 = i * num;
						int num5 = j * dotSize + dotSize + num2;
						int num6 = i * num + num;
						for (int k = num4; k < num6; k++)
						{
							for (int l = num3; l < num5; l++)
							{
								bitmap.SetPixel(l, k, Color.Black);
							}
						}
					}
				}
			}
			return bitmap;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001A580 File Offset: 0x00018780
		public virtual string GetDataURL(int dotSize, int aspectRatio, Rotation rotation)
		{
			Image bitmap = this.GetBitmap(dotSize, aspectRatio);
			bitmap.RotateFlip(RadBarcodeBase.RotateTypeFromRotation(rotation));
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Png);
			return "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001A5CC File Offset: 0x000187CC
		private static List<int> GetPath(bool[,] matrix, Rotation rotation)
		{
			matrix.GetLength(0);
			matrix.GetLength(1);
			List<int> list = new List<int>();
			if (rotation == Rotation.Rotate0 || Rotation.Rotate180 == rotation)
			{
				for (int i = 0; i < matrix.GetLength(1); i++)
				{
					for (int j = 0; j < matrix.GetLength(0); j++)
					{
						if (matrix[j, i])
						{
							int num = j;
							while (j < matrix.GetLength(0) && matrix[j, i])
							{
								j++;
							}
							if (j - num >= 1)
							{
								list.Add(2 * i + 1);
								list.Add(2 * num);
								list.Add(2 * i + 1);
								list.Add(2 * j);
							}
						}
					}
				}
			}
			else
			{
				for (int k = 0; k < matrix.GetLength(0); k++)
				{
					for (int l = 0; l < matrix.GetLength(1); l++)
					{
						if (matrix[k, l])
						{
							int num2 = l;
							while (l < matrix.GetLength(1) && matrix[k, l])
							{
								l++;
							}
							if (l - num2 >= 1)
							{
								list.Add(2 * num2);
								list.Add(2 * k + 1);
								list.Add(2 * l);
								list.Add(2 * k + 1);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001A708 File Offset: 0x00018908
		public Pair GetDimensions(Rotation rotation)
		{
			int num = 0;
			int num2 = 0;
			this.path = RadBarcodePDF417.GetPath(RadBarcodeBase.GetRotatedMatrix(this.encoder.DataMatrix, rotation), rotation);
			int num3 = 0;
			while (num3 + 3 < this.path.Count)
			{
				if (num < this.path[num3 + 2])
				{
					num = this.path[num3 + 2];
				}
				if (num2 < this.path[num3 + 3])
				{
					num2 = this.path[num3 + 3];
				}
				num3 += 4;
			}
			return new Pair(num, num2);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001A7A0 File Offset: 0x000189A0
		public virtual void RenderContentsRectangles(HtmlTextWriter writer, int aspectRatio, int aspectRatioY, int aspectRatioX)
		{
			writer.Write(string.Format(CultureInfo.InvariantCulture, "<path shape-rendering=\"crispEdges\" stroke-width=\"2\" stroke=\"black\" fill=\"black\" d=\"", new object[0]));
			int num = 0;
			while (num + 3 < this.path.Count)
			{
				writer.Write(string.Format(CultureInfo.InvariantCulture, "M {0} {1} {2} {3} ", new object[]
				{
					this.path[num] * aspectRatioY,
					this.path[num + 1] * aspectRatioX,
					this.path[num + 2] * aspectRatioY,
					this.path[num + 3] * aspectRatioX
				}));
				num += 4;
			}
			writer.Write("\"/>");
		}

		// Token: 0x04000152 RID: 338
		private PDF417Encoder encoder;

		// Token: 0x04000153 RID: 339
		private int ErrorCorrectionLevelProperty;

		// Token: 0x04000154 RID: 340
		private List<int> path;
	}
}
