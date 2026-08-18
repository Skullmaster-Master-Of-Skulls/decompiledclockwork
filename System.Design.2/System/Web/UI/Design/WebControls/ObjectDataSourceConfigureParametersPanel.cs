using System;
using System.Design;
using System.Drawing;
using System.Reflection;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000EE RID: 238
	internal sealed class ObjectDataSourceConfigureParametersPanel : WizardPanel
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x0002E198 File Offset: 0x0002C398
		public ObjectDataSourceConfigureParametersPanel(ObjectDataSourceDesigner objectDataSourceDesigner)
		{
			this._objectDataSourceDesigner = objectDataSourceDesigner;
			this._objectDataSource = (ObjectDataSource)this._objectDataSourceDesigner.Component;
			this.InitializeComponent();
			this.InitializeUI();
			this._parameterEditorUserControl.SetAllowCollectionChanges(false);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		private void InitializeComponent()
		{
			this._helpLabel = new System.Windows.Forms.Label();
			this._parameterEditorUserControl = new ParameterEditorUserControl(this._objectDataSource.Site, this._objectDataSource);
			this._signatureLabel = new System.Windows.Forms.Label();
			this._signatureTextBox = new System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this._helpLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._helpLabel.Location = new Point(0, 0);
			this._helpLabel.Name = "_helpLabel";
			this._helpLabel.Size = new Size(544, 45);
			this._helpLabel.TabIndex = 10;
			this._parameterEditorUserControl.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._parameterEditorUserControl.Location = new Point(0, 38);
			this._parameterEditorUserControl.Name = "_parameterEditorUserControl";
			this._parameterEditorUserControl.Size = new Size(544, 152);
			this._parameterEditorUserControl.TabIndex = 20;
			this._parameterEditorUserControl.ParametersChanged += this.OnParameterEditorUserControlParametersChanged;
			this._signatureLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._signatureLabel.Location = new Point(0, 214);
			this._signatureLabel.Name = "_signatureLabel";
			this._signatureLabel.Size = new Size(544, 16);
			this._signatureLabel.TabIndex = 30;
			this._signatureTextBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._signatureTextBox.BackColor = SystemColors.Control;
			this._signatureTextBox.Location = new Point(0, 232);
			this._signatureTextBox.Multiline = true;
			this._signatureTextBox.Name = "_signatureTextBox";
			this._signatureTextBox.ReadOnly = true;
			this._signatureTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._signatureTextBox.Size = new Size(544, 42);
			this._signatureTextBox.TabIndex = 40;
			this._signatureTextBox.Text = "";
			base.Controls.Add(this._signatureTextBox);
			base.Controls.Add(this._signatureLabel);
			base.Controls.Add(this._parameterEditorUserControl);
			base.Controls.Add(this._helpLabel);
			base.Name = "ObjectDataSourceConfigureParametersPanel";
			base.Size = new Size(544, 274);
			base.ResumeLayout(false);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0002E448 File Offset: 0x0002C648
		public void InitializeParameters(ParameterCollection selectParameters)
		{
			Parameter[] array = new Parameter[selectParameters.Count];
			selectParameters.CopyTo(array, 0);
			this._parameterEditorUserControl.AddParameters(array);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002E475 File Offset: 0x0002C675
		private void InitializeUI()
		{
			base.Caption = SR.GetString("ObjectDataSourceConfigureParametersPanel_PanelCaption");
			this._helpLabel.Text = SR.GetString("ObjectDataSourceConfigureParametersPanel_HelpLabel");
			this._signatureLabel.Text = SR.GetString("ObjectDataSource_General_MethodSignatureLabel");
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002E4B4 File Offset: 0x0002C6B4
		protected internal override void OnComplete()
		{
			this._objectDataSource.SelectParameters.Clear();
			Parameter[] parameters = this._parameterEditorUserControl.GetParameters();
			foreach (Parameter parameter in parameters)
			{
				this._objectDataSource.SelectParameters.Add(parameter);
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool OnNext()
		{
			return true;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002E503 File Offset: 0x0002C703
		private void OnParameterEditorUserControlParametersChanged(object sender, EventArgs e)
		{
			this.UpdateUI();
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00003937 File Offset: 0x00001B37
		public override void OnPrevious()
		{
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0002E50B File Offset: 0x0002C70B
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				base.ParentWizard.NextButton.Enabled = false;
				this.UpdateUI();
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002E533 File Offset: 0x0002C733
		public void ResetUI()
		{
			this._parameterEditorUserControl.ClearParameters();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002E540 File Offset: 0x0002C740
		public void SetMethod(MethodInfo selectMethodInfo)
		{
			this._signatureTextBox.Text = ObjectDataSourceMethodEditor.GetMethodSignature(selectMethodInfo);
			Parameter[] parameters = ObjectDataSourceDesigner.MergeParameters(this._parameterEditorUserControl.GetParameters(), selectMethodInfo);
			this._parameterEditorUserControl.ClearParameters();
			this._parameterEditorUserControl.AddParameters(parameters);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002E587 File Offset: 0x0002C787
		private void UpdateUI()
		{
			base.ParentWizard.FinishButton.Enabled = this._parameterEditorUserControl.ParametersConfigured;
		}

		// Token: 0x040004D8 RID: 1240
		private System.Windows.Forms.Label _helpLabel;

		// Token: 0x040004D9 RID: 1241
		private System.Windows.Forms.Label _signatureLabel;

		// Token: 0x040004DA RID: 1242
		private ParameterEditorUserControl _parameterEditorUserControl;

		// Token: 0x040004DB RID: 1243
		private System.Windows.Forms.TextBox _signatureTextBox;

		// Token: 0x040004DC RID: 1244
		private ObjectDataSource _objectDataSource;

		// Token: 0x040004DD RID: 1245
		private ObjectDataSourceDesigner _objectDataSourceDesigner;
	}
}
