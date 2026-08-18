using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200042C RID: 1068
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.UserControlDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignerCategory("UserControl")]
	[DefaultEvent("Load")]
	public class UserControl : ContainerControl
	{
		// Token: 0x060049FC RID: 18940 RVA: 0x001375E6 File Offset: 0x001357E6
		public UserControl()
		{
			base.SetScrollState(1, false);
			base.SetState(2, true);
			base.SetState(524288, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x060049FD RID: 18941 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x060049FE RID: 18942 RVA: 0x00011A4D File Offset: 0x0000FC4D
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

		// Token: 0x140003B5 RID: 949
		// (add) Token: 0x060049FF RID: 18943 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x06004A00 RID: 18944 RVA: 0x00011A5F File Offset: 0x0000FC5F
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

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06004A01 RID: 18945 RVA: 0x000236ED File Offset: 0x000218ED
		// (set) Token: 0x06004A02 RID: 18946 RVA: 0x00137618 File Offset: 0x00135818
		[SRDescription("ControlAutoSizeModeDescr")]
		[SRCategory("CatLayout")]
		[Browsable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[Localizable(true)]
		public AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoSizeMode));
				}
				if (base.GetAutoSizeMode() != value)
				{
					base.SetAutoSizeMode(value);
					Control control = (base.DesignMode || this.ParentInternal == null) ? this : this.ParentInternal;
					if (control != null)
					{
						if (control.LayoutEngine == DefaultLayout.Instance)
						{
							control.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(control, this, PropertyNames.AutoSize);
					}
				}
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06004A03 RID: 18947 RVA: 0x000B0DDB File Offset: 0x000AEFDB
		// (set) Token: 0x06004A04 RID: 18948 RVA: 0x000B0DE3 File Offset: 0x000AEFE3
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		// Token: 0x140003B6 RID: 950
		// (add) Token: 0x06004A05 RID: 18949 RVA: 0x000B0DEC File Offset: 0x000AEFEC
		// (remove) Token: 0x06004A06 RID: 18950 RVA: 0x000B0DF5 File Offset: 0x000AEFF5
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoValidateChanged
		{
			add
			{
				base.AutoValidateChanged += value;
			}
			remove
			{
				base.AutoValidateChanged -= value;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06004A07 RID: 18951 RVA: 0x0013769F File Offset: 0x0013589F
		// (set) Token: 0x06004A08 RID: 18952 RVA: 0x001376A7 File Offset: 0x001358A7
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.None)]
		[SRDescription("UserControlBorderStyleDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
					}
					this.borderStyle = value;
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06004A09 RID: 18953 RVA: 0x001376E8 File Offset: 0x001358E8
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 65536;
				createParams.ExStyle &= -513;
				createParams.Style &= -8388609;
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
				return createParams;
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06004A0A RID: 18954 RVA: 0x00137768 File Offset: 0x00135968
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 150);
			}
		}

		// Token: 0x140003B7 RID: 951
		// (add) Token: 0x06004A0B RID: 18955 RVA: 0x00137779 File Offset: 0x00135979
		// (remove) Token: 0x06004A0C RID: 18956 RVA: 0x0013778C File Offset: 0x0013598C
		[SRCategory("CatBehavior")]
		[SRDescription("UserControlOnLoadDescr")]
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(UserControl.EVENT_LOAD, value);
			}
			remove
			{
				base.Events.RemoveHandler(UserControl.EVENT_LOAD, value);
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06004A0E RID: 18958 RVA: 0x00024185 File Offset: 0x00022385
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x140003B8 RID: 952
		// (add) Token: 0x06004A0F RID: 18959 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06004A10 RID: 18960 RVA: 0x0004677A File Offset: 0x0004497A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x000B744F File Offset: 0x000B564F
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren()
		{
			return base.ValidateChildren();
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x000B7457 File Offset: 0x000B5657
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			return base.ValidateChildren(validationConstraints);
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x001377A0 File Offset: 0x001359A0
		private bool FocusInside()
		{
			if (!base.IsHandleCreated)
			{
				return false;
			}
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			if (focus == IntPtr.Zero)
			{
				return false;
			}
			IntPtr handle = base.Handle;
			return handle == focus || SafeNativeMethods.IsChild(new HandleRef(this, handle), new HandleRef(null, focus));
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x001377F4 File Offset: 0x001359F4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.OnLoad(EventArgs.Empty);
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x00137808 File Offset: 0x00135A08
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLoad(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[UserControl.EVENT_LOAD];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x00137836 File Offset: 0x00135A36
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.BackgroundImage != null)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x0013784D File Offset: 0x00135A4D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!this.FocusInside())
			{
				this.FocusInternal();
			}
			base.OnMouseDown(e);
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x00137868 File Offset: 0x00135A68
		private void WmSetFocus(ref Message m)
		{
			if (!base.HostedInWin32DialogManager)
			{
				IntSecurity.ModifyFocus.Assert();
				try
				{
					if (base.ActiveControl == null)
					{
						base.SelectNextControl(null, true, true, true, false);
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			if (!base.ValidationCancelled)
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x001378C4 File Offset: 0x00135AC4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 7)
			{
				this.WmSetFocus(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x040027C9 RID: 10185
		private static readonly object EVENT_LOAD = new object();

		// Token: 0x040027CA RID: 10186
		private BorderStyle borderStyle;
	}
}
