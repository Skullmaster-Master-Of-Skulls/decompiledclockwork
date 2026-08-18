using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020006AA RID: 1706
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public sealed class LogicalCallContext : ISerializable, ICloneable
	{
		// Token: 0x06003D9E RID: 15774 RVA: 0x000D288A File Offset: 0x000D188A
		internal LogicalCallContext()
		{
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x000D2894 File Offset: 0x000D1894
		internal LogicalCallContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("__RemotingData"))
				{
					this.m_RemotingData = (CallContextRemotingData)enumerator.Value;
				}
				else if (enumerator.Name.Equals("__SecurityData"))
				{
					if (context.State == StreamingContextStates.CrossAppDomain)
					{
						this.m_SecurityData = (CallContextSecurityData)enumerator.Value;
					}
				}
				else if (enumerator.Name.Equals("__HostContext"))
				{
					this.m_HostContext = enumerator.Value;
				}
				else if (enumerator.Name.Equals("__CorrelationMgrSlotPresent"))
				{
					this.m_IsCorrelationMgr = (bool)enumerator.Value;
				}
				else
				{
					this.Datastore[enumerator.Name] = enumerator.Value;
				}
			}
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x000D2978 File Offset: 0x000D1978
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(LogicalCallContext.s_callContextType);
			if (this.m_RemotingData != null)
			{
				info.AddValue("__RemotingData", this.m_RemotingData);
			}
			if (this.m_SecurityData != null && context.State == StreamingContextStates.CrossAppDomain)
			{
				info.AddValue("__SecurityData", this.m_SecurityData);
			}
			if (this.m_HostContext != null)
			{
				info.AddValue("__HostContext", this.m_HostContext);
			}
			if (this.m_IsCorrelationMgr)
			{
				info.AddValue("__CorrelationMgrSlotPresent", this.m_IsCorrelationMgr);
			}
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					info.AddValue((string)enumerator.Key, enumerator.Value);
				}
			}
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x000D2A48 File Offset: 0x000D1A48
		public object Clone()
		{
			LogicalCallContext logicalCallContext = new LogicalCallContext();
			if (this.m_RemotingData != null)
			{
				logicalCallContext.m_RemotingData = (CallContextRemotingData)this.m_RemotingData.Clone();
			}
			if (this.m_SecurityData != null)
			{
				logicalCallContext.m_SecurityData = (CallContextSecurityData)this.m_SecurityData.Clone();
			}
			if (this.m_HostContext != null)
			{
				logicalCallContext.m_HostContext = this.m_HostContext;
			}
			logicalCallContext.m_IsCorrelationMgr = this.m_IsCorrelationMgr;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				if (!this.m_IsCorrelationMgr)
				{
					while (enumerator.MoveNext())
					{
						logicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
					}
				}
				else
				{
					while (enumerator.MoveNext())
					{
						string text = (string)enumerator.Key;
						if (text.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
						{
							logicalCallContext.Datastore[text] = ((ICloneable)enumerator.Value).Clone();
						}
						else
						{
							logicalCallContext.Datastore[text] = enumerator.Value;
						}
					}
				}
			}
			return logicalCallContext;
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x000D2B50 File Offset: 0x000D1B50
		internal void Merge(LogicalCallContext lc)
		{
			if (lc != null && this != lc && lc.HasUserData)
			{
				IDictionaryEnumerator enumerator = lc.Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x000D2BA0 File Offset: 0x000D1BA0
		public bool HasInfo
		{
			get
			{
				bool result = false;
				if ((this.m_RemotingData != null && this.m_RemotingData.HasInfo) || (this.m_SecurityData != null && this.m_SecurityData.HasInfo) || this.m_HostContext != null || this.HasUserData)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x000D2BEC File Offset: 0x000D1BEC
		private bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x000D2C06 File Offset: 0x000D1C06
		internal CallContextRemotingData RemotingData
		{
			get
			{
				if (this.m_RemotingData == null)
				{
					this.m_RemotingData = new CallContextRemotingData();
				}
				return this.m_RemotingData;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x000D2C21 File Offset: 0x000D1C21
		internal CallContextSecurityData SecurityData
		{
			get
			{
				if (this.m_SecurityData == null)
				{
					this.m_SecurityData = new CallContextSecurityData();
				}
				return this.m_SecurityData;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x000D2C3C File Offset: 0x000D1C3C
		// (set) Token: 0x06003DA8 RID: 15784 RVA: 0x000D2C44 File Offset: 0x000D1C44
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

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x000D2C4D File Offset: 0x000D1C4D
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

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06003DAA RID: 15786 RVA: 0x000D2C68 File Offset: 0x000D1C68
		// (set) Token: 0x06003DAB RID: 15787 RVA: 0x000D2C7F File Offset: 0x000D1C7F
		internal IPrincipal Principal
		{
			get
			{
				if (this.m_SecurityData != null)
				{
					return this.m_SecurityData.Principal;
				}
				return null;
			}
			set
			{
				this.SecurityData.Principal = value;
			}
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000D2C8D File Offset: 0x000D1C8D
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000D2C9B File Offset: 0x000D1C9B
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000D2CA9 File Offset: 0x000D1CA9
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
			if (name.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
			{
				this.m_IsCorrelationMgr = true;
			}
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000D2CCC File Offset: 0x000D1CCC
		private Header[] InternalGetOutgoingHeaders()
		{
			Header[] sendHeaders = this._sendHeaders;
			this._sendHeaders = null;
			this._recvHeaders = null;
			return sendHeaders;
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000D2CEF File Offset: 0x000D1CEF
		internal void InternalSetHeaders(Header[] headers)
		{
			this._sendHeaders = headers;
			this._recvHeaders = null;
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000D2CFF File Offset: 0x000D1CFF
		internal Header[] InternalGetHeaders()
		{
			if (this._sendHeaders != null)
			{
				return this._sendHeaders;
			}
			return this._recvHeaders;
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000D2D18 File Offset: 0x000D1D18
		internal IPrincipal RemovePrincipalIfNotSerializable()
		{
			IPrincipal principal = this.Principal;
			if (principal != null && !principal.GetType().IsSerializable)
			{
				this.Principal = null;
			}
			return principal;
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000D2D44 File Offset: 0x000D1D44
		internal void PropagateOutgoingHeadersToMessage(IMessage msg)
		{
			Header[] array = this.InternalGetOutgoingHeaders();
			if (array != null)
			{
				IDictionary properties = msg.Properties;
				foreach (Header header in array)
				{
					if (header != null)
					{
						string propertyKeyForHeader = LogicalCallContext.GetPropertyKeyForHeader(header);
						properties[propertyKeyForHeader] = header;
					}
				}
			}
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000D2D90 File Offset: 0x000D1D90
		internal static string GetPropertyKeyForHeader(Header header)
		{
			if (header == null)
			{
				return null;
			}
			if (header.HeaderNamespace != null)
			{
				return header.Name + ", " + header.HeaderNamespace;
			}
			return header.Name;
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000D2DBC File Offset: 0x000D1DBC
		internal void PropagateIncomingHeadersToCallContext(IMessage msg)
		{
			IInternalMessage internalMessage = msg as IInternalMessage;
			if (internalMessage != null && !internalMessage.HasProperties())
			{
				return;
			}
			IDictionary properties = msg.Properties;
			IDictionaryEnumerator enumerator = properties.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				if (!text.StartsWith("__", StringComparison.Ordinal) && enumerator.Value is Header)
				{
					num++;
				}
			}
			Header[] array = null;
			if (num > 0)
			{
				array = new Header[num];
				num = 0;
				enumerator.Reset();
				while (enumerator.MoveNext())
				{
					string text2 = (string)enumerator.Key;
					if (!text2.StartsWith("__", StringComparison.Ordinal))
					{
						Header header = enumerator.Value as Header;
						if (header != null)
						{
							array[num++] = header;
						}
					}
				}
			}
			this._recvHeaders = array;
			this._sendHeaders = null;
		}

		// Token: 0x04001F7A RID: 8058
		private const string s_CorrelationMgrSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x04001F7B RID: 8059
		private static Type s_callContextType = typeof(LogicalCallContext);

		// Token: 0x04001F7C RID: 8060
		private Hashtable m_Datastore;

		// Token: 0x04001F7D RID: 8061
		private CallContextRemotingData m_RemotingData;

		// Token: 0x04001F7E RID: 8062
		private CallContextSecurityData m_SecurityData;

		// Token: 0x04001F7F RID: 8063
		private object m_HostContext;

		// Token: 0x04001F80 RID: 8064
		private bool m_IsCorrelationMgr;

		// Token: 0x04001F81 RID: 8065
		private Header[] _sendHeaders;

		// Token: 0x04001F82 RID: 8066
		private Header[] _recvHeaders;
	}
}
