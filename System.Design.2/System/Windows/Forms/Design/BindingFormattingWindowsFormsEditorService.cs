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
	// Token: 0x0200029B RID: 667
	internal class BindingFormattingWindowsFormsEditorService : Panel, IWindowsFormsEditorService, IServiceProvider, ITypeDescriptorContext
	{
		// Token: 0x060019AF RID: 6575 RVA: 0x00092E18 File Offset: 0x00091018
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

		// Token: 0x060019B0 RID: 6576 RVA: 0x00092F39 File Offset: 0x00091139
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new BindingFormattingWindowsFormsEditorService.BindingFormattingWindowFormsEditorAccessibleObject(this);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00092F44 File Offset: 0x00091144
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

		// Token: 0x060019B2 RID: 6578 RVA: 0x00092F98 File Offset: 0x00091198
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

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00093004 File Offset: 0x00091204
		private int PreferredHeight
		{
			get
			{
				return TextRenderer.MeasureText("j^", this.Font, new Size(32767, (int)((double)base.FontHeight * 1.25))).Height + SystemInformation.BorderSize.Height * 8 + base.Padding.Size.Height;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (set) Token: 0x060019B4 RID: 6580 RVA: 0x0009306C File Offset: 0x0009126C
		public ITypeDescriptorContext Context
		{
			set
			{
				this.context = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00093078 File Offset: 0x00091278
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

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x000930A6 File Offset: 0x000912A6
		object ITypeDescriptorContext.Instance
		{
			get
			{
				return this.ownerComponent;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x00003598 File Offset: 0x00001798
		PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000930AE File Offset: 0x000912AE
		void ITypeDescriptorContext.OnComponentChanged()
		{
			if (this.context != null)
			{
				this.context.OnComponentChanged();
			}
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000930C3 File Offset: 0x000912C3
		bool ITypeDescriptorContext.OnComponentChanging()
		{
			return this.context == null || this.context.OnComponentChanging();
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000930DA File Offset: 0x000912DA
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

		// Token: 0x060019BB RID: 6587 RVA: 0x00093106 File Offset: 0x00091306
		void IWindowsFormsEditorService.CloseDropDown()
		{
			this.dropDownHolder.SetComponent(null);
			this.dropDownHolder.Visible = false;
			this.button.Focus();
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0009312C File Offset: 0x0009132C
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

		// Token: 0x060019BD RID: 6589 RVA: 0x00093200 File Offset: 0x00091400
		DialogResult IWindowsFormsEditorService.ShowDialog(Form form)
		{
			return form.ShowDialog();
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00093208 File Offset: 0x00091408
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x00093210 File Offset: 0x00091410
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

		// Token: 0x170005B5 RID: 1461
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x0009325F File Offset: 0x0009145F
		public DataSourceUpdateMode DefaultDataSourceUpdateMode
		{
			set
			{
				this.defaultDataSourceUpdateMode = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (set) Token: 0x060019C1 RID: 6593 RVA: 0x00093268 File Offset: 0x00091468
		public IComponent OwnerComponent
		{
			set
			{
				this.ownerComponent = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x00093271 File Offset: 0x00091471
		public string PropertyName
		{
			set
			{
				this.propertyName = value;
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060019C3 RID: 6595 RVA: 0x0009327A File Offset: 0x0009147A
		// (remove) Token: 0x060019C4 RID: 6596 RVA: 0x00093293 File Offset: 0x00091493
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

		// Token: 0x060019C5 RID: 6597 RVA: 0x000932AC File Offset: 0x000914AC
		private void button_Click(object sender, EventArgs e)
		{
			this.DropDownPicker();
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000932B4 File Offset: 0x000914B4
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

		// Token: 0x060019C7 RID: 6599 RVA: 0x000933A0 File Offset: 0x000915A0
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

		// Token: 0x060019C8 RID: 6600 RVA: 0x00093517 File Offset: 0x00091717
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			base.Select();
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00093526 File Offset: 0x00091726
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00093535 File Offset: 0x00091735
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			base.Invalidate();
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00093544 File Offset: 0x00091744
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
					textFormatFlags |= TextFormatFlags.Default;
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
					textFormatFlags2 |= TextFormatFlags.Default;
				}
				else
				{
					textFormatFlags2 |= TextFormatFlags.Right;
				}
				TextRenderer.DrawText(p.Graphics, text, this.Font, bounds, this.ForeColor, textFormatFlags2);
				stringFormat.Dispose();
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000937A9 File Offset: 0x000919A9
		protected void OnPropertyValueChanged(EventArgs e)
		{
			if (this.propertyValueChanged != null)
			{
				this.propertyValueChanged(this, e);
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000937C0 File Offset: 0x000919C0
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

		// Token: 0x040015B5 RID: 5557
		private ITypeDescriptorContext context;

		// Token: 0x040015B6 RID: 5558
		private DropDownHolder dropDownHolder;

		// Token: 0x040015B7 RID: 5559
		private BindingFormattingWindowsFormsEditorService.DropDownButton button;

		// Token: 0x040015B8 RID: 5560
		private EventHandler propertyValueChanged;

		// Token: 0x040015B9 RID: 5561
		private Binding binding;

		// Token: 0x040015BA RID: 5562
		private IComponent ownerComponent;

		// Token: 0x040015BB RID: 5563
		private DataSourceUpdateMode defaultDataSourceUpdateMode;

		// Token: 0x040015BC RID: 5564
		private DesignBindingPicker designBindingPicker;

		// Token: 0x040015BD RID: 5565
		private string propertyName = string.Empty;

		// Token: 0x0200052E RID: 1326
		private class BindingFormattingWindowFormsEditorAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x0600304F RID: 12367 RVA: 0x001096B6 File Offset: 0x001078B6
			public BindingFormattingWindowFormsEditorAccessibleObject(BindingFormattingWindowsFormsEditorService owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x17000961 RID: 2401
			// (get) Token: 0x06003050 RID: 12368 RVA: 0x001096C6 File Offset: 0x001078C6
			public override string Name
			{
				get
				{
					return SR.GetString("BindingFormattingDialogBindingPickerAccName");
				}
			}

			// Token: 0x17000962 RID: 2402
			// (get) Token: 0x06003051 RID: 12369 RVA: 0x001096D2 File Offset: 0x001078D2
			public override string Value
			{
				get
				{
					return this.owner.Text;
				}
			}

			// Token: 0x06003052 RID: 12370 RVA: 0x001096DF File Offset: 0x001078DF
			public override void DoDefaultAction()
			{
				this.owner.DropDownPicker();
			}

			// Token: 0x040020D7 RID: 8407
			private BindingFormattingWindowsFormsEditorService owner;
		}

		// Token: 0x0200052F RID: 1327
		private class DropDownButton : Button
		{
			// Token: 0x06003053 RID: 12371 RVA: 0x001096EC File Offset: 0x001078EC
			public DropDownButton(BindingFormattingWindowsFormsEditorService owner)
			{
				this.owner = owner;
				base.TabStop = false;
			}

			// Token: 0x17000963 RID: 2403
			// (get) Token: 0x06003054 RID: 12372 RVA: 0x00109702 File Offset: 0x00107902
			protected override Size DefaultSize
			{
				get
				{
					return new Size(17, 19);
				}
			}

			// Token: 0x06003055 RID: 12373 RVA: 0x00109710 File Offset: 0x00107910
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

			// Token: 0x06003056 RID: 12374 RVA: 0x00109775 File Offset: 0x00107975
			protected override void OnEnabledChanged(EventArgs e)
			{
				base.OnEnabledChanged(e);
				if (!base.Enabled)
				{
					this.mouseIsDown = false;
					this.mouseIsOver = false;
				}
			}

			// Token: 0x06003057 RID: 12375 RVA: 0x00109794 File Offset: 0x00107994
			protected override void OnKeyDown(KeyEventArgs kevent)
			{
				base.OnKeyDown(kevent);
				if (kevent.KeyData == Keys.Space)
				{
					this.mouseIsDown = true;
					base.Invalidate();
				}
			}

			// Token: 0x06003058 RID: 12376 RVA: 0x001097B4 File Offset: 0x001079B4
			protected override void OnKeyUp(KeyEventArgs kevent)
			{
				base.OnKeyUp(kevent);
				if (this.mouseIsDown)
				{
					this.mouseIsDown = false;
					base.Invalidate();
				}
			}

			// Token: 0x06003059 RID: 12377 RVA: 0x001097D2 File Offset: 0x001079D2
			protected override void OnLostFocus(EventArgs e)
			{
				base.OnLostFocus(e);
				this.mouseIsDown = false;
				base.Invalidate();
			}

			// Token: 0x0600305A RID: 12378 RVA: 0x001097E8 File Offset: 0x001079E8
			protected override void OnMouseEnter(EventArgs e)
			{
				base.OnMouseEnter(e);
				if (!this.mouseIsOver)
				{
					this.mouseIsOver = true;
					base.Invalidate();
				}
			}

			// Token: 0x0600305B RID: 12379 RVA: 0x00109806 File Offset: 0x00107A06
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

			// Token: 0x0600305C RID: 12380 RVA: 0x00109833 File Offset: 0x00107A33
			protected override void OnMouseDown(MouseEventArgs mevent)
			{
				base.OnMouseDown(mevent);
				if (mevent.Button == MouseButtons.Left)
				{
					this.mouseIsDown = true;
					base.Invalidate();
				}
			}

			// Token: 0x0600305D RID: 12381 RVA: 0x00109858 File Offset: 0x00107A58
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

			// Token: 0x0600305E RID: 12382 RVA: 0x001098BB File Offset: 0x00107ABB
			protected override void OnMouseUp(MouseEventArgs mevent)
			{
				base.OnMouseUp(mevent);
				if (this.mouseIsDown)
				{
					this.mouseIsDown = false;
					base.Invalidate();
				}
			}

			// Token: 0x0600305F RID: 12383 RVA: 0x001098DC File Offset: 0x00107ADC
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

			// Token: 0x06003060 RID: 12384 RVA: 0x00109934 File Offset: 0x00107B34
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

			// Token: 0x040020D8 RID: 8408
			private bool mouseIsDown;

			// Token: 0x040020D9 RID: 8409
			private bool mouseIsOver;

			// Token: 0x040020DA RID: 8410
			private BindingFormattingWindowsFormsEditorService owner;

			// Token: 0x040020DB RID: 8411
			private const int WM_KILLFOCUS = 8;

			// Token: 0x040020DC RID: 8412
			private const int WM_CANCELMODE = 31;

			// Token: 0x040020DD RID: 8413
			private const int WM_CAPTURECHANGED = 533;
		}
	}
}
