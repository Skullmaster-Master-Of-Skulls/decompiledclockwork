using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AutoComboBox;
using AutoComboBox.InputDialogControls;
using AutoComboBox.MyControls;
using EmailClassLibrary;
using EncryptionClassLibrary;
using TechnoPro.Common.Core;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;
using UnivOleDb;

namespace ReportFunctions.ReportFunctionParameterControls
{
	// Token: 0x02000009 RID: 9
	public class ReportFunctionParameterControl_BatchEmail3 : UserControl, iReportFunctionParameter
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003064 File Offset: 0x00002064
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000309C File Offset: 0x0000209C
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ReportFunctionParameterControl_BatchEmail3));
			this.txt_adminEmail = new TextBox();
			this.label1 = new Label();
			this.chk_sendReport = new CheckBox();
			this.chk_testMode = new CheckBox();
			this.chk_enabled = new CheckBox();
			this.label2 = new Label();
			this.btn_icon = new Button();
			this.txt_emailHistoryTypeCode = new TextBox();
			this.label10 = new Label();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.chk_promptUser = new CheckBox();
			this.label15 = new Label();
			this.txt_priority = new TextBox();
			this.label14 = new Label();
			this.btn_testSettings = new Button();
			this.txt_delayBetweenSendingEmails = new NumericUpDown();
			this.label13 = new Label();
			this.label12 = new Label();
			this.txt_title = new TextBox();
			this.label11 = new Label();
			this.tabPage2 = new TabPage();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label5 = new Label();
			this.label7 = new Label();
			this.txt_attach = new TextBox();
			this.label4 = new Label();
			this.txt_subject = new TextBox();
			this.label3 = new Label();
			this.label6 = new Label();
			this.txt_bcc = new TextBox();
			this.txt_cc = new TextBox();
			this.label8 = new Label();
			this.label9 = new Label();
			this.txt_to = new TextBox();
			this.txt_from = new TextBox();
			this.tabControl2 = new TabControl();
			this.tabPage3 = new TabPage();
			this.txt_body = new TextBox();
			this.chk_sendAsHtml = new CheckBox();
			this.tabPage4 = new TabPage();
			this.browser_bodyPreview = new MyWebBrowser();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((ISupportInitialize)this.txt_delayBetweenSendingEmails).BeginInit();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			base.SuspendLayout();
			this.txt_adminEmail.Location = new Point(174, 197);
			this.txt_adminEmail.Margin = new Padding(3, 4, 3, 4);
			this.txt_adminEmail.Name = "txt_adminEmail";
			this.txt_adminEmail.Size = new Size(307, 22);
			this.txt_adminEmail.TabIndex = 9;
			this.label1.Location = new Point(57, 200);
			this.label1.Name = "label1";
			this.label1.Size = new Size(95, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Admin email:";
			this.label1.TextAlign = ContentAlignment.MiddleRight;
			this.chk_sendReport.AutoSize = true;
			this.chk_sendReport.Checked = true;
			this.chk_sendReport.CheckState = CheckState.Checked;
			this.chk_sendReport.Location = new Point(9, 38);
			this.chk_sendReport.Margin = new Padding(3, 4, 3, 4);
			this.chk_sendReport.Name = "chk_sendReport";
			this.chk_sendReport.Size = new Size(94, 20);
			this.chk_sendReport.TabIndex = 10;
			this.chk_sendReport.Text = "Send &report";
			this.chk_sendReport.UseVisualStyleBackColor = true;
			this.chk_testMode.AutoSize = true;
			this.chk_testMode.Checked = true;
			this.chk_testMode.CheckState = CheckState.Checked;
			this.chk_testMode.Location = new Point(9, 60);
			this.chk_testMode.Margin = new Padding(3, 5, 3, 5);
			this.chk_testMode.Name = "chk_testMode";
			this.chk_testMode.Size = new Size(425, 20);
			this.chk_testMode.TabIndex = 3;
			this.chk_testMode.Text = "Test mode (all emails will be diverted to the admin account for review)";
			this.chk_testMode.UseVisualStyleBackColor = true;
			this.chk_enabled.AutoSize = true;
			this.chk_enabled.Checked = true;
			this.chk_enabled.CheckState = CheckState.Checked;
			this.chk_enabled.Location = new Point(9, 17);
			this.chk_enabled.Margin = new Padding(3, 5, 3, 5);
			this.chk_enabled.Name = "chk_enabled";
			this.chk_enabled.Size = new Size(74, 20);
			this.chk_enabled.TabIndex = 2;
			this.chk_enabled.Text = "Enabled";
			this.chk_enabled.UseVisualStyleBackColor = true;
			this.label2.Location = new Point(9, 284);
			this.label2.Name = "label2";
			this.label2.Size = new Size(246, 36);
			this.label2.TabIndex = 10;
			this.label2.Text = "Appointment icon to mark email sent:";
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			this.btn_icon.AccessibleDescription = "Icon to be used for marking the email was sent on the appointment";
			this.btn_icon.AccessibleName = "Icon to be used for marking the email was sent on the appointment";
			this.btn_icon.Location = new Point(284, 286);
			this.btn_icon.Name = "btn_icon";
			this.btn_icon.Size = new Size(41, 34);
			this.btn_icon.TabIndex = 11;
			this.btn_icon.UseVisualStyleBackColor = true;
			this.btn_icon.Click += this.btn_icon_Click;
			this.txt_emailHistoryTypeCode.Location = new Point(174, 167);
			this.txt_emailHistoryTypeCode.Margin = new Padding(3, 4, 3, 4);
			this.txt_emailHistoryTypeCode.Name = "txt_emailHistoryTypeCode";
			this.txt_emailHistoryTypeCode.Size = new Size(307, 22);
			this.txt_emailHistoryTypeCode.TabIndex = 7;
			this.label10.Location = new Point(3, 170);
			this.label10.Name = "label10";
			this.label10.Size = new Size(149, 16);
			this.label10.TabIndex = 6;
			this.label10.Text = "Email history type code:";
			this.label10.TextAlign = ContentAlignment.MiddleRight;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = DockStyle.Fill;
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(632, 434);
			this.tabControl1.TabIndex = 0;
			this.tabPage1.Controls.Add(this.chk_promptUser);
			this.tabPage1.Controls.Add(this.label15);
			this.tabPage1.Controls.Add(this.txt_priority);
			this.tabPage1.Controls.Add(this.label14);
			this.tabPage1.Controls.Add(this.btn_testSettings);
			this.tabPage1.Controls.Add(this.txt_delayBetweenSendingEmails);
			this.tabPage1.Controls.Add(this.label13);
			this.tabPage1.Controls.Add(this.label12);
			this.tabPage1.Controls.Add(this.txt_title);
			this.tabPage1.Controls.Add(this.label11);
			this.tabPage1.Controls.Add(this.txt_emailHistoryTypeCode);
			this.tabPage1.Controls.Add(this.chk_enabled);
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.btn_icon);
			this.tabPage1.Controls.Add(this.txt_adminEmail);
			this.tabPage1.Controls.Add(this.chk_sendReport);
			this.tabPage1.Controls.Add(this.chk_testMode);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Location = new Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(624, 405);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Main settings";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.chk_promptUser.AutoSize = true;
			this.chk_promptUser.Location = new Point(9, 98);
			this.chk_promptUser.Margin = new Padding(3, 5, 3, 5);
			this.chk_promptUser.Name = "chk_promptUser";
			this.chk_promptUser.Size = new Size(352, 20);
			this.chk_promptUser.TabIndex = 19;
			this.chk_promptUser.Text = "Prompt the user to confirm they want to send the emails";
			this.chk_promptUser.UseVisualStyleBackColor = true;
			this.label15.Location = new Point(259, 226);
			this.label15.Name = "label15";
			this.label15.Size = new Size(222, 22);
			this.label15.TabIndex = 18;
			this.label15.Text = "(1-5, where 1 is urgent)";
			this.label15.TextAlign = ContentAlignment.MiddleLeft;
			this.txt_priority.Location = new Point(174, 226);
			this.txt_priority.Margin = new Padding(3, 4, 3, 4);
			this.txt_priority.Name = "txt_priority";
			this.txt_priority.Size = new Size(66, 22);
			this.txt_priority.TabIndex = 17;
			this.txt_priority.TextAlign = HorizontalAlignment.Center;
			this.label14.Location = new Point(3, 229);
			this.label14.Name = "label14";
			this.label14.Size = new Size(149, 16);
			this.label14.TabIndex = 16;
			this.label14.Text = "Priority:";
			this.label14.TextAlign = ContentAlignment.MiddleRight;
			this.btn_testSettings.Location = new Point(284, 338);
			this.btn_testSettings.Name = "btn_testSettings";
			this.btn_testSettings.Size = new Size(110, 44);
			this.btn_testSettings.TabIndex = 13;
			this.btn_testSettings.Text = "Test Smtp settings";
			this.btn_testSettings.UseVisualStyleBackColor = true;
			this.btn_testSettings.Click += this.btn_testSettings_Click;
			this.txt_delayBetweenSendingEmails.Location = new Point(283, 253);
			this.txt_delayBetweenSendingEmails.Name = "txt_delayBetweenSendingEmails";
			this.txt_delayBetweenSendingEmails.Size = new Size(76, 22);
			this.txt_delayBetweenSendingEmails.TabIndex = 15;
			this.txt_delayBetweenSendingEmails.TextAlign = HorizontalAlignment.Center;
			this.label13.Location = new Point(6, 252);
			this.label13.Name = "label13";
			this.label13.Size = new Size(295, 22);
			this.label13.TabIndex = 14;
			this.label13.Text = "Delay in seconds between sending emails:";
			this.label13.TextAlign = ContentAlignment.MiddleLeft;
			this.label12.Location = new Point(7, 333);
			this.label12.Name = "label12";
			this.label12.Size = new Size(252, 49);
			this.label12.TabIndex = 12;
			this.label12.Text = "Smtp settings are stored in the 'Everyone settings' section in the ClockWork Admin";
			this.label12.TextAlign = ContentAlignment.MiddleLeft;
			this.txt_title.Location = new Point(174, 137);
			this.txt_title.Margin = new Padding(3, 4, 3, 4);
			this.txt_title.Name = "txt_title";
			this.txt_title.Size = new Size(307, 22);
			this.txt_title.TabIndex = 5;
			this.label11.Location = new Point(3, 140);
			this.label11.Name = "label11";
			this.label11.Size = new Size(149, 16);
			this.label11.TabIndex = 5;
			this.label11.Text = "Title:";
			this.label11.TextAlign = ContentAlignment.MiddleRight;
			this.tabPage2.Controls.Add(this.tableLayoutPanel1);
			this.tabPage2.Location = new Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3);
			this.tabPage2.Size = new Size(624, 405);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Email Template";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.50503f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82.49497f));
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.txt_attach, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.txt_subject, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txt_bcc, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.txt_cc, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txt_to, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.txt_from, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.tabControl2, 1, 6);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(3, 3);
			this.tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.Size = new Size(618, 399);
			this.tableLayoutPanel1.TabIndex = 20;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(3, 168);
			this.label5.Name = "label5";
			this.label5.Size = new Size(38, 16);
			this.label5.TabIndex = 33;
			this.label5.Text = "Body";
			this.label7.AutoSize = true;
			this.label7.Location = new Point(3, 140);
			this.label7.Name = "label7";
			this.label7.Size = new Size(50, 16);
			this.label7.TabIndex = 31;
			this.label7.Text = "Attach:";
			this.txt_attach.Dock = DockStyle.Fill;
			this.txt_attach.Location = new Point(111, 143);
			this.txt_attach.Name = "txt_attach";
			this.txt_attach.Size = new Size(504, 22);
			this.txt_attach.TabIndex = 32;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(3, 112);
			this.label4.Name = "label4";
			this.label4.Size = new Size(52, 16);
			this.label4.TabIndex = 29;
			this.label4.Text = "Subject";
			this.txt_subject.Dock = DockStyle.Fill;
			this.txt_subject.Location = new Point(111, 115);
			this.txt_subject.Name = "txt_subject";
			this.txt_subject.Size = new Size(504, 22);
			this.txt_subject.TabIndex = 30;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(3, 84);
			this.label3.Name = "label3";
			this.label3.Size = new Size(35, 16);
			this.label3.TabIndex = 27;
			this.label3.Text = "Bcc:";
			this.label6.AutoSize = true;
			this.label6.Location = new Point(3, 56);
			this.label6.Name = "label6";
			this.label6.Size = new Size(28, 16);
			this.label6.TabIndex = 25;
			this.label6.Text = "Cc:";
			this.txt_bcc.Dock = DockStyle.Fill;
			this.txt_bcc.Location = new Point(111, 87);
			this.txt_bcc.Name = "txt_bcc";
			this.txt_bcc.Size = new Size(504, 22);
			this.txt_bcc.TabIndex = 28;
			this.txt_cc.Dock = DockStyle.Fill;
			this.txt_cc.Location = new Point(111, 59);
			this.txt_cc.Name = "txt_cc";
			this.txt_cc.Size = new Size(504, 22);
			this.txt_cc.TabIndex = 26;
			this.label8.AutoSize = true;
			this.label8.Location = new Point(3, 28);
			this.label8.Name = "label8";
			this.label8.Size = new Size(25, 16);
			this.label8.TabIndex = 23;
			this.label8.Text = "To:";
			this.label9.AutoSize = true;
			this.label9.Location = new Point(3, 0);
			this.label9.Name = "label9";
			this.label9.Size = new Size(42, 16);
			this.label9.TabIndex = 21;
			this.label9.Text = "From:";
			this.txt_to.Dock = DockStyle.Fill;
			this.txt_to.Location = new Point(111, 31);
			this.txt_to.Name = "txt_to";
			this.txt_to.Size = new Size(504, 22);
			this.txt_to.TabIndex = 24;
			this.txt_from.Dock = DockStyle.Fill;
			this.txt_from.Location = new Point(111, 3);
			this.txt_from.Name = "txt_from";
			this.txt_from.Size = new Size(504, 22);
			this.txt_from.TabIndex = 22;
			this.tabControl2.Controls.Add(this.tabPage3);
			this.tabControl2.Controls.Add(this.tabPage4);
			this.tabControl2.Dock = DockStyle.Fill;
			this.tabControl2.Location = new Point(111, 171);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new Size(504, 237);
			this.tabControl2.TabIndex = 34;
			this.tabControl2.SelectedIndexChanged += this.tabControl2_SelectedIndexChanged;
			this.tabPage3.Controls.Add(this.txt_body);
			this.tabPage3.Controls.Add(this.chk_sendAsHtml);
			this.tabPage3.Location = new Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new Padding(3);
			this.tabPage3.Size = new Size(496, 208);
			this.tabPage3.TabIndex = 0;
			this.tabPage3.Text = "Body text";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.txt_body.Dock = DockStyle.Fill;
			this.txt_body.Location = new Point(3, 20);
			this.txt_body.Multiline = true;
			this.txt_body.Name = "txt_body";
			this.txt_body.Size = new Size(490, 188);
			this.txt_body.TabIndex = 35;
			this.chk_sendAsHtml.AutoSize = true;
			this.chk_sendAsHtml.Checked = true;
			this.chk_sendAsHtml.CheckState = CheckState.Checked;
			this.chk_sendAsHtml.Dock = DockStyle.Top;
			this.chk_sendAsHtml.Location = new Point(3, 3);
			this.chk_sendAsHtml.Name = "chk_sendAsHtml";
			this.chk_sendAsHtml.Size = new Size(490, 17);
			this.chk_sendAsHtml.TabIndex = 36;
			this.chk_sendAsHtml.Text = "Send as Html";
			this.chk_sendAsHtml.UseVisualStyleBackColor = true;
			this.tabPage4.Controls.Add(this.browser_bodyPreview);
			this.tabPage4.Location = new Point(4, 25);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new Padding(3);
			this.tabPage4.Size = new Size(496, 208);
			this.tabPage4.TabIndex = 1;
			this.tabPage4.Text = "Body html preview";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.browser_bodyPreview.AllowNavigateExternalLink = false;
			this.browser_bodyPreview.Css = componentResourceManager.GetString("browser_bodyPreview.Css");
			this.browser_bodyPreview.Dock = DockStyle.Fill;
			this.browser_bodyPreview.Location = new Point(3, 3);
			this.browser_bodyPreview.Margin = new Padding(3, 4, 3, 4);
			this.browser_bodyPreview.MyPanel = null;
			this.browser_bodyPreview.Name = "browser_bodyPreview";
			this.browser_bodyPreview.Size = new Size(490, 205);
			this.browser_bodyPreview.TabIndex = 36;
			this.browser_bodyPreview.Title = "Html preview";
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tabControl1);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "ReportFunctionParameterControl_BatchEmail3";
			base.Size = new Size(632, 434);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((ISupportInitialize)this.txt_delayBetweenSendingEmails).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabControl2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004B96 File Offset: 0x00003B96
		public ReportFunctionParameterControl_BatchEmail3()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004BAF File Offset: 0x00003BAF
		public void Initialize(UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			this.da = da;
			this.tripleDES = tripleDES;
			this.browser_bodyPreview.HideRefreshButton();
			this.browser_bodyPreview.HideTitle();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004BD8 File Offset: 0x00003BD8
		private void AppendXmlAttribute(XmlDocument doc, ref XmlNode node, string attributeTitle, string attributeValue)
		{
			XmlAttribute xmlAttribute = doc.CreateAttribute(attributeTitle);
			xmlAttribute.Value = attributeValue;
			node.Attributes.Append(xmlAttribute);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004C08 File Offset: 0x00003C08
		private string GetEmailAttributeText(TextBox txt)
		{
			return txt.Text.Trim().Replace("#<", "#~").Replace(">#", "~#");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004C44 File Offset: 0x00003C44
		private string GetEmailAttributeDisplayText(string textFromXml)
		{
			return (textFromXml == null) ? "" : textFromXml.Replace("#~", "#<").Replace("~#", ">#");
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00004C80 File Offset: 0x00003C80
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00004F3F File Offset: 0x00003F3F
		public string Parameter
		{
			get
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml("<batchemails></batchemails>");
				XmlNode newChild = xmlDocument.CreateElement("batchemail");
				xmlDocument.DocumentElement.AppendChild(newChild);
				this.AppendXmlAttribute(xmlDocument, ref newChild, "emailhistorytypecode", this.txt_emailHistoryTypeCode.Text.Trim());
				this.AppendXmlAttribute(xmlDocument, ref newChild, "title", this.txt_title.Text.Trim());
				this.AppendXmlAttribute(xmlDocument, ref newChild, "to", this.GetEmailAttributeText(this.txt_to));
				this.AppendXmlAttribute(xmlDocument, ref newChild, "from", this.GetEmailAttributeText(this.txt_from));
				this.AppendXmlAttribute(xmlDocument, ref newChild, "cc", this.GetEmailAttributeText(this.txt_cc));
				this.AppendXmlAttribute(xmlDocument, ref newChild, "bcc", this.GetEmailAttributeText(this.txt_bcc));
				this.AppendXmlAttribute(xmlDocument, ref newChild, "subject", this.GetEmailAttributeText(this.txt_subject));
				this.AppendXmlAttribute(xmlDocument, ref newChild, "attachments", this.GetEmailAttributeText(this.txt_attach));
				this.AppendXmlAttribute(xmlDocument, ref newChild, "body", this.GetEmailAttributeText(this.txt_body));
				this.AppendXmlAttribute(xmlDocument, ref newChild, "bodyishtml", this.chk_sendAsHtml.Checked.ToString());
				this.AppendXmlAttribute(xmlDocument, ref newChild, "isactive", this.chk_enabled.Checked.ToString());
				this.AppendXmlAttribute(xmlDocument, ref newChild, "testmode", this.chk_testMode.Checked.ToString());
				this.AppendXmlAttribute(xmlDocument, ref newChild, "adminemail", this.txt_adminEmail.Text.Trim());
				this.AppendXmlAttribute(xmlDocument, ref newChild, "sendreport", this.chk_sendReport.Checked.ToString());
				this.AppendXmlAttribute(xmlDocument, ref newChild, "templateid", "0");
				this.AppendXmlAttribute(xmlDocument, ref newChild, "iconnum", this.GetIconNum().ToString());
				this.AppendXmlAttribute(xmlDocument, ref newChild, "delaybetweenemails", this.txt_delayBetweenSendingEmails.Value.ToString());
				this.AppendXmlAttribute(xmlDocument, ref newChild, "promptuser", this.chk_promptUser.Checked.ToString());
				string text = this.txt_priority.Text.Trim();
				if (!string.IsNullOrEmpty(text))
				{
					this.AppendXmlAttribute(xmlDocument, ref newChild, "priority", text);
				}
				StringBuilder stringBuilder = new StringBuilder();
				XmlTextWriter xmlTextWriter = new XmlTextWriter(new StringWriter(stringBuilder));
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlDocument.WriteTo(xmlTextWriter);
				xmlTextWriter.Flush();
				return stringBuilder.ToString();
			}
			set
			{
				this.SettingsToScreen(value);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004F4C File Offset: 0x00003F4C
		private void SettingsToScreen(string xml)
		{
			BatchEmail batchEmail = new BatchEmail(this.da, this.tripleDES, xml);
			this.txt_emailHistoryTypeCode.Text = batchEmail.EmailHistoryTypeCode;
			this.txt_title.Text = batchEmail.Title;
			this.txt_to.Text = this.GetEmailAttributeDisplayText(batchEmail.To);
			this.txt_from.Text = this.GetEmailAttributeDisplayText(batchEmail.From);
			this.txt_cc.Text = this.GetEmailAttributeDisplayText(batchEmail.Cc);
			this.txt_bcc.Text = this.GetEmailAttributeDisplayText(batchEmail.Bcc);
			this.txt_subject.Text = this.GetEmailAttributeDisplayText(batchEmail.Subject);
			this.txt_attach.Text = this.GetEmailAttributeDisplayText(batchEmail.Attachments);
			this.txt_body.Text = this.GetEmailAttributeDisplayText(batchEmail.Body);
			this.txt_delayBetweenSendingEmails.Value = batchEmail.DelayBetweenEmails;
			if (batchEmail.EmailArgs.ContainsKey("priority"))
			{
				string text = batchEmail.EmailArgs["priority"];
				if (!string.IsNullOrEmpty(text))
				{
					this.txt_priority.Text = text;
				}
			}
			this.chk_enabled.Checked = batchEmail.IsActive;
			this.chk_testMode.Checked = batchEmail.TestMode;
			this.chk_sendReport.Checked = batchEmail.SendReport;
			this.SetIcon(batchEmail.IconNum);
			this.txt_adminEmail.Text = batchEmail.AdminEmail;
			this.chk_promptUser.Checked = batchEmail.PromptUser;
			this.chk_sendAsHtml.Checked = batchEmail.BodyIsHtml;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000510C File Offset: 0x0000410C
		private void SetIcon(int iconNum)
		{
			if (iconNum >= 0)
			{
				using (IconPicker iconPicker = new IconPicker(this.da))
				{
					this.btn_icon.Tag = iconNum;
					this.btn_icon.Image = iconPicker.GetIconImage(iconNum);
				}
			}
			else
			{
				this.btn_icon.Tag = -1;
				this.btn_icon.Image = null;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000051A0 File Offset: 0x000041A0
		private int GetIconNum()
		{
			int result;
			if (this.btn_icon.Tag == null)
			{
				result = -1;
			}
			else
			{
				result = Convert.ToInt32(this.btn_icon.Tag);
			}
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000051DC File Offset: 0x000041DC
		private void btn_icon_Click(object sender, EventArgs e)
		{
			IconPicker iconPicker = new IconPicker(this.da);
			DialogResult dialogResult = iconPicker.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				this.btn_icon.Tag = iconPicker.selectedImageIndex;
				this.btn_icon.Image = iconPicker.GetIconImage(iconPicker.selectedImageIndex);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000523C File Offset: 0x0000423C
		private void btn_testSettings_Click(object sender, EventArgs e)
		{
			SmtpSettings smtpSettings = ReportFunction.GetSmtpSettings(this.da);
			DialogResult dialogResult = MessageBox.Show(string.Format("Here are the settings: \n{0}\nWould you like to send a test email?", smtpSettings.ToString()), "Test Smtp", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				string text = this.txt_adminEmail.Text.Trim();
				if (string.IsNullOrEmpty(text))
				{
					text = InputBox.GetUserInput(this, "Admin email", "The admin email address is missing.  Please enter the admin email address:", (smtpSettings.DefaultFrom == null) ? "" : smtpSettings.DefaultFrom);
					if (string.IsNullOrEmpty(text))
					{
						return;
					}
					this.txt_adminEmail.Text = text;
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.Cursor = Cursors.WaitCursor;
					try
					{
						IEmailManager emailManager = new EmailManager(new OperationContext
						{
							WhoAmI = 0
						});
						TPMailResult tpmailResult = emailManager.SendEmail(text, text, "ClockWork Smtp Test", "This is a test.", null, null, null, null);
						string errorMessage = tpmailResult.ErrorMessage;
						if (string.IsNullOrEmpty(errorMessage))
						{
							MessageBox.Show("It appears the send test email was successful.  Please check your email address to verify.");
						}
						else
						{
							MessageBox.Show("Send email failed: " + errorMessage);
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Something went wrong: " + ex.ToString());
					}
					finally
					{
						this.Cursor = Cursors.Default;
					}
				}
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000053CC File Offset: 0x000043CC
		private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.tabControl2.SelectedIndex == 1)
			{
				this.browser_bodyPreview.Css = "body { font-family: Arial } ";
				string text = this.txt_body.Text.Replace("#<", "<b><i> #&lt;").Replace(">#", "&gt;# </i></b>");
				if (this.chk_sendAsHtml.Checked && text.IndexOf("<br />") < 0 && text.IndexOf("<br>") < 0)
				{
					text = text.Replace(Environment.NewLine, "<br />");
				}
				this.browser_bodyPreview.ShowHtml(text);
			}
		}

		// Token: 0x040000A3 RID: 163
		private IContainer components = null;

		// Token: 0x040000A4 RID: 164
		private TextBox txt_adminEmail;

		// Token: 0x040000A5 RID: 165
		private Label label1;

		// Token: 0x040000A6 RID: 166
		private CheckBox chk_sendReport;

		// Token: 0x040000A7 RID: 167
		private CheckBox chk_testMode;

		// Token: 0x040000A8 RID: 168
		private CheckBox chk_enabled;

		// Token: 0x040000A9 RID: 169
		private Label label2;

		// Token: 0x040000AA RID: 170
		private Button btn_icon;

		// Token: 0x040000AB RID: 171
		private TextBox txt_emailHistoryTypeCode;

		// Token: 0x040000AC RID: 172
		private Label label10;

		// Token: 0x040000AD RID: 173
		private TabControl tabControl1;

		// Token: 0x040000AE RID: 174
		private TabPage tabPage1;

		// Token: 0x040000AF RID: 175
		private TabPage tabPage2;

		// Token: 0x040000B0 RID: 176
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x040000B1 RID: 177
		private Label label5;

		// Token: 0x040000B2 RID: 178
		private Label label7;

		// Token: 0x040000B3 RID: 179
		private TextBox txt_attach;

		// Token: 0x040000B4 RID: 180
		private Label label4;

		// Token: 0x040000B5 RID: 181
		private TextBox txt_subject;

		// Token: 0x040000B6 RID: 182
		private Label label3;

		// Token: 0x040000B7 RID: 183
		private Label label6;

		// Token: 0x040000B8 RID: 184
		private TextBox txt_bcc;

		// Token: 0x040000B9 RID: 185
		private TextBox txt_cc;

		// Token: 0x040000BA RID: 186
		private Label label8;

		// Token: 0x040000BB RID: 187
		private Label label9;

		// Token: 0x040000BC RID: 188
		private TextBox txt_to;

		// Token: 0x040000BD RID: 189
		private TextBox txt_from;

		// Token: 0x040000BE RID: 190
		private TabControl tabControl2;

		// Token: 0x040000BF RID: 191
		private TabPage tabPage3;

		// Token: 0x040000C0 RID: 192
		private TextBox txt_body;

		// Token: 0x040000C1 RID: 193
		private TabPage tabPage4;

		// Token: 0x040000C2 RID: 194
		private MyWebBrowser browser_bodyPreview;

		// Token: 0x040000C3 RID: 195
		private TextBox txt_title;

		// Token: 0x040000C4 RID: 196
		private Label label11;

		// Token: 0x040000C5 RID: 197
		private Label label12;

		// Token: 0x040000C6 RID: 198
		private Button btn_testSettings;

		// Token: 0x040000C7 RID: 199
		private CheckBox chk_sendAsHtml;

		// Token: 0x040000C8 RID: 200
		private Label label13;

		// Token: 0x040000C9 RID: 201
		private NumericUpDown txt_delayBetweenSendingEmails;

		// Token: 0x040000CA RID: 202
		private TextBox txt_priority;

		// Token: 0x040000CB RID: 203
		private Label label14;

		// Token: 0x040000CC RID: 204
		private Label label15;

		// Token: 0x040000CD RID: 205
		private CheckBox chk_promptUser;

		// Token: 0x040000CE RID: 206
		private UnivDataAdapter da;

		// Token: 0x040000CF RID: 207
		private TripleDESEncryptionClass tripleDES;
	}
}
