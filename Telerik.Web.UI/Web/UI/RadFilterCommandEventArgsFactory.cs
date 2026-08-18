using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020018A2 RID: 6306
	public static class RadFilterCommandEventArgsFactory
	{
		// Token: 0x0600F3E6 RID: 62438 RVA: 0x00377884 File Offset: 0x00375A84
		public static RadFilterCommandEventArgs CreateCommandEventArgs(RadFilterExpressionItem item, object commandSource, CommandEventArgs originalArgs)
		{
			string commandName = originalArgs.CommandName;
			return new RadFilterCommandEventArgs(item, commandSource, commandName, originalArgs.CommandArgument);
		}

		// Token: 0x0600F3E7 RID: 62439 RVA: 0x003778A8 File Offset: 0x00375AA8
		internal static bool ShouldHandleCommandInternal(CommandEventArgs args)
		{
			return string.Compare(args.CommandName, "ApplyExpressions", true) == 0;
		}

		// Token: 0x0600F3E8 RID: 62440 RVA: 0x003778C0 File Offset: 0x00375AC0
		internal static void HandleCommand(RadFilter ownerFilter, object commandSource, RadFilterCommandEventArgs args)
		{
			if (string.Compare(args.CommandName, "ApplyExpressions", true) == 0)
			{
				ownerFilter.HandleApplyCommand();
			}
		}
	}
}
