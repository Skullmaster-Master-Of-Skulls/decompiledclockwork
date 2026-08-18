using System;

namespace System.Net
{
	// Token: 0x020001B4 RID: 436
	internal class HeaderInfo
	{
		// Token: 0x0600113D RID: 4413 RVA: 0x0005D9A2 File Offset: 0x0005BBA2
		internal HeaderInfo(string name, bool requestRestricted, bool responseRestricted, bool multi, HeaderParser p)
		{
			this.HeaderName = name;
			this.IsRequestRestricted = requestRestricted;
			this.IsResponseRestricted = responseRestricted;
			this.Parser = p;
			this.AllowMultiValues = multi;
		}

		// Token: 0x04001416 RID: 5142
		internal readonly bool IsRequestRestricted;

		// Token: 0x04001417 RID: 5143
		internal readonly bool IsResponseRestricted;

		// Token: 0x04001418 RID: 5144
		internal readonly HeaderParser Parser;

		// Token: 0x04001419 RID: 5145
		internal readonly string HeaderName;

		// Token: 0x0400141A RID: 5146
		internal readonly bool AllowMultiValues;
	}
}
