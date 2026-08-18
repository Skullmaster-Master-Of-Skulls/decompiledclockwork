using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020007A6 RID: 1958
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x06003C3F RID: 15423 RVA: 0x0010163B File Offset: 0x0010063B
		internal WeakHashtable() : base(WeakHashtable._comparer)
		{
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x00101648 File Offset: 0x00100648
		public void SetWeak(object key, object value)
		{
			this.ScavengeKeys();
			this[new WeakHashtable.EqualityWeakReference(key)] = value;
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x00101660 File Offset: 0x00100660
		private void ScavengeKeys()
		{
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			if (this._lastHashCount == 0)
			{
				this._lastHashCount = count;
				return;
			}
			long totalMemory = GC.GetTotalMemory(false);
			if (this._lastGlobalMem == 0L)
			{
				this._lastGlobalMem = totalMemory;
				return;
			}
			float num = (float)(totalMemory - this._lastGlobalMem) / (float)this._lastGlobalMem;
			float num2 = (float)(count - this._lastHashCount) / (float)this._lastHashCount;
			if (num < 0f && num2 >= 0f)
			{
				ArrayList arrayList = null;
				foreach (object obj in this.Keys)
				{
					WeakReference weakReference = obj as WeakReference;
					if (weakReference != null && !weakReference.IsAlive)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(weakReference);
					}
				}
				if (arrayList != null)
				{
					foreach (object key in arrayList)
					{
						this.Remove(key);
					}
				}
			}
			this._lastGlobalMem = totalMemory;
			this._lastHashCount = count;
		}

		// Token: 0x04003522 RID: 13602
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x04003523 RID: 13603
		private long _lastGlobalMem;

		// Token: 0x04003524 RID: 13604
		private int _lastHashCount;

		// Token: 0x020007A7 RID: 1959
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x06003C43 RID: 15427 RVA: 0x001017B8 File Offset: 0x001007B8
			bool IEqualityComparer.Equals(object x, object y)
			{
				if (object.ReferenceEquals(x, y))
				{
					return true;
				}
				if (x == null || y == null)
				{
					return false;
				}
				if (x.GetHashCode() == y.GetHashCode())
				{
					WeakReference weakReference = x as WeakReference;
					WeakReference weakReference2 = y as WeakReference;
					if (weakReference != null)
					{
						if (!weakReference.IsAlive)
						{
							return false;
						}
						x = weakReference.Target;
					}
					if (weakReference2 != null)
					{
						if (!weakReference2.IsAlive)
						{
							return false;
						}
						y = weakReference2.Target;
					}
					return object.ReferenceEquals(x, y);
				}
				return false;
			}

			// Token: 0x06003C44 RID: 15428 RVA: 0x00101827 File Offset: 0x00100827
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x020007A8 RID: 1960
		private sealed class EqualityWeakReference : WeakReference
		{
			// Token: 0x06003C46 RID: 15430 RVA: 0x00101837 File Offset: 0x00100837
			internal EqualityWeakReference(object o) : base(o)
			{
				this._hashCode = o.GetHashCode();
			}

			// Token: 0x06003C47 RID: 15431 RVA: 0x0010184C File Offset: 0x0010084C
			public override bool Equals(object o)
			{
				return o != null && o.GetHashCode() == this._hashCode && (o == this || (this.IsAlive && object.ReferenceEquals(o, this.Target)));
			}

			// Token: 0x06003C48 RID: 15432 RVA: 0x00101880 File Offset: 0x00100880
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x04003525 RID: 13605
			private int _hashCode;
		}
	}
}
