using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000404 RID: 1028
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	[DefaultEvent("ButtonClick")]
	public class ToolStripSplitButton : ToolStripDropDownItem
	{
		// Token: 0x060046EA RID: 18154 RVA: 0x001298B0 File Offset: 0x00127AB0
		public ToolStripSplitButton()
		{
			this.Initialize();
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x001298DB File Offset: 0x00127ADB
		public ToolStripSplitButton(string text) : base(text, null, null)
		{
			this.Initialize();
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x00129909 File Offset: 0x00127B09
		public ToolStripSplitButton(Image image) : base(null, image, null)
		{
			this.Initialize();
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00129937 File Offset: 0x00127B37
		public ToolStripSplitButton(string text, Image image) : base(text, image, null)
		{
			this.Initialize();
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x00129965 File Offset: 0x00127B65
		public ToolStripSplitButton(string text, Image image, EventHandler onClick) : base(text, image, onClick)
		{
			this.Initialize();
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x00129993 File Offset: 0x00127B93
		public ToolStripSplitButton(string text, Image image, EventHandler onClick, string name) : base(text, image, onClick, name)
		{
			this.Initialize();
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x001299C3 File Offset: 0x00127BC3
		public ToolStripSplitButton(string text, Image image, params ToolStripItem[] dropDownItems) : base(text, image, dropDownItems)
		{
			this.Initialize();
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x060046F1 RID: 18161 RVA: 0x00111120 File Offset: 0x0010F320
		// (set) Token: 0x060046F2 RID: 18162 RVA: 0x00111128 File Offset: 0x0010F328
		[DefaultValue(true)]
		public new bool AutoToolTip
		{
			get
			{
				return base.AutoToolTip;
			}
			set
			{
				base.AutoToolTip = value;
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x060046F3 RID: 18163 RVA: 0x001299F1 File Offset: 0x00127BF1
		[Browsable(false)]
		public Rectangle ButtonBounds
		{
			get
			{
				return this.SplitButtonButton.Bounds;
			}
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x001299FE File Offset: 0x00127BFE
		[Browsable(false)]
		public bool ButtonPressed
		{
			get
			{
				return this.SplitButtonButton.Pressed;
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x00129A0B File Offset: 0x00127C0B
		[Browsable(false)]
		public bool ButtonSelected
		{
			get
			{
				return this.SplitButtonButton.Selected || this.DropDownButtonPressed;
			}
		}

		// Token: 0x1400038B RID: 907
		// (add) Token: 0x060046F6 RID: 18166 RVA: 0x00129A22 File Offset: 0x00127C22
		// (remove) Token: 0x060046F7 RID: 18167 RVA: 0x00129A35 File Offset: 0x00127C35
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnButtonClickDescr")]
		public event EventHandler ButtonClick
		{
			add
			{
				base.Events.AddHandler(ToolStripSplitButton.EventButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripSplitButton.EventButtonClick, value);
			}
		}

		// Token: 0x1400038C RID: 908
		// (add) Token: 0x060046F8 RID: 18168 RVA: 0x00129A48 File Offset: 0x00127C48
		// (remove) Token: 0x060046F9 RID: 18169 RVA: 0x00129A5B File Offset: 0x00127C5B
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnButtonDoubleClickDescr")]
		public event EventHandler ButtonDoubleClick
		{
			add
			{
				base.Events.AddHandler(ToolStripSplitButton.EventButtonDoubleClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripSplitButton.EventButtonDoubleClick, value);
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x00013062 File Offset: 0x00011262
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x00129A6E File Offset: 0x00127C6E
		// (set) Token: 0x060046FC RID: 18172 RVA: 0x00129A76 File Offset: 0x00127C76
		[DefaultValue(null)]
		[Browsable(false)]
		public ToolStripItem DefaultItem
		{
			get
			{
				return this.defaultItem;
			}
			set
			{
				if (this.defaultItem != value)
				{
					this.OnDefaultItemChanged(new EventArgs());
					this.defaultItem = value;
				}
			}
		}

		// Token: 0x1400038D RID: 909
		// (add) Token: 0x060046FD RID: 18173 RVA: 0x00129A93 File Offset: 0x00127C93
		// (remove) Token: 0x060046FE RID: 18174 RVA: 0x00129AA6 File Offset: 0x00127CA6
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnDefaultItemChangedDescr")]
		public event EventHandler DefaultItemChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripSplitButton.EventDefaultItemChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripSplitButton.EventDefaultItemChanged, value);
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x00129AB9 File Offset: 0x00127CB9
		protected internal override bool DismissWhenClicked
		{
			get
			{
				return !base.DropDown.Visible;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06004700 RID: 18176 RVA: 0x00129AC9 File Offset: 0x00127CC9
		internal override Rectangle DropDownButtonArea
		{
			get
			{
				return this.DropDownButtonBounds;
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06004701 RID: 18177 RVA: 0x00129AD1 File Offset: 0x00127CD1
		[Browsable(false)]
		public Rectangle DropDownButtonBounds
		{
			get
			{
				return this.dropDownButtonBounds;
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06004702 RID: 18178 RVA: 0x00129AD9 File Offset: 0x00127CD9
		[Browsable(false)]
		public bool DropDownButtonPressed
		{
			get
			{
				return base.DropDown.Visible;
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06004703 RID: 18179 RVA: 0x00129AE6 File Offset: 0x00127CE6
		[Browsable(false)]
		public bool DropDownButtonSelected
		{
			get
			{
				return this.Selected;
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06004704 RID: 18180 RVA: 0x00129AEE File Offset: 0x00127CEE
		// (set) Token: 0x06004705 RID: 18181 RVA: 0x00129AF8 File Offset: 0x00127CF8
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripSplitButtonDropDownButtonWidthDescr")]
		public int DropDownButtonWidth
		{
			get
			{
				return this.dropDownButtonWidth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("DropDownButtonWidth", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"DropDownButtonWidth",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.dropDownButtonWidth != value)
				{
					this.dropDownButtonWidth = value;
					this.InvalidateSplitButtonLayout();
					base.InvalidateItemLayout(PropertyNames.DropDownButtonWidth, true);
				}
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06004706 RID: 18182 RVA: 0x00129B6E File Offset: 0x00127D6E
		private int DefaultDropDownButtonWidth
		{
			get
			{
				if (!ToolStripSplitButton.isScalingInitialized)
				{
					if (DpiHelper.IsScalingRequired)
					{
						ToolStripSplitButton.scaledDropDownButtonWidth = DpiHelper.LogicalToDeviceUnitsX(11);
					}
					ToolStripSplitButton.isScalingInitialized = true;
				}
				return ToolStripSplitButton.scaledDropDownButtonWidth;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06004707 RID: 18183 RVA: 0x00129B98 File Offset: 0x00127D98
		private ToolStripSplitButton.ToolStripSplitButtonButton SplitButtonButton
		{
			get
			{
				if (this.splitButtonButton == null)
				{
					this.splitButtonButton = new ToolStripSplitButton.ToolStripSplitButtonButton(this);
				}
				this.splitButtonButton.Image = this.Image;
				this.splitButtonButton.Text = this.Text;
				this.splitButtonButton.BackColor = this.BackColor;
				this.splitButtonButton.ForeColor = this.ForeColor;
				this.splitButtonButton.Font = this.Font;
				this.splitButtonButton.ImageAlign = base.ImageAlign;
				this.splitButtonButton.TextAlign = this.TextAlign;
				this.splitButtonButton.TextImageRelation = base.TextImageRelation;
				return this.splitButtonButton;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06004708 RID: 18184 RVA: 0x00129C47 File Offset: 0x00127E47
		internal ToolStripItemInternalLayout SplitButtonButtonLayout
		{
			get
			{
				if (base.InternalLayout != null && this.splitButtonButtonLayout == null)
				{
					this.splitButtonButtonLayout = new ToolStripSplitButton.ToolStripSplitButtonButtonLayout(this);
				}
				return this.splitButtonButtonLayout;
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06004709 RID: 18185 RVA: 0x00129C6B File Offset: 0x00127E6B
		// (set) Token: 0x0600470A RID: 18186 RVA: 0x00129C73 File Offset: 0x00127E73
		[SRDescription("ToolStripSplitButtonSplitterWidthDescr")]
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal int SplitterWidth
		{
			get
			{
				return this.splitterWidth;
			}
			set
			{
				if (value < 0)
				{
					this.splitterWidth = 0;
				}
				else
				{
					this.splitterWidth = value;
				}
				this.InvalidateSplitButtonLayout();
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x0600470B RID: 18187 RVA: 0x00129C8F File Offset: 0x00127E8F
		[Browsable(false)]
		public Rectangle SplitterBounds
		{
			get
			{
				return this.splitterBounds;
			}
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x00129C98 File Offset: 0x00127E98
		private void CalculateLayout()
		{
			Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
			Rectangle empty = Rectangle.Empty;
			rectangle = new Rectangle(Point.Empty, new Size(Math.Min(base.Width, this.DropDownButtonWidth), base.Height));
			int width = Math.Max(0, base.Width - rectangle.Width);
			int height = Math.Max(0, base.Height);
			empty = new Rectangle(Point.Empty, new Size(width, height));
			empty.Width -= this.splitterWidth;
			if (this.RightToLeft == RightToLeft.No)
			{
				rectangle.Offset(empty.Right + this.splitterWidth, 0);
				this.splitterBounds = new Rectangle(empty.Right, empty.Top, this.splitterWidth, empty.Height);
			}
			else
			{
				empty.Offset(this.DropDownButtonWidth + this.splitterWidth, 0);
				this.splitterBounds = new Rectangle(rectangle.Right, rectangle.Top, this.splitterWidth, rectangle.Height);
			}
			this.SplitButtonButton.SetBounds(empty);
			this.SetDropDownButtonBounds(rectangle);
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x00129DC2 File Offset: 0x00127FC2
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripSplitButton.ToolStripSplitButtonUiaProvider(this);
			}
			if (AccessibilityImprovements.Level1)
			{
				return new ToolStripSplitButton.ToolStripSplitButtonExAccessibleObject(this);
			}
			return new ToolStripSplitButton.ToolStripSplitButtonAccessibleObject(this);
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x00114B4C File Offset: 0x00112D4C
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu(this, true);
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x00129DE6 File Offset: 0x00127FE6
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			this.splitButtonButtonLayout = null;
			return new ToolStripItemInternalLayout(this);
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x00129DF8 File Offset: 0x00127FF8
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size preferredSize = this.SplitButtonButtonLayout.GetPreferredSize(constrainingSize);
			preferredSize.Width += this.DropDownButtonWidth + this.SplitterWidth + this.Padding.Horizontal;
			return preferredSize;
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x00129E3D File Offset: 0x0012803D
		private void InvalidateSplitButtonLayout()
		{
			this.splitButtonButtonLayout = null;
			this.CalculateLayout();
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x00129E4C File Offset: 0x0012804C
		private void Initialize()
		{
			this.dropDownButtonWidth = this.DefaultDropDownButtonWidth;
			base.SupportsSpaceKey = true;
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x00129E61 File Offset: 0x00128061
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessDialogKey(Keys keyData)
		{
			if (this.Enabled && (keyData == Keys.Return || (base.SupportsSpaceKey && keyData == Keys.Space)))
			{
				this.PerformButtonClick();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00129E8C File Offset: 0x0012808C
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			this.PerformButtonClick();
			return true;
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x00129E98 File Offset: 0x00128098
		protected virtual void OnButtonClick(EventArgs e)
		{
			if (this.DefaultItem != null)
			{
				this.DefaultItem.FireEvent(ToolStripItemEventType.Click);
			}
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripSplitButton.EventButtonClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x00129EDC File Offset: 0x001280DC
		public virtual void OnButtonDoubleClick(EventArgs e)
		{
			if (this.DefaultItem != null)
			{
				this.DefaultItem.FireEvent(ToolStripItemEventType.DoubleClick);
			}
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripSplitButton.EventButtonDoubleClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x00129F20 File Offset: 0x00128120
		protected virtual void OnDefaultItemChanged(EventArgs e)
		{
			this.InvalidateSplitButtonLayout();
			if (this.CanRaiseEvents)
			{
				EventHandler eventHandler = base.Events[ToolStripSplitButton.EventDefaultItemChanged] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x00129F5C File Offset: 0x0012815C
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.DropDownButtonBounds.Contains(e.Location))
			{
				if (e.Button == MouseButtons.Left && !base.DropDown.Visible)
				{
					this.openMouseId = ((base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId());
					base.ShowDropDown(true);
					return;
				}
			}
			else
			{
				this.SplitButtonButton.Push(true);
			}
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x00129FCC File Offset: 0x001281CC
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (!this.Enabled)
			{
				return;
			}
			this.SplitButtonButton.Push(false);
			if (this.DropDownButtonBounds.Contains(e.Location) && e.Button == MouseButtons.Left && base.DropDown.Visible)
			{
				byte b = (base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId();
				if (b != this.openMouseId)
				{
					this.openMouseId = 0;
					ToolStripManager.ModalMenuFilter.CloseActiveDropDown(base.DropDown, ToolStripDropDownCloseReason.AppClicked);
					base.Select();
				}
			}
			Point pt = new Point(e.X, e.Y);
			if (e.Button == MouseButtons.Left && this.SplitButtonButton.Bounds.Contains(pt))
			{
				bool flag = false;
				if (base.DoubleClickEnabled)
				{
					long ticks = DateTime.Now.Ticks;
					long num = ticks - this.lastClickTime;
					this.lastClickTime = ticks;
					if (num >= 0L && num < ToolStripItem.DoubleClickTicks)
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.OnButtonDoubleClick(new EventArgs());
					this.lastClickTime = 0L;
					return;
				}
				this.OnButtonClick(new EventArgs());
			}
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x0012A0ED File Offset: 0x001282ED
		protected override void OnMouseLeave(EventArgs e)
		{
			this.openMouseId = 0;
			this.SplitButtonButton.Push(false);
			base.OnMouseLeave(e);
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x0012A109 File Offset: 0x00128309
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			this.InvalidateSplitButtonLayout();
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x0012A118 File Offset: 0x00128318
		protected override void OnPaint(PaintEventArgs e)
		{
			ToolStripRenderer renderer = base.Renderer;
			if (renderer != null)
			{
				this.InvalidateSplitButtonLayout();
				Graphics graphics = e.Graphics;
				renderer.DrawSplitButton(new ToolStripItemRenderEventArgs(graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) != ToolStripItemDisplayStyle.None)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(graphics, this, this.SplitButtonButtonLayout.ImageRectangle));
				}
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) != ToolStripItemDisplayStyle.None)
				{
					renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.SplitButtonButton.Text, this.SplitButtonButtonLayout.TextRectangle, this.ForeColor, this.Font, this.SplitButtonButtonLayout.TextFormat));
				}
			}
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x0012A1B2 File Offset: 0x001283B2
		public void PerformButtonClick()
		{
			if (this.Enabled && base.Available)
			{
				base.PerformClick();
				this.OnButtonClick(EventArgs.Empty);
			}
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x0012A1D5 File Offset: 0x001283D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetDropDownButtonWidth()
		{
			this.DropDownButtonWidth = this.DefaultDropDownButtonWidth;
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x0012A1E3 File Offset: 0x001283E3
		private void SetDropDownButtonBounds(Rectangle rect)
		{
			this.dropDownButtonBounds = rect;
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x0012A1EC File Offset: 0x001283EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeDropDownButtonWidth()
		{
			return this.DropDownButtonWidth != this.DefaultDropDownButtonWidth;
		}

		// Token: 0x040026C0 RID: 9920
		private ToolStripItem defaultItem;

		// Token: 0x040026C1 RID: 9921
		private ToolStripSplitButton.ToolStripSplitButtonButton splitButtonButton;

		// Token: 0x040026C2 RID: 9922
		private Rectangle dropDownButtonBounds = Rectangle.Empty;

		// Token: 0x040026C3 RID: 9923
		private ToolStripSplitButton.ToolStripSplitButtonButtonLayout splitButtonButtonLayout;

		// Token: 0x040026C4 RID: 9924
		private int dropDownButtonWidth;

		// Token: 0x040026C5 RID: 9925
		private int splitterWidth = 1;

		// Token: 0x040026C6 RID: 9926
		private Rectangle splitterBounds = Rectangle.Empty;

		// Token: 0x040026C7 RID: 9927
		private byte openMouseId;

		// Token: 0x040026C8 RID: 9928
		private long lastClickTime;

		// Token: 0x040026C9 RID: 9929
		private const int DEFAULT_DROPDOWN_WIDTH = 11;

		// Token: 0x040026CA RID: 9930
		private static readonly object EventDefaultItemChanged = new object();

		// Token: 0x040026CB RID: 9931
		private static readonly object EventButtonClick = new object();

		// Token: 0x040026CC RID: 9932
		private static readonly object EventButtonDoubleClick = new object();

		// Token: 0x040026CD RID: 9933
		private static readonly object EventDropDownOpened = new object();

		// Token: 0x040026CE RID: 9934
		private static readonly object EventDropDownClosed = new object();

		// Token: 0x040026CF RID: 9935
		private static bool isScalingInitialized = false;

		// Token: 0x040026D0 RID: 9936
		private static int scaledDropDownButtonWidth = 11;

		// Token: 0x0200081C RID: 2076
		private class ToolStripSplitButtonButton : ToolStripButton
		{
			// Token: 0x06006FD4 RID: 28628 RVA: 0x0019AF9D File Offset: 0x0019919D
			public ToolStripSplitButtonButton(ToolStripSplitButton owner)
			{
				this.owner = owner;
			}

			// Token: 0x1700186D RID: 6253
			// (get) Token: 0x06006FD5 RID: 28629 RVA: 0x0019AFAC File Offset: 0x001991AC
			// (set) Token: 0x06006FD6 RID: 28630 RVA: 0x000072B6 File Offset: 0x000054B6
			public override bool Enabled
			{
				get
				{
					return this.owner.Enabled;
				}
				set
				{
				}
			}

			// Token: 0x1700186E RID: 6254
			// (get) Token: 0x06006FD7 RID: 28631 RVA: 0x0019AFB9 File Offset: 0x001991B9
			// (set) Token: 0x06006FD8 RID: 28632 RVA: 0x000072B6 File Offset: 0x000054B6
			public override ToolStripItemDisplayStyle DisplayStyle
			{
				get
				{
					return this.owner.DisplayStyle;
				}
				set
				{
				}
			}

			// Token: 0x1700186F RID: 6255
			// (get) Token: 0x06006FD9 RID: 28633 RVA: 0x0019AFC6 File Offset: 0x001991C6
			// (set) Token: 0x06006FDA RID: 28634 RVA: 0x000072B6 File Offset: 0x000054B6
			public override Padding Padding
			{
				get
				{
					return this.owner.Padding;
				}
				set
				{
				}
			}

			// Token: 0x17001870 RID: 6256
			// (get) Token: 0x06006FDB RID: 28635 RVA: 0x0019AFD3 File Offset: 0x001991D3
			public override ToolStripTextDirection TextDirection
			{
				get
				{
					return this.owner.TextDirection;
				}
			}

			// Token: 0x17001871 RID: 6257
			// (get) Token: 0x06006FDC RID: 28636 RVA: 0x0019AFE0 File Offset: 0x001991E0
			// (set) Token: 0x06006FDD RID: 28637 RVA: 0x000072B6 File Offset: 0x000054B6
			public override Image Image
			{
				get
				{
					if ((this.owner.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
					{
						return this.owner.Image;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x17001872 RID: 6258
			// (get) Token: 0x06006FDE RID: 28638 RVA: 0x0019AFFF File Offset: 0x001991FF
			public override bool Selected
			{
				get
				{
					if (this.owner != null)
					{
						return this.owner.Selected;
					}
					return base.Selected;
				}
			}

			// Token: 0x17001873 RID: 6259
			// (get) Token: 0x06006FDF RID: 28639 RVA: 0x0019B01B File Offset: 0x0019921B
			// (set) Token: 0x06006FE0 RID: 28640 RVA: 0x000072B6 File Offset: 0x000054B6
			public override string Text
			{
				get
				{
					if ((this.owner.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
					{
						return this.owner.Text;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x04004332 RID: 17202
			private ToolStripSplitButton owner;
		}

		// Token: 0x0200081D RID: 2077
		private class ToolStripSplitButtonButtonLayout : ToolStripItemInternalLayout
		{
			// Token: 0x06006FE1 RID: 28641 RVA: 0x0019B03A File Offset: 0x0019923A
			public ToolStripSplitButtonButtonLayout(ToolStripSplitButton owner) : base(owner.SplitButtonButton)
			{
				this.owner = owner;
			}

			// Token: 0x17001874 RID: 6260
			// (get) Token: 0x06006FE2 RID: 28642 RVA: 0x0019B04F File Offset: 0x0019924F
			protected override ToolStripItem Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x17001875 RID: 6261
			// (get) Token: 0x06006FE3 RID: 28643 RVA: 0x0019B057 File Offset: 0x00199257
			protected override ToolStrip ParentInternal
			{
				get
				{
					return this.owner.ParentInternal;
				}
			}

			// Token: 0x17001876 RID: 6262
			// (get) Token: 0x06006FE4 RID: 28644 RVA: 0x0019B064 File Offset: 0x00199264
			public override Rectangle ImageRectangle
			{
				get
				{
					Rectangle imageRectangle = base.ImageRectangle;
					imageRectangle.Offset(this.owner.SplitButtonButton.Bounds.Location);
					return imageRectangle;
				}
			}

			// Token: 0x17001877 RID: 6263
			// (get) Token: 0x06006FE5 RID: 28645 RVA: 0x0019B098 File Offset: 0x00199298
			public override Rectangle TextRectangle
			{
				get
				{
					Rectangle textRectangle = base.TextRectangle;
					textRectangle.Offset(this.owner.SplitButtonButton.Bounds.Location);
					return textRectangle;
				}
			}

			// Token: 0x04004333 RID: 17203
			private ToolStripSplitButton owner;
		}

		// Token: 0x0200081E RID: 2078
		public class ToolStripSplitButtonAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006FE6 RID: 28646 RVA: 0x0019B0CC File Offset: 0x001992CC
			public ToolStripSplitButtonAccessibleObject(ToolStripSplitButton item) : base(item)
			{
				this.owner = item;
			}

			// Token: 0x06006FE7 RID: 28647 RVA: 0x0019B0DC File Offset: 0x001992DC
			internal override void ClearOwnerItem()
			{
				this.owner = null;
				base.ClearOwnerItem();
			}

			// Token: 0x06006FE8 RID: 28648 RVA: 0x0019B0EB File Offset: 0x001992EB
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (base.IsOwnerItemCleared())
				{
					return;
				}
				this.owner.PerformButtonClick();
			}

			// Token: 0x04004334 RID: 17204
			private ToolStripSplitButton owner;
		}

		// Token: 0x0200081F RID: 2079
		internal class ToolStripSplitButtonExAccessibleObject : ToolStripSplitButton.ToolStripSplitButtonAccessibleObject
		{
			// Token: 0x06006FE9 RID: 28649 RVA: 0x0019B101 File Offset: 0x00199301
			public ToolStripSplitButtonExAccessibleObject(ToolStripSplitButton item) : base(item)
			{
				this.ownerItem = item;
			}

			// Token: 0x06006FEA RID: 28650 RVA: 0x0019B111 File Offset: 0x00199311
			internal override void ClearOwnerItem()
			{
				this.ownerItem = null;
				base.ClearOwnerItem();
			}

			// Token: 0x06006FEB RID: 28651 RVA: 0x0019B120 File Offset: 0x00199320
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006FEC RID: 28652 RVA: 0x0019B13C File Offset: 0x0019933C
			internal override bool IsIAccessibleExSupported()
			{
				return this.ownerItem != null || (!base.IsOwnerItemCleared() && base.IsIAccessibleExSupported());
			}

			// Token: 0x06006FED RID: 28653 RVA: 0x0019B158 File Offset: 0x00199358
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerItemCleared() && ((patternId == 10005 && this.ownerItem.HasDropDownItems) || base.IsPatternSupported(patternId));
			}

			// Token: 0x06006FEE RID: 28654 RVA: 0x00016280 File Offset: 0x00014480
			internal override void Expand()
			{
				this.DoDefaultAction();
			}

			// Token: 0x06006FEF RID: 28655 RVA: 0x0019B182 File Offset: 0x00199382
			internal override void Collapse()
			{
				if (this.ownerItem != null && this.ownerItem.DropDown != null && this.ownerItem.DropDown.Visible)
				{
					this.ownerItem.DropDown.Close();
				}
			}

			// Token: 0x17001878 RID: 6264
			// (get) Token: 0x06006FF0 RID: 28656 RVA: 0x0019B1BB File Offset: 0x001993BB
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (base.IsOwnerItemCleared() || !this.ownerItem.DropDown.Visible)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					return UnsafeNativeMethods.ExpandCollapseState.Expanded;
				}
			}

			// Token: 0x06006FF1 RID: 28657 RVA: 0x0019B1DC File Offset: 0x001993DC
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerItemCleared())
				{
					return null;
				}
				if (direction != UnsafeNativeMethods.NavigateDirection.FirstChild)
				{
					if (direction != UnsafeNativeMethods.NavigateDirection.LastChild)
					{
						return base.FragmentNavigate(direction);
					}
					if (this.DropDownItemsCount <= 0)
					{
						return null;
					}
					return this.ownerItem.DropDown.Items[this.ownerItem.DropDown.Items.Count - 1].AccessibilityObject;
				}
				else
				{
					if (this.DropDownItemsCount <= 0)
					{
						return null;
					}
					return this.ownerItem.DropDown.Items[0].AccessibilityObject;
				}
			}

			// Token: 0x17001879 RID: 6265
			// (get) Token: 0x06006FF2 RID: 28658 RVA: 0x0019B268 File Offset: 0x00199468
			private int DropDownItemsCount
			{
				get
				{
					if (base.IsOwnerItemCleared() || (AccessibilityImprovements.Level3 && this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Collapsed))
					{
						return 0;
					}
					return this.ownerItem.DropDownItems.Count;
				}
			}

			// Token: 0x04004335 RID: 17205
			private ToolStripSplitButton ownerItem;
		}

		// Token: 0x02000820 RID: 2080
		internal class ToolStripSplitButtonUiaProvider : ToolStripDropDownItemAccessibleObject
		{
			// Token: 0x06006FF3 RID: 28659 RVA: 0x0019B293 File Offset: 0x00199493
			public ToolStripSplitButtonUiaProvider(ToolStripSplitButton owner) : base(owner)
			{
				this._accessibleObject = new ToolStripSplitButton.ToolStripSplitButtonExAccessibleObject(owner);
			}

			// Token: 0x06006FF4 RID: 28660 RVA: 0x0019B2A8 File Offset: 0x001994A8
			internal override void ClearOwnerItem()
			{
				ToolStripSplitButton.ToolStripSplitButtonExAccessibleObject accessibleObject = this._accessibleObject;
				if (accessibleObject != null)
				{
					accessibleObject.ClearOwnerItem();
				}
				this._accessibleObject = null;
				base.ClearOwnerItem();
			}

			// Token: 0x06006FF5 RID: 28661 RVA: 0x0019B2C8 File Offset: 0x001994C8
			public override void DoDefaultAction()
			{
				if (base.IsOwnerItemCleared())
				{
					return;
				}
				this._accessibleObject.DoDefaultAction();
			}

			// Token: 0x06006FF6 RID: 28662 RVA: 0x0019B2DE File Offset: 0x001994DE
			internal override object GetPropertyValue(int propertyID)
			{
				if (base.IsOwnerItemCleared())
				{
					return null;
				}
				return this._accessibleObject.GetPropertyValue(propertyID);
			}

			// Token: 0x06006FF7 RID: 28663 RVA: 0x0019B2F6 File Offset: 0x001994F6
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerItemCleared();
			}

			// Token: 0x06006FF8 RID: 28664 RVA: 0x0019B301 File Offset: 0x00199501
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerItemCleared() && this._accessibleObject.IsPatternSupported(patternId);
			}

			// Token: 0x06006FF9 RID: 28665 RVA: 0x00016280 File Offset: 0x00014480
			internal override void Expand()
			{
				this.DoDefaultAction();
			}

			// Token: 0x06006FFA RID: 28666 RVA: 0x0019B319 File Offset: 0x00199519
			internal override void Collapse()
			{
				if (base.IsOwnerItemCleared())
				{
					return;
				}
				this._accessibleObject.Collapse();
			}

			// Token: 0x1700187A RID: 6266
			// (get) Token: 0x06006FFB RID: 28667 RVA: 0x0019B32F File Offset: 0x0019952F
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					return this._accessibleObject.ExpandCollapseState;
				}
			}

			// Token: 0x06006FFC RID: 28668 RVA: 0x0019B346 File Offset: 0x00199546
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerItemCleared())
				{
					return null;
				}
				return this._accessibleObject.FragmentNavigate(direction);
			}

			// Token: 0x04004336 RID: 17206
			private ToolStripSplitButton.ToolStripSplitButtonExAccessibleObject _accessibleObject;
		}
	}
}
