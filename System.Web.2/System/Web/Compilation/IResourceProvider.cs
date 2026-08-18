using System;
using System.Globalization;
using System.Resources;

namespace System.Web.Compilation
{
	// Token: 0x02000846 RID: 2118
	public interface IResourceProvider
	{
		// Token: 0x060064AB RID: 25771
		object GetObject(string resourceKey, CultureInfo culture);

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x060064AC RID: 25772
		IResourceReader ResourceReader { get; }
	}
}
