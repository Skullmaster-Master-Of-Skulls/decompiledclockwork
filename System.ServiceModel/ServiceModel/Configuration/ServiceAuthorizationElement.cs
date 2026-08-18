using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Policy;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000684 RID: 1668
	public sealed class ServiceAuthorizationElement : BehaviorExtensionElement
	{
		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x000F4260 File Offset: 0x000F2460
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("principalPermissionMode", typeof(PrincipalPermissionMode), PrincipalPermissionMode.UseWindowsGroups, null, new ServiceModelEnumValidator(typeof(PrincipalPermissionModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("roleProviderName", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("impersonateCallerForAllOperations", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("impersonateOnSerializingReply", typeof(bool), false, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("serviceAuthorizationManagerType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("authorizationPolicies", typeof(AuthorizationPolicyTypeElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x000F4382 File Offset: 0x000F2582
		// (set) Token: 0x0600404A RID: 16458 RVA: 0x000F4394 File Offset: 0x000F2594
		[ConfigurationProperty("principalPermissionMode", DefaultValue = PrincipalPermissionMode.UseWindowsGroups)]
		[ServiceModelEnumValidator(typeof(PrincipalPermissionModeHelper))]
		public PrincipalPermissionMode PrincipalPermissionMode
		{
			get
			{
				return (PrincipalPermissionMode)base["principalPermissionMode"];
			}
			set
			{
				base["principalPermissionMode"] = value;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x0600404B RID: 16459 RVA: 0x000F43A7 File Offset: 0x000F25A7
		// (set) Token: 0x0600404C RID: 16460 RVA: 0x000F43B9 File Offset: 0x000F25B9
		[ConfigurationProperty("roleProviderName", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string RoleProviderName
		{
			get
			{
				return (string)base["roleProviderName"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["roleProviderName"] = value;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x000F43D6 File Offset: 0x000F25D6
		// (set) Token: 0x0600404E RID: 16462 RVA: 0x000F43E8 File Offset: 0x000F25E8
		[ConfigurationProperty("impersonateCallerForAllOperations", DefaultValue = false)]
		public bool ImpersonateCallerForAllOperations
		{
			get
			{
				return (bool)base["impersonateCallerForAllOperations"];
			}
			set
			{
				base["impersonateCallerForAllOperations"] = value;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x000F43FB File Offset: 0x000F25FB
		// (set) Token: 0x06004050 RID: 16464 RVA: 0x000F440D File Offset: 0x000F260D
		[ConfigurationProperty("impersonateOnSerializingReply", DefaultValue = false)]
		public bool ImpersonateOnSerializingReply
		{
			get
			{
				return (bool)base["impersonateOnSerializingReply"];
			}
			set
			{
				base["impersonateOnSerializingReply"] = value;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x000F4420 File Offset: 0x000F2620
		// (set) Token: 0x06004052 RID: 16466 RVA: 0x000F4432 File Offset: 0x000F2632
		[ConfigurationProperty("serviceAuthorizationManagerType", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string ServiceAuthorizationManagerType
		{
			get
			{
				return (string)base["serviceAuthorizationManagerType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["serviceAuthorizationManagerType"] = value;
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x000F444F File Offset: 0x000F264F
		[ConfigurationProperty("authorizationPolicies")]
		public AuthorizationPolicyTypeElementCollection AuthorizationPolicies
		{
			get
			{
				return (AuthorizationPolicyTypeElementCollection)base["authorizationPolicies"];
			}
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x000F4464 File Offset: 0x000F2664
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			ServiceAuthorizationElement serviceAuthorizationElement = (ServiceAuthorizationElement)from;
			this.PrincipalPermissionMode = serviceAuthorizationElement.PrincipalPermissionMode;
			this.RoleProviderName = serviceAuthorizationElement.RoleProviderName;
			this.ImpersonateCallerForAllOperations = serviceAuthorizationElement.ImpersonateCallerForAllOperations;
			this.ImpersonateOnSerializingReply = serviceAuthorizationElement.ImpersonateOnSerializingReply;
			this.ServiceAuthorizationManagerType = serviceAuthorizationElement.ServiceAuthorizationManagerType;
			AuthorizationPolicyTypeElementCollection authorizationPolicies = serviceAuthorizationElement.AuthorizationPolicies;
			AuthorizationPolicyTypeElementCollection authorizationPolicies2 = this.AuthorizationPolicies;
			for (int i = 0; i < authorizationPolicies.Count; i++)
			{
				authorizationPolicies2.Add(authorizationPolicies[i]);
			}
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x000F44E8 File Offset: 0x000F26E8
		protected internal override object CreateBehavior()
		{
			ServiceAuthorizationBehavior serviceAuthorizationBehavior = new ServiceAuthorizationBehavior();
			serviceAuthorizationBehavior.PrincipalPermissionMode = this.PrincipalPermissionMode;
			string roleProviderName = this.RoleProviderName;
			if (!string.IsNullOrEmpty(roleProviderName))
			{
				serviceAuthorizationBehavior.RoleProvider = SystemWebHelper.GetRoleProvider(roleProviderName);
				if (serviceAuthorizationBehavior.RoleProvider == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("InvalidRoleProviderSpecifiedInConfig", new object[]
					{
						roleProviderName
					})));
				}
			}
			serviceAuthorizationBehavior.ImpersonateCallerForAllOperations = this.ImpersonateCallerForAllOperations;
			serviceAuthorizationBehavior.ImpersonateOnSerializingReply = this.ImpersonateOnSerializingReply;
			string serviceAuthorizationManagerType = this.ServiceAuthorizationManagerType;
			if (!string.IsNullOrEmpty(serviceAuthorizationManagerType))
			{
				Type type = Type.GetType(serviceAuthorizationManagerType, true);
				if (!typeof(ServiceAuthorizationManager).IsAssignableFrom(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidServiceAuthorizationManagerType", new object[]
					{
						serviceAuthorizationManagerType,
						typeof(ServiceAuthorizationManager)
					})));
				}
				serviceAuthorizationBehavior.ServiceAuthorizationManager = (ServiceAuthorizationManager)Activator.CreateInstance(type);
			}
			AuthorizationPolicyTypeElementCollection authorizationPolicies = this.AuthorizationPolicies;
			if (authorizationPolicies.Count > 0)
			{
				List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>(authorizationPolicies.Count);
				for (int i = 0; i < authorizationPolicies.Count; i++)
				{
					Type type2 = Type.GetType(authorizationPolicies[i].PolicyType, true);
					if (!typeof(IAuthorizationPolicy).IsAssignableFrom(type2))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidAuthorizationPolicyType", new object[]
						{
							authorizationPolicies[i].PolicyType,
							typeof(IAuthorizationPolicy)
						})));
					}
					list.Add((IAuthorizationPolicy)Activator.CreateInstance(type2));
				}
				serviceAuthorizationBehavior.ExternalAuthorizationPolicies = list.AsReadOnly();
			}
			return serviceAuthorizationBehavior;
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06004056 RID: 16470 RVA: 0x000F4692 File Offset: 0x000F2892
		public override Type BehaviorType
		{
			get
			{
				return typeof(ServiceAuthorizationBehavior);
			}
		}

		// Token: 0x04002CCF RID: 11471
		private ConfigurationPropertyCollection properties;
	}
}
