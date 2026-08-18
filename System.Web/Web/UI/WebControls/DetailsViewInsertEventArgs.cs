using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000560 RID: 1376
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewInsertEventArgs : CancelEventArgs
	{
		// Token: 0x0600441D RID: 17437 RVA: 0x001198A7 File Offset: 0x001188A7
		public DetailsViewInsertEventArgs(object commandArgument) : base(false)
		{
			this._commandArgument = commandArgument;
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x001198B7 File Offset: 0x001188B7
		public object CommandArgument
		{
			get
			{
				return this._commandArgument;
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x001198BF File Offset: 0x001188BF
		public IOrderedDictionary Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new OrderedDictionary();
				}
				return this._values;
			}
		}

		// Token: 0x0400299A RID: 10650
		private object _commandArgument;

		// Token: 0x0400299B RID: 10651
		private OrderedDictionary _values;
	}
}
