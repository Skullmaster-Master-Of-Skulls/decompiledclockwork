using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003E7 RID: 999
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	[DesignerSerializer("System.Windows.Forms.Design.ToolStripMenuItemCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ToolStripMenuItem : ToolStripDropDownItem
	{
		// Token: 0x060043E1 RID: 17377 RVA: 0x0011F0C4 File Offset: 0x0011D2C4
		public ToolStripMenuItem()
		{
			this.Initialize();
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x0011F130 File Offset: 0x0011D330
		public ToolStripMenuItem(string text) : base(text, null, null)
		{
			this.Initialize();
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x0011F19C File Offset: 0x0011D39C
		public ToolStripMenuItem(Image image) : base(null, image, null)
		{
			this.Initialize();
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x0011F208 File Offset: 0x0011D408
		public ToolStripMenuItem(string text, Image image) : base(text, image, null)
		{
			this.Initialize();
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x0011F274 File Offset: 0x0011D474
		public ToolStripMenuItem(string text, Image image, EventHandler onClick) : base(text, image, onClick)
		{
			this.Initialize();
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x0011F2E0 File Offset: 0x0011D4E0
		public ToolStripMenuItem(string text, Image image, EventHandler onClick, string name) : base(text, image, onClick, name)
		{
			this.Initialize();
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x0011F350 File Offset: 0x0011D550
		public ToolStripMenuItem(string text, Image image, params ToolStripItem[] dropDownItems) : base(text, image, dropDownItems)
		{
			this.Initialize();
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x0011F3BC File Offset: 0x0011D5BC
		public ToolStripMenuItem(string text, Image image, EventHandler onClick, Keys shortcutKeys) : base(text, image, onClick)
		{
			this.Initialize();
			this.ShortcutKeys = shortcutKeys;
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x0011F430 File Offset: 0x0011D630
		internal ToolStripMenuItem(Form mdiForm)
		{
			this.Initialize();
			base.Properties.SetObject(ToolStripMenuItem.PropMdiForm, mdiForm);
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x0011F4AC File Offset: 0x0011D6AC
		internal ToolStripMenuItem(IntPtr hMenu, int nativeMenuCommandId, IWin32Window targetWindow)
		{
			this.Initialize();
			this.Overflow = ToolStripItemOverflow.Never;
			this.nativeMenuCommandID = nativeMenuCommandId;
			this.targetWindowHandle = Control.GetSafeHandle(targetWindow);
			this.nativeMenuHandle = hMenu;
			this.Image = this.GetNativeMenuItemImage();
			base.ImageScaling = ToolStripItemImageScaling.None;
			string nativeMenuItemTextAndShortcut = this.GetNativeMenuItemTextAndShortcut();
			if (nativeMenuItemTextAndShortcut != null)
			{
				string[] array = nativeMenuItemTextAndShortcut.Split(new char[]
				{
					'\t'
				});
				if (array.Length >= 1)
				{
					this.Text = array[0];
				}
				if (array.Length >= 2)
				{
					this.ShowShortcutKeys = true;
					this.ShortcutKeyDisplayString = array[1];
				}
			}
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x0011F58A File Offset: 0x0011D78A
		internal override void AutoHide(ToolStripItem otherItemBeingSelected)
		{
			if (base.IsOnDropDown)
			{
				ToolStripMenuItem.MenuTimer.Transition(this, otherItemBeingSelected as ToolStripMenuItem);
				return;
			}
			base.AutoHide(otherItemBeingSelected);
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x0011F5AD File Offset: 0x0011D7AD
		private void ClearShortcutCache()
		{
			this.cachedShortcutSize = Size.Empty;
			this.cachedShortcutText = null;
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x00114B4C File Offset: 0x00112D4C
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu(this, true);
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x0011F5C1 File Offset: 0x0011D7C1
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripMenuItemInternalLayout(this);
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x0011F5C9 File Offset: 0x0011D7C9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripMenuItem.ToolStripMenuItemAccessibleObject(this);
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x0011F5D4 File Offset: 0x0011D7D4
		private void Initialize()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.defaultPadding, 0);
				this.scaledDefaultDropDownPadding = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.defaultDropDownPadding, 0);
				this.scaledCheckMarkBitmapSize = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.checkMarkBitmapSize, 0);
			}
			this.Overflow = ToolStripItemOverflow.Never;
			base.MouseDownAndUpMustBeInSameItem = false;
			base.SupportsDisabledHotTracking = true;
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x060043F1 RID: 17393 RVA: 0x0011F630 File Offset: 0x0011D830
		protected override Size DefaultSize
		{
			get
			{
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return new Size(32, 19);
				}
				return DpiHelper.LogicalToDeviceUnits(new Size(32, 19), this.DeviceDpi);
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected internal override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060043F3 RID: 17395 RVA: 0x0011F657 File Offset: 0x0011D857
		protected override Padding DefaultPadding
		{
			get
			{
				if (base.IsOnDropDown)
				{
					return this.scaledDefaultDropDownPadding;
				}
				return this.scaledDefaultPadding;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x0011F670 File Offset: 0x0011D870
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x0011F6C1 File Offset: 0x0011D8C1
		public override bool Enabled
		{
			get
			{
				if (this.nativeMenuCommandID != -1)
				{
					return base.Enabled && this.nativeMenuHandle != IntPtr.Zero && this.targetWindowHandle != IntPtr.Zero && this.GetNativeMenuItemEnabled();
				}
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x0011F6CA File Offset: 0x0011D8CA
		// (set) Token: 0x060043F7 RID: 17399 RVA: 0x0011F6D5 File Offset: 0x0011D8D5
		[Bindable(true)]
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("CheckBoxCheckedDescr")]
		public bool Checked
		{
			get
			{
				return this.CheckState > CheckState.Unchecked;
			}
			set
			{
				if (value != this.Checked)
				{
					this.CheckState = (value ? CheckState.Checked : CheckState.Unchecked);
					base.InvokePaint();
				}
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x0011F6F4 File Offset: 0x0011D8F4
		internal Image CheckedImage
		{
			get
			{
				CheckState checkState = this.CheckState;
				if (checkState == CheckState.Indeterminate)
				{
					if (ToolStripMenuItem.indeterminateCheckedImage == null)
					{
						if (DpiHelper.EnableToolStripHighDpiImprovements)
						{
							ToolStripMenuItem.indeterminateCheckedImage = ToolStripMenuItem.GetBitmapFromIcon("IndeterminateChecked.ico", this.scaledCheckMarkBitmapSize);
						}
						else
						{
							Bitmap bitmap = new Bitmap(typeof(ToolStripMenuItem), "IndeterminateChecked.bmp");
							if (bitmap != null)
							{
								bitmap.MakeTransparent(bitmap.GetPixel(1, 1));
								if (DpiHelper.IsScalingRequired)
								{
									DpiHelper.ScaleBitmapLogicalToDevice(ref bitmap, 0);
								}
								ToolStripMenuItem.indeterminateCheckedImage = bitmap;
							}
						}
					}
					return ToolStripMenuItem.indeterminateCheckedImage;
				}
				if (checkState == CheckState.Checked)
				{
					if (ToolStripMenuItem.checkedImage == null)
					{
						if (DpiHelper.EnableToolStripHighDpiImprovements)
						{
							ToolStripMenuItem.checkedImage = ToolStripMenuItem.GetBitmapFromIcon("Checked.ico", this.scaledCheckMarkBitmapSize);
						}
						else
						{
							Bitmap bitmap2 = new Bitmap(typeof(ToolStripMenuItem), "Checked.bmp");
							if (bitmap2 != null)
							{
								bitmap2.MakeTransparent(bitmap2.GetPixel(1, 1));
								if (DpiHelper.IsScalingRequired)
								{
									DpiHelper.ScaleBitmapLogicalToDevice(ref bitmap2, 0);
								}
								ToolStripMenuItem.checkedImage = bitmap2;
							}
						}
					}
					return ToolStripMenuItem.checkedImage;
				}
				return null;
			}
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x0011F7E0 File Offset: 0x0011D9E0
		private static Bitmap GetBitmapFromIcon(string iconName, Size desiredIconSize)
		{
			Bitmap bitmap = null;
			Icon icon = new Icon(typeof(ToolStripMenuItem), iconName);
			if (icon != null)
			{
				Icon icon2 = new Icon(icon, desiredIconSize);
				if (icon2 != null)
				{
					try
					{
						bitmap = icon2.ToBitmap();
						if (bitmap != null)
						{
							bitmap.MakeTransparent(bitmap.GetPixel(1, 1));
							if (DpiHelper.IsScalingRequired && (bitmap.Size.Width != desiredIconSize.Width || bitmap.Size.Height != desiredIconSize.Height))
							{
								Bitmap bitmap2 = DpiHelper.CreateResizedBitmap(bitmap, desiredIconSize);
								if (bitmap2 != null)
								{
									bitmap.Dispose();
									bitmap = bitmap2;
								}
							}
						}
					}
					finally
					{
						icon.Dispose();
						icon2.Dispose();
					}
				}
			}
			return bitmap;
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x0011F894 File Offset: 0x0011DA94
		// (set) Token: 0x060043FB RID: 17403 RVA: 0x0011F89C File Offset: 0x0011DA9C
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripButtonCheckOnClickDescr")]
		public bool CheckOnClick
		{
			get
			{
				return this.checkOnClick;
			}
			set
			{
				this.checkOnClick = value;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x0011F8A8 File Offset: 0x0011DAA8
		// (set) Token: 0x060043FD RID: 17405 RVA: 0x0011F8DC File Offset: 0x0011DADC
		[Bindable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(CheckState.Unchecked)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("CheckBoxCheckStateDescr")]
		public CheckState CheckState
		{
			get
			{
				bool flag = false;
				object obj = base.Properties.GetInteger(ToolStripMenuItem.PropCheckState, out flag);
				if (!flag)
				{
					return CheckState.Unchecked;
				}
				return (CheckState)obj;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(CheckState));
				}
				if (value != this.CheckState)
				{
					base.Properties.SetInteger(ToolStripMenuItem.PropCheckState, (int)value);
					this.OnCheckedChanged(EventArgs.Empty);
					this.OnCheckStateChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000354 RID: 852
		// (add) Token: 0x060043FE RID: 17406 RVA: 0x0011F93F File Offset: 0x0011DB3F
		// (remove) Token: 0x060043FF RID: 17407 RVA: 0x0011F952 File Offset: 0x0011DB52
		[SRDescription("CheckBoxOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripMenuItem.EventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripMenuItem.EventCheckedChanged, value);
			}
		}

		// Token: 0x14000355 RID: 853
		// (add) Token: 0x06004400 RID: 17408 RVA: 0x0011F965 File Offset: 0x0011DB65
		// (remove) Token: 0x06004401 RID: 17409 RVA: 0x0011F978 File Offset: 0x0011DB78
		[SRDescription("CheckBoxOnCheckStateChangedDescr")]
		public event EventHandler CheckStateChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripMenuItem.EventCheckStateChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripMenuItem.EventCheckStateChanged, value);
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x0011F98B File Offset: 0x0011DB8B
		// (set) Token: 0x06004403 RID: 17411 RVA: 0x0011F993 File Offset: 0x0011DB93
		[DefaultValue(ToolStripItemOverflow.Never)]
		[SRDescription("ToolStripItemOverflowDescr")]
		[SRCategory("CatLayout")]
		public new ToolStripItemOverflow Overflow
		{
			get
			{
				return base.Overflow;
			}
			set
			{
				base.Overflow = value;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x0011F99C File Offset: 0x0011DB9C
		// (set) Token: 0x06004405 RID: 17413 RVA: 0x0011F9D0 File Offset: 0x0011DBD0
		[Localizable(true)]
		[DefaultValue(Keys.None)]
		[SRDescription("MenuItemShortCutDescr")]
		public Keys ShortcutKeys
		{
			get
			{
				bool flag = false;
				object obj = base.Properties.GetInteger(ToolStripMenuItem.PropShortcutKeys, out flag);
				if (!flag)
				{
					return Keys.None;
				}
				return (Keys)obj;
			}
			set
			{
				if (value != Keys.None && !ToolStripManager.IsValidShortcut(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Keys));
				}
				Keys shortcutKeys = this.ShortcutKeys;
				if (shortcutKeys != value)
				{
					this.ClearShortcutCache();
					ToolStrip owner = base.Owner;
					if (owner != null)
					{
						if (shortcutKeys != Keys.None)
						{
							owner.Shortcuts.Remove(shortcutKeys);
						}
						if (owner.Shortcuts.Contains(value))
						{
							owner.Shortcuts[value] = this;
						}
						else
						{
							owner.Shortcuts.Add(value, this);
						}
					}
					base.Properties.SetInteger(ToolStripMenuItem.PropShortcutKeys, (int)value);
					if (this.ShowShortcutKeys && base.IsOnDropDown)
					{
						ToolStripDropDownMenu toolStripDropDownMenu = base.GetCurrentParentDropDown() as ToolStripDropDownMenu;
						if (toolStripDropDownMenu != null)
						{
							LayoutTransaction.DoLayout(base.ParentInternal, this, "ShortcutKeys");
							toolStripDropDownMenu.AdjustSize();
						}
					}
				}
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06004406 RID: 17414 RVA: 0x0011FAB0 File Offset: 0x0011DCB0
		// (set) Token: 0x06004407 RID: 17415 RVA: 0x0011FAB8 File Offset: 0x0011DCB8
		[SRDescription("ToolStripMenuItemShortcutKeyDisplayStringDescr")]
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[Localizable(true)]
		public string ShortcutKeyDisplayString
		{
			get
			{
				return this.shortcutKeyDisplayString;
			}
			set
			{
				if (this.shortcutKeyDisplayString != value)
				{
					this.shortcutKeyDisplayString = value;
					this.ClearShortcutCache();
					if (this.ShowShortcutKeys)
					{
						ToolStripDropDown toolStripDropDown = base.ParentInternal as ToolStripDropDown;
						if (toolStripDropDown != null)
						{
							LayoutTransaction.DoLayout(toolStripDropDown, this, "ShortcutKeyDisplayString");
							toolStripDropDown.AdjustSize();
						}
					}
				}
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x0011FB09 File Offset: 0x0011DD09
		// (set) Token: 0x06004409 RID: 17417 RVA: 0x0011FB14 File Offset: 0x0011DD14
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("MenuItemShowShortCutDescr")]
		public bool ShowShortcutKeys
		{
			get
			{
				return this.showShortcutKeys;
			}
			set
			{
				if (value != this.showShortcutKeys)
				{
					this.ClearShortcutCache();
					this.showShortcutKeys = value;
					ToolStripDropDown toolStripDropDown = base.ParentInternal as ToolStripDropDown;
					if (toolStripDropDown != null)
					{
						LayoutTransaction.DoLayout(toolStripDropDown, this, "ShortcutKeys");
						toolStripDropDown.AdjustSize();
					}
				}
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x0011FB58 File Offset: 0x0011DD58
		internal bool IsTopLevel
		{
			get
			{
				return !(base.ParentInternal is ToolStripDropDown);
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x0600440B RID: 17419 RVA: 0x0011FB68 File Offset: 0x0011DD68
		[Browsable(false)]
		public bool IsMdiWindowListEntry
		{
			get
			{
				return this.MdiForm != null;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x0011FB73 File Offset: 0x0011DD73
		internal static MenuTimer MenuTimer
		{
			get
			{
				return ToolStripMenuItem.menuTimer;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x0011FB7A File Offset: 0x0011DD7A
		internal Form MdiForm
		{
			get
			{
				if (base.Properties.ContainsObject(ToolStripMenuItem.PropMdiForm))
				{
					return base.Properties.GetObject(ToolStripMenuItem.PropMdiForm) as Form;
				}
				return null;
			}
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x0011FBA8 File Offset: 0x0011DDA8
		internal ToolStripMenuItem Clone()
		{
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			toolStripMenuItem.Events.AddHandlers(base.Events);
			toolStripMenuItem.AccessibleName = base.AccessibleName;
			toolStripMenuItem.AccessibleRole = base.AccessibleRole;
			toolStripMenuItem.Alignment = base.Alignment;
			toolStripMenuItem.AllowDrop = this.AllowDrop;
			toolStripMenuItem.Anchor = base.Anchor;
			toolStripMenuItem.AutoSize = base.AutoSize;
			toolStripMenuItem.AutoToolTip = base.AutoToolTip;
			toolStripMenuItem.BackColor = this.BackColor;
			toolStripMenuItem.BackgroundImage = this.BackgroundImage;
			toolStripMenuItem.BackgroundImageLayout = this.BackgroundImageLayout;
			toolStripMenuItem.Checked = this.Checked;
			toolStripMenuItem.CheckOnClick = this.CheckOnClick;
			toolStripMenuItem.CheckState = this.CheckState;
			toolStripMenuItem.DisplayStyle = this.DisplayStyle;
			toolStripMenuItem.Dock = base.Dock;
			toolStripMenuItem.DoubleClickEnabled = base.DoubleClickEnabled;
			toolStripMenuItem.Enabled = this.Enabled;
			toolStripMenuItem.Font = this.Font;
			toolStripMenuItem.ForeColor = this.ForeColor;
			toolStripMenuItem.Image = this.Image;
			toolStripMenuItem.ImageAlign = base.ImageAlign;
			toolStripMenuItem.ImageScaling = base.ImageScaling;
			toolStripMenuItem.ImageTransparentColor = base.ImageTransparentColor;
			toolStripMenuItem.Margin = base.Margin;
			toolStripMenuItem.MergeAction = base.MergeAction;
			toolStripMenuItem.MergeIndex = base.MergeIndex;
			toolStripMenuItem.Name = base.Name;
			toolStripMenuItem.Overflow = this.Overflow;
			toolStripMenuItem.Padding = this.Padding;
			toolStripMenuItem.RightToLeft = this.RightToLeft;
			toolStripMenuItem.ShortcutKeys = this.ShortcutKeys;
			toolStripMenuItem.ShowShortcutKeys = this.ShowShortcutKeys;
			toolStripMenuItem.Tag = base.Tag;
			toolStripMenuItem.Text = this.Text;
			toolStripMenuItem.TextAlign = this.TextAlign;
			toolStripMenuItem.TextDirection = this.TextDirection;
			toolStripMenuItem.TextImageRelation = base.TextImageRelation;
			toolStripMenuItem.ToolTipText = base.ToolTipText;
			toolStripMenuItem.Visible = ((IArrangedElement)this).ParticipatesInLayout;
			if (!base.AutoSize)
			{
				toolStripMenuItem.Size = this.Size;
			}
			return toolStripMenuItem;
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x0011121F File Offset: 0x0010F41F
		// (set) Token: 0x06004410 RID: 17424 RVA: 0x0011FDB5 File Offset: 0x0011DFB5
		internal override int DeviceDpi
		{
			get
			{
				return base.DeviceDpi;
			}
			set
			{
				base.DeviceDpi = value;
				this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.defaultPadding, value);
				this.scaledDefaultDropDownPadding = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.defaultDropDownPadding, value);
				this.scaledCheckMarkBitmapSize = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.checkMarkBitmapSize, value);
			}
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x0011FDF4 File Offset: 0x0011DFF4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.lastOwner != null)
			{
				Keys shortcutKeys = this.ShortcutKeys;
				if (shortcutKeys != Keys.None && this.lastOwner.Shortcuts.ContainsKey(shortcutKeys))
				{
					this.lastOwner.Shortcuts.Remove(shortcutKeys);
				}
				this.lastOwner = null;
				if (this.MdiForm != null)
				{
					base.Properties.SetObject(ToolStripMenuItem.PropMdiForm, null);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x0011FE6C File Offset: 0x0011E06C
		private bool GetNativeMenuItemEnabled()
		{
			if (this.nativeMenuCommandID == -1 || this.nativeMenuHandle == IntPtr.Zero)
			{
				return false;
			}
			NativeMethods.MENUITEMINFO_T_RW menuiteminfo_T_RW = new NativeMethods.MENUITEMINFO_T_RW();
			menuiteminfo_T_RW.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T_RW));
			menuiteminfo_T_RW.fMask = 1;
			menuiteminfo_T_RW.fType = 1;
			menuiteminfo_T_RW.wID = this.nativeMenuCommandID;
			UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this, this.nativeMenuHandle), this.nativeMenuCommandID, false, menuiteminfo_T_RW);
			return (menuiteminfo_T_RW.fState & 3) == 0;
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x0011FEF0 File Offset: 0x0011E0F0
		private string GetNativeMenuItemTextAndShortcut()
		{
			if (this.nativeMenuCommandID == -1 || this.nativeMenuHandle == IntPtr.Zero)
			{
				return null;
			}
			string result = null;
			NativeMethods.MENUITEMINFO_T_RW menuiteminfo_T_RW = new NativeMethods.MENUITEMINFO_T_RW();
			menuiteminfo_T_RW.fMask = 64;
			menuiteminfo_T_RW.fType = 64;
			menuiteminfo_T_RW.wID = this.nativeMenuCommandID;
			menuiteminfo_T_RW.dwTypeData = IntPtr.Zero;
			UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this, this.nativeMenuHandle), this.nativeMenuCommandID, false, menuiteminfo_T_RW);
			if (menuiteminfo_T_RW.cch > 0)
			{
				menuiteminfo_T_RW.cch++;
				menuiteminfo_T_RW.wID = this.nativeMenuCommandID;
				IntPtr intPtr = Marshal.AllocCoTaskMem(menuiteminfo_T_RW.cch * Marshal.SystemDefaultCharSize);
				menuiteminfo_T_RW.dwTypeData = intPtr;
				try
				{
					UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this, this.nativeMenuHandle), this.nativeMenuCommandID, false, menuiteminfo_T_RW);
					if (menuiteminfo_T_RW.dwTypeData != IntPtr.Zero)
					{
						result = Marshal.PtrToStringAuto(menuiteminfo_T_RW.dwTypeData, menuiteminfo_T_RW.cch);
					}
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
				}
			}
			return result;
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x00120008 File Offset: 0x0011E208
		private Image GetNativeMenuItemImage()
		{
			if (this.nativeMenuCommandID == -1 || this.nativeMenuHandle == IntPtr.Zero)
			{
				return null;
			}
			NativeMethods.MENUITEMINFO_T_RW menuiteminfo_T_RW = new NativeMethods.MENUITEMINFO_T_RW();
			menuiteminfo_T_RW.fMask = 128;
			menuiteminfo_T_RW.fType = 128;
			menuiteminfo_T_RW.wID = this.nativeMenuCommandID;
			UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this, this.nativeMenuHandle), this.nativeMenuCommandID, false, menuiteminfo_T_RW);
			if (menuiteminfo_T_RW.hbmpItem != IntPtr.Zero && menuiteminfo_T_RW.hbmpItem.ToInt32() > 11)
			{
				return Image.FromHbitmap(menuiteminfo_T_RW.hbmpItem);
			}
			int num = -1;
			switch (menuiteminfo_T_RW.hbmpItem.ToInt32())
			{
			case 2:
			case 9:
				num = 3;
				break;
			case 3:
			case 7:
			case 11:
				num = 1;
				break;
			case 5:
			case 6:
			case 8:
				num = 0;
				break;
			case 10:
				num = 2;
				break;
			}
			if (num > -1)
			{
				Bitmap bitmap = new Bitmap(16, 16);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					ControlPaint.DrawCaptionButton(graphics, new Rectangle(Point.Empty, bitmap.Size), (CaptionButton)num, ButtonState.Flat);
					graphics.DrawRectangle(SystemPens.Control, 0, 0, bitmap.Width - 1, bitmap.Height - 1);
				}
				bitmap.MakeTransparent(SystemColors.Control);
				return bitmap;
			}
			return null;
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x00120178 File Offset: 0x0011E378
		internal Size GetShortcutTextSize()
		{
			if (!this.ShowShortcutKeys)
			{
				return Size.Empty;
			}
			string shortcutText = this.GetShortcutText();
			if (string.IsNullOrEmpty(shortcutText))
			{
				return Size.Empty;
			}
			if (this.cachedShortcutSize == Size.Empty)
			{
				this.cachedShortcutSize = TextRenderer.MeasureText(shortcutText, this.Font);
			}
			return this.cachedShortcutSize;
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x001201D2 File Offset: 0x0011E3D2
		internal string GetShortcutText()
		{
			if (this.cachedShortcutText == null)
			{
				this.cachedShortcutText = ToolStripMenuItem.ShortcutToText(this.ShortcutKeys, this.ShortcutKeyDisplayString);
			}
			return this.cachedShortcutText;
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x001201FC File Offset: 0x0011E3FC
		internal void HandleAutoExpansion()
		{
			if (this.Enabled && base.ParentInternal != null && base.ParentInternal.MenuAutoExpand && this.HasDropDownItems)
			{
				base.ShowDropDown();
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
				}
				base.DropDown.SelectNextToolStripItem(null, true);
			}
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x00120254 File Offset: 0x0011E454
		protected override void OnClick(EventArgs e)
		{
			if (this.checkOnClick)
			{
				this.Checked = !this.Checked;
			}
			base.OnClick(e);
			if (this.nativeMenuCommandID != -1)
			{
				if ((this.nativeMenuCommandID & 61440) != 0)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, this.targetWindowHandle), 274, this.nativeMenuCommandID, 0);
				}
				else
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, this.targetWindowHandle), 273, this.nativeMenuCommandID, 0);
				}
				base.Invalidate();
			}
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x001202DC File Offset: 0x0011E4DC
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripMenuItem.EventCheckedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x0012030C File Offset: 0x0011E50C
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange);
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripMenuItem.EventCheckStateChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x00120345 File Offset: 0x0011E545
		protected override void OnDropDownHide(EventArgs e)
		{
			ToolStripMenuItem.MenuTimer.Cancel(this);
			base.OnDropDownHide(e);
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x00120359 File Offset: 0x0011E559
		protected override void OnDropDownShow(EventArgs e)
		{
			ToolStripMenuItem.MenuTimer.Cancel(this);
			if (base.ParentInternal != null)
			{
				base.ParentInternal.MenuAutoExpand = true;
			}
			base.OnDropDownShow(e);
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x00120381 File Offset: 0x0011E581
		protected override void OnFontChanged(EventArgs e)
		{
			this.ClearShortcutCache();
			base.OnFontChanged(e);
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x00120390 File Offset: 0x0011E590
		internal void OnMenuAutoExpand()
		{
			base.ShowDropDown();
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x00120398 File Offset: 0x0011E598
		protected override void OnMouseDown(MouseEventArgs e)
		{
			ToolStripMenuItem.MenuTimer.Cancel(this);
			this.OnMouseButtonStateChange(e, true);
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x001203AD File Offset: 0x0011E5AD
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.OnMouseButtonStateChange(e, false);
			base.OnMouseUp(e);
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x001203C0 File Offset: 0x0011E5C0
		private void OnMouseButtonStateChange(MouseEventArgs e, bool isMouseDown)
		{
			bool flag = true;
			if (base.IsOnDropDown)
			{
				ToolStripDropDown currentParentDropDown = base.GetCurrentParentDropDown();
				base.SupportsRightClick = (currentParentDropDown.GetFirstDropDown() is ContextMenuStrip);
			}
			else
			{
				flag = !base.DropDown.Visible;
				base.SupportsRightClick = false;
			}
			if (e.Button == MouseButtons.Left || (e.Button == MouseButtons.Right && base.SupportsRightClick))
			{
				if (isMouseDown && flag)
				{
					this.openMouseId = ((base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId());
					base.ShowDropDown(true);
					return;
				}
				if (!isMouseDown && !flag)
				{
					byte b = (base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId();
					int num = (int)this.openMouseId;
					if ((int)b != num)
					{
						this.openMouseId = 0;
						ToolStripManager.ModalMenuFilter.CloseActiveDropDown(base.DropDown, ToolStripDropDownCloseReason.AppClicked);
						base.Select();
					}
				}
			}
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x00120496 File Offset: 0x0011E696
		protected override void OnMouseEnter(EventArgs e)
		{
			if (base.ParentInternal != null && base.ParentInternal.MenuAutoExpand && this.Selected)
			{
				ToolStripMenuItem.MenuTimer.Cancel(this);
				ToolStripMenuItem.MenuTimer.Start(this);
			}
			base.OnMouseEnter(e);
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x001204D2 File Offset: 0x0011E6D2
		protected override void OnMouseLeave(EventArgs e)
		{
			ToolStripMenuItem.MenuTimer.Cancel(this);
			base.OnMouseLeave(e);
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x001204E8 File Offset: 0x0011E6E8
		protected override void OnOwnerChanged(EventArgs e)
		{
			Keys shortcutKeys = this.ShortcutKeys;
			if (shortcutKeys != Keys.None)
			{
				if (this.lastOwner != null)
				{
					this.lastOwner.Shortcuts.Remove(shortcutKeys);
				}
				if (base.Owner != null)
				{
					if (base.Owner.Shortcuts.Contains(shortcutKeys))
					{
						base.Owner.Shortcuts[shortcutKeys] = this;
					}
					else
					{
						base.Owner.Shortcuts.Add(shortcutKeys, this);
					}
					this.lastOwner = base.Owner;
				}
			}
			base.OnOwnerChanged(e);
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x00120580 File Offset: 0x0011E780
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				Graphics graphics = e.Graphics;
				renderer.DrawMenuItemBackground(new ToolStripItemRenderEventArgs(graphics, this));
				Color textColor = SystemColors.MenuText;
				if (base.IsForeColorSet)
				{
					textColor = this.ForeColor;
				}
				else if (!this.IsTopLevel || ToolStripManager.VisualStylesEnabled)
				{
					if (this.Selected || this.Pressed)
					{
						textColor = SystemColors.HighlightText;
					}
					else
					{
						textColor = SystemColors.MenuText;
					}
				}
				bool flag = this.RightToLeft == RightToLeft.Yes;
				ToolStripMenuItemInternalLayout toolStripMenuItemInternalLayout = base.InternalLayout as ToolStripMenuItemInternalLayout;
				if (toolStripMenuItemInternalLayout != null && toolStripMenuItemInternalLayout.UseMenuLayout)
				{
					if (this.CheckState != CheckState.Unchecked && toolStripMenuItemInternalLayout.PaintCheck)
					{
						Rectangle imageRectangle = toolStripMenuItemInternalLayout.CheckRectangle;
						if (!toolStripMenuItemInternalLayout.ShowCheckMargin)
						{
							imageRectangle = toolStripMenuItemInternalLayout.ImageRectangle;
						}
						if (imageRectangle.Width != 0)
						{
							renderer.DrawItemCheck(new ToolStripItemImageRenderEventArgs(graphics, this, this.CheckedImage, imageRectangle));
						}
					}
					if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
					{
						renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.Text, base.InternalLayout.TextRectangle, textColor, this.Font, flag ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft));
						bool flag2 = this.ShowShortcutKeys;
						if (!base.DesignMode)
						{
							flag2 = (flag2 && !this.HasDropDownItems);
						}
						if (flag2)
						{
							renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.GetShortcutText(), base.InternalLayout.TextRectangle, textColor, this.Font, flag ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleRight));
						}
					}
					if (this.HasDropDownItems)
					{
						ArrowDirection arrowDirection = flag ? ArrowDirection.Left : ArrowDirection.Right;
						Color color = (this.Selected || this.Pressed) ? SystemColors.HighlightText : SystemColors.MenuText;
						color = (this.Enabled ? color : SystemColors.ControlDark);
						renderer.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, this, toolStripMenuItemInternalLayout.ArrowRectangle, color, arrowDirection));
					}
					if (toolStripMenuItemInternalLayout.PaintImage && (this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image && this.Image != null)
					{
						renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(graphics, this, base.InternalLayout.ImageRectangle));
						return;
					}
				}
				else
				{
					if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
					{
						renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.Text, base.InternalLayout.TextRectangle, textColor, this.Font, base.InternalLayout.TextFormat));
					}
					if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image && this.Image != null)
					{
						renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(graphics, this, base.InternalLayout.ImageRectangle));
					}
				}
			}
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x001207F7 File Offset: 0x0011E9F7
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (this.Enabled && this.ShortcutKeys == keyData && !this.HasDropDownItems)
			{
				base.FireEvent(ToolStripItemEventType.Click);
				return true;
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x00120823 File Offset: 0x0011EA23
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (this.HasDropDownItems)
			{
				base.Select();
				base.ShowDropDown();
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
				}
				base.DropDown.SelectNextToolStripItem(null, true);
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x00120864 File Offset: 0x0011EA64
		protected internal override void SetBounds(Rectangle rect)
		{
			ToolStripMenuItemInternalLayout toolStripMenuItemInternalLayout = base.InternalLayout as ToolStripMenuItemInternalLayout;
			if (toolStripMenuItemInternalLayout != null && toolStripMenuItemInternalLayout.UseMenuLayout)
			{
				ToolStripDropDownMenu toolStripDropDownMenu = base.Owner as ToolStripDropDownMenu;
				if (toolStripDropDownMenu != null)
				{
					rect.X -= toolStripDropDownMenu.Padding.Left;
					rect.X = Math.Max(rect.X, 0);
				}
			}
			base.SetBounds(rect);
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x001208CE File Offset: 0x0011EACE
		internal void SetNativeTargetWindow(IWin32Window window)
		{
			this.targetWindowHandle = Control.GetSafeHandle(window);
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x001208DC File Offset: 0x0011EADC
		internal void SetNativeTargetMenu(IntPtr hMenu)
		{
			this.nativeMenuHandle = hMenu;
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x001208E5 File Offset: 0x0011EAE5
		internal static string ShortcutToText(Keys shortcutKeys, string shortcutKeyDisplayString)
		{
			if (!string.IsNullOrEmpty(shortcutKeyDisplayString))
			{
				return shortcutKeyDisplayString;
			}
			if (shortcutKeys == Keys.None)
			{
				return string.Empty;
			}
			return TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(shortcutKeys);
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x00120914 File Offset: 0x0011EB14
		internal override bool IsBeingTabbedTo()
		{
			return base.IsBeingTabbedTo() || ToolStripManager.ModalMenuFilter.InMenuMode;
		}

		// Token: 0x04002602 RID: 9730
		private static MenuTimer menuTimer = new MenuTimer();

		// Token: 0x04002603 RID: 9731
		private static readonly int PropShortcutKeys = PropertyStore.CreateKey();

		// Token: 0x04002604 RID: 9732
		private static readonly int PropCheckState = PropertyStore.CreateKey();

		// Token: 0x04002605 RID: 9733
		private static readonly int PropMdiForm = PropertyStore.CreateKey();

		// Token: 0x04002606 RID: 9734
		private bool checkOnClick;

		// Token: 0x04002607 RID: 9735
		private bool showShortcutKeys = true;

		// Token: 0x04002608 RID: 9736
		private ToolStrip lastOwner;

		// Token: 0x04002609 RID: 9737
		private int nativeMenuCommandID = -1;

		// Token: 0x0400260A RID: 9738
		private IntPtr targetWindowHandle = IntPtr.Zero;

		// Token: 0x0400260B RID: 9739
		private IntPtr nativeMenuHandle = IntPtr.Zero;

		// Token: 0x0400260C RID: 9740
		[ThreadStatic]
		private static Image indeterminateCheckedImage;

		// Token: 0x0400260D RID: 9741
		[ThreadStatic]
		private static Image checkedImage;

		// Token: 0x0400260E RID: 9742
		private string shortcutKeyDisplayString;

		// Token: 0x0400260F RID: 9743
		private string cachedShortcutText;

		// Token: 0x04002610 RID: 9744
		private Size cachedShortcutSize = Size.Empty;

		// Token: 0x04002611 RID: 9745
		private static readonly Padding defaultPadding = new Padding(4, 0, 4, 0);

		// Token: 0x04002612 RID: 9746
		private static readonly Padding defaultDropDownPadding = new Padding(0, 1, 0, 1);

		// Token: 0x04002613 RID: 9747
		private static readonly Size checkMarkBitmapSize = new Size(16, 16);

		// Token: 0x04002614 RID: 9748
		private Padding scaledDefaultPadding = ToolStripMenuItem.defaultPadding;

		// Token: 0x04002615 RID: 9749
		private Padding scaledDefaultDropDownPadding = ToolStripMenuItem.defaultDropDownPadding;

		// Token: 0x04002616 RID: 9750
		private Size scaledCheckMarkBitmapSize = ToolStripMenuItem.checkMarkBitmapSize;

		// Token: 0x04002617 RID: 9751
		private byte openMouseId;

		// Token: 0x04002618 RID: 9752
		private static readonly object EventCheckedChanged = new object();

		// Token: 0x04002619 RID: 9753
		private static readonly object EventCheckStateChanged = new object();

		// Token: 0x0200080B RID: 2059
		[ComVisible(true)]
		internal class ToolStripMenuItemAccessibleObject : ToolStripDropDownItemAccessibleObject
		{
			// Token: 0x06006F42 RID: 28482 RVA: 0x00198470 File Offset: 0x00196670
			public ToolStripMenuItemAccessibleObject(ToolStripMenuItem ownerItem) : base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006F43 RID: 28483 RVA: 0x00198480 File Offset: 0x00196680
			internal override void ClearOwnerItem()
			{
				this.ownerItem = null;
				base.ClearOwnerItem();
			}

			// Token: 0x17001852 RID: 6226
			// (get) Token: 0x06006F44 RID: 28484 RVA: 0x00198490 File Offset: 0x00196690
			public override AccessibleStates State
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return AccessibleStates.None;
					}
					if (this.ownerItem.Enabled)
					{
						AccessibleStates accessibleStates = base.State;
						if ((accessibleStates & AccessibleStates.Pressed) == AccessibleStates.Pressed)
						{
							accessibleStates &= ~AccessibleStates.Pressed;
						}
						if (this.ownerItem.Checked)
						{
							accessibleStates |= AccessibleStates.Checked;
						}
						return accessibleStates;
					}
					return base.State;
				}
			}

			// Token: 0x06006F45 RID: 28485 RVA: 0x001984E0 File Offset: 0x001966E0
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50011;
				}
				if (!AccessibilityImprovements.Level2 || propertyID != 30006)
				{
					return base.GetPropertyValue(propertyID);
				}
				if (!base.IsOwnerItemCleared())
				{
					return this.ownerItem.GetShortcutText();
				}
				return string.Empty;
			}

			// Token: 0x0400431B RID: 17179
			private ToolStripMenuItem ownerItem;
		}
	}
}
