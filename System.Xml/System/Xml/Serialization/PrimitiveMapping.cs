using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C7 RID: 711
	internal class PrimitiveMapping : TypeMapping
	{
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x0009F2CC File Offset: 0x0009E2CC
		// (set) Token: 0x060021BD RID: 8637 RVA: 0x0009F2D4 File Offset: 0x0009E2D4
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

		// Token: 0x04001476 RID: 5238
		private bool isList;
	}
}
