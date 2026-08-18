using System;
using Spire.Xls.Core.Interface;

namespace Spire.Xls.Core
{
	// Token: 0x0200025A RID: 602
	public interface ICustomDocumentProperties
	{
		// Token: 0x17000C76 RID: 3190
		IDocumentProperty this[string strName]
		{
			get;
		}

		// Token: 0x17000C77 RID: 3191
		IDocumentProperty this[int iIndex]
		{
			get;
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06002403 RID: 9219
		int Count { get; }

		// Token: 0x06002404 RID: 9220
		void Remove(string strName);

		// Token: 0x06002405 RID: 9221
		bool Contains(string strName);

		// Token: 0x06002406 RID: 9222
		void Clear();
	}
}
