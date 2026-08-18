using System;
using System.Collections;
using System.Reflection;
using MailBee;

namespace a.b
{
	// Token: 0x02000276 RID: 630
	[DefaultMember("Item")]
	internal class d8 : CollectionBase
	{
		// Token: 0x0600168A RID: 5770 RVA: 0x0006749D File Offset: 0x0006649D
		public ew a(int A_0)
		{
			if (A_0 < 0 || A_0 > base.List.Count - 1)
			{
				return null;
			}
			return (ew)base.List[A_0];
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000674C6 File Offset: 0x000664C6
		public void b(int A_0, ew A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List[A_0] = A_1;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000674E0 File Offset: 0x000664E0
		public void a(ew A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(A_0);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000674FA File Offset: 0x000664FA
		public void a(int A_0, ew A_1)
		{
			if (A_0 < 0 || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Insert(A_0, A_1);
		}
	}
}
