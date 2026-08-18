using System;
using System.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020003E1 RID: 993
	public class CmsProcessableByteArray : CmsProcessable
	{
		// Token: 0x0600228E RID: 8846 RVA: 0x000D66FC File Offset: 0x000D56FC
		public CmsProcessableByteArray(byte[] bytes)
		{
			this.bytes = bytes;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000D670B File Offset: 0x000D570B
		public virtual Stream Read()
		{
			return new MemoryStream(this.bytes, false);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000D6719 File Offset: 0x000D5719
		public virtual void Write(Stream zOut)
		{
			zOut.Write(this.bytes, 0, this.bytes.Length);
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000D6730 File Offset: 0x000D5730
		public virtual object GetContent()
		{
			return this.bytes.Clone();
		}

		// Token: 0x040017B3 RID: 6067
		private readonly byte[] bytes;
	}
}
