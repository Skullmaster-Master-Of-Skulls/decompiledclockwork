using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.Xml;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x0200000A RID: 10
	public class RequestSecurityToken
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000027D8 File Offset: 0x000009D8
		public RequestSecurityToken() : this(string.Empty, string.Empty, 0, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey", null, null, null)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027F8 File Offset: 0x000009F8
		public RequestSecurityToken(string tokenType, string requestType, int keySize, string keyType, SecurityToken entropy, EndpointAddress appliesTo, Lifetime requestedLifetime)
		{
			this.KeyType = keyType;
			this.RequestType = requestType;
			this.RequestorEntropy = entropy;
			this.TokenType = tokenType;
			this.KeySize = keySize;
			this.AppliesTo = appliesTo;
			this.RequestedLifetime = requestedLifetime;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002849 File Offset: 0x00000A49
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002851 File Offset: 0x00000A51
		public string TokenType { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000285A File Offset: 0x00000A5A
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002862 File Offset: 0x00000A62
		public int KeySize { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000286B File Offset: 0x00000A6B
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002873 File Offset: 0x00000A73
		public EndpointAddress AppliesTo { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000287C File Offset: 0x00000A7C
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002884 File Offset: 0x00000A84
		public string RequestType { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000288D File Offset: 0x00000A8D
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002895 File Offset: 0x00000A95
		public string KeyType { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000289E File Offset: 0x00000A9E
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000028A6 File Offset: 0x00000AA6
		public SecurityToken RequestorEntropy { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028AF File Offset: 0x00000AAF
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000028B7 File Offset: 0x00000AB7
		public Lifetime RequestedLifetime { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028C0 File Offset: 0x00000AC0
		public virtual RequestSecurityTokenWriter TokenWriter
		{
			get
			{
				return new RequestSecurityTokenWriter();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028D7 File Offset: 0x00000AD7
		public void WriteTo(XmlWriter writer)
		{
			this.TokenWriter.WriteRST(writer, this);
		}
	}
}
