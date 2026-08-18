using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200014E RID: 334
	internal class PrimitiveMapping : TypeMapping
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x000675AE File Offset: 0x000657AE
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x000675B6 File Offset: 0x000657B6
		internal override bool IsList
		{
			get
			{
				return this.isList;
			}
			set
			{
				this.isList = value;
			}
		}

		// Token: 0x04000ADC RID: 2780
		private bool isList;
	}
}
