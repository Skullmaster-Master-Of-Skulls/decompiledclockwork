using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000535 RID: 1333
	[SupportsEventValidation]
	internal class DataControlLinkButton : LinkButton
	{
		// Token: 0x060041B3 RID: 16819 RVA: 0x001100A8 File Offset: 0x0010F0A8
		internal DataControlLinkButton(IPostBackContainer container)
		{
			this._container = container;
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x001100B7 File Offset: 0x0010F0B7
		// (set) Token: 0x060041B5 RID: 16821 RVA: 0x001100C9 File Offset: 0x0010F0C9
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

		// Token: 0x060041B6 RID: 16822 RVA: 0x001100EA File Offset: 0x0010F0EA
		internal void EnableCallback(string argument)
		{
			this._enableCallback = true;
			this._callbackArgument = argument;
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x001100FA File Offset: 0x0010F0FA
		protected override PostBackOptions GetPostBackOptions()
		{
			if (this._container != null)
			{
				return this._container.GetPostBackOptions(this);
			}
			return base.GetPostBackOptions();
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x00110117 File Offset: 0x0010F117
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.SetCallbackProperties();
			this.SetForeColor();
			base.Render(writer);
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x0011012C File Offset: 0x0010F12C
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

		// Token: 0x060041BA RID: 16826 RVA: 0x00110170 File Offset: 0x0010F170
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

		// Token: 0x040028BB RID: 10427
		private IPostBackContainer _container;

		// Token: 0x040028BC RID: 10428
		private string _callbackArgument;

		// Token: 0x040028BD RID: 10429
		private bool _enableCallback;
	}
}
