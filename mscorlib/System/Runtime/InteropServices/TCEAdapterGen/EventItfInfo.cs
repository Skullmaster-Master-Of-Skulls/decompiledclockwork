using System;
using System.Reflection;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020008F5 RID: 2293
	internal class EventItfInfo
	{
		// Token: 0x06005326 RID: 21286 RVA: 0x0012CB7D File Offset: 0x0012BB7D
		public EventItfInfo(string strEventItfName, string strSrcItfName, string strEventProviderName, Assembly asmImport, Assembly asmSrcItf)
		{
			this.m_strEventItfName = strEventItfName;
			this.m_strSrcItfName = strSrcItfName;
			this.m_strEventProviderName = strEventProviderName;
			this.m_asmImport = asmImport;
			this.m_asmSrcItf = asmSrcItf;
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x0012CBAC File Offset: 0x0012BBAC
		public Type GetEventItfType()
		{
			Type type = this.m_asmImport.GetType(this.m_strEventItfName, true, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			return type;
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x0012CBDC File Offset: 0x0012BBDC
		public Type GetSrcItfType()
		{
			Type type = this.m_asmSrcItf.GetType(this.m_strSrcItfName, true, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			return type;
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x0012CC0B File Offset: 0x0012BC0B
		public string GetEventProviderName()
		{
			return this.m_strEventProviderName;
		}

		// Token: 0x04002B09 RID: 11017
		private string m_strEventItfName;

		// Token: 0x04002B0A RID: 11018
		private string m_strSrcItfName;

		// Token: 0x04002B0B RID: 11019
		private string m_strEventProviderName;

		// Token: 0x04002B0C RID: 11020
		private Assembly m_asmImport;

		// Token: 0x04002B0D RID: 11021
		private Assembly m_asmSrcItf;
	}
}
