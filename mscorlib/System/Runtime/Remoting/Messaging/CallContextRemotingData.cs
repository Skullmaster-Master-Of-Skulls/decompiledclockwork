using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020006AC RID: 1708
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003DBC RID: 15804 RVA: 0x000D2EE4 File Offset: 0x000D1EE4
		// (set) Token: 0x06003DBD RID: 15805 RVA: 0x000D2EEC File Offset: 0x000D1EEC
		internal string LogicalCallID
		{
			get
			{
				return this._logicalCallID;
			}
			set
			{
				this._logicalCallID = value;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000D2EF5 File Offset: 0x000D1EF5
		internal bool HasInfo
		{
			get
			{
				return this._logicalCallID != null;
			}
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x000D2F04 File Offset: 0x000D1F04
		public object Clone()
		{
			return new CallContextRemotingData
			{
				LogicalCallID = this.LogicalCallID
			};
		}

		// Token: 0x04001F84 RID: 8068
		private string _logicalCallID;
	}
}
