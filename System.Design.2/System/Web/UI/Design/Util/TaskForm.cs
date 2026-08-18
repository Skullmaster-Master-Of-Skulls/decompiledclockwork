using System;
using System.Design;
using System.Drawing;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000165 RID: 357
	internal abstract partial class TaskForm : TaskFormBase
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x00051722 File Offset: 0x0004F922
		public TaskForm(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00051737 File Offset: 0x0004F937
		protected Button OKButton
		{
			get
			{
				return this._okButton;
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00051A7F File Offset: 0x0004FC7F
		private void InitializeUI()
		{
			this._cancelButton.Text = SR.GetString("Wizard_CancelButton");
			this._okButton.Text = SR.GetString("OKCaption");
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002AF61 File Offset: 0x00029161
		protected virtual void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnOKButtonClick(object sender, EventArgs e)
		{
		}
	}
}
