using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Data;
using System.Data;
using System.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002CE RID: 718
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	internal class DesignBindingPicker : ContainerControl
	{
		// Token: 0x06001C54 RID: 7252 RVA: 0x000AA70C File Offset: 0x000A890C
		public DesignBindingPicker()
		{
			base.SuspendLayout();
			if (!DesignBindingPicker.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					DesignBindingPicker.minimumHeight = DpiHelper.LogicalToDeviceUnitsY(250);
					DesignBindingPicker.minimumWidth = DpiHelper.LogicalToDeviceUnitsX(250);
				}
				DesignBindingPicker.isScalingInitialized = true;
			}
			this.treeViewCtrl = new DesignBindingPicker.BindingPickerTree();
			this.treeViewCtrl.HotTracking = true;
			this.treeViewCtrl.BackColor = SystemColors.Window;
			this.treeViewCtrl.ForeColor = SystemColors.WindowText;
			this.treeViewCtrl.BorderStyle = BorderStyle.None;
			Size size = this.treeViewCtrl.Size;
			this.treeViewCtrl.Dock = DockStyle.Fill;
			this.treeViewCtrl.MouseMove += this.treeViewCtrl_MouseMove;
			this.treeViewCtrl.MouseLeave += this.treeViewCtrl_MouseLeave;
			this.treeViewCtrl.AfterExpand += this.treeViewCtrl_AfterExpand;
			this.treeViewCtrl.AccessibleName = SR.GetString("DesignBindingPickerTreeViewAccessibleName");
			DesignerUtils.ApplyTreeViewThemeStyles(this.treeViewCtrl);
			Label label = new Label();
			label.Height = 1;
			label.BackColor = SystemColors.ControlDark;
			label.Dock = DockStyle.Top;
			this.addNewCtrl = new DesignBindingPicker.BindingPickerLink();
			this.addNewCtrl.Text = SR.GetString("DesignBindingPickerAddProjDataSourceLabel");
			this.addNewCtrl.TextAlign = ContentAlignment.MiddleLeft;
			this.addNewCtrl.BackColor = SystemColors.Window;
			this.addNewCtrl.ForeColor = SystemColors.WindowText;
			this.addNewCtrl.LinkBehavior = LinkBehavior.HoverUnderline;
			int num = this.addNewCtrl.Height;
			int width = this.addNewCtrl.Height;
			this.addNewCtrl.Dock = DockStyle.Fill;
			this.addNewCtrl.LinkClicked += this.addNewCtrl_Click;
			Bitmap bitmap = new Bitmap(BitmapSelector.GetResourceStream(typeof(DesignBindingPicker), "AddNewDataSource.bmp"));
			bitmap.MakeTransparent(Color.Magenta);
			if (DpiHelper.IsScalingRequired)
			{
				DpiHelper.ScaleBitmapLogicalToDevice(ref bitmap, 0);
				num = DpiHelper.LogicalToDeviceUnitsY(this.addNewCtrl.Height);
				width = DpiHelper.LogicalToDeviceUnitsX(this.addNewCtrl.Height);
			}
			PictureBox pictureBox = new PictureBox();
			pictureBox.Image = bitmap;
			pictureBox.BackColor = SystemColors.Window;
			pictureBox.ForeColor = SystemColors.WindowText;
			pictureBox.Width = width;
			pictureBox.Height = num;
			pictureBox.Dock = DockStyle.Left;
			pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			pictureBox.AccessibleRole = AccessibleRole.Graphic;
			this.addNewPanel = new Panel();
			this.addNewPanel.Controls.Add(this.addNewCtrl);
			this.addNewPanel.Controls.Add(pictureBox);
			this.addNewPanel.Controls.Add(label);
			this.addNewPanel.Height = num + 1;
			this.addNewPanel.Dock = DockStyle.Bottom;
			Label label2 = new Label();
			label2.Height = 1;
			label2.BackColor = SystemColors.ControlDark;
			label2.Dock = DockStyle.Top;
			this.helpTextCtrl = new DesignBindingPicker.HelpTextLabel();
			this.helpTextCtrl.TextAlign = ContentAlignment.TopLeft;
			this.helpTextCtrl.BackColor = SystemColors.Window;
			this.helpTextCtrl.ForeColor = SystemColors.WindowText;
			this.helpTextCtrl.Height *= 2;
			int num2 = this.helpTextCtrl.Height;
			if (DpiHelper.IsScalingRequired)
			{
				num2 = DpiHelper.LogicalToDeviceUnitsY(num2);
			}
			this.helpTextCtrl.Dock = DockStyle.Fill;
			this.helpTextPanel = new Panel();
			this.helpTextPanel.Controls.Add(this.helpTextCtrl);
			this.helpTextPanel.Controls.Add(label2);
			this.helpTextPanel.Height = num2 + 1;
			this.helpTextPanel.Dock = DockStyle.Bottom;
			base.Controls.Add(this.treeViewCtrl);
			base.Controls.Add(this.addNewPanel);
			base.Controls.Add(this.helpTextPanel);
			base.ResumeLayout(false);
			base.Size = size;
			this.BackColor = SystemColors.Control;
			base.ActiveControl = this.treeViewCtrl;
			base.AccessibleName = SR.GetString("DesignBindingPickerAccessibleName");
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000AAB34 File Offset: 0x000A8D34
		public DesignBinding Pick(ITypeDescriptorContext context, IServiceProvider provider, bool showDataSources, bool showDataMembers, bool selectListMembers, object rootDataSource, string rootDataMember, DesignBinding initialSelectedItem)
		{
			this.serviceProvider = provider;
			this.edSvc = (IWindowsFormsEditorService)this.serviceProvider.GetService(typeof(IWindowsFormsEditorService));
			this.dspSvc = (DataSourceProviderService)this.serviceProvider.GetService(typeof(DataSourceProviderService));
			this.typeSvc = (ITypeResolutionService)this.serviceProvider.GetService(typeof(ITypeResolutionService));
			this.hostSvc = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
			if (this.edSvc == null)
			{
				return null;
			}
			this.context = context;
			this.showDataSources = showDataSources;
			this.showDataMembers = showDataMembers;
			this.selectListMembers = (!showDataMembers || selectListMembers);
			this.rootDataSource = rootDataSource;
			this.rootDataMember = rootDataMember;
			IUIService iuiservice = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				if (iuiservice.Styles["VsColorPanelHyperLink"] is Color)
				{
					this.addNewCtrl.LinkColor = (Color)iuiservice.Styles["VsColorPanelHyperLink"];
				}
				if (iuiservice.Styles["VsColorPanelHyperLinkPressed"] is Color)
				{
					this.addNewCtrl.ActiveLinkColor = (Color)iuiservice.Styles["VsColorPanelHyperLinkPressed"];
				}
			}
			this.FillTree(initialSelectedItem);
			this.addNewPanel.Visible = (showDataSources && this.dspSvc != null && this.dspSvc.SupportsAddNewDataSource);
			this.helpTextPanel.Visible = showDataSources;
			this.UpdateHelpText(null);
			this.edSvc.DropDownControl(this);
			DesignBinding result = this.selectedItem;
			this.selectedItem = null;
			this.EmptyTree();
			this.serviceProvider = null;
			this.edSvc = null;
			this.dspSvc = null;
			this.hostSvc = null;
			context = null;
			return result;
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000AAD14 File Offset: 0x000A8F14
		private void CloseDropDown()
		{
			if (this.context.Instance is BindingSource && this.hostSvc != null)
			{
				BindingSourceDesigner bindingSourceDesigner = this.hostSvc.GetDesigner(this.context.Instance as IComponent) as BindingSourceDesigner;
				if (bindingSourceDesigner != null)
				{
					bindingSourceDesigner.BindingUpdatedByUser = true;
				}
			}
			if (this.edSvc != null)
			{
				this.edSvc.CloseDropDown();
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x000AAD79 File Offset: 0x000A8F79
		private void EmptyTree()
		{
			this.noneNode = null;
			this.otherNode = null;
			this.projectNode = null;
			this.instancesNode = null;
			this.selectedNode = null;
			this.treeViewCtrl.Nodes.Clear();
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000AADB0 File Offset: 0x000A8FB0
		private void FillTree(DesignBinding initialSelectedItem)
		{
			this.selectedItem = initialSelectedItem;
			this.EmptyTree();
			this.noneNode = new DesignBindingPicker.NoneNode();
			this.otherNode = new DesignBindingPicker.OtherNode();
			this.projectNode = new DesignBindingPicker.ProjectNode(this);
			if (this.hostSvc != null && this.hostSvc.RootComponent != null && this.hostSvc.RootComponent.Site != null)
			{
				this.instancesNode = new DesignBindingPicker.InstancesNode(this.hostSvc.RootComponent.Site.Name);
			}
			else
			{
				this.instancesNode = new DesignBindingPicker.InstancesNode(string.Empty);
			}
			this.treeViewCtrl.Nodes.Add(this.noneNode);
			if (this.showDataSources)
			{
				this.AddFormDataSources();
				this.AddProjectDataSources();
				if (this.projectNode.Nodes.Count > 0)
				{
					this.otherNode.Nodes.Add(this.projectNode);
				}
				if (this.instancesNode.Nodes.Count > 0)
				{
					this.otherNode.Nodes.Add(this.instancesNode);
				}
				if (this.otherNode.Nodes.Count > 0)
				{
					this.treeViewCtrl.Nodes.Add(this.otherNode);
				}
			}
			else
			{
				this.AddDataSourceContents(this.treeViewCtrl.Nodes, this.rootDataSource, this.rootDataMember, null);
			}
			if (this.selectedNode == null)
			{
				this.selectedNode = this.noneNode;
			}
			this.selectedItem = null;
			base.Width = Math.Max(base.Width, this.treeViewCtrl.PreferredWidth + SystemInformation.VerticalScrollBarWidth * 2);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000AAF50 File Offset: 0x000A9150
		private void AddFormDataSources()
		{
			IContainer container = null;
			if (this.context != null)
			{
				container = this.context.Container;
			}
			if (container == null && this.hostSvc != null)
			{
				container = this.hostSvc.Container;
			}
			if (container == null)
			{
				return;
			}
			container = DesignerUtils.CheckForNestedContainer(container);
			ComponentCollection components = container.Components;
			foreach (object obj in components)
			{
				IComponent component = (IComponent)obj;
				if (component != this.context.Instance && (!(component is DataTable) || !this.FindComponent(components, (component as DataTable).DataSet)))
				{
					if (component is BindingSource)
					{
						this.AddDataSource(this.treeViewCtrl.Nodes, component, null);
					}
					else
					{
						this.AddDataSource(this.instancesNode.Nodes, component, null);
					}
				}
			}
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000AB03C File Offset: 0x000A923C
		private void AddDataSource(TreeNodeCollection nodes, IComponent dataSource, string dataMember)
		{
			if (!this.showDataSources)
			{
				return;
			}
			if (!this.IsBindableDataSource(dataSource))
			{
				return;
			}
			string text = null;
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			try
			{
				propertyDescriptorCollection = this.GetItemProperties(dataSource, dataMember);
				if (propertyDescriptorCollection == null)
				{
					return;
				}
			}
			catch (ArgumentException ex)
			{
				text = ex.Message;
			}
			if (this.showDataMembers && propertyDescriptorCollection.Count == 0)
			{
				return;
			}
			DesignBindingPicker.DataSourceNode dataSourceNode = new DesignBindingPicker.DataSourceNode(this, dataSource, dataSource.Site.Name);
			nodes.Add(dataSourceNode);
			if (this.selectedItem != null && this.selectedItem.Equals(dataSource, ""))
			{
				this.selectedNode = dataSourceNode;
			}
			if (text == null)
			{
				this.AddDataSourceContents(dataSourceNode.Nodes, dataSource, dataMember, propertyDescriptorCollection);
				dataSourceNode.SubNodesFilled = true;
				return;
			}
			dataSourceNode.Error = text;
			dataSourceNode.ForeColor = SystemColors.GrayText;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000AB108 File Offset: 0x000A9308
		private void AddDataSourceContents(TreeNodeCollection nodes, object dataSource, string dataMember, PropertyDescriptorCollection properties)
		{
			if (!this.showDataMembers && !(dataSource is BindingSource))
			{
				return;
			}
			if (dataSource is Type)
			{
				try
				{
					dataSource = new BindingSource
					{
						DataSource = dataSource
					}.List;
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
			}
			if (!this.IsBindableDataSource(dataSource))
			{
				return;
			}
			if (properties == null)
			{
				properties = this.GetItemProperties(dataSource, dataMember);
				if (properties == null)
				{
					return;
				}
			}
			for (int i = 0; i < properties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = properties[i];
				if (this.IsBindableDataMember(propertyDescriptor))
				{
					string dataMember2 = string.IsNullOrEmpty(dataMember) ? propertyDescriptor.Name : (dataMember + "." + propertyDescriptor.Name);
					this.AddDataMember(nodes, dataSource, dataMember2, propertyDescriptor.Name, this.IsListMember(propertyDescriptor));
				}
			}
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000AB1E0 File Offset: 0x000A93E0
		private void AddDataMember(TreeNodeCollection nodes, object dataSource, string dataMember, string propertyName, bool isList)
		{
			bool flag = isList && dataSource is BindingSource;
			bool flag2 = this.showDataMembers && !this.selectListMembers;
			bool flag3 = flag && flag2;
			bool flag4 = (flag && !flag2) || this.context.Instance is BindingSource;
			if (flag3)
			{
				return;
			}
			if (this.selectListMembers && !isList)
			{
				return;
			}
			DesignBindingPicker.DataMemberNode dataMemberNode = new DesignBindingPicker.DataMemberNode(this, dataSource, dataMember, propertyName, isList);
			nodes.Add(dataMemberNode);
			if (this.selectedItem != null && this.selectedItem.Equals(dataSource, dataMember) && dataMemberNode != null)
			{
				this.selectedNode = dataMemberNode;
			}
			if (!flag4)
			{
				this.AddDataMemberContents(dataMemberNode);
			}
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000AB28C File Offset: 0x000A948C
		private void AddDataMemberContents(TreeNodeCollection nodes, object dataSource, string dataMember, bool isList)
		{
			if (!isList)
			{
				return;
			}
			PropertyDescriptorCollection itemProperties = this.GetItemProperties(dataSource, dataMember);
			if (itemProperties == null)
			{
				return;
			}
			for (int i = 0; i < itemProperties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = itemProperties[i];
				if (this.IsBindableDataMember(propertyDescriptor))
				{
					bool flag = this.IsListMember(propertyDescriptor);
					if (!this.selectListMembers || flag)
					{
						DesignBindingPicker.DataMemberNode dataMemberNode = new DesignBindingPicker.DataMemberNode(this, dataSource, dataMember + "." + propertyDescriptor.Name, propertyDescriptor.Name, flag);
						nodes.Add(dataMemberNode);
						if (this.selectedItem != null && this.selectedItem.DataSource == dataMemberNode.DataSource)
						{
							if (this.selectedItem.Equals(dataSource, dataMemberNode.DataMember))
							{
								this.selectedNode = dataMemberNode;
							}
							else if (!string.IsNullOrEmpty(this.selectedItem.DataMember) && this.selectedItem.DataMember.IndexOf(dataMemberNode.DataMember) == 0)
							{
								this.AddDataMemberContents(dataMemberNode);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x000AB384 File Offset: 0x000A9584
		private void AddDataMemberContents(TreeNodeCollection nodes, DesignBindingPicker.DataMemberNode dataMemberNode)
		{
			this.AddDataMemberContents(nodes, dataMemberNode.DataSource, dataMemberNode.DataMember, dataMemberNode.IsList);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000AB39F File Offset: 0x000A959F
		private void AddDataMemberContents(DesignBindingPicker.DataMemberNode dataMemberNode)
		{
			this.AddDataMemberContents(dataMemberNode.Nodes, dataMemberNode);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x000AB3B0 File Offset: 0x000A95B0
		private void AddProjectDataSources()
		{
			if (this.dspSvc == null)
			{
				return;
			}
			DataSourceGroupCollection dataSources = this.dspSvc.GetDataSources();
			if (dataSources == null)
			{
				return;
			}
			bool flag = this.selectedItem != null && this.selectedItem.DataSource is DataSourceDescriptor;
			foreach (object obj in dataSources)
			{
				DataSourceGroup dataSourceGroup = (DataSourceGroup)obj;
				if (dataSourceGroup != null)
				{
					if (dataSourceGroup.IsDefault)
					{
						this.AddProjectGroupContents(this.projectNode.Nodes, dataSourceGroup);
					}
					else
					{
						this.AddProjectGroup(this.projectNode.Nodes, dataSourceGroup, flag);
					}
				}
			}
			if (flag)
			{
				this.projectNode.FillSubNodes();
			}
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x000AB478 File Offset: 0x000A9678
		private void AddProjectGroup(TreeNodeCollection nodes, DataSourceGroup group, bool addMembers)
		{
			DesignBindingPicker.ProjectGroupNode projectGroupNode = new DesignBindingPicker.ProjectGroupNode(this, group.Name, group.Image);
			this.AddProjectGroupContents(projectGroupNode.Nodes, group);
			nodes.Add(projectGroupNode);
			if (addMembers)
			{
				projectGroupNode.FillSubNodes();
			}
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x000AB4B8 File Offset: 0x000A96B8
		private void AddProjectGroupContents(TreeNodeCollection nodes, DataSourceGroup group)
		{
			DataSourceDescriptorCollection dataSources = group.DataSources;
			if (dataSources == null)
			{
				return;
			}
			foreach (object obj in dataSources)
			{
				DataSourceDescriptor dataSourceDescriptor = (DataSourceDescriptor)obj;
				if (dataSourceDescriptor != null)
				{
					this.AddProjectDataSource(nodes, dataSourceDescriptor);
				}
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000AB51C File Offset: 0x000A971C
		private void AddProjectDataSource(TreeNodeCollection nodes, DataSourceDescriptor dsd)
		{
			Type type = this.GetType(dsd.TypeName, true, true);
			if (type != null && type.GetType() != DesignBindingPicker.runtimeType)
			{
				return;
			}
			DesignBindingPicker.ProjectDataSourceNode node = new DesignBindingPicker.ProjectDataSourceNode(this, dsd, dsd.Name, dsd.Image);
			nodes.Add(node);
			if (this.selectedItem != null && string.IsNullOrEmpty(this.selectedItem.DataMember))
			{
				if (this.selectedItem.DataSource is DataSourceDescriptor && string.Equals(dsd.Name, (this.selectedItem.DataSource as DataSourceDescriptor).Name, StringComparison.OrdinalIgnoreCase))
				{
					this.selectedNode = node;
					return;
				}
				if (this.selectedItem.DataSource is Type && string.Equals(dsd.TypeName, (this.selectedItem.DataSource as Type).FullName, StringComparison.OrdinalIgnoreCase))
				{
					this.selectedNode = node;
				}
			}
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000AB608 File Offset: 0x000A9808
		private void AddProjectDataSourceContents(TreeNodeCollection nodes, DesignBindingPicker.DataSourceNode projectDataSourceNode)
		{
			DataSourceDescriptor dataSourceDescriptor = projectDataSourceNode.DataSource as DataSourceDescriptor;
			if (dataSourceDescriptor == null)
			{
				return;
			}
			Type type = this.GetType(dataSourceDescriptor.TypeName, false, false);
			if (type == null)
			{
				return;
			}
			object obj = type;
			try
			{
				obj = Activator.CreateInstance(type);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			bool flag = obj is IListSource && (obj as IListSource).ContainsListCollection;
			if (flag && this.context.Instance is BindingSource)
			{
				return;
			}
			PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(obj);
			if (listItemProperties == null)
			{
				return;
			}
			foreach (object obj2 in listItemProperties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
				if (this.IsBindableDataMember(propertyDescriptor) && propertyDescriptor.IsBrowsable)
				{
					bool flag2 = this.IsListMember(propertyDescriptor);
					if ((!this.selectListMembers || flag2) && (flag || !flag2))
					{
						this.AddProjectDataMember(nodes, dataSourceDescriptor, propertyDescriptor, obj, flag2);
					}
				}
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000AB728 File Offset: 0x000A9928
		private void AddProjectDataSourceContents(DesignBindingPicker.DataSourceNode projectDataSourceNode)
		{
			this.AddProjectDataSourceContents(projectDataSourceNode.Nodes, projectDataSourceNode);
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x000AB738 File Offset: 0x000A9938
		private void AddProjectDataMember(TreeNodeCollection nodes, DataSourceDescriptor dsd, PropertyDescriptor pd, object dataSourceInstance, bool isList)
		{
			Type type = this.GetType(dsd.TypeName, true, true);
			if (type != null && type.GetType() != DesignBindingPicker.runtimeType)
			{
				return;
			}
			DesignBindingPicker.DataMemberNode dataMemberNode = new DesignBindingPicker.ProjectDataMemberNode(this, dsd, pd.Name, pd.Name, isList);
			nodes.Add(dataMemberNode);
			this.AddProjectDataMemberContents(dataMemberNode, dsd, pd, dataSourceInstance);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000AB79C File Offset: 0x000A999C
		private void AddProjectDataMemberContents(TreeNodeCollection nodes, DesignBindingPicker.DataMemberNode projectDataMemberNode, DataSourceDescriptor dsd, PropertyDescriptor propDesc, object dataSourceInstance)
		{
			if (this.selectListMembers)
			{
				return;
			}
			if (!projectDataMemberNode.IsList)
			{
				return;
			}
			if (dataSourceInstance == null)
			{
				return;
			}
			PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(dataSourceInstance, new PropertyDescriptor[]
			{
				propDesc
			});
			if (listItemProperties == null)
			{
				return;
			}
			foreach (object obj in listItemProperties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (this.IsBindableDataMember(propertyDescriptor) && propertyDescriptor.IsBrowsable)
				{
					bool flag = this.IsListMember(propertyDescriptor);
					if (!flag)
					{
						this.AddProjectDataMember(nodes, dsd, propertyDescriptor, dataSourceInstance, flag);
					}
				}
			}
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000AB844 File Offset: 0x000A9A44
		private void AddProjectDataMemberContents(DesignBindingPicker.DataMemberNode projectDataMemberNode, DataSourceDescriptor dsd, PropertyDescriptor pd, object dataSourceInstance)
		{
			this.AddProjectDataMemberContents(projectDataMemberNode.Nodes, projectDataMemberNode, dsd, pd, dataSourceInstance);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000AB858 File Offset: 0x000A9A58
		private BindingSource CreateNewBindingSource(object dataSource, string dataMember)
		{
			if (this.hostSvc == null || this.dspSvc == null)
			{
				return null;
			}
			BindingSource bindingSource = new BindingSource();
			try
			{
				bindingSource.DataSource = dataSource;
				bindingSource.DataMember = dataMember;
			}
			catch (Exception ex)
			{
				IUIService uiService = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
				DataGridViewDesigner.ShowErrorDialog(uiService, ex, this);
				return null;
			}
			string text = this.GetBindingSourceNamePrefix(dataSource, dataMember);
			if (this.serviceProvider != null)
			{
				text = ToolStripDesigner.NameFromText(text, bindingSource.GetType(), this.serviceProvider);
			}
			else
			{
				text += bindingSource.GetType().Name;
			}
			string uniqueSiteName = DesignerUtils.GetUniqueSiteName(this.hostSvc, text);
			DesignerTransaction designerTransaction = this.hostSvc.CreateTransaction(SR.GetString("DesignerBatchCreateTool", new object[]
			{
				uniqueSiteName
			}));
			try
			{
				try
				{
					this.hostSvc.Container.Add(bindingSource, uniqueSiteName);
				}
				catch (InvalidOperationException ex2)
				{
					if (designerTransaction != null)
					{
						designerTransaction.Cancel();
					}
					IUIService uiService2 = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
					DataGridViewDesigner.ShowErrorDialog(uiService2, ex2, this);
					return null;
				}
				catch (CheckoutException ex3)
				{
					if (designerTransaction != null)
					{
						designerTransaction.Cancel();
					}
					IUIService uiService3 = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
					DataGridViewDesigner.ShowErrorDialog(uiService3, ex3, this);
					return null;
				}
				this.dspSvc.NotifyDataSourceComponentAdded(bindingSource);
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
				}
			}
			return bindingSource;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000ABA00 File Offset: 0x000A9C00
		private BindingSource CreateNewBindingSource(DataSourceDescriptor dataSourceDescriptor, string dataMember)
		{
			if (this.hostSvc == null || this.dspSvc == null)
			{
				return null;
			}
			object projectDataSourceInstance = this.GetProjectDataSourceInstance(dataSourceDescriptor);
			if (projectDataSourceInstance == null)
			{
				return null;
			}
			return this.CreateNewBindingSource(projectDataSourceInstance, dataMember);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000ABA34 File Offset: 0x000A9C34
		private string GetBindingSourceNamePrefix(object dataSource, string dataMember)
		{
			if (!string.IsNullOrEmpty(dataMember))
			{
				return dataMember;
			}
			if (dataSource == null)
			{
				return "";
			}
			Type type = dataSource as Type;
			if (type != null)
			{
				return type.Name;
			}
			IComponent component = dataSource as IComponent;
			if (component != null)
			{
				ISite site = component.Site;
				if (site != null && !string.IsNullOrEmpty(site.Name))
				{
					return site.Name;
				}
			}
			return dataSource.GetType().Name;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x000ABA9E File Offset: 0x000A9C9E
		private Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (this.typeSvc != null)
			{
				return this.typeSvc.GetType(name, throwOnError, ignoreCase);
			}
			return Type.GetType(name, throwOnError, ignoreCase);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000ABAC0 File Offset: 0x000A9CC0
		private object GetProjectDataSourceInstance(DataSourceDescriptor dataSourceDescriptor)
		{
			Type type = this.GetType(dataSourceDescriptor.TypeName, true, true);
			if (!dataSourceDescriptor.IsDesignable)
			{
				return type;
			}
			foreach (object obj in this.hostSvc.Container.Components)
			{
				IComponent component = (IComponent)obj;
				if (type.Equals(component.GetType()))
				{
					return component;
				}
			}
			object result;
			try
			{
				result = this.dspSvc.AddDataSourceInstance(this.hostSvc, dataSourceDescriptor);
			}
			catch (InvalidOperationException ex)
			{
				IUIService uiService = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
				DataGridViewDesigner.ShowErrorDialog(uiService, ex, this);
				result = null;
			}
			catch (CheckoutException ex2)
			{
				IUIService uiService2 = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
				DataGridViewDesigner.ShowErrorDialog(uiService2, ex2, this);
				result = null;
			}
			return result;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000ABBD4 File Offset: 0x000A9DD4
		private bool FindComponent(ComponentCollection components, IComponent targetComponent)
		{
			foreach (object obj in components)
			{
				IComponent component = (IComponent)obj;
				if (component == targetComponent)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000ABC2C File Offset: 0x000A9E2C
		private bool IsBindableDataSource(object dataSource)
		{
			if (!(dataSource is IListSource) && !(dataSource is IList) && !(dataSource is Array))
			{
				return false;
			}
			ListBindableAttribute listBindableAttribute = (ListBindableAttribute)TypeDescriptor.GetAttributes(dataSource)[typeof(ListBindableAttribute)];
			return listBindableAttribute == null || listBindableAttribute.ListBindable;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000ABC7C File Offset: 0x000A9E7C
		private bool IsBindableDataMember(PropertyDescriptor property)
		{
			if (typeof(byte[]).IsAssignableFrom(property.PropertyType))
			{
				return true;
			}
			ListBindableAttribute listBindableAttribute = (ListBindableAttribute)property.Attributes[typeof(ListBindableAttribute)];
			return listBindableAttribute == null || listBindableAttribute.ListBindable;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000ABCCB File Offset: 0x000A9ECB
		private bool IsListMember(PropertyDescriptor property)
		{
			return !typeof(byte[]).IsAssignableFrom(property.PropertyType) && typeof(IList).IsAssignableFrom(property.PropertyType);
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000ABD00 File Offset: 0x000A9F00
		private PropertyDescriptorCollection GetItemProperties(object dataSource, string dataMember)
		{
			CurrencyManager currencyManager = (CurrencyManager)this.bindingContext[dataSource, dataMember];
			if (currencyManager != null)
			{
				return currencyManager.GetItemProperties();
			}
			return null;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000ABD2C File Offset: 0x000A9F2C
		private void UpdateHelpText(DesignBindingPicker.BindingPickerNode mouseNode)
		{
			if (this.instancesNode == null)
			{
				return;
			}
			string text = (mouseNode == null) ? null : mouseNode.HelpText;
			string text2 = (mouseNode == null) ? null : mouseNode.Error;
			if (text != null || text2 != null)
			{
				this.helpTextCtrl.BackColor = SystemColors.Info;
				this.helpTextCtrl.ForeColor = SystemColors.InfoText;
			}
			else
			{
				this.helpTextCtrl.BackColor = SystemColors.Window;
				this.helpTextCtrl.ForeColor = SystemColors.WindowText;
			}
			if (text2 != null)
			{
				this.helpTextCtrl.Text = text2;
				return;
			}
			if (text != null)
			{
				this.helpTextCtrl.Text = text;
				return;
			}
			if (this.selectedNode != null && this.selectedNode != this.noneNode)
			{
				this.helpTextCtrl.Text = string.Format(CultureInfo.CurrentCulture, SR.GetString("DesignBindingPickerHelpGenCurrentBinding"), new object[]
				{
					this.selectedNode.Text
				});
				return;
			}
			if (!this.showDataSources)
			{
				this.helpTextCtrl.Text = ((this.treeViewCtrl.Nodes.Count > 1) ? SR.GetString("DesignBindingPickerHelpGenPickMember") : "");
				return;
			}
			if (this.treeViewCtrl.Nodes.Count > 1 && this.treeViewCtrl.Nodes[1] is DesignBindingPicker.DataSourceNode)
			{
				this.helpTextCtrl.Text = SR.GetString("DesignBindingPickerHelpGenPickBindSrc");
				return;
			}
			if (this.instancesNode.Nodes.Count > 0 || this.projectNode.Nodes.Count > 0)
			{
				this.helpTextCtrl.Text = SR.GetString("DesignBindingPickerHelpGenPickDataSrc");
				return;
			}
			if (this.addNewPanel.Visible)
			{
				this.helpTextCtrl.Text = SR.GetString("DesignBindingPickerHelpGenAddDataSrc");
				return;
			}
			this.helpTextCtrl.Text = "";
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000ABEF6 File Offset: 0x000AA0F6
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.treeViewCtrl.Focus();
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000ABF0B File Offset: 0x000AA10B
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				this.ShowSelectedNode();
			}
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000ABF22 File Offset: 0x000AA122
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
			{
				width = Math.Max(width, DesignBindingPicker.minimumWidth);
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
			{
				height = Math.Max(height, DesignBindingPicker.minimumHeight);
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000ABF5C File Offset: 0x000AA15C
		private void addNewCtrl_Click(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (this.dspSvc == null || !this.dspSvc.SupportsAddNewDataSource)
			{
				return;
			}
			DataSourceGroup dataSourceGroup = this.dspSvc.InvokeAddNewDataSource(this, FormStartPosition.CenterScreen);
			if (dataSourceGroup == null || dataSourceGroup.DataSources.Count == 0)
			{
				return;
			}
			DataSourceDescriptor dataSource = dataSourceGroup.DataSources[0];
			this.FillTree(new DesignBinding(dataSource, ""));
			if (this.selectedNode == null)
			{
				return;
			}
			int count = this.selectedNode.Nodes.Count;
			if (this.context.Instance is BindingSource)
			{
				this.treeViewCtrl.SetSelectedItem(this.selectedNode);
			}
			if (count == 0 || this.context.Instance is BindingSource)
			{
				this.treeViewCtrl.SetSelectedItem(this.selectedNode);
				return;
			}
			if (count == 1)
			{
				this.treeViewCtrl.SetSelectedItem(this.selectedNode.Nodes[0]);
				return;
			}
			this.ShowSelectedNode();
			this.selectedNode.Expand();
			this.selectedNode = null;
			this.UpdateHelpText(null);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000AC060 File Offset: 0x000AA260
		private void treeViewCtrl_MouseMove(object sender, MouseEventArgs e)
		{
			Point pt = new Point(e.X, e.Y);
			TreeNode treeNode = this.treeViewCtrl.GetNodeAt(pt);
			if (treeNode != null && !treeNode.Bounds.Contains(pt))
			{
				treeNode = null;
			}
			this.UpdateHelpText(treeNode as DesignBindingPicker.BindingPickerNode);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000AC0AF File Offset: 0x000AA2AF
		private void treeViewCtrl_MouseLeave(object sender, EventArgs e)
		{
			this.UpdateHelpText(null);
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000AC0B8 File Offset: 0x000AA2B8
		private void treeViewCtrl_AfterExpand(object sender, TreeViewEventArgs tvcevent)
		{
			if (this.inSelectNode || !base.Visible)
			{
				return;
			}
			(tvcevent.Node as DesignBindingPicker.BindingPickerNode).OnExpand();
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000AC0DB File Offset: 0x000AA2DB
		private void ShowSelectedNode()
		{
			this.PostSelectTreeNode(this.selectedNode);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000AC0EC File Offset: 0x000AA2EC
		private void SelectTreeNode(TreeNode node)
		{
			if (this.inSelectNode)
			{
				return;
			}
			try
			{
				this.inSelectNode = true;
				this.treeViewCtrl.BeginUpdate();
				this.treeViewCtrl.SelectedNode = node;
				this.treeViewCtrl.EndUpdate();
			}
			finally
			{
				this.inSelectNode = false;
			}
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000AC148 File Offset: 0x000AA348
		private void PostSelectTreeNodeCallback(TreeNode node)
		{
			this.SelectTreeNode(null);
			this.SelectTreeNode(node);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x000AC158 File Offset: 0x000AA358
		private void PostSelectTreeNode(TreeNode node)
		{
			if (node != null && base.IsHandleCreated)
			{
				base.BeginInvoke(new DesignBindingPicker.PostSelectTreeNodeDelegate(this.PostSelectTreeNodeCallback), new object[]
				{
					node
				});
			}
		}

		// Token: 0x040016D4 RID: 5844
		private DesignBindingPicker.BindingPickerTree treeViewCtrl;

		// Token: 0x040016D5 RID: 5845
		private DesignBindingPicker.BindingPickerLink addNewCtrl;

		// Token: 0x040016D6 RID: 5846
		private Panel addNewPanel;

		// Token: 0x040016D7 RID: 5847
		private DesignBindingPicker.HelpTextLabel helpTextCtrl;

		// Token: 0x040016D8 RID: 5848
		private Panel helpTextPanel;

		// Token: 0x040016D9 RID: 5849
		private IServiceProvider serviceProvider;

		// Token: 0x040016DA RID: 5850
		private IWindowsFormsEditorService edSvc;

		// Token: 0x040016DB RID: 5851
		private DataSourceProviderService dspSvc;

		// Token: 0x040016DC RID: 5852
		private ITypeResolutionService typeSvc;

		// Token: 0x040016DD RID: 5853
		private IDesignerHost hostSvc;

		// Token: 0x040016DE RID: 5854
		private bool showDataSources;

		// Token: 0x040016DF RID: 5855
		private bool showDataMembers;

		// Token: 0x040016E0 RID: 5856
		private bool selectListMembers;

		// Token: 0x040016E1 RID: 5857
		private object rootDataSource;

		// Token: 0x040016E2 RID: 5858
		private string rootDataMember;

		// Token: 0x040016E3 RID: 5859
		private DesignBinding selectedItem;

		// Token: 0x040016E4 RID: 5860
		private TreeNode selectedNode;

		// Token: 0x040016E5 RID: 5861
		private bool inSelectNode;

		// Token: 0x040016E6 RID: 5862
		private DesignBindingPicker.NoneNode noneNode;

		// Token: 0x040016E7 RID: 5863
		private DesignBindingPicker.OtherNode otherNode;

		// Token: 0x040016E8 RID: 5864
		private DesignBindingPicker.ProjectNode projectNode;

		// Token: 0x040016E9 RID: 5865
		private DesignBindingPicker.InstancesNode instancesNode;

		// Token: 0x040016EA RID: 5866
		private const int minimumDimension = 250;

		// Token: 0x040016EB RID: 5867
		private static int minimumHeight = 250;

		// Token: 0x040016EC RID: 5868
		private static int minimumWidth = 250;

		// Token: 0x040016ED RID: 5869
		private static bool isScalingInitialized = false;

		// Token: 0x040016EE RID: 5870
		private ITypeDescriptorContext context;

		// Token: 0x040016EF RID: 5871
		private BindingContext bindingContext = new BindingContext();

		// Token: 0x040016F0 RID: 5872
		private static Type runtimeType = typeof(object).GetType().GetType();

		// Token: 0x02000559 RID: 1369
		// (Invoke) Token: 0x06003174 RID: 12660
		private delegate void PostSelectTreeNodeDelegate(TreeNode node);

		// Token: 0x0200055A RID: 1370
		internal class HelpTextLabel : Label
		{
			// Token: 0x06003177 RID: 12663 RVA: 0x0010D7FC File Offset: 0x0010B9FC
			protected override void OnPaint(PaintEventArgs e)
			{
				TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak;
				Rectangle bounds = new Rectangle(base.ClientRectangle.Location, base.ClientRectangle.Size);
				bounds.Inflate(-2, -2);
				TextRenderer.DrawText(e.Graphics, this.Text, this.Font, bounds, this.ForeColor, flags);
			}
		}

		// Token: 0x0200055B RID: 1371
		internal class BindingPickerLink : LinkLabel
		{
			// Token: 0x06003179 RID: 12665 RVA: 0x0010D864 File Offset: 0x0010BA64
			protected override bool IsInputKey(Keys key)
			{
				return key == Keys.Return || base.IsInputKey(key);
			}
		}

		// Token: 0x0200055C RID: 1372
		internal class BindingPickerTree : TreeView
		{
			// Token: 0x0600317B RID: 12667 RVA: 0x0010D87C File Offset: 0x0010BA7C
			internal BindingPickerTree()
			{
				Bitmap bitmap = new Bitmap(BitmapSelector.GetResourceStream(typeof(DesignBindingPicker), "DataPickerImages.bmp"));
				ImageList imageList = new ImageList();
				imageList.TransparentColor = Color.Magenta;
				imageList.Images.AddStrip(bitmap);
				if (DpiHelper.IsScalingRequired)
				{
					bitmap.MakeTransparent(Color.Magenta);
					ImageList imageList2 = new ImageList();
					Size size = DpiHelper.LogicalToDeviceUnits(imageList.ImageSize, 0);
					foreach (object obj in imageList.Images)
					{
						Image image = (Image)obj;
						Bitmap value = DpiHelper.CreateResizedBitmap((Bitmap)image, size);
						imageList2.Images.Add(value);
					}
					imageList.Dispose();
					imageList2.ImageSize = size;
					base.ImageList = imageList2;
				}
				else
				{
					base.ImageList = imageList;
				}
				base.ImageList.ColorDepth = ColorDepth.Depth24Bit;
			}

			// Token: 0x17000995 RID: 2453
			// (get) Token: 0x0600317C RID: 12668 RVA: 0x0010D984 File Offset: 0x0010BB84
			internal int PreferredWidth
			{
				get
				{
					return this.GetMaxItemWidth(base.Nodes);
				}
			}

			// Token: 0x0600317D RID: 12669 RVA: 0x0010D994 File Offset: 0x0010BB94
			private int GetMaxItemWidth(TreeNodeCollection nodes)
			{
				int num = 0;
				foreach (object obj in nodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					Rectangle bounds = treeNode.Bounds;
					int val = bounds.Left + bounds.Width;
					num = Math.Max(val, num);
					if (treeNode.IsExpanded)
					{
						num = Math.Max(num, this.GetMaxItemWidth(treeNode.Nodes));
					}
				}
				return num;
			}

			// Token: 0x0600317E RID: 12670 RVA: 0x0010DA24 File Offset: 0x0010BC24
			public void SetSelectedItem(TreeNode node)
			{
				DesignBindingPicker designBindingPicker = base.Parent as DesignBindingPicker;
				if (designBindingPicker == null)
				{
					return;
				}
				DesignBindingPicker.BindingPickerNode bindingPickerNode = node as DesignBindingPicker.BindingPickerNode;
				designBindingPicker.selectedItem = ((bindingPickerNode.CanSelect && bindingPickerNode.Error == null) ? bindingPickerNode.OnSelect() : null);
				if (designBindingPicker.selectedItem != null)
				{
					designBindingPicker.CloseDropDown();
				}
			}

			// Token: 0x0600317F RID: 12671 RVA: 0x0010DA78 File Offset: 0x0010BC78
			protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
			{
				TreeViewHitTestInfo treeViewHitTestInfo = base.HitTest(new Point(e.X, e.Y));
				if (treeViewHitTestInfo.Node == e.Node && (treeViewHitTestInfo.Location == TreeViewHitTestLocations.Image || treeViewHitTestInfo.Location == TreeViewHitTestLocations.Label))
				{
					this.SetSelectedItem(e.Node);
				}
				base.OnNodeMouseClick(e);
			}

			// Token: 0x06003180 RID: 12672 RVA: 0x0010DAD0 File Offset: 0x0010BCD0
			protected override void OnKeyUp(KeyEventArgs e)
			{
				base.OnKeyUp(e);
				if (e.KeyData == Keys.Return && base.SelectedNode != null)
				{
					this.SetSelectedItem(base.SelectedNode);
				}
			}

			// Token: 0x06003181 RID: 12673 RVA: 0x0010DAF7 File Offset: 0x0010BCF7
			protected override bool IsInputKey(Keys key)
			{
				return key == Keys.Return || base.IsInputKey(key);
			}
		}

		// Token: 0x0200055D RID: 1373
		internal class BindingPickerNode : TreeNode
		{
			// Token: 0x06003182 RID: 12674 RVA: 0x0010DB07 File Offset: 0x0010BD07
			public BindingPickerNode(DesignBindingPicker picker, string nodeName) : base(nodeName)
			{
				this.picker = picker;
			}

			// Token: 0x06003183 RID: 12675 RVA: 0x0010DB17 File Offset: 0x0010BD17
			public BindingPickerNode(DesignBindingPicker picker, string nodeName, DesignBindingPicker.BindingPickerNode.BindingImage index) : base(nodeName)
			{
				this.picker = picker;
				this.BindingImageIndex = (int)index;
			}

			// Token: 0x06003184 RID: 12676 RVA: 0x0010DB30 File Offset: 0x0010BD30
			public static DesignBindingPicker.BindingPickerNode.BindingImage BindingImageIndexForDataSource(object dataSource)
			{
				if (dataSource is BindingSource)
				{
					return DesignBindingPicker.BindingPickerNode.BindingImage.BindingSource;
				}
				IListSource listSource = dataSource as IListSource;
				if (listSource != null)
				{
					if (listSource.ContainsListCollection)
					{
						return DesignBindingPicker.BindingPickerNode.BindingImage.DataSource;
					}
					return DesignBindingPicker.BindingPickerNode.BindingImage.ListMember;
				}
				else
				{
					if (dataSource is IList)
					{
						return DesignBindingPicker.BindingPickerNode.BindingImage.ListMember;
					}
					return DesignBindingPicker.BindingPickerNode.BindingImage.FieldMember;
				}
			}

			// Token: 0x06003185 RID: 12677 RVA: 0x0010DB68 File Offset: 0x0010BD68
			public virtual void OnExpand()
			{
				this.FillSubNodes();
			}

			// Token: 0x06003186 RID: 12678 RVA: 0x0010DB70 File Offset: 0x0010BD70
			public virtual void FillSubNodes()
			{
				if (this.SubNodesFilled)
				{
					return;
				}
				foreach (object obj in base.Nodes)
				{
					DesignBindingPicker.BindingPickerNode bindingPickerNode = (DesignBindingPicker.BindingPickerNode)obj;
					bindingPickerNode.Fill();
				}
				this.SubNodesFilled = true;
			}

			// Token: 0x06003187 RID: 12679 RVA: 0x00003937 File Offset: 0x00001B37
			public virtual void Fill()
			{
			}

			// Token: 0x06003188 RID: 12680 RVA: 0x00003598 File Offset: 0x00001798
			public virtual DesignBinding OnSelect()
			{
				return null;
			}

			// Token: 0x17000996 RID: 2454
			// (get) Token: 0x06003189 RID: 12681 RVA: 0x0000445B File Offset: 0x0000265B
			public virtual bool CanSelect
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000997 RID: 2455
			// (get) Token: 0x0600318A RID: 12682 RVA: 0x0010DBD8 File Offset: 0x0010BDD8
			// (set) Token: 0x0600318B RID: 12683 RVA: 0x0010DBE0 File Offset: 0x0010BDE0
			public virtual string Error
			{
				get
				{
					return this.error;
				}
				set
				{
					this.error = value;
				}
			}

			// Token: 0x17000998 RID: 2456
			// (get) Token: 0x0600318C RID: 12684 RVA: 0x00003598 File Offset: 0x00001798
			public virtual string HelpText
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000999 RID: 2457
			// (set) Token: 0x0600318D RID: 12685 RVA: 0x0010DBE9 File Offset: 0x0010BDE9
			public int BindingImageIndex
			{
				set
				{
					base.ImageIndex = value;
					base.SelectedImageIndex = value;
				}
			}

			// Token: 0x1700099A RID: 2458
			// (set) Token: 0x0600318E RID: 12686 RVA: 0x0010DBFC File Offset: 0x0010BDFC
			public Image CustomBindingImage
			{
				set
				{
					try
					{
						ImageList.ImageCollection images = this.picker.treeViewCtrl.ImageList.Images;
						images.Add(value, Color.Transparent);
						this.BindingImageIndex = images.Count - 1;
					}
					catch (Exception)
					{
					}
				}
			}

			// Token: 0x1700099B RID: 2459
			// (get) Token: 0x0600318F RID: 12687 RVA: 0x0010DC50 File Offset: 0x0010BE50
			// (set) Token: 0x06003190 RID: 12688 RVA: 0x0010DC58 File Offset: 0x0010BE58
			public bool SubNodesFilled
			{
				get
				{
					return this.subNodesFilled;
				}
				set
				{
					this.subNodesFilled = true;
				}
			}

			// Token: 0x04002132 RID: 8498
			private string error;

			// Token: 0x04002133 RID: 8499
			private bool subNodesFilled;

			// Token: 0x04002134 RID: 8500
			protected DesignBindingPicker picker;

			// Token: 0x020005ED RID: 1517
			public enum BindingImage
			{
				// Token: 0x04002340 RID: 9024
				None,
				// Token: 0x04002341 RID: 9025
				Other,
				// Token: 0x04002342 RID: 9026
				Project,
				// Token: 0x04002343 RID: 9027
				Instances,
				// Token: 0x04002344 RID: 9028
				BindingSource,
				// Token: 0x04002345 RID: 9029
				ListMember,
				// Token: 0x04002346 RID: 9030
				FieldMember,
				// Token: 0x04002347 RID: 9031
				DataSource
			}
		}

		// Token: 0x0200055E RID: 1374
		internal class DataSourceNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x06003191 RID: 12689 RVA: 0x0010DC61 File Offset: 0x0010BE61
			public DataSourceNode(DesignBindingPicker picker, object dataSource, string nodeName) : base(picker, nodeName)
			{
				this.dataSource = dataSource;
				base.BindingImageIndex = (int)DesignBindingPicker.BindingPickerNode.BindingImageIndexForDataSource(dataSource);
			}

			// Token: 0x1700099C RID: 2460
			// (get) Token: 0x06003192 RID: 12690 RVA: 0x0010DC7E File Offset: 0x0010BE7E
			public object DataSource
			{
				get
				{
					return this.dataSource;
				}
			}

			// Token: 0x06003193 RID: 12691 RVA: 0x0010DC86 File Offset: 0x0010BE86
			public override DesignBinding OnSelect()
			{
				return new DesignBinding(this.DataSource, "");
			}

			// Token: 0x1700099D RID: 2461
			// (get) Token: 0x06003194 RID: 12692 RVA: 0x0010DC98 File Offset: 0x0010BE98
			public override bool CanSelect
			{
				get
				{
					return !this.picker.showDataMembers;
				}
			}

			// Token: 0x1700099E RID: 2462
			// (get) Token: 0x06003195 RID: 12693 RVA: 0x0010DCA8 File Offset: 0x0010BEA8
			public override string HelpText
			{
				get
				{
					string text;
					if (this.DataSource is DataSourceDescriptor)
					{
						text = "Project";
					}
					else if (this.DataSource is BindingSource)
					{
						text = "BindSrc";
					}
					else
					{
						text = "FormInst";
					}
					string text2;
					if (!(this is DesignBindingPicker.DataMemberNode))
					{
						text2 = "DS";
					}
					else if ((this as DesignBindingPicker.DataMemberNode).IsList)
					{
						text2 = "LM";
					}
					else
					{
						text2 = "DM";
					}
					string result;
					try
					{
						string name = string.Format(CultureInfo.CurrentCulture, "DesignBindingPickerHelpNode{0}{1}{2}", new object[]
						{
							text,
							text2,
							this.CanSelect ? "1" : "0"
						});
						result = SR.GetString(name);
					}
					catch
					{
						result = "";
					}
					return result;
				}
			}

			// Token: 0x04002135 RID: 8501
			private object dataSource;
		}

		// Token: 0x0200055F RID: 1375
		internal class DataMemberNode : DesignBindingPicker.DataSourceNode
		{
			// Token: 0x06003196 RID: 12694 RVA: 0x0010DD6C File Offset: 0x0010BF6C
			public DataMemberNode(DesignBindingPicker picker, object dataSource, string dataMember, string dataField, bool isList) : base(picker, dataSource, dataField)
			{
				this.dataMember = dataMember;
				this.isList = isList;
				base.BindingImageIndex = (isList ? 5 : 6);
			}

			// Token: 0x1700099F RID: 2463
			// (get) Token: 0x06003197 RID: 12695 RVA: 0x0010DD95 File Offset: 0x0010BF95
			public string DataMember
			{
				get
				{
					return this.dataMember;
				}
			}

			// Token: 0x170009A0 RID: 2464
			// (get) Token: 0x06003198 RID: 12696 RVA: 0x0010DD9D File Offset: 0x0010BF9D
			public bool IsList
			{
				get
				{
					return this.isList;
				}
			}

			// Token: 0x06003199 RID: 12697 RVA: 0x0010DDA5 File Offset: 0x0010BFA5
			public override void Fill()
			{
				this.picker.AddDataMemberContents(this);
			}

			// Token: 0x0600319A RID: 12698 RVA: 0x0010DDB4 File Offset: 0x0010BFB4
			public override DesignBinding OnSelect()
			{
				if (this.picker.showDataMembers)
				{
					return new DesignBinding(base.DataSource, this.DataMember);
				}
				BindingSource bindingSource = this.picker.CreateNewBindingSource(base.DataSource, this.DataMember);
				if (bindingSource != null)
				{
					return new DesignBinding(bindingSource, "");
				}
				return null;
			}

			// Token: 0x170009A1 RID: 2465
			// (get) Token: 0x0600319B RID: 12699 RVA: 0x0010DE08 File Offset: 0x0010C008
			public override bool CanSelect
			{
				get
				{
					return this.picker.selectListMembers == this.IsList;
				}
			}

			// Token: 0x04002136 RID: 8502
			private bool isList;

			// Token: 0x04002137 RID: 8503
			private string dataMember;
		}

		// Token: 0x02000560 RID: 1376
		internal class NoneNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x0600319C RID: 12700 RVA: 0x0010DE1D File Offset: 0x0010C01D
			public NoneNode() : base(null, SR.GetString("DesignBindingPickerNodeNone"), DesignBindingPicker.BindingPickerNode.BindingImage.None)
			{
			}

			// Token: 0x0600319D RID: 12701 RVA: 0x0010DE31 File Offset: 0x0010C031
			public override DesignBinding OnSelect()
			{
				return DesignBinding.Null;
			}

			// Token: 0x170009A2 RID: 2466
			// (get) Token: 0x0600319E RID: 12702 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool CanSelect
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170009A3 RID: 2467
			// (get) Token: 0x0600319F RID: 12703 RVA: 0x0010DE38 File Offset: 0x0010C038
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeNone");
				}
			}
		}

		// Token: 0x02000561 RID: 1377
		internal class OtherNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060031A0 RID: 12704 RVA: 0x0010DE44 File Offset: 0x0010C044
			public OtherNode() : base(null, SR.GetString("DesignBindingPickerNodeOther"), DesignBindingPicker.BindingPickerNode.BindingImage.Other)
			{
			}

			// Token: 0x170009A4 RID: 2468
			// (get) Token: 0x060031A1 RID: 12705 RVA: 0x0010DE58 File Offset: 0x0010C058
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeOther");
				}
			}
		}

		// Token: 0x02000562 RID: 1378
		internal class InstancesNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060031A2 RID: 12706 RVA: 0x0010DE64 File Offset: 0x0010C064
			public InstancesNode(string rootComponentName) : base(null, string.Format(CultureInfo.CurrentCulture, SR.GetString("DesignBindingPickerNodeInstances"), new object[]
			{
				rootComponentName
			}), DesignBindingPicker.BindingPickerNode.BindingImage.Instances)
			{
			}

			// Token: 0x170009A5 RID: 2469
			// (get) Token: 0x060031A3 RID: 12707 RVA: 0x0010DE8C File Offset: 0x0010C08C
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeInstances");
				}
			}
		}

		// Token: 0x02000563 RID: 1379
		internal class ProjectNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060031A4 RID: 12708 RVA: 0x0010DE98 File Offset: 0x0010C098
			public ProjectNode(DesignBindingPicker picker) : base(picker, SR.GetString("DesignBindingPickerNodeProject"), DesignBindingPicker.BindingPickerNode.BindingImage.Project)
			{
			}

			// Token: 0x170009A6 RID: 2470
			// (get) Token: 0x060031A5 RID: 12709 RVA: 0x0010DEAC File Offset: 0x0010C0AC
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeProject");
				}
			}
		}

		// Token: 0x02000564 RID: 1380
		internal class ProjectGroupNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060031A6 RID: 12710 RVA: 0x0010DEB8 File Offset: 0x0010C0B8
			public ProjectGroupNode(DesignBindingPicker picker, string nodeName, Image image) : base(picker, nodeName, DesignBindingPicker.BindingPickerNode.BindingImage.Project)
			{
				if (image != null)
				{
					base.CustomBindingImage = image;
				}
			}

			// Token: 0x170009A7 RID: 2471
			// (get) Token: 0x060031A7 RID: 12711 RVA: 0x0010DECD File Offset: 0x0010C0CD
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeProjectGroup");
				}
			}
		}

		// Token: 0x02000565 RID: 1381
		internal class ProjectDataSourceNode : DesignBindingPicker.DataSourceNode
		{
			// Token: 0x060031A8 RID: 12712 RVA: 0x0010DED9 File Offset: 0x0010C0D9
			public ProjectDataSourceNode(DesignBindingPicker picker, object dataSource, string nodeName, Image image) : base(picker, dataSource, nodeName)
			{
				if (image != null)
				{
					base.CustomBindingImage = image;
				}
			}

			// Token: 0x060031A9 RID: 12713 RVA: 0x00003937 File Offset: 0x00001B37
			public override void OnExpand()
			{
			}

			// Token: 0x060031AA RID: 12714 RVA: 0x0010DEF0 File Offset: 0x0010C0F0
			public override void Fill()
			{
				this.picker.AddProjectDataSourceContents(this);
			}

			// Token: 0x060031AB RID: 12715 RVA: 0x0010DF00 File Offset: 0x0010C100
			public override DesignBinding OnSelect()
			{
				DataSourceDescriptor dataSourceDescriptor = (DataSourceDescriptor)base.DataSource;
				if (this.picker.context.Instance is BindingSource)
				{
					object projectDataSourceInstance = this.picker.GetProjectDataSourceInstance(dataSourceDescriptor);
					if (projectDataSourceInstance != null)
					{
						return new DesignBinding(projectDataSourceInstance, "");
					}
					return null;
				}
				else
				{
					BindingSource bindingSource = this.picker.CreateNewBindingSource(dataSourceDescriptor, "");
					if (bindingSource != null)
					{
						return new DesignBinding(bindingSource, "");
					}
					return null;
				}
			}
		}

		// Token: 0x02000566 RID: 1382
		internal class ProjectDataMemberNode : DesignBindingPicker.DataMemberNode
		{
			// Token: 0x060031AC RID: 12716 RVA: 0x0010DF70 File Offset: 0x0010C170
			public ProjectDataMemberNode(DesignBindingPicker picker, object dataSource, string dataMember, string dataField, bool isList) : base(picker, dataSource, dataMember, dataField, isList)
			{
			}

			// Token: 0x060031AD RID: 12717 RVA: 0x00003937 File Offset: 0x00001B37
			public override void OnExpand()
			{
			}

			// Token: 0x060031AE RID: 12718 RVA: 0x0010DF80 File Offset: 0x0010C180
			public override DesignBinding OnSelect()
			{
				DesignBindingPicker.ProjectDataMemberNode projectDataMemberNode = base.Parent as DesignBindingPicker.ProjectDataMemberNode;
				string dataMember;
				string dataMember2;
				if (projectDataMemberNode != null)
				{
					dataMember = projectDataMemberNode.DataMember;
					dataMember2 = base.DataMember;
				}
				else if (base.IsList)
				{
					dataMember = base.DataMember;
					dataMember2 = "";
				}
				else
				{
					dataMember = "";
					dataMember2 = base.DataMember;
				}
				DataSourceDescriptor dataSourceDescriptor = (DataSourceDescriptor)base.DataSource;
				BindingSource bindingSource = this.picker.CreateNewBindingSource(dataSourceDescriptor, dataMember);
				if (bindingSource != null)
				{
					return new DesignBinding(bindingSource, dataMember2);
				}
				return null;
			}
		}
	}
}
