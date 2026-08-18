using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Xml
{
	// Token: 0x020000BE RID: 190
	internal class SecureStringHasher : IEqualityComparer<string>
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x0001788B File Offset: 0x00015A8B
		public SecureStringHasher()
		{
			this.hashCodeRandomizer = Environment.TickCount;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001789E File Offset: 0x00015A9E
		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.Ordinal);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000178A8 File Offset: 0x00015AA8
		[SecuritySafeCritical]
		public int GetHashCode(string key)
		{
			if (SecureStringHasher.hashCodeDelegate == null)
			{
				SecureStringHasher.hashCodeDelegate = SecureStringHasher.GetHashCodeDelegate();
			}
			return SecureStringHasher.hashCodeDelegate(key, key.Length, (long)this.hashCodeRandomizer);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000178D4 File Offset: 0x00015AD4
		[SecurityCritical]
		private static int GetHashCodeOfString(string key, int sLen, long additionalEntropy)
		{
			int num = (int)additionalEntropy;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7 ^ (int)key[i]);
			}
			num -= num >> 17;
			num -= num >> 11;
			return num - (num >> 5);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00017918 File Offset: 0x00015B18
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static SecureStringHasher.HashCodeOfStringDelegate GetHashCodeDelegate()
		{
			MethodInfo method = typeof(string).GetMethod("InternalMarvin32HashString", BindingFlags.Static | BindingFlags.NonPublic);
			if (method != null)
			{
				return (SecureStringHasher.HashCodeOfStringDelegate)Delegate.CreateDelegate(typeof(SecureStringHasher.HashCodeOfStringDelegate), method);
			}
			return new SecureStringHasher.HashCodeOfStringDelegate(SecureStringHasher.GetHashCodeOfString);
		}

		// Token: 0x040002C3 RID: 707
		[SecurityCritical]
		private static SecureStringHasher.HashCodeOfStringDelegate hashCodeDelegate;

		// Token: 0x040002C4 RID: 708
		private int hashCodeRandomizer;

		// Token: 0x02000323 RID: 803
		// (Invoke) Token: 0x06002DDC RID: 11740
		[SecurityCritical]
		private delegate int HashCodeOfStringDelegate(string s, int sLen, long additionalEntropy);
	}
}
