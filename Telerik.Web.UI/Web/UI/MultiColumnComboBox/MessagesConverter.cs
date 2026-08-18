using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x02000058 RID: 88
	public class MessagesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000737C File Offset: 0x0000557C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Messages messages = obj as Messages;
			ExplicitJavaScriptConverter.AddProperty(state, "clear", messages.Clear, "clear");
			ExplicitJavaScriptConverter.AddProperty(state, "noData", messages.NoData, "No data found.");
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000073BC File Offset: 0x000055BC
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
