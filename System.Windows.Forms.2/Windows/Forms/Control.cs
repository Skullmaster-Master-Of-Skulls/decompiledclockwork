using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Internal;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms.Automation;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;
using Accessibility;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x0200016A RID: 362
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Text")]
	[DefaultEvent("Click")]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Windows.Forms")]
	public class Control : Component, UnsafeNativeMethods.IOleControl, UnsafeNativeMethods.IOleObject, UnsafeNativeMethods.IOleInPlaceObject, UnsafeNativeMethods.IOleInPlaceActiveObject, UnsafeNativeMethods.IOleWindow, UnsafeNativeMethods.IViewObject, UnsafeNativeMethods.IViewObject2, UnsafeNativeMethods.IPersist, UnsafeNativeMethods.IPersistStreamInit, UnsafeNativeMethods.IPersistPropertyBag, UnsafeNativeMethods.IPersistStorage, UnsafeNativeMethods.IQuickActivate, ISupportOleDropSource, IDropTarget, ISynchronizeInvoke, IWin32Window, IArrangedElement, IComponent, IDisposable, IBindableComponent, IKeyboardToolTip
	{
		// Token: 0x06000F5A RID: 3930 RVA: 0x0002ECB4 File Offset: 0x0002CEB4
		static Control()
		{
			Control.WM_GETCONTROLNAME = SafeNativeMethods.RegisterWindowMessage("WM_GETCONTROLNAME");
			Control.WM_GETCONTROLTYPE = SafeNativeMethods.RegisterWindowMessage("WM_GETCONTROLTYPE");
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002F16D File Offset: 0x0002D36D
		public Control() : this(true)
		{
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002F178 File Offset: 0x0002D378
		internal Control(bool autoInstallSyncContext)
		{
			this.propertyStore = new PropertyStore();
			DpiHelper.InitializeDpiHelperForWinforms();
			this.deviceDpi = DpiHelper.DeviceDpi;
			this.window = new Control.ControlNativeWindow(this);
			this.RequiredScalingEnabled = true;
			this.RequiredScaling = BoundsSpecified.All;
			this.tabIndex = -1;
			this.state = 131086;
			this.state2 = 8;
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.Selectable | ControlStyles.StandardDoubleClick | ControlStyles.AllPaintingInWmPaint | ControlStyles.UseTextForAccessibility, true);
			this.InitMouseWheelSupport();
			if (this.DefaultMargin != CommonProperties.DefaultMargin)
			{
				this.Margin = this.DefaultMargin;
			}
			if (this.DefaultMinimumSize != CommonProperties.DefaultMinimumSize)
			{
				this.MinimumSize = this.DefaultMinimumSize;
			}
			if (this.DefaultMaximumSize != CommonProperties.DefaultMaximumSize)
			{
				this.MaximumSize = this.DefaultMaximumSize;
			}
			Size defaultSize = this.DefaultSize;
			this.width = defaultSize.Width;
			this.height = defaultSize.Height;
			CommonProperties.xClearPreferredSizeCache(this);
			if (this.width != 0 && this.height != 0)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				rect.left = (rect.right = (rect.top = (rect.bottom = 0)));
				CreateParams createParams = this.CreateParams;
				this.AdjustWindowRectEx(ref rect, createParams.Style, false, createParams.ExStyle);
				this.clientWidth = this.width - (rect.right - rect.left);
				this.clientHeight = this.height - (rect.bottom - rect.top);
			}
			if (autoInstallSyncContext)
			{
				WindowsFormsSynchronizationContext.InstallIfNeeded();
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002F312 File Offset: 0x0002D512
		public Control(string text) : this(null, text)
		{
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002F31C File Offset: 0x0002D51C
		public Control(string text, int left, int top, int width, int height) : this(null, text, left, top, width, height)
		{
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002F32C File Offset: 0x0002D52C
		public Control(Control parent, string text) : this()
		{
			this.Parent = parent;
			this.Text = text;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002F342 File Offset: 0x0002D542
		public Control(Control parent, string text, int left, int top, int width, int height) : this(parent, text)
		{
			this.Location = new Point(left, top);
			this.Size = new Size(width, height);
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0002F369 File Offset: 0x0002D569
		internal DpiAwarenessContext DpiAwarenessContext
		{
			get
			{
				return this.window.DpiAwarenessContext;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0002F378 File Offset: 0x0002D578
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlAccessibilityObjectDescr")]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)this.Properties.GetObject(Control.PropAccessibility);
				if (accessibleObject == null)
				{
					accessibleObject = this.CreateAccessibilityInstance();
					if (!(accessibleObject is Control.ControlAccessibleObject))
					{
						return null;
					}
					this.Properties.SetObject(Control.PropAccessibility, accessibleObject);
				}
				return accessibleObject;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0002F3C4 File Offset: 0x0002D5C4
		private AccessibleObject NcAccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)this.Properties.GetObject(Control.PropNcAccessibility);
				if (accessibleObject == null)
				{
					accessibleObject = new Control.ControlAccessibleObject(this, 0);
					this.Properties.SetObject(Control.PropNcAccessibility, accessibleObject);
				}
				return accessibleObject;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0002F404 File Offset: 0x0002D604
		private InternalAccessibleObject UnsafeAccessibilityObject
		{
			get
			{
				InternalAccessibleObject internalAccessibleObject = (InternalAccessibleObject)this.Properties.GetObject(Control.PropUnsafeAccessibility);
				if (internalAccessibleObject == null)
				{
					internalAccessibleObject = Control.CreateInternalAccessibleObject(this.AccessibilityObject);
					this.Properties.SetObject(Control.PropUnsafeAccessibility, internalAccessibleObject);
				}
				IntSecurity.UnmanagedCode.Assert();
				InternalAccessibleObject result;
				try
				{
					result = internalAccessibleObject;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return result;
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002F46C File Offset: 0x0002D66C
		internal static InternalAccessibleObject CreateInternalAccessibleObject(AccessibleObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			IntSecurity.UnmanagedCode.Assert();
			InternalAccessibleObject result;
			try
			{
				result = new InternalAccessibleObject(obj);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002F4A8 File Offset: 0x0002D6A8
		private AccessibleObject GetAccessibilityObject(int accObjId)
		{
			AccessibleObject result;
			if (accObjId != -4)
			{
				if (accObjId != 0)
				{
					if (accObjId > 0)
					{
						result = this.GetAccessibilityObjectById(accObjId);
					}
					else
					{
						result = null;
					}
				}
				else
				{
					result = this.NcAccessibilityObject;
				}
			}
			else
			{
				result = this.AccessibilityObject;
			}
			return result;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002F4E4 File Offset: 0x0002D6E4
		private InternalAccessibleObject GetInternalAccessibilityObject(int accObjId)
		{
			if (accObjId == -4)
			{
				return this.UnsafeAccessibilityObject;
			}
			AccessibleObject accessibilityObject = this.GetAccessibilityObject(accObjId);
			if (accessibilityObject == null)
			{
				return null;
			}
			return Control.CreateInternalAccessibleObject(accessibilityObject);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002F510 File Offset: 0x0002D710
		protected virtual AccessibleObject GetAccessibilityObjectById(int objectId)
		{
			if (AccessibilityImprovements.Level3 && this is IAutomationLiveRegion)
			{
				return this.AccessibilityObject;
			}
			return null;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0002F529 File Offset: 0x0002D729
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x0002F540 File Offset: 0x0002D740
		[SRCategory("CatAccessibility")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlAccessibleDefaultActionDescr")]
		public string AccessibleDefaultActionDescription
		{
			get
			{
				return (string)this.Properties.GetObject(Control.PropAccessibleDefaultActionDescription);
			}
			set
			{
				this.Properties.SetObject(Control.PropAccessibleDefaultActionDescription, value);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0002F553 File Offset: 0x0002D753
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x0002F56A File Offset: 0x0002D76A
		[SRCategory("CatAccessibility")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ControlAccessibleDescriptionDescr")]
		public string AccessibleDescription
		{
			get
			{
				return (string)this.Properties.GetObject(Control.PropAccessibleDescription);
			}
			set
			{
				this.Properties.SetObject(Control.PropAccessibleDescription, value);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0002F57D File Offset: 0x0002D77D
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x0002F594 File Offset: 0x0002D794
		[SRCategory("CatAccessibility")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ControlAccessibleNameDescr")]
		public string AccessibleName
		{
			get
			{
				return (string)this.Properties.GetObject(Control.PropAccessibleName);
			}
			set
			{
				this.Properties.SetObject(Control.PropAccessibleName, value);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0002F5A8 File Offset: 0x0002D7A8
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x0002F5CE File Offset: 0x0002D7CE
		[SRCategory("CatAccessibility")]
		[DefaultValue(AccessibleRole.Default)]
		[SRDescription("ControlAccessibleRoleDescr")]
		public AccessibleRole AccessibleRole
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(Control.PropAccessibleRole, out flag);
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
				this.Properties.SetInteger(Control.PropAccessibleRole, (int)value);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0002F608 File Offset: 0x0002D808
		private Color ActiveXAmbientBackColor
		{
			get
			{
				return this.ActiveXInstance.AmbientBackColor;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0002F615 File Offset: 0x0002D815
		private Color ActiveXAmbientForeColor
		{
			get
			{
				return this.ActiveXInstance.AmbientForeColor;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0002F622 File Offset: 0x0002D822
		private Font ActiveXAmbientFont
		{
			get
			{
				return this.ActiveXInstance.AmbientFont;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0002F62F File Offset: 0x0002D82F
		private bool ActiveXEventsFrozen
		{
			get
			{
				return this.ActiveXInstance.EventsFrozen;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0002F63C File Offset: 0x0002D83C
		private IntPtr ActiveXHWNDParent
		{
			get
			{
				return this.ActiveXInstance.HWNDParent;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0002F64C File Offset: 0x0002D84C
		private Control.ActiveXImpl ActiveXInstance
		{
			get
			{
				Control.ActiveXImpl activeXImpl = (Control.ActiveXImpl)this.Properties.GetObject(Control.PropActiveXImpl);
				if (activeXImpl == null)
				{
					if (this.GetState(524288))
					{
						throw new NotSupportedException(SR.GetString("AXTopLevelSource"));
					}
					activeXImpl = new Control.ActiveXImpl(this);
					this.SetState2(1024, true);
					this.Properties.SetObject(Control.PropActiveXImpl, activeXImpl);
				}
				return activeXImpl;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0002F6B4 File Offset: 0x0002D8B4
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x0002F6C0 File Offset: 0x0002D8C0
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ControlAllowDropDescr")]
		public virtual bool AllowDrop
		{
			get
			{
				return this.GetState(64);
			}
			set
			{
				if (this.GetState(64) != value)
				{
					if (value && !this.IsHandleCreated)
					{
						IntSecurity.ClipboardRead.Demand();
					}
					this.SetState(64, value);
					if (this.IsHandleCreated)
					{
						try
						{
							this.SetAcceptDrops(value);
						}
						catch
						{
							this.SetState(64, !value);
							throw;
						}
					}
				}
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0002F728 File Offset: 0x0002D928
		private AmbientProperties AmbientPropertiesService
		{
			get
			{
				bool flag;
				AmbientProperties ambientProperties = (AmbientProperties)this.Properties.GetObject(Control.PropAmbientPropertiesService, out flag);
				if (!flag)
				{
					if (this.Site != null)
					{
						ambientProperties = (AmbientProperties)this.Site.GetService(typeof(AmbientProperties));
					}
					else
					{
						ambientProperties = (AmbientProperties)this.GetService(typeof(AmbientProperties));
					}
					if (ambientProperties != null)
					{
						this.Properties.SetObject(Control.PropAmbientPropertiesService, ambientProperties);
					}
				}
				return ambientProperties;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0002F7A0 File Offset: 0x0002D9A0
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x0002F7A8 File Offset: 0x0002D9A8
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		[SRDescription("ControlAnchorDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public virtual AnchorStyles Anchor
		{
			get
			{
				return DefaultLayout.GetAnchor(this);
			}
			set
			{
				DefaultLayout.SetAnchor(this.ParentInternal, this, value);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0002F7B7 File Offset: 0x0002D9B7
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x0002F7C0 File Offset: 0x0002D9C0
		[SRCategory("CatLayout")]
		[RefreshProperties(RefreshProperties.All)]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlAutoSizeDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool AutoSize
		{
			get
			{
				return CommonProperties.GetAutoSize(this);
			}
			set
			{
				if (value != this.AutoSize)
				{
					CommonProperties.SetAutoSize(this, value);
					if (this.ParentInternal != null)
					{
						if (value && this.ParentInternal.LayoutEngine == DefaultLayout.Instance)
						{
							this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.AutoSize);
					}
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06000F7E RID: 3966 RVA: 0x0002F829 File Offset: 0x0002DA29
		// (remove) Token: 0x06000F7F RID: 3967 RVA: 0x0002F83C File Offset: 0x0002DA3C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler AutoSizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventAutoSizeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventAutoSizeChanged, value);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0002F84F File Offset: 0x0002DA4F
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x0002F87E File Offset: 0x0002DA7E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(typeof(Point), "0, 0")]
		public virtual Point AutoScrollOffset
		{
			get
			{
				if (this.Properties.ContainsObject(Control.PropAutoScrollOffset))
				{
					return (Point)this.Properties.GetObject(Control.PropAutoScrollOffset);
				}
				return Point.Empty;
			}
			set
			{
				if (this.AutoScrollOffset != value)
				{
					this.Properties.SetObject(Control.PropAutoScrollOffset, value);
				}
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0002F8A4 File Offset: 0x0002DAA4
		protected void SetAutoSizeMode(AutoSizeMode mode)
		{
			CommonProperties.SetAutoSizeMode(this, mode);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0002F8AD File Offset: 0x0002DAAD
		protected AutoSizeMode GetAutoSizeMode()
		{
			return CommonProperties.GetAutoSizeMode(this);
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0002F8B5 File Offset: 0x0002DAB5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual LayoutEngine LayoutEngine
		{
			get
			{
				return DefaultLayout.Instance;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0002F8BC File Offset: 0x0002DABC
		internal IntPtr BackColorBrush
		{
			get
			{
				object @object = this.Properties.GetObject(Control.PropBackBrush);
				if (@object != null)
				{
					return (IntPtr)@object;
				}
				if (!this.Properties.ContainsObject(Control.PropBackColor) && this.parent != null && this.parent.BackColor == this.BackColor)
				{
					return this.parent.BackColorBrush;
				}
				Color backColor = this.BackColor;
				IntPtr intPtr;
				if (ColorTranslator.ToOle(backColor) < 0)
				{
					intPtr = SafeNativeMethods.GetSysColorBrush(ColorTranslator.ToOle(backColor) & 255);
					this.SetState(2097152, false);
				}
				else
				{
					intPtr = SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(backColor));
					this.SetState(2097152, true);
				}
				this.Properties.SetObject(Control.PropBackBrush, intPtr);
				return intPtr;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0002F984 File Offset: 0x0002DB84
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x0002FA0C File Offset: 0x0002DC0C
		[SRCategory("CatAppearance")]
		[DispId(-501)]
		[SRDescription("ControlBackColorDescr")]
		public virtual Color BackColor
		{
			get
			{
				Color color = this.RawBackColor;
				if (!color.IsEmpty)
				{
					return color;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null && parentInternal.CanAccessProperties)
				{
					color = parentInternal.BackColor;
					if (this.IsValidBackColor(color))
					{
						return color;
					}
				}
				if (this.IsActiveX)
				{
					color = this.ActiveXAmbientBackColor;
				}
				if (color.IsEmpty)
				{
					AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
					if (ambientPropertiesService != null)
					{
						color = ambientPropertiesService.BackColor;
					}
				}
				if (!color.IsEmpty && this.IsValidBackColor(color))
				{
					return color;
				}
				return Control.DefaultBackColor;
			}
			set
			{
				if (!value.Equals(Color.Empty) && !this.GetStyle(ControlStyles.SupportsTransparentBackColor) && value.A < 255)
				{
					throw new ArgumentException(SR.GetString("TransparentBackColorNotAllowed"));
				}
				Color backColor = this.BackColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(Control.PropBackColor))
				{
					this.Properties.SetColor(Control.PropBackColor, value);
				}
				if (!backColor.Equals(this.BackColor))
				{
					this.OnBackColorChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06000F88 RID: 3976 RVA: 0x0002FAB5 File Offset: 0x0002DCB5
		// (remove) Token: 0x06000F89 RID: 3977 RVA: 0x0002FAC8 File Offset: 0x0002DCC8
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnBackColorChangedDescr")]
		public event EventHandler BackColorChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventBackColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventBackColor, value);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0002FADB File Offset: 0x0002DCDB
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x0002FAF2 File Offset: 0x0002DCF2
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageDescr")]
		public virtual Image BackgroundImage
		{
			get
			{
				return (Image)this.Properties.GetObject(Control.PropBackgroundImage);
			}
			set
			{
				if (this.BackgroundImage != value)
				{
					this.Properties.SetObject(Control.PropBackgroundImage, value);
					this.OnBackgroundImageChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06000F8C RID: 3980 RVA: 0x0002FB19 File Offset: 0x0002DD19
		// (remove) Token: 0x06000F8D RID: 3981 RVA: 0x0002FB2C File Offset: 0x0002DD2C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnBackgroundImageChangedDescr")]
		public event EventHandler BackgroundImageChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventBackgroundImage, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventBackgroundImage, value);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0002FB40 File Offset: 0x0002DD40
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0002FB78 File Offset: 0x0002DD78
		[SRCategory("CatAppearance")]
		[DefaultValue(ImageLayout.Tile)]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public virtual ImageLayout BackgroundImageLayout
		{
			get
			{
				if (!this.Properties.ContainsObject(Control.PropBackgroundImageLayout))
				{
					return ImageLayout.Tile;
				}
				return (ImageLayout)this.Properties.GetObject(Control.PropBackgroundImageLayout);
			}
			set
			{
				if (this.BackgroundImageLayout != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ImageLayout));
					}
					if (value == ImageLayout.Center || value == ImageLayout.Zoom || value == ImageLayout.Stretch)
					{
						this.SetStyle(ControlStyles.ResizeRedraw, true);
						if (ControlPaint.IsImageTransparent(this.BackgroundImage))
						{
							this.DoubleBuffered = true;
						}
					}
					this.Properties.SetObject(Control.PropBackgroundImageLayout, value);
					this.OnBackgroundImageLayoutChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06000F90 RID: 3984 RVA: 0x0002FBFE File Offset: 0x0002DDFE
		// (remove) Token: 0x06000F91 RID: 3985 RVA: 0x0002FC11 File Offset: 0x0002DE11
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnBackgroundImageLayoutChangedDescr")]
		public event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventBackgroundImageLayout, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventBackgroundImageLayout, value);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0002FC24 File Offset: 0x0002DE24
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x0002FC2E File Offset: 0x0002DE2E
		internal bool BecomingActiveControl
		{
			get
			{
				return this.GetState2(32);
			}
			set
			{
				if (value != this.BecomingActiveControl)
				{
					Application.ThreadContext.FromCurrent().ActivatingControl = (value ? this : null);
					this.SetState2(32, value);
				}
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0002FC54 File Offset: 0x0002DE54
		private bool ShouldSerializeAccessibleName()
		{
			string accessibleName = this.AccessibleName;
			return accessibleName != null && accessibleName.Length > 0;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0002FC78 File Offset: 0x0002DE78
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetBindings()
		{
			ControlBindingsCollection controlBindingsCollection = (ControlBindingsCollection)this.Properties.GetObject(Control.PropBindings);
			if (controlBindingsCollection != null)
			{
				controlBindingsCollection.Clear();
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0002FCA4 File Offset: 0x0002DEA4
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x0002FCE8 File Offset: 0x0002DEE8
		internal BindingContext BindingContextInternal
		{
			get
			{
				BindingContext bindingContext = (BindingContext)this.Properties.GetObject(Control.PropBindingManager);
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
				BindingContext bindingContext = (BindingContext)this.Properties.GetObject(Control.PropBindingManager);
				if (bindingContext != value)
				{
					this.Properties.SetObject(Control.PropBindingManager, value);
					this.OnBindingContextChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0002FD2D File Offset: 0x0002DF2D
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x0002FD35 File Offset: 0x0002DF35
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlBindingContextDescr")]
		public virtual BindingContext BindingContext
		{
			get
			{
				return this.BindingContextInternal;
			}
			set
			{
				this.BindingContextInternal = value;
			}
		}

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06000F9A RID: 3994 RVA: 0x0002FD3E File Offset: 0x0002DF3E
		// (remove) Token: 0x06000F9B RID: 3995 RVA: 0x0002FD51 File Offset: 0x0002DF51
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnBindingContextChangedDescr")]
		public event EventHandler BindingContextChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventBindingContext, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventBindingContext, value);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0002FD64 File Offset: 0x0002DF64
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlBottomDescr")]
		[SRCategory("CatLayout")]
		public int Bottom
		{
			get
			{
				return this.y + this.height;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0002FD73 File Offset: 0x0002DF73
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x0002FD92 File Offset: 0x0002DF92
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlBoundsDescr")]
		[SRCategory("CatLayout")]
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(this.x, this.y, this.width, this.height);
			}
			set
			{
				this.SetBounds(value.X, value.Y, value.Width, value.Height, BoundsSpecified.All);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool CanAccessProperties
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0002FDB8 File Offset: 0x0002DFB8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatFocus")]
		[SRDescription("ControlCanFocusDescr")]
		public bool CanFocus
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					return false;
				}
				bool flag = SafeNativeMethods.IsWindowVisible(new HandleRef(this.window, this.Handle));
				bool flag2 = SafeNativeMethods.IsWindowEnabled(new HandleRef(this.window, this.Handle));
				return flag && flag2;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0002FE00 File Offset: 0x0002E000
		protected override bool CanRaiseEvents
		{
			get
			{
				return !this.IsActiveX || !this.ActiveXEventsFrozen;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0002FE15 File Offset: 0x0002E015
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatFocus")]
		[SRDescription("ControlCanSelectDescr")]
		public bool CanSelect
		{
			get
			{
				return this.CanSelectCore();
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0002FE1D File Offset: 0x0002E01D
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x0002FE25 File Offset: 0x0002E025
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatFocus")]
		[SRDescription("ControlCaptureDescr")]
		public bool Capture
		{
			get
			{
				return this.CaptureInternal;
			}
			set
			{
				if (value)
				{
					IntSecurity.GetCapture.Demand();
				}
				this.CaptureInternal = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0002FE3B File Offset: 0x0002E03B
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x0002FE57 File Offset: 0x0002E057
		internal bool CaptureInternal
		{
			get
			{
				return this.IsHandleCreated && UnsafeNativeMethods.GetCapture() == this.Handle;
			}
			set
			{
				if (this.CaptureInternal != value)
				{
					if (value)
					{
						UnsafeNativeMethods.SetCapture(new HandleRef(this, this.Handle));
						return;
					}
					SafeNativeMethods.ReleaseCapture();
				}
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0002FE7E File Offset: 0x0002E07E
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x0002FE8B File Offset: 0x0002E08B
		[SRCategory("CatFocus")]
		[DefaultValue(true)]
		[SRDescription("ControlCausesValidationDescr")]
		public bool CausesValidation
		{
			get
			{
				return this.GetState(131072);
			}
			set
			{
				if (value != this.CausesValidation)
				{
					this.SetState(131072, value);
					this.OnCausesValidationChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06000FA9 RID: 4009 RVA: 0x0002FEAD File Offset: 0x0002E0AD
		// (remove) Token: 0x06000FAA RID: 4010 RVA: 0x0002FEC0 File Offset: 0x0002E0C0
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnCausesValidationChangedDescr")]
		public event EventHandler CausesValidationChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventCausesValidation, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventCausesValidation, value);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0002FED4 File Offset: 0x0002E0D4
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0002FF08 File Offset: 0x0002E108
		internal bool CacheTextInternal
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(Control.PropCacheTextCount, out flag);
				return integer > 0 || this.GetStyle(ControlStyles.CacheText);
			}
			set
			{
				if (this.GetStyle(ControlStyles.CacheText) || !this.IsHandleCreated)
				{
					return;
				}
				bool flag;
				int num = this.Properties.GetInteger(Control.PropCacheTextCount, out flag);
				if (value)
				{
					if (num == 0)
					{
						this.Properties.SetObject(Control.PropCacheTextField, this.text);
						if (this.text == null)
						{
							this.text = this.WindowText;
						}
					}
					num++;
				}
				else
				{
					num--;
					if (num == 0)
					{
						this.text = (string)this.Properties.GetObject(Control.PropCacheTextField, out flag);
					}
				}
				this.Properties.SetInteger(Control.PropCacheTextCount, num);
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0002FFA9 File Offset: 0x0002E1A9
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x0002FFB0 File Offset: 0x0002E1B0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlCheckForIllegalCrossThreadCalls")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public static bool CheckForIllegalCrossThreadCalls
		{
			get
			{
				return Control.checkForIllegalCrossThreadCalls;
			}
			set
			{
				Control.checkForIllegalCrossThreadCalls = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatLayout")]
		[SRDescription("ControlClientRectangleDescr")]
		public Rectangle ClientRectangle
		{
			get
			{
				return new Rectangle(0, 0, this.clientWidth, this.clientHeight);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0002FFCD File Offset: 0x0002E1CD
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x0002FFE0 File Offset: 0x0002E1E0
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlClientSizeDescr")]
		public Size ClientSize
		{
			get
			{
				return new Size(this.clientWidth, this.clientHeight);
			}
			set
			{
				this.SetClientSizeCore(value.Width, value.Height);
			}
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06000FB2 RID: 4018 RVA: 0x0002FFF6 File Offset: 0x0002E1F6
		// (remove) Token: 0x06000FB3 RID: 4019 RVA: 0x00030009 File Offset: 0x0002E209
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnClientSizeChangedDescr")]
		public event EventHandler ClientSizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventClientSize, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventClientSize, value);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0003001C File Offset: 0x0002E21C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("ControlCompanyNameDescr")]
		public string CompanyName
		{
			get
			{
				return this.VersionInfo.CompanyName;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0003002C File Offset: 0x0002E22C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlContainsFocusDescr")]
		public bool ContainsFocus
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					return false;
				}
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				return !(focus == IntPtr.Zero) && (focus == this.Handle || UnsafeNativeMethods.IsChild(new HandleRef(this, this.Handle), new HandleRef(this, focus)));
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00030085 File Offset: 0x0002E285
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x0003009C File Offset: 0x0002E29C
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ControlContextMenuDescr")]
		[Browsable(false)]
		public virtual ContextMenu ContextMenu
		{
			get
			{
				return (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
			}
			set
			{
				ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
				if (contextMenu != value)
				{
					EventHandler value2 = new EventHandler(this.DetachContextMenu);
					if (contextMenu != null)
					{
						contextMenu.Disposed -= value2;
					}
					this.Properties.SetObject(Control.PropContextMenu, value);
					if (value != null)
					{
						value.Disposed += value2;
					}
					this.OnContextMenuChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06000FB8 RID: 4024 RVA: 0x00030100 File Offset: 0x0002E300
		// (remove) Token: 0x06000FB9 RID: 4025 RVA: 0x00030113 File Offset: 0x0002E313
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnContextMenuChangedDescr")]
		[Browsable(false)]
		public event EventHandler ContextMenuChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventContextMenu, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventContextMenu, value);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00030126 File Offset: 0x0002E326
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x00030140 File Offset: 0x0002E340
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ControlContextMenuDescr")]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return (ContextMenuStrip)this.Properties.GetObject(Control.PropContextMenuStrip);
			}
			set
			{
				ContextMenuStrip contextMenuStrip = this.Properties.GetObject(Control.PropContextMenuStrip) as ContextMenuStrip;
				if (contextMenuStrip != value)
				{
					EventHandler value2 = new EventHandler(this.DetachContextMenuStrip);
					if (contextMenuStrip != null)
					{
						contextMenuStrip.Disposed -= value2;
					}
					this.Properties.SetObject(Control.PropContextMenuStrip, value);
					if (value != null)
					{
						value.Disposed += value2;
					}
					this.OnContextMenuStripChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06000FBC RID: 4028 RVA: 0x000301A4 File Offset: 0x0002E3A4
		// (remove) Token: 0x06000FBD RID: 4029 RVA: 0x000301B7 File Offset: 0x0002E3B7
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlContextMenuStripChangedDescr")]
		public event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventContextMenuStrip, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventContextMenuStrip, value);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000301CC File Offset: 0x0002E3CC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("ControlControlsDescr")]
		public Control.ControlCollection Controls
		{
			get
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection == null)
				{
					controlCollection = this.CreateControlsInstance();
					this.Properties.SetObject(Control.PropControlsCollection, controlCollection);
				}
				return controlCollection;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0003020B File Offset: 0x0002E40B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlCreatedDescr")]
		public bool Created
		{
			get
			{
				return (this.state & 1) != 0;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00030218 File Offset: 0x0002E418
		protected virtual CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (Control.needToLoadComCtl)
				{
					if (!(UnsafeNativeMethods.GetModuleHandle("comctl32.dll") != IntPtr.Zero) && !(UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable("comctl32.dll") != IntPtr.Zero))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						throw new Win32Exception(lastWin32Error, SR.GetString("LoadDLLError", new object[]
						{
							"comctl32.dll"
						}));
					}
					Control.needToLoadComCtl = false;
				}
				if (this.createParams == null)
				{
					this.createParams = new CreateParams();
				}
				CreateParams createParams = this.createParams;
				createParams.Style = 0;
				createParams.ExStyle = 0;
				createParams.ClassStyle = 0;
				createParams.Caption = this.text;
				createParams.X = this.x;
				createParams.Y = this.y;
				createParams.Width = this.width;
				createParams.Height = this.height;
				createParams.Style = 33554432;
				if (this.GetStyle(ControlStyles.ContainerControl))
				{
					createParams.ExStyle |= 65536;
				}
				createParams.ClassStyle = 8;
				if ((this.state & 524288) == 0)
				{
					createParams.Parent = ((this.parent == null) ? IntPtr.Zero : this.parent.InternalHandle);
					createParams.Style |= 1140850688;
				}
				else
				{
					createParams.Parent = IntPtr.Zero;
				}
				if ((this.state & 8) != 0)
				{
					createParams.Style |= 65536;
				}
				if ((this.state & 2) != 0)
				{
					createParams.Style |= 268435456;
				}
				if (!this.Enabled)
				{
					createParams.Style |= 134217728;
				}
				if (createParams.Parent == IntPtr.Zero && this.IsActiveX)
				{
					createParams.Parent = this.ActiveXHWNDParent;
				}
				if (this.RightToLeft == RightToLeft.Yes)
				{
					createParams.ExStyle |= 8192;
					createParams.ExStyle |= 4096;
					createParams.ExStyle |= 16384;
				}
				return createParams;
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00030422 File Offset: 0x0002E622
		internal virtual void NotifyValidationResult(object sender, CancelEventArgs ev)
		{
			this.ValidationCancelled = ev.Cancel;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00030430 File Offset: 0x0002E630
		internal bool ValidateActiveControl(out bool validatedControlAllowsFocusChange)
		{
			bool result = true;
			validatedControlAllowsFocusChange = false;
			IContainerControl containerControlInternal = this.GetContainerControlInternal();
			if (containerControlInternal != null && this.CausesValidation)
			{
				ContainerControl containerControl = containerControlInternal as ContainerControl;
				if (containerControl != null)
				{
					while (containerControl.ActiveControl == null)
					{
						Control parentInternal = containerControl.ParentInternal;
						if (parentInternal == null)
						{
							break;
						}
						ContainerControl containerControl2 = parentInternal.GetContainerControlInternal() as ContainerControl;
						if (containerControl2 == null)
						{
							break;
						}
						containerControl = containerControl2;
					}
					result = containerControl.ValidateInternal(true, out validatedControlAllowsFocusChange);
				}
			}
			return result;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x000304A0 File Offset: 0x0002E6A0
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x00030490 File Offset: 0x0002E690
		internal bool ValidationCancelled
		{
			get
			{
				if (this.GetState(268435456))
				{
					return true;
				}
				Control parentInternal = this.ParentInternal;
				return parentInternal != null && parentInternal.ValidationCancelled;
			}
			set
			{
				this.SetState(268435456, value);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x000304DC File Offset: 0x0002E6DC
		// (set) Token: 0x06000FC5 RID: 4037 RVA: 0x000304CE File Offset: 0x0002E6CE
		internal bool IsTopMdiWindowClosing
		{
			get
			{
				return this.GetState2(4096);
			}
			set
			{
				this.SetState2(4096, value);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x000304F7 File Offset: 0x0002E6F7
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x000304E9 File Offset: 0x0002E6E9
		internal bool IsCurrentlyBeingScaled
		{
			get
			{
				return this.GetState2(8192);
			}
			private set
			{
				this.SetState2(8192, value);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00030504 File Offset: 0x0002E704
		internal int CreateThreadId
		{
			get
			{
				if (this.IsHandleCreated)
				{
					int num;
					return SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, this.Handle), out num);
				}
				return SafeNativeMethods.GetCurrentThreadId();
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00030534 File Offset: 0x0002E734
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x000305BC File Offset: 0x0002E7BC
		[SRCategory("CatAppearance")]
		[SRDescription("ControlCursorDescr")]
		[AmbientValue(null)]
		public virtual Cursor Cursor
		{
			get
			{
				if (this.GetState(1024))
				{
					return Cursors.WaitCursor;
				}
				Cursor cursor = (Cursor)this.Properties.GetObject(Control.PropCursor);
				if (cursor != null)
				{
					return cursor;
				}
				Cursor defaultCursor = this.DefaultCursor;
				if (defaultCursor != Cursors.Default)
				{
					return defaultCursor;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					return parentInternal.Cursor;
				}
				AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
				if (ambientPropertiesService != null && ambientPropertiesService.Cursor != null)
				{
					return ambientPropertiesService.Cursor;
				}
				return defaultCursor;
			}
			set
			{
				Cursor left = (Cursor)this.Properties.GetObject(Control.PropCursor);
				Cursor cursor = this.Cursor;
				if (left != value)
				{
					IntSecurity.ModifyCursor.Demand();
					this.Properties.SetObject(Control.PropCursor, value);
				}
				if (this.IsHandleCreated)
				{
					NativeMethods.POINT point = new NativeMethods.POINT();
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.GetCursorPos(point);
					UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), ref rect);
					if ((rect.left <= point.x && point.x < rect.right && rect.top <= point.y && point.y < rect.bottom) || UnsafeNativeMethods.GetCapture() == this.Handle)
					{
						this.SendMessage(32, this.Handle, (IntPtr)1);
					}
				}
				if (!cursor.Equals(value))
				{
					this.OnCursorChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06000FCC RID: 4044 RVA: 0x000306B1 File Offset: 0x0002E8B1
		// (remove) Token: 0x06000FCD RID: 4045 RVA: 0x000306C4 File Offset: 0x0002E8C4
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnCursorChangedDescr")]
		public event EventHandler CursorChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventCursor, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventCursor, value);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x000306D8 File Offset: 0x0002E8D8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatData")]
		[SRDescription("ControlBindingsDescr")]
		[RefreshProperties(RefreshProperties.All)]
		[ParenthesizePropertyName(true)]
		public ControlBindingsCollection DataBindings
		{
			get
			{
				ControlBindingsCollection controlBindingsCollection = (ControlBindingsCollection)this.Properties.GetObject(Control.PropBindings);
				if (controlBindingsCollection == null)
				{
					controlBindingsCollection = new ControlBindingsCollection(this);
					this.Properties.SetObject(Control.PropBindings, controlBindingsCollection);
				}
				return controlBindingsCollection;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00030717 File Offset: 0x0002E917
		public static Color DefaultBackColor
		{
			get
			{
				return SystemColors.Control;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0003071E File Offset: 0x0002E91E
		protected virtual Cursor DefaultCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00030725 File Offset: 0x0002E925
		public static Font DefaultFont
		{
			get
			{
				if (Control.defaultFont == null)
				{
					Control.defaultFont = SystemFonts.DefaultFont;
				}
				return Control.defaultFont;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0003073D File Offset: 0x0002E93D
		public static Color DefaultForeColor
		{
			get
			{
				return SystemColors.ControlText;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00030744 File Offset: 0x0002E944
		protected virtual Padding DefaultMargin
		{
			get
			{
				return CommonProperties.DefaultMargin;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0003074B File Offset: 0x0002E94B
		protected virtual Size DefaultMaximumSize
		{
			get
			{
				return CommonProperties.DefaultMaximumSize;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00030752 File Offset: 0x0002E952
		protected virtual Size DefaultMinimumSize
		{
			get
			{
				return CommonProperties.DefaultMinimumSize;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected virtual Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00011A20 File Offset: 0x0000FC20
		private RightToLeft DefaultRightToLeft
		{
			get
			{
				return RightToLeft.No;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00030759 File Offset: 0x0002E959
		protected virtual Size DefaultSize
		{
			get
			{
				return Size.Empty;
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00030760 File Offset: 0x0002E960
		private void DetachContextMenu(object sender, EventArgs e)
		{
			this.ContextMenu = null;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00030769 File Offset: 0x0002E969
		private void DetachContextMenuStrip(object sender, EventArgs e)
		{
			this.ContextMenuStrip = null;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00030772 File Offset: 0x0002E972
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int DeviceDpi
		{
			get
			{
				if (DpiHelper.EnableDpiChangedMessageHandling)
				{
					return this.deviceDpi;
				}
				return DpiHelper.DeviceDpi;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00030788 File Offset: 0x0002E988
		internal Color DisabledColor
		{
			get
			{
				Color result = this.BackColor;
				if (result.A == 0)
				{
					Control parentInternal = this.ParentInternal;
					while (result.A == 0)
					{
						if (parentInternal == null)
						{
							result = SystemColors.Control;
							break;
						}
						result = parentInternal.BackColor;
						parentInternal = parentInternal.ParentInternal;
					}
				}
				return result;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlDisplayRectangleDescr")]
		public virtual Rectangle DisplayRectangle
		{
			get
			{
				return new Rectangle(0, 0, this.clientWidth, this.clientHeight);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x000307D1 File Offset: 0x0002E9D1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlDisposedDescr")]
		public bool IsDisposed
		{
			get
			{
				return this.GetState(2048);
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000307E0 File Offset: 0x0002E9E0
		private void DisposeFontHandle()
		{
			if (this.Properties.ContainsObject(Control.PropFontHandleWrapper))
			{
				Control.FontHandleWrapper fontHandleWrapper = this.Properties.GetObject(Control.PropFontHandleWrapper) as Control.FontHandleWrapper;
				if (fontHandleWrapper != null)
				{
					fontHandleWrapper.Dispose();
				}
				this.Properties.SetObject(Control.PropFontHandleWrapper, null);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0003082F File Offset: 0x0002EA2F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlDisposingDescr")]
		public bool Disposing
		{
			get
			{
				return this.GetState(4096);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0003083C File Offset: 0x0002EA3C
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x00030844 File Offset: 0x0002EA44
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(DockStyle.None)]
		[SRDescription("ControlDockDescr")]
		public virtual DockStyle Dock
		{
			get
			{
				return DefaultLayout.GetDock(this);
			}
			set
			{
				if (value != this.Dock)
				{
					this.SuspendLayout();
					try
					{
						DefaultLayout.SetDock(this, value);
						this.OnDockChanged(EventArgs.Empty);
					}
					finally
					{
						this.ResumeLayout();
					}
				}
			}
		}

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06000FE3 RID: 4067 RVA: 0x0003088C File Offset: 0x0002EA8C
		// (remove) Token: 0x06000FE4 RID: 4068 RVA: 0x0003089F File Offset: 0x0002EA9F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnDockChangedDescr")]
		public event EventHandler DockChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventDock, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDock, value);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x000308B2 File Offset: 0x0002EAB2
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x000308BF File Offset: 0x0002EABF
		[SRCategory("CatBehavior")]
		[SRDescription("ControlDoubleBufferedDescr")]
		protected virtual bool DoubleBuffered
		{
			get
			{
				return this.GetStyle(ControlStyles.OptimizedDoubleBuffer);
			}
			set
			{
				if (value != this.DoubleBuffered)
				{
					if (value)
					{
						this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value);
						return;
					}
					this.SetStyle(ControlStyles.OptimizedDoubleBuffer, value);
				}
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x000308E6 File Offset: 0x0002EAE6
		private bool DoubleBufferingEnabled
		{
			get
			{
				return this.GetStyle(ControlStyles.UserPaint | ControlStyles.DoubleBuffer);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x000308F3 File Offset: 0x0002EAF3
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x00030918 File Offset: 0x0002EB18
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DispId(-514)]
		[SRDescription("ControlEnabledDescr")]
		public bool Enabled
		{
			get
			{
				return this.GetState(4) && (this.ParentInternal == null || this.ParentInternal.Enabled);
			}
			set
			{
				bool enabled = this.Enabled;
				this.SetState(4, value);
				if (enabled != value)
				{
					if (!value)
					{
						this.SelectNextIfFocused();
					}
					this.OnEnabledChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06000FEA RID: 4074 RVA: 0x0003094C File Offset: 0x0002EB4C
		// (remove) Token: 0x06000FEB RID: 4075 RVA: 0x0003095F File Offset: 0x0002EB5F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnEnabledChangedDescr")]
		public event EventHandler EnabledChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventEnabled, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventEnabled, value);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00030972 File Offset: 0x0002EB72
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlFocusedDescr")]
		public virtual bool Focused
		{
			get
			{
				return this.IsHandleCreated && UnsafeNativeMethods.GetFocus() == this.Handle;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00030990 File Offset: 0x0002EB90
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x000309F8 File Offset: 0x0002EBF8
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DispId(-512)]
		[AmbientValue(null)]
		[SRDescription("ControlFontDescr")]
		public virtual Font Font
		{
			[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Windows.Forms.Control/ActiveXFontMarshaler)]
			get
			{
				Font font = (Font)this.Properties.GetObject(Control.PropFont);
				if (font != null)
				{
					return font;
				}
				Font font2 = this.GetParentFont();
				if (font2 != null)
				{
					return font2;
				}
				if (this.IsActiveX)
				{
					font2 = this.ActiveXAmbientFont;
					if (font2 != null)
					{
						return font2;
					}
				}
				AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
				if (ambientPropertiesService != null && ambientPropertiesService.Font != null)
				{
					return ambientPropertiesService.Font;
				}
				return Control.DefaultFont;
			}
			[param: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Windows.Forms.Control/ActiveXFontMarshaler)]
			set
			{
				Font font = (Font)this.Properties.GetObject(Control.PropFont);
				Font font2 = this.Font;
				bool flag = false;
				if (value == null)
				{
					if (font != null)
					{
						flag = true;
					}
				}
				else
				{
					flag = (font == null || !value.Equals(font));
				}
				if (flag)
				{
					this.Properties.SetObject(Control.PropFont, value);
					if (!font2.Equals(value))
					{
						this.DisposeFontHandle();
						if (this.Properties.ContainsInteger(Control.PropFontHeight))
						{
							this.Properties.SetInteger(Control.PropFontHeight, (value == null) ? -1 : value.Height);
						}
						using (new LayoutTransaction(this.ParentInternal, this, PropertyNames.Font))
						{
							this.OnFontChanged(EventArgs.Empty);
							return;
						}
					}
					if (this.IsHandleCreated && !this.GetStyle(ControlStyles.UserPaint))
					{
						this.DisposeFontHandle();
						this.SetWindowFont();
					}
				}
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00030AEC File Offset: 0x0002ECEC
		internal void ScaleFont(float factor)
		{
			Font font = (Font)this.Properties.GetObject(Control.PropFont);
			Font font2 = this.Font;
			Font font3 = DpiHelper.EnableDpiChangedHighDpiImprovements ? new Font(this.Font.FontFamily, this.Font.Size * factor, this.Font.Style, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont) : new Font(this.Font.FontFamily, this.Font.Size * factor, this.Font.Style);
			if (font == null || !font.Equals(font3))
			{
				this.Properties.SetObject(Control.PropFont, font3);
				if (!font2.Equals(font3))
				{
					this.DisposeFontHandle();
					if (this.Properties.ContainsInteger(Control.PropFontHeight))
					{
						this.Properties.SetInteger(Control.PropFontHeight, font3.Height);
					}
				}
			}
		}

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06000FF0 RID: 4080 RVA: 0x00030BE5 File Offset: 0x0002EDE5
		// (remove) Token: 0x06000FF1 RID: 4081 RVA: 0x00030BF8 File Offset: 0x0002EDF8
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnFontChangedDescr")]
		public event EventHandler FontChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventFont, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventFont, value);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00030C0C File Offset: 0x0002EE0C
		internal IntPtr FontHandle
		{
			get
			{
				Font font = (Font)this.Properties.GetObject(Control.PropFont);
				if (font != null)
				{
					Control.FontHandleWrapper fontHandleWrapper = (Control.FontHandleWrapper)this.Properties.GetObject(Control.PropFontHandleWrapper);
					if (fontHandleWrapper == null)
					{
						fontHandleWrapper = new Control.FontHandleWrapper(font);
						this.Properties.SetObject(Control.PropFontHandleWrapper, fontHandleWrapper);
					}
					return fontHandleWrapper.Handle;
				}
				if (this.parent != null)
				{
					return this.parent.FontHandle;
				}
				AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
				if (ambientPropertiesService != null && ambientPropertiesService.Font != null)
				{
					Control.FontHandleWrapper fontHandleWrapper2 = null;
					Font font2 = (Font)this.Properties.GetObject(Control.PropCurrentAmbientFont);
					if (font2 != null && font2 == ambientPropertiesService.Font)
					{
						fontHandleWrapper2 = (Control.FontHandleWrapper)this.Properties.GetObject(Control.PropFontHandleWrapper);
					}
					else
					{
						this.Properties.SetObject(Control.PropCurrentAmbientFont, ambientPropertiesService.Font);
					}
					if (fontHandleWrapper2 == null)
					{
						font = ambientPropertiesService.Font;
						fontHandleWrapper2 = new Control.FontHandleWrapper(font);
						this.Properties.SetObject(Control.PropFontHandleWrapper, fontHandleWrapper2);
					}
					return fontHandleWrapper2.Handle;
				}
				return Control.GetDefaultFontHandleWrapper().Handle;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00030D1C File Offset: 0x0002EF1C
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x00030DBD File Offset: 0x0002EFBD
		protected int FontHeight
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(Control.PropFontHeight, out flag);
				if (flag && integer != -1)
				{
					return integer;
				}
				Font font = (Font)this.Properties.GetObject(Control.PropFont);
				if (font != null)
				{
					integer = font.Height;
					this.Properties.SetInteger(Control.PropFontHeight, integer);
					return integer;
				}
				int num = -1;
				if (this.ParentInternal != null && this.ParentInternal.CanAccessProperties)
				{
					num = this.ParentInternal.FontHeight;
				}
				if (num == -1)
				{
					num = this.Font.Height;
					this.Properties.SetInteger(Control.PropFontHeight, num);
				}
				return num;
			}
			set
			{
				this.Properties.SetInteger(Control.PropFontHeight, value);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00030DD0 File Offset: 0x0002EFD0
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x00030E54 File Offset: 0x0002F054
		[SRCategory("CatAppearance")]
		[DispId(-513)]
		[SRDescription("ControlForeColorDescr")]
		public virtual Color ForeColor
		{
			get
			{
				Color color = this.Properties.GetColor(Control.PropForeColor);
				if (!color.IsEmpty)
				{
					return color;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null && parentInternal.CanAccessProperties)
				{
					return parentInternal.ForeColor;
				}
				Color result = Color.Empty;
				if (this.IsActiveX)
				{
					result = this.ActiveXAmbientForeColor;
				}
				if (result.IsEmpty)
				{
					AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
					if (ambientPropertiesService != null)
					{
						result = ambientPropertiesService.ForeColor;
					}
				}
				if (!result.IsEmpty)
				{
					return result;
				}
				return Control.DefaultForeColor;
			}
			set
			{
				Color foreColor = this.ForeColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(Control.PropForeColor))
				{
					this.Properties.SetColor(Control.PropForeColor, value);
				}
				if (!foreColor.Equals(this.ForeColor))
				{
					this.OnForeColorChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06000FF7 RID: 4087 RVA: 0x00030EB9 File Offset: 0x0002F0B9
		// (remove) Token: 0x06000FF8 RID: 4088 RVA: 0x00030ECC File Offset: 0x0002F0CC
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnForeColorChangedDescr")]
		public event EventHandler ForeColorChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventForeColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventForeColor, value);
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00030EDF File Offset: 0x0002F0DF
		private Font GetParentFont()
		{
			if (this.ParentInternal != null && this.ParentInternal.CanAccessProperties)
			{
				return this.ParentInternal.Font;
			}
			return null;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00030F04 File Offset: 0x0002F104
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual Size GetPreferredSize(Size proposedSize)
		{
			Size size;
			if (this.GetState(6144))
			{
				size = CommonProperties.xGetPreferredSizeCache(this);
			}
			else
			{
				proposedSize = LayoutUtils.ConvertZeroToUnbounded(proposedSize);
				proposedSize = this.ApplySizeConstraints(proposedSize);
				if (this.GetState2(2048))
				{
					Size result = CommonProperties.xGetPreferredSizeCache(this);
					if (!result.IsEmpty && proposedSize == LayoutUtils.MaxSize)
					{
						return result;
					}
				}
				this.CacheTextInternal = true;
				try
				{
					size = this.GetPreferredSizeCore(proposedSize);
				}
				finally
				{
					this.CacheTextInternal = false;
				}
				size = this.ApplySizeConstraints(size);
				if (this.GetState2(2048) && proposedSize == LayoutUtils.MaxSize)
				{
					CommonProperties.xSetPreferredSizeCache(this, size);
				}
			}
			return size;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00030FB8 File Offset: 0x0002F1B8
		internal virtual Size GetPreferredSizeCore(Size proposedSize)
		{
			return CommonProperties.GetSpecifiedBounds(this).Size;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00030FD4 File Offset: 0x0002F1D4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DispId(-515)]
		[SRDescription("ControlHandleDescr")]
		public IntPtr Handle
		{
			get
			{
				if (Control.checkForIllegalCrossThreadCalls && !Control.inCrossThreadSafeCall && this.InvokeRequired)
				{
					throw new InvalidOperationException(SR.GetString("IllegalCrossThreadCall", new object[]
					{
						this.Name
					}));
				}
				if (!this.IsHandleCreated)
				{
					this.CreateHandle();
				}
				return this.HandleInternal;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0003102A File Offset: 0x0002F22A
		internal IntPtr HandleInternal
		{
			get
			{
				return this.window.Handle;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00031038 File Offset: 0x0002F238
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHasChildrenDescr")]
		public bool HasChildren
		{
			get
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				return controlCollection != null && controlCollection.Count > 0;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool HasMenu
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00031069 File Offset: 0x0002F269
		// (set) Token: 0x06001001 RID: 4097 RVA: 0x00031071 File Offset: 0x0002F271
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHeightDescr")]
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.SetBounds(this.x, this.y, this.width, value, BoundsSpecified.Height);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x00031090 File Offset: 0x0002F290
		internal bool HostedInWin32DialogManager
		{
			get
			{
				if (!this.GetState(16777216))
				{
					Control topMostParent = this.TopMostParent;
					if (this != topMostParent)
					{
						this.SetState(33554432, topMostParent.HostedInWin32DialogManager);
					}
					else
					{
						IntPtr intPtr = UnsafeNativeMethods.GetParent(new HandleRef(this, this.Handle));
						IntPtr handle = intPtr;
						StringBuilder stringBuilder = new StringBuilder(32);
						this.SetState(33554432, false);
						while (intPtr != IntPtr.Zero)
						{
							int className = UnsafeNativeMethods.GetClassName(new HandleRef(null, handle), null, 0);
							if (className > stringBuilder.Capacity)
							{
								stringBuilder.Capacity = className + 5;
							}
							UnsafeNativeMethods.GetClassName(new HandleRef(null, handle), stringBuilder, stringBuilder.Capacity);
							if (stringBuilder.ToString() == "#32770")
							{
								this.SetState(33554432, true);
								break;
							}
							handle = intPtr;
							intPtr = UnsafeNativeMethods.GetParent(new HandleRef(null, intPtr));
						}
					}
					this.SetState(16777216, true);
				}
				return this.GetState(33554432);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00031183 File Offset: 0x0002F383
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHandleCreatedDescr")]
		public bool IsHandleCreated
		{
			get
			{
				return this.window.Handle != IntPtr.Zero;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0003119A File Offset: 0x0002F39A
		internal bool IsLayoutSuspended
		{
			get
			{
				return this.layoutSuspendCount > 0;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x000311A8 File Offset: 0x0002F3A8
		internal bool IsWindowObscured
		{
			get
			{
				if (!this.IsHandleCreated || !this.Visible)
				{
					return false;
				}
				bool result = false;
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					while (parentInternal.ParentInternal != null)
					{
						parentInternal = parentInternal.ParentInternal;
					}
				}
				UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), ref rect);
				Region region = new Region(Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom));
				try
				{
					IntPtr handle;
					if (parentInternal != null)
					{
						handle = parentInternal.Handle;
					}
					else
					{
						handle = this.Handle;
					}
					IntPtr handle2 = handle;
					IntPtr intPtr;
					while ((intPtr = UnsafeNativeMethods.GetWindow(new HandleRef(null, handle2), 3)) != IntPtr.Zero)
					{
						UnsafeNativeMethods.GetWindowRect(new HandleRef(null, intPtr), ref rect);
						Rectangle rect2 = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
						if (SafeNativeMethods.IsWindowVisible(new HandleRef(null, intPtr)))
						{
							region.Exclude(rect2);
						}
						handle2 = intPtr;
					}
					Graphics graphics = this.CreateGraphics();
					try
					{
						result = region.IsEmpty(graphics);
					}
					finally
					{
						graphics.Dispose();
					}
				}
				finally
				{
					region.Dispose();
				}
				return result;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x000312E4 File Offset: 0x0002F4E4
		internal IntPtr InternalHandle
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					return IntPtr.Zero;
				}
				return this.Handle;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x000312FC File Offset: 0x0002F4FC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlInvokeRequiredDescr")]
		public bool InvokeRequired
		{
			get
			{
				bool result;
				using (new Control.MultithreadSafeCallScope())
				{
					HandleRef hWnd;
					if (this.IsHandleCreated)
					{
						hWnd = new HandleRef(this, this.Handle);
					}
					else
					{
						Control control = this.FindMarshalingControl();
						if (!control.IsHandleCreated)
						{
							return false;
						}
						hWnd = new HandleRef(control, control.Handle);
					}
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(hWnd, out num);
					int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
					result = (windowThreadProcessId != currentThreadId);
				}
				return result;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00031388 File Offset: 0x0002F588
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x00031395 File Offset: 0x0002F595
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlIsAccessibleDescr")]
		public bool IsAccessible
		{
			get
			{
				return this.GetState(1048576);
			}
			set
			{
				this.SetState(1048576, value);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x000313A3 File Offset: 0x0002F5A3
		internal bool IsAccessibilityObjectCreated
		{
			get
			{
				return this.Properties.GetObject(Control.PropAccessibility) is AccessibleObject;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000313BD File Offset: 0x0002F5BD
		internal bool IsInternalAccessibilityObjectCreated
		{
			get
			{
				return this.Properties.GetObject(Control.PropUnsafeAccessibility) is InternalAccessibleObject;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x000313D7 File Offset: 0x0002F5D7
		internal bool IsActiveX
		{
			get
			{
				return this.GetState2(1024);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool IsContainerControl
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x000313E4 File Offset: 0x0002F5E4
		internal bool IsIEParent
		{
			get
			{
				return this.IsActiveX && this.ActiveXInstance.IsIE;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x000313FC File Offset: 0x0002F5FC
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("IsMirroredDescr")]
		public bool IsMirrored
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					CreateParams createParams = this.CreateParams;
					this.SetState(1073741824, (createParams.ExStyle & 4194304) != 0);
				}
				return this.GetState(1073741824);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool IsMnemonicsListenerAxSourced
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0003143D File Offset: 0x0002F63D
		private bool IsValidBackColor(Color c)
		{
			return c.IsEmpty || this.GetStyle(ControlStyles.SupportsTransparentBackColor) || c.A >= byte.MaxValue;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00031466 File Offset: 0x0002F666
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x0003146E File Offset: 0x0002F66E
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlLeftDescr")]
		public int Left
		{
			get
			{
				return this.x;
			}
			set
			{
				this.SetBounds(value, this.y, this.width, this.height, BoundsSpecified.X);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0003148A File Offset: 0x0002F68A
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x0003149D File Offset: 0x0002F69D
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlLocationDescr")]
		public Point Location
		{
			get
			{
				return new Point(this.x, this.y);
			}
			set
			{
				this.SetBounds(value.X, value.Y, this.width, this.height, BoundsSpecified.Location);
			}
		}

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06001016 RID: 4118 RVA: 0x000314C0 File Offset: 0x0002F6C0
		// (remove) Token: 0x06001017 RID: 4119 RVA: 0x000314D3 File Offset: 0x0002F6D3
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnLocationChangedDescr")]
		public event EventHandler LocationChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventLocation, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventLocation, value);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00019C19 File Offset: 0x00017E19
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x000314E6 File Offset: 0x0002F6E6
		[SRDescription("ControlMarginDescr")]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		public Padding Margin
		{
			get
			{
				return CommonProperties.GetMargin(this);
			}
			set
			{
				value = LayoutUtils.ClampNegativePaddingToZero(value);
				if (value != this.Margin)
				{
					CommonProperties.SetMargin(this, value);
					this.OnMarginChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x0600101A RID: 4122 RVA: 0x00031510 File Offset: 0x0002F710
		// (remove) Token: 0x0600101B RID: 4123 RVA: 0x00031523 File Offset: 0x0002F723
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnMarginChangedDescr")]
		public event EventHandler MarginChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventMarginChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMarginChanged, value);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00031536 File Offset: 0x0002F736
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x00031544 File Offset: 0x0002F744
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlMaximumSizeDescr")]
		[AmbientValue(typeof(Size), "0, 0")]
		public virtual Size MaximumSize
		{
			get
			{
				return CommonProperties.GetMaximumSize(this, this.DefaultMaximumSize);
			}
			set
			{
				if (value == Size.Empty)
				{
					CommonProperties.ClearMaximumSize(this);
					return;
				}
				if (value != this.MaximumSize)
				{
					CommonProperties.SetMaximumSize(this, value);
				}
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003156F File Offset: 0x0002F76F
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0003157D File Offset: 0x0002F77D
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlMinimumSizeDescr")]
		public virtual Size MinimumSize
		{
			get
			{
				return CommonProperties.GetMinimumSize(this, this.DefaultMinimumSize);
			}
			set
			{
				if (value != this.MinimumSize)
				{
					CommonProperties.SetMinimumSize(this, value);
				}
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00031594 File Offset: 0x0002F794
		public static Keys ModifierKeys
		{
			get
			{
				Keys keys = Keys.None;
				if (UnsafeNativeMethods.GetKeyState(16) < 0)
				{
					keys |= Keys.Shift;
				}
				if (UnsafeNativeMethods.GetKeyState(17) < 0)
				{
					keys |= Keys.Control;
				}
				if (UnsafeNativeMethods.GetKeyState(18) < 0)
				{
					keys |= Keys.Alt;
				}
				return keys;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x000315DC File Offset: 0x0002F7DC
		public static MouseButtons MouseButtons
		{
			get
			{
				MouseButtons mouseButtons = MouseButtons.None;
				if (UnsafeNativeMethods.GetKeyState(1) < 0)
				{
					mouseButtons |= MouseButtons.Left;
				}
				if (UnsafeNativeMethods.GetKeyState(2) < 0)
				{
					mouseButtons |= MouseButtons.Right;
				}
				if (UnsafeNativeMethods.GetKeyState(4) < 0)
				{
					mouseButtons |= MouseButtons.Middle;
				}
				if (UnsafeNativeMethods.GetKeyState(5) < 0)
				{
					mouseButtons |= MouseButtons.XButton1;
				}
				if (UnsafeNativeMethods.GetKeyState(6) < 0)
				{
					mouseButtons |= MouseButtons.XButton2;
				}
				return mouseButtons;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00031644 File Offset: 0x0002F844
		public static Point MousePosition
		{
			get
			{
				NativeMethods.POINT point = new NativeMethods.POINT();
				UnsafeNativeMethods.GetCursorPos(point);
				return new Point(point.x, point.y);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00031670 File Offset: 0x0002F870
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x000316B9 File Offset: 0x0002F8B9
		[Browsable(false)]
		public string Name
		{
			get
			{
				string text = (string)this.Properties.GetObject(Control.PropName);
				if (string.IsNullOrEmpty(text))
				{
					if (this.Site != null)
					{
						text = this.Site.Name;
					}
					if (text == null)
					{
						text = "";
					}
				}
				return text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Properties.SetObject(Control.PropName, null);
					return;
				}
				this.Properties.SetObject(Control.PropName, value);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x000316E6 File Offset: 0x0002F8E6
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x000316F8 File Offset: 0x0002F8F8
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlParentDescr")]
		public Control Parent
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.ParentInternal;
			}
			set
			{
				this.ParentInternal = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00031701 File Offset: 0x0002F901
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x00031709 File Offset: 0x0002F909
		internal virtual Control ParentInternal
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != value)
				{
					if (value != null)
					{
						value.Controls.Add(this);
						return;
					}
					this.parent.Controls.Remove(this);
				}
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x00031735 File Offset: 0x0002F935
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlProductNameDescr")]
		public string ProductName
		{
			get
			{
				return this.VersionInfo.ProductName;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00031742 File Offset: 0x0002F942
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlProductVersionDescr")]
		public string ProductVersion
		{
			get
			{
				return this.VersionInfo.ProductVersion;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0003174F File Offset: 0x0002F94F
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00031757 File Offset: 0x0002F957
		internal Color RawBackColor
		{
			get
			{
				return this.Properties.GetColor(Control.PropBackColor);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00031769 File Offset: 0x0002F969
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlRecreatingHandleDescr")]
		public bool RecreatingHandle
		{
			get
			{
				return (this.state & 16) != 0;
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void AddReflectChild()
		{
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void RemoveReflectChild()
		{
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00031777 File Offset: 0x0002F977
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x00031780 File Offset: 0x0002F980
		private Control ReflectParent
		{
			get
			{
				return this.reflectParent;
			}
			set
			{
				if (value != null)
				{
					value.AddReflectChild();
				}
				Control control = this.ReflectParent;
				this.reflectParent = value;
				if (control != null)
				{
					control.RemoveReflectChild();
				}
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x000317AD File Offset: 0x0002F9AD
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x000317C4 File Offset: 0x0002F9C4
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlRegionDescr")]
		public Region Region
		{
			get
			{
				return (Region)this.Properties.GetObject(Control.PropRegion);
			}
			set
			{
				if (this.GetState(524288))
				{
					IntSecurity.ChangeWindowRegionForTopLevel.Demand();
				}
				Region region = this.Region;
				if (region != value)
				{
					this.Properties.SetObject(Control.PropRegion, value);
					if (region != null)
					{
						region.Dispose();
					}
					if (this.IsHandleCreated)
					{
						IntPtr intPtr = IntPtr.Zero;
						try
						{
							if (value != null)
							{
								intPtr = this.GetHRgn(value);
							}
							if (this.IsActiveX)
							{
								intPtr = this.ActiveXMergeRegion(intPtr);
							}
							if (UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, this.Handle), new HandleRef(this, intPtr), SafeNativeMethods.IsWindowVisible(new HandleRef(this, this.Handle))) != 0)
							{
								intPtr = IntPtr.Zero;
							}
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
							}
						}
					}
					this.OnRegionChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06001034 RID: 4148 RVA: 0x000318A4 File Offset: 0x0002FAA4
		// (remove) Token: 0x06001035 RID: 4149 RVA: 0x000318B7 File Offset: 0x0002FAB7
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlRegionChangedDescr")]
		public event EventHandler RegionChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventRegionChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventRegionChanged, value);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x00013062 File Offset: 0x00011262
		[Obsolete("This property has been deprecated. Please use RightToLeft instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected internal bool RenderRightToLeft
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x000318CC File Offset: 0x0002FACC
		internal bool RenderTransparent
		{
			get
			{
				return this.GetStyle(ControlStyles.SupportsTransparentBackColor) && this.BackColor.A < byte.MaxValue;
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000318FD File Offset: 0x0002FAFD
		private bool RenderColorTransparent(Color c)
		{
			return this.GetStyle(ControlStyles.SupportsTransparentBackColor) && c.A < byte.MaxValue;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool RenderTransparencyWithVisualStyles
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0003191C File Offset: 0x0002FB1C
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x00031934 File Offset: 0x0002FB34
		internal BoundsSpecified RequiredScaling
		{
			get
			{
				if ((this.requiredScaling & 16) != 0)
				{
					return (BoundsSpecified)(this.requiredScaling & 15);
				}
				return BoundsSpecified.None;
			}
			set
			{
				byte b = this.requiredScaling & 16;
				this.requiredScaling = (byte)((value & BoundsSpecified.All) | (BoundsSpecified)b);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x00031959 File Offset: 0x0002FB59
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x00031968 File Offset: 0x0002FB68
		internal bool RequiredScalingEnabled
		{
			get
			{
				return (this.requiredScaling & 16) > 0;
			}
			set
			{
				byte b = this.requiredScaling & 15;
				this.requiredScaling = b;
				if (value)
				{
					this.requiredScaling |= 16;
				}
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0003199A File Offset: 0x0002FB9A
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x000319A4 File Offset: 0x0002FBA4
		[SRDescription("ControlResizeRedrawDescr")]
		protected bool ResizeRedraw
		{
			get
			{
				return this.GetStyle(ControlStyles.ResizeRedraw);
			}
			set
			{
				this.SetStyle(ControlStyles.ResizeRedraw, value);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x000319AF File Offset: 0x0002FBAF
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlRightDescr")]
		public int Right
		{
			get
			{
				return this.x + this.width;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x000319C0 File Offset: 0x0002FBC0
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x00031A04 File Offset: 0x0002FC04
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[AmbientValue(RightToLeft.Inherit)]
		[SRDescription("ControlRightToLeftDescr")]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				bool flag;
				int num = this.Properties.GetInteger(Control.PropRightToLeft, out flag);
				if (!flag)
				{
					num = 2;
				}
				if (num == 2)
				{
					Control parentInternal = this.ParentInternal;
					if (parentInternal != null)
					{
						num = (int)parentInternal.RightToLeft;
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
				if (this.Properties.ContainsInteger(Control.PropRightToLeft) || value != RightToLeft.Inherit)
				{
					this.Properties.SetInteger(Control.PropRightToLeft, (int)value);
				}
				if (rightToLeft != this.RightToLeft)
				{
					using (new LayoutTransaction(this, this, PropertyNames.RightToLeft))
					{
						this.OnRightToLeftChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x06001043 RID: 4163 RVA: 0x00031AA4 File Offset: 0x0002FCA4
		// (remove) Token: 0x06001044 RID: 4164 RVA: 0x00031AB7 File Offset: 0x0002FCB7
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftChangedDescr")]
		public event EventHandler RightToLeftChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventRightToLeft, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventRightToLeft, value);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00013062 File Offset: 0x00011262
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual bool ScaleChildren
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x00031ACA File Offset: 0x0002FCCA
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x00031AD4 File Offset: 0x0002FCD4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
				AmbientProperties ambientProperties = null;
				if (value != null)
				{
					ambientProperties = (AmbientProperties)value.GetService(typeof(AmbientProperties));
				}
				if (ambientPropertiesService != ambientProperties)
				{
					bool flag = !this.Properties.ContainsObject(Control.PropFont);
					bool flag2 = !this.Properties.ContainsObject(Control.PropBackColor);
					bool flag3 = !this.Properties.ContainsObject(Control.PropForeColor);
					bool flag4 = !this.Properties.ContainsObject(Control.PropCursor);
					Font font = null;
					Color color = Color.Empty;
					Color color2 = Color.Empty;
					Cursor cursor = null;
					if (flag)
					{
						font = this.Font;
					}
					if (flag2)
					{
						color = this.BackColor;
					}
					if (flag3)
					{
						color2 = this.ForeColor;
					}
					if (flag4)
					{
						cursor = this.Cursor;
					}
					this.Properties.SetObject(Control.PropAmbientPropertiesService, ambientProperties);
					base.Site = value;
					if (flag && !font.Equals(this.Font))
					{
						this.OnFontChanged(EventArgs.Empty);
					}
					if (flag3 && !color2.Equals(this.ForeColor))
					{
						this.OnForeColorChanged(EventArgs.Empty);
					}
					if (flag2 && !color.Equals(this.BackColor))
					{
						this.OnBackColorChanged(EventArgs.Empty);
					}
					if (flag4 && cursor.Equals(this.Cursor))
					{
						this.OnCursorChanged(EventArgs.Empty);
						return;
					}
				}
				else
				{
					base.Site = value;
				}
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00031C4A File Offset: 0x0002FE4A
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x00031C5D File Offset: 0x0002FE5D
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlSizeDescr")]
		public Size Size
		{
			get
			{
				return new Size(this.width, this.height);
			}
			set
			{
				this.SetBounds(this.x, this.y, value.Width, value.Height, BoundsSpecified.Size);
			}
		}

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x0600104A RID: 4170 RVA: 0x00031C81 File Offset: 0x0002FE81
		// (remove) Token: 0x0600104B RID: 4171 RVA: 0x00031C94 File Offset: 0x0002FE94
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnSizeChangedDescr")]
		public event EventHandler SizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventSize, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventSize, value);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00031CA7 File Offset: 0x0002FEA7
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x00031CBC File Offset: 0x0002FEBC
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[MergableProperty(false)]
		[SRDescription("ControlTabIndexDescr")]
		public int TabIndex
		{
			get
			{
				if (this.tabIndex != -1)
				{
					return this.tabIndex;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("TabIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"TabIndex",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.tabIndex != value)
				{
					this.tabIndex = value;
					this.OnTabIndexChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x0600104E RID: 4174 RVA: 0x00031D2B File Offset: 0x0002FF2B
		// (remove) Token: 0x0600104F RID: 4175 RVA: 0x00031D3E File Offset: 0x0002FF3E
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnTabIndexChangedDescr")]
		public event EventHandler TabIndexChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventTabIndex, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventTabIndex, value);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00031D51 File Offset: 0x0002FF51
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x00031D59 File Offset: 0x0002FF59
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[DispId(-516)]
		[SRDescription("ControlTabStopDescr")]
		public bool TabStop
		{
			get
			{
				return this.TabStopInternal;
			}
			set
			{
				if (this.TabStop != value)
				{
					this.TabStopInternal = value;
					if (this.IsHandleCreated)
					{
						this.SetWindowStyle(65536, value);
					}
					this.OnTabStopChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00031D8A File Offset: 0x0002FF8A
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x00031D97 File Offset: 0x0002FF97
		internal bool TabStopInternal
		{
			get
			{
				return (this.state & 8) != 0;
			}
			set
			{
				if (this.TabStopInternal != value)
				{
					this.SetState(8, value);
				}
			}
		}

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06001054 RID: 4180 RVA: 0x00031DAA File Offset: 0x0002FFAA
		// (remove) Token: 0x06001055 RID: 4181 RVA: 0x00031DBD File Offset: 0x0002FFBD
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnTabStopChangedDescr")]
		public event EventHandler TabStopChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventTabStop, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventTabStop, value);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x00031DD0 File Offset: 0x0002FFD0
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x00031DE2 File Offset: 0x0002FFE2
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.Properties.GetObject(Control.PropUserData);
			}
			set
			{
				this.Properties.SetObject(Control.PropUserData, value);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00031DF5 File Offset: 0x0002FFF5
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x00031E1C File Offset: 0x0003001C
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[Bindable(true)]
		[DispId(-517)]
		[SRDescription("ControlTextDescr")]
		public virtual string Text
		{
			get
			{
				if (!this.CacheTextInternal)
				{
					return this.WindowText;
				}
				if (this.text != null)
				{
					return this.text;
				}
				return "";
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == this.Text)
				{
					return;
				}
				if (this.CacheTextInternal)
				{
					this.text = value;
				}
				this.WindowText = value;
				this.OnTextChanged(EventArgs.Empty);
				if (this.IsMnemonicsListenerAxSourced)
				{
					for (Control control = this; control != null; control = control.ParentInternal)
					{
						Control.ActiveXImpl activeXImpl = (Control.ActiveXImpl)control.Properties.GetObject(Control.PropActiveXImpl);
						if (activeXImpl != null)
						{
							activeXImpl.UpdateAccelTable();
							return;
						}
					}
				}
			}
		}

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x0600105A RID: 4186 RVA: 0x00031E99 File Offset: 0x00030099
		// (remove) Token: 0x0600105B RID: 4187 RVA: 0x00031EAC File Offset: 0x000300AC
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnTextChangedDescr")]
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventText, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventText, value);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00031EBF File Offset: 0x000300BF
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x00031EC7 File Offset: 0x000300C7
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlTopDescr")]
		public int Top
		{
			get
			{
				return this.y;
			}
			set
			{
				this.SetBounds(this.x, value, this.width, this.height, BoundsSpecified.Y);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00031EE3 File Offset: 0x000300E3
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlTopLevelControlDescr")]
		public Control TopLevelControl
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.TopLevelControlInternal;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00031EF8 File Offset: 0x000300F8
		internal Control TopLevelControlInternal
		{
			get
			{
				Control control = this;
				while (control != null && !control.GetTopLevel())
				{
					control = control.ParentInternal;
				}
				return control;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00031F1C File Offset: 0x0003011C
		internal Control TopMostParent
		{
			get
			{
				Control control = this;
				while (control.ParentInternal != null)
				{
					control = control.ParentInternal;
				}
				return control;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00031F3D File Offset: 0x0003013D
		private BufferedGraphicsContext BufferContext
		{
			get
			{
				return BufferedGraphicsManager.Current;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00031F44 File Offset: 0x00030144
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal virtual bool ShowKeyboardCues
		{
			get
			{
				if (!this.IsHandleCreated || base.DesignMode)
				{
					return true;
				}
				if ((this.uiCuesState & 240) == 0)
				{
					if (SystemInformation.MenuAccessKeysUnderlined)
					{
						this.uiCuesState |= 32;
					}
					else
					{
						int num = (2 | (AccessibilityImprovements.Level1 ? 0 : 1)) << 16;
						this.uiCuesState |= 16;
						UnsafeNativeMethods.SendMessage(new HandleRef(this.TopMostParent, this.TopMostParent.Handle), 295, (IntPtr)(num | 1), IntPtr.Zero);
					}
				}
				return (this.uiCuesState & 240) == 32;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00031FE8 File Offset: 0x000301E8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal virtual bool ShowFocusCues
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					return true;
				}
				if ((this.uiCuesState & 15) == 0)
				{
					if (SystemInformation.MenuAccessKeysUnderlined)
					{
						this.uiCuesState |= 2;
					}
					else
					{
						this.uiCuesState |= 1;
						int num = 196608;
						UnsafeNativeMethods.SendMessage(new HandleRef(this.TopMostParent, this.TopMostParent.Handle), 295, (IntPtr)(num | 1), IntPtr.Zero);
					}
				}
				return (this.uiCuesState & 15) == 2;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0003206F File Offset: 0x0003026F
		internal virtual int ShowParams
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00032072 File Offset: 0x00030272
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x00032080 File Offset: 0x00030280
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ControlUseWaitCursorDescr")]
		public bool UseWaitCursor
		{
			get
			{
				return this.GetState(1024);
			}
			set
			{
				if (this.GetState(1024) != value)
				{
					this.SetState(1024, value);
					Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection != null)
					{
						for (int i = 0; i < controlCollection.Count; i++)
						{
							controlCollection[i].UseWaitCursor = value;
						}
					}
				}
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x000320E0 File Offset: 0x000302E0
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x00032120 File Offset: 0x00030320
		internal bool UseCompatibleTextRenderingInt
		{
			get
			{
				if (this.Properties.ContainsInteger(Control.PropUseCompatibleTextRendering))
				{
					bool flag;
					int integer = this.Properties.GetInteger(Control.PropUseCompatibleTextRendering, out flag);
					if (flag)
					{
						return integer == 1;
					}
				}
				return Control.UseCompatibleTextRenderingDefault;
			}
			set
			{
				if (this.SupportsUseCompatibleTextRendering && this.UseCompatibleTextRenderingInt != value)
				{
					this.Properties.SetInteger(Control.PropUseCompatibleTextRendering, value ? 1 : 0);
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.UseCompatibleTextRendering);
					this.Invalidate();
				}
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00032174 File Offset: 0x00030374
		private Control.ControlVersionInfo VersionInfo
		{
			get
			{
				Control.ControlVersionInfo controlVersionInfo = (Control.ControlVersionInfo)this.Properties.GetObject(Control.PropControlVersionInfo);
				if (controlVersionInfo == null)
				{
					controlVersionInfo = new Control.ControlVersionInfo(this);
					this.Properties.SetObject(Control.PropControlVersionInfo, controlVersionInfo);
				}
				return controlVersionInfo;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x000321B3 File Offset: 0x000303B3
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x000321BB File Offset: 0x000303BB
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ControlVisibleDescr")]
		public bool Visible
		{
			get
			{
				return this.GetVisibleCore();
			}
			set
			{
				this.SetVisibleCore(value);
			}
		}

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x0600106D RID: 4205 RVA: 0x000321C4 File Offset: 0x000303C4
		// (remove) Token: 0x0600106E RID: 4206 RVA: 0x000321D7 File Offset: 0x000303D7
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnVisibleChangedDescr")]
		public event EventHandler VisibleChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventVisible, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventVisible, value);
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x000321EC File Offset: 0x000303EC
		private void WaitForWaitHandle(WaitHandle waitHandle)
		{
			int createThreadId = this.CreateThreadId;
			Application.ThreadContext threadContext = Application.ThreadContext.FromId(createThreadId);
			if (threadContext == null)
			{
				return;
			}
			IntPtr handle = threadContext.GetHandle();
			bool flag = false;
			uint num = 0U;
			while (!flag)
			{
				bool exitCodeThread = UnsafeNativeMethods.GetExitCodeThread(handle, out num);
				if ((exitCodeThread && num != 259U) || (!exitCodeThread && Marshal.GetLastWin32Error() == 6) || AppDomain.CurrentDomain.IsFinalizingForUnload())
				{
					if (!waitHandle.WaitOne(1, false))
					{
						throw new InvalidAsynchronousStateException(SR.GetString("ThreadNoLongerValid"));
					}
					break;
				}
				else
				{
					if (this.IsDisposed && this.threadCallbackList != null && this.threadCallbackList.Count > 0)
					{
						Queue obj = this.threadCallbackList;
						lock (obj)
						{
							Exception exception = new ObjectDisposedException(base.GetType().Name);
							while (this.threadCallbackList.Count > 0)
							{
								Control.ThreadMethodEntry threadMethodEntry = (Control.ThreadMethodEntry)this.threadCallbackList.Dequeue();
								threadMethodEntry.exception = exception;
								threadMethodEntry.Complete();
							}
						}
					}
					flag = waitHandle.WaitOne(1000, false);
				}
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00032314 File Offset: 0x00030514
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0003231C File Offset: 0x0003051C
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlWidthDescr")]
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.SetBounds(this.x, this.y, value, this.height, BoundsSpecified.Width);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00032338 File Offset: 0x00030538
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x00032353 File Offset: 0x00030553
		private int WindowExStyle
		{
			get
			{
				return (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -20));
			}
			set
			{
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -20, new HandleRef(null, (IntPtr)value));
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00032375 File Offset: 0x00030575
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x00032390 File Offset: 0x00030590
		internal int WindowStyle
		{
			get
			{
				return (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -16));
			}
			set
			{
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -16, new HandleRef(null, (IntPtr)value));
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x000323B2 File Offset: 0x000305B2
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x000323BF File Offset: 0x000305BF
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlWindowTargetDescr")]
		public IWindowTarget WindowTarget
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.window.WindowTarget;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				this.window.WindowTarget = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x000323D0 File Offset: 0x000305D0
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x00032470 File Offset: 0x00030670
		internal virtual string WindowText
		{
			get
			{
				if (this.IsHandleCreated)
				{
					string result;
					using (new Control.MultithreadSafeCallScope())
					{
						int num = SafeNativeMethods.GetWindowTextLength(new HandleRef(this.window, this.Handle));
						if (SystemInformation.DbcsEnabled)
						{
							num = num * 2 + 1;
						}
						StringBuilder stringBuilder = new StringBuilder(num + 1);
						UnsafeNativeMethods.GetWindowText(new HandleRef(this.window, this.Handle), stringBuilder, stringBuilder.Capacity);
						result = stringBuilder.ToString();
					}
					return result;
				}
				if (this.text == null)
				{
					return "";
				}
				return this.text;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.WindowText.Equals(value))
				{
					if (this.IsHandleCreated)
					{
						UnsafeNativeMethods.SetWindowText(new HandleRef(this.window, this.Handle), value);
						return;
					}
					if (value.Length == 0)
					{
						this.text = null;
						return;
					}
					this.text = value;
				}
			}
		}

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x0600107A RID: 4218 RVA: 0x000324CD File Offset: 0x000306CD
		// (remove) Token: 0x0600107B RID: 4219 RVA: 0x000324E0 File Offset: 0x000306E0
		[SRCategory("CatAction")]
		[SRDescription("ControlOnClickDescr")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(Control.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventClick, value);
			}
		}

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x0600107C RID: 4220 RVA: 0x000324F3 File Offset: 0x000306F3
		// (remove) Token: 0x0600107D RID: 4221 RVA: 0x00032506 File Offset: 0x00030706
		[SRCategory("CatBehavior")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnControlAddedDescr")]
		public event ControlEventHandler ControlAdded
		{
			add
			{
				base.Events.AddHandler(Control.EventControlAdded, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventControlAdded, value);
			}
		}

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x0600107E RID: 4222 RVA: 0x00032519 File Offset: 0x00030719
		// (remove) Token: 0x0600107F RID: 4223 RVA: 0x0003252C File Offset: 0x0003072C
		[SRCategory("CatBehavior")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnControlRemovedDescr")]
		public event ControlEventHandler ControlRemoved
		{
			add
			{
				base.Events.AddHandler(Control.EventControlRemoved, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventControlRemoved, value);
			}
		}

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06001080 RID: 4224 RVA: 0x0003253F File Offset: 0x0003073F
		// (remove) Token: 0x06001081 RID: 4225 RVA: 0x00032552 File Offset: 0x00030752
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnDragDropDescr")]
		public event DragEventHandler DragDrop
		{
			add
			{
				base.Events.AddHandler(Control.EventDragDrop, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDragDrop, value);
			}
		}

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06001082 RID: 4226 RVA: 0x00032565 File Offset: 0x00030765
		// (remove) Token: 0x06001083 RID: 4227 RVA: 0x00032578 File Offset: 0x00030778
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnDragEnterDescr")]
		public event DragEventHandler DragEnter
		{
			add
			{
				base.Events.AddHandler(Control.EventDragEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDragEnter, value);
			}
		}

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06001084 RID: 4228 RVA: 0x0003258B File Offset: 0x0003078B
		// (remove) Token: 0x06001085 RID: 4229 RVA: 0x0003259E File Offset: 0x0003079E
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnDragOverDescr")]
		public event DragEventHandler DragOver
		{
			add
			{
				base.Events.AddHandler(Control.EventDragOver, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDragOver, value);
			}
		}

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06001086 RID: 4230 RVA: 0x000325B1 File Offset: 0x000307B1
		// (remove) Token: 0x06001087 RID: 4231 RVA: 0x000325C4 File Offset: 0x000307C4
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnDragLeaveDescr")]
		public event EventHandler DragLeave
		{
			add
			{
				base.Events.AddHandler(Control.EventDragLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDragLeave, value);
			}
		}

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06001088 RID: 4232 RVA: 0x000325D7 File Offset: 0x000307D7
		// (remove) Token: 0x06001089 RID: 4233 RVA: 0x000325EA File Offset: 0x000307EA
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnGiveFeedbackDescr")]
		public event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				base.Events.AddHandler(Control.EventGiveFeedback, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventGiveFeedback, value);
			}
		}

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x0600108A RID: 4234 RVA: 0x000325FD File Offset: 0x000307FD
		// (remove) Token: 0x0600108B RID: 4235 RVA: 0x00032610 File Offset: 0x00030810
		[SRCategory("CatPrivate")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnCreateHandleDescr")]
		public event EventHandler HandleCreated
		{
			add
			{
				base.Events.AddHandler(Control.EventHandleCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventHandleCreated, value);
			}
		}

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x0600108C RID: 4236 RVA: 0x00032623 File Offset: 0x00030823
		// (remove) Token: 0x0600108D RID: 4237 RVA: 0x00032636 File Offset: 0x00030836
		[SRCategory("CatPrivate")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnDestroyHandleDescr")]
		public event EventHandler HandleDestroyed
		{
			add
			{
				base.Events.AddHandler(Control.EventHandleDestroyed, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventHandleDestroyed, value);
			}
		}

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600108E RID: 4238 RVA: 0x00032649 File Offset: 0x00030849
		// (remove) Token: 0x0600108F RID: 4239 RVA: 0x0003265C File Offset: 0x0003085C
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnHelpDescr")]
		public event HelpEventHandler HelpRequested
		{
			add
			{
				base.Events.AddHandler(Control.EventHelpRequested, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventHelpRequested, value);
			}
		}

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x06001090 RID: 4240 RVA: 0x0003266F File Offset: 0x0003086F
		// (remove) Token: 0x06001091 RID: 4241 RVA: 0x00032682 File Offset: 0x00030882
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnInvalidateDescr")]
		public event InvalidateEventHandler Invalidated
		{
			add
			{
				base.Events.AddHandler(Control.EventInvalidated, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventInvalidated, value);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00032695 File Offset: 0x00030895
		[Browsable(false)]
		public Size PreferredSize
		{
			get
			{
				return this.GetPreferredSize(Size.Empty);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x000326A2 File Offset: 0x000308A2
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x000326B0 File Offset: 0x000308B0
		[SRDescription("ControlPaddingDescr")]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		public Padding Padding
		{
			get
			{
				return CommonProperties.GetPadding(this, this.DefaultPadding);
			}
			set
			{
				if (value != this.Padding)
				{
					CommonProperties.SetPadding(this, value);
					this.SetState(8388608, true);
					using (new LayoutTransaction(this.ParentInternal, this, PropertyNames.Padding))
					{
						this.OnPaddingChanged(EventArgs.Empty);
					}
					if (this.GetState(8388608))
					{
						LayoutTransaction.DoLayout(this, this, PropertyNames.Padding);
					}
				}
			}
		}

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06001095 RID: 4245 RVA: 0x00032730 File Offset: 0x00030930
		// (remove) Token: 0x06001096 RID: 4246 RVA: 0x00032743 File Offset: 0x00030943
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnPaddingChangedDescr")]
		public event EventHandler PaddingChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventPaddingChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventPaddingChanged, value);
			}
		}

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06001097 RID: 4247 RVA: 0x00032756 File Offset: 0x00030956
		// (remove) Token: 0x06001098 RID: 4248 RVA: 0x00032769 File Offset: 0x00030969
		[SRCategory("CatAppearance")]
		[SRDescription("ControlOnPaintDescr")]
		public event PaintEventHandler Paint
		{
			add
			{
				base.Events.AddHandler(Control.EventPaint, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventPaint, value);
			}
		}

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x06001099 RID: 4249 RVA: 0x0003277C File Offset: 0x0003097C
		// (remove) Token: 0x0600109A RID: 4250 RVA: 0x0003278F File Offset: 0x0003098F
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnQueryContinueDragDescr")]
		public event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				base.Events.AddHandler(Control.EventQueryContinueDrag, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventQueryContinueDrag, value);
			}
		}

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x0600109B RID: 4251 RVA: 0x000327A2 File Offset: 0x000309A2
		// (remove) Token: 0x0600109C RID: 4252 RVA: 0x000327B5 File Offset: 0x000309B5
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnQueryAccessibilityHelpDescr")]
		public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				base.Events.AddHandler(Control.EventQueryAccessibilityHelp, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventQueryAccessibilityHelp, value);
			}
		}

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x0600109D RID: 4253 RVA: 0x000327C8 File Offset: 0x000309C8
		// (remove) Token: 0x0600109E RID: 4254 RVA: 0x000327DB File Offset: 0x000309DB
		[SRCategory("CatAction")]
		[SRDescription("ControlOnDoubleClickDescr")]
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(Control.EventDoubleClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDoubleClick, value);
			}
		}

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x0600109F RID: 4255 RVA: 0x000327EE File Offset: 0x000309EE
		// (remove) Token: 0x060010A0 RID: 4256 RVA: 0x00032801 File Offset: 0x00030A01
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnEnterDescr")]
		public event EventHandler Enter
		{
			add
			{
				base.Events.AddHandler(Control.EventEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventEnter, value);
			}
		}

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x060010A1 RID: 4257 RVA: 0x00032814 File Offset: 0x00030A14
		// (remove) Token: 0x060010A2 RID: 4258 RVA: 0x00032827 File Offset: 0x00030A27
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnGotFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler GotFocus
		{
			add
			{
				base.Events.AddHandler(Control.EventGotFocus, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventGotFocus, value);
			}
		}

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x060010A3 RID: 4259 RVA: 0x0003283A File Offset: 0x00030A3A
		// (remove) Token: 0x060010A4 RID: 4260 RVA: 0x0003284D File Offset: 0x00030A4D
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyDownDescr")]
		public event KeyEventHandler KeyDown
		{
			add
			{
				base.Events.AddHandler(Control.EventKeyDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventKeyDown, value);
			}
		}

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x060010A5 RID: 4261 RVA: 0x00032860 File Offset: 0x00030A60
		// (remove) Token: 0x060010A6 RID: 4262 RVA: 0x00032873 File Offset: 0x00030A73
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyPressDescr")]
		public event KeyPressEventHandler KeyPress
		{
			add
			{
				base.Events.AddHandler(Control.EventKeyPress, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventKeyPress, value);
			}
		}

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x060010A7 RID: 4263 RVA: 0x00032886 File Offset: 0x00030A86
		// (remove) Token: 0x060010A8 RID: 4264 RVA: 0x00032899 File Offset: 0x00030A99
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyUpDescr")]
		public event KeyEventHandler KeyUp
		{
			add
			{
				base.Events.AddHandler(Control.EventKeyUp, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventKeyUp, value);
			}
		}

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x060010A9 RID: 4265 RVA: 0x000328AC File Offset: 0x00030AAC
		// (remove) Token: 0x060010AA RID: 4266 RVA: 0x000328BF File Offset: 0x00030ABF
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnLayoutDescr")]
		public event LayoutEventHandler Layout
		{
			add
			{
				base.Events.AddHandler(Control.EventLayout, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventLayout, value);
			}
		}

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x060010AB RID: 4267 RVA: 0x000328D2 File Offset: 0x00030AD2
		// (remove) Token: 0x060010AC RID: 4268 RVA: 0x000328E5 File Offset: 0x00030AE5
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnLeaveDescr")]
		public event EventHandler Leave
		{
			add
			{
				base.Events.AddHandler(Control.EventLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventLeave, value);
			}
		}

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x060010AD RID: 4269 RVA: 0x000328F8 File Offset: 0x00030AF8
		// (remove) Token: 0x060010AE RID: 4270 RVA: 0x0003290B File Offset: 0x00030B0B
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnLostFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler LostFocus
		{
			add
			{
				base.Events.AddHandler(Control.EventLostFocus, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventLostFocus, value);
			}
		}

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x060010AF RID: 4271 RVA: 0x0003291E File Offset: 0x00030B1E
		// (remove) Token: 0x060010B0 RID: 4272 RVA: 0x00032931 File Offset: 0x00030B31
		[SRCategory("CatAction")]
		[SRDescription("ControlOnMouseClickDescr")]
		public event MouseEventHandler MouseClick
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseClick, value);
			}
		}

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x060010B1 RID: 4273 RVA: 0x00032944 File Offset: 0x00030B44
		// (remove) Token: 0x060010B2 RID: 4274 RVA: 0x00032957 File Offset: 0x00030B57
		[SRCategory("CatAction")]
		[SRDescription("ControlOnMouseDoubleClickDescr")]
		public event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseDoubleClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseDoubleClick, value);
			}
		}

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x060010B3 RID: 4275 RVA: 0x0003296A File Offset: 0x00030B6A
		// (remove) Token: 0x060010B4 RID: 4276 RVA: 0x0003297D File Offset: 0x00030B7D
		[SRCategory("CatAction")]
		[SRDescription("ControlOnMouseCaptureChangedDescr")]
		public event EventHandler MouseCaptureChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseCaptureChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseCaptureChanged, value);
			}
		}

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x060010B5 RID: 4277 RVA: 0x00032990 File Offset: 0x00030B90
		// (remove) Token: 0x060010B6 RID: 4278 RVA: 0x000329A3 File Offset: 0x00030BA3
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseDownDescr")]
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseDown, value);
			}
		}

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x060010B7 RID: 4279 RVA: 0x000329B6 File Offset: 0x00030BB6
		// (remove) Token: 0x060010B8 RID: 4280 RVA: 0x000329C9 File Offset: 0x00030BC9
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseEnterDescr")]
		public event EventHandler MouseEnter
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseEnter, value);
			}
		}

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x060010B9 RID: 4281 RVA: 0x000329DC File Offset: 0x00030BDC
		// (remove) Token: 0x060010BA RID: 4282 RVA: 0x000329EF File Offset: 0x00030BEF
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseLeaveDescr")]
		public event EventHandler MouseLeave
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseLeave, value);
			}
		}

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x060010BB RID: 4283 RVA: 0x00032A02 File Offset: 0x00030C02
		// (remove) Token: 0x060010BC RID: 4284 RVA: 0x00032A15 File Offset: 0x00030C15
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnDpiChangedBeforeParentDescr")]
		public event EventHandler DpiChangedBeforeParent
		{
			add
			{
				base.Events.AddHandler(Control.EventDpiChangedBeforeParent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDpiChangedBeforeParent, value);
			}
		}

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x060010BD RID: 4285 RVA: 0x00032A28 File Offset: 0x00030C28
		// (remove) Token: 0x060010BE RID: 4286 RVA: 0x00032A3B File Offset: 0x00030C3B
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnDpiChangedAfterParentDescr")]
		public event EventHandler DpiChangedAfterParent
		{
			add
			{
				base.Events.AddHandler(Control.EventDpiChangedAfterParent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDpiChangedAfterParent, value);
			}
		}

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x060010BF RID: 4287 RVA: 0x00032A4E File Offset: 0x00030C4E
		// (remove) Token: 0x060010C0 RID: 4288 RVA: 0x00032A61 File Offset: 0x00030C61
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseHoverDescr")]
		public event EventHandler MouseHover
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseHover, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseHover, value);
			}
		}

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x060010C1 RID: 4289 RVA: 0x00032A74 File Offset: 0x00030C74
		// (remove) Token: 0x060010C2 RID: 4290 RVA: 0x00032A87 File Offset: 0x00030C87
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseMoveDescr")]
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseMove, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseMove, value);
			}
		}

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x060010C3 RID: 4291 RVA: 0x00032A9A File Offset: 0x00030C9A
		// (remove) Token: 0x060010C4 RID: 4292 RVA: 0x00032AAD File Offset: 0x00030CAD
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseUpDescr")]
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseUp, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseUp, value);
			}
		}

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x060010C5 RID: 4293 RVA: 0x00032AC0 File Offset: 0x00030CC0
		// (remove) Token: 0x060010C6 RID: 4294 RVA: 0x00032AD3 File Offset: 0x00030CD3
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseWheelDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event MouseEventHandler MouseWheel
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseWheel, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseWheel, value);
			}
		}

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x060010C7 RID: 4295 RVA: 0x00032AE6 File Offset: 0x00030CE6
		// (remove) Token: 0x060010C8 RID: 4296 RVA: 0x00032AF9 File Offset: 0x00030CF9
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnMoveDescr")]
		public event EventHandler Move
		{
			add
			{
				base.Events.AddHandler(Control.EventMove, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMove, value);
			}
		}

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x060010C9 RID: 4297 RVA: 0x00032B0C File Offset: 0x00030D0C
		// (remove) Token: 0x060010CA RID: 4298 RVA: 0x00032B1F File Offset: 0x00030D1F
		[SRCategory("CatKey")]
		[SRDescription("PreviewKeyDownDescr")]
		public event PreviewKeyDownEventHandler PreviewKeyDown
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			add
			{
				base.Events.AddHandler(Control.EventPreviewKeyDown, value);
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			remove
			{
				base.Events.RemoveHandler(Control.EventPreviewKeyDown, value);
			}
		}

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x060010CB RID: 4299 RVA: 0x00032B32 File Offset: 0x00030D32
		// (remove) Token: 0x060010CC RID: 4300 RVA: 0x00032B45 File Offset: 0x00030D45
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnResizeDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler Resize
		{
			add
			{
				base.Events.AddHandler(Control.EventResize, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventResize, value);
			}
		}

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x060010CD RID: 4301 RVA: 0x00032B58 File Offset: 0x00030D58
		// (remove) Token: 0x060010CE RID: 4302 RVA: 0x00032B6B File Offset: 0x00030D6B
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnChangeUICuesDescr")]
		public event UICuesEventHandler ChangeUICues
		{
			add
			{
				base.Events.AddHandler(Control.EventChangeUICues, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventChangeUICues, value);
			}
		}

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x060010CF RID: 4303 RVA: 0x00032B7E File Offset: 0x00030D7E
		// (remove) Token: 0x060010D0 RID: 4304 RVA: 0x00032B91 File Offset: 0x00030D91
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnStyleChangedDescr")]
		public event EventHandler StyleChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventStyleChanged, value);
			}
		}

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x060010D1 RID: 4305 RVA: 0x00032BA4 File Offset: 0x00030DA4
		// (remove) Token: 0x060010D2 RID: 4306 RVA: 0x00032BB7 File Offset: 0x00030DB7
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnSystemColorsChangedDescr")]
		public event EventHandler SystemColorsChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventSystemColorsChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventSystemColorsChanged, value);
			}
		}

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x060010D3 RID: 4307 RVA: 0x00032BCA File Offset: 0x00030DCA
		// (remove) Token: 0x060010D4 RID: 4308 RVA: 0x00032BDD File Offset: 0x00030DDD
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatingDescr")]
		public event CancelEventHandler Validating
		{
			add
			{
				base.Events.AddHandler(Control.EventValidating, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventValidating, value);
			}
		}

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x060010D5 RID: 4309 RVA: 0x00032BF0 File Offset: 0x00030DF0
		// (remove) Token: 0x060010D6 RID: 4310 RVA: 0x00032C03 File Offset: 0x00030E03
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatedDescr")]
		public event EventHandler Validated
		{
			add
			{
				base.Events.AddHandler(Control.EventValidated, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventValidated, value);
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00032C16 File Offset: 0x00030E16
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal void AccessibilityNotifyClients(AccessibleEvents accEvent, int childID)
		{
			this.AccessibilityNotifyClients(accEvent, -4, childID);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00032C22 File Offset: 0x00030E22
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void AccessibilityNotifyClients(AccessibleEvents accEvent, int objectID, int childID)
		{
			if (this.IsHandleCreated && !LocalAppContextSwitches.NoClientNotifications)
			{
				UnsafeNativeMethods.NotifyWinEvent((int)accEvent, new HandleRef(this, this.Handle), objectID, childID + 1);
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00032C49 File Offset: 0x00030E49
		private IntPtr ActiveXMergeRegion(IntPtr region)
		{
			return this.ActiveXInstance.MergeRegion(region);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00032C57 File Offset: 0x00030E57
		private void ActiveXOnFocus(bool focus)
		{
			this.ActiveXInstance.OnFocus(focus);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00032C65 File Offset: 0x00030E65
		private void ActiveXViewChanged()
		{
			this.ActiveXInstance.ViewChangedInternal();
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00032C72 File Offset: 0x00030E72
		private void ActiveXUpdateBounds(ref int x, ref int y, ref int width, ref int height, int flags)
		{
			this.ActiveXInstance.UpdateBounds(ref x, ref y, ref width, ref height, flags);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00032C88 File Offset: 0x00030E88
		internal virtual void AssignParent(Control value)
		{
			if (value != null)
			{
				this.RequiredScalingEnabled = value.RequiredScalingEnabled;
			}
			if (this.CanAccessProperties)
			{
				Font font = this.Font;
				Color foreColor = this.ForeColor;
				Color backColor = this.BackColor;
				RightToLeft rightToLeft = this.RightToLeft;
				bool enabled = this.Enabled;
				bool visible = this.Visible;
				this.parent = value;
				this.OnParentChanged(EventArgs.Empty);
				if (this.GetAnyDisposingInHierarchy())
				{
					return;
				}
				if (enabled != this.Enabled)
				{
					this.OnEnabledChanged(EventArgs.Empty);
				}
				bool visible2 = this.Visible;
				if (visible != visible2 && (visible || !visible2 || this.parent != null || this.GetTopLevel()))
				{
					this.OnVisibleChanged(EventArgs.Empty);
				}
				if (!font.Equals(this.Font))
				{
					this.OnFontChanged(EventArgs.Empty);
				}
				if (!foreColor.Equals(this.ForeColor))
				{
					this.OnForeColorChanged(EventArgs.Empty);
				}
				if (!backColor.Equals(this.BackColor))
				{
					this.OnBackColorChanged(EventArgs.Empty);
				}
				if (rightToLeft != this.RightToLeft)
				{
					this.OnRightToLeftChanged(EventArgs.Empty);
				}
				if (this.Properties.GetObject(Control.PropBindingManager) == null && this.Created)
				{
					this.OnBindingContextChanged(EventArgs.Empty);
				}
			}
			else
			{
				this.parent = value;
				this.OnParentChanged(EventArgs.Empty);
			}
			this.SetState(16777216, false);
			if (this.ParentInternal != null)
			{
				this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.All);
			}
		}

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x060010DE RID: 4318 RVA: 0x00032E15 File Offset: 0x00031015
		// (remove) Token: 0x060010DF RID: 4319 RVA: 0x00032E28 File Offset: 0x00031028
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnParentChangedDescr")]
		public event EventHandler ParentChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventParent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventParent, value);
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00032E3B File Offset: 0x0003103B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginInvoke(Delegate method)
		{
			return this.BeginInvoke(method, null);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00032E48 File Offset: 0x00031048
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginInvoke(Delegate method, params object[] args)
		{
			IAsyncResult result;
			using (new Control.MultithreadSafeCallScope())
			{
				Control control = this.FindMarshalingControl();
				result = (IAsyncResult)control.MarshaledInvoke(this, method, args, false);
			}
			return result;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00032E90 File Offset: 0x00031090
		internal void BeginUpdateInternal()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			if (this.updateCount == 0)
			{
				this.SendMessage(11, 0, 0);
			}
			this.updateCount += 1;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00032EC0 File Offset: 0x000310C0
		public void BringToFront()
		{
			if (this.parent != null)
			{
				this.parent.Controls.SetChildIndex(this, 0);
				return;
			}
			if (this.IsHandleCreated && this.GetTopLevel() && SafeNativeMethods.IsWindowEnabled(new HandleRef(this.window, this.Handle)))
			{
				SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.HWND_TOP, 0, 0, 0, 0, 3);
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00032F31 File Offset: 0x00031131
		internal virtual bool CanProcessMnemonic()
		{
			return this.Enabled && this.Visible && (this.parent == null || this.parent.CanProcessMnemonic());
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00032F5C File Offset: 0x0003115C
		internal virtual bool CanSelectCore()
		{
			if ((this.controlStyle & ControlStyles.Selectable) != ControlStyles.Selectable)
			{
				return false;
			}
			for (Control control = this; control != null; control = control.parent)
			{
				if (!control.Enabled || !control.Visible)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00032FA0 File Offset: 0x000311A0
		internal static void CheckParentingCycle(Control bottom, Control toFind)
		{
			Form form = null;
			Control control = null;
			for (Control control2 = bottom; control2 != null; control2 = control2.ParentInternal)
			{
				control = control2;
				if (control2 == toFind)
				{
					throw new ArgumentException(SR.GetString("CircularOwner"));
				}
			}
			if (control != null && control is Form)
			{
				Form form2 = (Form)control;
				for (Form form3 = form2; form3 != null; form3 = form3.OwnerInternal)
				{
					form = form3;
					if (form3 == toFind)
					{
						throw new ArgumentException(SR.GetString("CircularOwner"));
					}
				}
			}
			if (form != null && form.ParentInternal != null)
			{
				Control.CheckParentingCycle(form.ParentInternal, toFind);
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00033028 File Offset: 0x00031228
		private void ChildGotFocus(Control child)
		{
			if (this.IsActiveX)
			{
				this.ActiveXOnFocus(true);
			}
			if (this.parent != null)
			{
				this.parent.ChildGotFocus(child);
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003304D File Offset: 0x0003124D
		public bool Contains(Control ctl)
		{
			while (ctl != null)
			{
				ctl = ctl.ParentInternal;
				if (ctl == null)
				{
					return false;
				}
				if (ctl == this)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00033068 File Offset: 0x00031268
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new Control.ControlAccessibleObject(this);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00033070 File Offset: 0x00031270
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual Control.ControlCollection CreateControlsInstance()
		{
			return new Control.ControlCollection(this);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00033078 File Offset: 0x00031278
		public Graphics CreateGraphics()
		{
			Graphics result;
			using (new Control.MultithreadSafeCallScope())
			{
				IntSecurity.CreateGraphicsForControl.Demand();
				result = this.CreateGraphicsInternal();
			}
			return result;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000330BC File Offset: 0x000312BC
		internal Graphics CreateGraphicsInternal()
		{
			return Graphics.FromHwndInternal(this.Handle);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000330CC File Offset: 0x000312CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual void CreateHandle()
		{
			IntPtr userCookie = IntPtr.Zero;
			if (this.GetState(2048))
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.GetState(262144))
			{
				return;
			}
			Rectangle bounds;
			try
			{
				this.SetState(262144, true);
				bounds = this.Bounds;
				if (Application.UseVisualStyles)
				{
					userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				}
				CreateParams createParams = this.CreateParams;
				this.SetState(1073741824, (createParams.ExStyle & 4194304) != 0);
				if (this.parent != null)
				{
					Rectangle clientRectangle = this.parent.ClientRectangle;
					if (!clientRectangle.IsEmpty)
					{
						if (createParams.X != -2147483648)
						{
							createParams.X -= clientRectangle.X;
						}
						if (createParams.Y != -2147483648)
						{
							createParams.Y -= clientRectangle.Y;
						}
					}
				}
				if (createParams.Parent == IntPtr.Zero && (createParams.Style & 1073741824) != 0)
				{
					Application.ParkHandle(createParams, this.DpiAwarenessContext);
				}
				this.window.CreateHandle(createParams);
				this.UpdateReflectParent(true);
			}
			finally
			{
				this.SetState(262144, false);
				UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
			}
			if (this.Bounds != bounds)
			{
				LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00033230 File Offset: 0x00031430
		public void CreateControl()
		{
			bool created = this.Created;
			this.CreateControl(false);
			if (this.Properties.GetObject(Control.PropBindingManager) == null && this.ParentInternal != null && !created)
			{
				this.OnBindingContextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00033274 File Offset: 0x00031474
		internal void CreateControl(bool fIgnoreVisible)
		{
			bool flag = (this.state & 1) == 0;
			flag = (flag && this.Visible);
			if (flag || fIgnoreVisible)
			{
				this.state |= 1;
				bool flag2 = false;
				try
				{
					if (!this.IsHandleCreated)
					{
						this.CreateHandle();
					}
					Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection != null)
					{
						Control[] array = new Control[controlCollection.Count];
						controlCollection.CopyTo(array, 0);
						foreach (Control control in array)
						{
							if (control.IsHandleCreated)
							{
								control.SetParentHandle(this.Handle);
							}
							control.CreateControl(fIgnoreVisible);
						}
					}
					flag2 = true;
				}
				finally
				{
					if (!flag2)
					{
						this.state &= -2;
					}
				}
				this.OnCreateControl();
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00033358 File Offset: 0x00031558
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual void DefWndProc(ref Message m)
		{
			this.window.DefWndProc(ref m);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00033368 File Offset: 0x00031568
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual void DestroyHandle()
		{
			if (this.RecreatingHandle && this.threadCallbackList != null)
			{
				Queue obj = this.threadCallbackList;
				lock (obj)
				{
					if (Control.threadCallbackMessage != 0)
					{
						NativeMethods.MSG msg = default(NativeMethods.MSG);
						if (UnsafeNativeMethods.PeekMessage(ref msg, new HandleRef(this, this.Handle), Control.threadCallbackMessage, Control.threadCallbackMessage, 0))
						{
							this.SetState(32768, true);
						}
					}
				}
			}
			if (!this.RecreatingHandle && this.threadCallbackList != null)
			{
				Queue obj2 = this.threadCallbackList;
				lock (obj2)
				{
					Exception exception = new ObjectDisposedException(base.GetType().Name);
					while (this.threadCallbackList.Count > 0)
					{
						Control.ThreadMethodEntry threadMethodEntry = (Control.ThreadMethodEntry)this.threadCallbackList.Dequeue();
						threadMethodEntry.exception = exception;
						threadMethodEntry.Complete();
					}
				}
			}
			if ((64 & (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this.window, this.InternalHandle), -20))) != 0)
			{
				UnsafeNativeMethods.DefMDIChildProc(this.InternalHandle, 16, IntPtr.Zero, IntPtr.Zero);
			}
			else
			{
				this.window.DestroyHandle();
			}
			this.trackMouseEvent = null;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000334BC File Offset: 0x000316BC
		protected override void Dispose(bool disposing)
		{
			if (this.GetState(2097152))
			{
				object @object = this.Properties.GetObject(Control.PropBackBrush);
				if (@object != null)
				{
					IntPtr intPtr = (IntPtr)@object;
					if (intPtr != IntPtr.Zero)
					{
						SafeNativeMethods.DeleteObject(new HandleRef(this, intPtr));
					}
					this.Properties.SetObject(Control.PropBackBrush, null);
				}
			}
			this.UpdateReflectParent(false);
			if (disposing)
			{
				if (this.GetState(4096))
				{
					return;
				}
				if (this.GetState(262144))
				{
					throw new InvalidOperationException(SR.GetString("ClosingWhileCreatingHandle", new object[]
					{
						"Dispose"
					}));
				}
				this.SetState(4096, true);
				this.SuspendLayout();
				try
				{
					this.DisposeAxControls();
					ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
					if (contextMenu != null)
					{
						contextMenu.Disposed -= this.DetachContextMenu;
					}
					this.ResetBindings();
					if (this.IsHandleCreated)
					{
						this.DestroyHandle();
					}
					if (this.parent != null)
					{
						this.parent.Controls.Remove(this);
					}
					Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection != null)
					{
						for (int i = 0; i < controlCollection.Count; i++)
						{
							Control control = controlCollection[i];
							control.parent = null;
							control.Dispose();
						}
						this.Properties.SetObject(Control.PropControlsCollection, null);
					}
					base.Dispose(disposing);
					return;
				}
				finally
				{
					this.ResumeLayout(false);
					this.SetState(4096, false);
					this.SetState(2048, true);
				}
			}
			if (this.window != null)
			{
				this.window.ForceExitMessageLoop();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00033680 File Offset: 0x00031880
		internal virtual void DisposeAxControls()
		{
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].DisposeAxControls();
				}
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000336C4 File Offset: 0x000318C4
		[UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
		public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
		{
			int[] array = new int[1];
			UnsafeNativeMethods.IOleDropSource dropSource = new DropSource(this);
			IDataObject dataObject;
			if (data is IDataObject)
			{
				dataObject = (IDataObject)data;
			}
			else
			{
				DataObject dataObject2;
				if (data is IDataObject)
				{
					dataObject2 = new DataObject((IDataObject)data);
				}
				else
				{
					dataObject2 = new DataObject();
					dataObject2.SetData(data);
				}
				dataObject = dataObject2;
			}
			try
			{
				SafeNativeMethods.DoDragDrop(dataObject, dropSource, (int)allowedEffects, array);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
			return (DragDropEffects)array[0];
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00033748 File Offset: 0x00031948
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		public void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			if (bitmap == null)
			{
				throw new ArgumentNullException("bitmap");
			}
			if (targetBounds.Width <= 0 || targetBounds.Height <= 0 || targetBounds.X < 0 || targetBounds.Y < 0)
			{
				throw new ArgumentException("targetBounds");
			}
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			int nWidth = Math.Min(this.Width, targetBounds.Width);
			int nHeight = Math.Min(this.Height, targetBounds.Height);
			using (Bitmap bitmap2 = new Bitmap(nWidth, nHeight, bitmap.PixelFormat))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap2))
				{
					IntPtr hdc = graphics.GetHdc();
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 791, hdc, (IntPtr)30);
					using (Graphics graphics2 = Graphics.FromImage(bitmap))
					{
						IntPtr hdc2 = graphics2.GetHdc();
						SafeNativeMethods.BitBlt(new HandleRef(graphics2, hdc2), targetBounds.X, targetBounds.Y, nWidth, nHeight, new HandleRef(graphics, hdc), 0, 0, 13369376);
						graphics2.ReleaseHdcInternal(hdc2);
					}
					graphics.ReleaseHdcInternal(hdc);
				}
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000338A4 File Offset: 0x00031AA4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object EndInvoke(IAsyncResult asyncResult)
		{
			object retVal;
			using (new Control.MultithreadSafeCallScope())
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Control.ThreadMethodEntry threadMethodEntry = asyncResult as Control.ThreadMethodEntry;
				if (threadMethodEntry == null)
				{
					throw new ArgumentException(SR.GetString("ControlBadAsyncResult"), "asyncResult");
				}
				if (!asyncResult.IsCompleted)
				{
					Control control = this.FindMarshalingControl();
					int num;
					if (SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(control, control.Handle), out num) == SafeNativeMethods.GetCurrentThreadId())
					{
						control.InvokeMarshaledCallbacks();
					}
					else
					{
						control = threadMethodEntry.marshaler;
						control.WaitForWaitHandle(asyncResult.AsyncWaitHandle);
					}
				}
				if (threadMethodEntry.exception != null)
				{
					throw threadMethodEntry.exception;
				}
				retVal = threadMethodEntry.retVal;
			}
			return retVal;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00033960 File Offset: 0x00031B60
		internal bool EndUpdateInternal()
		{
			return this.EndUpdateInternal(true);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00033969 File Offset: 0x00031B69
		internal bool EndUpdateInternal(bool invalidate)
		{
			if (this.updateCount > 0)
			{
				this.updateCount -= 1;
				if (this.updateCount == 0)
				{
					this.SendMessage(11, -1, 0);
					if (invalidate)
					{
						this.Invalidate();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000339A2 File Offset: 0x00031BA2
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		public Form FindForm()
		{
			return this.FindFormInternal();
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000339AC File Offset: 0x00031BAC
		internal Form FindFormInternal()
		{
			Control control = this;
			while (control != null && !(control is Form))
			{
				control = control.ParentInternal;
			}
			return (Form)control;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000339D8 File Offset: 0x00031BD8
		private Control FindMarshalingControl()
		{
			Control result;
			lock (this)
			{
				Control control = this;
				while (control != null && !control.IsHandleCreated)
				{
					Control parentInternal = control.ParentInternal;
					control = parentInternal;
				}
				if (control == null)
				{
					control = this;
				}
				result = control;
			}
			return result;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00033A30 File Offset: 0x00031C30
		protected bool GetTopLevel()
		{
			return (this.state & 524288) != 0;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00033A44 File Offset: 0x00031C44
		internal void RaiseCreateHandleEvent(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventHandleCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00033A74 File Offset: 0x00031C74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseKeyEvent(object key, KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[key];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00033AA0 File Offset: 0x00031CA0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseMouseEvent(object key, MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[key];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00033ACA File Offset: 0x00031CCA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool Focus()
		{
			IntSecurity.ModifyFocus.Demand();
			return this.FocusInternal();
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00033ADC File Offset: 0x00031CDC
		internal virtual bool FocusInternal()
		{
			if (this.CanFocus)
			{
				UnsafeNativeMethods.SetFocus(new HandleRef(this, this.Handle));
			}
			if (this.Focused && this.ParentInternal != null)
			{
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					if (containerControlInternal is ContainerControl)
					{
						((ContainerControl)containerControlInternal).SetActiveControlInternal(this);
					}
					else
					{
						containerControlInternal.ActiveControl = this;
					}
				}
			}
			return this.Focused;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00033B45 File Offset: 0x00031D45
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Control FromChildHandle(IntPtr handle)
		{
			IntSecurity.ControlFromHandleOrLocation.Demand();
			return Control.FromChildHandleInternal(handle);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00033B58 File Offset: 0x00031D58
		internal static Control FromChildHandleInternal(IntPtr handle)
		{
			while (handle != IntPtr.Zero)
			{
				Control control = Control.FromHandleInternal(handle);
				if (control != null)
				{
					return control;
				}
				handle = UnsafeNativeMethods.GetAncestor(new HandleRef(null, handle), 1);
			}
			return null;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00033B90 File Offset: 0x00031D90
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Control FromHandle(IntPtr handle)
		{
			IntSecurity.ControlFromHandleOrLocation.Demand();
			return Control.FromHandleInternal(handle);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00033BA4 File Offset: 0x00031DA4
		internal static Control FromHandleInternal(IntPtr handle)
		{
			NativeWindow nativeWindow = NativeWindow.FromHandle(handle);
			while (nativeWindow != null && !(nativeWindow is Control.ControlNativeWindow))
			{
				nativeWindow = nativeWindow.PreviousWindow;
			}
			if (nativeWindow is Control.ControlNativeWindow)
			{
				return ((Control.ControlNativeWindow)nativeWindow).GetControl();
			}
			return null;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00033BE4 File Offset: 0x00031DE4
		internal Size ApplySizeConstraints(int width, int height)
		{
			return this.ApplyBoundsConstraints(0, 0, width, height).Size;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00033C04 File Offset: 0x00031E04
		internal Size ApplySizeConstraints(Size proposedSize)
		{
			return this.ApplyBoundsConstraints(0, 0, proposedSize.Width, proposedSize.Height).Size;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00033C30 File Offset: 0x00031E30
		internal virtual Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			if (this.MaximumSize != Size.Empty || this.MinimumSize != Size.Empty)
			{
				Size b = LayoutUtils.ConvertZeroToUnbounded(this.MaximumSize);
				Rectangle result = new Rectangle(suggestedX, suggestedY, 0, 0);
				result.Size = LayoutUtils.IntersectSizes(new Size(proposedWidth, proposedHeight), b);
				result.Size = LayoutUtils.UnionSizes(result.Size, this.MinimumSize);
				return result;
			}
			return new Rectangle(suggestedX, suggestedY, proposedWidth, proposedHeight);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00033CB4 File Offset: 0x00031EB4
		public Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
		{
			if (skipValue < GetChildAtPointSkip.None || skipValue > (GetChildAtPointSkip.Invisible | GetChildAtPointSkip.Disabled | GetChildAtPointSkip.Transparent))
			{
				throw new InvalidEnumArgumentException("skipValue", (int)skipValue, typeof(GetChildAtPointSkip));
			}
			IntPtr handle = UnsafeNativeMethods.ChildWindowFromPointEx(new HandleRef(null, this.Handle), pt.X, pt.Y, (int)skipValue);
			Control control = Control.FromChildHandleInternal(handle);
			if (control != null && !this.IsDescendant(control))
			{
				IntSecurity.ControlFromHandleOrLocation.Demand();
			}
			if (control != this)
			{
				return control;
			}
			return null;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00033D26 File Offset: 0x00031F26
		public Control GetChildAtPoint(Point pt)
		{
			return this.GetChildAtPoint(pt, GetChildAtPointSkip.None);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00033D30 File Offset: 0x00031F30
		public IContainerControl GetContainerControl()
		{
			IntSecurity.GetParent.Demand();
			return this.GetContainerControlInternal();
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00033D42 File Offset: 0x00031F42
		private static bool IsFocusManagingContainerControl(Control ctl)
		{
			return (ctl.controlStyle & ControlStyles.ContainerControl) == ControlStyles.ContainerControl && ctl is IContainerControl;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00033D5A File Offset: 0x00031F5A
		internal bool IsUpdating()
		{
			return this.updateCount > 0;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00033D68 File Offset: 0x00031F68
		internal IContainerControl GetContainerControlInternal()
		{
			Control control = this;
			if (control != null && this.IsContainerControl)
			{
				control = control.ParentInternal;
			}
			while (control != null && !Control.IsFocusManagingContainerControl(control))
			{
				control = control.ParentInternal;
			}
			return (IContainerControl)control;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00033DA3 File Offset: 0x00031FA3
		private static Control.FontHandleWrapper GetDefaultFontHandleWrapper()
		{
			if (Control.defaultFontHandleWrapper == null)
			{
				Control.defaultFontHandleWrapper = new Control.FontHandleWrapper(Control.DefaultFont);
			}
			return Control.defaultFontHandleWrapper;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00033DC0 File Offset: 0x00031FC0
		internal IntPtr GetHRgn(Region region)
		{
			Graphics graphics = this.CreateGraphicsInternal();
			IntPtr hrgn = region.GetHrgn(graphics);
			System.Internal.HandleCollector.Add(hrgn, NativeMethods.CommonHandles.GDI);
			graphics.Dispose();
			return hrgn;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00033DF0 File Offset: 0x00031FF0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, 0, 0);
			CreateParams createParams = this.CreateParams;
			this.AdjustWindowRectEx(ref rect, createParams.Style, this.HasMenu, createParams.ExStyle);
			float num = factor.Width;
			float num2 = factor.Height;
			int num3 = bounds.X;
			int num4 = bounds.Y;
			bool flag = !this.GetState(524288);
			if (flag)
			{
				ISite site = this.Site;
				if (site != null && site.DesignMode)
				{
					IDesignerHost designerHost = site.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (designerHost != null && designerHost.RootComponent == this)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				if ((specified & BoundsSpecified.X) != BoundsSpecified.None)
				{
					num3 = (int)Math.Round((double)((float)bounds.X * num));
				}
				if ((specified & BoundsSpecified.Y) != BoundsSpecified.None)
				{
					num4 = (int)Math.Round((double)((float)bounds.Y * num2));
				}
			}
			int num5 = bounds.Width;
			int num6 = bounds.Height;
			if ((this.controlStyle & ControlStyles.FixedWidth) != ControlStyles.FixedWidth && (specified & BoundsSpecified.Width) != BoundsSpecified.None)
			{
				int num7 = rect.right - rect.left;
				int num8 = bounds.Width - num7;
				num5 = (int)Math.Round((double)((float)num8 * num)) + num7;
			}
			if ((this.controlStyle & ControlStyles.FixedHeight) != ControlStyles.FixedHeight && (specified & BoundsSpecified.Height) != BoundsSpecified.None)
			{
				int num9 = rect.bottom - rect.top;
				int num10 = bounds.Height - num9;
				num6 = (int)Math.Round((double)((float)num10 * num2)) + num9;
			}
			return new Rectangle(num3, num4, num5, num6);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00033F6C File Offset: 0x0003216C
		private MouseButtons GetXButton(int wparam)
		{
			if (wparam == 1)
			{
				return MouseButtons.XButton1;
			}
			if (wparam != 2)
			{
				return MouseButtons.None;
			}
			return MouseButtons.XButton2;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00033F85 File Offset: 0x00032185
		internal virtual bool GetVisibleCore()
		{
			return this.GetState(2) && (this.ParentInternal == null || this.ParentInternal.GetVisibleCore());
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00033FA8 File Offset: 0x000321A8
		internal bool GetAnyDisposingInHierarchy()
		{
			Control control = this;
			bool result = false;
			while (control != null)
			{
				if (control.Disposing)
				{
					result = true;
					break;
				}
				control = control.parent;
			}
			return result;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00033FD4 File Offset: 0x000321D4
		private MenuItem GetMenuItemFromHandleId(IntPtr hmenu, int item)
		{
			MenuItem result = null;
			int menuItemID = UnsafeNativeMethods.GetMenuItemID(new HandleRef(null, hmenu), item);
			if (menuItemID == -1)
			{
				IntPtr intPtr = IntPtr.Zero;
				intPtr = UnsafeNativeMethods.GetSubMenu(new HandleRef(null, hmenu), item);
				int menuItemCount = UnsafeNativeMethods.GetMenuItemCount(new HandleRef(null, intPtr));
				MenuItem menuItem = null;
				for (int i = 0; i < menuItemCount; i++)
				{
					menuItem = this.GetMenuItemFromHandleId(intPtr, i);
					if (menuItem != null)
					{
						Menu menu = menuItem.Parent;
						if (menu != null && menu is MenuItem)
						{
							menuItem = (MenuItem)menu;
							break;
						}
						menuItem = null;
					}
				}
				result = menuItem;
			}
			else
			{
				Command commandFromID = Command.GetCommandFromID(menuItemID);
				if (commandFromID != null)
				{
					object target = commandFromID.Target;
					if (target != null && target is MenuItem.MenuItemData)
					{
						result = ((MenuItem.MenuItemData)target).baseItem;
					}
				}
			}
			return result;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00034094 File Offset: 0x00032294
		private ArrayList GetChildControlsTabOrderList(bool handleCreatedOnly)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (!handleCreatedOnly || control.IsHandleCreated)
				{
					arrayList.Add(new Control.ControlTabOrderHolder(arrayList.Count, control.TabIndex, control));
				}
			}
			arrayList.Sort(new Control.ControlTabOrderComparer());
			return arrayList;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0003411C File Offset: 0x0003231C
		private int[] GetChildWindowsInTabOrder()
		{
			ArrayList childWindowsTabOrderList = this.GetChildWindowsTabOrderList();
			int[] array = new int[childWindowsTabOrderList.Count];
			for (int i = 0; i < childWindowsTabOrderList.Count; i++)
			{
				array[i] = ((Control.ControlTabOrderHolder)childWindowsTabOrderList[i]).oldOrder;
			}
			return array;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00034164 File Offset: 0x00032364
		internal Control[] GetChildControlsInTabOrder(bool handleCreatedOnly)
		{
			ArrayList childControlsTabOrderList = this.GetChildControlsTabOrderList(handleCreatedOnly);
			Control[] array = new Control[childControlsTabOrderList.Count];
			for (int i = 0; i < childControlsTabOrderList.Count; i++)
			{
				array[i] = ((Control.ControlTabOrderHolder)childControlsTabOrderList[i]).control;
			}
			return array;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000341AC File Offset: 0x000323AC
		private static ArrayList GetChildWindows(IntPtr hWndParent)
		{
			ArrayList arrayList = new ArrayList();
			IntPtr intPtr = UnsafeNativeMethods.GetWindow(new HandleRef(null, hWndParent), 5);
			while (intPtr != IntPtr.Zero)
			{
				arrayList.Add(intPtr);
				intPtr = UnsafeNativeMethods.GetWindow(new HandleRef(null, intPtr), 2);
			}
			return arrayList;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000341F8 File Offset: 0x000323F8
		private ArrayList GetChildWindowsTabOrderList()
		{
			ArrayList arrayList = new ArrayList();
			ArrayList childWindows = Control.GetChildWindows(this.Handle);
			foreach (object obj in childWindows)
			{
				IntPtr handle = (IntPtr)obj;
				Control control = Control.FromHandleInternal(handle);
				int newOrder = (control == null) ? -1 : control.TabIndex;
				arrayList.Add(new Control.ControlTabOrderHolder(arrayList.Count, newOrder, control));
			}
			arrayList.Sort(new Control.ControlTabOrderComparer());
			return arrayList;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00034294 File Offset: 0x00032494
		internal virtual Control GetFirstChildControlInTabOrder(bool forward)
		{
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			Control control = null;
			if (controlCollection != null)
			{
				if (forward)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						if (control == null || control.tabIndex > controlCollection[i].tabIndex)
						{
							control = controlCollection[i];
						}
					}
				}
				else
				{
					for (int j = controlCollection.Count - 1; j >= 0; j--)
					{
						if (control == null || control.tabIndex < controlCollection[j].tabIndex)
						{
							control = controlCollection[j];
						}
					}
				}
			}
			return control;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00034324 File Offset: 0x00032524
		public Control GetNextControl(Control ctl, bool forward)
		{
			if (!this.Contains(ctl))
			{
				ctl = this;
			}
			if (forward)
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)ctl.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null && controlCollection.Count > 0 && (ctl == this || !Control.IsFocusManagingContainerControl(ctl)))
				{
					Control firstChildControlInTabOrder = ctl.GetFirstChildControlInTabOrder(true);
					if (firstChildControlInTabOrder != null)
					{
						return firstChildControlInTabOrder;
					}
				}
				while (ctl != this)
				{
					int num = ctl.tabIndex;
					bool flag = false;
					Control control = null;
					Control control2 = ctl.parent;
					int num2 = 0;
					Control.ControlCollection controlCollection2 = (Control.ControlCollection)control2.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection2 != null)
					{
						num2 = controlCollection2.Count;
					}
					for (int i = 0; i < num2; i++)
					{
						if (controlCollection2[i] != ctl)
						{
							if (controlCollection2[i].tabIndex >= num && (control == null || control.tabIndex > controlCollection2[i].tabIndex) && (controlCollection2[i].tabIndex != num || flag))
							{
								control = controlCollection2[i];
							}
						}
						else
						{
							flag = true;
						}
					}
					if (control != null)
					{
						return control;
					}
					ctl = ctl.parent;
				}
			}
			else
			{
				if (ctl != this)
				{
					int num3 = ctl.tabIndex;
					bool flag2 = false;
					Control control3 = null;
					Control control4 = ctl.parent;
					int num4 = 0;
					Control.ControlCollection controlCollection3 = (Control.ControlCollection)control4.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection3 != null)
					{
						num4 = controlCollection3.Count;
					}
					for (int j = num4 - 1; j >= 0; j--)
					{
						if (controlCollection3[j] != ctl)
						{
							if (controlCollection3[j].tabIndex <= num3 && (control3 == null || control3.tabIndex < controlCollection3[j].tabIndex) && (controlCollection3[j].tabIndex != num3 || flag2))
							{
								control3 = controlCollection3[j];
							}
						}
						else
						{
							flag2 = true;
						}
					}
					if (control3 != null)
					{
						ctl = control3;
					}
					else
					{
						if (control4 == this)
						{
							return null;
						}
						return control4;
					}
				}
				Control.ControlCollection controlCollection4 = (Control.ControlCollection)ctl.Properties.GetObject(Control.PropControlsCollection);
				while (controlCollection4 != null && controlCollection4.Count > 0 && (ctl == this || !Control.IsFocusManagingContainerControl(ctl)))
				{
					Control firstChildControlInTabOrder2 = ctl.GetFirstChildControlInTabOrder(false);
					if (firstChildControlInTabOrder2 == null)
					{
						break;
					}
					ctl = firstChildControlInTabOrder2;
					controlCollection4 = (Control.ControlCollection)ctl.Properties.GetObject(Control.PropControlsCollection);
				}
			}
			if (ctl != this)
			{
				return ctl;
			}
			return null;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00034584 File Offset: 0x00032784
		internal static IntPtr GetSafeHandle(IWin32Window window)
		{
			IntPtr intPtr = IntPtr.Zero;
			Control control = window as Control;
			if (control != null)
			{
				return control.Handle;
			}
			IntSecurity.AllWindows.Demand();
			intPtr = window.Handle;
			if (intPtr == IntPtr.Zero || UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr)))
			{
				return intPtr;
			}
			throw new Win32Exception(6);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000345DE File Offset: 0x000327DE
		internal bool GetState(int flag)
		{
			return (this.state & flag) != 0;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000345EB File Offset: 0x000327EB
		private bool GetState2(int flag)
		{
			return (this.state2 & flag) != 0;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000345F8 File Offset: 0x000327F8
		protected bool GetStyle(ControlStyles flag)
		{
			return (this.controlStyle & flag) == flag;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00034605 File Offset: 0x00032805
		public void Hide()
		{
			this.Visible = false;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00034610 File Offset: 0x00032810
		private void HookMouseEvent()
		{
			if (!this.GetState(16384))
			{
				this.SetState(16384, true);
				if (this.trackMouseEvent == null)
				{
					this.trackMouseEvent = new NativeMethods.TRACKMOUSEEVENT();
					this.trackMouseEvent.dwFlags = 3;
					this.trackMouseEvent.hwndTrack = this.Handle;
				}
				SafeNativeMethods.TrackMouseEvent(this.trackMouseEvent);
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00034672 File Offset: 0x00032872
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void InitLayout()
		{
			this.LayoutEngine.InitLayout(this, BoundsSpecified.All);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00034682 File Offset: 0x00032882
		private void InitScaling(BoundsSpecified specified)
		{
			this.requiredScaling |= (byte)(specified & BoundsSpecified.All);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00034698 File Offset: 0x00032898
		internal virtual IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			if (!this.GetStyle(ControlStyles.UserPaint))
			{
				SafeNativeMethods.SetTextColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.ForeColor));
				SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.BackColor));
				return this.BackColorBrush;
			}
			return UnsafeNativeMethods.GetStockObject(5);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000346EC File Offset: 0x000328EC
		private void InitMouseWheelSupport()
		{
			if (!Control.mouseWheelInit)
			{
				Control.mouseWheelRoutingNeeded = !SystemInformation.NativeMouseWheelSupport;
				if (Control.mouseWheelRoutingNeeded)
				{
					IntPtr value = IntPtr.Zero;
					value = UnsafeNativeMethods.FindWindow("MouseZ", "Magellan MSWHEEL");
					if (value != IntPtr.Zero)
					{
						int num = SafeNativeMethods.RegisterWindowMessage("MSWHEEL_ROLLMSG");
						if (num != 0)
						{
							Control.mouseWheelMessage = num;
						}
					}
				}
				Control.mouseWheelInit = true;
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00034751 File Offset: 0x00032951
		public void Invalidate(Region region)
		{
			this.Invalidate(region, false);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0003475C File Offset: 0x0003295C
		public void Invalidate(Region region, bool invalidateChildren)
		{
			if (region == null)
			{
				this.Invalidate(invalidateChildren);
				return;
			}
			if (this.IsHandleCreated)
			{
				IntPtr hrgn = this.GetHRgn(region);
				try
				{
					if (invalidateChildren)
					{
						SafeNativeMethods.RedrawWindow(new HandleRef(this, this.Handle), null, new HandleRef(region, hrgn), 133);
					}
					else
					{
						using (new Control.MultithreadSafeCallScope())
						{
							SafeNativeMethods.InvalidateRgn(new HandleRef(this, this.Handle), new HandleRef(region, hrgn), !this.GetStyle(ControlStyles.Opaque));
						}
					}
				}
				finally
				{
					SafeNativeMethods.DeleteObject(new HandleRef(region, hrgn));
				}
				Rectangle invalidRect = Rectangle.Empty;
				using (Graphics graphics = this.CreateGraphicsInternal())
				{
					invalidRect = Rectangle.Ceiling(region.GetBounds(graphics));
				}
				this.OnInvalidated(new InvalidateEventArgs(invalidRect));
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00034848 File Offset: 0x00032A48
		public void Invalidate()
		{
			this.Invalidate(false);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00034854 File Offset: 0x00032A54
		public void Invalidate(bool invalidateChildren)
		{
			if (this.IsHandleCreated)
			{
				if (invalidateChildren)
				{
					SafeNativeMethods.RedrawWindow(new HandleRef(this.window, this.Handle), null, NativeMethods.NullHandleRef, 133);
				}
				else
				{
					using (new Control.MultithreadSafeCallScope())
					{
						SafeNativeMethods.InvalidateRect(new HandleRef(this.window, this.Handle), null, (this.controlStyle & ControlStyles.Opaque) != ControlStyles.Opaque);
					}
				}
				this.NotifyInvalidate(this.ClientRectangle);
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000348E4 File Offset: 0x00032AE4
		public void Invalidate(Rectangle rc)
		{
			this.Invalidate(rc, false);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000348F0 File Offset: 0x00032AF0
		public void Invalidate(Rectangle rc, bool invalidateChildren)
		{
			if (rc.IsEmpty)
			{
				this.Invalidate(invalidateChildren);
				return;
			}
			if (this.IsHandleCreated)
			{
				if (invalidateChildren)
				{
					NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(rc.X, rc.Y, rc.Width, rc.Height);
					SafeNativeMethods.RedrawWindow(new HandleRef(this.window, this.Handle), ref rect, NativeMethods.NullHandleRef, 133);
				}
				else
				{
					NativeMethods.RECT rect2 = NativeMethods.RECT.FromXYWH(rc.X, rc.Y, rc.Width, rc.Height);
					using (new Control.MultithreadSafeCallScope())
					{
						SafeNativeMethods.InvalidateRect(new HandleRef(this.window, this.Handle), ref rect2, (this.controlStyle & ControlStyles.Opaque) != ControlStyles.Opaque);
					}
				}
				this.NotifyInvalidate(rc);
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000349D8 File Offset: 0x00032BD8
		public object Invoke(Delegate method)
		{
			return this.Invoke(method, null);
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000349E4 File Offset: 0x00032BE4
		public object Invoke(Delegate method, params object[] args)
		{
			object result;
			using (new Control.MultithreadSafeCallScope())
			{
				Control control = this.FindMarshalingControl();
				result = control.MarshaledInvoke(this, method, args, true);
			}
			return result;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00034A28 File Offset: 0x00032C28
		private void InvokeMarshaledCallback(Control.ThreadMethodEntry tme)
		{
			if (tme.executionContext != null)
			{
				if (Control.invokeMarshaledCallbackHelperDelegate == null)
				{
					Control.invokeMarshaledCallbackHelperDelegate = new ContextCallback(Control.InvokeMarshaledCallbackHelper);
				}
				if (SynchronizationContext.Current == null)
				{
					WindowsFormsSynchronizationContext.InstallIfNeeded();
				}
				tme.syncContext = SynchronizationContext.Current;
				ExecutionContext.Run(tme.executionContext, Control.invokeMarshaledCallbackHelperDelegate, tme);
				return;
			}
			Control.InvokeMarshaledCallbackHelper(tme);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00034A84 File Offset: 0x00032C84
		private static void InvokeMarshaledCallbackHelper(object obj)
		{
			Control.ThreadMethodEntry threadMethodEntry = (Control.ThreadMethodEntry)obj;
			if (threadMethodEntry.syncContext != null)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				try
				{
					SynchronizationContext.SetSynchronizationContext(threadMethodEntry.syncContext);
					Control.InvokeMarshaledCallbackDo(threadMethodEntry);
					return;
				}
				finally
				{
					SynchronizationContext.SetSynchronizationContext(synchronizationContext);
				}
			}
			Control.InvokeMarshaledCallbackDo(threadMethodEntry);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00034AD8 File Offset: 0x00032CD8
		private static void InvokeMarshaledCallbackDo(Control.ThreadMethodEntry tme)
		{
			if (tme.method is EventHandler)
			{
				if (tme.args == null || tme.args.Length < 1)
				{
					((EventHandler)tme.method)(tme.caller, EventArgs.Empty);
					return;
				}
				if (tme.args.Length < 2)
				{
					((EventHandler)tme.method)(tme.args[0], EventArgs.Empty);
					return;
				}
				((EventHandler)tme.method)(tme.args[0], (EventArgs)tme.args[1]);
				return;
			}
			else
			{
				if (tme.method is MethodInvoker)
				{
					((MethodInvoker)tme.method)();
					return;
				}
				if (tme.method is WaitCallback)
				{
					((WaitCallback)tme.method)(tme.args[0]);
					return;
				}
				tme.retVal = tme.method.DynamicInvoke(tme.args);
				return;
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00034BCC File Offset: 0x00032DCC
		private void InvokeMarshaledCallbacks()
		{
			Control.ThreadMethodEntry threadMethodEntry = null;
			Queue obj = this.threadCallbackList;
			lock (obj)
			{
				if (this.threadCallbackList.Count > 0)
				{
					threadMethodEntry = (Control.ThreadMethodEntry)this.threadCallbackList.Dequeue();
				}
				goto IL_E8;
			}
			IL_41:
			if (threadMethodEntry.method != null)
			{
				try
				{
					if (NativeWindow.WndProcShouldBeDebuggable && !threadMethodEntry.synchronous)
					{
						this.InvokeMarshaledCallback(threadMethodEntry);
					}
					else
					{
						try
						{
							this.InvokeMarshaledCallback(threadMethodEntry);
						}
						catch (Exception ex)
						{
							threadMethodEntry.exception = ex.GetBaseException();
						}
					}
				}
				finally
				{
					threadMethodEntry.Complete();
					if (!NativeWindow.WndProcShouldBeDebuggable && threadMethodEntry.exception != null && !threadMethodEntry.synchronous)
					{
						Application.OnThreadException(threadMethodEntry.exception);
					}
				}
			}
			Queue obj2 = this.threadCallbackList;
			lock (obj2)
			{
				if (this.threadCallbackList.Count > 0)
				{
					threadMethodEntry = (Control.ThreadMethodEntry)this.threadCallbackList.Dequeue();
				}
				else
				{
					threadMethodEntry = null;
				}
			}
			IL_E8:
			if (threadMethodEntry == null)
			{
				return;
			}
			goto IL_41;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00034CFC File Offset: 0x00032EFC
		protected void InvokePaint(Control c, PaintEventArgs e)
		{
			c.OnPaint(e);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00034D05 File Offset: 0x00032F05
		protected void InvokePaintBackground(Control c, PaintEventArgs e)
		{
			c.OnPaintBackground(e);
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00034D10 File Offset: 0x00032F10
		internal bool IsFontSet()
		{
			return (Font)this.Properties.GetObject(Control.PropFont) != null;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00034D3C File Offset: 0x00032F3C
		internal bool IsDescendant(Control descendant)
		{
			for (Control control = descendant; control != null; control = control.ParentInternal)
			{
				if (control == this)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00034D60 File Offset: 0x00032F60
		public static bool IsKeyLocked(Keys keyVal)
		{
			if (keyVal != Keys.Insert && keyVal != Keys.NumLock && keyVal != Keys.Capital && keyVal != Keys.Scroll)
			{
				throw new NotSupportedException(SR.GetString("ControlIsKeyLockedNumCapsScrollLockKeysSupportedOnly"));
			}
			int keyState = (int)UnsafeNativeMethods.GetKeyState((int)keyVal);
			if (keyVal == Keys.Insert || keyVal == Keys.Capital)
			{
				return (keyState & 1) != 0;
			}
			return (keyState & 32769) != 0;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00034DBC File Offset: 0x00032FBC
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool IsInputChar(char charCode)
		{
			int num;
			if (charCode == '\t')
			{
				num = 134;
			}
			else
			{
				num = 132;
			}
			return ((int)((long)this.SendMessage(135, 0, 0)) & num) != 0;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00034DF8 File Offset: 0x00032FF8
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			int num = 4;
			Keys keys = keyData & Keys.KeyCode;
			if (keys != Keys.Tab)
			{
				if (keys - Keys.Left <= 3)
				{
					num = 5;
				}
			}
			else
			{
				num = 6;
			}
			return this.IsHandleCreated && ((int)((long)this.SendMessage(135, 0, 0)) & num) != 0;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00034E58 File Offset: 0x00033058
		public static bool IsMnemonic(char charCode, string text)
		{
			if (charCode == '&')
			{
				return false;
			}
			if (text != null)
			{
				int num = -1;
				char c = char.ToUpper(charCode, CultureInfo.CurrentCulture);
				while (num + 1 < text.Length)
				{
					num = text.IndexOf('&', num + 1) + 1;
					if (num <= 0 || num >= text.Length)
					{
						break;
					}
					char c2 = char.ToUpper(text[num], CultureInfo.CurrentCulture);
					if (c2 == c || char.ToLower(c2, CultureInfo.CurrentCulture) == char.ToLower(c, CultureInfo.CurrentCulture))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00034ED4 File Offset: 0x000330D4
		private void ListenToUserPreferenceChanged(bool listen)
		{
			if (this.GetState2(4))
			{
				if (!listen)
				{
					this.SetState2(4, false);
					SystemEvents.UserPreferenceChanged -= this.UserPreferenceChanged;
					return;
				}
			}
			else if (listen)
			{
				this.SetState2(4, true);
				SystemEvents.UserPreferenceChanged += this.UserPreferenceChanged;
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00034F23 File Offset: 0x00033123
		public int LogicalToDeviceUnits(int value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, this.DeviceDpi);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00034F31 File Offset: 0x00033131
		public Size LogicalToDeviceUnits(Size value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, this.DeviceDpi);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00034F3F File Offset: 0x0003313F
		public void ScaleBitmapLogicalToDevice(ref Bitmap logicalBitmap)
		{
			DpiHelper.ScaleBitmapLogicalToDevice(ref logicalBitmap, this.DeviceDpi);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00034F4D File Offset: 0x0003314D
		internal void AdjustWindowRectEx(ref NativeMethods.RECT rect, int style, bool bMenu, int exStyle)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				SafeNativeMethods.AdjustWindowRectExForDpi(ref rect, style, bMenu, exStyle, (uint)this.deviceDpi);
				return;
			}
			SafeNativeMethods.AdjustWindowRectEx(ref rect, style, bMenu, exStyle);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00034F74 File Offset: 0x00033174
		private object MarshaledInvoke(Control caller, Delegate method, object[] args, bool synchronous)
		{
			if (!this.IsHandleCreated)
			{
				throw new InvalidOperationException(SR.GetString("ErrorNoMarshalingThread"));
			}
			Control.ActiveXImpl activeXImpl = (Control.ActiveXImpl)this.Properties.GetObject(Control.PropActiveXImpl);
			if (activeXImpl != null)
			{
				IntSecurity.UnmanagedCode.Demand();
			}
			bool flag = false;
			int num;
			if (SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, this.Handle), out num) == SafeNativeMethods.GetCurrentThreadId() && synchronous)
			{
				flag = true;
			}
			ExecutionContext executionContext = null;
			if (!flag)
			{
				executionContext = ExecutionContext.Capture();
			}
			Control.ThreadMethodEntry threadMethodEntry = new Control.ThreadMethodEntry(caller, this, method, args, synchronous, executionContext);
			lock (this)
			{
				if (this.threadCallbackList == null)
				{
					this.threadCallbackList = new Queue();
				}
			}
			Queue obj = this.threadCallbackList;
			lock (obj)
			{
				if (Control.threadCallbackMessage == 0)
				{
					Control.threadCallbackMessage = SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_ThreadCallbackMessage");
				}
				this.threadCallbackList.Enqueue(threadMethodEntry);
			}
			if (flag)
			{
				this.InvokeMarshaledCallbacks();
			}
			else
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this, this.Handle), Control.threadCallbackMessage, IntPtr.Zero, IntPtr.Zero);
			}
			if (!synchronous)
			{
				return threadMethodEntry;
			}
			if (!threadMethodEntry.IsCompleted)
			{
				this.WaitForWaitHandle(threadMethodEntry.AsyncWaitHandle);
			}
			if (threadMethodEntry.exception != null)
			{
				throw threadMethodEntry.exception;
			}
			return threadMethodEntry.retVal;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000350F4 File Offset: 0x000332F4
		private void MarshalStringToMessage(string value, ref Message m)
		{
			if (m.LParam == IntPtr.Zero)
			{
				m.Result = (IntPtr)((value.Length + 1) * Marshal.SystemDefaultCharSize);
				return;
			}
			if ((int)((long)m.WParam) < value.Length + 1)
			{
				m.Result = (IntPtr)(-1);
				return;
			}
			char[] chars = new char[1];
			byte[] bytes;
			byte[] bytes2;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				bytes = Encoding.Default.GetBytes(value);
				bytes2 = Encoding.Default.GetBytes(chars);
			}
			else
			{
				bytes = Encoding.Unicode.GetBytes(value);
				bytes2 = Encoding.Unicode.GetBytes(chars);
			}
			Marshal.Copy(bytes, 0, m.LParam, bytes.Length);
			Marshal.Copy(bytes2, 0, (IntPtr)((long)m.LParam + (long)bytes.Length), bytes2.Length);
			m.Result = (IntPtr)((bytes.Length + bytes2.Length) / Marshal.SystemDefaultCharSize);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000351D8 File Offset: 0x000333D8
		internal void NotifyEnter()
		{
			this.OnEnter(EventArgs.Empty);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000351E5 File Offset: 0x000333E5
		internal void NotifyLeave()
		{
			this.OnLeave(EventArgs.Empty);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000351F2 File Offset: 0x000333F2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void NotifyInvalidate(Rectangle invalidatedArea)
		{
			this.OnInvalidated(new InvalidateEventArgs(invalidatedArea));
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00035200 File Offset: 0x00033400
		private bool NotifyValidating()
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnValidating(cancelEventArgs);
			return cancelEventArgs.Cancel;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00035220 File Offset: 0x00033420
		private void NotifyValidated()
		{
			this.OnValidated(EventArgs.Empty);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003522D File Offset: 0x0003342D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void InvokeOnClick(Control toInvoke, EventArgs e)
		{
			if (toInvoke != null)
			{
				toInvoke.OnClick(e);
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0003523C File Offset: 0x0003343C
		protected virtual void OnAutoSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventAutoSizeChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0003526C File Offset: 0x0003346C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackColorChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			object @object = this.Properties.GetObject(Control.PropBackBrush);
			if (@object != null)
			{
				if (this.GetState(2097152))
				{
					IntPtr intPtr = (IntPtr)@object;
					if (intPtr != IntPtr.Zero)
					{
						SafeNativeMethods.DeleteObject(new HandleRef(this, intPtr));
					}
				}
				this.Properties.SetObject(Control.PropBackBrush, null);
			}
			this.Invalidate();
			EventHandler eventHandler = base.Events[Control.EventBackColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentBackColorChanged(e);
				}
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00035338 File Offset: 0x00033538
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackgroundImageChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.Invalidate();
			EventHandler eventHandler = base.Events[Control.EventBackgroundImage] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentBackgroundImageChanged(e);
				}
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000353AC File Offset: 0x000335AC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.Invalidate();
			EventHandler eventHandler = base.Events[Control.EventBackgroundImageLayout] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000353EC File Offset: 0x000335EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBindingContextChanged(EventArgs e)
		{
			if (this.Properties.GetObject(Control.PropBindings) != null)
			{
				this.UpdateBindings();
			}
			EventHandler eventHandler = base.Events[Control.EventBindingContext] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentBindingContextChanged(e);
				}
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003546C File Offset: 0x0003366C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCausesValidationChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventCausesValidation] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0003549A File Offset: 0x0003369A
		internal virtual void OnChildLayoutResuming(Control child, bool performLayout)
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.OnChildLayoutResuming(child, performLayout);
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000354B4 File Offset: 0x000336B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnContextMenuChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventContextMenu] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000354E4 File Offset: 0x000336E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnContextMenuStripChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventContextMenuStrip] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00035514 File Offset: 0x00033714
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCursorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventCursor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentCursorChanged(e);
				}
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003557C File Offset: 0x0003377C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDockChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventDock] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000355AC File Offset: 0x000337AC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnEnabledChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.IsHandleCreated)
			{
				SafeNativeMethods.EnableWindow(new HandleRef(this, this.Handle), this.Enabled);
				if (this.GetStyle(ControlStyles.UserPaint))
				{
					this.Invalidate();
					this.Update();
				}
			}
			EventHandler eventHandler = base.Events[Control.EventEnabled] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentEnabledChanged(e);
				}
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnFrameWindowActivate(bool fActivate)
		{
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00035650 File Offset: 0x00033850
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFontChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.Invalidate();
			if (this.Properties.ContainsInteger(Control.PropFontHeight))
			{
				this.Properties.SetInteger(Control.PropFontHeight, -1);
			}
			this.DisposeFontHandle();
			if (this.IsHandleCreated && !this.GetStyle(ControlStyles.UserPaint))
			{
				this.SetWindowFont();
			}
			EventHandler eventHandler = base.Events[Control.EventFont] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			using (new LayoutTransaction(this, this, PropertyNames.Font, false))
			{
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						controlCollection[i].OnParentFontChanged(e);
					}
				}
			}
			LayoutTransaction.DoLayout(this, this, PropertyNames.Font);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0003573C File Offset: 0x0003393C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnForeColorChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.Invalidate();
			EventHandler eventHandler = base.Events[Control.EventForeColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentForeColorChanged(e);
				}
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000357B0 File Offset: 0x000339B0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.SetState2(2, true);
			this.RecreateHandle();
			EventHandler eventHandler = base.Events[Control.EventRightToLeft] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentRightToLeftChanged(e);
				}
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnNotifyMessage(Message m)
		{
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003582C File Offset: 0x00033A2C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBackColorChanged(EventArgs e)
		{
			if (this.Properties.GetColor(Control.PropBackColor).IsEmpty)
			{
				this.OnBackColorChanged(e);
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0003585A File Offset: 0x00033A5A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBackgroundImageChanged(EventArgs e)
		{
			this.OnBackgroundImageChanged(e);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00035863 File Offset: 0x00033A63
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBindingContextChanged(EventArgs e)
		{
			if (this.Properties.GetObject(Control.PropBindingManager) == null)
			{
				this.OnBindingContextChanged(e);
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003587E File Offset: 0x00033A7E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentCursorChanged(EventArgs e)
		{
			if (this.Properties.GetObject(Control.PropCursor) == null)
			{
				this.OnCursorChanged(e);
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00035899 File Offset: 0x00033A99
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentEnabledChanged(EventArgs e)
		{
			if (this.GetState(4))
			{
				this.OnEnabledChanged(e);
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000358AB File Offset: 0x00033AAB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentFontChanged(EventArgs e)
		{
			if (this.Properties.GetObject(Control.PropFont) == null)
			{
				this.OnFontChanged(e);
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000358C8 File Offset: 0x00033AC8
		internal virtual void OnParentHandleRecreated()
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null && this.IsHandleCreated)
			{
				UnsafeNativeMethods.SetParent(new HandleRef(this, this.Handle), new HandleRef(parentInternal, parentInternal.Handle));
				this.UpdateZOrder();
			}
			this.SetState(536870912, false);
			if (this.ReflectParent == this.ParentInternal)
			{
				this.RecreateHandle();
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0003592B File Offset: 0x00033B2B
		internal virtual void OnParentHandleRecreating()
		{
			this.SetState(536870912, true);
			if (this.IsHandleCreated)
			{
				Application.ParkHandle(new HandleRef(this, this.Handle), this.DpiAwarenessContext);
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00035958 File Offset: 0x00033B58
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentForeColorChanged(EventArgs e)
		{
			if (this.Properties.GetColor(Control.PropForeColor).IsEmpty)
			{
				this.OnForeColorChanged(e);
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00035986 File Offset: 0x00033B86
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentRightToLeftChanged(EventArgs e)
		{
			if (!this.Properties.ContainsInteger(Control.PropRightToLeft) || this.Properties.GetInteger(Control.PropRightToLeft) == 2)
			{
				this.OnRightToLeftChanged(e);
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000359B4 File Offset: 0x00033BB4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentVisibleChanged(EventArgs e)
		{
			if (this.GetState(2))
			{
				this.OnVisibleChanged(e);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000359C8 File Offset: 0x00033BC8
		internal virtual void OnParentBecameInvisible()
		{
			if (this.GetState(2))
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						Control control = controlCollection[i];
						control.OnParentBecameInvisible();
					}
				}
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00035A18 File Offset: 0x00033C18
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPrint(PaintEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (this.GetStyle(ControlStyles.UserPaint))
			{
				this.PaintWithErrorHandling(e, 1);
				e.ResetGraphics();
				this.PaintWithErrorHandling(e, 2);
				return;
			}
			Control.PrintPaintEventArgs printPaintEventArgs = e as Control.PrintPaintEventArgs;
			bool flag = false;
			IntPtr intPtr = IntPtr.Zero;
			Message message;
			if (printPaintEventArgs == null)
			{
				IntPtr lparam = (IntPtr)30;
				intPtr = e.HDC;
				if (intPtr == IntPtr.Zero)
				{
					intPtr = e.Graphics.GetHdc();
					flag = true;
				}
				message = Message.Create(this.Handle, 792, intPtr, lparam);
			}
			else
			{
				message = printPaintEventArgs.Message;
			}
			try
			{
				this.DefWndProc(ref message);
			}
			finally
			{
				if (flag)
				{
					e.Graphics.ReleaseHdcInternal(intPtr);
				}
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00035AD8 File Offset: 0x00033CD8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTabIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventTabIndex] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00035B08 File Offset: 0x00033D08
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTabStopChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventTabStop] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00035B38 File Offset: 0x00033D38
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventText] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00035B68 File Offset: 0x00033D68
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnVisibleChanged(EventArgs e)
		{
			bool visible = this.Visible;
			if (visible)
			{
				this.UnhookMouseEvent();
				this.trackMouseEvent = null;
			}
			if (this.parent != null && visible && !this.Created && !this.GetAnyDisposingInHierarchy())
			{
				this.CreateControl();
			}
			EventHandler eventHandler = base.Events[Control.EventVisible] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					Control control = controlCollection[i];
					if (control.Visible)
					{
						control.OnParentVisibleChanged(e);
					}
					if (!visible)
					{
						control.OnParentBecameInvisible();
					}
				}
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00035C28 File Offset: 0x00033E28
		internal virtual void OnTopMostActiveXParentChanged(EventArgs e)
		{
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnTopMostActiveXParentChanged(e);
				}
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00035C6C File Offset: 0x00033E6C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventParent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.TopMostParent.IsActiveX)
			{
				this.OnTopMostActiveXParentChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00035CB4 File Offset: 0x00033EB4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00035CE4 File Offset: 0x00033EE4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClientSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventClientSize] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00035D14 File Offset: 0x00033F14
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnControlAdded(ControlEventArgs e)
		{
			ControlEventHandler controlEventHandler = (ControlEventHandler)base.Events[Control.EventControlAdded];
			if (controlEventHandler != null)
			{
				controlEventHandler(this, e);
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00035D44 File Offset: 0x00033F44
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnControlRemoved(ControlEventArgs e)
		{
			ControlEventHandler controlEventHandler = (ControlEventHandler)base.Events[Control.EventControlRemoved];
			if (controlEventHandler != null)
			{
				controlEventHandler(this, e);
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCreateControl()
		{
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00035D74 File Offset: 0x00033F74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHandleCreated(EventArgs e)
		{
			if (this.IsHandleCreated)
			{
				if (!this.GetStyle(ControlStyles.UserPaint))
				{
					this.SetWindowFont();
				}
				if (DpiHelper.EnableDpiChangedMessageHandling && !typeof(Form).IsAssignableFrom(base.GetType()))
				{
					int num = this.deviceDpi;
					this.deviceDpi = (int)UnsafeNativeMethods.GetDpiForWindow(new HandleRef(this, this.HandleInternal));
					if (num != this.deviceDpi)
					{
						this.RescaleConstantsForDpi(num, this.deviceDpi);
					}
				}
				this.SetAcceptDrops(this.AllowDrop);
				Region region = (Region)this.Properties.GetObject(Control.PropRegion);
				if (region != null)
				{
					IntPtr intPtr = this.GetHRgn(region);
					try
					{
						if (this.IsActiveX)
						{
							intPtr = this.ActiveXMergeRegion(intPtr);
						}
						if (UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, this.Handle), new HandleRef(this, intPtr), SafeNativeMethods.IsWindowVisible(new HandleRef(this, this.Handle))) != 0)
						{
							intPtr = IntPtr.Zero;
						}
					}
					finally
					{
						if (intPtr != IntPtr.Zero)
						{
							SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
						}
					}
				}
				Control.ControlAccessibleObject controlAccessibleObject = this.Properties.GetObject(Control.PropAccessibility) as Control.ControlAccessibleObject;
				Control.ControlAccessibleObject controlAccessibleObject2 = this.Properties.GetObject(Control.PropNcAccessibility) as Control.ControlAccessibleObject;
				IntPtr handle = this.Handle;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					if (controlAccessibleObject != null)
					{
						controlAccessibleObject.Handle = handle;
					}
					if (controlAccessibleObject2 != null)
					{
						controlAccessibleObject2.Handle = handle;
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (this.text != null && this.text.Length != 0)
				{
					UnsafeNativeMethods.SetWindowText(new HandleRef(this, this.Handle), this.text);
				}
				if (!(this is ScrollableControl) && !this.IsMirrored && this.GetState2(2) && !this.GetState2(1))
				{
					this.BeginInvoke(new EventHandler(this.OnSetScrollPosition));
					this.SetState2(1, true);
					this.SetState2(2, false);
				}
				if (this.GetState2(8))
				{
					this.ListenToUserPreferenceChanged(this.GetTopLevel());
				}
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventHandleCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.IsHandleCreated && this.GetState(32768))
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this, this.Handle), Control.threadCallbackMessage, IntPtr.Zero, IntPtr.Zero);
				this.SetState(32768, false);
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00035FE4 File Offset: 0x000341E4
		private void OnSetScrollPosition(object sender, EventArgs e)
		{
			this.SetState2(1, false);
			this.OnInvokedSetScrollPosition(sender, e);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00035FF8 File Offset: 0x000341F8
		internal virtual void OnInvokedSetScrollPosition(object sender, EventArgs e)
		{
			if (!(this is ScrollableControl) && !this.IsMirrored)
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
				scrollinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
				scrollinfo.fMask = 1;
				if (UnsafeNativeMethods.GetScrollInfo(new HandleRef(this, this.Handle), 0, scrollinfo))
				{
					scrollinfo.nPos = ((this.RightToLeft == RightToLeft.Yes) ? scrollinfo.nMax : scrollinfo.nMin);
					this.SendMessage(276, NativeMethods.Util.MAKELPARAM(4, scrollinfo.nPos), 0);
				}
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00036084 File Offset: 0x00034284
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLocationChanged(EventArgs e)
		{
			this.OnMove(EventArgs.Empty);
			EventHandler eventHandler = base.Events[Control.EventLocation] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000360C0 File Offset: 0x000342C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHandleDestroyed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventHandleDestroyed];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			this.UpdateReflectParent(false);
			if (!this.RecreatingHandle)
			{
				if (this.GetState(2097152))
				{
					object @object = this.Properties.GetObject(Control.PropBackBrush);
					if (@object != null)
					{
						IntPtr intPtr = (IntPtr)@object;
						if (intPtr != IntPtr.Zero)
						{
							SafeNativeMethods.DeleteObject(new HandleRef(this, intPtr));
						}
						this.Properties.SetObject(Control.PropBackBrush, null);
					}
				}
				this.ListenToUserPreferenceChanged(false);
			}
			try
			{
				if (!this.GetAnyDisposingInHierarchy())
				{
					this.text = this.Text;
					if (this.text != null && this.text.Length == 0)
					{
						this.text = null;
					}
				}
				this.SetAcceptDrops(false);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000361B0 File Offset: 0x000343B0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDoubleClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventDoubleClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000361E0 File Offset: 0x000343E0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragEnter(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.EventDragEnter];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00036210 File Offset: 0x00034410
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragOver(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.EventDragOver];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00036240 File Offset: 0x00034440
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventDragLeave];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00036270 File Offset: 0x00034470
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragDrop(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.EventDragDrop];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000362A0 File Offset: 0x000344A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
		{
			GiveFeedbackEventHandler giveFeedbackEventHandler = (GiveFeedbackEventHandler)base.Events[Control.EventGiveFeedback];
			if (giveFeedbackEventHandler != null)
			{
				giveFeedbackEventHandler(this, gfbevent);
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000362D0 File Offset: 0x000344D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnEnter(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventEnter];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000362FE File Offset: 0x000344FE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void InvokeGotFocus(Control toInvoke, EventArgs e)
		{
			if (toInvoke != null)
			{
				toInvoke.OnGotFocus(e);
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutGotFocus(toInvoke);
				}
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003631C File Offset: 0x0003451C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnGotFocus(EventArgs e)
		{
			if (this.IsActiveX)
			{
				this.ActiveXOnFocus(true);
			}
			if (this.parent != null)
			{
				this.parent.ChildGotFocus(this);
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventGotFocus];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00036370 File Offset: 0x00034570
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHelpRequested(HelpEventArgs hevent)
		{
			HelpEventHandler helpEventHandler = (HelpEventHandler)base.Events[Control.EventHelpRequested];
			if (helpEventHandler != null)
			{
				helpEventHandler(this, hevent);
				hevent.Handled = true;
			}
			if (!hevent.Handled && this.ParentInternal != null)
			{
				this.ParentInternal.OnHelpRequested(hevent);
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000363C4 File Offset: 0x000345C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnInvalidated(InvalidateEventArgs e)
		{
			if (this.IsActiveX)
			{
				this.ActiveXViewChanged();
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentInvalidated(e);
				}
			}
			InvalidateEventHandler invalidateEventHandler = (InvalidateEventHandler)base.Events[Control.EventInvalidated];
			if (invalidateEventHandler != null)
			{
				invalidateEventHandler(this, e);
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00036438 File Offset: 0x00034638
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnKeyDown(KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[Control.EventKeyDown];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00036468 File Offset: 0x00034668
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnKeyPress(KeyPressEventArgs e)
		{
			KeyPressEventHandler keyPressEventHandler = (KeyPressEventHandler)base.Events[Control.EventKeyPress];
			if (keyPressEventHandler != null)
			{
				keyPressEventHandler(this, e);
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00036498 File Offset: 0x00034698
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnKeyUp(KeyEventArgs e)
		{
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay && OsVersion.IsWindows11_OrGreater && (e.KeyCode.HasFlag(Keys.ControlKey) || e.KeyCode == Keys.Escape))
			{
				KeyboardToolTipStateMachine.HidePersistentTooltip();
			}
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[Control.EventKeyUp];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000364FC File Offset: 0x000346FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLayout(LayoutEventArgs levent)
		{
			if (this.IsActiveX)
			{
				this.ActiveXViewChanged();
			}
			LayoutEventHandler layoutEventHandler = (LayoutEventHandler)base.Events[Control.EventLayout];
			if (layoutEventHandler != null)
			{
				layoutEventHandler(this, levent);
			}
			bool flag = this.LayoutEngine.Layout(this, levent);
			if (flag && this.ParentInternal != null)
			{
				this.ParentInternal.SetState(8388608, true);
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00036562 File Offset: 0x00034762
		internal virtual void OnLayoutResuming(bool performLayout)
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.OnChildLayoutResuming(this, performLayout);
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnLayoutSuspended()
		{
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0003657C File Offset: 0x0003477C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventLeave];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000365AA File Offset: 0x000347AA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void InvokeLostFocus(Control toInvoke, EventArgs e)
		{
			if (toInvoke != null)
			{
				toInvoke.OnLostFocus(e);
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(toInvoke);
				}
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000365C8 File Offset: 0x000347C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLostFocus(EventArgs e)
		{
			if (this.IsActiveX)
			{
				this.ActiveXOnFocus(false);
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventLostFocus];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00036608 File Offset: 0x00034808
		protected virtual void OnMarginChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMarginChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00036638 File Offset: 0x00034838
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseDoubleClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseDoubleClick];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00036668 File Offset: 0x00034868
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseClick];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00036698 File Offset: 0x00034898
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseCaptureChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMouseCaptureChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000366C8 File Offset: 0x000348C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseDown(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseDown];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000366F8 File Offset: 0x000348F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseEnter(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMouseEnter];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00036728 File Offset: 0x00034928
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMouseLeave];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00036756 File Offset: 0x00034956
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnDpiChangedBeforeParent(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventDpiChangedBeforeParent];
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(this, e);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00036779 File Offset: 0x00034979
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnDpiChangedAfterParent(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventDpiChangedAfterParent];
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(this, e);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0003679C File Offset: 0x0003499C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseHover(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMouseHover];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000367CC File Offset: 0x000349CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseMove(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseMove];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000367FC File Offset: 0x000349FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseUp(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseUp];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003682C File Offset: 0x00034A2C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseWheel(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseWheel];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0003685C File Offset: 0x00034A5C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMove(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMove];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.RenderTransparent)
			{
				this.Invalidate();
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00036898 File Offset: 0x00034A98
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPaint(PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[Control.EventPaint];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000368C8 File Offset: 0x00034AC8
		protected virtual void OnPaddingChanged(EventArgs e)
		{
			if (this.GetStyle(ControlStyles.ResizeRedraw))
			{
				this.Invalidate();
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventPaddingChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00036908 File Offset: 0x00034B08
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPaintBackground(PaintEventArgs pevent)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetClientRect(new HandleRef(this.window, this.InternalHandle), ref rect);
			this.PaintBackground(pevent, new Rectangle(rect.left, rect.top, rect.right, rect.bottom));
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003695C File Offset: 0x00034B5C
		private void OnParentInvalidated(InvalidateEventArgs e)
		{
			if (!this.RenderTransparent)
			{
				return;
			}
			if (this.IsHandleCreated)
			{
				Rectangle rectangle = e.InvalidRect;
				Point location = this.Location;
				rectangle.Offset(-location.X, -location.Y);
				rectangle = Rectangle.Intersect(this.ClientRectangle, rectangle);
				if (rectangle.IsEmpty)
				{
					return;
				}
				this.Invalidate(rectangle);
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000369C0 File Offset: 0x00034BC0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
		{
			QueryContinueDragEventHandler queryContinueDragEventHandler = (QueryContinueDragEventHandler)base.Events[Control.EventQueryContinueDrag];
			if (queryContinueDragEventHandler != null)
			{
				queryContinueDragEventHandler(this, qcdevent);
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000369F0 File Offset: 0x00034BF0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRegionChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventRegionChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00036A20 File Offset: 0x00034C20
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResize(EventArgs e)
		{
			if ((this.controlStyle & ControlStyles.ResizeRedraw) == ControlStyles.ResizeRedraw || this.GetState(4194304))
			{
				this.Invalidate();
			}
			LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventResize];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00036A7C File Offset: 0x00034C7C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			PreviewKeyDownEventHandler previewKeyDownEventHandler = (PreviewKeyDownEventHandler)base.Events[Control.EventPreviewKeyDown];
			if (previewKeyDownEventHandler != null)
			{
				previewKeyDownEventHandler(this, e);
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00036AAC File Offset: 0x00034CAC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnSizeChanged(EventArgs e)
		{
			this.OnResize(EventArgs.Empty);
			EventHandler eventHandler = base.Events[Control.EventSize] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00036AE8 File Offset: 0x00034CE8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnChangeUICues(UICuesEventArgs e)
		{
			UICuesEventHandler uicuesEventHandler = (UICuesEventHandler)base.Events[Control.EventChangeUICues];
			if (uicuesEventHandler != null)
			{
				uicuesEventHandler(this, e);
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00036B18 File Offset: 0x00034D18
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventStyleChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00036B48 File Offset: 0x00034D48
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnSystemColorsChanged(EventArgs e)
		{
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnSystemColorsChanged(EventArgs.Empty);
				}
			}
			this.Invalidate();
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventSystemColorsChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00036BB8 File Offset: 0x00034DB8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnValidating(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Control.EventValidating];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00036BE8 File Offset: 0x00034DE8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnValidated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventValidated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00036C16 File Offset: 0x00034E16
		internal void PaintBackground(PaintEventArgs e, Rectangle rectangle)
		{
			this.PaintBackground(e, rectangle, this.BackColor, Point.Empty);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00036C2B File Offset: 0x00034E2B
		internal void PaintBackground(PaintEventArgs e, Rectangle rectangle, Color backColor)
		{
			this.PaintBackground(e, rectangle, backColor, Point.Empty);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00036C3C File Offset: 0x00034E3C
		internal void PaintBackground(PaintEventArgs e, Rectangle rectangle, Color backColor, Point scrollOffset)
		{
			if (this.RenderColorTransparent(backColor))
			{
				this.PaintTransparentBackground(e, rectangle);
			}
			bool flag = (this is Form || this is MdiClient) && this.IsMirrored;
			if (this.BackgroundImage != null && !DisplayInformation.HighContrast && !flag)
			{
				if (this.BackgroundImageLayout == ImageLayout.Tile && ControlPaint.IsImageTransparent(this.BackgroundImage))
				{
					this.PaintTransparentBackground(e, rectangle);
				}
				Point point = scrollOffset;
				ScrollableControl scrollableControl = this as ScrollableControl;
				if (scrollableControl != null && point != Point.Empty)
				{
					point = ((ScrollableControl)this).AutoScrollPosition;
				}
				if (ControlPaint.IsImageTransparent(this.BackgroundImage))
				{
					Control.PaintBackColor(e, rectangle, backColor);
				}
				ControlPaint.DrawBackgroundImage(e.Graphics, this.BackgroundImage, backColor, this.BackgroundImageLayout, this.ClientRectangle, rectangle, point, this.RightToLeft);
				return;
			}
			Control.PaintBackColor(e, rectangle, backColor);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00036D18 File Offset: 0x00034F18
		private static void PaintBackColor(PaintEventArgs e, Rectangle rectangle, Color backColor)
		{
			Color color = backColor;
			if (color.A == 255)
			{
				using (WindowsGraphics windowsGraphics = (e.HDC != IntPtr.Zero && DisplayInformation.BitsPerPixel > 8) ? WindowsGraphics.FromHdc(e.HDC) : WindowsGraphics.FromGraphics(e.Graphics))
				{
					color = windowsGraphics.GetNearestColor(color);
					using (WindowsBrush windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, color))
					{
						windowsGraphics.FillRectangle(windowsBrush, rectangle);
						return;
					}
				}
			}
			if (color.A > 0)
			{
				using (Brush brush = new SolidBrush(color))
				{
					e.Graphics.FillRectangle(brush, rectangle);
				}
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00036DF0 File Offset: 0x00034FF0
		private void PaintException(PaintEventArgs e)
		{
			int num = 2;
			using (Pen pen = new Pen(Color.Red, (float)num))
			{
				Rectangle clientRectangle = this.ClientRectangle;
				Rectangle rect = clientRectangle;
				int num2 = rect.X;
				rect.X = num2 + 1;
				num2 = rect.Y;
				rect.Y = num2 + 1;
				num2 = rect.Width;
				rect.Width = num2 - 1;
				num2 = rect.Height;
				rect.Height = num2 - 1;
				e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
				rect.Inflate(-1, -1);
				e.Graphics.FillRectangle(Brushes.White, rect);
				e.Graphics.DrawLine(pen, clientRectangle.Left, clientRectangle.Top, clientRectangle.Right, clientRectangle.Bottom);
				e.Graphics.DrawLine(pen, clientRectangle.Left, clientRectangle.Bottom, clientRectangle.Right, clientRectangle.Top);
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00036F14 File Offset: 0x00035114
		internal void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle)
		{
			this.PaintTransparentBackground(e, rectangle, null);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00036F20 File Offset: 0x00035120
		internal void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle, Region transparentRegion)
		{
			Graphics graphics = e.Graphics;
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				if (Application.RenderWithVisualStyles && parentInternal.RenderTransparencyWithVisualStyles)
				{
					GraphicsState graphicsState = null;
					if (transparentRegion != null)
					{
						graphicsState = graphics.Save();
					}
					try
					{
						if (transparentRegion != null)
						{
							graphics.Clip = transparentRegion;
						}
						ButtonRenderer.DrawParentBackground(graphics, rectangle, this);
						return;
					}
					finally
					{
						if (graphicsState != null)
						{
							graphics.Restore(graphicsState);
						}
					}
				}
				Rectangle rectangle2 = new Rectangle(-this.Left, -this.Top, parentInternal.Width, parentInternal.Height);
				Rectangle clipRect = new Rectangle(rectangle.Left + this.Left, rectangle.Top + this.Top, rectangle.Width, rectangle.Height);
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(graphics))
				{
					windowsGraphics.DeviceContext.TranslateTransform(-this.Left, -this.Top);
					using (PaintEventArgs paintEventArgs = new PaintEventArgs(windowsGraphics.GetHdc(), clipRect))
					{
						if (transparentRegion != null)
						{
							paintEventArgs.Graphics.Clip = transparentRegion;
							paintEventArgs.Graphics.TranslateClip(-rectangle2.X, -rectangle2.Y);
						}
						try
						{
							this.InvokePaintBackground(parentInternal, paintEventArgs);
							this.InvokePaint(parentInternal, paintEventArgs);
							return;
						}
						finally
						{
							if (transparentRegion != null)
							{
								paintEventArgs.Graphics.TranslateClip(rectangle2.X, rectangle2.Y);
							}
						}
					}
				}
			}
			graphics.FillRectangle(SystemBrushes.Control, rectangle);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000370BC File Offset: 0x000352BC
		private void PaintWithErrorHandling(PaintEventArgs e, short layer)
		{
			try
			{
				this.CacheTextInternal = true;
				if (this.GetState(4194304))
				{
					if (layer == 1)
					{
						this.PaintException(e);
					}
				}
				else
				{
					bool flag = true;
					try
					{
						if (layer != 1)
						{
							if (layer == 2)
							{
								this.OnPaint(e);
							}
						}
						else if (!this.GetStyle(ControlStyles.Opaque))
						{
							this.OnPaintBackground(e);
						}
						flag = false;
					}
					finally
					{
						if (flag)
						{
							this.SetState(4194304, true);
							this.Invalidate();
						}
					}
				}
			}
			finally
			{
				this.CacheTextInternal = false;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00037150 File Offset: 0x00035350
		internal ContainerControl ParentContainerControl
		{
			get
			{
				for (Control parentInternal = this.ParentInternal; parentInternal != null; parentInternal = parentInternal.ParentInternal)
				{
					if (parentInternal is ContainerControl)
					{
						return parentInternal as ContainerControl;
					}
				}
				return null;
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00037180 File Offset: 0x00035380
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void PerformLayout()
		{
			if (this.cachedLayoutEventArgs != null)
			{
				this.PerformLayout(this.cachedLayoutEventArgs);
				this.cachedLayoutEventArgs = null;
				this.SetState2(64, false);
				return;
			}
			this.PerformLayout(null, null);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000371AF File Offset: 0x000353AF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void PerformLayout(Control affectedControl, string affectedProperty)
		{
			this.PerformLayout(new LayoutEventArgs(affectedControl, affectedProperty));
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000371C0 File Offset: 0x000353C0
		internal void PerformLayout(LayoutEventArgs args)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.layoutSuspendCount > 0)
			{
				this.SetState(512, true);
				if (this.cachedLayoutEventArgs == null || (this.GetState2(64) && args != null))
				{
					this.cachedLayoutEventArgs = args;
					if (this.GetState2(64))
					{
						this.SetState2(64, false);
					}
				}
				this.LayoutEngine.ProcessSuspendedLayoutEventArgs(this, args);
				return;
			}
			this.layoutSuspendCount = 1;
			try
			{
				this.CacheTextInternal = true;
				this.OnLayout(args);
			}
			finally
			{
				this.CacheTextInternal = false;
				this.SetState(8389120, false);
				this.layoutSuspendCount = 0;
				if (this.ParentInternal != null && this.ParentInternal.GetState(8388608))
				{
					LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.PreferredSize);
				}
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00037298 File Offset: 0x00035498
		internal bool PerformControlValidation(bool bulkValidation)
		{
			if (!this.CausesValidation)
			{
				return false;
			}
			if (this.NotifyValidating())
			{
				return true;
			}
			if (bulkValidation || NativeWindow.WndProcShouldBeDebuggable)
			{
				this.NotifyValidated();
			}
			else
			{
				try
				{
					this.NotifyValidated();
				}
				catch (Exception t)
				{
					Application.OnThreadException(t);
				}
			}
			return false;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000372F0 File Offset: 0x000354F0
		internal bool PerformContainerValidation(ValidationConstraints validationConstraints)
		{
			bool result = false;
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if ((validationConstraints & ValidationConstraints.ImmediateChildren) != ValidationConstraints.ImmediateChildren && control.ShouldPerformContainerValidation() && control.PerformContainerValidation(validationConstraints))
				{
					result = true;
				}
				if (((validationConstraints & ValidationConstraints.Selectable) != ValidationConstraints.Selectable || control.GetStyle(ControlStyles.Selectable)) && ((validationConstraints & ValidationConstraints.Enabled) != ValidationConstraints.Enabled || control.Enabled) && ((validationConstraints & ValidationConstraints.Visible) != ValidationConstraints.Visible || control.Visible) && ((validationConstraints & ValidationConstraints.TabStop) != ValidationConstraints.TabStop || control.TabStop) && control.PerformControlValidation(true))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000373A8 File Offset: 0x000355A8
		public Point PointToClient(Point p)
		{
			return this.PointToClientInternal(p);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000373B4 File Offset: 0x000355B4
		internal Point PointToClientInternal(Point p)
		{
			NativeMethods.POINT point = new NativeMethods.POINT(p.X, p.Y);
			UnsafeNativeMethods.MapWindowPoints(NativeMethods.NullHandleRef, new HandleRef(this, this.Handle), point, 1);
			return new Point(point.x, point.y);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00037400 File Offset: 0x00035600
		public Point PointToScreen(Point p)
		{
			NativeMethods.POINT point = new NativeMethods.POINT(p.X, p.Y);
			UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, this.Handle), NativeMethods.NullHandleRef, point, 1);
			return new Point(point.x, point.y);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003744C File Offset: 0x0003564C
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual bool PreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256 || msg.Msg == 260)
			{
				if (!this.GetState2(512))
				{
					this.ProcessUICues(ref msg);
				}
				Keys keyData = (Keys)((long)msg.WParam) | Control.ModifierKeys;
				if (this.ProcessCmdKey(ref msg, keyData))
				{
					return true;
				}
				if (this.IsInputKey(keyData))
				{
					this.SetState2(128, true);
					return false;
				}
				IntSecurity.ModifyFocus.Assert();
				try
				{
					return this.ProcessDialogKey(keyData);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			bool result;
			if (msg.Msg == 258 || msg.Msg == 262)
			{
				if (msg.Msg == 258 && this.IsInputChar((char)((int)msg.WParam)))
				{
					this.SetState2(256, true);
					result = false;
				}
				else
				{
					result = this.ProcessDialogChar((char)((int)msg.WParam));
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00037554 File Offset: 0x00035754
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public PreProcessControlState PreProcessControlMessage(ref Message msg)
		{
			return Control.PreProcessControlMessageInternal(null, ref msg);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00037560 File Offset: 0x00035760
		internal static PreProcessControlState PreProcessControlMessageInternal(Control target, ref Message msg)
		{
			if (target == null)
			{
				target = Control.FromChildHandleInternal(msg.HWnd);
			}
			if (target == null)
			{
				return PreProcessControlState.MessageNotNeeded;
			}
			target.SetState2(128, false);
			target.SetState2(256, false);
			target.SetState2(512, true);
			PreProcessControlState result;
			try
			{
				Keys keyData = (Keys)((long)msg.WParam) | Control.ModifierKeys;
				if (msg.Msg == 256 || msg.Msg == 260)
				{
					target.ProcessUICues(ref msg);
					PreviewKeyDownEventArgs previewKeyDownEventArgs = new PreviewKeyDownEventArgs(keyData);
					target.OnPreviewKeyDown(previewKeyDownEventArgs);
					if (previewKeyDownEventArgs.IsInputKey)
					{
						return PreProcessControlState.MessageNeeded;
					}
				}
				PreProcessControlState preProcessControlState = PreProcessControlState.MessageNotNeeded;
				if (!target.PreProcessMessage(ref msg))
				{
					if (msg.Msg == 256 || msg.Msg == 260)
					{
						if (target.GetState2(128) || target.IsInputKey(keyData))
						{
							preProcessControlState = PreProcessControlState.MessageNeeded;
						}
					}
					else if ((msg.Msg == 258 || msg.Msg == 262) && (target.GetState2(256) || target.IsInputChar((char)((int)msg.WParam))))
					{
						preProcessControlState = PreProcessControlState.MessageNeeded;
					}
				}
				else
				{
					preProcessControlState = PreProcessControlState.MessageProcessed;
				}
				result = preProcessControlState;
			}
			finally
			{
				target.SetState2(512, false);
			}
			return result;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003769C File Offset: 0x0003589C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
			return (contextMenu != null && contextMenu.ProcessCmdKey(ref msg, keyData, this)) || (this.parent != null && this.parent.ProcessCmdKey(ref msg, keyData));
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000376E8 File Offset: 0x000358E8
		private void PrintToMetaFile(HandleRef hDC, IntPtr lParam)
		{
			lParam = (IntPtr)((long)lParam & -17L);
			NativeMethods.POINT point = new NativeMethods.POINT();
			bool flag = SafeNativeMethods.GetViewportOrgEx(hDC, point);
			HandleRef handleRef = new HandleRef(null, SafeNativeMethods.CreateRectRgn(point.x, point.y, point.x + this.Width, point.y + this.Height));
			try
			{
				NativeMethods.RegionFlags regionFlags = (NativeMethods.RegionFlags)SafeNativeMethods.SelectClipRgn(hDC, handleRef);
				this.PrintToMetaFileRecursive(hDC, lParam, new Rectangle(Point.Empty, this.Size));
			}
			finally
			{
				flag = SafeNativeMethods.DeleteObject(handleRef);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00037784 File Offset: 0x00035984
		internal virtual void PrintToMetaFileRecursive(HandleRef hDC, IntPtr lParam, Rectangle bounds)
		{
			using (new WindowsFormsUtils.DCMapping(hDC, bounds))
			{
				this.PrintToMetaFile_SendPrintMessage(hDC, (IntPtr)((long)lParam & -5L));
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				bool windowRect = UnsafeNativeMethods.GetWindowRect(new HandleRef(null, this.Handle), ref rect);
				Point location = this.PointToScreen(Point.Empty);
				location = new Point(location.X - rect.left, location.Y - rect.top);
				Rectangle bounds2 = new Rectangle(location, this.ClientSize);
				using (new WindowsFormsUtils.DCMapping(hDC, bounds2))
				{
					this.PrintToMetaFile_SendPrintMessage(hDC, (IntPtr)((long)lParam & -3L));
					int count = this.Controls.Count;
					for (int i = count - 1; i >= 0; i--)
					{
						Control control = this.Controls[i];
						if (control.Visible)
						{
							control.PrintToMetaFileRecursive(hDC, lParam, control.Bounds);
						}
					}
				}
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000378AC File Offset: 0x00035AAC
		private void PrintToMetaFile_SendPrintMessage(HandleRef hDC, IntPtr lParam)
		{
			if (this.GetStyle(ControlStyles.UserPaint))
			{
				this.SendMessage(791, hDC.Handle, lParam);
				return;
			}
			if (this.Controls.Count == 0)
			{
				lParam = (IntPtr)((long)lParam | 16L);
			}
			using (Control.MetafileDCWrapper metafileDCWrapper = new Control.MetafileDCWrapper(hDC, this.Size))
			{
				this.SendMessage(791, metafileDCWrapper.HDC, lParam);
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00037934 File Offset: 0x00035B34
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool ProcessDialogChar(char charCode)
		{
			return this.parent != null && this.parent.ProcessDialogChar(charCode);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0003794C File Offset: 0x00035B4C
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool ProcessDialogKey(Keys keyData)
		{
			return this.parent != null && this.parent.ProcessDialogKey(keyData);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00037964 File Offset: 0x00035B64
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual bool ProcessKeyEventArgs(ref Message m)
		{
			KeyEventArgs keyEventArgs = null;
			KeyPressEventArgs keyPressEventArgs = null;
			IntPtr wparam = IntPtr.Zero;
			if (m.Msg == 258 || m.Msg == 262)
			{
				int num = this.ImeWmCharsToIgnore;
				if (num > 0)
				{
					num--;
					this.ImeWmCharsToIgnore = num;
					return false;
				}
				keyPressEventArgs = new KeyPressEventArgs((char)((long)m.WParam));
				this.OnKeyPress(keyPressEventArgs);
				wparam = (IntPtr)((int)keyPressEventArgs.KeyChar);
			}
			else if (m.Msg == 646)
			{
				int num2 = this.ImeWmCharsToIgnore;
				if (Marshal.SystemDefaultCharSize == 1)
				{
					char keyChar = '\0';
					byte[] array = new byte[]
					{
						(byte)((int)((long)m.WParam) >> 8),
						(byte)((long)m.WParam)
					};
					char[] array2 = new char[1];
					int num3 = UnsafeNativeMethods.MultiByteToWideChar(0, 1, array, array.Length, array2, 0);
					if (num3 <= 0)
					{
						throw new Win32Exception();
					}
					array2 = new char[num3];
					UnsafeNativeMethods.MultiByteToWideChar(0, 1, array, array.Length, array2, array2.Length);
					if (array2[0] != '\0')
					{
						keyChar = array2[0];
						num2 += 2;
					}
					else if (array2[0] == '\0' && array2.Length >= 2)
					{
						keyChar = array2[1];
						num2++;
					}
					this.ImeWmCharsToIgnore = num2;
					keyPressEventArgs = new KeyPressEventArgs(keyChar);
				}
				else
				{
					num2 += 3 - Marshal.SystemDefaultCharSize;
					this.ImeWmCharsToIgnore = num2;
					keyPressEventArgs = new KeyPressEventArgs((char)((long)m.WParam));
				}
				char keyChar2 = keyPressEventArgs.KeyChar;
				this.OnKeyPress(keyPressEventArgs);
				if (keyPressEventArgs.KeyChar == keyChar2)
				{
					wparam = m.WParam;
				}
				else if (Marshal.SystemDefaultCharSize == 1)
				{
					string text = new string(new char[]
					{
						keyPressEventArgs.KeyChar
					});
					int num4 = UnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
					if (num4 >= 2)
					{
						byte[] array3 = new byte[num4];
						UnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, array3, array3.Length, IntPtr.Zero, IntPtr.Zero);
						int num5 = Marshal.SizeOf(typeof(IntPtr));
						if (num4 > num5)
						{
							num4 = num5;
						}
						long num6 = 0L;
						for (int i = 0; i < num4; i++)
						{
							num6 <<= 8;
							num6 |= (long)((ulong)array3[i]);
						}
						wparam = (IntPtr)num6;
					}
					else if (num4 == 1)
					{
						byte[] array3 = new byte[num4];
						UnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, array3, array3.Length, IntPtr.Zero, IntPtr.Zero);
						wparam = (IntPtr)((int)array3[0]);
					}
					else
					{
						wparam = m.WParam;
					}
				}
				else
				{
					wparam = (IntPtr)((int)keyPressEventArgs.KeyChar);
				}
			}
			else
			{
				keyEventArgs = new KeyEventArgs((Keys)((long)m.WParam) | Control.ModifierKeys);
				if (m.Msg == 256 || m.Msg == 260)
				{
					this.OnKeyDown(keyEventArgs);
				}
				else
				{
					this.OnKeyUp(keyEventArgs);
				}
			}
			if (keyPressEventArgs != null)
			{
				m.WParam = wparam;
				return keyPressEventArgs.Handled;
			}
			if (keyEventArgs.SuppressKeyPress)
			{
				this.RemovePendingMessages(258, 258);
				this.RemovePendingMessages(262, 262);
				this.RemovePendingMessages(646, 646);
			}
			return keyEventArgs.Handled;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00037C96 File Offset: 0x00035E96
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal virtual bool ProcessKeyMessage(ref Message m)
		{
			return (this.parent != null && this.parent.ProcessKeyPreview(ref m)) || this.ProcessKeyEventArgs(ref m);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00037CB7 File Offset: 0x00035EB7
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual bool ProcessKeyPreview(ref Message m)
		{
			return this.parent != null && this.parent.ProcessKeyPreview(ref m);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00011A20 File Offset: 0x0000FC20
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal virtual bool ProcessMnemonic(char charCode)
		{
			return false;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00037CD0 File Offset: 0x00035ED0
		internal void ProcessUICues(ref Message msg)
		{
			Keys keys = (Keys)((int)msg.WParam & 65535);
			if (keys != Keys.F10 && keys != Keys.Menu && keys != Keys.Tab)
			{
				return;
			}
			Control control = null;
			int num = (int)((long)this.SendMessage(297, 0, 0));
			if (num == 0)
			{
				control = this.TopMostParent;
				num = (int)control.SendMessage(297, 0, 0);
			}
			int num2 = 0;
			if ((keys == Keys.F10 || keys == Keys.Menu) && (num & 2) != 0)
			{
				num2 |= 2;
			}
			if (keys == Keys.Tab && (num & 1) != 0)
			{
				num2 |= 1;
			}
			if (num2 != 0)
			{
				if (control == null)
				{
					control = this.TopMostParent;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(control, control.Handle), (UnsafeNativeMethods.GetParent(new HandleRef(null, control.Handle)) == IntPtr.Zero) ? 295 : 296, (IntPtr)(2 | num2 << 16), IntPtr.Zero);
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00037DB0 File Offset: 0x00035FB0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseDragEvent(object key, DragEventArgs e)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[key];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, e);
			}
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00037DDC File Offset: 0x00035FDC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaisePaintEvent(object key, PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[Control.EventPaint];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00037E0C File Offset: 0x0003600C
		private void RemovePendingMessages(int msgMin, int msgMax)
		{
			if (!this.IsDisposed)
			{
				NativeMethods.MSG msg = default(NativeMethods.MSG);
				IntPtr handle = this.Handle;
				while (UnsafeNativeMethods.PeekMessage(ref msg, new HandleRef(this, handle), msgMin, msgMax, 1))
				{
				}
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00037E43 File Offset: 0x00036043
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetBackColor()
		{
			this.BackColor = Color.Empty;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00037E50 File Offset: 0x00036050
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetCursor()
		{
			this.Cursor = null;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00037E59 File Offset: 0x00036059
		private void ResetEnabled()
		{
			this.Enabled = true;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00037E62 File Offset: 0x00036062
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetFont()
		{
			this.Font = null;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00037E6B File Offset: 0x0003606B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetForeColor()
		{
			this.ForeColor = Color.Empty;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00037E78 File Offset: 0x00036078
		private void ResetLocation()
		{
			this.Location = new Point(0, 0);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00037E87 File Offset: 0x00036087
		private void ResetMargin()
		{
			this.Margin = this.DefaultMargin;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00037E95 File Offset: 0x00036095
		private void ResetMinimumSize()
		{
			this.MinimumSize = this.DefaultMinimumSize;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00037EA3 File Offset: 0x000360A3
		private void ResetPadding()
		{
			CommonProperties.ResetPadding(this);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00037EAB File Offset: 0x000360AB
		private void ResetSize()
		{
			this.Size = this.DefaultSize;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00037EB9 File Offset: 0x000360B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetRightToLeft()
		{
			this.RightToLeft = RightToLeft.Inherit;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00037EC2 File Offset: 0x000360C2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RecreateHandle()
		{
			this.RecreateHandleCore();
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00037ECC File Offset: 0x000360CC
		internal virtual void RecreateHandleCore()
		{
			lock (this)
			{
				if (this.IsHandleCreated)
				{
					bool containsFocus = this.ContainsFocus;
					bool flag2 = (this.state & 1) != 0;
					if (this.GetState(16384))
					{
						this.SetState(8192, true);
						this.UnhookMouseEvent();
					}
					HandleRef handleRef = new HandleRef(this, UnsafeNativeMethods.GetParent(new HandleRef(this, this.Handle)));
					try
					{
						Control[] array = null;
						this.state |= 16;
						try
						{
							Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
							if (controlCollection != null && controlCollection.Count > 0)
							{
								array = new Control[controlCollection.Count];
								for (int i = 0; i < controlCollection.Count; i++)
								{
									Control control = controlCollection[i];
									if (control != null && control.IsHandleCreated)
									{
										control.OnParentHandleRecreating();
										array[i] = control;
									}
									else
									{
										array[i] = null;
									}
								}
							}
							this.DestroyHandle();
							this.CreateHandle();
						}
						finally
						{
							this.state &= -17;
							if (array != null)
							{
								foreach (Control control2 in array)
								{
									if (control2 != null && control2.IsHandleCreated)
									{
										control2.OnParentHandleRecreated();
									}
								}
							}
						}
						if (flag2)
						{
							this.CreateControl();
						}
					}
					finally
					{
						if (handleRef.Handle != IntPtr.Zero && (Control.FromHandleInternal(handleRef.Handle) == null || this.parent == null) && UnsafeNativeMethods.IsWindow(handleRef))
						{
							UnsafeNativeMethods.SetParent(new HandleRef(this, this.Handle), handleRef);
						}
					}
					if (containsFocus)
					{
						this.FocusInternal();
					}
				}
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000380C8 File Offset: 0x000362C8
		public Rectangle RectangleToClient(Rectangle r)
		{
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(r.X, r.Y, r.Width, r.Height);
			UnsafeNativeMethods.MapWindowPoints(NativeMethods.NullHandleRef, new HandleRef(this, this.Handle), ref rect, 2);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00038130 File Offset: 0x00036330
		public Rectangle RectangleToScreen(Rectangle r)
		{
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(r.X, r.Y, r.Width, r.Height);
			UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, this.Handle), NativeMethods.NullHandleRef, ref rect, 2);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00038196 File Offset: 0x00036396
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static bool ReflectMessage(IntPtr hWnd, ref Message m)
		{
			IntSecurity.SendMessages.Demand();
			return Control.ReflectMessageInternal(hWnd, ref m);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000381AC File Offset: 0x000363AC
		internal static bool ReflectMessageInternal(IntPtr hWnd, ref Message m)
		{
			Control control = Control.FromHandleInternal(hWnd);
			if (control == null)
			{
				return false;
			}
			m.Result = control.SendMessage(8192 + m.Msg, m.WParam, m.LParam);
			return true;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000381EA File Offset: 0x000363EA
		public virtual void Refresh()
		{
			this.Invalidate(true);
			this.Update();
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000381FC File Offset: 0x000363FC
		internal virtual void ReleaseUiaProvider(IntPtr handle)
		{
			if (handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, handle), IntPtr.Zero, IntPtr.Zero, null);
				if (this.IsInternalAccessibilityObjectCreated && LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy && ApiHelper.IsApiAvailable("UIAutomationCore.dll", "UiaDisconnectProvider"))
				{
					int num = UnsafeNativeMethods.UiaDisconnectProvider(this.UnsafeAccessibilityObject);
				}
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0003825A File Offset: 0x0003645A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void ResetMouseEventArgs()
		{
			if (this.GetState(16384))
			{
				this.UnhookMouseEvent();
				this.HookMouseEvent();
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00038275 File Offset: 0x00036475
		public virtual void ResetText()
		{
			this.Text = string.Empty;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00038282 File Offset: 0x00036482
		private void ResetVisible()
		{
			this.Visible = true;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0003828B File Offset: 0x0003648B
		public void ResumeLayout()
		{
			this.ResumeLayout(true);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00038294 File Offset: 0x00036494
		public void ResumeLayout(bool performLayout)
		{
			bool flag = false;
			if (this.layoutSuspendCount > 0)
			{
				if (this.layoutSuspendCount == 1)
				{
					this.layoutSuspendCount += 1;
					try
					{
						this.OnLayoutResuming(performLayout);
					}
					finally
					{
						this.layoutSuspendCount -= 1;
					}
				}
				this.layoutSuspendCount -= 1;
				if (this.layoutSuspendCount == 0 && this.GetState(512) && performLayout)
				{
					this.PerformLayout();
					flag = true;
				}
			}
			if (!flag)
			{
				this.SetState2(64, true);
			}
			if (!performLayout)
			{
				CommonProperties.xClearPreferredSizeCache(this);
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						this.LayoutEngine.InitLayout(controlCollection[i], BoundsSpecified.All);
						CommonProperties.xClearPreferredSizeCache(controlCollection[i]);
					}
				}
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00038380 File Offset: 0x00036580
		internal void SetAcceptDrops(bool accept)
		{
			if (accept != this.GetState(128) && this.IsHandleCreated)
			{
				try
				{
					if (Application.OleRequired() != ApartmentState.STA)
					{
						throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
					}
					if (accept)
					{
						IntSecurity.ClipboardRead.Demand();
						int num = UnsafeNativeMethods.RegisterDragDrop(new HandleRef(this, this.Handle), new DropTarget(this));
						if (num != 0 && num != -2147221247)
						{
							throw new Win32Exception(num);
						}
					}
					else
					{
						int num2 = UnsafeNativeMethods.RevokeDragDrop(new HandleRef(this, this.Handle));
						if (num2 != 0 && num2 != -2147221248)
						{
							throw new Win32Exception(num2);
						}
					}
					this.SetState(128, accept);
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException(SR.GetString("DragDropRegFailed"), innerException);
				}
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0003844C File Offset: 0x0003664C
		[Obsolete("This method has been deprecated. Use the Scale(SizeF ratio) method instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Scale(float ratio)
		{
			this.ScaleCore(ratio, ratio);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00038458 File Offset: 0x00036658
		[Obsolete("This method has been deprecated. Use the Scale(SizeF ratio) method instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Scale(float dx, float dy)
		{
			this.SuspendLayout();
			try
			{
				this.ScaleCore(dx, dy);
			}
			finally
			{
				this.ResumeLayout();
			}
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0003848C File Offset: 0x0003668C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Scale(SizeF factor)
		{
			using (new LayoutTransaction(this, this, PropertyNames.Bounds, false))
			{
				this.ScaleControl(factor, factor, this);
				if (this.ScaleChildren)
				{
					Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection != null)
					{
						for (int i = 0; i < controlCollection.Count; i++)
						{
							Control control = controlCollection[i];
							control.Scale(factor);
						}
					}
				}
			}
			LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0003851C File Offset: 0x0003671C
		internal virtual void Scale(SizeF includedFactor, SizeF excludedFactor, Control requestingControl)
		{
			using (new LayoutTransaction(this, this, PropertyNames.Bounds, false))
			{
				this.ScaleControl(includedFactor, excludedFactor, requestingControl);
				this.ScaleChildControls(includedFactor, excludedFactor, requestingControl, false);
			}
			LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00038574 File Offset: 0x00036774
		internal void ScaleChildControls(SizeF includedFactor, SizeF excludedFactor, Control requestingControl, bool updateWindowFontIfNeeded = false)
		{
			if (this.ScaleChildren)
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						Control control = controlCollection[i];
						if (updateWindowFontIfNeeded)
						{
							control.UpdateWindowFontIfNeeded();
						}
						control.Scale(includedFactor, excludedFactor, requestingControl);
					}
				}
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x000385CE File Offset: 0x000367CE
		internal void UpdateWindowFontIfNeeded()
		{
			if (DpiHelper.EnableDpiChangedHighDpiImprovements && !this.GetStyle(ControlStyles.UserPaint) && this.Properties.GetObject(Control.PropFont) == null)
			{
				this.SetWindowFont();
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000385F8 File Offset: 0x000367F8
		internal void ScaleControl(SizeF includedFactor, SizeF excludedFactor, Control requestingControl)
		{
			try
			{
				this.IsCurrentlyBeingScaled = true;
				BoundsSpecified boundsSpecified = BoundsSpecified.None;
				BoundsSpecified boundsSpecified2 = BoundsSpecified.None;
				if (!includedFactor.IsEmpty)
				{
					boundsSpecified = this.RequiredScaling;
				}
				if (!excludedFactor.IsEmpty)
				{
					boundsSpecified2 |= (~this.RequiredScaling & BoundsSpecified.All);
				}
				if (boundsSpecified != BoundsSpecified.None)
				{
					this.ScaleControl(includedFactor, boundsSpecified);
				}
				if (boundsSpecified2 != BoundsSpecified.None)
				{
					this.ScaleControl(excludedFactor, boundsSpecified2);
				}
				if (!includedFactor.IsEmpty)
				{
					this.RequiredScaling = BoundsSpecified.None;
				}
			}
			finally
			{
				this.IsCurrentlyBeingScaled = false;
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00038678 File Offset: 0x00036878
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			CreateParams createParams = this.CreateParams;
			NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, 0, 0);
			this.AdjustWindowRectEx(ref rect, createParams.Style, this.HasMenu, createParams.ExStyle);
			Size size = this.MinimumSize;
			Size size2 = this.MaximumSize;
			this.MinimumSize = Size.Empty;
			this.MaximumSize = Size.Empty;
			Rectangle scaledBounds = this.GetScaledBounds(this.Bounds, factor, specified);
			float num = factor.Width;
			float num2 = factor.Height;
			Padding padding = this.Padding;
			Padding margin = this.Margin;
			if (num == 1f)
			{
				specified &= ~(BoundsSpecified.X | BoundsSpecified.Width);
			}
			if (num2 == 1f)
			{
				specified &= ~(BoundsSpecified.Y | BoundsSpecified.Height);
			}
			if (num != 1f)
			{
				padding.Left = (int)Math.Round((double)((float)padding.Left * num));
				padding.Right = (int)Math.Round((double)((float)padding.Right * num));
				margin.Left = (int)Math.Round((double)((float)margin.Left * num));
				margin.Right = (int)Math.Round((double)((float)margin.Right * num));
			}
			if (num2 != 1f)
			{
				padding.Top = (int)Math.Round((double)((float)padding.Top * num2));
				padding.Bottom = (int)Math.Round((double)((float)padding.Bottom * num2));
				margin.Top = (int)Math.Round((double)((float)margin.Top * num2));
				margin.Bottom = (int)Math.Round((double)((float)margin.Bottom * num2));
			}
			this.Padding = padding;
			this.Margin = margin;
			Size size3 = rect.Size;
			if (!size.IsEmpty)
			{
				size -= size3;
				size = this.ScaleSize(LayoutUtils.UnionSizes(Size.Empty, size), factor.Width, factor.Height) + size3;
			}
			if (!size2.IsEmpty)
			{
				size2 -= size3;
				size2 = this.ScaleSize(LayoutUtils.UnionSizes(Size.Empty, size2), factor.Width, factor.Height) + size3;
			}
			Size b = LayoutUtils.ConvertZeroToUnbounded(size2);
			Size a = LayoutUtils.IntersectSizes(scaledBounds.Size, b);
			a = LayoutUtils.UnionSizes(a, size);
			if (DpiHelper.EnableAnchorLayoutHighDpiImprovements && this.ParentInternal != null && this.ParentInternal.LayoutEngine == DefaultLayout.Instance)
			{
				DefaultLayout.ScaleAnchorInfo(this, factor);
			}
			this.SetBoundsCore(scaledBounds.X, scaledBounds.Y, a.Width, a.Height, BoundsSpecified.All);
			this.MaximumSize = size2;
			this.MinimumSize = size;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00038908 File Offset: 0x00036B08
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void ScaleCore(float dx, float dy)
		{
			this.SuspendLayout();
			try
			{
				int num = (int)Math.Round((double)((float)this.x * dx));
				int num2 = (int)Math.Round((double)((float)this.y * dy));
				int num3 = this.width;
				if ((this.controlStyle & ControlStyles.FixedWidth) != ControlStyles.FixedWidth)
				{
					num3 = (int)Math.Round((double)((float)(this.x + this.width) * dx)) - num;
				}
				int num4 = this.height;
				if ((this.controlStyle & ControlStyles.FixedHeight) != ControlStyles.FixedHeight)
				{
					num4 = (int)Math.Round((double)((float)(this.y + this.height) * dy)) - num2;
				}
				this.SetBounds(num, num2, num3, num4, BoundsSpecified.All);
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						controlCollection[i].Scale(dx, dy);
					}
				}
			}
			finally
			{
				this.ResumeLayout();
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00038A00 File Offset: 0x00036C00
		internal Size ScaleSize(Size startSize, float x, float y)
		{
			Size result = startSize;
			if (!this.GetStyle(ControlStyles.FixedWidth))
			{
				result.Width = (int)Math.Round((double)((float)result.Width * x));
			}
			if (!this.GetStyle(ControlStyles.FixedHeight))
			{
				result.Height = (int)Math.Round((double)((float)result.Height * y));
			}
			return result;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00038A54 File Offset: 0x00036C54
		public void Select()
		{
			this.Select(false, false);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00038A60 File Offset: 0x00036C60
		protected virtual void Select(bool directed, bool forward)
		{
			IContainerControl containerControlInternal = this.GetContainerControlInternal();
			if (containerControlInternal != null)
			{
				containerControlInternal.ActiveControl = this;
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00038A80 File Offset: 0x00036C80
		public bool SelectNextControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			Control nextSelectableControl = this.GetNextSelectableControl(ctl, forward, tabStopOnly, nested, wrap);
			if (nextSelectableControl != null)
			{
				nextSelectableControl.Select(true, forward);
				return true;
			}
			return false;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00038AAC File Offset: 0x00036CAC
		private Control GetNextSelectableControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			if (!this.Contains(ctl) || (!nested && ctl.parent != this))
			{
				ctl = null;
			}
			bool flag = false;
			Control control = ctl;
			for (;;)
			{
				ctl = this.GetNextControl(ctl, forward);
				if (ctl == null)
				{
					if (!wrap)
					{
						goto IL_71;
					}
					if (flag)
					{
						break;
					}
					flag = true;
				}
				else if (ctl.CanSelect && (!tabStopOnly || ctl.TabStop) && (nested || ctl.parent == this) && (!AccessibilityImprovements.Level3 || !(ctl.parent is ToolStrip)))
				{
					return ctl;
				}
				if (ctl == control)
				{
					goto IL_71;
				}
			}
			return null;
			IL_71:
			return null;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00038B2B File Offset: 0x00036D2B
		internal bool SelectNextControlInternal(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			return this.SelectNextControl(ctl, forward, tabStopOnly, nested, wrap);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00038B3C File Offset: 0x00036D3C
		private void SelectNextIfFocused()
		{
			if (this.ContainsFocus && this.ParentInternal != null)
			{
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					((Control)containerControlInternal).SelectNextControlInternal(this, true, true, true, true);
				}
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00038B79 File Offset: 0x00036D79
		internal IntPtr SendMessage(int msg, int wparam, int lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00038B8F File Offset: 0x00036D8F
		internal IntPtr SendMessage(int msg, ref int wparam, ref int lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, ref wparam, ref lparam);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00038BA5 File Offset: 0x00036DA5
		internal IntPtr SendMessage(int msg, int wparam, IntPtr lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, (IntPtr)wparam, lparam);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00038BC0 File Offset: 0x00036DC0
		internal IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00038BD6 File Offset: 0x00036DD6
		internal IntPtr SendMessage(int msg, IntPtr wparam, int lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, (IntPtr)lparam);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00038BF1 File Offset: 0x00036DF1
		internal IntPtr SendMessage(int msg, int wparam, ref NativeMethods.RECT lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, ref lparam);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00038C07 File Offset: 0x00036E07
		internal IntPtr SendMessage(int msg, bool wparam, int lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00038C1D File Offset: 0x00036E1D
		internal IntPtr SendMessage(int msg, int wparam, string lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00038C34 File Offset: 0x00036E34
		public void SendToBack()
		{
			if (this.parent != null)
			{
				this.parent.Controls.SetChildIndex(this, -1);
				return;
			}
			if (this.IsHandleCreated && this.GetTopLevel())
			{
				SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.HWND_BOTTOM, 0, 0, 0, 0, 3);
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00038C90 File Offset: 0x00036E90
		public void SetBounds(int x, int y, int width, int height)
		{
			if (this.x != x || this.y != y || this.width != width || this.height != height)
			{
				this.SetBoundsCore(x, y, width, height, BoundsSpecified.All);
				LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
				return;
			}
			this.InitScaling(BoundsSpecified.All);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00038CEC File Offset: 0x00036EEC
		public void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.X) == BoundsSpecified.None)
			{
				x = this.x;
			}
			if ((specified & BoundsSpecified.Y) == BoundsSpecified.None)
			{
				y = this.y;
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.None)
			{
				width = this.width;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.None)
			{
				height = this.height;
			}
			if (this.x != x || this.y != y || this.width != width || this.height != height)
			{
				this.SetBoundsCore(x, y, width, height, specified);
				LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
				return;
			}
			this.InitScaling(specified);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00038D80 File Offset: 0x00036F80
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.SuspendLayout();
			}
			try
			{
				if (this.x != x || this.y != y || this.width != width || this.height != height)
				{
					CommonProperties.UpdateSpecifiedBounds(this, x, y, width, height, specified);
					Rectangle rectangle = this.ApplyBoundsConstraints(x, y, width, height);
					width = rectangle.Width;
					height = rectangle.Height;
					x = rectangle.X;
					y = rectangle.Y;
					if (!this.IsHandleCreated)
					{
						this.UpdateBounds(x, y, width, height);
					}
					else if (!this.GetState(65536))
					{
						int num = 20;
						if (this.x == x && this.y == y)
						{
							num |= 2;
						}
						if (this.width == width && this.height == height)
						{
							num |= 1;
						}
						this.OnBoundsUpdate(x, y, width, height);
						SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.NullHandleRef, x, y, width, height, num);
					}
				}
			}
			finally
			{
				this.InitScaling(specified);
				if (this.ParentInternal != null)
				{
					CommonProperties.xClearPreferredSizeCache(this.ParentInternal);
					this.ParentInternal.LayoutEngine.InitLayout(this, specified);
					this.ParentInternal.ResumeLayout(true);
				}
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00038ED8 File Offset: 0x000370D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void SetClientSizeCore(int x, int y)
		{
			this.Size = this.SizeFromClientSize(x, y);
			this.clientWidth = x;
			this.clientHeight = y;
			this.OnClientSizeChanged(EventArgs.Empty);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00038F01 File Offset: 0x00037101
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual Size SizeFromClientSize(Size clientSize)
		{
			return this.SizeFromClientSize(clientSize.Width, clientSize.Height);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00038F18 File Offset: 0x00037118
		internal Size SizeFromClientSize(int width, int height)
		{
			NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, width, height);
			CreateParams createParams = this.CreateParams;
			this.AdjustWindowRectEx(ref rect, createParams.Style, this.HasMenu, createParams.ExStyle);
			return rect.Size;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00038F58 File Offset: 0x00037158
		private void SetHandle(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				this.SetState(1, false);
			}
			this.UpdateRoot();
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00038F78 File Offset: 0x00037178
		private void SetParentHandle(IntPtr value)
		{
			if (this.IsHandleCreated)
			{
				IntPtr value2 = UnsafeNativeMethods.GetParent(new HandleRef(this.window, this.Handle));
				bool topLevel = this.GetTopLevel();
				if (value2 != value || (value2 == IntPtr.Zero && !topLevel))
				{
					bool flag = (value2 == IntPtr.Zero && !topLevel) || (value == IntPtr.Zero && topLevel);
					if (flag)
					{
						Form form = this as Form;
						if (form != null && !form.CanRecreateHandle())
						{
							flag = false;
							this.UpdateStyles();
						}
					}
					if (flag)
					{
						this.RecreateHandle();
					}
					if (!this.GetTopLevel())
					{
						if (value == IntPtr.Zero)
						{
							Application.ParkHandle(new HandleRef(this.window, this.Handle), this.DpiAwarenessContext);
							this.UpdateRoot();
							return;
						}
						UnsafeNativeMethods.SetParent(new HandleRef(this.window, this.Handle), new HandleRef(null, value));
						if (this.parent != null)
						{
							this.parent.UpdateChildZOrder(this);
						}
						Application.UnparkHandle(new HandleRef(this.window, this.Handle), this.window.DpiAwarenessContext);
						return;
					}
				}
				else if (value == IntPtr.Zero && value2 == IntPtr.Zero && topLevel)
				{
					UnsafeNativeMethods.SetParent(new HandleRef(this.window, this.Handle), new HandleRef(null, IntPtr.Zero));
					Application.UnparkHandle(new HandleRef(this.window, this.Handle), this.window.DpiAwarenessContext);
				}
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00039105 File Offset: 0x00037305
		internal void SetState(int flag, bool value)
		{
			this.state = (value ? (this.state | flag) : (this.state & ~flag));
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00039123 File Offset: 0x00037323
		internal void SetState2(int flag, bool value)
		{
			this.state2 = (value ? (this.state2 | flag) : (this.state2 & ~flag));
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00039141 File Offset: 0x00037341
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void SetStyle(ControlStyles flag, bool value)
		{
			if ((flag & ControlStyles.EnableNotifyMessage) > (ControlStyles)0 && value)
			{
				IntSecurity.UnmanagedCode.Demand();
			}
			this.controlStyle = (value ? (this.controlStyle | flag) : (this.controlStyle & ~flag));
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00039178 File Offset: 0x00037378
		internal static IntPtr SetUpPalette(IntPtr dc, bool force, bool realizePalette)
		{
			IntPtr halftonePalette = Graphics.GetHalftonePalette();
			IntPtr intPtr = SafeNativeMethods.SelectPalette(new HandleRef(null, dc), new HandleRef(null, halftonePalette), force ? 0 : 1);
			if (intPtr != IntPtr.Zero && realizePalette)
			{
				SafeNativeMethods.RealizePalette(new HandleRef(null, dc));
			}
			return intPtr;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000391C4 File Offset: 0x000373C4
		protected void SetTopLevel(bool value)
		{
			if (value && this.IsActiveX)
			{
				throw new InvalidOperationException(SR.GetString("TopLevelNotAllowedIfActiveX"));
			}
			if (value)
			{
				if (this is Form)
				{
					IntSecurity.TopLevelWindow.Demand();
				}
				else
				{
					IntSecurity.UnrestrictedWindows.Demand();
				}
			}
			this.SetTopLevelInternal(value);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00039214 File Offset: 0x00037414
		internal void SetTopLevelInternal(bool value)
		{
			if (this.GetTopLevel() != value)
			{
				if (this.parent != null)
				{
					throw new ArgumentException(SR.GetString("TopLevelParentedControl"), "value");
				}
				this.SetState(524288, value);
				if (this.IsHandleCreated && this.GetState2(8))
				{
					this.ListenToUserPreferenceChanged(value);
				}
				this.UpdateStyles();
				this.SetParentHandle(IntPtr.Zero);
				if (value && this.Visible)
				{
					this.CreateControl();
				}
				this.UpdateRoot();
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00039294 File Offset: 0x00037494
		protected virtual void SetVisibleCore(bool value)
		{
			try
			{
				System.Internal.HandleCollector.SuspendCollect();
				if (this.GetVisibleCore() != value)
				{
					if (!value)
					{
						this.SelectNextIfFocused();
					}
					bool flag = false;
					if (this.GetTopLevel())
					{
						if (this.IsHandleCreated || value)
						{
							SafeNativeMethods.ShowWindow(new HandleRef(this, this.Handle), value ? this.ShowParams : 0);
						}
					}
					else if (this.IsHandleCreated || (value && this.parent != null && this.parent.Created))
					{
						this.SetState(2, value);
						flag = true;
						try
						{
							if (value)
							{
								this.CreateControl();
							}
							SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.NullHandleRef, 0, 0, 0, 0, 23 | (value ? 64 : 128));
						}
						catch
						{
							this.SetState(2, !value);
							throw;
						}
					}
					if (this.GetVisibleCore() != value)
					{
						this.SetState(2, value);
						flag = true;
					}
					if (flag)
					{
						using (new LayoutTransaction(this.parent, this, PropertyNames.Visible))
						{
							this.OnVisibleChanged(EventArgs.Empty);
						}
					}
					this.UpdateRoot();
				}
				else if (this.GetState(2) || value || !this.IsHandleCreated || SafeNativeMethods.IsWindowVisible(new HandleRef(this, this.Handle)))
				{
					this.SetState(2, value);
					if (this.IsHandleCreated)
					{
						SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.NullHandleRef, 0, 0, 0, 0, 23 | (value ? 64 : 128));
					}
				}
			}
			finally
			{
				System.Internal.HandleCollector.ResumeCollect();
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00039468 File Offset: 0x00037668
		internal static AutoValidate GetAutoValidateForControl(Control control)
		{
			ContainerControl parentContainerControl = control.ParentContainerControl;
			if (parentContainerControl == null)
			{
				return AutoValidate.EnablePreventFocusChange;
			}
			return parentContainerControl.AutoValidate;
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00039487 File Offset: 0x00037687
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal bool ShouldAutoValidate
		{
			get
			{
				return Control.GetAutoValidateForControl(this) > AutoValidate.Disable;
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00039492 File Offset: 0x00037692
		internal virtual bool ShouldPerformContainerValidation()
		{
			return this.GetStyle(ControlStyles.ContainerControl);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0003949C File Offset: 0x0003769C
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeBackColor()
		{
			return !this.Properties.GetColor(Control.PropBackColor).IsEmpty;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000394C4 File Offset: 0x000376C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeCursor()
		{
			bool flag;
			object @object = this.Properties.GetObject(Control.PropCursor, out flag);
			return flag && @object != null;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000394ED File Offset: 0x000376ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeEnabled()
		{
			return !this.GetState(4);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000394FC File Offset: 0x000376FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeForeColor()
		{
			return !this.Properties.GetColor(Control.PropForeColor).IsEmpty;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00039524 File Offset: 0x00037724
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeFont()
		{
			bool flag;
			object @object = this.Properties.GetObject(Control.PropFont, out flag);
			return flag && @object != null;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00039550 File Offset: 0x00037750
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeRightToLeft()
		{
			bool flag;
			int integer = this.Properties.GetInteger(Control.PropRightToLeft, out flag);
			return flag && integer != 2;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0003957C File Offset: 0x0003777C
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeVisible()
		{
			return !this.GetState(2);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00039588 File Offset: 0x00037788
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected HorizontalAlignment RtlTranslateAlignment(HorizontalAlignment align)
		{
			return this.RtlTranslateHorizontal(align);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00039591 File Offset: 0x00037791
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected LeftRightAlignment RtlTranslateAlignment(LeftRightAlignment align)
		{
			return this.RtlTranslateLeftRight(align);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0003959A File Offset: 0x0003779A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected ContentAlignment RtlTranslateAlignment(ContentAlignment align)
		{
			return this.RtlTranslateContent(align);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000395A3 File Offset: 0x000377A3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected HorizontalAlignment RtlTranslateHorizontal(HorizontalAlignment align)
		{
			if (RightToLeft.Yes == this.RightToLeft)
			{
				if (align == HorizontalAlignment.Left)
				{
					return HorizontalAlignment.Right;
				}
				if (HorizontalAlignment.Right == align)
				{
					return HorizontalAlignment.Left;
				}
			}
			return align;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x000395A3 File Offset: 0x000377A3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected LeftRightAlignment RtlTranslateLeftRight(LeftRightAlignment align)
		{
			if (RightToLeft.Yes == this.RightToLeft)
			{
				if (align == LeftRightAlignment.Left)
				{
					return LeftRightAlignment.Right;
				}
				if (LeftRightAlignment.Right == align)
				{
					return LeftRightAlignment.Left;
				}
			}
			return align;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000395BC File Offset: 0x000377BC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal ContentAlignment RtlTranslateContent(ContentAlignment align)
		{
			if (RightToLeft.Yes == this.RightToLeft)
			{
				if ((align & WindowsFormsUtils.AnyTopAlign) != (ContentAlignment)0)
				{
					if (align == ContentAlignment.TopLeft)
					{
						return ContentAlignment.TopRight;
					}
					if (align == ContentAlignment.TopRight)
					{
						return ContentAlignment.TopLeft;
					}
				}
				if ((align & WindowsFormsUtils.AnyMiddleAlign) != (ContentAlignment)0)
				{
					if (align == ContentAlignment.MiddleLeft)
					{
						return ContentAlignment.MiddleRight;
					}
					if (align == ContentAlignment.MiddleRight)
					{
						return ContentAlignment.MiddleLeft;
					}
				}
				if ((align & WindowsFormsUtils.AnyBottomAlign) != (ContentAlignment)0)
				{
					if (align == ContentAlignment.BottomLeft)
					{
						return ContentAlignment.BottomRight;
					}
					if (align == ContentAlignment.BottomRight)
					{
						return ContentAlignment.BottomLeft;
					}
				}
			}
			return align;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0003962C File Offset: 0x0003782C
		private void SetWindowFont()
		{
			this.SendMessage(48, this.FontHandle, 0);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00039640 File Offset: 0x00037840
		private void SetWindowStyle(int flag, bool value)
		{
			int num = (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -16));
			UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -16, new HandleRef(null, (IntPtr)(value ? (num | flag) : (num & ~flag))));
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00038282 File Offset: 0x00036482
		public void Show()
		{
			this.Visible = true;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00039694 File Offset: 0x00037894
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal bool ShouldSerializeMargin()
		{
			return !this.Margin.Equals(this.DefaultMargin);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x000396C3 File Offset: 0x000378C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeMaximumSize()
		{
			return this.MaximumSize != this.DefaultMaximumSize;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x000396D6 File Offset: 0x000378D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeMinimumSize()
		{
			return this.MinimumSize != this.DefaultMinimumSize;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000396EC File Offset: 0x000378EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal bool ShouldSerializePadding()
		{
			return !this.Padding.Equals(this.DefaultPadding);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0003971C File Offset: 0x0003791C
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeSize()
		{
			Size defaultSize = this.DefaultSize;
			return this.width != defaultSize.Width || this.height != defaultSize.Height;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00039753 File Offset: 0x00037953
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeText()
		{
			return this.Text.Length != 0;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00039763 File Offset: 0x00037963
		public void SuspendLayout()
		{
			this.layoutSuspendCount += 1;
			if (this.layoutSuspendCount == 1)
			{
				this.OnLayoutSuspended();
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00039783 File Offset: 0x00037983
		private void UnhookMouseEvent()
		{
			this.SetState(16384, false);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00039791 File Offset: 0x00037991
		public void Update()
		{
			SafeNativeMethods.UpdateWindow(new HandleRef(this.window, this.InternalHandle));
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x000397AC File Offset: 0x000379AC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal void UpdateBounds()
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetClientRect(new HandleRef(this.window, this.InternalHandle), ref rect);
			int right = rect.right;
			int bottom = rect.bottom;
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this.window, this.InternalHandle), ref rect);
			if (!this.GetTopLevel())
			{
				UnsafeNativeMethods.MapWindowPoints(NativeMethods.NullHandleRef, new HandleRef(null, UnsafeNativeMethods.GetParent(new HandleRef(this.window, this.InternalHandle))), ref rect, 2);
			}
			this.UpdateBounds(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, right, bottom);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00039864 File Offset: 0x00037A64
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateBounds(int x, int y, int width, int height)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			rect.left = (rect.right = (rect.top = (rect.bottom = 0)));
			CreateParams createParams = this.CreateParams;
			this.AdjustWindowRectEx(ref rect, createParams.Style, false, createParams.ExStyle);
			int num = width - (rect.right - rect.left);
			int num2 = height - (rect.bottom - rect.top);
			this.UpdateBounds(x, y, width, height, num, num2);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000398F0 File Offset: 0x00037AF0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateBounds(int x, int y, int width, int height, int clientWidth, int clientHeight)
		{
			bool flag = this.x != x || this.y != y;
			bool flag2 = this.Width != width || this.Height != height || this.clientWidth != clientWidth || this.clientHeight != clientHeight;
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.clientWidth = clientWidth;
			this.clientHeight = clientHeight;
			if (flag)
			{
				this.OnLocationChanged(EventArgs.Empty);
			}
			if (flag2)
			{
				this.OnSizeChanged(EventArgs.Empty);
				this.OnClientSizeChanged(EventArgs.Empty);
				CommonProperties.xClearPreferredSizeCache(this);
				LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000399B0 File Offset: 0x00037BB0
		private void UpdateBindings()
		{
			for (int i = 0; i < this.DataBindings.Count; i++)
			{
				BindingContext.UpdateBinding(this.BindingContext, this.DataBindings[i]);
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000399EC File Offset: 0x00037BEC
		private void UpdateChildControlIndex(Control ctl)
		{
			if (!LocalAppContextSwitches.AllowUpdateChildControlIndexForTabControls && base.GetType().IsAssignableFrom(typeof(TabControl)))
			{
				return;
			}
			int num = 0;
			int childIndex = this.Controls.GetChildIndex(ctl);
			IntPtr internalHandle = ctl.InternalHandle;
			while ((internalHandle = UnsafeNativeMethods.GetWindow(new HandleRef(null, internalHandle), 3)) != IntPtr.Zero)
			{
				Control control = Control.FromHandleInternal(internalHandle);
				if (control != null)
				{
					num = this.Controls.GetChildIndex(control, false) + 1;
					break;
				}
			}
			if (num > childIndex)
			{
				num--;
			}
			if (num != childIndex)
			{
				this.Controls.SetChildIndex(ctl, num);
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00039A80 File Offset: 0x00037C80
		private void UpdateReflectParent(bool findNewParent)
		{
			if (!this.Disposing && findNewParent && this.IsHandleCreated)
			{
				IntPtr intPtr = UnsafeNativeMethods.GetParent(new HandleRef(this, this.Handle));
				if (intPtr != IntPtr.Zero)
				{
					this.ReflectParent = Control.FromHandleInternal(intPtr);
					return;
				}
			}
			this.ReflectParent = null;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00039AD5 File Offset: 0x00037CD5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateZOrder()
		{
			if (this.parent != null)
			{
				this.parent.UpdateChildZOrder(this);
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00039AEC File Offset: 0x00037CEC
		private void UpdateChildZOrder(Control ctl)
		{
			if (!this.IsHandleCreated || !ctl.IsHandleCreated || ctl.parent != this)
			{
				return;
			}
			IntPtr intPtr = (IntPtr)NativeMethods.HWND_TOP;
			int num = this.Controls.GetChildIndex(ctl);
			while (--num >= 0)
			{
				Control control = this.Controls[num];
				if (control.IsHandleCreated && control.parent == this)
				{
					intPtr = control.Handle;
					break;
				}
			}
			if (UnsafeNativeMethods.GetWindow(new HandleRef(ctl.window, ctl.Handle), 3) != intPtr)
			{
				this.state |= 256;
				try
				{
					SafeNativeMethods.SetWindowPos(new HandleRef(ctl.window, ctl.Handle), new HandleRef(null, intPtr), 0, 0, 0, 0, 3);
				}
				finally
				{
					this.state &= -257;
				}
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00039BD8 File Offset: 0x00037DD8
		private void UpdateRoot()
		{
			this.window.LockReference(this.GetTopLevel() && this.Visible);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00039BF6 File Offset: 0x00037DF6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateStyles()
		{
			this.UpdateStylesCore();
			this.OnStyleChanged(EventArgs.Empty);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00039C0C File Offset: 0x00037E0C
		internal virtual void UpdateStylesCore()
		{
			if (this.IsHandleCreated)
			{
				CreateParams createParams = this.CreateParams;
				int windowStyle = this.WindowStyle;
				int windowExStyle = this.WindowExStyle;
				if ((this.state & 2) != 0)
				{
					createParams.Style |= 268435456;
				}
				if (windowStyle != createParams.Style)
				{
					this.WindowStyle = createParams.Style;
				}
				if (windowExStyle != createParams.ExStyle)
				{
					this.WindowExStyle = createParams.ExStyle;
					this.SetState(1073741824, (createParams.ExStyle & 4194304) != 0);
				}
				SafeNativeMethods.SetWindowPos(new HandleRef(this, this.Handle), NativeMethods.NullHandleRef, 0, 0, 0, 0, 55);
				this.Invalidate(true);
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00039CBD File Offset: 0x00037EBD
		private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			if (pref.Category == UserPreferenceCategory.Color)
			{
				Control.defaultFont = null;
				this.OnSystemColorsChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnBoundsUpdate(int x, int y, int width, int height)
		{
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00039CD9 File Offset: 0x00037ED9
		internal void WindowAssignHandle(IntPtr handle, bool value)
		{
			this.window.AssignHandle(handle, value);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00039CE8 File Offset: 0x00037EE8
		internal void WindowReleaseHandle()
		{
			this.window.ReleaseHandle();
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00039CF8 File Offset: 0x00037EF8
		private void WmClose(ref Message m)
		{
			if (this.ParentInternal != null)
			{
				IntPtr handle = this.Handle;
				IntPtr intPtr = handle;
				while (handle != IntPtr.Zero)
				{
					intPtr = handle;
					handle = UnsafeNativeMethods.GetParent(new HandleRef(null, handle));
					int num = (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(null, intPtr), -16));
					if ((num & 1073741824) == 0)
					{
						break;
					}
				}
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(null, intPtr), 16, IntPtr.Zero, IntPtr.Zero);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00039D7F File Offset: 0x00037F7F
		private void WmCaptureChanged(ref Message m)
		{
			this.OnMouseCaptureChanged(EventArgs.Empty);
			this.DefWndProc(ref m);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00039D93 File Offset: 0x00037F93
		private void WmCommand(ref Message m)
		{
			if (IntPtr.Zero == m.LParam)
			{
				if (Command.DispatchID(NativeMethods.Util.LOWORD(m.WParam)))
				{
					return;
				}
			}
			else if (Control.ReflectMessageInternal(m.LParam, ref m))
			{
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00039DD0 File Offset: 0x00037FD0
		internal virtual void WmContextMenu(ref Message m)
		{
			this.WmContextMenu(ref m, this);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00039DDC File Offset: 0x00037FDC
		internal void WmContextMenu(ref Message m, Control sourceControl)
		{
			ContextMenu contextMenu = this.Properties.GetObject(Control.PropContextMenu) as ContextMenu;
			ContextMenuStrip contextMenuStrip = (contextMenu != null) ? null : (this.Properties.GetObject(Control.PropContextMenuStrip) as ContextMenuStrip);
			if (contextMenu == null && contextMenuStrip == null)
			{
				this.DefWndProc(ref m);
				return;
			}
			int num = NativeMethods.Util.SignedLOWORD(m.LParam);
			int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
			bool isKeyboardActivated = false;
			Point point;
			if ((int)((long)m.LParam) == -1)
			{
				isKeyboardActivated = true;
				point = new Point(this.Width / 2, this.Height / 2);
			}
			else
			{
				point = this.PointToClientInternal(new Point(num, num2));
			}
			if (!this.ClientRectangle.Contains(point))
			{
				this.DefWndProc(ref m);
				return;
			}
			if (contextMenu != null)
			{
				contextMenu.Show(sourceControl, point);
				return;
			}
			if (contextMenuStrip != null)
			{
				contextMenuStrip.ShowInternal(sourceControl, point, isKeyboardActivated);
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00039EC0 File Offset: 0x000380C0
		private void WmCtlColorControl(ref Message m)
		{
			Control control = Control.FromHandleInternal(m.LParam);
			if (control != null)
			{
				m.Result = control.InitializeDCForWmCtlColor(m.WParam, m.Msg);
				if (m.Result != IntPtr.Zero)
				{
					return;
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00039F0E File Offset: 0x0003810E
		private void WmDisplayChange(ref Message m)
		{
			BufferedGraphicsManager.Current.Invalidate();
			this.DefWndProc(ref m);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00039F21 File Offset: 0x00038121
		private void WmDrawItem(ref Message m)
		{
			if (m.WParam == IntPtr.Zero)
			{
				this.WmDrawItemMenuItem(ref m);
				return;
			}
			this.WmOwnerDraw(ref m);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00039F44 File Offset: 0x00038144
		private void WmDrawItemMenuItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			MenuItem menuItemFromItemData = MenuItem.GetMenuItemFromItemData(drawitemstruct.itemData);
			if (menuItemFromItemData != null)
			{
				menuItemFromItemData.WmDrawItem(ref m);
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00039F80 File Offset: 0x00038180
		private void WmEraseBkgnd(ref Message m)
		{
			if (this.GetStyle(ControlStyles.UserPaint))
			{
				if (!this.GetStyle(ControlStyles.AllPaintingInWmPaint))
				{
					IntPtr wparam = m.WParam;
					if (wparam == IntPtr.Zero)
					{
						m.Result = (IntPtr)0;
						return;
					}
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.GetClientRect(new HandleRef(this, this.Handle), ref rect);
					using (PaintEventArgs paintEventArgs = new PaintEventArgs(wparam, Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom)))
					{
						this.PaintWithErrorHandling(paintEventArgs, 1);
					}
				}
				m.Result = (IntPtr)1;
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0003A040 File Offset: 0x00038240
		private void WmExitMenuLoop(ref Message m)
		{
			bool flag = (int)((long)m.WParam) != 0;
			if (flag)
			{
				ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
				if (contextMenu != null)
				{
					contextMenu.OnCollapse(EventArgs.Empty);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0003A090 File Offset: 0x00038290
		private void WmGetControlName(ref Message m)
		{
			string text;
			if (this.Site != null)
			{
				text = this.Site.Name;
			}
			else
			{
				text = this.Name;
			}
			if (text == null)
			{
				text = "";
			}
			this.MarshalStringToMessage(text, ref m);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0003A0CC File Offset: 0x000382CC
		private void WmGetControlType(ref Message m)
		{
			string assemblyQualifiedName = base.GetType().AssemblyQualifiedName;
			this.MarshalStringToMessage(assemblyQualifiedName, ref m);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0003A0F0 File Offset: 0x000382F0
		private void WmGetObject(ref Message m)
		{
			if (m.Msg == 61 && m.LParam == (IntPtr)(-25) && this.SupportsUiaProviders)
			{
				m.Result = UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, this.Handle), m.WParam, m.LParam, this.UnsafeAccessibilityObject);
				return;
			}
			UnsafeNativeMethods.IAccessibleInternal internalAccessibilityObject = this.GetInternalAccessibilityObject((int)((long)m.LParam));
			if (internalAccessibilityObject != null)
			{
				Guid guid = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
				try
				{
					object obj = internalAccessibilityObject;
					IAccessible accessible = obj as IAccessible;
					if (accessible != null)
					{
						throw new InvalidOperationException(SR.GetString("ControlAccessibileObjectInvalid"));
					}
					if (internalAccessibilityObject == null)
					{
						m.Result = (IntPtr)0;
					}
					else
					{
						IntPtr iunknownForObject = Marshal.GetIUnknownForObject(internalAccessibilityObject);
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							m.Result = UnsafeNativeMethods.LresultFromObject(ref guid, m.WParam, new HandleRef(internalAccessibilityObject, iunknownForObject));
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
							Marshal.Release(iunknownForObject);
						}
					}
					return;
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException(SR.GetString("RichControlLresult"), innerException);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0003A214 File Offset: 0x00038414
		private void WmHelp(ref Message m)
		{
			HelpInfo helpInfo = MessageBox.HelpInfo;
			if (helpInfo != null)
			{
				switch (helpInfo.Option)
				{
				case 1:
					Help.ShowHelp(this, helpInfo.HelpFilePath);
					break;
				case 2:
					Help.ShowHelp(this, helpInfo.HelpFilePath, helpInfo.Keyword);
					break;
				case 3:
					Help.ShowHelp(this, helpInfo.HelpFilePath, helpInfo.Navigator);
					break;
				case 4:
					Help.ShowHelp(this, helpInfo.HelpFilePath, helpInfo.Navigator, helpInfo.Param);
					break;
				}
			}
			NativeMethods.HELPINFO helpinfo = (NativeMethods.HELPINFO)m.GetLParam(typeof(NativeMethods.HELPINFO));
			HelpEventArgs helpEventArgs = new HelpEventArgs(new Point(helpinfo.MousePos.x, helpinfo.MousePos.y));
			this.OnHelpRequested(helpEventArgs);
			if (!helpEventArgs.Handled)
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0003A2E8 File Offset: 0x000384E8
		private void WmInitMenuPopup(ref Message m)
		{
			ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
			if (contextMenu != null && contextMenu.ProcessInitMenuPopup(m.WParam))
			{
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0003A324 File Offset: 0x00038524
		private void WmMeasureItem(ref Message m)
		{
			if (m.WParam == IntPtr.Zero)
			{
				NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
				MenuItem menuItemFromItemData = MenuItem.GetMenuItemFromItemData(measureitemstruct.itemData);
				if (menuItemFromItemData != null)
				{
					menuItemFromItemData.WmMeasureItem(ref m);
					return;
				}
			}
			else
			{
				this.WmOwnerDraw(ref m);
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0003A378 File Offset: 0x00038578
		private void WmMenuChar(ref Message m)
		{
			Menu contextMenu = this.ContextMenu;
			if (contextMenu != null)
			{
				contextMenu.WmMenuChar(ref m);
				m.Result != IntPtr.Zero;
				return;
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0003A3A8 File Offset: 0x000385A8
		private void WmMenuSelect(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.WParam);
			int num2 = NativeMethods.Util.HIWORD(m.WParam);
			IntPtr lparam = m.LParam;
			MenuItem menuItem = null;
			if ((num2 & 8192) == 0)
			{
				if ((num2 & 16) == 0)
				{
					Command commandFromID = Command.GetCommandFromID(num);
					if (commandFromID != null)
					{
						object target = commandFromID.Target;
						if (target != null && target is MenuItem.MenuItemData)
						{
							menuItem = ((MenuItem.MenuItemData)target).baseItem;
						}
					}
				}
				else
				{
					menuItem = this.GetMenuItemFromHandleId(lparam, num);
				}
			}
			if (menuItem != null)
			{
				menuItem.PerformSelect();
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0003A430 File Offset: 0x00038630
		private void WmCreate(ref Message m)
		{
			this.DefWndProc(ref m);
			if (this.parent != null)
			{
				this.parent.UpdateChildZOrder(this);
			}
			this.UpdateBounds();
			this.OnHandleCreated(EventArgs.Empty);
			if (!this.GetStyle(ControlStyles.CacheText))
			{
				this.text = null;
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0003A480 File Offset: 0x00038680
		private void WmDestroy(ref Message m)
		{
			if (!this.RecreatingHandle && !this.Disposing && !this.IsDisposed && this.GetState(16384))
			{
				this.OnMouseLeave(EventArgs.Empty);
				this.UnhookMouseEvent();
			}
			if (this.SupportsUiaProviders)
			{
				this.ReleaseUiaProvider(this.HandleInternal);
			}
			else if (LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy && this.IsInternalAccessibilityObjectCreated)
			{
				this.Properties.SetObject(Control.PropUnsafeAccessibility, null);
			}
			if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && !this.RecreatingHandle)
			{
				Control.ControlAccessibleObject controlAccessibleObject = this.Properties.GetObject(Control.PropAccessibility) as Control.ControlAccessibleObject;
				if (controlAccessibleObject != null)
				{
					controlAccessibleObject.ClearOwnerControlInternal();
				}
				Control.ControlAccessibleObject controlAccessibleObject2 = this.Properties.GetObject(Control.PropNcAccessibility) as Control.ControlAccessibleObject;
				if (controlAccessibleObject2 != null)
				{
					controlAccessibleObject2.ClearOwnerControlInternal();
				}
			}
			this.OnHandleDestroyed(EventArgs.Empty);
			if (!this.Disposing)
			{
				if (!this.RecreatingHandle)
				{
					this.SetState(1, false);
				}
			}
			else
			{
				this.SetState(2, false);
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0003A57A File Offset: 0x0003877A
		private void WmKeyChar(ref Message m)
		{
			if (this.ProcessKeyMessage(ref m))
			{
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0003A58D File Offset: 0x0003878D
		private void WmKillFocus(ref Message m)
		{
			this.WmImeKillFocus();
			this.DefWndProc(ref m);
			this.InvokeLostFocus(this, EventArgs.Empty);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0003A5A8 File Offset: 0x000387A8
		private void WmMouseDown(ref Message m, MouseButtons button, int clicks)
		{
			MouseButtons mouseButtons = Control.MouseButtons;
			this.SetState(134217728, true);
			if (!this.GetStyle(ControlStyles.UserMouse))
			{
				this.DefWndProc(ref m);
				if (this.IsDisposed)
				{
					return;
				}
			}
			else if (button == MouseButtons.Left && this.GetStyle(ControlStyles.Selectable))
			{
				this.FocusInternal();
			}
			if (mouseButtons != Control.MouseButtons)
			{
				return;
			}
			if (!this.GetState2(16))
			{
				this.CaptureInternal = true;
			}
			if (mouseButtons != Control.MouseButtons)
			{
				return;
			}
			if (this.Enabled)
			{
				this.OnMouseDown(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0003A64F File Offset: 0x0003884F
		private void WmMouseEnter(ref Message m)
		{
			this.DefWndProc(ref m);
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
			{
				KeyboardToolTipStateMachine.Instance.NotifyAboutMouseEnter(this);
			}
			this.OnMouseEnter(EventArgs.Empty);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0003A675 File Offset: 0x00038875
		private void WmMouseLeave(ref Message m)
		{
			this.DefWndProc(ref m);
			this.OnMouseLeave(EventArgs.Empty);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0003A68C File Offset: 0x0003888C
		private void WmDpiChangedBeforeParent(ref Message m)
		{
			this.DefWndProc(ref m);
			if (this.IsHandleCreated)
			{
				int num = this.deviceDpi;
				this.deviceDpi = (int)UnsafeNativeMethods.GetDpiForWindow(new HandleRef(this, this.HandleInternal));
				if (num != this.deviceDpi)
				{
					if (DpiHelper.EnableDpiChangedHighDpiImprovements)
					{
						Font font = (Font)this.Properties.GetObject(Control.PropFont);
						if (font != null)
						{
							float num2 = (float)this.deviceDpi / (float)num;
							this.Font = new Font(font.FontFamily, font.Size * num2, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
						}
					}
					this.RescaleConstantsForDpi(num, this.deviceDpi);
				}
			}
			this.OnDpiChangedBeforeParent(EventArgs.Empty);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0003A748 File Offset: 0x00038948
		private void WmDpiChangedAfterParent(ref Message m)
		{
			this.DefWndProc(ref m);
			uint dpiForWindow = UnsafeNativeMethods.GetDpiForWindow(new HandleRef(this, this.HandleInternal));
			this.OnDpiChangedAfterParent(EventArgs.Empty);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0003A779 File Offset: 0x00038979
		private void WmMouseHover(ref Message m)
		{
			this.DefWndProc(ref m);
			this.OnMouseHover(EventArgs.Empty);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0003A78D File Offset: 0x0003898D
		private void WmMouseMove(ref Message m)
		{
			if (!this.GetStyle(ControlStyles.UserMouse))
			{
				this.DefWndProc(ref m);
			}
			this.OnMouseMove(new MouseEventArgs(Control.MouseButtons, 0, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0003A7CC File Offset: 0x000389CC
		private void WmMouseUp(ref Message m, MouseButtons button, int clicks)
		{
			try
			{
				int num = NativeMethods.Util.SignedLOWORD(m.LParam);
				int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
				Point p = new Point(num, num2);
				p = this.PointToScreen(p);
				if (!this.GetStyle(ControlStyles.UserMouse))
				{
					this.DefWndProc(ref m);
				}
				else if (button == MouseButtons.Right)
				{
					this.SendMessage(123, this.Handle, NativeMethods.Util.MAKELPARAM(p.X, p.Y));
				}
				bool flag = false;
				if ((this.controlStyle & ControlStyles.StandardClick) == ControlStyles.StandardClick && this.GetState(134217728) && !this.IsDisposed && UnsafeNativeMethods.WindowFromPoint(p.X, p.Y) == this.Handle)
				{
					flag = true;
				}
				if (flag && !this.ValidationCancelled)
				{
					if (!this.GetState(67108864))
					{
						this.OnClick(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						this.OnMouseClick(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
					}
					else
					{
						this.OnDoubleClick(new MouseEventArgs(button, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						this.OnMouseDoubleClick(new MouseEventArgs(button, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
					}
				}
				this.OnMouseUp(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
			}
			finally
			{
				this.SetState(67108864, false);
				this.SetState(134217728, false);
				this.SetState(268435456, false);
				this.CaptureInternal = false;
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0003A9AC File Offset: 0x00038BAC
		private void WmMouseWheel(ref Message m)
		{
			Point p = new Point(NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
			p = this.PointToClient(p);
			HandledMouseEventArgs handledMouseEventArgs = new HandledMouseEventArgs(MouseButtons.None, 0, p.X, p.Y, NativeMethods.Util.SignedHIWORD(m.WParam));
			this.OnMouseWheel(handledMouseEventArgs);
			m.Result = (IntPtr)(handledMouseEventArgs.Handled ? 0 : 1);
			if (!handledMouseEventArgs.Handled)
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0003AA2C File Offset: 0x00038C2C
		private void WmMove(ref Message m)
		{
			this.DefWndProc(ref m);
			this.UpdateBounds();
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0003AA3C File Offset: 0x00038C3C
		private unsafe void WmNotify(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)((void*)m.LParam);
			if (!Control.ReflectMessageInternal(ptr->hwndFrom, ref m))
			{
				if (ptr->code == -521)
				{
					m.Result = UnsafeNativeMethods.SendMessage(new HandleRef(null, ptr->hwndFrom), 8192 + m.Msg, m.WParam, m.LParam);
					return;
				}
				if (ptr->code == -522)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(null, ptr->hwndFrom), 8192 + m.Msg, m.WParam, m.LParam);
				}
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0003AADE File Offset: 0x00038CDE
		private void WmNotifyFormat(ref Message m)
		{
			if (!Control.ReflectMessageInternal(m.WParam, ref m))
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0003AAF8 File Offset: 0x00038CF8
		private void WmOwnerDraw(ref Message m)
		{
			bool flag = false;
			int num = (int)((long)m.WParam);
			IntPtr intPtr = UnsafeNativeMethods.GetDlgItem(new HandleRef(null, m.HWnd), num);
			if (intPtr == IntPtr.Zero)
			{
				intPtr = (IntPtr)((long)num);
			}
			if (!Control.ReflectMessageInternal(intPtr, ref m))
			{
				IntPtr handleFromID = this.window.GetHandleFromID((short)NativeMethods.Util.LOWORD(m.WParam));
				if (handleFromID != IntPtr.Zero)
				{
					Control control = Control.FromHandleInternal(handleFromID);
					if (control != null)
					{
						m.Result = control.SendMessage(8192 + m.Msg, handleFromID, m.LParam);
						flag = true;
					}
				}
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0003ABA8 File Offset: 0x00038DA8
		private void WmPaint(ref Message m)
		{
			bool flag = this.DoubleBuffered || (this.GetStyle(ControlStyles.AllPaintingInWmPaint) && this.DoubleBufferingEnabled);
			IntPtr handle = IntPtr.Zero;
			NativeMethods.PAINTSTRUCT paintstruct = default(NativeMethods.PAINTSTRUCT);
			bool flag2 = false;
			try
			{
				IntPtr intPtr;
				Rectangle rectangle;
				if (m.WParam == IntPtr.Zero)
				{
					handle = this.Handle;
					intPtr = UnsafeNativeMethods.BeginPaint(new HandleRef(this, handle), ref paintstruct);
					if (intPtr == IntPtr.Zero)
					{
						return;
					}
					flag2 = true;
					rectangle = new Rectangle(paintstruct.rcPaint_left, paintstruct.rcPaint_top, paintstruct.rcPaint_right - paintstruct.rcPaint_left, paintstruct.rcPaint_bottom - paintstruct.rcPaint_top);
				}
				else
				{
					intPtr = m.WParam;
					rectangle = this.ClientRectangle;
				}
				if (!flag || (rectangle.Width > 0 && rectangle.Height > 0))
				{
					IntPtr intPtr2 = IntPtr.Zero;
					BufferedGraphics bufferedGraphics = null;
					PaintEventArgs paintEventArgs = null;
					GraphicsState graphicsState = null;
					try
					{
						if (flag || m.WParam == IntPtr.Zero)
						{
							intPtr2 = Control.SetUpPalette(intPtr, false, false);
						}
						if (flag)
						{
							try
							{
								bufferedGraphics = this.BufferContext.Allocate(intPtr, this.ClientRectangle);
							}
							catch (Exception ex)
							{
								if (ClientUtils.IsCriticalException(ex) && !(ex is OutOfMemoryException))
								{
									throw;
								}
								flag = false;
							}
						}
						if (bufferedGraphics != null)
						{
							bufferedGraphics.Graphics.SetClip(rectangle);
							paintEventArgs = new PaintEventArgs(bufferedGraphics.Graphics, rectangle);
							graphicsState = paintEventArgs.Graphics.Save();
						}
						else
						{
							paintEventArgs = new PaintEventArgs(intPtr, rectangle);
						}
						using (paintEventArgs)
						{
							try
							{
								if ((m.WParam == IntPtr.Zero && this.GetStyle(ControlStyles.AllPaintingInWmPaint)) || flag)
								{
									this.PaintWithErrorHandling(paintEventArgs, 1);
								}
							}
							finally
							{
								if (graphicsState != null)
								{
									paintEventArgs.Graphics.Restore(graphicsState);
								}
								else
								{
									paintEventArgs.ResetGraphics();
								}
							}
							this.PaintWithErrorHandling(paintEventArgs, 2);
							if (bufferedGraphics != null)
							{
								bufferedGraphics.Render();
							}
						}
					}
					finally
					{
						if (intPtr2 != IntPtr.Zero)
						{
							SafeNativeMethods.SelectPalette(new HandleRef(null, intPtr), new HandleRef(null, intPtr2), 0);
						}
						if (bufferedGraphics != null)
						{
							bufferedGraphics.Dispose();
						}
					}
				}
			}
			finally
			{
				if (flag2)
				{
					UnsafeNativeMethods.EndPaint(new HandleRef(this, handle), ref paintstruct);
				}
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0003AE54 File Offset: 0x00039054
		private void WmPrintClient(ref Message m)
		{
			using (PaintEventArgs paintEventArgs = new Control.PrintPaintEventArgs(m, m.WParam, this.ClientRectangle))
			{
				this.OnPrint(paintEventArgs);
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0003AE9C File Offset: 0x0003909C
		private void WmQueryNewPalette(ref Message m)
		{
			IntPtr dc = UnsafeNativeMethods.GetDC(new HandleRef(this, this.Handle));
			try
			{
				Control.SetUpPalette(dc, true, true);
			}
			finally
			{
				UnsafeNativeMethods.ReleaseDC(new HandleRef(this, this.Handle), new HandleRef(null, dc));
			}
			this.Invalidate(true);
			m.Result = (IntPtr)1;
			this.DefWndProc(ref m);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0003AF0C File Offset: 0x0003910C
		private void WmSetCursor(ref Message m)
		{
			if (m.WParam == this.InternalHandle && NativeMethods.Util.LOWORD(m.LParam) == 1)
			{
				Cursor.CurrentInternal = this.Cursor;
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0003AF44 File Offset: 0x00039144
		private unsafe void WmWindowPosChanging(ref Message m)
		{
			if (this.IsActiveX)
			{
				NativeMethods.WINDOWPOS* ptr = (NativeMethods.WINDOWPOS*)((void*)m.LParam);
				bool flag = false;
				if ((ptr->flags & 2) == 0 && (ptr->x != this.Left || ptr->y != this.Top))
				{
					flag = true;
				}
				if ((ptr->flags & 1) == 0 && (ptr->cx != this.Width || ptr->cy != this.Height))
				{
					flag = true;
				}
				if (flag)
				{
					this.ActiveXUpdateBounds(ref ptr->x, ref ptr->y, ref ptr->cx, ref ptr->cy, ptr->flags);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0003AFE8 File Offset: 0x000391E8
		private void WmParentNotify(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.WParam);
			IntPtr intPtr = IntPtr.Zero;
			if (num != 1)
			{
				if (num != 2)
				{
					intPtr = UnsafeNativeMethods.GetDlgItem(new HandleRef(this, this.Handle), NativeMethods.Util.HIWORD(m.WParam));
				}
			}
			else
			{
				intPtr = m.LParam;
			}
			if (intPtr == IntPtr.Zero || !Control.ReflectMessageInternal(intPtr, ref m))
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0003B054 File Offset: 0x00039254
		private void WmSetFocus(ref Message m)
		{
			this.WmImeSetFocus();
			if (!this.HostedInWin32DialogManager)
			{
				IContainerControl containerControlInternal = this.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					ContainerControl containerControl = containerControlInternal as ContainerControl;
					bool flag;
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
			this.DefWndProc(ref m);
			this.InvokeGotFocus(this, EventArgs.Empty);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0003B0D0 File Offset: 0x000392D0
		private void WmShowWindow(ref Message m)
		{
			this.DefWndProc(ref m);
			if ((this.state & 16) == 0)
			{
				bool flag = m.WParam != IntPtr.Zero;
				bool visible = this.Visible;
				if (flag)
				{
					bool value = this.GetState(2);
					this.SetState(2, true);
					bool flag2 = false;
					try
					{
						this.CreateControl();
						flag2 = true;
						goto IL_81;
					}
					finally
					{
						if (!flag2)
						{
							this.SetState(2, value);
						}
					}
				}
				bool flag3 = this.GetTopLevel();
				if (this.ParentInternal != null)
				{
					flag3 = this.ParentInternal.Visible;
				}
				if (flag3)
				{
					this.SetState(2, false);
				}
				IL_81:
				if (!this.GetState(536870912) && visible != flag)
				{
					this.OnVisibleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0003B18C File Offset: 0x0003938C
		private void WmUpdateUIState(ref Message m)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = (this.uiCuesState & 240) != 0;
			bool flag4 = (this.uiCuesState & 15) != 0;
			if (flag3)
			{
				flag = this.ShowKeyboardCues;
			}
			if (flag4)
			{
				flag2 = this.ShowFocusCues;
			}
			this.DefWndProc(ref m);
			int num = NativeMethods.Util.LOWORD(m.WParam);
			if (num == 3)
			{
				return;
			}
			UICues uicues = UICues.None;
			if ((NativeMethods.Util.HIWORD(m.WParam) & 2) != 0)
			{
				bool flag5 = num == 2;
				if (flag5 != flag || !flag3)
				{
					uicues |= UICues.ChangeKeyboard;
					this.uiCuesState &= -241;
					this.uiCuesState |= (flag5 ? 32 : 16);
				}
				if (flag5)
				{
					uicues |= UICues.ShowKeyboard;
				}
			}
			if ((NativeMethods.Util.HIWORD(m.WParam) & 1) != 0)
			{
				bool flag6 = num == 2;
				if (flag6 != flag2 || !flag4)
				{
					uicues |= UICues.ChangeFocus;
					this.uiCuesState &= -16;
					this.uiCuesState |= (flag6 ? 2 : 1);
				}
				if (flag6)
				{
					uicues |= UICues.ShowFocus;
				}
			}
			if ((uicues & UICues.Changed) != UICues.None)
			{
				this.OnChangeUICues(new UICuesEventArgs(uicues));
				this.Invalidate(true);
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0003B2B0 File Offset: 0x000394B0
		private unsafe void WmWindowPosChanged(ref Message m)
		{
			this.DefWndProc(ref m);
			this.UpdateBounds();
			if (this.parent != null && UnsafeNativeMethods.GetParent(new HandleRef(this.window, this.InternalHandle)) == this.parent.InternalHandle && (this.state & 256) == 0)
			{
				NativeMethods.WINDOWPOS* ptr = (NativeMethods.WINDOWPOS*)((void*)m.LParam);
				if ((ptr->flags & 4) == 0)
				{
					this.parent.UpdateChildControlIndex(this);
				}
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0003B32C File Offset: 0x0003952C
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual void WndProc(ref Message m)
		{
			if ((this.controlStyle & ControlStyles.EnableNotifyMessage) == ControlStyles.EnableNotifyMessage)
			{
				this.OnNotifyMessage(m);
			}
			int msg = m.Msg;
			if (msg <= 261)
			{
				if (msg <= 47)
				{
					if (msg <= 20)
					{
						if (msg <= 15)
						{
							switch (msg)
							{
							case 1:
								this.WmCreate(ref m);
								return;
							case 2:
								this.WmDestroy(ref m);
								return;
							case 3:
								this.WmMove(ref m);
								return;
							case 4:
							case 5:
							case 6:
								goto IL_65D;
							case 7:
								this.WmSetFocus(ref m);
								return;
							case 8:
								this.WmKillFocus(ref m);
								return;
							default:
								if (msg != 15)
								{
									goto IL_65D;
								}
								if (this.GetStyle(ControlStyles.UserPaint))
								{
									this.WmPaint(ref m);
									return;
								}
								this.DefWndProc(ref m);
								return;
							}
						}
						else
						{
							if (msg == 16)
							{
								this.WmClose(ref m);
								return;
							}
							if (msg != 20)
							{
								goto IL_65D;
							}
							this.WmEraseBkgnd(ref m);
							return;
						}
					}
					else if (msg <= 25)
					{
						if (msg == 24)
						{
							this.WmShowWindow(ref m);
							return;
						}
						if (msg != 25)
						{
							goto IL_65D;
						}
					}
					else
					{
						if (msg == 32)
						{
							this.WmSetCursor(ref m);
							return;
						}
						switch (msg)
						{
						case 43:
							this.WmDrawItem(ref m);
							return;
						case 44:
							this.WmMeasureItem(ref m);
							return;
						case 45:
						case 46:
						case 47:
							goto IL_426;
						default:
							goto IL_65D;
						}
					}
				}
				else if (msg <= 71)
				{
					if (msg <= 61)
					{
						if (msg == 57)
						{
							goto IL_426;
						}
						if (msg != 61)
						{
							goto IL_65D;
						}
						this.WmGetObject(ref m);
						return;
					}
					else
					{
						if (msg == 70)
						{
							this.WmWindowPosChanging(ref m);
							return;
						}
						if (msg != 71)
						{
							goto IL_65D;
						}
						this.WmWindowPosChanged(ref m);
						return;
					}
				}
				else if (msg <= 123)
				{
					switch (msg)
					{
					case 78:
						this.WmNotify(ref m);
						return;
					case 79:
					case 82:
					case 84:
						goto IL_65D;
					case 80:
						this.WmInputLangChangeRequest(ref m);
						return;
					case 81:
						this.WmInputLangChange(ref m);
						return;
					case 83:
						this.WmHelp(ref m);
						return;
					case 85:
						this.WmNotifyFormat(ref m);
						return;
					default:
						if (msg != 123)
						{
							goto IL_65D;
						}
						this.WmContextMenu(ref m);
						return;
					}
				}
				else
				{
					if (msg == 126)
					{
						this.WmDisplayChange(ref m);
						return;
					}
					if (msg - 256 > 2 && msg - 260 > 1)
					{
						goto IL_65D;
					}
					this.WmKeyChar(ref m);
					return;
				}
			}
			else if (msg <= 646)
			{
				if (msg <= 296)
				{
					if (msg <= 287)
					{
						switch (msg)
						{
						case 269:
							this.WmImeStartComposition(ref m);
							return;
						case 270:
							this.WmImeEndComposition(ref m);
							return;
						case 271:
						case 272:
						case 275:
						case 278:
							goto IL_65D;
						case 273:
							this.WmCommand(ref m);
							return;
						case 274:
							if (((int)((long)m.WParam) & 65520) == 61696 && ToolStripManager.ProcessMenuKey(ref m))
							{
								m.Result = IntPtr.Zero;
								return;
							}
							this.DefWndProc(ref m);
							return;
						case 276:
						case 277:
							goto IL_426;
						case 279:
							this.WmInitMenuPopup(ref m);
							return;
						default:
							if (msg != 287)
							{
								goto IL_65D;
							}
							this.WmMenuSelect(ref m);
							return;
						}
					}
					else
					{
						if (msg == 288)
						{
							this.WmMenuChar(ref m);
							return;
						}
						if (msg != 296)
						{
							goto IL_65D;
						}
						this.WmUpdateUIState(ref m);
						return;
					}
				}
				else if (msg <= 533)
				{
					if (msg - 306 > 6)
					{
						switch (msg)
						{
						case 512:
							this.WmMouseMove(ref m);
							return;
						case 513:
							this.WmMouseDown(ref m, MouseButtons.Left, 1);
							return;
						case 514:
							this.WmMouseUp(ref m, MouseButtons.Left, 1);
							return;
						case 515:
							this.WmMouseDown(ref m, MouseButtons.Left, 2);
							if (this.GetStyle(ControlStyles.StandardDoubleClick))
							{
								this.SetState(67108864, true);
								return;
							}
							return;
						case 516:
							this.WmMouseDown(ref m, MouseButtons.Right, 1);
							return;
						case 517:
							this.WmMouseUp(ref m, MouseButtons.Right, 1);
							return;
						case 518:
							this.WmMouseDown(ref m, MouseButtons.Right, 2);
							if (this.GetStyle(ControlStyles.StandardDoubleClick))
							{
								this.SetState(67108864, true);
								return;
							}
							return;
						case 519:
							this.WmMouseDown(ref m, MouseButtons.Middle, 1);
							return;
						case 520:
							this.WmMouseUp(ref m, MouseButtons.Middle, 1);
							return;
						case 521:
							this.WmMouseDown(ref m, MouseButtons.Middle, 2);
							if (this.GetStyle(ControlStyles.StandardDoubleClick))
							{
								this.SetState(67108864, true);
								return;
							}
							return;
						case 522:
							this.WmMouseWheel(ref m);
							return;
						case 523:
							this.WmMouseDown(ref m, this.GetXButton(NativeMethods.Util.HIWORD(m.WParam)), 1);
							return;
						case 524:
							this.WmMouseUp(ref m, this.GetXButton(NativeMethods.Util.HIWORD(m.WParam)), 1);
							return;
						case 525:
							this.WmMouseDown(ref m, this.GetXButton(NativeMethods.Util.HIWORD(m.WParam)), 2);
							if (this.GetStyle(ControlStyles.StandardDoubleClick))
							{
								this.SetState(67108864, true);
								return;
							}
							return;
						case 526:
						case 527:
						case 529:
						case 531:
						case 532:
							goto IL_65D;
						case 528:
							this.WmParentNotify(ref m);
							return;
						case 530:
							this.WmExitMenuLoop(ref m);
							return;
						case 533:
							this.WmCaptureChanged(ref m);
							return;
						default:
							goto IL_65D;
						}
					}
				}
				else
				{
					if (msg == 642)
					{
						this.WmImeNotify(ref m);
						return;
					}
					if (msg != 646)
					{
						goto IL_65D;
					}
					this.WmImeChar(ref m);
					return;
				}
			}
			else if (msg <= 739)
			{
				if (msg <= 675)
				{
					if (msg == 673)
					{
						this.WmMouseHover(ref m);
						return;
					}
					if (msg != 675)
					{
						goto IL_65D;
					}
					this.WmMouseLeave(ref m);
					return;
				}
				else if (msg != 738)
				{
					if (msg != 739)
					{
						goto IL_65D;
					}
					if (DpiHelper.EnableDpiChangedMessageHandling)
					{
						this.WmDpiChangedAfterParent(ref m);
						m.Result = IntPtr.Zero;
						return;
					}
					return;
				}
				else
				{
					if (DpiHelper.EnableDpiChangedMessageHandling)
					{
						this.WmDpiChangedBeforeParent(ref m);
						m.Result = IntPtr.Zero;
						return;
					}
					return;
				}
			}
			else if (msg <= 792)
			{
				if (msg == 783)
				{
					this.WmQueryNewPalette(ref m);
					return;
				}
				if (msg != 792)
				{
					goto IL_65D;
				}
				if (this.GetStyle(ControlStyles.UserPaint))
				{
					this.WmPrintClient(ref m);
					return;
				}
				this.DefWndProc(ref m);
				return;
			}
			else if (msg != 8217)
			{
				if (msg == 8277)
				{
					m.Result = (IntPtr)((Marshal.SystemDefaultCharSize == 1) ? 1 : 2);
					return;
				}
				if (msg - 8498 > 6)
				{
					goto IL_65D;
				}
			}
			this.WmCtlColorControl(ref m);
			return;
			IL_426:
			if (!Control.ReflectMessageInternal(m.LParam, ref m))
			{
				this.DefWndProc(ref m);
				return;
			}
			return;
			IL_65D:
			if (m.Msg == Control.threadCallbackMessage && m.Msg != 0)
			{
				this.InvokeMarshaledCallbacks();
				return;
			}
			if (m.Msg == Control.WM_GETCONTROLNAME)
			{
				this.WmGetControlName(ref m);
				return;
			}
			if (m.Msg == Control.WM_GETCONTROLTYPE)
			{
				this.WmGetControlType(ref m);
				return;
			}
			if (Control.mouseWheelRoutingNeeded && m.Msg == Control.mouseWheelMessage)
			{
				Keys keys = Keys.None;
				keys |= ((UnsafeNativeMethods.GetKeyState(17) < 0) ? Keys.Back : Keys.None);
				keys |= ((UnsafeNativeMethods.GetKeyState(16) < 0) ? Keys.MButton : Keys.None);
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				if (focus == IntPtr.Zero)
				{
					this.SendMessage(m.Msg, (IntPtr)((int)((long)m.WParam) << 16 | (int)keys), m.LParam);
				}
				else
				{
					IntPtr value = IntPtr.Zero;
					IntPtr desktopWindow = UnsafeNativeMethods.GetDesktopWindow();
					while (value == IntPtr.Zero && focus != IntPtr.Zero && focus != desktopWindow)
					{
						value = UnsafeNativeMethods.SendMessage(new HandleRef(null, focus), 522, (int)((long)m.WParam) << 16 | (int)keys, m.LParam);
						focus = UnsafeNativeMethods.GetParent(new HandleRef(null, focus));
					}
				}
			}
			if (m.Msg == NativeMethods.WM_MOUSEENTER)
			{
				this.WmMouseEnter(ref m);
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0003BADD File Offset: 0x00039CDD
		private void WndProcException(Exception e)
		{
			Application.OnThreadException(e);
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x0003BAE8 File Offset: 0x00039CE8
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection == null)
				{
					return ArrangedElementCollection.Empty;
				}
				return controlCollection;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0003BB15 File Offset: 0x00039D15
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				return this.ParentInternal;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x0003BB1D File Offset: 0x00039D1D
		bool IArrangedElement.ParticipatesInLayout
		{
			get
			{
				return this.GetState(2);
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0003BB26 File Offset: 0x00039D26
		void IArrangedElement.PerformLayout(IArrangedElement affectedElement, string affectedProperty)
		{
			this.PerformLayout(new LayoutEventArgs(affectedElement, affectedProperty));
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0003BB35 File Offset: 0x00039D35
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0003BB40 File Offset: 0x00039D40
		void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			ISite site = this.Site;
			IComponentChangeService componentChangeService = null;
			PropertyDescriptor propertyDescriptor = null;
			PropertyDescriptor propertyDescriptor2 = null;
			bool flag = false;
			bool flag2 = false;
			if (site != null && site.DesignMode)
			{
				componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					propertyDescriptor = TypeDescriptor.GetProperties(this)[PropertyNames.Size];
					propertyDescriptor2 = TypeDescriptor.GetProperties(this)[PropertyNames.Location];
					try
					{
						if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly && (bounds.Width != this.Width || bounds.Height != this.Height))
						{
							if (!(site is INestedSite))
							{
								componentChangeService.OnComponentChanging(this, propertyDescriptor);
							}
							flag = true;
						}
						if (propertyDescriptor2 != null && !propertyDescriptor2.IsReadOnly && (bounds.X != this.x || bounds.Y != this.y))
						{
							if (!(site is INestedSite))
							{
								componentChangeService.OnComponentChanging(this, propertyDescriptor2);
							}
							flag2 = true;
						}
					}
					catch (InvalidOperationException)
					{
					}
				}
			}
			this.SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);
			if (site != null && componentChangeService != null)
			{
				try
				{
					if (flag)
					{
						componentChangeService.OnComponentChanged(this, propertyDescriptor, null, null);
					}
					if (flag2)
					{
						componentChangeService.OnComponentChanged(this, propertyDescriptor2, null, null);
					}
				}
				catch (InvalidOperationException)
				{
				}
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool SupportsUiaProviders
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0003BC94 File Offset: 0x00039E94
		void IDropTarget.OnDragEnter(DragEventArgs drgEvent)
		{
			this.OnDragEnter(drgEvent);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0003BC9D File Offset: 0x00039E9D
		void IDropTarget.OnDragOver(DragEventArgs drgEvent)
		{
			this.OnDragOver(drgEvent);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0003BCA6 File Offset: 0x00039EA6
		void IDropTarget.OnDragLeave(EventArgs e)
		{
			this.OnDragLeave(e);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0003BCAF File Offset: 0x00039EAF
		void IDropTarget.OnDragDrop(DragEventArgs drgEvent)
		{
			this.OnDragDrop(drgEvent);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0003BCB8 File Offset: 0x00039EB8
		void ISupportOleDropSource.OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEventArgs)
		{
			this.OnGiveFeedback(giveFeedbackEventArgs);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0003BCC1 File Offset: 0x00039EC1
		void ISupportOleDropSource.OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEventArgs)
		{
			this.OnQueryContinueDrag(queryContinueDragEventArgs);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0003BCCC File Offset: 0x00039ECC
		int UnsafeNativeMethods.IOleControl.GetControlInfo(NativeMethods.tagCONTROLINFO pCI)
		{
			pCI.cb = Marshal.SizeOf(typeof(NativeMethods.tagCONTROLINFO));
			pCI.hAccel = IntPtr.Zero;
			pCI.cAccel = 0;
			pCI.dwFlags = 0;
			if (this.IsInputKey(Keys.Return))
			{
				pCI.dwFlags |= 1;
			}
			if (this.IsInputKey(Keys.Escape))
			{
				pCI.dwFlags |= 2;
			}
			this.ActiveXInstance.GetControlInfo(pCI);
			return 0;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0003BD48 File Offset: 0x00039F48
		int UnsafeNativeMethods.IOleControl.OnMnemonic(ref NativeMethods.MSG pMsg)
		{
			bool flag = this.ProcessMnemonic((char)((int)pMsg.wParam));
			return 0;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0003BD69 File Offset: 0x00039F69
		int UnsafeNativeMethods.IOleControl.OnAmbientPropertyChange(int dispID)
		{
			this.ActiveXInstance.OnAmbientPropertyChange(dispID);
			return 0;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0003BD78 File Offset: 0x00039F78
		int UnsafeNativeMethods.IOleControl.FreezeEvents(int bFreeze)
		{
			this.ActiveXInstance.EventsFrozen = (bFreeze != 0);
			return 0;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0003BD8A File Offset: 0x00039F8A
		int UnsafeNativeMethods.IOleInPlaceActiveObject.GetWindow(out IntPtr hwnd)
		{
			return ((UnsafeNativeMethods.IOleInPlaceObject)this).GetWindow(out hwnd);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0003BD93 File Offset: 0x00039F93
		void UnsafeNativeMethods.IOleInPlaceActiveObject.ContextSensitiveHelp(int fEnterMode)
		{
			((UnsafeNativeMethods.IOleInPlaceObject)this).ContextSensitiveHelp(fEnterMode);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0003BD9C File Offset: 0x00039F9C
		int UnsafeNativeMethods.IOleInPlaceActiveObject.TranslateAccelerator(ref NativeMethods.MSG lpmsg)
		{
			return this.ActiveXInstance.TranslateAccelerator(ref lpmsg);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0003BDAA File Offset: 0x00039FAA
		void UnsafeNativeMethods.IOleInPlaceActiveObject.OnFrameWindowActivate(bool fActivate)
		{
			this.OnFrameWindowActivate(fActivate);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0003BDB3 File Offset: 0x00039FB3
		void UnsafeNativeMethods.IOleInPlaceActiveObject.OnDocWindowActivate(int fActivate)
		{
			this.ActiveXInstance.OnDocWindowActivate(fActivate);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.IOleInPlaceActiveObject.ResizeBorder(NativeMethods.COMRECT prcBorder, UnsafeNativeMethods.IOleInPlaceUIWindow pUIWindow, bool fFrameWindow)
		{
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.IOleInPlaceActiveObject.EnableModeless(int fEnable)
		{
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0003BDC4 File Offset: 0x00039FC4
		int UnsafeNativeMethods.IOleInPlaceObject.GetWindow(out IntPtr hwnd)
		{
			return this.ActiveXInstance.GetWindow(out hwnd);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0003BDDF File Offset: 0x00039FDF
		void UnsafeNativeMethods.IOleInPlaceObject.ContextSensitiveHelp(int fEnterMode)
		{
			if (fEnterMode != 0)
			{
				this.OnHelpRequested(new HelpEventArgs(Control.MousePosition));
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0003BDF4 File Offset: 0x00039FF4
		void UnsafeNativeMethods.IOleInPlaceObject.InPlaceDeactivate()
		{
			this.ActiveXInstance.InPlaceDeactivate();
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0003BE01 File Offset: 0x0003A001
		int UnsafeNativeMethods.IOleInPlaceObject.UIDeactivate()
		{
			return this.ActiveXInstance.UIDeactivate();
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0003BE0E File Offset: 0x0003A00E
		void UnsafeNativeMethods.IOleInPlaceObject.SetObjectRects(NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect)
		{
			this.ActiveXInstance.SetObjectRects(lprcPosRect, lprcClipRect);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.IOleInPlaceObject.ReactivateAndUndo()
		{
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0003BE1D File Offset: 0x0003A01D
		int UnsafeNativeMethods.IOleObject.SetClientSite(UnsafeNativeMethods.IOleClientSite pClientSite)
		{
			this.ActiveXInstance.SetClientSite(pClientSite);
			return 0;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0003BE2C File Offset: 0x0003A02C
		UnsafeNativeMethods.IOleClientSite UnsafeNativeMethods.IOleObject.GetClientSite()
		{
			return this.ActiveXInstance.GetClientSite();
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IOleObject.SetHostNames(string szContainerApp, string szContainerObj)
		{
			return 0;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0003BE39 File Offset: 0x0003A039
		int UnsafeNativeMethods.IOleObject.Close(int dwSaveOption)
		{
			this.ActiveXInstance.Close(dwSaveOption);
			return 0;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IOleObject.SetMoniker(int dwWhichMoniker, object pmk)
		{
			return -2147467263;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0003BE4F File Offset: 0x0003A04F
		int UnsafeNativeMethods.IOleObject.GetMoniker(int dwAssign, int dwWhichMoniker, out object moniker)
		{
			moniker = null;
			return -2147467263;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IOleObject.InitFromData(IDataObject pDataObject, int fCreation, int dwReserved)
		{
			return -2147467263;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00016313 File Offset: 0x00014513
		int UnsafeNativeMethods.IOleObject.GetClipboardData(int dwReserved, out IDataObject data)
		{
			data = null;
			return -2147467263;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0003BE5C File Offset: 0x0003A05C
		int UnsafeNativeMethods.IOleObject.DoVerb(int iVerb, IntPtr lpmsg, UnsafeNativeMethods.IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, NativeMethods.COMRECT lprcPosRect)
		{
			short num = (short)iVerb;
			iVerb = (int)num;
			try
			{
				this.ActiveXInstance.DoVerb(iVerb, lpmsg, pActiveSite, lindex, hwndParent, lprcPosRect);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
			}
			return 0;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0003BEA8 File Offset: 0x0003A0A8
		int UnsafeNativeMethods.IOleObject.EnumVerbs(out UnsafeNativeMethods.IEnumOLEVERB e)
		{
			return Control.ActiveXImpl.EnumVerbs(out e);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IOleObject.OleUpdate()
		{
			return 0;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IOleObject.IsUpToDate()
		{
			return 0;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0003BEB0 File Offset: 0x0003A0B0
		int UnsafeNativeMethods.IOleObject.GetUserClassID(ref Guid pClsid)
		{
			pClsid = base.GetType().GUID;
			return 0;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0003BEC4 File Offset: 0x0003A0C4
		int UnsafeNativeMethods.IOleObject.GetUserType(int dwFormOfType, out string userType)
		{
			if (dwFormOfType == 1)
			{
				userType = base.GetType().FullName;
			}
			else
			{
				userType = base.GetType().Name;
			}
			return 0;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0003BEE7 File Offset: 0x0003A0E7
		int UnsafeNativeMethods.IOleObject.SetExtent(int dwDrawAspect, NativeMethods.tagSIZEL pSizel)
		{
			this.ActiveXInstance.SetExtent(dwDrawAspect, pSizel);
			return 0;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0003BEF7 File Offset: 0x0003A0F7
		int UnsafeNativeMethods.IOleObject.GetExtent(int dwDrawAspect, NativeMethods.tagSIZEL pSizel)
		{
			this.ActiveXInstance.GetExtent(dwDrawAspect, pSizel);
			return 0;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0003BF07 File Offset: 0x0003A107
		int UnsafeNativeMethods.IOleObject.Advise(IAdviseSink pAdvSink, out int cookie)
		{
			cookie = this.ActiveXInstance.Advise(pAdvSink);
			return 0;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0003BF18 File Offset: 0x0003A118
		int UnsafeNativeMethods.IOleObject.Unadvise(int dwConnection)
		{
			this.ActiveXInstance.Unadvise(dwConnection);
			return 0;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0003BF27 File Offset: 0x0003A127
		int UnsafeNativeMethods.IOleObject.EnumAdvise(out IEnumSTATDATA e)
		{
			e = null;
			return -2147467263;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0003BF34 File Offset: 0x0003A134
		int UnsafeNativeMethods.IOleObject.GetMiscStatus(int dwAspect, out int cookie)
		{
			if ((dwAspect & 1) != 0)
			{
				int num = 131456;
				if (this.GetStyle(ControlStyles.ResizeRedraw))
				{
					num |= 1;
				}
				if (this is IButtonControl)
				{
					num |= 4096;
				}
				cookie = num;
				return 0;
			}
			cookie = 0;
			return -2147221397;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00011A20 File Offset: 0x0000FC20
		int UnsafeNativeMethods.IOleObject.SetColorScheme(NativeMethods.tagLOGPALETTE pLogpal)
		{
			return 0;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0003BD8A File Offset: 0x00039F8A
		int UnsafeNativeMethods.IOleWindow.GetWindow(out IntPtr hwnd)
		{
			return ((UnsafeNativeMethods.IOleInPlaceObject)this).GetWindow(out hwnd);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0003BD93 File Offset: 0x00039F93
		void UnsafeNativeMethods.IOleWindow.ContextSensitiveHelp(int fEnterMode)
		{
			((UnsafeNativeMethods.IOleInPlaceObject)this).ContextSensitiveHelp(fEnterMode);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0003BF79 File Offset: 0x0003A179
		void UnsafeNativeMethods.IPersist.GetClassID(out Guid pClassID)
		{
			pClassID = base.GetType().GUID;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.IPersistPropertyBag.InitNew()
		{
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0003BF79 File Offset: 0x0003A179
		void UnsafeNativeMethods.IPersistPropertyBag.GetClassID(out Guid pClassID)
		{
			pClassID = base.GetType().GUID;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0003BF8C File Offset: 0x0003A18C
		void UnsafeNativeMethods.IPersistPropertyBag.Load(UnsafeNativeMethods.IPropertyBag pPropBag, UnsafeNativeMethods.IErrorLog pErrorLog)
		{
			this.ActiveXInstance.Load(pPropBag, pErrorLog);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0003BF9B File Offset: 0x0003A19B
		void UnsafeNativeMethods.IPersistPropertyBag.Save(UnsafeNativeMethods.IPropertyBag pPropBag, bool fClearDirty, bool fSaveAllProperties)
		{
			this.ActiveXInstance.Save(pPropBag, fClearDirty, fSaveAllProperties);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0003BF79 File Offset: 0x0003A179
		void UnsafeNativeMethods.IPersistStorage.GetClassID(out Guid pClassID)
		{
			pClassID = base.GetType().GUID;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0003BFAB File Offset: 0x0003A1AB
		int UnsafeNativeMethods.IPersistStorage.IsDirty()
		{
			return this.ActiveXInstance.IsDirty();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.IPersistStorage.InitNew(UnsafeNativeMethods.IStorage pstg)
		{
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0003BFB8 File Offset: 0x0003A1B8
		int UnsafeNativeMethods.IPersistStorage.Load(UnsafeNativeMethods.IStorage pstg)
		{
			this.ActiveXInstance.Load(pstg);
			return 0;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0003BFC7 File Offset: 0x0003A1C7
		void UnsafeNativeMethods.IPersistStorage.Save(UnsafeNativeMethods.IStorage pstg, bool fSameAsLoad)
		{
			this.ActiveXInstance.Save(pstg, fSameAsLoad);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.IPersistStorage.SaveCompleted(UnsafeNativeMethods.IStorage pStgNew)
		{
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.IPersistStorage.HandsOffStorage()
		{
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0003BF79 File Offset: 0x0003A179
		void UnsafeNativeMethods.IPersistStreamInit.GetClassID(out Guid pClassID)
		{
			pClassID = base.GetType().GUID;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0003BFAB File Offset: 0x0003A1AB
		int UnsafeNativeMethods.IPersistStreamInit.IsDirty()
		{
			return this.ActiveXInstance.IsDirty();
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0003BFD6 File Offset: 0x0003A1D6
		void UnsafeNativeMethods.IPersistStreamInit.Load(UnsafeNativeMethods.IStream pstm)
		{
			this.ActiveXInstance.Load(pstm);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0003BFE4 File Offset: 0x0003A1E4
		void UnsafeNativeMethods.IPersistStreamInit.Save(UnsafeNativeMethods.IStream pstm, bool fClearDirty)
		{
			this.ActiveXInstance.Save(pstm, fClearDirty);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.IPersistStreamInit.GetSizeMax(long pcbSize)
		{
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.IPersistStreamInit.InitNew()
		{
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0003BFF3 File Offset: 0x0003A1F3
		void UnsafeNativeMethods.IQuickActivate.QuickActivate(UnsafeNativeMethods.tagQACONTAINER pQaContainer, UnsafeNativeMethods.tagQACONTROL pQaControl)
		{
			this.ActiveXInstance.QuickActivate(pQaContainer, pQaControl);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0003C002 File Offset: 0x0003A202
		void UnsafeNativeMethods.IQuickActivate.SetContentExtent(NativeMethods.tagSIZEL pSizel)
		{
			this.ActiveXInstance.SetExtent(1, pSizel);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0003C011 File Offset: 0x0003A211
		void UnsafeNativeMethods.IQuickActivate.GetContentExtent(NativeMethods.tagSIZEL pSizel)
		{
			this.ActiveXInstance.GetExtent(1, pSizel);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0003C020 File Offset: 0x0003A220
		int UnsafeNativeMethods.IViewObject.Draw(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, NativeMethods.COMRECT lprcBounds, NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, int dwContinue)
		{
			try
			{
				this.ActiveXInstance.Draw(dwDrawAspect, lindex, pvAspect, ptd, hdcTargetDev, hdcDraw, lprcBounds, lprcWBounds, pfnContinue, dwContinue);
			}
			catch (ExternalException ex)
			{
				return ex.ErrorCode;
			}
			finally
			{
			}
			return 0;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IViewObject.GetColorSet(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, NativeMethods.tagLOGPALETTE ppColorSet)
		{
			return -2147467263;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IViewObject.Freeze(int dwDrawAspect, int lindex, IntPtr pvAspect, IntPtr pdwFreeze)
		{
			return -2147467263;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IViewObject.Unfreeze(int dwFreeze)
		{
			return -2147467263;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0003C078 File Offset: 0x0003A278
		void UnsafeNativeMethods.IViewObject.SetAdvise(int aspects, int advf, IAdviseSink pAdvSink)
		{
			this.ActiveXInstance.SetAdvise(aspects, advf, pAdvSink);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0003C088 File Offset: 0x0003A288
		void UnsafeNativeMethods.IViewObject.GetAdvise(int[] paspects, int[] padvf, IAdviseSink[] pAdvSink)
		{
			this.ActiveXInstance.GetAdvise(paspects, padvf, pAdvSink);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0003C098 File Offset: 0x0003A298
		void UnsafeNativeMethods.IViewObject2.Draw(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, NativeMethods.COMRECT lprcBounds, NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, int dwContinue)
		{
			this.ActiveXInstance.Draw(dwDrawAspect, lindex, pvAspect, ptd, hdcTargetDev, hdcDraw, lprcBounds, lprcWBounds, pfnContinue, dwContinue);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IViewObject2.GetColorSet(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, NativeMethods.tagLOGPALETTE ppColorSet)
		{
			return -2147467263;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IViewObject2.Freeze(int dwDrawAspect, int lindex, IntPtr pvAspect, IntPtr pdwFreeze)
		{
			return -2147467263;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0003BE48 File Offset: 0x0003A048
		int UnsafeNativeMethods.IViewObject2.Unfreeze(int dwFreeze)
		{
			return -2147467263;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0003C078 File Offset: 0x0003A278
		void UnsafeNativeMethods.IViewObject2.SetAdvise(int aspects, int advf, IAdviseSink pAdvSink)
		{
			this.ActiveXInstance.SetAdvise(aspects, advf, pAdvSink);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0003C088 File Offset: 0x0003A288
		void UnsafeNativeMethods.IViewObject2.GetAdvise(int[] paspects, int[] padvf, IAdviseSink[] pAdvSink)
		{
			this.ActiveXInstance.GetAdvise(paspects, padvf, pAdvSink);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0003C0C1 File Offset: 0x0003A2C1
		void UnsafeNativeMethods.IViewObject2.GetExtent(int dwDrawAspect, int lindex, NativeMethods.tagDVTARGETDEVICE ptd, NativeMethods.tagSIZEL lpsizel)
		{
			((UnsafeNativeMethods.IOleObject)this).GetExtent(dwDrawAspect, lpsizel);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0003C0D0 File Offset: 0x0003A2D0
		bool IKeyboardToolTip.CanShowToolTipsNow()
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			return this.IsHandleCreated && this.Visible && (toolStripControlHost == null || toolStripControlHost.CanShowToolTipsNow());
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0003C101 File Offset: 0x0003A301
		Rectangle IKeyboardToolTip.GetNativeScreenRectangle()
		{
			return this.GetToolNativeScreenRectangle();
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0003C10C File Offset: 0x0003A30C
		IList<Rectangle> IKeyboardToolTip.GetNeighboringToolsRectangles()
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			if (toolStripControlHost == null)
			{
				return this.GetOwnNeighboringToolsRectangles();
			}
			return toolStripControlHost.GetNeighboringToolsRectangles();
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0003C130 File Offset: 0x0003A330
		bool IKeyboardToolTip.IsHoveredWithMouse()
		{
			return this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition));
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0003C158 File Offset: 0x0003A358
		bool IKeyboardToolTip.HasRtlModeEnabled()
		{
			Control topLevelControlInternal = this.TopLevelControlInternal;
			return topLevelControlInternal != null && topLevelControlInternal.RightToLeft == RightToLeft.Yes && !this.IsMirrored;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0003C184 File Offset: 0x0003A384
		bool IKeyboardToolTip.AllowsToolTip()
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			return (toolStripControlHost == null || toolStripControlHost.AllowsToolTip()) && this.AllowsKeyboardToolTip();
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00006C59 File Offset: 0x00004E59
		IWin32Window IKeyboardToolTip.GetOwnerWindow()
		{
			return this;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0003C1AB File Offset: 0x0003A3AB
		void IKeyboardToolTip.OnHooked(ToolTip toolTip)
		{
			this.OnKeyboardToolTipHook(toolTip);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0003C1B4 File Offset: 0x0003A3B4
		void IKeyboardToolTip.OnUnhooked(ToolTip toolTip)
		{
			this.OnKeyboardToolTipUnhook(toolTip);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0003C1C0 File Offset: 0x0003A3C0
		string IKeyboardToolTip.GetCaptionForTool(ToolTip toolTip)
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			if (toolStripControlHost == null)
			{
				return toolTip.GetCaptionForTool(this);
			}
			return toolStripControlHost.GetCaptionForTool(toolTip);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0003C1E8 File Offset: 0x0003A3E8
		bool IKeyboardToolTip.ShowsOwnToolTip()
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			return (toolStripControlHost == null || toolStripControlHost.ShowsOwnToolTip()) && this.ShowsOwnKeyboardToolTip();
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0003C20F File Offset: 0x0003A40F
		bool IKeyboardToolTip.IsBeingTabbedTo()
		{
			return Control.AreCommonNavigationalKeysDown();
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0003C216 File Offset: 0x0003A416
		bool IKeyboardToolTip.AllowsChildrenToShowToolTips()
		{
			return this.AllowsChildrenToShowToolTips();
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0003C220 File Offset: 0x0003A420
		private IList<Rectangle> GetOwnNeighboringToolsRectangles()
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				Control[] array = new Control[]
				{
					parentInternal.GetNextSelectableControl(this, true, true, true, false),
					parentInternal.GetNextSelectableControl(this, false, true, true, false),
					parentInternal.GetNextSelectableControl(this, true, false, false, true),
					parentInternal.GetNextSelectableControl(this, false, false, false, true)
				};
				List<Rectangle> list = new List<Rectangle>(4);
				foreach (Control control in array)
				{
					if (control != null && control.IsHandleCreated)
					{
						list.Add(((IKeyboardToolTip)control).GetNativeScreenRectangle());
					}
				}
				return list;
			}
			return new Rectangle[0];
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool ShowsOwnKeyboardToolTip()
		{
			return true;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnKeyboardToolTipHook(ToolTip toolTip)
		{
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnKeyboardToolTipUnhook(ToolTip toolTip)
		{
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0003C2BC File Offset: 0x0003A4BC
		internal virtual Rectangle GetToolNativeScreenRectangle()
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), ref rect);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool AllowsKeyboardToolTip()
		{
			return true;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0003C302 File Offset: 0x0003A502
		private static bool IsKeyDown(Keys key)
		{
			return (Control.tempKeyboardStateArray[(int)key] & 128) > 0;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003C314 File Offset: 0x0003A514
		internal static bool AreCommonNavigationalKeysDown()
		{
			if (Control.tempKeyboardStateArray == null)
			{
				Control.tempKeyboardStateArray = new byte[256];
			}
			UnsafeNativeMethods.GetKeyboardState(Control.tempKeyboardStateArray);
			return Control.IsKeyDown(Keys.Tab) || Control.IsKeyDown(Keys.Up) || Control.IsKeyDown(Keys.Down) || Control.IsKeyDown(Keys.Left) || Control.IsKeyDown(Keys.Right) || Control.IsKeyDown(Keys.Menu) || Control.IsKeyDown(Keys.F10) || Control.IsKeyDown(Keys.Escape);
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0003C38C File Offset: 0x0003A58C
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x0003C3A8 File Offset: 0x0003A5A8
		internal ToolStripControlHost ToolStripControlHost
		{
			get
			{
				ToolStripControlHost result;
				this.toolStripControlHostReference.TryGetTarget(out result);
				return result;
			}
			set
			{
				this.toolStripControlHostReference.SetTarget(value);
			}
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool AllowsChildrenToShowToolTips()
		{
			return true;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0003C3B8 File Offset: 0x0003A5B8
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x0003C3FC File Offset: 0x0003A5FC
		internal ImeMode CachedImeMode
		{
			get
			{
				bool flag;
				ImeMode imeMode = (ImeMode)this.Properties.GetInteger(Control.PropImeMode, out flag);
				if (!flag)
				{
					imeMode = this.DefaultImeMode;
				}
				if (imeMode == ImeMode.Inherit)
				{
					Control parentInternal = this.ParentInternal;
					if (parentInternal != null)
					{
						imeMode = parentInternal.CachedImeMode;
					}
					else
					{
						imeMode = ImeMode.NoControl;
					}
				}
				return imeMode;
			}
			set
			{
				this.Properties.SetInteger(Control.PropImeMode, (int)value);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0003C40F File Offset: 0x0003A60F
		protected virtual bool CanEnableIme
		{
			get
			{
				return this.ImeSupported;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0003C417 File Offset: 0x0003A617
		internal ImeMode CurrentImeContextMode
		{
			get
			{
				if (this.IsHandleCreated)
				{
					return ImeContext.GetImeMode(this.Handle);
				}
				return ImeMode.Inherit;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00015ECF File Offset: 0x000140CF
		protected virtual ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Inherit;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0003C430 File Offset: 0x0003A630
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x0003C451 File Offset: 0x0003A651
		internal int DisableImeModeChangedCount
		{
			get
			{
				bool flag;
				return this.Properties.GetInteger(Control.PropDisableImeModeChangedCount, out flag);
			}
			set
			{
				this.Properties.SetInteger(Control.PropDisableImeModeChangedCount, value);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0003C464 File Offset: 0x0003A664
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x0003C478 File Offset: 0x0003A678
		private static bool IgnoreWmImeNotify
		{
			get
			{
				return Control.ignoreWmImeNotify;
			}
			set
			{
				Control.ignoreWmImeNotify = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0003C480 File Offset: 0x0003A680
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x0003C49C File Offset: 0x0003A69C
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[AmbientValue(ImeMode.Inherit)]
		[SRDescription("ControlIMEModeDescr")]
		public ImeMode ImeMode
		{
			get
			{
				ImeMode imeMode = this.ImeModeBase;
				if (imeMode == ImeMode.OnHalf)
				{
					imeMode = ImeMode.On;
				}
				return imeMode;
			}
			set
			{
				this.ImeModeBase = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0003C4A8 File Offset: 0x0003A6A8
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x0003C4C0 File Offset: 0x0003A6C0
		protected virtual ImeMode ImeModeBase
		{
			get
			{
				return this.CachedImeMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, -1, 12))
				{
					throw new InvalidEnumArgumentException("ImeMode", (int)value, typeof(ImeMode));
				}
				ImeMode cachedImeMode = this.CachedImeMode;
				this.CachedImeMode = value;
				if (cachedImeMode != value)
				{
					Control control = null;
					if (!base.DesignMode && ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
					{
						if (this.Focused)
						{
							control = this;
						}
						else if (this.ContainsFocus)
						{
							control = Control.FromChildHandleInternal(UnsafeNativeMethods.GetFocus());
						}
						if (control != null && control.CanEnableIme)
						{
							int disableImeModeChangedCount = this.DisableImeModeChangedCount;
							this.DisableImeModeChangedCount = disableImeModeChangedCount + 1;
							try
							{
								control.UpdateImeContextMode();
							}
							finally
							{
								disableImeModeChangedCount = this.DisableImeModeChangedCount;
								this.DisableImeModeChangedCount = disableImeModeChangedCount - 1;
							}
						}
					}
					this.VerifyImeModeChanged(cachedImeMode, this.CachedImeMode);
				}
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0003C58C File Offset: 0x0003A78C
		private bool ImeSupported
		{
			get
			{
				return this.DefaultImeMode != ImeMode.Disable;
			}
		}

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x060012E2 RID: 4834 RVA: 0x0003C59A File Offset: 0x0003A79A
		// (remove) Token: 0x060012E3 RID: 4835 RVA: 0x0003C5AD File Offset: 0x0003A7AD
		[WinCategory("Behavior")]
		[SRDescription("ControlOnImeModeChangedDescr")]
		public event EventHandler ImeModeChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventImeModeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventImeModeChanged, value);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0003C5C0 File Offset: 0x0003A7C0
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x0003C5D2 File Offset: 0x0003A7D2
		internal int ImeWmCharsToIgnore
		{
			get
			{
				return this.Properties.GetInteger(Control.PropImeWmCharsToIgnore);
			}
			set
			{
				if (this.ImeWmCharsToIgnore != -1)
				{
					this.Properties.SetInteger(Control.PropImeWmCharsToIgnore, value);
				}
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0003C5F0 File Offset: 0x0003A7F0
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x0003C61D File Offset: 0x0003A81D
		private bool LastCanEnableIme
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(Control.PropLastCanEnableIme, out flag);
				flag = (!flag || integer == 1);
				return flag;
			}
			set
			{
				this.Properties.SetInteger(Control.PropLastCanEnableIme, value ? 1 : 0);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0003C638 File Offset: 0x0003A838
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x0003C69A File Offset: 0x0003A89A
		private protected static ImeMode PropagatingImeMode
		{
			protected get
			{
				if (Control.propagatingImeMode == ImeMode.Inherit)
				{
					ImeMode imeMode = ImeMode.Inherit;
					IntPtr intPtr = UnsafeNativeMethods.GetFocus();
					if (intPtr != IntPtr.Zero)
					{
						imeMode = ImeContext.GetImeMode(intPtr);
						if (imeMode == ImeMode.Disable)
						{
							intPtr = UnsafeNativeMethods.GetAncestor(new HandleRef(null, intPtr), 2);
							if (intPtr != IntPtr.Zero)
							{
								imeMode = ImeContext.GetImeMode(intPtr);
							}
						}
					}
					Control.PropagatingImeMode = imeMode;
				}
				return Control.propagatingImeMode;
			}
			private set
			{
				if (Control.propagatingImeMode != value)
				{
					if (value == ImeMode.NoControl || value == ImeMode.Disable)
					{
						return;
					}
					Control.propagatingImeMode = value;
				}
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0003C6B4 File Offset: 0x0003A8B4
		internal void UpdateImeContextMode()
		{
			ImeMode[] inputLanguageTable = ImeModeConversion.InputLanguageTable;
			if (!base.DesignMode && inputLanguageTable != ImeModeConversion.UnsupportedTable && this.Focused)
			{
				ImeMode imeMode = ImeMode.Disable;
				ImeMode cachedImeMode = this.CachedImeMode;
				if (this.ImeSupported && this.CanEnableIme)
				{
					imeMode = ((cachedImeMode == ImeMode.NoControl) ? Control.PropagatingImeMode : cachedImeMode);
				}
				if (this.CurrentImeContextMode != imeMode && imeMode != ImeMode.Inherit)
				{
					int disableImeModeChangedCount = this.DisableImeModeChangedCount;
					this.DisableImeModeChangedCount = disableImeModeChangedCount + 1;
					ImeMode imeMode2 = Control.PropagatingImeMode;
					try
					{
						ImeContext.SetImeStatus(imeMode, this.Handle);
					}
					finally
					{
						disableImeModeChangedCount = this.DisableImeModeChangedCount;
						this.DisableImeModeChangedCount = disableImeModeChangedCount - 1;
						if (imeMode == ImeMode.Disable && inputLanguageTable == ImeModeConversion.ChineseTable)
						{
							Control.PropagatingImeMode = imeMode2;
						}
					}
					if (cachedImeMode == ImeMode.NoControl)
					{
						if (this.CanEnableIme)
						{
							Control.PropagatingImeMode = this.CurrentImeContextMode;
							return;
						}
					}
					else
					{
						if (this.CanEnableIme)
						{
							this.CachedImeMode = this.CurrentImeContextMode;
						}
						this.VerifyImeModeChanged(imeMode, this.CachedImeMode);
					}
				}
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0003C7B4 File Offset: 0x0003A9B4
		private void VerifyImeModeChanged(ImeMode oldMode, ImeMode newMode)
		{
			if (this.ImeSupported && this.DisableImeModeChangedCount == 0 && newMode != ImeMode.NoControl && oldMode != newMode)
			{
				this.OnImeModeChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0003C7D8 File Offset: 0x0003A9D8
		internal void VerifyImeRestrictedModeChanged()
		{
			bool canEnableIme = this.CanEnableIme;
			if (this.LastCanEnableIme != canEnableIme)
			{
				if (this.Focused)
				{
					int disableImeModeChangedCount = this.DisableImeModeChangedCount;
					this.DisableImeModeChangedCount = disableImeModeChangedCount + 1;
					try
					{
						this.UpdateImeContextMode();
					}
					finally
					{
						disableImeModeChangedCount = this.DisableImeModeChangedCount;
						this.DisableImeModeChangedCount = disableImeModeChangedCount - 1;
					}
				}
				ImeMode imeMode = this.CachedImeMode;
				ImeMode newMode = ImeMode.Disable;
				if (canEnableIme)
				{
					newMode = imeMode;
					imeMode = ImeMode.Disable;
				}
				this.VerifyImeModeChanged(imeMode, newMode);
				this.LastCanEnableIme = canEnableIme;
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0003C858 File Offset: 0x0003AA58
		internal void OnImeContextStatusChanged(IntPtr handle)
		{
			ImeMode imeMode = ImeContext.GetImeMode(handle);
			if (imeMode != ImeMode.Inherit)
			{
				ImeMode cachedImeMode = this.CachedImeMode;
				if (this.CanEnableIme)
				{
					if (cachedImeMode != ImeMode.NoControl)
					{
						this.CachedImeMode = imeMode;
						this.VerifyImeModeChanged(cachedImeMode, this.CachedImeMode);
						return;
					}
					Control.PropagatingImeMode = imeMode;
				}
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0003C8A0 File Offset: 0x0003AAA0
		protected virtual void OnImeModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventImeModeChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0003C8CE File Offset: 0x0003AACE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetImeMode()
		{
			this.ImeMode = this.DefaultImeMode;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0003C8DC File Offset: 0x0003AADC
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeImeMode()
		{
			bool flag;
			int integer = this.Properties.GetInteger(Control.PropImeMode, out flag);
			return flag && integer != (int)this.DefaultImeMode;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0003C910 File Offset: 0x0003AB10
		private void WmInputLangChange(ref Message m)
		{
			this.UpdateImeContextMode();
			if (ImeModeConversion.InputLanguageTable == ImeModeConversion.UnsupportedTable)
			{
				Control.PropagatingImeMode = ImeMode.Off;
			}
			if (LocalAppContextSwitches.EnableLegacyChineseIMEIndicator && ImeModeConversion.InputLanguageTable == ImeModeConversion.ChineseTable)
			{
				Control.IgnoreWmImeNotify = false;
			}
			Form form = this.FindFormInternal();
			if (form != null)
			{
				InputLanguageChangedEventArgs iplevent = InputLanguage.CreateInputLanguageChangedEventArgs(m);
				form.PerformOnInputLanguageChanged(iplevent);
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0003C974 File Offset: 0x0003AB74
		private void WmInputLangChangeRequest(ref Message m)
		{
			InputLanguageChangingEventArgs inputLanguageChangingEventArgs = InputLanguage.CreateInputLanguageChangingEventArgs(m);
			Form form = this.FindFormInternal();
			if (form != null)
			{
				form.PerformOnInputLanguageChanging(inputLanguageChangingEventArgs);
			}
			if (!inputLanguageChangingEventArgs.Cancel)
			{
				this.DefWndProc(ref m);
				return;
			}
			m.Result = IntPtr.Zero;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0003C9B9 File Offset: 0x0003ABB9
		private void WmImeChar(ref Message m)
		{
			if (this.ProcessKeyEventArgs(ref m))
			{
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0003C9CC File Offset: 0x0003ABCC
		private void WmImeEndComposition(ref Message m)
		{
			this.ImeWmCharsToIgnore = -1;
			this.DefWndProc(ref m);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0003C9DC File Offset: 0x0003ABDC
		private void WmImeNotify(ref Message m)
		{
			ImeMode[] inputLanguageTable = ImeModeConversion.InputLanguageTable;
			if (LocalAppContextSwitches.EnableLegacyChineseIMEIndicator && inputLanguageTable == ImeModeConversion.ChineseTable && !Control.lastLanguageChinese)
			{
				Control.IgnoreWmImeNotify = true;
			}
			Control.lastLanguageChinese = (inputLanguageTable == ImeModeConversion.ChineseTable);
			if (this.ImeSupported && inputLanguageTable != ImeModeConversion.UnsupportedTable && !Control.IgnoreWmImeNotify)
			{
				int num = (int)m.WParam;
				if (num == 6 || num == 8)
				{
					this.OnImeContextStatusChanged(this.Handle);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0003CA56 File Offset: 0x0003AC56
		internal void WmImeSetFocus()
		{
			if (ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
			{
				this.UpdateImeContextMode();
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0003CA6A File Offset: 0x0003AC6A
		private void WmImeStartComposition(ref Message m)
		{
			this.Properties.SetInteger(Control.PropImeWmCharsToIgnore, 0);
			this.DefWndProc(ref m);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0003CA84 File Offset: 0x0003AC84
		private void WmImeKillFocus()
		{
			Control topMostParent = this.TopMostParent;
			Form form = topMostParent as Form;
			if ((form == null || form.Modal) && !topMostParent.ContainsFocus && Control.propagatingImeMode != ImeMode.Inherit)
			{
				Control.IgnoreWmImeNotify = true;
				try
				{
					ImeContext.SetImeStatus(Control.PropagatingImeMode, topMostParent.Handle);
					Control.PropagatingImeMode = ImeMode.Inherit;
				}
				finally
				{
					Control.IgnoreWmImeNotify = false;
				}
			}
		}

		// Token: 0x04000824 RID: 2084
		internal static readonly TraceSwitch ControlKeyboardRouting;

		// Token: 0x04000825 RID: 2085
		internal static readonly TraceSwitch PaletteTracing;

		// Token: 0x04000826 RID: 2086
		internal static readonly TraceSwitch FocusTracing;

		// Token: 0x04000827 RID: 2087
		internal static readonly BooleanSwitch BufferPinkRect;

		// Token: 0x04000828 RID: 2088
		private static int WM_GETCONTROLNAME;

		// Token: 0x04000829 RID: 2089
		private static int WM_GETCONTROLTYPE;

		// Token: 0x0400082A RID: 2090
		internal const int STATE_CREATED = 1;

		// Token: 0x0400082B RID: 2091
		internal const int STATE_VISIBLE = 2;

		// Token: 0x0400082C RID: 2092
		internal const int STATE_ENABLED = 4;

		// Token: 0x0400082D RID: 2093
		internal const int STATE_TABSTOP = 8;

		// Token: 0x0400082E RID: 2094
		internal const int STATE_RECREATE = 16;

		// Token: 0x0400082F RID: 2095
		internal const int STATE_MODAL = 32;

		// Token: 0x04000830 RID: 2096
		internal const int STATE_ALLOWDROP = 64;

		// Token: 0x04000831 RID: 2097
		internal const int STATE_DROPTARGET = 128;

		// Token: 0x04000832 RID: 2098
		internal const int STATE_NOZORDER = 256;

		// Token: 0x04000833 RID: 2099
		internal const int STATE_LAYOUTDEFERRED = 512;

		// Token: 0x04000834 RID: 2100
		internal const int STATE_USEWAITCURSOR = 1024;

		// Token: 0x04000835 RID: 2101
		internal const int STATE_DISPOSED = 2048;

		// Token: 0x04000836 RID: 2102
		internal const int STATE_DISPOSING = 4096;

		// Token: 0x04000837 RID: 2103
		internal const int STATE_MOUSEENTERPENDING = 8192;

		// Token: 0x04000838 RID: 2104
		internal const int STATE_TRACKINGMOUSEEVENT = 16384;

		// Token: 0x04000839 RID: 2105
		internal const int STATE_THREADMARSHALLPENDING = 32768;

		// Token: 0x0400083A RID: 2106
		internal const int STATE_SIZELOCKEDBYOS = 65536;

		// Token: 0x0400083B RID: 2107
		internal const int STATE_CAUSESVALIDATION = 131072;

		// Token: 0x0400083C RID: 2108
		internal const int STATE_CREATINGHANDLE = 262144;

		// Token: 0x0400083D RID: 2109
		internal const int STATE_TOPLEVEL = 524288;

		// Token: 0x0400083E RID: 2110
		internal const int STATE_ISACCESSIBLE = 1048576;

		// Token: 0x0400083F RID: 2111
		internal const int STATE_OWNCTLBRUSH = 2097152;

		// Token: 0x04000840 RID: 2112
		internal const int STATE_EXCEPTIONWHILEPAINTING = 4194304;

		// Token: 0x04000841 RID: 2113
		internal const int STATE_LAYOUTISDIRTY = 8388608;

		// Token: 0x04000842 RID: 2114
		internal const int STATE_CHECKEDHOST = 16777216;

		// Token: 0x04000843 RID: 2115
		internal const int STATE_HOSTEDINDIALOG = 33554432;

		// Token: 0x04000844 RID: 2116
		internal const int STATE_DOUBLECLICKFIRED = 67108864;

		// Token: 0x04000845 RID: 2117
		internal const int STATE_MOUSEPRESSED = 134217728;

		// Token: 0x04000846 RID: 2118
		internal const int STATE_VALIDATIONCANCELLED = 268435456;

		// Token: 0x04000847 RID: 2119
		internal const int STATE_PARENTRECREATING = 536870912;

		// Token: 0x04000848 RID: 2120
		internal const int STATE_MIRRORED = 1073741824;

		// Token: 0x04000849 RID: 2121
		private const int STATE2_HAVEINVOKED = 1;

		// Token: 0x0400084A RID: 2122
		private const int STATE2_SETSCROLLPOS = 2;

		// Token: 0x0400084B RID: 2123
		private const int STATE2_LISTENINGTOUSERPREFERENCECHANGED = 4;

		// Token: 0x0400084C RID: 2124
		internal const int STATE2_INTERESTEDINUSERPREFERENCECHANGED = 8;

		// Token: 0x0400084D RID: 2125
		internal const int STATE2_MAINTAINSOWNCAPTUREMODE = 16;

		// Token: 0x0400084E RID: 2126
		private const int STATE2_BECOMINGACTIVECONTROL = 32;

		// Token: 0x0400084F RID: 2127
		private const int STATE2_CLEARLAYOUTARGS = 64;

		// Token: 0x04000850 RID: 2128
		private const int STATE2_INPUTKEY = 128;

		// Token: 0x04000851 RID: 2129
		private const int STATE2_INPUTCHAR = 256;

		// Token: 0x04000852 RID: 2130
		private const int STATE2_UICUES = 512;

		// Token: 0x04000853 RID: 2131
		private const int STATE2_ISACTIVEX = 1024;

		// Token: 0x04000854 RID: 2132
		internal const int STATE2_USEPREFERREDSIZECACHE = 2048;

		// Token: 0x04000855 RID: 2133
		internal const int STATE2_TOPMDIWINDOWCLOSING = 4096;

		// Token: 0x04000856 RID: 2134
		internal const int STATE2_CURRENTLYBEINGSCALED = 8192;

		// Token: 0x04000857 RID: 2135
		private static readonly object EventAutoSizeChanged = new object();

		// Token: 0x04000858 RID: 2136
		private static readonly object EventKeyDown = new object();

		// Token: 0x04000859 RID: 2137
		private static readonly object EventKeyPress = new object();

		// Token: 0x0400085A RID: 2138
		private static readonly object EventKeyUp = new object();

		// Token: 0x0400085B RID: 2139
		private static readonly object EventMouseDown = new object();

		// Token: 0x0400085C RID: 2140
		private static readonly object EventMouseEnter = new object();

		// Token: 0x0400085D RID: 2141
		private static readonly object EventMouseLeave = new object();

		// Token: 0x0400085E RID: 2142
		private static readonly object EventDpiChangedBeforeParent = new object();

		// Token: 0x0400085F RID: 2143
		private static readonly object EventDpiChangedAfterParent = new object();

		// Token: 0x04000860 RID: 2144
		private static readonly object EventMouseHover = new object();

		// Token: 0x04000861 RID: 2145
		private static readonly object EventMouseMove = new object();

		// Token: 0x04000862 RID: 2146
		private static readonly object EventMouseUp = new object();

		// Token: 0x04000863 RID: 2147
		private static readonly object EventMouseWheel = new object();

		// Token: 0x04000864 RID: 2148
		private static readonly object EventClick = new object();

		// Token: 0x04000865 RID: 2149
		private static readonly object EventClientSize = new object();

		// Token: 0x04000866 RID: 2150
		private static readonly object EventDoubleClick = new object();

		// Token: 0x04000867 RID: 2151
		private static readonly object EventMouseClick = new object();

		// Token: 0x04000868 RID: 2152
		private static readonly object EventMouseDoubleClick = new object();

		// Token: 0x04000869 RID: 2153
		private static readonly object EventMouseCaptureChanged = new object();

		// Token: 0x0400086A RID: 2154
		private static readonly object EventMove = new object();

		// Token: 0x0400086B RID: 2155
		private static readonly object EventResize = new object();

		// Token: 0x0400086C RID: 2156
		private static readonly object EventLayout = new object();

		// Token: 0x0400086D RID: 2157
		private static readonly object EventGotFocus = new object();

		// Token: 0x0400086E RID: 2158
		private static readonly object EventLostFocus = new object();

		// Token: 0x0400086F RID: 2159
		private static readonly object EventEnabledChanged = new object();

		// Token: 0x04000870 RID: 2160
		private static readonly object EventEnter = new object();

		// Token: 0x04000871 RID: 2161
		private static readonly object EventLeave = new object();

		// Token: 0x04000872 RID: 2162
		private static readonly object EventHandleCreated = new object();

		// Token: 0x04000873 RID: 2163
		private static readonly object EventHandleDestroyed = new object();

		// Token: 0x04000874 RID: 2164
		private static readonly object EventVisibleChanged = new object();

		// Token: 0x04000875 RID: 2165
		private static readonly object EventControlAdded = new object();

		// Token: 0x04000876 RID: 2166
		private static readonly object EventControlRemoved = new object();

		// Token: 0x04000877 RID: 2167
		private static readonly object EventChangeUICues = new object();

		// Token: 0x04000878 RID: 2168
		private static readonly object EventSystemColorsChanged = new object();

		// Token: 0x04000879 RID: 2169
		private static readonly object EventValidating = new object();

		// Token: 0x0400087A RID: 2170
		private static readonly object EventValidated = new object();

		// Token: 0x0400087B RID: 2171
		private static readonly object EventStyleChanged = new object();

		// Token: 0x0400087C RID: 2172
		private static readonly object EventImeModeChanged = new object();

		// Token: 0x0400087D RID: 2173
		private static readonly object EventHelpRequested = new object();

		// Token: 0x0400087E RID: 2174
		private static readonly object EventPaint = new object();

		// Token: 0x0400087F RID: 2175
		private static readonly object EventInvalidated = new object();

		// Token: 0x04000880 RID: 2176
		private static readonly object EventQueryContinueDrag = new object();

		// Token: 0x04000881 RID: 2177
		private static readonly object EventGiveFeedback = new object();

		// Token: 0x04000882 RID: 2178
		private static readonly object EventDragEnter = new object();

		// Token: 0x04000883 RID: 2179
		private static readonly object EventDragLeave = new object();

		// Token: 0x04000884 RID: 2180
		private static readonly object EventDragOver = new object();

		// Token: 0x04000885 RID: 2181
		private static readonly object EventDragDrop = new object();

		// Token: 0x04000886 RID: 2182
		private static readonly object EventQueryAccessibilityHelp = new object();

		// Token: 0x04000887 RID: 2183
		private static readonly object EventBackgroundImage = new object();

		// Token: 0x04000888 RID: 2184
		private static readonly object EventBackgroundImageLayout = new object();

		// Token: 0x04000889 RID: 2185
		private static readonly object EventBindingContext = new object();

		// Token: 0x0400088A RID: 2186
		private static readonly object EventBackColor = new object();

		// Token: 0x0400088B RID: 2187
		private static readonly object EventParent = new object();

		// Token: 0x0400088C RID: 2188
		private static readonly object EventVisible = new object();

		// Token: 0x0400088D RID: 2189
		private static readonly object EventText = new object();

		// Token: 0x0400088E RID: 2190
		private static readonly object EventTabStop = new object();

		// Token: 0x0400088F RID: 2191
		private static readonly object EventTabIndex = new object();

		// Token: 0x04000890 RID: 2192
		private static readonly object EventSize = new object();

		// Token: 0x04000891 RID: 2193
		private static readonly object EventRightToLeft = new object();

		// Token: 0x04000892 RID: 2194
		private static readonly object EventLocation = new object();

		// Token: 0x04000893 RID: 2195
		private static readonly object EventForeColor = new object();

		// Token: 0x04000894 RID: 2196
		private static readonly object EventFont = new object();

		// Token: 0x04000895 RID: 2197
		private static readonly object EventEnabled = new object();

		// Token: 0x04000896 RID: 2198
		private static readonly object EventDock = new object();

		// Token: 0x04000897 RID: 2199
		private static readonly object EventCursor = new object();

		// Token: 0x04000898 RID: 2200
		private static readonly object EventContextMenu = new object();

		// Token: 0x04000899 RID: 2201
		private static readonly object EventContextMenuStrip = new object();

		// Token: 0x0400089A RID: 2202
		private static readonly object EventCausesValidation = new object();

		// Token: 0x0400089B RID: 2203
		private static readonly object EventRegionChanged = new object();

		// Token: 0x0400089C RID: 2204
		private static readonly object EventMarginChanged = new object();

		// Token: 0x0400089D RID: 2205
		internal static readonly object EventPaddingChanged = new object();

		// Token: 0x0400089E RID: 2206
		private static readonly object EventPreviewKeyDown = new object();

		// Token: 0x0400089F RID: 2207
		private static int mouseWheelMessage = 522;

		// Token: 0x040008A0 RID: 2208
		private static bool mouseWheelRoutingNeeded;

		// Token: 0x040008A1 RID: 2209
		private static bool mouseWheelInit;

		// Token: 0x040008A2 RID: 2210
		private static int threadCallbackMessage;

		// Token: 0x040008A3 RID: 2211
		private static bool checkForIllegalCrossThreadCalls = Debugger.IsAttached;

		// Token: 0x040008A4 RID: 2212
		private static ContextCallback invokeMarshaledCallbackHelperDelegate;

		// Token: 0x040008A5 RID: 2213
		[ThreadStatic]
		private static bool inCrossThreadSafeCall = false;

		// Token: 0x040008A6 RID: 2214
		[ThreadStatic]
		internal static HelpInfo currentHelpInfo = null;

		// Token: 0x040008A7 RID: 2215
		private static Control.FontHandleWrapper defaultFontHandleWrapper;

		// Token: 0x040008A8 RID: 2216
		private const short PaintLayerBackground = 1;

		// Token: 0x040008A9 RID: 2217
		private const short PaintLayerForeground = 2;

		// Token: 0x040008AA RID: 2218
		private const byte RequiredScalingEnabledMask = 16;

		// Token: 0x040008AB RID: 2219
		private const byte RequiredScalingMask = 15;

		// Token: 0x040008AC RID: 2220
		private const byte HighOrderBitMask = 128;

		// Token: 0x040008AD RID: 2221
		private static Font defaultFont;

		// Token: 0x040008AE RID: 2222
		private static readonly int PropName = PropertyStore.CreateKey();

		// Token: 0x040008AF RID: 2223
		private static readonly int PropBackBrush = PropertyStore.CreateKey();

		// Token: 0x040008B0 RID: 2224
		private static readonly int PropFontHeight = PropertyStore.CreateKey();

		// Token: 0x040008B1 RID: 2225
		private static readonly int PropCurrentAmbientFont = PropertyStore.CreateKey();

		// Token: 0x040008B2 RID: 2226
		private static readonly int PropControlsCollection = PropertyStore.CreateKey();

		// Token: 0x040008B3 RID: 2227
		private static readonly int PropBackColor = PropertyStore.CreateKey();

		// Token: 0x040008B4 RID: 2228
		private static readonly int PropForeColor = PropertyStore.CreateKey();

		// Token: 0x040008B5 RID: 2229
		private static readonly int PropFont = PropertyStore.CreateKey();

		// Token: 0x040008B6 RID: 2230
		private static readonly int PropBackgroundImage = PropertyStore.CreateKey();

		// Token: 0x040008B7 RID: 2231
		private static readonly int PropFontHandleWrapper = PropertyStore.CreateKey();

		// Token: 0x040008B8 RID: 2232
		private static readonly int PropUserData = PropertyStore.CreateKey();

		// Token: 0x040008B9 RID: 2233
		private static readonly int PropContextMenu = PropertyStore.CreateKey();

		// Token: 0x040008BA RID: 2234
		private static readonly int PropCursor = PropertyStore.CreateKey();

		// Token: 0x040008BB RID: 2235
		private static readonly int PropRegion = PropertyStore.CreateKey();

		// Token: 0x040008BC RID: 2236
		private static readonly int PropRightToLeft = PropertyStore.CreateKey();

		// Token: 0x040008BD RID: 2237
		private static readonly int PropBindings = PropertyStore.CreateKey();

		// Token: 0x040008BE RID: 2238
		private static readonly int PropBindingManager = PropertyStore.CreateKey();

		// Token: 0x040008BF RID: 2239
		private static readonly int PropAccessibleDefaultActionDescription = PropertyStore.CreateKey();

		// Token: 0x040008C0 RID: 2240
		private static readonly int PropAccessibleDescription = PropertyStore.CreateKey();

		// Token: 0x040008C1 RID: 2241
		private static readonly int PropAccessibility = PropertyStore.CreateKey();

		// Token: 0x040008C2 RID: 2242
		private static readonly int PropUnsafeAccessibility = PropertyStore.CreateKey();

		// Token: 0x040008C3 RID: 2243
		private static readonly int PropNcAccessibility = PropertyStore.CreateKey();

		// Token: 0x040008C4 RID: 2244
		private static readonly int PropAccessibleName = PropertyStore.CreateKey();

		// Token: 0x040008C5 RID: 2245
		private static readonly int PropAccessibleRole = PropertyStore.CreateKey();

		// Token: 0x040008C6 RID: 2246
		private static readonly int PropPaintingException = PropertyStore.CreateKey();

		// Token: 0x040008C7 RID: 2247
		private static readonly int PropActiveXImpl = PropertyStore.CreateKey();

		// Token: 0x040008C8 RID: 2248
		private static readonly int PropControlVersionInfo = PropertyStore.CreateKey();

		// Token: 0x040008C9 RID: 2249
		private static readonly int PropBackgroundImageLayout = PropertyStore.CreateKey();

		// Token: 0x040008CA RID: 2250
		private static readonly int PropAccessibleHelpProvider = PropertyStore.CreateKey();

		// Token: 0x040008CB RID: 2251
		private static readonly int PropContextMenuStrip = PropertyStore.CreateKey();

		// Token: 0x040008CC RID: 2252
		private static readonly int PropAutoScrollOffset = PropertyStore.CreateKey();

		// Token: 0x040008CD RID: 2253
		private static readonly int PropUseCompatibleTextRendering = PropertyStore.CreateKey();

		// Token: 0x040008CE RID: 2254
		private static readonly int PropImeWmCharsToIgnore = PropertyStore.CreateKey();

		// Token: 0x040008CF RID: 2255
		private static readonly int PropImeMode = PropertyStore.CreateKey();

		// Token: 0x040008D0 RID: 2256
		private static readonly int PropDisableImeModeChangedCount = PropertyStore.CreateKey();

		// Token: 0x040008D1 RID: 2257
		private static readonly int PropLastCanEnableIme = PropertyStore.CreateKey();

		// Token: 0x040008D2 RID: 2258
		private static readonly int PropCacheTextCount = PropertyStore.CreateKey();

		// Token: 0x040008D3 RID: 2259
		private static readonly int PropCacheTextField = PropertyStore.CreateKey();

		// Token: 0x040008D4 RID: 2260
		private static readonly int PropAmbientPropertiesService = PropertyStore.CreateKey();

		// Token: 0x040008D5 RID: 2261
		private static bool needToLoadComCtl = true;

		// Token: 0x040008D6 RID: 2262
		internal static bool UseCompatibleTextRenderingDefault = true;

		// Token: 0x040008D7 RID: 2263
		private Control.ControlNativeWindow window;

		// Token: 0x040008D8 RID: 2264
		private Control parent;

		// Token: 0x040008D9 RID: 2265
		private Control reflectParent;

		// Token: 0x040008DA RID: 2266
		private CreateParams createParams;

		// Token: 0x040008DB RID: 2267
		private int x;

		// Token: 0x040008DC RID: 2268
		private int y;

		// Token: 0x040008DD RID: 2269
		private int width;

		// Token: 0x040008DE RID: 2270
		private int height;

		// Token: 0x040008DF RID: 2271
		private int clientWidth;

		// Token: 0x040008E0 RID: 2272
		private int clientHeight;

		// Token: 0x040008E1 RID: 2273
		private int state;

		// Token: 0x040008E2 RID: 2274
		private int state2;

		// Token: 0x040008E3 RID: 2275
		private ControlStyles controlStyle;

		// Token: 0x040008E4 RID: 2276
		private int tabIndex;

		// Token: 0x040008E5 RID: 2277
		private string text;

		// Token: 0x040008E6 RID: 2278
		private byte layoutSuspendCount;

		// Token: 0x040008E7 RID: 2279
		private byte requiredScaling;

		// Token: 0x040008E8 RID: 2280
		private PropertyStore propertyStore;

		// Token: 0x040008E9 RID: 2281
		private NativeMethods.TRACKMOUSEEVENT trackMouseEvent;

		// Token: 0x040008EA RID: 2282
		private short updateCount;

		// Token: 0x040008EB RID: 2283
		private LayoutEventArgs cachedLayoutEventArgs;

		// Token: 0x040008EC RID: 2284
		private Queue threadCallbackList;

		// Token: 0x040008ED RID: 2285
		internal int deviceDpi;

		// Token: 0x040008EE RID: 2286
		private int uiCuesState;

		// Token: 0x040008EF RID: 2287
		private const int UISTATE_FOCUS_CUES_MASK = 15;

		// Token: 0x040008F0 RID: 2288
		private const int UISTATE_FOCUS_CUES_HIDDEN = 1;

		// Token: 0x040008F1 RID: 2289
		private const int UISTATE_FOCUS_CUES_SHOW = 2;

		// Token: 0x040008F2 RID: 2290
		private const int UISTATE_KEYBOARD_CUES_MASK = 240;

		// Token: 0x040008F3 RID: 2291
		private const int UISTATE_KEYBOARD_CUES_HIDDEN = 16;

		// Token: 0x040008F4 RID: 2292
		private const int UISTATE_KEYBOARD_CUES_SHOW = 32;

		// Token: 0x040008F5 RID: 2293
		[ThreadStatic]
		private static byte[] tempKeyboardStateArray;

		// Token: 0x040008F6 RID: 2294
		private readonly WeakReference<ToolStripControlHost> toolStripControlHostReference = new WeakReference<ToolStripControlHost>(null);

		// Token: 0x040008F7 RID: 2295
		private const int ImeCharsToIgnoreDisabled = -1;

		// Token: 0x040008F8 RID: 2296
		private const int ImeCharsToIgnoreEnabled = 0;

		// Token: 0x040008F9 RID: 2297
		private static ImeMode propagatingImeMode = ImeMode.Inherit;

		// Token: 0x040008FA RID: 2298
		private static bool ignoreWmImeNotify;

		// Token: 0x040008FB RID: 2299
		private static bool lastLanguageChinese = false;

		// Token: 0x02000634 RID: 1588
		private class ControlTabOrderHolder
		{
			// Token: 0x060063FC RID: 25596 RVA: 0x00171F70 File Offset: 0x00170170
			internal ControlTabOrderHolder(int oldOrder, int newOrder, Control control)
			{
				this.oldOrder = oldOrder;
				this.newOrder = newOrder;
				this.control = control;
			}

			// Token: 0x04003959 RID: 14681
			internal readonly int oldOrder;

			// Token: 0x0400395A RID: 14682
			internal readonly int newOrder;

			// Token: 0x0400395B RID: 14683
			internal readonly Control control;
		}

		// Token: 0x02000635 RID: 1589
		private class ControlTabOrderComparer : IComparer
		{
			// Token: 0x060063FD RID: 25597 RVA: 0x00171F90 File Offset: 0x00170190
			int IComparer.Compare(object x, object y)
			{
				Control.ControlTabOrderHolder controlTabOrderHolder = (Control.ControlTabOrderHolder)x;
				Control.ControlTabOrderHolder controlTabOrderHolder2 = (Control.ControlTabOrderHolder)y;
				int num = controlTabOrderHolder.newOrder - controlTabOrderHolder2.newOrder;
				if (num == 0)
				{
					num = controlTabOrderHolder.oldOrder - controlTabOrderHolder2.oldOrder;
				}
				return num;
			}
		}

		// Token: 0x02000636 RID: 1590
		internal sealed class ControlNativeWindow : NativeWindow, IWindowTarget
		{
			// Token: 0x060063FF RID: 25599 RVA: 0x00171FCB File Offset: 0x001701CB
			internal ControlNativeWindow(Control control)
			{
				this.control = control;
				this.target = this;
			}

			// Token: 0x06006400 RID: 25600 RVA: 0x00171FE1 File Offset: 0x001701E1
			internal Control GetControl()
			{
				return this.control;
			}

			// Token: 0x06006401 RID: 25601 RVA: 0x00171FE9 File Offset: 0x001701E9
			protected override void OnHandleChange()
			{
				this.target.OnHandleChange(base.Handle);
			}

			// Token: 0x06006402 RID: 25602 RVA: 0x00171FFC File Offset: 0x001701FC
			public void OnHandleChange(IntPtr newHandle)
			{
				this.control.SetHandle(newHandle);
			}

			// Token: 0x06006403 RID: 25603 RVA: 0x0017200A File Offset: 0x0017020A
			internal void LockReference(bool locked)
			{
				if (locked)
				{
					if (!this.rootRef.IsAllocated)
					{
						this.rootRef = GCHandle.Alloc(this.GetControl(), GCHandleType.Normal);
						return;
					}
				}
				else if (this.rootRef.IsAllocated)
				{
					this.rootRef.Free();
				}
			}

			// Token: 0x06006404 RID: 25604 RVA: 0x00172047 File Offset: 0x00170247
			protected override void OnThreadException(Exception e)
			{
				this.control.WndProcException(e);
			}

			// Token: 0x06006405 RID: 25605 RVA: 0x00172055 File Offset: 0x00170255
			public void OnMessage(ref Message m)
			{
				this.control.WndProc(ref m);
			}

			// Token: 0x17001569 RID: 5481
			// (get) Token: 0x06006406 RID: 25606 RVA: 0x00172063 File Offset: 0x00170263
			// (set) Token: 0x06006407 RID: 25607 RVA: 0x0017206B File Offset: 0x0017026B
			internal IWindowTarget WindowTarget
			{
				get
				{
					return this.target;
				}
				set
				{
					this.target = value;
				}
			}

			// Token: 0x06006408 RID: 25608 RVA: 0x00172074 File Offset: 0x00170274
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 512)
				{
					if (msg != 522)
					{
						if (msg == 675)
						{
							this.control.UnhookMouseEvent();
						}
					}
					else
					{
						this.control.ResetMouseEventArgs();
					}
				}
				else if (!this.control.GetState(16384))
				{
					this.control.HookMouseEvent();
					if (!this.control.GetState(8192))
					{
						this.control.SendMessage(NativeMethods.WM_MOUSEENTER, 0, 0);
					}
					else
					{
						this.control.SetState(8192, false);
					}
				}
				this.target.OnMessage(ref m);
			}

			// Token: 0x0400395C RID: 14684
			private Control control;

			// Token: 0x0400395D RID: 14685
			private GCHandle rootRef;

			// Token: 0x0400395E RID: 14686
			internal IWindowTarget target;
		}

		// Token: 0x02000637 RID: 1591
		[ListBindable(false)]
		[ComVisible(false)]
		public class ControlCollection : ArrangedElementCollection, IList, ICollection, IEnumerable, ICloneable
		{
			// Token: 0x06006409 RID: 25609 RVA: 0x0017211B File Offset: 0x0017031B
			public ControlCollection(Control owner)
			{
				this.owner = owner;
			}

			// Token: 0x0600640A RID: 25610 RVA: 0x00172131 File Offset: 0x00170331
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x0600640B RID: 25611 RVA: 0x00172140 File Offset: 0x00170340
			public virtual void Add(Control value)
			{
				if (value == null)
				{
					return;
				}
				if (value.GetTopLevel())
				{
					throw new ArgumentException(SR.GetString("TopLevelControlAdd"));
				}
				if (this.owner.CreateThreadId != value.CreateThreadId)
				{
					throw new ArgumentException(SR.GetString("AddDifferentThreads"));
				}
				Control.CheckParentingCycle(this.owner, value);
				if (value.parent == this.owner)
				{
					value.SendToBack();
					return;
				}
				if (value.parent != null)
				{
					value.parent.Controls.Remove(value);
				}
				base.InnerList.Add(value);
				if (value.tabIndex == -1)
				{
					int num = 0;
					for (int i = 0; i < this.Count - 1; i++)
					{
						int tabIndex = this[i].TabIndex;
						if (num <= tabIndex)
						{
							num = tabIndex + 1;
						}
					}
					value.tabIndex = num;
				}
				this.owner.SuspendLayout();
				try
				{
					Control parent = value.parent;
					try
					{
						value.AssignParent(this.owner);
					}
					finally
					{
						if (parent != value.parent && (this.owner.state & 1) != 0)
						{
							value.SetParentHandle(this.owner.InternalHandle);
							if (value.Visible)
							{
								value.CreateControl();
							}
						}
					}
					value.InitLayout();
				}
				finally
				{
					this.owner.ResumeLayout(false);
				}
				LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
				this.owner.OnControlAdded(new ControlEventArgs(value));
			}

			// Token: 0x0600640C RID: 25612 RVA: 0x001722B8 File Offset: 0x001704B8
			int IList.Add(object control)
			{
				if (control is Control)
				{
					this.Add((Control)control);
					return this.IndexOf((Control)control);
				}
				throw new ArgumentException(SR.GetString("ControlBadControl"), "control");
			}

			// Token: 0x0600640D RID: 25613 RVA: 0x001722F0 File Offset: 0x001704F0
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public virtual void AddRange(Control[] controls)
			{
				if (controls == null)
				{
					throw new ArgumentNullException("controls");
				}
				if (controls.Length != 0)
				{
					this.owner.SuspendLayout();
					try
					{
						for (int i = 0; i < controls.Length; i++)
						{
							this.Add(controls[i]);
						}
					}
					finally
					{
						this.owner.ResumeLayout(true);
					}
				}
			}

			// Token: 0x0600640E RID: 25614 RVA: 0x00172350 File Offset: 0x00170550
			object ICloneable.Clone()
			{
				Control.ControlCollection controlCollection = this.owner.CreateControlsInstance();
				controlCollection.InnerList.AddRange(this);
				return controlCollection;
			}

			// Token: 0x0600640F RID: 25615 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
			public bool Contains(Control control)
			{
				return base.InnerList.Contains(control);
			}

			// Token: 0x06006410 RID: 25616 RVA: 0x00172378 File Offset: 0x00170578
			public Control[] Find(string key, bool searchAllChildren)
			{
				if (string.IsNullOrEmpty(key))
				{
					throw new ArgumentNullException("key", SR.GetString("FindKeyMayNotBeEmptyOrNull"));
				}
				ArrayList arrayList = this.FindInternal(key, searchAllChildren, this, new ArrayList());
				Control[] array = new Control[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006411 RID: 25617 RVA: 0x001723C8 File Offset: 0x001705C8
			private ArrayList FindInternal(string key, bool searchAllChildren, Control.ControlCollection controlsToLookIn, ArrayList foundControls)
			{
				if (controlsToLookIn == null || foundControls == null)
				{
					return null;
				}
				try
				{
					for (int i = 0; i < controlsToLookIn.Count; i++)
					{
						if (controlsToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(controlsToLookIn[i].Name, key, true))
						{
							foundControls.Add(controlsToLookIn[i]);
						}
					}
					if (searchAllChildren)
					{
						for (int j = 0; j < controlsToLookIn.Count; j++)
						{
							if (controlsToLookIn[j] != null && controlsToLookIn[j].Controls != null && controlsToLookIn[j].Controls.Count > 0)
							{
								foundControls = this.FindInternal(key, searchAllChildren, controlsToLookIn[j].Controls, foundControls);
							}
						}
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return foundControls;
			}

			// Token: 0x06006412 RID: 25618 RVA: 0x00172498 File Offset: 0x00170698
			public override IEnumerator GetEnumerator()
			{
				return new Control.ControlCollection.ControlCollectionEnumerator(this);
			}

			// Token: 0x06006413 RID: 25619 RVA: 0x0011CE4C File Offset: 0x0011B04C
			public int IndexOf(Control control)
			{
				return base.InnerList.IndexOf(control);
			}

			// Token: 0x06006414 RID: 25620 RVA: 0x001724A0 File Offset: 0x001706A0
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			// Token: 0x06006415 RID: 25621 RVA: 0x0011CEDC File Offset: 0x0011B0DC
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x1700156A RID: 5482
			// (get) Token: 0x06006416 RID: 25622 RVA: 0x0017251D File Offset: 0x0017071D
			public Control Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x06006417 RID: 25623 RVA: 0x00172528 File Offset: 0x00170728
			public virtual void Remove(Control value)
			{
				if (value == null)
				{
					return;
				}
				if (value.ParentInternal == this.owner)
				{
					value.SetParentHandle(IntPtr.Zero);
					base.InnerList.Remove(value);
					value.AssignParent(null);
					LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
					this.owner.OnControlRemoved(new ControlEventArgs(value));
					ContainerControl containerControl = this.owner.GetContainerControlInternal() as ContainerControl;
					if (containerControl != null)
					{
						containerControl.AfterControlRemoved(value, this.owner);
					}
				}
			}

			// Token: 0x06006418 RID: 25624 RVA: 0x001725A8 File Offset: 0x001707A8
			void IList.Remove(object control)
			{
				if (control is Control)
				{
					this.Remove((Control)control);
				}
			}

			// Token: 0x06006419 RID: 25625 RVA: 0x001725BE File Offset: 0x001707BE
			public void RemoveAt(int index)
			{
				this.Remove(this[index]);
			}

			// Token: 0x0600641A RID: 25626 RVA: 0x001725D0 File Offset: 0x001707D0
			public virtual void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x1700156B RID: 5483
			public virtual Control this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[]
						{
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return (Control)base.InnerList[index];
				}
			}

			// Token: 0x1700156C RID: 5484
			public virtual Control this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int index = this.IndexOfKey(key);
					if (this.IsValidIndex(index))
					{
						return this[index];
					}
					return null;
				}
			}

			// Token: 0x0600641D RID: 25629 RVA: 0x00172684 File Offset: 0x00170884
			public virtual void Clear()
			{
				this.owner.SuspendLayout();
				CommonProperties.xClearAllPreferredSizeCaches(this.owner);
				try
				{
					while (this.Count != 0)
					{
						this.RemoveAt(this.Count - 1);
					}
				}
				finally
				{
					this.owner.ResumeLayout();
				}
			}

			// Token: 0x0600641E RID: 25630 RVA: 0x001726E0 File Offset: 0x001708E0
			public int GetChildIndex(Control child)
			{
				return this.GetChildIndex(child, true);
			}

			// Token: 0x0600641F RID: 25631 RVA: 0x001726EC File Offset: 0x001708EC
			public virtual int GetChildIndex(Control child, bool throwException)
			{
				int num = this.IndexOf(child);
				if (num == -1 && throwException)
				{
					throw new ArgumentException(SR.GetString("ControlNotChild"));
				}
				return num;
			}

			// Token: 0x06006420 RID: 25632 RVA: 0x0017271C File Offset: 0x0017091C
			internal virtual void SetChildIndexInternal(Control child, int newIndex)
			{
				if (child == null)
				{
					throw new ArgumentNullException("child");
				}
				int childIndex = this.GetChildIndex(child);
				if (childIndex == newIndex)
				{
					return;
				}
				if (newIndex >= this.Count || newIndex == -1)
				{
					newIndex = this.Count - 1;
				}
				base.MoveElement(child, childIndex, newIndex);
				child.UpdateZOrder();
				LayoutTransaction.DoLayout(this.owner, child, PropertyNames.ChildIndex);
			}

			// Token: 0x06006421 RID: 25633 RVA: 0x0017277B File Offset: 0x0017097B
			public virtual void SetChildIndex(Control child, int newIndex)
			{
				this.SetChildIndexInternal(child, newIndex);
			}

			// Token: 0x0400395F RID: 14687
			private Control owner;

			// Token: 0x04003960 RID: 14688
			private int lastAccessedIndex = -1;

			// Token: 0x020008B6 RID: 2230
			private class ControlCollectionEnumerator : IEnumerator
			{
				// Token: 0x060072CF RID: 29391 RVA: 0x001A468C File Offset: 0x001A288C
				public ControlCollectionEnumerator(Control.ControlCollection controls)
				{
					this.controls = controls;
					this.originalCount = controls.Count;
					this.current = -1;
				}

				// Token: 0x060072D0 RID: 29392 RVA: 0x001A46AE File Offset: 0x001A28AE
				public bool MoveNext()
				{
					if (this.current < this.controls.Count - 1 && this.current < this.originalCount - 1)
					{
						this.current++;
						return true;
					}
					return false;
				}

				// Token: 0x060072D1 RID: 29393 RVA: 0x001A46E6 File Offset: 0x001A28E6
				public void Reset()
				{
					this.current = -1;
				}

				// Token: 0x17001933 RID: 6451
				// (get) Token: 0x060072D2 RID: 29394 RVA: 0x001A46EF File Offset: 0x001A28EF
				public object Current
				{
					get
					{
						if (this.current == -1)
						{
							return null;
						}
						return this.controls[this.current];
					}
				}

				// Token: 0x0400452D RID: 17709
				private Control.ControlCollection controls;

				// Token: 0x0400452E RID: 17710
				private int current;

				// Token: 0x0400452F RID: 17711
				private int originalCount;
			}
		}

		// Token: 0x02000638 RID: 1592
		private class ActiveXImpl : MarshalByRefObject, IWindowTarget
		{
			// Token: 0x06006422 RID: 25634 RVA: 0x00172788 File Offset: 0x00170988
			internal ActiveXImpl(Control control)
			{
				this.control = control;
				this.controlWindowTarget = control.WindowTarget;
				control.WindowTarget = this;
				this.adviseList = new ArrayList();
				this.activeXState = default(BitVector32);
				this.ambientProperties = new Control.AmbientProperty[]
				{
					new Control.AmbientProperty("Font", -703),
					new Control.AmbientProperty("BackColor", -701),
					new Control.AmbientProperty("ForeColor", -704)
				};
			}

			// Token: 0x1700156D RID: 5485
			// (get) Token: 0x06006423 RID: 25635 RVA: 0x00172818 File Offset: 0x00170A18
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			internal Color AmbientBackColor
			{
				get
				{
					Control.AmbientProperty ambientProperty = this.LookupAmbient(-701);
					if (ambientProperty.Empty)
					{
						object obj = null;
						if (this.GetAmbientProperty(-701, ref obj) && obj != null)
						{
							try
							{
								ambientProperty.Value = ColorTranslator.FromOle(Convert.ToInt32(obj, CultureInfo.InvariantCulture));
							}
							catch (Exception ex)
							{
								if (ClientUtils.IsSecurityOrCriticalException(ex))
								{
									throw;
								}
							}
						}
					}
					if (ambientProperty.Value == null)
					{
						return Color.Empty;
					}
					return (Color)ambientProperty.Value;
				}
			}

			// Token: 0x1700156E RID: 5486
			// (get) Token: 0x06006424 RID: 25636 RVA: 0x001728A0 File Offset: 0x00170AA0
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			internal Font AmbientFont
			{
				get
				{
					Control.AmbientProperty ambientProperty = this.LookupAmbient(-703);
					if (ambientProperty.Empty)
					{
						object obj = null;
						if (this.GetAmbientProperty(-703, ref obj))
						{
							try
							{
								IntPtr hfont = IntPtr.Zero;
								UnsafeNativeMethods.IFont font = (UnsafeNativeMethods.IFont)obj;
								IntSecurity.ObjectFromWin32Handle.Assert();
								Font value = null;
								try
								{
									hfont = font.GetHFont();
									value = Font.FromHfont(hfont);
								}
								finally
								{
									CodeAccessPermission.RevertAssert();
								}
								ambientProperty.Value = value;
							}
							catch (Exception ex)
							{
								if (ClientUtils.IsSecurityOrCriticalException(ex))
								{
									throw;
								}
								ambientProperty.Value = null;
							}
						}
					}
					return (Font)ambientProperty.Value;
				}
			}

			// Token: 0x1700156F RID: 5487
			// (get) Token: 0x06006425 RID: 25637 RVA: 0x0017294C File Offset: 0x00170B4C
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			internal Color AmbientForeColor
			{
				get
				{
					Control.AmbientProperty ambientProperty = this.LookupAmbient(-704);
					if (ambientProperty.Empty)
					{
						object obj = null;
						if (this.GetAmbientProperty(-704, ref obj) && obj != null)
						{
							try
							{
								ambientProperty.Value = ColorTranslator.FromOle(Convert.ToInt32(obj, CultureInfo.InvariantCulture));
							}
							catch (Exception ex)
							{
								if (ClientUtils.IsSecurityOrCriticalException(ex))
								{
									throw;
								}
							}
						}
					}
					if (ambientProperty.Value == null)
					{
						return Color.Empty;
					}
					return (Color)ambientProperty.Value;
				}
			}

			// Token: 0x17001570 RID: 5488
			// (get) Token: 0x06006426 RID: 25638 RVA: 0x001729D4 File Offset: 0x00170BD4
			// (set) Token: 0x06006427 RID: 25639 RVA: 0x001729E6 File Offset: 0x00170BE6
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			internal bool EventsFrozen
			{
				get
				{
					return this.activeXState[Control.ActiveXImpl.eventsFrozen];
				}
				set
				{
					this.activeXState[Control.ActiveXImpl.eventsFrozen] = value;
				}
			}

			// Token: 0x17001571 RID: 5489
			// (get) Token: 0x06006428 RID: 25640 RVA: 0x001729F9 File Offset: 0x00170BF9
			internal IntPtr HWNDParent
			{
				get
				{
					return this.hwndParent;
				}
			}

			// Token: 0x17001572 RID: 5490
			// (get) Token: 0x06006429 RID: 25641 RVA: 0x00172A04 File Offset: 0x00170C04
			internal bool IsIE
			{
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (!Control.ActiveXImpl.checkedIE)
					{
						if (this.clientSite == null)
						{
							return false;
						}
						if (Assembly.GetEntryAssembly() == null)
						{
							UnsafeNativeMethods.IOleContainer oleContainer;
							if (NativeMethods.Succeeded(this.clientSite.GetContainer(out oleContainer)) && oleContainer is NativeMethods.IHTMLDocument)
							{
								Control.ActiveXImpl.isIE = true;
							}
							if (oleContainer != null && UnsafeNativeMethods.IsComObject(oleContainer))
							{
								UnsafeNativeMethods.ReleaseComObject(oleContainer);
							}
						}
						Control.ActiveXImpl.checkedIE = true;
					}
					return Control.ActiveXImpl.isIE;
				}
			}

			// Token: 0x17001573 RID: 5491
			// (get) Token: 0x0600642A RID: 25642 RVA: 0x00172A70 File Offset: 0x00170C70
			private Point LogPixels
			{
				get
				{
					if (Control.ActiveXImpl.logPixels.IsEmpty)
					{
						Control.ActiveXImpl.logPixels = default(Point);
						IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
						Control.ActiveXImpl.logPixels.X = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
						Control.ActiveXImpl.logPixels.Y = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 90);
						UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
					}
					return Control.ActiveXImpl.logPixels;
				}
			}

			// Token: 0x0600642B RID: 25643 RVA: 0x00172AE6 File Offset: 0x00170CE6
			internal int Advise(IAdviseSink pAdvSink)
			{
				this.adviseList.Add(pAdvSink);
				return this.adviseList.Count;
			}

			// Token: 0x0600642C RID: 25644 RVA: 0x00172B00 File Offset: 0x00170D00
			internal void Close(int dwSaveOption)
			{
				if (this.activeXState[Control.ActiveXImpl.inPlaceActive])
				{
					this.InPlaceDeactivate();
				}
				if ((dwSaveOption == 0 || dwSaveOption == 2) && this.activeXState[Control.ActiveXImpl.isDirty])
				{
					if (this.clientSite != null)
					{
						this.clientSite.SaveObject();
					}
					this.SendOnSave();
				}
			}

			// Token: 0x0600642D RID: 25645 RVA: 0x00172B58 File Offset: 0x00170D58
			internal void DoVerb(int iVerb, IntPtr lpmsg, UnsafeNativeMethods.IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, NativeMethods.COMRECT lprcPosRect)
			{
				switch (iVerb)
				{
				case -5:
				case -4:
				case -1:
				case 0:
				{
					this.InPlaceActivate(iVerb);
					if (!(lpmsg != IntPtr.Zero))
					{
						return;
					}
					NativeMethods.MSG msg = (NativeMethods.MSG)UnsafeNativeMethods.PtrToStructure(lpmsg, typeof(NativeMethods.MSG));
					Control control = this.control;
					if (msg.hwnd != this.control.Handle && msg.message >= 512 && msg.message <= 522)
					{
						IntPtr handle = (msg.hwnd == IntPtr.Zero) ? hwndParent : msg.hwnd;
						NativeMethods.POINT point = new NativeMethods.POINT();
						point.x = NativeMethods.Util.LOWORD(msg.lParam);
						point.y = NativeMethods.Util.HIWORD(msg.lParam);
						UnsafeNativeMethods.MapWindowPoints(new HandleRef(null, handle), new HandleRef(this.control, this.control.Handle), point, 1);
						Control childAtPoint = control.GetChildAtPoint(new Point(point.x, point.y));
						if (childAtPoint != null && childAtPoint != control)
						{
							UnsafeNativeMethods.MapWindowPoints(new HandleRef(control, control.Handle), new HandleRef(childAtPoint, childAtPoint.Handle), point, 1);
							control = childAtPoint;
						}
						msg.lParam = NativeMethods.Util.MAKELPARAM(point.x, point.y);
					}
					if (msg.message == 256 && msg.wParam == (IntPtr)9)
					{
						control.SelectNextControl(null, Control.ModifierKeys != Keys.Shift, true, true, true);
						return;
					}
					control.SendMessage(msg.message, msg.wParam, msg.lParam);
					return;
				}
				case -3:
					this.UIDeactivate();
					this.InPlaceDeactivate();
					if (this.activeXState[Control.ActiveXImpl.inPlaceVisible])
					{
						this.SetInPlaceVisible(false);
						return;
					}
					return;
				}
				Control.ActiveXImpl.ThrowHr(-2147467263);
			}

			// Token: 0x0600642E RID: 25646 RVA: 0x00172D4C File Offset: 0x00170F4C
			internal void Draw(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, NativeMethods.COMRECT prcBounds, NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, int dwContinue)
			{
				if (dwDrawAspect != 1 && dwDrawAspect != 16 && dwDrawAspect != 32)
				{
					Control.ActiveXImpl.ThrowHr(-2147221397);
				}
				int objectType = UnsafeNativeMethods.GetObjectType(new HandleRef(null, hdcDraw));
				if (objectType == 4)
				{
					Control.ActiveXImpl.ThrowHr(-2147221184);
				}
				NativeMethods.POINT point = new NativeMethods.POINT();
				NativeMethods.POINT point2 = new NativeMethods.POINT();
				NativeMethods.SIZE size = new NativeMethods.SIZE();
				NativeMethods.SIZE size2 = new NativeMethods.SIZE();
				int nMapMode = 1;
				if (!this.control.IsHandleCreated)
				{
					this.control.CreateHandle();
				}
				if (prcBounds != null)
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(prcBounds.left, prcBounds.top, prcBounds.right, prcBounds.bottom);
					SafeNativeMethods.LPtoDP(new HandleRef(null, hdcDraw), ref rect, 2);
					nMapMode = SafeNativeMethods.SetMapMode(new HandleRef(null, hdcDraw), 8);
					SafeNativeMethods.SetWindowOrgEx(new HandleRef(null, hdcDraw), 0, 0, point2);
					SafeNativeMethods.SetWindowExtEx(new HandleRef(null, hdcDraw), this.control.Width, this.control.Height, size);
					SafeNativeMethods.SetViewportOrgEx(new HandleRef(null, hdcDraw), rect.left, rect.top, point);
					SafeNativeMethods.SetViewportExtEx(new HandleRef(null, hdcDraw), rect.right - rect.left, rect.bottom - rect.top, size2);
				}
				try
				{
					IntPtr intPtr = (IntPtr)30;
					if (objectType != 12)
					{
						this.control.SendMessage(791, hdcDraw, intPtr);
					}
					else
					{
						this.control.PrintToMetaFile(new HandleRef(null, hdcDraw), intPtr);
					}
				}
				finally
				{
					if (prcBounds != null)
					{
						SafeNativeMethods.SetWindowOrgEx(new HandleRef(null, hdcDraw), point2.x, point2.y, null);
						SafeNativeMethods.SetWindowExtEx(new HandleRef(null, hdcDraw), size.cx, size.cy, null);
						SafeNativeMethods.SetViewportOrgEx(new HandleRef(null, hdcDraw), point.x, point.y, null);
						SafeNativeMethods.SetViewportExtEx(new HandleRef(null, hdcDraw), size2.cx, size2.cy, null);
						SafeNativeMethods.SetMapMode(new HandleRef(null, hdcDraw), nMapMode);
					}
				}
			}

			// Token: 0x0600642F RID: 25647 RVA: 0x00172F64 File Offset: 0x00171164
			internal static int EnumVerbs(out UnsafeNativeMethods.IEnumOLEVERB e)
			{
				if (Control.ActiveXImpl.axVerbs == null)
				{
					NativeMethods.tagOLEVERB tagOLEVERB = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB2 = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB3 = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB4 = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB5 = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB6 = new NativeMethods.tagOLEVERB();
					tagOLEVERB.lVerb = -1;
					tagOLEVERB2.lVerb = -5;
					tagOLEVERB3.lVerb = -4;
					tagOLEVERB4.lVerb = -3;
					tagOLEVERB5.lVerb = 0;
					tagOLEVERB6.lVerb = -7;
					tagOLEVERB6.lpszVerbName = SR.GetString("AXProperties");
					tagOLEVERB6.grfAttribs = 2;
					Control.ActiveXImpl.axVerbs = new NativeMethods.tagOLEVERB[]
					{
						tagOLEVERB,
						tagOLEVERB2,
						tagOLEVERB3,
						tagOLEVERB4,
						tagOLEVERB5
					};
				}
				e = new Control.ActiveXVerbEnum(Control.ActiveXImpl.axVerbs);
				return 0;
			}

			// Token: 0x06006430 RID: 25648 RVA: 0x00173018 File Offset: 0x00171218
			private static byte[] FromBase64WrappedString(string text)
			{
				if (text.IndexOfAny(new char[]
				{
					' ',
					'\r',
					'\n'
				}) != -1)
				{
					StringBuilder stringBuilder = new StringBuilder(text.Length);
					for (int i = 0; i < text.Length; i++)
					{
						char c = text[i];
						if (c != '\n' && c != '\r' && c != ' ')
						{
							stringBuilder.Append(text[i]);
						}
					}
					return Convert.FromBase64String(stringBuilder.ToString());
				}
				return Convert.FromBase64String(text);
			}

			// Token: 0x06006431 RID: 25649 RVA: 0x00173094 File Offset: 0x00171294
			internal void GetAdvise(int[] paspects, int[] padvf, IAdviseSink[] pAdvSink)
			{
				if (paspects != null)
				{
					paspects[0] = 1;
				}
				if (padvf != null)
				{
					padvf[0] = 0;
					if (this.activeXState[Control.ActiveXImpl.viewAdviseOnlyOnce])
					{
						padvf[0] |= 4;
					}
					if (this.activeXState[Control.ActiveXImpl.viewAdvisePrimeFirst])
					{
						padvf[0] |= 2;
					}
				}
				if (pAdvSink != null)
				{
					pAdvSink[0] = this.viewAdviseSink;
				}
			}

			// Token: 0x06006432 RID: 25650 RVA: 0x001730F8 File Offset: 0x001712F8
			private bool GetAmbientProperty(int dispid, ref object obj)
			{
				if (this.clientSite is UnsafeNativeMethods.IDispatch)
				{
					UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)this.clientSite;
					object[] array = new object[1];
					Guid empty = Guid.Empty;
					int hr = -2147467259;
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						hr = dispatch.Invoke(dispid, ref empty, NativeMethods.LOCALE_USER_DEFAULT, 2, new NativeMethods.tagDISPPARAMS(), array, null, null);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (NativeMethods.Succeeded(hr))
					{
						obj = array[0];
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006433 RID: 25651 RVA: 0x0017317C File Offset: 0x0017137C
			internal UnsafeNativeMethods.IOleClientSite GetClientSite()
			{
				return this.clientSite;
			}

			// Token: 0x06006434 RID: 25652 RVA: 0x00173184 File Offset: 0x00171384
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal int GetControlInfo(NativeMethods.tagCONTROLINFO pCI)
			{
				if (this.accelCount == -1)
				{
					ArrayList arrayList = new ArrayList();
					this.GetMnemonicList(this.control, arrayList);
					this.accelCount = (short)arrayList.Count;
					if (this.accelCount > 0)
					{
						int num = UnsafeNativeMethods.SizeOf(typeof(NativeMethods.ACCEL));
						IntPtr intPtr = Marshal.AllocHGlobal(num * (int)this.accelCount * 2);
						try
						{
							NativeMethods.ACCEL accel = new NativeMethods.ACCEL();
							accel.cmd = 0;
							this.accelCount = 0;
							foreach (object obj in arrayList)
							{
								char c = (char)obj;
								IntPtr intPtr2 = (IntPtr)((long)intPtr + (long)((int)this.accelCount * num));
								if (c >= 'A' && c <= 'Z')
								{
									accel.fVirt = 17;
									accel.key = (UnsafeNativeMethods.VkKeyScan(c) & 255);
									Marshal.StructureToPtr(accel, intPtr2, false);
									this.accelCount += 1;
									intPtr2 = (IntPtr)((long)intPtr2 + (long)num);
									accel.fVirt = 21;
									Marshal.StructureToPtr(accel, intPtr2, false);
								}
								else
								{
									accel.fVirt = 17;
									short num2 = UnsafeNativeMethods.VkKeyScan(c);
									if ((num2 & 256) != 0)
									{
										NativeMethods.ACCEL accel2 = accel;
										accel2.fVirt |= 4;
									}
									accel.key = (num2 & 255);
									Marshal.StructureToPtr(accel, intPtr2, false);
								}
								NativeMethods.ACCEL accel3 = accel;
								accel3.cmd += 1;
								this.accelCount += 1;
							}
							if (this.accelTable != IntPtr.Zero)
							{
								UnsafeNativeMethods.DestroyAcceleratorTable(new HandleRef(this, this.accelTable));
								this.accelTable = IntPtr.Zero;
							}
							this.accelTable = UnsafeNativeMethods.CreateAcceleratorTable(new HandleRef(null, intPtr), (int)this.accelCount);
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								Marshal.FreeHGlobal(intPtr);
							}
						}
					}
				}
				pCI.cAccel = this.accelCount;
				pCI.hAccel = this.accelTable;
				return 0;
			}

			// Token: 0x06006435 RID: 25653 RVA: 0x001733C0 File Offset: 0x001715C0
			internal void GetExtent(int dwDrawAspect, NativeMethods.tagSIZEL pSizel)
			{
				if ((dwDrawAspect & 1) != 0)
				{
					Size size = this.control.Size;
					Point point = this.PixelToHiMetric(size.Width, size.Height);
					pSizel.cx = point.X;
					pSizel.cy = point.Y;
					return;
				}
				Control.ActiveXImpl.ThrowHr(-2147221397);
			}

			// Token: 0x06006436 RID: 25654 RVA: 0x00173418 File Offset: 0x00171618
			private void GetMnemonicList(Control control, ArrayList mnemonicList)
			{
				char mnemonic = WindowsFormsUtils.GetMnemonic(control.Text, true);
				if (mnemonic != '\0')
				{
					mnemonicList.Add(mnemonic);
				}
				foreach (object obj in control.Controls)
				{
					Control control2 = (Control)obj;
					if (control2 != null)
					{
						this.GetMnemonicList(control2, mnemonicList);
					}
				}
			}

			// Token: 0x06006437 RID: 25655 RVA: 0x00173494 File Offset: 0x00171694
			private string GetStreamName()
			{
				string text = this.control.GetType().FullName;
				int length = text.Length;
				if (length > 31)
				{
					text = text.Substring(length - 31);
				}
				return text;
			}

			// Token: 0x06006438 RID: 25656 RVA: 0x001734CA File Offset: 0x001716CA
			internal int GetWindow(out IntPtr hwnd)
			{
				if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
				{
					hwnd = IntPtr.Zero;
					return -2147467259;
				}
				hwnd = this.control.Handle;
				return 0;
			}

			// Token: 0x06006439 RID: 25657 RVA: 0x001734FC File Offset: 0x001716FC
			private Point HiMetricToPixel(int x, int y)
			{
				return new Point
				{
					X = (this.LogPixels.X * x + Control.ActiveXImpl.hiMetricPerInch / 2) / Control.ActiveXImpl.hiMetricPerInch,
					Y = (this.LogPixels.Y * y + Control.ActiveXImpl.hiMetricPerInch / 2) / Control.ActiveXImpl.hiMetricPerInch
				};
			}

			// Token: 0x0600643A RID: 25658 RVA: 0x0017355C File Offset: 0x0017175C
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal void InPlaceActivate(int verb)
			{
				UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
				if (oleInPlaceSite == null)
				{
					return;
				}
				if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
				{
					int num = oleInPlaceSite.CanInPlaceActivate();
					if (num != 0)
					{
						if (NativeMethods.Succeeded(num))
						{
							num = -2147467259;
						}
						Control.ActiveXImpl.ThrowHr(num);
					}
					oleInPlaceSite.OnInPlaceActivate();
					this.activeXState[Control.ActiveXImpl.inPlaceActive] = true;
				}
				if (!this.activeXState[Control.ActiveXImpl.inPlaceVisible])
				{
					NativeMethods.tagOIFI tagOIFI = new NativeMethods.tagOIFI();
					tagOIFI.cb = UnsafeNativeMethods.SizeOf(typeof(NativeMethods.tagOIFI));
					IntPtr handle = IntPtr.Zero;
					handle = oleInPlaceSite.GetWindow();
					NativeMethods.COMRECT lprcPosRect = new NativeMethods.COMRECT();
					NativeMethods.COMRECT lprcClipRect = new NativeMethods.COMRECT();
					if (this.inPlaceUiWindow != null && UnsafeNativeMethods.IsComObject(this.inPlaceUiWindow))
					{
						UnsafeNativeMethods.ReleaseComObject(this.inPlaceUiWindow);
						this.inPlaceUiWindow = null;
					}
					if (this.inPlaceFrame != null && UnsafeNativeMethods.IsComObject(this.inPlaceFrame))
					{
						UnsafeNativeMethods.ReleaseComObject(this.inPlaceFrame);
						this.inPlaceFrame = null;
					}
					UnsafeNativeMethods.IOleInPlaceFrame oleInPlaceFrame;
					UnsafeNativeMethods.IOleInPlaceUIWindow oleInPlaceUIWindow;
					oleInPlaceSite.GetWindowContext(out oleInPlaceFrame, out oleInPlaceUIWindow, lprcPosRect, lprcClipRect, tagOIFI);
					this.SetObjectRects(lprcPosRect, lprcClipRect);
					this.inPlaceFrame = oleInPlaceFrame;
					this.inPlaceUiWindow = oleInPlaceUIWindow;
					this.hwndParent = handle;
					UnsafeNativeMethods.SetParent(new HandleRef(this.control, this.control.Handle), new HandleRef(null, handle));
					this.control.CreateControl();
					this.clientSite.ShowObject();
					this.SetInPlaceVisible(true);
				}
				if (verb != 0 && verb != -4)
				{
					return;
				}
				if (!this.activeXState[Control.ActiveXImpl.uiActive])
				{
					this.activeXState[Control.ActiveXImpl.uiActive] = true;
					oleInPlaceSite.OnUIActivate();
					if (!this.control.ContainsFocus)
					{
						this.control.FocusInternal();
					}
					this.inPlaceFrame.SetActiveObject(this.control, null);
					if (this.inPlaceUiWindow != null)
					{
						this.inPlaceUiWindow.SetActiveObject(this.control, null);
					}
					int num2 = this.inPlaceFrame.SetBorderSpace(null);
					if (NativeMethods.Failed(num2) && num2 != -2147221491 && num2 != -2147221087 && num2 != -2147467263)
					{
						UnsafeNativeMethods.ThrowExceptionForHR(num2);
					}
					if (this.inPlaceUiWindow != null)
					{
						num2 = this.inPlaceFrame.SetBorderSpace(null);
						if (NativeMethods.Failed(num2) && num2 != -2147221491 && num2 != -2147221087 && num2 != -2147467263)
						{
							UnsafeNativeMethods.ThrowExceptionForHR(num2);
						}
					}
				}
			}

			// Token: 0x0600643B RID: 25659 RVA: 0x001737C4 File Offset: 0x001719C4
			internal void InPlaceDeactivate()
			{
				if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
				{
					return;
				}
				if (this.activeXState[Control.ActiveXImpl.uiActive])
				{
					this.UIDeactivate();
				}
				this.activeXState[Control.ActiveXImpl.inPlaceActive] = false;
				this.activeXState[Control.ActiveXImpl.inPlaceVisible] = false;
				UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
				if (oleInPlaceSite != null)
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						oleInPlaceSite.OnInPlaceDeactivate();
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				this.control.Visible = false;
				this.hwndParent = IntPtr.Zero;
				if (this.inPlaceUiWindow != null && UnsafeNativeMethods.IsComObject(this.inPlaceUiWindow))
				{
					UnsafeNativeMethods.ReleaseComObject(this.inPlaceUiWindow);
					this.inPlaceUiWindow = null;
				}
				if (this.inPlaceFrame != null && UnsafeNativeMethods.IsComObject(this.inPlaceFrame))
				{
					UnsafeNativeMethods.ReleaseComObject(this.inPlaceFrame);
					this.inPlaceFrame = null;
				}
			}

			// Token: 0x0600643C RID: 25660 RVA: 0x001738C0 File Offset: 0x00171AC0
			internal int IsDirty()
			{
				if (this.activeXState[Control.ActiveXImpl.isDirty])
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x0600643D RID: 25661 RVA: 0x001738D8 File Offset: 0x00171AD8
			private bool IsResourceProp(PropertyDescriptor prop)
			{
				TypeConverter converter = prop.Converter;
				Type[] array = new Type[]
				{
					typeof(string),
					typeof(byte[])
				};
				foreach (Type type in array)
				{
					if (converter.CanConvertTo(type) && converter.CanConvertFrom(type))
					{
						return false;
					}
				}
				return prop.GetValue(this.control) is ISerializable;
			}

			// Token: 0x0600643E RID: 25662 RVA: 0x0017394C File Offset: 0x00171B4C
			internal void Load(UnsafeNativeMethods.IStorage stg)
			{
				UnsafeNativeMethods.IStream stream = null;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					stream = stg.OpenStream(this.GetStreamName(), IntPtr.Zero, 16, 0);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147287038)
					{
						throw;
					}
					stream = stg.OpenStream(base.GetType().FullName, IntPtr.Zero, 16, 0);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				this.Load(stream);
				stream = null;
				if (UnsafeNativeMethods.IsComObject(stg))
				{
					UnsafeNativeMethods.ReleaseComObject(stg);
				}
			}

			// Token: 0x0600643F RID: 25663 RVA: 0x001739E4 File Offset: 0x00171BE4
			internal void Load(UnsafeNativeMethods.IStream stream)
			{
				Control.ActiveXImpl.PropertyBagStream propertyBagStream = new Control.ActiveXImpl.PropertyBagStream();
				propertyBagStream.Read(stream);
				this.Load(propertyBagStream, null);
				if (UnsafeNativeMethods.IsComObject(stream))
				{
					UnsafeNativeMethods.ReleaseComObject(stream);
				}
			}

			// Token: 0x06006440 RID: 25664 RVA: 0x00173A18 File Offset: 0x00171C18
			internal void Load(UnsafeNativeMethods.IPropertyBag pPropBag, UnsafeNativeMethods.IErrorLog pErrorLog)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.control, new Attribute[]
				{
					DesignerSerializationVisibilityAttribute.Visible
				});
				for (int i = 0; i < properties.Count; i++)
				{
					try
					{
						object obj = null;
						int hr = -2147467259;
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							hr = pPropBag.Read(properties[i].Name, ref obj, pErrorLog);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						if (NativeMethods.Succeeded(hr) && obj != null)
						{
							string text = null;
							int scode = 0;
							try
							{
								if (obj.GetType() != typeof(string))
								{
									obj = Convert.ToString(obj, CultureInfo.InvariantCulture);
								}
								if (this.IsResourceProp(properties[i]))
								{
									byte[] buffer = Convert.FromBase64String(obj.ToString());
									MemoryStream serializationStream = new MemoryStream(buffer);
									BinaryFormatter binaryFormatter = new BinaryFormatter();
									properties[i].SetValue(this.control, binaryFormatter.Deserialize(serializationStream));
								}
								else
								{
									TypeConverter converter = properties[i].Converter;
									object value = null;
									if (converter.CanConvertFrom(typeof(string)))
									{
										value = converter.ConvertFromInvariantString(obj.ToString());
									}
									else if (converter.CanConvertFrom(typeof(byte[])))
									{
										string text2 = obj.ToString();
										value = converter.ConvertFrom(null, CultureInfo.InvariantCulture, Control.ActiveXImpl.FromBase64WrappedString(text2));
									}
									properties[i].SetValue(this.control, value);
								}
							}
							catch (Exception ex)
							{
								text = ex.ToString();
								if (ex is ExternalException)
								{
									scode = ((ExternalException)ex).ErrorCode;
								}
								else
								{
									scode = -2147467259;
								}
							}
							if (text != null && pErrorLog != null)
							{
								NativeMethods.tagEXCEPINFO tagEXCEPINFO = new NativeMethods.tagEXCEPINFO();
								tagEXCEPINFO.bstrSource = this.control.GetType().FullName;
								tagEXCEPINFO.bstrDescription = text;
								tagEXCEPINFO.scode = scode;
								pErrorLog.AddError(properties[i].Name, tagEXCEPINFO);
							}
						}
					}
					catch (Exception ex2)
					{
						if (ClientUtils.IsSecurityOrCriticalException(ex2))
						{
							throw;
						}
					}
				}
				if (UnsafeNativeMethods.IsComObject(pPropBag))
				{
					UnsafeNativeMethods.ReleaseComObject(pPropBag);
				}
			}

			// Token: 0x06006441 RID: 25665 RVA: 0x00173C6C File Offset: 0x00171E6C
			private Control.AmbientProperty LookupAmbient(int dispid)
			{
				for (int i = 0; i < this.ambientProperties.Length; i++)
				{
					if (this.ambientProperties[i].DispID == dispid)
					{
						return this.ambientProperties[i];
					}
				}
				return this.ambientProperties[0];
			}

			// Token: 0x06006442 RID: 25666 RVA: 0x00173CB0 File Offset: 0x00171EB0
			internal IntPtr MergeRegion(IntPtr region)
			{
				if (this.clipRegion == IntPtr.Zero)
				{
					return region;
				}
				if (region == IntPtr.Zero)
				{
					return this.clipRegion;
				}
				IntPtr result;
				try
				{
					IntPtr intPtr = SafeNativeMethods.CreateRectRgn(0, 0, 0, 0);
					try
					{
						SafeNativeMethods.CombineRgn(new HandleRef(null, intPtr), new HandleRef(null, region), new HandleRef(this, this.clipRegion), 4);
						SafeNativeMethods.DeleteObject(new HandleRef(null, region));
					}
					catch
					{
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
						throw;
					}
					result = intPtr;
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					result = region;
				}
				return result;
			}

			// Token: 0x06006443 RID: 25667 RVA: 0x00173D60 File Offset: 0x00171F60
			private void CallParentPropertyChanged(Control control, string propName)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(propName);
				if (num <= 2626085950U)
				{
					if (num <= 777198197U)
					{
						if (num != 41545325U)
						{
							if (num != 777198197U)
							{
								return;
							}
							if (!(propName == "BackColor"))
							{
								return;
							}
							control.OnParentBackColorChanged(EventArgs.Empty);
							return;
						}
						else
						{
							if (!(propName == "BindingContext"))
							{
								return;
							}
							control.OnParentBindingContextChanged(EventArgs.Empty);
							return;
						}
					}
					else if (num != 1495943489U)
					{
						if (num != 2626085950U)
						{
							return;
						}
						if (!(propName == "Enabled"))
						{
							return;
						}
						control.OnParentEnabledChanged(EventArgs.Empty);
						return;
					}
					else
					{
						if (!(propName == "Visible"))
						{
							return;
						}
						control.OnParentVisibleChanged(EventArgs.Empty);
						return;
					}
				}
				else if (num <= 2936102910U)
				{
					if (num != 2809814704U)
					{
						if (num != 2936102910U)
						{
							return;
						}
						if (!(propName == "ForeColor"))
						{
							return;
						}
						control.OnParentForeColorChanged(EventArgs.Empty);
						return;
					}
					else
					{
						if (!(propName == "Font"))
						{
							return;
						}
						control.OnParentFontChanged(EventArgs.Empty);
						return;
					}
				}
				else if (num != 3049818181U)
				{
					if (num != 3770400898U)
					{
						return;
					}
					if (!(propName == "BackgroundImage"))
					{
						return;
					}
					control.OnParentBackgroundImageChanged(EventArgs.Empty);
					return;
				}
				else
				{
					if (!(propName == "RightToLeft"))
					{
						return;
					}
					control.OnParentRightToLeftChanged(EventArgs.Empty);
					return;
				}
			}

			// Token: 0x06006444 RID: 25668 RVA: 0x00173EA4 File Offset: 0x001720A4
			internal void OnAmbientPropertyChange(int dispID)
			{
				if (dispID != -1)
				{
					for (int i = 0; i < this.ambientProperties.Length; i++)
					{
						if (this.ambientProperties[i].DispID == dispID)
						{
							this.ambientProperties[i].ResetValue();
							this.CallParentPropertyChanged(this.control, this.ambientProperties[i].Name);
							return;
						}
					}
					object obj = new object();
					if (dispID != -713)
					{
						if (dispID == -710 && this.GetAmbientProperty(-710, ref obj))
						{
							this.activeXState[Control.ActiveXImpl.uiDead] = (bool)obj;
							return;
						}
					}
					else
					{
						IButtonControl buttonControl = this.control as IButtonControl;
						if (buttonControl != null && this.GetAmbientProperty(-713, ref obj))
						{
							buttonControl.NotifyDefault((bool)obj);
							return;
						}
					}
				}
				else
				{
					for (int j = 0; j < this.ambientProperties.Length; j++)
					{
						this.ambientProperties[j].ResetValue();
						this.CallParentPropertyChanged(this.control, this.ambientProperties[j].Name);
					}
				}
			}

			// Token: 0x06006445 RID: 25669 RVA: 0x00173FA8 File Offset: 0x001721A8
			internal void OnDocWindowActivate(int fActivate)
			{
				if (this.activeXState[Control.ActiveXImpl.uiActive] && fActivate != 0 && this.inPlaceFrame != null)
				{
					IntSecurity.UnmanagedCode.Assert();
					int num;
					try
					{
						num = this.inPlaceFrame.SetBorderSpace(null);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (NativeMethods.Failed(num) && num != -2147221087 && num != -2147467263)
					{
						UnsafeNativeMethods.ThrowExceptionForHR(num);
					}
				}
			}

			// Token: 0x06006446 RID: 25670 RVA: 0x00174020 File Offset: 0x00172220
			internal void OnFocus(bool focus)
			{
				if (this.activeXState[Control.ActiveXImpl.inPlaceActive] && this.clientSite is UnsafeNativeMethods.IOleControlSite)
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						((UnsafeNativeMethods.IOleControlSite)this.clientSite).OnFocus(focus ? 1 : 0);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				if (focus && this.activeXState[Control.ActiveXImpl.inPlaceActive] && !this.activeXState[Control.ActiveXImpl.uiActive])
				{
					this.InPlaceActivate(-4);
				}
			}

			// Token: 0x06006447 RID: 25671 RVA: 0x001740B8 File Offset: 0x001722B8
			private Point PixelToHiMetric(int x, int y)
			{
				return new Point
				{
					X = (Control.ActiveXImpl.hiMetricPerInch * x + (this.LogPixels.X >> 1)) / this.LogPixels.X,
					Y = (Control.ActiveXImpl.hiMetricPerInch * y + (this.LogPixels.Y >> 1)) / this.LogPixels.Y
				};
			}

			// Token: 0x06006448 RID: 25672 RVA: 0x0017412C File Offset: 0x0017232C
			internal void QuickActivate(UnsafeNativeMethods.tagQACONTAINER pQaContainer, UnsafeNativeMethods.tagQACONTROL pQaControl)
			{
				Control.AmbientProperty ambientProperty = this.LookupAmbient(-701);
				ambientProperty.Value = ColorTranslator.FromOle((int)pQaContainer.colorBack);
				ambientProperty = this.LookupAmbient(-704);
				ambientProperty.Value = ColorTranslator.FromOle((int)pQaContainer.colorFore);
				if (pQaContainer.pFont != null)
				{
					ambientProperty = this.LookupAmbient(-703);
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						IntPtr hfont = IntPtr.Zero;
						object pFont = pQaContainer.pFont;
						UnsafeNativeMethods.IFont font = (UnsafeNativeMethods.IFont)pFont;
						hfont = font.GetHFont();
						Font value = Font.FromHfont(hfont);
						ambientProperty.Value = value;
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
						ambientProperty.Value = null;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				pQaControl.cbSize = UnsafeNativeMethods.SizeOf(typeof(UnsafeNativeMethods.tagQACONTROL));
				this.SetClientSite(pQaContainer.pClientSite);
				if (pQaContainer.pAdviseSink != null)
				{
					this.SetAdvise(1, 0, (IAdviseSink)pQaContainer.pAdviseSink);
				}
				IntSecurity.UnmanagedCode.Assert();
				int dwMiscStatus;
				try
				{
					((UnsafeNativeMethods.IOleObject)this.control).GetMiscStatus(1, out dwMiscStatus);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				pQaControl.dwMiscStatus = dwMiscStatus;
				if (pQaContainer.pUnkEventSink != null && this.control is UserControl)
				{
					Type defaultEventsInterface = Control.ActiveXImpl.GetDefaultEventsInterface(this.control.GetType());
					if (defaultEventsInterface != null)
					{
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							Control.ActiveXImpl.AdviseHelper.AdviseConnectionPoint(this.control, pQaContainer.pUnkEventSink, defaultEventsInterface, out pQaControl.dwEventCookie);
						}
						catch (Exception ex2)
						{
							if (ClientUtils.IsSecurityOrCriticalException(ex2))
							{
								throw;
							}
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
				if (pQaContainer.pPropertyNotifySink != null && UnsafeNativeMethods.IsComObject(pQaContainer.pPropertyNotifySink))
				{
					UnsafeNativeMethods.ReleaseComObject(pQaContainer.pPropertyNotifySink);
				}
				if (pQaContainer.pUnkEventSink != null && UnsafeNativeMethods.IsComObject(pQaContainer.pUnkEventSink))
				{
					UnsafeNativeMethods.ReleaseComObject(pQaContainer.pUnkEventSink);
				}
			}

			// Token: 0x06006449 RID: 25673 RVA: 0x00174338 File Offset: 0x00172538
			private static Type GetDefaultEventsInterface(Type controlType)
			{
				Type type = null;
				object[] customAttributes = controlType.GetCustomAttributes(typeof(ComSourceInterfacesAttribute), false);
				if (customAttributes.Length != 0)
				{
					ComSourceInterfacesAttribute comSourceInterfacesAttribute = (ComSourceInterfacesAttribute)customAttributes[0];
					string text = comSourceInterfacesAttribute.Value.Split(new char[1])[0];
					type = controlType.Module.Assembly.GetType(text, false);
					if (type == null)
					{
						type = Type.GetType(text, false);
					}
				}
				return type;
			}

			// Token: 0x0600644A RID: 25674 RVA: 0x001743A0 File Offset: 0x001725A0
			internal void Save(UnsafeNativeMethods.IStorage stg, bool fSameAsLoad)
			{
				UnsafeNativeMethods.IStream stream = null;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					stream = stg.CreateStream(this.GetStreamName(), 4113, 0, 0);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				this.Save(stream, true);
				UnsafeNativeMethods.ReleaseComObject(stream);
			}

			// Token: 0x0600644B RID: 25675 RVA: 0x001743F4 File Offset: 0x001725F4
			internal void Save(UnsafeNativeMethods.IStream stream, bool fClearDirty)
			{
				Control.ActiveXImpl.PropertyBagStream propertyBagStream = new Control.ActiveXImpl.PropertyBagStream();
				this.Save(propertyBagStream, fClearDirty, false);
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					propertyBagStream.Write(stream);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (UnsafeNativeMethods.IsComObject(stream))
				{
					UnsafeNativeMethods.ReleaseComObject(stream);
				}
			}

			// Token: 0x0600644C RID: 25676 RVA: 0x00174448 File Offset: 0x00172648
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal void Save(UnsafeNativeMethods.IPropertyBag pPropBag, bool fClearDirty, bool fSaveAllProperties)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.control, new Attribute[]
				{
					DesignerSerializationVisibilityAttribute.Visible
				});
				for (int i = 0; i < properties.Count; i++)
				{
					if (fSaveAllProperties || properties[i].ShouldSerializeValue(this.control))
					{
						if (this.IsResourceProp(properties[i]))
						{
							MemoryStream memoryStream = new MemoryStream();
							BinaryFormatter binaryFormatter = new BinaryFormatter();
							binaryFormatter.Serialize(memoryStream, properties[i].GetValue(this.control));
							byte[] array = new byte[(int)memoryStream.Length];
							memoryStream.Position = 0L;
							memoryStream.Read(array, 0, array.Length);
							object obj = Convert.ToBase64String(array);
							pPropBag.Write(properties[i].Name, ref obj);
						}
						else
						{
							TypeConverter converter = properties[i].Converter;
							if (converter.CanConvertFrom(typeof(string)))
							{
								object obj = converter.ConvertToInvariantString(properties[i].GetValue(this.control));
								pPropBag.Write(properties[i].Name, ref obj);
							}
							else if (converter.CanConvertFrom(typeof(byte[])))
							{
								byte[] inArray = (byte[])converter.ConvertTo(null, CultureInfo.InvariantCulture, properties[i].GetValue(this.control), typeof(byte[]));
								object obj = Convert.ToBase64String(inArray);
								pPropBag.Write(properties[i].Name, ref obj);
							}
						}
					}
				}
				if (UnsafeNativeMethods.IsComObject(pPropBag))
				{
					UnsafeNativeMethods.ReleaseComObject(pPropBag);
				}
				if (fClearDirty)
				{
					this.activeXState[Control.ActiveXImpl.isDirty] = false;
				}
			}

			// Token: 0x0600644D RID: 25677 RVA: 0x001745F4 File Offset: 0x001727F4
			private void SendOnSave()
			{
				int count = this.adviseList.Count;
				IntSecurity.UnmanagedCode.Assert();
				for (int i = 0; i < count; i++)
				{
					IAdviseSink adviseSink = (IAdviseSink)this.adviseList[i];
					adviseSink.OnSave();
				}
			}

			// Token: 0x0600644E RID: 25678 RVA: 0x0017463C File Offset: 0x0017283C
			internal void SetAdvise(int aspects, int advf, IAdviseSink pAdvSink)
			{
				if ((aspects & 1) == 0)
				{
					Control.ActiveXImpl.ThrowHr(-2147221397);
				}
				this.activeXState[Control.ActiveXImpl.viewAdvisePrimeFirst] = ((advf & 2) != 0);
				this.activeXState[Control.ActiveXImpl.viewAdviseOnlyOnce] = ((advf & 4) != 0);
				if (this.viewAdviseSink != null && UnsafeNativeMethods.IsComObject(this.viewAdviseSink))
				{
					UnsafeNativeMethods.ReleaseComObject(this.viewAdviseSink);
				}
				this.viewAdviseSink = pAdvSink;
				if (this.activeXState[Control.ActiveXImpl.viewAdvisePrimeFirst])
				{
					this.ViewChanged();
				}
			}

			// Token: 0x0600644F RID: 25679 RVA: 0x001746CC File Offset: 0x001728CC
			internal void SetClientSite(UnsafeNativeMethods.IOleClientSite value)
			{
				if (this.clientSite != null)
				{
					if (value == null)
					{
						Control.ActiveXImpl.globalActiveXCount--;
						if (Control.ActiveXImpl.globalActiveXCount == 0 && this.IsIE)
						{
							new PermissionSet(PermissionState.Unrestricted).Assert();
							try
							{
								MethodInfo method = typeof(SystemEvents).GetMethod("Shutdown", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, new Type[0], new ParameterModifier[0]);
								if (method != null)
								{
									method.Invoke(null, null);
								}
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
						}
					}
					if (UnsafeNativeMethods.IsComObject(this.clientSite))
					{
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							Marshal.FinalReleaseComObject(this.clientSite);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
				this.clientSite = value;
				if (this.clientSite != null)
				{
					this.control.Site = new Control.AxSourcingSite(this.control, this.clientSite, "ControlAxSourcingSite");
				}
				else
				{
					this.control.Site = null;
				}
				object obj = new object();
				if (this.GetAmbientProperty(-710, ref obj))
				{
					this.activeXState[Control.ActiveXImpl.uiDead] = (bool)obj;
				}
				if (this.control is IButtonControl && this.GetAmbientProperty(-710, ref obj))
				{
					((IButtonControl)this.control).NotifyDefault((bool)obj);
				}
				if (this.clientSite == null)
				{
					if (this.accelTable != IntPtr.Zero)
					{
						UnsafeNativeMethods.DestroyAcceleratorTable(new HandleRef(this, this.accelTable));
						this.accelTable = IntPtr.Zero;
						this.accelCount = -1;
					}
					if (this.IsIE)
					{
						this.control.Dispose();
					}
				}
				else
				{
					Control.ActiveXImpl.globalActiveXCount++;
					if (Control.ActiveXImpl.globalActiveXCount == 1 && this.IsIE)
					{
						new PermissionSet(PermissionState.Unrestricted).Assert();
						try
						{
							MethodInfo method2 = typeof(SystemEvents).GetMethod("Startup", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, new Type[0], new ParameterModifier[0]);
							if (method2 != null)
							{
								method2.Invoke(null, null);
							}
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
				this.control.OnTopMostActiveXParentChanged(EventArgs.Empty);
			}

			// Token: 0x06006450 RID: 25680 RVA: 0x00174908 File Offset: 0x00172B08
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal void SetExtent(int dwDrawAspect, NativeMethods.tagSIZEL pSizel)
			{
				if ((dwDrawAspect & 1) != 0)
				{
					if (this.activeXState[Control.ActiveXImpl.changingExtents])
					{
						return;
					}
					this.activeXState[Control.ActiveXImpl.changingExtents] = true;
					try
					{
						Size size = new Size(this.HiMetricToPixel(pSizel.cx, pSizel.cy));
						if (this.activeXState[Control.ActiveXImpl.inPlaceActive])
						{
							UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
							if (oleInPlaceSite != null)
							{
								Rectangle bounds = this.control.Bounds;
								bounds.Location = new Point(bounds.X, bounds.Y);
								Size size2 = new Size(size.Width, size.Height);
								bounds.Width = size2.Width;
								bounds.Height = size2.Height;
								oleInPlaceSite.OnPosRectChange(NativeMethods.COMRECT.FromXYWH(bounds.X, bounds.Y, bounds.Width, bounds.Height));
							}
						}
						this.control.Size = size;
						if (!this.control.Size.Equals(size))
						{
							this.activeXState[Control.ActiveXImpl.isDirty] = true;
							if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
							{
								this.ViewChanged();
							}
							if (!this.activeXState[Control.ActiveXImpl.inPlaceActive] && this.clientSite != null)
							{
								this.clientSite.RequestNewObjectLayout();
							}
						}
						return;
					}
					finally
					{
						this.activeXState[Control.ActiveXImpl.changingExtents] = false;
					}
				}
				Control.ActiveXImpl.ThrowHr(-2147221397);
			}

			// Token: 0x06006451 RID: 25681 RVA: 0x00174AB4 File Offset: 0x00172CB4
			private void SetInPlaceVisible(bool visible)
			{
				this.activeXState[Control.ActiveXImpl.inPlaceVisible] = visible;
				this.control.Visible = visible;
			}

			// Token: 0x06006452 RID: 25682 RVA: 0x00174AD4 File Offset: 0x00172CD4
			internal void SetObjectRects(NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect)
			{
				Rectangle rectangle = Rectangle.FromLTRB(lprcPosRect.left, lprcPosRect.top, lprcPosRect.right, lprcPosRect.bottom);
				if (this.activeXState[Control.ActiveXImpl.adjustingRect])
				{
					this.adjustRect.left = rectangle.X;
					this.adjustRect.top = rectangle.Y;
					this.adjustRect.right = rectangle.Width + rectangle.X;
					this.adjustRect.bottom = rectangle.Height + rectangle.Y;
				}
				else
				{
					this.activeXState[Control.ActiveXImpl.adjustingRect] = true;
					try
					{
						this.control.Bounds = rectangle;
					}
					finally
					{
						this.activeXState[Control.ActiveXImpl.adjustingRect] = false;
					}
				}
				bool flag = false;
				if (this.clipRegion != IntPtr.Zero)
				{
					this.clipRegion = IntPtr.Zero;
					flag = true;
				}
				if (lprcClipRect != null)
				{
					Rectangle b = Rectangle.FromLTRB(lprcClipRect.left, lprcClipRect.top, lprcClipRect.right, lprcClipRect.bottom);
					Rectangle rectangle2;
					if (!b.IsEmpty)
					{
						rectangle2 = Rectangle.Intersect(rectangle, b);
					}
					else
					{
						rectangle2 = rectangle;
					}
					if (!rectangle2.Equals(rectangle))
					{
						NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height);
						IntPtr parent = UnsafeNativeMethods.GetParent(new HandleRef(this.control, this.control.Handle));
						UnsafeNativeMethods.MapWindowPoints(new HandleRef(null, parent), new HandleRef(this.control, this.control.Handle), ref rect, 2);
						this.clipRegion = SafeNativeMethods.CreateRectRgn(rect.left, rect.top, rect.right, rect.bottom);
						flag = true;
					}
				}
				if (flag && this.control.IsHandleCreated)
				{
					IntPtr handle = this.clipRegion;
					Region region = this.control.Region;
					if (region != null)
					{
						IntPtr hrgn = this.control.GetHRgn(region);
						handle = this.MergeRegion(hrgn);
					}
					UnsafeNativeMethods.SetWindowRgn(new HandleRef(this.control, this.control.Handle), new HandleRef(this, handle), SafeNativeMethods.IsWindowVisible(new HandleRef(this.control, this.control.Handle)));
				}
				this.control.Invalidate();
			}

			// Token: 0x06006453 RID: 25683 RVA: 0x00174D3C File Offset: 0x00172F3C
			internal static void ThrowHr(int hr)
			{
				ExternalException ex = new ExternalException(SR.GetString("ExternalException"), hr);
				throw ex;
			}

			// Token: 0x06006454 RID: 25684 RVA: 0x00174D5C File Offset: 0x00172F5C
			internal int TranslateAccelerator(ref NativeMethods.MSG lpmsg)
			{
				bool flag = false;
				switch (lpmsg.message)
				{
				case 256:
				case 258:
				case 260:
				case 262:
					flag = true;
					break;
				}
				Message message = Message.Create(lpmsg.hwnd, lpmsg.message, lpmsg.wParam, lpmsg.lParam);
				if (flag)
				{
					Control control = Control.FromChildHandleInternal(lpmsg.hwnd);
					if (control != null && (this.control == control || this.control.Contains(control)))
					{
						switch (Control.PreProcessControlMessageInternal(control, ref message))
						{
						case PreProcessControlState.MessageProcessed:
							lpmsg.message = message.Msg;
							lpmsg.wParam = message.WParam;
							lpmsg.lParam = message.LParam;
							return 0;
						case PreProcessControlState.MessageNeeded:
							UnsafeNativeMethods.TranslateMessage(ref lpmsg);
							if (SafeNativeMethods.IsWindowUnicode(new HandleRef(null, lpmsg.hwnd)))
							{
								UnsafeNativeMethods.DispatchMessageW(ref lpmsg);
							}
							else
							{
								UnsafeNativeMethods.DispatchMessageA(ref lpmsg);
							}
							return 0;
						}
					}
				}
				int result = 1;
				UnsafeNativeMethods.IOleControlSite oleControlSite = this.clientSite as UnsafeNativeMethods.IOleControlSite;
				if (oleControlSite != null)
				{
					int num = 0;
					if (UnsafeNativeMethods.GetKeyState(16) < 0)
					{
						num |= 1;
					}
					if (UnsafeNativeMethods.GetKeyState(17) < 0)
					{
						num |= 2;
					}
					if (UnsafeNativeMethods.GetKeyState(18) < 0)
					{
						num |= 4;
					}
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						result = oleControlSite.TranslateAccelerator(ref lpmsg, num);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return result;
			}

			// Token: 0x06006455 RID: 25685 RVA: 0x00174ED8 File Offset: 0x001730D8
			internal int UIDeactivate()
			{
				if (!this.activeXState[Control.ActiveXImpl.uiActive])
				{
					return 0;
				}
				this.activeXState[Control.ActiveXImpl.uiActive] = false;
				if (this.inPlaceUiWindow != null)
				{
					this.inPlaceUiWindow.SetActiveObject(null, null);
				}
				IntSecurity.UnmanagedCode.Assert();
				this.inPlaceFrame.SetActiveObject(null, null);
				UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
				if (oleInPlaceSite != null)
				{
					oleInPlaceSite.OnUIDeactivate(0);
				}
				return 0;
			}

			// Token: 0x06006456 RID: 25686 RVA: 0x00174F50 File Offset: 0x00173150
			internal void Unadvise(int dwConnection)
			{
				if (dwConnection > this.adviseList.Count || this.adviseList[dwConnection - 1] == null)
				{
					Control.ActiveXImpl.ThrowHr(-2147221500);
				}
				IAdviseSink adviseSink = (IAdviseSink)this.adviseList[dwConnection - 1];
				this.adviseList.RemoveAt(dwConnection - 1);
				if (adviseSink != null && UnsafeNativeMethods.IsComObject(adviseSink))
				{
					UnsafeNativeMethods.ReleaseComObject(adviseSink);
				}
			}

			// Token: 0x06006457 RID: 25687 RVA: 0x00174FBC File Offset: 0x001731BC
			internal void UpdateBounds(ref int x, ref int y, ref int width, ref int height, int flags)
			{
				if (!this.activeXState[Control.ActiveXImpl.adjustingRect] && this.activeXState[Control.ActiveXImpl.inPlaceVisible])
				{
					UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
					if (oleInPlaceSite != null)
					{
						NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
						if ((flags & 2) != 0)
						{
							comrect.left = this.control.Left;
							comrect.top = this.control.Top;
						}
						else
						{
							comrect.left = x;
							comrect.top = y;
						}
						if ((flags & 1) != 0)
						{
							comrect.right = comrect.left + this.control.Width;
							comrect.bottom = comrect.top + this.control.Height;
						}
						else
						{
							comrect.right = comrect.left + width;
							comrect.bottom = comrect.top + height;
						}
						this.adjustRect = comrect;
						this.activeXState[Control.ActiveXImpl.adjustingRect] = true;
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							oleInPlaceSite.OnPosRectChange(comrect);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
							this.adjustRect = null;
							this.activeXState[Control.ActiveXImpl.adjustingRect] = false;
						}
						if ((flags & 2) == 0)
						{
							x = comrect.left;
							y = comrect.top;
						}
						if ((flags & 1) == 0)
						{
							width = comrect.right - comrect.left;
							height = comrect.bottom - comrect.top;
						}
					}
				}
			}

			// Token: 0x06006458 RID: 25688 RVA: 0x00175134 File Offset: 0x00173334
			internal void UpdateAccelTable()
			{
				this.accelCount = -1;
				UnsafeNativeMethods.IOleControlSite oleControlSite = this.clientSite as UnsafeNativeMethods.IOleControlSite;
				if (oleControlSite != null)
				{
					IntSecurity.UnmanagedCode.Assert();
					oleControlSite.OnControlInfoChanged();
				}
			}

			// Token: 0x06006459 RID: 25689 RVA: 0x00175168 File Offset: 0x00173368
			internal void ViewChangedInternal()
			{
				this.ViewChanged();
			}

			// Token: 0x0600645A RID: 25690 RVA: 0x00175170 File Offset: 0x00173370
			private void ViewChanged()
			{
				if (this.viewAdviseSink != null && !this.activeXState[Control.ActiveXImpl.saving])
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						this.viewAdviseSink.OnViewChange(1, -1);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (this.activeXState[Control.ActiveXImpl.viewAdviseOnlyOnce])
					{
						if (UnsafeNativeMethods.IsComObject(this.viewAdviseSink))
						{
							UnsafeNativeMethods.ReleaseComObject(this.viewAdviseSink);
						}
						this.viewAdviseSink = null;
					}
				}
			}

			// Token: 0x0600645B RID: 25691 RVA: 0x001751F8 File Offset: 0x001733F8
			void IWindowTarget.OnHandleChange(IntPtr newHandle)
			{
				this.controlWindowTarget.OnHandleChange(newHandle);
			}

			// Token: 0x0600645C RID: 25692 RVA: 0x00175208 File Offset: 0x00173408
			void IWindowTarget.OnMessage(ref Message m)
			{
				if (this.activeXState[Control.ActiveXImpl.uiDead])
				{
					if (m.Msg >= 512 && m.Msg <= 522)
					{
						return;
					}
					if (m.Msg >= 161 && m.Msg <= 169)
					{
						return;
					}
					if (m.Msg >= 256 && m.Msg <= 264)
					{
						return;
					}
				}
				IntSecurity.UnmanagedCode.Assert();
				this.controlWindowTarget.OnMessage(ref m);
			}

			// Token: 0x04003961 RID: 14689
			private static readonly int hiMetricPerInch = 2540;

			// Token: 0x04003962 RID: 14690
			private static readonly int viewAdviseOnlyOnce = BitVector32.CreateMask();

			// Token: 0x04003963 RID: 14691
			private static readonly int viewAdvisePrimeFirst = BitVector32.CreateMask(Control.ActiveXImpl.viewAdviseOnlyOnce);

			// Token: 0x04003964 RID: 14692
			private static readonly int eventsFrozen = BitVector32.CreateMask(Control.ActiveXImpl.viewAdvisePrimeFirst);

			// Token: 0x04003965 RID: 14693
			private static readonly int changingExtents = BitVector32.CreateMask(Control.ActiveXImpl.eventsFrozen);

			// Token: 0x04003966 RID: 14694
			private static readonly int saving = BitVector32.CreateMask(Control.ActiveXImpl.changingExtents);

			// Token: 0x04003967 RID: 14695
			private static readonly int isDirty = BitVector32.CreateMask(Control.ActiveXImpl.saving);

			// Token: 0x04003968 RID: 14696
			private static readonly int inPlaceActive = BitVector32.CreateMask(Control.ActiveXImpl.isDirty);

			// Token: 0x04003969 RID: 14697
			private static readonly int inPlaceVisible = BitVector32.CreateMask(Control.ActiveXImpl.inPlaceActive);

			// Token: 0x0400396A RID: 14698
			private static readonly int uiActive = BitVector32.CreateMask(Control.ActiveXImpl.inPlaceVisible);

			// Token: 0x0400396B RID: 14699
			private static readonly int uiDead = BitVector32.CreateMask(Control.ActiveXImpl.uiActive);

			// Token: 0x0400396C RID: 14700
			private static readonly int adjustingRect = BitVector32.CreateMask(Control.ActiveXImpl.uiDead);

			// Token: 0x0400396D RID: 14701
			private static Point logPixels = Point.Empty;

			// Token: 0x0400396E RID: 14702
			private static NativeMethods.tagOLEVERB[] axVerbs;

			// Token: 0x0400396F RID: 14703
			private static int globalActiveXCount = 0;

			// Token: 0x04003970 RID: 14704
			private static bool checkedIE;

			// Token: 0x04003971 RID: 14705
			private static bool isIE;

			// Token: 0x04003972 RID: 14706
			private Control control;

			// Token: 0x04003973 RID: 14707
			private IWindowTarget controlWindowTarget;

			// Token: 0x04003974 RID: 14708
			private IntPtr clipRegion;

			// Token: 0x04003975 RID: 14709
			private UnsafeNativeMethods.IOleClientSite clientSite;

			// Token: 0x04003976 RID: 14710
			private UnsafeNativeMethods.IOleInPlaceUIWindow inPlaceUiWindow;

			// Token: 0x04003977 RID: 14711
			private UnsafeNativeMethods.IOleInPlaceFrame inPlaceFrame;

			// Token: 0x04003978 RID: 14712
			private ArrayList adviseList;

			// Token: 0x04003979 RID: 14713
			private IAdviseSink viewAdviseSink;

			// Token: 0x0400397A RID: 14714
			private BitVector32 activeXState;

			// Token: 0x0400397B RID: 14715
			private Control.AmbientProperty[] ambientProperties;

			// Token: 0x0400397C RID: 14716
			private IntPtr hwndParent;

			// Token: 0x0400397D RID: 14717
			private IntPtr accelTable;

			// Token: 0x0400397E RID: 14718
			private short accelCount = -1;

			// Token: 0x0400397F RID: 14719
			private NativeMethods.COMRECT adjustRect;

			// Token: 0x020008B7 RID: 2231
			internal static class AdviseHelper
			{
				// Token: 0x060072D3 RID: 29395 RVA: 0x001A4710 File Offset: 0x001A2910
				public static bool AdviseConnectionPoint(object connectionPoint, object sink, Type eventInterface, out int cookie)
				{
					bool result;
					using (Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer comConnectionPointContainer = new Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer(connectionPoint, true))
					{
						result = Control.ActiveXImpl.AdviseHelper.AdviseConnectionPoint(comConnectionPointContainer, sink, eventInterface, out cookie);
					}
					return result;
				}

				// Token: 0x060072D4 RID: 29396 RVA: 0x001A474C File Offset: 0x001A294C
				internal static bool AdviseConnectionPoint(Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer cpc, object sink, Type eventInterface, out int cookie)
				{
					bool result;
					using (Control.ActiveXImpl.AdviseHelper.ComConnectionPoint comConnectionPoint = cpc.FindConnectionPoint(eventInterface))
					{
						using (Control.ActiveXImpl.AdviseHelper.SafeIUnknown safeIUnknown = new Control.ActiveXImpl.AdviseHelper.SafeIUnknown(sink, true))
						{
							result = comConnectionPoint.Advise(safeIUnknown.DangerousGetHandle(), out cookie);
						}
					}
					return result;
				}

				// Token: 0x02000984 RID: 2436
				internal class SafeIUnknown : SafeHandle
				{
					// Token: 0x060075B2 RID: 30130 RVA: 0x001A9EC0 File Offset: 0x001A80C0
					public SafeIUnknown(object obj, bool addRefIntPtr) : this(obj, addRefIntPtr, Guid.Empty)
					{
					}

					// Token: 0x060075B3 RID: 30131 RVA: 0x001A9ED0 File Offset: 0x001A80D0
					public SafeIUnknown(object obj, bool addRefIntPtr, Guid iid) : base(IntPtr.Zero, true)
					{
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
						}
						finally
						{
							IntPtr intPtr;
							if (obj is IntPtr)
							{
								intPtr = (IntPtr)obj;
								if (addRefIntPtr)
								{
									Marshal.AddRef(intPtr);
								}
							}
							else
							{
								intPtr = Marshal.GetIUnknownForObject(obj);
							}
							if (iid != Guid.Empty)
							{
								IntPtr pUnk = intPtr;
								try
								{
									intPtr = Control.ActiveXImpl.AdviseHelper.SafeIUnknown.InternalQueryInterface(intPtr, ref iid);
								}
								finally
								{
									Marshal.Release(pUnk);
								}
							}
							this.handle = intPtr;
						}
					}

					// Token: 0x060075B4 RID: 30132 RVA: 0x001A9F58 File Offset: 0x001A8158
					private static IntPtr InternalQueryInterface(IntPtr pUnk, ref Guid iid)
					{
						IntPtr intPtr;
						if (Marshal.QueryInterface(pUnk, ref iid, out intPtr) != 0 || intPtr == IntPtr.Zero)
						{
							throw new InvalidCastException(SR.GetString("AxInterfaceNotSupported"));
						}
						return intPtr;
					}

					// Token: 0x17001B04 RID: 6916
					// (get) Token: 0x060075B5 RID: 30133 RVA: 0x001A9F90 File Offset: 0x001A8190
					public sealed override bool IsInvalid
					{
						get
						{
							return base.IsClosed || IntPtr.Zero == this.handle;
						}
					}

					// Token: 0x060075B6 RID: 30134 RVA: 0x001A9FAC File Offset: 0x001A81AC
					protected sealed override bool ReleaseHandle()
					{
						IntPtr handle = this.handle;
						this.handle = IntPtr.Zero;
						if (IntPtr.Zero != handle)
						{
							Marshal.Release(handle);
						}
						return true;
					}

					// Token: 0x060075B7 RID: 30135 RVA: 0x001A9FE0 File Offset: 0x001A81E0
					protected V LoadVtable<V>()
					{
						IntPtr ptr = Marshal.ReadIntPtr(this.handle, 0);
						return (V)((object)Marshal.PtrToStructure(ptr, typeof(V)));
					}
				}

				// Token: 0x02000985 RID: 2437
				internal sealed class ComConnectionPointContainer : Control.ActiveXImpl.AdviseHelper.SafeIUnknown
				{
					// Token: 0x060075B8 RID: 30136 RVA: 0x001AA00F File Offset: 0x001A820F
					public ComConnectionPointContainer(object obj, bool addRefIntPtr) : base(obj, addRefIntPtr, typeof(IConnectionPointContainer).GUID)
					{
						this.vtbl = base.LoadVtable<Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.VTABLE>();
					}

					// Token: 0x060075B9 RID: 30137 RVA: 0x001AA034 File Offset: 0x001A8234
					public Control.ActiveXImpl.AdviseHelper.ComConnectionPoint FindConnectionPoint(Type eventInterface)
					{
						Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.FindConnectionPointD findConnectionPointD = (Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.FindConnectionPointD)Marshal.GetDelegateForFunctionPointer(this.vtbl.FindConnectionPointPtr, typeof(Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.FindConnectionPointD));
						IntPtr zero = IntPtr.Zero;
						Guid guid = eventInterface.GUID;
						if (findConnectionPointD(this.handle, ref guid, out zero) != 0 || zero == IntPtr.Zero)
						{
							throw new ArgumentException(SR.GetString("AXNoConnectionPoint", new object[]
							{
								eventInterface.Name
							}));
						}
						return new Control.ActiveXImpl.AdviseHelper.ComConnectionPoint(zero, false);
					}

					// Token: 0x040047DC RID: 18396
					private Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.VTABLE vtbl;

					// Token: 0x02000988 RID: 2440
					[StructLayout(LayoutKind.Sequential)]
					private class VTABLE
					{
						// Token: 0x040047DF RID: 18399
						public IntPtr QueryInterfacePtr;

						// Token: 0x040047E0 RID: 18400
						public IntPtr AddRefPtr;

						// Token: 0x040047E1 RID: 18401
						public IntPtr ReleasePtr;

						// Token: 0x040047E2 RID: 18402
						public IntPtr EnumConnectionPointsPtr;

						// Token: 0x040047E3 RID: 18403
						public IntPtr FindConnectionPointPtr;
					}

					// Token: 0x02000989 RID: 2441
					// (Invoke) Token: 0x060075BE RID: 30142
					[UnmanagedFunctionPointer(CallingConvention.StdCall)]
					private delegate int FindConnectionPointD(IntPtr This, ref Guid iid, out IntPtr ppv);
				}

				// Token: 0x02000986 RID: 2438
				internal sealed class ComConnectionPoint : Control.ActiveXImpl.AdviseHelper.SafeIUnknown
				{
					// Token: 0x060075BA RID: 30138 RVA: 0x001AA0BA File Offset: 0x001A82BA
					public ComConnectionPoint(object obj, bool addRefIntPtr) : base(obj, addRefIntPtr, typeof(IConnectionPoint).GUID)
					{
						this.vtbl = base.LoadVtable<Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.VTABLE>();
					}

					// Token: 0x060075BB RID: 30139 RVA: 0x001AA0E0 File Offset: 0x001A82E0
					public bool Advise(IntPtr punkEventSink, out int cookie)
					{
						Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.AdviseD adviseD = (Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.AdviseD)Marshal.GetDelegateForFunctionPointer(this.vtbl.AdvisePtr, typeof(Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.AdviseD));
						return adviseD(this.handle, punkEventSink, out cookie) == 0;
					}

					// Token: 0x040047DD RID: 18397
					private Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.VTABLE vtbl;

					// Token: 0x0200098A RID: 2442
					[StructLayout(LayoutKind.Sequential)]
					private class VTABLE
					{
						// Token: 0x040047E4 RID: 18404
						public IntPtr QueryInterfacePtr;

						// Token: 0x040047E5 RID: 18405
						public IntPtr AddRefPtr;

						// Token: 0x040047E6 RID: 18406
						public IntPtr ReleasePtr;

						// Token: 0x040047E7 RID: 18407
						public IntPtr GetConnectionInterfacePtr;

						// Token: 0x040047E8 RID: 18408
						public IntPtr GetConnectionPointContainterPtr;

						// Token: 0x040047E9 RID: 18409
						public IntPtr AdvisePtr;

						// Token: 0x040047EA RID: 18410
						public IntPtr UnadvisePtr;

						// Token: 0x040047EB RID: 18411
						public IntPtr EnumConnectionsPtr;
					}

					// Token: 0x0200098B RID: 2443
					// (Invoke) Token: 0x060075C3 RID: 30147
					[UnmanagedFunctionPointer(CallingConvention.StdCall)]
					private delegate int AdviseD(IntPtr This, IntPtr punkEventSink, out int cookie);
				}
			}

			// Token: 0x020008B8 RID: 2232
			private class PropertyBagStream : UnsafeNativeMethods.IPropertyBag
			{
				// Token: 0x060072D5 RID: 29397 RVA: 0x001A47AC File Offset: 0x001A29AC
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				internal void Read(UnsafeNativeMethods.IStream istream)
				{
					Stream stream = new DataStreamFromComStream(istream);
					byte[] array = new byte[4096];
					int num = 0;
					int num2 = stream.Read(array, num, 4096);
					int num3 = num2;
					while (num2 == 4096)
					{
						byte[] array2 = new byte[array.Length + 4096];
						Array.Copy(array, array2, array.Length);
						array = array2;
						num += 4096;
						num2 = stream.Read(array, num, 4096);
						num3 += num2;
					}
					stream = new MemoryStream(array);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					try
					{
						this.bag = (Hashtable)binaryFormatter.Deserialize(stream);
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
						this.bag = new Hashtable();
					}
				}

				// Token: 0x060072D6 RID: 29398 RVA: 0x001A4870 File Offset: 0x001A2A70
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				int UnsafeNativeMethods.IPropertyBag.Read(string pszPropName, ref object pVar, UnsafeNativeMethods.IErrorLog pErrorLog)
				{
					if (!this.bag.Contains(pszPropName))
					{
						return -2147024809;
					}
					pVar = this.bag[pszPropName];
					return 0;
				}

				// Token: 0x060072D7 RID: 29399 RVA: 0x001A4895 File Offset: 0x001A2A95
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				int UnsafeNativeMethods.IPropertyBag.Write(string pszPropName, ref object pVar)
				{
					this.bag[pszPropName] = pVar;
					return 0;
				}

				// Token: 0x060072D8 RID: 29400 RVA: 0x001A48A8 File Offset: 0x001A2AA8
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				internal void Write(UnsafeNativeMethods.IStream istream)
				{
					Stream serializationStream = new DataStreamFromComStream(istream);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(serializationStream, this.bag);
				}

				// Token: 0x04004530 RID: 17712
				private Hashtable bag = new Hashtable();
			}
		}

		// Token: 0x02000639 RID: 1593
		private class AxSourcingSite : ISite, IServiceProvider
		{
			// Token: 0x0600645E RID: 25694 RVA: 0x00175357 File Offset: 0x00173557
			internal AxSourcingSite(IComponent component, UnsafeNativeMethods.IOleClientSite clientSite, string name)
			{
				this.component = component;
				this.clientSite = clientSite;
				this.name = name;
			}

			// Token: 0x17001574 RID: 5492
			// (get) Token: 0x0600645F RID: 25695 RVA: 0x00175374 File Offset: 0x00173574
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17001575 RID: 5493
			// (get) Token: 0x06006460 RID: 25696 RVA: 0x00015ECC File Offset: 0x000140CC
			public IContainer Container
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006461 RID: 25697 RVA: 0x0017537C File Offset: 0x0017357C
			public object GetService(Type service)
			{
				object result = null;
				if (service == typeof(HtmlDocument))
				{
					UnsafeNativeMethods.IOleContainer oleContainer;
					int container;
					try
					{
						IntSecurity.UnmanagedCode.Assert();
						container = this.clientSite.GetContainer(out oleContainer);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (NativeMethods.Succeeded(container) && oleContainer is UnsafeNativeMethods.IHTMLDocument)
					{
						if (this.shimManager == null)
						{
							this.shimManager = new HtmlShimManager();
						}
						result = new HtmlDocument(this.shimManager, oleContainer as UnsafeNativeMethods.IHTMLDocument);
					}
				}
				else if (this.clientSite.GetType().IsAssignableFrom(service))
				{
					IntSecurity.UnmanagedCode.Demand();
					result = this.clientSite;
				}
				return result;
			}

			// Token: 0x17001576 RID: 5494
			// (get) Token: 0x06006462 RID: 25698 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001577 RID: 5495
			// (get) Token: 0x06006463 RID: 25699 RVA: 0x0017542C File Offset: 0x0017362C
			// (set) Token: 0x06006464 RID: 25700 RVA: 0x00175434 File Offset: 0x00173634
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					if (value == null || this.name == null)
					{
						this.name = value;
					}
				}
			}

			// Token: 0x04003980 RID: 14720
			private IComponent component;

			// Token: 0x04003981 RID: 14721
			private UnsafeNativeMethods.IOleClientSite clientSite;

			// Token: 0x04003982 RID: 14722
			private string name;

			// Token: 0x04003983 RID: 14723
			private HtmlShimManager shimManager;
		}

		// Token: 0x0200063A RID: 1594
		private class ActiveXFontMarshaler : ICustomMarshaler
		{
			// Token: 0x06006465 RID: 25701 RVA: 0x000072B6 File Offset: 0x000054B6
			public void CleanUpManagedData(object obj)
			{
			}

			// Token: 0x06006466 RID: 25702 RVA: 0x00175448 File Offset: 0x00173648
			public void CleanUpNativeData(IntPtr pObj)
			{
				Marshal.Release(pObj);
			}

			// Token: 0x06006467 RID: 25703 RVA: 0x00175451 File Offset: 0x00173651
			internal static ICustomMarshaler GetInstance(string cookie)
			{
				if (Control.ActiveXFontMarshaler.instance == null)
				{
					Control.ActiveXFontMarshaler.instance = new Control.ActiveXFontMarshaler();
				}
				return Control.ActiveXFontMarshaler.instance;
			}

			// Token: 0x06006468 RID: 25704 RVA: 0x00015ECF File Offset: 0x000140CF
			public int GetNativeDataSize()
			{
				return -1;
			}

			// Token: 0x06006469 RID: 25705 RVA: 0x0017546C File Offset: 0x0017366C
			public IntPtr MarshalManagedToNative(object obj)
			{
				Font font = (Font)obj;
				NativeMethods.tagFONTDESC tagFONTDESC = new NativeMethods.tagFONTDESC();
				NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
				IntSecurity.ObjectFromWin32Handle.Assert();
				try
				{
					font.ToLogFont(logfont);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				tagFONTDESC.lpstrName = font.Name;
				tagFONTDESC.cySize = (long)(font.SizeInPoints * 10000f);
				tagFONTDESC.sWeight = (short)logfont.lfWeight;
				tagFONTDESC.sCharset = (short)logfont.lfCharSet;
				tagFONTDESC.fItalic = font.Italic;
				tagFONTDESC.fUnderline = font.Underline;
				tagFONTDESC.fStrikethrough = font.Strikeout;
				Guid guid = typeof(UnsafeNativeMethods.IFont).GUID;
				UnsafeNativeMethods.IFont o = UnsafeNativeMethods.OleCreateFontIndirect(tagFONTDESC, ref guid);
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(o);
				IntPtr result;
				int num = Marshal.QueryInterface(iunknownForObject, ref guid, out result);
				Marshal.Release(iunknownForObject);
				if (NativeMethods.Failed(num))
				{
					Marshal.ThrowExceptionForHR(num);
				}
				return result;
			}

			// Token: 0x0600646A RID: 25706 RVA: 0x0017555C File Offset: 0x0017375C
			public object MarshalNativeToManaged(IntPtr pObj)
			{
				UnsafeNativeMethods.IFont font = (UnsafeNativeMethods.IFont)Marshal.GetObjectForIUnknown(pObj);
				IntPtr hfont = IntPtr.Zero;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					hfont = font.GetHFont();
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				Font result = null;
				IntSecurity.ObjectFromWin32Handle.Assert();
				try
				{
					result = Font.FromHfont(hfont);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					result = Control.DefaultFont;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return result;
			}

			// Token: 0x04003984 RID: 14724
			private static Control.ActiveXFontMarshaler instance;
		}

		// Token: 0x0200063B RID: 1595
		private class ActiveXVerbEnum : UnsafeNativeMethods.IEnumOLEVERB
		{
			// Token: 0x0600646C RID: 25708 RVA: 0x001755EC File Offset: 0x001737EC
			internal ActiveXVerbEnum(NativeMethods.tagOLEVERB[] verbs)
			{
				this.verbs = verbs;
				this.current = 0;
			}

			// Token: 0x0600646D RID: 25709 RVA: 0x00175604 File Offset: 0x00173804
			public int Next(int celt, NativeMethods.tagOLEVERB rgelt, int[] pceltFetched)
			{
				int num = 0;
				if (celt != 1)
				{
					celt = 1;
				}
				while (celt > 0 && this.current < this.verbs.Length)
				{
					rgelt.lVerb = this.verbs[this.current].lVerb;
					rgelt.lpszVerbName = this.verbs[this.current].lpszVerbName;
					rgelt.fuFlags = this.verbs[this.current].fuFlags;
					rgelt.grfAttribs = this.verbs[this.current].grfAttribs;
					celt--;
					this.current++;
					num++;
				}
				if (pceltFetched != null)
				{
					pceltFetched[0] = num;
				}
				if (celt != 0)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x0600646E RID: 25710 RVA: 0x001756B7 File Offset: 0x001738B7
			public int Skip(int celt)
			{
				if (this.current + celt < this.verbs.Length)
				{
					this.current += celt;
					return 0;
				}
				this.current = this.verbs.Length;
				return 1;
			}

			// Token: 0x0600646F RID: 25711 RVA: 0x001756EA File Offset: 0x001738EA
			public void Reset()
			{
				this.current = 0;
			}

			// Token: 0x06006470 RID: 25712 RVA: 0x001756F3 File Offset: 0x001738F3
			public void Clone(out UnsafeNativeMethods.IEnumOLEVERB ppenum)
			{
				ppenum = new Control.ActiveXVerbEnum(this.verbs);
			}

			// Token: 0x04003985 RID: 14725
			private NativeMethods.tagOLEVERB[] verbs;

			// Token: 0x04003986 RID: 14726
			private int current;
		}

		// Token: 0x0200063C RID: 1596
		private class AmbientProperty
		{
			// Token: 0x06006471 RID: 25713 RVA: 0x00175702 File Offset: 0x00173902
			internal AmbientProperty(string name, int dispID)
			{
				this.name = name;
				this.dispID = dispID;
				this.value = null;
				this.empty = true;
			}

			// Token: 0x17001578 RID: 5496
			// (get) Token: 0x06006472 RID: 25714 RVA: 0x00175726 File Offset: 0x00173926
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17001579 RID: 5497
			// (get) Token: 0x06006473 RID: 25715 RVA: 0x0017572E File Offset: 0x0017392E
			internal int DispID
			{
				get
				{
					return this.dispID;
				}
			}

			// Token: 0x1700157A RID: 5498
			// (get) Token: 0x06006474 RID: 25716 RVA: 0x00175736 File Offset: 0x00173936
			internal bool Empty
			{
				get
				{
					return this.empty;
				}
			}

			// Token: 0x1700157B RID: 5499
			// (get) Token: 0x06006475 RID: 25717 RVA: 0x0017573E File Offset: 0x0017393E
			// (set) Token: 0x06006476 RID: 25718 RVA: 0x00175746 File Offset: 0x00173946
			internal object Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
					this.empty = false;
				}
			}

			// Token: 0x06006477 RID: 25719 RVA: 0x00175756 File Offset: 0x00173956
			internal void ResetValue()
			{
				this.empty = true;
				this.value = null;
			}

			// Token: 0x04003987 RID: 14727
			private string name;

			// Token: 0x04003988 RID: 14728
			private int dispID;

			// Token: 0x04003989 RID: 14729
			private object value;

			// Token: 0x0400398A RID: 14730
			private bool empty;
		}

		// Token: 0x0200063D RID: 1597
		private class MetafileDCWrapper : IDisposable
		{
			// Token: 0x06006478 RID: 25720 RVA: 0x00175768 File Offset: 0x00173968
			internal MetafileDCWrapper(HandleRef hOriginalDC, Size size)
			{
				if (size.Width < 0 || size.Height < 0)
				{
					throw new ArgumentException("size", SR.GetString("ControlMetaFileDCWrapperSizeInvalid"));
				}
				this.hMetafileDC = hOriginalDC;
				this.destRect = new NativeMethods.RECT(0, 0, size.Width, size.Height);
				this.hBitmapDC = new HandleRef(this, UnsafeNativeMethods.CreateCompatibleDC(NativeMethods.NullHandleRef));
				int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(this.hBitmapDC, 14);
				int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(this.hBitmapDC, 12);
				this.hBitmap = new HandleRef(this, SafeNativeMethods.CreateBitmap(size.Width, size.Height, deviceCaps, deviceCaps2, IntPtr.Zero));
				this.hOriginalBmp = new HandleRef(this, SafeNativeMethods.SelectObject(this.hBitmapDC, this.hBitmap));
			}

			// Token: 0x06006479 RID: 25721 RVA: 0x00175868 File Offset: 0x00173A68
			~MetafileDCWrapper()
			{
				((IDisposable)this).Dispose();
			}

			// Token: 0x0600647A RID: 25722 RVA: 0x00175894 File Offset: 0x00173A94
			void IDisposable.Dispose()
			{
				if (this.hBitmapDC.Handle == IntPtr.Zero || this.hMetafileDC.Handle == IntPtr.Zero || this.hBitmap.Handle == IntPtr.Zero)
				{
					return;
				}
				try
				{
					bool flag = this.DICopy(this.hMetafileDC, this.hBitmapDC, this.destRect, true);
					SafeNativeMethods.SelectObject(this.hBitmapDC, this.hOriginalBmp);
					flag = SafeNativeMethods.DeleteObject(this.hBitmap);
					flag = UnsafeNativeMethods.DeleteCompatibleDC(this.hBitmapDC);
				}
				finally
				{
					this.hBitmapDC = NativeMethods.NullHandleRef;
					this.hBitmap = NativeMethods.NullHandleRef;
					this.hOriginalBmp = NativeMethods.NullHandleRef;
					GC.SuppressFinalize(this);
				}
			}

			// Token: 0x1700157C RID: 5500
			// (get) Token: 0x0600647B RID: 25723 RVA: 0x00175968 File Offset: 0x00173B68
			internal IntPtr HDC
			{
				get
				{
					return this.hBitmapDC.Handle;
				}
			}

			// Token: 0x0600647C RID: 25724 RVA: 0x00175978 File Offset: 0x00173B78
			private unsafe bool DICopy(HandleRef hdcDest, HandleRef hdcSrc, NativeMethods.RECT rect, bool bStretch)
			{
				bool result = false;
				HandleRef hObject = new HandleRef(this, SafeNativeMethods.CreateBitmap(1, 1, 1, 1, IntPtr.Zero));
				if (hObject.Handle == IntPtr.Zero)
				{
					return result;
				}
				try
				{
					HandleRef handleRef = new HandleRef(this, SafeNativeMethods.SelectObject(hdcSrc, hObject));
					if (handleRef.Handle == IntPtr.Zero)
					{
						return result;
					}
					SafeNativeMethods.SelectObject(hdcSrc, handleRef);
					NativeMethods.BITMAP bitmap = new NativeMethods.BITMAP();
					if (UnsafeNativeMethods.GetObject(handleRef, Marshal.SizeOf(bitmap), bitmap) == 0)
					{
						return result;
					}
					NativeMethods.BITMAPINFO_FLAT bitmapinfo_FLAT = default(NativeMethods.BITMAPINFO_FLAT);
					bitmapinfo_FLAT.bmiHeader_biSize = Marshal.SizeOf(typeof(NativeMethods.BITMAPINFOHEADER));
					bitmapinfo_FLAT.bmiHeader_biWidth = bitmap.bmWidth;
					bitmapinfo_FLAT.bmiHeader_biHeight = bitmap.bmHeight;
					bitmapinfo_FLAT.bmiHeader_biPlanes = 1;
					bitmapinfo_FLAT.bmiHeader_biBitCount = bitmap.bmBitsPixel;
					bitmapinfo_FLAT.bmiHeader_biCompression = 0;
					bitmapinfo_FLAT.bmiHeader_biSizeImage = 0;
					bitmapinfo_FLAT.bmiHeader_biXPelsPerMeter = 0;
					bitmapinfo_FLAT.bmiHeader_biYPelsPerMeter = 0;
					bitmapinfo_FLAT.bmiHeader_biClrUsed = 0;
					bitmapinfo_FLAT.bmiHeader_biClrImportant = 0;
					bitmapinfo_FLAT.bmiColors = new byte[1024];
					long num = 1L << (int)(bitmap.bmBitsPixel * bitmap.bmPlanes & 31);
					if (num <= 256L)
					{
						byte[] array = new byte[Marshal.SizeOf(typeof(NativeMethods.PALETTEENTRY)) * 256];
						SafeNativeMethods.GetSystemPaletteEntries(hdcSrc, 0, (int)num, array);
						try
						{
							byte[] array2;
							byte* ptr;
							if ((array2 = bitmapinfo_FLAT.bmiColors) == null || array2.Length == 0)
							{
								ptr = null;
							}
							else
							{
								ptr = &array2[0];
							}
							try
							{
								byte[] array3;
								byte* ptr2;
								if ((array3 = array) == null || array3.Length == 0)
								{
									ptr2 = null;
								}
								else
								{
									ptr2 = &array3[0];
								}
								NativeMethods.RGBQUAD* ptr3 = (NativeMethods.RGBQUAD*)ptr;
								NativeMethods.PALETTEENTRY* ptr4 = (NativeMethods.PALETTEENTRY*)ptr2;
								for (long num2 = 0L; num2 < (long)((int)num); num2 += 1L)
								{
									ptr3[num2 * (long)sizeof(NativeMethods.RGBQUAD) / (long)sizeof(NativeMethods.RGBQUAD)].rgbRed = ptr4[num2 * (long)sizeof(NativeMethods.PALETTEENTRY) / (long)sizeof(NativeMethods.PALETTEENTRY)].peRed;
									ptr3[num2 * (long)sizeof(NativeMethods.RGBQUAD) / (long)sizeof(NativeMethods.RGBQUAD)].rgbBlue = ptr4[num2 * (long)sizeof(NativeMethods.PALETTEENTRY) / (long)sizeof(NativeMethods.PALETTEENTRY)].peBlue;
									ptr3[num2 * (long)sizeof(NativeMethods.RGBQUAD) / (long)sizeof(NativeMethods.RGBQUAD)].rgbGreen = ptr4[num2 * (long)sizeof(NativeMethods.PALETTEENTRY) / (long)sizeof(NativeMethods.PALETTEENTRY)].peGreen;
								}
							}
							finally
							{
								byte[] array3 = null;
							}
						}
						finally
						{
							byte[] array2 = null;
						}
					}
					long num3 = (long)bitmap.bmBitsPixel * (long)bitmap.bmWidth;
					long num4 = (num3 + 7L) / 8L;
					long num5 = num4 * (long)bitmap.bmHeight;
					byte[] array4 = new byte[num5];
					if (SafeNativeMethods.GetDIBits(hdcSrc, handleRef, 0, bitmap.bmHeight, array4, ref bitmapinfo_FLAT, 0) == 0)
					{
						return result;
					}
					int left;
					int top;
					int nDestWidth;
					int nDestHeight;
					if (bStretch)
					{
						left = rect.left;
						top = rect.top;
						nDestWidth = rect.right - rect.left;
						nDestHeight = rect.bottom - rect.top;
					}
					else
					{
						left = rect.left;
						top = rect.top;
						nDestWidth = bitmap.bmWidth;
						nDestHeight = bitmap.bmHeight;
					}
					int num6 = SafeNativeMethods.StretchDIBits(hdcDest, left, top, nDestWidth, nDestHeight, 0, 0, bitmap.bmWidth, bitmap.bmHeight, array4, ref bitmapinfo_FLAT, 0, 13369376);
					if (num6 == -1)
					{
						return result;
					}
					result = true;
				}
				finally
				{
					SafeNativeMethods.DeleteObject(hObject);
				}
				return result;
			}

			// Token: 0x0400398B RID: 14731
			private HandleRef hBitmapDC = NativeMethods.NullHandleRef;

			// Token: 0x0400398C RID: 14732
			private HandleRef hBitmap = NativeMethods.NullHandleRef;

			// Token: 0x0400398D RID: 14733
			private HandleRef hOriginalBmp = NativeMethods.NullHandleRef;

			// Token: 0x0400398E RID: 14734
			private HandleRef hMetafileDC = NativeMethods.NullHandleRef;

			// Token: 0x0400398F RID: 14735
			private NativeMethods.RECT destRect;
		}

		// Token: 0x0200063E RID: 1598
		[ComVisible(true)]
		public class ControlAccessibleObject : AccessibleObject
		{
			// Token: 0x0600647D RID: 25725 RVA: 0x00175CF0 File Offset: 0x00173EF0
			public ControlAccessibleObject(Control ownerControl)
			{
				if (ownerControl == null)
				{
					throw new ArgumentNullException("ownerControl");
				}
				this.ownerControl = ownerControl;
				IntPtr intPtr = ownerControl.Handle;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					this.Handle = intPtr;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x0600647E RID: 25726 RVA: 0x00175D54 File Offset: 0x00173F54
			internal ControlAccessibleObject(Control ownerControl, int accObjId)
			{
				if (ownerControl == null)
				{
					throw new ArgumentNullException("ownerControl");
				}
				base.AccessibleObjectId = accObjId;
				this.ownerControl = ownerControl;
				IntPtr intPtr = ownerControl.Handle;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					this.Handle = intPtr;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x0600647F RID: 25727 RVA: 0x00175DC0 File Offset: 0x00173FC0
			internal virtual void ClearOwnerControlInternal()
			{
				this.ownerControl = null;
			}

			// Token: 0x06006480 RID: 25728 RVA: 0x00175DC9 File Offset: 0x00173FC9
			internal bool IsOwnerControlDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null;
			}

			// Token: 0x06006481 RID: 25729 RVA: 0x00175DE0 File Offset: 0x00173FE0
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.IsOwnerControlDestroyed())
				{
					return base.FragmentNavigate(direction);
				}
				if (this.Owner.ToolStripControlHost != null && (direction == UnsafeNativeMethods.NavigateDirection.Parent || direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling || direction == UnsafeNativeMethods.NavigateDirection.NextSibling))
				{
					return this.Owner.ToolStripControlHost.AccessibilityObject.FragmentNavigate(direction);
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x1700157D RID: 5501
			// (get) Token: 0x06006482 RID: 25730 RVA: 0x00175E34 File Offset: 0x00174034
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (this.IsOwnerControlDestroyed())
					{
						return base.FragmentRoot;
					}
					ToolStripControlHost toolStripControlHost = this.Owner.ToolStripControlHost;
					ToolStrip toolStrip = (toolStripControlHost != null) ? toolStripControlHost.Owner : null;
					if (toolStrip != null && toolStrip.IsHandleCreated)
					{
						return toolStrip.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x06006483 RID: 25731 RVA: 0x00175E80 File Offset: 0x00174080
			internal override int[] GetSysChildOrder()
			{
				if (this.IsOwnerControlDestroyed())
				{
					return new int[0];
				}
				if (this.ownerControl.GetStyle(ControlStyles.ContainerControl))
				{
					return this.ownerControl.GetChildWindowsInTabOrder();
				}
				return base.GetSysChildOrder();
			}

			// Token: 0x06006484 RID: 25732 RVA: 0x00175EB4 File Offset: 0x001740B4
			internal override bool GetSysChild(AccessibleNavigation navdir, out AccessibleObject accessibleObject)
			{
				accessibleObject = null;
				if (this.IsOwnerControlDestroyed())
				{
					return false;
				}
				Control parentInternal = this.ownerControl.ParentInternal;
				int num = -1;
				Control[] array = null;
				switch (navdir)
				{
				case AccessibleNavigation.Next:
					if (base.IsNonClientObject && parentInternal != null)
					{
						array = parentInternal.GetChildControlsInTabOrder(true);
						num = Array.IndexOf<Control>(array, this.ownerControl);
						if (num != -1)
						{
							num++;
						}
					}
					break;
				case AccessibleNavigation.Previous:
					if (base.IsNonClientObject && parentInternal != null)
					{
						array = parentInternal.GetChildControlsInTabOrder(true);
						num = Array.IndexOf<Control>(array, this.ownerControl);
						if (num != -1)
						{
							num--;
						}
					}
					break;
				case AccessibleNavigation.FirstChild:
					if (base.IsClientObject)
					{
						array = this.ownerControl.GetChildControlsInTabOrder(true);
						num = 0;
					}
					break;
				case AccessibleNavigation.LastChild:
					if (base.IsClientObject)
					{
						array = this.ownerControl.GetChildControlsInTabOrder(true);
						num = array.Length - 1;
					}
					break;
				}
				if (array == null || array.Length == 0)
				{
					return false;
				}
				if (num >= 0 && num < array.Length)
				{
					accessibleObject = array[num].NcAccessibilityObject;
				}
				return true;
			}

			// Token: 0x1700157E RID: 5502
			// (get) Token: 0x06006485 RID: 25733 RVA: 0x00175FA4 File Offset: 0x001741A4
			public override string DefaultAction
			{
				get
				{
					if (this.IsOwnerControlDestroyed())
					{
						return base.DefaultAction;
					}
					string accessibleDefaultActionDescription = this.ownerControl.AccessibleDefaultActionDescription;
					if (accessibleDefaultActionDescription != null)
					{
						return accessibleDefaultActionDescription;
					}
					return base.DefaultAction;
				}
			}

			// Token: 0x1700157F RID: 5503
			// (get) Token: 0x06006486 RID: 25734 RVA: 0x00175FD7 File Offset: 0x001741D7
			internal override int[] RuntimeId
			{
				get
				{
					if (this.runtimeId == null)
					{
						this.runtimeId = new int[2];
						this.runtimeId[0] = 42;
						this.runtimeId[1] = (int)((long)this.Handle);
					}
					return this.runtimeId;
				}
			}

			// Token: 0x17001580 RID: 5504
			// (get) Token: 0x06006487 RID: 25735 RVA: 0x00176014 File Offset: 0x00174214
			public override string Description
			{
				get
				{
					if (this.IsOwnerControlDestroyed())
					{
						return base.Description;
					}
					string accessibleDescription = this.ownerControl.AccessibleDescription;
					if (accessibleDescription != null)
					{
						return accessibleDescription;
					}
					return base.Description;
				}
			}

			// Token: 0x17001581 RID: 5505
			// (get) Token: 0x06006488 RID: 25736 RVA: 0x00176047 File Offset: 0x00174247
			// (set) Token: 0x06006489 RID: 25737 RVA: 0x00176050 File Offset: 0x00174250
			public IntPtr Handle
			{
				get
				{
					return this.handle;
				}
				set
				{
					IntSecurity.UnmanagedCode.Demand();
					if (this.handle != value)
					{
						this.handle = value;
						if (Control.ControlAccessibleObject.oleAccAvailable == IntPtr.Zero)
						{
							return;
						}
						bool flag = false;
						if (Control.ControlAccessibleObject.oleAccAvailable == NativeMethods.InvalidIntPtr)
						{
							Control.ControlAccessibleObject.oleAccAvailable = UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable("oleacc.dll");
							flag = (Control.ControlAccessibleObject.oleAccAvailable != IntPtr.Zero);
						}
						if (this.handle != IntPtr.Zero && Control.ControlAccessibleObject.oleAccAvailable != IntPtr.Zero)
						{
							base.UseStdAccessibleObjects(this.handle);
						}
						if (flag)
						{
							UnsafeNativeMethods.FreeLibrary(new HandleRef(null, Control.ControlAccessibleObject.oleAccAvailable));
						}
					}
				}
			}

			// Token: 0x17001582 RID: 5506
			// (get) Token: 0x0600648A RID: 25738 RVA: 0x00176108 File Offset: 0x00174308
			public override string Help
			{
				get
				{
					if (this.IsOwnerControlDestroyed())
					{
						return base.Help;
					}
					QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)this.Owner.Events[Control.EventQueryAccessibilityHelp];
					if (queryAccessibilityHelpEventHandler != null)
					{
						QueryAccessibilityHelpEventArgs queryAccessibilityHelpEventArgs = new QueryAccessibilityHelpEventArgs();
						queryAccessibilityHelpEventHandler(this.Owner, queryAccessibilityHelpEventArgs);
						return queryAccessibilityHelpEventArgs.HelpString;
					}
					return base.Help;
				}
			}

			// Token: 0x17001583 RID: 5507
			// (get) Token: 0x0600648B RID: 25739 RVA: 0x00176164 File Offset: 0x00174364
			public override string KeyboardShortcut
			{
				get
				{
					char mnemonic = WindowsFormsUtils.GetMnemonic(this.TextLabel, false);
					if (mnemonic != '\0')
					{
						return "Alt+" + mnemonic.ToString();
					}
					return null;
				}
			}

			// Token: 0x17001584 RID: 5508
			// (get) Token: 0x0600648C RID: 25740 RVA: 0x00176194 File Offset: 0x00174394
			// (set) Token: 0x0600648D RID: 25741 RVA: 0x001761D1 File Offset: 0x001743D1
			public override string Name
			{
				get
				{
					if (this.IsOwnerControlDestroyed())
					{
						return WindowsFormsUtils.TextWithoutMnemonics(this.TextLabel);
					}
					string accessibleName = this.ownerControl.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					return WindowsFormsUtils.TextWithoutMnemonics(this.TextLabel);
				}
				set
				{
					if (this.IsOwnerControlDestroyed())
					{
						return;
					}
					this.ownerControl.AccessibleName = value;
				}
			}

			// Token: 0x17001585 RID: 5509
			// (get) Token: 0x0600648E RID: 25742 RVA: 0x001761E8 File Offset: 0x001743E8
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return base.Parent;
				}
			}

			// Token: 0x17001586 RID: 5510
			// (get) Token: 0x0600648F RID: 25743 RVA: 0x001761F0 File Offset: 0x001743F0
			internal string TextLabel
			{
				get
				{
					if (this.IsOwnerControlDestroyed())
					{
						return null;
					}
					if (this.ownerControl.GetStyle(ControlStyles.UseTextForAccessibility))
					{
						string text = this.ownerControl.Text;
						if (!string.IsNullOrEmpty(text))
						{
							return text;
						}
					}
					Label previousLabel = this.PreviousLabel;
					if (previousLabel != null)
					{
						string text2 = previousLabel.Text;
						if (!string.IsNullOrEmpty(text2))
						{
							return text2;
						}
					}
					return null;
				}
			}

			// Token: 0x17001587 RID: 5511
			// (get) Token: 0x06006490 RID: 25744 RVA: 0x0017624B File Offset: 0x0017444B
			public Control Owner
			{
				get
				{
					return this.ownerControl;
				}
			}

			// Token: 0x17001588 RID: 5512
			// (get) Token: 0x06006491 RID: 25745 RVA: 0x00176254 File Offset: 0x00174454
			internal Label PreviousLabel
			{
				get
				{
					if (this.IsOwnerControlDestroyed())
					{
						return null;
					}
					Control parentInternal = this.Owner.ParentInternal;
					if (parentInternal == null)
					{
						return null;
					}
					ContainerControl containerControl = parentInternal.GetContainerControlInternal() as ContainerControl;
					if (containerControl == null)
					{
						return null;
					}
					for (Control nextControl = containerControl.GetNextControl(this.Owner, false); nextControl != null; nextControl = containerControl.GetNextControl(nextControl, false))
					{
						if (nextControl is Label)
						{
							return nextControl as Label;
						}
						if (nextControl.Visible && nextControl.TabStop)
						{
							break;
						}
					}
					return null;
				}
			}

			// Token: 0x17001589 RID: 5513
			// (get) Token: 0x06006492 RID: 25746 RVA: 0x001762CC File Offset: 0x001744CC
			public override AccessibleRole Role
			{
				get
				{
					if (this.IsOwnerControlDestroyed())
					{
						return base.Role;
					}
					AccessibleRole accessibleRole = this.ownerControl.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return base.Role;
				}
			}

			// Token: 0x06006493 RID: 25747 RVA: 0x00176300 File Offset: 0x00174500
			public override int GetHelpTopic(out string fileName)
			{
				int result = 0;
				if (this.IsOwnerControlDestroyed())
				{
					fileName = string.Empty;
					return result;
				}
				QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)this.Owner.Events[Control.EventQueryAccessibilityHelp];
				if (queryAccessibilityHelpEventHandler != null)
				{
					QueryAccessibilityHelpEventArgs queryAccessibilityHelpEventArgs = new QueryAccessibilityHelpEventArgs();
					queryAccessibilityHelpEventHandler(this.Owner, queryAccessibilityHelpEventArgs);
					fileName = queryAccessibilityHelpEventArgs.HelpNamespace;
					if (!string.IsNullOrEmpty(fileName))
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.PathDiscovery, fileName);
					}
					try
					{
						result = int.Parse(queryAccessibilityHelpEventArgs.HelpKeyword, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
					return result;
				}
				return base.GetHelpTopic(out fileName);
			}

			// Token: 0x06006494 RID: 25748 RVA: 0x001763A4 File Offset: 0x001745A4
			public void NotifyClients(AccessibleEvents accEvent)
			{
				if (LocalAppContextSwitches.NoClientNotifications)
				{
					return;
				}
				UnsafeNativeMethods.NotifyWinEvent((int)accEvent, new HandleRef(this, this.Handle), -4, 0);
			}

			// Token: 0x06006495 RID: 25749 RVA: 0x001763C3 File Offset: 0x001745C3
			public void NotifyClients(AccessibleEvents accEvent, int childID)
			{
				if (LocalAppContextSwitches.NoClientNotifications)
				{
					return;
				}
				UnsafeNativeMethods.NotifyWinEvent((int)accEvent, new HandleRef(this, this.Handle), -4, childID + 1);
			}

			// Token: 0x06006496 RID: 25750 RVA: 0x001763E4 File Offset: 0x001745E4
			public void NotifyClients(AccessibleEvents accEvent, int objectID, int childID)
			{
				if (LocalAppContextSwitches.NoClientNotifications)
				{
					return;
				}
				UnsafeNativeMethods.NotifyWinEvent((int)accEvent, new HandleRef(this, this.Handle), objectID, childID + 1);
			}

			// Token: 0x06006497 RID: 25751 RVA: 0x00176404 File Offset: 0x00174604
			public override bool RaiseLiveRegionChanged()
			{
				if (!(this.Owner is IAutomationLiveRegion))
				{
					throw new InvalidOperationException(SR.GetString("OwnerControlIsNotALiveRegion"));
				}
				return base.RaiseAutomationEvent(20024);
			}

			// Token: 0x06006498 RID: 25752 RVA: 0x0017642E File Offset: 0x0017462E
			internal override bool IsIAccessibleExSupported()
			{
				return (AccessibilityImprovements.Level3 && this.Owner is IAutomationLiveRegion) || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006499 RID: 25753 RVA: 0x0017644C File Offset: 0x0017464C
			internal override object GetPropertyValue(int propertyID)
			{
				if (this.IsOwnerControlDestroyed())
				{
					return null;
				}
				if (AccessibilityImprovements.Level3 && propertyID == 30135 && this.Owner is IAutomationLiveRegion)
				{
					return ((IAutomationLiveRegion)this.Owner).LiveSetting;
				}
				if (this.Owner.SupportsUiaProviders)
				{
					if (propertyID <= 30009)
					{
						if (propertyID == 30007)
						{
							return string.Empty;
						}
						if (propertyID == 30009)
						{
							return this.Owner.CanSelect;
						}
					}
					else if (propertyID != 30013)
					{
						if (propertyID == 30019 || propertyID == 30022)
						{
							return false;
						}
					}
					else
					{
						string help = this.Help;
						if (!AccessibilityImprovements.Level3)
						{
							return help;
						}
						return help ?? string.Empty;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x1700158A RID: 5514
			// (get) Token: 0x0600649A RID: 25754 RVA: 0x00176518 File Offset: 0x00174718
			internal override UnsafeNativeMethods.IRawElementProviderSimple HostRawElementProvider
			{
				get
				{
					if (AccessibilityImprovements.Level3)
					{
						UnsafeNativeMethods.IRawElementProviderSimple result;
						UnsafeNativeMethods.UiaHostProviderFromHwnd(new HandleRef(this, this.Handle), out result);
						return result;
					}
					return base.HostRawElementProvider;
				}
			}

			// Token: 0x0600649B RID: 25755 RVA: 0x00176548 File Offset: 0x00174748
			public override string ToString()
			{
				if (this.Owner != null)
				{
					return "ControlAccessibleObject: Owner = " + this.Owner.ToString();
				}
				return "ControlAccessibleObject: Owner = null";
			}

			// Token: 0x04003990 RID: 14736
			private static IntPtr oleAccAvailable = NativeMethods.InvalidIntPtr;

			// Token: 0x04003991 RID: 14737
			private IntPtr handle = IntPtr.Zero;

			// Token: 0x04003992 RID: 14738
			private Control ownerControl;

			// Token: 0x04003993 RID: 14739
			private int[] runtimeId;
		}

		// Token: 0x0200063F RID: 1599
		internal sealed class FontHandleWrapper : MarshalByRefObject, IDisposable
		{
			// Token: 0x0600649D RID: 25757 RVA: 0x00176579 File Offset: 0x00174779
			internal FontHandleWrapper(Font font)
			{
				this.handle = font.ToHfont();
				System.Internal.HandleCollector.Add(this.handle, NativeMethods.CommonHandles.GDI);
			}

			// Token: 0x1700158B RID: 5515
			// (get) Token: 0x0600649E RID: 25758 RVA: 0x0017659E File Offset: 0x0017479E
			internal IntPtr Handle
			{
				get
				{
					return this.handle;
				}
			}

			// Token: 0x0600649F RID: 25759 RVA: 0x001765A6 File Offset: 0x001747A6
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x060064A0 RID: 25760 RVA: 0x001765B5 File Offset: 0x001747B5
			private void Dispose(bool disposing)
			{
				if (this.handle != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(this, this.handle));
					this.handle = IntPtr.Zero;
				}
			}

			// Token: 0x060064A1 RID: 25761 RVA: 0x001765E8 File Offset: 0x001747E8
			~FontHandleWrapper()
			{
				this.Dispose(false);
			}

			// Token: 0x04003994 RID: 14740
			private IntPtr handle;
		}

		// Token: 0x02000640 RID: 1600
		private class ThreadMethodEntry : IAsyncResult
		{
			// Token: 0x060064A2 RID: 25762 RVA: 0x00176618 File Offset: 0x00174818
			internal ThreadMethodEntry(Control caller, Control marshaler, Delegate method, object[] args, bool synchronous, ExecutionContext executionContext)
			{
				this.caller = caller;
				this.marshaler = marshaler;
				this.method = method;
				this.args = args;
				this.exception = null;
				this.retVal = null;
				this.synchronous = synchronous;
				this.isCompleted = false;
				this.resetEvent = null;
				this.executionContext = executionContext;
			}

			// Token: 0x060064A3 RID: 25763 RVA: 0x00176680 File Offset: 0x00174880
			~ThreadMethodEntry()
			{
				if (this.resetEvent != null)
				{
					this.resetEvent.Close();
				}
			}

			// Token: 0x1700158C RID: 5516
			// (get) Token: 0x060064A4 RID: 25764 RVA: 0x00015ECC File Offset: 0x000140CC
			public object AsyncState
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700158D RID: 5517
			// (get) Token: 0x060064A5 RID: 25765 RVA: 0x001766BC File Offset: 0x001748BC
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					if (this.resetEvent == null)
					{
						object obj = this.invokeSyncObject;
						lock (obj)
						{
							if (this.resetEvent == null)
							{
								this.resetEvent = new ManualResetEvent(false);
								if (this.isCompleted)
								{
									this.resetEvent.Set();
								}
							}
						}
					}
					return this.resetEvent;
				}
			}

			// Token: 0x1700158E RID: 5518
			// (get) Token: 0x060064A6 RID: 25766 RVA: 0x0017672C File Offset: 0x0017492C
			public bool CompletedSynchronously
			{
				get
				{
					return this.isCompleted && this.synchronous;
				}
			}

			// Token: 0x1700158F RID: 5519
			// (get) Token: 0x060064A7 RID: 25767 RVA: 0x00176741 File Offset: 0x00174941
			public bool IsCompleted
			{
				get
				{
					return this.isCompleted;
				}
			}

			// Token: 0x060064A8 RID: 25768 RVA: 0x0017674C File Offset: 0x0017494C
			internal void Complete()
			{
				object obj = this.invokeSyncObject;
				lock (obj)
				{
					this.isCompleted = true;
					if (this.resetEvent != null)
					{
						this.resetEvent.Set();
					}
				}
			}

			// Token: 0x04003995 RID: 14741
			internal Control caller;

			// Token: 0x04003996 RID: 14742
			internal Control marshaler;

			// Token: 0x04003997 RID: 14743
			internal Delegate method;

			// Token: 0x04003998 RID: 14744
			internal object[] args;

			// Token: 0x04003999 RID: 14745
			internal object retVal;

			// Token: 0x0400399A RID: 14746
			internal Exception exception;

			// Token: 0x0400399B RID: 14747
			internal bool synchronous;

			// Token: 0x0400399C RID: 14748
			private bool isCompleted;

			// Token: 0x0400399D RID: 14749
			private ManualResetEvent resetEvent;

			// Token: 0x0400399E RID: 14750
			private object invokeSyncObject = new object();

			// Token: 0x0400399F RID: 14751
			internal ExecutionContext executionContext;

			// Token: 0x040039A0 RID: 14752
			internal SynchronizationContext syncContext;
		}

		// Token: 0x02000641 RID: 1601
		private class ControlVersionInfo
		{
			// Token: 0x060064A9 RID: 25769 RVA: 0x001767A4 File Offset: 0x001749A4
			internal ControlVersionInfo(Control owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001590 RID: 5520
			// (get) Token: 0x060064AA RID: 25770 RVA: 0x001767B4 File Offset: 0x001749B4
			internal string CompanyName
			{
				get
				{
					if (this.companyName == null)
					{
						object[] customAttributes = this.owner.GetType().Module.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							this.companyName = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
						}
						if (this.companyName == null || this.companyName.Length == 0)
						{
							this.companyName = this.GetFileVersionInfo().CompanyName;
							if (this.companyName != null)
							{
								this.companyName = this.companyName.Trim();
							}
						}
						if (this.companyName == null || this.companyName.Length == 0)
						{
							string text = this.owner.GetType().Namespace;
							if (text == null)
							{
								text = string.Empty;
							}
							int num = text.IndexOf("/");
							if (num != -1)
							{
								this.companyName = text.Substring(0, num);
							}
							else
							{
								this.companyName = text;
							}
						}
					}
					return this.companyName;
				}
			}

			// Token: 0x17001591 RID: 5521
			// (get) Token: 0x060064AB RID: 25771 RVA: 0x001768A8 File Offset: 0x00174AA8
			internal string ProductName
			{
				get
				{
					if (this.productName == null)
					{
						object[] customAttributes = this.owner.GetType().Module.Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							this.productName = ((AssemblyProductAttribute)customAttributes[0]).Product;
						}
						if (this.productName == null || this.productName.Length == 0)
						{
							this.productName = this.GetFileVersionInfo().ProductName;
							if (this.productName != null)
							{
								this.productName = this.productName.Trim();
							}
						}
						if (this.productName == null || this.productName.Length == 0)
						{
							string text = this.owner.GetType().Namespace;
							if (text == null)
							{
								text = string.Empty;
							}
							int num = text.IndexOf(".");
							if (num != -1)
							{
								this.productName = text.Substring(num + 1);
							}
							else
							{
								this.productName = text;
							}
						}
					}
					return this.productName;
				}
			}

			// Token: 0x17001592 RID: 5522
			// (get) Token: 0x060064AC RID: 25772 RVA: 0x0017699C File Offset: 0x00174B9C
			internal string ProductVersion
			{
				get
				{
					if (this.productVersion == null)
					{
						object[] customAttributes = this.owner.GetType().Module.Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							this.productVersion = ((AssemblyInformationalVersionAttribute)customAttributes[0]).InformationalVersion;
						}
						if (this.productVersion == null || this.productVersion.Length == 0)
						{
							this.productVersion = this.GetFileVersionInfo().ProductVersion;
							if (this.productVersion != null)
							{
								this.productVersion = this.productVersion.Trim();
							}
						}
						if (this.productVersion == null || this.productVersion.Length == 0)
						{
							this.productVersion = "1.0.0.0";
						}
					}
					return this.productVersion;
				}
			}

			// Token: 0x060064AD RID: 25773 RVA: 0x00176A5C File Offset: 0x00174C5C
			private FileVersionInfo GetFileVersionInfo()
			{
				if (this.versionInfo == null)
				{
					new FileIOPermission(PermissionState.None)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Assert();
					string fullyQualifiedName;
					try
					{
						fullyQualifiedName = this.owner.GetType().Module.FullyQualifiedName;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					new FileIOPermission(FileIOPermissionAccess.Read, fullyQualifiedName).Assert();
					try
					{
						this.versionInfo = FileVersionInfo.GetVersionInfo(fullyQualifiedName);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return this.versionInfo;
			}

			// Token: 0x040039A1 RID: 14753
			private string companyName;

			// Token: 0x040039A2 RID: 14754
			private string productName;

			// Token: 0x040039A3 RID: 14755
			private string productVersion;

			// Token: 0x040039A4 RID: 14756
			private FileVersionInfo versionInfo;

			// Token: 0x040039A5 RID: 14757
			private Control owner;
		}

		// Token: 0x02000642 RID: 1602
		private sealed class MultithreadSafeCallScope : IDisposable
		{
			// Token: 0x060064AE RID: 25774 RVA: 0x00176AE8 File Offset: 0x00174CE8
			internal MultithreadSafeCallScope()
			{
				if (Control.checkForIllegalCrossThreadCalls && !Control.inCrossThreadSafeCall)
				{
					Control.inCrossThreadSafeCall = true;
					this.resultedInSet = true;
					return;
				}
				this.resultedInSet = false;
			}

			// Token: 0x060064AF RID: 25775 RVA: 0x00176B13 File Offset: 0x00174D13
			void IDisposable.Dispose()
			{
				if (this.resultedInSet)
				{
					Control.inCrossThreadSafeCall = false;
				}
			}

			// Token: 0x040039A6 RID: 14758
			private bool resultedInSet;
		}

		// Token: 0x02000643 RID: 1603
		private sealed class PrintPaintEventArgs : PaintEventArgs
		{
			// Token: 0x060064B0 RID: 25776 RVA: 0x00176B23 File Offset: 0x00174D23
			internal PrintPaintEventArgs(Message m, IntPtr dc, Rectangle clipRect) : base(dc, clipRect)
			{
				this.m = m;
			}

			// Token: 0x17001593 RID: 5523
			// (get) Token: 0x060064B1 RID: 25777 RVA: 0x00176B34 File Offset: 0x00174D34
			internal Message Message
			{
				get
				{
					return this.m;
				}
			}

			// Token: 0x040039A7 RID: 14759
			private Message m;
		}
	}
}
