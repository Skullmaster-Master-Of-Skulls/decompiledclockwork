using System;

namespace System.Security.Authentication.ExtendedProtection
{
	// Token: 0x02000448 RID: 1096
	public class TokenBinding
	{
		// Token: 0x060028A5 RID: 10405 RVA: 0x000BAAB8 File Offset: 0x000B8CB8
		internal TokenBinding(TokenBindingType bindingType, byte[] rawData)
		{
			this.BindingType = bindingType;
			this._rawTokenBindingId = rawData;
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000BAACE File Offset: 0x000B8CCE
		public byte[] GetRawTokenBindingId()
		{
			if (this._rawTokenBindingId == null)
			{
				return null;
			}
			return (byte[])this._rawTokenBindingId.Clone();
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000BAAEA File Offset: 0x000B8CEA
		// (set) Token: 0x060028A8 RID: 10408 RVA: 0x000BAAF2 File Offset: 0x000B8CF2
		public TokenBindingType BindingType { get; private set; }

		// Token: 0x04002275 RID: 8821
		private byte[] _rawTokenBindingId;
	}
}
