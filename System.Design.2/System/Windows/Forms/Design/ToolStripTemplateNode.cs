using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000362 RID: 866
	internal class ToolStripTemplateNode : IMenuStatusHandler
	{
		// Token: 0x0600237B RID: 9083 RVA: 0x000DDBE4 File Offset: 0x000DBDE4
		public ToolStripTemplateNode(IComponent component, string text, Image image)
		{
			this.component = component;
			this.activeItem = (component as ToolStripItem);
			this._designerHost = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
			this._designer = this._designerHost.GetDesigner(component);
			this._designSurface = (DesignSurface)component.Site.GetService(typeof(DesignSurface));
			if (this._designSurface != null)
			{
				this._designSurface.Flushed += this.OnLoaderFlushed;
			}
			if (!ToolStripTemplateNode.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					ToolStripTemplateNode.TOOLSTRIP_TEMPLATE_HEIGHT = DpiHelper.LogicalToDeviceUnitsY(22);
					ToolStripTemplateNode.TEMPLATE_HEIGHT = DpiHelper.LogicalToDeviceUnitsY(19);
					ToolStripTemplateNode.TOOLSTRIP_TEMPLATE_WIDTH = DpiHelper.LogicalToDeviceUnitsX(92);
					ToolStripTemplateNode.TEMPLATE_WIDTH = DpiHelper.LogicalToDeviceUnitsX(31);
					ToolStripTemplateNode.TEMPLATE_HOTREGION_WIDTH = DpiHelper.LogicalToDeviceUnitsX(9);
					ToolStripTemplateNode.MINITOOLSTRIP_DROPDOWN_BUTTON_WIDTH = DpiHelper.LogicalToDeviceUnitsX(11);
					ToolStripTemplateNode.MINITOOLSTRIP_TEXTBOX_WIDTH = DpiHelper.LogicalToDeviceUnitsX(90);
				}
				ToolStripTemplateNode.isScalingInitialized = true;
			}
			this.SetupNewEditNode(this, text, image, component);
			this.commands = new MenuCommand[]
			{
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyMoveUp),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyMoveDown),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyMoveLeft),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyMoveRight),
				new MenuCommand(new EventHandler(this.OnMenuCut), StandardCommands.Delete),
				new MenuCommand(new EventHandler(this.OnMenuCut), StandardCommands.Cut),
				new MenuCommand(new EventHandler(this.OnMenuCut), StandardCommands.Copy),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyNudgeUp),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyNudgeDown),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyNudgeLeft),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyNudgeRight),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeySizeWidthIncrease),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeySizeHeightIncrease),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeySizeWidthDecrease),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeySizeHeightDecrease),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyNudgeWidthIncrease),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyNudgeHeightIncrease),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyNudgeWidthDecrease),
				new MenuCommand(new EventHandler(this.OnMenuCut), MenuCommands.KeyNudgeHeightDecrease)
			};
			this.addCommands = new MenuCommand[]
			{
				new MenuCommand(new EventHandler(this.OnMenuCut), StandardCommands.Undo),
				new MenuCommand(new EventHandler(this.OnMenuCut), StandardCommands.Redo)
			};
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000DDF1D File Offset: 0x000DC11D
		// (set) Token: 0x0600237D RID: 9085 RVA: 0x000DDF28 File Offset: 0x000DC128
		public bool Active
		{
			get
			{
				return this.active;
			}
			set
			{
				if (this.active != value)
				{
					this.active = value;
					if (this.KeyboardService != null)
					{
						this.KeyboardService.TemplateNodeActive = value;
					}
					if (this.active)
					{
						this.OnActivated(new EventArgs());
						if (this.KeyboardService != null)
						{
							this.KeyboardService.ActiveTemplateNode = this;
						}
						IMenuCommandService menuCommandService = (IMenuCommandService)this.component.Site.GetService(typeof(IMenuCommandService));
						if (menuCommandService != null)
						{
							this.oldUndoCommand = menuCommandService.FindCommand(StandardCommands.Undo);
							if (this.oldUndoCommand != null)
							{
								menuCommandService.RemoveCommand(this.oldUndoCommand);
							}
							this.oldRedoCommand = menuCommandService.FindCommand(StandardCommands.Redo);
							if (this.oldRedoCommand != null)
							{
								menuCommandService.RemoveCommand(this.oldRedoCommand);
							}
							for (int i = 0; i < this.addCommands.Length; i++)
							{
								this.addCommands[i].Enabled = false;
								menuCommandService.AddCommand(this.addCommands[i]);
							}
						}
						IEventHandlerService eventHandlerService = (IEventHandlerService)this.component.Site.GetService(typeof(IEventHandlerService));
						if (eventHandlerService != null)
						{
							eventHandlerService.PushHandler(this);
							return;
						}
					}
					else
					{
						this.OnDeactivated(new EventArgs());
						if (this.KeyboardService != null)
						{
							this.KeyboardService.ActiveTemplateNode = null;
						}
						IMenuCommandService menuCommandService2 = (IMenuCommandService)this.component.Site.GetService(typeof(IMenuCommandService));
						if (menuCommandService2 != null)
						{
							for (int j = 0; j < this.addCommands.Length; j++)
							{
								menuCommandService2.RemoveCommand(this.addCommands[j]);
							}
						}
						if (this.oldUndoCommand != null)
						{
							menuCommandService2.AddCommand(this.oldUndoCommand);
						}
						if (this.oldRedoCommand != null)
						{
							menuCommandService2.AddCommand(this.oldRedoCommand);
						}
						IEventHandlerService eventHandlerService2 = (IEventHandlerService)this.component.Site.GetService(typeof(IEventHandlerService));
						if (eventHandlerService2 != null)
						{
							eventHandlerService2.PopHandler(this);
						}
					}
				}
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x000DE10D File Offset: 0x000DC30D
		// (set) Token: 0x0600237F RID: 9087 RVA: 0x000DE115 File Offset: 0x000DC315
		public ToolStripItem ActiveItem
		{
			get
			{
				return this.activeItem;
			}
			set
			{
				this.activeItem = value;
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06002380 RID: 9088 RVA: 0x000DE11E File Offset: 0x000DC31E
		// (remove) Token: 0x06002381 RID: 9089 RVA: 0x000DE137 File Offset: 0x000DC337
		public event EventHandler Activated
		{
			add
			{
				this.onActivated = (EventHandler)Delegate.Combine(this.onActivated, value);
			}
			remove
			{
				this.onActivated = (EventHandler)Delegate.Remove(this.onActivated, value);
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x000DE150 File Offset: 0x000DC350
		// (set) Token: 0x06002383 RID: 9091 RVA: 0x000DE158 File Offset: 0x000DC358
		public Rectangle Bounds
		{
			get
			{
				return this.boundingRect;
			}
			set
			{
				this.boundingRect = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x000DE161 File Offset: 0x000DC361
		// (set) Token: 0x06002385 RID: 9093 RVA: 0x000DE169 File Offset: 0x000DC369
		public DesignerToolStripControlHost ControlHost
		{
			get
			{
				return this.controlHost;
			}
			set
			{
				this.controlHost = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000DE174 File Offset: 0x000DC374
		private ContextMenuStrip DesignerContextMenu
		{
			get
			{
				BaseContextMenuStrip baseContextMenuStrip = new BaseContextMenuStrip(this.component.Site, this.controlHost);
				baseContextMenuStrip.Populated = false;
				baseContextMenuStrip.GroupOrdering.Clear();
				baseContextMenuStrip.GroupOrdering.AddRange(new string[]
				{
					"Code",
					"Custom",
					"Selection",
					"Edit"
				});
				baseContextMenuStrip.Text = "CustomContextMenu";
				TemplateNodeCustomMenuItemCollection templateNodeCustomMenuItemCollection = new TemplateNodeCustomMenuItemCollection(this.component.Site, this.controlHost);
				foreach (object obj in templateNodeCustomMenuItemCollection)
				{
					ToolStripItem item = (ToolStripItem)obj;
					baseContextMenuStrip.Groups["Custom"].Items.Add(item);
				}
				return baseContextMenuStrip;
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002387 RID: 9095 RVA: 0x000DE25C File Offset: 0x000DC45C
		// (remove) Token: 0x06002388 RID: 9096 RVA: 0x000DE275 File Offset: 0x000DC475
		public event EventHandler Deactivated
		{
			add
			{
				this.onDeactivated = (EventHandler)Delegate.Combine(this.onDeactivated, value);
			}
			remove
			{
				this.onDeactivated = (EventHandler)Delegate.Remove(this.onDeactivated, value);
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06002389 RID: 9097 RVA: 0x000DE28E File Offset: 0x000DC48E
		// (remove) Token: 0x0600238A RID: 9098 RVA: 0x000DE2A7 File Offset: 0x000DC4A7
		public event EventHandler Closed
		{
			add
			{
				this.onClosed = (EventHandler)Delegate.Combine(this.onClosed, value);
			}
			remove
			{
				this.onClosed = (EventHandler)Delegate.Remove(this.onClosed, value);
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x000DE2C0 File Offset: 0x000DC4C0
		public ToolStrip EditorToolStrip
		{
			get
			{
				return this._miniToolStrip;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000DE2C8 File Offset: 0x000DC4C8
		internal TextBox EditBox
		{
			get
			{
				if (this.centerTextBox == null)
				{
					return null;
				}
				return (TextBox)this.centerTextBox.Control;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000DE2E4 File Offset: 0x000DC4E4
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x000DE2EC File Offset: 0x000DC4EC
		public Rectangle HotRegion
		{
			get
			{
				return this.hotRegion;
			}
			set
			{
				this.hotRegion = value;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x000DE2F5 File Offset: 0x000DC4F5
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x000DE2FD File Offset: 0x000DC4FD
		public bool IMEModeSet
		{
			get
			{
				return this.imeModeSet;
			}
			set
			{
				this.imeModeSet = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x000DE306 File Offset: 0x000DC506
		private ToolStripKeyboardHandlingService KeyboardService
		{
			get
			{
				if (this.toolStripKeyBoardService == null)
				{
					this.toolStripKeyBoardService = (ToolStripKeyboardHandlingService)this.component.Site.GetService(typeof(ToolStripKeyboardHandlingService));
				}
				return this.toolStripKeyBoardService;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x000DE33B File Offset: 0x000DC53B
		private ISelectionService SelectionService
		{
			get
			{
				if (this.selectionService == null)
				{
					this.selectionService = (ISelectionService)this.component.Site.GetService(typeof(ISelectionService));
				}
				return this.selectionService;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x000DE370 File Offset: 0x000DC570
		private BehaviorService BehaviorService
		{
			get
			{
				if (this.behaviorService == null)
				{
					this.behaviorService = (BehaviorService)this.component.Site.GetService(typeof(BehaviorService));
				}
				return this.behaviorService;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x000DE3A5 File Offset: 0x000DC5A5
		// (set) Token: 0x06002395 RID: 9109 RVA: 0x000DE3AD File Offset: 0x000DC5AD
		public Type ToolStripItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x000DE3B6 File Offset: 0x000DC5B6
		// (set) Token: 0x06002397 RID: 9111 RVA: 0x000DE3BE File Offset: 0x000DC5BE
		internal bool IsSystemContextMenuDisplayed
		{
			get
			{
				return this.isSystemContextMenuDisplayed;
			}
			set
			{
				this.isSystemContextMenuDisplayed = value;
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000DE3C8 File Offset: 0x000DC5C8
		private void AddNewItemClick(object sender, EventArgs e)
		{
			if (this.addItemButton != null)
			{
				this.addItemButton.DropDown.Visible = false;
			}
			if (this.component is ToolStrip && this.SelectionService != null)
			{
				ToolStripDesigner toolStripDesigner = this._designerHost.GetDesigner(this.component) as ToolStripDesigner;
				try
				{
					if (toolStripDesigner != null)
					{
						toolStripDesigner.DontCloseOverflow = true;
					}
					this.SelectionService.SetSelectedComponents(new object[]
					{
						this.component
					});
				}
				finally
				{
					if (toolStripDesigner != null)
					{
						toolStripDesigner.DontCloseOverflow = false;
					}
				}
			}
			ItemTypeToolStripMenuItem itemTypeToolStripMenuItem = (ItemTypeToolStripMenuItem)sender;
			if (this.lastSelection != null)
			{
				this.lastSelection.Checked = false;
			}
			itemTypeToolStripMenuItem.Checked = true;
			this.lastSelection = itemTypeToolStripMenuItem;
			this.ToolStripItemType = itemTypeToolStripMenuItem.ItemType;
			ToolStrip currentParent = this.controlHost.GetCurrentParent();
			if (currentParent is MenuStrip)
			{
				this.CommitEditor(true, true, false);
			}
			else
			{
				this.CommitEditor(true, false, false);
			}
			if (this.KeyboardService != null)
			{
				this.KeyboardService.TemplateNodeActive = false;
			}
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000DE4CC File Offset: 0x000DC6CC
		private void CenterLabelClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (this.KeyboardService != null && this.KeyboardService.TemplateNodeActive)
				{
					return;
				}
				if (this.KeyboardService != null)
				{
					this.KeyboardService.SelectedDesignerControl = this.controlHost;
				}
				this.SelectionService.SetSelectedComponents(null, SelectionTypes.Replace);
				if (this.BehaviorService != null)
				{
					Point point = this.BehaviorService.ControlToAdornerWindow(this._miniToolStrip);
					point = this.BehaviorService.AdornerWindowPointToScreen(point);
					point.Offset(e.Location);
					this.DesignerContextMenu.Show(point);
					return;
				}
			}
			else
			{
				if (this.hotRegion.Contains(e.Location) && !this.KeyboardService.TemplateNodeActive)
				{
					if (this.KeyboardService != null)
					{
						this.KeyboardService.SelectedDesignerControl = this.controlHost;
					}
					this.SelectionService.SetSelectedComponents(null, SelectionTypes.Replace);
					ToolStripDropDown toolStripDropDown = this.contextMenu;
					if (toolStripDropDown != null)
					{
						toolStripDropDown.Closed -= this.OnContextMenuClosed;
						toolStripDropDown.Opened -= this.OnContextMenuOpened;
						toolStripDropDown.Dispose();
					}
					this.contextMenu = null;
					this.ShowDropDownMenu();
					return;
				}
				ToolStripDesigner.LastCursorPosition = Cursor.Position;
				if (this._designer is ToolStripDesigner)
				{
					if (this.KeyboardService.TemplateNodeActive)
					{
						this.KeyboardService.ActiveTemplateNode.Commit(false, false);
					}
					if (this.SelectionService.PrimarySelection == null)
					{
						this.SelectionService.SetSelectedComponents(new object[]
						{
							this.component
						}, SelectionTypes.Replace);
					}
					this.KeyboardService.SelectedDesignerControl = this.controlHost;
					this.SelectionService.SetSelectedComponents(null, SelectionTypes.Replace);
					((ToolStripDesigner)this._designer).ShowEditNode(true);
				}
				if (this._designer is ToolStripMenuItemDesigner)
				{
					IServiceProvider site = this.component.Site;
					if (this.KeyboardService.TemplateNodeActive)
					{
						ToolStripItem toolStripItem = this.component as ToolStripItem;
						if (toolStripItem != null)
						{
							if (toolStripItem.Visible)
							{
								this.KeyboardService.ActiveTemplateNode.Commit(false, false);
							}
							else
							{
								this.KeyboardService.ActiveTemplateNode.Commit(false, true);
							}
						}
						else
						{
							this.KeyboardService.ActiveTemplateNode.Commit(false, false);
						}
					}
					if (this._designer != null)
					{
						((ToolStripMenuItemDesigner)this._designer).EditTemplateNode(true);
						return;
					}
					ISelectionService selectionService = (ISelectionService)site.GetService(typeof(ISelectionService));
					ToolStripItem toolStripItem2 = selectionService.PrimarySelection as ToolStripItem;
					if (toolStripItem2 != null && this._designerHost != null)
					{
						ToolStripMenuItemDesigner toolStripMenuItemDesigner = this._designerHost.GetDesigner(toolStripItem2) as ToolStripMenuItemDesigner;
						if (toolStripMenuItemDesigner != null)
						{
							if (!toolStripItem2.IsOnDropDown)
							{
								Rectangle glyphBounds = toolStripMenuItemDesigner.GetGlyphBounds();
								ToolStripDesignerUtils.GetAdjustedBounds(toolStripItem2, ref glyphBounds);
								BehaviorService behaviorService = site.GetService(typeof(BehaviorService)) as BehaviorService;
								if (behaviorService != null)
								{
									behaviorService.Invalidate(glyphBounds);
								}
							}
							toolStripMenuItemDesigner.EditTemplateNode(true);
						}
					}
				}
			}
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000DE7A2 File Offset: 0x000DC9A2
		private void CenterLabelMouseEnter(object sender, EventArgs e)
		{
			if (this.renderer != null && !this.KeyboardService.TemplateNodeActive && this.renderer.State != 6)
			{
				this.renderer.State = 4;
				this._miniToolStrip.Invalidate();
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000DE7E0 File Offset: 0x000DC9E0
		private void CenterLabelMouseMove(object sender, MouseEventArgs e)
		{
			if (this.renderer != null && !this.KeyboardService.TemplateNodeActive && this.renderer.State != 6)
			{
				if (this.hotRegion.Contains(e.Location))
				{
					this.renderer.State = 5;
				}
				else
				{
					this.renderer.State = 4;
				}
				this._miniToolStrip.Invalidate();
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000DE848 File Offset: 0x000DCA48
		private void CenterLabelMouseLeave(object sender, EventArgs e)
		{
			if (this.renderer != null && !this.KeyboardService.TemplateNodeActive)
			{
				if (this.renderer.State != 6)
				{
					this.renderer.State = 0;
				}
				if (this.KeyboardService != null && this.KeyboardService.SelectedDesignerControl == this.controlHost)
				{
					this.renderer.State = 1;
				}
				this._miniToolStrip.Invalidate();
			}
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000DE8B6 File Offset: 0x000DCAB6
		private void CenterTextBoxMouseEnter(object sender, EventArgs e)
		{
			if (this.renderer != null)
			{
				this.renderer.State = 1;
				this._miniToolStrip.Invalidate();
			}
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000DE8D7 File Offset: 0x000DCAD7
		private void CenterTextBoxMouseLeave(object sender, EventArgs e)
		{
			if (this.renderer != null && !this.Active)
			{
				this.renderer.State = 0;
				this._miniToolStrip.Invalidate();
			}
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000DE900 File Offset: 0x000DCB00
		internal void CloseEditor()
		{
			if (this._miniToolStrip != null)
			{
				this.Active = false;
				if (this.lastSelection != null)
				{
					this.lastSelection.Dispose();
					this.lastSelection = null;
				}
				ToolStrip toolStrip = this.component as ToolStrip;
				if (toolStrip != null)
				{
					toolStrip.RightToLeftChanged -= this.OnRightToLeftChanged;
				}
				else
				{
					ToolStripDropDownItem toolStripDropDownItem = this.component as ToolStripDropDownItem;
					if (toolStripDropDownItem != null)
					{
						toolStripDropDownItem.RightToLeftChanged -= this.OnRightToLeftChanged;
					}
				}
				if (this.centerLabel != null)
				{
					this.centerLabel.MouseUp -= this.CenterLabelClick;
					this.centerLabel.MouseEnter -= this.CenterLabelMouseEnter;
					this.centerLabel.MouseMove -= this.CenterLabelMouseMove;
					this.centerLabel.MouseLeave -= this.CenterLabelMouseLeave;
					this.centerLabel.Dispose();
					this.centerLabel = null;
				}
				if (this.addItemButton != null)
				{
					this.addItemButton.MouseMove -= this.OnMouseMove;
					this.addItemButton.MouseUp -= this.OnMouseUp;
					this.addItemButton.MouseDown -= this.OnMouseDown;
					this.addItemButton.DropDownOpened -= this.OnAddItemButtonDropDownOpened;
					this.addItemButton.DropDown.Dispose();
					this.addItemButton.Dispose();
					this.addItemButton = null;
				}
				if (this.contextMenu != null)
				{
					this.contextMenu.Closed -= this.OnContextMenuClosed;
					this.contextMenu.Opened -= this.OnContextMenuOpened;
					this.contextMenu = null;
				}
				this._miniToolStrip.MouseLeave -= this.OnMouseLeave;
				this._miniToolStrip.Dispose();
				this._miniToolStrip = null;
				if (this._designSurface != null)
				{
					this._designSurface.Flushed -= this.OnLoaderFlushed;
					this._designSurface = null;
				}
				this._designer = null;
				this.OnClosed(new EventArgs());
			}
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000DEB18 File Offset: 0x000DCD18
		internal void Commit(bool enterKeyPressed, bool tabKeyPressed)
		{
			if (this._miniToolStrip != null && this.inSituMode)
			{
				string text = ((TextBox)this.centerTextBox.Control).Text;
				if (string.IsNullOrEmpty(text))
				{
					this.RollBack();
					return;
				}
				this.CommitEditor(true, enterKeyPressed, tabKeyPressed);
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000DEB63 File Offset: 0x000DCD63
		internal void CommitAndSelect()
		{
			this.Commit(false, false);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000DEB70 File Offset: 0x000DCD70
		private void CommitEditor(bool commit, bool enterKeyPressed, bool tabKeyPressed)
		{
			ToolStripItem toolStripItem = this.SelectionService.PrimarySelection as ToolStripItem;
			string text = (this.centerTextBox != null) ? ((TextBox)this.centerTextBox.Control).Text : string.Empty;
			this.ExitInSituEdit();
			this.FocusForm();
			if (commit && (this._designer is ToolStripDesigner || this._designer is ToolStripMenuItemDesigner))
			{
				if (text == "-" && this._designer is ToolStripMenuItemDesigner)
				{
					this.ToolStripItemType = typeof(ToolStripSeparator);
				}
				Type type;
				if (this.ToolStripItemType != null)
				{
					type = this.ToolStripItemType;
					this.ToolStripItemType = null;
				}
				else
				{
					Type[] standardItemTypes = ToolStripDesignerUtils.GetStandardItemTypes(this.component);
					type = standardItemTypes[0];
				}
				if (this._designer is ToolStripDesigner)
				{
					((ToolStripDesigner)this._designer).AddNewItem(type, text, enterKeyPressed, tabKeyPressed);
				}
				else
				{
					((ToolStripItemDesigner)this._designer).CommitEdit(type, text, commit, enterKeyPressed, tabKeyPressed);
				}
			}
			else if (this._designer is ToolStripItemDesigner)
			{
				((ToolStripItemDesigner)this._designer).CommitEdit(this._designer.Component.GetType(), text, commit, enterKeyPressed, tabKeyPressed);
			}
			if (toolStripItem != null && this._designerHost != null)
			{
				ToolStripItemDesigner toolStripItemDesigner = this._designerHost.GetDesigner(toolStripItem) as ToolStripItemDesigner;
				if (toolStripItemDesigner != null)
				{
					Rectangle glyphBounds = toolStripItemDesigner.GetGlyphBounds();
					ToolStripDesignerUtils.GetAdjustedBounds(toolStripItem, ref glyphBounds);
					glyphBounds.Inflate(1, 1);
					Region region = new Region(glyphBounds);
					glyphBounds.Inflate(-2, -2);
					region.Exclude(glyphBounds);
					if (this.BehaviorService != null)
					{
						this.BehaviorService.Invalidate(region);
					}
					region.Dispose();
				}
			}
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000DED20 File Offset: 0x000DCF20
		private void EnterInSituEdit()
		{
			if (!this.inSituMode)
			{
				if (this._miniToolStrip.Parent != null)
				{
					this._miniToolStrip.Parent.SuspendLayout();
				}
				try
				{
					this.Active = true;
					this.inSituMode = true;
					if (this.renderer != null)
					{
						this.renderer.State = 1;
					}
					TextBox textBox = new ToolStripTemplateNode.TemplateTextBox(this._miniToolStrip, this);
					textBox.BorderStyle = BorderStyle.FixedSingle;
					textBox.Text = this.centerLabel.Text;
					textBox.ForeColor = SystemColors.WindowText;
					this.centerTextBox = new ToolStripControlHost(textBox);
					this.centerTextBox.Dock = DockStyle.None;
					this.centerTextBox.AutoSize = false;
					this.centerTextBox.Width = ToolStripTemplateNode.MINITOOLSTRIP_TEXTBOX_WIDTH;
					ToolStripDropDownItem toolStripDropDownItem = this.activeItem as ToolStripDropDownItem;
					if (toolStripDropDownItem != null && !toolStripDropDownItem.IsOnDropDown)
					{
						this.centerTextBox.Margin = new Padding(1, 2, 1, 3);
					}
					else
					{
						this.centerTextBox.Margin = new Padding(1);
					}
					this.centerTextBox.Size = this._miniToolStrip.DisplayRectangle.Size - this.centerTextBox.Margin.Size;
					this.centerTextBox.Name = "centerTextBox";
					this.centerTextBox.MouseEnter += this.CenterTextBoxMouseEnter;
					this.centerTextBox.MouseLeave += this.CenterTextBoxMouseLeave;
					int num = this._miniToolStrip.Items.IndexOf(this.centerLabel);
					if (num != -1)
					{
						this._miniToolStrip.Items.Insert(num, this.centerTextBox);
						this._miniToolStrip.Items.Remove(this.centerLabel);
					}
					textBox.KeyUp += this.OnKeyUp;
					textBox.KeyDown += this.OnKeyDown;
					textBox.SelectAll();
					if (this._designerHost != null)
					{
						Control control = (Control)this._designerHost.RootComponent;
						NativeMethods.SendMessage(control.Handle, 11, 0, 0);
						textBox.Focus();
						NativeMethods.SendMessage(control.Handle, 11, 1, 0);
					}
				}
				finally
				{
					if (this._miniToolStrip.Parent != null)
					{
						this._miniToolStrip.Parent.ResumeLayout();
					}
				}
			}
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000DEF80 File Offset: 0x000DD180
		private void ExitInSituEdit()
		{
			if (this.centerTextBox != null && this.inSituMode)
			{
				if (this._miniToolStrip.Parent != null)
				{
					this._miniToolStrip.Parent.SuspendLayout();
				}
				try
				{
					int num = this._miniToolStrip.Items.IndexOf(this.centerTextBox);
					if (num != -1)
					{
						this.centerLabel.Text = SR.GetString("ToolStripDesignerTemplateNodeEnterText");
						this._miniToolStrip.Items.Insert(num, this.centerLabel);
						this._miniToolStrip.Items.Remove(this.centerTextBox);
						((TextBox)this.centerTextBox.Control).KeyUp -= this.OnKeyUp;
						((TextBox)this.centerTextBox.Control).KeyDown -= this.OnKeyDown;
					}
					this.centerTextBox.MouseEnter -= this.CenterTextBoxMouseEnter;
					this.centerTextBox.MouseLeave -= this.CenterTextBoxMouseLeave;
					this.centerTextBox.Dispose();
					this.centerTextBox = null;
					this.inSituMode = false;
					this.SetWidth(null);
				}
				finally
				{
					if (this._miniToolStrip.Parent != null)
					{
						this._miniToolStrip.Parent.ResumeLayout();
					}
					this.Active = false;
				}
			}
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000DF0E8 File Offset: 0x000DD2E8
		internal void FocusEditor(ToolStripItem currentItem)
		{
			if (currentItem != null)
			{
				this.centerLabel.Text = currentItem.Text;
			}
			this.EnterInSituEdit();
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000DF104 File Offset: 0x000DD304
		private void FocusForm()
		{
			DesignerFrame designerFrame = this.component.Site.GetService(typeof(ISplitWindowService)) as DesignerFrame;
			if (designerFrame != null && this._designerHost != null)
			{
				Control control = (Control)this._designerHost.RootComponent;
				NativeMethods.SendMessage(control.Handle, 11, 0, 0);
				designerFrame.Focus();
				NativeMethods.SendMessage(control.Handle, 11, 1, 0);
			}
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000DF176 File Offset: 0x000DD376
		protected void OnActivated(EventArgs e)
		{
			if (this.onActivated != null)
			{
				this.onActivated(this, e);
			}
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000DF18D File Offset: 0x000DD38D
		private void OnAddItemButtonDropDownOpened(object sender, EventArgs e)
		{
			this.addItemButton.DropDown.Focus();
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000DF1A0 File Offset: 0x000DD3A0
		protected void OnClosed(EventArgs e)
		{
			if (this.onClosed != null)
			{
				this.onClosed(this, e);
			}
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000DE8B6 File Offset: 0x000DCAB6
		private void OnContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			if (this.renderer != null)
			{
				this.renderer.State = 1;
				this._miniToolStrip.Invalidate();
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000DF1B7 File Offset: 0x000DD3B7
		private void OnContextMenuOpened(object sender, EventArgs e)
		{
			if (this.KeyboardService != null)
			{
				this.KeyboardService.TemplateNodeContextMenuOpen = true;
			}
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000DF1CD File Offset: 0x000DD3CD
		protected void OnDeactivated(EventArgs e)
		{
			if (this.onDeactivated != null)
			{
				this.onDeactivated(this, e);
			}
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000DEB63 File Offset: 0x000DCD63
		private void OnLoaderFlushed(object sender, EventArgs e)
		{
			this.Commit(false, false);
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000DF1E4 File Offset: 0x000DD3E4
		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			if (this.IMEModeSet)
			{
				return;
			}
			Keys keyCode = e.KeyCode;
			if (keyCode <= Keys.Escape)
			{
				if (keyCode != Keys.Return)
				{
					if (keyCode != Keys.Escape)
					{
						return;
					}
					this.CommitEditor(false, false, false);
					return;
				}
				else
				{
					if (this.ignoreFirstKeyUp)
					{
						this.ignoreFirstKeyUp = false;
						return;
					}
					this.OnKeyDefaultAction(sender, e);
				}
			}
			else if (keyCode != Keys.Up)
			{
				if (keyCode != Keys.Down)
				{
					return;
				}
				this.Commit(true, false);
				return;
			}
			else
			{
				this.Commit(false, true);
				if (this.KeyboardService != null)
				{
					this.KeyboardService.ProcessUpDown(false);
					return;
				}
			}
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000DF264 File Offset: 0x000DD464
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (this.IMEModeSet)
			{
				return;
			}
			if (e.KeyCode == Keys.A && (e.KeyData & Keys.Control) != Keys.None)
			{
				TextBox textBox = sender as TextBox;
				if (textBox != null)
				{
					textBox.SelectAll();
				}
			}
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000DF2A4 File Offset: 0x000DD4A4
		private void OnKeyDefaultAction(object sender, EventArgs e)
		{
			this.Active = false;
			if (this.centerTextBox.Control != null)
			{
				string text = ((TextBox)this.centerTextBox.Control).Text;
				if (string.IsNullOrEmpty(text))
				{
					this.CommitEditor(false, false, false);
					return;
				}
				this.CommitEditor(true, true, false);
			}
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00003937 File Offset: 0x00001B37
		private void OnMenuCut(object sender, EventArgs e)
		{
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000DF2F8 File Offset: 0x000DD4F8
		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && this.BehaviorService != null)
			{
				Point point = this.BehaviorService.ControlToAdornerWindow(this._miniToolStrip);
				point = this.BehaviorService.AdornerWindowPointToScreen(point);
				point.Offset(e.Location);
				this.DesignerContextMenu.Show(point);
			}
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000DF352 File Offset: 0x000DD552
		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (this.KeyboardService != null)
			{
				this.KeyboardService.SelectedDesignerControl = this.controlHost;
			}
			this.SelectionService.SetSelectedComponents(null, SelectionTypes.Replace);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000DF37C File Offset: 0x000DD57C
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			this.renderer.State = 0;
			if (this.renderer != null)
			{
				if (this.addItemButton != null)
				{
					if (this.addItemButton.ButtonBounds.Contains(e.Location))
					{
						this.renderer.State = 2;
					}
					else if (this.addItemButton.DropDownButtonBounds.Contains(e.Location))
					{
						this.renderer.State = 3;
					}
				}
				this._miniToolStrip.Invalidate();
			}
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000DF400 File Offset: 0x000DD600
		private void OnMouseLeave(object sender, EventArgs e)
		{
			if (this.SelectionService != null)
			{
				ToolStripItem toolStripItem = this.SelectionService.PrimarySelection as ToolStripItem;
				if (toolStripItem != null && this.renderer != null && this.renderer.State != 6)
				{
					this.renderer.State = 0;
				}
				if (this.KeyboardService != null && this.KeyboardService.SelectedDesignerControl == this.controlHost)
				{
					this.renderer.State = 1;
				}
				this._miniToolStrip.Invalidate();
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000DF480 File Offset: 0x000DD680
		private void OnRightToLeftChanged(object sender, EventArgs e)
		{
			ToolStrip toolStrip = sender as ToolStrip;
			if (toolStrip != null)
			{
				this._miniToolStrip.RightToLeft = toolStrip.RightToLeft;
				return;
			}
			ToolStripDropDownItem toolStripDropDownItem = sender as ToolStripDropDownItem;
			this._miniToolStrip.RightToLeft = toolStripDropDownItem.RightToLeft;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000DF4C4 File Offset: 0x000DD6C4
		public bool OverrideInvoke(MenuCommand cmd)
		{
			for (int i = 0; i < this.commands.Length; i++)
			{
				if (this.commands[i].CommandID.Equals(cmd.CommandID) && (cmd.CommandID == StandardCommands.Delete || cmd.CommandID == StandardCommands.Cut || cmd.CommandID == StandardCommands.Copy))
				{
					this.commands[i].Invoke();
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000DF538 File Offset: 0x000DD738
		public bool OverrideStatus(MenuCommand cmd)
		{
			for (int i = 0; i < this.commands.Length; i++)
			{
				if (this.commands[i].CommandID.Equals(cmd.CommandID))
				{
					cmd.Enabled = false;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000DF57C File Offset: 0x000DD77C
		internal void RollBack()
		{
			if (this._miniToolStrip != null && this.inSituMode)
			{
				this.CommitEditor(false, false, false);
			}
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000DF597 File Offset: 0x000DD797
		internal void ShowContextMenu(Point pt)
		{
			this.DesignerContextMenu.Show(pt);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000DF5A8 File Offset: 0x000DD7A8
		internal void ShowDropDownMenu()
		{
			if (this.addItemButton != null)
			{
				if (!this._isPopulated)
				{
					this._isPopulated = true;
					ToolStripDesignerUtils.GetCustomNewItemDropDown(this.contextMenu, this.component, null, new EventHandler(this.AddNewItemClick), false, this.component.Site);
				}
				this.addItemButton.ShowDropDown();
				return;
			}
			if (this.BehaviorService != null)
			{
				Point point = this.BehaviorService.ControlToAdornerWindow(this._miniToolStrip);
				point = this.BehaviorService.AdornerWindowPointToScreen(point);
				Rectangle rectangle = new Rectangle(point, this._miniToolStrip.Size);
				if (this.contextMenu == null)
				{
					this._isPopulated = true;
					this.contextMenu = ToolStripDesignerUtils.GetNewItemDropDown(this.component, null, new EventHandler(this.AddNewItemClick), false, this.component.Site, true);
					this.contextMenu.Closed += this.OnContextMenuClosed;
					this.contextMenu.Opened += this.OnContextMenuOpened;
					this.contextMenu.Text = "ItemSelectionMenu";
				}
				else if (!this._isPopulated)
				{
					this._isPopulated = true;
					ToolStripDesignerUtils.GetCustomNewItemDropDown(this.contextMenu, this.component, null, new EventHandler(this.AddNewItemClick), false, this.component.Site);
				}
				ToolStrip toolStrip = this.component as ToolStrip;
				if (toolStrip != null)
				{
					this.contextMenu.RightToLeft = toolStrip.RightToLeft;
				}
				else
				{
					ToolStripDropDownItem toolStripDropDownItem = this.component as ToolStripDropDownItem;
					if (toolStripDropDownItem != null)
					{
						this.contextMenu.RightToLeft = toolStripDropDownItem.RightToLeft;
					}
				}
				this.contextMenu.Show(rectangle.X, rectangle.Y + rectangle.Height);
				this.contextMenu.Focus();
				if (this.renderer != null)
				{
					this.renderer.State = 6;
					this._miniToolStrip.Invalidate();
				}
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000DF780 File Offset: 0x000DD980
		private void SetUpMenuTemplateNode(ToolStripTemplateNode owner, string text, Image image, IComponent currentItem)
		{
			this.centerLabel = new ToolStripLabel();
			this.centerLabel.Text = text;
			this.centerLabel.AutoSize = false;
			this.centerLabel.IsLink = false;
			this.centerLabel.Margin = new Padding(1);
			if (currentItem is ToolStripDropDownItem)
			{
				this.centerLabel.Margin = new Padding(1, 2, 1, 3);
			}
			this.centerLabel.Padding = new Padding(0, 1, 0, 0);
			this.centerLabel.Name = "centerLabel";
			this.centerLabel.Size = this._miniToolStrip.DisplayRectangle.Size - this.centerLabel.Margin.Size;
			this.centerLabel.ToolTipText = SR.GetString("ToolStripDesignerTemplateNodeLabelToolTip");
			this.centerLabel.MouseUp += this.CenterLabelClick;
			this.centerLabel.MouseEnter += this.CenterLabelMouseEnter;
			this.centerLabel.MouseMove += this.CenterLabelMouseMove;
			this.centerLabel.MouseLeave += this.CenterLabelMouseLeave;
			this._miniToolStrip.Items.AddRange(new ToolStripItem[]
			{
				this.centerLabel
			});
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000DF8D4 File Offset: 0x000DDAD4
		private void SetUpToolTemplateNode(ToolStripTemplateNode owner, string text, Image image, IComponent component)
		{
			this.addItemButton = new ToolStripSplitButton();
			this.addItemButton.AutoSize = false;
			this.addItemButton.Margin = new Padding(1);
			this.addItemButton.Size = this._miniToolStrip.DisplayRectangle.Size - this.addItemButton.Margin.Size;
			this.addItemButton.DropDownButtonWidth = ToolStripTemplateNode.MINITOOLSTRIP_DROPDOWN_BUTTON_WIDTH;
			this.addItemButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			if (component is StatusStrip)
			{
				this.addItemButton.ToolTipText = SR.GetString("ToolStripDesignerTemplateNodeSplitButtonStatusStripToolTip");
			}
			else
			{
				this.addItemButton.ToolTipText = SR.GetString("ToolStripDesignerTemplateNodeSplitButtonToolTip");
			}
			this.addItemButton.MouseDown += this.OnMouseDown;
			this.addItemButton.MouseMove += this.OnMouseMove;
			this.addItemButton.MouseUp += this.OnMouseUp;
			this.addItemButton.DropDownOpened += this.OnAddItemButtonDropDownOpened;
			this.contextMenu = ToolStripDesignerUtils.GetNewItemDropDown(component, null, new EventHandler(this.AddNewItemClick), false, component.Site, false);
			this.contextMenu.Text = "ItemSelectionMenu";
			this.contextMenu.Closed += this.OnContextMenuClosed;
			this.contextMenu.Opened += this.OnContextMenuOpened;
			this.addItemButton.DropDown = this.contextMenu;
			try
			{
				if (this.addItemButton.DropDownItems.Count > 0)
				{
					ItemTypeToolStripMenuItem defaultItem = (ItemTypeToolStripMenuItem)this.addItemButton.DropDownItems[0];
					this.addItemButton.ImageTransparentColor = Color.Lime;
					Bitmap bitmap = new Bitmap(BitmapSelector.GetResourceStream(typeof(ToolStripTemplateNode), "ToolStripTemplateNode.bmp"));
					if (DpiHelper.IsScalingRequired)
					{
						bitmap.MakeTransparent(Color.Lime);
						DpiHelper.ScaleBitmapLogicalToDevice(ref bitmap, 0);
					}
					this.addItemButton.Image = bitmap;
					this.addItemButton.DefaultItem = defaultItem;
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			this._miniToolStrip.Items.AddRange(new ToolStripItem[]
			{
				this.addItemButton
			});
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000DFB24 File Offset: 0x000DDD24
		private void SetupNewEditNode(ToolStripTemplateNode owner, string text, Image image, IComponent currentItem)
		{
			this.renderer = new ToolStripTemplateNode.MiniToolStripRenderer(owner);
			this._miniToolStrip = new ToolStripTemplateNode.TransparentToolStrip(owner);
			ToolStrip toolStrip = currentItem as ToolStrip;
			if (toolStrip != null)
			{
				this._miniToolStrip.RightToLeft = toolStrip.RightToLeft;
				toolStrip.RightToLeftChanged += this.OnRightToLeftChanged;
				this._miniToolStrip.Site = toolStrip.Site;
			}
			ToolStripDropDownItem toolStripDropDownItem = currentItem as ToolStripDropDownItem;
			if (toolStripDropDownItem != null)
			{
				this._miniToolStrip.RightToLeft = toolStripDropDownItem.RightToLeft;
				toolStripDropDownItem.RightToLeftChanged += this.OnRightToLeftChanged;
			}
			this._miniToolStrip.SuspendLayout();
			this._miniToolStrip.CanOverflow = false;
			this._miniToolStrip.Cursor = Cursors.Default;
			this._miniToolStrip.Dock = DockStyle.None;
			this._miniToolStrip.GripStyle = ToolStripGripStyle.Hidden;
			this._miniToolStrip.Name = "miniToolStrip";
			this._miniToolStrip.TabIndex = 0;
			this._miniToolStrip.Text = "miniToolStrip";
			this._miniToolStrip.Visible = true;
			this._miniToolStrip.Renderer = this.renderer;
			if (currentItem is MenuStrip || currentItem is ToolStripDropDownItem)
			{
				this.SetUpMenuTemplateNode(owner, text, image, currentItem);
			}
			else
			{
				this.SetUpToolTemplateNode(owner, text, image, currentItem);
			}
			this._miniToolStrip.MouseLeave += this.OnMouseLeave;
			this._miniToolStrip.ResumeLayout();
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000DFC8A File Offset: 0x000DDE8A
		internal void SetWidth(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				this._miniToolStrip.Width = this.centerLabel.Width + 2;
				return;
			}
			this.centerLabel.Text = text;
		}

		// Token: 0x04001A0A RID: 6666
		private const int GLYPHBORDER = 1;

		// Token: 0x04001A0B RID: 6667
		private const int GLYPHINSET = 2;

		// Token: 0x04001A0C RID: 6668
		private const int TOOLSTRIP_TEMPLATE_HEIGHT_ORIGINAL = 22;

		// Token: 0x04001A0D RID: 6669
		private const int TEMPLATE_HEIGHT_ORIGINAL = 19;

		// Token: 0x04001A0E RID: 6670
		private const int TOOLSTRIP_TEMPLATE_WIDTH_ORIGINAL = 92;

		// Token: 0x04001A0F RID: 6671
		private const int TEMPLATE_WIDTH_ORIGINAL = 31;

		// Token: 0x04001A10 RID: 6672
		private const int MINITOOLSTRIP_DROPDOWN_BUTTON_WIDTH_ORIGINAL = 11;

		// Token: 0x04001A11 RID: 6673
		private const int TEMPLATE_HOTREGION_WIDTH_ORIGINAL = 9;

		// Token: 0x04001A12 RID: 6674
		private const int MINITOOLSTRIP_TEXTBOX_WIDTH_ORIGINAL = 90;

		// Token: 0x04001A13 RID: 6675
		private static int TOOLSTRIP_TEMPLATE_HEIGHT = 22;

		// Token: 0x04001A14 RID: 6676
		private static int TEMPLATE_HEIGHT = 19;

		// Token: 0x04001A15 RID: 6677
		private static int TOOLSTRIP_TEMPLATE_WIDTH = 92;

		// Token: 0x04001A16 RID: 6678
		private static int TEMPLATE_WIDTH = 31;

		// Token: 0x04001A17 RID: 6679
		private static int MINITOOLSTRIP_DROPDOWN_BUTTON_WIDTH = 11;

		// Token: 0x04001A18 RID: 6680
		private static int TEMPLATE_HOTREGION_WIDTH = 9;

		// Token: 0x04001A19 RID: 6681
		private static int MINITOOLSTRIP_TEXTBOX_WIDTH = 90;

		// Token: 0x04001A1A RID: 6682
		private static bool isScalingInitialized = false;

		// Token: 0x04001A1B RID: 6683
		private IComponent component;

		// Token: 0x04001A1C RID: 6684
		private IDesigner _designer;

		// Token: 0x04001A1D RID: 6685
		private IDesignerHost _designerHost;

		// Token: 0x04001A1E RID: 6686
		private MenuCommand[] commands;

		// Token: 0x04001A1F RID: 6687
		private MenuCommand[] addCommands;

		// Token: 0x04001A20 RID: 6688
		private ToolStripTemplateNode.TransparentToolStrip _miniToolStrip;

		// Token: 0x04001A21 RID: 6689
		private ToolStripLabel centerLabel;

		// Token: 0x04001A22 RID: 6690
		private ToolStripSplitButton addItemButton;

		// Token: 0x04001A23 RID: 6691
		private ToolStripControlHost centerTextBox;

		// Token: 0x04001A24 RID: 6692
		internal bool ignoreFirstKeyUp;

		// Token: 0x04001A25 RID: 6693
		private Rectangle boundingRect;

		// Token: 0x04001A26 RID: 6694
		private bool inSituMode;

		// Token: 0x04001A27 RID: 6695
		private bool active;

		// Token: 0x04001A28 RID: 6696
		private ItemTypeToolStripMenuItem lastSelection;

		// Token: 0x04001A29 RID: 6697
		private ToolStripTemplateNode.MiniToolStripRenderer renderer;

		// Token: 0x04001A2A RID: 6698
		private Type itemType;

		// Token: 0x04001A2B RID: 6699
		private ToolStripKeyboardHandlingService toolStripKeyBoardService;

		// Token: 0x04001A2C RID: 6700
		private ISelectionService selectionService;

		// Token: 0x04001A2D RID: 6701
		private BehaviorService behaviorService;

		// Token: 0x04001A2E RID: 6702
		private DesignerToolStripControlHost controlHost;

		// Token: 0x04001A2F RID: 6703
		private ToolStripItem activeItem;

		// Token: 0x04001A30 RID: 6704
		private EventHandler onActivated;

		// Token: 0x04001A31 RID: 6705
		private EventHandler onClosed;

		// Token: 0x04001A32 RID: 6706
		private EventHandler onDeactivated;

		// Token: 0x04001A33 RID: 6707
		private MenuCommand oldUndoCommand;

		// Token: 0x04001A34 RID: 6708
		private MenuCommand oldRedoCommand;

		// Token: 0x04001A35 RID: 6709
		private NewItemsContextMenuStrip contextMenu;

		// Token: 0x04001A36 RID: 6710
		private Rectangle hotRegion;

		// Token: 0x04001A37 RID: 6711
		private bool imeModeSet;

		// Token: 0x04001A38 RID: 6712
		private DesignSurface _designSurface;

		// Token: 0x04001A39 RID: 6713
		private bool isSystemContextMenuDisplayed;

		// Token: 0x04001A3A RID: 6714
		private bool _isPopulated;

		// Token: 0x0200059D RID: 1437
		private class TemplateTextBox : TextBox
		{
			// Token: 0x06003358 RID: 13144 RVA: 0x00118950 File Offset: 0x00116B50
			public TemplateTextBox(ToolStripTemplateNode.TransparentToolStrip parent, ToolStripTemplateNode owner)
			{
				this.parent = parent;
				this.owner = owner;
				this.AutoSize = false;
				this.Multiline = false;
			}

			// Token: 0x06003359 RID: 13145 RVA: 0x00118974 File Offset: 0x00116B74
			private bool IsParentWindow(IntPtr hWnd)
			{
				return hWnd == this.parent.Handle;
			}

			// Token: 0x0600335A RID: 13146 RVA: 0x0011898C File Offset: 0x00116B8C
			protected override bool IsInputKey(Keys keyData)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys == Keys.Return)
				{
					this.owner.Commit(true, false);
					return true;
				}
				return base.IsInputKey(keyData);
			}

			// Token: 0x0600335B RID: 13147 RVA: 0x001189BC File Offset: 0x00116BBC
			protected override bool ProcessDialogKey(Keys keyData)
			{
				if (keyData == Keys.ProcessKey)
				{
					this.owner.IMEModeSet = true;
				}
				else
				{
					this.owner.IMEModeSet = false;
					this.owner.ignoreFirstKeyUp = false;
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x0600335C RID: 13148 RVA: 0x001189F4 File Offset: 0x00116BF4
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 8)
				{
					if (msg == 123)
					{
						this.owner.IsSystemContextMenuDisplayed = true;
						base.WndProc(ref m);
						this.owner.IsSystemContextMenuDisplayed = false;
						return;
					}
					base.WndProc(ref m);
				}
				else
				{
					base.WndProc(ref m);
					IntPtr wparam = m.WParam;
					if (!this.IsParentWindow(wparam))
					{
						this.owner.Commit(false, false);
						return;
					}
				}
			}

			// Token: 0x04002260 RID: 8800
			private ToolStripTemplateNode.TransparentToolStrip parent;

			// Token: 0x04002261 RID: 8801
			private ToolStripTemplateNode owner;

			// Token: 0x04002262 RID: 8802
			private const int IMEMODE = 229;
		}

		// Token: 0x0200059E RID: 1438
		public class TransparentToolStrip : ToolStrip
		{
			// Token: 0x0600335D RID: 13149 RVA: 0x00118A5F File Offset: 0x00116C5F
			public TransparentToolStrip(ToolStripTemplateNode owner)
			{
				this.owner = owner;
				this.currentItem = owner.component;
				base.TabStop = true;
				base.SetStyle(ControlStyles.Selectable, true);
				this.AutoSize = false;
			}

			// Token: 0x17000A07 RID: 2567
			// (get) Token: 0x0600335E RID: 13150 RVA: 0x00118A94 File Offset: 0x00116C94
			public ToolStripTemplateNode TemplateNode
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x0600335F RID: 13151 RVA: 0x00118A9C File Offset: 0x00116C9C
			private void CommitAndSelectNext(bool forward)
			{
				this.owner.Commit(false, true);
				if (this.owner.KeyboardService != null)
				{
					this.owner.KeyboardService.ProcessKeySelect(!forward, null);
				}
			}

			// Token: 0x06003360 RID: 13152 RVA: 0x00118AD0 File Offset: 0x00116CD0
			private ToolStripItem GetSelectedItem()
			{
				ToolStripItem result = null;
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (this.Items[i].Selected)
					{
						result = this.Items[i];
					}
				}
				return result;
			}

			// Token: 0x06003361 RID: 13153 RVA: 0x00118B16 File Offset: 0x00116D16
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			public override Size GetPreferredSize(Size proposedSize)
			{
				if (this.currentItem is ToolStripDropDownItem)
				{
					return new Size(base.Width, ToolStripTemplateNode.TOOLSTRIP_TEMPLATE_HEIGHT);
				}
				return new Size(base.Width, ToolStripTemplateNode.TEMPLATE_HEIGHT);
			}

			// Token: 0x06003362 RID: 13154 RVA: 0x00118B48 File Offset: 0x00116D48
			private bool ProcessTabKey(bool forward)
			{
				ToolStripItem selectedItem = this.GetSelectedItem();
				if (selectedItem is ToolStripControlHost)
				{
					this.CommitAndSelectNext(forward);
					return true;
				}
				return false;
			}

			// Token: 0x06003363 RID: 13155 RVA: 0x00118B70 File Offset: 0x00116D70
			protected override bool ProcessDialogKey(Keys keyData)
			{
				bool flag = false;
				if (this.owner.Active)
				{
					if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
					{
						Keys keys = keyData & Keys.KeyCode;
						if (keys == Keys.Tab)
						{
							flag = this.ProcessTabKey((keyData & Keys.Shift) == Keys.None);
						}
					}
					if (flag)
					{
						return flag;
					}
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x06003364 RID: 13156 RVA: 0x00118BC0 File Offset: 0x00116DC0
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
			{
				if (this.currentItem is ToolStripDropDownItem)
				{
					base.SetBoundsCore(x, y, ToolStripTemplateNode.TOOLSTRIP_TEMPLATE_WIDTH, ToolStripTemplateNode.TOOLSTRIP_TEMPLATE_HEIGHT, specified);
					return;
				}
				if (this.currentItem is MenuStrip)
				{
					base.SetBoundsCore(x, y, ToolStripTemplateNode.TOOLSTRIP_TEMPLATE_WIDTH, ToolStripTemplateNode.TEMPLATE_HEIGHT, specified);
					return;
				}
				base.SetBoundsCore(x, y, ToolStripTemplateNode.TEMPLATE_WIDTH, ToolStripTemplateNode.TEMPLATE_HEIGHT, specified);
			}

			// Token: 0x04002263 RID: 8803
			private ToolStripTemplateNode owner;

			// Token: 0x04002264 RID: 8804
			private IComponent currentItem;
		}

		// Token: 0x0200059F RID: 1439
		public class MiniToolStripRenderer : ToolStripSystemRenderer
		{
			// Token: 0x06003365 RID: 13157 RVA: 0x00118C28 File Offset: 0x00116E28
			public MiniToolStripRenderer(ToolStripTemplateNode owner)
			{
				this.owner = owner;
				this.selectedBorderColor = Color.FromArgb(46, 106, 197);
				this.defaultBorderColor = Color.FromArgb(171, 171, 171);
				this.dropDownMouseOverColor = Color.FromArgb(193, 210, 238);
				this.dropDownMouseDownColor = Color.FromArgb(152, 181, 226);
				this.toolStripBorderColor = Color.White;
			}

			// Token: 0x17000A08 RID: 2568
			// (get) Token: 0x06003366 RID: 13158 RVA: 0x00118CBA File Offset: 0x00116EBA
			// (set) Token: 0x06003367 RID: 13159 RVA: 0x00118CC2 File Offset: 0x00116EC2
			public int State
			{
				get
				{
					return this.state;
				}
				set
				{
					this.state = value;
				}
			}

			// Token: 0x06003368 RID: 13160 RVA: 0x00118CCC File Offset: 0x00116ECC
			private void DrawArrow(Graphics g, Rectangle bounds)
			{
				int width = bounds.Width;
				bounds.Width = width - 1;
				base.DrawArrow(new ToolStripArrowRenderEventArgs(g, null, bounds, SystemColors.ControlText, ArrowDirection.Down));
			}

			// Token: 0x06003369 RID: 13161 RVA: 0x00118D00 File Offset: 0x00116F00
			private void DrawDropDown(Graphics g, Rectangle bounds, int state)
			{
				switch (state)
				{
				case 4:
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, Color.White, this.defaultBorderColor, LinearGradientMode.Vertical))
					{
						g.FillRectangle(linearGradientBrush, bounds);
						goto IL_87;
					}
					break;
				case 5:
					break;
				case 6:
					goto IL_62;
				default:
					goto IL_87;
				}
				using (SolidBrush solidBrush = new SolidBrush(this.dropDownMouseOverColor))
				{
					g.FillRectangle(solidBrush, this.hotRegion);
					goto IL_87;
				}
				IL_62:
				using (SolidBrush solidBrush2 = new SolidBrush(this.dropDownMouseDownColor))
				{
					g.FillRectangle(solidBrush2, this.hotRegion);
				}
				IL_87:
				this.DrawArrow(g, bounds);
			}

			// Token: 0x0600336A RID: 13162 RVA: 0x00118DC4 File Offset: 0x00116FC4
			protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
			{
				if (this.owner.component is MenuStrip || this.owner.component is ToolStripDropDownItem)
				{
					Graphics graphics = e.Graphics;
					graphics.Clear(this.toolStripBorderColor);
					return;
				}
				base.OnRenderToolStripBackground(e);
			}

			// Token: 0x0600336B RID: 13163 RVA: 0x00118E10 File Offset: 0x00117010
			protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
			{
				Graphics graphics = e.Graphics;
				Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
				Pen pen = new Pen(this.toolStripBorderColor);
				Rectangle rect = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				graphics.DrawRectangle(pen, rect);
				pen.Dispose();
			}

			// Token: 0x0600336C RID: 13164 RVA: 0x00118E7C File Offset: 0x0011707C
			protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
			{
				base.OnRenderLabelBackground(e);
				ToolStripItem item = e.Item;
				Graphics graphics = e.Graphics;
				Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
				Rectangle rect = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				Pen pen = new Pen(this.defaultBorderColor);
				if (this.state == 1)
				{
					using (SolidBrush solidBrush = new SolidBrush(this.toolStripBorderColor))
					{
						graphics.FillRectangle(solidBrush, rect);
					}
					if (this.owner.EditorToolStrip.RightToLeft == RightToLeft.Yes)
					{
						this.hotRegion = new Rectangle(rectangle.Left + 2, rectangle.Top + 2, ToolStripTemplateNode.TEMPLATE_HOTREGION_WIDTH, rectangle.Bottom - 4);
					}
					else
					{
						this.hotRegion = new Rectangle(rectangle.Right - ToolStripTemplateNode.TEMPLATE_HOTREGION_WIDTH - 2, rectangle.Top + 2, ToolStripTemplateNode.TEMPLATE_HOTREGION_WIDTH, rectangle.Bottom - 4);
					}
					this.owner.HotRegion = this.hotRegion;
					pen.Color = Color.Black;
					item.ForeColor = this.defaultBorderColor;
					graphics.DrawRectangle(pen, rect);
				}
				if (this.state == 4)
				{
					if (this.owner.EditorToolStrip.RightToLeft == RightToLeft.Yes)
					{
						this.hotRegion = new Rectangle(rectangle.Left + 2, rectangle.Top + 2, ToolStripTemplateNode.TEMPLATE_HOTREGION_WIDTH, rectangle.Bottom - 4);
					}
					else
					{
						this.hotRegion = new Rectangle(rectangle.Right - ToolStripTemplateNode.TEMPLATE_HOTREGION_WIDTH - 2, rectangle.Top + 2, ToolStripTemplateNode.TEMPLATE_HOTREGION_WIDTH, rectangle.Bottom - 4);
					}
					this.owner.HotRegion = this.hotRegion;
					graphics.Clear(this.toolStripBorderColor);
					this.DrawDropDown(graphics, this.hotRegion, this.state);
					pen.Color = Color.Black;
					pen.DashStyle = DashStyle.Dot;
					graphics.DrawRectangle(pen, rect);
				}
				if (this.state == 5)
				{
					graphics.Clear(this.toolStripBorderColor);
					this.DrawDropDown(graphics, this.hotRegion, this.state);
					pen.Color = Color.Black;
					pen.DashStyle = DashStyle.Dot;
					item.ForeColor = this.defaultBorderColor;
					graphics.DrawRectangle(pen, rect);
				}
				if (this.state == 6)
				{
					graphics.Clear(this.toolStripBorderColor);
					this.DrawDropDown(graphics, this.hotRegion, this.state);
					pen.Color = Color.Black;
					item.ForeColor = this.defaultBorderColor;
					graphics.DrawRectangle(pen, rect);
				}
				if (this.state == 0)
				{
					graphics.Clear(this.toolStripBorderColor);
					graphics.DrawRectangle(pen, rect);
					item.ForeColor = this.defaultBorderColor;
				}
				pen.Dispose();
			}

			// Token: 0x0600336D RID: 13165 RVA: 0x0011915C File Offset: 0x0011735C
			protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
			{
				Graphics graphics = e.Graphics;
				ToolStripSplitButton toolStripSplitButton = e.Item as ToolStripSplitButton;
				if (toolStripSplitButton != null)
				{
					Rectangle dropDownButtonBounds = toolStripSplitButton.DropDownButtonBounds;
					using (Pen pen = new Pen(this.toolStripBorderColor))
					{
						graphics.DrawLine(pen, dropDownButtonBounds.Left, dropDownButtonBounds.Top + 1, dropDownButtonBounds.Left, dropDownButtonBounds.Bottom - 1);
					}
					Rectangle rectangle = new Rectangle(Point.Empty, toolStripSplitButton.Size);
					bool flag = false;
					if (toolStripSplitButton.DropDownButtonPressed)
					{
						this.state = 0;
						Rectangle rect = new Rectangle(dropDownButtonBounds.Left + 1, dropDownButtonBounds.Top, dropDownButtonBounds.Right, dropDownButtonBounds.Bottom);
						using (SolidBrush solidBrush = new SolidBrush(this.dropDownMouseDownColor))
						{
							graphics.FillRectangle(solidBrush, rect);
						}
						flag = true;
					}
					else if (this.state == 2)
					{
						using (SolidBrush solidBrush2 = new SolidBrush(this.dropDownMouseOverColor))
						{
							graphics.FillRectangle(solidBrush2, toolStripSplitButton.ButtonBounds);
						}
						flag = true;
					}
					else if (this.state == 3)
					{
						Rectangle rect2 = new Rectangle(dropDownButtonBounds.Left + 1, dropDownButtonBounds.Top, dropDownButtonBounds.Right, dropDownButtonBounds.Bottom);
						using (SolidBrush solidBrush3 = new SolidBrush(this.dropDownMouseOverColor))
						{
							graphics.FillRectangle(solidBrush3, rect2);
						}
						flag = true;
					}
					else if (this.state == 1)
					{
						flag = true;
					}
					Pen pen2;
					if (flag)
					{
						pen2 = new Pen(this.selectedBorderColor);
					}
					else
					{
						pen2 = new Pen(this.defaultBorderColor);
					}
					Rectangle rect3 = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					graphics.DrawRectangle(pen2, rect3);
					pen2.Dispose();
					base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, toolStripSplitButton, toolStripSplitButton.DropDownButtonBounds, SystemColors.ControlText, ArrowDirection.Down));
				}
			}

			// Token: 0x04002265 RID: 8805
			private int state;

			// Token: 0x04002266 RID: 8806
			private Color selectedBorderColor;

			// Token: 0x04002267 RID: 8807
			private Color defaultBorderColor;

			// Token: 0x04002268 RID: 8808
			private Color dropDownMouseOverColor;

			// Token: 0x04002269 RID: 8809
			private Color dropDownMouseDownColor;

			// Token: 0x0400226A RID: 8810
			private Color toolStripBorderColor;

			// Token: 0x0400226B RID: 8811
			private ToolStripTemplateNode owner;

			// Token: 0x0400226C RID: 8812
			private Rectangle hotRegion = Rectangle.Empty;
		}
	}
}
