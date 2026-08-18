using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000141 RID: 321
	public interface IMetadataAnnotationSerializer
	{
		// Token: 0x06000AA8 RID: 2728
		string Serialize(string name, object value);

		// Token: 0x06000AA9 RID: 2729
		object Deserialize(string name, string value);
	}
}
