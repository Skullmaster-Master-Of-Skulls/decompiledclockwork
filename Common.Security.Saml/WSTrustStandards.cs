using System;
using System.Linq;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000020 RID: 32
	public static class WSTrustStandards
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000059C0 File Offset: 0x00003BC0
		public static bool Contains(this string[] array, string pattern)
		{
			return array.Any((string value) => value.Equals(pattern));
		}

		// Token: 0x0400005A RID: 90
		public static string[] NamespacesUri = new string[]
		{
			"http://docs.oasis-open.org/ws-sx/ws-trust/200512",
			"http://schemas.xmlsoap.org/ws/2005/02/trust"
		};

		// Token: 0x0400005B RID: 91
		public static string Wss10NamespaceUri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

		// Token: 0x0400005C RID: 92
		public static string[] AsymetricKeyTypes = new string[]
		{
			"http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey",
			"http://docs.oasis-open.org/ws-sx/ws-trust/200512/AsymmetricKey"
		};

		// Token: 0x0400005D RID: 93
		public const string UtilityNamespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

		// Token: 0x0400005E RID: 94
		public static string ClaimsDialect = "http://schemas.xmlsoap.org/ws/2005/05/identity";

		// Token: 0x02000034 RID: 52
		public static class Oasis
		{
			// Token: 0x04000094 RID: 148
			public const string TargetNamespaceUri = "http://docs.oasis-open.org/ws-sx/ws-trust/200512";

			// Token: 0x0200003C RID: 60
			public static class KeyTypes
			{
				// Token: 0x040000BE RID: 190
				public const string Symmetric = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey";

				// Token: 0x040000BF RID: 191
				public const string ASymmetric = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/ASymmetricKey";

				// Token: 0x040000C0 RID: 192
				public const string Nonce = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Nonce";

				// Token: 0x040000C1 RID: 193
				public const string Bearer = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer";
			}

			// Token: 0x0200003D RID: 61
			public static class Actions
			{
				// Token: 0x040000C2 RID: 194
				public const string Issue = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue";

				// Token: 0x040000C3 RID: 195
				public const string IssueReply = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal";

				// Token: 0x040000C4 RID: 196
				public const string Renew = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew";

				// Token: 0x040000C5 RID: 197
				public const string RenewReply = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew";
			}

			// Token: 0x0200003E RID: 62
			public static class ComputedKeyAlgorithms
			{
				// Token: 0x040000C6 RID: 198
				public const string PSHA1 = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1";
			}
		}

		// Token: 0x02000035 RID: 53
		public static class Elements
		{
			// Token: 0x04000095 RID: 149
			public const string KeySize = "KeySize";

			// Token: 0x04000096 RID: 150
			public const string KeyType = "KeyType";

			// Token: 0x04000097 RID: 151
			public const string UseKey = "UseKey";

			// Token: 0x04000098 RID: 152
			public const string Entropy = "Entropy";

			// Token: 0x04000099 RID: 153
			public const string BinarySecret = "BinarySecret";

			// Token: 0x0400009A RID: 154
			public const string RequestSecurityToken = "RequestSecurityToken";

			// Token: 0x0400009B RID: 155
			public const string RequestSecurityTokenResponseCollection = "RequestSecurityTokenResponseCollection";

			// Token: 0x0400009C RID: 156
			public const string RequestSecurityTokenResponse = "RequestSecurityTokenResponse";

			// Token: 0x0400009D RID: 157
			public const string RequestType = "RequestType";

			// Token: 0x0400009E RID: 158
			public const string TokenType = "TokenType";

			// Token: 0x0400009F RID: 159
			public const string RequestedSecurityToken = "RequestedSecurityToken";

			// Token: 0x040000A0 RID: 160
			public const string RequestedAttachedReference = "RequestedAttachedReference";

			// Token: 0x040000A1 RID: 161
			public const string RequestedUnattachedReference = "RequestedUnattachedReference";

			// Token: 0x040000A2 RID: 162
			public const string RequestedProofToken = "RequestedProofToken";

			// Token: 0x040000A3 RID: 163
			public const string ComputedKey = "ComputedKey";

			// Token: 0x040000A4 RID: 164
			public const string ComputedKeyAlgorithm = "ComputedKeyAlgorithm";

			// Token: 0x040000A5 RID: 165
			public const string Claims = "Claims";

			// Token: 0x040000A6 RID: 166
			public const string WssSecurityTokenReference = "SecurityTokenReference";

			// Token: 0x040000A7 RID: 167
			public const string SecondaryParameters = "SecondaryParameters";

			// Token: 0x040000A8 RID: 168
			public const string Lifetime = "Lifetime";

			// Token: 0x040000A9 RID: 169
			public const string RenewTarget = "RenewTarget";
		}

		// Token: 0x02000036 RID: 54
		public static class LifetimeElements
		{
			// Token: 0x040000AA RID: 170
			public const string Created = "Created";

			// Token: 0x040000AB RID: 171
			public const string Expires = "Expires";
		}

		// Token: 0x02000037 RID: 55
		public static class Attributes
		{
			// Token: 0x040000AC RID: 172
			public const string Context = "Context";

			// Token: 0x040000AD RID: 173
			public const string Type = "Type";

			// Token: 0x040000AE RID: 174
			public const string Dialect = "Dialect";
		}

		// Token: 0x02000038 RID: 56
		public static class RequestTypes
		{
			// Token: 0x040000AF RID: 175
			public const string Issue = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue";

			// Token: 0x040000B0 RID: 176
			public const string Renew = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Renew";
		}
	}
}
