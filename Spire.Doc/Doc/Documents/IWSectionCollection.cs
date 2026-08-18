using System;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x020004EC RID: 1260
	public interface IWSectionCollection : IDocumentObjectCollection
	{
		// Token: 0x170003F2 RID: 1010
		Section this[int index]
		{
			get;
		}

		// Token: 0x0600411F RID: 16671
		int Add(ISection section);

		// Token: 0x06004120 RID: 16672
		int IndexOf(ISection section);
	}
}
