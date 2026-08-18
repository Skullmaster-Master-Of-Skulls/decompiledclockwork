using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200030E RID: 782
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultEvent("ValueChanged")]
	[DefaultBindingProperty("Value")]
	[SRDescription("DescriptionNumericUpDown")]
	public class NumericUpDown : UpDownBase, ISupportInitialize
	{
		// Token: 0x060031BC RID: 12732 RVA: 0x000E067C File Offset: 0x000DE87C
		public NumericUpDown()
		{
			base.SetState2(2048, true);
			this.Text = "0";
			this.StopAcceleration();
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060031BD RID: 12733 RVA: 0x000E06D8 File Offset: 0x000DE8D8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public NumericUpDownAccelerationCollection Accelerations
		{
			get
			{
				if (this.accelerations == null)
				{
					this.accelerations = new NumericUpDownAccelerationCollection();
				}
				return this.accelerations;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x000E06F3 File Offset: 0x000DE8F3
		// (set) Token: 0x060031BF RID: 12735 RVA: 0x000E06FC File Offset: 0x000DE8FC
		[SRCategory("CatData")]
		[DefaultValue(0)]
		[SRDescription("NumericUpDownDecimalPlacesDescr")]
		public int DecimalPlaces
		{
			get
			{
				return this.decimalPlaces;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("DecimalPlaces", SR.GetString("InvalidBoundArgument", new object[]
					{
						"DecimalPlaces",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture),
						"99"
					}));
				}
				this.decimalPlaces = value;
				this.UpdateEditText();
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x000E076A File Offset: 0x000DE96A
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x000E0772 File Offset: 0x000DE972
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("NumericUpDownHexadecimalDescr")]
		public bool Hexadecimal
		{
			get
			{
				return this.hexadecimal;
			}
			set
			{
				this.hexadecimal = value;
				this.UpdateEditText();
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000E0781 File Offset: 0x000DE981
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x000E07AC File Offset: 0x000DE9AC
		[SRCategory("CatData")]
		[SRDescription("NumericUpDownIncrementDescr")]
		public decimal Increment
		{
			get
			{
				if (this.accelerationsCurrentIndex != -1)
				{
					return this.Accelerations[this.accelerationsCurrentIndex].Increment;
				}
				return this.increment;
			}
			set
			{
				if (value < 0m)
				{
					throw new ArgumentOutOfRangeException("Increment", SR.GetString("InvalidArgument", new object[]
					{
						"Increment",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.increment = value;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000E07FF File Offset: 0x000DE9FF
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000E0807 File Offset: 0x000DEA07
		[SRCategory("CatData")]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("NumericUpDownMaximumDescr")]
		public decimal Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				this.maximum = value;
				if (this.minimum > this.maximum)
				{
					this.minimum = this.maximum;
				}
				this.Value = this.Constrain(this.currentValue);
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000E0841 File Offset: 0x000DEA41
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x000E0849 File Offset: 0x000DEA49
		[SRCategory("CatData")]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("NumericUpDownMinimumDescr")]
		public decimal Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				this.minimum = value;
				if (this.minimum > this.maximum)
				{
					this.maximum = value;
				}
				this.Value = this.Constrain(this.currentValue);
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x060031C9 RID: 12745 RVA: 0x0001365E File Offset: 0x0001185E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		// Token: 0x14000249 RID: 585
		// (add) Token: 0x060031CA RID: 12746 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x060031CB RID: 12747 RVA: 0x00013670 File Offset: 0x00011870
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x000E087E File Offset: 0x000DEA7E
		private bool Spinning
		{
			get
			{
				return this.accelerations != null && this.buttonPressedStartTime != -1L;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000E0897 File Offset: 0x000DEA97
		// (set) Token: 0x060031CE RID: 12750 RVA: 0x000E089F File Offset: 0x000DEA9F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
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

		// Token: 0x1400024A RID: 586
		// (add) Token: 0x060031CF RID: 12751 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x060031D0 RID: 12752 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000E08A8 File Offset: 0x000DEAA8
		// (set) Token: 0x060031D2 RID: 12754 RVA: 0x000E08B0 File Offset: 0x000DEAB0
		[SRCategory("CatData")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("NumericUpDownThousandsSeparatorDescr")]
		public bool ThousandsSeparator
		{
			get
			{
				return this.thousandsSeparator;
			}
			set
			{
				this.thousandsSeparator = value;
				this.UpdateEditText();
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000E08BF File Offset: 0x000DEABF
		// (set) Token: 0x060031D4 RID: 12756 RVA: 0x000E08D8 File Offset: 0x000DEAD8
		[SRCategory("CatAppearance")]
		[Bindable(true)]
		[SRDescription("NumericUpDownValueDescr")]
		public decimal Value
		{
			get
			{
				if (base.UserEdit)
				{
					this.ValidateEditText();
				}
				return this.currentValue;
			}
			set
			{
				if (value != this.currentValue)
				{
					if (!this.initializing && (value < this.minimum || value > this.maximum))
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							value.ToString(CultureInfo.CurrentCulture),
							"'Minimum'",
							"'Maximum'"
						}));
					}
					this.currentValue = value;
					this.OnValueChanged(EventArgs.Empty);
					this.currentValueChanged = true;
					this.UpdateEditText();
				}
			}
		}

		// Token: 0x1400024B RID: 587
		// (add) Token: 0x060031D5 RID: 12757 RVA: 0x000E097B File Offset: 0x000DEB7B
		// (remove) Token: 0x060031D6 RID: 12758 RVA: 0x000E0994 File Offset: 0x000DEB94
		[SRCategory("CatAction")]
		[SRDescription("NumericUpDownOnValueChangedDescr")]
		public event EventHandler ValueChanged
		{
			add
			{
				this.onValueChanged = (EventHandler)Delegate.Combine(this.onValueChanged, value);
			}
			remove
			{
				this.onValueChanged = (EventHandler)Delegate.Remove(this.onValueChanged, value);
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000E09AD File Offset: 0x000DEBAD
		public void BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000E09B6 File Offset: 0x000DEBB6
		private decimal Constrain(decimal value)
		{
			if (value < this.minimum)
			{
				value = this.minimum;
			}
			if (value > this.maximum)
			{
				value = this.maximum;
			}
			return value;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000E09E5 File Offset: 0x000DEBE5
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new NumericUpDown.NumericUpDownAccessibleObject(this);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000E09F0 File Offset: 0x000DEBF0
		public override void DownButton()
		{
			this.SetNextAcceleration();
			if (base.UserEdit)
			{
				this.ParseEditText();
			}
			decimal num = this.currentValue;
			try
			{
				num -= this.Increment;
				if (num < this.minimum)
				{
					num = this.minimum;
					if (this.Spinning)
					{
						this.StopAcceleration();
					}
				}
			}
			catch (OverflowException)
			{
				num = this.minimum;
			}
			this.Value = num;
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000E0A6C File Offset: 0x000DEC6C
		public void EndInit()
		{
			this.initializing = false;
			this.Value = this.Constrain(this.currentValue);
			this.UpdateEditText();
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000E0A8D File Offset: 0x000DEC8D
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (base.InterceptArrowKeys && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) && !this.Spinning)
			{
				this.StartAcceleration();
			}
			base.OnKeyDown(e);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000E0AC0 File Offset: 0x000DECC0
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (base.InterceptArrowKeys && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
			{
				this.StopAcceleration();
			}
			base.OnKeyUp(e);
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000E0AEC File Offset: 0x000DECEC
		protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
		{
			base.OnTextBoxKeyPress(source, e);
			NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
			string numberDecimalSeparator = numberFormat.NumberDecimalSeparator;
			string numberGroupSeparator = numberFormat.NumberGroupSeparator;
			string negativeSign = numberFormat.NegativeSign;
			string text = e.KeyChar.ToString();
			if (!char.IsDigit(e.KeyChar) && !text.Equals(numberDecimalSeparator) && !text.Equals(numberGroupSeparator) && !text.Equals(negativeSign) && e.KeyChar != '\b' && (!this.Hexadecimal || ((e.KeyChar < 'a' || e.KeyChar > 'f') && (e.KeyChar < 'A' || e.KeyChar > 'F'))) && (Control.ModifierKeys & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				e.Handled = true;
				SafeNativeMethods.MessageBeep(0);
			}
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000E0BB1 File Offset: 0x000DEDB1
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (this.onValueChanged != null)
			{
				this.onValueChanged(this, e);
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000E0BC8 File Offset: 0x000DEDC8
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (base.UserEdit)
			{
				this.UpdateEditText();
			}
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000E0BDF File Offset: 0x000DEDDF
		internal override void OnStartTimer()
		{
			this.StartAcceleration();
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000E0BE7 File Offset: 0x000DEDE7
		internal override void OnStopTimer()
		{
			this.StopAcceleration();
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000E0BF0 File Offset: 0x000DEDF0
		protected void ParseEditText()
		{
			try
			{
				if (!string.IsNullOrEmpty(this.Text) && (this.Text.Length != 1 || !(this.Text == "-")))
				{
					if (this.Hexadecimal)
					{
						this.Value = this.Constrain(Convert.ToDecimal(Convert.ToInt32(this.Text, 16)));
					}
					else
					{
						this.Value = this.Constrain(decimal.Parse(this.Text, CultureInfo.CurrentCulture));
					}
				}
			}
			catch
			{
			}
			finally
			{
				base.UserEdit = false;
			}
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000E0C98 File Offset: 0x000DEE98
		private void SetNextAcceleration()
		{
			if (this.Spinning && this.accelerationsCurrentIndex < this.accelerations.Count - 1)
			{
				long ticks = DateTime.Now.Ticks;
				long num = ticks - this.buttonPressedStartTime;
				long num2 = 10000000L * (long)this.accelerations[this.accelerationsCurrentIndex + 1].Seconds;
				if (num > num2)
				{
					this.buttonPressedStartTime = ticks;
					this.accelerationsCurrentIndex++;
				}
			}
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000E0D13 File Offset: 0x000DEF13
		private void ResetIncrement()
		{
			this.Increment = NumericUpDown.DefaultIncrement;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000E0D20 File Offset: 0x000DEF20
		private void ResetMaximum()
		{
			this.Maximum = NumericUpDown.DefaultMaximum;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000E0D2D File Offset: 0x000DEF2D
		private void ResetMinimum()
		{
			this.Minimum = NumericUpDown.DefaultMinimum;
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000E0D3A File Offset: 0x000DEF3A
		private void ResetValue()
		{
			this.Value = NumericUpDown.DefaultValue;
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000E0D48 File Offset: 0x000DEF48
		private bool ShouldSerializeIncrement()
		{
			return !this.Increment.Equals(NumericUpDown.DefaultIncrement);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000E0D6C File Offset: 0x000DEF6C
		private bool ShouldSerializeMaximum()
		{
			return !this.Maximum.Equals(NumericUpDown.DefaultMaximum);
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000E0D90 File Offset: 0x000DEF90
		private bool ShouldSerializeMinimum()
		{
			return !this.Minimum.Equals(NumericUpDown.DefaultMinimum);
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000E0DB4 File Offset: 0x000DEFB4
		private bool ShouldSerializeValue()
		{
			return !this.Value.Equals(NumericUpDown.DefaultValue);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000E0DD8 File Offset: 0x000DEFD8
		private void StartAcceleration()
		{
			this.buttonPressedStartTime = DateTime.Now.Ticks;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000E0DF8 File Offset: 0x000DEFF8
		private void StopAcceleration()
		{
			this.accelerationsCurrentIndex = -1;
			this.buttonPressedStartTime = -1L;
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000E0E0C File Offset: 0x000DF00C
		public override string ToString()
		{
			string text = base.ToString();
			return string.Concat(new string[]
			{
				text,
				", Minimum = ",
				this.Minimum.ToString(CultureInfo.CurrentCulture),
				", Maximum = ",
				this.Maximum.ToString(CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000E0E70 File Offset: 0x000DF070
		public override void UpButton()
		{
			this.SetNextAcceleration();
			if (base.UserEdit)
			{
				this.ParseEditText();
			}
			decimal num = this.currentValue;
			try
			{
				num += this.Increment;
				if (num > this.maximum)
				{
					num = this.maximum;
					if (this.Spinning)
					{
						this.StopAcceleration();
					}
				}
			}
			catch (OverflowException)
			{
				num = this.maximum;
			}
			this.Value = num;
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000E0EEC File Offset: 0x000DF0EC
		private string GetNumberText(decimal num)
		{
			string result;
			if (this.Hexadecimal)
			{
				result = ((long)num).ToString("X", CultureInfo.InvariantCulture);
			}
			else
			{
				result = num.ToString((this.ThousandsSeparator ? "N" : "F") + this.DecimalPlaces.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
			}
			return result;
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000E0F58 File Offset: 0x000DF158
		protected override void UpdateEditText()
		{
			if (this.initializing)
			{
				return;
			}
			if (base.UserEdit)
			{
				this.ParseEditText();
			}
			if (this.currentValueChanged || (!string.IsNullOrEmpty(this.Text) && (this.Text.Length != 1 || !(this.Text == "-"))))
			{
				this.currentValueChanged = false;
				base.ChangingText = true;
				this.Text = this.GetNumberText(this.currentValue);
			}
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000E0FD1 File Offset: 0x000DF1D1
		protected override void ValidateEditText()
		{
			this.ParseEditText();
			this.UpdateEditText();
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000E0FE0 File Offset: 0x000DF1E0
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			int preferredHeight = base.PreferredHeight;
			int num = this.Hexadecimal ? 16 : 10;
			int largestDigit = this.GetLargestDigit(0, num);
			int num2 = (int)Math.Floor(Math.Log(Math.Max(-(double)this.Minimum, (double)this.Maximum), (double)num));
			int num3;
			if (this.Hexadecimal)
			{
				num3 = (int)Math.Floor(Math.Log(9.223372036854776E+18, (double)num));
			}
			else
			{
				num3 = (int)Math.Floor(Math.Log(7.922816251426434E+28, (double)num));
			}
			bool flag = num2 >= num3;
			decimal num4;
			if (largestDigit != 0 || num2 == 1)
			{
				num4 = largestDigit;
			}
			else
			{
				num4 = this.GetLargestDigit(1, num);
			}
			if (flag)
			{
				num2 = num3 - 1;
			}
			for (int i = 0; i < num2; i++)
			{
				num4 = num4 * num + largestDigit;
			}
			int num5 = TextRenderer.MeasureText(this.GetNumberText(num4), this.Font).Width;
			if (flag)
			{
				string text;
				if (this.Hexadecimal)
				{
					text = ((long)num4).ToString("X", CultureInfo.InvariantCulture);
				}
				else
				{
					text = num4.ToString(CultureInfo.CurrentCulture);
				}
				int width = TextRenderer.MeasureText(text, this.Font).Width;
				num5 += width / (num2 + 1);
			}
			int width2 = base.SizeFromClientSize(num5, preferredHeight).Width + this.upDownButtons.Width;
			return new Size(width2, preferredHeight) + this.Padding.Size;
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000E1184 File Offset: 0x000DF384
		private int GetLargestDigit(int start, int end)
		{
			int result = -1;
			int num = -1;
			for (int i = start; i < end; i++)
			{
				char c;
				if (i < 10)
				{
					c = i.ToString(CultureInfo.InvariantCulture)[0];
				}
				else
				{
					c = (char)(65 + (i - 10));
				}
				Size size = TextRenderer.MeasureText(c.ToString(), this.Font);
				if (size.Width >= num)
				{
					num = size.Width;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x04001E4C RID: 7756
		private static readonly decimal DefaultValue = 0m;

		// Token: 0x04001E4D RID: 7757
		private static readonly decimal DefaultMinimum = 0m;

		// Token: 0x04001E4E RID: 7758
		private static readonly decimal DefaultMaximum = 100m;

		// Token: 0x04001E4F RID: 7759
		private const int DefaultDecimalPlaces = 0;

		// Token: 0x04001E50 RID: 7760
		private static readonly decimal DefaultIncrement = 1m;

		// Token: 0x04001E51 RID: 7761
		private const bool DefaultThousandsSeparator = false;

		// Token: 0x04001E52 RID: 7762
		private const bool DefaultHexadecimal = false;

		// Token: 0x04001E53 RID: 7763
		private const int InvalidValue = -1;

		// Token: 0x04001E54 RID: 7764
		private int decimalPlaces;

		// Token: 0x04001E55 RID: 7765
		private decimal increment = NumericUpDown.DefaultIncrement;

		// Token: 0x04001E56 RID: 7766
		private bool thousandsSeparator;

		// Token: 0x04001E57 RID: 7767
		private decimal minimum = NumericUpDown.DefaultMinimum;

		// Token: 0x04001E58 RID: 7768
		private decimal maximum = NumericUpDown.DefaultMaximum;

		// Token: 0x04001E59 RID: 7769
		private bool hexadecimal;

		// Token: 0x04001E5A RID: 7770
		private decimal currentValue = NumericUpDown.DefaultValue;

		// Token: 0x04001E5B RID: 7771
		private bool currentValueChanged;

		// Token: 0x04001E5C RID: 7772
		private EventHandler onValueChanged;

		// Token: 0x04001E5D RID: 7773
		private bool initializing;

		// Token: 0x04001E5E RID: 7774
		private NumericUpDownAccelerationCollection accelerations;

		// Token: 0x04001E5F RID: 7775
		private int accelerationsCurrentIndex;

		// Token: 0x04001E60 RID: 7776
		private long buttonPressedStartTime;

		// Token: 0x020007CA RID: 1994
		[ComVisible(true)]
		internal class NumericUpDownAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006D7D RID: 28029 RVA: 0x0009B963 File Offset: 0x00099B63
			public NumericUpDownAccessibleObject(NumericUpDown owner) : base(owner)
			{
			}

			// Token: 0x170017E7 RID: 6119
			// (get) Token: 0x06006D7E RID: 28030 RVA: 0x001926C8 File Offset: 0x001908C8
			// (set) Token: 0x06006D7F RID: 28031 RVA: 0x0001106B File Offset: 0x0000F26B
			public override string Name
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return null;
					}
					string name = base.Name;
					return ((NumericUpDown)base.Owner).GetAccessibleName(name);
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x170017E8 RID: 6120
			// (get) Token: 0x06006D80 RID: 28032 RVA: 0x001926F8 File Offset: 0x001908F8
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.SpinButton;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					if (AccessibilityImprovements.Level1)
					{
						return AccessibleRole.SpinButton;
					}
					return AccessibleRole.ComboBox;
				}
			}

			// Token: 0x06006D81 RID: 28033 RVA: 0x00192730 File Offset: 0x00190930
			public override AccessibleObject GetChild(int index)
			{
				if (!base.IsOwnerControlDestroyed() && index >= 0 && index < this.GetChildCount())
				{
					if (index == 0)
					{
						return ((UpDownBase)base.Owner).TextBox.AccessibilityObject.Parent;
					}
					if (index == 1)
					{
						return ((UpDownBase)base.Owner).UpDownButtonsInternal.AccessibilityObject.Parent;
					}
				}
				return null;
			}

			// Token: 0x06006D82 RID: 28034 RVA: 0x0001627D File Offset: 0x0001447D
			public override int GetChildCount()
			{
				return 2;
			}
		}
	}
}
