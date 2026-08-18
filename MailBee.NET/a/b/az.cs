using System;
using System.IO;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002C8 RID: 712
	internal class az : e5, gc
	{
		// Token: 0x06001891 RID: 6289 RVA: 0x0006EF29 File Offset: 0x0006DF29
		protected az()
		{
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0006EF31 File Offset: 0x0006DF31
		public new bool c()
		{
			return this.e;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0006EF3C File Offset: 0x0006DF3C
		public az(h4 A_0)
		{
			if (!(A_0 is hz))
			{
				throw new MailBeeOutlookMsgParsingException(Resources.Instance.ErrorDesc_OleDocCannotOpenInternalDocumentStorage, 1200);
			}
			hz hz = (hz)A_0;
			DirectoryNode directoryNode = (DirectoryNode)A_0.t();
			if (hz.a() != null)
			{
				this.f = new bo(A_0);
			}
			else if (directoryNode.FileSystem != null)
			{
				this.f = new bo(A_0);
			}
			else
			{
				if (directoryNode.NFileSystem == null)
				{
					throw new MailBeeOutlookMsgParsingException("No FileSystem bound on the parent, can't read contents", 1200);
				}
				this.f = new l(A_0);
			}
			if (this.f.c())
			{
				this.e = true;
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0006EFE3 File Offset: 0x0006DFE3
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.f.Seek(offset, origin);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0006EFF2 File Offset: 0x0006DFF2
		public override long get_Length()
		{
			return this.f.Length;
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0006EFFF File Offset: 0x0006DFFF
		public override long get_Position()
		{
			return this.f.Position;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0006F00C File Offset: 0x0006E00C
		public override void set_Position(long value)
		{
			this.f.Position = value;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0006F01A File Offset: 0x0006E01A
		public az(eg A_0)
		{
			this.f = new bo(A_0);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0006F02E File Offset: 0x0006E02E
		public az(hw A_0)
		{
			this.f = new l(A_0);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0006F042 File Offset: 0x0006E042
		public override int aq()
		{
			return this.f.aq();
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0006F04F File Offset: 0x0006E04F
		public override void Close()
		{
			this.f.Close();
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0006F05C File Offset: 0x0006E05C
		public override void ar(int A_0)
		{
			this.f.ar(A_0);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0006F06A File Offset: 0x0006E06A
		public override bool cc()
		{
			return true;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0006F06D File Offset: 0x0006E06D
		public override int @as()
		{
			return this.f.@as();
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0006F07A File Offset: 0x0006E07A
		public new virtual int a(byte[] A_0)
		{
			return this.Read(A_0, 0, A_0.Length);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0006F087 File Offset: 0x0006E087
		public override int Read(byte[] b, int off, int len)
		{
			return this.f.Read(b, off, len);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0006F097 File Offset: 0x0006E097
		public override void at()
		{
			this.f.at();
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0006F0A4 File Offset: 0x0006E0A4
		public virtual long au(long A_0)
		{
			return this.f.au(A_0);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0006F0B2 File Offset: 0x0006E0B2
		public override int ReadByte()
		{
			return this.f.ReadByte();
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0006F0BF File Offset: 0x0006E0BF
		public virtual double aw()
		{
			return this.f.aw();
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0006F0CC File Offset: 0x0006E0CC
		public virtual short az()
		{
			return (short)this.a1();
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0006F0D5 File Offset: 0x0006E0D5
		public virtual void ay(byte[] A_0)
		{
			this.av(A_0, 0, A_0.Length);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0006F0E2 File Offset: 0x0006E0E2
		public virtual void av(byte[] A_0, int A_1, int A_2)
		{
			this.f.av(A_0, A_1, A_2);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0006F0F2 File Offset: 0x0006E0F2
		public virtual long ax()
		{
			return this.f.ax();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0006F0FF File Offset: 0x0006E0FF
		public virtual int a0()
		{
			return this.f.a0();
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0006F10C File Offset: 0x0006E10C
		public virtual int a1()
		{
			return this.f.a1();
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0006F119 File Offset: 0x0006E119
		public virtual int a2()
		{
			return this.f.a2();
		}

		// Token: 0x0400123B RID: 4667
		protected new static int a = -1;

		// Token: 0x0400123C RID: 4668
		protected new static int b = 2;

		// Token: 0x0400123D RID: 4669
		protected new static int c = 4;

		// Token: 0x0400123E RID: 4670
		protected new static int d = 8;

		// Token: 0x0400123F RID: 4671
		protected bool e;

		// Token: 0x04001240 RID: 4672
		private az f;
	}
}
