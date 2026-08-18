using System;
using System.Diagnostics;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000009 RID: 9
	[DebuggerDisplay("Count = {Count}")]
	public sealed class ApplicationDomainCollection : ConfigurationElementCollectionBase<ApplicationDomain>
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003619 File Offset: 0x00002619
		internal ApplicationDomainCollection(WorkerProcess parentWorkerProcess)
		{
			this._parentWorkerProcess = parentWorkerProcess;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003628 File Offset: 0x00002628
		protected override ApplicationDomain CreateNewElement(string elementTagName)
		{
			return new ApplicationDomain(this._parentWorkerProcess);
		}

		// Token: 0x0400001B RID: 27
		private WorkerProcess _parentWorkerProcess;
	}
}
