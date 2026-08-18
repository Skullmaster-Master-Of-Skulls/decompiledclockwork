using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200007E RID: 126
	public struct MetadataStringComparer
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		internal MetadataStringComparer(MetadataReader reader)
		{
			this._reader = reader;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000E2B5 File Offset: 0x0000C4B5
		public bool Equals(StringHandle handle, string value)
		{
			return this.Equals(handle, value, false);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		public bool Equals(StringHandle handle, string value, bool ignoreCase)
		{
			if (value == null)
			{
				Throw.ValueArgumentNull();
			}
			return this._reader.StringStream.Equals(handle, value, this._reader.utf8Decoder, ignoreCase);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		public bool Equals(NamespaceDefinitionHandle handle, string value)
		{
			return this.Equals(handle, value, false);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		public bool Equals(NamespaceDefinitionHandle handle, string value, bool ignoreCase)
		{
			if (value == null)
			{
				Throw.ValueArgumentNull();
			}
			if (handle.HasFullName)
			{
				return this._reader.StringStream.Equals(handle.GetFullName(), value, this._reader.utf8Decoder, ignoreCase);
			}
			return value == this._reader.namespaceCache.GetFullName(handle);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000E34E File Offset: 0x0000C54E
		public bool Equals(DocumentNameBlobHandle handle, string value)
		{
			return this.Equals(handle, value, false);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000E359 File Offset: 0x0000C559
		public bool Equals(DocumentNameBlobHandle handle, string value, bool ignoreCase)
		{
			if (value == null)
			{
				Throw.ValueArgumentNull();
			}
			return this._reader.BlobStream.DocumentNameEquals(handle, value, ignoreCase);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000E376 File Offset: 0x0000C576
		public bool StartsWith(StringHandle handle, string value)
		{
			return this.StartsWith(handle, value, false);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000E381 File Offset: 0x0000C581
		public bool StartsWith(StringHandle handle, string value, bool ignoreCase)
		{
			if (value == null)
			{
				Throw.ValueArgumentNull();
			}
			return this._reader.StringStream.StartsWith(handle, value, this._reader.utf8Decoder, ignoreCase);
		}

		// Token: 0x040003A7 RID: 935
		private readonly MetadataReader _reader;
	}
}
