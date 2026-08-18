using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x020005D2 RID: 1490
	public interface ITabSheet : IExcelApplication
	{
		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06005880 RID: 22656
		// (set) Token: 0x06005881 RID: 22657
		ExcelColors TabKnownColor { get; set; }

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06005882 RID: 22658
		// (set) Token: 0x06005883 RID: 22659
		Color TabColor { get; set; }

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06005884 RID: 22660
		IPictures Pictures { get; }

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06005885 RID: 22661
		IWorkbook Workbook { get; }

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06005886 RID: 22662
		// (set) Token: 0x06005887 RID: 22663
		bool IsRightToLeft { get; set; }

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06005888 RID: 22664
		bool IsSelected { get; }

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06005889 RID: 22665
		int TabIndex { get; }

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600588A RID: 22666
		// (set) Token: 0x0600588B RID: 22667
		string Name { get; set; }

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x0600588C RID: 22668
		// (set) Token: 0x0600588D RID: 22669
		WorksheetVisibility Visibility { get; set; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x0600588E RID: 22670
		ITextBoxes TextBoxes { get; }

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x0600588F RID: 22671
		ICheckBoxes CheckBoxes { get; }

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06005890 RID: 22672
		IComboBoxes ComboBoxes { get; }

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06005891 RID: 22673
		IRadioButtons RadioButtons { get; }

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06005892 RID: 22674
		string CodeName { get; }

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06005893 RID: 22675
		bool ProtectContents { get; }

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06005894 RID: 22676
		bool ProtectDrawingObjects { get; }

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06005895 RID: 22677
		bool ProtectScenarios { get; }

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06005896 RID: 22678
		SheetProtectionType Protection { get; }

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06005897 RID: 22679
		bool IsPasswordProtected { get; }

		// Token: 0x06005898 RID: 22680
		void Activate();

		// Token: 0x06005899 RID: 22681
		void Select();

		// Token: 0x0600589A RID: 22682
		void Unselect();

		// Token: 0x0600589B RID: 22683
		void Protect(string password);

		// Token: 0x0600589C RID: 22684
		void Protect(string password, SheetProtectionType options);

		// Token: 0x0600589D RID: 22685
		void Unprotect(string password);
	}
}
