using System;
using System.Collections.Generic;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200017B RID: 379
	public class SecurityTokenHandlerCollectionManager
	{
		// Token: 0x06000C0B RID: 3083 RVA: 0x00037DAB File Offset: 0x00035FAB
		public SecurityTokenHandlerCollectionManager(string serviceName)
		{
			if (serviceName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceName");
			}
			this.serviceName = serviceName;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00037DE3 File Offset: 0x00035FE3
		private SecurityTokenHandlerCollectionManager() : this("")
		{
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00037DF0 File Offset: 0x00035FF0
		public int Count
		{
			get
			{
				return this.collections.Count;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00037DFD File Offset: 0x00035FFD
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00037E05 File Offset: 0x00036005
		public IEnumerable<SecurityTokenHandlerCollection> SecurityTokenHandlerCollections
		{
			get
			{
				return this.collections.Values;
			}
		}

		// Token: 0x170002FA RID: 762
		public SecurityTokenHandlerCollection this[string usage]
		{
			get
			{
				if (usage == null)
				{
					throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("usage");
				}
				return this.collections[usage];
			}
			set
			{
				if (usage == null)
				{
					throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("usage");
				}
				this.collections[usage] = value;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00037E4B File Offset: 0x0003604B
		public static SecurityTokenHandlerCollectionManager CreateEmptySecurityTokenHandlerCollectionManager()
		{
			return new SecurityTokenHandlerCollectionManager("");
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00037E58 File Offset: 0x00036058
		public static SecurityTokenHandlerCollectionManager CreateDefaultSecurityTokenHandlerCollectionManager()
		{
			SecurityTokenHandlerCollection value = SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
			SecurityTokenHandlerCollectionManager securityTokenHandlerCollectionManager = new SecurityTokenHandlerCollectionManager("");
			securityTokenHandlerCollectionManager.collections.Clear();
			securityTokenHandlerCollectionManager.collections.Add("", value);
			return securityTokenHandlerCollectionManager;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00037E93 File Offset: 0x00036093
		public bool ContainsKey(string usage)
		{
			return this.collections.ContainsKey(usage);
		}

		// Token: 0x04000C54 RID: 3156
		private Dictionary<string, SecurityTokenHandlerCollection> collections = new Dictionary<string, SecurityTokenHandlerCollection>();

		// Token: 0x04000C55 RID: 3157
		private string serviceName = "";

		// Token: 0x02000270 RID: 624
		public static class Usage
		{
			// Token: 0x04001113 RID: 4371
			public const string Default = "";

			// Token: 0x04001114 RID: 4372
			public const string ActAs = "ActAs";

			// Token: 0x04001115 RID: 4373
			public const string OnBehalfOf = "OnBehalfOf";
		}
	}
}
