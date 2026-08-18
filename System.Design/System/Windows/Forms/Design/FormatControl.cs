using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200023C RID: 572
	internal class FormatControl : UserControl
	{
		// Token: 0x0600159F RID: 5535 RVA: 0x00071101 File Offset: 0x00070101
		public FormatControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0007111A File Offset: 0x0007011A
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x00071122 File Offset: 0x00070122
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

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x0007112C File Offset: 0x0007012C
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x0007115C File Offset: 0x0007015C
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

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x000711C1 File Offset: 0x000701C1
		public FormatControl.FormatTypeClass FormatTypeItem
		{
			get
			{
				return this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000711D4 File Offset: 0x000701D4
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x000711FD File Offset: 0x000701FD
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

		// Token: 0x17000386 RID: 902
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x00071239 File Offset: 0x00070239
		public bool NullValueTextBoxEnabled
		{
			set
			{
				this.nullValueTextBox.Enabled = value;
			}
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00071248 File Offset: 0x00070248
		private void customStringTextBox_TextChanged(object sender, EventArgs e)
		{
			FormatControl.CustomFormatType customFormatType = this.formatTypeListBox.SelectedItem as FormatControl.CustomFormatType;
			this.sampleLabel.Text = customFormatType.SampleString;
			this.dirty = true;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00071280 File Offset: 0x00070280
		private void dateTimeFormatsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			FormatControl.FormatTypeClass formatTypeClass = this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass;
			this.sampleLabel.Text = formatTypeClass.SampleString;
			this.dirty = true;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x000712B8 File Offset: 0x000702B8
		private void decimalPlacesUpDown_ValueChanged(object sender, EventArgs e)
		{
			FormatControl.FormatTypeClass formatTypeClass = this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass;
			this.sampleLabel.Text = formatTypeClass.SampleString;
			this.dirty = true;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x000712EE File Offset: 0x000702EE
		private void formatGroupBox_Enter(object sender, EventArgs e)
		{
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000712F0 File Offset: 0x000702F0
		private void formatTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			FormatControl.FormatTypeClass formatTypeClass = this.formatTypeListBox.SelectedItem as FormatControl.FormatTypeClass;
			this.UpdateControlVisibility(formatTypeClass);
			this.sampleLabel.Text = formatTypeClass.SampleString;
			this.explanationLabel.Text = formatTypeClass.TopLabelString;
			this.dirty = true;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00071340 File Offset: 0x00070340
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

		// Token: 0x060015AE RID: 5550 RVA: 0x000713B8 File Offset: 0x000703B8
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

		// Token: 0x060015AF RID: 5551 RVA: 0x000714A4 File Offset: 0x000704A4
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

		// Token: 0x060015B0 RID: 5552 RVA: 0x000715B4 File Offset: 0x000705B4
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

		// Token: 0x060015B1 RID: 5553 RVA: 0x000717F0 File Offset: 0x000707F0
		private void UpdateCustomStringTextBox()
		{
			this.customStringTextBox = new TextBox();
			this.customStringTextBox.AccessibleDescription = SR.GetString("BindingFormattingDialogCustomFormatAccessibleDescription");
			this.customStringTextBox.Margin = new Padding(0, 3, 0, 3);
			this.customStringTextBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.customStringTextBox.TabIndex = 3;
			this.customStringTextBox.TextChanged += this.customStringTextBox_TextChanged;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00071861 File Offset: 0x00070861
		private void UpdateFormatTypeListBoxHeight()
		{
			this.formatTypeListBox.Height = this.tableLayoutPanel1.Bottom - this.formatTypeListBox.Top;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00071888 File Offset: 0x00070888
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

		// Token: 0x060015B4 RID: 5556 RVA: 0x00071A00 File Offset: 0x00070A00
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

		// Token: 0x060015B5 RID: 5557 RVA: 0x00071AC8 File Offset: 0x00070AC8
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

		// Token: 0x060015B6 RID: 5558 RVA: 0x00071D18 File Offset: 0x00070D18
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

		// Token: 0x060015B7 RID: 5559 RVA: 0x00071D59 File Offset: 0x00070D59
		private void nullValueTextBox_TextChanged(object sender, EventArgs e)
		{
			this.dirty = true;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00071D62 File Offset: 0x00070D62
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00071D84 File Offset: 0x00070D84
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormatControl));
			this.formatGroupBox = new GroupBox();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.secondRowLabel = new Label();
			this.nullValueLabel = new Label();
			this.nullValueTextBox = new TextBox();
			this.decimalPlacesUpDown = new NumericUpDown();
			this.thirdRowLabel = new Label();
			this.dateTimeFormatsListBox = new ListBox();
			this.sampleGroupBox = new GroupBox();
			this.sampleLabel = new Label();
			this.formatTypeListBox = new ListBox();
			this.formatTypeLabel = new Label();
			this.explanationLabel = new Label();
			this.formatGroupBox.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.decimalPlacesUpDown).BeginInit();
			this.sampleGroupBox.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.formatGroupBox, "formatGroupBox");
			this.formatGroupBox.Margin = new Padding(0);
			this.formatGroupBox.Controls.Add(this.tableLayoutPanel1);
			this.formatGroupBox.Controls.Add(this.sampleGroupBox);
			this.formatGroupBox.Controls.Add(this.formatTypeListBox);
			this.formatGroupBox.Controls.Add(this.formatTypeLabel);
			this.formatGroupBox.Controls.Add(this.explanationLabel);
			this.formatGroupBox.Name = "formatGroupBox";
			this.formatGroupBox.TabStop = false;
			this.formatGroupBox.Enter += this.formatGroupBox_Enter;
			componentResourceManager.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.secondRowLabel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.nullValueLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.nullValueTextBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.decimalPlacesUpDown, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.thirdRowLabel, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.dateTimeFormatsListBox, 0, 2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			componentResourceManager.ApplyResources(this.secondRowLabel, "secondRowLabel");
			this.secondRowLabel.MinimumSize = new Size(81, 14);
			this.secondRowLabel.Name = "secondRowLabel";
			componentResourceManager.ApplyResources(this.nullValueLabel, "nullValueLabel");
			this.nullValueLabel.MinimumSize = new Size(81, 14);
			this.nullValueLabel.Name = "nullValueLabel";
			componentResourceManager.ApplyResources(this.nullValueTextBox, "nullValueTextBox");
			this.nullValueTextBox.Margin = new Padding(0, 3, 0, 3);
			this.nullValueTextBox.Name = "nullValueTextBox";
			this.nullValueTextBox.TextChanged += this.nullValueTextBox_TextChanged;
			componentResourceManager.ApplyResources(this.decimalPlacesUpDown, "decimalPlacesUpDown");
			this.decimalPlacesUpDown.Margin = new Padding(0, 3, 0, 3);
			NumericUpDown numericUpDown = this.decimalPlacesUpDown;
			int[] array = new int[4];
			array[0] = 6;
			numericUpDown.Maximum = new decimal(array);
			NumericUpDown numericUpDown2 = this.decimalPlacesUpDown;
			int[] array2 = new int[4];
			array2[0] = 2;
			numericUpDown2.Value = new decimal(array2);
			this.decimalPlacesUpDown.Name = "decimalPlacesUpDown";
			this.decimalPlacesUpDown.ValueChanged += this.decimalPlacesUpDown_ValueChanged;
			componentResourceManager.ApplyResources(this.thirdRowLabel, "thirdRowLabel");
			this.thirdRowLabel.Name = "thirdRowLabel";
			componentResourceManager.ApplyResources(this.dateTimeFormatsListBox, "dateTimeFormatsListBox");
			this.dateTimeFormatsListBox.FormattingEnabled = true;
			this.dateTimeFormatsListBox.Margin = new Padding(3, 0, 0, 0);
			this.dateTimeFormatsListBox.Name = "dateTimeFormatsListBox";
			componentResourceManager.ApplyResources(this.sampleGroupBox, "sampleGroupBox");
			this.sampleGroupBox.Controls.Add(this.sampleLabel);
			this.sampleGroupBox.MinimumSize = new Size(250, 38);
			this.sampleGroupBox.Name = "sampleGroupBox";
			this.sampleGroupBox.Padding = new Padding(0);
			this.sampleGroupBox.TabStop = false;
			componentResourceManager.ApplyResources(this.sampleLabel, "sampleLabel");
			this.sampleLabel.Name = "sampleLabel";
			componentResourceManager.ApplyResources(this.formatTypeListBox, "formatTypeListBox");
			this.formatTypeListBox.FormattingEnabled = true;
			this.formatTypeListBox.Name = "formatTypeListBox";
			this.formatTypeListBox.SelectedIndexChanged += this.formatTypeListBox_SelectedIndexChanged;
			componentResourceManager.ApplyResources(this.formatTypeLabel, "formatTypeLabel");
			this.formatTypeLabel.Name = "formatTypeLabel";
			componentResourceManager.ApplyResources(this.explanationLabel, "explanationLabel");
			this.explanationLabel.MinimumSize = new Size(0, 30);
			this.explanationLabel.Name = "explanationLabel";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.formatGroupBox);
			this.MinimumSize = new Size(390, 237);
			base.Name = "FormatControl";
			base.Load += this.FormatControl_Load;
			this.formatGroupBox.ResumeLayout(false);
			this.formatGroupBox.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.decimalPlacesUpDown).EndInit();
			this.sampleGroupBox.ResumeLayout(false);
			this.sampleGroupBox.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040012B8 RID: 4792
		private const int NoFormattingIndex = 0;

		// Token: 0x040012B9 RID: 4793
		private const int NumericIndex = 1;

		// Token: 0x040012BA RID: 4794
		private const int CurrencyIndex = 2;

		// Token: 0x040012BB RID: 4795
		private const int DateTimeIndex = 3;

		// Token: 0x040012BC RID: 4796
		private const int ScientificIndex = 4;

		// Token: 0x040012BD RID: 4797
		private const int CustomIndex = 5;

		// Token: 0x040012BE RID: 4798
		private TextBox customStringTextBox = new TextBox();

		// Token: 0x040012BF RID: 4799
		private static DateTime dateTimeFormatValue = DateTime.Now;

		// Token: 0x040012C0 RID: 4800
		private bool dirty;

		// Token: 0x040012C1 RID: 4801
		private bool loaded;

		// Token: 0x040012C2 RID: 4802
		private IContainer components;

		// Token: 0x040012C3 RID: 4803
		private GroupBox formatGroupBox;

		// Token: 0x040012C4 RID: 4804
		private Label explanationLabel;

		// Token: 0x040012C5 RID: 4805
		private Label formatTypeLabel;

		// Token: 0x040012C6 RID: 4806
		private ListBox formatTypeListBox;

		// Token: 0x040012C7 RID: 4807
		private GroupBox sampleGroupBox;

		// Token: 0x040012C8 RID: 4808
		private Label sampleLabel;

		// Token: 0x040012C9 RID: 4809
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x040012CA RID: 4810
		private Label nullValueLabel;

		// Token: 0x040012CB RID: 4811
		private Label secondRowLabel;

		// Token: 0x040012CC RID: 4812
		private TextBox nullValueTextBox;

		// Token: 0x040012CD RID: 4813
		private Label thirdRowLabel;

		// Token: 0x040012CE RID: 4814
		private ListBox dateTimeFormatsListBox;

		// Token: 0x040012CF RID: 4815
		private NumericUpDown decimalPlacesUpDown;

		// Token: 0x0200023D RID: 573
		private class DateTimeFormatsListBoxItem
		{
			// Token: 0x060015BB RID: 5563 RVA: 0x000723CA File Offset: 0x000713CA
			public DateTimeFormatsListBoxItem(DateTime value, string formatString)
			{
				this.value = value;
				this.formatString = formatString;
			}

			// Token: 0x17000387 RID: 903
			// (get) Token: 0x060015BC RID: 5564 RVA: 0x000723E0 File Offset: 0x000713E0
			public string FormatString
			{
				get
				{
					return this.formatString;
				}
			}

			// Token: 0x060015BD RID: 5565 RVA: 0x000723E8 File Offset: 0x000713E8
			public override string ToString()
			{
				return this.value.ToString(this.formatString, CultureInfo.CurrentCulture);
			}

			// Token: 0x040012D0 RID: 4816
			private DateTime value;

			// Token: 0x040012D1 RID: 4817
			private string formatString;
		}

		// Token: 0x0200023E RID: 574
		internal abstract class FormatTypeClass
		{
			// Token: 0x17000388 RID: 904
			// (get) Token: 0x060015BE RID: 5566
			public abstract string TopLabelString { get; }

			// Token: 0x17000389 RID: 905
			// (get) Token: 0x060015BF RID: 5567
			public abstract string SampleString { get; }

			// Token: 0x1700038A RID: 906
			// (get) Token: 0x060015C0 RID: 5568
			public abstract bool DropDownVisible { get; }

			// Token: 0x1700038B RID: 907
			// (get) Token: 0x060015C1 RID: 5569
			public abstract bool ListBoxVisible { get; }

			// Token: 0x1700038C RID: 908
			// (get) Token: 0x060015C2 RID: 5570
			public abstract bool FormatStringTextBoxVisible { get; }

			// Token: 0x1700038D RID: 909
			// (get) Token: 0x060015C3 RID: 5571
			public abstract bool FormatLabelVisible { get; }

			// Token: 0x1700038E RID: 910
			// (get) Token: 0x060015C4 RID: 5572
			public abstract string FormatString { get; }

			// Token: 0x060015C5 RID: 5573
			public abstract bool Parse(string formatString);

			// Token: 0x060015C6 RID: 5574
			public abstract void PushFormatStringIntoFormatType(string formatString);
		}

		// Token: 0x0200023F RID: 575
		private class NoFormattingFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x1700038F RID: 911
			// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00072408 File Offset: 0x00071408
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeNoFormattingExplanation");
				}
			}

			// Token: 0x17000390 RID: 912
			// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00072414 File Offset: 0x00071414
			public override string SampleString
			{
				get
				{
					return "-1234.5";
				}
			}

			// Token: 0x17000391 RID: 913
			// (get) Token: 0x060015CA RID: 5578 RVA: 0x0007241B File Offset: 0x0007141B
			public override bool DropDownVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000392 RID: 914
			// (get) Token: 0x060015CB RID: 5579 RVA: 0x0007241E File Offset: 0x0007141E
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000393 RID: 915
			// (get) Token: 0x060015CC RID: 5580 RVA: 0x00072421 File Offset: 0x00071421
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000394 RID: 916
			// (get) Token: 0x060015CD RID: 5581 RVA: 0x00072424 File Offset: 0x00071424
			public override string FormatString
			{
				get
				{
					return "";
				}
			}

			// Token: 0x17000395 RID: 917
			// (get) Token: 0x060015CE RID: 5582 RVA: 0x0007242B File Offset: 0x0007142B
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015CF RID: 5583 RVA: 0x0007242E File Offset: 0x0007142E
			public override bool Parse(string formatString)
			{
				return false;
			}

			// Token: 0x060015D0 RID: 5584 RVA: 0x00072431 File Offset: 0x00071431
			public override void PushFormatStringIntoFormatType(string formatString)
			{
			}

			// Token: 0x060015D1 RID: 5585 RVA: 0x00072433 File Offset: 0x00071433
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeNoFormatting");
			}
		}

		// Token: 0x02000240 RID: 576
		private class NumericFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x060015D3 RID: 5587 RVA: 0x00072447 File Offset: 0x00071447
			public NumericFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x17000396 RID: 918
			// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00072456 File Offset: 0x00071456
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeNumericExplanation");
				}
			}

			// Token: 0x17000397 RID: 919
			// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00072464 File Offset: 0x00071464
			public override string SampleString
			{
				get
				{
					return -1234.5678.ToString(this.FormatString, CultureInfo.CurrentCulture);
				}
			}

			// Token: 0x17000398 RID: 920
			// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0007248D File Offset: 0x0007148D
			public override bool DropDownVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000399 RID: 921
			// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00072490 File Offset: 0x00071490
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700039A RID: 922
			// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00072493 File Offset: 0x00071493
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700039B RID: 923
			// (get) Token: 0x060015D9 RID: 5593 RVA: 0x00072498 File Offset: 0x00071498
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

			// Token: 0x1700039C RID: 924
			// (get) Token: 0x060015DA RID: 5594 RVA: 0x0007250E File Offset: 0x0007150E
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015DB RID: 5595 RVA: 0x00072514 File Offset: 0x00071514
			public static bool ParseStatic(string formatString)
			{
				return formatString.Equals("N0") || formatString.Equals("N1") || formatString.Equals("N2") || formatString.Equals("N3") || formatString.Equals("N4") || formatString.Equals("N5") || formatString.Equals("N6");
			}

			// Token: 0x060015DC RID: 5596 RVA: 0x0007257C File Offset: 0x0007157C
			public override bool Parse(string formatString)
			{
				return FormatControl.NumericFormatType.ParseStatic(formatString);
			}

			// Token: 0x060015DD RID: 5597 RVA: 0x00072584 File Offset: 0x00071584
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

			// Token: 0x060015DE RID: 5598 RVA: 0x0007268C File Offset: 0x0007168C
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeNumeric");
			}

			// Token: 0x040012D2 RID: 4818
			private FormatControl owner;
		}

		// Token: 0x02000241 RID: 577
		private class CurrencyFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x060015DF RID: 5599 RVA: 0x00072698 File Offset: 0x00071698
			public CurrencyFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x1700039D RID: 925
			// (get) Token: 0x060015E0 RID: 5600 RVA: 0x000726A7 File Offset: 0x000716A7
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeCurrencyExplanation");
				}
			}

			// Token: 0x1700039E RID: 926
			// (get) Token: 0x060015E1 RID: 5601 RVA: 0x000726B4 File Offset: 0x000716B4
			public override string SampleString
			{
				get
				{
					return -1234.5678.ToString(this.FormatString, CultureInfo.CurrentCulture);
				}
			}

			// Token: 0x1700039F RID: 927
			// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000726DD File Offset: 0x000716DD
			public override bool DropDownVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003A0 RID: 928
			// (get) Token: 0x060015E3 RID: 5603 RVA: 0x000726E0 File Offset: 0x000716E0
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003A1 RID: 929
			// (get) Token: 0x060015E4 RID: 5604 RVA: 0x000726E3 File Offset: 0x000716E3
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003A2 RID: 930
			// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000726E8 File Offset: 0x000716E8
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

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0007275E File Offset: 0x0007175E
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015E7 RID: 5607 RVA: 0x00072764 File Offset: 0x00071764
			public static bool ParseStatic(string formatString)
			{
				return formatString.Equals("C0") || formatString.Equals("C1") || formatString.Equals("C2") || formatString.Equals("C3") || formatString.Equals("C4") || formatString.Equals("C5") || formatString.Equals("C6");
			}

			// Token: 0x060015E8 RID: 5608 RVA: 0x000727CC File Offset: 0x000717CC
			public override bool Parse(string formatString)
			{
				return FormatControl.CurrencyFormatType.ParseStatic(formatString);
			}

			// Token: 0x060015E9 RID: 5609 RVA: 0x000727D4 File Offset: 0x000717D4
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

			// Token: 0x060015EA RID: 5610 RVA: 0x000728DC File Offset: 0x000718DC
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeCurrency");
			}

			// Token: 0x040012D3 RID: 4819
			private FormatControl owner;
		}

		// Token: 0x02000242 RID: 578
		private class DateTimeFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x060015EB RID: 5611 RVA: 0x000728E8 File Offset: 0x000718E8
			public DateTimeFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x060015EC RID: 5612 RVA: 0x000728F7 File Offset: 0x000718F7
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeDateTimeExplanation");
				}
			}

			// Token: 0x170003A5 RID: 933
			// (get) Token: 0x060015ED RID: 5613 RVA: 0x00072903 File Offset: 0x00071903
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

			// Token: 0x170003A6 RID: 934
			// (get) Token: 0x060015EE RID: 5614 RVA: 0x00072932 File Offset: 0x00071932
			public override bool DropDownVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x060015EF RID: 5615 RVA: 0x00072935 File Offset: 0x00071935
			public override bool ListBoxVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00072938 File Offset: 0x00071938
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0007293C File Offset: 0x0007193C
			public override string FormatString
			{
				get
				{
					FormatControl.DateTimeFormatsListBoxItem dateTimeFormatsListBoxItem = this.owner.dateTimeFormatsListBox.SelectedItem as FormatControl.DateTimeFormatsListBoxItem;
					return dateTimeFormatsListBoxItem.FormatString;
				}
			}

			// Token: 0x170003AA RID: 938
			// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00072965 File Offset: 0x00071965
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015F3 RID: 5619 RVA: 0x00072968 File Offset: 0x00071968
			public static bool ParseStatic(string formatString)
			{
				return formatString.Equals("d") || formatString.Equals("D") || formatString.Equals("f") || formatString.Equals("F") || formatString.Equals("g") || formatString.Equals("G") || formatString.Equals("t") || formatString.Equals("T") || formatString.Equals("M");
			}

			// Token: 0x060015F4 RID: 5620 RVA: 0x000729EA File Offset: 0x000719EA
			public override bool Parse(string formatString)
			{
				return FormatControl.DateTimeFormatType.ParseStatic(formatString);
			}

			// Token: 0x060015F5 RID: 5621 RVA: 0x000729F4 File Offset: 0x000719F4
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

			// Token: 0x060015F6 RID: 5622 RVA: 0x00072AAE File Offset: 0x00071AAE
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeDateTime");
			}

			// Token: 0x040012D4 RID: 4820
			private FormatControl owner;
		}

		// Token: 0x02000243 RID: 579
		private class ScientificFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x060015F7 RID: 5623 RVA: 0x00072ABA File Offset: 0x00071ABA
			public ScientificFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x170003AB RID: 939
			// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00072AC9 File Offset: 0x00071AC9
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeScientificExplanation");
				}
			}

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00072AD8 File Offset: 0x00071AD8
			public override string SampleString
			{
				get
				{
					return -1234.5678.ToString(this.FormatString, CultureInfo.CurrentCulture);
				}
			}

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x060015FA RID: 5626 RVA: 0x00072B01 File Offset: 0x00071B01
			public override bool DropDownVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x060015FB RID: 5627 RVA: 0x00072B04 File Offset: 0x00071B04
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x060015FC RID: 5628 RVA: 0x00072B07 File Offset: 0x00071B07
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x060015FD RID: 5629 RVA: 0x00072B0C File Offset: 0x00071B0C
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

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x060015FE RID: 5630 RVA: 0x00072B82 File Offset: 0x00071B82
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060015FF RID: 5631 RVA: 0x00072B88 File Offset: 0x00071B88
			public static bool ParseStatic(string formatString)
			{
				return formatString.Equals("E0") || formatString.Equals("E1") || formatString.Equals("E2") || formatString.Equals("E3") || formatString.Equals("E4") || formatString.Equals("E5") || formatString.Equals("E6");
			}

			// Token: 0x06001600 RID: 5632 RVA: 0x00072BF0 File Offset: 0x00071BF0
			public override bool Parse(string formatString)
			{
				return FormatControl.ScientificFormatType.ParseStatic(formatString);
			}

			// Token: 0x06001601 RID: 5633 RVA: 0x00072BF8 File Offset: 0x00071BF8
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

			// Token: 0x06001602 RID: 5634 RVA: 0x00072D00 File Offset: 0x00071D00
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeScientific");
			}

			// Token: 0x040012D5 RID: 4821
			private FormatControl owner;
		}

		// Token: 0x02000244 RID: 580
		private class CustomFormatType : FormatControl.FormatTypeClass
		{
			// Token: 0x06001603 RID: 5635 RVA: 0x00072D0C File Offset: 0x00071D0C
			public CustomFormatType(FormatControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06001604 RID: 5636 RVA: 0x00072D1B File Offset: 0x00071D1B
			public override string TopLabelString
			{
				get
				{
					return SR.GetString("BindingFormattingDialogFormatTypeCustomExplanation");
				}
			}

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06001605 RID: 5637 RVA: 0x00072D28 File Offset: 0x00071D28
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

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x06001606 RID: 5638 RVA: 0x00072E30 File Offset: 0x00071E30
			public override bool DropDownVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x06001607 RID: 5639 RVA: 0x00072E33 File Offset: 0x00071E33
			public override bool ListBoxVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x06001608 RID: 5640 RVA: 0x00072E36 File Offset: 0x00071E36
			public override bool FormatStringTextBoxVisible
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x06001609 RID: 5641 RVA: 0x00072E39 File Offset: 0x00071E39
			public override bool FormatLabelVisible
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003B8 RID: 952
			// (get) Token: 0x0600160A RID: 5642 RVA: 0x00072E3C File Offset: 0x00071E3C
			public override string FormatString
			{
				get
				{
					return this.owner.customStringTextBox.Text;
				}
			}

			// Token: 0x0600160B RID: 5643 RVA: 0x00072E4E File Offset: 0x00071E4E
			public static bool ParseStatic(string formatString)
			{
				return true;
			}

			// Token: 0x0600160C RID: 5644 RVA: 0x00072E51 File Offset: 0x00071E51
			public override bool Parse(string formatString)
			{
				return FormatControl.CustomFormatType.ParseStatic(formatString);
			}

			// Token: 0x0600160D RID: 5645 RVA: 0x00072E59 File Offset: 0x00071E59
			public override void PushFormatStringIntoFormatType(string formatString)
			{
				this.owner.customStringTextBox.Text = formatString;
			}

			// Token: 0x0600160E RID: 5646 RVA: 0x00072E6C File Offset: 0x00071E6C
			public override string ToString()
			{
				return SR.GetString("BindingFormattingDialogFormatTypeCustom");
			}

			// Token: 0x040012D6 RID: 4822
			private FormatControl owner;
		}
	}
}
