using System;
using System.Diagnostics;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000079 RID: 121
	[DebuggerDisplay("ProcessId = {ProcessId}")]
	public sealed class WorkerProcess : ConfigurationElement
	{
		// Token: 0x0600037A RID: 890 RVA: 0x00009174 File Offset: 0x00008174
		internal WorkerProcess()
		{
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000917C File Offset: 0x0000817C
		public ApplicationDomainCollection ApplicationDomains
		{
			get
			{
				this._applicationDomains = new ApplicationDomainCollection(this);
				this._applicationDomains.Initialize(base.Configuration, base.AppHostElement.GetElementByName("appDomains"));
				return this._applicationDomains;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000091B1 File Offset: 0x000081B1
		public string AppPoolName
		{
			get
			{
				return (string)base.GetAttributeValue("AppPoolName");
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000091C3 File Offset: 0x000081C3
		public string ProcessGuid
		{
			get
			{
				return (string)base.GetAttributeValue("guid");
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000091D5 File Offset: 0x000081D5
		public int ProcessId
		{
			get
			{
				return (int)((long)base.GetAttributeValue("processId"));
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000091E8 File Offset: 0x000081E8
		public WorkerProcessState State
		{
			get
			{
				return (WorkerProcessState)base.GetAttributeValue("state");
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000091FC File Offset: 0x000081FC
		public RequestCollection GetRequests(int timeElapsedFilter)
		{
			RequestCollection requestCollection = new RequestCollection(this.ProcessId);
			ConfigurationMethodInstance configurationMethodInstance = base.Methods["GetRequests"].CreateInstance();
			configurationMethodInstance.Input["timeElapsedFilter"] = timeElapsedFilter;
			configurationMethodInstance.Execute();
			requestCollection.Initialize(base.Configuration, configurationMethodInstance.Output.AppHostElement);
			return requestCollection;
		}

		// Token: 0x04000132 RID: 306
		private ApplicationDomainCollection _applicationDomains;
	}
}
