using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ComboBox
{
	// Token: 0x02000A15 RID: 2581
	public class ClassicRenderer : DecoratedRendererBase
	{
		// Token: 0x060061E3 RID: 25059 RVA: 0x0017164E File Offset: 0x0016F84E
		public ClassicRenderer(RadComboBox owner) : base(owner)
		{
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x00171658 File Offset: 0x0016F858
		protected override void RenderWrapper(HtmlTextWriter writer)
		{
			Table table = new Table();
			if (base.Owner.EnableTableHeaders)
			{
				AccessibilityHelper.AddAccessibilityRow(table, base.Owner.TableCaption);
			}
			if (!base.Owner.IsControlEnabled)
			{
				table.Attributes["class"] = "rcbDisabled";
			}
			table.Attributes["summary"] = base.Owner.TableSummary;
			string text = "border-width:0;border-collapse:collapse;";
			if (base.Owner.InDesignMode || base.Owner.Browser.IsBrowser("IE"))
			{
				text += "table-layout:fixed;";
			}
			if (base.Owner.Label.Length > 0)
			{
				if (base.Owner.Width == Unit.Empty)
				{
					text += string.Format("width:160px", new object[0]);
				}
				else if (base.Owner.Width.Type != UnitType.Percentage)
				{
					text += string.Format("width:{0}{1}", base.Owner.Width.Value, "px");
				}
				else
				{
					text += "display:none";
				}
			}
			else if (base.Owner.InDesignMode || (!base.Owner.Browser.IsBrowser("Gecko") && !base.Owner.Browser.IsBrowser("Firefox")))
			{
				text += "width:100%";
			}
			table.Attributes["style"] = text;
			TableRow tableRow = new TableRow();
			if (base.Owner.ReadOnly)
			{
				tableRow.Attributes["class"] = "rcbReadOnly";
			}
			TableCell tableCell = new TableCell();
			if (base.Owner.InDesignMode || base.Owner.Browser.IsBrowser("IE"))
			{
				tableCell.Style["margin-top"] = "-1px";
				tableCell.Style["margin-bottom"] = "-1px";
			}
			tableCell.Style["width"] = "100%";
			Control child = base.CreateInput();
			tableCell.Controls.Add(child);
			TableCell tableCell2 = new TableCell();
			if (base.Owner.InDesignMode || base.Owner.Browser.IsBrowser("IE"))
			{
				tableCell2.Style["margin-top"] = "-1px";
				tableCell2.Style["margin-bottom"] = "-1px";
			}
			HyperLink hyperLink = new HyperLink
			{
				Text = "select"
			};
			string text2 = "overflow: hidden;display: block;";
			if (base.Owner.InDesignMode || !base.Owner.Browser.IsBrowser("Opera"))
			{
				text2 += "position: relative;outline: none;";
			}
			hyperLink.Attributes["style"] = text2;
			hyperLink.Attributes["id"] = base.Owner.ClientID + "_Arrow";
			tableCell2.Controls.Add(hyperLink);
			if (base.Owner.RadComboBoxImagePosition == RadComboBoxImagePosition.Right)
			{
				tableCell2.Attributes["class"] = "rcbArrowCell rcbArrowCellRight";
				if (!base.Owner.ShowToggleImage)
				{
					AttributeCollection attributes;
					(attributes = tableCell2.Attributes)["class"] = attributes["class"] + " rcbArrowCellHidden";
				}
				tableCell.Attributes["class"] = "rcbInputCell rcbInputCellLeft";
				tableRow.Cells.Add(tableCell);
				tableRow.Cells.Add(tableCell2);
			}
			else
			{
				tableCell2.Attributes["class"] = "rcbArrowCell rcbArrowCellLeft";
				if (!base.Owner.ShowToggleImage)
				{
					AttributeCollection attributes2;
					(attributes2 = tableCell2.Attributes)["class"] = attributes2["class"] + " rcbArrowCellHidden";
				}
				tableCell.Attributes["class"] = "rcbInputCell rcbInputCellRight";
				tableRow.Cells.Add(tableCell2);
				tableRow.Cells.Add(tableCell);
			}
			if (!string.IsNullOrEmpty(base.Owner.TableCaption))
			{
				table.Caption = base.Owner.TableCaption;
			}
			table.Rows.Add(tableRow);
			table.RenderControl(writer);
		}
	}
}
