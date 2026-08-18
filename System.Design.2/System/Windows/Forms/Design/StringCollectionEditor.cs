using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200033A RID: 826
	internal class StringCollectionEditor : CollectionEditor
	{
		// Token: 0x06002082 RID: 8322 RVA: 0x00023ABB File Offset: 0x00021CBB
		public StringCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000C59C8 File Offset: 0x000C3BC8
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			return new StringCollectionEditor.StringCollectionForm(this);
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x000C59D0 File Offset: 0x000C3BD0
		protected override string HelpTopic
		{
			get
			{
				return "net.ComponentModel.StringCollectionEditor";
			}
		}

		// Token: 0x0200058F RID: 1423
		private class StringCollectionForm : CollectionEditor.CollectionForm
		{
			// Token: 0x060032D0 RID: 13008 RVA: 0x0011386B File Offset: 0x00111A6B
			public StringCollectionForm(CollectionEditor editor) : base(editor)
			{
				this.editor = (StringCollectionEditor)editor;
				this.InitializeComponent();
				this.HookEvents();
			}

			// Token: 0x060032D1 RID: 13009 RVA: 0x0011388C File Offset: 0x00111A8C
			private void Edit1_keyDown(object sender, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Escape)
				{
					this.cancelButton.PerformClick();
					e.Handled = true;
				}
			}

			// Token: 0x060032D2 RID: 13010 RVA: 0x001138AA File Offset: 0x00111AAA
			private void StringCollectionEditor_HelpButtonClicked(object sender, CancelEventArgs e)
			{
				e.Cancel = true;
				this.editor.ShowHelp();
			}

			// Token: 0x060032D3 RID: 13011 RVA: 0x001138BE File Offset: 0x00111ABE
			private void Form_HelpRequested(object sender, HelpEventArgs e)
			{
				this.editor.ShowHelp();
			}

			// Token: 0x060032D4 RID: 13012 RVA: 0x001138CC File Offset: 0x00111ACC
			private void HookEvents()
			{
				this.textEntry.KeyDown += this.Edit1_keyDown;
				this.okButton.Click += this.OKButton_click;
				base.HelpButtonClicked += this.StringCollectionEditor_HelpButtonClicked;
			}

			// Token: 0x060032D5 RID: 13013 RVA: 0x0011391C File Offset: 0x00111B1C
			private void InitializeComponent()
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(StringCollectionEditor));
				this.instruction = new Label();
				this.textEntry = new TextBox();
				this.okButton = new Button();
				this.cancelButton = new Button();
				this.overarchingLayoutPanel = new TableLayoutPanel();
				this.overarchingLayoutPanel.SuspendLayout();
				base.SuspendLayout();
				componentResourceManager.ApplyResources(this.instruction, "instruction");
				this.overarchingLayoutPanel.SetColumnSpan(this.instruction, 2);
				this.instruction.Name = "instruction";
				componentResourceManager.ApplyResources(this.textEntry, "textEntry");
				this.overarchingLayoutPanel.SetColumnSpan(this.textEntry, 2);
				this.textEntry.AcceptsTab = true;
				this.textEntry.AcceptsReturn = true;
				this.textEntry.Name = "textEntry";
				componentResourceManager.ApplyResources(this.okButton, "okButton");
				this.okButton.DialogResult = DialogResult.OK;
				this.okButton.Name = "okButton";
				componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
				this.cancelButton.DialogResult = DialogResult.Cancel;
				this.cancelButton.Name = "cancelButton";
				componentResourceManager.ApplyResources(this.overarchingLayoutPanel, "overarchingLayoutPanel");
				this.overarchingLayoutPanel.Controls.Add(this.instruction, 0, 0);
				this.overarchingLayoutPanel.Controls.Add(this.textEntry, 0, 2);
				this.overarchingLayoutPanel.Controls.Add(this.okButton, 0, 3);
				this.overarchingLayoutPanel.Controls.Add(this.cancelButton, 1, 3);
				this.overarchingLayoutPanel.Name = "overarchingLayoutPanel";
				componentResourceManager.ApplyResources(this, "$this");
				base.AutoScaleMode = AutoScaleMode.Font;
				base.Controls.Add(this.overarchingLayoutPanel);
				base.HelpButton = true;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.Name = "StringCollectionEditor";
				base.ShowIcon = false;
				base.ShowInTaskbar = false;
				this.overarchingLayoutPanel.ResumeLayout(false);
				this.overarchingLayoutPanel.PerformLayout();
				base.HelpRequested += this.Form_HelpRequested;
				base.ResumeLayout(false);
				base.PerformLayout();
			}

			// Token: 0x060032D6 RID: 13014 RVA: 0x00113B64 File Offset: 0x00111D64
			private void OKButton_click(object sender, EventArgs e)
			{
				char[] separator = new char[]
				{
					'\n'
				};
				char[] trimChars = new char[]
				{
					'\r'
				};
				string[] array = this.textEntry.Text.Split(separator);
				object[] items = base.Items;
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					array[i] = array[i].Trim(trimChars);
				}
				bool flag = true;
				if (num == items.Length)
				{
					int num2 = 0;
					while (num2 < num && array[num2].Equals((string)items[num2]))
					{
						num2++;
					}
					if (num2 == num)
					{
						flag = false;
					}
				}
				if (!flag)
				{
					base.DialogResult = DialogResult.Cancel;
					return;
				}
				if (array.Length != 0 && array[array.Length - 1].Length == 0)
				{
					num--;
				}
				object[] array2 = new object[num];
				for (int j = 0; j < num; j++)
				{
					array2[j] = array[j];
				}
				base.Items = array2;
			}

			// Token: 0x060032D7 RID: 13015 RVA: 0x00113C50 File Offset: 0x00111E50
			protected override void OnEditValueChanged()
			{
				object[] items = base.Items;
				string text = string.Empty;
				for (int i = 0; i < items.Length; i++)
				{
					if (items[i] is string)
					{
						text += (string)items[i];
						if (i != items.Length - 1)
						{
							text += "\r\n";
						}
					}
				}
				this.textEntry.Text = text;
			}

			// Token: 0x040021F3 RID: 8691
			private Label instruction;

			// Token: 0x040021F4 RID: 8692
			private TextBox textEntry;

			// Token: 0x040021F5 RID: 8693
			private Button okButton;

			// Token: 0x040021F6 RID: 8694
			private Button cancelButton;

			// Token: 0x040021F7 RID: 8695
			private TableLayoutPanel overarchingLayoutPanel;

			// Token: 0x040021F8 RID: 8696
			private StringCollectionEditor editor;
		}
	}
}
