using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B0 RID: 944
	[SupportsEventValidation]
	internal sealed class DataControlButton : Button
	{
		// Token: 0x06002D95 RID: 11669 RVA: 0x00095220 File Offset: 0x00093420
		internal DataControlButton(IPostBackContainer container)
		{
			this._container = container;
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x0009522F File Offset: 0x0009342F
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

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06002D99 RID: 11673 RVA: 0x00010D64 File Offset: 0x0000EF64
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

		// Token: 0x06002D9A RID: 11674 RVA: 0x00095240 File Offset: 0x00093440
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

		// Token: 0x04001F96 RID: 8086
		private IPostBackContainer _container;
	}
}
