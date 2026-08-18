using System;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x02000024 RID: 36
	public sealed class CacheFileLockException : Exception
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x00006E74 File Offset: 0x00005074
		public CacheFileLockException(string lockFile, Exception innerException = null) : base("Could not create the cache lock file because it already exists: {0}\r\nThis usually indicates that you another process is already running using this lockfile.".InvariantFormat(new object[]
		{
			lockFile
		}), innerException)
		{
		}
	}
}
