using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x0200021E RID: 542
	public class HtmlFileNameEditor : FileNameEditor
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x000ADDA4 File Offset: 0x000ACDA4
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 4;
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
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("䠟嘡䤣䨥", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("栟瘡椣樥ࠧ䰩䔫䈭唯䄱ᐳḵሷᐹ吻䨽ⴿ⹁罃晅扇摉⑋㩍㵏筑⡓籕癗㉙⡛㍝౟奡乣䡥gṩū", a_);
		}
	}
}
