using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002F7 RID: 759
	public interface IMenuEditorService
	{
		// Token: 0x06001E48 RID: 7752
		Menu GetMenu();

		// Token: 0x06001E49 RID: 7753
		bool IsActive();

		// Token: 0x06001E4A RID: 7754
		void SetMenu(Menu menu);

		// Token: 0x06001E4B RID: 7755
		void SetSelection(MenuItem item);

		// Token: 0x06001E4C RID: 7756
		bool MessageFilter(ref Message m);
	}
}
