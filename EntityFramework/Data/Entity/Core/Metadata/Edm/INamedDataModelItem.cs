using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020001F6 RID: 502
	internal interface INamedDataModelItem
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001192 RID: 4498
		string Name { get; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06001193 RID: 4499
		string Identity { get; }
	}
}
