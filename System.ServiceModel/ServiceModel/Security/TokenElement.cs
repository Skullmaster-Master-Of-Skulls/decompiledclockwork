using System;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B6 RID: 694
	internal class TokenElement : ISecurityElement
	{
		// Token: 0x060015EA RID: 5610 RVA: 0x00053B36 File Offset: 0x00051D36
		public TokenElement(SecurityToken token, SecurityStandardsManager standardsManager)
		{
			this.token = token;
			this.standardsManager = standardsManager;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00053B4C File Offset: 0x00051D4C
		public override bool Equals(object item)
		{
			TokenElement tokenElement = item as TokenElement;
			return tokenElement != null && this.token == tokenElement.token && this.standardsManager == tokenElement.standardsManager;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00053B81 File Offset: 0x00051D81
		public override int GetHashCode()
		{
			return this.token.GetHashCode() ^ this.standardsManager.GetHashCode();
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00053B9A File Offset: 0x00051D9A
		public bool HasId
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00053B9D File Offset: 0x00051D9D
		public string Id
		{
			get
			{
				return this.token.Id;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00053BAA File Offset: 0x00051DAA
		public SecurityToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00053BB2 File Offset: 0x00051DB2
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			this.standardsManager.SecurityTokenSerializer.WriteToken(writer, this.token);
		}

		// Token: 0x04001B91 RID: 7057
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001B92 RID: 7058
		private SecurityToken token;
	}
}
