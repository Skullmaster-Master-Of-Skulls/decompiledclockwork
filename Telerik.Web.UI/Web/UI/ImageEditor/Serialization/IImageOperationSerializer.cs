using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000BAF RID: 2991
	public interface IImageOperationSerializer
	{
		// Token: 0x1700250B RID: 9483
		// (get) Token: 0x0600718D RID: 29069
		string Name { get; }

		// Token: 0x0600718E RID: 29070
		string Serialize(IImageOperation operation);

		// Token: 0x0600718F RID: 29071
		IImageOperation Deserialize(string value);

		// Token: 0x06007190 RID: 29072
		IImageOperation FromData(Dictionary<string, object> data);
	}
}
