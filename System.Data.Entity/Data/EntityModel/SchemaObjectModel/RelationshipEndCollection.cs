using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000304 RID: 772
	internal sealed class RelationshipEndCollection : IList<IRelationshipEnd>, ICollection<IRelationshipEnd>, IEnumerable<IRelationshipEnd>, IEnumerable
	{
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x000AD26E File Offset: 0x000AB46E
		public int Count
		{
			get
			{
				return this.KeysInDefOrder.Count;
			}
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000AD27C File Offset: 0x000AB47C
		public void Add(IRelationshipEnd end)
		{
			SchemaElement end2 = end as SchemaElement;
			if (!RelationshipEndCollection.IsEndValid(end))
			{
				return;
			}
			if (!this.ValidateUniqueName(end2, end.Name))
			{
				return;
			}
			this.EndLookup.Add(end.Name, end);
			this.KeysInDefOrder.Add(end.Name);
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000AD2CC File Offset: 0x000AB4CC
		private static bool IsEndValid(IRelationshipEnd end)
		{
			return !string.IsNullOrEmpty(end.Name);
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000AD2DC File Offset: 0x000AB4DC
		private bool ValidateUniqueName(SchemaElement end, string name)
		{
			if (this.EndLookup.ContainsKey(name))
			{
				end.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.EndNameAlreadyDefinedDuplicate(name));
				return false;
			}
			return true;
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000AD300 File Offset: 0x000AB500
		public bool Remove(IRelationshipEnd end)
		{
			if (!RelationshipEndCollection.IsEndValid(end))
			{
				return false;
			}
			this.KeysInDefOrder.Remove(end.Name);
			return this.EndLookup.Remove(end.Name);
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000AD33C File Offset: 0x000AB53C
		public bool Contains(string name)
		{
			return this.EndLookup.ContainsKey(name);
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000AD34A File Offset: 0x000AB54A
		public bool Contains(IRelationshipEnd end)
		{
			return this.Contains(end.Name);
		}

		// Token: 0x170008F0 RID: 2288
		public IRelationshipEnd this[int index]
		{
			get
			{
				return this.EndLookup[this.KeysInDefOrder[index]];
			}
			set
			{
				throw EntityUtil.NotSupported();
			}
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000AD371 File Offset: 0x000AB571
		public IEnumerator<IRelationshipEnd> GetEnumerator()
		{
			return new RelationshipEndCollection.Enumerator(this.EndLookup, this.KeysInDefOrder);
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000AD384 File Offset: 0x000AB584
		public bool TryGetEnd(string name, out IRelationshipEnd end)
		{
			return this.EndLookup.TryGetValue(name, out end);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000AD371 File Offset: 0x000AB571
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new RelationshipEndCollection.Enumerator(this.EndLookup, this.KeysInDefOrder);
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000AD393 File Offset: 0x000AB593
		private Dictionary<string, IRelationshipEnd> EndLookup
		{
			get
			{
				if (this._endLookup == null)
				{
					this._endLookup = new Dictionary<string, IRelationshipEnd>(StringComparer.Ordinal);
				}
				return this._endLookup;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x000AD3B3 File Offset: 0x000AB5B3
		private List<string> KeysInDefOrder
		{
			get
			{
				if (this._keysInDefOrder == null)
				{
					this._keysInDefOrder = new List<string>();
				}
				return this._keysInDefOrder;
			}
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000AD3CE File Offset: 0x000AB5CE
		public void Clear()
		{
			this.EndLookup.Clear();
			this.KeysInDefOrder.Clear();
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x000173E2 File Offset: 0x000155E2
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x00013A81 File Offset: 0x00011C81
		int IList<IRelationshipEnd>.IndexOf(IRelationshipEnd end)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x00013A81 File Offset: 0x00011C81
		void IList<IRelationshipEnd>.Insert(int index, IRelationshipEnd end)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x00013A81 File Offset: 0x00011C81
		void IList<IRelationshipEnd>.RemoveAt(int index)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000AD3E8 File Offset: 0x000AB5E8
		public void CopyTo(IRelationshipEnd[] ends, int index)
		{
			foreach (IRelationshipEnd relationshipEnd in this)
			{
				ends[index++] = relationshipEnd;
			}
		}

		// Token: 0x040013EF RID: 5103
		private Dictionary<string, IRelationshipEnd> _endLookup;

		// Token: 0x040013F0 RID: 5104
		private List<string> _keysInDefOrder;

		// Token: 0x02000636 RID: 1590
		private sealed class Enumerator : IEnumerator<IRelationshipEnd>, IDisposable, IEnumerator
		{
			// Token: 0x0600438D RID: 17293 RVA: 0x000F5D73 File Offset: 0x000F3F73
			public Enumerator(Dictionary<string, IRelationshipEnd> data, List<string> keysInDefOrder)
			{
				this._Enumerator = keysInDefOrder.GetEnumerator();
				this._Data = data;
			}

			// Token: 0x0600438E RID: 17294 RVA: 0x000F5D8E File Offset: 0x000F3F8E
			public void Reset()
			{
				((IEnumerator)this._Enumerator).Reset();
			}

			// Token: 0x17000B9F RID: 2975
			// (get) Token: 0x0600438F RID: 17295 RVA: 0x000F5DA0 File Offset: 0x000F3FA0
			public IRelationshipEnd Current
			{
				get
				{
					return this._Data[this._Enumerator.Current];
				}
			}

			// Token: 0x17000BA0 RID: 2976
			// (get) Token: 0x06004390 RID: 17296 RVA: 0x000F5DA0 File Offset: 0x000F3FA0
			object IEnumerator.Current
			{
				get
				{
					return this._Data[this._Enumerator.Current];
				}
			}

			// Token: 0x06004391 RID: 17297 RVA: 0x000F5DB8 File Offset: 0x000F3FB8
			public bool MoveNext()
			{
				return this._Enumerator.MoveNext();
			}

			// Token: 0x06004392 RID: 17298 RVA: 0x000089D0 File Offset: 0x00006BD0
			public void Dispose()
			{
			}

			// Token: 0x04001EBE RID: 7870
			private List<string>.Enumerator _Enumerator;

			// Token: 0x04001EBF RID: 7871
			private Dictionary<string, IRelationshipEnd> _Data;
		}
	}
}
