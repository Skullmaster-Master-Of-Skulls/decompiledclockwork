using System;

namespace Spire.Doc.Interface
{
	// Token: 0x02000507 RID: 1287
	public interface IDocumentCollection : ICollectionBase
	{
		// Token: 0x17000431 RID: 1073
		IDocument this[int index]
		{
			get;
		}
	}
}
