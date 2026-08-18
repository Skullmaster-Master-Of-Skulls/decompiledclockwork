using System;

namespace System.Web.Util
{
	// Token: 0x020001FF RID: 511
	internal struct FileTimeInfo
	{
		// Token: 0x0600191B RID: 6427 RVA: 0x0004DC0C File Offset: 0x0004BE0C
		internal FileTimeInfo(long lastWriteTime, long size)
		{
			this.LastWriteTime = lastWriteTime;
			this.Size = size;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0004DC1C File Offset: 0x0004BE1C
		public override bool Equals(object obj)
		{
			if (obj is FileTimeInfo)
			{
				FileTimeInfo fileTimeInfo = (FileTimeInfo)obj;
				return this.LastWriteTime == fileTimeInfo.LastWriteTime && this.Size == fileTimeInfo.Size;
			}
			return false;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0004DC58 File Offset: 0x0004BE58
		public static bool operator ==(FileTimeInfo value1, FileTimeInfo value2)
		{
			return value1.LastWriteTime == value2.LastWriteTime && value1.Size == value2.Size;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0004DC78 File Offset: 0x0004BE78
		public static bool operator !=(FileTimeInfo value1, FileTimeInfo value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0004DC84 File Offset: 0x0004BE84
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this.LastWriteTime.GetHashCode(), this.Size.GetHashCode());
		}

		// Token: 0x040017AD RID: 6061
		internal long LastWriteTime;

		// Token: 0x040017AE RID: 6062
		internal long Size;

		// Token: 0x040017AF RID: 6063
		internal static readonly FileTimeInfo MinValue = new FileTimeInfo(0L, 0L);
	}
}
