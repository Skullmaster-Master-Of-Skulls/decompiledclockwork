using System;
using System.Net.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000D1 RID: 209
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class FaultContractAttribute : Attribute
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0001559F File Offset: 0x0001379F
		[__DynamicallyInvokable]
		public FaultContractAttribute(Type detailType)
		{
			if (detailType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("detailType"));
			}
			this.type = detailType;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000155CC File Offset: 0x000137CC
		[__DynamicallyInvokable]
		public Type DetailType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000155D4 File Offset: 0x000137D4
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x000155DC File Offset: 0x000137DC
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000155F8 File Offset: 0x000137F8
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00015600 File Offset: 0x00013800
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00015653 File Offset: 0x00013853
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0001565B File Offset: 0x0001385B
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00015677 File Offset: 0x00013877
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0001567F File Offset: 0x0001387F
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x000156AC File Offset: 0x000138AC
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x040009A0 RID: 2464
		private string action;

		// Token: 0x040009A1 RID: 2465
		private string name;

		// Token: 0x040009A2 RID: 2466
		private string ns;

		// Token: 0x040009A3 RID: 2467
		private Type type;

		// Token: 0x040009A4 RID: 2468
		private ProtectionLevel protectionLevel;

		// Token: 0x040009A5 RID: 2469
		private bool hasProtectionLevel;

		// Token: 0x040009A6 RID: 2470
		internal const string ProtectionLevelPropertyName = "ProtectionLevel";
	}
}
