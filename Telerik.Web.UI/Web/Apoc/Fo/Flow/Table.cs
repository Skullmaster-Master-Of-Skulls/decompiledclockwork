using System;
using System.Collections;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x02001403 RID: 5123
	internal class Table : FObj
	{
		// Token: 0x0600D26E RID: 53870 RVA: 0x002EA57F File Offset: 0x002E877F
		public new static FObj.Maker GetMaker()
		{
			return new Table.Maker();
		}

		// Token: 0x0600D26F RID: 53871 RVA: 0x002EA586 File Offset: 0x002E8786
		public Table(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:table";
		}

		// Token: 0x0600D270 RID: 53872 RVA: 0x002EA5A8 File Offset: 0x002E87A8
		public override Status Layout(Area area)
		{
			if (this.marker == -1001)
			{
				return new Status(1);
			}
			bool flag = area is BlockArea;
			if (this.marker == -1000)
			{
				this.propMgr.GetAccessibilityProps();
				this.propMgr.GetAuralProps();
				this.propMgr.GetBorderAndPadding();
				this.propMgr.GetBackgroundProps();
				this.propMgr.GetMarginProps();
				this.propMgr.GetRelativePositionProps();
				this.breakBefore = this.properties.GetProperty("break-before").GetEnum();
				this.breakAfter = this.properties.GetProperty("break-after").GetEnum();
				this.spaceBefore = this.properties.GetProperty("space-before.optimum").GetLength().MValue();
				this.spaceAfter = this.properties.GetProperty("space-after.optimum").GetLength().MValue();
				this.ipd = this.properties.GetProperty("inline-progression-dimension").GetLengthRange();
				this.height = this.properties.GetProperty("height").GetLength().MValue();
				this.bAutoLayout = (this.properties.GetProperty("table-layout").GetEnum() == 7);
				this.id = this.properties.GetProperty("id").GetString();
				this.omitHeaderAtBreak = (this.properties.GetProperty("table-omit-header-at-break").GetEnum() == 81);
				this.omitFooterAtBreak = (this.properties.GetProperty("table-omit-footer-at-break").GetEnum() == 81);
				if (flag)
				{
					area.end();
				}
				if (this.areaContainer == null)
				{
					area.getIDReferences().CreateID(this.id);
				}
				this.marker = 0;
				if (this.breakBefore == 58)
				{
					return new Status(4);
				}
				if (this.breakBefore == 55)
				{
					return new Status(6);
				}
				if (this.breakBefore == 26)
				{
					return new Status(5);
				}
			}
			if (this.spaceBefore != 0 && this.marker == 0)
			{
				area.addDisplaySpace(this.spaceBefore);
			}
			if (this.marker == 0 && this.areaContainer == null)
			{
				area.getIDReferences().ConfigureID(this.id, area);
			}
			int num = area.spaceLeft();
			this.areaContainer = new AreaContainer(this.propMgr.GetFontState(area.getFontInfo()), 0, 0, area.getAllocationWidth(), area.spaceLeft(), 73);
			this.areaContainer.foCreator = this;
			this.areaContainer.setPage(area.getPage());
			this.areaContainer.setParent(area);
			this.areaContainer.setBackground(this.propMgr.GetBackgroundProps());
			this.areaContainer.setBorderAndPadding(this.propMgr.GetBorderAndPadding());
			this.areaContainer.start();
			this.areaContainer.setAbsoluteHeight(area.getAbsoluteHeight());
			this.areaContainer.setIDReferences(area.getIDReferences());
			bool flag2 = false;
			bool flag3 = false;
			int count = this.children.Count;
			if (this.columns.Count == 0)
			{
				this.FindColumns(this.areaContainer);
				if (this.bAutoLayout)
				{
					ApocDriver.ActiveDriver.FireApocWarning("table-layout=auto is not supported, using fixed!");
				}
				this.contentWidth = this.CalcFixedColumnWidths(this.areaContainer.getAllocationWidth());
			}
			this.areaContainer.setAllocationWidth(this.contentWidth);
			this.layoutColumns(this.areaContainer);
			for (int i = this.marker; i < count; i++)
			{
				FONode fonode = (FONode)this.children[i];
				TableHeader tableHeader = fonode as TableHeader;
				TableFooter tableFooter = fonode as TableFooter;
				TableBody tableBody = fonode as TableBody;
				if (tableHeader != null)
				{
					if (this.columns.Count == 0)
					{
						ApocDriver.ActiveDriver.FireApocWarning("Current implementation of tables requires a table-column for each column, indicating column-width");
						return new Status(1);
					}
					this.tableHeader = tableHeader;
					this.tableHeader.SetColumns(this.columns);
				}
				else if (tableFooter != null)
				{
					if (this.columns.Count == 0)
					{
						ApocDriver.ActiveDriver.FireApocWarning("Current implementation of tables requires a table-column for each column, indicating column-width");
						return new Status(1);
					}
					this.tableFooter = tableFooter;
					this.tableFooter.SetColumns(this.columns);
				}
				else if (tableBody != null)
				{
					if (this.columns.Count == 0)
					{
						ApocDriver.ActiveDriver.FireApocWarning("Current implementation of tables requires a table-column for each column, indicating column-width");
						return new Status(1);
					}
					if (this.tableHeader != null && !flag2)
					{
						if (this.tableHeader.Layout(this.areaContainer).isIncomplete())
						{
							this.tableHeader.ResetMarker();
							return new Status(2);
						}
						flag2 = true;
						this.tableHeader.ResetMarker();
						area.setMaxHeight(area.getMaxHeight() - num + this.areaContainer.getMaxHeight());
					}
					if (this.tableFooter != null && !this.omitFooterAtBreak && !flag3)
					{
						if (this.tableFooter.Layout(this.areaContainer).isIncomplete())
						{
							return new Status(2);
						}
						flag3 = true;
						this.tableFooter.ResetMarker();
					}
					fonode.SetWidows(this.widows);
					fonode.SetOrphans(this.orphans);
					tableBody.SetColumns(this.columns);
					Status status;
					Status result = status = fonode.Layout(this.areaContainer);
					if (status.isIncomplete())
					{
						this.marker = i;
						if (this.bodyCount == 0 && result.getCode() == 2)
						{
							if (this.tableHeader != null)
							{
								this.tableHeader.RemoveLayout(this.areaContainer);
							}
							if (this.tableFooter != null)
							{
								this.tableFooter.RemoveLayout(this.areaContainer);
							}
							this.ResetMarker();
						}
						if (this.areaContainer.getContentHeight() > 0)
						{
							area.addChild(this.areaContainer);
							area.increaseHeight(this.areaContainer.GetHeight());
							if (this.omitHeaderAtBreak)
							{
								this.tableHeader = null;
							}
							if (this.tableFooter != null && !this.omitFooterAtBreak)
							{
								tableBody.SetYPosition(this.tableFooter.GetYPosition());
								this.tableFooter.SetYPosition(this.tableFooter.GetYPosition() + tableBody.GetHeight());
							}
							this.SetupColumnHeights();
							result = new Status(3);
						}
						return result;
					}
					this.bodyCount++;
					area.setMaxHeight(area.getMaxHeight() - num + this.areaContainer.getMaxHeight());
					if (this.tableFooter != null && !this.omitFooterAtBreak)
					{
						tableBody.SetYPosition(this.tableFooter.GetYPosition());
						this.tableFooter.SetYPosition(this.tableFooter.GetYPosition() + tableBody.GetHeight());
					}
				}
			}
			if (this.tableFooter != null && this.omitFooterAtBreak && this.tableFooter.Layout(this.areaContainer).isIncomplete())
			{
				ApocDriver.ActiveDriver.FireApocWarning("Footer could not fit on page, moving last body row to next page");
				area.addChild(this.areaContainer);
				area.increaseHeight(this.areaContainer.GetHeight());
				if (this.omitHeaderAtBreak)
				{
					this.tableHeader = null;
				}
				this.tableFooter.RemoveLayout(this.areaContainer);
				this.tableFooter.ResetMarker();
				return new Status(3);
			}
			if (this.height != 0)
			{
				this.areaContainer.SetHeight(this.height);
			}
			this.SetupColumnHeights();
			this.areaContainer.end();
			area.addChild(this.areaContainer);
			area.increaseHeight(this.areaContainer.GetHeight());
			if (this.spaceAfter != 0)
			{
				area.addDisplaySpace(this.spaceAfter);
			}
			if (flag)
			{
				area.start();
			}
			if (this.breakAfter == 58)
			{
				this.marker = -1001;
				return new Status(4);
			}
			if (this.breakAfter == 55)
			{
				this.marker = -1001;
				return new Status(6);
			}
			if (this.breakAfter == 26)
			{
				this.marker = -1001;
				return new Status(5);
			}
			return new Status(1);
		}

		// Token: 0x0600D271 RID: 53873 RVA: 0x002EAD94 File Offset: 0x002E8F94
		protected void SetupColumnHeights()
		{
			foreach (object obj in this.columns)
			{
				TableColumn tableColumn = (TableColumn)obj;
				if (tableColumn != null)
				{
					tableColumn.SetHeight(this.areaContainer.getContentHeight());
				}
			}
		}

		// Token: 0x0600D272 RID: 53874 RVA: 0x002EADFC File Offset: 0x002E8FFC
		private void FindColumns(Area areaContainer)
		{
			int num = 1;
			foreach (object obj in this.children)
			{
				FONode fonode = (FONode)obj;
				TableColumn tableColumn = fonode as TableColumn;
				if (tableColumn != null)
				{
					tableColumn.DoSetup(areaContainer);
					int numColumnsRepeated = tableColumn.GetNumColumnsRepeated();
					int num2 = tableColumn.GetColumnNumber();
					if (num2 == 0)
					{
						num2 = num;
					}
					for (int i = 0; i < numColumnsRepeated; i++)
					{
						if (num2 < this.columns.Count && this.columns[num2 - 1] != null)
						{
							ApocDriver.ActiveDriver.FireApocWarning("More than one column object assigned to column " + num2);
						}
						this.columns.Insert(num2 - 1, tableColumn);
						num2++;
					}
					num = num2;
				}
			}
		}

		// Token: 0x0600D273 RID: 53875 RVA: 0x002EAEEC File Offset: 0x002E90EC
		private int CalcFixedColumnWidths(int maxAllocationWidth)
		{
			int num = 1;
			int num2 = 0;
			double num3 = 0.0;
			int num4 = 0;
			double num5 = 0.0;
			double num6 = 0.0;
			double num7 = 100000.0;
			foreach (object obj in this.columns)
			{
				TableColumn tableColumn = (TableColumn)obj;
				if (tableColumn == null)
				{
					ApocDriver.ActiveDriver.FireApocWarning("No table-column specification for column " + num);
					num2++;
				}
				else
				{
					Length columnWidthAsLength = tableColumn.GetColumnWidthAsLength();
					double tableUnits = columnWidthAsLength.GetTableUnits();
					if (tableUnits > 0.0 && tableUnits < num7 && columnWidthAsLength.MValue() == 0)
					{
						num7 = tableUnits;
					}
					num3 += tableUnits;
					num4 += columnWidthAsLength.MValue();
				}
				num++;
			}
			this.SetIPD(num3 > 0.0, maxAllocationWidth);
			if (num3 > 0.0)
			{
				int num8;
				if (this.optIPD > num4)
				{
					num8 = this.optIPD - num4;
				}
				else if (this.maxIPD > num4)
				{
					num8 = this.maxIPD - num4;
				}
				else
				{
					num8 = maxAllocationWidth - num4;
				}
				if (num8 > 0)
				{
					num6 = (double)num8 / num3;
				}
				else
				{
					ApocDriver.ActiveDriver.FireApocWarning(string.Format("Sum of fixed column widths {0} greater than maximum available IPD {1}; no space for {2} propertional units", num4, maxAllocationWidth, num3));
					num6 = 10000.0 / num7;
				}
			}
			else if (this.minIPD > num4)
			{
				num5 = (double)this.minIPD / (double)num4;
			}
			else if (this.maxIPD < num4)
			{
				if (this.maxIPD != 0)
				{
					ApocDriver.ActiveDriver.FireApocWarning(string.Concat(new object[]
					{
						"Sum of fixed column widths ",
						num4,
						" greater than maximum specified IPD ",
						this.maxIPD
					}));
				}
			}
			else if (this.optIPD != -1 && num4 != this.optIPD)
			{
				ApocDriver.ActiveDriver.FireApocWarning(string.Concat(new object[]
				{
					"Sum of fixed column widths ",
					num4,
					" differs from specified optimum IPD ",
					this.optIPD
				}));
			}
			int num9 = 0;
			foreach (object obj2 in this.columns)
			{
				TableColumn tableColumn2 = (TableColumn)obj2;
				if (tableColumn2 != null)
				{
					tableColumn2.SetColumnOffset(num9);
					Length columnWidthAsLength2 = tableColumn2.GetColumnWidthAsLength();
					if (num6 > 0.0)
					{
						columnWidthAsLength2.ResolveTableUnit(num6);
					}
					int num10 = columnWidthAsLength2.MValue();
					if (num10 <= 0)
					{
						ApocDriver.ActiveDriver.FireApocWarning("Zero-width table column!");
					}
					if (num5 > 0.0)
					{
						num10 = (int)((double)num10 * num5);
					}
					tableColumn2.SetColumnWidth(num10);
					num9 += num10;
				}
			}
			return num9;
		}

		// Token: 0x0600D274 RID: 53876 RVA: 0x002EB208 File Offset: 0x002E9408
		private void layoutColumns(Area tableArea)
		{
			foreach (object obj in this.columns)
			{
				TableColumn tableColumn = (TableColumn)obj;
				if (tableColumn != null)
				{
					tableColumn.Layout(tableArea);
				}
			}
		}

		// Token: 0x0600D275 RID: 53877 RVA: 0x002EB268 File Offset: 0x002E9468
		public int GetAreaHeight()
		{
			return this.areaContainer.GetHeight();
		}

		// Token: 0x0600D276 RID: 53878 RVA: 0x002EB275 File Offset: 0x002E9475
		public override int GetContentWidth()
		{
			if (this.areaContainer != null)
			{
				return this.areaContainer.getContentWidth();
			}
			return 0;
		}

		// Token: 0x0600D277 RID: 53879 RVA: 0x002EB28C File Offset: 0x002E948C
		private void SetIPD(bool bHasProportionalUnits, int maxAllocIPD)
		{
			bool flag = !this.ipd.GetMaximum().GetLength().IsAuto();
			if (flag)
			{
				this.maxIPD = this.ipd.GetMaximum().GetLength().MValue();
			}
			else
			{
				this.maxIPD = maxAllocIPD;
			}
			if (this.ipd.GetOptimum().GetLength().IsAuto())
			{
				this.optIPD = -1;
			}
			else
			{
				this.optIPD = this.ipd.GetMaximum().GetLength().MValue();
			}
			if (this.ipd.GetMinimum().GetLength().IsAuto())
			{
				this.minIPD = -1;
			}
			else
			{
				this.minIPD = this.ipd.GetMinimum().GetLength().MValue();
			}
			if (bHasProportionalUnits && this.optIPD < 0)
			{
				if (this.minIPD > 0)
				{
					if (flag)
					{
						this.optIPD = (this.minIPD + this.maxIPD) / 2;
						return;
					}
					this.optIPD = this.minIPD;
					return;
				}
				else
				{
					if (flag)
					{
						this.optIPD = this.maxIPD;
						return;
					}
					ApocDriver.ActiveDriver.FireApocError("At least one of minimum, optimum, or maximum IPD must be specified on table.");
					this.optIPD = this.maxIPD;
				}
			}
		}

		// Token: 0x040038C7 RID: 14535
		private const int MINCOLWIDTH = 10000;

		// Token: 0x040038C8 RID: 14536
		private int breakBefore;

		// Token: 0x040038C9 RID: 14537
		private int breakAfter;

		// Token: 0x040038CA RID: 14538
		private int spaceBefore;

		// Token: 0x040038CB RID: 14539
		private int spaceAfter;

		// Token: 0x040038CC RID: 14540
		private LengthRange ipd;

		// Token: 0x040038CD RID: 14541
		private int height;

		// Token: 0x040038CE RID: 14542
		private string id;

		// Token: 0x040038CF RID: 14543
		private TableHeader tableHeader;

		// Token: 0x040038D0 RID: 14544
		private TableFooter tableFooter;

		// Token: 0x040038D1 RID: 14545
		private bool omitHeaderAtBreak;

		// Token: 0x040038D2 RID: 14546
		private bool omitFooterAtBreak;

		// Token: 0x040038D3 RID: 14547
		private ArrayList columns = new ArrayList();

		// Token: 0x040038D4 RID: 14548
		private int bodyCount;

		// Token: 0x040038D5 RID: 14549
		private bool bAutoLayout;

		// Token: 0x040038D6 RID: 14550
		private int contentWidth;

		// Token: 0x040038D7 RID: 14551
		private int optIPD;

		// Token: 0x040038D8 RID: 14552
		private int minIPD;

		// Token: 0x040038D9 RID: 14553
		private int maxIPD;

		// Token: 0x040038DA RID: 14554
		private AreaContainer areaContainer;

		// Token: 0x02001404 RID: 5124
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D278 RID: 53880 RVA: 0x002EB3B5 File Offset: 0x002E95B5
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Table(parent, propertyList);
			}
		}
	}
}
