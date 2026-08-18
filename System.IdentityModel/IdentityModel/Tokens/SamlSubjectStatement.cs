using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000164 RID: 356
	public abstract class SamlSubjectStatement : SamlStatement
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x000361B8 File Offset: 0x000343B8
		protected SamlSubjectStatement()
		{
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000361C0 File Offset: 0x000343C0
		protected SamlSubjectStatement(SamlSubject samlSubject)
		{
			if (samlSubject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSubject"));
			}
			this.subject = samlSubject;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000361E7 File Offset: 0x000343E7
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x000361F0 File Offset: 0x000343F0
		public SamlSubject SamlSubject
		{
			get
			{
				return this.subject;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.subject = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0003623E File Offset: 0x0003443E
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00036246 File Offset: 0x00034446
		public override void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				this.subject.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00036264 File Offset: 0x00034464
		public override IAuthorizationPolicy CreatePolicy(ClaimSet issuer, SamlSecurityTokenAuthenticator samlAuthenticator)
		{
			if (issuer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuer");
			}
			if (this.policy == null)
			{
				List<ClaimSet> list = new List<ClaimSet>();
				ClaimSet claimSet = this.subject.ExtractSubjectKeyClaimSet(samlAuthenticator);
				if (claimSet != null)
				{
					list.Add(claimSet);
				}
				List<Claim> list2 = new List<Claim>();
				ReadOnlyCollection<Claim> readOnlyCollection = this.subject.ExtractClaims();
				for (int i = 0; i < readOnlyCollection.Count; i++)
				{
					list2.Add(readOnlyCollection[i]);
				}
				this.AddClaimsToList(list2);
				list.Add(new DefaultClaimSet(issuer, list2));
				this.policy = new UnconditionalPolicy(this.subject.Identity, list.AsReadOnly(), SecurityUtils.MaxUtcDateTime);
			}
			return this.policy;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0003631C File Offset: 0x0003451C
		protected void SetSubject(SamlSubject samlSubject)
		{
			if (samlSubject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSubject"));
			}
			this.subject = samlSubject;
		}

		// Token: 0x06000B49 RID: 2889
		protected abstract void AddClaimsToList(IList<Claim> claims);

		// Token: 0x04000BF5 RID: 3061
		private SamlSubject subject;

		// Token: 0x04000BF6 RID: 3062
		private IAuthorizationPolicy policy;

		// Token: 0x04000BF7 RID: 3063
		private bool isReadOnly;
	}
}
