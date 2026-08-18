using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200026D RID: 621
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("Enter")]
	[DefaultProperty("Text")]
	[Designer("System.Windows.Forms.Design.GroupBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionGroupBox")]
	public class GroupBox : Control
	{
		// Token: 0x060027B1 RID: 10161 RVA: 0x000B9060 File Offset: 0x000B7260
		public GroupBox()
		{
			base.SetState2(2048, true);
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, this.OwnerDraw);
			base.SetStyle(ControlStyles.Selectable, false);
			this.TabStop = false;
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x000B90B9 File Offset: 0x000B72B9
		// (set) Token: 0x060027B3 RID: 10163 RVA: 0x000B90C1 File Offset: 0x000B72C1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x060027B5 RID: 10165 RVA: 0x00011A4D File Offset: 0x0000FC4D
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x140001B7 RID: 439
		// (add) Token: 0x060027B6 RID: 10166 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x060027B7 RID: 10167 RVA: 0x00011A5F File Offset: 0x0000FC5F
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

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060027B8 RID: 10168 RVA: 0x000236ED File Offset: 0x000218ED
		// (set) Token: 0x060027B9 RID: 10169 RVA: 0x000B90CC File Offset: 0x000B72CC
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
					if (this.ParentInternal != null)
					{
						if (this.ParentInternal.LayoutEngine == DefaultLayout.Instance)
						{
							this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.AutoSize);
					}
				}
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x000B9150 File Offset: 0x000B7350
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				if (!this.OwnerDraw)
				{
					createParams.ClassName = "BUTTON";
					createParams.Style |= 7;
				}
				else
				{
					createParams.ClassName = null;
					createParams.Style &= -8;
				}
				createParams.ExStyle |= 65536;
				return createParams;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x000B91B0 File Offset: 0x000B73B0
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(3);
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x000B91B8 File Offset: 0x000B73B8
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x000B91C8 File Offset: 0x000B73C8
		public override Rectangle DisplayRectangle
		{
			get
			{
				Size clientSize = base.ClientSize;
				if (this.fontHeight == -1)
				{
					this.fontHeight = this.Font.Height;
					this.cachedFont = this.Font;
				}
				else if (this.cachedFont != this.Font)
				{
					this.fontHeight = this.Font.Height;
					this.cachedFont = this.Font;
				}
				Padding padding = base.Padding;
				return new Rectangle(padding.Left, this.fontHeight + padding.Top, Math.Max(clientSize.Width - padding.Horizontal, 0), Math.Max(clientSize.Height - this.fontHeight - padding.Vertical, 0));
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x000B9281 File Offset: 0x000B7481
		// (set) Token: 0x060027BF RID: 10175 RVA: 0x000B928C File Offset: 0x000B748C
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Standard)]
		[SRDescription("ButtonFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flatStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				if (this.flatStyle != value)
				{
					bool ownerDraw = this.OwnerDraw;
					this.flatStyle = value;
					bool flag = this.OwnerDraw != ownerDraw;
					base.SetStyle(ControlStyles.ContainerControl, true);
					base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.UserMouse | ControlStyles.SupportsTransparentBackColor, this.OwnerDraw);
					if (flag)
					{
						base.RecreateHandle();
						return;
					}
					this.Refresh();
				}
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000B930C File Offset: 0x000B750C
		private bool OwnerDraw
		{
			get
			{
				return this.FlatStyle != FlatStyle.System;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x060027C2 RID: 10178 RVA: 0x000B2619 File Offset: 0x000B0819
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
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

		// Token: 0x140001B8 RID: 440
		// (add) Token: 0x060027C3 RID: 10179 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x060027C4 RID: 10180 RVA: 0x000B262B File Offset: 0x000B082B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
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

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x060027C6 RID: 10182 RVA: 0x000B931C File Offset: 0x000B751C
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				bool visible = base.Visible;
				try
				{
					if (visible && base.IsHandleCreated)
					{
						base.SendMessage(11, 0, 0);
					}
					base.Text = value;
				}
				finally
				{
					if (visible && base.IsHandleCreated)
					{
						base.SendMessage(11, 1, 0);
					}
				}
				base.Invalidate(true);
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000249A3 File Offset: 0x00022BA3
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x000249AB File Offset: 0x00022BAB
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return base.UseCompatibleTextRenderingInt;
			}
			set
			{
				base.UseCompatibleTextRenderingInt = value;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x140001B9 RID: 441
		// (add) Token: 0x060027CA RID: 10186 RVA: 0x000131E8 File Offset: 0x000113E8
		// (remove) Token: 0x060027CB RID: 10187 RVA: 0x000131F1 File Offset: 0x000113F1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		// Token: 0x140001BA RID: 442
		// (add) Token: 0x060027CC RID: 10188 RVA: 0x000131FA File Offset: 0x000113FA
		// (remove) Token: 0x060027CD RID: 10189 RVA: 0x00013203 File Offset: 0x00011403
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		// Token: 0x140001BB RID: 443
		// (add) Token: 0x060027CE RID: 10190 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x060027CF RID: 10191 RVA: 0x000238FC File Offset: 0x00021AFC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		// Token: 0x140001BC RID: 444
		// (add) Token: 0x060027D0 RID: 10192 RVA: 0x00023905 File Offset: 0x00021B05
		// (remove) Token: 0x060027D1 RID: 10193 RVA: 0x0002390E File Offset: 0x00021B0E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.MouseDoubleClick += value;
			}
			remove
			{
				base.MouseDoubleClick -= value;
			}
		}

		// Token: 0x140001BD RID: 445
		// (add) Token: 0x060027D2 RID: 10194 RVA: 0x000B9380 File Offset: 0x000B7580
		// (remove) Token: 0x060027D3 RID: 10195 RVA: 0x000B9389 File Offset: 0x000B7589
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		// Token: 0x140001BE RID: 446
		// (add) Token: 0x060027D4 RID: 10196 RVA: 0x000B9392 File Offset: 0x000B7592
		// (remove) Token: 0x060027D5 RID: 10197 RVA: 0x000B939B File Offset: 0x000B759B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		// Token: 0x140001BF RID: 447
		// (add) Token: 0x060027D6 RID: 10198 RVA: 0x000B93A4 File Offset: 0x000B75A4
		// (remove) Token: 0x060027D7 RID: 10199 RVA: 0x000B93AD File Offset: 0x000B75AD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		// Token: 0x140001C0 RID: 448
		// (add) Token: 0x060027D8 RID: 10200 RVA: 0x000B93B6 File Offset: 0x000B75B6
		// (remove) Token: 0x060027D9 RID: 10201 RVA: 0x000B93BF File Offset: 0x000B75BF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				base.MouseDown += value;
			}
			remove
			{
				base.MouseDown -= value;
			}
		}

		// Token: 0x140001C1 RID: 449
		// (add) Token: 0x060027DA RID: 10202 RVA: 0x000B93C8 File Offset: 0x000B75C8
		// (remove) Token: 0x060027DB RID: 10203 RVA: 0x000B93D1 File Offset: 0x000B75D1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				base.MouseUp += value;
			}
			remove
			{
				base.MouseUp -= value;
			}
		}

		// Token: 0x140001C2 RID: 450
		// (add) Token: 0x060027DC RID: 10204 RVA: 0x00011C92 File Offset: 0x0000FE92
		// (remove) Token: 0x060027DD RID: 10205 RVA: 0x00011C9B File Offset: 0x0000FE9B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		// Token: 0x140001C3 RID: 451
		// (add) Token: 0x060027DE RID: 10206 RVA: 0x00011C5C File Offset: 0x0000FE5C
		// (remove) Token: 0x060027DF RID: 10207 RVA: 0x00011C65 File Offset: 0x0000FE65
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler MouseEnter
		{
			add
			{
				base.MouseEnter += value;
			}
			remove
			{
				base.MouseEnter -= value;
			}
		}

		// Token: 0x140001C4 RID: 452
		// (add) Token: 0x060027E0 RID: 10208 RVA: 0x00011C6E File Offset: 0x0000FE6E
		// (remove) Token: 0x060027E1 RID: 10209 RVA: 0x00011C77 File Offset: 0x0000FE77
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler MouseLeave
		{
			add
			{
				base.MouseLeave += value;
			}
			remove
			{
				base.MouseLeave -= value;
			}
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000B93DC File Offset: 0x000B75DC
		protected override void OnPaint(PaintEventArgs e)
		{
			if (Application.RenderWithVisualStyles && base.Width >= 10 && base.Height >= 10)
			{
				GroupBoxState state = base.Enabled ? GroupBoxState.Normal : GroupBoxState.Disabled;
				TextFormatFlags textFormatFlags = TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform;
				if (!this.ShowKeyboardCues)
				{
					textFormatFlags |= TextFormatFlags.HidePrefix;
				}
				if (this.RightToLeft == RightToLeft.Yes)
				{
					textFormatFlags |= (TextFormatFlags.Right | TextFormatFlags.RightToLeft);
				}
				if (this.ShouldSerializeForeColor() || !base.Enabled)
				{
					Color textColor = base.Enabled ? this.ForeColor : TextRenderer.DisabledTextColor(this.BackColor);
					GroupBoxRenderer.DrawGroupBox(e.Graphics, new Rectangle(0, 0, base.Width, base.Height), this.Text, this.Font, textColor, textFormatFlags, state);
				}
				else
				{
					GroupBoxRenderer.DrawGroupBox(e.Graphics, new Rectangle(0, 0, base.Width, base.Height), this.Text, this.Font, textFormatFlags, state);
				}
			}
			else
			{
				this.DrawGroupBox(e);
			}
			base.OnPaint(e);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000B94D8 File Offset: 0x000B76D8
		private void DrawGroupBox(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = base.ClientRectangle;
			int num = 8;
			Color disabledColor = base.DisabledColor;
			Pen pen = new Pen(ControlPaint.Light(disabledColor, 1f));
			Pen pen2 = new Pen(ControlPaint.Dark(disabledColor, 0f));
			clientRectangle.X += num;
			clientRectangle.Width -= 2 * num;
			try
			{
				Size size;
				if (this.UseCompatibleTextRendering)
				{
					using (Brush brush = new SolidBrush(this.ForeColor))
					{
						using (StringFormat stringFormat = new StringFormat())
						{
							stringFormat.HotkeyPrefix = (this.ShowKeyboardCues ? HotkeyPrefix.Show : HotkeyPrefix.Hide);
							if (this.RightToLeft == RightToLeft.Yes)
							{
								stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
							}
							size = Size.Ceiling(graphics.MeasureString(this.Text, this.Font, clientRectangle.Width, stringFormat));
							if (base.Enabled)
							{
								graphics.DrawString(this.Text, this.Font, brush, clientRectangle, stringFormat);
								goto IL_1E6;
							}
							ControlPaint.DrawStringDisabled(graphics, this.Text, this.Font, disabledColor, clientRectangle, stringFormat);
							goto IL_1E6;
						}
					}
				}
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(graphics))
				{
					IntTextFormatFlags intTextFormatFlags = IntTextFormatFlags.TextBoxControl | IntTextFormatFlags.WordBreak;
					if (!this.ShowKeyboardCues)
					{
						intTextFormatFlags |= IntTextFormatFlags.HidePrefix;
					}
					if (this.RightToLeft == RightToLeft.Yes)
					{
						intTextFormatFlags |= IntTextFormatFlags.RightToLeft;
						intTextFormatFlags |= IntTextFormatFlags.Right;
					}
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(this.Font))
					{
						size = windowsGraphics.MeasureText(this.Text, windowsFont, new Size(clientRectangle.Width, int.MaxValue), intTextFormatFlags);
						if (base.Enabled)
						{
							windowsGraphics.DrawText(this.Text, windowsFont, clientRectangle, this.ForeColor, intTextFormatFlags);
						}
						else
						{
							ControlPaint.DrawStringDisabled(windowsGraphics, this.Text, this.Font, disabledColor, clientRectangle, (TextFormatFlags)intTextFormatFlags);
						}
					}
				}
				IL_1E6:
				int num2 = num;
				if (this.RightToLeft == RightToLeft.Yes)
				{
					num2 += clientRectangle.Width - size.Width;
				}
				int x = Math.Min(num2 + size.Width, base.Width - 6);
				int num3 = base.FontHeight / 2;
				if (SystemInformation.HighContrast && AccessibilityImprovements.Level1)
				{
					Color color;
					if (base.Enabled)
					{
						color = this.ForeColor;
					}
					else
					{
						color = SystemColors.GrayText;
					}
					bool flag = !color.IsSystemColor;
					Pen pen3 = null;
					try
					{
						if (flag)
						{
							pen3 = new Pen(color);
						}
						else
						{
							pen3 = SystemPens.FromSystemColor(color);
						}
						graphics.DrawLine(pen3, 0, num3, 0, base.Height);
						graphics.DrawLine(pen3, 0, base.Height - 1, base.Width, base.Height - 1);
						graphics.DrawLine(pen3, 0, num3, num2, num3);
						graphics.DrawLine(pen3, x, num3, base.Width - 1, num3);
						graphics.DrawLine(pen3, base.Width - 1, num3, base.Width - 1, base.Height - 1);
						return;
					}
					finally
					{
						if (flag && pen3 != null)
						{
							pen3.Dispose();
						}
					}
				}
				graphics.DrawLine(pen, 1, num3, 1, base.Height - 1);
				graphics.DrawLine(pen2, 0, num3, 0, base.Height - 2);
				graphics.DrawLine(pen, 0, base.Height - 1, base.Width, base.Height - 1);
				graphics.DrawLine(pen2, 0, base.Height - 2, base.Width - 1, base.Height - 2);
				graphics.DrawLine(pen2, 0, num3 - 1, num2, num3 - 1);
				graphics.DrawLine(pen, 1, num3, num2, num3);
				graphics.DrawLine(pen2, x, num3 - 1, base.Width - 2, num3 - 1);
				graphics.DrawLine(pen, x, num3, base.Width - 1, num3);
				graphics.DrawLine(pen, base.Width - 1, num3 - 1, base.Width - 1, base.Height - 1);
				graphics.DrawLine(pen2, base.Width - 2, num3, base.Width - 2, base.Height - 2);
			}
			finally
			{
				pen.Dispose();
				pen2.Dispose();
			}
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000B9994 File Offset: 0x000B7B94
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size sz = this.SizeFromClientSize(Size.Empty);
			Size sz2 = sz + new Size(0, this.fontHeight) + base.Padding.Size;
			Size preferredSize = this.LayoutEngine.GetPreferredSize(this, proposedSize - sz2);
			return preferredSize + sz2;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000B99EE File Offset: 0x000B7BEE
		protected override void OnFontChanged(EventArgs e)
		{
			this.fontHeight = -1;
			this.cachedFont = null;
			base.Invalidate();
			base.OnFontChanged(e);
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000B9A0C File Offset: 0x000B7C0C
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.Text) && this.CanProcessMnemonic())
			{
				IntSecurity.ModifyFocus.Assert();
				try
				{
					base.SelectNextControl(null, true, true, true, false);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return true;
			}
			return false;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000B9A60 File Offset: 0x000B7C60
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			if (factor.Width != 1f && factor.Height != 1f)
			{
				this.fontHeight = -1;
				this.cachedFont = null;
			}
			base.ScaleControl(factor, specified);
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x00028D57 File Offset: 0x00026F57
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000B9A94 File Offset: 0x000B7C94
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", Text: " + this.Text;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000B9ABC File Offset: 0x000B7CBC
		private void WmEraseBkgnd(ref Message m)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			SafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
			using (Graphics graphics = Graphics.FromHdcInternal(m.WParam))
			{
				using (Brush brush = new SolidBrush(this.BackColor))
				{
					graphics.FillRectangle(brush, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
				}
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000B9B68 File Offset: 0x000B7D68
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (this.OwnerDraw)
			{
				base.WndProc(ref m);
				return;
			}
			int msg = m.Msg;
			if (msg != 20)
			{
				if (msg != 61)
				{
					if (msg == 792)
					{
						goto IL_29;
					}
					base.WndProc(ref m);
				}
				else
				{
					base.WndProc(ref m);
					if ((int)((long)m.LParam) == -12)
					{
						m.Result = IntPtr.Zero;
						return;
					}
				}
				return;
			}
			IL_29:
			this.WmEraseBkgnd(ref m);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000B9BD0 File Offset: 0x000B7DD0
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new GroupBox.GroupBoxAccessibleObject(this);
		}

		// Token: 0x04001061 RID: 4193
		private int fontHeight = -1;

		// Token: 0x04001062 RID: 4194
		private Font cachedFont;

		// Token: 0x04001063 RID: 4195
		private FlatStyle flatStyle = FlatStyle.Standard;

		// Token: 0x020006A4 RID: 1700
		[ComVisible(true)]
		internal class GroupBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060067F1 RID: 26609 RVA: 0x0009B963 File Offset: 0x00099B63
			internal GroupBoxAccessibleObject(GroupBox owner) : base(owner)
			{
			}

			// Token: 0x1700168B RID: 5771
			// (get) Token: 0x060067F2 RID: 26610 RVA: 0x001841A0 File Offset: 0x001823A0
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.Grouping;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Grouping;
				}
			}

			// Token: 0x060067F3 RID: 26611 RVA: 0x0009B96C File Offset: 0x00099B6C
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerControlDestroyed() && (AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported());
			}

			// Token: 0x060067F4 RID: 26612 RVA: 0x001841CC File Offset: 0x001823CC
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerControlDestroyed() && ((AccessibilityImprovements.Level3 && patternId == 10018) || base.IsPatternSupported(patternId));
			}

			// Token: 0x060067F5 RID: 26613 RVA: 0x001841F0 File Offset: 0x001823F0
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID != 30003)
				{
					if (propertyID != 30005)
					{
						if (propertyID == 30009)
						{
							return true;
						}
					}
					else if (AccessibilityImprovements.Level3)
					{
						return this.Name;
					}
					return base.GetPropertyValue(propertyID);
				}
				return 50026;
			}
		}
	}
}
