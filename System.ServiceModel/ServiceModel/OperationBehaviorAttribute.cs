using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel
{
	// Token: 0x020000DB RID: 219
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class OperationBehaviorAttribute : Attribute, IOperationBehavior
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000159EA File Offset: 0x00013BEA
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x000159F2 File Offset: 0x00013BF2
		public bool TransactionAutoComplete
		{
			get
			{
				return this.autoCompleteTransaction;
			}
			set
			{
				this.autoCompleteTransaction = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000159FB File Offset: 0x00013BFB
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x00015A03 File Offset: 0x00013C03
		public bool TransactionScopeRequired
		{
			get
			{
				return this.autoEnlistTransaction;
			}
			set
			{
				this.autoEnlistTransaction = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00015A0C File Offset: 0x00013C0C
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00015A14 File Offset: 0x00013C14
		public bool AutoDisposeParameters
		{
			get
			{
				return this.autoDisposeParameters;
			}
			set
			{
				this.autoDisposeParameters = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00015A1D File Offset: 0x00013C1D
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00015A25 File Offset: 0x00013C25
		public ImpersonationOption Impersonation
		{
			get
			{
				return this.impersonation;
			}
			set
			{
				if (!ImpersonationOptionHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.impersonation = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00015A4B File Offset: 0x00013C4B
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00015A53 File Offset: 0x00013C53
		public ReleaseInstanceMode ReleaseInstanceMode
		{
			get
			{
				return this.releaseInstance;
			}
			set
			{
				if (!ReleaseInstanceModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.releaseInstance = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00015A79 File Offset: 0x00013C79
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00015A81 File Offset: 0x00013C81
		internal bool PreferAsyncInvocation
		{
			get
			{
				return this.preferAsyncInvocation;
			}
			set
			{
				this.preferAsyncInvocation = value;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00015A8A File Offset: 0x00013C8A
		void IOperationBehavior.Validate(OperationDescription description)
		{
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00015A8C File Offset: 0x00013C8C
		void IOperationBehavior.AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00015A90 File Offset: 0x00013C90
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (dispatch == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dispatch");
			}
			if (description.IsServerInitiated() && this.releaseInstance != ReleaseInstanceMode.None)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxOperationBehaviorAttributeReleaseInstanceModeDoesNotApplyToCallback", new object[]
				{
					description.Name
				})));
			}
			dispatch.TransactionRequired = this.autoEnlistTransaction;
			dispatch.TransactionAutoComplete = this.autoCompleteTransaction;
			dispatch.AutoDisposeParameters = this.autoDisposeParameters;
			dispatch.ReleaseInstanceBeforeCall = ((this.releaseInstance & ReleaseInstanceMode.BeforeCall) > ReleaseInstanceMode.None);
			dispatch.ReleaseInstanceAfterCall = ((this.releaseInstance & ReleaseInstanceMode.AfterCall) > ReleaseInstanceMode.None);
			dispatch.Impersonation = this.Impersonation;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00015B4E File Offset: 0x00013D4E
		void IOperationBehavior.ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
		{
		}

		// Token: 0x040009C6 RID: 2502
		internal const ImpersonationOption DefaultImpersonationOption = ImpersonationOption.NotAllowed;

		// Token: 0x040009C7 RID: 2503
		private bool autoCompleteTransaction = true;

		// Token: 0x040009C8 RID: 2504
		private bool autoEnlistTransaction;

		// Token: 0x040009C9 RID: 2505
		private bool autoDisposeParameters = true;

		// Token: 0x040009CA RID: 2506
		private bool preferAsyncInvocation;

		// Token: 0x040009CB RID: 2507
		private ImpersonationOption impersonation;

		// Token: 0x040009CC RID: 2508
		private ReleaseInstanceMode releaseInstance;
	}
}
