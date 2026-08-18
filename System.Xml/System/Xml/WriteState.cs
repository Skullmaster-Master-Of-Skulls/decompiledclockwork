using System;

namespace System.Xml
{
	// Token: 0x020000A8 RID: 168
	public enum WriteState
	{
		// Token: 0x04000828 RID: 2088
		Start,
		// Token: 0x04000829 RID: 2089
		Prolog,
		// Token: 0x0400082A RID: 2090
		Element,
		// Token: 0x0400082B RID: 2091
		Attribute,
		// Token: 0x0400082C RID: 2092
		Content,
		// Token: 0x0400082D RID: 2093
		Closed,
		// Token: 0x0400082E RID: 2094
		Error
	}
}
