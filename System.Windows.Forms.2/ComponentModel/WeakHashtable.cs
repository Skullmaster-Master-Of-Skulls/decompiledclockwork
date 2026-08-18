using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020000FA RID: 250
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x0000C541 File Offset: 0x0000A741
		internal WeakHashtable() : base(WeakHashtable._comparer)
		{
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000C54E File Offset: 0x0000A74E
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000C556 File Offset: 0x0000A756
		public override void Remove(object key)
		{
			base.Remove(key);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000C55F File Offset: 0x0000A75F
		public void SetWeak(object key, object value)
		{
			this.ScavengeKeys();
			this[new WeakHashtable.EqualityWeakReference(key)] = value;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000C574 File Offset: 0x0000A774
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

		// Token: 0x04000434 RID: 1076
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x04000435 RID: 1077
		private long _lastGlobalMem;

		// Token: 0x04000436 RID: 1078
		private int _lastHashCount;

		// Token: 0x02000545 RID: 1349
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x06005569 RID: 21865 RVA: 0x00166448 File Offset: 0x00164648
			bool IEqualityComparer.Equals(object x, object y)
			{
				if (x == null)
				{
					return y == null;
				}
				if (y != null && x.GetHashCode() == y.GetHashCode())
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
					return x == y;
				}
				return false;
			}

			// Token: 0x0600556A RID: 21866 RVA: 0x001664AC File Offset: 0x001646AC
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x02000546 RID: 1350
		private sealed class EqualityWeakReference : WeakReference
		{
			// Token: 0x0600556C RID: 21868 RVA: 0x001664B4 File Offset: 0x001646B4
			internal EqualityWeakReference(object o) : base(o)
			{
				this._hashCode = o.GetHashCode();
			}

			// Token: 0x0600556D RID: 21869 RVA: 0x001664C9 File Offset: 0x001646C9
			public override bool Equals(object o)
			{
				return o != null && o.GetHashCode() == this._hashCode && (o == this || (this.IsAlive && o == this.Target));
			}

			// Token: 0x0600556E RID: 21870 RVA: 0x001664F8 File Offset: 0x001646F8
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x0400380D RID: 14349
			private int _hashCode;
		}
	}
}
