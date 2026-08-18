using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200013A RID: 314
	public class Saml2AuthorizationDecisionStatement : Saml2Statement
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x00024AA1 File Offset: 0x00022CA1
		public Saml2AuthorizationDecisionStatement(Uri resource, SamlAccessDecision decision) : this(resource, decision, null)
		{
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00024AAC File Offset: 0x00022CAC
		public Saml2AuthorizationDecisionStatement(Uri resource, SamlAccessDecision decision, IEnumerable<Saml2Action> actions)
		{
			if (null == resource)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("resource");
			}
			if (!resource.IsAbsoluteUri && !resource.Equals(Saml2AuthorizationDecisionStatement.EmptyResource))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("resource", SR.GetString("ID4121"));
			}
			if (decision < SamlAccessDecision.Permit || decision > SamlAccessDecision.Indeterminate)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("decision"));
			}
			this.resource = resource;
			this.decision = decision;
			if (actions != null)
			{
				foreach (Saml2Action item in actions)
				{
					this.actions.Add(item);
				}
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00024B80 File Offset: 0x00022D80
		public Collection<Saml2Action> Actions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x00024B88 File Offset: 0x00022D88
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x00024B90 File Offset: 0x00022D90
		public SamlAccessDecision Decision
		{
			get
			{
				return this.decision;
			}
			set
			{
				if (value < SamlAccessDecision.Permit || value > SamlAccessDecision.Indeterminate)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.decision = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00024BB6 File Offset: 0x00022DB6
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x00024BBE File Offset: 0x00022DBE
		public Saml2Evidence Evidence
		{
			get
			{
				return this.evidence;
			}
			set
			{
				this.evidence = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00024BC7 File Offset: 0x00022DC7
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x00024BD0 File Offset: 0x00022DD0
		public Uri Resource
		{
			get
			{
				return this.resource;
			}
			set
			{
				if (null == value)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (!value.IsAbsoluteUri && !value.Equals(Saml2AuthorizationDecisionStatement.EmptyResource))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4121"));
				}
				this.resource = value;
			}
		}

		// Token: 0x04000B46 RID: 2886
		public static readonly Uri EmptyResource = new Uri(string.Empty, UriKind.Relative);

		// Token: 0x04000B47 RID: 2887
		private Collection<Saml2Action> actions = new Collection<Saml2Action>();

		// Token: 0x04000B48 RID: 2888
		private Saml2Evidence evidence;

		// Token: 0x04000B49 RID: 2889
		private SamlAccessDecision decision;

		// Token: 0x04000B4A RID: 2890
		private Uri resource;
	}
}
