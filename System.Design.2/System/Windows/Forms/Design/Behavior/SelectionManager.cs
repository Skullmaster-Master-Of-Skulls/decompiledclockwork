using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200038D RID: 909
	internal sealed class SelectionManager : IDisposable
	{
		// Token: 0x06002522 RID: 9506 RVA: 0x000E8248 File Offset: 0x000E6448
		public SelectionManager(IServiceProvider serviceProvider, BehaviorService behaviorService)
		{
			this.prevSelectionBounds = null;
			this.prevPrimarySelection = null;
			this.behaviorService = behaviorService;
			this.serviceProvider = serviceProvider;
			this.selSvc = (ISelectionService)serviceProvider.GetService(typeof(ISelectionService));
			this.designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
			if (this.designerHost != null)
			{
				ISelectionService selectionService = this.selSvc;
			}
			behaviorService.BeginDrag += this.OnBeginDrag;
			behaviorService.Synchronize += this.OnSynchronize;
			this.selSvc.SelectionChanged += this.OnSelectionChanged;
			this.rootComponent = (Control)this.designerHost.RootComponent;
			this.selectionAdorner = new Adorner();
			this.bodyAdorner = new Adorner();
			behaviorService.Adorners.Add(this.bodyAdorner);
			behaviorService.Adorners.Add(this.selectionAdorner);
			this.componentToDesigner = new Hashtable();
			IComponentChangeService componentChangeService = (IComponentChangeService)serviceProvider.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentAdded += this.OnComponentAdded;
				componentChangeService.ComponentRemoved += this.OnComponentRemoved;
				componentChangeService.ComponentChanged += this.OnComponentChanged;
			}
			this.designerHost.TransactionClosed += this.OnTransactionClosed;
			DesignerOptionService designerOptionService = this.designerHost.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
			if (designerOptionService != null)
			{
				PropertyDescriptor propertyDescriptor = designerOptionService.Options.Properties["UseSmartTags"];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool) && (bool)propertyDescriptor.GetValue(null))
				{
					this.designerActionUI = new DesignerActionUI(serviceProvider, this.selectionAdorner);
					behaviorService.DesignerActionUI = this.designerActionUI;
				}
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x000E8432 File Offset: 0x000E6632
		internal Adorner BodyGlyphAdorner
		{
			get
			{
				return this.bodyAdorner;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x000E843A File Offset: 0x000E663A
		// (set) Token: 0x06002525 RID: 9509 RVA: 0x000E8442 File Offset: 0x000E6642
		internal bool NeedRefresh
		{
			get
			{
				return this.needRefresh;
			}
			set
			{
				this.needRefresh = value;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x000E844B File Offset: 0x000E664B
		internal Adorner SelectionGlyphAdorner
		{
			get
			{
				return this.selectionAdorner;
			}
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000E8454 File Offset: 0x000E6654
		private void AddAllControlGlyphs(Control parent, ArrayList selComps, object primarySelection)
		{
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				this.AddAllControlGlyphs(parent2, selComps, primarySelection);
			}
			GlyphSelectionType selType = GlyphSelectionType.NotSelected;
			if (selComps.Contains(parent))
			{
				if (parent.Equals(primarySelection))
				{
					selType = GlyphSelectionType.SelectedPrimary;
				}
				else
				{
					selType = GlyphSelectionType.Selected;
				}
			}
			this.AddControlGlyphs(parent, selType);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000E84D4 File Offset: 0x000E66D4
		private void AddControlGlyphs(Control c, GlyphSelectionType selType)
		{
			ControlDesigner controlDesigner = (ControlDesigner)this.componentToDesigner[c];
			if (controlDesigner != null)
			{
				ControlBodyGlyph controlGlyphInternal = controlDesigner.GetControlGlyphInternal(selType);
				if (controlGlyphInternal != null)
				{
					this.bodyAdorner.Glyphs.Add(controlGlyphInternal);
					if (selType == GlyphSelectionType.SelectedPrimary || selType == GlyphSelectionType.Selected)
					{
						if (this.curSelectionBounds[this.curCompIndex] == Rectangle.Empty)
						{
							this.curSelectionBounds[this.curCompIndex] = controlGlyphInternal.Bounds;
						}
						else
						{
							this.curSelectionBounds[this.curCompIndex] = Rectangle.Union(this.curSelectionBounds[this.curCompIndex], controlGlyphInternal.Bounds);
						}
					}
				}
				GlyphCollection glyphs = controlDesigner.GetGlyphs(selType);
				if (glyphs != null)
				{
					this.selectionAdorner.Glyphs.AddRange(glyphs);
					if (selType == GlyphSelectionType.SelectedPrimary || selType == GlyphSelectionType.Selected)
					{
						foreach (object obj in glyphs)
						{
							Glyph glyph = (Glyph)obj;
							this.curSelectionBounds[this.curCompIndex] = Rectangle.Union(this.curSelectionBounds[this.curCompIndex], glyph.Bounds);
						}
					}
				}
			}
			if (selType == GlyphSelectionType.SelectedPrimary || selType == GlyphSelectionType.Selected)
			{
				this.curCompIndex++;
			}
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000E8630 File Offset: 0x000E6830
		public void Dispose()
		{
			if (this.designerHost != null)
			{
				this.designerHost.TransactionClosed -= this.OnTransactionClosed;
				this.designerHost = null;
			}
			if (this.serviceProvider != null)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.serviceProvider.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdded -= this.OnComponentAdded;
					componentChangeService.ComponentChanged -= this.OnComponentChanged;
					componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				}
				if (this.selSvc != null)
				{
					this.selSvc.SelectionChanged -= this.OnSelectionChanged;
					this.selSvc = null;
				}
				this.serviceProvider = null;
			}
			if (this.behaviorService != null)
			{
				this.behaviorService.Adorners.Remove(this.bodyAdorner);
				this.behaviorService.Adorners.Remove(this.selectionAdorner);
				this.behaviorService.BeginDrag -= this.OnBeginDrag;
				this.behaviorService.Synchronize -= this.OnSynchronize;
				this.behaviorService = null;
			}
			if (this.selectionAdorner != null)
			{
				this.selectionAdorner.Glyphs.Clear();
				this.selectionAdorner = null;
			}
			if (this.bodyAdorner != null)
			{
				this.bodyAdorner.Glyphs.Clear();
				this.bodyAdorner = null;
			}
			if (this.designerActionUI != null)
			{
				this.designerActionUI.Dispose();
				this.designerActionUI = null;
			}
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000E87B0 File Offset: 0x000E69B0
		public void Refresh()
		{
			this.NeedRefresh = false;
			this.OnSelectionChanged(this, null);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000E87C4 File Offset: 0x000E69C4
		private void OnComponentAdded(object source, ComponentEventArgs ce)
		{
			IComponent component = ce.Component;
			IDesigner designer = this.designerHost.GetDesigner(component);
			if (designer is ControlDesigner)
			{
				this.componentToDesigner.Add(component, designer);
			}
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000E87FC File Offset: 0x000E69FC
		private void OnBeginDrag(object source, BehaviorDragDropEventArgs e)
		{
			ArrayList arrayList = new ArrayList(e.DragComponents);
			ArrayList arrayList2 = new ArrayList();
			foreach (object obj in this.bodyAdorner.Glyphs)
			{
				ControlBodyGlyph controlBodyGlyph = (ControlBodyGlyph)obj;
				if (controlBodyGlyph.RelatedComponent is Control && (arrayList.Contains(controlBodyGlyph.RelatedComponent) || !((Control)controlBodyGlyph.RelatedComponent).AllowDrop))
				{
					arrayList2.Add(controlBodyGlyph);
				}
			}
			foreach (object obj2 in arrayList2)
			{
				Glyph value = (Glyph)obj2;
				this.bodyAdorner.Glyphs.Remove(value);
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000E88F4 File Offset: 0x000E6AF4
		internal void OnBeginDrag(BehaviorDragDropEventArgs e)
		{
			this.OnBeginDrag(null, e);
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000E88FE File Offset: 0x000E6AFE
		private void OnComponentChanged(object source, ComponentChangedEventArgs ce)
		{
			if (this.selSvc.GetComponentSelected(ce.Component))
			{
				if (!this.designerHost.InTransaction)
				{
					this.Refresh();
					return;
				}
				this.NeedRefresh = true;
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000E892E File Offset: 0x000E6B2E
		private void OnComponentRemoved(object source, ComponentEventArgs ce)
		{
			if (this.componentToDesigner.Contains(ce.Component))
			{
				this.componentToDesigner.Remove(ce.Component);
			}
			if (this.designerActionUI != null)
			{
				this.designerActionUI.RemoveActionGlyph(ce.Component);
			}
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000E8970 File Offset: 0x000E6B70
		private Region DetermineRegionToRefresh(object primarySelection)
		{
			Region region = new Region(Rectangle.Empty);
			Rectangle[] array;
			Rectangle[] array2;
			if (this.curSelectionBounds.Length >= this.prevSelectionBounds.Length)
			{
				array = this.curSelectionBounds;
				array2 = this.prevSelectionBounds;
			}
			else
			{
				array = this.prevSelectionBounds;
				array2 = this.curSelectionBounds;
			}
			bool[] array3 = new bool[array2.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array3[i] = false;
			}
			for (int j = 0; j < array.Length; j++)
			{
				bool flag = false;
				Rectangle rectangle = array[j];
				for (int k = 0; k < array2.Length; k++)
				{
					if (rectangle.IntersectsWith(array2[k]))
					{
						Rectangle rectangle2 = array2[k];
						flag = true;
						if (rectangle != rectangle2)
						{
							region.Union(rectangle);
							region.Union(rectangle2);
						}
						array3[k] = true;
						break;
					}
				}
				if (!flag)
				{
					region.Union(rectangle);
				}
			}
			for (int l = 0; l < array3.Length; l++)
			{
				if (!array3[l])
				{
					region.Union(array2[l]);
				}
			}
			using (Graphics adornerWindowGraphics = this.behaviorService.AdornerWindowGraphics)
			{
				if (region.IsEmpty(adornerWindowGraphics) && primarySelection != null && !primarySelection.Equals(this.prevPrimarySelection))
				{
					for (int m = 0; m < this.curSelectionBounds.Length; m++)
					{
						region.Union(this.curSelectionBounds[m]);
					}
				}
			}
			return region;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000E8AEC File Offset: 0x000E6CEC
		private void OnSynchronize(object sender, EventArgs e)
		{
			this.Refresh();
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000E8AF4 File Offset: 0x000E6CF4
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			if (!this.selectionChanging)
			{
				this.selectionChanging = true;
				this.selectionAdorner.Glyphs.Clear();
				this.bodyAdorner.Glyphs.Clear();
				ArrayList arrayList = new ArrayList(this.selSvc.GetSelectedComponents());
				object primarySelection = this.selSvc.PrimarySelection;
				this.curCompIndex = 0;
				this.curSelectionBounds = new Rectangle[arrayList.Count];
				this.AddAllControlGlyphs(this.rootComponent, arrayList, primarySelection);
				if (this.prevSelectionBounds != null)
				{
					Region region = this.DetermineRegionToRefresh(primarySelection);
					using (Graphics adornerWindowGraphics = this.behaviorService.AdornerWindowGraphics)
					{
						if (!region.IsEmpty(adornerWindowGraphics))
						{
							this.selectionAdorner.Invalidate(region);
						}
						goto IL_11E;
					}
				}
				if (this.curSelectionBounds.Length != 0)
				{
					Rectangle rectangle = this.curSelectionBounds[0];
					for (int i = 1; i < this.curSelectionBounds.Length; i++)
					{
						rectangle = Rectangle.Union(rectangle, this.curSelectionBounds[i]);
					}
					if (rectangle != Rectangle.Empty)
					{
						this.selectionAdorner.Invalidate(rectangle);
					}
				}
				else
				{
					this.selectionAdorner.Invalidate();
				}
				IL_11E:
				this.prevPrimarySelection = primarySelection;
				if (this.curSelectionBounds.Length != 0)
				{
					this.prevSelectionBounds = new Rectangle[this.curSelectionBounds.Length];
					Array.Copy(this.curSelectionBounds, this.prevSelectionBounds, this.curSelectionBounds.Length);
				}
				else
				{
					this.prevSelectionBounds = null;
				}
				this.selectionChanging = false;
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000E8C7C File Offset: 0x000E6E7C
		private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (e.LastTransaction && this.NeedRefresh)
			{
				this.Refresh();
			}
		}

		// Token: 0x04001AFC RID: 6908
		private Adorner selectionAdorner;

		// Token: 0x04001AFD RID: 6909
		private Adorner bodyAdorner;

		// Token: 0x04001AFE RID: 6910
		private BehaviorService behaviorService;

		// Token: 0x04001AFF RID: 6911
		private IServiceProvider serviceProvider;

		// Token: 0x04001B00 RID: 6912
		private Hashtable componentToDesigner;

		// Token: 0x04001B01 RID: 6913
		private Control rootComponent;

		// Token: 0x04001B02 RID: 6914
		private ISelectionService selSvc;

		// Token: 0x04001B03 RID: 6915
		private IDesignerHost designerHost;

		// Token: 0x04001B04 RID: 6916
		private bool needRefresh;

		// Token: 0x04001B05 RID: 6917
		private Rectangle[] prevSelectionBounds;

		// Token: 0x04001B06 RID: 6918
		private object prevPrimarySelection;

		// Token: 0x04001B07 RID: 6919
		private Rectangle[] curSelectionBounds;

		// Token: 0x04001B08 RID: 6920
		private int curCompIndex;

		// Token: 0x04001B09 RID: 6921
		private DesignerActionUI designerActionUI;

		// Token: 0x04001B0A RID: 6922
		private bool selectionChanging;
	}
}
