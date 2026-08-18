using System;
using System.Drawing;

namespace a.b
{
	// Token: 0x020003B6 RID: 950
	internal class iw
	{
		// Token: 0x0600224C RID: 8780 RVA: 0x0008C339 File Offset: 0x0008B339
		public iw(object A_0)
		{
			this.a(A_0);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0008C34F File Offset: 0x0008B34F
		public object b()
		{
			if (this.a)
			{
				return null;
			}
			return this.b;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0008C366 File Offset: 0x0008B366
		public void a(object A_0)
		{
			if (A_0 == null)
			{
				this.a = true;
				return;
			}
			this.a = false;
			this.b = (Color)A_0;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0008C386 File Offset: 0x0008B386
		public bool a()
		{
			return !this.a;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0008C391 File Offset: 0x0008B391
		public Color c()
		{
			return this.b;
		}

		// Token: 0x0400168C RID: 5772
		private bool a = true;

		// Token: 0x0400168D RID: 5773
		private Color b;
	}
}
