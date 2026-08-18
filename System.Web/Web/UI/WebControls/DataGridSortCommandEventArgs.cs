using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000546 RID: 1350
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataGridSortCommandEventArgs : EventArgs
	{
		// Token: 0x06004289 RID: 17033 RVA: 0x00113BC1 File Offset: 0x00112BC1
		public DataGridSortCommandEventArgs(object commandSource, DataGridCommandEventArgs dce)
		{
			this.commandSource = commandSource;
			this.sortExpression = (string)dce.CommandArgument;
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x0600428A RID: 17034 RVA: 0x00113BE1 File Offset: 0x00112BE1
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x00113BE9 File Offset: 0x00112BE9
		public string SortExpression
		{
			get
			{
				return this.sortExpression;
			}
		}

		// Token: 0x0400291B RID: 10523
		private string sortExpression;

		// Token: 0x0400291C RID: 10524
		private object commandSource;
	}
}
