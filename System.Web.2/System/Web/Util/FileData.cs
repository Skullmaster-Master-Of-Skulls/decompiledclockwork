using System;

namespace System.Web.Util
{
	// Token: 0x020001FD RID: 509
	internal abstract class FileData
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0004DA7C File Offset: 0x0004BC7C
		internal string Name
		{
			get
			{
				return this._wfd.cFileName;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0004DA89 File Offset: 0x0004BC89
		internal string FullName
		{
			get
			{
				return this._path + "\\" + this._wfd.cFileName;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x0004DAA6 File Offset: 0x0004BCA6
		internal bool IsDirectory
		{
			get
			{
				return (this._wfd.dwFileAttributes & 16U) > 0U;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0004DAB9 File Offset: 0x0004BCB9
		internal bool IsHidden
		{
			get
			{
				return (this._wfd.dwFileAttributes & 2U) > 0U;
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0004DACB File Offset: 0x0004BCCB
		internal FindFileData GetFindFileData()
		{
			return new FindFileData(ref this._wfd);
		}

		// Token: 0x040017AA RID: 6058
		protected string _path;

		// Token: 0x040017AB RID: 6059
		protected UnsafeNativeMethods.WIN32_FIND_DATA _wfd;
	}
}
