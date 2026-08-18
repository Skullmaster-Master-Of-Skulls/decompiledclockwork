using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Grid
{
	// Token: 0x020011A3 RID: 4515
	internal class GridGroupPanelJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x0600B99B RID: 47515 RVA: 0x002925D0 File Offset: 0x002907D0
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600B99C RID: 47516 RVA: 0x002925D8 File Offset: 0x002907D8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			GroupItemCell groupItemCell = obj as GroupItemCell;
			dictionary.Add("HierarchicalIndex", groupItemCell.HierarchicalIndex);
			dictionary.Add("DataField", groupItemCell.DataField);
			return dictionary;
		}

		// Token: 0x17003BF1 RID: 15345
		// (get) Token: 0x0600B99D RID: 47517 RVA: 0x00292618 File Offset: 0x00290818
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(GroupItemCell)
				};
			}
		}
	}
}
