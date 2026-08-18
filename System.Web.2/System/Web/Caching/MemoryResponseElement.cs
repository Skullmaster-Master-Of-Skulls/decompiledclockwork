using System;
using System.Security.Permissions;

namespace System.Web.Caching
{
	// Token: 0x02000885 RID: 2181
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
	[Serializable]
	public class MemoryResponseElement : ResponseElement
	{
		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x060066A2 RID: 26274 RVA: 0x001698A0 File Offset: 0x00167AA0
		public byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x060066A3 RID: 26275 RVA: 0x001698A8 File Offset: 0x00167AA8
		public long Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x060066A4 RID: 26276 RVA: 0x001697FE File Offset: 0x001679FE
		private MemoryResponseElement()
		{
		}

		// Token: 0x060066A5 RID: 26277 RVA: 0x001698B0 File Offset: 0x00167AB0
		public MemoryResponseElement(byte[] buffer, long length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (length < 0L || length > (long)buffer.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this._buffer = buffer;
			this._length = length;
		}

		// Token: 0x040034ED RID: 13549
		private byte[] _buffer;

		// Token: 0x040034EE RID: 13550
		private long _length;
	}
}
