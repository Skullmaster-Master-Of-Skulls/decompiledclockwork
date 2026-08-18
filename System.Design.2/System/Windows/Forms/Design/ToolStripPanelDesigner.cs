using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000361 RID: 865
	internal class ToolStripPanelDesigner : ScrollableControlDesigner
	{
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x000DCF94 File Offset: 0x000DB194
		private Pen BorderPen
		{
			get
			{
				Color color = ((double)this.Control.BackColor.GetBrightness() < 0.5) ? ControlPaint.Light(this.Control.BackColor) : ControlPaint.Dark(this.Control.BackColor);
				return new Pen(color)
				{
					DashStyle = DashStyle.Dash
				};
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x000DCFF4 File Offset: 0x000DB1F4
		private ContextMenuStrip DesignerContextMenu
		{
			get
			{
				if (this.contextMenu == null)
				{
					this.contextMenu = new BaseContextMenuStrip(base.Component.Site, base.Component as Component);
					this.contextMenu.GroupOrdering.Clear();
					this.contextMenu.GroupOrdering.AddRange(new string[]
					{
						"Code",
						"Verbs",
						"Custom",
						"Selection",
						"Edit",
						"Properties"
					});
					this.contextMenu.Text = "CustomContextMenu";
				}
				return this.contextMenu;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x000DD099 File Offset: 0x000DB299
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				if (this.panel != null && this.panel.Parent is ToolStripContainer && base.InheritanceAttribute == InheritanceAttribute.Inherited)
				{
					return InheritanceAttribute.InheritedReadOnly;
				}
				return base.InheritanceAttribute;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x000DD0CE File Offset: 0x000DB2CE
		// (set) Token: 0x0600235D RID: 9053 RVA: 0x000DD0E5 File Offset: 0x000DB2E5
		private Padding Padding
		{
			get
			{
				return (Padding)base.ShadowProperties["Padding"];
			}
			set
			{
				base.ShadowProperties["Padding"] = value;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool ParticipatesWithSnapLines
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x000DD100 File Offset: 0x000DB300
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules result = base.SelectionRules;
				if (this.panel != null && this.panel.Parent is ToolStripContainer)
				{
					result = SelectionRules.Locked;
				}
				return result;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x000DD135 File Offset: 0x000DB335
		public ToolStripPanelSelectionGlyph ToolStripPanelSelectorGlyph
		{
			get
			{
				return this.containerSelectorGlyph;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x0009F679 File Offset: 0x0009D879
		// (set) Token: 0x06002362 RID: 9058 RVA: 0x000DD13D File Offset: 0x000DB33D
		private bool Visible
		{
			get
			{
				return (bool)base.ShadowProperties["Visible"];
			}
			set
			{
				base.ShadowProperties["Visible"] = value;
				this.panel.Visible = value;
			}
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000DD161 File Offset: 0x000DB361
		public override bool CanParent(Control control)
		{
			return control is ToolStrip;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000DD16C File Offset: 0x000DB36C
		public override bool CanBeParentedTo(IDesigner parentDesigner)
		{
			return this.panel != null && !(this.panel.Parent is ToolStripContainer);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000DD18E File Offset: 0x000DB38E
		private void ComponentChangeSvc_ComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (this.containerSelectorGlyph != null)
			{
				this.containerSelectorGlyph.UpdateGlyph();
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000DD1A4 File Offset: 0x000DB3A4
		protected override IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			if (tool != null)
			{
				Type type = tool.GetType(this.designerHost);
				if (!typeof(ToolStrip).IsAssignableFrom(type))
				{
					ToolStripContainer toolStripContainer = this.panel.Parent as ToolStripContainer;
					if (toolStripContainer != null)
					{
						ToolStripContentPanel contentPanel = toolStripContainer.ContentPanel;
						if (contentPanel != null)
						{
							PanelDesigner panelDesigner = this.designerHost.GetDesigner(contentPanel) as PanelDesigner;
							if (panelDesigner != null)
							{
								ParentControlDesigner.InvokeCreateTool(panelDesigner, tool);
							}
						}
					}
				}
				else
				{
					base.CreateToolCore(tool, x, y, width, height, hasLocation, hasSize);
				}
			}
			return null;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000DD224 File Offset: 0x000DB424
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.selectionSvc != null && this.selectionSvc.PrimarySelection == null)
				{
					this.OnSelectionChanging(this.selectionSvc, EventArgs.Empty);
				}
				base.Dispose(disposing);
			}
			finally
			{
				if (disposing && this.contextMenu != null)
				{
					this.contextMenu.Dispose();
				}
				if (this.selectionSvc != null)
				{
					this.selectionSvc.SelectionChanging -= this.OnSelectionChanging;
					this.selectionSvc.SelectionChanged -= this.OnSelectionChanged;
					this.selectionSvc = null;
				}
				if (this.componentChangeSvc != null)
				{
					this.componentChangeSvc.ComponentChanged -= this.ComponentChangeSvc_ComponentChanged;
				}
				this.panel.ControlAdded -= this.OnControlAdded;
				this.panel.ControlRemoved -= this.OnControlRemoved;
			}
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000DD314 File Offset: 0x000DB514
		private void DrawBorder(Graphics graphics)
		{
			Pen borderPen = this.BorderPen;
			Rectangle clientRectangle = this.Control.ClientRectangle;
			int num = clientRectangle.Width;
			clientRectangle.Width = num - 1;
			num = clientRectangle.Height;
			clientRectangle.Height = num - 1;
			graphics.DrawRectangle(borderPen, clientRectangle);
			borderPen.Dispose();
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000DD364 File Offset: 0x000DB564
		internal void ExpandTopPanel()
		{
			if (this.containerSelectorGlyph == null)
			{
				this.behavior = new ToolStripPanelSelectionBehavior(this.panel, base.Component.Site);
				this.containerSelectorGlyph = new ToolStripPanelSelectionGlyph(Rectangle.Empty, Cursors.Default, this.panel, base.Component.Site, this.behavior);
			}
			if (this.panel != null && this.panel.Dock == DockStyle.Top)
			{
				this.panel.Padding = new Padding(0, 0, 25, 25);
				this.containerSelectorGlyph.IsExpanded = true;
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000DD3F9 File Offset: 0x000DB5F9
		private void OnKeyShowDesignerActions(object sender, EventArgs e)
		{
			if (this.containerSelectorGlyph != null)
			{
				this.behavior.OnMouseDown(this.containerSelectorGlyph, MouseButtons.Left, Point.Empty);
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000DD420 File Offset: 0x000DB620
		internal Glyph GetGlyph()
		{
			if (this.panel != null)
			{
				if (this.containerSelectorGlyph == null)
				{
					this.behavior = new ToolStripPanelSelectionBehavior(this.panel, base.Component.Site);
					this.containerSelectorGlyph = new ToolStripPanelSelectionGlyph(Rectangle.Empty, Cursors.Default, this.panel, base.Component.Site, this.behavior);
				}
				if (this.panel.Visible)
				{
					return this.containerSelectorGlyph;
				}
			}
			return null;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000DD49C File Offset: 0x000DB69C
		protected override Control GetParentForComponent(IComponent component)
		{
			Type type = component.GetType();
			if (typeof(ToolStrip).IsAssignableFrom(type))
			{
				return this.panel;
			}
			ToolStripContainer toolStripContainer = this.panel.Parent as ToolStripContainer;
			if (toolStripContainer != null)
			{
				return toolStripContainer.ContentPanel;
			}
			return null;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000DD4E8 File Offset: 0x000DB6E8
		public override void Initialize(IComponent component)
		{
			this.panel = (component as ToolStripPanel);
			base.Initialize(component);
			this.Padding = this.panel.Padding;
			this.designerHost = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
			if (this.selectionSvc == null)
			{
				this.selectionSvc = (ISelectionService)this.GetService(typeof(ISelectionService));
				this.selectionSvc.SelectionChanging += this.OnSelectionChanging;
				this.selectionSvc.SelectionChanged += this.OnSelectionChanged;
			}
			if (this.designerHost != null)
			{
				this.componentChangeSvc = (IComponentChangeService)this.designerHost.GetService(typeof(IComponentChangeService));
			}
			if (this.componentChangeSvc != null)
			{
				this.componentChangeSvc.ComponentChanged += this.ComponentChangeSvc_ComponentChanged;
			}
			this.panel.ControlAdded += this.OnControlAdded;
			this.panel.ControlRemoved += this.OnControlRemoved;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000DD5FF File Offset: 0x000DB7FF
		internal void InvalidateGlyph()
		{
			if (this.containerSelectorGlyph != null)
			{
				base.BehaviorService.Invalidate(this.containerSelectorGlyph.Bounds);
			}
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000DD620 File Offset: 0x000DB820
		private void OnControlAdded(object sender, ControlEventArgs e)
		{
			if (e.Control is ToolStrip)
			{
				this.panel.Padding = new Padding(0);
				if (this.containerSelectorGlyph != null)
				{
					this.containerSelectorGlyph.IsExpanded = false;
				}
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(e.Control)["Dock"];
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(e.Control, DockStyle.None);
				}
				if (this.designerHost != null && !this.designerHost.Loading)
				{
					SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
					if (selectionManager != null)
					{
						selectionManager.Refresh();
					}
				}
			}
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000DD6C4 File Offset: 0x000DB8C4
		private void OnControlRemoved(object sender, ControlEventArgs e)
		{
			if (this.panel.Controls.Count == 0)
			{
				if (this.containerSelectorGlyph != null)
				{
					this.containerSelectorGlyph.IsExpanded = false;
				}
				if (this.designerHost != null && !this.designerHost.Loading)
				{
					SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
					if (selectionManager != null)
					{
						selectionManager.Refresh();
					}
				}
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000DD72B File Offset: 0x000DB92B
		protected override void OnContextMenu(int x, int y)
		{
			if (this.panel != null && this.panel.Parent is ToolStripContainer)
			{
				this.DesignerContextMenu.Show(x, y);
				return;
			}
			base.OnContextMenu(x, y);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000DD760 File Offset: 0x000DB960
		private void OnSelectionChanging(object sender, EventArgs e)
		{
			if (this.designerShortCutCommand != null)
			{
				IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
				if (menuCommandService != null)
				{
					menuCommandService.RemoveCommand(this.designerShortCutCommand);
					if (this.oldShortCutCommand != null)
					{
						menuCommandService.AddCommand(this.oldShortCutCommand);
					}
				}
				this.designerShortCutCommand = null;
			}
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000DD7B8 File Offset: 0x000DB9B8
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			if (this.selectionSvc.PrimarySelection == this.panel)
			{
				this.designerShortCutCommand = new MenuCommand(new EventHandler(this.OnKeyShowDesignerActions), MenuCommands.KeyInvokeSmartTag);
				IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
				if (menuCommandService != null)
				{
					this.oldShortCutCommand = menuCommandService.FindCommand(MenuCommands.KeyInvokeSmartTag);
					if (this.oldShortCutCommand != null)
					{
						menuCommandService.RemoveCommand(this.oldShortCutCommand);
					}
					menuCommandService.AddCommand(this.designerShortCutCommand);
				}
			}
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000DD840 File Offset: 0x000DBA40
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			if (!ToolStripDesignerUtils.DisplayInformation.TerminalServer && !ToolStripDesignerUtils.DisplayInformation.HighContrast && !ToolStripDesignerUtils.DisplayInformation.LowResolution)
			{
				using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.White)))
				{
					pe.Graphics.FillRectangle(brush, this.panel.ClientRectangle);
				}
			}
			this.DrawBorder(pe.Graphics);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000DD8B4 File Offset: 0x000DBAB4
		protected override void PreFilterEvents(IDictionary events)
		{
			base.PreFilterEvents(events);
			if (this.panel.Parent is ToolStripContainer)
			{
				string[] array = new string[]
				{
					"AutoSizeChanged",
					"BindingContextChanged",
					"CausesValidationChanged",
					"ChangeUICues",
					"DockChanged",
					"DragDrop",
					"DragEnter",
					"DragLeave",
					"DragOver",
					"EnabledChanged",
					"FontChanged",
					"ForeColorChanged",
					"GiveFeedback",
					"ImeModeChanged",
					"KeyDown",
					"KeyPress",
					"KeyUp",
					"LocationChanged",
					"MarginChanged",
					"MouseCaptureChanged",
					"Move",
					"QueryAccessibilityHelp",
					"QueryContinueDrag",
					"RegionChanged",
					"Scroll",
					"Validated",
					"Validating"
				};
				for (int i = 0; i < array.Length; i++)
				{
					EventDescriptor eventDescriptor = (EventDescriptor)events[array[i]];
					if (eventDescriptor != null)
					{
						events[array[i]] = TypeDescriptor.CreateEvent(eventDescriptor.ComponentType, eventDescriptor, new Attribute[]
						{
							BrowsableAttribute.No
						});
					}
				}
			}
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000DDA14 File Offset: 0x000DBC14
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			if (this.panel.Parent is ToolStripContainer)
			{
				properties.Remove("Modifiers");
				properties.Remove("Locked");
				properties.Remove("GenerateMember");
				string[] array = new string[]
				{
					"Anchor",
					"AutoSize",
					"Dock",
					"DockPadding",
					"Height",
					"Location",
					"Name",
					"Orientation",
					"Renderer",
					"RowMargin",
					"Size",
					"Visible",
					"Width"
				};
				for (int i = 0; i < array.Length; i++)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
					if (propertyDescriptor != null)
					{
						properties[array[i]] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
						{
							BrowsableAttribute.No,
							DesignerSerializationVisibilityAttribute.Hidden
						});
					}
				}
			}
			string[] array2 = new string[]
			{
				"Padding",
				"Visible"
			};
			Attribute[] attributes = new Attribute[0];
			for (int j = 0; j < array2.Length; j++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array2[j]];
				if (propertyDescriptor != null)
				{
					properties[array2[j]] = TypeDescriptor.CreateProperty(typeof(ToolStripPanelDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000DDB84 File Offset: 0x000DBD84
		private bool ShouldSerializePadding()
		{
			return !((Padding)base.ShadowProperties["Padding"]).Equals(ToolStripPanelDesigner._defaultPadding);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000DDBC1 File Offset: 0x000DBDC1
		private bool ShouldSerializeVisible()
		{
			return !this.Visible;
		}

		// Token: 0x04001A00 RID: 6656
		private ToolStripPanel panel;

		// Token: 0x04001A01 RID: 6657
		private IComponentChangeService componentChangeSvc;

		// Token: 0x04001A02 RID: 6658
		private static Padding _defaultPadding = new Padding(0);

		// Token: 0x04001A03 RID: 6659
		private IDesignerHost designerHost;

		// Token: 0x04001A04 RID: 6660
		private ToolStripPanelSelectionGlyph containerSelectorGlyph;

		// Token: 0x04001A05 RID: 6661
		private ToolStripPanelSelectionBehavior behavior;

		// Token: 0x04001A06 RID: 6662
		private BaseContextMenuStrip contextMenu;

		// Token: 0x04001A07 RID: 6663
		private ISelectionService selectionSvc;

		// Token: 0x04001A08 RID: 6664
		private MenuCommand designerShortCutCommand;

		// Token: 0x04001A09 RID: 6665
		private MenuCommand oldShortCutCommand;
	}
}
