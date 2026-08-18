using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000B2 RID: 178
	public class BerGenerator : Asn1Generator
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x0001CE3C File Offset: 0x0001BE3C
		protected BerGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001CE45 File Offset: 0x0001BE45
		public BerGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream)
		{
			this._tagged = true;
			this._isExplicit = isExplicit;
			this._tagNo = tagNo;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001CE63 File Offset: 0x0001BE63
		public override void AddObject(Asn1Encodable obj)
		{
			new BerOutputStream(base.Out).WriteObject(obj);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001CE76 File Offset: 0x0001BE76
		public override Stream GetRawOutputStream()
		{
			return base.Out;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001CE7E File Offset: 0x0001BE7E
		public override void Close()
		{
			this.WriteBerEnd();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001CE86 File Offset: 0x0001BE86
		private void WriteHdr(int tag)
		{
			base.Out.WriteByte((byte)tag);
			base.Out.WriteByte(128);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001CEA8 File Offset: 0x0001BEA8
		protected void WriteBerHeader(int tag)
		{
			if (!this._tagged)
			{
				this.WriteHdr(tag);
				return;
			}
			int num = this._tagNo | 128;
			if (this._isExplicit)
			{
				this.WriteHdr(num | 32);
				this.WriteHdr(tag);
				return;
			}
			if ((tag & 32) != 0)
			{
				this.WriteHdr(num | 32);
				return;
			}
			this.WriteHdr(num);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001CF04 File Offset: 0x0001BF04
		protected void WriteBerBody(Stream contentStream)
		{
			Streams.PipeAll(contentStream, base.Out);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001CF14 File Offset: 0x0001BF14
		protected void WriteBerEnd()
		{
			base.Out.WriteByte(0);
			base.Out.WriteByte(0);
			if (this._tagged && this._isExplicit)
			{
				base.Out.WriteByte(0);
				base.Out.WriteByte(0);
			}
		}

		// Token: 0x040002B9 RID: 697
		private bool _tagged;

		// Token: 0x040002BA RID: 698
		private bool _isExplicit;

		// Token: 0x040002BB RID: 699
		private int _tagNo;
	}
}
