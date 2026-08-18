using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200056B RID: 1387
	[Serializable]
	public sealed class SharedPersonalizationStateInfo : PersonalizationStateInfo
	{
		// Token: 0x06004660 RID: 18016 RVA: 0x000E7C31 File Offset: 0x000E5E31
		public SharedPersonalizationStateInfo(string path, DateTime lastUpdatedDate, int size, int sizeOfPersonalizations, int countOfPersonalizations) : base(path, lastUpdatedDate, size)
		{
			PersonalizationProviderHelper.CheckNegativeInteger(sizeOfPersonalizations, "sizeOfPersonalizations");
			PersonalizationProviderHelper.CheckNegativeInteger(countOfPersonalizations, "countOfPersonalizations");
			this._sizeOfPersonalizations = sizeOfPersonalizations;
			this._countOfPersonalizations = countOfPersonalizations;
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06004661 RID: 18017 RVA: 0x000E7C64 File Offset: 0x000E5E64
		public int SizeOfPersonalizations
		{
			get
			{
				return this._sizeOfPersonalizations;
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06004662 RID: 18018 RVA: 0x000E7C6C File Offset: 0x000E5E6C
		public int CountOfPersonalizations
		{
			get
			{
				return this._countOfPersonalizations;
			}
		}

		// Token: 0x0400269E RID: 9886
		private int _sizeOfPersonalizations;

		// Token: 0x0400269F RID: 9887
		private int _countOfPersonalizations;
	}
}
