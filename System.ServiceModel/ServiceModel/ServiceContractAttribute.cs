using System;
using System.Net.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000E0 RID: 224
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class ServiceContractAttribute : Attribute
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000164D0 File Offset: 0x000146D0
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x000164D8 File Offset: 0x000146D8
		[__DynamicallyInvokable]
		public string ConfigurationName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.configurationName;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value == string.Empty)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxConfigurationNameCannotBeEmpty")));
				}
				this.configurationName = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001652B File Offset: 0x0001472B
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00016534 File Offset: 0x00014734
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
				if (value == string.Empty)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxNameCannotBeEmpty")));
				}
				this.name = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00016587 File Offset: 0x00014787
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0001658F File Offset: 0x0001478F
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					NamingHelper.CheckUriProperty(value, "Namespace");
				}
				this.ns = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000165AB File Offset: 0x000147AB
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x000165B3 File Offset: 0x000147B3
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000165E0 File Offset: 0x000147E0
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000165E8 File Offset: 0x000147E8
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x000165F0 File Offset: 0x000147F0
		public SessionMode SessionMode
		{
			get
			{
				return this.sessionMode;
			}
			set
			{
				if (!SessionModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.sessionMode = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00016616 File Offset: 0x00014816
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0001661E File Offset: 0x0001481E
		[__DynamicallyInvokable]
		public Type CallbackContract
		{
			[__DynamicallyInvokable]
			get
			{
				return this.callbackContract;
			}
			[__DynamicallyInvokable]
			set
			{
				this.callbackContract = value;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00016627 File Offset: 0x00014827
		[__DynamicallyInvokable]
		public ServiceContractAttribute()
		{
		}

		// Token: 0x040009F4 RID: 2548
		private Type callbackContract;

		// Token: 0x040009F5 RID: 2549
		private string configurationName;

		// Token: 0x040009F6 RID: 2550
		private string name;

		// Token: 0x040009F7 RID: 2551
		private string ns;

		// Token: 0x040009F8 RID: 2552
		private SessionMode sessionMode;

		// Token: 0x040009F9 RID: 2553
		private ProtectionLevel protectionLevel;

		// Token: 0x040009FA RID: 2554
		private bool hasProtectionLevel;
	}
}
