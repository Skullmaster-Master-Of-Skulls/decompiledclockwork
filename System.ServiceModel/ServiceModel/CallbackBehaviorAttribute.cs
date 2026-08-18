using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Transactions;

namespace System.ServiceModel
{
	// Token: 0x020000CF RID: 207
	[AttributeUsage(AttributeTargets.Class)]
	[__DynamicallyInvokable]
	public sealed class CallbackBehaviorAttribute : Attribute, IEndpointBehavior
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00015267 File Offset: 0x00013467
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0001526F File Offset: 0x0001346F
		[__DynamicallyInvokable]
		public bool AutomaticSessionShutdown
		{
			[__DynamicallyInvokable]
			get
			{
				return this.automaticSessionShutdown;
			}
			[__DynamicallyInvokable]
			set
			{
				this.automaticSessionShutdown = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00015278 File Offset: 0x00013478
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00015280 File Offset: 0x00013480
		public IsolationLevel TransactionIsolationLevel
		{
			get
			{
				return this.transactionIsolationLevel;
			}
			set
			{
				if (value > IsolationLevel.Unspecified)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.transactionIsolationLevel = value;
				this.isolationLevelSet = true;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000152A9 File Offset: 0x000134A9
		internal bool IsolationLevelSet
		{
			get
			{
				return this.isolationLevelSet;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000152B1 File Offset: 0x000134B1
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x000152B9 File Offset: 0x000134B9
		public bool IncludeExceptionDetailInFaults
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

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x000152C2 File Offset: 0x000134C2
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x000152CA File Offset: 0x000134CA
		public ConcurrencyMode ConcurrencyMode
		{
			get
			{
				return this.concurrencyMode;
			}
			set
			{
				if (!ConcurrencyModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.concurrencyMode = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x000152F0 File Offset: 0x000134F0
		// (set) Token: 0x060003BA RID: 954 RVA: 0x000152F8 File Offset: 0x000134F8
		public string TransactionTimeout
		{
			get
			{
				return this.transactionTimeoutString;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				try
				{
					TimeSpan t = TimeSpan.Parse(value, CultureInfo.InvariantCulture);
					if (t < TimeSpan.Zero)
					{
						string @string = SR.GetString("SFxTimeoutOutOfRange0");
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, @string));
					}
					this.transactionTimeout = t;
					this.transactionTimeoutString = value;
					this.transactionTimeoutSet = true;
				}
				catch (FormatException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxTimeoutInvalidStringFormat"), "value", innerException));
				}
				catch (OverflowException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000153C4 File Offset: 0x000135C4
		internal bool TransactionTimeoutSet
		{
			get
			{
				return this.transactionTimeoutSet;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000153CC File Offset: 0x000135CC
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000153D4 File Offset: 0x000135D4
		[__DynamicallyInvokable]
		public bool UseSynchronizationContext
		{
			[__DynamicallyInvokable]
			get
			{
				return this.useSynchronizationContext;
			}
			[__DynamicallyInvokable]
			set
			{
				this.useSynchronizationContext = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000153DD File Offset: 0x000135DD
		// (set) Token: 0x060003BF RID: 959 RVA: 0x000153E5 File Offset: 0x000135E5
		public bool ValidateMustUnderstand
		{
			get
			{
				return this.validateMustUnderstand;
			}
			set
			{
				this.validateMustUnderstand = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000153EE File Offset: 0x000135EE
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x000153F6 File Offset: 0x000135F6
		public bool IgnoreExtensionDataObject
		{
			get
			{
				return this.ignoreExtensionDataObject;
			}
			set
			{
				this.ignoreExtensionDataObject = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x000153FF File Offset: 0x000135FF
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00015407 File Offset: 0x00013607
		public int MaxItemsInObjectGraph
		{
			get
			{
				return this.maxItemsInObjectGraph;
			}
			set
			{
				this.maxItemsInObjectGraph = value;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00015410 File Offset: 0x00013610
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetIsolationLevel(ChannelDispatcher channelDispatcher)
		{
			if (channelDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelDispatcher");
			}
			channelDispatcher.TransactionIsolationLevel = this.transactionIsolationLevel;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00015431 File Offset: 0x00013631
		[__DynamicallyInvokable]
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00015433 File Offset: 0x00013633
		[__DynamicallyInvokable]
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00015438 File Offset: 0x00013638
		[__DynamicallyInvokable]
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime clientRuntime)
		{
			if (!serviceEndpoint.Contract.IsDuplex())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCallbackBehaviorAttributeOnlyOnDuplex", new object[]
				{
					serviceEndpoint.Contract.Name
				})));
			}
			DispatchRuntime dispatchRuntime = clientRuntime.DispatchRuntime;
			dispatchRuntime.ValidateMustUnderstand = this.validateMustUnderstand;
			dispatchRuntime.ConcurrencyMode = this.concurrencyMode;
			dispatchRuntime.ChannelDispatcher.IncludeExceptionDetailInFaults = this.includeExceptionDetailInFaults;
			dispatchRuntime.AutomaticInputSessionShutdown = this.automaticSessionShutdown;
			if (!this.useSynchronizationContext)
			{
				dispatchRuntime.SynchronizationContext = null;
			}
			dispatchRuntime.ChannelDispatcher.TransactionTimeout = this.transactionTimeout;
			if (this.isolationLevelSet)
			{
				this.SetIsolationLevel(dispatchRuntime.ChannelDispatcher);
			}
			DataContractSerializerServiceBehavior.ApplySerializationSettings(serviceEndpoint, this.ignoreExtensionDataObject, this.maxItemsInObjectGraph);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00015502 File Offset: 0x00013702
		[__DynamicallyInvokable]
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFXEndpointBehaviorUsedOnWrongSide", new object[]
			{
				typeof(CallbackBehaviorAttribute).Name
			})));
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00015535 File Offset: 0x00013735
		[__DynamicallyInvokable]
		public CallbackBehaviorAttribute()
		{
		}

		// Token: 0x04000992 RID: 2450
		private ConcurrencyMode concurrencyMode;

		// Token: 0x04000993 RID: 2451
		private bool includeExceptionDetailInFaults;

		// Token: 0x04000994 RID: 2452
		private bool validateMustUnderstand = true;

		// Token: 0x04000995 RID: 2453
		private bool ignoreExtensionDataObject;

		// Token: 0x04000996 RID: 2454
		private int maxItemsInObjectGraph = int.MaxValue;

		// Token: 0x04000997 RID: 2455
		private bool automaticSessionShutdown = true;

		// Token: 0x04000998 RID: 2456
		private bool useSynchronizationContext = true;

		// Token: 0x04000999 RID: 2457
		internal static IsolationLevel DefaultIsolationLevel = IsolationLevel.Unspecified;

		// Token: 0x0400099A RID: 2458
		private IsolationLevel transactionIsolationLevel = CallbackBehaviorAttribute.DefaultIsolationLevel;

		// Token: 0x0400099B RID: 2459
		private bool isolationLevelSet;

		// Token: 0x0400099C RID: 2460
		private TimeSpan transactionTimeout = TimeSpan.Zero;

		// Token: 0x0400099D RID: 2461
		private string transactionTimeoutString;

		// Token: 0x0400099E RID: 2462
		private bool transactionTimeoutSet;
	}
}
