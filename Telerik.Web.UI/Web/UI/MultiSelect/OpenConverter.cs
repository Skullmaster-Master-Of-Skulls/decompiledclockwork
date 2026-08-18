using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x02000610 RID: 1552
	public class OpenConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600386E RID: 14446 RVA: 0x000B9A08 File Offset: 0x000B7C08
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Open open = obj as Open;
			ExplicitJavaScriptConverter.AddProperty(state, "effects", open.Effects, "");
			ExplicitJavaScriptConverter.AddProperty(state, "duration", open.Duration, 200.0);
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000B9A58 File Offset: 0x000B7C58
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Open)
				};
			}
		}
	}
}
