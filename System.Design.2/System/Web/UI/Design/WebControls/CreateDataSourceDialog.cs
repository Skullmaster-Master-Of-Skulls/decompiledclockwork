using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000AE RID: 174
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed partial class CreateDataSourceDialog : TaskForm
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x000194AC File Offset: 0x000176AC
		public CreateDataSourceDialog(ControlDesigner controlDesigner, Type dataSourceType, bool configure) : base(controlDesigner.Component.Site)
		{
			this._controlDesigner = controlDesigner;
			this._controlID = ((Control)controlDesigner.Component).ID;
			this._dataSourceType = dataSourceType;
			this._configure = configure;
			this._displayNameComparer = new CreateDataSourceDialog.DisplayNameComparer();
			base.Glyph = BitmapSelector.CreateBitmap(base.GetType(), "datasourcewizard.bmp");
			this.CreatePanel();
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001951C File Offset: 0x0001771C
		public string DataSourceID
		{
			get
			{
				if (this._dataSourceID == null)
				{
					return string.Empty;
				}
				return this._dataSourceID;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00019534 File Offset: 0x00017734
		private TypeDescriptionProvider TypeDescriptionProvider
		{
			get
			{
				Control control = this._controlDesigner.Component as Control;
				if (control != null)
				{
					return TypeDescriptor.GetProvider(control);
				}
				return null;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001955D File Offset: 0x0001775D
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.DataBoundControl.CreateDataSourceDialog";
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00019564 File Offset: 0x00017764
		private string CreateNewDataSource(Type dataSourceType)
		{
			string text = this._idTextBox.Text;
			string result = string.Empty;
			if (dataSourceType != null)
			{
				object obj = Activator.CreateInstance(dataSourceType);
				if (obj != null)
				{
					Control control = obj as Control;
					if (control != null)
					{
						control.ID = text;
						ISite site = this.GetSite();
						if (site != null)
						{
							INameCreationService nameCreationService = (INameCreationService)site.GetService(typeof(INameCreationService));
							if (nameCreationService != null)
							{
								try
								{
									nameCreationService.ValidateName(text);
								}
								catch (Exception ex)
								{
									UIServiceHelper.ShowError(site, SR.GetString("CreateDataSource_NameNotValid", new object[]
									{
										ex.Message
									}));
									this._idTextBox.Focus();
									return result;
								}
								IContainer container = site.Container;
								if (container == null)
								{
									goto IL_F1;
								}
								ComponentCollection components = container.Components;
								if (components != null && components[text] != null)
								{
									UIServiceHelper.ShowError(site, SR.GetString("CreateDataSource_NameNotUnique"));
									this._idTextBox.Focus();
									return result;
								}
							}
							IL_F1:
							IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
							if (designerHost != null)
							{
								IComponent rootComponent = designerHost.RootComponent;
								if (rootComponent != null)
								{
									WebFormsRootDesigner webFormsRootDesigner = designerHost.GetDesigner(rootComponent) as WebFormsRootDesigner;
									if (webFormsRootDesigner != null)
									{
										Control referenceControl = this.GetComponent() as Control;
										result = webFormsRootDesigner.AddControlToDocument(control, referenceControl, ControlLocation.After);
										IDesigner designer = designerHost.GetDesigner(control);
										IDataSourceDesigner dataSourceDesigner = designer as IDataSourceDesigner;
										if (dataSourceDesigner != null)
										{
											if (dataSourceDesigner.CanConfigure && this._configure)
											{
												dataSourceDesigner.Configure();
											}
										}
										else
										{
											IHierarchicalDataSourceDesigner hierarchicalDataSourceDesigner = designer as IHierarchicalDataSourceDesigner;
											if (hierarchicalDataSourceDesigner != null && hierarchicalDataSourceDesigner.CanConfigure && this._configure)
											{
												hierarchicalDataSourceDesigner.Configure();
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00019728 File Offset: 0x00017928
		private void CreatePanel()
		{
			base.SuspendLayout();
			this.CreatePanelControls();
			this.InitializePanelControls();
			base.InitializeForm();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00019750 File Offset: 0x00017950
		private void CreatePanelControls()
		{
			this._selectLabel = new System.Windows.Forms.Label();
			this._dataSourceTypesListView = new ListView();
			this._descriptionBox = new System.Windows.Forms.TextBox();
			this._idLabel = new System.Windows.Forms.Label();
			this._idTextBox = new System.Windows.Forms.TextBox();
			this._selectLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._selectLabel.Location = new Point(0, 0);
			this._selectLabel.Name = "_selectLabel";
			this._selectLabel.Size = new Size(544, 16);
			this._selectLabel.TabIndex = 0;
			this._dataSourceTypesListView.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._dataSourceTypesListView.Location = new Point(0, 18);
			this._dataSourceTypesListView.Name = "_dataSourceTypesListView";
			this._dataSourceTypesListView.Size = new Size(544, 90);
			this._dataSourceTypesListView.TabIndex = 1;
			this._dataSourceTypesListView.SelectedIndexChanged += this.OnDataSourceTypeChosen;
			this._dataSourceTypesListView.Alignment = ListViewAlignment.Left;
			this._dataSourceTypesListView.LabelWrap = true;
			this._dataSourceTypesListView.MultiSelect = false;
			this._dataSourceTypesListView.HideSelection = false;
			this._dataSourceTypesListView.ListViewItemSorter = this._displayNameComparer;
			this._dataSourceTypesListView.Sorting = SortOrder.Ascending;
			this._dataSourceTypesListView.MouseDoubleClick += this.OnListViewDoubleClick;
			this._descriptionBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._descriptionBox.Location = new Point(0, 112);
			this._descriptionBox.Name = "_descriptionBox";
			this._descriptionBox.Size = new Size(544, 55);
			this._descriptionBox.TabIndex = 2;
			this._descriptionBox.ReadOnly = true;
			this._descriptionBox.Multiline = true;
			this._descriptionBox.TabStop = false;
			this._descriptionBox.BackColor = SystemColors.Control;
			this._descriptionBox.Multiline = true;
			this._idLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._idLabel.Location = new Point(0, 176);
			this._idLabel.Name = "_idLabel";
			this._idLabel.Size = new Size(544, 16);
			this._idLabel.TabIndex = 3;
			this._idTextBox.Location = new Point(0, 194);
			this._idTextBox.Name = "_idTextBox";
			this._idTextBox.Size = new Size(220, 20);
			this._idTextBox.TabIndex = 4;
			this._idTextBox.TextChanged += this.OnIDChanged;
			base.TaskPanel.Controls.Add(this._idTextBox);
			base.TaskPanel.Controls.Add(this._idLabel);
			base.TaskPanel.Controls.Add(this._descriptionBox);
			base.TaskPanel.Controls.Add(this._dataSourceTypesListView);
			base.TaskPanel.Controls.Add(this._selectLabel);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00019A6B File Offset: 0x00017C6B
		private IComponent GetComponent()
		{
			if (this._controlDesigner != null)
			{
				return this._controlDesigner.Component;
			}
			return null;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00019A84 File Offset: 0x00017C84
		private string GetNewDataSourceName(Type dataSourceType)
		{
			if (dataSourceType != null)
			{
				ISite site = this.GetSite();
				if (site != null)
				{
					INameCreationService nameCreationService = (INameCreationService)site.GetService(typeof(INameCreationService));
					if (nameCreationService != null)
					{
						return nameCreationService.CreateName(site.Container, dataSourceType);
					}
					return site.Name + "_DataSource";
				}
			}
			return string.Empty;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00019AE4 File Offset: 0x00017CE4
		private ISite GetSite()
		{
			IComponent component = this.GetComponent();
			if (component != null)
			{
				return component.Site;
			}
			return null;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00019B04 File Offset: 0x00017D04
		private bool IsTargetFramework45OrAbove()
		{
			TypeDescriptionProvider typeDescriptionProvider = this.TypeDescriptionProvider;
			return typeDescriptionProvider != null && typeDescriptionProvider.IsSupportedType(typeof(HtmlVideo));
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00019B2D File Offset: 0x00017D2D
		private bool IsSupportedInTargetFramework(Type type)
		{
			return !(type == typeof(AccessDataSource)) || !this.IsTargetFramework45OrAbove();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00019B4C File Offset: 0x00017D4C
		private void InitializePanelControls()
		{
			this._selectLabel.Text = SR.GetString("CreateDataSource_SelectType");
			this._idLabel.Text = SR.GetString("CreateDataSource_ID");
			base.OKButton.Enabled = false;
			this.Text = SR.GetString("CreateDataSource_Title");
			this._descriptionBox.Text = SR.GetString("CreateDataSource_SelectTypeDesc");
			base.AccessibleDescription = SR.GetString("CreateDataSource_Description");
			base.CaptionLabel.Text = SR.GetString("CreateDataSource_Caption");
			this.UpdateFonts();
			ISite site = this.GetSite();
			if (site != null)
			{
				IComponentDiscoveryService componentDiscoveryService = (IComponentDiscoveryService)site.GetService(typeof(IComponentDiscoveryService));
				IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
				if (componentDiscoveryService != null)
				{
					ICollection componentTypes = componentDiscoveryService.GetComponentTypes(designerHost, this._dataSourceType);
					if (componentTypes != null)
					{
						ImageList imageList = new ImageList();
						imageList.ColorDepth = ColorDepth.Depth32Bit;
						bool flag = this.IsTargetFramework45OrAbove();
						Type[] array = new Type[componentTypes.Count];
						componentTypes.CopyTo(array, 0);
						foreach (Type type in array)
						{
							if (this.IsSupportedInTargetFramework(type))
							{
								AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
								Bitmap bitmap = null;
								if (attributes != null)
								{
									ToolboxBitmapAttribute toolboxBitmapAttribute = attributes[typeof(ToolboxBitmapAttribute)] as ToolboxBitmapAttribute;
									if (toolboxBitmapAttribute != null && !toolboxBitmapAttribute.Equals(ToolboxBitmapAttribute.Default))
									{
										bitmap = (toolboxBitmapAttribute.GetImage(type, true) as Bitmap);
									}
								}
								if (bitmap == null)
								{
									bitmap = BitmapSelector.CreateBitmap(base.GetType(), "CustomDataSource.bmp");
								}
								imageList.ImageSize = new Size(32, 32);
								imageList.Images.Add(type.FullName, bitmap);
								this._dataSourceTypesListView.Items.Add(new CreateDataSourceDialog.DataSourceListViewItem(type));
							}
						}
						this._dataSourceTypesListView.Sort();
						this._dataSourceTypesListView.LargeImageList = imageList;
					}
				}
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00019D48 File Offset: 0x00017F48
		protected override void OnClosing(CancelEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK && this._dataSourceTypesListView.SelectedItems.Count > 0)
			{
				CreateDataSourceDialog.DataSourceListViewItem dataSourceListViewItem = this._dataSourceTypesListView.SelectedItems[0] as CreateDataSourceDialog.DataSourceListViewItem;
				Type dataSourceType = dataSourceListViewItem.DataSourceType;
				string text = this.CreateNewDataSource(dataSourceType);
				if (text.Length > 0)
				{
					this._dataSourceID = text;
				}
				else
				{
					e.Cancel = true;
				}
				TypeDescriptor.Refresh(this.GetComponent());
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00019DBC File Offset: 0x00017FBC
		private void OnDataSourceTypeChosen(object sender, EventArgs e)
		{
			if (this._dataSourceTypesListView.SelectedItems.Count > 0)
			{
				CreateDataSourceDialog.DataSourceListViewItem dataSourceListViewItem = this._dataSourceTypesListView.SelectedItems[0] as CreateDataSourceDialog.DataSourceListViewItem;
				Type dataSourceType = dataSourceListViewItem.DataSourceType;
				this._idTextBox.Text = this.GetNewDataSourceName(dataSourceType);
				this._descriptionBox.Text = dataSourceListViewItem.GetDescriptionText();
			}
			this.UpdateOKButtonEnabled();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00019E23 File Offset: 0x00018023
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFonts();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00019E32 File Offset: 0x00018032
		private void OnIDChanged(object sender, EventArgs e)
		{
			this.UpdateOKButtonEnabled();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00019E3A File Offset: 0x0001803A
		private void OnListViewDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00019E56 File Offset: 0x00018056
		private void UpdateFonts()
		{
			this._selectLabel.Font = new Font(this.Font, FontStyle.Bold);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00019E70 File Offset: 0x00018070
		private void UpdateOKButtonEnabled()
		{
			if (this._idTextBox.Text.Length > 0 && this._dataSourceTypesListView.SelectedItems.Count > 0)
			{
				base.OKButton.Enabled = true;
				return;
			}
			base.OKButton.Enabled = false;
		}

		// Token: 0x0400028D RID: 653
		private ControlDesigner _controlDesigner;

		// Token: 0x0400028E RID: 654
		private string _controlID;

		// Token: 0x0400028F RID: 655
		private Type _dataSourceType;

		// Token: 0x04000290 RID: 656
		private CreateDataSourceDialog.DisplayNameComparer _displayNameComparer;

		// Token: 0x04000291 RID: 657
		private string _dataSourceID;

		// Token: 0x04000292 RID: 658
		private bool _configure;

		// Token: 0x04000293 RID: 659
		private System.Windows.Forms.Label _selectLabel;

		// Token: 0x04000294 RID: 660
		private ListView _dataSourceTypesListView;

		// Token: 0x04000295 RID: 661
		private System.Windows.Forms.TextBox _descriptionBox;

		// Token: 0x04000296 RID: 662
		private System.Windows.Forms.Label _idLabel;

		// Token: 0x04000297 RID: 663
		private System.Windows.Forms.TextBox _idTextBox;

		// Token: 0x020003DB RID: 987
		private class DataSourceListViewItem : ListViewItem
		{
			// Token: 0x0600271E RID: 10014 RVA: 0x000F12AB File Offset: 0x000EF4AB
			public DataSourceListViewItem(Type dataSourceType)
			{
				this._dataSourceType = dataSourceType;
				base.Text = this.GetDisplayName();
				base.ImageKey = this._dataSourceType.FullName;
			}

			// Token: 0x1700083B RID: 2107
			// (get) Token: 0x0600271F RID: 10015 RVA: 0x000F12D7 File Offset: 0x000EF4D7
			public Type DataSourceType
			{
				get
				{
					return this._dataSourceType;
				}
			}

			// Token: 0x06002720 RID: 10016 RVA: 0x000F12E0 File Offset: 0x000EF4E0
			public string GetDescriptionText()
			{
				AttributeCollection attributes = TypeDescriptor.GetAttributes(this._dataSourceType);
				if (attributes != null)
				{
					DescriptionAttribute descriptionAttribute = attributes[typeof(DescriptionAttribute)] as DescriptionAttribute;
					if (descriptionAttribute != null)
					{
						return descriptionAttribute.Description;
					}
				}
				return string.Empty;
			}

			// Token: 0x06002721 RID: 10017 RVA: 0x000F1324 File Offset: 0x000EF524
			public string GetDisplayName()
			{
				if (this._displayName == null)
				{
					AttributeCollection attributes = TypeDescriptor.GetAttributes(this._dataSourceType);
					this._displayName = string.Empty;
					if (attributes != null)
					{
						DisplayNameAttribute displayNameAttribute = attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
						if (displayNameAttribute != null)
						{
							this._displayName = displayNameAttribute.DisplayName;
						}
					}
					if (string.IsNullOrEmpty(this._displayName))
					{
						this._displayName = this._dataSourceType.Name;
					}
				}
				return this._displayName;
			}

			// Token: 0x04001C22 RID: 7202
			private Type _dataSourceType;

			// Token: 0x04001C23 RID: 7203
			private string _displayName;
		}

		// Token: 0x020003DC RID: 988
		private class DisplayNameComparer : IComparer
		{
			// Token: 0x06002722 RID: 10018 RVA: 0x000F139C File Offset: 0x000EF59C
			public int Compare(object x, object y)
			{
				if (!(x is CreateDataSourceDialog.DataSourceListViewItem) || !(y is CreateDataSourceDialog.DataSourceListViewItem))
				{
					return 0;
				}
				return this.Compare((CreateDataSourceDialog.DataSourceListViewItem)x, (CreateDataSourceDialog.DataSourceListViewItem)y);
			}

			// Token: 0x06002723 RID: 10019 RVA: 0x000F13C4 File Offset: 0x000EF5C4
			private int Compare(CreateDataSourceDialog.DataSourceListViewItem x, CreateDataSourceDialog.DataSourceListViewItem y)
			{
				StringComparer stringComparer = StringComparer.Create(CultureInfo.CurrentCulture, true);
				return stringComparer.Compare(x.GetDisplayName(), y.GetDisplayName());
			}
		}
	}
}
