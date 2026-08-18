using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x0200060E RID: 1550
	public class MessagesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003865 RID: 14437 RVA: 0x000B98CC File Offset: 0x000B7ACC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Messages messages = obj as Messages;
			ExplicitJavaScriptConverter.AddProperty(state, "clear", messages.Clear, "clear");
			ExplicitJavaScriptConverter.AddProperty(state, "deleteTag", messages.DeleteTag, "delete");
			ExplicitJavaScriptConverter.AddProperty(state, "noData", messages.NoData, "No data found.");
			ExplicitJavaScriptConverter.AddProperty(state, "singleTag", messages.SingleTag, "items selected");
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000B9938 File Offset: 0x000B7B38
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Messages)
				};
			}
		}
	}
}
