using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000453 RID: 1107
	public sealed class QRCodeWriter
	{
		// Token: 0x0600255B RID: 9563 RVA: 0x000E237D File Offset: 0x000E137D
		public ByteMatrix Encode(string contents, int width, int height)
		{
			return this.Encode(contents, width, height, null);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000E238C File Offset: 0x000E138C
		public ByteMatrix Encode(string contents, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			if (contents == null || contents.Length == 0)
			{
				throw new ArgumentException("Found empty contents");
			}
			if (width < 0 || height < 0)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Requested dimensions are too small: ",
					width,
					'x',
					height
				}));
			}
			ErrorCorrectionLevel ecLevel = ErrorCorrectionLevel.L;
			if (hints != null && hints.ContainsKey(EncodeHintType.ERROR_CORRECTION))
			{
				ecLevel = (ErrorCorrectionLevel)hints[EncodeHintType.ERROR_CORRECTION];
			}
			QRCode qrcode = new QRCode();
			Encoder.Encode(contents, ecLevel, hints, qrcode);
			return QRCodeWriter.RenderResult(qrcode, width, height);
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000E2430 File Offset: 0x000E1430
		private static ByteMatrix RenderResult(QRCode code, int width, int height)
		{
			ByteMatrix matrix = code.GetMatrix();
			int width2 = matrix.GetWidth();
			int height2 = matrix.GetHeight();
			int num = width2 + 8;
			int num2 = height2 + 8;
			int num3 = Math.Max(width, num);
			int num4 = Math.Max(height, num2);
			int num5 = Math.Min(num3 / num, num4 / num2);
			int num6 = (num3 - width2 * num5) / 2;
			int num7 = (num4 - height2 * num5) / 2;
			ByteMatrix byteMatrix = new ByteMatrix(num3, num4);
			sbyte[][] array = byteMatrix.GetArray();
			sbyte[] array2 = new sbyte[num3];
			for (int i = 0; i < num7; i++)
			{
				QRCodeWriter.SetRowColor(array[i], -1);
			}
			sbyte[][] array3 = matrix.GetArray();
			for (int j = 0; j < height2; j++)
			{
				for (int k = 0; k < num6; k++)
				{
					array2[k] = -1;
				}
				int num8 = num6;
				for (int l = 0; l < width2; l++)
				{
					sbyte b = (array3[j][l] == 1) ? 0 : -1;
					for (int m = 0; m < num5; m++)
					{
						array2[num8 + m] = b;
					}
					num8 += num5;
				}
				num8 = num6 + width2 * num5;
				for (int n = num8; n < num3; n++)
				{
					array2[n] = -1;
				}
				num8 = num7 + j * num5;
				for (int num9 = 0; num9 < num5; num9++)
				{
					Array.Copy(array2, 0, array[num8 + num9], 0, num3);
				}
			}
			int num10 = num7 + height2 * num5;
			for (int num11 = num10; num11 < num4; num11++)
			{
				QRCodeWriter.SetRowColor(array[num11], -1);
			}
			return byteMatrix;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000E25C0 File Offset: 0x000E15C0
		private static void SetRowColor(sbyte[] row, sbyte value)
		{
			for (int i = 0; i < row.Length; i++)
			{
				row[i] = value;
			}
		}

		// Token: 0x04001A27 RID: 6695
		private const int QUIET_ZONE_SIZE = 4;
	}
}
