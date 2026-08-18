using System;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x0200018B RID: 395
	internal class PropertyElement
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x0009DF44 File Offset: 0x0009C144
		protected internal PropertyElement(string n, string v)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.name = n;
			this.value_Renamed = v;
			this.next = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x040011C0 RID: 4544
		protected internal string name;

		// Token: 0x040011C1 RID: 4545
		protected internal string value_Renamed;

		// Token: 0x040011C2 RID: 4546
		protected internal PropertyElement next;
	}
}
