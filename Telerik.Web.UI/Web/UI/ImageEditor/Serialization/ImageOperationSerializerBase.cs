using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000BAE RID: 2990
	public abstract class ImageOperationSerializerBase
	{
		// Token: 0x06007186 RID: 29062 RVA: 0x001A95A0 File Offset: 0x001A77A0
		public ImageOperationSerializerBase()
		{
			this._serializer = new JavaScriptSerializer();
		}

		// Token: 0x06007187 RID: 29063 RVA: 0x001A95B3 File Offset: 0x001A77B3
		public string Serialize(IImageOperation operation)
		{
			return this._serializer.Serialize(this.ToData(operation));
		}

		// Token: 0x06007188 RID: 29064 RVA: 0x001A95C7 File Offset: 0x001A77C7
		public IImageOperation Deserialize(string value)
		{
			return this.FromData(this._serializer.DeserializeObject(value) as Dictionary<string, object>);
		}

		// Token: 0x1700250A RID: 9482
		// (get) Token: 0x06007189 RID: 29065
		public abstract string Name { get; }

		// Token: 0x0600718A RID: 29066
		public abstract Dictionary<string, object> ToData(IImageOperation operation);

		// Token: 0x0600718B RID: 29067 RVA: 0x001A95E0 File Offset: 0x001A77E0
		protected bool NameInDataIsCorrect(Dictionary<string, object> data)
		{
			return data.ContainsKey("name") && data["name"].ToString() == this.Name;
		}

		// Token: 0x0600718C RID: 29068
		public abstract IImageOperation FromData(Dictionary<string, object> data);

		// Token: 0x04001EA2 RID: 7842
		private readonly JavaScriptSerializer _serializer;
	}
}
