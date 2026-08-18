using System;

namespace System.ComponentModel
{
	// Token: 0x02000567 RID: 1383
	public interface INestedSite : ISite, IServiceProvider
	{
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060033B4 RID: 13236
		string FullName { get; }
	}
}
