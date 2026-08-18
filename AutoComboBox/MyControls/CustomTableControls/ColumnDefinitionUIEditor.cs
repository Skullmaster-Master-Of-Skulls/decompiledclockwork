using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200005A RID: 90
	public class ColumnDefinitionUIEditor : UserControl
	{
		// Token: 0x0600032F RID: 815 RVA: 0x00019B00 File Offset: 0x00018B00
		public ColumnDefinitionUIEditor(ColumnDefinition target, Dictionary<string, string> existedNames, Form parent, bool showApply)
		{
			this.__existedNames = existedNames;
			TopPanel topPanel = new TopPanel();
			topPanel.Location = new Point(0, 0);
			topPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
			base.Controls.Add(this.topPanel = topPanel);
			this.tb = topPanel.ColumnNameTextBox;
			this.cb = topPanel.ColumnTypeComboBox;
			this.cb.DataSource = ColumnTypeDefUtil.StringRepresentationsOfTypes;
			BtmPanel btmPanel = new BtmPanel(parent, new BtmPanel.BoolReturnMethod(this.commitToChange), new BtmPanel.VoidReturnMethod(this.spawnClone), showApply);
			btmPanel.setAnchorAndGoToDefaultLocation(AnchorStyles.Bottom | AnchorStyles.Right, base.Size);
			base.Controls.Add(this.btmPanel = btmPanel);
			this.__Target = target;
			base.Load += this.OnLoad;
			this.cb.SelectedIndexChanged += this.SelectedIndexChanged;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00019C04 File Offset: 0x00018C04
		private void OnLoad(object sender, EventArgs e)
		{
			this.tb.Text = this.__Target.ColumnName;
			ColumnTypeDef columnType = this.__Target.ColumnType;
			int selectedIndex = this.cb.SelectedIndex;
			if (columnType == null)
			{
				this.cb.SelectedIndex = 0;
			}
			else
			{
				this.editingType = (int)ColumnTypeDefUtil.enumOf(columnType.GetType());
				this.editings[this.editingType] = (ColumnTypeDef)((ICloneable)columnType).Clone();
				this.cb.SelectedIndex = this.editingType;
			}
			if (selectedIndex == this.cb.SelectedIndex)
			{
				this.prepareEditor();
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00019CBD File Offset: 0x00018CBD
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.ClientSize = new Size(484, 264);
			base.Name = "ColumnTypeUIEditorForm";
			base.ResumeLayout(false);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00019CF4 File Offset: 0x00018CF4
		private void SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.currentEditorPanel != null)
			{
				this.currentEditorPanel.save();
				ColumnTypeEditUtil.Release(this.currentEditorPanel);
			}
			this.prepareEditor();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00019D30 File Offset: 0x00018D30
		private void prepareEditor()
		{
			this.editingType = this.cb.SelectedIndex;
			if (this.editings[this.editingType] == null)
			{
				this.editings[this.editingType] = (Activator.CreateInstance(ColumnTypeDefUtil.Types[this.editingType]) as ColumnTypeDef);
			}
			this.currentEditorPanel = ColumnTypeEditUtil.getEditor(this.editingType, this.editings[this.editingType]);
			this.currentEditorPanel.Width = Math.Max(500, this.currentEditorPanel.Width);
			base.Size = new Size(this.currentEditorPanel.Width + 20, this.topPanel.Height + this.btmPanel.Height + this.currentEditorPanel.Height + 50);
			this.currentEditorPanel.Location = new Point(0, this.topPanel.Height);
			this.currentEditorPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			base.Controls.Add(this.currentEditorPanel);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00019E47 File Offset: 0x00018E47
		private void spawnClone()
		{
			this.editings[this.editingType] = (ColumnTypeDef)((ICloneable)this.__Target.ColumnType).Clone();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00019E74 File Offset: 0x00018E74
		private bool commitToChange()
		{
			string text = this.tb.Text;
			string text2 = this.tb.Text;
			string columnName = this.__Target.ColumnName;
			bool result;
			if (string.IsNullOrEmpty(text))
			{
				MessageBox.Show("The name of the column cannot be empty");
				result = false;
			}
			else if (!text2.Equals(columnName) && this.__existedNames.ContainsKey(text))
			{
				MessageBox.Show("The name: \"" + text + "\" already exists, please enter another one");
				result = false;
			}
			else
			{
				this.currentEditorPanel.save();
				this.__Target.ColumnType = this.editings[this.editingType];
				this.__Target.ColumnName = text2;
				if (columnName != null)
				{
					this.__existedNames.Remove(columnName);
				}
				this.__existedNames.Add(text2, null);
				result = true;
			}
			return result;
		}

		// Token: 0x0400031D RID: 797
		private ColumnDefinition __Target;

		// Token: 0x0400031E RID: 798
		private Control topPanel;

		// Token: 0x0400031F RID: 799
		private Control btmPanel;

		// Token: 0x04000320 RID: 800
		private ColumnTypeEditorPanel currentEditorPanel;

		// Token: 0x04000321 RID: 801
		private ComboBox cb;

		// Token: 0x04000322 RID: 802
		private TextBox tb;

		// Token: 0x04000323 RID: 803
		private int editingType;

		// Token: 0x04000324 RID: 804
		private ColumnTypeDef[] editings = new ColumnTypeDef[ColumnTypeDefUtil.Types.Length];

		// Token: 0x04000325 RID: 805
		private Dictionary<string, string> __existedNames;
	}
}
