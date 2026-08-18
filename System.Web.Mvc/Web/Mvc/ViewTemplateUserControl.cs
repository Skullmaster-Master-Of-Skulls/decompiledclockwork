using System;

namespace System.Web.Mvc
{
	// Token: 0x0200018E RID: 398
	public class ViewTemplateUserControl<TModel> : ViewUserControl<TModel>
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0001E2D1 File Offset: 0x0001C4D1
		protected string FormattedModelValue
		{
			get
			{
				return base.ViewData.TemplateInfo.FormattedModelValue.ToString();
			}
		}
	}
}
