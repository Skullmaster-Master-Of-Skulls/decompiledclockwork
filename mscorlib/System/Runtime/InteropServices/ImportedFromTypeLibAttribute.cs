using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004E8 RID: 1256
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ImportedFromTypeLibAttribute : Attribute
	{
		// Token: 0x06003155 RID: 12629 RVA: 0x000A9110 File Offset: 0x000A8110
		public ImportedFromTypeLibAttribute(string tlbFile)
		{
			this._val = tlbFile;
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000A911F File Offset: 0x000A811F
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001901 RID: 6401
		internal string _val;
	}
}
