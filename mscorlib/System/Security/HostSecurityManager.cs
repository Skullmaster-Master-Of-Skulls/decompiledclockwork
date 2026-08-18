using System;
using System.Collections;
using System.Deployment.Internal.Isolation.Manifest;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Security
{
	// Token: 0x02000673 RID: 1651
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class HostSecurityManager
	{
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000C9428 File Offset: 0x000C8428
		public virtual HostSecurityManagerOptions Flags
		{
			get
			{
				return HostSecurityManagerOptions.AllFlags;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x000C942C File Offset: 0x000C842C
		public virtual PolicyLevel DomainPolicy
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000C942F File Offset: 0x000C842F
		public virtual Evidence ProvideAppDomainEvidence(Evidence inputEvidence)
		{
			return inputEvidence;
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000C9432 File Offset: 0x000C8432
		public virtual Evidence ProvideAssemblyEvidence(Assembly loadedAssembly, Evidence inputEvidence)
		{
			return inputEvidence;
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000C9438 File Offset: 0x000C8438
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		public virtual ApplicationTrust DetermineApplicationTrust(Evidence applicationEvidence, Evidence activatorEvidence, TrustManagerContext context)
		{
			if (applicationEvidence == null)
			{
				throw new ArgumentNullException("applicationEvidence");
			}
			IEnumerator hostEnumerator = applicationEvidence.GetHostEnumerator();
			ActivationArguments activationArguments = null;
			ApplicationTrust applicationTrust = null;
			while (hostEnumerator.MoveNext())
			{
				if (activationArguments == null)
				{
					activationArguments = (hostEnumerator.Current as ActivationArguments);
				}
				if (applicationTrust == null)
				{
					applicationTrust = (hostEnumerator.Current as ApplicationTrust);
				}
				if (activationArguments != null && applicationTrust != null)
				{
					break;
				}
			}
			if (activationArguments == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
			}
			ActivationContext activationContext = activationArguments.ActivationContext;
			if (activationContext == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
			}
			if (applicationTrust != null && !CmsUtils.CompareIdentities(applicationTrust.ApplicationIdentity, activationArguments.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion))
			{
				applicationTrust = null;
			}
			if (applicationTrust == null)
			{
				if (AppDomain.CurrentDomain.ApplicationTrust != null && CmsUtils.CompareIdentities(AppDomain.CurrentDomain.ApplicationTrust.ApplicationIdentity, activationArguments.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion))
				{
					applicationTrust = AppDomain.CurrentDomain.ApplicationTrust;
				}
				else
				{
					applicationTrust = ApplicationSecurityManager.DetermineApplicationTrustInternal(activationContext, context);
				}
			}
			ApplicationSecurityInfo applicationSecurityInfo = new ApplicationSecurityInfo(activationContext);
			if (applicationTrust != null && applicationTrust.IsApplicationTrustedToRun && !applicationSecurityInfo.DefaultRequestSet.IsSubsetOf(applicationTrust.DefaultGrantSet.PermissionSet))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Policy_AppTrustMustGrantAppRequest"));
			}
			return applicationTrust;
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000C9550 File Offset: 0x000C8550
		public virtual PermissionSet ResolvePolicy(Evidence evidence)
		{
			return SecurityManager.PolicyManager.ResolveHelper(evidence);
		}
	}
}
