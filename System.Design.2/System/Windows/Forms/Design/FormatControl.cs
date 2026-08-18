using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002E5 RID: 741
	internal class FormatControl : UserControl
	{
		// Token: 0x06001DA1 RID: 7585 RVA: 0x000B3883 File Offset: 0x000B1A83
		public FormatControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x000B389C File Offset: 0x000B1A9C
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x000B38A4 File Offset: 0x000B1AA4
		public bool Dirty
		{
			get
			{
				return this.dirty;
			}
			set
			{
				this.dirty = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x000B38B0 File Offset: 0x000B1AB0
		// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x000B38E0 File Offset: 0x000B1AE0
		public string FormatType
		{
			get
			{
				FormatControl.FormatTypeClass formatTypeClass = this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass;
				if (formatTypeClass != null)
				{
					return formatTypeClass.ToString();
				}
				return string.Empty;
			}
			set
			{
				this.formatTypeListBox.SelectedIndex = 0;
				for (int i = 0; i < this.formatTypeListBox.Items.Count; i++)
				{
					FormatControl.FormatTypeClass formatTypeClass = this.formatTypeListBox.Items[i] as FormatControl.FormatTypeClass;
					if (formatTypeClass.ToString().Equals(value))
					{
						this.formatTypeListBox.SelectedIndex = i;
					}
				}
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x000B3945 File Offset: 0x000B1B45
		public FormatControl.FormatTypeClass FormatTypeItem
		{
			get
			{
				return this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x000B3958 File Offset: 0x000B1B58
		// (set) Token: 0x06001DA8 RID: 7592 RVA: 0x000B3981 File Offset: 0x000B1B81
		public string NullValue
		{
			get
			{
				string text = this.nullValueTextBox.Text.Trim();
				if (text.Length != 0)
				{
					return text;
				}
				return null;
			}
			set
			{
				this.nullValueTextBox.TextChanged -= this.nullValueTextBox_TextChanged;
				this.nullValueTextBox.Text = value;
				this.nullValueTextBox.TextChanged += this.nullValueTextBox_TextChanged;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x000B39BD File Offset: 0x000B1BBD
		public bool NullValueTextBoxEnabled
		{
			set
			{
				this.nullValueTextBox.Enabled = value;
			}
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x000B39CC File Offset: 0x000B1BCC
		private void customStringTextBox_TextChanged(object sender, EventArgs e)
		{
			FormatControl.CustomFormatType customFormatType = this.formatTypeListBox.SelectedItem as FormatControl.CustomFormatType;
			this.sampleLabel.Text = customFormatType.SampleString;
			this.dirty = true;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x000B3A04 File Offset: 0x000B1C04
		private void dateTimeFormatsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			FormatControl.FormatTypeClass formatTypeClass = this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass;
			this.sampleLabel.Text = formatTypeClass.SampleString;
			this.dirty = true;
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000B3A3C File Offset: 0x000B1C3C
		private void decimalPlacesUpDown_ValueChanged(object sender, EventArgs e)
		{
			FormatControl.FormatTypeClass formatTypeClass = this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass;
			this.sampleLabel.Text = formatTypeClass.SampleString;
			this.dirty = true;
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00003937 File Offset: 0x00001B37
		private void formatGroupBox_Enter(object sender, EventArgs e)
		{
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000B3A74 File Offset: 0x000B1C74
		private void formatTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			FormatControl.FormatTypeClass formatTypeClass = this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass;
			this.UpdateControlVisibility(formatTypeClass);
			this.sampleLabel.Text = formatTypeClass.SampleString;
			this.explanationLabel.Text = formatTypeClass.TopLabelString;
			this.dirty = true;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x000B3AC4 File Offset: 0x000B1CC4
		public static string FormatTypeStringFromFormatString(string formatString)
		{
			if (string.IsNullOrEmpty(formatString))
			{
				return SR.GetString("BindingFormattingDialogFormatTypeNoFormatting");
			}
			if (FormatControl.NumericFormatType.ParseStatic(formatString))
			{
				return SR.GetString("BindingFormattingDialogFormatTypeNumeric");
			}
			if (FormatControl.CurrencyFormatType.ParseStatic(formatString))
			{
				return SR.GetString("BindingFormattingDialogFormatTypeCurrency");
			}
			if (FormatControl.DateTimeFormatType.ParseStatic(formatString))
			{
				return SR.GetString("BindingFormattingDialogFormatTypeDateTime");
			}
			if (FormatControl.ScientificFormatType.ParseStatic(formatString))
			{
				return SR.GetString("BindingFormattingDialogFormatTypeScientific");
			}
			return SR.GetString("BindingFormattingDialogFormatTypeCustom");
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x000B3B3C File Offset: 0x000B1D3C
		protected override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.formatTypeLabel.Text))
			{
				this.formatTypeListBox.Focus();
				return true;
			}
			if (Control.IsMnemonic(charCode, this.nullValueLabel.Text))
			{
				this.nullValueTextBox.Focus();
				return true;
			}
			switch (this.formatTypeListBox.SelectedIndex)
			{
			case 0:
				return false;
			case 1:
			case 2:
			case 4:
				if (Control.IsMnemonic(charCode, this.secondRowLabel.Text))
				{
					this.decimalPlacesUpDown.Focus();
					return true;
				}
				return false;
			case 3:
				if (Control.IsMnemonic(charCode, this.secondRowLabel.Text))
				{
					this.dateTimeFormatsListBox.Focus();
					return true;
				}
				return false;
			case 5:
				if (Control.IsMnemonic(charCode, this.secondRowLabel.Text))
				{
					this.customStringTextBox.Focus();
					return true;
				}
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x000B3C24 File Offset: 0x000B1E24
		public void ResetFormattingInfo()
		{
			this.decimalPlacesUpDown.ValueChanged -= this.decimalPlacesUpDown_ValueChanged;
			this.customStringTextBox.TextChanged -= this.customStringTextBox_TextChanged;
			this.dateTimeFormatsListBox.SelectedIndexChanged -= this.dateTimeFormatsListBox_SelectedIndexChanged;
			this.formatTypeListBox.SelectedIndexChanged -= this.formatTypeListBox_SelectedIndexChanged;
			this.decimalPlacesUpDown.Value = 2m;
			this.nullValueTextBox.Text = string.Empty;
			this.dateTimeFormatsListBox.SelectedIndex = -1;
			this.formatTypeListBox.SelectedIndex = -1;
			this.customStringTextBox.Text = string.Empty;
			this.decimalPlacesUpDown.ValueChanged += this.decimalPlacesUpDown_ValueChanged;
			this.customStringTextBox.TextChanged += this.customStringTextBox_TextChanged;
			this.dateTimeFormatsListBox.SelectedIndexChanged += this.dateTimeFormatsListBox_SelectedIndexChanged;
			this.formatTypeListBox.SelectedIndexChanged += this.formatTypeListBox_SelectedIndexChanged;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x000B3D34 File Offset: 0x000B1F34
		private void UpdateControlVisibility(FormatControl.FormatTypeClass formatType)
		{
			if (formatType == null)
			{
				this.explanationLabel.Visible = false;
				this.sampleLabel.Visible = false;
				this.nullValueLabel.Visible = false;
				this.secondRowLabel.Visible = false;
				this.nullValueTextBox.Visible = false;
				this.thirdRowLabel.Visible = false;
				this.dateTimeFormatsListBox.Visible = false;
				this.customStringTextBox.Visible = false;
				this.decimalPlacesUpDown.Visible = false;
				return;
			}
			this.tableLayoutPanel1.SuspendLayout();
			this.secondRowLabel.Text = "";
			if (formatType.DropDownVisible)
			{
				this.secondRowLabel.Text = SR.GetString("BindingFormattingDialogDecimalPlaces");
				this.decimalPlacesUpDown.Visible = true;
			}
			else
			{
				this.decimalPlacesUpDown.Visible = false;
			}
			if (formatType.FormatStringTextBoxVisible)
			{
				this.secondRowLabel.Text = SR.GetString("BindingFormattingDialogCustomFormat");
				this.thirdRowLabel.Visible = true;
				this.tableLayoutPanel1.SetColumn(this.thirdRowLabel, 0);
				this.tableLayoutPanel1.SetColumnSpan(this.thirdRowLabel, 2);
				this.customStringTextBox.Visible = true;
				if (this.tableLayoutPanel1.Controls.Contains(this.dateTimeFormatsListBox))
				{
					this.tableLayoutPanel1.Controls.Remove(this.dateTimeFormatsListBox);
				}
				this.tableLayoutPanel1.Controls.Add(this.customStringTextBox, 1, 1);
			}
			else
			{
				this.thirdRowLabel.Visible = false;
				this.customStringTextBox.Visible = false;
			}
			if (formatType.ListBoxVisible)
			{
				this.secondRowLabel.Text = SR.GetString("BindingFormattingDialogType");
				if (this.tableLayoutPanel1.Controls.Contains(this.customStringTextBox))
				{
					this.tableLayoutPanel1.Controls.Remove(this.customStringTextBox);
				}
				this.dateTimeFormatsListBox.Visible = true;
				this.tableLayoutPanel1.Controls.Add(this.dateTimeFormatsListBox, 0, 2);
				this.tableLayoutPanel1.SetColumn(this.dateTimeFormatsListBox, 0);
				this.tableLayoutPanel1.SetColumnSpan(this.dateTimeFormatsListBox, 2);
			}
			else
			{
				this.dateTimeFormatsListBox.Visible = false;
			}
			this.tableLayoutPanel1.ResumeLayout(true);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x000B3F70 File Offset: 0x000B2170
		private void UpdateCustomStringTextBox()
		{
			this.customStringTextBox = new TextBox();
			this.customStringTextBox.AccessibleDescription = SR.GetString("BindingFormattingDialogCustomFormatAccessibleDescription");
			this.customStringTextBox.Margin = new Padding(0, 3, 0, 3);
			this.customStringTextBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.customStringTextBox.TabIndex = 3;
			this.customStringTextBox.TextChanged += this.customStringTextBox_TextChanged;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000B3FE1 File Offset: 0x000B21E1
		private void UpdateFormatTypeListBoxHeight()
		{
			this.formatTypeListBox.Height = this.tableLayoutPanel1.Bottom - this.formatTypeListBox.Top;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000B4008 File Offset: 0x000B2208
		private void UpdateFormatTypeListBoxItems()
		{
			this.dateTimeFormatsListBox.SelectedIndexChanged -= this.dateTimeFormatsListBox_SelectedIndexChanged;
			this.dateTimeFormatsListBox.Items.Clear();
			this.dateTimeFormatsListBox.Items.Add(new FormatControl.DateTimeFormatsListBoxItem(FormatControl.dateTimeFormatValue, "d"));
			this.dateTimeFormatsListBox.Items.Add(new FormatControl.DateTimeFormatsListBoxItem(FormatControl.dateTimeFormatValue, "D"));
			this.dateTimeFormatsListBox.Items.Add(new FormatControl.DateTimeFormatsListBoxItem(FormatControl.dateTimeFormatValue, "f"));
			this.dateTimeFormatsListBox.Items.Add(new FormatControl.DateTimeFormatsListBoxItem(FormatControl.dateTimeFormatValue, "F"));
			this.dateTimeFormatsListBox.Items.Add(new FormatControl.DateTimeFormatsListBoxItem(FormatControl.dateTimeFormatValue, "g"));
			this.dateTimeFormatsListBox.Items.Add(new FormatControl.DateTimeFormatsListBoxItem(FormatControl.dateTimeFormatValue, "G"));
			this.dateTimeFormatsListBox.Items.Add(new FormatControl.DateTimeFormatsListBoxItem(FormatControl.dateTimeFormatValue, "t"));
			this.dateTimeFormatsListBox.Items.Add(new FormatControl.DateTimeFormatsListBoxItem(FormatControl.dateTimeFormatValue, "T"));
			this.dateTimeFormatsListBox.Items.Add(new FormatControl.DateTimeFormatsListBoxItem(FormatControl.dateTimeFormatValue, "M"));
			this.dateTimeFormatsListBox.SelectedIndex = 0;
			this.dateTimeFormatsListBox.SelectedIndexChanged += this.dateTimeFormatsListBox_SelectedIndexChanged;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x000B4180 File Offset: 0x000B2380
		private void UpdateTBLHeight()
		{
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel1.Controls.Add(this.customStringTextBox, 1, 1);
			this.customStringTextBox.Visible = false;
			this.thirdRowLabel.MaximumSize = new Size(this.tableLayoutPanel1.Width, 0);
			this.dateTimeFormatsListBox.Visible = false;
			this.tableLayoutPanel1.SetColumn(this.thirdRowLabel, 0);
			this.tableLayoutPanel1.SetColumnSpan(this.thirdRowLabel, 2);
			this.thirdRowLabel.AutoSize = true;
			this.tableLayoutPanel1.ResumeLayout(true);
			this.tableLayoutPanel1.MinimumSize = new Size(this.tableLayoutPanel1.Width, this.tableLayoutPanel1.Height);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000B4248 File Offset: 0x000B2448
		private void FormatControl_Load(object sender, EventArgs e)
		{
			if (this.loaded)
			{
				return;
			}
			this.nullValueLabel.Text = SR.GetString("BindingFormattingDialogNullValue");
			int num = this.nullValueLabel.Width;
			int num2 = this.nullValueLabel.Height;
			this.secondRowLabel.Text = SR.GetString("BindingFormattingDialogDecimalPlaces");
			num = Math.Max(num, this.secondRowLabel.Width);
			num2 = Math.Max(num2, this.secondRowLabel.Height);
			this.secondRowLabel.Text = SR.GetString("BindingFormattingDialogCustomFormat");
			num = Math.Max(num, this.secondRowLabel.Width);
			num2 = Math.Max(num2, this.secondRowLabel.Height);
			this.nullValueLabel.MinimumSize = new Size(num, num2);
			this.secondRowLabel.MinimumSize = new Size(num, num2);
			this.formatTypeListBox.SelectedIndexChanged -= this.formatTypeListBox_SelectedIndexChanged;
			this.formatTypeListBox.Items.Clear();
			this.formatTypeListBox.Items.Add(new FormatControl.NoFormattingFormatType());
			this.formatTypeListBox.Items.Add(new FormatControl.NumericFormatType(this));
			this.formatTypeListBox.Items.Add(new FormatControl.CurrencyFormatType(this));
			this.formatTypeListBox.Items.Add(new FormatControl.DateTimeFormatType(this));
			this.formatTypeListBox.Items.Add(new FormatControl.ScientificFormatType(this));
			this.formatTypeListBox.Items.Add(new FormatControl.CustomFormatType(this));
			this.formatTypeListBox.SelectedIndex = 0;
			this.formatTypeListBox.SelectedIndexChanged += this.formatTypeListBox_SelectedIndexChanged;
			this.UpdateCustomStringTextBox();
			this.UpdateTBLHeight();
			this.UpdateFormatTypeListBoxHeight();
			this.UpdateFormatTypeListBoxItems();
			this.UpdateControlVisibility(this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass);
			this.sampleLabel.Text = (this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass).SampleString;
			this.explanationLabel.Size = new Size(this.formatGroupBox.Width - 10, 30);
			this.explanationLabel.Text = (this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass).TopLabelString;
			this.dirty = false;
			this.FormatControlFinishedLoading();
			this.loaded = true;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000B4498 File Offset: 0x000B2698
		private void FormatControlFinishedLoading()
		{
			FormatStringDialog formatStringDialog = null;
			for (Control parent = base.Parent; parent != null; parent = parent.Parent)
			{
				BindingFormattingDialog bindingFormattingDialog = parent as BindingFormattingDialog;
				formatStringDialog = (parent as FormatStringDialog);
				if (bindingFormattingDialog != null || formatStringDialog != null)
				{
					break;
				}
			}
			if (formatStringDialog != null)
			{
				formatStringDialog.FormatControlFinishedLoading();
			}
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000B44D9 File Offset: 0x000B26D9
		private void nullValueTextBox_TextChanged(object sender, EventArgs e)
		{
			this.dirty = true;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000B44E2 File Offset: 0x000B26E2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000B4504 File Offset: 0x000B2704
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormatControl));
			this.formatGroupBox = new GroupBox();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.explanationLabel = new Label();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.sampleGroupBox = new GroupBox();
			this.sampleLabel = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.secondRowLabel = new Label();
			this.nullValueLabel = new Label();
			this.nullValueTextBox = new TextBox();
			this.decimalPlacesUpDown = new NumericUpDown();
			this.thirdRowLabel = new Label();
			this.dateTimeFormatsListBox = new ListBox();
			this.formatTypeLabel = new Label();
			this.formatTypeListBox = new ListBox();
			this.formatGroupBox.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.sampleGroupBox.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.decimalPlacesUpDown).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.formatGroupBox, "formatGroupBox");
			this.formatGroupBox.Controls.Add(this.tableLayoutPanel3);
			this.formatGroupBox.Dock = DockStyle.Fill;
			this.formatGroupBox.Name = "formatGroupBox";
			this.formatGroupBox.TabStop = false;
			this.formatGroupBox.Enter += this.formatGroupBox_Enter;
			componentResourceManager.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
			this.tableLayoutPanel3.Controls.Add(this.explanationLabel, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.formatTypeLabel, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.formatTypeListBox, 0, 2);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			componentResourceManager.ApplyResources(this.explanationLabel, "explanationLabel");
			this.tableLayoutPanel3.SetColumnSpan(this.explanationLabel, 2);
			this.explanationLabel.Name = "explanationLabel";
			componentResourceManager.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.sampleGroupBox, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel3.SetRowSpan(this.tableLayoutPanel2, 2);
			componentResourceManager.ApplyResources(this.sampleGroupBox, "sampleGroupBox");
			this.sampleGroupBox.Controls.Add(this.sampleLabel);
			this.sampleGroupBox.MinimumSize = new Size(250, 38);
			this.sampleGroupBox.Name = "sampleGroupBox";
			this.sampleGroupBox.TabStop = false;
			componentResourceManager.ApplyResources(this.sampleLabel, "sampleLabel");
			this.sampleLabel.Name = "sampleLabel";
			componentResourceManager.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.secondRowLabel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.nullValueLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.nullValueTextBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.decimalPlacesUpDown, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.thirdRowLabel, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.dateTimeFormatsListBox, 0, 2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			componentResourceManager.ApplyResources(this.secondRowLabel, "secondRowLabel");
			this.secondRowLabel.MinimumSize = new Size(81, 14);
			this.secondRowLabel.Name = "secondRowLabel";
			componentResourceManager.ApplyResources(this.nullValueLabel, "nullValueLabel");
			this.nullValueLabel.MinimumSize = new Size(81, 14);
			this.nullValueLabel.Name = "nullValueLabel";
			componentResourceManager.ApplyResources(this.nullValueTextBox, "nullValueTextBox");
			this.nullValueTextBox.Name = "nullValueTextBox";
			this.nullValueTextBox.TextChanged += this.nullValueTextBox_TextChanged;
			componentResourceManager.ApplyResources(this.decimalPlacesUpDown, "decimalPlacesUpDown");
			NumericUpDown numericUpDown = this.decimalPlacesUpDown;
			int[] array = new int[4];
			array[0] = 6;
			numericUpDown.Maximum = new decimal(array);
			this.decimalPlacesUpDown.Name = "decimalPlacesUpDown";
			NumericUpDown numericUpDown2 = this.decimalPlacesUpDown;
			int[] array2 = new int[4];
			array2[0] = 2;
			numericUpDown2.Value = new decimal(array2);
			this.decimalPlacesUpDown.ValueChanged += this.decimalPlacesUpDown_ValueChanged;
			componentResourceManager.ApplyResources(this.thirdRowLabel, "thirdRowLabel");
			this.thirdRowLabel.Name = "thirdRowLabel";
			componentResourceManager.ApplyResources(this.dateTimeFormatsListBox, "dateTimeFormatsListBox");
			this.dateTimeFormatsListBox.FormattingEnabled = true;
			this.dateTimeFormatsListBox.Name = "dateTimeFormatsListBox";
			componentResourceManager.ApplyResources(this.formatTypeLabel, "formatTypeLabel");
			this.formatTypeLabel.Name = "formatTypeLabel";
			componentResourceManager.ApplyResources(this.formatTypeListBox, "formatTypeListBox");
			this.formatTypeListBox.FormattingEnabled = true;
			this.formatTypeListBox.Name = "formatTypeListBox";
			this.formatTypeListBox.SelectedIndexChanged += this.formatTypeListBox_SelectedIndexChanged;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.formatGroupBox);
			this.MinimumSize = new Size(390, 237);
			base.Name = "FormatControl";
			base.Load += this.FormatControl_Load;
			this.formatGroupBox.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.sampleGroupBox.ResumeLayout(false);
			this.sampleGroupBox.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.decimalPlacesUpDown).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04001788 RID: 6024
		private const int NoFormattingIndex = 0;

		// Token: 0x04001789 RID: 6025
		private const int NumericIndex = 1;

		// Token: 0x0400178A RID: 6026
		private const int CurrencyIndex = 2;

		// Token: 0x0400178B RID: 6027
		private const int DateTimeIndex = 3;

		// Token: 0x0400178C RID: 6028
		private const int ScientificIndex = 4;

		// Token: 0x0400178D RID: 6029
		private const int CustomIndex = 5;

		// Token: 0x0400178E RID: 6030
		private TextBox customStringTextBox = new TextBox();

		// Token: 0x0400178F RID: 6031
		private static DateTime dateTimeFormatValue = DateTime.Now;

		// Token: 0x04001790 RID: 6032
		private bool dirty;

		// Token: 0x04001791 RID: 6033
		private bool loaded;

		// Token: 0x04001792 RID: 6034
		private IContainer components;

		// Token: 0x04001793 RID: 6035
		private GroupBox formatGroupBox;

		// Token: 0x04001794 RID: 6036
		private Label explanationLabel;

		// Token: 0x04001795 RID: 6037
		private Label formatTypeLabel;

		// Token: 0x04001796 RID: 6038
		private ListBox formatTypeListBox;

		// Token: 0x04001797 RID: 6039
		private GroupBox sampleGroupBox;

		// Token: 0x04001798 RID: 6040
		private Label sampleLabel;

		// Token: 0x04001799 RID: 6041
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x0400179A RID: 6042
		private Label nullValueLabel;

		// Token: 0x0400179B RID: 6043
		private Label secondRowLabel;

		// Token: 0x0400179C RID: 6044
		private TextBox nullValueTextBox;

		// Token: 0x0400179D RID: 6045
		private Label thirdRowLabel;

		// Token: 0x0400179E RID: 6046
		private ListBox dateTimeFormatsListBox;

		// Token: 0x0400179F RID: 6047
		private NumericUpDown decimalPlacesUpDown;

		// Token: 0x040017A0 RID: 6048
		private TableLayoutPanel tableLayoutPanel2;

		// Token: 0x040017A1 RID: 6049
		private TableLayoutPanel tableLayoutPanel3;

		// Token: 0x02000574 RID: 1396
		private class DateTimeFormatsListBoxItem
		{
			// Token: 0x060031F0 RID: 12784 RVA: 0x0010F9D9 File Offset: 0x0010DBD9
			public DateTimeFormatsListBoxItem(DateTime value, string formatString)
			{
				this.value = value;
				this.formatString = formatString;
			}

			// Token: 0x170009B1 RID: 2481
			// (get) Token: 0x060031F1 RID: 12785 RVA: 0x0010F9EF File Offset: 0x0010DBEF
			public string FormatString
			{
				get
				{
					return this.formatString;
				}
			}

			// Token: 0x060031F2 RID: 12786 RVA: 0x0010F9F7 File Offset: 0x0010DBF7
			public override string ToString()
			{
				return this.value.ToString(this.formatString, CultureInfo.CurrentCulture);
			}

			// Token: 0x04002182 RID: 8578
			private DateTime value;

			// Token: 0x04002183 RID: 8579
			private string formatString;
		}

		// Token: 0x02000575 RID: 1397
		internal abstract class FormatTypeClass
		{
			// Token: 0x170009B2 RID: 2482
			// (get) Token: 0x060031F3 RID: 12787
			public abstract string TopLabelString { get; }

			// Token: 0x170009B3 RID: 2483
			// (get) Token: 0x060031F4 RID: 12788
			public abstract string SampleString { get; }

			// Token: 0x170009B4 RID: 2484
			// (get) Token: 0x060031F5 RID: 12789
			public abstract bool DropDownVisible { get; }

			// Token: 0x170009B5 RID: 2485
			// (get) Token: 0x060031F6 RID: 12790
			public abstract bool ListBoxVisible { get; }

			// Token: 0x170009B6 RID: 2486
			// (get) Token: 0x060031F7 RID: 12791
			public abstract bool FormatStringTextBoxVisible { get; }

			// Token: 0x170009B7 RID: 2487
			// (get) Token: 0x060031F8 RID: 12792
			public abstract bool FormatLabelVisible { get; }

			// Token: 0x170009B8 RID: 2488
			// (get) Token: 0x060031F9 RID: 12793
			public abstract string FormatString { get; }

			// Token: 0x060031FA RID: 12794
			public abstract bool Parse(string formatString);

			// Token: 0x060031FB RID: 12795
			public abstract void PushFormatStringIntoFormatType(string formatString);
		}

		// Token: 0x02000576 RID: 1398
		private class NoFormattingFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x170009B9 RID: 2489
			// (get) Token: 0x060031FD RID: 12797 RVA: 0x0010FA0F File Offset: 0x0010DC0F
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeNoFormattingExplanation");
				}
			}

			// Token: 0x170009BA RID: 2490
			// (get) Token: 0x060031FE RID: 12798 RVA: 0x0010FA1B File Offset: 0x0010DC1B
			public override string SampleString
			{
				get
				{
					return "-1234.5";
				}
			}

			// Token: 0x170009BB RID: 2491
			// (get) Token: 0x060031FF RID: 12799 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool DropDownVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009BC RID: 2492
			// (get) Token: 0x06003200 RID: 12800 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009BD RID: 2493
			// (get) Token: 0x06003201 RID: 12801 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009BE RID: 2494
			// (get) Token: 0x06003202 RID: 12802 RVA: 0x0010C52A File Offset: 0x0010A72A
			public override string FormatString
			{
				get
				{
					return "";
				}
			}

			// Token: 0x170009BF RID: 2495
			// (get) Token: 0x06003203 RID: 12803 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003204 RID: 12804 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool Parse(string formatString)
			{
				return false;
			}

			// Token: 0x06003205 RID: 12805 RVA: 0x00003937 File Offset: 0x00001B37
			public override void PushFormatStringIntoFormatType(string formatString)
			{
			}

			// Token: 0x06003206 RID: 12806 RVA: 0x0010FA22 File Offset: 0x0010DC22
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeNoFormatting");
			}
		}

		// Token: 0x02000577 RID: 1399
		private class NumericFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x06003208 RID: 12808 RVA: 0x0010FA36 File Offset: 0x0010DC36
			public NumericFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x170009C0 RID: 2496
			// (get) Token: 0x06003209 RID: 12809 RVA: 0x0010FA45 File Offset: 0x0010DC45
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeNumericExplanation");
				}
			}

			// Token: 0x170009C1 RID: 2497
			// (get) Token: 0x0600320A RID: 12810 RVA: 0x0010FA54 File Offset: 0x0010DC54
			public override string SampleString
			{
				get
				{
					return -1234.5678.ToString(this.FormatString, CultureInfo.CurrentCulture);
				}
			}

			// Token: 0x170009C2 RID: 2498
			// (get) Token: 0x0600320B RID: 12811 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool DropDownVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170009C3 RID: 2499
			// (get) Token: 0x0600320C RID: 12812 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009C4 RID: 2500
			// (get) Token: 0x0600320D RID: 12813 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009C5 RID: 2501
			// (get) Token: 0x0600320E RID: 12814 RVA: 0x0010FA80 File Offset: 0x0010DC80
			public override string FormatString
			{
				get
				{
					switch ((int)this.owner.decimalPlacesUpDown.Value)
					{
					case 0:
						return "N0";
					case 1:
						return "N1";
					case 2:
						return "N2";
					case 3:
						return "N3";
					case 4:
						return "N4";
					case 5:
						return "N5";
					case 6:
						return "N6";
					default:
						return "";
					}
				}
			}

			// Token: 0x170009C6 RID: 2502
			// (get) Token: 0x0600320F RID: 12815 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003210 RID: 12816 RVA: 0x0010FAF8 File Offset: 0x0010DCF8
			public static bool ParseStatic(string formatString)
			{
				return formatString.Equals("N0") || formatString.Equals("N1") || formatString.Equals("N2") || formatString.Equals("N3") || formatString.Equals("N4") || formatString.Equals("N5") || formatString.Equals("N6");
			}

			// Token: 0x06003211 RID: 12817 RVA: 0x0010FB60 File Offset: 0x0010DD60
			public override bool Parse(string formatString)
			{
				return FormatControl.NumericFormatType.ParseStatic(formatString);
			}

			// Token: 0x06003212 RID: 12818 RVA: 0x0010FB68 File Offset: 0x0010DD68
			public override void PushFormatStringIntoFormatType(string formatString)
			{
				if (formatString.Equals("N0"))
				{
					this.owner.decimalPlacesUpDown.Value = 0m;
					return;
				}
				if (formatString.Equals("N1"))
				{
					this.owner.decimalPlacesUpDown.Value = 1m;
					return;
				}
				if (formatString.Equals("N2"))
				{
					this.owner.decimalPlacesUpDown.Value = 2m;
					return;
				}
				if (formatString.Equals("N3"))
				{
					this.owner.decimalPlacesUpDown.Value = 3m;
					return;
				}
				if (formatString.Equals("N4"))
				{
					this.owner.decimalPlacesUpDown.Value = 4m;
					return;
				}
				if (formatString.Equals("N5"))
				{
					this.owner.decimalPlacesUpDown.Value = 5m;
					return;
				}
				if (formatString.Equals("N6"))
				{
					this.owner.decimalPlacesUpDown.Value = 6m;
				}
			}

			// Token: 0x06003213 RID: 12819 RVA: 0x0010FC6E File Offset: 0x0010DE6E
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeNumeric");
			}

			// Token: 0x04002184 RID: 8580
			private FormatControl owner;
		}

		// Token: 0x02000578 RID: 1400
		private class CurrencyFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x06003214 RID: 12820 RVA: 0x0010FC7A File Offset: 0x0010DE7A
			public CurrencyFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x170009C7 RID: 2503
			// (get) Token: 0x06003215 RID: 12821 RVA: 0x0010FC89 File Offset: 0x0010DE89
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeCurrencyExplanation");
				}
			}

			// Token: 0x170009C8 RID: 2504
			// (get) Token: 0x06003216 RID: 12822 RVA: 0x0010FC98 File Offset: 0x0010DE98
			public override string SampleString
			{
				get
				{
					return -1234.5678.ToString(this.FormatString, CultureInfo.CurrentCulture);
				}
			}

			// Token: 0x170009C9 RID: 2505
			// (get) Token: 0x06003217 RID: 12823 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool DropDownVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170009CA RID: 2506
			// (get) Token: 0x06003218 RID: 12824 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009CB RID: 2507
			// (get) Token: 0x06003219 RID: 12825 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009CC RID: 2508
			// (get) Token: 0x0600321A RID: 12826 RVA: 0x0010FCC4 File Offset: 0x0010DEC4
			public override string FormatString
			{
				get
				{
					switch ((int)this.owner.decimalPlacesUpDown.Value)
					{
					case 0:
						return "C0";
					case 1:
						return "C1";
					case 2:
						return "C2";
					case 3:
						return "C3";
					case 4:
						return "C4";
					case 5:
						return "C5";
					case 6:
						return "C6";
					default:
						return "";
					}
				}
			}

			// Token: 0x170009CD RID: 2509
			// (get) Token: 0x0600321B RID: 12827 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600321C RID: 12828 RVA: 0x0010FD3C File Offset: 0x0010DF3C
			public static bool ParseStatic(string formatString)
			{
				return formatString.Equals("C0") || formatString.Equals("C1") || formatString.Equals("C2") || formatString.Equals("C3") || formatString.Equals("C4") || formatString.Equals("C5") || formatString.Equals("C6");
			}

			// Token: 0x0600321D RID: 12829 RVA: 0x0010FDA4 File Offset: 0x0010DFA4
			public override bool Parse(string formatString)
			{
				return FormatControl.CurrencyFormatType.ParseStatic(formatString);
			}

			// Token: 0x0600321E RID: 12830 RVA: 0x0010FDAC File Offset: 0x0010DFAC
			public override void PushFormatStringIntoFormatType(string formatString)
			{
				if (formatString.Equals("C0"))
				{
					this.owner.decimalPlacesUpDown.Value = 0m;
					return;
				}
				if (formatString.Equals("C1"))
				{
					this.owner.decimalPlacesUpDown.Value = 1m;
					return;
				}
				if (formatString.Equals("C2"))
				{
					this.owner.decimalPlacesUpDown.Value = 2m;
					return;
				}
				if (formatString.Equals("C3"))
				{
					this.owner.decimalPlacesUpDown.Value = 3m;
					return;
				}
				if (formatString.Equals("C4"))
				{
					this.owner.decimalPlacesUpDown.Value = 4m;
					return;
				}
				if (formatString.Equals("C5"))
				{
					this.owner.decimalPlacesUpDown.Value = 5m;
					return;
				}
				if (formatString.Equals("C6"))
				{
					this.owner.decimalPlacesUpDown.Value = 6m;
				}
			}

			// Token: 0x0600321F RID: 12831 RVA: 0x0010FEB2 File Offset: 0x0010E0B2
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeCurrency");
			}

			// Token: 0x04002185 RID: 8581
			private FormatControl owner;
		}

		// Token: 0x02000579 RID: 1401
		private class DateTimeFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x06003220 RID: 12832 RVA: 0x0010FEBE File Offset: 0x0010E0BE
			public DateTimeFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x170009CE RID: 2510
			// (get) Token: 0x06003221 RID: 12833 RVA: 0x0010FECD File Offset: 0x0010E0CD
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeDateTimeExplanation");
				}
			}

			// Token: 0x170009CF RID: 2511
			// (get) Token: 0x06003222 RID: 12834 RVA: 0x0010FED9 File Offset: 0x0010E0D9
			public override string SampleString
			{
				get
				{
					if (this.owner.dateTimeFormatsListBox.SelectedItem == null)
					{
						return "";
					}
					return FormatControl.dateTimeFormatValue.ToString(this.FormatString, CultureInfo.CurrentCulture);
				}
			}

			// Token: 0x170009D0 RID: 2512
			// (get) Token: 0x06003223 RID: 12835 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool DropDownVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009D1 RID: 2513
			// (get) Token: 0x06003224 RID: 12836 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool ListBoxVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170009D2 RID: 2514
			// (get) Token: 0x06003225 RID: 12837 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009D3 RID: 2515
			// (get) Token: 0x06003226 RID: 12838 RVA: 0x0010FF08 File Offset: 0x0010E108
			public override string FormatString
			{
				get
				{
					FormatControl.DateTimeFormatsListBoxItem dateTimeFormatsListBoxItem = this.owner.dateTimeFormatsListBox.SelectedItem as FormatControl.DateTimeFormatsListBoxItem;
					return dateTimeFormatsListBoxItem.FormatString;
				}
			}

			// Token: 0x170009D4 RID: 2516
			// (get) Token: 0x06003227 RID: 12839 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003228 RID: 12840 RVA: 0x0010FF34 File Offset: 0x0010E134
			public static bool ParseStatic(string formatString)
			{
				return formatString.Equals("d") || formatString.Equals("D") || formatString.Equals("f") || formatString.Equals("F") || formatString.Equals("g") || formatString.Equals("G") || formatString.Equals("t") || formatString.Equals("T") || formatString.Equals("M");
			}

			// Token: 0x06003229 RID: 12841 RVA: 0x0010FFB6 File Offset: 0x0010E1B6
			public override bool Parse(string formatString)
			{
				return FormatControl.DateTimeFormatType.ParseStatic(formatString);
			}

			// Token: 0x0600322A RID: 12842 RVA: 0x0010FFC0 File Offset: 0x0010E1C0
			public override void PushFormatStringIntoFormatType(string formatString)
			{
				int selectedIndex = -1;
				if (formatString.Equals("d"))
				{
					selectedIndex = 0;
				}
				else if (formatString.Equals("D"))
				{
					selectedIndex = 1;
				}
				else if (formatString.Equals("f"))
				{
					selectedIndex = 2;
				}
				else if (formatString.Equals("F"))
				{
					selectedIndex = 3;
				}
				else if (formatString.Equals("g"))
				{
					selectedIndex = 4;
				}
				else if (formatString.Equals("G"))
				{
					selectedIndex = 5;
				}
				else if (formatString.Equals("t"))
				{
					selectedIndex = 6;
				}
				else if (formatString.Equals("T"))
				{
					selectedIndex = 7;
				}
				else if (formatString.Equals("M"))
				{
					selectedIndex = 8;
				}
				this.owner.dateTimeFormatsListBox.SelectedIndex = selectedIndex;
			}

			// Token: 0x0600322B RID: 12843 RVA: 0x0011007A File Offset: 0x0010E27A
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeDateTime");
			}

			// Token: 0x04002186 RID: 8582
			private FormatControl owner;
		}

		// Token: 0x0200057A RID: 1402
		private class ScientificFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x0600322C RID: 12844 RVA: 0x00110086 File Offset: 0x0010E286
			public ScientificFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x170009D5 RID: 2517
			// (get) Token: 0x0600322D RID: 12845 RVA: 0x00110095 File Offset: 0x0010E295
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeScientificExplanation");
				}
			}

			// Token: 0x170009D6 RID: 2518
			// (get) Token: 0x0600322E RID: 12846 RVA: 0x001100A4 File Offset: 0x0010E2A4
			public override string SampleString
			{
				get
				{
					return -1234.5678.ToString(this.FormatString, CultureInfo.CurrentCulture);
				}
			}

			// Token: 0x170009D7 RID: 2519
			// (get) Token: 0x0600322F RID: 12847 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool DropDownVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170009D8 RID: 2520
			// (get) Token: 0x06003230 RID: 12848 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009D9 RID: 2521
			// (get) Token: 0x06003231 RID: 12849 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009DA RID: 2522
			// (get) Token: 0x06003232 RID: 12850 RVA: 0x001100D0 File Offset: 0x0010E2D0
			public override string FormatString
			{
				get
				{
					switch ((int)this.owner.decimalPlacesUpDown.Value)
					{
					case 0:
						return "E0";
					case 1:
						return "E1";
					case 2:
						return "E2";
					case 3:
						return "E3";
					case 4:
						return "E4";
					case 5:
						return "E5";
					case 6:
						return "E6";
					default:
						return "";
					}
				}
			}

			// Token: 0x170009DB RID: 2523
			// (get) Token: 0x06003233 RID: 12851 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003234 RID: 12852 RVA: 0x00110148 File Offset: 0x0010E348
			public static bool ParseStatic(string formatString)
			{
				return formatString.Equals("E0") || formatString.Equals("E1") || formatString.Equals("E2") || formatString.Equals("E3") || formatString.Equals("E4") || formatString.Equals("E5") || formatString.Equals("E6");
			}

			// Token: 0x06003235 RID: 12853 RVA: 0x001101B0 File Offset: 0x0010E3B0
			public override bool Parse(string formatString)
			{
				return FormatControl.ScientificFormatType.ParseStatic(formatString);
			}

			// Token: 0x06003236 RID: 12854 RVA: 0x001101B8 File Offset: 0x0010E3B8
			public override void PushFormatStringIntoFormatType(string formatString)
			{
				if (formatString.Equals("E0"))
				{
					this.owner.decimalPlacesUpDown.Value = 0m;
					return;
				}
				if (formatString.Equals("E1"))
				{
					this.owner.decimalPlacesUpDown.Value = 1m;
					return;
				}
				if (formatString.Equals("E2"))
				{
					this.owner.decimalPlacesUpDown.Value = 2m;
					return;
				}
				if (formatString.Equals("E3"))
				{
					this.owner.decimalPlacesUpDown.Value = 3m;
					return;
				}
				if (formatString.Equals("E4"))
				{
					this.owner.decimalPlacesUpDown.Value = 4m;
					return;
				}
				if (formatString.Equals("E5"))
				{
					this.owner.decimalPlacesUpDown.Value = 5m;
					return;
				}
				if (formatString.Equals("E6"))
				{
					this.owner.decimalPlacesUpDown.Value = 6m;
				}
			}

			// Token: 0x06003237 RID: 12855 RVA: 0x001102BE File Offset: 0x0010E4BE
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeScientific");
			}

			// Token: 0x04002187 RID: 8583
			private FormatControl owner;
		}

		// Token: 0x0200057B RID: 1403
		private class CustomFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x06003238 RID: 12856 RVA: 0x001102CA File Offset: 0x0010E4CA
			public CustomFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x170009DC RID: 2524
			// (get) Token: 0x06003239 RID: 12857 RVA: 0x001102D9 File Offset: 0x0010E4D9
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeCustomExplanation");
				}
			}

			// Token: 0x170009DD RID: 2525
			// (get) Token: 0x0600323A RID: 12858 RVA: 0x001102E8 File Offset: 0x0010E4E8
			public override string SampleString
			{
				get
				{
					string formatString = this.FormatString;
					if (string.IsNullOrEmpty(formatString))
					{
						return "";
					}
					string text = "";
					if (FormatControl.DateTimeFormatType.ParseStatic(formatString))
					{
						text = FormatControl.dateTimeFormatValue.ToString(formatString, CultureInfo.CurrentCulture);
					}
					if (text.Equals(""))
					{
						try
						{
							text = -1234.5678.ToString(formatString, CultureInfo.CurrentCulture);
						}
						catch (FormatException)
						{
							text = "";
						}
					}
					if (text.Equals(""))
					{
						try
						{
							text = -1234.ToString(formatString, CultureInfo.CurrentCulture);
						}
						catch (FormatException)
						{
							text = "";
						}
					}
					if (text.Equals(""))
					{
						try
						{
							text = FormatControl.dateTimeFormatValue.ToString(formatString, CultureInfo.CurrentCulture);
						}
						catch (FormatException)
						{
							text = "";
						}
					}
					if (text.Equals(""))
					{
						text = SR.GetString("BindingFormattingDialogFormatTypeCustomInvalidFormat");
					}
					return text;
				}
			}

			// Token: 0x170009DE RID: 2526
			// (get) Token: 0x0600323B RID: 12859 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool DropDownVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009DF RID: 2527
			// (get) Token: 0x0600323C RID: 12860 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009E0 RID: 2528
			// (get) Token: 0x0600323D RID: 12861 RVA: 0x00003B0F File Offset: 0x00001D0F
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170009E1 RID: 2529
			// (get) Token: 0x0600323E RID: 12862 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009E2 RID: 2530
			// (get) Token: 0x0600323F RID: 12863 RVA: 0x001103F0 File Offset: 0x0010E5F0
			public override string FormatString
			{
				get
				{
					return this.owner.customStringTextBox.Text;
				}
			}

			// Token: 0x06003240 RID: 12864 RVA: 0x00003B0F File Offset: 0x00001D0F
			public static bool ParseStatic(string formatString)
			{
				return true;
			}

			// Token: 0x06003241 RID: 12865 RVA: 0x00110402 File Offset: 0x0010E602
			public override bool Parse(string formatString)
			{
				return FormatControl.CustomFormatType.ParseStatic(formatString);
			}

			// Token: 0x06003242 RID: 12866 RVA: 0x0011040A File Offset: 0x0010E60A
			public override void PushFormatStringIntoFormatType(string formatString)
			{
				this.owner.customStringTextBox.Text = formatString;
			}

			// Token: 0x06003243 RID: 12867 RVA: 0x0011041D File Offset: 0x0010E61D
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeCustom");
			}

			// Token: 0x04002188 RID: 8584
			private FormatControl owner;
		}
	}
}
