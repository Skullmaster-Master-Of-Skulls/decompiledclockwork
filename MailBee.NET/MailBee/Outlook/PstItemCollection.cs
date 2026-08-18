using System;
using System.Collections;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x020005B7 RID: 1463
	public class PstItemCollection : CollectionBase
	{
		// Token: 0x06003118 RID: 12568 RVA: 0x000E6663 File Offset: 0x000E5663
		internal PstItemCollection(bj A_0)
		{
			this.a = A_0;
		}

		// Token: 0x17000669 RID: 1641
		public PstItem this[int index]
		{
			get
			{
				this.a.a(index);
				d6 d = this.a.b(1);
				if (d.Count <= 0)
				{
					return null;
				}
				if (d.a(0) is fo)
				{
					return new PstContact((fo)d.a(0));
				}
				if (d.a(0) is h5)
				{
					return new PstRss((h5)d.a(0));
				}
				if (d.a(0) is cv)
				{
					return new PstTask((cv)d.a(0));
				}
				if (d.a(0) is by)
				{
					return new PstAppointment((by)d.a(0));
				}
				if (d.a(0) is el)
				{
					return new PstDistList((el)d.a(0));
				}
				if (d.a(0) is co)
				{
					return new PstMessage((co)d.a(0));
				}
				return new PstItem(d.a(0));
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000E6774 File Offset: 0x000E5774
		public new int Count
		{
			get
			{
				return this.a.i();
			}
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000E6781 File Offset: 0x000E5781
		public new IEnumerator GetEnumerator()
		{
			return new g0(this.a);
		}

		// Token: 0x04002049 RID: 8265
		private bj a;
	}
}
