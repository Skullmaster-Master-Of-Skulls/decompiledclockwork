using System;
using System.IO;
using TechnoPro.Common.DataFileIO.cs.Base;

namespace TechnoPro.Common.DataFileIO.cs.TabDelimited
{
	// Token: 0x02000003 RID: 3
	public class TabStream : BaseStream
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00003100 File Offset: 0x00001300
		public TabStream() : base(null, '\t', true)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000310E File Offset: 0x0000130E
		public TabStream(TextReader reader) : base(reader, '\t', true)
		{
		}
	}
}
