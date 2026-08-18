using System;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x02000016 RID: 22
	public class TreeNodeDataItemOrGroup<I, G> where I : class where G : class
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003345 File Offset: 0x00001545
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000334D File Offset: 0x0000154D
		public I Item { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003356 File Offset: 0x00001556
		// (set) Token: 0x0600008C RID: 140 RVA: 0x0000335E File Offset: 0x0000155E
		public G Group { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003367 File Offset: 0x00001567
		public string GroupTitle
		{
			get
			{
				if (this.Group != null)
				{
					return this.Group.ToString();
				}
				return "";
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000338C File Offset: 0x0000158C
		public bool IsGroup
		{
			get
			{
				return this.Item == null && this.Group != null;
			}
		}
	}
}
