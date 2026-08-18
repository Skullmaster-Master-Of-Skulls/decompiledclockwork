using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005A9 RID: 1449
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class ObjectResult<T> : ObjectResult, IEnumerable<!0>, IEnumerable, IDbAsyncEnumerable<!0>, IDbAsyncEnumerable
	{
		// Token: 0x0600398F RID: 14735 RVA: 0x00111418 File Offset: 0x0010F618
		protected ObjectResult()
		{
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x00111420 File Offset: 0x0010F620
		internal ObjectResult(Shaper<T> shaper, EntitySet singleEntitySet, TypeUsage resultItemType) : this(shaper, singleEntitySet, resultItemType, true, true, null)
		{
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x00111430 File Offset: 0x0010F630
		internal ObjectResult(Shaper<T> shaper, EntitySet singleEntitySet, TypeUsage resultItemType, bool readerOwned, bool shouldReleaseConnection, DbCommand command = null) : this(shaper, singleEntitySet, resultItemType, readerOwned, shouldReleaseConnection, null, null, command)
		{
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x00111450 File Offset: 0x0010F650
		internal ObjectResult(Shaper<T> shaper, EntitySet singleEntitySet, TypeUsage resultItemType, bool readerOwned, bool shouldReleaseConnection, NextResultGenerator nextResultGenerator, Action<object, EventArgs> onReaderDispose, DbCommand command = null)
		{
			this._shaper = shaper;
			this._reader = this._shaper.Reader;
			this._command = command;
			this._singleEntitySet = singleEntitySet;
			this._resultItemType = resultItemType;
			this._readerOwned = readerOwned;
			this._shouldReleaseConnection = shouldReleaseConnection;
			this._nextResultGenerator = nextResultGenerator;
			this._onReaderDispose = onReaderDispose;
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x001114B1 File Offset: 0x0010F6B1
		private void EnsureCanEnumerateResults()
		{
			if (this._shaper == null)
			{
				throw new InvalidOperationException(Strings.Materializer_CannotReEnumerateQueryResults);
			}
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x001114C6 File Offset: 0x0010F6C6
		public virtual IEnumerator<T> GetEnumerator()
		{
			return this.GetDbEnumerator();
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x001114D0 File Offset: 0x0010F6D0
		internal virtual IDbEnumerator<T> GetDbEnumerator()
		{
			this.EnsureCanEnumerateResults();
			Shaper<T> shaper = this._shaper;
			this._shaper = null;
			return shaper.GetEnumerator();
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x001114F9 File Offset: 0x0010F6F9
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator<T> IDbAsyncEnumerable<!0>.GetAsyncEnumerator()
		{
			return this.GetDbEnumerator();
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x00111504 File Offset: 0x0010F704
		protected override void Dispose(bool disposing)
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
				if (this._shaper.Context != null && this._readerOwned && this._shouldReleaseConnection)
				{
					this._shaper.Context.ReleaseConnection();
				}
				this._shaper = null;
			}
			if (this._command != null)
			{
				this._command.Dispose();
				this._command = null;
			}
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x001115AD File Offset: 0x0010F7AD
		internal override IDbAsyncEnumerator GetAsyncEnumeratorInternal()
		{
			return this.GetDbEnumerator();
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x001115B5 File Offset: 0x0010F7B5
		internal override IEnumerator GetEnumeratorInternal()
		{
			return this.GetDbEnumerator();
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x001115C0 File Offset: 0x0010F7C0
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

		// Token: 0x0600399B RID: 14747 RVA: 0x00111614 File Offset: 0x0010F814
		internal override ObjectResult<TElement> GetNextResultInternal<TElement>()
		{
			if (this._nextResultGenerator == null)
			{
				return null;
			}
			return this._nextResultGenerator.GetNextResult<TElement>(this._reader);
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x00111631 File Offset: 0x0010F831
		public override Type ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x040015E8 RID: 5608
		private Shaper<T> _shaper;

		// Token: 0x040015E9 RID: 5609
		private DbDataReader _reader;

		// Token: 0x040015EA RID: 5610
		private DbCommand _command;

		// Token: 0x040015EB RID: 5611
		private readonly EntitySet _singleEntitySet;

		// Token: 0x040015EC RID: 5612
		private readonly TypeUsage _resultItemType;

		// Token: 0x040015ED RID: 5613
		private readonly bool _readerOwned;

		// Token: 0x040015EE RID: 5614
		private readonly bool _shouldReleaseConnection;

		// Token: 0x040015EF RID: 5615
		private IBindingList _cachedBindingList;

		// Token: 0x040015F0 RID: 5616
		private NextResultGenerator _nextResultGenerator;

		// Token: 0x040015F1 RID: 5617
		private Action<object, EventArgs> _onReaderDispose;
	}
}
