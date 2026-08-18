using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001F9 RID: 505
	public class ProtectedKey
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x0004740E File Offset: 0x0004560E
		public ProtectedKey(byte[] secret)
		{
			this._secret = secret;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004741D File Offset: 0x0004561D
		public ProtectedKey(byte[] secret, EncryptingCredentials wrappingCredentials)
		{
			this._secret = secret;
			this._wrappingCredentials = wrappingCredentials;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00047433 File Offset: 0x00045633
		public byte[] GetKeyBytes()
		{
			return this._secret;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0004743B File Offset: 0x0004563B
		public EncryptingCredentials WrappingCredentials
		{
			get
			{
				return this._wrappingCredentials;
			}
		}

		// Token: 0x04000E76 RID: 3702
		private byte[] _secret;

		// Token: 0x04000E77 RID: 3703
		private EncryptingCredentials _wrappingCredentials;
	}
}
