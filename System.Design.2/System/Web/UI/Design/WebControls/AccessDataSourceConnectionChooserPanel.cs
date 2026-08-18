using System;
using System.ComponentModel;
using System.ComponentModel.Design.Data;
using System.Design;
using System.Drawing;
using System.IO;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000095 RID: 149
	internal class AccessDataSourceConnectionChooserPanel : SqlDataSourceConnectionPanel
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x000140DA File Offset: 0x000122DA
		public AccessDataSourceConnectionChooserPanel(AccessDataSourceDesigner accessDataSourceDesigner, AccessDataSource accessDataSource) : base(accessDataSourceDesigner)
		{
			this._accessDataSource = accessDataSource;
			this._accessDataSourceDesigner = accessDataSourceDesigner;
			this.InitializeComponent();
			this.InitializeUI();
			this.DataFile = this._accessDataSource.DataFile;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00014110 File Offset: 0x00012310
		public override DesignerDataConnection DataConnection
		{
			get
			{
				AccessDataSource accessDataSource = new AccessDataSource();
				accessDataSource.DataFile = this.DataFile;
				return new DesignerDataConnection("AccessDataSource", accessDataSource.ProviderName, AccessDataSourceDesigner.GetConnectionString(base.ServiceProvider, accessDataSource));
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001414B File Offset: 0x0001234B
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00014158 File Offset: 0x00012358
		private string DataFile
		{
			get
			{
				return this._dataFileTextBox.Text;
			}
			set
			{
				this._dataFileTextBox.Text = value;
				this._dataFileTextBox.Select(0, 0);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00014174 File Offset: 0x00012374
		private void InitializeComponent()
		{
			this._dataFileLabel = new System.Windows.Forms.Label();
			this._dataFileTextBox = new System.Windows.Forms.TextBox();
			this._selectFileButton = new System.Windows.Forms.Button();
			this._helpLabel = new System.Windows.Forms.Label();
			base.SuspendLayout();
			this._dataFileLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._dataFileLabel.Location = new Point(0, 0);
			this._dataFileLabel.Name = "_dataFileLabel";
			this._dataFileLabel.Size = new Size(463, 16);
			this._dataFileLabel.TabIndex = 10;
			this._dataFileTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._dataFileTextBox.Location = new Point(0, 18);
			this._dataFileTextBox.Name = "_dataFileTextBox";
			this._dataFileTextBox.Size = new Size(463, 20);
			this._dataFileTextBox.TabIndex = 20;
			this._dataFileTextBox.TextChanged += this.OnDataFileTextBoxTextChanged;
			this._selectFileButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this._selectFileButton.Location = new Point(469, 17);
			this._selectFileButton.Name = "_selectFileButton";
			this._selectFileButton.Size = new Size(75, 23);
			this._selectFileButton.TabIndex = 30;
			this._selectFileButton.Click += this.OnSelectFileButtonClick;
			this._helpLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._helpLabel.Location = new Point(0, 44);
			this._helpLabel.Name = "_helpLabel";
			this._helpLabel.Size = new Size(463, 32);
			this._helpLabel.TabIndex = 40;
			base.Controls.Add(this._helpLabel);
			base.Controls.Add(this._selectFileButton);
			base.Controls.Add(this._dataFileTextBox);
			base.Controls.Add(this._dataFileLabel);
			base.Name = "AccessDataSourceConnectionChooserPanel";
			base.Size = new Size(544, 274);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000143A4 File Offset: 0x000125A4
		private void InitializeUI()
		{
			this._dataFileLabel.Text = SR.GetString("AccessDataSourceConnectionChooserPanel_DataFileLabel");
			this._selectFileButton.Text = SR.GetString("AccessDataSourceConnectionChooserPanel_BrowseButton");
			this._helpLabel.Text = SR.GetString("AccessDataSourceConnectionChooserPanel_HelpLabel");
			base.Caption = SR.GetString("AccessDataSourceConnectionChooserPanel_PanelCaption");
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00014400 File Offset: 0x00012600
		protected internal override void OnComplete()
		{
			if (this._accessDataSource.DataFile != this.DataFile)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._accessDataSource)["DataFile"];
				propertyDescriptor.ResetValue(this._accessDataSource);
				propertyDescriptor.SetValue(this._accessDataSource, this.DataFile);
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00014459 File Offset: 0x00012659
		private void OnDataFileTextBoxTextChanged(object sender, EventArgs e)
		{
			this.SetEnabledState();
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00014464 File Offset: 0x00012664
		private void OnSelectFileButtonClick(object sender, EventArgs e)
		{
			string text = UrlBuilder.BuildUrl(this._accessDataSource, this, this.DataFile, SR.GetString("MdbDataFileEditor_Caption"), SR.GetString("MdbDataFileEditor_Filter"));
			if (text != null)
			{
				this.DataFile = text;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000144A4 File Offset: 0x000126A4
		public override bool OnNext()
		{
			string path = UrlPath.MapPath(base.ServiceProvider, this.DataFile);
			if (!File.Exists(path))
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("AccessDataSourceConnectionChooserPanel_FileNotFound", new object[]
				{
					this.DataFile
				}));
				return false;
			}
			return base.OnNext();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000144F7 File Offset: 0x000126F7
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			base.ParentWizard.FinishButton.Enabled = false;
			if (base.Visible)
			{
				this.SetEnabledState();
				return;
			}
			base.ParentWizard.NextButton.Enabled = true;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00014531 File Offset: 0x00012731
		private void SetEnabledState()
		{
			if (base.ParentWizard != null)
			{
				base.ParentWizard.NextButton.Enabled = (this._dataFileTextBox.Text.Length > 0);
			}
		}

		// Token: 0x040001C6 RID: 454
		private System.Windows.Forms.Label _dataFileLabel;

		// Token: 0x040001C7 RID: 455
		private System.Windows.Forms.TextBox _dataFileTextBox;

		// Token: 0x040001C8 RID: 456
		private System.Windows.Forms.Button _selectFileButton;

		// Token: 0x040001C9 RID: 457
		private System.Windows.Forms.Label _helpLabel;

		// Token: 0x040001CA RID: 458
		private AccessDataSource _accessDataSource;

		// Token: 0x040001CB RID: 459
		private AccessDataSourceDesigner _accessDataSourceDesigner;
	}
}
