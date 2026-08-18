using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Web.UI;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009FE RID: 2558
	internal class RadBarcodeQRCode
	{
		// Token: 0x06006113 RID: 24851 RVA: 0x0016CFE0 File Offset: 0x0016B1E0
		public RadBarcodeQRCode(QRCodeSettings settings, string text)
		{
			int version = settings.Version;
			this.qR = new QRCode(settings.Mode, settings.Version, settings.ErrorCorrectionLevel, settings.ECI, settings.FNC1, settings.ApplicationIndicator, settings.AutoIncreaseVersion);
			this.encodedData = this.qR.EncodeData(text, ref version);
			settings.Version = version;
		}

		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x06006114 RID: 24852 RVA: 0x0016D055 File Offset: 0x0016B255
		public int NumberOfModules
		{
			get
			{
				return this.qR.VersionDimension + 8;
			}
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x0016D064 File Offset: 0x0016B264
		public virtual void RenderContentsRectangles(HtmlTextWriter writer, Rotation rotation)
		{
			List<int> path = RadBarcodeQRCode.GetPath(RadBarcodeBase.GetRotatedMatrix(this.qR.BinaryMatrix, rotation));
			writer.Write(string.Format(CultureInfo.InvariantCulture, "<path shape-rendering=\"crispEdges\" stroke-width=\"2\" stroke=\"black\" fill=\"black\" d=\"", new object[0]));
			int num = 0;
			while (num + 3 < path.Count)
			{
				writer.Write(string.Format(CultureInfo.InvariantCulture, "M {0} {1} {2} {3} ", new object[]
				{
					path[num],
					path[num + 1],
					path[num + 2],
					path[num + 3]
				}));
				num += 4;
			}
			writer.Write("\"/>");
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x0016D120 File Offset: 0x0016B320
		private static List<int> GetPath(bool[,] matrix)
		{
			bool[,] array = new bool[matrix.GetLength(0), matrix.GetLength(1)];
			List<int> list = new List<int>();
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (matrix[i, j])
					{
						int num = j;
						while (j < matrix.GetLength(1) && matrix[i, j])
						{
							j++;
						}
						if (j - num > 1)
						{
							list.Add(2 * num);
							list.Add(2 * i + 1);
							list.Add(2 * j);
							list.Add(2 * i + 1);
						}
						else
						{
							array[i, num] = true;
						}
					}
				}
			}
			for (int k = 0; k < matrix.GetLength(1); k++)
			{
				for (int l = 0; l < matrix.GetLength(0); l++)
				{
					if (matrix[l, k])
					{
						int num2 = l;
						while (l < matrix.GetLength(0) && matrix[l, k])
						{
							l++;
						}
						if (l - num2 > 1 || array[num2, k])
						{
							list.Add(2 * k + 1);
							list.Add(2 * num2);
							list.Add(2 * k + 1);
							list.Add(2 * l);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x0016D274 File Offset: 0x0016B474
		public Image GetBitmap(int dotSize)
		{
			int length = this.qR.BinaryMatrix.GetLength(0);
			Bitmap bitmap = new Bitmap(length * dotSize, length * dotSize);
			for (int i = 0; i < length * dotSize; i++)
			{
				for (int j = 0; j < length * dotSize; j++)
				{
					bitmap.SetPixel(j, i, this.qR.BinaryMatrix[i / dotSize, j / dotSize] ? Color.Black : Color.White);
				}
			}
			return bitmap;
		}

		// Token: 0x06006118 RID: 24856 RVA: 0x0016D2E8 File Offset: 0x0016B4E8
		public virtual string GetDataURL(int dotSize, Rotation rotation)
		{
			Image bitmap = this.GetBitmap(dotSize);
			bitmap.RotateFlip(RadBarcodeBase.RotateTypeFromRotation(rotation));
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Png);
			return "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
		}

		// Token: 0x040017B6 RID: 6070
		private QRCode qR;

		// Token: 0x040017B7 RID: 6071
		private string encodedData = "";
	}
}
