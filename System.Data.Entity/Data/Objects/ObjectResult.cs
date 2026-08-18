using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Common.Internal.Materialization;
using System.Data.Metadata.Edm;

namespace System.Data.Objects
{
	// Token: 0x02000130 RID: 304
	public sealed class ObjectResult<T> : ObjectResult, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600161B RID: 5659 RVA: 0x0004AA40 File Offset: 0x00048C40
		internal ObjectResult(Shaper<T> shaper, EntitySet singleEntitySet, TypeUsage resultItemType) : this(shaper, singleEntitySet, resultItemType, true)
		{
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0004AA4C File Offset: 0x00048C4C
		internal ObjectResult(Shaper<T> shaper, EntitySet singleEntitySet, TypeUsage resultItemType, bool readerOwned) : this(shaper, singleEntitySet, resultItemType, readerOwned, null, null)
		{
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0004AA5C File Offset: 0x00048C5C
		internal ObjectResult(Shaper<T> shaper, EntitySet singleEntitySet, TypeUsage resultItemType, bool readerOwned, NextResultGenerator nextResultGenerator, Action<object, EventArgs> onReaderDispose)
		{
			this._shaper = shaper;
			this._reader = this._shaper.Reader;
			this._singleEntitySet = singleEntitySet;
			this._resultItemType = resultItemType;
			this._readerOwned = readerOwned;
			this._nextResultGenerator = nextResultGenerator;
			this._onReaderDispose = onReaderDispose;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0004AAAD File Offset: 0x00048CAD
		private void EnsureCanEnumerateResults()
		{
			if (this._shaper == null)
			{
				throw EntityUtil.CannotReEnumerateQueryResults();
			}
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0004AAC0 File Offset: 0x00048CC0
		public IEnumerator<T> GetEnumerator()
		{
			this.EnsureCanEnumerateResults();
			Shaper<T> shaper = this._shaper;
			this._shaper = null;
			return shaper.GetEnumerator();
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0004AAEC File Offset: 0x00048CEC
		public override void Dispose()
		{
			DbDataReader reader = this._reader;
			this._reader = null;
			this._nextResultGenerator = null;
			if (reader != null && this._readerOwned)
			{
				reader.Dispose();
				if (this._onReaderDispose != null)
				{
					this._onReaderDispose(this, new EventArgs());
					this._onReaderDispose = null;
				}
			}
			if (this._shaper != null)
			{
				if (this._shaper.Context != null && this._readerOwned)
				{
					this._shaper.Context.ReleaseConnection();
				}
				this._shaper = null;
			}
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0004AB73 File Offset: 0x00048D73
		internal override IEnumerator GetEnumeratorInternal()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0004AB7C File Offset: 0x00048D7C
		internal override IList GetIListSourceListInternal()
		{
			if (this._cachedBindingList == null)
			{
				this.EnsureCanEnumerateResults();
				bool forceReadOnly = this._shaper.MergeOption == MergeOption.NoTracking;
				this._cachedBindingList = ObjectViewFactory.CreateViewForQuery<T>(this._resultItemType, this, this._shaper.Context, forceReadOnly, this._singleEntitySet);
			}
			return this._cachedBindingList;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0004ABD0 File Offset: 0x00048DD0
		internal override ObjectResult<TElement> GetNextResultInternal<TElement>()
		{
			if (this._nextResultGenerator == null)
			{
				return null;
			}
			return this._nextResultGenerator.GetNextResult<TElement>(this._reader);
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0004ABED File Offset: 0x00048DED
		public override Type ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x04000A47 RID: 2631
		private Shaper<T> _shaper;

		// Token: 0x04000A48 RID: 2632
		private DbDataReader _reader;

		// Token: 0x04000A49 RID: 2633
		private readonly EntitySet _singleEntitySet;

		// Token: 0x04000A4A RID: 2634
		private readonly TypeUsage _resultItemType;

		// Token: 0x04000A4B RID: 2635
		private readonly bool _readerOwned;

		// Token: 0x04000A4C RID: 2636
		private IBindingList _cachedBindingList;

		// Token: 0x04000A4D RID: 2637
		private NextResultGenerator _nextResultGenerator;

		// Token: 0x04000A4E RID: 2638
		private Action<object, EventArgs> _onReaderDispose;
	}
}
