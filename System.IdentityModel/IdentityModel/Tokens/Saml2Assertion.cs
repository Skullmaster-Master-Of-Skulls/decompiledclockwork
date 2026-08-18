using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000133 RID: 307
	public class Saml2Assertion
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x0002428C File Offset: 0x0002248C
		public Saml2Assertion(Saml2NameIdentifier issuer)
		{
			if (issuer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuer");
			}
			this.issuer = issuer;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000242F0 File Offset: 0x000224F0
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x000242F8 File Offset: 0x000224F8
		public Saml2Advice Advice
		{
			get
			{
				return this.advice;
			}
			set
			{
				this.advice = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00024301 File Offset: 0x00022501
		public virtual bool CanWriteSourceData
		{
			get
			{
				return this.sourceData != null;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0002430C File Offset: 0x0002250C
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x00024314 File Offset: 0x00022514
		public Saml2Conditions Conditions
		{
			get
			{
				return this.conditions;
			}
			set
			{
				this.conditions = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0002431D File Offset: 0x0002251D
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x00024325 File Offset: 0x00022525
		public EncryptingCredentials EncryptingCredentials
		{
			get
			{
				return this.encryptingCredentials;
			}
			set
			{
				this.encryptingCredentials = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0002432E File Offset: 0x0002252E
		public Collection<EncryptedKeyIdentifierClause> ExternalEncryptedKeys
		{
			get
			{
				return this.externalEncryptedKeys;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00024336 File Offset: 0x00022536
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x0002433E File Offset: 0x0002253E
		public Saml2Id Id
		{
			get
			{
				return this.id;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.id = value;
				this.sourceData = null;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00024361 File Offset: 0x00022561
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00024369 File Offset: 0x00022569
		public DateTime IssueInstant
		{
			get
			{
				return this.issueInstant;
			}
			set
			{
				this.issueInstant = DateTimeUtil.ToUniversalTime(value);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00024377 File Offset: 0x00022577
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0002437F File Offset: 0x0002257F
		public Saml2NameIdentifier Issuer
		{
			get
			{
				return this.issuer;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.issuer = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0002439B File Offset: 0x0002259B
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x000243A3 File Offset: 0x000225A3
		public SigningCredentials SigningCredentials
		{
			get
			{
				return this.signingCredentials;
			}
			set
			{
				this.signingCredentials = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x000243AC File Offset: 0x000225AC
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x000243B4 File Offset: 0x000225B4
		public Saml2Subject Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x000243BD File Offset: 0x000225BD
		public Collection<Saml2Statement> Statements
		{
			get
			{
				return this.statements;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x000243C5 File Offset: 0x000225C5
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000243D0 File Offset: 0x000225D0
		public virtual void WriteSourceData(XmlWriter writer)
		{
			if (!this.CanWriteSourceData)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4140")));
			}
			XmlDictionaryWriter writer2 = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			this.sourceData.SetElementExclusion(null, null);
			this.sourceData.GetWriter().WriteTo(writer2, new DictionaryManager());
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00024429 File Offset: 0x00022629
		internal virtual void CaptureSourceData(EnvelopedSignatureReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			this.sourceData = reader.XmlTokens;
		}

		// Token: 0x04000B2A RID: 2858
		private Saml2Advice advice;

		// Token: 0x04000B2B RID: 2859
		private Saml2Conditions conditions;

		// Token: 0x04000B2C RID: 2860
		private EncryptingCredentials encryptingCredentials;

		// Token: 0x04000B2D RID: 2861
		private Collection<EncryptedKeyIdentifierClause> externalEncryptedKeys = new Collection<EncryptedKeyIdentifierClause>();

		// Token: 0x04000B2E RID: 2862
		private Saml2Id id = new Saml2Id();

		// Token: 0x04000B2F RID: 2863
		private DateTime issueInstant = DateTime.UtcNow;

		// Token: 0x04000B30 RID: 2864
		private Saml2NameIdentifier issuer;

		// Token: 0x04000B31 RID: 2865
		private SigningCredentials signingCredentials;

		// Token: 0x04000B32 RID: 2866
		private XmlTokenStream sourceData;

		// Token: 0x04000B33 RID: 2867
		private Collection<Saml2Statement> statements = new Collection<Saml2Statement>();

		// Token: 0x04000B34 RID: 2868
		private Saml2Subject subject;

		// Token: 0x04000B35 RID: 2869
		private string version = "2.0";
	}
}
