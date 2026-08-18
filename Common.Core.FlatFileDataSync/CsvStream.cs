using System;
using System.IO;

namespace TechnoPro.Common.Core.FlatFileDataSync
{
	// Token: 0x02000003 RID: 3
	public class CsvStream : BaseStream
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002DF6 File Offset: 0x00000FF6
		public CsvStream() : base(null, ',', false)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002E02 File Offset: 0x00001002
		public CsvStream(TextReader reader) : base(reader, ',', false)
		{
		}
	}
}
