using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000356 RID: 854
	internal class ToolStripDropDownDesigner : ComponentDesigner
	{
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x000D2315 File Offset: 0x000D0515
		// (set) Token: 0x06002242 RID: 8770 RVA: 0x000D232C File Offset: 0x000D052C
		private bool AutoClose
		{
			get
			{
				return (bool)base.ShadowProperties["AutoClose"];
			}
			set
			{
				base.ShadowProperties["AutoClose"] = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x0009EF25 File Offset: 0x0009D125
		// (set) Token: 0x06002244 RID: 8772 RVA: 0x0009EF3C File Offset: 0x0009D13C
		private bool AllowDrop
		{
			get
			{
				return (bool)base.ShadowProperties["AllowDrop"];
			}
			set
			{
				base.ShadowProperties["AllowDrop"] = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000D2344 File Offset: 0x000D0544
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				ContextMenuStripActionList contextMenuStripActionList = new ContextMenuStripActionList(this);
				if (contextMenuStripActionList != null)
				{
					designerActionListCollection.Add(contextMenuStripActionList);
				}
				DesignerVerbCollection verbs = this.Verbs;
				if (verbs != null && verbs.Count != 0)
				{
					DesignerVerb[] array = new DesignerVerb[verbs.Count];
					verbs.CopyTo(array, 0);
					designerActionListCollection.Add(new DesignerActionVerbList(array));
				}
				return designerActionListCollection;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x000D23A9 File Offset: 0x000D05A9
		public override ICollection AssociatedComponents
		{
			get
			{
				return ((ToolStrip)base.Component).Items;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x000D23BB File Offset: 0x000D05BB
		public ToolStripMenuItem DesignerMenuItem
		{
			get
			{
				return this.menuItem;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x000D23C3 File Offset: 0x000D05C3
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x000D23CE File Offset: 0x000D05CE
		internal bool EditingCollection
		{
			get
			{
				return this._editingCollection > 0U;
			}
			set
			{
				if (value)
				{
					this._editingCollection += 1U;
					return;
				}
				this._editingCollection -= 1U;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x000D23F0 File Offset: 0x000D05F0
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				if (base.InheritanceAttribute == InheritanceAttribute.Inherited)
				{
					return InheritanceAttribute.InheritedReadOnly;
				}
				return base.InheritanceAttribute;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x000D240B File Offset: 0x000D060B
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x000D2418 File Offset: 0x000D0618
		private RightToLeft RightToLeft
		{
			get
			{
				return this.dropDown.RightToLeft;
			}
			set
			{
				if (this.menuItem != null && this.designMenu != null && value != this.RightToLeft)
				{
					Rectangle rectangle = Rectangle.Empty;
					try
					{
						rectangle = this.dropDown.Bounds;
						this.menuItem.HideDropDown();
						this.designMenu.RightToLeft = value;
						this.dropDown.RightToLeft = value;
					}
					finally
					{
						BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
						if (behaviorService != null && rectangle != Rectangle.Empty)
						{
							behaviorService.Invalidate(rectangle);
						}
						ToolStripMenuItemDesigner toolStripMenuItemDesigner = (ToolStripMenuItemDesigner)this.host.GetDesigner(this.menuItem);
						if (toolStripMenuItemDesigner != null)
						{
							toolStripMenuItemDesigner.InitializeDropDown();
						}
					}
				}
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000D24DC File Offset: 0x000D06DC
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x000D25E0 File Offset: 0x000D07E0
		private string SettingsKey
		{
			get
			{
				if (string.IsNullOrEmpty((string)base.ShadowProperties["SettingsKey"]))
				{
					IPersistComponentSettings persistComponentSettings = base.Component as IPersistComponentSettings;
					if (persistComponentSettings != null && this.host != null)
					{
						if (persistComponentSettings.SettingsKey == null)
						{
							IComponent rootComponent = this.host.RootComponent;
							if (rootComponent != null && rootComponent != persistComponentSettings)
							{
								base.ShadowProperties["SettingsKey"] = string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[]
								{
									rootComponent.Site.Name,
									base.Component.Site.Name
								});
							}
							else
							{
								base.ShadowProperties["SettingsKey"] = base.Component.Site.Name;
							}
						}
						persistComponentSettings.SettingsKey = (base.ShadowProperties["SettingsKey"] as string);
						return persistComponentSettings.SettingsKey;
					}
				}
				return base.ShadowProperties["SettingsKey"] as string;
			}
			set
			{
				base.ShadowProperties["SettingsKey"] = value;
				IPersistComponentSettings persistComponentSettings = base.Component as IPersistComponentSettings;
				if (persistComponentSettings != null)
				{
					persistComponentSettings.SettingsKey = value;
				}
			}
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000D2614 File Offset: 0x000D0814
		private void AddSelectionGlyphs(SelectionManager selMgr, ISelectionService selectionService)
		{
			ICollection selectedComponents = selectionService.GetSelectedComponents();
			GlyphCollection glyphCollection = new GlyphCollection();
			foreach (object obj in selectedComponents)
			{
				ToolStripItem toolStripItem = obj as ToolStripItem;
				if (toolStripItem != null)
				{
					ToolStripItemDesigner toolStripItemDesigner = (ToolStripItemDesigner)this.host.GetDesigner(toolStripItem);
					if (toolStripItemDesigner != null)
					{
						toolStripItemDesigner.GetGlyphs(ref glyphCollection, new ResizeBehavior(toolStripItem.Site));
					}
				}
			}
			if (glyphCollection.Count > 0)
			{
				selMgr.SelectionGlyphAdorner.Glyphs.AddRange(glyphCollection);
			}
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000D26C0 File Offset: 0x000D08C0
		internal void AddSelectionGlyphs()
		{
			SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
			if (selectionManager != null)
			{
				this.AddSelectionGlyphs(selectionManager, this.selSvc);
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000D26F4 File Offset: 0x000D08F4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.selSvc != null)
				{
					this.selSvc.SelectionChanged -= this.OnSelectionChanged;
					this.selSvc.SelectionChanging -= this.OnSelectionChanging;
				}
				this.DisposeMenu();
				if (this.designMenu != null)
				{
					this.designMenu.Dispose();
					this.designMenu = null;
				}
				if (this.dummyToolStripGlyph != null)
				{
					this.dummyToolStripGlyph = null;
				}
				if (this.undoEngine != null)
				{
					this.undoEngine.Undone -= this.OnUndone;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000D2794 File Offset: 0x000D0994
		private void DisposeMenu()
		{
			this.HideMenu();
			Control control = this.host.RootComponent as Control;
			if (control != null)
			{
				if (this.designMenu != null)
				{
					control.Controls.Remove(this.designMenu);
				}
				if (this.menuItem != null)
				{
					if (this.nestedContainer != null)
					{
						this.nestedContainer.Dispose();
						this.nestedContainer = null;
					}
					this.menuItem.Dispose();
					this.menuItem = null;
				}
			}
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000D2808 File Offset: 0x000D0A08
		private void HideMenu()
		{
			if (this.menuItem == null)
			{
				return;
			}
			if (this.parentMenu != null && this.parentFormDesigner != null)
			{
				this.parentFormDesigner.Menu = this.parentMenu;
			}
			this.selected = false;
			Control control = this.host.RootComponent as Control;
			if (control != null)
			{
				this.menuItem.DropDown.AutoClose = true;
				this.menuItem.HideDropDown();
				this.menuItem.Visible = false;
				this.designMenu.Visible = false;
				ToolStripAdornerWindowService toolStripAdornerWindowService = (ToolStripAdornerWindowService)this.GetService(typeof(ToolStripAdornerWindowService));
				if (toolStripAdornerWindowService != null)
				{
					toolStripAdornerWindowService.Invalidate();
				}
				BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
				if (behaviorService != null)
				{
					if (this.dummyToolStripGlyph != null)
					{
						SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
						if (selectionManager != null)
						{
							if (selectionManager.BodyGlyphAdorner.Glyphs.Contains(this.dummyToolStripGlyph))
							{
								selectionManager.BodyGlyphAdorner.Glyphs.Remove(this.dummyToolStripGlyph);
							}
							selectionManager.Refresh();
						}
					}
					this.dummyToolStripGlyph = null;
				}
				if (this.menuItem != null)
				{
					ToolStripMenuItemDesigner toolStripMenuItemDesigner = this.host.GetDesigner(this.menuItem) as ToolStripMenuItemDesigner;
					if (toolStripMenuItemDesigner != null)
					{
						toolStripMenuItemDesigner.UnHookEvents();
						toolStripMenuItemDesigner.RemoveTypeHereNode(this.menuItem);
					}
				}
			}
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000D2960 File Offset: 0x000D0B60
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if ((ToolStripKeyboardHandlingService)this.GetService(typeof(ToolStripKeyboardHandlingService)) == null)
			{
				ToolStripKeyboardHandlingService toolStripKeyboardHandlingService = new ToolStripKeyboardHandlingService(component.Site);
			}
			if ((ISupportInSituService)this.GetService(typeof(ISupportInSituService)) == null)
			{
				ISupportInSituService supportInSituService = new ToolStripInSituService(base.Component.Site);
			}
			this.dropDown = (ToolStripDropDown)base.Component;
			this.dropDown.Visible = false;
			this.AutoClose = this.dropDown.AutoClose;
			this.AllowDrop = this.dropDown.AllowDrop;
			this.selSvc = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (this.selSvc != null)
			{
				if (this.host != null && !this.host.Loading)
				{
					this.selSvc.SetSelectedComponents(new IComponent[]
					{
						this.host.RootComponent
					}, SelectionTypes.Replace);
				}
				this.selSvc.SelectionChanging += this.OnSelectionChanging;
				this.selSvc.SelectionChanged += this.OnSelectionChanged;
			}
			this.designMenu = new MenuStrip();
			this.designMenu.Visible = false;
			this.designMenu.AutoSize = false;
			this.designMenu.Dock = DockStyle.Top;
			if (DpiHelper.IsScalingRequired)
			{
				this.designMenu.Height = DpiHelper.LogicalToDeviceUnitsY(this.designMenu.Height);
			}
			Control control = this.host.RootComponent as Control;
			if (control != null)
			{
				this.menuItem = new ToolStripMenuItem();
				this.menuItem.BackColor = SystemColors.Window;
				this.menuItem.Name = base.Component.Site.Name;
				this.menuItem.Text = ((this.dropDown != null) ? this.dropDown.GetType().Name : this.menuItem.Name);
				this.designMenu.Items.Add(this.menuItem);
				control.Controls.Add(this.designMenu);
				this.designMenu.SendToBack();
				this.nestedContainer = (this.GetService(typeof(INestedContainer)) as INestedContainer);
				if (this.nestedContainer != null)
				{
					this.nestedContainer.Add(this.menuItem, "ContextMenuStrip");
				}
			}
			new EditorServiceContext(this, TypeDescriptor.GetProperties(base.Component)["Items"], SR.GetString("ToolStripItemCollectionEditorVerb"));
			if (this.undoEngine == null)
			{
				this.undoEngine = (this.GetService(typeof(UndoEngine)) as UndoEngine);
				if (this.undoEngine != null)
				{
					this.undoEngine.Undone += this.OnUndone;
				}
			}
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000D2C48 File Offset: 0x000D0E48
		private bool IsContextMenuStripItemSelected(ISelectionService selectionService)
		{
			bool result = false;
			if (this.menuItem == null)
			{
				return result;
			}
			ToolStripDropDown toolStripDropDown = null;
			IComponent component = (IComponent)selectionService.PrimarySelection;
			if (component == null && this.dropDown.Visible)
			{
				ToolStripKeyboardHandlingService toolStripKeyboardHandlingService = (ToolStripKeyboardHandlingService)this.GetService(typeof(ToolStripKeyboardHandlingService));
				if (toolStripKeyboardHandlingService != null)
				{
					component = (IComponent)toolStripKeyboardHandlingService.SelectedDesignerControl;
				}
			}
			if (component is ToolStripDropDownItem)
			{
				ToolStripDropDownItem toolStripDropDownItem = component as ToolStripDropDownItem;
				if (toolStripDropDownItem != null && toolStripDropDownItem == this.menuItem)
				{
					toolStripDropDown = this.menuItem.DropDown;
				}
				else
				{
					ToolStripMenuItemDesigner toolStripMenuItemDesigner = (ToolStripMenuItemDesigner)this.host.GetDesigner(component);
					if (toolStripMenuItemDesigner != null)
					{
						toolStripDropDown = toolStripMenuItemDesigner.GetFirstDropDown((ToolStripDropDownItem)component);
					}
				}
			}
			else if (component is ToolStripItem)
			{
				ToolStripDropDown toolStripDropDown2 = ((ToolStripItem)component).GetCurrentParent() as ToolStripDropDown;
				if (toolStripDropDown2 == null)
				{
					toolStripDropDown2 = (((ToolStripItem)component).Owner as ToolStripDropDown);
				}
				if (toolStripDropDown2 != null && toolStripDropDown2.Visible)
				{
					ToolStripItem ownerItem = toolStripDropDown2.OwnerItem;
					if (ownerItem != null && ownerItem == this.menuItem)
					{
						toolStripDropDown = this.menuItem.DropDown;
					}
					else
					{
						ToolStripMenuItemDesigner toolStripMenuItemDesigner2 = (ToolStripMenuItemDesigner)this.host.GetDesigner(ownerItem);
						if (toolStripMenuItemDesigner2 != null)
						{
							toolStripDropDown = toolStripMenuItemDesigner2.GetFirstDropDown((ToolStripDropDownItem)ownerItem);
						}
					}
				}
			}
			if (toolStripDropDown != null)
			{
				ToolStripItem ownerItem2 = toolStripDropDown.OwnerItem;
				if (ownerItem2 == this.menuItem)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000D2DA4 File Offset: 0x000D0FA4
		private void OnSelectionChanging(object sender, EventArgs e)
		{
			ISelectionService selectionService = (ISelectionService)sender;
			bool flag = this.IsContextMenuStripItemSelected(selectionService) || base.Component.Equals(selectionService.PrimarySelection);
			if (this.selected && !flag)
			{
				this.HideMenu();
			}
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000D2DE8 File Offset: 0x000D0FE8
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			if (base.Component == null || this.menuItem == null)
			{
				return;
			}
			ISelectionService selectionService = (ISelectionService)sender;
			if (selectionService.GetComponentSelected(this.menuItem))
			{
				selectionService.SetSelectedComponents(new IComponent[]
				{
					base.Component
				}, SelectionTypes.Replace);
			}
			if (base.Component.Equals(selectionService.PrimarySelection) && this.selected)
			{
				return;
			}
			bool flag = this.IsContextMenuStripItemSelected(selectionService) || base.Component.Equals(selectionService.PrimarySelection);
			if (flag)
			{
				if (!this.dropDown.Visible)
				{
					this.ShowMenu();
				}
				SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
				if (selectionManager != null)
				{
					if (this.dummyToolStripGlyph != null)
					{
						selectionManager.BodyGlyphAdorner.Glyphs.Insert(0, this.dummyToolStripGlyph);
					}
					this.AddSelectionGlyphs(selectionManager, selectionService);
				}
			}
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000D2EC4 File Offset: 0x000D10C4
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"AutoClose",
				"SettingsKey",
				"RightToLeft",
				"AllowDrop"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(ToolStripDropDownDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000D2F40 File Offset: 0x000D1140
		public void ResetSettingsKey()
		{
			IPersistComponentSettings persistComponentSettings = base.Component as IPersistComponentSettings;
			if (persistComponentSettings != null)
			{
				this.SettingsKey = null;
			}
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000D2F63 File Offset: 0x000D1163
		private void ResetAutoClose()
		{
			base.ShadowProperties["AutoClose"] = true;
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000D2F7B File Offset: 0x000D117B
		private void RestoreAutoClose()
		{
			this.dropDown.AutoClose = (bool)base.ShadowProperties["AutoClose"];
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000D2F9D File Offset: 0x000D119D
		private void ResetAllowDrop()
		{
			base.ShadowProperties["AllowDrop"] = false;
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000D2FB5 File Offset: 0x000D11B5
		private void RestoreAllowDrop()
		{
			this.dropDown.AutoClose = (bool)base.ShadowProperties["AllowDrop"];
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000D2FD7 File Offset: 0x000D11D7
		private void ResetRightToLeft()
		{
			this.RightToLeft = RightToLeft.No;
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000D2FE0 File Offset: 0x000D11E0
		public void ShowMenu()
		{
			int num = this.dropDown.Items.Count - 1;
			if (num >= 0)
			{
				this.ShowMenu(this.dropDown.Items[num]);
				return;
			}
			this.ShowMenu(null);
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000D3024 File Offset: 0x000D1224
		public void ShowMenu(ToolStripItem selectedItem)
		{
			if (this.menuItem == null)
			{
				return;
			}
			Control parent = this.designMenu.Parent;
			Form form = parent as Form;
			if (form != null)
			{
				this.parentFormDesigner = (this.host.GetDesigner(form) as FormDocumentDesigner);
				if (this.parentFormDesigner != null && this.parentFormDesigner.Menu != null)
				{
					this.parentMenu = this.parentFormDesigner.Menu;
					this.parentFormDesigner.Menu = null;
				}
			}
			this.selected = true;
			this.designMenu.Visible = true;
			this.designMenu.BringToFront();
			this.menuItem.Visible = true;
			if (this.currentParent != null && this.currentParent != this.menuItem)
			{
				ToolStripMenuItemDesigner toolStripMenuItemDesigner = this.host.GetDesigner(this.currentParent) as ToolStripMenuItemDesigner;
				if (toolStripMenuItemDesigner != null)
				{
					toolStripMenuItemDesigner.RemoveTypeHereNode(this.currentParent);
				}
			}
			this.menuItem.DropDown = this.dropDown;
			this.menuItem.DropDown.OwnerItem = this.menuItem;
			if (this.dropDown.Items.Count > 0)
			{
				ToolStripItem[] array = new ToolStripItem[this.dropDown.Items.Count];
				this.dropDown.Items.CopyTo(array, 0);
				foreach (ToolStripItem toolStripItem in array)
				{
					if (toolStripItem is DesignerToolStripControlHost)
					{
						this.dropDown.Items.Remove(toolStripItem);
					}
				}
			}
			ToolStripMenuItemDesigner toolStripMenuItemDesigner2 = (ToolStripMenuItemDesigner)this.host.GetDesigner(this.menuItem);
			BehaviorService behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
			if (behaviorService != null)
			{
				if (toolStripMenuItemDesigner2 != null && parent != null)
				{
					Rectangle parentBounds = behaviorService.ControlRectInAdornerWindow(parent);
					Rectangle itemBounds = behaviorService.ControlRectInAdornerWindow(this.designMenu);
					if (ToolStripDesigner.IsGlyphTotallyVisible(itemBounds, parentBounds))
					{
						toolStripMenuItemDesigner2.InitializeDropDown();
					}
				}
				if (this.dummyToolStripGlyph == null)
				{
					Point pos = behaviorService.ControlToAdornerWindow(this.designMenu);
					Rectangle bounds = this.designMenu.Bounds;
					bounds.Offset(pos);
					this.dummyToolStripGlyph = new ControlBodyGlyph(bounds, Cursor.Current, this.menuItem, new ToolStripDropDownDesigner.ContextMenuStripBehavior(this.menuItem));
					SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
					if (selectionManager != null)
					{
						selectionManager.BodyGlyphAdorner.Glyphs.Insert(0, this.dummyToolStripGlyph);
					}
				}
				if (selectedItem != null)
				{
					ToolStripKeyboardHandlingService toolStripKeyboardHandlingService = (ToolStripKeyboardHandlingService)this.GetService(typeof(ToolStripKeyboardHandlingService));
					if (toolStripKeyboardHandlingService != null)
					{
						toolStripKeyboardHandlingService.SelectedDesignerControl = selectedItem;
					}
				}
			}
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000D32A8 File Offset: 0x000D14A8
		private bool ShouldSerializeSettingsKey()
		{
			IPersistComponentSettings persistComponentSettings = base.Component as IPersistComponentSettings;
			return persistComponentSettings != null && persistComponentSettings.SaveSettings && this.SettingsKey != null;
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000D32D8 File Offset: 0x000D14D8
		private bool ShouldSerializeAutoClose()
		{
			bool flag = (bool)base.ShadowProperties["AutoClose"];
			return !flag;
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000D32FF File Offset: 0x000D14FF
		private bool ShouldSerializeAllowDrop()
		{
			return this.AllowDrop;
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000D3307 File Offset: 0x000D1507
		private bool ShouldSerializeRightToLeft()
		{
			return this.RightToLeft > RightToLeft.No;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000D3312 File Offset: 0x000D1512
		private void OnUndone(object source, EventArgs e)
		{
			if (this.selSvc != null && base.Component.Equals(this.selSvc.PrimarySelection))
			{
				this.HideMenu();
				this.ShowMenu();
			}
		}

		// Token: 0x04001998 RID: 6552
		private ISelectionService selSvc;

		// Token: 0x04001999 RID: 6553
		private MenuStrip designMenu;

		// Token: 0x0400199A RID: 6554
		private ToolStripMenuItem menuItem;

		// Token: 0x0400199B RID: 6555
		private IDesignerHost host;

		// Token: 0x0400199C RID: 6556
		private ToolStripDropDown dropDown;

		// Token: 0x0400199D RID: 6557
		private bool selected;

		// Token: 0x0400199E RID: 6558
		private ControlBodyGlyph dummyToolStripGlyph;

		// Token: 0x0400199F RID: 6559
		private uint _editingCollection;

		// Token: 0x040019A0 RID: 6560
		private MainMenu parentMenu;

		// Token: 0x040019A1 RID: 6561
		private FormDocumentDesigner parentFormDesigner;

		// Token: 0x040019A2 RID: 6562
		internal ToolStripMenuItem currentParent;

		// Token: 0x040019A3 RID: 6563
		private INestedContainer nestedContainer;

		// Token: 0x040019A4 RID: 6564
		private UndoEngine undoEngine;

		// Token: 0x02000599 RID: 1433
		internal class ContextMenuStripBehavior : Behavior
		{
			// Token: 0x0600334C RID: 13132 RVA: 0x0011845C File Offset: 0x0011665C
			internal ContextMenuStripBehavior(ToolStripMenuItem menuItem)
			{
				this.item = menuItem;
			}

			// Token: 0x0600334D RID: 13133 RVA: 0x0011846B File Offset: 0x0011666B
			public override bool OnMouseUp(Glyph g, MouseButtons button)
			{
				return button == MouseButtons.Left;
			}

			// Token: 0x0400225B RID: 8795
			private ToolStripMenuItem item;
		}
	}
}
