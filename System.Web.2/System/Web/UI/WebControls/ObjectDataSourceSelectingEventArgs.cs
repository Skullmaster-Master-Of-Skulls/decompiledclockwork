using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200048E RID: 1166
	public class ObjectDataSourceSelectingEventArgs : ObjectDataSourceMethodEventArgs
	{
		// Token: 0x060039B6 RID: 14774 RVA: 0x000BAE65 File Offset: 0x000B9065
		public ObjectDataSourceSelectingEventArgs(IOrderedDictionary inputParameters, DataSourceSelectArguments arguments, bool executingSelectCount) : base(inputParameters)
		{
			this._arguments = arguments;
			this._executingSelectCount = executingSelectCount;
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x060039B7 RID: 14775 RVA: 0x000BAE7C File Offset: 0x000B907C
		public DataSourceSelectArguments Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000BAE84 File Offset: 0x000B9084
		public bool ExecutingSelectCount
		{
			get
			{
				return this._executingSelectCount;
			}
		}

		// Token: 0x040022BE RID: 8894
		private DataSourceSelectArguments _arguments;

		// Token: 0x040022BF RID: 8895
		private bool _executingSelectCount;
	}
}
