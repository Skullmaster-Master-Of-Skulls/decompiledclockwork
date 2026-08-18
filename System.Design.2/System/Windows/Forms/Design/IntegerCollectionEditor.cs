using System;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002FB RID: 763
	internal class IntegerCollectionEditor : CollectionEditor
	{
		// Token: 0x06001E5B RID: 7771 RVA: 0x00023ABB File Offset: 0x00021CBB
		public IntegerCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x000B7344 File Offset: 0x000B5544
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			return new IntegerCollectionEditor.IntegerCollectionForm(this);
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x000B734C File Offset: 0x000B554C
		protected override string HelpTopic
		{
			get
			{
				return "net.ComponentModel.IntegerCollectionEditor";
			}
		}

		// Token: 0x0200057E RID: 1406
		private class IntegerCollectionForm : CollectionEditor.CollectionForm
		{
			// Token: 0x06003265 RID: 12901 RVA: 0x001108B4 File Offset: 0x0010EAB4
			public IntegerCollectionForm(CollectionEditor editor) : base(editor)
			{
				this.editor = (IntegerCollectionEditor)editor;
				this.InitializeComponent();
			}

			// Token: 0x06003266 RID: 12902 RVA: 0x00110911 File Offset: 0x0010EB11
			private void Edit1_keyDown(object sender, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Escape)
				{
					this.cancelButton.PerformClick();
					e.Handled = true;
				}
			}

			// Token: 0x06003267 RID: 12903 RVA: 0x0011092F File Offset: 0x0010EB2F
			private void HelpButton_click(object sender, EventArgs e)
			{
				this.editor.ShowHelp();
			}

			// Token: 0x06003268 RID: 12904 RVA: 0x0011092F File Offset: 0x0010EB2F
			private void Form_HelpRequested(object sender, HelpEventArgs e)
			{
				this.editor.ShowHelp();
			}

			// Token: 0x06003269 RID: 12905 RVA: 0x0011093C File Offset: 0x0010EB3C
			private void InitializeComponent()
			{
				this.instruction.Location = new Point(4, 7);
				this.instruction.Size = new Size(422, 14);
				this.instruction.TabIndex = 0;
				this.instruction.TabStop = false;
				this.instruction.Text = SR.GetString("IntegerCollectionEditorInstruction");
				this.textEntry.Location = new Point(4, 22);
				this.textEntry.Size = new Size(422, 244);
				this.textEntry.TabIndex = 0;
				this.textEntry.Text = "";
				this.textEntry.AcceptsTab = false;
				this.textEntry.AcceptsReturn = true;
				this.textEntry.AutoSize = false;
				this.textEntry.Multiline = true;
				this.textEntry.ScrollBars = ScrollBars.Both;
				this.textEntry.WordWrap = false;
				this.textEntry.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				this.textEntry.KeyDown += this.Edit1_keyDown;
				this.okButton.Location = new Point(185, 274);
				this.okButton.Size = new Size(75, 23);
				this.okButton.TabIndex = 1;
				this.okButton.Text = SR.GetString("IntegerCollectionEditorOKCaption");
				this.okButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				this.okButton.DialogResult = DialogResult.OK;
				this.okButton.Click += this.OKButton_click;
				this.cancelButton.Location = new Point(264, 274);
				this.cancelButton.Size = new Size(75, 23);
				this.cancelButton.TabIndex = 2;
				this.cancelButton.Text = SR.GetString("IntegerCollectionEditorCancelCaption");
				this.cancelButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				this.cancelButton.DialogResult = DialogResult.Cancel;
				this.helpButton.Location = new Point(343, 274);
				this.helpButton.Size = new Size(75, 23);
				this.helpButton.TabIndex = 3;
				this.helpButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				this.helpButton.Text = SR.GetString("IntegerCollectionEditorHelpCaption");
				base.Location = new Point(7, 7);
				this.Text = SR.GetString("IntegerCollectionEditorTitle");
				base.AcceptButton = this.okButton;
				base.AutoScaleMode = AutoScaleMode.Font;
				base.AutoScaleDimensions = new SizeF(6f, 13f);
				base.CancelButton = this.cancelButton;
				base.ClientSize = new Size(429, 307);
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.ControlBox = false;
				base.ShowInTaskbar = false;
				base.StartPosition = FormStartPosition.CenterScreen;
				this.MinimumSize = new Size(300, 200);
				this.helpButton.Click += this.HelpButton_click;
				base.HelpRequested += this.Form_HelpRequested;
				base.Controls.Clear();
				base.Controls.AddRange(new Control[]
				{
					this.instruction,
					this.textEntry,
					this.okButton,
					this.cancelButton,
					this.helpButton
				});
			}

			// Token: 0x0600326A RID: 12906 RVA: 0x00110CA8 File Offset: 0x0010EEA8
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
				if (array.Length != 0 && array[array.Length - 1].Length == 0)
				{
					num--;
				}
				int[] array2 = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = array[i].Trim(trimChars);
					try
					{
						array2[i] = int.Parse(array[i], CultureInfo.CurrentCulture);
					}
					catch (Exception ex)
					{
						this.DisplayError(ex);
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
				}
				bool flag = true;
				if (num == items.Length)
				{
					int num2 = 0;
					while (num2 < num && array2[num2].Equals((int)items[num2]))
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
				object[] array3 = new object[num];
				for (int j = 0; j < num; j++)
				{
					array3[j] = array2[j];
				}
				base.Items = array3;
			}

			// Token: 0x0600326B RID: 12907 RVA: 0x00110DE4 File Offset: 0x0010EFE4
			protected override void OnEditValueChanged()
			{
				object[] items = base.Items;
				string text = string.Empty;
				for (int i = 0; i < items.Length; i++)
				{
					if (items[i] is int)
					{
						text += ((int)items[i]).ToString(CultureInfo.CurrentCulture);
						if (i != items.Length - 1)
						{
							text += "\r\n";
						}
					}
				}
				this.textEntry.Text = text;
			}

			// Token: 0x0400218C RID: 8588
			private Label instruction = new Label();

			// Token: 0x0400218D RID: 8589
			private TextBox textEntry = new TextBox();

			// Token: 0x0400218E RID: 8590
			private Button okButton = new Button();

			// Token: 0x0400218F RID: 8591
			private Button cancelButton = new Button();

			// Token: 0x04002190 RID: 8592
			private Button helpButton = new Button();

			// Token: 0x04002191 RID: 8593
			private IntegerCollectionEditor editor;
		}
	}
}
