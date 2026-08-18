using System;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000181 RID: 385
	public class SessionSecurityTokenCacheKey
	{
		// Token: 0x06000C7A RID: 3194 RVA: 0x0003A550 File Offset: 0x00038750
		public SessionSecurityTokenCacheKey(string endpointId, UniqueId contextId, UniqueId keyGeneration)
		{
			if (endpointId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointId");
			}
			if (contextId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contextId");
			}
			this.endpointId = endpointId;
			this.contextId = contextId;
			this.keyGeneration = keyGeneration;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0003A5A4 File Offset: 0x000387A4
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x0003A5AC File Offset: 0x000387AC
		public bool IgnoreKeyGeneration
		{
			get
			{
				return this.ignoreKeyGeneration;
			}
			set
			{
				this.ignoreKeyGeneration = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0003A5B5 File Offset: 0x000387B5
		public UniqueId ContextId
		{
			get
			{
				return this.contextId;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0003A5BD File Offset: 0x000387BD
		public string EndpointId
		{
			get
			{
				return this.endpointId;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0003A5C5 File Offset: 0x000387C5
		public UniqueId KeyGeneration
		{
			get
			{
				return this.keyGeneration;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0003A5CD File Offset: 0x000387CD
		public static bool operator ==(SessionSecurityTokenCacheKey first, SessionSecurityTokenCacheKey second)
		{
			if (first == null)
			{
				return second == null;
			}
			return first.Equals(second);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0003A5DE File Offset: 0x000387DE
		public static bool operator !=(SessionSecurityTokenCacheKey first, SessionSecurityTokenCacheKey second)
		{
			return !(first == second);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0003A5EC File Offset: 0x000387EC
		public override bool Equals(object obj)
		{
			if (obj is SessionSecurityTokenCacheKey)
			{
				SessionSecurityTokenCacheKey sessionSecurityTokenCacheKey = obj as SessionSecurityTokenCacheKey;
				return !(sessionSecurityTokenCacheKey.ContextId != this.contextId) && StringComparer.Ordinal.Equals(sessionSecurityTokenCacheKey.EndpointId, this.endpointId) && (this.ignoreKeyGeneration || sessionSecurityTokenCacheKey.IgnoreKeyGeneration || sessionSecurityTokenCacheKey.KeyGeneration == this.keyGeneration);
			}
			return false;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0003A65C File Offset: 0x0003885C
		public override int GetHashCode()
		{
			if (this.keyGeneration == null)
			{
				return this.contextId.GetHashCode();
			}
			return this.contextId.GetHashCode() ^ this.keyGeneration.GetHashCode();
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0003A690 File Offset: 0x00038890
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.endpointId);
			stringBuilder.Append(';');
			stringBuilder.Append(this.contextId.ToString());
			stringBuilder.Append(';');
			if (!this.ignoreKeyGeneration && this.keyGeneration != null)
			{
				stringBuilder.Append(this.keyGeneration.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000C84 RID: 3204
		private UniqueId contextId;

		// Token: 0x04000C85 RID: 3205
		private UniqueId keyGeneration;

		// Token: 0x04000C86 RID: 3206
		private string endpointId;

		// Token: 0x04000C87 RID: 3207
		private bool ignoreKeyGeneration;
	}
}
