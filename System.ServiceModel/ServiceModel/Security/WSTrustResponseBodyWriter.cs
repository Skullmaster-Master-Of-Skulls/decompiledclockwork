using System;
using System.IdentityModel.Protocols.WSTrust;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000376 RID: 886
	public class WSTrustResponseBodyWriter : BodyWriter
	{
		// Token: 0x060020BC RID: 8380 RVA: 0x00078CD0 File Offset: 0x00076ED0
		public WSTrustResponseBodyWriter(RequestSecurityTokenResponse requestSecurityTokenResponse, WSTrustResponseSerializer serializer, WSTrustSerializationContext context) : base(true)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serializer");
			}
			if (requestSecurityTokenResponse == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestSecurityTokenResponse");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this._serializer = serializer;
			this._rstr = requestSecurityTokenResponse;
			this._context = context;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00078D32 File Offset: 0x00076F32
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this._serializer.WriteXml(this._rstr, writer, this._context);
		}

		// Token: 0x04001F26 RID: 7974
		private WSTrustResponseSerializer _serializer;

		// Token: 0x04001F27 RID: 7975
		private RequestSecurityTokenResponse _rstr;

		// Token: 0x04001F28 RID: 7976
		private WSTrustSerializationContext _context;
	}
}
