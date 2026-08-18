using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x020003B0 RID: 944
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DesignerSerializer("System.Windows.Forms.Design.ToolStripCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("System.Windows.Forms.Design.ToolStripDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Items")]
	[SRDescription("DescriptionToolStrip")]
	[DefaultEvent("ItemClicked")]
	public class ToolStrip : ScrollableControl, IArrangedElement, IComponent, IDisposable, ISupportToolStripPanel
	{
		// Token: 0x06003DA0 RID: 15776 RVA: 0x0010B88C File Offset: 0x00109A8C
		public ToolStrip()
		{
			if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
			{
				ToolStripManager.CurrentDpi = base.DeviceDpi;
				this.defaultFont = ToolStripManager.DefaultFont;
				ToolStrip.iconWidth = DpiHelper.LogicalToDeviceUnits(16, base.DeviceDpi);
				ToolStrip.iconHeight = DpiHelper.LogicalToDeviceUnits(16, base.DeviceDpi);
				ToolStrip.insertionBeamWidth = DpiHelper.LogicalToDeviceUnits(6, base.DeviceDpi);
				this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStrip.defaultPadding, base.DeviceDpi);
				this.scaledDefaultGripMargin = DpiHelper.LogicalToDeviceUnits(ToolStrip.defaultGripMargin, base.DeviceDpi);
			}
			else if (DpiHelper.IsScalingRequired)
			{
				ToolStrip.iconWidth = DpiHelper.LogicalToDeviceUnitsX(16);
				ToolStrip.iconHeight = DpiHelper.LogicalToDeviceUnitsY(16);
				if (DpiHelper.EnableToolStripHighDpiImprovements)
				{
					ToolStrip.insertionBeamWidth = DpiHelper.LogicalToDeviceUnitsX(6);
					this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStrip.defaultPadding, 0);
					this.scaledDefaultGripMargin = DpiHelper.LogicalToDeviceUnits(ToolStrip.defaultGripMargin, 0);
				}
			}
			this.imageScalingSize = new Size(ToolStrip.iconWidth, ToolStrip.iconHeight);
			base.SuspendLayout();
			this.CanOverflow = true;
			this.TabStop = false;
			this.MenuAutoExpand = false;
			base.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.Selectable, false);
			this.SetToolStripState(192, true);
			base.SetState2(2064, true);
			ToolStripManager.ToolStrips.Add(this);
			this.layoutEngine = new ToolStripSplitStackLayout(this);
			this.Dock = this.DefaultDock;
			this.AutoSize = true;
			this.CausesValidation = false;
			Size defaultSize = this.DefaultSize;
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
			this.ShowItemToolTips = this.DefaultShowItemToolTips;
			base.ResumeLayout(true);
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x0010BA91 File Offset: 0x00109C91
		public ToolStrip(params ToolStripItem[] items) : this()
		{
			this.Items.AddRange(items);
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x0010BAA5 File Offset: 0x00109CA5
		internal ArrayList ActiveDropDowns
		{
			get
			{
				return this.activeDropDowns;
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x0010BAAD File Offset: 0x00109CAD
		// (set) Token: 0x06003DA4 RID: 15780 RVA: 0x0010BABA File Offset: 0x00109CBA
		internal virtual bool KeyboardActive
		{
			get
			{
				return this.GetToolStripState(32768);
			}
			set
			{
				this.SetToolStripState(32768, value);
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x00013062 File Offset: 0x00011262
		// (set) Token: 0x06003DA6 RID: 15782 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual bool AllItemsVisible
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x06003DA8 RID: 15784 RVA: 0x0010BAC8 File Offset: 0x00109CC8
		[DefaultValue(true)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				if (this.IsInToolStripPanel && base.AutoSize && !value)
				{
					Rectangle specifiedBounds = CommonProperties.GetSpecifiedBounds(this);
					specifiedBounds.Location = base.Location;
					CommonProperties.UpdateSpecifiedBounds(this, specifiedBounds.X, specifiedBounds.Y, specifiedBounds.Width, specifiedBounds.Height, BoundsSpecified.Location);
				}
				base.AutoSize = value;
			}
		}

		// Token: 0x140002F5 RID: 757
		// (add) Token: 0x06003DA9 RID: 15785 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x06003DAA RID: 15786 RVA: 0x00011A5F File Offset: 0x0000FC5F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06003DAB RID: 15787 RVA: 0x000B0CB7 File Offset: 0x000AEEB7
		// (set) Token: 0x06003DAC RID: 15788 RVA: 0x0010BB26 File Offset: 0x00109D26
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("ToolStripDoesntSupportAutoScroll"));
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06003DAD RID: 15789 RVA: 0x00011A23 File Offset: 0x0000FC23
		// (set) Token: 0x06003DAE RID: 15790 RVA: 0x00011A2B File Offset: 0x0000FC2B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x00011A34 File Offset: 0x0000FC34
		// (set) Token: 0x06003DB0 RID: 15792 RVA: 0x00011A3C File Offset: 0x0000FC3C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x000FC60B File Offset: 0x000FA80B
		// (set) Token: 0x06003DB2 RID: 15794 RVA: 0x000FC613 File Offset: 0x000FA813
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Point AutoScrollPosition
		{
			get
			{
				return base.AutoScrollPosition;
			}
			set
			{
				base.AutoScrollPosition = value;
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x000B90B9 File Offset: 0x000B72B9
		// (set) Token: 0x06003DB4 RID: 15796 RVA: 0x0010BB37 File Offset: 0x00109D37
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				if (value && this.AllowItemReorder)
				{
					throw new ArgumentException(SR.GetString("ToolStripAllowItemReorderAndAllowDropCannotBeSetToTrue"));
				}
				base.AllowDrop = value;
				if (value)
				{
					this.DropTargetManager.EnsureRegistered(this);
					return;
				}
				this.DropTargetManager.EnsureUnRegistered(this);
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06003DB5 RID: 15797 RVA: 0x0010BB77 File Offset: 0x00109D77
		// (set) Token: 0x06003DB6 RID: 15798 RVA: 0x0010BB80 File Offset: 0x00109D80
		[DefaultValue(false)]
		[SRDescription("ToolStripAllowItemReorderDescr")]
		[SRCategory("CatBehavior")]
		public bool AllowItemReorder
		{
			get
			{
				return this.GetToolStripState(2);
			}
			set
			{
				if (this.GetToolStripState(2) != value)
				{
					if (this.AllowDrop && value)
					{
						throw new ArgumentException(SR.GetString("ToolStripAllowItemReorderAndAllowDropCannotBeSetToTrue"));
					}
					this.SetToolStripState(2, value);
					if (value)
					{
						ToolStripSplitStackDragDropHandler toolStripSplitStackDragDropHandler = new ToolStripSplitStackDragDropHandler(this);
						this.ItemReorderDropSource = toolStripSplitStackDragDropHandler;
						this.ItemReorderDropTarget = toolStripSplitStackDragDropHandler;
						this.DropTargetManager.EnsureRegistered(this);
						return;
					}
					this.DropTargetManager.EnsureUnRegistered(this);
				}
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06003DB7 RID: 15799 RVA: 0x0010BBEA File Offset: 0x00109DEA
		// (set) Token: 0x06003DB8 RID: 15800 RVA: 0x0010BBF7 File Offset: 0x00109DF7
		[DefaultValue(true)]
		[SRDescription("ToolStripAllowMergeDescr")]
		[SRCategory("CatBehavior")]
		public bool AllowMerge
		{
			get
			{
				return this.GetToolStripState(128);
			}
			set
			{
				if (this.GetToolStripState(128) != value)
				{
					this.SetToolStripState(128, value);
				}
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x000FFF04 File Offset: 0x000FE104
		// (set) Token: 0x06003DBA RID: 15802 RVA: 0x0010BC14 File Offset: 0x00109E14
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				using (new LayoutTransaction(this, this, PropertyNames.Anchor))
				{
					base.Anchor = value;
				}
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x0001A1E5 File Offset: 0x000183E5
		// (set) Token: 0x06003DBC RID: 15804 RVA: 0x00012F98 File Offset: 0x00011198
		[SRDescription("ToolStripBackColorDescr")]
		[SRCategory("CatAppearance")]
		public new Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x140002F6 RID: 758
		// (add) Token: 0x06003DBD RID: 15805 RVA: 0x0010BC54 File Offset: 0x00109E54
		// (remove) Token: 0x06003DBE RID: 15806 RVA: 0x0010BC67 File Offset: 0x00109E67
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripOnBeginDrag")]
		public event EventHandler BeginDrag
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventBeginDrag, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventBeginDrag, value);
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06003DBF RID: 15807 RVA: 0x0010BC7C File Offset: 0x00109E7C
		// (set) Token: 0x06003DC0 RID: 15808 RVA: 0x0010BCBE File Offset: 0x00109EBE
		public override BindingContext BindingContext
		{
			get
			{
				BindingContext bindingContext = (BindingContext)base.Properties.GetObject(ToolStrip.PropBindingContext);
				if (bindingContext != null)
				{
					return bindingContext;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null && parentInternal.CanAccessProperties)
				{
					return parentInternal.BindingContext;
				}
				return null;
			}
			set
			{
				if (base.Properties.GetObject(ToolStrip.PropBindingContext) != value)
				{
					base.Properties.SetObject(ToolStrip.PropBindingContext, value);
					this.OnBindingContextChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x0010BCEF File Offset: 0x00109EEF
		// (set) Token: 0x06003DC2 RID: 15810 RVA: 0x0010BCF8 File Offset: 0x00109EF8
		[DefaultValue(true)]
		[SRDescription("ToolStripCanOverflowDescr")]
		[SRCategory("CatLayout")]
		public bool CanOverflow
		{
			get
			{
				return this.GetToolStripState(1);
			}
			set
			{
				if (this.GetToolStripState(1) != value)
				{
					this.SetToolStripState(1, value);
					this.InvalidateLayout();
				}
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x0010BD12 File Offset: 0x00109F12
		internal bool CanHotTrack
		{
			get
			{
				return this.Focused || !base.ContainsFocus;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x000E2B53 File Offset: 0x000E0D53
		// (set) Token: 0x06003DC5 RID: 15813 RVA: 0x000E2B5B File Offset: 0x000E0D5B
		[Browsable(false)]
		[DefaultValue(false)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		// Token: 0x140002F7 RID: 759
		// (add) Token: 0x06003DC6 RID: 15814 RVA: 0x000E2B64 File Offset: 0x000E0D64
		// (remove) Token: 0x06003DC7 RID: 15815 RVA: 0x000E2B6D File Offset: 0x000E0D6D
		[Browsable(false)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000EC606 File Offset: 0x000EA806
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x140002F8 RID: 760
		// (add) Token: 0x06003DC9 RID: 15817 RVA: 0x000FC6D2 File Offset: 0x000FA8D2
		// (remove) Token: 0x06003DCA RID: 15818 RVA: 0x000FC6DB File Offset: 0x000FA8DB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlAdded
		{
			add
			{
				base.ControlAdded += value;
			}
			remove
			{
				base.ControlAdded -= value;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x0001A23C File Offset: 0x0001843C
		// (set) Token: 0x06003DCC RID: 15820 RVA: 0x0001A244 File Offset: 0x00018444
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		// Token: 0x140002F9 RID: 761
		// (add) Token: 0x06003DCD RID: 15821 RVA: 0x000463EF File Offset: 0x000445EF
		// (remove) Token: 0x06003DCE RID: 15822 RVA: 0x000463F8 File Offset: 0x000445F8
		[Browsable(false)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		// Token: 0x140002FA RID: 762
		// (add) Token: 0x06003DCF RID: 15823 RVA: 0x000FC6E4 File Offset: 0x000FA8E4
		// (remove) Token: 0x06003DD0 RID: 15824 RVA: 0x000FC6ED File Offset: 0x000FA8ED
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlRemoved
		{
			add
			{
				base.ControlRemoved += value;
			}
			remove
			{
				base.ControlRemoved -= value;
			}
		}

		// Token: 0x140002FB RID: 763
		// (add) Token: 0x06003DD1 RID: 15825 RVA: 0x0010BD27 File Offset: 0x00109F27
		// (remove) Token: 0x06003DD2 RID: 15826 RVA: 0x0010BD3A File Offset: 0x00109F3A
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripOnEndDrag")]
		public event EventHandler EndDrag
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventEndDrag, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventEndDrag, value);
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x0010BD4D File Offset: 0x00109F4D
		// (set) Token: 0x06003DD4 RID: 15828 RVA: 0x0001A27A File Offset: 0x0001847A
		public override Font Font
		{
			get
			{
				if (base.IsFontSet())
				{
					return base.Font;
				}
				if (this.defaultFont == null)
				{
					this.defaultFont = ToolStripManager.DefaultFont;
				}
				return this.defaultFont;
			}
			set
			{
				base.Font = value;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x0010BD77 File Offset: 0x00109F77
		protected override Size DefaultSize
		{
			get
			{
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return new Size(100, 25);
				}
				return DpiHelper.LogicalToDeviceUnits(new Size(100, 25), base.DeviceDpi);
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06003DD6 RID: 15830 RVA: 0x0010BD9E File Offset: 0x00109F9E
		protected override Padding DefaultPadding
		{
			get
			{
				return this.scaledDefaultPadding;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003DD8 RID: 15832 RVA: 0x00013062 File Offset: 0x00011262
		protected virtual DockStyle DefaultDock
		{
			get
			{
				return DockStyle.Top;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x0010BDA6 File Offset: 0x00109FA6
		protected virtual Padding DefaultGripMargin
		{
			get
			{
				if (this.toolStripGrip != null)
				{
					return this.toolStripGrip.DefaultMargin;
				}
				return this.scaledDefaultGripMargin;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06003DDA RID: 15834 RVA: 0x00013062 File Offset: 0x00011262
		protected virtual bool DefaultShowItemToolTips
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x0010BDC4 File Offset: 0x00109FC4
		// (set) Token: 0x06003DDC RID: 15836 RVA: 0x0010BE91 File Offset: 0x0010A091
		[Browsable(false)]
		[SRDescription("ToolStripDefaultDropDownDirectionDescr")]
		[SRCategory("CatBehavior")]
		public virtual ToolStripDropDownDirection DefaultDropDownDirection
		{
			get
			{
				ToolStripDropDownDirection toolStripDropDownDirection = this.toolStripDropDownDirection;
				if (toolStripDropDownDirection == ToolStripDropDownDirection.Default)
				{
					if (this.Orientation == Orientation.Vertical)
					{
						if (this.IsInToolStripPanel)
						{
							DockStyle dockStyle = (this.ParentInternal != null) ? this.ParentInternal.Dock : DockStyle.Left;
							toolStripDropDownDirection = ((dockStyle == DockStyle.Right) ? ToolStripDropDownDirection.Left : ToolStripDropDownDirection.Right);
							if (base.DesignMode && dockStyle == DockStyle.Left)
							{
								toolStripDropDownDirection = ToolStripDropDownDirection.Right;
							}
						}
						else
						{
							toolStripDropDownDirection = ((this.Dock == DockStyle.Right && this.RightToLeft == RightToLeft.No) ? ToolStripDropDownDirection.Left : ToolStripDropDownDirection.Right);
							if (base.DesignMode && this.Dock == DockStyle.Left)
							{
								toolStripDropDownDirection = ToolStripDropDownDirection.Right;
							}
						}
					}
					else
					{
						DockStyle dock = this.Dock;
						if (this.IsInToolStripPanel && this.ParentInternal != null)
						{
							dock = this.ParentInternal.Dock;
						}
						if (dock == DockStyle.Bottom)
						{
							toolStripDropDownDirection = ((this.RightToLeft == RightToLeft.Yes) ? ToolStripDropDownDirection.AboveLeft : ToolStripDropDownDirection.AboveRight);
						}
						else
						{
							toolStripDropDownDirection = ((this.RightToLeft == RightToLeft.Yes) ? ToolStripDropDownDirection.BelowLeft : ToolStripDropDownDirection.BelowRight);
						}
					}
				}
				return toolStripDropDownDirection;
			}
			set
			{
				if (value > ToolStripDropDownDirection.Right && value != ToolStripDropDownDirection.Default)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripDropDownDirection));
				}
				this.toolStripDropDownDirection = value;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x0010BEB8 File Offset: 0x0010A0B8
		[DefaultValue(DockStyle.Top)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (value != this.Dock)
				{
					using (new LayoutTransaction(this, this, PropertyNames.Dock))
					{
						using (new LayoutTransaction(this.ParentInternal, this, PropertyNames.Dock))
						{
							DefaultLayout.SetDock(this, value);
							this.UpdateLayoutStyle(this.Dock);
						}
					}
					this.OnDockChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x0010BF40 File Offset: 0x0010A140
		internal virtual NativeWindow DropDownOwnerWindow
		{
			get
			{
				if (this.dropDownOwnerWindow == null)
				{
					this.dropDownOwnerWindow = new NativeWindow();
				}
				if (this.dropDownOwnerWindow.Handle == IntPtr.Zero)
				{
					CreateParams createParams = new CreateParams();
					createParams.ExStyle = 128;
					this.dropDownOwnerWindow.CreateHandle(createParams);
				}
				return this.dropDownOwnerWindow;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06003DE0 RID: 15840 RVA: 0x0010BF9A File Offset: 0x0010A19A
		// (set) Token: 0x06003DE1 RID: 15841 RVA: 0x0010BFB6 File Offset: 0x0010A1B6
		internal ToolStripDropTargetManager DropTargetManager
		{
			get
			{
				if (this.dropTargetManager == null)
				{
					this.dropTargetManager = new ToolStripDropTargetManager(this);
				}
				return this.dropTargetManager;
			}
			set
			{
				this.dropTargetManager = value;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x0010BFBF File Offset: 0x0010A1BF
		protected internal virtual ToolStripItemCollection DisplayedItems
		{
			get
			{
				if (this.displayedItems == null)
				{
					this.displayedItems = new ToolStripItemCollection(this, false);
				}
				return this.displayedItems;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x0010BFDC File Offset: 0x0010A1DC
		public override Rectangle DisplayRectangle
		{
			get
			{
				Rectangle displayRectangle = base.DisplayRectangle;
				if (this.LayoutEngine is ToolStripSplitStackLayout && this.GripStyle == ToolStripGripStyle.Visible)
				{
					if (this.Orientation == Orientation.Horizontal)
					{
						int num = this.Grip.GripThickness + this.Grip.Margin.Horizontal;
						displayRectangle.Width -= num;
						displayRectangle.X += ((this.RightToLeft == RightToLeft.No) ? num : 0);
					}
					else
					{
						int num2 = this.Grip.GripThickness + this.Grip.Margin.Vertical;
						displayRectangle.Y += num2;
						displayRectangle.Height -= num2;
					}
				}
				return displayRectangle;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x06003DE5 RID: 15845 RVA: 0x00013238 File Offset: 0x00011438
		[Browsable(false)]
		public new Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x140002FC RID: 764
		// (add) Token: 0x06003DE6 RID: 15846 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x06003DE7 RID: 15847 RVA: 0x0005AAD7 File Offset: 0x00058CD7
		[Browsable(false)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x0010C09E File Offset: 0x0010A29E
		private bool HasKeyboardInput
		{
			get
			{
				return base.ContainsFocus || (ToolStripManager.ModalMenuFilter.InMenuMode && ToolStripManager.ModalMenuFilter.GetActiveToolStrip() == this);
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x0010C0BC File Offset: 0x0010A2BC
		internal ToolStripGrip Grip
		{
			get
			{
				if (this.toolStripGrip == null)
				{
					this.toolStripGrip = new ToolStripGrip();
					this.toolStripGrip.Overflow = ToolStripItemOverflow.Never;
					this.toolStripGrip.Visible = (this.toolStripGripStyle == ToolStripGripStyle.Visible);
					this.toolStripGrip.AutoSize = false;
					this.toolStripGrip.ParentInternal = this;
					this.toolStripGrip.Margin = this.DefaultGripMargin;
				}
				return this.toolStripGrip;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x0010C12B File Offset: 0x0010A32B
		// (set) Token: 0x06003DEB RID: 15851 RVA: 0x0010C134 File Offset: 0x0010A334
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripGripStyleDescr")]
		[DefaultValue(ToolStripGripStyle.Visible)]
		public ToolStripGripStyle GripStyle
		{
			get
			{
				return this.toolStripGripStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripGripStyle));
				}
				if (this.toolStripGripStyle != value)
				{
					this.toolStripGripStyle = value;
					this.Grip.Visible = (this.toolStripGripStyle == ToolStripGripStyle.Visible);
					LayoutTransaction.DoLayout(this, this, PropertyNames.GripStyle);
				}
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x0010C197 File Offset: 0x0010A397
		[Browsable(false)]
		public ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				if (this.LayoutStyle != ToolStripLayoutStyle.HorizontalStackWithOverflow)
				{
					return ToolStripGripDisplayStyle.Horizontal;
				}
				return ToolStripGripDisplayStyle.Vertical;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x0010C1A5 File Offset: 0x0010A3A5
		// (set) Token: 0x06003DEE RID: 15854 RVA: 0x0010C1B2 File Offset: 0x0010A3B2
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripGripDisplayStyleDescr")]
		public Padding GripMargin
		{
			get
			{
				return this.Grip.Margin;
			}
			set
			{
				this.Grip.Margin = value;
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06003DEF RID: 15855 RVA: 0x0010C1C0 File Offset: 0x0010A3C0
		[Browsable(false)]
		public Rectangle GripRectangle
		{
			get
			{
				if (this.GripStyle != ToolStripGripStyle.Visible)
				{
					return Rectangle.Empty;
				}
				return this.Grip.Bounds;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x0010C1DC File Offset: 0x0010A3DC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool HasChildren
		{
			get
			{
				return base.HasChildren;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x0010C1E4 File Offset: 0x0010A3E4
		// (set) Token: 0x06003DF2 RID: 15858 RVA: 0x0010C274 File Offset: 0x0010A474
		internal bool HasVisibleItems
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					foreach (object obj in this.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj;
						if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
						{
							this.SetToolStripState(4096, true);
							return true;
						}
					}
					this.SetToolStripState(4096, false);
					return false;
				}
				return this.GetToolStripState(4096);
			}
			set
			{
				this.SetToolStripState(4096, value);
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x0010C282 File Offset: 0x0010A482
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new HScrollProperties HorizontalScroll
		{
			get
			{
				return base.HorizontalScroll;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x0010C28A File Offset: 0x0010A48A
		// (set) Token: 0x06003DF5 RID: 15861 RVA: 0x0010C292 File Offset: 0x0010A492
		[DefaultValue(typeof(Size), "16,16")]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripImageScalingSizeDescr")]
		public Size ImageScalingSize
		{
			get
			{
				return this.ImageScalingSizeInternal;
			}
			set
			{
				this.ImageScalingSizeInternal = value;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x0010C29B File Offset: 0x0010A49B
		// (set) Token: 0x06003DF7 RID: 15863 RVA: 0x0010C2A4 File Offset: 0x0010A4A4
		internal virtual Size ImageScalingSizeInternal
		{
			get
			{
				return this.imageScalingSize;
			}
			set
			{
				if (this.imageScalingSize != value)
				{
					this.imageScalingSize = value;
					LayoutTransaction.DoLayoutIf(this.Items.Count > 0, this, this, PropertyNames.ImageScalingSize);
					foreach (object obj in this.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj;
						toolStripItem.OnImageScalingSizeChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x0010C330 File Offset: 0x0010A530
		// (set) Token: 0x06003DF9 RID: 15865 RVA: 0x0010C338 File Offset: 0x0010A538
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripImageListDescr")]
		[Browsable(false)]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				if (this.imageList != value)
				{
					EventHandler value2 = new EventHandler(this.ImageListRecreateHandle);
					if (this.imageList != null)
					{
						this.imageList.RecreateHandle -= value2;
					}
					this.imageList = value;
					if (value != null)
					{
						value.RecreateHandle += value2;
					}
					foreach (object obj in this.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj;
						toolStripItem.InvalidateImageListImage();
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003DFA RID: 15866 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool IsMnemonicsListenerAxSourced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06003DFB RID: 15867 RVA: 0x0010C3D4 File Offset: 0x0010A5D4
		internal bool IsInToolStripPanel
		{
			get
			{
				return this.ToolStripPanelRow != null;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x0010C3DF File Offset: 0x0010A5DF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsCurrentlyDragging
		{
			get
			{
				return this.GetToolStripState(2048);
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x0010C3EC File Offset: 0x0010A5EC
		private bool IsLocationChanging
		{
			get
			{
				return this.GetToolStripState(1024);
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x0010C3F9 File Offset: 0x0010A5F9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatData")]
		[SRDescription("ToolStripItemsDescr")]
		[MergableProperty(false)]
		public virtual ToolStripItemCollection Items
		{
			get
			{
				if (this.toolStripItemCollection == null)
				{
					this.toolStripItemCollection = new ToolStripItemCollection(this, true);
				}
				return this.toolStripItemCollection;
			}
		}

		// Token: 0x140002FD RID: 765
		// (add) Token: 0x06003DFF RID: 15871 RVA: 0x0010C416 File Offset: 0x0010A616
		// (remove) Token: 0x06003E00 RID: 15872 RVA: 0x0010C429 File Offset: 0x0010A629
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemAddedDescr")]
		public event ToolStripItemEventHandler ItemAdded
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventItemAdded, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventItemAdded, value);
			}
		}

		// Token: 0x140002FE RID: 766
		// (add) Token: 0x06003E01 RID: 15873 RVA: 0x0010C43C File Offset: 0x0010A63C
		// (remove) Token: 0x06003E02 RID: 15874 RVA: 0x0010C44F File Offset: 0x0010A64F
		[SRCategory("CatAction")]
		[SRDescription("ToolStripItemOnClickDescr")]
		public event ToolStripItemClickedEventHandler ItemClicked
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventItemClicked, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventItemClicked, value);
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x0010C462 File Offset: 0x0010A662
		private CachedItemHdcInfo ItemHdcInfo
		{
			get
			{
				if (this.cachedItemHdcInfo == null)
				{
					this.cachedItemHdcInfo = new CachedItemHdcInfo();
				}
				return this.cachedItemHdcInfo;
			}
		}

		// Token: 0x140002FF RID: 767
		// (add) Token: 0x06003E04 RID: 15876 RVA: 0x0010C47D File Offset: 0x0010A67D
		// (remove) Token: 0x06003E05 RID: 15877 RVA: 0x0010C490 File Offset: 0x0010A690
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemRemovedDescr")]
		public event ToolStripItemEventHandler ItemRemoved
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventItemRemoved, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventItemRemoved, value);
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x0010C4A3 File Offset: 0x0010A6A3
		[Browsable(false)]
		public bool IsDropDown
		{
			get
			{
				return this is ToolStripDropDown;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003E07 RID: 15879 RVA: 0x0010C4AE File Offset: 0x0010A6AE
		internal bool IsDisposingItems
		{
			get
			{
				return this.GetToolStripState(4);
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x0010C4B7 File Offset: 0x0010A6B7
		// (set) Token: 0x06003E09 RID: 15881 RVA: 0x0010C4BF File Offset: 0x0010A6BF
		internal IDropTarget ItemReorderDropTarget
		{
			get
			{
				return this.itemReorderDropTarget;
			}
			set
			{
				this.itemReorderDropTarget = value;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x0010C4C8 File Offset: 0x0010A6C8
		// (set) Token: 0x06003E0B RID: 15883 RVA: 0x0010C4D0 File Offset: 0x0010A6D0
		internal ISupportOleDropSource ItemReorderDropSource
		{
			get
			{
				return this.itemReorderDropSource;
			}
			set
			{
				this.itemReorderDropSource = value;
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06003E0C RID: 15884 RVA: 0x0010C4D9 File Offset: 0x0010A6D9
		internal bool IsInDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06003E0D RID: 15885 RVA: 0x0010C4E4 File Offset: 0x0010A6E4
		internal bool IsTopInDesignMode
		{
			get
			{
				ToolStrip toplevelOwnerToolStrip = this.GetToplevelOwnerToolStrip();
				return toplevelOwnerToolStrip != null && toplevelOwnerToolStrip.IsInDesignMode;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06003E0E RID: 15886 RVA: 0x0010C503 File Offset: 0x0010A703
		internal bool IsSelectionSuspended
		{
			get
			{
				return this.GetToolStripState(16384);
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06003E0F RID: 15887 RVA: 0x0010C510 File Offset: 0x0010A710
		internal ToolStripItem LastMouseDownedItem
		{
			get
			{
				if (this.lastMouseDownedItem != null && (this.lastMouseDownedItem.IsDisposed || this.lastMouseDownedItem.ParentInternal != this))
				{
					this.lastMouseDownedItem = null;
				}
				return this.lastMouseDownedItem;
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06003E10 RID: 15888 RVA: 0x0010C542 File Offset: 0x0010A742
		// (set) Token: 0x06003E11 RID: 15889 RVA: 0x0010C54A File Offset: 0x0010A74A
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LayoutSettings LayoutSettings
		{
			get
			{
				return this.layoutSettings;
			}
			set
			{
				this.layoutSettings = value;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x0010C554 File Offset: 0x0010A754
		// (set) Token: 0x06003E13 RID: 15891 RVA: 0x0010C584 File Offset: 0x0010A784
		[SRDescription("ToolStripLayoutStyle")]
		[SRCategory("CatLayout")]
		[AmbientValue(ToolStripLayoutStyle.StackWithOverflow)]
		public ToolStripLayoutStyle LayoutStyle
		{
			get
			{
				if (this.layoutStyle == ToolStripLayoutStyle.StackWithOverflow)
				{
					Orientation orientation = this.Orientation;
					if (orientation == Orientation.Horizontal)
					{
						return ToolStripLayoutStyle.HorizontalStackWithOverflow;
					}
					if (orientation == Orientation.Vertical)
					{
						return ToolStripLayoutStyle.VerticalStackWithOverflow;
					}
				}
				return this.layoutStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripLayoutStyle));
				}
				if (this.layoutStyle != value)
				{
					this.layoutStyle = value;
					switch (value)
					{
					case ToolStripLayoutStyle.Flow:
						if (!(this.layoutEngine is FlowLayout))
						{
							this.layoutEngine = FlowLayout.Instance;
						}
						this.UpdateOrientation(Orientation.Horizontal);
						goto IL_EA;
					case ToolStripLayoutStyle.Table:
						if (!(this.layoutEngine is TableLayout))
						{
							this.layoutEngine = TableLayout.Instance;
						}
						this.UpdateOrientation(Orientation.Horizontal);
						goto IL_EA;
					}
					if (value != ToolStripLayoutStyle.StackWithOverflow)
					{
						this.UpdateOrientation((value == ToolStripLayoutStyle.VerticalStackWithOverflow) ? Orientation.Vertical : Orientation.Horizontal);
					}
					else if (this.IsInToolStripPanel)
					{
						this.UpdateLayoutStyle(this.ToolStripPanelRow.Orientation);
					}
					else
					{
						this.UpdateLayoutStyle(this.Dock);
					}
					if (!(this.layoutEngine is ToolStripSplitStackLayout))
					{
						this.layoutEngine = new ToolStripSplitStackLayout(this);
					}
					IL_EA:
					using (LayoutTransaction.CreateTransactionIf(base.IsHandleCreated, this, this, PropertyNames.LayoutStyle))
					{
						this.LayoutSettings = this.CreateLayoutSettings(this.layoutStyle);
					}
					this.OnLayoutStyleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000300 RID: 768
		// (add) Token: 0x06003E14 RID: 15892 RVA: 0x0010C6C8 File Offset: 0x0010A8C8
		// (remove) Token: 0x06003E15 RID: 15893 RVA: 0x0010C6DB File Offset: 0x0010A8DB
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLayoutCompleteDescr")]
		public event EventHandler LayoutCompleted
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventLayoutCompleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventLayoutCompleted, value);
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003E16 RID: 15894 RVA: 0x0010C6EE File Offset: 0x0010A8EE
		// (set) Token: 0x06003E17 RID: 15895 RVA: 0x0010C6F6 File Offset: 0x0010A8F6
		internal bool LayoutRequired
		{
			get
			{
				return this.layoutRequired;
			}
			set
			{
				this.layoutRequired = value;
			}
		}

		// Token: 0x14000301 RID: 769
		// (add) Token: 0x06003E18 RID: 15896 RVA: 0x0010C6FF File Offset: 0x0010A8FF
		// (remove) Token: 0x06003E19 RID: 15897 RVA: 0x0010C712 File Offset: 0x0010A912
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLayoutStyleChangedDescr")]
		public event EventHandler LayoutStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventLayoutStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventLayoutStyleChanged, value);
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06003E1A RID: 15898 RVA: 0x0010C725 File Offset: 0x0010A925
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return this.layoutEngine;
			}
		}

		// Token: 0x14000302 RID: 770
		// (add) Token: 0x06003E1B RID: 15899 RVA: 0x0010C72D File Offset: 0x0010A92D
		// (remove) Token: 0x06003E1C RID: 15900 RVA: 0x0010C740 File Offset: 0x0010A940
		internal event ToolStripLocationCancelEventHandler LocationChanging
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventLocationChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventLocationChanging, value);
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x0010C754 File Offset: 0x0010A954
		protected internal virtual Size MaxItemSize
		{
			get
			{
				return this.DisplayRectangle.Size;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06003E1E RID: 15902 RVA: 0x0010C76F File Offset: 0x0010A96F
		// (set) Token: 0x06003E1F RID: 15903 RVA: 0x0010C79E File Offset: 0x0010A99E
		internal bool MenuAutoExpand
		{
			get
			{
				if (base.DesignMode || !this.GetToolStripState(8))
				{
					return false;
				}
				if (!this.IsDropDown && !ToolStripManager.ModalMenuFilter.InMenuMode)
				{
					this.SetToolStripState(8, false);
					return false;
				}
				return true;
			}
			set
			{
				if (!base.DesignMode)
				{
					this.SetToolStripState(8, value);
				}
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x0010C7B0 File Offset: 0x0010A9B0
		internal Stack<MergeHistory> MergeHistoryStack
		{
			get
			{
				if (this.mergeHistoryStack == null)
				{
					this.mergeHistoryStack = new Stack<MergeHistory>();
				}
				return this.mergeHistoryStack;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06003E21 RID: 15905 RVA: 0x0010C7CB File Offset: 0x0010A9CB
		private MouseHoverTimer MouseHoverTimer
		{
			get
			{
				if (this.mouseHoverTimer == null)
				{
					this.mouseHoverTimer = new MouseHoverTimer();
				}
				return this.mouseHoverTimer;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06003E22 RID: 15906 RVA: 0x0010C7E8 File Offset: 0x0010A9E8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ToolStripOverflowButton OverflowButton
		{
			get
			{
				if (this.toolStripOverflowButton == null)
				{
					this.toolStripOverflowButton = new ToolStripOverflowButton(this);
					this.toolStripOverflowButton.Overflow = ToolStripItemOverflow.Never;
					this.toolStripOverflowButton.ParentInternal = this;
					this.toolStripOverflowButton.Alignment = ToolStripItemAlignment.Right;
					this.toolStripOverflowButton.Size = this.toolStripOverflowButton.GetPreferredSize(this.DisplayRectangle.Size - base.Padding.Size);
				}
				return this.toolStripOverflowButton;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06003E23 RID: 15907 RVA: 0x0010C86A File Offset: 0x0010AA6A
		internal ToolStripItemCollection OverflowItems
		{
			get
			{
				if (this.overflowItems == null)
				{
					this.overflowItems = new ToolStripItemCollection(this, false);
				}
				return this.overflowItems;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x0010C887 File Offset: 0x0010AA87
		[Browsable(false)]
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
		}

		// Token: 0x14000303 RID: 771
		// (add) Token: 0x06003E25 RID: 15909 RVA: 0x0010C88F File Offset: 0x0010AA8F
		// (remove) Token: 0x06003E26 RID: 15910 RVA: 0x0010C8A2 File Offset: 0x0010AAA2
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripPaintGripDescr")]
		public event PaintEventHandler PaintGrip
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventPaintGrip, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventPaintGrip, value);
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x0010C8B5 File Offset: 0x0010AAB5
		internal ToolStrip.RestoreFocusMessageFilter RestoreFocusFilter
		{
			get
			{
				if (this.restoreFocusFilter == null)
				{
					this.restoreFocusFilter = new ToolStrip.RestoreFocusMessageFilter(this);
				}
				return this.restoreFocusFilter;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x0010C8D1 File Offset: 0x0010AAD1
		internal ToolStripPanelCell ToolStripPanelCell
		{
			get
			{
				return ((ISupportToolStripPanel)this).ToolStripPanelCell;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x0010C8D9 File Offset: 0x0010AAD9
		internal ToolStripPanelRow ToolStripPanelRow
		{
			get
			{
				return ((ISupportToolStripPanel)this).ToolStripPanelRow;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06003E2A RID: 15914 RVA: 0x0010C8E4 File Offset: 0x0010AAE4
		ToolStripPanelCell ISupportToolStripPanel.ToolStripPanelCell
		{
			get
			{
				ToolStripPanelCell toolStripPanelCell = null;
				if (!this.IsDropDown && !base.IsDisposed)
				{
					if (base.Properties.ContainsObject(ToolStrip.PropToolStripPanelCell))
					{
						toolStripPanelCell = (ToolStripPanelCell)base.Properties.GetObject(ToolStrip.PropToolStripPanelCell);
					}
					else
					{
						toolStripPanelCell = new ToolStripPanelCell(this);
						base.Properties.SetObject(ToolStrip.PropToolStripPanelCell, toolStripPanelCell);
					}
				}
				return toolStripPanelCell;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06003E2B RID: 15915 RVA: 0x0010C948 File Offset: 0x0010AB48
		// (set) Token: 0x06003E2C RID: 15916 RVA: 0x0010C96C File Offset: 0x0010AB6C
		ToolStripPanelRow ISupportToolStripPanel.ToolStripPanelRow
		{
			get
			{
				if (this.ToolStripPanelCell == null)
				{
					return null;
				}
				return this.ToolStripPanelCell.ToolStripPanelRow;
			}
			set
			{
				ToolStripPanelRow toolStripPanelRow = this.ToolStripPanelRow;
				if (toolStripPanelRow != value)
				{
					ToolStripPanelCell toolStripPanelCell = this.ToolStripPanelCell;
					if (toolStripPanelCell == null)
					{
						return;
					}
					toolStripPanelCell.ToolStripPanelRow = value;
					if (value != null)
					{
						if (toolStripPanelRow == null || toolStripPanelRow.Orientation != value.Orientation)
						{
							if (this.layoutStyle == ToolStripLayoutStyle.StackWithOverflow)
							{
								this.UpdateLayoutStyle(value.Orientation);
								return;
							}
							this.UpdateOrientation(value.Orientation);
							return;
						}
					}
					else
					{
						if (toolStripPanelRow != null && toolStripPanelRow.ControlsInternal.Contains(this))
						{
							toolStripPanelRow.ControlsInternal.Remove(this);
						}
						this.UpdateLayoutStyle(this.Dock);
					}
				}
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06003E2D RID: 15917 RVA: 0x0010C9F5 File Offset: 0x0010ABF5
		// (set) Token: 0x06003E2E RID: 15918 RVA: 0x0010CA02 File Offset: 0x0010AC02
		[DefaultValue(false)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripStretchDescr")]
		public bool Stretch
		{
			get
			{
				return this.GetToolStripState(512);
			}
			set
			{
				if (this.Stretch != value)
				{
					this.SetToolStripState(512, value);
				}
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003E2F RID: 15919 RVA: 0x0010CA19 File Offset: 0x0010AC19
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode && !this.IsTopInDesignMode;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x0010CA38 File Offset: 0x0010AC38
		// (set) Token: 0x06003E31 RID: 15921 RVA: 0x0010CAB0 File Offset: 0x0010ACB0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripRenderer Renderer
		{
			get
			{
				if (this.IsDropDown)
				{
					ToolStripDropDown toolStripDropDown = this as ToolStripDropDown;
					if ((toolStripDropDown is ToolStripOverflow || toolStripDropDown.IsAutoGenerated) && toolStripDropDown.OwnerToolStrip != null)
					{
						return toolStripDropDown.OwnerToolStrip.Renderer;
					}
				}
				if (this.RenderMode == ToolStripRenderMode.ManagerRenderMode)
				{
					return ToolStripManager.Renderer;
				}
				this.SetToolStripState(64, false);
				if (this.renderer == null)
				{
					this.Renderer = ToolStripManager.CreateRenderer(this.RenderMode);
				}
				return this.renderer;
			}
			set
			{
				if (this.renderer != value)
				{
					this.SetToolStripState(64, value == null);
					this.renderer = value;
					this.currentRendererType = ((this.renderer != null) ? this.renderer.GetType() : typeof(Type));
					this.OnRendererChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000304 RID: 772
		// (add) Token: 0x06003E32 RID: 15922 RVA: 0x0010CB09 File Offset: 0x0010AD09
		// (remove) Token: 0x06003E33 RID: 15923 RVA: 0x0010CB1C File Offset: 0x0010AD1C
		public event EventHandler RendererChanged
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EventRendererChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EventRendererChanged, value);
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x0010CB30 File Offset: 0x0010AD30
		// (set) Token: 0x06003E35 RID: 15925 RVA: 0x0010CB8C File Offset: 0x0010AD8C
		[SRDescription("ToolStripRenderModeDescr")]
		[SRCategory("CatAppearance")]
		public ToolStripRenderMode RenderMode
		{
			get
			{
				if (this.GetToolStripState(64))
				{
					return ToolStripRenderMode.ManagerRenderMode;
				}
				if (this.renderer != null && !this.renderer.IsAutoGenerated)
				{
					return ToolStripRenderMode.Custom;
				}
				if (this.currentRendererType == ToolStripManager.ProfessionalRendererType)
				{
					return ToolStripRenderMode.Professional;
				}
				if (this.currentRendererType == ToolStripManager.SystemRendererType)
				{
					return ToolStripRenderMode.System;
				}
				return ToolStripRenderMode.Custom;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripRenderMode));
				}
				if (value == ToolStripRenderMode.Custom)
				{
					throw new NotSupportedException(SR.GetString("ToolStripRenderModeUseRendererPropertyInstead"));
				}
				if (value == ToolStripRenderMode.ManagerRenderMode)
				{
					if (!this.GetToolStripState(64))
					{
						this.SetToolStripState(64, true);
						this.OnRendererChanged(EventArgs.Empty);
						return;
					}
				}
				else
				{
					this.SetToolStripState(64, false);
					this.Renderer = ToolStripManager.CreateRenderer(value);
				}
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x0010CC0A File Offset: 0x0010AE0A
		internal bool ShowKeyboardCuesInternal
		{
			get
			{
				return this.ShowKeyboardCues;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x0010CC12 File Offset: 0x0010AE12
		// (set) Token: 0x06003E38 RID: 15928 RVA: 0x0010CC1C File Offset: 0x0010AE1C
		[DefaultValue(true)]
		[SRDescription("ToolStripShowItemToolTipsDescr")]
		[SRCategory("CatBehavior")]
		public bool ShowItemToolTips
		{
			get
			{
				return this.showItemToolTips;
			}
			set
			{
				if (this.showItemToolTips != value)
				{
					this.showItemToolTips = value;
					if (!this.showItemToolTips)
					{
						this.UpdateToolTip(null);
					}
					if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
					{
						ToolTip toolTip = this.ToolTip;
						foreach (object obj in this.Items)
						{
							ToolStripItem tool = (ToolStripItem)obj;
							if (this.showItemToolTips)
							{
								KeyboardToolTipStateMachine.Instance.Hook(tool, toolTip);
							}
							else
							{
								KeyboardToolTipStateMachine.Instance.Unhook(tool, toolTip);
							}
						}
					}
					if (this.toolStripOverflowButton != null && this.OverflowButton.HasDropDownItems)
					{
						this.OverflowButton.DropDown.ShowItemToolTips = value;
					}
				}
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06003E39 RID: 15929 RVA: 0x0010CCE8 File Offset: 0x0010AEE8
		internal Hashtable Shortcuts
		{
			get
			{
				if (this.shortcuts == null)
				{
					this.shortcuts = new Hashtable(1);
				}
				return this.shortcuts;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x06003E3B RID: 15931 RVA: 0x000B2619 File Offset: 0x000B0819
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[DispId(-516)]
		[SRDescription("ControlTabStopDescr")]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x0010CD04 File Offset: 0x0010AF04
		internal ToolTip ToolTip
		{
			get
			{
				ToolTip toolTip;
				if (!base.Properties.ContainsObject(ToolStrip.PropToolTip))
				{
					toolTip = new ToolTip();
					base.Properties.SetObject(ToolStrip.PropToolTip, toolTip);
				}
				else
				{
					toolTip = (ToolTip)base.Properties.GetObject(ToolStrip.PropToolTip);
				}
				return toolTip;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003E3D RID: 15933 RVA: 0x0010CD54 File Offset: 0x0010AF54
		// (set) Token: 0x06003E3E RID: 15934 RVA: 0x0010CD94 File Offset: 0x0010AF94
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		[SRDescription("ToolStripTextDirectionDescr")]
		[SRCategory("CatAppearance")]
		public virtual ToolStripTextDirection TextDirection
		{
			get
			{
				ToolStripTextDirection toolStripTextDirection = ToolStripTextDirection.Inherit;
				if (base.Properties.ContainsObject(ToolStrip.PropTextDirection))
				{
					toolStripTextDirection = (ToolStripTextDirection)base.Properties.GetObject(ToolStrip.PropTextDirection);
				}
				if (toolStripTextDirection == ToolStripTextDirection.Inherit)
				{
					toolStripTextDirection = ToolStripTextDirection.Horizontal;
				}
				return toolStripTextDirection;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripTextDirection));
				}
				base.Properties.SetObject(ToolStrip.PropTextDirection, value);
				using (new LayoutTransaction(this, this, "TextDirection"))
				{
					for (int i = 0; i < this.Items.Count; i++)
					{
						this.Items[i].OnOwnerTextDirectionChanged();
					}
				}
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x0010CE30 File Offset: 0x0010B030
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new VScrollProperties VerticalScroll
		{
			get
			{
				return base.VerticalScroll;
			}
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x0010CE38 File Offset: 0x0010B038
		void ISupportToolStripPanel.BeginDrag()
		{
			this.OnBeginDrag(EventArgs.Empty);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x0010CE48 File Offset: 0x0010B048
		internal virtual void ChangeSelection(ToolStripItem nextItem)
		{
			if (nextItem != null)
			{
				ToolStripControlHost toolStripControlHost = nextItem as ToolStripControlHost;
				if (base.ContainsFocus && !this.Focused)
				{
					this.FocusInternal();
					if (toolStripControlHost == null)
					{
						this.KeyboardActive = true;
					}
				}
				if (toolStripControlHost != null)
				{
					if (this.hwndThatLostFocus == IntPtr.Zero)
					{
						this.SnapFocus(UnsafeNativeMethods.GetFocus());
					}
					toolStripControlHost.Control.Select();
					toolStripControlHost.Control.FocusInternal();
				}
				nextItem.Select();
				ToolStripMenuItem toolStripMenuItem = nextItem as ToolStripMenuItem;
				if (toolStripMenuItem != null && !this.IsDropDown)
				{
					toolStripMenuItem.HandleAutoExpansion();
				}
			}
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x0010CED5 File Offset: 0x0010B0D5
		protected virtual LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle layoutStyle)
		{
			if (layoutStyle == ToolStripLayoutStyle.Flow)
			{
				return new FlowLayoutSettings(this);
			}
			if (layoutStyle != ToolStripLayoutStyle.Table)
			{
				return null;
			}
			return new TableLayoutSettings(this);
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x0010CEF0 File Offset: 0x0010B0F0
		protected internal virtual ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			if (text == "-")
			{
				return new ToolStripSeparator();
			}
			return new ToolStripButton(text, image, onClick);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x0010CF0D File Offset: 0x0010B10D
		private void ClearAllSelections()
		{
			this.ClearAllSelectionsExcept(null);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x0010CF18 File Offset: 0x0010B118
		private void ClearAllSelectionsExcept(ToolStripItem item)
		{
			Rectangle rectangle = (item == null) ? Rectangle.Empty : item.Bounds;
			Region region = null;
			try
			{
				for (int i = 0; i < this.DisplayedItems.Count; i++)
				{
					if (this.DisplayedItems[i] != item)
					{
						if (item != null && this.DisplayedItems[i].Pressed)
						{
							ToolStripDropDownItem toolStripDropDownItem = this.DisplayedItems[i] as ToolStripDropDownItem;
							if (toolStripDropDownItem != null && toolStripDropDownItem.HasDropDownItems)
							{
								toolStripDropDownItem.AutoHide(item);
							}
						}
						bool flag = false;
						if (this.DisplayedItems[i].Selected)
						{
							this.DisplayedItems[i].Unselect();
							flag = true;
						}
						if (flag)
						{
							if (region == null)
							{
								region = new Region(rectangle);
							}
							region.Union(this.DisplayedItems[i].Bounds);
						}
					}
				}
				if (region != null)
				{
					base.Invalidate(region, true);
					base.Update();
				}
				else if (rectangle != Rectangle.Empty)
				{
					base.Invalidate(rectangle, true);
					base.Update();
				}
			}
			finally
			{
				if (region != null)
				{
					region.Dispose();
				}
			}
			if (base.IsHandleCreated && item != null)
			{
				int childID = this.DisplayedItems.IndexOf(item);
				base.AccessibilityNotifyClients(AccessibleEvents.Focus, childID);
			}
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x0010D060 File Offset: 0x0010B260
		internal void ClearInsertionMark()
		{
			if (this.lastInsertionMarkRect != Rectangle.Empty)
			{
				Rectangle rc = this.lastInsertionMarkRect;
				this.lastInsertionMarkRect = Rectangle.Empty;
				base.Invalidate(rc);
			}
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x0010D098 File Offset: 0x0010B298
		private void ClearLastMouseDownedItem()
		{
			ToolStripItem toolStripItem = this.lastMouseDownedItem;
			this.lastMouseDownedItem = null;
			if (this.IsSelectionSuspended)
			{
				this.SetToolStripState(16384, false);
				if (toolStripItem != null)
				{
					toolStripItem.Invalidate();
				}
			}
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x0010D0D0 File Offset: 0x0010B2D0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ToolStripOverflow overflow = this.GetOverflow();
				try
				{
					base.SuspendLayout();
					if (overflow != null)
					{
						overflow.SuspendLayout();
					}
					this.SetToolStripState(4, true);
					this.lastMouseDownedItem = null;
					this.HookStaticEvents(false);
					ToolStripPanelCell toolStripPanelCell = base.Properties.GetObject(ToolStrip.PropToolStripPanelCell) as ToolStripPanelCell;
					if (toolStripPanelCell != null)
					{
						toolStripPanelCell.Dispose();
					}
					if (this.cachedItemHdcInfo != null)
					{
						this.cachedItemHdcInfo.Dispose();
					}
					if (this.mouseHoverTimer != null)
					{
						this.mouseHoverTimer.Dispose();
					}
					ToolTip toolTip = (ToolTip)base.Properties.GetObject(ToolStrip.PropToolTip);
					if (toolTip != null)
					{
						toolTip.Dispose();
					}
					if (!this.Items.IsReadOnly)
					{
						for (int i = this.Items.Count - 1; i >= 0; i--)
						{
							this.Items[i].Dispose();
						}
						this.Items.Clear();
					}
					if (this.toolStripGrip != null)
					{
						this.toolStripGrip.Dispose();
					}
					if (this.toolStripOverflowButton != null)
					{
						this.toolStripOverflowButton.Dispose();
					}
					if (this.restoreFocusFilter != null)
					{
						Application.ThreadContext.FromCurrent().RemoveMessageFilter(this.restoreFocusFilter);
						this.restoreFocusFilter = null;
					}
					bool flag = false;
					if (ToolStripManager.ModalMenuFilter.GetActiveToolStrip() == this)
					{
						flag = true;
					}
					ToolStripManager.ModalMenuFilter.RemoveActiveToolStrip(this);
					if (flag && ToolStripManager.ModalMenuFilter.GetActiveToolStrip() == null)
					{
						ToolStripManager.ModalMenuFilter.ExitMenuMode();
					}
					ToolStripManager.ToolStrips.Remove(this);
				}
				finally
				{
					base.ResumeLayout(false);
					if (overflow != null)
					{
						overflow.ResumeLayout(false);
					}
					this.SetToolStripState(4, false);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x0010D26C File Offset: 0x0010B46C
		internal void DoLayoutIfHandleCreated(ToolStripItemEventArgs e)
		{
			if (base.IsHandleCreated)
			{
				LayoutTransaction.DoLayout(this, e.Item, PropertyNames.Items);
				base.Invalidate();
				if (this.CanOverflow && this.OverflowButton.HasDropDown)
				{
					if (this.DeferOverflowDropDownLayout())
					{
						CommonProperties.xClearPreferredSizeCache(this.OverflowButton.DropDown);
						this.OverflowButton.DropDown.LayoutRequired = true;
						return;
					}
					LayoutTransaction.DoLayout(this.OverflowButton.DropDown, e.Item, PropertyNames.Items);
					this.OverflowButton.DropDown.Invalidate();
					return;
				}
			}
			else
			{
				CommonProperties.xClearPreferredSizeCache(this);
				this.LayoutRequired = true;
				if (this.CanOverflow && this.OverflowButton.HasDropDown)
				{
					this.OverflowButton.DropDown.LayoutRequired = true;
				}
			}
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x0010D33F File Offset: 0x0010B53F
		private bool DeferOverflowDropDownLayout()
		{
			return base.IsLayoutSuspended || !this.OverflowButton.DropDown.Visible || !this.OverflowButton.DropDown.IsHandleCreated;
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x0010D370 File Offset: 0x0010B570
		void ISupportToolStripPanel.EndDrag()
		{
			ToolStripPanel.ClearDragFeedback();
			this.OnEndDrag(EventArgs.Empty);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x0010D382 File Offset: 0x0010B582
		internal ToolStripOverflow GetOverflow()
		{
			if (this.toolStripOverflowButton != null && this.toolStripOverflowButton.HasDropDown)
			{
				return this.toolStripOverflowButton.DropDown as ToolStripOverflow;
			}
			return null;
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x0010D3AB File Offset: 0x0010B5AB
		internal byte GetMouseId()
		{
			if (this.mouseDownID == 0)
			{
				this.mouseDownID += 1;
			}
			return this.mouseDownID;
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x0010D3CA File Offset: 0x0010B5CA
		internal virtual ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction, bool rtlAware)
		{
			if (rtlAware && this.RightToLeft == RightToLeft.Yes)
			{
				if (direction == ArrowDirection.Right)
				{
					direction = ArrowDirection.Left;
				}
				else if (direction == ArrowDirection.Left)
				{
					direction = ArrowDirection.Right;
				}
			}
			return this.GetNextItem(start, direction);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x0010D3F4 File Offset: 0x0010B5F4
		public virtual ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction)
		{
			if (!WindowsFormsUtils.EnumValidator.IsValidArrowDirection(direction))
			{
				throw new InvalidEnumArgumentException("direction", (int)direction, typeof(ArrowDirection));
			}
			if (direction <= ArrowDirection.Up)
			{
				if (direction == ArrowDirection.Left)
				{
					return this.GetNextItemHorizontal(start, false);
				}
				if (direction == ArrowDirection.Up)
				{
					return this.GetNextItemVertical(start, false);
				}
			}
			else
			{
				if (direction == ArrowDirection.Right)
				{
					return this.GetNextItemHorizontal(start, true);
				}
				if (direction == ArrowDirection.Down)
				{
					return this.GetNextItemVertical(start, true);
				}
			}
			return null;
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x0010D460 File Offset: 0x0010B660
		private ToolStripItem GetNextItemHorizontal(ToolStripItem start, bool forward)
		{
			if (this.DisplayedItems.Count <= 0)
			{
				return null;
			}
			if (start == null)
			{
				start = this.GetStartItem(forward);
			}
			int num = this.DisplayedItems.IndexOf(start);
			if (num == -1)
			{
				return null;
			}
			int count = this.DisplayedItems.Count;
			for (;;)
			{
				if (forward)
				{
					num = (num + 1) % count;
				}
				else
				{
					num = ((--num < 0) ? (count + num) : num);
				}
				ToolStripDropDown toolStripDropDown = this as ToolStripDropDown;
				if (toolStripDropDown != null && toolStripDropDown.OwnerItem != null && toolStripDropDown.OwnerItem.IsInDesignMode)
				{
					break;
				}
				if (this.DisplayedItems[num].CanKeyboardSelect)
				{
					goto Block_9;
				}
				if (this.DisplayedItems[num] == start)
				{
					goto Block_10;
				}
			}
			return this.DisplayedItems[num];
			Block_9:
			return this.DisplayedItems[num];
			Block_10:
			return null;
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x0010D520 File Offset: 0x0010B720
		private ToolStripItem GetStartItem(bool forward)
		{
			if (forward)
			{
				return this.DisplayedItems[this.DisplayedItems.Count - 1];
			}
			if (AccessibilityImprovements.Level3 && !(this is ToolStripDropDown))
			{
				return this.DisplayedItems[(this.DisplayedItems.Count > 1) ? 1 : 0];
			}
			return this.DisplayedItems[0];
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x0010D584 File Offset: 0x0010B784
		private ToolStripItem GetNextItemVertical(ToolStripItem selectedItem, bool down)
		{
			ToolStripItem toolStripItem = null;
			ToolStripItem toolStripItem2 = null;
			double num = double.MaxValue;
			double num2 = double.MaxValue;
			double num3 = double.MaxValue;
			if (selectedItem == null)
			{
				return this.GetNextItemHorizontal(selectedItem, down);
			}
			ToolStripDropDown toolStripDropDown = this as ToolStripDropDown;
			if (toolStripDropDown != null && toolStripDropDown.OwnerItem != null && (toolStripDropDown.OwnerItem.IsInDesignMode || (toolStripDropDown.OwnerItem.Owner != null && toolStripDropDown.OwnerItem.Owner.IsInDesignMode)))
			{
				return this.GetNextItemHorizontal(selectedItem, down);
			}
			Point point = new Point(selectedItem.Bounds.X + selectedItem.Width / 2, selectedItem.Bounds.Y + selectedItem.Height / 2);
			for (int i = 0; i < this.DisplayedItems.Count; i++)
			{
				ToolStripItem toolStripItem3 = this.DisplayedItems[i];
				if (toolStripItem3 != selectedItem && toolStripItem3.CanKeyboardSelect && (down || toolStripItem3.Bounds.Bottom <= selectedItem.Bounds.Top) && (!down || toolStripItem3.Bounds.Top >= selectedItem.Bounds.Bottom))
				{
					Point point2 = new Point(toolStripItem3.Bounds.X + toolStripItem3.Width / 2, down ? toolStripItem3.Bounds.Top : toolStripItem3.Bounds.Bottom);
					int num4 = point2.X - point.X;
					int num5 = point2.Y - point.Y;
					double num6 = Math.Sqrt((double)(num5 * num5 + num4 * num4));
					if (num5 != 0)
					{
						double num7 = Math.Abs(Math.Atan((double)(num4 / num5)));
						num2 = Math.Min(num2, num7);
						num = Math.Min(num, num6);
						if (num2 == num7 && num2 != double.NaN)
						{
							toolStripItem = toolStripItem3;
						}
						if (num == num6)
						{
							toolStripItem2 = toolStripItem3;
							num3 = num7;
						}
					}
				}
			}
			if (toolStripItem == null || toolStripItem2 == null)
			{
				return this.GetNextItemHorizontal(null, down);
			}
			if (num3 == num2)
			{
				return toolStripItem2;
			}
			if ((!down && toolStripItem.Bounds.Bottom <= toolStripItem2.Bounds.Top) || (down && toolStripItem.Bounds.Top > toolStripItem2.Bounds.Bottom))
			{
				return toolStripItem2;
			}
			return toolStripItem;
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x0010D810 File Offset: 0x0010BA10
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (proposedSize.Width == 1)
			{
				proposedSize.Width = int.MaxValue;
			}
			if (proposedSize.Height == 1)
			{
				proposedSize.Height = int.MaxValue;
			}
			Padding padding = base.Padding;
			Size preferredSize = this.LayoutEngine.GetPreferredSize(this, proposedSize - padding.Size);
			Padding padding2 = base.Padding;
			if (padding != padding2)
			{
				CommonProperties.xClearPreferredSizeCache(this);
			}
			return preferredSize + padding2.Size;
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x0010D890 File Offset: 0x0010BA90
		internal static Size GetPreferredSizeHorizontal(IArrangedElement container, Size proposedConstraints)
		{
			Size size = Size.Empty;
			ToolStrip toolStrip = container as ToolStrip;
			Size size2 = toolStrip.DefaultSize - toolStrip.Padding.Size;
			size.Height = Math.Max(0, size2.Height);
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < toolStrip.Items.Count; i++)
			{
				ToolStripItem toolStripItem = toolStrip.Items[i];
				if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
				{
					flag2 = true;
					if (toolStripItem.Overflow != ToolStripItemOverflow.Always)
					{
						Padding margin = toolStripItem.Margin;
						Size preferredItemSize = ToolStrip.GetPreferredItemSize(toolStripItem);
						size.Width += margin.Horizontal + preferredItemSize.Width;
						size.Height = Math.Max(size.Height, margin.Vertical + preferredItemSize.Height);
					}
					else
					{
						flag = true;
					}
				}
			}
			if (toolStrip.Items.Count == 0 || !flag2)
			{
				size = size2;
			}
			if (flag)
			{
				ToolStripOverflowButton overflowButton = toolStrip.OverflowButton;
				Padding margin2 = overflowButton.Margin;
				size.Width += margin2.Horizontal + overflowButton.Bounds.Width;
			}
			else
			{
				size.Width += 2;
			}
			if (toolStrip.GripStyle == ToolStripGripStyle.Visible)
			{
				Padding gripMargin = toolStrip.GripMargin;
				size.Width += gripMargin.Horizontal + toolStrip.Grip.GripThickness;
			}
			size = LayoutUtils.IntersectSizes(size, proposedConstraints);
			return size;
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x0010DA14 File Offset: 0x0010BC14
		internal static Size GetPreferredSizeVertical(IArrangedElement container, Size proposedConstraints)
		{
			Size size = Size.Empty;
			bool flag = false;
			ToolStrip toolStrip = container as ToolStrip;
			bool flag2 = false;
			for (int i = 0; i < toolStrip.Items.Count; i++)
			{
				ToolStripItem toolStripItem = toolStrip.Items[i];
				if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
				{
					flag2 = true;
					if (toolStripItem.Overflow != ToolStripItemOverflow.Always)
					{
						Size preferredItemSize = ToolStrip.GetPreferredItemSize(toolStripItem);
						Padding margin = toolStripItem.Margin;
						size.Height += margin.Vertical + preferredItemSize.Height;
						size.Width = Math.Max(size.Width, margin.Horizontal + preferredItemSize.Width);
					}
					else
					{
						flag = true;
					}
				}
			}
			if (toolStrip.Items.Count == 0 || !flag2)
			{
				size = LayoutUtils.FlipSize(toolStrip.DefaultSize);
			}
			if (flag)
			{
				ToolStripOverflowButton overflowButton = toolStrip.OverflowButton;
				Padding margin2 = overflowButton.Margin;
				size.Height += margin2.Vertical + overflowButton.Bounds.Height;
			}
			else
			{
				size.Height += 2;
			}
			if (toolStrip.GripStyle == ToolStripGripStyle.Visible)
			{
				Padding gripMargin = toolStrip.GripMargin;
				size.Height += gripMargin.Vertical + toolStrip.Grip.GripThickness;
			}
			if (toolStrip.Size != size)
			{
				CommonProperties.xClearPreferredSizeCache(toolStrip);
			}
			return size;
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x0010DB76 File Offset: 0x0010BD76
		private static Size GetPreferredItemSize(ToolStripItem item)
		{
			if (!item.AutoSize)
			{
				return item.Size;
			}
			return item.GetPreferredSize(Size.Empty);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x0010DB92 File Offset: 0x0010BD92
		internal static Graphics GetMeasurementGraphics()
		{
			return WindowsFormsUtils.CreateMeasurementGraphics();
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x0010DB9C File Offset: 0x0010BD9C
		internal ToolStripItem GetSelectedItem()
		{
			ToolStripItem result = null;
			for (int i = 0; i < this.DisplayedItems.Count; i++)
			{
				if (this.DisplayedItems[i].Selected)
				{
					result = this.DisplayedItems[i];
				}
			}
			return result;
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x0010DBE2 File Offset: 0x0010BDE2
		internal bool GetToolStripState(int flag)
		{
			return (this.toolStripState & flag) != 0;
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00006C59 File Offset: 0x00004E59
		internal virtual ToolStrip GetToplevelOwnerToolStrip()
		{
			return this;
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00006C59 File Offset: 0x00004E59
		internal virtual Control GetOwnerControl()
		{
			return this;
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x0010DBF0 File Offset: 0x0010BDF0
		private void HandleMouseLeave()
		{
			if (this.lastMouseActiveItem != null)
			{
				if (!base.DesignMode)
				{
					this.MouseHoverTimer.Cancel(this.lastMouseActiveItem);
				}
				try
				{
					this.lastMouseActiveItem.FireEvent(EventArgs.Empty, ToolStripItemEventType.MouseLeave);
				}
				finally
				{
					this.lastMouseActiveItem = null;
				}
			}
			ToolStripMenuItem.MenuTimer.HandleToolStripMouseLeave(this);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x0010DC58 File Offset: 0x0010BE58
		internal void HandleItemClick(ToolStripItem dismissingItem)
		{
			ToolStripItemClickedEventArgs e = new ToolStripItemClickedEventArgs(dismissingItem);
			this.OnItemClicked(e);
			if (!this.IsDropDown && dismissingItem.IsOnOverflow)
			{
				this.OverflowButton.DropDown.HandleItemClick(dismissingItem);
			}
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x0010DC94 File Offset: 0x0010BE94
		internal virtual void HandleItemClicked(ToolStripItem dismissingItem)
		{
			ToolStripDropDownItem toolStripDropDownItem = dismissingItem as ToolStripDropDownItem;
			if (toolStripDropDownItem != null && !toolStripDropDownItem.HasDropDownItems)
			{
				this.KeyboardActive = false;
			}
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x0010DCBC File Offset: 0x0010BEBC
		private void HookStaticEvents(bool hook)
		{
			if (hook)
			{
				if (this.alreadyHooked)
				{
					return;
				}
				try
				{
					ToolStripManager.RendererChanged += this.OnDefaultRendererChanged;
					SystemEvents.UserPreferenceChanged += this.OnUserPreferenceChanged;
					return;
				}
				finally
				{
					this.alreadyHooked = true;
				}
			}
			if (this.alreadyHooked)
			{
				try
				{
					ToolStripManager.RendererChanged -= this.OnDefaultRendererChanged;
					SystemEvents.UserPreferenceChanged -= this.OnUserPreferenceChanged;
				}
				finally
				{
					this.alreadyHooked = false;
				}
			}
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x0010DD50 File Offset: 0x0010BF50
		private void InitializeRenderer(ToolStripRenderer renderer)
		{
			using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this, this, PropertyNames.Renderer))
			{
				renderer.Initialize(this);
				for (int i = 0; i < this.Items.Count; i++)
				{
					renderer.InitializeItem(this.Items[i]);
				}
			}
			base.Invalidate(this.Controls.Count > 0);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x0010DDD0 File Offset: 0x0010BFD0
		private void InvalidateLayout()
		{
			if (base.IsHandleCreated)
			{
				LayoutTransaction.DoLayout(this, this, null);
			}
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x0010DDE4 File Offset: 0x0010BFE4
		internal void InvalidateTextItems()
		{
			using (new LayoutTransaction(this, this, "ShowKeyboardFocusCues", base.Visible))
			{
				for (int i = 0; i < this.DisplayedItems.Count; i++)
				{
					if ((this.DisplayedItems[i].DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
					{
						this.DisplayedItems[i].InvalidateItemLayout("ShowKeyboardFocusCues");
					}
				}
			}
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x0010DE64 File Offset: 0x0010C064
		protected override bool IsInputKey(Keys keyData)
		{
			ToolStripItem selectedItem = this.GetSelectedItem();
			return (selectedItem != null && selectedItem.IsInputKey(keyData)) || base.IsInputKey(keyData);
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x0010DE90 File Offset: 0x0010C090
		protected override bool IsInputChar(char charCode)
		{
			ToolStripItem selectedItem = this.GetSelectedItem();
			return (selectedItem != null && selectedItem.IsInputChar(charCode)) || base.IsInputChar(charCode);
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x0010DEBC File Offset: 0x0010C0BC
		private static bool IsPseudoMnemonic(char charCode, string text)
		{
			if (!string.IsNullOrEmpty(text) && !WindowsFormsUtils.ContainsMnemonic(text))
			{
				char c = char.ToUpper(charCode, CultureInfo.CurrentCulture);
				char c2 = char.ToUpper(text[0], CultureInfo.CurrentCulture);
				if (c2 == c || char.ToLower(charCode, CultureInfo.CurrentCulture) == char.ToLower(text[0], CultureInfo.CurrentCulture))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x0010DF1C File Offset: 0x0010C11C
		internal void InvokePaintItem(ToolStripItem item)
		{
			base.Invalidate(item.Bounds);
			base.Update();
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x0010DF30 File Offset: 0x0010C130
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x0010DF38 File Offset: 0x0010C138
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			Point location = base.Location;
			if (!this.IsCurrentlyDragging && !this.IsLocationChanging && this.IsInToolStripPanel)
			{
				ToolStripLocationCancelEventArgs toolStripLocationCancelEventArgs = new ToolStripLocationCancelEventArgs(new Point(x, y), false);
				try
				{
					if (location.X != x || location.Y != y)
					{
						this.SetToolStripState(1024, true);
						this.OnLocationChanging(toolStripLocationCancelEventArgs);
					}
					if (!toolStripLocationCancelEventArgs.Cancel)
					{
						base.SetBoundsCore(x, y, width, height, specified);
					}
					return;
				}
				finally
				{
					this.SetToolStripState(1024, false);
				}
			}
			if (this.IsCurrentlyDragging)
			{
				Region transparentRegion = this.Renderer.GetTransparentRegion(this);
				if (transparentRegion != null && (location.X != x || location.Y != y))
				{
					try
					{
						base.Invalidate(transparentRegion);
						base.Update();
					}
					finally
					{
						transparentRegion.Dispose();
					}
				}
			}
			this.SetToolStripState(1024, false);
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000072B6 File Offset: 0x000054B6
		internal void PaintParentRegion(Graphics g, Region region)
		{
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x0010E038 File Offset: 0x0010C238
		internal bool ProcessCmdKeyInternal(ref Message m, Keys keyData)
		{
			return this.ProcessCmdKey(ref m, keyData);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x0010E044 File Offset: 0x0010C244
		internal override void PrintToMetaFileRecursive(HandleRef hDC, IntPtr lParam, Rectangle bounds)
		{
			using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					IntPtr hdc = graphics.GetHdc();
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 791, hdc, (IntPtr)30);
					IntPtr handle = hDC.Handle;
					SafeNativeMethods.BitBlt(new HandleRef(this, handle), bounds.X, bounds.Y, bounds.Width, bounds.Height, new HandleRef(graphics, hdc), 0, 0, 13369376);
					graphics.ReleaseHdcInternal(hdc);
				}
			}
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x0010E10C File Offset: 0x0010C30C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (ToolStripManager.IsMenuKey(keyData) && !this.IsDropDown && ToolStripManager.ModalMenuFilter.InMenuMode)
			{
				this.ClearAllSelections();
				ToolStripManager.ModalMenuFilter.MenuKeyToggle = true;
				ToolStripManager.ModalMenuFilter.ExitMenuMode();
			}
			ToolStripItem selectedItem = this.GetSelectedItem();
			if (selectedItem != null && selectedItem.ProcessCmdKey(ref m, keyData))
			{
				return true;
			}
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem != selectedItem && toolStripItem.ProcessCmdKey(ref m, keyData))
				{
					return true;
				}
			}
			if (!this.IsDropDown)
			{
				bool flag = (keyData & Keys.Control) == Keys.Control && (keyData & Keys.KeyCode) == Keys.Tab;
				if (flag && !this.TabStop && this.HasKeyboardInput)
				{
					bool flag2;
					if ((keyData & Keys.Shift) == Keys.None)
					{
						flag2 = ToolStripManager.SelectNextToolStrip(this, true);
					}
					else
					{
						flag2 = ToolStripManager.SelectNextToolStrip(this, false);
					}
					if (flag2)
					{
						return true;
					}
				}
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x0010E220 File Offset: 0x0010C420
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			bool flag = false;
			ToolStripItem selectedItem = this.GetSelectedItem();
			if (selectedItem != null && selectedItem.ProcessDialogKey(keyData))
			{
				return true;
			}
			bool flag2 = (keyData & (Keys.Control | Keys.Alt)) > Keys.None;
			Keys keys = keyData & Keys.KeyCode;
			if (keys <= Keys.Tab)
			{
				if (keys != Keys.Back)
				{
					if (keys == Keys.Tab)
					{
						if (!flag2)
						{
							flag = this.ProcessTabKey((keyData & Keys.Shift) == Keys.None);
						}
					}
				}
				else if (!base.ContainsFocus)
				{
					flag = this.ProcessTabKey(false);
				}
			}
			else if (keys != Keys.Escape)
			{
				switch (keys)
				{
				case Keys.End:
					this.SelectNextToolStripItem(null, false);
					flag = true;
					break;
				case Keys.Home:
					this.SelectNextToolStripItem(null, true);
					flag = true;
					break;
				case Keys.Left:
				case Keys.Up:
				case Keys.Right:
				case Keys.Down:
					flag = this.ProcessArrowKey(keys);
					break;
				}
			}
			else if (!flag2 && !this.TabStop)
			{
				this.RestoreFocusInternal();
				flag = true;
			}
			if (flag)
			{
				return flag;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x0010E2FD File Offset: 0x0010C4FD
		internal virtual void ProcessDuplicateMnemonic(ToolStripItem item, char charCode)
		{
			if (!this.CanProcessMnemonic())
			{
				return;
			}
			if (item != null)
			{
				this.SetFocusUnsafe();
				item.Select();
			}
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x0010E318 File Offset: 0x0010C518
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (!this.CanProcessMnemonic())
			{
				return false;
			}
			if (this.Focused || base.ContainsFocus)
			{
				return this.ProcessMnemonicInternal(charCode);
			}
			bool inMenuMode = ToolStripManager.ModalMenuFilter.InMenuMode;
			if (!inMenuMode && Control.ModifierKeys == Keys.Alt)
			{
				return this.ProcessMnemonicInternal(charCode);
			}
			return inMenuMode && ToolStripManager.ModalMenuFilter.GetActiveToolStrip() == this && this.ProcessMnemonicInternal(charCode);
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x0010E378 File Offset: 0x0010C578
		private bool ProcessMnemonicInternal(char charCode)
		{
			if (!this.CanProcessMnemonic())
			{
				return false;
			}
			ToolStripItem selectedItem = this.GetSelectedItem();
			int num = 0;
			if (selectedItem != null)
			{
				num = this.DisplayedItems.IndexOf(selectedItem);
			}
			num = Math.Max(0, num);
			ToolStripItem toolStripItem = null;
			bool flag = false;
			int num2 = num;
			for (int i = 0; i < this.DisplayedItems.Count; i++)
			{
				ToolStripItem toolStripItem2 = this.DisplayedItems[num2];
				num2 = (num2 + 1) % this.DisplayedItems.Count;
				if (!string.IsNullOrEmpty(toolStripItem2.Text) && toolStripItem2.Enabled && (toolStripItem2.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
				{
					flag = (flag || toolStripItem2 is ToolStripMenuItem);
					if (Control.IsMnemonic(charCode, toolStripItem2.Text))
					{
						if (toolStripItem != null)
						{
							if (toolStripItem == selectedItem)
							{
								this.ProcessDuplicateMnemonic(toolStripItem2, charCode);
							}
							else
							{
								this.ProcessDuplicateMnemonic(toolStripItem, charCode);
							}
							return true;
						}
						toolStripItem = toolStripItem2;
					}
				}
			}
			if (toolStripItem != null)
			{
				return toolStripItem.ProcessMnemonic(charCode);
			}
			if (!flag)
			{
				return false;
			}
			num2 = num;
			for (int j = 0; j < this.DisplayedItems.Count; j++)
			{
				ToolStripItem toolStripItem3 = this.DisplayedItems[num2];
				num2 = (num2 + 1) % this.DisplayedItems.Count;
				if (toolStripItem3 is ToolStripMenuItem && !string.IsNullOrEmpty(toolStripItem3.Text) && toolStripItem3.Enabled && (toolStripItem3.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text && ToolStrip.IsPseudoMnemonic(charCode, toolStripItem3.Text))
				{
					if (toolStripItem != null)
					{
						if (toolStripItem == selectedItem)
						{
							this.ProcessDuplicateMnemonic(toolStripItem3, charCode);
						}
						else
						{
							this.ProcessDuplicateMnemonic(toolStripItem, charCode);
						}
						return true;
					}
					toolStripItem = toolStripItem3;
				}
			}
			return toolStripItem != null && toolStripItem.ProcessMnemonic(charCode);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x0010E518 File Offset: 0x0010C718
		private bool ProcessTabKey(bool forward)
		{
			if (this.TabStop)
			{
				return false;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				forward = !forward;
			}
			this.SelectNextToolStripItem(this.GetSelectedItem(), forward);
			return true;
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x0010E544 File Offset: 0x0010C744
		internal virtual bool ProcessArrowKey(Keys keyCode)
		{
			bool result = false;
			ToolStripMenuItem.MenuTimer.Cancel();
			switch (keyCode)
			{
			case Keys.Left:
			case Keys.Right:
				result = this.ProcessLeftRightArrowKey(keyCode == Keys.Right);
				break;
			case Keys.Up:
			case Keys.Down:
				if (this.IsDropDown || this.Orientation != Orientation.Horizontal)
				{
					ToolStripItem selectedItem = this.GetSelectedItem();
					if (keyCode == Keys.Down)
					{
						ToolStripItem nextItem = this.GetNextItem(selectedItem, ArrowDirection.Down);
						if (nextItem != null)
						{
							this.ChangeSelection(nextItem);
							result = true;
						}
					}
					else
					{
						ToolStripItem nextItem2 = this.GetNextItem(selectedItem, ArrowDirection.Up);
						if (nextItem2 != null)
						{
							this.ChangeSelection(nextItem2);
							result = true;
						}
					}
				}
				break;
			}
			return result;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x0010E5D0 File Offset: 0x0010C7D0
		private bool ProcessLeftRightArrowKey(bool right)
		{
			ToolStripItem selectedItem = this.GetSelectedItem();
			ToolStripItem toolStripItem = this.SelectNextToolStripItem(this.GetSelectedItem(), right);
			return true;
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x0010E5F3 File Offset: 0x0010C7F3
		internal void NotifySelectionChange(ToolStripItem item)
		{
			if (item == null)
			{
				this.ClearAllSelections();
				return;
			}
			if (item.Selected)
			{
				this.ClearAllSelectionsExcept(item);
			}
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x0010E60E File Offset: 0x0010C80E
		private void OnDefaultRendererChanged(object sender, EventArgs e)
		{
			if (this.GetToolStripState(64))
			{
				this.OnRendererChanged(e);
			}
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x0010E624 File Offset: 0x0010C824
		protected virtual void OnBeginDrag(EventArgs e)
		{
			this.SetToolStripState(2048, true);
			this.ClearAllSelections();
			this.UpdateToolTip(null);
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.EventBeginDrag];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x0010E66C File Offset: 0x0010C86C
		protected virtual void OnEndDrag(EventArgs e)
		{
			this.SetToolStripState(2048, false);
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.EventEndDrag];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x0010E6A6 File Offset: 0x0010C8A6
		protected override void OnDockChanged(EventArgs e)
		{
			base.OnDockChanged(e);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x0010E6B0 File Offset: 0x0010C8B0
		protected virtual void OnRendererChanged(EventArgs e)
		{
			this.InitializeRenderer(this.Renderer);
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.EventRendererChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x0010E6EC File Offset: 0x0010C8EC
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i] != null && this.Items[i].ParentInternal == this)
				{
					this.Items[i].OnParentEnabledChanged(e);
				}
			}
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x0010E74A File Offset: 0x0010C94A
		internal void OnDefaultFontChanged()
		{
			this.defaultFont = null;
			if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
			{
				ToolStripManager.CurrentDpi = base.DeviceDpi;
				this.defaultFont = ToolStripManager.DefaultFont;
			}
			if (!base.IsFontSet())
			{
				this.OnFontChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x0010E784 File Offset: 0x0010C984
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.Items[i].OnOwnerFontChanged(e);
			}
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x0010E7C0 File Offset: 0x0010C9C0
		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x0010E7C9 File Offset: 0x0010C9C9
		protected override void OnHandleCreated(EventArgs e)
		{
			if ((this.AllowDrop || this.AllowItemReorder) && this.DropTargetManager != null)
			{
				this.DropTargetManager.EnsureRegistered(this);
			}
			base.OnHandleCreated(e);
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x0010E7F6 File Offset: 0x0010C9F6
		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (this.DropTargetManager != null)
			{
				this.DropTargetManager.EnsureUnRegistered(this);
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x0010E814 File Offset: 0x0010CA14
		protected internal virtual void OnItemAdded(ToolStripItemEventArgs e)
		{
			this.DoLayoutIfHandleCreated(e);
			if (!this.HasVisibleItems && e.Item != null && ((IArrangedElement)e.Item).ParticipatesInLayout)
			{
				this.HasVisibleItems = true;
			}
			ToolStripItemEventHandler toolStripItemEventHandler = (ToolStripItemEventHandler)base.Events[ToolStrip.EventItemAdded];
			if (toolStripItemEventHandler != null)
			{
				toolStripItemEventHandler(this, e);
			}
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x0010E870 File Offset: 0x0010CA70
		protected virtual void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			ToolStripItemClickedEventHandler toolStripItemClickedEventHandler = (ToolStripItemClickedEventHandler)base.Events[ToolStrip.EventItemClicked];
			if (toolStripItemClickedEventHandler != null)
			{
				toolStripItemClickedEventHandler(this, e);
			}
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0010E8A0 File Offset: 0x0010CAA0
		protected internal virtual void OnItemRemoved(ToolStripItemEventArgs e)
		{
			this.OnItemVisibleChanged(e, true);
			ToolStripItemEventHandler toolStripItemEventHandler = (ToolStripItemEventHandler)base.Events[ToolStrip.EventItemRemoved];
			if (toolStripItemEventHandler != null)
			{
				toolStripItemEventHandler(this, e);
			}
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x0010E8D8 File Offset: 0x0010CAD8
		internal void OnItemVisibleChanged(ToolStripItemEventArgs e, bool performLayout)
		{
			if (e.Item == this.lastMouseActiveItem)
			{
				this.lastMouseActiveItem = null;
			}
			if (e.Item == this.LastMouseDownedItem)
			{
				this.lastMouseDownedItem = null;
			}
			if (e.Item == this.currentlyActiveTooltipItem)
			{
				this.UpdateToolTip(null);
			}
			if (performLayout)
			{
				this.DoLayoutIfHandleCreated(e);
			}
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x0010E930 File Offset: 0x0010CB30
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.LayoutRequired = false;
			ToolStripOverflow overflow = this.GetOverflow();
			if (overflow != null)
			{
				overflow.SuspendLayout();
				this.toolStripOverflowButton.Size = this.toolStripOverflowButton.GetPreferredSize(this.DisplayRectangle.Size - base.Padding.Size);
			}
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.Items[i].OnLayout(e);
			}
			base.OnLayout(e);
			this.SetDisplayedItems();
			this.OnLayoutCompleted(EventArgs.Empty);
			base.Invalidate();
			if (overflow != null)
			{
				overflow.ResumeLayout();
			}
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x0010E9DC File Offset: 0x0010CBDC
		protected virtual void OnLayoutCompleted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.EventLayoutCompleted];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x0010EA0C File Offset: 0x0010CC0C
		protected virtual void OnLayoutStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.EventLayoutStyleChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x0010EA3A File Offset: 0x0010CC3A
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.ClearAllSelections();
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x0010EA49 File Offset: 0x0010CC49
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			if (!this.IsDropDown)
			{
				Application.ThreadContext.FromCurrent().RemoveMessageFilter(this.RestoreFocusFilter);
			}
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x0010EA6C File Offset: 0x0010CC6C
		internal virtual void OnLocationChanging(ToolStripLocationCancelEventArgs e)
		{
			ToolStripLocationCancelEventHandler toolStripLocationCancelEventHandler = (ToolStripLocationCancelEventHandler)base.Events[ToolStrip.EventLocationChanging];
			if (toolStripLocationCancelEventHandler != null)
			{
				toolStripLocationCancelEventHandler(this, e);
			}
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x0010EA9C File Offset: 0x0010CC9C
		protected override void OnMouseDown(MouseEventArgs mea)
		{
			this.mouseDownID += 1;
			ToolStripItem itemAt = this.GetItemAt(mea.X, mea.Y);
			if (itemAt != null)
			{
				if (!this.IsDropDown && !(itemAt is ToolStripDropDownItem))
				{
					this.SetToolStripState(16384, true);
					base.CaptureInternal = true;
				}
				this.MenuAutoExpand = true;
				if (mea != null)
				{
					Point point = itemAt.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripCoords, ToolStripPointType.ToolStripItemCoords);
					mea = new MouseEventArgs(mea.Button, mea.Clicks, point.X, point.Y, mea.Delta);
				}
				this.lastMouseDownedItem = itemAt;
				itemAt.FireEvent(mea, ToolStripItemEventType.MouseDown);
				return;
			}
			base.OnMouseDown(mea);
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x0010EB54 File Offset: 0x0010CD54
		protected override void OnMouseMove(MouseEventArgs mea)
		{
			ToolStripItem toolStripItem = this.GetItemAt(mea.X, mea.Y);
			if (!this.Grip.MovingToolStrip)
			{
				if (toolStripItem != this.lastMouseActiveItem)
				{
					this.HandleMouseLeave();
					this.lastMouseActiveItem = ((toolStripItem is ToolStripControlHost) ? null : toolStripItem);
					if (this.lastMouseActiveItem != null)
					{
						toolStripItem.FireEvent(new EventArgs(), ToolStripItemEventType.MouseEnter);
					}
					if (!base.DesignMode)
					{
						this.MouseHoverTimer.Start(this.lastMouseActiveItem);
					}
				}
			}
			else
			{
				toolStripItem = this.Grip;
			}
			if (toolStripItem != null)
			{
				Point point = toolStripItem.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripCoords, ToolStripPointType.ToolStripItemCoords);
				mea = new MouseEventArgs(mea.Button, mea.Clicks, point.X, point.Y, mea.Delta);
				toolStripItem.FireEvent(mea, ToolStripItemEventType.MouseMove);
				return;
			}
			base.OnMouseMove(mea);
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x0010EC2C File Offset: 0x0010CE2C
		protected override void OnMouseLeave(EventArgs e)
		{
			this.HandleMouseLeave();
			base.OnMouseLeave(e);
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x0010EC3B File Offset: 0x0010CE3B
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			if (!this.GetToolStripState(8192))
			{
				this.Grip.MovingToolStrip = false;
			}
			this.ClearLastMouseDownedItem();
			base.OnMouseCaptureChanged(e);
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x0010EC64 File Offset: 0x0010CE64
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			ToolStripItem toolStripItem = this.Grip.MovingToolStrip ? this.Grip : this.GetItemAt(mea.X, mea.Y);
			if (toolStripItem != null)
			{
				if (mea != null)
				{
					Point point = toolStripItem.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripCoords, ToolStripPointType.ToolStripItemCoords);
					mea = new MouseEventArgs(mea.Button, mea.Clicks, point.X, point.Y, mea.Delta);
				}
				toolStripItem.FireEvent(mea, ToolStripItemEventType.MouseUp);
			}
			else
			{
				base.OnMouseUp(mea);
			}
			this.ClearLastMouseDownedItem();
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x0010ECF8 File Offset: 0x0010CEF8
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			Size size = this.largestDisplayedItemSize;
			bool flag = false;
			Rectangle displayRectangle = this.DisplayRectangle;
			using (Region transparentRegion = this.Renderer.GetTransparentRegion(this))
			{
				if (!LayoutUtils.IsZeroWidthOrHeight(size))
				{
					if (transparentRegion != null)
					{
						transparentRegion.Intersect(graphics.Clip);
						graphics.ExcludeClip(transparentRegion);
						flag = true;
					}
					using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(graphics, ApplyGraphicsProperties.Clipping))
					{
						HandleRef handleRef = new HandleRef(this, windowsGraphics.GetHdc());
						HandleRef cachedItemDC = this.ItemHdcInfo.GetCachedItemDC(handleRef, size);
						Graphics graphics2 = Graphics.FromHdcInternal(cachedItemDC.Handle);
						try
						{
							for (int i = 0; i < this.DisplayedItems.Count; i++)
							{
								ToolStripItem toolStripItem = this.DisplayedItems[i];
								if (toolStripItem != null)
								{
									Rectangle clipRectangle = e.ClipRectangle;
									Rectangle bounds = toolStripItem.Bounds;
									if (!this.IsDropDown && toolStripItem.Owner == this)
									{
										clipRectangle.Intersect(displayRectangle);
									}
									clipRectangle.Intersect(bounds);
									if (!LayoutUtils.IsZeroWidthOrHeight(clipRectangle))
									{
										Size size2 = toolStripItem.Size;
										if (!LayoutUtils.AreWidthAndHeightLarger(size, size2))
										{
											this.largestDisplayedItemSize = size2;
											size = size2;
											graphics2.Dispose();
											cachedItemDC = this.ItemHdcInfo.GetCachedItemDC(handleRef, size);
											graphics2 = Graphics.FromHdcInternal(cachedItemDC.Handle);
										}
										clipRectangle.Offset(-bounds.X, -bounds.Y);
										SafeNativeMethods.BitBlt(cachedItemDC, 0, 0, toolStripItem.Size.Width, toolStripItem.Size.Height, handleRef, toolStripItem.Bounds.X, toolStripItem.Bounds.Y, 13369376);
										using (PaintEventArgs paintEventArgs = new PaintEventArgs(graphics2, clipRectangle))
										{
											toolStripItem.FireEvent(paintEventArgs, ToolStripItemEventType.Paint);
										}
										SafeNativeMethods.BitBlt(handleRef, toolStripItem.Bounds.X, toolStripItem.Bounds.Y, toolStripItem.Size.Width, toolStripItem.Size.Height, cachedItemDC, 0, 0, 13369376);
									}
								}
							}
						}
						finally
						{
							if (graphics2 != null)
							{
								graphics2.Dispose();
							}
						}
					}
				}
				this.Renderer.DrawToolStripBorder(new ToolStripRenderEventArgs(graphics, this));
				if (flag)
				{
					graphics.SetClip(transparentRegion, CombineMode.Union);
				}
				this.PaintInsertionMark(graphics);
			}
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x0010EFD4 File Offset: 0x0010D1D4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			using (new LayoutTransaction(this, this, PropertyNames.RightToLeft))
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					this.Items[i].OnParentRightToLeftChanged(e);
				}
				if (this.toolStripOverflowButton != null)
				{
					this.toolStripOverflowButton.OnParentRightToLeftChanged(e);
				}
				if (this.toolStripGrip != null)
				{
					this.toolStripGrip.OnParentRightToLeftChanged(e);
				}
			}
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x0010F064 File Offset: 0x0010D264
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			Graphics graphics = e.Graphics;
			GraphicsState graphicsState = graphics.Save();
			try
			{
				using (Region transparentRegion = this.Renderer.GetTransparentRegion(this))
				{
					if (transparentRegion != null)
					{
						this.EraseCorners(e, transparentRegion);
						graphics.ExcludeClip(transparentRegion);
					}
				}
				this.Renderer.DrawToolStripBackground(new ToolStripRenderEventArgs(graphics, this));
			}
			finally
			{
				if (graphicsState != null)
				{
					graphics.Restore(graphicsState);
				}
			}
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x0010F0EC File Offset: 0x0010D2EC
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (!base.Disposing && !base.IsDisposed)
			{
				this.HookStaticEvents(base.Visible);
			}
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x0010F111 File Offset: 0x0010D311
		private void EraseCorners(PaintEventArgs e, Region transparentRegion)
		{
			if (transparentRegion != null)
			{
				base.PaintTransparentBackground(e, base.ClientRectangle, transparentRegion);
			}
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x0010F124 File Offset: 0x0010D324
		protected internal virtual void OnPaintGrip(PaintEventArgs e)
		{
			this.Renderer.DrawGrip(new ToolStripGripRenderEventArgs(e.Graphics, this));
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[ToolStrip.EventPaintGrip];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x0010F169 File Offset: 0x0010D369
		protected override void OnScroll(ScrollEventArgs se)
		{
			if (se.Type != ScrollEventType.ThumbTrack && se.NewValue != se.OldValue)
			{
				this.ScrollInternal(se.OldValue - se.NewValue);
			}
			base.OnScroll(se);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x0010F19C File Offset: 0x0010D39C
		private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			UserPreferenceCategory category = e.Category;
			if (category != UserPreferenceCategory.General)
			{
				if (category == UserPreferenceCategory.Window)
				{
					this.OnDefaultFontChanged();
					return;
				}
			}
			else
			{
				this.InvalidateTextItems();
			}
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x0010F1C6 File Offset: 0x0010D3C6
		protected override void OnTabStopChanged(EventArgs e)
		{
			base.SetStyle(ControlStyles.Selectable, this.TabStop);
			base.OnTabStopChanged(e);
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x0010F1E0 File Offset: 0x0010D3E0
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements && deviceDpiOld != deviceDpiNew)
			{
				ToolStripManager.CurrentDpi = deviceDpiNew;
				this.defaultFont = ToolStripManager.DefaultFont;
				this.ResetScaling(deviceDpiNew);
				if (this.toolStripGrip != null)
				{
					this.toolStripGrip.ToolStrip_RescaleConstants(deviceDpiOld, deviceDpiNew);
				}
				Action<int, int> action = this.rescaleConstsCallbackDelegate;
				if (action == null)
				{
					return;
				}
				action(deviceDpiOld, deviceDpiNew);
			}
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x0010F240 File Offset: 0x0010D440
		internal virtual void ResetScaling(int newDpi)
		{
			ToolStrip.iconWidth = DpiHelper.LogicalToDeviceUnits(16, newDpi);
			ToolStrip.iconHeight = DpiHelper.LogicalToDeviceUnits(16, newDpi);
			ToolStrip.insertionBeamWidth = DpiHelper.LogicalToDeviceUnits(6, newDpi);
			this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStrip.defaultPadding, newDpi);
			this.scaledDefaultGripMargin = DpiHelper.LogicalToDeviceUnits(ToolStrip.defaultGripMargin, newDpi);
			this.imageScalingSize = new Size(ToolStrip.iconWidth, ToolStrip.iconHeight);
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x0010F2AC File Offset: 0x0010D4AC
		internal void PaintInsertionMark(Graphics g)
		{
			if (this.lastInsertionMarkRect != Rectangle.Empty)
			{
				int num = ToolStrip.insertionBeamWidth;
				if (this.Orientation == Orientation.Horizontal)
				{
					int x = this.lastInsertionMarkRect.X;
					int num2 = x + 2;
					g.DrawLines(SystemPens.ControlText, new Point[]
					{
						new Point(num2, this.lastInsertionMarkRect.Y),
						new Point(num2, this.lastInsertionMarkRect.Bottom - 1),
						new Point(num2 + 1, this.lastInsertionMarkRect.Y),
						new Point(num2 + 1, this.lastInsertionMarkRect.Bottom - 1)
					});
					g.DrawLines(SystemPens.ControlText, new Point[]
					{
						new Point(x, this.lastInsertionMarkRect.Bottom - 1),
						new Point(x + num - 1, this.lastInsertionMarkRect.Bottom - 1),
						new Point(x + 1, this.lastInsertionMarkRect.Bottom - 2),
						new Point(x + num - 2, this.lastInsertionMarkRect.Bottom - 2)
					});
					g.DrawLines(SystemPens.ControlText, new Point[]
					{
						new Point(x, this.lastInsertionMarkRect.Y),
						new Point(x + num - 1, this.lastInsertionMarkRect.Y),
						new Point(x + 1, this.lastInsertionMarkRect.Y + 1),
						new Point(x + num - 2, this.lastInsertionMarkRect.Y + 1)
					});
					return;
				}
				num = ToolStrip.insertionBeamWidth;
				int y = this.lastInsertionMarkRect.Y;
				int num3 = y + 2;
				g.DrawLines(SystemPens.ControlText, new Point[]
				{
					new Point(this.lastInsertionMarkRect.X, num3),
					new Point(this.lastInsertionMarkRect.Right - 1, num3),
					new Point(this.lastInsertionMarkRect.X, num3 + 1),
					new Point(this.lastInsertionMarkRect.Right - 1, num3 + 1)
				});
				g.DrawLines(SystemPens.ControlText, new Point[]
				{
					new Point(this.lastInsertionMarkRect.X, y),
					new Point(this.lastInsertionMarkRect.X, y + num - 1),
					new Point(this.lastInsertionMarkRect.X + 1, y + 1),
					new Point(this.lastInsertionMarkRect.X + 1, y + num - 2)
				});
				g.DrawLines(SystemPens.ControlText, new Point[]
				{
					new Point(this.lastInsertionMarkRect.Right - 1, y),
					new Point(this.lastInsertionMarkRect.Right - 1, y + num - 1),
					new Point(this.lastInsertionMarkRect.Right - 2, y + 1),
					new Point(this.lastInsertionMarkRect.Right - 2, y + num - 2)
				});
			}
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x0010F601 File Offset: 0x0010D801
		internal void PaintInsertionMark(Rectangle insertionRect)
		{
			if (this.lastInsertionMarkRect != insertionRect)
			{
				this.ClearInsertionMark();
				this.lastInsertionMarkRect = insertionRect;
				base.Invalidate(insertionRect);
			}
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x0010F625 File Offset: 0x0010D825
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Control GetChildAtPoint(Point point)
		{
			return base.GetChildAtPoint(point);
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x0010F62E File Offset: 0x0010D82E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
		{
			return base.GetChildAtPoint(pt, skipValue);
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x00015ECC File Offset: 0x000140CC
		internal override Control GetFirstChildControlInTabOrder(bool forward)
		{
			return null;
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x0010F638 File Offset: 0x0010D838
		public ToolStripItem GetItemAt(int x, int y)
		{
			return this.GetItemAt(new Point(x, y));
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x0010F648 File Offset: 0x0010D848
		public ToolStripItem GetItemAt(Point point)
		{
			Rectangle rect = new Rectangle(point, ToolStrip.onePixel);
			if (this.lastMouseActiveItem != null && this.lastMouseActiveItem.Bounds.IntersectsWith(rect) && this.lastMouseActiveItem.ParentInternal == this)
			{
				return this.lastMouseActiveItem;
			}
			for (int i = 0; i < this.DisplayedItems.Count; i++)
			{
				if (this.DisplayedItems[i] != null && this.DisplayedItems[i].ParentInternal == this)
				{
					Rectangle rect2 = this.DisplayedItems[i].Bounds;
					if (this.toolStripGrip != null && this.DisplayedItems[i] == this.toolStripGrip)
					{
						rect2 = LayoutUtils.InflateRect(rect2, this.GripMargin);
					}
					if (rect2.IntersectsWith(rect))
					{
						return this.DisplayedItems[i];
					}
				}
			}
			return null;
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x0010F723 File Offset: 0x0010D923
		private void RestoreFocusInternal(bool wasInMenuMode)
		{
			if (wasInMenuMode == ToolStripManager.ModalMenuFilter.InMenuMode)
			{
				this.RestoreFocusInternal();
			}
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x0010F734 File Offset: 0x0010D934
		internal void RestoreFocusInternal()
		{
			ToolStripManager.ModalMenuFilter.MenuKeyToggle = false;
			this.ClearAllSelections();
			this.lastMouseDownedItem = null;
			ToolStripManager.ModalMenuFilter.ExitMenuMode();
			if (!this.IsDropDown)
			{
				Application.ThreadContext.FromCurrent().RemoveMessageFilter(this.RestoreFocusFilter);
				this.MenuAutoExpand = false;
				if (!base.DesignMode && !this.TabStop && (this.Focused || base.ContainsFocus))
				{
					this.RestoreFocus();
				}
			}
			if (this.KeyboardActive && !this.Focused && !base.ContainsFocus)
			{
				this.KeyboardActive = false;
			}
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x0010F7C0 File Offset: 0x0010D9C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void RestoreFocus()
		{
			bool flag = false;
			if (this.hwndThatLostFocus != IntPtr.Zero && this.hwndThatLostFocus != base.Handle)
			{
				Control control = Control.FromHandleInternal(this.hwndThatLostFocus);
				this.hwndThatLostFocus = IntPtr.Zero;
				if (control != null && control.Visible)
				{
					flag = control.FocusInternal();
				}
			}
			this.hwndThatLostFocus = IntPtr.Zero;
			if (!flag)
			{
				UnsafeNativeMethods.SetFocus(NativeMethods.NullHandleRef);
			}
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x0010F836 File Offset: 0x0010DA36
		internal virtual void ResetRenderMode()
		{
			this.RenderMode = ToolStripRenderMode.ManagerRenderMode;
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x0010F83F File Offset: 0x0010DA3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetMinimumSize()
		{
			CommonProperties.SetMinimumSize(this, new Size(-1, -1));
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x0010F84E File Offset: 0x0010DA4E
		private void ResetGripMargin()
		{
			this.GripMargin = this.Grip.DefaultMargin;
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x0010F861 File Offset: 0x0010DA61
		internal void ResumeCaputureMode()
		{
			this.SetToolStripState(8192, false);
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x0010F86F File Offset: 0x0010DA6F
		internal void SuspendCaputureMode()
		{
			this.SetToolStripState(8192, true);
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x0010F880 File Offset: 0x0010DA80
		internal virtual void ScrollInternal(int delta)
		{
			base.SuspendLayout();
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				Point location = toolStripItem.Bounds.Location;
				location.Y -= delta;
				this.SetItemLocation(toolStripItem, location);
			}
			base.ResumeLayout(false);
			base.Invalidate();
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x0010F910 File Offset: 0x0010DB10
		protected internal void SetItemLocation(ToolStripItem item, Point location)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item.Owner != this)
			{
				throw new NotSupportedException(SR.GetString("ToolStripCanOnlyPositionItsOwnItems"));
			}
			item.SetBounds(new Rectangle(location, item.Size));
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x0010F94B File Offset: 0x0010DB4B
		protected static void SetItemParent(ToolStripItem item, ToolStrip parent)
		{
			item.Parent = parent;
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x0010F954 File Offset: 0x0010DB54
		protected override void SetVisibleCore(bool visible)
		{
			if (visible)
			{
				this.SnapMouseLocation();
			}
			else
			{
				if (!base.Disposing && !base.IsDisposed)
				{
					this.ClearAllSelections();
				}
				CachedItemHdcInfo cachedItemHdcInfo = this.cachedItemHdcInfo;
				this.cachedItemHdcInfo = null;
				this.lastMouseDownedItem = null;
				if (cachedItemHdcInfo != null)
				{
					cachedItemHdcInfo.Dispose();
				}
			}
			base.SetVisibleCore(visible);
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x0010F9A8 File Offset: 0x0010DBA8
		internal bool ShouldSelectItem()
		{
			if (this.mouseEnterWhenShown == ToolStrip.InvalidMouseEnter)
			{
				return true;
			}
			Point lastCursorPoint = WindowsFormsUtils.LastCursorPoint;
			if (this.mouseEnterWhenShown != lastCursorPoint)
			{
				this.mouseEnterWhenShown = ToolStrip.InvalidMouseEnter;
				return true;
			}
			return false;
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x0010F9EC File Offset: 0x0010DBEC
		protected override void Select(bool directed, bool forward)
		{
			bool flag = true;
			if (this.ParentInternal != null)
			{
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					containerControlInternal.ActiveControl = this;
					flag = (containerControlInternal.ActiveControl == this);
				}
			}
			if (directed && flag)
			{
				this.SelectNextToolStripItem(null, forward);
			}
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x0010FA34 File Offset: 0x0010DC34
		internal ToolStripItem SelectNextToolStripItem(ToolStripItem start, bool forward)
		{
			ToolStripItem nextItem = this.GetNextItem(start, forward ? ArrowDirection.Right : ArrowDirection.Left, true);
			this.ChangeSelection(nextItem);
			return nextItem;
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x0010FA5A File Offset: 0x0010DC5A
		internal void SetFocusUnsafe()
		{
			if (this.TabStop)
			{
				this.FocusInternal();
				return;
			}
			ToolStripManager.ModalMenuFilter.SetActiveToolStrip(this, false);
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x0010FA74 File Offset: 0x0010DC74
		private void SetupGrip()
		{
			Rectangle empty = Rectangle.Empty;
			Rectangle displayRectangle = this.DisplayRectangle;
			if (this.Orientation == Orientation.Horizontal)
			{
				empty.X = Math.Max(0, displayRectangle.X - this.Grip.GripThickness);
				empty.Y = Math.Max(0, displayRectangle.Top - this.Grip.Margin.Top);
				empty.Width = this.Grip.GripThickness;
				empty.Height = displayRectangle.Height;
				if (this.RightToLeft == RightToLeft.Yes)
				{
					empty.X = base.ClientRectangle.Right - empty.Width - this.Grip.Margin.Horizontal;
					empty.X += this.Grip.Margin.Left;
				}
				else
				{
					empty.X -= this.Grip.Margin.Right;
				}
			}
			else
			{
				empty.X = displayRectangle.Left;
				empty.Y = displayRectangle.Top - (this.Grip.GripThickness + this.Grip.Margin.Bottom);
				empty.Width = displayRectangle.Width;
				empty.Height = this.Grip.GripThickness;
			}
			if (this.Grip.Bounds != empty)
			{
				this.Grip.SetBounds(empty);
			}
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x0010FBFC File Offset: 0x0010DDFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void SetAutoScrollMargin(int x, int y)
		{
			base.SetAutoScrollMargin(x, y);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x0010FC08 File Offset: 0x0010DE08
		internal void SetLargestItemSize(Size size)
		{
			if (this.toolStripOverflowButton != null && this.toolStripOverflowButton.Visible)
			{
				size = LayoutUtils.UnionSizes(size, this.toolStripOverflowButton.Bounds.Size);
			}
			if (this.toolStripGrip != null && this.toolStripGrip.Visible)
			{
				size = LayoutUtils.UnionSizes(size, this.toolStripGrip.Bounds.Size);
			}
			this.largestDisplayedItemSize = size;
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x0010FC7C File Offset: 0x0010DE7C
		protected virtual void SetDisplayedItems()
		{
			this.DisplayedItems.Clear();
			this.OverflowItems.Clear();
			this.HasVisibleItems = false;
			Size size = Size.Empty;
			if (this.LayoutEngine is ToolStripSplitStackLayout)
			{
				if (ToolStripGripStyle.Visible == this.GripStyle)
				{
					this.DisplayedItems.Add(this.Grip);
					this.SetupGrip();
				}
				Rectangle displayRectangle = this.DisplayRectangle;
				int num = -1;
				for (int i = 0; i < 2; i++)
				{
					int num2 = 0;
					if (i == 1)
					{
						num2 = num;
					}
					while (num2 >= 0 && num2 < this.Items.Count)
					{
						ToolStripItem toolStripItem = this.Items[num2];
						ToolStripItemPlacement placement = toolStripItem.Placement;
						if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
						{
							if (placement == ToolStripItemPlacement.Main)
							{
								bool flag = false;
								if (i == 0)
								{
									flag = (toolStripItem.Alignment == ToolStripItemAlignment.Left);
									if (!flag)
									{
										num = num2;
									}
								}
								else if (i == 1)
								{
									flag = (toolStripItem.Alignment == ToolStripItemAlignment.Right);
								}
								if (flag)
								{
									this.HasVisibleItems = true;
									size = LayoutUtils.UnionSizes(size, toolStripItem.Bounds.Size);
									this.DisplayedItems.Add(toolStripItem);
								}
							}
							else if (placement == ToolStripItemPlacement.Overflow && !(toolStripItem is ToolStripSeparator))
							{
								if (toolStripItem is ToolStripControlHost && this.OverflowButton.DropDown.IsRestrictedWindow)
								{
									toolStripItem.SetPlacement(ToolStripItemPlacement.None);
								}
								else
								{
									this.OverflowItems.Add(toolStripItem);
								}
							}
						}
						else
						{
							toolStripItem.SetPlacement(ToolStripItemPlacement.None);
						}
						num2 = ((i == 0) ? (num2 + 1) : (num2 - 1));
					}
				}
				ToolStripOverflow overflow = this.GetOverflow();
				if (overflow != null)
				{
					overflow.LayoutRequired = true;
				}
				if (this.OverflowItems.Count == 0)
				{
					this.OverflowButton.Visible = false;
				}
				else if (this.CanOverflow)
				{
					this.DisplayedItems.Add(this.OverflowButton);
				}
			}
			else
			{
				Rectangle clientRectangle = base.ClientRectangle;
				bool allItemsVisible = true;
				for (int j = 0; j < this.Items.Count; j++)
				{
					ToolStripItem toolStripItem2 = this.Items[j];
					if (((IArrangedElement)toolStripItem2).ParticipatesInLayout)
					{
						toolStripItem2.ParentInternal = this;
						bool flag2 = !this.IsDropDown;
						bool flag3 = toolStripItem2.Bounds.IntersectsWith(clientRectangle);
						if (!clientRectangle.Contains(clientRectangle.X, toolStripItem2.Bounds.Top) || !clientRectangle.Contains(clientRectangle.X, toolStripItem2.Bounds.Bottom))
						{
							allItemsVisible = false;
						}
						if (!flag2 || flag3)
						{
							this.HasVisibleItems = true;
							size = LayoutUtils.UnionSizes(size, toolStripItem2.Bounds.Size);
							this.DisplayedItems.Add(toolStripItem2);
							toolStripItem2.SetPlacement(ToolStripItemPlacement.Main);
						}
					}
					else
					{
						toolStripItem2.SetPlacement(ToolStripItemPlacement.None);
					}
				}
				this.AllItemsVisible = allItemsVisible;
			}
			this.SetLargestItemSize(size);
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x0010FF67 File Offset: 0x0010E167
		internal void SetToolStripState(int flag, bool value)
		{
			this.toolStripState = (value ? (this.toolStripState | flag) : (this.toolStripState & ~flag));
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0010FF85 File Offset: 0x0010E185
		internal void SnapMouseLocation()
		{
			this.mouseEnterWhenShown = WindowsFormsUtils.LastCursorPoint;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x0010FF94 File Offset: 0x0010E194
		private void SnapFocus(IntPtr otherHwnd)
		{
			if (!this.TabStop && !this.IsDropDown)
			{
				bool flag = false;
				if (this.Focused && otherHwnd != base.Handle)
				{
					flag = true;
				}
				else if (!base.ContainsFocus && !this.Focused)
				{
					flag = true;
				}
				if (flag)
				{
					this.SnapMouseLocation();
					HandleRef hWndParent = new HandleRef(this, base.Handle);
					HandleRef hwnd = new HandleRef(null, otherHwnd);
					if (hWndParent.Handle != hwnd.Handle && !UnsafeNativeMethods.IsChild(hWndParent, hwnd))
					{
						HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(this);
						HandleRef rootHWnd2 = WindowsFormsUtils.GetRootHWnd(hwnd);
						if (rootHWnd.Handle == rootHWnd2.Handle && rootHWnd.Handle != IntPtr.Zero)
						{
							this.hwndThatLostFocus = hwnd.Handle;
						}
					}
				}
			}
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x00110067 File Offset: 0x0010E267
		internal void SnapFocusChange(ToolStrip otherToolStrip)
		{
			otherToolStrip.hwndThatLostFocus = this.hwndThatLostFocus;
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x00110075 File Offset: 0x0010E275
		private bool ShouldSerializeDefaultDropDownDirection()
		{
			return this.toolStripDropDownDirection != ToolStripDropDownDirection.Default;
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x00110083 File Offset: 0x0010E283
		internal virtual bool ShouldSerializeLayoutStyle()
		{
			return this.layoutStyle > ToolStripLayoutStyle.StackWithOverflow;
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x00110090 File Offset: 0x0010E290
		internal override bool ShouldSerializeMinimumSize()
		{
			Size size = new Size(-1, -1);
			return CommonProperties.GetMinimumSize(this, size) != size;
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x001100B3 File Offset: 0x0010E2B3
		private bool ShouldSerializeGripMargin()
		{
			return this.GripMargin != this.DefaultGripMargin;
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x001100C6 File Offset: 0x0010E2C6
		internal virtual bool ShouldSerializeRenderMode()
		{
			return this.RenderMode != ToolStripRenderMode.ManagerRenderMode && this.RenderMode > ToolStripRenderMode.Custom;
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x001100DC File Offset: 0x0010E2DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			stringBuilder.Append(", Name: ");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Items: ").Append(this.Items.Count);
			return stringBuilder.ToString();
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x00110130 File Offset: 0x0010E330
		internal void UpdateToolTip(ToolStripItem item)
		{
			if (this.ShowItemToolTips && item != this.currentlyActiveTooltipItem && this.ToolTip != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					this.ToolTip.Hide(this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					this.ToolTip.Active = false;
				}
				this.currentlyActiveTooltipItem = item;
				if (this.currentlyActiveTooltipItem != null && !this.GetToolStripState(2048))
				{
					Cursor currentInternal = Cursor.CurrentInternal;
					if (currentInternal != null)
					{
						if (AccessibilityImprovements.UseLegacyToolTipDisplay)
						{
							this.ToolTip.Active = true;
						}
						Point point = Cursor.Position;
						point.Y += this.Cursor.Size.Height - currentInternal.HotSpot.Y;
						point = WindowsFormsUtils.ConstrainToScreenBounds(new Rectangle(point, ToolStrip.onePixel)).Location;
						IntSecurity.AllWindows.Assert();
						try
						{
							this.ToolTip.Show(this.currentlyActiveTooltipItem.ToolTipText, this, base.PointToClient(point), this.ToolTip.AutoPopDelay);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
			}
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x00110280 File Offset: 0x0010E480
		private void UpdateLayoutStyle(DockStyle newDock)
		{
			if (!this.IsInToolStripPanel && this.layoutStyle != ToolStripLayoutStyle.HorizontalStackWithOverflow && this.layoutStyle != ToolStripLayoutStyle.VerticalStackWithOverflow)
			{
				using (new LayoutTransaction(this, this, PropertyNames.Orientation))
				{
					if (newDock == DockStyle.Left || newDock == DockStyle.Right)
					{
						this.UpdateOrientation(Orientation.Vertical);
					}
					else
					{
						this.UpdateOrientation(Orientation.Horizontal);
					}
				}
				this.OnLayoutStyleChanged(EventArgs.Empty);
				if (this.ParentInternal != null)
				{
					LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Orientation);
				}
			}
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x0011030C File Offset: 0x0010E50C
		private void UpdateLayoutStyle(Orientation newRaftingRowOrientation)
		{
			if (this.layoutStyle != ToolStripLayoutStyle.HorizontalStackWithOverflow && this.layoutStyle != ToolStripLayoutStyle.VerticalStackWithOverflow)
			{
				using (new LayoutTransaction(this, this, PropertyNames.Orientation))
				{
					this.UpdateOrientation(newRaftingRowOrientation);
					if (this.LayoutEngine is ToolStripSplitStackLayout && this.layoutStyle == ToolStripLayoutStyle.StackWithOverflow)
					{
						this.OnLayoutStyleChanged(EventArgs.Empty);
					}
					return;
				}
			}
			this.UpdateOrientation(newRaftingRowOrientation);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x00110384 File Offset: 0x0010E584
		private void UpdateOrientation(Orientation newOrientation)
		{
			if (newOrientation != this.orientation)
			{
				Size size = CommonProperties.GetSpecifiedBounds(this).Size;
				this.orientation = newOrientation;
				this.SetupGrip();
			}
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x001103B8 File Offset: 0x0010E5B8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 7)
			{
				this.SnapFocus(m.WParam);
			}
			if (m.Msg == 33)
			{
				Point point = base.PointToClient(WindowsFormsUtils.LastCursorPoint);
				IntPtr value = UnsafeNativeMethods.ChildWindowFromPointEx(new HandleRef(null, base.Handle), point.X, point.Y, 7);
				if (value == base.Handle)
				{
					this.lastMouseDownedItem = null;
					m.Result = (IntPtr)3;
					if (!this.IsDropDown && !this.IsInDesignMode)
					{
						HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(this);
						if (rootHWnd.Handle != IntPtr.Zero)
						{
							IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
							if (activeWindow != rootHWnd.Handle)
							{
								m.Result = (IntPtr)2;
							}
						}
					}
					return;
				}
				this.SnapFocus(UnsafeNativeMethods.GetFocus());
				if (!this.IsDropDown && !this.TabStop)
				{
					Application.ThreadContext.FromCurrent().AddMessageFilter(this.RestoreFocusFilter);
				}
			}
			base.WndProc(ref m);
			if (m.Msg == 130 && this.dropDownOwnerWindow != null)
			{
				this.dropDownOwnerWindow.DestroyHandle();
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x001104D3 File Offset: 0x0010E6D3
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x001104DB File Offset: 0x0010E6DB
		void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			this.SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x0003BB1D File Offset: 0x00039D1D
		bool IArrangedElement.ParticipatesInLayout
		{
			get
			{
				return base.GetState(2);
			}
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00110500 File Offset: 0x0010E700
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStrip.ToolStripAccessibleObject(this);
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x00110508 File Offset: 0x0010E708
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new WindowsFormsUtils.ReadOnlyControlCollection(this, !base.DesignMode);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x00110519 File Offset: 0x0010E719
		internal void OnItemAddedInternal(ToolStripItem item)
		{
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay && this.ShowItemToolTips)
			{
				KeyboardToolTipStateMachine.Instance.Hook(item, this.ToolTip);
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x0011053B File Offset: 0x0010E73B
		internal void OnItemRemovedInternal(ToolStripItem item)
		{
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
			{
				KeyboardToolTipStateMachine.Instance.Unhook(item, this.ToolTip);
			}
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x00110555 File Offset: 0x0010E755
		internal override bool AllowsChildrenToShowToolTips()
		{
			return base.AllowsChildrenToShowToolTips() && this.ShowItemToolTips;
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x00110568 File Offset: 0x0010E768
		internal override bool ShowsOwnKeyboardToolTip()
		{
			bool flag = false;
			int count = this.Items.Count;
			while (count-- != 0 && !flag)
			{
				ToolStripItem toolStripItem = this.Items[count];
				if (toolStripItem.CanKeyboardSelect && toolStripItem.Visible)
				{
					flag = true;
				}
			}
			return !flag;
		}

		// Token: 0x04002437 RID: 9271
		private static Size onePixel = new Size(1, 1);

		// Token: 0x04002438 RID: 9272
		internal static Point InvalidMouseEnter = new Point(int.MaxValue, int.MaxValue);

		// Token: 0x04002439 RID: 9273
		private ToolStripItemCollection toolStripItemCollection;

		// Token: 0x0400243A RID: 9274
		private ToolStripOverflowButton toolStripOverflowButton;

		// Token: 0x0400243B RID: 9275
		private ToolStripGrip toolStripGrip;

		// Token: 0x0400243C RID: 9276
		private ToolStripItemCollection displayedItems;

		// Token: 0x0400243D RID: 9277
		private ToolStripItemCollection overflowItems;

		// Token: 0x0400243E RID: 9278
		private ToolStripDropTargetManager dropTargetManager;

		// Token: 0x0400243F RID: 9279
		private IntPtr hwndThatLostFocus = IntPtr.Zero;

		// Token: 0x04002440 RID: 9280
		private ToolStripItem lastMouseActiveItem;

		// Token: 0x04002441 RID: 9281
		private ToolStripItem lastMouseDownedItem;

		// Token: 0x04002442 RID: 9282
		private LayoutEngine layoutEngine;

		// Token: 0x04002443 RID: 9283
		private ToolStripLayoutStyle layoutStyle;

		// Token: 0x04002444 RID: 9284
		private LayoutSettings layoutSettings;

		// Token: 0x04002445 RID: 9285
		private Rectangle lastInsertionMarkRect = Rectangle.Empty;

		// Token: 0x04002446 RID: 9286
		private ImageList imageList;

		// Token: 0x04002447 RID: 9287
		private ToolStripGripStyle toolStripGripStyle = ToolStripGripStyle.Visible;

		// Token: 0x04002448 RID: 9288
		private ISupportOleDropSource itemReorderDropSource;

		// Token: 0x04002449 RID: 9289
		private IDropTarget itemReorderDropTarget;

		// Token: 0x0400244A RID: 9290
		private int toolStripState;

		// Token: 0x0400244B RID: 9291
		private bool showItemToolTips;

		// Token: 0x0400244C RID: 9292
		private MouseHoverTimer mouseHoverTimer;

		// Token: 0x0400244D RID: 9293
		private ToolStripItem currentlyActiveTooltipItem;

		// Token: 0x0400244E RID: 9294
		private NativeWindow dropDownOwnerWindow;

		// Token: 0x0400244F RID: 9295
		private byte mouseDownID;

		// Token: 0x04002450 RID: 9296
		private Orientation orientation;

		// Token: 0x04002451 RID: 9297
		private ArrayList activeDropDowns = new ArrayList(1);

		// Token: 0x04002452 RID: 9298
		private ToolStripRenderer renderer;

		// Token: 0x04002453 RID: 9299
		private Type currentRendererType = typeof(Type);

		// Token: 0x04002454 RID: 9300
		private Hashtable shortcuts;

		// Token: 0x04002455 RID: 9301
		private Stack<MergeHistory> mergeHistoryStack;

		// Token: 0x04002456 RID: 9302
		private ToolStripDropDownDirection toolStripDropDownDirection = ToolStripDropDownDirection.Default;

		// Token: 0x04002457 RID: 9303
		private Size largestDisplayedItemSize = Size.Empty;

		// Token: 0x04002458 RID: 9304
		private CachedItemHdcInfo cachedItemHdcInfo;

		// Token: 0x04002459 RID: 9305
		private bool alreadyHooked;

		// Token: 0x0400245A RID: 9306
		private Size imageScalingSize;

		// Token: 0x0400245B RID: 9307
		private const int ICON_DIMENSION = 16;

		// Token: 0x0400245C RID: 9308
		private static int iconWidth = 16;

		// Token: 0x0400245D RID: 9309
		private static int iconHeight = 16;

		// Token: 0x0400245E RID: 9310
		private Font defaultFont;

		// Token: 0x0400245F RID: 9311
		private ToolStrip.RestoreFocusMessageFilter restoreFocusFilter;

		// Token: 0x04002460 RID: 9312
		private bool layoutRequired;

		// Token: 0x04002461 RID: 9313
		private static readonly Padding defaultPadding = new Padding(0, 0, 1, 0);

		// Token: 0x04002462 RID: 9314
		private static readonly Padding defaultGripMargin = new Padding(2);

		// Token: 0x04002463 RID: 9315
		private Padding scaledDefaultPadding = ToolStrip.defaultPadding;

		// Token: 0x04002464 RID: 9316
		private Padding scaledDefaultGripMargin = ToolStrip.defaultGripMargin;

		// Token: 0x04002465 RID: 9317
		private Point mouseEnterWhenShown = ToolStrip.InvalidMouseEnter;

		// Token: 0x04002466 RID: 9318
		private const int INSERTION_BEAM_WIDTH = 6;

		// Token: 0x04002467 RID: 9319
		internal static int insertionBeamWidth = 6;

		// Token: 0x04002468 RID: 9320
		private static readonly object EventPaintGrip = new object();

		// Token: 0x04002469 RID: 9321
		private static readonly object EventLayoutCompleted = new object();

		// Token: 0x0400246A RID: 9322
		private static readonly object EventItemAdded = new object();

		// Token: 0x0400246B RID: 9323
		private static readonly object EventItemRemoved = new object();

		// Token: 0x0400246C RID: 9324
		private static readonly object EventLayoutStyleChanged = new object();

		// Token: 0x0400246D RID: 9325
		private static readonly object EventRendererChanged = new object();

		// Token: 0x0400246E RID: 9326
		private static readonly object EventItemClicked = new object();

		// Token: 0x0400246F RID: 9327
		private static readonly object EventLocationChanging = new object();

		// Token: 0x04002470 RID: 9328
		private static readonly object EventBeginDrag = new object();

		// Token: 0x04002471 RID: 9329
		private static readonly object EventEndDrag = new object();

		// Token: 0x04002472 RID: 9330
		private static readonly int PropBindingContext = PropertyStore.CreateKey();

		// Token: 0x04002473 RID: 9331
		private static readonly int PropTextDirection = PropertyStore.CreateKey();

		// Token: 0x04002474 RID: 9332
		private static readonly int PropToolTip = PropertyStore.CreateKey();

		// Token: 0x04002475 RID: 9333
		private static readonly int PropToolStripPanelCell = PropertyStore.CreateKey();

		// Token: 0x04002476 RID: 9334
		internal const int STATE_CANOVERFLOW = 1;

		// Token: 0x04002477 RID: 9335
		internal const int STATE_ALLOWITEMREORDER = 2;

		// Token: 0x04002478 RID: 9336
		internal const int STATE_DISPOSINGITEMS = 4;

		// Token: 0x04002479 RID: 9337
		internal const int STATE_MENUAUTOEXPAND = 8;

		// Token: 0x0400247A RID: 9338
		internal const int STATE_MENUAUTOEXPANDDEFAULT = 16;

		// Token: 0x0400247B RID: 9339
		internal const int STATE_SCROLLBUTTONS = 32;

		// Token: 0x0400247C RID: 9340
		internal const int STATE_USEDEFAULTRENDERER = 64;

		// Token: 0x0400247D RID: 9341
		internal const int STATE_ALLOWMERGE = 128;

		// Token: 0x0400247E RID: 9342
		internal const int STATE_RAFTING = 256;

		// Token: 0x0400247F RID: 9343
		internal const int STATE_STRETCH = 512;

		// Token: 0x04002480 RID: 9344
		internal const int STATE_LOCATIONCHANGING = 1024;

		// Token: 0x04002481 RID: 9345
		internal const int STATE_DRAGGING = 2048;

		// Token: 0x04002482 RID: 9346
		internal const int STATE_HASVISIBLEITEMS = 4096;

		// Token: 0x04002483 RID: 9347
		internal const int STATE_SUSPENDCAPTURE = 8192;

		// Token: 0x04002484 RID: 9348
		internal const int STATE_LASTMOUSEDOWNEDITEMCAPTURE = 16384;

		// Token: 0x04002485 RID: 9349
		internal const int STATE_MENUACTIVE = 32768;

		// Token: 0x04002486 RID: 9350
		internal static readonly TraceSwitch SelectionDebug;

		// Token: 0x04002487 RID: 9351
		internal static readonly TraceSwitch DropTargetDebug;

		// Token: 0x04002488 RID: 9352
		internal static readonly TraceSwitch LayoutDebugSwitch;

		// Token: 0x04002489 RID: 9353
		internal static readonly TraceSwitch MouseActivateDebug;

		// Token: 0x0400248A RID: 9354
		internal static readonly TraceSwitch MergeDebug;

		// Token: 0x0400248B RID: 9355
		internal static readonly TraceSwitch SnapFocusDebug;

		// Token: 0x0400248C RID: 9356
		internal static readonly TraceSwitch FlickerDebug;

		// Token: 0x0400248D RID: 9357
		internal static readonly TraceSwitch ItemReorderDebug;

		// Token: 0x0400248E RID: 9358
		internal static readonly TraceSwitch MDIMergeDebug;

		// Token: 0x0400248F RID: 9359
		internal static readonly TraceSwitch MenuAutoExpandDebug;

		// Token: 0x04002490 RID: 9360
		internal static readonly TraceSwitch ControlTabDebug;

		// Token: 0x04002491 RID: 9361
		internal Action<int, int> rescaleConstsCallbackDelegate;

		// Token: 0x020007F7 RID: 2039
		// (Invoke) Token: 0x06006EA8 RID: 28328
		private delegate void BooleanMethodInvoker(bool arg);

		// Token: 0x020007F8 RID: 2040
		[ComVisible(true)]
		public class ToolStripAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006EAB RID: 28331 RVA: 0x00195A06 File Offset: 0x00193C06
			public ToolStripAccessibleObject(ToolStrip owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006EAC RID: 28332 RVA: 0x00195A18 File Offset: 0x00193C18
			internal override void ClearOwnerControlInternal()
			{
				ToolStrip toolStrip = this.owner;
				if (toolStrip != null)
				{
					ToolStripOverflowButton toolStripOverflowButton = toolStrip.toolStripOverflowButton;
					if (toolStripOverflowButton != null)
					{
						toolStripOverflowButton.ClearAccessibilityObjectOwner();
					}
				}
				ToolStrip toolStrip2 = this.owner;
				ToolStripItemCollection toolStripItemCollection = (toolStrip2 != null) ? toolStrip2.Items : null;
				if (toolStripItemCollection != null)
				{
					for (int i = 0; i < toolStripItemCollection.Count; i++)
					{
						ToolStripItem toolStripItem = toolStripItemCollection[i];
						if (toolStripItem != null)
						{
							toolStripItem.ClearAccessibilityObjectOwner();
						}
					}
				}
				this.owner = null;
				base.ClearOwnerControlInternal();
			}

			// Token: 0x06006EAD RID: 28333 RVA: 0x00195A88 File Offset: 0x00193C88
			public override AccessibleObject HitTest(int x, int y)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				Point point = this.owner.PointToClient(new Point(x, y));
				ToolStripItem itemAt = this.owner.GetItemAt(point);
				if (itemAt == null || itemAt.AccessibilityObject == null)
				{
					return base.HitTest(x, y);
				}
				return itemAt.AccessibilityObject;
			}

			// Token: 0x06006EAE RID: 28334 RVA: 0x00195ADC File Offset: 0x00193CDC
			public override AccessibleObject GetChild(int index)
			{
				if (this.owner == null || this.owner.Items == null)
				{
					return null;
				}
				if (index == 0 && this.owner.Grip.Visible)
				{
					return this.owner.Grip.AccessibilityObject;
				}
				if (this.owner.Grip.Visible && index > 0)
				{
					index--;
				}
				if (index < this.owner.Items.Count)
				{
					ToolStripItem toolStripItem = null;
					int num = 0;
					for (int i = 0; i < this.owner.Items.Count; i++)
					{
						if (this.owner.Items[i].Available && this.owner.Items[i].Alignment == ToolStripItemAlignment.Left)
						{
							if (num == index)
							{
								toolStripItem = this.owner.Items[i];
								break;
							}
							num++;
						}
					}
					if (toolStripItem == null)
					{
						for (int j = 0; j < this.owner.Items.Count; j++)
						{
							if (this.owner.Items[j].Available && this.owner.Items[j].Alignment == ToolStripItemAlignment.Right)
							{
								if (num == index)
								{
									toolStripItem = this.owner.Items[j];
									break;
								}
								num++;
							}
						}
					}
					if (toolStripItem == null)
					{
						return null;
					}
					if (toolStripItem.Placement == ToolStripItemPlacement.Overflow)
					{
						return new ToolStrip.ToolStripAccessibleObjectWrapperForItemsOnOverflow(toolStripItem);
					}
					return toolStripItem.AccessibilityObject;
				}
				else
				{
					if (this.owner.CanOverflow && this.owner.OverflowButton.Visible && index == this.owner.Items.Count)
					{
						return this.owner.OverflowButton.AccessibilityObject;
					}
					return null;
				}
			}

			// Token: 0x06006EAF RID: 28335 RVA: 0x00195C90 File Offset: 0x00193E90
			public override int GetChildCount()
			{
				if (this.owner == null || this.owner.Items == null)
				{
					return -1;
				}
				int num = 0;
				for (int i = 0; i < this.owner.Items.Count; i++)
				{
					if (this.owner.Items[i].Available)
					{
						num++;
					}
				}
				if (this.owner.Grip.Visible)
				{
					num++;
				}
				if (this.owner.CanOverflow && this.owner.OverflowButton.Visible)
				{
					num++;
				}
				return num;
			}

			// Token: 0x06006EB0 RID: 28336 RVA: 0x00195D28 File Offset: 0x00193F28
			internal AccessibleObject GetChildFragment(int fragmentIndex, bool getOverflowItem = false, UnsafeNativeMethods.NavigateDirection direction = UnsafeNativeMethods.NavigateDirection.NextSibling)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				ToolStripItemCollection toolStripItemCollection = getOverflowItem ? this.owner.OverflowItems : this.owner.DisplayedItems;
				int count = toolStripItemCollection.Count;
				if (!getOverflowItem && this.owner.CanOverflow && this.owner.OverflowButton.Visible && fragmentIndex == count - 1)
				{
					return this.owner.OverflowButton.AccessibilityObject;
				}
				int i = 0;
				while (i < count)
				{
					ToolStripItem toolStripItem = toolStripItemCollection[i];
					if (toolStripItem.Available && (AccessibilityImprovements.Level5 || toolStripItem.Alignment == ToolStripItemAlignment.Left) && fragmentIndex == i)
					{
						ToolStripControlHost toolStripControlHost = toolStripItem as ToolStripControlHost;
						if (toolStripControlHost == null)
						{
							return toolStripItem.AccessibilityObject;
						}
						Control control = toolStripControlHost.Control;
						if (this.IsItemEmptyLabel(toolStripItem) || control == null || !control.IsHandleCreated || !control.SupportsUiaProviders)
						{
							return this.GetFollowingChildFragment(fragmentIndex, toolStripItemCollection, direction);
						}
						return toolStripControlHost.ControlAccessibilityObject;
					}
					else
					{
						i++;
					}
				}
				int j = 0;
				while (j < count)
				{
					ToolStripItem toolStripItem2 = this.owner.Items[j];
					if (toolStripItem2.Available && toolStripItem2.Alignment == ToolStripItemAlignment.Right && fragmentIndex == j)
					{
						ToolStripControlHost toolStripControlHost2 = toolStripItem2 as ToolStripControlHost;
						if (toolStripControlHost2 == null)
						{
							return toolStripItem2.AccessibilityObject;
						}
						Control control2 = toolStripControlHost2.Control;
						if (this.IsItemEmptyLabel(toolStripItem2) || control2 == null || !control2.IsHandleCreated || !control2.SupportsUiaProviders)
						{
							return this.GetFollowingChildFragment(fragmentIndex, toolStripItemCollection, direction);
						}
						return toolStripControlHost2.ControlAccessibilityObject;
					}
					else
					{
						j++;
					}
				}
				return null;
			}

			// Token: 0x06006EB1 RID: 28337 RVA: 0x00195EAA File Offset: 0x001940AA
			internal int GetChildOverflowFragmentCount()
			{
				if (this.owner == null || this.owner.OverflowItems == null)
				{
					return -1;
				}
				return this.owner.OverflowItems.Count;
			}

			// Token: 0x06006EB2 RID: 28338 RVA: 0x00195ED4 File Offset: 0x001940D4
			private AccessibleObject GetFollowingChildFragment(int index, ToolStripItemCollection items, UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
					for (int i = index + 1; i < items.Count; i++)
					{
						ToolStripItem toolStripItem = items[i];
						if (!this.IsItemNativeControl(toolStripItem) && !this.IsItemEmptyLabel(toolStripItem))
						{
							ToolStripControlHost toolStripControlHost = toolStripItem as ToolStripControlHost;
							if (toolStripControlHost == null)
							{
								return toolStripItem.AccessibilityObject;
							}
							Control control = toolStripControlHost.Control;
							if (control != null && control.IsHandleCreated)
							{
								return toolStripControlHost.ControlAccessibilityObject;
							}
						}
					}
					break;
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				case UnsafeNativeMethods.NavigateDirection.LastChild:
					for (int j = index - 1; j >= 0; j--)
					{
						ToolStripItem toolStripItem2 = items[j];
						if (!this.IsItemNativeControl(toolStripItem2) && !this.IsItemEmptyLabel(toolStripItem2))
						{
							ToolStripControlHost toolStripControlHost2 = toolStripItem2 as ToolStripControlHost;
							if (toolStripControlHost2 == null)
							{
								return toolStripItem2.AccessibilityObject;
							}
							Control control2 = toolStripControlHost2.Control;
							if (control2 != null && control2.IsHandleCreated)
							{
								return toolStripControlHost2.ControlAccessibilityObject;
							}
						}
					}
					break;
				}
				return null;
			}

			// Token: 0x06006EB3 RID: 28339 RVA: 0x00195FBD File Offset: 0x001941BD
			internal int GetChildFragmentCount()
			{
				if (this.owner == null || this.owner.DisplayedItems == null)
				{
					return -1;
				}
				return this.owner.DisplayedItems.Count;
			}

			// Token: 0x06006EB4 RID: 28340 RVA: 0x00195FE8 File Offset: 0x001941E8
			internal int GetChildFragmentIndex(ToolStripItem.ToolStripItemAccessibleObject child)
			{
				if (child.IsOwnerItemCleared() || this.owner == null || this.owner.Items == null)
				{
					return -1;
				}
				if (child.Owner == this.owner.Grip)
				{
					return 0;
				}
				ToolStripItemPlacement placement = child.Owner.Placement;
				ToolStripItemCollection toolStripItemCollection;
				if (this.owner is ToolStripOverflow)
				{
					toolStripItemCollection = this.owner.DisplayedItems;
				}
				else
				{
					if (this.owner.CanOverflow && this.owner.OverflowButton.Visible && child.Owner == this.owner.OverflowButton)
					{
						return this.GetChildFragmentCount() - 1;
					}
					toolStripItemCollection = ((placement == ToolStripItemPlacement.Main) ? this.owner.DisplayedItems : this.owner.OverflowItems);
				}
				for (int i = 0; i < toolStripItemCollection.Count; i++)
				{
					ToolStripItem toolStripItem = toolStripItemCollection[i];
					if (toolStripItem.Available && toolStripItem.Alignment == ToolStripItemAlignment.Left && child.Owner == toolStripItemCollection[i])
					{
						return i;
					}
				}
				for (int j = 0; j < toolStripItemCollection.Count; j++)
				{
					ToolStripItem toolStripItem2 = toolStripItemCollection[j];
					if (toolStripItem2.Available && toolStripItem2.Alignment == ToolStripItemAlignment.Right && child.Owner == toolStripItemCollection[j])
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06006EB5 RID: 28341 RVA: 0x00196128 File Offset: 0x00194328
			internal int GetChildIndex(ToolStripItem.ToolStripItemAccessibleObject child)
			{
				if (child.IsOwnerItemCleared() || this.owner == null || this.owner.Items == null)
				{
					return -1;
				}
				int num = 0;
				if (this.owner.Grip.Visible)
				{
					if (child.Owner == this.owner.Grip)
					{
						return 0;
					}
					num = 1;
				}
				if (this.owner.CanOverflow && this.owner.OverflowButton.Visible && child.Owner == this.owner.OverflowButton)
				{
					return this.owner.Items.Count + num;
				}
				for (int i = 0; i < this.owner.Items.Count; i++)
				{
					if (this.owner.Items[i].Available && this.owner.Items[i].Alignment == ToolStripItemAlignment.Left)
					{
						if (child.Owner == this.owner.Items[i])
						{
							return num;
						}
						num++;
					}
				}
				for (int j = 0; j < this.owner.Items.Count; j++)
				{
					if (this.owner.Items[j].Available && this.owner.Items[j].Alignment == ToolStripItemAlignment.Right)
					{
						if (child.Owner == this.owner.Items[j])
						{
							return num;
						}
						num++;
					}
				}
				return -1;
			}

			// Token: 0x17001828 RID: 6184
			// (get) Token: 0x06006EB6 RID: 28342 RVA: 0x0019629C File Offset: 0x0019449C
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.ToolBar;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.ToolBar;
				}
			}

			// Token: 0x17001829 RID: 6185
			// (get) Token: 0x06006EB7 RID: 28343 RVA: 0x001962C8 File Offset: 0x001944C8
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return null;
					}
					ToolStripControlHost toolStripControlHost = base.Owner.ToolStripControlHost;
					ToolStrip toolStrip = (toolStripControlHost != null) ? toolStripControlHost.Owner : null;
					if (toolStrip != null && toolStrip.IsHandleCreated)
					{
						return toolStrip.AccessibilityObject;
					}
					return this;
				}
			}

			// Token: 0x06006EB8 RID: 28344 RVA: 0x0019630C File Offset: 0x0019450C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (AccessibilityImprovements.Level3)
				{
					if (direction != UnsafeNativeMethods.NavigateDirection.FirstChild)
					{
						if (direction == UnsafeNativeMethods.NavigateDirection.LastChild)
						{
							int childFragmentCount = this.GetChildFragmentCount();
							if (childFragmentCount > 0)
							{
								return this.GetChildFragment(childFragmentCount - 1, false, direction);
							}
						}
					}
					else
					{
						int childFragmentCount = this.GetChildFragmentCount();
						if (childFragmentCount > 0)
						{
							return this.GetChildFragment(0, false, direction);
						}
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x06006EB9 RID: 28345 RVA: 0x0019635D File Offset: 0x0019455D
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3)
				{
					if (propertyID == 30003)
					{
						return 50021;
					}
					if (propertyID == 30005)
					{
						return this.Name;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006EBA RID: 28346 RVA: 0x001841CC File Offset: 0x001823CC
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerControlDestroyed() && ((AccessibilityImprovements.Level3 && patternId == 10018) || base.IsPatternSupported(patternId));
			}

			// Token: 0x06006EBB RID: 28347 RVA: 0x0019638F File Offset: 0x0019458F
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerControlDestroyed() && (AccessibilityImprovements.Level3 && !this.owner.IsInDesignMode) && !this.owner.IsTopInDesignMode;
			}

			// Token: 0x06006EBC RID: 28348 RVA: 0x001963C0 File Offset: 0x001945C0
			private bool IsItemNativeControl(ToolStripItem item)
			{
				ToolStripControlHost toolStripControlHost = item as ToolStripControlHost;
				if (toolStripControlHost != null)
				{
					Control control = toolStripControlHost.Control;
					return control != null && !control.SupportsUiaProviders;
				}
				return false;
			}

			// Token: 0x06006EBD RID: 28349 RVA: 0x001963F0 File Offset: 0x001945F0
			private bool IsItemEmptyLabel(ToolStripItem item)
			{
				ToolStripControlHost toolStripControlHost = item as ToolStripControlHost;
				Label label = ((toolStripControlHost != null) ? toolStripControlHost.Control : null) as Label;
				return label != null && string.IsNullOrEmpty(label.Text);
			}

			// Token: 0x040042EC RID: 17132
			private ToolStrip owner;
		}

		// Token: 0x020007F9 RID: 2041
		private class ToolStripAccessibleObjectWrapperForItemsOnOverflow : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006EBE RID: 28350 RVA: 0x00166C85 File Offset: 0x00164E85
			public ToolStripAccessibleObjectWrapperForItemsOnOverflow(ToolStripItem item) : base(item)
			{
			}

			// Token: 0x1700182A RID: 6186
			// (get) Token: 0x06006EBF RID: 28351 RVA: 0x00196428 File Offset: 0x00194628
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = base.State;
					accessibleStates |= AccessibleStates.Offscreen;
					return accessibleStates | AccessibleStates.Invisible;
				}
			}
		}

		// Token: 0x020007FA RID: 2042
		internal class RestoreFocusMessageFilter : IMessageFilter
		{
			// Token: 0x06006EC0 RID: 28352 RVA: 0x0019644D File Offset: 0x0019464D
			public RestoreFocusMessageFilter(ToolStrip ownerToolStrip)
			{
				this.ownerToolStrip = ownerToolStrip;
			}

			// Token: 0x06006EC1 RID: 28353 RVA: 0x0019645C File Offset: 0x0019465C
			public bool PreFilterMessage(ref Message m)
			{
				if (this.ownerToolStrip.Disposing || this.ownerToolStrip.IsDisposed || this.ownerToolStrip.IsDropDown)
				{
					return false;
				}
				int msg = m.Msg;
				if (msg <= 167)
				{
					if (msg != 161 && msg != 164 && msg != 167)
					{
						return false;
					}
				}
				else if (msg != 513 && msg != 516 && msg != 519)
				{
					return false;
				}
				if (this.ownerToolStrip.ContainsFocus && !UnsafeNativeMethods.IsChild(new HandleRef(this, this.ownerToolStrip.Handle), new HandleRef(this, m.HWnd)))
				{
					HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(this.ownerToolStrip);
					if (rootHWnd.Handle == m.HWnd || UnsafeNativeMethods.IsChild(rootHWnd, new HandleRef(this, m.HWnd)))
					{
						this.RestoreFocusInternal();
					}
				}
				return false;
			}

			// Token: 0x06006EC2 RID: 28354 RVA: 0x00196544 File Offset: 0x00194744
			private void RestoreFocusInternal()
			{
				this.ownerToolStrip.BeginInvoke(new ToolStrip.BooleanMethodInvoker(this.ownerToolStrip.RestoreFocusInternal), new object[]
				{
					ToolStripManager.ModalMenuFilter.InMenuMode
				});
				Application.ThreadContext.FromCurrent().RemoveMessageFilter(this);
			}

			// Token: 0x040042ED RID: 17133
			private ToolStrip ownerToolStrip;
		}
	}
}
