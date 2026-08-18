using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000047 RID: 71
	public class AccommodationControl2 : UserControl, MyDynamicControl
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00016298 File Offset: 0x00015298
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000162D0 File Offset: 0x000152D0
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AccommodationControl2));
			this.lbl = new Label();
			this.btn = new Button();
			this.chk = new CheckBox();
			this.cmb = new AutoComboBox();
			this.txt = new TextBox();
			this.dtp = new MyDateTimePicker();
			base.SuspendLayout();
			this.lbl.BackColor = SystemColors.Info;
			this.lbl.BorderStyle = BorderStyle.FixedSingle;
			this.lbl.Dock = DockStyle.Right;
			this.lbl.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbl.ForeColor = SystemColors.InfoText;
			this.lbl.Location = new Point(485, 1);
			this.lbl.Name = "lbl";
			this.lbl.Size = new Size(143, 30);
			this.lbl.TabIndex = 22;
			this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn.AccessibleDescription = "Open popup window editor for this information";
			this.btn.AccessibleName = "Open popup window editor for this information";
			this.btn.BackColor = SystemColors.Control;
			this.btn.BackgroundImage = (Image)componentResourceManager.GetObject("btn.BackgroundImage");
			this.btn.BackgroundImageLayout = ImageLayout.Center;
			this.btn.Dock = DockStyle.Right;
			this.btn.FlatStyle = FlatStyle.Flat;
			this.btn.ForeColor = SystemColors.Control;
			this.btn.Location = new Point(628, 1);
			this.btn.Name = "btn";
			this.btn.Size = new Size(18, 30);
			this.btn.TabIndex = 31;
			this.btn.TabStop = false;
			this.btn.UseVisualStyleBackColor = false;
			this.btn.Click += this.btn_Click;
			this.chk.Dock = DockStyle.Left;
			this.chk.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chk.Location = new Point(1, 1);
			this.chk.Name = "chk";
			this.chk.Size = new Size(184, 30);
			this.chk.TabIndex = 33;
			this.chk.Text = "Accommodation";
			this.chk.UseVisualStyleBackColor = true;
			this.chk.CheckedChanged += this.chk_CheckedChanged;
			this.cmb.AccessibleRole = AccessibleRole.ComboBox;
			this.cmb.AllowUserToEnterAnyText = true;
			this.cmb.AltValueMember = null;
			this.cmb.AutoCompleteEnabled = true;
			this.cmb.CalcButtonCid = 0;
			this.cmb.ChildLookupGroupId = 0;
			this.cmb.CidToNotifyWithValueMember = 0;
			this.cmb.Dock = DockStyle.Fill;
			this.cmb.FormattingEnabled = true;
			this.cmb.GotoNextItemOnDoubleClick = false;
			this.cmb.IgnoreScrollWheel = true;
			this.cmb.Location = new Point(185, 1);
			this.cmb.LookupGroupId = 0;
			this.cmb.Name = "cmb";
			this.cmb.Size = new Size(300, 21);
			this.cmb.TabIndex = 34;
			this.cmb.TryToSelectOnFocusLeave = true;
			this.cmb.Visible = false;
			this.cmb.Leave += this.cmb_Leave;
			this.txt.Dock = DockStyle.Fill;
			this.txt.Location = new Point(185, 1);
			this.txt.Name = "txt";
			this.txt.Size = new Size(300, 20);
			this.txt.TabIndex = 35;
			this.txt.Visible = false;
			this.txt.TextChanged += this.txt_TextChanged;
			this.dtp.BaseValue = new DateTime(2009, 4, 16, 18, 51, 16, 335);
			this.dtp.CustomFormat = "MMMM dd, yyyy";
			this.dtp.Dock = DockStyle.Fill;
			this.dtp.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.dtp.Format = DateTimePickerFormat.Custom;
			this.dtp.GreyedOut = false;
			this.dtp.Location = new Point(185, 1);
			this.dtp.Name = "dtp";
			this.dtp.Size = new Size(300, 22);
			this.dtp.TabIndex = 36;
			this.dtp.Value = new DateTime(2009, 4, 16, 18, 51, 16, 335);
			this.dtp.Visible = false;
			this.dtp.ValueChanged += this.dtp_ValueChanged;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.txt);
			base.Controls.Add(this.dtp);
			base.Controls.Add(this.cmb);
			base.Controls.Add(this.chk);
			base.Controls.Add(this.lbl);
			base.Controls.Add(this.btn);
			base.Name = "AccommodationControl2";
			base.Padding = new Padding(1);
			base.Size = new Size(647, 32);
			base.Resize += this.AccommodationControl2_Resize;
			base.SizeChanged += this.AccommodationControl2_SizeChanged;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00016938 File Offset: 0x00015938
		public AccommodationControl2()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00016990 File Offset: 0x00015990
		// (set) Token: 0x060002AF RID: 687 RVA: 0x000169A8 File Offset: 0x000159A8
		public bool Approved
		{
			get
			{
				return this.approved;
			}
			set
			{
				this.approved = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000169B4 File Offset: 0x000159B4
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x000169CC File Offset: 0x000159CC
		public bool RecommendedToStudentButDeclined
		{
			get
			{
				return this.recommendedToStudentButDeclined;
			}
			set
			{
				this.recommendedToStudentButDeclined = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000169D8 File Offset: 0x000159D8
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000169F0 File Offset: 0x000159F0
		public string RecommendedToStudentButDeclinedDetail
		{
			get
			{
				return this.recommendedToStudentButDeclinedDetail;
			}
			set
			{
				this.recommendedToStudentButDeclinedDetail = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000169FC File Offset: 0x000159FC
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00016A14 File Offset: 0x00015A14
		public int DefaultShowOnLetter
		{
			get
			{
				return this.defaultShowOnLetter;
			}
			set
			{
				this.defaultShowOnLetter = value;
				this.showOnLetter = (this.defaultShowOnLetter > 0);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00016A30 File Offset: 0x00015A30
		public bool FilledIn
		{
			get
			{
				bool result;
				switch (this.accommodationControlType)
				{
				case AccommodationControlType.CheckBox:
					result = this.Chk_caption.Checked;
					break;
				case AccommodationControlType.TextBox:
					result = (this.txt.Text.Trim().Length > 0);
					break;
				case AccommodationControlType.ComboBoxSimple:
				{
					DataRow dataRow = this.cmb.SelectedDataRow();
					result = (dataRow != null);
					break;
				}
				case AccommodationControlType.ComboText:
					result = (this.cmb.Text.Trim().Length > 0);
					break;
				case AccommodationControlType.Date:
					result = (this.dtp.Value != DateTime.MinValue);
					break;
				default:
					result = false;
					break;
				}
				return result;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00016ADA File Offset: 0x00015ADA
		public void FromString(string s)
		{
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00016AE0 File Offset: 0x00015AE0
		public object ReportObject
		{
			get
			{
				object result;
				switch (this.accommodationControlType)
				{
				case AccommodationControlType.CheckBox:
					result = this.Chk_caption.Checked;
					break;
				case AccommodationControlType.TextBox:
					result = (this.txt.Text.Trim().Length > 0);
					break;
				case AccommodationControlType.ComboBoxSimple:
				{
					DataRow dataRow = this.cmb.SelectedDataRow();
					result = (dataRow != null);
					break;
				}
				case AccommodationControlType.ComboText:
					result = (this.cmb.Text.Trim().Length > 0);
					break;
				case AccommodationControlType.Date:
					result = (this.dtp.Value != DateTime.MinValue);
					break;
				default:
					result = false;
					break;
				}
				return result;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00016BB0 File Offset: 0x00015BB0
		public CheckBox GetCheckBox()
		{
			return this.chk;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00016BC8 File Offset: 0x00015BC8
		public TextBox GetTextBox()
		{
			return this.txt;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00016BE0 File Offset: 0x00015BE0
		public AutoComboBox GetDropList()
		{
			return this.cmb;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00016BF8 File Offset: 0x00015BF8
		public MyDateTimePicker GetDateTimePicker()
		{
			return this.dtp;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00016C10 File Offset: 0x00015C10
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00016C28 File Offset: 0x00015C28
		public string ValueText
		{
			get
			{
				switch (this.accommodationControlType)
				{
				case AccommodationControlType.TextBox:
					return this.txt.Text;
				case AccommodationControlType.ComboBoxSimple:
				{
					DataRow dataRow = this.cmb.SelectedDataRow();
					if (dataRow != null)
					{
						return dataRow[1].ToString();
					}
					break;
				}
				case AccommodationControlType.ComboText:
				{
					string text = this.cmb.Text;
					if (text.Trim().Length > 0)
					{
						return text;
					}
					break;
				}
				case AccommodationControlType.Date:
					if (this.dtp.Value != DateTime.MinValue)
					{
						return this.dtp.Value.ToString("MMMM d, yyyy");
					}
					break;
				}
				return "";
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00016D04 File Offset: 0x00015D04
		public string GetDataWithValueText()
		{
			return this.GetDataWithValueText(this.ValueText);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00016D24 File Offset: 0x00015D24
		public string GetDataWithValueTextAndSummaryHtml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.GetDataWithValueText());
			StringBuilder summary = this.GetSummary();
			if (summary.Length > 0)
			{
				stringBuilder.Append(string.Format(" <span style='font-style:italic;'>{0}</span>", summary.ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00016D80 File Offset: 0x00015D80
		public string GetDataWithValueText(string valueText)
		{
			string text = this.Chk_caption.Text;
			if (valueText.Length > 0)
			{
				text = text + ": " + this.ValueText;
			}
			return text;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00016DC4 File Offset: 0x00015DC4
		public void Reset()
		{
			this.Offline = false;
			this.ShowOnLetter = (this.defaultShowOnLetter > 0);
			this.TextForLetter = "";
			this.PrivateNote = "";
			this.ExpiryDate = DateTime.MinValue;
			this.recommendedToStudentButDeclined = false;
			this.recommendedToStudentButDeclinedDetail = "";
			this.approved = false;
			if (this.chk.Visible)
			{
				this.chk.Checked = false;
			}
			if (this.cmb.Visible)
			{
				this.cmb.SelectedIndex = -1;
				this.cmb.BringToFront();
			}
			if (this.dtp.Visible)
			{
				this.dtp.Value = DateTime.MinValue;
				this.dtp.BringToFront();
			}
			if (this.txt.Visible)
			{
				this.txt.Text = "";
				this.txt.BringToFront();
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00016ED8 File Offset: 0x00015ED8
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00016EF0 File Offset: 0x00015EF0
		public bool Offline
		{
			get
			{
				return this.offline;
			}
			set
			{
				this.offline = value;
				this.UpdateLabelDisplay();
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00016F04 File Offset: 0x00015F04
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00016F1C File Offset: 0x00015F1C
		public bool ShowOnLetter
		{
			get
			{
				return this.showOnLetter;
			}
			set
			{
				this.showOnLetter = value;
				this.UpdateLabelDisplay();
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00016F30 File Offset: 0x00015F30
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00016F48 File Offset: 0x00015F48
		public string TextForLetter
		{
			get
			{
				return this.textForLetter;
			}
			set
			{
				this.textForLetter = value;
				this.UpdateLabelDisplay();
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00016F5C File Offset: 0x00015F5C
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00016F74 File Offset: 0x00015F74
		public string PrivateNote
		{
			get
			{
				return this.privateNote;
			}
			set
			{
				this.privateNote = value;
				this.UpdateLabelDisplay();
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00016F88 File Offset: 0x00015F88
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00016FA0 File Offset: 0x00015FA0
		public DateTime ExpiryDate
		{
			get
			{
				return this.expiryDate;
			}
			set
			{
				this.expiryDate = value;
				this.UpdateLabelDisplay();
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00016FB1 File Offset: 0x00015FB1
		public void SetForeColour(Color c)
		{
			this.chk.ForeColor = c;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00016FC4 File Offset: 0x00015FC4
		public CheckBox Chk_caption
		{
			get
			{
				return this.chk;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00016FDC File Offset: 0x00015FDC
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00016FF4 File Offset: 0x00015FF4
		public AccommodationControlType AccommodationControlType
		{
			get
			{
				return this.accommodationControlType;
			}
			set
			{
				this.accommodationControlType = value;
				switch (this.accommodationControlType)
				{
				case AccommodationControlType.TextBox:
					this.txt.Visible = true;
					this.txt.BringToFront();
					break;
				case AccommodationControlType.ComboBoxSimple:
					this.cmb.Visible = true;
					this.cmb.BringToFront();
					break;
				case AccommodationControlType.ComboText:
					this.cmb.Visible = true;
					this.cmb.BringToFront();
					break;
				case AccommodationControlType.Date:
					this.dtp.Visible = true;
					this.dtp.BringToFront();
					break;
				}
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001709C File Offset: 0x0001609C
		public void SetReadOnly()
		{
			this.chk.Enabled = false;
			this.btn.Enabled = false;
			this.lbl.Visible = false;
			if (this.txt.Visible)
			{
				this.txt.ReadOnly = true;
			}
			else
			{
				base.Enabled = false;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00017100 File Offset: 0x00016100
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00017120 File Offset: 0x00016120
		public string Caption
		{
			get
			{
				return this.chk.Text;
			}
			set
			{
				this.chk.Text = value;
				if (this.chk.Text.Trim().Length > 0)
				{
					using (Graphics graphics = base.CreateGraphics())
					{
						int width = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal).Width;
						int num = this.chk.Width - this.chk.Padding.Left - this.chk.Padding.Right;
						int num2 = Convert.ToInt32(graphics.MeasureString(this.chk.Text, this.chk.Font, num - width - 2).Height);
						if (this.defaultLblHeight == 0)
						{
							this.defaultLblHeight = this.lbl.Height;
						}
						if (num2 > this.defaultLblHeight)
						{
							base.Height = num2;
						}
					}
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00017248 File Offset: 0x00016248
		public void SetIndent(int indent)
		{
			this.chk.Padding = new Padding(indent, this.chk.Padding.Top, this.chk.Padding.Right, this.chk.Padding.Bottom);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000172A4 File Offset: 0x000162A4
		private void AccommodationControl2_SizeChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000172B4 File Offset: 0x000162B4
		private void btn_Click(object sender, EventArgs e)
		{
			AccommodationControlPopup accommodationControlPopup = new AccommodationControlPopup(this.accommodationControlType, this.chk.Text, this.chk.Checked, this.textForLetter, this.offline, this.showOnLetter, this.expiryDate, this.privateNote, this.recommendedToStudentButDeclined, this.recommendedToStudentButDeclinedDetail, this.approved);
			DialogResult dialogResult = accommodationControlPopup.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				this.chk.Checked = true;
				this.textForLetter = accommodationControlPopup.LetterText;
				this.offline = accommodationControlPopup.Offline;
				this.showOnLetter = accommodationControlPopup.Letter;
				this.expiryDate = accommodationControlPopup.ExpiryDate;
				this.privateNote = accommodationControlPopup.PrivateNote;
				this.approved = accommodationControlPopup.Approved;
				this.recommendedToStudentButDeclined = accommodationControlPopup.RecommendedToStudentButDeclined;
				this.recommendedToStudentButDeclinedDetail = accommodationControlPopup.RecommendedToStudentButDeclinedDetail;
				this.UpdateLabelDisplay();
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000173A0 File Offset: 0x000163A0
		private StringBuilder GetSummary()
		{
			List<string> list = new List<string>();
			if (this.offline)
			{
				list.Add("Offline");
			}
			if (!this.showOnLetter)
			{
				list.Add("Not on letter");
			}
			if (this.expiryDate != DateTime.MinValue)
			{
				if (this.expiryDate < DateTime.Now)
				{
					list.Add("Expired");
				}
				else
				{
					list.Add("Expires on " + this.expiryDate.ToString("yyyy-MM-dd"));
				}
			}
			int num = this.textForLetter.Length;
			if (num > 0)
			{
				if (num > 10)
				{
					num = 10;
				}
				list.Add("Letter [" + this.textForLetter.Substring(0, num) + "]");
			}
			num = this.privateNote.Length;
			if (num > 0)
			{
				if (num > 10)
				{
					num = 10;
				}
				list.Add("Private [" + this.privateNote.Substring(0, num) + "]");
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(list[i]);
			}
			return stringBuilder;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00017538 File Offset: 0x00016538
		private void UpdateLabelDisplay()
		{
			StringBuilder summary = this.GetSummary();
			this.lbl.Text = summary.ToString();
			this.lbl.Visible = (this.lbl.Text.Length > 0);
			if (this.lbl.Visible)
			{
				this.lbl.BringToFront();
			}
			if (this.txt.Visible)
			{
				this.txt.BringToFront();
			}
			else if (this.cmb.Visible)
			{
				this.cmb.BringToFront();
			}
			else if (this.dtp.Visible)
			{
				this.dtp.BringToFront();
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000175FC File Offset: 0x000165FC
		private bool IsExtraControlVisible()
		{
			return this.cmb.Visible || this.dtp.Visible || this.txt.Visible;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00017638 File Offset: 0x00016638
		public string Text2
		{
			get
			{
				string result;
				if (this.cmb.Visible)
				{
					result = this.cmb.SelectedText;
				}
				else if (this.dtp.Visible)
				{
					result = this.dtp.Value.ToString("yyyy-MM-dd");
				}
				else if (this.txt.Visible)
				{
					result = this.txt.Text;
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000176C0 File Offset: 0x000166C0
		public int CmbValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000176D4 File Offset: 0x000166D4
		public string CmbText
		{
			get
			{
				return this.cmb.Text;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000176F4 File Offset: 0x000166F4
		public DateTime DtpValue
		{
			get
			{
				return this.dtp.Value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00017714 File Offset: 0x00016714
		public AutoComboBox Cmb
		{
			get
			{
				return this.cmb;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0001772C File Offset: 0x0001672C
		public TextBox Txt
		{
			get
			{
				return this.txt;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00017744 File Offset: 0x00016744
		public MyDateTimePicker Dtp
		{
			get
			{
				return this.dtp;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0001775C File Offset: 0x0001675C
		public bool Checked
		{
			get
			{
				return this.chk.Checked;
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001777C File Offset: 0x0001677C
		private void AccommodationControl2_Resize(object sender, EventArgs e)
		{
			double num = Convert.ToDouble(base.Bounds.Width - this.btn.Width);
			double num2 = this.IsExtraControlVisible() ? 3.0 : 2.4;
			int num3 = Convert.ToInt32(num / num2) - 2;
			int num4 = num3;
			if (num3 > 0)
			{
				this.chk.Width = num3;
			}
			else
			{
				this.chk.Width = 5;
			}
			if (num4 > 0)
			{
				this.lbl.Width = num4;
			}
			else
			{
				this.lbl.Width = 5;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00017828 File Offset: 0x00016828
		private void chk_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chk.Checked)
			{
				switch (this.accommodationControlType)
				{
				case AccommodationControlType.TextBox:
					this.txt.Enabled = true;
					break;
				case AccommodationControlType.ComboBoxSimple:
					this.cmb.Enabled = true;
					break;
				case AccommodationControlType.ComboText:
					this.cmb.Enabled = true;
					break;
				case AccommodationControlType.Date:
					this.dtp.Enabled = true;
					break;
				}
			}
			else
			{
				switch (this.accommodationControlType)
				{
				case AccommodationControlType.TextBox:
					this.txt.Enabled = false;
					break;
				case AccommodationControlType.ComboBoxSimple:
					this.cmb.Enabled = false;
					break;
				case AccommodationControlType.ComboText:
					this.cmb.Enabled = false;
					break;
				case AccommodationControlType.Date:
					this.dtp.Enabled = false;
					break;
				}
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00017908 File Offset: 0x00016908
		private void cmb_Leave(object sender, EventArgs e)
		{
			if (this.cmb.SelectedText.Trim().Length > 0 && !this.chk.Checked)
			{
				this.chk.Checked = true;
			}
			else
			{
				DataRow dataRow = this.cmb.SelectedDataRow();
				if (dataRow != null && !this.chk.Checked)
				{
					this.chk.Checked = true;
				}
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00017984 File Offset: 0x00016984
		private void txt_TextChanged(object sender, EventArgs e)
		{
			if (this.txt.Text.Trim().Length > 0 && !this.chk.Checked)
			{
				this.chk.Checked = true;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000179CC File Offset: 0x000169CC
		private void dtp_ValueChanged(object sender, EventArgs e)
		{
			if (this.dtp.Value != DateTime.MinValue && !this.chk.Checked)
			{
				this.chk.Checked = true;
			}
		}

		// Token: 0x0400021E RID: 542
		private IContainer components = null;

		// Token: 0x0400021F RID: 543
		private Label lbl;

		// Token: 0x04000220 RID: 544
		private Button btn;

		// Token: 0x04000221 RID: 545
		private CheckBox chk;

		// Token: 0x04000222 RID: 546
		private AutoComboBox cmb;

		// Token: 0x04000223 RID: 547
		private TextBox txt;

		// Token: 0x04000224 RID: 548
		private MyDateTimePicker dtp;

		// Token: 0x04000225 RID: 549
		private AccommodationControlType accommodationControlType;

		// Token: 0x04000226 RID: 550
		private bool offline = false;

		// Token: 0x04000227 RID: 551
		private bool showOnLetter = true;

		// Token: 0x04000228 RID: 552
		private bool approved = false;

		// Token: 0x04000229 RID: 553
		private string textForLetter = "";

		// Token: 0x0400022A RID: 554
		private string privateNote = "";

		// Token: 0x0400022B RID: 555
		private DateTime expiryDate;

		// Token: 0x0400022C RID: 556
		private int defaultShowOnLetter;

		// Token: 0x0400022D RID: 557
		private bool recommendedToStudentButDeclined;

		// Token: 0x0400022E RID: 558
		private string recommendedToStudentButDeclinedDetail;

		// Token: 0x0400022F RID: 559
		private int defaultLblHeight = 0;
	}
}
