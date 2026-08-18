using System;
using System.Collections;
using System.Text;

namespace MailBee.ImapMail
{
	// Token: 0x02000194 RID: 404
	public class ImapNamespaceCollectionSet
	{
		// Token: 0x06000E77 RID: 3703 RVA: 0x00035D30 File Offset: 0x00034D30
		internal ImapNamespaceCollectionSet(ImapNamespaceCollection A_0, ImapNamespaceCollection A_1, ImapNamespaceCollection A_2, bool A_3)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00035D55 File Offset: 0x00034D55
		public ImapNamespaceCollection Personal
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00035D5D File Offset: 0x00034D5D
		public ImapNamespaceCollection OtherUser
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00035D65 File Offset: 0x00034D65
		public ImapNamespaceCollection Shared
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00035D6D File Offset: 0x00034D6D
		public bool IsValid
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00035D78 File Offset: 0x00034D78
		internal static ImapNamespaceCollectionSet a(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null || A_0.Count == 0)
			{
				return null;
			}
			bool a_ = A_0.Count >= 3;
			ImapNamespaceCollection imapNamespaceCollection = null;
			ImapNamespaceCollection imapNamespaceCollection2 = null;
			ImapNamespaceCollection imapNamespaceCollection3 = null;
			if (A_0.Count >= 1)
			{
				if (A_0[0] is ArrayList)
				{
					imapNamespaceCollection = ImapNamespaceCollection.a((ArrayList)A_0[0], A_1);
					if (imapNamespaceCollection == null)
					{
						a_ = false;
					}
				}
				else if (A_0[0] != null)
				{
					a_ = false;
				}
				if (A_0.Count >= 2)
				{
					if (A_0[1] is ArrayList)
					{
						imapNamespaceCollection2 = ImapNamespaceCollection.a((ArrayList)A_0[1], A_1);
						if (imapNamespaceCollection2 == null)
						{
							a_ = false;
						}
					}
					else if (A_0[1] != null)
					{
						a_ = false;
					}
					if (A_0.Count >= 2)
					{
						if (A_0[2] is ArrayList)
						{
							imapNamespaceCollection3 = ImapNamespaceCollection.a((ArrayList)A_0[2], A_1);
							if (imapNamespaceCollection3 == null)
							{
								a_ = false;
							}
						}
						else if (A_0[2] != null)
						{
							a_ = false;
						}
					}
					else
					{
						a_ = false;
					}
				}
				else
				{
					a_ = false;
				}
				return new ImapNamespaceCollectionSet(imapNamespaceCollection, imapNamespaceCollection2, imapNamespaceCollection3, a_);
			}
			return null;
		}

		// Token: 0x04000948 RID: 2376
		private ImapNamespaceCollection a;

		// Token: 0x04000949 RID: 2377
		private ImapNamespaceCollection b;

		// Token: 0x0400094A RID: 2378
		private ImapNamespaceCollection c;

		// Token: 0x0400094B RID: 2379
		private bool d;
	}
}
