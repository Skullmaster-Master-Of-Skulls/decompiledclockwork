using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x0200008D RID: 141
	public class UserConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			User user = obj as User;
			ExplicitJavaScriptConverter.AddProperty(state, "iconUrl", user.IconUrl, "");
			ExplicitJavaScriptConverter.AddProperty(state, "name", user.Name, "User");
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(User)
				};
			}
		}
	}
}
