using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000321 RID: 801
	internal class PrintDialogDesigner : ComponentDesigner
	{
		// Token: 0x06001FCA RID: 8138 RVA: 0x000C0D8C File Offset: 0x000BEF8C
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			PrintDialog printDialog = base.Component as PrintDialog;
			if (printDialog != null)
			{
				printDialog.UseEXDialog = true;
			}
		}
	}
}
