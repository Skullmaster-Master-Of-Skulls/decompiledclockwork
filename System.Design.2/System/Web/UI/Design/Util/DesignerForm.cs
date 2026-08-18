using System;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000160 RID: 352
	internal abstract partial class DesignerForm : Form
	{
		// Token: 0x06000C60 RID: 3168 RVA: 0x00051100 File Offset: 0x0004F300
		protected DesignerForm(IServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;
			this._firstActivate = true;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00051116 File Offset: 0x0004F316
		protected internal IServiceProvider ServiceProvider
		{
			get
			{
				return this._serviceProvider;
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00051134 File Offset: 0x0004F334
		protected void InitializeForm()
		{
			Font dialogFont = UIServiceHelper.GetDialogFont(this.ServiceProvider);
			if (dialogFont != null)
			{
				this.Font = dialogFont;
			}
			string @string = SR.GetString("RTL");
			if (!string.Equals(@string, "RTL_False", StringComparison.Ordinal))
			{
				this.RightToLeft = RightToLeft.Yes;
				this.RightToLeftLayout = true;
			}
			this.AutoScaleBaseSize = new Size(5, 14);
			base.HelpButton = true;
			base.MinimizeBox = false;
			base.MaximizeBox = false;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000511B6 File Offset: 0x0004F3B6
		protected override object GetService(Type serviceType)
		{
			if (this._serviceProvider != null)
			{
				return this._serviceProvider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x000511CE File Offset: 0x0004F3CE
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			if (this._firstActivate)
			{
				this._firstActivate = false;
				this.OnInitialActivated(e);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000C66 RID: 3174
		protected abstract string HelpTopic { get; }

		// Token: 0x06000C67 RID: 3175 RVA: 0x000511ED File Offset: 0x0004F3ED
		protected sealed override void OnHelpRequested(HelpEventArgs hevent)
		{
			this.ShowHelp();
			hevent.Handled = true;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnInitialActivated(EventArgs e)
		{
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x000511FC File Offset: 0x0004F3FC
		private void ShowHelp()
		{
			if (this.ServiceProvider != null)
			{
				IHelpService helpService = (IHelpService)this.ServiceProvider.GetService(typeof(IHelpService));
				if (helpService != null)
				{
					helpService.ShowHelpFromKeyword(this.HelpTopic);
				}
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0005123B File Offset: 0x0004F43B
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 274 && (int)m.WParam == 61824)
			{
				this.ShowHelp();
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x040007A1 RID: 1953
		private const int SC_CONTEXTHELP = 61824;

		// Token: 0x040007A2 RID: 1954
		private const int WM_SYSCOMMAND = 274;

		// Token: 0x040007A4 RID: 1956
		private bool _firstActivate;
	}
}
