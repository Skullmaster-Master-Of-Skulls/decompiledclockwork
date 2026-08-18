using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000861 RID: 2145
	internal class WebServiceBuildProvider : SimpleHandlerBuildProvider
	{
		// Token: 0x06006568 RID: 25960 RVA: 0x00164DD0 File Offset: 0x00162FD0
		protected override SimpleWebHandlerParser CreateParser()
		{
			return new WebServiceParser(base.VirtualPath);
		}
	}
}
