using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000626 RID: 1574
	public interface IUnvalidatedValueProviderSource : IValueProviderSource
	{
		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06004ECF RID: 20175
		// (set) Token: 0x06004ED0 RID: 20176
		bool ValidateInput { get; set; }
	}
}
