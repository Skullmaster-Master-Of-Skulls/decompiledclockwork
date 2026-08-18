using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003EB RID: 1003
	internal class CompositeKey
	{
		// Token: 0x0600250D RID: 9485 RVA: 0x000AED42 File Offset: 0x000ACF42
		internal CompositeKey(PropagatorResult[] constants)
		{
			this.KeyComponents = constants;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000AED51 File Offset: 0x000ACF51
		internal static IEqualityComparer<CompositeKey> CreateComparer(KeyManager keyManager)
		{
			return new CompositeKey.CompositeKeyComparer(keyManager);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000AED5C File Offset: 0x000ACF5C
		internal CompositeKey Merge(KeyManager keyManager, CompositeKey other)
		{
			PropagatorResult[] array = new PropagatorResult[this.KeyComponents.Length];
			for (int i = 0; i < this.KeyComponents.Length; i++)
			{
				array[i] = this.KeyComponents[i].Merge(keyManager, other.KeyComponents[i]);
			}
			return new CompositeKey(array);
		}

		// Token: 0x04000DC0 RID: 3520
		internal readonly PropagatorResult[] KeyComponents;

		// Token: 0x020003EC RID: 1004
		private class CompositeKeyComparer : IEqualityComparer<CompositeKey>
		{
			// Token: 0x06002510 RID: 9488 RVA: 0x000AEDA9 File Offset: 0x000ACFA9
			internal CompositeKeyComparer(KeyManager manager)
			{
				this._manager = manager;
			}

			// Token: 0x06002511 RID: 9489 RVA: 0x000AEDB8 File Offset: 0x000ACFB8
			public bool Equals(CompositeKey left, CompositeKey right)
			{
				if (object.ReferenceEquals(left, right))
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

			// Token: 0x06002512 RID: 9490 RVA: 0x000AEE70 File Offset: 0x000AD070
			public int GetHashCode(CompositeKey key)
			{
				int num = 0;
				foreach (PropagatorResult keyComponent in key.KeyComponents)
				{
					num = (num << 5 ^ this.GetComponentHashCode(keyComponent));
				}
				return num;
			}

			// Token: 0x06002513 RID: 9491 RVA: 0x000AEEA8 File Offset: 0x000AD0A8
			private int GetComponentHashCode(PropagatorResult keyComponent)
			{
				if (keyComponent.Identifier == -1)
				{
					return ByValueEqualityComparer.Default.GetHashCode(keyComponent.GetSimpleValue());
				}
				return this._manager.GetCliqueIdentifier(keyComponent.Identifier).GetHashCode();
			}

			// Token: 0x04000DC1 RID: 3521
			private readonly KeyManager _manager;
		}
	}
}
