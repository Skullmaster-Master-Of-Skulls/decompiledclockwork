using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CustomControl.OrientAbleTextControls;

namespace AutoComboBox.MyControls
{
	// Token: 0x0200000F RID: 15
	public class MyRadioGroupPrimary : UserControl, MyDynamicControl
	{
		// Token: 0x06000044 RID: 68 RVA: 0x0000369C File Offset: 0x0000269C
		public new string ToString()
		{
			return "Not supported";
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000036B3 File Offset: 0x000026B3
		public void FromString(string s)
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000036B8 File Offset: 0x000026B8
		public object ReportObject
		{
			get
			{
				return this.SelectedIntValue;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000036D5 File Offset: 0x000026D5
		public MyRadioGroupPrimary()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000036FC File Offset: 0x000026FC
		public MyRadioGroupPrimary(RadioGroupPrimaryType PrimaryType)
		{
			this.primaryType = PrimaryType;
			this.InitializeComponent();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000372C File Offset: 0x0000272C
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00003744 File Offset: 0x00002744
		public bool ReadOnlyPrimary
		{
			get
			{
				return this.readOnlyPrimary;
			}
			set
			{
				this.readOnlyPrimary = value;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000374E File Offset: 0x0000274E
		private void orientedTextLabel2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003754 File Offset: 0x00002754
		public bool FilledIn
		{
			get
			{
				return this.SelectedIntValue > -1;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003770 File Offset: 0x00002770
		public string SelectedText
		{
			get
			{
				Control parent = base.Parent;
				if (parent != null)
				{
					foreach (object obj in parent.Controls)
					{
						Control control = (Control)obj;
						if (control is MyRadioGroupPrimaryCheckboxMultiple)
						{
							MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control;
							if (myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked && myRadioGroupPrimaryCheckboxMultiple.Tag is DataRow)
							{
								DataRow dataRow = (DataRow)myRadioGroupPrimaryCheckboxMultiple.Tag;
								string text = dataRow["controlcaption"].ToString();
								int num = text.IndexOf("~~");
								if (num > 0)
								{
									text = text.Substring(0, num);
								}
								return text;
							}
						}
					}
				}
				return "";
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000388C File Offset: 0x0000288C
		public void Clear()
		{
			Control parent = base.Parent;
			if (parent != null)
			{
				foreach (object obj in parent.Controls)
				{
					Control control = (Control)obj;
					if (control is MyRadioGroupPrimaryCheckboxMultiple)
					{
						MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control;
						myRadioGroupPrimaryCheckboxMultiple.Checked = false;
						myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked = false;
					}
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000392C File Offset: 0x0000292C
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003A18 File Offset: 0x00002A18
		public int SelectedIntValue
		{
			get
			{
				Control parent = base.Parent;
				if (parent != null)
				{
					foreach (object obj in parent.Controls)
					{
						Control control = (Control)obj;
						if (control is MyRadioGroupPrimaryCheckboxMultiple)
						{
							MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control;
							if (myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked && myRadioGroupPrimaryCheckboxMultiple.Tag is DataRow)
							{
								DataRow dataRow = (DataRow)myRadioGroupPrimaryCheckboxMultiple.Tag;
								return (int)dataRow["controlid"];
							}
						}
					}
				}
				return -1;
			}
			set
			{
				Control parent = base.Parent;
				if (parent != null)
				{
					foreach (object obj in parent.Controls)
					{
						Control control = (Control)obj;
						if (control is MyRadioGroupPrimaryCheckboxMultiple)
						{
							MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)control;
							if (myRadioGroupPrimaryCheckboxMultiple.Tag is DataRow)
							{
								DataRow dataRow = (DataRow)myRadioGroupPrimaryCheckboxMultiple.Tag;
								int num = (int)dataRow["controlid"];
								if (num == value)
								{
									myRadioGroupPrimaryCheckboxMultiple.PrimaryChecked = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003B08 File Offset: 0x00002B08
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003B40 File Offset: 0x00002B40
		private void InitializeComponent()
		{
			this.orientedTextLabel2 = new OrientedTextLabel();
			this.orientedTextLabel1 = new OrientedTextLabel();
			base.SuspendLayout();
			this.orientedTextLabel2.Dock = DockStyle.Left;
			this.orientedTextLabel2.FlatStyle = FlatStyle.Flat;
			this.orientedTextLabel2.Font = new Font("Arial Narrow", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.orientedTextLabel2.Location = new Point(35, 0);
			this.orientedTextLabel2.Name = "orientedTextLabel2";
			this.orientedTextLabel2.RotationAngle = -60.0;
			this.orientedTextLabel2.Size = new Size(42, 62);
			this.orientedTextLabel2.TabIndex = 1;
			this.orientedTextLabel2.Text = "Secondary";
			this.orientedTextLabel2.TextDirection = Direction.AntiClockwise;
			this.orientedTextLabel2.TextOrientation = CustomControl.OrientAbleTextControls.Orientation.Rotate;
			this.orientedTextLabel2.Click += this.orientedTextLabel2_Click;
			this.orientedTextLabel1.BackColor = Color.Transparent;
			this.orientedTextLabel1.Dock = DockStyle.Left;
			this.orientedTextLabel1.Font = new Font("Arial Narrow", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.orientedTextLabel1.Location = new Point(0, 0);
			this.orientedTextLabel1.Name = "orientedTextLabel1";
			this.orientedTextLabel1.RotationAngle = -60.0;
			this.orientedTextLabel1.Size = new Size(35, 62);
			this.orientedTextLabel1.TabIndex = 0;
			this.orientedTextLabel1.Text = "Primary";
			this.orientedTextLabel1.TextAlign = ContentAlignment.BottomCenter;
			this.orientedTextLabel1.TextDirection = Direction.AntiClockwise;
			this.orientedTextLabel1.TextOrientation = CustomControl.OrientAbleTextControls.Orientation.Rotate;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.orientedTextLabel2);
			base.Controls.Add(this.orientedTextLabel1);
			base.Name = "MyRadioGroupPrimary";
			base.Size = new Size(320, 62);
			base.ResumeLayout(false);
		}

		// Token: 0x04000065 RID: 101
		private RadioGroupPrimaryType primaryType = RadioGroupPrimaryType.PrimaryAndSecondary;

		// Token: 0x04000066 RID: 102
		private bool readOnlyPrimary = false;

		// Token: 0x04000067 RID: 103
		private IContainer components = null;

		// Token: 0x04000068 RID: 104
		private OrientedTextLabel orientedTextLabel1;

		// Token: 0x04000069 RID: 105
		private OrientedTextLabel orientedTextLabel2;
	}
}
