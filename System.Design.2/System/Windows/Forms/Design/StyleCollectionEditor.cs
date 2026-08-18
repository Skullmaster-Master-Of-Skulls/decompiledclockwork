using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200033B RID: 827
	internal class StyleCollectionEditor : CollectionEditor
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x000C59D7 File Offset: 0x000C3BD7
		public StyleCollectionEditor(Type type) : base(type)
		{
			this.isRowCollection = type.IsAssignableFrom(typeof(TableLayoutRowStyleCollection));
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000C59F6 File Offset: 0x000C3BF6
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			return new StyleCollectionEditor.StyleEditorForm(this, this.isRowCollection);
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x000C5A04 File Offset: 0x000C3C04
		protected override string HelpTopic
		{
			get
			{
				return this.helptopic;
			}
		}

		// Token: 0x040018F4 RID: 6388
		private bool isRowCollection;

		// Token: 0x040018F5 RID: 6389
		protected string helptopic;

		// Token: 0x02000590 RID: 1424
		protected class NavigationalTableLayoutPanel : TableLayoutPanel
		{
			// Token: 0x170009F7 RID: 2551
			// (get) Token: 0x060032D8 RID: 13016 RVA: 0x00113CB4 File Offset: 0x00111EB4
			private List<RadioButton> RadioButtons
			{
				get
				{
					List<RadioButton> list = new List<RadioButton>();
					foreach (object obj in base.Controls)
					{
						Control control = (Control)obj;
						RadioButton radioButton = control as RadioButton;
						if (radioButton != null)
						{
							list.Add(radioButton);
						}
					}
					return list;
				}
			}

			// Token: 0x060032D9 RID: 13017 RVA: 0x00113D24 File Offset: 0x00111F24
			protected override bool ProcessDialogKey(Keys keyData)
			{
				bool flag = keyData == Keys.Down;
				bool flag2 = keyData == Keys.Up;
				if (flag || flag2)
				{
					List<RadioButton> radioButtons = this.RadioButtons;
					for (int i = 0; i < radioButtons.Count; i++)
					{
						RadioButton radioButton = radioButtons[i];
						if (radioButton.Focused)
						{
							int index;
							if (flag)
							{
								index = ((i == this.RadioButtons.Count - 1) ? 0 : (i + 1));
							}
							else
							{
								index = ((i == 0) ? (this.RadioButtons.Count - 1) : (i - 1));
							}
							radioButtons[index].Focus();
							return true;
						}
					}
				}
				return base.ProcessDialogKey(keyData);
			}
		}

		// Token: 0x02000591 RID: 1425
		protected class StyleEditorForm : CollectionEditor.CollectionForm
		{
			// Token: 0x060032DB RID: 13019 RVA: 0x00113DC0 File Offset: 0x00111FC0
			internal StyleEditorForm(CollectionEditor editor, bool isRowCollection) : base(editor)
			{
				this.editor = (StyleCollectionEditor)editor;
				this.isRowCollection = isRowCollection;
				this.InitializeComponent();
				this.HookEvents();
				DesignerUtils.ApplyListViewThemeStyles(this.columnsAndRowsListView);
				base.ActiveControl = this.columnsAndRowsListView;
				this.tlp = (base.Context.Instance as TableLayoutPanel);
				this.tlp.SuspendLayout();
				this.deleteList = new ArrayList();
				IDesignerHost designerHost = this.tlp.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					this.tlpDesigner = (designerHost.GetDesigner(this.tlp) as TableLayoutPanelDesigner);
					this.compSvc = (designerHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService);
				}
				this.rowStyleProp = TypeDescriptor.GetProperties(this.tlp)["RowStyles"];
				this.colStyleProp = TypeDescriptor.GetProperties(this.tlp)["ColumnStyles"];
				this.tlpDesigner.SuspendEnsureAvailableStyles();
			}

			// Token: 0x060032DC RID: 13020 RVA: 0x00113EE4 File Offset: 0x001120E4
			private void HookEvents()
			{
				base.HelpButtonClicked += this.OnHelpButtonClicked;
				this.columnsAndRowsListView.SelectedIndexChanged += this.OnListViewSelectedIndexChanged;
				this.columnsOrRowsComboBox.SelectionChangeCommitted += this.OnComboBoxSelectionChangeCommitted;
				this.okButton.Click += this.OnOkButtonClick;
				this.cancelButton.Click += this.OnCancelButtonClick;
				this.addButton.Click += this.OnAddButtonClick;
				this.removeButton.Click += this.OnRemoveButtonClick;
				this.insertButton.Click += this.OnInsertButtonClick;
				this.absoluteRadioButton.Enter += this.OnAbsoluteEnter;
				this.absoluteNumericUpDown.ValueChanged += this.OnValueChanged;
				this.percentRadioButton.Enter += this.OnPercentEnter;
				this.percentNumericUpDown.ValueChanged += this.OnValueChanged;
				this.autoSizedRadioButton.Enter += this.OnAutoSizeEnter;
				base.Shown += this.OnShown;
				this.helperLinkLabel1.LinkClicked += this.OnLink1Click;
				this.helperLinkLabel2.LinkClicked += this.OnLink2Click;
			}

			// Token: 0x060032DD RID: 13021 RVA: 0x00114058 File Offset: 0x00112258
			private void InitializeComponent()
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(StyleCollectionEditor));
				this.addRemoveInsertTableLayoutPanel = new TableLayoutPanel();
				this.addButton = new Button();
				this.removeButton = new Button();
				this.insertButton = new Button();
				this.okCancelTableLayoutPanel = new TableLayoutPanel();
				this.okButton = new Button();
				this.cancelButton = new Button();
				this.overarchingTableLayoutPanel = new TableLayoutPanel();
				this.showTableLayoutPanel = new TableLayoutPanel();
				this.memberTypeLabel = new Label();
				this.columnsOrRowsComboBox = new ComboBox();
				this.columnsAndRowsListView = new ListView();
				this.membersColumnHeader = new ColumnHeader(componentResourceManager.GetString("columnsAndRowsListView.Columns"));
				this.sizeTypeColumnHeader = new ColumnHeader(componentResourceManager.GetString("columnsAndRowsListView.Columns1"));
				this.valueColumnHeader = new ColumnHeader(componentResourceManager.GetString("columnsAndRowsListView.Columns2"));
				this.helperTextTableLayoutPanel = new TableLayoutPanel();
				this.infoPictureBox2 = new PictureBox();
				this.infoPictureBox1 = new PictureBox();
				this.helperLinkLabel1 = new LinkLabel();
				this.helperLinkLabel2 = new LinkLabel();
				this.sizeTypeGroupBox = new GroupBox();
				this.sizeTypeTableLayoutPanel = new StyleCollectionEditor.NavigationalTableLayoutPanel();
				this.absoluteNumericUpDown = new NumericUpDown();
				this.absoluteRadioButton = new RadioButton();
				this.pixelsLabel = new Label();
				this.percentLabel = new Label();
				this.percentRadioButton = new RadioButton();
				this.autoSizedRadioButton = new RadioButton();
				this.percentNumericUpDown = new NumericUpDown();
				this.addRemoveInsertTableLayoutPanel.SuspendLayout();
				this.okCancelTableLayoutPanel.SuspendLayout();
				this.overarchingTableLayoutPanel.SuspendLayout();
				this.showTableLayoutPanel.SuspendLayout();
				this.helperTextTableLayoutPanel.SuspendLayout();
				((ISupportInitialize)this.infoPictureBox2).BeginInit();
				((ISupportInitialize)this.infoPictureBox1).BeginInit();
				this.sizeTypeGroupBox.SuspendLayout();
				this.sizeTypeTableLayoutPanel.SuspendLayout();
				((ISupportInitialize)this.absoluteNumericUpDown).BeginInit();
				((ISupportInitialize)this.percentNumericUpDown).BeginInit();
				base.SuspendLayout();
				componentResourceManager.ApplyResources(this.addRemoveInsertTableLayoutPanel, "addRemoveInsertTableLayoutPanel");
				this.addRemoveInsertTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
				this.addRemoveInsertTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
				this.addRemoveInsertTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
				this.addRemoveInsertTableLayoutPanel.Controls.Add(this.addButton, 0, 0);
				this.addRemoveInsertTableLayoutPanel.Controls.Add(this.removeButton, 1, 0);
				this.addRemoveInsertTableLayoutPanel.Controls.Add(this.insertButton, 2, 0);
				this.addRemoveInsertTableLayoutPanel.Margin = new Padding(0, 3, 3, 3);
				this.addRemoveInsertTableLayoutPanel.Name = "addRemoveInsertTableLayoutPanel";
				this.addRemoveInsertTableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.addButton, "addButton");
				this.addButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				this.addButton.Margin = new Padding(0, 0, 4, 0);
				this.addButton.MinimumSize = new Size(75, 23);
				this.addButton.Name = "addButton";
				this.addButton.Padding = new Padding(10, 0, 10, 0);
				componentResourceManager.ApplyResources(this.removeButton, "removeButton");
				this.removeButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				this.removeButton.Margin = new Padding(2, 0, 2, 0);
				this.removeButton.MinimumSize = new Size(75, 23);
				this.removeButton.Name = "removeButton";
				this.removeButton.Padding = new Padding(10, 0, 10, 0);
				componentResourceManager.ApplyResources(this.insertButton, "insertButton");
				this.insertButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				this.insertButton.Margin = new Padding(4, 0, 0, 0);
				this.insertButton.MinimumSize = new Size(75, 23);
				this.insertButton.Name = "insertButton";
				this.insertButton.Padding = new Padding(10, 0, 10, 0);
				componentResourceManager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
				this.overarchingTableLayoutPanel.SetColumnSpan(this.okCancelTableLayoutPanel, 2);
				this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.okCancelTableLayoutPanel.Controls.Add(this.okButton, 0, 0);
				this.okCancelTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
				this.okCancelTableLayoutPanel.Margin = new Padding(0, 6, 0, 0);
				this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
				this.okCancelTableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.okButton, "okButton");
				this.okButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				this.okButton.Margin = new Padding(0, 0, 3, 0);
				this.okButton.MinimumSize = new Size(75, 23);
				this.okButton.Name = "okButton";
				this.okButton.Padding = new Padding(10, 0, 10, 0);
				componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
				this.cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				this.cancelButton.DialogResult = DialogResult.Cancel;
				this.cancelButton.Margin = new Padding(3, 0, 0, 0);
				this.cancelButton.MinimumSize = new Size(75, 23);
				this.cancelButton.Name = "cancelButton";
				this.cancelButton.Padding = new Padding(10, 0, 10, 0);
				componentResourceManager.ApplyResources(this.overarchingTableLayoutPanel, "overarchingTableLayoutPanel");
				this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
				this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				this.overarchingTableLayoutPanel.Controls.Add(this.sizeTypeGroupBox, 1, 0);
				this.overarchingTableLayoutPanel.Controls.Add(this.okCancelTableLayoutPanel, 0, 4);
				this.overarchingTableLayoutPanel.Controls.Add(this.showTableLayoutPanel, 0, 0);
				this.overarchingTableLayoutPanel.Controls.Add(this.addRemoveInsertTableLayoutPanel, 0, 3);
				this.overarchingTableLayoutPanel.Controls.Add(this.columnsAndRowsListView, 0, 1);
				this.overarchingTableLayoutPanel.Controls.Add(this.helperTextTableLayoutPanel, 1, 2);
				this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
				this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
				this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
				this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
				this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
				this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.showTableLayoutPanel, "showTableLayoutPanel");
				this.showTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
				this.showTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
				this.showTableLayoutPanel.Controls.Add(this.memberTypeLabel, 0, 0);
				this.showTableLayoutPanel.Controls.Add(this.columnsOrRowsComboBox, 1, 0);
				this.showTableLayoutPanel.Margin = new Padding(0, 0, 3, 3);
				this.showTableLayoutPanel.Name = "showTableLayoutPanel";
				this.showTableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.memberTypeLabel, "memberTypeLabel");
				this.memberTypeLabel.Margin = new Padding(0, 0, 3, 0);
				this.memberTypeLabel.Name = "memberTypeLabel";
				componentResourceManager.ApplyResources(this.columnsOrRowsComboBox, "columnsOrRowsComboBox");
				this.columnsOrRowsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				this.columnsOrRowsComboBox.FormattingEnabled = true;
				this.columnsOrRowsComboBox.Items.AddRange(new object[]
				{
					componentResourceManager.GetString("columnsOrRowsComboBox.Items"),
					componentResourceManager.GetString("columnsOrRowsComboBox.Items1")
				});
				this.columnsOrRowsComboBox.Margin = new Padding(3, 0, 0, 0);
				this.columnsOrRowsComboBox.Name = "columnsOrRowsComboBox";
				componentResourceManager.ApplyResources(this.columnsAndRowsListView, "columnsAndRowsListView");
				this.columnsAndRowsListView.Columns.AddRange(new ColumnHeader[]
				{
					this.membersColumnHeader,
					this.sizeTypeColumnHeader,
					this.valueColumnHeader
				});
				this.columnsAndRowsListView.FullRowSelect = true;
				this.columnsAndRowsListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
				this.columnsAndRowsListView.HideSelection = false;
				this.columnsAndRowsListView.Margin = new Padding(0, 3, 3, 3);
				this.columnsAndRowsListView.Name = "columnsAndRowsListView";
				this.overarchingTableLayoutPanel.SetRowSpan(this.columnsAndRowsListView, 2);
				this.columnsAndRowsListView.View = View.Details;
				componentResourceManager.ApplyResources(this.membersColumnHeader, "membersColumnHeader");
				componentResourceManager.ApplyResources(this.sizeTypeColumnHeader, "sizeTypeColumnHeader");
				componentResourceManager.ApplyResources(this.valueColumnHeader, "valueColumnHeader");
				componentResourceManager.ApplyResources(this.helperTextTableLayoutPanel, "helperTextTableLayoutPanel");
				this.helperTextTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
				this.helperTextTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
				this.helperTextTableLayoutPanel.Controls.Add(this.infoPictureBox2, 0, 1);
				this.helperTextTableLayoutPanel.Controls.Add(this.infoPictureBox1, 0, 0);
				this.helperTextTableLayoutPanel.Controls.Add(this.helperLinkLabel1, 1, 0);
				this.helperTextTableLayoutPanel.Controls.Add(this.helperLinkLabel2, 1, 1);
				this.helperTextTableLayoutPanel.Margin = new Padding(6, 6, 0, 3);
				this.helperTextTableLayoutPanel.Name = "helperTextTableLayoutPanel";
				this.helperTextTableLayoutPanel.RowStyles.Add(new RowStyle());
				this.helperTextTableLayoutPanel.RowStyles.Add(new RowStyle());
				componentResourceManager.ApplyResources(this.infoPictureBox2, "infoPictureBox2");
				this.infoPictureBox2.Name = "infoPictureBox2";
				this.infoPictureBox2.TabStop = false;
				componentResourceManager.ApplyResources(this.infoPictureBox1, "infoPictureBox1");
				this.infoPictureBox1.Name = "infoPictureBox1";
				this.infoPictureBox1.TabStop = false;
				if (DpiHelper.IsScalingRequired)
				{
					Bitmap image = this.infoPictureBox1.Image as Bitmap;
					DpiHelper.ScaleBitmapLogicalToDevice(ref image, 0);
					this.infoPictureBox1.Image = image;
					image = (this.infoPictureBox2.Image as Bitmap);
					DpiHelper.ScaleBitmapLogicalToDevice(ref image, 0);
					this.infoPictureBox2.Image = image;
					this.scaledUpDownLeftMargin = DpiHelper.LogicalToDeviceUnitsX(10);
					this.scaledUpDownTopMargin = DpiHelper.LogicalToDeviceUnitsY(4);
					this.scaledLabelRightMargin = DpiHelper.LogicalToDeviceUnitsX(5);
				}
				componentResourceManager.ApplyResources(this.helperLinkLabel1, "helperLinkLabel1");
				this.helperLinkLabel1.Margin = new Padding(3, 0, 0, 3);
				this.helperLinkLabel1.Name = "helperLinkLabel1";
				this.helperLinkLabel1.TabStop = true;
				this.helperLinkLabel1.UseCompatibleTextRendering = true;
				componentResourceManager.ApplyResources(this.helperLinkLabel2, "helperLinkLabel2");
				this.helperLinkLabel2.Margin = new Padding(3, 3, 0, 0);
				this.helperLinkLabel2.Name = "helperLinkLabel2";
				this.helperLinkLabel2.TabStop = true;
				this.helperLinkLabel2.UseCompatibleTextRendering = true;
				componentResourceManager.ApplyResources(this.sizeTypeGroupBox, "sizeTypeGroupBox");
				this.sizeTypeGroupBox.Controls.Add(this.sizeTypeTableLayoutPanel);
				this.sizeTypeGroupBox.Margin = new Padding(6, 0, 0, 3);
				this.sizeTypeGroupBox.Name = "sizeTypeGroupBox";
				this.sizeTypeGroupBox.Padding = new Padding(0);
				this.overarchingTableLayoutPanel.SetRowSpan(this.sizeTypeGroupBox, 2);
				this.sizeTypeGroupBox.TabStop = false;
				componentResourceManager.ApplyResources(this.sizeTypeTableLayoutPanel, "sizeTypeTableLayoutPanel");
				this.sizeTypeTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));
				this.sizeTypeTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));
				this.sizeTypeTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));
				this.sizeTypeTableLayoutPanel.Controls.Add(this.absoluteNumericUpDown, 1, 0);
				this.sizeTypeTableLayoutPanel.Controls.Add(this.absoluteRadioButton, 0, 0);
				this.sizeTypeTableLayoutPanel.Controls.Add(this.pixelsLabel, 2, 0);
				this.sizeTypeTableLayoutPanel.Controls.Add(this.percentLabel, 2, 1);
				this.sizeTypeTableLayoutPanel.Controls.Add(this.percentRadioButton, 0, 1);
				this.sizeTypeTableLayoutPanel.Controls.Add(this.autoSizedRadioButton, 0, 2);
				this.sizeTypeTableLayoutPanel.Controls.Add(this.percentNumericUpDown, 1, 1);
				this.sizeTypeTableLayoutPanel.Margin = new Padding(9);
				this.sizeTypeTableLayoutPanel.Name = "sizeTypeTableLayoutPanel";
				this.sizeTypeTableLayoutPanel.AutoSize = true;
				this.sizeTypeTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				componentResourceManager.ApplyResources(this.absoluteNumericUpDown, "absoluteNumericUpDown");
				NumericUpDown numericUpDown = this.absoluteNumericUpDown;
				int[] array = new int[4];
				array[0] = 99999;
				numericUpDown.Maximum = new decimal(array);
				this.absoluteNumericUpDown.Name = "absoluteNumericUpDown";
				this.absoluteNumericUpDown.Margin = new Padding(this.scaledUpDownLeftMargin, this.scaledUpDownTopMargin, 0, 0);
				this.absoluteNumericUpDown.AutoScaleMode = AutoScaleMode.Font;
				componentResourceManager.ApplyResources(this.absoluteRadioButton, "absoluteRadioButton");
				this.absoluteRadioButton.Margin = new Padding(0, 3, 3, 0);
				this.absoluteRadioButton.Name = "absoluteRadioButton";
				componentResourceManager.ApplyResources(this.pixelsLabel, "pixelsLabel");
				this.pixelsLabel.Name = "pixelsLabel";
				this.pixelsLabel.Margin = new Padding(0, 0, this.scaledLabelRightMargin, 0);
				componentResourceManager.ApplyResources(this.percentLabel, "percentLabel");
				this.percentLabel.Name = "percentLabel";
				this.percentLabel.Margin = new Padding(0, 0, this.scaledLabelRightMargin, 0);
				componentResourceManager.ApplyResources(this.percentRadioButton, "percentRadioButton");
				this.percentRadioButton.Margin = new Padding(0, 3, 3, 0);
				this.percentRadioButton.Name = "percentRadioButton";
				componentResourceManager.ApplyResources(this.autoSizedRadioButton, "autoSizedRadioButton");
				this.autoSizedRadioButton.Margin = new Padding(0, 3, 3, 0);
				this.autoSizedRadioButton.Name = "autoSizedRadioButton";
				componentResourceManager.ApplyResources(this.percentNumericUpDown, "percentNumericUpDown");
				this.percentNumericUpDown.DecimalPlaces = 2;
				NumericUpDown numericUpDown2 = this.percentNumericUpDown;
				int[] array2 = new int[4];
				array2[0] = 9999;
				numericUpDown2.Maximum = new decimal(array2);
				this.percentNumericUpDown.Name = "percentNumericUpDown";
				this.percentNumericUpDown.Margin = new Padding(this.scaledUpDownLeftMargin, this.scaledUpDownTopMargin, 0, 0);
				this.percentNumericUpDown.AutoScaleMode = AutoScaleMode.Font;
				base.AcceptButton = this.okButton;
				componentResourceManager.ApplyResources(this, "$this");
				base.AutoScaleMode = AutoScaleMode.Font;
				base.CancelButton = this.cancelButton;
				base.Controls.Add(this.overarchingTableLayoutPanel);
				base.HelpButton = true;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.Name = "Form1";
				base.ShowIcon = false;
				base.ShowInTaskbar = false;
				this.addRemoveInsertTableLayoutPanel.ResumeLayout(false);
				this.addRemoveInsertTableLayoutPanel.PerformLayout();
				this.okCancelTableLayoutPanel.ResumeLayout(false);
				this.okCancelTableLayoutPanel.PerformLayout();
				this.overarchingTableLayoutPanel.ResumeLayout(false);
				this.overarchingTableLayoutPanel.PerformLayout();
				this.showTableLayoutPanel.ResumeLayout(false);
				this.showTableLayoutPanel.PerformLayout();
				this.helperTextTableLayoutPanel.ResumeLayout(false);
				this.helperTextTableLayoutPanel.PerformLayout();
				((ISupportInitialize)this.infoPictureBox2).EndInit();
				((ISupportInitialize)this.infoPictureBox1).EndInit();
				this.sizeTypeGroupBox.ResumeLayout(false);
				this.sizeTypeTableLayoutPanel.ResumeLayout(false);
				this.sizeTypeTableLayoutPanel.PerformLayout();
				((ISupportInitialize)this.absoluteNumericUpDown).EndInit();
				((ISupportInitialize)this.percentNumericUpDown).EndInit();
				base.ResumeLayout(false);
			}

			// Token: 0x060032DE RID: 13022 RVA: 0x001150F9 File Offset: 0x001132F9
			private void OnShown(object sender, EventArgs e)
			{
				this.isDialogDirty = false;
				this.columnsAndRowsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}

			// Token: 0x060032DF RID: 13023 RVA: 0x00115110 File Offset: 0x00113310
			private void OnLink1Click(object sender, LinkLabelLinkClickedEventArgs e)
			{
				CancelEventArgs e2 = new CancelEventArgs();
				this.editor.helptopic = "net.ComponentModel.StyleCollectionEditor.TLP.SpanRowsColumns";
				this.OnHelpButtonClicked(sender, e2);
			}

			// Token: 0x060032E0 RID: 13024 RVA: 0x0011513C File Offset: 0x0011333C
			private void OnLink2Click(object sender, LinkLabelLinkClickedEventArgs e)
			{
				CancelEventArgs e2 = new CancelEventArgs();
				this.editor.helptopic = "net.ComponentModel.StyleCollectionEditor.TLP.AnchorDock";
				this.OnHelpButtonClicked(sender, e2);
			}

			// Token: 0x060032E1 RID: 13025 RVA: 0x00115167 File Offset: 0x00113367
			private void OnHelpButtonClicked(object sender, CancelEventArgs e)
			{
				e.Cancel = true;
				this.editor.helptopic = "net.ComponentModel.StyleCollectionEditor";
				this.editor.ShowHelp();
			}

			// Token: 0x060032E2 RID: 13026 RVA: 0x00003937 File Offset: 0x00001B37
			protected override void OnEditValueChanged()
			{
			}

			// Token: 0x060032E3 RID: 13027 RVA: 0x0011518C File Offset: 0x0011338C
			protected internal override DialogResult ShowEditorDialog(IWindowsFormsEditorService edSvc)
			{
				if (this.compSvc != null)
				{
					if (this.rowStyleProp != null)
					{
						this.compSvc.OnComponentChanging(this.tlp, this.rowStyleProp);
					}
					if (this.colStyleProp != null)
					{
						this.compSvc.OnComponentChanging(this.tlp, this.colStyleProp);
					}
				}
				int[] columnWidths = this.tlp.GetColumnWidths();
				int[] rowHeights = this.tlp.GetRowHeights();
				if (this.tlp.ColumnStyles.Count > columnWidths.Length)
				{
					int num = this.tlp.ColumnStyles.Count - columnWidths.Length;
					for (int i = 0; i < num; i++)
					{
						this.tlp.ColumnStyles.RemoveAt(this.tlp.ColumnStyles.Count - 1);
					}
				}
				if (this.tlp.RowStyles.Count > rowHeights.Length)
				{
					int num2 = this.tlp.RowStyles.Count - rowHeights.Length;
					for (int j = 0; j < num2; j++)
					{
						this.tlp.RowStyles.RemoveAt(this.tlp.RowStyles.Count - 1);
					}
				}
				this.columnsOrRowsComboBox.SelectedIndex = (this.isRowCollection ? 1 : 0);
				this.InitListView();
				return base.ShowEditorDialog(edSvc);
			}

			// Token: 0x060032E4 RID: 13028 RVA: 0x001152D4 File Offset: 0x001134D4
			private string FormatValueString(SizeType type, float value)
			{
				if (type == SizeType.Absolute)
				{
					return value.ToString(CultureInfo.CurrentCulture);
				}
				if (type == SizeType.Percent)
				{
					return (value / 100f).ToString("P", CultureInfo.CurrentCulture);
				}
				return string.Empty;
			}

			// Token: 0x060032E5 RID: 13029 RVA: 0x00115318 File Offset: 0x00113518
			private void InitListView()
			{
				this.columnsAndRowsListView.Items.Clear();
				string str = this.isRowCollection ? "Row" : "Column";
				int num = this.isRowCollection ? this.tlp.RowStyles.Count : this.tlp.ColumnStyles.Count;
				for (int i = 0; i < num; i++)
				{
					string text;
					string text2;
					if (this.isRowCollection)
					{
						RowStyle rowStyle = this.tlp.RowStyles[i];
						text = rowStyle.SizeType.ToString();
						text2 = this.FormatValueString(rowStyle.SizeType, rowStyle.Height);
					}
					else
					{
						ColumnStyle columnStyle = this.tlp.ColumnStyles[i];
						text = columnStyle.SizeType.ToString();
						text2 = this.FormatValueString(columnStyle.SizeType, columnStyle.Width);
					}
					this.columnsAndRowsListView.Items.Add(new ListViewItem(new string[]
					{
						str + (i + 1).ToString(CultureInfo.InvariantCulture),
						text,
						text2
					}));
				}
				if (num > 0)
				{
					this.ClearAndSetSelectionAndFocus(0);
				}
				this.removeButton.Enabled = (this.columnsAndRowsListView.Items.Count > 1);
			}

			// Token: 0x060032E6 RID: 13030 RVA: 0x0011547C File Offset: 0x0011367C
			private void UpdateListViewItem(int index, string member, string type, string value)
			{
				this.columnsAndRowsListView.Items[index].SubItems[StyleCollectionEditor.StyleEditorForm.MEMBER_INDEX].Text = member;
				this.columnsAndRowsListView.Items[index].SubItems[StyleCollectionEditor.StyleEditorForm.TYPE_INDEX].Text = type;
				this.columnsAndRowsListView.Items[index].SubItems[StyleCollectionEditor.StyleEditorForm.VALUE_INDEX].Text = value;
			}

			// Token: 0x060032E7 RID: 13031 RVA: 0x001154FC File Offset: 0x001136FC
			private void UpdateListViewMember()
			{
				for (int i = 0; i < this.columnsAndRowsListView.Items.Count; i++)
				{
					this.columnsAndRowsListView.Items[i].SubItems[StyleCollectionEditor.StyleEditorForm.MEMBER_INDEX].Text = (this.isRowCollection ? "Row" : "Column") + (i + 1).ToString(CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x060032E8 RID: 13032 RVA: 0x00115572 File Offset: 0x00113772
			private void OnComboBoxSelectionChangeCommitted(object sender, EventArgs e)
			{
				this.isRowCollection = (this.columnsOrRowsComboBox.SelectedIndex != 0);
				this.InitListView();
			}

			// Token: 0x060032E9 RID: 13033 RVA: 0x00115590 File Offset: 0x00113790
			private void OnListViewSelectedIndexChanged(object sender, EventArgs e)
			{
				ListView.SelectedListViewItemCollection selectedItems = this.columnsAndRowsListView.SelectedItems;
				if (selectedItems.Count == 0)
				{
					if (!this.haveInvoked)
					{
						base.BeginInvoke(new EventHandler(this.OnListSelectionComplete));
						this.haveInvoked = true;
					}
					return;
				}
				this.sizeTypeGroupBox.Enabled = true;
				this.insertButton.Enabled = true;
				if (selectedItems.Count == this.columnsAndRowsListView.Items.Count)
				{
					this.removeButton.Enabled = false;
				}
				else
				{
					this.removeButton.Enabled = (this.columnsAndRowsListView.Items.Count > 1);
				}
				if (selectedItems.Count == 1)
				{
					int index = this.columnsAndRowsListView.Items.IndexOf(selectedItems[0]);
					if (this.isRowCollection)
					{
						this.UpdateGroupBox(this.tlp.RowStyles[index].SizeType, this.tlp.RowStyles[index].Height);
						return;
					}
					this.UpdateGroupBox(this.tlp.ColumnStyles[index].SizeType, this.tlp.ColumnStyles[index].Width);
					return;
				}
				else
				{
					bool flag = true;
					int index2 = this.columnsAndRowsListView.Items.IndexOf(selectedItems[0]);
					SizeType sizeType = this.isRowCollection ? this.tlp.RowStyles[index2].SizeType : this.tlp.ColumnStyles[index2].SizeType;
					float num = this.isRowCollection ? this.tlp.RowStyles[index2].Height : this.tlp.ColumnStyles[index2].Width;
					for (int i = 1; i < selectedItems.Count; i++)
					{
						index2 = this.columnsAndRowsListView.Items.IndexOf(selectedItems[i]);
						if (sizeType != (this.isRowCollection ? this.tlp.RowStyles[index2].SizeType : this.tlp.ColumnStyles[index2].SizeType))
						{
							flag = false;
							break;
						}
						if (num != (this.isRowCollection ? this.tlp.RowStyles[index2].Height : this.tlp.ColumnStyles[index2].Width))
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						this.ResetAllRadioButtons();
						return;
					}
					this.UpdateGroupBox(sizeType, num);
					return;
				}
			}

			// Token: 0x060032EA RID: 13034 RVA: 0x00115820 File Offset: 0x00113A20
			private void OnListSelectionComplete(object sender, EventArgs e)
			{
				this.haveInvoked = false;
				if (this.columnsAndRowsListView.SelectedItems.Count == 0)
				{
					this.ResetAllRadioButtons();
					this.sizeTypeGroupBox.Enabled = false;
					this.insertButton.Enabled = false;
					this.removeButton.Enabled = false;
				}
			}

			// Token: 0x060032EB RID: 13035 RVA: 0x00115870 File Offset: 0x00113A70
			private void ResetAllRadioButtons()
			{
				this.absoluteRadioButton.Checked = false;
				this.ResetAbsolute();
				this.percentRadioButton.Checked = false;
				this.ResetPercent();
				this.autoSizedRadioButton.Checked = false;
			}

			// Token: 0x060032EC RID: 13036 RVA: 0x001158A4 File Offset: 0x00113AA4
			private void ResetAbsolute()
			{
				this.absoluteNumericUpDown.ValueChanged -= this.OnValueChanged;
				this.absoluteNumericUpDown.Enabled = false;
				this.absoluteNumericUpDown.Value = DesignerUtils.MINIMUMSTYLESIZE;
				this.absoluteNumericUpDown.ValueChanged += this.OnValueChanged;
			}

			// Token: 0x060032ED RID: 13037 RVA: 0x00115900 File Offset: 0x00113B00
			private void ResetPercent()
			{
				this.percentNumericUpDown.ValueChanged -= this.OnValueChanged;
				this.percentNumericUpDown.Enabled = false;
				this.percentNumericUpDown.Value = DesignerUtils.MINIMUMSTYLEPERCENT;
				this.percentNumericUpDown.ValueChanged += this.OnValueChanged;
			}

			// Token: 0x060032EE RID: 13038 RVA: 0x0011595C File Offset: 0x00113B5C
			private void UpdateGroupBox(SizeType type, float value)
			{
				switch (type)
				{
				case SizeType.AutoSize:
					this.autoSizedRadioButton.Checked = true;
					this.ResetAbsolute();
					this.ResetPercent();
					return;
				case SizeType.Absolute:
					this.absoluteRadioButton.Checked = true;
					this.absoluteNumericUpDown.Enabled = true;
					try
					{
						this.absoluteNumericUpDown.Value = (decimal)value;
					}
					catch (ArgumentOutOfRangeException)
					{
						this.absoluteNumericUpDown.Value = DesignerUtils.MINIMUMSTYLESIZE;
					}
					this.ResetPercent();
					return;
				case SizeType.Percent:
					this.percentRadioButton.Checked = true;
					this.percentNumericUpDown.Enabled = true;
					try
					{
						this.percentNumericUpDown.Value = (decimal)value;
					}
					catch (ArgumentOutOfRangeException)
					{
						this.percentNumericUpDown.Value = DesignerUtils.MINIMUMSTYLEPERCENT;
					}
					this.ResetAbsolute();
					return;
				default:
					return;
				}
			}

			// Token: 0x060032EF RID: 13039 RVA: 0x00115A44 File Offset: 0x00113C44
			private void ClearAndSetSelectionAndFocus(int index)
			{
				this.columnsAndRowsListView.BeginUpdate();
				this.columnsAndRowsListView.Focus();
				if (this.columnsAndRowsListView.FocusedItem != null)
				{
					this.columnsAndRowsListView.FocusedItem.Focused = false;
				}
				this.columnsAndRowsListView.SelectedItems.Clear();
				this.columnsAndRowsListView.Items[index].Selected = true;
				this.columnsAndRowsListView.Items[index].Focused = true;
				this.columnsAndRowsListView.Items[index].EnsureVisible();
				this.columnsAndRowsListView.EndUpdate();
			}

			// Token: 0x060032F0 RID: 13040 RVA: 0x00115AE8 File Offset: 0x00113CE8
			private void AddItem(int index)
			{
				this.tlpDesigner.InsertRowCol(this.isRowCollection, index);
				string text;
				if (this.isRowCollection)
				{
					text = "Row" + this.tlp.RowStyles.Count.ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					text = "Column" + this.tlp.RowStyles.Count.ToString(CultureInfo.InvariantCulture);
				}
				if (text != null)
				{
					this.columnsAndRowsListView.Items.Insert(index, new ListViewItem(new string[]
					{
						text,
						SizeType.Absolute.ToString(),
						DesignerUtils.MINIMUMSTYLESIZE.ToString(CultureInfo.InvariantCulture)
					}));
					this.UpdateListViewMember();
					this.ClearAndSetSelectionAndFocus(index);
				}
			}

			// Token: 0x060032F1 RID: 13041 RVA: 0x00115BB8 File Offset: 0x00113DB8
			private void OnAddButtonClick(object sender, EventArgs e)
			{
				this.isDialogDirty = true;
				this.AddItem(this.columnsAndRowsListView.Items.Count);
			}

			// Token: 0x060032F2 RID: 13042 RVA: 0x00115BD8 File Offset: 0x00113DD8
			private void OnInsertButtonClick(object sender, EventArgs e)
			{
				this.isDialogDirty = true;
				this.AddItem(this.columnsAndRowsListView.SelectedIndices[0]);
				this.tlpDesigner.FixUpControlsOnInsert(this.isRowCollection, this.columnsAndRowsListView.SelectedIndices[0]);
			}

			// Token: 0x060032F3 RID: 13043 RVA: 0x00115C28 File Offset: 0x00113E28
			private void OnRemoveButtonClick(object sender, EventArgs e)
			{
				if (this.columnsAndRowsListView.Items.Count == 1 || this.columnsAndRowsListView.Items.Count == this.columnsAndRowsListView.SelectedIndices.Count)
				{
					return;
				}
				this.isDialogDirty = true;
				int num = this.columnsAndRowsListView.SelectedIndices[0];
				for (int i = this.columnsAndRowsListView.SelectedIndices.Count - 1; i >= 0; i--)
				{
					int index = this.columnsAndRowsListView.SelectedIndices[i];
					this.tlpDesigner.FixUpControlsOnDelete(this.isRowCollection, index, this.deleteList);
					this.tlpDesigner.DeleteRowCol(this.isRowCollection, index);
					if (this.isRowCollection)
					{
						this.columnsAndRowsListView.Items.RemoveAt(index);
					}
					else
					{
						this.columnsAndRowsListView.Items.RemoveAt(index);
					}
				}
				if (num >= this.columnsAndRowsListView.Items.Count)
				{
					num--;
				}
				this.UpdateListViewMember();
				this.ClearAndSetSelectionAndFocus(num);
			}

			// Token: 0x060032F4 RID: 13044 RVA: 0x00115D30 File Offset: 0x00113F30
			private void UpdateTypeAndValue(SizeType type, float value)
			{
				for (int i = 0; i < this.columnsAndRowsListView.SelectedIndices.Count; i++)
				{
					int index = this.columnsAndRowsListView.SelectedIndices[i];
					if (this.isRowCollection)
					{
						this.tlp.RowStyles[index].SizeType = type;
						this.tlp.RowStyles[index].Height = value;
					}
					else
					{
						this.tlp.ColumnStyles[index].SizeType = type;
						this.tlp.ColumnStyles[index].Width = value;
					}
					this.UpdateListViewItem(index, this.columnsAndRowsListView.Items[index].SubItems[StyleCollectionEditor.StyleEditorForm.MEMBER_INDEX].Text, type.ToString(), this.FormatValueString(type, value));
				}
			}

			// Token: 0x060032F5 RID: 13045 RVA: 0x00115E17 File Offset: 0x00114017
			private void OnAbsoluteEnter(object sender, EventArgs e)
			{
				this.isDialogDirty = true;
				this.UpdateTypeAndValue(SizeType.Absolute, (float)this.absoluteNumericUpDown.Value);
				this.absoluteNumericUpDown.Enabled = true;
				this.ResetPercent();
			}

			// Token: 0x060032F6 RID: 13046 RVA: 0x00115E4A File Offset: 0x0011404A
			private void OnPercentEnter(object sender, EventArgs e)
			{
				this.isDialogDirty = true;
				this.UpdateTypeAndValue(SizeType.Percent, (float)this.percentNumericUpDown.Value);
				this.percentNumericUpDown.Enabled = true;
				this.ResetAbsolute();
			}

			// Token: 0x060032F7 RID: 13047 RVA: 0x00115E7D File Offset: 0x0011407D
			private void OnAutoSizeEnter(object sender, EventArgs e)
			{
				this.isDialogDirty = true;
				this.UpdateTypeAndValue(SizeType.AutoSize, 0f);
				this.ResetAbsolute();
				this.ResetPercent();
			}

			// Token: 0x060032F8 RID: 13048 RVA: 0x00115EA0 File Offset: 0x001140A0
			private void OnValueChanged(object sender, EventArgs e)
			{
				if (this.absoluteNumericUpDown == sender && this.absoluteRadioButton.Checked)
				{
					this.isDialogDirty = true;
					this.UpdateTypeAndValue(SizeType.Absolute, (float)this.absoluteNumericUpDown.Value);
					return;
				}
				if (this.percentNumericUpDown == sender && this.percentRadioButton.Checked)
				{
					this.isDialogDirty = true;
					this.UpdateTypeAndValue(SizeType.Percent, (float)this.percentNumericUpDown.Value);
				}
			}

			// Token: 0x060032F9 RID: 13049 RVA: 0x00115F18 File Offset: 0x00114118
			private void NormalizePercentStyle(bool normalizeRow)
			{
				int num = normalizeRow ? this.tlp.RowStyles.Count : this.tlp.ColumnStyles.Count;
				float num2 = 0f;
				for (int i = 0; i < num; i++)
				{
					if (normalizeRow)
					{
						if (this.tlp.RowStyles[i].SizeType == SizeType.Percent)
						{
							num2 += this.tlp.RowStyles[i].Height;
						}
					}
					else if (this.tlp.ColumnStyles[i].SizeType == SizeType.Percent)
					{
						num2 += this.tlp.ColumnStyles[i].Width;
					}
				}
				if (num2 == 100f || num2 == 0f)
				{
					return;
				}
				for (int j = 0; j < num; j++)
				{
					if (normalizeRow)
					{
						if (this.tlp.RowStyles[j].SizeType == SizeType.Percent)
						{
							this.tlp.RowStyles[j].Height = this.tlp.RowStyles[j].Height * 100f / num2;
						}
					}
					else if (this.tlp.ColumnStyles[j].SizeType == SizeType.Percent)
					{
						this.tlp.ColumnStyles[j].Width = this.tlp.ColumnStyles[j].Width * 100f / num2;
					}
				}
			}

			// Token: 0x060032FA RID: 13050 RVA: 0x0011608B File Offset: 0x0011428B
			private void NormalizePercentStyles()
			{
				this.NormalizePercentStyle(true);
				this.NormalizePercentStyle(false);
			}

			// Token: 0x060032FB RID: 13051 RVA: 0x0011609C File Offset: 0x0011429C
			private void OnOkButtonClick(object sender, EventArgs e)
			{
				if (this.isDialogDirty)
				{
					if (this.absoluteRadioButton.Checked)
					{
						this.UpdateTypeAndValue(SizeType.Absolute, (float)this.absoluteNumericUpDown.Value);
					}
					else if (this.percentRadioButton.Checked)
					{
						this.UpdateTypeAndValue(SizeType.Percent, (float)this.percentNumericUpDown.Value);
					}
					else if (this.autoSizedRadioButton.Checked)
					{
						this.UpdateTypeAndValue(SizeType.AutoSize, 0f);
					}
					this.NormalizePercentStyles();
					if (this.deleteList.Count > 0)
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.tlp)["Controls"];
						if (this.compSvc != null && propertyDescriptor != null)
						{
							this.compSvc.OnComponentChanging(this.tlp, propertyDescriptor);
						}
						IDesignerHost designerHost = this.tlp.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
						if (designerHost != null)
						{
							foreach (object obj in this.deleteList)
							{
								ArrayList arrayList = new ArrayList();
								DesignerUtils.GetAssociatedComponents((IComponent)obj, designerHost, arrayList);
								foreach (object obj2 in arrayList)
								{
									IComponent component = (IComponent)obj2;
									this.compSvc.OnComponentChanging(component, null);
								}
								designerHost.DestroyComponent(obj as Component);
							}
						}
						if (this.compSvc != null && propertyDescriptor != null)
						{
							this.compSvc.OnComponentChanged(this.tlp, propertyDescriptor, null, null);
						}
					}
					if (this.compSvc != null)
					{
						if (this.rowStyleProp != null)
						{
							this.compSvc.OnComponentChanged(this.tlp, this.rowStyleProp, null, null);
						}
						if (this.colStyleProp != null)
						{
							this.compSvc.OnComponentChanged(this.tlp, this.colStyleProp, null, null);
						}
					}
					base.DialogResult = DialogResult.OK;
				}
				else
				{
					base.DialogResult = DialogResult.Cancel;
				}
				this.tlpDesigner.ResumeEnsureAvailableStyles(true);
				this.tlp.ResumeLayout();
			}

			// Token: 0x060032FC RID: 13052 RVA: 0x001162D4 File Offset: 0x001144D4
			private void OnCancelButtonClick(object sender, EventArgs e)
			{
				this.tlpDesigner.ResumeEnsureAvailableStyles(false);
				this.tlp.ResumeLayout();
				base.DialogResult = DialogResult.Cancel;
			}

			// Token: 0x040021F9 RID: 8697
			private StyleCollectionEditor editor;

			// Token: 0x040021FA RID: 8698
			private bool isRowCollection;

			// Token: 0x040021FB RID: 8699
			private TableLayoutPanel tlp;

			// Token: 0x040021FC RID: 8700
			private TableLayoutPanelDesigner tlpDesigner;

			// Token: 0x040021FD RID: 8701
			private IComponentChangeService compSvc;

			// Token: 0x040021FE RID: 8702
			private ArrayList deleteList;

			// Token: 0x040021FF RID: 8703
			private bool isDialogDirty;

			// Token: 0x04002200 RID: 8704
			private bool haveInvoked;

			// Token: 0x04002201 RID: 8705
			private static int MEMBER_INDEX = 0;

			// Token: 0x04002202 RID: 8706
			private static int TYPE_INDEX = 1;

			// Token: 0x04002203 RID: 8707
			private static int VALUE_INDEX = 2;

			// Token: 0x04002204 RID: 8708
			private PropertyDescriptor rowStyleProp;

			// Token: 0x04002205 RID: 8709
			private PropertyDescriptor colStyleProp;

			// Token: 0x04002206 RID: 8710
			private TableLayoutPanel overarchingTableLayoutPanel;

			// Token: 0x04002207 RID: 8711
			private TableLayoutPanel addRemoveInsertTableLayoutPanel;

			// Token: 0x04002208 RID: 8712
			private Button addButton;

			// Token: 0x04002209 RID: 8713
			private Button removeButton;

			// Token: 0x0400220A RID: 8714
			private Button insertButton;

			// Token: 0x0400220B RID: 8715
			private TableLayoutPanel okCancelTableLayoutPanel;

			// Token: 0x0400220C RID: 8716
			private Button okButton;

			// Token: 0x0400220D RID: 8717
			private Button cancelButton;

			// Token: 0x0400220E RID: 8718
			private Label memberTypeLabel;

			// Token: 0x0400220F RID: 8719
			private ComboBox columnsOrRowsComboBox;

			// Token: 0x04002210 RID: 8720
			private GroupBox sizeTypeGroupBox;

			// Token: 0x04002211 RID: 8721
			private RadioButton absoluteRadioButton;

			// Token: 0x04002212 RID: 8722
			private RadioButton percentRadioButton;

			// Token: 0x04002213 RID: 8723
			private RadioButton autoSizedRadioButton;

			// Token: 0x04002214 RID: 8724
			private StyleCollectionEditor.NavigationalTableLayoutPanel sizeTypeTableLayoutPanel;

			// Token: 0x04002215 RID: 8725
			private Label pixelsLabel;

			// Token: 0x04002216 RID: 8726
			private NumericUpDown absoluteNumericUpDown;

			// Token: 0x04002217 RID: 8727
			private Label percentLabel;

			// Token: 0x04002218 RID: 8728
			private NumericUpDown percentNumericUpDown;

			// Token: 0x04002219 RID: 8729
			private ListView columnsAndRowsListView;

			// Token: 0x0400221A RID: 8730
			private ColumnHeader membersColumnHeader;

			// Token: 0x0400221B RID: 8731
			private ColumnHeader sizeTypeColumnHeader;

			// Token: 0x0400221C RID: 8732
			private TableLayoutPanel helperTextTableLayoutPanel;

			// Token: 0x0400221D RID: 8733
			private PictureBox infoPictureBox1;

			// Token: 0x0400221E RID: 8734
			private PictureBox infoPictureBox2;

			// Token: 0x0400221F RID: 8735
			private LinkLabel helperLinkLabel1;

			// Token: 0x04002220 RID: 8736
			private LinkLabel helperLinkLabel2;

			// Token: 0x04002221 RID: 8737
			private TableLayoutPanel showTableLayoutPanel;

			// Token: 0x04002222 RID: 8738
			private ColumnHeader valueColumnHeader;

			// Token: 0x04002223 RID: 8739
			private const int UpDownLeftMargin = 10;

			// Token: 0x04002224 RID: 8740
			private int scaledUpDownLeftMargin = 10;

			// Token: 0x04002225 RID: 8741
			private const int UpDownTopMargin = 4;

			// Token: 0x04002226 RID: 8742
			private int scaledUpDownTopMargin = 4;

			// Token: 0x04002227 RID: 8743
			private const int LabelRightMargin = 5;

			// Token: 0x04002228 RID: 8744
			private int scaledLabelRightMargin = 5;
		}
	}
}
