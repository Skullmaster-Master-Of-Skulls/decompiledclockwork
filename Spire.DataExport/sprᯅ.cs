using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Spire.DataExport.Utils;

// Token: 0x02000127 RID: 295
internal class sprᯅ : Button
{
	// Token: 0x060006E3 RID: 1763 RVA: 0x00041C88 File Offset: 0x00040C88
	public sprᯅ()
	{
		this.ᜀ();
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00041CB0 File Offset: 0x00040CB0
	static sprᯅ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᯅ.ᜄ = new Size(4, 4);
		sprᯅ.ᜅ = Color.FromArgb(64, 164, 164, 164);
		sprᯅ.ᜆ = Color.FromArgb(64, Color.White);
		sprᯅ.ᜇ = Color.FromArgb(250, 250, 248);
		sprᯅ.ᜈ = Color.FromArgb(240, 240, 234);
		sprᯅ.ᜉ = Color.FromArgb(0, 60, 116);
		sprᯅ.ᜊ = Color.FromArgb(236, 235, 230);
		sprᯅ.ᜋ = Color.FromArgb(226, 223, 214);
		sprᯅ.ᜌ = Color.FromArgb(214, 208, 197);
		sprᯅ.\u170D = Color.FromArgb(128, 236, 234, 230);
		sprᯅ.ᜎ = Color.FromArgb(128, 224, 220, 212);
		sprᯅ.ᜏ = Color.FromArgb(128, 234, 228, 218);
		sprᯅ.ᜐ = Color.FromArgb(128, 212, 208, 196);
		sprᯅ.ᜑ = Color.FromArgb(234, 233, 227);
		sprᯅ.\u1712 = Color.FromArgb(242, 241, 238);
		sprᯅ.\u1713 = Color.FromArgb(209, 204, 193);
		sprᯅ.\u1714 = Color.FromArgb(220, 216, 207);
		sprᯅ.\u1715 = Color.FromArgb(216, 213, 203);
		sprᯅ.\u1716 = Color.FromArgb(222, 220, 211);
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00041EC0 File Offset: 0x00040EC0
	public FlatStyle ᜂ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return base.FlatStyle;
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00041F04 File Offset: 0x00040F04
	public void ᜀ(FlatStyle A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		base.FlatStyle = FlatStyle.Standard;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00041F48 File Offset: 0x00040F48
	public emunType.BtnShape ᜄ()
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return this.\u1718;
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00041F8C File Offset: 0x00040F8C
	public void ᜀ(emunType.BtnShape A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.\u1718 = A_0;
		base.Invalidate();
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x00041FD4 File Offset: 0x00040FD4
	public emunType.XPStyle ᜅ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.\u1717;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00042018 File Offset: 0x00041018
	public void ᜀ(emunType.XPStyle A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.\u1717 = A_0;
		base.Invalidate();
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00042060 File Offset: 0x00041060
	public Point ᜃ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜃ;
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x000420A4 File Offset: 0x000410A4
	public void ᜀ(Point A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜃ = A_0;
		base.Invalidate();
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x000420EC File Offset: 0x000410EC
	private Rectangle ᜁ()
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Rectangle clientRectangle = base.ClientRectangle;
		return new Rectangle(1, 1, clientRectangle.Width - 3, clientRectangle.Height - 3);
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00042148 File Offset: 0x00041148
	protected virtual void ᜀ(EventArgs A_0)
	{
		for (;;)
		{
			for (;;)
			{
				base.Capture = false;
				this.ᜂ = false;
				Rectangle clientRectangle = base.ClientRectangle;
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (clientRectangle.Contains(base.PointToClient(Control.MousePosition)))
						{
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜁ = sprᯅ.ControlState.Normal;
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_A7;
					case 2:
						goto IL_93;
					case 3:
						this.ᜁ = sprᯅ.ControlState.Hover;
						num = 1;
						continue;
					}
					break;
				}
			}
		}
		IL_93:
		IL_A7:
		base.Invalidate();
		base.OnClick(A_0);
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0004220C File Offset: 0x0004120C
	protected virtual void ᜁ(EventArgs A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		base.OnMouseEnter(A_0);
		this.ᜁ = sprᯅ.ControlState.Hover;
		base.Invalidate();
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0004225C File Offset: 0x0004125C
	protected override void OnMouseDown(MouseEventArgs mea)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			for (;;)
			{
				base.OnMouseDown(mea);
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜂ = true;
						this.ᜁ = sprᯅ.ControlState.Pressed;
						base.Invalidate();
						num = 0;
						continue;
					case 2:
						if (mea.Button == MouseButtons.Left)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				}
			}
			break;
		}
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x000422F0 File Offset: 0x000412F0
	protected override void OnMouseMove(MouseEventArgs mea)
	{
		for (;;)
		{
			for (;;)
			{
				base.OnMouseMove(mea);
				Rectangle clientRectangle = base.ClientRectangle;
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 8;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (base.Capture)
							{
								num = 3;
								continue;
							}
							return;
						}
						break;
					case 2:
						if (!this.ᜂ)
						{
							num = 9;
							continue;
						}
						return;
					case 3:
						num = 2;
						continue;
					case 4:
						return;
					case 5:
						if (this.ᜁ == sprᯅ.ControlState.Pressed)
						{
							if (true)
							{
							}
							num = 10;
							continue;
						}
						return;
					case 6:
						num = 1;
						continue;
					case 7:
						if (clientRectangle.Contains(mea.X, mea.Y))
						{
							num = 0;
							continue;
						}
						num = 5;
						continue;
					case 8:
						if (this.ᜁ == sprᯅ.ControlState.Hover)
						{
							num = 6;
							continue;
						}
						return;
					case 9:
						goto IL_B3;
					case 10:
						this.ᜂ = false;
						this.ᜁ = sprᯅ.ControlState.Hover;
						base.Invalidate();
						num = 4;
						continue;
					}
					break;
				}
			}
		}
		IL_B3:
		this.ᜂ = true;
		this.ᜁ = sprᯅ.ControlState.Pressed;
		base.Invalidate();
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x00042458 File Offset: 0x00041458
	protected virtual void ᜂ(EventArgs A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		base.OnMouseLeave(A_0);
		this.ᜁ = sprᯅ.ControlState.Normal;
		base.Invalidate();
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x000424A8 File Offset: 0x000414A8
	protected override void OnPaint(PaintEventArgs pea)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				this.OnPaintBackground(pea);
				sprᯅ.ControlState controlState = this.ᜁ;
				int num = 15;
				for (;;)
				{
					emunType.BtnShape u4;
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						goto IL_349;
					case 2:
						num = 21;
						continue;
					case 3:
						goto IL_35A;
					case 4:
						num = 27;
						continue;
					case 5:
						num = 24;
						continue;
					case 6:
						goto IL_24A;
					case 7:
						goto IL_1D1;
					case 8:
						goto IL_100;
					case 9:
						goto IL_105;
					case 10:
						goto IL_38E;
					case 11:
						goto IL_278;
					case 12:
						goto IL_306;
					case 13:
						num = 11;
						continue;
					case 14:
					{
						emunType.BtnShape u;
						switch (u)
						{
						case emunType.BtnShape.Rectangle:
							this.\u1712(pea.Graphics);
							num = 26;
							continue;
						case emunType.BtnShape.Ellipse:
							this.ᜌ(pea.Graphics);
							num = 8;
							continue;
						default:
							num = 0;
							continue;
						}
						break;
					}
					case 15:
						switch (controlState)
						{
						case sprᯅ.ControlState.Normal:
							num = 3;
							continue;
						case sprᯅ.ControlState.Hover:
						{
							emunType.BtnShape u2 = this.\u1718;
							num = 23;
							continue;
						}
						case sprᯅ.ControlState.Pressed:
						{
							emunType.BtnShape u3 = this.\u1718;
							num = 17;
							continue;
						}
						default:
							num = 13;
							continue;
						}
						break;
					case 16:
						switch (u4)
						{
						case emunType.BtnShape.Rectangle:
							this.ᜊ(pea.Graphics);
							num = 18;
							continue;
						case emunType.BtnShape.Ellipse:
							this.ᜋ(pea.Graphics);
							num = 28;
							continue;
						default:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_35A;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						}
						break;
					case 17:
					{
						emunType.BtnShape u3;
						switch (u3)
						{
						case emunType.BtnShape.Rectangle:
							this.\u170D(pea.Graphics);
							num = 25;
							continue;
						case emunType.BtnShape.Ellipse:
							this.ᜏ(pea.Graphics);
							num = 6;
							continue;
						default:
							num = 19;
							continue;
						}
						break;
					}
					case 18:
						goto IL_2BA;
					case 19:
						num = 20;
						continue;
					case 20:
						goto IL_22D;
					case 21:
						goto IL_E3;
					case 22:
						num = 29;
						continue;
					case 23:
					{
						emunType.BtnShape u2;
						switch (u2)
						{
						case emunType.BtnShape.Rectangle:
							this.ᜐ(pea.Graphics);
							num = 7;
							continue;
						case emunType.BtnShape.Ellipse:
							this.ᜑ(pea.Graphics);
							num = 12;
							continue;
						default:
							num = 2;
							continue;
						}
						break;
					}
					case 24:
						goto IL_21C;
					case 25:
						goto IL_1EE;
					case 26:
						goto IL_20B;
					case 27:
						if (!this.Focused)
						{
							num = 22;
							continue;
						}
						goto IL_105;
					case 28:
						goto IL_267;
					case 29:
					{
						if (base.IsDefault)
						{
							num = 9;
							continue;
						}
						emunType.BtnShape u = this.\u1718;
						num = 14;
						continue;
					}
					}
					break;
					IL_105:
					u4 = this.\u1718;
					num = 16;
					continue;
					IL_35A:
					if (base.Enabled)
					{
						num = 4;
					}
					else
					{
						this.ᜉ(pea.Graphics);
						num = 10;
					}
				}
			}
			IL_E3:
			IL_100:
			IL_1D1:
			IL_1EE:
			IL_20B:
			IL_21C:
			IL_22D:
			IL_24A:
			IL_267:
			IL_278:
			IL_2BA:
			IL_306:
			IL_349:
			IL_38E:
			this.ᜈ(pea.Graphics);
			return;
		}
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x00042854 File Offset: 0x00041854
	protected override void OnEnabledChanged(EventArgs ea)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		base.OnEnabledChanged(ea);
		this.ᜁ = sprᯅ.ControlState.Normal;
		base.Invalidate();
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x000428A4 File Offset: 0x000418A4
	private void \u1712(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜆ(A_0);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x000428E8 File Offset: 0x000418E8
	private void ᜑ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜇ(A_0);
		this.ᜀ(A_0);
		this.ᜂ(A_0);
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00042938 File Offset: 0x00041938
	private void ᜐ(Graphics A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜆ(A_0);
		Rectangle rectangle = this.ᜁ();
		Pen pen = new Pen(Color.FromArgb(255, 240, 207));
		Pen pen2 = new Pen(Color.FromArgb(253, 216, 137));
		A_0.DrawLine(pen, rectangle.Left + 2, rectangle.Top + 1, rectangle.Right - 2, rectangle.Top + 1);
		A_0.DrawLine(pen2, rectangle.Left + 1, rectangle.Top + 2, rectangle.Right - 1, rectangle.Top + 2);
		pen.Dispose();
		pen2.Dispose();
		Pen pen3 = new Pen(Color.FromArgb(248, 178, 48));
		Pen pen4 = new Pen(Color.FromArgb(229, 151, 0));
		A_0.DrawLine(pen3, rectangle.Left + 1, rectangle.Bottom - 2, rectangle.Right - 1, rectangle.Bottom - 2);
		A_0.DrawLine(pen4, rectangle.Left + 2, rectangle.Bottom - 1, rectangle.Right - 2, rectangle.Bottom - 1);
		pen3.Dispose();
		pen4.Dispose();
		Rectangle rect = new Rectangle(rectangle.Left + 1, rectangle.Top + 3, 2, rectangle.Height - 5);
		Rectangle rect2 = new Rectangle(rectangle.Right - 2, rectangle.Top + 3, 2, rectangle.Height - 5);
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(254, 221, 149), Color.FromArgb(249, 180, 53), LinearGradientMode.Vertical);
		A_0.FillRectangle(linearGradientBrush, rect);
		A_0.FillRectangle(linearGradientBrush, rect2);
		linearGradientBrush.Dispose();
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00042B3C File Offset: 0x00041B3C
	private void ᜏ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜎ(A_0);
		this.ᜂ(A_0);
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x00042B88 File Offset: 0x00041B88
	private void ᜎ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Rectangle rectangle = this.ᜁ();
		Rectangle rect = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 1, rectangle.Height - 1);
		SolidBrush brush = new SolidBrush(Color.FromArgb(226, 225, 218));
		A_0.FillEllipse(brush, rect);
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x00042C18 File Offset: 0x00041C18
	private void \u170D(Graphics A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		Rectangle rectangle = this.ᜁ();
		this.ᜅ(A_0);
		Rectangle rect = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 1, rectangle.Height - 1);
		SolidBrush solidBrush = new SolidBrush(Color.FromArgb(226, 225, 218));
		A_0.FillRectangle(solidBrush, rect);
		solidBrush.Dispose();
		this.ᜃ(A_0);
		Pen pen = new Pen(sprᯅ.ᜑ);
		Pen pen2 = new Pen(sprᯅ.\u1712);
		A_0.DrawLine(pen, rectangle.Left + 1, rectangle.Bottom - 2, rectangle.Right - 1, rectangle.Bottom - 2);
		A_0.DrawLine(pen2, rectangle.Left + 2, rectangle.Bottom - 1, rectangle.Right - 2, rectangle.Bottom - 1);
		pen.Dispose();
		pen2.Dispose();
		Pen pen3 = new Pen(sprᯅ.\u1713);
		Pen pen4 = new Pen(sprᯅ.\u1714);
		A_0.DrawLine(pen3, rectangle.Left + 2, rectangle.Top + 1, rectangle.Right - 2, rectangle.Top + 1);
		A_0.DrawLine(pen4, rectangle.Left + 1, rectangle.Top + 2, rectangle.Right - 1, rectangle.Top + 2);
		pen3.Dispose();
		pen4.Dispose();
		Pen pen5 = new Pen(sprᯅ.\u1715);
		Pen pen6 = new Pen(sprᯅ.\u1716);
		A_0.DrawLine(pen5, rectangle.Left + 1, rectangle.Top + 3, rectangle.Left + 1, rectangle.Bottom - 3);
		A_0.DrawLine(pen6, rectangle.Left + 2, rectangle.Top + 3, rectangle.Left + 2, rectangle.Bottom - 3);
		pen5.Dispose();
		pen6.Dispose();
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00042E34 File Offset: 0x00041E34
	private void ᜌ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜇ(A_0);
		this.ᜂ(A_0);
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00042E80 File Offset: 0x00041E80
	private void ᜋ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜇ(A_0);
		this.ᜁ(A_0);
		this.ᜂ(A_0);
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x00042ED0 File Offset: 0x00041ED0
	private void ᜊ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜆ(A_0);
		Rectangle rectangle = this.ᜁ();
		Pen pen = new Pen(Color.FromArgb(206, 231, 255));
		Pen pen2 = new Pen(Color.FromArgb(188, 212, 246));
		A_0.DrawLine(pen, rectangle.Left + 2, rectangle.Top + 1, rectangle.Right - 2, rectangle.Top + 1);
		A_0.DrawLine(pen2, rectangle.Left + 1, rectangle.Top + 2, rectangle.Right - 1, rectangle.Top + 2);
		pen.Dispose();
		pen2.Dispose();
		Pen pen3 = new Pen(Color.FromArgb(137, 173, 228));
		Pen pen4 = new Pen(Color.FromArgb(105, 130, 238));
		A_0.DrawLine(pen3, rectangle.Left + 1, rectangle.Bottom - 2, rectangle.Right - 1, rectangle.Bottom - 2);
		A_0.DrawLine(pen4, rectangle.Left + 2, rectangle.Bottom - 1, rectangle.Right - 2, rectangle.Bottom - 1);
		pen3.Dispose();
		pen4.Dispose();
		Rectangle rect = new Rectangle(rectangle.Left + 1, rectangle.Top + 3, 2, rectangle.Height - 5);
		Rectangle rect2 = new Rectangle(rectangle.Right - 2, rectangle.Top + 3, 2, rectangle.Height - 5);
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(186, 211, 245), Color.FromArgb(137, 173, 228), LinearGradientMode.Vertical);
		A_0.FillRectangle(linearGradientBrush, rect);
		A_0.FillRectangle(linearGradientBrush, rect2);
		linearGradientBrush.Dispose();
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x000430D8 File Offset: 0x000420D8
	private void ᜉ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		Rectangle a_ = this.ᜁ();
		Rectangle rect = new Rectangle(a_.X + 1, a_.Y + 1, a_.Width - 1, a_.Height - 1);
		SolidBrush solidBrush = new SolidBrush(Color.FromArgb(245, 244, 234));
		A_0.FillRectangle(solidBrush, rect);
		solidBrush.Dispose();
		Pen pen = new Pen(Color.FromArgb(201, 199, 186));
		sprỎ.ᜀ(A_0, pen, a_, sprᯅ.ᜄ);
		pen.Dispose();
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0004319C File Offset: 0x0004219C
	private void ᜈ(Graphics A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 17;
			SolidBrush solidBrush;
			StringFormat stringFormat;
			for (;;)
			{
				ContentAlignment imageAlign;
				Point point;
				Rectangle r;
				switch (num)
				{
				case 0:
					if (imageAlign != ContentAlignment.MiddleCenter)
					{
						num = 8;
						continue;
					}
					point.X = (base.ClientRectangle.Width - base.Image.Width) / 2;
					point.Y = (base.ClientRectangle.Height - base.Image.Height) / 2;
					r.Width = 0;
					r.Height = 0;
					r.X = base.ClientRectangle.Width;
					r.Y = base.ClientRectangle.Height;
					num = 6;
					continue;
				case 1:
					if (imageAlign != ContentAlignment.MiddleLeft)
					{
						num = 12;
						continue;
					}
					point.X = 6;
					point.Y = base.ClientRectangle.Height / 2 - base.Image.Height / 2;
					r.Width = base.ClientRectangle.Width - base.Image.Width;
					r.Height = base.ClientRectangle.Height;
					r.X = base.Image.Width;
					r.Y = 0;
					num = 22;
					continue;
				case 2:
					if (base.Image != null)
					{
						num = 9;
						continue;
					}
					A_0.DrawString(this.Text, this.Font, solidBrush, base.ClientRectangle, stringFormat);
					num = 27;
					continue;
				case 3:
					if (base.Enabled)
					{
						num = 23;
						continue;
					}
					ControlPaint.DrawImageDisabled(A_0, base.Image, this.ᜃ.X, this.ᜃ.Y, this.BackColor);
					num = 14;
					continue;
				case 4:
					goto IL_34B;
				case 5:
					num = 28;
					continue;
				case 6:
					goto IL_B7;
				case 7:
					num = 26;
					continue;
				case 8:
					num = 21;
					continue;
				case 9:
					goto IL_1A2;
				case 10:
					goto IL_310;
				case 11:
					solidBrush = new SolidBrush(this.ForeColor);
					num = 13;
					continue;
				case 12:
					num = 15;
					continue;
				case 13:
					goto IL_21A;
				case 14:
					goto IL_34B;
				case 15:
					goto IL_B7;
				case 16:
					A_0.DrawString(this.Text, this.Font, solidBrush, r, stringFormat);
					num = 10;
					continue;
				case 18:
					goto IL_B7;
				case 19:
					num = 1;
					continue;
				case 20:
					if (imageAlign <= ContentAlignment.MiddleLeft)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 21:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A2;
					default:
						if (false)
						{
						}
						if (imageAlign != ContentAlignment.MiddleRight)
						{
							num = 7;
							continue;
						}
						r.Width = base.ClientRectangle.Width - base.Image.Width - 8;
						r.Height = base.ClientRectangle.Height;
						r.X = 0;
						r.Y = 0;
						point.X = r.Width;
						point.Y = base.ClientRectangle.Height / 2 - base.Image.Height / 2;
						num = 18;
						continue;
					}
					break;
				case 22:
					goto IL_B7;
				case 23:
					A_0.DrawImage(base.Image, point);
					num = 4;
					continue;
				case 24:
					goto IL_B7;
				case 25:
					if (ContentAlignment.MiddleCenter != base.ImageAlign)
					{
						num = 16;
						continue;
					}
					goto IL_5D8;
				case 26:
					goto IL_B7;
				case 27:
					goto IL_3A0;
				case 28:
					if (imageAlign != ContentAlignment.TopCenter)
					{
						num = 19;
						continue;
					}
					point.Y = 2;
					point.X = (base.ClientRectangle.Width - base.Image.Width) / 2;
					r.Width = base.ClientRectangle.Width;
					r.Height = base.ClientRectangle.Height - base.Image.Height - 4;
					r.X = base.ClientRectangle.X;
					r.Y = base.Image.Height;
					num = 24;
					continue;
				case 29:
					goto IL_21A;
				}
				if (base.Enabled)
				{
					num = 11;
					continue;
				}
				if (true)
				{
				}
				solidBrush = new SolidBrush(sprỎ.ᜀ());
				num = 29;
				continue;
				IL_B7:
				point.X += this.ᜃ.X;
				point.Y += this.ᜃ.Y;
				num = 3;
				continue;
				IL_1A2:
				r = default(Rectangle);
				point = new Point(6, 4);
				imageAlign = base.ImageAlign;
				num = 20;
				continue;
				IL_21A:
				stringFormat = sprỎ.ᜀ(this.TextAlign);
				stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
				num = 2;
				continue;
				IL_34B:
				num = 25;
			}
			IL_310:
			IL_3A0:
			IL_5D8:
			solidBrush.Dispose();
			stringFormat.Dispose();
			return;
		}
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00043790 File Offset: 0x00042790
	private void ᜇ(Graphics A_0)
	{
		switch (0)
		{
		default:
		{
			Rectangle rect;
			LinearGradientBrush linearGradientBrush;
			for (;;)
			{
				IL_33:
				rect = this.ᜁ();
				linearGradientBrush = null;
				emunType.XPStyle u = this.\u1717;
				for (;;)
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_10A;
						case 1:
							goto IL_B8;
						case 2:
							switch (u)
							{
							case emunType.XPStyle.Default:
								linearGradientBrush = new LinearGradientBrush(rect, sprᯅ.ᜇ, sprᯅ.ᜈ, LinearGradientMode.Vertical);
								num = 3;
								continue;
							case emunType.XPStyle.Blue:
								linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(248, 252, 253), Color.FromArgb(172, 171, 201), LinearGradientMode.Vertical);
								num = 4;
								continue;
							case emunType.XPStyle.OliveGreen:
								linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(250, 250, 240), Color.FromArgb(235, 220, 190), LinearGradientMode.Vertical);
								num = 1;
								continue;
							case emunType.XPStyle.Silver:
								linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(253, 253, 253), Color.FromArgb(205, 205, 205), LinearGradientMode.Vertical);
								num = 5;
								continue;
							default:
								num = 6;
								continue;
							}
							break;
						case 3:
							goto IL_16B;
						case 4:
							goto IL_F9;
						case 5:
							goto IL_14B;
						case 6:
							num = 0;
							continue;
						}
						goto IL_33;
					}
					IL_16B:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_189;
					}
				}
			}
			IL_B8:
			IL_F9:
			IL_10A:
			IL_14B:
			goto IL_194;
			IL_189:
			if (false)
			{
			}
			IL_194:
			float[] factors = new float[]
			{
				0f,
				0.008f,
				1f
			};
			float[] positions = new float[]
			{
				0f,
				0.22f,
				1f
			};
			linearGradientBrush.Blend = new Blend
			{
				Factors = factors,
				Positions = positions
			};
			A_0.FillEllipse(linearGradientBrush, rect);
			return;
		}
		}
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x00043994 File Offset: 0x00042994
	private void ᜆ(Graphics A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Rectangle rectangle = this.ᜁ();
				this.ᜅ(A_0);
				Rectangle rect = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 1, rectangle.Height - 1);
				LinearGradientBrush linearGradientBrush = null;
				emunType.XPStyle u = this.\u1717;
				if (true)
				{
				}
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3D3;
					case 1:
					{
						Pen pen = new Pen(sprᯅ.ᜊ);
						Pen pen2 = new Pen(sprᯅ.ᜋ);
						Pen pen3 = new Pen(sprᯅ.ᜌ);
						A_0.DrawLine(pen, rectangle.Left + 1, rectangle.Bottom - 3, rectangle.Right - 1, rectangle.Bottom - 3);
						A_0.DrawLine(pen2, rectangle.Left + 1, rectangle.Bottom - 2, rectangle.Right - 1, rectangle.Bottom - 2);
						A_0.DrawLine(pen3, rectangle.Left + 2, rectangle.Bottom - 1, rectangle.Right - 2, rectangle.Bottom - 1);
						pen.Dispose();
						pen2.Dispose();
						pen3.Dispose();
						Point point = new Point(rectangle.Right - 2, rectangle.Top + 1);
						Point point2 = new Point(rectangle.Right - 2, rectangle.Bottom - 1);
						Point point3 = new Point(rectangle.Right - 1, rectangle.Top + 2);
						Point point4 = new Point(rectangle.Right - 1, rectangle.Bottom - 2);
						LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(point, point2, sprᯅ.\u170D, sprᯅ.ᜎ);
						Pen pen4 = new Pen(linearGradientBrush2);
						LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(point3, point4, sprᯅ.ᜏ, sprᯅ.ᜐ);
						Pen pen5 = new Pen(linearGradientBrush3);
						A_0.DrawLine(pen4, point, point2);
						A_0.DrawLine(pen5, point3, point4);
						pen4.Dispose();
						pen5.Dispose();
						linearGradientBrush2.Dispose();
						linearGradientBrush3.Dispose();
						Pen pen6 = new Pen(Color.White);
						A_0.DrawLine(pen6, rectangle.Left + 2, rectangle.Top + 1, rectangle.Right - 2, rectangle.Top + 1);
						A_0.DrawLine(pen6, rectangle.Left + 1, rectangle.Top + 2, rectangle.Right - 1, rectangle.Top + 2);
						A_0.DrawLine(pen6, rectangle.Left + 1, rectangle.Top + 3, rectangle.Right - 1, rectangle.Top + 3);
						pen6.Dispose();
						num = 8;
						continue;
					}
					case 2:
						goto IL_3D3;
					case 3:
						goto IL_3D3;
					case 4:
						goto IL_3D3;
					case 5:
						switch (u)
						{
						case emunType.XPStyle.Default:
							linearGradientBrush = new LinearGradientBrush(rect, sprᯅ.ᜇ, sprᯅ.ᜈ, LinearGradientMode.Vertical);
							num = 0;
							continue;
						case emunType.XPStyle.Blue:
							linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(248, 252, 253), Color.FromArgb(172, 171, 201), LinearGradientMode.Vertical);
							num = 4;
							continue;
						case emunType.XPStyle.OliveGreen:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(250, 250, 240), Color.FromArgb(235, 220, 190), LinearGradientMode.Vertical);
								num = 3;
								continue;
							}
							break;
						case emunType.XPStyle.Silver:
							linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(253, 253, 253), Color.FromArgb(205, 205, 205), LinearGradientMode.Vertical);
							num = 2;
							continue;
						default:
							num = 9;
							continue;
						}
						break;
					case 6:
						goto IL_3D3;
					case 7:
						if (this.\u1717 == emunType.XPStyle.Default)
						{
							num = 1;
							continue;
						}
						return;
					case 8:
						return;
					case 9:
						num = 6;
						continue;
					}
					break;
					IL_3D3:
					float[] factors = new float[]
					{
						0f,
						0.08f,
						1f
					};
					float[] positions = new float[]
					{
						0f,
						0.32f,
						1f
					};
					linearGradientBrush.Blend = new Blend
					{
						Factors = factors,
						Positions = positions
					};
					A_0.FillRectangle(linearGradientBrush, rect);
					linearGradientBrush.Dispose();
					this.ᜃ(A_0);
					num = 7;
				}
			}
			return;
		}
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00043E50 File Offset: 0x00042E50
	private void ᜅ(Graphics A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, sprᯅ.ᜅ, sprᯅ.ᜆ, LinearGradientMode.Vertical);
		A_0.FillRectangle(linearGradientBrush, base.ClientRectangle);
		linearGradientBrush.Dispose();
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x00043EB8 File Offset: 0x00042EB8
	private void ᜄ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, sprᯅ.ᜅ, sprᯅ.ᜆ, LinearGradientMode.Vertical);
		A_0.FillRectangle(linearGradientBrush, base.ClientRectangle);
		linearGradientBrush.Dispose();
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x00043F20 File Offset: 0x00042F20
	private void ᜃ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		Pen pen = new Pen(sprᯅ.ᜉ);
		sprỎ.ᜀ(A_0, pen, this.ᜁ(), sprᯅ.ᜄ);
		pen.Dispose();
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x00043F80 File Offset: 0x00042F80
	private void ᜂ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		Pen pen = new Pen(Color.FromArgb(0, 0, 0));
		SmoothingMode smoothingMode = A_0.SmoothingMode;
		A_0.SmoothingMode = SmoothingMode.AntiAlias;
		A_0.DrawEllipse(pen, this.ᜁ());
		A_0.SmoothingMode = smoothingMode;
		pen.Dispose();
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00043FF4 File Offset: 0x00042FF4
	private void ᜁ(Graphics A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		Pen pen = new Pen(Color.FromArgb(137, 173, 228), 2f);
		Rectangle rect = new Rectangle(this.ᜁ().X + 2, this.ᜁ().Y + 1, this.ᜁ().Width - 4, this.ᜁ().Height - 2);
		SmoothingMode smoothingMode = A_0.SmoothingMode;
		A_0.SmoothingMode = SmoothingMode.AntiAlias;
		A_0.DrawEllipse(pen, rect);
		A_0.SmoothingMode = smoothingMode;
		pen.Dispose();
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x000440BC File Offset: 0x000430BC
	private void ᜀ(Graphics A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		Pen pen = new Pen(Color.FromArgb(248, 178, 48), 2f);
		Rectangle rect = new Rectangle(this.ᜁ().X + 2, this.ᜁ().Y + 1, this.ᜁ().Width - 4, this.ᜁ().Height - 2);
		SmoothingMode smoothingMode = A_0.SmoothingMode;
		A_0.SmoothingMode = SmoothingMode.AntiAlias;
		A_0.DrawEllipse(pen, rect);
		A_0.SmoothingMode = smoothingMode;
		pen.Dispose();
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00044180 File Offset: 0x00043180
	protected override void Dispose(bool disposing)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_6C;
				}
				break;
			case 1:
				num = 4;
				continue;
			case 3:
				this.ᜀ.Dispose();
				if (true)
				{
				}
				num = 0;
				continue;
			case 4:
				if (this.ᜀ != null)
				{
					num = 3;
					continue;
				}
				goto IL_91;
			}
			if (!disposing)
			{
				goto IL_91;
			}
			num = 1;
		}
		IL_6C:
		if (false)
		{
		}
		IL_91:
		base.Dispose(disposing);
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00044228 File Offset: 0x00043228
	private void ᜀ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜀ = new Container();
	}

	// Token: 0x040005BE RID: 1470
	private Container ᜀ;

	// Token: 0x040005BF RID: 1471
	private sprᯅ.ControlState ᜁ;

	// Token: 0x040005C0 RID: 1472
	private bool ᜂ;

	// Token: 0x040005C1 RID: 1473
	private Point ᜃ;

	// Token: 0x040005C2 RID: 1474
	private static readonly Size ᜄ;

	// Token: 0x040005C3 RID: 1475
	private static readonly Color ᜅ;

	// Token: 0x040005C4 RID: 1476
	private static readonly Color ᜆ;

	// Token: 0x040005C5 RID: 1477
	private static readonly Color ᜇ;

	// Token: 0x040005C6 RID: 1478
	private static readonly Color ᜈ;

	// Token: 0x040005C7 RID: 1479
	private static readonly Color ᜉ;

	// Token: 0x040005C8 RID: 1480
	private static readonly Color ᜊ;

	// Token: 0x040005C9 RID: 1481
	private static readonly Color ᜋ;

	// Token: 0x040005CA RID: 1482
	private static readonly Color ᜌ;

	// Token: 0x040005CB RID: 1483
	private static readonly Color \u170D;

	// Token: 0x040005CC RID: 1484
	private static readonly Color ᜎ;

	// Token: 0x040005CD RID: 1485
	private static readonly Color ᜏ;

	// Token: 0x040005CE RID: 1486
	private static readonly Color ᜐ;

	// Token: 0x040005CF RID: 1487
	private static readonly Color ᜑ;

	// Token: 0x040005D0 RID: 1488
	private static readonly Color \u1712;

	// Token: 0x040005D1 RID: 1489
	private static readonly Color \u1713;

	// Token: 0x040005D2 RID: 1490
	private static readonly Color \u1714;

	// Token: 0x040005D3 RID: 1491
	private static readonly Color \u1715;

	// Token: 0x040005D4 RID: 1492
	private static readonly Color \u1716;

	// Token: 0x040005D5 RID: 1493
	private emunType.XPStyle \u1717;

	// Token: 0x040005D6 RID: 1494
	private emunType.BtnShape \u1718;

	// Token: 0x02000128 RID: 296
	public enum ControlState
	{
		// Token: 0x040005D8 RID: 1496
		Normal,
		// Token: 0x040005D9 RID: 1497
		Hover,
		// Token: 0x040005DA RID: 1498
		Pressed,
		// Token: 0x040005DB RID: 1499
		Default,
		// Token: 0x040005DC RID: 1500
		Disabled
	}
}
