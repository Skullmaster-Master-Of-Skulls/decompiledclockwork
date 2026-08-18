using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020009AB RID: 2475
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class AutoCompleteBoxClientState
	{
		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x06005EF9 RID: 24313 RVA: 0x00121DFA File Offset: 0x0011FFFA
		// (set) Token: 0x06005EFA RID: 24314 RVA: 0x00121E02 File Offset: 0x00120002
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

		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x06005EFB RID: 24315 RVA: 0x00121E0B File Offset: 0x0012000B
		// (set) Token: 0x06005EFC RID: 24316 RVA: 0x00121E13 File Offset: 0x00120013
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}

		// Token: 0x040016D6 RID: 5846
		private ClientStateLogEntry[] _logEntries;

		// Token: 0x040016D7 RID: 5847
		private bool _enabled;
	}
}
