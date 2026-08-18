using System;
using System.Collections;
using System.Security;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000111 RID: 273
	internal static class ClientUtils
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x00014E0E File Offset: 0x0001300E
		public static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00014E4B File Offset: 0x0001304B
		public static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || ClientUtils.IsCriticalException(ex);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00014E60 File Offset: 0x00013060
		public static int GetBitCount(uint x)
		{
			int num = 0;
			while (x > 0U)
			{
				x &= x - 1U;
				num++;
			}
			return num;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00014E84 File Offset: 0x00013084
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
		{
			return value >= minValue && value <= maxValue;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00014EA4 File Offset: 0x000130A4
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue, int maxNumberOfBitsOn)
		{
			bool flag = value >= minValue && value <= maxValue;
			return flag && ClientUtils.GetBitCount((uint)value) <= maxNumberOfBitsOn;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00014ED8 File Offset: 0x000130D8
		public static bool IsEnumValid_Masked(Enum enumValue, int value, uint mask)
		{
			return ((long)value & (long)((ulong)mask)) == (long)value;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00014EF0 File Offset: 0x000130F0
		public static bool IsEnumValid_NotSequential(Enum enumValue, int value, params int[] enumValues)
		{
			for (int i = 0; i < enumValues.Length; i++)
			{
				if (enumValues[i] == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x020005FC RID: 1532
		internal class WeakRefCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060061A5 RID: 24997 RVA: 0x00168FC6 File Offset: 0x001671C6
			internal WeakRefCollection()
			{
				this._innerList = new ArrayList(4);
			}

			// Token: 0x060061A6 RID: 24998 RVA: 0x00168FE5 File Offset: 0x001671E5
			internal WeakRefCollection(int size)
			{
				this._innerList = new ArrayList(size);
			}

			// Token: 0x170014F9 RID: 5369
			// (get) Token: 0x060061A7 RID: 24999 RVA: 0x00169004 File Offset: 0x00167204
			internal ArrayList InnerList
			{
				get
				{
					return this._innerList;
				}
			}

			// Token: 0x170014FA RID: 5370
			// (get) Token: 0x060061A8 RID: 25000 RVA: 0x0016900C File Offset: 0x0016720C
			// (set) Token: 0x060061A9 RID: 25001 RVA: 0x00169014 File Offset: 0x00167214
			public int RefCheckThreshold
			{
				get
				{
					return this.refCheckThreshold;
				}
				set
				{
					this.refCheckThreshold = value;
				}
			}

			// Token: 0x170014FB RID: 5371
			public object this[int index]
			{
				get
				{
					ClientUtils.WeakRefCollection.WeakRefObject weakRefObject = this.InnerList[index] as ClientUtils.WeakRefCollection.WeakRefObject;
					if (weakRefObject != null && weakRefObject.IsAlive)
					{
						return weakRefObject.Target;
					}
					return null;
				}
				set
				{
					this.InnerList[index] = this.CreateWeakRefObject(value);
				}
			}

			// Token: 0x060061AC RID: 25004 RVA: 0x00169068 File Offset: 0x00167268
			public void ScavengeReferences()
			{
				int num = 0;
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (this[num] == null)
					{
						this.InnerList.RemoveAt(num);
					}
					else
					{
						num++;
					}
				}
			}

			// Token: 0x060061AD RID: 25005 RVA: 0x001690A8 File Offset: 0x001672A8
			public override bool Equals(object obj)
			{
				ClientUtils.WeakRefCollection weakRefCollection = obj as ClientUtils.WeakRefCollection;
				if (weakRefCollection == this)
				{
					return true;
				}
				if (weakRefCollection == null || this.Count != weakRefCollection.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this.InnerList[i] != weakRefCollection.InnerList[i] && (this.InnerList[i] == null || !this.InnerList[i].Equals(weakRefCollection.InnerList[i])))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060061AE RID: 25006 RVA: 0x0014D6AD File Offset: 0x0014B8AD
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x060061AF RID: 25007 RVA: 0x00169130 File Offset: 0x00167330
			private ClientUtils.WeakRefCollection.WeakRefObject CreateWeakRefObject(object value)
			{
				if (value == null)
				{
					return null;
				}
				return new ClientUtils.WeakRefCollection.WeakRefObject(value);
			}

			// Token: 0x060061B0 RID: 25008 RVA: 0x00169140 File Offset: 0x00167340
			private static void Copy(ClientUtils.WeakRefCollection sourceList, int sourceIndex, ClientUtils.WeakRefCollection destinationList, int destinationIndex, int length)
			{
				if (sourceIndex < destinationIndex)
				{
					sourceIndex += length;
					destinationIndex += length;
					while (length > 0)
					{
						destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
						length--;
					}
					return;
				}
				while (length > 0)
				{
					destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
					length--;
				}
			}

			// Token: 0x060061B1 RID: 25009 RVA: 0x001691BC File Offset: 0x001673BC
			public void RemoveByHashCode(object value)
			{
				if (value == null)
				{
					return;
				}
				int hashCode = value.GetHashCode();
				for (int i = 0; i < this.InnerList.Count; i++)
				{
					if (this.InnerList[i] != null && this.InnerList[i].GetHashCode() == hashCode)
					{
						this.RemoveAt(i);
						return;
					}
				}
			}

			// Token: 0x060061B2 RID: 25010 RVA: 0x00169214 File Offset: 0x00167414
			public void Clear()
			{
				this.InnerList.Clear();
			}

			// Token: 0x170014FC RID: 5372
			// (get) Token: 0x060061B3 RID: 25011 RVA: 0x00169221 File Offset: 0x00167421
			public bool IsFixedSize
			{
				get
				{
					return this.InnerList.IsFixedSize;
				}
			}

			// Token: 0x060061B4 RID: 25012 RVA: 0x0016922E File Offset: 0x0016742E
			public bool Contains(object value)
			{
				return this.InnerList.Contains(this.CreateWeakRefObject(value));
			}

			// Token: 0x060061B5 RID: 25013 RVA: 0x00169242 File Offset: 0x00167442
			public void RemoveAt(int index)
			{
				this.InnerList.RemoveAt(index);
			}

			// Token: 0x060061B6 RID: 25014 RVA: 0x00169250 File Offset: 0x00167450
			public void Remove(object value)
			{
				this.InnerList.Remove(this.CreateWeakRefObject(value));
			}

			// Token: 0x060061B7 RID: 25015 RVA: 0x00169264 File Offset: 0x00167464
			public int IndexOf(object value)
			{
				return this.InnerList.IndexOf(this.CreateWeakRefObject(value));
			}

			// Token: 0x060061B8 RID: 25016 RVA: 0x00169278 File Offset: 0x00167478
			public void Insert(int index, object value)
			{
				this.InnerList.Insert(index, this.CreateWeakRefObject(value));
			}

			// Token: 0x060061B9 RID: 25017 RVA: 0x0016928D File Offset: 0x0016748D
			public int Add(object value)
			{
				if (this.Count > this.RefCheckThreshold)
				{
					this.ScavengeReferences();
				}
				return this.InnerList.Add(this.CreateWeakRefObject(value));
			}

			// Token: 0x170014FD RID: 5373
			// (get) Token: 0x060061BA RID: 25018 RVA: 0x001692B5 File Offset: 0x001674B5
			public int Count
			{
				get
				{
					return this.InnerList.Count;
				}
			}

			// Token: 0x170014FE RID: 5374
			// (get) Token: 0x060061BB RID: 25019 RVA: 0x001692C2 File Offset: 0x001674C2
			object ICollection.SyncRoot
			{
				get
				{
					return this.InnerList.SyncRoot;
				}
			}

			// Token: 0x170014FF RID: 5375
			// (get) Token: 0x060061BC RID: 25020 RVA: 0x001692CF File Offset: 0x001674CF
			public bool IsReadOnly
			{
				get
				{
					return this.InnerList.IsReadOnly;
				}
			}

			// Token: 0x060061BD RID: 25021 RVA: 0x001692DC File Offset: 0x001674DC
			public void CopyTo(Array array, int index)
			{
				this.InnerList.CopyTo(array, index);
			}

			// Token: 0x17001500 RID: 5376
			// (get) Token: 0x060061BE RID: 25022 RVA: 0x001692EB File Offset: 0x001674EB
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.InnerList.IsSynchronized;
				}
			}

			// Token: 0x060061BF RID: 25023 RVA: 0x001692F8 File Offset: 0x001674F8
			public IEnumerator GetEnumerator()
			{
				return this.InnerList.GetEnumerator();
			}

			// Token: 0x0400389E RID: 14494
			private int refCheckThreshold = int.MaxValue;

			// Token: 0x0400389F RID: 14495
			private ArrayList _innerList;

			// Token: 0x020008B1 RID: 2225
			internal class WeakRefObject
			{
				// Token: 0x06007296 RID: 29334 RVA: 0x001A4156 File Offset: 0x001A2356
				internal WeakRefObject(object obj)
				{
					this.weakHolder = new WeakReference(obj);
					this.hash = obj.GetHashCode();
				}

				// Token: 0x17001920 RID: 6432
				// (get) Token: 0x06007297 RID: 29335 RVA: 0x001A4176 File Offset: 0x001A2376
				internal bool IsAlive
				{
					get
					{
						return this.weakHolder.IsAlive;
					}
				}

				// Token: 0x17001921 RID: 6433
				// (get) Token: 0x06007298 RID: 29336 RVA: 0x001A4183 File Offset: 0x001A2383
				internal object Target
				{
					get
					{
						return this.weakHolder.Target;
					}
				}

				// Token: 0x06007299 RID: 29337 RVA: 0x001A4190 File Offset: 0x001A2390
				public override int GetHashCode()
				{
					return this.hash;
				}

				// Token: 0x0600729A RID: 29338 RVA: 0x001A4198 File Offset: 0x001A2398
				public override bool Equals(object obj)
				{
					ClientUtils.WeakRefCollection.WeakRefObject weakRefObject = obj as ClientUtils.WeakRefCollection.WeakRefObject;
					return weakRefObject == this || (weakRefObject != null && (weakRefObject.Target == this.Target || (this.Target != null && this.Target.Equals(weakRefObject.Target))));
				}

				// Token: 0x04004523 RID: 17699
				private int hash;

				// Token: 0x04004524 RID: 17700
				private WeakReference weakHolder;
			}
		}
	}
}
