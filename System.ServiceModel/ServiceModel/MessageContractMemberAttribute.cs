using System;
using System.Net.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000D6 RID: 214
	[__DynamicallyInvokable]
	public abstract class MessageContractMemberAttribute : Attribute
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000157F6 File Offset: 0x000139F6
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x000157FE File Offset: 0x000139FE
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
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value.Length > 0)
				{
					NamingHelper.CheckUriProperty(value, "Namespace");
				}
				this.ns = value;
				this.isNamespaceSetExplicit = true;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00015835 File Offset: 0x00013A35
		internal bool IsNamespaceSetExplicit
		{
			get
			{
				return this.isNamespaceSetExplicit;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001583D File Offset: 0x00013A3D
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00015848 File Offset: 0x00013A48
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
				this.isNameSetExplicit = true;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000158A2 File Offset: 0x00013AA2
		internal bool IsNameSetExplicit
		{
			get
			{
				return this.isNameSetExplicit;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000158AA File Offset: 0x00013AAA
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x000158B2 File Offset: 0x00013AB2
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000158DF File Offset: 0x00013ADF
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000158E7 File Offset: 0x00013AE7
		[__DynamicallyInvokable]
		protected MessageContractMemberAttribute()
		{
		}

		// Token: 0x040009B3 RID: 2483
		private string name;

		// Token: 0x040009B4 RID: 2484
		private string ns;

		// Token: 0x040009B5 RID: 2485
		private bool isNameSetExplicit;

		// Token: 0x040009B6 RID: 2486
		private bool isNamespaceSetExplicit;

		// Token: 0x040009B7 RID: 2487
		private ProtectionLevel protectionLevel;

		// Token: 0x040009B8 RID: 2488
		private bool hasProtectionLevel;

		// Token: 0x040009B9 RID: 2489
		internal const string NamespacePropertyName = "Namespace";

		// Token: 0x040009BA RID: 2490
		internal const string NamePropertyName = "Name";

		// Token: 0x040009BB RID: 2491
		internal const string ProtectionLevelPropertyName = "ProtectionLevel";
	}
}
