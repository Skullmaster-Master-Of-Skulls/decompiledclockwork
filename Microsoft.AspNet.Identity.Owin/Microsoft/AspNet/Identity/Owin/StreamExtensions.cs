using System;
using System.IO;
using System.Text;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000009 RID: 9
	internal static class StreamExtensions
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002D7C File Offset: 0x00000F7C
		public static BinaryReader CreateReader(this Stream stream)
		{
			return new BinaryReader(stream, StreamExtensions.DefaultEncoding, true);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002D8A File Offset: 0x00000F8A
		public static BinaryWriter CreateWriter(this Stream stream)
		{
			return new BinaryWriter(stream, StreamExtensions.DefaultEncoding, true);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002D98 File Offset: 0x00000F98
		public static DateTimeOffset ReadDateTimeOffset(this BinaryReader reader)
		{
			return new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002DAA File Offset: 0x00000FAA
		public static void Write(this BinaryWriter writer, DateTimeOffset value)
		{
			writer.Write(value.UtcTicks);
		}

		// Token: 0x0400000A RID: 10
		internal static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);
	}
}
