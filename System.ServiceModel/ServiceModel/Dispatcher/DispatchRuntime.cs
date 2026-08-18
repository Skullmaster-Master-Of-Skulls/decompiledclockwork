using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Policy;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.Threading;
using System.Web.Security;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000543 RID: 1347
	[__DynamicallyInvokable]
	public sealed class DispatchRuntime
	{
		// Token: 0x060032F1 RID: 13041 RVA: 0x000C501A File Offset: 0x000C321A
		internal DispatchRuntime(EndpointDispatcher endpointDispatcher) : this(new SharedRuntimeState(true))
		{
			if (endpointDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointDispatcher");
			}
			this.endpointDispatcher = endpointDispatcher;
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000C5044 File Offset: 0x000C3244
		internal DispatchRuntime(ClientRuntime proxyRuntime, SharedRuntimeState shared) : this(shared)
		{
			if (proxyRuntime == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("proxyRuntime");
			}
			this.proxyRuntime = proxyRuntime;
			this.instanceProvider = new DispatchRuntime.CallbackInstanceProvider();
			this.channelDispatcher = new ChannelDispatcher(shared);
			this.instanceContextProvider = InstanceContextProviderBase.GetProviderForMode(InstanceContextMode.PerSession, this);
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000C5098 File Offset: 0x000C3298
		private DispatchRuntime(SharedRuntimeState shared)
		{
			this.shared = shared;
			this.operations = new DispatchRuntime.OperationCollection(this);
			this.inputSessionShutdownHandlers = this.NewBehaviorCollection<IInputSessionShutdown>();
			this.messageInspectors = this.NewBehaviorCollection<IDispatchMessageInspector>();
			this.instanceContextInitializers = this.NewBehaviorCollection<IInstanceContextInitializer>();
			this.synchronizationContext = ThreadBehavior.GetCurrentSynchronizationContext();
			this.automaticInputSessionShutdown = true;
			this.principalPermissionMode = PrincipalPermissionMode.UseWindowsGroups;
			this.securityAuditLogLocation = AuditLogLocation.Default;
			this.suppressAuditFailure = true;
			this.serviceAuthorizationAuditLevel = AuditLevel.None;
			this.messageAuthenticationAuditLevel = AuditLevel.None;
			this.unhandled = new DispatchOperation(this, "*", "*", "*");
			this.unhandled.InternalFormatter = MessageOperationFormatter.Instance;
			this.unhandled.InternalInvoker = new DispatchRuntime.UnhandledActionInvoker(this);
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000C5153 File Offset: 0x000C3353
		// (set) Token: 0x060032F5 RID: 13045 RVA: 0x000C515C File Offset: 0x000C335C
		public IInstanceContextProvider InstanceContextProvider
		{
			get
			{
				return this.instanceContextProvider;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.instanceContextProvider = value;
				}
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000C51BC File Offset: 0x000C33BC
		// (set) Token: 0x060032F7 RID: 13047 RVA: 0x000C51C4 File Offset: 0x000C33C4
		public InstanceContext SingletonInstanceContext
		{
			get
			{
				return this.singleton;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.singleton = value;
				}
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000C5224 File Offset: 0x000C3424
		// (set) Token: 0x060032F9 RID: 13049 RVA: 0x000C522C File Offset: 0x000C342C
		public ConcurrencyMode ConcurrencyMode
		{
			get
			{
				return this.concurrencyMode;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.concurrencyMode = value;
				}
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000C5274 File Offset: 0x000C3474
		// (set) Token: 0x060032FB RID: 13051 RVA: 0x000C527C File Offset: 0x000C347C
		public bool EnsureOrderedDispatch
		{
			get
			{
				return this.ensureOrderedDispatch;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.ensureOrderedDispatch = value;
				}
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000C52C4 File Offset: 0x000C34C4
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x000C52CC File Offset: 0x000C34CC
		public AuditLogLocation SecurityAuditLogLocation
		{
			get
			{
				return this.securityAuditLogLocation;
			}
			set
			{
				if (!AuditLogLocationHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.securityAuditLogLocation = value;
				}
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x000C5330 File Offset: 0x000C3530
		// (set) Token: 0x060032FF RID: 13055 RVA: 0x000C5338 File Offset: 0x000C3538
		public bool SuppressAuditFailure
		{
			get
			{
				return this.suppressAuditFailure;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.suppressAuditFailure = value;
				}
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x000C5380 File Offset: 0x000C3580
		// (set) Token: 0x06003301 RID: 13057 RVA: 0x000C5388 File Offset: 0x000C3588
		public AuditLevel ServiceAuthorizationAuditLevel
		{
			get
			{
				return this.serviceAuthorizationAuditLevel;
			}
			set
			{
				if (!AuditLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.serviceAuthorizationAuditLevel = value;
				}
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003302 RID: 13058 RVA: 0x000C53EC File Offset: 0x000C35EC
		// (set) Token: 0x06003303 RID: 13059 RVA: 0x000C53F4 File Offset: 0x000C35F4
		public AuditLevel MessageAuthenticationAuditLevel
		{
			get
			{
				return this.messageAuthenticationAuditLevel;
			}
			set
			{
				if (!AuditLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.messageAuthenticationAuditLevel = value;
				}
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000C5458 File Offset: 0x000C3658
		// (set) Token: 0x06003305 RID: 13061 RVA: 0x000C5460 File Offset: 0x000C3660
		public ReadOnlyCollection<IAuthorizationPolicy> ExternalAuthorizationPolicies
		{
			get
			{
				return this.externalAuthorizationPolicies;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.externalAuthorizationPolicies = value;
					this.isExternalPoliciesSet = true;
				}
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x000C54B0 File Offset: 0x000C36B0
		// (set) Token: 0x06003307 RID: 13063 RVA: 0x000C54B8 File Offset: 0x000C36B8
		public ServiceAuthenticationManager ServiceAuthenticationManager
		{
			get
			{
				return this.serviceAuthenticationManager;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.serviceAuthenticationManager = value;
					this.isAuthenticationManagerSet = true;
				}
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x000C5508 File Offset: 0x000C3708
		// (set) Token: 0x06003309 RID: 13065 RVA: 0x000C5510 File Offset: 0x000C3710
		public ServiceAuthorizationManager ServiceAuthorizationManager
		{
			get
			{
				return this.serviceAuthorizationManager;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.serviceAuthorizationManager = value;
					this.isAuthorizationManagerSet = true;
				}
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000C5560 File Offset: 0x000C3760
		// (set) Token: 0x0600330B RID: 13067 RVA: 0x000C5568 File Offset: 0x000C3768
		public bool AutomaticInputSessionShutdown
		{
			get
			{
				return this.automaticInputSessionShutdown;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.automaticInputSessionShutdown = value;
				}
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000C55B0 File Offset: 0x000C37B0
		public ChannelDispatcher ChannelDispatcher
		{
			get
			{
				return this.channelDispatcher ?? this.endpointDispatcher.ChannelDispatcher;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x000C55C8 File Offset: 0x000C37C8
		public ClientRuntime CallbackClientRuntime
		{
			get
			{
				if (this.proxyRuntime == null)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.proxyRuntime == null)
						{
							this.proxyRuntime = new ClientRuntime(this, this.shared);
						}
					}
				}
				return this.proxyRuntime;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000C562C File Offset: 0x000C382C
		public EndpointDispatcher EndpointDispatcher
		{
			get
			{
				return this.endpointDispatcher;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000C5634 File Offset: 0x000C3834
		// (set) Token: 0x06003310 RID: 13072 RVA: 0x000C563C File Offset: 0x000C383C
		public bool ImpersonateCallerForAllOperations
		{
			get
			{
				return this.impersonateCallerForAllOperations;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.impersonateCallerForAllOperations = value;
				}
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000C5684 File Offset: 0x000C3884
		// (set) Token: 0x06003312 RID: 13074 RVA: 0x000C568C File Offset: 0x000C388C
		public bool ImpersonateOnSerializingReply
		{
			get
			{
				return this.impersonateOnSerializingReply;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.impersonateOnSerializingReply = value;
				}
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000C56D4 File Offset: 0x000C38D4
		// (set) Token: 0x06003314 RID: 13076 RVA: 0x000C56DC File Offset: 0x000C38DC
		internal bool RequireClaimsPrincipalOnOperationContext
		{
			get
			{
				return this.requireClaimsPrincipalOnOperationContext;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.requireClaimsPrincipalOnOperationContext = value;
				}
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000C5724 File Offset: 0x000C3924
		public SynchronizedCollection<IInputSessionShutdown> InputSessionShutdownHandlers
		{
			get
			{
				return this.inputSessionShutdownHandlers;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000C572C File Offset: 0x000C392C
		// (set) Token: 0x06003317 RID: 13079 RVA: 0x000C5734 File Offset: 0x000C3934
		public bool IgnoreTransactionMessageProperty
		{
			get
			{
				return this.ignoreTransactionMessageProperty;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.ignoreTransactionMessageProperty = value;
				}
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000C577C File Offset: 0x000C397C
		// (set) Token: 0x06003319 RID: 13081 RVA: 0x000C5784 File Offset: 0x000C3984
		public IInstanceProvider InstanceProvider
		{
			get
			{
				return this.instanceProvider;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.instanceProvider = value;
				}
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x000C57CC File Offset: 0x000C39CC
		public SynchronizedCollection<IDispatchMessageInspector> MessageInspectors
		{
			get
			{
				return this.messageInspectors;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000C57D4 File Offset: 0x000C39D4
		public SynchronizedKeyedCollection<string, DispatchOperation> Operations
		{
			get
			{
				return this.operations;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000C57DC File Offset: 0x000C39DC
		// (set) Token: 0x0600331D RID: 13085 RVA: 0x000C57E4 File Offset: 0x000C39E4
		public IDispatchOperationSelector OperationSelector
		{
			get
			{
				return this.operationSelector;
			}
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

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000C582C File Offset: 0x000C3A2C
		// (set) Token: 0x0600331F RID: 13087 RVA: 0x000C5834 File Offset: 0x000C3A34
		public bool ReleaseServiceInstanceOnTransactionComplete
		{
			get
			{
				return this.releaseServiceInstanceOnTransactionComplete;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.releaseServiceInstanceOnTransactionComplete = value;
				}
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000C587C File Offset: 0x000C3A7C
		public SynchronizedCollection<IInstanceContextInitializer> InstanceContextInitializers
		{
			get
			{
				return this.instanceContextInitializers;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x000C5884 File Offset: 0x000C3A84
		// (set) Token: 0x06003322 RID: 13090 RVA: 0x000C588C File Offset: 0x000C3A8C
		public SynchronizationContext SynchronizationContext
		{
			get
			{
				return this.synchronizationContext;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.synchronizationContext = value;
				}
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x000C58D4 File Offset: 0x000C3AD4
		// (set) Token: 0x06003324 RID: 13092 RVA: 0x000C58DC File Offset: 0x000C3ADC
		public PrincipalPermissionMode PrincipalPermissionMode
		{
			get
			{
				return this.principalPermissionMode;
			}
			set
			{
				if (!PrincipalPermissionModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.principalPermissionMode = value;
				}
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000C5940 File Offset: 0x000C3B40
		// (set) Token: 0x06003326 RID: 13094 RVA: 0x000C5950 File Offset: 0x000C3B50
		public RoleProvider RoleProvider
		{
			get
			{
				return (RoleProvider)this.roleProvider;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.roleProvider = value;
				}
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000C5998 File Offset: 0x000C3B98
		// (set) Token: 0x06003328 RID: 13096 RVA: 0x000C59A0 File Offset: 0x000C3BA0
		public bool TransactionAutoCompleteOnSessionClose
		{
			get
			{
				return this.transactionAutoCompleteOnSessionClose;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.transactionAutoCompleteOnSessionClose = value;
				}
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000C59E8 File Offset: 0x000C3BE8
		// (set) Token: 0x0600332A RID: 13098 RVA: 0x000C59F0 File Offset: 0x000C3BF0
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.type = value;
				}
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000C5A38 File Offset: 0x000C3C38
		// (set) Token: 0x0600332C RID: 13100 RVA: 0x000C5A40 File Offset: 0x000C3C40
		public DispatchOperation UnhandledDispatchOperation
		{
			get
			{
				return this.unhandled;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.unhandled = value;
				}
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000C5A9C File Offset: 0x000C3C9C
		// (set) Token: 0x0600332E RID: 13102 RVA: 0x000C5AAC File Offset: 0x000C3CAC
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

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000C5AF8 File Offset: 0x000C3CF8
		// (set) Token: 0x06003330 RID: 13104 RVA: 0x000C5B00 File Offset: 0x000C3D00
		public bool PreserveMessage
		{
			get
			{
				return this.preserveMessage;
			}
			set
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.InvalidateRuntime();
					this.preserveMessage = value;
				}
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x000C5B48 File Offset: 0x000C3D48
		internal bool RequiresAuthentication
		{
			get
			{
				return this.isAuthenticationManagerSet;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000C5B50 File Offset: 0x000C3D50
		internal bool RequiresAuthorization
		{
			get
			{
				return this.isAuthorizationManagerSet || this.isExternalPoliciesSet || AuditLevel.Success == (this.serviceAuthorizationAuditLevel & AuditLevel.Success);
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000C5B70 File Offset: 0x000C3D70
		internal bool HasMatchAllOperation
		{
			get
			{
				object thisLock = this.ThisLock;
				bool result;
				lock (thisLock)
				{
					result = !(this.unhandled.Invoker is DispatchRuntime.UnhandledActionInvoker);
				}
				return result;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000C5BC4 File Offset: 0x000C3DC4
		internal bool EnableFaults
		{
			get
			{
				if (this.IsOnServer)
				{
					ChannelDispatcher channelDispatcher = this.ChannelDispatcher;
					return channelDispatcher != null && channelDispatcher.EnableFaults;
				}
				return this.shared.EnableFaults;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x000C5BF7 File Offset: 0x000C3DF7
		internal bool IsOnServer
		{
			get
			{
				return this.shared.IsOnServer;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000C5C04 File Offset: 0x000C3E04
		internal bool ManualAddressing
		{
			get
			{
				if (this.IsOnServer)
				{
					ChannelDispatcher channelDispatcher = this.ChannelDispatcher;
					return channelDispatcher != null && channelDispatcher.ManualAddressing;
				}
				return this.shared.ManualAddressing;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x000C5C38 File Offset: 0x000C3E38
		internal int MaxCallContextInitializers
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
						num = Math.Max(num, this.operations[i].CallContextInitializers.Count);
					}
					num = Math.Max(num, this.unhandled.CallContextInitializers.Count);
					result = num;
				}
				return result;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000C5CC4 File Offset: 0x000C3EC4
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
					num = Math.Max(num, this.unhandled.ParameterInspectors.Count);
					result = num;
				}
				return result;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000C5D50 File Offset: 0x000C3F50
		internal ClientRuntime ClientRuntime
		{
			get
			{
				return this.proxyRuntime;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000C5D58 File Offset: 0x000C3F58
		internal object ThisLock
		{
			get
			{
				return this.shared;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000C5D60 File Offset: 0x000C3F60
		internal bool IsRoleProviderSet
		{
			get
			{
				return this.roleProvider != null;
			}
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000C5D6C File Offset: 0x000C3F6C
		internal DispatchOperationRuntime GetOperation(ref Message message)
		{
			ImmutableDispatchRuntime immutableDispatchRuntime = this.GetRuntime();
			return immutableDispatchRuntime.GetOperation(ref message);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000C5D88 File Offset: 0x000C3F88
		internal ImmutableDispatchRuntime GetRuntime()
		{
			ImmutableDispatchRuntime immutableDispatchRuntime = this.runtime;
			if (immutableDispatchRuntime != null)
			{
				return immutableDispatchRuntime;
			}
			return this.GetRuntimeCore();
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000C5DA8 File Offset: 0x000C3FA8
		private ImmutableDispatchRuntime GetRuntimeCore()
		{
			object thisLock = this.ThisLock;
			ImmutableDispatchRuntime result;
			lock (thisLock)
			{
				if (this.runtime == null)
				{
					this.runtime = new ImmutableDispatchRuntime(this);
				}
				result = this.runtime;
			}
			return result;
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000C5E00 File Offset: 0x000C4000
		internal void InvalidateRuntime()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.shared.ThrowIfImmutable();
				this.runtime = null;
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000C5E4C File Offset: 0x000C404C
		internal void LockDownProperties()
		{
			this.shared.LockDownProperties();
			if (this.concurrencyMode != ConcurrencyMode.Single && this.ensureOrderedDispatch)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxDispatchRuntimeNonConcurrentOrEnsureOrderedDispatch")));
			}
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000C5E83 File Offset: 0x000C4083
		internal SynchronizedCollection<T> NewBehaviorCollection<T>()
		{
			return new DispatchRuntime.DispatchBehaviorCollection<T>(this);
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000C5E8C File Offset: 0x000C408C
		internal void SetDebugFlagInDispatchOperations(bool includeExceptionDetailInFaults)
		{
			foreach (DispatchOperation dispatchOperation in this.operations)
			{
				dispatchOperation.IncludeExceptionDetailInFaults = includeExceptionDetailInFaults;
			}
		}

		// Token: 0x04002752 RID: 10066
		private ServiceAuthenticationManager serviceAuthenticationManager;

		// Token: 0x04002753 RID: 10067
		private ServiceAuthorizationManager serviceAuthorizationManager;

		// Token: 0x04002754 RID: 10068
		private ReadOnlyCollection<IAuthorizationPolicy> externalAuthorizationPolicies;

		// Token: 0x04002755 RID: 10069
		private AuditLogLocation securityAuditLogLocation;

		// Token: 0x04002756 RID: 10070
		private ConcurrencyMode concurrencyMode;

		// Token: 0x04002757 RID: 10071
		private bool ensureOrderedDispatch;

		// Token: 0x04002758 RID: 10072
		private bool suppressAuditFailure;

		// Token: 0x04002759 RID: 10073
		private AuditLevel serviceAuthorizationAuditLevel;

		// Token: 0x0400275A RID: 10074
		private AuditLevel messageAuthenticationAuditLevel;

		// Token: 0x0400275B RID: 10075
		private bool automaticInputSessionShutdown;

		// Token: 0x0400275C RID: 10076
		private ChannelDispatcher channelDispatcher;

		// Token: 0x0400275D RID: 10077
		private SynchronizedCollection<IInputSessionShutdown> inputSessionShutdownHandlers;

		// Token: 0x0400275E RID: 10078
		private EndpointDispatcher endpointDispatcher;

		// Token: 0x0400275F RID: 10079
		private IInstanceProvider instanceProvider;

		// Token: 0x04002760 RID: 10080
		private IInstanceContextProvider instanceContextProvider;

		// Token: 0x04002761 RID: 10081
		private InstanceContext singleton;

		// Token: 0x04002762 RID: 10082
		private bool ignoreTransactionMessageProperty;

		// Token: 0x04002763 RID: 10083
		private SynchronizedCollection<IDispatchMessageInspector> messageInspectors;

		// Token: 0x04002764 RID: 10084
		private DispatchRuntime.OperationCollection operations;

		// Token: 0x04002765 RID: 10085
		private IDispatchOperationSelector operationSelector;

		// Token: 0x04002766 RID: 10086
		private ClientRuntime proxyRuntime;

		// Token: 0x04002767 RID: 10087
		private ImmutableDispatchRuntime runtime;

		// Token: 0x04002768 RID: 10088
		private SynchronizedCollection<IInstanceContextInitializer> instanceContextInitializers;

		// Token: 0x04002769 RID: 10089
		private bool isExternalPoliciesSet;

		// Token: 0x0400276A RID: 10090
		private bool isAuthenticationManagerSet;

		// Token: 0x0400276B RID: 10091
		private bool isAuthorizationManagerSet;

		// Token: 0x0400276C RID: 10092
		private SynchronizationContext synchronizationContext;

		// Token: 0x0400276D RID: 10093
		private PrincipalPermissionMode principalPermissionMode;

		// Token: 0x0400276E RID: 10094
		private object roleProvider;

		// Token: 0x0400276F RID: 10095
		private Type type;

		// Token: 0x04002770 RID: 10096
		private DispatchOperation unhandled;

		// Token: 0x04002771 RID: 10097
		private bool transactionAutoCompleteOnSessionClose;

		// Token: 0x04002772 RID: 10098
		private bool impersonateCallerForAllOperations;

		// Token: 0x04002773 RID: 10099
		private bool impersonateOnSerializingReply;

		// Token: 0x04002774 RID: 10100
		private bool releaseServiceInstanceOnTransactionComplete;

		// Token: 0x04002775 RID: 10101
		private SharedRuntimeState shared;

		// Token: 0x04002776 RID: 10102
		private bool preserveMessage;

		// Token: 0x04002777 RID: 10103
		private bool requireClaimsPrincipalOnOperationContext;

		// Token: 0x02000C6A RID: 3178
		internal class UnhandledActionInvoker : IOperationInvoker
		{
			// Token: 0x060077E4 RID: 30692 RVA: 0x001C0A3F File Offset: 0x001BEC3F
			public UnhandledActionInvoker(DispatchRuntime dispatchRuntime)
			{
				this.dispatchRuntime = dispatchRuntime;
			}

			// Token: 0x17001B57 RID: 6999
			// (get) Token: 0x060077E5 RID: 30693 RVA: 0x001C0A4E File Offset: 0x001BEC4E
			public bool IsSynchronous
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060077E6 RID: 30694 RVA: 0x001C0A51 File Offset: 0x001BEC51
			public object[] AllocateInputs()
			{
				return new object[1];
			}

			// Token: 0x060077E7 RID: 30695 RVA: 0x001C0A5C File Offset: 0x001BEC5C
			public object Invoke(object instance, object[] inputs, out object[] outputs)
			{
				outputs = EmptyArray<object>.Allocate(0);
				Message message = inputs[0] as Message;
				if (message == null)
				{
					return null;
				}
				string action = message.Headers.Action;
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 524343, SR.GetString("TraceCodeUnhandledAction"), new StringTraceRecord("Action", action), this, null, message);
				}
				FaultCode code = FaultCode.CreateSenderFaultCode("ActionNotSupported", message.Version.Addressing.Namespace);
				string @string = SR.GetString("SFxNoEndpointMatchingContract", new object[]
				{
					action
				});
				FaultReason reason = new FaultReason(@string);
				FaultException exception = new FaultException(reason, code);
				ErrorBehavior.ThrowAndCatch(exception);
				ServiceChannel serviceChannel = OperationContext.Current.InternalServiceChannel;
				OperationContext.Current.OperationCompleted += delegate(object sender, EventArgs e)
				{
					ChannelDispatcher channelDispatcher = this.dispatchRuntime.ChannelDispatcher;
					if (!channelDispatcher.HandleError(exception) && serviceChannel.HasSession)
					{
						try
						{
							serviceChannel.Close(ChannelHandler.CloseAfterFaultTimeout);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							channelDispatcher.HandleError(ex);
						}
					}
				};
				if (this.dispatchRuntime.shared.EnableFaults)
				{
					MessageFault fault = MessageFault.CreateFault(code, reason, action);
					return Message.CreateMessage(message.Version, fault, message.Version.Addressing.DefaultFaultAction);
				}
				OperationContext.Current.RequestContext.Close();
				OperationContext.Current.RequestContext = null;
				return null;
			}

			// Token: 0x060077E8 RID: 30696 RVA: 0x001C0B91 File Offset: 0x001BED91
			public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x060077E9 RID: 30697 RVA: 0x001C0BA2 File Offset: 0x001BEDA2
			public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x0400447A RID: 17530
			private DispatchRuntime dispatchRuntime;
		}

		// Token: 0x02000C6B RID: 3179
		private class DispatchBehaviorCollection<T> : SynchronizedCollection<T>
		{
			// Token: 0x060077EA RID: 30698 RVA: 0x001C0BB3 File Offset: 0x001BEDB3
			internal DispatchBehaviorCollection(DispatchRuntime outer) : base(outer.ThisLock)
			{
				this.outer = outer;
			}

			// Token: 0x060077EB RID: 30699 RVA: 0x001C0BC8 File Offset: 0x001BEDC8
			protected override void ClearItems()
			{
				this.outer.InvalidateRuntime();
				base.ClearItems();
			}

			// Token: 0x060077EC RID: 30700 RVA: 0x001C0BDB File Offset: 0x001BEDDB
			protected override void InsertItem(int index, T item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				this.outer.InvalidateRuntime();
				base.InsertItem(index, item);
			}

			// Token: 0x060077ED RID: 30701 RVA: 0x001C0C08 File Offset: 0x001BEE08
			protected override void RemoveItem(int index)
			{
				this.outer.InvalidateRuntime();
				base.RemoveItem(index);
			}

			// Token: 0x060077EE RID: 30702 RVA: 0x001C0C1C File Offset: 0x001BEE1C
			protected override void SetItem(int index, T item)
			{
				if (item == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
				}
				this.outer.InvalidateRuntime();
				base.SetItem(index, item);
			}

			// Token: 0x0400447B RID: 17531
			private DispatchRuntime outer;
		}

		// Token: 0x02000C6C RID: 3180
		private class OperationCollection : SynchronizedKeyedCollection<string, DispatchOperation>
		{
			// Token: 0x060077EF RID: 30703 RVA: 0x001C0C49 File Offset: 0x001BEE49
			internal OperationCollection(DispatchRuntime outer) : base(outer.ThisLock)
			{
				this.outer = outer;
			}

			// Token: 0x060077F0 RID: 30704 RVA: 0x001C0C5E File Offset: 0x001BEE5E
			protected override void ClearItems()
			{
				this.outer.InvalidateRuntime();
				base.ClearItems();
			}

			// Token: 0x060077F1 RID: 30705 RVA: 0x001C0C71 File Offset: 0x001BEE71
			protected override string GetKeyForItem(DispatchOperation item)
			{
				return item.Name;
			}

			// Token: 0x060077F2 RID: 30706 RVA: 0x001C0C7C File Offset: 0x001BEE7C
			protected override void InsertItem(int index, DispatchOperation item)
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

			// Token: 0x060077F3 RID: 30707 RVA: 0x001C0CD2 File Offset: 0x001BEED2
			protected override void RemoveItem(int index)
			{
				this.outer.InvalidateRuntime();
				base.RemoveItem(index);
			}

			// Token: 0x060077F4 RID: 30708 RVA: 0x001C0CE8 File Offset: 0x001BEEE8
			protected override void SetItem(int index, DispatchOperation item)
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

			// Token: 0x0400447C RID: 17532
			private DispatchRuntime outer;
		}

		// Token: 0x02000C6D RID: 3181
		private class CallbackInstanceProvider : IInstanceProvider
		{
			// Token: 0x060077F5 RID: 30709 RVA: 0x001C0D3E File Offset: 0x001BEF3E
			object IInstanceProvider.GetInstance(InstanceContext instanceContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCannotActivateCallbackInstace")));
			}

			// Token: 0x060077F6 RID: 30710 RVA: 0x001C0D59 File Offset: 0x001BEF59
			object IInstanceProvider.GetInstance(InstanceContext instanceContext, Message message)
			{
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCannotActivateCallbackInstace")), message);
			}

			// Token: 0x060077F7 RID: 30711 RVA: 0x001C0D70 File Offset: 0x001BEF70
			void IInstanceProvider.ReleaseInstance(InstanceContext instanceContext, object instance)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCannotActivateCallbackInstace")));
			}
		}
	}
}
