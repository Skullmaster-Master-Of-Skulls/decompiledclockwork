using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls.ListControls
{
	// Token: 0x02000525 RID: 1317
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class FormatPage : BaseDataListPage
	{
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x0010B40F File Offset: 0x0010A40F
		protected override string HelpKeyword
		{
			get
			{
				if (base.IsDataGridMode)
				{
					return "net.Asp.DataGridProperties.Format";
				}
				return "net.Asp.DataListProperties.Format";
			}
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x0010B424 File Offset: 0x0010A424
		private void InitFontList()
		{
			try
			{
				FontFamily[] families = FontFamily.Families;
				for (int i = 0; i < families.Length; i++)
				{
					if (this.fontNameCombo.Items.Count == 0 || this.fontNameCombo.FindStringExact(families[i].Name) == -1)
					{
						this.fontNameCombo.Items.Add(families[i].Name);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x0010B49C File Offset: 0x0010A49C
		private void InitForm()
		{
			System.Windows.Forms.Label label = new System.Windows.Forms.Label();
			this.formatTree = new System.Windows.Forms.TreeView();
			this.stylePanel = new System.Windows.Forms.Panel();
			GroupLabel groupLabel = new GroupLabel();
			System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
			this.foreColorCombo = new ColorComboBox();
			this.foreColorPickerButton = new System.Windows.Forms.Button();
			System.Windows.Forms.Label label3 = new System.Windows.Forms.Label();
			this.backColorCombo = new ColorComboBox();
			this.backColorPickerButton = new System.Windows.Forms.Button();
			System.Windows.Forms.Label label4 = new System.Windows.Forms.Label();
			this.fontNameCombo = new ComboBox();
			System.Windows.Forms.Label label5 = new System.Windows.Forms.Label();
			this.fontSizeCombo = new UnsettableComboBox();
			this.fontSizeUnit = new UnitControl();
			this.boldCheck = new System.Windows.Forms.CheckBox();
			this.italicCheck = new System.Windows.Forms.CheckBox();
			this.underlineCheck = new System.Windows.Forms.CheckBox();
			this.strikeOutCheck = new System.Windows.Forms.CheckBox();
			this.overlineCheck = new System.Windows.Forms.CheckBox();
			GroupLabel groupLabel2 = new GroupLabel();
			System.Windows.Forms.Label label6 = new System.Windows.Forms.Label();
			this.horzAlignCombo = new UnsettableComboBox();
			this.vertAlignLabel = new System.Windows.Forms.Label();
			this.vertAlignCombo = new UnsettableComboBox();
			this.allowWrappingCheck = new System.Windows.Forms.CheckBox();
			GroupLabel groupLabel3 = null;
			System.Windows.Forms.Label label7 = null;
			if (base.IsDataGridMode)
			{
				this.columnPanel = new System.Windows.Forms.Panel();
				groupLabel3 = new GroupLabel();
				label7 = new System.Windows.Forms.Label();
				this.widthUnit = new UnitControl();
			}
			label.SetBounds(4, 4, 111, 14);
			label.Text = SR.GetString("BDLFmt_Objects");
			label.TabStop = false;
			label.TabIndex = 2;
			this.formatTree.SetBounds(4, 20, 162, 350);
			this.formatTree.HideSelection = false;
			this.formatTree.TabIndex = 3;
			this.formatTree.AfterSelect += this.OnSelChangedFormatObject;
			this.stylePanel.SetBounds(177, 4, 230, 370);
			this.stylePanel.TabIndex = 6;
			this.stylePanel.Visible = false;
			groupLabel.SetBounds(0, 2, 224, 14);
			groupLabel.Text = SR.GetString("BDLFmt_AppearanceGroup");
			groupLabel.TabStop = false;
			groupLabel.TabIndex = 1;
			label2.SetBounds(8, 19, 160, 14);
			label2.Text = SR.GetString("BDLFmt_ForeColor");
			label2.TabStop = false;
			label2.TabIndex = 2;
			this.foreColorCombo.SetBounds(8, 37, 102, 22);
			this.foreColorCombo.TabIndex = 3;
			this.foreColorCombo.TextChanged += this.OnFormatChanged;
			this.foreColorCombo.SelectedIndexChanged += this.OnFormatChanged;
			this.foreColorPickerButton.SetBounds(114, 36, 24, 22);
			this.foreColorPickerButton.TabIndex = 4;
			this.foreColorPickerButton.Text = "...";
			this.foreColorPickerButton.FlatStyle = FlatStyle.System;
			this.foreColorPickerButton.Click += this.OnClickForeColorPicker;
			this.foreColorPickerButton.AccessibleName = SR.GetString("BDLFmt_ChooseColorButton");
			this.foreColorPickerButton.AccessibleDescription = SR.GetString("BDLFmt_ChooseForeColorDesc");
			label3.SetBounds(8, 62, 160, 14);
			label3.Text = SR.GetString("BDLFmt_BackColor");
			label3.TabStop = false;
			label3.TabIndex = 5;
			this.backColorCombo.SetBounds(8, 78, 102, 22);
			this.backColorCombo.TabIndex = 6;
			this.backColorCombo.TextChanged += this.OnFormatChanged;
			this.backColorCombo.SelectedIndexChanged += this.OnFormatChanged;
			this.backColorPickerButton.SetBounds(114, 77, 24, 22);
			this.backColorPickerButton.TabIndex = 7;
			this.backColorPickerButton.Text = "...";
			this.backColorPickerButton.FlatStyle = FlatStyle.System;
			this.backColorPickerButton.Click += this.OnClickBackColorPicker;
			this.backColorPickerButton.AccessibleName = SR.GetString("BDLFmt_ChooseColorButton");
			this.backColorPickerButton.AccessibleDescription = SR.GetString("BDLFmt_ChooseBackColorDesc");
			label4.SetBounds(8, 104, 160, 14);
			label4.Text = SR.GetString("BDLFmt_FontName");
			label4.TabStop = false;
			label4.TabIndex = 8;
			this.fontNameCombo.SetBounds(8, 120, 200, 22);
			this.fontNameCombo.Sorted = true;
			this.fontNameCombo.TabIndex = 9;
			this.fontNameCombo.SelectedIndexChanged += this.OnFontNameChanged;
			this.fontNameCombo.TextChanged += this.OnFontNameChanged;
			label5.SetBounds(8, 146, 160, 14);
			label5.Text = SR.GetString("BDLFmt_FontSize");
			label5.TabStop = false;
			label5.TabIndex = 10;
			this.fontSizeCombo.SetBounds(8, 162, 100, 22);
			this.fontSizeCombo.TabIndex = 11;
			this.fontSizeCombo.MaxDropDownItems = 11;
			this.fontSizeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.fontSizeCombo.Items.AddRange(new object[]
			{
				SR.GetString("BDLFmt_FS_Smaller"),
				SR.GetString("BDLFmt_FS_Larger"),
				SR.GetString("BDLFmt_FS_XXSmall"),
				SR.GetString("BDLFmt_FS_XSmall"),
				SR.GetString("BDLFmt_FS_Small"),
				SR.GetString("BDLFmt_FS_Medium"),
				SR.GetString("BDLFmt_FS_Large"),
				SR.GetString("BDLFmt_FS_XLarge"),
				SR.GetString("BDLFmt_FS_XXLarge"),
				SR.GetString("BDLFmt_FS_Custom")
			});
			this.fontSizeCombo.SelectedIndexChanged += this.OnFontSizeChanged;
			this.fontSizeUnit.SetBounds(112, 162, 96, 22);
			this.fontSizeUnit.AllowNegativeValues = false;
			this.fontSizeUnit.TabIndex = 12;
			this.fontSizeUnit.Changed += this.OnFormatChanged;
			this.fontSizeUnit.ValueAccessibleDescription = SR.GetString("BDLFmt_FontSizeValueDesc");
			this.fontSizeUnit.ValueAccessibleName = SR.GetString("BDLFmt_FontSizeValueName");
			this.fontSizeUnit.UnitAccessibleDescription = SR.GetString("BDLFmt_FontSizeUnitDesc");
			this.fontSizeUnit.UnitAccessibleName = SR.GetString("BDLFmt_FontSizeUnitName");
			this.boldCheck.SetBounds(8, 186, 106, 20);
			this.boldCheck.Text = SR.GetString("BDLFmt_FontBold");
			this.boldCheck.TabIndex = 13;
			this.boldCheck.TextAlign = ContentAlignment.MiddleLeft;
			this.boldCheck.FlatStyle = FlatStyle.System;
			this.boldCheck.CheckedChanged += this.OnFormatChanged;
			this.italicCheck.SetBounds(8, 204, 106, 20);
			this.italicCheck.Text = SR.GetString("BDLFmt_FontItalic");
			this.italicCheck.TabIndex = 14;
			this.italicCheck.TextAlign = ContentAlignment.MiddleLeft;
			this.italicCheck.FlatStyle = FlatStyle.System;
			this.italicCheck.CheckedChanged += this.OnFormatChanged;
			this.underlineCheck.SetBounds(8, 222, 106, 20);
			this.underlineCheck.Text = SR.GetString("BDLFmt_FontUnderline");
			this.underlineCheck.TabIndex = 15;
			this.underlineCheck.TextAlign = ContentAlignment.MiddleLeft;
			this.underlineCheck.FlatStyle = FlatStyle.System;
			this.underlineCheck.CheckedChanged += this.OnFormatChanged;
			this.strikeOutCheck.SetBounds(120, 186, 106, 20);
			this.strikeOutCheck.Text = SR.GetString("BDLFmt_FontStrikeout");
			this.strikeOutCheck.TabIndex = 16;
			this.strikeOutCheck.TextAlign = ContentAlignment.MiddleLeft;
			this.strikeOutCheck.FlatStyle = FlatStyle.System;
			this.strikeOutCheck.CheckedChanged += this.OnFormatChanged;
			this.overlineCheck.SetBounds(120, 204, 106, 20);
			this.overlineCheck.Text = SR.GetString("BDLFmt_FontOverline");
			this.overlineCheck.TabIndex = 17;
			this.overlineCheck.TextAlign = ContentAlignment.MiddleLeft;
			this.overlineCheck.FlatStyle = FlatStyle.System;
			this.overlineCheck.CheckedChanged += this.OnFormatChanged;
			groupLabel2.SetBounds(0, 246, 224, 14);
			groupLabel2.Text = SR.GetString("BDLFmt_AlignmentGroup");
			groupLabel2.TabStop = false;
			groupLabel2.TabIndex = 18;
			label6.SetBounds(8, 264, 160, 14);
			label6.Text = SR.GetString("BDLFmt_HorzAlign");
			label6.TabStop = false;
			label6.TabIndex = 19;
			this.horzAlignCombo.SetBounds(8, 280, 190, 22);
			this.horzAlignCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.horzAlignCombo.Items.AddRange(new object[]
			{
				SR.GetString("BDLFmt_HA_Left"),
				SR.GetString("BDLFmt_HA_Center"),
				SR.GetString("BDLFmt_HA_Right"),
				SR.GetString("BDLFmt_HA_Justify")
			});
			this.horzAlignCombo.TabIndex = 20;
			this.horzAlignCombo.SelectedIndexChanged += this.OnFormatChanged;
			this.vertAlignLabel.SetBounds(8, 306, 160, 14);
			this.vertAlignLabel.Text = SR.GetString("BDLFmt_VertAlign");
			this.vertAlignLabel.TabStop = false;
			this.vertAlignLabel.TabIndex = 21;
			this.vertAlignCombo.SetBounds(8, 322, 190, 22);
			this.vertAlignCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.vertAlignCombo.Items.AddRange(new object[]
			{
				SR.GetString("BDLFmt_VA_Top"),
				SR.GetString("BDLFmt_VA_Middle"),
				SR.GetString("BDLFmt_VA_Bottom")
			});
			this.vertAlignCombo.TabIndex = 22;
			this.vertAlignCombo.SelectedIndexChanged += this.OnFormatChanged;
			this.allowWrappingCheck.SetBounds(8, 348, 200, 17);
			this.allowWrappingCheck.Text = SR.GetString("BDLFmt_AllowWrapping");
			this.allowWrappingCheck.TabIndex = 24;
			this.allowWrappingCheck.FlatStyle = FlatStyle.System;
			this.allowWrappingCheck.CheckedChanged += this.OnFormatChanged;
			if (base.IsDataGridMode)
			{
				this.columnPanel.SetBounds(177, 4, 279, 350);
				this.columnPanel.TabIndex = 7;
				this.columnPanel.Visible = false;
				groupLabel3.SetBounds(0, 0, 279, 14);
				groupLabel3.Text = SR.GetString("BDLFmt_LayoutGroup");
				groupLabel3.TabStop = false;
				groupLabel3.TabIndex = 0;
				label7.SetBounds(8, 20, 64, 14);
				label7.Text = SR.GetString("BDLFmt_Width");
				label7.TabStop = false;
				label7.TabIndex = 1;
				this.widthUnit.SetBounds(80, 17, 102, 22);
				this.widthUnit.AllowNegativeValues = false;
				this.widthUnit.DefaultUnit = 0;
				this.widthUnit.TabIndex = 2;
				this.widthUnit.Changed += this.OnFormatChanged;
				this.widthUnit.ValueAccessibleName = SR.GetString("BDLFmt_WidthValueName");
				this.widthUnit.ValueAccessibleDescription = SR.GetString("BDLFmt_WidthValueDesc");
				this.widthUnit.UnitAccessibleName = SR.GetString("BDLFmt_WidthUnitName");
				this.widthUnit.UnitAccessibleDescription = SR.GetString("BDLFmt_WidthUnitDesc");
			}
			this.Text = SR.GetString("BDLFmt_Text");
			base.AccessibleDescription = SR.GetString("BDLFmt_Desc");
			base.Size = new Size(408, 370);
			base.CommitOnDeactivate = true;
			base.Icon = new Icon(base.GetType(), "FormatPage.ico");
			this.stylePanel.Controls.Clear();
			this.stylePanel.Controls.AddRange(new Control[]
			{
				this.allowWrappingCheck,
				this.vertAlignCombo,
				this.vertAlignLabel,
				this.horzAlignCombo,
				label6,
				groupLabel2,
				this.overlineCheck,
				this.strikeOutCheck,
				this.underlineCheck,
				this.italicCheck,
				this.boldCheck,
				this.fontSizeUnit,
				this.fontSizeCombo,
				label5,
				this.fontNameCombo,
				label4,
				this.backColorPickerButton,
				this.backColorCombo,
				label3,
				this.foreColorPickerButton,
				this.foreColorCombo,
				label2,
				groupLabel
			});
			if (base.IsDataGridMode)
			{
				this.columnPanel.Controls.Clear();
				this.columnPanel.Controls.AddRange(new Control[]
				{
					this.widthUnit,
					label7,
					groupLabel3
				});
				base.Controls.Clear();
				base.Controls.AddRange(new Control[]
				{
					this.columnPanel,
					this.stylePanel,
					this.formatTree,
					label
				});
				return;
			}
			base.Controls.Clear();
			base.Controls.AddRange(new Control[]
			{
				this.stylePanel,
				this.formatTree,
				label
			});
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x0010C280 File Offset: 0x0010B280
		private void InitFormatTree()
		{
			if (base.IsDataGridMode)
			{
				System.Web.UI.WebControls.DataGrid dataGrid = (System.Web.UI.WebControls.DataGrid)base.GetBaseControl();
				FormatPage.FormatObject formatObject = new FormatPage.FormatStyle(dataGrid.ControlStyle);
				formatObject.LoadFormatInfo();
				FormatPage.FormatTreeNode formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_EntireDG"), formatObject);
				this.formatTree.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataGrid.HeaderStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Header"), formatObject);
				this.formatTree.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataGrid.FooterStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Footer"), formatObject);
				this.formatTree.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataGrid.PagerStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Pager"), formatObject);
				this.formatTree.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				FormatPage.FormatTreeNode formatTreeNode2 = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Items"), null);
				this.formatTree.Nodes.Add(formatTreeNode2);
				formatObject = new FormatPage.FormatStyle(dataGrid.ItemStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_NormalItems"), formatObject);
				formatTreeNode2.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataGrid.AlternatingItemStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_AltItems"), formatObject);
				formatTreeNode2.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataGrid.SelectedItemStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_SelItems"), formatObject);
				formatTreeNode2.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataGrid.EditItemStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_EditItems"), formatObject);
				formatTreeNode2.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				DataGridColumnCollection columns = dataGrid.Columns;
				int count = columns.Count;
				if (count != 0)
				{
					FormatPage.FormatTreeNode formatTreeNode3 = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Columns"), null);
					this.formatTree.Nodes.Add(formatTreeNode3);
					for (int i = 0; i < count; i++)
					{
						DataGridColumn dataGridColumn = columns[i];
						string text = "Columns[" + i.ToString(NumberFormatInfo.CurrentInfo) + "]";
						string headerText = dataGridColumn.HeaderText;
						if (headerText.Length != 0)
						{
							text = text + " - " + headerText;
						}
						formatObject = new FormatPage.FormatColumn(dataGridColumn);
						formatObject.LoadFormatInfo();
						FormatPage.FormatTreeNode formatTreeNode4 = new FormatPage.FormatTreeNode(text, formatObject);
						formatTreeNode3.Nodes.Add(formatTreeNode4);
						this.formatNodes.Add(formatTreeNode4);
						formatObject = new FormatPage.FormatStyle(dataGridColumn.HeaderStyle);
						formatObject.LoadFormatInfo();
						formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Header"), formatObject);
						formatTreeNode4.Nodes.Add(formatTreeNode);
						this.formatNodes.Add(formatTreeNode);
						formatObject = new FormatPage.FormatStyle(dataGridColumn.FooterStyle);
						formatObject.LoadFormatInfo();
						formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Footer"), formatObject);
						formatTreeNode4.Nodes.Add(formatTreeNode);
						this.formatNodes.Add(formatTreeNode);
						formatObject = new FormatPage.FormatStyle(dataGridColumn.ItemStyle);
						formatObject.LoadFormatInfo();
						formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Items"), formatObject);
						formatTreeNode4.Nodes.Add(formatTreeNode);
						this.formatNodes.Add(formatTreeNode);
					}
					return;
				}
			}
			else
			{
				DataList dataList = (DataList)base.GetBaseControl();
				FormatPage.FormatObject formatObject = new FormatPage.FormatStyle(dataList.ControlStyle);
				formatObject.LoadFormatInfo();
				FormatPage.FormatTreeNode formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_EntireDL"), formatObject);
				this.formatTree.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataList.HeaderStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Header"), formatObject);
				this.formatTree.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataList.FooterStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Footer"), formatObject);
				this.formatTree.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				FormatPage.FormatTreeNode formatTreeNode5 = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Items"), null);
				this.formatTree.Nodes.Add(formatTreeNode5);
				formatObject = new FormatPage.FormatStyle(dataList.ItemStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_NormalItems"), formatObject);
				formatTreeNode5.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataList.AlternatingItemStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_AltItems"), formatObject);
				formatTreeNode5.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataList.SelectedItemStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_SelItems"), formatObject);
				formatTreeNode5.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataList.EditItemStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_EditItems"), formatObject);
				formatTreeNode5.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
				formatObject = new FormatPage.FormatStyle(dataList.SeparatorStyle);
				formatObject.LoadFormatInfo();
				formatTreeNode = new FormatPage.FormatTreeNode(SR.GetString("BDLFmt_Node_Separators"), formatObject);
				this.formatTree.Nodes.Add(formatTreeNode);
				this.formatNodes.Add(formatTreeNode);
			}
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x0010C890 File Offset: 0x0010B890
		private void InitFormatUI()
		{
			this.foreColorCombo.Color = null;
			this.backColorCombo.Color = null;
			this.fontNameCombo.Text = string.Empty;
			this.fontNameCombo.SelectedIndex = -1;
			this.fontSizeCombo.SelectedIndex = -1;
			this.fontSizeUnit.Value = null;
			this.italicCheck.Checked = false;
			this.underlineCheck.Checked = false;
			this.strikeOutCheck.Checked = false;
			this.overlineCheck.Checked = false;
			this.horzAlignCombo.SelectedIndex = -1;
			this.vertAlignCombo.SelectedIndex = -1;
			this.allowWrappingCheck.Checked = false;
			if (base.IsDataGridMode)
			{
				this.widthUnit.Value = null;
				this.columnPanel.Visible = false;
			}
			this.stylePanel.Visible = false;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x0010C969 File Offset: 0x0010B969
		private void InitPage()
		{
			this.formatNodes = new ArrayList();
			this.propChangesPending = false;
			this.fontNameChanged = false;
			this.currentFormatNode = null;
			this.currentFormatObject = null;
			this.formatTree.Nodes.Clear();
			this.InitFormatUI();
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0010C9A8 File Offset: 0x0010B9A8
		protected override void LoadComponent()
		{
			if (base.IsFirstActivate())
			{
				this.InitFontList();
			}
			this.InitPage();
			this.InitFormatTree();
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x0010C9C4 File Offset: 0x0010B9C4
		private void LoadFormatProperties()
		{
			if (this.currentFormatObject != null)
			{
				base.EnterLoadingMode();
				this.InitFormatUI();
				if (this.currentFormatObject is FormatPage.FormatStyle)
				{
					FormatPage.FormatStyle formatStyle = (FormatPage.FormatStyle)this.currentFormatObject;
					this.foreColorCombo.Color = formatStyle.foreColor;
					this.backColorCombo.Color = formatStyle.backColor;
					int num = -1;
					if (formatStyle.fontName.Length != 0)
					{
						num = this.fontNameCombo.FindStringExact(formatStyle.fontName);
					}
					if (num != -1)
					{
						this.fontNameCombo.SelectedIndex = num;
					}
					else
					{
						this.fontNameCombo.Text = formatStyle.fontName;
					}
					this.boldCheck.Checked = formatStyle.bold;
					this.italicCheck.Checked = formatStyle.italic;
					this.underlineCheck.Checked = formatStyle.underline;
					this.strikeOutCheck.Checked = formatStyle.strikeOut;
					this.overlineCheck.Checked = formatStyle.overline;
					if (formatStyle.fontType != -1)
					{
						this.fontSizeCombo.SelectedIndex = formatStyle.fontType;
						if (formatStyle.fontType == 10)
						{
							this.fontSizeUnit.Value = formatStyle.fontSize;
						}
					}
					if (formatStyle.horzAlignment == 0)
					{
						this.horzAlignCombo.SelectedIndex = -1;
					}
					else
					{
						this.horzAlignCombo.SelectedIndex = formatStyle.horzAlignment;
					}
					if (formatStyle.vertAlignment == 0)
					{
						this.vertAlignCombo.SelectedIndex = -1;
					}
					else
					{
						this.vertAlignCombo.SelectedIndex = formatStyle.vertAlignment;
					}
					this.allowWrappingCheck.Checked = formatStyle.allowWrapping;
				}
				else
				{
					FormatPage.FormatColumn formatColumn = (FormatPage.FormatColumn)this.currentFormatObject;
					this.widthUnit.Value = formatColumn.width;
				}
				base.ExitLoadingMode();
			}
			this.UpdateEnabledVisibleState();
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0010CB80 File Offset: 0x0010BB80
		private void OnClickBackColorPicker(object source, EventArgs e)
		{
			string text = this.backColorCombo.Color;
			text = ColorBuilder.BuildColor(base.GetBaseControl(), this, text);
			if (text != null)
			{
				this.backColorCombo.Color = text;
				this.OnFormatChanged(this.backColorCombo, EventArgs.Empty);
			}
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x0010CBC8 File Offset: 0x0010BBC8
		private void OnClickForeColorPicker(object source, EventArgs e)
		{
			string text = this.foreColorCombo.Color;
			text = ColorBuilder.BuildColor(base.GetBaseControl(), this, text);
			if (text != null)
			{
				this.foreColorCombo.Color = text;
				this.OnFormatChanged(this.foreColorCombo, EventArgs.Empty);
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x0010CC0F File Offset: 0x0010BC0F
		private void OnFontNameChanged(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.fontNameChanged = true;
			this.OnFormatChanged(this.fontNameCombo, EventArgs.Empty);
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0010CC32 File Offset: 0x0010BC32
		private void OnFontSizeChanged(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.UpdateEnabledVisibleState();
			this.OnFormatChanged(this.fontSizeCombo, EventArgs.Empty);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x0010CC54 File Offset: 0x0010BC54
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (this.formatTree.Nodes.Count != 0)
			{
				IntPtr handle = this.formatTree.Handle;
				this.formatTree.SelectedNode = this.formatTree.Nodes[0];
			}
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x0010CCA2 File Offset: 0x0010BCA2
		private void OnFormatChanged(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			if (this.currentFormatNode != null)
			{
				this.SetDirty();
				this.propChangesPending = true;
				this.currentFormatNode.Dirty = true;
			}
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x0010CCD0 File Offset: 0x0010BCD0
		private void OnSelChangedFormatObject(object source, TreeViewEventArgs e)
		{
			if (this.propChangesPending)
			{
				this.SaveFormatProperties();
			}
			this.currentFormatNode = (FormatPage.FormatTreeNode)this.formatTree.SelectedNode;
			if (this.currentFormatNode != null)
			{
				this.currentFormatObject = this.currentFormatNode.FormatObject;
			}
			else
			{
				this.currentFormatObject = null;
			}
			this.LoadFormatProperties();
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0010CD2C File Offset: 0x0010BD2C
		protected override void SaveComponent()
		{
			if (this.propChangesPending)
			{
				this.SaveFormatProperties();
			}
			foreach (object obj in this.formatNodes)
			{
				FormatPage.FormatTreeNode formatTreeNode = (FormatPage.FormatTreeNode)obj;
				if (formatTreeNode.Dirty)
				{
					FormatPage.FormatObject formatObject = formatTreeNode.FormatObject;
					formatObject.SaveFormatInfo();
					formatTreeNode.Dirty = false;
				}
			}
			BaseDataListDesigner baseDesigner = base.GetBaseDesigner();
			baseDesigner.OnStylesChanged();
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x0010CD94 File Offset: 0x0010BD94
		private void SaveFormatProperties()
		{
			if (this.currentFormatObject != null)
			{
				if (this.currentFormatObject is FormatPage.FormatStyle)
				{
					FormatPage.FormatStyle formatStyle = (FormatPage.FormatStyle)this.currentFormatObject;
					formatStyle.foreColor = this.foreColorCombo.Color;
					formatStyle.backColor = this.backColorCombo.Color;
					if (this.fontNameChanged)
					{
						formatStyle.fontName = this.fontNameCombo.Text.Trim();
						formatStyle.fontNameChanged = true;
						this.fontNameChanged = false;
					}
					formatStyle.bold = this.boldCheck.Checked;
					formatStyle.italic = this.italicCheck.Checked;
					formatStyle.underline = this.underlineCheck.Checked;
					formatStyle.strikeOut = this.strikeOutCheck.Checked;
					formatStyle.overline = this.overlineCheck.Checked;
					if (this.fontSizeCombo.IsSet())
					{
						formatStyle.fontType = this.fontSizeCombo.SelectedIndex;
						if (formatStyle.fontType == 10)
						{
							formatStyle.fontSize = this.fontSizeUnit.Value;
						}
					}
					else
					{
						formatStyle.fontType = -1;
					}
					int num = this.horzAlignCombo.SelectedIndex;
					if (num == -1)
					{
						num = 0;
					}
					formatStyle.horzAlignment = num;
					num = this.vertAlignCombo.SelectedIndex;
					if (num == -1)
					{
						num = 0;
					}
					formatStyle.vertAlignment = num;
					formatStyle.allowWrapping = this.allowWrappingCheck.Checked;
				}
				else
				{
					FormatPage.FormatColumn formatColumn = (FormatPage.FormatColumn)this.currentFormatObject;
					formatColumn.width = this.widthUnit.Value;
				}
				this.currentFormatNode.Dirty = true;
			}
			this.propChangesPending = false;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x0010CF22 File Offset: 0x0010BF22
		public override void SetComponent(IComponent component)
		{
			base.SetComponent(component);
			this.InitForm();
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x0010CF34 File Offset: 0x0010BF34
		private void UpdateEnabledVisibleState()
		{
			if (this.currentFormatObject == null)
			{
				this.stylePanel.Visible = false;
				if (base.IsDataGridMode)
				{
					this.columnPanel.Visible = false;
					return;
				}
			}
			else if (this.currentFormatObject is FormatPage.FormatStyle)
			{
				this.stylePanel.Visible = true;
				if (base.IsDataGridMode)
				{
					this.columnPanel.Visible = false;
				}
				this.fontSizeUnit.Enabled = (this.fontSizeCombo.SelectedIndex == 10);
				if (((FormatPage.FormatStyle)this.currentFormatObject).IsTableItemStyle)
				{
					this.vertAlignLabel.Visible = true;
					this.vertAlignCombo.Visible = true;
					this.allowWrappingCheck.Visible = true;
					return;
				}
				this.vertAlignLabel.Visible = false;
				this.vertAlignCombo.Visible = false;
				this.allowWrappingCheck.Visible = false;
				return;
			}
			else
			{
				this.stylePanel.Visible = false;
				this.columnPanel.Visible = true;
			}
		}

		// Token: 0x04001FDC RID: 8156
		private const int IDX_ENTIRE = 0;

		// Token: 0x04001FDD RID: 8157
		private const int IDX_PAGER = 1;

		// Token: 0x04001FDE RID: 8158
		private const int IDX_HEADER = 0;

		// Token: 0x04001FDF RID: 8159
		private const int IDX_FOOTER = 1;

		// Token: 0x04001FE0 RID: 8160
		private const int IDX_ROW_NORMAL = 2;

		// Token: 0x04001FE1 RID: 8161
		private const int IDX_ROW_ALT = 3;

		// Token: 0x04001FE2 RID: 8162
		private const int IDX_ROW_SELECTED = 4;

		// Token: 0x04001FE3 RID: 8163
		private const int IDX_ROW_EDIT = 5;

		// Token: 0x04001FE4 RID: 8164
		private const int ROW_TYPE_COUNT = 6;

		// Token: 0x04001FE5 RID: 8165
		private const int COL_ROW_TYPE_COUNT = 3;

		// Token: 0x04001FE6 RID: 8166
		private const int IDX_ITEM_NORMAL = 2;

		// Token: 0x04001FE7 RID: 8167
		private const int IDX_ITEM_ALT = 3;

		// Token: 0x04001FE8 RID: 8168
		private const int IDX_ITEM_SELECTED = 4;

		// Token: 0x04001FE9 RID: 8169
		private const int IDX_ITEM_EDIT = 5;

		// Token: 0x04001FEA RID: 8170
		private const int IDX_ITEM_SEPARATOR = 6;

		// Token: 0x04001FEB RID: 8171
		private const int ITEM_TYPE_COUNT = 7;

		// Token: 0x04001FEC RID: 8172
		private const int IDX_FSIZE_SMALLER = 1;

		// Token: 0x04001FED RID: 8173
		private const int IDX_FSIZE_LARGER = 2;

		// Token: 0x04001FEE RID: 8174
		private const int IDX_FSIZE_XXSMALL = 3;

		// Token: 0x04001FEF RID: 8175
		private const int IDX_FSIZE_XSMALL = 4;

		// Token: 0x04001FF0 RID: 8176
		private const int IDX_FSIZE_SMALL = 5;

		// Token: 0x04001FF1 RID: 8177
		private const int IDX_FSIZE_MEDIUM = 6;

		// Token: 0x04001FF2 RID: 8178
		private const int IDX_FSIZE_LARGE = 7;

		// Token: 0x04001FF3 RID: 8179
		private const int IDX_FSIZE_XLARGE = 8;

		// Token: 0x04001FF4 RID: 8180
		private const int IDX_FSIZE_XXLARGE = 9;

		// Token: 0x04001FF5 RID: 8181
		private const int IDX_FSIZE_CUSTOM = 10;

		// Token: 0x04001FF6 RID: 8182
		private const int IDX_HALIGN_NOTSET = 0;

		// Token: 0x04001FF7 RID: 8183
		private const int IDX_HALIGN_LEFT = 1;

		// Token: 0x04001FF8 RID: 8184
		private const int IDX_HALIGN_CENTER = 2;

		// Token: 0x04001FF9 RID: 8185
		private const int IDX_HALIGN_RIGHT = 3;

		// Token: 0x04001FFA RID: 8186
		private const int IDX_HALIGN_JUSTIFY = 4;

		// Token: 0x04001FFB RID: 8187
		private const int IDX_VALIGN_NOTSET = 0;

		// Token: 0x04001FFC RID: 8188
		private const int IDX_VALIGN_TOP = 1;

		// Token: 0x04001FFD RID: 8189
		private const int IDX_VALIGN_MIDDLE = 2;

		// Token: 0x04001FFE RID: 8190
		private const int IDX_VALIGN_BOTTOM = 3;

		// Token: 0x04001FFF RID: 8191
		private System.Windows.Forms.TreeView formatTree;

		// Token: 0x04002000 RID: 8192
		private System.Windows.Forms.Panel stylePanel;

		// Token: 0x04002001 RID: 8193
		private ColorComboBox foreColorCombo;

		// Token: 0x04002002 RID: 8194
		private System.Windows.Forms.Button foreColorPickerButton;

		// Token: 0x04002003 RID: 8195
		private ColorComboBox backColorCombo;

		// Token: 0x04002004 RID: 8196
		private System.Windows.Forms.Button backColorPickerButton;

		// Token: 0x04002005 RID: 8197
		private ComboBox fontNameCombo;

		// Token: 0x04002006 RID: 8198
		private UnsettableComboBox fontSizeCombo;

		// Token: 0x04002007 RID: 8199
		private UnitControl fontSizeUnit;

		// Token: 0x04002008 RID: 8200
		private System.Windows.Forms.CheckBox boldCheck;

		// Token: 0x04002009 RID: 8201
		private System.Windows.Forms.CheckBox italicCheck;

		// Token: 0x0400200A RID: 8202
		private System.Windows.Forms.CheckBox underlineCheck;

		// Token: 0x0400200B RID: 8203
		private System.Windows.Forms.CheckBox strikeOutCheck;

		// Token: 0x0400200C RID: 8204
		private System.Windows.Forms.CheckBox overlineCheck;

		// Token: 0x0400200D RID: 8205
		private System.Windows.Forms.Panel columnPanel;

		// Token: 0x0400200E RID: 8206
		private UnitControl widthUnit;

		// Token: 0x0400200F RID: 8207
		private System.Windows.Forms.CheckBox allowWrappingCheck;

		// Token: 0x04002010 RID: 8208
		private UnsettableComboBox horzAlignCombo;

		// Token: 0x04002011 RID: 8209
		private System.Windows.Forms.Label vertAlignLabel;

		// Token: 0x04002012 RID: 8210
		private UnsettableComboBox vertAlignCombo;

		// Token: 0x04002013 RID: 8211
		private FormatPage.FormatObject currentFormatObject;

		// Token: 0x04002014 RID: 8212
		private FormatPage.FormatTreeNode currentFormatNode;

		// Token: 0x04002015 RID: 8213
		private bool propChangesPending;

		// Token: 0x04002016 RID: 8214
		private bool fontNameChanged;

		// Token: 0x04002017 RID: 8215
		private ArrayList formatNodes;

		// Token: 0x02000526 RID: 1318
		private class FormatTreeNode : System.Windows.Forms.TreeNode
		{
			// Token: 0x06002F03 RID: 12035 RVA: 0x0010D033 File Offset: 0x0010C033
			public FormatTreeNode(string text, FormatPage.FormatObject formatObject) : base(text)
			{
				this.formatObject = formatObject;
			}

			// Token: 0x170008E1 RID: 2273
			// (get) Token: 0x06002F04 RID: 12036 RVA: 0x0010D043 File Offset: 0x0010C043
			// (set) Token: 0x06002F05 RID: 12037 RVA: 0x0010D04B File Offset: 0x0010C04B
			public bool Dirty
			{
				get
				{
					return this.dirty;
				}
				set
				{
					this.dirty = value;
				}
			}

			// Token: 0x170008E2 RID: 2274
			// (get) Token: 0x06002F06 RID: 12038 RVA: 0x0010D054 File Offset: 0x0010C054
			public FormatPage.FormatObject FormatObject
			{
				get
				{
					return this.formatObject;
				}
			}

			// Token: 0x04002018 RID: 8216
			protected FormatPage.FormatObject formatObject;

			// Token: 0x04002019 RID: 8217
			protected bool dirty;
		}

		// Token: 0x02000527 RID: 1319
		private abstract class FormatObject
		{
			// Token: 0x06002F07 RID: 12039
			public abstract void LoadFormatInfo();

			// Token: 0x06002F08 RID: 12040
			public abstract void SaveFormatInfo();
		}

		// Token: 0x02000528 RID: 1320
		private class FormatStyle : FormatPage.FormatObject
		{
			// Token: 0x06002F0A RID: 12042 RVA: 0x0010D064 File Offset: 0x0010C064
			public FormatStyle(Style runtimeStyle)
			{
				this.runtimeStyle = runtimeStyle;
			}

			// Token: 0x170008E3 RID: 2275
			// (get) Token: 0x06002F0B RID: 12043 RVA: 0x0010D073 File Offset: 0x0010C073
			public bool IsTableItemStyle
			{
				get
				{
					return this.runtimeStyle is TableItemStyle;
				}
			}

			// Token: 0x06002F0C RID: 12044 RVA: 0x0010D084 File Offset: 0x0010C084
			public override void LoadFormatInfo()
			{
				Color c = this.runtimeStyle.BackColor;
				this.backColor = ColorTranslator.ToHtml(c);
				c = this.runtimeStyle.ForeColor;
				this.foreColor = ColorTranslator.ToHtml(c);
				FontInfo font = this.runtimeStyle.Font;
				this.fontName = font.Name;
				this.fontNameChanged = false;
				this.bold = font.Bold;
				this.italic = font.Italic;
				this.underline = font.Underline;
				this.strikeOut = font.Strikeout;
				this.overline = font.Overline;
				this.fontType = -1;
				FontUnit size = font.Size;
				if (!size.IsEmpty)
				{
					this.fontSize = null;
					switch (size.Type)
					{
					case FontSize.AsUnit:
						this.fontType = 10;
						this.fontSize = size.ToString(CultureInfo.CurrentCulture);
						break;
					case FontSize.Smaller:
						this.fontType = 1;
						break;
					case FontSize.Larger:
						this.fontType = 2;
						break;
					case FontSize.XXSmall:
						this.fontType = 3;
						break;
					case FontSize.XSmall:
						this.fontType = 4;
						break;
					case FontSize.Small:
						this.fontType = 5;
						break;
					case FontSize.Medium:
						this.fontType = 6;
						break;
					case FontSize.Large:
						this.fontType = 7;
						break;
					case FontSize.XLarge:
						this.fontType = 8;
						break;
					case FontSize.XXLarge:
						this.fontType = 9;
						break;
					}
				}
				TableItemStyle tableItemStyle = null;
				HorizontalAlign horizontalAlign;
				if (this.runtimeStyle is TableItemStyle)
				{
					tableItemStyle = (TableItemStyle)this.runtimeStyle;
					horizontalAlign = tableItemStyle.HorizontalAlign;
					this.allowWrapping = tableItemStyle.Wrap;
				}
				else
				{
					horizontalAlign = ((TableStyle)this.runtimeStyle).HorizontalAlign;
				}
				this.horzAlignment = 0;
				switch (horizontalAlign)
				{
				case HorizontalAlign.Left:
					this.horzAlignment = 1;
					break;
				case HorizontalAlign.Center:
					this.horzAlignment = 2;
					break;
				case HorizontalAlign.Right:
					this.horzAlignment = 3;
					break;
				case HorizontalAlign.Justify:
					this.horzAlignment = 4;
					break;
				}
				if (tableItemStyle != null)
				{
					VerticalAlign verticalAlign = tableItemStyle.VerticalAlign;
					this.vertAlignment = 0;
					switch (verticalAlign)
					{
					case VerticalAlign.Top:
						this.vertAlignment = 1;
						return;
					case VerticalAlign.Middle:
						this.vertAlignment = 2;
						return;
					case VerticalAlign.Bottom:
						this.vertAlignment = 3;
						break;
					default:
						return;
					}
				}
			}

			// Token: 0x06002F0D RID: 12045 RVA: 0x0010D2B4 File Offset: 0x0010C2B4
			public override void SaveFormatInfo()
			{
				try
				{
					this.runtimeStyle.BackColor = ColorTranslator.FromHtml(this.backColor);
					this.runtimeStyle.ForeColor = ColorTranslator.FromHtml(this.foreColor);
				}
				catch
				{
				}
				FontInfo font = this.runtimeStyle.Font;
				if (this.fontNameChanged)
				{
					font.Name = this.fontName;
					this.fontNameChanged = false;
				}
				font.Bold = this.bold;
				font.Italic = this.italic;
				font.Underline = this.underline;
				font.Strikeout = this.strikeOut;
				font.Overline = this.overline;
				if (this.fontType != -1)
				{
					switch (this.fontType)
					{
					case 1:
						break;
					case 2:
						font.Size = FontUnit.Larger;
						goto IL_17D;
					case 3:
						font.Size = FontUnit.XXSmall;
						goto IL_17D;
					case 4:
						font.Size = FontUnit.XSmall;
						goto IL_17D;
					case 5:
						font.Size = FontUnit.Small;
						goto IL_17D;
					case 6:
						font.Size = FontUnit.Medium;
						goto IL_17D;
					case 7:
						font.Size = FontUnit.Large;
						goto IL_17D;
					case 8:
						font.Size = FontUnit.XLarge;
						goto IL_17D;
					case 9:
						font.Size = FontUnit.XXLarge;
						goto IL_17D;
					case 10:
						try
						{
							font.Size = new FontUnit(this.fontSize, CultureInfo.InvariantCulture);
							goto IL_17D;
						}
						catch
						{
							goto IL_17D;
						}
						break;
					default:
						goto IL_17D;
					}
					font.Size = FontUnit.Smaller;
				}
				else
				{
					font.Size = FontUnit.Empty;
				}
				IL_17D:
				TableItemStyle tableItemStyle = null;
				HorizontalAlign horizontalAlign = HorizontalAlign.NotSet;
				switch (this.horzAlignment)
				{
				case 0:
					horizontalAlign = HorizontalAlign.NotSet;
					break;
				case 1:
					horizontalAlign = HorizontalAlign.Left;
					break;
				case 2:
					horizontalAlign = HorizontalAlign.Center;
					break;
				case 3:
					horizontalAlign = HorizontalAlign.Right;
					break;
				case 4:
					horizontalAlign = HorizontalAlign.Justify;
					break;
				}
				if (this.runtimeStyle is TableItemStyle)
				{
					tableItemStyle = (TableItemStyle)this.runtimeStyle;
					tableItemStyle.HorizontalAlign = horizontalAlign;
					if (!this.allowWrapping)
					{
						tableItemStyle.Wrap = false;
					}
				}
				else
				{
					((TableStyle)this.runtimeStyle).HorizontalAlign = horizontalAlign;
				}
				if (tableItemStyle != null)
				{
					switch (this.vertAlignment)
					{
					case 0:
						tableItemStyle.VerticalAlign = VerticalAlign.NotSet;
						return;
					case 1:
						tableItemStyle.VerticalAlign = VerticalAlign.Top;
						return;
					case 2:
						tableItemStyle.VerticalAlign = VerticalAlign.Middle;
						return;
					case 3:
						tableItemStyle.VerticalAlign = VerticalAlign.Bottom;
						break;
					default:
						return;
					}
				}
			}

			// Token: 0x0400201A RID: 8218
			public string foreColor;

			// Token: 0x0400201B RID: 8219
			public string backColor;

			// Token: 0x0400201C RID: 8220
			public string fontName;

			// Token: 0x0400201D RID: 8221
			public bool fontNameChanged;

			// Token: 0x0400201E RID: 8222
			public int fontType;

			// Token: 0x0400201F RID: 8223
			public string fontSize;

			// Token: 0x04002020 RID: 8224
			public bool bold;

			// Token: 0x04002021 RID: 8225
			public bool italic;

			// Token: 0x04002022 RID: 8226
			public bool underline;

			// Token: 0x04002023 RID: 8227
			public bool strikeOut;

			// Token: 0x04002024 RID: 8228
			public bool overline;

			// Token: 0x04002025 RID: 8229
			public int horzAlignment;

			// Token: 0x04002026 RID: 8230
			public int vertAlignment;

			// Token: 0x04002027 RID: 8231
			public bool allowWrapping;

			// Token: 0x04002028 RID: 8232
			protected Style runtimeStyle;
		}

		// Token: 0x02000529 RID: 1321
		private class FormatColumn : FormatPage.FormatObject
		{
			// Token: 0x06002F0E RID: 12046 RVA: 0x0010D51C File Offset: 0x0010C51C
			public FormatColumn(DataGridColumn runtimeColumn)
			{
				this.runtimeColumn = runtimeColumn;
			}

			// Token: 0x06002F0F RID: 12047 RVA: 0x0010D52C File Offset: 0x0010C52C
			public override void LoadFormatInfo()
			{
				TableItemStyle headerStyle = this.runtimeColumn.HeaderStyle;
				if (!headerStyle.Width.IsEmpty)
				{
					this.width = headerStyle.Width.ToString(NumberFormatInfo.CurrentInfo);
					return;
				}
				this.width = null;
			}

			// Token: 0x06002F10 RID: 12048 RVA: 0x0010D578 File Offset: 0x0010C578
			public override void SaveFormatInfo()
			{
				TableItemStyle headerStyle = this.runtimeColumn.HeaderStyle;
				if (this.width == null)
				{
					headerStyle.Width = Unit.Empty;
					return;
				}
				try
				{
					headerStyle.Width = new Unit(this.width, CultureInfo.InvariantCulture);
				}
				catch
				{
				}
			}

			// Token: 0x04002029 RID: 8233
			public string width;

			// Token: 0x0400202A RID: 8234
			protected DataGridColumn runtimeColumn;
		}
	}
}
