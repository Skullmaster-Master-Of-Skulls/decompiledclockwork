using System;
using System.IO;
using TechnoPro.Common.DataFileIO.cs.Base;

namespace TechnoPro.Common.DataFileIO.cs.Csv
{
	// Token: 0x02000009 RID: 9
	public class CsvStream : BaseStream
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00003F31 File Offset: 0x00002131
		public CsvStream() : base(null, ',', false)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003F3F File Offset: 0x0000213F
		public CsvStream(TextReader reader) : base(reader, ',', false)
		{
		}
	}
}
