using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C8 RID: 712
	internal class NullableMapping : TypeMapping
	{
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0009F2E5 File Offset: 0x0009E2E5
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x0009F2ED File Offset: 0x0009E2ED
		internal TypeMapping BaseMapping
		{
			get
			{
				return this.baseMapping;
			}
			set
			{
				this.baseMapping = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0009F2F6 File Offset: 0x0009E2F6
		internal override string DefaultElementName
		{
			get
			{
				return this.BaseMapping.DefaultElementName;
			}
		}

		// Token: 0x04001477 RID: 5239
		private TypeMapping baseMapping;
	}
}
