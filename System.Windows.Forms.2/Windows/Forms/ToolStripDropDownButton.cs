using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003C0 RID: 960
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	public class ToolStripDropDownButton : ToolStripDropDownItem
	{
		// Token: 0x060040D5 RID: 16597 RVA: 0x00114A62 File Offset: 0x00112C62
		public ToolStripDropDownButton()
		{
			this.Initialize();
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x00114A77 File Offset: 0x00112C77
		public ToolStripDropDownButton(string text) : base(text, null, null)
		{
			this.Initialize();
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x00114A8F File Offset: 0x00112C8F
		public ToolStripDropDownButton(Image image) : base(null, image, null)
		{
			this.Initialize();
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x00114AA7 File Offset: 0x00112CA7
		public ToolStripDropDownButton(string text, Image image) : base(text, image, null)
		{
			this.Initialize();
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x00114ABF File Offset: 0x00112CBF
		public ToolStripDropDownButton(string text, Image image, EventHandler onClick) : base(text, image, onClick)
		{
			this.Initialize();
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x00114AD7 File Offset: 0x00112CD7
		public ToolStripDropDownButton(string text, Image image, EventHandler onClick, string name) : base(text, image, onClick, name)
		{
			this.Initialize();
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x00114AF1 File Offset: 0x00112CF1
		public ToolStripDropDownButton(string text, Image image, params ToolStripItem[] dropDownItems) : base(text, image, dropDownItems)
		{
			this.Initialize();
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x00114B09 File Offset: 0x00112D09
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level1)
			{
				return new ToolStripDropDownButton.ToolStripDropDownButtonAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x00111120 File Offset: 0x0010F320
		// (set) Token: 0x060040DE RID: 16606 RVA: 0x00111128 File Offset: 0x0010F328
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

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x00013062 File Offset: 0x00011262
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x00114B1F File Offset: 0x00112D1F
		// (set) Token: 0x060040E1 RID: 16609 RVA: 0x00114B27 File Offset: 0x00112D27
		[DefaultValue(true)]
		[SRDescription("ToolStripDropDownButtonShowDropDownArrowDescr")]
		[SRCategory("CatAppearance")]
		public bool ShowDropDownArrow
		{
			get
			{
				return this.showDropDownArrow;
			}
			set
			{
				if (this.showDropDownArrow != value)
				{
					this.showDropDownArrow = value;
					base.InvalidateItemLayout(PropertyNames.ShowDropDownArrow);
				}
			}
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x00114B44 File Offset: 0x00112D44
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout(this);
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x00114B4C File Offset: 0x00112D4C
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu(this, true);
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x00114B55 File Offset: 0x00112D55
		private void Initialize()
		{
			base.SupportsSpaceKey = true;
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x00114B60 File Offset: 0x00112D60
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (Control.ModifierKeys != Keys.Alt && e.Button == MouseButtons.Left)
			{
				if (base.DropDown.Visible)
				{
					ToolStripManager.ModalMenuFilter.CloseActiveDropDown(base.DropDown, ToolStripDropDownCloseReason.AppClicked);
				}
				else
				{
					this.openMouseId = ((base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId());
					base.ShowDropDown(true);
				}
			}
			base.OnMouseDown(e);
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x00114BCC File Offset: 0x00112DCC
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (Control.ModifierKeys != Keys.Alt && e.Button == MouseButtons.Left)
			{
				byte b = (base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId();
				if (b != this.openMouseId)
				{
					this.openMouseId = 0;
					ToolStripManager.ModalMenuFilter.CloseActiveDropDown(base.DropDown, ToolStripDropDownCloseReason.AppClicked);
					base.Select();
				}
			}
			base.OnMouseUp(e);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x00114C32 File Offset: 0x00112E32
		protected override void OnMouseLeave(EventArgs e)
		{
			this.openMouseId = 0;
			base.OnMouseLeave(e);
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x00114C44 File Offset: 0x00112E44
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				Graphics graphics = e.Graphics;
				renderer.DrawDropDownButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(graphics, this, base.InternalLayout.ImageRectangle));
				}
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
				{
					renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.Text, base.InternalLayout.TextRectangle, this.ForeColor, this.Font, base.InternalLayout.TextFormat));
				}
				if (this.ShowDropDownArrow)
				{
					ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout toolStripDropDownButtonInternalLayout = base.InternalLayout as ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout;
					Rectangle arrowRectangle = (toolStripDropDownButtonInternalLayout != null) ? toolStripDropDownButtonInternalLayout.DropDownArrowRect : Rectangle.Empty;
					Color arrowColor;
					if (this.Selected && !this.Pressed && AccessibilityImprovements.Level2 && SystemInformation.HighContrast)
					{
						arrowColor = (this.Enabled ? SystemColors.HighlightText : SystemColors.ControlDark);
					}
					else
					{
						arrowColor = (this.Enabled ? SystemColors.ControlText : SystemColors.ControlDark);
					}
					renderer.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, this, arrowRectangle, arrowColor, ArrowDirection.Down));
				}
			}
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x00114D62 File Offset: 0x00112F62
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (this.HasDropDownItems)
			{
				base.Select();
				base.ShowDropDown();
				return true;
			}
			return false;
		}

		// Token: 0x040024EC RID: 9452
		private bool showDropDownArrow = true;

		// Token: 0x040024ED RID: 9453
		private byte openMouseId;

		// Token: 0x02000801 RID: 2049
		[ComVisible(true)]
		internal class ToolStripDropDownButtonAccessibleObject : ToolStripDropDownItemAccessibleObject
		{
			// Token: 0x06006EE7 RID: 28391 RVA: 0x00196A38 File Offset: 0x00194C38
			public ToolStripDropDownButtonAccessibleObject(ToolStripDropDownButton ownerItem) : base(ownerItem)
			{
			}

			// Token: 0x06006EE8 RID: 28392 RVA: 0x00196A41 File Offset: 0x00194C41
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}
		}

		// Token: 0x02000802 RID: 2050
		internal class ToolStripDropDownButtonInternalLayout : ToolStripItemInternalLayout
		{
			// Token: 0x06006EE9 RID: 28393 RVA: 0x00196A60 File Offset: 0x00194C60
			public ToolStripDropDownButtonInternalLayout(ToolStripDropDownButton ownerItem) : base(ownerItem)
			{
				if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize = DpiHelper.LogicalToDeviceUnits(ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSizeUnscaled, ownerItem.DeviceDpi);
					this.scaledDropDownArrowPadding = DpiHelper.LogicalToDeviceUnits(ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowPadding, ownerItem.DeviceDpi);
				}
				else if (DpiHelper.IsScalingRequired)
				{
					ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize = DpiHelper.LogicalToDeviceUnits(ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSizeUnscaled, 0);
					this.scaledDropDownArrowPadding = DpiHelper.LogicalToDeviceUnits(ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowPadding, 0);
				}
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006EEA RID: 28394 RVA: 0x00196AF0 File Offset: 0x00194CF0
			public override Size GetPreferredSize(Size constrainingSize)
			{
				Size preferredSize = base.GetPreferredSize(constrainingSize);
				if (this.ownerItem.ShowDropDownArrow)
				{
					if (this.ownerItem.TextDirection == ToolStripTextDirection.Horizontal)
					{
						preferredSize.Width += this.DropDownArrowRect.Width + this.scaledDropDownArrowPadding.Horizontal;
					}
					else
					{
						preferredSize.Height += this.DropDownArrowRect.Height + this.scaledDropDownArrowPadding.Vertical;
					}
				}
				return preferredSize;
			}

			// Token: 0x06006EEB RID: 28395 RVA: 0x00196B74 File Offset: 0x00194D74
			protected override ToolStripItemInternalLayout.ToolStripItemLayoutOptions CommonLayoutOptions()
			{
				ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions = base.CommonLayoutOptions();
				if (this.ownerItem.ShowDropDownArrow)
				{
					if (this.ownerItem.TextDirection == ToolStripTextDirection.Horizontal)
					{
						int num = ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Width + this.scaledDropDownArrowPadding.Horizontal;
						ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions2 = toolStripItemLayoutOptions;
						toolStripItemLayoutOptions2.client.Width = toolStripItemLayoutOptions2.client.Width - num;
						if (this.ownerItem.RightToLeft == RightToLeft.Yes)
						{
							toolStripItemLayoutOptions.client.Offset(num, 0);
							this.dropDownArrowRect = new Rectangle(this.scaledDropDownArrowPadding.Left, 0, ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Width, this.ownerItem.Bounds.Height);
						}
						else
						{
							this.dropDownArrowRect = new Rectangle(toolStripItemLayoutOptions.client.Right, 0, ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Width, this.ownerItem.Bounds.Height);
						}
					}
					else
					{
						int num2 = ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Height + this.scaledDropDownArrowPadding.Vertical;
						ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions3 = toolStripItemLayoutOptions;
						toolStripItemLayoutOptions3.client.Height = toolStripItemLayoutOptions3.client.Height - num2;
						this.dropDownArrowRect = new Rectangle(0, toolStripItemLayoutOptions.client.Bottom + this.scaledDropDownArrowPadding.Top, this.ownerItem.Bounds.Width - 1, ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Height);
					}
				}
				return toolStripItemLayoutOptions;
			}

			// Token: 0x17001839 RID: 6201
			// (get) Token: 0x06006EEC RID: 28396 RVA: 0x00196CCA File Offset: 0x00194ECA
			public Rectangle DropDownArrowRect
			{
				get
				{
					return this.dropDownArrowRect;
				}
			}

			// Token: 0x040042FA RID: 17146
			private ToolStripDropDownButton ownerItem;

			// Token: 0x040042FB RID: 17147
			private static readonly Size dropDownArrowSizeUnscaled = new Size(5, 3);

			// Token: 0x040042FC RID: 17148
			private static Size dropDownArrowSize = ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSizeUnscaled;

			// Token: 0x040042FD RID: 17149
			private const int DROP_DOWN_ARROW_PADDING = 2;

			// Token: 0x040042FE RID: 17150
			private static Padding dropDownArrowPadding = new Padding(2);

			// Token: 0x040042FF RID: 17151
			private Padding scaledDropDownArrowPadding = ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowPadding;

			// Token: 0x04004300 RID: 17152
			private Rectangle dropDownArrowRect = Rectangle.Empty;
		}
	}
}
