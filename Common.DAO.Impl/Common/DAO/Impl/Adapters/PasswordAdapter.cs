using System;
using System.Text;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x02000183 RID: 387
	public static class PasswordAdapter
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x00079328 File Offset: 0x00077528
		public static string PasswordToString(this byte[] binaryPassword)
		{
			return Encoding.ASCII.GetString(binaryPassword);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00079348 File Offset: 0x00077548
		public static byte[] PasswordToBytes(this string password)
		{
			return Encoding.ASCII.GetBytes(password);
		}
	}
}
