using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200024F RID: 591
	public class ConnectionEditableConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001593 RID: 5523 RVA: 0x00049D58 File Offset: 0x00047F58
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ConnectionEditable connectionEditable = obj as ConnectionEditable;
			if (connectionEditable.ToolsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "tools", connectionEditable.ToolsCollection.ItemsList, null);
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00049D90 File Offset: 0x00047F90
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ConnectionEditable)
				};
			}
		}
	}
}
