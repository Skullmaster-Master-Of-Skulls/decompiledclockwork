using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200029B RID: 667
	internal sealed class BinarySerializeSerializer : Serializer
	{
		// Token: 0x0600226F RID: 8815 RVA: 0x0028BF88 File Offset: 0x0028B388
		internal BinarySerializeSerializer(Type t) : base(t)
		{
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0028BFA8 File Offset: 0x0028B3A8
		public override void Serialize(Stream s, object o)
		{
			BinaryWriter w = new BinaryWriter(s);
			((IBinarySerialize)o).Write(w);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0028BFC8 File Offset: 0x0028B3C8
		public override object Deserialize(Stream s)
		{
			object obj = Activator.CreateInstance(this.m_type);
			BinaryReader r = new BinaryReader(s);
			((IBinarySerialize)obj).Read(r);
			return obj;
		}
	}
}
