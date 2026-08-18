using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001E1 RID: 481
	public class RadDataFormPageChangedEventArgs : RadDataFormCommandEventArgs
	{
		// Token: 0x0600110F RID: 4367 RVA: 0x0003E98E File Offset: 0x0003CB8E
		public RadDataFormPageChangedEventArgs(RadDataFormItem item, object commandSource, object argument) : base(item, commandSource, "Page", argument)
		{
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0003E99E File Offset: 0x0003CB9E
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x0003E9A6 File Offset: 0x0003CBA6
		public int NewPageIndex { get; internal set; }

		// Token: 0x06001112 RID: 4370 RVA: 0x0003E9B0 File Offset: 0x0003CBB0
		internal static void HandlePaging(RadDataForm ownerDataForm, object commandSource, string argument)
		{
			int num = ownerDataForm.CurrentPageIndex;
			int pageSize = ownerDataForm.PageSize;
			int num2 = -1;
			if (string.Compare(argument, "Next", true) == 0)
			{
				num++;
				if (num > ownerDataForm.PageCount - 1)
				{
					num = ownerDataForm.PageCount - 1;
				}
			}
			else if (string.Compare(argument, "Prev", true) == 0)
			{
				num--;
				if (num < 0)
				{
					return;
				}
			}
			else if (string.Compare(argument, "First", true) == 0)
			{
				num = 0;
			}
			else if (string.Compare(argument, "Last", true) == 0)
			{
				num = ownerDataForm.PageCount - 1;
			}
			else if (int.TryParse(argument, out num2))
			{
				num = num2;
			}
			RadDataFormPageChangedEventArgs.EventState eventState = new RadDataFormPageChangedEventArgs.EventState
			{
				OwnerDataForm = ownerDataForm,
				EventArgs = argument,
				CommandSource = commandSource,
				NewIndex = num
			};
			if (RadDataFormPageChangedEventArgs.CallPageIndexChangedEvent(eventState))
			{
				return;
			}
			ownerDataForm.CurrentPageIndex = eventState.NewIndex;
			if (!ownerDataForm.EnableViewState)
			{
				ownerDataForm.DataSource = null;
			}
			RadDataFormRebindReason rebindReason = RadDataFormRebindReason.PostBackEvent;
			ownerDataForm.ObtainDataSource(rebindReason);
			ownerDataForm.ClearEditItems();
			ownerDataForm.DataBind();
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003EAAC File Offset: 0x0003CCAC
		private static bool CallPageIndexChangedEvent(RadDataFormPageChangedEventArgs.EventState state)
		{
			RadDataFormPageChangedEventArgs radDataFormPageChangedEventArgs = new RadDataFormPageChangedEventArgs(state.Item, state.CommandSource, state.EventArgs)
			{
				NewPageIndex = state.NewIndex
			};
			state.OwnerDataForm.FirePageIndexChanged(radDataFormPageChangedEventArgs);
			state.NewIndex = radDataFormPageChangedEventArgs.NewPageIndex;
			return radDataFormPageChangedEventArgs.Canceled;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003EAFD File Offset: 0x0003CCFD
		public override void ExecuteCommand(object source)
		{
			RadDataFormPageChangedEventArgs.HandlePaging(this.DataFormItem.OwnerDataForm, base.EventSource, (string)base.CommandArgument);
		}

		// Token: 0x020001E2 RID: 482
		private class ArgumentName
		{
			// Token: 0x040004E0 RID: 1248
			public const string Next = "Next";

			// Token: 0x040004E1 RID: 1249
			public const string Prev = "Prev";

			// Token: 0x040004E2 RID: 1250
			public const string First = "First";

			// Token: 0x040004E3 RID: 1251
			public const string Last = "Last";
		}

		// Token: 0x020001E3 RID: 483
		private class EventState
		{
			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001116 RID: 4374 RVA: 0x0003EB28 File Offset: 0x0003CD28
			// (set) Token: 0x06001117 RID: 4375 RVA: 0x0003EB30 File Offset: 0x0003CD30
			public RadDataForm OwnerDataForm { get; set; }

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001118 RID: 4376 RVA: 0x0003EB39 File Offset: 0x0003CD39
			// (set) Token: 0x06001119 RID: 4377 RVA: 0x0003EB41 File Offset: 0x0003CD41
			public string EventArgs { get; set; }

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x0600111A RID: 4378 RVA: 0x0003EB4A File Offset: 0x0003CD4A
			// (set) Token: 0x0600111B RID: 4379 RVA: 0x0003EB52 File Offset: 0x0003CD52
			public object CommandSource { get; set; }

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x0600111C RID: 4380 RVA: 0x0003EB5B File Offset: 0x0003CD5B
			// (set) Token: 0x0600111D RID: 4381 RVA: 0x0003EB63 File Offset: 0x0003CD63
			public int NewIndex { get; set; }

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x0600111E RID: 4382 RVA: 0x0003EB6C File Offset: 0x0003CD6C
			// (set) Token: 0x0600111F RID: 4383 RVA: 0x0003EB74 File Offset: 0x0003CD74
			public RadDataFormItem Item { get; set; }
		}
	}
}
