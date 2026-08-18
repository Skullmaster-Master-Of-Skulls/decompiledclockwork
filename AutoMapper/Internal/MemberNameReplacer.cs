using System;

namespace AutoMapper.Internal
{
	// Token: 0x020000AF RID: 175
	public class MemberNameReplacer
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x00013BB8 File Offset: 0x00011DB8
		public MemberNameReplacer(string originalValue, string newValue)
		{
			this.OriginalValue = originalValue;
			this.NewValue = newValue;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00013BCE File Offset: 0x00011DCE
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00013BD6 File Offset: 0x00011DD6
		public string OriginalValue { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00013BDF File Offset: 0x00011DDF
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00013BE7 File Offset: 0x00011DE7
		public string NewValue { get; private set; }
	}
}
