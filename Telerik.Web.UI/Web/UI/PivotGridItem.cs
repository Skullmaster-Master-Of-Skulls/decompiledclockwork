using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200076A RID: 1898
	public class PivotGridItem : PivotGridTableRow, INamingContainer
	{
		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x060042E2 RID: 17122 RVA: 0x000D0BFC File Offset: 0x000CEDFC
		// (set) Token: 0x060042E3 RID: 17123 RVA: 0x000D0C04 File Offset: 0x000CEE04
		public PivotGridItemType ItemType { get; protected set; }

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x060042E4 RID: 17124 RVA: 0x000D0C0D File Offset: 0x000CEE0D
		// (set) Token: 0x060042E5 RID: 17125 RVA: 0x000D0C15 File Offset: 0x000CEE15
		public RadPivotGrid OwnerPivotGrid { get; internal set; }

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x000D0C1E File Offset: 0x000CEE1E
		// (set) Token: 0x060042E7 RID: 17127 RVA: 0x000D0C26 File Offset: 0x000CEE26
		public virtual bool IsDataBinding { get; set; }

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x060042E8 RID: 17128 RVA: 0x000D0C2F File Offset: 0x000CEE2F
		// (set) Token: 0x060042E9 RID: 17129 RVA: 0x000D0C37 File Offset: 0x000CEE37
		internal PivotGridItemDecorator Decorator { get; private set; }

		// Token: 0x060042EA RID: 17130 RVA: 0x000D0C40 File Offset: 0x000CEE40
		public PivotGridItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding)
		{
			this.OwnerPivotGrid = ownerPivotGrid;
			this.ItemType = itemType;
			this.IsDataBinding = isDataBinding;
			this.SetupDecorator();
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x000D0C64 File Offset: 0x000CEE64
		protected virtual void SetupDecorator()
		{
			switch (this.ItemType)
			{
			case PivotGridItemType.Item:
				this.Decorator = new PivotGridItemDecorator(this);
				return;
			case PivotGridItemType.Filter:
				this.Decorator = new PivotGridFilterItemDecorator(this);
				return;
			case PivotGridItemType.PagerItem:
				this.Decorator = new PivotGridPagerItemDecorator(this);
				return;
			case PivotGridItemType.Total:
			case PivotGridItemType.GrandTotal:
				break;
			case PivotGridItemType.Selected:
				this.Decorator = new PivotGridSelectedItemDecorator(this);
				break;
			case PivotGridItemType.RowHeader:
				this.Decorator = new PivotGridRowHeaderItemDecorator(this);
				return;
			case PivotGridItemType.ColumnHeader:
				this.Decorator = new PivotGridColumnHeaderItemDecorator(this);
				return;
			case PivotGridItemType.Aggregate:
				this.Decorator = new PivotGridAggregateItemDecorator(this);
				return;
			case PivotGridItemType.Row:
				this.Decorator = new PivotGridRowItemDecorator(this);
				return;
			case PivotGridItemType.NoRecordsTemplateItem:
				this.Decorator = new PivotGridNoRecordsItemDecorator(this);
				return;
			default:
				return;
			}
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x000D0D1F File Offset: 0x000CEF1F
		internal virtual void Initialize()
		{
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x000D0D21 File Offset: 0x000CEF21
		internal virtual int CalculateCellSpan()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x000D0D28 File Offset: 0x000CEF28
		public virtual void PrepareItemStyle()
		{
			if (this.Decorator != null)
			{
				this.Decorator.DecorateItem(this.OwnerPivotGrid);
			}
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x000D0D43 File Offset: 0x000CEF43
		protected virtual PivotGridTableCell CreateCellObject()
		{
			return new PivotGridTableCell(true);
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x000D0D4C File Offset: 0x000CEF4C
		internal virtual void CallOnCellCreated(PivotGridCell cell)
		{
			PivotGridCellCreatedEventArgs e = new PivotGridCellCreatedEventArgs(cell);
			this.OwnerPivotGrid.FireCellCreated(e);
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x000D0D6C File Offset: 0x000CEF6C
		internal virtual void CallOnCellDataBound(PivotGridCell cell)
		{
			PivotGridCellDataBoundEventArgs e = new PivotGridCellDataBoundEventArgs(cell);
			this.OwnerPivotGrid.FireCellDataBound(e);
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x000D0D8C File Offset: 0x000CEF8C
		protected virtual void CallOnItemCreated()
		{
			PivotGridItemCreatedEventArgs e = new PivotGridItemCreatedEventArgs(this);
			this.OwnerPivotGrid.FireItemCreated(e);
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x000D0DAC File Offset: 0x000CEFAC
		protected virtual void CallOnItemDataBound()
		{
			PivotGridItemDataBoundEventArgs e = new PivotGridItemDataBoundEventArgs(this);
			this.OwnerPivotGrid.FireItemDataBound(e);
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000D0DCC File Offset: 0x000CEFCC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			if (args is CommandEventArgs && !(args is PivotGridCommandEventArgs))
			{
				PivotGridCommandEventArgs args2 = PivotGridCommandEventArgsFactory.CreateCommandEventArgs(this, source, args as CommandEventArgs);
				base.RaiseBubbleEvent(this, args2);
				return true;
			}
			return base.OnBubbleEvent(source, args);
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000D0E0C File Offset: 0x000CF00C
		[SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
		public void FireCommandEvent(string commandName, object commandArgument)
		{
			CommandEventArgs args = new CommandEventArgs(commandName, commandArgument);
			this.OnBubbleEvent(this, args);
		}
	}
}
