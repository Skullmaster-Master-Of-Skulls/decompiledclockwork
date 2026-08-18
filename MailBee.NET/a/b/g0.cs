using System;
using System.Collections;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000330 RID: 816
	internal class g0 : IEnumerator
	{
		// Token: 0x06001D91 RID: 7569 RVA: 0x0007F641 File Offset: 0x0007E641
		internal g0(bj A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0007F650 File Offset: 0x0007E650
		public bool MoveNext()
		{
			d6 d = this.a.b(1);
			if (d.Count > 0)
			{
				if (d.a(0) is fo)
				{
					this.b = new PstContact((fo)d.a(0));
				}
				else if (d.a(0) is h5)
				{
					this.b = new PstRss((h5)d.a(0));
				}
				else if (d.a(0) is cv)
				{
					this.b = new PstTask((cv)d.a(0));
				}
				else if (d.a(0) is by)
				{
					this.b = new PstAppointment((by)d.a(0));
				}
				else if (d.a(0) is el)
				{
					this.b = new PstDistList((el)d.a(0));
				}
				else if (d.a(0) is co)
				{
					this.b = new PstMessage((co)d.a(0));
				}
				else
				{
					this.b = new PstItem(d.a(0));
				}
				return true;
			}
			this.b = null;
			return false;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0007F785 File Offset: 0x0007E785
		public void Reset()
		{
			this.a.a(0);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0007F793 File Offset: 0x0007E793
		public object get_Current()
		{
			return this.b;
		}

		// Token: 0x04001388 RID: 5000
		private bj a;

		// Token: 0x04001389 RID: 5001
		private PstItem b;
	}
}
