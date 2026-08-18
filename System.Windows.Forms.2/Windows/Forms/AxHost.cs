using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	// Token: 0x0200012E RID: 302
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultEvent("Enter")]
	[Designer("System.Windows.Forms.Design.AxHostDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class AxHost : Control, ISupportInitialize, ICustomTypeDescriptor
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0001A004 File Offset: 0x00018204
		protected AxHost(string clsid) : this(clsid, 0)
		{
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001A010 File Offset: 0x00018210
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected AxHost(string clsid, int flags)
		{
			if (Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("AXMTAThread", new object[]
				{
					clsid
				}));
			}
			this.oleSite = new AxHost.OleInterfaces(this);
			this.selectionChangeHandler = new EventHandler(this.OnNewSelection);
			this.clsid = new Guid(clsid);
			this.flags = flags;
			this.axState[AxHost.assignUniqueID] = !base.GetType().GUID.Equals(AxHost.comctlImageCombo_Clsid);
			this.axState[AxHost.needLicenseKey] = true;
			this.axState[AxHost.rejectSelection] = true;
			this.isMaskEdit = this.clsid.Equals(AxHost.maskEdit_Clsid);
			this.onContainerVisibleChanged = new EventHandler(this.OnContainerVisibleChanged);
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0001A12C File Offset: 0x0001832C
		private bool CanUIActivate
		{
			get
			{
				return this.IsUserMode() || this.editMode != 0;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0001A144 File Offset: 0x00018344
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				if (this.axState[AxHost.fOwnWindow] && this.IsUserMode())
				{
					createParams.Style &= -268435457;
				}
				return createParams;
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001A185 File Offset: 0x00018385
		private bool GetAxState(int mask)
		{
			return this.axState[mask];
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001A193 File Offset: 0x00018393
		private void SetAxState(int mask, bool value)
		{
			this.axState[mask] = value;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void AttachInterfaces()
		{
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001A1A4 File Offset: 0x000183A4
		private void RealizeStyles()
		{
			base.SetStyle(ControlStyles.UserPaint, false);
			int num = 0;
			int miscStatus = this.GetOleObject().GetMiscStatus(1, out num);
			if (!NativeMethods.Failed(miscStatus))
			{
				this.miscStatusBits = num;
				this.ParseMiscBits(this.miscStatusBits);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001A1E5 File Offset: 0x000183E5
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x00012F98 File Offset: 0x00011198
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
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

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0001A1F5 File Offset: 0x000183F5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060009AB RID: 2475 RVA: 0x0001A1FE File Offset: 0x000183FE
		// (remove) Token: 0x060009AC RID: 2476 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseClick
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"MouseClick"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060009AD RID: 2477 RVA: 0x0001A21D File Offset: 0x0001841D
		// (remove) Token: 0x060009AE RID: 2478 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseDoubleClick
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"MouseDoubleClick"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001A23C File Offset: 0x0001843C
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0001A244 File Offset: 0x00018444
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00011B2D File Offset: 0x0000FD2D
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0001A24D File Offset: 0x0001844D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001A256 File Offset: 0x00018456
		protected override Size DefaultSize
		{
			get
			{
				return new Size(75, 23);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0001A261 File Offset: 0x00018461
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x0001A269 File Offset: 0x00018469
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new virtual bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x0001A27A File Offset: 0x0001847A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00013238 File Offset: 0x00011438
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0001A28C File Offset: 0x0001848C
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x0001A2A4 File Offset: 0x000184A4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Localizable(true)]
		public new virtual bool RightToLeft
		{
			get
			{
				RightToLeft rightToLeft = base.RightToLeft;
				return rightToLeft == System.Windows.Forms.RightToLeft.Yes;
			}
			set
			{
				base.RightToLeft = (value ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0001A2B3 File Offset: 0x000184B3
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x0001A2BB File Offset: 0x000184BB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001A2C4 File Offset: 0x000184C4
		internal override bool CanAccessProperties
		{
			get
			{
				int num = this.GetOcState();
				return (this.axState[AxHost.fOwnWindow] && (num > 2 || (this.IsUserMode() && num >= 2))) || num >= 4;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001A303 File Offset: 0x00018503
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected bool PropsValid()
		{
			return this.CanAccessProperties;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void BeginInit()
		{
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001A30C File Offset: 0x0001850C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndInit()
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.CreateControl(true);
				ContainerControl containerControl = this.ContainingControl;
				if (containerControl != null)
				{
					containerControl.VisibleChanged += this.onContainerVisibleChanged;
				}
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001A344 File Offset: 0x00018544
		private void OnContainerVisibleChanged(object sender, EventArgs e)
		{
			ContainerControl containerControl = this.ContainingControl;
			if (containerControl != null)
			{
				if (containerControl.Visible && base.Visible && !this.axState[AxHost.fOwnWindow])
				{
					this.MakeVisibleWithShow();
					return;
				}
				if (!containerControl.Visible && base.Visible && base.IsHandleCreated && this.GetOcState() >= 4)
				{
					this.HideAxControl();
					return;
				}
				if (containerControl.Visible && !base.GetState(2) && base.IsHandleCreated && this.GetOcState() >= 4)
				{
					this.HideAxControl();
				}
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0001A3D4 File Offset: 0x000185D4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EditMode
		{
			get
			{
				return this.editMode != 0;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001A3DF File Offset: 0x000185DF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasAboutBox
		{
			get
			{
				return this.aboutBoxDelegate != null;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0001A3EA File Offset: 0x000185EA
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0001A3F2 File Offset: 0x000185F2
		private int NoComponentChangeEvents
		{
			get
			{
				return this.noComponentChange;
			}
			set
			{
				this.noComponentChange = value;
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001A3FB File Offset: 0x000185FB
		public void ShowAboutBox()
		{
			if (this.aboutBoxDelegate != null)
			{
				this.aboutBoxDelegate();
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060009C8 RID: 2504 RVA: 0x0001A410 File Offset: 0x00018610
		// (remove) Token: 0x060009C9 RID: 2505 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"BackColorChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060009CA RID: 2506 RVA: 0x0001A42F File Offset: 0x0001862F
		// (remove) Token: 0x060009CB RID: 2507 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"BackgroundImageChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060009CC RID: 2508 RVA: 0x0001A44E File Offset: 0x0001864E
		// (remove) Token: 0x060009CD RID: 2509 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"BackgroundImageLayoutChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060009CE RID: 2510 RVA: 0x0001A46D File Offset: 0x0001866D
		// (remove) Token: 0x060009CF RID: 2511 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BindingContextChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"BindingContextChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060009D0 RID: 2512 RVA: 0x0001A48C File Offset: 0x0001868C
		// (remove) Token: 0x060009D1 RID: 2513 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"ContextMenuChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060009D2 RID: 2514 RVA: 0x0001A4AB File Offset: 0x000186AB
		// (remove) Token: 0x060009D3 RID: 2515 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CursorChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"CursorChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060009D4 RID: 2516 RVA: 0x0001A4CA File Offset: 0x000186CA
		// (remove) Token: 0x060009D5 RID: 2517 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"EnabledChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060009D6 RID: 2518 RVA: 0x0001A4E9 File Offset: 0x000186E9
		// (remove) Token: 0x060009D7 RID: 2519 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"FontChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060009D8 RID: 2520 RVA: 0x0001A508 File Offset: 0x00018708
		// (remove) Token: 0x060009D9 RID: 2521 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"ForeColorChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060009DA RID: 2522 RVA: 0x0001A527 File Offset: 0x00018727
		// (remove) Token: 0x060009DB RID: 2523 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"RightToLeftChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060009DC RID: 2524 RVA: 0x0001A546 File Offset: 0x00018746
		// (remove) Token: 0x060009DD RID: 2525 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"TextChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060009DE RID: 2526 RVA: 0x0001A565 File Offset: 0x00018765
		// (remove) Token: 0x060009DF RID: 2527 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Click
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"Click"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060009E0 RID: 2528 RVA: 0x0001A584 File Offset: 0x00018784
		// (remove) Token: 0x060009E1 RID: 2529 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragDrop
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"DragDrop"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060009E2 RID: 2530 RVA: 0x0001A5A3 File Offset: 0x000187A3
		// (remove) Token: 0x060009E3 RID: 2531 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragEnter
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"DragEnter"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060009E4 RID: 2532 RVA: 0x0001A5C2 File Offset: 0x000187C2
		// (remove) Token: 0x060009E5 RID: 2533 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragOver
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"DragOver"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060009E6 RID: 2534 RVA: 0x0001A5E1 File Offset: 0x000187E1
		// (remove) Token: 0x060009E7 RID: 2535 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragLeave
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"DragLeave"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060009E8 RID: 2536 RVA: 0x0001A600 File Offset: 0x00018800
		// (remove) Token: 0x060009E9 RID: 2537 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"GiveFeedback"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060009EA RID: 2538 RVA: 0x0001A61F File Offset: 0x0001881F
		// (remove) Token: 0x060009EB RID: 2539 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event HelpEventHandler HelpRequested
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"HelpRequested"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060009EC RID: 2540 RVA: 0x0001A63E File Offset: 0x0001883E
		// (remove) Token: 0x060009ED RID: 2541 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"Paint"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060009EE RID: 2542 RVA: 0x0001A65D File Offset: 0x0001885D
		// (remove) Token: 0x060009EF RID: 2543 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"QueryContinueDrag"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060009F0 RID: 2544 RVA: 0x0001A67C File Offset: 0x0001887C
		// (remove) Token: 0x060009F1 RID: 2545 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"QueryAccessibilityHelp"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060009F2 RID: 2546 RVA: 0x0001A69B File Offset: 0x0001889B
		// (remove) Token: 0x060009F3 RID: 2547 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DoubleClick
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"DoubleClick"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060009F4 RID: 2548 RVA: 0x0001A6BA File Offset: 0x000188BA
		// (remove) Token: 0x060009F5 RID: 2549 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"ImeModeChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060009F6 RID: 2550 RVA: 0x0001A6D9 File Offset: 0x000188D9
		// (remove) Token: 0x060009F7 RID: 2551 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"KeyDown"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060009F8 RID: 2552 RVA: 0x0001A6F8 File Offset: 0x000188F8
		// (remove) Token: 0x060009F9 RID: 2553 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"KeyPress"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060009FA RID: 2554 RVA: 0x0001A717 File Offset: 0x00018917
		// (remove) Token: 0x060009FB RID: 2555 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"KeyUp"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060009FC RID: 2556 RVA: 0x0001A736 File Offset: 0x00018936
		// (remove) Token: 0x060009FD RID: 2557 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event LayoutEventHandler Layout
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"Layout"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060009FE RID: 2558 RVA: 0x0001A755 File Offset: 0x00018955
		// (remove) Token: 0x060009FF RID: 2559 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"MouseDown"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000A00 RID: 2560 RVA: 0x0001A774 File Offset: 0x00018974
		// (remove) Token: 0x06000A01 RID: 2561 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseEnter
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"MouseEnter"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06000A02 RID: 2562 RVA: 0x0001A793 File Offset: 0x00018993
		// (remove) Token: 0x06000A03 RID: 2563 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseLeave
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"MouseLeave"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000A04 RID: 2564 RVA: 0x0001A7B2 File Offset: 0x000189B2
		// (remove) Token: 0x06000A05 RID: 2565 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseHover
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"MouseHover"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000A06 RID: 2566 RVA: 0x0001A7D1 File Offset: 0x000189D1
		// (remove) Token: 0x06000A07 RID: 2567 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"MouseMove"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000A08 RID: 2568 RVA: 0x0001A7F0 File Offset: 0x000189F0
		// (remove) Token: 0x06000A09 RID: 2569 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"MouseUp"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000A0A RID: 2570 RVA: 0x0001A80F File Offset: 0x00018A0F
		// (remove) Token: 0x06000A0B RID: 2571 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseWheel
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"MouseWheel"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000A0C RID: 2572 RVA: 0x0001A82E File Offset: 0x00018A2E
		// (remove) Token: 0x06000A0D RID: 2573 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event UICuesEventHandler ChangeUICues
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"ChangeUICues"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000A0E RID: 2574 RVA: 0x0001A84D File Offset: 0x00018A4D
		// (remove) Token: 0x06000A0F RID: 2575 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler StyleChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[]
				{
					"StyleChanged"
				}));
			}
			remove
			{
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001A86C File Offset: 0x00018A6C
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.AmbientChanged(-703);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001A880 File Offset: 0x00018A80
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.AmbientChanged(-704);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001A894 File Offset: 0x00018A94
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.AmbientChanged(-701);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001A8A8 File Offset: 0x00018AA8
		private void AmbientChanged(int dispid)
		{
			if (this.GetOcx() != null)
			{
				try
				{
					base.Invalidate();
					this.GetOleControl().OnAmbientPropertyChange(dispid);
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		private bool OwnWindow()
		{
			return this.axState[AxHost.fOwnWindow] || this.axState[AxHost.fFakingWindow];
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001A90E File Offset: 0x00018B0E
		private IntPtr GetHandleNoCreate()
		{
			if (base.IsHandleCreated)
			{
				return base.Handle;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001A924 File Offset: 0x00018B24
		private ISelectionService GetSelectionService()
		{
			return AxHost.GetSelectionService(this);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001A92C File Offset: 0x00018B2C
		private static ISelectionService GetSelectionService(Control ctl)
		{
			ISite site = ctl.Site;
			if (site != null)
			{
				object service = site.GetService(typeof(ISelectionService));
				return service as ISelectionService;
			}
			return null;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001A95C File Offset: 0x00018B5C
		private void AddSelectionHandler()
		{
			if (this.axState[AxHost.addedSelectionHandler])
			{
				return;
			}
			ISelectionService selectionService = this.GetSelectionService();
			if (selectionService != null)
			{
				selectionService.SelectionChanging += this.selectionChangeHandler;
			}
			this.axState[AxHost.addedSelectionHandler] = true;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		private void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			if (e.Component == this)
			{
				UnsafeNativeMethods.IOleControl oleControl = this.GetOcx() as UnsafeNativeMethods.IOleControl;
				if (oleControl != null)
				{
					oleControl.OnAmbientPropertyChange(-702);
				}
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		private bool RemoveSelectionHandler()
		{
			if (!this.axState[AxHost.addedSelectionHandler])
			{
				return false;
			}
			ISelectionService selectionService = this.GetSelectionService();
			if (selectionService != null)
			{
				selectionService.SelectionChanging -= this.selectionChangeHandler;
			}
			this.axState[AxHost.addedSelectionHandler] = false;
			return true;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001AA24 File Offset: 0x00018C24
		private void SyncRenameNotification(bool hook)
		{
			if (base.DesignMode && hook != this.axState[AxHost.renameEventHooked])
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					if (hook)
					{
						componentChangeService.ComponentRename += this.OnComponentRename;
					}
					else
					{
						componentChangeService.ComponentRename -= this.OnComponentRename;
					}
					this.axState[AxHost.renameEventHooked] = hook;
				}
			}
		}

		// Token: 0x170002A3 RID: 675
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x0001AAA0 File Offset: 0x00018CA0
		public override ISite Site
		{
			set
			{
				if (this.axState[AxHost.disposed])
				{
					return;
				}
				bool flag = this.RemoveSelectionHandler();
				bool flag2 = this.IsUserMode();
				this.SyncRenameNotification(false);
				base.Site = value;
				bool flag3 = this.IsUserMode();
				if (!flag3)
				{
					this.GetOcxCreate();
				}
				if (flag)
				{
					this.AddSelectionHandler();
				}
				this.SyncRenameNotification(value != null);
				if (value != null && !flag3 && flag2 != flag3 && this.GetOcState() > 1)
				{
					this.TransitionDownTo(1);
					this.TransitionUpTo(4);
					ContainerControl containerControl = this.ContainingControl;
					if (containerControl != null && containerControl.Visible && base.Visible)
					{
						this.MakeVisibleWithShow();
					}
				}
				if (flag2 != flag3 && !base.IsHandleCreated && !this.axState[AxHost.disposed] && this.GetOcx() != null)
				{
					this.RealizeStyles();
				}
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001AB70 File Offset: 0x00018D70
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLostFocus(EventArgs e)
		{
			bool flag = this.GetHandleNoCreate() != this.hwndFocus;
			if (flag && base.IsHandleCreated)
			{
				flag = !UnsafeNativeMethods.IsChild(new HandleRef(this, this.GetHandleNoCreate()), new HandleRef(null, this.hwndFocus));
			}
			base.OnLostFocus(e);
			if (flag)
			{
				this.UiDeactivate();
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001ABCC File Offset: 0x00018DCC
		private void OnNewSelection(object sender, EventArgs e)
		{
			if (this.IsUserMode())
			{
				return;
			}
			ISelectionService selectionService = this.GetSelectionService();
			if (selectionService != null)
			{
				if (this.GetOcState() >= 8 && !selectionService.GetComponentSelected(this))
				{
					int hr = this.UiDeactivate();
					NativeMethods.Failed(hr);
				}
				if (!selectionService.GetComponentSelected(this))
				{
					if (this.editMode != 0)
					{
						this.GetParentContainer().OnExitEditMode(this);
						this.editMode = 0;
					}
					this.SetSelectionStyle(1);
					this.RemoveSelectionHandler();
					return;
				}
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["SelectionStyle"];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(int))
				{
					int num = (int)propertyDescriptor.GetValue(this);
					if (num != this.selectionStyle)
					{
						propertyDescriptor.SetValue(this, this.selectionStyle);
					}
				}
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001AC95 File Offset: 0x00018E95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			base.DrawToBitmap(bitmap, targetBounds);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		protected override void CreateHandle()
		{
			if (!base.IsHandleCreated)
			{
				this.TransitionUpTo(2);
				if (!this.axState[AxHost.fOwnWindow])
				{
					if (this.axState[AxHost.fNeedOwnWindow])
					{
						this.axState[AxHost.fNeedOwnWindow] = false;
						this.axState[AxHost.fFakingWindow] = true;
						base.CreateHandle();
					}
					else
					{
						this.TransitionUpTo(4);
						if (this.axState[AxHost.fNeedOwnWindow])
						{
							this.CreateHandle();
							return;
						}
					}
				}
				else
				{
					base.SetState(2, false);
					base.CreateHandle();
				}
				this.GetParentContainer().ControlCreated(this);
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001AD47 File Offset: 0x00018F47
		private NativeMethods.COMRECT GetClipRect(NativeMethods.COMRECT clipRect)
		{
			if (clipRect != null)
			{
				AxHost.FillInRect(clipRect, new Rectangle(0, 0, 32000, 32000));
			}
			return clipRect;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001AD68 File Offset: 0x00018F68
		private static int SetupLogPixels(bool force)
		{
			if (AxHost.logPixelsX == -1 || force)
			{
				IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
				if (dc == IntPtr.Zero)
				{
					return -2147467259;
				}
				AxHost.logPixelsX = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
				AxHost.logPixelsY = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 90);
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			}
			return 0;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001ADD8 File Offset: 0x00018FD8
		private void HiMetric2Pixel(NativeMethods.tagSIZEL sz, NativeMethods.tagSIZEL szout)
		{
			NativeMethods._POINTL pointl = new NativeMethods._POINTL();
			pointl.x = sz.cx;
			pointl.y = sz.cy;
			NativeMethods.tagPOINTF tagPOINTF = new NativeMethods.tagPOINTF();
			((UnsafeNativeMethods.IOleControlSite)this.oleSite).TransformCoords(pointl, tagPOINTF, 6);
			szout.cx = (int)tagPOINTF.x;
			szout.cy = (int)tagPOINTF.y;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001AE34 File Offset: 0x00019034
		private void Pixel2hiMetric(NativeMethods.tagSIZEL sz, NativeMethods.tagSIZEL szout)
		{
			NativeMethods.tagPOINTF tagPOINTF = new NativeMethods.tagPOINTF();
			tagPOINTF.x = (float)sz.cx;
			tagPOINTF.y = (float)sz.cy;
			NativeMethods._POINTL pointl = new NativeMethods._POINTL();
			((UnsafeNativeMethods.IOleControlSite)this.oleSite).TransformCoords(pointl, tagPOINTF, 10);
			szout.cx = pointl.x;
			szout.cy = pointl.y;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001AE90 File Offset: 0x00019090
		private static int Pixel2Twip(int v, bool xDirection)
		{
			AxHost.SetupLogPixels(false);
			int num = xDirection ? AxHost.logPixelsX : AxHost.logPixelsY;
			return (int)((double)v / (double)num * 72.0 * 20.0);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001AED0 File Offset: 0x000190D0
		private static int Twip2Pixel(double v, bool xDirection)
		{
			AxHost.SetupLogPixels(false);
			int num = xDirection ? AxHost.logPixelsX : AxHost.logPixelsY;
			return (int)(v / 20.0 / 72.0 * (double)num);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001AF10 File Offset: 0x00019110
		private static int Twip2Pixel(int v, bool xDirection)
		{
			AxHost.SetupLogPixels(false);
			int num = xDirection ? AxHost.logPixelsX : AxHost.logPixelsY;
			return (int)((double)v / 20.0 / 72.0 * (double)num);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001AF50 File Offset: 0x00019150
		private Size SetExtent(int width, int height)
		{
			NativeMethods.tagSIZEL tagSIZEL = new NativeMethods.tagSIZEL();
			tagSIZEL.cx = width;
			tagSIZEL.cy = height;
			bool flag = !this.IsUserMode();
			try
			{
				this.Pixel2hiMetric(tagSIZEL, tagSIZEL);
				this.GetOleObject().SetExtent(1, tagSIZEL);
			}
			catch (COMException)
			{
				flag = true;
			}
			if (flag)
			{
				this.GetOleObject().GetExtent(1, tagSIZEL);
				try
				{
					this.GetOleObject().SetExtent(1, tagSIZEL);
				}
				catch (COMException ex)
				{
				}
			}
			return this.GetExtent();
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001AFE0 File Offset: 0x000191E0
		private Size GetExtent()
		{
			NativeMethods.tagSIZEL tagSIZEL = new NativeMethods.tagSIZEL();
			this.GetOleObject().GetExtent(1, tagSIZEL);
			this.HiMetric2Pixel(tagSIZEL, tagSIZEL);
			return new Size(tagSIZEL.cx, tagSIZEL.cy);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001B01A File Offset: 0x0001921A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			return bounds;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001B01D File Offset: 0x0001921D
		private void SetObjectRects(Rectangle bounds)
		{
			if (this.GetOcState() < 4)
			{
				return;
			}
			this.GetInPlaceObject().SetObjectRects(AxHost.FillInRect(new NativeMethods.COMRECT(), bounds), this.GetClipRect(new NativeMethods.COMRECT()));
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001B04C File Offset: 0x0001924C
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.GetAxState(AxHost.handlePosRectChanged))
			{
				return;
			}
			this.axState[AxHost.handlePosRectChanged] = true;
			Size size = base.ApplySizeConstraints(width, height);
			width = size.Width;
			height = size.Height;
			try
			{
				if (this.axState[AxHost.fFakingWindow])
				{
					base.SetBoundsCore(x, y, width, height, specified);
				}
				else
				{
					Rectangle bounds = base.Bounds;
					if (bounds.X != x || bounds.Y != y || bounds.Width != width || bounds.Height != height)
					{
						if (!base.IsHandleCreated)
						{
							base.UpdateBounds(x, y, width, height);
						}
						else
						{
							if (this.GetOcState() > 2)
							{
								this.CheckSubclassing();
								if (width != bounds.Width || height != bounds.Height)
								{
									Size size2 = this.SetExtent(width, height);
									width = size2.Width;
									height = size2.Height;
								}
							}
							if (this.axState[AxHost.manualUpdate])
							{
								this.SetObjectRects(new Rectangle(x, y, width, height));
								this.CheckSubclassing();
								base.UpdateBounds();
							}
							else
							{
								this.SetObjectRects(new Rectangle(x, y, width, height));
								base.SetBoundsCore(x, y, width, height, specified);
								base.Invalidate();
							}
						}
					}
				}
			}
			finally
			{
				this.axState[AxHost.handlePosRectChanged] = false;
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001B1CC File Offset: 0x000193CC
		private bool CheckSubclassing()
		{
			if (!base.IsHandleCreated || this.wndprocAddr == IntPtr.Zero)
			{
				return true;
			}
			IntPtr handle = base.Handle;
			IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -4);
			if (windowLong == this.wndprocAddr)
			{
				return true;
			}
			if ((int)((long)base.SendMessage(this.REGMSG_MSG, 0, 0)) == 123)
			{
				this.wndprocAddr = windowLong;
				return true;
			}
			base.WindowReleaseHandle();
			UnsafeNativeMethods.SetWindowLong(new HandleRef(this, handle), -4, new HandleRef(this, windowLong));
			base.WindowAssignHandle(handle, this.axState[AxHost.assignUniqueID]);
			this.InformOfNewHandle();
			this.axState[AxHost.manualUpdate] = true;
			return false;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001B287 File Offset: 0x00019487
		protected override void DestroyHandle()
		{
			if (this.axState[AxHost.fOwnWindow])
			{
				base.DestroyHandle();
				return;
			}
			if (base.IsHandleCreated)
			{
				this.TransitionDownTo(2);
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001B2B4 File Offset: 0x000194B4
		private void TransitionDownTo(int state)
		{
			if (this.axState[AxHost.inTransition])
			{
				return;
			}
			try
			{
				this.axState[AxHost.inTransition] = true;
				while (state < this.GetOcState())
				{
					int num = this.GetOcState();
					switch (num)
					{
					case 1:
						this.ReleaseAxControl();
						this.SetOcState(0);
						continue;
					case 2:
						this.StopEvents();
						this.DisposeAxControl();
						this.SetOcState(1);
						continue;
					case 3:
						break;
					case 4:
						if (this.axState[AxHost.fFakingWindow])
						{
							this.DestroyFakeWindow();
							this.SetOcState(2);
						}
						else
						{
							this.InPlaceDeactivate();
						}
						this.SetOcState(2);
						continue;
					default:
						if (num == 8)
						{
							int num2 = this.UiDeactivate();
							this.SetOcState(4);
							continue;
						}
						if (num == 16)
						{
							this.SetOcState(8);
							continue;
						}
						break;
					}
					this.SetOcState(this.GetOcState() - 1);
				}
			}
			finally
			{
				this.axState[AxHost.inTransition] = false;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001B3C0 File Offset: 0x000195C0
		private void TransitionUpTo(int state)
		{
			if (this.axState[AxHost.inTransition])
			{
				return;
			}
			try
			{
				this.axState[AxHost.inTransition] = true;
				while (state > this.GetOcState())
				{
					switch (this.GetOcState())
					{
					case 0:
						this.axState[AxHost.disposed] = false;
						this.GetOcxCreate();
						this.SetOcState(1);
						continue;
					case 1:
						this.ActivateAxControl();
						this.SetOcState(2);
						if (this.IsUserMode())
						{
							this.StartEvents();
							continue;
						}
						continue;
					case 2:
						this.axState[AxHost.ownDisposing] = false;
						if (!this.axState[AxHost.fOwnWindow])
						{
							this.InPlaceActivate();
							if (!base.Visible && this.ContainingControl != null && this.ContainingControl.Visible)
							{
								this.HideAxControl();
							}
							else
							{
								base.CreateControl(true);
								if (!this.IsUserMode() && !this.axState[AxHost.ocxStateSet])
								{
									Size extent = this.GetExtent();
									Rectangle bounds = base.Bounds;
									if (bounds.Size.Equals(this.DefaultSize) && !bounds.Size.Equals(extent))
									{
										bounds.Width = extent.Width;
										bounds.Height = extent.Height;
										base.Bounds = bounds;
									}
								}
							}
						}
						if (this.GetOcState() < 4)
						{
							this.SetOcState(4);
						}
						this.OnInPlaceActive();
						continue;
					case 4:
						this.DoVerb(-1);
						this.SetOcState(8);
						continue;
					}
					this.SetOcState(this.GetOcState() + 1);
				}
			}
			finally
			{
				this.axState[AxHost.inTransition] = false;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnInPlaceActive()
		{
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001B5C0 File Offset: 0x000197C0
		private void InPlaceActivate()
		{
			try
			{
				this.DoVerb(-5);
			}
			catch (Exception inner)
			{
				throw new TargetInvocationException(SR.GetString("AXNohWnd", new object[]
				{
					base.GetType().Name
				}), inner);
			}
			this.EnsureWindowPresent();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001B614 File Offset: 0x00019814
		private void InPlaceDeactivate()
		{
			this.axState[AxHost.ownDisposing] = true;
			ContainerControl containerControl = this.ContainingControl;
			if (containerControl != null && containerControl.ActiveControl == this)
			{
				containerControl.ActiveControl = null;
			}
			try
			{
				this.GetInPlaceObject().InPlaceDeactivate();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001B670 File Offset: 0x00019870
		private void UiActivate()
		{
			if (this.CanUIActivate)
			{
				this.DoVerb(-4);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001B682 File Offset: 0x00019882
		private void DestroyFakeWindow()
		{
			this.axState[AxHost.fFakingWindow] = false;
			base.DestroyHandle();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001B69C File Offset: 0x0001989C
		private void EnsureWindowPresent()
		{
			if (!base.IsHandleCreated)
			{
				try
				{
					((UnsafeNativeMethods.IOleClientSite)this.oleSite).ShowObject();
				}
				catch
				{
				}
			}
			if (base.IsHandleCreated)
			{
				return;
			}
			if (this.ParentInternal != null)
			{
				throw new NotSupportedException(SR.GetString("AXNohWnd", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001B708 File Offset: 0x00019908
		protected override void SetVisibleCore(bool value)
		{
			if (base.GetState(2) != value)
			{
				bool visible = base.Visible;
				if ((base.IsHandleCreated || value) && this.ParentInternal != null && this.ParentInternal.Created && !this.axState[AxHost.fOwnWindow])
				{
					this.TransitionUpTo(2);
					if (value)
					{
						if (this.axState[AxHost.fFakingWindow])
						{
							this.DestroyFakeWindow();
						}
						if (!base.IsHandleCreated)
						{
							try
							{
								this.SetExtent(base.Width, base.Height);
								this.InPlaceActivate();
								base.CreateControl(true);
								goto IL_AE;
							}
							catch
							{
								this.MakeVisibleWithShow();
								goto IL_AE;
							}
						}
						this.MakeVisibleWithShow();
					}
					else
					{
						this.HideAxControl();
					}
				}
				IL_AE:
				if (!value)
				{
					this.axState[AxHost.fNeedOwnWindow] = false;
				}
				if (!this.axState[AxHost.fOwnWindow])
				{
					base.SetState(2, value);
					if (base.Visible != visible)
					{
						this.OnVisibleChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001B818 File Offset: 0x00019A18
		private void MakeVisibleWithShow()
		{
			ContainerControl containerControl = this.ContainingControl;
			Control control = (containerControl == null) ? null : containerControl.ActiveControl;
			try
			{
				this.DoVerb(-1);
			}
			catch (Exception inner)
			{
				throw new TargetInvocationException(SR.GetString("AXNohWnd", new object[]
				{
					base.GetType().Name
				}), inner);
			}
			this.EnsureWindowPresent();
			base.CreateControl(true);
			if (containerControl != null && containerControl.ActiveControl != control)
			{
				containerControl.ActiveControl = control;
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001B898 File Offset: 0x00019A98
		private void HideAxControl()
		{
			this.DoVerb(-3);
			if (this.GetOcState() < 4)
			{
				this.axState[AxHost.fNeedOwnWindow] = true;
				this.SetOcState(4);
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00013062 File Offset: 0x00011262
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool IsInputChar(char charCode)
		{
			return true;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001B8C3 File Offset: 0x00019AC3
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return !this.ignoreDialogKeys && base.ProcessDialogKey(keyData);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001B8D8 File Offset: 0x00019AD8
		public override bool PreProcessMessage(ref Message msg)
		{
			if (this.IsUserMode())
			{
				if (this.axState[AxHost.siteProcessedInputKey])
				{
					return base.PreProcessMessage(ref msg);
				}
				NativeMethods.MSG msg2 = default(NativeMethods.MSG);
				msg2.message = msg.Msg;
				msg2.wParam = msg.WParam;
				msg2.lParam = msg.LParam;
				msg2.hwnd = msg.HWnd;
				this.axState[AxHost.siteProcessedInputKey] = false;
				try
				{
					UnsafeNativeMethods.IOleInPlaceActiveObject inPlaceActiveObject = this.GetInPlaceActiveObject();
					if (inPlaceActiveObject != null)
					{
						int num = inPlaceActiveObject.TranslateAccelerator(ref msg2);
						msg.Msg = msg2.message;
						msg.WParam = msg2.wParam;
						msg.LParam = msg2.lParam;
						msg.HWnd = msg2.hwnd;
						if (num == 0)
						{
							return true;
						}
						if (num == 1)
						{
							bool result = false;
							this.ignoreDialogKeys = true;
							try
							{
								result = base.PreProcessMessage(ref msg);
							}
							finally
							{
								this.ignoreDialogKeys = false;
							}
							return result;
						}
						if (this.axState[AxHost.siteProcessedInputKey])
						{
							return base.PreProcessMessage(ref msg);
						}
						return false;
					}
				}
				finally
				{
					this.axState[AxHost.siteProcessedInputKey] = false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001BA20 File Offset: 0x00019C20
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.CanSelect)
			{
				try
				{
					NativeMethods.tagCONTROLINFO tagCONTROLINFO = new NativeMethods.tagCONTROLINFO();
					int controlInfo = this.GetOleControl().GetControlInfo(tagCONTROLINFO);
					if (NativeMethods.Failed(controlInfo))
					{
						return false;
					}
					NativeMethods.MSG msg = default(NativeMethods.MSG);
					msg.hwnd = ((this.ContainingControl == null) ? IntPtr.Zero : this.ContainingControl.Handle);
					msg.message = 260;
					msg.wParam = (IntPtr)((int)char.ToUpper(charCode, CultureInfo.CurrentCulture));
					msg.lParam = (IntPtr)538443777;
					msg.time = SafeNativeMethods.GetTickCount();
					NativeMethods.POINT point = new NativeMethods.POINT();
					UnsafeNativeMethods.GetCursorPos(point);
					msg.pt_x = point.x;
					msg.pt_y = point.y;
					if (SafeNativeMethods.IsAccelerator(new HandleRef(tagCONTROLINFO, tagCONTROLINFO.hAccel), (int)tagCONTROLINFO.cAccel, ref msg, null))
					{
						this.GetOleControl().OnMnemonic(ref msg);
						base.Focus();
						return true;
					}
				}
				catch (Exception ex)
				{
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001BB40 File Offset: 0x00019D40
		protected void SetAboutBoxDelegate(AxHost.AboutBoxDelegate d)
		{
			this.aboutBoxDelegate = (AxHost.AboutBoxDelegate)Delegate.Combine(this.aboutBoxDelegate, d);
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0001BB59 File Offset: 0x00019D59
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0001BB84 File Offset: 0x00019D84
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AxHost.State OcxState
		{
			get
			{
				if (this.IsDirty() || this.ocxState == null)
				{
					this.ocxState = this.CreateNewOcxState(this.ocxState);
				}
				return this.ocxState;
			}
			set
			{
				this.axState[AxHost.ocxStateSet] = true;
				if (value == null)
				{
					return;
				}
				if (this.storageType != -1 && this.storageType != value.type)
				{
					throw new InvalidOperationException(SR.GetString("AXOcxStateLoaded"));
				}
				if (this.ocxState == value)
				{
					return;
				}
				this.ocxState = value;
				if (this.ocxState != null)
				{
					this.axState[AxHost.manualUpdate] = this.ocxState._GetManualUpdate();
					this.licenseKey = this.ocxState._GetLicenseKey();
				}
				else
				{
					this.axState[AxHost.manualUpdate] = false;
					this.licenseKey = null;
				}
				if (this.ocxState != null && this.GetOcState() >= 2)
				{
					this.DepersistControl();
				}
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001BC44 File Offset: 0x00019E44
		private AxHost.State CreateNewOcxState(AxHost.State oldOcxState)
		{
			int noComponentChangeEvents = this.NoComponentChangeEvents;
			this.NoComponentChangeEvents = noComponentChangeEvents + 1;
			try
			{
				if (this.GetOcState() < 2)
				{
					return null;
				}
				try
				{
					AxHost.PropertyBagStream propertyBagStream = null;
					if (this.iPersistPropBag != null)
					{
						propertyBagStream = new AxHost.PropertyBagStream();
						this.iPersistPropBag.Save(propertyBagStream, true, true);
					}
					int num = this.storageType;
					if (num > 1)
					{
						if (num != 2)
						{
							return null;
						}
						if (oldOcxState != null)
						{
							return oldOcxState.RefreshStorage(this.iPersistStorage);
						}
						return null;
					}
					else
					{
						MemoryStream memoryStream = new MemoryStream();
						if (this.storageType == 0)
						{
							this.iPersistStream.Save(new UnsafeNativeMethods.ComStreamFromDataStream(memoryStream), true);
						}
						else
						{
							this.iPersistStreamInit.Save(new UnsafeNativeMethods.ComStreamFromDataStream(memoryStream), true);
						}
						if (memoryStream != null)
						{
							return new AxHost.State(memoryStream, this.storageType, this, propertyBagStream);
						}
						if (propertyBagStream != null)
						{
							return new AxHost.State(propertyBagStream);
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
			finally
			{
				noComponentChangeEvents = this.NoComponentChangeEvents;
				this.NoComponentChangeEvents = noComponentChangeEvents - 1;
			}
			return null;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0001BD4C File Offset: 0x00019F4C
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0001BD72 File Offset: 0x00019F72
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ContainerControl ContainingControl
		{
			get
			{
				IntSecurity.GetParent.Demand();
				if (this.containingControl == null)
				{
					this.containingControl = this.FindContainerControlInternal();
				}
				return this.containingControl;
			}
			set
			{
				this.containingControl = value;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001BD7C File Offset: 0x00019F7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal override bool ShouldSerializeText()
		{
			bool result = false;
			try
			{
				result = (this.Text.Length != 0);
			}
			catch (COMException)
			{
			}
			return result;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeContainingControl()
		{
			return this.ContainingControl != this.ParentInternal;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001BDC4 File Offset: 0x00019FC4
		private ContainerControl FindContainerControlInternal()
		{
			if (this.Site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.Site.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					ContainerControl containerControl = designerHost.RootComponent as ContainerControl;
					if (containerControl != null)
					{
						return containerControl;
					}
				}
			}
			ContainerControl result = null;
			for (Control control = this; control != null; control = control.ParentInternal)
			{
				ContainerControl containerControl2 = control as ContainerControl;
				if (containerControl2 != null)
				{
					result = containerControl2;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001BE2C File Offset: 0x0001A02C
		private bool IsDirty()
		{
			if (this.GetOcState() < 2)
			{
				return false;
			}
			if (this.axState[AxHost.valueChanged])
			{
				this.axState[AxHost.valueChanged] = false;
				return true;
			}
			int num;
			switch (this.storageType)
			{
			case 0:
				num = this.iPersistStream.IsDirty();
				break;
			case 1:
				num = this.iPersistStreamInit.IsDirty();
				break;
			case 2:
				num = this.iPersistStorage.IsDirty();
				break;
			default:
				return true;
			}
			if (num == 1)
			{
				return false;
			}
			NativeMethods.Failed(num);
			return true;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001BEC4 File Offset: 0x0001A0C4
		internal bool IsUserMode()
		{
			ISite site = this.Site;
			return site == null || !site.DesignMode;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001BEE8 File Offset: 0x0001A0E8
		private object GetAmbientProperty(int dispid)
		{
			Control parentInternal = this.ParentInternal;
			if (dispid != -732)
			{
				switch (dispid)
				{
				case -715:
					return true;
				case -713:
					return false;
				case -712:
					return false;
				case -711:
					return false;
				case -710:
					return false;
				case -709:
					return this.IsUserMode();
				case -706:
					return true;
				case -705:
					return Thread.CurrentThread.CurrentCulture.LCID;
				case -704:
					if (parentInternal != null)
					{
						return AxHost.GetOleColorFromColor(parentInternal.ForeColor);
					}
					return null;
				case -703:
					if (parentInternal != null)
					{
						return AxHost.GetIFontFromFont(parentInternal.Font);
					}
					return null;
				case -702:
				{
					string text = this.GetParentContainer().GetNameForControl(this);
					if (text == null)
					{
						text = "";
					}
					return text;
				}
				case -701:
					if (parentInternal != null)
					{
						return AxHost.GetOleColorFromColor(parentInternal.BackColor);
					}
					return null;
				}
				return null;
			}
			Control control = this;
			while (control != null)
			{
				if (control.RightToLeft == System.Windows.Forms.RightToLeft.No)
				{
					return false;
				}
				if (control.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
				{
					return true;
				}
				if (control.RightToLeft == System.Windows.Forms.RightToLeft.Inherit)
				{
					control = control.Parent;
				}
			}
			return null;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001C030 File Offset: 0x0001A230
		public void DoVerb(int verb)
		{
			Control parentInternal = this.ParentInternal;
			this.GetOleObject().DoVerb(verb, IntPtr.Zero, this.oleSite, -1, (parentInternal != null) ? parentInternal.Handle : IntPtr.Zero, AxHost.FillInRect(new NativeMethods.COMRECT(), base.Bounds));
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001C07D File Offset: 0x0001A27D
		private bool AwaitingDefreezing()
		{
			return this.freezeCount > 0;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001C088 File Offset: 0x0001A288
		private void Freeze(bool v)
		{
			if (v)
			{
				try
				{
					this.GetOleControl().FreezeEvents(-1);
				}
				catch (COMException ex)
				{
				}
				this.freezeCount++;
				return;
			}
			try
			{
				this.GetOleControl().FreezeEvents(0);
			}
			catch (COMException ex2)
			{
			}
			this.freezeCount--;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001C0F8 File Offset: 0x0001A2F8
		private int UiDeactivate()
		{
			bool value = this.axState[AxHost.ownDisposing];
			this.axState[AxHost.ownDisposing] = true;
			int result = 0;
			try
			{
				result = this.GetInPlaceObject().UIDeactivate();
			}
			finally
			{
				this.axState[AxHost.ownDisposing] = value;
			}
			return result;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001C15C File Offset: 0x0001A35C
		private int GetOcState()
		{
			return this.ocState;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001C164 File Offset: 0x0001A364
		private void SetOcState(int nv)
		{
			this.ocState = nv;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001C16D File Offset: 0x0001A36D
		private string GetLicenseKey()
		{
			return this.GetLicenseKey(this.clsid);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001C17C File Offset: 0x0001A37C
		private string GetLicenseKey(Guid clsid)
		{
			if (this.licenseKey != null || !this.axState[AxHost.needLicenseKey])
			{
				return this.licenseKey;
			}
			try
			{
				UnsafeNativeMethods.IClassFactory2 classFactory = UnsafeNativeMethods.CoGetClassObject(ref clsid, 1, 0, ref AxHost.icf2_Guid);
				NativeMethods.tagLICINFO tagLICINFO = new NativeMethods.tagLICINFO();
				classFactory.GetLicInfo(tagLICINFO);
				if (tagLICINFO.fRuntimeAvailable != 0)
				{
					string[] array = new string[1];
					classFactory.RequestLicKey(0, array);
					this.licenseKey = array[0];
					return this.licenseKey;
				}
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == AxHost.E_NOINTERFACE.ErrorCode)
				{
					return null;
				}
				this.axState[AxHost.needLicenseKey] = false;
			}
			catch (Exception ex2)
			{
				this.axState[AxHost.needLicenseKey] = false;
			}
			return null;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001C254 File Offset: 0x0001A454
		private void CreateWithoutLicense(Guid clsid)
		{
			object obj = UnsafeNativeMethods.CoCreateInstance(ref clsid, null, 1, ref NativeMethods.ActiveX.IID_IUnknown);
			this.instance = obj;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001C27C File Offset: 0x0001A47C
		private void CreateWithLicense(string license, Guid clsid)
		{
			if (license != null)
			{
				try
				{
					UnsafeNativeMethods.IClassFactory2 classFactory = UnsafeNativeMethods.CoGetClassObject(ref clsid, 1, 0, ref AxHost.icf2_Guid);
					if (classFactory != null)
					{
						classFactory.CreateInstanceLic(null, null, ref NativeMethods.ActiveX.IID_IUnknown, license, out this.instance);
					}
				}
				catch (Exception ex)
				{
				}
			}
			if (this.instance == null)
			{
				this.CreateWithoutLicense(clsid);
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		private void CreateInstance()
		{
			try
			{
				this.instance = this.CreateInstanceCore(this.clsid);
			}
			catch (ExternalException ex)
			{
				if (ex.ErrorCode == -2147221230)
				{
					throw new LicenseException(base.GetType(), this, SR.GetString("AXNoLicenseToUse"));
				}
				throw;
			}
			this.SetOcState(1);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001C338 File Offset: 0x0001A538
		protected virtual object CreateInstanceCore(Guid clsid)
		{
			if (this.IsUserMode())
			{
				this.CreateWithLicense(this.licenseKey, clsid);
			}
			else
			{
				this.CreateWithoutLicense(clsid);
			}
			return this.instance;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001C360 File Offset: 0x0001A560
		private CategoryAttribute GetCategoryForDispid(int dispid)
		{
			NativeMethods.ICategorizeProperties categorizeProperties = this.GetCategorizeProperties();
			if (categorizeProperties == null)
			{
				return null;
			}
			int num = 0;
			try
			{
				categorizeProperties.MapPropertyToCategory(dispid, ref num);
				if (num != 0)
				{
					int num2 = -num;
					if (num2 > 0 && num2 < AxHost.categoryNames.Length && AxHost.categoryNames[num2] != null)
					{
						return AxHost.categoryNames[num2];
					}
					num2 = -num2;
					int num3 = num2;
					if (this.objectDefinedCategoryNames != null)
					{
						CategoryAttribute categoryAttribute = (CategoryAttribute)this.objectDefinedCategoryNames[num3];
						if (categoryAttribute != null)
						{
							return categoryAttribute;
						}
					}
					string text = null;
					if (categorizeProperties.GetCategoryName(num2, CultureInfo.CurrentCulture.LCID, out text) == 0 && text != null)
					{
						CategoryAttribute categoryAttribute = new CategoryAttribute(text);
						if (this.objectDefinedCategoryNames == null)
						{
							this.objectDefinedCategoryNames = new Hashtable();
						}
						this.objectDefinedCategoryNames.Add(num3, categoryAttribute);
						return categoryAttribute;
					}
				}
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001C450 File Offset: 0x0001A650
		private void SetSelectionStyle(int selectionStyle)
		{
			if (!this.IsUserMode())
			{
				ISelectionService selectionService = this.GetSelectionService();
				this.selectionStyle = selectionStyle;
				if (selectionService != null && selectionService.GetComponentSelected(this))
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["SelectionStyle"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(int))
					{
						propertyDescriptor.SetValue(this, selectionStyle);
					}
				}
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001C4B8 File Offset: 0x0001A6B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void InvokeEditMode()
		{
			if (this.editMode != 0)
			{
				return;
			}
			this.AddSelectionHandler();
			this.editMode = 2;
			this.SetSelectionStyle(2);
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			try
			{
				this.UiActivate();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001C504 File Offset: 0x0001A704
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			if (!this.axState[AxHost.editorRefresh] && this.HasPropertyPages())
			{
				this.axState[AxHost.editorRefresh] = true;
				TypeDescriptor.Refresh(base.GetType());
			}
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00015ECC File Offset: 0x000140CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00015ECC File Offset: 0x000140CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00015ECC File Offset: 0x000140CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001C543 File Offset: 0x0001A743
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001C54C File Offset: 0x0001A74C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001C558 File Offset: 0x0001A758
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			if (editorBaseType != typeof(ComponentEditor))
			{
				return null;
			}
			if (this.editor != null)
			{
				return this.editor;
			}
			if (this.editor == null && this.HasPropertyPages())
			{
				this.editor = new AxHost.AxComponentEditor();
			}
			return this.editor;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001C5A9 File Offset: 0x0001A7A9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001C5B2 File Offset: 0x0001A7B2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001C5BC File Offset: 0x0001A7BC
		private void OnIdle(object sender, EventArgs e)
		{
			if (this.axState[AxHost.refreshProperties])
			{
				TypeDescriptor.Refresh(base.GetType());
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0001C5DB File Offset: 0x0001A7DB
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		private bool RefreshAllProperties
		{
			get
			{
				return this.axState[AxHost.refreshProperties];
			}
			set
			{
				this.axState[AxHost.refreshProperties] = value;
				if (value && !this.axState[AxHost.listeningToIdle])
				{
					Application.Idle += this.OnIdle;
					this.axState[AxHost.listeningToIdle] = true;
					return;
				}
				if (!value && this.axState[AxHost.listeningToIdle])
				{
					Application.Idle -= this.OnIdle;
					this.axState[AxHost.listeningToIdle] = false;
				}
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001C680 File Offset: 0x0001A880
		private PropertyDescriptorCollection FillProperties(Attribute[] attributes)
		{
			if (this.RefreshAllProperties)
			{
				this.RefreshAllProperties = false;
				this.propsStash = null;
				this.attribsStash = null;
			}
			else if (this.propsStash != null)
			{
				if (attributes == null && this.attribsStash == null)
				{
					return this.propsStash;
				}
				if (attributes != null && this.attribsStash != null && attributes.Length == this.attribsStash.Length)
				{
					bool flag = true;
					int num = 0;
					foreach (Attribute attribute in attributes)
					{
						if (!attribute.Equals(this.attribsStash[num++]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return this.propsStash;
					}
				}
			}
			ArrayList arrayList = new ArrayList();
			if (this.properties == null)
			{
				this.properties = new Hashtable();
			}
			if (this.propertyInfos == null)
			{
				this.propertyInfos = new Hashtable();
				PropertyInfo[] array = base.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
				foreach (PropertyInfo propertyInfo in array)
				{
					this.propertyInfos.Add(propertyInfo.Name, propertyInfo);
				}
			}
			PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(this, null, true);
			if (propertyDescriptorCollection != null)
			{
				for (int k = 0; k < propertyDescriptorCollection.Count; k++)
				{
					if (propertyDescriptorCollection[k].DesignTimeOnly)
					{
						arrayList.Add(propertyDescriptorCollection[k]);
					}
					else
					{
						string name = propertyDescriptorCollection[k].Name;
						PropertyInfo propertyInfo2 = (PropertyInfo)this.propertyInfos[name];
						if (!(propertyInfo2 != null) || propertyInfo2.CanRead)
						{
							if (!this.properties.ContainsKey(name))
							{
								PropertyDescriptor propertyDescriptor;
								if (propertyInfo2 != null)
								{
									propertyDescriptor = new AxHost.AxPropertyDescriptor(propertyDescriptorCollection[k], this);
									((AxHost.AxPropertyDescriptor)propertyDescriptor).UpdateAttributes();
								}
								else
								{
									propertyDescriptor = propertyDescriptorCollection[k];
								}
								this.properties.Add(name, propertyDescriptor);
								arrayList.Add(propertyDescriptor);
							}
							else
							{
								PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)this.properties[name];
								AxHost.AxPropertyDescriptor axPropertyDescriptor = propertyDescriptor2 as AxHost.AxPropertyDescriptor;
								if ((!(propertyInfo2 == null) || axPropertyDescriptor == null) && (!(propertyInfo2 != null) || axPropertyDescriptor != null))
								{
									if (axPropertyDescriptor != null)
									{
										axPropertyDescriptor.UpdateAttributes();
									}
									arrayList.Add(propertyDescriptor2);
								}
							}
						}
					}
				}
				if (attributes != null)
				{
					Attribute attribute2 = null;
					foreach (Attribute attribute3 in attributes)
					{
						if (attribute3 is BrowsableAttribute)
						{
							attribute2 = attribute3;
						}
					}
					if (attribute2 != null)
					{
						ArrayList arrayList2 = null;
						foreach (object obj in arrayList)
						{
							PropertyDescriptor propertyDescriptor3 = (PropertyDescriptor)obj;
							if (propertyDescriptor3 is AxHost.AxPropertyDescriptor)
							{
								Attribute attribute4 = propertyDescriptor3.Attributes[typeof(BrowsableAttribute)];
								if (attribute4 != null && !attribute4.Equals(attribute2))
								{
									if (arrayList2 == null)
									{
										arrayList2 = new ArrayList();
									}
									arrayList2.Add(propertyDescriptor3);
								}
							}
						}
						if (arrayList2 != null)
						{
							foreach (object obj2 in arrayList2)
							{
								arrayList.Remove(obj2);
							}
						}
					}
				}
			}
			PropertyDescriptor[] array3 = new PropertyDescriptor[arrayList.Count];
			arrayList.CopyTo(array3, 0);
			this.propsStash = new PropertyDescriptorCollection(array3);
			this.attribsStash = attributes;
			return this.propsStash;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001CA08 File Offset: 0x0001AC08
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.FillProperties(null);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001CA11 File Offset: 0x0001AC11
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return this.FillProperties(attributes);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00006C59 File Offset: 0x00004E59
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001CA1C File Offset: 0x0001AC1C
		private AxHost.AxPropertyDescriptor GetPropertyDescriptorFromDispid(int dispid)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this.FillProperties(null);
			foreach (object obj in propertyDescriptorCollection)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				AxHost.AxPropertyDescriptor axPropertyDescriptor = propertyDescriptor as AxHost.AxPropertyDescriptor;
				if (axPropertyDescriptor != null && axPropertyDescriptor.Dispid == dispid)
				{
					return axPropertyDescriptor;
				}
			}
			return null;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001CA90 File Offset: 0x0001AC90
		private void ActivateAxControl()
		{
			if (this.QuickActivate())
			{
				this.DepersistControl();
			}
			else
			{
				this.SlowActivate();
			}
			this.SetOcState(2);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001CAAF File Offset: 0x0001ACAF
		private void DepersistFromIPropertyBag(UnsafeNativeMethods.IPropertyBag propBag)
		{
			this.iPersistPropBag.Load(propBag, null);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001CABE File Offset: 0x0001ACBE
		private void DepersistFromIStream(UnsafeNativeMethods.IStream istream)
		{
			this.storageType = 0;
			this.iPersistStream.Load(istream);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001CAD3 File Offset: 0x0001ACD3
		private void DepersistFromIStreamInit(UnsafeNativeMethods.IStream istream)
		{
			this.storageType = 1;
			this.iPersistStreamInit.Load(istream);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
		private void DepersistFromIStorage(UnsafeNativeMethods.IStorage storage)
		{
			this.storageType = 2;
			if (storage != null)
			{
				int num = this.iPersistStorage.Load(storage);
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001CB10 File Offset: 0x0001AD10
		private void DepersistControl()
		{
			this.Freeze(true);
			if (this.ocxState != null)
			{
				switch (this.ocxState.Type)
				{
				case 0:
					try
					{
						this.iPersistStream = (UnsafeNativeMethods.IPersistStream)this.instance;
						this.DepersistFromIStream(this.ocxState.GetStream());
						goto IL_1D5;
					}
					catch (Exception ex)
					{
						goto IL_1D5;
					}
					break;
				case 1:
					break;
				case 2:
					try
					{
						this.iPersistStorage = (UnsafeNativeMethods.IPersistStorage)this.instance;
						this.DepersistFromIStorage(this.ocxState.GetStorage());
						goto IL_1D5;
					}
					catch (Exception ex2)
					{
						goto IL_1D5;
					}
					goto IL_1C5;
				default:
					goto IL_1C5;
				}
				if (this.instance is UnsafeNativeMethods.IPersistStreamInit)
				{
					try
					{
						this.iPersistStreamInit = (UnsafeNativeMethods.IPersistStreamInit)this.instance;
						this.DepersistFromIStreamInit(this.ocxState.GetStream());
					}
					catch (Exception ex3)
					{
					}
					this.GetControlEnabled();
					goto IL_1D5;
				}
				this.ocxState.Type = 0;
				this.DepersistControl();
				return;
				IL_1C5:
				throw new InvalidOperationException(SR.GetString("UnableToInitComponent"));
				IL_1D5:
				if (this.ocxState.GetPropBag() != null)
				{
					try
					{
						this.iPersistPropBag = (UnsafeNativeMethods.IPersistPropertyBag)this.instance;
						this.DepersistFromIPropertyBag(this.ocxState.GetPropBag());
					}
					catch (Exception ex4)
					{
					}
				}
				return;
			}
			if (this.instance is UnsafeNativeMethods.IPersistStreamInit)
			{
				this.iPersistStreamInit = (UnsafeNativeMethods.IPersistStreamInit)this.instance;
				try
				{
					this.storageType = 1;
					this.iPersistStreamInit.InitNew();
				}
				catch (Exception ex5)
				{
				}
				return;
			}
			if (this.instance is UnsafeNativeMethods.IPersistStream)
			{
				this.storageType = 0;
				this.iPersistStream = (UnsafeNativeMethods.IPersistStream)this.instance;
				return;
			}
			if (this.instance is UnsafeNativeMethods.IPersistStorage)
			{
				this.storageType = 2;
				this.ocxState = new AxHost.State(this);
				this.iPersistStorage = (UnsafeNativeMethods.IPersistStorage)this.instance;
				try
				{
					this.iPersistStorage.InitNew(this.ocxState.GetStorage());
				}
				catch (Exception ex6)
				{
				}
				return;
			}
			if (this.instance is UnsafeNativeMethods.IPersistPropertyBag)
			{
				this.iPersistPropBag = (UnsafeNativeMethods.IPersistPropertyBag)this.instance;
				try
				{
					this.iPersistPropBag.InitNew();
				}
				catch (Exception ex7)
				{
				}
			}
			throw new InvalidOperationException(SR.GetString("UnableToInitComponent"));
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001CD80 File Offset: 0x0001AF80
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object GetOcx()
		{
			return this.instance;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001CD88 File Offset: 0x0001AF88
		private object GetOcxCreate()
		{
			if (this.instance == null)
			{
				this.CreateInstance();
				this.RealizeStyles();
				this.AttachInterfaces();
				this.oleSite.OnOcxCreate();
			}
			return this.instance;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001CDB8 File Offset: 0x0001AFB8
		private void StartEvents()
		{
			if (!this.axState[AxHost.sinkAttached])
			{
				try
				{
					this.CreateSink();
					this.oleSite.StartEvents();
				}
				catch (Exception ex)
				{
				}
				this.axState[AxHost.sinkAttached] = true;
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001CE10 File Offset: 0x0001B010
		private void StopEvents()
		{
			if (this.axState[AxHost.sinkAttached])
			{
				try
				{
					this.DetachSink();
				}
				catch (Exception ex)
				{
				}
				this.axState[AxHost.sinkAttached] = false;
			}
			this.oleSite.StopEvents();
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void CreateSink()
		{
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void DetachSink()
		{
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001CE68 File Offset: 0x0001B068
		private bool CanShowPropertyPages()
		{
			return this.GetOcState() >= 2 && this.GetOcx() is NativeMethods.ISpecifyPropertyPages;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001CE84 File Offset: 0x0001B084
		public bool HasPropertyPages()
		{
			if (!this.CanShowPropertyPages())
			{
				return false;
			}
			NativeMethods.ISpecifyPropertyPages specifyPropertyPages = (NativeMethods.ISpecifyPropertyPages)this.GetOcx();
			try
			{
				NativeMethods.tagCAUUID tagCAUUID = new NativeMethods.tagCAUUID();
				try
				{
					specifyPropertyPages.GetPages(tagCAUUID);
					if (tagCAUUID.cElems > 0)
					{
						return true;
					}
				}
				finally
				{
					if (tagCAUUID.pElems != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(tagCAUUID.pElems);
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001CF08 File Offset: 0x0001B108
		private unsafe void ShowPropertyPageForDispid(int dispid, Guid guid)
		{
			try
			{
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this.GetOcx());
				UnsafeNativeMethods.OleCreatePropertyFrameIndirect(new NativeMethods.OCPFIPARAMS
				{
					hwndOwner = ((this.ContainingControl == null) ? IntPtr.Zero : this.ContainingControl.Handle),
					lpszCaption = base.Name,
					ppUnk = (IntPtr)(&iunknownForObject),
					uuid = (IntPtr)(&guid),
					dispidInitial = dispid
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001CF94 File Offset: 0x0001B194
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void MakeDirty()
		{
			ISite site = this.Site;
			if (site == null)
			{
				return;
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
			if (componentChangeService == null)
			{
				return;
			}
			componentChangeService.OnComponentChanging(this, null);
			componentChangeService.OnComponentChanged(this, null, null, null);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
		public void ShowPropertyPages()
		{
			if (this.ParentInternal == null)
			{
				return;
			}
			if (!this.ParentInternal.IsHandleCreated)
			{
				return;
			}
			this.ShowPropertyPages(this.ParentInternal);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001D000 File Offset: 0x0001B200
		public void ShowPropertyPages(Control control)
		{
			try
			{
				if (this.CanShowPropertyPages())
				{
					NativeMethods.ISpecifyPropertyPages specifyPropertyPages = (NativeMethods.ISpecifyPropertyPages)this.GetOcx();
					NativeMethods.tagCAUUID tagCAUUID = new NativeMethods.tagCAUUID();
					try
					{
						specifyPropertyPages.GetPages(tagCAUUID);
						if (tagCAUUID.cElems <= 0)
						{
							return;
						}
					}
					catch
					{
						return;
					}
					IDesignerHost designerHost = null;
					if (this.Site != null)
					{
						designerHost = (IDesignerHost)this.Site.GetService(typeof(IDesignerHost));
					}
					DesignerTransaction designerTransaction = null;
					try
					{
						if (designerHost != null)
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("AXEditProperties"));
						}
						string caption = null;
						object ocx = this.GetOcx();
						IntPtr handle = (this.ContainingControl == null) ? IntPtr.Zero : this.ContainingControl.Handle;
						SafeNativeMethods.OleCreatePropertyFrame(new HandleRef(this, handle), 0, 0, caption, 1, ref ocx, tagCAUUID.cElems, new HandleRef(null, tagCAUUID.pElems), Application.CurrentCulture.LCID, 0, IntPtr.Zero);
					}
					finally
					{
						if (this.oleSite != null)
						{
							((UnsafeNativeMethods.IPropertyNotifySink)this.oleSite).OnChanged(-1);
						}
						if (designerTransaction != null)
						{
							designerTransaction.Commit();
						}
						if (tagCAUUID.pElems != IntPtr.Zero)
						{
							Marshal.FreeCoTaskMem(tagCAUUID.pElems);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001D174 File Offset: 0x0001B374
		internal override IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			if (this.isMaskEdit)
			{
				return base.InitializeDCForWmCtlColor(dc, msg);
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001D18C File Offset: 0x0001B38C
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 83)
			{
				if (msg <= 21)
				{
					if (msg != 2)
					{
						if (msg == 8)
						{
							this.hwndFocus = m.WParam;
							try
							{
								base.WndProc(ref m);
								return;
							}
							finally
							{
								this.hwndFocus = IntPtr.Zero;
							}
							goto IL_F9;
						}
						if (msg - 20 > 1)
						{
							goto IL_1D0;
						}
					}
					else
					{
						if (this.GetOcState() >= 4)
						{
							UnsafeNativeMethods.IOleInPlaceObject inPlaceObject = this.GetInPlaceObject();
							IntPtr handle;
							if (NativeMethods.Succeeded(inPlaceObject.GetWindow(out handle)))
							{
								Application.ParkHandle(new HandleRef(inPlaceObject, handle), DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED);
							}
						}
						bool state = base.GetState(2);
						this.TransitionDownTo(2);
						this.DetachAndForward(ref m);
						if (state != base.GetState(2))
						{
							base.SetState(2, state);
							return;
						}
						return;
					}
				}
				else if (msg != 32 && msg != 43)
				{
					if (msg != 83)
					{
						goto IL_1D0;
					}
					base.WndProc(ref m);
					this.DefWndProc(ref m);
					return;
				}
			}
			else if (msg <= 257)
			{
				if (msg != 123)
				{
					if (msg != 130)
					{
						if (msg != 257)
						{
							goto IL_1D0;
						}
						if (this.axState[AxHost.processingKeyUp])
						{
							return;
						}
						this.axState[AxHost.processingKeyUp] = true;
						try
						{
							if (base.PreProcessControlMessage(ref m) != PreProcessControlState.MessageProcessed)
							{
								this.DefWndProc(ref m);
							}
							return;
						}
						finally
						{
							this.axState[AxHost.processingKeyUp] = false;
						}
					}
					this.DetachAndForward(ref m);
					return;
				}
				this.DefWndProc(ref m);
				return;
			}
			else
			{
				if (msg == 273)
				{
					goto IL_F9;
				}
				switch (msg)
				{
				case 513:
				case 516:
				case 519:
					if (this.IsUserMode())
					{
						base.Focus();
					}
					this.DefWndProc(ref m);
					return;
				case 514:
				case 515:
				case 517:
				case 518:
				case 520:
				case 521:
					break;
				default:
					if (msg != 8277)
					{
						goto IL_1D0;
					}
					break;
				}
			}
			this.DefWndProc(ref m);
			return;
			IL_F9:
			if (!Control.ReflectMessageInternal(m.LParam, ref m))
			{
				this.DefWndProc(ref m);
				return;
			}
			return;
			IL_1D0:
			if (m.Msg == this.REGMSG_MSG)
			{
				m.Result = (IntPtr)123;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		private void DetachAndForward(ref Message m)
		{
			IntPtr handleNoCreate = this.GetHandleNoCreate();
			this.DetachWindow();
			if (handleNoCreate != IntPtr.Zero)
			{
				IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handleNoCreate), -4);
				m.Result = UnsafeNativeMethods.CallWindowProc(windowLong, handleNoCreate, m.Msg, m.WParam, m.LParam);
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001D400 File Offset: 0x0001B600
		private void DetachWindow()
		{
			if (base.IsHandleCreated)
			{
				this.OnHandleDestroyed(EventArgs.Empty);
				for (Control control = this; control != null; control = control.ParentInternal)
				{
				}
				base.WindowReleaseHandle();
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001D434 File Offset: 0x0001B634
		private void InformOfNewHandle()
		{
			for (Control control = this; control != null; control = control.ParentInternal)
			{
			}
			this.wndprocAddr = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -4);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001D468 File Offset: 0x0001B668
		private void AttachWindow(IntPtr hwnd)
		{
			if (!this.axState[AxHost.fFakingWindow])
			{
				base.WindowAssignHandle(hwnd, this.axState[AxHost.assignUniqueID]);
			}
			base.UpdateZOrder();
			Size size = base.Size;
			base.UpdateBounds();
			Size extent = this.GetExtent();
			Point location = base.Location;
			if (size.Width < extent.Width || size.Height < extent.Height)
			{
				base.Bounds = new Rectangle(location.X, location.Y, extent.Width, extent.Height);
			}
			else
			{
				Size size2 = this.SetExtent(size.Width, size.Height);
				if (!size2.Equals(size))
				{
					base.Bounds = new Rectangle(location.X, location.Y, size2.Width, size2.Height);
				}
			}
			this.OnHandleCreated(EventArgs.Empty);
			this.InformOfNewHandle();
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001D56A File Offset: 0x0001B76A
		protected override void OnHandleCreated(EventArgs e)
		{
			if (Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
			}
			base.SetAcceptDrops(this.AllowDrop);
			base.RaiseCreateHandleEvent(e);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001D596 File Offset: 0x0001B796
		private int Pix2HM(int pix, int logP)
		{
			return (2540 * pix + (logP >> 1)) / logP;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001D5A5 File Offset: 0x0001B7A5
		private int HM2Pix(int hm, int logP)
		{
			return (logP * hm + 1270) / 2540;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		private bool QuickActivate()
		{
			if (!(this.instance is UnsafeNativeMethods.IQuickActivate))
			{
				return false;
			}
			UnsafeNativeMethods.IQuickActivate quickActivate = (UnsafeNativeMethods.IQuickActivate)this.instance;
			UnsafeNativeMethods.tagQACONTAINER tagQACONTAINER = new UnsafeNativeMethods.tagQACONTAINER();
			UnsafeNativeMethods.tagQACONTROL tagQACONTROL = new UnsafeNativeMethods.tagQACONTROL();
			tagQACONTAINER.pClientSite = this.oleSite;
			tagQACONTAINER.pPropertyNotifySink = this.oleSite;
			tagQACONTAINER.pFont = AxHost.GetIFontFromFont(this.GetParentContainer().parent.Font);
			tagQACONTAINER.dwAppearance = 0;
			tagQACONTAINER.lcid = Application.CurrentCulture.LCID;
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				tagQACONTAINER.colorFore = AxHost.GetOleColorFromColor(parentInternal.ForeColor);
				tagQACONTAINER.colorBack = AxHost.GetOleColorFromColor(parentInternal.BackColor);
			}
			else
			{
				tagQACONTAINER.colorFore = AxHost.GetOleColorFromColor(SystemColors.WindowText);
				tagQACONTAINER.colorBack = AxHost.GetOleColorFromColor(SystemColors.Window);
			}
			tagQACONTAINER.dwAmbientFlags = 224;
			if (this.IsUserMode())
			{
				tagQACONTAINER.dwAmbientFlags |= 4;
			}
			try
			{
				quickActivate.QuickActivate(tagQACONTAINER, tagQACONTROL);
			}
			catch (Exception ex)
			{
				this.DisposeAxControl();
				return false;
			}
			this.miscStatusBits = tagQACONTROL.dwMiscStatus;
			this.ParseMiscBits(this.miscStatusBits);
			return true;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001D6EC File Offset: 0x0001B8EC
		internal override void DisposeAxControls()
		{
			this.axState[AxHost.rejectSelection] = true;
			base.DisposeAxControls();
			this.TransitionDownTo(0);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001D70C File Offset: 0x0001B90C
		private bool GetControlEnabled()
		{
			bool result;
			try
			{
				result = base.IsHandleCreated;
			}
			catch (Exception ex)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001D738 File Offset: 0x0001B938
		internal override bool CanSelectCore()
		{
			return this.GetControlEnabled() && !this.axState[AxHost.rejectSelection] && base.CanSelectCore();
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001D75C File Offset: 0x0001B95C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.TransitionDownTo(0);
				if (this.newParent != null)
				{
					this.newParent.Dispose();
				}
				if (this.oleSite != null)
				{
					this.oleSite.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001D795 File Offset: 0x0001B995
		private bool GetSiteOwnsDeactivation()
		{
			return this.axState[AxHost.ownDisposing];
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001D7A7 File Offset: 0x0001B9A7
		private void DisposeAxControl()
		{
			if (this.GetParentContainer() != null)
			{
				this.GetParentContainer().RemoveControl(this);
			}
			this.TransitionDownTo(2);
			if (this.GetOcState() == 2)
			{
				this.GetOleObject().SetClientSite(null);
				this.SetOcState(1);
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001D7E4 File Offset: 0x0001B9E4
		private void ReleaseAxControl()
		{
			int noComponentChangeEvents = this.NoComponentChangeEvents;
			this.NoComponentChangeEvents = noComponentChangeEvents + 1;
			ContainerControl containerControl = this.ContainingControl;
			if (containerControl != null)
			{
				containerControl.VisibleChanged -= this.onContainerVisibleChanged;
			}
			try
			{
				if (this.instance != null)
				{
					Marshal.FinalReleaseComObject(this.instance);
					this.instance = null;
					this.iOleInPlaceObject = null;
					this.iOleObject = null;
					this.iOleControl = null;
					this.iOleInPlaceActiveObject = null;
					this.iOleInPlaceActiveObjectExternal = null;
					this.iPerPropertyBrowsing = null;
					this.iCategorizeProperties = null;
					this.iPersistStream = null;
					this.iPersistStreamInit = null;
					this.iPersistStorage = null;
				}
				this.axState[AxHost.checkedIppb] = false;
				this.axState[AxHost.checkedCP] = false;
				this.axState[AxHost.disposed] = true;
				this.freezeCount = 0;
				this.axState[AxHost.sinkAttached] = false;
				this.wndprocAddr = IntPtr.Zero;
				this.SetOcState(0);
			}
			finally
			{
				noComponentChangeEvents = this.NoComponentChangeEvents;
				this.NoComponentChangeEvents = noComponentChangeEvents - 1;
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		private void ParseMiscBits(int bits)
		{
			this.axState[AxHost.fOwnWindow] = ((bits & 1024) != 0 && this.IsUserMode());
			this.axState[AxHost.fSimpleFrame] = ((bits & 65536) != 0);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001D93C File Offset: 0x0001BB3C
		private void SlowActivate()
		{
			bool flag = false;
			if ((this.miscStatusBits & 131072) != 0)
			{
				this.GetOleObject().SetClientSite(this.oleSite);
				flag = true;
			}
			this.DepersistControl();
			if (!flag)
			{
				this.GetOleObject().SetClientSite(this.oleSite);
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001D988 File Offset: 0x0001BB88
		private static NativeMethods.COMRECT FillInRect(NativeMethods.COMRECT dest, Rectangle source)
		{
			dest.left = source.X;
			dest.top = source.Y;
			dest.right = source.Width + source.X;
			dest.bottom = source.Height + source.Y;
			return dest;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		private AxHost.AxContainer GetParentContainer()
		{
			IntSecurity.GetParent.Demand();
			if (this.container == null)
			{
				this.container = AxHost.AxContainer.FindContainerForControl(this);
			}
			if (this.container == null)
			{
				ContainerControl containerControl = this.ContainingControl;
				if (containerControl == null)
				{
					if (this.newParent == null)
					{
						this.newParent = new ContainerControl();
						this.axContainer = this.newParent.CreateAxContainer();
						this.axContainer.AddControl(this);
					}
					return this.axContainer;
				}
				this.container = containerControl.CreateAxContainer();
				this.container.AddControl(this);
				this.containingControl = containerControl;
			}
			return this.container;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001DA75 File Offset: 0x0001BC75
		private UnsafeNativeMethods.IOleControl GetOleControl()
		{
			if (this.iOleControl == null)
			{
				this.iOleControl = (UnsafeNativeMethods.IOleControl)this.instance;
			}
			return this.iOleControl;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001DA98 File Offset: 0x0001BC98
		private UnsafeNativeMethods.IOleInPlaceActiveObject GetInPlaceActiveObject()
		{
			if (this.iOleInPlaceActiveObjectExternal != null)
			{
				return this.iOleInPlaceActiveObjectExternal;
			}
			if (this.iOleInPlaceActiveObject == null)
			{
				try
				{
					this.iOleInPlaceActiveObject = (UnsafeNativeMethods.IOleInPlaceActiveObject)this.instance;
				}
				catch (InvalidCastException ex)
				{
				}
			}
			return this.iOleInPlaceActiveObject;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001DAE8 File Offset: 0x0001BCE8
		private UnsafeNativeMethods.IOleObject GetOleObject()
		{
			if (this.iOleObject == null)
			{
				this.iOleObject = (UnsafeNativeMethods.IOleObject)this.instance;
			}
			return this.iOleObject;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001DB09 File Offset: 0x0001BD09
		private UnsafeNativeMethods.IOleInPlaceObject GetInPlaceObject()
		{
			if (this.iOleInPlaceObject == null)
			{
				this.iOleInPlaceObject = (UnsafeNativeMethods.IOleInPlaceObject)this.instance;
			}
			return this.iOleInPlaceObject;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		private NativeMethods.ICategorizeProperties GetCategorizeProperties()
		{
			if (this.iCategorizeProperties == null && !this.axState[AxHost.checkedCP] && this.instance != null)
			{
				this.axState[AxHost.checkedCP] = true;
				if (this.instance is NativeMethods.ICategorizeProperties)
				{
					this.iCategorizeProperties = (NativeMethods.ICategorizeProperties)this.instance;
				}
			}
			return this.iCategorizeProperties;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001DB90 File Offset: 0x0001BD90
		private NativeMethods.IPerPropertyBrowsing GetPerPropertyBrowsing()
		{
			if (this.iPerPropertyBrowsing == null && !this.axState[AxHost.checkedIppb] && this.instance != null)
			{
				this.axState[AxHost.checkedIppb] = true;
				if (this.instance is NativeMethods.IPerPropertyBrowsing)
				{
					this.iPerPropertyBrowsing = (NativeMethods.IPerPropertyBrowsing)this.instance;
				}
			}
			return this.iPerPropertyBrowsing;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		private static object GetPICTDESCFromPicture(Image image)
		{
			Bitmap bitmap = image as Bitmap;
			if (bitmap != null)
			{
				return new NativeMethods.PICTDESCbmp(bitmap);
			}
			Metafile metafile = image as Metafile;
			if (metafile != null)
			{
				return new NativeMethods.PICTDESCemf(metafile);
			}
			throw new ArgumentException(SR.GetString("AXUnknownImage"), "image");
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001DC38 File Offset: 0x0001BE38
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIPictureFromPicture(Image image)
		{
			if (image == null)
			{
				return null;
			}
			object pictdescfromPicture = AxHost.GetPICTDESCFromPicture(image);
			return UnsafeNativeMethods.OleCreateIPictureIndirect(pictdescfromPicture, ref AxHost.ipicture_Guid, true);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001DC60 File Offset: 0x0001BE60
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIPictureFromCursor(Cursor cursor)
		{
			if (cursor == null)
			{
				return null;
			}
			NativeMethods.PICTDESCicon pictdesc = new NativeMethods.PICTDESCicon(Icon.FromHandle(cursor.Handle));
			return UnsafeNativeMethods.OleCreateIPictureIndirect(pictdesc, ref AxHost.ipicture_Guid, true);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001DC98 File Offset: 0x0001BE98
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIPictureDispFromPicture(Image image)
		{
			if (image == null)
			{
				return null;
			}
			object pictdescfromPicture = AxHost.GetPICTDESCFromPicture(image);
			return UnsafeNativeMethods.OleCreateIPictureDispIndirect(pictdescfromPicture, ref AxHost.ipictureDisp_Guid, true);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Image GetPictureFromIPicture(object picture)
		{
			if (picture == null)
			{
				return null;
			}
			IntPtr paletteHandle = IntPtr.Zero;
			UnsafeNativeMethods.IPicture picture2 = (UnsafeNativeMethods.IPicture)picture;
			int pictureType = (int)picture2.GetPictureType();
			if (pictureType == 1)
			{
				try
				{
					paletteHandle = picture2.GetHPal();
				}
				catch (COMException)
				{
				}
			}
			return AxHost.GetPictureFromParams(picture2, picture2.GetHandle(), pictureType, paletteHandle, picture2.GetWidth(), picture2.GetHeight());
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001DD20 File Offset: 0x0001BF20
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Image GetPictureFromIPictureDisp(object picture)
		{
			if (picture == null)
			{
				return null;
			}
			IntPtr paletteHandle = IntPtr.Zero;
			UnsafeNativeMethods.IPictureDisp pictureDisp = (UnsafeNativeMethods.IPictureDisp)picture;
			int pictureType = (int)pictureDisp.PictureType;
			if (pictureType == 1)
			{
				try
				{
					paletteHandle = pictureDisp.HPal;
				}
				catch (COMException)
				{
				}
			}
			return AxHost.GetPictureFromParams(pictureDisp, pictureDisp.Handle, pictureType, paletteHandle, pictureDisp.Width, pictureDisp.Height);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0001DD80 File Offset: 0x0001BF80
		private static Image GetPictureFromParams(object pict, IntPtr handle, int type, IntPtr paletteHandle, int width, int height)
		{
			switch (type)
			{
			case -1:
				return null;
			case 0:
				return null;
			case 1:
				return Image.FromHbitmap(handle, paletteHandle);
			case 2:
				return (Image)new Metafile(handle, new WmfPlaceableFileHeader
				{
					BboxRight = (short)width,
					BboxBottom = (short)height
				}, false).Clone();
			case 3:
				return (Image)Icon.FromHandle(handle).Clone();
			case 4:
				return (Image)new Metafile(handle, false).Clone();
			default:
				throw new ArgumentException(SR.GetString("AXUnknownImage"), "type");
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0001DE20 File Offset: 0x0001C020
		private static NativeMethods.FONTDESC GetFONTDESCFromFont(Font font)
		{
			NativeMethods.FONTDESC fontdesc = null;
			if (AxHost.fontTable == null)
			{
				AxHost.fontTable = new Hashtable();
			}
			else
			{
				fontdesc = (NativeMethods.FONTDESC)AxHost.fontTable[font];
			}
			if (fontdesc == null)
			{
				fontdesc = new NativeMethods.FONTDESC();
				fontdesc.lpstrName = font.Name;
				fontdesc.cySize = (long)(font.SizeInPoints * 10000f);
				NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
				font.ToLogFont(logfont);
				fontdesc.sWeight = (short)logfont.lfWeight;
				fontdesc.sCharset = (short)logfont.lfCharSet;
				fontdesc.fItalic = font.Italic;
				fontdesc.fUnderline = font.Underline;
				fontdesc.fStrikethrough = font.Strikeout;
				AxHost.fontTable[font] = fontdesc;
			}
			return fontdesc;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001DED2 File Offset: 0x0001C0D2
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Color GetColorFromOleColor(uint color)
		{
			return ColorTranslator.FromOle((int)color);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001DEDA File Offset: 0x0001C0DA
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static uint GetOleColorFromColor(Color color)
		{
			return (uint)ColorTranslator.ToOle(color);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIFontFromFont(Font font)
		{
			if (font == null)
			{
				return null;
			}
			if (font.Unit != GraphicsUnit.Point)
			{
				throw new ArgumentException(SR.GetString("AXFontUnitNotPoint"), "font");
			}
			object result;
			try
			{
				result = UnsafeNativeMethods.OleCreateIFontIndirect(AxHost.GetFONTDESCFromFont(font), ref AxHost.ifont_Guid);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001DF40 File Offset: 0x0001C140
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Font GetFontFromIFont(object font)
		{
			if (font == null)
			{
				return null;
			}
			UnsafeNativeMethods.IFont font2 = (UnsafeNativeMethods.IFont)font;
			Font result;
			try
			{
				Font font3 = Font.FromHfont(font2.GetHFont());
				if (font3.Unit != GraphicsUnit.Point)
				{
					font3 = new Font(font3.Name, font3.SizeInPoints, font3.Style, GraphicsUnit.Point, font3.GdiCharSet, font3.GdiVerticalFont);
				}
				result = font3;
			}
			catch (Exception ex)
			{
				result = Control.DefaultFont;
			}
			return result;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIFontDispFromFont(Font font)
		{
			if (font == null)
			{
				return null;
			}
			if (font.Unit != GraphicsUnit.Point)
			{
				throw new ArgumentException(SR.GetString("AXFontUnitNotPoint"), "font");
			}
			return SafeNativeMethods.OleCreateIFontDispIndirect(AxHost.GetFONTDESCFromFont(font), ref AxHost.ifontDisp_Guid);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Font GetFontFromIFontDisp(object font)
		{
			if (font == null)
			{
				return null;
			}
			UnsafeNativeMethods.IFont font2 = font as UnsafeNativeMethods.IFont;
			if (font2 != null)
			{
				return AxHost.GetFontFromIFont(font2);
			}
			SafeNativeMethods.IFontDisp fontDisp = (SafeNativeMethods.IFontDisp)font;
			FontStyle fontStyle = FontStyle.Regular;
			Font result;
			try
			{
				if (fontDisp.Bold)
				{
					fontStyle |= FontStyle.Bold;
				}
				if (fontDisp.Italic)
				{
					fontStyle |= FontStyle.Italic;
				}
				if (fontDisp.Underline)
				{
					fontStyle |= FontStyle.Underline;
				}
				if (fontDisp.Strikethrough)
				{
					fontStyle |= FontStyle.Strikeout;
				}
				if (fontDisp.Weight >= 700)
				{
					fontStyle |= FontStyle.Bold;
				}
				Font font3 = new Font(fontDisp.Name, (float)fontDisp.Size / 10000f, fontStyle, GraphicsUnit.Point, (byte)fontDisp.Charset);
				result = font3;
			}
			catch (Exception ex)
			{
				result = Control.DefaultFont;
			}
			return result;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static double GetOADateFromTime(DateTime time)
		{
			return time.ToOADate();
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001E0B5 File Offset: 0x0001C2B5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static DateTime GetTimeFromOADate(double date)
		{
			return DateTime.FromOADate(date);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
		private int Convert2int(object o, bool xDirection)
		{
			o = ((Array)o).GetValue(0);
			if (o.GetType() == typeof(float))
			{
				return AxHost.Twip2Pixel(Convert.ToDouble(o, CultureInfo.InvariantCulture), xDirection);
			}
			return Convert.ToInt32(o, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001E10F File Offset: 0x0001C30F
		private short Convert2short(object o)
		{
			o = ((Array)o).GetValue(0);
			return Convert.ToInt16(o, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001E12A File Offset: 0x0001C32A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseMove(object o1, object o2, object o3, object o4)
		{
			this.RaiseOnMouseMove(this.Convert2short(o1), this.Convert2short(o2), this.Convert2int(o3, true), this.Convert2int(o4, false));
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001E151 File Offset: 0x0001C351
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseMove(short button, short shift, float x, float y)
		{
			this.RaiseOnMouseMove(button, shift, AxHost.Twip2Pixel((int)x, true), AxHost.Twip2Pixel((int)y, false));
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001E16C File Offset: 0x0001C36C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseMove(short button, short shift, int x, int y)
		{
			base.OnMouseMove(new MouseEventArgs((MouseButtons)(button << 20), 1, x, y, 0));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001E182 File Offset: 0x0001C382
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseUp(object o1, object o2, object o3, object o4)
		{
			this.RaiseOnMouseUp(this.Convert2short(o1), this.Convert2short(o2), this.Convert2int(o3, true), this.Convert2int(o4, false));
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001E1A9 File Offset: 0x0001C3A9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseUp(short button, short shift, float x, float y)
		{
			this.RaiseOnMouseUp(button, shift, AxHost.Twip2Pixel((int)x, true), AxHost.Twip2Pixel((int)y, false));
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001E1C4 File Offset: 0x0001C3C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseUp(short button, short shift, int x, int y)
		{
			base.OnMouseUp(new MouseEventArgs((MouseButtons)(button << 20), 1, x, y, 0));
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001E1DA File Offset: 0x0001C3DA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseDown(object o1, object o2, object o3, object o4)
		{
			this.RaiseOnMouseDown(this.Convert2short(o1), this.Convert2short(o2), this.Convert2int(o3, true), this.Convert2int(o4, false));
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001E201 File Offset: 0x0001C401
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseDown(short button, short shift, float x, float y)
		{
			this.RaiseOnMouseDown(button, shift, AxHost.Twip2Pixel((int)x, true), AxHost.Twip2Pixel((int)y, false));
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001E21C File Offset: 0x0001C41C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseDown(short button, short shift, int x, int y)
		{
			base.OnMouseDown(new MouseEventArgs((MouseButtons)(button << 20), 1, x, y, 0));
		}

		// Token: 0x0400062D RID: 1581
		private static TraceSwitch AxHTraceSwitch = new TraceSwitch("AxHTrace", "ActiveX handle tracing");

		// Token: 0x0400062E RID: 1582
		private static TraceSwitch AxPropTraceSwitch = new TraceSwitch("AxPropTrace", "ActiveX property tracing");

		// Token: 0x0400062F RID: 1583
		private static TraceSwitch AxHostSwitch = new TraceSwitch("AxHost", "ActiveX host creation");

		// Token: 0x04000630 RID: 1584
		private static BooleanSwitch AxIgnoreTMSwitch = new BooleanSwitch("AxIgnoreTM", "ActiveX switch to ignore thread models");

		// Token: 0x04000631 RID: 1585
		private static BooleanSwitch AxAlwaysSaveSwitch = new BooleanSwitch("AxAlwaysSave", "ActiveX to save all controls regardless of their IsDirty function return value");

		// Token: 0x04000632 RID: 1586
		private static COMException E_NOTIMPL = new COMException(SR.GetString("AXNotImplemented"), -2147483647);

		// Token: 0x04000633 RID: 1587
		private static COMException E_INVALIDARG = new COMException(SR.GetString("AXInvalidArgument"), -2147024809);

		// Token: 0x04000634 RID: 1588
		private static COMException E_FAIL = new COMException(SR.GetString("AXUnknownError"), -2147467259);

		// Token: 0x04000635 RID: 1589
		private static COMException E_NOINTERFACE = new COMException(SR.GetString("AxInterfaceNotSupported"), -2147467262);

		// Token: 0x04000636 RID: 1590
		private const int INPROC_SERVER = 1;

		// Token: 0x04000637 RID: 1591
		private const int OC_PASSIVE = 0;

		// Token: 0x04000638 RID: 1592
		private const int OC_LOADED = 1;

		// Token: 0x04000639 RID: 1593
		private const int OC_RUNNING = 2;

		// Token: 0x0400063A RID: 1594
		private const int OC_INPLACE = 4;

		// Token: 0x0400063B RID: 1595
		private const int OC_UIACTIVE = 8;

		// Token: 0x0400063C RID: 1596
		private const int OC_OPEN = 16;

		// Token: 0x0400063D RID: 1597
		private const int EDITM_NONE = 0;

		// Token: 0x0400063E RID: 1598
		private const int EDITM_OBJECT = 1;

		// Token: 0x0400063F RID: 1599
		private const int EDITM_HOST = 2;

		// Token: 0x04000640 RID: 1600
		private const int STG_UNKNOWN = -1;

		// Token: 0x04000641 RID: 1601
		private const int STG_STREAM = 0;

		// Token: 0x04000642 RID: 1602
		private const int STG_STREAMINIT = 1;

		// Token: 0x04000643 RID: 1603
		private const int STG_STORAGE = 2;

		// Token: 0x04000644 RID: 1604
		private const int OLEIVERB_SHOW = -1;

		// Token: 0x04000645 RID: 1605
		private const int OLEIVERB_HIDE = -3;

		// Token: 0x04000646 RID: 1606
		private const int OLEIVERB_UIACTIVATE = -4;

		// Token: 0x04000647 RID: 1607
		private const int OLEIVERB_INPLACEACTIVATE = -5;

		// Token: 0x04000648 RID: 1608
		private const int OLEIVERB_PROPERTIES = -7;

		// Token: 0x04000649 RID: 1609
		private const int OLEIVERB_PRIMARY = 0;

		// Token: 0x0400064A RID: 1610
		private readonly int REGMSG_MSG = SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_subclassCheck");

		// Token: 0x0400064B RID: 1611
		private const int REGMSG_RETVAL = 123;

		// Token: 0x0400064C RID: 1612
		private static int logPixelsX = -1;

		// Token: 0x0400064D RID: 1613
		private static int logPixelsY = -1;

		// Token: 0x0400064E RID: 1614
		private static Guid icf2_Guid = typeof(UnsafeNativeMethods.IClassFactory2).GUID;

		// Token: 0x0400064F RID: 1615
		private static Guid ifont_Guid = typeof(UnsafeNativeMethods.IFont).GUID;

		// Token: 0x04000650 RID: 1616
		private static Guid ifontDisp_Guid = typeof(SafeNativeMethods.IFontDisp).GUID;

		// Token: 0x04000651 RID: 1617
		private static Guid ipicture_Guid = typeof(UnsafeNativeMethods.IPicture).GUID;

		// Token: 0x04000652 RID: 1618
		private static Guid ipictureDisp_Guid = typeof(UnsafeNativeMethods.IPictureDisp).GUID;

		// Token: 0x04000653 RID: 1619
		private static Guid ivbformat_Guid = typeof(UnsafeNativeMethods.IVBFormat).GUID;

		// Token: 0x04000654 RID: 1620
		private static Guid ioleobject_Guid = typeof(UnsafeNativeMethods.IOleObject).GUID;

		// Token: 0x04000655 RID: 1621
		private static Guid dataSource_Guid = new Guid("{7C0FFAB3-CD84-11D0-949A-00A0C91110ED}");

		// Token: 0x04000656 RID: 1622
		private static Guid windowsMediaPlayer_Clsid = new Guid("{22d6f312-b0f6-11d0-94ab-0080c74c7e95}");

		// Token: 0x04000657 RID: 1623
		private static Guid comctlImageCombo_Clsid = new Guid("{a98a24c0-b06f-3684-8c12-c52ae341e0bc}");

		// Token: 0x04000658 RID: 1624
		private static Guid maskEdit_Clsid = new Guid("{c932ba85-4374-101b-a56c-00aa003668dc}");

		// Token: 0x04000659 RID: 1625
		private static Hashtable fontTable;

		// Token: 0x0400065A RID: 1626
		private static readonly int ocxStateSet = BitVector32.CreateMask();

		// Token: 0x0400065B RID: 1627
		private static readonly int editorRefresh = BitVector32.CreateMask(AxHost.ocxStateSet);

		// Token: 0x0400065C RID: 1628
		private static readonly int listeningToIdle = BitVector32.CreateMask(AxHost.editorRefresh);

		// Token: 0x0400065D RID: 1629
		private static readonly int refreshProperties = BitVector32.CreateMask(AxHost.listeningToIdle);

		// Token: 0x0400065E RID: 1630
		private static readonly int checkedIppb = BitVector32.CreateMask(AxHost.refreshProperties);

		// Token: 0x0400065F RID: 1631
		private static readonly int checkedCP = BitVector32.CreateMask(AxHost.checkedIppb);

		// Token: 0x04000660 RID: 1632
		private static readonly int fNeedOwnWindow = BitVector32.CreateMask(AxHost.checkedCP);

		// Token: 0x04000661 RID: 1633
		private static readonly int fOwnWindow = BitVector32.CreateMask(AxHost.fNeedOwnWindow);

		// Token: 0x04000662 RID: 1634
		private static readonly int fSimpleFrame = BitVector32.CreateMask(AxHost.fOwnWindow);

		// Token: 0x04000663 RID: 1635
		private static readonly int fFakingWindow = BitVector32.CreateMask(AxHost.fSimpleFrame);

		// Token: 0x04000664 RID: 1636
		private static readonly int rejectSelection = BitVector32.CreateMask(AxHost.fFakingWindow);

		// Token: 0x04000665 RID: 1637
		private static readonly int ownDisposing = BitVector32.CreateMask(AxHost.rejectSelection);

		// Token: 0x04000666 RID: 1638
		private static readonly int sinkAttached = BitVector32.CreateMask(AxHost.ownDisposing);

		// Token: 0x04000667 RID: 1639
		private static readonly int disposed = BitVector32.CreateMask(AxHost.sinkAttached);

		// Token: 0x04000668 RID: 1640
		private static readonly int manualUpdate = BitVector32.CreateMask(AxHost.disposed);

		// Token: 0x04000669 RID: 1641
		private static readonly int addedSelectionHandler = BitVector32.CreateMask(AxHost.manualUpdate);

		// Token: 0x0400066A RID: 1642
		private static readonly int valueChanged = BitVector32.CreateMask(AxHost.addedSelectionHandler);

		// Token: 0x0400066B RID: 1643
		private static readonly int handlePosRectChanged = BitVector32.CreateMask(AxHost.valueChanged);

		// Token: 0x0400066C RID: 1644
		private static readonly int siteProcessedInputKey = BitVector32.CreateMask(AxHost.handlePosRectChanged);

		// Token: 0x0400066D RID: 1645
		private static readonly int needLicenseKey = BitVector32.CreateMask(AxHost.siteProcessedInputKey);

		// Token: 0x0400066E RID: 1646
		private static readonly int inTransition = BitVector32.CreateMask(AxHost.needLicenseKey);

		// Token: 0x0400066F RID: 1647
		private static readonly int processingKeyUp = BitVector32.CreateMask(AxHost.inTransition);

		// Token: 0x04000670 RID: 1648
		private static readonly int assignUniqueID = BitVector32.CreateMask(AxHost.processingKeyUp);

		// Token: 0x04000671 RID: 1649
		private static readonly int renameEventHooked = BitVector32.CreateMask(AxHost.assignUniqueID);

		// Token: 0x04000672 RID: 1650
		private BitVector32 axState;

		// Token: 0x04000673 RID: 1651
		private int storageType = -1;

		// Token: 0x04000674 RID: 1652
		private int ocState;

		// Token: 0x04000675 RID: 1653
		private int miscStatusBits;

		// Token: 0x04000676 RID: 1654
		private int freezeCount;

		// Token: 0x04000677 RID: 1655
		private int flags;

		// Token: 0x04000678 RID: 1656
		private int selectionStyle;

		// Token: 0x04000679 RID: 1657
		private int editMode;

		// Token: 0x0400067A RID: 1658
		private int noComponentChange;

		// Token: 0x0400067B RID: 1659
		private IntPtr wndprocAddr = IntPtr.Zero;

		// Token: 0x0400067C RID: 1660
		private Guid clsid;

		// Token: 0x0400067D RID: 1661
		private string text = "";

		// Token: 0x0400067E RID: 1662
		private string licenseKey;

		// Token: 0x0400067F RID: 1663
		private readonly AxHost.OleInterfaces oleSite;

		// Token: 0x04000680 RID: 1664
		private AxHost.AxComponentEditor editor;

		// Token: 0x04000681 RID: 1665
		private AxHost.AxContainer container;

		// Token: 0x04000682 RID: 1666
		private ContainerControl containingControl;

		// Token: 0x04000683 RID: 1667
		private ContainerControl newParent;

		// Token: 0x04000684 RID: 1668
		private AxHost.AxContainer axContainer;

		// Token: 0x04000685 RID: 1669
		private AxHost.State ocxState;

		// Token: 0x04000686 RID: 1670
		private IntPtr hwndFocus = IntPtr.Zero;

		// Token: 0x04000687 RID: 1671
		private Hashtable properties;

		// Token: 0x04000688 RID: 1672
		private Hashtable propertyInfos;

		// Token: 0x04000689 RID: 1673
		private PropertyDescriptorCollection propsStash;

		// Token: 0x0400068A RID: 1674
		private Attribute[] attribsStash;

		// Token: 0x0400068B RID: 1675
		private object instance;

		// Token: 0x0400068C RID: 1676
		private UnsafeNativeMethods.IOleInPlaceObject iOleInPlaceObject;

		// Token: 0x0400068D RID: 1677
		private UnsafeNativeMethods.IOleObject iOleObject;

		// Token: 0x0400068E RID: 1678
		private UnsafeNativeMethods.IOleControl iOleControl;

		// Token: 0x0400068F RID: 1679
		private UnsafeNativeMethods.IOleInPlaceActiveObject iOleInPlaceActiveObject;

		// Token: 0x04000690 RID: 1680
		private UnsafeNativeMethods.IOleInPlaceActiveObject iOleInPlaceActiveObjectExternal;

		// Token: 0x04000691 RID: 1681
		private NativeMethods.IPerPropertyBrowsing iPerPropertyBrowsing;

		// Token: 0x04000692 RID: 1682
		private NativeMethods.ICategorizeProperties iCategorizeProperties;

		// Token: 0x04000693 RID: 1683
		private UnsafeNativeMethods.IPersistPropertyBag iPersistPropBag;

		// Token: 0x04000694 RID: 1684
		private UnsafeNativeMethods.IPersistStream iPersistStream;

		// Token: 0x04000695 RID: 1685
		private UnsafeNativeMethods.IPersistStreamInit iPersistStreamInit;

		// Token: 0x04000696 RID: 1686
		private UnsafeNativeMethods.IPersistStorage iPersistStorage;

		// Token: 0x04000697 RID: 1687
		private AxHost.AboutBoxDelegate aboutBoxDelegate;

		// Token: 0x04000698 RID: 1688
		private EventHandler selectionChangeHandler;

		// Token: 0x04000699 RID: 1689
		private bool isMaskEdit;

		// Token: 0x0400069A RID: 1690
		private bool ignoreDialogKeys;

		// Token: 0x0400069B RID: 1691
		private EventHandler onContainerVisibleChanged;

		// Token: 0x0400069C RID: 1692
		private static CategoryAttribute[] categoryNames = new CategoryAttribute[]
		{
			null,
			new WinCategoryAttribute("Default"),
			new WinCategoryAttribute("Default"),
			new WinCategoryAttribute("Font"),
			new WinCategoryAttribute("Layout"),
			new WinCategoryAttribute("Appearance"),
			new WinCategoryAttribute("Behavior"),
			new WinCategoryAttribute("Data"),
			new WinCategoryAttribute("List"),
			new WinCategoryAttribute("Text"),
			new WinCategoryAttribute("Scale"),
			new WinCategoryAttribute("DDE")
		};

		// Token: 0x0400069D RID: 1693
		private Hashtable objectDefinedCategoryNames;

		// Token: 0x0400069E RID: 1694
		private const int HMperInch = 2540;

		// Token: 0x02000608 RID: 1544
		internal class AxFlags
		{
			// Token: 0x040038DE RID: 14558
			internal const int PreventEditMode = 1;

			// Token: 0x040038DF RID: 14559
			internal const int IncludePropertiesVerb = 2;

			// Token: 0x040038E0 RID: 14560
			internal const int IgnoreThreadModel = 268435456;
		}

		// Token: 0x02000609 RID: 1545
		[AttributeUsage(AttributeTargets.Class, Inherited = false)]
		public sealed class ClsidAttribute : Attribute
		{
			// Token: 0x0600623F RID: 25151 RVA: 0x0016B990 File Offset: 0x00169B90
			public ClsidAttribute(string clsid)
			{
				this.val = clsid;
			}

			// Token: 0x1700150A RID: 5386
			// (get) Token: 0x06006240 RID: 25152 RVA: 0x0016B99F File Offset: 0x00169B9F
			public string Value
			{
				get
				{
					return this.val;
				}
			}

			// Token: 0x040038E1 RID: 14561
			private string val;
		}

		// Token: 0x0200060A RID: 1546
		[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
		public sealed class TypeLibraryTimeStampAttribute : Attribute
		{
			// Token: 0x06006241 RID: 25153 RVA: 0x0016B9A7 File Offset: 0x00169BA7
			public TypeLibraryTimeStampAttribute(string timestamp)
			{
				this.val = DateTime.Parse(timestamp, CultureInfo.InvariantCulture);
			}

			// Token: 0x1700150B RID: 5387
			// (get) Token: 0x06006242 RID: 25154 RVA: 0x0016B9C0 File Offset: 0x00169BC0
			public DateTime Value
			{
				get
				{
					return this.val;
				}
			}

			// Token: 0x040038E2 RID: 14562
			private DateTime val;
		}

		// Token: 0x0200060B RID: 1547
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class ConnectionPointCookie
		{
			// Token: 0x06006243 RID: 25155 RVA: 0x0016B9C8 File Offset: 0x00169BC8
			public ConnectionPointCookie(object source, object sink, Type eventInterface) : this(source, sink, eventInterface, true)
			{
			}

			// Token: 0x06006244 RID: 25156 RVA: 0x0016B9D4 File Offset: 0x00169BD4
			internal ConnectionPointCookie(object source, object sink, Type eventInterface, bool throwException)
			{
				if (source is UnsafeNativeMethods.IConnectionPointContainer)
				{
					UnsafeNativeMethods.IConnectionPointContainer connectionPointContainer = (UnsafeNativeMethods.IConnectionPointContainer)source;
					try
					{
						Guid guid = eventInterface.GUID;
						if (connectionPointContainer.FindConnectionPoint(ref guid, out this.connectionPoint) != 0)
						{
							this.connectionPoint = null;
						}
					}
					catch
					{
						this.connectionPoint = null;
					}
					if (this.connectionPoint == null)
					{
						if (throwException)
						{
							throw new ArgumentException(SR.GetString("AXNoEventInterface", new object[]
							{
								eventInterface.Name
							}));
						}
					}
					else if (sink == null || !eventInterface.IsInstanceOfType(sink))
					{
						if (throwException)
						{
							throw new InvalidCastException(SR.GetString("AXNoSinkImplementation", new object[]
							{
								eventInterface.Name
							}));
						}
					}
					else
					{
						int num = this.connectionPoint.Advise(sink, ref this.cookie);
						if (num == 0)
						{
							this.threadId = Thread.CurrentThread.ManagedThreadId;
						}
						else
						{
							this.cookie = 0;
							Marshal.ReleaseComObject(this.connectionPoint);
							this.connectionPoint = null;
							if (throwException)
							{
								throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SR.GetString("AXNoSinkAdvise", new object[]
								{
									eventInterface.Name
								}), new object[]
								{
									num
								}));
							}
						}
					}
				}
				else if (throwException)
				{
					throw new InvalidCastException(SR.GetString("AXNoConnectionPointContainer"));
				}
				if (this.connectionPoint == null || this.cookie == 0)
				{
					if (this.connectionPoint != null)
					{
						Marshal.ReleaseComObject(this.connectionPoint);
					}
					if (throwException)
					{
						throw new ArgumentException(SR.GetString("AXNoConnectionPoint", new object[]
						{
							eventInterface.Name
						}));
					}
				}
			}

			// Token: 0x06006245 RID: 25157 RVA: 0x0016BB6C File Offset: 0x00169D6C
			public void Disconnect()
			{
				if (this.connectionPoint != null && this.cookie != 0)
				{
					try
					{
						this.connectionPoint.Unadvise(this.cookie);
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
					finally
					{
						this.cookie = 0;
					}
					try
					{
						Marshal.ReleaseComObject(this.connectionPoint);
					}
					catch (Exception ex2)
					{
						if (ClientUtils.IsCriticalException(ex2))
						{
							throw;
						}
					}
					finally
					{
						this.connectionPoint = null;
					}
				}
			}

			// Token: 0x06006246 RID: 25158 RVA: 0x0016BC0C File Offset: 0x00169E0C
			protected override void Finalize()
			{
				try
				{
					if (this.connectionPoint != null && this.cookie != 0 && !AppDomain.CurrentDomain.IsFinalizingForUnload())
					{
						SynchronizationContext synchronizationContext = SynchronizationContext.Current;
						if (synchronizationContext != null)
						{
							synchronizationContext.Post(new SendOrPostCallback(this.AttemptDisconnect), null);
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x06006247 RID: 25159 RVA: 0x0016BC6C File Offset: 0x00169E6C
			private void AttemptDisconnect(object trash)
			{
				if (this.threadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.Disconnect();
				}
			}

			// Token: 0x1700150C RID: 5388
			// (get) Token: 0x06006248 RID: 25160 RVA: 0x0016BC86 File Offset: 0x00169E86
			internal bool Connected
			{
				get
				{
					return this.connectionPoint != null && this.cookie != 0;
				}
			}

			// Token: 0x040038E3 RID: 14563
			private UnsafeNativeMethods.IConnectionPoint connectionPoint;

			// Token: 0x040038E4 RID: 14564
			private int cookie;

			// Token: 0x040038E5 RID: 14565
			internal int threadId;
		}

		// Token: 0x0200060C RID: 1548
		public enum ActiveXInvokeKind
		{
			// Token: 0x040038E7 RID: 14567
			MethodInvoke,
			// Token: 0x040038E8 RID: 14568
			PropertyGet,
			// Token: 0x040038E9 RID: 14569
			PropertySet
		}

		// Token: 0x0200060D RID: 1549
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class InvalidActiveXStateException : Exception
		{
			// Token: 0x06006249 RID: 25161 RVA: 0x0016BC9B File Offset: 0x00169E9B
			public InvalidActiveXStateException(string name, AxHost.ActiveXInvokeKind kind)
			{
				this.name = name;
				this.kind = kind;
			}

			// Token: 0x0600624A RID: 25162 RVA: 0x0016BCB1 File Offset: 0x00169EB1
			public InvalidActiveXStateException()
			{
			}

			// Token: 0x0600624B RID: 25163 RVA: 0x0016BCBC File Offset: 0x00169EBC
			public override string ToString()
			{
				switch (this.kind)
				{
				case AxHost.ActiveXInvokeKind.MethodInvoke:
					return SR.GetString("AXInvalidMethodInvoke", new object[]
					{
						this.name
					});
				case AxHost.ActiveXInvokeKind.PropertyGet:
					return SR.GetString("AXInvalidPropertyGet", new object[]
					{
						this.name
					});
				case AxHost.ActiveXInvokeKind.PropertySet:
					return SR.GetString("AXInvalidPropertySet", new object[]
					{
						this.name
					});
				default:
					return base.ToString();
				}
			}

			// Token: 0x040038EA RID: 14570
			private string name;

			// Token: 0x040038EB RID: 14571
			private AxHost.ActiveXInvokeKind kind;
		}

		// Token: 0x0200060E RID: 1550
		private class OleInterfaces : UnsafeNativeMethods.IOleControlSite, UnsafeNativeMethods.IOleClientSite, UnsafeNativeMethods.IOleInPlaceSite, UnsafeNativeMethods.ISimpleFrameSite, UnsafeNativeMethods.IVBGetControl, UnsafeNativeMethods.IGetVBAObject, UnsafeNativeMethods.IPropertyNotifySink, IReflect, IDisposable
		{
			// Token: 0x0600624C RID: 25164 RVA: 0x0016BD38 File Offset: 0x00169F38
			internal OleInterfaces(AxHost host)
			{
				if (host == null)
				{
					throw new ArgumentNullException("host");
				}
				this.host = host;
			}

			// Token: 0x0600624D RID: 25165 RVA: 0x0016BD58 File Offset: 0x00169F58
			private void Dispose(bool disposing)
			{
				if (disposing && !AppDomain.CurrentDomain.IsFinalizingForUnload())
				{
					SynchronizationContext synchronizationContext = SynchronizationContext.Current;
					if (synchronizationContext != null)
					{
						synchronizationContext.Post(new SendOrPostCallback(this.AttemptStopEvents), null);
					}
				}
			}

			// Token: 0x0600624E RID: 25166 RVA: 0x0016BD90 File Offset: 0x00169F90
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x0600624F RID: 25167 RVA: 0x0016BD9F File Offset: 0x00169F9F
			internal AxHost GetAxHost()
			{
				return this.host;
			}

			// Token: 0x06006250 RID: 25168 RVA: 0x0016BDA7 File Offset: 0x00169FA7
			internal void OnOcxCreate()
			{
				this.StartEvents();
			}

			// Token: 0x06006251 RID: 25169 RVA: 0x0016BDB0 File Offset: 0x00169FB0
			internal void StartEvents()
			{
				if (this.connectionPoint != null)
				{
					return;
				}
				object ocx = this.host.GetOcx();
				try
				{
					this.connectionPoint = new AxHost.ConnectionPointCookie(ocx, this, typeof(UnsafeNativeMethods.IPropertyNotifySink));
				}
				catch
				{
				}
			}

			// Token: 0x06006252 RID: 25170 RVA: 0x0016BE00 File Offset: 0x0016A000
			private void AttemptStopEvents(object trash)
			{
				if (this.connectionPoint == null)
				{
					return;
				}
				if (this.connectionPoint.threadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.StopEvents();
				}
			}

			// Token: 0x06006253 RID: 25171 RVA: 0x0016BE28 File Offset: 0x0016A028
			internal void StopEvents()
			{
				if (this.connectionPoint != null)
				{
					this.connectionPoint.Disconnect();
					this.connectionPoint = null;
				}
			}

			// Token: 0x06006254 RID: 25172 RVA: 0x0016BE44 File Offset: 0x0016A044
			int UnsafeNativeMethods.IGetVBAObject.GetObject(ref Guid riid, UnsafeNativeMethods.IVBFormat[] rval, int dwReserved)
			{
				if (rval == null || riid.Equals(Guid.Empty))
				{
					return -2147024809;
				}
				if (riid.Equals(AxHost.ivbformat_Guid))
				{
					rval[0] = new AxHost.VBFormat();
					return 0;
				}
				rval[0] = null;
				return -2147467262;
			}

			// Token: 0x06006255 RID: 25173 RVA: 0x0016BE7C File Offset: 0x0016A07C
			int UnsafeNativeMethods.IVBGetControl.EnumControls(int dwOleContF, int dwWhich, out UnsafeNativeMethods.IEnumUnknown ppenum)
			{
				ppenum = null;
				ppenum = this.host.GetParentContainer().EnumControls(this.host, dwOleContF, dwWhich);
				return 0;
			}

			// Token: 0x06006256 RID: 25174 RVA: 0x00011A20 File Offset: 0x0000FC20
			int UnsafeNativeMethods.ISimpleFrameSite.PreMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, ref int pdwCookie)
			{
				return 0;
			}

			// Token: 0x06006257 RID: 25175 RVA: 0x00013062 File Offset: 0x00011262
			int UnsafeNativeMethods.ISimpleFrameSite.PostMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, int dwCookie)
			{
				return 1;
			}

			// Token: 0x06006258 RID: 25176 RVA: 0x00015ECC File Offset: 0x000140CC
			MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
			{
				return null;
			}

			// Token: 0x06006259 RID: 25177 RVA: 0x00015ECC File Offset: 0x000140CC
			MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x0600625A RID: 25178 RVA: 0x0016BE9C File Offset: 0x0016A09C
			MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
			{
				return new MethodInfo[0];
			}

			// Token: 0x0600625B RID: 25179 RVA: 0x00015ECC File Offset: 0x000140CC
			FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x0600625C RID: 25180 RVA: 0x0016BEA4 File Offset: 0x0016A0A4
			FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
			{
				return new FieldInfo[0];
			}

			// Token: 0x0600625D RID: 25181 RVA: 0x00015ECC File Offset: 0x000140CC
			PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x0600625E RID: 25182 RVA: 0x00015ECC File Offset: 0x000140CC
			PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
			{
				return null;
			}

			// Token: 0x0600625F RID: 25183 RVA: 0x0016BEAC File Offset: 0x0016A0AC
			PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
			{
				return new PropertyInfo[0];
			}

			// Token: 0x06006260 RID: 25184 RVA: 0x0016BEB4 File Offset: 0x0016A0B4
			MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
			{
				return new MemberInfo[0];
			}

			// Token: 0x06006261 RID: 25185 RVA: 0x0016BEB4 File Offset: 0x0016A0B4
			MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
			{
				return new MemberInfo[0];
			}

			// Token: 0x06006262 RID: 25186 RVA: 0x0016BEBC File Offset: 0x0016A0BC
			object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
			{
				if (name.StartsWith("[DISPID="))
				{
					int num = name.IndexOf("]");
					int dispid = int.Parse(name.Substring(8, num - 8), CultureInfo.InvariantCulture);
					object ambientProperty = this.host.GetAmbientProperty(dispid);
					if (ambientProperty != null)
					{
						return ambientProperty;
					}
				}
				throw AxHost.E_FAIL;
			}

			// Token: 0x1700150D RID: 5389
			// (get) Token: 0x06006263 RID: 25187 RVA: 0x00015ECC File Offset: 0x000140CC
			Type IReflect.UnderlyingSystemType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006264 RID: 25188 RVA: 0x00011A20 File Offset: 0x0000FC20
			int UnsafeNativeMethods.IOleControlSite.OnControlInfoChanged()
			{
				return 0;
			}

			// Token: 0x06006265 RID: 25189 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleControlSite.LockInPlaceActive(int fLock)
			{
				return -2147467263;
			}

			// Token: 0x06006266 RID: 25190 RVA: 0x0016BF0E File Offset: 0x0016A10E
			int UnsafeNativeMethods.IOleControlSite.GetExtendedControl(out object ppDisp)
			{
				ppDisp = this.host.GetParentContainer().GetProxyForControl(this.host);
				if (ppDisp == null)
				{
					return -2147467263;
				}
				return 0;
			}

			// Token: 0x06006267 RID: 25191 RVA: 0x0016BF34 File Offset: 0x0016A134
			int UnsafeNativeMethods.IOleControlSite.TransformCoords(NativeMethods._POINTL pPtlHimetric, NativeMethods.tagPOINTF pPtfContainer, int dwFlags)
			{
				int num = AxHost.SetupLogPixels(false);
				if (NativeMethods.Failed(num))
				{
					return num;
				}
				if ((dwFlags & 4) != 0)
				{
					if ((dwFlags & 2) != 0)
					{
						pPtfContainer.x = (float)this.host.HM2Pix(pPtlHimetric.x, AxHost.logPixelsX);
						pPtfContainer.y = (float)this.host.HM2Pix(pPtlHimetric.y, AxHost.logPixelsY);
					}
					else
					{
						if ((dwFlags & 1) == 0)
						{
							return -2147024809;
						}
						pPtfContainer.x = (float)this.host.HM2Pix(pPtlHimetric.x, AxHost.logPixelsX);
						pPtfContainer.y = (float)this.host.HM2Pix(pPtlHimetric.y, AxHost.logPixelsY);
					}
				}
				else
				{
					if ((dwFlags & 8) == 0)
					{
						return -2147024809;
					}
					if ((dwFlags & 2) != 0)
					{
						pPtlHimetric.x = this.host.Pix2HM((int)pPtfContainer.x, AxHost.logPixelsX);
						pPtlHimetric.y = this.host.Pix2HM((int)pPtfContainer.y, AxHost.logPixelsY);
					}
					else
					{
						if ((dwFlags & 1) == 0)
						{
							return -2147024809;
						}
						pPtlHimetric.x = this.host.Pix2HM((int)pPtfContainer.x, AxHost.logPixelsX);
						pPtlHimetric.y = this.host.Pix2HM((int)pPtfContainer.y, AxHost.logPixelsY);
					}
				}
				return 0;
			}

			// Token: 0x06006268 RID: 25192 RVA: 0x0016C080 File Offset: 0x0016A280
			int UnsafeNativeMethods.IOleControlSite.TranslateAccelerator(ref NativeMethods.MSG pMsg, int grfModifiers)
			{
				this.host.SetAxState(AxHost.siteProcessedInputKey, true);
				Message message = default(Message);
				message.Msg = pMsg.message;
				message.WParam = pMsg.wParam;
				message.LParam = pMsg.lParam;
				message.HWnd = pMsg.hwnd;
				int result;
				try
				{
					result = (this.host.PreProcessMessage(ref message) ? 0 : 1);
				}
				finally
				{
					this.host.SetAxState(AxHost.siteProcessedInputKey, false);
				}
				return result;
			}

			// Token: 0x06006269 RID: 25193 RVA: 0x00011A20 File Offset: 0x0000FC20
			int UnsafeNativeMethods.IOleControlSite.OnFocus(int fGotFocus)
			{
				return 0;
			}

			// Token: 0x0600626A RID: 25194 RVA: 0x0016C118 File Offset: 0x0016A318
			int UnsafeNativeMethods.IOleControlSite.ShowPropertyFrame()
			{
				if (this.host.CanShowPropertyPages())
				{
					this.host.ShowPropertyPages();
					return 0;
				}
				return -2147467263;
			}

			// Token: 0x0600626B RID: 25195 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleClientSite.SaveObject()
			{
				return -2147467263;
			}

			// Token: 0x0600626C RID: 25196 RVA: 0x0003BE4F File Offset: 0x0003A04F
			int UnsafeNativeMethods.IOleClientSite.GetMoniker(int dwAssign, int dwWhichMoniker, out object moniker)
			{
				moniker = null;
				return -2147467263;
			}

			// Token: 0x0600626D RID: 25197 RVA: 0x0016C139 File Offset: 0x0016A339
			int UnsafeNativeMethods.IOleClientSite.GetContainer(out UnsafeNativeMethods.IOleContainer container)
			{
				container = this.host.GetParentContainer();
				return 0;
			}

			// Token: 0x0600626E RID: 25198 RVA: 0x0016C14C File Offset: 0x0016A34C
			int UnsafeNativeMethods.IOleClientSite.ShowObject()
			{
				if (this.host.GetAxState(AxHost.fOwnWindow))
				{
					return 0;
				}
				if (this.host.GetAxState(AxHost.fFakingWindow))
				{
					this.host.DestroyFakeWindow();
					this.host.TransitionDownTo(1);
					this.host.TransitionUpTo(4);
				}
				if (this.host.GetOcState() < 4)
				{
					return 0;
				}
				IntPtr intPtr;
				if (NativeMethods.Succeeded(this.host.GetInPlaceObject().GetWindow(out intPtr)))
				{
					if (this.host.GetHandleNoCreate() != intPtr)
					{
						this.host.DetachWindow();
						if (intPtr != IntPtr.Zero)
						{
							this.host.AttachWindow(intPtr);
						}
					}
				}
				else if (this.host.GetInPlaceObject() is UnsafeNativeMethods.IOleInPlaceObjectWindowless)
				{
					throw new InvalidOperationException(SR.GetString("AXWindowlessControl"));
				}
				return 0;
			}

			// Token: 0x0600626F RID: 25199 RVA: 0x00011A20 File Offset: 0x0000FC20
			int UnsafeNativeMethods.IOleClientSite.OnShowWindow(int fShow)
			{
				return 0;
			}

			// Token: 0x06006270 RID: 25200 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleClientSite.RequestNewObjectLayout()
			{
				return -2147467263;
			}

			// Token: 0x06006271 RID: 25201 RVA: 0x0016C228 File Offset: 0x0016A428
			IntPtr UnsafeNativeMethods.IOleInPlaceSite.GetWindow()
			{
				IntPtr result;
				try
				{
					Control parentInternal = this.host.ParentInternal;
					result = ((parentInternal != null) ? parentInternal.Handle : IntPtr.Zero);
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return result;
			}

			// Token: 0x06006272 RID: 25202 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleInPlaceSite.ContextSensitiveHelp(int fEnterMode)
			{
				return -2147467263;
			}

			// Token: 0x06006273 RID: 25203 RVA: 0x00011A20 File Offset: 0x0000FC20
			int UnsafeNativeMethods.IOleInPlaceSite.CanInPlaceActivate()
			{
				return 0;
			}

			// Token: 0x06006274 RID: 25204 RVA: 0x0016C268 File Offset: 0x0016A468
			int UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceActivate()
			{
				this.host.SetAxState(AxHost.ownDisposing, false);
				this.host.SetAxState(AxHost.rejectSelection, false);
				this.host.SetOcState(4);
				return 0;
			}

			// Token: 0x06006275 RID: 25205 RVA: 0x0016C299 File Offset: 0x0016A499
			int UnsafeNativeMethods.IOleInPlaceSite.OnUIActivate()
			{
				this.host.SetOcState(8);
				this.host.GetParentContainer().OnUIActivate(this.host);
				return 0;
			}

			// Token: 0x06006276 RID: 25206 RVA: 0x0016C2C0 File Offset: 0x0016A4C0
			int UnsafeNativeMethods.IOleInPlaceSite.GetWindowContext(out UnsafeNativeMethods.IOleInPlaceFrame ppFrame, out UnsafeNativeMethods.IOleInPlaceUIWindow ppDoc, NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect, NativeMethods.tagOIFI lpFrameInfo)
			{
				ppDoc = null;
				ppFrame = this.host.GetParentContainer();
				AxHost.FillInRect(lprcPosRect, this.host.Bounds);
				this.host.GetClipRect(lprcClipRect);
				if (lpFrameInfo != null)
				{
					lpFrameInfo.cb = Marshal.SizeOf(typeof(NativeMethods.tagOIFI));
					lpFrameInfo.fMDIApp = false;
					lpFrameInfo.hAccel = IntPtr.Zero;
					lpFrameInfo.cAccelEntries = 0;
					lpFrameInfo.hwndFrame = this.host.ParentInternal.Handle;
				}
				return 0;
			}

			// Token: 0x06006277 RID: 25207 RVA: 0x0016C34C File Offset: 0x0016A54C
			int UnsafeNativeMethods.IOleInPlaceSite.Scroll(NativeMethods.tagSIZE scrollExtant)
			{
				try
				{
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return 1;
			}

			// Token: 0x06006278 RID: 25208 RVA: 0x0016C370 File Offset: 0x0016A570
			int UnsafeNativeMethods.IOleInPlaceSite.OnUIDeactivate(int fUndoable)
			{
				this.host.GetParentContainer().OnUIDeactivate(this.host);
				if (this.host.GetOcState() > 4)
				{
					this.host.SetOcState(4);
				}
				return 0;
			}

			// Token: 0x06006279 RID: 25209 RVA: 0x0016C3A4 File Offset: 0x0016A5A4
			int UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceDeactivate()
			{
				if (this.host.GetOcState() == 8)
				{
					((UnsafeNativeMethods.IOleInPlaceSite)this).OnUIDeactivate(0);
				}
				this.host.GetParentContainer().OnInPlaceDeactivate(this.host);
				this.host.DetachWindow();
				this.host.SetOcState(2);
				return 0;
			}

			// Token: 0x0600627A RID: 25210 RVA: 0x00011A20 File Offset: 0x0000FC20
			int UnsafeNativeMethods.IOleInPlaceSite.DiscardUndoState()
			{
				return 0;
			}

			// Token: 0x0600627B RID: 25211 RVA: 0x0016C3F5 File Offset: 0x0016A5F5
			int UnsafeNativeMethods.IOleInPlaceSite.DeactivateAndUndo()
			{
				return this.host.GetInPlaceObject().UIDeactivate();
			}

			// Token: 0x0600627C RID: 25212 RVA: 0x0016C408 File Offset: 0x0016A608
			int UnsafeNativeMethods.IOleInPlaceSite.OnPosRectChange(NativeMethods.COMRECT lprcPosRect)
			{
				bool flag = true;
				if (AxHost.windowsMediaPlayer_Clsid.Equals(this.host.clsid))
				{
					flag = this.host.GetAxState(AxHost.handlePosRectChanged);
				}
				if (flag)
				{
					this.host.GetInPlaceObject().SetObjectRects(lprcPosRect, this.host.GetClipRect(new NativeMethods.COMRECT()));
					this.host.MakeDirty();
				}
				return 0;
			}

			// Token: 0x0600627D RID: 25213 RVA: 0x0016C470 File Offset: 0x0016A670
			void UnsafeNativeMethods.IPropertyNotifySink.OnChanged(int dispid)
			{
				if (this.host.NoComponentChangeEvents != 0)
				{
					return;
				}
				AxHost axHost = this.host;
				int noComponentChangeEvents = axHost.NoComponentChangeEvents;
				axHost.NoComponentChangeEvents = noComponentChangeEvents + 1;
				try
				{
					AxHost.AxPropertyDescriptor axPropertyDescriptor = null;
					if (dispid != -1)
					{
						axPropertyDescriptor = this.host.GetPropertyDescriptorFromDispid(dispid);
						if (axPropertyDescriptor != null)
						{
							axPropertyDescriptor.OnValueChanged(this.host);
							if (!axPropertyDescriptor.SettingValue)
							{
								axPropertyDescriptor.UpdateTypeConverterAndTypeEditor(true);
							}
						}
					}
					else
					{
						PropertyDescriptorCollection properties = ((ICustomTypeDescriptor)this.host).GetProperties();
						foreach (object obj in properties)
						{
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
							axPropertyDescriptor = (propertyDescriptor as AxHost.AxPropertyDescriptor);
							if (axPropertyDescriptor != null && !axPropertyDescriptor.SettingValue)
							{
								axPropertyDescriptor.UpdateTypeConverterAndTypeEditor(true);
							}
						}
					}
					ISite site = this.host.Site;
					if (site != null)
					{
						IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
						if (componentChangeService != null)
						{
							try
							{
								componentChangeService.OnComponentChanging(this.host, axPropertyDescriptor);
							}
							catch (CheckoutException ex)
							{
								if (ex == CheckoutException.Canceled)
								{
									return;
								}
								throw ex;
							}
							componentChangeService.OnComponentChanged(this.host, axPropertyDescriptor, null, (axPropertyDescriptor != null) ? axPropertyDescriptor.GetValue(this.host) : null);
						}
					}
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
				finally
				{
					AxHost axHost2 = this.host;
					noComponentChangeEvents = axHost2.NoComponentChangeEvents;
					axHost2.NoComponentChangeEvents = noComponentChangeEvents - 1;
				}
			}

			// Token: 0x0600627E RID: 25214 RVA: 0x00011A20 File Offset: 0x0000FC20
			int UnsafeNativeMethods.IPropertyNotifySink.OnRequestEdit(int dispid)
			{
				return 0;
			}

			// Token: 0x040038EC RID: 14572
			private AxHost host;

			// Token: 0x040038ED RID: 14573
			private AxHost.ConnectionPointCookie connectionPoint;
		}

		// Token: 0x0200060F RID: 1551
		private class VBFormat : UnsafeNativeMethods.IVBFormat
		{
			// Token: 0x0600627F RID: 25215 RVA: 0x0016C620 File Offset: 0x0016A820
			int UnsafeNativeMethods.IVBFormat.Format(ref object var, IntPtr pszFormat, IntPtr lpBuffer, short cpBuffer, int lcid, short firstD, short firstW, short[] result)
			{
				if (result == null)
				{
					return -2147024809;
				}
				result[0] = 0;
				if (lpBuffer == IntPtr.Zero || cpBuffer < 2)
				{
					return -2147024809;
				}
				IntPtr zero = IntPtr.Zero;
				int num = UnsafeNativeMethods.VarFormat(ref var, new HandleRef(null, pszFormat), (int)firstD, (int)firstW, 32U, ref zero);
				try
				{
					int num2 = 0;
					if (zero != IntPtr.Zero)
					{
						cpBuffer -= 1;
						short val;
						while (num2 < (int)cpBuffer && (val = Marshal.ReadInt16(zero, num2 * 2)) != 0)
						{
							Marshal.WriteInt16(lpBuffer, num2 * 2, val);
							num2++;
						}
					}
					Marshal.WriteInt16(lpBuffer, num2 * 2, 0);
					result[0] = (short)num2;
				}
				finally
				{
					SafeNativeMethods.SysFreeString(new HandleRef(null, zero));
				}
				return 0;
			}
		}

		// Token: 0x02000610 RID: 1552
		internal class EnumUnknown : UnsafeNativeMethods.IEnumUnknown
		{
			// Token: 0x06006281 RID: 25217 RVA: 0x0016C6DC File Offset: 0x0016A8DC
			internal EnumUnknown(object[] arr)
			{
				this.arr = arr;
				this.loc = 0;
				this.size = ((arr == null) ? 0 : arr.Length);
			}

			// Token: 0x06006282 RID: 25218 RVA: 0x0016C701 File Offset: 0x0016A901
			private EnumUnknown(object[] arr, int loc) : this(arr)
			{
				this.loc = loc;
			}

			// Token: 0x06006283 RID: 25219 RVA: 0x0016C714 File Offset: 0x0016A914
			int UnsafeNativeMethods.IEnumUnknown.Next(int celt, IntPtr rgelt, IntPtr pceltFetched)
			{
				if (pceltFetched != IntPtr.Zero)
				{
					Marshal.WriteInt32(pceltFetched, 0, 0);
				}
				if (celt < 0)
				{
					return -2147024809;
				}
				int num = 0;
				if (this.loc >= this.size)
				{
					num = 0;
				}
				else
				{
					while (this.loc < this.size && num < celt)
					{
						if (this.arr[this.loc] != null)
						{
							Marshal.WriteIntPtr(rgelt, Marshal.GetIUnknownForObject(this.arr[this.loc]));
							rgelt = (IntPtr)((long)rgelt + (long)sizeof(IntPtr));
							num++;
						}
						this.loc++;
					}
				}
				if (pceltFetched != IntPtr.Zero)
				{
					Marshal.WriteInt32(pceltFetched, 0, num);
				}
				if (num != celt)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06006284 RID: 25220 RVA: 0x0016C7D0 File Offset: 0x0016A9D0
			int UnsafeNativeMethods.IEnumUnknown.Skip(int celt)
			{
				this.loc += celt;
				if (this.loc >= this.size)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06006285 RID: 25221 RVA: 0x0016C7F1 File Offset: 0x0016A9F1
			void UnsafeNativeMethods.IEnumUnknown.Reset()
			{
				this.loc = 0;
			}

			// Token: 0x06006286 RID: 25222 RVA: 0x0016C7FA File Offset: 0x0016A9FA
			void UnsafeNativeMethods.IEnumUnknown.Clone(out UnsafeNativeMethods.IEnumUnknown ppenum)
			{
				ppenum = new AxHost.EnumUnknown(this.arr, this.loc);
			}

			// Token: 0x040038EE RID: 14574
			private object[] arr;

			// Token: 0x040038EF RID: 14575
			private int loc;

			// Token: 0x040038F0 RID: 14576
			private int size;
		}

		// Token: 0x02000611 RID: 1553
		internal class AxContainer : UnsafeNativeMethods.IOleContainer, UnsafeNativeMethods.IOleInPlaceFrame, IReflect
		{
			// Token: 0x06006287 RID: 25223 RVA: 0x0016C80F File Offset: 0x0016AA0F
			internal AxContainer(ContainerControl parent)
			{
				this.parent = parent;
				if (parent.Created)
				{
					this.FormCreated();
				}
			}

			// Token: 0x06006288 RID: 25224 RVA: 0x00015ECC File Offset: 0x000140CC
			MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
			{
				return null;
			}

			// Token: 0x06006289 RID: 25225 RVA: 0x00015ECC File Offset: 0x000140CC
			MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x0600628A RID: 25226 RVA: 0x0016BE9C File Offset: 0x0016A09C
			MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
			{
				return new MethodInfo[0];
			}

			// Token: 0x0600628B RID: 25227 RVA: 0x00015ECC File Offset: 0x000140CC
			FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x0600628C RID: 25228 RVA: 0x0016BEA4 File Offset: 0x0016A0A4
			FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
			{
				return new FieldInfo[0];
			}

			// Token: 0x0600628D RID: 25229 RVA: 0x00015ECC File Offset: 0x000140CC
			PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x0600628E RID: 25230 RVA: 0x00015ECC File Offset: 0x000140CC
			PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
			{
				return null;
			}

			// Token: 0x0600628F RID: 25231 RVA: 0x0016BEAC File Offset: 0x0016A0AC
			PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
			{
				return new PropertyInfo[0];
			}

			// Token: 0x06006290 RID: 25232 RVA: 0x0016BEB4 File Offset: 0x0016A0B4
			MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
			{
				return new MemberInfo[0];
			}

			// Token: 0x06006291 RID: 25233 RVA: 0x0016BEB4 File Offset: 0x0016A0B4
			MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
			{
				return new MemberInfo[0];
			}

			// Token: 0x06006292 RID: 25234 RVA: 0x0016C838 File Offset: 0x0016AA38
			object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
			{
				foreach (object obj in this.containerCache)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string nameForControl = this.GetNameForControl((Control)dictionaryEntry.Key);
					if (nameForControl.Equals(name))
					{
						return this.GetProxyForControl((Control)dictionaryEntry.Value);
					}
				}
				throw AxHost.E_FAIL;
			}

			// Token: 0x1700150E RID: 5390
			// (get) Token: 0x06006293 RID: 25235 RVA: 0x00015ECC File Offset: 0x000140CC
			Type IReflect.UnderlyingSystemType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006294 RID: 25236 RVA: 0x0016C8C8 File Offset: 0x0016AAC8
			internal UnsafeNativeMethods.IExtender GetProxyForControl(Control ctl)
			{
				UnsafeNativeMethods.IExtender extender = null;
				if (this.proxyCache == null)
				{
					this.proxyCache = new Hashtable();
				}
				else
				{
					extender = (UnsafeNativeMethods.IExtender)this.proxyCache[ctl];
				}
				if (extender == null)
				{
					if (ctl != this.parent && !this.GetControlBelongs(ctl))
					{
						AxHost.AxContainer axContainer = AxHost.AxContainer.FindContainerForControl(ctl);
						if (axContainer == null)
						{
							return null;
						}
						extender = new AxHost.AxContainer.ExtenderProxy(ctl, axContainer);
					}
					else
					{
						extender = new AxHost.AxContainer.ExtenderProxy(ctl, this);
					}
					this.proxyCache.Add(ctl, extender);
				}
				return extender;
			}

			// Token: 0x06006295 RID: 25237 RVA: 0x0016C940 File Offset: 0x0016AB40
			internal string GetNameForControl(Control ctl)
			{
				string text = (ctl.Site != null) ? ctl.Site.Name : ctl.Name;
				if (text != null)
				{
					return text;
				}
				return "";
			}

			// Token: 0x06006296 RID: 25238 RVA: 0x00006C59 File Offset: 0x00004E59
			internal object GetProxyForContainer()
			{
				return this;
			}

			// Token: 0x06006297 RID: 25239 RVA: 0x0016C974 File Offset: 0x0016AB74
			internal void AddControl(Control ctl)
			{
				lock (this)
				{
					if (this.containerCache.Contains(ctl))
					{
						throw new ArgumentException(SR.GetString("AXDuplicateControl", new object[]
						{
							this.GetNameForControl(ctl)
						}), "ctl");
					}
					this.containerCache.Add(ctl, ctl);
					if (this.assocContainer == null)
					{
						ISite site = ctl.Site;
						if (site != null)
						{
							this.assocContainer = site.Container;
							IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
							if (componentChangeService != null)
							{
								componentChangeService.ComponentRemoved += this.OnComponentRemoved;
							}
						}
					}
				}
			}

			// Token: 0x06006298 RID: 25240 RVA: 0x0016CA34 File Offset: 0x0016AC34
			internal void RemoveControl(Control ctl)
			{
				lock (this)
				{
					if (this.containerCache.Contains(ctl))
					{
						this.containerCache.Remove(ctl);
					}
				}
			}

			// Token: 0x06006299 RID: 25241 RVA: 0x0016CA84 File Offset: 0x0016AC84
			private void LockComponents()
			{
				this.lockCount++;
			}

			// Token: 0x0600629A RID: 25242 RVA: 0x0016CA94 File Offset: 0x0016AC94
			private void UnlockComponents()
			{
				this.lockCount--;
				if (this.lockCount == 0)
				{
					this.components = null;
				}
			}

			// Token: 0x0600629B RID: 25243 RVA: 0x0016CAB4 File Offset: 0x0016ACB4
			internal UnsafeNativeMethods.IEnumUnknown EnumControls(Control ctl, int dwOleContF, int dwWhich)
			{
				this.GetComponents();
				this.LockComponents();
				UnsafeNativeMethods.IEnumUnknown result;
				try
				{
					ArrayList arrayList = null;
					bool selected = (dwWhich & 1073741824) != 0;
					bool flag = (dwWhich & 134217728) != 0;
					bool flag2 = (dwWhich & 268435456) != 0;
					bool flag3 = (dwWhich & 536870912) != 0;
					dwWhich &= -2013265921;
					if (flag2 && flag3)
					{
						throw AxHost.E_INVALIDARG;
					}
					if ((dwWhich == 2 || dwWhich == 3) && (flag2 || flag3))
					{
						throw AxHost.E_INVALIDARG;
					}
					int num = 0;
					int num2 = -1;
					Control[] array = null;
					switch (dwWhich)
					{
					case 1:
					{
						Control parentInternal = ctl.ParentInternal;
						if (parentInternal != null)
						{
							array = parentInternal.GetChildControlsInTabOrder(false);
							if (flag3)
							{
								num2 = ctl.TabIndex;
							}
							else if (flag2)
							{
								num = ctl.TabIndex + 1;
							}
						}
						else
						{
							array = new Control[0];
						}
						ctl = null;
						break;
					}
					case 2:
						arrayList = new ArrayList();
						this.MaybeAdd(arrayList, ctl, selected, dwOleContF, false);
						while (ctl != null)
						{
							AxHost.AxContainer axContainer = AxHost.AxContainer.FindContainerForControl(ctl);
							if (axContainer == null)
							{
								break;
							}
							this.MaybeAdd(arrayList, axContainer.parent, selected, dwOleContF, true);
							ctl = axContainer.parent;
						}
						break;
					case 3:
						array = ctl.GetChildControlsInTabOrder(false);
						ctl = null;
						break;
					case 4:
					{
						Hashtable hashtable = this.GetComponents();
						array = new Control[hashtable.Keys.Count];
						hashtable.Keys.CopyTo(array, 0);
						ctl = this.parent;
						break;
					}
					default:
						throw AxHost.E_INVALIDARG;
					}
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						if (num2 == -1 && array != null)
						{
							num2 = array.Length;
						}
						if (ctl != null)
						{
							this.MaybeAdd(arrayList, ctl, selected, dwOleContF, false);
						}
						for (int i = num; i < num2; i++)
						{
							this.MaybeAdd(arrayList, array[i], selected, dwOleContF, false);
						}
					}
					object[] array2 = new object[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					if (flag)
					{
						int j = 0;
						int num3 = array2.Length - 1;
						while (j < num3)
						{
							object obj = array2[j];
							array2[j] = array2[num3];
							array2[num3] = obj;
							j++;
							num3--;
						}
					}
					result = new AxHost.EnumUnknown(array2);
				}
				finally
				{
					this.UnlockComponents();
				}
				return result;
			}

			// Token: 0x0600629C RID: 25244 RVA: 0x0016CCD8 File Offset: 0x0016AED8
			private void MaybeAdd(ArrayList l, Control ctl, bool selected, int dwOleContF, bool ignoreBelong)
			{
				if (!ignoreBelong && ctl != this.parent && !this.GetControlBelongs(ctl))
				{
					return;
				}
				if (selected)
				{
					ISelectionService selectionService = AxHost.GetSelectionService(ctl);
					if (selectionService == null || !selectionService.GetComponentSelected(this))
					{
						return;
					}
				}
				AxHost axHost = ctl as AxHost;
				if (axHost != null && (dwOleContF & 1) != 0)
				{
					l.Add(axHost.GetOcx());
					return;
				}
				if ((dwOleContF & 4) != 0)
				{
					object proxyForControl = this.GetProxyForControl(ctl);
					if (proxyForControl != null)
					{
						l.Add(proxyForControl);
					}
				}
			}

			// Token: 0x0600629D RID: 25245 RVA: 0x0016CD4C File Offset: 0x0016AF4C
			private void FillComponentsTable(IContainer container)
			{
				if (container != null)
				{
					ComponentCollection componentCollection = container.Components;
					if (componentCollection != null)
					{
						this.components = new Hashtable();
						foreach (object obj in componentCollection)
						{
							IComponent component = (IComponent)obj;
							if (component is Control && component != this.parent && component.Site != null)
							{
								this.components.Add(component, component);
							}
						}
						return;
					}
				}
				bool flag = true;
				Control[] array = new Control[this.containerCache.Values.Count];
				this.containerCache.Values.CopyTo(array, 0);
				if (array != null)
				{
					if (array.Length != 0 && this.components == null)
					{
						this.components = new Hashtable();
						flag = false;
					}
					for (int i = 0; i < array.Length; i++)
					{
						if (flag && !this.components.Contains(array[i]))
						{
							this.components.Add(array[i], array[i]);
						}
					}
				}
				this.GetAllChildren(this.parent);
			}

			// Token: 0x0600629E RID: 25246 RVA: 0x0016CE6C File Offset: 0x0016B06C
			private void GetAllChildren(Control ctl)
			{
				if (ctl == null)
				{
					return;
				}
				if (this.components == null)
				{
					this.components = new Hashtable();
				}
				if (ctl != this.parent && !this.components.Contains(ctl))
				{
					this.components.Add(ctl, ctl);
				}
				foreach (object obj in ctl.Controls)
				{
					Control ctl2 = (Control)obj;
					this.GetAllChildren(ctl2);
				}
			}

			// Token: 0x0600629F RID: 25247 RVA: 0x0016CF00 File Offset: 0x0016B100
			private Hashtable GetComponents()
			{
				return this.GetComponents(this.GetParentsContainer());
			}

			// Token: 0x060062A0 RID: 25248 RVA: 0x0016CF0E File Offset: 0x0016B10E
			private Hashtable GetComponents(IContainer cont)
			{
				if (this.lockCount == 0)
				{
					this.FillComponentsTable(cont);
				}
				return this.components;
			}

			// Token: 0x060062A1 RID: 25249 RVA: 0x0016CF28 File Offset: 0x0016B128
			private bool GetControlBelongs(Control ctl)
			{
				Hashtable hashtable = this.GetComponents();
				return hashtable[ctl] != null;
			}

			// Token: 0x060062A2 RID: 25250 RVA: 0x0016CF48 File Offset: 0x0016B148
			private IContainer GetParentIsDesigned()
			{
				ISite site = this.parent.Site;
				if (site != null && site.DesignMode)
				{
					return site.Container;
				}
				return null;
			}

			// Token: 0x060062A3 RID: 25251 RVA: 0x0016CF74 File Offset: 0x0016B174
			private IContainer GetParentsContainer()
			{
				IContainer parentIsDesigned = this.GetParentIsDesigned();
				if (parentIsDesigned != null)
				{
					return parentIsDesigned;
				}
				return this.assocContainer;
			}

			// Token: 0x060062A4 RID: 25252 RVA: 0x0016CF94 File Offset: 0x0016B194
			private bool RegisterControl(AxHost ctl)
			{
				ISite site = ctl.Site;
				if (site != null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						if (this.assocContainer != null)
						{
							return container == this.assocContainer;
						}
						this.assocContainer = container;
						IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
						if (componentChangeService != null)
						{
							componentChangeService.ComponentRemoved += this.OnComponentRemoved;
						}
						return true;
					}
				}
				return false;
			}

			// Token: 0x060062A5 RID: 25253 RVA: 0x0016CFFC File Offset: 0x0016B1FC
			private void OnComponentRemoved(object sender, ComponentEventArgs e)
			{
				Control control = e.Component as Control;
				if (sender == this.assocContainer && control != null)
				{
					this.RemoveControl(control);
				}
			}

			// Token: 0x060062A6 RID: 25254 RVA: 0x0016D028 File Offset: 0x0016B228
			internal static AxHost.AxContainer FindContainerForControl(Control ctl)
			{
				AxHost axHost = ctl as AxHost;
				if (axHost != null)
				{
					if (axHost.container != null)
					{
						return axHost.container;
					}
					ContainerControl containingControl = axHost.ContainingControl;
					if (containingControl != null)
					{
						AxHost.AxContainer axContainer = containingControl.CreateAxContainer();
						if (axContainer.RegisterControl(axHost))
						{
							axContainer.AddControl(axHost);
							return axContainer;
						}
					}
				}
				return null;
			}

			// Token: 0x060062A7 RID: 25255 RVA: 0x0016D072 File Offset: 0x0016B272
			internal void OnInPlaceDeactivate(AxHost site)
			{
				if (this.siteActive == site)
				{
					this.siteActive = null;
					if (site.GetSiteOwnsDeactivation())
					{
						this.parent.ActiveControl = null;
					}
				}
			}

			// Token: 0x060062A8 RID: 25256 RVA: 0x0016D098 File Offset: 0x0016B298
			internal void OnUIDeactivate(AxHost site)
			{
				this.siteUIActive = null;
				site.RemoveSelectionHandler();
				site.SetSelectionStyle(1);
				site.editMode = 0;
				if (site.GetSiteOwnsDeactivation())
				{
					ContainerControl containingControl = site.ContainingControl;
				}
			}

			// Token: 0x060062A9 RID: 25257 RVA: 0x0016D0D4 File Offset: 0x0016B2D4
			internal void OnUIActivate(AxHost site)
			{
				if (this.siteUIActive == site)
				{
					return;
				}
				if (this.siteUIActive != null && this.siteUIActive != site)
				{
					AxHost axHost = this.siteUIActive;
					bool axState = axHost.GetAxState(AxHost.ownDisposing);
					try
					{
						axHost.SetAxState(AxHost.ownDisposing, true);
						axHost.GetInPlaceObject().UIDeactivate();
					}
					finally
					{
						axHost.SetAxState(AxHost.ownDisposing, axState);
					}
				}
				site.AddSelectionHandler();
				this.siteUIActive = site;
				ContainerControl containingControl = site.ContainingControl;
				if (containingControl != null)
				{
					containingControl.ActiveControl = site;
				}
			}

			// Token: 0x060062AA RID: 25258 RVA: 0x0016D164 File Offset: 0x0016B364
			private void ListAxControls(ArrayList list, bool fuseOcx)
			{
				Hashtable hashtable = this.GetComponents();
				if (hashtable == null)
				{
					return;
				}
				Control[] array = new Control[hashtable.Keys.Count];
				hashtable.Keys.CopyTo(array, 0);
				if (array != null)
				{
					foreach (Control control in array)
					{
						AxHost axHost = control as AxHost;
						if (axHost != null)
						{
							if (fuseOcx)
							{
								list.Add(axHost.GetOcx());
							}
							else
							{
								list.Add(control);
							}
						}
					}
				}
			}

			// Token: 0x060062AB RID: 25259 RVA: 0x0016D1D6 File Offset: 0x0016B3D6
			internal void ControlCreated(AxHost invoker)
			{
				if (this.formAlreadyCreated)
				{
					if (invoker.IsUserMode() && invoker.AwaitingDefreezing())
					{
						invoker.Freeze(false);
						return;
					}
				}
				else
				{
					this.parent.CreateAxContainer();
				}
			}

			// Token: 0x060062AC RID: 25260 RVA: 0x0016D204 File Offset: 0x0016B404
			internal void FormCreated()
			{
				if (this.formAlreadyCreated)
				{
					return;
				}
				this.formAlreadyCreated = true;
				ArrayList arrayList = new ArrayList();
				this.ListAxControls(arrayList, false);
				AxHost[] array = new AxHost[arrayList.Count];
				arrayList.CopyTo(array, 0);
				foreach (AxHost axHost in array)
				{
					if (axHost.GetOcState() >= 2 && axHost.IsUserMode() && axHost.AwaitingDefreezing())
					{
						axHost.Freeze(false);
					}
				}
			}

			// Token: 0x060062AD RID: 25261 RVA: 0x00139FE5 File Offset: 0x001381E5
			int UnsafeNativeMethods.IOleContainer.ParseDisplayName(object pbc, string pszDisplayName, int[] pchEaten, object[] ppmkOut)
			{
				if (ppmkOut != null)
				{
					ppmkOut[0] = null;
				}
				return -2147467263;
			}

			// Token: 0x060062AE RID: 25262 RVA: 0x0016D278 File Offset: 0x0016B478
			int UnsafeNativeMethods.IOleContainer.EnumObjects(int grfFlags, out UnsafeNativeMethods.IEnumUnknown ppenum)
			{
				ppenum = null;
				if ((grfFlags & 1) != 0)
				{
					ArrayList arrayList = new ArrayList();
					this.ListAxControls(arrayList, true);
					if (arrayList.Count > 0)
					{
						object[] array = new object[arrayList.Count];
						arrayList.CopyTo(array, 0);
						ppenum = new AxHost.EnumUnknown(array);
						return 0;
					}
				}
				ppenum = new AxHost.EnumUnknown(null);
				return 0;
			}

			// Token: 0x060062AF RID: 25263 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleContainer.LockContainer(bool fLock)
			{
				return -2147467263;
			}

			// Token: 0x060062B0 RID: 25264 RVA: 0x0016D2CB File Offset: 0x0016B4CB
			IntPtr UnsafeNativeMethods.IOleInPlaceFrame.GetWindow()
			{
				return this.parent.Handle;
			}

			// Token: 0x060062B1 RID: 25265 RVA: 0x00011A20 File Offset: 0x0000FC20
			int UnsafeNativeMethods.IOleInPlaceFrame.ContextSensitiveHelp(int fEnterMode)
			{
				return 0;
			}

			// Token: 0x060062B2 RID: 25266 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleInPlaceFrame.GetBorder(NativeMethods.COMRECT lprectBorder)
			{
				return -2147467263;
			}

			// Token: 0x060062B3 RID: 25267 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleInPlaceFrame.RequestBorderSpace(NativeMethods.COMRECT pborderwidths)
			{
				return -2147467263;
			}

			// Token: 0x060062B4 RID: 25268 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleInPlaceFrame.SetBorderSpace(NativeMethods.COMRECT pborderwidths)
			{
				return -2147467263;
			}

			// Token: 0x060062B5 RID: 25269 RVA: 0x0016D2D8 File Offset: 0x0016B4D8
			internal void OnExitEditMode(AxHost ctl)
			{
				if (this.ctlInEditMode == null || this.ctlInEditMode != ctl)
				{
					return;
				}
				this.ctlInEditMode = null;
			}

			// Token: 0x060062B6 RID: 25270 RVA: 0x0016D2F4 File Offset: 0x0016B4F4
			int UnsafeNativeMethods.IOleInPlaceFrame.SetActiveObject(UnsafeNativeMethods.IOleInPlaceActiveObject pActiveObject, string pszObjName)
			{
				if (this.siteUIActive != null && this.siteUIActive.iOleInPlaceActiveObjectExternal != pActiveObject)
				{
					if (this.siteUIActive.iOleInPlaceActiveObjectExternal != null)
					{
						Marshal.ReleaseComObject(this.siteUIActive.iOleInPlaceActiveObjectExternal);
					}
					this.siteUIActive.iOleInPlaceActiveObjectExternal = pActiveObject;
				}
				if (pActiveObject == null)
				{
					if (this.ctlInEditMode != null)
					{
						this.ctlInEditMode.editMode = 0;
						this.ctlInEditMode = null;
					}
					return 0;
				}
				AxHost axHost = null;
				if (pActiveObject is UnsafeNativeMethods.IOleObject)
				{
					UnsafeNativeMethods.IOleObject oleObject = (UnsafeNativeMethods.IOleObject)pActiveObject;
					try
					{
						UnsafeNativeMethods.IOleClientSite clientSite = oleObject.GetClientSite();
						if (clientSite is AxHost.OleInterfaces)
						{
							axHost = ((AxHost.OleInterfaces)clientSite).GetAxHost();
						}
					}
					catch (COMException ex)
					{
					}
					if (this.ctlInEditMode != null)
					{
						this.ctlInEditMode.SetSelectionStyle(1);
						this.ctlInEditMode.editMode = 0;
					}
					if (axHost == null)
					{
						this.ctlInEditMode = null;
					}
					else if (!axHost.IsUserMode())
					{
						this.ctlInEditMode = axHost;
						axHost.editMode = 1;
						axHost.AddSelectionHandler();
						axHost.SetSelectionStyle(2);
					}
				}
				return 0;
			}

			// Token: 0x060062B7 RID: 25271 RVA: 0x00011A20 File Offset: 0x0000FC20
			int UnsafeNativeMethods.IOleInPlaceFrame.InsertMenus(IntPtr hmenuShared, NativeMethods.tagOleMenuGroupWidths lpMenuWidths)
			{
				return 0;
			}

			// Token: 0x060062B8 RID: 25272 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleInPlaceFrame.SetMenu(IntPtr hmenuShared, IntPtr holemenu, IntPtr hwndActiveObject)
			{
				return -2147467263;
			}

			// Token: 0x060062B9 RID: 25273 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleInPlaceFrame.RemoveMenus(IntPtr hmenuShared)
			{
				return -2147467263;
			}

			// Token: 0x060062BA RID: 25274 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleInPlaceFrame.SetStatusText(string pszStatusText)
			{
				return -2147467263;
			}

			// Token: 0x060062BB RID: 25275 RVA: 0x0003BE48 File Offset: 0x0003A048
			int UnsafeNativeMethods.IOleInPlaceFrame.EnableModeless(bool fEnable)
			{
				return -2147467263;
			}

			// Token: 0x060062BC RID: 25276 RVA: 0x00013062 File Offset: 0x00011262
			int UnsafeNativeMethods.IOleInPlaceFrame.TranslateAccelerator(ref NativeMethods.MSG lpmsg, short wID)
			{
				return 1;
			}

			// Token: 0x040038F1 RID: 14577
			internal ContainerControl parent;

			// Token: 0x040038F2 RID: 14578
			private IContainer assocContainer;

			// Token: 0x040038F3 RID: 14579
			private AxHost siteUIActive;

			// Token: 0x040038F4 RID: 14580
			private AxHost siteActive;

			// Token: 0x040038F5 RID: 14581
			private bool formAlreadyCreated;

			// Token: 0x040038F6 RID: 14582
			private Hashtable containerCache = new Hashtable();

			// Token: 0x040038F7 RID: 14583
			private int lockCount;

			// Token: 0x040038F8 RID: 14584
			private Hashtable components;

			// Token: 0x040038F9 RID: 14585
			private Hashtable proxyCache;

			// Token: 0x040038FA RID: 14586
			private AxHost ctlInEditMode;

			// Token: 0x040038FB RID: 14587
			private const int GC_CHILD = 1;

			// Token: 0x040038FC RID: 14588
			private const int GC_LASTSIBLING = 2;

			// Token: 0x040038FD RID: 14589
			private const int GC_FIRSTSIBLING = 4;

			// Token: 0x040038FE RID: 14590
			private const int GC_CONTAINER = 32;

			// Token: 0x040038FF RID: 14591
			private const int GC_PREVSIBLING = 64;

			// Token: 0x04003900 RID: 14592
			private const int GC_NEXTSIBLING = 128;

			// Token: 0x020008B5 RID: 2229
			private class ExtenderProxy : UnsafeNativeMethods.IExtender, UnsafeNativeMethods.IVBGetControl, UnsafeNativeMethods.IGetVBAObject, UnsafeNativeMethods.IGetOleObject, IReflect
			{
				// Token: 0x060072A0 RID: 29344 RVA: 0x001A41E3 File Offset: 0x001A23E3
				internal ExtenderProxy(Control principal, AxHost.AxContainer container)
				{
					this.pRef = new WeakReference(principal);
					this.pContainer = new WeakReference(container);
				}

				// Token: 0x060072A1 RID: 29345 RVA: 0x001A4203 File Offset: 0x001A2403
				private Control GetP()
				{
					return (Control)this.pRef.Target;
				}

				// Token: 0x060072A2 RID: 29346 RVA: 0x001A4215 File Offset: 0x001A2415
				private AxHost.AxContainer GetC()
				{
					return (AxHost.AxContainer)this.pContainer.Target;
				}

				// Token: 0x060072A3 RID: 29347 RVA: 0x001A4227 File Offset: 0x001A2427
				int UnsafeNativeMethods.IVBGetControl.EnumControls(int dwOleContF, int dwWhich, out UnsafeNativeMethods.IEnumUnknown ppenum)
				{
					ppenum = this.GetC().EnumControls(this.GetP(), dwOleContF, dwWhich);
					return 0;
				}

				// Token: 0x060072A4 RID: 29348 RVA: 0x001A4240 File Offset: 0x001A2440
				object UnsafeNativeMethods.IGetOleObject.GetOleObject(ref Guid riid)
				{
					if (!riid.Equals(AxHost.ioleobject_Guid))
					{
						throw AxHost.E_INVALIDARG;
					}
					Control p = this.GetP();
					if (p != null && p is AxHost)
					{
						return ((AxHost)p).GetOcx();
					}
					throw AxHost.E_FAIL;
				}

				// Token: 0x060072A5 RID: 29349 RVA: 0x0016BE44 File Offset: 0x0016A044
				int UnsafeNativeMethods.IGetVBAObject.GetObject(ref Guid riid, UnsafeNativeMethods.IVBFormat[] rval, int dwReserved)
				{
					if (rval == null || riid.Equals(Guid.Empty))
					{
						return -2147024809;
					}
					if (riid.Equals(AxHost.ivbformat_Guid))
					{
						rval[0] = new AxHost.VBFormat();
						return 0;
					}
					rval[0] = null;
					return -2147467262;
				}

				// Token: 0x17001922 RID: 6434
				// (get) Token: 0x060072A6 RID: 29350 RVA: 0x001A4284 File Offset: 0x001A2484
				// (set) Token: 0x060072A7 RID: 29351 RVA: 0x001A42A8 File Offset: 0x001A24A8
				public int Align
				{
					get
					{
						int num = (int)this.GetP().Dock;
						if (num < 0 || num > 4)
						{
							num = 0;
						}
						return num;
					}
					set
					{
						this.GetP().Dock = (DockStyle)value;
					}
				}

				// Token: 0x17001923 RID: 6435
				// (get) Token: 0x060072A8 RID: 29352 RVA: 0x001A42B6 File Offset: 0x001A24B6
				// (set) Token: 0x060072A9 RID: 29353 RVA: 0x001A42C8 File Offset: 0x001A24C8
				public uint BackColor
				{
					get
					{
						return AxHost.GetOleColorFromColor(this.GetP().BackColor);
					}
					set
					{
						this.GetP().BackColor = AxHost.GetColorFromOleColor(value);
					}
				}

				// Token: 0x17001924 RID: 6436
				// (get) Token: 0x060072AA RID: 29354 RVA: 0x001A42DB File Offset: 0x001A24DB
				// (set) Token: 0x060072AB RID: 29355 RVA: 0x001A42E8 File Offset: 0x001A24E8
				public bool Enabled
				{
					get
					{
						return this.GetP().Enabled;
					}
					set
					{
						this.GetP().Enabled = value;
					}
				}

				// Token: 0x17001925 RID: 6437
				// (get) Token: 0x060072AC RID: 29356 RVA: 0x001A42F6 File Offset: 0x001A24F6
				// (set) Token: 0x060072AD RID: 29357 RVA: 0x001A4308 File Offset: 0x001A2508
				public uint ForeColor
				{
					get
					{
						return AxHost.GetOleColorFromColor(this.GetP().ForeColor);
					}
					set
					{
						this.GetP().ForeColor = AxHost.GetColorFromOleColor(value);
					}
				}

				// Token: 0x17001926 RID: 6438
				// (get) Token: 0x060072AE RID: 29358 RVA: 0x001A431B File Offset: 0x001A251B
				// (set) Token: 0x060072AF RID: 29359 RVA: 0x001A432E File Offset: 0x001A252E
				public int Height
				{
					get
					{
						return AxHost.Pixel2Twip(this.GetP().Height, false);
					}
					set
					{
						this.GetP().Height = AxHost.Twip2Pixel(value, false);
					}
				}

				// Token: 0x17001927 RID: 6439
				// (get) Token: 0x060072B0 RID: 29360 RVA: 0x001A4342 File Offset: 0x001A2542
				// (set) Token: 0x060072B1 RID: 29361 RVA: 0x001A4355 File Offset: 0x001A2555
				public int Left
				{
					get
					{
						return AxHost.Pixel2Twip(this.GetP().Left, true);
					}
					set
					{
						this.GetP().Left = AxHost.Twip2Pixel(value, true);
					}
				}

				// Token: 0x17001928 RID: 6440
				// (get) Token: 0x060072B2 RID: 29362 RVA: 0x001A4369 File Offset: 0x001A2569
				public object Parent
				{
					get
					{
						return this.GetC().GetProxyForControl(this.GetC().parent);
					}
				}

				// Token: 0x17001929 RID: 6441
				// (get) Token: 0x060072B3 RID: 29363 RVA: 0x001A4381 File Offset: 0x001A2581
				// (set) Token: 0x060072B4 RID: 29364 RVA: 0x001A438F File Offset: 0x001A258F
				public short TabIndex
				{
					get
					{
						return (short)this.GetP().TabIndex;
					}
					set
					{
						this.GetP().TabIndex = (int)value;
					}
				}

				// Token: 0x1700192A RID: 6442
				// (get) Token: 0x060072B5 RID: 29365 RVA: 0x001A439D File Offset: 0x001A259D
				// (set) Token: 0x060072B6 RID: 29366 RVA: 0x001A43AA File Offset: 0x001A25AA
				public bool TabStop
				{
					get
					{
						return this.GetP().TabStop;
					}
					set
					{
						this.GetP().TabStop = value;
					}
				}

				// Token: 0x1700192B RID: 6443
				// (get) Token: 0x060072B7 RID: 29367 RVA: 0x001A43B8 File Offset: 0x001A25B8
				// (set) Token: 0x060072B8 RID: 29368 RVA: 0x001A43CB File Offset: 0x001A25CB
				public int Top
				{
					get
					{
						return AxHost.Pixel2Twip(this.GetP().Top, false);
					}
					set
					{
						this.GetP().Top = AxHost.Twip2Pixel(value, false);
					}
				}

				// Token: 0x1700192C RID: 6444
				// (get) Token: 0x060072B9 RID: 29369 RVA: 0x001A43DF File Offset: 0x001A25DF
				// (set) Token: 0x060072BA RID: 29370 RVA: 0x001A43EC File Offset: 0x001A25EC
				public bool Visible
				{
					get
					{
						return this.GetP().Visible;
					}
					set
					{
						this.GetP().Visible = value;
					}
				}

				// Token: 0x1700192D RID: 6445
				// (get) Token: 0x060072BB RID: 29371 RVA: 0x001A43FA File Offset: 0x001A25FA
				// (set) Token: 0x060072BC RID: 29372 RVA: 0x001A440D File Offset: 0x001A260D
				public int Width
				{
					get
					{
						return AxHost.Pixel2Twip(this.GetP().Width, true);
					}
					set
					{
						this.GetP().Width = AxHost.Twip2Pixel(value, true);
					}
				}

				// Token: 0x1700192E RID: 6446
				// (get) Token: 0x060072BD RID: 29373 RVA: 0x001A4421 File Offset: 0x001A2621
				public string Name
				{
					get
					{
						return this.GetC().GetNameForControl(this.GetP());
					}
				}

				// Token: 0x1700192F RID: 6447
				// (get) Token: 0x060072BE RID: 29374 RVA: 0x001A4434 File Offset: 0x001A2634
				public IntPtr Hwnd
				{
					get
					{
						return this.GetP().Handle;
					}
				}

				// Token: 0x17001930 RID: 6448
				// (get) Token: 0x060072BF RID: 29375 RVA: 0x001A4441 File Offset: 0x001A2641
				public object Container
				{
					get
					{
						return this.GetC().GetProxyForContainer();
					}
				}

				// Token: 0x17001931 RID: 6449
				// (get) Token: 0x060072C0 RID: 29376 RVA: 0x001A444E File Offset: 0x001A264E
				// (set) Token: 0x060072C1 RID: 29377 RVA: 0x001A445B File Offset: 0x001A265B
				public string Text
				{
					get
					{
						return this.GetP().Text;
					}
					set
					{
						this.GetP().Text = value;
					}
				}

				// Token: 0x060072C2 RID: 29378 RVA: 0x000072B6 File Offset: 0x000054B6
				public void Move(object left, object top, object width, object height)
				{
				}

				// Token: 0x060072C3 RID: 29379 RVA: 0x00015ECC File Offset: 0x000140CC
				MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
				{
					return null;
				}

				// Token: 0x060072C4 RID: 29380 RVA: 0x00015ECC File Offset: 0x000140CC
				MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
				{
					return null;
				}

				// Token: 0x060072C5 RID: 29381 RVA: 0x001A4469 File Offset: 0x001A2669
				MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
				{
					return new MethodInfo[]
					{
						base.GetType().GetMethod("Move")
					};
				}

				// Token: 0x060072C6 RID: 29382 RVA: 0x00015ECC File Offset: 0x000140CC
				FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
				{
					return null;
				}

				// Token: 0x060072C7 RID: 29383 RVA: 0x0016BEA4 File Offset: 0x0016A0A4
				FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
				{
					return new FieldInfo[0];
				}

				// Token: 0x060072C8 RID: 29384 RVA: 0x001A4484 File Offset: 0x001A2684
				PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
				{
					PropertyInfo property = this.GetP().GetType().GetProperty(name, bindingAttr);
					if (property == null)
					{
						property = base.GetType().GetProperty(name, bindingAttr);
					}
					return property;
				}

				// Token: 0x060072C9 RID: 29385 RVA: 0x001A44BC File Offset: 0x001A26BC
				PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
				{
					PropertyInfo property = this.GetP().GetType().GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
					if (property == null)
					{
						property = base.GetType().GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
					}
					return property;
				}

				// Token: 0x060072CA RID: 29386 RVA: 0x001A4504 File Offset: 0x001A2704
				PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
				{
					PropertyInfo[] properties = base.GetType().GetProperties(bindingAttr);
					PropertyInfo[] properties2 = this.GetP().GetType().GetProperties(bindingAttr);
					if (properties == null)
					{
						return properties2;
					}
					if (properties2 == null)
					{
						return properties;
					}
					int num = 0;
					PropertyInfo[] array = new PropertyInfo[properties.Length + properties2.Length];
					foreach (PropertyInfo propertyInfo in properties)
					{
						array[num++] = propertyInfo;
					}
					foreach (PropertyInfo propertyInfo2 in properties2)
					{
						array[num++] = propertyInfo2;
					}
					return array;
				}

				// Token: 0x060072CB RID: 29387 RVA: 0x001A4598 File Offset: 0x001A2798
				MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
				{
					MemberInfo[] member = this.GetP().GetType().GetMember(name, bindingAttr);
					if (member == null)
					{
						member = base.GetType().GetMember(name, bindingAttr);
					}
					return member;
				}

				// Token: 0x060072CC RID: 29388 RVA: 0x001A45CC File Offset: 0x001A27CC
				MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
				{
					MemberInfo[] members = base.GetType().GetMembers(bindingAttr);
					MemberInfo[] members2 = this.GetP().GetType().GetMembers(bindingAttr);
					if (members == null)
					{
						return members2;
					}
					if (members2 == null)
					{
						return members;
					}
					MemberInfo[] array = new MemberInfo[members.Length + members2.Length];
					Array.Copy(members, 0, array, 0, members.Length);
					Array.Copy(members2, 0, array, members.Length, members2.Length);
					return array;
				}

				// Token: 0x060072CD RID: 29389 RVA: 0x001A462C File Offset: 0x001A282C
				object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
				{
					object result;
					try
					{
						result = base.GetType().InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
					}
					catch (MissingMethodException)
					{
						result = this.GetP().GetType().InvokeMember(name, invokeAttr, binder, this.GetP(), args, modifiers, culture, namedParameters);
					}
					return result;
				}

				// Token: 0x17001932 RID: 6450
				// (get) Token: 0x060072CE RID: 29390 RVA: 0x00015ECC File Offset: 0x000140CC
				Type IReflect.UnderlyingSystemType
				{
					get
					{
						return null;
					}
				}

				// Token: 0x0400452B RID: 17707
				private WeakReference pRef;

				// Token: 0x0400452C RID: 17708
				private WeakReference pContainer;
			}
		}

		// Token: 0x02000612 RID: 1554
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class StateConverter : TypeConverter
		{
			// Token: 0x060062BD RID: 25277 RVA: 0x0016D3F4 File Offset: 0x0016B5F4
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(byte[]) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x060062BE RID: 25278 RVA: 0x0016D412 File Offset: 0x0016B612
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(byte[]) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060062BF RID: 25279 RVA: 0x0016D430 File Offset: 0x0016B630
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is byte[])
				{
					MemoryStream ms = new MemoryStream((byte[])value);
					return new AxHost.State(ms);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x060062C0 RID: 25280 RVA: 0x0016D464 File Offset: 0x0016B664
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw new ArgumentNullException("destinationType");
				}
				if (!(destinationType == typeof(byte[])))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (value != null)
				{
					MemoryStream memoryStream = new MemoryStream();
					AxHost.State state = (AxHost.State)value;
					state.Save(memoryStream);
					memoryStream.Close();
					return memoryStream.ToArray();
				}
				return new byte[0];
			}
		}

		// Token: 0x02000613 RID: 1555
		[TypeConverter(typeof(TypeConverter))]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[Serializable]
		public class State : ISerializable
		{
			// Token: 0x060062C2 RID: 25282 RVA: 0x0016D4D0 File Offset: 0x0016B6D0
			internal State(MemoryStream ms, int storageType, AxHost ctl, AxHost.PropertyBagStream propBag)
			{
				this.type = storageType;
				this.propBag = propBag;
				this.length = (int)ms.Length;
				this.ms = ms;
				this.manualUpdate = ctl.GetAxState(AxHost.manualUpdate);
				this.licenseKey = ctl.GetLicenseKey();
			}

			// Token: 0x060062C3 RID: 25283 RVA: 0x0016D52A File Offset: 0x0016B72A
			internal State(AxHost.PropertyBagStream propBag)
			{
				this.propBag = propBag;
			}

			// Token: 0x060062C4 RID: 25284 RVA: 0x0016D540 File Offset: 0x0016B740
			internal State(MemoryStream ms)
			{
				this.ms = ms;
				this.length = (int)ms.Length;
				this.InitializeFromStream(ms);
			}

			// Token: 0x060062C5 RID: 25285 RVA: 0x0016D56A File Offset: 0x0016B76A
			internal State(AxHost ctl)
			{
				this.CreateStorage();
				this.manualUpdate = ctl.GetAxState(AxHost.manualUpdate);
				this.licenseKey = ctl.GetLicenseKey();
				this.type = 2;
			}

			// Token: 0x060062C6 RID: 25286 RVA: 0x0016D5A3 File Offset: 0x0016B7A3
			public State(Stream ms, int storageType, bool manualUpdate, string licKey)
			{
				this.type = storageType;
				this.length = (int)ms.Length;
				this.manualUpdate = manualUpdate;
				this.licenseKey = licKey;
				this.InitializeBufferFromStream(ms);
			}

			// Token: 0x060062C7 RID: 25287 RVA: 0x0016D5DC File Offset: 0x0016B7DC
			protected State(SerializationInfo info, StreamingContext context)
			{
				SerializationInfoEnumerator enumerator = info.GetEnumerator();
				if (enumerator == null)
				{
					return;
				}
				while (enumerator.MoveNext())
				{
					if (string.Compare(enumerator.Name, "Data", true, CultureInfo.InvariantCulture) == 0)
					{
						try
						{
							byte[] array = (byte[])enumerator.Value;
							if (array != null)
							{
								this.InitializeFromStream(new MemoryStream(array));
							}
							continue;
						}
						catch (Exception ex)
						{
							continue;
						}
					}
					if (string.Compare(enumerator.Name, "PropertyBagBinary", true, CultureInfo.InvariantCulture) == 0)
					{
						try
						{
							byte[] array2 = (byte[])enumerator.Value;
							if (array2 != null)
							{
								this.propBag = new AxHost.PropertyBagStream();
								this.propBag.Read(new MemoryStream(array2));
							}
						}
						catch (Exception ex2)
						{
						}
					}
				}
			}

			// Token: 0x1700150F RID: 5391
			// (get) Token: 0x060062C8 RID: 25288 RVA: 0x0016D6AC File Offset: 0x0016B8AC
			// (set) Token: 0x060062C9 RID: 25289 RVA: 0x0016D6B4 File Offset: 0x0016B8B4
			internal int Type
			{
				get
				{
					return this.type;
				}
				set
				{
					this.type = value;
				}
			}

			// Token: 0x060062CA RID: 25290 RVA: 0x0016D6BD File Offset: 0x0016B8BD
			internal bool _GetManualUpdate()
			{
				return this.manualUpdate;
			}

			// Token: 0x060062CB RID: 25291 RVA: 0x0016D6C5 File Offset: 0x0016B8C5
			internal string _GetLicenseKey()
			{
				return this.licenseKey;
			}

			// Token: 0x060062CC RID: 25292 RVA: 0x0016D6D0 File Offset: 0x0016B8D0
			private void CreateStorage()
			{
				IntPtr intPtr = IntPtr.Zero;
				if (this.buffer != null)
				{
					intPtr = UnsafeNativeMethods.GlobalAlloc(2, this.length);
					IntPtr intPtr2 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
					try
					{
						if (intPtr2 != IntPtr.Zero)
						{
							Marshal.Copy(this.buffer, 0, intPtr2, this.length);
						}
					}
					finally
					{
						UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr));
					}
				}
				bool flag = false;
				try
				{
					this.iLockBytes = UnsafeNativeMethods.CreateILockBytesOnHGlobal(new HandleRef(null, intPtr), true);
					if (this.buffer == null)
					{
						this.storage = UnsafeNativeMethods.StgCreateDocfileOnILockBytes(this.iLockBytes, 4114, 0);
					}
					else
					{
						this.storage = UnsafeNativeMethods.StgOpenStorageOnILockBytes(this.iLockBytes, null, 18, 0, 0);
					}
				}
				catch (Exception ex)
				{
					flag = true;
				}
				if (flag)
				{
					if (this.iLockBytes == null && intPtr != IntPtr.Zero)
					{
						UnsafeNativeMethods.GlobalFree(new HandleRef(null, intPtr));
					}
					else
					{
						this.iLockBytes = null;
					}
					this.storage = null;
				}
			}

			// Token: 0x060062CD RID: 25293 RVA: 0x0016D7DC File Offset: 0x0016B9DC
			internal UnsafeNativeMethods.IPropertyBag GetPropBag()
			{
				return this.propBag;
			}

			// Token: 0x060062CE RID: 25294 RVA: 0x0016D7E4 File Offset: 0x0016B9E4
			internal UnsafeNativeMethods.IStorage GetStorage()
			{
				if (this.storage == null)
				{
					this.CreateStorage();
				}
				return this.storage;
			}

			// Token: 0x060062CF RID: 25295 RVA: 0x0016D7FC File Offset: 0x0016B9FC
			internal UnsafeNativeMethods.IStream GetStream()
			{
				if (this.ms == null)
				{
					if (this.buffer == null)
					{
						return null;
					}
					this.ms = new MemoryStream(this.buffer);
				}
				else
				{
					this.ms.Seek(0L, SeekOrigin.Begin);
				}
				return new UnsafeNativeMethods.ComStreamFromDataStream(this.ms);
			}

			// Token: 0x060062D0 RID: 25296 RVA: 0x0016D848 File Offset: 0x0016BA48
			private void InitializeFromStream(Stream ids)
			{
				BinaryReader binaryReader = new BinaryReader(ids);
				this.type = binaryReader.ReadInt32();
				int num = binaryReader.ReadInt32();
				this.manualUpdate = binaryReader.ReadBoolean();
				int num2 = binaryReader.ReadInt32();
				if (num2 != 0)
				{
					this.licenseKey = new string(binaryReader.ReadChars(num2));
				}
				for (int i = binaryReader.ReadInt32(); i > 0; i--)
				{
					int num3 = binaryReader.ReadInt32();
					ids.Position += (long)num3;
				}
				this.length = binaryReader.ReadInt32();
				if (this.length > 0)
				{
					this.buffer = binaryReader.ReadBytes(this.length);
				}
			}

			// Token: 0x060062D1 RID: 25297 RVA: 0x0016D8E8 File Offset: 0x0016BAE8
			private void InitializeBufferFromStream(Stream ids)
			{
				BinaryReader binaryReader = new BinaryReader(ids);
				this.length = binaryReader.ReadInt32();
				if (this.length > 0)
				{
					this.buffer = binaryReader.ReadBytes(this.length);
				}
			}

			// Token: 0x060062D2 RID: 25298 RVA: 0x0016D924 File Offset: 0x0016BB24
			internal AxHost.State RefreshStorage(UnsafeNativeMethods.IPersistStorage iPersistStorage)
			{
				if (this.storage == null || this.iLockBytes == null)
				{
					return null;
				}
				iPersistStorage.Save(this.storage, true);
				this.storage.Commit(0);
				iPersistStorage.HandsOffStorage();
				try
				{
					this.buffer = null;
					this.ms = null;
					NativeMethods.STATSTG statstg = new NativeMethods.STATSTG();
					this.iLockBytes.Stat(statstg, 1);
					this.length = (int)statstg.cbSize;
					this.buffer = new byte[this.length];
					IntPtr hglobalFromILockBytes = UnsafeNativeMethods.GetHGlobalFromILockBytes(this.iLockBytes);
					IntPtr intPtr = UnsafeNativeMethods.GlobalLock(new HandleRef(null, hglobalFromILockBytes));
					try
					{
						if (intPtr != IntPtr.Zero)
						{
							Marshal.Copy(intPtr, this.buffer, 0, this.length);
						}
						else
						{
							this.length = 0;
							this.buffer = null;
						}
					}
					finally
					{
						UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, hglobalFromILockBytes));
					}
				}
				finally
				{
					iPersistStorage.SaveCompleted(this.storage);
				}
				return this;
			}

			// Token: 0x060062D3 RID: 25299 RVA: 0x0016DA24 File Offset: 0x0016BC24
			internal void Save(MemoryStream stream)
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.Write(this.type);
				binaryWriter.Write(this.VERSION);
				binaryWriter.Write(this.manualUpdate);
				if (this.licenseKey != null)
				{
					binaryWriter.Write(this.licenseKey.Length);
					binaryWriter.Write(this.licenseKey.ToCharArray());
				}
				else
				{
					binaryWriter.Write(0);
				}
				binaryWriter.Write(0);
				binaryWriter.Write(this.length);
				if (this.buffer != null)
				{
					binaryWriter.Write(this.buffer);
					return;
				}
				if (this.ms != null)
				{
					this.ms.Position = 0L;
					this.ms.WriteTo(stream);
				}
			}

			// Token: 0x060062D4 RID: 25300 RVA: 0x0016DAD8 File Offset: 0x0016BCD8
			void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
			{
				IntSecurity.UnmanagedCode.Demand();
				MemoryStream memoryStream = new MemoryStream();
				this.Save(memoryStream);
				si.AddValue("Data", memoryStream.ToArray());
				if (this.propBag != null)
				{
					try
					{
						memoryStream = new MemoryStream();
						this.propBag.Write(memoryStream);
						si.AddValue("PropertyBagBinary", memoryStream.ToArray());
					}
					catch (Exception ex)
					{
					}
				}
			}

			// Token: 0x04003901 RID: 14593
			private int VERSION = 1;

			// Token: 0x04003902 RID: 14594
			private int length;

			// Token: 0x04003903 RID: 14595
			private byte[] buffer;

			// Token: 0x04003904 RID: 14596
			internal int type;

			// Token: 0x04003905 RID: 14597
			private MemoryStream ms;

			// Token: 0x04003906 RID: 14598
			private UnsafeNativeMethods.IStorage storage;

			// Token: 0x04003907 RID: 14599
			private UnsafeNativeMethods.ILockBytes iLockBytes;

			// Token: 0x04003908 RID: 14600
			private bool manualUpdate;

			// Token: 0x04003909 RID: 14601
			private string licenseKey;

			// Token: 0x0400390A RID: 14602
			private AxHost.PropertyBagStream propBag;
		}

		// Token: 0x02000614 RID: 1556
		internal class PropertyBagStream : UnsafeNativeMethods.IPropertyBag
		{
			// Token: 0x060062D5 RID: 25301 RVA: 0x0016DB50 File Offset: 0x0016BD50
			internal void Read(Stream stream)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				try
				{
					this.bag = (Hashtable)binaryFormatter.Deserialize(stream);
				}
				catch
				{
					this.bag = new Hashtable();
				}
			}

			// Token: 0x060062D6 RID: 25302 RVA: 0x0016DB98 File Offset: 0x0016BD98
			int UnsafeNativeMethods.IPropertyBag.Read(string pszPropName, ref object pVar, UnsafeNativeMethods.IErrorLog pErrorLog)
			{
				if (!this.bag.Contains(pszPropName))
				{
					return -2147024809;
				}
				pVar = this.bag[pszPropName];
				if (pVar != null)
				{
					return 0;
				}
				return -2147024809;
			}

			// Token: 0x060062D7 RID: 25303 RVA: 0x0016DBC7 File Offset: 0x0016BDC7
			int UnsafeNativeMethods.IPropertyBag.Write(string pszPropName, ref object pVar)
			{
				if (pVar != null && !pVar.GetType().IsSerializable)
				{
					return 0;
				}
				this.bag[pszPropName] = pVar;
				return 0;
			}

			// Token: 0x060062D8 RID: 25304 RVA: 0x0016DBEC File Offset: 0x0016BDEC
			internal void Write(Stream stream)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, this.bag);
			}

			// Token: 0x0400390B RID: 14603
			private Hashtable bag = new Hashtable();
		}

		// Token: 0x02000615 RID: 1557
		// (Invoke) Token: 0x060062DB RID: 25307
		protected delegate void AboutBoxDelegate();

		// Token: 0x02000616 RID: 1558
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class AxComponentEditor : WindowsFormsComponentEditor
		{
			// Token: 0x060062DE RID: 25310 RVA: 0x0016DC20 File Offset: 0x0016BE20
			public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
			{
				AxHost axHost = obj as AxHost;
				if (axHost != null)
				{
					try
					{
						((UnsafeNativeMethods.IOleControlSite)axHost.oleSite).ShowPropertyFrame();
						return true;
					}
					catch (Exception ex)
					{
						throw;
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x02000617 RID: 1559
		internal class AxPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x060062E0 RID: 25312 RVA: 0x0016DC60 File Offset: 0x0016BE60
			internal AxPropertyDescriptor(PropertyDescriptor baseProp, AxHost owner) : base(baseProp)
			{
				this.baseProp = baseProp;
				this.owner = owner;
				this.dispid = (DispIdAttribute)baseProp.Attributes[typeof(DispIdAttribute)];
				if (this.dispid != null)
				{
					if (!this.IsBrowsable && !this.IsReadOnly)
					{
						Guid propertyPage = this.GetPropertyPage(this.dispid.Value);
						if (!Guid.Empty.Equals(propertyPage))
						{
							this.AddAttribute(new BrowsableAttribute(true));
						}
					}
					CategoryAttribute categoryForDispid = owner.GetCategoryForDispid(this.dispid.Value);
					if (categoryForDispid != null)
					{
						this.AddAttribute(categoryForDispid);
					}
					if (this.PropertyType.GUID.Equals(AxHost.dataSource_Guid))
					{
						this.SetFlag(8, true);
					}
				}
			}

			// Token: 0x17001510 RID: 5392
			// (get) Token: 0x060062E1 RID: 25313 RVA: 0x0016DD31 File Offset: 0x0016BF31
			public override Type ComponentType
			{
				get
				{
					return this.baseProp.ComponentType;
				}
			}

			// Token: 0x17001511 RID: 5393
			// (get) Token: 0x060062E2 RID: 25314 RVA: 0x0016DD3E File Offset: 0x0016BF3E
			public override TypeConverter Converter
			{
				get
				{
					if (this.dispid != null)
					{
						this.UpdateTypeConverterAndTypeEditorInternal(false, this.Dispid);
					}
					if (this.converter == null)
					{
						return base.Converter;
					}
					return this.converter;
				}
			}

			// Token: 0x17001512 RID: 5394
			// (get) Token: 0x060062E3 RID: 25315 RVA: 0x0016DD6C File Offset: 0x0016BF6C
			internal int Dispid
			{
				get
				{
					DispIdAttribute dispIdAttribute = (DispIdAttribute)this.baseProp.Attributes[typeof(DispIdAttribute)];
					if (dispIdAttribute != null)
					{
						return dispIdAttribute.Value;
					}
					return -1;
				}
			}

			// Token: 0x17001513 RID: 5395
			// (get) Token: 0x060062E4 RID: 25316 RVA: 0x0016DDA4 File Offset: 0x0016BFA4
			public override bool IsReadOnly
			{
				get
				{
					return this.baseProp.IsReadOnly;
				}
			}

			// Token: 0x17001514 RID: 5396
			// (get) Token: 0x060062E5 RID: 25317 RVA: 0x0016DDB1 File Offset: 0x0016BFB1
			public override Type PropertyType
			{
				get
				{
					return this.baseProp.PropertyType;
				}
			}

			// Token: 0x17001515 RID: 5397
			// (get) Token: 0x060062E6 RID: 25318 RVA: 0x0016DDBE File Offset: 0x0016BFBE
			internal bool SettingValue
			{
				get
				{
					return this.GetFlag(16);
				}
			}

			// Token: 0x060062E7 RID: 25319 RVA: 0x0016DDC8 File Offset: 0x0016BFC8
			private void AddAttribute(Attribute attr)
			{
				this.updateAttrs.Add(attr);
			}

			// Token: 0x060062E8 RID: 25320 RVA: 0x0016DDD7 File Offset: 0x0016BFD7
			public override bool CanResetValue(object o)
			{
				return this.baseProp.CanResetValue(o);
			}

			// Token: 0x060062E9 RID: 25321 RVA: 0x0016DDE5 File Offset: 0x0016BFE5
			public override object GetEditor(Type editorBaseType)
			{
				this.UpdateTypeConverterAndTypeEditorInternal(false, this.dispid.Value);
				if (editorBaseType.Equals(typeof(UITypeEditor)) && this.editor != null)
				{
					return this.editor;
				}
				return base.GetEditor(editorBaseType);
			}

			// Token: 0x060062EA RID: 25322 RVA: 0x0016DE21 File Offset: 0x0016C021
			private bool GetFlag(int flagValue)
			{
				return (this.flags & flagValue) == flagValue;
			}

			// Token: 0x060062EB RID: 25323 RVA: 0x0016DE30 File Offset: 0x0016C030
			private Guid GetPropertyPage(int dispid)
			{
				try
				{
					NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = this.owner.GetPerPropertyBrowsing();
					if (perPropertyBrowsing == null)
					{
						return Guid.Empty;
					}
					Guid result;
					if (NativeMethods.Succeeded(perPropertyBrowsing.MapPropertyToPage(dispid, out result)))
					{
						return result;
					}
				}
				catch (COMException)
				{
				}
				catch (Exception ex)
				{
				}
				return Guid.Empty;
			}

			// Token: 0x060062EC RID: 25324 RVA: 0x0016DE94 File Offset: 0x0016C094
			public override object GetValue(object component)
			{
				if ((!this.GetFlag(8) && !this.owner.CanAccessProperties) || this.GetFlag(4))
				{
					return null;
				}
				object value;
				try
				{
					AxHost axHost = this.owner;
					int noComponentChangeEvents = axHost.NoComponentChangeEvents;
					axHost.NoComponentChangeEvents = noComponentChangeEvents + 1;
					value = this.baseProp.GetValue(component);
				}
				catch (Exception ex)
				{
					if (!this.GetFlag(2))
					{
						this.SetFlag(2, true);
						this.AddAttribute(new BrowsableAttribute(false));
						this.owner.RefreshAllProperties = true;
						this.SetFlag(4, true);
					}
					throw ex;
				}
				finally
				{
					AxHost axHost2 = this.owner;
					int noComponentChangeEvents = axHost2.NoComponentChangeEvents;
					axHost2.NoComponentChangeEvents = noComponentChangeEvents - 1;
				}
				return value;
			}

			// Token: 0x060062ED RID: 25325 RVA: 0x0016DF50 File Offset: 0x0016C150
			public void OnValueChanged(object component)
			{
				this.OnValueChanged(component, EventArgs.Empty);
			}

			// Token: 0x060062EE RID: 25326 RVA: 0x0016DF5E File Offset: 0x0016C15E
			public override void ResetValue(object o)
			{
				this.baseProp.ResetValue(o);
			}

			// Token: 0x060062EF RID: 25327 RVA: 0x0016DF6C File Offset: 0x0016C16C
			private void SetFlag(int flagValue, bool value)
			{
				if (value)
				{
					this.flags |= flagValue;
					return;
				}
				this.flags &= ~flagValue;
			}

			// Token: 0x060062F0 RID: 25328 RVA: 0x0016DF90 File Offset: 0x0016C190
			public override void SetValue(object component, object value)
			{
				if (!this.GetFlag(8) && !this.owner.CanAccessProperties)
				{
					return;
				}
				try
				{
					this.SetFlag(16, true);
					if (this.PropertyType.IsEnum && value.GetType() != this.PropertyType)
					{
						this.baseProp.SetValue(component, Enum.ToObject(this.PropertyType, value));
					}
					else
					{
						this.baseProp.SetValue(component, value);
					}
				}
				finally
				{
					this.SetFlag(16, false);
				}
				this.OnValueChanged(component);
				if (this.owner == component)
				{
					this.owner.SetAxState(AxHost.valueChanged, true);
				}
			}

			// Token: 0x060062F1 RID: 25329 RVA: 0x0016E044 File Offset: 0x0016C244
			public override bool ShouldSerializeValue(object o)
			{
				return this.baseProp.ShouldSerializeValue(o);
			}

			// Token: 0x060062F2 RID: 25330 RVA: 0x0016E054 File Offset: 0x0016C254
			internal void UpdateAttributes()
			{
				if (this.updateAttrs.Count == 0)
				{
					return;
				}
				ArrayList arrayList = new ArrayList(this.AttributeArray);
				foreach (object obj in this.updateAttrs)
				{
					Attribute value = (Attribute)obj;
					arrayList.Add(value);
				}
				Attribute[] array = new Attribute[arrayList.Count];
				arrayList.CopyTo(array, 0);
				this.AttributeArray = array;
				this.updateAttrs.Clear();
			}

			// Token: 0x060062F3 RID: 25331 RVA: 0x0016E0F4 File Offset: 0x0016C2F4
			internal void UpdateTypeConverterAndTypeEditor(bool force)
			{
				if (this.GetFlag(1) && force)
				{
					this.SetFlag(1, false);
				}
			}

			// Token: 0x060062F4 RID: 25332 RVA: 0x0016E10C File Offset: 0x0016C30C
			internal void UpdateTypeConverterAndTypeEditorInternal(bool force, int dispid)
			{
				if (this.GetFlag(1) && !force)
				{
					return;
				}
				if (this.owner.GetOcx() == null)
				{
					return;
				}
				try
				{
					NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = this.owner.GetPerPropertyBrowsing();
					if (perPropertyBrowsing != null)
					{
						NativeMethods.CA_STRUCT ca_STRUCT = new NativeMethods.CA_STRUCT();
						NativeMethods.CA_STRUCT ca_STRUCT2 = new NativeMethods.CA_STRUCT();
						int num = 0;
						try
						{
							num = perPropertyBrowsing.GetPredefinedStrings(dispid, ca_STRUCT, ca_STRUCT2);
						}
						catch (ExternalException ex)
						{
							num = ex.ErrorCode;
						}
						bool flag;
						if (num != 0)
						{
							flag = false;
							if (this.converter is Com2EnumConverter)
							{
								this.converter = null;
							}
						}
						else
						{
							flag = true;
						}
						if (flag)
						{
							OleStrCAMarshaler oleStrCAMarshaler = new OleStrCAMarshaler(ca_STRUCT);
							Int32CAMarshaler int32CAMarshaler = new Int32CAMarshaler(ca_STRUCT2);
							if (oleStrCAMarshaler.Count > 0 && int32CAMarshaler.Count > 0)
							{
								if (this.converter == null)
								{
									this.converter = new AxHost.AxEnumConverter(this, new AxHost.AxPerPropertyBrowsingEnum(this, this.owner, oleStrCAMarshaler, int32CAMarshaler, true));
								}
								else if (this.converter is AxHost.AxEnumConverter)
								{
									((AxHost.AxEnumConverter)this.converter).RefreshValues();
									AxHost.AxPerPropertyBrowsingEnum axPerPropertyBrowsingEnum = ((AxHost.AxEnumConverter)this.converter).com2Enum as AxHost.AxPerPropertyBrowsingEnum;
									if (axPerPropertyBrowsingEnum != null)
									{
										axPerPropertyBrowsingEnum.RefreshArrays(oleStrCAMarshaler, int32CAMarshaler);
									}
								}
							}
						}
						else if ((ComAliasNameAttribute)this.baseProp.Attributes[typeof(ComAliasNameAttribute)] == null)
						{
							Guid propertyPage = this.GetPropertyPage(dispid);
							if (!Guid.Empty.Equals(propertyPage))
							{
								this.editor = new AxHost.AxPropertyTypeEditor(this, propertyPage);
								if (!this.IsBrowsable)
								{
									this.AddAttribute(new BrowsableAttribute(true));
								}
							}
						}
					}
					this.SetFlag(1, true);
				}
				catch (Exception ex2)
				{
				}
			}

			// Token: 0x0400390C RID: 14604
			private PropertyDescriptor baseProp;

			// Token: 0x0400390D RID: 14605
			internal AxHost owner;

			// Token: 0x0400390E RID: 14606
			private DispIdAttribute dispid;

			// Token: 0x0400390F RID: 14607
			private TypeConverter converter;

			// Token: 0x04003910 RID: 14608
			private UITypeEditor editor;

			// Token: 0x04003911 RID: 14609
			private ArrayList updateAttrs = new ArrayList();

			// Token: 0x04003912 RID: 14610
			private int flags;

			// Token: 0x04003913 RID: 14611
			private const int FlagUpdatedEditorAndConverter = 1;

			// Token: 0x04003914 RID: 14612
			private const int FlagCheckGetter = 2;

			// Token: 0x04003915 RID: 14613
			private const int FlagGettterThrew = 4;

			// Token: 0x04003916 RID: 14614
			private const int FlagIgnoreCanAccessProperties = 8;

			// Token: 0x04003917 RID: 14615
			private const int FlagSettingValue = 16;
		}

		// Token: 0x02000618 RID: 1560
		private class AxPropertyTypeEditor : UITypeEditor
		{
			// Token: 0x060062F5 RID: 25333 RVA: 0x0016E2D8 File Offset: 0x0016C4D8
			public AxPropertyTypeEditor(AxHost.AxPropertyDescriptor pd, Guid guid)
			{
				this.propDesc = pd;
				this.guid = guid;
			}

			// Token: 0x060062F6 RID: 25334 RVA: 0x0016E2F0 File Offset: 0x0016C4F0
			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
			{
				try
				{
					object instance = context.Instance;
					this.propDesc.owner.ShowPropertyPageForDispid(this.propDesc.Dispid, this.guid);
				}
				catch (Exception ex)
				{
					if (provider != null)
					{
						IUIService iuiservice = (IUIService)provider.GetService(typeof(IUIService));
						if (iuiservice != null)
						{
							iuiservice.ShowError(ex, SR.GetString("ErrorTypeConverterFailed"));
						}
					}
				}
				return value;
			}

			// Token: 0x060062F7 RID: 25335 RVA: 0x0001627D File Offset: 0x0001447D
			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.Modal;
			}

			// Token: 0x04003918 RID: 14616
			private AxHost.AxPropertyDescriptor propDesc;

			// Token: 0x04003919 RID: 14617
			private Guid guid;
		}

		// Token: 0x02000619 RID: 1561
		private class AxEnumConverter : Com2EnumConverter
		{
			// Token: 0x060062F8 RID: 25336 RVA: 0x0016E368 File Offset: 0x0016C568
			public AxEnumConverter(AxHost.AxPropertyDescriptor target, Com2Enum com2Enum) : base(com2Enum)
			{
				this.target = target;
			}

			// Token: 0x060062F9 RID: 25337 RVA: 0x0016E378 File Offset: 0x0016C578
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter converter = this.target.Converter;
				return base.GetStandardValues(context);
			}

			// Token: 0x0400391A RID: 14618
			private AxHost.AxPropertyDescriptor target;
		}

		// Token: 0x0200061A RID: 1562
		private class AxPerPropertyBrowsingEnum : Com2Enum
		{
			// Token: 0x060062FA RID: 25338 RVA: 0x0016E39A File Offset: 0x0016C59A
			public AxPerPropertyBrowsingEnum(AxHost.AxPropertyDescriptor targetObject, AxHost owner, OleStrCAMarshaler names, Int32CAMarshaler values, bool allowUnknowns) : base(new string[0], new object[0], allowUnknowns)
			{
				this.target = targetObject;
				this.nameMarshaller = names;
				this.valueMarshaller = values;
				this.owner = owner;
				this.arraysFetched = false;
			}

			// Token: 0x17001516 RID: 5398
			// (get) Token: 0x060062FB RID: 25339 RVA: 0x0016E3D4 File Offset: 0x0016C5D4
			public override object[] Values
			{
				get
				{
					this.EnsureArrays();
					return base.Values;
				}
			}

			// Token: 0x17001517 RID: 5399
			// (get) Token: 0x060062FC RID: 25340 RVA: 0x0016E3E2 File Offset: 0x0016C5E2
			public override string[] Names
			{
				get
				{
					this.EnsureArrays();
					return base.Names;
				}
			}

			// Token: 0x060062FD RID: 25341 RVA: 0x0016E3F0 File Offset: 0x0016C5F0
			private void EnsureArrays()
			{
				if (this.arraysFetched)
				{
					return;
				}
				this.arraysFetched = true;
				try
				{
					object[] items = this.nameMarshaller.Items;
					object[] items2 = this.valueMarshaller.Items;
					NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = this.owner.GetPerPropertyBrowsing();
					int num = 0;
					if (items.Length != 0)
					{
						object[] array = new object[items2.Length];
						NativeMethods.VARIANT variant = new NativeMethods.VARIANT();
						for (int i = 0; i < items.Length; i++)
						{
							int dwCookie = (int)items2[i];
							if (items[i] != null && items[i] is string)
							{
								variant.vt = 0;
								if (perPropertyBrowsing.GetPredefinedValue(this.target.Dispid, dwCookie, variant) == 0 && variant.vt != 0)
								{
									array[i] = variant.ToObject();
								}
								variant.Clear();
								num++;
							}
						}
						if (num > 0)
						{
							string[] array2 = new string[num];
							Array.Copy(items, 0, array2, 0, num);
							base.PopulateArrays(array2, array);
						}
					}
				}
				catch (Exception ex)
				{
				}
			}

			// Token: 0x060062FE RID: 25342 RVA: 0x0016E4F4 File Offset: 0x0016C6F4
			internal void RefreshArrays(OleStrCAMarshaler names, Int32CAMarshaler values)
			{
				this.nameMarshaller = names;
				this.valueMarshaller = values;
				this.arraysFetched = false;
			}

			// Token: 0x060062FF RID: 25343 RVA: 0x000072B6 File Offset: 0x000054B6
			protected override void PopulateArrays(string[] names, object[] values)
			{
			}

			// Token: 0x06006300 RID: 25344 RVA: 0x0016E50B File Offset: 0x0016C70B
			public override object FromString(string s)
			{
				this.EnsureArrays();
				return base.FromString(s);
			}

			// Token: 0x06006301 RID: 25345 RVA: 0x0016E51A File Offset: 0x0016C71A
			public override string ToString(object v)
			{
				this.EnsureArrays();
				return base.ToString(v);
			}

			// Token: 0x0400391B RID: 14619
			private AxHost.AxPropertyDescriptor target;

			// Token: 0x0400391C RID: 14620
			private AxHost owner;

			// Token: 0x0400391D RID: 14621
			private OleStrCAMarshaler nameMarshaller;

			// Token: 0x0400391E RID: 14622
			private Int32CAMarshaler valueMarshaller;

			// Token: 0x0400391F RID: 14623
			private bool arraysFetched;
		}
	}
}
