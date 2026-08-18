using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000217 RID: 535
	public class DBFFileNameEditor : FileNameEditor
	{
		// Token: 0x06000FFD RID: 4093 RVA: 0x000ACB60 File Offset: 0x000ABB60
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 6;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.InitializeDialog(openFileDialog);
			openFileDialog.CheckFileExists = false;
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("䘡䘣䀥", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("昡昣急ࠧ䰩䔫䈭唯䄱ᐳḵሷᐹ堻尽☿歁㡃汅晇⹉⹋⡍", a_);
		}
	}
}
