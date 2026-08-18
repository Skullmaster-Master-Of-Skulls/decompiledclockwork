using System;
using System.IO;
using System.Resources;

namespace System.Reflection.Emit
{
	// Token: 0x02000802 RID: 2050
	internal class ResWriterData
	{
		// Token: 0x060048B9 RID: 18617 RVA: 0x000FD57D File Offset: 0x000FC57D
		internal ResWriterData(ResourceWriter resWriter, Stream memoryStream, string strName, string strFileName, string strFullFileName, ResourceAttributes attribute)
		{
			this.m_resWriter = resWriter;
			this.m_memoryStream = memoryStream;
			this.m_strName = strName;
			this.m_strFileName = strFileName;
			this.m_strFullFileName = strFullFileName;
			this.m_nextResWriter = null;
			this.m_attribute = attribute;
		}

		// Token: 0x04002576 RID: 9590
		internal ResourceWriter m_resWriter;

		// Token: 0x04002577 RID: 9591
		internal string m_strName;

		// Token: 0x04002578 RID: 9592
		internal string m_strFileName;

		// Token: 0x04002579 RID: 9593
		internal string m_strFullFileName;

		// Token: 0x0400257A RID: 9594
		internal Stream m_memoryStream;

		// Token: 0x0400257B RID: 9595
		internal ResWriterData m_nextResWriter;

		// Token: 0x0400257C RID: 9596
		internal ResourceAttributes m_attribute;
	}
}
