using System;

namespace Telerik.Licensing
{
	// Token: 0x02000414 RID: 1044
	internal interface ILicenseManager
	{
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060025D9 RID: 9689
		ILicenseContextData ContextData { get; }

		// Token: 0x060025DA RID: 9690
		void SaveLicenseKey(Type type, ILicenseKey key);

		// Token: 0x060025DB RID: 9691
		ILicenseKey ExtractLicenseKey(Type type);
	}
}
