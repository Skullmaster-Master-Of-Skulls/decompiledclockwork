using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001089 RID: 4233
	internal class EditorLinkConverter : EditorConverterBase
	{
		// Token: 0x1700369E RID: 13982
		// (get) Token: 0x0600AA20 RID: 43552 RVA: 0x0024E354 File Offset: 0x0024C554
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(EditorLink)
				};
			}
		}
	}
}
