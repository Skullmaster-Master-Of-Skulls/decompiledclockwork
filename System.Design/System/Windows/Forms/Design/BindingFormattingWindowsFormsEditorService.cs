using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001A0 RID: 416
	internal class BindingFormattingWindowsFormsEditorService : Panel, IWindowsFormsEditorService, ITypeDescriptorContext, IServiceProvider
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x00044FE4 File Offset: 0x00043FE4
		public BindingFormattingWindowsFormsEditorService()
		{
			this.BackColor = SystemColors.Window;
			this.Text = SR.GetString("DataGridNoneString");
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.Selectable, true);
			base.SetStyle(ControlStyles.UseTextForAccessibility, true);
			base.AccessibleRole = AccessibleRole.DropList;
			base.TabStop = true;
			this.button = new BindingFormattingWindowsFormsEditorService.DropDownButton(this);
			this.button.FlatStyle = FlatStyle.Popup;
			this.button.Image = this.CreateDownArrow();
			this.button.Padding = new Padding(0);
			this.button.BackColor = SystemColors.Control;
			this.button.ForeColor = SystemColors.ControlText;
			this.button.Click += this.button_Click;
			this.button.Size = new Size(SystemInformation.VerticalScrollBarArrowHeight, this.Font.Height + 2);
			this.button.AccessibleName = SR.GetString("BindingFormattingDialogDataSourcePickerDropDownAccName");
			base.Controls.Add(this.button);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00045105 File Offset: 0x00044105
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new BindingFormattingWindowsFormsEditorService.BindingFormattingWindowFormsEditorAccessibleObject(this);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00045110 File Offset: 0x00044110
		private Bitmap CreateDownArrow()
		{
			Bitmap result = null;
			try
			{
				Icon icon = new Icon(typeof(BindingFormattingDialog), "BindingFormattingDialog.Arrow.ico");
				result = icon.ToBitmap();
				icon.Dispose();
			}
			catch
			{
				result = new Bitmap(16, 16);
			}
			return result;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00045164 File Offset: 0x00044164
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, this.PreferredHeight, specified);
			int height2 = base.Height - 2;
			int horizontalScrollBarThumbWidth = SystemInformation.HorizontalScrollBarThumbWidth;
			int y2 = base.Width - horizontalScrollBarThumbWidth - 2;
			int x2 = 1;
			if (this.RightToLeft == RightToLeft.No)
			{
				this.button.Bounds = new Rectangle(x2, y2, horizontalScrollBarThumbWidth, height2);
				return;
			}
			this.button.Bounds = new Rectangle(x2, 2, horizontalScrollBarThumbWidth, height2);
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x000451D0 File Offset: 0x000441D0
		private int PreferredHeight
		{
			get
			{
				return TextRenderer.MeasureText("j^", this.Font, new Size(32767, (int)((double)base.FontHeight * 1.25))).Height + SystemInformation.BorderSize.Height * 8 + base.Padding.Size.Height;
			}
		}

		// Token: 0x1700027C RID: 636
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x00045238 File Offset: 0x00044238
		public ITypeDescriptorContext Context
		{
			set
			{
				this.context = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00045244 File Offset: 0x00044244
		IContainer ITypeDescriptorContext.Container
		{
			get
			{
				if (this.ownerComponent == null)
				{
					return null;
				}
				ISite site = this.ownerComponent.Site;
				if (site == null)
				{
					return null;
				}
				return site.Container;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00045272 File Offset: 0x00044272
		object ITypeDescriptorContext.Instance
		{
			get
			{
				return this.ownerComponent;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0004527A File Offset: 0x0004427A
		PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0004527D File Offset: 0x0004427D
		void ITypeDescriptorContext.OnComponentChanged()
		{
			if (this.context != null)
			{
				this.context.OnComponentChanged();
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00045292 File Offset: 0x00044292
		bool ITypeDescriptorContext.OnComponentChanging()
		{
			return this.context == null || this.context.OnComponentChanging();
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000452A9 File Offset: 0x000442A9
		object IServiceProvider.GetService(Type type)
		{
			if (type == typeof(IWindowsFormsEditorService))
			{
				return this;
			}
			if (this.context != null)
			{
				return this.context.GetService(type);
			}
			return null;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000452D0 File Offset: 0x000442D0
		void IWindowsFormsEditorService.CloseDropDown()
		{
			this.dropDownHolder.SetComponent(null);
			this.dropDownHolder.Visible = false;
			this.button.Focus();
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000452F8 File Offset: 0x000442F8
		void IWindowsFormsEditorService.DropDownControl(Control ctl)
		{
			if (this.dropDownHolder == null)
			{
				this.dropDownHolder = new DropDownHolder(this);
			}
			this.dropDownHolder.SetComponent(ctl);
			this.dropDownHolder.Location = base.PointToScreen(new Point(0, base.Height));
			try
			{
				this.dropDownHolder.Visible = true;
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), -8, new HandleRef(this, base.Handle));
				this.dropDownHolder.FocusComponent();
				this.dropDownHolder.DoModalLoop();
			}
			finally
			{
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), -8, new HandleRef(null, IntPtr.Zero));
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000453CC File Offset: 0x000443CC
		DialogResult IWindowsFormsEditorService.ShowDialog(Form form)
		{
			return form.ShowDialog();
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x000453D4 File Offset: 0x000443D4
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x000453DC File Offset: 0x000443DC
		public Binding Binding
		{
			get
			{
				return this.binding;
			}
			set
			{
				if (this.binding == value)
				{
					return;
				}
				this.binding = value;
				if (this.binding != null)
				{
					this.Text = BindingFormattingWindowsFormsEditorService.ConstructDisplayTextFromBinding(this.binding);
				}
				else
				{
					this.Text = SR.GetString("DataGridNoneString");
				}
				base.Invalidate();
			}
		}

		// Token: 0x17000281 RID: 641
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x0004542B File Offset: 0x0004442B
		public DataSourceUpdateMode DefaultDataSourceUpdateMode
		{
			set
			{
				this.defaultDataSourceUpdateMode = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x00045434 File Offset: 0x00044434
		public IComponent OwnerComponent
		{
			set
			{
				this.ownerComponent = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x0004543D File Offset: 0x0004443D
		public string PropertyName
		{
			set
			{
				this.propertyName = value;
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000F9D RID: 3997 RVA: 0x00045446 File Offset: 0x00044446
		// (remove) Token: 0x06000F9E RID: 3998 RVA: 0x0004545F File Offset: 0x0004445F
		public event EventHandler PropertyValueChanged
		{
			add
			{
				this.propertyValueChanged = (EventHandler)Delegate.Combine(this.propertyValueChanged, value);
			}
			remove
			{
				this.propertyValueChanged = (EventHandler)Delegate.Remove(this.propertyValueChanged, value);
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00045478 File Offset: 0x00044478
		private void button_Click(object sender, EventArgs e)
		{
			this.DropDownPicker();
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00045480 File Offset: 0x00044480
		private static string ConstructDisplayTextFromBinding(Binding binding)
		{
			string str;
			if (binding.DataSource == null)
			{
				str = SR.GetString("DataGridNoneString");
			}
			else if (binding.DataSource is IComponent)
			{
				IComponent component = binding.DataSource as IComponent;
				if (component.Site != null)
				{
					str = component.Site.Name;
				}
				else
				{
					str = "";
				}
			}
			else if (binding.DataSource is IListSource || binding.DataSource is IList || binding.DataSource is Array)
			{
				str = SR.GetString("BindingFormattingDialogList");
			}
			else
			{
				string text = TypeDescriptor.GetClassName(binding.DataSource);
				int num = text.LastIndexOf(".");
				if (num != -1)
				{
					text = text.Substring(num + 1);
				}
				str = string.Format(CultureInfo.CurrentCulture, "({0})", new object[]
				{
					text
				});
			}
			return str + " - " + binding.BindingMemberInfo.BindingMember;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00045574 File Offset: 0x00044574
		private void DropDownPicker()
		{
			if (this.designBindingPicker == null)
			{
				this.designBindingPicker = new DesignBindingPicker();
				this.designBindingPicker.Width = base.Width;
			}
			DesignBinding initialSelectedItem = null;
			if (this.binding != null)
			{
				initialSelectedItem = new DesignBinding(this.binding.DataSource, this.binding.BindingMemberInfo.BindingMember);
			}
			DesignBinding designBinding = this.designBindingPicker.Pick(this, this, true, true, false, null, string.Empty, initialSelectedItem);
			if (designBinding == null)
			{
				return;
			}
			Binding binding = this.binding;
			Binding binding2 = null;
			string formatString = (binding != null) ? binding.FormatString : string.Empty;
			IFormatProvider formatInfo = (binding != null) ? binding.FormatInfo : null;
			object nullValue = (binding != null) ? binding.NullValue : null;
			DataSourceUpdateMode dataSourceUpdateMode = (binding != null) ? binding.DataSourceUpdateMode : this.defaultDataSourceUpdateMode;
			if (designBinding.DataSource != null && !string.IsNullOrEmpty(designBinding.DataMember))
			{
				binding2 = new Binding(this.propertyName, designBinding.DataSource, designBinding.DataMember, true, dataSourceUpdateMode, nullValue, formatString, formatInfo);
			}
			this.Binding = binding2;
			bool flag = binding2 == null || binding != null || (binding2 != null && binding == null) || (binding2 != null && binding != null && (binding2.DataSource != binding.DataSource || !binding2.BindingMemberInfo.Equals(binding.BindingMemberInfo)));
			if (flag)
			{
				this.OnPropertyValueChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000456EE File Offset: 0x000446EE
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			base.Select();
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000456FD File Offset: 0x000446FD
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0004570C File Offset: 0x0004470C
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			base.Invalidate();
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0004571C File Offset: 0x0004471C
		protected override void OnPaint(PaintEventArgs p)
		{
			base.OnPaint(p);
			string text = this.Text;
			if (ComboBoxRenderer.IsSupported)
			{
				Rectangle rectangle = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width, base.ClientRectangle.Height);
				SolidBrush solidBrush;
				SolidBrush solidBrush2;
				ComboBoxState state;
				if (!base.Enabled)
				{
					solidBrush = (SolidBrush)SystemBrushes.ControlDark;
					solidBrush2 = (SolidBrush)SystemBrushes.Control;
					state = ComboBoxState.Disabled;
				}
				else if (base.ContainsFocus)
				{
					solidBrush = (SolidBrush)SystemBrushes.HighlightText;
					solidBrush2 = (SolidBrush)SystemBrushes.Highlight;
					state = ComboBoxState.Hot;
				}
				else
				{
					solidBrush = (SolidBrush)SystemBrushes.WindowText;
					solidBrush2 = (SolidBrush)SystemBrushes.Window;
					state = ComboBoxState.Normal;
				}
				ComboBoxRenderer.DrawTextBox(p.Graphics, rectangle, string.Empty, this.Font, state);
				Graphics graphics = p.Graphics;
				rectangle.Inflate(-2, -2);
				ControlPaint.DrawBorder(graphics, rectangle, solidBrush2.Color, ButtonBorderStyle.None);
				rectangle.Inflate(-1, -1);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					rectangle.X += this.button.Width;
				}
				rectangle.Width -= this.button.Width;
				graphics.FillRectangle(solidBrush2, rectangle);
				TextFormatFlags textFormatFlags = TextFormatFlags.VerticalCenter;
				if (this.RightToLeft == RightToLeft.No)
				{
					textFormatFlags = textFormatFlags;
				}
				else
				{
					textFormatFlags |= TextFormatFlags.Right;
				}
				if (base.ContainsFocus)
				{
					ControlPaint.DrawFocusRectangle(graphics, rectangle, Color.Empty, solidBrush2.Color);
				}
				TextRenderer.DrawText(graphics, text, this.Font, rectangle, solidBrush.Color, textFormatFlags);
				return;
			}
			if (!string.IsNullOrEmpty(text))
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Near;
				stringFormat.LineAlignment = StringAlignment.Near;
				Rectangle clientRectangle = base.ClientRectangle;
				Rectangle bounds = new Rectangle(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, clientRectangle.Height);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					bounds.X += this.button.Width;
				}
				bounds.Width -= this.button.Width;
				TextFormatFlags textFormatFlags2 = TextFormatFlags.VerticalCenter;
				if (this.RightToLeft == RightToLeft.No)
				{
					textFormatFlags2 = textFormatFlags2;
				}
				else
				{
					textFormatFlags2 |= TextFormatFlags.Right;
				}
				TextRenderer.DrawText(p.Graphics, text, this.Font, bounds, this.ForeColor, textFormatFlags2);
				stringFormat.Dispose();
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0004597D File Offset: 0x0004497D
		protected void OnPropertyValueChanged(EventArgs e)
		{
			if (this.propertyValueChanged != null)
			{
				this.propertyValueChanged(this, e);
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00045994 File Offset: 0x00044994
		protected override bool ProcessDialogKey(Keys keyData)
		{
			Keys modifierKeys = Control.ModifierKeys;
			if ((modifierKeys & Keys.Alt) == Keys.Alt && (keyData & Keys.KeyCode) == Keys.Down)
			{
				this.DropDownPicker();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x0400100D RID: 4109
		private ITypeDescriptorContext context;

		// Token: 0x0400100E RID: 4110
		private DropDownHolder dropDownHolder;

		// Token: 0x0400100F RID: 4111
		private BindingFormattingWindowsFormsEditorService.DropDownButton button;

		// Token: 0x04001010 RID: 4112
		private EventHandler propertyValueChanged;

		// Token: 0x04001011 RID: 4113
		private Binding binding;

		// Token: 0x04001012 RID: 4114
		private IComponent ownerComponent;

		// Token: 0x04001013 RID: 4115
		private DataSourceUpdateMode defaultDataSourceUpdateMode;

		// Token: 0x04001014 RID: 4116
		private DesignBindingPicker designBindingPicker;

		// Token: 0x04001015 RID: 4117
		private string propertyName = string.Empty;

		// Token: 0x020001A1 RID: 417
		private class BindingFormattingWindowFormsEditorAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06000FA8 RID: 4008 RVA: 0x000459CF File Offset: 0x000449CF
			public BindingFormattingWindowFormsEditorAccessibleObject(BindingFormattingWindowsFormsEditorService owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x000459DF File Offset: 0x000449DF
			public override string Name
			{
				get
				{
					return SR.GetString("BindingFormattingDialogBindingPickerAccName");
				}
			}

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06000FAA RID: 4010 RVA: 0x000459EB File Offset: 0x000449EB
			public override string Value
			{
				get
				{
					return this.owner.Text;
				}
			}

			// Token: 0x06000FAB RID: 4011 RVA: 0x000459F8 File Offset: 0x000449F8
			public override void DoDefaultAction()
			{
				this.owner.DropDownPicker();
			}

			// Token: 0x04001016 RID: 4118
			private BindingFormattingWindowsFormsEditorService owner;
		}

		// Token: 0x020001A2 RID: 418
		private class DropDownButton : Button
		{
			// Token: 0x06000FAC RID: 4012 RVA: 0x00045A05 File Offset: 0x00044A05
			public DropDownButton(BindingFormattingWindowsFormsEditorService owner)
			{
				this.owner = owner;
				base.TabStop = false;
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00045A1B File Offset: 0x00044A1B
			protected override Size DefaultSize
			{
				get
				{
					return new Size(17, 19);
				}
			}

			// Token: 0x06000FAE RID: 4014 RVA: 0x00045A28 File Offset: 0x00044A28
			protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
			{
				height = Math.Min(height, this.owner.Height - 2);
				width = SystemInformation.HorizontalScrollBarThumbWidth;
				y = 1;
				if (base.Parent != null)
				{
					if (base.Parent.RightToLeft == RightToLeft.No)
					{
						x = base.Parent.Width - width - 1;
					}
					else
					{
						x = 1;
					}
				}
				base.SetBoundsCore(x, y, width, height, specified);
			}

			// Token: 0x06000FAF RID: 4015 RVA: 0x00045A8D File Offset: 0x00044A8D
			protected override void OnEnabledChanged(EventArgs e)
			{
				base.OnEnabledChanged(e);
				if (!base.Enabled)
				{
					this.mouseIsDown = false;
					this.mouseIsOver = false;
				}
			}

			// Token: 0x06000FB0 RID: 4016 RVA: 0x00045AAC File Offset: 0x00044AAC
			protected override void OnKeyDown(KeyEventArgs kevent)
			{
				base.OnKeyDown(kevent);
				if (kevent.KeyData == Keys.Space)
				{
					this.mouseIsDown = true;
					base.Invalidate();
				}
			}

			// Token: 0x06000FB1 RID: 4017 RVA: 0x00045ACC File Offset: 0x00044ACC
			protected override void OnKeyUp(KeyEventArgs kevent)
			{
				base.OnKeyUp(kevent);
				if (this.mouseIsDown)
				{
					this.mouseIsDown = false;
					base.Invalidate();
				}
			}

			// Token: 0x06000FB2 RID: 4018 RVA: 0x00045AEA File Offset: 0x00044AEA
			protected override void OnLostFocus(EventArgs e)
			{
				base.OnLostFocus(e);
				this.mouseIsDown = false;
				base.Invalidate();
			}

			// Token: 0x06000FB3 RID: 4019 RVA: 0x00045B00 File Offset: 0x00044B00
			protected override void OnMouseEnter(EventArgs e)
			{
				base.OnMouseEnter(e);
				if (!this.mouseIsOver)
				{
					this.mouseIsOver = true;
					base.Invalidate();
				}
			}

			// Token: 0x06000FB4 RID: 4020 RVA: 0x00045B1E File Offset: 0x00044B1E
			protected override void OnMouseLeave(EventArgs e)
			{
				base.OnMouseLeave(e);
				if (this.mouseIsOver || this.mouseIsDown)
				{
					this.mouseIsOver = false;
					this.mouseIsDown = false;
					base.Invalidate();
				}
			}

			// Token: 0x06000FB5 RID: 4021 RVA: 0x00045B4B File Offset: 0x00044B4B
			protected override void OnMouseDown(MouseEventArgs mevent)
			{
				base.OnMouseDown(mevent);
				if (mevent.Button == MouseButtons.Left)
				{
					this.mouseIsDown = true;
					base.Invalidate();
				}
			}

			// Token: 0x06000FB6 RID: 4022 RVA: 0x00045B70 File Offset: 0x00044B70
			protected override void OnMouseMove(MouseEventArgs mevent)
			{
				base.OnMouseMove(mevent);
				if (mevent.Button != MouseButtons.None)
				{
					if (!base.ClientRectangle.Contains(mevent.X, mevent.Y))
					{
						if (this.mouseIsDown)
						{
							this.mouseIsDown = false;
							base.Invalidate();
							return;
						}
					}
					else if (!this.mouseIsDown)
					{
						this.mouseIsDown = true;
						base.Invalidate();
					}
				}
			}

			// Token: 0x06000FB7 RID: 4023 RVA: 0x00045BD3 File Offset: 0x00044BD3
			protected override void OnMouseUp(MouseEventArgs mevent)
			{
				base.OnMouseUp(mevent);
				if (this.mouseIsDown)
				{
					this.mouseIsDown = false;
					base.Invalidate();
				}
			}

			// Token: 0x06000FB8 RID: 4024 RVA: 0x00045BF4 File Offset: 0x00044BF4
			protected override void OnPaint(PaintEventArgs pevent)
			{
				base.OnPaint(pevent);
				if (VisualStyleRenderer.IsSupported)
				{
					ComboBoxState state = ComboBoxState.Normal;
					if (!base.Enabled)
					{
						state = ComboBoxState.Disabled;
					}
					if (this.mouseIsDown && this.mouseIsOver)
					{
						state = ComboBoxState.Pressed;
					}
					else if (this.mouseIsOver)
					{
						state = ComboBoxState.Hot;
					}
					ComboBoxRenderer.DrawDropDownButton(pevent.Graphics, pevent.ClipRectangle, state);
				}
			}

			// Token: 0x06000FB9 RID: 4025 RVA: 0x00045C4C File Offset: 0x00044C4C
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg == 8 || msg == 31 || msg == 533)
				{
					this.mouseIsDown = false;
					base.Invalidate();
					base.WndProc(ref m);
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x04001017 RID: 4119
			private const int WM_KILLFOCUS = 8;

			// Token: 0x04001018 RID: 4120
			private const int WM_CANCELMODE = 31;

			// Token: 0x04001019 RID: 4121
			private const int WM_CAPTURECHANGED = 533;

			// Token: 0x0400101A RID: 4122
			private bool mouseIsDown;

			// Token: 0x0400101B RID: 4123
			private bool mouseIsOver;

			// Token: 0x0400101C RID: 4124
			private BindingFormattingWindowsFormsEditorService owner;
		}
	}
}
