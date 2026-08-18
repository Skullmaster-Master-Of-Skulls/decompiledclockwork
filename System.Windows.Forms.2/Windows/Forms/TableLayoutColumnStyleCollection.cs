using System;
using System.Collections;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200039C RID: 924
	public class TableLayoutColumnStyleCollection : TableLayoutStyleCollection
	{
		// Token: 0x06003C4F RID: 15439 RVA: 0x001071B4 File Offset: 0x001053B4
		internal TableLayoutColumnStyleCollection(IArrangedElement Owner) : base(Owner)
		{
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x001071BD File Offset: 0x001053BD
		internal TableLayoutColumnStyleCollection() : base(null)
		{
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x001071C6 File Offset: 0x001053C6
		internal override string PropertyName
		{
			get
			{
				return PropertyNames.ColumnStyles;
			}
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x00106F1A File Offset: 0x0010511A
		public int Add(ColumnStyle columnStyle)
		{
			return ((IList)this).Add(columnStyle);
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x001071CD File Offset: 0x001053CD
		public void Insert(int index, ColumnStyle columnStyle)
		{
			((IList)this).Insert(index, columnStyle);
		}

		// Token: 0x17000EB2 RID: 3762
		public ColumnStyle this[int index]
		{
			get
			{
				return (ColumnStyle)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x001071E5 File Offset: 0x001053E5
		public void Remove(ColumnStyle columnStyle)
		{
			((IList)this).Remove(columnStyle);
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x001071EE File Offset: 0x001053EE
		public bool Contains(ColumnStyle columnStyle)
		{
			return ((IList)this).Contains(columnStyle);
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x001071F7 File Offset: 0x001053F7
		public int IndexOf(ColumnStyle columnStyle)
		{
			return ((IList)this).IndexOf(columnStyle);
		}
	}
}
