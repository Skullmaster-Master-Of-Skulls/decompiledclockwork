using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200006D RID: 109
	internal abstract class Serializer
	{
		// Token: 0x06000536 RID: 1334
		public abstract object Deserialize(Stream s);

		// Token: 0x06000537 RID: 1335
		public abstract void Serialize(Stream s, object o);

		// Token: 0x06000538 RID: 1336 RVA: 0x000475E4 File Offset: 0x000469E4
		protected Serializer(Type t)
		{
			this.m_type = t;
		}

		// Token: 0x040001E9 RID: 489
		protected Type m_type;
	}
}
