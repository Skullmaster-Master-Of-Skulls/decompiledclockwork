using System;
using System.IO;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200028A RID: 650
	internal class InputStream : StreamReader
	{
		// Token: 0x06001956 RID: 6486 RVA: 0x00108F90 File Offset: 0x00107190
		public InputStream(Stream sr) : base(sr)
		{
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00108F9C File Offset: 0x0010719C
		public InputStream(string fname) : base(fname)
		{
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00108FA8 File Offset: 0x001071A8
		public T ReadObjectData<T>() where T : IStreamable, new()
		{
			T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			if (result.ReadFromStream(this) == 0)
			{
				return result;
			}
			return default(T);
		}
	}
}
