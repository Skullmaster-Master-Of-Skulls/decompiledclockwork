using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Policy;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web.Security;

namespace System.ServiceModel.Description
{
	// Token: 0x020003C3 RID: 963
	public sealed class ServiceAuthorizationBehavior : IServiceBehavior
	{
		// Token: 0x06002415 RID: 9237 RVA: 0x00083445 File Offset: 0x00081645
		public ServiceAuthorizationBehavior()
		{
			this.impersonateCallerForAllOperations = false;
			this.impersonateOnSerializingReply = false;
			this.principalPermissionMode = PrincipalPermissionMode.UseWindowsGroups;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x00083464 File Offset: 0x00081664
		private ServiceAuthorizationBehavior(ServiceAuthorizationBehavior other)
		{
			this.impersonateCallerForAllOperations = other.impersonateCallerForAllOperations;
			this.impersonateOnSerializingReply = other.impersonateOnSerializingReply;
			this.principalPermissionMode = other.principalPermissionMode;
			this.roleProvider = other.roleProvider;
			this.isExternalPoliciesSet = other.isExternalPoliciesSet;
			this.isAuthorizationManagerSet = other.isAuthorizationManagerSet;
			if (other.isExternalPoliciesSet || other.isAuthorizationManagerSet)
			{
				this.CopyAuthorizationPoliciesAndManager(other);
			}
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x000834E2 File Offset: 0x000816E2
		// (set) Token: 0x06002418 RID: 9240 RVA: 0x000834EA File Offset: 0x000816EA
		public ReadOnlyCollection<IAuthorizationPolicy> ExternalAuthorizationPolicies
		{
			get
			{
				return this.externalAuthorizationPolicies;
			}
			set
			{
				this.ThrowIfImmutable();
				this.isExternalPoliciesSet = true;
				this.externalAuthorizationPolicies = value;
			}
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x00083500 File Offset: 0x00081700
		public bool ShouldSerializeExternalAuthorizationPolicies()
		{
			return this.isExternalPoliciesSet;
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x00083508 File Offset: 0x00081708
		// (set) Token: 0x0600241B RID: 9243 RVA: 0x00083510 File Offset: 0x00081710
		public ServiceAuthorizationManager ServiceAuthorizationManager
		{
			get
			{
				return this.serviceAuthorizationManager;
			}
			set
			{
				this.ThrowIfImmutable();
				this.isAuthorizationManagerSet = true;
				this.serviceAuthorizationManager = value;
			}
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x00083526 File Offset: 0x00081726
		public bool ShouldSerializeServiceAuthorizationManager()
		{
			return this.isAuthorizationManagerSet;
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x0008352E File Offset: 0x0008172E
		// (set) Token: 0x0600241E RID: 9246 RVA: 0x00083536 File Offset: 0x00081736
		[DefaultValue(PrincipalPermissionMode.UseWindowsGroups)]
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
				this.ThrowIfImmutable();
				this.principalPermissionMode = value;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x00083562 File Offset: 0x00081762
		// (set) Token: 0x06002420 RID: 9248 RVA: 0x0008356F File Offset: 0x0008176F
		[DefaultValue(null)]
		public RoleProvider RoleProvider
		{
			get
			{
				return (RoleProvider)this.roleProvider;
			}
			set
			{
				this.ThrowIfImmutable();
				this.roleProvider = value;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x0008357E File Offset: 0x0008177E
		// (set) Token: 0x06002422 RID: 9250 RVA: 0x00083586 File Offset: 0x00081786
		[DefaultValue(false)]
		public bool ImpersonateCallerForAllOperations
		{
			get
			{
				return this.impersonateCallerForAllOperations;
			}
			set
			{
				this.ThrowIfImmutable();
				this.impersonateCallerForAllOperations = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x00083595 File Offset: 0x00081795
		// (set) Token: 0x06002424 RID: 9252 RVA: 0x0008359D File Offset: 0x0008179D
		[DefaultValue(false)]
		public bool ImpersonateOnSerializingReply
		{
			get
			{
				return this.impersonateOnSerializingReply;
			}
			set
			{
				this.ThrowIfImmutable();
				this.impersonateOnSerializingReply = value;
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000835AC File Offset: 0x000817AC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ApplyAuthorizationPoliciesAndManager(DispatchRuntime behavior)
		{
			if (this.externalAuthorizationPolicies != null)
			{
				behavior.ExternalAuthorizationPolicies = this.externalAuthorizationPolicies;
			}
			if (this.serviceAuthorizationManager != null)
			{
				behavior.ServiceAuthorizationManager = this.serviceAuthorizationManager;
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000835D6 File Offset: 0x000817D6
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CopyAuthorizationPoliciesAndManager(ServiceAuthorizationBehavior other)
		{
			this.externalAuthorizationPolicies = other.externalAuthorizationPolicies;
			this.serviceAuthorizationManager = other.serviceAuthorizationManager;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000835F0 File Offset: 0x000817F0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ApplyRoleProvider(DispatchRuntime dispatchRuntime)
		{
			dispatchRuntime.RoleProvider = (RoleProvider)this.roleProvider;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00083603 File Offset: 0x00081803
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00083605 File Offset: 0x00081805
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00083608 File Offset: 0x00081808
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("description"));
			}
			if (serviceHostBase == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serviceHostBase"));
			}
			for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
			{
				ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
				if (channelDispatcher != null && !ServiceMetadataBehavior.IsHttpGetMetadataDispatcher(description, channelDispatcher))
				{
					foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
					{
						DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
						dispatchRuntime.PrincipalPermissionMode = this.principalPermissionMode;
						if (!endpointDispatcher.IsSystemEndpoint)
						{
							dispatchRuntime.ImpersonateCallerForAllOperations = this.impersonateCallerForAllOperations;
							dispatchRuntime.ImpersonateOnSerializingReply = this.impersonateOnSerializingReply;
						}
						if (this.roleProvider != null)
						{
							this.ApplyRoleProvider(dispatchRuntime);
						}
						if (this.isAuthorizationManagerSet || this.isExternalPoliciesSet)
						{
							this.ApplyAuthorizationPoliciesAndManager(dispatchRuntime);
						}
					}
				}
			}
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00083720 File Offset: 0x00081920
		internal ServiceAuthorizationBehavior Clone()
		{
			return new ServiceAuthorizationBehavior(this);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00083728 File Offset: 0x00081928
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00083731 File Offset: 0x00081931
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x0400204D RID: 8269
		internal const bool DefaultImpersonateCallerForAllOperations = false;

		// Token: 0x0400204E RID: 8270
		internal const bool DefaultImpersonateOnSerializingReply = false;

		// Token: 0x0400204F RID: 8271
		internal const PrincipalPermissionMode DefaultPrincipalPermissionMode = PrincipalPermissionMode.UseWindowsGroups;

		// Token: 0x04002050 RID: 8272
		private bool impersonateCallerForAllOperations;

		// Token: 0x04002051 RID: 8273
		private bool impersonateOnSerializingReply;

		// Token: 0x04002052 RID: 8274
		private ReadOnlyCollection<IAuthorizationPolicy> externalAuthorizationPolicies;

		// Token: 0x04002053 RID: 8275
		private ServiceAuthorizationManager serviceAuthorizationManager;

		// Token: 0x04002054 RID: 8276
		private PrincipalPermissionMode principalPermissionMode;

		// Token: 0x04002055 RID: 8277
		private object roleProvider;

		// Token: 0x04002056 RID: 8278
		private bool isExternalPoliciesSet;

		// Token: 0x04002057 RID: 8279
		private bool isAuthorizationManagerSet;

		// Token: 0x04002058 RID: 8280
		private bool isReadOnly;
	}
}
