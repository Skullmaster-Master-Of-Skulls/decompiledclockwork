using System;
using System.Xml;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x0200020F RID: 527
	public class WSTrustFeb2005ResponseSerializer : WSTrustResponseSerializer
	{
		// Token: 0x0600114A RID: 4426 RVA: 0x00048613 File Offset: 0x00046813
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
			return WSTrustSerializationHelper.CreateResponse(reader, context, this, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00048648 File Offset: 0x00046848
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
			WSTrustSerializationHelper.ReadRSTRXml(reader, rstr, context, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0004869C File Offset: 0x0004689C
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
			WSTrustSerializationHelper.WriteKnownResponseElement(rstr, writer, context, this, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000486F0 File Offset: 0x000468F0
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
			WSTrustSerializationHelper.WriteResponse(response, writer, context, this, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00048744 File Offset: 0x00046944
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
			WSTrustSerializationHelper.WriteRSTRXml(writer, elementName, elementValue, context, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000487AE File Offset: 0x000469AE
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("RequestSecurityTokenResponse", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}
	}
}
