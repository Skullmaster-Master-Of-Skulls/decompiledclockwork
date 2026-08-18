using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace Spire.Xls.Core
{
	// Token: 0x02000201 RID: 513
	public interface IPictures : IExcelApplication, IEnumerable
	{
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06001CF1 RID: 7409
		int Count { get; }

		// Token: 0x17000AC5 RID: 2757
		IPictureShape this[int Index]
		{
			get;
		}

		// Token: 0x06001CF3 RID: 7411
		IPictureShape Add(Image image, string pictureName);

		// Token: 0x06001CF4 RID: 7412
		IPictureShape Add(Image image, string pictureName, ImageFormatType imageFormat);

		// Token: 0x06001CF5 RID: 7413
		IPictureShape Add(string strFileName);

		// Token: 0x06001CF6 RID: 7414
		IPictureShape Add(string strFileName, ImageFormatType imageFormat);

		// Token: 0x06001CF7 RID: 7415
		IPictureShape Add(int topRow, int leftColumn, Image image);

		// Token: 0x06001CF8 RID: 7416
		IPictureShape Add(int topRow, int leftColumn, Image image, ImageFormatType imageFormat);

		// Token: 0x06001CF9 RID: 7417
		IPictureShape Add(int topRow, int leftColumn, Stream stream);

		// Token: 0x06001CFA RID: 7418
		IPictureShape Add(int topRow, int leftColumn, Stream stream, ImageFormatType imageFormat);

		// Token: 0x06001CFB RID: 7419
		IPictureShape Add(int topRow, int leftColumn, string fileName);

		// Token: 0x06001CFC RID: 7420
		IPictureShape Add(int topRow, int leftColumn, string fileName, ImageFormatType imageFormat);

		// Token: 0x06001CFD RID: 7421
		IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Image image);

		// Token: 0x06001CFE RID: 7422
		IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Image image, ImageFormatType imageFormat);

		// Token: 0x06001CFF RID: 7423
		IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Stream stream);

		// Token: 0x06001D00 RID: 7424
		IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Stream stream, ImageFormatType imageFormat);

		// Token: 0x06001D01 RID: 7425
		IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, string fileName);

		// Token: 0x06001D02 RID: 7426
		IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, string fileName, ImageFormatType imageFormat);

		// Token: 0x06001D03 RID: 7427
		IPictureShape Add(int topRow, int leftColumn, Image image, int scaleWidth, int scaleHeight);

		// Token: 0x06001D04 RID: 7428
		IPictureShape Add(int topRow, int leftColumn, Image image, int scaleWidth, int scaleHeight, ImageFormatType imageFormat);

		// Token: 0x06001D05 RID: 7429
		IPictureShape Add(int topRow, int leftColumn, Stream stream, int scaleWidth, int scaleHeight);

		// Token: 0x06001D06 RID: 7430
		IPictureShape Add(int topRow, int leftColumn, Stream stream, int scaleWidth, int scaleHeight, ImageFormatType imageFormat);

		// Token: 0x06001D07 RID: 7431
		IPictureShape Add(int topRow, int leftColumn, string fileName, int scaleWidth, int scaleHeight);

		// Token: 0x06001D08 RID: 7432
		IPictureShape Add(int topRow, int leftColumn, string fileName, int scaleWidth, int scaleHeight, ImageFormatType imageFormat);
	}
}
