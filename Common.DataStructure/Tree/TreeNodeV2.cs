using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.Common.DataStructure.Tree
{
	// Token: 0x02000014 RID: 20
	[DataContract(Namespace = "http://tpro.ca")]
	public class TreeNodeV2<T>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000328D File Offset: 0x0000148D
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003295 File Offset: 0x00001495
		[DataMember]
		public T Value { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000329E File Offset: 0x0000149E
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000032A6 File Offset: 0x000014A6
		[DataMember]
		public IList<TreeNodeV2<T>> Nodes { get; set; }

		// Token: 0x0600007E RID: 126 RVA: 0x000032AF File Offset: 0x000014AF
		public TreeNodeV2()
		{
			this.Nodes = new List<TreeNodeV2<T>>();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000032C2 File Offset: 0x000014C2
		public TreeNodeV2(T value)
		{
			this.Nodes = new List<TreeNodeV2<T>>();
			this.Value = value;
		}
	}
}
