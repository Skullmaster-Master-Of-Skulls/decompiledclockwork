using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000309 RID: 777
	internal abstract class SspiNegotiationTokenProvider : NegotiationTokenProvider<SspiNegotiationTokenProviderState>
	{
		// Token: 0x06001AA6 RID: 6822 RVA: 0x00063A90 File Offset: 0x00061C90
		protected SspiNegotiationTokenProvider() : this(null)
		{
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00063A99 File Offset: 0x00061C99
		protected SspiNegotiationTokenProvider(SecurityBindingElement securityBindingElement)
		{
			this.securityBindingElement = securityBindingElement;
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x00063AA8 File Offset: 0x00061CA8
		// (set) Token: 0x06001AA9 RID: 6825 RVA: 0x00063AB0 File Offset: 0x00061CB0
		public bool NegotiateTokenOnOpen
		{
			get
			{
				return this.negotiateTokenOnOpen;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.negotiateTokenOnOpen = value;
			}
		}

		// Token: 0x06001AAA RID: 6826
		protected abstract ReadOnlyCollection<IAuthorizationPolicy> ValidateSspiNegotiation(ISspiNegotiation sspiNegotiation);

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001AAB RID: 6827
		public abstract XmlDictionaryString NegotiationValueType { get; }

		// Token: 0x06001AAC RID: 6828 RVA: 0x00063AC4 File Offset: 0x00061CC4
		public override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.EnsureEndpointAddressDoesNotRequireEncryption(base.TargetAddress);
			base.OnOpen(timeoutHelper.RemainingTime());
			if (this.negotiateTokenOnOpen)
			{
				base.DoNegotiation(timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00063B08 File Offset: 0x00061D08
		protected override IChannelFactory<IRequestChannel> GetNegotiationChannelFactory(IChannelFactory<IRequestChannel> transportChannelFactory, ChannelBuilder channelBuilder)
		{
			return transportChannelFactory;
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00063B0B File Offset: 0x00061D0B
		private void ValidateIncomingBinaryNegotiation(BinaryNegotiation incomingNego)
		{
			incomingNego.Validate(this.NegotiationValueType);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00063B1C File Offset: 0x00061D1C
		private static void AddToDigest(HashAlgorithm negotiationDigest, Stream stream)
		{
			stream.Flush();
			stream.Seek(0L, SeekOrigin.Begin);
			CanonicalizationDriver canonicalizationDriver = new CanonicalizationDriver();
			canonicalizationDriver.SetInput(stream);
			byte[] bytes = canonicalizationDriver.GetBytes();
			lock (negotiationDigest)
			{
				negotiationDigest.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00063B84 File Offset: 0x00061D84
		private static void AddToDigest(SspiNegotiationTokenProviderState sspiState, RequestSecurityToken rst)
		{
			MemoryStream stream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream);
			rst.WriteTo(xmlDictionaryWriter);
			xmlDictionaryWriter.Flush();
			SspiNegotiationTokenProvider.AddToDigest(sspiState.NegotiationDigest, stream);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00063BB8 File Offset: 0x00061DB8
		private void AddToDigest(SspiNegotiationTokenProviderState sspiState, RequestSecurityTokenResponse rstr, bool wasReceived, bool isFinalRstr)
		{
			MemoryStream stream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream);
			if (!wasReceived)
			{
				rstr.WriteTo(xmlDictionaryWriter);
			}
			else if (!isFinalRstr)
			{
				rstr.RequestSecurityTokenResponseXml.WriteTo(xmlDictionaryWriter);
			}
			else
			{
				XmlElement xmlElement = (XmlElement)rstr.RequestSecurityTokenResponseXml.CloneNode(true);
				List<XmlNode> list = new List<XmlNode>(2);
				for (int i = 0; i < xmlElement.ChildNodes.Count; i++)
				{
					XmlNode xmlNode = xmlElement.ChildNodes[i];
					if (base.StandardsManager.TrustDriver.IsRequestedSecurityTokenElement(xmlNode.LocalName, xmlNode.NamespaceURI))
					{
						list.Add(xmlNode);
					}
					else if (base.StandardsManager.TrustDriver.IsRequestedProofTokenElement(xmlNode.LocalName, xmlNode.NamespaceURI))
					{
						list.Add(xmlNode);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					xmlElement.RemoveChild(list[j]);
				}
				xmlElement.WriteTo(xmlDictionaryWriter);
			}
			xmlDictionaryWriter.Flush();
			SspiNegotiationTokenProvider.AddToDigest(sspiState.NegotiationDigest, stream);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00063CC8 File Offset: 0x00061EC8
		private static bool IsCorrectAuthenticator(SspiNegotiationTokenProviderState sspiState, byte[] proofKey, byte[] serverAuthenticator)
		{
			HashAlgorithm negotiationDigest = sspiState.NegotiationDigest;
			byte[] hash;
			lock (negotiationDigest)
			{
				sspiState.NegotiationDigest.TransformFinalBlock(CryptoHelper.EmptyBuffer, 0, 0);
				hash = sspiState.NegotiationDigest.Hash;
			}
			Psha1DerivedKeyGenerator psha1DerivedKeyGenerator = new Psha1DerivedKeyGenerator(proofKey);
			byte[] array = psha1DerivedKeyGenerator.GenerateDerivedKey(SecurityUtils.CombinedHashLabel, hash, 256, 0);
			if (array.Length != serverAuthenticator.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != serverAuthenticator[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00063D68 File Offset: 0x00061F68
		private BodyWriter PrepareRstr(SspiNegotiationTokenProviderState sspiState, byte[] outgoingBlob)
		{
			RequestSecurityTokenResponse requestSecurityTokenResponse = new RequestSecurityTokenResponse(base.StandardsManager);
			requestSecurityTokenResponse.Context = sspiState.Context;
			requestSecurityTokenResponse.SetBinaryNegotiation(new BinaryNegotiation(this.NegotiationValueType, outgoingBlob));
			requestSecurityTokenResponse.MakeReadOnly();
			this.AddToDigest(sspiState, requestSecurityTokenResponse, false, false);
			return requestSecurityTokenResponse;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00063DB0 File Offset: 0x00061FB0
		protected override BodyWriter GetFirstOutgoingMessageBody(SspiNegotiationTokenProviderState sspiState, out MessageProperties messageProperties)
		{
			messageProperties = null;
			RequestSecurityToken requestSecurityToken = new RequestSecurityToken(base.StandardsManager, false);
			requestSecurityToken.Context = sspiState.Context;
			requestSecurityToken.TokenType = base.StandardsManager.SecureConversationDriver.TokenTypeUri;
			requestSecurityToken.KeySize = base.SecurityAlgorithmSuite.DefaultSymmetricKeyLength;
			requestSecurityToken.OnGetBinaryNegotiation = new RequestSecurityToken.OnGetBinaryNegotiationCallback(new SspiNegotiationTokenProvider.GetOutgoingBlobProxy(sspiState, this, requestSecurityToken).GetOutgoingBlob);
			return requestSecurityToken;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00063E1C File Offset: 0x0006201C
		protected override IRequestChannel CreateClientChannel(EndpointAddress target, Uri via)
		{
			IRequestChannel requestChannel = base.CreateClientChannel(target, via);
			if (!SecurityUtils.IsChannelBindingDisabled && this.securityBindingElement is TransportSecurityBindingElement)
			{
				IChannelBindingProvider property = requestChannel.GetProperty<IChannelBindingProvider>();
				if (property != null)
				{
					property.EnableChannelBindingSupport();
				}
			}
			return requestChannel;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00063E58 File Offset: 0x00062058
		protected override BodyWriter GetNextOutgoingMessageBody(Message incomingMessage, SspiNegotiationTokenProviderState sspiState)
		{
			try
			{
				IssuanceTokenProviderBase<SspiNegotiationTokenProviderState>.ThrowIfFault(incomingMessage, base.TargetAddress);
			}
			catch (FaultException ex)
			{
				if (!ex.Code.IsSenderFault)
				{
					throw;
				}
				if (ex.Code.SubCode.Name == "FailedAuthentication" || ex.Code.SubCode.Name == "FailedAuthentication")
				{
					throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("AuthenticationOfClientFailed"), ex), incomingMessage);
				}
				throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("FailedSspiNegotiation"), ex), incomingMessage);
			}
			RequestSecurityTokenResponse requestSecurityTokenResponse = null;
			RequestSecurityTokenResponse requestSecurityTokenResponse2 = null;
			XmlDictionaryReader readerAtBodyContents = incomingMessage.GetReaderAtBodyContents();
			using (readerAtBodyContents)
			{
				if (base.StandardsManager.TrustDriver.IsAtRequestSecurityTokenResponseCollection(readerAtBodyContents))
				{
					RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = base.StandardsManager.TrustDriver.CreateRequestSecurityTokenResponseCollection(readerAtBodyContents);
					using (IEnumerator<RequestSecurityTokenResponse> enumerator = requestSecurityTokenResponseCollection.RstrCollection.GetEnumerator())
					{
						enumerator.MoveNext();
						requestSecurityTokenResponse = enumerator.Current;
						if (enumerator.MoveNext())
						{
							requestSecurityTokenResponse2 = enumerator.Current;
						}
					}
					if (requestSecurityTokenResponse2 == null)
					{
						throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("AuthenticatorNotPresentInRSTRCollection")), incomingMessage);
					}
					if (requestSecurityTokenResponse2.Context != requestSecurityTokenResponse.Context)
					{
						throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("RSTRAuthenticatorHasBadContext")), incomingMessage);
					}
					this.AddToDigest(sspiState, requestSecurityTokenResponse, true, true);
				}
				else if (base.StandardsManager.TrustDriver.IsAtRequestSecurityTokenResponse(readerAtBodyContents))
				{
					requestSecurityTokenResponse = RequestSecurityTokenResponse.CreateFrom(base.StandardsManager, readerAtBodyContents);
					this.AddToDigest(sspiState, requestSecurityTokenResponse, true, false);
				}
				else
				{
					base.StandardsManager.TrustDriver.OnRSTRorRSTRCMissingException();
				}
				incomingMessage.ReadFromBodyContentsToEnd(readerAtBodyContents);
			}
			if (requestSecurityTokenResponse.Context != sspiState.Context)
			{
				throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("BadSecurityNegotiationContext")), incomingMessage);
			}
			BinaryNegotiation binaryNegotiation = requestSecurityTokenResponse.GetBinaryNegotiation();
			byte[] array;
			if (binaryNegotiation != null)
			{
				this.ValidateIncomingBinaryNegotiation(binaryNegotiation);
				array = binaryNegotiation.GetNegotiationData();
			}
			else
			{
				array = null;
			}
			if (array == null && !sspiState.SspiNegotiation.IsCompleted)
			{
				throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NoBinaryNegoToReceive")), incomingMessage);
			}
			BodyWriter result;
			if (array == null && sspiState.SspiNegotiation.IsCompleted)
			{
				this.OnNegotiationComplete(sspiState, requestSecurityTokenResponse, requestSecurityTokenResponse2);
				result = null;
			}
			else
			{
				byte[] outgoingBlob = sspiState.SspiNegotiation.GetOutgoingBlob(array, SecurityUtils.GetChannelBindingFromMessage(incomingMessage), null);
				if (outgoingBlob == null && !sspiState.SspiNegotiation.IsCompleted)
				{
					throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NoBinaryNegoToSend")), incomingMessage);
				}
				if (outgoingBlob == null && sspiState.SspiNegotiation.IsCompleted)
				{
					this.OnNegotiationComplete(sspiState, requestSecurityTokenResponse, requestSecurityTokenResponse2);
					result = null;
				}
				else
				{
					result = this.PrepareRstr(sspiState, outgoingBlob);
				}
			}
			return result;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00064120 File Offset: 0x00062320
		private void OnNegotiationComplete(SspiNegotiationTokenProviderState sspiState, RequestSecurityTokenResponse negotiationRstr, RequestSecurityTokenResponse authenticatorRstr)
		{
			ISspiNegotiation sspiNegotiation = sspiState.SspiNegotiation;
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies = this.ValidateSspiNegotiation(sspiNegotiation);
			SecurityTokenResolver resolver = new SspiNegotiationTokenProvider.SspiSecurityTokenResolver(sspiNegotiation);
			GenericXmlSecurityToken issuedToken = negotiationRstr.GetIssuedToken(resolver, EmptyReadOnlyCollection<SecurityTokenAuthenticator>.Instance, SecurityKeyEntropyMode.ServerEntropy, null, base.SecurityContextTokenUri, authorizationPolicies, 0, false);
			if (issuedToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NoServiceTokenReceived")));
			}
			WrappedKeySecurityToken wrappedKeySecurityToken = issuedToken.ProofToken as WrappedKeySecurityToken;
			if (wrappedKeySecurityToken == null || wrappedKeySecurityToken.WrappingAlgorithm != sspiNegotiation.KeyEncryptionAlgorithm)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("ProofTokenWasNotWrappedCorrectly")));
			}
			byte[] wrappedKey = wrappedKeySecurityToken.GetWrappedKey();
			if (authenticatorRstr == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("RSTRAuthenticatorNotPresent")));
			}
			byte[] authenticator = authenticatorRstr.GetAuthenticator();
			if (authenticator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("RSTRAuthenticatorNotPresent")));
			}
			if (!SspiNegotiationTokenProvider.IsCorrectAuthenticator(sspiState, wrappedKey, authenticator))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("RSTRAuthenticatorIncorrect")));
			}
			sspiState.SetServiceToken(issuedToken);
		}

		// Token: 0x04001D2F RID: 7471
		private bool negotiateTokenOnOpen;

		// Token: 0x04001D30 RID: 7472
		private SecurityBindingElement securityBindingElement;

		// Token: 0x02000B68 RID: 2920
		private class GetOutgoingBlobProxy
		{
			// Token: 0x06007250 RID: 29264 RVA: 0x001AADE1 File Offset: 0x001A8FE1
			public GetOutgoingBlobProxy(SspiNegotiationTokenProviderState sspiState, SspiNegotiationTokenProvider sspiProvider, RequestSecurityToken rst)
			{
				this._sspiState = sspiState;
				this._sspiProvider = sspiProvider;
				this._rst = rst;
			}

			// Token: 0x06007251 RID: 29265 RVA: 0x001AAE00 File Offset: 0x001A9000
			public void GetOutgoingBlob(ChannelBinding channelBinding)
			{
				byte[] outgoingBlob = this._sspiState.SspiNegotiation.GetOutgoingBlob(null, channelBinding, null);
				if (outgoingBlob == null && !this._sspiState.SspiNegotiation.IsCompleted)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NoBinaryNegoToSend")));
				}
				this._rst.SetBinaryNegotiation(new BinaryNegotiation(this._sspiProvider.NegotiationValueType, outgoingBlob));
				SspiNegotiationTokenProvider.AddToDigest(this._sspiState, this._rst);
				this._rst.MakeReadOnly();
			}

			// Token: 0x040040B0 RID: 16560
			private RequestSecurityToken _rst;

			// Token: 0x040040B1 RID: 16561
			private SspiNegotiationTokenProvider _sspiProvider;

			// Token: 0x040040B2 RID: 16562
			private SspiNegotiationTokenProviderState _sspiState;
		}

		// Token: 0x02000B69 RID: 2921
		private class SspiSecurityTokenResolver : SecurityTokenResolver, ISspiNegotiationInfo
		{
			// Token: 0x06007252 RID: 29266 RVA: 0x001AAE88 File Offset: 0x001A9088
			public SspiSecurityTokenResolver(ISspiNegotiation sspiNegotiation)
			{
				this.sspiNegotiation = sspiNegotiation;
			}

			// Token: 0x17001A85 RID: 6789
			// (get) Token: 0x06007253 RID: 29267 RVA: 0x001AAE97 File Offset: 0x001A9097
			public ISspiNegotiation SspiNegotiation
			{
				get
				{
					return this.sspiNegotiation;
				}
			}

			// Token: 0x06007254 RID: 29268 RVA: 0x001AAE9F File Offset: 0x001A909F
			protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
			{
				token = null;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x06007255 RID: 29269 RVA: 0x001AAEB3 File Offset: 0x001A90B3
			protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
			{
				token = null;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x06007256 RID: 29270 RVA: 0x001AAEC7 File Offset: 0x001A90C7
			protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
			{
				key = null;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x040040B3 RID: 16563
			private ISspiNegotiation sspiNegotiation;
		}
	}
}
