using System;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005FB RID: 1531
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ObjectDataSourceSelectingEventArgs : ObjectDataSourceMethodEventArgs
	{
		// Token: 0x06004BA9 RID: 19369 RVA: 0x00133821 File Offset: 0x00132821
		public ObjectDataSourceSelectingEventArgs(IOrderedDictionary inputParameters, DataSourceSelectArguments arguments, bool executingSelectCount) : base(inputParameters)
		{
			this._arguments = arguments;
			this._executingSelectCount = executingSelectCount;
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06004BAA RID: 19370 RVA: 0x00133838 File Offset: 0x00132838
		public DataSourceSelectArguments Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06004BAB RID: 19371 RVA: 0x00133840 File Offset: 0x00132840
		public bool ExecutingSelectCount
		{
			get
			{
				return this._executingSelectCount;
			}
		}

		// Token: 0x04002BAF RID: 11183
		private DataSourceSelectArguments _arguments;

		// Token: 0x04002BB0 RID: 11184
		private bool _executingSelectCount;
	}
}
