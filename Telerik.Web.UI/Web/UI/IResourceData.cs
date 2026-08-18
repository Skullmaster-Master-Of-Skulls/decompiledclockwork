using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001A09 RID: 6665
	public interface IResourceData
	{
		// Token: 0x17004DDC RID: 19932
		// (get) Token: 0x0601020C RID: 66060
		// (set) Token: 0x0601020D RID: 66061
		object Key { get; set; }

		// Token: 0x17004DDD RID: 19933
		// (get) Token: 0x0601020E RID: 66062
		// (set) Token: 0x0601020F RID: 66063
		string Text { get; set; }

		// Token: 0x17004DDE RID: 19934
		// (get) Token: 0x06010210 RID: 66064
		// (set) Token: 0x06010211 RID: 66065
		string Type { get; set; }

		// Token: 0x17004DDF RID: 19935
		// (get) Token: 0x06010212 RID: 66066
		// (set) Token: 0x06010213 RID: 66067
		bool Available { get; set; }

		// Token: 0x17004DE0 RID: 19936
		// (get) Token: 0x06010214 RID: 66068
		// (set) Token: 0x06010215 RID: 66069
		string EncodedKey { get; set; }

		// Token: 0x17004DE1 RID: 19937
		// (get) Token: 0x06010216 RID: 66070
		// (set) Token: 0x06010217 RID: 66071
		IDictionary<string, string> Attributes { get; set; }

		// Token: 0x06010218 RID: 66072
		void CopyFrom(Resource srcResource);

		// Token: 0x06010219 RID: 66073
		void CopyTo(Resource destResource);
	}
}
