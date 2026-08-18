using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace System.IdentityModel
{
	// Token: 0x020000B4 RID: 180
	internal static class UniqueId
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x000148B4 File Offset: 0x00012AB4
		public static string CreateUniqueId()
		{
			return UniqueId.optimizedNcNamePrefix + UniqueId.GetNextId();
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000148C5 File Offset: 0x00012AC5
		public static string CreateUniqueId(string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("prefix");
			}
			return prefix + UniqueId.reusableUuid + "-" + UniqueId.GetNextId();
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000148F4 File Offset: 0x00012AF4
		public static string CreateRandomId()
		{
			return "_" + UniqueId.GetRandomUuid();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014905 File Offset: 0x00012B05
		public static string CreateRandomId(string prefix)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("prefix");
			}
			return prefix + UniqueId.GetRandomUuid();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001492A File Offset: 0x00012B2A
		public static Uri CreateRandomUri()
		{
			return new Uri("urn:uuid:" + UniqueId.GetRandomUuid());
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00014940 File Offset: 0x00012B40
		private static string GetNextId()
		{
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			byte[] array = new byte[16];
			randomNumberGenerator.GetBytes(array);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.AppendFormat("{0:X2}", array[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00014990 File Offset: 0x00012B90
		private static string GetRandomUuid()
		{
			return Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture);
		}

		// Token: 0x040004CC RID: 1228
		private const int RandomSaltSize = 16;

		// Token: 0x040004CD RID: 1229
		private const string NcNamePrefix = "_";

		// Token: 0x040004CE RID: 1230
		private const string UuidUriPrefix = "urn:uuid:";

		// Token: 0x040004CF RID: 1231
		private static readonly string reusableUuid = UniqueId.GetRandomUuid();

		// Token: 0x040004D0 RID: 1232
		private static readonly string optimizedNcNamePrefix = "_" + UniqueId.reusableUuid + "-";
	}
}
