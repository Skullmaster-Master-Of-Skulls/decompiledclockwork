using System;

namespace System.Collections.Generic
{
	// Token: 0x0200009C RID: 156
	internal struct SparseArrayBuilder<T>
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0000C039 File Offset: 0x0000A239
		public SparseArrayBuilder(bool initialize)
		{
			this = default(SparseArrayBuilder<T>);
			this._builder = new LargeArrayBuilder<T>(true);
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000C04E File Offset: 0x0000A24E
		public int Count
		{
			get
			{
				return checked(this._builder.Count + this._reservedCount);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000C062 File Offset: 0x0000A262
		public ArrayBuilder<Marker> Markers
		{
			get
			{
				return this._markers;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000C06A File Offset: 0x0000A26A
		public void Add(T item)
		{
			this._builder.Add(item);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000C078 File Offset: 0x0000A278
		public void AddRange(IEnumerable<T> items)
		{
			this._builder.AddRange(items);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000C088 File Offset: 0x0000A288
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			CopyPosition position = CopyPosition.Start;
			for (int i = 0; i < this._markers.Count; i++)
			{
				Marker marker = this._markers[i];
				int num2 = Math.Min(marker.Index - num, count);
				if (num2 > 0)
				{
					position = this._builder.CopyTo(position, array, arrayIndex, num2);
					arrayIndex += num2;
					num += num2;
					count -= num2;
				}
				if (count == 0)
				{
					return;
				}
				int num3 = Math.Min(marker.Count, count);
				arrayIndex += num3;
				num += num3;
				count -= num3;
			}
			this._builder.CopyTo(position, array, arrayIndex, count);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000C12C File Offset: 0x0000A32C
		public void Reserve(int count)
		{
			this._markers.Add(new Marker(count, this.Count));
			checked
			{
				this._reservedCount += count;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000C154 File Offset: 0x0000A354
		public bool ReserveOrAdd(IEnumerable<T> items)
		{
			int num;
			if (EnumerableHelpers.TryGetCount<T>(items, out num))
			{
				if (num > 0)
				{
					this.Reserve(num);
					return true;
				}
			}
			else
			{
				this.AddRange(items);
			}
			return false;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000C180 File Offset: 0x0000A380
		public T[] ToArray()
		{
			if (this._markers.Count == 0)
			{
				return this._builder.ToArray();
			}
			T[] array = new T[this.Count];
			this.CopyTo(array, 0, array.Length);
			return array;
		}

		// Token: 0x040004ED RID: 1261
		private LargeArrayBuilder<T> _builder;

		// Token: 0x040004EE RID: 1262
		private ArrayBuilder<Marker> _markers;

		// Token: 0x040004EF RID: 1263
		private int _reservedCount;
	}
}
