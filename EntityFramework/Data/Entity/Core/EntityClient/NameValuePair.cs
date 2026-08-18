using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x02000349 RID: 841
	internal sealed class NameValuePair
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0009089D File Offset: 0x0008EA9D
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x000908A5 File Offset: 0x0008EAA5
		internal NameValuePair Next
		{
			get
			{
				return this._next;
			}
			set
			{
				if (this._next != null || value == null)
				{
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1014));
				}
				this._next = value;
			}
		}

		// Token: 0x04000A43 RID: 2627
		private NameValuePair _next;
	}
}
