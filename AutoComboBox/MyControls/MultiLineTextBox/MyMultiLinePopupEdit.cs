using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox.MyControls.MultiLineTextBox
{
	// Token: 0x02000045 RID: 69
	public class MyMultiLinePopupEdit : UserControl
	{
		// Token: 0x06000285 RID: 645 RVA: 0x000151F4 File Offset: 0x000141F4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0001522C File Offset: 0x0001422C
		private void InitializeComponent()
		{
			this.txt = new NTextBox();
			this.toolStrip1 = new ToolStrip();
			this.btn_spellCheck = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.btn_close = new ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.txt.Dock = DockStyle.Fill;
			this.txt.Location = new Point(2, 2);
			this.txt.Margin = new Padding(3, 4, 3, 4);
			this.txt.Multiline = true;
			this.txt.Name = "txt";
			this.txt.ScrollBars = ScrollBars.Vertical;
			this.txt.Size = new Size(466, 271);
			this.txt.TabIndex = 0;
			this.txt.KeyDown += this.txt_KeyDown;
			this.toolStrip1.Dock = DockStyle.Bottom;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btn_spellCheck,
				this.toolStripSeparator1,
				this.btn_close
			});
			this.toolStrip1.Location = new Point(2, 273);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(466, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_spellCheck.Image = Resources.spellcheck;
			this.btn_spellCheck.ImageTransparentColor = Color.Magenta;
			this.btn_spellCheck.Name = "btn_spellCheck";
			this.btn_spellCheck.Size = new Size(86, 22);
			this.btn_spellCheck.Text = "Spe&ll check";
			this.btn_spellCheck.Click += this.btn_spellCheck_Click;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.btn_close.Image = Resources.delete2;
			this.btn_close.ImageTransparentColor = Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new Size(56, 22);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += this.btn_close_Click;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.BorderStyle = BorderStyle.Fixed3D;
			base.Controls.Add(this.txt);
			base.Controls.Add(this.toolStrip1);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "MyMultiLinePopupEdit";
			base.Padding = new Padding(2);
			base.Size = new Size(470, 300);
			base.KeyUp += this.MyMultiLinePopupEdit_KeyUp;
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000155B4 File Offset: 0x000145B4
		// (set) Token: 0x06000288 RID: 648 RVA: 0x000155CC File Offset: 0x000145CC
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
				this.txt.ReadOnly = value;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000155E3 File Offset: 0x000145E3
		public MyMultiLinePopupEdit()
		{
			this.InitializeComponent();
			this.txt.popup = this;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00015610 File Offset: 0x00014610
		private void MyMultiLinePopupEdit_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				e.Handled = true;
				this.HideMe();
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00015640 File Offset: 0x00014640
		protected override void OnLeave(EventArgs e)
		{
			this.HideMe();
			base.OnLeave(e);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00015652 File Offset: 0x00014652
		public void HideMe()
		{
			base.Hide();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0001565C File Offset: 0x0001465C
		public void ShowMe(MultiLineItem editingItem)
		{
			this.editingItem = editingItem;
			base.Show();
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00015670 File Offset: 0x00014670
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0001568D File Offset: 0x0001468D
		public int index
		{
			get
			{
				return this.txt.index;
			}
			set
			{
				this.txt.index = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0001569C File Offset: 0x0001469C
		// (set) Token: 0x06000291 RID: 657 RVA: 0x000156B9 File Offset: 0x000146B9
		public new string Text
		{
			get
			{
				return this.txt.Text;
			}
			set
			{
				this.txt.Text = value;
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000156C9 File Offset: 0x000146C9
		public void SelectAll()
		{
			this.txt.SelectAll();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000156D8 File Offset: 0x000146D8
		private void btn_close_Click(object sender, EventArgs e)
		{
			this.HideMe();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000156E2 File Offset: 0x000146E2
		private void btn_spellCheck_Click(object sender, EventArgs e)
		{
			this.txt.SpellCheck();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000156F1 File Offset: 0x000146F1
		public void ShowSpellCheck()
		{
			this.txt.ShowSpellChecker();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00015700 File Offset: 0x00014700
		private void txt_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Alt)
			{
				this.btn_close_Click(this.btn_close, new EventArgs());
			}
		}

		// Token: 0x04000209 RID: 521
		private IContainer components = null;

		// Token: 0x0400020A RID: 522
		private NTextBox txt;

		// Token: 0x0400020B RID: 523
		private ToolStrip toolStrip1;

		// Token: 0x0400020C RID: 524
		private ToolStripButton btn_spellCheck;

		// Token: 0x0400020D RID: 525
		private ToolStripButton btn_close;

		// Token: 0x0400020E RID: 526
		private ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400020F RID: 527
		public MyMultilineTextBox mllb;

		// Token: 0x04000210 RID: 528
		public MultiLineItem editingItem;

		// Token: 0x04000211 RID: 529
		private bool isReadOnly = false;
	}
}
