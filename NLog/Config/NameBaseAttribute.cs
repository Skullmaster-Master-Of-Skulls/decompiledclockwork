using System;

namespace NLog.Config
{
	// Token: 0x02000031 RID: 49
	public abstract class NameBaseAttribute : Attribute
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x000035F9 File Offset: 0x000017F9
		protected NameBaseAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003608 File Offset: 0x00001808
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003610 File Offset: 0x00001810
		public string Name { get; private set; }
	}
}
