using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel
{
	// Token: 0x02000046 RID: 70
	internal sealed class ImmutableCollection<T> : Collection<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000B754 File Offset: 0x00009954
		public void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000B75D File Offset: 0x0000995D
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B765 File Offset: 0x00009965
		protected override void ClearItems()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			base.ClearItems();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B78F File Offset: 0x0000998F
		protected override void InsertItem(int index, T item)
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			base.InsertItem(index, item);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B7BB File Offset: 0x000099BB
		protected override void RemoveItem(int index)
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			base.RemoveItem(index);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B7E6 File Offset: 0x000099E6
		protected override void SetItem(int index, T item)
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			base.SetItem(index, item);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000B75D File Offset: 0x0000995D
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B75D File Offset: 0x0000995D
		bool IList.IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x04000295 RID: 661
		private bool isReadOnly;
	}
}
