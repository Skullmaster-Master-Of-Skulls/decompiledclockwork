using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.OrgChart.WebServiceBindings
{
	// Token: 0x02000BE7 RID: 3047
	internal class OrgChartNodeServiceSettingsConverter : JavaScriptConverter
	{
		// Token: 0x06007411 RID: 29713 RVA: 0x001B14F9 File Offset: 0x001AF6F9
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007412 RID: 29714 RVA: 0x001B1500 File Offset: 0x001AF700
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			OrgChartNodeServiceSettings orgChartNodeServiceSettings = obj as OrgChartNodeServiceSettings;
			return new Dictionary<string, object>
			{
				{
					"path",
					WebServiceSettingsConverter.ResolveUrl(orgChartNodeServiceSettings.Path)
				},
				{
					"method",
					orgChartNodeServiceSettings.Method
				},
				{
					"useHttpGet",
					orgChartNodeServiceSettings.UseHttpGet
				}
			};
		}

		// Token: 0x170025C4 RID: 9668
		// (get) Token: 0x06007413 RID: 29715 RVA: 0x001B1624 File Offset: 0x001AF824
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(OrgChartNodeServiceSettings);
				yield break;
			}
		}
	}
}
