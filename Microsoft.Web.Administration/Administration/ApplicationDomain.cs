using System;
using System.Diagnostics;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000008 RID: 8
	[DebuggerDisplay("Id = {Id}")]
	public class ApplicationDomain : ConfigurationElement
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000035AC File Offset: 0x000025AC
		internal ApplicationDomain(WorkerProcess parentWorkerProcess)
		{
			this._parentWorkerProcess = parentWorkerProcess;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000035BB File Offset: 0x000025BB
		public string Id
		{
			get
			{
				return (string)base.GetAttributeValue("id");
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000035CD File Offset: 0x000025CD
		public int Idle
		{
			get
			{
				return (int)((long)base.GetAttributeValue("idle"));
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000035E0 File Offset: 0x000025E0
		public string PhysicalPath
		{
			get
			{
				return (string)base.GetAttributeValue("physicalPath");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000035F2 File Offset: 0x000025F2
		public string VirtualPath
		{
			get
			{
				return (string)base.GetAttributeValue("virtualPath");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003604 File Offset: 0x00002604
		public WorkerProcess WorkerProcess
		{
			get
			{
				return this._parentWorkerProcess;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000360C File Offset: 0x0000260C
		public void Unload()
		{
			base.ExecuteMethod("Unload");
		}

		// Token: 0x0400001A RID: 26
		private WorkerProcess _parentWorkerProcess;
	}
}
