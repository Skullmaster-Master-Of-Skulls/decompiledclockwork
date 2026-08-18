using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000307 RID: 775
	internal abstract class SspiNegotiationTokenAuthenticator : NegotiationTokenAuthenticator<SspiNegotiationTokenAuthenticatorState>
	{
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x0006310C File Offset: 0x0006130C
		// (set) Token: 0x06001A85 RID: 6789 RVA: 0x00063114 File Offset: 0x00061314
		public ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.extendedProtectionPolicy;
			}
			set
			{
				this.extendedProtectionPolicy = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0006311D File Offset: 0x0006131D
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00063128 File Offset: 0x00061328
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0006319C File Offset: 0x0006139C
		public string DefaultServiceBinding
		{
			get
			{
				if (this.defaultServiceBinding == null)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.defaultServiceBinding == null)
						{
							this.defaultServiceBinding = SecurityUtils.GetSpnFromIdentity(SecurityUtils.CreateWindowsIdentity(), new EndpointAddress(base.ListenUri, new AddressHeader[0]));
						}
					}
				}
				return this.defaultServiceBinding;
			}
			set
			{
				this.defaultServiceBinding = value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001A89 RID: 6793
		public abstract XmlDictionaryString NegotiationValueType { get; }

		// Token: 0x06001A8A RID: 6794
		protected abstract ReadOnlyCollection<IAuthorizationPolicy> ValidateSspiNegotiation(ISspiNegotiation sspiNegotiation);

		// Token: 0x06001A8B RID: 6795
		protected abstract SspiNegotiationTokenAuthenticatorState CreateSspiState(byte[] incomingBlob, string incomingValueTypeUri);

		// Token: 0x06001A8C RID: 6796 RVA: 0x000631A8 File Offset: 0x000613A8
		protected virtual void IssueServiceToken(SspiNegotiationTokenAuthenticatorState sspiState, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, out SecurityContextSecurityToken serviceToken, out WrappedKeySecurityToken proofToken, out int issuedKeySize)
		{
			UniqueId contextId = SecurityUtils.GenerateUniqueId();
			string id = SecurityUtils.GenerateId();
			if (sspiState.RequestedKeySize == 0)
			{
				issuedKeySize = base.SecurityAlgorithmSuite.DefaultSymmetricKeyLength;
			}
			else
			{
				issuedKeySize = sspiState.RequestedKeySize;
			}
			byte[] array = new byte[issuedKeySize / 8];
			CryptoHelper.FillRandomBytes(array);
			DateTime utcNow = DateTime.UtcNow;
			DateTime tokenExpirationTime = TimeoutHelper.Add(utcNow, base.ServiceTokenLifetime);
			serviceToken = base.IssueSecurityContextToken(contextId, id, array, utcNow, tokenExpirationTime, authorizationPolicies, base.EncryptStateInServiceToken);
			proofToken = new WrappedKeySecurityToken(string.Empty, array, sspiState.SspiNegotiation);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00063230 File Offset: 0x00061430
		protected virtual void ValidateIncomingBinaryNegotiation(BinaryNegotiation incomingNego)
		{
			if (incomingNego == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NoBinaryNegoToReceive")));
			}
			incomingNego.Validate(this.NegotiationValueType);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0006325B File Offset: 0x0006145B
		protected virtual BinaryNegotiation GetOutgoingBinaryNegotiation(ISspiNegotiation sspiNegotiation, byte[] outgoingBlob)
		{
			return new BinaryNegotiation(this.NegotiationValueType, outgoingBlob);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0006326C File Offset: 0x0006146C
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

		// Token: 0x06001A90 RID: 6800 RVA: 0x000632D4 File Offset: 0x000614D4
		private static void AddToDigest(SspiNegotiationTokenAuthenticatorState sspiState, RequestSecurityToken rst)
		{
			MemoryStream stream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream);
			rst.RequestSecurityTokenXml.WriteTo(xmlDictionaryWriter);
			xmlDictionaryWriter.Flush();
			SspiNegotiationTokenAuthenticator.AddToDigest(sspiState.NegotiationDigest, stream);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0006330C File Offset: 0x0006150C
		private static void AddToDigest(SspiNegotiationTokenAuthenticatorState sspiState, RequestSecurityTokenResponse rstr, bool wasReceived)
		{
			MemoryStream stream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream);
			if (wasReceived)
			{
				rstr.RequestSecurityTokenResponseXml.WriteTo(xmlDictionaryWriter);
			}
			else
			{
				rstr.WriteTo(xmlDictionaryWriter);
			}
			xmlDictionaryWriter.Flush();
			SspiNegotiationTokenAuthenticator.AddToDigest(sspiState.NegotiationDigest, stream);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00063350 File Offset: 0x00061550
		private static byte[] ComputeAuthenticator(SspiNegotiationTokenAuthenticatorState sspiState, byte[] key)
		{
			HashAlgorithm negotiationDigest = sspiState.NegotiationDigest;
			byte[] hash;
			lock (negotiationDigest)
			{
				sspiState.NegotiationDigest.TransformFinalBlock(CryptoHelper.EmptyBuffer, 0, 0);
				hash = sspiState.NegotiationDigest.Hash;
			}
			Psha1DerivedKeyGenerator psha1DerivedKeyGenerator = new Psha1DerivedKeyGenerator(key);
			return psha1DerivedKeyGenerator.GenerateDerivedKey(SecurityUtils.CombinedHashLabel, hash, 256, 0);
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x000633C4 File Offset: 0x000615C4
		protected override bool IsMultiLegNegotiation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000633C7 File Offset: 0x000615C7
		protected override Binding GetNegotiationBinding(Binding binding)
		{
			return binding;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000633CA File Offset: 0x000615CA
		protected override MessageFilter GetListenerFilter()
		{
			return new SspiNegotiationTokenAuthenticator.SspiNegotiationFilter(this);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000633D4 File Offset: 0x000615D4
		protected override BodyWriter ProcessRequestSecurityToken(Message request, RequestSecurityToken requestSecurityToken, out SspiNegotiationTokenAuthenticatorState negotiationState)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			if (requestSecurityToken == null)
			{
				throw TraceUtility.ThrowHelperArgumentNull("requestSecurityToken", request);
			}
			if (requestSecurityToken.RequestType != null && requestSecurityToken.RequestType != base.StandardsManager.TrustDriver.RequestTypeIssue)
			{
				throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("InvalidRstRequestType", new object[]
				{
					requestSecurityToken.RequestType
				})), request);
			}
			BinaryNegotiation binaryNegotiation = requestSecurityToken.GetBinaryNegotiation();
			this.ValidateIncomingBinaryNegotiation(binaryNegotiation);
			negotiationState = this.CreateSspiState(binaryNegotiation.GetNegotiationData(), binaryNegotiation.ValueTypeUri);
			SspiNegotiationTokenAuthenticator.AddToDigest(negotiationState, requestSecurityToken);
			negotiationState.Context = requestSecurityToken.Context;
			if (requestSecurityToken.KeySize != 0)
			{
				WSTrust.Driver.ValidateRequestedKeySize(requestSecurityToken.KeySize, base.SecurityAlgorithmSuite);
			}
			negotiationState.RequestedKeySize = requestSecurityToken.KeySize;
			string a;
			string a2;
			requestSecurityToken.GetAppliesToQName(out a, out a2);
			if (a == "EndpointReference" && a2 == request.Version.Addressing.Namespace)
			{
				DataContractSerializer dataContractSerializer;
				if (request.Version.Addressing == AddressingVersion.WSAddressing10)
				{
					dataContractSerializer = DataContractSerializerDefaults.CreateSerializer(typeof(EndpointAddress10), int.MaxValue);
					negotiationState.AppliesTo = requestSecurityToken.GetAppliesTo<EndpointAddress10>(dataContractSerializer).ToEndpointAddress();
				}
				else
				{
					if (request.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
						{
							request.Version.Addressing
						})));
					}
					dataContractSerializer = DataContractSerializerDefaults.CreateSerializer(typeof(EndpointAddressAugust2004), int.MaxValue);
					negotiationState.AppliesTo = requestSecurityToken.GetAppliesTo<EndpointAddressAugust2004>(dataContractSerializer).ToEndpointAddress();
				}
				negotiationState.AppliesToSerializer = dataContractSerializer;
			}
			return this.ProcessNegotiation(negotiationState, request, binaryNegotiation);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00063598 File Offset: 0x00061798
		protected override BodyWriter ProcessRequestSecurityTokenResponse(SspiNegotiationTokenAuthenticatorState negotiationState, Message request, RequestSecurityTokenResponse requestSecurityTokenResponse)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			if (requestSecurityTokenResponse == null)
			{
				throw TraceUtility.ThrowHelperArgumentNull("requestSecurityTokenResponse", request);
			}
			if (requestSecurityTokenResponse.Context != negotiationState.Context)
			{
				throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("BadSecurityNegotiationContext")), request);
			}
			SspiNegotiationTokenAuthenticator.AddToDigest(negotiationState, requestSecurityTokenResponse, true);
			BinaryNegotiation binaryNegotiation = requestSecurityTokenResponse.GetBinaryNegotiation();
			this.ValidateIncomingBinaryNegotiation(binaryNegotiation);
			return this.ProcessNegotiation(negotiationState, request, binaryNegotiation);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00063610 File Offset: 0x00061810
		private BodyWriter ProcessNegotiation(SspiNegotiationTokenAuthenticatorState negotiationState, Message incomingMessage, BinaryNegotiation incomingNego)
		{
			ISspiNegotiation sspiNegotiation = negotiationState.SspiNegotiation;
			byte[] outgoingBlob = sspiNegotiation.GetOutgoingBlob(incomingNego.GetNegotiationData(), SecurityUtils.GetChannelBindingFromMessage(incomingMessage), this.extendedProtectionPolicy);
			if (!sspiNegotiation.IsValidContext)
			{
				throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidSspiNegotiation")), incomingMessage);
			}
			if (outgoingBlob == null && !sspiNegotiation.IsCompleted)
			{
				throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NoBinaryNegoToSend")), incomingMessage);
			}
			BinaryNegotiation binaryNegotiation;
			if (outgoingBlob != null)
			{
				binaryNegotiation = this.GetOutgoingBinaryNegotiation(sspiNegotiation, outgoingBlob);
			}
			else
			{
				binaryNegotiation = null;
			}
			BodyWriter result;
			if (sspiNegotiation.IsCompleted)
			{
				ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies = this.ValidateSspiNegotiation(sspiNegotiation);
				SecurityContextSecurityToken securityContextSecurityToken;
				WrappedKeySecurityToken requestedProofToken;
				int keySize;
				this.IssueServiceToken(negotiationState, authorizationPolicies, out securityContextSecurityToken, out requestedProofToken, out keySize);
				negotiationState.SetServiceToken(securityContextSecurityToken);
				SecurityKeyIdentifierClause requestedUnattachedReference = base.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(securityContextSecurityToken, SecurityTokenReferenceStyle.External);
				SecurityKeyIdentifierClause requestedAttachedReference = base.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(securityContextSecurityToken, SecurityTokenReferenceStyle.Internal);
				RequestSecurityTokenResponse requestSecurityTokenResponse = new RequestSecurityTokenResponse(base.StandardsManager);
				requestSecurityTokenResponse.Context = negotiationState.Context;
				requestSecurityTokenResponse.KeySize = keySize;
				requestSecurityTokenResponse.TokenType = base.SecurityContextTokenUri;
				if (binaryNegotiation != null)
				{
					requestSecurityTokenResponse.SetBinaryNegotiation(binaryNegotiation);
				}
				requestSecurityTokenResponse.RequestedUnattachedReference = requestedUnattachedReference;
				requestSecurityTokenResponse.RequestedAttachedReference = requestedAttachedReference;
				requestSecurityTokenResponse.SetLifetime(securityContextSecurityToken.ValidFrom, securityContextSecurityToken.ValidTo);
				if (negotiationState.AppliesTo != null)
				{
					if (incomingMessage.Version.Addressing == AddressingVersion.WSAddressing10)
					{
						requestSecurityTokenResponse.SetAppliesTo<EndpointAddress10>(EndpointAddress10.FromEndpointAddress(negotiationState.AppliesTo), negotiationState.AppliesToSerializer);
					}
					else
					{
						if (incomingMessage.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
							{
								incomingMessage.Version.Addressing
							})));
						}
						requestSecurityTokenResponse.SetAppliesTo<EndpointAddressAugust2004>(EndpointAddressAugust2004.FromEndpointAddress(negotiationState.AppliesTo), negotiationState.AppliesToSerializer);
					}
				}
				requestSecurityTokenResponse.MakeReadOnly();
				SspiNegotiationTokenAuthenticator.AddToDigest(negotiationState, requestSecurityTokenResponse, false);
				RequestSecurityTokenResponse requestSecurityTokenResponse2 = new RequestSecurityTokenResponse(base.StandardsManager);
				requestSecurityTokenResponse2.RequestedSecurityToken = securityContextSecurityToken;
				requestSecurityTokenResponse2.RequestedProofToken = requestedProofToken;
				requestSecurityTokenResponse2.Context = negotiationState.Context;
				requestSecurityTokenResponse2.KeySize = keySize;
				requestSecurityTokenResponse2.TokenType = base.SecurityContextTokenUri;
				if (binaryNegotiation != null)
				{
					requestSecurityTokenResponse2.SetBinaryNegotiation(binaryNegotiation);
				}
				requestSecurityTokenResponse2.RequestedAttachedReference = requestedAttachedReference;
				requestSecurityTokenResponse2.RequestedUnattachedReference = requestedUnattachedReference;
				if (negotiationState.AppliesTo != null)
				{
					if (incomingMessage.Version.Addressing == AddressingVersion.WSAddressing10)
					{
						requestSecurityTokenResponse2.SetAppliesTo<EndpointAddress10>(EndpointAddress10.FromEndpointAddress(negotiationState.AppliesTo), negotiationState.AppliesToSerializer);
					}
					else
					{
						if (incomingMessage.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
							{
								incomingMessage.Version.Addressing
							})));
						}
						requestSecurityTokenResponse2.SetAppliesTo<EndpointAddressAugust2004>(EndpointAddressAugust2004.FromEndpointAddress(negotiationState.AppliesTo), negotiationState.AppliesToSerializer);
					}
				}
				requestSecurityTokenResponse2.MakeReadOnly();
				byte[] authenticator = SspiNegotiationTokenAuthenticator.ComputeAuthenticator(negotiationState, securityContextSecurityToken.GetKeyBytes());
				RequestSecurityTokenResponse requestSecurityTokenResponse3 = new RequestSecurityTokenResponse(base.StandardsManager);
				requestSecurityTokenResponse3.Context = negotiationState.Context;
				requestSecurityTokenResponse3.SetAuthenticator(authenticator);
				requestSecurityTokenResponse3.MakeReadOnly();
				result = new RequestSecurityTokenResponseCollection(new List<RequestSecurityTokenResponse>(2)
				{
					requestSecurityTokenResponse2,
					requestSecurityTokenResponse3
				}, base.StandardsManager);
			}
			else
			{
				RequestSecurityTokenResponse requestSecurityTokenResponse4 = new RequestSecurityTokenResponse(base.StandardsManager);
				requestSecurityTokenResponse4.Context = negotiationState.Context;
				requestSecurityTokenResponse4.SetBinaryNegotiation(binaryNegotiation);
				requestSecurityTokenResponse4.MakeReadOnly();
				SspiNegotiationTokenAuthenticator.AddToDigest(negotiationState, requestSecurityTokenResponse4, false);
				result = requestSecurityTokenResponse4;
			}
			return result;
		}

		// Token: 0x04001D26 RID: 7462
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x04001D27 RID: 7463
		private string defaultServiceBinding;

		// Token: 0x04001D28 RID: 7464
		private object thisLock = new object();

		// Token: 0x02000B67 RID: 2919
		private class SspiNegotiationFilter : HeaderFilter
		{
			// Token: 0x0600724E RID: 29262 RVA: 0x001AAD6F File Offset: 0x001A8F6F
			public SspiNegotiationFilter(SspiNegotiationTokenAuthenticator authenticator)
			{
				this.authenticator = authenticator;
			}

			// Token: 0x0600724F RID: 29263 RVA: 0x001AAD80 File Offset: 0x001A8F80
			public override bool Match(Message message)
			{
				return (message.Headers.Action == this.authenticator.RequestSecurityTokenAction.Value || message.Headers.Action == this.authenticator.RequestSecurityTokenResponseAction.Value) && !SecurityVersion.Default.DoesMessageContainSecurityHeader(message);
			}

			// Token: 0x040040AF RID: 16559
			private SspiNegotiationTokenAuthenticator authenticator;
		}
	}
}
