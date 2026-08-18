using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000167 RID: 359
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ContainerControl : ScrollableControl, IContainerControl
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x0002D090 File Offset: 0x0002B290
		public ContainerControl()
		{
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
			base.SetState2(2048, true);
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0002D0DF File Offset: 0x0002B2DF
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x0002D0E8 File Offset: 0x0002B2E8
		[Localizable(true)]
		[Browsable(false)]
		[SRCategory("CatLayout")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SizeF AutoScaleDimensions
		{
			get
			{
				return this.autoScaleDimensions;
			}
			set
			{
				if (value.Width < 0f || value.Height < 0f)
				{
					throw new ArgumentOutOfRangeException(SR.GetString("ContainerControlInvalidAutoScaleDimensions"), "value");
				}
				this.autoScaleDimensions = value;
				if (!this.autoScaleDimensions.IsEmpty)
				{
					this.LayoutScalingNeeded();
				}
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0002D140 File Offset: 0x0002B340
		protected SizeF AutoScaleFactor
		{
			get
			{
				SizeF sizeF = this.CurrentAutoScaleDimensions;
				SizeF sizeF2 = this.AutoScaleDimensions;
				if (sizeF2.IsEmpty)
				{
					return new SizeF(1f, 1f);
				}
				return new SizeF(sizeF.Width / sizeF2.Width, sizeF.Height / sizeF2.Height);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0002D197 File Offset: 0x0002B397
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0002D1A0 File Offset: 0x0002B3A0
		[SRCategory("CatLayout")]
		[SRDescription("ContainerControlAutoScaleModeDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AutoScaleMode AutoScaleMode
		{
			get
			{
				return this.autoScaleMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoScaleMode));
				}
				bool flag = false;
				if (value != this.autoScaleMode)
				{
					if (this.autoScaleMode != AutoScaleMode.Inherit)
					{
						this.autoScaleDimensions = SizeF.Empty;
					}
					this.currentAutoScaleDimensions = SizeF.Empty;
					this.autoScaleMode = value;
					flag = true;
				}
				this.OnAutoScaleModeChanged();
				if (flag)
				{
					this.LayoutScalingNeeded();
				}
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0002D215 File Offset: 0x0002B415
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x0002D22D File Offset: 0x0002B42D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[AmbientValue(AutoValidate.Inherit)]
		[SRCategory("CatBehavior")]
		[SRDescription("ContainerControlAutoValidate")]
		public virtual AutoValidate AutoValidate
		{
			get
			{
				if (this.autoValidate == AutoValidate.Inherit)
				{
					return Control.GetAutoValidateForControl(this);
				}
				return this.autoValidate;
			}
			set
			{
				if (value - AutoValidate.Inherit > 3)
				{
					throw new InvalidEnumArgumentException("AutoValidate", (int)value, typeof(AutoValidate));
				}
				if (this.autoValidate != value)
				{
					this.autoValidate = value;
					this.OnAutoValidateChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x06000EF7 RID: 3831 RVA: 0x0002D266 File Offset: 0x0002B466
		// (remove) Token: 0x06000EF8 RID: 3832 RVA: 0x0002D27F File Offset: 0x0002B47F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ContainerControlOnAutoValidateChangedDescr")]
		public event EventHandler AutoValidateChanged
		{
			add
			{
				this.autoValidateChanged = (EventHandler)Delegate.Combine(this.autoValidateChanged, value);
			}
			remove
			{
				this.autoValidateChanged = (EventHandler)Delegate.Remove(this.autoValidateChanged, value);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0002D298 File Offset: 0x0002B498
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x0002D2BD File Offset: 0x0002B4BD
		[Browsable(false)]
		[SRDescription("ContainerControlBindingContextDescr")]
		public override BindingContext BindingContext
		{
			get
			{
				BindingContext bindingContext = base.BindingContext;
				if (bindingContext == null)
				{
					bindingContext = new BindingContext();
					this.BindingContext = bindingContext;
				}
				return bindingContext;
			}
			set
			{
				base.BindingContext = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected override bool CanEnableIme
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0002D2C6 File Offset: 0x0002B4C6
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0002D2CE File Offset: 0x0002B4CE
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ContainerControlActiveControlDescr")]
		public Control ActiveControl
		{
			get
			{
				return this.activeControl;
			}
			set
			{
				this.SetActiveControl(value);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0002D2D8 File Offset: 0x0002B4D8
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 65536;
				return createParams;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0002D300 File Offset: 0x0002B500
		[Browsable(false)]
		[SRCategory("CatLayout")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SizeF CurrentAutoScaleDimensions
		{
			get
			{
				if (this.currentAutoScaleDimensions.IsEmpty)
				{
					AutoScaleMode autoScaleMode = this.AutoScaleMode;
					if (autoScaleMode != AutoScaleMode.Font)
					{
						if (autoScaleMode != AutoScaleMode.Dpi)
						{
							this.currentAutoScaleDimensions = this.AutoScaleDimensions;
						}
						else if (DpiHelper.EnableDpiChangedMessageHandling)
						{
							this.currentAutoScaleDimensions = new SizeF((float)this.deviceDpi, (float)this.deviceDpi);
						}
						else
						{
							this.currentAutoScaleDimensions = WindowsGraphicsCacheManager.MeasurementGraphics.DeviceContext.Dpi;
						}
					}
					else
					{
						this.currentAutoScaleDimensions = this.GetFontAutoScaleDimensions();
					}
				}
				return this.currentAutoScaleDimensions;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0002D389 File Offset: 0x0002B589
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ContainerControlParentFormDescr")]
		public Form ParentForm
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.ParentFormInternal;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0002D39B File Offset: 0x0002B59B
		internal Form ParentFormInternal
		{
			get
			{
				if (this.ParentInternal != null)
				{
					return this.ParentInternal.FindFormInternal();
				}
				if (this is Form)
				{
					return null;
				}
				return base.FindFormInternal();
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002D3C1 File Offset: 0x0002B5C1
		bool IContainerControl.ActivateControl(Control control)
		{
			IntSecurity.ModifyFocus.Demand();
			return this.ActivateControlInternal(control, true);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002D3D5 File Offset: 0x0002B5D5
		internal bool ActivateControlInternal(Control control)
		{
			return this.ActivateControlInternal(control, true);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002D3E0 File Offset: 0x0002B5E0
		internal bool ActivateControlInternal(Control control, bool originator)
		{
			bool result = true;
			bool flag = false;
			ContainerControl containerControl = null;
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				containerControl = (parentInternal.GetContainerControlInternal() as ContainerControl);
				if (containerControl != null)
				{
					flag = (containerControl.ActiveControl != this);
				}
			}
			if (control != this.activeControl || flag)
			{
				if (flag && !containerControl.ActivateControlInternal(this, false))
				{
					return false;
				}
				result = this.AssignActiveControlInternal((control == this) ? null : control);
			}
			if (originator)
			{
				this.ScrollActiveControlIntoView();
			}
			return result;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0002D454 File Offset: 0x0002B654
		internal bool HasFocusableChild()
		{
			Control control = null;
			do
			{
				control = base.GetNextControl(control, true);
			}
			while ((control == null || !control.CanSelect || !control.TabStop) && control != null);
			return control != null;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0002D486 File Offset: 0x0002B686
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			base.AdjustFormScrollbars(displayScrollbars);
			if (!base.GetScrollState(8))
			{
				this.ScrollActiveControlIntoView();
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002D4A0 File Offset: 0x0002B6A0
		internal virtual void AfterControlRemoved(Control control, Control oldParent)
		{
			ContainerControl containerControl;
			if (control == this.activeControl || control.Contains(this.activeControl))
			{
				IntSecurity.ModifyFocus.Assert();
				bool flag;
				try
				{
					flag = base.SelectNextControl(control, true, true, true, true);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (flag && this.activeControl != control)
				{
					if (!this.activeControl.Parent.IsTopMdiWindowClosing)
					{
						this.FocusActiveControlInternal();
					}
				}
				else
				{
					this.SetActiveControlInternal(null);
				}
			}
			else if (this.activeControl == null && this.ParentInternal != null)
			{
				containerControl = (this.ParentInternal.GetContainerControlInternal() as ContainerControl);
				if (containerControl != null && containerControl.ActiveControl == this)
				{
					Form form = base.FindFormInternal();
					if (form != null)
					{
						IntSecurity.ModifyFocus.Assert();
						try
						{
							form.SelectNextControl(this, true, true, true, true);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
			}
			containerControl = this;
			while (containerControl != null)
			{
				Control parentInternal = containerControl.ParentInternal;
				if (parentInternal == null)
				{
					break;
				}
				containerControl = (parentInternal.GetContainerControlInternal() as ContainerControl);
				if (containerControl != null && containerControl.unvalidatedControl != null && (containerControl.unvalidatedControl == control || control.Contains(containerControl.unvalidatedControl)))
				{
					containerControl.unvalidatedControl = oldParent;
				}
			}
			if (control == this.unvalidatedControl || control.Contains(this.unvalidatedControl))
			{
				this.unvalidatedControl = null;
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0002D5E8 File Offset: 0x0002B7E8
		private bool AssignActiveControlInternal(Control value)
		{
			if (this.activeControl != value)
			{
				try
				{
					if (value != null)
					{
						value.BecomingActiveControl = true;
					}
					this.activeControl = value;
					this.UpdateFocusedControl();
				}
				finally
				{
					if (value != null)
					{
						value.BecomingActiveControl = false;
					}
				}
				if (this.activeControl == value)
				{
					Form form = base.FindFormInternal();
					if (form != null)
					{
						form.UpdateDefaultButton();
					}
				}
			}
			else
			{
				this.focusedControl = this.activeControl;
			}
			return this.activeControl == value;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0002D664 File Offset: 0x0002B864
		private void AxContainerFormCreated()
		{
			((AxHost.AxContainer)base.Properties.GetObject(ContainerControl.PropAxContainer)).FormCreated();
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002D680 File Offset: 0x0002B880
		internal override bool CanProcessMnemonic()
		{
			return this.state[ContainerControl.stateProcessingMnemonic] || base.CanProcessMnemonic();
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002D69C File Offset: 0x0002B89C
		internal AxHost.AxContainer CreateAxContainer()
		{
			object obj = base.Properties.GetObject(ContainerControl.PropAxContainer);
			if (obj == null)
			{
				obj = new AxHost.AxContainer(this);
				base.Properties.SetObject(ContainerControl.PropAxContainer, obj);
			}
			return (AxHost.AxContainer)obj;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002D6DB File Offset: 0x0002B8DB
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.activeControl = null;
			}
			base.Dispose(disposing);
			this.focusedControl = null;
			this.unvalidatedControl = null;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002D6FC File Offset: 0x0002B8FC
		private void EnableRequiredScaling(Control start, bool enable)
		{
			start.RequiredScalingEnabled = enable;
			foreach (object obj in start.Controls)
			{
				Control start2 = (Control)obj;
				this.EnableRequiredScaling(start2, enable);
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002D760 File Offset: 0x0002B960
		internal void FocusActiveControlInternal()
		{
			if (this.activeControl != null && this.activeControl.Visible)
			{
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				if (focus == IntPtr.Zero || Control.FromChildHandleInternal(focus) != this.activeControl)
				{
					UnsafeNativeMethods.SetFocus(new HandleRef(this.activeControl, this.activeControl.Handle));
					return;
				}
			}
			else
			{
				ContainerControl containerControl = this;
				while (containerControl != null && !containerControl.Visible)
				{
					Control parentInternal = containerControl.ParentInternal;
					if (parentInternal == null)
					{
						break;
					}
					containerControl = (parentInternal.GetContainerControlInternal() as ContainerControl);
				}
				if (containerControl != null && containerControl.Visible)
				{
					UnsafeNativeMethods.SetFocus(new HandleRef(containerControl, containerControl.Handle));
				}
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0002D804 File Offset: 0x0002BA04
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size sz = this.SizeFromClientSize(Size.Empty);
			Size sz2 = sz + base.Padding.Size;
			return this.LayoutEngine.GetPreferredSize(this, proposedSize - sz2) + sz2;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002D84C File Offset: 0x0002BA4C
		internal override Rectangle GetToolNativeScreenRectangle()
		{
			if (base.GetTopLevel())
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
				NativeMethods.POINT point = new NativeMethods.POINT(0, 0);
				UnsafeNativeMethods.ClientToScreen(new HandleRef(this, base.Handle), point);
				return new Rectangle(point.x, point.y, rect.right, rect.bottom);
			}
			return base.GetToolNativeScreenRectangle();
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0002D8BC File Offset: 0x0002BABC
		private SizeF GetFontAutoScaleDimensions()
		{
			SizeF empty = SizeF.Empty;
			IntPtr intPtr = UnsafeNativeMethods.CreateCompatibleDC(NativeMethods.NullHandleRef);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			HandleRef hDC = new HandleRef(this, intPtr);
			try
			{
				HandleRef hObject = new HandleRef(this, base.FontHandle);
				HandleRef hObject2 = new HandleRef(this, SafeNativeMethods.SelectObject(hDC, hObject));
				try
				{
					NativeMethods.TEXTMETRIC textmetric = default(NativeMethods.TEXTMETRIC);
					SafeNativeMethods.GetTextMetrics(hDC, ref textmetric);
					empty.Height = (float)textmetric.tmHeight;
					if ((textmetric.tmPitchAndFamily & 1) != 0)
					{
						IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
						IntUnsafeNativeMethods.GetTextExtentPoint32(hDC, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", size);
						empty.Width = (float)((int)Math.Round((double)((float)size.cx / (float)"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Length)));
					}
					else
					{
						empty.Width = (float)textmetric.tmAveCharWidth;
					}
				}
				finally
				{
					SafeNativeMethods.SelectObject(hDC, hObject2);
				}
			}
			finally
			{
				UnsafeNativeMethods.DeleteCompatibleDC(hDC);
			}
			return empty;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		private void LayoutScalingNeeded()
		{
			this.EnableRequiredScaling(this, true);
			this.state[ContainerControl.stateScalingNeededOnLayout] = true;
			if (!base.IsLayoutSuspended)
			{
				LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnAutoScaleModeChanged()
		{
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0002D9E7 File Offset: 0x0002BBE7
		protected virtual void OnAutoValidateChanged(EventArgs e)
		{
			if (this.autoValidateChanged != null)
			{
				this.autoValidateChanged(this, e);
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002DA00 File Offset: 0x0002BC00
		internal override void OnFrameWindowActivate(bool fActivate)
		{
			if (fActivate)
			{
				IntSecurity.ModifyFocus.Assert();
				try
				{
					if (this.ActiveControl == null)
					{
						base.SelectNextControl(null, true, true, true, false);
					}
					this.InnerMostActiveContainerControl.FocusActiveControlInternal();
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0002DA54 File Offset: 0x0002BC54
		internal override void OnChildLayoutResuming(Control child, bool performLayout)
		{
			base.OnChildLayoutResuming(child, performLayout);
			if (DpiHelper.EnableSinglePassScalingOfDpiForms && this.AutoScaleMode == AutoScaleMode.Dpi)
			{
				return;
			}
			if (!this.state[ContainerControl.stateScalingChild] && !performLayout && this.AutoScaleMode != AutoScaleMode.None && this.AutoScaleMode != AutoScaleMode.Inherit && this.state[ContainerControl.stateScalingNeededOnLayout])
			{
				this.state[ContainerControl.stateScalingChild] = true;
				try
				{
					child.Scale(this.AutoScaleFactor, SizeF.Empty, this);
				}
				finally
				{
					this.state[ContainerControl.stateScalingChild] = false;
				}
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0002DAFC File Offset: 0x0002BCFC
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (base.Properties.GetObject(ContainerControl.PropAxContainer) != null)
			{
				this.AxContainerFormCreated();
			}
			this.OnBindingContextChanged(EventArgs.Empty);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0002DB28 File Offset: 0x0002BD28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			if (this.AutoScaleMode == AutoScaleMode.Font)
			{
				this.currentAutoScaleDimensions = SizeF.Empty;
				this.SuspendAllLayout(this);
				try
				{
					this.PerformAutoScale(!base.RequiredScalingEnabled, true);
				}
				finally
				{
					this.ResumeAllLayout(this, false);
				}
			}
			base.OnFontChanged(e);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002DB84 File Offset: 0x0002BD84
		internal void FormDpiChanged(float factor)
		{
			this.currentAutoScaleDimensions = SizeF.Empty;
			this.SuspendAllLayout(this);
			SizeF sizeF = new SizeF(factor, factor);
			try
			{
				base.ScaleChildControls(sizeF, sizeF, this, true);
			}
			finally
			{
				this.ResumeAllLayout(this, false);
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0002DBD4 File Offset: 0x0002BDD4
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.PerformNeededAutoScaleOnLayout();
			base.OnLayout(e);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0002DBE3 File Offset: 0x0002BDE3
		internal override void OnLayoutResuming(bool performLayout)
		{
			this.PerformNeededAutoScaleOnLayout();
			base.OnLayoutResuming(performLayout);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0002DBF2 File Offset: 0x0002BDF2
		protected override void OnParentChanged(EventArgs e)
		{
			this.state[ContainerControl.stateParentChanged] = !base.RequiredScalingEnabled;
			base.OnParentChanged(e);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0002DC14 File Offset: 0x0002BE14
		public void PerformAutoScale()
		{
			this.PerformAutoScale(true, true);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0002DC20 File Offset: 0x0002BE20
		private void PerformAutoScale(bool includedBounds, bool excludedBounds)
		{
			bool flag = false;
			try
			{
				if (this.AutoScaleMode != AutoScaleMode.None && this.AutoScaleMode != AutoScaleMode.Inherit)
				{
					this.SuspendAllLayout(this);
					flag = true;
					SizeF includedFactor = SizeF.Empty;
					SizeF excludedFactor = SizeF.Empty;
					if (includedBounds)
					{
						includedFactor = this.AutoScaleFactor;
					}
					if (excludedBounds)
					{
						excludedFactor = this.AutoScaleFactor;
					}
					this.Scale(includedFactor, excludedFactor, this);
					this.autoScaleDimensions = this.CurrentAutoScaleDimensions;
				}
			}
			finally
			{
				if (includedBounds)
				{
					this.state[ContainerControl.stateScalingNeededOnLayout] = false;
					this.EnableRequiredScaling(this, false);
				}
				this.state[ContainerControl.stateParentChanged] = false;
				if (flag)
				{
					this.ResumeAllLayout(this, false);
				}
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0002DCCC File Offset: 0x0002BECC
		private void PerformNeededAutoScaleOnLayout()
		{
			if (this.state[ContainerControl.stateScalingNeededOnLayout])
			{
				this.PerformAutoScale(this.state[ContainerControl.stateScalingNeededOnLayout], false);
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0002DCF8 File Offset: 0x0002BEF8
		internal void ResumeAllLayout(Control start, bool performLayout)
		{
			Control.ControlCollection controls = start.Controls;
			for (int i = 0; i < controls.Count; i++)
			{
				this.ResumeAllLayout(controls[i], performLayout);
			}
			start.ResumeLayout(performLayout);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0002DD34 File Offset: 0x0002BF34
		internal void SuspendAllLayout(Control start)
		{
			start.SuspendLayout();
			CommonProperties.xClearPreferredSizeCache(start);
			Control.ControlCollection controls = start.Controls;
			for (int i = 0; i < controls.Count; i++)
			{
				this.SuspendAllLayout(controls[i]);
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0002DD74 File Offset: 0x0002BF74
		internal override void Scale(SizeF includedFactor, SizeF excludedFactor, Control requestingControl)
		{
			if (this.AutoScaleMode == AutoScaleMode.Inherit)
			{
				base.Scale(includedFactor, excludedFactor, requestingControl);
				return;
			}
			SizeF sizeF = excludedFactor;
			SizeF includedFactor2 = includedFactor;
			if (!sizeF.IsEmpty)
			{
				sizeF = this.AutoScaleFactor;
			}
			if (this.AutoScaleMode == AutoScaleMode.None)
			{
				includedFactor2 = this.AutoScaleFactor;
			}
			using (new LayoutTransaction(this, this, PropertyNames.Bounds, false))
			{
				SizeF excludedFactor2 = sizeF;
				if (!excludedFactor.IsEmpty && this.ParentInternal != null)
				{
					excludedFactor2 = SizeF.Empty;
					bool flag = requestingControl != this || this.state[ContainerControl.stateParentChanged];
					if (!flag)
					{
						bool flag2 = false;
						bool flag3 = false;
						ISite site = this.Site;
						ISite site2 = this.ParentInternal.Site;
						if (site != null)
						{
							flag2 = site.DesignMode;
						}
						if (site2 != null)
						{
							flag3 = site2.DesignMode;
						}
						if (flag2 && !flag3)
						{
							flag = true;
						}
					}
					if (flag)
					{
						excludedFactor2 = excludedFactor;
					}
				}
				base.ScaleControl(includedFactor, excludedFactor2, requestingControl);
				base.ScaleChildControls(includedFactor2, sizeF, requestingControl, false);
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0002DE70 File Offset: 0x0002C070
		private bool ProcessArrowKey(bool forward)
		{
			Control control = this;
			if (this.activeControl != null)
			{
				control = this.activeControl.ParentInternal;
			}
			return control.SelectNextControl(this.activeControl, forward, false, false, true);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogChar(char charCode)
		{
			ContainerControl containerControl = base.GetContainerControlInternal() as ContainerControl;
			return (containerControl != null && charCode != ' ' && this.ProcessMnemonic(charCode)) || base.ProcessDialogChar(charCode);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0002DED8 File Offset: 0x0002C0D8
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys != Keys.Tab)
				{
					if (keys - Keys.Left <= 3)
					{
						if (this.ProcessArrowKey(keys == Keys.Right || keys == Keys.Down))
						{
							return true;
						}
					}
				}
				else if (this.ProcessTabKey((keyData & Keys.Shift) == Keys.None))
				{
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0002DF36 File Offset: 0x0002C136
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return base.ProcessCmdKey(ref msg, keyData) || (this.ParentInternal == null && ToolStripManager.ProcessCmdKey(ref msg, keyData));
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0002DF58 File Offset: 0x0002C158
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (!this.CanProcessMnemonic())
			{
				return false;
			}
			if (base.Controls.Count == 0)
			{
				return false;
			}
			Control control = this.ActiveControl;
			this.state[ContainerControl.stateProcessingMnemonic] = true;
			bool result = false;
			try
			{
				bool flag = false;
				Control control2 = control;
				for (;;)
				{
					control2 = base.GetNextControl(control2, true);
					if (control2 != null)
					{
						if (control2.ProcessMnemonic(charCode))
						{
							break;
						}
					}
					else
					{
						if (flag)
						{
							goto Block_7;
						}
						flag = true;
					}
					if (control2 == control)
					{
						goto Block_8;
					}
				}
				result = true;
				Block_7:
				Block_8:;
			}
			finally
			{
				this.state[ContainerControl.stateProcessingMnemonic] = false;
			}
			return result;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0002DFE8 File Offset: 0x0002C1E8
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool ProcessTabKey(bool forward)
		{
			return base.SelectNextControl(this.activeControl, forward, true, true, false);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0002E000 File Offset: 0x0002C200
		private ScrollableControl FindScrollableParent(Control ctl)
		{
			Control parentInternal = ctl.ParentInternal;
			while (parentInternal != null && !(parentInternal is ScrollableControl))
			{
				parentInternal = parentInternal.ParentInternal;
			}
			if (parentInternal != null)
			{
				return (ScrollableControl)parentInternal;
			}
			return null;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0002E034 File Offset: 0x0002C234
		private void ScrollActiveControlIntoView()
		{
			Control control = this.activeControl;
			if (control != null)
			{
				for (ScrollableControl scrollableControl = this.FindScrollableParent(control); scrollableControl != null; scrollableControl = this.FindScrollableParent(scrollableControl))
				{
					scrollableControl.ScrollControlIntoView(this.activeControl);
				}
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0002E070 File Offset: 0x0002C270
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
				base.SelectNextControl(null, forward, true, true, false);
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0002E0B8 File Offset: 0x0002C2B8
		private void SetActiveControl(Control ctl)
		{
			this.SetActiveControlInternal(ctl);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0002E0C4 File Offset: 0x0002C2C4
		internal void SetActiveControlInternal(Control value)
		{
			if (this.activeControl != value || (value != null && !value.Focused))
			{
				if (value != null && !base.Contains(value))
				{
					throw new ArgumentException(SR.GetString("CannotActivateControl"));
				}
				ContainerControl containerControl = this;
				if (value != null && value.ParentInternal != null)
				{
					containerControl = (value.ParentInternal.GetContainerControlInternal() as ContainerControl);
				}
				bool flag;
				if (containerControl != null)
				{
					flag = containerControl.ActivateControlInternal(value, false);
				}
				else
				{
					flag = this.AssignActiveControlInternal(value);
				}
				if (containerControl != null && flag)
				{
					ContainerControl containerControl2 = this;
					while (containerControl2.ParentInternal != null && containerControl2.ParentInternal.GetContainerControlInternal() is ContainerControl)
					{
						containerControl2 = (containerControl2.ParentInternal.GetContainerControlInternal() as ContainerControl);
					}
					if (containerControl2.ContainsFocus && (value == null || !(value is UserControl) || (value is UserControl && !((UserControl)value).HasFocusableChild())))
					{
						containerControl.FocusActiveControlInternal();
					}
				}
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0002E1A0 File Offset: 0x0002C3A0
		internal ContainerControl InnerMostActiveContainerControl
		{
			get
			{
				ContainerControl containerControl = this;
				while (containerControl.ActiveControl is ContainerControl)
				{
					containerControl = (ContainerControl)containerControl.ActiveControl;
				}
				return containerControl;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0002E1CC File Offset: 0x0002C3CC
		internal ContainerControl InnerMostFocusedContainerControl
		{
			get
			{
				ContainerControl containerControl = this;
				while (containerControl.focusedControl is ContainerControl)
				{
					containerControl = (ContainerControl)containerControl.focusedControl;
				}
				return containerControl;
			}
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void UpdateDefaultButton()
		{
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0002E1F8 File Offset: 0x0002C3F8
		internal void UpdateFocusedControl()
		{
			this.EnsureUnvalidatedControl(this.focusedControl);
			Control control = this.focusedControl;
			while (this.activeControl != control)
			{
				if (control == null || control.IsDescendant(this.activeControl))
				{
					Control parentInternal = this.activeControl;
					for (;;)
					{
						Control parentInternal2 = parentInternal.ParentInternal;
						if (parentInternal2 == this || parentInternal2 == control)
						{
							break;
						}
						parentInternal = parentInternal.ParentInternal;
					}
					Control control2 = this.focusedControl = control;
					this.EnterValidation(parentInternal);
					if (this.focusedControl != control2)
					{
						control = this.focusedControl;
						continue;
					}
					control = parentInternal;
					if (NativeWindow.WndProcShouldBeDebuggable)
					{
						control.NotifyEnter();
						continue;
					}
					try
					{
						control.NotifyEnter();
						continue;
					}
					catch (Exception t)
					{
						Application.OnThreadException(t);
						continue;
					}
				}
				ContainerControl innerMostFocusedContainerControl = this.InnerMostFocusedContainerControl;
				Control control3 = null;
				if (innerMostFocusedContainerControl.focusedControl != null)
				{
					control = innerMostFocusedContainerControl.focusedControl;
					control3 = innerMostFocusedContainerControl;
					if (innerMostFocusedContainerControl != this)
					{
						innerMostFocusedContainerControl.focusedControl = null;
						if (innerMostFocusedContainerControl.ParentInternal == null || !(innerMostFocusedContainerControl.ParentInternal is MdiClient))
						{
							innerMostFocusedContainerControl.activeControl = null;
						}
					}
				}
				else
				{
					control = innerMostFocusedContainerControl;
					if (innerMostFocusedContainerControl.ParentInternal != null)
					{
						ContainerControl containerControl = innerMostFocusedContainerControl.ParentInternal.GetContainerControlInternal() as ContainerControl;
						control3 = containerControl;
						if (containerControl != null && containerControl != this)
						{
							containerControl.focusedControl = null;
							containerControl.activeControl = null;
						}
					}
				}
				do
				{
					Control control4 = control;
					if (control != null)
					{
						control = control.ParentInternal;
					}
					if (control == this)
					{
						control = null;
					}
					if (control4 != null)
					{
						if (NativeWindow.WndProcShouldBeDebuggable)
						{
							control4.NotifyLeave();
						}
						else
						{
							try
							{
								control4.NotifyLeave();
							}
							catch (Exception t2)
							{
								Application.OnThreadException(t2);
							}
						}
					}
				}
				while (control != null && control != control3 && !control.IsDescendant(this.activeControl));
			}
			this.focusedControl = this.activeControl;
			if (this.activeControl != null)
			{
				this.EnterValidation(this.activeControl);
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		private void EnsureUnvalidatedControl(Control candidate)
		{
			if (this.state[ContainerControl.stateValidating])
			{
				return;
			}
			if (this.unvalidatedControl != null)
			{
				return;
			}
			if (candidate == null)
			{
				return;
			}
			if (!candidate.ShouldAutoValidate)
			{
				return;
			}
			this.unvalidatedControl = candidate;
			while (this.unvalidatedControl is ContainerControl)
			{
				ContainerControl containerControl = this.unvalidatedControl as ContainerControl;
				if (containerControl.unvalidatedControl != null && containerControl.unvalidatedControl.ShouldAutoValidate)
				{
					this.unvalidatedControl = containerControl.unvalidatedControl;
				}
				else
				{
					if (containerControl.activeControl == null || !containerControl.activeControl.ShouldAutoValidate)
					{
						break;
					}
					this.unvalidatedControl = containerControl.activeControl;
				}
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0002E464 File Offset: 0x0002C664
		private void EnterValidation(Control enterControl)
		{
			if (this.unvalidatedControl == null)
			{
				return;
			}
			if (!enterControl.CausesValidation)
			{
				return;
			}
			AutoValidate autoValidateForControl = Control.GetAutoValidateForControl(this.unvalidatedControl);
			if (autoValidateForControl == AutoValidate.Disable)
			{
				return;
			}
			Control control = enterControl;
			while (control != null && !control.IsDescendant(this.unvalidatedControl))
			{
				control = control.ParentInternal;
			}
			bool preventFocusChangeOnError = autoValidateForControl == AutoValidate.EnablePreventFocusChange;
			this.ValidateThroughAncestor(control, preventFocusChangeOnError);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0002E4BD File Offset: 0x0002C6BD
		public bool Validate()
		{
			return this.Validate(false);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0002E4C8 File Offset: 0x0002C6C8
		public bool Validate(bool checkAutoValidate)
		{
			bool flag;
			return this.ValidateInternal(checkAutoValidate, out flag);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0002E4E0 File Offset: 0x0002C6E0
		internal bool ValidateInternal(bool checkAutoValidate, out bool validatedControlAllowsFocusChange)
		{
			validatedControlAllowsFocusChange = false;
			if (this.AutoValidate == AutoValidate.EnablePreventFocusChange || (this.activeControl != null && this.activeControl.CausesValidation))
			{
				if (this.unvalidatedControl == null)
				{
					if (this.focusedControl is ContainerControl && this.focusedControl.CausesValidation)
					{
						ContainerControl containerControl = (ContainerControl)this.focusedControl;
						if (!containerControl.ValidateInternal(checkAutoValidate, out validatedControlAllowsFocusChange))
						{
							return false;
						}
					}
					else
					{
						this.unvalidatedControl = this.focusedControl;
					}
				}
				bool preventFocusChangeOnError = true;
				Control control = (this.unvalidatedControl != null) ? this.unvalidatedControl : this.focusedControl;
				if (control != null)
				{
					AutoValidate autoValidateForControl = Control.GetAutoValidateForControl(control);
					if (checkAutoValidate && autoValidateForControl == AutoValidate.Disable)
					{
						return true;
					}
					preventFocusChangeOnError = (autoValidateForControl == AutoValidate.EnablePreventFocusChange);
					validatedControlAllowsFocusChange = (autoValidateForControl == AutoValidate.EnableAllowFocusChange);
				}
				return this.ValidateThroughAncestor(null, preventFocusChangeOnError);
			}
			return true;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0002E59A File Offset: 0x0002C79A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ValidateChildren()
		{
			return this.ValidateChildren(ValidationConstraints.Selectable);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0002E5A3 File Offset: 0x0002C7A3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			if (validationConstraints < ValidationConstraints.None || validationConstraints > (ValidationConstraints.Selectable | ValidationConstraints.Enabled | ValidationConstraints.Visible | ValidationConstraints.TabStop | ValidationConstraints.ImmediateChildren))
			{
				throw new InvalidEnumArgumentException("validationConstraints", (int)validationConstraints, typeof(ValidationConstraints));
			}
			return !base.PerformContainerValidation(validationConstraints);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0002E5D0 File Offset: 0x0002C7D0
		private bool ValidateThroughAncestor(Control ancestorControl, bool preventFocusChangeOnError)
		{
			if (ancestorControl == null)
			{
				ancestorControl = this;
			}
			if (this.state[ContainerControl.stateValidating])
			{
				return false;
			}
			if (this.unvalidatedControl == null)
			{
				this.unvalidatedControl = this.focusedControl;
			}
			if (this.unvalidatedControl == null)
			{
				return true;
			}
			if (!ancestorControl.IsDescendant(this.unvalidatedControl))
			{
				return false;
			}
			this.state[ContainerControl.stateValidating] = true;
			bool flag = false;
			Control control = this.activeControl;
			Control parentInternal = this.unvalidatedControl;
			if (control != null)
			{
				control.ValidationCancelled = false;
				if (control is ContainerControl)
				{
					ContainerControl containerControl = control as ContainerControl;
					containerControl.ResetValidationFlag();
				}
			}
			try
			{
				while (parentInternal != null && parentInternal != ancestorControl)
				{
					try
					{
						flag = parentInternal.PerformControlValidation(false);
					}
					catch
					{
						flag = true;
						throw;
					}
					if (flag)
					{
						break;
					}
					parentInternal = parentInternal.ParentInternal;
				}
				if (flag && preventFocusChangeOnError)
				{
					if (this.unvalidatedControl == null && parentInternal != null && ancestorControl.IsDescendant(parentInternal))
					{
						this.unvalidatedControl = parentInternal;
					}
					if (control == this.activeControl && control != null)
					{
						control.NotifyValidationResult(parentInternal, new CancelEventArgs
						{
							Cancel = true
						});
						if (control is ContainerControl)
						{
							ContainerControl containerControl2 = control as ContainerControl;
							if (containerControl2.focusedControl != null)
							{
								containerControl2.focusedControl.ValidationCancelled = true;
							}
							containerControl2.ResetActiveAndFocusedControlsRecursive();
						}
					}
					this.SetActiveControlInternal(this.unvalidatedControl);
				}
			}
			finally
			{
				this.unvalidatedControl = null;
				this.state[ContainerControl.stateValidating] = false;
			}
			return !flag;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0002E744 File Offset: 0x0002C944
		private void ResetValidationFlag()
		{
			Control.ControlCollection controls = base.Controls;
			int count = controls.Count;
			for (int i = 0; i < count; i++)
			{
				controls[i].ValidationCancelled = false;
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0002E778 File Offset: 0x0002C978
		internal void ResetActiveAndFocusedControlsRecursive()
		{
			if (this.activeControl is ContainerControl)
			{
				((ContainerControl)this.activeControl).ResetActiveAndFocusedControlsRecursive();
			}
			this.activeControl = null;
			this.focusedControl = null;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0002E7A5 File Offset: 0x0002C9A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeAutoValidate()
		{
			return this.autoValidate != AutoValidate.Inherit;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0002E7B4 File Offset: 0x0002C9B4
		private void WmSetFocus(ref Message m)
		{
			if (base.HostedInWin32DialogManager)
			{
				base.WndProc(ref m);
				return;
			}
			if (this.ActiveControl != null)
			{
				base.WmImeSetFocus();
				if (!this.ActiveControl.Visible)
				{
					base.InvokeGotFocus(this, EventArgs.Empty);
				}
				this.FocusActiveControlInternal();
				return;
			}
			if (this.ParentInternal != null)
			{
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					bool flag = false;
					ContainerControl containerControl = containerControlInternal as ContainerControl;
					if (containerControl != null)
					{
						flag = containerControl.ActivateControlInternal(this);
					}
					else
					{
						IntSecurity.ModifyFocus.Assert();
						try
						{
							flag = containerControlInternal.ActivateControl(this);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					if (!flag)
					{
						return;
					}
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0002E864 File Offset: 0x0002CA64
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

		// Token: 0x04000810 RID: 2064
		private Control activeControl;

		// Token: 0x04000811 RID: 2065
		private Control focusedControl;

		// Token: 0x04000812 RID: 2066
		private Control unvalidatedControl;

		// Token: 0x04000813 RID: 2067
		private AutoValidate autoValidate = AutoValidate.Inherit;

		// Token: 0x04000814 RID: 2068
		private EventHandler autoValidateChanged;

		// Token: 0x04000815 RID: 2069
		private SizeF autoScaleDimensions = SizeF.Empty;

		// Token: 0x04000816 RID: 2070
		private SizeF currentAutoScaleDimensions = SizeF.Empty;

		// Token: 0x04000817 RID: 2071
		private AutoScaleMode autoScaleMode = AutoScaleMode.Inherit;

		// Token: 0x04000818 RID: 2072
		private BitVector32 state;

		// Token: 0x04000819 RID: 2073
		private static readonly int stateScalingNeededOnLayout = BitVector32.CreateMask();

		// Token: 0x0400081A RID: 2074
		private static readonly int stateValidating = BitVector32.CreateMask(ContainerControl.stateScalingNeededOnLayout);

		// Token: 0x0400081B RID: 2075
		private static readonly int stateProcessingMnemonic = BitVector32.CreateMask(ContainerControl.stateValidating);

		// Token: 0x0400081C RID: 2076
		private static readonly int stateScalingChild = BitVector32.CreateMask(ContainerControl.stateProcessingMnemonic);

		// Token: 0x0400081D RID: 2077
		private static readonly int stateParentChanged = BitVector32.CreateMask(ContainerControl.stateScalingChild);

		// Token: 0x0400081E RID: 2078
		private static readonly int PropAxContainer = PropertyStore.CreateKey();

		// Token: 0x0400081F RID: 2079
		private const string fontMeasureString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	}
}
