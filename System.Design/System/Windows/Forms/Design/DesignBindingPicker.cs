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
	// Token: 0x020001FD RID: 509
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	internal class DesignBindingPicker : ContainerControl
	{
		// Token: 0x06001360 RID: 4960 RVA: 0x00062C34 File Offset: 0x00061C34
		public DesignBindingPicker()
		{
			base.SuspendLayout();
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
			int height = this.addNewCtrl.Height;
			this.addNewCtrl.Dock = DockStyle.Fill;
			this.addNewCtrl.LinkClicked += this.addNewCtrl_Click;
			Bitmap bitmap = new Bitmap(typeof(DesignBindingPicker), "AddNewDataSource.bmp");
			bitmap.MakeTransparent(Color.Magenta);
			PictureBox pictureBox = new PictureBox();
			pictureBox.Image = bitmap;
			pictureBox.BackColor = SystemColors.Window;
			pictureBox.ForeColor = SystemColors.WindowText;
			pictureBox.Width = height;
			pictureBox.Height = height;
			pictureBox.Dock = DockStyle.Left;
			pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			pictureBox.AccessibleRole = AccessibleRole.Graphic;
			this.addNewPanel = new Panel();
			this.addNewPanel.Controls.Add(this.addNewCtrl);
			this.addNewPanel.Controls.Add(pictureBox);
			this.addNewPanel.Controls.Add(label);
			this.addNewPanel.Height = height + 1;
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
			int height2 = this.helpTextCtrl.Height;
			this.helpTextCtrl.Dock = DockStyle.Fill;
			this.helpTextPanel = new Panel();
			this.helpTextPanel.Controls.Add(this.helpTextCtrl);
			this.helpTextPanel.Controls.Add(label2);
			this.helpTextPanel.Height = height2 + 1;
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

		// Token: 0x06001361 RID: 4961 RVA: 0x00062FCC File Offset: 0x00061FCC
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

		// Token: 0x06001362 RID: 4962 RVA: 0x000631AC File Offset: 0x000621AC
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

		// Token: 0x06001363 RID: 4963 RVA: 0x00063211 File Offset: 0x00062211
		private void EmptyTree()
		{
			this.noneNode = null;
			this.otherNode = null;
			this.projectNode = null;
			this.instancesNode = null;
			this.selectedNode = null;
			this.treeViewCtrl.Nodes.Clear();
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00063248 File Offset: 0x00062248
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

		// Token: 0x06001365 RID: 4965 RVA: 0x000633E8 File Offset: 0x000623E8
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

		// Token: 0x06001366 RID: 4966 RVA: 0x000634D4 File Offset: 0x000624D4
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
			if (!this.showDataMembers || propertyDescriptorCollection.Count != 0)
			{
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
				return;
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0006359C File Offset: 0x0006259C
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
				catch
				{
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

		// Token: 0x06001368 RID: 4968 RVA: 0x00063680 File Offset: 0x00062680
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

		// Token: 0x06001369 RID: 4969 RVA: 0x00063730 File Offset: 0x00062730
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

		// Token: 0x0600136A RID: 4970 RVA: 0x00063828 File Offset: 0x00062828
		private void AddDataMemberContents(TreeNodeCollection nodes, DesignBindingPicker.DataMemberNode dataMemberNode)
		{
			this.AddDataMemberContents(nodes, dataMemberNode.DataSource, dataMemberNode.DataMember, dataMemberNode.IsList);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00063843 File Offset: 0x00062843
		private void AddDataMemberContents(DesignBindingPicker.DataMemberNode dataMemberNode)
		{
			this.AddDataMemberContents(dataMemberNode.Nodes, dataMemberNode);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00063854 File Offset: 0x00062854
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

		// Token: 0x0600136D RID: 4973 RVA: 0x0006391C File Offset: 0x0006291C
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

		// Token: 0x0600136E RID: 4974 RVA: 0x0006395C File Offset: 0x0006295C
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

		// Token: 0x0600136F RID: 4975 RVA: 0x000639C0 File Offset: 0x000629C0
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

		// Token: 0x06001370 RID: 4976 RVA: 0x00063AA0 File Offset: 0x00062AA0
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
			catch
			{
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

		// Token: 0x06001371 RID: 4977 RVA: 0x00063BC8 File Offset: 0x00062BC8
		private void AddProjectDataSourceContents(DesignBindingPicker.DataSourceNode projectDataSourceNode)
		{
			this.AddProjectDataSourceContents(projectDataSourceNode.Nodes, projectDataSourceNode);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00063BD8 File Offset: 0x00062BD8
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

		// Token: 0x06001373 RID: 4979 RVA: 0x00063C30 File Offset: 0x00062C30
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

		// Token: 0x06001374 RID: 4980 RVA: 0x00063CDC File Offset: 0x00062CDC
		private void AddProjectDataMemberContents(DesignBindingPicker.DataMemberNode projectDataMemberNode, DataSourceDescriptor dsd, PropertyDescriptor pd, object dataSourceInstance)
		{
			this.AddProjectDataMemberContents(projectDataMemberNode.Nodes, projectDataMemberNode, dsd, pd, dataSourceInstance);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00063CF0 File Offset: 0x00062CF0
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

		// Token: 0x06001376 RID: 4982 RVA: 0x00063EA4 File Offset: 0x00062EA4
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

		// Token: 0x06001377 RID: 4983 RVA: 0x00063ED8 File Offset: 0x00062ED8
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

		// Token: 0x06001378 RID: 4984 RVA: 0x00063F3C File Offset: 0x00062F3C
		private Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (this.typeSvc != null)
			{
				return this.typeSvc.GetType(name, throwOnError, ignoreCase);
			}
			return Type.GetType(name, throwOnError, ignoreCase);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00063F60 File Offset: 0x00062F60
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

		// Token: 0x0600137A RID: 4986 RVA: 0x00064078 File Offset: 0x00063078
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

		// Token: 0x0600137B RID: 4987 RVA: 0x000640D0 File Offset: 0x000630D0
		private bool IsBindableDataSource(object dataSource)
		{
			if (!(dataSource is IListSource) && !(dataSource is IList) && !(dataSource is Array))
			{
				return false;
			}
			ListBindableAttribute listBindableAttribute = (ListBindableAttribute)TypeDescriptor.GetAttributes(dataSource)[typeof(ListBindableAttribute)];
			return listBindableAttribute == null || listBindableAttribute.ListBindable;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00064120 File Offset: 0x00063120
		private bool IsBindableDataMember(PropertyDescriptor property)
		{
			if (typeof(byte[]).IsAssignableFrom(property.PropertyType))
			{
				return true;
			}
			ListBindableAttribute listBindableAttribute = (ListBindableAttribute)property.Attributes[typeof(ListBindableAttribute)];
			return listBindableAttribute == null || listBindableAttribute.ListBindable;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0006416F File Offset: 0x0006316F
		private bool IsListMember(PropertyDescriptor property)
		{
			return !typeof(byte[]).IsAssignableFrom(property.PropertyType) && typeof(IList).IsAssignableFrom(property.PropertyType);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000641A4 File Offset: 0x000631A4
		private PropertyDescriptorCollection GetItemProperties(object dataSource, string dataMember)
		{
			CurrencyManager currencyManager = (CurrencyManager)this.bindingContext[dataSource, dataMember];
			if (currencyManager != null)
			{
				return currencyManager.GetItemProperties();
			}
			return null;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000641D0 File Offset: 0x000631D0
		private void UpdateHelpText(DesignBindingPicker.BindingPickerNode mouseNode)
		{
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

		// Token: 0x06001380 RID: 4992 RVA: 0x00064393 File Offset: 0x00063393
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			this.treeViewCtrl.Focus();
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000643A8 File Offset: 0x000633A8
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				this.ShowSelectedNode();
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000643BF File Offset: 0x000633BF
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
			{
				width = Math.Max(width, 250);
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
			{
				height = Math.Max(height, 250);
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000643F8 File Offset: 0x000633F8
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

		// Token: 0x06001384 RID: 4996 RVA: 0x000644FC File Offset: 0x000634FC
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

		// Token: 0x06001385 RID: 4997 RVA: 0x0006454B File Offset: 0x0006354B
		private void treeViewCtrl_MouseLeave(object sender, EventArgs e)
		{
			this.UpdateHelpText(null);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00064554 File Offset: 0x00063554
		private void treeViewCtrl_AfterExpand(object sender, TreeViewEventArgs tvcevent)
		{
			if (this.inSelectNode || !base.Visible)
			{
				return;
			}
			(tvcevent.Node as DesignBindingPicker.BindingPickerNode).OnExpand();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00064577 File Offset: 0x00063577
		private void ShowSelectedNode()
		{
			this.PostSelectTreeNode(this.selectedNode);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00064588 File Offset: 0x00063588
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

		// Token: 0x06001389 RID: 5001 RVA: 0x000645E4 File Offset: 0x000635E4
		private void PostSelectTreeNodeCallback(TreeNode node)
		{
			this.SelectTreeNode(null);
			this.SelectTreeNode(node);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000645F4 File Offset: 0x000635F4
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

		// Token: 0x040011A2 RID: 4514
		private const int minimumHeight = 250;

		// Token: 0x040011A3 RID: 4515
		private const int minimumWidth = 250;

		// Token: 0x040011A4 RID: 4516
		private DesignBindingPicker.BindingPickerTree treeViewCtrl;

		// Token: 0x040011A5 RID: 4517
		private DesignBindingPicker.BindingPickerLink addNewCtrl;

		// Token: 0x040011A6 RID: 4518
		private Panel addNewPanel;

		// Token: 0x040011A7 RID: 4519
		private DesignBindingPicker.HelpTextLabel helpTextCtrl;

		// Token: 0x040011A8 RID: 4520
		private Panel helpTextPanel;

		// Token: 0x040011A9 RID: 4521
		private IServiceProvider serviceProvider;

		// Token: 0x040011AA RID: 4522
		private IWindowsFormsEditorService edSvc;

		// Token: 0x040011AB RID: 4523
		private DataSourceProviderService dspSvc;

		// Token: 0x040011AC RID: 4524
		private ITypeResolutionService typeSvc;

		// Token: 0x040011AD RID: 4525
		private IDesignerHost hostSvc;

		// Token: 0x040011AE RID: 4526
		private bool showDataSources;

		// Token: 0x040011AF RID: 4527
		private bool showDataMembers;

		// Token: 0x040011B0 RID: 4528
		private bool selectListMembers;

		// Token: 0x040011B1 RID: 4529
		private object rootDataSource;

		// Token: 0x040011B2 RID: 4530
		private string rootDataMember;

		// Token: 0x040011B3 RID: 4531
		private DesignBinding selectedItem;

		// Token: 0x040011B4 RID: 4532
		private TreeNode selectedNode;

		// Token: 0x040011B5 RID: 4533
		private bool inSelectNode;

		// Token: 0x040011B6 RID: 4534
		private DesignBindingPicker.NoneNode noneNode;

		// Token: 0x040011B7 RID: 4535
		private DesignBindingPicker.OtherNode otherNode;

		// Token: 0x040011B8 RID: 4536
		private DesignBindingPicker.ProjectNode projectNode;

		// Token: 0x040011B9 RID: 4537
		private DesignBindingPicker.InstancesNode instancesNode;

		// Token: 0x040011BA RID: 4538
		private ITypeDescriptorContext context;

		// Token: 0x040011BB RID: 4539
		private BindingContext bindingContext = new BindingContext();

		// Token: 0x040011BC RID: 4540
		private static Type runtimeType = typeof(object).GetType().GetType();

		// Token: 0x020001FE RID: 510
		// (Invoke) Token: 0x0600138D RID: 5005
		private delegate void PostSelectTreeNodeDelegate(TreeNode node);

		// Token: 0x020001FF RID: 511
		internal class HelpTextLabel : Label
		{
			// Token: 0x06001390 RID: 5008 RVA: 0x00064648 File Offset: 0x00063648
			protected override void OnPaint(PaintEventArgs e)
			{
				TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak;
				Rectangle bounds = new Rectangle(base.ClientRectangle.Location, base.ClientRectangle.Size);
				bounds.Inflate(-2, -2);
				TextRenderer.DrawText(e.Graphics, this.Text, this.Font, bounds, this.ForeColor, flags);
			}
		}

		// Token: 0x02000200 RID: 512
		internal class BindingPickerLink : LinkLabel
		{
			// Token: 0x06001392 RID: 5010 RVA: 0x000646B0 File Offset: 0x000636B0
			protected override bool IsInputKey(Keys key)
			{
				return key == Keys.Return || base.IsInputKey(key);
			}
		}

		// Token: 0x02000201 RID: 513
		internal class BindingPickerTree : TreeView
		{
			// Token: 0x06001394 RID: 5012 RVA: 0x000646C8 File Offset: 0x000636C8
			internal BindingPickerTree()
			{
				Image value = new Bitmap(typeof(DesignBindingPicker), "DataPickerImages.bmp");
				ImageList imageList = new ImageList();
				imageList.TransparentColor = Color.Magenta;
				imageList.Images.AddStrip(value);
				imageList.ColorDepth = ColorDepth.Depth24Bit;
				base.ImageList = imageList;
			}

			// Token: 0x1700031E RID: 798
			// (get) Token: 0x06001395 RID: 5013 RVA: 0x0006471D File Offset: 0x0006371D
			internal int PreferredWidth
			{
				get
				{
					return this.GetMaxItemWidth(base.Nodes);
				}
			}

			// Token: 0x06001396 RID: 5014 RVA: 0x0006472C File Offset: 0x0006372C
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

			// Token: 0x06001397 RID: 5015 RVA: 0x000647C0 File Offset: 0x000637C0
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

			// Token: 0x06001398 RID: 5016 RVA: 0x00064814 File Offset: 0x00063814
			protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
			{
				TreeViewHitTestInfo treeViewHitTestInfo = base.HitTest(new Point(e.X, e.Y));
				if (treeViewHitTestInfo.Node == e.Node && (treeViewHitTestInfo.Location == TreeViewHitTestLocations.Image || treeViewHitTestInfo.Location == TreeViewHitTestLocations.Label))
				{
					this.SetSelectedItem(e.Node);
				}
				base.OnNodeMouseClick(e);
			}

			// Token: 0x06001399 RID: 5017 RVA: 0x0006486C File Offset: 0x0006386C
			protected override void OnKeyUp(KeyEventArgs e)
			{
				base.OnKeyUp(e);
				if (e.KeyData == Keys.Return && base.SelectedNode != null)
				{
					this.SetSelectedItem(base.SelectedNode);
				}
			}

			// Token: 0x0600139A RID: 5018 RVA: 0x00064893 File Offset: 0x00063893
			protected override bool IsInputKey(Keys key)
			{
				return key == Keys.Return || base.IsInputKey(key);
			}
		}

		// Token: 0x02000202 RID: 514
		internal class BindingPickerNode : TreeNode
		{
			// Token: 0x0600139B RID: 5019 RVA: 0x000648A3 File Offset: 0x000638A3
			public BindingPickerNode(DesignBindingPicker picker, string nodeName) : base(nodeName)
			{
				this.picker = picker;
			}

			// Token: 0x0600139C RID: 5020 RVA: 0x000648B3 File Offset: 0x000638B3
			public BindingPickerNode(DesignBindingPicker picker, string nodeName, DesignBindingPicker.BindingPickerNode.BindingImage index) : base(nodeName)
			{
				this.picker = picker;
				this.BindingImageIndex = (int)index;
			}

			// Token: 0x0600139D RID: 5021 RVA: 0x000648CC File Offset: 0x000638CC
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

			// Token: 0x0600139E RID: 5022 RVA: 0x00064904 File Offset: 0x00063904
			public virtual void OnExpand()
			{
				this.FillSubNodes();
			}

			// Token: 0x0600139F RID: 5023 RVA: 0x0006490C File Offset: 0x0006390C
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

			// Token: 0x060013A0 RID: 5024 RVA: 0x00064974 File Offset: 0x00063974
			public virtual void Fill()
			{
			}

			// Token: 0x060013A1 RID: 5025 RVA: 0x00064976 File Offset: 0x00063976
			public virtual DesignBinding OnSelect()
			{
				return null;
			}

			// Token: 0x1700031F RID: 799
			// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00064979 File Offset: 0x00063979
			public virtual bool CanSelect
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000320 RID: 800
			// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0006497C File Offset: 0x0006397C
			// (set) Token: 0x060013A4 RID: 5028 RVA: 0x00064984 File Offset: 0x00063984
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

			// Token: 0x17000321 RID: 801
			// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0006498D File Offset: 0x0006398D
			public virtual string HelpText
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000322 RID: 802
			// (set) Token: 0x060013A6 RID: 5030 RVA: 0x00064990 File Offset: 0x00063990
			public int BindingImageIndex
			{
				set
				{
					base.ImageIndex = value;
					base.SelectedImageIndex = value;
				}
			}

			// Token: 0x17000323 RID: 803
			// (set) Token: 0x060013A7 RID: 5031 RVA: 0x000649A0 File Offset: 0x000639A0
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
					catch
					{
					}
				}
			}

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00064A04 File Offset: 0x00063A04
			// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00064A0C File Offset: 0x00063A0C
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

			// Token: 0x040011BD RID: 4541
			private string error;

			// Token: 0x040011BE RID: 4542
			private bool subNodesFilled;

			// Token: 0x040011BF RID: 4543
			protected DesignBindingPicker picker;

			// Token: 0x02000203 RID: 515
			public enum BindingImage
			{
				// Token: 0x040011C1 RID: 4545
				None,
				// Token: 0x040011C2 RID: 4546
				Other,
				// Token: 0x040011C3 RID: 4547
				Project,
				// Token: 0x040011C4 RID: 4548
				Instances,
				// Token: 0x040011C5 RID: 4549
				BindingSource,
				// Token: 0x040011C6 RID: 4550
				ListMember,
				// Token: 0x040011C7 RID: 4551
				FieldMember,
				// Token: 0x040011C8 RID: 4552
				DataSource
			}
		}

		// Token: 0x02000204 RID: 516
		internal class DataSourceNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060013AA RID: 5034 RVA: 0x00064A15 File Offset: 0x00063A15
			public DataSourceNode(DesignBindingPicker picker, object dataSource, string nodeName) : base(picker, nodeName)
			{
				this.dataSource = dataSource;
				base.BindingImageIndex = (int)DesignBindingPicker.BindingPickerNode.BindingImageIndexForDataSource(dataSource);
			}

			// Token: 0x17000325 RID: 805
			// (get) Token: 0x060013AB RID: 5035 RVA: 0x00064A32 File Offset: 0x00063A32
			public object DataSource
			{
				get
				{
					return this.dataSource;
				}
			}

			// Token: 0x060013AC RID: 5036 RVA: 0x00064A3A File Offset: 0x00063A3A
			public override DesignBinding OnSelect()
			{
				return new DesignBinding(this.DataSource, "");
			}

			// Token: 0x17000326 RID: 806
			// (get) Token: 0x060013AD RID: 5037 RVA: 0x00064A4C File Offset: 0x00063A4C
			public override bool CanSelect
			{
				get
				{
					return !this.picker.showDataMembers;
				}
			}

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x060013AE RID: 5038 RVA: 0x00064A5C File Offset: 0x00063A5C
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

			// Token: 0x040011C9 RID: 4553
			private object dataSource;
		}

		// Token: 0x02000205 RID: 517
		internal class DataMemberNode : DesignBindingPicker.DataSourceNode
		{
			// Token: 0x060013AF RID: 5039 RVA: 0x00064B24 File Offset: 0x00063B24
			public DataMemberNode(DesignBindingPicker picker, object dataSource, string dataMember, string dataField, bool isList) : base(picker, dataSource, dataField)
			{
				this.dataMember = dataMember;
				this.isList = isList;
				base.BindingImageIndex = (isList ? 5 : 6);
			}

			// Token: 0x17000328 RID: 808
			// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00064B4D File Offset: 0x00063B4D
			public string DataMember
			{
				get
				{
					return this.dataMember;
				}
			}

			// Token: 0x17000329 RID: 809
			// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00064B55 File Offset: 0x00063B55
			public bool IsList
			{
				get
				{
					return this.isList;
				}
			}

			// Token: 0x060013B2 RID: 5042 RVA: 0x00064B5D File Offset: 0x00063B5D
			public override void Fill()
			{
				this.picker.AddDataMemberContents(this);
			}

			// Token: 0x060013B3 RID: 5043 RVA: 0x00064B6C File Offset: 0x00063B6C
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

			// Token: 0x1700032A RID: 810
			// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00064BC0 File Offset: 0x00063BC0
			public override bool CanSelect
			{
				get
				{
					return this.picker.selectListMembers == this.IsList;
				}
			}

			// Token: 0x040011CA RID: 4554
			private bool isList;

			// Token: 0x040011CB RID: 4555
			private string dataMember;
		}

		// Token: 0x02000206 RID: 518
		internal class NoneNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060013B5 RID: 5045 RVA: 0x00064BD5 File Offset: 0x00063BD5
			public NoneNode() : base(null, SR.GetString("DesignBindingPickerNodeNone"), DesignBindingPicker.BindingPickerNode.BindingImage.None)
			{
			}

			// Token: 0x060013B6 RID: 5046 RVA: 0x00064BE9 File Offset: 0x00063BE9
			public override DesignBinding OnSelect()
			{
				return DesignBinding.Null;
			}

			// Token: 0x1700032B RID: 811
			// (get) Token: 0x060013B7 RID: 5047 RVA: 0x00064BF0 File Offset: 0x00063BF0
			public override bool CanSelect
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00064BF3 File Offset: 0x00063BF3
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeNone");
				}
			}
		}

		// Token: 0x02000207 RID: 519
		internal class OtherNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060013B9 RID: 5049 RVA: 0x00064BFF File Offset: 0x00063BFF
			public OtherNode() : base(null, SR.GetString("DesignBindingPickerNodeOther"), DesignBindingPicker.BindingPickerNode.BindingImage.Other)
			{
			}

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x060013BA RID: 5050 RVA: 0x00064C13 File Offset: 0x00063C13
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeOther");
				}
			}
		}

		// Token: 0x02000208 RID: 520
		internal class InstancesNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060013BB RID: 5051 RVA: 0x00064C20 File Offset: 0x00063C20
			public InstancesNode(string rootComponentName) : base(null, string.Format(CultureInfo.CurrentCulture, SR.GetString("DesignBindingPickerNodeInstances"), new object[]
			{
				rootComponentName
			}), DesignBindingPicker.BindingPickerNode.BindingImage.Instances)
			{
			}

			// Token: 0x1700032E RID: 814
			// (get) Token: 0x060013BC RID: 5052 RVA: 0x00064C55 File Offset: 0x00063C55
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeInstances");
				}
			}
		}

		// Token: 0x02000209 RID: 521
		internal class ProjectNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060013BD RID: 5053 RVA: 0x00064C61 File Offset: 0x00063C61
			public ProjectNode(DesignBindingPicker picker) : base(picker, SR.GetString("DesignBindingPickerNodeProject"), DesignBindingPicker.BindingPickerNode.BindingImage.Project)
			{
			}

			// Token: 0x1700032F RID: 815
			// (get) Token: 0x060013BE RID: 5054 RVA: 0x00064C75 File Offset: 0x00063C75
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeProject");
				}
			}
		}

		// Token: 0x0200020A RID: 522
		internal class ProjectGroupNode : DesignBindingPicker.BindingPickerNode
		{
			// Token: 0x060013BF RID: 5055 RVA: 0x00064C81 File Offset: 0x00063C81
			public ProjectGroupNode(DesignBindingPicker picker, string nodeName, Image image) : base(picker, nodeName, DesignBindingPicker.BindingPickerNode.BindingImage.Project)
			{
				if (image != null)
				{
					base.CustomBindingImage = image;
				}
			}

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00064C96 File Offset: 0x00063C96
			public override string HelpText
			{
				get
				{
					return SR.GetString("DesignBindingPickerHelpNodeProjectGroup");
				}
			}
		}

		// Token: 0x0200020B RID: 523
		internal class ProjectDataSourceNode : DesignBindingPicker.DataSourceNode
		{
			// Token: 0x060013C1 RID: 5057 RVA: 0x00064CA2 File Offset: 0x00063CA2
			public ProjectDataSourceNode(DesignBindingPicker picker, object dataSource, string nodeName, Image image) : base(picker, dataSource, nodeName)
			{
				if (image != null)
				{
					base.CustomBindingImage = image;
				}
			}

			// Token: 0x060013C2 RID: 5058 RVA: 0x00064CB9 File Offset: 0x00063CB9
			public override void OnExpand()
			{
			}

			// Token: 0x060013C3 RID: 5059 RVA: 0x00064CBB File Offset: 0x00063CBB
			public override void Fill()
			{
				this.picker.AddProjectDataSourceContents(this);
			}

			// Token: 0x060013C4 RID: 5060 RVA: 0x00064CCC File Offset: 0x00063CCC
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

		// Token: 0x0200020C RID: 524
		internal class ProjectDataMemberNode : DesignBindingPicker.DataMemberNode
		{
			// Token: 0x060013C5 RID: 5061 RVA: 0x00064D3C File Offset: 0x00063D3C
			public ProjectDataMemberNode(DesignBindingPicker picker, object dataSource, string dataMember, string dataField, bool isList) : base(picker, dataSource, dataMember, dataField, isList)
			{
			}

			// Token: 0x060013C6 RID: 5062 RVA: 0x00064D4B File Offset: 0x00063D4B
			public override void OnExpand()
			{
			}

			// Token: 0x060013C7 RID: 5063 RVA: 0x00064D50 File Offset: 0x00063D50
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
