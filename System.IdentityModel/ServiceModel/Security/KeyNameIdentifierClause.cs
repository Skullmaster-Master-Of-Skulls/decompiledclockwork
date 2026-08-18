using System;
using System.Globalization;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Security
{
	// Token: 0x0200000D RID: 13
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public class KeyNameIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002C63 File Offset: 0x00000E63
		public KeyNameIdentifierClause(string keyName) : base(null)
		{
			if (keyName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyName");
			}
			this.keyName = keyName;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002C86 File Offset: 0x00000E86
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002C90 File Offset: 0x00000E90
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			KeyNameIdentifierClause keyNameIdentifierClause = keyIdentifierClause as KeyNameIdentifierClause;
			return this == keyNameIdentifierClause || (keyNameIdentifierClause != null && keyNameIdentifierClause.Matches(this.keyName));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002CBB File Offset: 0x00000EBB
		public bool Matches(string keyName)
		{
			return this.keyName == keyName;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002CC9 File Offset: 0x00000EC9
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "KeyNameIdentifierClause(KeyName = '{0}')", new object[]
			{
				this.KeyName
			});
		}

		// Token: 0x0400006D RID: 109
		private string keyName;
	}
}
