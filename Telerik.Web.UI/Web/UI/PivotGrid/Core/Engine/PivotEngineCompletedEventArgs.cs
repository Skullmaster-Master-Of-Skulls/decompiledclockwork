using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x02000D45 RID: 3397
	internal class PivotEngineCompletedEventArgs : EventArgs
	{
		// Token: 0x06007E77 RID: 32375 RVA: 0x001CF94C File Offset: 0x001CDB4C
		internal PivotEngineCompletedEventArgs(ReadOnlyCollection<Exception> innerExceptions, PivotEngineStatus status)
		{
			this.InnerExceptions = innerExceptions;
			this.Status = status;
			if (this.InnerExceptions == null)
			{
				this.InnerExceptions = new ReadOnlyCollection<Exception>(new List<Exception>());
			}
		}

		// Token: 0x1700284E RID: 10318
		// (get) Token: 0x06007E78 RID: 32376 RVA: 0x001CF97A File Offset: 0x001CDB7A
		// (set) Token: 0x06007E79 RID: 32377 RVA: 0x001CF982 File Offset: 0x001CDB82
		public PivotEngineStatus Status { get; private set; }

		// Token: 0x1700284F RID: 10319
		// (get) Token: 0x06007E7A RID: 32378 RVA: 0x001CF98B File Offset: 0x001CDB8B
		// (set) Token: 0x06007E7B RID: 32379 RVA: 0x001CF993 File Offset: 0x001CDB93
		public ReadOnlyCollection<Exception> InnerExceptions { get; private set; }
	}
}
