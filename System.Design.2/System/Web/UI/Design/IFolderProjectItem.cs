using System;
using System.Collections;

namespace System.Web.UI.Design
{
	// Token: 0x02000053 RID: 83
	public interface IFolderProjectItem
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A3 RID: 675
		ICollection Children { get; }

		// Token: 0x060002A4 RID: 676
		IDocumentProjectItem AddDocument(string name, byte[] content);

		// Token: 0x060002A5 RID: 677
		IFolderProjectItem AddFolder(string name);
	}
}
