using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200025D RID: 605
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[Designer("System.Windows.Forms.Design.FormDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[DesignerCategory("Form")]
	[DefaultEvent("Load")]
	[InitializationEvent("Load")]
	public class Form : ContainerControl
	{
		// Token: 0x0600260B RID: 9739 RVA: 0x000B0830 File Offset: 0x000AEA30
		public Form()
		{
			bool isRestrictedWindow = this.IsRestrictedWindow;
			this.formStateEx[Form.FormStateExShowIcon] = 1;
			base.SetState(2, false);
			base.SetState(524288, true);
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x000B08B3 File Offset: 0x000AEAB3
		// (set) Token: 0x0600260D RID: 9741 RVA: 0x000B08CA File Offset: 0x000AEACA
		[DefaultValue(null)]
		[SRDescription("FormAcceptButtonDescr")]
		public IButtonControl AcceptButton
		{
			get
			{
				return (IButtonControl)base.Properties.GetObject(Form.PropAcceptButton);
			}
			set
			{
				if (this.AcceptButton != value)
				{
					base.Properties.SetObject(Form.PropAcceptButton, value);
					this.UpdateDefaultButton();
				}
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x000B08EC File Offset: 0x000AEAEC
		// (set) Token: 0x0600260F RID: 9743 RVA: 0x000B0928 File Offset: 0x000AEB28
		internal bool Active
		{
			get
			{
				Form parentFormInternal = base.ParentFormInternal;
				if (parentFormInternal == null)
				{
					return this.formState[Form.FormStateIsActive] != 0;
				}
				return parentFormInternal.ActiveControl == this && parentFormInternal.Active;
			}
			set
			{
				if (this.formState[Form.FormStateIsActive] != 0 != value)
				{
					if (value && !this.CanRecreateHandle())
					{
						return;
					}
					this.formState[Form.FormStateIsActive] = (value ? 1 : 0);
					if (value)
					{
						this.formState[Form.FormStateIsWindowActivated] = 1;
						if (this.IsRestrictedWindow)
						{
							this.WindowText = this.userWindowText;
						}
						if (!base.ValidationCancelled)
						{
							if (base.ActiveControl == null)
							{
								base.SelectNextControlInternal(null, true, true, true, false);
							}
							base.InnerMostActiveContainerControl.FocusActiveControlInternal();
						}
						this.OnActivated(EventArgs.Empty);
						return;
					}
					this.formState[Form.FormStateIsWindowActivated] = 0;
					if (this.IsRestrictedWindow)
					{
						this.Text = this.userWindowText;
					}
					this.OnDeactivate(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x000B09FC File Offset: 0x000AEBFC
		public static Form ActiveForm
		{
			get
			{
				IntSecurity.GetParent.Demand();
				IntPtr foregroundWindow = UnsafeNativeMethods.GetForegroundWindow();
				Control control = Control.FromHandleInternal(foregroundWindow);
				if (control != null && control is Form)
				{
					return (Form)control;
				}
				return null;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x000B0A34 File Offset: 0x000AEC34
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormActiveMDIChildDescr")]
		public Form ActiveMdiChild
		{
			get
			{
				Form form = this.ActiveMdiChildInternal;
				if (form == null && this.ctlClient != null && this.ctlClient.IsHandleCreated)
				{
					IntPtr handle = this.ctlClient.SendMessage(553, 0, 0);
					form = (Control.FromHandleInternal(handle) as Form);
				}
				if (form != null && form.Visible && form.Enabled)
				{
					return form;
				}
				return null;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x000B0A95 File Offset: 0x000AEC95
		// (set) Token: 0x06002613 RID: 9747 RVA: 0x000B0AAC File Offset: 0x000AECAC
		internal Form ActiveMdiChildInternal
		{
			get
			{
				return (Form)base.Properties.GetObject(Form.PropActiveMdiChild);
			}
			set
			{
				base.Properties.SetObject(Form.PropActiveMdiChild, value);
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000B0ABF File Offset: 0x000AECBF
		// (set) Token: 0x06002615 RID: 9749 RVA: 0x000B0AD6 File Offset: 0x000AECD6
		private Form FormerlyActiveMdiChild
		{
			get
			{
				return (Form)base.Properties.GetObject(Form.PropFormerlyActiveMdiChild);
			}
			set
			{
				base.Properties.SetObject(Form.PropFormerlyActiveMdiChild, value);
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x000B0AE9 File Offset: 0x000AECE9
		// (set) Token: 0x06002617 RID: 9751 RVA: 0x000B0B00 File Offset: 0x000AED00
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlAllowTransparencyDescr")]
		public bool AllowTransparency
		{
			get
			{
				return this.formState[Form.FormStateAllowTransparency] != 0;
			}
			set
			{
				if (value != (this.formState[Form.FormStateAllowTransparency] != 0) && OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
				{
					this.formState[Form.FormStateAllowTransparency] = (value ? 1 : 0);
					this.formState[Form.FormStateLayered] = this.formState[Form.FormStateAllowTransparency];
					base.UpdateStyles();
					if (!value)
					{
						if (base.Properties.ContainsObject(Form.PropOpacity))
						{
							base.Properties.SetObject(Form.PropOpacity, 1f);
						}
						if (base.Properties.ContainsObject(Form.PropTransparencyKey))
						{
							base.Properties.SetObject(Form.PropTransparencyKey, Color.Empty);
						}
						this.UpdateLayered();
					}
				}
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x000B0BD8 File Offset: 0x000AEDD8
		// (set) Token: 0x06002619 RID: 9753 RVA: 0x000B0BF0 File Offset: 0x000AEDF0
		[SRCategory("CatLayout")]
		[SRDescription("FormAutoScaleDescr")]
		[Obsolete("This property has been deprecated. Use the AutoScaleMode property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AutoScale
		{
			get
			{
				return this.formState[Form.FormStateAutoScaling] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExSettingAutoScale] = 1;
				try
				{
					if (value)
					{
						this.formState[Form.FormStateAutoScaling] = 1;
						base.AutoScaleMode = AutoScaleMode.None;
					}
					else
					{
						this.formState[Form.FormStateAutoScaling] = 0;
					}
				}
				finally
				{
					this.formStateEx[Form.FormStateExSettingAutoScale] = 0;
				}
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x000B0C60 File Offset: 0x000AEE60
		// (set) Token: 0x0600261B RID: 9755 RVA: 0x000B0CAE File Offset: 0x000AEEAE
		[Localizable(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Size AutoScaleBaseSize
		{
			get
			{
				if (this.autoScaleBaseSize.IsEmpty)
				{
					SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
					return new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
				}
				return this.autoScaleBaseSize;
			}
			set
			{
				this.autoScaleBaseSize = value;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000B0CB7 File Offset: 0x000AEEB7
		// (set) Token: 0x0600261D RID: 9757 RVA: 0x000B0CBF File Offset: 0x000AEEBF
		[Localizable(true)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				if (value)
				{
					this.IsMdiContainer = false;
				}
				base.AutoScroll = value;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x000B0CD2 File Offset: 0x000AEED2
		// (set) Token: 0x0600261F RID: 9759 RVA: 0x000B0CE8 File Offset: 0x000AEEE8
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return this.formStateEx[Form.FormStateExAutoSize] != 0;
			}
			set
			{
				if (value != this.AutoSize)
				{
					this.formStateEx[Form.FormStateExAutoSize] = (value ? 1 : 0);
					if (!this.AutoSize)
					{
						this.minAutoSize = Size.Empty;
						this.Size = CommonProperties.GetSpecifiedBounds(this).Size;
					}
					LayoutTransaction.DoLayout(this, this, PropertyNames.AutoSize);
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1400019D RID: 413
		// (add) Token: 0x06002620 RID: 9760 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x06002621 RID: 9761 RVA: 0x00011A5F File Offset: 0x0000FC5F
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

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x000236ED File Offset: 0x000218ED
		// (set) Token: 0x06002623 RID: 9763 RVA: 0x000B0D54 File Offset: 0x000AEF54
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

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x000B0DDB File Offset: 0x000AEFDB
		// (set) Token: 0x06002625 RID: 9765 RVA: 0x000B0DE3 File Offset: 0x000AEFE3
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

		// Token: 0x1400019E RID: 414
		// (add) Token: 0x06002626 RID: 9766 RVA: 0x000B0DEC File Offset: 0x000AEFEC
		// (remove) Token: 0x06002627 RID: 9767 RVA: 0x000B0DF5 File Offset: 0x000AEFF5
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

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x000B0E00 File Offset: 0x000AF000
		// (set) Token: 0x06002629 RID: 9769 RVA: 0x00012F98 File Offset: 0x00011198
		public override Color BackColor
		{
			get
			{
				Color rawBackColor = base.RawBackColor;
				if (!rawBackColor.IsEmpty)
				{
					return rawBackColor;
				}
				return Control.DefaultBackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x000B0E24 File Offset: 0x000AF024
		// (set) Token: 0x0600262B RID: 9771 RVA: 0x000B0E39 File Offset: 0x000AF039
		private bool CalledClosing
		{
			get
			{
				return this.formStateEx[Form.FormStateExCalledClosing] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExCalledClosing] = (value ? 1 : 0);
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x000B0E52 File Offset: 0x000AF052
		// (set) Token: 0x0600262D RID: 9773 RVA: 0x000B0E67 File Offset: 0x000AF067
		private bool CalledCreateControl
		{
			get
			{
				return this.formStateEx[Form.FormStateExCalledCreateControl] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExCalledCreateControl] = (value ? 1 : 0);
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000B0E80 File Offset: 0x000AF080
		// (set) Token: 0x0600262F RID: 9775 RVA: 0x000B0E95 File Offset: 0x000AF095
		private bool CalledMakeVisible
		{
			get
			{
				return this.formStateEx[Form.FormStateExCalledMakeVisible] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExCalledMakeVisible] = (value ? 1 : 0);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x000B0EAE File Offset: 0x000AF0AE
		// (set) Token: 0x06002631 RID: 9777 RVA: 0x000B0EC3 File Offset: 0x000AF0C3
		private bool CalledOnLoad
		{
			get
			{
				return this.formStateEx[Form.FormStateExCalledOnLoad] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExCalledOnLoad] = (value ? 1 : 0);
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x000B0EDC File Offset: 0x000AF0DC
		// (set) Token: 0x06002633 RID: 9779 RVA: 0x000B0EF0 File Offset: 0x000AF0F0
		[SRCategory("CatAppearance")]
		[DefaultValue(FormBorderStyle.Sizable)]
		[DispId(-504)]
		[SRDescription("FormBorderStyleDescr")]
		public FormBorderStyle FormBorderStyle
		{
			get
			{
				return (FormBorderStyle)this.formState[Form.FormStateBorderStyle];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 6))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FormBorderStyle));
				}
				if (this.IsRestrictedWindow)
				{
					switch (value)
					{
					case FormBorderStyle.None:
						value = FormBorderStyle.FixedSingle;
						break;
					case FormBorderStyle.FixedSingle:
					case FormBorderStyle.Fixed3D:
					case FormBorderStyle.FixedDialog:
					case FormBorderStyle.Sizable:
						break;
					case FormBorderStyle.FixedToolWindow:
						value = FormBorderStyle.FixedSingle;
						break;
					case FormBorderStyle.SizableToolWindow:
						value = FormBorderStyle.Sizable;
						break;
					default:
						value = FormBorderStyle.Sizable;
						break;
					}
				}
				this.formState[Form.FormStateBorderStyle] = (int)value;
				if (this.formState[Form.FormStateSetClientSize] == 1 && !base.IsHandleCreated)
				{
					this.ClientSize = this.ClientSize;
				}
				Rectangle rectangle = this.restoredWindowBounds;
				BoundsSpecified boundsSpecified = this.restoredWindowBoundsSpecified;
				int value2 = this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize];
				int value3 = this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize];
				this.UpdateFormStyles();
				if (this.formState[Form.FormStateIconSet] == 0 && !this.IsRestrictedWindow)
				{
					this.UpdateWindowIcon(false);
				}
				if (this.WindowState != FormWindowState.Normal)
				{
					this.restoredWindowBounds = rectangle;
					this.restoredWindowBoundsSpecified = boundsSpecified;
					this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] = value2;
					this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] = value3;
				}
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000B1028 File Offset: 0x000AF228
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x000B103F File Offset: 0x000AF23F
		[DefaultValue(null)]
		[SRDescription("FormCancelButtonDescr")]
		public IButtonControl CancelButton
		{
			get
			{
				return (IButtonControl)base.Properties.GetObject(Form.PropCancelButton);
			}
			set
			{
				base.Properties.SetObject(Form.PropCancelButton, value);
				if (value != null && value.DialogResult == DialogResult.None)
				{
					value.DialogResult = DialogResult.Cancel;
				}
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000B1064 File Offset: 0x000AF264
		// (set) Token: 0x06002637 RID: 9783 RVA: 0x000B106C File Offset: 0x000AF26C
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new Size ClientSize
		{
			get
			{
				return base.ClientSize;
			}
			set
			{
				base.ClientSize = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x000B1075 File Offset: 0x000AF275
		// (set) Token: 0x06002639 RID: 9785 RVA: 0x000B108A File Offset: 0x000AF28A
		[SRCategory("CatWindowStyle")]
		[DefaultValue(true)]
		[SRDescription("FormControlBoxDescr")]
		public bool ControlBox
		{
			get
			{
				return this.formState[Form.FormStateControlBox] != 0;
			}
			set
			{
				if (this.IsRestrictedWindow)
				{
					return;
				}
				if (value)
				{
					this.formState[Form.FormStateControlBox] = 1;
				}
				else
				{
					this.formState[Form.FormStateControlBox] = 0;
				}
				this.UpdateFormStyles();
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x000B10C4 File Offset: 0x000AF2C4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				if (base.IsHandleCreated && (base.WindowStyle & 134217728) != 0)
				{
					createParams.Style |= 134217728;
				}
				else if (this.TopLevel)
				{
					createParams.Style &= -134217729;
				}
				if (this.TopLevel && this.formState[Form.FormStateLayered] != 0)
				{
					createParams.ExStyle |= 524288;
				}
				IWin32Window win32Window = (IWin32Window)base.Properties.GetObject(Form.PropDialogOwner);
				if (win32Window != null)
				{
					createParams.Parent = Control.GetSafeHandle(win32Window);
				}
				this.FillInCreateParamsBorderStyles(createParams);
				this.FillInCreateParamsWindowState(createParams);
				this.FillInCreateParamsBorderIcons(createParams);
				if (this.formState[Form.FormStateTaskBar] != 0)
				{
					createParams.ExStyle |= 262144;
				}
				FormBorderStyle formBorderStyle = this.FormBorderStyle;
				if (!this.ShowIcon && (formBorderStyle == FormBorderStyle.Sizable || formBorderStyle == FormBorderStyle.Fixed3D || formBorderStyle == FormBorderStyle.FixedSingle))
				{
					createParams.ExStyle |= 1;
				}
				if (this.IsMdiChild)
				{
					if (base.Visible && (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Normal))
					{
						Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
						Form activeMdiChildInternal = form.ActiveMdiChildInternal;
						if (activeMdiChildInternal != null && activeMdiChildInternal.WindowState == FormWindowState.Maximized)
						{
							createParams.Style |= 16777216;
							this.formState[Form.FormStateWindowState] = 2;
							base.SetState(65536, true);
						}
					}
					if (this.formState[Form.FormStateMdiChildMax] != 0)
					{
						createParams.Style |= 16777216;
					}
					createParams.ExStyle |= 64;
				}
				if (this.TopLevel || this.IsMdiChild)
				{
					this.FillInCreateParamsStartPosition(createParams);
					if ((createParams.Style & 268435456) != 0)
					{
						this.formState[Form.FormStateShowWindowOnCreate] = 1;
						createParams.Style &= -268435457;
					}
					else
					{
						this.formState[Form.FormStateShowWindowOnCreate] = 0;
					}
				}
				if (this.IsRestrictedWindow)
				{
					createParams.Caption = this.RestrictedWindowText(createParams.Caption);
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 5242880;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x000B132C File Offset: 0x000AF52C
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x000B1334 File Offset: 0x000AF534
		internal CloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
			set
			{
				this.closeReason = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x000B1340 File Offset: 0x000AF540
		internal static Icon DefaultIcon
		{
			get
			{
				if (Form.defaultIcon == null)
				{
					object obj = Form.internalSyncObject;
					lock (obj)
					{
						if (Form.defaultIcon == null)
						{
							Form.defaultIcon = new Icon(typeof(Form), "wfc.ico");
						}
					}
				}
				return Form.defaultIcon;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.NoControl;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x000B13A8 File Offset: 0x000AF5A8
		private static Icon DefaultRestrictedIcon
		{
			get
			{
				if (Form.defaultRestrictedIcon == null)
				{
					object obj = Form.internalSyncObject;
					lock (obj)
					{
						if (Form.defaultRestrictedIcon == null)
						{
							Form.defaultRestrictedIcon = new Icon(typeof(Form), "wfsecurity.ico");
						}
					}
				}
				return Form.defaultRestrictedIcon;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x000B1410 File Offset: 0x000AF610
		protected override Size DefaultSize
		{
			get
			{
				return new Size(300, 300);
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x000B1424 File Offset: 0x000AF624
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x000B1469 File Offset: 0x000AF669
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormDesktopBoundsDescr")]
		public Rectangle DesktopBounds
		{
			get
			{
				Rectangle workingArea = SystemInformation.WorkingArea;
				Rectangle bounds = base.Bounds;
				bounds.X -= workingArea.X;
				bounds.Y -= workingArea.Y;
				return bounds;
			}
			set
			{
				this.SetDesktopBounds(value.X, value.Y, value.Width, value.Height);
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x000B1490 File Offset: 0x000AF690
		// (set) Token: 0x06002644 RID: 9796 RVA: 0x000B14D5 File Offset: 0x000AF6D5
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormDesktopLocationDescr")]
		public Point DesktopLocation
		{
			get
			{
				Rectangle workingArea = SystemInformation.WorkingArea;
				Point location = this.Location;
				location.X -= workingArea.X;
				location.Y -= workingArea.Y;
				return location;
			}
			set
			{
				this.SetDesktopLocation(value.X, value.Y);
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x000B14EB File Offset: 0x000AF6EB
		// (set) Token: 0x06002646 RID: 9798 RVA: 0x000B14F3 File Offset: 0x000AF6F3
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormDialogResultDescr")]
		public DialogResult DialogResult
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

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x000B1524 File Offset: 0x000AF724
		internal override bool HasMenu
		{
			get
			{
				bool result = false;
				Menu menu = this.Menu;
				if (this.TopLevel && menu != null && menu.ItemCount > 0)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x000B1551 File Offset: 0x000AF751
		// (set) Token: 0x06002649 RID: 9801 RVA: 0x000B1566 File Offset: 0x000AF766
		[SRCategory("CatWindowStyle")]
		[DefaultValue(false)]
		[SRDescription("FormHelpButtonDescr")]
		public bool HelpButton
		{
			get
			{
				return this.formState[Form.FormStateHelpButton] != 0;
			}
			set
			{
				if (value)
				{
					this.formState[Form.FormStateHelpButton] = 1;
				}
				else
				{
					this.formState[Form.FormStateHelpButton] = 0;
				}
				this.UpdateFormStyles();
			}
		}

		// Token: 0x1400019F RID: 415
		// (add) Token: 0x0600264A RID: 9802 RVA: 0x000B1595 File Offset: 0x000AF795
		// (remove) Token: 0x0600264B RID: 9803 RVA: 0x000B15A8 File Offset: 0x000AF7A8
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[SRCategory("CatBehavior")]
		[SRDescription("FormHelpButtonClickedDescr")]
		public event CancelEventHandler HelpButtonClicked
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_HELPBUTTONCLICKED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_HELPBUTTONCLICKED, value);
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x000B15BB File Offset: 0x000AF7BB
		// (set) Token: 0x0600264D RID: 9805 RVA: 0x000B15EC File Offset: 0x000AF7EC
		[AmbientValue(null)]
		[Localizable(true)]
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormIconDescr")]
		public Icon Icon
		{
			get
			{
				if (this.formState[Form.FormStateIconSet] != 0)
				{
					return this.icon;
				}
				if (this.IsRestrictedWindow)
				{
					return Form.DefaultRestrictedIcon;
				}
				return Form.DefaultIcon;
			}
			set
			{
				if (this.icon != value && !this.IsRestrictedWindow)
				{
					if (value == Form.defaultIcon)
					{
						value = null;
					}
					this.formState[Form.FormStateIconSet] = ((value == null) ? 0 : 1);
					this.icon = value;
					if (this.smallIcon != null)
					{
						this.smallIcon.Dispose();
						this.smallIcon = null;
					}
					this.UpdateWindowIcon(true);
				}
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x000B1654 File Offset: 0x000AF854
		// (set) Token: 0x0600264F RID: 9807 RVA: 0x000B1669 File Offset: 0x000AF869
		private bool IsClosing
		{
			get
			{
				return this.formStateEx[Form.FormStateExWindowClosing] == 1;
			}
			set
			{
				this.formStateEx[Form.FormStateExWindowClosing] = (value ? 1 : 0);
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x000B1682 File Offset: 0x000AF882
		private bool IsMaximized
		{
			get
			{
				return this.WindowState == FormWindowState.Maximized || (this.IsMdiChild && this.formState[Form.FormStateMdiChildMax] == 1);
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x000B16AC File Offset: 0x000AF8AC
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormIsMDIChildDescr")]
		public bool IsMdiChild
		{
			get
			{
				return base.Properties.GetObject(Form.PropFormMdiParent) != null;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000B16C1 File Offset: 0x000AF8C1
		// (set) Token: 0x06002653 RID: 9811 RVA: 0x000B16EC File Offset: 0x000AF8EC
		internal bool IsMdiChildFocusable
		{
			get
			{
				return base.Properties.ContainsObject(Form.PropMdiChildFocusable) && (bool)base.Properties.GetObject(Form.PropMdiChildFocusable);
			}
			set
			{
				if (value != this.IsMdiChildFocusable)
				{
					base.Properties.SetObject(Form.PropMdiChildFocusable, value);
				}
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x000B170D File Offset: 0x000AF90D
		// (set) Token: 0x06002655 RID: 9813 RVA: 0x000B1718 File Offset: 0x000AF918
		[SRCategory("CatWindowStyle")]
		[DefaultValue(false)]
		[SRDescription("FormIsMDIContainerDescr")]
		public bool IsMdiContainer
		{
			get
			{
				return this.ctlClient != null;
			}
			set
			{
				if (value == this.IsMdiContainer)
				{
					return;
				}
				if (value)
				{
					this.AllowTransparency = false;
					base.Controls.Add(new MdiClient());
				}
				else
				{
					this.ActiveMdiChildInternal = null;
					this.ctlClient.Dispose();
				}
				base.Invalidate();
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x000B1758 File Offset: 0x000AF958
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsRestrictedWindow
		{
			get
			{
				if (this.formState[Form.FormStateIsRestrictedWindowChecked] == 0)
				{
					this.formState[Form.FormStateIsRestrictedWindow] = 0;
					try
					{
						IntSecurity.WindowAdornmentModification.Demand();
					}
					catch (SecurityException)
					{
						this.formState[Form.FormStateIsRestrictedWindow] = 1;
					}
					catch
					{
						this.formState[Form.FormStateIsRestrictedWindow] = 1;
						this.formState[Form.FormStateIsRestrictedWindowChecked] = 1;
						throw;
					}
					this.formState[Form.FormStateIsRestrictedWindowChecked] = 1;
				}
				return this.formState[Form.FormStateIsRestrictedWindow] != 0;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x000B1810 File Offset: 0x000AFA10
		// (set) Token: 0x06002658 RID: 9816 RVA: 0x000B1825 File Offset: 0x000AFA25
		[DefaultValue(false)]
		[SRDescription("FormKeyPreviewDescr")]
		public bool KeyPreview
		{
			get
			{
				return this.formState[Form.FormStateKeyPreview] != 0;
			}
			set
			{
				if (value)
				{
					this.formState[Form.FormStateKeyPreview] = 1;
					return;
				}
				this.formState[Form.FormStateKeyPreview] = 0;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x000B184D File Offset: 0x000AFA4D
		// (set) Token: 0x0600265A RID: 9818 RVA: 0x000B1855 File Offset: 0x000AFA55
		[SettingsBindable(true)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x000B185E File Offset: 0x000AFA5E
		// (set) Token: 0x0600265C RID: 9820 RVA: 0x000B1870 File Offset: 0x000AFA70
		protected Rectangle MaximizedBounds
		{
			get
			{
				return base.Properties.GetRectangle(Form.PropMaximizedBounds);
			}
			set
			{
				if (!value.Equals(this.MaximizedBounds))
				{
					base.Properties.SetRectangle(Form.PropMaximizedBounds, value);
					this.OnMaximizedBoundsChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140001A0 RID: 416
		// (add) Token: 0x0600265D RID: 9821 RVA: 0x000B18A8 File Offset: 0x000AFAA8
		// (remove) Token: 0x0600265E RID: 9822 RVA: 0x000B18BB File Offset: 0x000AFABB
		[SRCategory("CatPropertyChanged")]
		[SRDescription("FormOnMaximizedBoundsChangedDescr")]
		public event EventHandler MaximizedBoundsChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MAXIMIZEDBOUNDSCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MAXIMIZEDBOUNDSCHANGED, value);
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000B18CE File Offset: 0x000AFACE
		// (set) Token: 0x06002660 RID: 9824 RVA: 0x000B1910 File Offset: 0x000AFB10
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("FormMaximumSizeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(typeof(Size), "0, 0")]
		public override Size MaximumSize
		{
			get
			{
				if (base.Properties.ContainsInteger(Form.PropMaxTrackSizeWidth))
				{
					return new Size(base.Properties.GetInteger(Form.PropMaxTrackSizeWidth), base.Properties.GetInteger(Form.PropMaxTrackSizeHeight));
				}
				return Size.Empty;
			}
			set
			{
				if (!value.Equals(this.MaximumSize))
				{
					if (value.Width < 0 || value.Height < 0)
					{
						throw new ArgumentOutOfRangeException("MaximumSize");
					}
					base.Properties.SetInteger(Form.PropMaxTrackSizeWidth, value.Width);
					base.Properties.SetInteger(Form.PropMaxTrackSizeHeight, value.Height);
					if (!this.MinimumSize.IsEmpty && !value.IsEmpty)
					{
						if (base.Properties.GetInteger(Form.PropMinTrackSizeWidth) > value.Width)
						{
							base.Properties.SetInteger(Form.PropMinTrackSizeWidth, value.Width);
						}
						if (base.Properties.GetInteger(Form.PropMinTrackSizeHeight) > value.Height)
						{
							base.Properties.SetInteger(Form.PropMinTrackSizeHeight, value.Height);
						}
					}
					Size size = this.Size;
					if (!value.IsEmpty && (size.Width > value.Width || size.Height > value.Height))
					{
						this.Size = new Size(Math.Min(size.Width, value.Width), Math.Min(size.Height, value.Height));
					}
					this.OnMaximumSizeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140001A1 RID: 417
		// (add) Token: 0x06002661 RID: 9825 RVA: 0x000B1A6C File Offset: 0x000AFC6C
		// (remove) Token: 0x06002662 RID: 9826 RVA: 0x000B1A7F File Offset: 0x000AFC7F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("FormOnMaximumSizeChangedDescr")]
		public event EventHandler MaximumSizeChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MAXIMUMSIZECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MAXIMUMSIZECHANGED, value);
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x000B1A92 File Offset: 0x000AFC92
		// (set) Token: 0x06002664 RID: 9828 RVA: 0x000B1AA9 File Offset: 0x000AFCA9
		[SRCategory("CatWindowStyle")]
		[DefaultValue(null)]
		[SRDescription("FormMenuStripDescr")]
		[TypeConverter(typeof(ReferenceConverter))]
		public MenuStrip MainMenuStrip
		{
			get
			{
				return (MenuStrip)base.Properties.GetObject(Form.PropMainMenuStrip);
			}
			set
			{
				base.Properties.SetObject(Form.PropMainMenuStrip, value);
				if (base.IsHandleCreated && this.Menu == null)
				{
					this.UpdateMenuHandles();
				}
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000B1AD2 File Offset: 0x000AFCD2
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x000B1ADA File Offset: 0x000AFCDA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Margin
		{
			get
			{
				return base.Margin;
			}
			set
			{
				base.Margin = value;
			}
		}

		// Token: 0x140001A2 RID: 418
		// (add) Token: 0x06002667 RID: 9831 RVA: 0x000B1AE3 File Offset: 0x000AFCE3
		// (remove) Token: 0x06002668 RID: 9832 RVA: 0x000B1AEC File Offset: 0x000AFCEC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MarginChanged
		{
			add
			{
				base.MarginChanged += value;
			}
			remove
			{
				base.MarginChanged -= value;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x000B1AF5 File Offset: 0x000AFCF5
		// (set) Token: 0x0600266A RID: 9834 RVA: 0x000B1B0C File Offset: 0x000AFD0C
		[SRCategory("CatWindowStyle")]
		[DefaultValue(null)]
		[SRDescription("FormMenuDescr")]
		[TypeConverter(typeof(ReferenceConverter))]
		[Browsable(false)]
		public MainMenu Menu
		{
			get
			{
				return (MainMenu)base.Properties.GetObject(Form.PropMainMenu);
			}
			set
			{
				MainMenu menu = this.Menu;
				if (menu != value)
				{
					if (menu != null)
					{
						menu.form = null;
					}
					base.Properties.SetObject(Form.PropMainMenu, value);
					if (value != null)
					{
						if (value.form != null)
						{
							value.form.Menu = null;
						}
						value.form = this;
					}
					if (this.formState[Form.FormStateSetClientSize] == 1 && !base.IsHandleCreated)
					{
						this.ClientSize = this.ClientSize;
					}
					this.MenuChanged(0, value);
				}
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000B1B8C File Offset: 0x000AFD8C
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x000B1BCC File Offset: 0x000AFDCC
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("FormMinimumSizeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public override Size MinimumSize
		{
			get
			{
				if (base.Properties.ContainsInteger(Form.PropMinTrackSizeWidth))
				{
					return new Size(base.Properties.GetInteger(Form.PropMinTrackSizeWidth), base.Properties.GetInteger(Form.PropMinTrackSizeHeight));
				}
				return this.DefaultMinimumSize;
			}
			set
			{
				if (!value.Equals(this.MinimumSize))
				{
					if (value.Width < 0 || value.Height < 0)
					{
						throw new ArgumentOutOfRangeException("MinimumSize");
					}
					Rectangle bounds = base.Bounds;
					bounds.Size = value;
					value = WindowsFormsUtils.ConstrainToScreenWorkingAreaBounds(bounds).Size;
					base.Properties.SetInteger(Form.PropMinTrackSizeWidth, value.Width);
					base.Properties.SetInteger(Form.PropMinTrackSizeHeight, value.Height);
					if (!this.MaximumSize.IsEmpty && !value.IsEmpty)
					{
						if (base.Properties.GetInteger(Form.PropMaxTrackSizeWidth) < value.Width)
						{
							base.Properties.SetInteger(Form.PropMaxTrackSizeWidth, value.Width);
						}
						if (base.Properties.GetInteger(Form.PropMaxTrackSizeHeight) < value.Height)
						{
							base.Properties.SetInteger(Form.PropMaxTrackSizeHeight, value.Height);
						}
					}
					Size size = this.Size;
					if (size.Width < value.Width || size.Height < value.Height)
					{
						this.Size = new Size(Math.Max(size.Width, value.Width), Math.Max(size.Height, value.Height));
					}
					if (base.IsHandleCreated)
					{
						SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef, this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height, 4);
					}
					this.OnMinimumSizeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140001A3 RID: 419
		// (add) Token: 0x0600266D RID: 9837 RVA: 0x000B1D98 File Offset: 0x000AFF98
		// (remove) Token: 0x0600266E RID: 9838 RVA: 0x000B1DAB File Offset: 0x000AFFAB
		[SRCategory("CatPropertyChanged")]
		[SRDescription("FormOnMinimumSizeChangedDescr")]
		public event EventHandler MinimumSizeChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MINIMUMSIZECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MINIMUMSIZECHANGED, value);
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x000B1DBE File Offset: 0x000AFFBE
		// (set) Token: 0x06002670 RID: 9840 RVA: 0x000B1DD3 File Offset: 0x000AFFD3
		[SRCategory("CatWindowStyle")]
		[DefaultValue(true)]
		[SRDescription("FormMaximizeBoxDescr")]
		public bool MaximizeBox
		{
			get
			{
				return this.formState[Form.FormStateMaximizeBox] != 0;
			}
			set
			{
				if (value)
				{
					this.formState[Form.FormStateMaximizeBox] = 1;
				}
				else
				{
					this.formState[Form.FormStateMaximizeBox] = 0;
				}
				this.UpdateFormStyles();
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x000B1E02 File Offset: 0x000B0002
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormMDIChildrenDescr")]
		public Form[] MdiChildren
		{
			get
			{
				if (this.ctlClient != null)
				{
					return this.ctlClient.MdiChildren;
				}
				return new Form[0];
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000B1E1E File Offset: 0x000B001E
		internal MdiClient MdiClient
		{
			get
			{
				return this.ctlClient;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x000B1E26 File Offset: 0x000B0026
		// (set) Token: 0x06002674 RID: 9844 RVA: 0x000B1E38 File Offset: 0x000B0038
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormMDIParentDescr")]
		public Form MdiParent
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.MdiParentInternal;
			}
			set
			{
				this.MdiParentInternal = value;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000B1E41 File Offset: 0x000B0041
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x000B1E58 File Offset: 0x000B0058
		private Form MdiParentInternal
		{
			get
			{
				return (Form)base.Properties.GetObject(Form.PropFormMdiParent);
			}
			set
			{
				Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
				if (value == form && (value != null || this.ParentInternal == null))
				{
					return;
				}
				if (value != null && base.CreateThreadId != value.CreateThreadId)
				{
					throw new ArgumentException(SR.GetString("AddDifferentThreads"), "value");
				}
				bool state = base.GetState(2);
				base.Visible = false;
				try
				{
					if (value == null)
					{
						this.ParentInternal = null;
						base.SetTopLevel(true);
					}
					else
					{
						if (this.IsMdiContainer)
						{
							throw new ArgumentException(SR.GetString("FormMDIParentAndChild"), "value");
						}
						if (!value.IsMdiContainer)
						{
							throw new ArgumentException(SR.GetString("MDIParentNotContainer"), "value");
						}
						this.Dock = DockStyle.None;
						base.Properties.SetObject(Form.PropFormMdiParent, value);
						base.SetState(524288, false);
						this.ParentInternal = value.MdiClient;
						if (this.ParentInternal.IsHandleCreated && this.IsMdiChild && base.IsHandleCreated)
						{
							this.DestroyHandle();
						}
					}
					this.InvalidateMergedMenu();
					this.UpdateMenuHandles();
				}
				finally
				{
					base.UpdateStyles();
					base.Visible = state;
				}
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000B1F90 File Offset: 0x000B0190
		// (set) Token: 0x06002678 RID: 9848 RVA: 0x000B1FA7 File Offset: 0x000B01A7
		private MdiWindowListStrip MdiWindowListStrip
		{
			get
			{
				return base.Properties.GetObject(Form.PropMdiWindowListStrip) as MdiWindowListStrip;
			}
			set
			{
				base.Properties.SetObject(Form.PropMdiWindowListStrip, value);
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000B1FBA File Offset: 0x000B01BA
		// (set) Token: 0x0600267A RID: 9850 RVA: 0x000B1FD1 File Offset: 0x000B01D1
		private MdiControlStrip MdiControlStrip
		{
			get
			{
				return base.Properties.GetObject(Form.PropMdiControlStrip) as MdiControlStrip;
			}
			set
			{
				base.Properties.SetObject(Form.PropMdiControlStrip, value);
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000B1FE4 File Offset: 0x000B01E4
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormMergedMenuDescr")]
		public MainMenu MergedMenu
		{
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return this.MergedMenuPrivate;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000B1FEC File Offset: 0x000B01EC
		private MainMenu MergedMenuPrivate
		{
			get
			{
				Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
				if (form == null)
				{
					return null;
				}
				MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropMergedMenu);
				if (mainMenu != null)
				{
					return mainMenu;
				}
				MainMenu menu = form.Menu;
				MainMenu menu2 = this.Menu;
				if (menu2 == null)
				{
					return menu;
				}
				if (menu == null)
				{
					return menu2;
				}
				mainMenu = new MainMenu();
				mainMenu.ownerForm = this;
				mainMenu.MergeMenu(menu);
				mainMenu.MergeMenu(menu2);
				base.Properties.SetObject(Form.PropMergedMenu, mainMenu);
				return mainMenu;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000B2074 File Offset: 0x000B0274
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x000B2089 File Offset: 0x000B0289
		[SRCategory("CatWindowStyle")]
		[DefaultValue(true)]
		[SRDescription("FormMinimizeBoxDescr")]
		public bool MinimizeBox
		{
			get
			{
				return this.formState[Form.FormStateMinimizeBox] != 0;
			}
			set
			{
				if (value)
				{
					this.formState[Form.FormStateMinimizeBox] = 1;
				}
				else
				{
					this.formState[Form.FormStateMinimizeBox] = 0;
				}
				this.UpdateFormStyles();
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000B20B8 File Offset: 0x000B02B8
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormModalDescr")]
		public bool Modal
		{
			get
			{
				return base.GetState(32);
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x000B20C4 File Offset: 0x000B02C4
		// (set) Token: 0x06002681 RID: 9857 RVA: 0x000B20FC File Offset: 0x000B02FC
		[SRCategory("CatWindowStyle")]
		[TypeConverter(typeof(OpacityConverter))]
		[SRDescription("FormOpacityDescr")]
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				object @object = base.Properties.GetObject(Form.PropOpacity);
				if (@object != null)
				{
					return Convert.ToDouble(@object, CultureInfo.InvariantCulture);
				}
				return 1.0;
			}
			set
			{
				if (this.IsRestrictedWindow)
				{
					value = Math.Max(value, 0.5);
				}
				if (value > 1.0)
				{
					value = 1.0;
				}
				else if (value < 0.0)
				{
					value = 0.0;
				}
				base.Properties.SetObject(Form.PropOpacity, value);
				bool flag = this.formState[Form.FormStateLayered] != 0;
				if (this.OpacityAsByte < 255 && OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
				{
					this.AllowTransparency = true;
					if (this.formState[Form.FormStateLayered] != 1)
					{
						this.formState[Form.FormStateLayered] = 1;
						if (!flag)
						{
							base.UpdateStyles();
						}
					}
				}
				else
				{
					this.formState[Form.FormStateLayered] = ((this.TransparencyKey != Color.Empty) ? 1 : 0);
					if (flag != (this.formState[Form.FormStateLayered] != 0))
					{
						int num = (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -20));
						CreateParams createParams = this.CreateParams;
						if (num != createParams.ExStyle)
						{
							UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -20, new HandleRef(null, (IntPtr)createParams.ExStyle));
						}
					}
				}
				this.UpdateLayered();
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x000B226A File Offset: 0x000B046A
		private byte OpacityAsByte
		{
			get
			{
				return (byte)(this.Opacity * 255.0);
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000B2280 File Offset: 0x000B0480
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormOwnedFormsDescr")]
		public Form[] OwnedForms
		{
			get
			{
				Form[] sourceArray = (Form[])base.Properties.GetObject(Form.PropOwnedForms);
				int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				Form[] array = new Form[integer];
				if (integer > 0)
				{
					Array.Copy(sourceArray, 0, array, 0, integer);
				}
				return array;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x000B22CA File Offset: 0x000B04CA
		// (set) Token: 0x06002685 RID: 9861 RVA: 0x000B22DC File Offset: 0x000B04DC
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormOwnerDescr")]
		public Form Owner
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.OwnerInternal;
			}
			set
			{
				Form ownerInternal = this.OwnerInternal;
				if (ownerInternal == value)
				{
					return;
				}
				if (value != null && !this.TopLevel)
				{
					throw new ArgumentException(SR.GetString("NonTopLevelCantHaveOwner"), "value");
				}
				Control.CheckParentingCycle(this, value);
				Control.CheckParentingCycle(value, this);
				base.Properties.SetObject(Form.PropOwner, null);
				if (ownerInternal != null)
				{
					ownerInternal.RemoveOwnedForm(this);
				}
				base.Properties.SetObject(Form.PropOwner, value);
				if (value != null)
				{
					value.AddOwnedForm(this);
				}
				this.UpdateHandleWithOwner();
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x000B235F File Offset: 0x000B055F
		internal Form OwnerInternal
		{
			get
			{
				return (Form)base.Properties.GetObject(Form.PropOwner);
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x000B2378 File Offset: 0x000B0578
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Rectangle RestoreBounds
		{
			get
			{
				if (this.restoreBounds.Width == -1 && this.restoreBounds.Height == -1 && this.restoreBounds.X == -1 && this.restoreBounds.Y == -1)
				{
					return base.Bounds;
				}
				return this.restoreBounds;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x000B23CA File Offset: 0x000B05CA
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x000B23D4 File Offset: 0x000B05D4
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.rightToLeftLayout;
			}
			set
			{
				if (value != this.rightToLeftLayout)
				{
					this.rightToLeftLayout = value;
					using (new LayoutTransaction(this, this, PropertyNames.RightToLeftLayout))
					{
						this.OnRightToLeftLayoutChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x000B2428 File Offset: 0x000B0628
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x000B2430 File Offset: 0x000B0630
		internal override Control ParentInternal
		{
			get
			{
				return base.ParentInternal;
			}
			set
			{
				if (value != null)
				{
					this.Owner = null;
				}
				base.ParentInternal = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x000B2443 File Offset: 0x000B0643
		// (set) Token: 0x0600268D RID: 9869 RVA: 0x000B2458 File Offset: 0x000B0658
		[DefaultValue(true)]
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormShowInTaskbarDescr")]
		public bool ShowInTaskbar
		{
			get
			{
				return this.formState[Form.FormStateTaskBar] != 0;
			}
			set
			{
				if (this.IsRestrictedWindow)
				{
					return;
				}
				if (this.ShowInTaskbar != value)
				{
					if (value)
					{
						this.formState[Form.FormStateTaskBar] = 1;
					}
					else
					{
						this.formState[Form.FormStateTaskBar] = 0;
					}
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000B24AC File Offset: 0x000B06AC
		// (set) Token: 0x0600268F RID: 9871 RVA: 0x000B24C1 File Offset: 0x000B06C1
		[DefaultValue(true)]
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormShowIconDescr")]
		public bool ShowIcon
		{
			get
			{
				return this.formStateEx[Form.FormStateExShowIcon] != 0;
			}
			set
			{
				if (value)
				{
					this.formStateEx[Form.FormStateExShowIcon] = 1;
				}
				else
				{
					if (this.IsRestrictedWindow)
					{
						return;
					}
					this.formStateEx[Form.FormStateExShowIcon] = 0;
					base.UpdateStyles();
				}
				this.UpdateWindowIcon(true);
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x000B2500 File Offset: 0x000B0700
		internal override int ShowParams
		{
			get
			{
				FormWindowState windowState = this.WindowState;
				if (windowState == FormWindowState.Minimized)
				{
					return 2;
				}
				if (windowState == FormWindowState.Maximized)
				{
					return 3;
				}
				if (this.ShowWithoutActivation)
				{
					return 4;
				}
				return 5;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x00011A20 File Offset: 0x0000FC20
		[Browsable(false)]
		protected virtual bool ShowWithoutActivation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000B252B File Offset: 0x000B072B
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x000B2533 File Offset: 0x000B0733
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(false)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x000B253C File Offset: 0x000B073C
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x000B2550 File Offset: 0x000B0750
		[SRCategory("CatWindowStyle")]
		[DefaultValue(SizeGripStyle.Auto)]
		[SRDescription("FormSizeGripStyleDescr")]
		public SizeGripStyle SizeGripStyle
		{
			get
			{
				return (SizeGripStyle)this.formState[Form.FormStateSizeGripStyle];
			}
			set
			{
				if (this.SizeGripStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(SizeGripStyle));
					}
					this.formState[Form.FormStateSizeGripStyle] = (int)value;
					this.UpdateRenderSizeGrip();
				}
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x000B25A3 File Offset: 0x000B07A3
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x000B25B5 File Offset: 0x000B07B5
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[DefaultValue(FormStartPosition.WindowsDefaultLocation)]
		[SRDescription("FormStartPositionDescr")]
		public FormStartPosition StartPosition
		{
			get
			{
				return (FormStartPosition)this.formState[Form.FormStateStartPos];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FormStartPosition));
				}
				this.formState[Form.FormStateStartPos] = (int)value;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x000B25EE File Offset: 0x000B07EE
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x000B25F6 File Offset: 0x000B07F6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x140001A4 RID: 420
		// (add) Token: 0x0600269A RID: 9882 RVA: 0x000B25FF File Offset: 0x000B07FF
		// (remove) Token: 0x0600269B RID: 9883 RVA: 0x000B2608 File Offset: 0x000B0808
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x000B2619 File Offset: 0x000B0819
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x140001A5 RID: 421
		// (add) Token: 0x0600269E RID: 9886 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x0600269F RID: 9887 RVA: 0x000B262B File Offset: 0x000B082B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x000B2634 File Offset: 0x000B0834
		private HandleRef TaskbarOwner
		{
			get
			{
				if (this.ownerWindow == null)
				{
					this.ownerWindow = new NativeWindow();
				}
				if (this.ownerWindow.Handle == IntPtr.Zero)
				{
					CreateParams createParams = new CreateParams();
					createParams.ExStyle = 128;
					this.ownerWindow.CreateHandle(createParams);
				}
				return new HandleRef(this.ownerWindow, this.ownerWindow.Handle);
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x060026A2 RID: 9890 RVA: 0x00024185 File Offset: 0x00022385
		[SettingsBindable(true)]
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

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x000B269E File Offset: 0x000B089E
		// (set) Token: 0x060026A4 RID: 9892 RVA: 0x000B26A6 File Offset: 0x000B08A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool TopLevel
		{
			get
			{
				return base.GetTopLevel();
			}
			set
			{
				if (!value && this.IsMdiContainer && !base.DesignMode)
				{
					throw new ArgumentException(SR.GetString("MDIContainerMustBeTopLevel"), "value");
				}
				base.SetTopLevel(value);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x000B26D7 File Offset: 0x000B08D7
		// (set) Token: 0x060026A6 RID: 9894 RVA: 0x000B26EC File Offset: 0x000B08EC
		[DefaultValue(false)]
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormTopMostDescr")]
		public bool TopMost
		{
			get
			{
				return this.formState[Form.FormStateTopMost] != 0;
			}
			set
			{
				if (this.IsRestrictedWindow)
				{
					return;
				}
				if (base.IsHandleCreated && this.TopLevel)
				{
					HandleRef hWndInsertAfter = value ? NativeMethods.HWND_TOPMOST : NativeMethods.HWND_NOTOPMOST;
					SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), hWndInsertAfter, 0, 0, 0, 0, 3);
				}
				if (value)
				{
					this.formState[Form.FormStateTopMost] = 1;
					return;
				}
				this.formState[Form.FormStateTopMost] = 0;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x000B2760 File Offset: 0x000B0960
		// (set) Token: 0x060026A8 RID: 9896 RVA: 0x000B2790 File Offset: 0x000B0990
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormTransparencyKeyDescr")]
		public Color TransparencyKey
		{
			get
			{
				object @object = base.Properties.GetObject(Form.PropTransparencyKey);
				if (@object != null)
				{
					return (Color)@object;
				}
				return Color.Empty;
			}
			set
			{
				base.Properties.SetObject(Form.PropTransparencyKey, value);
				if (!this.IsMdiContainer)
				{
					bool flag = this.formState[Form.FormStateLayered] == 1;
					if (value != Color.Empty)
					{
						IntSecurity.TransparentWindows.Demand();
						this.AllowTransparency = true;
						this.formState[Form.FormStateLayered] = 1;
					}
					else
					{
						this.formState[Form.FormStateLayered] = ((this.OpacityAsByte < byte.MaxValue) ? 1 : 0);
					}
					if (flag != (this.formState[Form.FormStateLayered] != 0))
					{
						base.UpdateStyles();
					}
					this.UpdateLayered();
				}
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000B2848 File Offset: 0x000B0A48
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetVisibleCore(bool value)
		{
			if (this.GetVisibleCore() == value && this.dialogResult == DialogResult.OK)
			{
				return;
			}
			if (this.GetVisibleCore() == value && (!value || this.CalledMakeVisible))
			{
				base.SetVisibleCore(value);
				return;
			}
			if (value)
			{
				this.CalledMakeVisible = true;
				if (this.CalledCreateControl)
				{
					if (this.CalledOnLoad)
					{
						if (!Application.OpenFormsInternal.Contains(this))
						{
							Application.OpenFormsInternalAdd(this);
						}
					}
					else
					{
						this.CalledOnLoad = true;
						this.OnLoad(EventArgs.Empty);
						if (this.dialogResult != DialogResult.None)
						{
							value = false;
						}
					}
				}
			}
			else
			{
				this.ResetSecurityTip(true);
			}
			if (!this.IsMdiChild)
			{
				base.SetVisibleCore(value);
				if (this.formState[Form.FormStateSWCalled] == 0)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 24, value ? 1 : 0, 0);
				}
			}
			else
			{
				if (base.IsHandleCreated)
				{
					this.DestroyHandle();
				}
				if (!value)
				{
					this.InvalidateMergedMenu();
					base.SetState(2, false);
				}
				else
				{
					base.SetState(2, true);
					this.MdiParentInternal.MdiClient.PerformLayout();
					if (this.ParentInternal != null && this.ParentInternal.Visible)
					{
						base.SuspendLayout();
						try
						{
							SafeNativeMethods.ShowWindow(new HandleRef(this, base.Handle), 5);
							base.CreateControl();
							if (this.WindowState == FormWindowState.Maximized)
							{
								this.MdiParentInternal.UpdateWindowIcon(true);
							}
						}
						finally
						{
							base.ResumeLayout();
						}
					}
				}
				this.OnVisibleChanged(EventArgs.Empty);
			}
			if (value && !this.IsMdiChild && (this.WindowState == FormWindowState.Maximized || this.TopMost))
			{
				if (base.ActiveControl == null)
				{
					base.SelectNextControlInternal(null, true, true, true, false);
				}
				base.FocusActiveControlInternal();
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x000B29FC File Offset: 0x000B0BFC
		// (set) Token: 0x060026AB RID: 9899 RVA: 0x000B2A10 File Offset: 0x000B0C10
		[SRCategory("CatLayout")]
		[DefaultValue(FormWindowState.Normal)]
		[SRDescription("FormWindowStateDescr")]
		public FormWindowState WindowState
		{
			get
			{
				return (FormWindowState)this.formState[Form.FormStateWindowState];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FormWindowState));
				}
				if (this.TopLevel && this.IsRestrictedWindow && value != FormWindowState.Normal)
				{
					return;
				}
				if (value != FormWindowState.Normal)
				{
					if (value - FormWindowState.Minimized <= 1)
					{
						base.SetState(65536, true);
					}
				}
				else
				{
					base.SetState(65536, false);
				}
				if (base.IsHandleCreated && base.Visible)
				{
					IntPtr handle = base.Handle;
					switch (value)
					{
					case FormWindowState.Normal:
						SafeNativeMethods.ShowWindow(new HandleRef(this, handle), 1);
						break;
					case FormWindowState.Minimized:
						SafeNativeMethods.ShowWindow(new HandleRef(this, handle), 6);
						break;
					case FormWindowState.Maximized:
						SafeNativeMethods.ShowWindow(new HandleRef(this, handle), 3);
						break;
					}
				}
				this.formState[Form.FormStateWindowState] = (int)value;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x000B2AE6 File Offset: 0x000B0CE6
		// (set) Token: 0x060026AD RID: 9901 RVA: 0x000B2B20 File Offset: 0x000B0D20
		internal override string WindowText
		{
			get
			{
				if (!this.IsRestrictedWindow || this.formState[Form.FormStateIsWindowActivated] != 1)
				{
					return base.WindowText;
				}
				if (this.userWindowText == null)
				{
					return "";
				}
				return this.userWindowText;
			}
			set
			{
				string windowText = this.WindowText;
				this.userWindowText = value;
				if (this.IsRestrictedWindow && this.formState[Form.FormStateIsWindowActivated] == 1)
				{
					if (value == null)
					{
						value = "";
					}
					base.WindowText = this.RestrictedWindowText(value);
				}
				else
				{
					base.WindowText = value;
				}
				if (windowText == null || windowText.Length == 0 || value == null || value.Length == 0)
				{
					this.UpdateFormStyles();
				}
			}
		}

		// Token: 0x140001A6 RID: 422
		// (add) Token: 0x060026AE RID: 9902 RVA: 0x000B2B92 File Offset: 0x000B0D92
		// (remove) Token: 0x060026AF RID: 9903 RVA: 0x000B2BA5 File Offset: 0x000B0DA5
		[SRCategory("CatFocus")]
		[SRDescription("FormOnActivateDescr")]
		public event EventHandler Activated
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_ACTIVATED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_ACTIVATED, value);
			}
		}

		// Token: 0x140001A7 RID: 423
		// (add) Token: 0x060026B0 RID: 9904 RVA: 0x000B2BB8 File Offset: 0x000B0DB8
		// (remove) Token: 0x060026B1 RID: 9905 RVA: 0x000B2BCB File Offset: 0x000B0DCB
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnClosingDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event CancelEventHandler Closing
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_CLOSING, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_CLOSING, value);
			}
		}

		// Token: 0x140001A8 RID: 424
		// (add) Token: 0x060026B2 RID: 9906 RVA: 0x000B2BDE File Offset: 0x000B0DDE
		// (remove) Token: 0x060026B3 RID: 9907 RVA: 0x000B2BF1 File Offset: 0x000B0DF1
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnClosedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler Closed
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_CLOSED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_CLOSED, value);
			}
		}

		// Token: 0x140001A9 RID: 425
		// (add) Token: 0x060026B4 RID: 9908 RVA: 0x000B2C04 File Offset: 0x000B0E04
		// (remove) Token: 0x060026B5 RID: 9909 RVA: 0x000B2C17 File Offset: 0x000B0E17
		[SRCategory("CatFocus")]
		[SRDescription("FormOnDeactivateDescr")]
		public event EventHandler Deactivate
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_DEACTIVATE, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_DEACTIVATE, value);
			}
		}

		// Token: 0x140001AA RID: 426
		// (add) Token: 0x060026B6 RID: 9910 RVA: 0x000B2C2A File Offset: 0x000B0E2A
		// (remove) Token: 0x060026B7 RID: 9911 RVA: 0x000B2C3D File Offset: 0x000B0E3D
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnFormClosingDescr")]
		public event FormClosingEventHandler FormClosing
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_FORMCLOSING, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_FORMCLOSING, value);
			}
		}

		// Token: 0x140001AB RID: 427
		// (add) Token: 0x060026B8 RID: 9912 RVA: 0x000B2C50 File Offset: 0x000B0E50
		// (remove) Token: 0x060026B9 RID: 9913 RVA: 0x000B2C63 File Offset: 0x000B0E63
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnFormClosedDescr")]
		public event FormClosedEventHandler FormClosed
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_FORMCLOSED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_FORMCLOSED, value);
			}
		}

		// Token: 0x140001AC RID: 428
		// (add) Token: 0x060026BA RID: 9914 RVA: 0x000B2C76 File Offset: 0x000B0E76
		// (remove) Token: 0x060026BB RID: 9915 RVA: 0x000B2C89 File Offset: 0x000B0E89
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnLoadDescr")]
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_LOAD, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_LOAD, value);
			}
		}

		// Token: 0x140001AD RID: 429
		// (add) Token: 0x060026BC RID: 9916 RVA: 0x000B2C9C File Offset: 0x000B0E9C
		// (remove) Token: 0x060026BD RID: 9917 RVA: 0x000B2CAF File Offset: 0x000B0EAF
		[SRCategory("CatLayout")]
		[SRDescription("FormOnMDIChildActivateDescr")]
		public event EventHandler MdiChildActivate
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MDI_CHILD_ACTIVATE, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MDI_CHILD_ACTIVATE, value);
			}
		}

		// Token: 0x140001AE RID: 430
		// (add) Token: 0x060026BE RID: 9918 RVA: 0x000B2CC2 File Offset: 0x000B0EC2
		// (remove) Token: 0x060026BF RID: 9919 RVA: 0x000B2CD5 File Offset: 0x000B0ED5
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnMenuCompleteDescr")]
		[Browsable(false)]
		public event EventHandler MenuComplete
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MENUCOMPLETE, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MENUCOMPLETE, value);
			}
		}

		// Token: 0x140001AF RID: 431
		// (add) Token: 0x060026C0 RID: 9920 RVA: 0x000B2CE8 File Offset: 0x000B0EE8
		// (remove) Token: 0x060026C1 RID: 9921 RVA: 0x000B2CFB File Offset: 0x000B0EFB
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnMenuStartDescr")]
		[Browsable(false)]
		public event EventHandler MenuStart
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MENUSTART, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MENUSTART, value);
			}
		}

		// Token: 0x140001B0 RID: 432
		// (add) Token: 0x060026C2 RID: 9922 RVA: 0x000B2D0E File Offset: 0x000B0F0E
		// (remove) Token: 0x060026C3 RID: 9923 RVA: 0x000B2D21 File Offset: 0x000B0F21
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnInputLangChangeDescr")]
		public event InputLanguageChangedEventHandler InputLanguageChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_INPUTLANGCHANGE, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_INPUTLANGCHANGE, value);
			}
		}

		// Token: 0x140001B1 RID: 433
		// (add) Token: 0x060026C4 RID: 9924 RVA: 0x000B2D34 File Offset: 0x000B0F34
		// (remove) Token: 0x060026C5 RID: 9925 RVA: 0x000B2D47 File Offset: 0x000B0F47
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnInputLangChangeRequestDescr")]
		public event InputLanguageChangingEventHandler InputLanguageChanging
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_INPUTLANGCHANGEREQUEST, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_INPUTLANGCHANGEREQUEST, value);
			}
		}

		// Token: 0x140001B2 RID: 434
		// (add) Token: 0x060026C6 RID: 9926 RVA: 0x000B2D5A File Offset: 0x000B0F5A
		// (remove) Token: 0x060026C7 RID: 9927 RVA: 0x000B2D6D File Offset: 0x000B0F6D
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
		}

		// Token: 0x140001B3 RID: 435
		// (add) Token: 0x060026C8 RID: 9928 RVA: 0x000B2D80 File Offset: 0x000B0F80
		// (remove) Token: 0x060026C9 RID: 9929 RVA: 0x000B2D93 File Offset: 0x000B0F93
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnShownDescr")]
		public event EventHandler Shown
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_SHOWN, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_SHOWN, value);
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000B2DA8 File Offset: 0x000B0FA8
		public void Activate()
		{
			IntSecurity.ModifyFocus.Demand();
			if (base.Visible && base.IsHandleCreated)
			{
				if (this.IsMdiChild)
				{
					this.MdiParentInternal.MdiClient.SendMessage(546, base.Handle, 0);
					return;
				}
				UnsafeNativeMethods.SetForegroundWindow(new HandleRef(this, base.Handle));
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000B2E07 File Offset: 0x000B1007
		protected void ActivateMdiChild(Form form)
		{
			IntSecurity.ModifyFocus.Demand();
			this.ActivateMdiChildInternal(form);
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000B2E1C File Offset: 0x000B101C
		private void ActivateMdiChildInternal(Form form)
		{
			if (this.FormerlyActiveMdiChild != null && !this.FormerlyActiveMdiChild.IsClosing)
			{
				this.FormerlyActiveMdiChild.UpdateWindowIcon(true);
				this.FormerlyActiveMdiChild = null;
			}
			Form activeMdiChildInternal = this.ActiveMdiChildInternal;
			if (activeMdiChildInternal == form)
			{
				return;
			}
			if (activeMdiChildInternal != null)
			{
				activeMdiChildInternal.Active = false;
			}
			this.ActiveMdiChildInternal = form;
			if (form != null)
			{
				form.IsMdiChildFocusable = true;
				form.Active = true;
			}
			else if (this.Active)
			{
				base.ActivateControlInternal(this);
			}
			this.OnMdiChildActivate(EventArgs.Empty);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000B2EA0 File Offset: 0x000B10A0
		public void AddOwnedForm(Form ownedForm)
		{
			if (ownedForm == null)
			{
				return;
			}
			if (ownedForm.OwnerInternal != this)
			{
				ownedForm.Owner = this;
				return;
			}
			Form[] array = (Form[])base.Properties.GetObject(Form.PropOwnedForms);
			int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
			for (int i = 0; i < integer; i++)
			{
				if (array[i] == ownedForm)
				{
					return;
				}
			}
			if (array == null)
			{
				array = new Form[4];
				base.Properties.SetObject(Form.PropOwnedForms, array);
			}
			else if (array.Length == integer)
			{
				Form[] array2 = new Form[integer * 2];
				Array.Copy(array, 0, array2, 0, integer);
				array = array2;
				base.Properties.SetObject(Form.PropOwnedForms, array);
			}
			array[integer] = ownedForm;
			base.Properties.SetInteger(Form.PropOwnedFormsCount, integer + 1);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000B2F5C File Offset: 0x000B115C
		private float AdjustScale(float scale)
		{
			if (scale < 0.92f)
			{
				return scale + 0.08f;
			}
			if (scale < 1f)
			{
				return 1f;
			}
			if (scale > 1.01f)
			{
				return scale + 0.08f;
			}
			return scale;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000B2F8D File Offset: 0x000B118D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			if (this.WindowState != FormWindowState.Minimized)
			{
				base.AdjustFormScrollbars(displayScrollbars);
			}
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000B2FA0 File Offset: 0x000B11A0
		private void AdjustSystemMenu(IntPtr hmenu)
		{
			this.UpdateWindowState();
			FormWindowState windowState = this.WindowState;
			FormBorderStyle formBorderStyle = this.FormBorderStyle;
			bool flag = formBorderStyle == FormBorderStyle.SizableToolWindow || formBorderStyle == FormBorderStyle.Sizable;
			bool flag2 = this.MinimizeBox && windowState != FormWindowState.Minimized;
			bool flag3 = this.MaximizeBox && windowState != FormWindowState.Maximized;
			bool controlBox = this.ControlBox;
			bool flag4 = windowState > FormWindowState.Normal;
			bool flag5 = flag && windowState != FormWindowState.Minimized && windowState != FormWindowState.Maximized;
			if (!flag2)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61472, 1);
			}
			else
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61472, 0);
			}
			if (!flag3)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61488, 1);
			}
			else
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61488, 0);
			}
			if (!controlBox)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61536, 1);
			}
			else
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61536, 0);
			}
			if (!flag4)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61728, 1);
			}
			else
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61728, 0);
			}
			if (!flag5)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61440, 1);
				return;
			}
			UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61440, 0);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x000B30F0 File Offset: 0x000B12F0
		private void AdjustSystemMenu()
		{
			if (base.IsHandleCreated)
			{
				IntPtr hmenu = UnsafeNativeMethods.GetSystemMenu(new HandleRef(this, base.Handle), false);
				this.AdjustSystemMenu(hmenu);
				hmenu = IntPtr.Zero;
			}
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x000B3128 File Offset: 0x000B1328
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method has been deprecated. Use the ApplyAutoScaling method instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected void ApplyAutoScaling()
		{
			if (!this.autoScaleBaseSize.IsEmpty)
			{
				Size size = this.AutoScaleBaseSize;
				SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
				Size size2 = new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
				if (size.Equals(size2))
				{
					return;
				}
				float dy = this.AdjustScale((float)size2.Height / (float)size.Height);
				float dx = this.AdjustScale((float)size2.Width / (float)size.Width);
				base.Scale(dx, dy);
				this.AutoScaleBaseSize = size2;
			}
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000B31D4 File Offset: 0x000B13D4
		private void ApplyClientSize()
		{
			if (this.formState[Form.FormStateWindowState] != 0 || !base.IsHandleCreated)
			{
				return;
			}
			Size clientSize = this.ClientSize;
			bool hscroll = base.HScroll;
			bool vscroll = base.VScroll;
			bool flag = false;
			if (this.formState[Form.FormStateSetClientSize] != 0)
			{
				flag = true;
				this.formState[Form.FormStateSetClientSize] = 0;
			}
			if (flag)
			{
				if (hscroll)
				{
					clientSize.Height += SystemInformation.HorizontalScrollBarHeight;
				}
				if (vscroll)
				{
					clientSize.Width += SystemInformation.VerticalScrollBarWidth;
				}
			}
			IntPtr handle = base.Handle;
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			SafeNativeMethods.GetClientRect(new HandleRef(this, handle), ref rect);
			Rectangle rectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			Rectangle bounds = base.Bounds;
			if (clientSize.Width != rectangle.Width)
			{
				Size size = this.ComputeWindowSize(clientSize);
				if (vscroll)
				{
					size.Width += SystemInformation.VerticalScrollBarWidth;
				}
				if (hscroll)
				{
					size.Height += SystemInformation.HorizontalScrollBarHeight;
				}
				bounds.Width = size.Width;
				bounds.Height = size.Height;
				base.Bounds = bounds;
				SafeNativeMethods.GetClientRect(new HandleRef(this, handle), ref rect);
				rectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
			if (clientSize.Height != rectangle.Height)
			{
				int num = clientSize.Height - rectangle.Height;
				bounds.Height += num;
				base.Bounds = bounds;
			}
			base.UpdateBounds();
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x000B3390 File Offset: 0x000B1590
		internal override void AssignParent(Control value)
		{
			Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
			if (form != null && form.MdiClient != value)
			{
				base.Properties.SetObject(Form.PropFormMdiParent, null);
			}
			base.AssignParent(value);
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000B33D8 File Offset: 0x000B15D8
		internal bool CheckCloseDialog(bool closingOnly)
		{
			if (this.dialogResult == DialogResult.None && base.Visible)
			{
				return false;
			}
			try
			{
				FormClosingEventArgs formClosingEventArgs = new FormClosingEventArgs(this.closeReason, false);
				if (!this.CalledClosing)
				{
					this.OnClosing(formClosingEventArgs);
					this.OnFormClosing(formClosingEventArgs);
					if (formClosingEventArgs.Cancel)
					{
						this.dialogResult = DialogResult.None;
					}
					else
					{
						this.CalledClosing = true;
					}
				}
				if (!closingOnly && this.dialogResult != DialogResult.None)
				{
					FormClosedEventArgs e = new FormClosedEventArgs(this.closeReason);
					this.OnClosed(e);
					this.OnFormClosed(e);
					this.CalledClosing = false;
				}
			}
			catch (Exception t)
			{
				this.dialogResult = DialogResult.None;
				if (NativeWindow.WndProcShouldBeDebuggable)
				{
					throw;
				}
				Application.OnThreadException(t);
			}
			return this.dialogResult != DialogResult.None || !base.Visible;
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x000B34A0 File Offset: 0x000B16A0
		public void Close()
		{
			if (base.GetState(262144))
			{
				throw new InvalidOperationException(SR.GetString("ClosingWhileCreatingHandle", new object[]
				{
					"Close"
				}));
			}
			if (base.IsHandleCreated)
			{
				this.closeReason = CloseReason.UserClosing;
				base.SendMessage(16, 0, 0);
				return;
			}
			base.Dispose();
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000B34FC File Offset: 0x000B16FC
		private Size ComputeWindowSize(Size clientSize)
		{
			CreateParams createParams = this.CreateParams;
			return this.ComputeWindowSize(clientSize, createParams.Style, createParams.ExStyle);
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000B3524 File Offset: 0x000B1724
		private Size ComputeWindowSize(Size clientSize, int style, int exStyle)
		{
			NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, clientSize.Width, clientSize.Height);
			base.AdjustWindowRectEx(ref rect, style, this.HasMenu, exStyle);
			return new Size(rect.right - rect.left, rect.bottom - rect.top);
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000B3577 File Offset: 0x000B1777
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new Form.ControlCollection(this);
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000B357F File Offset: 0x000B177F
		internal override void AfterControlRemoved(Control control, Control oldParent)
		{
			base.AfterControlRemoved(control, oldParent);
			if (control == this.AcceptButton)
			{
				this.AcceptButton = null;
			}
			if (control == this.CancelButton)
			{
				this.CancelButton = null;
			}
			if (control == this.ctlClient)
			{
				this.ctlClient = null;
				this.UpdateMenuHandles();
			}
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x000B35C0 File Offset: 0x000B17C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void CreateHandle()
		{
			Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
			if (form != null)
			{
				form.SuspendUpdateMenuHandles();
			}
			try
			{
				if (this.IsMdiChild && this.MdiParentInternal.IsHandleCreated)
				{
					MdiClient mdiClient = this.MdiParentInternal.MdiClient;
					if (mdiClient != null && !mdiClient.IsHandleCreated)
					{
						mdiClient.CreateControl();
					}
				}
				if (this.IsMdiChild && this.formState[Form.FormStateWindowState] == 2)
				{
					this.formState[Form.FormStateWindowState] = 0;
					this.formState[Form.FormStateMdiChildMax] = 1;
					base.CreateHandle();
					this.formState[Form.FormStateWindowState] = 2;
					this.formState[Form.FormStateMdiChildMax] = 0;
				}
				else
				{
					base.CreateHandle();
				}
				this.UpdateHandleWithOwner();
				this.UpdateWindowIcon(false);
				this.AdjustSystemMenu();
				if (this.formState[Form.FormStateStartPos] != 3)
				{
					this.ApplyClientSize();
				}
				if (this.formState[Form.FormStateShowWindowOnCreate] == 1)
				{
					base.Visible = true;
				}
				if (this.Menu != null || !this.TopLevel || this.IsMdiContainer)
				{
					this.UpdateMenuHandles();
				}
				if (!this.ShowInTaskbar && this.OwnerInternal == null && this.TopLevel)
				{
					UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, this.TaskbarOwner);
					Icon icon = this.Icon;
					if (icon != null && this.TaskbarOwner.Handle != IntPtr.Zero)
					{
						UnsafeNativeMethods.SendMessage(this.TaskbarOwner, 128, 1, icon.Handle);
					}
				}
				if (this.formState[Form.FormStateTopMost] != 0)
				{
					this.TopMost = true;
				}
			}
			finally
			{
				if (form != null)
				{
					form.ResumeUpdateMenuHandles();
				}
				base.UpdateStyles();
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000B37A8 File Offset: 0x000B19A8
		private void DeactivateMdiChild()
		{
			Form activeMdiChildInternal = this.ActiveMdiChildInternal;
			if (activeMdiChildInternal != null)
			{
				Form mdiParentInternal = activeMdiChildInternal.MdiParentInternal;
				activeMdiChildInternal.Active = false;
				activeMdiChildInternal.IsMdiChildFocusable = false;
				if (!activeMdiChildInternal.IsClosing)
				{
					this.FormerlyActiveMdiChild = activeMdiChildInternal;
				}
				bool flag = true;
				foreach (Form form in mdiParentInternal.MdiChildren)
				{
					if (form != this && form.Visible)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					mdiParentInternal.ActivateMdiChildInternal(null);
				}
				this.ActiveMdiChildInternal = null;
				this.UpdateMenuHandles();
				this.UpdateToolStrip();
			}
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000B3834 File Offset: 0x000B1A34
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void DefWndProc(ref Message m)
		{
			if (this.ctlClient != null && this.ctlClient.IsHandleCreated && this.ctlClient.ParentInternal == this)
			{
				m.Result = UnsafeNativeMethods.DefFrameProc(m.HWnd, this.ctlClient.Handle, m.Msg, m.WParam, m.LParam);
				return;
			}
			if (this.formStateEx[Form.FormStateExUseMdiChildProc] != 0)
			{
				m.Result = UnsafeNativeMethods.DefMDIChildProc(m.HWnd, m.Msg, m.WParam, m.LParam);
				return;
			}
			base.DefWndProc(ref m);
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000B38D0 File Offset: 0x000B1AD0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.CalledOnLoad = false;
				this.CalledMakeVisible = false;
				this.CalledCreateControl = false;
				if (base.Properties.ContainsObject(Form.PropAcceptButton))
				{
					base.Properties.SetObject(Form.PropAcceptButton, null);
				}
				if (base.Properties.ContainsObject(Form.PropCancelButton))
				{
					base.Properties.SetObject(Form.PropCancelButton, null);
				}
				if (base.Properties.ContainsObject(Form.PropDefaultButton))
				{
					base.Properties.SetObject(Form.PropDefaultButton, null);
				}
				if (base.Properties.ContainsObject(Form.PropActiveMdiChild))
				{
					base.Properties.SetObject(Form.PropActiveMdiChild, null);
				}
				if (this.MdiWindowListStrip != null)
				{
					this.MdiWindowListStrip.Dispose();
					this.MdiWindowListStrip = null;
				}
				if (this.MdiControlStrip != null)
				{
					this.MdiControlStrip.Dispose();
					this.MdiControlStrip = null;
				}
				if (this.MainMenuStrip != null)
				{
					this.MainMenuStrip = null;
				}
				Form form = (Form)base.Properties.GetObject(Form.PropOwner);
				if (form != null)
				{
					form.RemoveOwnedForm(this);
					base.Properties.SetObject(Form.PropOwner, null);
				}
				Form[] array = (Form[])base.Properties.GetObject(Form.PropOwnedForms);
				int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				for (int i = integer - 1; i >= 0; i--)
				{
					if (array[i] != null)
					{
						array[i].Dispose();
					}
				}
				if (this.smallIcon != null)
				{
					this.smallIcon.Dispose();
					this.smallIcon = null;
				}
				this.ResetSecurityTip(false);
				base.Dispose(disposing);
				this.ctlClient = null;
				MainMenu menu = this.Menu;
				if (menu != null && menu.ownerForm == this)
				{
					menu.Dispose();
					base.Properties.SetObject(Form.PropMainMenu, null);
				}
				if (base.Properties.GetObject(Form.PropCurMenu) != null)
				{
					base.Properties.SetObject(Form.PropCurMenu, null);
				}
				this.MenuChanged(0, null);
				MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropDummyMenu);
				if (mainMenu != null)
				{
					mainMenu.Dispose();
					base.Properties.SetObject(Form.PropDummyMenu, null);
				}
				MainMenu mainMenu2 = (MainMenu)base.Properties.GetObject(Form.PropMergedMenu);
				if (mainMenu2 != null)
				{
					if (mainMenu2.ownerForm == this || mainMenu2.form == null)
					{
						mainMenu2.Dispose();
					}
					base.Properties.SetObject(Form.PropMergedMenu, null);
					return;
				}
			}
			else
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000B3B48 File Offset: 0x000B1D48
		private void FillInCreateParamsBorderIcons(CreateParams cp)
		{
			if (this.FormBorderStyle != FormBorderStyle.None)
			{
				if (this.Text != null && this.Text.Length != 0)
				{
					cp.Style |= 12582912;
				}
				if (this.ControlBox || this.IsRestrictedWindow)
				{
					cp.Style |= 13107200;
				}
				else
				{
					cp.Style &= -524289;
				}
				if (this.MaximizeBox || this.IsRestrictedWindow)
				{
					cp.Style |= 65536;
				}
				else
				{
					cp.Style &= -65537;
				}
				if (this.MinimizeBox || this.IsRestrictedWindow)
				{
					cp.Style |= 131072;
				}
				else
				{
					cp.Style &= -131073;
				}
				if (this.HelpButton && !this.MaximizeBox && !this.MinimizeBox && this.ControlBox)
				{
					cp.ExStyle |= 1024;
					return;
				}
				cp.ExStyle &= -1025;
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000B3C70 File Offset: 0x000B1E70
		private void FillInCreateParamsBorderStyles(CreateParams cp)
		{
			switch (this.formState[Form.FormStateBorderStyle])
			{
			case 0:
				if (!this.IsRestrictedWindow)
				{
					return;
				}
				break;
			case 1:
				break;
			case 2:
				cp.Style |= 8388608;
				cp.ExStyle |= 512;
				return;
			case 3:
				cp.Style |= 8388608;
				cp.ExStyle |= 1;
				return;
			case 4:
				cp.Style |= 8650752;
				return;
			case 5:
				cp.Style |= 8388608;
				cp.ExStyle |= 128;
				return;
			case 6:
				cp.Style |= 8650752;
				cp.ExStyle |= 128;
				return;
			default:
				return;
			}
			cp.Style |= 8388608;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000B3D74 File Offset: 0x000B1F74
		private void FillInCreateParamsStartPosition(CreateParams cp)
		{
			if (this.formState[Form.FormStateSetClientSize] != 0)
			{
				int style = cp.Style & -553648129;
				Size size = this.ComputeWindowSize(this.ClientSize, style, cp.ExStyle);
				if (this.IsRestrictedWindow)
				{
					size = this.ApplyBoundsConstraints(cp.X, cp.Y, size.Width, size.Height).Size;
				}
				cp.Width = size.Width;
				cp.Height = size.Height;
			}
			switch (this.formState[Form.FormStateStartPos])
			{
			case 1:
			{
				if (this.IsMdiChild)
				{
					Control mdiClient = this.MdiParentInternal.MdiClient;
					Rectangle clientRectangle = mdiClient.ClientRectangle;
					cp.X = Math.Max(clientRectangle.X, clientRectangle.X + (clientRectangle.Width - cp.Width) / 2);
					cp.Y = Math.Max(clientRectangle.Y, clientRectangle.Y + (clientRectangle.Height - cp.Height) / 2);
					return;
				}
				IWin32Window win32Window = (IWin32Window)base.Properties.GetObject(Form.PropDialogOwner);
				Screen screen;
				if (this.OwnerInternal != null || win32Window != null)
				{
					IntPtr hwnd = (win32Window != null) ? Control.GetSafeHandle(win32Window) : this.OwnerInternal.Handle;
					screen = Screen.FromHandleInternal(hwnd);
				}
				else
				{
					screen = Screen.FromPoint(Control.MousePosition);
				}
				Rectangle workingArea = screen.WorkingArea;
				if (this.WindowState != FormWindowState.Maximized)
				{
					cp.X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - cp.Width) / 2);
					cp.Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - cp.Height) / 2);
					return;
				}
				return;
			}
			case 2:
			case 4:
				break;
			case 3:
				cp.Width = int.MinValue;
				cp.Height = int.MinValue;
				break;
			default:
				return;
			}
			if (!this.IsMdiChild || this.Dock == DockStyle.None)
			{
				cp.X = int.MinValue;
				cp.Y = int.MinValue;
				return;
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000B3F9C File Offset: 0x000B219C
		private void FillInCreateParamsWindowState(CreateParams cp)
		{
			FormWindowState formWindowState = (FormWindowState)this.formState[Form.FormStateWindowState];
			if (formWindowState != FormWindowState.Minimized)
			{
				if (formWindowState == FormWindowState.Maximized)
				{
					cp.Style |= 16777216;
					return;
				}
			}
			else
			{
				cp.Style |= 536870912;
			}
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000B3FE7 File Offset: 0x000B21E7
		internal override bool FocusInternal()
		{
			if (this.IsMdiChild)
			{
				this.MdiParentInternal.MdiClient.SendMessage(546, base.Handle, 0);
				return this.Focused;
			}
			return base.FocusInternal();
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000B401C File Offset: 0x000B221C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method has been deprecated. Use the AutoScaleDimensions property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static SizeF GetAutoScaleSize(Font font)
		{
			float height = (float)font.Height;
			float width = 9f;
			try
			{
				using (Graphics graphics = Graphics.FromHwndInternal(IntPtr.Zero))
				{
					string text = "The quick brown fox jumped over the lazy dog.";
					double num = 44.54999694824219;
					float width2 = graphics.MeasureString(text, font).Width;
					width = (float)((double)width2 / num);
				}
			}
			catch
			{
			}
			return new SizeF(width, height);
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000B40A4 File Offset: 0x000B22A4
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			return base.GetPreferredSizeCore(proposedSize);
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000B40BC File Offset: 0x000B22BC
		private void ResolveZoneAndSiteNames(ArrayList sites, ref string securityZone, ref string securitySite)
		{
			securityZone = SR.GetString("SecurityRestrictedWindowTextUnknownZone");
			securitySite = SR.GetString("SecurityRestrictedWindowTextUnknownSite");
			try
			{
				if (sites != null && sites.Count != 0)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in sites)
					{
						if (obj == null)
						{
							return;
						}
						string text = obj.ToString();
						if (text.Length == 0)
						{
							return;
						}
						Zone zone = Zone.CreateFromUrl(text);
						if (!zone.SecurityZone.Equals(SecurityZone.MyComputer))
						{
							string text2 = zone.SecurityZone.ToString();
							if (!arrayList.Contains(text2))
							{
								arrayList.Add(text2);
							}
						}
					}
					if (arrayList.Count == 0)
					{
						securityZone = SecurityZone.MyComputer.ToString();
					}
					else if (arrayList.Count == 1)
					{
						securityZone = arrayList[0].ToString();
					}
					else
					{
						securityZone = SR.GetString("SecurityRestrictedWindowTextMixedZone");
					}
					ArrayList arrayList2 = new ArrayList();
					new FileIOPermission(PermissionState.None)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Assert();
					try
					{
						foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
						{
							if (assembly.GlobalAssemblyCache)
							{
								arrayList2.Add(assembly.CodeBase.ToUpper(CultureInfo.InvariantCulture));
							}
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					ArrayList arrayList3 = new ArrayList();
					foreach (object obj2 in sites)
					{
						Uri uri = new Uri(obj2.ToString());
						if (!arrayList2.Contains(uri.AbsoluteUri.ToUpper(CultureInfo.InvariantCulture)))
						{
							string host = uri.Host;
							if (host.Length > 0 && !arrayList3.Contains(host))
							{
								arrayList3.Add(host);
							}
						}
					}
					if (arrayList3.Count == 0)
					{
						new EnvironmentPermission(PermissionState.Unrestricted).Assert();
						try
						{
							securitySite = Environment.MachineName;
							goto IL_24D;
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					if (arrayList3.Count == 1)
					{
						securitySite = arrayList3[0].ToString();
					}
					else
					{
						securitySite = SR.GetString("SecurityRestrictedWindowTextMultipleSites");
					}
					IL_24D:;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000B4398 File Offset: 0x000B2598
		private string RestrictedWindowText(string original)
		{
			this.EnsureSecurityInformation();
			return string.Format(CultureInfo.CurrentCulture, Application.SafeTopLevelCaptionFormat, new object[]
			{
				original,
				this.securityZone,
				this.securitySite
			});
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000B43CC File Offset: 0x000B25CC
		private void EnsureSecurityInformation()
		{
			if (this.securityZone == null || this.securitySite == null)
			{
				ArrayList arrayList;
				ArrayList sites;
				SecurityManager.GetZoneAndOrigin(out arrayList, out sites);
				this.ResolveZoneAndSiteNames(sites, ref this.securityZone, ref this.securitySite);
			}
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000B4405 File Offset: 0x000B2605
		private void CallShownEvent()
		{
			this.OnShown(EventArgs.Empty);
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000B4412 File Offset: 0x000B2612
		internal override bool CanSelectCore()
		{
			return base.GetStyle(ControlStyles.Selectable) && base.Enabled && base.Visible;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000B4434 File Offset: 0x000B2634
		internal bool CanRecreateHandle()
		{
			return !this.IsMdiChild || (base.GetState(2) && base.IsHandleCreated);
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000B4451 File Offset: 0x000B2651
		internal override bool CanProcessMnemonic()
		{
			return (!this.IsMdiChild || (this.formStateEx[Form.FormStateExMnemonicProcessed] != 1 && this == this.MdiParentInternal.ActiveMdiChildInternal && this.WindowState != FormWindowState.Minimized)) && base.CanProcessMnemonic();
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000B4490 File Offset: 0x000B2690
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.ProcessMnemonic(charCode))
			{
				return true;
			}
			if (this.IsMdiContainer)
			{
				if (base.Controls.Count > 1)
				{
					for (int i = 0; i < base.Controls.Count; i++)
					{
						Control control = base.Controls[i];
						if (!(control is MdiClient) && control.ProcessMnemonic(charCode))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000B44F8 File Offset: 0x000B26F8
		protected void CenterToParent()
		{
			if (this.TopLevel)
			{
				Point location = default(Point);
				Size size = this.Size;
				IntPtr intPtr = IntPtr.Zero;
				intPtr = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -8);
				if (intPtr != IntPtr.Zero)
				{
					Screen screen = Screen.FromHandleInternal(intPtr);
					Rectangle workingArea = screen.WorkingArea;
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.GetWindowRect(new HandleRef(null, intPtr), ref rect);
					location.X = (rect.left + rect.right - size.Width) / 2;
					if (location.X < workingArea.X)
					{
						location.X = workingArea.X;
					}
					else if (location.X + size.Width > workingArea.X + workingArea.Width)
					{
						location.X = workingArea.X + workingArea.Width - size.Width;
					}
					location.Y = (rect.top + rect.bottom - size.Height) / 2;
					if (location.Y < workingArea.Y)
					{
						location.Y = workingArea.Y;
					}
					else if (location.Y + size.Height > workingArea.Y + workingArea.Height)
					{
						location.Y = workingArea.Y + workingArea.Height - size.Height;
					}
					this.Location = location;
					return;
				}
				this.CenterToScreen();
			}
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000B467C File Offset: 0x000B287C
		protected void CenterToScreen()
		{
			Point location = default(Point);
			Screen screen;
			if (this.OwnerInternal != null)
			{
				screen = Screen.FromControl(this.OwnerInternal);
			}
			else
			{
				IntPtr intPtr = IntPtr.Zero;
				if (this.TopLevel)
				{
					intPtr = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -8);
				}
				if (intPtr != IntPtr.Zero)
				{
					screen = Screen.FromHandleInternal(intPtr);
				}
				else
				{
					screen = Screen.FromPoint(Control.MousePosition);
				}
			}
			Rectangle workingArea = screen.WorkingArea;
			location.X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - base.Width) / 2);
			location.Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - base.Height) / 2);
			this.Location = location;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000B4750 File Offset: 0x000B2950
		private void InvalidateMergedMenu()
		{
			if (base.Properties.ContainsObject(Form.PropMergedMenu))
			{
				MainMenu mainMenu = base.Properties.GetObject(Form.PropMergedMenu) as MainMenu;
				if (mainMenu != null && mainMenu.ownerForm == this)
				{
					mainMenu.Dispose();
				}
				base.Properties.SetObject(Form.PropMergedMenu, null);
			}
			Form parentFormInternal = base.ParentFormInternal;
			if (parentFormInternal != null)
			{
				parentFormInternal.MenuChanged(0, parentFormInternal.Menu);
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x000B47BF File Offset: 0x000B29BF
		public void LayoutMdi(MdiLayout value)
		{
			if (this.ctlClient == null)
			{
				return;
			}
			this.ctlClient.LayoutMdi(value);
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000B47D8 File Offset: 0x000B29D8
		internal void MenuChanged(int change, Menu menu)
		{
			Form parentFormInternal = base.ParentFormInternal;
			if (parentFormInternal != null && this == parentFormInternal.ActiveMdiChildInternal)
			{
				parentFormInternal.MenuChanged(change, menu);
				return;
			}
			switch (change)
			{
			case 0:
			case 3:
				if (this.ctlClient != null && this.ctlClient.IsHandleCreated)
				{
					if (base.IsHandleCreated)
					{
						this.UpdateMenuHandles(null, false);
					}
					Control.ControlCollection controls = this.ctlClient.Controls;
					int count = controls.Count;
					while (count-- > 0)
					{
						Control control = controls[count];
						if (control is Form && control.Properties.ContainsObject(Form.PropMergedMenu))
						{
							MainMenu mainMenu = control.Properties.GetObject(Form.PropMergedMenu) as MainMenu;
							if (mainMenu != null && mainMenu.ownerForm == control)
							{
								mainMenu.Dispose();
							}
							control.Properties.SetObject(Form.PropMergedMenu, null);
						}
					}
					this.UpdateMenuHandles();
					return;
				}
				if (menu == this.Menu && change == 0)
				{
					this.UpdateMenuHandles();
					return;
				}
				break;
			case 1:
				if (menu == this.Menu || (this.ActiveMdiChildInternal != null && menu == this.ActiveMdiChildInternal.Menu))
				{
					this.UpdateMenuHandles();
					return;
				}
				break;
			case 2:
				if (this.ctlClient != null && this.ctlClient.IsHandleCreated)
				{
					this.UpdateMenuHandles();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x000B491C File Offset: 0x000B2B1C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnActivated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_ACTIVATED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000B494A File Offset: 0x000B2B4A
		internal override void OnAutoScaleModeChanged()
		{
			base.OnAutoScaleModeChanged();
			if (this.formStateEx[Form.FormStateExSettingAutoScale] != 1)
			{
				this.AutoScale = false;
			}
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x000B496C File Offset: 0x000B2B6C
		protected override void OnBackgroundImageChanged(EventArgs e)
		{
			base.OnBackgroundImageChanged(e);
			if (this.IsMdiContainer)
			{
				this.MdiClient.BackgroundImage = this.BackgroundImage;
				this.MdiClient.Invalidate();
			}
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000B4999 File Offset: 0x000B2B99
		protected override void OnBackgroundImageLayoutChanged(EventArgs e)
		{
			base.OnBackgroundImageLayoutChanged(e);
			if (this.IsMdiContainer)
			{
				this.MdiClient.BackgroundImageLayout = this.BackgroundImageLayout;
				this.MdiClient.Invalidate();
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000B49C8 File Offset: 0x000B2BC8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClosing(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Form.EVENT_CLOSING];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000B49F8 File Offset: 0x000B2BF8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_CLOSED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000B4A28 File Offset: 0x000B2C28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFormClosing(FormClosingEventArgs e)
		{
			FormClosingEventHandler formClosingEventHandler = (FormClosingEventHandler)base.Events[Form.EVENT_FORMCLOSING];
			if (formClosingEventHandler != null)
			{
				formClosingEventHandler(this, e);
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000B4A58 File Offset: 0x000B2C58
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFormClosed(FormClosedEventArgs e)
		{
			Application.OpenFormsInternalRemove(this);
			FormClosedEventHandler formClosedEventHandler = (FormClosedEventHandler)base.Events[Form.EVENT_FORMCLOSED];
			if (formClosedEventHandler != null)
			{
				formClosedEventHandler(this, e);
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000B4A8C File Offset: 0x000B2C8C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnCreateControl()
		{
			this.CalledCreateControl = true;
			base.OnCreateControl();
			if (this.CalledMakeVisible && !this.CalledOnLoad)
			{
				this.CalledOnLoad = true;
				this.OnLoad(EventArgs.Empty);
			}
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000B4AC0 File Offset: 0x000B2CC0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDeactivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_DEACTIVATE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000B4AF0 File Offset: 0x000B2CF0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			if (!base.DesignMode && base.Enabled && this.Active)
			{
				if (base.ActiveControl == null)
				{
					base.SelectNextControlInternal(this, true, true, true, true);
					return;
				}
				base.FocusActiveControlInternal();
			}
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000B4B39 File Offset: 0x000B2D39
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (this.IsMdiChild)
			{
				base.UpdateFocusedControl();
			}
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000B4B50 File Offset: 0x000B2D50
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			if (base.DesignMode)
			{
				this.UpdateAutoScaleBaseSize();
			}
			base.OnFontChanged(e);
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000B4B67 File Offset: 0x000B2D67
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			this.formStateEx[Form.FormStateExUseMdiChildProc] = ((this.IsMdiChild && base.Visible) ? 1 : 0);
			base.OnHandleCreated(e);
			this.UpdateLayered();
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000B4B9A File Offset: 0x000B2D9A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			this.formStateEx[Form.FormStateExUseMdiChildProc] = 0;
			Application.OpenFormsInternalRemove(this);
			this.ResetSecurityTip(true);
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000B4BC4 File Offset: 0x000B2DC4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHelpButtonClicked(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Form.EVENT_HELPBUTTONCLICKED];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000B4BF4 File Offset: 0x000B2DF4
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (this.AutoSize)
			{
				Size preferredSize = base.PreferredSize;
				this.minAutoSize = preferredSize;
				Size size = (this.AutoSizeMode == AutoSizeMode.GrowAndShrink) ? preferredSize : LayoutUtils.UnionSizes(preferredSize, this.Size);
				if (this != null)
				{
					((IArrangedElement)this).SetBounds(new Rectangle(base.Left, base.Top, size.Width, size.Height), BoundsSpecified.None);
				}
			}
			base.OnLayout(levent);
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000B4C64 File Offset: 0x000B2E64
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLoad(EventArgs e)
		{
			Application.OpenFormsInternalAdd(this);
			if (Application.UseWaitCursor)
			{
				base.UseWaitCursor = true;
			}
			if (this.formState[Form.FormStateAutoScaling] == 1 && !base.DesignMode)
			{
				this.formState[Form.FormStateAutoScaling] = 0;
				this.ApplyAutoScaling();
			}
			if (base.GetState(32))
			{
				FormStartPosition formStartPosition = (FormStartPosition)this.formState[Form.FormStateStartPos];
				if (formStartPosition == FormStartPosition.CenterParent)
				{
					this.CenterToParent();
				}
				else if (formStartPosition == FormStartPosition.CenterScreen)
				{
					this.CenterToScreen();
				}
			}
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_LOAD];
			if (eventHandler != null)
			{
				string text = this.Text;
				eventHandler(this, e);
				foreach (object obj in base.Controls)
				{
					Control control = (Control)obj;
					control.Invalidate();
				}
			}
			if (base.IsHandleCreated)
			{
				base.BeginInvoke(new MethodInvoker(this.CallShownEvent));
			}
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000B4D7C File Offset: 0x000B2F7C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMaximizedBoundsChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Form.EVENT_MAXIMIZEDBOUNDSCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000B4DAC File Offset: 0x000B2FAC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMaximumSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Form.EVENT_MAXIMUMSIZECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000B4DDC File Offset: 0x000B2FDC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMinimumSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Form.EVENT_MINIMUMSIZECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000B4E0C File Offset: 0x000B300C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnInputLanguageChanged(InputLanguageChangedEventArgs e)
		{
			InputLanguageChangedEventHandler inputLanguageChangedEventHandler = (InputLanguageChangedEventHandler)base.Events[Form.EVENT_INPUTLANGCHANGE];
			if (inputLanguageChangedEventHandler != null)
			{
				inputLanguageChangedEventHandler(this, e);
			}
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000B4E3C File Offset: 0x000B303C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnInputLanguageChanging(InputLanguageChangingEventArgs e)
		{
			InputLanguageChangingEventHandler inputLanguageChangingEventHandler = (InputLanguageChangingEventHandler)base.Events[Form.EVENT_INPUTLANGCHANGEREQUEST];
			if (inputLanguageChangingEventHandler != null)
			{
				inputLanguageChangingEventHandler(this, e);
			}
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000B4E6C File Offset: 0x000B306C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			this.UpdateRenderSizeGrip();
			Form mdiParentInternal = this.MdiParentInternal;
			if (mdiParentInternal != null)
			{
				mdiParentInternal.UpdateMdiWindowListStrip();
			}
			base.OnVisibleChanged(e);
			bool flag = false;
			if (base.IsHandleCreated && base.Visible && this.AcceptButton != null && UnsafeNativeMethods.SystemParametersInfo(95, 0, ref flag, 0) && flag)
			{
				Control control = this.AcceptButton as Control;
				NativeMethods.POINT point = new NativeMethods.POINT(control.Left + control.Width / 2, control.Top + control.Height / 2);
				UnsafeNativeMethods.ClientToScreen(new HandleRef(this, base.Handle), point);
				if (!control.IsWindowObscured)
				{
					IntSecurity.AdjustCursorPosition.Assert();
					try
					{
						Cursor.Position = new Point(point.x, point.y);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000B4F48 File Offset: 0x000B3148
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMdiChildActivate(EventArgs e)
		{
			this.UpdateMenuHandles();
			this.UpdateToolStrip();
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_MDI_CHILD_ACTIVATE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000B4F84 File Offset: 0x000B3184
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMenuStart(EventArgs e)
		{
			Form.SecurityToolTip securityToolTip = (Form.SecurityToolTip)base.Properties.GetObject(Form.PropSecurityTip);
			if (securityToolTip != null)
			{
				securityToolTip.Pop(true);
			}
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_MENUSTART];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000B4FD4 File Offset: 0x000B31D4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMenuComplete(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_MENUCOMPLETE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000B5004 File Offset: 0x000B3204
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.formState[Form.FormStateRenderSizeGrip] != 0)
			{
				Size clientSize = this.ClientSize;
				if (Application.RenderWithVisualStyles)
				{
					if (this.sizeGripRenderer == null)
					{
						this.sizeGripRenderer = new VisualStyleRenderer(VisualStyleElement.Status.Gripper.Normal);
					}
					this.sizeGripRenderer.DrawBackground(e.Graphics, new Rectangle(clientSize.Width - 16, clientSize.Height - 16, 16, 16));
				}
				else
				{
					ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, clientSize.Width - 16, clientSize.Height - 16, 16, 16);
				}
			}
			if (this.IsMdiContainer)
			{
				e.Graphics.FillRectangle(SystemBrushes.AppWorkspace, base.ClientRectangle);
			}
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000B50C7 File Offset: 0x000B32C7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.formState[Form.FormStateRenderSizeGrip] != 0)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000B50E8 File Offset: 0x000B32E8
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		protected virtual void OnDpiChanged(DpiChangedEventArgs e)
		{
			if (e.DeviceDpiNew != e.DeviceDpiOld)
			{
				CommonProperties.xClearAllPreferredSizeCaches(this);
				DpiChangedEventHandler dpiChangedEventHandler = (DpiChangedEventHandler)base.Events[Form.EVENT_DPI_CHANGED];
				if (dpiChangedEventHandler != null)
				{
					dpiChangedEventHandler(this, e);
				}
				if (!e.Cancel)
				{
					float num = (float)e.DeviceDpiNew / (float)e.DeviceDpiOld;
					base.SuspendAllLayout(this);
					try
					{
						if (DpiHelper.EnableDpiChangedHighDpiImprovements && num < 1f)
						{
							this.MinimumSize = new Size(e.SuggestedRectangle.Width, e.SuggestedRectangle.Height);
						}
						SafeNativeMethods.SetWindowPos(new HandleRef(this, base.HandleInternal), NativeMethods.NullHandleRef, e.SuggestedRectangle.X, e.SuggestedRectangle.Y, e.SuggestedRectangle.Width, e.SuggestedRectangle.Height, 20);
						if (base.AutoScaleMode != AutoScaleMode.Font)
						{
							this.Font = (DpiHelper.EnableDpiChangedHighDpiImprovements ? new Font(this.Font.FontFamily, this.Font.Size * num, this.Font.Style, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont) : new Font(this.Font.FontFamily, this.Font.Size * num, this.Font.Style));
							base.FormDpiChanged(num);
						}
						else
						{
							base.ScaleFont(num);
							base.FormDpiChanged(num);
						}
					}
					finally
					{
						base.ResumeAllLayout(this, DpiHelper.EnableDpiChangedHighDpiImprovements);
					}
				}
			}
		}

		// Token: 0x140001B4 RID: 436
		// (add) Token: 0x06002711 RID: 10001 RVA: 0x000B52A8 File Offset: 0x000B34A8
		// (remove) Token: 0x06002712 RID: 10002 RVA: 0x000B52BB File Offset: 0x000B34BB
		[SRCategory("CatLayout")]
		[SRDescription("FormOnDpiChangedDescr")]
		public event DpiChangedEventHandler DpiChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_DPI_CHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_DPI_CHANGED, value);
			}
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000B52D0 File Offset: 0x000B34D0
		private void WmDpiChanged(ref Message m)
		{
			this.DefWndProc(ref m);
			DpiChangedEventArgs dpiChangedEventArgs = new DpiChangedEventArgs(this.deviceDpi, m);
			this.deviceDpi = dpiChangedEventArgs.DeviceDpiNew;
			this.OnDpiChanged(dpiChangedEventArgs);
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x00011A20 File Offset: 0x0000FC20
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual bool OnGetDpiScaledSize(int deviceDpiOld, int deviceDpiNew, ref Size desiredSize)
		{
			return false;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000B530C File Offset: 0x000B350C
		private void WmGetDpiScaledSize(ref Message m)
		{
			this.DefWndProc(ref m);
			Size size = default(Size);
			if (this.OnGetDpiScaledSize(this.deviceDpi, NativeMethods.Util.SignedLOWORD(m.WParam), ref size))
			{
				m.Result = (IntPtr)((this.Size.Height & 65535) << 16 | (this.Size.Width & 65535));
				return;
			}
			m.Result = IntPtr.Zero;
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000B5388 File Offset: 0x000B3588
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			if (base.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				base.RecreateHandle();
			}
			EventHandler eventHandler = base.Events[Form.EVENT_RIGHTTOLEFTLAYOUTCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				foreach (object obj in base.Controls)
				{
					Control control = (Control)obj;
					control.RecreateHandleCore();
				}
			}
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000B5424 File Offset: 0x000B3624
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnShown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_SHOWN];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000B5454 File Offset: 0x000B3654
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			int num = (this.Text.Length == 0) ? 1 : 0;
			if (!this.ControlBox && this.formState[Form.FormStateIsTextEmpty] != num)
			{
				base.RecreateHandle();
			}
			this.formState[Form.FormStateIsTextEmpty] = num;
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000B54AC File Offset: 0x000B36AC
		internal void PerformOnInputLanguageChanged(InputLanguageChangedEventArgs iplevent)
		{
			this.OnInputLanguageChanged(iplevent);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000B54B5 File Offset: 0x000B36B5
		internal void PerformOnInputLanguageChanging(InputLanguageChangingEventArgs iplcevent)
		{
			this.OnInputLanguageChanging(iplcevent);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000B54C0 File Offset: 0x000B36C0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (base.ProcessCmdKey(ref msg, keyData))
			{
				return true;
			}
			MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropCurMenu);
			if (mainMenu != null && mainMenu.ProcessCmdKey(ref msg, keyData))
			{
				return true;
			}
			bool result = false;
			NativeMethods.MSG msg2 = default(NativeMethods.MSG);
			msg2.message = msg.Msg;
			msg2.wParam = msg.WParam;
			msg2.lParam = msg.LParam;
			msg2.hwnd = msg.HWnd;
			if (this.ctlClient != null && this.ctlClient.Handle != IntPtr.Zero && UnsafeNativeMethods.TranslateMDISysAccel(this.ctlClient.Handle, ref msg2))
			{
				result = true;
			}
			msg.Msg = msg2.message;
			msg.WParam = msg2.wParam;
			msg.LParam = msg2.lParam;
			msg.HWnd = msg2.hwnd;
			return result;
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x000B55A4 File Offset: 0x000B37A4
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys != Keys.Return)
				{
					if (keys == Keys.Escape)
					{
						IButtonControl buttonControl = (IButtonControl)base.Properties.GetObject(Form.PropCancelButton);
						if (buttonControl != null)
						{
							buttonControl.PerformClick();
							return true;
						}
					}
				}
				else
				{
					IButtonControl buttonControl = (IButtonControl)base.Properties.GetObject(Form.PropDefaultButton);
					if (buttonControl != null)
					{
						if (buttonControl is Control)
						{
							buttonControl.PerformClick();
						}
						return true;
					}
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000B5620 File Offset: 0x000B3820
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogChar(char charCode)
		{
			if (this.IsMdiChild && charCode != ' ')
			{
				if (this.ProcessMnemonic(charCode))
				{
					return true;
				}
				this.formStateEx[Form.FormStateExMnemonicProcessed] = 1;
				try
				{
					return base.ProcessDialogChar(charCode);
				}
				finally
				{
					this.formStateEx[Form.FormStateExMnemonicProcessed] = 0;
				}
			}
			return base.ProcessDialogChar(charCode);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000B568C File Offset: 0x000B388C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessKeyPreview(ref Message m)
		{
			return (this.formState[Form.FormStateKeyPreview] != 0 && this.ProcessKeyEventArgs(ref m)) || base.ProcessKeyPreview(ref m);
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000B56B4 File Offset: 0x000B38B4
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessTabKey(bool forward)
		{
			if (base.SelectNextControl(base.ActiveControl, forward, true, true, true))
			{
				return true;
			}
			if (this.IsMdiChild || base.ParentFormInternal == null)
			{
				bool flag = base.SelectNextControl(null, forward, true, true, false);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000B56F8 File Offset: 0x000B38F8
		internal void RaiseFormClosedOnAppExit()
		{
			if (!this.Modal)
			{
				int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				if (integer > 0)
				{
					Form[] ownedForms = this.OwnedForms;
					FormClosedEventArgs e = new FormClosedEventArgs(CloseReason.FormOwnerClosing);
					for (int i = integer - 1; i >= 0; i--)
					{
						if (ownedForms[i] != null && !Application.OpenFormsInternal.Contains(ownedForms[i]))
						{
							ownedForms[i].OnFormClosed(e);
						}
					}
				}
			}
			this.OnFormClosed(new FormClosedEventArgs(CloseReason.ApplicationExitCall));
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000B5768 File Offset: 0x000B3968
		internal bool RaiseFormClosingOnAppExit()
		{
			FormClosingEventArgs formClosingEventArgs = new FormClosingEventArgs(CloseReason.ApplicationExitCall, false);
			if (!this.Modal)
			{
				int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				if (integer > 0)
				{
					Form[] ownedForms = this.OwnedForms;
					FormClosingEventArgs formClosingEventArgs2 = new FormClosingEventArgs(CloseReason.FormOwnerClosing, false);
					for (int i = integer - 1; i >= 0; i--)
					{
						if (ownedForms[i] != null && !Application.OpenFormsInternal.Contains(ownedForms[i]))
						{
							ownedForms[i].OnFormClosing(formClosingEventArgs2);
							if (formClosingEventArgs2.Cancel)
							{
								formClosingEventArgs.Cancel = true;
								break;
							}
						}
					}
				}
			}
			this.OnFormClosing(formClosingEventArgs);
			return formClosingEventArgs.Cancel;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000B57FC File Offset: 0x000B39FC
		internal override void RecreateHandleCore()
		{
			NativeMethods.WINDOWPLACEMENT windowplacement = default(NativeMethods.WINDOWPLACEMENT);
			FormStartPosition formStartPosition = FormStartPosition.Manual;
			if (!this.IsMdiChild && (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized))
			{
				windowplacement.length = Marshal.SizeOf(typeof(NativeMethods.WINDOWPLACEMENT));
				UnsafeNativeMethods.GetWindowPlacement(new HandleRef(this, base.Handle), ref windowplacement);
			}
			if (this.StartPosition != FormStartPosition.Manual)
			{
				formStartPosition = this.StartPosition;
				this.StartPosition = FormStartPosition.Manual;
			}
			Form.EnumThreadWindowsCallback enumThreadWindowsCallback = null;
			SafeNativeMethods.EnumThreadWindowsCallback enumThreadWindowsCallback2 = null;
			if (base.IsHandleCreated)
			{
				enumThreadWindowsCallback = new Form.EnumThreadWindowsCallback();
				if (enumThreadWindowsCallback != null)
				{
					enumThreadWindowsCallback2 = new SafeNativeMethods.EnumThreadWindowsCallback(enumThreadWindowsCallback.Callback);
					UnsafeNativeMethods.EnumThreadWindows(SafeNativeMethods.GetCurrentThreadId(), new NativeMethods.EnumThreadWindowsCallback(enumThreadWindowsCallback2.Invoke), new HandleRef(this, base.Handle));
					enumThreadWindowsCallback.ResetOwners();
				}
			}
			base.RecreateHandleCore();
			if (enumThreadWindowsCallback != null)
			{
				enumThreadWindowsCallback.SetOwners(new HandleRef(this, base.Handle));
			}
			if (formStartPosition != FormStartPosition.Manual)
			{
				this.StartPosition = formStartPosition;
			}
			if (windowplacement.length > 0)
			{
				UnsafeNativeMethods.SetWindowPlacement(new HandleRef(this, base.Handle), ref windowplacement);
			}
			if (enumThreadWindowsCallback2 != null)
			{
				GC.KeepAlive(enumThreadWindowsCallback2);
			}
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000B5904 File Offset: 0x000B3B04
		public void RemoveOwnedForm(Form ownedForm)
		{
			if (ownedForm == null)
			{
				return;
			}
			if (ownedForm.OwnerInternal != null)
			{
				ownedForm.Owner = null;
				return;
			}
			Form[] array = (Form[])base.Properties.GetObject(Form.PropOwnedForms);
			int num = base.Properties.GetInteger(Form.PropOwnedFormsCount);
			if (array != null)
			{
				for (int i = 0; i < num; i++)
				{
					if (ownedForm.Equals(array[i]))
					{
						array[i] = null;
						if (i + 1 < num)
						{
							Array.Copy(array, i + 1, array, i, num - i - 1);
							array[num - 1] = null;
						}
						num--;
					}
				}
				base.Properties.SetInteger(Form.PropOwnedFormsCount, num);
			}
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000B599B File Offset: 0x000B3B9B
		private void ResetIcon()
		{
			this.icon = null;
			if (this.smallIcon != null)
			{
				this.smallIcon.Dispose();
				this.smallIcon = null;
			}
			this.formState[Form.FormStateIconSet] = 0;
			this.UpdateWindowIcon(true);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000B59D8 File Offset: 0x000B3BD8
		private void ResetSecurityTip(bool modalOnly)
		{
			Form.SecurityToolTip securityToolTip = (Form.SecurityToolTip)base.Properties.GetObject(Form.PropSecurityTip);
			if (securityToolTip != null && ((modalOnly && securityToolTip.Modal) || !modalOnly))
			{
				securityToolTip.Dispose();
				base.Properties.SetObject(Form.PropSecurityTip, null);
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000B5A25 File Offset: 0x000B3C25
		private void ResetTransparencyKey()
		{
			this.TransparencyKey = Color.Empty;
		}

		// Token: 0x140001B5 RID: 437
		// (add) Token: 0x06002727 RID: 10023 RVA: 0x000B5A32 File Offset: 0x000B3C32
		// (remove) Token: 0x06002728 RID: 10024 RVA: 0x000B5A45 File Offset: 0x000B3C45
		[SRCategory("CatAction")]
		[SRDescription("FormOnResizeBeginDescr")]
		public event EventHandler ResizeBegin
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_RESIZEBEGIN, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_RESIZEBEGIN, value);
			}
		}

		// Token: 0x140001B6 RID: 438
		// (add) Token: 0x06002729 RID: 10025 RVA: 0x000B5A58 File Offset: 0x000B3C58
		// (remove) Token: 0x0600272A RID: 10026 RVA: 0x000B5A6B File Offset: 0x000B3C6B
		[SRCategory("CatAction")]
		[SRDescription("FormOnResizeEndDescr")]
		public event EventHandler ResizeEnd
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_RESIZEEND, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_RESIZEEND, value);
			}
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000B5A7E File Offset: 0x000B3C7E
		private void ResumeLayoutFromMinimize()
		{
			if (this.formState[Form.FormStateWindowState] == 1)
			{
				base.ResumeLayout();
			}
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000B5A9C File Offset: 0x000B3C9C
		private void RestoreWindowBoundsIfNecessary()
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				Size size = this.restoredWindowBounds.Size;
				if ((this.restoredWindowBoundsSpecified & BoundsSpecified.Size) != BoundsSpecified.None)
				{
					size = base.SizeFromClientSize(size.Width, size.Height);
				}
				base.SetBounds(this.restoredWindowBounds.X, this.restoredWindowBounds.Y, (this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] == 1) ? size.Width : this.restoredWindowBounds.Width, (this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] == 1) ? size.Height : this.restoredWindowBounds.Height, this.restoredWindowBoundsSpecified);
				this.restoredWindowBoundsSpecified = BoundsSpecified.None;
				this.restoredWindowBounds = new Rectangle(-1, -1, -1, -1);
				this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] = 0;
				this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] = 0;
			}
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000B5B88 File Offset: 0x000B3D88
		private void RestrictedProcessNcActivate()
		{
			if (base.IsDisposed || base.Disposing)
			{
				return;
			}
			Form.SecurityToolTip securityToolTip = (Form.SecurityToolTip)base.Properties.GetObject(Form.PropSecurityTip);
			if (securityToolTip == null)
			{
				if (base.IsHandleCreated && UnsafeNativeMethods.GetForegroundWindow() == base.Handle)
				{
					securityToolTip = new Form.SecurityToolTip(this);
					base.Properties.SetObject(Form.PropSecurityTip, securityToolTip);
					return;
				}
			}
			else
			{
				if (!base.IsHandleCreated || UnsafeNativeMethods.GetForegroundWindow() != base.Handle)
				{
					securityToolTip.Pop(false);
					return;
				}
				securityToolTip.Show();
			}
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000B5C1C File Offset: 0x000B3E1C
		private void ResumeUpdateMenuHandles()
		{
			int num = this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount];
			if (num <= 0)
			{
				throw new InvalidOperationException(SR.GetString("TooManyResumeUpdateMenuHandles"));
			}
			if ((this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount] = num - 1) == 0 && this.formStateEx[Form.FormStateExUpdateMenuHandlesDeferred] != 0)
			{
				this.UpdateMenuHandles();
			}
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000B5C7E File Offset: 0x000B3E7E
		protected override void Select(bool directed, bool forward)
		{
			IntSecurity.ModifyFocus.Demand();
			this.SelectInternal(directed, forward);
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000B5C94 File Offset: 0x000B3E94
		private void SelectInternal(bool directed, bool forward)
		{
			IntSecurity.ModifyFocus.Assert();
			if (directed)
			{
				base.SelectNextControl(null, forward, true, true, false);
			}
			if (this.TopLevel)
			{
				UnsafeNativeMethods.SetActiveWindow(new HandleRef(this, base.Handle));
				return;
			}
			if (this.IsMdiChild)
			{
				UnsafeNativeMethods.SetActiveWindow(new HandleRef(this.MdiParentInternal, this.MdiParentInternal.Handle));
				this.MdiParentInternal.MdiClient.SendMessage(546, base.Handle, 0);
				return;
			}
			Form parentFormInternal = base.ParentFormInternal;
			if (parentFormInternal != null)
			{
				parentFormInternal.ActiveControl = this;
			}
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000B5D28 File Offset: 0x000B3F28
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float x, float y)
		{
			base.SuspendLayout();
			try
			{
				if (this.WindowState == FormWindowState.Normal)
				{
					Size clientSize = this.ClientSize;
					Size minimumSize = this.MinimumSize;
					Size maximumSize = this.MaximumSize;
					if (!this.MinimumSize.IsEmpty)
					{
						this.MinimumSize = base.ScaleSize(minimumSize, x, y);
					}
					if (!this.MaximumSize.IsEmpty)
					{
						this.MaximumSize = base.ScaleSize(maximumSize, x, y);
					}
					this.ClientSize = base.ScaleSize(clientSize, x, y);
				}
				base.ScaleDockPadding(x, y);
				foreach (object obj in base.Controls)
				{
					Control control = (Control)obj;
					if (control != null)
					{
						control.Scale(x, y);
					}
				}
			}
			finally
			{
				base.ResumeLayout();
			}
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000B5E1C File Offset: 0x000B401C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if (this.WindowState != FormWindowState.Normal)
			{
				bounds = this.RestoreBounds;
			}
			return base.GetScaledBounds(bounds, factor, specified);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000B5E38 File Offset: 0x000B4038
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			this.formStateEx[Form.FormStateExInScale] = 1;
			try
			{
				if (this.MdiParentInternal != null)
				{
					specified &= ~(BoundsSpecified.X | BoundsSpecified.Y);
				}
				base.ScaleControl(factor, specified);
			}
			finally
			{
				this.formStateEx[Form.FormStateExInScale] = 0;
			}
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000B5E90 File Offset: 0x000B4090
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.WindowState != FormWindowState.Normal)
			{
				if (x != -1 || y != -1)
				{
					this.restoredWindowBoundsSpecified |= (specified & BoundsSpecified.Location);
				}
				this.restoredWindowBoundsSpecified |= (specified & BoundsSpecified.Size);
				if ((specified & BoundsSpecified.X) != BoundsSpecified.None)
				{
					this.restoredWindowBounds.X = x;
				}
				if ((specified & BoundsSpecified.Y) != BoundsSpecified.None)
				{
					this.restoredWindowBounds.Y = y;
				}
				if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
				{
					this.restoredWindowBounds.Width = width;
					this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] = 0;
				}
				if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
				{
					this.restoredWindowBounds.Height = height;
					this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] = 0;
				}
			}
			if ((specified & BoundsSpecified.X) != BoundsSpecified.None)
			{
				this.restoreBounds.X = x;
			}
			if ((specified & BoundsSpecified.Y) != BoundsSpecified.None)
			{
				this.restoreBounds.Y = y;
			}
			if ((specified & BoundsSpecified.Width) != BoundsSpecified.None || this.restoreBounds.Width == -1)
			{
				this.restoreBounds.Width = width;
			}
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None || this.restoreBounds.Height == -1)
			{
				this.restoreBounds.Height = height;
			}
			if (this.WindowState == FormWindowState.Normal && (base.Height != height || base.Width != width))
			{
				Size maxWindowTrackSize = SystemInformation.MaxWindowTrackSize;
				if (height > maxWindowTrackSize.Height)
				{
					height = maxWindowTrackSize.Height;
				}
				if (width > maxWindowTrackSize.Width)
				{
					width = maxWindowTrackSize.Width;
				}
			}
			FormBorderStyle formBorderStyle = this.FormBorderStyle;
			if (formBorderStyle != FormBorderStyle.None && formBorderStyle != FormBorderStyle.FixedToolWindow && formBorderStyle != FormBorderStyle.SizableToolWindow && this.ParentInternal == null)
			{
				Size minWindowTrackSize = SystemInformation.MinWindowTrackSize;
				if (height < minWindowTrackSize.Height)
				{
					height = minWindowTrackSize.Height;
				}
				if (width < minWindowTrackSize.Width)
				{
					width = minWindowTrackSize.Width;
				}
			}
			if (this.IsRestrictedWindow)
			{
				Rectangle left = this.ApplyBoundsConstraints(x, y, width, height);
				if (left != new Rectangle(x, y, width, height))
				{
					base.SetBoundsCore(left.X, left.Y, left.Width, left.Height, BoundsSpecified.All);
					return;
				}
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000B608C File Offset: 0x000B428C
		internal override Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			Rectangle rectangle = base.ApplyBoundsConstraints(suggestedX, suggestedY, proposedWidth, proposedHeight);
			if (this.IsRestrictedWindow)
			{
				Screen[] allScreens = Screen.AllScreens;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				for (int i = 0; i < allScreens.Length; i++)
				{
					Rectangle workingArea = allScreens[i].WorkingArea;
					if (workingArea.Contains(suggestedX, suggestedY))
					{
						flag = true;
					}
					if (workingArea.Contains(suggestedX + proposedWidth, suggestedY))
					{
						flag2 = true;
					}
					if (workingArea.Contains(suggestedX, suggestedY + proposedHeight))
					{
						flag3 = true;
					}
					if (workingArea.Contains(suggestedX + proposedWidth, suggestedY + proposedHeight))
					{
						flag4 = true;
					}
				}
				if (!flag || !flag2 || !flag3 || !flag4)
				{
					if (this.formStateEx[Form.FormStateExInScale] == 1)
					{
						rectangle = WindowsFormsUtils.ConstrainToScreenWorkingAreaBounds(rectangle);
					}
					else
					{
						rectangle.X = base.Left;
						rectangle.Y = base.Top;
						rectangle.Width = base.Width;
						rectangle.Height = base.Height;
					}
				}
			}
			return rectangle;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000B617C File Offset: 0x000B437C
		private void SetDefaultButton(IButtonControl button)
		{
			IButtonControl buttonControl = (IButtonControl)base.Properties.GetObject(Form.PropDefaultButton);
			if (buttonControl != button)
			{
				if (buttonControl != null)
				{
					buttonControl.NotifyDefault(false);
				}
				base.Properties.SetObject(Form.PropDefaultButton, button);
				if (button != null)
				{
					button.NotifyDefault(true);
				}
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000B61C8 File Offset: 0x000B43C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetClientSizeCore(int x, int y)
		{
			bool hscroll = base.HScroll;
			bool vscroll = base.VScroll;
			base.SetClientSizeCore(x, y);
			if (base.IsHandleCreated)
			{
				if (base.VScroll != vscroll && base.VScroll)
				{
					x += SystemInformation.VerticalScrollBarWidth;
				}
				if (base.HScroll != hscroll && base.HScroll)
				{
					y += SystemInformation.HorizontalScrollBarHeight;
				}
				if (x != this.ClientSize.Width || y != this.ClientSize.Height)
				{
					base.SetClientSizeCore(x, y);
				}
			}
			this.formState[Form.FormStateSetClientSize] = 1;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000B6264 File Offset: 0x000B4464
		public void SetDesktopBounds(int x, int y, int width, int height)
		{
			Rectangle workingArea = SystemInformation.WorkingArea;
			base.SetBounds(x + workingArea.X, y + workingArea.Y, width, height, BoundsSpecified.All);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000B6294 File Offset: 0x000B4494
		public void SetDesktopLocation(int x, int y)
		{
			Rectangle workingArea = SystemInformation.WorkingArea;
			this.Location = new Point(workingArea.X + x, workingArea.Y + y);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000B62C4 File Offset: 0x000B44C4
		public void Show(IWin32Window owner)
		{
			if (owner == this)
			{
				throw new InvalidOperationException(SR.GetString("OwnsSelfOrOwner", new object[]
				{
					"Show"
				}));
			}
			if (base.Visible)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnVisible", new object[]
				{
					"Show"
				}));
			}
			if (!base.Enabled)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnDisabled", new object[]
				{
					"Show"
				}));
			}
			if (!this.TopLevel)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnNonTopLevel", new object[]
				{
					"Show"
				}));
			}
			if (!SystemInformation.UserInteractive)
			{
				throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
			}
			if (owner != null && ((int)UnsafeNativeMethods.GetWindowLong(new HandleRef(owner, Control.GetSafeHandle(owner)), -20) & 8) == 0 && owner is Control)
			{
				owner = ((Control)owner).TopLevelControlInternal;
			}
			IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
			IntPtr intPtr = (owner == null) ? activeWindow : Control.GetSafeHandle(owner);
			IntPtr intPtr2 = IntPtr.Zero;
			base.Properties.SetObject(Form.PropDialogOwner, owner);
			Form ownerInternal = this.OwnerInternal;
			if (owner is Form && owner != ownerInternal)
			{
				this.Owner = (Form)owner;
			}
			if (intPtr != IntPtr.Zero && intPtr != base.Handle)
			{
				if (UnsafeNativeMethods.GetWindowLong(new HandleRef(owner, intPtr), -8) == base.Handle)
				{
					throw new ArgumentException(SR.GetString("OwnsSelfOrOwner", new object[]
					{
						"show"
					}), "owner");
				}
				intPtr2 = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -8);
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, new HandleRef(owner, intPtr));
			}
			base.Visible = true;
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000B6488 File Offset: 0x000B4688
		public DialogResult ShowDialog()
		{
			return this.ShowDialog(null);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000B6494 File Offset: 0x000B4694
		public DialogResult ShowDialog(IWin32Window owner)
		{
			if (owner == this)
			{
				throw new ArgumentException(SR.GetString("OwnsSelfOrOwner", new object[]
				{
					"showDialog"
				}), "owner");
			}
			if (base.Visible)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnVisible", new object[]
				{
					"showDialog"
				}));
			}
			if (!base.Enabled)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnDisabled", new object[]
				{
					"showDialog"
				}));
			}
			if (!this.TopLevel)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnNonTopLevel", new object[]
				{
					"showDialog"
				}));
			}
			if (this.Modal)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnModal", new object[]
				{
					"showDialog"
				}));
			}
			if (!SystemInformation.UserInteractive)
			{
				throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
			}
			if (owner != null && ((int)UnsafeNativeMethods.GetWindowLong(new HandleRef(owner, Control.GetSafeHandle(owner)), -20) & 8) == 0 && owner is Control)
			{
				owner = ((Control)owner).TopLevelControlInternal;
			}
			this.CalledOnLoad = false;
			this.CalledMakeVisible = false;
			this.CloseReason = CloseReason.None;
			IntPtr capture = UnsafeNativeMethods.GetCapture();
			if (capture != IntPtr.Zero)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(null, capture), 31, IntPtr.Zero, IntPtr.Zero);
				SafeNativeMethods.ReleaseCapture();
			}
			IntPtr intPtr = UnsafeNativeMethods.GetActiveWindow();
			IntPtr intPtr2 = (owner == null) ? intPtr : Control.GetSafeHandle(owner);
			IntPtr intPtr3 = IntPtr.Zero;
			base.Properties.SetObject(Form.PropDialogOwner, owner);
			Form ownerInternal = this.OwnerInternal;
			if (owner is Form && owner != ownerInternal)
			{
				this.Owner = (Form)owner;
			}
			try
			{
				base.SetState(32, true);
				this.dialogResult = DialogResult.None;
				base.CreateControl();
				if (intPtr2 != IntPtr.Zero && intPtr2 != base.Handle)
				{
					if (UnsafeNativeMethods.GetWindowLong(new HandleRef(owner, intPtr2), -8) == base.Handle)
					{
						throw new ArgumentException(SR.GetString("OwnsSelfOrOwner", new object[]
						{
							"showDialog"
						}), "owner");
					}
					intPtr3 = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -8);
					UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, new HandleRef(owner, intPtr2));
				}
				try
				{
					if (this.dialogResult == DialogResult.None)
					{
						Application.RunDialog(this);
					}
				}
				finally
				{
					if (!UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr)))
					{
						intPtr = intPtr2;
					}
					if (UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr)) && SafeNativeMethods.IsWindowVisible(new HandleRef(null, intPtr)))
					{
						UnsafeNativeMethods.SetActiveWindow(new HandleRef(null, intPtr));
					}
					else if (UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr2)) && SafeNativeMethods.IsWindowVisible(new HandleRef(null, intPtr2)))
					{
						UnsafeNativeMethods.SetActiveWindow(new HandleRef(null, intPtr2));
					}
					this.SetVisibleCore(false);
					if (base.IsHandleCreated)
					{
						if (this.OwnerInternal != null && this.OwnerInternal.IsMdiContainer)
						{
							this.OwnerInternal.Invalidate(true);
							this.OwnerInternal.Update();
						}
						this.DestroyHandle();
					}
					base.SetState(32, false);
				}
			}
			finally
			{
				this.Owner = ownerInternal;
				base.Properties.SetObject(Form.PropDialogOwner, null);
			}
			return this.DialogResult;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000B0BD8 File Offset: 0x000AEDD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeAutoScaleBaseSize()
		{
			return this.formState[Form.FormStateAutoScaling] != 0;
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x00013062 File Offset: 0x00011262
		private bool ShouldSerializeClientSize()
		{
			return true;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000B67F4 File Offset: 0x000B49F4
		private bool ShouldSerializeIcon()
		{
			return this.formState[Form.FormStateIconSet] == 1;
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000B6809 File Offset: 0x000B4A09
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeLocation()
		{
			return base.Left != 0 || base.Top != 0;
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x00011A20 File Offset: 0x0000FC20
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal override bool ShouldSerializeSize()
		{
			return false;
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000B6820 File Offset: 0x000B4A20
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal bool ShouldSerializeTransparencyKey()
		{
			return !this.TransparencyKey.Equals(Color.Empty);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000B684E File Offset: 0x000B4A4E
		private void SuspendLayoutForMinimize()
		{
			if (this.formState[Form.FormStateWindowState] != 1)
			{
				base.SuspendLayout();
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000B686C File Offset: 0x000B4A6C
		private void SuspendUpdateMenuHandles()
		{
			int num = this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount];
			this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount] = num + 1;
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000B68A0 File Offset: 0x000B4AA0
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", Text: " + this.Text;
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000B68C5 File Offset: 0x000B4AC5
		private void UpdateAutoScaleBaseSize()
		{
			this.autoScaleBaseSize = Size.Empty;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000B68D4 File Offset: 0x000B4AD4
		private void UpdateRenderSizeGrip()
		{
			int num = this.formState[Form.FormStateRenderSizeGrip];
			switch (this.FormBorderStyle)
			{
			case FormBorderStyle.None:
			case FormBorderStyle.FixedSingle:
			case FormBorderStyle.Fixed3D:
			case FormBorderStyle.FixedDialog:
			case FormBorderStyle.FixedToolWindow:
				this.formState[Form.FormStateRenderSizeGrip] = 0;
				break;
			case FormBorderStyle.Sizable:
			case FormBorderStyle.SizableToolWindow:
				switch (this.SizeGripStyle)
				{
				case SizeGripStyle.Auto:
					if (base.GetState(32))
					{
						this.formState[Form.FormStateRenderSizeGrip] = 1;
					}
					else
					{
						this.formState[Form.FormStateRenderSizeGrip] = 0;
					}
					break;
				case SizeGripStyle.Show:
					this.formState[Form.FormStateRenderSizeGrip] = 1;
					break;
				case SizeGripStyle.Hide:
					this.formState[Form.FormStateRenderSizeGrip] = 0;
					break;
				}
				break;
			}
			if (this.formState[Form.FormStateRenderSizeGrip] != num)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000B69BC File Offset: 0x000B4BBC
		protected override void UpdateDefaultButton()
		{
			ContainerControl containerControl = this;
			while (containerControl.ActiveControl is ContainerControl)
			{
				containerControl = (containerControl.ActiveControl as ContainerControl);
				if (containerControl is Form)
				{
					containerControl = this;
					break;
				}
			}
			if (containerControl.ActiveControl is IButtonControl)
			{
				this.SetDefaultButton((IButtonControl)containerControl.ActiveControl);
				return;
			}
			this.SetDefaultButton(this.AcceptButton);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000B6A20 File Offset: 0x000B4C20
		private void UpdateHandleWithOwner()
		{
			if (base.IsHandleCreated && this.TopLevel)
			{
				HandleRef dwNewLong = NativeMethods.NullHandleRef;
				Form form = (Form)base.Properties.GetObject(Form.PropOwner);
				if (form != null)
				{
					dwNewLong = new HandleRef(form, form.Handle);
				}
				else if (!this.ShowInTaskbar)
				{
					dwNewLong = this.TaskbarOwner;
				}
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, dwNewLong);
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000B6A90 File Offset: 0x000B4C90
		private void UpdateLayered()
		{
			if (this.formState[Form.FormStateLayered] != 0 && base.IsHandleCreated && this.TopLevel && OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
			{
				Color transparencyKey = this.TransparencyKey;
				bool flag;
				if (transparencyKey.IsEmpty)
				{
					flag = UnsafeNativeMethods.SetLayeredWindowAttributes(new HandleRef(this, base.Handle), 0, this.OpacityAsByte, 2);
				}
				else if (this.OpacityAsByte == 255)
				{
					flag = UnsafeNativeMethods.SetLayeredWindowAttributes(new HandleRef(this, base.Handle), ColorTranslator.ToWin32(transparencyKey), 0, 1);
				}
				else
				{
					flag = UnsafeNativeMethods.SetLayeredWindowAttributes(new HandleRef(this, base.Handle), ColorTranslator.ToWin32(transparencyKey), this.OpacityAsByte, 3);
				}
				if (!flag)
				{
					throw new Win32Exception();
				}
			}
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000B6B58 File Offset: 0x000B4D58
		private void UpdateMenuHandles()
		{
			if (base.Properties.GetObject(Form.PropCurMenu) != null)
			{
				base.Properties.SetObject(Form.PropCurMenu, null);
			}
			if (base.IsHandleCreated)
			{
				if (!this.TopLevel)
				{
					this.UpdateMenuHandles(null, true);
					return;
				}
				Form activeMdiChildInternal = this.ActiveMdiChildInternal;
				if (activeMdiChildInternal != null)
				{
					this.UpdateMenuHandles(activeMdiChildInternal.MergedMenuPrivate, true);
					return;
				}
				this.UpdateMenuHandles(this.Menu, true);
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000B6BC8 File Offset: 0x000B4DC8
		private void UpdateMenuHandles(MainMenu menu, bool forceRedraw)
		{
			int num = this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount];
			if (num > 0 && menu != null)
			{
				this.formStateEx[Form.FormStateExUpdateMenuHandlesDeferred] = 1;
				return;
			}
			if (menu != null)
			{
				menu.form = this;
			}
			if (menu != null || base.Properties.ContainsObject(Form.PropCurMenu))
			{
				base.Properties.SetObject(Form.PropCurMenu, menu);
			}
			if (this.ctlClient == null || !this.ctlClient.IsHandleCreated)
			{
				if (menu != null)
				{
					UnsafeNativeMethods.SetMenu(new HandleRef(this, base.Handle), new HandleRef(menu, menu.Handle));
				}
				else
				{
					UnsafeNativeMethods.SetMenu(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef);
				}
			}
			else
			{
				MenuStrip mainMenuStrip = this.MainMenuStrip;
				if (mainMenuStrip == null || menu != null)
				{
					MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropDummyMenu);
					if (mainMenu == null)
					{
						mainMenu = new MainMenu();
						mainMenu.ownerForm = this;
						base.Properties.SetObject(Form.PropDummyMenu, mainMenu);
					}
					UnsafeNativeMethods.SendMessage(new HandleRef(this.ctlClient, this.ctlClient.Handle), 560, mainMenu.Handle, IntPtr.Zero);
					if (menu != null)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this.ctlClient, this.ctlClient.Handle), 560, menu.Handle, IntPtr.Zero);
					}
				}
				if (menu == null && mainMenuStrip != null)
				{
					IntPtr menu2 = UnsafeNativeMethods.GetMenu(new HandleRef(this, base.Handle));
					if (menu2 != IntPtr.Zero)
					{
						UnsafeNativeMethods.SetMenu(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef);
						Form activeMdiChildInternal = this.ActiveMdiChildInternal;
						if (activeMdiChildInternal != null && activeMdiChildInternal.WindowState == FormWindowState.Maximized)
						{
							activeMdiChildInternal.RecreateHandle();
						}
						CommonProperties.xClearPreferredSizeCache(this);
					}
				}
			}
			if (forceRedraw)
			{
				SafeNativeMethods.DrawMenuBar(new HandleRef(this, base.Handle));
			}
			this.formStateEx[Form.FormStateExUpdateMenuHandlesDeferred] = 0;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000B6DB4 File Offset: 0x000B4FB4
		internal void UpdateFormStyles()
		{
			Size clientSize = this.ClientSize;
			base.UpdateStyles();
			if (!this.ClientSize.Equals(clientSize))
			{
				this.ClientSize = clientSize;
			}
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000B6DF4 File Offset: 0x000B4FF4
		private static Type FindClosestStockType(Type type)
		{
			Type[] array = new Type[]
			{
				typeof(MenuStrip)
			};
			foreach (Type type2 in array)
			{
				if (type2.IsAssignableFrom(type))
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000B6E38 File Offset: 0x000B5038
		private void UpdateToolStrip()
		{
			ToolStrip mainMenuStrip = this.MainMenuStrip;
			ArrayList arrayList = ToolStripManager.FindMergeableToolStrips(this.ActiveMdiChildInternal);
			if (mainMenuStrip != null)
			{
				ToolStripManager.RevertMerge(mainMenuStrip);
			}
			this.UpdateMdiWindowListStrip();
			if (this.ActiveMdiChildInternal != null)
			{
				foreach (object obj in arrayList)
				{
					ToolStrip toolStrip = (ToolStrip)obj;
					Type left = Form.FindClosestStockType(toolStrip.GetType());
					if (mainMenuStrip != null)
					{
						Type type = Form.FindClosestStockType(mainMenuStrip.GetType());
						if (type != null && left != null && left == type && mainMenuStrip.GetType().IsAssignableFrom(toolStrip.GetType()))
						{
							ToolStripManager.Merge(toolStrip, mainMenuStrip);
							break;
						}
					}
				}
			}
			Form activeMdiChildInternal = this.ActiveMdiChildInternal;
			this.UpdateMdiControlStrip(activeMdiChildInternal != null && activeMdiChildInternal.IsMaximized);
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000B6F2C File Offset: 0x000B512C
		private void UpdateMdiControlStrip(bool maximized)
		{
			if (this.formStateEx[Form.FormStateExInUpdateMdiControlStrip] != 0)
			{
				return;
			}
			this.formStateEx[Form.FormStateExInUpdateMdiControlStrip] = 1;
			try
			{
				MdiControlStrip mdiControlStrip = this.MdiControlStrip;
				if (this.MdiControlStrip != null)
				{
					if (mdiControlStrip.MergedMenu != null)
					{
						ToolStripManager.RevertMergeInternal(mdiControlStrip.MergedMenu, mdiControlStrip, true);
					}
					mdiControlStrip.MergedMenu = null;
					mdiControlStrip.Dispose();
					this.MdiControlStrip = null;
				}
				if (this.ActiveMdiChildInternal != null && maximized && this.ActiveMdiChildInternal.ControlBox && this.Menu == null)
				{
					IntPtr menu = UnsafeNativeMethods.GetMenu(new HandleRef(this, base.Handle));
					if (menu == IntPtr.Zero)
					{
						MenuStrip mainMenuStrip = ToolStripManager.GetMainMenuStrip(this);
						if (mainMenuStrip != null)
						{
							this.MdiControlStrip = new MdiControlStrip(this.ActiveMdiChildInternal);
							ToolStripManager.Merge(this.MdiControlStrip, mainMenuStrip);
							this.MdiControlStrip.MergedMenu = mainMenuStrip;
						}
					}
				}
			}
			finally
			{
				this.formStateEx[Form.FormStateExInUpdateMdiControlStrip] = 0;
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000B7030 File Offset: 0x000B5230
		internal void UpdateMdiWindowListStrip()
		{
			if (this.IsMdiContainer)
			{
				if (this.MdiWindowListStrip != null && this.MdiWindowListStrip.MergedMenu != null)
				{
					ToolStripManager.RevertMergeInternal(this.MdiWindowListStrip.MergedMenu, this.MdiWindowListStrip, true);
				}
				MenuStrip mainMenuStrip = ToolStripManager.GetMainMenuStrip(this);
				if (mainMenuStrip != null && mainMenuStrip.MdiWindowListItem != null)
				{
					if (this.MdiWindowListStrip == null)
					{
						this.MdiWindowListStrip = new MdiWindowListStrip();
					}
					int count = mainMenuStrip.MdiWindowListItem.DropDownItems.Count;
					bool includeSeparator = count > 0 && !(mainMenuStrip.MdiWindowListItem.DropDownItems[count - 1] is ToolStripSeparator);
					this.MdiWindowListStrip.PopulateItems(this, mainMenuStrip.MdiWindowListItem, includeSeparator);
					ToolStripManager.Merge(this.MdiWindowListStrip, mainMenuStrip);
					this.MdiWindowListStrip.MergedMenu = mainMenuStrip;
				}
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000B7100 File Offset: 0x000B5300
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResizeBegin(EventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_RESIZEBEGIN];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000B7138 File Offset: 0x000B5338
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResizeEnd(EventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_RESIZEEND];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000B716E File Offset: 0x000B536E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);
			this.AdjustSystemMenu();
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000B7180 File Offset: 0x000B5380
		private void UpdateWindowIcon(bool redrawFrame)
		{
			if (base.IsHandleCreated)
			{
				Icon icon;
				if ((this.FormBorderStyle == FormBorderStyle.FixedDialog && this.formState[Form.FormStateIconSet] == 0 && !this.IsRestrictedWindow) || !this.ShowIcon)
				{
					icon = null;
				}
				else
				{
					icon = this.Icon;
				}
				if (icon != null)
				{
					if (this.smallIcon == null)
					{
						try
						{
							this.smallIcon = new Icon(icon, SystemInformation.SmallIconSize);
						}
						catch
						{
						}
					}
					if (this.smallIcon != null)
					{
						base.SendMessage(128, 0, this.smallIcon.Handle);
					}
					base.SendMessage(128, 1, icon.Handle);
				}
				else
				{
					base.SendMessage(128, 0, 0);
					base.SendMessage(128, 1, 0);
				}
				if (redrawFrame)
				{
					SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), null, NativeMethods.NullHandleRef, 1025);
				}
			}
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000B7270 File Offset: 0x000B5470
		private void UpdateWindowState()
		{
			if (base.IsHandleCreated)
			{
				FormWindowState windowState = this.WindowState;
				NativeMethods.WINDOWPLACEMENT windowplacement = default(NativeMethods.WINDOWPLACEMENT);
				windowplacement.length = Marshal.SizeOf(typeof(NativeMethods.WINDOWPLACEMENT));
				UnsafeNativeMethods.GetWindowPlacement(new HandleRef(this, base.Handle), ref windowplacement);
				switch (windowplacement.showCmd)
				{
				case 1:
				case 4:
				case 5:
				case 8:
				case 9:
					if (this.formState[Form.FormStateWindowState] != 0)
					{
						this.formState[Form.FormStateWindowState] = 0;
					}
					break;
				case 2:
				case 6:
				case 7:
					if (this.formState[Form.FormStateMdiChildMax] == 0)
					{
						this.formState[Form.FormStateWindowState] = 1;
					}
					break;
				case 3:
					if (this.formState[Form.FormStateMdiChildMax] == 0)
					{
						this.formState[Form.FormStateWindowState] = 2;
					}
					break;
				}
				if (windowState == FormWindowState.Normal && this.WindowState != FormWindowState.Normal)
				{
					if (this.WindowState == FormWindowState.Minimized)
					{
						this.SuspendLayoutForMinimize();
					}
					this.restoredWindowBounds.Size = this.ClientSize;
					this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] = 1;
					this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] = 1;
					this.restoredWindowBoundsSpecified = BoundsSpecified.Size;
					this.restoredWindowBounds.Location = this.Location;
					this.restoredWindowBoundsSpecified |= BoundsSpecified.Location;
					this.restoreBounds.Size = this.Size;
					this.restoreBounds.Location = this.Location;
				}
				if (windowState == FormWindowState.Minimized && this.WindowState != FormWindowState.Minimized)
				{
					this.ResumeLayoutFromMinimize();
				}
				FormWindowState windowState2 = this.WindowState;
				if (windowState2 != FormWindowState.Normal)
				{
					if (windowState2 - FormWindowState.Minimized <= 1)
					{
						base.SetState(65536, true);
					}
				}
				else
				{
					base.SetState(65536, false);
				}
				if (windowState != this.WindowState)
				{
					this.AdjustSystemMenu();
				}
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000B744F File Offset: 0x000B564F
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren()
		{
			return base.ValidateChildren();
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000B7457 File Offset: 0x000B5657
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			return base.ValidateChildren(validationConstraints);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000B7460 File Offset: 0x000B5660
		private void WmActivate(ref Message m)
		{
			Application.FormActivated(this.Modal, true);
			this.Active = (NativeMethods.Util.LOWORD(m.WParam) != 0);
			Application.FormActivated(this.Modal, this.Active);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000B7493 File Offset: 0x000B5693
		private void WmEnterSizeMove(ref Message m)
		{
			this.formStateEx[Form.FormStateExInModalSizingLoop] = 1;
			this.OnResizeBegin(EventArgs.Empty);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000B74B1 File Offset: 0x000B56B1
		private void WmExitSizeMove(ref Message m)
		{
			this.formStateEx[Form.FormStateExInModalSizingLoop] = 0;
			this.OnResizeEnd(EventArgs.Empty);
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000B74D0 File Offset: 0x000B56D0
		private void WmCreate(ref Message m)
		{
			base.WndProc(ref m);
			NativeMethods.STARTUPINFO_I startupinfo_I = new NativeMethods.STARTUPINFO_I();
			UnsafeNativeMethods.GetStartupInfo(startupinfo_I);
			if (this.TopLevel && (startupinfo_I.dwFlags & 1) != 0)
			{
				short wShowWindow = startupinfo_I.wShowWindow;
				if (wShowWindow == 3)
				{
					this.WindowState = FormWindowState.Maximized;
					return;
				}
				if (wShowWindow != 6)
				{
					return;
				}
				this.WindowState = FormWindowState.Minimized;
			}
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000B7524 File Offset: 0x000B5724
		private void WmClose(ref Message m)
		{
			FormClosingEventArgs formClosingEventArgs = new FormClosingEventArgs(this.CloseReason, false);
			if (m.Msg != 22)
			{
				if (this.Modal)
				{
					if (this.dialogResult == DialogResult.None)
					{
						this.dialogResult = DialogResult.Cancel;
					}
					this.CalledClosing = false;
					formClosingEventArgs.Cancel = !this.CheckCloseDialog(true);
				}
				else
				{
					formClosingEventArgs.Cancel = !base.Validate(true);
					if (this.IsMdiContainer)
					{
						FormClosingEventArgs formClosingEventArgs2 = new FormClosingEventArgs(CloseReason.MdiFormClosing, formClosingEventArgs.Cancel);
						foreach (Form form in this.MdiChildren)
						{
							if (form.IsHandleCreated)
							{
								form.OnClosing(formClosingEventArgs2);
								form.OnFormClosing(formClosingEventArgs2);
								if (formClosingEventArgs2.Cancel)
								{
									formClosingEventArgs.Cancel = true;
									break;
								}
							}
						}
					}
					Form[] ownedForms = this.OwnedForms;
					int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
					for (int j = integer - 1; j >= 0; j--)
					{
						FormClosingEventArgs formClosingEventArgs3 = new FormClosingEventArgs(CloseReason.FormOwnerClosing, formClosingEventArgs.Cancel);
						if (ownedForms[j] != null)
						{
							ownedForms[j].OnFormClosing(formClosingEventArgs3);
							if (formClosingEventArgs3.Cancel)
							{
								formClosingEventArgs.Cancel = true;
								break;
							}
						}
					}
					this.OnClosing(formClosingEventArgs);
					this.OnFormClosing(formClosingEventArgs);
				}
				if (m.Msg == 17)
				{
					m.Result = (IntPtr)(formClosingEventArgs.Cancel ? 0 : 1);
				}
				else if (formClosingEventArgs.Cancel && this.MdiParent != null)
				{
					this.CloseReason = CloseReason.None;
				}
				if (this.Modal)
				{
					return;
				}
			}
			else
			{
				formClosingEventArgs.Cancel = (m.WParam == IntPtr.Zero);
			}
			if (m.Msg != 17 && !formClosingEventArgs.Cancel)
			{
				this.IsClosing = true;
				FormClosedEventArgs e;
				if (this.IsMdiContainer)
				{
					e = new FormClosedEventArgs(CloseReason.MdiFormClosing);
					foreach (Form form2 in this.MdiChildren)
					{
						if (form2.IsHandleCreated)
						{
							form2.IsTopMdiWindowClosing = this.IsClosing;
							form2.OnClosed(e);
							form2.OnFormClosed(e);
						}
					}
				}
				Form[] ownedForms2 = this.OwnedForms;
				int integer2 = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				for (int l = integer2 - 1; l >= 0; l--)
				{
					e = new FormClosedEventArgs(CloseReason.FormOwnerClosing);
					if (ownedForms2[l] != null)
					{
						ownedForms2[l].OnClosed(e);
						ownedForms2[l].OnFormClosed(e);
					}
				}
				e = new FormClosedEventArgs(this.CloseReason);
				this.OnClosed(e);
				this.OnFormClosed(e);
				base.Dispose();
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000B77A1 File Offset: 0x000B59A1
		private void WmEnterMenuLoop(ref Message m)
		{
			this.OnMenuStart(EventArgs.Empty);
			base.WndProc(ref m);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000B77B5 File Offset: 0x000B59B5
		private void WmEraseBkgnd(ref Message m)
		{
			this.UpdateWindowState();
			base.WndProc(ref m);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000B77C4 File Offset: 0x000B59C4
		private void WmExitMenuLoop(ref Message m)
		{
			this.OnMenuComplete(EventArgs.Empty);
			base.WndProc(ref m);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000B77D8 File Offset: 0x000B59D8
		private void WmGetMinMaxInfo(ref Message m)
		{
			Size minTrack = (this.AutoSize && this.formStateEx[Form.FormStateExInModalSizingLoop] == 1) ? LayoutUtils.UnionSizes(this.minAutoSize, this.MinimumSize) : this.MinimumSize;
			Size maximumSize = this.MaximumSize;
			Rectangle maximizedBounds = this.MaximizedBounds;
			if (!minTrack.IsEmpty || !maximumSize.IsEmpty || !maximizedBounds.IsEmpty || this.IsRestrictedWindow)
			{
				this.WmGetMinMaxInfoHelper(ref m, minTrack, maximumSize, maximizedBounds);
			}
			if (this.IsMdiChild)
			{
				base.WndProc(ref m);
				return;
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000B7868 File Offset: 0x000B5A68
		private void WmGetMinMaxInfoHelper(ref Message m, Size minTrack, Size maxTrack, Rectangle maximizedBounds)
		{
			NativeMethods.MINMAXINFO minmaxinfo = (NativeMethods.MINMAXINFO)m.GetLParam(typeof(NativeMethods.MINMAXINFO));
			if (!minTrack.IsEmpty)
			{
				minmaxinfo.ptMinTrackSize.x = minTrack.Width;
				minmaxinfo.ptMinTrackSize.y = minTrack.Height;
				if (maxTrack.IsEmpty)
				{
					Size size = SystemInformation.VirtualScreen.Size;
					if (minTrack.Height > size.Height)
					{
						minmaxinfo.ptMaxTrackSize.y = int.MaxValue;
					}
					if (minTrack.Width > size.Width)
					{
						minmaxinfo.ptMaxTrackSize.x = int.MaxValue;
					}
				}
			}
			if (!maxTrack.IsEmpty)
			{
				Size minWindowTrackSize = SystemInformation.MinWindowTrackSize;
				minmaxinfo.ptMaxTrackSize.x = Math.Max(maxTrack.Width, minWindowTrackSize.Width);
				minmaxinfo.ptMaxTrackSize.y = Math.Max(maxTrack.Height, minWindowTrackSize.Height);
			}
			if (!maximizedBounds.IsEmpty && !this.IsRestrictedWindow)
			{
				minmaxinfo.ptMaxPosition.x = maximizedBounds.X;
				minmaxinfo.ptMaxPosition.y = maximizedBounds.Y;
				minmaxinfo.ptMaxSize.x = maximizedBounds.Width;
				minmaxinfo.ptMaxSize.y = maximizedBounds.Height;
			}
			if (this.IsRestrictedWindow)
			{
				minmaxinfo.ptMinTrackSize.x = Math.Max(minmaxinfo.ptMinTrackSize.x, 100);
				minmaxinfo.ptMinTrackSize.y = Math.Max(minmaxinfo.ptMinTrackSize.y, SystemInformation.CaptionButtonSize.Height * 3);
			}
			Marshal.StructureToPtr(minmaxinfo, m.LParam, false);
			m.Result = IntPtr.Zero;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000B7A1C File Offset: 0x000B5C1C
		private void WmInitMenuPopup(ref Message m)
		{
			MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropCurMenu);
			if (mainMenu != null && mainMenu.ProcessInitMenuPopup(m.WParam))
			{
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000B7A58 File Offset: 0x000B5C58
		private void WmMenuChar(ref Message m)
		{
			MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropCurMenu);
			if (mainMenu == null)
			{
				Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
				if (form != null && form.Menu != null)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(form, form.Handle), 274, new IntPtr(61696), m.WParam);
					m.Result = (IntPtr)NativeMethods.Util.MAKELONG(0, 1);
					return;
				}
			}
			if (mainMenu != null)
			{
				mainMenu.WmMenuChar(ref m);
				if (m.Result != IntPtr.Zero)
				{
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000B7B00 File Offset: 0x000B5D00
		private void WmMdiActivate(ref Message m)
		{
			base.WndProc(ref m);
			Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
			if (form != null)
			{
				if (base.Handle == m.WParam)
				{
					form.DeactivateMdiChild();
					return;
				}
				if (base.Handle == m.LParam)
				{
					form.ActivateMdiChildInternal(this);
				}
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000B7B64 File Offset: 0x000B5D64
		private void WmNcButtonDown(ref Message m)
		{
			if (this.IsMdiChild)
			{
				Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
				if (form.ActiveMdiChildInternal == this && base.ActiveControl != null && !base.ActiveControl.ContainsFocus)
				{
					base.InnerMostActiveContainerControl.FocusActiveControlInternal();
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000B7BC0 File Offset: 0x000B5DC0
		private void WmNCDestroy(ref Message m)
		{
			MainMenu menu = this.Menu;
			MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropDummyMenu);
			MainMenu mainMenu2 = (MainMenu)base.Properties.GetObject(Form.PropCurMenu);
			MainMenu mainMenu3 = (MainMenu)base.Properties.GetObject(Form.PropMergedMenu);
			if (menu != null)
			{
				menu.ClearHandles();
			}
			if (mainMenu2 != null)
			{
				mainMenu2.ClearHandles();
			}
			if (mainMenu3 != null)
			{
				mainMenu3.ClearHandles();
			}
			if (mainMenu != null)
			{
				mainMenu.ClearHandles();
			}
			base.WndProc(ref m);
			if (this.ownerWindow != null)
			{
				this.ownerWindow.DestroyHandle();
				this.ownerWindow = null;
			}
			if (this.Modal && this.dialogResult == DialogResult.None)
			{
				this.DialogResult = DialogResult.Cancel;
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000B7C74 File Offset: 0x000B5E74
		private void WmNCHitTest(ref Message m)
		{
			if (this.formState[Form.FormStateRenderSizeGrip] != 0)
			{
				int x = NativeMethods.Util.LOWORD(m.LParam);
				int y = NativeMethods.Util.HIWORD(m.LParam);
				NativeMethods.POINT point = new NativeMethods.POINT(x, y);
				UnsafeNativeMethods.ScreenToClient(new HandleRef(this, base.Handle), point);
				Size clientSize = this.ClientSize;
				if (point.x >= clientSize.Width - 16 && point.y >= clientSize.Height - 16 && clientSize.Height >= 16)
				{
					m.Result = (base.IsMirrored ? ((IntPtr)16) : ((IntPtr)17));
					return;
				}
			}
			base.WndProc(ref m);
			if (this.AutoSizeMode == AutoSizeMode.GrowAndShrink)
			{
				int num = (int)((long)m.Result);
				if (num >= 10 && num <= 17)
				{
					m.Result = (IntPtr)18;
				}
			}
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000B7D54 File Offset: 0x000B5F54
		private void WmShowWindow(ref Message m)
		{
			this.formState[Form.FormStateSWCalled] = 1;
			base.WndProc(ref m);
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000B7D70 File Offset: 0x000B5F70
		private void WmSysCommand(ref Message m)
		{
			bool flag = true;
			int num = NativeMethods.Util.LOWORD(m.WParam) & 65520;
			if (num <= 61456)
			{
				if (num == 61440 || num == 61456)
				{
					this.formStateEx[Form.FormStateExInModalSizingLoop] = 1;
				}
			}
			else if (num != 61536)
			{
				if (num != 61696)
				{
					if (num == 61824)
					{
						CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
						this.OnHelpButtonClicked(cancelEventArgs);
						if (cancelEventArgs.Cancel)
						{
							flag = false;
						}
					}
				}
				else if (this.IsMdiChild && !this.ControlBox)
				{
					flag = false;
				}
			}
			else
			{
				this.CloseReason = CloseReason.UserClosing;
				if (this.IsMdiChild && !this.ControlBox)
				{
					flag = false;
				}
			}
			if (Command.DispatchID(NativeMethods.Util.LOWORD(m.WParam)))
			{
				flag = false;
			}
			if (flag)
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000B7E40 File Offset: 0x000B6040
		private void WmSize(ref Message m)
		{
			if (this.ctlClient == null)
			{
				base.WndProc(ref m);
				if (this.MdiControlStrip == null && this.MdiParentInternal != null && this.MdiParentInternal.ActiveMdiChildInternal == this)
				{
					int num = m.WParam.ToInt32();
					this.MdiParentInternal.UpdateMdiControlStrip(num == 2);
				}
			}
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000B7E98 File Offset: 0x000B6098
		private void WmUnInitMenuPopup(ref Message m)
		{
			if (this.Menu != null)
			{
				this.Menu.OnCollapse(EventArgs.Empty);
			}
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000B7EB2 File Offset: 0x000B60B2
		private void WmWindowPosChanged(ref Message m)
		{
			this.UpdateWindowState();
			base.WndProc(ref m);
			this.RestoreWindowBoundsIfNecessary();
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000B7EC8 File Offset: 0x000B60C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 167)
			{
				if (msg <= 36)
				{
					if (msg <= 5)
					{
						if (msg == 1)
						{
							this.WmCreate(ref m);
							return;
						}
						if (msg != 5)
						{
							goto IL_2D0;
						}
						this.WmSize(ref m);
						return;
					}
					else
					{
						if (msg == 6)
						{
							this.WmActivate(ref m);
							return;
						}
						switch (msg)
						{
						case 16:
							if (this.CloseReason == CloseReason.None)
							{
								this.CloseReason = CloseReason.TaskManagerClosing;
							}
							this.WmClose(ref m);
							return;
						case 17:
						case 22:
							this.CloseReason = CloseReason.WindowsShutDown;
							this.WmClose(ref m);
							return;
						case 18:
						case 19:
						case 21:
						case 23:
							goto IL_2D0;
						case 20:
							this.WmEraseBkgnd(ref m);
							return;
						case 24:
							this.WmShowWindow(ref m);
							return;
						default:
							if (msg != 36)
							{
								goto IL_2D0;
							}
							this.WmGetMinMaxInfo(ref m);
							return;
						}
					}
				}
				else if (msg <= 134)
				{
					if (msg == 71)
					{
						this.WmWindowPosChanged(ref m);
						return;
					}
					switch (msg)
					{
					case 130:
						this.WmNCDestroy(ref m);
						return;
					case 131:
					case 133:
						goto IL_2D0;
					case 132:
						this.WmNCHitTest(ref m);
						return;
					case 134:
						if (this.IsRestrictedWindow)
						{
							base.BeginInvoke(new MethodInvoker(this.RestrictedProcessNcActivate));
						}
						base.WndProc(ref m);
						return;
					default:
						goto IL_2D0;
					}
				}
				else if (msg != 161 && msg != 164 && msg != 167)
				{
					goto IL_2D0;
				}
			}
			else if (msg <= 293)
			{
				if (msg <= 274)
				{
					if (msg != 171)
					{
						if (msg != 274)
						{
							goto IL_2D0;
						}
						this.WmSysCommand(ref m);
						return;
					}
				}
				else
				{
					if (msg == 279)
					{
						this.WmInitMenuPopup(ref m);
						return;
					}
					if (msg == 288)
					{
						this.WmMenuChar(ref m);
						return;
					}
					if (msg != 293)
					{
						goto IL_2D0;
					}
					this.WmUnInitMenuPopup(ref m);
					return;
				}
			}
			else if (msg <= 561)
			{
				switch (msg)
				{
				case 529:
					this.WmEnterMenuLoop(ref m);
					return;
				case 530:
					this.WmExitMenuLoop(ref m);
					return;
				case 531:
				case 532:
					goto IL_2D0;
				case 533:
					base.WndProc(ref m);
					if (base.CaptureInternal && Control.MouseButtons == MouseButtons.None)
					{
						base.CaptureInternal = false;
						return;
					}
					return;
				default:
					if (msg == 546)
					{
						this.WmMdiActivate(ref m);
						return;
					}
					if (msg != 561)
					{
						goto IL_2D0;
					}
					this.WmEnterSizeMove(ref m);
					this.DefWndProc(ref m);
					return;
				}
			}
			else
			{
				if (msg == 562)
				{
					this.WmExitSizeMove(ref m);
					this.DefWndProc(ref m);
					return;
				}
				if (msg != 736)
				{
					if (msg != 737)
					{
						goto IL_2D0;
					}
					if (DpiHelper.EnableDpiChangedMessageHandling)
					{
						this.WmGetDpiScaledSize(ref m);
						return;
					}
					m.Result = IntPtr.Zero;
					return;
				}
				else
				{
					if (DpiHelper.EnableDpiChangedMessageHandling)
					{
						this.WmDpiChanged(ref m);
						m.Result = IntPtr.Zero;
						return;
					}
					m.Result = (IntPtr)1;
					return;
				}
			}
			this.WmNcButtonDown(ref m);
			return;
			IL_2D0:
			base.WndProc(ref m);
		}

		// Token: 0x04000FCB RID: 4043
		private static readonly object EVENT_ACTIVATED = new object();

		// Token: 0x04000FCC RID: 4044
		private static readonly object EVENT_CLOSING = new object();

		// Token: 0x04000FCD RID: 4045
		private static readonly object EVENT_CLOSED = new object();

		// Token: 0x04000FCE RID: 4046
		private static readonly object EVENT_FORMCLOSING = new object();

		// Token: 0x04000FCF RID: 4047
		private static readonly object EVENT_FORMCLOSED = new object();

		// Token: 0x04000FD0 RID: 4048
		private static readonly object EVENT_DEACTIVATE = new object();

		// Token: 0x04000FD1 RID: 4049
		private static readonly object EVENT_LOAD = new object();

		// Token: 0x04000FD2 RID: 4050
		private static readonly object EVENT_MDI_CHILD_ACTIVATE = new object();

		// Token: 0x04000FD3 RID: 4051
		private static readonly object EVENT_INPUTLANGCHANGE = new object();

		// Token: 0x04000FD4 RID: 4052
		private static readonly object EVENT_INPUTLANGCHANGEREQUEST = new object();

		// Token: 0x04000FD5 RID: 4053
		private static readonly object EVENT_MENUSTART = new object();

		// Token: 0x04000FD6 RID: 4054
		private static readonly object EVENT_MENUCOMPLETE = new object();

		// Token: 0x04000FD7 RID: 4055
		private static readonly object EVENT_MAXIMUMSIZECHANGED = new object();

		// Token: 0x04000FD8 RID: 4056
		private static readonly object EVENT_MINIMUMSIZECHANGED = new object();

		// Token: 0x04000FD9 RID: 4057
		private static readonly object EVENT_HELPBUTTONCLICKED = new object();

		// Token: 0x04000FDA RID: 4058
		private static readonly object EVENT_SHOWN = new object();

		// Token: 0x04000FDB RID: 4059
		private static readonly object EVENT_RESIZEBEGIN = new object();

		// Token: 0x04000FDC RID: 4060
		private static readonly object EVENT_RESIZEEND = new object();

		// Token: 0x04000FDD RID: 4061
		private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();

		// Token: 0x04000FDE RID: 4062
		private static readonly object EVENT_DPI_CHANGED = new object();

		// Token: 0x04000FDF RID: 4063
		private static readonly BitVector32.Section FormStateAllowTransparency = BitVector32.CreateSection(1);

		// Token: 0x04000FE0 RID: 4064
		private static readonly BitVector32.Section FormStateBorderStyle = BitVector32.CreateSection(6, Form.FormStateAllowTransparency);

		// Token: 0x04000FE1 RID: 4065
		private static readonly BitVector32.Section FormStateTaskBar = BitVector32.CreateSection(1, Form.FormStateBorderStyle);

		// Token: 0x04000FE2 RID: 4066
		private static readonly BitVector32.Section FormStateControlBox = BitVector32.CreateSection(1, Form.FormStateTaskBar);

		// Token: 0x04000FE3 RID: 4067
		private static readonly BitVector32.Section FormStateKeyPreview = BitVector32.CreateSection(1, Form.FormStateControlBox);

		// Token: 0x04000FE4 RID: 4068
		private static readonly BitVector32.Section FormStateLayered = BitVector32.CreateSection(1, Form.FormStateKeyPreview);

		// Token: 0x04000FE5 RID: 4069
		private static readonly BitVector32.Section FormStateMaximizeBox = BitVector32.CreateSection(1, Form.FormStateLayered);

		// Token: 0x04000FE6 RID: 4070
		private static readonly BitVector32.Section FormStateMinimizeBox = BitVector32.CreateSection(1, Form.FormStateMaximizeBox);

		// Token: 0x04000FE7 RID: 4071
		private static readonly BitVector32.Section FormStateHelpButton = BitVector32.CreateSection(1, Form.FormStateMinimizeBox);

		// Token: 0x04000FE8 RID: 4072
		private static readonly BitVector32.Section FormStateStartPos = BitVector32.CreateSection(4, Form.FormStateHelpButton);

		// Token: 0x04000FE9 RID: 4073
		private static readonly BitVector32.Section FormStateWindowState = BitVector32.CreateSection(2, Form.FormStateStartPos);

		// Token: 0x04000FEA RID: 4074
		private static readonly BitVector32.Section FormStateShowWindowOnCreate = BitVector32.CreateSection(1, Form.FormStateWindowState);

		// Token: 0x04000FEB RID: 4075
		private static readonly BitVector32.Section FormStateAutoScaling = BitVector32.CreateSection(1, Form.FormStateShowWindowOnCreate);

		// Token: 0x04000FEC RID: 4076
		private static readonly BitVector32.Section FormStateSetClientSize = BitVector32.CreateSection(1, Form.FormStateAutoScaling);

		// Token: 0x04000FED RID: 4077
		private static readonly BitVector32.Section FormStateTopMost = BitVector32.CreateSection(1, Form.FormStateSetClientSize);

		// Token: 0x04000FEE RID: 4078
		private static readonly BitVector32.Section FormStateSWCalled = BitVector32.CreateSection(1, Form.FormStateTopMost);

		// Token: 0x04000FEF RID: 4079
		private static readonly BitVector32.Section FormStateMdiChildMax = BitVector32.CreateSection(1, Form.FormStateSWCalled);

		// Token: 0x04000FF0 RID: 4080
		private static readonly BitVector32.Section FormStateRenderSizeGrip = BitVector32.CreateSection(1, Form.FormStateMdiChildMax);

		// Token: 0x04000FF1 RID: 4081
		private static readonly BitVector32.Section FormStateSizeGripStyle = BitVector32.CreateSection(2, Form.FormStateRenderSizeGrip);

		// Token: 0x04000FF2 RID: 4082
		private static readonly BitVector32.Section FormStateIsRestrictedWindow = BitVector32.CreateSection(1, Form.FormStateSizeGripStyle);

		// Token: 0x04000FF3 RID: 4083
		private static readonly BitVector32.Section FormStateIsRestrictedWindowChecked = BitVector32.CreateSection(1, Form.FormStateIsRestrictedWindow);

		// Token: 0x04000FF4 RID: 4084
		private static readonly BitVector32.Section FormStateIsWindowActivated = BitVector32.CreateSection(1, Form.FormStateIsRestrictedWindowChecked);

		// Token: 0x04000FF5 RID: 4085
		private static readonly BitVector32.Section FormStateIsTextEmpty = BitVector32.CreateSection(1, Form.FormStateIsWindowActivated);

		// Token: 0x04000FF6 RID: 4086
		private static readonly BitVector32.Section FormStateIsActive = BitVector32.CreateSection(1, Form.FormStateIsTextEmpty);

		// Token: 0x04000FF7 RID: 4087
		private static readonly BitVector32.Section FormStateIconSet = BitVector32.CreateSection(1, Form.FormStateIsActive);

		// Token: 0x04000FF8 RID: 4088
		private static readonly BitVector32.Section FormStateExCalledClosing = BitVector32.CreateSection(1);

		// Token: 0x04000FF9 RID: 4089
		private static readonly BitVector32.Section FormStateExUpdateMenuHandlesSuspendCount = BitVector32.CreateSection(8, Form.FormStateExCalledClosing);

		// Token: 0x04000FFA RID: 4090
		private static readonly BitVector32.Section FormStateExUpdateMenuHandlesDeferred = BitVector32.CreateSection(1, Form.FormStateExUpdateMenuHandlesSuspendCount);

		// Token: 0x04000FFB RID: 4091
		private static readonly BitVector32.Section FormStateExUseMdiChildProc = BitVector32.CreateSection(1, Form.FormStateExUpdateMenuHandlesDeferred);

		// Token: 0x04000FFC RID: 4092
		private static readonly BitVector32.Section FormStateExCalledOnLoad = BitVector32.CreateSection(1, Form.FormStateExUseMdiChildProc);

		// Token: 0x04000FFD RID: 4093
		private static readonly BitVector32.Section FormStateExCalledMakeVisible = BitVector32.CreateSection(1, Form.FormStateExCalledOnLoad);

		// Token: 0x04000FFE RID: 4094
		private static readonly BitVector32.Section FormStateExCalledCreateControl = BitVector32.CreateSection(1, Form.FormStateExCalledMakeVisible);

		// Token: 0x04000FFF RID: 4095
		private static readonly BitVector32.Section FormStateExAutoSize = BitVector32.CreateSection(1, Form.FormStateExCalledCreateControl);

		// Token: 0x04001000 RID: 4096
		private static readonly BitVector32.Section FormStateExInUpdateMdiControlStrip = BitVector32.CreateSection(1, Form.FormStateExAutoSize);

		// Token: 0x04001001 RID: 4097
		private static readonly BitVector32.Section FormStateExShowIcon = BitVector32.CreateSection(1, Form.FormStateExInUpdateMdiControlStrip);

		// Token: 0x04001002 RID: 4098
		private static readonly BitVector32.Section FormStateExMnemonicProcessed = BitVector32.CreateSection(1, Form.FormStateExShowIcon);

		// Token: 0x04001003 RID: 4099
		private static readonly BitVector32.Section FormStateExInScale = BitVector32.CreateSection(1, Form.FormStateExMnemonicProcessed);

		// Token: 0x04001004 RID: 4100
		private static readonly BitVector32.Section FormStateExInModalSizingLoop = BitVector32.CreateSection(1, Form.FormStateExInScale);

		// Token: 0x04001005 RID: 4101
		private static readonly BitVector32.Section FormStateExSettingAutoScale = BitVector32.CreateSection(1, Form.FormStateExInModalSizingLoop);

		// Token: 0x04001006 RID: 4102
		private static readonly BitVector32.Section FormStateExWindowBoundsWidthIsClientSize = BitVector32.CreateSection(1, Form.FormStateExSettingAutoScale);

		// Token: 0x04001007 RID: 4103
		private static readonly BitVector32.Section FormStateExWindowBoundsHeightIsClientSize = BitVector32.CreateSection(1, Form.FormStateExWindowBoundsWidthIsClientSize);

		// Token: 0x04001008 RID: 4104
		private static readonly BitVector32.Section FormStateExWindowClosing = BitVector32.CreateSection(1, Form.FormStateExWindowBoundsHeightIsClientSize);

		// Token: 0x04001009 RID: 4105
		private const int SizeGripSize = 16;

		// Token: 0x0400100A RID: 4106
		private static Icon defaultIcon = null;

		// Token: 0x0400100B RID: 4107
		private static Icon defaultRestrictedIcon = null;

		// Token: 0x0400100C RID: 4108
		private static object internalSyncObject = new object();

		// Token: 0x0400100D RID: 4109
		private static readonly int PropAcceptButton = PropertyStore.CreateKey();

		// Token: 0x0400100E RID: 4110
		private static readonly int PropCancelButton = PropertyStore.CreateKey();

		// Token: 0x0400100F RID: 4111
		private static readonly int PropDefaultButton = PropertyStore.CreateKey();

		// Token: 0x04001010 RID: 4112
		private static readonly int PropDialogOwner = PropertyStore.CreateKey();

		// Token: 0x04001011 RID: 4113
		private static readonly int PropMainMenu = PropertyStore.CreateKey();

		// Token: 0x04001012 RID: 4114
		private static readonly int PropDummyMenu = PropertyStore.CreateKey();

		// Token: 0x04001013 RID: 4115
		private static readonly int PropCurMenu = PropertyStore.CreateKey();

		// Token: 0x04001014 RID: 4116
		private static readonly int PropMergedMenu = PropertyStore.CreateKey();

		// Token: 0x04001015 RID: 4117
		private static readonly int PropOwner = PropertyStore.CreateKey();

		// Token: 0x04001016 RID: 4118
		private static readonly int PropOwnedForms = PropertyStore.CreateKey();

		// Token: 0x04001017 RID: 4119
		private static readonly int PropMaximizedBounds = PropertyStore.CreateKey();

		// Token: 0x04001018 RID: 4120
		private static readonly int PropOwnedFormsCount = PropertyStore.CreateKey();

		// Token: 0x04001019 RID: 4121
		private static readonly int PropMinTrackSizeWidth = PropertyStore.CreateKey();

		// Token: 0x0400101A RID: 4122
		private static readonly int PropMinTrackSizeHeight = PropertyStore.CreateKey();

		// Token: 0x0400101B RID: 4123
		private static readonly int PropMaxTrackSizeWidth = PropertyStore.CreateKey();

		// Token: 0x0400101C RID: 4124
		private static readonly int PropMaxTrackSizeHeight = PropertyStore.CreateKey();

		// Token: 0x0400101D RID: 4125
		private static readonly int PropFormMdiParent = PropertyStore.CreateKey();

		// Token: 0x0400101E RID: 4126
		private static readonly int PropActiveMdiChild = PropertyStore.CreateKey();

		// Token: 0x0400101F RID: 4127
		private static readonly int PropFormerlyActiveMdiChild = PropertyStore.CreateKey();

		// Token: 0x04001020 RID: 4128
		private static readonly int PropMdiChildFocusable = PropertyStore.CreateKey();

		// Token: 0x04001021 RID: 4129
		private static readonly int PropMainMenuStrip = PropertyStore.CreateKey();

		// Token: 0x04001022 RID: 4130
		private static readonly int PropMdiWindowListStrip = PropertyStore.CreateKey();

		// Token: 0x04001023 RID: 4131
		private static readonly int PropMdiControlStrip = PropertyStore.CreateKey();

		// Token: 0x04001024 RID: 4132
		private static readonly int PropSecurityTip = PropertyStore.CreateKey();

		// Token: 0x04001025 RID: 4133
		private static readonly int PropOpacity = PropertyStore.CreateKey();

		// Token: 0x04001026 RID: 4134
		private static readonly int PropTransparencyKey = PropertyStore.CreateKey();

		// Token: 0x04001027 RID: 4135
		private BitVector32 formState = new BitVector32(135992);

		// Token: 0x04001028 RID: 4136
		private BitVector32 formStateEx;

		// Token: 0x04001029 RID: 4137
		private Icon icon;

		// Token: 0x0400102A RID: 4138
		private Icon smallIcon;

		// Token: 0x0400102B RID: 4139
		private Size autoScaleBaseSize = Size.Empty;

		// Token: 0x0400102C RID: 4140
		private Size minAutoSize = Size.Empty;

		// Token: 0x0400102D RID: 4141
		private Rectangle restoredWindowBounds = new Rectangle(-1, -1, -1, -1);

		// Token: 0x0400102E RID: 4142
		private BoundsSpecified restoredWindowBoundsSpecified;

		// Token: 0x0400102F RID: 4143
		private DialogResult dialogResult;

		// Token: 0x04001030 RID: 4144
		private MdiClient ctlClient;

		// Token: 0x04001031 RID: 4145
		private NativeWindow ownerWindow;

		// Token: 0x04001032 RID: 4146
		private string userWindowText;

		// Token: 0x04001033 RID: 4147
		private string securityZone;

		// Token: 0x04001034 RID: 4148
		private string securitySite;

		// Token: 0x04001035 RID: 4149
		private bool rightToLeftLayout;

		// Token: 0x04001036 RID: 4150
		private Rectangle restoreBounds = new Rectangle(-1, -1, -1, -1);

		// Token: 0x04001037 RID: 4151
		private CloseReason closeReason;

		// Token: 0x04001038 RID: 4152
		private VisualStyleRenderer sizeGripRenderer;

		// Token: 0x04001039 RID: 4153
		private static readonly object EVENT_MAXIMIZEDBOUNDSCHANGED = new object();

		// Token: 0x020006A1 RID: 1697
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			// Token: 0x060067DD RID: 26589 RVA: 0x0018396F File Offset: 0x00181B6F
			public ControlCollection(Form owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x060067DE RID: 26590 RVA: 0x00183980 File Offset: 0x00181B80
			public override void Add(Control value)
			{
				if (value is MdiClient && this.owner.ctlClient == null)
				{
					if (!this.owner.TopLevel && !this.owner.DesignMode)
					{
						throw new ArgumentException(SR.GetString("MDIContainerMustBeTopLevel"), "value");
					}
					this.owner.AutoScroll = false;
					if (this.owner.IsMdiChild)
					{
						throw new ArgumentException(SR.GetString("FormMDIParentAndChild"), "value");
					}
					this.owner.ctlClient = (MdiClient)value;
				}
				if (value is Form && ((Form)value).MdiParentInternal != null)
				{
					throw new ArgumentException(SR.GetString("FormMDIParentCannotAdd"), "value");
				}
				base.Add(value);
				if (this.owner.ctlClient != null)
				{
					this.owner.ctlClient.SendToBack();
				}
			}

			// Token: 0x060067DF RID: 26591 RVA: 0x00183A5E File Offset: 0x00181C5E
			public override void Remove(Control value)
			{
				if (value == this.owner.ctlClient)
				{
					this.owner.ctlClient = null;
				}
				base.Remove(value);
			}

			// Token: 0x04003AE0 RID: 15072
			private Form owner;
		}

		// Token: 0x020006A2 RID: 1698
		private class EnumThreadWindowsCallback
		{
			// Token: 0x060067E0 RID: 26592 RVA: 0x00002843 File Offset: 0x00000A43
			internal EnumThreadWindowsCallback()
			{
			}

			// Token: 0x060067E1 RID: 26593 RVA: 0x00183A84 File Offset: 0x00181C84
			internal bool Callback(IntPtr hWnd, IntPtr lParam)
			{
				HandleRef handleRef = new HandleRef(null, hWnd);
				IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(handleRef, -8);
				if (windowLong == lParam)
				{
					if (this.ownedWindows == null)
					{
						this.ownedWindows = new List<HandleRef>();
					}
					this.ownedWindows.Add(handleRef);
				}
				return true;
			}

			// Token: 0x060067E2 RID: 26594 RVA: 0x00183ACC File Offset: 0x00181CCC
			internal void ResetOwners()
			{
				if (this.ownedWindows != null)
				{
					foreach (HandleRef hWnd in this.ownedWindows)
					{
						UnsafeNativeMethods.SetWindowLong(hWnd, -8, NativeMethods.NullHandleRef);
					}
				}
			}

			// Token: 0x060067E3 RID: 26595 RVA: 0x00183B30 File Offset: 0x00181D30
			internal void SetOwners(HandleRef hRefOwner)
			{
				if (this.ownedWindows != null)
				{
					foreach (HandleRef hWnd in this.ownedWindows)
					{
						UnsafeNativeMethods.SetWindowLong(hWnd, -8, hRefOwner);
					}
				}
			}

			// Token: 0x04003AE1 RID: 15073
			private List<HandleRef> ownedWindows;
		}

		// Token: 0x020006A3 RID: 1699
		private class SecurityToolTip : IDisposable
		{
			// Token: 0x060067E4 RID: 26596 RVA: 0x00183B90 File Offset: 0x00181D90
			internal SecurityToolTip(Form owner)
			{
				this.owner = owner;
				this.SetupText();
				this.window = new Form.SecurityToolTip.ToolTipNativeWindow(this);
				this.SetupToolTip();
				owner.LocationChanged += this.FormLocationChanged;
				owner.HandleCreated += this.FormHandleCreated;
			}

			// Token: 0x17001689 RID: 5769
			// (get) Token: 0x060067E5 RID: 26597 RVA: 0x00183BF0 File Offset: 0x00181DF0
			private CreateParams CreateParams
			{
				get
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 8
					});
					CreateParams createParams = new CreateParams();
					createParams.Parent = this.owner.Handle;
					createParams.ClassName = "tooltips_class32";
					createParams.Style |= 65;
					createParams.ExStyle = 0;
					createParams.Caption = null;
					return createParams;
				}
			}

			// Token: 0x1700168A RID: 5770
			// (get) Token: 0x060067E6 RID: 26598 RVA: 0x00183C51 File Offset: 0x00181E51
			internal bool Modal
			{
				get
				{
					return this.first;
				}
			}

			// Token: 0x060067E7 RID: 26599 RVA: 0x00183C5C File Offset: 0x00181E5C
			public void Dispose()
			{
				if (this.owner != null)
				{
					this.owner.LocationChanged -= this.FormLocationChanged;
				}
				if (this.window.Handle != IntPtr.Zero)
				{
					this.window.DestroyHandle();
					this.window = null;
				}
			}

			// Token: 0x060067E8 RID: 26600 RVA: 0x00183CB4 File Offset: 0x00181EB4
			private NativeMethods.TOOLINFO_T GetTOOLINFO()
			{
				NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
				toolinfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));
				toolinfo_T.uFlags |= 16;
				toolinfo_T.lpszText = this.toolTipText;
				if (this.owner.RightToLeft == RightToLeft.Yes)
				{
					toolinfo_T.uFlags |= 4;
				}
				if (!this.first)
				{
					toolinfo_T.uFlags |= 256;
					toolinfo_T.hwnd = this.owner.Handle;
					Size captionButtonSize = SystemInformation.CaptionButtonSize;
					Rectangle r = new Rectangle(this.owner.Left, this.owner.Top, captionButtonSize.Width, SystemInformation.CaptionHeight);
					r = this.owner.RectangleToClient(r);
					r.Width -= r.X;
					r.Y++;
					toolinfo_T.rect = NativeMethods.RECT.FromXYWH(r.X, r.Y, r.Width, r.Height);
					toolinfo_T.uId = IntPtr.Zero;
				}
				else
				{
					toolinfo_T.uFlags |= 33;
					toolinfo_T.hwnd = IntPtr.Zero;
					toolinfo_T.uId = this.owner.Handle;
				}
				return toolinfo_T;
			}

			// Token: 0x060067E9 RID: 26601 RVA: 0x00183E04 File Offset: 0x00182004
			private void SetupText()
			{
				this.owner.EnsureSecurityInformation();
				string @string = SR.GetString("SecurityToolTipMainText");
				string string2 = SR.GetString("SecurityToolTipSourceInformation", new object[]
				{
					this.owner.securitySite
				});
				this.toolTipText = SR.GetString("SecurityToolTipTextFormat", new object[]
				{
					@string,
					string2
				});
			}

			// Token: 0x060067EA RID: 26602 RVA: 0x00183E64 File Offset: 0x00182064
			private void SetupToolTip()
			{
				this.window.CreateHandle(this.CreateParams);
				SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.window.Handle), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19);
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1048, 0, this.owner.Width);
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), NativeMethods.TTM_SETTITLE, 2, SR.GetString("SecurityToolTipCaption"));
				(int)UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO());
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1025, 1, 0);
				this.Show();
			}

			// Token: 0x060067EB RID: 26603 RVA: 0x00183F58 File Offset: 0x00182158
			private void RecreateHandle()
			{
				if (this.window != null)
				{
					if (this.window.Handle != IntPtr.Zero)
					{
						this.window.DestroyHandle();
					}
					this.SetupToolTip();
				}
			}

			// Token: 0x060067EC RID: 26604 RVA: 0x00183F8A File Offset: 0x0018218A
			private void FormHandleCreated(object sender, EventArgs e)
			{
				this.RecreateHandle();
			}

			// Token: 0x060067ED RID: 26605 RVA: 0x00183F94 File Offset: 0x00182194
			private void FormLocationChanged(object sender, EventArgs e)
			{
				if (this.window == null || !this.first)
				{
					this.Pop(true);
					return;
				}
				Size captionButtonSize = SystemInformation.CaptionButtonSize;
				if (this.owner.WindowState == FormWindowState.Minimized)
				{
					this.Pop(true);
					return;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1042, 0, NativeMethods.Util.MAKELONG(this.owner.Left + captionButtonSize.Width / 2, this.owner.Top + SystemInformation.CaptionHeight));
			}

			// Token: 0x060067EE RID: 26606 RVA: 0x00184024 File Offset: 0x00182224
			internal void Pop(bool noLongerFirst)
			{
				if (noLongerFirst)
				{
					this.first = false;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1041, 0, this.GetTOOLINFO());
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), NativeMethods.TTM_DELTOOL, 0, this.GetTOOLINFO());
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO());
			}

			// Token: 0x060067EF RID: 26607 RVA: 0x001840B4 File Offset: 0x001822B4
			internal void Show()
			{
				if (this.first)
				{
					Size captionButtonSize = SystemInformation.CaptionButtonSize;
					UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1042, 0, NativeMethods.Util.MAKELONG(this.owner.Left + captionButtonSize.Width / 2, this.owner.Top + SystemInformation.CaptionHeight));
					UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1041, 1, this.GetTOOLINFO());
				}
			}

			// Token: 0x060067F0 RID: 26608 RVA: 0x00184144 File Offset: 0x00182344
			private void WndProc(ref Message msg)
			{
				if (this.first && (msg.Msg == 513 || msg.Msg == 516 || msg.Msg == 519 || msg.Msg == 523))
				{
					this.Pop(true);
				}
				this.window.DefWndProc(ref msg);
			}

			// Token: 0x04003AE2 RID: 15074
			private Form owner;

			// Token: 0x04003AE3 RID: 15075
			private string toolTipText;

			// Token: 0x04003AE4 RID: 15076
			private bool first = true;

			// Token: 0x04003AE5 RID: 15077
			private Form.SecurityToolTip.ToolTipNativeWindow window;

			// Token: 0x020008BD RID: 2237
			private sealed class ToolTipNativeWindow : NativeWindow
			{
				// Token: 0x060072E0 RID: 29408 RVA: 0x001A49EE File Offset: 0x001A2BEE
				internal ToolTipNativeWindow(Form.SecurityToolTip control)
				{
					this.control = control;
				}

				// Token: 0x060072E1 RID: 29409 RVA: 0x001A49FD File Offset: 0x001A2BFD
				protected override void WndProc(ref Message m)
				{
					if (this.control != null)
					{
						this.control.WndProc(ref m);
					}
				}

				// Token: 0x04004536 RID: 17718
				private Form.SecurityToolTip control;
			}
		}
	}
}
