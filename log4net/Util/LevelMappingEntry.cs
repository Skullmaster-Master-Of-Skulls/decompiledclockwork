using System;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x0200000D RID: 13
	public abstract class LevelMappingEntry : IOptionHandler
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003333 File Offset: 0x00001533
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000333B File Offset: 0x0000153B
		public Level Level
		{
			get
			{
				return this.m_level;
			}
			set
			{
				this.m_level = value;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003344 File Offset: 0x00001544
		public virtual void ActivateOptions()
		{
		}

		// Token: 0x0400003F RID: 63
		private Level m_level;
	}
}
