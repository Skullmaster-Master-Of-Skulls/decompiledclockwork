namespace DynamicScreens.AdminTools
{
	// Token: 0x0200001C RID: 28
	public partial class ScreenTypeChooser : global::System.Windows.Forms.Form
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0001794C File Offset: 0x0001694C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00017988 File Offset: 0x00016988
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DynamicScreens.AdminTools.ScreenTypeChooser));
			this.rbtn_perStudent = new global::System.Windows.Forms.RadioButton();
			this.rbtn_perAppointment = new global::System.Windows.Forms.RadioButton();
			this.rbtn_anonymous = new global::System.Windows.Forms.RadioButton();
			this.rbnt_survey = new global::System.Windows.Forms.RadioButton();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			this.label6 = new global::System.Windows.Forms.Label();
			this.btn_ok = new global::System.Windows.Forms.Button();
			this.label7 = new global::System.Windows.Forms.Label();
			this.rbtn_staffPA = new global::System.Windows.Forms.RadioButton();
			this.label8 = new global::System.Windows.Forms.Label();
			this.rb_infoPM = new global::System.Windows.Forms.RadioButton();
			this.label9 = new global::System.Windows.Forms.Label();
			this.rbtn_instructorPerDate = new global::System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.rbtn_perStudent.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rbtn_perStudent.Location = new global::System.Drawing.Point(8, 26);
			this.rbtn_perStudent.Name = "rbtn_perStudent";
			this.rbtn_perStudent.Padding = new global::System.Windows.Forms.Padding(25, 0, 0, 0);
			this.rbtn_perStudent.Size = new global::System.Drawing.Size(564, 32);
			this.rbtn_perStudent.TabIndex = 0;
			this.rbtn_perStudent.Text = "Per student";
			this.rbtn_perAppointment.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rbtn_perAppointment.Location = new global::System.Drawing.Point(8, 138);
			this.rbtn_perAppointment.Name = "rbtn_perAppointment";
			this.rbtn_perAppointment.Padding = new global::System.Windows.Forms.Padding(25, 0, 0, 0);
			this.rbtn_perAppointment.Size = new global::System.Drawing.Size(564, 32);
			this.rbtn_perAppointment.TabIndex = 1;
			this.rbtn_perAppointment.Text = "Per appointment";
			this.rbtn_anonymous.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rbtn_anonymous.Location = new global::System.Drawing.Point(8, 194);
			this.rbtn_anonymous.Name = "rbtn_anonymous";
			this.rbtn_anonymous.Padding = new global::System.Windows.Forms.Padding(25, 0, 0, 0);
			this.rbtn_anonymous.Size = new global::System.Drawing.Size(564, 32);
			this.rbtn_anonymous.TabIndex = 2;
			this.rbtn_anonymous.Text = "Anonymous";
			this.rbnt_survey.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rbnt_survey.Location = new global::System.Drawing.Point(8, 250);
			this.rbnt_survey.Name = "rbnt_survey";
			this.rbnt_survey.Padding = new global::System.Windows.Forms.Padding(25, 0, 0, 0);
			this.rbnt_survey.Size = new global::System.Drawing.Size(564, 32);
			this.rbnt_survey.TabIndex = 3;
			this.rbnt_survey.Text = "Survey / evaluation";
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(8, 2);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(564, 24);
			this.label1.TabIndex = 4;
			this.label1.Text = "Please select the type of form (screen):";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(8, 58);
			this.label2.Name = "label2";
			this.label2.Padding = new global::System.Windows.Forms.Padding(50, 0, 0, 0);
			this.label2.Size = new global::System.Drawing.Size(564, 24);
			this.label2.TabIndex = 5;
			this.label2.Text = "     Each student gets a single screen with fields of data - you can update the fields at any time.";
			this.label3.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label3.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(8, 170);
			this.label3.Name = "label3";
			this.label3.Padding = new global::System.Windows.Forms.Padding(50, 0, 0, 0);
			this.label3.Size = new global::System.Drawing.Size(564, 24);
			this.label3.TabIndex = 6;
			this.label3.Text = "     Each student gets a single screen with fields of data - you can update the fields at any time.";
			this.label4.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label4.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(8, 226);
			this.label4.Name = "label4";
			this.label4.Padding = new global::System.Windows.Forms.Padding(50, 0, 0, 0);
			this.label4.Size = new global::System.Drawing.Size(564, 24);
			this.label4.TabIndex = 7;
			this.label4.Text = "     Each student gets a single screen with fields of data - you can update the fields at any time.";
			this.label5.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label5.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new global::System.Drawing.Point(8, 282);
			this.label5.Name = "label5";
			this.label5.Padding = new global::System.Windows.Forms.Padding(50, 0, 0, 0);
			this.label5.Size = new global::System.Drawing.Size(564, 24);
			this.label5.TabIndex = 8;
			this.label5.Text = "     Each student gets a single screen with fields of data - you can update the fields at any time.";
			this.panel1.Controls.Add(this.btn_cancel);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.btn_ok);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new global::System.Drawing.Point(8, 439);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(564, 48);
			this.panel1.TabIndex = 9;
			this.btn_cancel.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_cancel.Location = new global::System.Drawing.Point(316, 0);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(120, 48);
			this.btn_cancel.TabIndex = 2;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.label6.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.label6.Location = new global::System.Drawing.Point(436, 0);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(8, 48);
			this.label6.TabIndex = 1;
			this.btn_ok.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_ok.Location = new global::System.Drawing.Point(444, 0);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(120, 48);
			this.btn_ok.TabIndex = 0;
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.label7.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label7.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label7.Location = new global::System.Drawing.Point(8, 338);
			this.label7.Name = "label7";
			this.label7.Padding = new global::System.Windows.Forms.Padding(50, 0, 0, 0);
			this.label7.Size = new global::System.Drawing.Size(564, 24);
			this.label7.TabIndex = 11;
			this.label7.Text = "     Each staff gets a single screen with fields of data for each date.";
			this.rbtn_staffPA.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rbtn_staffPA.Location = new global::System.Drawing.Point(8, 306);
			this.rbtn_staffPA.Name = "rbtn_staffPA";
			this.rbtn_staffPA.Padding = new global::System.Windows.Forms.Padding(25, 0, 0, 0);
			this.rbtn_staffPA.Size = new global::System.Drawing.Size(564, 32);
			this.rbtn_staffPA.TabIndex = 10;
			this.rbtn_staffPA.Text = "Staff per date";
			this.label8.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label8.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label8.Location = new global::System.Drawing.Point(8, 114);
			this.label8.Name = "label8";
			this.label8.Padding = new global::System.Windows.Forms.Padding(50, 0, 0, 0);
			this.label8.Size = new global::System.Drawing.Size(564, 24);
			this.label8.TabIndex = 13;
			this.label8.Text = "     You can create any number of instances of a form for each student.  Form entries are dated.";
			this.rb_infoPM.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rb_infoPM.Location = new global::System.Drawing.Point(8, 82);
			this.rb_infoPM.Name = "rb_infoPM";
			this.rb_infoPM.Padding = new global::System.Windows.Forms.Padding(25, 0, 0, 0);
			this.rb_infoPM.Size = new global::System.Drawing.Size(564, 32);
			this.rb_infoPM.TabIndex = 12;
			this.rb_infoPM.Text = "Per date";
			this.label9.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label9.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label9.Location = new global::System.Drawing.Point(8, 394);
			this.label9.Name = "label9";
			this.label9.Padding = new global::System.Windows.Forms.Padding(50, 0, 0, 0);
			this.label9.Size = new global::System.Drawing.Size(564, 24);
			this.label9.TabIndex = 15;
			this.label9.Text = "Use for instructor test info (by date) for online test confirmation";
			this.rbtn_instructorPerDate.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rbtn_instructorPerDate.Location = new global::System.Drawing.Point(8, 362);
			this.rbtn_instructorPerDate.Name = "rbtn_instructorPerDate";
			this.rbtn_instructorPerDate.Padding = new global::System.Windows.Forms.Padding(25, 0, 0, 0);
			this.rbtn_instructorPerDate.Size = new global::System.Drawing.Size(564, 32);
			this.rbtn_instructorPerDate.TabIndex = 14;
			this.rbtn_instructorPerDate.Text = "Instructor per date";
			this.AutoScaleBaseSize = new global::System.Drawing.Size(8, 19);
			base.ClientSize = new global::System.Drawing.Size(576, 489);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.rbtn_instructorPerDate);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.rbtn_staffPA);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.rbnt_survey);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.rbtn_anonymous);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.rbtn_perAppointment);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.rb_infoPM);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.rbtn_perStudent);
			base.Controls.Add(this.label1);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "ScreenTypeChooser";
			base.Padding = new global::System.Windows.Forms.Padding(8, 2, 4, 2);
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select form/screen type";
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000140 RID: 320
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000141 RID: 321
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000142 RID: 322
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000143 RID: 323
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000144 RID: 324
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000145 RID: 325
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000146 RID: 326
		private global::System.Windows.Forms.Button btn_ok;

		// Token: 0x04000147 RID: 327
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000148 RID: 328
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x04000149 RID: 329
		private global::System.Windows.Forms.RadioButton rbtn_perStudent;

		// Token: 0x0400014A RID: 330
		private global::System.Windows.Forms.RadioButton rbtn_perAppointment;

		// Token: 0x0400014B RID: 331
		private global::System.Windows.Forms.RadioButton rbtn_anonymous;

		// Token: 0x0400014C RID: 332
		private global::System.Windows.Forms.RadioButton rbnt_survey;

		// Token: 0x0400014D RID: 333
		private global::System.Windows.Forms.Label label7;

		// Token: 0x0400014E RID: 334
		private global::System.Windows.Forms.RadioButton rbtn_staffPA;

		// Token: 0x0400014F RID: 335
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000150 RID: 336
		private global::System.Windows.Forms.RadioButton rb_infoPM;

		// Token: 0x04000151 RID: 337
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000152 RID: 338
		private global::System.Windows.Forms.RadioButton rbtn_instructorPerDate;

		// Token: 0x04000153 RID: 339
		private global::System.ComponentModel.Container components = null;
	}
}
