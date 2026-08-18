using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200136A RID: 4970
	public class AjaxRequestEventArgs : EventArgs
	{
		// Token: 0x0600CF92 RID: 53138 RVA: 0x002E0CCC File Offset: 0x002DEECC
		public AjaxRequestEventArgs(string argument)
		{
			this.argument = argument;
		}

		// Token: 0x170042B8 RID: 17080
		// (get) Token: 0x0600CF93 RID: 53139 RVA: 0x002E0CDB File Offset: 0x002DEEDB
		public string Argument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x040037A5 RID: 14245
		private string argument;
	}
}
