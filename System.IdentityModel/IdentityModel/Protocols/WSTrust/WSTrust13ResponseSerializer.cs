using System;
using System.Xml;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000209 RID: 521
	public class WSTrust13ResponseSerializer : WSTrustResponseSerializer
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x00048054 File Offset: 0x00046254
		public override RequestSecurityTokenResponse ReadXml(XmlReader reader, WSTrustSerializationContext context)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			bool flag = false;
			if (reader.IsStartElement("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
			{
				reader.ReadStartElement("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
				flag = true;
			}
			RequestSecurityTokenResponse requestSecurityTokenResponse = WSTrustSerializationHelper.CreateResponse(reader, context, this, WSTrustConstantsAdapter.Trust13);
			requestSecurityTokenResponse.IsFinal = flag;
			if (flag)
			{
				reader.ReadEndElement();
			}
			return requestSecurityTokenResponse;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000480CC File Offset: 0x000462CC
		public override void ReadXmlElement(XmlReader reader, RequestSecurityTokenResponse rstr, WSTrustSerializationContext context)
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
			if (reader.IsStartElement("KeyWrapAlgorithm", "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
			{
				rstr.KeyWrapAlgorithm = reader.ReadElementContentAsString();
				return;
			}
			WSTrustSerializationHelper.ReadRSTRXml(reader, rstr, context, WSTrustConstantsAdapter.Trust13);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00048140 File Offset: 0x00046340
		public override void WriteKnownResponseElement(RequestSecurityTokenResponse rstr, XmlWriter writer, WSTrustSerializationContext context)
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
			WSTrustSerializationHelper.WriteKnownResponseElement(rstr, writer, context, this, WSTrustConstantsAdapter.Trust13);
			if (!string.IsNullOrEmpty(rstr.KeyWrapAlgorithm))
			{
				this.WriteXmlElement(writer, "KeyWrapAlgorithm", rstr.KeyWrapAlgorithm, rstr, context);
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000481B8 File Offset: 0x000463B8
		public override void WriteXml(RequestSecurityTokenResponse response, XmlWriter writer, WSTrustSerializationContext context)
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
			if (response.IsFinal)
			{
				writer.WriteStartElement("trust", "RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
			}
			WSTrustSerializationHelper.WriteResponse(response, writer, context, this, WSTrustConstantsAdapter.Trust13);
			if (response.IsFinal)
			{
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00048238 File Offset: 0x00046438
		public override void WriteXmlElement(XmlWriter writer, string elementName, object elementValue, RequestSecurityTokenResponse rstr, WSTrustSerializationContext context)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (string.IsNullOrEmpty(elementName))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("elementName");
			}
			if (rstr == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (StringComparer.Ordinal.Equals(elementName, "KeyWrapAlgorithm"))
			{
				writer.WriteElementString("trust", "KeyWrapAlgorithm", "http://docs.oasis-open.org/ws-sx/ws-trust/200512", (string)elementValue);
				return;
			}
			WSTrustSerializationHelper.WriteRSTRXml(writer, elementName, elementValue, context, WSTrustConstantsAdapter.Trust13);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000482D0 File Offset: 0x000464D0
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("RequestSecurityTokenResponseCollection", "http://docs.oasis-open.org/ws-sx/ws-trust/200512") || reader.IsStartElement("RequestSecurityTokenResponse", "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
		}
	}
}
