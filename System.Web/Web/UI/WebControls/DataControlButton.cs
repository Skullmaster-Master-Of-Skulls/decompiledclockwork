using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200052B RID: 1323
	[SupportsEventValidation]
	internal sealed class DataControlButton : Button
	{
		// Token: 0x0600412C RID: 16684 RVA: 0x0010ECAC File Offset: 0x0010DCAC
		internal DataControlButton(IPostBackContainer container)
		{
			this._container = container;
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x0010ECBB File Offset: 0x0010DCBB
		// (set) Token: 0x0600412E RID: 16686 RVA: 0x0010ECBE File Offset: 0x0010DCBE
		public override bool CausesValidation
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("CannotSetValidationOnDataControlButtons"));
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x0010ECCF File Offset: 0x0010DCCF
		// (set) Token: 0x06004130 RID: 16688 RVA: 0x0010ECD2 File Offset: 0x0010DCD2
		public override bool UseSubmitBehavior
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x0010ECDC File Offset: 0x0010DCDC
		protected sealed override PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions;
			if (this._container != null)
			{
				postBackOptions = this._container.GetPostBackOptions(this);
				if (this.Page != null)
				{
					postBackOptions.ClientSubmit = true;
				}
			}
			else
			{
				postBackOptions = base.GetPostBackOptions();
			}
			return postBackOptions;
		}

		// Token: 0x0400289A RID: 10394
		private IPostBackContainer _container;
	}
}
