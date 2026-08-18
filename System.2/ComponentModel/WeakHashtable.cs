using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005C6 RID: 1478
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x0600374F RID: 14159 RVA: 0x000F04E8 File Offset: 0x000EE6E8
		internal WeakHashtable() : base(WeakHashtable._comparer)
		{
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000F04F5 File Offset: 0x000EE6F5
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000F04FD File Offset: 0x000EE6FD
		public override void Remove(object key)
		{
			base.Remove(key);
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000F0506 File Offset: 0x000EE706
		public void SetWeak(object key, object value)
		{
			this.ScavengeKeys();
			this[new WeakHashtable.EqualityWeakReference(key)] = value;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000F051C File Offset: 0x000EE71C
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

		// Token: 0x04002AEA RID: 10986
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x04002AEB RID: 10987
		private long _lastGlobalMem;

		// Token: 0x04002AEC RID: 10988
		private int _lastHashCount;

		// Token: 0x020008AB RID: 2219
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x06004602 RID: 17922 RVA: 0x0012485C File Offset: 0x00122A5C
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

			// Token: 0x06004603 RID: 17923 RVA: 0x001248C0 File Offset: 0x00122AC0
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x020008AC RID: 2220
		private sealed class EqualityWeakReference : WeakReference
		{
			// Token: 0x06004605 RID: 17925 RVA: 0x001248D0 File Offset: 0x00122AD0
			internal EqualityWeakReference(object o) : base(o)
			{
				this._hashCode = o.GetHashCode();
			}

			// Token: 0x06004606 RID: 17926 RVA: 0x001248E5 File Offset: 0x00122AE5
			public override bool Equals(object o)
			{
				return o != null && o.GetHashCode() == this._hashCode && (o == this || (this.IsAlive && o == this.Target));
			}

			// Token: 0x06004607 RID: 17927 RVA: 0x00124914 File Offset: 0x00122B14
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x04003804 RID: 14340
			private int _hashCode;
		}
	}
}
