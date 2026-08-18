using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000025 RID: 37
	internal sealed class SecurityToken
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000364A File Offset: 0x0000184A
		public SecurityToken(byte[] data)
		{
			this._data = (byte[])data.Clone();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003663 File Offset: 0x00001863
		internal byte[] GetDataNoClone()
		{
			return this._data;
		}

		// Token: 0x04000013 RID: 19
		private readonly byte[] _data;
	}
}
