using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200029B RID: 667
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class DbRawSqlQuery<TElement> : IEnumerable<!0>, IEnumerable, IListSource, IDbAsyncEnumerable<!0>, IDbAsyncEnumerable
	{
		// Token: 0x06001793 RID: 6035 RVA: 0x00078996 File Offset: 0x00076B96
		internal DbRawSqlQuery(InternalSqlQuery internalQuery)
		{
			this._internalQuery = internalQuery;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000789A5 File Offset: 0x00076BA5
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public virtual DbRawSqlQuery<TElement> AsStreaming()
		{
			if (this._internalQuery != null)
			{
				return new DbRawSqlQuery<TElement>(this._internalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000789C1 File Offset: 0x00076BC1
		public virtual IEnumerator<TElement> GetEnumerator()
		{
			return (IEnumerator<TElement>)this.GetInternalQueryWithCheck("GetEnumerator").GetEnumerator();
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000789D8 File Offset: 0x00076BD8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000789E0 File Offset: 0x00076BE0
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator<TElement> IDbAsyncEnumerable<!0>.GetAsyncEnumerator()
		{
			return (IDbAsyncEnumerator<TElement>)this.GetInternalQueryWithCheck("IDbAsyncEnumerable<TElement>.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000789F7 File Offset: 0x00076BF7
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this._internalQuery.GetAsyncEnumerator();
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00078A04 File Offset: 0x00076C04
		public Task ForEachAsync(Action<TElement> action)
		{
			Check.NotNull<Action<TElement>>(action, "action");
			return this.ForEachAsync(action, CancellationToken.None);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00078A1E File Offset: 0x00076C1E
		public Task ForEachAsync(Action<TElement> action, CancellationToken cancellationToken)
		{
			Check.NotNull<Action<TElement>>(action, "action");
			return this.ForEachAsync(action, cancellationToken);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00078A34 File Offset: 0x00076C34
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<List<TElement>> ToListAsync()
		{
			return this.ToListAsync<TElement>();
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00078A3C File Offset: 0x00076C3C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<List<TElement>> ToListAsync(CancellationToken cancellationToken)
		{
			return this.ToListAsync(cancellationToken);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00078A45 File Offset: 0x00076C45
		public Task<TElement[]> ToArrayAsync()
		{
			return this.ToArrayAsync<TElement>();
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00078A4D File Offset: 0x00076C4D
		public Task<TElement[]> ToArrayAsync(CancellationToken cancellationToken)
		{
			return this.ToArrayAsync(cancellationToken);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00078A56 File Offset: 0x00076C56
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey>(Func<TElement, TKey> keySelector)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			return this.ToDictionaryAsync(keySelector);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00078A6B File Offset: 0x00076C6B
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey>(Func<TElement, TKey> keySelector, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			return this.ToDictionaryAsync(keySelector, cancellationToken);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00078A81 File Offset: 0x00076C81
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey>(Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			return this.ToDictionaryAsync(keySelector, comparer);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00078A97 File Offset: 0x00076C97
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey>(Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			return this.ToDictionaryAsync(keySelector, comparer, cancellationToken);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00078AAE File Offset: 0x00076CAE
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<Dictionary<TKey, TResult>> ToDictionaryAsync<TKey, TResult>(Func<TElement, TKey> keySelector, Func<TElement, TResult> elementSelector)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TElement, TResult>>(elementSelector, "elementSelector");
			return this.ToDictionaryAsync(keySelector, elementSelector);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00078AD0 File Offset: 0x00076CD0
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<Dictionary<TKey, TResult>> ToDictionaryAsync<TKey, TResult>(Func<TElement, TKey> keySelector, Func<TElement, TResult> elementSelector, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TElement, TResult>>(elementSelector, "elementSelector");
			return this.ToDictionaryAsync(keySelector, elementSelector, cancellationToken);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00078AF3 File Offset: 0x00076CF3
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<Dictionary<TKey, TResult>> ToDictionaryAsync<TKey, TResult>(Func<TElement, TKey> keySelector, Func<TElement, TResult> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TElement, TResult>>(elementSelector, "elementSelector");
			return this.ToDictionaryAsync(keySelector, elementSelector, comparer);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00078B16 File Offset: 0x00076D16
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<Dictionary<TKey, TResult>> ToDictionaryAsync<TKey, TResult>(Func<TElement, TKey> keySelector, Func<TElement, TResult> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TElement, TResult>>(elementSelector, "elementSelector");
			return this.ToDictionaryAsync(keySelector, elementSelector, comparer, cancellationToken);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00078B3B File Offset: 0x00076D3B
		public Task<TElement> FirstAsync()
		{
			return this.FirstAsync<TElement>();
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00078B43 File Offset: 0x00076D43
		public Task<TElement> FirstAsync(CancellationToken cancellationToken)
		{
			return this.FirstAsync(cancellationToken);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00078B4C File Offset: 0x00076D4C
		public Task<TElement> FirstAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.FirstAsync(predicate);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00078B61 File Offset: 0x00076D61
		public Task<TElement> FirstAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.FirstAsync(predicate, cancellationToken);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00078B77 File Offset: 0x00076D77
		public Task<TElement> FirstOrDefaultAsync()
		{
			return this.FirstOrDefaultAsync<TElement>();
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00078B7F File Offset: 0x00076D7F
		public Task<TElement> FirstOrDefaultAsync(CancellationToken cancellationToken)
		{
			return this.FirstOrDefaultAsync(cancellationToken);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00078B88 File Offset: 0x00076D88
		public Task<TElement> FirstOrDefaultAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.FirstOrDefaultAsync(predicate);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00078B9D File Offset: 0x00076D9D
		public Task<TElement> FirstOrDefaultAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.FirstOrDefaultAsync(predicate, cancellationToken);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00078BB3 File Offset: 0x00076DB3
		public Task<TElement> SingleAsync()
		{
			return this.SingleAsync<TElement>();
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00078BBB File Offset: 0x00076DBB
		public Task<TElement> SingleAsync(CancellationToken cancellationToken)
		{
			return this.SingleAsync(cancellationToken);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00078BC4 File Offset: 0x00076DC4
		public Task<TElement> SingleAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.SingleAsync(predicate);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00078BD9 File Offset: 0x00076DD9
		public Task<TElement> SingleAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.SingleAsync(predicate, cancellationToken);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00078BEF File Offset: 0x00076DEF
		public Task<TElement> SingleOrDefaultAsync()
		{
			return this.SingleOrDefaultAsync<TElement>();
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00078BF7 File Offset: 0x00076DF7
		public Task<TElement> SingleOrDefaultAsync(CancellationToken cancellationToken)
		{
			return this.SingleOrDefaultAsync(cancellationToken);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00078C00 File Offset: 0x00076E00
		public Task<TElement> SingleOrDefaultAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.SingleOrDefaultAsync(predicate);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00078C15 File Offset: 0x00076E15
		public Task<TElement> SingleOrDefaultAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.SingleOrDefaultAsync(predicate, cancellationToken);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00078C2B File Offset: 0x00076E2B
		public Task<bool> ContainsAsync(TElement value)
		{
			return this.ContainsAsync(value);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00078C34 File Offset: 0x00076E34
		public Task<bool> ContainsAsync(TElement value, CancellationToken cancellationToken)
		{
			return this.ContainsAsync(value, cancellationToken);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00078C3E File Offset: 0x00076E3E
		public Task<bool> AnyAsync()
		{
			return this.AnyAsync<TElement>();
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00078C46 File Offset: 0x00076E46
		public Task<bool> AnyAsync(CancellationToken cancellationToken)
		{
			return this.AnyAsync(cancellationToken);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00078C4F File Offset: 0x00076E4F
		public Task<bool> AnyAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.AnyAsync(predicate);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00078C64 File Offset: 0x00076E64
		public Task<bool> AnyAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.AnyAsync(predicate, cancellationToken);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00078C7A File Offset: 0x00076E7A
		public Task<bool> AllAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.AllAsync(predicate);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00078C8F File Offset: 0x00076E8F
		public Task<bool> AllAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.AllAsync(predicate, cancellationToken);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00078CA5 File Offset: 0x00076EA5
		public Task<int> CountAsync()
		{
			return this.CountAsync<TElement>();
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00078CAD File Offset: 0x00076EAD
		public Task<int> CountAsync(CancellationToken cancellationToken)
		{
			return this.CountAsync(cancellationToken);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00078CB6 File Offset: 0x00076EB6
		public Task<int> CountAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.CountAsync(predicate);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00078CCB File Offset: 0x00076ECB
		public Task<int> CountAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.CountAsync(predicate, cancellationToken);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00078CE1 File Offset: 0x00076EE1
		public Task<long> LongCountAsync()
		{
			return this.LongCountAsync<TElement>();
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00078CE9 File Offset: 0x00076EE9
		public Task<long> LongCountAsync(CancellationToken cancellationToken)
		{
			return this.LongCountAsync(cancellationToken);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00078CF2 File Offset: 0x00076EF2
		public Task<long> LongCountAsync(Func<TElement, bool> predicate)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.LongCountAsync(predicate);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00078D07 File Offset: 0x00076F07
		public Task<long> LongCountAsync(Func<TElement, bool> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<TElement, bool>>(predicate, "predicate");
			return this.LongCountAsync(predicate, cancellationToken);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00078D1D File Offset: 0x00076F1D
		public Task<TElement> MinAsync()
		{
			return this.MinAsync<TElement>();
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00078D25 File Offset: 0x00076F25
		public Task<TElement> MinAsync(CancellationToken cancellationToken)
		{
			return this.MinAsync(cancellationToken);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00078D2E File Offset: 0x00076F2E
		public Task<TElement> MaxAsync()
		{
			return this.MaxAsync<TElement>();
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00078D36 File Offset: 0x00076F36
		public Task<TElement> MaxAsync(CancellationToken cancellationToken)
		{
			return this.MaxAsync(cancellationToken);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00078D3F File Offset: 0x00076F3F
		public override string ToString()
		{
			if (this._internalQuery != null)
			{
				return this._internalQuery.ToString();
			}
			return base.ToString();
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x00078D5B File Offset: 0x00076F5B
		internal InternalSqlQuery InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00078D63 File Offset: 0x00076F63
		private InternalSqlQuery GetInternalQueryWithCheck(string memberName)
		{
			if (this._internalQuery == null)
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSqlQuery<>).Name));
			}
			return this._internalQuery;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x00078D99 File Offset: 0x00076F99
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00078D9C File Offset: 0x00076F9C
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IList IListSource.GetList()
		{
			throw Error.DbQuery_BindingToDbQueryNotSupported();
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00078DA3 File Offset: 0x00076FA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00078DAC File Offset: 0x00076FAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00078DB4 File Offset: 0x00076FB4
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000858 RID: 2136
		private readonly InternalSqlQuery _internalQuery;
	}
}
