using System;
using System.Diagnostics;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000065 RID: 101
	[DebuggerDisplay("Count = {Count}")]
	public sealed class RequestCollection : ConfigurationElementCollectionBase<Request>
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000736A File Offset: 0x0000636A
		internal RequestCollection(int processId)
		{
			this._processId = processId;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00007379 File Offset: 0x00006379
		protected override Request CreateNewElement(string elementTagName)
		{
			return new Request(this._processId);
		}

		// Token: 0x040000FB RID: 251
		private int _processId;
	}
}
