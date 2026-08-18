using System;
using Spire.Doc.Collections;

namespace Spire.Doc.Interface
{
	// Token: 0x02000094 RID: 148
	public interface ICompositeObject : IDocumentObject
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D7 RID: 215
		DocumentObjectCollection ChildObjects { get; }
	}
}
