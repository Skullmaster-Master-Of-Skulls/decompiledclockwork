using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	// Token: 0x020003B6 RID: 950
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripButton : ToolStripItem
	{
		// Token: 0x06003EF3 RID: 16115 RVA: 0x0011108B File Offset: 0x0010F28B
		public ToolStripButton()
		{
			this.Initialize();
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x001110A1 File Offset: 0x0010F2A1
		public ToolStripButton(string text) : base(text, null, null)
		{
			this.Initialize();
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x001110BA File Offset: 0x0010F2BA
		public ToolStripButton(Image image) : base(null, image, null)
		{
			this.Initialize();
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x001110D3 File Offset: 0x0010F2D3
		public ToolStripButton(string text, Image image) : base(text, image, null)
		{
			this.Initialize();
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x001110EC File Offset: 0x0010F2EC
		public ToolStripButton(string text, Image image, EventHandler onClick) : base(text, image, onClick)
		{
			this.Initialize();
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x00111105 File Offset: 0x0010F305
		public ToolStripButton(string text, Image image, EventHandler onClick, string name) : base(text, image, onClick, name)
		{
			this.Initialize();
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003EF9 RID: 16121 RVA: 0x00111120 File Offset: 0x0010F320
		// (set) Token: 0x06003EFA RID: 16122 RVA: 0x00111128 File Offset: 0x0010F328
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

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003EFB RID: 16123 RVA: 0x00013062 File Offset: 0x00011262
		public override bool CanSelect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x00111131 File Offset: 0x0010F331
		// (set) Token: 0x06003EFD RID: 16125 RVA: 0x00111139 File Offset: 0x0010F339
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

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x00111142 File Offset: 0x0010F342
		// (set) Token: 0x06003EFF RID: 16127 RVA: 0x0011114D File Offset: 0x0010F34D
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripButtonCheckedDescr")]
		public bool Checked
		{
			get
			{
				return this.checkState > CheckState.Unchecked;
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

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003F00 RID: 16128 RVA: 0x0011116B File Offset: 0x0010F36B
		// (set) Token: 0x06003F01 RID: 16129 RVA: 0x00111174 File Offset: 0x0010F374
		[SRCategory("CatAppearance")]
		[DefaultValue(CheckState.Unchecked)]
		[SRDescription("CheckBoxCheckStateDescr")]
		public CheckState CheckState
		{
			get
			{
				return this.checkState;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(CheckState));
				}
				if (value != this.checkState)
				{
					this.checkState = value;
					base.Invalidate();
					this.OnCheckedChanged(EventArgs.Empty);
					this.OnCheckStateChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000305 RID: 773
		// (add) Token: 0x06003F02 RID: 16130 RVA: 0x001111D3 File Offset: 0x0010F3D3
		// (remove) Token: 0x06003F03 RID: 16131 RVA: 0x001111E6 File Offset: 0x0010F3E6
		[SRDescription("CheckBoxOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripButton.EventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripButton.EventCheckedChanged, value);
			}
		}

		// Token: 0x14000306 RID: 774
		// (add) Token: 0x06003F04 RID: 16132 RVA: 0x001111F9 File Offset: 0x0010F3F9
		// (remove) Token: 0x06003F05 RID: 16133 RVA: 0x0011120C File Offset: 0x0010F40C
		[SRDescription("CheckBoxOnCheckStateChangedDescr")]
		public event EventHandler CheckStateChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripButton.EventCheckStateChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripButton.EventCheckStateChanged, value);
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003F06 RID: 16134 RVA: 0x00013062 File Offset: 0x00011262
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003F07 RID: 16135 RVA: 0x0011121F File Offset: 0x0010F41F
		// (set) Token: 0x06003F08 RID: 16136 RVA: 0x00111227 File Offset: 0x0010F427
		internal override int DeviceDpi
		{
			get
			{
				return base.DeviceDpi;
			}
			set
			{
				if (base.DeviceDpi != value)
				{
					base.DeviceDpi = value;
					this.standardButtonWidth = DpiHelper.LogicalToDeviceUnits(23, this.DeviceDpi);
				}
			}
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0011124C File Offset: 0x0010F44C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripButton.ToolStripButtonAccessibleObject(this);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x00111254 File Offset: 0x0010F454
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size preferredSize = base.GetPreferredSize(constrainingSize);
			preferredSize.Width = Math.Max(preferredSize.Width, this.standardButtonWidth);
			return preferredSize;
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x00111283 File Offset: 0x0010F483
		private void Initialize()
		{
			base.SupportsSpaceKey = true;
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.standardButtonWidth = DpiHelper.LogicalToDeviceUnitsX(23);
			}
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x001112A0 File Offset: 0x0010F4A0
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripButton.EventCheckedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x001112D0 File Offset: 0x0010F4D0
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange);
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripButton.EventCheckStateChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x0011130C File Offset: 0x0010F50C
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				renderer.DrawButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, base.InternalLayout.ImageRectangle)
					{
						ShiftOnPress = true
					});
				}
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
				{
					renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, base.InternalLayout.TextRectangle, this.ForeColor, this.Font, base.InternalLayout.TextFormat));
				}
			}
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x001113B3 File Offset: 0x0010F5B3
		protected override void OnClick(EventArgs e)
		{
			if (this.checkOnClick)
			{
				this.Checked = !this.Checked;
			}
			base.OnClick(e);
		}

		// Token: 0x040024A0 RID: 9376
		private CheckState checkState;

		// Token: 0x040024A1 RID: 9377
		private bool checkOnClick;

		// Token: 0x040024A2 RID: 9378
		private const int STANDARD_BUTTON_WIDTH = 23;

		// Token: 0x040024A3 RID: 9379
		private int standardButtonWidth = 23;

		// Token: 0x040024A4 RID: 9380
		private static readonly object EventCheckedChanged = new object();

		// Token: 0x040024A5 RID: 9381
		private static readonly object EventCheckStateChanged = new object();

		// Token: 0x020007FC RID: 2044
		[ComVisible(true)]
		internal class ToolStripButtonAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006EC3 RID: 28355 RVA: 0x00196581 File Offset: 0x00194781
			public ToolStripButtonAccessibleObject(ToolStripButton ownerItem) : base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006EC4 RID: 28356 RVA: 0x00196591 File Offset: 0x00194791
			internal override void ClearOwnerItem()
			{
				this.ownerItem = null;
				base.ClearOwnerItem();
			}

			// Token: 0x06006EC5 RID: 28357 RVA: 0x001965A0 File Offset: 0x001947A0
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x1700182B RID: 6187
			// (get) Token: 0x06006EC6 RID: 28358 RVA: 0x001965C3 File Offset: 0x001947C3
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return AccessibleRole.PushButton;
					}
					if (this.ownerItem.CheckOnClick && AccessibilityImprovements.Level1)
					{
						return AccessibleRole.CheckButton;
					}
					return base.Role;
				}
			}

			// Token: 0x1700182C RID: 6188
			// (get) Token: 0x06006EC7 RID: 28359 RVA: 0x001965F0 File Offset: 0x001947F0
			public override AccessibleStates State
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return AccessibleStates.None;
					}
					if (this.ownerItem.Enabled && this.ownerItem.Checked)
					{
						return base.State | AccessibleStates.Checked;
					}
					if (AccessibilityImprovements.Level1 && !this.ownerItem.Enabled && this.ownerItem.Selected)
					{
						return base.State | AccessibleStates.Focused;
					}
					return base.State;
				}
			}

			// Token: 0x040042F3 RID: 17139
			private ToolStripButton ownerItem;
		}
	}
}
