using System;
using System.Collections.Generic;
using System.IO;

namespace a.b
{
	// Token: 0x020002EC RID: 748
	internal class gz : f2, af
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x000742D8 File Offset: 0x000732D8
		public gz(c3 A_0) : base(A_0)
		{
			this.a = A_0.b();
			this.b = null;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000742F4 File Offset: 0x000732F4
		public gz(c3 A_0, d3 A_1) : base(A_0, cf.a(A_1.fc(A_0.g(), -1)))
		{
			this.a = A_0.b();
			this.b = null;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00074324 File Offset: 0x00073324
		public new void a()
		{
			List<ed> list = new List<ed>(this.b.Count);
			for (int i = 0; i < this.b.Count; i++)
			{
				list.Add(this.b[i]);
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].d(j);
			}
			this.b = hv.a(this.a, list);
			for (int k = 0; k < list.Count; k++)
			{
				list[k].lk();
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000743B7 File Offset: 0x000733B7
		public override int ap()
		{
			if (this.b != null)
			{
				return this.b.Length;
			}
			return 0;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000743CC File Offset: 0x000733CC
		public void a3(Stream A_0)
		{
			if (this.b != null)
			{
				for (int i = 0; i < this.b.Length; i++)
				{
					this.b[i].a3(A_0);
				}
			}
		}

		// Token: 0x040012D7 RID: 4823
		private new y a;

		// Token: 0x040012D8 RID: 4824
		private new af[] b;
	}
}
