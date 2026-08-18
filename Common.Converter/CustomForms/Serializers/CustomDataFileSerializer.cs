using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms.Serializers
{
	// Token: 0x02000009 RID: 9
	public class CustomDataFileSerializer : ICustomDataSerializer<CustomDataFile>
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002DAC File Offset: 0x00000FAC
		public CustomDataSerialized Serialize(CustomDataFile dataObj)
		{
			bool flag = this.IsValueEmptyForStorage(dataObj);
			CustomDataSerialized result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = new XDocument(new object[]
				{
					new XElement(eCustomDataPrimitiveType.File.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag, new object[]
					{
						new XAttribute("id", dataObj.DataInstanceId.ToString()),
						new XAttribute("fid", dataObj.FileId.ToString())
					})
				}).ToString();
				eCustomDataPrimitiveType dataType = dataObj.DataType;
				Guid dataInstanceId = dataObj.DataInstanceId;
				string xml = text;
				Guid? fileId = dataObj.FileId;
				IDictionary<string, object> extraValues;
				if (dataObj.FileId == null)
				{
					extraValues = null;
				}
				else
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary.Add("fn", dataObj.Filename);
					extraValues = dictionary;
					dictionary.Add("fs", dataObj.FileSize.ToString());
				}
				result = new CustomDataSerialized(dataType, dataInstanceId, xml, fileId, extraValues);
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002EB0 File Offset: 0x000010B0
		public CustomDataFile DeSerialize(CustomDataSerialized serializedData)
		{
			XDocument xdocument = XDocument.Parse(serializedData.DataValueXml);
			XElement xelement = xdocument.Descendants(eCustomDataPrimitiveType.File.GetAttribute<CustomDataPrimitiveTypeAttribute>().XmlTag).FirstOrDefault<XElement>();
			string text;
			if (xelement == null)
			{
				text = null;
			}
			else
			{
				XAttribute xattribute = xelement.Attributes("fid").First<XAttribute>();
				text = ((xattribute != null) ? xattribute.Value : null);
			}
			string text2 = (text ?? "").Trim();
			string filename = (serializedData.ExtraValues != null && serializedData.ExtraValues.ContainsKey("fn")) ? ((serializedData.ExtraValues["fn"] as string) ?? "") : "";
			string text3 = (serializedData.ExtraValues != null && serializedData.ExtraValues.ContainsKey("fs")) ? ((serializedData.ExtraValues["fs"] as string) ?? "") : "";
			long fileSize;
			bool flag = text3.Length < 1 || !long.TryParse(text3, out fileSize);
			if (flag)
			{
				fileSize = 0L;
			}
			CustomDataFile customDataFile = new CustomDataFile();
			string text4;
			if (xelement == null)
			{
				text4 = null;
			}
			else
			{
				XAttribute xattribute2 = xelement.Attributes("id").First<XAttribute>();
				text4 = ((xattribute2 != null) ? xattribute2.Value : null);
			}
			customDataFile.DataInstanceId = new Guid(text4 ?? "");
			customDataFile.DataType = eCustomDataPrimitiveType.File;
			customDataFile.FileId = (string.IsNullOrEmpty(text2) ? null : new Guid?(new Guid(text2)));
			customDataFile.Filename = filename;
			customDataFile.FileSize = fileSize;
			return customDataFile;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000304C File Offset: 0x0000124C
		public bool IsValueEmptyForStorage(CustomDataFile dataObj)
		{
			Guid? guid = (dataObj != null) ? dataObj.FileId : null;
			return guid == null;
		}
	}
}
