using System;
using System.Collections;
using System.Collections.Generic;

namespace a.b
{
	// Token: 0x020002D5 RID: 725
	internal class ga : IEnumerable<he>
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x000713C0 File Offset: 0x000703C0
		public ga(e9 A_0, int A_1)
		{
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000713D6 File Offset: 0x000703D6
		public ga(e9 A_0)
		{
			this.a = A_0;
			this.b = -2;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000713ED File Offset: 0x000703ED
		public int b()
		{
			return this.b;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000713F5 File Offset: 0x000703F5
		public IEnumerator<he> GetEnumerator()
		{
			return this.a();
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000713FD File Offset: 0x000703FD
		IEnumerator IEnumerable.d()
		{
			return this.a();
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00071405 File Offset: 0x00070405
		public IEnumerator<he> a()
		{
			if (this.b == -2)
			{
				throw new InvalidOperationException("Can't read from a new stream before it has been written to");
			}
			return new cc(this.a, this.b);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00071430 File Offset: 0x00070430
		public void a(byte[] A_0)
		{
			int num = this.a.il();
			int num2 = (int)Math.Ceiling((double)A_0.Length / (double)num);
			d7 d = this.a.ik();
			int num3 = -2;
			int num4 = this.b;
			for (int i = 0; i < num2; i++)
			{
				int num5 = num4;
				if (num5 == -2)
				{
					num5 = this.a.ij();
					d.a(num5);
					num4 = -2;
					if (num3 != -2)
					{
						this.a.ii(num3, num5);
					}
					this.a.ii(num5, -2);
					if (this.b == -2)
					{
						this.b = num5;
					}
				}
				else
				{
					d.a(num5);
					num4 = this.a.ih(num5);
				}
				he he = this.a.@if(num5);
				int num6 = i * num;
				int a_ = Math.Min(A_0.Length - num6, num);
				he.b(A_0, num6, a_);
				num3 = num5;
			}
			int a_2 = num3;
			new ga(this.a, num4).a(d);
			this.a.ii(a_2, -2);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00071548 File Offset: 0x00070548
		public void c()
		{
			d7 a_ = this.a.ik();
			this.a(a_);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00071568 File Offset: 0x00070568
		private void a(d7 A_0)
		{
			int num = this.b;
			while (num != -2)
			{
				int a_ = num;
				A_0.a(a_);
				num = this.a.ih(a_);
				this.a.ii(a_, -1);
			}
			this.b = -2;
		}

		// Token: 0x0400126D RID: 4717
		private e9 a;

		// Token: 0x0400126E RID: 4718
		private int b;
	}
}
