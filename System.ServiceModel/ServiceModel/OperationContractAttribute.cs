using System;
using System.Net.Security;
using System.Reflection;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000DC RID: 220
	[AttributeUsage(AttributeTargets.Method)]
	[__DynamicallyInvokable]
	public sealed class OperationContractAttribute : Attribute
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00015B66 File Offset: 0x00013D66
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x00015B70 File Offset: 0x00013D70
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value == "")
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxNameCannotBeEmpty")));
				}
				this.name = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00015BC3 File Offset: 0x00013DC3
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x00015BCB File Offset: 0x00013DCB
		[__DynamicallyInvokable]
		public string Action
		{
			[__DynamicallyInvokable]
			get
			{
				return this.action;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.action = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00015BE7 File Offset: 0x00013DE7
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00015BEF File Offset: 0x00013DEF
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.protectionLevel = value;
				this.hasProtectionLevel = true;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00015C1C File Offset: 0x00013E1C
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00015C24 File Offset: 0x00013E24
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00015C2C File Offset: 0x00013E2C
		[__DynamicallyInvokable]
		public string ReplyAction
		{
			[__DynamicallyInvokable]
			get
			{
				return this.replyAction;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.replyAction = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00015C48 File Offset: 0x00013E48
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00015C50 File Offset: 0x00013E50
		[__DynamicallyInvokable]
		public bool AsyncPattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.asyncPattern;
			}
			[__DynamicallyInvokable]
			set
			{
				this.asyncPattern = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00015C59 File Offset: 0x00013E59
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x00015C61 File Offset: 0x00013E61
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
				this.isOneWay = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00015C6A File Offset: 0x00013E6A
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00015C72 File Offset: 0x00013E72
		public bool IsInitiating
		{
			get
			{
				return this.isInitiating;
			}
			set
			{
				this.isInitiating = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00015C7B File Offset: 0x00013E7B
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x00015C83 File Offset: 0x00013E83
		public bool IsTerminating
		{
			get
			{
				return this.isTerminating;
			}
			set
			{
				this.isTerminating = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00015C8C File Offset: 0x00013E8C
		internal bool IsSessionOpenNotificationEnabled
		{
			get
			{
				return this.Action == "http://schemas.microsoft.com/2011/02/session/onopen";
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00015CA0 File Offset: 0x00013EA0
		internal void EnsureInvariants(MethodInfo methodInfo, string operationName)
		{
			if (this.IsSessionOpenNotificationEnabled && (!this.IsOneWay || !this.IsInitiating || methodInfo.GetParameters().Length != 0))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContractIsNotSelfConsistentWhenIsSessionOpenNotificationEnabled", new object[]
				{
					operationName,
					"Action",
					"http://schemas.microsoft.com/2011/02/session/onopen",
					"IsOneWay",
					"IsInitiating"
				})));
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00015D12 File Offset: 0x00013F12
		[__DynamicallyInvokable]
		public OperationContractAttribute()
		{
		}

		// Token: 0x040009CD RID: 2509
		private string name;

		// Token: 0x040009CE RID: 2510
		private string action;

		// Token: 0x040009CF RID: 2511
		private string replyAction;

		// Token: 0x040009D0 RID: 2512
		private bool asyncPattern;

		// Token: 0x040009D1 RID: 2513
		private bool isInitiating = true;

		// Token: 0x040009D2 RID: 2514
		private bool isTerminating;

		// Token: 0x040009D3 RID: 2515
		private bool isOneWay;

		// Token: 0x040009D4 RID: 2516
		private ProtectionLevel protectionLevel;

		// Token: 0x040009D5 RID: 2517
		private bool hasProtectionLevel;

		// Token: 0x040009D6 RID: 2518
		internal const string ActionPropertyName = "Action";

		// Token: 0x040009D7 RID: 2519
		internal const string ProtectionLevelPropertyName = "ProtectionLevel";

		// Token: 0x040009D8 RID: 2520
		internal const string ReplyActionPropertyName = "ReplyAction";
	}
}
