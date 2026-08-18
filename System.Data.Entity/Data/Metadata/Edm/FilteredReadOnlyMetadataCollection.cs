using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001DB RID: 475
	internal class FilteredReadOnlyMetadataCollection<TDerived, TBase> : ReadOnlyMetadataCollection<TDerived>, IBaseList<TBase>, IList, ICollection, IEnumerable where TDerived : TBase where TBase : MetadataItem
	{
		// Token: 0x0600200E RID: 8206 RVA: 0x00070054 File Offset: 0x0006E254
		internal FilteredReadOnlyMetadataCollection(ReadOnlyMetadataCollection<TBase> collection, Predicate<TBase> predicate) : base(FilteredReadOnlyMetadataCollection<TDerived, TBase>.FilterCollection(collection, predicate))
		{
			this._source = collection;
			this._predicate = predicate;
		}

		// Token: 0x17000673 RID: 1651
		public override TDerived this[string identity]
		{
			get
			{
				TBase tbase = this._source[identity];
				if (this._predicate(tbase))
				{
					return (TDerived)((object)tbase);
				}
				throw EntityUtil.ItemInvalidIdentity(identity, "identity");
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000700B4 File Offset: 0x0006E2B4
		public override TDerived GetValue(string identity, bool ignoreCase)
		{
			TBase value = this._source.GetValue(identity, ignoreCase);
			if (this._predicate(value))
			{
				return (TDerived)((object)value);
			}
			throw EntityUtil.ItemInvalidIdentity(identity, "identity");
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000700F4 File Offset: 0x0006E2F4
		public override bool Contains(string identity)
		{
			TBase obj;
			return this._source.TryGetValue(identity, false, out obj) && this._predicate(obj);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00070120 File Offset: 0x0006E320
		public override bool TryGetValue(string identity, bool ignoreCase, out TDerived item)
		{
			item = default(TDerived);
			TBase tbase;
			if (this._source.TryGetValue(identity, ignoreCase, out tbase) && this._predicate(tbase))
			{
				item = (TDerived)((object)tbase);
				return true;
			}
			return false;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00070168 File Offset: 0x0006E368
		internal static List<TDerived> FilterCollection(ReadOnlyMetadataCollection<TBase> collection, Predicate<TBase> predicate)
		{
			List<TDerived> list = new List<TDerived>(collection.Count);
			foreach (TBase tbase in collection)
			{
				if (predicate(tbase))
				{
					list.Add((TDerived)((object)tbase));
				}
			}
			return list;
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000701D8 File Offset: 0x0006E3D8
		public override int IndexOf(TDerived value)
		{
			TBase tbase;
			if (this._source.TryGetValue(value.Identity, false, out tbase) && this._predicate(tbase))
			{
				return base.IndexOf((TDerived)((object)tbase));
			}
			return -1;
		}

		// Token: 0x17000674 RID: 1652
		TBase IBaseList<!1>.this[string identity]
		{
			get
			{
				return (TBase)((object)this[identity]);
			}
		}

		// Token: 0x17000675 RID: 1653
		TBase IBaseList<!1>.this[int index]
		{
			get
			{
				return (TBase)((object)base[index]);
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x00070247 File Offset: 0x0006E447
		int IBaseList<!1>.IndexOf(TBase item)
		{
			if (this._predicate(item))
			{
				return this.IndexOf((TDerived)((object)item));
			}
			return -1;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0007026A File Offset: 0x0006E46A
		bool IList.get_IsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x04000E36 RID: 3638
		private readonly ReadOnlyMetadataCollection<TBase> _source;

		// Token: 0x04000E37 RID: 3639
		private readonly Predicate<TBase> _predicate;
	}
}
