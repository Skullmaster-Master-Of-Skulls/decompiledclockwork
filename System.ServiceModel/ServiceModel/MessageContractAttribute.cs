using System;
using System.Net.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000D5 RID: 213
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class MessageContractAttribute : Attribute
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00015718 File Offset: 0x00013918
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00015720 File Offset: 0x00013920
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

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001574D File Offset: 0x0001394D
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00015755 File Offset: 0x00013955
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0001575D File Offset: 0x0001395D
		[__DynamicallyInvokable]
		public bool IsWrapped
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isWrapped;
			}
			[__DynamicallyInvokable]
			set
			{
				this.isWrapped = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00015766 File Offset: 0x00013966
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00015770 File Offset: 0x00013970
		[__DynamicallyInvokable]
		public string WrapperName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.wrappedName;
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
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxWrapperNameCannotBeEmpty")));
				}
				this.wrappedName = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000157C3 File Offset: 0x000139C3
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x000157CB File Offset: 0x000139CB
		[__DynamicallyInvokable]
		public string WrapperNamespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.wrappedNs;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					NamingHelper.CheckUriProperty(value, "WrapperNamespace");
				}
				this.wrappedNs = value;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000157E7 File Offset: 0x000139E7
		[__DynamicallyInvokable]
		public MessageContractAttribute()
		{
		}

		// Token: 0x040009AD RID: 2477
		private bool isWrapped = true;

		// Token: 0x040009AE RID: 2478
		private string wrappedName;

		// Token: 0x040009AF RID: 2479
		private string wrappedNs;

		// Token: 0x040009B0 RID: 2480
		private ProtectionLevel protectionLevel;

		// Token: 0x040009B1 RID: 2481
		private bool hasProtectionLevel;

		// Token: 0x040009B2 RID: 2482
		internal const string ProtectionLevelPropertyName = "ProtectionLevel";
	}
}
