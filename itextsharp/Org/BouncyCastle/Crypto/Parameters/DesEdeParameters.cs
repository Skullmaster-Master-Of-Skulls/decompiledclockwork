using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200034A RID: 842
	public class DesEdeParameters : DesParameters
	{
		// Token: 0x06001E56 RID: 7766 RVA: 0x000B59CC File Offset: 0x000B49CC
		private static byte[] FixKey(byte[] key, int keyOff, int keyLen)
		{
			byte[] array = new byte[24];
			if (keyLen != 16)
			{
				if (keyLen != 24)
				{
					throw new ArgumentException("Bad length for DESede key: " + keyLen, "keyLen");
				}
				Array.Copy(key, keyOff, array, 0, 24);
			}
			else
			{
				Array.Copy(key, keyOff, array, 0, 16);
				Array.Copy(key, keyOff, array, 16, 8);
			}
			if (DesEdeParameters.IsWeakKey(array))
			{
				throw new ArgumentException("attempt to create weak DESede key");
			}
			return array;
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x000B5A43 File Offset: 0x000B4A43
		public DesEdeParameters(byte[] key) : base(DesEdeParameters.FixKey(key, 0, key.Length))
		{
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000B5A55 File Offset: 0x000B4A55
		public DesEdeParameters(byte[] key, int keyOff, int keyLen) : base(DesEdeParameters.FixKey(key, keyOff, keyLen))
		{
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000B5A68 File Offset: 0x000B4A68
		public static bool IsWeakKey(byte[] key, int offset, int length)
		{
			for (int i = offset; i < length; i += 8)
			{
				if (DesParameters.IsWeakKey(key, i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x000B5A8D File Offset: 0x000B4A8D
		public new static bool IsWeakKey(byte[] key, int offset)
		{
			return DesEdeParameters.IsWeakKey(key, offset, key.Length - offset);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000B5A9B File Offset: 0x000B4A9B
		public new static bool IsWeakKey(byte[] key)
		{
			return DesEdeParameters.IsWeakKey(key, 0, key.Length);
		}

		// Token: 0x04001509 RID: 5385
		public const int DesEdeKeyLength = 24;
	}
}
