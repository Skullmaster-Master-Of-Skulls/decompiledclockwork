using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoComboBox;
using AutoComboBox.HelperForms;
using AutoComboBox.InputDialogControls;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using ImportExportClassLibrary.Properties;
using MailMerging;
using Microsoft.Win32;
using TechnoPro.Common.UI.WinForms.Settings.SettingCtrls;
using TechnoPro.Common.UI.WinForms.TestBooking.Forms.ContextPageInfo;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x02000026 RID: 38
	public partial class TemplateInDatabaseListDialog : Form
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000FF RID: 255 RVA: 0x00006864 File Offset: 0x00005864
		// (remove) Token: 0x06000100 RID: 256 RVA: 0x0000689C File Offset: 0x0000589C
		public event GetCodesHandler OnCodesRequested;

		// Token: 0x06000101 RID: 257 RVA: 0x000068D1 File Offset: 0x000058D1
		private List<MailMergeCodeValue> FireOnCodesRequested()
		{
			if (this.OnCodesRequested != null)
			{
				return this.OnCodesRequested();
			}
			return null;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000068E8 File Offset: 0x000058E8
		public TemplateInDatabaseListDialog.DateType TypeOfDate
		{
			get
			{
				if (this.rbtn_specificDateTime.Checked)
				{
					return TemplateInDatabaseListDialog.DateType.OnlyShowForSpecificDateTime;
				}
				if (this.rbtn_useExisting.Checked)
				{
					return TemplateInDatabaseListDialog.DateType.UseWhatWasOnTheTestListing;
				}
				if (this.rb_useWhatISelected.Checked)
				{
					return TemplateInDatabaseListDialog.DateType.UseWhatISelectedOnTheTestListing;
				}
				return TemplateInDatabaseListDialog.DateType.Unknown;
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006918 File Offset: 0x00005918
		public TemplateInDatabaseListDialog(UnivDataAdapter da, TemplateInDatabase.TemplateDialogType templateDialogType, string prefixGroup, TemplateInDatabaseCollection templates, string title, string captionMessage, string defaultNameToSelect, bool canModifyTemplates, DataTable t_forSorting)
		{
			this.t_forSorting = t_forSorting;
			this.da = da;
			this.prefixGroup = prefixGroup;
			this.templateDialogType = templateDialogType;
			this.templates = templates;
			this.defaultNameToSelect = defaultNameToSelect;
			this.canModifyTemplates = canModifyTemplates;
			this.InitializeComponent();
			this.Text = title;
			this.lbl_captionMessage.Text = captionMessage;
			if (canModifyTemplates)
			{
				this.toolstrip_modifyTemplates.Visible = true;
			}
			else
			{
				this.editEmailForThisTemplateToolStripMenuItem.Enabled = false;
			}
			if (templateDialogType != TemplateInDatabase.TemplateDialogType.Tests)
			{
				return;
			}
			this.gb_dates.Visible = true;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000069AC File Offset: 0x000059AC
		protected void TemplateInDatabaseListDialog_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.O && this.templateDialogType == TemplateInDatabase.TemplateDialogType.Tests)
			{
				FrmContextInfoExportExamListing frmContextInfoExportExamListing = new FrmContextInfoExportExamListing();
				frmContextInfoExportExamListing.Init();
				frmContextInfoExportExamListing.ShowDialog(this);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000069E8 File Offset: 0x000059E8
		private void TemplateInDatabaseListDialog_Load(object sender, EventArgs e)
		{
			this.RefreshTemplatesList();
			this.dtp_date.Value = DateTime.Now;
			this.cmb_time.SelectedIndex = 0;
			string registryValueString = RegistryFunctions.GetRegistryValueString(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_filename", false);
			string registryValueString2 = RegistryFunctions.GetRegistryValueString(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_dates", false);
			string registryValueString3 = RegistryFunctions.GetRegistryValueString(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_date", false);
			string registryValueString4 = RegistryFunctions.GetRegistryValueString(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_time", false);
			string registryValueString5 = RegistryFunctions.GetRegistryValueString(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_sorting", false);
			string registryValueString6 = RegistryFunctions.GetRegistryValueString(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_includecancellednoshow", false);
			this.chk_includeCancelledAndNoshow.Checked = (!string.IsNullOrEmpty(registryValueString6) && registryValueString6.Equals("1"));
			if (registryValueString.Length > 0)
			{
				this.SelectTemplate(registryValueString);
			}
			if (registryValueString2 == "0")
			{
				this.rbtn_useExisting.Checked = true;
			}
			else if (registryValueString2 == "1")
			{
				this.rbtn_specificDateTime.Checked = true;
			}
			else if (registryValueString2 == "2")
			{
				this.rb_useWhatISelected.Checked = true;
			}
			if (registryValueString3.Trim().Length > 0)
			{
				try
				{
					this.dtp_date.Value = DateTime.Parse(registryValueString3);
				}
				catch
				{
				}
			}
			if (registryValueString4.Length > 0)
			{
				this.SelectCmbItem(this.cmb_time, registryValueString4);
			}
			if (registryValueString5.Length > 0)
			{
				this.txt_sort.Text = registryValueString5;
			}
			if (this.templateDialogType == TemplateInDatabase.TemplateDialogType.Email)
			{
				this.btn_useBlankTemplate.Visible = true;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006B98 File Offset: 0x00005B98
		private void SelectTemplate(string name)
		{
			string strB = name.ToLower().Trim();
			foreach (object obj in this.lv_templates.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				string text = listViewItem.Text.ToLower().Trim();
				if (text.CompareTo(strB) == 0)
				{
					listViewItem.Selected = true;
					break;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006C24 File Offset: 0x00005C24
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006C31 File Offset: 0x00005C31
		public bool Button_SelectTemplate_Visible
		{
			get
			{
				return this.btn_selectTemplate.Visible;
			}
			set
			{
				this.btn_selectTemplate.Visible = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006C3F File Offset: 0x00005C3F
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006C4C File Offset: 0x00005C4C
		public bool Button_ChooseFile_Visible
		{
			get
			{
				return this.btn_chooseAFile.Visible;
			}
			set
			{
				this.btn_chooseAFile.Visible = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006C5A File Offset: 0x00005C5A
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006C67 File Offset: 0x00005C67
		public string CancelButtonText
		{
			get
			{
				return this.btn_cancel.Text;
			}
			set
			{
				this.btn_cancel.Text = value;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006C78 File Offset: 0x00005C78
		private void SelectCmbItem(AutoComboBox cmb, string text)
		{
			string strB = text.ToLower().Trim();
			for (int i = 0; i < cmb.Items.Count; i++)
			{
				string text2 = cmb.Items[i].ToString();
				string text3 = text2.Trim().ToLower();
				if (text3.CompareTo(strB) == 0)
				{
					cmb.SelectedIndex = i;
					return;
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006CD8 File Offset: 0x00005CD8
		private void RefreshTemplatesList()
		{
			this.templates = TemplateInDatabase.GetAvailableTemplatesNoFiles(this.da, this.prefixGroup);
			this.lv_templates.BeginUpdate();
			this.lv_templates.SuspendLayout();
			this.lv_templates.Items.Clear();
			foreach (TemplateInDatabase templateInDatabase in this.templates)
			{
				ListViewItem listViewItem = new ListViewItem(string.Format("{0} ({1})", templateInDatabase.Name, templateInDatabase.TemplateId.ToString()));
				listViewItem.Tag = templateInDatabase;
				this.lv_templates.Items.Add(listViewItem);
			}
			this.lv_templates.ResumeLayout();
			this.lv_templates.EndUpdate();
			if (this.defaultNameToSelect.Length > 0)
			{
				foreach (object obj in this.lv_templates.Items)
				{
					ListViewItem listViewItem2 = (ListViewItem)obj;
					if (listViewItem2.Text.CompareTo(this.defaultNameToSelect) == 0)
					{
						listViewItem2.Selected = true;
						break;
					}
				}
				this.defaultNameToSelect = "";
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006E38 File Offset: 0x00005E38
		private void lv_templates_DoubleClick(object sender, EventArgs e)
		{
			this.btn_selectTemplate_Click(this.btn_selectTemplate, null);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006E47 File Offset: 0x00005E47
		private void btn_refreshPreview_Click(object sender, EventArgs e)
		{
			this.RefreshPreview();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006E50 File Offset: 0x00005E50
		private void RefreshPreview()
		{
			TemplateInDatabase selectedTemplate = this.GetSelectedTemplate(true);
			if (selectedTemplate != null)
			{
				string filename = selectedTemplate.Filename;
				if (string.IsNullOrEmpty(filename))
				{
					return;
				}
				string text = Path.GetExtension(filename).ToLower();
				if (text.CompareTo(".doc") == 0)
				{
					TemplatesClass.PreviewWord(filename, this.rtf_preview);
					return;
				}
				if (text.CompareTo(".rtf") == 0)
				{
					this.rtf_preview.Rtf = File.ReadAllText(filename);
					return;
				}
				this.rtf_preview.Text = File.ReadAllText(filename);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006ECE File Offset: 0x00005ECE
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006ED6 File Offset: 0x00005ED6
		private void btn_selectTemplate_Click(object sender, EventArgs e)
		{
			if (this.lv_templates.SelectedItems.Count > 0)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
				return;
			}
			MessageBox.Show("Please select a template from the list first.");
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006F04 File Offset: 0x00005F04
		public void TurnOnOptionToUseWhatISelectedOnTheTestBookingsList()
		{
			this.rb_useWhatISelected.Visible = true;
			this.rb_useWhatISelected.Checked = true;
			this.chk_includeCancelledAndNoshow.Visible = true;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006F2C File Offset: 0x00005F2C
		public TemplateInDatabase SelectedTemplate
		{
			get
			{
				if (this.manualTemplateFilename != null)
				{
					return new TemplateInDatabase(this.manualTemplateFilename);
				}
				if (this.useBlankTemplate)
				{
					return new TemplateInDatabase();
				}
				if (this.lv_templates.SelectedItems.Count > 0)
				{
					return this.GetSelectedTemplate(true);
				}
				return null;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006F7C File Offset: 0x00005F7C
		private void btn_sort_Click(object sender, EventArgs e)
		{
			InputMultipleOrderedItems inputMultipleOrderedItems = new InputMultipleOrderedItems("Sort", "Please select the column(s) you would like to sort by:", this.t_forSorting.Columns, this.txt_sort.Text);
			DialogResult dialogResult = inputMultipleOrderedItems.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				this.txt_sort.Text = inputMultipleOrderedItems.ChosenItems_string;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006FCC File Offset: 0x00005FCC
		private void btn_addTemplate_Click(object sender, EventArgs e)
		{
			string userInput = InputBox.GetUserInput(this, "Add new template", "Please enter a title for the new template:", "New template");
			if (userInput != null && userInput.Trim().Length > 0)
			{
				TemplateInDatabase.CreateNewTemplate(this.da, this.prefixGroup + "_" + userInput);
				this.defaultNameToSelect = userInput;
				this.RefreshTemplatesList();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000702C File Offset: 0x0000602C
		private void EditEmailXmlTemplate(TemplateInDatabase template, string templateText)
		{
			EmailSettingCtrl emailSettingCtrl = new EmailSettingCtrl();
			emailSettingCtrl.SetStringValue(templateText);
			emailSettingCtrl.IsActiveCheckboxVisible = false;
			DialogResult dialogResult = frmSaveClose.ShowForm(this, template.TemplateNameWithPrefix, emailSettingCtrl);
			if (dialogResult == DialogResult.OK)
			{
				string text = (emailSettingCtrl.Value == null) ? "" : emailSettingCtrl.Value.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					TemplateInDatabase.ReplaceTemplateFileWithText(this.da, template.TemplateId, text, ".xml");
					this.RefreshTemplatesList();
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000070A0 File Offset: 0x000060A0
		private List<TemplateInDatabase> GetSelectedTemplates(bool includeFiles)
		{
			List<TemplateInDatabase> list = new List<TemplateInDatabase>();
			foreach (object obj in this.lv_templates.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				TemplateInDatabase templateInDatabase = (TemplateInDatabase)listViewItem.Tag;
				if (includeFiles)
				{
					list.Add(TemplateInDatabase.LoadTemplate(this.da, templateInDatabase.TemplateId));
				}
				else
				{
					list.Add(templateInDatabase);
				}
			}
			return list;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007130 File Offset: 0x00006130
		private TemplateInDatabase GetSelectedTemplate(bool includeFile)
		{
			if (this.lv_templates.SelectedItems.Count <= 0)
			{
				return null;
			}
			TemplateInDatabase templateInDatabase = (TemplateInDatabase)this.lv_templates.SelectedItems[0].Tag;
			if (includeFile)
			{
				return TemplateInDatabase.LoadTemplate(this.da, templateInDatabase.TemplateId);
			}
			return templateInDatabase;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00007184 File Offset: 0x00006184
		public bool IncludeCancelledAndNoshow
		{
			get
			{
				return this.chk_includeCancelledAndNoshow.Checked;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007194 File Offset: 0x00006194
		private void AskUserHowToAddTemplateFile(TemplateInDatabase template)
		{
			AppDelete appDelete = new AppDelete(AppDeleteFunctionality.CheckboxIsHidden);
			appDelete.Button2Text = "&Create a new email template\nThis template will be used as an email template - click to create a new blank email template";
			appDelete.Button1Text = "&Specify a file\nChoose a file to attach to this template";
			appDelete.Button3Text = "&Cancel\nYou can use the 'replace' button on the previous dialog box to specify a file that should be used for this template";
			appDelete.Title = "The template text has not been specified yet.";
			DialogResult dialogResult = appDelete.ShowDialog(this);
			if (dialogResult == DialogResult.Yes)
			{
				if (appDelete.ButtonClicked == 1)
				{
					OpenFileDialog openFileDialog = new OpenFileDialog();
					DialogResult dialogResult2 = openFileDialog.ShowDialog(this);
					if (dialogResult2 == DialogResult.OK)
					{
						string fileName = openFileDialog.FileName;
						return;
					}
				}
				else if (appDelete.ButtonClicked == 2)
				{
					this.EditEmailXmlTemplate(template, "");
				}
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007218 File Offset: 0x00006218
		private void btn_editTemplate_Click(object sender, EventArgs e)
		{
			TemplateInDatabase selectedTemplate = this.GetSelectedTemplate(true);
			if (selectedTemplate != null)
			{
				int templateId = selectedTemplate.TemplateId;
				string text = (templateId > 0) ? selectedTemplate.FilenameDontAskUsingOpenFileDialog : "";
				if (templateId < 0)
				{
					MessageBox.Show("Something is wrong; invalid template id");
					return;
				}
				if (string.IsNullOrEmpty(text))
				{
					this.AskUserHowToAddTemplateFile(selectedTemplate);
					return;
				}
				string text2 = Path.GetExtension(text).ToLower();
				if (text2.Equals(".html") || text2.Equals(".htm"))
				{
					string userInput = InputBox.GetUserInput(this, "Edit template", "Please make your changes:", File.ReadAllText(text), 500, false);
					if (!string.IsNullOrEmpty(userInput))
					{
						TemplateInDatabase.ReplaceTemplateFileWithText(this.da, templateId, userInput, text2);
						this.RefreshTemplatesList();
						return;
					}
				}
				else if (text2.Equals(".txt") || text2.Equals(".xml"))
				{
					string text3 = File.ReadAllText(text);
					if (text3.StartsWith("<email>"))
					{
						this.EditEmailXmlTemplate(selectedTemplate, text3);
						return;
					}
					string userInput2 = InputBox.GetUserInput(this, "Edit template", "Please make your changes:", File.ReadAllText(text), 500, false);
					if (!string.IsNullOrEmpty(userInput2))
					{
						TemplateInDatabase.ReplaceTemplateFileWithText(this.da, templateId, userInput2, text2);
						this.RefreshTemplatesList();
						return;
					}
				}
				else
				{
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					processStartInfo.FileName = text;
					processStartInfo.UseShellExecute = true;
					new Process
					{
						StartInfo = processStartInfo
					}.Start();
					DialogResult dialogResult = MessageBox.Show("Click OK to update the template with your changes", "Save your changes then click OK", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.OK)
					{
						try
						{
							IL_174:
							string parameterValue = BinaryFile.ConvertFileToBase64Text(text);
							this.da.SelectCommand.CommandText = "UPDATE emailtemplates SET emisc=@emisc WHERE templateid=@templateid";
							this.da.SelectCommand.Parameters.Clear();
							this.da.SelectCommand.Parameters.Add("@emisc", parameterValue);
							this.da.SelectCommand.Parameters.Add("@templateid", templateId);
							this.da.Fill(new DataTable());
							if (this.lv_templates.SelectedItems.Count > 0)
							{
								this.defaultNameToSelect = this.lv_templates.SelectedItems[0].Text;
							}
							this.RefreshTemplatesList();
							MessageBox.Show("Done!");
						}
						catch (Exception)
						{
							DialogResult dialogResult2 = MessageBox.Show("Something went wrong trying to update the template.  Windows puts a lock on the template when you are editing it - if you have left the template open please save and close it.  Click the 'Retry' button to try a second time after you have closed the template.", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation);
							if (dialogResult2 == DialogResult.Retry)
							{
								goto IL_174;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007490 File Offset: 0x00006490
		private void btn_replaceTemplate_Click(object sender, EventArgs e)
		{
			TemplateInDatabase selectedTemplate = this.GetSelectedTemplate(false);
			if (selectedTemplate != null)
			{
				int templateId = selectedTemplate.TemplateId;
				if (templateId >= 0)
				{
					OpenFileDialog openFileDialog = new OpenFileDialog();
					DialogResult dialogResult = openFileDialog.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						Exception ex = TemplateInDatabase.ReplaceTemplateFile(this.da, templateId, openFileDialog.FileName);
						if (ex != null)
						{
							MessageBox.Show("Something went wrong: " + ex.ToString());
							return;
						}
						MessageBox.Show("Done!");
					}
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00007500 File Offset: 0x00006500
		private void btn_deleteTemplate_Click(object sender, EventArgs e)
		{
			TemplateInDatabase selectedTemplate = this.GetSelectedTemplate(false);
			if (selectedTemplate != null)
			{
				int templateId = selectedTemplate.TemplateId;
				if (templateId >= 0)
				{
					DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this template?", "Delete template", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes)
					{
						this.da.SelectCommand.CommandText = "DELETE FROM emailtemplates WHERE templateid=@tid";
						this.da.SelectCommand.Parameters.Clear();
						this.da.SelectCommand.Parameters.Add("@tid", templateId);
						this.da.Fill(new DataTable());
						this.RefreshTemplatesList();
					}
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000075A0 File Offset: 0x000065A0
		private void TemplateInDatabaseListDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				string valueObject = (this.lv_templates.SelectedItems.Count > 0) ? this.lv_templates.SelectedItems[0].Text.ToLower().Trim() : "";
				RegistryFunctions.SetRegistryValue(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_filename", valueObject, false);
				string valueObject2;
				if (this.rbtn_useExisting.Checked)
				{
					valueObject2 = "0";
				}
				else if (this.rb_useWhatISelected.Checked)
				{
					valueObject2 = "2";
				}
				else
				{
					valueObject2 = "1";
				}
				RegistryFunctions.SetRegistryValue(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_dates", valueObject2, false);
				RegistryFunctions.SetRegistryValue(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_date", this.dtp_date.Value.ToString("yyyy-MM-dd"), false);
				RegistryFunctions.SetRegistryValue(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_time", this.cmb_time.Text, false);
				RegistryFunctions.SetRegistryValue(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_sorting", this.txt_sort.Text, false);
				RegistryFunctions.SetRegistryValue(Registry.CurrentUser, RegistryFunctions.registryBreakdown, "tests_excel_template_includecancellednoshow", this.chk_includeCancelledAndNoshow.Checked ? "1" : "0", false);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000076F4 File Offset: 0x000066F4
		public DateTime GetSpecificDate()
		{
			if (this.rbtn_useExisting.Checked)
			{
				return DateTime.MinValue;
			}
			DateTime value = this.dtp_date.Value;
			string text;
			if (this.cmb_time.SelectedIndex >= 0)
			{
				text = this.cmb_time.Items[this.cmb_time.SelectedIndex].ToString();
			}
			else
			{
				text = "12:00 am";
			}
			if (text.Length > 0 && text[0] == '<')
			{
				text = "12:00 am";
			}
			return DateTime.Parse(value.ToString("yyyy-MM-dd") + " " + text);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000778D File Offset: 0x0000678D
		public string GetSort()
		{
			return this.txt_sort.Text;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000779A File Offset: 0x0000679A
		private void rbtn_useExisting_CheckedChanged(object sender, EventArgs e)
		{
			this.p_specificDateTime.Enabled = this.rbtn_specificDateTime.Checked;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000077B2 File Offset: 0x000067B2
		private void rbtn_specificDateTime_CheckedChanged(object sender, EventArgs e)
		{
			this.p_specificDateTime.Enabled = this.rbtn_specificDateTime.Checked;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000077CA File Offset: 0x000067CA
		public bool UseBlankTemplate
		{
			get
			{
				return this.useBlankTemplate;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000077D2 File Offset: 0x000067D2
		private void btn_useBlankTemplate_Click(object sender, EventArgs e)
		{
			this.useBlankTemplate = true;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000077E8 File Offset: 0x000067E8
		private void btn_chooseAFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			DialogResult dialogResult = openFileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				this.manualTemplateFilename = openFileDialog.FileName;
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007820 File Offset: 0x00006820
		private void cm_templates_Opening(object sender, CancelEventArgs e)
		{
			this.editEmailForThisTemplateToolStripMenuItem.Enabled = (this.lv_templates.SelectedItems.Count > 0);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007840 File Offset: 0x00006840
		private void editEmailForThisTemplateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TemplateInDatabase selectedTemplate = this.GetSelectedTemplate(true);
			if (selectedTemplate != null)
			{
				TemplateInDatabaseEmailSettings templateInDatabaseEmailSettings = new TemplateInDatabaseEmailSettings(this.da, null, selectedTemplate.TemplateId);
				templateInDatabaseEmailSettings.ShowDialog(this);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00007873 File Offset: 0x00006873
		public bool UserChoseExportToExcel
		{
			get
			{
				return this.userChoseExportToExcel;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000787B File Offset: 0x0000687B
		private void btn_exportToExcel_Click(object sender, EventArgs e)
		{
			this.userChoseExportToExcel = true;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007891 File Offset: 0x00006891
		public void ShowExportToExcelButton()
		{
			this.btn_exportToExcel.Visible = true;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000078A0 File Offset: 0x000068A0
		private void btn_backup_Click(object sender, EventArgs e)
		{
			List<TemplateInDatabase> selectedTemplates = this.GetSelectedTemplates(true);
			if (selectedTemplates.Count == 1)
			{
				TemplateInDatabase templateInDatabase = selectedTemplates[0];
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				string filenameDontAskUsingOpenFileDialog = templateInDatabase.FilenameDontAskUsingOpenFileDialog;
				if (!string.IsNullOrEmpty(filenameDontAskUsingOpenFileDialog))
				{
					saveFileDialog.Filter = string.Format("All files|*.*|*{0}|*{0}", Path.GetExtension(filenameDontAskUsingOpenFileDialog));
					DialogResult dialogResult = saveFileDialog.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						File.Copy(filenameDontAskUsingOpenFileDialog, saveFileDialog.FileName);
						return;
					}
				}
			}
			else if (selectedTemplates.Count > 1)
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				DialogResult dialogResult = folderBrowserDialog.ShowDialog(this);
				if (dialogResult == DialogResult.OK)
				{
					foreach (TemplateInDatabase templateInDatabase2 in selectedTemplates)
					{
						new SaveFileDialog();
						string filenameDontAskUsingOpenFileDialog2 = templateInDatabase2.FilenameDontAskUsingOpenFileDialog;
						if (!string.IsNullOrEmpty(filenameDontAskUsingOpenFileDialog2))
						{
							string fileName = Path.GetFileName(filenameDontAskUsingOpenFileDialog2);
							File.Copy(filenameDontAskUsingOpenFileDialog2, Path.Combine(folderBrowserDialog.SelectedPath, fileName));
						}
					}
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000079A4 File Offset: 0x000069A4
		private void lv_templates_SizeChanged_1(object sender, EventArgs e)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000079A8 File Offset: 0x000069A8
		private void btn_viewCodes_Click(object sender, EventArgs e)
		{
			List<MailMergeCodeValue> list = this.FireOnCodesRequested();
			if (list != null)
			{
				DataGridView2.ShowDataGridView2(this, "Codes", list, new string[]
				{
					"code",
					"value"
				});
				return;
			}
			MessageBox.Show("Codes are not available.");
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000079F0 File Offset: 0x000069F0
		private void lv_templates_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.lv_templates_DoubleClick(this.lv_templates, new EventArgs());
			}
		}

		// Token: 0x04000054 RID: 84
		private TemplateInDatabaseCollection templates;

		// Token: 0x04000055 RID: 85
		private string defaultNameToSelect;

		// Token: 0x04000056 RID: 86
		private bool canModifyTemplates;

		// Token: 0x04000057 RID: 87
		private TemplateInDatabase.TemplateDialogType templateDialogType;

		// Token: 0x04000058 RID: 88
		private string prefixGroup;

		// Token: 0x04000059 RID: 89
		private UnivDataAdapter da;

		// Token: 0x0400005A RID: 90
		private DataTable t_forSorting;

		// Token: 0x0400005B RID: 91
		private string manualTemplateFilename;

		// Token: 0x0400005C RID: 92
		private bool useBlankTemplate;

		// Token: 0x0400005D RID: 93
		private bool userChoseExportToExcel;

		// Token: 0x02000027 RID: 39
		public enum DateType
		{
			// Token: 0x04000086 RID: 134
			Unknown,
			// Token: 0x04000087 RID: 135
			UseWhatWasOnTheTestListing,
			// Token: 0x04000088 RID: 136
			OnlyShowForSpecificDateTime,
			// Token: 0x04000089 RID: 137
			UseWhatISelectedOnTheTestListing
		}
	}
}
