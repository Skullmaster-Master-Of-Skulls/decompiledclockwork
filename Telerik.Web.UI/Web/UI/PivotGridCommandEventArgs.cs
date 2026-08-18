using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C2F RID: 3119
	public class PivotGridCommandEventArgs : CommandEventArgs, IPivotGridCommandEvent
	{
		// Token: 0x17002677 RID: 9847
		// (get) Token: 0x06007650 RID: 30288 RVA: 0x001B772B File Offset: 0x001B592B
		// (set) Token: 0x06007651 RID: 30289 RVA: 0x001B7733 File Offset: 0x001B5933
		public virtual PivotGridItem Item { get; set; }

		// Token: 0x17002678 RID: 9848
		// (get) Token: 0x06007652 RID: 30290 RVA: 0x001B773C File Offset: 0x001B593C
		// (set) Token: 0x06007653 RID: 30291 RVA: 0x001B7744 File Offset: 0x001B5944
		public object EventSource { get; set; }

		// Token: 0x06007654 RID: 30292 RVA: 0x001B774D File Offset: 0x001B594D
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public PivotGridCommandEventArgs(PivotGridItem pivotGridItem, object eventSource, CommandEventArgs args) : base(args)
		{
			this.Item = pivotGridItem;
			this.EventSource = eventSource;
		}

		// Token: 0x06007655 RID: 30293 RVA: 0x001B7764 File Offset: 0x001B5964
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal PivotGridCommandEventArgs(PivotGridItem pivotGridItem, object eventSource, string name, object argument) : base(name, argument)
		{
			this.Item = pivotGridItem;
			this.EventSource = eventSource;
		}

		// Token: 0x06007656 RID: 30294 RVA: 0x001B777D File Offset: 0x001B597D
		public PivotGridCommandEventArgs(CommandEventArgs eventArgs) : base(eventArgs)
		{
		}

		// Token: 0x06007657 RID: 30295 RVA: 0x001B7786 File Offset: 0x001B5986
		public PivotGridCommandEventArgs(PivotGridCommandEventArgs eventArgs) : base(eventArgs)
		{
		}

		// Token: 0x06007658 RID: 30296 RVA: 0x001B778F File Offset: 0x001B598F
		public PivotGridCommandEventArgs(string name, object argument) : base(name, argument)
		{
		}

		// Token: 0x17002679 RID: 9849
		// (get) Token: 0x06007659 RID: 30297 RVA: 0x001B7799 File Offset: 0x001B5999
		// (set) Token: 0x0600765A RID: 30298 RVA: 0x001B77A1 File Offset: 0x001B59A1
		public virtual bool Canceled { get; set; }

		// Token: 0x0600765B RID: 30299 RVA: 0x001B77AC File Offset: 0x001B59AC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public virtual void ExecuteCommand(object source)
		{
			string commandName = base.CommandName;
			RadPivotGrid ownerPivotGrid = this.Item.OwnerPivotGrid;
			if (string.Compare(commandName, "RebindPivotGrid", true, CultureInfo.InvariantCulture) == 0)
			{
				ownerPivotGrid.Rebind();
			}
		}
	}
}
