using System;
using System.Web.UI.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200014C RID: 332
	public class NoBotExtenderDesigner : ControlDesigner
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x0001785C File Offset: 0x00015A5C
		public override string GetDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml();
		}
	}
}
