using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000143 RID: 323
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionButton")]
	[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Button : ButtonBase, IButtonControl
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x000236C4 File Offset: 0x000218C4
		public Button()
		{
			base.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, false);
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x000236ED File Offset: 0x000218ED
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x000236F8 File Offset: 0x000218F8
		[SRCategory("CatLayout")]
		[Browsable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[Localizable(true)]
		[SRDescription("ControlAutoSizeModeDescr")]
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
					if (this.ParentInternal != null)
					{
						if (this.ParentInternal.LayoutEngine == DefaultLayout.Instance)
						{
							this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.AutoSize);
					}
				}
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00023779 File Offset: 0x00021979
		internal override ButtonBaseAdapter CreateFlatAdapter()
		{
			return new ButtonFlatAdapter(this);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00023781 File Offset: 0x00021981
		internal override ButtonBaseAdapter CreatePopupAdapter()
		{
			return new ButtonPopupAdapter(this);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00023789 File Offset: 0x00021989
		internal override ButtonBaseAdapter CreateStandardAdapter()
		{
			return new ButtonStandardAdapter(this);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00023794 File Offset: 0x00021994
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			if (base.FlatStyle != FlatStyle.System)
			{
				Size preferredSizeCore = base.GetPreferredSizeCore(proposedConstraints);
				if (this.AutoSizeMode != AutoSizeMode.GrowAndShrink)
				{
					return LayoutUtils.UnionSizes(preferredSizeCore, base.Size);
				}
				return preferredSizeCore;
			}
			else
			{
				if (this.systemSize.Width == -2147483648)
				{
					Size clientSize = TextRenderer.MeasureText(this.Text, this.Font);
					clientSize = this.SizeFromClientSize(clientSize);
					clientSize.Width += 14;
					clientSize.Height += 9;
					this.systemSize = clientSize;
				}
				Size size = this.systemSize + base.Padding.Size;
				if (this.AutoSizeMode != AutoSizeMode.GrowAndShrink)
				{
					return LayoutUtils.UnionSizes(size, base.Size);
				}
				return size;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002384C File Offset: 0x00021A4C
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "BUTTON";
				if (base.GetStyle(ControlStyles.UserPaint))
				{
					createParams.Style |= 11;
				}
				else
				{
					createParams.Style |= 0;
					if (base.IsDefault)
					{
						createParams.Style |= 1;
					}
				}
				return createParams;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x000238AA File Offset: 0x00021AAA
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x000238B2 File Offset: 0x00021AB2
		[SRCategory("CatBehavior")]
		[DefaultValue(DialogResult.None)]
		[SRDescription("ButtonDialogResultDescr")]
		public virtual DialogResult DialogResult
		{
			get
			{
				return this.dialogResult;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DialogResult));
				}
				this.dialogResult = value;
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x000238E1 File Offset: 0x00021AE1
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000238EA File Offset: 0x00021AEA
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06000C5C RID: 3164 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x06000C5D RID: 3165 RVA: 0x000238FC File Offset: 0x00021AFC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06000C5E RID: 3166 RVA: 0x00023905 File Offset: 0x00021B05
		// (remove) Token: 0x06000C5F RID: 3167 RVA: 0x0002390E File Offset: 0x00021B0E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.MouseDoubleClick += value;
			}
			remove
			{
				base.MouseDoubleClick -= value;
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00023917 File Offset: 0x00021B17
		public virtual void NotifyDefault(bool value)
		{
			if (base.IsDefault != value)
			{
				base.IsDefault = value;
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002392C File Offset: 0x00021B2C
		protected override void OnClick(EventArgs e)
		{
			Form form = base.FindFormInternal();
			if (form != null)
			{
				form.DialogResult = this.dialogResult;
			}
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
			base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
			base.OnClick(e);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002396E File Offset: 0x00021B6E
		protected override void OnFontChanged(EventArgs e)
		{
			this.systemSize = new Size(int.MinValue, int.MinValue);
			base.OnFontChanged(e);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002398C File Offset: 0x00021B8C
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left && base.MouseIsPressed)
			{
				bool mouseIsDown = base.MouseIsDown;
				if (base.GetStyle(ControlStyles.UserPaint))
				{
					base.ResetFlagsandPaint();
				}
				if (mouseIsDown)
				{
					Point point = base.PointToScreen(new Point(mevent.X, mevent.Y));
					if (UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle && !base.ValidationCancelled)
					{
						if (base.GetStyle(ControlStyles.UserPaint))
						{
							this.OnClick(mevent);
						}
						this.OnMouseClick(mevent);
					}
				}
			}
			base.OnMouseUp(mevent);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00023A25 File Offset: 0x00021C25
		protected override void OnTextChanged(EventArgs e)
		{
			this.systemSize = new Size(int.MinValue, int.MinValue);
			base.OnTextChanged(e);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00023A43 File Offset: 0x00021C43
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.systemSize = new Size(int.MinValue, int.MinValue);
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00023A6C File Offset: 0x00021C6C
		public void PerformClick()
		{
			if (base.CanSelect)
			{
				bool flag2;
				bool flag = base.ValidateActiveControl(out flag2);
				if (!base.ValidationCancelled && (flag || flag2))
				{
					base.ResetFlagsandPaint();
					this.OnClick(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00023AA8 File Offset: 0x00021CA8
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.UseMnemonic && this.CanProcessMnemonic() && Control.IsMnemonic(charCode, this.Text))
			{
				this.PerformClick();
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00023AD8 File Offset: 0x00021CD8
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", Text: " + this.Text;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00023B00 File Offset: 0x00021D00
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 20)
			{
				if (msg == 8465)
				{
					if (NativeMethods.Util.HIWORD(m.WParam) == 0 && !base.ValidationCancelled)
					{
						this.OnClick(EventArgs.Empty);
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
				}
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0400072F RID: 1839
		private DialogResult dialogResult;

		// Token: 0x04000730 RID: 1840
		private const int InvalidDimensionValue = -2147483648;

		// Token: 0x04000731 RID: 1841
		private Size systemSize = new Size(int.MinValue, int.MinValue);
	}
}
