using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000048 RID: 72
	public abstract class Asn1Generator
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000A445 File Offset: 0x00009445
		protected Asn1Generator(Stream outStream)
		{
			this._out = outStream;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000A454 File Offset: 0x00009454
		protected Stream Out
		{
			get
			{
				return this._out;
			}
		}

		// Token: 0x060001E8 RID: 488
		public abstract void AddObject(Asn1Encodable obj);

		// Token: 0x060001E9 RID: 489
		public abstract Stream GetRawOutputStream();

		// Token: 0x060001EA RID: 490
		public abstract void Close();

		// Token: 0x040000D9 RID: 217
		private Stream _out;
	}
}
