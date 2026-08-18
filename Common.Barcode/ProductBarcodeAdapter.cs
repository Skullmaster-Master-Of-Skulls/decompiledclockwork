using System;
using System.Drawing;
using BarcodeLib;
using ClockWorkLogger;

namespace TechnoPro.Common.Barcode
{
	// Token: 0x02000002 RID: 2
	public static class ProductBarcodeAdapter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static Image Encode(this string barcodeText, int imgWidth = 200, int imgHeight = 100)
		{
			Image result;
			try
			{
				result = new Barcode
				{
					Alignment = AlignmentPositions.CENTER,
					EncodedType = TYPE.CODE39,
					IncludeLabel = true,
					RotateFlipType = RotateFlipType.RotateNoneFlipNone,
					BackColor = Color.White,
					ForeColor = Color.Black,
					LabelPosition = LabelPositions.BOTTOMCENTER,
					Width = imgWidth,
					Height = imgHeight
				}.Encode(TYPE.CODE39, barcodeText.ToUpper().Trim());
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ProductBarcodeAdapter::Encode: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}
	}
}
