using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200006F RID: 111
	internal sealed class BinarySerializeSerializer : Serializer
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x00047688 File Offset: 0x00046A88
		internal BinarySerializeSerializer(Type t) : base(t)
		{
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0004769C File Offset: 0x00046A9C
		public override void Serialize(Stream s, object o)
		{
			BinaryWriter w = new BinaryWriter(s);
			((IBinarySerialize)o).Write(w);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000476BC File Offset: 0x00046ABC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Deserialize(Stream s)
		{
			object obj = Activator.CreateInstance(this.m_type);
			BinaryReader r = new BinaryReader(s);
			((IBinarySerialize)obj).Read(r);
			return obj;
		}
	}
}
