using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x02000535 RID: 1333
	public class CellCollection : DocumentObjectCollection
	{
		// Token: 0x1700053D RID: 1341
		public TableCell this[int index]
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
				return base.InnerList[index] as TableCell;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x00409908 File Offset: 0x00408908
		protected override Type[] TypesOfElement
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
				return CellCollection.ᜀ;
			}
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x00409948 File Offset: 0x00408948
		public CellCollection(TableRow owner) : base(owner.Document, owner)
		{
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x00409964 File Offset: 0x00408964
		public int Add(TableCell cell)
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
			this.ᜀ();
			return base.Add(cell);
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x004099AC File Offset: 0x004089AC
		public void Insert(int index, TableCell cell)
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
			this.ᜀ();
			base.Insert(index, cell);
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x004099F8 File Offset: 0x004089F8
		public int IndexOf(TableCell cell)
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
			return base.IndexOf(cell);
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x00409A3C File Offset: 0x00408A3C
		public void Remove(TableCell cell)
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
			this.ᜀ();
			this.ᜀ(cell.GetCellIndex());
			base.Remove(cell);
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x00409A90 File Offset: 0x00408A90
		public new void RemoveAt(int index)
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
			this.ᜀ();
			this.ᜀ(index);
			base.RemoveAt(index);
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x00409AE0 File Offset: 0x00408AE0
		private new void ᜀ(int A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					TableCell tableCell = this[A_0];
					Document document = tableCell.Document;
					int num = 0;
					int count = document.Bookmarks.Count;
					int num2 = 9;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							int cellIndex;
							Bookmark bookmark;
							if (cellIndex >= bookmark.BookmarkStart.ColumnFirst)
							{
								num2 = 6;
								continue;
							}
							goto IL_B5;
						}
						case 1:
						{
							int cellIndex;
							Bookmark bookmark;
							if (cellIndex < bookmark.BookmarkStart.ColumnFirst)
							{
								num2 = 2;
								continue;
							}
							goto IL_181;
						}
						case 2:
						{
							Bookmark bookmark;
							bookmark.BookmarkStart.ColumnFirst--;
							bookmark.BookmarkStart.ColumnLast--;
							num2 = 11;
							continue;
						}
						case 3:
							goto IL_181;
						case 4:
						{
							Bookmark bookmark;
							document.Bookmarks.Remove(bookmark);
							num2 = 3;
							continue;
						}
						case 5:
						{
							if (num >= count)
							{
								num2 = 10;
								continue;
							}
							if (true)
							{
							}
							Bookmark bookmark = document.Bookmarks[num];
							int cellIndex = tableCell.GetCellIndex();
							num2 = 0;
							continue;
						}
						case 6:
							num2 = 8;
							continue;
						case 7:
							goto IL_10F;
						case 8:
						{
							int cellIndex;
							Bookmark bookmark;
							if (cellIndex <= bookmark.BookmarkStart.ColumnLast)
							{
								num2 = 4;
								continue;
							}
							goto IL_B5;
						}
						case 9:
							goto IL_10F;
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_181;
							default:
								goto IL_1C7;
							}
							break;
						case 11:
							goto IL_181;
						}
						break;
						IL_B5:
						num2 = 1;
						continue;
						IL_10F:
						num2 = 5;
						continue;
						IL_181:
						num++;
						num2 = 7;
					}
				}
				IL_1C7:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x00409CBC File Offset: 0x00408CBC
		protected override string GetTagItemName()
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("୧ཀྵkɭ", a_);
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x00409D10 File Offset: 0x00408D10
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
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
			return new TableCell(base.Document);
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x00409D58 File Offset: 0x00408D58
		private new void ᜀ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					(base.Owner as TableRow).ᜊ();
					num = 3;
					continue;
				case 2:
					num = 5;
					continue;
				case 3:
					return;
				case 4:
					num = 6;
					continue;
				case 5:
					if (base.Owner.Document.ᜇ)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3F;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 6:
					if (base.Owner is TableRow)
					{
						num = 2;
						continue;
					}
					return;
				}
				goto IL_34;
				IL_3F:
				num = 4;
				continue;
				IL_34:
				if (base.Owner != null)
				{
					goto IL_3F;
				}
				break;
			}
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x00409E3C File Offset: 0x00408E3C
		// Note: this type is marked as 'beforefieldinit'.
		static CellCollection()
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
			CellCollection.ᜀ = new Type[]
			{
				typeof(TableCell)
			};
		}

		// Token: 0x0400366D RID: 13933
		private string \u2593\u0099\u0090\u0080;

		// Token: 0x0400366E RID: 13934
		private float \u2609\u0085\u0080\u0080;

		// Token: 0x0400366F RID: 13935
		private new static readonly Type[] ᜀ;
	}
}
