using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200027E RID: 638
	internal interface IStreamable
	{
		// Token: 0x0600190F RID: 6415
		int WriteToStream(OutputStream ostrm);

		// Token: 0x06001910 RID: 6416
		int ReadFromStream(InputStream istrm);
	}
}
