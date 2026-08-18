using System;

namespace log4net.Util
{
	// Token: 0x02000112 RID: 274
	public class PropertyEntry
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00018F17 File Offset: 0x00017117
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00018F1F File Offset: 0x0001711F
		public string Key
		{
			get
			{
				return this.m_key;
			}
			set
			{
				this.m_key = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00018F28 File Offset: 0x00017128
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x00018F30 File Offset: 0x00017130
		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00018F3C File Offset: 0x0001713C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"PropertyEntry(Key=",
				this.m_key,
				", Value=",
				this.m_value,
				")"
			});
		}

		// Token: 0x040002EE RID: 750
		private string m_key;

		// Token: 0x040002EF RID: 751
		private object m_value;
	}
}
