using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x020004C6 RID: 1222
	internal abstract class bk : bo, av
	{
		// Token: 0x0600298E RID: 10638 RVA: 0x000C14EA File Offset: 0x000C04EA
		public bk()
		{
		}

		// Token: 0x0600298F RID: 10639
		public abstract bool b();

		// Token: 0x06002990 RID: 10640
		public new abstract void c(DataTransferEventArgs A_0);

		// Token: 0x06002991 RID: 10641
		public abstract bool d();

		// Token: 0x06002992 RID: 10642
		public new abstract void e(DataTransferEventArgs A_0);

		// Token: 0x06002993 RID: 10643
		public abstract bool f();

		// Token: 0x06002994 RID: 10644
		public abstract void g(DataTransferEventArgs A_0);

		// Token: 0x06002995 RID: 10645
		public abstract bool h();

		// Token: 0x06002996 RID: 10646
		public new abstract void i(DataTransferEventArgs A_0);

		// Token: 0x06002997 RID: 10647 RVA: 0x000C14F2 File Offset: 0x000C04F2
		public virtual Socket lv()
		{
			return ((be)this.p).a7();
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000C1504 File Offset: 0x000C0504
		public virtual Stream ba()
		{
			return ((be)this.p).a3();
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000C1516 File Offset: 0x000C0516
		public virtual int lw()
		{
			return ((be)this.p).a4();
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000C1528 File Offset: 0x000C0528
		public virtual void lm(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			if (this.e && ((av)this).f() && !this.c)
			{
				byte[] array = new byte[A_2];
				Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
				((be)A_3).c(array);
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000C1570 File Offset: 0x000C0570
		public virtual void ln(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			if (this.e && ((av)this).h() && !this.c)
			{
				byte[] array = new byte[A_2];
				Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
				((be)A_3).a(array);
			}
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000C15B8 File Offset: 0x000C05B8
		public virtual Task mw(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			if (this.e && ((av)this).f() && !this.c)
			{
				byte[] array = new byte[A_2];
				Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
				((be)A_3).c(array);
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000C1604 File Offset: 0x000C0604
		public virtual Task mx(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			if (this.e && ((av)this).h() && !this.c)
			{
				byte[] array = new byte[A_2];
				Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
				((be)A_3).a(array);
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000C1650 File Offset: 0x000C0650
		public void d(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			if (this.e && ((av)this).b() && !this.c)
			{
				byte[] array = new byte[A_2];
				Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
				((be)A_3).b(array);
			}
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000C1698 File Offset: 0x000C0698
		public void f(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			if (this.e && ((av)this).d() && !this.c)
			{
				byte[] array = new byte[A_2];
				Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
				((be)A_3).d(array);
			}
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000C16DD File Offset: 0x000C06DD
		public new Task e(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			this.d(A_0, A_1, A_2, A_3);
			return Task.FromResult<int>(0);
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000C16F0 File Offset: 0x000C06F0
		public new Task c(byte[] A_0, int A_1, int A_2, bc A_3)
		{
			this.f(A_0, A_1, A_2, A_3);
			return Task.FromResult<int>(0);
		}
	}
}
