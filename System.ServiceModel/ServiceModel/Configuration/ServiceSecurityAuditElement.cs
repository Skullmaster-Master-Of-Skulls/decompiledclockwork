using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200068E RID: 1678
	public sealed class ServiceSecurityAuditElement : BehaviorExtensionElement
	{
		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x000F6BC0 File Offset: 0x000F4DC0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("auditLogLocation", typeof(AuditLogLocation), AuditLogLocation.Default, null, new ServiceModelEnumValidator(typeof(AuditLogLocationHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("suppressAuditFailure", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("serviceAuthorizationAuditLevel", typeof(AuditLevel), AuditLevel.None, null, new ServiceModelEnumValidator(typeof(AuditLevelHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("messageAuthenticationAuditLevel", typeof(AuditLevel), AuditLevel.None, null, new ServiceModelEnumValidator(typeof(AuditLevelHelper)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x000F6CA9 File Offset: 0x000F4EA9
		// (set) Token: 0x060040ED RID: 16621 RVA: 0x000F6CBB File Offset: 0x000F4EBB
		[ConfigurationProperty("auditLogLocation", DefaultValue = AuditLogLocation.Default)]
		[ServiceModelEnumValidator(typeof(AuditLogLocationHelper))]
		public AuditLogLocation AuditLogLocation
		{
			get
			{
				return (AuditLogLocation)base["auditLogLocation"];
			}
			set
			{
				base["auditLogLocation"] = value;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x000F6CCE File Offset: 0x000F4ECE
		// (set) Token: 0x060040EF RID: 16623 RVA: 0x000F6CE0 File Offset: 0x000F4EE0
		[ConfigurationProperty("suppressAuditFailure", DefaultValue = true)]
		public bool SuppressAuditFailure
		{
			get
			{
				return (bool)base["suppressAuditFailure"];
			}
			set
			{
				base["suppressAuditFailure"] = value;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x000F6CF3 File Offset: 0x000F4EF3
		// (set) Token: 0x060040F1 RID: 16625 RVA: 0x000F6D05 File Offset: 0x000F4F05
		[ConfigurationProperty("serviceAuthorizationAuditLevel", DefaultValue = AuditLevel.None)]
		[ServiceModelEnumValidator(typeof(AuditLevelHelper))]
		public AuditLevel ServiceAuthorizationAuditLevel
		{
			get
			{
				return (AuditLevel)base["serviceAuthorizationAuditLevel"];
			}
			set
			{
				base["serviceAuthorizationAuditLevel"] = value;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x000F6D18 File Offset: 0x000F4F18
		// (set) Token: 0x060040F3 RID: 16627 RVA: 0x000F6D2A File Offset: 0x000F4F2A
		[ConfigurationProperty("messageAuthenticationAuditLevel", DefaultValue = AuditLevel.None)]
		[ServiceModelEnumValidator(typeof(AuditLevelHelper))]
		public AuditLevel MessageAuthenticationAuditLevel
		{
			get
			{
				return (AuditLevel)base["messageAuthenticationAuditLevel"];
			}
			set
			{
				base["messageAuthenticationAuditLevel"] = value;
			}
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x000F6D40 File Offset: 0x000F4F40
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ServiceSecurityAuditElement serviceSecurityAuditElement = (ServiceSecurityAuditElement)from;
			this.AuditLogLocation = serviceSecurityAuditElement.AuditLogLocation;
			this.SuppressAuditFailure = serviceSecurityAuditElement.SuppressAuditFailure;
			this.ServiceAuthorizationAuditLevel = serviceSecurityAuditElement.ServiceAuthorizationAuditLevel;
			this.MessageAuthenticationAuditLevel = serviceSecurityAuditElement.MessageAuthenticationAuditLevel;
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000F6D8C File Offset: 0x000F4F8C
		protected internal override object CreateBehavior()
		{
			return new ServiceSecurityAuditBehavior
			{
				AuditLogLocation = this.AuditLogLocation,
				SuppressAuditFailure = this.SuppressAuditFailure,
				ServiceAuthorizationAuditLevel = this.ServiceAuthorizationAuditLevel,
				MessageAuthenticationAuditLevel = this.MessageAuthenticationAuditLevel
			};
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x000F6DD0 File Offset: 0x000F4FD0
		public override Type BehaviorType
		{
			get
			{
				return typeof(ServiceSecurityAuditBehavior);
			}
		}

		// Token: 0x04002CDB RID: 11483
		private ConfigurationPropertyCollection properties;
	}
}
