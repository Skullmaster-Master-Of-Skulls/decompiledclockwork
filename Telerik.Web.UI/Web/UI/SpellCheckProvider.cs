using System;

namespace Telerik.Web.UI
{
	// Token: 0x020011E9 RID: 4585
	public enum SpellCheckProvider
	{
		// Token: 0x040031D3 RID: 12755
		[Obsolete("This value is obsolete. Please, use PhoneticProvider instead.", false)]
		TelerikProvider,
		// Token: 0x040031D4 RID: 12756
		EditDistanceProvider,
		// Token: 0x040031D5 RID: 12757
		PhoneticProvider,
		// Token: 0x040031D6 RID: 12758
		MicrosoftWordProvider
	}
}
