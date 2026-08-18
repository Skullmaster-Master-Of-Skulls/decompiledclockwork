using System;
using System.Text;

namespace a.b
{
	// Token: 0x0200035D RID: 861
	internal sealed class c7 : iy
	{
		// Token: 0x06001F65 RID: 8037 RVA: 0x00085AC4 File Offset: 0x00084AC4
		public c7() : base(ie.b)
		{
			this.b();
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x00085ADE File Offset: 0x00084ADE
		public string a()
		{
			return this.a.ToString();
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00085AEB File Offset: 0x00084AEB
		public void b()
		{
			this.a.Remove(0, this.a.Length);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00085B05 File Offset: 0x00084B05
		protected override void ft(bp A_0)
		{
			this.a.Append(A_0.eu());
		}

		// Token: 0x0400143E RID: 5182
		private readonly StringBuilder a = new StringBuilder();
	}
}
