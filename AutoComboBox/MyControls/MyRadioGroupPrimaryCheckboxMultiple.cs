using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000075 RID: 117
	public class MyRadioGroupPrimaryCheckboxMultiple : UserControl, MyDynamicControl
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x000255EC File Offset: 0x000245EC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00025624 File Offset: 0x00024624
		private void InitializeComponent()
		{
			this.chk = new MyCheckBox();
			this.rb = new MyRadioButton();
			base.SuspendLayout();
			this.chk.AutoCheckThisBoxWhenOtherControlModified_cid = 0;
			this.chk.AutoResizeMode = CheckBoxAutoResizeMode.none;
			this.chk.AutoSize = true;
			this.chk.Dock = DockStyle.Fill;
			this.chk.Location = new Point(24, 3);
			this.chk.Name = "chk";
			this.chk.Padding = new Padding(0, 1, 0, 0);
			this.chk.SetEnabledControl = null;
			this.chk.SetEnabledControlId = -1;
			this.chk.Size = new Size(208, 20);
			this.chk.TabIndex = 2;
			this.chk.Text = "Chronic Medical / System / Medical Condition Chronic Medical / System / Medical Condition";
			this.chk.UseVisualStyleBackColor = true;
			this.chk.CheckedChanged += this.checkBoxX1_CheckedChanged;
			this.rb.AutoCheck = false;
			this.rb.AutoSizeHeight = false;
			this.rb.Dock = DockStyle.Left;
			this.rb.Location = new Point(0, 3);
			this.rb.Name = "rb";
			this.rb.Size = new Size(24, 20);
			this.rb.TabIndex = 1;
			this.rb.TabStop = true;
			this.rb.UseVisualStyleBackColor = true;
			this.rb.CheckedChanged += this.myRadioButton1_CheckedChanged;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.chk);
			base.Controls.Add(this.rb);
			base.Margin = new Padding(0);
			base.Name = "MyRadioGroupPrimaryCheckboxMultiple";
			base.Padding = new Padding(0, 3, 0, 3);
			base.Size = new Size(232, 26);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00025860 File Offset: 0x00024860
		public new string ToString()
		{
			return "Un-supported";
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00025877 File Offset: 0x00024877
		public void FromString(string s)
		{
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0002587C File Offset: 0x0002487C
		public object ReportObject
		{
			get
			{
				return this.PrimaryChecked;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00025899 File Offset: 0x00024899
		public MyRadioGroupPrimaryCheckboxMultiple()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000258C7 File Offset: 0x000248C7
		public void SetForeColour(Color c)
		{
			this.rb.ForeColor = c;
			this.chk.ForeColor = c;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x000258E4 File Offset: 0x000248E4
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00025901 File Offset: 0x00024901
		public override string Text
		{
			get
			{
				return this.chk.Text;
			}
			set
			{
				this.chk.Text = value;
				this.rb.AccessibleDescription = value;
				this.rb.AccessibleName = value;
				this.AutoSizeMe();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00025934 File Offset: 0x00024934
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0002594C File Offset: 0x0002494C
		public bool AllowBoth
		{
			get
			{
				return this.allowBoth;
			}
			set
			{
				this.allowBoth = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00025958 File Offset: 0x00024958
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00025970 File Offset: 0x00024970
		public int AutoCheckThisBoxWhenOtherControlModified_cid
		{
			get
			{
				return this.autoCheckThisBoxWhenOtherControlModified_cid;
			}
			set
			{
				this.autoCheckThisBoxWhenOtherControlModified_cid = value;
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0002597A File Offset: 0x0002497A
		public void HidePrimary()
		{
			this.rb.Visible = false;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0002598A File Offset: 0x0002498A
		public void HideSecondary()
		{
			this.chk.Visible = false;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0002599C File Offset: 0x0002499C
		public MyCheckBox MyCheckbox
		{
			get
			{
				return this.chk;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x000259B4 File Offset: 0x000249B4
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x000259D4 File Offset: 0x000249D4
		public bool ReadOnlyPrimary
		{
			get
			{
				return !this.rb.Enabled;
			}
			set
			{
				this.rb.Enabled = !value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000259E8 File Offset: 0x000249E8
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00025A08 File Offset: 0x00024A08
		public bool ReadOnlySecondary
		{
			get
			{
				return !this.chk.Enabled;
			}
			set
			{
				this.chk.Enabled = !value;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00025A1C File Offset: 0x00024A1C
		private void AutoSizeMe()
		{
			if (this.singleCheckboxHeight == 0)
			{
				this.singleCheckboxHeight = this.chk.Height;
			}
			Graphics graphics = base.CreateGraphics();
			SizeF layoutArea = new SizeF((float)(this.chk.Width - 25), 100000f);
			StringFormat stringFormat = new StringFormat();
			int num;
			int num2;
			SizeF sizeF = graphics.MeasureString(this.chk.Text, this.chk.Font, layoutArea, stringFormat, out num, out num2);
			int num3 = this.singleCheckboxHeight * num2 + (base.Height - this.chk.Height);
			if (num3 < this.singleCheckboxHeight)
			{
				num3 = this.singleCheckboxHeight;
			}
			if (num3 > 0)
			{
				base.Height = num3;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00025AEC File Offset: 0x00024AEC
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00025B09 File Offset: 0x00024B09
		public bool Checked
		{
			get
			{
				return this.chk.Checked;
			}
			set
			{
				this.chk.Checked = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00025B1C File Offset: 0x00024B1C
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00025B39 File Offset: 0x00024B39
		public bool PrimaryChecked
		{
			get
			{
				return this.rb.Checked;
			}
			set
			{
				this.rb.Checked = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00025B4C File Offset: 0x00024B4C
		public bool FilledIn
		{
			get
			{
				return this.Checked || this.PrimaryChecked;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00025B6F File Offset: 0x00024B6F
		private void checkBoxX2_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00025B74 File Offset: 0x00024B74
		public bool PrimaryEquals(MyRadioButton rbtn)
		{
			return this.rb == rbtn;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00025B90 File Offset: 0x00024B90
		private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.allowBoth)
			{
				if (this.chk.Checked && this.rb.Checked)
				{
					this.chk.Checked = false;
				}
				else if (this.autoCheckThisBoxWhenOtherControlModified_cid > 0)
				{
					if (!this.chk.Checked)
					{
						try
						{
							Control parent = base.Parent;
							foreach (object obj in parent.Controls)
							{
								Control control = (Control)obj;
								if (control.Tag is DataRow)
								{
									DataRow dataRow = (DataRow)control.Tag;
									int num = (int)dataRow[0];
									if (num == this.autoCheckThisBoxWhenOtherControlModified_cid)
									{
										if (control is AutoComboBox)
										{
											AutoComboBox autoComboBox = (AutoComboBox)control;
											if (autoComboBox.SelectedIndex >= 0)
											{
												autoComboBox.SelectedIndex = -1;
											}
										}
										else if (control is TextBox)
										{
											TextBox textBox = (TextBox)control;
											if (textBox.Text.Length > 0)
											{
												textBox.Text = "";
											}
										}
										break;
									}
								}
							}
						}
						catch
						{
						}
					}
					else
					{
						try
						{
							Control parent = base.Parent;
							foreach (object obj2 in parent.Controls)
							{
								Control control = (Control)obj2;
								if (control.Tag is DataRow)
								{
									DataRow dataRow = (DataRow)control.Tag;
									int num = (int)dataRow[0];
									if (num == this.autoCheckThisBoxWhenOtherControlModified_cid)
									{
										if (control is CheckBox)
										{
											CheckBox checkBox = (CheckBox)control;
											this.chk.Checked = false;
										}
										else if (control is MyRadioGroupPrimaryCheckboxMultiple)
										{
											MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control;
											myRadioGroupPrimaryCheckboxMultiple.Checked = false;
										}
										break;
									}
								}
							}
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00025EA4 File Offset: 0x00024EA4
		private void myRadioButton1_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rb.Checked)
			{
				Control parent = base.Parent;
				foreach (object obj in parent.Controls)
				{
					Control control = (Control)obj;
					if (control is MyRadioGroupPrimaryCheckboxMultiple)
					{
						MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control;
						if (myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked && myRadioGroupPrimaryCheckboxMultiple != this)
						{
							myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked = false;
						}
					}
				}
				if (!this.allowBoth && this.chk.Checked)
				{
					this.chk.Checked = false;
				}
			}
		}

		// Token: 0x040003F3 RID: 1011
		private IContainer components = null;

		// Token: 0x040003F4 RID: 1012
		private MyCheckBox chk;

		// Token: 0x040003F5 RID: 1013
		private MyRadioButton rb;

		// Token: 0x040003F6 RID: 1014
		private int singleCheckboxHeight = 0;

		// Token: 0x040003F7 RID: 1015
		private bool allowBoth = false;

		// Token: 0x040003F8 RID: 1016
		private int autoCheckThisBoxWhenOtherControlModified_cid = 0;
	}
}
