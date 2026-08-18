using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020006A9 RID: 1705
	internal class IllogicalCallContext : ICloneable
	{
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003D95 RID: 15765 RVA: 0x000D27C3 File Offset: 0x000D17C3
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06003D96 RID: 15766 RVA: 0x000D27DE File Offset: 0x000D17DE
		// (set) Token: 0x06003D97 RID: 15767 RVA: 0x000D27E6 File Offset: 0x000D17E6
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000D27EF File Offset: 0x000D17EF
		internal bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x000D2809 File Offset: 0x000D1809
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x000D2817 File Offset: 0x000D1817
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x000D2825 File Offset: 0x000D1825
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x000D2834 File Offset: 0x000D1834
		public object Clone()
		{
			IllogicalCallContext illogicalCallContext = new IllogicalCallContext();
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					illogicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
			return illogicalCallContext;
		}

		// Token: 0x04001F78 RID: 8056
		private Hashtable m_Datastore;

		// Token: 0x04001F79 RID: 8057
		private object m_HostContext;
	}
}
