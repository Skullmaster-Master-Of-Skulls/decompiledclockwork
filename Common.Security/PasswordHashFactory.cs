using System;
using System.Linq;

namespace TechnoPro.Common.Security.Hashing
{
	// Token: 0x02000009 RID: 9
	public static class PasswordHashFactory
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002308 File Offset: 0x00000508
		private static T GetAttribute<T>(Enum enumeration) where T : Attribute
		{
			T result;
			if ((result = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault<T>()) == null)
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002354 File Offset: 0x00000554
		public static IHashingProvider GetHashingProvider(string hashingType)
		{
			if (!Enum.IsDefined(typeof(eHashingType), hashingType))
			{
				return null;
			}
			return PasswordHashFactory.GetHashingProvider((eHashingType)Enum.Parse(typeof(eHashingType), hashingType));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002384 File Offset: 0x00000584
		public static IHashingProvider GetHashingProvider(eHashingType hashingType)
		{
			HashingTypeAttribute attribute = PasswordHashFactory.GetAttribute<HashingTypeAttribute>(hashingType);
			Type type = Type.GetType("TechnoPro.Common.Security.Hashing." + attribute.ProviderHashClassName);
			if (!(type != null))
			{
				return null;
			}
			return (IHashingProvider)Activator.CreateInstance(type);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023CC File Offset: 0x000005CC
		public static bool SlowEquals(byte[] a, byte[] b)
		{
			uint num = (uint)(a.Length ^ b.Length);
			int num2 = 0;
			while (num2 < a.Length && num2 < b.Length)
			{
				num |= (uint)(a[num2] ^ b[num2]);
				num2++;
			}
			return num == 0U;
		}
	}
}
