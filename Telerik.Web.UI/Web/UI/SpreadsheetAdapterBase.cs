using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020008D7 RID: 2263
	public abstract class SpreadsheetAdapterBase
	{
		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x0600552C RID: 21804 RVA: 0x00104396 File Offset: 0x00102596
		// (set) Token: 0x0600552D RID: 21805 RVA: 0x0010439E File Offset: 0x0010259E
		public ISpreadsheet Owner { get; set; }

		// Token: 0x0600552E RID: 21806 RVA: 0x001043A7 File Offset: 0x001025A7
		public SpreadsheetAdapterBase(ISpreadsheet owner)
		{
			this.Owner = owner;
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x001043B8 File Offset: 0x001025B8
		protected SpreadsheetToolbar GetDefaultToolbar()
		{
			SpreadsheetToolbar spreadsheetToolbar = new SpreadsheetToolbar();
			foreach (KeyValuePair<string, SpreadsheetToolName[][]> keyValuePair in SpreadsheetAdapterBase.DefaultToolbar)
			{
				SpreadsheetToolbarTab spreadsheetToolbarTab = new SpreadsheetToolbarTab
				{
					Text = this.GetLocalizedString(keyValuePair.Key)
				};
				spreadsheetToolbar.Tabs.Add(spreadsheetToolbarTab);
				SpreadsheetToolName[][] value = keyValuePair.Value;
				for (int i = 0; i < value.Length; i++)
				{
					SpreadsheetToolbarGroup spreadsheetToolbarGroup = new SpreadsheetToolbarGroup();
					for (int j = 0; j < value[i].Length; j++)
					{
						SpreadsheetToolName spreadsheetToolName = value[i][j];
						bool showLabel = spreadsheetToolName == SpreadsheetToolName.MergeCells || spreadsheetToolName == SpreadsheetToolName.Validation;
						spreadsheetToolbarGroup.Tools.Add(new SpreadsheetTool
						{
							Name = spreadsheetToolName,
							ShowLabel = showLabel
						});
					}
					spreadsheetToolbarTab.Groups.Add(spreadsheetToolbarGroup);
				}
			}
			return spreadsheetToolbar;
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x001044C0 File Offset: 0x001026C0
		protected string GetLocalizedString(string key)
		{
			return this.Owner.Localization.GetString(key);
		}

		// Token: 0x040014F5 RID: 5365
		internal static readonly Dictionary<SpreadsheetToolName, SpreadsheetToolInfo> DefaultTools = new Dictionary<SpreadsheetToolName, SpreadsheetToolInfo>
		{
			{
				SpreadsheetToolName.Open,
				new SpreadsheetToolInfo("Action", string.Empty, "open", string.Empty, "folder-open", "ToolBarOpen")
			},
			{
				SpreadsheetToolName.ExportAs,
				new SpreadsheetToolInfo("DialogCommand", "exportAsDialog", string.Empty, "exportAs", "download", "ToolBarExportAs")
			},
			{
				SpreadsheetToolName.Undo,
				new SpreadsheetToolInfo("Action", string.Empty, "undo", string.Empty, "undo", "ToolBarUndo")
			},
			{
				SpreadsheetToolName.Redo,
				new SpreadsheetToolInfo("Action", string.Empty, "redo", string.Empty, "redo", "ToolBarRedo")
			},
			{
				SpreadsheetToolName.Save,
				new SpreadsheetToolInfo("ToolBarCommand", string.Empty, "save", string.Empty, "save", "ToolBarSave")
			},
			{
				SpreadsheetToolName.Cut,
				new SpreadsheetToolInfo("ToolbarCutCommand", string.Empty, string.Empty, string.Empty, "cut", "ToolBarCut")
			},
			{
				SpreadsheetToolName.Copy,
				new SpreadsheetToolInfo("ToolbarCopyCommand", string.Empty, string.Empty, string.Empty, "copy", "ToolBarCopy")
			},
			{
				SpreadsheetToolName.Paste,
				new SpreadsheetToolInfo("ToolbarPasteCommand", string.Empty, string.Empty, string.Empty, "paste", "ToolBarPaste")
			},
			{
				SpreadsheetToolName.Bold,
				new SpreadsheetToolInfo("PropertyChangeCommand", "bold", "true", "bold", "bold", "ToolBarBold")
			},
			{
				SpreadsheetToolName.Italic,
				new SpreadsheetToolInfo("PropertyChangeCommand", "italic", "true", "italic", "italic", "ToolBarItalic")
			},
			{
				SpreadsheetToolName.Underline,
				new SpreadsheetToolInfo("PropertyChangeCommand", "underline", "true", "underline", "underline", "ToolBarUnderline")
			},
			{
				SpreadsheetToolName.InsertComment,
				new SpreadsheetToolInfo("DialogCommand", "insertComment", string.Empty, "add-comment", "add-comment", "ToolBarInsertComment")
			},
			{
				SpreadsheetToolName.InsertImage,
				new SpreadsheetToolInfo("DialogCommand", "insertImage", string.Empty, string.Empty, "image", "ToolBarInsertImage")
			},
			{
				SpreadsheetToolName.FontFamily,
				new SpreadsheetToolInfo("PropertyChangeCommand", "fontFamily", string.Empty, string.Empty, "font-family", "ToolBarFontFamily")
			},
			{
				SpreadsheetToolName.FontSize,
				new SpreadsheetToolInfo("PropertyChangeCommand", "fontSize", string.Empty, string.Empty, "font-size", "ToolBarFontSize")
			},
			{
				SpreadsheetToolName.BackgroundColor,
				new SpreadsheetToolInfo("PropertyChangeCommand", "background", string.Empty, string.Empty, "rssBgColor", "ToolBarBackgroundColor")
			},
			{
				SpreadsheetToolName.TextColor,
				new SpreadsheetToolInfo("PropertyChangeCommand", "color", string.Empty, string.Empty, "rssTextColor", "ToolBarTextColor")
			},
			{
				SpreadsheetToolName.BorderType,
				new SpreadsheetToolInfo("BorderChangeCommand", "borderType", "allBorders", string.Empty, "borders-all", "ToolBarBordersAll", new List<SpreadsheetToolInfo>
				{
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "allBorders", string.Empty, "borders-all", "ToolBarBordersAll"),
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "insideBorders", string.Empty, "borders-inside", "ToolBarBordersInside"),
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "insideHorizontalBorders", string.Empty, "borders-inside-horizontal", "ToolBarBordersInsideHorizontal"),
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "insideVerticalBorders", string.Empty, "borders-inside-vertical", "ToolBarBordersInsideVertical"),
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "outsideBorders", string.Empty, "borders-outside", "ToolBarBordersOutside"),
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "leftBorder", string.Empty, "borders-left", "ToolBarBordersLeft"),
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "topBorder", string.Empty, "borders-top", "ToolBarBordersTop"),
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "rightBorder", string.Empty, "borders-right", "ToolBarBordersRight"),
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "bottomBorder", string.Empty, "borders-bottom", "ToolBarBordersBottom"),
					new SpreadsheetToolInfo("BorderChangeCommand", string.Empty, "noBorders", string.Empty, "borders-no", "ToolBarBordersNo")
				})
			},
			{
				SpreadsheetToolName.BorderColor,
				new SpreadsheetToolInfo("BorderChangeCommand", "borderColor", string.Empty, string.Empty, "rssBorderColor", "ToolBarBorderColor")
			},
			{
				SpreadsheetToolName.TextWrap,
				new SpreadsheetToolInfo("TextWrapCommand", "wrap", "true", "wrap", "text-wrap", "ToolBarTextWrap")
			},
			{
				SpreadsheetToolName.HorizontalAlignment,
				new SpreadsheetToolInfo(string.Empty, string.Empty, string.Empty, string.Empty, "align-left", "ToolBarHorizontalAlignment", new List<SpreadsheetToolInfo>
				{
					new SpreadsheetToolInfo("PropertyChangeCommand", "textAlign", "left", string.Empty, "align-left", "ToolBarAlignLeft"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "textAlign", "center", string.Empty, "align-center", "ToolBarAlignCenter"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "textAlign", "right", string.Empty, "align-right", "ToolBarAlignRight"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "textAlign", "justify", string.Empty, "align-justify", "ToolBarAlignJustify")
				})
			},
			{
				SpreadsheetToolName.VerticalAlignment,
				new SpreadsheetToolInfo(string.Empty, string.Empty, string.Empty, string.Empty, "align-top", "ToolBarVerticalAlignment", new List<SpreadsheetToolInfo>
				{
					new SpreadsheetToolInfo("PropertyChangeCommand", "verticalAlign", "top", string.Empty, "align-top", "ToolBarAlignTop"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "verticalAlign", "center", string.Empty, "align-middle", "ToolBarAlignMiddle"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "verticalAlign", "bottom", string.Empty, "align-bottom", "ToolBarAlignBottom")
				})
			},
			{
				SpreadsheetToolName.MergeCells,
				new SpreadsheetToolInfo("MergeCellCommand", string.Empty, "cells", string.Empty, "merge-cells", "ToolBarMergeCells", new List<SpreadsheetToolInfo>
				{
					new SpreadsheetToolInfo("MergeCellCommand", string.Empty, "cells", string.Empty, "merge-cells", "ToolBarMergeCells"),
					new SpreadsheetToolInfo("MergeCellCommand", string.Empty, "horizontally", string.Empty, "merge-cells-h", "ToolBarMergeHorizontally"),
					new SpreadsheetToolInfo("MergeCellCommand", string.Empty, "vertically", string.Empty, "merge-cells-v", "ToolBarMergeVertically"),
					new SpreadsheetToolInfo("MergeCellCommand", string.Empty, "unmerge", string.Empty, "unmerge", "ToolBarUnmerge")
				})
			},
			{
				SpreadsheetToolName.Format,
				new SpreadsheetToolInfo(string.Empty, string.Empty, string.Empty, string.Empty, "custom-format", "ToolBarFormat", new List<SpreadsheetToolInfo>
				{
					new SpreadsheetToolInfo("PropertyChangeCommand", "format", string.Empty, string.Empty, string.Empty, "ToolBarFormatAutomatic"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "format", "#,0.00", string.Empty, string.Empty, "ToolBarFormatNumber"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "format", "0.00%", string.Empty, string.Empty, "ToolBarFormatPercent"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "format", "_(\"$\"* #,##0.00_);_(\"$\"* (#,##0.00);_(\"$\"* \"-\"??_);_(@_)", string.Empty, string.Empty, "ToolBarFormatFinancial"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "format", "$#,##0.00;[Red]$#,##0.00", string.Empty, string.Empty, "ToolBarFormatCurrency"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "format", "m/d/yyyy", string.Empty, string.Empty, "ToolBarFormatDate"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "format", "h:mm:ss AM/PM", string.Empty, string.Empty, "ToolBarFormatTime"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "format", "m/d/yyyy h:mm", string.Empty, string.Empty, "ToolBarFormatDateTime"),
					new SpreadsheetToolInfo("PropertyChangeCommand", "format", "[h]:mm:ss", string.Empty, string.Empty, "ToolBarFormatDuration"),
					new SpreadsheetToolInfo("DialogCommand", "formatCells", string.Empty, string.Empty, string.Empty, "ToolBarMoreFormats")
				})
			},
			{
				SpreadsheetToolName.Freeze,
				new SpreadsheetToolInfo("FreezePanesCommand", string.Empty, "panes", string.Empty, "freeze-panes", "ToolBarFreezePanes", new List<SpreadsheetToolInfo>
				{
					new SpreadsheetToolInfo("FreezePanesCommand", string.Empty, "panes", string.Empty, "freeze-panes", "ToolBarFreezePanes"),
					new SpreadsheetToolInfo("FreezePanesCommand", string.Empty, "rows", string.Empty, "freeze-rows", "ToolBarFreezeRows"),
					new SpreadsheetToolInfo("FreezePanesCommand", string.Empty, "columns", string.Empty, "freeze-columns", "ToolBarFreezeColumns"),
					new SpreadsheetToolInfo("FreezePanesCommand", string.Empty, "unfreeze", string.Empty, "unfreeze", "ToolBarUnfreeze")
				})
			},
			{
				SpreadsheetToolName.FormatIncreaseDecimal,
				new SpreadsheetToolInfo("AdjustDecimalsCommand", string.Empty, "1", string.Empty, "increase-decimal", "ToolBarFormatIncreaseDecimal")
			},
			{
				SpreadsheetToolName.FormatDecreaseDecimal,
				new SpreadsheetToolInfo("AdjustDecimalsCommand", string.Empty, "-1", string.Empty, "decrease-decimal", "ToolBarFormatDecreaseDecimal")
			},
			{
				SpreadsheetToolName.InsertCells,
				new SpreadsheetToolInfo("AddColumnCommand", string.Empty, "left", string.Empty, "table-insert-column-to-the-left", "ToolBarAddColumnLeft", new List<SpreadsheetToolInfo>
				{
					new SpreadsheetToolInfo("AddColumnCommand", string.Empty, "left", string.Empty, "table-insert-column-to-the-left", "ToolBarAddColumnLeft"),
					new SpreadsheetToolInfo("AddColumnCommand", string.Empty, "right", string.Empty, "table-insert-column-to-the-right", "ToolBarAddColumnRight"),
					new SpreadsheetToolInfo("AddRowCommand", string.Empty, "above", string.Empty, "table-insert-row-above", "ToolBarAddRowAbove"),
					new SpreadsheetToolInfo("AddRowCommand", string.Empty, "below", string.Empty, "table-insert-row-below", "ToolBarAddRowBelow")
				})
			},
			{
				SpreadsheetToolName.DeleteCells,
				new SpreadsheetToolInfo(string.Empty, string.Empty, string.Empty, string.Empty, "table-delete-column", "ToolBarDeleteCells", new List<SpreadsheetToolInfo>
				{
					new SpreadsheetToolInfo("DeleteColumnCommand", string.Empty, string.Empty, string.Empty, "table-delete-column", "ToolBarDeleteColumn"),
					new SpreadsheetToolInfo("DeleteRowCommand", string.Empty, string.Empty, string.Empty, "table-delete-row", "ToolBarDeleteRow")
				})
			},
			{
				SpreadsheetToolName.Sort,
				new SpreadsheetToolInfo("SortCommand", string.Empty, "asc", string.Empty, "sort-asc", "ToolBarSortAscending", new List<SpreadsheetToolInfo>
				{
					new SpreadsheetToolInfo("SortCommand", string.Empty, "asc", string.Empty, "sort-asc", "ToolBarSortAscending"),
					new SpreadsheetToolInfo("SortCommand", string.Empty, "desc", string.Empty, "sort-desc", "ToolBarSortDescending")
				})
			},
			{
				SpreadsheetToolName.Filter,
				new SpreadsheetToolInfo("FilterCommand", "hasFilter", string.Empty, string.Empty, "filter", "ToolBarFilter")
			},
			{
				SpreadsheetToolName.Validation,
				new SpreadsheetToolInfo("DialogCommand", "validation", string.Empty, string.Empty, "data-validation", "ToolBarValidation")
			},
			{
				SpreadsheetToolName.GridLines,
				new SpreadsheetToolInfo("GridLinesChangeCommand", "gridLines", "true", string.Empty, "borders-no", "ToolBarGridLines")
			},
			{
				SpreadsheetToolName.Hyperlink,
				new SpreadsheetToolInfo("DialogCommand", "hyperlink", string.Empty, string.Empty, "link", "ToolBarHyperlink")
			}
		};

		// Token: 0x040014F6 RID: 5366
		internal static readonly Dictionary<string, SpreadsheetToolName[][]> DefaultToolbar = new Dictionary<string, SpreadsheetToolName[][]>
		{
			{
				"ToolBarHome",
				new SpreadsheetToolName[][]
				{
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.Open
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.ExportAs
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.Undo,
						SpreadsheetToolName.Redo,
						SpreadsheetToolName.Save
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.Paste,
						SpreadsheetToolName.Cut,
						SpreadsheetToolName.Copy
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.Bold,
						SpreadsheetToolName.Italic,
						SpreadsheetToolName.Underline
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.Hyperlink
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.InsertComment
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.InsertImage
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.FontFamily
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.FontSize
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.BackgroundColor,
						SpreadsheetToolName.TextColor
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.BorderType,
						SpreadsheetToolName.BorderColor
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.HorizontalAlignment,
						SpreadsheetToolName.VerticalAlignment
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.TextWrap,
						SpreadsheetToolName.MergeCells
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.Format,
						SpreadsheetToolName.FormatIncreaseDecimal,
						SpreadsheetToolName.FormatDecreaseDecimal
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.Freeze
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.Filter
					},
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.GridLines
					}
				}
			},
			{
				"ToolBarInsert",
				new SpreadsheetToolName[][]
				{
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.InsertCells,
						SpreadsheetToolName.DeleteCells
					}
				}
			},
			{
				"ToolBarData",
				new SpreadsheetToolName[][]
				{
					new SpreadsheetToolName[]
					{
						SpreadsheetToolName.Sort,
						SpreadsheetToolName.Filter,
						SpreadsheetToolName.Validation
					}
				}
			}
		};

		// Token: 0x040014F7 RID: 5367
		internal static readonly string[] DefaultFontSizes = new string[]
		{
			"8",
			"9",
			"10",
			"11",
			"12",
			"13",
			"14",
			"16",
			"18",
			"20",
			"22",
			"24",
			"26",
			"28",
			"36",
			"48",
			"72"
		};

		// Token: 0x040014F8 RID: 5368
		internal static readonly string[] DefaultFontFamilies = new string[]
		{
			"Arial",
			"Courier New",
			"Georgia",
			"Times New Roman",
			"Trebuchet MS",
			"Verdana"
		};
	}
}
