using System;

namespace System.Configuration.Internal
{
	// Token: 0x020000AD RID: 173
	public interface IConfigErrorInfo
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060006EA RID: 1770
		string Filename { get; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060006EB RID: 1771
		int LineNumber { get; }
	}
}
