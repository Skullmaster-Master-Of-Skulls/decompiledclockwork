using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	// Token: 0x020003FF RID: 1023
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	public class ToolStripSeparator : ToolStripItem
	{
		// Token: 0x06004690 RID: 18064 RVA: 0x00128CF9 File Offset: 0x00126EF9
		public ToolStripSeparator()
		{
			this.ForeColor = SystemColors.ControlDark;
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06004691 RID: 18065 RVA: 0x00111120 File Offset: 0x0010F320
		// (set) Token: 0x06004692 RID: 18066 RVA: 0x00111128 File Offset: 0x0010F328
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x00128D0C File Offset: 0x00126F0C
		// (set) Token: 0x06004694 RID: 18068 RVA: 0x00128D14 File Offset: 0x00126F14
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

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x00128D1D File Offset: 0x00126F1D
		// (set) Token: 0x06004696 RID: 18070 RVA: 0x00128D25 File Offset: 0x00126F25
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

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x0010C4D9 File Offset: 0x0010A6D9
		public override bool CanSelect
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06004698 RID: 18072 RVA: 0x00128D2E File Offset: 0x00126F2E
		protected override Size DefaultSize
		{
			get
			{
				return new Size(6, 6);
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected internal override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x00111CC1 File Offset: 0x0010FEC1
		// (set) Token: 0x0600469B RID: 18075 RVA: 0x00111CC9 File Offset: 0x0010FEC9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool DoubleClickEnabled
		{
			get
			{
				return base.DoubleClickEnabled;
			}
			set
			{
				base.DoubleClickEnabled = value;
			}
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x0600469C RID: 18076 RVA: 0x00128D37 File Offset: 0x00126F37
		// (set) Token: 0x0600469D RID: 18077 RVA: 0x0011F6C1 File Offset: 0x0011D8C1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Enabled
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

		// Token: 0x14000388 RID: 904
		// (add) Token: 0x0600469E RID: 18078 RVA: 0x00128D3F File Offset: 0x00126F3F
		// (remove) Token: 0x0600469F RID: 18079 RVA: 0x00128D48 File Offset: 0x00126F48
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x060046A0 RID: 18080 RVA: 0x00111C8A File Offset: 0x0010FE8A
		// (set) Token: 0x060046A1 RID: 18081 RVA: 0x00111C92 File Offset: 0x0010FE92
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return base.DisplayStyle;
			}
			set
			{
				base.DisplayStyle = value;
			}
		}

		// Token: 0x14000389 RID: 905
		// (add) Token: 0x060046A2 RID: 18082 RVA: 0x00128D51 File Offset: 0x00126F51
		// (remove) Token: 0x060046A3 RID: 18083 RVA: 0x00128D5A File Offset: 0x00126F5A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DisplayStyleChanged
		{
			add
			{
				base.DisplayStyleChanged += value;
			}
			remove
			{
				base.DisplayStyleChanged -= value;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x00128D63 File Offset: 0x00126F63
		// (set) Token: 0x060046A5 RID: 18085 RVA: 0x00128D6B File Offset: 0x00126F6B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x060046A6 RID: 18086 RVA: 0x00111DAF File Offset: 0x0010FFAF
		// (set) Token: 0x060046A7 RID: 18087 RVA: 0x00111DB7 File Offset: 0x0010FFB7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment ImageAlign
		{
			get
			{
				return base.ImageAlign;
			}
			set
			{
				base.ImageAlign = value;
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x060046A8 RID: 18088 RVA: 0x00111D7C File Offset: 0x0010FF7C
		// (set) Token: 0x060046A9 RID: 18089 RVA: 0x00111D84 File Offset: 0x0010FF84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image Image
		{
			get
			{
				return base.Image;
			}
			set
			{
				base.Image = value;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060046AA RID: 18090 RVA: 0x00128D74 File Offset: 0x00126F74
		// (set) Token: 0x060046AB RID: 18091 RVA: 0x00128D7C File Offset: 0x00126F7C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int ImageIndex
		{
			get
			{
				return base.ImageIndex;
			}
			set
			{
				base.ImageIndex = value;
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060046AC RID: 18092 RVA: 0x00128D85 File Offset: 0x00126F85
		// (set) Token: 0x060046AD RID: 18093 RVA: 0x00128D8D File Offset: 0x00126F8D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ImageKey
		{
			get
			{
				return base.ImageKey;
			}
			set
			{
				base.ImageKey = value;
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060046AE RID: 18094 RVA: 0x00111D9E File Offset: 0x0010FF9E
		// (set) Token: 0x060046AF RID: 18095 RVA: 0x00111DA6 File Offset: 0x0010FFA6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color ImageTransparentColor
		{
			get
			{
				return base.ImageTransparentColor;
			}
			set
			{
				base.ImageTransparentColor = value;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060046B0 RID: 18096 RVA: 0x00111D8D File Offset: 0x0010FF8D
		// (set) Token: 0x060046B1 RID: 18097 RVA: 0x00111D95 File Offset: 0x0010FF95
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return base.ImageScaling;
			}
			set
			{
				base.ImageScaling = value;
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060046B2 RID: 18098 RVA: 0x00128D98 File Offset: 0x00126F98
		private bool IsVertical
		{
			get
			{
				ToolStrip toolStrip = base.ParentInternal;
				if (toolStrip == null)
				{
					toolStrip = base.Owner;
				}
				ToolStripDropDownMenu toolStripDropDownMenu = toolStrip as ToolStripDropDownMenu;
				if (toolStripDropDownMenu != null)
				{
					return false;
				}
				switch (toolStrip.LayoutStyle)
				{
				case ToolStripLayoutStyle.VerticalStackWithOverflow:
					return false;
				}
				return true;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060046B3 RID: 18099 RVA: 0x00128DE6 File Offset: 0x00126FE6
		// (set) Token: 0x060046B4 RID: 18100 RVA: 0x00128DEE File Offset: 0x00126FEE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x1400038A RID: 906
		// (add) Token: 0x060046B5 RID: 18101 RVA: 0x00127098 File Offset: 0x00125298
		// (remove) Token: 0x060046B6 RID: 18102 RVA: 0x001270A1 File Offset: 0x001252A1
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

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060046B7 RID: 18103 RVA: 0x00111FCC File Offset: 0x001101CC
		// (set) Token: 0x060046B8 RID: 18104 RVA: 0x00111FD4 File Offset: 0x001101D4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060046B9 RID: 18105 RVA: 0x00111FDD File Offset: 0x001101DD
		// (set) Token: 0x060046BA RID: 18106 RVA: 0x00111FE5 File Offset: 0x001101E5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return base.TextDirection;
			}
			set
			{
				base.TextDirection = value;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060046BB RID: 18107 RVA: 0x00111FEE File Offset: 0x001101EE
		// (set) Token: 0x060046BC RID: 18108 RVA: 0x00111FF6 File Offset: 0x001101F6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new TextImageRelation TextImageRelation
		{
			get
			{
				return base.TextImageRelation;
			}
			set
			{
				base.TextImageRelation = value;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060046BD RID: 18109 RVA: 0x0011C115 File Offset: 0x0011A315
		// (set) Token: 0x060046BE RID: 18110 RVA: 0x00128DF7 File Offset: 0x00126FF7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ToolTipText
		{
			get
			{
				return base.ToolTipText;
			}
			set
			{
				base.ToolTipText = value;
			}
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060046BF RID: 18111 RVA: 0x00111EB0 File Offset: 0x001100B0
		// (set) Token: 0x060046C0 RID: 18112 RVA: 0x00111EB8 File Offset: 0x001100B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool RightToLeftAutoMirrorImage
		{
			get
			{
				return base.RightToLeftAutoMirrorImage;
			}
			set
			{
				base.RightToLeftAutoMirrorImage = value;
			}
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x00128E00 File Offset: 0x00127000
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripSeparator.ToolStripSeparatorAccessibleObject(this);
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x00128E08 File Offset: 0x00127008
		public override Size GetPreferredSize(Size constrainingSize)
		{
			ToolStrip toolStrip = base.ParentInternal;
			if (toolStrip == null)
			{
				toolStrip = base.Owner;
			}
			if (toolStrip == null)
			{
				return new Size(6, 6);
			}
			ToolStripDropDownMenu toolStripDropDownMenu = toolStrip as ToolStripDropDownMenu;
			if (toolStripDropDownMenu != null)
			{
				return new Size(toolStrip.Width - (toolStrip.Padding.Horizontal - toolStripDropDownMenu.ImageMargin.Width), 6);
			}
			if (toolStrip.LayoutStyle != ToolStripLayoutStyle.HorizontalStackWithOverflow || toolStrip.LayoutStyle != ToolStripLayoutStyle.VerticalStackWithOverflow)
			{
				constrainingSize.Width = 23;
				constrainingSize.Height = 23;
			}
			if (this.IsVertical)
			{
				return new Size(6, constrainingSize.Height);
			}
			return new Size(constrainingSize.Width, 6);
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x00128EAD File Offset: 0x001270AD
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null && base.ParentInternal != null)
			{
				base.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(e.Graphics, this, this.IsVertical));
			}
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x00128EDC File Offset: 0x001270DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripItem.EventFontChanged, e);
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x00128EEA File Offset: 0x001270EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal override bool ShouldSerializeForeColor()
		{
			return this.ForeColor != SystemColors.ControlDark;
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x00128EFC File Offset: 0x001270FC
		protected internal override void SetBounds(Rectangle rect)
		{
			ToolStripDropDownMenu toolStripDropDownMenu = base.Owner as ToolStripDropDownMenu;
			if (toolStripDropDownMenu != null && toolStripDropDownMenu != null)
			{
				rect.X = 2;
				rect.Width = toolStripDropDownMenu.Width - 4;
			}
			base.SetBounds(rect);
		}

		// Token: 0x040026BB RID: 9915
		private const int WINBAR_SEPARATORTHICKNESS = 6;

		// Token: 0x040026BC RID: 9916
		private const int WINBAR_SEPARATORHEIGHT = 23;

		// Token: 0x0200081A RID: 2074
		[ComVisible(true)]
		internal class ToolStripSeparatorAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006FCF RID: 28623 RVA: 0x00166C85 File Offset: 0x00164E85
			public ToolStripSeparatorAccessibleObject(ToolStripSeparator ownerItem) : base(ownerItem)
			{
			}

			// Token: 0x1700186C RID: 6252
			// (get) Token: 0x06006FD0 RID: 28624 RVA: 0x0019AE20 File Offset: 0x00199020
			public override AccessibleRole Role
			{
				get
				{
					if (!base.IsOwnerItemCleared())
					{
						ToolStripSeparator toolStripSeparator = base.Owner as ToolStripSeparator;
						if (toolStripSeparator != null)
						{
							AccessibleRole accessibleRole = toolStripSeparator.AccessibleRole;
							if (accessibleRole != AccessibleRole.Default)
							{
								return accessibleRole;
							}
							return AccessibleRole.Separator;
						}
					}
					return AccessibleRole.Separator;
				}
			}

			// Token: 0x06006FD1 RID: 28625 RVA: 0x0019AE56 File Offset: 0x00199056
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50038;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
