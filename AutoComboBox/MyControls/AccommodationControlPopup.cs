using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x0200005E RID: 94
	public partial class AccommodationControlPopup : Form
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0001A2D4 File Offset: 0x000192D4
		public AccommodationControlPopup()
		{
			this.InitializeComponent();
			this.dtp_expiry.Value = DateTime.Now.AddMonths(4);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001A314 File Offset: 0x00019314
		public AccommodationControlPopup(AccommodationControlType controlType, string caption, bool captionChecked, string textForLetter, bool offline, bool letter, DateTime expiryDate, string privateNote, bool recommendedToStudentButDeclined, string recommendedToStudentButDeclinedDetail, bool approved)
		{
			this.InitializeComponent();
			this.dtp_expiry.Value = DateTime.Now.AddMonths(4);
			this.Text = "Accommodation: " + caption;
			this.chk_caption.Checked = true;
			this.txt_letterText.Text = textForLetter;
			this.chk_offline.Checked = offline;
			this.chk_letter.Checked = letter;
			this.dtp_expiry.Value = expiryDate;
			this.txt_rationale.Text = privateNote;
			this.chk_approved.Checked = approved;
			this.chk_recommendedToStudentButDeclined.Checked = recommendedToStudentButDeclined;
			this.txt_recommendedToStudentButDeclinedDetail.Text = recommendedToStudentButDeclinedDetail;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0001A3E4 File Offset: 0x000193E4
		public bool CaptionChecked
		{
			get
			{
				return this.chk_caption.Checked;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0001A404 File Offset: 0x00019404
		public string LetterText
		{
			get
			{
				return this.txt_letterText.Text;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0001A424 File Offset: 0x00019424
		public bool Offline
		{
			get
			{
				return this.chk_offline.Checked;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0001A444 File Offset: 0x00019444
		public bool Letter
		{
			get
			{
				return this.chk_letter.Checked;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001A464 File Offset: 0x00019464
		public DateTime ExpiryDate
		{
			get
			{
				return this.dtp_expiry.Value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0001A484 File Offset: 0x00019484
		public string PrivateNote
		{
			get
			{
				return this.txt_rationale.Text;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0001A4A4 File Offset: 0x000194A4
		public bool Approved
		{
			get
			{
				return this.chk_approved.Checked;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0001A4C4 File Offset: 0x000194C4
		public bool RecommendedToStudentButDeclined
		{
			get
			{
				return this.chk_recommendedToStudentButDeclined.Checked;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0001A4E4 File Offset: 0x000194E4
		public string RecommendedToStudentButDeclinedDetail
		{
			get
			{
				return this.txt_recommendedToStudentButDeclinedDetail.Text;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001A501 File Offset: 0x00019501
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001A50B File Offset: 0x0001950B
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001A51D File Offset: 0x0001951D
		private void AccommodationControlPopup_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001A520 File Offset: 0x00019520
		private void chk_caption_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateEnabled();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001A52C File Offset: 0x0001952C
		private void UpdateEnabled()
		{
			bool @checked = this.chk_caption.Checked;
			this.txt_letterText.Enabled = @checked;
			this.chk_offline.Enabled = @checked;
			this.chk_letter.Enabled = @checked;
			this.dtp_expiry.Enabled = @checked;
			this.btn_clearExpiry.Enabled = @checked;
			this.txt_rationale.Enabled = @checked;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001A594 File Offset: 0x00019594
		private void btn_clearExpiry_Click(object sender, EventArgs e)
		{
			this.dtp_expiry.Value = DateTime.MinValue;
		}
	}
}
