using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000350 RID: 848
	internal class ToolStripContainerDesigner : ParentControlDesigner
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x000CC583 File Offset: 0x000CA783
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x000CC59A File Offset: 0x000CA79A
		private bool TopToolStripPanelVisible
		{
			get
			{
				return (bool)base.ShadowProperties["TopToolStripPanelVisible"];
			}
			set
			{
				base.ShadowProperties["TopToolStripPanelVisible"] = value;
				((ToolStripContainer)base.Component).TopToolStripPanelVisible = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000CC5C3 File Offset: 0x000CA7C3
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x000CC5DA File Offset: 0x000CA7DA
		private bool LeftToolStripPanelVisible
		{
			get
			{
				return (bool)base.ShadowProperties["LeftToolStripPanelVisible"];
			}
			set
			{
				base.ShadowProperties["LeftToolStripPanelVisible"] = value;
				((ToolStripContainer)base.Component).LeftToolStripPanelVisible = value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000CC603 File Offset: 0x000CA803
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x000CC61A File Offset: 0x000CA81A
		private bool RightToolStripPanelVisible
		{
			get
			{
				return (bool)base.ShadowProperties["RightToolStripPanelVisible"];
			}
			set
			{
				base.ShadowProperties["RightToolStripPanelVisible"] = value;
				((ToolStripContainer)base.Component).RightToolStripPanelVisible = value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x000CC643 File Offset: 0x000CA843
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x000CC65A File Offset: 0x000CA85A
		private bool BottomToolStripPanelVisible
		{
			get
			{
				return (bool)base.ShadowProperties["BottomToolStripPanelVisible"];
			}
			set
			{
				base.ShadowProperties["BottomToolStripPanelVisible"] = value;
				((ToolStripContainer)base.Component).BottomToolStripPanelVisible = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x000CC684 File Offset: 0x000CA884
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				return new DesignerActionListCollection
				{
					new ToolStripContainerActionList(this.toolStripContainer)
					{
						AutoShow = true
					}
				};
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool AllowControlLasso
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x000CC6B3 File Offset: 0x000CA8B3
		protected override bool DrawGrid
		{
			get
			{
				return !this.disableDrawGrid && base.DrawGrid;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x000CC6C8 File Offset: 0x000CA8C8
		public override IList SnapLines
		{
			get
			{
				return base.SnapLinesInternal() as ArrayList;
			}
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000CC6E2 File Offset: 0x000CA8E2
		public override int NumberOfInternalControlDesigners()
		{
			return this.panels.Length;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000CC6EC File Offset: 0x000CA8EC
		public override ControlDesigner InternalControlDesigner(int internalControlIndex)
		{
			if (internalControlIndex < this.panels.Length && internalControlIndex >= 0)
			{
				Control component = this.panels[internalControlIndex];
				return this.designerHost.GetDesigner(component) as ControlDesigner;
			}
			return null;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x000CC724 File Offset: 0x000CA924
		public override ICollection AssociatedComponents
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.toolStripContainer.Controls)
				{
					Control control = (Control)obj;
					foreach (object obj2 in control.Controls)
					{
						Control value = (Control)obj2;
						arrayList.Add(value);
					}
				}
				return arrayList;
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000CC7D4 File Offset: 0x000CA9D4
		protected override IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			if (tool != null)
			{
				Type type = tool.GetType(this.designerHost);
				if (typeof(StatusStrip).IsAssignableFrom(type))
				{
					ParentControlDesigner.InvokeCreateTool(this.GetDesigner(this.bottomToolStripPanel), tool);
				}
				else if (typeof(ToolStrip).IsAssignableFrom(type))
				{
					ParentControlDesigner.InvokeCreateTool(this.GetDesigner(this.topToolStripPanel), tool);
				}
				else
				{
					ParentControlDesigner.InvokeCreateTool(this.GetDesigner(this.contentToolStripPanel), tool);
				}
			}
			return null;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool CanParent(Control control)
		{
			return false;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000CC850 File Offset: 0x000CAA50
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.selectionSvc != null)
			{
				this.selectionSvc = null;
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000CC868 File Offset: 0x000CAA68
		private ToolStripPanelDesigner GetDesigner(ToolStripPanel panel)
		{
			return this.designerHost.GetDesigner(panel) as ToolStripPanelDesigner;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000CC87B File Offset: 0x000CAA7B
		private PanelDesigner GetDesigner(ToolStripContentPanel panel)
		{
			return this.designerHost.GetDesigner(panel) as PanelDesigner;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000CC890 File Offset: 0x000CAA90
		private ToolStripContainer ContainerParent(Control c)
		{
			ToolStripContainer result = null;
			if (c != null && !(c is ToolStripContainer))
			{
				while (c.Parent != null)
				{
					if (c.Parent is ToolStripContainer)
					{
						result = (c.Parent as ToolStripContainer);
						break;
					}
					c = c.Parent;
				}
			}
			return result;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000CC8D8 File Offset: 0x000CAAD8
		protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
		{
			SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
			if (selectionManager != null)
			{
				for (int i = 0; i <= 4; i++)
				{
					Control control = this.panels[i];
					Rectangle bounds = base.BehaviorService.ControlRectInAdornerWindow(control);
					ControlDesigner controlDesigner = this.InternalControlDesigner(i);
					this.OnSetCursor();
					if (controlDesigner != null)
					{
						ControlBodyGlyph value = new ControlBodyGlyph(bounds, Cursor.Current, control, controlDesigner);
						selectionManager.BodyGlyphAdorner.Glyphs.Add(value);
						bool flag = true;
						ICollection selectedComponents = this.selectionSvc.GetSelectedComponents();
						if (!this.selectionSvc.GetComponentSelected(this.toolStripContainer))
						{
							foreach (object obj in selectedComponents)
							{
								ToolStripContainer toolStripContainer = this.ContainerParent(obj as Control);
								flag = (toolStripContainer == this.toolStripContainer);
							}
						}
						if (flag)
						{
							ToolStripPanelDesigner toolStripPanelDesigner = controlDesigner as ToolStripPanelDesigner;
							if (toolStripPanelDesigner != null)
							{
								this.AddPanelSelectionGlyph(toolStripPanelDesigner, selectionManager);
							}
						}
					}
				}
			}
			return base.GetControlGlyph(selectionType);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000CCA0C File Offset: 0x000CAC0C
		private Control GetAssociatedControl(Component c)
		{
			if (c is Control)
			{
				return c as Control;
			}
			if (c is ToolStripItem)
			{
				ToolStripItem toolStripItem = c as ToolStripItem;
				Control control = toolStripItem.GetCurrentParent();
				if (control == null)
				{
					control = toolStripItem.Owner;
				}
				return control;
			}
			return null;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000CCA4C File Offset: 0x000CAC4C
		private bool CheckDropDownBounds(ToolStripDropDownItem dropDownItem, Glyph childGlyph, GlyphCollection glyphs)
		{
			if (dropDownItem != null)
			{
				Rectangle bounds = childGlyph.Bounds;
				Rectangle rect = base.BehaviorService.ControlRectInAdornerWindow(dropDownItem.DropDown);
				if (!bounds.IntersectsWith(rect))
				{
					glyphs.Insert(0, childGlyph);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000CCA8C File Offset: 0x000CAC8C
		private bool CheckAssociatedControl(Component c, Glyph childGlyph, GlyphCollection glyphs)
		{
			bool flag = false;
			ToolStripDropDownItem toolStripDropDownItem = c as ToolStripDropDownItem;
			if (toolStripDropDownItem != null)
			{
				flag = this.CheckDropDownBounds(toolStripDropDownItem, childGlyph, glyphs);
			}
			if (!flag)
			{
				Control associatedControl = this.GetAssociatedControl(c);
				if (associatedControl != null && associatedControl != this.toolStripContainer && !UnsafeNativeMethods.IsChild(new HandleRef(this.toolStripContainer, this.toolStripContainer.Handle), new HandleRef(associatedControl, associatedControl.Handle)))
				{
					Rectangle bounds = childGlyph.Bounds;
					Rectangle rect = base.BehaviorService.ControlRectInAdornerWindow(associatedControl);
					if (c == this.designerHost.RootComponent || !bounds.IntersectsWith(rect))
					{
						glyphs.Insert(0, childGlyph);
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000CCB28 File Offset: 0x000CAD28
		protected override Control GetParentForComponent(IComponent component)
		{
			Type type = component.GetType();
			if (typeof(StatusStrip).IsAssignableFrom(type))
			{
				return this.bottomToolStripPanel;
			}
			if (typeof(ToolStrip).IsAssignableFrom(type))
			{
				return this.topToolStripPanel;
			}
			return this.contentToolStripPanel;
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000CCB74 File Offset: 0x000CAD74
		public override void Initialize(IComponent component)
		{
			this.toolStripContainer = (ToolStripContainer)component;
			base.Initialize(component);
			base.AutoResizeHandles = true;
			this.topToolStripPanel = this.toolStripContainer.TopToolStripPanel;
			this.bottomToolStripPanel = this.toolStripContainer.BottomToolStripPanel;
			this.leftToolStripPanel = this.toolStripContainer.LeftToolStripPanel;
			this.rightToolStripPanel = this.toolStripContainer.RightToolStripPanel;
			this.contentToolStripPanel = this.toolStripContainer.ContentPanel;
			this.panels = new Control[]
			{
				this.contentToolStripPanel,
				this.leftToolStripPanel,
				this.rightToolStripPanel,
				this.topToolStripPanel,
				this.bottomToolStripPanel
			};
			ToolboxBitmapAttribute toolboxBitmapAttribute = new ToolboxBitmapAttribute(typeof(ToolStripPanel), "ToolStripContainer_BottomToolStripPanel.bmp");
			ToolboxBitmapAttribute toolboxBitmapAttribute2 = new ToolboxBitmapAttribute(typeof(ToolStripPanel), "ToolStripContainer_RightToolStripPanel.bmp");
			ToolboxBitmapAttribute toolboxBitmapAttribute3 = new ToolboxBitmapAttribute(typeof(ToolStripPanel), "ToolStripContainer_TopToolStripPanel.bmp");
			ToolboxBitmapAttribute toolboxBitmapAttribute4 = new ToolboxBitmapAttribute(typeof(ToolStripPanel), "ToolStripContainer_LeftToolStripPanel.bmp");
			TypeDescriptor.AddAttributes(this.bottomToolStripPanel, new Attribute[]
			{
				toolboxBitmapAttribute,
				new DescriptionAttribute("bottom")
			});
			TypeDescriptor.AddAttributes(this.rightToolStripPanel, new Attribute[]
			{
				toolboxBitmapAttribute2,
				new DescriptionAttribute("right")
			});
			TypeDescriptor.AddAttributes(this.leftToolStripPanel, new Attribute[]
			{
				toolboxBitmapAttribute4,
				new DescriptionAttribute("left")
			});
			TypeDescriptor.AddAttributes(this.topToolStripPanel, new Attribute[]
			{
				toolboxBitmapAttribute3,
				new DescriptionAttribute("top")
			});
			base.EnableDesignMode(this.topToolStripPanel, "TopToolStripPanel");
			base.EnableDesignMode(this.bottomToolStripPanel, "BottomToolStripPanel");
			base.EnableDesignMode(this.leftToolStripPanel, "LeftToolStripPanel");
			base.EnableDesignMode(this.rightToolStripPanel, "RightToolStripPanel");
			base.EnableDesignMode(this.contentToolStripPanel, "ContentPanel");
			this.designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (this.selectionSvc == null)
			{
				this.selectionSvc = (ISelectionService)this.GetService(typeof(ISelectionService));
			}
			if (this.topToolStripPanel != null)
			{
				ToolStripPanelDesigner toolStripPanelDesigner = this.designerHost.GetDesigner(this.topToolStripPanel) as ToolStripPanelDesigner;
				toolStripPanelDesigner.ExpandTopPanel();
			}
			this.TopToolStripPanelVisible = this.toolStripContainer.TopToolStripPanelVisible;
			this.LeftToolStripPanelVisible = this.toolStripContainer.LeftToolStripPanelVisible;
			this.RightToolStripPanelVisible = this.toolStripContainer.RightToolStripPanelVisible;
			this.BottomToolStripPanelVisible = this.toolStripContainer.BottomToolStripPanelVisible;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000CCE0C File Offset: 0x000CB00C
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000CCE18 File Offset: 0x000CB018
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			try
			{
				this.disableDrawGrid = true;
				base.OnPaintAdornments(pe);
			}
			finally
			{
				this.disableDrawGrid = false;
			}
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000CCE50 File Offset: 0x000CB050
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"TopToolStripPanelVisible",
				"LeftToolStripPanelVisible",
				"RightToolStripPanelVisible",
				"BottomToolStripPanelVisible"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(ToolStripContainerDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000CCECC File Offset: 0x000CB0CC
		private void AddPanelSelectionGlyph(ToolStripPanelDesigner designer, SelectionManager selMgr)
		{
			if (designer != null)
			{
				Glyph glyph = designer.GetGlyph();
				if (glyph != null)
				{
					ICollection selectedComponents = this.selectionSvc.GetSelectedComponents();
					foreach (object obj in selectedComponents)
					{
						Component component = obj as Component;
						if (component != null && !this.CheckAssociatedControl(component, glyph, selMgr.BodyGlyphAdorner.Glyphs))
						{
							selMgr.BodyGlyphAdorner.Glyphs.Insert(0, glyph);
						}
					}
				}
			}
		}

		// Token: 0x04001940 RID: 6464
		private ToolStripPanel topToolStripPanel;

		// Token: 0x04001941 RID: 6465
		private ToolStripPanel bottomToolStripPanel;

		// Token: 0x04001942 RID: 6466
		private ToolStripPanel leftToolStripPanel;

		// Token: 0x04001943 RID: 6467
		private ToolStripPanel rightToolStripPanel;

		// Token: 0x04001944 RID: 6468
		private ToolStripContentPanel contentToolStripPanel;

		// Token: 0x04001945 RID: 6469
		private Control[] panels;

		// Token: 0x04001946 RID: 6470
		private const string topToolStripPanelName = "TopToolStripPanel";

		// Token: 0x04001947 RID: 6471
		private const string bottomToolStripPanelName = "BottomToolStripPanel";

		// Token: 0x04001948 RID: 6472
		private const string leftToolStripPanelName = "LeftToolStripPanel";

		// Token: 0x04001949 RID: 6473
		private const string rightToolStripPanelName = "RightToolStripPanel";

		// Token: 0x0400194A RID: 6474
		private const string contentToolStripPanelName = "ContentPanel";

		// Token: 0x0400194B RID: 6475
		private IDesignerHost designerHost;

		// Token: 0x0400194C RID: 6476
		private ISelectionService selectionSvc;

		// Token: 0x0400194D RID: 6477
		private ToolStripContainer toolStripContainer;

		// Token: 0x0400194E RID: 6478
		private bool disableDrawGrid;
	}
}
