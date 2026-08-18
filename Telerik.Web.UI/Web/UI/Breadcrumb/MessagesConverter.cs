using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Breadcrumb
{
	// Token: 0x02000012 RID: 18
	public class MessagesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600010F RID: 271 RVA: 0x0000397C File Offset: 0x00001B7C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Messages messages = obj as Messages;
			ExplicitJavaScriptConverter.AddProperty(state, "rootTitle", messages.RootTitle, "Go to root");
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000039A8 File Offset: 0x00001BA8
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
