using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;
using Accessibility;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002B1 RID: 689
	public class ControlDesigner : ComponentDesigner
	{
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06001AF3 RID: 6899 RVA: 0x0009EEB8 File Offset: 0x0009D0B8
		// (remove) Token: 0x06001AF4 RID: 6900 RVA: 0x0009EEF0 File Offset: 0x0009D0F0
		private event EventHandler disposingHandler;

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0009EF25 File Offset: 0x0009D125
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x0009EF3C File Offset: 0x0009D13C
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

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0009EF54 File Offset: 0x0009D154
		protected BehaviorService BehaviorService
		{
			get
			{
				if (this.behaviorService == null)
				{
					this.behaviorService = (BehaviorService)this.GetService(typeof(BehaviorService));
				}
				return this.behaviorService;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x0009EF7F File Offset: 0x0009D17F
		// (set) Token: 0x06001AF9 RID: 6905 RVA: 0x0009EF87 File Offset: 0x0009D187
		internal bool ForceVisible
		{
			get
			{
				return this.forceVisible;
			}
			set
			{
				this.forceVisible = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0009EF90 File Offset: 0x0009D190
		private Dictionary<IntPtr, bool> SubclassedChildWindows
		{
			get
			{
				if (this.subclassedChildren == null)
				{
					this.subclassedChildren = new Dictionary<IntPtr, bool>();
				}
				return this.subclassedChildren;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0009EFAB File Offset: 0x0009D1AB
		private IOverlayService OverlayService
		{
			get
			{
				if (this.overlayService == null)
				{
					this.overlayService = (IOverlayService)this.GetService(typeof(IOverlayService));
				}
				return this.overlayService;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x0009EFD6 File Offset: 0x0009D1D6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		private ControlDesigner.DesignerControlCollection Controls
		{
			get
			{
				if (this.controls == null)
				{
					this.controls = new ControlDesigner.DesignerControlCollection(this.Control);
				}
				return this.controls;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0009EFF8 File Offset: 0x0009D1F8
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x0009F044 File Offset: 0x0009D244
		private Point Location
		{
			get
			{
				Point location = this.Control.Location;
				ScrollableControl scrollableControl = this.Control.Parent as ScrollableControl;
				if (scrollableControl != null)
				{
					Point autoScrollPosition = scrollableControl.AutoScrollPosition;
					location.Offset(-autoScrollPosition.X, -autoScrollPosition.Y);
				}
				return location;
			}
			set
			{
				ScrollableControl scrollableControl = this.Control.Parent as ScrollableControl;
				if (scrollableControl != null)
				{
					Point autoScrollPosition = scrollableControl.AutoScrollPosition;
					value.Offset(autoScrollPosition.X, autoScrollPosition.Y);
				}
				this.Control.Location = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0009F090 File Offset: 0x0009D290
		public override ICollection AssociatedComponents
		{
			get
			{
				ArrayList arrayList = null;
				foreach (object obj in this.Control.Controls)
				{
					Control control = (Control)obj;
					if (control.Site != null)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(control);
					}
				}
				if (arrayList != null)
				{
					return arrayList;
				}
				return base.AssociatedComponents;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0009F110 File Offset: 0x0009D310
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x0009F128 File Offset: 0x0009D328
		private ContextMenu ContextMenu
		{
			get
			{
				return (ContextMenu)base.ShadowProperties["ContextMenu"];
			}
			set
			{
				ContextMenu contextMenu = (ContextMenu)base.ShadowProperties["ContextMenu"];
				if (contextMenu != value)
				{
					EventHandler value2 = new EventHandler(this.DetachContextMenu);
					if (contextMenu != null)
					{
						contextMenu.Disposed -= value2;
					}
					base.ShadowProperties["ContextMenu"] = value;
					if (value != null)
					{
						value.Disposed += value2;
					}
				}
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0009F181 File Offset: 0x0009D381
		public virtual AccessibleObject AccessibilityObject
		{
			get
			{
				if (this.accessibilityObj == null)
				{
					this.accessibilityObj = new ControlDesigner.ControlDesignerAccessibleObject(this, this.Control);
				}
				return this.accessibilityObj;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0009F1A3 File Offset: 0x0009D3A3
		public virtual Control Control
		{
			get
			{
				return (Control)base.Component;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0009F1B0 File Offset: 0x0009D3B0
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x0009F1B8 File Offset: 0x0009D3B8
		private ControlDesigner.IDesignerTarget DesignerTarget
		{
			get
			{
				return this.designerTarget;
			}
			set
			{
				this.designerTarget = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0009F1C1 File Offset: 0x0009D3C1
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x0009F1D8 File Offset: 0x0009D3D8
		private bool Enabled
		{
			get
			{
				return (bool)base.ShadowProperties["Enabled"];
			}
			set
			{
				base.ShadowProperties["Enabled"] = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x0000445B File Offset: 0x0000265B
		protected virtual bool EnableDragRect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0009F1F0 File Offset: 0x0009D3F0
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x0009F1F8 File Offset: 0x0009D3F8
		private bool Locked
		{
			get
			{
				return this.locked;
			}
			set
			{
				if (this.locked != value)
				{
					this.locked = value;
				}
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0009F20A File Offset: 0x0009D40A
		// (set) Token: 0x06001B0C RID: 6924 RVA: 0x0009F21C File Offset: 0x0009D41C
		private string Name
		{
			get
			{
				return base.Component.Site.Name;
			}
			set
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost == null || (designerHost != null && !designerHost.Loading))
				{
					base.Component.Site.Name = value;
				}
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0009F260 File Offset: 0x0009D460
		protected override IComponent ParentComponent
		{
			get
			{
				Control control = base.Component as Control;
				if (control != null && control.Parent != null)
				{
					return control.Parent;
				}
				return base.ParentComponent;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x00003B0F File Offset: 0x00001D0F
		public virtual bool ParticipatesWithSnapLines
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual int NumberOfInternalControlDesigners()
		{
			return 0;
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00003598 File Offset: 0x00001798
		public virtual ControlDesigner InternalControlDesigner(int internalControlIndex)
		{
			return null;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0009F294 File Offset: 0x0009D494
		private bool IsResizableConsiderAutoSize(PropertyDescriptor autoSizeProp, PropertyDescriptor autoSizeModeProp)
		{
			object component = base.Component;
			bool result = true;
			bool flag = false;
			bool flag2 = false;
			if (autoSizeProp != null && !autoSizeProp.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden) && !autoSizeProp.Attributes.Contains(BrowsableAttribute.No))
			{
				flag = (bool)autoSizeProp.GetValue(component);
			}
			if (autoSizeModeProp != null)
			{
				AutoSizeMode autoSizeMode = (AutoSizeMode)autoSizeModeProp.GetValue(component);
				flag2 = (autoSizeMode == AutoSizeMode.GrowOnly);
			}
			if (flag)
			{
				result = flag2;
			}
			return result;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0009F2FF File Offset: 0x0009D4FF
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x0009F307 File Offset: 0x0009D507
		public bool AutoResizeHandles
		{
			get
			{
				return this.autoResizeHandles;
			}
			set
			{
				this.autoResizeHandles = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x0009F310 File Offset: 0x0009D510
		public virtual SelectionRules SelectionRules
		{
			get
			{
				object component = base.Component;
				SelectionRules selectionRules = SelectionRules.Visible;
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
				PropertyDescriptor autoSizeProp = properties["AutoSize"];
				PropertyDescriptor autoSizeModeProp = properties["AutoSizeMode"];
				PropertyDescriptor propertyDescriptor;
				if ((propertyDescriptor = properties["Location"]) != null && !propertyDescriptor.IsReadOnly)
				{
					selectionRules |= SelectionRules.Moveable;
				}
				if ((propertyDescriptor = properties["Size"]) != null && !propertyDescriptor.IsReadOnly)
				{
					if (this.AutoResizeHandles && base.Component != this.host.RootComponent)
					{
						selectionRules = (this.IsResizableConsiderAutoSize(autoSizeProp, autoSizeModeProp) ? (selectionRules | SelectionRules.AllSizeable) : selectionRules);
					}
					else
					{
						selectionRules |= SelectionRules.AllSizeable;
					}
				}
				PropertyDescriptor propertyDescriptor2 = properties["Dock"];
				if (propertyDescriptor2 != null)
				{
					DockStyle dockStyle = (DockStyle)((int)propertyDescriptor2.GetValue(component));
					if (this.Control.Parent != null && this.Control.Parent.IsMirrored)
					{
						if (dockStyle == DockStyle.Left)
						{
							dockStyle = DockStyle.Right;
						}
						else if (dockStyle == DockStyle.Right)
						{
							dockStyle = DockStyle.Left;
						}
					}
					switch (dockStyle)
					{
					case DockStyle.Top:
						selectionRules &= ~(SelectionRules.Moveable | SelectionRules.TopSizeable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
						break;
					case DockStyle.Bottom:
						selectionRules &= ~(SelectionRules.Moveable | SelectionRules.BottomSizeable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
						break;
					case DockStyle.Left:
						selectionRules &= ~(SelectionRules.Moveable | SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.LeftSizeable);
						break;
					case DockStyle.Right:
						selectionRules &= ~(SelectionRules.Moveable | SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.RightSizeable);
						break;
					case DockStyle.Fill:
						selectionRules &= ~(SelectionRules.Moveable | SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
						break;
					}
				}
				PropertyDescriptor propertyDescriptor3 = properties["Locked"];
				if (propertyDescriptor3 != null)
				{
					object value = propertyDescriptor3.GetValue(component);
					if (value is bool && (bool)value)
					{
						selectionRules = (SelectionRules.Visible | SelectionRules.Locked);
					}
				}
				return selectionRules;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x00003B0F File Offset: 0x00001D0F
		internal virtual bool ControlSupportsSnaplines
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0009F494 File Offset: 0x0009D694
		internal Point GetOffsetToClientArea()
		{
			NativeMethods.POINT point = new NativeMethods.POINT(0, 0);
			NativeMethods.MapWindowPoints(this.Control.Handle, this.Control.Parent.Handle, point, 1);
			Point location = this.Control.Location;
			if (this.Control.IsMirrored != this.Control.Parent.IsMirrored)
			{
				location.Offset(this.Control.Width, 0);
			}
			return new Point(Math.Abs(point.x - location.X), point.y - location.Y);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0009F52E File Offset: 0x0009D72E
		internal IList SnapLinesInternal()
		{
			return this.SnapLinesInternal(this.Control.Margin);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0009F544 File Offset: 0x0009D744
		internal IList SnapLinesInternal(Padding margin)
		{
			ArrayList arrayList = new ArrayList(4);
			int width = this.Control.Width;
			int height = this.Control.Height;
			arrayList.Add(new SnapLine(SnapLineType.Top, 0, SnapLinePriority.Low));
			arrayList.Add(new SnapLine(SnapLineType.Bottom, height - 1, SnapLinePriority.Low));
			arrayList.Add(new SnapLine(SnapLineType.Left, 0, SnapLinePriority.Low));
			arrayList.Add(new SnapLine(SnapLineType.Right, width - 1, SnapLinePriority.Low));
			arrayList.Add(new SnapLine(SnapLineType.Horizontal, -margin.Top, "Margin.Top", SnapLinePriority.Always));
			arrayList.Add(new SnapLine(SnapLineType.Horizontal, margin.Bottom + height, "Margin.Bottom", SnapLinePriority.Always));
			arrayList.Add(new SnapLine(SnapLineType.Vertical, -margin.Left, "Margin.Left", SnapLinePriority.Always));
			arrayList.Add(new SnapLine(SnapLineType.Vertical, margin.Right + width, "Margin.Right", SnapLinePriority.Always));
			return arrayList;
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0009F61F File Offset: 0x0009D81F
		public virtual IList SnapLines
		{
			get
			{
				return this.SnapLinesInternal();
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0009F627 File Offset: 0x0009D827
		internal virtual Behavior StandardBehavior
		{
			get
			{
				if (this.resizeBehavior == null)
				{
					this.resizeBehavior = new ResizeBehavior(base.Component.Site);
				}
				return this.resizeBehavior;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0000445B File Offset: 0x0000265B
		internal virtual bool SerializePerformLayout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0009F64D File Offset: 0x0009D84D
		internal Behavior MoveBehavior
		{
			get
			{
				if (this.moveBehavior == null)
				{
					this.moveBehavior = new ContainerSelectorBehavior(this.Control, base.Component.Site);
				}
				return this.moveBehavior;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0009F679 File Offset: 0x0009D879
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x0009F690 File Offset: 0x0009D890
		private bool Visible
		{
			get
			{
				return (bool)base.ShadowProperties["Visible"];
			}
			set
			{
				base.ShadowProperties["Visible"] = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0009F6A8 File Offset: 0x0009D8A8
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				if (base.IsRootDesigner)
				{
					return InheritanceAttribute.Inherited;
				}
				return base.InheritanceAttribute;
			}
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0009F6BE File Offset: 0x0009D8BE
		protected void BaseWndProc(ref Message m)
		{
			m.Result = NativeMethods.DefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0009F6E3 File Offset: 0x0009D8E3
		internal override bool CanBeAssociatedWith(IDesigner parentDesigner)
		{
			return this.CanBeParentedTo(parentDesigner);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0009F6EC File Offset: 0x0009D8EC
		public virtual bool CanBeParentedTo(IDesigner parentDesigner)
		{
			ParentControlDesigner parentControlDesigner = parentDesigner as ParentControlDesigner;
			return parentControlDesigner != null && !this.Control.Contains(parentControlDesigner.Control);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0009F71C File Offset: 0x0009D91C
		private void DataBindingsCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			Control control = base.Component as Control;
			if (control != null)
			{
				if (control.DataBindings.Count == 0 && this.removalNotificationHooked)
				{
					IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentRemoved -= this.DataSource_ComponentRemoved;
					}
					this.removalNotificationHooked = false;
					return;
				}
				if (control.DataBindings.Count > 0 && !this.removalNotificationHooked)
				{
					IComponentChangeService componentChangeService2 = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					if (componentChangeService2 != null)
					{
						componentChangeService2.ComponentRemoved += this.DataSource_ComponentRemoved;
					}
					this.removalNotificationHooked = true;
				}
			}
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0009F7CC File Offset: 0x0009D9CC
		private void DataSource_ComponentRemoved(object sender, ComponentEventArgs e)
		{
			Control control = base.Component as Control;
			if (control != null)
			{
				control.DataBindings.CollectionChanged -= this.dataBindingsCollectionChanged;
				for (int i = 0; i < control.DataBindings.Count; i++)
				{
					Binding binding = control.DataBindings[i];
					if (binding.DataSource == e.Component)
					{
						control.DataBindings.Remove(binding);
					}
				}
				if (control.DataBindings.Count == 0)
				{
					IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentRemoved -= this.DataSource_ComponentRemoved;
					}
					this.removalNotificationHooked = false;
				}
				control.DataBindings.CollectionChanged += this.dataBindingsCollectionChanged;
			}
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0009F889 File Offset: 0x0009DA89
		protected void DefWndProc(ref Message m)
		{
			this.designerTarget.DefWndProc(ref m);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0009F897 File Offset: 0x0009DA97
		private void DetachContextMenu(object sender, EventArgs e)
		{
			this.ContextMenu = null;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0009F8A0 File Offset: 0x0009DAA0
		protected void DisplayError(Exception e)
		{
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				iuiservice.ShowError(e);
				return;
			}
			string text = e.Message;
			if (text == null || text.Length == 0)
			{
				text = e.ToString();
			}
			RTLAwareMessageBox.Show(this.Control, text, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0009F8FC File Offset: 0x0009DAFC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.Control != null)
				{
					if (this.dataBindingsCollectionChanged != null)
					{
						this.Control.DataBindings.CollectionChanged -= this.dataBindingsCollectionChanged;
					}
					if (base.Inherited && this.inheritanceUI != null)
					{
						this.inheritanceUI.RemoveInheritedControl(this.Control);
					}
					if (this.removalNotificationHooked)
					{
						IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
						if (componentChangeService != null)
						{
							componentChangeService.ComponentRemoved -= this.DataSource_ComponentRemoved;
						}
						this.removalNotificationHooked = false;
					}
					if (this.disposingHandler != null)
					{
						this.disposingHandler(this, EventArgs.Empty);
					}
					this.UnhookChildControls(this.Control);
				}
				if (this.ContextMenu != null)
				{
					this.ContextMenu.Disposed -= this.DetachContextMenu;
				}
				if (this.designerTarget != null)
				{
					this.designerTarget.Dispose();
				}
				this.downPos = Point.Empty;
				this.Control.ControlAdded -= this.OnControlAdded;
				this.Control.ControlRemoved -= this.OnControlRemoved;
				this.Control.ParentChanged -= this.OnParentChanged;
				this.Control.SizeChanged -= this.OnSizeChanged;
				this.Control.LocationChanged -= this.OnLocationChanged;
				this.Control.EnabledChanged -= this.OnEnabledChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0009FA88 File Offset: 0x0009DC88
		protected bool EnableDesignMode(Control child, string name)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			INestedContainer nestedContainer = this.GetService(typeof(INestedContainer)) as INestedContainer;
			if (nestedContainer == null)
			{
				return false;
			}
			for (int i = 0; i < nestedContainer.Components.Count; i++)
			{
				if (nestedContainer.Components[i].Equals(child))
				{
					return true;
				}
			}
			nestedContainer.Add(child, name);
			return true;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0009FB04 File Offset: 0x0009DD04
		protected void EnableDragDrop(bool value)
		{
			Control control = this.Control;
			if (control == null)
			{
				return;
			}
			if (value)
			{
				control.DragDrop += this.OnDragDrop;
				control.DragOver += this.OnDragOver;
				control.DragEnter += this.OnDragEnter;
				control.DragLeave += this.OnDragLeave;
				control.GiveFeedback += this.OnGiveFeedback;
				this.hadDragDrop = control.AllowDrop;
				if (!this.hadDragDrop)
				{
					control.AllowDrop = true;
				}
				this.revokeDragDrop = false;
				return;
			}
			control.DragDrop -= this.OnDragDrop;
			control.DragOver -= this.OnDragOver;
			control.DragEnter -= this.OnDragEnter;
			control.DragLeave -= this.OnDragLeave;
			control.GiveFeedback -= this.OnGiveFeedback;
			if (!this.hadDragDrop)
			{
				control.AllowDrop = false;
			}
			this.revokeDragDrop = true;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0009FC0C File Offset: 0x0009DE0C
		protected virtual ControlBodyGlyph GetControlGlyph(GlyphSelectionType selectionType)
		{
			this.OnSetCursor();
			Cursor cursor = Cursor.Current;
			Rectangle bounds = this.BehaviorService.ControlRectInAdornerWindow(this.Control);
			ControlBodyGlyph controlBodyGlyph = null;
			Control parent = this.Control.Parent;
			if (parent != null && this.host != null && this.host.RootComponent != base.Component)
			{
				Rectangle rectangle = parent.RectangleToScreen(parent.ClientRectangle);
				Rectangle rect = this.Control.RectangleToScreen(this.Control.ClientRectangle);
				if (!rectangle.Contains(rect) && !rectangle.IntersectsWith(rect))
				{
					ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
					if (selectionService != null && selectionService.GetComponentSelected(this.Control))
					{
						controlBodyGlyph = new ControlBodyGlyph(bounds, cursor, this.Control, this.MoveBehavior);
					}
					else if (cursor == Cursors.SizeAll)
					{
						cursor = Cursors.Default;
					}
				}
			}
			if (controlBodyGlyph == null)
			{
				controlBodyGlyph = new ControlBodyGlyph(bounds, cursor, this.Control, this);
			}
			return controlBodyGlyph;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0009FD0E File Offset: 0x0009DF0E
		internal ControlBodyGlyph GetControlGlyphInternal(GlyphSelectionType selectionType)
		{
			return this.GetControlGlyph(selectionType);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0009FD18 File Offset: 0x0009DF18
		public virtual GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
		{
			GlyphCollection glyphCollection = new GlyphCollection();
			if (selectionType != GlyphSelectionType.NotSelected)
			{
				Rectangle controlBounds = this.BehaviorService.ControlRectInAdornerWindow(this.Control);
				bool primarySelection = selectionType == GlyphSelectionType.SelectedPrimary;
				SelectionRules selectionRules = this.SelectionRules;
				if (this.Locked || this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
				{
					glyphCollection.Add(new LockedHandleGlyph(controlBounds, primarySelection));
					glyphCollection.Add(new LockedBorderGlyph(controlBounds, SelectionBorderGlyphType.Top));
					glyphCollection.Add(new LockedBorderGlyph(controlBounds, SelectionBorderGlyphType.Bottom));
					glyphCollection.Add(new LockedBorderGlyph(controlBounds, SelectionBorderGlyphType.Left));
					glyphCollection.Add(new LockedBorderGlyph(controlBounds, SelectionBorderGlyphType.Right));
				}
				else if ((selectionRules & SelectionRules.AllSizeable) == SelectionRules.None)
				{
					glyphCollection.Add(new NoResizeHandleGlyph(controlBounds, selectionRules, primarySelection, this.MoveBehavior));
					glyphCollection.Add(new NoResizeSelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Top, this.MoveBehavior));
					glyphCollection.Add(new NoResizeSelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Bottom, this.MoveBehavior));
					glyphCollection.Add(new NoResizeSelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Left, this.MoveBehavior));
					glyphCollection.Add(new NoResizeSelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Right, this.MoveBehavior));
					if (TypeDescriptor.GetAttributes(base.Component).Contains(DesignTimeVisibleAttribute.Yes) && this.behaviorService.DesignerActionUI != null)
					{
						Glyph designerActionGlyph = this.behaviorService.DesignerActionUI.GetDesignerActionGlyph(base.Component);
						if (designerActionGlyph != null)
						{
							glyphCollection.Insert(0, designerActionGlyph);
						}
					}
				}
				else
				{
					if ((selectionRules & SelectionRules.TopSizeable) != SelectionRules.None)
					{
						glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.MiddleTop, this.StandardBehavior, primarySelection));
						if ((selectionRules & SelectionRules.LeftSizeable) != SelectionRules.None)
						{
							glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.UpperLeft, this.StandardBehavior, primarySelection));
						}
						if ((selectionRules & SelectionRules.RightSizeable) != SelectionRules.None)
						{
							glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.UpperRight, this.StandardBehavior, primarySelection));
						}
					}
					if ((selectionRules & SelectionRules.BottomSizeable) != SelectionRules.None)
					{
						glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.MiddleBottom, this.StandardBehavior, primarySelection));
						if ((selectionRules & SelectionRules.LeftSizeable) != SelectionRules.None)
						{
							glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.LowerLeft, this.StandardBehavior, primarySelection));
						}
						if ((selectionRules & SelectionRules.RightSizeable) != SelectionRules.None)
						{
							glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.LowerRight, this.StandardBehavior, primarySelection));
						}
					}
					if ((selectionRules & SelectionRules.LeftSizeable) != SelectionRules.None)
					{
						glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.MiddleLeft, this.StandardBehavior, primarySelection));
					}
					if ((selectionRules & SelectionRules.RightSizeable) != SelectionRules.None)
					{
						glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.MiddleRight, this.StandardBehavior, primarySelection));
					}
					glyphCollection.Add(new SelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Top, this.StandardBehavior));
					glyphCollection.Add(new SelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Bottom, this.StandardBehavior));
					glyphCollection.Add(new SelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Left, this.StandardBehavior));
					glyphCollection.Add(new SelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Right, this.StandardBehavior));
					if (TypeDescriptor.GetAttributes(base.Component).Contains(DesignTimeVisibleAttribute.Yes) && this.behaviorService.DesignerActionUI != null)
					{
						Glyph designerActionGlyph2 = this.behaviorService.DesignerActionUI.GetDesignerActionGlyph(base.Component);
						if (designerActionGlyph2 != null)
						{
							glyphCollection.Insert(0, designerActionGlyph2);
						}
					}
				}
			}
			return glyphCollection;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0000445B File Offset: 0x0000265B
		protected virtual bool GetHitTest(Point point)
		{
			return false;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0009FFE8 File Offset: 0x0009E1E8
		private int GetParentPointFromLparam(IntPtr lParam)
		{
			Point p = new Point(NativeMethods.Util.SignedLOWORD((int)((long)lParam)), NativeMethods.Util.SignedHIWORD((int)((long)lParam)));
			p = this.Control.PointToScreen(p);
			p = this.Control.Parent.PointToClient(p);
			return NativeMethods.Util.MAKELONG(p.X, p.Y);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000A0048 File Offset: 0x0009E248
		protected void HookChildControls(Control firstChild)
		{
			foreach (object obj in firstChild.Controls)
			{
				Control control = (Control)obj;
				if (control != null && this.host != null && !(this.host.GetDesigner(control) is ControlDesigner))
				{
					IWindowTarget windowTarget = control.WindowTarget;
					if (!(windowTarget is ControlDesigner.ChildWindowTarget))
					{
						control.WindowTarget = new ControlDesigner.ChildWindowTarget(this, control, windowTarget);
						control.ControlAdded += this.OnControlAdded;
					}
					if (control.IsHandleCreated)
					{
						Application.OleRequired();
						NativeMethods.RevokeDragDrop(control.Handle);
						this.HookChildHandles(control.Handle);
					}
					else
					{
						control.HandleCreated += this.OnChildHandleCreated;
					}
					this.HookChildControls(control);
				}
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x000A0134 File Offset: 0x0009E334
		private int CurrentProcessId
		{
			get
			{
				if (ControlDesigner.currentProcessId == 0)
				{
					ControlDesigner.currentProcessId = SafeNativeMethods.GetCurrentProcessId();
				}
				return ControlDesigner.currentProcessId;
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000A014C File Offset: 0x0009E34C
		private bool IsWindowInCurrentProcess(IntPtr hwnd)
		{
			int num;
			UnsafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, hwnd), out num);
			return num == this.CurrentProcessId;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000A0174 File Offset: 0x0009E374
		private void OnChildHandleCreated(object sender, EventArgs e)
		{
			Control control = sender as Control;
			if (control != null)
			{
				this.HookChildHandles(control.Handle);
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000A0198 File Offset: 0x0009E398
		internal void HookChildHandles(IntPtr firstChild)
		{
			IntPtr intPtr = firstChild;
			while (intPtr != IntPtr.Zero && this.IsWindowInCurrentProcess(intPtr))
			{
				Control control = Control.FromHandle(intPtr);
				if (control == null && !this.SubclassedChildWindows.ContainsKey(intPtr))
				{
					NativeMethods.RevokeDragDrop(intPtr);
					new ControlDesigner.ChildSubClass(this, intPtr);
					this.SubclassedChildWindows[intPtr] = true;
				}
				if (control == null || this.Control is UserControl)
				{
					this.HookChildHandles(NativeMethods.GetWindow(intPtr, 5));
				}
				intPtr = NativeMethods.GetWindow(intPtr, 2);
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000A0218 File Offset: 0x0009E418
		internal void RemoveSubclassedWindow(IntPtr hwnd)
		{
			if (this.SubclassedChildWindows.ContainsKey(hwnd))
			{
				this.SubclassedChildWindows.Remove(hwnd);
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000A0238 File Offset: 0x0009E438
		public override void Initialize(IComponent component)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component.GetType());
			PropertyDescriptor propertyDescriptor = properties["Visible"];
			if (propertyDescriptor == null || propertyDescriptor.PropertyType != typeof(bool) || !propertyDescriptor.ShouldSerializeValue(component))
			{
				this.Visible = true;
			}
			else
			{
				this.Visible = (bool)propertyDescriptor.GetValue(component);
			}
			PropertyDescriptor propertyDescriptor2 = properties["Enabled"];
			if (propertyDescriptor2 == null || propertyDescriptor2.PropertyType != typeof(bool) || !propertyDescriptor2.ShouldSerializeValue(component))
			{
				this.Enabled = true;
			}
			else
			{
				this.Enabled = (bool)propertyDescriptor2.GetValue(component);
			}
			this.initializing = true;
			base.Initialize(component);
			this.initializing = false;
			this.host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			AttributeCollection attributes = TypeDescriptor.GetAttributes(base.Component);
			DockingAttribute dockingAttribute = (DockingAttribute)attributes[typeof(DockingAttribute)];
			if (dockingAttribute != null && dockingAttribute.DockingBehavior != DockingBehavior.Never)
			{
				this.dockingAction = new ControlDesigner.DockingActionList(this);
				DesignerActionService designerActionService = this.GetService(typeof(DesignerActionService)) as DesignerActionService;
				if (designerActionService != null)
				{
					designerActionService.Add(base.Component, this.dockingAction);
				}
			}
			this.dataBindingsCollectionChanged = new CollectionChangeEventHandler(this.DataBindingsCollectionChanged);
			this.Control.DataBindings.CollectionChanged += this.dataBindingsCollectionChanged;
			this.Control.ControlAdded += this.OnControlAdded;
			this.Control.ControlRemoved += this.OnControlRemoved;
			this.Control.ParentChanged += this.OnParentChanged;
			this.Control.SizeChanged += this.OnSizeChanged;
			this.Control.LocationChanged += this.OnLocationChanged;
			this.DesignerTarget = new ControlDesigner.DesignerWindowTarget(this);
			if (this.Control.IsHandleCreated)
			{
				this.OnCreateHandle();
			}
			if (base.Inherited && this.host != null && this.host.RootComponent != component)
			{
				this.inheritanceUI = (InheritanceUI)this.GetService(typeof(InheritanceUI));
				if (this.inheritanceUI != null)
				{
					this.inheritanceUI.AddInheritedControl(this.Control, this.InheritanceAttribute.InheritanceLevel);
				}
			}
			if ((this.host == null || this.host.RootComponent != component) && this.ForceVisible)
			{
				this.Control.Visible = true;
			}
			this.Control.Enabled = true;
			this.Control.EnabledChanged += this.OnEnabledChanged;
			this.AllowDrop = this.Control.AllowDrop;
			this.statusCommandUI = new StatusCommandUI(component.Site);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000A0508 File Offset: 0x0009E708
		public override void InitializeExistingComponent(IDictionary defaultValues)
		{
			base.InitializeExistingComponent(defaultValues);
			foreach (object obj in this.Control.Controls)
			{
				Control control = (Control)obj;
				if (control != null)
				{
					ISite site = control.Site;
					ControlDesigner.ChildWindowTarget childWindowTarget = control.WindowTarget as ControlDesigner.ChildWindowTarget;
					if (site != null && childWindowTarget != null)
					{
						control.WindowTarget = childWindowTarget.OldWindowTarget;
					}
				}
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000A0594 File Offset: 0x0009E794
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			ISite site = base.Component.Site;
			if (site != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Text"];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string) && !propertyDescriptor.IsReadOnly && propertyDescriptor.IsBrowsable)
				{
					propertyDescriptor.SetValue(base.Component, site.Name);
				}
			}
			if (defaultValues != null)
			{
				IComponent component = defaultValues["Parent"] as IComponent;
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (component != null && designerHost != null)
				{
					ParentControlDesigner parentControlDesigner = designerHost.GetDesigner(component) as ParentControlDesigner;
					if (parentControlDesigner != null)
					{
						parentControlDesigner.AddControl(this.Control, defaultValues);
					}
					Control control = component as Control;
					if (control != null)
					{
						AttributeCollection attributes = TypeDescriptor.GetAttributes(base.Component);
						DockingAttribute dockingAttribute = (DockingAttribute)attributes[typeof(DockingAttribute)];
						if (dockingAttribute != null && dockingAttribute.DockingBehavior != DockingBehavior.Never && dockingAttribute.DockingBehavior == DockingBehavior.AutoDock)
						{
							bool flag = true;
							foreach (object obj in control.Controls)
							{
								Control control2 = (Control)obj;
								if (control2 != this.Control && control2.Dock == DockStyle.None)
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(base.Component)["Dock"];
								if (propertyDescriptor2 != null && propertyDescriptor2.IsBrowsable)
								{
									propertyDescriptor2.SetValue(base.Component, DockStyle.Fill);
								}
							}
						}
					}
				}
			}
			base.InitializeNewComponent(defaultValues);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000A075C File Offset: 0x0009E95C
		[Obsolete("This method has been deprecated. Use InitializeNewComponent instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public override void OnSetComponentDefaults()
		{
			ISite site = base.Component.Site;
			if (site != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Text"];
				if (propertyDescriptor != null && propertyDescriptor.IsBrowsable)
				{
					propertyDescriptor.SetValue(base.Component, site.Name);
				}
			}
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000A07AC File Offset: 0x0009E9AC
		private bool IsDoubleClick(int x, int y)
		{
			bool flag = false;
			int doubleClickTime = SystemInformation.DoubleClickTime;
			int num = SafeNativeMethods.GetTickCount() - this.lastClickMessageTime;
			if (num <= doubleClickTime)
			{
				Size doubleClickSize = SystemInformation.DoubleClickSize;
				if (x >= this.lastClickMessagePositionX - doubleClickSize.Width && x <= this.lastClickMessagePositionX + doubleClickSize.Width && y >= this.lastClickMessagePositionY - doubleClickSize.Height && y <= this.lastClickMessagePositionY + doubleClickSize.Height)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.lastClickMessagePositionX = x;
				this.lastClickMessagePositionY = y;
				this.lastClickMessageTime = SafeNativeMethods.GetTickCount();
			}
			else
			{
				this.lastClickMessagePositionX = (this.lastClickMessagePositionY = 0);
				this.lastClickMessageTime = 0;
			}
			return flag;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000A0856 File Offset: 0x0009EA56
		private bool IsMouseMessage(int msg)
		{
			return (msg >= 512 && msg <= 522) || (msg - 160 <= 9 || msg - 171 <= 2 || msg - 672 <= 3);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000A088C File Offset: 0x0009EA8C
		protected virtual void OnContextMenu(int x, int y)
		{
			this.ShowContextMenu(x, y);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000A0898 File Offset: 0x0009EA98
		private void OnControlAdded(object sender, ControlEventArgs e)
		{
			if (e.Control != null && this.host != null && !(this.host.GetDesigner(e.Control) is ControlDesigner))
			{
				IWindowTarget windowTarget = e.Control.WindowTarget;
				if (!(windowTarget is ControlDesigner.ChildWindowTarget))
				{
					e.Control.WindowTarget = new ControlDesigner.ChildWindowTarget(this, e.Control, windowTarget);
					e.Control.ControlAdded += this.OnControlAdded;
				}
				if (e.Control.IsHandleCreated)
				{
					Application.OleRequired();
					NativeMethods.RevokeDragDrop(e.Control.Handle);
					this.HookChildControls(e.Control);
				}
			}
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000A0948 File Offset: 0x0009EB48
		private void OnControlRemoved(object sender, ControlEventArgs e)
		{
			if (e.Control != null)
			{
				ControlDesigner.ChildWindowTarget childWindowTarget = e.Control.WindowTarget as ControlDesigner.ChildWindowTarget;
				if (childWindowTarget != null)
				{
					e.Control.WindowTarget = childWindowTarget.OldWindowTarget;
				}
				this.UnhookChildControls(e.Control);
			}
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000A098E File Offset: 0x0009EB8E
		protected virtual void OnCreateHandle()
		{
			this.OnHandleChange();
			if (this.revokeDragDrop)
			{
				NativeMethods.RevokeDragDrop(this.Control.Handle);
			}
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x000A09AF File Offset: 0x0009EBAF
		private void OnDragEnter(object s, DragEventArgs e)
		{
			if (this.BehaviorService != null)
			{
				this.BehaviorService.StartDragNotification();
			}
			this.OnDragEnter(e);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000A09CC File Offset: 0x0009EBCC
		protected virtual void OnDragEnter(DragEventArgs de)
		{
			Control control = this.Control;
			DragEventHandler value = new DragEventHandler(this.OnDragEnter);
			control.DragEnter -= value;
			((IDropTarget)this.Control).OnDragEnter(de);
			control.DragEnter += value;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000A0A07 File Offset: 0x0009EC07
		private void OnDragDrop(object s, DragEventArgs e)
		{
			if (this.BehaviorService != null)
			{
				this.BehaviorService.EndDragNotification();
			}
			this.OnDragDrop(e);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnDragComplete(DragEventArgs de)
		{
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000A0A24 File Offset: 0x0009EC24
		protected virtual void OnDragDrop(DragEventArgs de)
		{
			Control control = this.Control;
			DragEventHandler value = new DragEventHandler(this.OnDragDrop);
			control.DragDrop -= value;
			((IDropTarget)this.Control).OnDragDrop(de);
			control.DragDrop += value;
			this.OnDragComplete(de);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000A0A66 File Offset: 0x0009EC66
		private void OnDragLeave(object s, EventArgs e)
		{
			this.OnDragLeave(e);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000A0A70 File Offset: 0x0009EC70
		protected virtual void OnDragLeave(EventArgs e)
		{
			Control control = this.Control;
			EventHandler value = new EventHandler(this.OnDragLeave);
			control.DragLeave -= value;
			((IDropTarget)this.Control).OnDragLeave(e);
			control.DragLeave += value;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000A0AAB File Offset: 0x0009ECAB
		private void OnDragOver(object s, DragEventArgs e)
		{
			this.OnDragOver(e);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000A0AB4 File Offset: 0x0009ECB4
		protected virtual void OnDragOver(DragEventArgs de)
		{
			Control control = this.Control;
			DragEventHandler value = new DragEventHandler(this.OnDragOver);
			control.DragOver -= value;
			((IDropTarget)this.Control).OnDragOver(de);
			control.DragOver += value;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000A0AEF File Offset: 0x0009ECEF
		private void OnGiveFeedback(object s, GiveFeedbackEventArgs e)
		{
			this.OnGiveFeedback(e);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
		{
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000A0AF8 File Offset: 0x0009ECF8
		private void OnHandleChange()
		{
			this.HookChildHandles(NativeMethods.GetWindow(this.Control.Handle, 5));
			this.HookChildControls(this.Control);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000A0B20 File Offset: 0x0009ED20
		private void OnMouseDoubleClick()
		{
			try
			{
				this.DoDefaultAction();
			}
			catch (Exception ex)
			{
				this.DisplayError(ex);
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x000A0B5C File Offset: 0x0009ED5C
		protected virtual void OnMouseDragBegin(int x, int y)
		{
			if (this.BehaviorService == null && this.mouseDragLast != ControlDesigner.InvalidPoint)
			{
				return;
			}
			this.mouseDragLast = new Point(x, y);
			this.ctrlSelect = ((Control.ModifierKeys & Keys.Control) > Keys.None);
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (!this.ctrlSelect && selectionService != null)
			{
				selectionService.SetSelectedComponents(new object[]
				{
					base.Component
				}, SelectionTypes.Click);
			}
			this.Control.Capture = true;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x000A0BEC File Offset: 0x0009EDEC
		protected virtual void OnMouseDragEnd(bool cancel)
		{
			this.mouseDragLast = ControlDesigner.InvalidPoint;
			this.Control.Capture = false;
			if (!this.mouseDragMoved)
			{
				if (!cancel)
				{
					ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
					if ((Control.ModifierKeys & Keys.Shift) <= Keys.None && (this.ctrlSelect || (selectionService != null && !selectionService.GetComponentSelected(base.Component))))
					{
						if (selectionService != null)
						{
							selectionService.SetSelectedComponents(new object[]
							{
								base.Component
							}, SelectionTypes.Click);
						}
						this.ctrlSelect = false;
					}
				}
				return;
			}
			this.mouseDragMoved = false;
			this.ctrlSelect = false;
			if (this.BehaviorService != null && this.BehaviorService.Dragging && cancel)
			{
				this.BehaviorService.CancelDrag = true;
			}
			if (this.selectionUISvc == null)
			{
				this.selectionUISvc = (ISelectionUIService)this.GetService(typeof(ISelectionUIService));
			}
			if (this.selectionUISvc == null)
			{
				return;
			}
			if (this.selectionUISvc.Dragging)
			{
				this.selectionUISvc.EndDrag(cancel);
			}
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000A0CF8 File Offset: 0x0009EEF8
		protected virtual void OnMouseDragMove(int x, int y)
		{
			if (!this.mouseDragMoved)
			{
				Size dragSize = SystemInformation.DragSize;
				Size doubleClickSize = SystemInformation.DoubleClickSize;
				dragSize.Width = Math.Max(dragSize.Width, doubleClickSize.Width);
				dragSize.Height = Math.Max(dragSize.Height, doubleClickSize.Height);
				if (this.mouseDragLast == ControlDesigner.InvalidPoint || (Math.Abs(this.mouseDragLast.X - x) < dragSize.Width && Math.Abs(this.mouseDragLast.Y - y) < dragSize.Height))
				{
					return;
				}
				this.mouseDragMoved = true;
				this.ctrlSelect = false;
			}
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null && !base.Component.Equals(selectionService.PrimarySelection))
			{
				selectionService.SetSelectedComponents(new object[]
				{
					base.Component
				}, SelectionTypes.Click | SelectionTypes.Toggle);
			}
			if (this.BehaviorService != null && selectionService != null)
			{
				ArrayList arrayList = new ArrayList();
				ICollection selectedComponents = selectionService.GetSelectedComponents();
				Control control = null;
				foreach (object obj in selectedComponents)
				{
					IComponent component = (IComponent)obj;
					Control control2 = component as Control;
					if (control2 != null)
					{
						if (control == null)
						{
							control = control2.Parent;
						}
						else if (!control.Equals(control2.Parent))
						{
							continue;
						}
						ControlDesigner controlDesigner = this.host.GetDesigner(component) as ControlDesigner;
						if (controlDesigner != null && (controlDesigner.SelectionRules & SelectionRules.Moveable) != SelectionRules.None)
						{
							arrayList.Add(component);
						}
					}
				}
				if (arrayList.Count > 0)
				{
					using (this.BehaviorService.AdornerWindowGraphics)
					{
						DropSourceBehavior dropSourceBehavior = new DropSourceBehavior(arrayList, this.Control.Parent, this.mouseDragLast);
						this.BehaviorService.DoDragDrop(dropSourceBehavior);
					}
				}
			}
			this.mouseDragLast = ControlDesigner.InvalidPoint;
			this.mouseDragMoved = false;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000A0F1C File Offset: 0x0009F11C
		protected virtual void OnMouseEnter()
		{
			Control control = this.Control;
			Control control2 = control;
			object obj = null;
			while (obj == null && control2 != null)
			{
				control2 = control2.Parent;
				if (control2 != null)
				{
					object designer = this.host.GetDesigner(control2);
					if (designer != this)
					{
						obj = designer;
					}
				}
			}
			ControlDesigner controlDesigner = obj as ControlDesigner;
			if (controlDesigner != null)
			{
				controlDesigner.OnMouseEnter();
			}
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x000A0F6C File Offset: 0x0009F16C
		protected virtual void OnMouseHover()
		{
			Control control = this.Control;
			Control control2 = control;
			object obj = null;
			while (obj == null && control2 != null)
			{
				control2 = control2.Parent;
				if (control2 != null)
				{
					object designer = this.host.GetDesigner(control2);
					if (designer != this)
					{
						obj = designer;
					}
				}
			}
			ControlDesigner controlDesigner = obj as ControlDesigner;
			if (controlDesigner != null)
			{
				controlDesigner.OnMouseHover();
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000A0FBC File Offset: 0x0009F1BC
		protected virtual void OnMouseLeave()
		{
			Control control = this.Control;
			Control control2 = control;
			object obj = null;
			while (obj == null && control2 != null)
			{
				control2 = control2.Parent;
				if (control2 != null)
				{
					object designer = this.host.GetDesigner(control2);
					if (designer != this)
					{
						obj = designer;
					}
				}
			}
			ControlDesigner controlDesigner = obj as ControlDesigner;
			if (controlDesigner != null)
			{
				controlDesigner.OnMouseLeave();
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000A100C File Offset: 0x0009F20C
		protected virtual void OnPaintAdornments(PaintEventArgs pe)
		{
			if (this.inheritanceUI != null && pe.ClipRectangle.IntersectsWith(this.inheritanceUI.InheritanceGlyphRectangle))
			{
				pe.Graphics.DrawImage(this.inheritanceUI.InheritanceGlyph, 0, 0);
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000A1054 File Offset: 0x0009F254
		private void OnParentChanged(object sender, EventArgs e)
		{
			if (this.Control.IsHandleCreated)
			{
				this.OnHandleChange();
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x000A106C File Offset: 0x0009F26C
		private void OnSizeChanged(object sender, EventArgs e)
		{
			ComponentCache componentCache = (ComponentCache)this.GetService(typeof(ComponentCache));
			object component = base.Component;
			if (componentCache != null && component != null)
			{
				componentCache.RemoveEntry(component);
			}
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000A10A4 File Offset: 0x0009F2A4
		private void OnLocationChanged(object sender, EventArgs e)
		{
			ComponentCache componentCache = (ComponentCache)this.GetService(typeof(ComponentCache));
			object component = base.Component;
			if (componentCache != null && component != null)
			{
				componentCache.RemoveEntry(component);
			}
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000A10DC File Offset: 0x0009F2DC
		private void OnEnabledChanged(object sender, EventArgs e)
		{
			if (!this.enabledchangerecursionguard)
			{
				this.enabledchangerecursionguard = true;
				try
				{
					this.Control.Enabled = true;
				}
				finally
				{
					this.enabledchangerecursionguard = false;
				}
			}
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000A1120 File Offset: 0x0009F320
		protected virtual void OnSetCursor()
		{
			if (this.Control.Dock != DockStyle.None)
			{
				Cursor.Current = Cursors.Default;
				return;
			}
			if (this.toolboxSvc == null)
			{
				this.toolboxSvc = (IToolboxService)this.GetService(typeof(IToolboxService));
			}
			if (this.toolboxSvc != null && this.toolboxSvc.SetCursor())
			{
				return;
			}
			if (!this.locationChecked)
			{
				this.locationChecked = true;
				try
				{
					this.hasLocation = (TypeDescriptor.GetProperties(base.Component)["Location"] != null);
				}
				catch
				{
				}
			}
			if (!this.hasLocation)
			{
				Cursor.Current = Cursors.Default;
				return;
			}
			if (this.Locked)
			{
				Cursor.Current = Cursors.Default;
				return;
			}
			Cursor.Current = Cursors.SizeAll;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000A11F0 File Offset: 0x0009F3F0
		private void PaintException(PaintEventArgs e, Exception ex)
		{
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Near;
			stringFormat.LineAlignment = StringAlignment.Near;
			string text = ex.ToString();
			stringFormat.SetMeasurableCharacterRanges(new CharacterRange[]
			{
				new CharacterRange(0, text.Length)
			});
			int num = 2;
			Size iconSize = SystemInformation.IconSize;
			int num2 = num * 2;
			int num3 = num * 2;
			Rectangle clientRectangle = this.Control.ClientRectangle;
			Rectangle rect = clientRectangle;
			int num4 = rect.X;
			rect.X = num4 + 1;
			num4 = rect.Y;
			rect.Y = num4 + 1;
			rect.Width -= 2;
			rect.Height -= 2;
			Rectangle rect2 = new Rectangle(num2, num3, iconSize.Width, iconSize.Height);
			Rectangle rectangle = clientRectangle;
			rectangle.X = rect2.X + rect2.Width + 2 * num2;
			rectangle.Y = rect2.Y;
			rectangle.Width -= rectangle.X + num2 + num;
			rectangle.Height -= rectangle.Y + num3 + num;
			using (Font font = new Font(this.Control.Font.FontFamily, (float)Math.Max(SystemInformation.ToolWindowCaptionHeight - SystemInformation.BorderSize.Height - 2, this.Control.Font.Height), GraphicsUnit.Pixel))
			{
				using (Region region = e.Graphics.MeasureCharacterRanges(text, font, rectangle, stringFormat)[0])
				{
					Region clip = e.Graphics.Clip;
					e.Graphics.ExcludeClip(region);
					e.Graphics.ExcludeClip(rect2);
					try
					{
						e.Graphics.FillRectangle(Brushes.White, clientRectangle);
					}
					finally
					{
						e.Graphics.Clip = clip;
					}
					using (Pen pen = new Pen(Color.Red, (float)num))
					{
						e.Graphics.DrawRectangle(pen, rect);
					}
					Icon error = SystemIcons.Error;
					e.Graphics.FillRectangle(Brushes.White, rect2);
					e.Graphics.DrawIcon(error, rect2.X, rect2.Y);
					num4 = rectangle.X;
					rectangle.X = num4 + 1;
					e.Graphics.IntersectClip(region);
					try
					{
						e.Graphics.FillRectangle(Brushes.White, rectangle);
						e.Graphics.DrawString(text, font, new SolidBrush(this.Control.ForeColor), rectangle, stringFormat);
					}
					finally
					{
						e.Graphics.Clip = clip;
					}
				}
			}
			stringFormat.Dispose();
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000A1520 File Offset: 0x0009F720
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"Visible",
				"Enabled",
				"ContextMenu",
				"AllowDrop",
				"Location",
				"Name"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(ControlDesigner), propertyDescriptor, attributes);
				}
			}
			PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)properties["Controls"];
			if (propertyDescriptor2 != null)
			{
				Attribute[] array2 = new Attribute[propertyDescriptor2.Attributes.Count];
				propertyDescriptor2.Attributes.CopyTo(array2, 0);
				properties["Controls"] = TypeDescriptor.CreateProperty(typeof(ControlDesigner), "Controls", typeof(ControlDesigner.DesignerControlCollection), array2);
			}
			PropertyDescriptor propertyDescriptor3 = (PropertyDescriptor)properties["Size"];
			if (propertyDescriptor3 != null)
			{
				properties["Size"] = new ControlDesigner.CanResetSizePropertyDescriptor(propertyDescriptor3);
			}
			properties["Locked"] = TypeDescriptor.CreateProperty(typeof(ControlDesigner), "Locked", typeof(bool), new Attribute[]
			{
				new DefaultValueAttribute(false),
				BrowsableAttribute.Yes,
				CategoryAttribute.Design,
				DesignOnlyAttribute.Yes,
				new SRDescriptionAttribute("lockedDescr")
			});
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000A1696 File Offset: 0x0009F896
		private void ResetVisible()
		{
			this.Visible = true;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000A169F File Offset: 0x0009F89F
		private void ResetEnabled()
		{
			this.Enabled = true;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000A16A8 File Offset: 0x0009F8A8
		internal void SetUnhandledException(Control owner, Exception exception)
		{
			if (this.thrownException == null)
			{
				this.thrownException = exception;
				if (owner == null)
				{
					owner = this.Control;
				}
				string text = string.Empty;
				string[] array = exception.StackTrace.Split(new char[]
				{
					'\r',
					'\n'
				});
				string fullName = owner.GetType().FullName;
				foreach (string text2 in array)
				{
					if (text2.IndexOf(fullName) != -1)
					{
						text = string.Format(CultureInfo.CurrentCulture, "{0}\r\n{1}", new object[]
						{
							text,
							text2
						});
					}
				}
				Exception e = new Exception(SR.GetString("ControlDesigner_WndProcException", new object[]
				{
					fullName,
					exception.Message,
					text
				}), exception);
				this.DisplayError(e);
				foreach (object obj in this.Control.Controls)
				{
					Control control = (Control)obj;
					control.Visible = false;
				}
				this.Control.Invalidate(true);
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000A17DC File Offset: 0x0009F9DC
		private bool ShouldSerializeAllowDrop()
		{
			return this.AllowDrop != this.hadDragDrop;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000A17EF File Offset: 0x0009F9EF
		private bool ShouldSerializeEnabled()
		{
			return base.ShadowProperties.ShouldSerializeValue("Enabled", true);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000A1807 File Offset: 0x0009FA07
		private bool ShouldSerializeVisible()
		{
			return base.ShadowProperties.ShouldSerializeValue("Visible", true);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000A1820 File Offset: 0x0009FA20
		private bool ShouldSerializeName()
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (!this.initializing)
			{
				return base.ShadowProperties.ShouldSerializeValue("Name", null);
			}
			return base.Component != designerHost.RootComponent;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000A1870 File Offset: 0x0009FA70
		protected void UnhookChildControls(Control firstChild)
		{
			if (this.host == null)
			{
				this.host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			}
			foreach (object obj in firstChild.Controls)
			{
				Control control = (Control)obj;
				IWindowTarget windowTarget = null;
				if (control != null)
				{
					windowTarget = control.WindowTarget;
					ControlDesigner.ChildWindowTarget childWindowTarget = windowTarget as ControlDesigner.ChildWindowTarget;
					if (childWindowTarget != null)
					{
						control.WindowTarget = childWindowTarget.OldWindowTarget;
					}
				}
				if (!(windowTarget is ControlDesigner.DesignerWindowTarget))
				{
					this.UnhookChildControls(control);
				}
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x000A191C File Offset: 0x0009FB1C
		protected virtual void WndProc(ref Message m)
		{
			IMouseHandler mouseHandler = null;
			if (m.Msg == 132 && !this.inHitTest)
			{
				this.inHitTest = true;
				Point point = new Point((int)((short)NativeMethods.Util.LOWORD((int)((long)m.LParam))), (int)((short)NativeMethods.Util.HIWORD((int)((long)m.LParam))));
				try
				{
					this.liveRegion = this.GetHitTest(point);
				}
				catch (Exception ex)
				{
					this.liveRegion = false;
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
				this.inHitTest = false;
			}
			bool flag = m.Msg == 123;
			if (this.liveRegion && (this.IsMouseMessage(m.Msg) || flag))
			{
				if (m.Msg == 123)
				{
					ControlDesigner.inContextMenu = true;
				}
				try
				{
					this.DefWndProc(ref m);
				}
				finally
				{
					if (m.Msg == 123)
					{
						ControlDesigner.inContextMenu = false;
					}
					if (m.Msg == 514)
					{
						this.OnMouseDragEnd(true);
					}
				}
				return;
			}
			int num = 0;
			int num2 = 0;
			if ((m.Msg >= 512 && m.Msg <= 522) || (m.Msg >= 160 && m.Msg <= 169) || m.Msg == 32)
			{
				if (this.eventSvc == null)
				{
					this.eventSvc = (IEventHandlerService)this.GetService(typeof(IEventHandlerService));
				}
				if (this.eventSvc != null)
				{
					mouseHandler = (IMouseHandler)this.eventSvc.GetHandler(typeof(IMouseHandler));
				}
			}
			if (m.Msg >= 512 && m.Msg <= 522)
			{
				NativeMethods.POINT point2 = new NativeMethods.POINT();
				point2.x = NativeMethods.Util.SignedLOWORD((int)((long)m.LParam));
				point2.y = NativeMethods.Util.SignedHIWORD((int)((long)m.LParam));
				NativeMethods.MapWindowPoints(m.HWnd, IntPtr.Zero, point2, 1);
				num = point2.x;
				num2 = point2.y;
			}
			else if (m.Msg >= 160 && m.Msg <= 169)
			{
				num = NativeMethods.Util.SignedLOWORD((int)((long)m.LParam));
				num2 = NativeMethods.Util.SignedHIWORD((int)((long)m.LParam));
			}
			MouseButtons mouseButtons = MouseButtons.None;
			int msg = m.Msg;
			if (msg <= 32)
			{
				if (msg <= 7)
				{
					if (msg != 1)
					{
						if (msg == 5)
						{
							if (this.thrownException != null)
							{
								this.Control.Invalidate();
							}
							this.DefWndProc(ref m);
							return;
						}
						if (msg != 7)
						{
							goto IL_BA9;
						}
						if (this.host == null || this.host.RootComponent == null)
						{
							return;
						}
						IRootDesigner rootDesigner = this.host.GetDesigner(this.host.RootComponent) as IRootDesigner;
						if (rootDesigner == null)
						{
							return;
						}
						ViewTechnology[] supportedTechnologies = rootDesigner.SupportedTechnologies;
						if (supportedTechnologies.Length == 0)
						{
							return;
						}
						Control control = rootDesigner.GetView(supportedTechnologies[0]) as Control;
						if (control != null)
						{
							control.Focus();
							return;
						}
						return;
					}
					else
					{
						this.DefWndProc(ref m);
						if (m.HWnd == this.Control.Handle)
						{
							this.OnCreateHandle();
							return;
						}
						return;
					}
				}
				else if (msg != 15)
				{
					if (msg == 31)
					{
						this.OnMouseDragEnd(true);
						this.DefWndProc(ref m);
						return;
					}
					if (msg != 32)
					{
						goto IL_BA9;
					}
					goto IL_A63;
				}
			}
			else if (msg <= 134)
			{
				if (msg == 61)
				{
					if (-4 == (int)((long)m.LParam))
					{
						Guid guid = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
						try
						{
							IAccessible accessibilityObject = this.AccessibilityObject;
							if (accessibilityObject == null)
							{
								m.Result = (IntPtr)0;
							}
							else
							{
								IntPtr iunknownForObject = Marshal.GetIUnknownForObject(accessibilityObject);
								try
								{
									m.Result = UnsafeNativeMethods.LresultFromObject(ref guid, m.WParam, iunknownForObject);
								}
								finally
								{
									Marshal.Release(iunknownForObject);
								}
							}
							return;
						}
						catch (Exception ex2)
						{
							throw ex2;
						}
					}
					this.DefWndProc(ref m);
					return;
				}
				if (msg != 123)
				{
					if (msg - 133 > 1)
					{
						goto IL_BA9;
					}
					if (m.Msg == 134)
					{
						this.DefWndProc(ref m);
					}
					else if (this.thrownException == null)
					{
						this.DefWndProc(ref m);
					}
					if (this.OverlayService != null && this.Control != null && this.Control.Size != this.Control.ClientSize && this.Control.Parent != null)
					{
						Rectangle rect = new Rectangle(this.Control.Parent.PointToScreen(this.Control.Location), this.Control.Size);
						Rectangle rect2 = new Rectangle(this.Control.PointToScreen(Point.Empty), this.Control.ClientSize);
						using (Region region = new Region(rect))
						{
							region.Exclude(rect2);
							this.OverlayService.InvalidateOverlays(region);
							return;
						}
						goto IL_A63;
					}
					return;
				}
				else
				{
					if (ControlDesigner.inContextMenu)
					{
						return;
					}
					num = NativeMethods.Util.SignedLOWORD((int)((long)m.LParam));
					num2 = NativeMethods.Util.SignedHIWORD((int)((long)m.LParam));
					ToolStripKeyboardHandlingService toolStripKeyboardHandlingService = (ToolStripKeyboardHandlingService)this.GetService(typeof(ToolStripKeyboardHandlingService));
					bool flag2 = false;
					if (toolStripKeyboardHandlingService != null)
					{
						flag2 = toolStripKeyboardHandlingService.OnContextMenu(num, num2);
					}
					if (!flag2)
					{
						if (num == -1 && num2 == -1)
						{
							Point position = Cursor.Position;
							num = position.X;
							num2 = position.Y;
						}
						this.OnContextMenu(num, num2);
						return;
					}
					return;
				}
			}
			else
			{
				if (msg <= 522)
				{
					switch (msg)
					{
					case 160:
						goto IL_5D9;
					case 161:
					case 164:
						goto IL_44D;
					case 162:
					case 165:
						goto IL_6B1;
					case 163:
					case 166:
						break;
					case 167:
					case 168:
					case 169:
						return;
					default:
						switch (msg)
						{
						case 512:
							goto IL_5D9;
						case 513:
						case 516:
							goto IL_44D;
						case 514:
						case 517:
							goto IL_6B1;
						case 515:
						case 518:
							break;
						case 519:
						case 520:
						case 521:
						case 522:
							return;
						default:
							goto IL_BA9;
						}
						break;
					}
					if (m.Msg == 166 || m.Msg == 518)
					{
						mouseButtons = MouseButtons.Right;
					}
					else
					{
						mouseButtons = MouseButtons.Left;
					}
					if (mouseButtons != MouseButtons.Left)
					{
						return;
					}
					if (mouseHandler != null)
					{
						mouseHandler.OnMouseDoubleClick(base.Component);
						return;
					}
					this.OnMouseDoubleClick();
					return;
					IL_44D:
					if (m.Msg == 164 || m.Msg == 516)
					{
						mouseButtons = MouseButtons.Right;
					}
					else
					{
						mouseButtons = MouseButtons.Left;
					}
					NativeMethods.SendMessage(this.Control.Handle, 7, 0, 0);
					if (mouseButtons == MouseButtons.Left && this.IsDoubleClick(num, num2))
					{
						if (mouseHandler != null)
						{
							mouseHandler.OnMouseDoubleClick(base.Component);
							return;
						}
						this.OnMouseDoubleClick();
						return;
					}
					else
					{
						this.toolPassThrough = false;
						if (!this.EnableDragRect && mouseButtons == MouseButtons.Left)
						{
							if (this.toolboxSvc == null)
							{
								this.toolboxSvc = (IToolboxService)this.GetService(typeof(IToolboxService));
							}
							if (this.toolboxSvc != null && this.toolboxSvc.GetSelectedToolboxItem((IDesignerHost)this.GetService(typeof(IDesignerHost))) != null)
							{
								this.toolPassThrough = true;
							}
						}
						else
						{
							this.toolPassThrough = false;
						}
						if (this.toolPassThrough)
						{
							NativeMethods.SendMessage(this.Control.Parent.Handle, m.Msg, m.WParam, (IntPtr)this.GetParentPointFromLparam(m.LParam));
							return;
						}
						if (mouseHandler != null)
						{
							mouseHandler.OnMouseDown(base.Component, mouseButtons, num, num2);
						}
						else if (mouseButtons == MouseButtons.Left)
						{
							this.OnMouseDragBegin(num, num2);
						}
						else if (mouseButtons == MouseButtons.Right)
						{
							ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
							if (selectionService != null)
							{
								selectionService.SetSelectedComponents(new object[]
								{
									base.Component
								}, SelectionTypes.Click);
							}
						}
						this.lastMoveScreenX = num;
						this.lastMoveScreenY = num2;
						return;
					}
					IL_5D9:
					if (((int)((long)m.WParam) & 1) != 0)
					{
						mouseButtons = MouseButtons.Left;
					}
					else if (((int)((long)m.WParam) & 2) != 0)
					{
						mouseButtons = MouseButtons.Right;
						this.toolPassThrough = false;
					}
					else
					{
						this.toolPassThrough = false;
					}
					if (this.lastMoveScreenX != num || this.lastMoveScreenY != num2)
					{
						if (this.toolPassThrough)
						{
							NativeMethods.SendMessage(this.Control.Parent.Handle, m.Msg, m.WParam, (IntPtr)this.GetParentPointFromLparam(m.LParam));
							return;
						}
						if (mouseHandler != null)
						{
							mouseHandler.OnMouseMove(base.Component, num, num2);
						}
						else if (mouseButtons == MouseButtons.Left)
						{
							this.OnMouseDragMove(num, num2);
						}
					}
					this.lastMoveScreenX = num;
					this.lastMoveScreenY = num2;
					if (m.Msg == 512)
					{
						this.BaseWndProc(ref m);
						return;
					}
					return;
					IL_6B1:
					if (m.Msg == 165 || m.Msg == 517)
					{
						mouseButtons = MouseButtons.Right;
					}
					else
					{
						mouseButtons = MouseButtons.Left;
					}
					if (mouseHandler != null)
					{
						mouseHandler.OnMouseUp(base.Component, mouseButtons);
					}
					else
					{
						if (this.toolPassThrough)
						{
							NativeMethods.SendMessage(this.Control.Parent.Handle, m.Msg, m.WParam, (IntPtr)this.GetParentPointFromLparam(m.LParam));
							this.toolPassThrough = false;
							return;
						}
						if (mouseButtons == MouseButtons.Left)
						{
							this.OnMouseDragEnd(false);
						}
					}
					this.toolPassThrough = false;
					this.BaseWndProc(ref m);
					return;
				}
				switch (msg)
				{
				case 672:
				case 674:
					return;
				case 673:
					if (mouseHandler != null)
					{
						mouseHandler.OnMouseHover(base.Component);
						return;
					}
					this.OnMouseHover();
					return;
				case 675:
					this.OnMouseLeave();
					this.BaseWndProc(ref m);
					return;
				default:
					if (msg != 792)
					{
						goto IL_BA9;
					}
					using (Graphics graphics = Graphics.FromHdc(m.WParam))
					{
						using (PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, this.Control.ClientRectangle))
						{
							this.DefWndProc(ref m);
							this.OnPaintAdornments(paintEventArgs);
							return;
						}
					}
					break;
				}
			}
			if (OleDragDropHandler.FreezePainting)
			{
				NativeMethods.ValidateRect(m.HWnd, IntPtr.Zero);
				return;
			}
			if (this.Control == null)
			{
				return;
			}
			NativeMethods.RECT rect3 = default(NativeMethods.RECT);
			IntPtr intPtr = NativeMethods.CreateRectRgn(0, 0, 0, 0);
			NativeMethods.GetUpdateRgn(m.HWnd, intPtr, false);
			NativeMethods.GetUpdateRect(m.HWnd, ref rect3, false);
			Region region2 = Region.FromHrgn(intPtr);
			Rectangle rectangle = Rectangle.Empty;
			try
			{
				if (this.thrownException == null)
				{
					this.DefWndProc(ref m);
				}
				Graphics graphics2 = Graphics.FromHwnd(m.HWnd);
				try
				{
					if (m.HWnd != this.Control.Handle)
					{
						NativeMethods.POINT point3 = new NativeMethods.POINT();
						point3.x = 0;
						point3.y = 0;
						NativeMethods.MapWindowPoints(m.HWnd, this.Control.Handle, point3, 1);
						graphics2.TranslateTransform((float)(-(float)point3.x), (float)(-(float)point3.y));
						NativeMethods.MapWindowPoints(m.HWnd, this.Control.Handle, ref rect3, 2);
					}
					rectangle = new Rectangle(rect3.left, rect3.top, rect3.right - rect3.left, rect3.bottom - rect3.top);
					PaintEventArgs paintEventArgs2 = new PaintEventArgs(graphics2, rectangle);
					try
					{
						graphics2.Clip = region2;
						if (this.thrownException == null)
						{
							this.OnPaintAdornments(paintEventArgs2);
						}
						else
						{
							UnsafeNativeMethods.PAINTSTRUCT paintstruct = default(UnsafeNativeMethods.PAINTSTRUCT);
							UnsafeNativeMethods.BeginPaint(m.HWnd, ref paintstruct);
							this.PaintException(paintEventArgs2, this.thrownException);
							UnsafeNativeMethods.EndPaint(m.HWnd, ref paintstruct);
						}
					}
					finally
					{
						paintEventArgs2.Dispose();
					}
				}
				finally
				{
					graphics2.Dispose();
				}
			}
			finally
			{
				region2.Dispose();
				NativeMethods.DeleteObject(intPtr);
			}
			if (this.OverlayService != null)
			{
				rectangle.Location = this.Control.PointToScreen(rectangle.Location);
				this.OverlayService.InvalidateOverlays(rectangle);
				return;
			}
			return;
			IL_A63:
			if (this.liveRegion)
			{
				this.DefWndProc(ref m);
				return;
			}
			if (mouseHandler != null)
			{
				mouseHandler.OnSetCursor(base.Component);
				return;
			}
			this.OnSetCursor();
			return;
			IL_BA9:
			if (m.Msg == NativeMethods.WM_MOUSEENTER)
			{
				this.OnMouseEnter();
				this.BaseWndProc(ref m);
				return;
			}
			if (m.Msg < 256 || m.Msg > 264)
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x04001614 RID: 5652
		protected static readonly Point InvalidPoint = new Point(int.MinValue, int.MinValue);

		// Token: 0x04001615 RID: 5653
		private static int currentProcessId;

		// Token: 0x04001616 RID: 5654
		private IDesignerHost host;

		// Token: 0x04001617 RID: 5655
		private ControlDesigner.IDesignerTarget designerTarget;

		// Token: 0x04001618 RID: 5656
		private bool liveRegion;

		// Token: 0x04001619 RID: 5657
		private bool inHitTest;

		// Token: 0x0400161A RID: 5658
		private bool hasLocation;

		// Token: 0x0400161B RID: 5659
		private bool locationChecked;

		// Token: 0x0400161C RID: 5660
		private bool locked;

		// Token: 0x0400161D RID: 5661
		private bool initializing;

		// Token: 0x0400161E RID: 5662
		private bool enabledchangerecursionguard;

		// Token: 0x0400161F RID: 5663
		private BehaviorService behaviorService;

		// Token: 0x04001620 RID: 5664
		private ResizeBehavior resizeBehavior;

		// Token: 0x04001621 RID: 5665
		private ContainerSelectorBehavior moveBehavior;

		// Token: 0x04001622 RID: 5666
		private ISelectionUIService selectionUISvc;

		// Token: 0x04001623 RID: 5667
		private IEventHandlerService eventSvc;

		// Token: 0x04001624 RID: 5668
		private IToolboxService toolboxSvc;

		// Token: 0x04001625 RID: 5669
		private InheritanceUI inheritanceUI;

		// Token: 0x04001626 RID: 5670
		private IOverlayService overlayService;

		// Token: 0x04001627 RID: 5671
		private Point mouseDragLast = ControlDesigner.InvalidPoint;

		// Token: 0x04001628 RID: 5672
		private bool mouseDragMoved;

		// Token: 0x04001629 RID: 5673
		private int lastMoveScreenX;

		// Token: 0x0400162A RID: 5674
		private int lastMoveScreenY;

		// Token: 0x0400162B RID: 5675
		private int lastClickMessageTime;

		// Token: 0x0400162C RID: 5676
		private int lastClickMessagePositionX;

		// Token: 0x0400162D RID: 5677
		private int lastClickMessagePositionY;

		// Token: 0x0400162E RID: 5678
		private Point downPos = Point.Empty;

		// Token: 0x04001630 RID: 5680
		private CollectionChangeEventHandler dataBindingsCollectionChanged;

		// Token: 0x04001631 RID: 5681
		private Exception thrownException;

		// Token: 0x04001632 RID: 5682
		private bool ctrlSelect;

		// Token: 0x04001633 RID: 5683
		private bool toolPassThrough;

		// Token: 0x04001634 RID: 5684
		private bool removalNotificationHooked;

		// Token: 0x04001635 RID: 5685
		private bool revokeDragDrop = true;

		// Token: 0x04001636 RID: 5686
		private bool hadDragDrop;

		// Token: 0x04001637 RID: 5687
		private ControlDesigner.DesignerControlCollection controls;

		// Token: 0x04001638 RID: 5688
		private static bool inContextMenu = false;

		// Token: 0x04001639 RID: 5689
		private ControlDesigner.DockingActionList dockingAction;

		// Token: 0x0400163A RID: 5690
		private StatusCommandUI statusCommandUI;

		// Token: 0x0400163B RID: 5691
		private bool forceVisible = true;

		// Token: 0x0400163C RID: 5692
		private bool autoResizeHandles;

		// Token: 0x0400163D RID: 5693
		private Dictionary<IntPtr, bool> subclassedChildren;

		// Token: 0x0400163E RID: 5694
		protected AccessibleObject accessibilityObj;

		// Token: 0x02000541 RID: 1345
		private class ChildSubClass : NativeWindow, ControlDesigner.IDesignerTarget, IDisposable
		{
			// Token: 0x060030D4 RID: 12500 RVA: 0x0010C188 File Offset: 0x0010A388
			public ChildSubClass(ControlDesigner designer, IntPtr hwnd)
			{
				this.designer = designer;
				if (designer != null)
				{
					designer.disposingHandler += this.OnDesignerDisposing;
				}
				base.AssignHandle(hwnd);
			}

			// Token: 0x060030D5 RID: 12501 RVA: 0x0010C1B3 File Offset: 0x0010A3B3
			void ControlDesigner.IDesignerTarget.DefWndProc(ref Message m)
			{
				base.DefWndProc(ref m);
			}

			// Token: 0x060030D6 RID: 12502 RVA: 0x0010C1BC File Offset: 0x0010A3BC
			public void Dispose()
			{
				this.designer = null;
			}

			// Token: 0x060030D7 RID: 12503 RVA: 0x0010C1C5 File Offset: 0x0010A3C5
			private void OnDesignerDisposing(object sender, EventArgs e)
			{
				this.Dispose();
			}

			// Token: 0x060030D8 RID: 12504 RVA: 0x0010C1D0 File Offset: 0x0010A3D0
			protected override void WndProc(ref Message m)
			{
				if (this.designer == null)
				{
					base.DefWndProc(ref m);
					return;
				}
				if (m.Msg == 2)
				{
					this.designer.RemoveSubclassedWindow(m.HWnd);
				}
				if (m.Msg == 528 && NativeMethods.Util.LOWORD((int)((long)m.WParam)) == 1)
				{
					this.designer.HookChildHandles(m.LParam);
				}
				ControlDesigner.IDesignerTarget designerTarget = this.designer.DesignerTarget;
				this.designer.DesignerTarget = this;
				try
				{
					this.designer.WndProc(ref m);
				}
				catch (Exception exception)
				{
					this.designer.SetUnhandledException(Control.FromChildHandle(m.HWnd), exception);
				}
				finally
				{
					if (this.designer != null && this.designer.Component != null)
					{
						this.designer.DesignerTarget = designerTarget;
					}
				}
			}

			// Token: 0x04002111 RID: 8465
			private ControlDesigner designer;
		}

		// Token: 0x02000542 RID: 1346
		private class ChildWindowTarget : IWindowTarget, ControlDesigner.IDesignerTarget, IDisposable
		{
			// Token: 0x060030D9 RID: 12505 RVA: 0x0010C2B8 File Offset: 0x0010A4B8
			public ChildWindowTarget(ControlDesigner designer, Control childControl, IWindowTarget oldWindowTarget)
			{
				this.designer = designer;
				this.childControl = childControl;
				this.oldWindowTarget = oldWindowTarget;
			}

			// Token: 0x1700096E RID: 2414
			// (get) Token: 0x060030DA RID: 12506 RVA: 0x0010C2E0 File Offset: 0x0010A4E0
			public IWindowTarget OldWindowTarget
			{
				get
				{
					return this.oldWindowTarget;
				}
			}

			// Token: 0x060030DB RID: 12507 RVA: 0x0010C2E8 File Offset: 0x0010A4E8
			public void DefWndProc(ref Message m)
			{
				this.oldWindowTarget.OnMessage(ref m);
			}

			// Token: 0x060030DC RID: 12508 RVA: 0x00003937 File Offset: 0x00001B37
			public void Dispose()
			{
			}

			// Token: 0x060030DD RID: 12509 RVA: 0x0010C2F6 File Offset: 0x0010A4F6
			public void OnHandleChange(IntPtr newHandle)
			{
				this.handle = newHandle;
				this.oldWindowTarget.OnHandleChange(newHandle);
			}

			// Token: 0x060030DE RID: 12510 RVA: 0x0010C30C File Offset: 0x0010A50C
			public void OnMessage(ref Message m)
			{
				if (this.designer.Component == null)
				{
					this.oldWindowTarget.OnMessage(ref m);
					return;
				}
				ControlDesigner.IDesignerTarget designerTarget = this.designer.DesignerTarget;
				this.designer.DesignerTarget = this;
				try
				{
					this.designer.WndProc(ref m);
				}
				catch (Exception exception)
				{
					this.designer.SetUnhandledException(this.childControl, exception);
				}
				finally
				{
					if (this.designer.DesignerTarget == null)
					{
						designerTarget.Dispose();
					}
					else
					{
						this.designer.DesignerTarget = designerTarget;
					}
					if (m.Msg == 1)
					{
						NativeMethods.RevokeDragDrop(this.handle);
					}
				}
			}

			// Token: 0x04002112 RID: 8466
			private ControlDesigner designer;

			// Token: 0x04002113 RID: 8467
			private Control childControl;

			// Token: 0x04002114 RID: 8468
			private IWindowTarget oldWindowTarget;

			// Token: 0x04002115 RID: 8469
			private IntPtr handle = IntPtr.Zero;
		}

		// Token: 0x02000543 RID: 1347
		private interface IDesignerTarget : IDisposable
		{
			// Token: 0x060030DF RID: 12511
			void DefWndProc(ref Message m);
		}

		// Token: 0x02000544 RID: 1348
		private class DesignerWindowTarget : IWindowTarget, ControlDesigner.IDesignerTarget, IDisposable
		{
			// Token: 0x060030E0 RID: 12512 RVA: 0x0010C3C4 File Offset: 0x0010A5C4
			public DesignerWindowTarget(ControlDesigner designer)
			{
				Control control = designer.Control;
				this.designer = designer;
				this.oldTarget = control.WindowTarget;
				control.WindowTarget = this;
			}

			// Token: 0x060030E1 RID: 12513 RVA: 0x0010C3F8 File Offset: 0x0010A5F8
			public void DefWndProc(ref Message m)
			{
				this.oldTarget.OnMessage(ref m);
			}

			// Token: 0x060030E2 RID: 12514 RVA: 0x0010C406 File Offset: 0x0010A606
			public void Dispose()
			{
				if (this.designer != null)
				{
					this.designer.Control.WindowTarget = this.oldTarget;
					this.designer = null;
				}
			}

			// Token: 0x060030E3 RID: 12515 RVA: 0x0010C42D File Offset: 0x0010A62D
			public void OnHandleChange(IntPtr newHandle)
			{
				this.oldTarget.OnHandleChange(newHandle);
				if (newHandle != IntPtr.Zero)
				{
					this.designer.OnHandleChange();
				}
			}

			// Token: 0x060030E4 RID: 12516 RVA: 0x0010C454 File Offset: 0x0010A654
			public void OnMessage(ref Message m)
			{
				ControlDesigner controlDesigner = this.designer;
				if (controlDesigner != null)
				{
					ControlDesigner.IDesignerTarget designerTarget = controlDesigner.DesignerTarget;
					controlDesigner.DesignerTarget = this;
					try
					{
						controlDesigner.WndProc(ref m);
						return;
					}
					catch (Exception exception)
					{
						controlDesigner.SetUnhandledException(controlDesigner.Control, exception);
						return;
					}
					finally
					{
						controlDesigner.DesignerTarget = designerTarget;
					}
				}
				this.DefWndProc(ref m);
			}

			// Token: 0x04002116 RID: 8470
			internal ControlDesigner designer;

			// Token: 0x04002117 RID: 8471
			internal IWindowTarget oldTarget;
		}

		// Token: 0x02000545 RID: 1349
		[ComVisible(true)]
		public class ControlDesignerAccessibleObject : AccessibleObject
		{
			// Token: 0x060030E5 RID: 12517 RVA: 0x0010C4C0 File Offset: 0x0010A6C0
			public ControlDesignerAccessibleObject(ControlDesigner designer, Control control)
			{
				this.designer = designer;
				this.control = control;
			}

			// Token: 0x1700096F RID: 2415
			// (get) Token: 0x060030E6 RID: 12518 RVA: 0x0010C4D6 File Offset: 0x0010A6D6
			public override Rectangle Bounds
			{
				get
				{
					return this.control.AccessibilityObject.Bounds;
				}
			}

			// Token: 0x17000970 RID: 2416
			// (get) Token: 0x060030E7 RID: 12519 RVA: 0x0010C4E8 File Offset: 0x0010A6E8
			public override string Description
			{
				get
				{
					return this.control.AccessibilityObject.Description;
				}
			}

			// Token: 0x17000971 RID: 2417
			// (get) Token: 0x060030E8 RID: 12520 RVA: 0x0010C4FA File Offset: 0x0010A6FA
			private IDesignerHost DesignerHost
			{
				get
				{
					if (this.host == null)
					{
						this.host = (IDesignerHost)this.designer.GetService(typeof(IDesignerHost));
					}
					return this.host;
				}
			}

			// Token: 0x17000972 RID: 2418
			// (get) Token: 0x060030E9 RID: 12521 RVA: 0x0010C52A File Offset: 0x0010A72A
			public override string DefaultAction
			{
				get
				{
					return "";
				}
			}

			// Token: 0x17000973 RID: 2419
			// (get) Token: 0x060030EA RID: 12522 RVA: 0x0010C531 File Offset: 0x0010A731
			public override string Name
			{
				get
				{
					return this.control.Name;
				}
			}

			// Token: 0x17000974 RID: 2420
			// (get) Token: 0x060030EB RID: 12523 RVA: 0x0010C53E File Offset: 0x0010A73E
			public override AccessibleObject Parent
			{
				get
				{
					return this.control.AccessibilityObject.Parent;
				}
			}

			// Token: 0x17000975 RID: 2421
			// (get) Token: 0x060030EC RID: 12524 RVA: 0x0010C550 File Offset: 0x0010A750
			public override AccessibleRole Role
			{
				get
				{
					return this.control.AccessibilityObject.Role;
				}
			}

			// Token: 0x17000976 RID: 2422
			// (get) Token: 0x060030ED RID: 12525 RVA: 0x0010C562 File Offset: 0x0010A762
			private ISelectionService SelectionService
			{
				get
				{
					if (this.selSvc == null)
					{
						this.selSvc = (ISelectionService)this.designer.GetService(typeof(ISelectionService));
					}
					return this.selSvc;
				}
			}

			// Token: 0x17000977 RID: 2423
			// (get) Token: 0x060030EE RID: 12526 RVA: 0x0010C594 File Offset: 0x0010A794
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = this.control.AccessibilityObject.State;
					ISelectionService selectionService = this.SelectionService;
					if (selectionService != null)
					{
						if (selectionService.GetComponentSelected(this.control))
						{
							accessibleStates |= AccessibleStates.Selected;
						}
						if (selectionService.PrimarySelection == this.control)
						{
							accessibleStates |= AccessibleStates.Focused;
						}
					}
					return accessibleStates;
				}
			}

			// Token: 0x17000978 RID: 2424
			// (get) Token: 0x060030EF RID: 12527 RVA: 0x0010C5E1 File Offset: 0x0010A7E1
			public override string Value
			{
				get
				{
					return this.control.AccessibilityObject.Value;
				}
			}

			// Token: 0x060030F0 RID: 12528 RVA: 0x0010C5F4 File Offset: 0x0010A7F4
			public override AccessibleObject GetChild(int index)
			{
				Control.ControlAccessibleObject controlAccessibleObject = this.control.AccessibilityObject.GetChild(index) as Control.ControlAccessibleObject;
				if (controlAccessibleObject != null)
				{
					AccessibleObject designerAccessibleObject = this.GetDesignerAccessibleObject(controlAccessibleObject);
					if (designerAccessibleObject != null)
					{
						return designerAccessibleObject;
					}
				}
				return this.control.AccessibilityObject.GetChild(index);
			}

			// Token: 0x060030F1 RID: 12529 RVA: 0x0010C639 File Offset: 0x0010A839
			public override int GetChildCount()
			{
				return this.control.AccessibilityObject.GetChildCount();
			}

			// Token: 0x060030F2 RID: 12530 RVA: 0x0010C64C File Offset: 0x0010A84C
			private AccessibleObject GetDesignerAccessibleObject(Control.ControlAccessibleObject cao)
			{
				if (cao == null)
				{
					return null;
				}
				ControlDesigner controlDesigner = this.DesignerHost.GetDesigner(cao.Owner) as ControlDesigner;
				if (controlDesigner != null)
				{
					return controlDesigner.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x060030F3 RID: 12531 RVA: 0x0010C680 File Offset: 0x0010A880
			public override AccessibleObject GetFocused()
			{
				if ((this.State & AccessibleStates.Focused) != AccessibleStates.None)
				{
					return this;
				}
				return base.GetFocused();
			}

			// Token: 0x060030F4 RID: 12532 RVA: 0x0010C694 File Offset: 0x0010A894
			public override AccessibleObject GetSelected()
			{
				if ((this.State & AccessibleStates.Selected) != AccessibleStates.None)
				{
					return this;
				}
				return base.GetFocused();
			}

			// Token: 0x060030F5 RID: 12533 RVA: 0x0010C6A8 File Offset: 0x0010A8A8
			public override AccessibleObject HitTest(int x, int y)
			{
				return this.control.AccessibilityObject.HitTest(x, y);
			}

			// Token: 0x04002118 RID: 8472
			private ControlDesigner designer;

			// Token: 0x04002119 RID: 8473
			private Control control;

			// Token: 0x0400211A RID: 8474
			private IDesignerHost host;

			// Token: 0x0400211B RID: 8475
			private ISelectionService selSvc;
		}

		// Token: 0x02000546 RID: 1350
		[ListBindable(false)]
		[DesignerSerializer(typeof(ControlDesigner.DesignerControlCollectionCodeDomSerializer), typeof(CodeDomSerializer))]
		internal class DesignerControlCollection : Control.ControlCollection, IList, ICollection, IEnumerable
		{
			// Token: 0x060030F6 RID: 12534 RVA: 0x0010C6BC File Offset: 0x0010A8BC
			public DesignerControlCollection(Control owner) : base(owner)
			{
				this.realCollection = owner.Controls;
			}

			// Token: 0x17000979 RID: 2425
			// (get) Token: 0x060030F7 RID: 12535 RVA: 0x0010C6D1 File Offset: 0x0010A8D1
			public override int Count
			{
				get
				{
					return this.realCollection.Count;
				}
			}

			// Token: 0x1700097A RID: 2426
			// (get) Token: 0x060030F8 RID: 12536 RVA: 0x0000CA50 File Offset: 0x0000AC50
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700097B RID: 2427
			// (get) Token: 0x060030F9 RID: 12537 RVA: 0x0000445B File Offset: 0x0000265B
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700097C RID: 2428
			// (get) Token: 0x060030FA RID: 12538 RVA: 0x0000445B File Offset: 0x0000265B
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700097D RID: 2429
			// (get) Token: 0x060030FB RID: 12539 RVA: 0x0010C6DE File Offset: 0x0010A8DE
			public new bool IsReadOnly
			{
				get
				{
					return this.realCollection.IsReadOnly;
				}
			}

			// Token: 0x060030FC RID: 12540 RVA: 0x0010C6EB File Offset: 0x0010A8EB
			int IList.Add(object control)
			{
				return ((IList)this.realCollection).Add(control);
			}

			// Token: 0x060030FD RID: 12541 RVA: 0x0010C6F9 File Offset: 0x0010A8F9
			public override void Add(Control c)
			{
				this.realCollection.Add(c);
			}

			// Token: 0x060030FE RID: 12542 RVA: 0x0010C707 File Offset: 0x0010A907
			public override void AddRange(Control[] controls)
			{
				this.realCollection.AddRange(controls);
			}

			// Token: 0x060030FF RID: 12543 RVA: 0x0010C715 File Offset: 0x0010A915
			bool IList.Contains(object control)
			{
				return ((IList)this.realCollection).Contains(control);
			}

			// Token: 0x06003100 RID: 12544 RVA: 0x0010C723 File Offset: 0x0010A923
			public new void CopyTo(Array dest, int index)
			{
				this.realCollection.CopyTo(dest, index);
			}

			// Token: 0x06003101 RID: 12545 RVA: 0x0010C732 File Offset: 0x0010A932
			public override bool Equals(object other)
			{
				return this.realCollection.Equals(other);
			}

			// Token: 0x06003102 RID: 12546 RVA: 0x0010C740 File Offset: 0x0010A940
			public new IEnumerator GetEnumerator()
			{
				return this.realCollection.GetEnumerator();
			}

			// Token: 0x06003103 RID: 12547 RVA: 0x0010C74D File Offset: 0x0010A94D
			public override int GetHashCode()
			{
				return this.realCollection.GetHashCode();
			}

			// Token: 0x06003104 RID: 12548 RVA: 0x0010C75A File Offset: 0x0010A95A
			int IList.IndexOf(object control)
			{
				return ((IList)this.realCollection).IndexOf(control);
			}

			// Token: 0x06003105 RID: 12549 RVA: 0x0010C768 File Offset: 0x0010A968
			void IList.Insert(int index, object value)
			{
				((IList)this.realCollection).Insert(index, value);
			}

			// Token: 0x06003106 RID: 12550 RVA: 0x0010C777 File Offset: 0x0010A977
			void IList.Remove(object control)
			{
				((IList)this.realCollection).Remove(control);
			}

			// Token: 0x06003107 RID: 12551 RVA: 0x0010C785 File Offset: 0x0010A985
			void IList.RemoveAt(int index)
			{
				((IList)this.realCollection).RemoveAt(index);
			}

			// Token: 0x1700097E RID: 2430
			object IList.this[int index]
			{
				get
				{
					return ((IList)this.realCollection)[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x0600310A RID: 12554 RVA: 0x0010C7A1 File Offset: 0x0010A9A1
			public override int GetChildIndex(Control child, bool throwException)
			{
				return this.realCollection.GetChildIndex(child, throwException);
			}

			// Token: 0x0600310B RID: 12555 RVA: 0x0010C7B0 File Offset: 0x0010A9B0
			public override void SetChildIndex(Control child, int newIndex)
			{
				this.realCollection.SetChildIndex(child, newIndex);
			}

			// Token: 0x0600310C RID: 12556 RVA: 0x0010C7C0 File Offset: 0x0010A9C0
			public override void Clear()
			{
				for (int i = this.realCollection.Count - 1; i >= 0; i--)
				{
					if (this.realCollection[i] != null && this.realCollection[i].Site != null && TypeDescriptor.GetAttributes(this.realCollection[i]).Contains(InheritanceAttribute.NotInherited))
					{
						this.realCollection.RemoveAt(i);
					}
				}
			}

			// Token: 0x0400211C RID: 8476
			private Control.ControlCollection realCollection;
		}

		// Token: 0x02000547 RID: 1351
		internal class DesignerControlCollectionCodeDomSerializer : CollectionCodeDomSerializer
		{
			// Token: 0x0600310D RID: 12557 RVA: 0x0010C830 File Offset: 0x0010AA30
			protected override object SerializeCollection(IDesignerSerializationManager manager, CodeExpression targetExpression, Type targetType, ICollection originalCollection, ICollection valuesToSerialize)
			{
				ArrayList arrayList = new ArrayList();
				if (valuesToSerialize != null && valuesToSerialize.Count > 0)
				{
					foreach (object obj in valuesToSerialize)
					{
						IComponent component = obj as IComponent;
						if (component != null && component.Site != null && !(component.Site is INestedSite))
						{
							arrayList.Add(component);
						}
					}
				}
				return base.SerializeCollection(manager, targetExpression, targetType, originalCollection, arrayList);
			}
		}

		// Token: 0x02000548 RID: 1352
		private class DockingActionList : DesignerActionList
		{
			// Token: 0x0600310F RID: 12559 RVA: 0x0010C8C4 File Offset: 0x0010AAC4
			public DockingActionList(ControlDesigner owner) : base(owner.Component)
			{
				this._designer = owner;
				this._host = (base.GetService(typeof(IDesignerHost)) as IDesignerHost);
			}

			// Token: 0x06003110 RID: 12560 RVA: 0x0010C8F4 File Offset: 0x0010AAF4
			private string GetActionName()
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Dock"];
				if (propertyDescriptor == null)
				{
					return null;
				}
				DockStyle dockStyle = (DockStyle)propertyDescriptor.GetValue(base.Component);
				if (dockStyle == DockStyle.Fill)
				{
					return SR.GetString("DesignerShortcutUndockInParent");
				}
				return SR.GetString("DesignerShortcutDockInParent");
			}

			// Token: 0x06003111 RID: 12561 RVA: 0x0010C948 File Offset: 0x0010AB48
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				string actionName = this.GetActionName();
				if (actionName != null)
				{
					designerActionItemCollection.Add(new DesignerActionVerbItem(new DesignerVerb(this.GetActionName(), new EventHandler(this.OnDockActionClick))));
				}
				return designerActionItemCollection;
			}

			// Token: 0x06003112 RID: 12562 RVA: 0x0010C98C File Offset: 0x0010AB8C
			private void OnDockActionClick(object sender, EventArgs e)
			{
				DesignerVerb designerVerb = sender as DesignerVerb;
				if (designerVerb != null && this._host != null)
				{
					using (DesignerTransaction designerTransaction = this._host.CreateTransaction(designerVerb.Text))
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Dock"];
						DockStyle dockStyle = (DockStyle)propertyDescriptor.GetValue(base.Component);
						if (dockStyle == DockStyle.Fill)
						{
							propertyDescriptor.SetValue(base.Component, DockStyle.None);
						}
						else
						{
							propertyDescriptor.SetValue(base.Component, DockStyle.Fill);
						}
						designerTransaction.Commit();
					}
				}
			}

			// Token: 0x0400211D RID: 8477
			private ControlDesigner _designer;

			// Token: 0x0400211E RID: 8478
			private IDesignerHost _host;
		}

		// Token: 0x02000549 RID: 1353
		internal class TransparentBehavior : Behavior
		{
			// Token: 0x06003113 RID: 12563 RVA: 0x0010CA34 File Offset: 0x0010AC34
			internal TransparentBehavior(ControlDesigner designer)
			{
				this.designer = designer;
			}

			// Token: 0x06003114 RID: 12564 RVA: 0x0010CA4E File Offset: 0x0010AC4E
			internal bool IsTransparent(Point p)
			{
				return this.designer.GetHitTest(p);
			}

			// Token: 0x06003115 RID: 12565 RVA: 0x0010CA5C File Offset: 0x0010AC5C
			public override void OnDragDrop(Glyph g, DragEventArgs e)
			{
				this.controlRect = Rectangle.Empty;
				this.designer.OnDragDrop(e);
			}

			// Token: 0x06003116 RID: 12566 RVA: 0x0010CA78 File Offset: 0x0010AC78
			public override void OnDragEnter(Glyph g, DragEventArgs e)
			{
				if (this.designer != null && this.designer.Control != null)
				{
					this.controlRect = this.designer.Control.RectangleToScreen(this.designer.Control.ClientRectangle);
				}
				this.designer.OnDragEnter(e);
			}

			// Token: 0x06003117 RID: 12567 RVA: 0x0010CACC File Offset: 0x0010ACCC
			public override void OnDragLeave(Glyph g, EventArgs e)
			{
				this.controlRect = Rectangle.Empty;
				this.designer.OnDragLeave(e);
			}

			// Token: 0x06003118 RID: 12568 RVA: 0x0010CAE8 File Offset: 0x0010ACE8
			public override void OnDragOver(Glyph g, DragEventArgs e)
			{
				if (e != null && this.controlRect != Rectangle.Empty && !this.controlRect.Contains(new Point(e.X, e.Y)))
				{
					e.Effect = DragDropEffects.None;
					return;
				}
				this.designer.OnDragOver(e);
			}

			// Token: 0x06003119 RID: 12569 RVA: 0x0010CB3C File Offset: 0x0010AD3C
			public override void OnGiveFeedback(Glyph g, GiveFeedbackEventArgs e)
			{
				this.designer.OnGiveFeedback(e);
			}

			// Token: 0x0400211F RID: 8479
			private ControlDesigner designer;

			// Token: 0x04002120 RID: 8480
			private Rectangle controlRect = Rectangle.Empty;
		}

		// Token: 0x0200054A RID: 1354
		private class CanResetSizePropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x0600311A RID: 12570 RVA: 0x0010CB4A File Offset: 0x0010AD4A
			public CanResetSizePropertyDescriptor(PropertyDescriptor pd) : base(pd)
			{
				this._basePropDesc = pd;
			}

			// Token: 0x1700097F RID: 2431
			// (get) Token: 0x0600311B RID: 12571 RVA: 0x0010CB5A File Offset: 0x0010AD5A
			public override Type ComponentType
			{
				get
				{
					return this._basePropDesc.ComponentType;
				}
			}

			// Token: 0x17000980 RID: 2432
			// (get) Token: 0x0600311C RID: 12572 RVA: 0x0010CB67 File Offset: 0x0010AD67
			public override string DisplayName
			{
				get
				{
					return this._basePropDesc.DisplayName;
				}
			}

			// Token: 0x17000981 RID: 2433
			// (get) Token: 0x0600311D RID: 12573 RVA: 0x0010CB74 File Offset: 0x0010AD74
			public override bool IsReadOnly
			{
				get
				{
					return this._basePropDesc.IsReadOnly;
				}
			}

			// Token: 0x17000982 RID: 2434
			// (get) Token: 0x0600311E RID: 12574 RVA: 0x0010CB81 File Offset: 0x0010AD81
			public override Type PropertyType
			{
				get
				{
					return this._basePropDesc.PropertyType;
				}
			}

			// Token: 0x0600311F RID: 12575 RVA: 0x0010CB8E File Offset: 0x0010AD8E
			public override bool CanResetValue(object component)
			{
				return this._basePropDesc.ShouldSerializeValue(component);
			}

			// Token: 0x06003120 RID: 12576 RVA: 0x0010CB9C File Offset: 0x0010AD9C
			public override object GetValue(object component)
			{
				return this._basePropDesc.GetValue(component);
			}

			// Token: 0x06003121 RID: 12577 RVA: 0x0010CBAA File Offset: 0x0010ADAA
			public override void ResetValue(object component)
			{
				this._basePropDesc.ResetValue(component);
			}

			// Token: 0x06003122 RID: 12578 RVA: 0x0010CBB8 File Offset: 0x0010ADB8
			public override void SetValue(object component, object value)
			{
				this._basePropDesc.SetValue(component, value);
			}

			// Token: 0x06003123 RID: 12579 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool ShouldSerializeValue(object component)
			{
				return true;
			}

			// Token: 0x04002121 RID: 8481
			private PropertyDescriptor _basePropDesc;
		}
	}
}
