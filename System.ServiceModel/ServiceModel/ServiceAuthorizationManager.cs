using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000C2 RID: 194
	public class ServiceAuthorizationManager
	{
		// Token: 0x0600037A RID: 890 RVA: 0x00014800 File Offset: 0x00012A00
		public virtual bool CheckAccess(OperationContext operationContext, ref Message message)
		{
			return this.CheckAccess(operationContext);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001480C File Offset: 0x00012A0C
		public virtual bool CheckAccess(OperationContext operationContext)
		{
			if (operationContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("operationContext");
			}
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies = this.GetAuthorizationPolicies(operationContext);
			operationContext.IncomingMessageProperties.Security.ServiceSecurityContext = new ServiceSecurityContext(authorizationPolicies ?? EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance);
			return this.CheckAccessCore(operationContext);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001485C File Offset: 0x00012A5C
		protected virtual ReadOnlyCollection<IAuthorizationPolicy> GetAuthorizationPolicies(OperationContext operationContext)
		{
			SecurityMessageProperty security = operationContext.IncomingMessageProperties.Security;
			if (security == null)
			{
				return EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}
			ReadOnlyCollection<IAuthorizationPolicy> externalAuthorizationPolicies = security.ExternalAuthorizationPolicies;
			if (security.ServiceSecurityContext == null)
			{
				return externalAuthorizationPolicies ?? EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies = security.ServiceSecurityContext.AuthorizationPolicies;
			if (externalAuthorizationPolicies == null || externalAuthorizationPolicies.Count <= 0)
			{
				return authorizationPolicies;
			}
			List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>(authorizationPolicies);
			list.AddRange(externalAuthorizationPolicies);
			return list.AsReadOnly();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000148C6 File Offset: 0x00012AC6
		protected virtual bool CheckAccessCore(OperationContext operationContext)
		{
			return true;
		}
	}
}
