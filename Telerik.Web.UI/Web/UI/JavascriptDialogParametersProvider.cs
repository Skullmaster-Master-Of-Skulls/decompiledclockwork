using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200103A RID: 4154
	internal class JavascriptDialogParametersProvider : DialogParametersProvider
	{
		// Token: 0x0600A38B RID: 41867 RVA: 0x00246310 File Offset: 0x00244510
		public JavascriptDialogParametersProvider(Page page) : base(page)
		{
		}

		// Token: 0x0600A38C RID: 41868 RVA: 0x00246319 File Offset: 0x00244519
		public override DialogParameters GetDialogParameters(string dialogOpenerIdentifier, string dialogName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600A38D RID: 41869 RVA: 0x00246320 File Offset: 0x00244520
		public override void StoreAllParameters(string dialogOpenerIdentifier, DialogParametersDictionary dialogDefinitions)
		{
			throw new NotImplementedException();
		}
	}
}
