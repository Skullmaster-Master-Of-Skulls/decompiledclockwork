using System;
using System.Collections;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200039D RID: 925
	public class TableLayoutRowStyleCollection : TableLayoutStyleCollection
	{
		// Token: 0x06003C59 RID: 15449 RVA: 0x001071B4 File Offset: 0x001053B4
		internal TableLayoutRowStyleCollection(IArrangedElement Owner) : base(Owner)
		{
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x001071BD File Offset: 0x001053BD
		internal TableLayoutRowStyleCollection() : base(null)
		{
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x00107200 File Offset: 0x00105400
		internal override string PropertyName
		{
			get
			{
				return PropertyNames.RowStyles;
			}
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x00106F1A File Offset: 0x0010511A
		public int Add(RowStyle rowStyle)
		{
			return ((IList)this).Add(rowStyle);
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x001071CD File Offset: 0x001053CD
		public void Insert(int index, RowStyle rowStyle)
		{
			((IList)this).Insert(index, rowStyle);
		}

		// Token: 0x17000EB4 RID: 3764
		public RowStyle this[int index]
		{
			get
			{
				return (RowStyle)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x001071E5 File Offset: 0x001053E5
		public void Remove(RowStyle rowStyle)
		{
			((IList)this).Remove(rowStyle);
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x001071EE File Offset: 0x001053EE
		public bool Contains(RowStyle rowStyle)
		{
			return ((IList)this).Contains(rowStyle);
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x001071F7 File Offset: 0x001053F7
		public int IndexOf(RowStyle rowStyle)
		{
			return ((IList)this).IndexOf(rowStyle);
		}
	}
}
