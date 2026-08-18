using System;
using System.Collections.Generic;
using System.IO;

namespace a.b
{
	// Token: 0x020002E8 RID: 744
	internal class k : f2
	{
		// Token: 0x06001A4D RID: 6733 RVA: 0x00073EE6 File Offset: 0x00072EE6
		public k(c3 A_0) : base(A_0)
		{
			this.a = A_0.b();
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00073EFB File Offset: 0x00072EFB
		public k(c3 A_0, h0 A_1) : base(A_0, k.a(new ga(A_1, A_0.g()).GetEnumerator(), A_0.b()))
		{
			this.a = A_0.b();
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00073F2C File Offset: 0x00072F2C
		private new static List<ed> a(IEnumerator<he> A_0, y A_1)
		{
			List<ed> result;
			try
			{
				List<ed> list = new List<ed>();
				while (A_0.MoveNext())
				{
					he he = A_0.Current;
					byte[] array;
					if (he.k() && he.e() == 0 && he.a().Length == A_1.f())
					{
						array = he.a();
					}
					else
					{
						array = new byte[A_1.f()];
						int a_ = array.Length;
						if (he.d() < A_1.f())
						{
							a_ = he.d();
						}
						he.c(array, 0, a_);
					}
					cf.a(array, list);
				}
				result = list;
			}
			catch (IOException ex)
			{
				throw ex;
			}
			return result;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00073FC8 File Offset: 0x00072FC8
		public override int ap()
		{
			int num = this.b.Count * 128;
			return (int)Math.Ceiling(1.0 * (double)num / (double)this.a.f());
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00074008 File Offset: 0x00073008
		public new void a(ga A_0)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				foreach (ed ed in this.b)
				{
					if (ed != null)
					{
						ed.a(memoryStream);
					}
				}
				A_0.a(memoryStream.ToArray());
				if (this.c() != A_0.b())
				{
					this.jm(A_0.b());
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x040012CD RID: 4813
		private new y a;
	}
}
