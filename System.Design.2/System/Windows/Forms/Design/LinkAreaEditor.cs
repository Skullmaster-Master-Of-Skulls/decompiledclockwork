using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000304 RID: 772
	internal class LinkAreaEditor : UITypeEditor
	{
		// Token: 0x06001E9D RID: 7837 RVA: 0x000B75E0 File Offset: 0x000B57E0
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				IHelpService helpService = (IHelpService)provider.GetService(typeof(IHelpService));
				if (windowsFormsEditorService != null)
				{
					if (this.linkAreaUI == null)
					{
						this.linkAreaUI = DpiHelper.CreateInstanceInSystemAwareContext<LinkAreaEditor.LinkAreaUI>(() => new LinkAreaEditor.LinkAreaUI(this, helpService));
					}
					string text = string.Empty;
					PropertyDescriptor propertyDescriptor = null;
					if (context != null && context.Instance != null)
					{
						propertyDescriptor = TypeDescriptor.GetProperties(context.Instance)["Text"];
						if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
						{
							text = (string)propertyDescriptor.GetValue(context.Instance);
						}
					}
					string text2 = text;
					this.linkAreaUI.SampleText = text;
					this.linkAreaUI.Start(windowsFormsEditorService, value);
					if (windowsFormsEditorService.ShowDialog(this.linkAreaUI) == DialogResult.OK)
					{
						value = this.linkAreaUI.Value;
						text = this.linkAreaUI.SampleText;
						if (!text2.Equals(text) && propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
						{
							propertyDescriptor.SetValue(context.Instance, text);
						}
					}
					this.linkAreaUI.End();
				}
			}
			return value;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x040017D3 RID: 6099
		private LinkAreaEditor.LinkAreaUI linkAreaUI;

		// Token: 0x0200057F RID: 1407
		internal class LinkAreaUI : Form
		{
			// Token: 0x0600326C RID: 12908 RVA: 0x00110E54 File Offset: 0x0010F054
			public LinkAreaUI(LinkAreaEditor editor, IHelpService helpService)
			{
				this.editor = editor;
				this.helpService = helpService;
				this.InitializeComponent();
			}

			// Token: 0x170009EB RID: 2539
			// (get) Token: 0x0600326D RID: 12909 RVA: 0x00110EA7 File Offset: 0x0010F0A7
			// (set) Token: 0x0600326E RID: 12910 RVA: 0x00110EB4 File Offset: 0x0010F0B4
			public string SampleText
			{
				get
				{
					return this.sampleEdit.Text;
				}
				set
				{
					this.sampleEdit.Text = value;
					this.UpdateSelection();
				}
			}

			// Token: 0x170009EC RID: 2540
			// (get) Token: 0x0600326F RID: 12911 RVA: 0x00110EC8 File Offset: 0x0010F0C8
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x06003270 RID: 12912 RVA: 0x00110ED0 File Offset: 0x0010F0D0
			public void End()
			{
				this.edSvc = null;
				this.value = null;
			}

			// Token: 0x06003271 RID: 12913 RVA: 0x00110EE0 File Offset: 0x0010F0E0
			private void InitializeComponent()
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(LinkAreaEditor));
				this.caption = new Label();
				this.sampleEdit = new TextBox();
				this.okButton = new Button();
				this.cancelButton = new Button();
				this.okCancelTableLayoutPanel = new TableLayoutPanel();
				this.okCancelTableLayoutPanel.SuspendLayout();
				base.SuspendLayout();
				this.okButton.Click += this.okButton_click;
				componentResourceManager.ApplyResources(this.caption, "caption");
				this.caption.Margin = new Padding(3, 1, 3, 0);
				this.caption.Name = "caption";
				componentResourceManager.ApplyResources(this.sampleEdit, "sampleEdit");
				this.sampleEdit.Margin = new Padding(3, 2, 3, 3);
				this.sampleEdit.Name = "sampleEdit";
				this.sampleEdit.HideSelection = false;
				this.sampleEdit.ScrollBars = ScrollBars.Vertical;
				componentResourceManager.ApplyResources(this.okButton, "okButton");
				this.okButton.DialogResult = DialogResult.OK;
				this.okButton.Margin = new Padding(0, 0, 2, 0);
				this.okButton.Name = "okButton";
				componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
				this.cancelButton.DialogResult = DialogResult.Cancel;
				this.cancelButton.Margin = new Padding(3, 0, 0, 0);
				this.cancelButton.Name = "cancelButton";
				componentResourceManager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
				this.okCancelTableLayoutPanel.ColumnCount = 2;
				this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.okCancelTableLayoutPanel.Controls.Add(this.okButton, 0, 0);
				this.okCancelTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
				this.okCancelTableLayoutPanel.Margin = new Padding(3, 1, 3, 3);
				this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
				this.okCancelTableLayoutPanel.RowCount = 1;
				this.okCancelTableLayoutPanel.RowStyles.Add(new RowStyle());
				this.okCancelTableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this, "$this");
				base.AutoScaleMode = AutoScaleMode.Font;
				base.CancelButton = this.cancelButton;
				base.Controls.Add(this.okCancelTableLayoutPanel);
				base.Controls.Add(this.sampleEdit);
				base.Controls.Add(this.caption);
				base.HelpButton = true;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.Name = "LinkAreaEditor";
				base.ShowIcon = false;
				base.ShowInTaskbar = false;
				base.HelpButtonClicked += this.LinkAreaEditor_HelpButtonClicked;
				this.okCancelTableLayoutPanel.ResumeLayout(false);
				this.okCancelTableLayoutPanel.PerformLayout();
				base.ResumeLayout(false);
				base.PerformLayout();
			}

			// Token: 0x06003272 RID: 12914 RVA: 0x001111F7 File Offset: 0x0010F3F7
			private void okButton_click(object sender, EventArgs e)
			{
				this.value = new LinkArea(this.sampleEdit.SelectionStart, this.sampleEdit.SelectionLength);
			}

			// Token: 0x170009ED RID: 2541
			// (get) Token: 0x06003273 RID: 12915 RVA: 0x0011121F File Offset: 0x0010F41F
			private string HelpTopic
			{
				get
				{
					return "net.ComponentModel.LinkAreaEditor";
				}
			}

			// Token: 0x06003274 RID: 12916 RVA: 0x00111226 File Offset: 0x0010F426
			private void ShowHelp()
			{
				if (this.helpService != null)
				{
					this.helpService.ShowHelpFromKeyword(this.HelpTopic);
				}
			}

			// Token: 0x06003275 RID: 12917 RVA: 0x00111241 File Offset: 0x0010F441
			private void LinkAreaEditor_HelpButtonClicked(object sender, CancelEventArgs e)
			{
				e.Cancel = true;
				this.ShowHelp();
			}

			// Token: 0x06003276 RID: 12918 RVA: 0x00111250 File Offset: 0x0010F450
			public void Start(IWindowsFormsEditorService edSvc, object value)
			{
				this.edSvc = edSvc;
				this.value = value;
				this.UpdateSelection();
				base.ActiveControl = this.sampleEdit;
			}

			// Token: 0x06003277 RID: 12919 RVA: 0x00111274 File Offset: 0x0010F474
			private void UpdateSelection()
			{
				if (this.value is LinkArea)
				{
					LinkArea linkArea = (LinkArea)this.value;
					try
					{
						this.sampleEdit.SelectionStart = linkArea.Start;
						this.sampleEdit.SelectionLength = linkArea.Length;
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
				}
			}

			// Token: 0x04002192 RID: 8594
			private Label caption = new Label();

			// Token: 0x04002193 RID: 8595
			private TextBox sampleEdit = new TextBox();

			// Token: 0x04002194 RID: 8596
			private Button okButton = new Button();

			// Token: 0x04002195 RID: 8597
			private Button cancelButton = new Button();

			// Token: 0x04002196 RID: 8598
			private TableLayoutPanel okCancelTableLayoutPanel;

			// Token: 0x04002197 RID: 8599
			private LinkAreaEditor editor;

			// Token: 0x04002198 RID: 8600
			private IWindowsFormsEditorService edSvc;

			// Token: 0x04002199 RID: 8601
			private object value;

			// Token: 0x0400219A RID: 8602
			private IHelpService helpService;
		}
	}
}
