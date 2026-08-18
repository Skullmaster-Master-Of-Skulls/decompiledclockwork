using System;
using System.Collections;

namespace MailBee.ImapMail
{
	// Token: 0x02000176 RID: 374
	public class FolderCollection : CollectionBase
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x00032F7C File Offset: 0x00031F7C
		internal FolderCollection()
		{
		}

		// Token: 0x1700041C RID: 1052
		public Folder this[int index]
		{
			get
			{
				return (Folder)base.List[index];
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00032F97 File Offset: 0x00031F97
		internal void a(Folder A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00032FA6 File Offset: 0x00031FA6
		public void Reverse()
		{
			base.InnerList.Reverse();
		}
	}
}
