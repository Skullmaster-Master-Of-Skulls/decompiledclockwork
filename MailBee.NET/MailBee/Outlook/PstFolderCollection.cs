using System;
using System.Collections;

namespace MailBee.Outlook
{
	// Token: 0x020005B5 RID: 1461
	public class PstFolderCollection : CollectionBase
	{
		// Token: 0x06003114 RID: 12564 RVA: 0x000E6614 File Offset: 0x000E5614
		internal PstFolderCollection()
		{
		}

		// Token: 0x17000668 RID: 1640
		public PstFolder this[int index]
		{
			get
			{
				return (PstFolder)base.List[index];
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				base.List[index] = value;
			}
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000E6649 File Offset: 0x000E5649
		internal void a(PstFolder A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(A_0);
		}
	}
}
