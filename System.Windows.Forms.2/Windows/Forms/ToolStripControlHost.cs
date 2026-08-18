using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003B8 RID: 952
	public class ToolStripControlHost : ToolStripItem
	{
		// Token: 0x06003F68 RID: 16232 RVA: 0x00111AEC File Offset: 0x0010FCEC
		public ToolStripControlHost(Control c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "ControlCannotBeNull");
			}
			this.control = c;
			this.SyncControlParent();
			c.Visible = true;
			this.SetBounds(c.Bounds);
			Rectangle bounds = this.Bounds;
			CommonProperties.UpdateSpecifiedBounds(c, bounds.X, bounds.Y, bounds.Width, bounds.Height);
			if (AccessibilityImprovements.Level3)
			{
				c.ToolStripControlHost = this;
			}
			this.OnSubscribeControlEvents(c);
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x00111B78 File Offset: 0x0010FD78
		public ToolStripControlHost(Control c, string name) : this(c)
		{
			base.Name = name;
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x00111B88 File Offset: 0x0010FD88
		// (set) Token: 0x06003F6B RID: 16235 RVA: 0x00111B95 File Offset: 0x0010FD95
		public override Color BackColor
		{
			get
			{
				return this.Control.BackColor;
			}
			set
			{
				this.Control.BackColor = value;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x00111BA3 File Offset: 0x0010FDA3
		// (set) Token: 0x06003F6D RID: 16237 RVA: 0x00111BB0 File Offset: 0x0010FDB0
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		[DefaultValue(null)]
		public override Image BackgroundImage
		{
			get
			{
				return this.Control.BackgroundImage;
			}
			set
			{
				this.Control.BackgroundImage = value;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x00111BBE File Offset: 0x0010FDBE
		// (set) Token: 0x06003F6F RID: 16239 RVA: 0x00111BCB File Offset: 0x0010FDCB
		[SRCategory("CatAppearance")]
		[DefaultValue(ImageLayout.Tile)]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return this.Control.BackgroundImageLayout;
			}
			set
			{
				this.Control.BackgroundImageLayout = value;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x00111BD9 File Offset: 0x0010FDD9
		public override bool CanSelect
		{
			get
			{
				return this.control != null && (base.DesignMode || this.Control.CanSelect);
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06003F71 RID: 16241 RVA: 0x00111BFA File Offset: 0x0010FDFA
		// (set) Token: 0x06003F72 RID: 16242 RVA: 0x00111C07 File Offset: 0x0010FE07
		[SRCategory("CatFocus")]
		[DefaultValue(true)]
		[SRDescription("ControlCausesValidationDescr")]
		public bool CausesValidation
		{
			get
			{
				return this.Control.CausesValidation;
			}
			set
			{
				this.Control.CausesValidation = value;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x00111C15 File Offset: 0x0010FE15
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x00111C1D File Offset: 0x0010FE1D
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Browsable(false)]
		public ContentAlignment ControlAlign
		{
			get
			{
				return this.controlAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.controlAlign != value)
				{
					this.controlAlign = value;
					this.OnBoundsChanged();
				}
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x00111C53 File Offset: 0x0010FE53
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x00111C5B File Offset: 0x0010FE5B
		internal AccessibleObject ControlAccessibilityObject
		{
			get
			{
				Control control = this.Control;
				if (control == null)
				{
					return null;
				}
				return control.AccessibilityObject;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x00111C6E File Offset: 0x0010FE6E
		protected override Size DefaultSize
		{
			get
			{
				if (this.Control != null)
				{
					return this.Control.Size;
				}
				return base.DefaultSize;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x00111C8A File Offset: 0x0010FE8A
		// (set) Token: 0x06003F79 RID: 16249 RVA: 0x00111C92 File Offset: 0x0010FE92
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return base.DisplayStyle;
			}
			set
			{
				base.DisplayStyle = value;
			}
		}

		// Token: 0x1400030D RID: 781
		// (add) Token: 0x06003F7A RID: 16250 RVA: 0x00111C9B File Offset: 0x0010FE9B
		// (remove) Token: 0x06003F7B RID: 16251 RVA: 0x00111CAE File Offset: 0x0010FEAE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DisplayStyleChanged
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

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x00111CC1 File Offset: 0x0010FEC1
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x00111CC9 File Offset: 0x0010FEC9
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool DoubleClickEnabled
		{
			get
			{
				return base.DoubleClickEnabled;
			}
			set
			{
				base.DoubleClickEnabled = value;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x00111CD2 File Offset: 0x0010FED2
		// (set) Token: 0x06003F7F RID: 16255 RVA: 0x00111CDF File Offset: 0x0010FEDF
		public override Font Font
		{
			get
			{
				return this.Control.Font;
			}
			set
			{
				this.Control.Font = value;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x00111CED File Offset: 0x0010FEED
		// (set) Token: 0x06003F81 RID: 16257 RVA: 0x00111CFA File Offset: 0x0010FEFA
		public override bool Enabled
		{
			get
			{
				return this.Control.Enabled;
			}
			set
			{
				this.Control.Enabled = value;
			}
		}

		// Token: 0x1400030E RID: 782
		// (add) Token: 0x06003F82 RID: 16258 RVA: 0x00111D08 File Offset: 0x0010FF08
		// (remove) Token: 0x06003F83 RID: 16259 RVA: 0x00111D1B File Offset: 0x0010FF1B
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnEnterDescr")]
		public event EventHandler Enter
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventEnter, value);
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x00111D2E File Offset: 0x0010FF2E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public virtual bool Focused
		{
			get
			{
				return this.Control.Focused;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06003F85 RID: 16261 RVA: 0x00111D3B File Offset: 0x0010FF3B
		// (set) Token: 0x06003F86 RID: 16262 RVA: 0x00111D48 File Offset: 0x0010FF48
		public override Color ForeColor
		{
			get
			{
				return this.Control.ForeColor;
			}
			set
			{
				this.Control.ForeColor = value;
			}
		}

		// Token: 0x1400030F RID: 783
		// (add) Token: 0x06003F87 RID: 16263 RVA: 0x00111D56 File Offset: 0x0010FF56
		// (remove) Token: 0x06003F88 RID: 16264 RVA: 0x00111D69 File Offset: 0x0010FF69
		[SRCategory("CatFocus")]
		[SRDescription("ToolStripItemOnGotFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler GotFocus
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventGotFocus, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventGotFocus, value);
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x00111D7C File Offset: 0x0010FF7C
		// (set) Token: 0x06003F8A RID: 16266 RVA: 0x00111D84 File Offset: 0x0010FF84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image Image
		{
			get
			{
				return base.Image;
			}
			set
			{
				base.Image = value;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x00111D8D File Offset: 0x0010FF8D
		// (set) Token: 0x06003F8C RID: 16268 RVA: 0x00111D95 File Offset: 0x0010FF95
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return base.ImageScaling;
			}
			set
			{
				base.ImageScaling = value;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x00111D9E File Offset: 0x0010FF9E
		// (set) Token: 0x06003F8E RID: 16270 RVA: 0x00111DA6 File Offset: 0x0010FFA6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color ImageTransparentColor
		{
			get
			{
				return base.ImageTransparentColor;
			}
			set
			{
				base.ImageTransparentColor = value;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x00111DAF File Offset: 0x0010FFAF
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x00111DB7 File Offset: 0x0010FFB7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment ImageAlign
		{
			get
			{
				return base.ImageAlign;
			}
			set
			{
				base.ImageAlign = value;
			}
		}

		// Token: 0x14000310 RID: 784
		// (add) Token: 0x06003F91 RID: 16273 RVA: 0x00111DC0 File Offset: 0x0010FFC0
		// (remove) Token: 0x06003F92 RID: 16274 RVA: 0x00111DD3 File Offset: 0x0010FFD3
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnLeaveDescr")]
		public event EventHandler Leave
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventLeave, value);
			}
		}

		// Token: 0x14000311 RID: 785
		// (add) Token: 0x06003F93 RID: 16275 RVA: 0x00111DE6 File Offset: 0x0010FFE6
		// (remove) Token: 0x06003F94 RID: 16276 RVA: 0x00111DF9 File Offset: 0x0010FFF9
		[SRCategory("CatFocus")]
		[SRDescription("ToolStripItemOnLostFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler LostFocus
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventLostFocus, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventLostFocus, value);
			}
		}

		// Token: 0x14000312 RID: 786
		// (add) Token: 0x06003F95 RID: 16277 RVA: 0x00111E0C File Offset: 0x0011000C
		// (remove) Token: 0x06003F96 RID: 16278 RVA: 0x00111E1F File Offset: 0x0011001F
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyDownDescr")]
		public event KeyEventHandler KeyDown
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventKeyDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventKeyDown, value);
			}
		}

		// Token: 0x14000313 RID: 787
		// (add) Token: 0x06003F97 RID: 16279 RVA: 0x00111E32 File Offset: 0x00110032
		// (remove) Token: 0x06003F98 RID: 16280 RVA: 0x00111E45 File Offset: 0x00110045
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyPressDescr")]
		public event KeyPressEventHandler KeyPress
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventKeyPress, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventKeyPress, value);
			}
		}

		// Token: 0x14000314 RID: 788
		// (add) Token: 0x06003F99 RID: 16281 RVA: 0x00111E58 File Offset: 0x00110058
		// (remove) Token: 0x06003F9A RID: 16282 RVA: 0x00111E6B File Offset: 0x0011006B
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyUpDescr")]
		public event KeyEventHandler KeyUp
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventKeyUp, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventKeyUp, value);
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x00111E7E File Offset: 0x0011007E
		// (set) Token: 0x06003F9C RID: 16284 RVA: 0x00111E9A File Offset: 0x0011009A
		public override RightToLeft RightToLeft
		{
			get
			{
				if (this.control != null)
				{
					return this.control.RightToLeft;
				}
				return base.RightToLeft;
			}
			set
			{
				if (this.control != null)
				{
					this.control.RightToLeft = value;
				}
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x00111EB0 File Offset: 0x001100B0
		// (set) Token: 0x06003F9E RID: 16286 RVA: 0x00111EB8 File Offset: 0x001100B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool RightToLeftAutoMirrorImage
		{
			get
			{
				return base.RightToLeftAutoMirrorImage;
			}
			set
			{
				base.RightToLeftAutoMirrorImage = value;
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x00111EC1 File Offset: 0x001100C1
		public override bool Selected
		{
			get
			{
				return this.Control != null && this.Control.Focused;
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x00111ED8 File Offset: 0x001100D8
		// (set) Token: 0x06003FA1 RID: 16289 RVA: 0x00111EE0 File Offset: 0x001100E0
		public override Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				Rectangle right = Rectangle.Empty;
				if (this.control != null)
				{
					right = this.control.Bounds;
					right.Size = value;
					CommonProperties.UpdateSpecifiedBounds(this.control, right.X, right.Y, right.Width, right.Height);
				}
				base.Size = value;
				if (this.control != null)
				{
					Rectangle bounds = this.control.Bounds;
					if (bounds != right)
					{
						CommonProperties.UpdateSpecifiedBounds(this.control, bounds.X, bounds.Y, bounds.Width, bounds.Height);
					}
				}
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x00031ACA File Offset: 0x0002FCCA
		// (set) Token: 0x06003FA3 RID: 16291 RVA: 0x00111F81 File Offset: 0x00110181
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (value != null)
				{
					this.Control.Site = new ToolStripControlHost.StubSite(this.Control, this);
					return;
				}
				this.Control.Site = null;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x00111FB1 File Offset: 0x001101B1
		// (set) Token: 0x06003FA5 RID: 16293 RVA: 0x00111FBE File Offset: 0x001101BE
		[DefaultValue("")]
		public override string Text
		{
			get
			{
				return this.Control.Text;
			}
			set
			{
				this.Control.Text = value;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x00111FCC File Offset: 0x001101CC
		// (set) Token: 0x06003FA7 RID: 16295 RVA: 0x00111FD4 File Offset: 0x001101D4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x00111FDD File Offset: 0x001101DD
		// (set) Token: 0x06003FA9 RID: 16297 RVA: 0x00111FE5 File Offset: 0x001101E5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return base.TextDirection;
			}
			set
			{
				base.TextDirection = value;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06003FAA RID: 16298 RVA: 0x00111FEE File Offset: 0x001101EE
		// (set) Token: 0x06003FAB RID: 16299 RVA: 0x00111FF6 File Offset: 0x001101F6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new TextImageRelation TextImageRelation
		{
			get
			{
				return base.TextImageRelation;
			}
			set
			{
				base.TextImageRelation = value;
			}
		}

		// Token: 0x14000315 RID: 789
		// (add) Token: 0x06003FAC RID: 16300 RVA: 0x00111FFF File Offset: 0x001101FF
		// (remove) Token: 0x06003FAD RID: 16301 RVA: 0x00112012 File Offset: 0x00110212
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatingDescr")]
		public event CancelEventHandler Validating
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventValidating, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventValidating, value);
			}
		}

		// Token: 0x14000316 RID: 790
		// (add) Token: 0x06003FAE RID: 16302 RVA: 0x00112025 File Offset: 0x00110225
		// (remove) Token: 0x06003FAF RID: 16303 RVA: 0x00112038 File Offset: 0x00110238
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatedDescr")]
		public event EventHandler Validated
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EventValidated, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EventValidated, value);
			}
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0011204B File Offset: 0x0011024B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return base.CreateAccessibilityInstance();
			}
			return this.Control.AccessibilityObject;
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x00112066 File Offset: 0x00110266
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.Control != null)
			{
				this.OnUnsubscribeControlEvents(this.Control);
				this.Control.Dispose();
				this.control = null;
			}
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x00112098 File Offset: 0x00110298
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Focus()
		{
			this.Control.Focus();
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x001120A8 File Offset: 0x001102A8
		public override Size GetPreferredSize(Size constrainingSize)
		{
			if (this.control != null)
			{
				return this.Control.GetPreferredSize(constrainingSize - this.Padding.Size) + this.Padding.Size;
			}
			return base.GetPreferredSize(constrainingSize);
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x001120F7 File Offset: 0x001102F7
		private void HandleClick(object sender, EventArgs e)
		{
			this.OnClick(e);
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x00112100 File Offset: 0x00110300
		private void HandleBackColorChanged(object sender, EventArgs e)
		{
			this.OnBackColorChanged(e);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x00112109 File Offset: 0x00110309
		private void HandleDoubleClick(object sender, EventArgs e)
		{
			this.OnDoubleClick(e);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x00112112 File Offset: 0x00110312
		private void HandleDragDrop(object sender, DragEventArgs e)
		{
			this.OnDragDrop(e);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0011211B File Offset: 0x0011031B
		private void HandleDragEnter(object sender, DragEventArgs e)
		{
			this.OnDragEnter(e);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x00112124 File Offset: 0x00110324
		private void HandleDragLeave(object sender, EventArgs e)
		{
			this.OnDragLeave(e);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x0011212D File Offset: 0x0011032D
		private void HandleDragOver(object sender, DragEventArgs e)
		{
			this.OnDragOver(e);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x00112136 File Offset: 0x00110336
		private void HandleEnter(object sender, EventArgs e)
		{
			this.OnEnter(e);
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x0011213F File Offset: 0x0011033F
		private void HandleEnabledChanged(object sender, EventArgs e)
		{
			this.OnEnabledChanged(e);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x00112148 File Offset: 0x00110348
		private void HandleForeColorChanged(object sender, EventArgs e)
		{
			this.OnForeColorChanged(e);
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x00112151 File Offset: 0x00110351
		private void HandleGiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			this.OnGiveFeedback(e);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x0011215A File Offset: 0x0011035A
		private void HandleGotFocus(object sender, EventArgs e)
		{
			this.OnGotFocus(e);
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x00112163 File Offset: 0x00110363
		private void HandleLocationChanged(object sender, EventArgs e)
		{
			this.OnLocationChanged(e);
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x0011216C File Offset: 0x0011036C
		private void HandleLostFocus(object sender, EventArgs e)
		{
			this.OnLostFocus(e);
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x00112175 File Offset: 0x00110375
		private void HandleKeyDown(object sender, KeyEventArgs e)
		{
			this.OnKeyDown(e);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x0011217E File Offset: 0x0011037E
		private void HandleKeyPress(object sender, KeyPressEventArgs e)
		{
			this.OnKeyPress(e);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x00112187 File Offset: 0x00110387
		private void HandleKeyUp(object sender, KeyEventArgs e)
		{
			this.OnKeyUp(e);
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x00112190 File Offset: 0x00110390
		private void HandleLeave(object sender, EventArgs e)
		{
			this.OnLeave(e);
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x00112199 File Offset: 0x00110399
		private void HandleMouseDown(object sender, MouseEventArgs e)
		{
			this.OnMouseDown(e);
			base.RaiseMouseEvent(ToolStripItem.EventMouseDown, e);
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x001121AE File Offset: 0x001103AE
		private void HandleMouseEnter(object sender, EventArgs e)
		{
			this.OnMouseEnter(e);
			base.RaiseEvent(ToolStripItem.EventMouseEnter, e);
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x001121C3 File Offset: 0x001103C3
		private void HandleMouseLeave(object sender, EventArgs e)
		{
			this.OnMouseLeave(e);
			base.RaiseEvent(ToolStripItem.EventMouseLeave, e);
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x001121D8 File Offset: 0x001103D8
		private void HandleMouseHover(object sender, EventArgs e)
		{
			this.OnMouseHover(e);
			base.RaiseEvent(ToolStripItem.EventMouseHover, e);
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x001121ED File Offset: 0x001103ED
		private void HandleMouseMove(object sender, MouseEventArgs e)
		{
			this.OnMouseMove(e);
			base.RaiseMouseEvent(ToolStripItem.EventMouseMove, e);
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x00112202 File Offset: 0x00110402
		private void HandleMouseUp(object sender, MouseEventArgs e)
		{
			this.OnMouseUp(e);
			base.RaiseMouseEvent(ToolStripItem.EventMouseUp, e);
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x00112217 File Offset: 0x00110417
		private void HandlePaint(object sender, PaintEventArgs e)
		{
			this.OnPaint(e);
			base.RaisePaintEvent(ToolStripItem.EventPaint, e);
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0011222C File Offset: 0x0011042C
		private void HandleQueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
		{
			QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)base.Events[ToolStripItem.EventQueryAccessibilityHelp];
			if (queryAccessibilityHelpEventHandler != null)
			{
				queryAccessibilityHelpEventHandler(this, e);
			}
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0011225A File Offset: 0x0011045A
		private void HandleQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			this.OnQueryContinueDrag(e);
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x00112263 File Offset: 0x00110463
		private void HandleRightToLeftChanged(object sender, EventArgs e)
		{
			this.OnRightToLeftChanged(e);
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x0011226C File Offset: 0x0011046C
		private void HandleResize(object sender, EventArgs e)
		{
			if (this.suspendSyncSizeCount == 0)
			{
				this.OnHostedControlResize(e);
			}
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x0011227D File Offset: 0x0011047D
		private void HandleTextChanged(object sender, EventArgs e)
		{
			this.OnTextChanged(e);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x00112288 File Offset: 0x00110488
		private void HandleControlVisibleChanged(object sender, EventArgs e)
		{
			bool participatesInLayout = ((IArrangedElement)this.Control).ParticipatesInLayout;
			bool participatesInLayout2 = ((IArrangedElement)this).ParticipatesInLayout;
			if (participatesInLayout2 != participatesInLayout)
			{
				base.Visible = this.Control.Visible;
			}
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x001122BD File Offset: 0x001104BD
		private void HandleValidating(object sender, CancelEventArgs e)
		{
			this.OnValidating(e);
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x001122C6 File Offset: 0x001104C6
		private void HandleValidated(object sender, EventArgs e)
		{
			this.OnValidated(e);
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x001122CF File Offset: 0x001104CF
		internal override void OnAccessibleDescriptionChanged(EventArgs e)
		{
			this.Control.AccessibleDescription = base.AccessibleDescription;
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x001122E2 File Offset: 0x001104E2
		internal override void OnAccessibleNameChanged(EventArgs e)
		{
			this.Control.AccessibleName = base.AccessibleName;
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x001122F5 File Offset: 0x001104F5
		internal override void OnAccessibleDefaultActionDescriptionChanged(EventArgs e)
		{
			this.Control.AccessibleDefaultActionDescription = base.AccessibleDefaultActionDescription;
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x00112308 File Offset: 0x00110508
		internal override void OnAccessibleRoleChanged(EventArgs e)
		{
			this.Control.AccessibleRole = base.AccessibleRole;
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x0011231B File Offset: 0x0011051B
		protected virtual void OnEnter(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventEnter, e);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x00112329 File Offset: 0x00110529
		protected virtual void OnGotFocus(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventGotFocus, e);
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x00112337 File Offset: 0x00110537
		protected virtual void OnLeave(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventLeave, e);
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x00112345 File Offset: 0x00110545
		protected virtual void OnLostFocus(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventLostFocus, e);
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x00112353 File Offset: 0x00110553
		protected virtual void OnKeyDown(KeyEventArgs e)
		{
			base.RaiseKeyEvent(ToolStripControlHost.EventKeyDown, e);
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x00112361 File Offset: 0x00110561
		protected virtual void OnKeyPress(KeyPressEventArgs e)
		{
			base.RaiseKeyPressEvent(ToolStripControlHost.EventKeyPress, e);
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x0011236F File Offset: 0x0011056F
		protected virtual void OnKeyUp(KeyEventArgs e)
		{
			base.RaiseKeyEvent(ToolStripControlHost.EventKeyUp, e);
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x00112380 File Offset: 0x00110580
		protected override void OnBoundsChanged()
		{
			if (this.control != null)
			{
				this.SuspendSizeSync();
				IArrangedElement arrangedElement = this.control;
				if (arrangedElement == null)
				{
					return;
				}
				Size size = LayoutUtils.DeflateRect(this.Bounds, this.Padding).Size;
				Rectangle rectangle = LayoutUtils.Align(size, this.Bounds, this.ControlAlign);
				arrangedElement.SetBounds(rectangle, BoundsSpecified.None);
				if (rectangle != this.control.Bounds)
				{
					rectangle = LayoutUtils.Align(this.control.Size, this.Bounds, this.ControlAlign);
					arrangedElement.SetBounds(rectangle, BoundsSpecified.None);
				}
				this.ResumeSizeSync();
			}
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000072B6 File Offset: 0x000054B6
		protected override void OnPaint(PaintEventArgs e)
		{
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000072B6 File Offset: 0x000054B6
		protected internal override void OnLayout(LayoutEventArgs e)
		{
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x0011241C File Offset: 0x0011061C
		protected override void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
		{
			if (oldParent != null && base.Owner == null && newParent == null && this.Control != null)
			{
				WindowsFormsUtils.ReadOnlyControlCollection controlCollection = ToolStripControlHost.GetControlCollection(this.Control.ParentInternal as ToolStrip);
				if (controlCollection != null)
				{
					controlCollection.RemoveInternal(this.Control);
				}
			}
			else
			{
				this.SyncControlParent();
			}
			base.OnParentChanged(oldParent, newParent);
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x00112474 File Offset: 0x00110674
		protected virtual void OnSubscribeControlEvents(Control control)
		{
			if (control != null)
			{
				control.Click += this.HandleClick;
				control.BackColorChanged += this.HandleBackColorChanged;
				control.DoubleClick += this.HandleDoubleClick;
				control.DragDrop += this.HandleDragDrop;
				control.DragEnter += this.HandleDragEnter;
				control.DragLeave += this.HandleDragLeave;
				control.DragOver += this.HandleDragOver;
				control.Enter += this.HandleEnter;
				control.EnabledChanged += this.HandleEnabledChanged;
				control.ForeColorChanged += this.HandleForeColorChanged;
				control.GiveFeedback += this.HandleGiveFeedback;
				control.GotFocus += this.HandleGotFocus;
				control.Leave += this.HandleLeave;
				control.LocationChanged += this.HandleLocationChanged;
				control.LostFocus += this.HandleLostFocus;
				control.KeyDown += this.HandleKeyDown;
				control.KeyPress += this.HandleKeyPress;
				control.KeyUp += this.HandleKeyUp;
				control.MouseDown += this.HandleMouseDown;
				control.MouseEnter += this.HandleMouseEnter;
				control.MouseHover += this.HandleMouseHover;
				control.MouseLeave += this.HandleMouseLeave;
				control.MouseMove += this.HandleMouseMove;
				control.MouseUp += this.HandleMouseUp;
				control.Paint += this.HandlePaint;
				control.QueryAccessibilityHelp += this.HandleQueryAccessibilityHelp;
				control.QueryContinueDrag += this.HandleQueryContinueDrag;
				control.Resize += this.HandleResize;
				control.RightToLeftChanged += this.HandleRightToLeftChanged;
				control.TextChanged += this.HandleTextChanged;
				control.VisibleChanged += this.HandleControlVisibleChanged;
				control.Validating += this.HandleValidating;
				control.Validated += this.HandleValidated;
			}
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x001126DC File Offset: 0x001108DC
		protected virtual void OnUnsubscribeControlEvents(Control control)
		{
			if (control != null)
			{
				control.Click -= this.HandleClick;
				control.BackColorChanged -= this.HandleBackColorChanged;
				control.DoubleClick -= this.HandleDoubleClick;
				control.DragDrop -= this.HandleDragDrop;
				control.DragEnter -= this.HandleDragEnter;
				control.DragLeave -= this.HandleDragLeave;
				control.DragOver -= this.HandleDragOver;
				control.Enter -= this.HandleEnter;
				control.EnabledChanged -= this.HandleEnabledChanged;
				control.ForeColorChanged -= this.HandleForeColorChanged;
				control.GiveFeedback -= this.HandleGiveFeedback;
				control.GotFocus -= this.HandleGotFocus;
				control.Leave -= this.HandleLeave;
				control.LocationChanged -= this.HandleLocationChanged;
				control.LostFocus -= this.HandleLostFocus;
				control.KeyDown -= this.HandleKeyDown;
				control.KeyPress -= this.HandleKeyPress;
				control.KeyUp -= this.HandleKeyUp;
				control.MouseDown -= this.HandleMouseDown;
				control.MouseEnter -= this.HandleMouseEnter;
				control.MouseHover -= this.HandleMouseHover;
				control.MouseLeave -= this.HandleMouseLeave;
				control.MouseMove -= this.HandleMouseMove;
				control.MouseUp -= this.HandleMouseUp;
				control.Paint -= this.HandlePaint;
				control.QueryAccessibilityHelp -= this.HandleQueryAccessibilityHelp;
				control.QueryContinueDrag -= this.HandleQueryContinueDrag;
				control.Resize -= this.HandleResize;
				control.RightToLeftChanged -= this.HandleRightToLeftChanged;
				control.TextChanged -= this.HandleTextChanged;
				control.VisibleChanged -= this.HandleControlVisibleChanged;
				control.Validating -= this.HandleValidating;
				control.Validated -= this.HandleValidated;
			}
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x00112941 File Offset: 0x00110B41
		protected virtual void OnValidating(CancelEventArgs e)
		{
			base.RaiseCancelEvent(ToolStripControlHost.EventValidating, e);
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x0011294F File Offset: 0x00110B4F
		protected virtual void OnValidated(EventArgs e)
		{
			base.RaiseEvent(ToolStripControlHost.EventValidated, e);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x00112960 File Offset: 0x00110B60
		private static WindowsFormsUtils.ReadOnlyControlCollection GetControlCollection(ToolStrip toolStrip)
		{
			return (toolStrip != null) ? ((WindowsFormsUtils.ReadOnlyControlCollection)toolStrip.Controls) : null;
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x00112980 File Offset: 0x00110B80
		private void SyncControlParent()
		{
			WindowsFormsUtils.ReadOnlyControlCollection controlCollection = ToolStripControlHost.GetControlCollection(base.ParentInternal);
			if (controlCollection != null)
			{
				controlCollection.AddInternal(this.Control);
			}
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x001129A8 File Offset: 0x00110BA8
		protected virtual void OnHostedControlResize(EventArgs e)
		{
			this.Size = this.Control.Size;
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x00011A20 File Offset: 0x0000FC20
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return false;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x001129BB File Offset: 0x00110BBB
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (this.control != null)
			{
				return this.control.ProcessMnemonic(charCode);
			}
			return base.ProcessMnemonic(charCode);
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x00011A20 File Offset: 0x0000FC20
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessDialogKey(Keys keyData)
		{
			return false;
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x001129DC File Offset: 0x00110BDC
		protected override void SetVisibleCore(bool visible)
		{
			if (this.inSetVisibleCore)
			{
				return;
			}
			this.inSetVisibleCore = true;
			this.Control.SuspendLayout();
			try
			{
				this.Control.Visible = visible;
			}
			finally
			{
				this.Control.ResumeLayout(false);
				base.SetVisibleCore(visible);
				this.inSetVisibleCore = false;
			}
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x00112A40 File Offset: 0x00110C40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetBackColor()
		{
			this.Control.ResetBackColor();
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x00112A4D File Offset: 0x00110C4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetForeColor()
		{
			this.Control.ResetForeColor();
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x00112A5A File Offset: 0x00110C5A
		private void SuspendSizeSync()
		{
			this.suspendSyncSizeCount++;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x00112A6A File Offset: 0x00110C6A
		private void ResumeSizeSync()
		{
			this.suspendSyncSizeCount--;
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x00112A7A File Offset: 0x00110C7A
		internal override bool ShouldSerializeBackColor()
		{
			if (this.control != null)
			{
				return this.control.ShouldSerializeBackColor();
			}
			return base.ShouldSerializeBackColor();
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x00112A96 File Offset: 0x00110C96
		internal override bool ShouldSerializeForeColor()
		{
			if (this.control != null)
			{
				return this.control.ShouldSerializeForeColor();
			}
			return base.ShouldSerializeForeColor();
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x00112AB2 File Offset: 0x00110CB2
		internal override bool ShouldSerializeFont()
		{
			if (this.control != null)
			{
				return this.control.ShouldSerializeFont();
			}
			return base.ShouldSerializeFont();
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x00112ACE File Offset: 0x00110CCE
		internal override bool ShouldSerializeRightToLeft()
		{
			if (this.control != null)
			{
				return this.control.ShouldSerializeRightToLeft();
			}
			return base.ShouldSerializeRightToLeft();
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x00112AEA File Offset: 0x00110CEA
		internal override void OnKeyboardToolTipHook(ToolTip toolTip)
		{
			base.OnKeyboardToolTipHook(toolTip);
			KeyboardToolTipStateMachine.Instance.Hook(this.Control, toolTip);
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x00112B04 File Offset: 0x00110D04
		internal override void OnKeyboardToolTipUnhook(ToolTip toolTip)
		{
			base.OnKeyboardToolTipUnhook(toolTip);
			KeyboardToolTipStateMachine.Instance.Unhook(this.Control, toolTip);
		}

		// Token: 0x040024B0 RID: 9392
		private Control control;

		// Token: 0x040024B1 RID: 9393
		private int suspendSyncSizeCount;

		// Token: 0x040024B2 RID: 9394
		private ContentAlignment controlAlign = ContentAlignment.MiddleCenter;

		// Token: 0x040024B3 RID: 9395
		private bool inSetVisibleCore;

		// Token: 0x040024B4 RID: 9396
		internal static readonly object EventGotFocus = new object();

		// Token: 0x040024B5 RID: 9397
		internal static readonly object EventLostFocus = new object();

		// Token: 0x040024B6 RID: 9398
		internal static readonly object EventKeyDown = new object();

		// Token: 0x040024B7 RID: 9399
		internal static readonly object EventKeyPress = new object();

		// Token: 0x040024B8 RID: 9400
		internal static readonly object EventKeyUp = new object();

		// Token: 0x040024B9 RID: 9401
		internal static readonly object EventEnter = new object();

		// Token: 0x040024BA RID: 9402
		internal static readonly object EventLeave = new object();

		// Token: 0x040024BB RID: 9403
		internal static readonly object EventValidated = new object();

		// Token: 0x040024BC RID: 9404
		internal static readonly object EventValidating = new object();

		// Token: 0x020007FF RID: 2047
		private class StubSite : ISite, IServiceProvider, IDictionaryService
		{
			// Token: 0x06006ED8 RID: 28376 RVA: 0x001967D4 File Offset: 0x001949D4
			public StubSite(Component control, Component host)
			{
				this.comp = control;
				this.owner = host;
			}

			// Token: 0x17001833 RID: 6195
			// (get) Token: 0x06006ED9 RID: 28377 RVA: 0x001967EA File Offset: 0x001949EA
			IComponent ISite.Component
			{
				get
				{
					return this.comp;
				}
			}

			// Token: 0x17001834 RID: 6196
			// (get) Token: 0x06006EDA RID: 28378 RVA: 0x001967F2 File Offset: 0x001949F2
			IContainer ISite.Container
			{
				get
				{
					return this.owner.Site.Container;
				}
			}

			// Token: 0x17001835 RID: 6197
			// (get) Token: 0x06006EDB RID: 28379 RVA: 0x00196804 File Offset: 0x00194A04
			bool ISite.DesignMode
			{
				get
				{
					return this.owner.Site.DesignMode;
				}
			}

			// Token: 0x17001836 RID: 6198
			// (get) Token: 0x06006EDC RID: 28380 RVA: 0x00196816 File Offset: 0x00194A16
			// (set) Token: 0x06006EDD RID: 28381 RVA: 0x00196828 File Offset: 0x00194A28
			string ISite.Name
			{
				get
				{
					return this.owner.Site.Name;
				}
				set
				{
					this.owner.Site.Name = value;
				}
			}

			// Token: 0x06006EDE RID: 28382 RVA: 0x0019683C File Offset: 0x00194A3C
			object IServiceProvider.GetService(Type service)
			{
				if (service == null)
				{
					throw new ArgumentNullException("service");
				}
				if (service == typeof(IDictionaryService))
				{
					return this;
				}
				if (this.owner.Site != null)
				{
					return this.owner.Site.GetService(service);
				}
				return null;
			}

			// Token: 0x06006EDF RID: 28383 RVA: 0x00196894 File Offset: 0x00194A94
			object IDictionaryService.GetKey(object value)
			{
				if (this._dictionary != null)
				{
					foreach (object obj in this._dictionary)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						object value2 = dictionaryEntry.Value;
						if (value != null && value.Equals(value2))
						{
							return dictionaryEntry.Key;
						}
					}
				}
				return null;
			}

			// Token: 0x06006EE0 RID: 28384 RVA: 0x00196914 File Offset: 0x00194B14
			object IDictionaryService.GetValue(object key)
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				return null;
			}

			// Token: 0x06006EE1 RID: 28385 RVA: 0x0019692C File Offset: 0x00194B2C
			void IDictionaryService.SetValue(object key, object value)
			{
				if (this._dictionary == null)
				{
					this._dictionary = new Hashtable();
				}
				if (value == null)
				{
					this._dictionary.Remove(key);
					return;
				}
				this._dictionary[key] = value;
			}

			// Token: 0x040042F6 RID: 17142
			private Hashtable _dictionary;

			// Token: 0x040042F7 RID: 17143
			private IComponent comp;

			// Token: 0x040042F8 RID: 17144
			private IComponent owner;
		}
	}
}
