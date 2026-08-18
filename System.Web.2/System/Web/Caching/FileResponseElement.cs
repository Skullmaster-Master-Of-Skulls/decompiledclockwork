using System;
using System.Security.Permissions;

namespace System.Web.Caching
{
	// Token: 0x02000882 RID: 2178
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
	[Serializable]
	public class FileResponseElement : ResponseElement
	{
		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x06006695 RID: 26261 RVA: 0x001697E6 File Offset: 0x001679E6
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x06006696 RID: 26262 RVA: 0x001697EE File Offset: 0x001679EE
		public long Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x06006697 RID: 26263 RVA: 0x001697F6 File Offset: 0x001679F6
		public long Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x06006698 RID: 26264 RVA: 0x001697FE File Offset: 0x001679FE
		private FileResponseElement()
		{
		}

		// Token: 0x06006699 RID: 26265 RVA: 0x00169808 File Offset: 0x00167A08
		public FileResponseElement(string path, long offset, long length)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this._path = path;
			this._offset = offset;
			this._length = length;
		}

		// Token: 0x040034E8 RID: 13544
		private string _path;

		// Token: 0x040034E9 RID: 13545
		private long _offset;

		// Token: 0x040034EA RID: 13546
		private long _length;
	}
}
