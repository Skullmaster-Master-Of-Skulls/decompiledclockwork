using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x020016AF RID: 5807
	internal class AsyncUploadClientStateConverter : JavaScriptConverter
	{
		// Token: 0x0600E03C RID: 57404 RVA: 0x0031DEA8 File Offset: 0x0031C0A8
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			ArrayList arrayList = (ArrayList)dictionary["uploadedFiles"];
			List<UploadedFileInfo> list = new List<UploadedFileInfo>();
			bool isEnabled = bool.Parse((string)dictionary["isEnabled"]);
			foreach (object obj in arrayList)
			{
				IDictionary<string, object> dictionary2 = (IDictionary<string, object>)obj;
				UploadedFileInfo uploadedFileInfo = new UploadedFileInfo();
				IDictionary<string, object> dictionary3 = (IDictionary<string, object>)dictionary2["fileInfo"];
				MetaData metaData = serializer.Deserialize<MetaData>(CryptoService.GetService("").Decrypt((string)dictionary2["metaData"]));
				uploadedFileInfo.ContentLength = Convert.ToInt64(dictionary3["ContentLength"]);
				uploadedFileInfo.ContentType = (string)dictionary3["ContentType"];
				uploadedFileInfo.FileName = (string)dictionary3["FileName"];
				uploadedFileInfo.Index = (int)dictionary3["Index"];
				if (dictionary3.ContainsKey("DateJson") && dictionary3["DateJson"] != null)
				{
					DateTime lastModifiedDate;
					DateTime.TryParse(dictionary3["DateJson"].ToString(), out lastModifiedDate);
					uploadedFileInfo.LastModifiedDate = lastModifiedDate;
				}
				uploadedFileInfo.FileType = metaData.AsyncUploadTypeName;
				uploadedFileInfo.TempFileName = metaData.TempFileName;
				uploadedFileInfo.SerializedData = this.GetJsonFromDictionary(dictionary3);
				list.Add(uploadedFileInfo);
			}
			return new RadAsyncUploadClientState
			{
				UploadedFiles = list.ToArray(),
				IsEnabled = isEnabled
			};
		}

		// Token: 0x0600E03D RID: 57405 RVA: 0x0031E06C File Offset: 0x0031C26C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			return null;
		}

		// Token: 0x170044B6 RID: 17590
		// (get) Token: 0x0600E03E RID: 57406 RVA: 0x0031E070 File Offset: 0x0031C270
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadAsyncUploadClientState)
				};
			}
		}

		// Token: 0x0600E03F RID: 57407 RVA: 0x0031E094 File Offset: 0x0031C294
		private string GetJsonFromDictionary(IDictionary<string, object> dict)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			foreach (KeyValuePair<string, object> keyValuePair in dict)
			{
				stringBuilder.Append("\"" + keyValuePair.Key + "\"");
				stringBuilder.Append(":\"" + keyValuePair.Value + "\",");
			}
			stringBuilder.Append("}");
			int startIndex = stringBuilder.Length - 2;
			stringBuilder.Remove(startIndex, 1);
			return stringBuilder.ToString();
		}
	}
}
