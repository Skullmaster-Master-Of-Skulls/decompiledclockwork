using System;

namespace System.Web.Caching
{
	// Token: 0x0200089A RID: 2202
	internal struct ExpiresEntryRef
	{
		// Token: 0x0600673A RID: 26426 RVA: 0x0016D510 File Offset: 0x0016B710
		internal ExpiresEntryRef(int pageIndex, int entryIndex)
		{
			this._ref = (uint)(pageIndex << 8 | (entryIndex & 255));
		}

		// Token: 0x0600673B RID: 26427 RVA: 0x0016D523 File Offset: 0x0016B723
		public override bool Equals(object value)
		{
			return value is ExpiresEntryRef && this._ref == ((ExpiresEntryRef)value)._ref;
		}

		// Token: 0x0600673C RID: 26428 RVA: 0x0016D542 File Offset: 0x0016B742
		public static bool operator !=(ExpiresEntryRef r1, ExpiresEntryRef r2)
		{
			return r1._ref != r2._ref;
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x0016D555 File Offset: 0x0016B755
		public static bool operator ==(ExpiresEntryRef r1, ExpiresEntryRef r2)
		{
			return r1._ref == r2._ref;
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x0016D565 File Offset: 0x0016B765
		public override int GetHashCode()
		{
			return (int)this._ref;
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x0600673F RID: 26431 RVA: 0x0016D570 File Offset: 0x0016B770
		internal int PageIndex
		{
			get
			{
				return (int)(this._ref >> 8);
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x06006740 RID: 26432 RVA: 0x0016D588 File Offset: 0x0016B788
		internal int Index
		{
			get
			{
				return (int)(this._ref & 255U);
			}
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x06006741 RID: 26433 RVA: 0x0016D5A3 File Offset: 0x0016B7A3
		internal bool IsInvalid
		{
			get
			{
				return this._ref == 0U;
			}
		}

		// Token: 0x0400355C RID: 13660
		internal static readonly ExpiresEntryRef INVALID = new ExpiresEntryRef(0, 0);

		// Token: 0x0400355D RID: 13661
		private const uint ENTRY_MASK = 255U;

		// Token: 0x0400355E RID: 13662
		private const uint PAGE_MASK = 4294967040U;

		// Token: 0x0400355F RID: 13663
		private const int PAGE_SHIFT = 8;

		// Token: 0x04003560 RID: 13664
		private uint _ref;
	}
}
