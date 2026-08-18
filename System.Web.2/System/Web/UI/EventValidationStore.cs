using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Web.Security.Cryptography;

namespace System.Web.UI
{
	// Token: 0x02000233 RID: 563
	internal sealed class EventValidationStore
	{
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x0005396B File Offset: 0x00051B6B
		public int Count
		{
			get
			{
				return this._hashes.Count;
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00053978 File Offset: 0x00051B78
		public void Add(string target, string argument)
		{
			this._hashes.Add(EventValidationStore.Hash(target, argument));
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00053990 File Offset: 0x00051B90
		public EventValidationStore Clone()
		{
			EventValidationStore eventValidationStore = new EventValidationStore();
			eventValidationStore._hashes.UnionWith(this._hashes);
			return eventValidationStore;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000539B5 File Offset: 0x00051BB5
		public bool Contains(string target, string argument)
		{
			return this._hashes.Contains(EventValidationStore.Hash(target, argument));
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000539CC File Offset: 0x00051BCC
		private static void CopyStringToBuffer(string s, byte[] buffer, ref int offset)
		{
			int num = (s != null) ? s.Length : 0;
			int num2 = offset;
			offset = num2 + 1;
			buffer[num2] = (byte)(num >> 24);
			num2 = offset;
			offset = num2 + 1;
			buffer[num2] = (byte)(num >> 16);
			num2 = offset;
			offset = num2 + 1;
			buffer[num2] = (byte)(num >> 8);
			num2 = offset;
			offset = num2 + 1;
			buffer[num2] = (byte)num;
			if (s != null)
			{
				foreach (char c in s)
				{
					num2 = offset;
					offset = num2 + 1;
					buffer[num2] = (byte)(c >> 8);
					num2 = offset;
					offset = num2 + 1;
					buffer[num2] = (byte)c;
				}
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00053A5C File Offset: 0x00051C5C
		public static EventValidationStore DeserializeFrom(Stream inputStream)
		{
			EventValidationStore.DeserializingBinaryReader deserializingBinaryReader = new EventValidationStore.DeserializingBinaryReader(inputStream);
			byte b = deserializingBinaryReader.ReadByte();
			if (b != 0)
			{
				throw new InvalidOperationException(SR.GetString("InvalidSerializedData"));
			}
			EventValidationStore eventValidationStore = new EventValidationStore();
			int num = deserializingBinaryReader.Read7BitEncodedInt();
			for (int i = 0; i < num; i++)
			{
				byte[] array = deserializingBinaryReader.ReadBytes(16);
				if (array.Length != 16)
				{
					throw new InvalidOperationException(SR.GetString("InvalidSerializedData"));
				}
				eventValidationStore._hashes.Add(array);
			}
			return eventValidationStore;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00053AD8 File Offset: 0x00051CD8
		private static byte[] Hash(string target, string argument)
		{
			int num = (target != null) ? target.Length : 0;
			int num2 = (argument != null) ? argument.Length : 0;
			byte[] buffer = new byte[8 + (num + num2) * 2];
			int num3 = 0;
			EventValidationStore.CopyStringToBuffer(target, buffer, ref num3);
			EventValidationStore.CopyStringToBuffer(argument, buffer, ref num3);
			byte[] src;
			using (SHA256 sha = CryptoAlgorithms.CreateSHA256())
			{
				src = sha.ComputeHash(buffer);
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(src, 0, array, 0, 16);
			return array;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00053B68 File Offset: 0x00051D68
		public void SerializeTo(Stream outputStream)
		{
			EventValidationStore.SerializingBinaryWriter serializingBinaryWriter = new EventValidationStore.SerializingBinaryWriter(outputStream);
			serializingBinaryWriter.Write(0);
			serializingBinaryWriter.Write7BitEncodedInt(this._hashes.Count);
			foreach (byte[] buffer in this._hashes)
			{
				serializingBinaryWriter.Write(buffer);
			}
		}

		// Token: 0x0400184B RID: 6219
		private const int HASH_SIZE_IN_BYTES = 16;

		// Token: 0x0400184C RID: 6220
		private readonly HashSet<byte[]> _hashes = new HashSet<byte[]>(EventValidationStore.HashEqualityComparer.Instance);

		// Token: 0x02000950 RID: 2384
		private sealed class HashEqualityComparer : IEqualityComparer<byte[]>
		{
			// Token: 0x060069A6 RID: 27046 RVA: 0x000030B5 File Offset: 0x000012B5
			private HashEqualityComparer()
			{
			}

			// Token: 0x060069A7 RID: 27047 RVA: 0x00177A98 File Offset: 0x00175C98
			public bool Equals(byte[] x, byte[] y)
			{
				for (int i = 0; i < 16; i++)
				{
					if (x[i] != y[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060069A8 RID: 27048 RVA: 0x00177ABD File Offset: 0x00175CBD
			public int GetHashCode(byte[] obj)
			{
				return BitConverter.ToInt32(obj, 0);
			}

			// Token: 0x040037DA RID: 14298
			internal static readonly EventValidationStore.HashEqualityComparer Instance = new EventValidationStore.HashEqualityComparer();
		}

		// Token: 0x02000951 RID: 2385
		private sealed class DeserializingBinaryReader : BinaryReader
		{
			// Token: 0x060069AA RID: 27050 RVA: 0x00177AD2 File Offset: 0x00175CD2
			public DeserializingBinaryReader(Stream input) : base(input)
			{
			}

			// Token: 0x060069AB RID: 27051 RVA: 0x00006164 File Offset: 0x00004364
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x060069AC RID: 27052 RVA: 0x00177ADB File Offset: 0x00175CDB
			public new int Read7BitEncodedInt()
			{
				return base.Read7BitEncodedInt();
			}
		}

		// Token: 0x02000952 RID: 2386
		private sealed class SerializingBinaryWriter : BinaryWriter
		{
			// Token: 0x060069AD RID: 27053 RVA: 0x00177AE3 File Offset: 0x00175CE3
			public SerializingBinaryWriter(Stream input) : base(input)
			{
			}

			// Token: 0x060069AE RID: 27054 RVA: 0x00006164 File Offset: 0x00004364
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x060069AF RID: 27055 RVA: 0x00177AEC File Offset: 0x00175CEC
			public new void Write7BitEncodedInt(int value)
			{
				base.Write7BitEncodedInt(value);
			}
		}
	}
}
