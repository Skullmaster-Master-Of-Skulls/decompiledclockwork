using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BC RID: 1212
	public class ParametersWithSalt : ICipherParameters
	{
		// Token: 0x06002953 RID: 10579 RVA: 0x000FC69A File Offset: 0x000FB69A
		public ParametersWithSalt(ICipherParameters parameters, byte[] salt) : this(parameters, salt, 0, salt.Length)
		{
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000FC6A8 File Offset: 0x000FB6A8
		public ParametersWithSalt(ICipherParameters parameters, byte[] salt, int saltOff, int saltLen)
		{
			this.salt = new byte[saltLen];
			this.parameters = parameters;
			Array.Copy(salt, saltOff, this.salt, 0, saltLen);
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000FC6D4 File Offset: 0x000FB6D4
		public byte[] GetSalt()
		{
			return this.salt;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x000FC6DC File Offset: 0x000FB6DC
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001CED RID: 7405
		private byte[] salt;

		// Token: 0x04001CEE RID: 7406
		private ICipherParameters parameters;
	}
}
