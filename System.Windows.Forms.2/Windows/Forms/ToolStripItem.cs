using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003CB RID: 971
	[DesignTimeVisible(false)]
	[Designer("System.Windows.Forms.Design.ToolStripItemDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("Click")]
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	public abstract class ToolStripItem : Component, IDropTarget, ISupportOleDropSource, IArrangedElement, IComponent, IDisposable, IKeyboardToolTip
	{
		// Token: 0x0600419C RID: 16796 RVA: 0x00118C74 File Offset: 0x00116E74
		protected ToolStripItem()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripItem.defaultMargin, 0);
				this.scaledDefaultStatusStripMargin = DpiHelper.LogicalToDeviceUnits(ToolStripItem.defaultStatusStripMargin, 0);
			}
			this.state[ToolStripItem.stateEnabled | ToolStripItem.stateAutoSize | ToolStripItem.stateVisible | ToolStripItem.stateContstructing | ToolStripItem.stateSupportsItemClick | ToolStripItem.stateInvalidMirroredImage | ToolStripItem.stateMouseDownAndUpMustBeInSameItem | ToolStripItem.stateUseAmbientMargin] = true;
			this.state[ToolStripItem.stateAllowDrop | ToolStripItem.stateMouseDownAndNoDrag | ToolStripItem.stateSupportsRightClick | ToolStripItem.statePressed | ToolStripItem.stateSelected | ToolStripItem.stateDisposed | ToolStripItem.stateDoubleClickEnabled | ToolStripItem.stateRightToLeftAutoMirrorImage | ToolStripItem.stateSupportsSpaceKey] = false;
			this.SetAmbientMargin();
			this.Size = this.DefaultSize;
			this.DisplayStyle = this.DefaultDisplayStyle;
			CommonProperties.SetAutoSize(this, true);
			this.state[ToolStripItem.stateContstructing] = false;
			this.AutoToolTip = this.DefaultAutoToolTip;
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x00118DEE File Offset: 0x00116FEE
		protected ToolStripItem(string text, Image image, EventHandler onClick) : this(text, image, onClick, null)
		{
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x00118DFA File Offset: 0x00116FFA
		protected ToolStripItem(string text, Image image, EventHandler onClick, string name) : this()
		{
			this.Text = text;
			this.Image = image;
			if (onClick != null)
			{
				this.Click += onClick;
			}
			this.Name = name;
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x00118E24 File Offset: 0x00117024
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolStripItemAccessibilityObjectDescr")]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)this.Properties.GetObject(ToolStripItem.PropAccessibility);
				if (accessibleObject == null)
				{
					accessibleObject = this.CreateAccessibilityInstance();
					this.Properties.SetObject(ToolStripItem.PropAccessibility, accessibleObject);
				}
				return accessibleObject;
			}
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x00118E64 File Offset: 0x00117064
		internal virtual void ClearAccessibilityObjectOwner()
		{
			object @object = this.Properties.GetObject(ToolStripItem.PropAccessibility);
			ToolStripItem.ToolStripItemAccessibleObject toolStripItemAccessibleObject = @object as ToolStripItem.ToolStripItemAccessibleObject;
			if (toolStripItemAccessibleObject != null)
			{
				toolStripItemAccessibleObject.ClearOwnerItem();
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x00118E92 File Offset: 0x00117092
		// (set) Token: 0x060041A2 RID: 16802 RVA: 0x00118EA9 File Offset: 0x001170A9
		[SRCategory("CatAccessibility")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolStripItemAccessibleDefaultActionDescr")]
		public string AccessibleDefaultActionDescription
		{
			get
			{
				return (string)this.Properties.GetObject(ToolStripItem.PropAccessibleDefaultActionDescription);
			}
			set
			{
				this.Properties.SetObject(ToolStripItem.PropAccessibleDefaultActionDescription, value);
				this.OnAccessibleDefaultActionDescriptionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x00118EC7 File Offset: 0x001170C7
		// (set) Token: 0x060041A4 RID: 16804 RVA: 0x00118EDE File Offset: 0x001170DE
		[SRCategory("CatAccessibility")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ToolStripItemAccessibleDescriptionDescr")]
		public string AccessibleDescription
		{
			get
			{
				return (string)this.Properties.GetObject(ToolStripItem.PropAccessibleDescription);
			}
			set
			{
				this.Properties.SetObject(ToolStripItem.PropAccessibleDescription, value);
				this.OnAccessibleDescriptionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x00118EFC File Offset: 0x001170FC
		// (set) Token: 0x060041A6 RID: 16806 RVA: 0x00118F13 File Offset: 0x00117113
		[SRCategory("CatAccessibility")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ToolStripItemAccessibleNameDescr")]
		public string AccessibleName
		{
			get
			{
				return (string)this.Properties.GetObject(ToolStripItem.PropAccessibleName);
			}
			set
			{
				this.Properties.SetObject(ToolStripItem.PropAccessibleName, value);
				this.OnAccessibleNameChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x060041A7 RID: 16807 RVA: 0x00118F34 File Offset: 0x00117134
		// (set) Token: 0x060041A8 RID: 16808 RVA: 0x00118F5C File Offset: 0x0011715C
		[SRCategory("CatAccessibility")]
		[DefaultValue(AccessibleRole.Default)]
		[SRDescription("ToolStripItemAccessibleRoleDescr")]
		public AccessibleRole AccessibleRole
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(ToolStripItem.PropAccessibleRole, out flag);
				if (flag)
				{
					return (AccessibleRole)integer;
				}
				return AccessibleRole.Default;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, -1, 64))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AccessibleRole));
				}
				this.Properties.SetInteger(ToolStripItem.PropAccessibleRole, (int)value);
				this.OnAccessibleRoleChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x00118FAC File Offset: 0x001171AC
		// (set) Token: 0x060041AA RID: 16810 RVA: 0x00118FB4 File Offset: 0x001171B4
		[DefaultValue(ToolStripItemAlignment.Left)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripItemAlignmentDescr")]
		public ToolStripItemAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripItemAlignment));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					if (this.ParentInternal != null && this.ParentInternal.IsHandleCreated)
					{
						this.ParentInternal.PerformLayout();
					}
				}
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x00119017 File Offset: 0x00117217
		// (set) Token: 0x060041AC RID: 16812 RVA: 0x00119029 File Offset: 0x00117229
		[SRCategory("CatDragDrop")]
		[DefaultValue(false)]
		[SRDescription("ToolStripItemAllowDropDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public virtual bool AllowDrop
		{
			get
			{
				return this.state[ToolStripItem.stateAllowDrop];
			}
			set
			{
				if (value != this.state[ToolStripItem.stateAllowDrop])
				{
					this.EnsureParentDropTargetRegistered();
					this.state[ToolStripItem.stateAllowDrop] = value;
				}
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x00119055 File Offset: 0x00117255
		// (set) Token: 0x060041AE RID: 16814 RVA: 0x00119067 File Offset: 0x00117267
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.All)]
		[Localizable(true)]
		[SRDescription("ToolStripItemAutoSizeDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool AutoSize
		{
			get
			{
				return this.state[ToolStripItem.stateAutoSize];
			}
			set
			{
				if (this.state[ToolStripItem.stateAutoSize] != value)
				{
					this.state[ToolStripItem.stateAutoSize] = value;
					CommonProperties.SetAutoSize(this, value);
					this.InvalidateItemLayout(PropertyNames.AutoSize);
				}
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x060041AF RID: 16815 RVA: 0x0011909F File Offset: 0x0011729F
		// (set) Token: 0x060041B0 RID: 16816 RVA: 0x001190B1 File Offset: 0x001172B1
		[DefaultValue(false)]
		[SRDescription("ToolStripItemAutoToolTipDescr")]
		[SRCategory("CatBehavior")]
		public bool AutoToolTip
		{
			get
			{
				return this.state[ToolStripItem.stateAutoToolTip];
			}
			set
			{
				this.state[ToolStripItem.stateAutoToolTip] = value;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x001190C4 File Offset: 0x001172C4
		// (set) Token: 0x060041B2 RID: 16818 RVA: 0x001190D6 File Offset: 0x001172D6
		[Browsable(false)]
		[SRDescription("ToolStripItemAvailableDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Available
		{
			get
			{
				return this.state[ToolStripItem.stateVisible];
			}
			set
			{
				this.SetVisibleCore(value);
			}
		}

		// Token: 0x14000338 RID: 824
		// (add) Token: 0x060041B3 RID: 16819 RVA: 0x001190DF File Offset: 0x001172DF
		// (remove) Token: 0x060041B4 RID: 16820 RVA: 0x001190F2 File Offset: 0x001172F2
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnAvailableChangedDescr")]
		public event EventHandler AvailableChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventAvailableChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventAvailableChanged, value);
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x00119105 File Offset: 0x00117305
		// (set) Token: 0x060041B6 RID: 16822 RVA: 0x0011911C File Offset: 0x0011731C
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		[DefaultValue(null)]
		public virtual Image BackgroundImage
		{
			get
			{
				return this.Properties.GetObject(ToolStripItem.PropBackgroundImage) as Image;
			}
			set
			{
				if (this.BackgroundImage != value)
				{
					this.Properties.SetObject(ToolStripItem.PropBackgroundImage, value);
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x0011913E File Offset: 0x0011733E
		// (set) Token: 0x060041B8 RID: 16824 RVA: 0x00119146 File Offset: 0x00117346
		internal virtual int DeviceDpi
		{
			get
			{
				return this.deviceDpi;
			}
			set
			{
				this.deviceDpi = value;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x060041B9 RID: 16825 RVA: 0x00119150 File Offset: 0x00117350
		// (set) Token: 0x060041BA RID: 16826 RVA: 0x00119188 File Offset: 0x00117388
		[SRCategory("CatAppearance")]
		[DefaultValue(ImageLayout.Tile)]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public virtual ImageLayout BackgroundImageLayout
		{
			get
			{
				if (!this.Properties.ContainsObject(ToolStripItem.PropBackgroundImageLayout))
				{
					return ImageLayout.Tile;
				}
				return (ImageLayout)this.Properties.GetObject(ToolStripItem.PropBackgroundImageLayout);
			}
			set
			{
				if (this.BackgroundImageLayout != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ImageLayout));
					}
					this.Properties.SetObject(ToolStripItem.PropBackgroundImageLayout, value);
					this.Invalidate();
				}
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x001191E0 File Offset: 0x001173E0
		// (set) Token: 0x060041BC RID: 16828 RVA: 0x00119218 File Offset: 0x00117418
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemBackColorDescr")]
		public virtual Color BackColor
		{
			get
			{
				Color rawBackColor = this.RawBackColor;
				if (!rawBackColor.IsEmpty)
				{
					return rawBackColor;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					return parentInternal.BackColor;
				}
				return Control.DefaultBackColor;
			}
			set
			{
				Color backColor = this.BackColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(ToolStripItem.PropBackColor))
				{
					this.Properties.SetColor(ToolStripItem.PropBackColor, value);
				}
				if (!backColor.Equals(this.BackColor))
				{
					this.OnBackColorChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000339 RID: 825
		// (add) Token: 0x060041BD RID: 16829 RVA: 0x0011927D File Offset: 0x0011747D
		// (remove) Token: 0x060041BE RID: 16830 RVA: 0x00119290 File Offset: 0x00117490
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnBackColorChangedDescr")]
		public event EventHandler BackColorChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventBackColorChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventBackColorChanged, value);
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x060041BF RID: 16831 RVA: 0x001192A3 File Offset: 0x001174A3
		[Browsable(false)]
		public virtual Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x001192AC File Offset: 0x001174AC
		internal Rectangle ClientBounds
		{
			get
			{
				Rectangle result = this.bounds;
				result.Location = Point.Empty;
				return result;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x001192D0 File Offset: 0x001174D0
		[Browsable(false)]
		public Rectangle ContentRectangle
		{
			get
			{
				Rectangle result = LayoutUtils.InflateRect(this.InternalLayout.ContentRectangle, this.Padding);
				result.Size = LayoutUtils.UnionSizes(Size.Empty, result.Size);
				return result;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x00013062 File Offset: 0x00011262
		[Browsable(false)]
		public virtual bool CanSelect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x060041C3 RID: 16835 RVA: 0x0011930D File Offset: 0x0011750D
		internal virtual bool CanKeyboardSelect
		{
			get
			{
				return this.CanSelect;
			}
		}

		// Token: 0x1400033A RID: 826
		// (add) Token: 0x060041C4 RID: 16836 RVA: 0x00119315 File Offset: 0x00117515
		// (remove) Token: 0x060041C5 RID: 16837 RVA: 0x00119328 File Offset: 0x00117528
		[SRCategory("CatAction")]
		[SRDescription("ToolStripItemOnClickDescr")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventClick, value);
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x0011933B File Offset: 0x0011753B
		// (set) Token: 0x060041C7 RID: 16839 RVA: 0x00119343 File Offset: 0x00117543
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		public AnchorStyles Anchor
		{
			get
			{
				return CommonProperties.xGetAnchor(this);
			}
			set
			{
				if (value != this.Anchor)
				{
					CommonProperties.xSetAnchor(this, value);
					if (this.ParentInternal != null)
					{
						LayoutTransaction.DoLayout(this, this.ParentInternal, PropertyNames.Anchor);
					}
				}
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x0011936E File Offset: 0x0011756E
		// (set) Token: 0x060041C9 RID: 16841 RVA: 0x00119378 File Offset: 0x00117578
		[Browsable(false)]
		[DefaultValue(DockStyle.None)]
		public DockStyle Dock
		{
			get
			{
				return CommonProperties.xGetDock(this);
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 5))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DockStyle));
				}
				if (value != this.Dock)
				{
					CommonProperties.xSetDock(this, value);
					if (this.ParentInternal != null)
					{
						LayoutTransaction.DoLayout(this, this.ParentInternal, PropertyNames.Dock);
					}
				}
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool DefaultAutoToolTip
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060041CB RID: 16843 RVA: 0x001193D4 File Offset: 0x001175D4
		protected internal virtual Padding DefaultMargin
		{
			get
			{
				if (this.Owner != null && this.Owner is StatusStrip)
				{
					return this.scaledDefaultStatusStripMargin;
				}
				return this.scaledDefaultMargin;
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060041CC RID: 16844 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected virtual Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x060041CD RID: 16845 RVA: 0x001193F8 File Offset: 0x001175F8
		protected virtual Size DefaultSize
		{
			get
			{
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return new Size(23, 23);
				}
				return DpiHelper.LogicalToDeviceUnits(new Size(23, 23), this.DeviceDpi);
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x00023D73 File Offset: 0x00021F73
		protected virtual ToolStripItemDisplayStyle DefaultDisplayStyle
		{
			get
			{
				return ToolStripItemDisplayStyle.ImageAndText;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x060041CF RID: 16847 RVA: 0x00013062 File Offset: 0x00011262
		protected internal virtual bool DismissWhenClicked
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x0011941F File Offset: 0x0011761F
		// (set) Token: 0x060041D1 RID: 16849 RVA: 0x00119428 File Offset: 0x00117628
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemDisplayStyleDescr")]
		public virtual ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return this.displayStyle;
			}
			set
			{
				if (this.displayStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripItemDisplayStyle));
					}
					this.displayStyle = value;
					if (!this.state[ToolStripItem.stateContstructing])
					{
						this.InvalidateItemLayout(PropertyNames.DisplayStyle);
						this.OnDisplayStyleChanged(new EventArgs());
					}
				}
			}
		}

		// Token: 0x1400033B RID: 827
		// (add) Token: 0x060041D2 RID: 16850 RVA: 0x00111C9B File Offset: 0x0010FE9B
		// (remove) Token: 0x060041D3 RID: 16851 RVA: 0x00111CAE File Offset: 0x0010FEAE
		public event EventHandler DisplayStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDisplayStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDisplayStyleChanged, value);
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x0001627D File Offset: 0x0001447D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		private RightToLeft DefaultRightToLeft
		{
			get
			{
				return RightToLeft.Inherit;
			}
		}

		// Token: 0x1400033C RID: 828
		// (add) Token: 0x060041D5 RID: 16853 RVA: 0x00119493 File Offset: 0x00117693
		// (remove) Token: 0x060041D6 RID: 16854 RVA: 0x001194A6 File Offset: 0x001176A6
		[SRCategory("CatAction")]
		[SRDescription("ControlOnDoubleClickDescr")]
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDoubleClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDoubleClick, value);
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x060041D7 RID: 16855 RVA: 0x001194B9 File Offset: 0x001176B9
		// (set) Token: 0x060041D8 RID: 16856 RVA: 0x001194CB File Offset: 0x001176CB
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemDoubleClickedEnabledDescr")]
		public bool DoubleClickEnabled
		{
			get
			{
				return this.state[ToolStripItem.stateDoubleClickEnabled];
			}
			set
			{
				this.state[ToolStripItem.stateDoubleClickEnabled] = value;
			}
		}

		// Token: 0x1400033D RID: 829
		// (add) Token: 0x060041D9 RID: 16857 RVA: 0x001194DE File Offset: 0x001176DE
		// (remove) Token: 0x060041DA RID: 16858 RVA: 0x001194F1 File Offset: 0x001176F1
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragDropDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event DragEventHandler DragDrop
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDragDrop, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDragDrop, value);
			}
		}

		// Token: 0x1400033E RID: 830
		// (add) Token: 0x060041DB RID: 16859 RVA: 0x00119504 File Offset: 0x00117704
		// (remove) Token: 0x060041DC RID: 16860 RVA: 0x00119517 File Offset: 0x00117717
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragEnterDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event DragEventHandler DragEnter
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDragEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDragEnter, value);
			}
		}

		// Token: 0x1400033F RID: 831
		// (add) Token: 0x060041DD RID: 16861 RVA: 0x0011952A File Offset: 0x0011772A
		// (remove) Token: 0x060041DE RID: 16862 RVA: 0x0011953D File Offset: 0x0011773D
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragOverDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event DragEventHandler DragOver
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDragOver, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDragOver, value);
			}
		}

		// Token: 0x14000340 RID: 832
		// (add) Token: 0x060041DF RID: 16863 RVA: 0x00119550 File Offset: 0x00117750
		// (remove) Token: 0x060041E0 RID: 16864 RVA: 0x00119563 File Offset: 0x00117763
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragLeaveDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event EventHandler DragLeave
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDragLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDragLeave, value);
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x00119576 File Offset: 0x00117776
		private DropSource DropSource
		{
			get
			{
				if (this.ParentInternal != null && this.ParentInternal.AllowItemReorder && this.ParentInternal.ItemReorderDropSource != null)
				{
					return new DropSource(this.ParentInternal.ItemReorderDropSource);
				}
				return new DropSource(this);
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x060041E2 RID: 16866 RVA: 0x001195B4 File Offset: 0x001177B4
		// (set) Token: 0x060041E3 RID: 16867 RVA: 0x001195EC File Offset: 0x001177EC
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ToolStripItemEnabledDescr")]
		[DefaultValue(true)]
		public virtual bool Enabled
		{
			get
			{
				bool flag = true;
				if (this.Owner != null)
				{
					flag = this.Owner.Enabled;
				}
				return this.state[ToolStripItem.stateEnabled] && flag;
			}
			set
			{
				if (this.state[ToolStripItem.stateEnabled] != value)
				{
					this.state[ToolStripItem.stateEnabled] = value;
					if (!this.state[ToolStripItem.stateEnabled])
					{
						bool flag = this.state[ToolStripItem.stateSelected];
						this.state[ToolStripItem.stateSelected | ToolStripItem.statePressed] = false;
						if (flag && !AccessibilityImprovements.UseLegacyToolTipDisplay)
						{
							KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
						}
					}
					this.OnEnabledChanged(EventArgs.Empty);
					this.Invalidate();
				}
				this.OnInternalEnabledChanged(EventArgs.Empty);
			}
		}

		// Token: 0x14000341 RID: 833
		// (add) Token: 0x060041E4 RID: 16868 RVA: 0x00119688 File Offset: 0x00117888
		// (remove) Token: 0x060041E5 RID: 16869 RVA: 0x0011969B File Offset: 0x0011789B
		[SRDescription("ToolStripItemEnabledChangedDescr")]
		public event EventHandler EnabledChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventEnabledChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventEnabledChanged, value);
			}
		}

		// Token: 0x14000342 RID: 834
		// (add) Token: 0x060041E6 RID: 16870 RVA: 0x001196AE File Offset: 0x001178AE
		// (remove) Token: 0x060041E7 RID: 16871 RVA: 0x001196C1 File Offset: 0x001178C1
		internal event EventHandler InternalEnabledChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventInternalEnabledChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventInternalEnabledChanged, value);
			}
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x001196D4 File Offset: 0x001178D4
		private void EnsureParentDropTargetRegistered()
		{
			if (this.ParentInternal != null)
			{
				IntSecurity.ClipboardRead.Demand();
				this.ParentInternal.DropTargetManager.EnsureRegistered(this);
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x001196FC File Offset: 0x001178FC
		// (set) Token: 0x060041EA RID: 16874 RVA: 0x0011973C File Offset: 0x0011793C
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemForeColorDescr")]
		public virtual Color ForeColor
		{
			get
			{
				Color color = this.Properties.GetColor(ToolStripItem.PropForeColor);
				if (!color.IsEmpty)
				{
					return color;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					return parentInternal.ForeColor;
				}
				return Control.DefaultForeColor;
			}
			set
			{
				Color foreColor = this.ForeColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(ToolStripItem.PropForeColor))
				{
					this.Properties.SetColor(ToolStripItem.PropForeColor, value);
				}
				if (!foreColor.Equals(this.ForeColor))
				{
					this.OnForeColorChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000343 RID: 835
		// (add) Token: 0x060041EB RID: 16875 RVA: 0x001197A1 File Offset: 0x001179A1
		// (remove) Token: 0x060041EC RID: 16876 RVA: 0x001197B4 File Offset: 0x001179B4
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnForeColorChangedDescr")]
		public event EventHandler ForeColorChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventForeColorChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventForeColorChanged, value);
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x001197C8 File Offset: 0x001179C8
		// (set) Token: 0x060041EE RID: 16878 RVA: 0x00119810 File Offset: 0x00117A10
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemFontDescr")]
		public virtual Font Font
		{
			get
			{
				Font font = (Font)this.Properties.GetObject(ToolStripItem.PropFont);
				if (font != null)
				{
					return font;
				}
				Font ownerFont = this.GetOwnerFont();
				if (ownerFont != null)
				{
					return ownerFont;
				}
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return ToolStripManager.DefaultFont;
				}
				return this.defaultFont;
			}
			set
			{
				Font font = (Font)this.Properties.GetObject(ToolStripItem.PropFont);
				if (font != value)
				{
					this.Properties.SetObject(ToolStripItem.PropFont, value);
					this.OnFontChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000344 RID: 836
		// (add) Token: 0x060041EF RID: 16879 RVA: 0x00119853 File Offset: 0x00117A53
		// (remove) Token: 0x060041F0 RID: 16880 RVA: 0x00119866 File Offset: 0x00117A66
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnGiveFeedbackDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventGiveFeedback, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventGiveFeedback, value);
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060041F1 RID: 16881 RVA: 0x0011987C File Offset: 0x00117A7C
		// (set) Token: 0x060041F2 RID: 16882 RVA: 0x00119898 File Offset: 0x00117A98
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				return this.Bounds.Height;
			}
			set
			{
				Rectangle rectangle = this.Bounds;
				this.SetBounds(rectangle.X, rectangle.Y, rectangle.Width, value);
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x001198C8 File Offset: 0x00117AC8
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return ToolStripItem.EmptyChildCollection;
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060041F4 RID: 16884 RVA: 0x001198CF File Offset: 0x00117ACF
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				if (this.ParentInternal == null)
				{
					return this.Owner;
				}
				return this.ParentInternal;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x00114EF8 File Offset: 0x001130F8
		Rectangle IArrangedElement.DisplayRectangle
		{
			get
			{
				return this.Bounds;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x001190C4 File Offset: 0x001172C4
		bool IArrangedElement.ParticipatesInLayout
		{
			get
			{
				return this.state[ToolStripItem.stateVisible];
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x001198E6 File Offset: 0x00117AE6
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x001198EE File Offset: 0x00117AEE
		void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			this.SetBounds(bounds);
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000072B6 File Offset: 0x000054B6
		void IArrangedElement.PerformLayout(IArrangedElement container, string propertyName)
		{
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x001198F7 File Offset: 0x00117AF7
		// (set) Token: 0x060041FB RID: 16891 RVA: 0x001198FF File Offset: 0x00117AFF
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageAlignDescr")]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this.imageAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.imageAlign != value)
				{
					this.imageAlign = value;
					this.InvalidateItemLayout(PropertyNames.ImageAlign);
				}
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x0011993C File Offset: 0x00117B3C
		// (set) Token: 0x060041FD RID: 16893 RVA: 0x001199F8 File Offset: 0x00117BF8
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		public virtual Image Image
		{
			get
			{
				Image image = (Image)this.Properties.GetObject(ToolStripItem.PropImage);
				if (image != null || this.Owner == null || this.Owner.ImageList == null || this.ImageIndexer.ActualIndex < 0)
				{
					return image;
				}
				if (this.ImageIndexer.ActualIndex < this.Owner.ImageList.Images.Count)
				{
					image = this.Owner.ImageList.Images[this.ImageIndexer.ActualIndex];
					this.state[ToolStripItem.stateInvalidMirroredImage] = true;
					this.Properties.SetObject(ToolStripItem.PropImage, image);
					return image;
				}
				return null;
			}
			set
			{
				if (this.Image != value)
				{
					this.StopAnimate();
					Bitmap bitmap = value as Bitmap;
					if (bitmap != null && this.ImageTransparentColor != Color.Empty)
					{
						if (bitmap.RawFormat.Guid != ImageFormat.Icon.Guid && !ImageAnimator.CanAnimate(bitmap))
						{
							bitmap.MakeTransparent(this.ImageTransparentColor);
						}
						value = bitmap;
					}
					if (value != null)
					{
						this.ImageIndex = -1;
					}
					this.Properties.SetObject(ToolStripItem.PropImage, value);
					this.state[ToolStripItem.stateInvalidMirroredImage] = true;
					this.Animate();
					this.InvalidateItemLayout(PropertyNames.Image);
				}
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x00119AA3 File Offset: 0x00117CA3
		// (set) Token: 0x060041FF RID: 16895 RVA: 0x00119AAC File Offset: 0x00117CAC
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageTransparentColorDescr")]
		public Color ImageTransparentColor
		{
			get
			{
				return this.imageTransparentColor;
			}
			set
			{
				if (this.imageTransparentColor != value)
				{
					this.imageTransparentColor = value;
					Bitmap bitmap = this.Image as Bitmap;
					if (bitmap != null && value != Color.Empty && bitmap.RawFormat.Guid != ImageFormat.Icon.Guid && !ImageAnimator.CanAnimate(bitmap))
					{
						bitmap.MakeTransparent(this.imageTransparentColor);
					}
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x00119B20 File Offset: 0x00117D20
		// (set) Token: 0x06004201 RID: 16897 RVA: 0x00119B98 File Offset: 0x00117D98
		[SRDescription("ToolStripItemImageIndexDescr")]
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(false)]
		[RelatedImageList("Owner.ImageList")]
		public int ImageIndex
		{
			get
			{
				if (this.Owner != null && this.ImageIndexer.Index != -1 && this.Owner.ImageList != null && this.ImageIndexer.Index >= this.Owner.ImageList.Images.Count)
				{
					return this.Owner.ImageList.Images.Count - 1;
				}
				return this.ImageIndexer.Index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						-1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.ImageIndexer.Index = value;
				this.state[ToolStripItem.stateInvalidMirroredImage] = true;
				this.Properties.SetObject(ToolStripItem.PropImage, null);
				this.InvalidateItemLayout(PropertyNames.ImageIndex);
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x00119C25 File Offset: 0x00117E25
		internal ToolStripItemImageIndexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ToolStripItemImageIndexer(this);
				}
				return this.imageIndexer;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x00119C41 File Offset: 0x00117E41
		// (set) Token: 0x06004204 RID: 16900 RVA: 0x00119C4E File Offset: 0x00117E4E
		[SRDescription("ToolStripItemImageKeyDescr")]
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(false)]
		[RelatedImageList("Owner.ImageList")]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				this.ImageIndexer.Key = value;
				this.state[ToolStripItem.stateInvalidMirroredImage] = true;
				this.Properties.SetObject(ToolStripItem.PropImage, null);
				this.InvalidateItemLayout(PropertyNames.ImageKey);
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x00119C89 File Offset: 0x00117E89
		// (set) Token: 0x06004206 RID: 16902 RVA: 0x00119C94 File Offset: 0x00117E94
		[SRCategory("CatAppearance")]
		[DefaultValue(ToolStripItemImageScaling.SizeToFit)]
		[Localizable(true)]
		[SRDescription("ToolStripItemImageScalingDescr")]
		public ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return this.imageScaling;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripItemImageScaling));
				}
				if (this.imageScaling != value)
				{
					this.imageScaling = value;
					this.InvalidateItemLayout(PropertyNames.ImageScaling);
				}
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x00119CE2 File Offset: 0x00117EE2
		internal ToolStripItemInternalLayout InternalLayout
		{
			get
			{
				if (this.toolStripItemInternalLayout == null)
				{
					this.toolStripItemInternalLayout = this.CreateInternalLayout();
				}
				return this.toolStripItemInternalLayout;
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x00119D00 File Offset: 0x00117F00
		internal bool IsForeColorSet
		{
			get
			{
				if (!this.Properties.GetColor(ToolStripItem.PropForeColor).IsEmpty)
				{
					return true;
				}
				Control parentInternal = this.ParentInternal;
				return parentInternal != null && parentInternal.ShouldSerializeForeColor();
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x0010C4D9 File Offset: 0x0010A6D9
		internal bool IsInDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x0600420A RID: 16906 RVA: 0x00119D3B File Offset: 0x00117F3B
		[Browsable(false)]
		public bool IsDisposed
		{
			get
			{
				return this.state[ToolStripItem.stateDisposed];
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x00119D4D File Offset: 0x00117F4D
		[Browsable(false)]
		public bool IsOnDropDown
		{
			get
			{
				if (this.ParentInternal != null)
				{
					return this.ParentInternal.IsDropDown;
				}
				return this.Owner != null && this.Owner.IsDropDown;
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x00119D7B File Offset: 0x00117F7B
		[Browsable(false)]
		public bool IsOnOverflow
		{
			get
			{
				return this.Placement == ToolStripItemPlacement.Overflow;
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool IsMnemonicsListenerAxSourced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x14000345 RID: 837
		// (add) Token: 0x0600420E RID: 16910 RVA: 0x00119D86 File Offset: 0x00117F86
		// (remove) Token: 0x0600420F RID: 16911 RVA: 0x00119D99 File Offset: 0x00117F99
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripItemOnLocationChangedDescr")]
		public event EventHandler LocationChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventLocationChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventLocationChanged, value);
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x00019C19 File Offset: 0x00017E19
		// (set) Token: 0x06004211 RID: 16913 RVA: 0x00119DAC File Offset: 0x00117FAC
		[SRDescription("ToolStripItemMarginDescr")]
		[SRCategory("CatLayout")]
		public Padding Margin
		{
			get
			{
				return CommonProperties.GetMargin(this);
			}
			set
			{
				if (this.Margin != value)
				{
					this.state[ToolStripItem.stateUseAmbientMargin] = false;
					CommonProperties.SetMargin(this, value);
				}
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x00119DD4 File Offset: 0x00117FD4
		// (set) Token: 0x06004213 RID: 16915 RVA: 0x00119DFA File Offset: 0x00117FFA
		[SRDescription("ToolStripMergeActionDescr")]
		[DefaultValue(MergeAction.Append)]
		[SRCategory("CatLayout")]
		public MergeAction MergeAction
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(ToolStripItem.PropMergeAction, out flag);
				if (flag)
				{
					return (MergeAction)integer;
				}
				return MergeAction.Append;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MergeAction));
				}
				this.Properties.SetInteger(ToolStripItem.PropMergeAction, (int)value);
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x00119E34 File Offset: 0x00118034
		// (set) Token: 0x06004215 RID: 16917 RVA: 0x00119E5A File Offset: 0x0011805A
		[SRDescription("ToolStripMergeIndexDescr")]
		[DefaultValue(-1)]
		[SRCategory("CatLayout")]
		public int MergeIndex
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(ToolStripItem.PropMergeIndex, out flag);
				if (flag)
				{
					return integer;
				}
				return -1;
			}
			set
			{
				this.Properties.SetInteger(ToolStripItem.PropMergeIndex, value);
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x00119E6D File Offset: 0x0011806D
		// (set) Token: 0x06004217 RID: 16919 RVA: 0x00119E7F File Offset: 0x0011807F
		internal bool MouseDownAndUpMustBeInSameItem
		{
			get
			{
				return this.state[ToolStripItem.stateMouseDownAndUpMustBeInSameItem];
			}
			set
			{
				this.state[ToolStripItem.stateMouseDownAndUpMustBeInSameItem] = value;
			}
		}

		// Token: 0x14000346 RID: 838
		// (add) Token: 0x06004218 RID: 16920 RVA: 0x00119E92 File Offset: 0x00118092
		// (remove) Token: 0x06004219 RID: 16921 RVA: 0x00119EA5 File Offset: 0x001180A5
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseDownDescr")]
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseDown, value);
			}
		}

		// Token: 0x14000347 RID: 839
		// (add) Token: 0x0600421A RID: 16922 RVA: 0x00119EB8 File Offset: 0x001180B8
		// (remove) Token: 0x0600421B RID: 16923 RVA: 0x00119ECB File Offset: 0x001180CB
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseEnterDescr")]
		public event EventHandler MouseEnter
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseEnter, value);
			}
		}

		// Token: 0x14000348 RID: 840
		// (add) Token: 0x0600421C RID: 16924 RVA: 0x00119EDE File Offset: 0x001180DE
		// (remove) Token: 0x0600421D RID: 16925 RVA: 0x00119EF1 File Offset: 0x001180F1
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseLeaveDescr")]
		public event EventHandler MouseLeave
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseLeave, value);
			}
		}

		// Token: 0x14000349 RID: 841
		// (add) Token: 0x0600421E RID: 16926 RVA: 0x00119F04 File Offset: 0x00118104
		// (remove) Token: 0x0600421F RID: 16927 RVA: 0x00119F17 File Offset: 0x00118117
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseHoverDescr")]
		public event EventHandler MouseHover
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseHover, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseHover, value);
			}
		}

		// Token: 0x1400034A RID: 842
		// (add) Token: 0x06004220 RID: 16928 RVA: 0x00119F2A File Offset: 0x0011812A
		// (remove) Token: 0x06004221 RID: 16929 RVA: 0x00119F3D File Offset: 0x0011813D
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseMoveDescr")]
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseMove, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseMove, value);
			}
		}

		// Token: 0x1400034B RID: 843
		// (add) Token: 0x06004222 RID: 16930 RVA: 0x00119F50 File Offset: 0x00118150
		// (remove) Token: 0x06004223 RID: 16931 RVA: 0x00119F63 File Offset: 0x00118163
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseUpDescr")]
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseUp, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseUp, value);
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x00119F76 File Offset: 0x00118176
		// (set) Token: 0x06004225 RID: 16933 RVA: 0x00119F93 File Offset: 0x00118193
		[Browsable(false)]
		[DefaultValue(null)]
		public string Name
		{
			get
			{
				return WindowsFormsUtils.GetComponentName(this, (string)this.Properties.GetObject(ToolStripItem.PropName));
			}
			set
			{
				if (base.DesignMode)
				{
					return;
				}
				this.Properties.SetObject(ToolStripItem.PropName, value);
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x00119FAF File Offset: 0x001181AF
		// (set) Token: 0x06004227 RID: 16935 RVA: 0x00119FB7 File Offset: 0x001181B7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStrip Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				if (this.owner != value)
				{
					if (this.owner != null)
					{
						this.owner.Items.Remove(this);
					}
					if (value != null)
					{
						value.Items.Add(this);
					}
				}
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x00119FEC File Offset: 0x001181EC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripItem OwnerItem
		{
			get
			{
				ToolStripDropDown toolStripDropDown = null;
				if (this.ParentInternal != null)
				{
					toolStripDropDown = (this.ParentInternal as ToolStripDropDown);
				}
				else if (this.Owner != null)
				{
					toolStripDropDown = (this.Owner as ToolStripDropDown);
				}
				if (toolStripDropDown != null)
				{
					return toolStripDropDown.OwnerItem;
				}
				return null;
			}
		}

		// Token: 0x1400034C RID: 844
		// (add) Token: 0x06004229 RID: 16937 RVA: 0x0011A030 File Offset: 0x00118230
		// (remove) Token: 0x0600422A RID: 16938 RVA: 0x0011A043 File Offset: 0x00118243
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemOwnerChangedDescr")]
		public event EventHandler OwnerChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventOwnerChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventOwnerChanged, value);
			}
		}

		// Token: 0x1400034D RID: 845
		// (add) Token: 0x0600422B RID: 16939 RVA: 0x0011A056 File Offset: 0x00118256
		// (remove) Token: 0x0600422C RID: 16940 RVA: 0x0011A069 File Offset: 0x00118269
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemOnPaintDescr")]
		public event PaintEventHandler Paint
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventPaint, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventPaint, value);
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x0600422D RID: 16941 RVA: 0x0011A07C File Offset: 0x0011827C
		// (set) Token: 0x0600422E RID: 16942 RVA: 0x0011A084 File Offset: 0x00118284
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal ToolStrip Parent
		{
			get
			{
				return this.ParentInternal;
			}
			set
			{
				this.ParentInternal = value;
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x0011A08D File Offset: 0x0011828D
		// (set) Token: 0x06004230 RID: 16944 RVA: 0x0011A098 File Offset: 0x00118298
		[DefaultValue(ToolStripItemOverflow.AsNeeded)]
		[SRDescription("ToolStripItemOverflowDescr")]
		[SRCategory("CatLayout")]
		public ToolStripItemOverflow Overflow
		{
			get
			{
				return this.overflow;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripGripStyle));
				}
				if (this.overflow != value)
				{
					this.overflow = value;
					if (this.Owner != null)
					{
						LayoutTransaction.DoLayout(this.Owner, this.Owner, "Overflow");
					}
				}
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06004231 RID: 16945 RVA: 0x0011A0F9 File Offset: 0x001182F9
		// (set) Token: 0x06004232 RID: 16946 RVA: 0x0011A107 File Offset: 0x00118307
		[SRDescription("ToolStripItemPaddingDescr")]
		[SRCategory("CatLayout")]
		public virtual Padding Padding
		{
			get
			{
				return CommonProperties.GetPadding(this, this.DefaultPadding);
			}
			set
			{
				if (this.Padding != value)
				{
					CommonProperties.SetPadding(this, value);
					this.InvalidateItemLayout(PropertyNames.Padding);
				}
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x0011A129 File Offset: 0x00118329
		// (set) Token: 0x06004234 RID: 16948 RVA: 0x0011A134 File Offset: 0x00118334
		internal ToolStrip ParentInternal
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != value)
				{
					ToolStrip oldParent = this.parent;
					this.parent = value;
					this.OnParentChanged(oldParent, value);
				}
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x0011A160 File Offset: 0x00118360
		[Browsable(false)]
		public ToolStripItemPlacement Placement
		{
			get
			{
				return this.placement;
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x0011A168 File Offset: 0x00118368
		internal Size PreferredImageSize
		{
			get
			{
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) != ToolStripItemDisplayStyle.Image)
				{
					return Size.Empty;
				}
				Image image = (Image)this.Properties.GetObject(ToolStripItem.PropImage);
				bool flag = this.Owner != null && this.Owner.ImageList != null && this.ImageIndexer.ActualIndex >= 0;
				if (this.ImageScaling == ToolStripItemImageScaling.SizeToFit)
				{
					ToolStrip toolStrip = this.Owner;
					if (toolStrip != null && (image != null || flag))
					{
						return toolStrip.ImageScalingSize;
					}
				}
				Size result = Size.Empty;
				if (flag)
				{
					result = this.Owner.ImageList.ImageSize;
				}
				else
				{
					result = ((image == null) ? Size.Empty : image.Size);
				}
				return result;
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x0011A216 File Offset: 0x00118416
		internal PropertyStore Properties
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.propertyStore = new PropertyStore();
				}
				return this.propertyStore;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x0011A231 File Offset: 0x00118431
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Pressed
		{
			get
			{
				return this.CanSelect && this.state[ToolStripItem.statePressed];
			}
		}

		// Token: 0x1400034E RID: 846
		// (add) Token: 0x06004239 RID: 16953 RVA: 0x0011A24D File Offset: 0x0011844D
		// (remove) Token: 0x0600423A RID: 16954 RVA: 0x0011A260 File Offset: 0x00118460
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnQueryContinueDragDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventQueryContinueDrag, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventQueryContinueDrag, value);
			}
		}

		// Token: 0x1400034F RID: 847
		// (add) Token: 0x0600423B RID: 16955 RVA: 0x0011A273 File Offset: 0x00118473
		// (remove) Token: 0x0600423C RID: 16956 RVA: 0x0011A286 File Offset: 0x00118486
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemOnQueryAccessibilityHelpDescr")]
		public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventQueryAccessibilityHelp, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventQueryAccessibilityHelp, value);
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x0011A299 File Offset: 0x00118499
		internal Color RawBackColor
		{
			get
			{
				return this.Properties.GetColor(ToolStripItem.PropBackColor);
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x0011A2AB File Offset: 0x001184AB
		internal ToolStripRenderer Renderer
		{
			get
			{
				if (this.Owner != null)
				{
					return this.Owner.Renderer;
				}
				if (this.ParentInternal == null)
				{
					return null;
				}
				return this.ParentInternal.Renderer;
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x0011A2D8 File Offset: 0x001184D8
		// (set) Token: 0x06004240 RID: 16960 RVA: 0x0011A338 File Offset: 0x00118538
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemRightToLeftDescr")]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				bool flag;
				int num = this.Properties.GetInteger(ToolStripItem.PropRightToLeft, out flag);
				if (!flag)
				{
					num = 2;
				}
				if (num == 2)
				{
					if (this.Owner != null)
					{
						num = (int)this.Owner.RightToLeft;
					}
					else if (this.ParentInternal != null)
					{
						num = (int)this.ParentInternal.RightToLeft;
					}
					else
					{
						num = (int)this.DefaultRightToLeft;
					}
				}
				return (RightToLeft)num;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("RightToLeft", (int)value, typeof(RightToLeft));
				}
				RightToLeft rightToLeft = this.RightToLeft;
				if (this.Properties.ContainsInteger(ToolStripItem.PropRightToLeft) || value != RightToLeft.Inherit)
				{
					this.Properties.SetInteger(ToolStripItem.PropRightToLeft, (int)value);
				}
				if (rightToLeft != this.RightToLeft)
				{
					this.OnRightToLeftChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x0011A3AD File Offset: 0x001185AD
		// (set) Token: 0x06004242 RID: 16962 RVA: 0x0011A3BF File Offset: 0x001185BF
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemRightToLeftAutoMirrorImageDescr")]
		public bool RightToLeftAutoMirrorImage
		{
			get
			{
				return this.state[ToolStripItem.stateRightToLeftAutoMirrorImage];
			}
			set
			{
				if (this.state[ToolStripItem.stateRightToLeftAutoMirrorImage] != value)
				{
					this.state[ToolStripItem.stateRightToLeftAutoMirrorImage] = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x0011A3EC File Offset: 0x001185EC
		internal Image MirroredImage
		{
			get
			{
				if (!this.state[ToolStripItem.stateInvalidMirroredImage])
				{
					return this.Properties.GetObject(ToolStripItem.PropMirroredImage) as Image;
				}
				Image image = this.Image;
				if (image != null)
				{
					Image image2 = image.Clone() as Image;
					image2.RotateFlip(RotateFlipType.RotateNoneFlipX);
					this.Properties.SetObject(ToolStripItem.PropMirroredImage, image2);
					this.state[ToolStripItem.stateInvalidMirroredImage] = false;
					return image2;
				}
				return null;
			}
		}

		// Token: 0x14000350 RID: 848
		// (add) Token: 0x06004244 RID: 16964 RVA: 0x0011A463 File Offset: 0x00118663
		// (remove) Token: 0x06004245 RID: 16965 RVA: 0x0011A476 File Offset: 0x00118676
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnRightToLeftChangedDescr")]
		public event EventHandler RightToLeftChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventRightToLeft, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventRightToLeft, value);
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06004246 RID: 16966 RVA: 0x0011A48C File Offset: 0x0011868C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Selected
		{
			get
			{
				return this.CanSelect && !base.DesignMode && (this.state[ToolStripItem.stateSelected] || (this.ParentInternal != null && this.ParentInternal.IsSelectionSuspended && this.ParentInternal.LastMouseDownedItem == this));
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x0011A4E4 File Offset: 0x001186E4
		protected internal virtual bool ShowKeyboardCues
		{
			get
			{
				return base.DesignMode || ToolStripManager.ShowMenuFocusCues;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06004248 RID: 16968 RVA: 0x0011A4F8 File Offset: 0x001186F8
		// (set) Token: 0x06004249 RID: 16969 RVA: 0x0011A514 File Offset: 0x00118714
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ToolStripItemSizeDescr")]
		public virtual Size Size
		{
			get
			{
				return this.Bounds.Size;
			}
			set
			{
				Rectangle rectangle = this.Bounds;
				rectangle.Size = value;
				this.SetBounds(rectangle);
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x0600424A RID: 16970 RVA: 0x0011A537 File Offset: 0x00118737
		// (set) Token: 0x0600424B RID: 16971 RVA: 0x0011A549 File Offset: 0x00118749
		internal bool SupportsRightClick
		{
			get
			{
				return this.state[ToolStripItem.stateSupportsRightClick];
			}
			set
			{
				this.state[ToolStripItem.stateSupportsRightClick] = value;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x0011A55C File Offset: 0x0011875C
		// (set) Token: 0x0600424D RID: 16973 RVA: 0x0011A56E File Offset: 0x0011876E
		internal bool SupportsItemClick
		{
			get
			{
				return this.state[ToolStripItem.stateSupportsItemClick];
			}
			set
			{
				this.state[ToolStripItem.stateSupportsItemClick] = value;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x0600424E RID: 16974 RVA: 0x0011A581 File Offset: 0x00118781
		// (set) Token: 0x0600424F RID: 16975 RVA: 0x0011A593 File Offset: 0x00118793
		internal bool SupportsSpaceKey
		{
			get
			{
				return this.state[ToolStripItem.stateSupportsSpaceKey];
			}
			set
			{
				this.state[ToolStripItem.stateSupportsSpaceKey] = value;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x0011A5A6 File Offset: 0x001187A6
		// (set) Token: 0x06004251 RID: 16977 RVA: 0x0011A5B8 File Offset: 0x001187B8
		internal bool SupportsDisabledHotTracking
		{
			get
			{
				return this.state[ToolStripItem.stateSupportsDisabledHotTracking];
			}
			set
			{
				this.state[ToolStripItem.stateSupportsDisabledHotTracking] = value;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06004252 RID: 16978 RVA: 0x0011A5CB File Offset: 0x001187CB
		// (set) Token: 0x06004253 RID: 16979 RVA: 0x0011A5F1 File Offset: 0x001187F1
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ToolStripItemTagDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				if (this.Properties.ContainsObject(ToolStripItem.PropTag))
				{
					return this.propertyStore.GetObject(ToolStripItem.PropTag);
				}
				return null;
			}
			set
			{
				this.Properties.SetObject(ToolStripItem.PropTag, value);
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x0011A604 File Offset: 0x00118804
		// (set) Token: 0x06004255 RID: 16981 RVA: 0x0011A633 File Offset: 0x00118833
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemTextDescr")]
		public virtual string Text
		{
			get
			{
				if (this.Properties.ContainsObject(ToolStripItem.PropText))
				{
					return (string)this.Properties.GetObject(ToolStripItem.PropText);
				}
				return "";
			}
			set
			{
				if (value != this.Text)
				{
					this.Properties.SetObject(ToolStripItem.PropText, value);
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06004256 RID: 16982 RVA: 0x0011A65F File Offset: 0x0011885F
		// (set) Token: 0x06004257 RID: 16983 RVA: 0x0011A667 File Offset: 0x00118867
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemTextAlignDescr")]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.textAlign != value)
				{
					this.textAlign = value;
					this.InvalidateItemLayout(PropertyNames.TextAlign);
				}
			}
		}

		// Token: 0x14000351 RID: 849
		// (add) Token: 0x06004258 RID: 16984 RVA: 0x0011A6A2 File Offset: 0x001188A2
		// (remove) Token: 0x06004259 RID: 16985 RVA: 0x0011A6B5 File Offset: 0x001188B5
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnTextChangedDescr")]
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventText, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventText, value);
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x0600425A RID: 16986 RVA: 0x0011A6C8 File Offset: 0x001188C8
		// (set) Token: 0x0600425B RID: 16987 RVA: 0x0011A730 File Offset: 0x00118930
		[SRDescription("ToolStripTextDirectionDescr")]
		[SRCategory("CatAppearance")]
		public virtual ToolStripTextDirection TextDirection
		{
			get
			{
				ToolStripTextDirection toolStripTextDirection = ToolStripTextDirection.Inherit;
				if (this.Properties.ContainsObject(ToolStripItem.PropTextDirection))
				{
					toolStripTextDirection = (ToolStripTextDirection)this.Properties.GetObject(ToolStripItem.PropTextDirection);
				}
				if (toolStripTextDirection == ToolStripTextDirection.Inherit)
				{
					if (this.ParentInternal != null)
					{
						toolStripTextDirection = this.ParentInternal.TextDirection;
					}
					else
					{
						toolStripTextDirection = ((this.Owner == null) ? ToolStripTextDirection.Horizontal : this.Owner.TextDirection);
					}
				}
				return toolStripTextDirection;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripTextDirection));
				}
				this.Properties.SetObject(ToolStripItem.PropTextDirection, value);
				this.InvalidateItemLayout("TextDirection");
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x0600425C RID: 16988 RVA: 0x0011A784 File Offset: 0x00118984
		// (set) Token: 0x0600425D RID: 16989 RVA: 0x0011A78C File Offset: 0x0011898C
		[DefaultValue(TextImageRelation.ImageBeforeText)]
		[Localizable(true)]
		[SRDescription("ToolStripItemTextImageRelationDescr")]
		[SRCategory("CatAppearance")]
		public TextImageRelation TextImageRelation
		{
			get
			{
				return this.textImageRelation;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidTextImageRelation(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TextImageRelation));
				}
				if (value != this.TextImageRelation)
				{
					this.textImageRelation = value;
					this.InvalidateItemLayout(PropertyNames.TextImageRelation);
				}
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x0011A7C8 File Offset: 0x001189C8
		// (set) Token: 0x0600425F RID: 16991 RVA: 0x0011A81D File Offset: 0x00118A1D
		[SRDescription("ToolStripItemToolTipTextDescr")]
		[SRCategory("CatBehavior")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		public string ToolTipText
		{
			get
			{
				if (this.AutoToolTip && string.IsNullOrEmpty(this.toolTipText))
				{
					string text = this.Text;
					if (WindowsFormsUtils.ContainsMnemonic(text))
					{
						text = string.Join("", text.Split(new char[]
						{
							'&'
						}));
					}
					return text;
				}
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x0011A826 File Offset: 0x00118A26
		// (set) Token: 0x06004261 RID: 16993 RVA: 0x001190D6 File Offset: 0x001172D6
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ToolStripItemVisibleDescr")]
		public bool Visible
		{
			get
			{
				return this.ParentInternal != null && this.ParentInternal.Visible && this.Available;
			}
			set
			{
				this.SetVisibleCore(value);
			}
		}

		// Token: 0x14000352 RID: 850
		// (add) Token: 0x06004262 RID: 16994 RVA: 0x0011A845 File Offset: 0x00118A45
		// (remove) Token: 0x06004263 RID: 16995 RVA: 0x0011A858 File Offset: 0x00118A58
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnVisibleChangedDescr")]
		public event EventHandler VisibleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventVisibleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventVisibleChanged, value);
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x0011A86C File Offset: 0x00118A6C
		// (set) Token: 0x06004265 RID: 16997 RVA: 0x0011A888 File Offset: 0x00118A88
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Width
		{
			get
			{
				return this.Bounds.Width;
			}
			set
			{
				Rectangle rectangle = this.Bounds;
				this.SetBounds(rectangle.X, rectangle.Y, value, rectangle.Height);
			}
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x0011A8B8 File Offset: 0x00118AB8
		internal void AccessibilityNotifyClients(AccessibleEvents accEvent)
		{
			if (this.ParentInternal != null)
			{
				int childID = this.ParentInternal.DisplayedItems.IndexOf(this);
				this.ParentInternal.AccessibilityNotifyClients(accEvent, childID);
			}
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x0011A8EC File Offset: 0x00118AEC
		private void Animate()
		{
			this.Animate(!base.DesignMode && this.Visible && this.Enabled && this.ParentInternal != null);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x0011A918 File Offset: 0x00118B18
		private void StopAnimate()
		{
			this.Animate(false);
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x0011A924 File Offset: 0x00118B24
		private void Animate(bool animate)
		{
			if (animate != this.state[ToolStripItem.stateCurrentlyAnimatingImage])
			{
				if (animate)
				{
					if (this.Image != null)
					{
						ImageAnimator.Animate(this.Image, new EventHandler(this.OnAnimationFrameChanged));
						this.state[ToolStripItem.stateCurrentlyAnimatingImage] = animate;
						return;
					}
				}
				else if (this.Image != null)
				{
					ImageAnimator.StopAnimate(this.Image, new EventHandler(this.OnAnimationFrameChanged));
					this.state[ToolStripItem.stateCurrentlyAnimatingImage] = animate;
				}
			}
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x0011A9A8 File Offset: 0x00118BA8
		internal bool BeginDragForItemReorder()
		{
			if (Control.ModifierKeys == Keys.Alt && this.ParentInternal.Items.Contains(this) && this.ParentInternal.AllowItemReorder)
			{
				this.DoDragDrop(this, DragDropEffects.Move);
				return true;
			}
			return false;
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x0011A9EF File Offset: 0x00118BEF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripItem.ToolStripItemAccessibleObject(this);
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x0011A9F7 File Offset: 0x00118BF7
		internal virtual ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripItemInternalLayout(this);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x0011AA00 File Offset: 0x00118C00
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.state[ToolStripItem.stateDisposing] = true;
				if (this.Owner != null)
				{
					this.StopAnimate();
					this.Owner.Items.Remove(this);
					this.toolStripItemInternalLayout = null;
					this.state[ToolStripItem.stateDisposed] = true;
				}
			}
			if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
			{
				this.ClearAccessibilityObjectOwner();
			}
			base.Dispose(disposing);
			if (disposing)
			{
				this.Properties.SetObject(ToolStripItem.PropMirroredImage, null);
				this.Properties.SetObject(ToolStripItem.PropImage, null);
				this.state[ToolStripItem.stateDisposing] = false;
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x0600426E RID: 17006 RVA: 0x0011AAA2 File Offset: 0x00118CA2
		internal static long DoubleClickTicks
		{
			get
			{
				return (long)(SystemInformation.DoubleClickTime * 10000);
			}
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x0011AAB0 File Offset: 0x00118CB0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
		public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
		{
			int[] array = new int[1];
			UnsafeNativeMethods.IOleDropSource dropSource = this.DropSource;
			IDataObject dataObject = data as IDataObject;
			if (dataObject == null)
			{
				IDataObject dataObject2 = data as IDataObject;
				DataObject dataObject3;
				if (dataObject2 != null)
				{
					dataObject3 = new DataObject(dataObject2);
				}
				else if (data is ToolStripItem)
				{
					dataObject3 = new DataObject();
					dataObject3.SetData(typeof(ToolStripItem).ToString(), data);
				}
				else
				{
					dataObject3 = new DataObject();
					dataObject3.SetData(data);
				}
				dataObject = dataObject3;
			}
			try
			{
				SafeNativeMethods.DoDragDrop(dataObject, dropSource, (int)allowedEffects, array);
			}
			catch
			{
			}
			return (DragDropEffects)array[0];
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x0011AB48 File Offset: 0x00118D48
		internal void FireEvent(ToolStripItemEventType met)
		{
			this.FireEvent(new EventArgs(), met);
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x0011AB58 File Offset: 0x00118D58
		internal void FireEvent(EventArgs e, ToolStripItemEventType met)
		{
			switch (met)
			{
			case ToolStripItemEventType.Paint:
				this.HandlePaint(e as PaintEventArgs);
				return;
			case ToolStripItemEventType.LocationChanged:
				this.OnLocationChanged(e);
				return;
			case ToolStripItemEventType.MouseMove:
				if (!this.Enabled && this.ParentInternal != null)
				{
					this.BeginDragForItemReorder();
					return;
				}
				this.FireEventInteractive(e, met);
				return;
			case ToolStripItemEventType.MouseEnter:
				this.HandleMouseEnter(e);
				return;
			case ToolStripItemEventType.MouseLeave:
				if (!this.Enabled && this.ParentInternal != null)
				{
					this.ParentInternal.UpdateToolTip(null);
					return;
				}
				this.HandleMouseLeave(e);
				return;
			case ToolStripItemEventType.MouseHover:
				if (!this.Enabled && this.ParentInternal != null && !string.IsNullOrEmpty(this.ToolTipText))
				{
					this.ParentInternal.UpdateToolTip(this);
					return;
				}
				this.FireEventInteractive(e, met);
				return;
			}
			this.FireEventInteractive(e, met);
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x0011AC30 File Offset: 0x00118E30
		internal void FireEventInteractive(EventArgs e, ToolStripItemEventType met)
		{
			if (this.Enabled)
			{
				switch (met)
				{
				case ToolStripItemEventType.MouseUp:
					this.HandleMouseUp(e as MouseEventArgs);
					return;
				case ToolStripItemEventType.MouseDown:
					this.HandleMouseDown(e as MouseEventArgs);
					return;
				case ToolStripItemEventType.MouseMove:
					this.HandleMouseMove(e as MouseEventArgs);
					return;
				case ToolStripItemEventType.MouseEnter:
				case ToolStripItemEventType.MouseLeave:
					break;
				case ToolStripItemEventType.MouseHover:
					this.HandleMouseHover(e);
					return;
				case ToolStripItemEventType.Click:
					this.HandleClick(e);
					return;
				case ToolStripItemEventType.DoubleClick:
					this.HandleDoubleClick(e);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x0011ACAC File Offset: 0x00118EAC
		private Font GetOwnerFont()
		{
			if (this.Owner != null)
			{
				return this.Owner.Font;
			}
			return null;
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x0011ACC3 File Offset: 0x00118EC3
		public ToolStrip GetCurrentParent()
		{
			return this.Parent;
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x0011ACCB File Offset: 0x00118ECB
		internal ToolStripDropDown GetCurrentParentDropDown()
		{
			if (this.ParentInternal != null)
			{
				return this.ParentInternal as ToolStripDropDown;
			}
			return this.Owner as ToolStripDropDown;
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x0011ACEC File Offset: 0x00118EEC
		public virtual Size GetPreferredSize(Size constrainingSize)
		{
			constrainingSize = LayoutUtils.ConvertZeroToUnbounded(constrainingSize);
			return this.InternalLayout.GetPreferredSize(constrainingSize - this.Padding.Size) + this.Padding.Size;
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x0011AD34 File Offset: 0x00118F34
		internal Size GetTextSize()
		{
			if (string.IsNullOrEmpty(this.Text))
			{
				return Size.Empty;
			}
			if (this.cachedTextSize == Size.Empty)
			{
				this.cachedTextSize = TextRenderer.MeasureText(this.Text, this.Font);
			}
			return this.cachedTextSize;
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x0011AD83 File Offset: 0x00118F83
		public void Invalidate()
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.Invalidate(this.Bounds, true);
			}
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x0011ADA0 File Offset: 0x00118FA0
		public void Invalidate(Rectangle r)
		{
			Point location = this.TranslatePoint(r.Location, ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ToolStripCoords);
			if (this.ParentInternal != null)
			{
				this.ParentInternal.Invalidate(new Rectangle(location, r.Size), true);
			}
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x0011ADDE File Offset: 0x00118FDE
		internal void InvalidateItemLayout(string affectedProperty, bool invalidatePainting)
		{
			this.toolStripItemInternalLayout = null;
			if (this.Owner != null)
			{
				LayoutTransaction.DoLayout(this.Owner, this, affectedProperty);
			}
			if (invalidatePainting && this.Owner != null)
			{
				this.Owner.Invalidate();
			}
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x0011AE12 File Offset: 0x00119012
		internal void InvalidateItemLayout(string affectedProperty)
		{
			this.InvalidateItemLayout(affectedProperty, true);
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x0011AE1C File Offset: 0x0011901C
		internal void InvalidateImageListImage()
		{
			if (this.ImageIndexer.ActualIndex >= 0)
			{
				this.Properties.SetObject(ToolStripItem.PropImage, null);
				this.InvalidateItemLayout(PropertyNames.Image);
			}
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x0011AE48 File Offset: 0x00119048
		internal void InvokePaint()
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.InvokePaintItem(this);
			}
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected internal virtual bool IsInputKey(Keys keyData)
		{
			return false;
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected internal virtual bool IsInputChar(char charCode)
		{
			return false;
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x0011AE60 File Offset: 0x00119060
		private void HandleClick(EventArgs e)
		{
			try
			{
				if (!base.DesignMode)
				{
					this.state[ToolStripItem.statePressed] = true;
				}
				this.InvokePaint();
				if (this.SupportsItemClick && this.Owner != null)
				{
					this.Owner.HandleItemClick(this);
				}
				this.OnClick(e);
				if (this.SupportsItemClick && this.Owner != null)
				{
					this.Owner.HandleItemClicked(this);
				}
			}
			finally
			{
				this.state[ToolStripItem.statePressed] = false;
			}
			this.Invalidate();
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x0011AEF8 File Offset: 0x001190F8
		private void HandleDoubleClick(EventArgs e)
		{
			this.OnDoubleClick(e);
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x0011AF01 File Offset: 0x00119101
		private void HandlePaint(PaintEventArgs e)
		{
			this.Animate();
			ImageAnimator.UpdateFrames(this.Image);
			this.OnPaint(e);
			this.RaisePaintEvent(ToolStripItem.EventPaint, e);
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x0011AF28 File Offset: 0x00119128
		private void HandleMouseEnter(EventArgs e)
		{
			if (!base.DesignMode && this.ParentInternal != null && this.ParentInternal.CanHotTrack && this.ParentInternal.ShouldSelectItem())
			{
				if (this.Enabled)
				{
					bool menuAutoExpand = this.ParentInternal.MenuAutoExpand;
					if (this.ParentInternal.LastMouseDownedItem == this && UnsafeNativeMethods.GetKeyState(1) < 0)
					{
						this.Push(true);
					}
					this.Select();
					this.ParentInternal.MenuAutoExpand = menuAutoExpand;
				}
				else if (this.SupportsDisabledHotTracking)
				{
					this.Select();
				}
			}
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
			{
				KeyboardToolTipStateMachine.Instance.NotifyAboutMouseEnter(this);
			}
			if (this.Enabled)
			{
				this.OnMouseEnter(e);
				this.RaiseEvent(ToolStripItem.EventMouseEnter, e);
			}
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x0011AFE0 File Offset: 0x001191E0
		private void HandleMouseMove(MouseEventArgs mea)
		{
			if (this.Enabled && this.CanSelect && !this.Selected && this.ParentInternal != null && this.ParentInternal.CanHotTrack && this.ParentInternal.ShouldSelectItem())
			{
				this.Select();
			}
			this.OnMouseMove(mea);
			this.RaiseMouseEvent(ToolStripItem.EventMouseMove, mea);
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x0011B040 File Offset: 0x00119240
		private void HandleMouseHover(EventArgs e)
		{
			this.OnMouseHover(e);
			this.RaiseEvent(ToolStripItem.EventMouseHover, e);
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x0011B058 File Offset: 0x00119258
		private void HandleLeave()
		{
			if (this.state[ToolStripItem.stateMouseDownAndNoDrag] || this.state[ToolStripItem.statePressed] || this.state[ToolStripItem.stateSelected])
			{
				this.state[ToolStripItem.stateMouseDownAndNoDrag | ToolStripItem.statePressed | ToolStripItem.stateSelected] = false;
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
				}
				this.Invalidate();
			}
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x0011B0D0 File Offset: 0x001192D0
		private void HandleMouseLeave(EventArgs e)
		{
			this.HandleLeave();
			if (this.Enabled)
			{
				this.OnMouseLeave(e);
				this.RaiseEvent(ToolStripItem.EventMouseLeave, e);
			}
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x0011B0F4 File Offset: 0x001192F4
		private void HandleMouseDown(MouseEventArgs e)
		{
			this.state[ToolStripItem.stateMouseDownAndNoDrag] = !this.BeginDragForItemReorder();
			if (this.state[ToolStripItem.stateMouseDownAndNoDrag])
			{
				if (e.Button == MouseButtons.Left)
				{
					this.Push(true);
				}
				this.OnMouseDown(e);
				this.RaiseMouseEvent(ToolStripItem.EventMouseDown, e);
			}
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x0011B154 File Offset: 0x00119354
		private void HandleMouseUp(MouseEventArgs e)
		{
			bool flag = this.ParentInternal.LastMouseDownedItem == this;
			if (!flag && !this.MouseDownAndUpMustBeInSameItem)
			{
				flag = this.ParentInternal.ShouldSelectItem();
			}
			if (this.state[ToolStripItem.stateMouseDownAndNoDrag] || flag)
			{
				this.Push(false);
				if (e.Button == MouseButtons.Left || (e.Button == MouseButtons.Right && this.state[ToolStripItem.stateSupportsRightClick]))
				{
					bool flag2 = false;
					if (this.DoubleClickEnabled)
					{
						long ticks = DateTime.Now.Ticks;
						long num = ticks - this.lastClickTime;
						this.lastClickTime = ticks;
						if (num >= 0L && num < ToolStripItem.DoubleClickTicks)
						{
							flag2 = true;
						}
					}
					if (flag2)
					{
						this.HandleDoubleClick(new EventArgs());
						this.lastClickTime = 0L;
					}
					else
					{
						this.HandleClick(new EventArgs());
					}
				}
				this.OnMouseUp(e);
				this.RaiseMouseEvent(ToolStripItem.EventMouseUp, e);
			}
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnAccessibleDescriptionChanged(EventArgs e)
		{
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnAccessibleNameChanged(EventArgs e)
		{
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnAccessibleDefaultActionDescriptionChanged(EventArgs e)
		{
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnAccessibleRoleChanged(EventArgs e)
		{
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x0011B23F File Offset: 0x0011943F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackColorChanged(EventArgs e)
		{
			this.Invalidate();
			this.RaiseEvent(ToolStripItem.EventBackColorChanged, e);
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x0011B253 File Offset: 0x00119453
		protected virtual void OnBoundsChanged()
		{
			LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
			this.InternalLayout.PerformLayout();
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x0011B271 File Offset: 0x00119471
		protected virtual void OnClick(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventClick, e);
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x000072B6 File Offset: 0x000054B6
		protected internal virtual void OnLayout(LayoutEventArgs e)
		{
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x0011B27F File Offset: 0x0011947F
		void IDropTarget.OnDragEnter(DragEventArgs dragEvent)
		{
			this.OnDragEnter(dragEvent);
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x0011B288 File Offset: 0x00119488
		void IDropTarget.OnDragOver(DragEventArgs dragEvent)
		{
			this.OnDragOver(dragEvent);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x0011B291 File Offset: 0x00119491
		void IDropTarget.OnDragLeave(EventArgs e)
		{
			this.OnDragLeave(e);
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x0011B29A File Offset: 0x0011949A
		void IDropTarget.OnDragDrop(DragEventArgs dragEvent)
		{
			this.OnDragDrop(dragEvent);
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x0011B2A3 File Offset: 0x001194A3
		void ISupportOleDropSource.OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEventArgs)
		{
			this.OnGiveFeedback(giveFeedbackEventArgs);
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x0011B2AC File Offset: 0x001194AC
		void ISupportOleDropSource.OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEventArgs)
		{
			this.OnQueryContinueDrag(queryContinueDragEventArgs);
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x0011B2B8 File Offset: 0x001194B8
		private void OnAnimationFrameChanged(object o, EventArgs e)
		{
			ToolStrip parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				if (parentInternal.Disposing || parentInternal.IsDisposed)
				{
					return;
				}
				if (parentInternal.IsHandleCreated && parentInternal.InvokeRequired)
				{
					parentInternal.BeginInvoke(new EventHandler(this.OnAnimationFrameChanged), new object[]
					{
						o,
						e
					});
					return;
				}
				this.Invalidate();
			}
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x0011B318 File Offset: 0x00119518
		protected virtual void OnAvailableChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventAvailableChanged, e);
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x0011B326 File Offset: 0x00119526
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragEnter(DragEventArgs dragEvent)
		{
			this.RaiseDragEvent(ToolStripItem.EventDragEnter, dragEvent);
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x0011B334 File Offset: 0x00119534
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragOver(DragEventArgs dragEvent)
		{
			this.RaiseDragEvent(ToolStripItem.EventDragOver, dragEvent);
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x0011B342 File Offset: 0x00119542
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragLeave(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventDragLeave, e);
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x0011B350 File Offset: 0x00119550
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragDrop(DragEventArgs dragEvent)
		{
			this.RaiseDragEvent(ToolStripItem.EventDragDrop, dragEvent);
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x0011B35E File Offset: 0x0011955E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDisplayStyleChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventDisplayStyleChanged, e);
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x0011B36C File Offset: 0x0011956C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEvent)
		{
			GiveFeedbackEventHandler giveFeedbackEventHandler = (GiveFeedbackEventHandler)base.Events[ToolStripItem.EventGiveFeedback];
			if (giveFeedbackEventHandler != null)
			{
				giveFeedbackEventHandler(this, giveFeedbackEvent);
			}
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnImageScalingSizeChanged(EventArgs e)
		{
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x0011B39A File Offset: 0x0011959A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEvent)
		{
			this.RaiseQueryContinueDragEvent(ToolStripItem.EventQueryContinueDrag, queryContinueDragEvent);
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x0011B3A8 File Offset: 0x001195A8
		protected virtual void OnDoubleClick(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventDoubleClick, e);
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x0011B3B6 File Offset: 0x001195B6
		protected virtual void OnEnabledChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventEnabledChanged, e);
			this.Animate();
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x0011B3CA File Offset: 0x001195CA
		internal void OnInternalEnabledChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventInternalEnabledChanged, e);
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x0011B3D8 File Offset: 0x001195D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnForeColorChanged(EventArgs e)
		{
			this.Invalidate();
			this.RaiseEvent(ToolStripItem.EventForeColorChanged, e);
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x0011B3EC File Offset: 0x001195EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFontChanged(EventArgs e)
		{
			this.cachedTextSize = Size.Empty;
			if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
			{
				this.InvalidateItemLayout(PropertyNames.Font);
			}
			else
			{
				this.toolStripItemInternalLayout = null;
			}
			this.RaiseEvent(ToolStripItem.EventFontChanged, e);
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x0011B424 File Offset: 0x00119624
		protected virtual void OnLocationChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventLocationChanged, e);
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseEnter(EventArgs e)
		{
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseMove(MouseEventArgs mea)
		{
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x0011B432 File Offset: 0x00119632
		protected virtual void OnMouseHover(EventArgs e)
		{
			if (this.ParentInternal != null && !string.IsNullOrEmpty(this.ToolTipText))
			{
				this.ParentInternal.UpdateToolTip(this);
			}
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x0011B455 File Offset: 0x00119655
		protected virtual void OnMouseLeave(EventArgs e)
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.UpdateToolTip(null);
			}
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseDown(MouseEventArgs e)
		{
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseUp(MouseEventArgs e)
		{
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnPaint(PaintEventArgs e)
		{
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x0011B46C File Offset: 0x0011966C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBackColorChanged(EventArgs e)
		{
			if (this.Properties.GetColor(ToolStripItem.PropBackColor).IsEmpty)
			{
				this.OnBackColorChanged(e);
			}
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x0011B49A File Offset: 0x0011969A
		protected virtual void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
		{
			this.SetAmbientMargin();
			if (oldParent != null && oldParent.DropTargetManager != null)
			{
				oldParent.DropTargetManager.EnsureUnRegistered(this);
			}
			if (this.AllowDrop && newParent != null)
			{
				this.EnsureParentDropTargetRegistered();
			}
			this.Animate();
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x0011B4D0 File Offset: 0x001196D0
		protected internal virtual void OnParentEnabledChanged(EventArgs e)
		{
			this.OnEnabledChanged(EventArgs.Empty);
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x0011B4DD File Offset: 0x001196DD
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void OnOwnerFontChanged(EventArgs e)
		{
			if (this.Properties.GetObject(ToolStripItem.PropFont) == null)
			{
				this.OnFontChanged(e);
			}
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x0011B4F8 File Offset: 0x001196F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentForeColorChanged(EventArgs e)
		{
			if (this.Properties.GetColor(ToolStripItem.PropForeColor).IsEmpty)
			{
				this.OnForeColorChanged(e);
			}
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x0011B526 File Offset: 0x00119726
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void OnParentRightToLeftChanged(EventArgs e)
		{
			if (!this.Properties.ContainsInteger(ToolStripItem.PropRightToLeft) || this.Properties.GetInteger(ToolStripItem.PropRightToLeft) == 2)
			{
				this.OnRightToLeftChanged(e);
			}
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x0011B554 File Offset: 0x00119754
		protected virtual void OnOwnerChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventOwnerChanged, e);
			this.SetAmbientMargin();
			if (this.Owner != null)
			{
				bool flag = false;
				int num = this.Properties.GetInteger(ToolStripItem.PropRightToLeft, out flag);
				if (!flag)
				{
					num = 2;
				}
				if (num == 2 && this.RightToLeft != this.DefaultRightToLeft)
				{
					this.OnRightToLeftChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x0011B5B4 File Offset: 0x001197B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal void OnOwnerTextDirectionChanged()
		{
			ToolStripTextDirection toolStripTextDirection = ToolStripTextDirection.Inherit;
			if (this.Properties.ContainsObject(ToolStripItem.PropTextDirection))
			{
				toolStripTextDirection = (ToolStripTextDirection)this.Properties.GetObject(ToolStripItem.PropTextDirection);
			}
			if (toolStripTextDirection == ToolStripTextDirection.Inherit)
			{
				this.InvalidateItemLayout("TextDirection");
			}
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x0011B5F9 File Offset: 0x001197F9
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			this.InvalidateItemLayout(PropertyNames.RightToLeft);
			this.RaiseEvent(ToolStripItem.EventRightToLeft, e);
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0011B612 File Offset: 0x00119812
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTextChanged(EventArgs e)
		{
			this.cachedTextSize = Size.Empty;
			this.InvalidateItemLayout(PropertyNames.Text);
			this.RaiseEvent(ToolStripItem.EventText, e);
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x0011B638 File Offset: 0x00119838
		protected virtual void OnVisibleChanged(EventArgs e)
		{
			if (this.Owner != null && !this.Owner.IsDisposed && !this.Owner.Disposing)
			{
				this.Owner.OnItemVisibleChanged(new ToolStripItemEventArgs(this), true);
			}
			this.RaiseEvent(ToolStripItem.EventVisibleChanged, e);
			this.Animate();
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x0011B68B File Offset: 0x0011988B
		public void PerformClick()
		{
			if (this.Enabled && this.Available)
			{
				this.FireEvent(ToolStripItemEventType.Click);
			}
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x0011B6A4 File Offset: 0x001198A4
		internal void Push(bool push)
		{
			if (!this.CanSelect || !this.Enabled || base.DesignMode)
			{
				return;
			}
			if (this.state[ToolStripItem.statePressed] != push)
			{
				this.state[ToolStripItem.statePressed] = push;
				if (this.Available)
				{
					this.Invalidate();
				}
			}
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x0011B6FC File Offset: 0x001198FC
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal virtual bool ProcessDialogKey(Keys keyData)
		{
			if (keyData == Keys.Return || (this.state[ToolStripItem.stateSupportsSpaceKey] && keyData == Keys.Space))
			{
				this.FireEvent(ToolStripItemEventType.Click);
				if (this.ParentInternal != null && !this.ParentInternal.IsDropDown && (!AccessibilityImprovements.Level2 || this.Enabled))
				{
					this.ParentInternal.RestoreFocusInternal();
				}
				return true;
			}
			return false;
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x00011A20 File Offset: 0x0000FC20
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal virtual bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return false;
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x0011B75E File Offset: 0x0011995E
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal virtual bool ProcessMnemonic(char charCode)
		{
			this.FireEvent(ToolStripItemEventType.Click);
			return true;
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x0011B768 File Offset: 0x00119968
		internal void RaiseCancelEvent(object key, CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[key];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x0011B794 File Offset: 0x00119994
		internal void RaiseDragEvent(object key, DragEventArgs e)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[key];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, e);
			}
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x0011B7C0 File Offset: 0x001199C0
		internal void RaiseEvent(object key, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[key];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x0011B7EC File Offset: 0x001199EC
		internal void RaiseKeyEvent(object key, KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[key];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x0011B818 File Offset: 0x00119A18
		internal void RaiseKeyPressEvent(object key, KeyPressEventArgs e)
		{
			KeyPressEventHandler keyPressEventHandler = (KeyPressEventHandler)base.Events[key];
			if (keyPressEventHandler != null)
			{
				keyPressEventHandler(this, e);
			}
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x0011B844 File Offset: 0x00119A44
		internal void RaiseMouseEvent(object key, MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[key];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x0011B870 File Offset: 0x00119A70
		internal void RaisePaintEvent(object key, PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[key];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x0011B89C File Offset: 0x00119A9C
		internal void RaiseQueryContinueDragEvent(object key, QueryContinueDragEventArgs e)
		{
			QueryContinueDragEventHandler queryContinueDragEventHandler = (QueryContinueDragEventHandler)base.Events[key];
			if (queryContinueDragEventHandler != null)
			{
				queryContinueDragEventHandler(this, e);
			}
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x0011B8C6 File Offset: 0x00119AC6
		private void ResetToolTipText()
		{
			this.toolTipText = null;
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x0011B8CF File Offset: 0x00119ACF
		internal virtual void ToolStrip_RescaleConstants(int oldDpi, int newDpi)
		{
			this.DeviceDpi = newDpi;
			this.RescaleConstantsInternal(newDpi);
			this.OnFontChanged(EventArgs.Empty);
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x0011B8EA File Offset: 0x00119AEA
		internal void RescaleConstantsInternal(int newDpi)
		{
			ToolStripManager.CurrentDpi = newDpi;
			this.defaultFont = ToolStripManager.DefaultFont;
			this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripItem.defaultMargin, this.deviceDpi);
			this.scaledDefaultStatusStripMargin = DpiHelper.LogicalToDeviceUnits(ToolStripItem.defaultStatusStripMargin, this.deviceDpi);
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x0011B92C File Offset: 0x00119B2C
		public void Select()
		{
			if (!this.CanSelect)
			{
				return;
			}
			if (this.Owner != null && this.Owner.IsCurrentlyDragging)
			{
				return;
			}
			if (this.ParentInternal != null && this.ParentInternal.IsSelectionSuspended)
			{
				return;
			}
			if (!this.Selected)
			{
				this.state[ToolStripItem.stateSelected] = true;
				if (this.ParentInternal != null)
				{
					this.ParentInternal.NotifySelectionChange(this);
				}
				if (this.IsOnDropDown && this.OwnerItem != null && this.OwnerItem.IsOnDropDown)
				{
					this.OwnerItem.Select();
				}
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutGotFocus(this);
				}
				if (AccessibilityImprovements.Level3 && this.AccessibilityObject is ToolStripItem.ToolStripItemAccessibleObject)
				{
					((ToolStripItem.ToolStripItemAccessibleObject)this.AccessibilityObject).RaiseFocusChanged();
				}
			}
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x0011B9FC File Offset: 0x00119BFC
		internal void SetOwner(ToolStrip newOwner)
		{
			if (this.owner != newOwner)
			{
				Font font = this.Font;
				if (this.owner != null)
				{
					ToolStrip toolStrip = this.owner;
					toolStrip.rescaleConstsCallbackDelegate = (Action<int, int>)Delegate.Remove(toolStrip.rescaleConstsCallbackDelegate, new Action<int, int>(this.ToolStrip_RescaleConstants));
				}
				this.owner = newOwner;
				if (this.owner != null)
				{
					ToolStrip toolStrip2 = this.owner;
					toolStrip2.rescaleConstsCallbackDelegate = (Action<int, int>)Delegate.Combine(toolStrip2.rescaleConstsCallbackDelegate, new Action<int, int>(this.ToolStrip_RescaleConstants));
				}
				if (newOwner == null)
				{
					this.ParentInternal = null;
				}
				if (!this.state[ToolStripItem.stateDisposing] && !this.IsDisposed)
				{
					this.OnOwnerChanged(EventArgs.Empty);
					if (font != this.Font)
					{
						this.OnFontChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x0011BAC8 File Offset: 0x00119CC8
		protected virtual void SetVisibleCore(bool visible)
		{
			if (this.state[ToolStripItem.stateVisible] != visible)
			{
				this.state[ToolStripItem.stateVisible] = visible;
				this.Unselect();
				this.Push(false);
				this.OnAvailableChanged(EventArgs.Empty);
				this.OnVisibleChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x0011BB1C File Offset: 0x00119D1C
		protected internal virtual void SetBounds(Rectangle bounds)
		{
			Rectangle right = this.bounds;
			this.bounds = bounds;
			if (!this.state[ToolStripItem.stateContstructing])
			{
				if (this.bounds != right)
				{
					this.OnBoundsChanged();
				}
				if (this.bounds.Location != right.Location)
				{
					this.OnLocationChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x0011BB81 File Offset: 0x00119D81
		internal void SetBounds(int x, int y, int width, int height)
		{
			this.SetBounds(new Rectangle(x, y, width, height));
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x0011BB93 File Offset: 0x00119D93
		internal void SetPlacement(ToolStripItemPlacement placement)
		{
			this.placement = placement;
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x0011BB9C File Offset: 0x00119D9C
		internal void SetAmbientMargin()
		{
			if (this.state[ToolStripItem.stateUseAmbientMargin] && this.Margin != this.DefaultMargin)
			{
				CommonProperties.SetMargin(this, this.DefaultMargin);
			}
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x0011BBCF File Offset: 0x00119DCF
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeImageTransparentColor()
		{
			return this.ImageTransparentColor != Color.Empty;
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0011BBE4 File Offset: 0x00119DE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeBackColor()
		{
			return !this.Properties.GetColor(ToolStripItem.PropBackColor).IsEmpty;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0011BC0C File Offset: 0x00119E0C
		private bool ShouldSerializeDisplayStyle()
		{
			return this.DisplayStyle != this.DefaultDisplayStyle;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0011BC1F File Offset: 0x00119E1F
		private bool ShouldSerializeToolTipText()
		{
			return !string.IsNullOrEmpty(this.toolTipText);
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0011BC30 File Offset: 0x00119E30
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeForeColor()
		{
			return !this.Properties.GetColor(ToolStripItem.PropForeColor).IsEmpty;
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x0011BC58 File Offset: 0x00119E58
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeFont()
		{
			bool flag;
			object @object = this.Properties.GetObject(ToolStripItem.PropFont, out flag);
			return flag && @object != null;
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0011BC81 File Offset: 0x00119E81
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializePadding()
		{
			return this.Padding != this.DefaultPadding;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x0011BC94 File Offset: 0x00119E94
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeMargin()
		{
			return this.Margin != this.DefaultMargin;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x0011BCA7 File Offset: 0x00119EA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeVisible()
		{
			return !this.state[ToolStripItem.stateVisible];
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0011BCBC File Offset: 0x00119EBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeImage()
		{
			return this.Image != null && this.ImageIndexer.ActualIndex < 0;
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x0011BCD6 File Offset: 0x00119ED6
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeImageKey()
		{
			return this.Image != null && this.ImageIndexer.ActualIndex >= 0 && this.ImageIndexer.Key != null && this.ImageIndexer.Key.Length != 0;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x0011BD12 File Offset: 0x00119F12
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeImageIndex()
		{
			return this.Image != null && this.ImageIndexer.ActualIndex >= 0 && this.ImageIndexer.Index != -1;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x0011BD40 File Offset: 0x00119F40
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeRightToLeft()
		{
			bool flag = false;
			int integer = this.Properties.GetInteger(ToolStripItem.PropRightToLeft, out flag);
			return flag && integer != (int)this.DefaultRightToLeft;
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x0011BD74 File Offset: 0x00119F74
		private bool ShouldSerializeTextDirection()
		{
			ToolStripTextDirection toolStripTextDirection = ToolStripTextDirection.Inherit;
			if (this.Properties.ContainsObject(ToolStripItem.PropTextDirection))
			{
				toolStripTextDirection = (ToolStripTextDirection)this.Properties.GetObject(ToolStripItem.PropTextDirection);
			}
			return toolStripTextDirection > ToolStripTextDirection.Inherit;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x0011BDAF File Offset: 0x00119FAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetBackColor()
		{
			this.BackColor = Color.Empty;
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x0011BDBC File Offset: 0x00119FBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetDisplayStyle()
		{
			this.DisplayStyle = this.DefaultDisplayStyle;
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x0011BDCA File Offset: 0x00119FCA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetForeColor()
		{
			this.ForeColor = Color.Empty;
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x0011BDD7 File Offset: 0x00119FD7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetFont()
		{
			this.Font = null;
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x0011BDE0 File Offset: 0x00119FE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetImage()
		{
			this.Image = null;
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x0011BDE9 File Offset: 0x00119FE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		private void ResetImageTransparentColor()
		{
			this.ImageTransparentColor = Color.Empty;
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x0011BDF6 File Offset: 0x00119FF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetMargin()
		{
			this.state[ToolStripItem.stateUseAmbientMargin] = true;
			this.SetAmbientMargin();
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x00037EA3 File Offset: 0x000360A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetPadding()
		{
			CommonProperties.ResetPadding(this);
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x0011BE0F File Offset: 0x0011A00F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetRightToLeft()
		{
			this.RightToLeft = RightToLeft.Inherit;
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x0011BE18 File Offset: 0x0011A018
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetTextDirection()
		{
			this.TextDirection = ToolStripTextDirection.Inherit;
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x0011BE24 File Offset: 0x0011A024
		internal Point TranslatePoint(Point fromPoint, ToolStripPointType fromPointType, ToolStripPointType toPointType)
		{
			ToolStrip toolStrip = this.ParentInternal;
			if (toolStrip == null)
			{
				toolStrip = ((this.IsOnOverflow && this.Owner != null) ? this.Owner.OverflowButton.DropDown : this.Owner);
			}
			if (toolStrip == null)
			{
				return fromPoint;
			}
			if (fromPointType == toPointType)
			{
				return fromPoint;
			}
			Point result = Point.Empty;
			Point location = this.Bounds.Location;
			if (fromPointType == ToolStripPointType.ScreenCoords)
			{
				result = toolStrip.PointToClient(fromPoint);
				if (toPointType == ToolStripPointType.ToolStripItemCoords)
				{
					result.X += location.X;
					result.Y += location.Y;
				}
			}
			else
			{
				if (fromPointType == ToolStripPointType.ToolStripItemCoords)
				{
					fromPoint.X += location.X;
					fromPoint.Y += location.Y;
				}
				if (toPointType == ToolStripPointType.ScreenCoords)
				{
					result = toolStrip.PointToScreen(fromPoint);
				}
				else if (toPointType == ToolStripPointType.ToolStripItemCoords)
				{
					fromPoint.X -= location.X;
					fromPoint.Y -= location.Y;
					result = fromPoint;
				}
				else
				{
					result = fromPoint;
				}
			}
			return result;
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x0011BF34 File Offset: 0x0011A134
		internal ToolStrip RootToolStrip
		{
			get
			{
				ToolStripItem toolStripItem = this;
				while (toolStripItem.OwnerItem != null)
				{
					toolStripItem = toolStripItem.OwnerItem;
				}
				return toolStripItem.ParentInternal;
			}
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x0011BF5A File Offset: 0x0011A15A
		public override string ToString()
		{
			if (this.Text != null && this.Text.Length != 0)
			{
				return this.Text;
			}
			return base.ToString();
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x0011BF80 File Offset: 0x0011A180
		internal void Unselect()
		{
			if (this.state[ToolStripItem.stateSelected])
			{
				this.state[ToolStripItem.stateSelected] = false;
				if (this.Available)
				{
					this.Invalidate();
					if (this.ParentInternal != null)
					{
						this.ParentInternal.NotifySelectionChange(this);
					}
					if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
					{
						KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
					}
				}
			}
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x0011BFE4 File Offset: 0x0011A1E4
		bool IKeyboardToolTip.CanShowToolTipsNow()
		{
			return this.Visible && this.parent != null && ((IKeyboardToolTip)this.parent).AllowsChildrenToShowToolTips();
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x0011C003 File Offset: 0x0011A203
		Rectangle IKeyboardToolTip.GetNativeScreenRectangle()
		{
			return this.AccessibilityObject.Bounds;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0011C010 File Offset: 0x0011A210
		IList<Rectangle> IKeyboardToolTip.GetNeighboringToolsRectangles()
		{
			List<Rectangle> list = new List<Rectangle>(3);
			if (this.parent != null)
			{
				ToolStripItemCollection displayedItems = this.parent.DisplayedItems;
				int num = 0;
				int count = displayedItems.Count;
				bool flag = false;
				while (!flag && num < count)
				{
					flag = (displayedItems[num] == this);
					if (flag)
					{
						int num2 = num - 1;
						if (num2 >= 0)
						{
							list.Add(((IKeyboardToolTip)displayedItems[num2]).GetNativeScreenRectangle());
						}
						int num3 = num + 1;
						if (num3 < count)
						{
							list.Add(((IKeyboardToolTip)displayedItems[num3]).GetNativeScreenRectangle());
						}
					}
					else
					{
						num++;
					}
				}
			}
			ToolStripDropDown toolStripDropDown = this.parent as ToolStripDropDown;
			if (toolStripDropDown != null && toolStripDropDown.OwnerItem != null)
			{
				list.Add(((IKeyboardToolTip)toolStripDropDown.OwnerItem).GetNativeScreenRectangle());
			}
			return list;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0011C0CC File Offset: 0x0011A2CC
		bool IKeyboardToolTip.IsHoveredWithMouse()
		{
			return ((IKeyboardToolTip)this).GetNativeScreenRectangle().Contains(Control.MousePosition);
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0011C0EC File Offset: 0x0011A2EC
		bool IKeyboardToolTip.HasRtlModeEnabled()
		{
			return this.parent != null && ((IKeyboardToolTip)this.parent).HasRtlModeEnabled();
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x00013062 File Offset: 0x00011262
		bool IKeyboardToolTip.AllowsToolTip()
		{
			return true;
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x0011A07C File Offset: 0x0011827C
		IWin32Window IKeyboardToolTip.GetOwnerWindow()
		{
			return this.ParentInternal;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x0011C103 File Offset: 0x0011A303
		void IKeyboardToolTip.OnHooked(ToolTip toolTip)
		{
			this.OnKeyboardToolTipHook(toolTip);
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x0011C10C File Offset: 0x0011A30C
		void IKeyboardToolTip.OnUnhooked(ToolTip toolTip)
		{
			this.OnKeyboardToolTipUnhook(toolTip);
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x0011C115 File Offset: 0x0011A315
		string IKeyboardToolTip.GetCaptionForTool(ToolTip toolTip)
		{
			return this.ToolTipText;
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x00013062 File Offset: 0x00011262
		bool IKeyboardToolTip.ShowsOwnToolTip()
		{
			return true;
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x0011C11D File Offset: 0x0011A31D
		bool IKeyboardToolTip.IsBeingTabbedTo()
		{
			return this.IsBeingTabbedTo();
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x00013062 File Offset: 0x00011262
		bool IKeyboardToolTip.AllowsChildrenToShowToolTips()
		{
			return true;
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnKeyboardToolTipHook(ToolTip toolTip)
		{
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnKeyboardToolTipUnhook(ToolTip toolTip)
		{
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x0003C20F File Offset: 0x0003A40F
		internal virtual bool IsBeingTabbedTo()
		{
			return Control.AreCommonNavigationalKeysDown();
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x0011C125 File Offset: 0x0011A325
		internal static bool GetIsOffscreenPropertyValue(ToolStripItemPlacement toolStripItemPlacement, Rectangle bounds)
		{
			return toolStripItemPlacement != ToolStripItemPlacement.Main || bounds.Height <= 0 || bounds.Width <= 0;
		}

		// Token: 0x04002530 RID: 9520
		internal static readonly TraceSwitch MouseDebugging;

		// Token: 0x04002531 RID: 9521
		private Rectangle bounds = Rectangle.Empty;

		// Token: 0x04002532 RID: 9522
		private PropertyStore propertyStore;

		// Token: 0x04002533 RID: 9523
		private ToolStripItemAlignment alignment;

		// Token: 0x04002534 RID: 9524
		private ToolStrip parent;

		// Token: 0x04002535 RID: 9525
		private ToolStrip owner;

		// Token: 0x04002536 RID: 9526
		private ToolStripItemOverflow overflow = ToolStripItemOverflow.AsNeeded;

		// Token: 0x04002537 RID: 9527
		private ToolStripItemPlacement placement = ToolStripItemPlacement.None;

		// Token: 0x04002538 RID: 9528
		private ContentAlignment imageAlign = ContentAlignment.MiddleCenter;

		// Token: 0x04002539 RID: 9529
		private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

		// Token: 0x0400253A RID: 9530
		private TextImageRelation textImageRelation = TextImageRelation.ImageBeforeText;

		// Token: 0x0400253B RID: 9531
		private ToolStripItemImageIndexer imageIndexer;

		// Token: 0x0400253C RID: 9532
		private ToolStripItemInternalLayout toolStripItemInternalLayout;

		// Token: 0x0400253D RID: 9533
		private BitVector32 state;

		// Token: 0x0400253E RID: 9534
		private string toolTipText;

		// Token: 0x0400253F RID: 9535
		private Color imageTransparentColor = Color.Empty;

		// Token: 0x04002540 RID: 9536
		private ToolStripItemImageScaling imageScaling = ToolStripItemImageScaling.SizeToFit;

		// Token: 0x04002541 RID: 9537
		private Size cachedTextSize = Size.Empty;

		// Token: 0x04002542 RID: 9538
		private static readonly Padding defaultMargin = new Padding(0, 1, 0, 2);

		// Token: 0x04002543 RID: 9539
		private static readonly Padding defaultStatusStripMargin = new Padding(0, 2, 0, 0);

		// Token: 0x04002544 RID: 9540
		private Padding scaledDefaultMargin = ToolStripItem.defaultMargin;

		// Token: 0x04002545 RID: 9541
		private Padding scaledDefaultStatusStripMargin = ToolStripItem.defaultStatusStripMargin;

		// Token: 0x04002546 RID: 9542
		private ToolStripItemDisplayStyle displayStyle = ToolStripItemDisplayStyle.ImageAndText;

		// Token: 0x04002547 RID: 9543
		private static readonly ArrangedElementCollection EmptyChildCollection = new ArrangedElementCollection();

		// Token: 0x04002548 RID: 9544
		internal static readonly object EventMouseDown = new object();

		// Token: 0x04002549 RID: 9545
		internal static readonly object EventMouseEnter = new object();

		// Token: 0x0400254A RID: 9546
		internal static readonly object EventMouseLeave = new object();

		// Token: 0x0400254B RID: 9547
		internal static readonly object EventMouseHover = new object();

		// Token: 0x0400254C RID: 9548
		internal static readonly object EventMouseMove = new object();

		// Token: 0x0400254D RID: 9549
		internal static readonly object EventMouseUp = new object();

		// Token: 0x0400254E RID: 9550
		internal static readonly object EventMouseWheel = new object();

		// Token: 0x0400254F RID: 9551
		internal static readonly object EventClick = new object();

		// Token: 0x04002550 RID: 9552
		internal static readonly object EventDoubleClick = new object();

		// Token: 0x04002551 RID: 9553
		internal static readonly object EventDragDrop = new object();

		// Token: 0x04002552 RID: 9554
		internal static readonly object EventDragEnter = new object();

		// Token: 0x04002553 RID: 9555
		internal static readonly object EventDragLeave = new object();

		// Token: 0x04002554 RID: 9556
		internal static readonly object EventDragOver = new object();

		// Token: 0x04002555 RID: 9557
		internal static readonly object EventDisplayStyleChanged = new object();

		// Token: 0x04002556 RID: 9558
		internal static readonly object EventEnabledChanged = new object();

		// Token: 0x04002557 RID: 9559
		internal static readonly object EventInternalEnabledChanged = new object();

		// Token: 0x04002558 RID: 9560
		internal static readonly object EventFontChanged = new object();

		// Token: 0x04002559 RID: 9561
		internal static readonly object EventForeColorChanged = new object();

		// Token: 0x0400255A RID: 9562
		internal static readonly object EventBackColorChanged = new object();

		// Token: 0x0400255B RID: 9563
		internal static readonly object EventGiveFeedback = new object();

		// Token: 0x0400255C RID: 9564
		internal static readonly object EventQueryContinueDrag = new object();

		// Token: 0x0400255D RID: 9565
		internal static readonly object EventQueryAccessibilityHelp = new object();

		// Token: 0x0400255E RID: 9566
		internal static readonly object EventMove = new object();

		// Token: 0x0400255F RID: 9567
		internal static readonly object EventResize = new object();

		// Token: 0x04002560 RID: 9568
		internal static readonly object EventLayout = new object();

		// Token: 0x04002561 RID: 9569
		internal static readonly object EventLocationChanged = new object();

		// Token: 0x04002562 RID: 9570
		internal static readonly object EventRightToLeft = new object();

		// Token: 0x04002563 RID: 9571
		internal static readonly object EventVisibleChanged = new object();

		// Token: 0x04002564 RID: 9572
		internal static readonly object EventAvailableChanged = new object();

		// Token: 0x04002565 RID: 9573
		internal static readonly object EventOwnerChanged = new object();

		// Token: 0x04002566 RID: 9574
		internal static readonly object EventPaint = new object();

		// Token: 0x04002567 RID: 9575
		internal static readonly object EventText = new object();

		// Token: 0x04002568 RID: 9576
		internal static readonly object EventSelectedChanged = new object();

		// Token: 0x04002569 RID: 9577
		private static readonly int PropName = PropertyStore.CreateKey();

		// Token: 0x0400256A RID: 9578
		private static readonly int PropText = PropertyStore.CreateKey();

		// Token: 0x0400256B RID: 9579
		private static readonly int PropBackColor = PropertyStore.CreateKey();

		// Token: 0x0400256C RID: 9580
		private static readonly int PropForeColor = PropertyStore.CreateKey();

		// Token: 0x0400256D RID: 9581
		private static readonly int PropImage = PropertyStore.CreateKey();

		// Token: 0x0400256E RID: 9582
		private static readonly int PropFont = PropertyStore.CreateKey();

		// Token: 0x0400256F RID: 9583
		private static readonly int PropRightToLeft = PropertyStore.CreateKey();

		// Token: 0x04002570 RID: 9584
		private static readonly int PropTag = PropertyStore.CreateKey();

		// Token: 0x04002571 RID: 9585
		private static readonly int PropAccessibility = PropertyStore.CreateKey();

		// Token: 0x04002572 RID: 9586
		private static readonly int PropAccessibleName = PropertyStore.CreateKey();

		// Token: 0x04002573 RID: 9587
		private static readonly int PropAccessibleRole = PropertyStore.CreateKey();

		// Token: 0x04002574 RID: 9588
		private static readonly int PropAccessibleHelpProvider = PropertyStore.CreateKey();

		// Token: 0x04002575 RID: 9589
		private static readonly int PropAccessibleDefaultActionDescription = PropertyStore.CreateKey();

		// Token: 0x04002576 RID: 9590
		private static readonly int PropAccessibleDescription = PropertyStore.CreateKey();

		// Token: 0x04002577 RID: 9591
		private static readonly int PropTextDirection = PropertyStore.CreateKey();

		// Token: 0x04002578 RID: 9592
		private static readonly int PropMirroredImage = PropertyStore.CreateKey();

		// Token: 0x04002579 RID: 9593
		private static readonly int PropBackgroundImage = PropertyStore.CreateKey();

		// Token: 0x0400257A RID: 9594
		private static readonly int PropBackgroundImageLayout = PropertyStore.CreateKey();

		// Token: 0x0400257B RID: 9595
		private static readonly int PropMergeAction = PropertyStore.CreateKey();

		// Token: 0x0400257C RID: 9596
		private static readonly int PropMergeIndex = PropertyStore.CreateKey();

		// Token: 0x0400257D RID: 9597
		private static readonly int stateAllowDrop = BitVector32.CreateMask();

		// Token: 0x0400257E RID: 9598
		private static readonly int stateVisible = BitVector32.CreateMask(ToolStripItem.stateAllowDrop);

		// Token: 0x0400257F RID: 9599
		private static readonly int stateEnabled = BitVector32.CreateMask(ToolStripItem.stateVisible);

		// Token: 0x04002580 RID: 9600
		private static readonly int stateMouseDownAndNoDrag = BitVector32.CreateMask(ToolStripItem.stateEnabled);

		// Token: 0x04002581 RID: 9601
		private static readonly int stateAutoSize = BitVector32.CreateMask(ToolStripItem.stateMouseDownAndNoDrag);

		// Token: 0x04002582 RID: 9602
		private static readonly int statePressed = BitVector32.CreateMask(ToolStripItem.stateAutoSize);

		// Token: 0x04002583 RID: 9603
		private static readonly int stateSelected = BitVector32.CreateMask(ToolStripItem.statePressed);

		// Token: 0x04002584 RID: 9604
		private static readonly int stateContstructing = BitVector32.CreateMask(ToolStripItem.stateSelected);

		// Token: 0x04002585 RID: 9605
		private static readonly int stateDisposed = BitVector32.CreateMask(ToolStripItem.stateContstructing);

		// Token: 0x04002586 RID: 9606
		private static readonly int stateCurrentlyAnimatingImage = BitVector32.CreateMask(ToolStripItem.stateDisposed);

		// Token: 0x04002587 RID: 9607
		private static readonly int stateDoubleClickEnabled = BitVector32.CreateMask(ToolStripItem.stateCurrentlyAnimatingImage);

		// Token: 0x04002588 RID: 9608
		private static readonly int stateAutoToolTip = BitVector32.CreateMask(ToolStripItem.stateDoubleClickEnabled);

		// Token: 0x04002589 RID: 9609
		private static readonly int stateSupportsRightClick = BitVector32.CreateMask(ToolStripItem.stateAutoToolTip);

		// Token: 0x0400258A RID: 9610
		private static readonly int stateSupportsItemClick = BitVector32.CreateMask(ToolStripItem.stateSupportsRightClick);

		// Token: 0x0400258B RID: 9611
		private static readonly int stateRightToLeftAutoMirrorImage = BitVector32.CreateMask(ToolStripItem.stateSupportsItemClick);

		// Token: 0x0400258C RID: 9612
		private static readonly int stateInvalidMirroredImage = BitVector32.CreateMask(ToolStripItem.stateRightToLeftAutoMirrorImage);

		// Token: 0x0400258D RID: 9613
		private static readonly int stateSupportsSpaceKey = BitVector32.CreateMask(ToolStripItem.stateInvalidMirroredImage);

		// Token: 0x0400258E RID: 9614
		private static readonly int stateMouseDownAndUpMustBeInSameItem = BitVector32.CreateMask(ToolStripItem.stateSupportsSpaceKey);

		// Token: 0x0400258F RID: 9615
		private static readonly int stateSupportsDisabledHotTracking = BitVector32.CreateMask(ToolStripItem.stateMouseDownAndUpMustBeInSameItem);

		// Token: 0x04002590 RID: 9616
		private static readonly int stateUseAmbientMargin = BitVector32.CreateMask(ToolStripItem.stateSupportsDisabledHotTracking);

		// Token: 0x04002591 RID: 9617
		private static readonly int stateDisposing = BitVector32.CreateMask(ToolStripItem.stateUseAmbientMargin);

		// Token: 0x04002592 RID: 9618
		private long lastClickTime;

		// Token: 0x04002593 RID: 9619
		private int deviceDpi = DpiHelper.DeviceDpi;

		// Token: 0x04002594 RID: 9620
		internal Font defaultFont = ToolStripManager.DefaultFont;

		// Token: 0x02000805 RID: 2053
		[ComVisible(true)]
		public class ToolStripItemAccessibleObject : AccessibleObject
		{
			// Token: 0x06006EF6 RID: 28406 RVA: 0x00196E08 File Offset: 0x00195008
			public ToolStripItemAccessibleObject(ToolStripItem ownerItem)
			{
				if (ownerItem == null)
				{
					throw new ArgumentNullException("ownerItem");
				}
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006EF7 RID: 28407 RVA: 0x00196E25 File Offset: 0x00195025
			internal virtual void ClearOwnerItem()
			{
				this.ownerItem = null;
			}

			// Token: 0x06006EF8 RID: 28408 RVA: 0x00196E2E File Offset: 0x0019502E
			internal bool IsOwnerItemCleared()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerItem == null;
			}

			// Token: 0x1700183C RID: 6204
			// (get) Token: 0x06006EF9 RID: 28409 RVA: 0x00196E44 File Offset: 0x00195044
			public override string DefaultAction
			{
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return string.Empty;
					}
					string accessibleDefaultActionDescription = this.ownerItem.AccessibleDefaultActionDescription;
					if (accessibleDefaultActionDescription != null)
					{
						return accessibleDefaultActionDescription;
					}
					return SR.GetString("AccessibleActionPress");
				}
			}

			// Token: 0x1700183D RID: 6205
			// (get) Token: 0x06006EFA RID: 28410 RVA: 0x00196E7C File Offset: 0x0019507C
			public override string Description
			{
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return string.Empty;
					}
					string accessibleDescription = this.ownerItem.AccessibleDescription;
					if (accessibleDescription != null)
					{
						return accessibleDescription;
					}
					return base.Description;
				}
			}

			// Token: 0x1700183E RID: 6206
			// (get) Token: 0x06006EFB RID: 28411 RVA: 0x00196EB0 File Offset: 0x001950B0
			public override string Help
			{
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return string.Empty;
					}
					QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)this.Owner.Events[ToolStripItem.EventQueryAccessibilityHelp];
					if (queryAccessibilityHelpEventHandler != null)
					{
						QueryAccessibilityHelpEventArgs queryAccessibilityHelpEventArgs = new QueryAccessibilityHelpEventArgs();
						queryAccessibilityHelpEventHandler(this.Owner, queryAccessibilityHelpEventArgs);
						return queryAccessibilityHelpEventArgs.HelpString;
					}
					return base.Help;
				}
			}

			// Token: 0x06006EFC RID: 28412 RVA: 0x00196F09 File Offset: 0x00195109
			internal override bool IsPatternSupported(int patternId)
			{
				return !this.IsOwnerItemCleared() && ((AccessibilityImprovements.Level3 && patternId == 10018) || base.IsPatternSupported(patternId));
			}

			// Token: 0x1700183F RID: 6207
			// (get) Token: 0x06006EFD RID: 28413 RVA: 0x00196F30 File Offset: 0x00195130
			public override string KeyboardShortcut
			{
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return string.Empty;
					}
					char mnemonic = WindowsFormsUtils.GetMnemonic(this.ownerItem.Text, false);
					if (this.ownerItem.IsOnDropDown)
					{
						if (mnemonic != '\0')
						{
							return mnemonic.ToString();
						}
						return string.Empty;
					}
					else
					{
						if (mnemonic != '\0')
						{
							return "Alt+" + mnemonic.ToString();
						}
						return string.Empty;
					}
				}
			}

			// Token: 0x17001840 RID: 6208
			// (get) Token: 0x06006EFE RID: 28414 RVA: 0x00196F98 File Offset: 0x00195198
			internal override int[] RuntimeId
			{
				get
				{
					if (AccessibilityImprovements.Level1)
					{
						if (this.runtimeId == null)
						{
							this.runtimeId = new int[2];
							this.runtimeId[0] = (AccessibilityImprovements.Level3 ? 3 : 42);
							this.runtimeId[1] = (this.IsOwnerItemCleared() ? 0 : this.ownerItem.GetHashCode());
						}
						return this.runtimeId;
					}
					return base.RuntimeId;
				}
			}

			// Token: 0x06006EFF RID: 28415 RVA: 0x00197000 File Offset: 0x00195200
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level1)
				{
					if (propertyID == 30005)
					{
						return this.Name;
					}
					if (propertyID == 30028)
					{
						return this.IsPatternSupported(10005);
					}
				}
				if (AccessibilityImprovements.Level3)
				{
					switch (propertyID)
					{
					case 30007:
						return this.KeyboardShortcut;
					case 30008:
						return !this.IsOwnerItemCleared() && this.ownerItem.Selected;
					case 30009:
						return !this.IsOwnerItemCleared() && this.ownerItem.CanSelect;
					case 30010:
						return !this.IsOwnerItemCleared() && this.ownerItem.Enabled;
					case 30011:
					case 30012:
						break;
					case 30013:
						return this.Help ?? string.Empty;
					default:
						if (propertyID == 30019)
						{
							return false;
						}
						if (propertyID == 30022)
						{
							return this.IsOwnerItemCleared() || ((!AccessibilityImprovements.Level5) ? (this.ownerItem.Placement > ToolStripItemPlacement.Main) : ToolStripItem.GetIsOffscreenPropertyValue(this.ownerItem.Placement, this.Bounds));
						}
						break;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001841 RID: 6209
			// (get) Token: 0x06006F00 RID: 28416 RVA: 0x00197140 File Offset: 0x00195340
			// (set) Token: 0x06006F01 RID: 28417 RVA: 0x00197190 File Offset: 0x00195390
			public override string Name
			{
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return string.Empty;
					}
					string accessibleName = this.ownerItem.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					string name = base.Name;
					if (name == null || name.Length == 0)
					{
						return WindowsFormsUtils.TextWithoutMnemonics(this.ownerItem.Text);
					}
					return name;
				}
				set
				{
					if (this.IsOwnerItemCleared())
					{
						return;
					}
					this.ownerItem.AccessibleName = value;
				}
			}

			// Token: 0x17001842 RID: 6210
			// (get) Token: 0x06006F02 RID: 28418 RVA: 0x001971A7 File Offset: 0x001953A7
			internal ToolStripItem Owner
			{
				get
				{
					return this.ownerItem;
				}
			}

			// Token: 0x17001843 RID: 6211
			// (get) Token: 0x06006F03 RID: 28419 RVA: 0x001971B0 File Offset: 0x001953B0
			public override AccessibleRole Role
			{
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return AccessibleRole.PushButton;
					}
					AccessibleRole accessibleRole = this.ownerItem.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.PushButton;
				}
			}

			// Token: 0x17001844 RID: 6212
			// (get) Token: 0x06006F04 RID: 28420 RVA: 0x001971DC File Offset: 0x001953DC
			public override AccessibleStates State
			{
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return AccessibleStates.None;
					}
					if (!this.ownerItem.CanSelect)
					{
						return base.State | this.additionalState;
					}
					if (this.ownerItem.Enabled)
					{
						AccessibleStates accessibleStates = AccessibleStates.Focusable | this.additionalState;
						if (this.ownerItem.Selected || this.ownerItem.Pressed)
						{
							accessibleStates |= (AccessibleStates.Focused | AccessibleStates.HotTracked);
						}
						if (this.ownerItem.Pressed)
						{
							accessibleStates |= AccessibleStates.Pressed;
						}
						return accessibleStates;
					}
					if (AccessibilityImprovements.Level2 && this.ownerItem.Selected && this.ownerItem is ToolStripMenuItem)
					{
						return AccessibleStates.Unavailable | this.additionalState | AccessibleStates.Focused;
					}
					if (AccessibilityImprovements.Level1 && this.ownerItem.Selected && this.ownerItem is ToolStripMenuItem)
					{
						return AccessibleStates.Focused;
					}
					return AccessibleStates.Unavailable | this.additionalState;
				}
			}

			// Token: 0x06006F05 RID: 28421 RVA: 0x001972B4 File Offset: 0x001954B4
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (this.Owner != null)
				{
					this.Owner.PerformClick();
				}
			}

			// Token: 0x06006F06 RID: 28422 RVA: 0x001972CC File Offset: 0x001954CC
			public override int GetHelpTopic(out string fileName)
			{
				fileName = null;
				if (this.IsOwnerItemCleared())
				{
					return 0;
				}
				int result = 0;
				QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)this.Owner.Events[ToolStripItem.EventQueryAccessibilityHelp];
				if (queryAccessibilityHelpEventHandler != null)
				{
					QueryAccessibilityHelpEventArgs queryAccessibilityHelpEventArgs = new QueryAccessibilityHelpEventArgs();
					queryAccessibilityHelpEventHandler(this.Owner, queryAccessibilityHelpEventArgs);
					fileName = queryAccessibilityHelpEventArgs.HelpNamespace;
					if (fileName != null && fileName.Length > 0)
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.PathDiscovery, fileName);
					}
					try
					{
						result = int.Parse(queryAccessibilityHelpEventArgs.HelpKeyword, CultureInfo.InvariantCulture);
					}
					catch
					{
					}
					return result;
				}
				return base.GetHelpTopic(out fileName);
			}

			// Token: 0x06006F07 RID: 28423 RVA: 0x00197368 File Offset: 0x00195568
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (this.IsOwnerItemCleared())
				{
					return null;
				}
				ToolStripItem toolStripItem = null;
				if (this.Owner != null)
				{
					ToolStrip parentInternal = this.Owner.ParentInternal;
					if (parentInternal == null)
					{
						return null;
					}
					bool flag = parentInternal.RightToLeft == RightToLeft.No;
					switch (navigationDirection)
					{
					case AccessibleNavigation.Up:
						toolStripItem = (this.Owner.IsOnDropDown ? parentInternal.GetNextItem(this.Owner, ArrowDirection.Up) : parentInternal.GetNextItem(this.Owner, ArrowDirection.Left, true));
						break;
					case AccessibleNavigation.Down:
						toolStripItem = (this.Owner.IsOnDropDown ? parentInternal.GetNextItem(this.Owner, ArrowDirection.Down) : parentInternal.GetNextItem(this.Owner, ArrowDirection.Right, true));
						break;
					case AccessibleNavigation.Left:
					case AccessibleNavigation.Previous:
						toolStripItem = parentInternal.GetNextItem(this.Owner, ArrowDirection.Left, true);
						break;
					case AccessibleNavigation.Right:
					case AccessibleNavigation.Next:
						toolStripItem = parentInternal.GetNextItem(this.Owner, ArrowDirection.Right, true);
						break;
					case AccessibleNavigation.FirstChild:
						toolStripItem = parentInternal.GetNextItem(null, ArrowDirection.Right, true);
						break;
					case AccessibleNavigation.LastChild:
						toolStripItem = parentInternal.GetNextItem(null, ArrowDirection.Left, true);
						break;
					}
				}
				if (toolStripItem != null)
				{
					return toolStripItem.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x06006F08 RID: 28424 RVA: 0x00197478 File Offset: 0x00195678
			public void AddState(AccessibleStates state)
			{
				if (state == AccessibleStates.None)
				{
					this.additionalState = state;
					return;
				}
				this.additionalState |= state;
			}

			// Token: 0x06006F09 RID: 28425 RVA: 0x00197493 File Offset: 0x00195693
			public override string ToString()
			{
				if (this.Owner != null)
				{
					return "ToolStripItemAccessibleObject: Owner = " + this.Owner.ToString();
				}
				return "ToolStripItemAccessibleObject: Owner = null";
			}

			// Token: 0x17001845 RID: 6213
			// (get) Token: 0x06006F0A RID: 28426 RVA: 0x001974B8 File Offset: 0x001956B8
			public override Rectangle Bounds
			{
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return Rectangle.Empty;
					}
					Rectangle bounds = this.Owner.Bounds;
					if (this.Owner.ParentInternal != null && this.Owner.ParentInternal.Visible)
					{
						return new Rectangle(this.Owner.ParentInternal.PointToScreen(bounds.Location), bounds.Size);
					}
					return Rectangle.Empty;
				}
			}

			// Token: 0x17001846 RID: 6214
			// (get) Token: 0x06006F0B RID: 28427 RVA: 0x00197528 File Offset: 0x00195728
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return null;
					}
					if (this.Owner.IsOnDropDown)
					{
						ToolStripDropDown currentParentDropDown = this.Owner.GetCurrentParentDropDown();
						if (currentParentDropDown.OwnerItem != null)
						{
							return currentParentDropDown.OwnerItem.AccessibilityObject;
						}
						return currentParentDropDown.AccessibilityObject;
					}
					else
					{
						if (this.Owner.Parent == null)
						{
							return base.Parent;
						}
						return this.Owner.Parent.AccessibilityObject;
					}
				}
			}

			// Token: 0x17001847 RID: 6215
			// (get) Token: 0x06006F0C RID: 28428 RVA: 0x00197597 File Offset: 0x00195797
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (this.IsOwnerItemCleared())
					{
						return null;
					}
					ToolStrip rootToolStrip = this.ownerItem.RootToolStrip;
					if (rootToolStrip == null)
					{
						return null;
					}
					return rootToolStrip.AccessibilityObject;
				}
			}

			// Token: 0x06006F0D RID: 28429 RVA: 0x001975BC File Offset: 0x001957BC
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.IsOwnerItemCleared())
				{
					return null;
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.Parent)
				{
					return this.Parent;
				}
				if (direction - UnsafeNativeMethods.NavigateDirection.NextSibling > 1)
				{
					return base.FragmentNavigate(direction);
				}
				int num = this.GetChildFragmentIndex();
				if (num == -1)
				{
					return null;
				}
				int num2 = (direction == UnsafeNativeMethods.NavigateDirection.NextSibling) ? 1 : -1;
				AccessibleObject accessibleObject = null;
				if (AccessibilityImprovements.Level3)
				{
					num += num2;
					int childFragmentCount = this.GetChildFragmentCount();
					if (num >= 0 && num < childFragmentCount)
					{
						accessibleObject = this.GetChildFragment(num, direction);
					}
				}
				else
				{
					do
					{
						num += num2;
						accessibleObject = ((num >= 0 && num < this.Parent.GetChildCount()) ? this.Parent.GetChild(num) : null);
					}
					while (accessibleObject != null && accessibleObject is Control.ControlAccessibleObject);
				}
				return accessibleObject;
			}

			// Token: 0x06006F0E RID: 28430 RVA: 0x0019765C File Offset: 0x0019585C
			private AccessibleObject GetChildFragment(int index, UnsafeNativeMethods.NavigateDirection direction = UnsafeNativeMethods.NavigateDirection.NextSibling)
			{
				ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject = this.Parent as ToolStrip.ToolStripAccessibleObject;
				if (toolStripAccessibleObject != null)
				{
					return toolStripAccessibleObject.GetChildFragment(index, false, direction);
				}
				ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject toolStripOverflowButtonAccessibleObject = this.Parent as ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject;
				if (toolStripOverflowButtonAccessibleObject != null)
				{
					ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject2 = toolStripOverflowButtonAccessibleObject.Parent as ToolStrip.ToolStripAccessibleObject;
					if (toolStripAccessibleObject2 != null)
					{
						return toolStripAccessibleObject2.GetChildFragment(index, true, direction);
					}
				}
				ToolStripDropDownItemAccessibleObject toolStripDropDownItemAccessibleObject = this.Parent as ToolStripDropDownItemAccessibleObject;
				if (toolStripDropDownItemAccessibleObject != null)
				{
					return toolStripDropDownItemAccessibleObject.GetChildFragment(index, direction);
				}
				return null;
			}

			// Token: 0x06006F0F RID: 28431 RVA: 0x001976C4 File Offset: 0x001958C4
			private int GetChildFragmentCount()
			{
				ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject = this.Parent as ToolStrip.ToolStripAccessibleObject;
				if (toolStripAccessibleObject != null)
				{
					return toolStripAccessibleObject.GetChildFragmentCount();
				}
				ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject toolStripOverflowButtonAccessibleObject = this.Parent as ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject;
				if (toolStripOverflowButtonAccessibleObject != null)
				{
					ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject2 = toolStripOverflowButtonAccessibleObject.Parent as ToolStrip.ToolStripAccessibleObject;
					if (toolStripAccessibleObject2 != null)
					{
						return toolStripAccessibleObject2.GetChildOverflowFragmentCount();
					}
				}
				ToolStripDropDownItemAccessibleObject toolStripDropDownItemAccessibleObject = this.Parent as ToolStripDropDownItemAccessibleObject;
				if (toolStripDropDownItemAccessibleObject != null)
				{
					return toolStripDropDownItemAccessibleObject.GetChildCount();
				}
				return -1;
			}

			// Token: 0x06006F10 RID: 28432 RVA: 0x00197724 File Offset: 0x00195924
			private int GetChildFragmentIndex()
			{
				ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject = this.Parent as ToolStrip.ToolStripAccessibleObject;
				if (toolStripAccessibleObject != null)
				{
					return toolStripAccessibleObject.GetChildFragmentIndex(this);
				}
				ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject toolStripOverflowButtonAccessibleObject = this.Parent as ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject;
				if (toolStripOverflowButtonAccessibleObject != null)
				{
					ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject2 = toolStripOverflowButtonAccessibleObject.Parent as ToolStrip.ToolStripAccessibleObject;
					if (toolStripAccessibleObject2 != null)
					{
						return toolStripAccessibleObject2.GetChildFragmentIndex(this);
					}
				}
				ToolStripDropDownItemAccessibleObject toolStripDropDownItemAccessibleObject = this.Parent as ToolStripDropDownItemAccessibleObject;
				if (toolStripDropDownItemAccessibleObject != null)
				{
					return toolStripDropDownItemAccessibleObject.GetChildFragmentIndex(this);
				}
				return -1;
			}

			// Token: 0x06006F11 RID: 28433 RVA: 0x00197786 File Offset: 0x00195986
			internal override void SetFocus()
			{
				if (this.IsOwnerItemCleared())
				{
					return;
				}
				this.Owner.Select();
			}

			// Token: 0x06006F12 RID: 28434 RVA: 0x0019779C File Offset: 0x0019599C
			internal void RaiseFocusChanged()
			{
				if (this.IsOwnerItemCleared())
				{
					return;
				}
				ToolStrip rootToolStrip = this.ownerItem.RootToolStrip;
				if (rootToolStrip != null && rootToolStrip.SupportsUiaProviders)
				{
					base.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x04004303 RID: 17155
			private ToolStripItem ownerItem;

			// Token: 0x04004304 RID: 17156
			private AccessibleStates additionalState;

			// Token: 0x04004305 RID: 17157
			private int[] runtimeId;
		}
	}
}
