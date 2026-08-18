using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduler.OData
{
	// Token: 0x02000E66 RID: 3686
	internal class ODataWebServiceClient : SchedulerWebServiceClient
	{
		// Token: 0x06008BD5 RID: 35797 RVA: 0x001FCA7C File Offset: 0x001FAC7C
		public ODataWebServiceClient(RadScheduler owner) : base(owner)
		{
		}

		// Token: 0x06008BD6 RID: 35798 RVA: 0x001FCA88 File Offset: 0x001FAC88
		protected override List<Resource> LoadResources(WebClient client, ResourcesPopulatingEventArgs args)
		{
			string arg = client.BaseAddress.TrimEnd(new char[]
			{
				'/'
			});
			List<Resource> list = new List<Resource>();
			foreach (ODataResourceType odataResourceType in base.Settings.ODataSettings.ResourceTypes)
			{
				string address = string.Format("{0}/{1}?{2}", arg, odataResourceType.Container, "$format=json");
				string response = client.DownloadString(address);
				List<ResourceData> list2 = ODataWebServiceClient.DeserializeResponse(odataResourceType, response);
				list.AddRange(base.ParseResourceData(list2.ToArray()));
			}
			return list;
		}

		// Token: 0x06008BD7 RID: 35799 RVA: 0x001FCB40 File Offset: 0x001FAD40
		private static List<ResourceData> DeserializeResponse(ODataResourceType resource, string response)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new ODataResourceConverter(resource)
			});
			return javaScriptSerializer.Deserialize<List<ResourceData>>(response);
		}

		// Token: 0x04002727 RID: 10023
		public const string Format_Query_String_Param = "$format=json";
	}
}
