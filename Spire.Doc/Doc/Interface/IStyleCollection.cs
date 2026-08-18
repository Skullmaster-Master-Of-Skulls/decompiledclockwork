using System;
using Spire.Doc.Documents;

namespace Spire.Doc.Interface
{
	// Token: 0x0200050A RID: 1290
	public interface IStyleCollection : ICollectionBase
	{
		// Token: 0x17000434 RID: 1076
		IStyle this[int index]
		{
			get;
		}

		// Token: 0x0600425C RID: 16988
		int Add(IStyle style);

		// Token: 0x0600425D RID: 16989
		Style FindByName(string name);

		// Token: 0x0600425E RID: 16990
		IStyle FindByName(string name, StyleType styleType);
	}
}
