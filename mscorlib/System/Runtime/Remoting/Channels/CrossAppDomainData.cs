using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006D0 RID: 1744
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x000D789A File Offset: 0x000D689A
		internal virtual IntPtr ContextID
		{
			get
			{
				return new IntPtr((long)this._ContextID);
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x000D78AC File Offset: 0x000D68AC
		internal virtual int DomainID
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x000D78B4 File Offset: 0x000D68B4
		internal virtual string ProcessGuid
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x000D78BC File Offset: 0x000D68BC
		internal CrossAppDomainData(IntPtr ctxId, int domainID, string processGuid)
		{
			this._DomainID = domainID;
			this._processGuid = processGuid;
			this._ContextID = ctxId.ToInt64();
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x000D78F0 File Offset: 0x000D68F0
		internal bool IsFromThisProcess()
		{
			return Identity.ProcessGuid.Equals(this._processGuid);
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x000D7902 File Offset: 0x000D6902
		internal bool IsFromThisAppDomain()
		{
			return this.IsFromThisProcess() && Thread.GetDomain().GetId() == this._DomainID;
		}

		// Token: 0x04001FF7 RID: 8183
		private object _ContextID = 0;

		// Token: 0x04001FF8 RID: 8184
		private int _DomainID;

		// Token: 0x04001FF9 RID: 8185
		private string _processGuid;
	}
}
