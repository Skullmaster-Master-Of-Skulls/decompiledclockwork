using System;
using System.Diagnostics;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200007A RID: 122
	[DebuggerDisplay("Count = {Count}")]
	public sealed class WorkerProcessCollection : ConfigurationElementCollectionBase<WorkerProcess>
	{
		// Token: 0x06000381 RID: 897 RVA: 0x0000925F File Offset: 0x0000825F
		internal WorkerProcessCollection()
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00009267 File Offset: 0x00008267
		protected override WorkerProcess CreateNewElement(string elementTagName)
		{
			return new WorkerProcess();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009270 File Offset: 0x00008270
		public WorkerProcess GetWorkerProcess(int processId)
		{
			foreach (WorkerProcess workerProcess in this)
			{
				if (workerProcess != null && workerProcess.ProcessId == processId)
				{
					return workerProcess;
				}
			}
			return null;
		}
	}
}
