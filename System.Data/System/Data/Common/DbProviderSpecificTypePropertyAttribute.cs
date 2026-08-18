using System;

namespace System.Data.Common
{
	// Token: 0x02000143 RID: 323
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[Serializable]
	public sealed class DbProviderSpecificTypePropertyAttribute : Attribute
	{
		// Token: 0x060014EC RID: 5356 RVA: 0x00241C68 File Offset: 0x00241068
		public DbProviderSpecificTypePropertyAttribute(bool isProviderSpecificTypeProperty)
		{
			this._isProviderSpecificTypeProperty = isProviderSpecificTypeProperty;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00241C88 File Offset: 0x00241088
		public bool IsProviderSpecificTypeProperty
		{
			get
			{
				return this._isProviderSpecificTypeProperty;
			}
		}

		// Token: 0x04000C68 RID: 3176
		private bool _isProviderSpecificTypeProperty;
	}
}
