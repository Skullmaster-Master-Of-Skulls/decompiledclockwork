using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001B58 RID: 7000
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ToolBarClientState
	{
		// Token: 0x170052C4 RID: 21188
		// (get) Token: 0x06010F47 RID: 69447 RVA: 0x003C0F68 File Offset: 0x003BF168
		// (set) Token: 0x06010F48 RID: 69448 RVA: 0x003C0F70 File Offset: 0x003BF170
		public ClientStateLogEntry[] LogEntries
		{
			get
			{
				return this._logEntries;
			}
			set
			{
				this._logEntries = value;
			}
		}

		// Token: 0x04004BE0 RID: 19424
		private ClientStateLogEntry[] _logEntries;
	}
}
