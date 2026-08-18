using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x0200003C RID: 60
	public class CommentsCollection : XlsCommentsCollection
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00025378 File Offset: 0x00024378
		internal CommentsCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00025390 File Offset: 0x00024390
		public ExcelComment AddComment(CellRange range)
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
			return new ExcelComment(base.AddComment(range));
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000253D8 File Offset: 0x000243D8
		public new ExcelComment AddComment(int rowIndex, int columnIndex)
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
			return new ExcelComment(base.AddComment(rowIndex, columnIndex));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00025420 File Offset: 0x00024420
		public void Remove(ExcelComment comment)
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
			base.Remove(comment.Wrapped);
		}

		// Token: 0x17000155 RID: 341
		public ExcelComment this[int index]
		{
			get
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
				return new ExcelComment(base.InnerList[index]);
			}
		}

		// Token: 0x17000156 RID: 342
		public ExcelComment this[int Row, int Column]
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
				return new ExcelComment(base[Row, Column]);
			}
		}
	}
}
