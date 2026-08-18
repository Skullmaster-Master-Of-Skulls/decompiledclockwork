using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000208 RID: 520
	public class WSTrust13RequestSerializer : WSTrustRequestSerializer
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x00047A4C File Offset: 0x00045C4C
		public override RequestSecurityToken ReadXml(XmlReader reader, WSTrustSerializationContext context)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return WSTrustSerializationHelper.CreateRequest(reader, context, this, WSTrustConstantsAdapter.Trust13);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00047A84 File Offset: 0x00045C84
		public override void ReadXmlElement(XmlReader reader, RequestSecurityToken rst, WSTrustSerializationContext context)
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
			if (reader.IsStartElement("SecondaryParameters", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
			{
				rst.SecondaryParameters = this.ReadSecondaryParameters(reader, context);
				return;
			}
			if (reader.IsStartElement("KeyWrapAlgorithm", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
			{
				rst.KeyWrapAlgorithm = reader.ReadElementContentAsString();
				if (!UriUtil.CanCreateValidUri(rst.KeyWrapAlgorithm, UriKind.Absolute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
					{
						"KeyWrapAlgorithm",
						"http://docs.oasis-open.org/ws-sx/ws-trust/200512",
						rst.KeyWrapAlgorithm
					})));
				}
				return;
			}
			else
			{
				if (!reader.IsStartElement("ValidateTarget", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
				{
					WSTrustSerializationHelper.ReadRSTXml(reader, rst, context, WSTrustConstantsAdapter.Trust13);
					return;
				}
				if (!reader.IsEmptyElement)
				{
					rst.ValidateTarget = new SecurityTokenElement(WSTrustSerializationHelper.ReadInnerXml(reader), context.SecurityTokenHandlers);
				}
				else
				{
					reader.Read();
				}
				if (rst.ValidateTarget == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3221")));
				}
				return;
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00047BBC File Offset: 0x00045DBC
		public override void WriteKnownRequestElement(RequestSecurityToken rst, XmlWriter writer, WSTrustSerializationContext context)
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
			WSTrustSerializationHelper.WriteKnownRequestElement(rst, writer, context, this, WSTrustConstantsAdapter.Trust13);
			if (!string.IsNullOrEmpty(rst.KeyWrapAlgorithm))
			{
				if (!UriUtil.CanCreateValidUri(rst.KeyWrapAlgorithm, UriKind.Absolute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
					{
						"KeyWrapAlgorithm",
						"http://docs.oasis-open.org/ws-sx/ws-trust/200512",
						rst.KeyWrapAlgorithm
					})));
				}
				this.WriteXmlElement(writer, "KeyWrapAlgorithm", rst.KeyWrapAlgorithm, rst, context);
			}
			if (rst.SecondaryParameters != null)
			{
				this.WriteXmlElement(writer, "SecondaryParameters", rst.SecondaryParameters, rst, context);
			}
			if (rst.ValidateTarget != null)
			{
				this.WriteXmlElement(writer, "ValidateTarget", rst.ValidateTarget, rst, context);
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00047CB0 File Offset: 0x00045EB0
		public override void WriteXml(RequestSecurityToken request, XmlWriter writer, WSTrustSerializationContext context)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			WSTrustSerializationHelper.WriteRequest(request, writer, context, this, WSTrustConstantsAdapter.Trust13);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00047D04 File Offset: 0x00045F04
		public override void WriteXmlElement(XmlWriter writer, string elementName, object elementValue, RequestSecurityToken rst, WSTrustSerializationContext context)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (string.IsNullOrEmpty(elementName))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("elementName");
			}
			if (rst == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (StringComparer.Ordinal.Equals(elementName, "SecondaryParameters"))
			{
				RequestSecurityToken requestSecurityToken = elementValue as RequestSecurityToken;
				if (requestSecurityToken == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2064", new object[]
					{
						"SecondaryParameters"
					})));
				}
				if (requestSecurityToken.SecondaryParameters != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2055")));
				}
				writer.WriteStartElement("trust", "SecondaryParameters", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
				this.WriteKnownRequestElement(requestSecurityToken, writer, context);
				foreach (KeyValuePair<string, object> keyValuePair in requestSecurityToken.Properties)
				{
					this.WriteXmlElement(writer, keyValuePair.Key, keyValuePair.Value, rst, context);
				}
				writer.WriteEndElement();
				return;
			}
			else
			{
				if (StringComparer.Ordinal.Equals(elementName, "KeyWrapAlgorithm"))
				{
					writer.WriteElementString("trust", "KeyWrapAlgorithm", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", (string)elementValue);
					return;
				}
				if (!StringComparer.Ordinal.Equals(elementName, "ValidateTarget"))
				{
					WSTrustSerializationHelper.WriteRSTXml(writer, elementName, elementValue, context, WSTrustConstantsAdapter.Trust13);
					return;
				}
				SecurityTokenElement securityTokenElement = elementValue as SecurityTokenElement;
				if (securityTokenElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("elementValue", SR.GetString("ID3222", new object[]
					{
						"ValidateTarget",
						"http://docs.oasis-open.org/ws-sx/ws-trust/200512",
						typeof(SecurityTokenElement),
						elementValue
					}));
				}
				writer.WriteStartElement("trust", "ValidateTarget", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
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
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00047F28 File Offset: 0x00046128
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("RequestSecurityToken", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00047F50 File Offset: 0x00046150
		protected virtual RequestSecurityToken ReadSecondaryParameters(XmlReader reader, WSTrustSerializationContext context)
		{
			RequestSecurityToken requestSecurityToken = this.CreateRequestSecurityToken();
			if (reader.IsEmptyElement)
			{
				reader.Read();
				reader.MoveToContent();
				return requestSecurityToken;
			}
			reader.ReadStartElement();
			while (reader.IsStartElement())
			{
				if (reader.IsStartElement("KeyWrapAlgorithm", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
				{
					requestSecurityToken.KeyWrapAlgorithm = reader.ReadElementContentAsString();
					if (!UriUtil.CanCreateValidUri(requestSecurityToken.KeyWrapAlgorithm, UriKind.Absolute))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3135", new object[]
						{
							"KeyWrapAlgorithm",
							"http://docs.oasis-open.org/ws-sx/ws-trust/200512",
							requestSecurityToken.KeyWrapAlgorithm
						})));
					}
				}
				else
				{
					if (reader.IsStartElement("SecondaryParameters", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WSTrustSerializationException(SR.GetString("ID3130")));
					}
					WSTrustSerializationHelper.ReadRSTXml(reader, requestSecurityToken, context, WSTrustConstantsAdapter.GetConstantsAdapter(reader.NamespaceURI) ?? WSTrustConstantsAdapter.TrustFeb2005);
				}
			}
			reader.ReadEndElement();
			return requestSecurityToken;
		}
	}
}
