using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000BE5 RID: 3045
	internal class OrgChartGroupItemJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x06007409 RID: 29705 RVA: 0x001B125C File Offset: 0x001AF45C
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x001B1264 File Offset: 0x001AF464
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			OrgChartGroupItem orgChartGroupItem = obj as OrgChartGroupItem;
			return new Dictionary<string, object>
			{
				{
					"Id",
					orgChartGroupItem.ID
				}
			};
		}

		// Token: 0x170025C2 RID: 9666
		// (get) Token: 0x0600740B RID: 29707 RVA: 0x001B135C File Offset: 0x001AF55C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(OrgChartGroupItem);
				yield break;
			}
		}
	}
}
