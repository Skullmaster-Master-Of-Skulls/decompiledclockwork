using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000156 RID: 342
	public class SamlAuthorizationDecisionStatement : SamlSubjectStatement
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x0002F284 File Offset: 0x0002D484
		public SamlAuthorizationDecisionStatement()
		{
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002F297 File Offset: 0x0002D497
		public SamlAuthorizationDecisionStatement(SamlSubject samlSubject, string resource, SamlAccessDecision accessDecision, IEnumerable<SamlAction> samlActions) : this(samlSubject, resource, accessDecision, samlActions, null)
		{
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002F2A8 File Offset: 0x0002D4A8
		public SamlAuthorizationDecisionStatement(SamlSubject samlSubject, string resource, SamlAccessDecision accessDecision, IEnumerable<SamlAction> samlActions, SamlEvidence samlEvidence) : base(samlSubject)
		{
			if (samlActions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlActions"));
			}
			foreach (SamlAction samlAction in samlActions)
			{
				if (samlAction == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEntityCannotBeNullOrEmpty", new object[]
					{
						XD.SamlDictionary.Action.Value
					}));
				}
				this.actions.Add(samlAction);
			}
			this.evidence = samlEvidence;
			this.accessDecision = accessDecision;
			this.resource = resource;
			this.CheckObjectValidity();
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002F370 File Offset: 0x0002D570
		public static string ClaimType
		{
			get
			{
				return ClaimTypes.AuthorizationDecision;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0002F377 File Offset: 0x0002D577
		public IList<SamlAction> SamlActions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0002F37F File Offset: 0x0002D57F
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0002F387 File Offset: 0x0002D587
		public SamlAccessDecision AccessDecision
		{
			get
			{
				return this.accessDecision;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.accessDecision = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0002F3B2 File Offset: 0x0002D5B2
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x0002F3BA File Offset: 0x0002D5BA
		public SamlEvidence Evidence
		{
			get
			{
				return this.evidence;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.evidence = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0002F3E5 File Offset: 0x0002D5E5
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0002F3F0 File Offset: 0x0002D5F0
		public string Resource
		{
			get
			{
				return this.resource;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAuthorizationDecisionResourceRequired"));
				}
				this.resource = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0002F443 File Offset: 0x0002D643
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002F44C File Offset: 0x0002D64C
		public override void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				if (this.evidence != null)
				{
					this.evidence.MakeReadOnly();
				}
				foreach (SamlAction samlAction in this.actions)
				{
					samlAction.MakeReadOnly();
				}
				this.actions.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002F4C8 File Offset: 0x0002D6C8
		protected override void AddClaimsToList(IList<Claim> claims)
		{
			if (claims == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("claims"));
			}
			for (int i = 0; i < this.actions.Count; i++)
			{
				claims.Add(new Claim(ClaimTypes.AuthorizationDecision, new SamlAuthorizationDecisionClaimResource(this.resource, this.accessDecision, this.actions[i].Namespace, this.actions[i].Action), Rights.PossessProperty));
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002F54C File Offset: 0x0002D74C
		private void CheckObjectValidity()
		{
			if (base.SamlSubject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLSubjectStatementRequiresSubject")));
			}
			if (string.IsNullOrEmpty(this.resource))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorizationDecisionResourceRequired")));
			}
			if (this.actions.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorizationDecisionShouldHaveOneAction")));
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002F5CC File Offset: 0x0002D7CC
		public override void ReadXml(XmlDictionaryReader reader, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			this.resource = reader.GetAttribute(samlDictionary.Resource, null);
			if (string.IsNullOrEmpty(this.resource))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorizationDecisionStatementMissingResourceAttributeOnRead")));
			}
			string attribute = reader.GetAttribute(samlDictionary.Decision, null);
			if (string.IsNullOrEmpty(attribute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorizationDecisionStatementMissingDecisionAttributeOnRead")));
			}
			if (attribute.Equals(SamlAccessDecision.Deny.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				this.accessDecision = SamlAccessDecision.Deny;
			}
			else if (attribute.Equals(SamlAccessDecision.Permit.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				this.accessDecision = SamlAccessDecision.Permit;
			}
			else
			{
				this.accessDecision = SamlAccessDecision.Indeterminate;
			}
			reader.MoveToContent();
			reader.Read();
			if (!reader.IsStartElement(samlDictionary.Subject, samlDictionary.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorizationDecisionStatementMissingSubjectOnRead")));
			}
			SamlSubject samlSubject = new SamlSubject();
			samlSubject.ReadXml(reader, samlSerializer, keyInfoSerializer, outOfBandTokenResolver);
			base.SamlSubject = samlSubject;
			while (reader.IsStartElement())
			{
				if (reader.IsStartElement(samlDictionary.Action, samlDictionary.Namespace))
				{
					SamlAction samlAction = new SamlAction();
					samlAction.ReadXml(reader, samlSerializer, keyInfoSerializer, outOfBandTokenResolver);
					this.actions.Add(samlAction);
				}
				else
				{
					if (!reader.IsStartElement(samlDictionary.Evidence, samlDictionary.Namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLBadSchema", new object[]
						{
							samlDictionary.AuthorizationDecisionStatement
						})));
					}
					if (this.evidence != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorizationDecisionHasMoreThanOneEvidence")));
					}
					this.evidence = new SamlEvidence();
					this.evidence.ReadXml(reader, samlSerializer, keyInfoSerializer, outOfBandTokenResolver);
				}
			}
			if (this.actions.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorizationDecisionShouldHaveOneActionOnRead")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002F814 File Offset: 0x0002DA14
		public override void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
		{
			this.CheckObjectValidity();
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.AuthorizationDecisionStatement, samlDictionary.Namespace);
			writer.WriteStartAttribute(samlDictionary.Decision, null);
			writer.WriteString(this.accessDecision.ToString());
			writer.WriteEndAttribute();
			writer.WriteStartAttribute(samlDictionary.Resource, null);
			writer.WriteString(this.resource);
			writer.WriteEndAttribute();
			base.SamlSubject.WriteXml(writer, samlSerializer, keyInfoSerializer);
			foreach (SamlAction samlAction in this.actions)
			{
				samlAction.WriteXml(writer, samlSerializer, keyInfoSerializer);
			}
			if (this.evidence != null)
			{
				this.evidence.WriteXml(writer, samlSerializer, keyInfoSerializer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000BBC RID: 3004
		private SamlEvidence evidence;

		// Token: 0x04000BBD RID: 3005
		private readonly ImmutableCollection<SamlAction> actions = new ImmutableCollection<SamlAction>();

		// Token: 0x04000BBE RID: 3006
		private SamlAccessDecision accessDecision;

		// Token: 0x04000BBF RID: 3007
		private string resource;

		// Token: 0x04000BC0 RID: 3008
		private bool isReadOnly;
	}
}
