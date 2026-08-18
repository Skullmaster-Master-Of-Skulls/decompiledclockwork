using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000145 RID: 325
	public class Saml2Subject
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0002B73C File Offset: 0x0002993C
		public Saml2Subject()
		{
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002B74F File Offset: 0x0002994F
		public Saml2Subject(Saml2NameIdentifier nameId)
		{
			this.nameId = nameId;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002B769 File Offset: 0x00029969
		public Saml2Subject(Saml2SubjectConfirmation subjectConfirmation)
		{
			if (subjectConfirmation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subjectConfirmation");
			}
			this.subjectConfirmations.Add(subjectConfirmation);
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0002B79B File Offset: 0x0002999B
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x0002B7A3 File Offset: 0x000299A3
		public Saml2NameIdentifier NameId
		{
			get
			{
				return this.nameId;
			}
			set
			{
				this.nameId = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0002B7AC File Offset: 0x000299AC
		public Collection<Saml2SubjectConfirmation> SubjectConfirmations
		{
			get
			{
				return this.subjectConfirmations;
			}
		}

		// Token: 0x04000B6D RID: 2925
		private Saml2NameIdentifier nameId;

		// Token: 0x04000B6E RID: 2926
		private Collection<Saml2SubjectConfirmation> subjectConfirmations = new Collection<Saml2SubjectConfirmation>();
	}
}
