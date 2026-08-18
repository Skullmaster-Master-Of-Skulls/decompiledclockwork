using System;

namespace Spire.Doc.Collections
{
	// Token: 0x0200052C RID: 1324
	public class BookmarkCollection : CollectionEx
	{
		// Token: 0x17000533 RID: 1331
		public Bookmark this[string name]
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
				return this.FindByName(name);
			}
		}

		// Token: 0x17000534 RID: 1332
		public Bookmark this[int index]
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
				return base.InnerList[index] as Bookmark;
			}
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x00407868 File Offset: 0x00406868
		internal BookmarkCollection(Document A_0) : base(A_0, A_0)
		{
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x00407880 File Offset: 0x00406880
		public Bookmark FindByName(string name)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5B:
				goto IL_A0;
			default:
				if (false)
				{
				}
				goto IL_46;
			}
			int num;
			int num2;
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_CC;
				case 1:
				{
					Bookmark bookmark;
					if (bookmark.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
					{
						num = 2;
						continue;
					}
					num2++;
					num = 3;
					continue;
				}
				case 2:
				{
					Bookmark bookmark;
					return bookmark;
				}
				case 3:
					goto IL_69;
				case 4:
				{
					if (num2 >= base.InnerList.Count)
					{
						num = 0;
						continue;
					}
					Bookmark bookmark = base.InnerList[num2] as Bookmark;
					num = 1;
					continue;
				}
				case 5:
					goto IL_5B;
				}
				goto IL_46;
			}
			IL_69:
			goto IL_A0;
			IL_CC:
			return null;
			IL_46:
			name.Replace('-', '_');
			num2 = 0;
			num = 5;
			goto IL_1E;
			IL_A0:
			if (true)
			{
			}
			num = 4;
			goto IL_1E;
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x0040795C File Offset: 0x0040695C
		public void RemoveAt(int index)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			Bookmark bookmark = base.InnerList[index] as Bookmark;
			this.Remove(bookmark);
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x004079B0 File Offset: 0x004069B0
		public void Remove(Bookmark bookmark)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			for (;;)
			{
				base.InnerList.Remove(bookmark);
				BookmarkStart bookmarkStart = bookmark.BookmarkStart;
				BookmarkEnd bookmarkEnd = bookmark.BookmarkEnd;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (bookmarkStart != null)
						{
							num = 5;
							continue;
						}
						goto IL_85;
					case 1:
						if (true)
						{
						}
						if (bookmarkEnd != null)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						bookmarkEnd.RemoveSelf();
						num = 3;
						continue;
					case 3:
						return;
					case 4:
						goto IL_85;
					case 5:
						bookmarkStart.RemoveSelf();
						num = 4;
						continue;
					}
					break;
					IL_85:
					num = 1;
				}
			}
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x00407A74 File Offset: 0x00406A74
		public void Clear()
		{
			for (;;)
			{
				IL_00:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						if (base.InnerList.Count <= 0)
						{
							num = 0;
							continue;
						}
						int index = base.InnerList.Count - 1;
						this.RemoveAt(index);
						num = 3;
						continue;
					}
					}
					IL_22:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					goto IL_22;
				}
			}
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x00407B10 File Offset: 0x00406B10
		internal void ᜀ(Bookmark A_0)
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

		// Token: 0x06004557 RID: 17751 RVA: 0x00407B58 File Offset: 0x00406B58
		internal void ᜀ(BookmarkStart A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				Bookmark bookmark = this[A_0.Name];
				if (bookmark == null)
				{
					bookmark = new Bookmark(A_0);
					this.ᜀ(bookmark);
					return;
				}
				if (true)
				{
				}
				break;
			}
			}
			A_0.ᜀ(A_0.Name + Guid.NewGuid().ToString());
			A_0.RemoveSelf();
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x00407BE0 File Offset: 0x00406BE0
		internal void ᜀ(BookmarkEnd A_0)
		{
			for (;;)
			{
				Bookmark bookmark = this[A_0.Name];
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9A;
					case 1:
						goto IL_55;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
						{
							if (false)
							{
							}
							BookmarkEnd bookmarkEnd = bookmark.BookmarkEnd;
							num = 3;
							continue;
						}
						}
						break;
					case 3:
					{
						BookmarkEnd bookmarkEnd;
						if (bookmarkEnd != null)
						{
							num = 0;
							continue;
						}
						bookmark.ᜀ(A_0);
						num = 1;
						continue;
					}
					case 4:
						if (true)
						{
						}
						if (bookmark != null)
						{
							num = 2;
							continue;
						}
						return;
					}
					break;
				}
			}
			IL_55:
			return;
			IL_9A:
			A_0.RemoveSelf();
		}
	}
}
