using System;
using System.ComponentModel;

namespace Telerik.Web
{
	// Token: 0x02001AF7 RID: 6903
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum ClientStateLogEntryType
	{
		// Token: 0x04004A8F RID: 19087
		Invalid,
		// Token: 0x04004A90 RID: 19088
		Insert,
		// Token: 0x04004A91 RID: 19089
		Remove,
		// Token: 0x04004A92 RID: 19090
		Clear,
		// Token: 0x04004A93 RID: 19091
		Update,
		// Token: 0x04004A94 RID: 19092
		Reorder
	}
}
