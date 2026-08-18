using System;
using System.IO;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200028B RID: 651
	internal class OutputStream : StreamWriter
	{
		// Token: 0x06001959 RID: 6489 RVA: 0x00108FF4 File Offset: 0x001071F4
		public OutputStream(string str) : base(str)
		{
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00109000 File Offset: 0x00107200
		public void WriteObjectData(IStreamable obj)
		{
			obj.WriteToStream(this);
		}
	}
}
