using System;
using System.Collections;

namespace MailBee.ImapMail
{
	// Token: 0x0200017C RID: 380
	public class ImapBodyStructureCollection : CollectionBase
	{
		// Token: 0x06000E27 RID: 3623 RVA: 0x000357E8 File Offset: 0x000347E8
		internal ImapBodyStructureCollection()
		{
		}

		// Token: 0x17000462 RID: 1122
		public ImapBodyStructure this[int index]
		{
			get
			{
				return (ImapBodyStructure)base.List[index];
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00035803 File Offset: 0x00034803
		internal void a(ImapBodyStructure A_0)
		{
			base.List.Add(A_0);
		}
	}
}
