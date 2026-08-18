using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.InputDialogControls;
using AutoComboBox.Properties;
using EncryptionClassLibrary;
using UnivOleDb;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000044 RID: 68
	public class MyInfoBox : UserControl, IDisposable
	{
		// Token: 0x06000277 RID: 631 RVA: 0x000148E8 File Offset: 0x000138E8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				if (this.controlWithUser != null)
				{
					if (this.controlWithUser is AutoComboBox)
					{
						AutoComboBox autoComboBox = (AutoComboBox)this.controlWithUser;
						autoComboBox.UserSelectedSomething -= this.cmb_UserSelectedSomething;
						autoComboBox.UserSelectedSameItem -= this.cmb_UserSelectedSomething;
					}
					this.controlWithUser = null;
				}
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00014980 File Offset: 0x00013980
		private void InitializeComponent()
		{
			this.toolStrip1 = new ToolStrip();
			this.btn_edit = new ToolStripButton();
			this.rtf = new RichTextBoxPrintCtrl();
			this.btn_lookup = new ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btn_edit,
				this.btn_lookup
			});
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(230, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.TabStop = true;
			this.btn_edit.Image = Resources.document_ok;
			this.btn_edit.ImageTransparentColor = Color.Magenta;
			this.btn_edit.Name = "btn_edit";
			this.btn_edit.Size = new Size(122, 22);
			this.btn_edit.Text = "Edit this information";
			this.btn_edit.Click += this.btn_edit_Click;
			this.rtf.Dock = DockStyle.Fill;
			this.rtf.HiglightColor = RtfColor.White;
			this.rtf.Location = new Point(0, 25);
			this.rtf.Margin = new Padding(3, 4, 3, 4);
			this.rtf.Name = "rtf";
			this.rtf.ReadOnly = true;
			this.rtf.Size = new Size(230, 24);
			this.rtf.TabIndex = 1;
			this.rtf.Text = "";
			this.rtf.TextColor = RtfColor.Black;
			this.btn_lookup.Image = Resources.news_view;
			this.btn_lookup.ImageTransparentColor = Color.Magenta;
			this.btn_lookup.Name = "btn_lookup";
			this.btn_lookup.Size = new Size(82, 22);
			this.btn_lookup.Text = "Lookup info";
			this.btn_lookup.Click += this.btn_lookup_Click;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.rtf);
			base.Controls.Add(this.toolStrip1);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "MyInfoBox";
			base.Size = new Size(230, 49);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00014C94 File Offset: 0x00013C94
		public MyInfoBox()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00014CCC File Offset: 0x00013CCC
		public MyInfoBox(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int cidWithUser, int cidWithData)
		{
			this.tripleDES = tripleDES;
			this.da = da;
			this.cidWithData = cidWithData;
			this.cidWithUser = cidWithUser;
			this.InitializeComponent();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00014D2C File Offset: 0x00013D2C
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00014D44 File Offset: 0x00013D44
		public int CidWithUser
		{
			get
			{
				return this.cidWithUser;
			}
			set
			{
				this.cidWithUser = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00014D50 File Offset: 0x00013D50
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00014D68 File Offset: 0x00013D68
		public int CidWithData
		{
			get
			{
				return this.cidWithData;
			}
			set
			{
				this.cidWithData = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00014D72 File Offset: 0x00013D72
		public UnivDataAdapter Da
		{
			set
			{
				this.da = value;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00014D7C File Offset: 0x00013D7C
		private void btn_edit_Click(object sender, EventArgs e)
		{
			if (this.cidWithData > 0)
			{
				if (this.currentPid > 0)
				{
					NotesZoom2 notesZoom = new NotesZoom2(false);
					notesZoom.TextEntered = this.rtf.Rtf;
					DialogResult dialogResult = notesZoom.ShowDialog(this);
					if (dialogResult == DialogResult.OK)
					{
						this.rtf.Rtf = notesZoom.TextEntered;
						byte[] parameterValue = this.tripleDES.Encrypt(notesZoom.TextEntered);
						this.da.SelectCommand.CommandText = "INSERT INTO otherinfops (screennum,personid,controlid,controlvalue) SELECT 0,@pid,@cid,@cv WHERE NOT EXISTS(SELECT dataid FROM otherinfops WHERE personid=@pid AND controlid=@cid)";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@pid", this.currentPid);
						this.da.SelectCommand.Parameters.Add("@cid", this.cidWithData);
						this.da.SelectCommand.Parameters.Add("@cv", parameterValue);
						this.da.Fill(new DataTable());
						this.da.SelectCommand.CommandText = "UPDATE otherinfops SET controlvalue=@cv WHERE personid=@pid AND controlid=@cid";
						this.da.Fill(new DataTable());
					}
				}
				else
				{
					MessageBox.Show("Please select something first.");
				}
			}
			else
			{
				MessageBox.Show("This control has not been properly configured.  Please set the control id that holds the lookup data in the admin (forms editor).");
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00014EF0 File Offset: 0x00013EF0
		public bool SetupHandlerToDetectWhenUserIsChanged(Control topParent)
		{
			Control control = this.FindControl(topParent, this.cidWithUser);
			this.controlWithUser = control;
			bool result;
			if (control != null)
			{
				if (control is AutoComboBox)
				{
					AutoComboBox autoComboBox = (AutoComboBox)control;
					autoComboBox.UserSelectedSomething += this.cmb_UserSelectedSomething;
					autoComboBox.UserSelectedSameItem += this.cmb_UserSelectedSomething;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00014F68 File Offset: 0x00013F68
		public void cmb_UserSelectedSomething(object sender)
		{
			if (sender is AutoComboBox)
			{
				AutoComboBox autoComboBox = (AutoComboBox)sender;
				DataRow dataRow = autoComboBox.SelectedDataRow();
				if (dataRow != null)
				{
					this.currentPid = (int)dataRow[autoComboBox.ValueMember];
					this.da.SelectCommand.CommandText = "SELECT controlvalue FROM otherinfops WHERE personid=@pid AND controlid=@cid";
					this.da.SelectCommand.Parameters.Clear();
					this.da.SelectCommand.Parameters.Add("@pid", this.currentPid);
					this.da.SelectCommand.Parameters.Add("@cid", this.cidWithData);
					DataTable dataTable = new DataTable();
					this.da.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
					{
						this.rtf.Rtf = this.tripleDES.Decrypt((byte[])dataTable.Rows[0][0]);
					}
					else
					{
						this.rtf.Clear();
					}
				}
				else
				{
					this.currentPid = 0;
					this.rtf.Clear();
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000150D0 File Offset: 0x000140D0
		private Control FindControl(Control parent, int cid)
		{
			if (parent.Tag != null && parent.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)parent.Tag;
				int num = (dataRow.Table != null && dataRow.Table.Columns.Contains("controlid") && dataRow["controlid"] != DBNull.Value) ? ((int)dataRow["controlid"]) : 0;
				if (num == cid)
				{
					return parent;
				}
			}
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				Control control = this.FindControl(parent2, cid);
				if (control != null)
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000151E4 File Offset: 0x000141E4
		private void btn_lookup_Click(object sender, EventArgs e)
		{
			this.cmb_UserSelectedSomething(this.controlWithUser);
		}

		// Token: 0x040001FE RID: 510
		private IContainer components = null;

		// Token: 0x040001FF RID: 511
		private ToolStrip toolStrip1;

		// Token: 0x04000200 RID: 512
		private ToolStripButton btn_edit;

		// Token: 0x04000201 RID: 513
		private RichTextBoxPrintCtrl rtf;

		// Token: 0x04000202 RID: 514
		private ToolStripButton btn_lookup;

		// Token: 0x04000203 RID: 515
		private int currentPid = 0;

		// Token: 0x04000204 RID: 516
		private UnivDataAdapter da;

		// Token: 0x04000205 RID: 517
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x04000206 RID: 518
		private int cidWithUser = 0;

		// Token: 0x04000207 RID: 519
		private int cidWithData = 0;

		// Token: 0x04000208 RID: 520
		private Control controlWithUser = null;
	}
}
