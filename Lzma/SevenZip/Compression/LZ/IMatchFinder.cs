using System;

namespace SevenZip.Compression.LZ
{
	// Token: 0x02000010 RID: 16
	internal interface IMatchFinder : IInWindowStream
	{
		// Token: 0x0600005A RID: 90
		void Create(uint historySize, uint keepAddBufferBefore, uint matchMaxLen, uint keepAddBufferAfter);

		// Token: 0x0600005B RID: 91
		uint GetMatches(uint[] distances);

		// Token: 0x0600005C RID: 92
		void Skip(uint num);
	}
}
