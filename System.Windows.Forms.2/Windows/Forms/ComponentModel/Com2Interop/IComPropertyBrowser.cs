using System;
using System.ComponentModel.Design;
using Microsoft.Win32;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B6 RID: 1206
	public interface IComPropertyBrowser
	{
		// Token: 0x06004F7A RID: 20346
		void DropDownDone();

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06004F7B RID: 20347
		bool InPropertySet { get; }

		// Token: 0x14000415 RID: 1045
		// (add) Token: 0x06004F7C RID: 20348
		// (remove) Token: 0x06004F7D RID: 20349
		event ComponentRenameEventHandler ComComponentNameChanged;

		// Token: 0x06004F7E RID: 20350
		bool EnsurePendingChangesCommitted();

		// Token: 0x06004F7F RID: 20351
		void HandleF4();

		// Token: 0x06004F80 RID: 20352
		void LoadState(RegistryKey key);

		// Token: 0x06004F81 RID: 20353
		void SaveState(RegistryKey key);
	}
}
