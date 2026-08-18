using System;

namespace System.Windows.Forms
{
	// Token: 0x0200028D RID: 653
	public interface IDataGridEditingService
	{
		// Token: 0x0600299B RID: 10651
		bool BeginEdit(DataGridColumnStyle gridColumn, int rowNumber);

		// Token: 0x0600299C RID: 10652
		bool EndEdit(DataGridColumnStyle gridColumn, int rowNumber, bool shouldAbort);
	}
}
