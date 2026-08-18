using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000597 RID: 1431
	[__DynamicallyInvokable]
	public sealed class ClientOperation : ClientOperationCompatBase
	{
		// Token: 0x06003746 RID: 14150 RVA: 0x000D54A0 File Offset: 0x000D36A0
		[__DynamicallyInvokable]
		public ClientOperation(ClientRuntime parent, string name, string action) : this(parent, name, action, null)
		{
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000D54AC File Offset: 0x000D36AC
		[__DynamicallyInvokable]
		public ClientOperation(ClientRuntime parent, string name, string action, string replyAction)
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
			this.replyAction = replyAction;
			this.faultContractInfos = parent.NewBehaviorCollection<FaultContractInfo>();
			this.parameterInspectors = parent.NewBehaviorCollection<IParameterInspector>();
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003748 RID: 14152 RVA: 0x000D5521 File Offset: 0x000D3721
		[__DynamicallyInvokable]
		public string Action
		{
			[__DynamicallyInvokable]
			get
			{
				return this.action;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x000D5529 File Offset: 0x000D3729
		public SynchronizedCollection<FaultContractInfo> FaultContractInfos
		{
			get
			{
				return this.faultContractInfos;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000D5531 File Offset: 0x000D3731
		// (set) Token: 0x0600374B RID: 14155 RVA: 0x000D553C File Offset: 0x000D373C
		[__DynamicallyInvokable]
		public MethodInfo BeginMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.beginMethod;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.beginMethod = value;
				}
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000D5590 File Offset: 0x000D3790
		// (set) Token: 0x0600374D RID: 14157 RVA: 0x000D5598 File Offset: 0x000D3798
		[__DynamicallyInvokable]
		public MethodInfo EndMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.endMethod;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.endMethod = value;
				}
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000D55EC File Offset: 0x000D37EC
		// (set) Token: 0x0600374F RID: 14159 RVA: 0x000D55F4 File Offset: 0x000D37F4
		[__DynamicallyInvokable]
		public MethodInfo SyncMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.syncMethod;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.syncMethod = value;
				}
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x000D5648 File Offset: 0x000D3848
		// (set) Token: 0x06003751 RID: 14161 RVA: 0x000D5650 File Offset: 0x000D3850
		[__DynamicallyInvokable]
		public IClientMessageFormatter Formatter
		{
			[__DynamicallyInvokable]
			get
			{
				return this.formatter;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06003752 RID: 14162 RVA: 0x000D56A4 File Offset: 0x000D38A4
		// (set) Token: 0x06003753 RID: 14163 RVA: 0x000D56C8 File Offset: 0x000D38C8
		internal IClientFaultFormatter FaultFormatter
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

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x000D5720 File Offset: 0x000D3920
		internal bool IsFaultFormatterSetExplicit
		{
			get
			{
				return this.isFaultFormatterSetExplicit;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x000D5728 File Offset: 0x000D3928
		// (set) Token: 0x06003756 RID: 14166 RVA: 0x000D5730 File Offset: 0x000D3930
		internal IClientMessageFormatter InternalFormatter
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

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06003757 RID: 14167 RVA: 0x000D5739 File Offset: 0x000D3939
		// (set) Token: 0x06003758 RID: 14168 RVA: 0x000D5744 File Offset: 0x000D3944
		public bool IsInitiating
		{
			get
			{
				return this.isInitiating;
			}
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.isInitiating = value;
				}
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000D5798 File Offset: 0x000D3998
		// (set) Token: 0x0600375A RID: 14170 RVA: 0x000D57A0 File Offset: 0x000D39A0
		[__DynamicallyInvokable]
		public bool IsOneWay
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isOneWay;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.isOneWay = value;
				}
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000D57F4 File Offset: 0x000D39F4
		// (set) Token: 0x0600375C RID: 14172 RVA: 0x000D57FC File Offset: 0x000D39FC
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

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x000D5850 File Offset: 0x000D3A50
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000D5858 File Offset: 0x000D3A58
		[__DynamicallyInvokable]
		public ICollection<IParameterInspector> ClientParameterInspectors
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ParameterInspectors;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x000D5860 File Offset: 0x000D3A60
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new SynchronizedCollection<IParameterInspector> ParameterInspectors
		{
			get
			{
				return this.parameterInspectors;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000D5868 File Offset: 0x000D3A68
		[__DynamicallyInvokable]
		public ClientRuntime Parent
		{
			[__DynamicallyInvokable]
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000D5870 File Offset: 0x000D3A70
		[__DynamicallyInvokable]
		public string ReplyAction
		{
			[__DynamicallyInvokable]
			get
			{
				return this.replyAction;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x000D5878 File Offset: 0x000D3A78
		// (set) Token: 0x06003763 RID: 14179 RVA: 0x000D5880 File Offset: 0x000D3A80
		[__DynamicallyInvokable]
		public bool SerializeRequest
		{
			[__DynamicallyInvokable]
			get
			{
				return this.serializeRequest;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.serializeRequest = value;
				}
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x000D58D4 File Offset: 0x000D3AD4
		// (set) Token: 0x06003765 RID: 14181 RVA: 0x000D58DC File Offset: 0x000D3ADC
		[__DynamicallyInvokable]
		public bool DeserializeReply
		{
			[__DynamicallyInvokable]
			get
			{
				return this.deserializeReply;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.deserializeReply = value;
				}
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000D5930 File Offset: 0x000D3B30
		// (set) Token: 0x06003767 RID: 14183 RVA: 0x000D5938 File Offset: 0x000D3B38
		[__DynamicallyInvokable]
		public MethodInfo TaskMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.taskMethod;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.taskMethod = value;
				}
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06003768 RID: 14184 RVA: 0x000D598C File Offset: 0x000D3B8C
		// (set) Token: 0x06003769 RID: 14185 RVA: 0x000D5994 File Offset: 0x000D3B94
		[__DynamicallyInvokable]
		public Type TaskTResult
		{
			[__DynamicallyInvokable]
			get
			{
				return this.taskTResult;
			}
			[__DynamicallyInvokable]
			set
			{
				object thisLock = this.parent.ThisLock;
				lock (thisLock)
				{
					this.parent.InvalidateRuntime();
					this.taskTResult = value;
				}
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000D59E8 File Offset: 0x000D3BE8
		// (set) Token: 0x0600376B RID: 14187 RVA: 0x000D59F0 File Offset: 0x000D3BF0
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

		// Token: 0x0400291C RID: 10524
		private string action;

		// Token: 0x0400291D RID: 10525
		private SynchronizedCollection<FaultContractInfo> faultContractInfos;

		// Token: 0x0400291E RID: 10526
		private bool serializeRequest;

		// Token: 0x0400291F RID: 10527
		private bool deserializeReply;

		// Token: 0x04002920 RID: 10528
		private IClientMessageFormatter formatter;

		// Token: 0x04002921 RID: 10529
		private IClientFaultFormatter faultFormatter;

		// Token: 0x04002922 RID: 10530
		private bool isInitiating = true;

		// Token: 0x04002923 RID: 10531
		private bool isOneWay;

		// Token: 0x04002924 RID: 10532
		private bool isTerminating;

		// Token: 0x04002925 RID: 10533
		private bool isSessionOpenNotificationEnabled;

		// Token: 0x04002926 RID: 10534
		private string name;

		// Token: 0x04002927 RID: 10535
		private ClientRuntime parent;

		// Token: 0x04002928 RID: 10536
		private string replyAction;

		// Token: 0x04002929 RID: 10537
		private MethodInfo beginMethod;

		// Token: 0x0400292A RID: 10538
		private MethodInfo endMethod;

		// Token: 0x0400292B RID: 10539
		private MethodInfo syncMethod;

		// Token: 0x0400292C RID: 10540
		private MethodInfo taskMethod;

		// Token: 0x0400292D RID: 10541
		private Type taskTResult;

		// Token: 0x0400292E RID: 10542
		private bool isFaultFormatterSetExplicit;
	}
}
