using System;
using System.Collections;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013C4 RID: 5060
	internal abstract class AbstractTableBody : FObj
	{
		// Token: 0x0600D1A2 RID: 53666 RVA: 0x002E5FC5 File Offset: 0x002E41C5
		public AbstractTableBody(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			if (!(parent is Table))
			{
				ApocDriver.ActiveDriver.FireApocError("A table body must be child of fo:table, not " + parent.GetName());
			}
		}

		// Token: 0x0600D1A3 RID: 53667 RVA: 0x002E5FF1 File Offset: 0x002E41F1
		public void SetColumns(ArrayList columns)
		{
			this.columns = columns;
		}

		// Token: 0x0600D1A4 RID: 53668 RVA: 0x002E5FFA File Offset: 0x002E41FA
		public virtual void SetYPosition(int value)
		{
			this.areaContainer.setYPosition(value);
		}

		// Token: 0x0600D1A5 RID: 53669 RVA: 0x002E6008 File Offset: 0x002E4208
		public virtual int GetYPosition()
		{
			return this.areaContainer.GetCurrentYPosition();
		}

		// Token: 0x0600D1A6 RID: 53670 RVA: 0x002E6015 File Offset: 0x002E4215
		public int GetHeight()
		{
			return this.areaContainer.GetHeight() + this.spaceBefore + this.spaceAfter;
		}

		// Token: 0x0600D1A7 RID: 53671 RVA: 0x002E6030 File Offset: 0x002E4230
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
				this.propMgr.GetRelativePositionProps();
				this.spaceBefore = this.properties.GetProperty("space-before.optimum").GetLength().MValue();
				this.spaceAfter = this.properties.GetProperty("space-after.optimum").GetLength().MValue();
				this.id = this.properties.GetProperty("id").GetString();
				try
				{
					area.getIDReferences().CreateID(this.id);
				}
				catch (ApocException ex)
				{
					throw ex;
				}
				if (flag)
				{
					area.end();
				}
				if (this.rowSpanMgr == null)
				{
					this.rowSpanMgr = new RowSpanMgr(this.columns.Count);
				}
				this.marker = 0;
			}
			if (this.spaceBefore != 0 && this.marker == 0)
			{
				area.increaseHeight(this.spaceBefore);
			}
			if (this.marker == 0)
			{
				area.getIDReferences().ConfigureID(this.id, area);
			}
			int num = area.spaceLeft();
			this.areaContainer = new AreaContainer(this.propMgr.GetFontState(area.getFontInfo()), 0, area.getContentHeight(), area.getContentWidth(), area.spaceLeft(), 61);
			this.areaContainer.foCreator = this;
			this.areaContainer.setPage(area.getPage());
			this.areaContainer.setParent(area);
			this.areaContainer.setBackground(this.propMgr.GetBackgroundProps());
			this.areaContainer.setBorderAndPadding(this.propMgr.GetBorderAndPadding());
			this.areaContainer.start();
			this.areaContainer.setAbsoluteHeight(area.getAbsoluteHeight());
			this.areaContainer.setIDReferences(area.getIDReferences());
			Hashtable hashtable = new Hashtable();
			int count = this.children.Count;
			TableRow tableRow = null;
			bool flag2 = true;
			for (int i = this.marker; i < count; i++)
			{
				object obj = this.children[i];
				Marker marker = obj as Marker;
				if (marker != null)
				{
					marker.Layout(area);
				}
				else
				{
					TableRow tableRow2 = obj as TableRow;
					if (tableRow2 == null)
					{
						throw new ApocException("Currently only Table Rows are supported in table body, header and footer");
					}
					tableRow2.SetRowSpanMgr(this.rowSpanMgr);
					tableRow2.SetColumns(this.columns);
					tableRow2.DoSetup(this.areaContainer);
					if ((tableRow2.GetKeepWithPrevious().GetKeepType() != "KEEP_WITH_AUTO" || tableRow2.GetKeepWithNext().GetKeepType() != "KEEP_WITH_AUTO" || tableRow2.GetKeepTogether().GetKeepType() != "KEEP_WITH_AUTO") && tableRow != null && !hashtable.Contains(tableRow))
					{
						hashtable.Add(tableRow, null);
					}
					else
					{
						if (flag2 && hashtable.Count > 0)
						{
							hashtable = new Hashtable();
						}
						if (flag2 && i > this.marker)
						{
							this.rowSpanMgr.SetIgnoreKeeps(false);
						}
					}
					bool flag3 = i == this.marker;
					if (!flag3 && hashtable.Count > 0 && this.children.IndexOf(hashtable[0]) == this.marker)
					{
						flag3 = true;
					}
					tableRow2.setIgnoreKeepTogether(flag3 && this.startsAC(area));
					Status result = tableRow2.Layout(this.areaContainer);
					if (result.isIncomplete())
					{
						if (result.isPageBreak())
						{
							this.marker = i;
							area.addChild(this.areaContainer);
							area.increaseHeight(this.areaContainer.GetHeight());
							if (i == count - 1)
							{
								this.marker = -1001;
								if (this.spaceAfter != 0)
								{
									area.increaseHeight(this.spaceAfter);
								}
							}
							return result;
						}
						if (hashtable.Count > 0 && !this.rowSpanMgr.IgnoreKeeps())
						{
							tableRow2.RemoveLayout(this.areaContainer);
							foreach (object obj2 in hashtable.Keys)
							{
								TableRow tableRow3 = (TableRow)obj2;
								tableRow3.RemoveLayout(this.areaContainer);
								i--;
							}
							if (i == 0)
							{
								this.ResetMarker();
								this.rowSpanMgr.SetIgnoreKeeps(true);
								return new Status(2);
							}
						}
						this.marker = i;
						if (i != 0 && result.getCode() == 2)
						{
							result = new Status(3);
						}
						if (i != 0 || this.areaContainer.getContentHeight() > 0)
						{
							area.addChild(this.areaContainer);
							area.increaseHeight(this.areaContainer.GetHeight());
						}
						this.rowSpanMgr.SetIgnoreKeeps(true);
						return result;
					}
					else
					{
						if (result.getCode() == 8 || this.rowSpanMgr.HasUnfinishedSpans())
						{
							hashtable.Add(tableRow2, null);
							flag2 = false;
						}
						else
						{
							flag2 = true;
						}
						tableRow = tableRow2;
						area.setMaxHeight(area.getMaxHeight() - num + this.areaContainer.getMaxHeight());
						num = area.spaceLeft();
					}
				}
			}
			area.addChild(this.areaContainer);
			this.areaContainer.end();
			area.increaseHeight(this.areaContainer.GetHeight());
			if (this.spaceAfter != 0)
			{
				area.increaseHeight(this.spaceAfter);
				area.setMaxHeight(area.getMaxHeight() - this.spaceAfter);
			}
			if (flag)
			{
				area.start();
			}
			return new Status(1);
		}

		// Token: 0x0600D1A8 RID: 53672 RVA: 0x002E65DC File Offset: 0x002E47DC
		internal void RemoveLayout(Area area)
		{
			if (this.areaContainer != null)
			{
				area.removeChild(this.areaContainer);
			}
			if (this.spaceBefore != 0)
			{
				area.increaseHeight(-this.spaceBefore);
			}
			if (this.spaceAfter != 0)
			{
				area.increaseHeight(-this.spaceAfter);
			}
			this.ResetMarker();
			this.RemoveID(area.getIDReferences());
		}

		// Token: 0x0600D1A9 RID: 53673 RVA: 0x002E663C File Offset: 0x002E483C
		private bool startsAC(Area area)
		{
			Area parent;
			while ((parent = area.getParent()) != null && !parent.hasNonSpaceChildren())
			{
				AreaContainer areaContainer = parent as AreaContainer;
				if (areaContainer != null && areaContainer.getPosition() == 1)
				{
					return true;
				}
				area = parent;
			}
			return false;
		}

		// Token: 0x04003851 RID: 14417
		protected int spaceBefore;

		// Token: 0x04003852 RID: 14418
		protected int spaceAfter;

		// Token: 0x04003853 RID: 14419
		protected string id;

		// Token: 0x04003854 RID: 14420
		protected ArrayList columns;

		// Token: 0x04003855 RID: 14421
		protected RowSpanMgr rowSpanMgr;

		// Token: 0x04003856 RID: 14422
		protected AreaContainer areaContainer;
	}
}
