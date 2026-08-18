using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B8 RID: 952
	[SupportsEventValidation]
	internal class DataControlLinkButton : LinkButton
	{
		// Token: 0x06002DF8 RID: 11768 RVA: 0x0009611D File Offset: 0x0009431D
		internal DataControlLinkButton(IPostBackContainer container)
		{
			this._container = container;
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x0009612C File Offset: 0x0009432C
		// (set) Token: 0x06002DFA RID: 11770 RVA: 0x0009613E File Offset: 0x0009433E
		public override bool CausesValidation
		{
			get
			{
				return this._container == null && base.CausesValidation;
			}
			set
			{
				if (this._container != null)
				{
					throw new NotSupportedException(SR.GetString("CannotSetValidationOnDataControlButtons"));
				}
				base.CausesValidation = value;
			}
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x0009615F File Offset: 0x0009435F
		internal void EnableCallback(string argument)
		{
			this._enableCallback = true;
			this._callbackArgument = argument;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0009616F File Offset: 0x0009436F
		protected override PostBackOptions GetPostBackOptions()
		{
			if (this._container != null)
			{
				return this._container.GetPostBackOptions(this);
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0009618C File Offset: 0x0009438C
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.SetCallbackProperties();
			this.SetForeColor();
			base.Render(writer);
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000961A4 File Offset: 0x000943A4
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

		// Token: 0x06002DFF RID: 11775 RVA: 0x000961E8 File Offset: 0x000943E8
		protected virtual void SetForeColor()
		{
			if (!base.ControlStyle.IsSet(4))
			{
				Control control = this;
				for (int i = 0; i < 3; i++)
				{
					control = control.Parent;
					Color foreColor = ((WebControl)control).ForeColor;
					if (foreColor != Color.Empty)
					{
						this.ForeColor = foreColor;
						return;
					}
				}
			}
		}

		// Token: 0x04001FB8 RID: 8120
		private IPostBackContainer _container;

		// Token: 0x04001FB9 RID: 8121
		private string _callbackArgument;

		// Token: 0x04001FBA RID: 8122
		private bool _enableCallback;
	}
}
