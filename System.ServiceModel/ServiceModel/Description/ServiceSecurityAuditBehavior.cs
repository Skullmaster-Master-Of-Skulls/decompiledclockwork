using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003C5 RID: 965
	public sealed class ServiceSecurityAuditBehavior : IServiceBehavior
	{
		// Token: 0x0600243F RID: 9279 RVA: 0x00083B80 File Offset: 0x00081D80
		public ServiceSecurityAuditBehavior()
		{
			this.auditLogLocation = AuditLogLocation.Default;
			this.suppressAuditFailure = true;
			this.serviceAuthorizationAuditLevel = AuditLevel.None;
			this.messageAuthenticationAuditLevel = AuditLevel.None;
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00083BA4 File Offset: 0x00081DA4
		private ServiceSecurityAuditBehavior(ServiceSecurityAuditBehavior behavior)
		{
			this.auditLogLocation = behavior.auditLogLocation;
			this.suppressAuditFailure = behavior.suppressAuditFailure;
			this.serviceAuthorizationAuditLevel = behavior.serviceAuthorizationAuditLevel;
			this.messageAuthenticationAuditLevel = behavior.messageAuthenticationAuditLevel;
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x00083BDC File Offset: 0x00081DDC
		// (set) Token: 0x06002442 RID: 9282 RVA: 0x00083BE4 File Offset: 0x00081DE4
		public AuditLogLocation AuditLogLocation
		{
			get
			{
				return this.auditLogLocation;
			}
			set
			{
				if (!AuditLogLocationHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.auditLogLocation = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x00083C0A File Offset: 0x00081E0A
		// (set) Token: 0x06002444 RID: 9284 RVA: 0x00083C12 File Offset: 0x00081E12
		public bool SuppressAuditFailure
		{
			get
			{
				return this.suppressAuditFailure;
			}
			set
			{
				this.suppressAuditFailure = value;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x00083C1B File Offset: 0x00081E1B
		// (set) Token: 0x06002446 RID: 9286 RVA: 0x00083C23 File Offset: 0x00081E23
		public AuditLevel ServiceAuthorizationAuditLevel
		{
			get
			{
				return this.serviceAuthorizationAuditLevel;
			}
			set
			{
				if (!AuditLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.serviceAuthorizationAuditLevel = value;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x00083C49 File Offset: 0x00081E49
		// (set) Token: 0x06002448 RID: 9288 RVA: 0x00083C51 File Offset: 0x00081E51
		public AuditLevel MessageAuthenticationAuditLevel
		{
			get
			{
				return this.messageAuthenticationAuditLevel;
			}
			set
			{
				if (!AuditLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.messageAuthenticationAuditLevel = value;
			}
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00083C77 File Offset: 0x00081E77
		internal ServiceSecurityAuditBehavior Clone()
		{
			return new ServiceSecurityAuditBehavior(this);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00083C7F File Offset: 0x00081E7F
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00083C81 File Offset: 0x00081E81
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parameters"));
			}
			parameters.Add(this);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00083CA4 File Offset: 0x00081EA4
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
				if (channelDispatcher != null)
				{
					foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
					{
						if (!endpointDispatcher.IsSystemEndpoint)
						{
							DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
							dispatchRuntime.SecurityAuditLogLocation = this.auditLogLocation;
							dispatchRuntime.SuppressAuditFailure = this.suppressAuditFailure;
							dispatchRuntime.ServiceAuthorizationAuditLevel = this.serviceAuthorizationAuditLevel;
							dispatchRuntime.MessageAuthenticationAuditLevel = this.messageAuthenticationAuditLevel;
						}
					}
				}
			}
		}

		// Token: 0x0400205F RID: 8287
		internal const AuditLogLocation defaultAuditLogLocation = AuditLogLocation.Default;

		// Token: 0x04002060 RID: 8288
		internal const bool defaultSuppressAuditFailure = true;

		// Token: 0x04002061 RID: 8289
		internal const AuditLevel defaultServiceAuthorizationAuditLevel = AuditLevel.None;

		// Token: 0x04002062 RID: 8290
		internal const AuditLevel defaultMessageAuthenticationAuditLevel = AuditLevel.None;

		// Token: 0x04002063 RID: 8291
		private AuditLogLocation auditLogLocation;

		// Token: 0x04002064 RID: 8292
		private bool suppressAuditFailure;

		// Token: 0x04002065 RID: 8293
		private AuditLevel serviceAuthorizationAuditLevel;

		// Token: 0x04002066 RID: 8294
		private AuditLevel messageAuthenticationAuditLevel;
	}
}
