using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200006E RID: 110
	internal sealed class NormalizedSerializer : Serializer
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x00047600 File Offset: 0x00046A00
		internal NormalizedSerializer(Type t) : base(t)
		{
			SqlUserDefinedTypeAttribute udtAttribute = SerializationHelperSql9.GetUdtAttribute(t);
			this.m_normalizer = new BinaryOrderedUdtNormalizer(t, true);
			this.m_isFixedSize = udtAttribute.IsFixedLength;
			this.m_maxSize = this.m_normalizer.Size;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00047648 File Offset: 0x00046A48
		public override void Serialize(Stream s, object o)
		{
			this.m_normalizer.NormalizeTopObject(o, s);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00047664 File Offset: 0x00046A64
		public override object Deserialize(Stream s)
		{
			return this.m_normalizer.DeNormalizeTopObject(this.m_type, s);
		}

		// Token: 0x040001EA RID: 490
		private BinaryOrderedUdtNormalizer m_normalizer;

		// Token: 0x040001EB RID: 491
		private bool m_isFixedSize;

		// Token: 0x040001EC RID: 492
		private int m_maxSize;
	}
}
