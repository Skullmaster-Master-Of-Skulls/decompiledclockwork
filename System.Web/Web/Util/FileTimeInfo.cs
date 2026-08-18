using System;

namespace System.Web.Util
{
	// Token: 0x02000767 RID: 1895
	internal struct FileTimeInfo
	{
		// Token: 0x06005C07 RID: 23559 RVA: 0x0017160B File Offset: 0x0017060B
		internal FileTimeInfo(long lastWriteTime, long size)
		{
			this.LastWriteTime = lastWriteTime;
			this.Size = size;
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x0017161C File Offset: 0x0017061C
		public override bool Equals(object obj)
		{
			if (obj is FileTimeInfo)
			{
				FileTimeInfo fileTimeInfo = (FileTimeInfo)obj;
				return this.LastWriteTime == fileTimeInfo.LastWriteTime && this.Size == fileTimeInfo.Size;
			}
			return false;
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x0017165A File Offset: 0x0017065A
		public static bool operator ==(FileTimeInfo value1, FileTimeInfo value2)
		{
			return value1.LastWriteTime == value2.LastWriteTime && value1.Size == value2.Size;
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x0017167E File Offset: 0x0017067E
		public static bool operator !=(FileTimeInfo value1, FileTimeInfo value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x0017168A File Offset: 0x0017068A
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this.LastWriteTime.GetHashCode(), this.Size.GetHashCode());
		}

		// Token: 0x0400313B RID: 12603
		internal long LastWriteTime;

		// Token: 0x0400313C RID: 12604
		internal long Size;

		// Token: 0x0400313D RID: 12605
		internal static readonly FileTimeInfo MinValue = new FileTimeInfo(0L, 0L);
	}
}
