using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200029A RID: 666
	internal sealed class NormalizedSerializer : Serializer
	{
		// Token: 0x0600226C RID: 8812 RVA: 0x0028BEE8 File Offset: 0x0028B2E8
		internal NormalizedSerializer(Type t) : base(t)
		{
			SqlUserDefinedTypeAttribute udtAttribute = SerializationHelperSql9.GetUdtAttribute(t);
			this.m_normalizer = new BinaryOrderedUdtNormalizer(t, true);
			this.m_isFixedSize = udtAttribute.IsFixedLength;
			this.m_maxSize = this.m_normalizer.Size;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0028BF38 File Offset: 0x0028B338
		public override void Serialize(Stream s, object o)
		{
			this.m_normalizer.NormalizeTopObject(o, s);
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0028BF58 File Offset: 0x0028B358
		public override object Deserialize(Stream s)
		{
			return this.m_normalizer.DeNormalizeTopObject(this.m_type, s);
		}

		// Token: 0x04001666 RID: 5734
		private BinaryOrderedUdtNormalizer m_normalizer;

		// Token: 0x04001667 RID: 5735
		private bool m_isFixedSize;

		// Token: 0x04001668 RID: 5736
		private int m_maxSize;
	}
}
