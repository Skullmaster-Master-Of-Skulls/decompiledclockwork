using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;
using DevComponents.DotNetBar.Controls;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000009 RID: 9
	public class SearchControl : UserControl
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002B14 File Offset: 0x00001B14
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B4C File Offset: 0x00001B4C
		private void InitializeComponent()
		{
			this.textBoxX1 = new TextBoxX();
			this.toolStrip1 = new ToolStrip();
			this.lbl_matches = new ToolStripLabel();
			this.btn_previous = new ToolStripButton();
			this.btn_next = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.btn_close = new ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.textBoxX1.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.textBoxX1.AutoCompleteSource = AutoCompleteSource.RecentlyUsedList;
			this.textBoxX1.Border.Class = "TextBoxBorder";
			this.textBoxX1.Dock = DockStyle.Fill;
			this.textBoxX1.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.textBoxX1.Location = new Point(4, 4);
			this.textBoxX1.Name = "textBoxX1";
			this.textBoxX1.Size = new Size(275, 21);
			this.textBoxX1.TabIndex = 1;
			this.textBoxX1.WatermarkBehavior = 1;
			this.textBoxX1.WatermarkText = "Enter a search string ...";
			this.textBoxX1.KeyDown += this.textBoxX1_KeyDown;
			this.textBoxX1.Leave += this.textBoxX1_Leave;
			this.toolStrip1.AccessibleDescription = "Search results information";
			this.toolStrip1.AccessibleName = "Search results information";
			this.toolStrip1.Dock = DockStyle.Right;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.lbl_matches,
				this.btn_previous,
				this.btn_next,
				this.toolStripSeparator1,
				this.btn_close
			});
			this.toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new Point(279, 4);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(78, 24);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.TabStop = true;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStrip1.KeyDown += this.toolStrip1_KeyDown;
			this.lbl_matches.BackColor = SystemColors.InactiveCaption;
			this.lbl_matches.ForeColor = SystemColors.InactiveCaptionText;
			this.lbl_matches.Name = "lbl_matches";
			this.lbl_matches.Size = new Size(0, 21);
			this.btn_previous.AccessibleDescription = "Move to previous search result for the selected search";
			this.btn_previous.AccessibleName = "Move to previous search result for the selected search";
			this.btn_previous.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_previous.Image = Resources.nav_up_blue;
			this.btn_previous.ImageTransparentColor = Color.Magenta;
			this.btn_previous.Name = "btn_previous";
			this.btn_previous.Size = new Size(23, 21);
			this.btn_previous.Text = "Move to previous search result for the selected search";
			this.btn_previous.ToolTipText = "Move to previous search result for the selected search";
			this.btn_previous.Click += this.btn_previous_Click;
			this.btn_next.AccessibleDescription = "Move to next search result for the selected search";
			this.btn_next.AccessibleName = "Move to next search result for the selected search";
			this.btn_next.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_next.Image = Resources.nav_down_blue;
			this.btn_next.ImageTransparentColor = Color.Magenta;
			this.btn_next.Name = "btn_next";
			this.btn_next.Size = new Size(23, 21);
			this.btn_next.Text = "Move to next search result for the selected search";
			this.btn_next.Click += this.btn_next_Click;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 24);
			this.btn_close.AccessibleDescription = "Close this search box";
			this.btn_close.AccessibleName = "Close this search box";
			this.btn_close.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_close.Image = Resources.delete;
			this.btn_close.ImageTransparentColor = Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new Size(23, 21);
			this.btn_close.Text = "Close this search box";
			this.btn_close.ToolTipText = "Close this search box";
			this.btn_close.Click += this.btn_close_Click;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.BorderStyle = BorderStyle.FixedSingle;
			base.Controls.Add(this.textBoxX1);
			base.Controls.Add(this.toolStrip1);
			base.Name = "SearchControl";
			base.Padding = new Padding(4);
			base.Size = new Size(361, 32);
			base.Enter += this.SearchControl_Enter;
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001F RID: 31 RVA: 0x000030DC File Offset: 0x000020DC
		// (remove) Token: 0x06000020 RID: 32 RVA: 0x00003118 File Offset: 0x00002118
		public event SearchControl.SearchRequestHandler SearchRequested;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000021 RID: 33 RVA: 0x00003154 File Offset: 0x00002154
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x00003190 File Offset: 0x00002190
		public event SearchControl.SearchGotoResultHandler SearchGotoResultRequested;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000023 RID: 35 RVA: 0x000031CC File Offset: 0x000021CC
		// (remove) Token: 0x06000024 RID: 36 RVA: 0x00003208 File Offset: 0x00002208
		public event EventHandler CloseRequested;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00003244 File Offset: 0x00002244
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000325C File Offset: 0x0000225C
		public List<SearchMatchResult> Results
		{
			get
			{
				return this.results;
			}
			set
			{
				this.results = value;
				this.currentResultsIndex = 0;
				this.UpdateGuiWithResults();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003274 File Offset: 0x00002274
		public SearchControl()
		{
			this.currentResultsIndex = 0;
			this.results = new List<SearchMatchResult>();
			this.InitializeComponent();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000032A0 File Offset: 0x000022A0
		private void UpdateGuiWithResults()
		{
			if (this.results == null || this.results.Count < 1)
			{
				this.lbl_matches.Text = ((this.textBoxX1.Text.Trim().Length > 0) ? "No match found." : "");
			}
			else
			{
				this.lbl_matches.Text = string.Format("{0} of {1}", (this.currentResultsIndex + 1).ToString(), this.results.Count.ToString());
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003340 File Offset: 0x00002340
		private void OnSearchRequested(string searchText)
		{
			if (this.SearchRequested != null)
			{
				this.SearchRequested(this, searchText);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000336C File Offset: 0x0000236C
		private void OnSearchGotoResultRequested(SearchMatchResult result)
		{
			if (this.SearchGotoResultRequested != null)
			{
				this.SearchGotoResultRequested(this, result);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003398 File Offset: 0x00002398
		private void OnCloseRequested()
		{
			if (this.CloseRequested != null)
			{
				this.CloseRequested(this, new EventArgs());
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000033C7 File Offset: 0x000023C7
		private void btn_close_Click(object sender, EventArgs e)
		{
			this.OnCloseRequested();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000033D1 File Offset: 0x000023D1
		private void btn_next_Click(object sender, EventArgs e)
		{
			this.GotoNextMatch();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000033DC File Offset: 0x000023DC
		private void GotoNextMatch()
		{
			if (this.results != null && this.results.Count > 0)
			{
				int num = this.currentResultsIndex + 1;
				if (num < this.results.Count)
				{
					this.currentResultsIndex = num;
					this.OnSearchGotoResultRequested(this.results[this.currentResultsIndex]);
					this.UpdateGuiWithResults();
				}
			}
			else if (this.textBoxX1.Text.Trim().Length > 0)
			{
				this.OnSearchRequested(this.textBoxX1.Text);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003488 File Offset: 0x00002488
		private void GotoPreviousMatch()
		{
			if (this.results != null && this.results.Count > 0)
			{
				int num = this.currentResultsIndex - 1;
				if (num >= 0)
				{
					this.currentResultsIndex = num;
					this.OnSearchGotoResultRequested(this.results[this.currentResultsIndex]);
					this.UpdateGuiWithResults();
				}
			}
			else if (this.textBoxX1.Text.Trim().Length > 0)
			{
				this.OnSearchRequested(this.textBoxX1.Text);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003526 File Offset: 0x00002526
		private void btn_previous_Click(object sender, EventArgs e)
		{
			this.GotoPreviousMatch();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003530 File Offset: 0x00002530
		private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
		{
			this.KeyDown(e);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000353B File Offset: 0x0000253B
		private void SearchControl_KeyDown(object sender, KeyEventArgs e)
		{
			this.KeyDown(e);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003548 File Offset: 0x00002548
		private new void KeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && this.textBoxX1.Text.Trim().Length > 0)
			{
				this.OnSearchRequested(this.textBoxX1.Text);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				this.OnCloseRequested();
			}
			else if (e.KeyCode == Keys.Next)
			{
				this.GotoNextMatch();
			}
			else if (e.KeyCode == Keys.Prior)
			{
				this.GotoPreviousMatch();
			}
			else if (e.KeyCode == Keys.F3)
			{
				if (e.Shift)
				{
					this.GotoPreviousMatch();
				}
				else
				{
					this.GotoNextMatch();
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000361F File Offset: 0x0000261F
		private void SearchControl_Enter(object sender, EventArgs e)
		{
			this.textBoxX1.Focus();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003630 File Offset: 0x00002630
		private void toolStrip1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.OnCloseRequested();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003658 File Offset: 0x00002658
		private void textBoxX1_Leave(object sender, EventArgs e)
		{
			if (this.textBoxX1.Text.Trim().Length > 0)
			{
				this.OnSearchRequested(this.textBoxX1.Text);
			}
		}

		// Token: 0x04000051 RID: 81
		private IContainer components = null;

		// Token: 0x04000052 RID: 82
		private TextBoxX textBoxX1;

		// Token: 0x04000053 RID: 83
		private ToolStrip toolStrip1;

		// Token: 0x04000054 RID: 84
		private ToolStripButton btn_previous;

		// Token: 0x04000055 RID: 85
		private ToolStripButton btn_next;

		// Token: 0x04000056 RID: 86
		private ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000057 RID: 87
		private ToolStripButton btn_close;

		// Token: 0x04000058 RID: 88
		private ToolStripLabel lbl_matches;

		// Token: 0x04000059 RID: 89
		private List<SearchMatchResult> results;

		// Token: 0x0400005A RID: 90
		private int currentResultsIndex;

		// Token: 0x0200000A RID: 10
		public enum SearchDirection
		{
			// Token: 0x0400005F RID: 95
			Up,
			// Token: 0x04000060 RID: 96
			Down
		}

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000038 RID: 56
		public delegate void SearchRequestHandler(object sender, string searchText);

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x0600003C RID: 60
		public delegate void SearchGotoResultHandler(object sender, SearchMatchResult result);
	}
}
