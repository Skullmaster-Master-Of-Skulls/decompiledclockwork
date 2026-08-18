using System;

namespace NLog.Config
{
	// Token: 0x02000041 RID: 65
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ArrayParameterAttribute : Attribute
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00004BD5 File Offset: 0x00002DD5
		public ArrayParameterAttribute(Type itemType, string elementName)
		{
			this.ItemType = itemType;
			this.ElementName = elementName;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004BEB File Offset: 0x00002DEB
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00004BF3 File Offset: 0x00002DF3
		public Type ItemType { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004BFC File Offset: 0x00002DFC
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00004C04 File Offset: 0x00002E04
		public string ElementName { get; private set; }
	}
}
