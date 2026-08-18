using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020009A0 RID: 2464
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class WizardClientState
	{
		// Token: 0x17001EF9 RID: 7929
		// (get) Token: 0x06005E03 RID: 24067 RVA: 0x0011F5B4 File Offset: 0x0011D7B4
		// (set) Token: 0x06005E04 RID: 24068 RVA: 0x0011F5BC File Offset: 0x0011D7BC
		public int ActiveIndex
		{
			get
			{
				return this._activeIndex;
			}
			set
			{
				this._activeIndex = value;
			}
		}

		// Token: 0x17001EFA RID: 7930
		// (get) Token: 0x06005E05 RID: 24069 RVA: 0x0011F5C5 File Offset: 0x0011D7C5
		// (set) Token: 0x06005E06 RID: 24070 RVA: 0x0011F5CD File Offset: 0x0011D7CD
		public int ProgressPercent
		{
			get
			{
				return this._progressPercent;
			}
			set
			{
				this._progressPercent = value;
			}
		}

		// Token: 0x17001EFB RID: 7931
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x0011F5D6 File Offset: 0x0011D7D6
		// (set) Token: 0x06005E08 RID: 24072 RVA: 0x0011F5DE File Offset: 0x0011D7DE
		public IList<ClientStateLogEntry> ChangeLog
		{
			get
			{
				return this._changeLog;
			}
			set
			{
				this._changeLog = value;
			}
		}

		// Token: 0x040016AA RID: 5802
		private int _activeIndex;

		// Token: 0x040016AB RID: 5803
		private int _progressPercent;

		// Token: 0x040016AC RID: 5804
		private IList<ClientStateLogEntry> _changeLog;
	}
}
