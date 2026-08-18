using System;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000321 RID: 801
	internal class i7 : c2, IDisposable
	{
		// Token: 0x06001CD2 RID: 7378 RVA: 0x0007D940 File Offset: 0x0007C940
		public void Dispose()
		{
			this.a(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0007D94F File Offset: 0x0007C94F
		protected virtual void a(bool A_0)
		{
			if (A_0 && this.a != null)
			{
				this.a.Dispose();
				this.a = null;
			}
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0007D96E File Offset: 0x0007C96E
		public i7(Stream A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0007D980 File Offset: 0x0007C980
		public void pj(int A_0)
		{
			try
			{
				this.a.WriteByte((byte)A_0);
			}
			catch (IOException a_)
			{
				throw new RuntimeException(a_);
			}
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0007D9B4 File Offset: 0x0007C9B4
		public void pk(double A_0)
		{
			this.pm(BitConverter.DoubleToInt64Bits(A_0));
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0007D9C4 File Offset: 0x0007C9C4
		public void pl(int A_0)
		{
			int num = A_0 >> 24 & 255;
			int num2 = A_0 >> 16 & 255;
			int num3 = A_0 >> 8 & 255;
			int num4 = A_0 & 255;
			try
			{
				this.a.WriteByte((byte)num4);
				this.a.WriteByte((byte)num3);
				this.a.WriteByte((byte)num2);
				this.a.WriteByte((byte)num);
			}
			catch (IOException a_)
			{
				throw new RuntimeException(a_);
			}
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0007DA48 File Offset: 0x0007CA48
		public void pm(long A_0)
		{
			this.pl((int)A_0);
			this.pl((int)(A_0 >> 32));
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0007DA60 File Offset: 0x0007CA60
		public void pn(int A_0)
		{
			int num = A_0 >> 8 & 255;
			int num2 = A_0 & 255;
			try
			{
				this.a.WriteByte((byte)num2);
				this.a.WriteByte((byte)num);
			}
			catch (IOException a_)
			{
				throw new RuntimeException(a_);
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0007DAB4 File Offset: 0x0007CAB4
		public void po(byte[] A_0)
		{
			try
			{
				this.a.Write(A_0, 0, A_0.Length);
			}
			catch (IOException a_)
			{
				throw new RuntimeException(a_);
			}
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0007DAEC File Offset: 0x0007CAEC
		public void pp(byte[] A_0, int A_1, int A_2)
		{
			try
			{
				this.a.Write(A_0, A_1, A_2);
			}
			catch (IOException a_)
			{
				throw new RuntimeException(a_);
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0007DB20 File Offset: 0x0007CB20
		public void a()
		{
			this.a.Flush();
		}

		// Token: 0x0400136C RID: 4972
		private Stream a;
	}
}
