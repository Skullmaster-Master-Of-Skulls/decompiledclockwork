using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000C41 RID: 3137
	public class PivotGridUpdateLayoutEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x060076A2 RID: 30370 RVA: 0x001B8B58 File Offset: 0x001B6D58
		public PivotGridUpdateLayoutEventArgs(PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "UpdateLayout", argument)
		{
		}

		// Token: 0x17002695 RID: 9877
		// (get) Token: 0x060076A3 RID: 30371 RVA: 0x001B8B68 File Offset: 0x001B6D68
		protected virtual RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.Item.OwnerPivotGrid;
			}
		}

		// Token: 0x17002696 RID: 9878
		// (get) Token: 0x060076A4 RID: 30372 RVA: 0x001B8B75 File Offset: 0x001B6D75
		public List<UpdateLayoutCommand> UpdateLayoutCommands
		{
			get
			{
				if (this.updateLayoutCommands == null)
				{
					this.ConstructUpdateLayoutCommands();
				}
				return this.updateLayoutCommands;
			}
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x001B8B8C File Offset: 0x001B6D8C
		public override void ExecuteCommand(object source)
		{
			this.OwnerPivotGrid.FireUpdateLayout(this);
			if (this.Canceled)
			{
				return;
			}
			foreach (UpdateLayoutCommand updateLayoutCommand in this.UpdateLayoutCommands)
			{
				updateLayoutCommand.Execute();
			}
			PivotGridRebindReason rebindReason = PivotGridRebindReason.PostBackEvent;
			this.OwnerPivotGrid.ObtainDataSource(rebindReason, false);
			this.OwnerPivotGrid.DataBind();
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x001B8C10 File Offset: 0x001B6E10
		private void ConstructUpdateLayoutCommands()
		{
			this.updateLayoutCommands = new List<UpdateLayoutCommand>();
			string[] array = base.CommandArgument.ToString().Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					this.updateLayoutCommands.Add(new UpdateLayoutCommand(this.OwnerPivotGrid, text));
				}
			}
		}

		// Token: 0x040020A2 RID: 8354
		private List<UpdateLayoutCommand> updateLayoutCommands;
	}
}
