using System;
using Spire.Doc.Fields;

namespace Spire.Doc.Collections
{
	// Token: 0x02000530 RID: 1328
	public class CommentsCollection : CollectionEx
	{
		// Token: 0x0600456E RID: 17774 RVA: 0x00408B44 File Offset: 0x00407B44
		public CommentsCollection(Document doc) : base(doc, doc)
		{
		}

		// Token: 0x17000539 RID: 1337
		public Comment this[int index]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base.InnerList[index] as Comment;
			}
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x00408BA8 File Offset: 0x00407BA8
		public int Counts()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return base.InnerList.Count;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x00408BF0 File Offset: 0x00407BF0
		public void RemoveAt(int index)
		{
			for (;;)
			{
				Comment comment = base.InnerList[index] as Comment;
				base.InnerList.Remove(comment);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (comment.OwnerParagraph != null)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							comment.OwnerParagraph.Items.Remove(comment);
							if (true)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x00408C98 File Offset: 0x00407C98
		public void Clear()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (base.InnerList.Count <= 0)
					{
						num = 1;
						continue;
					}
					int index = base.InnerList.Count - 1;
					this.RemoveAt(index);
					if (true)
					{
					}
					num = 2;
					continue;
				}
				case 1:
					return;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				IL_3E:
				num = 0;
				continue;
				goto IL_3E;
			}
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x00408D34 File Offset: 0x00407D34
		internal void ᜀ(Comment A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x00408D7C File Offset: 0x00407D7C
		public void Remove(Comment comment)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.InnerList.Remove(comment);
			comment.OwnerParagraph.Items.Remove(comment);
		}
	}
}
