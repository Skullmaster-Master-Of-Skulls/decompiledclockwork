using System;

namespace System.Data.Common
{
	// Token: 0x020002FB RID: 763
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[Serializable]
	public sealed class DbProviderSpecificTypePropertyAttribute : Attribute
	{
		// Token: 0x0600308E RID: 12430 RVA: 0x0012EED0 File Offset: 0x0012E2D0
		public DbProviderSpecificTypePropertyAttribute(bool isProviderSpecificTypeProperty)
		{
			this._isProviderSpecificTypeProperty = isProviderSpecificTypeProperty;
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x0012EEEC File Offset: 0x0012E2EC
		public bool IsProviderSpecificTypeProperty
		{
			get
			{
				return this._isProviderSpecificTypeProperty;
			}
		}

		// Token: 0x04001D4F RID: 7503
		private bool _isProviderSpecificTypeProperty;
	}
}
