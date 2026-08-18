using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CustomControl.OrientAbleTextControls;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000012 RID: 18
	public class MyMultiCheckbox : UserControl, MyDynamicControl
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003FA0 File Offset: 0x00002FA0
		public new string ToString()
		{
			return this.ToStringMailMerge();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003FB8 File Offset: 0x00002FB8
		public void FromString(string s)
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003FBC File Offset: 0x00002FBC
		public object ReportObject
		{
			get
			{
				return this.GetText();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003FD4 File Offset: 0x00002FD4
		public MyMultiCheckbox()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003FED File Offset: 0x00002FED
		public MyMultiCheckbox(string[] colCaptions, bool hideCaption)
		{
			this.hideCaption = hideCaption;
			this.numCheckBoxes = colCaptions.Length;
			this.colCaptions = colCaptions;
			this.InitializeComponent();
			this.ctrls = null;
			this.Init();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000402C File Offset: 0x0000302C
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				control.BackColor = this.BackColor;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000040A0 File Offset: 0x000030A0
		public void Reset()
		{
			if (this.checkboxes != null)
			{
				foreach (CheckBox checkBox in this.checkboxes)
				{
					if (checkBox != null)
					{
						checkBox.Checked = false;
					}
				}
			}
			if (this.ctrls != null)
			{
				foreach (Control control in this.ctrls)
				{
					if (control != null)
					{
						if (control is CheckBox)
						{
							((CheckBox)control).Checked = false;
						}
						else if (control is TextBox)
						{
							control.Text = "";
						}
						else if (control is ComboBox)
						{
							((ComboBox)control).SelectedIndex = -1;
						}
					}
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00004198 File Offset: 0x00003198
		public bool FilledIn
		{
			get
			{
				if (this.checkboxes != null)
				{
					foreach (CheckBox checkBox in this.checkboxes)
					{
						if (checkBox != null && checkBox.Checked)
						{
							return true;
						}
					}
				}
				if (this.ctrls != null)
				{
					foreach (Control control in this.ctrls)
					{
						bool flag;
						if (control == null)
						{
							flag = false;
						}
						else if (control is MyDynamicControl)
						{
							flag = ((MyDynamicControl)control).FilledIn;
						}
						else if (control is CheckBox)
						{
							flag = ((CheckBox)control).Checked;
						}
						else
						{
							flag = (!(control is Label) && control.Text.Trim().Length > 0);
						}
						if (flag)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000042D0 File Offset: 0x000032D0
		public CheckBox[] CheckBoxes
		{
			get
			{
				return this.checkboxes;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000042E8 File Offset: 0x000032E8
		public MyMultiCheckbox(int width, string[] colCaptions, bool hideCaption, Control[] controls)
		{
			this.hideCaption = hideCaption;
			this.numCheckBoxes = colCaptions.Length;
			this.colCaptions = colCaptions;
			this.ctrls = controls;
			this.InitializeComponent();
			base.Width = width;
			this.Init();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000433C File Offset: 0x0000333C
		public MyMultiCheckbox(int width, string[] colCaptions)
		{
			this.hideCaption = false;
			this.numCheckBoxes = 0;
			this.checkboxes = new CheckBox[0];
			this.colCaptions = colCaptions;
			this.ctrls = null;
			this.InitializeComponent();
			base.Width = width;
			int num = this.chk.Width + 4;
			this.tableLayoutPanel1.ColumnCount = colCaptions.Length + 1;
			this.tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
			this.tableLayoutPanel1.ColumnStyles[0].Width = (float)num;
			for (int i = 1; i < colCaptions.Length; i++)
			{
				this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, (float)num));
			}
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			int num2 = base.Height;
			for (int i = 0; i < colCaptions.Length; i++)
			{
				string text = colCaptions[i];
				Control control;
				if (text.Length == 1)
				{
					control = new Label();
					control.Font = new Font(this.Font.FontFamily, 8f, FontStyle.Bold);
				}
				else
				{
					control = new OrientedTextLabel
					{
						RotationAngle = -90.0
					};
					control.Font = new Font(this.Font.FontFamily, 8f, FontStyle.Bold);
					Graphics graphics = control.CreateGraphics();
					int num3 = Convert.ToInt32(graphics.MeasureString(text, control.Font).Width + 4f);
					if (num3 > num2)
					{
						num2 = num3;
					}
				}
				control.Text = text;
				control.Width = num;
				control.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				this.tableLayoutPanel1.Controls.Add(control, i, 0);
			}
			base.Height = num2;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000453C File Offset: 0x0000353C
		public int NumCheckboxes
		{
			get
			{
				return this.numCheckBoxes;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004554 File Offset: 0x00003554
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00004594 File Offset: 0x00003594
		public override string Text
		{
			get
			{
				return (this.colCaptions == null || this.colCaptions.Length < 1) ? "" : this.colCaptions[this.colCaptions.Length - 1];
			}
			set
			{
				if (this.colCaptions != null && this.colCaptions.Length > 0)
				{
					this.colCaptions[this.colCaptions.Length - 1] = value;
					if (value.Length > 0 && this.checkboxes != null && this.checkboxes.Length > 0)
					{
						this.checkboxes[this.checkboxes.Length - 1].Text = value;
					}
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004614 File Offset: 0x00003614
		public string ToStringMailMerge()
		{
			string result;
			if (this.ctrls == null || this.ctrls.Length < 1)
			{
				result = this.Text;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.Text);
				bool flag = true;
				foreach (Control control in this.ctrls)
				{
					string text = "";
					if (control is TextBox)
					{
						text = ((TextBox)control).Text;
					}
					else if (control is ComboBox)
					{
						text = ((ComboBox)control).Text;
						if (text.Length < 1)
						{
							text = ((ComboBox)control).SelectedText;
						}
						int length = text.Length;
						bool flag2 = 0 == 0;
					}
					if (text.Length > 0)
					{
						if (flag)
						{
							stringBuilder.Append(" (");
							flag = false;
						}
						else
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(text);
					}
				}
				if (!flag)
				{
					stringBuilder.Append(")");
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000476C File Offset: 0x0000376C
		private void Init()
		{
			int num = this.chk.Width + 4;
			this.tableLayoutPanel1.ColumnCount = ((this.ctrls == null) ? this.numCheckBoxes : (this.ctrls.Length + this.numCheckBoxes));
			if (this.numCheckBoxes > 1)
			{
				this.tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
				this.tableLayoutPanel1.ColumnStyles[0].Width = (float)num;
			}
			else
			{
				this.tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.AutoSize;
			}
			for (int i = 1; i < this.numCheckBoxes; i++)
			{
				if (i < this.numCheckBoxes - 1)
				{
					this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, (float)num));
				}
				else
				{
					this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				}
			}
			if (this.ctrls != null)
			{
				for (int i = 0; i < this.ctrls.Length; i++)
				{
					this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				}
			}
			this.checkboxes = new CheckBox[this.numCheckBoxes];
			for (int i = 0; i < this.numCheckBoxes; i++)
			{
				MyCheckBox myCheckBox = new MyCheckBox();
				myCheckBox.Dock = DockStyle.Top;
				myCheckBox.Font = this.chk.Font;
				string text = (i < this.numCheckBoxes) ? this.colCaptions[i] : "";
				if (i < this.numCheckBoxes - 1)
				{
					myCheckBox.Width = num;
					myCheckBox.AutoSize = false;
					myCheckBox.Text = "";
				}
				else
				{
					myCheckBox.AutoSize = true;
					myCheckBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
					myCheckBox.Text = text;
				}
				this.tableLayoutPanel1.Controls.Add(myCheckBox, i, 0);
				this.checkboxes[i] = myCheckBox;
			}
			if (this.ctrls != null)
			{
				for (int i = 0; i < this.ctrls.Length; i++)
				{
					Control control = this.ctrls[i];
					control.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
					control.Width = 10;
					this.tableLayoutPanel1.Controls.Add(control, this.numCheckBoxes + i, 0);
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000049F4 File Offset: 0x000039F4
		public void SetTextBoxText(string text)
		{
			for (int i = this.tableLayoutPanel1.Controls.Count - 1; i >= 0; i--)
			{
				Control control = this.tableLayoutPanel1.Controls[i];
				if (control is TextBox)
				{
					control.Text = text;
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004A54 File Offset: 0x00003A54
		public CheckBox GetLastCheckbox()
		{
			CheckBox result;
			if (this.checkboxes != null && this.checkboxes.Length > 0)
			{
				result = this.checkboxes[this.checkboxes.Length - 1];
			}
			else
			{
				result = this.chk;
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004AA0 File Offset: 0x00003AA0
		public Font GetFont()
		{
			return this.chk.Font;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004AC0 File Offset: 0x00003AC0
		public void SetFont(Font font)
		{
			this.chk.Font = font;
			if (this.checkboxes != null)
			{
				foreach (MyCheckBox myCheckBox in this.checkboxes)
				{
					myCheckBox.Font = font;
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004B18 File Offset: 0x00003B18
		private TextBox FindTextBox()
		{
			if (this.ctrls != null && this.ctrls.Length > 0)
			{
				foreach (Control control in this.ctrls)
				{
					if (control is TextBox)
					{
						return (TextBox)control;
					}
				}
			}
			return null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004B8C File Offset: 0x00003B8C
		public string GetText()
		{
			TextBox textBox = this.FindTextBox();
			return (textBox != null) ? textBox.Text : "";
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004BB8 File Offset: 0x00003BB8
		public void SetText(string text)
		{
			TextBox textBox = this.FindTextBox();
			if (textBox != null)
			{
				textBox.Text = text;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004BE0 File Offset: 0x00003BE0
		public AutoComboBox GetComboBox()
		{
			for (int i = this.tableLayoutPanel1.Controls.Count - 1; i >= 0; i--)
			{
				Control control = this.tableLayoutPanel1.Controls[i];
				if (control is AutoComboBox)
				{
					return (AutoComboBox)control;
				}
			}
			return null;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004C48 File Offset: 0x00003C48
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00004CD4 File Offset: 0x00003CD4
		public int CheckedIntVal
		{
			get
			{
				int result;
				if (this.checkboxes == null || this.checkboxes.Length < 1)
				{
					result = 0;
				}
				else
				{
					int num = 0;
					for (int i = 0; i < this.checkboxes.Length; i++)
					{
						CheckBox checkBox = this.checkboxes[i];
						if (checkBox.Checked)
						{
							int num2 = Convert.ToInt32(Math.Pow(2.0, (double)i));
							num += num2;
						}
					}
					result = num;
				}
				return result;
			}
			set
			{
				for (int i = 0; i < this.checkboxes.Length; i++)
				{
					CheckBox checkBox = this.checkboxes[i];
					int num = Convert.ToInt32(Math.Pow(2.0, (double)i));
					checkBox.Checked = ((value & num) == num);
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004D28 File Offset: 0x00003D28
		private void MyMultiCheckbox_SizeChanged(object sender, EventArgs e)
		{
			if (this.checkboxes != null)
			{
				foreach (MyCheckBox myCheckBox in this.checkboxes)
				{
					myCheckBox.Height = this.tableLayoutPanel1.Height;
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004D7C File Offset: 0x00003D7C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				if (this.tableLayoutPanel1 != null)
				{
					foreach (object obj in this.tableLayoutPanel1.Controls)
					{
						Control control = (Control)obj;
						control.Dispose();
					}
					this.tableLayoutPanel1.Controls.Clear();
				}
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004E30 File Offset: 0x00003E30
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.chk = new CheckBox();
			base.SuspendLayout();
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.BackColor = SystemColors.Control;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Margin = new Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.Size = new Size(565, 28);
			this.tableLayoutPanel1.TabIndex = 4;
			this.chk.AutoSize = true;
			this.chk.BackColor = SystemColors.ActiveCaption;
			this.chk.Location = new Point(3, 6);
			this.chk.Margin = new Padding(3, 4, 3, 4);
			this.chk.Name = "chk";
			this.chk.Size = new Size(15, 14);
			this.chk.TabIndex = 5;
			this.chk.UseVisualStyleBackColor = false;
			this.chk.Visible = false;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.chk);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "MyMultiCheckbox";
			base.Size = new Size(565, 28);
			base.SizeChanged += this.MyMultiCheckbox_SizeChanged;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000072 RID: 114
		private CheckBox[] checkboxes;

		// Token: 0x04000073 RID: 115
		private string[] colCaptions;

		// Token: 0x04000074 RID: 116
		private OrientedTextLabel[] labels;

		// Token: 0x04000075 RID: 117
		private int numCheckBoxes;

		// Token: 0x04000076 RID: 118
		private bool hideCaption;

		// Token: 0x04000077 RID: 119
		private Control[] ctrls;

		// Token: 0x04000078 RID: 120
		private IContainer components = null;

		// Token: 0x04000079 RID: 121
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x0400007A RID: 122
		private CheckBox chk;
	}
}
