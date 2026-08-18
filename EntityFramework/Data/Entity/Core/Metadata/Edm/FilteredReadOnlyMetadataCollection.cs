using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E7 RID: 1255
	internal class FilteredReadOnlyMetadataCollection<TDerived, TBase> : ReadOnlyMetadataCollection<TDerived>, IBaseList<TBase>, IList, ICollection, IEnumerable where TDerived : TBase where TBase : MetadataItem
	{
		// Token: 0x06002EA8 RID: 11944 RVA: 0x000DF783 File Offset: 0x000DD983
		internal FilteredReadOnlyMetadataCollection(ReadOnlyMetadataCollection<TBase> collection, Predicate<TBase> predicate) : base(FilteredReadOnlyMetadataCollection<TDerived, TBase>.FilterCollection(collection, predicate))
		{
			this._source = collection;
			this._predicate = predicate;
		}

		// Token: 0x170006EA RID: 1770
		public override TDerived this[string identity]
		{
			get
			{
				TBase tbase = this._source[identity];
				if (this._predicate(tbase))
				{
					return (TDerived)((object)tbase);
				}
				throw new ArgumentException(Strings.ItemInvalidIdentity(identity), "identity");
			}
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000DF7E4 File Offset: 0x000DD9E4
		public override TDerived GetValue(string identity, bool ignoreCase)
		{
			TBase value = this._source.GetValue(identity, ignoreCase);
			if (this._predicate(value))
			{
				return (TDerived)((object)value);
			}
			throw new ArgumentException(Strings.ItemInvalidIdentity(identity), "identity");
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000DF82C File Offset: 0x000DDA2C
		public override bool Contains(string identity)
		{
			TBase obj;
			return this._source.TryGetValue(identity, false, out obj) && this._predicate(obj);
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000DF858 File Offset: 0x000DDA58
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

		// Token: 0x06002EAD RID: 11949 RVA: 0x000DF8A0 File Offset: 0x000DDAA0
		internal static List<TDerived> FilterCollection(ReadOnlyMetadataCollection<TBase> collection, Predicate<TBase> predicate)
		{
			List<TDerived> list = new List<TDerived>(collection.Count);
			for (int i = 0; i < collection.Count; i++)
			{
				TBase tbase = collection[i];
				if (predicate(tbase))
				{
					list.Add((TDerived)((object)tbase));
				}
			}
			return list;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000DF8F0 File Offset: 0x000DDAF0
		[SuppressMessage("Microsoft.Design", "CA1061:DoNotHideBaseClassMethods")]
		public override int IndexOf(TDerived value)
		{
			TBase tbase;
			if (this._source.TryGetValue(value.Identity, false, out tbase) && this._predicate(tbase))
			{
				return base.IndexOf((TDerived)((object)tbase));
			}
			return -1;
		}

		// Token: 0x170006EB RID: 1771
		TBase IBaseList<!1>.this[string identity]
		{
			get
			{
				return (TBase)((object)this[identity]);
			}
		}

		// Token: 0x170006EC RID: 1772
		TBase IBaseList<!1>.this[int index]
		{
			get
			{
				return (TBase)((object)base[index]);
			}
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000DF961 File Offset: 0x000DDB61
		int IBaseList<!1>.IndexOf(TBase item)
		{
			if (this._predicate(item))
			{
				return this.IndexOf((TDerived)((object)item));
			}
			return -1;
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000DF984 File Offset: 0x000DDB84
		bool IList.get_IsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x040011C6 RID: 4550
		private readonly ReadOnlyMetadataCollection<TBase> _source;

		// Token: 0x040011C7 RID: 4551
		private readonly Predicate<TBase> _predicate;
	}
}
