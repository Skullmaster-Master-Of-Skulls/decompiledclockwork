using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x0200049A RID: 1178
	public class XmlStateSerializer : IStateSerializer
	{
		// Token: 0x060029E1 RID: 10721 RVA: 0x00086D88 File Offset: 0x00084F88
		public XmlStateSerializer()
		{
			this._serializer = new XmlSerializer(typeof(List<RadControlState>));
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00086DA8 File Offset: 0x00084FA8
		public string Serialize(RadControlState state)
		{
			List<RadControlState> stateCollection = new List<RadControlState>
			{
				state
			};
			return this.Serialize(stateCollection);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x00086DCC File Offset: 0x00084FCC
		public RadControlState Deserialize(string stateData)
		{
			RadControlState result = null;
			List<RadControlState> list = this.DeserializeCollection(stateData);
			if (list.Count > 0)
			{
				result = list[0];
			}
			return result;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x00086DF8 File Offset: 0x00084FF8
		public string Serialize(List<RadControlState> stateCollection)
		{
			string result = string.Empty;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this._serializer.Serialize(memoryStream, stateCollection);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x00086E6C File Offset: 0x0008506C
		public List<RadControlState> DeserializeCollection(string stateData)
		{
			List<RadControlState> result = new List<RadControlState>();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(memoryStream))
				{
					streamWriter.Write(stateData);
					streamWriter.Flush();
					memoryStream.Seek(0L, SeekOrigin.Begin);
					if (memoryStream.Length > 0L)
					{
						result = (List<RadControlState>)this._serializer.Deserialize(memoryStream);
					}
				}
			}
			return result;
		}

		// Token: 0x04000ABD RID: 2749
		private XmlSerializer _serializer;
	}
}
