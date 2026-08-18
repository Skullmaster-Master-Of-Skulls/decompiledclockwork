using System;
using System.IO;
using TechnoPro.Common.DataFileIO.cs.Base;

namespace TechnoPro.Common.DataFileIO.cs.CharDelimited
{
	// Token: 0x0200000B RID: 11
	public class CharStream : BaseStream
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00003100 File Offset: 0x00001300
		public CharStream() : base(null, '\t', true)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000043FC File Offset: 0x000025FC
		public CharStream(char delimiter, TextReader reader) : base(reader, delimiter, true)
		{
		}
	}
}
