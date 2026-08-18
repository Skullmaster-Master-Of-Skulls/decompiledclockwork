using System;
using System.Globalization;
using System.Resources;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F4 RID: 1524
	public interface IResourceService
	{
		// Token: 0x0600384B RID: 14411
		IResourceReader GetResourceReader(CultureInfo info);

		// Token: 0x0600384C RID: 14412
		IResourceWriter GetResourceWriter(CultureInfo info);
	}
}
