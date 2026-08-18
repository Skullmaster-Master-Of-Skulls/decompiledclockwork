using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox;
using DevComponents.DotNetBar;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI.DynamicDataForms
{
	// Token: 0x02000017 RID: 23
	public class DataPerApp2 : UserControl
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005800 File Offset: 0x00004800
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005838 File Offset: 0x00004838
		private void InitializeComponent()
		{
			this.components = new Container();
			this.p_data = new MyPanel();
			this.toolTip1 = new ToolTip(this.components);
			this.splitter_ps = new ExpandableSplitter();
			this.p_data_ps = new MyPanel();
			this.lbl_noPermissionsToChangeMessage = new Label();
			base.SuspendLayout();
			this.p_data.AutoScroll = true;
			this.p_data.BalloonTip = null;
			this.p_data.BorderStyle = BorderStyle.Fixed3D;
			this.p_data.Caption = "";
			this.p_data.DefaultActiveControl = 0;
			this.p_data.Dock = DockStyle.Fill;
			this.p_data.FirstName = null;
			this.p_data.IsDynamicScreenContainer = false;
			this.p_data.IsTopLevelDynamicControlsContainer = false;
			this.p_data.LastName = null;
			this.p_data.Location = new Point(0, 124);
			this.p_data.Margin = new Padding(3, 4, 3, 4);
			this.p_data.Name = "p_data";
			this.p_data.Pid = 0;
			this.p_data.PrimaryClientDescription = null;
			this.p_data.PrimaryClientPid = 0;
			this.p_data.Screen = null;
			this.p_data.Size = new Size(555, 258);
			this.p_data.Student_no = null;
			this.p_data.TabIndex = 21;
			this.p_data.Tag2 = null;
			this.p_data.Tag3 = null;
			this.p_data.TagInt = -1;
			this.p_data.Tooltip = null;
			this.splitter_ps.BackColor2 = Color.FromArgb(0, 45, 150);
			this.splitter_ps.BackColor2SchemePart = 53;
			this.splitter_ps.BackColorSchemePart = 51;
			this.splitter_ps.BorderStyle = BorderStyle.Fixed3D;
			this.splitter_ps.Dock = DockStyle.Top;
			this.splitter_ps.ExpandFillColor = Color.FromArgb(0, 45, 150);
			this.splitter_ps.ExpandFillColorSchemePart = 53;
			this.splitter_ps.ExpandLineColor = SystemColors.ControlText;
			this.splitter_ps.ExpandLineColorSchemePart = 40;
			this.splitter_ps.GripDarkColor = SystemColors.ControlText;
			this.splitter_ps.GripDarkColorSchemePart = 40;
			this.splitter_ps.GripLightColor = Color.FromArgb(223, 237, 254);
			this.splitter_ps.GripLightColorSchemePart = 0;
			this.splitter_ps.HotBackColor = Color.FromArgb(254, 142, 75);
			this.splitter_ps.HotBackColor2 = Color.FromArgb(255, 207, 139);
			this.splitter_ps.HotBackColor2SchemePart = 35;
			this.splitter_ps.HotBackColorSchemePart = 34;
			this.splitter_ps.HotExpandFillColor = Color.FromArgb(0, 45, 150);
			this.splitter_ps.HotExpandFillColorSchemePart = 53;
			this.splitter_ps.HotExpandLineColor = SystemColors.ControlText;
			this.splitter_ps.HotExpandLineColorSchemePart = 40;
			this.splitter_ps.HotGripDarkColor = Color.FromArgb(0, 45, 150);
			this.splitter_ps.HotGripDarkColorSchemePart = 53;
			this.splitter_ps.HotGripLightColor = Color.FromArgb(223, 237, 254);
			this.splitter_ps.HotGripLightColorSchemePart = 0;
			this.splitter_ps.Location = new Point(0, 111);
			this.splitter_ps.Name = "splitter_ps";
			this.splitter_ps.Size = new Size(555, 13);
			this.splitter_ps.TabIndex = 23;
			this.splitter_ps.TabStop = false;
			this.splitter_ps.Visible = false;
			this.p_data_ps.AutoScroll = true;
			this.p_data_ps.BackColor = SystemColors.Control;
			this.p_data_ps.BalloonTip = null;
			this.p_data_ps.BorderStyle = BorderStyle.Fixed3D;
			this.p_data_ps.Caption = "";
			this.p_data_ps.DefaultActiveControl = 0;
			this.p_data_ps.Dock = DockStyle.Top;
			this.p_data_ps.FirstName = null;
			this.p_data_ps.Font = new Font("Arial", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.p_data_ps.IsDynamicScreenContainer = false;
			this.p_data_ps.IsTopLevelDynamicControlsContainer = true;
			this.p_data_ps.LastName = null;
			this.p_data_ps.Location = new Point(0, 0);
			this.p_data_ps.Name = "p_data_ps";
			this.p_data_ps.Pid = 0;
			this.p_data_ps.PrimaryClientDescription = null;
			this.p_data_ps.PrimaryClientPid = 0;
			this.p_data_ps.Screen = null;
			this.p_data_ps.Size = new Size(555, 111);
			this.p_data_ps.Student_no = null;
			this.p_data_ps.TabIndex = 22;
			this.p_data_ps.Tag2 = null;
			this.p_data_ps.Tag3 = null;
			this.p_data_ps.TagInt = -1;
			this.p_data_ps.Tooltip = null;
			this.p_data_ps.Visible = false;
			this.lbl_noPermissionsToChangeMessage.BackColor = SystemColors.Highlight;
			this.lbl_noPermissionsToChangeMessage.BorderStyle = BorderStyle.Fixed3D;
			this.lbl_noPermissionsToChangeMessage.Dock = DockStyle.Bottom;
			this.lbl_noPermissionsToChangeMessage.Font = new Font("Arial", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbl_noPermissionsToChangeMessage.ForeColor = SystemColors.HighlightText;
			this.lbl_noPermissionsToChangeMessage.Location = new Point(0, 382);
			this.lbl_noPermissionsToChangeMessage.Name = "lbl_noPermissionsToChangeMessage";
			this.lbl_noPermissionsToChangeMessage.Size = new Size(555, 51);
			this.lbl_noPermissionsToChangeMessage.TabIndex = 24;
			this.lbl_noPermissionsToChangeMessage.Text = "Only the person who had the appointment with this student is allowed to enter this assessment";
			this.lbl_noPermissionsToChangeMessage.TextAlign = ContentAlignment.MiddleCenter;
			this.lbl_noPermissionsToChangeMessage.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.p_data);
			base.Controls.Add(this.splitter_ps);
			base.Controls.Add(this.p_data_ps);
			base.Controls.Add(this.lbl_noPermissionsToChangeMessage);
			base.Name = "DataPerApp2";
			base.Size = new Size(555, 433);
			base.ResumeLayout(false);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005F11 File Offset: 0x00004F11
		public DataPerApp2()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005F2A File Offset: 0x00004F2A
		public void RenderForm(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int screenNum)
		{
			this.Cursor = Cursors.WaitCursor;
			base.SuspendLayout();
			base.ResumeLayout();
			this.Cursor = Cursors.Default;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005F53 File Offset: 0x00004F53
		private void btn_cancel_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005F56 File Offset: 0x00004F56
		private void btn_save_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0400006C RID: 108
		private IContainer components = null;

		// Token: 0x0400006D RID: 109
		private MyPanel p_data;

		// Token: 0x0400006E RID: 110
		private ToolTip toolTip1;

		// Token: 0x0400006F RID: 111
		private ExpandableSplitter splitter_ps;

		// Token: 0x04000070 RID: 112
		private MyPanel p_data_ps;

		// Token: 0x04000071 RID: 113
		private Label lbl_noPermissionsToChangeMessage;
	}
}
