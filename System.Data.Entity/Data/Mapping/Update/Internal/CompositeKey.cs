using System;
using System.Collections.Generic;
using System.Data.Common.Utils;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002C6 RID: 710
	internal class CompositeKey
	{
		// Token: 0x060029EC RID: 10732 RVA: 0x000A3F56 File Offset: 0x000A2156
		internal CompositeKey(PropagatorResult[] constants)
		{
			this.KeyComponents = constants;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000A3F65 File Offset: 0x000A2165
		internal static IEqualityComparer<CompositeKey> CreateComparer(KeyManager keyManager)
		{
			return new CompositeKey.CompositeKeyComparer(keyManager);
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000A3F70 File Offset: 0x000A2170
		internal CompositeKey Merge(KeyManager keyManager, CompositeKey other)
		{
			PropagatorResult[] array = new PropagatorResult[this.KeyComponents.Length];
			for (int i = 0; i < this.KeyComponents.Length; i++)
			{
				array[i] = this.KeyComponents[i].Merge(keyManager, other.KeyComponents[i]);
			}
			return new CompositeKey(array);
		}

		// Token: 0x040012B1 RID: 4785
		internal readonly PropagatorResult[] KeyComponents;

		// Token: 0x0200061B RID: 1563
		private class CompositeKeyComparer : IEqualityComparer<CompositeKey>
		{
			// Token: 0x060042B8 RID: 17080 RVA: 0x000F25EC File Offset: 0x000F07EC
			internal CompositeKeyComparer(KeyManager manager)
			{
				this._manager = EntityUtil.CheckArgumentNull<KeyManager>(manager, "manager");
			}

			// Token: 0x060042B9 RID: 17081 RVA: 0x000F2608 File Offset: 0x000F0808
			public bool Equals(CompositeKey left, CompositeKey right)
			{
				if (left == right)
				{
					return true;
				}
				if (left == null || right == null)
				{
					return false;
				}
				if (left.KeyComponents.Length != right.KeyComponents.Length)
				{
					return false;
				}
				for (int i = 0; i < left.KeyComponents.Length; i++)
				{
					PropagatorResult propagatorResult = left.KeyComponents[i];
					PropagatorResult propagatorResult2 = right.KeyComponents[i];
					if (propagatorResult.Identifier != -1)
					{
						if (propagatorResult2.Identifier == -1 || this._manager.GetCliqueIdentifier(propagatorResult.Identifier) != this._manager.GetCliqueIdentifier(propagatorResult2.Identifier))
						{
							return false;
						}
					}
					else if (propagatorResult2.Identifier != -1 || !ByValueEqualityComparer.Default.Equals(propagatorResult.GetSimpleValue(), propagatorResult2.GetSimpleValue()))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060042BA RID: 17082 RVA: 0x000F26B8 File Offset: 0x000F08B8
			public int GetHashCode(CompositeKey key)
			{
				EntityUtil.CheckArgumentNull<CompositeKey>(key, "key");
				int num = 0;
				foreach (PropagatorResult keyComponent in key.KeyComponents)
				{
					num = (num << 5 ^ this.GetComponentHashCode(keyComponent));
				}
				return num;
			}

			// Token: 0x060042BB RID: 17083 RVA: 0x000F26FC File Offset: 0x000F08FC
			private int GetComponentHashCode(PropagatorResult keyComponent)
			{
				if (keyComponent.Identifier == -1)
				{
					return ByValueEqualityComparer.Default.GetHashCode(keyComponent.GetSimpleValue());
				}
				return this._manager.GetCliqueIdentifier(keyComponent.Identifier).GetHashCode();
			}

			// Token: 0x04001E4A RID: 7754
			private readonly KeyManager _manager;
		}
	}
}
