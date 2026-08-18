using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.OrgChart.WebServiceBindings
{
	// Token: 0x02000C15 RID: 3093
	internal class OrgChartGroupItemServiceSettingsConverter : JavaScriptConverter
	{
		// Token: 0x060075DA RID: 30170 RVA: 0x001B65A3 File Offset: 0x001B47A3
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060075DB RID: 30171 RVA: 0x001B65AC File Offset: 0x001B47AC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			OrgChartGroupItemServiceSettings orgChartGroupItemServiceSettings = obj as OrgChartGroupItemServiceSettings;
			return new Dictionary<string, object>
			{
				{
					"path",
					WebServiceSettingsConverter.ResolveUrl(orgChartGroupItemServiceSettings.Path)
				},
				{
					"method",
					orgChartGroupItemServiceSettings.Method
				},
				{
					"useHttpGet",
					orgChartGroupItemServiceSettings.UseHttpGet
				}
			};
		}

		// Token: 0x17002657 RID: 9815
		// (get) Token: 0x060075DC RID: 30172 RVA: 0x001B66D0 File Offset: 0x001B48D0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(OrgChartGroupItemServiceSettings);
				yield break;
			}
		}
	}
}
