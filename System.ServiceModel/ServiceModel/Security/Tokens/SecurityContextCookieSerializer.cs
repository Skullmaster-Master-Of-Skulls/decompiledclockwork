using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000395 RID: 917
	internal struct SecurityContextCookieSerializer
	{
		// Token: 0x060021E6 RID: 8678 RVA: 0x0007C0F6 File Offset: 0x0007A2F6
		public SecurityContextCookieSerializer(SecurityStateEncoder securityStateEncoder, IList<Type> knownTypes)
		{
			if (securityStateEncoder == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityStateEncoder");
			}
			this.securityStateEncoder = securityStateEncoder;
			this.knownTypes = (knownTypes ?? new List<Type>());
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0007C124 File Offset: 0x0007A324
		private SecurityContextSecurityToken DeserializeContext(byte[] serializedContext, byte[] cookieBlob, string id, XmlDictionaryReaderQuotas quotas)
		{
			SctClaimDictionary instance = SctClaimDictionary.Instance;
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(serializedContext, 0, serializedContext.Length, instance, quotas, null, null);
			int num = -1;
			UniqueId uniqueId = null;
			DateTime minUtcDateTime = SecurityUtils.MinUtcDateTime;
			DateTime maxUtcDateTime = SecurityUtils.MaxUtcDateTime;
			byte[] array = null;
			string text = null;
			UniqueId keyGeneration = null;
			DateTime minUtcDateTime2 = SecurityUtils.MinUtcDateTime;
			DateTime maxUtcDateTime2 = SecurityUtils.MaxUtcDateTime;
			List<ClaimSet> list = null;
			IList<IIdentity> identities = null;
			bool isCookieMode = true;
			xmlDictionaryReader.ReadFullStartElement(instance.SecurityContextSecurityToken, instance.EmptyString);
			while (xmlDictionaryReader.IsStartElement())
			{
				if (xmlDictionaryReader.IsStartElement(instance.Version, instance.EmptyString))
				{
					num = xmlDictionaryReader.ReadElementContentAsInt();
				}
				else if (xmlDictionaryReader.IsStartElement(instance.ContextId, instance.EmptyString))
				{
					uniqueId = xmlDictionaryReader.ReadElementContentAsUniqueId();
				}
				else if (xmlDictionaryReader.IsStartElement(instance.Id, instance.EmptyString))
				{
					text = xmlDictionaryReader.ReadElementContentAsString();
				}
				else if (xmlDictionaryReader.IsStartElement(instance.EffectiveTime, instance.EmptyString))
				{
					minUtcDateTime = new DateTime(XmlHelper.ReadElementContentAsInt64(xmlDictionaryReader), DateTimeKind.Utc);
				}
				else if (xmlDictionaryReader.IsStartElement(instance.ExpiryTime, instance.EmptyString))
				{
					maxUtcDateTime = new DateTime(XmlHelper.ReadElementContentAsInt64(xmlDictionaryReader), DateTimeKind.Utc);
				}
				else if (xmlDictionaryReader.IsStartElement(instance.Key, instance.EmptyString))
				{
					array = xmlDictionaryReader.ReadElementContentAsBase64();
				}
				else if (xmlDictionaryReader.IsStartElement(instance.KeyGeneration, instance.EmptyString))
				{
					keyGeneration = xmlDictionaryReader.ReadElementContentAsUniqueId();
				}
				else if (xmlDictionaryReader.IsStartElement(instance.KeyEffectiveTime, instance.EmptyString))
				{
					minUtcDateTime2 = new DateTime(XmlHelper.ReadElementContentAsInt64(xmlDictionaryReader), DateTimeKind.Utc);
				}
				else if (xmlDictionaryReader.IsStartElement(instance.KeyExpiryTime, instance.EmptyString))
				{
					maxUtcDateTime2 = new DateTime(XmlHelper.ReadElementContentAsInt64(xmlDictionaryReader), DateTimeKind.Utc);
				}
				else if (xmlDictionaryReader.IsStartElement(instance.Identities, instance.EmptyString))
				{
					identities = SctClaimSerializer.DeserializeIdentities(xmlDictionaryReader, instance, DataContractSerializerDefaults.CreateSerializer(typeof(IIdentity), this.knownTypes, int.MaxValue));
				}
				else if (xmlDictionaryReader.IsStartElement(instance.ClaimSets, instance.EmptyString))
				{
					xmlDictionaryReader.ReadStartElement();
					DataContractSerializer serializer = DataContractSerializerDefaults.CreateSerializer(typeof(ClaimSet), this.knownTypes, int.MaxValue);
					DataContractSerializer claimSerializer = DataContractSerializerDefaults.CreateSerializer(typeof(Claim), this.knownTypes, int.MaxValue);
					list = new List<ClaimSet>(1);
					while (xmlDictionaryReader.IsStartElement())
					{
						list.Add(SctClaimSerializer.DeserializeClaimSet(xmlDictionaryReader, instance, serializer, claimSerializer));
					}
					xmlDictionaryReader.ReadEndElement();
				}
				else if (xmlDictionaryReader.IsStartElement(instance.IsCookieMode, instance.EmptyString))
				{
					isCookieMode = (xmlDictionaryReader.ReadElementString() == "1");
				}
				else
				{
					SecurityContextCookieSerializer.OnInvalidCookieFailure(SR.GetString("SctCookieXmlParseError"));
				}
			}
			xmlDictionaryReader.ReadEndElement();
			if (num != 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SerializedTokenVersionUnsupported", new object[]
				{
					num
				})));
			}
			if (uniqueId == null)
			{
				SecurityContextCookieSerializer.OnInvalidCookieFailure(SR.GetString("SctCookieValueMissingOrIncorrect", new object[]
				{
					"ContextId"
				}));
			}
			if (array == null || array.Length == 0)
			{
				SecurityContextCookieSerializer.OnInvalidCookieFailure(SR.GetString("SctCookieValueMissingOrIncorrect", new object[]
				{
					"Key"
				}));
			}
			if (text != id)
			{
				SecurityContextCookieSerializer.OnInvalidCookieFailure(SR.GetString("SctCookieValueMissingOrIncorrect", new object[]
				{
					"Id"
				}));
			}
			List<IAuthorizationPolicy> list2;
			if (list != null)
			{
				list2 = new List<IAuthorizationPolicy>(1);
				list2.Add(new SecurityContextCookieSerializer.SctUnconditionalPolicy(identities, list, maxUtcDateTime));
			}
			else
			{
				list2 = null;
			}
			return new SecurityContextSecurityToken(uniqueId, text, array, minUtcDateTime, maxUtcDateTime, (list2 != null) ? list2.AsReadOnly() : null, isCookieMode, cookieBlob, keyGeneration, minUtcDateTime2, maxUtcDateTime2);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0007C4C0 File Offset: 0x0007A6C0
		public byte[] CreateCookieFromSecurityContext(UniqueId contextId, string id, byte[] key, DateTime tokenEffectiveTime, DateTime tokenExpirationTime, UniqueId keyGeneration, DateTime keyEffectiveTime, DateTime keyExpirationTime, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			if (contextId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contextId");
			}
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream, SctClaimDictionary.Instance, null);
			SctClaimDictionary instance = SctClaimDictionary.Instance;
			xmlDictionaryWriter.WriteStartElement(instance.SecurityContextSecurityToken, instance.EmptyString);
			xmlDictionaryWriter.WriteStartElement(instance.Version, instance.EmptyString);
			xmlDictionaryWriter.WriteValue(1);
			xmlDictionaryWriter.WriteEndElement();
			if (id != null)
			{
				xmlDictionaryWriter.WriteElementString(instance.Id, instance.EmptyString, id);
			}
			XmlHelper.WriteElementStringAsUniqueId(xmlDictionaryWriter, instance.ContextId, instance.EmptyString, contextId);
			xmlDictionaryWriter.WriteStartElement(instance.Key, instance.EmptyString);
			xmlDictionaryWriter.WriteBase64(key, 0, key.Length);
			xmlDictionaryWriter.WriteEndElement();
			if (keyGeneration != null)
			{
				XmlHelper.WriteElementStringAsUniqueId(xmlDictionaryWriter, instance.KeyGeneration, instance.EmptyString, keyGeneration);
			}
			XmlHelper.WriteElementContentAsInt64(xmlDictionaryWriter, instance.EffectiveTime, instance.EmptyString, tokenEffectiveTime.ToUniversalTime().Ticks);
			XmlHelper.WriteElementContentAsInt64(xmlDictionaryWriter, instance.ExpiryTime, instance.EmptyString, tokenExpirationTime.ToUniversalTime().Ticks);
			XmlHelper.WriteElementContentAsInt64(xmlDictionaryWriter, instance.KeyEffectiveTime, instance.EmptyString, keyEffectiveTime.ToUniversalTime().Ticks);
			XmlHelper.WriteElementContentAsInt64(xmlDictionaryWriter, instance.KeyExpiryTime, instance.EmptyString, keyExpirationTime.ToUniversalTime().Ticks);
			AuthorizationContext authorizationContext = null;
			if (authorizationPolicies != null)
			{
				authorizationContext = AuthorizationContext.CreateDefaultAuthorizationContext(authorizationPolicies);
			}
			if (authorizationContext != null && authorizationContext.ClaimSets.Count != 0)
			{
				DataContractSerializer serializer = DataContractSerializerDefaults.CreateSerializer(typeof(IIdentity), this.knownTypes, int.MaxValue);
				DataContractSerializer serializer2 = DataContractSerializerDefaults.CreateSerializer(typeof(ClaimSet), this.knownTypes, int.MaxValue);
				DataContractSerializer claimSerializer = DataContractSerializerDefaults.CreateSerializer(typeof(Claim), this.knownTypes, int.MaxValue);
				SctClaimSerializer.SerializeIdentities(authorizationContext, instance, xmlDictionaryWriter, serializer);
				xmlDictionaryWriter.WriteStartElement(instance.ClaimSets, instance.EmptyString);
				for (int i = 0; i < authorizationContext.ClaimSets.Count; i++)
				{
					SctClaimSerializer.SerializeClaimSet(authorizationContext.ClaimSets[i], instance, xmlDictionaryWriter, serializer2, claimSerializer);
				}
				xmlDictionaryWriter.WriteEndElement();
			}
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.Flush();
			byte[] data = memoryStream.ToArray();
			return this.securityStateEncoder.EncodeSecurityState(data);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0007C724 File Offset: 0x0007A924
		public SecurityContextSecurityToken CreateSecurityContextFromCookie(byte[] encodedCookie, UniqueId contextId, UniqueId generation, string id, XmlDictionaryReaderQuotas quotas)
		{
			byte[] serializedContext = null;
			try
			{
				serializedContext = this.securityStateEncoder.DecodeSecurityState(encodedCookie);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				SecurityContextCookieSerializer.OnInvalidCookieFailure(SR.GetString("SctCookieBlobDecodeFailure"), ex);
			}
			SecurityContextSecurityToken securityContextSecurityToken = this.DeserializeContext(serializedContext, encodedCookie, id, quotas);
			if (securityContextSecurityToken.ContextId != contextId)
			{
				SecurityContextCookieSerializer.OnInvalidCookieFailure(SR.GetString("SctCookieValueMissingOrIncorrect", new object[]
				{
					"ContextId"
				}));
			}
			if (securityContextSecurityToken.KeyGeneration != generation)
			{
				SecurityContextCookieSerializer.OnInvalidCookieFailure(SR.GetString("SctCookieValueMissingOrIncorrect", new object[]
				{
					"KeyGeneration"
				}));
			}
			return securityContextSecurityToken;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0007C7D4 File Offset: 0x0007A9D4
		internal static void OnInvalidCookieFailure(string reason)
		{
			SecurityContextCookieSerializer.OnInvalidCookieFailure(reason, null);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0007C7DD File Offset: 0x0007A9DD
		internal static void OnInvalidCookieFailure(string reason, Exception e)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("InvalidSecurityContextCookie", new object[]
			{
				reason
			}), e));
		}

		// Token: 0x04001F86 RID: 8070
		private const int SupportedPersistanceVersion = 1;

		// Token: 0x04001F87 RID: 8071
		private SecurityStateEncoder securityStateEncoder;

		// Token: 0x04001F88 RID: 8072
		private IList<Type> knownTypes;

		// Token: 0x02000B9A RID: 2970
		private class SctUnconditionalPolicy : IAuthorizationPolicy, IAuthorizationComponent
		{
			// Token: 0x0600736A RID: 29546 RVA: 0x001AE8DC File Offset: 0x001ACADC
			public SctUnconditionalPolicy(IList<IIdentity> identities, IList<ClaimSet> claimSets, DateTime expirationTime)
			{
				this.identities = identities;
				this.claimSets = claimSets;
				this.expirationTime = expirationTime;
			}

			// Token: 0x17001AC1 RID: 6849
			// (get) Token: 0x0600736B RID: 29547 RVA: 0x001AE904 File Offset: 0x001ACB04
			public string Id
			{
				get
				{
					return this.id.Value;
				}
			}

			// Token: 0x17001AC2 RID: 6850
			// (get) Token: 0x0600736C RID: 29548 RVA: 0x001AE911 File Offset: 0x001ACB11
			public ClaimSet Issuer
			{
				get
				{
					return ClaimSet.System;
				}
			}

			// Token: 0x0600736D RID: 29549 RVA: 0x001AE918 File Offset: 0x001ACB18
			public bool Evaluate(EvaluationContext evaluationContext, ref object state)
			{
				for (int i = 0; i < this.claimSets.Count; i++)
				{
					evaluationContext.AddClaimSet(this, this.claimSets[i]);
				}
				if (this.identities != null)
				{
					object obj;
					if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
					{
						evaluationContext.Properties.Add("Identities", this.identities);
					}
					else
					{
						List<IIdentity> list = obj as List<IIdentity>;
						if (list != null)
						{
							list.AddRange(this.identities);
						}
					}
				}
				evaluationContext.RecordExpirationTime(this.expirationTime);
				return true;
			}

			// Token: 0x0400415B RID: 16731
			private SecurityUniqueId id = SecurityUniqueId.Create();

			// Token: 0x0400415C RID: 16732
			private IList<IIdentity> identities;

			// Token: 0x0400415D RID: 16733
			private IList<ClaimSet> claimSets;

			// Token: 0x0400415E RID: 16734
			private DateTime expirationTime;
		}
	}
}
