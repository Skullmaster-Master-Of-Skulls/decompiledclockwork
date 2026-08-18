using System;
using System.Collections;
using System.Text;

namespace MailBee.ImapMail
{
	// Token: 0x02000193 RID: 403
	public class ImapNamespaceCollection : CollectionBase
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x00035C0C File Offset: 0x00034C0C
		internal ImapNamespaceCollection()
		{
		}

		// Token: 0x1700047E RID: 1150
		public ImapNamespace this[int index]
		{
			get
			{
				return (ImapNamespace)base.List[index];
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00035C27 File Offset: 0x00034C27
		internal void a(ImapNamespace A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00035C38 File Offset: 0x00034C38
		public bool IsValid
		{
			get
			{
				foreach (object obj in base.List)
				{
					if (obj != null && !((ImapNamespace)obj).IsValid)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00035C9C File Offset: 0x00034C9C
		internal static ImapNamespaceCollection a(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				return null;
			}
			ImapNamespaceCollection imapNamespaceCollection = new ImapNamespaceCollection();
			foreach (object obj in A_0)
			{
				if (obj == null)
				{
					imapNamespaceCollection.a(null);
				}
				else
				{
					if (!(obj is ArrayList))
					{
						return null;
					}
					ImapNamespace imapNamespace = ImapNamespace.a((ArrayList)obj, A_1);
					if (imapNamespace == null)
					{
						return null;
					}
					imapNamespaceCollection.a(imapNamespace);
				}
			}
			return imapNamespaceCollection;
		}
	}
}
