using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000330 RID: 816
	internal class SplitContainerDesigner : ParentControlDesigner
	{
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x000C369C File Offset: 0x000C189C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				SplitContainerDesigner.OrientationActionList value = new SplitContainerDesigner.OrientationActionList(this);
				designerActionListCollection.Add(value);
				return designerActionListCollection;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool AllowControlLasso
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x000C36BF File Offset: 0x000C18BF
		protected override bool DrawGrid
		{
			get
			{
				return !this.disableDrawGrid && base.DrawGrid;
			}
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000C36D1 File Offset: 0x000C18D1
		protected override Control GetParentForComponent(IComponent component)
		{
			return this.splitterPanel1;
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x000C36DC File Offset: 0x000C18DC
		public override IList SnapLines
		{
			get
			{
				return base.SnapLinesInternal() as ArrayList;
			}
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000C36F6 File Offset: 0x000C18F6
		public override int NumberOfInternalControlDesigners()
		{
			return SplitContainerDesigner.numberOfSplitterPanels;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000C3700 File Offset: 0x000C1900
		public override ControlDesigner InternalControlDesigner(int internalControlIndex)
		{
			SplitterPanel component;
			if (internalControlIndex != 0)
			{
				if (internalControlIndex != 1)
				{
					return null;
				}
				component = this.splitterPanel2;
			}
			else
			{
				component = this.splitterPanel1;
			}
			return this.designerHost.GetDesigner(component) as ControlDesigner;
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x000C373B File Offset: 0x000C193B
		// (set) Token: 0x0600203C RID: 8252 RVA: 0x000C3744 File Offset: 0x000C1944
		internal SplitterPanel Selected
		{
			get
			{
				return this.selectedPanel;
			}
			set
			{
				if (this.selectedPanel != null)
				{
					SplitterPanelDesigner splitterPanelDesigner = (SplitterPanelDesigner)this.designerHost.GetDesigner(this.selectedPanel);
					splitterPanelDesigner.Selected = false;
				}
				if (value != null)
				{
					SplitterPanelDesigner splitterPanelDesigner2 = (SplitterPanelDesigner)this.designerHost.GetDesigner(value);
					this.selectedPanel = value;
					splitterPanelDesigner2.Selected = true;
					return;
				}
				if (this.selectedPanel != null)
				{
					SplitterPanelDesigner splitterPanelDesigner3 = (SplitterPanelDesigner)this.designerHost.GetDesigner(this.selectedPanel);
					this.selectedPanel = null;
					splitterPanelDesigner3.Selected = false;
				}
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x000C37C8 File Offset: 0x000C19C8
		public override ICollection AssociatedComponents
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.splitContainer.Controls)
				{
					SplitterPanel splitterPanel = (SplitterPanel)obj;
					foreach (object obj2 in splitterPanel.Controls)
					{
						Control value = (Control)obj2;
						arrayList.Add(value);
					}
				}
				return arrayList;
			}
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000C3878 File Offset: 0x000C1A78
		protected override void OnDragEnter(DragEventArgs de)
		{
			de.Effect = DragDropEffects.None;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000C3884 File Offset: 0x000C1A84
		protected override IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			if (this.Selected == null)
			{
				this.Selected = this.splitterPanel1;
			}
			SplitterPanelDesigner toInvoke = (SplitterPanelDesigner)this.designerHost.GetDesigner(this.Selected);
			ParentControlDesigner.InvokeCreateTool(toInvoke, tool);
			return null;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000C38C4 File Offset: 0x000C1AC4
		protected override void Dispose(bool disposing)
		{
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SelectionChanged -= this.OnSelectionChanged;
			}
			this.splitContainer.MouseDown -= this.OnSplitContainer;
			this.splitContainer.SplitterMoved -= this.OnSplitterMoved;
			this.splitContainer.SplitterMoving -= this.OnSplitterMoving;
			this.splitContainer.DoubleClick -= this.OnSplitContainerDoubleClick;
			base.Dispose(disposing);
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000C395F File Offset: 0x000C1B5F
		protected override bool GetHitTest(Point point)
		{
			return this.InheritanceAttribute != InheritanceAttribute.InheritedReadOnly && this.splitContainerSelected;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000C3978 File Offset: 0x000C1B78
		protected override ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
		{
			SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
			if (selectionManager != null)
			{
				Rectangle bounds = base.BehaviorService.ControlRectInAdornerWindow(this.splitterPanel1);
				SplitterPanelDesigner splitterPanelDesigner = this.designerHost.GetDesigner(this.splitterPanel1) as SplitterPanelDesigner;
				this.OnSetCursor();
				if (splitterPanelDesigner != null)
				{
					ControlBodyGlyph value = new ControlBodyGlyph(bounds, Cursor.Current, this.splitterPanel1, splitterPanelDesigner);
					selectionManager.BodyGlyphAdorner.Glyphs.Add(value);
				}
				bounds = base.BehaviorService.ControlRectInAdornerWindow(this.splitterPanel2);
				splitterPanelDesigner = (this.designerHost.GetDesigner(this.splitterPanel2) as SplitterPanelDesigner);
				if (splitterPanelDesigner != null)
				{
					ControlBodyGlyph value = new ControlBodyGlyph(bounds, Cursor.Current, this.splitterPanel2, splitterPanelDesigner);
					selectionManager.BodyGlyphAdorner.Glyphs.Add(value);
				}
			}
			return base.GetControlGlyph(selectionType);
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000C3A54 File Offset: 0x000C1C54
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			base.AutoResizeHandles = true;
			this.splitContainer = (component as SplitContainer);
			this.splitterPanel1 = this.splitContainer.Panel1;
			this.splitterPanel2 = this.splitContainer.Panel2;
			base.EnableDesignMode(this.splitContainer.Panel1, "Panel1");
			base.EnableDesignMode(this.splitContainer.Panel2, "Panel2");
			this.designerHost = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
			if (this.selectedPanel == null)
			{
				this.Selected = this.splitterPanel1;
			}
			this.splitContainer.MouseDown += this.OnSplitContainer;
			this.splitContainer.SplitterMoved += this.OnSplitterMoved;
			this.splitContainer.SplitterMoving += this.OnSplitterMoving;
			this.splitContainer.DoubleClick += this.OnSplitContainerDoubleClick;
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SelectionChanged += this.OnSelectionChanged;
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000C3B88 File Offset: 0x000C1D88
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

		// Token: 0x06002045 RID: 8261 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool CanParent(Control control)
		{
			return false;
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000C3BC0 File Offset: 0x000C1DC0
		private void OnSplitContainer(object sender, MouseEventArgs e)
		{
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			selectionService.SetSelectedComponents(new object[]
			{
				this.Control
			});
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000C3BF8 File Offset: 0x000C1DF8
		private void OnSplitContainerDoubleClick(object sender, EventArgs e)
		{
			if (this.splitContainerSelected)
			{
				try
				{
					this.DoDefaultAction();
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
					base.DisplayError(ex);
				}
			}
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000C3C3C File Offset: 0x000C1E3C
		private void OnSplitterMoved(object sender, SplitterEventArgs e)
		{
			if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly || this.splitterDistanceException)
			{
				return;
			}
			try
			{
				base.RaiseComponentChanging(TypeDescriptor.GetProperties(this.splitContainer)["SplitterDistance"]);
				base.RaiseComponentChanged(TypeDescriptor.GetProperties(this.splitContainer)["SplitterDistance"], null, null);
				if (this.disabledGlyphs)
				{
					base.BehaviorService.EnableAllAdorners(true);
					SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
					if (selectionManager != null)
					{
						selectionManager.Refresh();
					}
					this.disabledGlyphs = false;
				}
			}
			catch (InvalidOperationException ex)
			{
				IUIService iuiservice = (IUIService)base.Component.Site.GetService(typeof(IUIService));
				iuiservice.ShowError(ex.Message);
			}
			catch (CheckoutException ex2)
			{
				if (ex2 == CheckoutException.Canceled)
				{
					try
					{
						this.splitterDistanceException = true;
						this.splitContainer.SplitterDistance = this.initialSplitterDist;
						goto IL_E7;
					}
					finally
					{
						this.splitterDistanceException = false;
					}
					goto IL_E5;
					IL_E7:
					return;
				}
				IL_E5:
				throw;
			}
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000C3D5C File Offset: 0x000C1F5C
		private void OnSplitterMoving(object sender, SplitterCancelEventArgs e)
		{
			this.initialSplitterDist = this.splitContainer.SplitterDistance;
			if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
			{
				return;
			}
			this.disabledGlyphs = true;
			Adorner adorner = null;
			SelectionManager selectionManager = (SelectionManager)this.GetService(typeof(SelectionManager));
			if (selectionManager != null)
			{
				adorner = selectionManager.BodyGlyphAdorner;
			}
			foreach (Adorner adorner2 in base.BehaviorService.Adorners)
			{
				if (adorner == null || !adorner2.Equals(adorner))
				{
					adorner2.EnabledInternal = false;
				}
			}
			base.BehaviorService.Invalidate();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in adorner.Glyphs)
			{
				ControlBodyGlyph controlBodyGlyph = (ControlBodyGlyph)obj;
				if (!(controlBodyGlyph.RelatedComponent is SplitterPanel))
				{
					arrayList.Add(controlBodyGlyph);
				}
			}
			foreach (object obj2 in arrayList)
			{
				Glyph value = (Glyph)obj2;
				adorner.Glyphs.Remove(value);
			}
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000C3ED4 File Offset: 0x000C20D4
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			this.splitContainerSelected = false;
			if (selectionService != null)
			{
				ICollection selectedComponents = selectionService.GetSelectedComponents();
				foreach (object obj in selectedComponents)
				{
					SplitterPanel splitterPanel = SplitContainerDesigner.CheckIfPanelSelected(obj);
					if (splitterPanel != null && splitterPanel.Parent == this.splitContainer)
					{
						this.splitContainerSelected = false;
						this.Selected = splitterPanel;
						break;
					}
					this.Selected = null;
					if (obj == this.splitContainer)
					{
						this.splitContainerSelected = true;
						break;
					}
				}
			}
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000C3F8C File Offset: 0x000C218C
		private static SplitterPanel CheckIfPanelSelected(object comp)
		{
			return comp as SplitterPanel;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000C3F94 File Offset: 0x000C2194
		internal void SplitterPanelHover()
		{
			this.OnMouseHover();
		}

		// Token: 0x040018CB RID: 6347
		private const string panel1Name = "Panel1";

		// Token: 0x040018CC RID: 6348
		private const string panel2Name = "Panel2";

		// Token: 0x040018CD RID: 6349
		private IDesignerHost designerHost;

		// Token: 0x040018CE RID: 6350
		private SplitContainer splitContainer;

		// Token: 0x040018CF RID: 6351
		private SplitterPanel selectedPanel;

		// Token: 0x040018D0 RID: 6352
		private static int numberOfSplitterPanels = 2;

		// Token: 0x040018D1 RID: 6353
		private SplitterPanel splitterPanel1;

		// Token: 0x040018D2 RID: 6354
		private SplitterPanel splitterPanel2;

		// Token: 0x040018D3 RID: 6355
		private bool disableDrawGrid;

		// Token: 0x040018D4 RID: 6356
		private bool disabledGlyphs;

		// Token: 0x040018D5 RID: 6357
		private bool splitContainerSelected;

		// Token: 0x040018D6 RID: 6358
		private int initialSplitterDist;

		// Token: 0x040018D7 RID: 6359
		private bool splitterDistanceException;

		// Token: 0x0200058E RID: 1422
		private class OrientationActionList : DesignerActionList
		{
			// Token: 0x060032CD RID: 13005 RVA: 0x001136EC File Offset: 0x001118EC
			public OrientationActionList(SplitContainerDesigner owner) : base(owner.Component)
			{
				this.owner = owner;
				this.ownerComponent = (owner.Component as Component);
				if (this.ownerComponent != null)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.ownerComponent)["Orientation"];
					if (propertyDescriptor != null)
					{
						this.actionName = (((Orientation)propertyDescriptor.GetValue(this.ownerComponent) == Orientation.Horizontal) ? SR.GetString("DesignerShortcutVerticalOrientation") : SR.GetString("DesignerShortcutHorizontalOrientation"));
					}
				}
			}

			// Token: 0x060032CE RID: 13006 RVA: 0x00113774 File Offset: 0x00111974
			private void OnOrientationActionClick(object sender, EventArgs e)
			{
				DesignerVerb designerVerb = sender as DesignerVerb;
				if (designerVerb != null)
				{
					Orientation orientation = designerVerb.Text.Equals(SR.GetString("DesignerShortcutHorizontalOrientation")) ? Orientation.Horizontal : Orientation.Vertical;
					this.actionName = ((orientation == Orientation.Horizontal) ? SR.GetString("DesignerShortcutVerticalOrientation") : SR.GetString("DesignerShortcutHorizontalOrientation"));
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.ownerComponent)["Orientation"];
					if (propertyDescriptor != null && (Orientation)propertyDescriptor.GetValue(this.ownerComponent) != orientation)
					{
						propertyDescriptor.SetValue(this.ownerComponent, orientation);
					}
					DesignerActionUIService designerActionUIService = (DesignerActionUIService)this.owner.GetService(typeof(DesignerActionUIService));
					if (designerActionUIService != null)
					{
						designerActionUIService.Refresh(this.ownerComponent);
					}
				}
			}

			// Token: 0x060032CF RID: 13007 RVA: 0x00113834 File Offset: 0x00111A34
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				return new DesignerActionItemCollection
				{
					new DesignerActionVerbItem(new DesignerVerb(this.actionName, new EventHandler(this.OnOrientationActionClick)))
				};
			}

			// Token: 0x040021F0 RID: 8688
			private string actionName;

			// Token: 0x040021F1 RID: 8689
			private SplitContainerDesigner owner;

			// Token: 0x040021F2 RID: 8690
			private Component ownerComponent;
		}
	}
}
