using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000BE6 RID: 3046
	internal class OrgChartNodeJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x0600740D RID: 29709 RVA: 0x001B1381 File Offset: 0x001AF581
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600740E RID: 29710 RVA: 0x001B1388 File Offset: 0x001AF588
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			OrgChartNode orgChartNode = obj as OrgChartNode;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Id", orgChartNode.ID);
			dictionary.Add("ColumnCount", orgChartNode.ColumnCount);
			if (orgChartNode.GroupItems.Count > 0)
			{
				dictionary.Add("groupItems", orgChartNode.GroupItems);
			}
			if (orgChartNode.Nodes.Count > 0)
			{
				dictionary.Add("nodes", orgChartNode.Nodes);
			}
			return dictionary;
		}

		// Token: 0x170025C3 RID: 9667
		// (get) Token: 0x0600740F RID: 29711 RVA: 0x001B14D4 File Offset: 0x001AF6D4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(OrgChartNode);
				yield break;
			}
		}
	}
}
