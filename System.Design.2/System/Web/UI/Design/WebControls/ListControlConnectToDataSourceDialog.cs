using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000D7 RID: 215
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed partial class ListControlConnectToDataSourceDialog : TaskForm
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x00027C8C File Offset: 0x00025E8C
		public ListControlConnectToDataSourceDialog(ListControlDesigner controlDesigner) : base(controlDesigner.Component.Site)
		{
			this._controlDesigner = controlDesigner;
			this._originalDataSourceID = controlDesigner.DataSourceID;
			this.SuppressChangedEvents(this._controlDesigner.DataSourceDesigner);
			base.Glyph = BitmapSelector.CreateBitmap(base.GetType(), "datasourcewizard.bmp");
			this.CreatePanel();
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00027CEA File Offset: 0x00025EEA
		private System.Web.UI.WebControls.ListControl Control
		{
			get
			{
				return this._controlDesigner.Component as System.Web.UI.WebControls.ListControl;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00027CFC File Offset: 0x00025EFC
		private string DataSourceID
		{
			set
			{
				this._controlDesigner.DataSourceID = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00027D0A File Offset: 0x00025F0A
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.ListControl.ConnectToDataSource";
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00027D11 File Offset: 0x00025F11
		private IList<IDataSourceDesigner> SuppressedDataSources
		{
			get
			{
				if (this._suppressedDataSources == null)
				{
					this._suppressedDataSources = new List<IDataSourceDesigner>();
				}
				return this._suppressedDataSources;
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00027D2C File Offset: 0x00025F2C
		private void FillDataSourceList()
		{
			this._dataSourceBox.Items.Clear();
			IComponent component = this.GetComponent();
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["DataSourceID"];
			TypeConverter converter = propertyDescriptor.Converter;
			ITypeDescriptorContext context = new ListControlConnectToDataSourceDialog.TypeDescriptorContext(component);
			ICollection standardValues = converter.GetStandardValues(context);
			foreach (object obj in standardValues)
			{
				string item = (string)obj;
				this._dataSourceBox.Items.Add(item);
			}
			string dataSourceID = this.Control.DataSourceID;
			if (dataSourceID.Length <= 0)
			{
				this._dataSourceBox.SelectedIndex = this._dataSourceBox.Items.IndexOf(SR.GetString("DataSourceIDChromeConverter_NoDataSource"));
				return;
			}
			int num = this._dataSourceBox.Items.IndexOf(dataSourceID);
			if (num > -1)
			{
				this._dataSourceBox.SelectedIndex = num;
				return;
			}
			this._dataSourceBox.SelectedIndex = this._dataSourceBox.Items.Add(dataSourceID);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00027E58 File Offset: 0x00026058
		private void FillFieldLists(bool preserveSelection)
		{
			object selectedItem = this._dataTextFieldBox.SelectedItem;
			object selectedItem2 = this._dataValueFieldBox.SelectedItem;
			this._dataTextFieldBox.Items.Clear();
			this._dataTextFieldBox.Text = string.Empty;
			this._dataValueFieldBox.Items.Clear();
			this._dataValueFieldBox.Text = string.Empty;
			IDataSourceFieldSchema[] fieldSchemas = this.GetFieldSchemas();
			if (fieldSchemas != null && fieldSchemas.Length != 0)
			{
				foreach (IDataSourceFieldSchema dataSourceFieldSchema in fieldSchemas)
				{
					this._dataTextFieldBox.Items.Add(dataSourceFieldSchema.Name);
					this._dataValueFieldBox.Items.Add(dataSourceFieldSchema.Name);
				}
				this._dataTextFieldBox.SelectedIndex = 0;
				if (selectedItem != null && preserveSelection)
				{
					if (this._dataTextFieldBox.Items.Contains(selectedItem))
					{
						this._dataTextFieldBox.SelectedItem = selectedItem;
					}
					else
					{
						this._dataTextFieldBox.Items.Insert(0, selectedItem);
					}
				}
				this._dataValueFieldBox.SelectedIndex = 0;
				if (selectedItem2 != null && preserveSelection)
				{
					if (this._dataValueFieldBox.Items.Contains(selectedItem2))
					{
						this._dataValueFieldBox.SelectedItem = selectedItem2;
						return;
					}
					this._dataValueFieldBox.Items.Insert(0, selectedItem2);
				}
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00027FA8 File Offset: 0x000261A8
		private IComponent GetComponent()
		{
			if (this._controlDesigner != null)
			{
				return this._controlDesigner.Component;
			}
			return null;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00027FC0 File Offset: 0x000261C0
		private IDataSourceFieldSchema[] GetFieldSchemas()
		{
			if (this._fieldSchemas == null)
			{
				IDataSourceViewSchema dataSourceViewSchema = null;
				DesignerDataSourceView designerView = this._controlDesigner.DesignerView;
				if (designerView != null)
				{
					try
					{
						dataSourceViewSchema = designerView.Schema;
					}
					catch (Exception ex)
					{
						IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)base.ServiceProvider.GetService(typeof(IComponentDesignerDebugService));
						if (componentDesignerDebugService != null)
						{
							componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
							{
								"DesignerDataSourceView.Schema",
								ex.Message
							}));
						}
					}
				}
				if (dataSourceViewSchema != null)
				{
					this._fieldSchemas = dataSourceViewSchema.GetFields();
				}
			}
			return this._fieldSchemas;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0002805C File Offset: 0x0002625C
		private void CreatePanel()
		{
			base.SuspendLayout();
			this.CreatePanelControls();
			this.InitializePanelControls();
			base.InitializeForm();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00028084 File Offset: 0x00026284
		private void CreatePanelControls()
		{
			this._dataSourceBox = new ComboBox();
			this._dataTextFieldBox = new ComboBox();
			this._dataValueFieldBox = new ComboBox();
			this._refreshSchemaLink = new LinkLabel();
			this._dataSourceLabel = new System.Windows.Forms.Label();
			this._dataTextFieldLabel = new System.Windows.Forms.Label();
			this._dataValueFieldLabel = new System.Windows.Forms.Label();
			this._dataSourceLabel.Location = new Point(0, 0);
			this._dataSourceLabel.Name = "_dataSourceLabel";
			this._dataSourceLabel.Size = new Size(450, 16);
			this._dataSourceLabel.TabIndex = 0;
			this._dataSourceBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this._dataSourceBox.Location = new Point(0, 18);
			this._dataSourceBox.Name = "_dataSourceBox";
			this._dataSourceBox.Size = new Size(192, 21);
			this._dataSourceBox.TabIndex = 1;
			this._dataSourceBox.SelectedIndexChanged += this.OnSelectedDataSourceChanged;
			this._dataTextFieldLabel.Location = new Point(0, 47);
			this._dataTextFieldLabel.Name = "_dataTextFieldLabel";
			this._dataTextFieldLabel.Size = new Size(450, 16);
			this._dataTextFieldLabel.TabIndex = 2;
			this._dataTextFieldBox.DropDownStyle = ComboBoxStyle.DropDown;
			this._dataTextFieldBox.Location = new Point(0, 65);
			this._dataTextFieldBox.Name = "_dataTextFieldBox";
			this._dataTextFieldBox.Size = new Size(192, 21);
			this._dataTextFieldBox.TabIndex = 3;
			this._dataValueFieldLabel.Location = new Point(0, 94);
			this._dataValueFieldLabel.Name = "_dataValueFieldLabel";
			this._dataValueFieldLabel.Size = new Size(450, 16);
			this._dataValueFieldLabel.TabIndex = 4;
			this._dataValueFieldBox.DropDownStyle = ComboBoxStyle.DropDown;
			this._dataValueFieldBox.Location = new Point(0, 112);
			this._dataValueFieldBox.Name = "_dataValueFieldBox";
			this._dataValueFieldBox.Size = new Size(192, 21);
			this._dataValueFieldBox.TabIndex = 5;
			this._refreshSchemaLink.Links.Add(new LinkLabel.Link(0, 150));
			this._refreshSchemaLink.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this._refreshSchemaLink.Location = new Point(0, 254);
			this._refreshSchemaLink.Name = "_refreshSchemaLink";
			this._refreshSchemaLink.Size = new Size(290, 16);
			this._refreshSchemaLink.TabIndex = 6;
			this._refreshSchemaLink.TabStop = true;
			this._refreshSchemaLink.Visible = false;
			this._refreshSchemaLink.LinkClicked += this.OnRefreshSchema;
			base.TaskPanel.Controls.Add(this._dataValueFieldLabel);
			base.TaskPanel.Controls.Add(this._dataTextFieldLabel);
			base.TaskPanel.Controls.Add(this._dataSourceLabel);
			base.TaskPanel.Controls.Add(this._refreshSchemaLink);
			base.TaskPanel.Controls.Add(this._dataValueFieldBox);
			base.TaskPanel.Controls.Add(this._dataTextFieldBox);
			base.TaskPanel.Controls.Add(this._dataSourceBox);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000283F8 File Offset: 0x000265F8
		private void InitializePanelControls()
		{
			string name = this.Control.GetType().Name;
			this._dataSourceLabel.Text = SR.GetString("ListControlCreateDataSource_SelectDataSource");
			this._dataTextFieldLabel.Text = SR.GetString("ListControlCreateDataSource_SelectDataTextField", new object[]
			{
				name
			});
			this._dataValueFieldLabel.Text = SR.GetString("ListControlCreateDataSource_SelectDataValueField", new object[]
			{
				name
			});
			this._refreshSchemaLink.Text = SR.GetString("DataSourceDesigner_RefreshSchemaNoHotkey");
			this.Text = SR.GetString("ListControlCreateDataSource_Title");
			base.AccessibleDescription = SR.GetString("ListControlCreateDataSource_Description", new object[]
			{
				name
			});
			base.CaptionLabel.Text = SR.GetString("ListControlCreateDataSource_Caption");
			this.FillDataSourceList();
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000284C3 File Offset: 0x000266C3
		protected override void OnCancelButtonClick(object sender, EventArgs e)
		{
			this.DataSourceID = this._originalDataSourceID;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000284D1 File Offset: 0x000266D1
		protected override void OnClosed(EventArgs e)
		{
			this.ResumeChangedEvents();
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000284D9 File Offset: 0x000266D9
		protected override void OnOKButtonClick(object sender, EventArgs e)
		{
			this.Control.DataTextField = this._dataTextFieldBox.Text;
			this.Control.DataValueField = this._dataValueFieldBox.Text;
			TypeDescriptor.Refresh(this.GetComponent());
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00028514 File Offset: 0x00026714
		private void OnRefreshSchema(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this._fieldSchemas = null;
			IDataSourceDesigner dataSourceDesigner = this._controlDesigner.DataSourceDesigner;
			if (dataSourceDesigner != null && dataSourceDesigner.CanRefreshSchema)
			{
				dataSourceDesigner.RefreshSchema(false);
				this.FillFieldLists(true);
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00028550 File Offset: 0x00026750
		private void OnSelectedDataSourceChanged(object sender, EventArgs e)
		{
			this._fieldSchemas = null;
			this.DataSourceID = this._dataSourceBox.Text;
			string dataSourceID = this._controlDesigner.DataSourceID;
			if (dataSourceID.Length > 0)
			{
				if (!this._dataSourceBox.Items.Contains(dataSourceID))
				{
					this.FillDataSourceList();
				}
				this._dataSourceBox.SelectedItem = dataSourceID;
				this._dataTextFieldBox.Enabled = true;
				this._dataValueFieldBox.Enabled = true;
				base.OKButton.Enabled = true;
				this._refreshSchemaLink.Visible = false;
				if (this._controlDesigner.DataSourceDesigner != null)
				{
					this.SuppressChangedEvents(this._controlDesigner.DataSourceDesigner);
					this._refreshSchemaLink.Visible = this._controlDesigner.DataSourceDesigner.CanRefreshSchema;
				}
				this.FillFieldLists(false);
				string dataTextField = this.Control.DataTextField;
				if (dataTextField.Length > 0)
				{
					int num = -1;
					for (int i = 0; i < this._dataTextFieldBox.Items.Count; i++)
					{
						if (string.Compare(this._dataTextFieldBox.Items[i].ToString(), dataTextField, StringComparison.OrdinalIgnoreCase) == 0)
						{
							num = i;
							break;
						}
					}
					if (this._dataTextFieldBox.Items.Count > 0)
					{
						if (num >= 0)
						{
							this._dataTextFieldBox.SelectedIndex = num;
						}
					}
					else
					{
						this._dataTextFieldBox.Items.Add(dataTextField);
						this._dataTextFieldBox.SelectedIndex = 0;
					}
				}
				string dataValueField = this.Control.DataValueField;
				if (dataValueField.Length > 0)
				{
					int num2 = -1;
					for (int j = 0; j < this._dataValueFieldBox.Items.Count; j++)
					{
						if (string.Compare(this._dataValueFieldBox.Items[j].ToString(), dataValueField, StringComparison.OrdinalIgnoreCase) == 0)
						{
							num2 = j;
							break;
						}
					}
					if (this._dataValueFieldBox.Items.Count <= 0)
					{
						this._dataValueFieldBox.Items.Add(dataValueField);
						this._dataValueFieldBox.SelectedIndex = 0;
						return;
					}
					if (num2 >= 0)
					{
						this._dataValueFieldBox.SelectedIndex = num2;
						return;
					}
				}
			}
			else
			{
				this._dataTextFieldBox.Items.Clear();
				this._dataValueFieldBox.Items.Clear();
				this._dataTextFieldBox.Text = string.Empty;
				this._dataValueFieldBox.Text = string.Empty;
				this._dataTextFieldBox.Enabled = false;
				this._dataValueFieldBox.Enabled = false;
				base.OKButton.Enabled = !string.Equals(dataSourceID, this._originalDataSourceID, StringComparison.Ordinal);
				this._refreshSchemaLink.Visible = false;
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000287EC File Offset: 0x000269EC
		private void ResumeChangedEvents()
		{
			foreach (IDataSourceDesigner dataSourceDesigner in this.SuppressedDataSources)
			{
				dataSourceDesigner.ResumeDataSourceEvents();
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00028838 File Offset: 0x00026A38
		private void SuppressChangedEvents(IDataSourceDesigner dsd)
		{
			if (dsd != null && !this.SuppressedDataSources.Contains(dsd))
			{
				this.SuppressedDataSources.Add(dsd);
				dsd.SuppressDataSourceEvents();
			}
		}

		// Token: 0x0400045C RID: 1116
		private ListControlDesigner _controlDesigner;

		// Token: 0x0400045D RID: 1117
		private string _originalDataSourceID;

		// Token: 0x0400045E RID: 1118
		private IDataSourceFieldSchema[] _fieldSchemas;

		// Token: 0x0400045F RID: 1119
		private ComboBox _dataSourceBox;

		// Token: 0x04000460 RID: 1120
		private ComboBox _dataTextFieldBox;

		// Token: 0x04000461 RID: 1121
		private System.Windows.Forms.Label _dataSourceLabel;

		// Token: 0x04000462 RID: 1122
		private System.Windows.Forms.Label _dataTextFieldLabel;

		// Token: 0x04000463 RID: 1123
		private System.Windows.Forms.Label _dataValueFieldLabel;

		// Token: 0x04000464 RID: 1124
		private ComboBox _dataValueFieldBox;

		// Token: 0x04000465 RID: 1125
		private LinkLabel _refreshSchemaLink;

		// Token: 0x04000466 RID: 1126
		private IList<IDataSourceDesigner> _suppressedDataSources;

		// Token: 0x02000404 RID: 1028
		private sealed class TypeDescriptorContext : ITypeDescriptorContext, IServiceProvider
		{
			// Token: 0x060027B1 RID: 10161 RVA: 0x000F3FFB File Offset: 0x000F21FB
			public TypeDescriptorContext(IComponent component)
			{
				this._component = component;
			}

			// Token: 0x17000848 RID: 2120
			// (get) Token: 0x060027B2 RID: 10162 RVA: 0x000F400C File Offset: 0x000F220C
			public IContainer Container
			{
				get
				{
					ISite site = this._component.Site;
					if (site != null)
					{
						return site.Container;
					}
					return null;
				}
			}

			// Token: 0x17000849 RID: 2121
			// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000F4030 File Offset: 0x000F2230
			public object Instance
			{
				get
				{
					return this._component;
				}
			}

			// Token: 0x1700084A RID: 2122
			// (get) Token: 0x060027B4 RID: 10164 RVA: 0x00003598 File Offset: 0x00001798
			public PropertyDescriptor PropertyDescriptor
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060027B5 RID: 10165 RVA: 0x000F4038 File Offset: 0x000F2238
			public object GetService(Type serviceType)
			{
				if (this._component.Site == null)
				{
					return null;
				}
				return this._component.Site.GetService(serviceType);
			}

			// Token: 0x060027B6 RID: 10166 RVA: 0x00003B0F File Offset: 0x00001D0F
			public bool OnComponentChanging()
			{
				return true;
			}

			// Token: 0x060027B7 RID: 10167 RVA: 0x00003937 File Offset: 0x00001B37
			public void OnComponentChanged()
			{
			}

			// Token: 0x04001C6B RID: 7275
			private IComponent _component;
		}
	}
}
