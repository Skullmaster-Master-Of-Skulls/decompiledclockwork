using System;

namespace Spire.Doc.Interface
{
	// Token: 0x020004ED RID: 1261
	public interface IDocumentObjectCollection : ICollectionBase
	{
		// Token: 0x170003F3 RID: 1011
		DocumentObject this[int index]
		{
			get;
		}
	}
}
