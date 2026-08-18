using System;
using System.Web;
using EncryptionClassLibrary;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x02000187 RID: 391
	public static class WebApplicationUtilityAdapter
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x000795A4 File Offset: 0x000777A4
		public static string EncodeUrlVariable(this int variable, IEncryption encryption)
		{
			return WebApplicationUtilityAdapter.ConvertIntParameterToUrlString(variable, encryption);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000795C0 File Offset: 0x000777C0
		private static string ConvertIntParameterToUrlString(int parameter, IEncryption encryption)
		{
			string str = DateTime.Now.ToString("yyyy-MM-dd H:mm");
			string plainText = parameter.ToString() + "`" + str;
			byte[] inArray = encryption.Encrypt(plainText);
			string str2 = Convert.ToBase64String(inArray);
			return HttpUtility.UrlEncode(str2);
		}
	}
}
