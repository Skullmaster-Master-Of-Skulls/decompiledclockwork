using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000C40 RID: 3136
	public class UpdateLayoutCommand
	{
		// Token: 0x0600769C RID: 30364 RVA: 0x001B886C File Offset: 0x001B6A6C
		public UpdateLayoutCommand(RadPivotGrid ownerPivotGrid, string command)
		{
			this.ownerPivotGrid = ownerPivotGrid;
			int num = command.IndexOf('(') + 1;
			int num2 = command.LastIndexOf(')');
			string argumentsAsString = command.Substring(num, num2 - num);
			this.FillArgumnetsArray(argumentsAsString);
			string a;
			if ((a = command.Substring(0, num - 1)) != null)
			{
				if (a == "r")
				{
					this.type = UpdateLayoutCommandType.Reorder;
					return;
				}
				if (a == "h")
				{
					this.type = UpdateLayoutCommandType.Hide;
					return;
				}
				if (a == "s")
				{
					this.type = UpdateLayoutCommandType.Sort;
					return;
				}
				if (a == "sh")
				{
					this.type = UpdateLayoutCommandType.Show;
					return;
				}
				if (!(a == "a"))
				{
					return;
				}
				this.type = UpdateLayoutCommandType.AggregateChange;
			}
		}

		// Token: 0x17002692 RID: 9874
		// (get) Token: 0x0600769D RID: 30365 RVA: 0x001B8923 File Offset: 0x001B6B23
		public RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.ownerPivotGrid;
			}
		}

		// Token: 0x17002693 RID: 9875
		// (get) Token: 0x0600769E RID: 30366 RVA: 0x001B892B File Offset: 0x001B6B2B
		public UpdateLayoutCommandType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002694 RID: 9876
		// (get) Token: 0x0600769F RID: 30367 RVA: 0x001B8933 File Offset: 0x001B6B33
		public List<object> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x060076A0 RID: 30368 RVA: 0x001B893C File Offset: 0x001B6B3C
		public void Execute()
		{
			switch (this.Type)
			{
			case UpdateLayoutCommandType.Reorder:
			{
				string text = (string)this.Arguments[0];
				if (this.OwnerPivotGrid.Fields.GetFieldByUniqueName(text) == null)
				{
					this.OwnerPivotGrid.PromissedFieldsForCreation.Add(text);
				}
				this.OwnerPivotGrid.TryReorderField(text, (PivotGridFieldZoneType)this.Arguments[1], (int)this.Arguments[2]);
				return;
			}
			case UpdateLayoutCommandType.Show:
			{
				PivotGridField fieldByUniqueName = this.OwnerPivotGrid.Fields.GetFieldByUniqueName((string)this.Arguments[0]);
				if (fieldByUniqueName == null)
				{
					this.OwnerPivotGrid.PromissedFieldsForCreation.Add((string)this.Arguments[0]);
					return;
				}
				fieldByUniqueName.Show();
				return;
			}
			case UpdateLayoutCommandType.Hide:
			{
				PivotGridField fieldByUniqueName = this.OwnerPivotGrid.Fields.GetFieldByUniqueName((string)this.Arguments[0]);
				if (fieldByUniqueName != null)
				{
					fieldByUniqueName.IsHidden = true;
					return;
				}
				break;
			}
			case UpdateLayoutCommandType.Sort:
			{
				PivotGridField fieldByUniqueName = this.OwnerPivotGrid.Fields.GetFieldByUniqueName((string)this.Arguments[0]);
				if (fieldByUniqueName != null)
				{
					this.OwnerPivotGrid.SortExpressions.ChangeSortOrder((string)this.Arguments[0], this.OwnerPivotGrid.AllowNaturalSort);
					return;
				}
				break;
			}
			case UpdateLayoutCommandType.AggregateChange:
				this.OwnerPivotGrid.AggregatesPosition = (PivotGridAxis)this.Arguments[0];
				this.OwnerPivotGrid.AggregatesLevel = (int)this.Arguments[1];
				break;
			default:
				return;
			}
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x001B8AE0 File Offset: 0x001B6CE0
		private void FillArgumnetsArray(string argumentsAsString)
		{
			this.arguments = new List<object>();
			string[] array = argumentsAsString.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				text.Trim();
				int num;
				if (int.TryParse(text, out num))
				{
					this.arguments.Add(num);
				}
				else
				{
					this.arguments.Add(text);
				}
			}
		}

		// Token: 0x0400209F RID: 8351
		private RadPivotGrid ownerPivotGrid;

		// Token: 0x040020A0 RID: 8352
		private UpdateLayoutCommandType type;

		// Token: 0x040020A1 RID: 8353
		private List<object> arguments;
	}
}
