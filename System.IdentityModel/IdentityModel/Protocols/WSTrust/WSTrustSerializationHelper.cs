using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000215 RID: 533
	internal class WSTrustSerializationHelper
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x00048B30 File Offset: 0x00046D30
		public static RequestSecurityToken CreateRequest(XmlReader reader, WSTrustSerializationContext context, WSTrustRequestSerializer requestSerializer, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (requestSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestSerializer");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (!reader.IsStartElement(trustConstants.Elements.RequestSecurityToken, trustConstants.NamespaceURI))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3032", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI,
					trustConstants.Elements.RequestSecurityToken,
					trustConstants.NamespaceURI
				})));
			}
			bool isEmptyElement = reader.IsEmptyElement;
			RequestSecurityToken requestSecurityToken = requestSerializer.CreateRequestSecurityToken();
			requestSecurityToken.Context = reader.GetAttribute(trustConstants.Attributes.Context);
			reader.Read();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					requestSerializer.ReadXmlElement(reader, requestSecurityToken, context);
				}
				reader.ReadEndElement();
			}
			requestSerializer.Validate(requestSecurityToken);
			return requestSecurityToken;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00048C3C File Offset: 0x00046E3C
		public static void ReadRSTXml(XmlReader reader, RequestSecurityToken rst, WSTrustSerializationContext context, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (rst == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (reader.IsStartElement(trustConstants.Elements.TokenType, trustConstants.NamespaceURI))
			{
				rst.TokenType = reader.ReadElementContentAsString();
				if (!UriUtil.CanCreateValidUri(rst.TokenType, UriKind.Absolute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
					{
						trustConstants.Elements.TokenType,
						trustConstants.NamespaceURI,
						rst.TokenType
					})));
				}
				return;
			}
			else
			{
				if (reader.IsStartElement(trustConstants.Elements.RequestType, trustConstants.NamespaceURI))
				{
					rst.RequestType = WSTrustSerializationHelper.ReadRequestType(reader, trustConstants);
					return;
				}
				if (reader.IsStartElement("AppliesTo", "http://schemas.xmlsoap.org/ws/2004/09/policy"))
				{
					rst.AppliesTo = WSTrustSerializationHelper.ReadAppliesTo(reader, trustConstants);
					return;
				}
				if (reader.IsStartElement(trustConstants.Elements.Issuer, trustConstants.NamespaceURI))
				{
					rst.Issuer = WSTrustSerializationHelper.ReadOnBehalfOfIssuer(reader, trustConstants);
					return;
				}
				if (reader.IsStartElement(trustConstants.Elements.ProofEncryption, trustConstants.NamespaceURI))
				{
					if (!reader.IsEmptyElement)
					{
						rst.ProofEncryption = new SecurityTokenElement(WSTrustSerializationHelper.ReadInnerXml(reader), context.SecurityTokenHandlers);
					}
					else
					{
						reader.Read();
					}
					if (rst.ProofEncryption == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3218")));
					}
					return;
				}
				else if (reader.IsStartElement(trustConstants.Elements.Encryption, trustConstants.NamespaceURI))
				{
					if (!reader.IsEmptyElement)
					{
						rst.Encryption = new SecurityTokenElement(WSTrustSerializationHelper.ReadInnerXml(reader), context.SecurityTokenHandlers);
					}
					else
					{
						reader.Read();
					}
					if (rst.Encryption == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3268")));
					}
					return;
				}
				else if (reader.IsStartElement(trustConstants.Elements.DelegateTo, trustConstants.NamespaceURI))
				{
					if (!reader.IsEmptyElement)
					{
						rst.DelegateTo = new SecurityTokenElement(WSTrustSerializationHelper.ReadInnerXml(reader), context.SecurityTokenHandlers);
					}
					else
					{
						reader.Read();
					}
					if (rst.DelegateTo == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3219")));
					}
					return;
				}
				else if (reader.IsStartElement(trustConstants.Elements.Claims, trustConstants.NamespaceURI))
				{
					rst.Claims.Dialect = reader.GetAttribute(trustConstants.Attributes.Dialect);
					if (rst.Claims.Dialect != null && !UriUtil.CanCreateValidUri(rst.Claims.Dialect, UriKind.Absolute))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3136", new object[]
						{
							trustConstants.Attributes.Dialect,
							reader.LocalName,
							reader.NamespaceURI,
							rst.Claims.Dialect
						})));
					}
					string requestClaimNamespace = WSTrustSerializationHelper.GetRequestClaimNamespace(rst.Claims.Dialect);
					bool isEmptyElement = reader.IsEmptyElement;
					reader.ReadStartElement(trustConstants.Elements.Claims, trustConstants.NamespaceURI);
					if (!isEmptyElement)
					{
						while (reader.IsStartElement("ClaimType", requestClaimNamespace))
						{
							isEmptyElement = reader.IsEmptyElement;
							string attribute = reader.GetAttribute("Uri");
							if (string.IsNullOrEmpty(attribute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3009")));
							}
							bool isOptional = false;
							string attribute2 = reader.GetAttribute("Optional");
							if (!string.IsNullOrEmpty(attribute2))
							{
								isOptional = XmlConvert.ToBoolean(attribute2);
							}
							reader.Read();
							reader.MoveToContent();
							string value = null;
							if (!isEmptyElement)
							{
								if (reader.IsStartElement("Value", requestClaimNamespace))
								{
									if (!StringComparer.Ordinal.Equals(rst.Claims.Dialect, "http://docs.oasis-open.org/wsfed/authorization/200706/authclaims"))
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3258", new object[]
										{
											rst.Claims.Dialect,
											"http://docs.oasis-open.org/wsfed/authorization/200706/authclaims"
										})));
									}
									value = reader.ReadElementContentAsString("Value", requestClaimNamespace);
								}
								reader.ReadEndElement();
							}
							rst.Claims.Add(new RequestClaim(attribute, isOptional, value));
						}
						reader.ReadEndElement();
					}
					return;
				}
				else if (reader.IsStartElement(trustConstants.Elements.Entropy, trustConstants.NamespaceURI))
				{
					bool isEmptyElement = reader.IsEmptyElement;
					reader.ReadStartElement(trustConstants.Elements.Entropy, trustConstants.NamespaceURI);
					if (!isEmptyElement)
					{
						ProtectedKey protectedKey = WSTrustSerializationHelper.ReadProtectedKey(reader, context, trustConstants);
						if (protectedKey == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3026")));
						}
						rst.Entropy = new Entropy(protectedKey);
						reader.ReadEndElement();
					}
					if (rst.Entropy == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3026")));
					}
					return;
				}
				else
				{
					if (reader.IsStartElement(trustConstants.Elements.BinaryExchange, trustConstants.NamespaceURI))
					{
						rst.BinaryExchange = WSTrustSerializationHelper.ReadBinaryExchange(reader, trustConstants);
						return;
					}
					if (reader.IsStartElement(trustConstants.Elements.Lifetime, trustConstants.NamespaceURI))
					{
						rst.Lifetime = WSTrustSerializationHelper.ReadLifetime(reader, trustConstants);
						return;
					}
					if (reader.IsStartElement(trustConstants.Elements.RenewTarget, trustConstants.NamespaceURI))
					{
						if (!reader.IsEmptyElement)
						{
							rst.RenewTarget = new SecurityTokenElement(WSTrustSerializationHelper.ReadInnerXml(reader), context.SecurityTokenHandlers);
						}
						else
						{
							reader.Read();
						}
						if (rst.RenewTarget == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3151")));
						}
						return;
					}
					else if (reader.IsStartElement(trustConstants.Elements.OnBehalfOf, trustConstants.NamespaceURI))
					{
						if (!reader.IsEmptyElement)
						{
							if (!context.SecurityTokenHandlerCollectionManager.ContainsKey("OnBehalfOf"))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3264")));
							}
							rst.OnBehalfOf = new SecurityTokenElement(WSTrustSerializationHelper.ReadInnerXml(reader), context.SecurityTokenHandlerCollectionManager["OnBehalfOf"]);
						}
						else
						{
							reader.Read();
						}
						if (rst.OnBehalfOf == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3152")));
						}
						return;
					}
					else if (reader.IsStartElement("ActAs", "http://docs.oasis-open.org/ws-sx/ws-trust/200802"))
					{
						if (!reader.IsEmptyElement)
						{
							if (!context.SecurityTokenHandlerCollectionManager.ContainsKey("ActAs"))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3265")));
							}
							rst.ActAs = new SecurityTokenElement(WSTrustSerializationHelper.ReadInnerXml(reader), context.SecurityTokenHandlerCollectionManager["ActAs"]);
						}
						else
						{
							reader.Read();
						}
						if (rst.ActAs == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3153")));
						}
						return;
					}
					else
					{
						if (reader.IsStartElement(trustConstants.Elements.KeyType, trustConstants.NamespaceURI))
						{
							rst.KeyType = WSTrustSerializationHelper.ReadKeyType(reader, trustConstants);
							return;
						}
						if (reader.IsStartElement(trustConstants.Elements.KeySize, trustConstants.NamespaceURI))
						{
							if (!reader.IsEmptyElement)
							{
								rst.KeySizeInBits = new int?(int.Parse(reader.ReadElementContentAsString(), CultureInfo.InvariantCulture));
							}
							else
							{
								reader.Read();
							}
							if (rst.KeySizeInBits == null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3154")));
							}
							return;
						}
						else if (reader.IsStartElement(trustConstants.Elements.UseKey, trustConstants.NamespaceURI))
						{
							bool isEmptyElement = reader.IsEmptyElement;
							reader.ReadStartElement();
							if (!isEmptyElement)
							{
								if (!context.SecurityTokenHandlers.CanReadToken(reader))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3165")));
								}
								SecurityToken securityToken = context.SecurityTokenHandlers.ReadToken(reader);
								SecurityKeyIdentifier securityKeyIdentifier = new SecurityKeyIdentifier();
								if (securityToken.CanCreateKeyIdentifierClause<RsaKeyIdentifierClause>())
								{
									securityKeyIdentifier.Add(securityToken.CreateKeyIdentifierClause<RsaKeyIdentifierClause>());
								}
								else
								{
									if (!securityToken.CanCreateKeyIdentifierClause<X509RawDataKeyIdentifierClause>())
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3166")));
									}
									securityKeyIdentifier.Add(securityToken.CreateKeyIdentifierClause<X509RawDataKeyIdentifierClause>());
								}
								SecurityToken token;
								if (!context.UseKeyTokenResolver.TryResolveToken(securityKeyIdentifier, out token))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3092", new object[]
									{
										securityKeyIdentifier
									})));
								}
								rst.UseKey = new UseKey(securityKeyIdentifier, token);
								reader.ReadEndElement();
							}
							if (rst.UseKey == null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3155")));
							}
							return;
						}
						else if (reader.IsStartElement(trustConstants.Elements.SignWith, trustConstants.NamespaceURI))
						{
							rst.SignWith = reader.ReadElementContentAsString();
							if (!UriUtil.CanCreateValidUri(rst.SignWith, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.SignWith,
									trustConstants.NamespaceURI,
									rst.SignWith
								})));
							}
							return;
						}
						else if (reader.IsStartElement(trustConstants.Elements.EncryptWith, trustConstants.NamespaceURI))
						{
							rst.EncryptWith = reader.ReadElementContentAsString();
							if (!UriUtil.CanCreateValidUri(rst.EncryptWith, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.EncryptWith,
									trustConstants.NamespaceURI,
									rst.EncryptWith
								})));
							}
							return;
						}
						else
						{
							if (reader.IsStartElement(trustConstants.Elements.ComputedKeyAlgorithm, trustConstants.NamespaceURI))
							{
								rst.ComputedKeyAlgorithm = WSTrustSerializationHelper.ReadComputedKeyAlgorithm(reader, trustConstants);
								return;
							}
							if (reader.IsStartElement(trustConstants.Elements.AuthenticationType, trustConstants.NamespaceURI))
							{
								rst.AuthenticationType = reader.ReadElementContentAsString(trustConstants.Elements.AuthenticationType, trustConstants.NamespaceURI);
								if (!UriUtil.CanCreateValidUri(rst.AuthenticationType, UriKind.Absolute))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
									{
										trustConstants.Elements.AuthenticationType,
										trustConstants.NamespaceURI,
										rst.AuthenticationType
									})));
								}
								return;
							}
							else if (reader.IsStartElement(trustConstants.Elements.EncryptionAlgorithm, trustConstants.NamespaceURI))
							{
								rst.EncryptionAlgorithm = reader.ReadElementContentAsString(trustConstants.Elements.EncryptionAlgorithm, trustConstants.NamespaceURI);
								if (!UriUtil.CanCreateValidUri(rst.EncryptionAlgorithm, UriKind.Absolute))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
									{
										trustConstants.Elements.EncryptionAlgorithm,
										trustConstants.NamespaceURI,
										rst.EncryptionAlgorithm
									})));
								}
								return;
							}
							else if (reader.IsStartElement(trustConstants.Elements.CanonicalizationAlgorithm, trustConstants.NamespaceURI))
							{
								rst.CanonicalizationAlgorithm = reader.ReadElementContentAsString(trustConstants.Elements.CanonicalizationAlgorithm, trustConstants.NamespaceURI);
								if (!UriUtil.CanCreateValidUri(rst.CanonicalizationAlgorithm, UriKind.Absolute))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
									{
										trustConstants.Elements.CanonicalizationAlgorithm,
										trustConstants.NamespaceURI,
										rst.CanonicalizationAlgorithm
									})));
								}
								return;
							}
							else if (reader.IsStartElement(trustConstants.Elements.SignatureAlgorithm, trustConstants.NamespaceURI))
							{
								rst.SignatureAlgorithm = reader.ReadElementContentAsString(trustConstants.Elements.SignatureAlgorithm, trustConstants.NamespaceURI);
								if (!UriUtil.CanCreateValidUri(rst.SignatureAlgorithm, UriKind.Absolute))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
									{
										trustConstants.Elements.SignatureAlgorithm,
										trustConstants.NamespaceURI,
										rst.SignatureAlgorithm
									})));
								}
								return;
							}
							else
							{
								if (reader.IsStartElement(trustConstants.Elements.Forwardable, trustConstants.NamespaceURI))
								{
									rst.Forwardable = new bool?(reader.ReadElementContentAsBoolean());
									return;
								}
								if (reader.IsStartElement(trustConstants.Elements.Delegatable, trustConstants.NamespaceURI))
								{
									rst.Delegatable = new bool?(reader.ReadElementContentAsBoolean());
									return;
								}
								if (reader.IsStartElement(trustConstants.Elements.AllowPostdating, trustConstants.NamespaceURI))
								{
									rst.AllowPostdating = true;
									bool isEmptyElement = reader.IsEmptyElement;
									reader.Read();
									reader.MoveToContent();
									if (!isEmptyElement)
									{
										reader.ReadEndElement();
									}
									return;
								}
								if (reader.IsStartElement(trustConstants.Elements.Renewing, trustConstants.NamespaceURI))
								{
									bool isEmptyElement = reader.IsEmptyElement;
									string attribute3 = reader.GetAttribute(trustConstants.Attributes.Allow);
									bool allowRenewal = true;
									bool okForRenewalAfterExpiration = false;
									if (!string.IsNullOrEmpty(attribute3))
									{
										allowRenewal = XmlConvert.ToBoolean(attribute3);
									}
									attribute3 = reader.GetAttribute(trustConstants.Attributes.OK);
									if (!string.IsNullOrEmpty(attribute3))
									{
										okForRenewalAfterExpiration = XmlConvert.ToBoolean(attribute3);
									}
									rst.Renewing = new Renewing(allowRenewal, okForRenewalAfterExpiration);
									reader.Read();
									reader.MoveToContent();
									if (!isEmptyElement)
									{
										reader.ReadEndElement();
									}
									return;
								}
								if (reader.IsStartElement(trustConstants.Elements.CancelTarget, trustConstants.NamespaceURI))
								{
									if (!reader.IsEmptyElement)
									{
										rst.CancelTarget = new SecurityTokenElement(WSTrustSerializationHelper.ReadInnerXml(reader), context.SecurityTokenHandlers);
									}
									else
									{
										reader.Read();
									}
									if (rst.CancelTarget == null)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3220")));
									}
									return;
								}
								else
								{
									if (reader.IsStartElement(trustConstants.Elements.Participants, trustConstants.NamespaceURI))
									{
										EndpointReference primary = null;
										List<EndpointReference> list = new List<EndpointReference>();
										bool isEmptyElement = reader.IsEmptyElement;
										reader.Read();
										reader.MoveToContent();
										if (!isEmptyElement)
										{
											if (reader.IsStartElement(trustConstants.Elements.Primary, trustConstants.NamespaceURI))
											{
												reader.ReadStartElement(trustConstants.Elements.Primary, trustConstants.NamespaceURI);
												primary = EndpointReference.ReadFrom(XmlDictionaryReader.CreateDictionaryReader(reader));
												reader.ReadEndElement();
											}
											while (reader.IsStartElement(trustConstants.Elements.Participant, trustConstants.NamespaceURI))
											{
												reader.ReadStartElement(trustConstants.Elements.Participant, trustConstants.NamespaceURI);
												list.Add(EndpointReference.ReadFrom(XmlDictionaryReader.CreateDictionaryReader(reader)));
												reader.ReadEndElement();
											}
											if (reader.IsStartElement())
											{
												throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3223", new object[]
												{
													trustConstants.Elements.Participants,
													trustConstants.NamespaceURI,
													reader.LocalName,
													reader.NamespaceURI
												})));
											}
											rst.Participants = new Participants();
											rst.Participants.Primary = primary;
											rst.Participants.Participant.AddRange(list);
											reader.ReadEndElement();
										}
										return;
									}
									if (reader.IsStartElement("AdditionalContext", "http://docs.oasis-open.org/wsfed/authorization/200706"))
									{
										rst.AdditionalContext = new AdditionalContext();
										bool isEmptyElement = reader.IsEmptyElement;
										reader.Read();
										reader.MoveToContent();
										if (!isEmptyElement)
										{
											while (reader.IsStartElement("ContextItem", "http://docs.oasis-open.org/wsfed/authorization/200706"))
											{
												Uri name = null;
												Uri scope = null;
												string value2 = null;
												string attribute4 = reader.GetAttribute("Name");
												if (string.IsNullOrEmpty(attribute4) || !UriUtil.TryCreateValidUri(attribute4, UriKind.Absolute, out name))
												{
													throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3136", new object[]
													{
														"Name",
														reader.LocalName,
														reader.NamespaceURI,
														attribute4
													})));
												}
												attribute4 = reader.GetAttribute("Scope");
												if (!string.IsNullOrEmpty(attribute4) && !UriUtil.TryCreateValidUri(attribute4, UriKind.Absolute, out scope))
												{
													throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3136", new object[]
													{
														"Scope",
														reader.LocalName,
														reader.NamespaceURI,
														attribute4
													})));
												}
												if (reader.IsEmptyElement)
												{
													reader.Read();
												}
												else
												{
													reader.Read();
													if (reader.IsStartElement("Value", "http://docs.oasis-open.org/wsfed/authorization/200706"))
													{
														value2 = reader.ReadElementContentAsString("Value", "http://docs.oasis-open.org/wsfed/authorization/200706");
													}
													reader.ReadEndElement();
												}
												rst.AdditionalContext.Items.Add(new ContextItem(name, value2, scope));
											}
											if (reader.IsStartElement())
											{
												throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3223", new object[]
												{
													"AdditionalContext",
													"http://docs.oasis-open.org/wsfed/authorization/200706",
													reader.LocalName,
													reader.NamespaceURI
												})));
											}
											reader.ReadEndElement();
										}
										return;
									}
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3007", new object[]
									{
										reader.LocalName,
										reader.NamespaceURI
									})));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00049CD0 File Offset: 0x00047ED0
		public static void WriteRequest(RequestSecurityToken rst, XmlWriter writer, WSTrustSerializationContext context, WSTrustRequestSerializer requestSerializer, WSTrustConstantsAdapter trustConstants)
		{
			if (rst == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
			}
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (requestSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestSerializer");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			requestSerializer.Validate(rst);
			writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.RequestSecurityToken, trustConstants.NamespaceURI);
			if (rst.Context != null)
			{
				writer.WriteAttributeString(trustConstants.Attributes.Context, rst.Context);
			}
			requestSerializer.WriteKnownRequestElement(rst, writer, context);
			foreach (KeyValuePair<string, object> keyValuePair in rst.Properties)
			{
				requestSerializer.WriteXmlElement(writer, keyValuePair.Key, keyValuePair.Value, rst, context);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00049DEC File Offset: 0x00047FEC
		public static void WriteKnownRequestElement(RequestSecurityToken rst, XmlWriter writer, WSTrustSerializationContext context, WSTrustRequestSerializer requestSerializer, WSTrustConstantsAdapter trustConstants)
		{
			if (rst == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
			}
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (requestSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestSerializer");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (rst.AppliesTo != null)
			{
				requestSerializer.WriteXmlElement(writer, "AppliesTo", rst.AppliesTo, rst, context);
			}
			if (rst.Claims.Count > 0)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.Claims, rst.Claims, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.ComputedKeyAlgorithm))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.ComputedKeyAlgorithm, rst.ComputedKeyAlgorithm, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.SignWith))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.SignWith, rst.SignWith, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.EncryptWith))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.EncryptWith, rst.EncryptWith, rst, context);
			}
			if (rst.Entropy != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.Entropy, rst.Entropy, rst, context);
			}
			if (rst.KeySizeInBits != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.KeySize, rst.KeySizeInBits, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.KeyType))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.KeyType, rst.KeyType, rst, context);
			}
			if (rst.Lifetime != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.Lifetime, rst.Lifetime, rst, context);
			}
			if (rst.RenewTarget != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.RenewTarget, rst.RenewTarget, rst, context);
			}
			if (rst.OnBehalfOf != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.OnBehalfOf, rst.OnBehalfOf, rst, context);
			}
			if (rst.ActAs != null)
			{
				requestSerializer.WriteXmlElement(writer, "ActAs", rst.ActAs, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.RequestType))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.RequestType, rst.RequestType, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.TokenType))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.TokenType, rst.TokenType, rst, context);
			}
			if (rst.UseKey != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.UseKey, rst.UseKey, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.AuthenticationType))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.AuthenticationType, rst.AuthenticationType, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.EncryptionAlgorithm))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.EncryptionAlgorithm, rst.EncryptionAlgorithm, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.CanonicalizationAlgorithm))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.CanonicalizationAlgorithm, rst.CanonicalizationAlgorithm, rst, context);
			}
			if (!string.IsNullOrEmpty(rst.SignatureAlgorithm))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.SignatureAlgorithm, rst.SignatureAlgorithm, rst, context);
			}
			if (rst.BinaryExchange != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.BinaryExchange, rst.BinaryExchange, rst, context);
			}
			if (rst.Issuer != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.Issuer, rst.Issuer, rst, context);
			}
			if (rst.ProofEncryption != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.ProofEncryption, rst.ProofEncryption, rst, context);
			}
			if (rst.Encryption != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.Encryption, rst.Encryption, rst, context);
			}
			if (rst.DelegateTo != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.DelegateTo, rst.DelegateTo, rst, context);
			}
			if (rst.Forwardable != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.Forwardable, rst.Forwardable.Value, rst, context);
			}
			if (rst.Delegatable != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.Delegatable, rst.Delegatable.Value, rst, context);
			}
			if (rst.AllowPostdating)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.AllowPostdating, rst.AllowPostdating, rst, context);
			}
			if (rst.Renewing != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.Renewing, rst.Renewing, rst, context);
			}
			if (rst.CancelTarget != null)
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.CancelTarget, rst.CancelTarget, rst, context);
			}
			if (rst.Participants != null && (rst.Participants.Primary != null || rst.Participants.Participant.Count > 0))
			{
				requestSerializer.WriteXmlElement(writer, trustConstants.Elements.Participants, rst.Participants, rst, context);
			}
			if (rst.AdditionalContext != null)
			{
				requestSerializer.WriteXmlElement(writer, "AdditionalContext", rst.AdditionalContext, rst, context);
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0004A318 File Offset: 0x00048518
		public static void WriteRSTXml(XmlWriter writer, string elementName, object elementValue, WSTrustSerializationContext context, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (string.IsNullOrEmpty(elementName))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("elementName");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (StringComparer.Ordinal.Equals(elementName, "AppliesTo"))
			{
				EndpointReference appliesTo = elementValue as EndpointReference;
				WSTrustSerializationHelper.WriteAppliesTo(writer, appliesTo, trustConstants);
				return;
			}
			if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Claims))
			{
				writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Claims, trustConstants.NamespaceURI);
				RequestClaimCollection requestClaimCollection = (RequestClaimCollection)elementValue;
				if (requestClaimCollection.Dialect != null && !UriUtil.CanCreateValidUri(requestClaimCollection.Dialect, UriKind.Absolute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3136", new object[]
					{
						trustConstants.Attributes.Dialect,
						trustConstants.Elements.Claims,
						trustConstants.NamespaceURI,
						requestClaimCollection.Dialect
					})));
				}
				string requestClaimNamespace = WSTrustSerializationHelper.GetRequestClaimNamespace(requestClaimCollection.Dialect);
				string text = writer.LookupPrefix(requestClaimNamespace);
				if (string.IsNullOrEmpty(text))
				{
					text = WSTrustSerializationHelper.GetRequestClaimPrefix(requestClaimCollection.Dialect);
					writer.WriteAttributeString("xmlns", text, null, requestClaimNamespace);
				}
				writer.WriteAttributeString(trustConstants.Attributes.Dialect, (!string.IsNullOrEmpty(requestClaimCollection.Dialect)) ? requestClaimCollection.Dialect : "http://schemas.xmlsoap.org/ws/2005/05/identity");
				foreach (RequestClaim requestClaim in requestClaimCollection)
				{
					writer.WriteStartElement(text, "ClaimType", requestClaimNamespace);
					writer.WriteAttributeString("Uri", requestClaim.ClaimType);
					writer.WriteAttributeString("Optional", requestClaim.IsOptional ? "true" : "false");
					if (requestClaim.Value != null)
					{
						if (!StringComparer.Ordinal.Equals(requestClaimCollection.Dialect, "http://docs.oasis-open.org/wsfed/authorization/200706/authclaims"))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3257", new object[]
							{
								requestClaimCollection.Dialect,
								"http://docs.oasis-open.org/wsfed/authorization/200706/authclaims"
							})));
						}
						writer.WriteElementString(text, "Value", requestClaimNamespace, requestClaim.Value);
					}
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				return;
			}
			else
			{
				if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.ComputedKeyAlgorithm))
				{
					WSTrustSerializationHelper.WriteComputedKeyAlgorithm(writer, trustConstants.Elements.ComputedKeyAlgorithm, (string)elementValue, trustConstants);
					return;
				}
				if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.BinaryExchange))
				{
					WSTrustSerializationHelper.WriteBinaryExchange(writer, elementValue as BinaryExchange, trustConstants);
					return;
				}
				if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Issuer))
				{
					WSTrustSerializationHelper.WriteOnBehalfOfIssuer(writer, elementValue as EndpointReference, trustConstants);
					return;
				}
				if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.SignWith))
				{
					if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
						{
							trustConstants.Elements.SignWith,
							trustConstants.NamespaceURI,
							(string)elementValue
						})));
					}
					writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.SignWith, trustConstants.NamespaceURI, (string)elementValue);
					return;
				}
				else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.EncryptWith))
				{
					if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
						{
							trustConstants.Elements.EncryptWith,
							trustConstants.NamespaceURI,
							(string)elementValue
						})));
					}
					writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.EncryptWith, trustConstants.NamespaceURI, (string)elementValue);
					return;
				}
				else
				{
					if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Entropy))
					{
						Entropy entropy = elementValue as Entropy;
						if (entropy != null)
						{
							writer.WriteStartElement(trustConstants.Elements.Entropy, trustConstants.NamespaceURI);
							WSTrustSerializationHelper.WriteProtectedKey(writer, entropy, context, trustConstants);
							writer.WriteEndElement();
						}
						return;
					}
					if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.KeySize))
					{
						writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.KeySize, trustConstants.NamespaceURI, Convert.ToString((int)elementValue, CultureInfo.InvariantCulture));
						return;
					}
					if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.KeyType))
					{
						WSTrustSerializationHelper.WriteKeyType(writer, (string)elementValue, trustConstants);
						return;
					}
					if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Lifetime))
					{
						Lifetime lifetime = (Lifetime)elementValue;
						WSTrustSerializationHelper.WriteLifetime(writer, lifetime, trustConstants);
						return;
					}
					if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.RenewTarget))
					{
						SecurityTokenElement securityTokenElement = elementValue as SecurityTokenElement;
						if (securityTokenElement == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("elementValue", SR.GetString("ID3222", new object[]
							{
								trustConstants.Elements.RenewTarget,
								trustConstants.NamespaceURI,
								typeof(SecurityTokenElement),
								elementValue
							}));
						}
						writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.RenewTarget, trustConstants.NamespaceURI);
						if (securityTokenElement.SecurityTokenXml != null)
						{
							securityTokenElement.SecurityTokenXml.WriteTo(writer);
						}
						else
						{
							context.SecurityTokenHandlers.WriteToken(writer, securityTokenElement.GetSecurityToken());
						}
						writer.WriteEndElement();
						return;
					}
					else
					{
						if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.OnBehalfOf))
						{
							writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.OnBehalfOf, trustConstants.NamespaceURI);
							WSTrustSerializationHelper.WriteTokenElement((SecurityTokenElement)elementValue, "OnBehalfOf", context, writer);
							writer.WriteEndElement();
							return;
						}
						if (StringComparer.Ordinal.Equals(elementName, "ActAs"))
						{
							writer.WriteStartElement("tr", "ActAs", "http://docs.oasis-open.org/ws-sx/ws-trust/200802");
							WSTrustSerializationHelper.WriteTokenElement((SecurityTokenElement)elementValue, "ActAs", context, writer);
							writer.WriteEndElement();
							return;
						}
						if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.RequestType))
						{
							if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.RequestType,
									trustConstants.NamespaceURI,
									(string)elementValue
								})));
							}
							WSTrustSerializationHelper.WriteRequestType(writer, (string)elementValue, trustConstants);
							return;
						}
						else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.TokenType))
						{
							if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.TokenType,
									trustConstants.NamespaceURI,
									(string)elementValue
								})));
							}
							writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.TokenType, trustConstants.NamespaceURI, (string)elementValue);
							return;
						}
						else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.UseKey))
						{
							UseKey useKey = (UseKey)elementValue;
							if (useKey.Token == null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3012")));
							}
							if (!context.SecurityTokenHandlers.CanWriteToken(useKey.Token))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3017")));
							}
							writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.UseKey, trustConstants.NamespaceURI);
							context.SecurityTokenHandlers.WriteToken(writer, useKey.Token);
							writer.WriteEndElement();
							return;
						}
						else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.AuthenticationType))
						{
							if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.AuthenticationType,
									trustConstants.NamespaceURI,
									(string)elementValue
								})));
							}
							writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.AuthenticationType, trustConstants.NamespaceURI, (string)elementValue);
							return;
						}
						else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.EncryptionAlgorithm))
						{
							if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.EncryptionAlgorithm,
									trustConstants.NamespaceURI,
									(string)elementValue
								})));
							}
							writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.EncryptionAlgorithm, trustConstants.NamespaceURI, (string)elementValue);
							return;
						}
						else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.CanonicalizationAlgorithm))
						{
							if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.CanonicalizationAlgorithm,
									trustConstants.NamespaceURI,
									(string)elementValue
								})));
							}
							writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.CanonicalizationAlgorithm, trustConstants.NamespaceURI, (string)elementValue);
							return;
						}
						else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.SignatureAlgorithm))
						{
							if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.SignatureAlgorithm,
									trustConstants.NamespaceURI,
									(string)elementValue
								})));
							}
							writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.SignatureAlgorithm, trustConstants.NamespaceURI, (string)elementValue);
							return;
						}
						else
						{
							if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Encryption))
							{
								SecurityTokenElement securityTokenElement2 = (SecurityTokenElement)elementValue;
								writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Encryption, trustConstants.NamespaceURI);
								if (securityTokenElement2.SecurityTokenXml != null)
								{
									securityTokenElement2.SecurityTokenXml.WriteTo(writer);
								}
								else
								{
									context.SecurityTokenHandlers.WriteToken(writer, securityTokenElement2.GetSecurityToken());
								}
								writer.WriteEndElement();
								return;
							}
							if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.ProofEncryption))
							{
								SecurityTokenElement securityTokenElement3 = (SecurityTokenElement)elementValue;
								writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.ProofEncryption, trustConstants.NamespaceURI);
								if (securityTokenElement3.SecurityTokenXml != null)
								{
									securityTokenElement3.SecurityTokenXml.WriteTo(writer);
								}
								else
								{
									context.SecurityTokenHandlers.WriteToken(writer, securityTokenElement3.GetSecurityToken());
								}
								writer.WriteEndElement();
								return;
							}
							if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.DelegateTo))
							{
								SecurityTokenElement securityTokenElement4 = (SecurityTokenElement)elementValue;
								writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.DelegateTo, trustConstants.NamespaceURI);
								if (securityTokenElement4.SecurityTokenXml != null)
								{
									securityTokenElement4.SecurityTokenXml.WriteTo(writer);
								}
								else
								{
									context.SecurityTokenHandlers.WriteToken(writer, securityTokenElement4.GetSecurityToken());
								}
								writer.WriteEndElement();
								return;
							}
							if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Forwardable))
							{
								if (!(elementValue is bool))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("elementValue", SR.GetString("ID3222", new object[]
									{
										trustConstants.Elements.Forwardable,
										trustConstants.NamespaceURI,
										typeof(bool),
										elementValue
									}));
								}
								writer.WriteStartElement(trustConstants.Elements.Forwardable, trustConstants.NamespaceURI);
								writer.WriteString(XmlConvert.ToString((bool)elementValue));
								writer.WriteEndElement();
								return;
							}
							else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Delegatable))
							{
								if (!(elementValue is bool))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("elementValue", SR.GetString("ID3222", new object[]
									{
										trustConstants.Elements.Delegatable,
										trustConstants.NamespaceURI,
										typeof(bool),
										elementValue
									}));
								}
								writer.WriteStartElement(trustConstants.Elements.Delegatable, trustConstants.NamespaceURI);
								writer.WriteString(XmlConvert.ToString((bool)elementValue));
								writer.WriteEndElement();
								return;
							}
							else
							{
								if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.AllowPostdating))
								{
									writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.AllowPostdating, trustConstants.NamespaceURI);
									writer.WriteEndElement();
									return;
								}
								if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Renewing))
								{
									Renewing renewing = elementValue as Renewing;
									if (renewing == null)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("elementValue", SR.GetString("ID3222", new object[]
										{
											trustConstants.Elements.Renewing,
											trustConstants.NamespaceURI,
											typeof(Renewing),
											elementValue
										}));
									}
									writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Renewing, trustConstants.NamespaceURI);
									writer.WriteAttributeString(trustConstants.Attributes.Allow, XmlConvert.ToString(renewing.AllowRenewal));
									writer.WriteAttributeString(trustConstants.Attributes.OK, XmlConvert.ToString(renewing.OkForRenewalAfterExpiration));
									writer.WriteEndElement();
									return;
								}
								else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.CancelTarget))
								{
									SecurityTokenElement securityTokenElement5 = elementValue as SecurityTokenElement;
									if (securityTokenElement5 == null)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("elementValue", SR.GetString("ID3222", new object[]
										{
											trustConstants.Elements.CancelTarget,
											trustConstants.NamespaceURI,
											typeof(SecurityTokenElement),
											elementValue
										}));
									}
									writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.CancelTarget, trustConstants.NamespaceURI);
									if (securityTokenElement5.SecurityTokenXml != null)
									{
										securityTokenElement5.SecurityTokenXml.WriteTo(writer);
									}
									else
									{
										context.SecurityTokenHandlers.WriteToken(writer, securityTokenElement5.GetSecurityToken());
									}
									writer.WriteEndElement();
									return;
								}
								else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Participants))
								{
									Participants participants = elementValue as Participants;
									if (participants == null)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("elementValue", SR.GetString("ID3222", new object[]
										{
											trustConstants.Elements.Participant,
											trustConstants.NamespaceURI,
											typeof(Participants),
											elementValue
										}));
									}
									writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Participants, trustConstants.NamespaceURI);
									if (participants.Primary != null)
									{
										writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Primary, trustConstants.NamespaceURI);
										participants.Primary.WriteTo(writer);
										writer.WriteEndElement();
									}
									foreach (EndpointReference endpointReference in participants.Participant)
									{
										writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Participant, trustConstants.NamespaceURI);
										endpointReference.WriteTo(writer);
										writer.WriteEndElement();
									}
									writer.WriteEndElement();
									return;
								}
								else
								{
									if (!StringComparer.Ordinal.Equals(elementName, "AdditionalContext"))
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3013", new object[]
										{
											elementName,
											elementValue.GetType()
										})));
									}
									AdditionalContext additionalContext = elementValue as AdditionalContext;
									if (additionalContext == null)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("elementValue", SR.GetString("ID3222", new object[]
										{
											"AdditionalContext",
											"http://docs.oasis-open.org/wsfed/authorization/200706",
											typeof(AdditionalContext),
											elementValue
										}));
									}
									writer.WriteStartElement("auth", "AdditionalContext", "http://docs.oasis-open.org/wsfed/authorization/200706");
									foreach (ContextItem contextItem in additionalContext.Items)
									{
										writer.WriteStartElement("auth", "ContextItem", "http://docs.oasis-open.org/wsfed/authorization/200706");
										writer.WriteAttributeString("Name", contextItem.Name.AbsoluteUri);
										if (contextItem.Scope != null)
										{
											writer.WriteAttributeString("Scope", contextItem.Scope.AbsoluteUri);
										}
										if (contextItem.Value != null)
										{
											writer.WriteElementString("Value", "http://docs.oasis-open.org/wsfed/authorization/200706", contextItem.Value);
										}
										writer.WriteEndElement();
									}
									writer.WriteEndElement();
									return;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0004B488 File Offset: 0x00049688
		public static RequestSecurityTokenResponse CreateResponse(XmlReader reader, WSTrustSerializationContext context, WSTrustResponseSerializer responseSerializer, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (responseSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("responseSerializer");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (!reader.IsStartElement(trustConstants.Elements.RequestSecurityTokenResponse, trustConstants.NamespaceURI))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3032", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI,
					trustConstants.Elements.RequestSecurityTokenResponse,
					trustConstants.NamespaceURI
				})));
			}
			RequestSecurityTokenResponse requestSecurityTokenResponse = responseSerializer.CreateInstance();
			bool isEmptyElement = reader.IsEmptyElement;
			requestSecurityTokenResponse.Context = reader.GetAttribute(trustConstants.Attributes.Context);
			reader.Read();
			if (!isEmptyElement)
			{
				while (reader.IsStartElement())
				{
					responseSerializer.ReadXmlElement(reader, requestSecurityTokenResponse, context);
				}
				reader.ReadEndElement();
			}
			responseSerializer.Validate(requestSecurityTokenResponse);
			return requestSecurityTokenResponse;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0004B594 File Offset: 0x00049794
		public static void ReadRSTRXml(XmlReader reader, RequestSecurityTokenResponse rstr, WSTrustSerializationContext context, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (rstr == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (reader.IsStartElement(trustConstants.Elements.Entropy, trustConstants.NamespaceURI))
			{
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement(trustConstants.Elements.Entropy, trustConstants.NamespaceURI);
					ProtectedKey protectedKey = WSTrustSerializationHelper.ReadProtectedKey(reader, context, trustConstants);
					if (protectedKey == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3026")));
					}
					rstr.Entropy = new Entropy(protectedKey);
					reader.ReadEndElement();
				}
				else
				{
					reader.Read();
				}
				if (rstr.Entropy == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3026")));
				}
				return;
			}
			else if (reader.IsStartElement(trustConstants.Elements.KeySize, trustConstants.NamespaceURI))
			{
				if (!reader.IsEmptyElement)
				{
					rstr.KeySizeInBits = new int?(Convert.ToInt32(reader.ReadElementContentAsString(), CultureInfo.InvariantCulture));
				}
				else
				{
					reader.Read();
				}
				if (rstr.KeySizeInBits == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3154")));
				}
				return;
			}
			else
			{
				if (reader.IsStartElement(trustConstants.Elements.RequestType, trustConstants.NamespaceURI))
				{
					rstr.RequestType = WSTrustSerializationHelper.ReadRequestType(reader, trustConstants);
					return;
				}
				if (reader.IsStartElement(trustConstants.Elements.Lifetime, trustConstants.NamespaceURI))
				{
					rstr.Lifetime = WSTrustSerializationHelper.ReadLifetime(reader, trustConstants);
					return;
				}
				if (reader.IsStartElement(trustConstants.Elements.RequestedSecurityToken, trustConstants.NamespaceURI))
				{
					if (!reader.IsEmptyElement)
					{
						rstr.RequestedSecurityToken = new RequestedSecurityToken(WSTrustSerializationHelper.ReadInnerXml(reader));
					}
					else
					{
						reader.Read();
					}
					if (rstr.RequestedSecurityToken == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3158")));
					}
					return;
				}
				else
				{
					if (reader.IsStartElement("AppliesTo", "http://schemas.xmlsoap.org/ws/2004/09/policy"))
					{
						rstr.AppliesTo = WSTrustSerializationHelper.ReadAppliesTo(reader, trustConstants);
						return;
					}
					if (reader.IsStartElement(trustConstants.Elements.RequestedProofToken, trustConstants.NamespaceURI))
					{
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							if (reader.LocalName == trustConstants.Elements.ComputedKey && reader.NamespaceURI == trustConstants.NamespaceURI)
							{
								rstr.RequestedProofToken = new RequestedProofToken(WSTrustSerializationHelper.ReadComputedKeyAlgorithm(reader, trustConstants));
							}
							else
							{
								ProtectedKey protectedKey2 = WSTrustSerializationHelper.ReadProtectedKey(reader, context, trustConstants);
								if (protectedKey2 == null)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3025")));
								}
								rstr.RequestedProofToken = new RequestedProofToken(protectedKey2);
							}
							reader.ReadEndElement();
						}
						else
						{
							reader.Read();
						}
						if (rstr.RequestedProofToken == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3025")));
						}
						return;
					}
					else if (reader.IsStartElement(trustConstants.Elements.RequestedAttachedReference, trustConstants.NamespaceURI))
					{
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							rstr.RequestedAttachedReference = context.SecurityTokenHandlers.ReadKeyIdentifierClause(reader);
							reader.ReadEndElement();
						}
						else
						{
							reader.Read();
						}
						if (rstr.RequestedAttachedReference == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3159")));
						}
						return;
					}
					else if (reader.IsStartElement(trustConstants.Elements.RequestedUnattachedReference, trustConstants.NamespaceURI))
					{
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement();
							rstr.RequestedUnattachedReference = context.SecurityTokenHandlers.ReadKeyIdentifierClause(reader);
							reader.ReadEndElement();
						}
						else
						{
							reader.Read();
						}
						if (rstr.RequestedUnattachedReference == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3160")));
						}
						return;
					}
					else if (reader.IsStartElement(trustConstants.Elements.TokenType, trustConstants.NamespaceURI))
					{
						rstr.TokenType = reader.ReadElementContentAsString();
						if (!UriUtil.CanCreateValidUri(rstr.TokenType, UriKind.Absolute))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
							{
								trustConstants.Elements.TokenType,
								trustConstants.NamespaceURI,
								rstr.TokenType
							})));
						}
						return;
					}
					else
					{
						if (reader.IsStartElement(trustConstants.Elements.KeyType, trustConstants.NamespaceURI))
						{
							rstr.KeyType = WSTrustSerializationHelper.ReadKeyType(reader, trustConstants);
							return;
						}
						if (reader.IsStartElement(trustConstants.Elements.AuthenticationType, trustConstants.NamespaceURI))
						{
							rstr.AuthenticationType = reader.ReadElementContentAsString(trustConstants.Elements.AuthenticationType, trustConstants.NamespaceURI);
							if (!UriUtil.CanCreateValidUri(rstr.AuthenticationType, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.AuthenticationType,
									trustConstants.NamespaceURI,
									rstr.AuthenticationType
								})));
							}
							return;
						}
						else if (reader.IsStartElement(trustConstants.Elements.EncryptionAlgorithm, trustConstants.NamespaceURI))
						{
							rstr.EncryptionAlgorithm = reader.ReadElementContentAsString(trustConstants.Elements.EncryptionAlgorithm, trustConstants.NamespaceURI);
							if (!UriUtil.CanCreateValidUri(rstr.EncryptionAlgorithm, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.EncryptionAlgorithm,
									trustConstants.NamespaceURI,
									rstr.EncryptionAlgorithm
								})));
							}
							return;
						}
						else if (reader.IsStartElement(trustConstants.Elements.CanonicalizationAlgorithm, trustConstants.NamespaceURI))
						{
							rstr.CanonicalizationAlgorithm = reader.ReadElementContentAsString(trustConstants.Elements.CanonicalizationAlgorithm, trustConstants.NamespaceURI);
							if (!UriUtil.CanCreateValidUri(rstr.CanonicalizationAlgorithm, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.CanonicalizationAlgorithm,
									trustConstants.NamespaceURI,
									rstr.CanonicalizationAlgorithm
								})));
							}
							return;
						}
						else if (reader.IsStartElement(trustConstants.Elements.SignatureAlgorithm, trustConstants.NamespaceURI))
						{
							rstr.SignatureAlgorithm = reader.ReadElementContentAsString(trustConstants.Elements.SignatureAlgorithm, trustConstants.NamespaceURI);
							if (!UriUtil.CanCreateValidUri(rstr.SignatureAlgorithm, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.SignatureAlgorithm,
									trustConstants.NamespaceURI,
									rstr.SignatureAlgorithm
								})));
							}
							return;
						}
						else if (reader.IsStartElement(trustConstants.Elements.SignWith, trustConstants.NamespaceURI))
						{
							rstr.SignWith = reader.ReadElementContentAsString();
							if (!UriUtil.CanCreateValidUri(rstr.SignWith, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.SignWith,
									trustConstants.NamespaceURI,
									rstr.SignWith
								})));
							}
							return;
						}
						else if (reader.IsStartElement(trustConstants.Elements.EncryptWith, trustConstants.NamespaceURI))
						{
							rstr.EncryptWith = reader.ReadElementContentAsString();
							if (!UriUtil.CanCreateValidUri(rstr.EncryptWith, UriKind.Absolute))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
								{
									trustConstants.Elements.EncryptWith,
									trustConstants.NamespaceURI,
									rstr.EncryptWith
								})));
							}
							return;
						}
						else
						{
							if (reader.IsStartElement(trustConstants.Elements.BinaryExchange, trustConstants.NamespaceURI))
							{
								rstr.BinaryExchange = WSTrustSerializationHelper.ReadBinaryExchange(reader, trustConstants);
								return;
							}
							if (reader.IsStartElement(trustConstants.Elements.Status, trustConstants.NamespaceURI))
							{
								rstr.Status = WSTrustSerializationHelper.ReadStatus(reader, trustConstants);
								return;
							}
							if (reader.IsStartElement(trustConstants.Elements.RequestedTokenCancelled, trustConstants.NamespaceURI))
							{
								rstr.RequestedTokenCancelled = true;
								reader.ReadStartElement();
								return;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3007", new object[]
							{
								reader.LocalName,
								reader.NamespaceURI
							})));
						}
					}
				}
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0004BDA8 File Offset: 0x00049FA8
		public static void WriteResponse(RequestSecurityTokenResponse response, XmlWriter writer, WSTrustSerializationContext context, WSTrustResponseSerializer responseSerializer, WSTrustConstantsAdapter trustConstants)
		{
			if (response == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("response");
			}
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (responseSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("responseSerializer");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			responseSerializer.Validate(response);
			writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.RequestSecurityTokenResponse, trustConstants.NamespaceURI);
			if (!string.IsNullOrEmpty(response.Context))
			{
				writer.WriteAttributeString(trustConstants.Attributes.Context, response.Context);
			}
			responseSerializer.WriteKnownResponseElement(response, writer, context);
			foreach (KeyValuePair<string, object> keyValuePair in response.Properties)
			{
				responseSerializer.WriteXmlElement(writer, keyValuePair.Key, keyValuePair.Value, response, context);
			}
			writer.WriteEndElement();
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0004BEC8 File Offset: 0x0004A0C8
		public static void WriteKnownResponseElement(RequestSecurityTokenResponse rstr, XmlWriter writer, WSTrustSerializationContext context, WSTrustResponseSerializer responseSerializer, WSTrustConstantsAdapter trustConstants)
		{
			if (rstr == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
			}
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (responseSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("responseSerializer");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (rstr.Entropy != null)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.Entropy, rstr.Entropy, rstr, context);
			}
			if (rstr.KeySizeInBits != null)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.KeySize, rstr.KeySizeInBits, rstr, context);
			}
			if (rstr.Lifetime != null)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.Lifetime, rstr.Lifetime, rstr, context);
			}
			if (rstr.AppliesTo != null)
			{
				responseSerializer.WriteXmlElement(writer, "AppliesTo", rstr.AppliesTo, rstr, context);
			}
			if (rstr.RequestedSecurityToken != null)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.RequestedSecurityToken, rstr.RequestedSecurityToken, rstr, context);
			}
			if (rstr.RequestedProofToken != null)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.RequestedProofToken, rstr.RequestedProofToken, rstr, context);
			}
			if (rstr.RequestedAttachedReference != null)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.RequestedAttachedReference, rstr.RequestedAttachedReference, rstr, context);
			}
			if (rstr.RequestedUnattachedReference != null)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.RequestedUnattachedReference, rstr.RequestedUnattachedReference, rstr, context);
			}
			if (!string.IsNullOrEmpty(rstr.SignWith))
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.SignWith, rstr.SignWith, rstr, context);
			}
			if (!string.IsNullOrEmpty(rstr.EncryptWith))
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.EncryptWith, rstr.EncryptWith, rstr, context);
			}
			if (!string.IsNullOrEmpty(rstr.TokenType))
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.TokenType, rstr.TokenType, rstr, context);
			}
			if (!string.IsNullOrEmpty(rstr.RequestType))
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.RequestType, rstr.RequestType, rstr, context);
			}
			if (!string.IsNullOrEmpty(rstr.KeyType))
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.KeyType, rstr.KeyType, rstr, context);
			}
			if (!string.IsNullOrEmpty(rstr.AuthenticationType))
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.AuthenticationType, rstr.AuthenticationType, rstr, context);
			}
			if (!string.IsNullOrEmpty(rstr.EncryptionAlgorithm))
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.EncryptionAlgorithm, rstr.EncryptionAlgorithm, rstr, context);
			}
			if (!string.IsNullOrEmpty(rstr.CanonicalizationAlgorithm))
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.CanonicalizationAlgorithm, rstr.CanonicalizationAlgorithm, rstr, context);
			}
			if (!string.IsNullOrEmpty(rstr.SignatureAlgorithm))
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.SignatureAlgorithm, rstr.SignatureAlgorithm, rstr, context);
			}
			if (rstr.BinaryExchange != null)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.BinaryExchange, rstr.BinaryExchange, rstr, context);
			}
			if (rstr.Status != null)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.Status, rstr.Status, rstr, context);
			}
			if (rstr.RequestedTokenCancelled)
			{
				responseSerializer.WriteXmlElement(writer, trustConstants.Elements.RequestedTokenCancelled, rstr.RequestedTokenCancelled, rstr, context);
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004C22C File Offset: 0x0004A42C
		public static void WriteRSTRXml(XmlWriter writer, string elementName, object elementValue, WSTrustSerializationContext context, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (string.IsNullOrEmpty(elementName))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("elementName");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Entropy))
			{
				Entropy entropy = elementValue as Entropy;
				if (entropy != null)
				{
					writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Entropy, trustConstants.NamespaceURI);
					WSTrustSerializationHelper.WriteProtectedKey(writer, entropy, context, trustConstants);
					writer.WriteEndElement();
				}
				return;
			}
			if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.KeySize))
			{
				writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.KeySize, trustConstants.NamespaceURI, Convert.ToString((int)elementValue, CultureInfo.InvariantCulture));
				return;
			}
			if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Lifetime))
			{
				Lifetime lifetime = (Lifetime)elementValue;
				WSTrustSerializationHelper.WriteLifetime(writer, lifetime, trustConstants);
				return;
			}
			if (StringComparer.Ordinal.Equals(elementName, "AppliesTo"))
			{
				EndpointReference appliesTo = elementValue as EndpointReference;
				WSTrustSerializationHelper.WriteAppliesTo(writer, appliesTo, trustConstants);
				return;
			}
			if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.RequestedSecurityToken))
			{
				RequestedSecurityToken requestedSecurityToken = (RequestedSecurityToken)elementValue;
				writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.RequestedSecurityToken, trustConstants.NamespaceURI);
				if (requestedSecurityToken.SecurityTokenXml != null)
				{
					requestedSecurityToken.SecurityTokenXml.WriteTo(writer);
				}
				else
				{
					context.SecurityTokenHandlers.WriteToken(writer, requestedSecurityToken.SecurityToken);
				}
				writer.WriteEndElement();
				return;
			}
			if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.RequestedProofToken))
			{
				RequestedProofToken requestedProofToken = (RequestedProofToken)elementValue;
				if (string.IsNullOrEmpty(requestedProofToken.ComputedKeyAlgorithm) && requestedProofToken.ProtectedKey == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3021")));
				}
				writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.RequestedProofToken, trustConstants.NamespaceURI);
				if (!string.IsNullOrEmpty(requestedProofToken.ComputedKeyAlgorithm))
				{
					WSTrustSerializationHelper.WriteComputedKeyAlgorithm(writer, trustConstants.Elements.ComputedKey, requestedProofToken.ComputedKeyAlgorithm, trustConstants);
				}
				else
				{
					WSTrustSerializationHelper.WriteProtectedKey(writer, requestedProofToken.ProtectedKey, context, trustConstants);
				}
				writer.WriteEndElement();
				return;
			}
			else
			{
				if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.RequestedAttachedReference))
				{
					writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.RequestedAttachedReference, trustConstants.NamespaceURI);
					context.SecurityTokenHandlers.WriteKeyIdentifierClause(writer, (SecurityKeyIdentifierClause)elementValue);
					writer.WriteEndElement();
					return;
				}
				if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.RequestedUnattachedReference))
				{
					writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.RequestedUnattachedReference, trustConstants.NamespaceURI);
					context.SecurityTokenHandlers.WriteKeyIdentifierClause(writer, (SecurityKeyIdentifierClause)elementValue);
					writer.WriteEndElement();
					return;
				}
				if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.TokenType))
				{
					if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
						{
							trustConstants.Elements.TokenType,
							trustConstants.NamespaceURI,
							(string)elementValue
						})));
					}
					writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.TokenType, trustConstants.NamespaceURI, (string)elementValue);
					return;
				}
				else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.RequestType))
				{
					if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
						{
							trustConstants.Elements.RequestType,
							trustConstants.NamespaceURI,
							(string)elementValue
						})));
					}
					WSTrustSerializationHelper.WriteRequestType(writer, (string)elementValue, trustConstants);
					return;
				}
				else
				{
					if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.KeyType))
					{
						WSTrustSerializationHelper.WriteKeyType(writer, (string)elementValue, trustConstants);
						return;
					}
					if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.AuthenticationType))
					{
						if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
							{
								trustConstants.Elements.AuthenticationType,
								trustConstants.NamespaceURI,
								(string)elementValue
							})));
						}
						writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.AuthenticationType, trustConstants.NamespaceURI, (string)elementValue);
						return;
					}
					else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.EncryptionAlgorithm))
					{
						if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
							{
								trustConstants.Elements.EncryptionAlgorithm,
								trustConstants.NamespaceURI,
								(string)elementValue
							})));
						}
						writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.EncryptionAlgorithm, trustConstants.NamespaceURI, (string)elementValue);
						return;
					}
					else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.CanonicalizationAlgorithm))
					{
						if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
							{
								trustConstants.Elements.CanonicalizationAlgorithm,
								trustConstants.NamespaceURI,
								(string)elementValue
							})));
						}
						writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.CanonicalizationAlgorithm, trustConstants.NamespaceURI, (string)elementValue);
						return;
					}
					else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.SignatureAlgorithm))
					{
						if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
							{
								trustConstants.Elements.SignatureAlgorithm,
								trustConstants.NamespaceURI,
								(string)elementValue
							})));
						}
						writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.SignatureAlgorithm, trustConstants.NamespaceURI, (string)elementValue);
						return;
					}
					else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.SignWith))
					{
						if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
							{
								trustConstants.Elements.SignWith,
								trustConstants.NamespaceURI,
								(string)elementValue
							})));
						}
						writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.SignWith, trustConstants.NamespaceURI, (string)elementValue);
						return;
					}
					else if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.EncryptWith))
					{
						if (!UriUtil.CanCreateValidUri((string)elementValue, UriKind.Absolute))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
							{
								trustConstants.Elements.EncryptWith,
								trustConstants.NamespaceURI,
								(string)elementValue
							})));
						}
						writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.EncryptWith, trustConstants.NamespaceURI, (string)elementValue);
						return;
					}
					else
					{
						if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.BinaryExchange))
						{
							WSTrustSerializationHelper.WriteBinaryExchange(writer, elementValue as BinaryExchange, trustConstants);
							return;
						}
						if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.Status))
						{
							WSTrustSerializationHelper.WriteStatus(writer, elementValue as Status, trustConstants);
							return;
						}
						if (StringComparer.Ordinal.Equals(elementName, trustConstants.Elements.RequestedTokenCancelled))
						{
							writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.RequestedTokenCancelled, trustConstants.NamespaceURI);
							writer.WriteEndElement();
							return;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3013", new object[]
						{
							elementName,
							elementValue.GetType()
						})));
					}
				}
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0004CA88 File Offset: 0x0004AC88
		public static string ReadComputedKeyAlgorithm(XmlReader reader, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			string text = reader.ReadElementContentAsString();
			if (string.IsNullOrEmpty(text))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3006")));
			}
			if (!UriUtil.CanCreateValidUri(text, UriKind.Absolute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
				{
					trustConstants.Elements.ComputedKeyAlgorithm,
					trustConstants.NamespaceURI,
					text
				})));
			}
			if (StringComparer.Ordinal.Equals(text, trustConstants.ComputedKeyAlgorithm.Psha1))
			{
				text = "http://schemas.microsoft.com/idfx/computedkeyalgorithm/psha1";
			}
			return text;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0004CB48 File Offset: 0x0004AD48
		public static void WriteComputedKeyAlgorithm(XmlWriter writer, string elementName, string computedKeyAlgorithm, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (string.IsNullOrEmpty(computedKeyAlgorithm))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("computedKeyAlgorithm");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (!UriUtil.CanCreateValidUri(computedKeyAlgorithm, UriKind.Absolute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
				{
					elementName,
					trustConstants.NamespaceURI,
					computedKeyAlgorithm
				})));
			}
			string text;
			if (StringComparer.Ordinal.Equals(computedKeyAlgorithm, "http://schemas.microsoft.com/idfx/computedkeyalgorithm/psha1"))
			{
				text = trustConstants.ComputedKeyAlgorithm.Psha1;
			}
			else
			{
				text = computedKeyAlgorithm;
			}
			if (!UriUtil.CanCreateValidUri(text, UriKind.Absolute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
				{
					elementName,
					trustConstants.NamespaceURI,
					text
				})));
			}
			writer.WriteElementString(trustConstants.Prefix, elementName, trustConstants.NamespaceURI, text);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004CC3C File Offset: 0x0004AE3C
		public static Status ReadStatus(XmlReader reader, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (!reader.IsStartElement(trustConstants.Elements.Status, trustConstants.NamespaceURI))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3032", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI,
					trustConstants.Elements.Status,
					trustConstants.NamespaceURI
				})));
			}
			string reason = null;
			reader.ReadStartElement();
			if (!reader.IsStartElement(trustConstants.Elements.Code, trustConstants.NamespaceURI))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3032", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI,
					trustConstants.Elements.Code,
					trustConstants.NamespaceURI
				})));
			}
			string code = reader.ReadElementContentAsString(trustConstants.Elements.Code, trustConstants.NamespaceURI);
			if (reader.IsStartElement(trustConstants.Elements.Reason, trustConstants.NamespaceURI))
			{
				reason = reader.ReadElementContentAsString(trustConstants.Elements.Reason, trustConstants.NamespaceURI);
			}
			reader.ReadEndElement();
			return new Status(code, reason);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0004CD94 File Offset: 0x0004AF94
		public static BinaryExchange ReadBinaryExchange(XmlReader reader, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (!reader.IsStartElement(trustConstants.Elements.BinaryExchange, trustConstants.NamespaceURI))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3032", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI,
					trustConstants.Elements.BinaryExchange,
					trustConstants.NamespaceURI
				})));
			}
			string attribute = reader.GetAttribute(trustConstants.Attributes.ValueType);
			if (string.IsNullOrEmpty(attribute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID0001", new object[]
				{
					trustConstants.Attributes.ValueType,
					reader.Name
				})));
			}
			Uri valueType;
			if (!UriUtil.TryCreateValidUri(attribute, UriKind.Absolute, out valueType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3136", new object[]
				{
					trustConstants.Attributes.ValueType,
					reader.LocalName,
					reader.NamespaceURI,
					attribute
				})));
			}
			attribute = reader.GetAttribute(trustConstants.Attributes.EncodingType);
			if (string.IsNullOrEmpty(attribute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID0001", new object[]
				{
					trustConstants.Attributes.EncodingType,
					reader.Name
				})));
			}
			Uri uri;
			if (!UriUtil.TryCreateValidUri(attribute, UriKind.Absolute, out uri))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3136", new object[]
				{
					trustConstants.Attributes.EncodingType,
					reader.LocalName,
					reader.NamespaceURI,
					attribute
				})));
			}
			string absoluteUri = uri.AbsoluteUri;
			byte[] binaryData;
			if (!(absoluteUri == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"))
			{
				if (!(absoluteUri == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3215", new object[]
					{
						uri,
						reader.LocalName,
						reader.NamespaceURI,
						string.Format(CultureInfo.InvariantCulture, "({0}, {1})", new object[]
						{
							"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary",
							"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary"
						})
					})));
				}
				binaryData = SoapHexBinary.Parse(reader.ReadElementContentAsString()).Value;
			}
			else
			{
				binaryData = Convert.FromBase64String(reader.ReadElementContentAsString());
			}
			return new BinaryExchange(binaryData, valueType, uri);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0004D018 File Offset: 0x0004B218
		public static void WriteBinaryExchange(XmlWriter writer, BinaryExchange binaryExchange, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (binaryExchange == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binaryExchange");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			string absoluteUri = binaryExchange.EncodingType.AbsoluteUri;
			string text;
			if (!(absoluteUri == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"))
			{
				if (!(absoluteUri == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3217", new object[]
					{
						binaryExchange.EncodingType.AbsoluteUri,
						string.Format(CultureInfo.InvariantCulture, "({0}, {1})", new object[]
						{
							"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary",
							"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary"
						})
					})));
				}
				SoapHexBinary soapHexBinary = new SoapHexBinary(binaryExchange.BinaryData);
				text = soapHexBinary.ToString();
			}
			else
			{
				text = Convert.ToBase64String(binaryExchange.BinaryData);
			}
			writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.BinaryExchange, trustConstants.NamespaceURI);
			writer.WriteAttributeString(trustConstants.Attributes.ValueType, binaryExchange.ValueType.AbsoluteUri);
			writer.WriteAttributeString(trustConstants.Attributes.EncodingType, binaryExchange.EncodingType.AbsoluteUri);
			writer.WriteString(text);
			writer.WriteEndElement();
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0004D164 File Offset: 0x0004B364
		public static void WriteStatus(XmlWriter writer, Status status, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (status == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("status");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (status.Code == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("status code");
			}
			writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Status, trustConstants.NamespaceURI);
			writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Code, trustConstants.NamespaceURI);
			writer.WriteString(status.Code);
			writer.WriteEndElement();
			if (status.Reason != null)
			{
				writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Reason, trustConstants.NamespaceURI);
				writer.WriteString(status.Reason);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0004D24C File Offset: 0x0004B44C
		public static ProtectedKey ReadProtectedKey(XmlReader reader, WSTrustSerializationContext context, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			ProtectedKey result = null;
			if (!reader.IsEmptyElement)
			{
				if (reader.IsStartElement(trustConstants.Elements.BinarySecret, trustConstants.NamespaceURI))
				{
					BinarySecretSecurityToken binarySecretSecurityToken = WSTrustSerializationHelper.ReadBinarySecretSecurityToken(reader, trustConstants);
					byte[] keyBytes = binarySecretSecurityToken.GetKeyBytes();
					result = new ProtectedKey(keyBytes);
				}
				else if (context.SecurityTokenHandlers.CanReadKeyIdentifierClause(reader))
				{
					EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = context.SecurityTokenHandlers.ReadKeyIdentifierClause(reader) as EncryptedKeyIdentifierClause;
					if (encryptedKeyIdentifierClause != null)
					{
						SecurityKey securityKey = null;
						foreach (SecurityKeyIdentifierClause keyIdentifierClause in encryptedKeyIdentifierClause.EncryptingKeyIdentifier)
						{
							if (context.TokenResolver.TryResolveSecurityKey(keyIdentifierClause, out securityKey))
							{
								break;
							}
						}
						if (securityKey == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3027", new object[]
							{
								"the SecurityHeaderTokenResolver or OutOfBandTokenResolver"
							})));
						}
						byte[] secret = securityKey.DecryptKey(encryptedKeyIdentifierClause.EncryptionMethod, encryptedKeyIdentifierClause.GetEncryptedKey());
						EncryptingCredentials wrappingCredentials = new EncryptingCredentials(securityKey, encryptedKeyIdentifierClause.EncryptingKeyIdentifier, encryptedKeyIdentifierClause.EncryptionMethod);
						result = new ProtectedKey(secret, wrappingCredentials);
					}
				}
			}
			return result;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
		public static void WriteProtectedKey(XmlWriter writer, ProtectedKey protectedKey, WSTrustSerializationContext context, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (protectedKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("protectedKey");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (protectedKey.WrappingCredentials != null)
			{
				byte[] encryptedKey = protectedKey.WrappingCredentials.SecurityKey.EncryptKey(protectedKey.WrappingCredentials.Algorithm, protectedKey.GetKeyBytes());
				EncryptedKeyIdentifierClause keyIdentifierClause = new EncryptedKeyIdentifierClause(encryptedKey, protectedKey.WrappingCredentials.Algorithm, protectedKey.WrappingCredentials.SecurityKeyIdentifier);
				context.SecurityTokenHandlers.WriteKeyIdentifierClause(writer, keyIdentifierClause);
				return;
			}
			BinarySecretSecurityToken token = new BinarySecretSecurityToken(protectedKey.GetKeyBytes());
			WSTrustSerializationHelper.WriteBinarySecretSecurityToken(writer, token, trustConstants);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0004D450 File Offset: 0x0004B650
		public static string ReadRequestType(XmlReader reader, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			string text = reader.ReadElementContentAsString();
			if (!UriUtil.CanCreateValidUri(text, UriKind.Absolute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
				{
					trustConstants.Elements.RequestType,
					trustConstants.NamespaceURI,
					text
				})));
			}
			if (trustConstants.RequestTypes.Issue.Equals(text))
			{
				return "http://schemas.microsoft.com/idfx/requesttype/issue";
			}
			if (trustConstants.RequestTypes.Cancel.Equals(text))
			{
				return "http://schemas.microsoft.com/idfx/requesttype/cancel";
			}
			if (trustConstants.RequestTypes.Renew.Equals(text))
			{
				return "http://schemas.microsoft.com/idfx/requesttype/renew";
			}
			if (trustConstants.RequestTypes.Validate.Equals(text))
			{
				return "http://schemas.microsoft.com/idfx/requesttype/validate";
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3011", new object[]
			{
				text
			})));
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0004D558 File Offset: 0x0004B758
		public static void WriteRequestType(XmlWriter writer, string requestType, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (requestType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestType");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			string value;
			if (StringComparer.Ordinal.Equals(requestType, "http://schemas.microsoft.com/idfx/requesttype/issue") || StringComparer.Ordinal.Equals(requestType, trustConstants.RequestTypes.Issue))
			{
				value = trustConstants.RequestTypes.Issue;
			}
			else if (StringComparer.Ordinal.Equals(requestType, "http://schemas.microsoft.com/idfx/requesttype/renew") || StringComparer.Ordinal.Equals(requestType, trustConstants.RequestTypes.Renew))
			{
				value = trustConstants.RequestTypes.Renew;
			}
			else if (StringComparer.Ordinal.Equals(requestType, "http://schemas.microsoft.com/idfx/requesttype/cancel") || StringComparer.Ordinal.Equals(requestType, trustConstants.RequestTypes.Cancel))
			{
				value = trustConstants.RequestTypes.Cancel;
			}
			else
			{
				if (!StringComparer.Ordinal.Equals(requestType, "http://schemas.microsoft.com/idfx/requesttype/validate") && !StringComparer.Ordinal.Equals(requestType, trustConstants.RequestTypes.Validate))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3011", new object[]
					{
						requestType
					})));
				}
				value = trustConstants.RequestTypes.Validate;
			}
			writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.RequestType, trustConstants.NamespaceURI, value);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0004D6C8 File Offset: 0x0004B8C8
		public static Lifetime ReadLifetime(XmlReader reader, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			DateTime? created = null;
			DateTime? expires = null;
			Lifetime lifetime = null;
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				if (reader.IsStartElement("Created", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"))
				{
					reader.ReadStartElement("Created", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
					created = new DateTime?(DateTime.ParseExact(reader.ReadString(), DateTimeFormats.Accepted, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime());
					reader.ReadEndElement();
				}
				if (reader.IsStartElement("Expires", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"))
				{
					reader.ReadStartElement("Expires", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
					expires = new DateTime?(DateTime.ParseExact(reader.ReadString(), DateTimeFormats.Accepted, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime());
					reader.ReadEndElement();
				}
				reader.ReadEndElement();
				lifetime = new Lifetime(created, expires);
			}
			if (lifetime == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3161")));
			}
			return lifetime;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0004D7E8 File Offset: 0x0004B9E8
		public static void WriteLifetime(XmlWriter writer, Lifetime lifetime, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (lifetime == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("lifetime");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Lifetime, trustConstants.NamespaceURI);
			if (lifetime.Created != null)
			{
				writer.WriteElementString("wsu", "Created", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", lifetime.Created.Value.ToString(DateTimeFormats.Generated, CultureInfo.InvariantCulture));
			}
			if (lifetime.Expires != null)
			{
				writer.WriteElementString("wsu", "Expires", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", lifetime.Expires.Value.ToString(DateTimeFormats.Generated, CultureInfo.InvariantCulture));
			}
			writer.WriteEndElement();
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0004D8DC File Offset: 0x0004BADC
		public static EndpointReference ReadOnBehalfOfIssuer(XmlReader reader, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (!reader.IsStartElement(trustConstants.Elements.Issuer, trustConstants.NamespaceURI))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3032", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI,
					trustConstants.Elements.Issuer,
					trustConstants.NamespaceURI
				})));
			}
			EndpointReference endpointReference = null;
			if (!reader.IsEmptyElement)
			{
				reader.ReadStartElement();
				endpointReference = EndpointReference.ReadFrom(XmlDictionaryReader.CreateDictionaryReader(reader));
				reader.ReadEndElement();
			}
			if (endpointReference == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3216")));
			}
			return endpointReference;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0004D9B4 File Offset: 0x0004BBB4
		public static void WriteOnBehalfOfIssuer(XmlWriter writer, EndpointReference issuer, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (issuer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuer");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			writer.WriteStartElement(trustConstants.Prefix, trustConstants.Elements.Issuer, trustConstants.NamespaceURI);
			issuer.WriteTo(writer);
			writer.WriteEndElement();
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0004DA24 File Offset: 0x0004BC24
		public static EndpointReference ReadAppliesTo(XmlReader reader, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			EndpointReference endpointReference = null;
			if (!reader.IsEmptyElement)
			{
				reader.ReadStartElement();
				endpointReference = EndpointReference.ReadFrom(XmlDictionaryReader.CreateDictionaryReader(reader));
				reader.ReadEndElement();
			}
			if (endpointReference == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3162")));
			}
			return endpointReference;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004DA98 File Offset: 0x0004BC98
		public static void WriteAppliesTo(XmlWriter writer, EndpointReference appliesTo, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (appliesTo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("appliesTo");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			writer.WriteStartElement("wsp", "AppliesTo", "http://schemas.xmlsoap.org/ws/2004/09/policy");
			appliesTo.WriteTo(writer);
			writer.WriteEndElement();
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0004DB00 File Offset: 0x0004BD00
		public static string ReadKeyType(XmlReader reader, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			string text = reader.ReadElementContentAsString();
			if (!UriUtil.CanCreateValidUri(text, UriKind.Absolute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
				{
					trustConstants.Elements.KeyType,
					trustConstants.NamespaceURI,
					text
				})));
			}
			if (trustConstants.KeyTypes.Symmetric.Equals(text))
			{
				return "http://schemas.microsoft.com/idfx/keytype/symmetric";
			}
			if (trustConstants.KeyTypes.Asymmetric.Equals(text))
			{
				return "http://schemas.microsoft.com/idfx/keytype/asymmetric";
			}
			if (trustConstants.KeyTypes.Bearer.Equals(text))
			{
				return "http://schemas.microsoft.com/idfx/keytype/bearer";
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3020", new object[]
			{
				text
			})));
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0004DBEC File Offset: 0x0004BDEC
		public static void WriteKeyType(XmlWriter writer, string keyType, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (string.IsNullOrEmpty(keyType))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("keyType");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			if (!UriUtil.CanCreateValidUri(keyType, UriKind.Absolute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
				{
					trustConstants.Elements.KeyType,
					trustConstants.NamespaceURI,
					keyType
				})));
			}
			string value;
			if (StringComparer.Ordinal.Equals(keyType, "http://schemas.microsoft.com/idfx/keytype/asymmetric") || StringComparer.Ordinal.Equals(keyType, trustConstants.KeyTypes.Asymmetric))
			{
				value = trustConstants.KeyTypes.Asymmetric;
			}
			else if (StringComparer.Ordinal.Equals(keyType, "http://schemas.microsoft.com/idfx/keytype/symmetric") || StringComparer.Ordinal.Equals(keyType, trustConstants.KeyTypes.Symmetric))
			{
				value = trustConstants.KeyTypes.Symmetric;
			}
			else
			{
				if (!StringComparer.Ordinal.Equals(keyType, "http://schemas.microsoft.com/idfx/keytype/bearer") && !StringComparer.Ordinal.Equals(keyType, trustConstants.KeyTypes.Bearer))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3010", new object[]
					{
						keyType
					})));
				}
				value = trustConstants.KeyTypes.Bearer;
			}
			writer.WriteElementString(trustConstants.Prefix, trustConstants.Elements.KeyType, trustConstants.NamespaceURI, value);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004DD65 File Offset: 0x0004BF65
		public static XmlElement ReadInnerXml(XmlReader reader)
		{
			return WSTrustSerializationHelper.ReadInnerXml(reader, false);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0004DD70 File Offset: 0x0004BF70
		public static XmlElement ReadInnerXml(XmlReader reader, bool onStartElement)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			string localName = reader.LocalName;
			string namespaceURI = reader.NamespaceURI;
			if (reader.IsEmptyElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3061", new object[]
				{
					localName,
					namespaceURI
				})));
			}
			if (!onStartElement)
			{
				reader.ReadStartElement();
			}
			reader.MoveToContent();
			XmlElement documentElement;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8, false))
				{
					xmlWriter.WriteNode(reader, true);
					xmlWriter.Flush();
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				if (memoryStream.Length == 0L)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3061", new object[]
					{
						localName,
						namespaceURI
					})));
				}
				XmlDictionaryReader reader2 = XmlDictionaryReader.CreateTextReader(memoryStream, Encoding.UTF8, XmlDictionaryReaderQuotas.Max, null);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.Load(reader2);
				documentElement = xmlDocument.DocumentElement;
			}
			if (!onStartElement)
			{
				reader.ReadEndElement();
			}
			return documentElement;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0004DEB0 File Offset: 0x0004C0B0
		public static BinarySecretSecurityToken ReadBinarySecretSecurityToken(XmlReader reader, WSTrustConstantsAdapter trustConstants)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			string text = reader.ReadElementContentAsString(trustConstants.Elements.BinarySecret, trustConstants.NamespaceURI);
			if (string.IsNullOrEmpty(text))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3164")));
			}
			return new BinarySecretSecurityToken(Convert.FromBase64String(text));
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0004DF28 File Offset: 0x0004C128
		public static void WriteBinarySecretSecurityToken(XmlWriter writer, BinarySecretSecurityToken token, WSTrustConstantsAdapter trustConstants)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (trustConstants == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustConstants");
			}
			byte[] keyBytes = token.GetKeyBytes();
			writer.WriteStartElement(trustConstants.Elements.BinarySecret, trustConstants.NamespaceURI);
			writer.WriteBase64(keyBytes, 0, keyBytes.Length);
			writer.WriteEndElement();
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004DF9D File Offset: 0x0004C19D
		private static string GetRequestClaimNamespace(string dialect)
		{
			if (StringComparer.Ordinal.Equals(dialect, "http://docs.oasis-open.org/wsfed/authorization/200706/authclaims"))
			{
				return "http://docs.oasis-open.org/wsfed/authorization/200706";
			}
			return "http://schemas.xmlsoap.org/ws/2005/05/identity";
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0004DFBC File Offset: 0x0004C1BC
		private static string GetRequestClaimPrefix(string dialect)
		{
			if (StringComparer.Ordinal.Equals(dialect, "http://docs.oasis-open.org/wsfed/authorization/200706/authclaims"))
			{
				return "auth";
			}
			return "i";
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004DFDC File Offset: 0x0004C1DC
		private static void WriteTokenElement(SecurityTokenElement tokenElement, string usage, WSTrustSerializationContext context, XmlWriter writer)
		{
			if (tokenElement.SecurityTokenXml != null)
			{
				tokenElement.SecurityTokenXml.WriteTo(writer);
				return;
			}
			SecurityTokenHandlerCollection securityTokenHandlerCollection;
			if (context.SecurityTokenHandlerCollectionManager.ContainsKey(usage))
			{
				securityTokenHandlerCollection = context.SecurityTokenHandlerCollectionManager[usage];
			}
			else
			{
				securityTokenHandlerCollection = context.SecurityTokenHandlers;
			}
			SecurityToken securityToken = tokenElement.GetSecurityToken();
			bool flag = false;
			if (securityTokenHandlerCollection != null && securityTokenHandlerCollection.CanWriteToken(securityToken))
			{
				securityTokenHandlerCollection.WriteToken(writer, securityToken);
				flag = true;
			}
			if (!flag)
			{
				context.SecurityTokenHandlers.WriteToken(writer, securityToken);
			}
		}
	}
}
