using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200032F RID: 815
	internal sealed class IssuedTokensHeader : MessageHeader
	{
		// Token: 0x06001D2F RID: 7471 RVA: 0x0006CAE5 File Offset: 0x0006ACE5
		public IssuedTokensHeader(RequestSecurityTokenResponse tokenIssuance, MessageSecurityVersion version, SecurityTokenSerializer tokenSerializer) : this(tokenIssuance, new SecurityStandardsManager(version, tokenSerializer))
		{
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0006CAF8 File Offset: 0x0006ACF8
		public IssuedTokensHeader(RequestSecurityTokenResponse tokenIssuance, SecurityStandardsManager standardsManager)
		{
			if (tokenIssuance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenIssuance");
			}
			this.Initialize(new Collection<RequestSecurityTokenResponse>
			{
				tokenIssuance
			}, standardsManager);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0006CB34 File Offset: 0x0006AD34
		public IssuedTokensHeader(IEnumerable<RequestSecurityTokenResponse> tokenIssuances, SecurityStandardsManager standardsManager)
		{
			if (tokenIssuances == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenIssuances");
			}
			int num = 0;
			Collection<RequestSecurityTokenResponse> collection = new Collection<RequestSecurityTokenResponse>();
			foreach (RequestSecurityTokenResponse requestSecurityTokenResponse in tokenIssuances)
			{
				if (requestSecurityTokenResponse == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "tokenIssuances[{0}]", new object[]
					{
						num
					}));
				}
				collection.Add(requestSecurityTokenResponse);
				num++;
			}
			this.Initialize(collection, standardsManager);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0006CBD8 File Offset: 0x0006ADD8
		private void Initialize(Collection<RequestSecurityTokenResponse> coll, SecurityStandardsManager standardsManager)
		{
			if (standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("standardsManager"));
			}
			this.standardsManager = standardsManager;
			this.tokenIssuances = new ReadOnlyCollection<RequestSecurityTokenResponse>(coll);
			this.actor = base.Actor;
			this.mustUnderstand = base.MustUnderstand;
			this.relay = base.Relay;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0006CC34 File Offset: 0x0006AE34
		public IssuedTokensHeader(XmlReader xmlReader, MessageVersion version, SecurityStandardsManager standardsManager)
		{
			if (xmlReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlReader");
			}
			if (standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("standardsManager"));
			}
			this.standardsManager = standardsManager;
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(xmlReader);
			MessageHeader.GetHeaderAttributes(xmlDictionaryReader, version, out this.actor, out this.mustUnderstand, out this.relay, out this.isRefParam);
			xmlDictionaryReader.ReadStartElement(this.Name, this.Namespace);
			Collection<RequestSecurityTokenResponse> collection = new Collection<RequestSecurityTokenResponse>();
			if (this.standardsManager.TrustDriver.IsAtRequestSecurityTokenResponseCollection(xmlDictionaryReader))
			{
				RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = this.standardsManager.TrustDriver.CreateRequestSecurityTokenResponseCollection(xmlDictionaryReader);
				using (IEnumerator<RequestSecurityTokenResponse> enumerator = requestSecurityTokenResponseCollection.RstrCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RequestSecurityTokenResponse item = enumerator.Current;
						collection.Add(item);
					}
					goto IL_E8;
				}
			}
			RequestSecurityTokenResponse item2 = this.standardsManager.TrustDriver.CreateRequestSecurityTokenResponse(xmlDictionaryReader);
			collection.Add(item2);
			IL_E8:
			this.tokenIssuances = new ReadOnlyCollection<RequestSecurityTokenResponse>(collection);
			xmlDictionaryReader.ReadEndElement();
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0006CD4C File Offset: 0x0006AF4C
		public ReadOnlyCollection<RequestSecurityTokenResponse> TokenIssuances
		{
			get
			{
				return this.tokenIssuances;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0006CD54 File Offset: 0x0006AF54
		public override string Actor
		{
			get
			{
				return this.actor;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0006CD5C File Offset: 0x0006AF5C
		public override bool IsReferenceParameter
		{
			get
			{
				return this.isRefParam;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0006CD64 File Offset: 0x0006AF64
		public override bool MustUnderstand
		{
			get
			{
				return this.mustUnderstand;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0006CD6C File Offset: 0x0006AF6C
		public override bool Relay
		{
			get
			{
				return this.relay;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0006CD74 File Offset: 0x0006AF74
		public override string Name
		{
			get
			{
				return this.standardsManager.TrustDriver.IssuedTokensHeaderName;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0006CD86 File Offset: 0x0006AF86
		public override string Namespace
		{
			get
			{
				return this.standardsManager.TrustDriver.IssuedTokensHeaderNamespace;
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0006CD98 File Offset: 0x0006AF98
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (this.tokenIssuances.Count == 1)
			{
				this.standardsManager.TrustDriver.WriteRequestSecurityTokenResponse(this.tokenIssuances[0], writer);
				return;
			}
			RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = new RequestSecurityTokenResponseCollection(this.tokenIssuances, this.standardsManager);
			requestSecurityTokenResponseCollection.WriteTo(writer);
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0006CDEA File Offset: 0x0006AFEA
		internal static Collection<RequestSecurityTokenResponse> ExtractIssuances(Message message, MessageSecurityVersion version, WSSecurityTokenSerializer tokenSerializer, string[] actors, XmlQualifiedName expectedAppliesToQName)
		{
			return IssuedTokensHeader.ExtractIssuances(message, new SecurityStandardsManager(version, tokenSerializer), actors, expectedAppliesToQName);
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0006CDFC File Offset: 0x0006AFFC
		internal static Collection<RequestSecurityTokenResponse> ExtractIssuances(Message message, SecurityStandardsManager standardsManager, string[] actors, XmlQualifiedName expectedAppliesToQName)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (standardsManager == null)
			{
				standardsManager = SecurityStandardsManager.DefaultInstance;
			}
			if (actors == null)
			{
				throw TraceUtility.ThrowHelperArgumentNull("actors", message);
			}
			Collection<RequestSecurityTokenResponse> collection = new Collection<RequestSecurityTokenResponse>();
			for (int i = 0; i < message.Headers.Count; i++)
			{
				if (message.Headers[i].Name == standardsManager.TrustDriver.IssuedTokensHeaderName && message.Headers[i].Namespace == standardsManager.TrustDriver.IssuedTokensHeaderNamespace)
				{
					bool flag = false;
					for (int j = 0; j < actors.Length; j++)
					{
						if (actors[j] == message.Headers[i].Actor)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						IssuedTokensHeader issuedTokensHeader = new IssuedTokensHeader(message.Headers.GetReaderAtHeader(i), message.Version, standardsManager);
						for (int k = 0; k < issuedTokensHeader.TokenIssuances.Count; k++)
						{
							bool flag2;
							if (expectedAppliesToQName != null)
							{
								string a;
								string a2;
								issuedTokensHeader.TokenIssuances[k].GetAppliesToQName(out a, out a2);
								flag2 = (a == expectedAppliesToQName.Name && a2 == expectedAppliesToQName.Namespace);
							}
							else
							{
								flag2 = true;
							}
							if (flag2)
							{
								collection.Add(issuedTokensHeader.TokenIssuances[k]);
							}
						}
					}
				}
			}
			return collection;
		}

		// Token: 0x04001DFD RID: 7677
		private ReadOnlyCollection<RequestSecurityTokenResponse> tokenIssuances;

		// Token: 0x04001DFE RID: 7678
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001DFF RID: 7679
		private string actor;

		// Token: 0x04001E00 RID: 7680
		private bool mustUnderstand;

		// Token: 0x04001E01 RID: 7681
		private bool relay;

		// Token: 0x04001E02 RID: 7682
		private bool isRefParam;
	}
}
