using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;

namespace System.IdentityModel
{
	// Token: 0x02000073 RID: 115
	public class Scope
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x0000DE78 File Offset: 0x0000C078
		public Scope() : this(null, null, null)
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000DE83 File Offset: 0x0000C083
		public Scope(string appliesToAddress) : this(appliesToAddress, null, null)
		{
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000DE8E File Offset: 0x0000C08E
		public Scope(string appliesToAddress, SigningCredentials signingCredentials) : this(appliesToAddress, signingCredentials, null)
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000DE99 File Offset: 0x0000C099
		public Scope(string appliesToAddress, EncryptingCredentials encryptingCredentials) : this(appliesToAddress, null, encryptingCredentials)
		{
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		public Scope(string appliesToAddress, SigningCredentials signingCredentials, EncryptingCredentials encryptingCredentials)
		{
			this._appliesToAddress = appliesToAddress;
			this._signingCredentials = signingCredentials;
			this._encryptingCredentials = encryptingCredentials;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000DEDA File Offset: 0x0000C0DA
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000DEE2 File Offset: 0x0000C0E2
		public virtual string AppliesToAddress
		{
			get
			{
				return this._appliesToAddress;
			}
			set
			{
				this._appliesToAddress = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000DEEB File Offset: 0x0000C0EB
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000DEF3 File Offset: 0x0000C0F3
		public virtual EncryptingCredentials EncryptingCredentials
		{
			get
			{
				return this._encryptingCredentials;
			}
			set
			{
				this._encryptingCredentials = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000DF04 File Offset: 0x0000C104
		public virtual string ReplyToAddress
		{
			get
			{
				return this._replyToAddress;
			}
			set
			{
				this._replyToAddress = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000DF0D File Offset: 0x0000C10D
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000DF15 File Offset: 0x0000C115
		public virtual SigningCredentials SigningCredentials
		{
			get
			{
				return this._signingCredentials;
			}
			set
			{
				this._signingCredentials = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000DF1E File Offset: 0x0000C11E
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000DF26 File Offset: 0x0000C126
		public virtual bool SymmetricKeyEncryptionRequired
		{
			get
			{
				return this._symmetricKeyEncryptionRequired;
			}
			set
			{
				this._symmetricKeyEncryptionRequired = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000DF2F File Offset: 0x0000C12F
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000DF37 File Offset: 0x0000C137
		public virtual bool TokenEncryptionRequired
		{
			get
			{
				return this._tokenEncryptionRequired;
			}
			set
			{
				this._tokenEncryptionRequired = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000DF40 File Offset: 0x0000C140
		public virtual Dictionary<string, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x0400036F RID: 879
		private string _appliesToAddress;

		// Token: 0x04000370 RID: 880
		private string _replyToAddress;

		// Token: 0x04000371 RID: 881
		private EncryptingCredentials _encryptingCredentials;

		// Token: 0x04000372 RID: 882
		private SigningCredentials _signingCredentials;

		// Token: 0x04000373 RID: 883
		private bool _symmetricKeyEncryptionRequired = true;

		// Token: 0x04000374 RID: 884
		private bool _tokenEncryptionRequired = true;

		// Token: 0x04000375 RID: 885
		private Dictionary<string, object> _properties = new Dictionary<string, object>();
	}
}
