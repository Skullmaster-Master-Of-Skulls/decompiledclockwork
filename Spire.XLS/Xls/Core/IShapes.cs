using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x020003F1 RID: 1009
	public interface IShapes : IExcelApplication, IEnumerable
	{
		// Token: 0x06003CA7 RID: 15527
		IPictureShape AddPicture(Image image, string pictureName, ImageFormatType imageFormat);

		// Token: 0x06003CA8 RID: 15528
		IPictureShape AddPicture(string fileName);

		// Token: 0x06003CA9 RID: 15529
		ICommentShape AddComment(string commentText, bool bIsParseOptions);

		// Token: 0x06003CAA RID: 15530
		ICommentShape AddComment(string commentText);

		// Token: 0x06003CAB RID: 15531
		ICommentShape AddComment();

		// Token: 0x06003CAC RID: 15532
		IChartShape Add();

		// Token: 0x06003CAD RID: 15533
		IShape AddCopy(IShape sourceShape);

		// Token: 0x06003CAE RID: 15534
		IShape AddCopy(IShape sourceShape, Dictionary<string, string> hashNewNames, List<int> arrFontIndexes);

		// Token: 0x06003CAF RID: 15535
		ITextBoxShape AddTextBox();

		// Token: 0x06003CB0 RID: 15536
		ICheckBoxShape AddCheckBox();

		// Token: 0x06003CB1 RID: 15537
		IComboBoxShape AddComboBox();

		// Token: 0x06003CB2 RID: 15538
		IRadioButton AddRadioButton();

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06003CB3 RID: 15539
		int Count { get; }

		// Token: 0x17000D02 RID: 3330
		IShape this[int index]
		{
			get;
		}

		// Token: 0x17000D03 RID: 3331
		IShape this[string strShapeName]
		{
			get;
		}
	}
}
