using System;
using System.Xml;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000211 RID: 529
	public abstract class WSTrustRequestSerializer
	{
		// Token: 0x06001178 RID: 4472
		public abstract RequestSecurityToken ReadXml(XmlReader reader, WSTrustSerializationContext context);

		// Token: 0x06001179 RID: 4473
		public abstract void ReadXmlElement(XmlReader reader, RequestSecurityToken requestSecurityToken, WSTrustSerializationContext context);

		// Token: 0x0600117A RID: 4474
		public abstract void WriteKnownRequestElement(RequestSecurityToken requestSecurityToken, XmlWriter writer, WSTrustSerializationContext context);

		// Token: 0x0600117B RID: 4475
		public abstract void WriteXml(RequestSecurityToken request, XmlWriter writer, WSTrustSerializationContext context);

		// Token: 0x0600117C RID: 4476
		public abstract void WriteXmlElement(XmlWriter writer, string elementName, object elementValue, RequestSecurityToken requestSecurityToken, WSTrustSerializationContext context);

		// Token: 0x0600117D RID: 4477 RVA: 0x00048976 File Offset: 0x00046B76
		public virtual RequestSecurityToken CreateRequestSecurityToken()
		{
			return new RequestSecurityToken();
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00048980 File Offset: 0x00046B80
		public virtual void Validate(RequestSecurityToken requestSecurityToken)
		{
			if (requestSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
			}
			if ((StringComparer.Ordinal.Equals(requestSecurityToken.RequestType, "http://schemas.microsoft.com/idfx/requesttype/issue") || requestSecurityToken.RequestType == null) && StringComparer.Ordinal.Equals(requestSecurityToken.KeyType, "http://schemas.microsoft.com/idfx/keytype/asymmetric") && (requestSecurityToken.UseKey == null || (requestSecurityToken.UseKey.SecurityKeyIdentifier == null && requestSecurityToken.UseKey.Token == null)))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3091")));
			}
		}

		// Token: 0x0600117F RID: 4479
		public abstract bool CanRead(XmlReader reader);

		// Token: 0x06001180 RID: 4480 RVA: 0x00048A12 File Offset: 0x00046C12
		protected virtual void ReadCustomElement(XmlReader reader, WSTrustSerializationContext context)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID2072", new object[]
			{
				reader.LocalName
			})));
		}
	}
}
