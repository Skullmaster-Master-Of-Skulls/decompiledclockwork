using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000862 RID: 2146
	internal class WebHandlerBuildProvider : SimpleHandlerBuildProvider
	{
		// Token: 0x0600656A RID: 25962 RVA: 0x00164DE5 File Offset: 0x00162FE5
		protected override SimpleWebHandlerParser CreateParser()
		{
			return new WebHandlerParser(base.VirtualPath);
		}
	}
}
