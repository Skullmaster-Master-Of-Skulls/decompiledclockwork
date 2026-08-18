using System;
using System.Collections.Generic;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200008D RID: 141
	public class PerThreadLifetimeManager : LifetimeManager
	{
		// Token: 0x06000295 RID: 661 RVA: 0x000085C5 File Offset: 0x000067C5
		public PerThreadLifetimeManager()
		{
			this.key = Guid.NewGuid();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000085D8 File Offset: 0x000067D8
		public override object GetValue()
		{
			PerThreadLifetimeManager.EnsureValues();
			object result;
			PerThreadLifetimeManager.values.TryGetValue(this.key, out result);
			return result;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000085FE File Offset: 0x000067FE
		public override void SetValue(object newValue)
		{
			PerThreadLifetimeManager.EnsureValues();
			PerThreadLifetimeManager.values[this.key] = newValue;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008616 File Offset: 0x00006816
		public override void RemoveValue()
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00008618 File Offset: 0x00006818
		private static void EnsureValues()
		{
			if (PerThreadLifetimeManager.values == null)
			{
				PerThreadLifetimeManager.values = new Dictionary<Guid, object>();
			}
		}

		// Token: 0x0400008F RID: 143
		[ThreadStatic]
		private static Dictionary<Guid, object> values;

		// Token: 0x04000090 RID: 144
		private readonly Guid key;
	}
}
