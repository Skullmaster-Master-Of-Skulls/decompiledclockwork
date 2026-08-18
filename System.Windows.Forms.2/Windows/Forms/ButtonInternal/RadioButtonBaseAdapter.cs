using System;
using System.Drawing;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C3 RID: 1219
	internal abstract class RadioButtonBaseAdapter : CheckableControlBaseAdapter
	{
		// Token: 0x06004FFF RID: 20479 RVA: 0x0014AE0E File Offset: 0x0014900E
		internal RadioButtonBaseAdapter(ButtonBase control) : base(control)
		{
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06005000 RID: 20480 RVA: 0x0014C0E0 File Offset: 0x0014A2E0
		protected new RadioButton Control
		{
			get
			{
				return (RadioButton)base.Control;
			}
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0014C0ED File Offset: 0x0014A2ED
		protected void DrawCheckFlat(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout, Color checkColor, Color checkBackground, Color checkBorder)
		{
			this.DrawCheckBackgroundFlat(e, layout.checkBounds, checkBorder, checkBackground);
			this.DrawCheckOnly(e, layout, checkColor, checkBackground, true);
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0014C10C File Offset: 0x0014A30C
		protected void DrawCheckBackground3DLite(PaintEventArgs e, Rectangle bounds, Color checkColor, Color checkBackground, ButtonBaseAdapter.ColorData colors, bool disabledColors)
		{
			Graphics graphics = e.Graphics;
			Color color = checkBackground;
			if (!this.Control.Enabled && disabledColors)
			{
				color = SystemColors.Control;
			}
			using (Brush brush = new SolidBrush(color))
			{
				using (Pen pen = new Pen(colors.buttonShadow))
				{
					using (Pen pen2 = new Pen(colors.buttonFace))
					{
						using (Pen pen3 = new Pen(colors.highlight))
						{
							int num = bounds.Width;
							bounds.Width = num - 1;
							num = bounds.Height;
							bounds.Height = num - 1;
							graphics.DrawPie(pen, bounds, 136f, 88f);
							graphics.DrawPie(pen, bounds, 226f, 88f);
							graphics.DrawPie(pen3, bounds, 316f, 88f);
							graphics.DrawPie(pen3, bounds, 46f, 88f);
							bounds.Inflate(-1, -1);
							graphics.FillEllipse(brush, bounds);
							graphics.DrawEllipse(pen2, bounds);
						}
					}
				}
			}
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0014C258 File Offset: 0x0014A458
		protected void DrawCheckBackgroundFlat(PaintEventArgs e, Rectangle bounds, Color borderColor, Color checkBackground)
		{
			Color color = checkBackground;
			Color color2 = borderColor;
			if (!this.Control.Enabled)
			{
				if (!SystemInformation.HighContrast || !AccessibilityImprovements.Level1)
				{
					color2 = ControlPaint.ContrastControlDark;
				}
				color = SystemColors.Control;
			}
			double dpiScaleRatio = base.GetDpiScaleRatio(e.Graphics);
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(e.Graphics))
			{
				using (WindowsPen windowsPen = new WindowsPen(windowsGraphics.DeviceContext, color2))
				{
					using (WindowsBrush windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, color))
					{
						if (dpiScaleRatio > 1.1)
						{
							int num = bounds.Width;
							bounds.Width = num - 1;
							num = bounds.Height;
							bounds.Height = num - 1;
							windowsGraphics.DrawAndFillEllipse(windowsPen, windowsBrush, bounds);
							bounds.Inflate(-1, -1);
						}
						else
						{
							RadioButtonBaseAdapter.DrawAndFillEllipse(windowsGraphics, windowsPen, windowsBrush, bounds);
						}
					}
				}
			}
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0014C368 File Offset: 0x0014A568
		private static void DrawAndFillEllipse(WindowsGraphics wg, WindowsPen borderPen, WindowsBrush fieldBrush, Rectangle bounds)
		{
			if (wg == null)
			{
				return;
			}
			wg.FillRectangle(fieldBrush, new Rectangle(bounds.X + 2, bounds.Y + 2, 8, 8));
			wg.FillRectangle(fieldBrush, new Rectangle(bounds.X + 4, bounds.Y + 1, 4, 10));
			wg.FillRectangle(fieldBrush, new Rectangle(bounds.X + 1, bounds.Y + 4, 10, 4));
			wg.DrawLine(borderPen, new Point(bounds.X + 4, bounds.Y), new Point(bounds.X + 8, bounds.Y));
			wg.DrawLine(borderPen, new Point(bounds.X + 4, bounds.Y + 11), new Point(bounds.X + 8, bounds.Y + 11));
			wg.DrawLine(borderPen, new Point(bounds.X + 2, bounds.Y + 1), new Point(bounds.X + 4, bounds.Y + 1));
			wg.DrawLine(borderPen, new Point(bounds.X + 8, bounds.Y + 1), new Point(bounds.X + 10, bounds.Y + 1));
			wg.DrawLine(borderPen, new Point(bounds.X + 2, bounds.Y + 10), new Point(bounds.X + 4, bounds.Y + 10));
			wg.DrawLine(borderPen, new Point(bounds.X + 8, bounds.Y + 10), new Point(bounds.X + 10, bounds.Y + 10));
			wg.DrawLine(borderPen, new Point(bounds.X, bounds.Y + 4), new Point(bounds.X, bounds.Y + 8));
			wg.DrawLine(borderPen, new Point(bounds.X + 11, bounds.Y + 4), new Point(bounds.X + 11, bounds.Y + 8));
			wg.DrawLine(borderPen, new Point(bounds.X + 1, bounds.Y + 2), new Point(bounds.X + 1, bounds.Y + 4));
			wg.DrawLine(borderPen, new Point(bounds.X + 1, bounds.Y + 8), new Point(bounds.X + 1, bounds.Y + 10));
			wg.DrawLine(borderPen, new Point(bounds.X + 10, bounds.Y + 2), new Point(bounds.X + 10, bounds.Y + 4));
			wg.DrawLine(borderPen, new Point(bounds.X + 10, bounds.Y + 8), new Point(bounds.X + 10, bounds.Y + 10));
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0014C65F File Offset: 0x0014A85F
		private static int GetScaledNumber(int n, double scale)
		{
			return (int)((double)n * scale);
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0014C668 File Offset: 0x0014A868
		protected void DrawCheckOnly(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout, Color checkColor, Color checkBackground, bool disabledColors)
		{
			if (this.Control.Checked)
			{
				if (!this.Control.Enabled && disabledColors)
				{
					checkColor = SystemColors.ControlDark;
				}
				double dpiScaleRatio = base.GetDpiScaleRatio(e.Graphics);
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(e.Graphics))
				{
					using (WindowsBrush windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, checkColor))
					{
						int num = 5;
						Rectangle rect = new Rectangle(layout.checkBounds.X + RadioButtonBaseAdapter.GetScaledNumber(num, dpiScaleRatio), layout.checkBounds.Y + RadioButtonBaseAdapter.GetScaledNumber(num - 1, dpiScaleRatio), RadioButtonBaseAdapter.GetScaledNumber(2, dpiScaleRatio), RadioButtonBaseAdapter.GetScaledNumber(4, dpiScaleRatio));
						windowsGraphics.FillRectangle(windowsBrush, rect);
						Rectangle rect2 = new Rectangle(layout.checkBounds.X + RadioButtonBaseAdapter.GetScaledNumber(num - 1, dpiScaleRatio), layout.checkBounds.Y + RadioButtonBaseAdapter.GetScaledNumber(num, dpiScaleRatio), RadioButtonBaseAdapter.GetScaledNumber(4, dpiScaleRatio), RadioButtonBaseAdapter.GetScaledNumber(2, dpiScaleRatio));
						windowsGraphics.FillRectangle(windowsBrush, rect2);
					}
				}
			}
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0014C788 File Offset: 0x0014A988
		protected ButtonState GetState()
		{
			ButtonState buttonState = ButtonState.Normal;
			if (this.Control.Checked)
			{
				buttonState |= ButtonState.Checked;
			}
			else
			{
				buttonState |= ButtonState.Normal;
			}
			if (!this.Control.Enabled)
			{
				buttonState |= ButtonState.Inactive;
			}
			if (this.Control.MouseIsDown)
			{
				buttonState |= ButtonState.Pushed;
			}
			return buttonState;
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0014C7E0 File Offset: 0x0014A9E0
		protected void DrawCheckBox(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout)
		{
			Graphics graphics = e.Graphics;
			Rectangle checkBounds = layout.checkBounds;
			if (!Application.RenderWithVisualStyles)
			{
				int x = checkBounds.X;
				checkBounds.X = x - 1;
			}
			ButtonState state = this.GetState();
			if (Application.RenderWithVisualStyles)
			{
				RadioButtonRenderer.DrawRadioButton(graphics, new Point(checkBounds.Left, checkBounds.Top), RadioButtonRenderer.ConvertFromButtonState(state, this.Control.MouseIsOver), this.Control.HandleInternal);
				return;
			}
			ControlPaint.DrawRadioButton(graphics, checkBounds, state);
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0014C85F File Offset: 0x0014AA5F
		protected void AdjustFocusRectangle(ButtonBaseAdapter.LayoutData layout)
		{
			if (AccessibilityImprovements.Level2 && string.IsNullOrEmpty(this.Control.Text))
			{
				layout.focus = (this.Control.AutoSize ? layout.checkBounds : layout.field);
			}
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x0014C89C File Offset: 0x0014AA9C
		internal override ButtonBaseAdapter.LayoutOptions CommonLayout()
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = base.CommonLayout();
			layoutOptions.checkAlign = this.Control.CheckAlign;
			return layoutOptions;
		}
	}
}
