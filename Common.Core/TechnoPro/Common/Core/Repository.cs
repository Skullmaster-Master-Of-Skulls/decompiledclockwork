using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core
{
	// Token: 0x0200001B RID: 27
	public class Repository<TU, TV> : Dictionary<TU, TV>, IRepository<TU, TV> where TV : BusinessBase<TU>
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000045D0 File Offset: 0x000027D0
		public IList<TV> ToList()
		{
			List<TV> list = new List<TV>(base.Count);
			foreach (TV item in base.Values)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000463C File Offset: 0x0000283C
		public object SyncObj
		{
			get
			{
				bool flag = this._syncRoot == null;
				if (flag)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004678 File Offset: 0x00002878
		int IRepository<!0, !1>.Count
		{
			get
			{
				object syncObj = this.SyncObj;
				int count;
				lock (syncObj)
				{
					count = base.Count;
				}
				return count;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000046C0 File Offset: 0x000028C0
		bool IRepository<!0, !1>.Contains(TU id)
		{
			object syncObj = this.SyncObj;
			bool result;
			lock (syncObj)
			{
				result = base.ContainsKey(id);
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004708 File Offset: 0x00002908
		bool IRepository<!0, !1>.Contains(TV entity)
		{
			object syncObj = this.SyncObj;
			bool result;
			lock (syncObj)
			{
				result = base.ContainsValue(entity);
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004750 File Offset: 0x00002950
		void IRepository<!0, !1>.Remove(TU id)
		{
			object syncObj = this.SyncObj;
			lock (syncObj)
			{
				base.Remove(id);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004798 File Offset: 0x00002998
		void IRepository<!0, !1>.Remove(TV entity)
		{
			object syncObj = this.SyncObj;
			lock (syncObj)
			{
				base.Remove(entity.Id);
			}
		}

		// Token: 0x17000036 RID: 54
		TV IRepository<!0, !1>.this[TU id]
		{
			get
			{
				object syncObj = this.SyncObj;
				TV result;
				lock (syncObj)
				{
					result = (base.ContainsKey(id) ? base[id] : default(TV));
				}
				return result;
			}
			set
			{
				object syncObj = this.SyncObj;
				lock (syncObj)
				{
					base[id] = value;
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004890 File Offset: 0x00002A90
		TV IRepository<!0, !1>.Get(TU id)
		{
			object syncObj = this.SyncObj;
			TV result;
			lock (syncObj)
			{
				result = (base.ContainsKey(id) ? base[id] : default(TV));
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000048EC File Offset: 0x00002AEC
		TV IRepository<!0, !1>.Save(TV entity)
		{
			return ((IRepository<TU, TV>)this).SaveOrUpdate(entity);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004908 File Offset: 0x00002B08
		TV IRepository<!0, !1>.SaveOrUpdate(TV entity)
		{
			object syncObj = this.SyncObj;
			lock (syncObj)
			{
				bool flag2 = base.ContainsKey(entity.Id);
				if (flag2)
				{
					base[entity.Id] = entity;
				}
				else
				{
					base.Add(entity.Id, entity);
				}
			}
			return entity;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000498C File Offset: 0x00002B8C
		void IRepository<!0, !1>.Update(TV entity)
		{
			object syncObj = this.SyncObj;
			lock (syncObj)
			{
				bool flag2 = base.ContainsKey(entity.Id);
				if (flag2)
				{
					base[entity.Id] = entity;
				}
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000049F4 File Offset: 0x00002BF4
		TV IRepository<!0, !1>.FindOne(Predicate<TV> filter)
		{
			object syncObj = this.SyncObj;
			lock (syncObj)
			{
				foreach (TV tv in base.Values)
				{
					bool flag2 = filter(tv);
					if (flag2)
					{
						return tv;
					}
				}
			}
			return default(TV);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004A94 File Offset: 0x00002C94
		ICollection<TV> IRepository<!0, !1>.FindAll(Predicate<TV> filter)
		{
			List<TV> list = new List<TV>();
			object syncObj = this.SyncObj;
			lock (syncObj)
			{
				foreach (TV tv in base.Values)
				{
					bool flag2 = filter(tv);
					if (flag2)
					{
						list.Add(tv);
					}
				}
			}
			return list;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004B38 File Offset: 0x00002D38
		IEnumerable<TV> IRepository<!0, !1>.Items
		{
			get
			{
				object syncObj = this.SyncObj;
				IEnumerable<TV> values;
				lock (syncObj)
				{
					values = base.Values;
				}
				return values;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004B80 File Offset: 0x00002D80
		int IRepository<!0, !1>.RemoveAll(Predicate<TV> filter)
		{
			object syncObj = this.SyncObj;
			int count;
			lock (syncObj)
			{
				IEnumerable<TV> source = from val in base.Values
				where filter(val)
				select val;
				List<TV> list = source.ToList<TV>();
				foreach (TV tv in list)
				{
					base.Remove(tv.Id);
				}
				count = list.Count;
			}
			return count;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004C48 File Offset: 0x00002E48
		void IRepository<!0, !1>.Clear()
		{
			object syncObj = this.SyncObj;
			lock (syncObj)
			{
				base.Clear();
			}
		}

		// Token: 0x0400003B RID: 59
		protected object _syncRoot;
	}
}
