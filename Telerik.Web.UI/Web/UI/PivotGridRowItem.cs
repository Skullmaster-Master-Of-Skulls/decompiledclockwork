using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DE5 RID: 3557
	public class PivotGridRowItem : PivotGridItem
	{
		// Token: 0x170029BD RID: 10685
		// (get) Token: 0x06008413 RID: 33811 RVA: 0x001E1EA0 File Offset: 0x001E00A0
		// (set) Token: 0x06008414 RID: 33812 RVA: 0x001E1EA8 File Offset: 0x001E00A8
		public List<PivotGridRowZone> RowZones { get; protected set; }

		// Token: 0x170029BE RID: 10686
		// (get) Token: 0x06008415 RID: 33813 RVA: 0x001E1EB1 File Offset: 0x001E00B1
		// (set) Token: 0x06008416 RID: 33814 RVA: 0x001E1EB9 File Offset: 0x001E00B9
		public PivotGridColumnHeaderZone ColumnHeaderZone { get; protected set; }

		// Token: 0x170029BF RID: 10687
		// (get) Token: 0x06008417 RID: 33815 RVA: 0x001E1EC2 File Offset: 0x001E00C2
		// (set) Token: 0x06008418 RID: 33816 RVA: 0x001E1ECA File Offset: 0x001E00CA
		public PivotGridDataZone DataZone { get; protected set; }

		// Token: 0x170029C0 RID: 10688
		// (get) Token: 0x06008419 RID: 33817 RVA: 0x001E1ED3 File Offset: 0x001E00D3
		// (set) Token: 0x0600841A RID: 33818 RVA: 0x001E1EDB File Offset: 0x001E00DB
		internal PivotGridTableCell ScrollBarCell { get; set; }

		// Token: 0x0600841B RID: 33819 RVA: 0x001E1F58 File Offset: 0x001E0158
		internal override void Initialize()
		{
			this.RowZones = new List<PivotGridRowZone>();
			this.Controls.Clear();
			int num = (from f in base.OwnerPivotGrid.Fields
			where f is PivotGridRowField && !f.IsHidden
			select f).Count<PivotGridField>();
			bool flag = base.OwnerPivotGrid.FieldsPopupSettings.RowFieldsMinCount != 0 && base.OwnerPivotGrid.FieldsPopupSettings.RowFieldsMinCount <= num;
			if (base.OwnerPivotGrid.RowTableLayout == PivotGridLayout.Compact || flag)
			{
				if (num > 0)
				{
					PivotGridRowZone pivotGridRowZone = (PivotGridRowZone)this.CreateCellObject();
					this.RowZones.Add(pivotGridRowZone);
					this.Cells.Add(pivotGridRowZone);
				}
			}
			else
			{
				int num2 = -1;
				if (base.OwnerPivotGrid.AggregatesPosition == PivotGridAxis.Rows)
				{
					if (base.OwnerPivotGrid.Fields.Count((PivotGridField f) => f is PivotGridAggregateField && !f.IsHidden) > 1)
					{
						if (base.OwnerPivotGrid.Fields.Count((PivotGridField f) => f is PivotGridRowField && !f.IsHidden) > 0)
						{
							num2 = base.OwnerPivotGrid.AggregatesLevel;
							if (num2 < 0 || num2 > num)
							{
								num2 = num;
							}
							num++;
						}
					}
				}
				for (int i = 0; i < num; i++)
				{
					if (i == num2)
					{
						TableCell tableCell = new TableCell();
						tableCell.Text = "&nbsp;";
						tableCell.CssClass = "rpgRowsZone";
						this.Cells.Add(tableCell);
					}
					else
					{
						PivotGridRowZone pivotGridRowZone2 = (PivotGridRowZone)this.CreateCellObject();
						this.RowZones.Add(pivotGridRowZone2);
						this.Cells.Add(pivotGridRowZone2);
					}
				}
			}
			if (base.OwnerPivotGrid.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				this.zoneType = PivotGridZoneType.ColumnHeader;
				this.ColumnHeaderZone = (PivotGridColumnHeaderZone)this.CreateCellObject();
				this.ColumnHeaderZone.CssClass = "rpgColumnHeaderZone";
				this.Cells.Add(this.ColumnHeaderZone);
			}
			else
			{
				this.zoneType = PivotGridZoneType.Data;
				this.DataZone = (PivotGridDataZone)this.CreateCellObject();
				this.Cells.Add(this.DataZone);
			}
			if (base.OwnerPivotGrid.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				this.ScrollBarCell = new PivotGridTableCell();
				this.Cells.Add(this.ScrollBarCell);
				this.ScrollBarCell.RowSpan = 2;
				this.ScrollBarCell.CssClass = "rpgVerticalScroll";
				base.OwnerPivotGrid.verticalScrollDiv = new Panel();
				base.OwnerPivotGrid.verticalScrollDiv.ID = "Vertical";
				base.OwnerPivotGrid.verticalScrollDiv.CssClass = "rpgVerticalScrollDiv";
				this.ScrollBarCell.Controls.Add(base.OwnerPivotGrid.verticalScrollDiv);
				Panel child = new Panel();
				base.OwnerPivotGrid.verticalScrollDiv.Controls.Add(child);
			}
			int num3 = 0;
			bool flag2;
			if (base.OwnerPivotGrid.AggregatesPosition == PivotGridAxis.Rows)
			{
				flag2 = ((from field in base.OwnerPivotGrid.Fields
				where field is PivotGridAggregateField && !field.IsHidden
				select field).Count<PivotGridField>() > 1);
			}
			else
			{
				flag2 = false;
			}
			bool flag3 = flag2;
			List<PivotGridField> list = (from f in base.OwnerPivotGrid.Fields
			where f is PivotGridRowField && !f.IsHidden
			orderby f.ZoneIndex
			select f).ToList<PivotGridField>();
			int num4 = list.Count<PivotGridField>();
			if (base.OwnerPivotGrid.RowTableLayout == PivotGridLayout.Compact || flag)
			{
				if (this.RowZones.Count > 0)
				{
					if (base.OwnerPivotGrid.Fields.Owner.ShowRowHeaderZone)
					{
						this.RowZones[0].Initialize(list.ToList<PivotGridField>());
					}
					this.RowZones[0].ColumnSpan = list.Count;
					if (flag3)
					{
						this.RowZones[0].ColumnSpan++;
					}
				}
			}
			else if (base.OwnerPivotGrid.Fields.Owner.ShowRowHeaderZone)
			{
				foreach (PivotGridField field2 in list)
				{
					this.RowZones[num3++].Initialize(field2);
					num4++;
				}
			}
			if (num4 == 0)
			{
				TableCell tableCell2 = new TableCell();
				this.Controls.AddAt(0, tableCell2);
				tableCell2.ID = "DropFieldHereCell";
				if (base.OwnerPivotGrid.Fields.Owner.ShowRowHeaderZone)
				{
					tableCell2.Text = base.OwnerPivotGrid.RowHeaderZoneText;
				}
			}
			this.CallOnItemCreated();
			base.OwnerPivotGrid.Items.Add(this);
			if (this.IsDataBinding)
			{
				this.DataBind();
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x0600841C RID: 33820 RVA: 0x001E2474 File Offset: 0x001E0674
		protected override PivotGridTableCell CreateCellObject()
		{
			if (this.zoneType == PivotGridZoneType.Row)
			{
				this.rowZoneCount++;
				return new PivotGridRowZone(base.OwnerPivotGrid)
				{
					ID = "RowZone" + this.rowZoneCount.ToString()
				};
			}
			if (this.zoneType == PivotGridZoneType.ColumnHeader)
			{
				return new PivotGridColumnHeaderZone(base.OwnerPivotGrid)
				{
					ID = "ColumnHeaderZone"
				};
			}
			return new PivotGridDataZone(base.OwnerPivotGrid)
			{
				ID = "DataZone"
			};
		}

		// Token: 0x0600841D RID: 33821 RVA: 0x001E24FC File Offset: 0x001E06FC
		public PivotGridRowItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding) : base(ownerPivotGrid, itemType, isDataBinding)
		{
		}

		// Token: 0x0600841E RID: 33822 RVA: 0x001E2510 File Offset: 0x001E0710
		public PivotGridRowZone GetRowZone()
		{
			foreach (object obj in this.Cells)
			{
				PivotGridRowZone pivotGridRowZone = obj as PivotGridRowZone;
				if (pivotGridRowZone != null)
				{
					return pivotGridRowZone;
				}
			}
			return null;
		}

		// Token: 0x040024A4 RID: 9380
		private PivotGridZoneType zoneType = PivotGridZoneType.Row;

		// Token: 0x040024A5 RID: 9381
		private int rowZoneCount;
	}
}
