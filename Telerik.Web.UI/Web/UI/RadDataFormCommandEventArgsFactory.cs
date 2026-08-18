using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020001DB RID: 475
	public static class RadDataFormCommandEventArgsFactory
	{
		// Token: 0x060010FA RID: 4346 RVA: 0x0003E7F0 File Offset: 0x0003C9F0
		public static RadDataFormCommandEventArgs CreateCommandEventArgs(RadDataFormItem item, object commandSource, CommandEventArgs originalArgs)
		{
			string commandName = originalArgs.CommandName;
			if (string.Compare(commandName, "Page", true) == 0)
			{
				return new RadDataFormPageChangedEventArgs(item, commandSource, originalArgs.CommandArgument);
			}
			RadDataForm ownerDataForm = item.OwnerDataForm;
			RadDataFormCommandEventArgs radDataFormCommandEventArgs = new RadDataFormCommandEventArgs(item, commandSource, originalArgs);
			radDataFormCommandEventArgs.Canceled = !ownerDataForm.ValidationSettings.ValidateCommand(radDataFormCommandEventArgs.CommandName);
			return radDataFormCommandEventArgs;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0003E84C File Offset: 0x0003CA4C
		internal static bool HandleCommand(RadDataForm ownerDataForm, object commandSource, CommandEventArgs originalArgs)
		{
			string commandName = originalArgs.CommandName;
			bool result = false;
			if (string.Compare(commandName, "Page", true) == 0)
			{
				RadDataFormPageChangedEventArgs.HandlePaging(ownerDataForm, commandSource, (string)originalArgs.CommandArgument);
				result = true;
			}
			else if (string.Compare(commandName, "InitInsert", true) == 0)
			{
				ownerDataForm.ShowInsertItem();
				result = true;
			}
			else if (string.Compare(commandName, "RebindDataForm", true) == 0)
			{
				ownerDataForm.Rebind();
				result = true;
			}
			return result;
		}
	}
}
