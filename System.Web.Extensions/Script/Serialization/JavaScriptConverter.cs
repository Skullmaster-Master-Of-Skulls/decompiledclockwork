using System;
using System.Collections.Generic;

namespace System.Web.Script.Serialization
{
	// Token: 0x02000106 RID: 262
	public abstract class JavaScriptConverter
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000DD9 RID: 3545
		public abstract IEnumerable<Type> SupportedTypes { get; }

		// Token: 0x06000DDA RID: 3546
		public abstract object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer);

		// Token: 0x06000DDB RID: 3547
		public abstract IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer);
	}
}
