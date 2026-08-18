using System;
using System.Drawing;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FF RID: 1279
	internal sealed class DropDownButton : Button
	{
		// Token: 0x060053BD RID: 21437 RVA: 0x0015ED77 File Offset: 0x0015CF77
		public DropDownButton()
		{
			base.SetStyle(ControlStyles.Selectable, true);
			this.SetAccessibleName();
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x060053BE RID: 21438 RVA: 0x0015ED91 File Offset: 0x0015CF91
		// (set) Token: 0x060053BF RID: 21439 RVA: 0x0015ED99 File Offset: 0x0015CF99
		public bool IgnoreMouse
		{
			get
			{
				return this.ignoreMouse;
			}
			set
			{
				this.ignoreMouse = value;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x060053C0 RID: 21440 RVA: 0x000A8615 File Offset: 0x000A6815
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		// Token: 0x17001403 RID: 5123
		// (set) Token: 0x060053C1 RID: 21441 RVA: 0x0015EDA2 File Offset: 0x0015CFA2
		public bool UseComboBoxTheme
		{
			set
			{
				if (this.useComboBoxTheme != value)
				{
					this.useComboBoxTheme = value;
					if (AccessibilityImprovements.Level1)
					{
						this.SetAccessibleName();
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x0015EDC7 File Offset: 0x0015CFC7
		protected override void OnClick(EventArgs e)
		{
			if (!this.IgnoreMouse)
			{
				base.OnClick(e);
			}
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x0015EDD8 File Offset: 0x0015CFD8
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (!this.IgnoreMouse)
			{
				base.OnMouseUp(e);
			}
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0015EDE9 File Offset: 0x0015CFE9
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!this.IgnoreMouse)
			{
				base.OnMouseDown(e);
			}
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x0015EDFC File Offset: 0x0015CFFC
		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			if (Application.RenderWithVisualStyles & this.useComboBoxTheme)
			{
				ComboBoxState comboBoxState = ComboBoxState.Normal;
				if (base.MouseIsDown)
				{
					comboBoxState = ComboBoxState.Pressed;
				}
				else if (base.MouseIsOver)
				{
					comboBoxState = ComboBoxState.Hot;
				}
				Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
				if (comboBoxState == ComboBoxState.Normal)
				{
					pevent.Graphics.FillRectangle(SystemBrushes.Window, rectangle);
				}
				if (!DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					ComboBoxRenderer.DrawDropDownButton(pevent.Graphics, rectangle, comboBoxState);
				}
				else
				{
					ComboBoxRenderer.DrawDropDownButtonForHandle(pevent.Graphics, rectangle, comboBoxState, base.HandleInternal);
				}
				if (AccessibilityImprovements.Level1 && this.Focused)
				{
					rectangle.Inflate(-1, -1);
					ControlPaint.DrawFocusRectangle(pevent.Graphics, rectangle, this.ForeColor, this.BackColor);
				}
			}
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x0015EEBC File Offset: 0x0015D0BC
		internal void PerformButtonClick()
		{
			if (base.Visible && base.Enabled)
			{
				this.OnClick(EventArgs.Empty);
			}
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x0015EED9 File Offset: 0x0015D0D9
		private void SetAccessibleName()
		{
			if (AccessibilityImprovements.Level1 && this.useComboBoxTheme)
			{
				base.AccessibleName = SR.GetString("PropertyGridDropDownButtonComboBoxAccessibleName");
				return;
			}
			base.AccessibleName = SR.GetString("PropertyGridDropDownButtonAccessibleName");
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x0015EF0B File Offset: 0x0015D10B
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new DropDownButtonAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x0015EF21 File Offset: 0x0015D121
		internal override ButtonBaseAdapter CreateStandardAdapter()
		{
			return new DropDownButtonAdapter(this);
		}

		// Token: 0x040036D0 RID: 14032
		private bool useComboBoxTheme;

		// Token: 0x040036D1 RID: 14033
		private bool ignoreMouse;
	}
}
