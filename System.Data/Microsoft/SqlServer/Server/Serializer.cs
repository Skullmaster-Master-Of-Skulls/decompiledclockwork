using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000299 RID: 665
	internal abstract class Serializer
	{
		// Token: 0x06002269 RID: 8809
		public abstract object Deserialize(Stream s);

		// Token: 0x0600226A RID: 8810
		public abstract void Serialize(Stream s, object o);

		// Token: 0x0600226B RID: 8811 RVA: 0x0028BEC8 File Offset: 0x0028B2C8
		protected Serializer(Type t)
		{
			this.m_type = t;
		}

		// Token: 0x04001665 RID: 5733
		protected Type m_type;
	}
}
