using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B7 RID: 951
	[SupportsEventValidation]
	internal sealed class DataControlImageButton : ImageButton
	{
		// Token: 0x06002DF1 RID: 11761 RVA: 0x0009608E File Offset: 0x0009428E
		internal DataControlImageButton(IPostBackContainer container)
		{
			this._container = container;
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x0009522F File Offset: 0x0009342F
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

		// Token: 0x06002DF4 RID: 11764 RVA: 0x0009609D File Offset: 0x0009429D
		internal void EnableCallback(string argument)
		{
			this._enableCallback = true;
			this._callbackArgument = argument;
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000960AD File Offset: 0x000942AD
		protected sealed override PostBackOptions GetPostBackOptions()
		{
			if (this._container != null)
			{
				return this._container.GetPostBackOptions(this);
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000960CA File Offset: 0x000942CA
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.SetCallbackProperties();
			base.Render(writer);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000960DC File Offset: 0x000942DC
		private void SetCallbackProperties()
		{
			if (this._enableCallback)
			{
				ICallbackContainer callbackContainer = this._container as ICallbackContainer;
				if (callbackContainer != null)
				{
					string callbackScript = callbackContainer.GetCallbackScript(this, this._callbackArgument);
					if (!string.IsNullOrEmpty(callbackScript))
					{
						this.OnClientClick = callbackScript;
					}
				}
			}
		}

		// Token: 0x04001FB5 RID: 8117
		private IPostBackContainer _container;

		// Token: 0x04001FB6 RID: 8118
		private string _callbackArgument;

		// Token: 0x04001FB7 RID: 8119
		private bool _enableCallback;
	}
}
