using System;
using System.Collections;

namespace MailBee.AntiSpam
{
	// Token: 0x0200012E RID: 302
	public class RblStatusCollection : CollectionBase
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x0002D24D File Offset: 0x0002C24D
		internal RblStatusCollection()
		{
		}

		// Token: 0x170002EF RID: 751
		public RblStatus this[int index]
		{
			get
			{
				return (RblStatus)base.List[index];
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002D268 File Offset: 0x0002C268
		internal void a(RblStatus A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0002D278 File Offset: 0x0002C278
		public bool IsInRbls
		{
			get
			{
				using (IEnumerator enumerator = base.List.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((RblStatus)enumerator.Current).IsIPAddressInRbl)
						{
							return true;
						}
					}
				}
				return false;
			}
		}
	}
}
