using System;
using System.IO;

namespace a.b
{
	// Token: 0x020002E1 RID: 737
	internal class hc : cl
	{
		// Token: 0x06001A07 RID: 6663 RVA: 0x0007335E File Offset: 0x0007235E
		public hc(FileInfo A_0)
		{
			if (A_0.Exists)
			{
				throw new FileNotFoundException(A_0.FullName);
			}
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0007337C File Offset: 0x0007237C
		public hc(FileStream A_0)
		{
			A_0.Position = 0L;
			byte[] array = new byte[A_0.Length];
			A_0.Read(array, 0, (int)A_0.Length);
			MemoryStream memoryStream = new MemoryStream(array, 0, array.Length);
			this.a = memoryStream;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000733C6 File Offset: 0x000723C6
		public void a()
		{
			this.a(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000733D5 File Offset: 0x000723D5
		private void a(bool A_0)
		{
			if (A_0 && this.a != null)
			{
				this.a.Dispose();
				this.a = null;
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000733F4 File Offset: 0x000723F4
		~hc()
		{
			this.a(false);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00073424 File Offset: 0x00072424
		public override he m3(int A_0, long A_1)
		{
			if (A_1 >= this.m6())
			{
				throw new ArgumentException("Position " + A_1 + " past the end of the file");
			}
			this.a.Position = A_1;
			he he = he.a(A_0);
			if (g9.a(this.a, he.a()) == -1)
			{
				throw new ArgumentException("Position " + A_1 + " past the end of the file");
			}
			he.b(0);
			return he;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0007349F File Offset: 0x0007249F
		public override void m4(he A_0, long A_1)
		{
			this.a.Write(A_0.a(), (int)A_1, A_0.f());
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000734BC File Offset: 0x000724BC
		public override void m5(Stream A_0)
		{
			byte[] array = new byte[A_0.Length];
			A_0.Write(array, 0, array.Length);
			this.a.Write(array, 0, array.Length);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000734F1 File Offset: 0x000724F1
		public override long m6()
		{
			return this.a.Length;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000734FE File Offset: 0x000724FE
		public override void m7()
		{
			this.a.Close();
		}

		// Token: 0x040012A3 RID: 4771
		private Stream a;
	}
}
