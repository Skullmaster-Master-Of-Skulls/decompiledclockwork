using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200034F RID: 847
	internal class ToolStripContainerActionList : DesignerActionList
	{
		// Token: 0x06002173 RID: 8563 RVA: 0x000CBDB4 File Offset: 0x000C9FB4
		public ToolStripContainerActionList(ToolStripContainer control) : base(control)
		{
			this.container = control;
			this.provider = this.container.Site;
			this.host = (this.provider.GetService(typeof(IDesignerHost)) as IDesignerHost);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000CBE00 File Offset: 0x000CA000
		private object GetProperty(Component comp, string propertyName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(comp)[propertyName];
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.GetValue(comp);
			}
			return null;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000CBE28 File Offset: 0x000CA028
		private void ChangeProperty(Component comp, string propertyName, object value)
		{
			if (this.host != null)
			{
				ToolStripPanel toolStripPanel = comp as ToolStripPanel;
				ToolStripPanelDesigner toolStripPanelDesigner = this.host.GetDesigner(comp) as ToolStripPanelDesigner;
				if (propertyName.Equals("Visible"))
				{
					foreach (object obj in toolStripPanel.Controls)
					{
						Control component = (Control)obj;
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Visible"];
						if (propertyDescriptor != null)
						{
							propertyDescriptor.SetValue(component, value);
						}
					}
					if (!(bool)value)
					{
						if (toolStripPanel != null)
						{
							toolStripPanel.Padding = new Padding(0);
						}
						if (toolStripPanelDesigner != null && toolStripPanelDesigner.ToolStripPanelSelectorGlyph != null)
						{
							toolStripPanelDesigner.ToolStripPanelSelectorGlyph.IsExpanded = false;
						}
					}
				}
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(comp)[propertyName];
				if (propertyDescriptor2 != null)
				{
					propertyDescriptor2.SetValue(comp, value);
				}
				SelectionManager selectionManager = (SelectionManager)this.provider.GetService(typeof(SelectionManager));
				if (selectionManager != null)
				{
					selectionManager.Refresh();
				}
				if (toolStripPanelDesigner != null)
				{
					toolStripPanelDesigner.InvalidateGlyph();
				}
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000CBF4C File Offset: 0x000CA14C
		private bool IsDockFilled
		{
			get
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.container)["Dock"];
				return propertyDescriptor == null || (DockStyle)propertyDescriptor.GetValue(this.container) == DockStyle.Fill;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000CBF8C File Offset: 0x000CA18C
		private bool ProvideReparent
		{
			get
			{
				if (this.host != null)
				{
					Control control = this.host.RootComponent as Control;
					if (control != null && this.container.Parent == control && this.IsDockFilled && control.Controls.Count > 1)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000CBFDC File Offset: 0x000CA1DC
		public void SetDockToForm()
		{
			if (this.host != null)
			{
				Control control = this.host.RootComponent as Control;
				if (control != null && this.container.Parent != control)
				{
					control.Controls.Add(this.container);
				}
				if (!this.IsDockFilled)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.container)["Dock"];
					if (propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(this.container, DockStyle.Fill);
					}
				}
			}
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000CC058 File Offset: 0x000CA258
		public void ReparentControls()
		{
			if (this.host != null)
			{
				Control control = this.host.RootComponent as Control;
				if (control != null && this.container.Parent == control && control.Controls.Count > 1)
				{
					Control control2 = this.container.ContentPanel;
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control2)["AutoScroll"];
					if (propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(control2, true);
					}
					DesignerTransaction designerTransaction = this.host.CreateTransaction("Reparent Transaction");
					try
					{
						Control[] array = new Control[control.Controls.Count];
						control.Controls.CopyTo(array, 0);
						foreach (Control control3 in array)
						{
							if (control3 != this.container && !(control3 is MdiClient))
							{
								InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(control3)[typeof(InheritanceAttribute)];
								if (inheritanceAttribute != null && inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly)
								{
									IComponentChangeService componentChangeService = this.provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
									if (control3 is ToolStrip)
									{
										control2 = this.GetParent(control3);
									}
									else
									{
										control2 = this.container.ContentPanel;
									}
									PropertyDescriptor member = TypeDescriptor.GetProperties(control2)["Controls"];
									Control parent = control3.Parent;
									if (parent != null)
									{
										if (componentChangeService != null)
										{
											componentChangeService.OnComponentChanging(parent, member);
										}
										parent.Controls.Remove(control3);
									}
									if (componentChangeService != null)
									{
										componentChangeService.OnComponentChanging(control2, member);
									}
									control2.Controls.Add(control3);
									if (componentChangeService != null && parent != null)
									{
										componentChangeService.OnComponentChanged(parent, member, null, null);
									}
									if (componentChangeService != null)
									{
										componentChangeService.OnComponentChanged(control2, member, null, null);
									}
								}
							}
						}
					}
					catch
					{
						if (designerTransaction != null)
						{
							designerTransaction.Cancel();
							designerTransaction = null;
						}
					}
					finally
					{
						if (designerTransaction != null)
						{
							designerTransaction.Commit();
							designerTransaction = null;
						}
						ISelectionService selectionService = this.provider.GetService(typeof(ISelectionService)) as ISelectionService;
						if (selectionService != null)
						{
							selectionService.SetSelectedComponents(new IComponent[]
							{
								control2
							});
						}
					}
				}
			}
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000CC2B0 File Offset: 0x000CA4B0
		private Control GetParent(Control c)
		{
			Control result = this.container.ContentPanel;
			DockStyle dock = c.Dock;
			foreach (object obj in this.container.Controls)
			{
				Control control = (Control)obj;
				if (control is ToolStripPanel && control.Dock == dock)
				{
					result = control;
					break;
				}
			}
			return result;
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x000CC334 File Offset: 0x000CA534
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x000CC34C File Offset: 0x000CA54C
		public bool TopVisible
		{
			get
			{
				return (bool)this.GetProperty(this.container, "TopToolStripPanelVisible");
			}
			set
			{
				if (value != this.TopVisible)
				{
					this.ChangeProperty(this.container, "TopToolStripPanelVisible", value);
				}
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x000CC36E File Offset: 0x000CA56E
		// (set) Token: 0x0600217E RID: 8574 RVA: 0x000CC386 File Offset: 0x000CA586
		public bool BottomVisible
		{
			get
			{
				return (bool)this.GetProperty(this.container, "BottomToolStripPanelVisible");
			}
			set
			{
				if (value != this.BottomVisible)
				{
					this.ChangeProperty(this.container, "BottomToolStripPanelVisible", value);
				}
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x000CC3A8 File Offset: 0x000CA5A8
		// (set) Token: 0x06002180 RID: 8576 RVA: 0x000CC3C0 File Offset: 0x000CA5C0
		public bool LeftVisible
		{
			get
			{
				return (bool)this.GetProperty(this.container, "LeftToolStripPanelVisible");
			}
			set
			{
				if (value != this.LeftVisible)
				{
					this.ChangeProperty(this.container, "LeftToolStripPanelVisible", value);
				}
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x000CC3E2 File Offset: 0x000CA5E2
		// (set) Token: 0x06002182 RID: 8578 RVA: 0x000CC3FA File Offset: 0x000CA5FA
		public bool RightVisible
		{
			get
			{
				return (bool)this.GetProperty(this.container, "RightToolStripPanelVisible");
			}
			set
			{
				if (value != this.RightVisible)
				{
					this.ChangeProperty(this.container, "RightToolStripPanelVisible", value);
				}
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000CC41C File Offset: 0x000CA61C
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			designerActionItemCollection.Add(new DesignerActionHeaderItem(SR.GetString("ToolStripContainerActionList_Visible"), SR.GetString("ToolStripContainerActionList_Show")));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("TopVisible", SR.GetString("ToolStripContainerActionList_Top"), SR.GetString("ToolStripContainerActionList_Show"), SR.GetString("ToolStripContainerActionList_TopDesc")));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("BottomVisible", SR.GetString("ToolStripContainerActionList_Bottom"), SR.GetString("ToolStripContainerActionList_Show"), SR.GetString("ToolStripContainerActionList_BottomDesc")));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("LeftVisible", SR.GetString("ToolStripContainerActionList_Left"), SR.GetString("ToolStripContainerActionList_Show"), SR.GetString("ToolStripContainerActionList_LeftDesc")));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("RightVisible", SR.GetString("ToolStripContainerActionList_Right"), SR.GetString("ToolStripContainerActionList_Show"), SR.GetString("ToolStripContainerActionList_RightDesc")));
			if (!this.IsDockFilled)
			{
				bool flag = true;
				if (this.host != null)
				{
					Control control = this.host.RootComponent as UserControl;
					if (control != null)
					{
						flag = false;
					}
				}
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "SetDockToForm", flag ? SR.GetString("DesignerShortcutDockInForm") : SR.GetString("DesignerShortcutDockInUserControl")));
			}
			if (this.ProvideReparent)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ReparentControls", SR.GetString("DesignerShortcutReparentControls")));
			}
			return designerActionItemCollection;
		}

		// Token: 0x0400193D RID: 6461
		private ToolStripContainer container;

		// Token: 0x0400193E RID: 6462
		private IServiceProvider provider;

		// Token: 0x0400193F RID: 6463
		private IDesignerHost host;
	}
}
