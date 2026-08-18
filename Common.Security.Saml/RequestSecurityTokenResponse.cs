using System;
using System.Globalization;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Xml;
using System.Xml.Linq;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x0200000C RID: 12
	[MessageContract(WrapperName = "RequestSecurityTokenResponseCollection", WrapperNamespace = "http://docs.oasis-open.org/ws-sx/ws-trust/200512")]
	public class RequestSecurityTokenResponse
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002975 File Offset: 0x00000B75
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000297D File Offset: 0x00000B7D
		public SecurityToken RequestedSecurityToken { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002986 File Offset: 0x00000B86
		// (set) Token: 0x0600004B RID: 75 RVA: 0x0000298E File Offset: 0x00000B8E
		public SecurityToken RequestedProofToken { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002997 File Offset: 0x00000B97
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000299F File Offset: 0x00000B9F
		public SecurityKeyIdentifierClause RequestedAttachedReference { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000029A8 File Offset: 0x00000BA8
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000029B0 File Offset: 0x00000BB0
		public SecurityKeyIdentifierClause RequestedUnattachedReference { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000029B9 File Offset: 0x00000BB9
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000029C1 File Offset: 0x00000BC1
		public SecurityToken IssuerEntropy { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000029CA File Offset: 0x00000BCA
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000029D2 File Offset: 0x00000BD2
		public bool ComputeKey { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000029DB File Offset: 0x00000BDB
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000029E3 File Offset: 0x00000BE3
		public string Context { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000029EC File Offset: 0x00000BEC
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000029F4 File Offset: 0x00000BF4
		public string TokenType { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000029FD File Offset: 0x00000BFD
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002A05 File Offset: 0x00000C05
		public int KeySize { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002A0E File Offset: 0x00000C0E
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002A16 File Offset: 0x00000C16
		public EndpointAddress AppliesTo { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002A1F File Offset: 0x00000C1F
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002A27 File Offset: 0x00000C27
		public Lifetime TokenLifetime { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002A30 File Offset: 0x00000C30
		public RequestSecurityTokenResponse() : this(string.Empty, string.Empty, 0, null, null, null, false)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002A4C File Offset: 0x00000C4C
		public RequestSecurityTokenResponse(string context, string tokenType, int keySize, EndpointAddress appliesTo, SecurityToken requestedSecurityToken, SecurityToken requestedProofToken, bool computeKey) : this(context, tokenType, keySize, appliesTo, requestedSecurityToken, requestedProofToken, null, null, computeKey, null, null)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A70 File Offset: 0x00000C70
		public RequestSecurityTokenResponse(string context, string tokenType, int keySize, EndpointAddress appliesTo, SecurityToken requestedSecurityToken, SecurityToken requestedProofToken, SecurityKeyIdentifierClause requestedAttachedReference, SecurityKeyIdentifierClause requestedUnattachedReference, bool computeKey, SecurityToken issuerEntropy, Lifetime tokenLifetime)
		{
			this.RequestedSecurityToken = requestedSecurityToken;
			this.RequestedProofToken = requestedProofToken;
			this.RequestedAttachedReference = requestedAttachedReference;
			this.RequestedUnattachedReference = requestedUnattachedReference;
			this.ComputeKey = computeKey;
			this.Context = context;
			this.TokenType = tokenType;
			this.KeySize = keySize;
			this.AppliesTo = appliesTo;
			this.IssuerEntropy = issuerEntropy;
			this.TokenLifetime = tokenLifetime;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public static byte[] ComputeCombinedKey(byte[] requestorEntropy, byte[] issuerEntropy, int keySize)
		{
			bool flag = keySize < 64 || keySize > 4096;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("keySize");
			}
			HMACSHA1 hmacsha = new HMACSHA1(requestorEntropy, true);
			byte[] array = new byte[keySize / 8];
			byte[] array2 = issuerEntropy;
			byte[] array3 = new byte[hmacsha.HashSize / 8 + array2.Length];
			int i = 0;
			while (i < array.Length)
			{
				hmacsha.Initialize();
				array2 = hmacsha.ComputeHash(array2);
				array2.CopyTo(array3, 0);
				issuerEntropy.CopyTo(array3, array2.Length);
				hmacsha.Initialize();
				byte[] array4 = hmacsha.ComputeHash(array3);
				foreach (byte b in array4)
				{
					bool flag2 = i < array.Length;
					if (!flag2)
					{
						break;
					}
					array[i++] = b;
				}
			}
			return array;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002BC8 File Offset: 0x00000DC8
		private RequestSecurityTokenResponse BuildRequestSecurityTokenResponse(XmlReader xmlReader)
		{
			RequestSecurityTokenResponseBuilder requestSecurityTokenResponseBuilder = new RequestSecurityTokenResponseBuilder();
			requestSecurityTokenResponseBuilder.Clear();
			requestSecurityTokenResponseBuilder.AddContext(xmlReader.GetAttribute("Context", string.Empty));
			int depth = xmlReader.Depth;
			WSSecurityTokenSerializer serializer = new WSSecurityTokenSerializer();
			while (xmlReader.Read())
			{
				bool flag = XmlNodeType.Element != xmlReader.NodeType;
				if (!flag)
				{
					string localName = xmlReader.LocalName;
					string text = localName;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 3452920994U)
					{
						if (num <= 2164581020U)
						{
							if (num != 2089097138U)
							{
								if (num == 2164581020U)
								{
									if (text == "RequestedAttachedReference")
									{
										RequestSecurityTokenResponse.ProcessRequestedAttachedReference(xmlReader, requestSecurityTokenResponseBuilder, serializer);
									}
								}
							}
							else if (text == "Entropy")
							{
								RequestSecurityTokenResponse.ProcessRequestedEntropy(xmlReader, requestSecurityTokenResponseBuilder, serializer);
							}
						}
						else if (num != 2992533650U)
						{
							if (num == 3452920994U)
							{
								if (text == "TokenType")
								{
									RequestSecurityTokenResponse.ProcessTokenType(xmlReader, requestSecurityTokenResponseBuilder);
								}
							}
						}
						else if (text == "RequestedSecurityToken")
						{
							RequestSecurityTokenResponse.ProcessRequestSecurityToken(xmlReader, requestSecurityTokenResponseBuilder);
						}
					}
					else if (num <= 3953207576U)
					{
						if (num != 3466172438U)
						{
							if (num == 3953207576U)
							{
								if (text == "RequestedProofToken")
								{
									RequestSecurityTokenResponse.ProcessRequestedProofToken(xmlReader, requestSecurityTokenResponseBuilder, serializer);
								}
							}
						}
						else if (text == "AppliesTo")
						{
							RequestSecurityTokenResponse.ProcessAppliesTo(xmlReader, requestSecurityTokenResponseBuilder);
						}
					}
					else if (num != 4079835662U)
					{
						if (num == 4165056189U)
						{
							if (text == "RequestedUnattachedReference")
							{
								RequestSecurityTokenResponse.ProcessRequestedUnattachedReference(xmlReader, requestSecurityTokenResponseBuilder, serializer);
							}
						}
					}
					else if (text == "Lifetime")
					{
						RequestSecurityTokenResponse.ProcessLifetime(xmlReader, requestSecurityTokenResponseBuilder);
					}
					bool flag2 = "RequestSecurityTokenResponse" == xmlReader.LocalName && xmlReader.Depth == depth && XmlNodeType.EndElement == xmlReader.NodeType;
					if (flag2)
					{
						break;
					}
				}
			}
			return requestSecurityTokenResponseBuilder.ToObject();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002DF4 File Offset: 0x00000FF4
		private static void ProcessTokenType(XmlReader xmlReader, RequestSecurityTokenResponseBuilder tokenResponseBuilder)
		{
			bool flag = !xmlReader.IsEmptyElement && WSTrustStandards.NamespacesUri.Contains(xmlReader.NamespaceURI);
			if (flag)
			{
				xmlReader.Read();
				tokenResponseBuilder.AddTokenType(xmlReader.ReadContentAsString());
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E38 File Offset: 0x00001038
		private static void ProcessRequestSecurityToken(XmlReader xmlReader, RequestSecurityTokenResponseBuilder tokenResponseBuilder)
		{
			bool flag = !xmlReader.IsEmptyElement && WSTrustStandards.NamespacesUri.Contains(xmlReader.NamespaceURI);
			if (flag)
			{
				xmlReader.Read();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(xmlReader);
				tokenResponseBuilder.AddRequestedSecurityToken(xmlDocument.DocumentElement);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E8C File Offset: 0x0000108C
		private static void ProcessLifetime(XmlReader xmlReader, RequestSecurityTokenResponseBuilder tokenResponseBuilder)
		{
			bool flag = !xmlReader.IsEmptyElement && WSTrustStandards.NamespacesUri.Contains(xmlReader.NamespaceURI);
			if (flag)
			{
				XElement xelement = XElement.Load(xmlReader.ReadSubtree(), LoadOptions.SetBaseUri);
				XElement xelement2 = xelement.Descendants().FirstOrDefault((XElement element) => element.Name.LocalName == "Created");
				XElement xelement3 = xelement.Descendants().FirstOrDefault((XElement element) => element.Name.LocalName == "Expires");
				DateTime dateTime = (xelement2 != null) ? Convert.ToDateTime(xelement2.Value, CultureInfo.InvariantCulture) : DateTime.MinValue;
				DateTime dateTime2 = (xelement3 != null) ? Convert.ToDateTime(xelement3.Value, CultureInfo.InvariantCulture) : DateTime.MinValue;
				bool flag2 = dateTime != DateTime.MinValue && dateTime2 != DateTime.MinValue;
				if (flag2)
				{
					tokenResponseBuilder.AddTokenLifetime(new Lifetime(dateTime, dateTime2));
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F8C File Offset: 0x0000118C
		private static void ProcessRequestedProofToken(XmlReader xmlReader, RequestSecurityTokenResponseBuilder tokenResponseBuilder, WSSecurityTokenSerializer serializer)
		{
			bool flag = !xmlReader.IsEmptyElement && WSTrustStandards.NamespacesUri.Contains(xmlReader.NamespaceURI);
			if (flag)
			{
				xmlReader.Read();
				bool flag2 = xmlReader.LocalName == "ComputedKey" && !xmlReader.IsEmptyElement;
				if (flag2)
				{
					tokenResponseBuilder.AddComputeKey(true);
					xmlReader.ReadContentAsString();
				}
				else
				{
					tokenResponseBuilder.AddRequestedProofToken(serializer.ReadToken(xmlReader, null));
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003008 File Offset: 0x00001208
		private static void ProcessRequestedEntropy(XmlReader xmlReader, RequestSecurityTokenResponseBuilder tokenResponseBuilder, WSSecurityTokenSerializer serializer)
		{
			bool flag = !xmlReader.IsEmptyElement && WSTrustStandards.NamespacesUri.Contains(xmlReader.NamespaceURI);
			if (flag)
			{
				xmlReader.Read();
				tokenResponseBuilder.AddRequestedProofToken(serializer.ReadToken(xmlReader, null));
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003050 File Offset: 0x00001250
		private static void ProcessRequestedAttachedReference(XmlReader xmlReader, RequestSecurityTokenResponseBuilder tokenResponseBuilder, WSSecurityTokenSerializer serializer)
		{
			bool flag = !xmlReader.IsEmptyElement && WSTrustStandards.NamespacesUri.Contains(xmlReader.NamespaceURI);
			if (flag)
			{
				xmlReader.Read();
				tokenResponseBuilder.AddRequestedAttachedReference(serializer.ReadKeyIdentifierClause(xmlReader));
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003094 File Offset: 0x00001294
		private static void ProcessRequestedUnattachedReference(XmlReader xmlReader, RequestSecurityTokenResponseBuilder tokenResponseBuilder, WSSecurityTokenSerializer serializer)
		{
			bool flag = !xmlReader.IsEmptyElement && WSTrustStandards.NamespacesUri.Contains(xmlReader.NamespaceURI);
			if (flag)
			{
				xmlReader.Read();
				tokenResponseBuilder.AddRequestedUnattachedReference(serializer.ReadKeyIdentifierClause(xmlReader));
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000030D8 File Offset: 0x000012D8
		private static void ProcessAppliesTo(XmlReader xmlReader, RequestSecurityTokenResponseBuilder tokenResponseBuilder)
		{
			bool flag = !xmlReader.IsEmptyElement && "http://www.w3.org/ns/ws-policy" == xmlReader.NamespaceURI;
			if (flag)
			{
				tokenResponseBuilder.AddAppliesTo(RequestSecurityTokenResponse.ProcessAppliesToElement(xmlReader));
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003114 File Offset: 0x00001314
		private static EndpointAddress ProcessAppliesToElement(XmlReader xmlReader)
		{
			int depth = xmlReader.Depth;
			EndpointAddress result = null;
			while (xmlReader.Read())
			{
				bool flag = xmlReader.NodeType == XmlNodeType.Element && !xmlReader.IsEmptyElement && xmlReader.LocalName == "EndpointReference";
				if (flag)
				{
					string namespaceURI = xmlReader.NamespaceURI;
					string a = namespaceURI;
					if (!(a == "http://www.w3.org/2005/08/addressing"))
					{
						if (a == "http://schemas.xmlsoap.org/ws/2004/08/addressing")
						{
							DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(EndpointAddressAugust2004));
							EndpointAddressAugust2004 endpointAddressAugust = (EndpointAddressAugust2004)dataContractSerializer.ReadObject(xmlReader, false);
							result = endpointAddressAugust.ToEndpointAddress();
						}
					}
					else
					{
						DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(EndpointAddress10));
						EndpointAddress10 endpointAddress = (EndpointAddress10)dataContractSerializer.ReadObject(xmlReader, false);
						result = endpointAddress.ToEndpointAddress();
					}
				}
				bool flag2 = "AppliesTo" == xmlReader.LocalName && ("http://www.w3.org/ns/ws-policy" == xmlReader.NamespaceURI || "http://schemas.xmlsoap.org/ws/2004/09/policy" == xmlReader.NamespaceURI) && xmlReader.Depth == depth && XmlNodeType.EndElement == xmlReader.NodeType;
				if (flag2)
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003244 File Offset: 0x00001444
		public void CreateFrom(XmlReader reader)
		{
			bool flag = reader == null;
			if (flag)
			{
				throw new ArgumentNullException("reader", "Operation needs a valid reader");
			}
			bool isEmptyElement = reader.IsEmptyElement;
			if (isEmptyElement)
			{
				throw new ArgumentException("reader cannot be an empty element", "reader");
			}
			RequestSecurityTokenResponse requestSecurityTokenResponse = this.BuildRequestSecurityTokenResponse(reader);
			this.RequestedSecurityToken = requestSecurityTokenResponse.RequestedSecurityToken;
			this.RequestedProofToken = requestSecurityTokenResponse.RequestedProofToken;
			this.RequestedAttachedReference = requestSecurityTokenResponse.RequestedAttachedReference;
			this.RequestedUnattachedReference = requestSecurityTokenResponse.RequestedUnattachedReference;
			this.ComputeKey = requestSecurityTokenResponse.ComputeKey;
			this.Context = requestSecurityTokenResponse.Context;
			this.TokenType = requestSecurityTokenResponse.TokenType;
			this.KeySize = requestSecurityTokenResponse.KeySize;
			this.AppliesTo = requestSecurityTokenResponse.AppliesTo;
			this.IssuerEntropy = requestSecurityTokenResponse.IssuerEntropy;
			this.TokenLifetime = requestSecurityTokenResponse.TokenLifetime;
		}
	}
}
