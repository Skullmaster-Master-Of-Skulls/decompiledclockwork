using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000553 RID: 1363
	[__DynamicallyInvokable]
	public sealed class DispatchOperation
	{
		// Token: 0x06003478 RID: 13432 RVA: 0x000CB2E8 File Offset: 0x000C94E8
		[__DynamicallyInvokable]
		public DispatchOperation(DispatchRuntime parent, string name, string action)
		{
			if (parent == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parent");
			}
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			this.parent = parent;
			this.name = name;
			this.action = action;
			this.impersonation = ImpersonationOption.NotAllowed;
			this.callContextInitializers = parent.NewBehaviorCollection<ICallContextInitializer>();
			this.faultContractInfos = parent.NewBehaviorCollection<FaultContractInfo>();
			this.parameterInspectors = parent.NewBehaviorCollection<IParameterInspector>();
			this.isOneWay = true;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000CB37D File Offset: 0x000C957D
		public DispatchOperation(DispatchRuntime parent, string name, string action, string replyAction) : this(parent, name, action)
		{
			this.replyAction = replyAction;
			this.isOneWay = false;
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x000CB397 File Offset: 0x000C9597
		[__DynamicallyInvokable]
		public bool IsOneWay
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isOneWay;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000CB39F File Offset: 0x000C959F
		[__DynamicallyInvokable]
		public string Action
		{
			[__DynamicallyInvokable]
			get
			{
				return this.action;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x000CB3A7 File Offset: 0x000C95A7
		public SynchronizedCollection<ICallContextInitializer> CallContextInitializers
		{
			get
			{
				return this.callContextInitializers;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000CB3AF File Offset: 0x000C95AF
		public SynchronizedCollection<FaultContractInfo> FaultContractInfos
		{
			get
			{
				return this.faultContractInfos;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000CB3B7 File Offset: 0x000C95B7
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x000CB3C0 File Offset: 0x000C95C0
		[__DynamicallyInvokable]
		public bool AutoDisposeParameters
		{
			[__DynamicallyInvokable]
			get
			{
				return this.autoDisposeParameters;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.autoDisposeParameters = value;
				}
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000CB414 File Offset: 0x000C9614
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x000CB41C File Offset: 0x000C961C
		public IDispatchMessageFormatter Formatter
		{
			get
			{
				return this.formatter;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.formatter = value;
				}
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000CB470 File Offset: 0x000C9670
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x000CB494 File Offset: 0x000C9694
		internal IDispatchFaultFormatter FaultFormatter
		{
			get
			{
				if (this.faultFormatter == null)
				{
					this.faultFormatter = new DataContractSerializerFaultFormatter(this.faultContractInfos);
				}
				return this.faultFormatter;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.faultFormatter = value;
					this.isFaultFormatterSetExplicit = true;
				}
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000CB4EC File Offset: 0x000C96EC
		// (set) Token: 0x06003485 RID: 13445 RVA: 0x000CB4F4 File Offset: 0x000C96F4
		internal bool IncludeExceptionDetailInFaults
		{
			get
			{
				return this.includeExceptionDetailInFaults;
			}
			set
			{
				this.includeExceptionDetailInFaults = value;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000CB4FD File Offset: 0x000C96FD
		internal bool IsFaultFormatterSetExplicit
		{
			get
			{
				return this.isFaultFormatterSetExplicit;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000CB505 File Offset: 0x000C9705
		// (set) Token: 0x06003488 RID: 13448 RVA: 0x000CB510 File Offset: 0x000C9710
		public ImpersonationOption Impersonation
		{
			get
			{
				return this.impersonation;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.impersonation = value;
				}
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x000CB564 File Offset: 0x000C9764
		// (set) Token: 0x0600348A RID: 13450 RVA: 0x000CB56C File Offset: 0x000C976C
		internal bool HasNoDisposableParameters
		{
			get
			{
				return this.hasNoDisposableParameters;
			}
			set
			{
				this.hasNoDisposableParameters = value;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x000CB575 File Offset: 0x000C9775
		// (set) Token: 0x0600348C RID: 13452 RVA: 0x000CB57D File Offset: 0x000C977D
		internal IDispatchMessageFormatter InternalFormatter
		{
			get
			{
				return this.formatter;
			}
			set
			{
				this.formatter = value;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000CB586 File Offset: 0x000C9786
		// (set) Token: 0x0600348E RID: 13454 RVA: 0x000CB58E File Offset: 0x000C978E
		internal IOperationInvoker InternalInvoker
		{
			get
			{
				return this.invoker;
			}
			set
			{
				this.invoker = value;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000CB597 File Offset: 0x000C9797
		// (set) Token: 0x06003490 RID: 13456 RVA: 0x000CB5A0 File Offset: 0x000C97A0
		public IOperationInvoker Invoker
		{
			get
			{
				return this.invoker;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.invoker = value;
				}
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000CB5F4 File Offset: 0x000C97F4
		// (set) Token: 0x06003492 RID: 13458 RVA: 0x000CB5FC File Offset: 0x000C97FC
		public bool IsTerminating
		{
			get
			{
				return this.isTerminating;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.isTerminating = value;
				}
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000CB650 File Offset: 0x000C9850
		// (set) Token: 0x06003494 RID: 13460 RVA: 0x000CB658 File Offset: 0x000C9858
		internal bool IsSessionOpenNotificationEnabled
		{
			get
			{
				return this.isSessionOpenNotificationEnabled;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.isSessionOpenNotificationEnabled = value;
				}
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000CB6AC File Offset: 0x000C98AC
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x000CB6B4 File Offset: 0x000C98B4
		public SynchronizedCollection<IParameterInspector> ParameterInspectors
		{
			get
			{
				return this.parameterInspectors;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000CB6BC File Offset: 0x000C98BC
		[__DynamicallyInvokable]
		public DispatchRuntime Parent
		{
			[__DynamicallyInvokable]
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000CB6C4 File Offset: 0x000C98C4
		// (set) Token: 0x06003499 RID: 13465 RVA: 0x000CB6CC File Offset: 0x000C98CC
		internal ReceiveContextAcknowledgementMode ReceiveContextAcknowledgementMode { get; set; }

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x000CB6D5 File Offset: 0x000C98D5
		// (set) Token: 0x0600349B RID: 13467 RVA: 0x000CB6E7 File Offset: 0x000C98E7
		internal bool BufferedReceiveEnabled
		{
			get
			{
				return this.parent.ChannelDispatcher.BufferedReceiveEnabled;
			}
			set
			{
				this.parent.ChannelDispatcher.BufferedReceiveEnabled = value;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000CB6FA File Offset: 0x000C98FA
		// (set) Token: 0x0600349D RID: 13469 RVA: 0x000CB704 File Offset: 0x000C9904
		public bool ReleaseInstanceAfterCall
		{
			get
			{
				return this.releaseInstanceAfterCall;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.releaseInstanceAfterCall = value;
				}
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000CB758 File Offset: 0x000C9958
		// (set) Token: 0x0600349F RID: 13471 RVA: 0x000CB760 File Offset: 0x000C9960
		public bool ReleaseInstanceBeforeCall
		{
			get
			{
				return this.releaseInstanceBeforeCall;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.releaseInstanceBeforeCall = value;
				}
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000CB7B4 File Offset: 0x000C99B4
		public string ReplyAction
		{
			get
			{
				return this.replyAction;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000CB7BC File Offset: 0x000C99BC
		// (set) Token: 0x060034A2 RID: 13474 RVA: 0x000CB7C4 File Offset: 0x000C99C4
		[__DynamicallyInvokable]
		public bool DeserializeRequest
		{
			[__DynamicallyInvokable]
			get
			{
				return this.deserializeRequest;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.deserializeRequest = value;
				}
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060034A3 RID: 13475 RVA: 0x000CB818 File Offset: 0x000C9A18
		// (set) Token: 0x060034A4 RID: 13476 RVA: 0x000CB820 File Offset: 0x000C9A20
		[__DynamicallyInvokable]
		public bool SerializeReply
		{
			[__DynamicallyInvokable]
			get
			{
				return this.serializeReply;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.serializeReply = value;
				}
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000CB874 File Offset: 0x000C9A74
		// (set) Token: 0x060034A6 RID: 13478 RVA: 0x000CB87C File Offset: 0x000C9A7C
		public bool TransactionAutoComplete
		{
			get
			{
				return this.transactionAutoComplete;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.transactionAutoComplete = value;
				}
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060034A7 RID: 13479 RVA: 0x000CB8D0 File Offset: 0x000C9AD0
		// (set) Token: 0x060034A8 RID: 13480 RVA: 0x000CB8D8 File Offset: 0x000C9AD8
		public bool TransactionRequired
		{
			get
			{
				return this.transactionRequired;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.transactionRequired = value;
				}
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x000CB92C File Offset: 0x000C9B2C
		// (set) Token: 0x060034AA RID: 13482 RVA: 0x000CB934 File Offset: 0x000C9B34
		public bool IsInsideTransactedReceiveScope
		{
			get
			{
				return this.isInsideTransactedReceiveScope;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.isInsideTransactedReceiveScope = value;
				}
			}
		}

		// Token: 0x040027F7 RID: 10231
		private string action;

		// Token: 0x040027F8 RID: 10232
		private SynchronizedCollection<ICallContextInitializer> callContextInitializers;

		// Token: 0x040027F9 RID: 10233
		private SynchronizedCollection<FaultContractInfo> faultContractInfos;

		// Token: 0x040027FA RID: 10234
		private IDispatchMessageFormatter formatter;

		// Token: 0x040027FB RID: 10235
		private IDispatchFaultFormatter faultFormatter;

		// Token: 0x040027FC RID: 10236
		private bool includeExceptionDetailInFaults;

		// Token: 0x040027FD RID: 10237
		private ImpersonationOption impersonation;

		// Token: 0x040027FE RID: 10238
		private IOperationInvoker invoker;

		// Token: 0x040027FF RID: 10239
		private bool isTerminating;

		// Token: 0x04002800 RID: 10240
		private bool isSessionOpenNotificationEnabled;

		// Token: 0x04002801 RID: 10241
		private string name;

		// Token: 0x04002802 RID: 10242
		private SynchronizedCollection<IParameterInspector> parameterInspectors;

		// Token: 0x04002803 RID: 10243
		private DispatchRuntime parent;

		// Token: 0x04002804 RID: 10244
		private bool releaseInstanceAfterCall;

		// Token: 0x04002805 RID: 10245
		private bool releaseInstanceBeforeCall;

		// Token: 0x04002806 RID: 10246
		private string replyAction;

		// Token: 0x04002807 RID: 10247
		private bool transactionAutoComplete;

		// Token: 0x04002808 RID: 10248
		private bool transactionRequired;

		// Token: 0x04002809 RID: 10249
		private bool deserializeRequest = true;

		// Token: 0x0400280A RID: 10250
		private bool serializeReply = true;

		// Token: 0x0400280B RID: 10251
		private bool isOneWay;

		// Token: 0x0400280C RID: 10252
		private bool autoDisposeParameters = true;

		// Token: 0x0400280D RID: 10253
		private bool hasNoDisposableParameters;

		// Token: 0x0400280E RID: 10254
		private bool isFaultFormatterSetExplicit;

		// Token: 0x0400280F RID: 10255
		private bool isInsideTransactedReceiveScope;
	}
}
