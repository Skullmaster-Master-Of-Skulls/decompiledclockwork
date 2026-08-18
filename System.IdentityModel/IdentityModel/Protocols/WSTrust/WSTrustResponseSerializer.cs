using System;
using System.Xml;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000212 RID: 530
	public abstract class WSTrustResponseSerializer
	{
		// Token: 0x06001182 RID: 4482
		public abstract RequestSecurityTokenResponse ReadXml(XmlReader reader, WSTrustSerializationContext context);

		// Token: 0x06001183 RID: 4483
		public abstract void ReadXmlElement(XmlReader reader, RequestSecurityTokenResponse requestSecurityTokenResponse, WSTrustSerializationContext context);

		// Token: 0x06001184 RID: 4484
		public abstract void WriteKnownResponseElement(RequestSecurityTokenResponse requestSecurityTokenResponse, XmlWriter writer, WSTrustSerializationContext context);

		// Token: 0x06001185 RID: 4485
		public abstract void WriteXml(RequestSecurityTokenResponse response, XmlWriter writer, WSTrustSerializationContext context);

		// Token: 0x06001186 RID: 4486
		public abstract void WriteXmlElement(XmlWriter writer, string elementName, object elementValue, RequestSecurityTokenResponse requestSecurityTokenResponse, WSTrustSerializationContext context);

		// Token: 0x06001187 RID: 4487 RVA: 0x00048A3C File Offset: 0x00046C3C
		public virtual RequestSecurityTokenResponse CreateInstance()
		{
			return new RequestSecurityTokenResponse();
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00048A43 File Offset: 0x00046C43
		public virtual void Validate(RequestSecurityTokenResponse requestSecurityTokenResponse)
		{
			if (requestSecurityTokenResponse == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
			}
		}

		// Token: 0x06001189 RID: 4489
		public abstract bool CanRead(XmlReader reader);
	}
}
