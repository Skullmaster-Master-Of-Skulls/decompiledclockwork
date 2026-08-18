using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007DE RID: 2014
	internal sealed class BinaryObjectString : IStreamable
	{
		// Token: 0x06004757 RID: 18263 RVA: 0x000F49F3 File Offset: 0x000F39F3
		internal BinaryObjectString()
		{
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x000F49FB File Offset: 0x000F39FB
		internal void Set(int objectId, string value)
		{
			this.objectId = objectId;
			this.value = value;
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x000F4A0B File Offset: 0x000F3A0B
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(6);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.value);
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x000F4A2C File Offset: 0x000F3A2C
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadString();
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x000F4A46 File Offset: 0x000F3A46
		public void Dump()
		{
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x000F4A48 File Offset: 0x000F3A48
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002417 RID: 9239
		internal int objectId;

		// Token: 0x04002418 RID: 9240
		internal string value;
	}
}
