using System;
using System.IO;
using System.Text;

namespace Ionic.Zip
{
	// Token: 0x02000039 RID: 57
	public class ReadOptions
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000FF1E File Offset: 0x0000E11E
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000FF26 File Offset: 0x0000E126
		public EventHandler<ReadProgressEventArgs> ReadProgress { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000FF2F File Offset: 0x0000E12F
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000FF37 File Offset: 0x0000E137
		public TextWriter StatusMessageWriter { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000FF40 File Offset: 0x0000E140
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000FF48 File Offset: 0x0000E148
		public Encoding Encoding { get; set; }
	}
}
