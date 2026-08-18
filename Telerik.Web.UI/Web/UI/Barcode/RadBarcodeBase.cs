using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009FF RID: 2559
	internal class RadBarcodeBase
	{
		// Token: 0x06006119 RID: 24857 RVA: 0x0016D330 File Offset: 0x0016B530
		public virtual void RenderContentsRectangles(HtmlTextWriter writer)
		{
		}

		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x0600611A RID: 24858 RVA: 0x0016D332 File Offset: 0x0016B532
		// (set) Token: 0x0600611B RID: 24859 RVA: 0x0016D33A File Offset: 0x0016B53A
		public virtual string Text { get; set; }

		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x0600611C RID: 24860 RVA: 0x0016D343 File Offset: 0x0016B543
		// (set) Token: 0x0600611D RID: 24861 RVA: 0x0016D34B File Offset: 0x0016B54B
		public virtual Unit Width { get; set; }

		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x0600611E RID: 24862 RVA: 0x0016D354 File Offset: 0x0016B554
		// (set) Token: 0x0600611F RID: 24863 RVA: 0x0016D35C File Offset: 0x0016B55C
		public virtual Unit Height { get; set; }

		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x06006120 RID: 24864 RVA: 0x0016D365 File Offset: 0x0016B565
		// (set) Token: 0x06006121 RID: 24865 RVA: 0x0016D36D File Offset: 0x0016B56D
		public bool RenderChecksum { get; set; }

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x0016D376 File Offset: 0x0016B576
		// (set) Token: 0x06006123 RID: 24867 RVA: 0x0016D37E File Offset: 0x0016B57E
		public bool ShowChecksum { get; set; }

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x06006124 RID: 24868 RVA: 0x0016D387 File Offset: 0x0016B587
		// (set) Token: 0x06006125 RID: 24869 RVA: 0x0016D38F File Offset: 0x0016B58F
		public bool ShowText { get; set; }

		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x06006126 RID: 24870 RVA: 0x0016D398 File Offset: 0x0016B598
		// (set) Token: 0x06006127 RID: 24871 RVA: 0x0016D3A0 File Offset: 0x0016B5A0
		public float ShortLinesLengthPercentage { get; set; }

		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x06006128 RID: 24872 RVA: 0x0016D3A9 File Offset: 0x0016B5A9
		// (set) Token: 0x06006129 RID: 24873 RVA: 0x0016D3B1 File Offset: 0x0016B5B1
		public float VerticalTextPositionPercentage { get; set; }

		// Token: 0x0600612A RID: 24874 RVA: 0x0016D3BC File Offset: 0x0016B5BC
		protected int GetTrailingZeros(string value)
		{
			if (value.Length == 0)
			{
				return 0;
			}
			int num = value.Length - 1;
			int num2 = 0;
			while (value[num] == '0')
			{
				num2++;
				num--;
			}
			return num2;
		}

		// Token: 0x0600612B RID: 24875 RVA: 0x0016D3F4 File Offset: 0x0016B5F4
		protected int GetLeadingZeros(string value)
		{
			if (value.Length == 0)
			{
				return 0;
			}
			int num = 0;
			while (value[num] == '0')
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600612C RID: 24876 RVA: 0x0016D420 File Offset: 0x0016B620
		public System.Drawing.Image GetBitmap(int lineWidth)
		{
			StringBuilder stringBuilder = new StringBuilder();
			HtmlTextWriter writer = new HtmlTextWriter(new StringWriter(stringBuilder, CultureInfo.InvariantCulture));
			this.RenderContentsRectangles(writer);
			XmlTextReader xmlTextReader = new XmlTextReader(new StringReader("<parent>" + stringBuilder.ToString() + "</parent>"));
			char[] trimChars = new char[]
			{
				'%'
			};
			List<double> list = new List<double>();
			List<double> list2 = new List<double>();
			List<double> list3 = new List<double>();
			List<double> list4 = new List<double>();
			double num = 1.0;
			while (xmlTextReader.Read())
			{
				if (xmlTextReader.Name == "rect" && xmlTextReader["x"] != null)
				{
					double item = double.Parse(xmlTextReader["x"].TrimEnd(trimChars), CultureInfo.InvariantCulture) / 100.0;
					double item2 = double.Parse(xmlTextReader["y"].TrimEnd(trimChars), CultureInfo.InvariantCulture) / 100.0;
					double num2 = double.Parse(xmlTextReader["width"].TrimEnd(trimChars), CultureInfo.InvariantCulture) / 100.0;
					double item3 = double.Parse(xmlTextReader["height"].TrimEnd(trimChars), CultureInfo.InvariantCulture) / 100.0;
					list.Add(item);
					list2.Add(item2);
					list3.Add(num2);
					list4.Add(item3);
					if (num > num2)
					{
						num = num2;
					}
				}
			}
			int num3;
			int num4;
			if (lineWidth >= 1 && num > 0.0 && num < 0.5)
			{
				num3 = (int)(0.01 + (double)lineWidth / num);
				if (this.Height.Value > 0.0 && this.Width.Value > 0.0)
				{
					num4 = (int)(this.Height.Value * ((double)num3 / this.Width.Value));
				}
				else
				{
					num4 = num3 / 2;
				}
			}
			else
			{
				num3 = (int)this.Width.Value;
				num4 = (int)this.Height.Value;
			}
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < list.Count; i++)
			{
				int num7 = (int)(0.01 + (double)num3 * (list[i] + list3[i]));
				int num8 = (int)(0.01 + (double)num4 * (list2[i] + list4[i]));
				num5 = ((num5 > num7) ? num5 : num7);
				num6 = ((num6 > num8) ? num6 : num8);
			}
			if (num5 == 0 || num6 == 0)
			{
				return null;
			}
			Bitmap bitmap = new Bitmap(num5, num6);
			for (int j = 0; j < num5; j++)
			{
				for (int k = 0; k < num6; k++)
				{
					bitmap.SetPixel(j, k, Color.White);
				}
			}
			for (int l = 0; l < list.Count; l++)
			{
				for (int m = (int)(0.01 + (double)num3 * list[l]); m < (int)(0.01 + (double)num3 * (list[l] + list3[l])); m++)
				{
					for (int n = (int)(0.01 + (double)num4 * list2[l]); n < (int)(0.01 + (double)num4 * (list2[l] + list4[l])); n++)
					{
						bitmap.SetPixel(m, n, Color.Black);
					}
				}
			}
			return bitmap;
		}

		// Token: 0x0600612D RID: 24877 RVA: 0x0016D7E8 File Offset: 0x0016B9E8
		internal static RotateFlipType RotateTypeFromRotation(Rotation rotation)
		{
			switch (rotation)
			{
			case Rotation.Rotate90:
				return RotateFlipType.Rotate90FlipNone;
			case Rotation.Rotate180:
				return RotateFlipType.Rotate180FlipNone;
			case Rotation.Rotate270:
				return RotateFlipType.Rotate270FlipNone;
			default:
				return RotateFlipType.RotateNoneFlipNone;
			}
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x0016D814 File Offset: 0x0016BA14
		internal static bool[,] GetRotatedMatrix(bool[,] matrix, Rotation rotation)
		{
			bool[,] array;
			if (rotation == Rotation.Rotate0)
			{
				array = new bool[matrix.GetLength(0), matrix.GetLength(1)];
				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						array[i, j] = matrix[i, j];
					}
				}
			}
			else if (Rotation.Rotate90 == rotation)
			{
				array = new bool[matrix.GetLength(1), matrix.GetLength(0)];
				int k = 0;
				int num = 0;
				while (k < matrix.GetLength(1))
				{
					int l = 0;
					int num2 = array.GetLength(1) - 1;
					while (l < matrix.GetLength(0))
					{
						array[num, num2] = matrix[l, k];
						l++;
						num2--;
					}
					k++;
					num++;
				}
			}
			else if (Rotation.Rotate270 == rotation)
			{
				array = new bool[matrix.GetLength(1), matrix.GetLength(0)];
				int m = 0;
				int num3 = array.GetLength(0) - 1;
				while (m < matrix.GetLength(1))
				{
					int n = 0;
					int num4 = 0;
					while (n < matrix.GetLength(0))
					{
						array[num3, num4] = matrix[n, m];
						n++;
						num4++;
					}
					m++;
					num3--;
				}
			}
			else
			{
				array = new bool[matrix.GetLength(0), matrix.GetLength(1)];
				int num5 = 0;
				int num6 = array.GetLength(0) - 1;
				while (num5 < matrix.GetLength(0))
				{
					int num7 = 0;
					int num8 = array.GetLength(1) - 1;
					while (num7 < matrix.GetLength(1))
					{
						array[num6, num8] = matrix[num5, num7];
						num7++;
						num8--;
					}
					num5++;
					num6--;
				}
			}
			return array;
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x0016D9D0 File Offset: 0x0016BBD0
		public virtual string GetDataURL(int lineWidth, Rotation rotation)
		{
			System.Drawing.Image bitmap = this.GetBitmap(lineWidth);
			if (bitmap == null)
			{
				return "";
			}
			bitmap.RotateFlip(RadBarcodeBase.RotateTypeFromRotation(rotation));
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Png);
			return "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
		}
	}
}
