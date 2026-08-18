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
	// Token: 0x0200015D RID: 349
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class FormatPage : BaseDataListPage
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0004F28F File Offset: 0x0004D48F
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

		// Token: 0x06000C43 RID: 3139 RVA: 0x0004F2A4 File Offset: 0x0004D4A4
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
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0004F31C File Offset: 0x0004D51C
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
			base.Icon = BitmapSelector.CreateIcon(base.GetType(), "FormatPage.ico");
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

		// Token: 0x06000C45 RID: 3141 RVA: 0x000500B0 File Offset: 0x0004E2B0
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

		// Token: 0x06000C46 RID: 3142 RVA: 0x000506C0 File Offset: 0x0004E8C0
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

		// Token: 0x06000C47 RID: 3143 RVA: 0x00050799 File Offset: 0x0004E999
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

		// Token: 0x06000C48 RID: 3144 RVA: 0x000507D8 File Offset: 0x0004E9D8
		protected override void LoadComponent()
		{
			if (base.IsFirstActivate())
			{
				this.InitFontList();
			}
			this.InitPage();
			this.InitFormatTree();
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000507F4 File Offset: 0x0004E9F4
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

		// Token: 0x06000C4A RID: 3146 RVA: 0x000509B0 File Offset: 0x0004EBB0
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

		// Token: 0x06000C4B RID: 3147 RVA: 0x000509F8 File Offset: 0x0004EBF8
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

		// Token: 0x06000C4C RID: 3148 RVA: 0x00050A3F File Offset: 0x0004EC3F
		private void OnFontNameChanged(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.fontNameChanged = true;
			this.OnFormatChanged(this.fontNameCombo, EventArgs.Empty);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00050A62 File Offset: 0x0004EC62
		private void OnFontSizeChanged(object source, EventArgs e)
		{
			if (base.IsLoading())
			{
				return;
			}
			this.UpdateEnabledVisibleState();
			this.OnFormatChanged(this.fontSizeCombo, EventArgs.Empty);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00050A84 File Offset: 0x0004EC84
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (this.formatTree.Nodes.Count != 0)
			{
				IntPtr handle = this.formatTree.Handle;
				this.formatTree.SelectedNode = this.formatTree.Nodes[0];
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00050AD2 File Offset: 0x0004ECD2
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

		// Token: 0x06000C50 RID: 3152 RVA: 0x00050B00 File Offset: 0x0004ED00
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

		// Token: 0x06000C51 RID: 3153 RVA: 0x00050B5C File Offset: 0x0004ED5C
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

		// Token: 0x06000C52 RID: 3154 RVA: 0x00050BC4 File Offset: 0x0004EDC4
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

		// Token: 0x06000C53 RID: 3155 RVA: 0x00050D52 File Offset: 0x0004EF52
		public override void SetComponent(IComponent component)
		{
			base.SetComponent(component);
			this.InitForm();
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00050D64 File Offset: 0x0004EF64
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

		// Token: 0x04000762 RID: 1890
		private const int IDX_ENTIRE = 0;

		// Token: 0x04000763 RID: 1891
		private const int IDX_PAGER = 1;

		// Token: 0x04000764 RID: 1892
		private const int IDX_HEADER = 0;

		// Token: 0x04000765 RID: 1893
		private const int IDX_FOOTER = 1;

		// Token: 0x04000766 RID: 1894
		private const int IDX_ROW_NORMAL = 2;

		// Token: 0x04000767 RID: 1895
		private const int IDX_ROW_ALT = 3;

		// Token: 0x04000768 RID: 1896
		private const int IDX_ROW_SELECTED = 4;

		// Token: 0x04000769 RID: 1897
		private const int IDX_ROW_EDIT = 5;

		// Token: 0x0400076A RID: 1898
		private const int ROW_TYPE_COUNT = 6;

		// Token: 0x0400076B RID: 1899
		private const int COL_ROW_TYPE_COUNT = 3;

		// Token: 0x0400076C RID: 1900
		private const int IDX_ITEM_NORMAL = 2;

		// Token: 0x0400076D RID: 1901
		private const int IDX_ITEM_ALT = 3;

		// Token: 0x0400076E RID: 1902
		private const int IDX_ITEM_SELECTED = 4;

		// Token: 0x0400076F RID: 1903
		private const int IDX_ITEM_EDIT = 5;

		// Token: 0x04000770 RID: 1904
		private const int IDX_ITEM_SEPARATOR = 6;

		// Token: 0x04000771 RID: 1905
		private const int ITEM_TYPE_COUNT = 7;

		// Token: 0x04000772 RID: 1906
		private const int IDX_FSIZE_SMALLER = 1;

		// Token: 0x04000773 RID: 1907
		private const int IDX_FSIZE_LARGER = 2;

		// Token: 0x04000774 RID: 1908
		private const int IDX_FSIZE_XXSMALL = 3;

		// Token: 0x04000775 RID: 1909
		private const int IDX_FSIZE_XSMALL = 4;

		// Token: 0x04000776 RID: 1910
		private const int IDX_FSIZE_SMALL = 5;

		// Token: 0x04000777 RID: 1911
		private const int IDX_FSIZE_MEDIUM = 6;

		// Token: 0x04000778 RID: 1912
		private const int IDX_FSIZE_LARGE = 7;

		// Token: 0x04000779 RID: 1913
		private const int IDX_FSIZE_XLARGE = 8;

		// Token: 0x0400077A RID: 1914
		private const int IDX_FSIZE_XXLARGE = 9;

		// Token: 0x0400077B RID: 1915
		private const int IDX_FSIZE_CUSTOM = 10;

		// Token: 0x0400077C RID: 1916
		private const int IDX_HALIGN_NOTSET = 0;

		// Token: 0x0400077D RID: 1917
		private const int IDX_HALIGN_LEFT = 1;

		// Token: 0x0400077E RID: 1918
		private const int IDX_HALIGN_CENTER = 2;

		// Token: 0x0400077F RID: 1919
		private const int IDX_HALIGN_RIGHT = 3;

		// Token: 0x04000780 RID: 1920
		private const int IDX_HALIGN_JUSTIFY = 4;

		// Token: 0x04000781 RID: 1921
		private const int IDX_VALIGN_NOTSET = 0;

		// Token: 0x04000782 RID: 1922
		private const int IDX_VALIGN_TOP = 1;

		// Token: 0x04000783 RID: 1923
		private const int IDX_VALIGN_MIDDLE = 2;

		// Token: 0x04000784 RID: 1924
		private const int IDX_VALIGN_BOTTOM = 3;

		// Token: 0x04000785 RID: 1925
		private System.Windows.Forms.TreeView formatTree;

		// Token: 0x04000786 RID: 1926
		private System.Windows.Forms.Panel stylePanel;

		// Token: 0x04000787 RID: 1927
		private ColorComboBox foreColorCombo;

		// Token: 0x04000788 RID: 1928
		private System.Windows.Forms.Button foreColorPickerButton;

		// Token: 0x04000789 RID: 1929
		private ColorComboBox backColorCombo;

		// Token: 0x0400078A RID: 1930
		private System.Windows.Forms.Button backColorPickerButton;

		// Token: 0x0400078B RID: 1931
		private ComboBox fontNameCombo;

		// Token: 0x0400078C RID: 1932
		private UnsettableComboBox fontSizeCombo;

		// Token: 0x0400078D RID: 1933
		private UnitControl fontSizeUnit;

		// Token: 0x0400078E RID: 1934
		private System.Windows.Forms.CheckBox boldCheck;

		// Token: 0x0400078F RID: 1935
		private System.Windows.Forms.CheckBox italicCheck;

		// Token: 0x04000790 RID: 1936
		private System.Windows.Forms.CheckBox underlineCheck;

		// Token: 0x04000791 RID: 1937
		private System.Windows.Forms.CheckBox strikeOutCheck;

		// Token: 0x04000792 RID: 1938
		private System.Windows.Forms.CheckBox overlineCheck;

		// Token: 0x04000793 RID: 1939
		private System.Windows.Forms.Panel columnPanel;

		// Token: 0x04000794 RID: 1940
		private UnitControl widthUnit;

		// Token: 0x04000795 RID: 1941
		private System.Windows.Forms.CheckBox allowWrappingCheck;

		// Token: 0x04000796 RID: 1942
		private UnsettableComboBox horzAlignCombo;

		// Token: 0x04000797 RID: 1943
		private System.Windows.Forms.Label vertAlignLabel;

		// Token: 0x04000798 RID: 1944
		private UnsettableComboBox vertAlignCombo;

		// Token: 0x04000799 RID: 1945
		private FormatPage.FormatObject currentFormatObject;

		// Token: 0x0400079A RID: 1946
		private FormatPage.FormatTreeNode currentFormatNode;

		// Token: 0x0400079B RID: 1947
		private bool propChangesPending;

		// Token: 0x0400079C RID: 1948
		private bool fontNameChanged;

		// Token: 0x0400079D RID: 1949
		private ArrayList formatNodes;

		// Token: 0x02000478 RID: 1144
		private class FormatTreeNode : System.Windows.Forms.TreeNode
		{
			// Token: 0x06002A2F RID: 10799 RVA: 0x000FD379 File Offset: 0x000FB579
			public FormatTreeNode(string text, FormatPage.FormatObject formatObject) : base(text)
			{
				this.formatObject = formatObject;
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x06002A30 RID: 10800 RVA: 0x000FD389 File Offset: 0x000FB589
			// (set) Token: 0x06002A31 RID: 10801 RVA: 0x000FD391 File Offset: 0x000FB591
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

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x06002A32 RID: 10802 RVA: 0x000FD39A File Offset: 0x000FB59A
			public FormatPage.FormatObject FormatObject
			{
				get
				{
					return this.formatObject;
				}
			}

			// Token: 0x04001D95 RID: 7573
			protected FormatPage.FormatObject formatObject;

			// Token: 0x04001D96 RID: 7574
			protected bool dirty;
		}

		// Token: 0x02000479 RID: 1145
		private abstract class FormatObject
		{
			// Token: 0x06002A33 RID: 10803
			public abstract void LoadFormatInfo();

			// Token: 0x06002A34 RID: 10804
			public abstract void SaveFormatInfo();
		}

		// Token: 0x0200047A RID: 1146
		private class FormatStyle : FormatPage.FormatObject
		{
			// Token: 0x06002A36 RID: 10806 RVA: 0x000FD3A2 File Offset: 0x000FB5A2
			public FormatStyle(Style runtimeStyle)
			{
				this.runtimeStyle = runtimeStyle;
			}

			// Token: 0x170008F3 RID: 2291
			// (get) Token: 0x06002A37 RID: 10807 RVA: 0x000FD3B1 File Offset: 0x000FB5B1
			public bool IsTableItemStyle
			{
				get
				{
					return this.runtimeStyle is TableItemStyle;
				}
			}

			// Token: 0x06002A38 RID: 10808 RVA: 0x000FD3C4 File Offset: 0x000FB5C4
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

			// Token: 0x06002A39 RID: 10809 RVA: 0x000FD5EC File Offset: 0x000FB7EC
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
						goto IL_17E;
					case 3:
						font.Size = FontUnit.XXSmall;
						goto IL_17E;
					case 4:
						font.Size = FontUnit.XSmall;
						goto IL_17E;
					case 5:
						font.Size = FontUnit.Small;
						goto IL_17E;
					case 6:
						font.Size = FontUnit.Medium;
						goto IL_17E;
					case 7:
						font.Size = FontUnit.Large;
						goto IL_17E;
					case 8:
						font.Size = FontUnit.XLarge;
						goto IL_17E;
					case 9:
						font.Size = FontUnit.XXLarge;
						goto IL_17E;
					case 10:
						try
						{
							font.Size = new FontUnit(this.fontSize, CultureInfo.InvariantCulture);
							goto IL_17E;
						}
						catch
						{
							goto IL_17E;
						}
						break;
					default:
						goto IL_17E;
					}
					font.Size = FontUnit.Smaller;
				}
				else
				{
					font.Size = FontUnit.Empty;
				}
				IL_17E:
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

			// Token: 0x04001D97 RID: 7575
			public string foreColor;

			// Token: 0x04001D98 RID: 7576
			public string backColor;

			// Token: 0x04001D99 RID: 7577
			public string fontName;

			// Token: 0x04001D9A RID: 7578
			public bool fontNameChanged;

			// Token: 0x04001D9B RID: 7579
			public int fontType;

			// Token: 0x04001D9C RID: 7580
			public string fontSize;

			// Token: 0x04001D9D RID: 7581
			public bool bold;

			// Token: 0x04001D9E RID: 7582
			public bool italic;

			// Token: 0x04001D9F RID: 7583
			public bool underline;

			// Token: 0x04001DA0 RID: 7584
			public bool strikeOut;

			// Token: 0x04001DA1 RID: 7585
			public bool overline;

			// Token: 0x04001DA2 RID: 7586
			public int horzAlignment;

			// Token: 0x04001DA3 RID: 7587
			public int vertAlignment;

			// Token: 0x04001DA4 RID: 7588
			public bool allowWrapping;

			// Token: 0x04001DA5 RID: 7589
			protected Style runtimeStyle;
		}

		// Token: 0x0200047B RID: 1147
		private class FormatColumn : FormatPage.FormatObject
		{
			// Token: 0x06002A3A RID: 10810 RVA: 0x000FD854 File Offset: 0x000FBA54
			public FormatColumn(DataGridColumn runtimeColumn)
			{
				this.runtimeColumn = runtimeColumn;
			}

			// Token: 0x06002A3B RID: 10811 RVA: 0x000FD864 File Offset: 0x000FBA64
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

			// Token: 0x06002A3C RID: 10812 RVA: 0x000FD8B0 File Offset: 0x000FBAB0
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

			// Token: 0x04001DA6 RID: 7590
			public string width;

			// Token: 0x04001DA7 RID: 7591
			protected DataGridColumn runtimeColumn;
		}
	}
}
