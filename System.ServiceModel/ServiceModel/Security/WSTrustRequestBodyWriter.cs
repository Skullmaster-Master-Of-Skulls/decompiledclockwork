using System;
using System.IdentityModel.Protocols.WSTrust;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000374 RID: 884
	public class WSTrustRequestBodyWriter : BodyWriter
	{
		// Token: 0x060020B7 RID: 8375 RVA: 0x00078C2C File Offset: 0x00076E2C
		public WSTrustRequestBodyWriter(RequestSecurityToken requestSecurityToken, WSTrustRequestSerializer serializer, WSTrustSerializationContext serializationContext) : base(true)
		{
			if (requestSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestSecurityToken");
			}
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializer");
			}
			if (serializationContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializationContext");
			}
			this._requestSecurityToken = requestSecurityToken;
			this._serializer = serializer;
			this._serializationContext = serializationContext;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00078C8E File Offset: 0x00076E8E
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this._serializer.WriteXml(this._requestSecurityToken, writer, this._serializationContext);
		}

		// Token: 0x04001F21 RID: 7969
		private WSTrustSerializationContext _serializationContext;

		// Token: 0x04001F22 RID: 7970
		private RequestSecurityToken _requestSecurityToken;

		// Token: 0x04001F23 RID: 7971
		private WSTrustRequestSerializer _serializer;
	}
}
