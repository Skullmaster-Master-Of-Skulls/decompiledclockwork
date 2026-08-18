using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Runtime.Diagnostics;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002D4 RID: 724
	public abstract class IdentityVerifier
	{
		// Token: 0x060017B3 RID: 6067 RVA: 0x0005A5E8 File Offset: 0x000587E8
		public static IdentityVerifier CreateDefault()
		{
			return IdentityVerifier.DefaultIdentityVerifier.Instance;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0005A5F0 File Offset: 0x000587F0
		internal bool CheckAccess(EndpointAddress reference, Message message)
		{
			if (reference == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reference");
			}
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			EndpointIdentity identity;
			if (!this.TryGetIdentity(reference, out identity))
			{
				return false;
			}
			SecurityMessageProperty securityMessageProperty = null;
			if (message.Properties != null)
			{
				securityMessageProperty = message.Properties.Security;
			}
			return securityMessageProperty != null && securityMessageProperty.ServiceSecurityContext != null && this.CheckAccess(identity, securityMessageProperty.ServiceSecurityContext.AuthorizationContext);
		}

		// Token: 0x060017B5 RID: 6069
		public abstract bool CheckAccess(EndpointIdentity identity, AuthorizationContext authContext);

		// Token: 0x060017B6 RID: 6070
		public abstract bool TryGetIdentity(EndpointAddress reference, out EndpointIdentity identity);

		// Token: 0x060017B7 RID: 6071 RVA: 0x0005A66B File Offset: 0x0005886B
		private static void AdjustAddress(ref EndpointAddress reference, Uri via)
		{
			if (reference.Identity == null && reference.Uri != via)
			{
				reference = new EndpointAddress(via, new AddressHeader[0]);
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0005A693 File Offset: 0x00058893
		internal bool TryGetIdentity(EndpointAddress reference, Uri via, out EndpointIdentity identity)
		{
			IdentityVerifier.AdjustAddress(ref reference, via);
			return this.TryGetIdentity(reference, out identity);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0005A6A5 File Offset: 0x000588A5
		internal void EnsureIncomingIdentity(EndpointAddress serviceReference, AuthorizationContext authorizationContext)
		{
			this.EnsureIdentity(serviceReference, authorizationContext, "IdentityCheckFailedForIncomingMessage");
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0005A6B4 File Offset: 0x000588B4
		internal void EnsureOutgoingIdentity(EndpointAddress serviceReference, Uri via, AuthorizationContext authorizationContext)
		{
			IdentityVerifier.AdjustAddress(ref serviceReference, via);
			this.EnsureIdentity(serviceReference, authorizationContext, "IdentityCheckFailedForOutgoingMessage");
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0005A6CC File Offset: 0x000588CC
		internal void EnsureOutgoingIdentity(EndpointAddress serviceReference, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			if (authorizationPolicies == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authorizationPolicies");
			}
			AuthorizationContext authorizationContext = AuthorizationContext.CreateDefaultAuthorizationContext(authorizationPolicies);
			this.EnsureIdentity(serviceReference, authorizationContext, "IdentityCheckFailedForOutgoingMessage");
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0005A700 File Offset: 0x00058900
		private void EnsureIdentity(EndpointAddress serviceReference, AuthorizationContext authorizationContext, string errorString)
		{
			if (authorizationContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authorizationContext");
			}
			EndpointIdentity endpointIdentity;
			if (!this.TryGetIdentity(serviceReference, out endpointIdentity))
			{
				SecurityTraceRecordHelper.TraceIdentityVerificationFailure(endpointIdentity, authorizationContext, base.GetType());
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString(errorString, new object[]
				{
					endpointIdentity,
					serviceReference
				})));
			}
			if (!this.CheckAccess(endpointIdentity, authorizationContext))
			{
				Exception exception = this.CreateIdentityCheckException(endpointIdentity, authorizationContext, errorString, serviceReference);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(exception);
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0005A780 File Offset: 0x00058980
		private Exception CreateIdentityCheckException(EndpointIdentity identity, AuthorizationContext authorizationContext, string errorString, EndpointAddress serviceReference)
		{
			Exception result;
			if (identity.IdentityClaim != null && identity.IdentityClaim.ClaimType == ClaimTypes.Dns && identity.IdentityClaim.Right == Rights.PossessProperty && identity.IdentityClaim.Resource is string)
			{
				string text = (string)identity.IdentityClaim.Resource;
				string text2 = null;
				for (int i = 0; i < authorizationContext.ClaimSets.Count; i++)
				{
					ClaimSet claimSet = authorizationContext.ClaimSets[i];
					foreach (Claim claim in claimSet.FindClaims(ClaimTypes.Dns, Rights.PossessProperty))
					{
						if (claim.Resource is string)
						{
							text2 = (string)claim.Resource;
							break;
						}
					}
					if (text2 != null)
					{
						break;
					}
				}
				if ("IdentityCheckFailedForIncomingMessage".Equals(errorString))
				{
					if (text2 == null)
					{
						result = new MessageSecurityException(SR.GetString("DnsIdentityCheckFailedForIncomingMessageLackOfDnsClaim", new object[]
						{
							text
						}));
					}
					else
					{
						result = new MessageSecurityException(SR.GetString("DnsIdentityCheckFailedForIncomingMessage", new object[]
						{
							text,
							text2
						}));
					}
				}
				else if ("IdentityCheckFailedForOutgoingMessage".Equals(errorString))
				{
					if (text2 == null)
					{
						result = new MessageSecurityException(SR.GetString("DnsIdentityCheckFailedForOutgoingMessageLackOfDnsClaim", new object[]
						{
							text
						}));
					}
					else
					{
						result = new MessageSecurityException(SR.GetString("DnsIdentityCheckFailedForOutgoingMessage", new object[]
						{
							text,
							text2
						}));
					}
				}
				else
				{
					result = new MessageSecurityException(SR.GetString(errorString, new object[]
					{
						identity,
						serviceReference
					}));
				}
			}
			else
			{
				result = new MessageSecurityException(SR.GetString(errorString, new object[]
				{
					identity,
					serviceReference
				}));
			}
			return result;
		}

		// Token: 0x02000B52 RID: 2898
		private class DefaultIdentityVerifier : IdentityVerifier
		{
			// Token: 0x17001A61 RID: 6753
			// (get) Token: 0x0600712A RID: 28970 RVA: 0x001A56A2 File Offset: 0x001A38A2
			public static IdentityVerifier.DefaultIdentityVerifier Instance
			{
				get
				{
					return IdentityVerifier.DefaultIdentityVerifier.instance;
				}
			}

			// Token: 0x0600712B RID: 28971 RVA: 0x001A56AC File Offset: 0x001A38AC
			public override bool TryGetIdentity(EndpointAddress reference, out EndpointIdentity identity)
			{
				if (reference == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reference");
				}
				identity = reference.Identity;
				if (identity == null)
				{
					identity = this.TryCreateDnsIdentity(reference);
				}
				if (identity == null)
				{
					SecurityTraceRecordHelper.TraceIdentityDeterminationFailure(reference, typeof(IdentityVerifier.DefaultIdentityVerifier));
					return false;
				}
				SecurityTraceRecordHelper.TraceIdentityDeterminationSuccess(reference, identity, typeof(IdentityVerifier.DefaultIdentityVerifier));
				return true;
			}

			// Token: 0x0600712C RID: 28972 RVA: 0x001A5710 File Offset: 0x001A3910
			private EndpointIdentity TryCreateDnsIdentity(EndpointAddress reference)
			{
				Uri uri = reference.Uri;
				if (!uri.IsAbsoluteUri)
				{
					return null;
				}
				return EndpointIdentity.CreateDnsIdentity(uri.DnsSafeHost);
			}

			// Token: 0x0600712D RID: 28973 RVA: 0x001A573C File Offset: 0x001A393C
			private SecurityIdentifier GetSecurityIdentifier(Claim claim)
			{
				if (claim.Resource is WindowsIdentity)
				{
					return ((WindowsIdentity)claim.Resource).User;
				}
				if (claim.Resource is WindowsSidIdentity)
				{
					return ((WindowsSidIdentity)claim.Resource).SecurityIdentifier;
				}
				return claim.Resource as SecurityIdentifier;
			}

			// Token: 0x0600712E RID: 28974 RVA: 0x001A5790 File Offset: 0x001A3990
			private Claim CheckDnsEquivalence(ClaimSet claimSet, string expectedSpn)
			{
				IEnumerable<Claim> enumerable = claimSet.FindClaims(ClaimTypes.Spn, Rights.PossessProperty);
				foreach (Claim claim in enumerable)
				{
					if (expectedSpn.Equals((string)claim.Resource, StringComparison.OrdinalIgnoreCase))
					{
						return claim;
					}
				}
				return null;
			}

			// Token: 0x0600712F RID: 28975 RVA: 0x001A5800 File Offset: 0x001A3A00
			private Claim CheckSidEquivalence(SecurityIdentifier identitySid, ClaimSet claimSet)
			{
				foreach (Claim claim in claimSet)
				{
					SecurityIdentifier securityIdentifier = this.GetSecurityIdentifier(claim);
					if (securityIdentifier != null && identitySid.Equals(securityIdentifier))
					{
						return claim;
					}
				}
				return null;
			}

			// Token: 0x06007130 RID: 28976 RVA: 0x001A5864 File Offset: 0x001A3A64
			public override bool CheckAccess(EndpointIdentity identity, AuthorizationContext authContext)
			{
				EventTraceActivity eventTraceActivity = null;
				if (identity == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
				}
				if (authContext == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authContext");
				}
				if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
				{
					eventTraceActivity = EventTraceActivityHelper.TryExtractActivity((OperationContext.Current != null) ? OperationContext.Current.IncomingMessage : null);
				}
				for (int i = 0; i < authContext.ClaimSets.Count; i++)
				{
					ClaimSet claimSet = authContext.ClaimSets[i];
					if (claimSet.ContainsClaim(identity.IdentityClaim))
					{
						SecurityTraceRecordHelper.TraceIdentityVerificationSuccess(eventTraceActivity, identity, identity.IdentityClaim, base.GetType());
						return true;
					}
					string text = null;
					if (ClaimTypes.Dns.Equals(identity.IdentityClaim.ClaimType))
					{
						text = string.Format(CultureInfo.InvariantCulture, "host/{0}", new object[]
						{
							(string)identity.IdentityClaim.Resource
						});
						Claim claim = this.CheckDnsEquivalence(claimSet, text);
						if (claim != null)
						{
							SecurityTraceRecordHelper.TraceIdentityVerificationSuccess(eventTraceActivity, identity, claim, base.GetType());
							return true;
						}
					}
					SecurityIdentifier securityIdentifier = null;
					if (ClaimTypes.Sid.Equals(identity.IdentityClaim.ClaimType))
					{
						securityIdentifier = this.GetSecurityIdentifier(identity.IdentityClaim);
					}
					else if (ClaimTypes.Upn.Equals(identity.IdentityClaim.ClaimType))
					{
						securityIdentifier = ((UpnEndpointIdentity)identity).GetUpnSid();
					}
					else if (ClaimTypes.Spn.Equals(identity.IdentityClaim.ClaimType))
					{
						securityIdentifier = ((SpnEndpointIdentity)identity).GetSpnSid();
					}
					else if (ClaimTypes.Dns.Equals(identity.IdentityClaim.ClaimType))
					{
						securityIdentifier = new SpnEndpointIdentity(text).GetSpnSid();
					}
					if (securityIdentifier != null)
					{
						Claim claim2 = this.CheckSidEquivalence(securityIdentifier, claimSet);
						if (claim2 != null)
						{
							SecurityTraceRecordHelper.TraceIdentityVerificationSuccess(eventTraceActivity, identity, claim2, base.GetType());
							return true;
						}
					}
				}
				SecurityTraceRecordHelper.TraceIdentityVerificationFailure(identity, authContext, base.GetType());
				if (TD.SecurityIdentityVerificationFailureIsEnabled())
				{
					TD.SecurityIdentityVerificationFailure(eventTraceActivity);
				}
				return false;
			}

			// Token: 0x0400405F RID: 16479
			private static readonly IdentityVerifier.DefaultIdentityVerifier instance = new IdentityVerifier.DefaultIdentityVerifier();
		}
	}
}
