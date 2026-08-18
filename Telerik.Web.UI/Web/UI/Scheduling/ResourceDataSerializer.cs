using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001A2A RID: 6698
	internal class ResourceDataSerializer
	{
		// Token: 0x06010429 RID: 66601 RVA: 0x003A24C4 File Offset: 0x003A06C4
		public string Serialize(ISchedulerInfo obj)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(obj.GetType(), new Type[]
			{
				typeof(SchedulerInfo)
			});
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, obj);
				@string = Encoding.Default.GetString(memoryStream.ToArray());
			}
			return Regex.Replace(@string, "\"__type.*?,", "");
		}

		// Token: 0x0601042A RID: 66602 RVA: 0x003A2540 File Offset: 0x003A0740
		public ResourceData[] Deserialize(string json)
		{
			if (json.StartsWith("{\"d\":"))
			{
				int length = json.Length - "{\"d\":".Length - 1;
				json = json.Substring("{\"d\":".Length, length);
			}
			json = Regex.Replace(json, "Telerik\\.Web\\.UI\\.ResourceData", "ResourceData:#Telerik.Web.UI");
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(ResourceData[]), new Type[]
			{
				typeof(ResourceData)
			});
			ResourceData[] result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
			{
				result = (dataContractJsonSerializer.ReadObject(memoryStream) as ResourceData[]);
			}
			return result;
		}
	}
}
