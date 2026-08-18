using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x0200000F RID: 15
	public class RequestSecurityTokenWriter
	{
		// Token: 0x06000095 RID: 149 RVA: 0x000035A0 File Offset: 0x000017A0
		public void WriteRST(XmlWriter writer, RequestSecurityToken requestSecurityToken)
		{
			writer.WriteStartElement("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
			this.WriteTokenType(writer, requestSecurityToken);
			this.WriteRequestType(writer, requestSecurityToken);
			this.WriteAppliesTo(writer, requestSecurityToken);
			this.WriteRequestorEntropy(writer, requestSecurityToken);
			this.WriteLifeTime(writer, requestSecurityToken);
			this.WriteKeyType(writer, requestSecurityToken);
			this.WriteKeySize(writer, requestSecurityToken);
			this.WriteComputedKeyAlgorithm(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000360D File Offset: 0x0000180D
		private void WriteComputedKeyAlgorithm(XmlWriter writer)
		{
			writer.WriteStartElement("ComputedKeyAlgorithm", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
			writer.WriteValue("http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1");
			writer.WriteEndElement();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003634 File Offset: 0x00001834
		private void WriteLifeTime(XmlWriter writer, RequestSecurityToken requestSecurityToken)
		{
			bool flag = requestSecurityToken.RequestedLifetime == null;
			if (!flag)
			{
				writer.WriteStartElement("Lifetime", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
				writer.WriteStartElement("Created", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
				DateTime? dateTime = requestSecurityToken.RequestedLifetime.Created;
				writer.WriteString(((dateTime != null) ? dateTime.GetValueOrDefault().ToString("o") : null) ?? string.Empty);
				writer.WriteEndElement();
				writer.WriteStartElement("Expires", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
				dateTime = requestSecurityToken.RequestedLifetime.Expires;
				writer.WriteString(((dateTime != null) ? dateTime.GetValueOrDefault().ToString("o") : null) ?? string.Empty);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003714 File Offset: 0x00001914
		private void WriteTokenType(XmlWriter writer, RequestSecurityToken requestSecurityToken)
		{
			bool flag = string.IsNullOrEmpty(requestSecurityToken.TokenType);
			if (!flag)
			{
				writer.WriteStartElement("TokenType", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
				writer.WriteString(requestSecurityToken.TokenType);
				writer.WriteEndElement();
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003758 File Offset: 0x00001958
		private void WriteRequestType(XmlWriter writer, RequestSecurityToken requestSecurityToken)
		{
			bool flag = string.IsNullOrEmpty(requestSecurityToken.RequestType);
			if (!flag)
			{
				writer.WriteStartElement("RequestType", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
				writer.WriteString(requestSecurityToken.RequestType);
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000379C File Offset: 0x0000199C
		private void WriteAppliesTo(XmlWriter writer, RequestSecurityToken requestSecurityToken)
		{
			bool flag = requestSecurityToken.AppliesTo == null;
			if (!flag)
			{
				writer.WriteStartElement("AppliesTo", "http://www.w3.org/ns/ws-policy");
				requestSecurityToken.AppliesTo.WriteTo(AddressingVersion.WSAddressing10, writer);
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000037E8 File Offset: 0x000019E8
		private void WriteRequestorEntropy(XmlWriter writer, RequestSecurityToken requestSecurityToken)
		{
			bool flag = requestSecurityToken.RequestorEntropy == null;
			if (!flag)
			{
				writer.WriteStartElement("Entropy", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
				BinarySecretSecurityToken binarySecretSecurityToken = requestSecurityToken.RequestorEntropy as BinarySecretSecurityToken;
				bool flag2 = binarySecretSecurityToken != null;
				if (flag2)
				{
					writer.WriteStartElement("BinarySecret", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
					writer.WriteAttributeString("Type", "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Nonce");
					byte[] keyBytes = binarySecretSecurityToken.GetKeyBytes();
					writer.WriteBase64(keyBytes, 0, keyBytes.Length);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003870 File Offset: 0x00001A70
		private void WriteKeyType(XmlWriter writer, RequestSecurityToken requestSecurityToken)
		{
			bool flag = string.IsNullOrEmpty(requestSecurityToken.KeyType);
			if (!flag)
			{
				writer.WriteStartElement("KeyType", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
				writer.WriteString(requestSecurityToken.KeyType);
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000038B4 File Offset: 0x00001AB4
		private void WriteKeySize(XmlWriter writer, RequestSecurityToken requestSecurityToken)
		{
			bool flag = requestSecurityToken.KeySize <= 0;
			if (!flag)
			{
				writer.WriteStartElement("KeySize", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
				writer.WriteValue(requestSecurityToken.KeySize);
				writer.WriteEndElement();
			}
		}
	}
}
