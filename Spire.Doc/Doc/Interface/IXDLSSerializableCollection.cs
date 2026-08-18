using System;
using System.Collections;

namespace Spire.Doc.Interface
{
	// Token: 0x02000502 RID: 1282
	public interface IXDLSSerializableCollection : IEnumerable
	{
		// Token: 0x0600423F RID: 16959
		IDocumentSerializable AddNewItem(IXDLSContentReader reader);

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06004240 RID: 16960
		string TagItemName { get; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06004241 RID: 16961
		int Count { get; }
	}
}
