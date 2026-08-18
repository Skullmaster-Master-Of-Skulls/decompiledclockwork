using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x0200010C RID: 268
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.UpDownBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class UpDownBase : ContainerControl
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x000118AC File Offset: 0x0000FAAC
		public UpDownBase()
		{
			if (DpiHelper.IsScalingRequired)
			{
				this.defaultButtonsWidth = base.LogicalToDeviceUnits(16);
			}
			this.upDownButtons = new UpDownBase.UpDownButtons(this);
			this.upDownEdit = new UpDownBase.UpDownEdit(this);
			this.upDownEdit.BorderStyle = BorderStyle.None;
			this.upDownEdit.AutoSize = false;
			this.upDownEdit.KeyDown += this.OnTextBoxKeyDown;
			this.upDownEdit.KeyPress += this.OnTextBoxKeyPress;
			this.upDownEdit.TextChanged += this.OnTextBoxTextChanged;
			this.upDownEdit.LostFocus += this.OnTextBoxLostFocus;
			this.upDownEdit.Resize += this.OnTextBoxResize;
			this.upDownButtons.TabStop = false;
			this.upDownButtons.Size = new Size(this.defaultButtonsWidth, this.PreferredHeight);
			this.upDownButtons.UpDown += this.OnUpDown;
			base.Controls.AddRange(new Control[]
			{
				this.upDownButtons,
				this.upDownEdit
			});
			base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.FixedHeight, true);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoScroll
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00011A23 File Offset: 0x0000FC23
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00011A2B File Offset: 0x0000FC2B
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00011A34 File Offset: 0x0000FC34
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00011A3C File Offset: 0x0000FC3C
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

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00011A4D File Offset: 0x0000FC4D
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
				base.AutoSize = value;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060004ED RID: 1261 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x060004EE RID: 1262 RVA: 0x00011A5F File Offset: 0x0000FC5F
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

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00011A68 File Offset: 0x0000FC68
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00011A75 File Offset: 0x0000FC75
		public override Color BackColor
		{
			get
			{
				return this.upDownEdit.BackColor;
			}
			set
			{
				base.BackColor = value;
				this.upDownEdit.BackColor = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00011A98 File Offset: 0x0000FC98
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060004F3 RID: 1267 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x060004F4 RID: 1268 RVA: 0x00011AAA File Offset: 0x0000FCAA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00011ABB File Offset: 0x0000FCBB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060004F7 RID: 1271 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x060004F8 RID: 1272 RVA: 0x00011ACD File Offset: 0x0000FCCD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00011AD6 File Offset: 0x0000FCD6
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x00011ADE File Offset: 0x0000FCDE
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("UpDownBaseBorderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00011B1C File Offset: 0x0000FD1C
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00011B24 File Offset: 0x0000FD24
		protected bool ChangingText
		{
			get
			{
				return this.changingText;
			}
			set
			{
				this.changingText = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00011B2D File Offset: 0x0000FD2D
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00011B35 File Offset: 0x0000FD35
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
				this.upDownEdit.ContextMenu = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00011B4A File Offset: 0x0000FD4A
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00011B52 File Offset: 0x0000FD52
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
				this.upDownEdit.ContextMenuStrip = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00011B68 File Offset: 0x0000FD68
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style &= -8388609;
				if (!Application.RenderWithVisualStyles)
				{
					BorderStyle borderStyle = this.borderStyle;
					if (borderStyle != BorderStyle.FixedSingle)
					{
						if (borderStyle == BorderStyle.Fixed3D)
						{
							createParams.ExStyle |= 512;
						}
					}
					else
					{
						createParams.Style |= 8388608;
					}
				}
				return createParams;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00011BCB File Offset: 0x0000FDCB
		protected override Size DefaultSize
		{
			get
			{
				return new Size(120, this.PreferredHeight);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00011BDA File Offset: 0x0000FDDA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00011BE2 File Offset: 0x0000FDE2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlFocusedDescr")]
		public override bool Focused
		{
			get
			{
				return this.upDownEdit.Focused;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00011BEF File Offset: 0x0000FDEF
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00011BFC File Offset: 0x0000FDFC
		public override Color ForeColor
		{
			get
			{
				return this.upDownEdit.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				this.upDownEdit.ForeColor = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00011C11 File Offset: 0x0000FE11
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x00011C19 File Offset: 0x0000FE19
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("UpDownBaseInterceptArrowKeysDescr")]
		public bool InterceptArrowKeys
		{
			get
			{
				return this.interceptArrowKeys;
			}
			set
			{
				this.interceptArrowKeys = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00011C22 File Offset: 0x0000FE22
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00011C2A File Offset: 0x0000FE2A
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = new Size(value.Width, 0);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00011C3F File Offset: 0x0000FE3F
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x00011C47 File Offset: 0x0000FE47
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = new Size(value.Width, 0);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600050D RID: 1293 RVA: 0x00011C5C File Offset: 0x0000FE5C
		// (remove) Token: 0x0600050E RID: 1294 RVA: 0x00011C65 File Offset: 0x0000FE65
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseEnter
		{
			add
			{
				base.MouseEnter += value;
			}
			remove
			{
				base.MouseEnter -= value;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600050F RID: 1295 RVA: 0x00011C6E File Offset: 0x0000FE6E
		// (remove) Token: 0x06000510 RID: 1296 RVA: 0x00011C77 File Offset: 0x0000FE77
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseLeave
		{
			add
			{
				base.MouseLeave += value;
			}
			remove
			{
				base.MouseLeave -= value;
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000511 RID: 1297 RVA: 0x00011C80 File Offset: 0x0000FE80
		// (remove) Token: 0x06000512 RID: 1298 RVA: 0x00011C89 File Offset: 0x0000FE89
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseHover
		{
			add
			{
				base.MouseHover += value;
			}
			remove
			{
				base.MouseHover -= value;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000513 RID: 1299 RVA: 0x00011C92 File Offset: 0x0000FE92
		// (remove) Token: 0x06000514 RID: 1300 RVA: 0x00011C9B File Offset: 0x0000FE9B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00011CA4 File Offset: 0x0000FEA4
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("UpDownBasePreferredHeightDescr")]
		public int PreferredHeight
		{
			get
			{
				int num = base.FontHeight;
				if (this.borderStyle != BorderStyle.None)
				{
					num += SystemInformation.BorderSize.Height * 4 + 3;
				}
				else
				{
					num += 3;
				}
				return num;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00011CDB File Offset: 0x0000FEDB
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("UpDownBaseReadOnlyDescr")]
		public bool ReadOnly
		{
			get
			{
				return this.upDownEdit.ReadOnly;
			}
			set
			{
				this.upDownEdit.ReadOnly = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00011CF6 File Offset: 0x0000FEF6
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x00011D03 File Offset: 0x0000FF03
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return this.upDownEdit.Text;
			}
			set
			{
				this.upDownEdit.Text = value;
				this.ChangingText = false;
				if (this.UserEdit)
				{
					this.ValidateEditText();
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00011D26 File Offset: 0x0000FF26
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x00011D33 File Offset: 0x0000FF33
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(HorizontalAlignment.Left)]
		[SRDescription("UpDownBaseTextAlignDescr")]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.upDownEdit.TextAlign;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				this.upDownEdit.TextAlign = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00011D67 File Offset: 0x0000FF67
		internal TextBox TextBox
		{
			get
			{
				return this.upDownEdit;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00011D6F File Offset: 0x0000FF6F
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00011D78 File Offset: 0x0000FF78
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(LeftRightAlignment.Right)]
		[SRDescription("UpDownBaseAlignmentDescr")]
		public LeftRightAlignment UpDownAlign
		{
			get
			{
				return this.upDownAlign;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(LeftRightAlignment));
				}
				if (this.upDownAlign != value)
				{
					this.upDownAlign = value;
					this.PositionControls();
					base.Invalidate();
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00011DC7 File Offset: 0x0000FFC7
		internal UpDownBase.UpDownButtons UpDownButtonsInternal
		{
			get
			{
				return this.upDownButtons;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00011DCF File Offset: 0x0000FFCF
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x00011DD7 File Offset: 0x0000FFD7
		protected bool UserEdit
		{
			get
			{
				return this.userEdit;
			}
			set
			{
				this.userEdit = value;
			}
		}

		// Token: 0x06000522 RID: 1314
		public abstract void DownButton();

		// Token: 0x06000523 RID: 1315 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		internal override Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			return base.ApplyBoundsConstraints(suggestedX, suggestedY, proposedWidth, this.PreferredHeight);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00011DF1 File Offset: 0x0000FFF1
		internal string GetAccessibleName(string baseName)
		{
			if (baseName == null)
			{
				if (AccessibilityImprovements.Level5)
				{
					return string.Empty;
				}
				if (AccessibilityImprovements.Level3)
				{
					return SR.GetString("SpinnerAccessibleName");
				}
				if (AccessibilityImprovements.Level1)
				{
					return base.GetType().Name;
				}
			}
			return baseName;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00011E29 File Offset: 0x00010029
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			this.defaultButtonsWidth = base.LogicalToDeviceUnits(16);
			this.upDownButtons.Width = this.defaultButtonsWidth;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnChanged(object source, EventArgs e)
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00011E52 File Offset: 0x00010052
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.PositionControls();
			SystemEvents.UserPreferenceChanged += this.UserPreferenceChanged;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00011E72 File Offset: 0x00010072
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.UserPreferenceChanged;
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00011E8C File Offset: 0x0001008C
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Rectangle bounds = this.upDownEdit.Bounds;
			if (Application.RenderWithVisualStyles)
			{
				if (this.borderStyle == BorderStyle.None)
				{
					goto IL_249;
				}
				Rectangle clientRectangle = base.ClientRectangle;
				Rectangle clipRectangle = e.ClipRectangle;
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
				int num = 1;
				Rectangle clipRectangle2 = new Rectangle(clientRectangle.Left, clientRectangle.Top, num, clientRectangle.Height);
				Rectangle clipRectangle3 = new Rectangle(clientRectangle.Left, clientRectangle.Top, clientRectangle.Width, num);
				Rectangle clipRectangle4 = new Rectangle(clientRectangle.Right - num, clientRectangle.Top, num, clientRectangle.Height);
				Rectangle clipRectangle5 = new Rectangle(clientRectangle.Left, clientRectangle.Bottom - num, clientRectangle.Width, num);
				clipRectangle2.Intersect(clipRectangle);
				clipRectangle3.Intersect(clipRectangle);
				clipRectangle4.Intersect(clipRectangle);
				clipRectangle5.Intersect(clipRectangle);
				visualStyleRenderer.DrawBackground(e.Graphics, clientRectangle, clipRectangle2, base.HandleInternal);
				visualStyleRenderer.DrawBackground(e.Graphics, clientRectangle, clipRectangle3, base.HandleInternal);
				visualStyleRenderer.DrawBackground(e.Graphics, clientRectangle, clipRectangle4, base.HandleInternal);
				visualStyleRenderer.DrawBackground(e.Graphics, clientRectangle, clipRectangle5, base.HandleInternal);
				using (Pen pen = new Pen(this.BackColor))
				{
					Rectangle rect = bounds;
					int num2 = rect.X;
					rect.X = num2 - 1;
					num2 = rect.Y;
					rect.Y = num2 - 1;
					num2 = rect.Width;
					rect.Width = num2 + 1;
					num2 = rect.Height;
					rect.Height = num2 + 1;
					e.Graphics.DrawRectangle(pen, rect);
					goto IL_249;
				}
			}
			using (Pen pen2 = new Pen(this.BackColor, (float)(base.Enabled ? 2 : 1)))
			{
				Rectangle rect2 = bounds;
				rect2.Inflate(1, 1);
				if (!base.Enabled)
				{
					int num2 = rect2.X;
					rect2.X = num2 - 1;
					num2 = rect2.Y;
					rect2.Y = num2 - 1;
					num2 = rect2.Width;
					rect2.Width = num2 + 1;
					num2 = rect2.Height;
					rect2.Height = num2 + 1;
				}
				e.Graphics.DrawRectangle(pen2, rect2);
			}
			IL_249:
			if (!base.Enabled && this.BorderStyle != BorderStyle.None && !this.upDownEdit.ShouldSerializeBackColor())
			{
				bounds.Inflate(1, 1);
				ControlPaint.DrawBorder(e.Graphics, bounds, SystemColors.Control, ButtonBorderStyle.Solid);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00012138 File Offset: 0x00010338
		protected virtual void OnTextBoxKeyDown(object source, KeyEventArgs e)
		{
			this.OnKeyDown(e);
			if (this.interceptArrowKeys)
			{
				if (e.KeyData == Keys.Up)
				{
					this.UpButton();
					e.Handled = true;
				}
				else if (e.KeyData == Keys.Down)
				{
					this.DownButton();
					e.Handled = true;
				}
			}
			if (e.KeyCode == Keys.Return && this.UserEdit)
			{
				this.ValidateEditText();
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001219C File Offset: 0x0001039C
		protected virtual void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
		{
			this.OnKeyPress(e);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000121A5 File Offset: 0x000103A5
		protected virtual void OnTextBoxLostFocus(object source, EventArgs e)
		{
			if (this.UserEdit)
			{
				this.ValidateEditText();
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000121B5 File Offset: 0x000103B5
		protected virtual void OnTextBoxResize(object source, EventArgs e)
		{
			base.Height = this.PreferredHeight;
			this.PositionControls();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000121C9 File Offset: 0x000103C9
		protected virtual void OnTextBoxTextChanged(object source, EventArgs e)
		{
			if (this.changingText)
			{
				this.ChangingText = false;
			}
			else
			{
				this.UserEdit = true;
			}
			this.OnTextChanged(e);
			this.OnChanged(source, new EventArgs());
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnStartTimer()
		{
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnStopTimer()
		{
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000121F6 File Offset: 0x000103F6
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Clicks == 2 && e.Button == MouseButtons.Left)
			{
				this.doubleClickFired = true;
			}
			base.OnMouseDown(e);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001221C File Offset: 0x0001041C
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left)
			{
				Point point = base.PointToScreen(new Point(mevent.X, mevent.Y));
				if (UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle && !base.ValidationCancelled)
				{
					if (!this.doubleClickFired)
					{
						this.OnClick(mevent);
						this.OnMouseClick(mevent);
					}
					else
					{
						this.doubleClickFired = false;
						this.OnDoubleClick(mevent);
						this.OnMouseDoubleClick(mevent);
					}
				}
				this.doubleClickFired = false;
			}
			base.OnMouseUp(mevent);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000122B4 File Offset: 0x000104B4
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
			if (handledMouseEventArgs != null)
			{
				if (handledMouseEventArgs.Handled)
				{
					return;
				}
				handledMouseEventArgs.Handled = true;
			}
			if ((Control.ModifierKeys & (Keys.Shift | Keys.Alt)) != Keys.None || Control.MouseButtons != MouseButtons.None)
			{
				return;
			}
			int num = SystemInformation.MouseWheelScrollLines;
			if (num == 0)
			{
				return;
			}
			this.wheelDelta += e.Delta;
			float num2 = (float)this.wheelDelta / 120f;
			if (num == -1)
			{
				num = 1;
			}
			int num3 = (int)((float)num * num2);
			if (num3 != 0)
			{
				if (num3 > 0)
				{
					for (int i = num3; i > 0; i--)
					{
						this.UpButton();
					}
					this.wheelDelta -= (int)((float)num3 * (120f / (float)num));
					return;
				}
				for (int i = -num3; i > 0; i--)
				{
					this.DownButton();
				}
				this.wheelDelta -= (int)((float)num3 * (120f / (float)num));
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00012395 File Offset: 0x00010595
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.PositionControls();
			base.OnLayout(e);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000123A4 File Offset: 0x000105A4
		protected override void OnFontChanged(EventArgs e)
		{
			base.FontHeight = -1;
			base.Height = this.PreferredHeight;
			this.PositionControls();
			base.OnFontChanged(e);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000123C6 File Offset: 0x000105C6
		private void OnUpDown(object source, UpDownEventArgs e)
		{
			if (e.ButtonID == 1)
			{
				this.UpButton();
				return;
			}
			if (e.ButtonID == 2)
			{
				this.DownButton();
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000123E8 File Offset: 0x000105E8
		private void PositionControls()
		{
			Rectangle bounds = Rectangle.Empty;
			Rectangle empty = Rectangle.Empty;
			Rectangle rectangle = new Rectangle(Point.Empty, base.ClientSize);
			int width = rectangle.Width;
			bool renderWithVisualStyles = Application.RenderWithVisualStyles;
			BorderStyle borderStyle = this.BorderStyle;
			int num = (borderStyle == BorderStyle.None) ? 0 : 2;
			rectangle.Inflate(-num, -num);
			if (this.upDownEdit != null)
			{
				bounds = rectangle;
				bounds.Size = new Size(rectangle.Width - this.defaultButtonsWidth, rectangle.Height);
			}
			if (this.upDownButtons != null)
			{
				int num2 = renderWithVisualStyles ? 1 : 2;
				if (borderStyle == BorderStyle.None)
				{
					num2 = 0;
				}
				empty = new Rectangle(rectangle.Right - this.defaultButtonsWidth + num2, rectangle.Top - num2, this.defaultButtonsWidth, rectangle.Height + num2 * 2);
			}
			LeftRightAlignment align = this.UpDownAlign;
			if (base.RtlTranslateLeftRight(align) == LeftRightAlignment.Left)
			{
				empty.X = width - empty.Right;
				bounds.X = width - bounds.Right;
			}
			if (this.upDownEdit != null)
			{
				this.upDownEdit.Bounds = bounds;
			}
			if (this.upDownButtons != null)
			{
				this.upDownButtons.Bounds = empty;
				this.upDownButtons.Invalidate();
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00012522 File Offset: 0x00010722
		public void Select(int start, int length)
		{
			this.upDownEdit.Select(start, length);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00012534 File Offset: 0x00010734
		private MouseEventArgs TranslateMouseEvent(Control child, MouseEventArgs e)
		{
			if (child != null && base.IsHandleCreated)
			{
				NativeMethods.POINT point = new NativeMethods.POINT(e.X, e.Y);
				UnsafeNativeMethods.MapWindowPoints(new HandleRef(child, child.Handle), new HandleRef(this, base.Handle), point, 1);
				return new MouseEventArgs(e.Button, e.Clicks, point.x, point.y, e.Delta);
			}
			return e;
		}

		// Token: 0x0600053A RID: 1338
		public abstract void UpButton();

		// Token: 0x0600053B RID: 1339
		protected abstract void UpdateEditText();

		// Token: 0x0600053C RID: 1340 RVA: 0x000125A3 File Offset: 0x000107A3
		private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			if (pref.Category == UserPreferenceCategory.Locale)
			{
				this.UpdateEditText();
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void ValidateEditText()
		{
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000125B8 File Offset: 0x000107B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 7)
			{
				if (msg != 8)
				{
					base.WndProc(ref m);
					return;
				}
				this.DefWndProc(ref m);
				return;
			}
			else
			{
				if (base.HostedInWin32DialogManager)
				{
					if (this.TextBox.CanFocus)
					{
						UnsafeNativeMethods.SetFocus(new HandleRef(this.TextBox, this.TextBox.Handle));
					}
					base.WndProc(ref m);
					return;
				}
				if (base.ActiveControl == null)
				{
					base.SetActiveControlInternal(this.TextBox);
					return;
				}
				base.FocusActiveControlInternal();
				return;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001263A File Offset: 0x0001083A
		internal void SetToolTip(ToolTip toolTip, string caption)
		{
			toolTip.SetToolTip(this.upDownEdit, caption);
			toolTip.SetToolTip(this.upDownButtons, caption);
		}

		// Token: 0x040004B7 RID: 1207
		private const int DefaultWheelScrollLinesPerPage = 1;

		// Token: 0x040004B8 RID: 1208
		private const int DefaultButtonsWidth = 16;

		// Token: 0x040004B9 RID: 1209
		private const int DefaultControlWidth = 120;

		// Token: 0x040004BA RID: 1210
		private const int ThemedBorderWidth = 1;

		// Token: 0x040004BB RID: 1211
		private const BorderStyle DefaultBorderStyle = BorderStyle.Fixed3D;

		// Token: 0x040004BC RID: 1212
		private static readonly bool DefaultInterceptArrowKeys = true;

		// Token: 0x040004BD RID: 1213
		private const LeftRightAlignment DefaultUpDownAlign = LeftRightAlignment.Right;

		// Token: 0x040004BE RID: 1214
		private const int DefaultTimerInterval = 500;

		// Token: 0x040004BF RID: 1215
		internal UpDownBase.UpDownEdit upDownEdit;

		// Token: 0x040004C0 RID: 1216
		internal UpDownBase.UpDownButtons upDownButtons;

		// Token: 0x040004C1 RID: 1217
		private bool interceptArrowKeys = UpDownBase.DefaultInterceptArrowKeys;

		// Token: 0x040004C2 RID: 1218
		private LeftRightAlignment upDownAlign = LeftRightAlignment.Right;

		// Token: 0x040004C3 RID: 1219
		private bool userEdit;

		// Token: 0x040004C4 RID: 1220
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x040004C5 RID: 1221
		private int wheelDelta;

		// Token: 0x040004C6 RID: 1222
		private bool changingText;

		// Token: 0x040004C7 RID: 1223
		private bool doubleClickFired;

		// Token: 0x040004C8 RID: 1224
		internal int defaultButtonsWidth = 16;

		// Token: 0x02000558 RID: 1368
		internal class UpDownEdit : TextBox
		{
			// Token: 0x060055B6 RID: 21942 RVA: 0x001673E4 File Offset: 0x001655E4
			internal UpDownEdit(UpDownBase parent)
			{
				base.SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
				base.SetStyle(ControlStyles.Selectable, false);
				this.parent = parent;
			}

			// Token: 0x17001491 RID: 5265
			// (get) Token: 0x060055B7 RID: 21943 RVA: 0x00167408 File Offset: 0x00165608
			// (set) Token: 0x060055B8 RID: 21944 RVA: 0x00167410 File Offset: 0x00165610
			public override string Text
			{
				get
				{
					return base.Text;
				}
				set
				{
					bool flag = value != base.Text;
					base.Text = value;
					if (flag && AccessibilityImprovements.Level1)
					{
						base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
					}
				}
			}

			// Token: 0x060055B9 RID: 21945 RVA: 0x00167447 File Offset: 0x00165647
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level5)
				{
					return new UpDownBase.UpDownEdit.UpDownEditAccessibleObjectLevel5(this, this.parent);
				}
				return new UpDownBase.UpDownEdit.UpDownEditAccessibleObject(this, this.parent);
			}

			// Token: 0x060055BA RID: 21946 RVA: 0x00167469 File Offset: 0x00165669
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (e.Clicks == 2 && e.Button == MouseButtons.Left)
				{
					this.doubleClickFired = true;
				}
				this.parent.OnMouseDown(this.parent.TranslateMouseEvent(this, e));
			}

			// Token: 0x060055BB RID: 21947 RVA: 0x001674A0 File Offset: 0x001656A0
			protected override void OnMouseUp(MouseEventArgs e)
			{
				Point p = new Point(e.X, e.Y);
				p = base.PointToScreen(p);
				MouseEventArgs e2 = this.parent.TranslateMouseEvent(this, e);
				if (e.Button == MouseButtons.Left)
				{
					if (!this.parent.ValidationCancelled && UnsafeNativeMethods.WindowFromPoint(p.X, p.Y) == base.Handle)
					{
						if (!this.doubleClickFired)
						{
							this.parent.OnClick(e2);
							this.parent.OnMouseClick(e2);
						}
						else
						{
							this.doubleClickFired = false;
							this.parent.OnDoubleClick(e2);
							this.parent.OnMouseDoubleClick(e2);
						}
					}
					this.doubleClickFired = false;
				}
				this.parent.OnMouseUp(e2);
			}

			// Token: 0x060055BC RID: 21948 RVA: 0x00167564 File Offset: 0x00165764
			internal override void WmContextMenu(ref Message m)
			{
				if (this.ContextMenu == null && this.ContextMenuStrip != null)
				{
					base.WmContextMenu(ref m, this.parent);
					return;
				}
				base.WmContextMenu(ref m, this);
			}

			// Token: 0x060055BD RID: 21949 RVA: 0x0016758C File Offset: 0x0016578C
			protected override void OnKeyUp(KeyEventArgs e)
			{
				this.parent.OnKeyUp(e);
			}

			// Token: 0x060055BE RID: 21950 RVA: 0x0016759A File Offset: 0x0016579A
			protected override void OnGotFocus(EventArgs e)
			{
				this.parent.SetActiveControlInternal(this);
				this.parent.InvokeGotFocus(this.parent, e);
			}

			// Token: 0x060055BF RID: 21951 RVA: 0x001675BA File Offset: 0x001657BA
			protected override void OnLostFocus(EventArgs e)
			{
				this.parent.InvokeLostFocus(this.parent, e);
			}

			// Token: 0x04003832 RID: 14386
			private UpDownBase parent;

			// Token: 0x04003833 RID: 14387
			private bool doubleClickFired;

			// Token: 0x020008A6 RID: 2214
			internal class UpDownEditAccessibleObjectLevel5 : TextBoxBase.TextBoxBaseAccessibleObject
			{
				// Token: 0x06007265 RID: 29285 RVA: 0x001A3FC7 File Offset: 0x001A21C7
				public UpDownEditAccessibleObjectLevel5(UpDownBase.UpDownEdit owner, UpDownBase parent) : base(owner)
				{
					this._parent = parent.AccessibilityObject;
				}

				// Token: 0x17001914 RID: 6420
				// (get) Token: 0x06007266 RID: 29286 RVA: 0x001A3FDC File Offset: 0x001A21DC
				// (set) Token: 0x06007267 RID: 29287 RVA: 0x001A3FE9 File Offset: 0x001A21E9
				public override string Name
				{
					get
					{
						return this._parent.Name;
					}
					set
					{
						this._parent.Name = value;
					}
				}

				// Token: 0x17001915 RID: 6421
				// (get) Token: 0x06007268 RID: 29288 RVA: 0x001A3FF7 File Offset: 0x001A21F7
				public override string KeyboardShortcut
				{
					get
					{
						return this._parent.KeyboardShortcut;
					}
				}

				// Token: 0x040044DC RID: 17628
				private readonly AccessibleObject _parent;
			}

			// Token: 0x020008A7 RID: 2215
			internal class UpDownEditAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x06007269 RID: 29289 RVA: 0x001A4004 File Offset: 0x001A2204
				public UpDownEditAccessibleObject(UpDownBase.UpDownEdit owner, UpDownBase parent) : base(owner)
				{
					this.parent = parent;
				}

				// Token: 0x0600726A RID: 29290 RVA: 0x001A4014 File Offset: 0x001A2214
				internal override void ClearOwnerControlInternal()
				{
					this.parent = null;
					base.ClearOwnerControlInternal();
				}

				// Token: 0x17001916 RID: 6422
				// (get) Token: 0x0600726B RID: 29291 RVA: 0x001A4023 File Offset: 0x001A2223
				// (set) Token: 0x0600726C RID: 29292 RVA: 0x001A4043 File Offset: 0x001A2243
				public override string Name
				{
					get
					{
						if (base.IsOwnerControlDestroyed())
						{
							return string.Empty;
						}
						return this.parent.AccessibilityObject.Name;
					}
					set
					{
						if (base.IsOwnerControlDestroyed())
						{
							return;
						}
						this.parent.AccessibilityObject.Name = value;
					}
				}

				// Token: 0x17001917 RID: 6423
				// (get) Token: 0x0600726D RID: 29293 RVA: 0x001A405F File Offset: 0x001A225F
				public override string KeyboardShortcut
				{
					get
					{
						if (base.IsOwnerControlDestroyed())
						{
							return string.Empty;
						}
						return this.parent.AccessibilityObject.KeyboardShortcut;
					}
				}

				// Token: 0x040044DD RID: 17629
				private UpDownBase parent;
			}
		}

		// Token: 0x02000559 RID: 1369
		internal class UpDownButtons : Control
		{
			// Token: 0x060055C0 RID: 21952 RVA: 0x001675CE File Offset: 0x001657CE
			internal UpDownButtons(UpDownBase parent)
			{
				base.SetStyle(ControlStyles.Opaque | ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
				base.SetStyle(ControlStyles.Selectable, false);
				this.parent = parent;
			}

			// Token: 0x14000417 RID: 1047
			// (add) Token: 0x060055C1 RID: 21953 RVA: 0x001675F2 File Offset: 0x001657F2
			// (remove) Token: 0x060055C2 RID: 21954 RVA: 0x0016760B File Offset: 0x0016580B
			public event UpDownEventHandler UpDown
			{
				add
				{
					this.upDownEventHandler = (UpDownEventHandler)Delegate.Combine(this.upDownEventHandler, value);
				}
				remove
				{
					this.upDownEventHandler = (UpDownEventHandler)Delegate.Remove(this.upDownEventHandler, value);
				}
			}

			// Token: 0x060055C3 RID: 21955 RVA: 0x00167624 File Offset: 0x00165824
			private void BeginButtonPress(MouseEventArgs e)
			{
				int num = base.Size.Height / 2;
				if (e.Y < num)
				{
					this.pushed = (this.captured = UpDownBase.ButtonID.Up);
					base.Invalidate();
				}
				else
				{
					this.pushed = (this.captured = UpDownBase.ButtonID.Down);
					base.Invalidate();
				}
				base.CaptureInternal = true;
				this.OnUpDown(new UpDownEventArgs((int)this.pushed));
				this.StartTimer();
			}

			// Token: 0x060055C4 RID: 21956 RVA: 0x00167697 File Offset: 0x00165897
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				return new UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject(this);
			}

			// Token: 0x060055C5 RID: 21957 RVA: 0x0016769F File Offset: 0x0016589F
			private void EndButtonPress()
			{
				this.pushed = UpDownBase.ButtonID.None;
				this.captured = UpDownBase.ButtonID.None;
				this.StopTimer();
				base.CaptureInternal = false;
				base.Invalidate();
			}

			// Token: 0x060055C6 RID: 21958 RVA: 0x001676C4 File Offset: 0x001658C4
			protected override void OnMouseDown(MouseEventArgs e)
			{
				this.parent.FocusInternal();
				if (!this.parent.ValidationCancelled && e.Button == MouseButtons.Left)
				{
					this.BeginButtonPress(e);
				}
				if (e.Clicks == 2 && e.Button == MouseButtons.Left)
				{
					this.doubleClickFired = true;
				}
				this.parent.OnMouseDown(this.parent.TranslateMouseEvent(this, e));
			}

			// Token: 0x060055C7 RID: 21959 RVA: 0x00167734 File Offset: 0x00165934
			protected override void OnMouseMove(MouseEventArgs e)
			{
				if (base.Capture)
				{
					Rectangle clientRectangle = base.ClientRectangle;
					clientRectangle.Height /= 2;
					if (this.captured == UpDownBase.ButtonID.Down)
					{
						clientRectangle.Y += clientRectangle.Height;
					}
					if (clientRectangle.Contains(e.X, e.Y))
					{
						if (this.pushed != this.captured)
						{
							this.StartTimer();
							this.pushed = this.captured;
							base.Invalidate();
						}
					}
					else if (this.pushed != UpDownBase.ButtonID.None)
					{
						this.StopTimer();
						this.pushed = UpDownBase.ButtonID.None;
						base.Invalidate();
					}
				}
				Rectangle clientRectangle2 = base.ClientRectangle;
				Rectangle clientRectangle3 = base.ClientRectangle;
				clientRectangle2.Height /= 2;
				clientRectangle3.Y += clientRectangle3.Height / 2;
				if (clientRectangle2.Contains(e.X, e.Y))
				{
					this.mouseOver = UpDownBase.ButtonID.Up;
					base.Invalidate();
				}
				else if (clientRectangle3.Contains(e.X, e.Y))
				{
					this.mouseOver = UpDownBase.ButtonID.Down;
					base.Invalidate();
				}
				this.parent.OnMouseMove(this.parent.TranslateMouseEvent(this, e));
			}

			// Token: 0x060055C8 RID: 21960 RVA: 0x0016786C File Offset: 0x00165A6C
			protected override void OnMouseUp(MouseEventArgs e)
			{
				if (!this.parent.ValidationCancelled && e.Button == MouseButtons.Left)
				{
					this.EndButtonPress();
				}
				Point p = new Point(e.X, e.Y);
				p = base.PointToScreen(p);
				MouseEventArgs e2 = this.parent.TranslateMouseEvent(this, e);
				if (e.Button == MouseButtons.Left)
				{
					if (!this.parent.ValidationCancelled && UnsafeNativeMethods.WindowFromPoint(p.X, p.Y) == base.Handle)
					{
						if (!this.doubleClickFired)
						{
							this.parent.OnClick(e2);
						}
						else
						{
							this.doubleClickFired = false;
							this.parent.OnDoubleClick(e2);
							this.parent.OnMouseDoubleClick(e2);
						}
					}
					this.doubleClickFired = false;
				}
				this.parent.OnMouseUp(e2);
			}

			// Token: 0x060055C9 RID: 21961 RVA: 0x00167944 File Offset: 0x00165B44
			protected override void OnMouseLeave(EventArgs e)
			{
				this.mouseOver = UpDownBase.ButtonID.None;
				base.Invalidate();
				this.parent.OnMouseLeave(e);
			}

			// Token: 0x060055CA RID: 21962 RVA: 0x00167960 File Offset: 0x00165B60
			protected override void OnPaint(PaintEventArgs e)
			{
				int num = base.ClientSize.Height / 2;
				if (Application.RenderWithVisualStyles)
				{
					VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer((this.mouseOver == UpDownBase.ButtonID.Up) ? VisualStyleElement.Spin.Up.Hot : VisualStyleElement.Spin.Up.Normal);
					if (!base.Enabled)
					{
						visualStyleRenderer.SetParameters(VisualStyleElement.Spin.Up.Disabled);
					}
					else if (this.pushed == UpDownBase.ButtonID.Up)
					{
						visualStyleRenderer.SetParameters(VisualStyleElement.Spin.Up.Pressed);
					}
					visualStyleRenderer.DrawBackground(e.Graphics, new Rectangle(0, 0, this.parent.defaultButtonsWidth, num), base.HandleInternal);
					if (!base.Enabled)
					{
						visualStyleRenderer.SetParameters(VisualStyleElement.Spin.Down.Disabled);
					}
					else if (this.pushed == UpDownBase.ButtonID.Down)
					{
						visualStyleRenderer.SetParameters(VisualStyleElement.Spin.Down.Pressed);
					}
					else
					{
						visualStyleRenderer.SetParameters((this.mouseOver == UpDownBase.ButtonID.Down) ? VisualStyleElement.Spin.Down.Hot : VisualStyleElement.Spin.Down.Normal);
					}
					visualStyleRenderer.DrawBackground(e.Graphics, new Rectangle(0, num, this.parent.defaultButtonsWidth, num), base.HandleInternal);
				}
				else
				{
					ControlPaint.DrawScrollButton(e.Graphics, new Rectangle(0, 0, this.parent.defaultButtonsWidth, num), ScrollButton.Up, (this.pushed == UpDownBase.ButtonID.Up) ? ButtonState.Pushed : (base.Enabled ? ButtonState.Normal : ButtonState.Inactive));
					ControlPaint.DrawScrollButton(e.Graphics, new Rectangle(0, num, this.parent.defaultButtonsWidth, num), ScrollButton.Down, (this.pushed == UpDownBase.ButtonID.Down) ? ButtonState.Pushed : (base.Enabled ? ButtonState.Normal : ButtonState.Inactive));
				}
				if (num != (base.ClientSize.Height + 1) / 2)
				{
					using (Pen pen = new Pen(this.parent.BackColor))
					{
						Rectangle clientRectangle = base.ClientRectangle;
						e.Graphics.DrawLine(pen, clientRectangle.Left, clientRectangle.Bottom - 1, clientRectangle.Right - 1, clientRectangle.Bottom - 1);
					}
				}
				base.OnPaint(e);
			}

			// Token: 0x060055CB RID: 21963 RVA: 0x00167B5C File Offset: 0x00165D5C
			protected virtual void OnUpDown(UpDownEventArgs upevent)
			{
				if (this.upDownEventHandler != null)
				{
					this.upDownEventHandler(this, upevent);
				}
			}

			// Token: 0x060055CC RID: 21964 RVA: 0x00167B74 File Offset: 0x00165D74
			protected void StartTimer()
			{
				this.parent.OnStartTimer();
				if (this.timer == null)
				{
					this.timer = new Timer();
					this.timer.Tick += this.TimerHandler;
				}
				this.timerInterval = 500;
				this.timer.Interval = this.timerInterval;
				this.timer.Start();
			}

			// Token: 0x060055CD RID: 21965 RVA: 0x00167BDD File Offset: 0x00165DDD
			protected void StopTimer()
			{
				if (this.timer != null)
				{
					this.timer.Stop();
					this.timer.Dispose();
					this.timer = null;
				}
				this.parent.OnStopTimer();
			}

			// Token: 0x060055CE RID: 21966 RVA: 0x00167C10 File Offset: 0x00165E10
			private void TimerHandler(object source, EventArgs args)
			{
				if (!base.Capture)
				{
					this.EndButtonPress();
					return;
				}
				this.OnUpDown(new UpDownEventArgs((int)this.pushed));
				if (this.timer != null)
				{
					this.timerInterval *= 7;
					this.timerInterval /= 10;
					if (this.timerInterval < 1)
					{
						this.timerInterval = 1;
					}
					this.timer.Interval = this.timerInterval;
				}
			}

			// Token: 0x04003834 RID: 14388
			private UpDownBase parent;

			// Token: 0x04003835 RID: 14389
			private UpDownBase.ButtonID pushed;

			// Token: 0x04003836 RID: 14390
			private UpDownBase.ButtonID captured;

			// Token: 0x04003837 RID: 14391
			private UpDownBase.ButtonID mouseOver;

			// Token: 0x04003838 RID: 14392
			private UpDownEventHandler upDownEventHandler;

			// Token: 0x04003839 RID: 14393
			private Timer timer;

			// Token: 0x0400383A RID: 14394
			private int timerInterval;

			// Token: 0x0400383B RID: 14395
			private bool doubleClickFired;

			// Token: 0x020008A8 RID: 2216
			internal class UpDownButtonsAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x0600726E RID: 29294 RVA: 0x0009B963 File Offset: 0x00099B63
				public UpDownButtonsAccessibleObject(UpDownBase.UpDownButtons owner) : base(owner)
				{
				}

				// Token: 0x17001918 RID: 6424
				// (get) Token: 0x0600726F RID: 29295 RVA: 0x001A4080 File Offset: 0x001A2280
				// (set) Token: 0x06007270 RID: 29296 RVA: 0x0001106B File Offset: 0x0000F26B
				public override string Name
				{
					get
					{
						if (base.IsOwnerControlDestroyed())
						{
							return string.Empty;
						}
						string name = base.Name;
						if (name != null && name.Length != 0)
						{
							return name;
						}
						if (AccessibilityImprovements.Level3)
						{
							return base.Owner.ParentInternal.GetType().Name;
						}
						return SR.GetString("SpinnerAccessibleName");
					}
					set
					{
						base.Name = value;
					}
				}

				// Token: 0x17001919 RID: 6425
				// (get) Token: 0x06007271 RID: 29297 RVA: 0x001A40D8 File Offset: 0x001A22D8
				public override AccessibleRole Role
				{
					get
					{
						if (base.IsOwnerControlDestroyed())
						{
							return AccessibleRole.SpinButton;
						}
						AccessibleRole accessibleRole = base.Owner.AccessibleRole;
						if (accessibleRole != AccessibleRole.Default)
						{
							return accessibleRole;
						}
						return AccessibleRole.SpinButton;
					}
				}

				// Token: 0x1700191A RID: 6426
				// (get) Token: 0x06007272 RID: 29298 RVA: 0x001A4104 File Offset: 0x001A2304
				private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject UpButton
				{
					get
					{
						if (this.upButton == null)
						{
							this.upButton = new UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject(this, true);
						}
						return this.upButton;
					}
				}

				// Token: 0x1700191B RID: 6427
				// (get) Token: 0x06007273 RID: 29299 RVA: 0x001A4121 File Offset: 0x001A2321
				private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject DownButton
				{
					get
					{
						if (this.downButton == null)
						{
							this.downButton = new UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject(this, false);
						}
						return this.downButton;
					}
				}

				// Token: 0x06007274 RID: 29300 RVA: 0x001A413E File Offset: 0x001A233E
				public override AccessibleObject GetChild(int index)
				{
					if (index == 0)
					{
						return this.UpButton;
					}
					if (index == 1)
					{
						return this.DownButton;
					}
					return null;
				}

				// Token: 0x06007275 RID: 29301 RVA: 0x0001627D File Offset: 0x0001447D
				public override int GetChildCount()
				{
					return 2;
				}

				// Token: 0x040044DE RID: 17630
				private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject upButton;

				// Token: 0x040044DF RID: 17631
				private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject downButton;

				// Token: 0x02000983 RID: 2435
				internal class DirectionButtonAccessibleObject : AccessibleObject
				{
					// Token: 0x060075AC RID: 30124 RVA: 0x001A9E04 File Offset: 0x001A8004
					public DirectionButtonAccessibleObject(UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject parent, bool up)
					{
						this.parent = parent;
						this.up = up;
					}

					// Token: 0x17001B00 RID: 6912
					// (get) Token: 0x060075AD RID: 30125 RVA: 0x001A9E1C File Offset: 0x001A801C
					public override Rectangle Bounds
					{
						get
						{
							if (this.parent.IsOwnerControlDestroyed())
							{
								return Rectangle.Empty;
							}
							Rectangle bounds = ((UpDownBase.UpDownButtons)this.parent.Owner).Bounds;
							bounds.Height /= 2;
							if (!this.up)
							{
								bounds.Y += bounds.Height;
							}
							return ((UpDownBase.UpDownButtons)this.parent.Owner).ParentInternal.RectangleToScreen(bounds);
						}
					}

					// Token: 0x17001B01 RID: 6913
					// (get) Token: 0x060075AE RID: 30126 RVA: 0x001A9E99 File Offset: 0x001A8099
					// (set) Token: 0x060075AF RID: 30127 RVA: 0x000072B6 File Offset: 0x000054B6
					public override string Name
					{
						get
						{
							if (this.up)
							{
								return SR.GetString("UpDownBaseUpButtonAccName");
							}
							return SR.GetString("UpDownBaseDownButtonAccName");
						}
						set
						{
						}
					}

					// Token: 0x17001B02 RID: 6914
					// (get) Token: 0x060075B0 RID: 30128 RVA: 0x001A9EB8 File Offset: 0x001A80B8
					public override AccessibleObject Parent
					{
						[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
						get
						{
							return this.parent;
						}
					}

					// Token: 0x17001B03 RID: 6915
					// (get) Token: 0x060075B1 RID: 30129 RVA: 0x0015F2AD File Offset: 0x0015D4AD
					public override AccessibleRole Role
					{
						get
						{
							return AccessibleRole.PushButton;
						}
					}

					// Token: 0x040047DA RID: 18394
					private bool up;

					// Token: 0x040047DB RID: 18395
					private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject parent;
				}
			}
		}

		// Token: 0x0200055A RID: 1370
		internal enum ButtonID
		{
			// Token: 0x0400383D RID: 14397
			None,
			// Token: 0x0400383E RID: 14398
			Up,
			// Token: 0x0400383F RID: 14399
			Down
		}
	}
}
