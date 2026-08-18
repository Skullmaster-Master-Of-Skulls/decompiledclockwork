using System;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001C2 RID: 450
	internal class OraXmlTypeHeader
	{
		// Token: 0x06001157 RID: 4439 RVA: 0x000BF9D8 File Offset: 0x000BDBD8
		internal bool HasSchema()
		{
			return (this.m_xmlFlag & 8U) != 0U;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000BF9E8 File Offset: 0x000BDBE8
		internal bool IsFragment()
		{
			return (this.m_xmlFlag & 32U) != 0U;
		}

		// Token: 0x040013AC RID: 5036
		internal TypeOfXmlType m_typeOfXmlType;

		// Token: 0x040013AD RID: 5037
		internal uint m_xmlFlag;

		// Token: 0x040013AE RID: 5038
		internal long m_dataLength;

		// Token: 0x040013AF RID: 5039
		internal int m_headerLength;

		// Token: 0x040013B0 RID: 5040
		internal byte[] m_schoid;

		// Token: 0x040013B1 RID: 5041
		internal byte[] m_schElem;

		// Token: 0x040013B2 RID: 5042
		internal byte[] m_snapshot;
	}
}
