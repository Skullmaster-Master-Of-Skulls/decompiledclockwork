using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000060 RID: 96
	internal struct DocumentHandle : IEquatable<DocumentHandle>
	{
		// Token: 0x060002AA RID: 682 RVA: 0x000072D5 File Offset: 0x000054D5
		private DocumentHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000072DE File Offset: 0x000054DE
		internal static DocumentHandle FromRowId(int rowId)
		{
			return new DocumentHandle(rowId);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000072E6 File Offset: 0x000054E6
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000072F1 File Offset: 0x000054F1
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000072F9 File Offset: 0x000054F9
		public static bool operator ==(DocumentHandle left, DocumentHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00007309 File Offset: 0x00005509
		public override bool Equals(object obj)
		{
			return obj is DocumentHandle && ((DocumentHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00007328 File Offset: 0x00005528
		public bool Equals(DocumentHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007338 File Offset: 0x00005538
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007353 File Offset: 0x00005553
		public static bool operator !=(DocumentHandle left, DocumentHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400034C RID: 844
		private readonly int _rowId;
	}
}
