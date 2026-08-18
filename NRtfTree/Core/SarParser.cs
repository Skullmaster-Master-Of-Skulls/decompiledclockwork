using System;

namespace Net.Sgoliver.NRtfTree.Core
{
	// Token: 0x02000018 RID: 24
	public abstract class SarParser
	{
		// Token: 0x0600013F RID: 319
		public abstract void StartRtfDocument();

		// Token: 0x06000140 RID: 320
		public abstract void EndRtfDocument();

		// Token: 0x06000141 RID: 321
		public abstract void StartRtfGroup();

		// Token: 0x06000142 RID: 322
		public abstract void EndRtfGroup();

		// Token: 0x06000143 RID: 323
		public abstract void RtfKeyword(string key, bool hasParameter, int parameter);

		// Token: 0x06000144 RID: 324
		public abstract void RtfControl(string key, bool hasParameter, int parameter);

		// Token: 0x06000145 RID: 325
		public abstract void RtfText(string text);
	}
}
