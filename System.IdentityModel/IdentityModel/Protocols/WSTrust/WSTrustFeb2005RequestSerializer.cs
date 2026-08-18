using System;
using System.Xml;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x0200020E RID: 526
	public class WSTrustFeb2005RequestSerializer : WSTrustRequestSerializer
	{
		// Token: 0x06001143 RID: 4419 RVA: 0x00048452 File Offset: 0x00046652
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
			return WSTrustSerializationHelper.CreateRequest(reader, context, this, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00048488 File Offset: 0x00046688
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
			WSTrustSerializationHelper.ReadRSTXml(reader, rst, context, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000484DC File Offset: 0x000466DC
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
			WSTrustSerializationHelper.WriteKnownRequestElement(rst, writer, context, this, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00048530 File Offset: 0x00046730
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
			WSTrustSerializationHelper.WriteRequest(request, writer, context, this, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00048584 File Offset: 0x00046784
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
			WSTrustSerializationHelper.WriteRSTXml(writer, elementName, elementValue, context, WSTrustConstantsAdapter.TrustFeb2005);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000485EE File Offset: 0x000467EE
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("RequestSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust");
		}
	}
}
