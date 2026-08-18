using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Net;
using System.Runtime;
using System.Security.Principal;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000305 RID: 773
	internal sealed class SpnegoTokenAuthenticator : SspiNegotiationTokenAuthenticator
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00062B8F File Offset: 0x00060D8F
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x00062B97 File Offset: 0x00060D97
		public bool ExtractGroupsForWindowsAccounts
		{
			get
			{
				return this.extractGroupsForWindowsAccounts;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.extractGroupsForWindowsAccounts = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00062BAB File Offset: 0x00060DAB
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x00062BB3 File Offset: 0x00060DB3
		public NetworkCredential ServerCredential
		{
			get
			{
				return this.serverCredential;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.serverCredential = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00062BC7 File Offset: 0x00060DC7
		// (set) Token: 0x06001A63 RID: 6755 RVA: 0x00062BCF File Offset: 0x00060DCF
		public bool AllowUnauthenticatedCallers
		{
			get
			{
				return this.allowUnauthenticatedCallers;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.allowUnauthenticatedCallers = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x00062BE3 File Offset: 0x00060DE3
		public override XmlDictionaryString NegotiationValueType
		{
			get
			{
				return XD.TrustApr2004Dictionary.SpnegoValueTypeUri;
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00062BEF File Offset: 0x00060DEF
		public override void OnOpening()
		{
			base.OnOpening();
			if (this.credentialsHandle == null)
			{
				this.credentialsHandle = SecurityUtils.GetCredentialsHandle("Negotiate", this.serverCredential, true, new string[0]);
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00062C1C File Offset: 0x00060E1C
		public override void OnClose(TimeSpan timeout)
		{
			base.OnClose(timeout);
			this.FreeCredentialsHandle();
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00062C2B File Offset: 0x00060E2B
		public override void OnAbort()
		{
			base.OnAbort();
			this.FreeCredentialsHandle();
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00062C39 File Offset: 0x00060E39
		private void FreeCredentialsHandle()
		{
			if (this.credentialsHandle != null)
			{
				this.credentialsHandle.Close();
				this.credentialsHandle = null;
			}
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00062C58 File Offset: 0x00060E58
		protected override SspiNegotiationTokenAuthenticatorState CreateSspiState(byte[] incomingBlob, string incomingValueTypeUri)
		{
			ISspiNegotiation sspiNegotiation = new WindowsSspiNegotiation("Negotiate", this.credentialsHandle, base.DefaultServiceBinding);
			return new SspiNegotiationTokenAuthenticatorState(sspiNegotiation);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00062C84 File Offset: 0x00060E84
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateSspiNegotiation(ISspiNegotiation sspiNegotiation)
		{
			WindowsSspiNegotiation windowsSspiNegotiation = (WindowsSspiNegotiation)sspiNegotiation;
			if (!windowsSspiNegotiation.IsValidContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("InvalidSspiNegotiation")));
			}
			SecurityTraceRecordHelper.TraceServiceSpnego(windowsSspiNegotiation);
			if (base.IsClientAnonymous)
			{
				return EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}
			ReadOnlyCollection<IAuthorizationPolicy> result;
			using (SafeCloseHandle contextToken = windowsSspiNegotiation.GetContextToken())
			{
				WindowsIdentity windowsIdentity = new WindowsIdentity(contextToken.DangerousGetHandle(), windowsSspiNegotiation.ProtocolName);
				SecurityUtils.ValidateAnonymityConstraint(windowsIdentity, this.AllowUnauthenticatedCallers);
				List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>(1);
				WindowsClaimSet issuance = new WindowsClaimSet(windowsIdentity, windowsSspiNegotiation.ProtocolName, this.extractGroupsForWindowsAccounts, false);
				list.Add(new UnconditionalPolicy(issuance, TimeoutHelper.Add(DateTime.UtcNow, base.ServiceTokenLifetime)));
				result = list.AsReadOnly();
			}
			return result;
		}

		// Token: 0x04001D1A RID: 7450
		private bool extractGroupsForWindowsAccounts;

		// Token: 0x04001D1B RID: 7451
		private NetworkCredential serverCredential;

		// Token: 0x04001D1C RID: 7452
		private bool allowUnauthenticatedCallers;

		// Token: 0x04001D1D RID: 7453
		private SafeFreeCredentials credentialsHandle;
	}
}
