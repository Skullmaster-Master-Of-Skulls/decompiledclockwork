using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000380 RID: 896
	internal sealed class RelationshipEndCollection : IList<IRelationshipEnd>, ICollection<IRelationshipEnd>, IEnumerable<IRelationshipEnd>, IEnumerable
	{
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00099259 File Offset: 0x00097459
		public int Count
		{
			get
			{
				return this.KeysInDefOrder.Count;
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00099268 File Offset: 0x00097468
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

		// Token: 0x0600205B RID: 8283 RVA: 0x000992B8 File Offset: 0x000974B8
		private static bool IsEndValid(IRelationshipEnd end)
		{
			return !string.IsNullOrEmpty(end.Name);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000992C8 File Offset: 0x000974C8
		private bool ValidateUniqueName(SchemaElement end, string name)
		{
			if (this.EndLookup.ContainsKey(name))
			{
				end.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.EndNameAlreadyDefinedDuplicate(name));
				return false;
			}
			return true;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000992EC File Offset: 0x000974EC
		public bool Remove(IRelationshipEnd end)
		{
			if (!RelationshipEndCollection.IsEndValid(end))
			{
				return false;
			}
			this.KeysInDefOrder.Remove(end.Name);
			return this.EndLookup.Remove(end.Name);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00099328 File Offset: 0x00097528
		public bool Contains(string name)
		{
			return this.EndLookup.ContainsKey(name);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00099336 File Offset: 0x00097536
		public bool Contains(IRelationshipEnd end)
		{
			return this.Contains(end.Name);
		}

		// Token: 0x17000416 RID: 1046
		public IRelationshipEnd this[int index]
		{
			get
			{
				return this.EndLookup[this.KeysInDefOrder[index]];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00099364 File Offset: 0x00097564
		public IEnumerator<IRelationshipEnd> GetEnumerator()
		{
			return new RelationshipEndCollection.Enumerator(this.EndLookup, this.KeysInDefOrder);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00099377 File Offset: 0x00097577
		public bool TryGetEnd(string name, out IRelationshipEnd end)
		{
			return this.EndLookup.TryGetValue(name, out end);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00099386 File Offset: 0x00097586
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new RelationshipEndCollection.Enumerator(this.EndLookup, this.KeysInDefOrder);
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x00099399 File Offset: 0x00097599
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

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x000993B9 File Offset: 0x000975B9
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

		// Token: 0x06002067 RID: 8295 RVA: 0x000993D4 File Offset: 0x000975D4
		public void Clear()
		{
			this.EndLookup.Clear();
			this.KeysInDefOrder.Clear();
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x000993EC File Offset: 0x000975EC
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x000993EF File Offset: 0x000975EF
		int IList<IRelationshipEnd>.IndexOf(IRelationshipEnd end)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000993F6 File Offset: 0x000975F6
		void IList<IRelationshipEnd>.Insert(int index, IRelationshipEnd end)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x000993FD File Offset: 0x000975FD
		void IList<IRelationshipEnd>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00099404 File Offset: 0x00097604
		public void CopyTo(IRelationshipEnd[] ends, int index)
		{
			foreach (IRelationshipEnd relationshipEnd in this)
			{
				ends[index++] = relationshipEnd;
			}
		}

		// Token: 0x04000B7E RID: 2942
		private Dictionary<string, IRelationshipEnd> _endLookup;

		// Token: 0x04000B7F RID: 2943
		private List<string> _keysInDefOrder;

		// Token: 0x02000381 RID: 897
		private sealed class Enumerator : IEnumerator<IRelationshipEnd>, IDisposable, IEnumerator
		{
			// Token: 0x0600206E RID: 8302 RVA: 0x00099458 File Offset: 0x00097658
			public Enumerator(Dictionary<string, IRelationshipEnd> data, List<string> keysInDefOrder)
			{
				this._Enumerator = keysInDefOrder.GetEnumerator();
				this._Data = data;
			}

			// Token: 0x0600206F RID: 8303 RVA: 0x00099473 File Offset: 0x00097673
			public void Reset()
			{
				((IEnumerator)this._Enumerator).Reset();
			}

			// Token: 0x1700041A RID: 1050
			// (get) Token: 0x06002070 RID: 8304 RVA: 0x00099485 File Offset: 0x00097685
			public IRelationshipEnd Current
			{
				get
				{
					return this._Data[this._Enumerator.Current];
				}
			}

			// Token: 0x1700041B RID: 1051
			// (get) Token: 0x06002071 RID: 8305 RVA: 0x0009949D File Offset: 0x0009769D
			object IEnumerator.Current
			{
				get
				{
					return this._Data[this._Enumerator.Current];
				}
			}

			// Token: 0x06002072 RID: 8306 RVA: 0x000994B5 File Offset: 0x000976B5
			public bool MoveNext()
			{
				return this._Enumerator.MoveNext();
			}

			// Token: 0x06002073 RID: 8307 RVA: 0x000994C2 File Offset: 0x000976C2
			public void Dispose()
			{
			}

			// Token: 0x04000B80 RID: 2944
			private List<string>.Enumerator _Enumerator;

			// Token: 0x04000B81 RID: 2945
			private readonly Dictionary<string, IRelationshipEnd> _Data;
		}
	}
}
