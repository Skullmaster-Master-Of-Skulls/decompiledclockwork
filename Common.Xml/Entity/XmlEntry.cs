using System;

namespace TechnoPro.Common.Xml.Entity
{
	// Token: 0x02000004 RID: 4
	public class XmlEntry
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002BE7 File Offset: 0x00000DE7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002BEF File Offset: 0x00000DEF
		public string Name { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002BF8 File Offset: 0x00000DF8
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002C00 File Offset: 0x00000E00
		public string Value { get; set; }

		// Token: 0x06000013 RID: 19 RVA: 0x00002C09 File Offset: 0x00000E09
		public override string ToString()
		{
			return string.Format("XmlEntry: Name={0}:Value={1}", this.Name ?? "", this.Value ?? "NULL");
		}
	}
}
