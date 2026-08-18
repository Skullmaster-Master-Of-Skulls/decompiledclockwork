using System;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Security;

namespace System.Reflection.Metadata
{
	// Token: 0x02000052 RID: 82
	internal struct BlobContentId : IEquatable<BlobContentId>
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00005D95 File Offset: 0x00003F95
		public Guid Guid { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00005D9D File Offset: 0x00003F9D
		public uint Stamp { get; }

		// Token: 0x06000233 RID: 563 RVA: 0x00005DA5 File Offset: 0x00003FA5
		public BlobContentId(Guid guid, uint stamp)
		{
			this.Guid = guid;
			this.Stamp = stamp;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00005DB5 File Offset: 0x00003FB5
		public BlobContentId(ImmutableArray<byte> id)
		{
			this = new BlobContentId(id.UnderlyingArray);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00005DC4 File Offset: 0x00003FC4
		[SecuritySafeCritical]
		public unsafe BlobContentId(byte[] id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length != 20)
			{
				throw new ArgumentException("UnexpectedArrayLength", "id");
			}
			fixed (byte* ptr = &id[0])
			{
				byte* buffer = ptr;
				BlobReader blobReader = new BlobReader(buffer, id.Length);
				this.Guid = blobReader.ReadGuid();
				this.Stamp = blobReader.ReadUInt32();
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00005E2C File Offset: 0x0000402C
		public bool IsDefault
		{
			get
			{
				return this.Guid == default(Guid) && this.Stamp == 0U;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00005E5A File Offset: 0x0000405A
		public bool Equals(BlobContentId other)
		{
			return this.Guid == other.Guid && this.Stamp == other.Stamp;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00005E81 File Offset: 0x00004081
		public override bool Equals(object obj)
		{
			return obj is BlobContentId && this.Equals((BlobContentId)obj);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00005E9C File Offset: 0x0000409C
		public override int GetHashCode()
		{
			return Hash.Combine(this.Stamp, this.Guid.GetHashCode());
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00005EC8 File Offset: 0x000040C8
		public static bool operator ==(BlobContentId left, BlobContentId right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00005ED2 File Offset: 0x000040D2
		public static bool operator !=(BlobContentId left, BlobContentId right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040002F9 RID: 761
		private const int Size = 20;
	}
}
