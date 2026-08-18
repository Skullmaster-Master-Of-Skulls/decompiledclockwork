using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200054B RID: 1355
	[__DynamicallyInvokable]
	public sealed class ClientRuntime : ClientRuntimeCompatBase
	{
		// Token: 0x06003376 RID: 13174 RVA: 0x000C6BE6 File Offset: 0x000C4DE6
		internal ClientRuntime(DispatchRuntime dispatchRuntime, SharedRuntimeState shared) : this(dispatchRuntime.EndpointDispatcher.ContractName, dispatchRuntime.EndpointDispatcher.ContractNamespace, shared)
		{
			if (dispatchRuntime == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dispatchRuntime");
			}
			this.dispatchRuntime = dispatchRuntime;
			this.shared = shared;
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000C6C26 File Offset: 0x000C4E26
		internal ClientRuntime(string contractName, string contractNamespace) : this(contractName, contractNamespace, new SharedRuntimeState(false))
		{
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000C6C38 File Offset: 0x000C4E38
		private ClientRuntime(string contractName, string contractNamespace, SharedRuntimeState shared)
		{
			this.contractName = contractName;
			this.contractNamespace = contractNamespace;
			this.shared = shared;
			ClientRuntime.OperationCollection operationCollection = new ClientRuntime.OperationCollection(this);
			this.operations = operationCollection;
			this.compatOperations = new ClientRuntime.OperationCollectionWrapper(operationCollection);
			this.channelInitializers = new ClientRuntime.ProxyBehaviorCollection<IChannelInitializer>(this);
			this.messageInspectors = new ClientRuntime.ProxyBehaviorCollection<IClientMessageInspector>(this);
			this.interactiveChannelInitializers = new ClientRuntime.ProxyBehaviorCollection<IInteractiveChannelInitializer>(this);
			this.unhandled = new ClientOperation(this, "*", "*", "*");
			this.unhandled.InternalFormatter = new MessageOperationFormatter();
			this.maxFaultSize = 65536;
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000C6CE2 File Offset: 0x000C4EE2
		// (set) Token: 0x0600337A RID: 13178 RVA: 0x000C6CEC File Offset: 0x000C4EEC
		internal bool AddTransactionFlowProperties
		{
			get
			{
				return this.addTransactionFlowProperties;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.addTransactionFlowProperties = value;
				}
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000C6D34 File Offset: 0x000C4F34
		// (set) Token: 0x0600337C RID: 13180 RVA: 0x000C6D3C File Offset: 0x000C4F3C
		public Type CallbackClientType
		{
			get
			{
				return this.callbackProxyType;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.callbackProxyType = value;
				}
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000C6D84 File Offset: 0x000C4F84
		public SynchronizedCollection<IChannelInitializer> ChannelInitializers
		{
			get
			{
				return this.channelInitializers;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x000C6D8C File Offset: 0x000C4F8C
		[__DynamicallyInvokable]
		public string ContractName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.contractName;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000C6D94 File Offset: 0x000C4F94
		[__DynamicallyInvokable]
		public string ContractNamespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.contractNamespace;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x000C6D9C File Offset: 0x000C4F9C
		// (set) Token: 0x06003381 RID: 13185 RVA: 0x000C6DA4 File Offset: 0x000C4FA4
		[__DynamicallyInvokable]
		public Type ContractClientType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.contractProxyType;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.contractProxyType = value;
				}
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000C6DEC File Offset: 0x000C4FEC
		// (set) Token: 0x06003383 RID: 13187 RVA: 0x000C6E07 File Offset: 0x000C5007
		internal IdentityVerifier IdentityVerifier
		{
			get
			{
				if (this.identityVerifier == null)
				{
					this.identityVerifier = IdentityVerifier.CreateDefault();
				}
				return this.identityVerifier;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.InvalidateRuntime();
				this.identityVerifier = value;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000C6E29 File Offset: 0x000C5029
		// (set) Token: 0x06003385 RID: 13189 RVA: 0x000C6E34 File Offset: 0x000C5034
		[__DynamicallyInvokable]
		public Uri Via
		{
			[__DynamicallyInvokable]
			get
			{
				return this.via;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.via = value;
				}
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000C6E7C File Offset: 0x000C507C
		// (set) Token: 0x06003387 RID: 13191 RVA: 0x000C6E8C File Offset: 0x000C508C
		public bool ValidateMustUnderstand
		{
			get
			{
				return this.shared.ValidateMustUnderstand;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.shared.ValidateMustUnderstand = value;
				}
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x000C6ED8 File Offset: 0x000C50D8
		// (set) Token: 0x06003389 RID: 13193 RVA: 0x000C6EE0 File Offset: 0x000C50E0
		public bool MessageVersionNoneFaultsEnabled
		{
			get
			{
				return this.messageVersionNoneFaultsEnabled;
			}
			set
			{
				this.InvalidateRuntime();
				this.messageVersionNoneFaultsEnabled = value;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000C6EEF File Offset: 0x000C50EF
		internal DispatchRuntime DispatchRuntime
		{
			get
			{
				return this.dispatchRuntime;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x000C6EF7 File Offset: 0x000C50F7
		public DispatchRuntime CallbackDispatchRuntime
		{
			get
			{
				if (this.dispatchRuntime == null)
				{
					this.dispatchRuntime = new DispatchRuntime(this, this.shared);
				}
				return this.dispatchRuntime;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000C6F19 File Offset: 0x000C5119
		// (set) Token: 0x0600338D RID: 13197 RVA: 0x000C6F3C File Offset: 0x000C513C
		internal bool EnableFaults
		{
			get
			{
				if (this.IsOnServer)
				{
					return this.dispatchRuntime.EnableFaults;
				}
				return this.shared.EnableFaults;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.IsOnServer)
					{
						string @string = SR.GetString("SFxSetEnableFaultsOnChannelDispatcher0");
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(@string));
					}
					this.InvalidateRuntime();
					this.shared.EnableFaults = value;
				}
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000C6FAC File Offset: 0x000C51AC
		public SynchronizedCollection<IInteractiveChannelInitializer> InteractiveChannelInitializers
		{
			get
			{
				return this.interactiveChannelInitializers;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x0600338F RID: 13199 RVA: 0x000C6FB4 File Offset: 0x000C51B4
		// (set) Token: 0x06003390 RID: 13200 RVA: 0x000C6FBC File Offset: 0x000C51BC
		[__DynamicallyInvokable]
		public int MaxFaultSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxFaultSize;
			}
			[__DynamicallyInvokable]
			set
			{
				this.InvalidateRuntime();
				this.maxFaultSize = value;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x000C6FCB File Offset: 0x000C51CB
		internal bool IsOnServer
		{
			get
			{
				return this.shared.IsOnServer;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000C6FD8 File Offset: 0x000C51D8
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x000C6FFC File Offset: 0x000C51FC
		[__DynamicallyInvokable]
		public bool ManualAddressing
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.IsOnServer)
				{
					return this.dispatchRuntime.ManualAddressing;
				}
				return this.shared.ManualAddressing;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.IsOnServer)
					{
						string @string = SR.GetString("SFxSetManualAddresssingOnChannelDispatcher0");
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(@string));
					}
					this.InvalidateRuntime();
					this.shared.ManualAddressing = value;
				}
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000C706C File Offset: 0x000C526C
		internal int MaxParameterInspectors
		{
			get
			{
				object thisLock = this.ThisLock;
				int result;
				lock (thisLock)
				{
					int num = 0;
					for (int i = 0; i < this.operations.Count; i++)
					{
						num = Math.Max(num, this.operations[i].ParameterInspectors.Count);
					}
					result = num;
				}
				return result;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06003395 RID: 13205 RVA: 0x000C70E0 File Offset: 0x000C52E0
		[__DynamicallyInvokable]
		public ICollection<IClientMessageInspector> ClientMessageInspectors
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MessageInspectors;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000C70E8 File Offset: 0x000C52E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new SynchronizedCollection<IClientMessageInspector> MessageInspectors
		{
			get
			{
				return this.messageInspectors;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x000C70F0 File Offset: 0x000C52F0
		[__DynamicallyInvokable]
		public ICollection<ClientOperation> ClientOperations
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Operations;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000C70F8 File Offset: 0x000C52F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new SynchronizedKeyedCollection<string, ClientOperation> Operations
		{
			get
			{
				return this.operations;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000C7100 File Offset: 0x000C5300
		// (set) Token: 0x0600339A RID: 13210 RVA: 0x000C7108 File Offset: 0x000C5308
		[__DynamicallyInvokable]
		public IClientOperationSelector OperationSelector
		{
			[__DynamicallyInvokable]
			get
			{
				return this.operationSelector;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.operationSelector = value;
				}
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000C7150 File Offset: 0x000C5350
		internal object ThisLock
		{
			get
			{
				return this.shared;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000C7158 File Offset: 0x000C5358
		[__DynamicallyInvokable]
		public ClientOperation UnhandledClientOperation
		{
			[__DynamicallyInvokable]
			get
			{
				return this.unhandled;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000C7160 File Offset: 0x000C5360
		// (set) Token: 0x0600339E RID: 13214 RVA: 0x000C7168 File Offset: 0x000C5368
		internal bool UseSynchronizationContext
		{
			get
			{
				return this.useSynchronizationContext;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.useSynchronizationContext = value;
				}
			}
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000C71B0 File Offset: 0x000C53B0
		internal T[] GetArray<T>(SynchronizedCollection<T> collection)
		{
			object syncRoot = collection.SyncRoot;
			T[] result;
			lock (syncRoot)
			{
				if (collection.Count == 0)
				{
					result = EmptyArray<T>.Instance;
				}
				else
				{
					T[] array = new T[collection.Count];
					collection.CopyTo(array, 0);
					result = array;
				}
			}
			return result;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000C7214 File Offset: 0x000C5414
		internal ImmutableClientRuntime GetRuntime()
		{
			object thisLock = this.ThisLock;
			ImmutableClientRuntime result;
			lock (thisLock)
			{
				if (this.runtime == null)
				{
					this.runtime = new ImmutableClientRuntime(this);
				}
				result = this.runtime;
			}
			return result;
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000C726C File Offset: 0x000C546C
		internal void InvalidateRuntime()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.shared.ThrowIfImmutable();
				this.runtime = null;
			}
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000C72B8 File Offset: 0x000C54B8
		internal void LockDownProperties()
		{
			this.shared.LockDownProperties();
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000C72C5 File Offset: 0x000C54C5
		internal SynchronizedCollection<T> NewBehaviorCollection<T>()
		{
			return new ClientRuntime.ProxyBehaviorCollection<T>(this);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000C72CD File Offset: 0x000C54CD
		internal bool IsFault(ref Message reply)
		{
			return reply != null && (reply.IsFault || (this.MessageVersionNoneFaultsEnabled && ClientRuntime.IsMessageVersionNoneFault(ref reply, this.MaxFaultSize)));
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000C72FC File Offset: 0x000C54FC
		internal static bool IsMessageVersionNoneFault(ref Message message, int maxFaultSize)
		{
			if (message.Version != MessageVersion.None || message.IsEmpty)
			{
				return false;
			}
			HttpResponseMessageProperty httpResponseMessageProperty = message.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
			if (httpResponseMessageProperty == null || httpResponseMessageProperty.StatusCode != HttpStatusCode.InternalServerError)
			{
				return false;
			}
			bool result;
			using (MessageBuffer messageBuffer = message.CreateBufferedCopy(maxFaultSize))
			{
				message.Close();
				message = messageBuffer.CreateMessage();
				using (Message message2 = messageBuffer.CreateMessage())
				{
					using (XmlDictionaryReader readerAtBodyContents = message2.GetReaderAtBodyContents())
					{
						result = readerAtBodyContents.IsStartElement(XD.MessageDictionary.Fault, MessageVersion.None.Envelope.DictionaryNamespace);
					}
				}
			}
			return result;
		}

		// Token: 0x0400278B RID: 10123
		private bool addTransactionFlowProperties = true;

		// Token: 0x0400278C RID: 10124
		private Type callbackProxyType;

		// Token: 0x0400278D RID: 10125
		private ClientRuntime.ProxyBehaviorCollection<IChannelInitializer> channelInitializers;

		// Token: 0x0400278E RID: 10126
		private string contractName;

		// Token: 0x0400278F RID: 10127
		private string contractNamespace;

		// Token: 0x04002790 RID: 10128
		private Type contractProxyType;

		// Token: 0x04002791 RID: 10129
		private DispatchRuntime dispatchRuntime;

		// Token: 0x04002792 RID: 10130
		private IdentityVerifier identityVerifier;

		// Token: 0x04002793 RID: 10131
		private ClientRuntime.ProxyBehaviorCollection<IInteractiveChannelInitializer> interactiveChannelInitializers;

		// Token: 0x04002794 RID: 10132
		private IClientOperationSelector operationSelector;

		// Token: 0x04002795 RID: 10133
		private ImmutableClientRuntime runtime;

		// Token: 0x04002796 RID: 10134
		private ClientOperation unhandled;

		// Token: 0x04002797 RID: 10135
		private bool useSynchronizationContext = true;

		// Token: 0x04002798 RID: 10136
		private Uri via;

		// Token: 0x04002799 RID: 10137
		private SharedRuntimeState shared;

		// Token: 0x0400279A RID: 10138
		private int maxFaultSize;

		// Token: 0x0400279B RID: 10139
		private bool messageVersionNoneFaultsEnabled;

		// Token: 0x02000C6F RID: 3183
		private class ProxyBehaviorCollection<T> : SynchronizedCollection<T>
		{
			// Token: 0x060077FC RID: 30716 RVA: 0x001C0F2C File Offset: 0x001BF12C
			internal ProxyBehaviorCollection(ClientRuntime outer) : base(outer.ThisLock)
			{
				this.outer = outer;
			}

			// Token: 0x060077FD RID: 30717 RVA: 0x001C0F41 File Offset: 0x001BF141
			protected override void ClearItems()
			{
				this.outer.InvalidateRuntime();
				base.ClearItems();
			}

			// Token: 0x060077FE RID: 30718 RVA: 0x001C0F54 File Offset: 0x001BF154
			protected override void InsertItem(int index, T item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				this.outer.InvalidateRuntime();
				base.InsertItem(index, item);
			}

			// Token: 0x060077FF RID: 30719 RVA: 0x001C0F81 File Offset: 0x001BF181
			protected override void RemoveItem(int index)
			{
				this.outer.InvalidateRuntime();
				base.RemoveItem(index);
			}

			// Token: 0x06007800 RID: 30720 RVA: 0x001C0F95 File Offset: 0x001BF195
			protected override void SetItem(int index, T item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				this.outer.InvalidateRuntime();
				base.SetItem(index, item);
			}

			// Token: 0x0400447E RID: 17534
			private ClientRuntime outer;
		}

		// Token: 0x02000C70 RID: 3184
		private class OperationCollection : SynchronizedKeyedCollection<string, ClientOperation>
		{
			// Token: 0x06007801 RID: 30721 RVA: 0x001C0FC2 File Offset: 0x001BF1C2
			internal OperationCollection(ClientRuntime outer) : base(outer.ThisLock)
			{
				this.outer = outer;
			}

			// Token: 0x06007802 RID: 30722 RVA: 0x001C0FD7 File Offset: 0x001BF1D7
			protected override void ClearItems()
			{
				this.outer.InvalidateRuntime();
				base.ClearItems();
			}

			// Token: 0x06007803 RID: 30723 RVA: 0x001C0FEA File Offset: 0x001BF1EA
			protected override string GetKeyForItem(ClientOperation item)
			{
				return item.Name;
			}

			// Token: 0x06007804 RID: 30724 RVA: 0x001C0FF4 File Offset: 0x001BF1F4
			protected override void InsertItem(int index, ClientOperation item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				if (item.Parent != this.outer)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxMismatchedOperationParent"));
				}
				this.outer.InvalidateRuntime();
				base.InsertItem(index, item);
			}

			// Token: 0x06007805 RID: 30725 RVA: 0x001C104A File Offset: 0x001BF24A
			protected override void RemoveItem(int index)
			{
				this.outer.InvalidateRuntime();
				base.RemoveItem(index);
			}

			// Token: 0x06007806 RID: 30726 RVA: 0x001C1060 File Offset: 0x001BF260
			protected override void SetItem(int index, ClientOperation item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				if (item.Parent != this.outer)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxMismatchedOperationParent"));
				}
				this.outer.InvalidateRuntime();
				base.SetItem(index, item);
			}

			// Token: 0x06007807 RID: 30727 RVA: 0x001C10B6 File Offset: 0x001BF2B6
			internal void InternalClearItems()
			{
				this.ClearItems();
			}

			// Token: 0x06007808 RID: 30728 RVA: 0x001C10BE File Offset: 0x001BF2BE
			internal string InternalGetKeyForItem(ClientOperation item)
			{
				return this.GetKeyForItem(item);
			}

			// Token: 0x06007809 RID: 30729 RVA: 0x001C10C7 File Offset: 0x001BF2C7
			internal void InternalInsertItem(int index, ClientOperation item)
			{
				this.InsertItem(index, item);
			}

			// Token: 0x0600780A RID: 30730 RVA: 0x001C10D1 File Offset: 0x001BF2D1
			internal void InternalRemoveItem(int index)
			{
				this.RemoveItem(index);
			}

			// Token: 0x0600780B RID: 30731 RVA: 0x001C10DA File Offset: 0x001BF2DA
			internal void InternalSetItem(int index, ClientOperation item)
			{
				this.SetItem(index, item);
			}

			// Token: 0x0400447F RID: 17535
			private ClientRuntime outer;
		}

		// Token: 0x02000C71 RID: 3185
		private class OperationCollectionWrapper : KeyedCollection<string, ClientOperation>
		{
			// Token: 0x0600780C RID: 30732 RVA: 0x001C10E4 File Offset: 0x001BF2E4
			internal OperationCollectionWrapper(ClientRuntime.OperationCollection inner)
			{
				this.inner = inner;
			}

			// Token: 0x0600780D RID: 30733 RVA: 0x001C10F3 File Offset: 0x001BF2F3
			protected override void ClearItems()
			{
				this.inner.InternalClearItems();
			}

			// Token: 0x0600780E RID: 30734 RVA: 0x001C1100 File Offset: 0x001BF300
			protected override string GetKeyForItem(ClientOperation item)
			{
				return this.inner.InternalGetKeyForItem(item);
			}

			// Token: 0x0600780F RID: 30735 RVA: 0x001C110E File Offset: 0x001BF30E
			protected override void InsertItem(int index, ClientOperation item)
			{
				this.inner.InternalInsertItem(index, item);
			}

			// Token: 0x06007810 RID: 30736 RVA: 0x001C111D File Offset: 0x001BF31D
			protected override void RemoveItem(int index)
			{
				this.inner.InternalRemoveItem(index);
			}

			// Token: 0x06007811 RID: 30737 RVA: 0x001C112B File Offset: 0x001BF32B
			protected override void SetItem(int index, ClientOperation item)
			{
				this.inner.InternalSetItem(index, item);
			}

			// Token: 0x04004480 RID: 17536
			private ClientRuntime.OperationCollection inner;
		}
	}
}
