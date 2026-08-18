using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200018F RID: 399
	public abstract class PbeParametersGenerator
	{
		// Token: 0x06000F7B RID: 3963 RVA: 0x0005945E File Offset: 0x0005845E
		public virtual void Init(byte[] password, byte[] salt, int iterationCount)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt");
			}
			this.mPassword = Arrays.Clone(password);
			this.mSalt = Arrays.Clone(salt);
			this.mIterationCount = iterationCount;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0005949B File Offset: 0x0005849B
		public virtual byte[] Password
		{
			get
			{
				return Arrays.Clone(this.mPassword);
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000594A8 File Offset: 0x000584A8
		[Obsolete("Use 'Password' property")]
		public byte[] GetPassword()
		{
			return this.Password;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x000594B0 File Offset: 0x000584B0
		public virtual byte[] Salt
		{
			get
			{
				return Arrays.Clone(this.mSalt);
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x000594BD File Offset: 0x000584BD
		[Obsolete("Use 'Salt' property")]
		public byte[] GetSalt()
		{
			return this.Salt;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x000594C5 File Offset: 0x000584C5
		public virtual int IterationCount
		{
			get
			{
				return this.mIterationCount;
			}
		}

		// Token: 0x06000F81 RID: 3969
		[Obsolete("Use version with 'algorithm' parameter")]
		public abstract ICipherParameters GenerateDerivedParameters(int keySize);

		// Token: 0x06000F82 RID: 3970
		public abstract ICipherParameters GenerateDerivedParameters(string algorithm, int keySize);

		// Token: 0x06000F83 RID: 3971
		[Obsolete("Use version with 'algorithm' parameter")]
		public abstract ICipherParameters GenerateDerivedParameters(int keySize, int ivSize);

		// Token: 0x06000F84 RID: 3972
		public abstract ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize);

		// Token: 0x06000F85 RID: 3973
		public abstract ICipherParameters GenerateDerivedMacParameters(int keySize);

		// Token: 0x06000F86 RID: 3974 RVA: 0x000594CD File Offset: 0x000584CD
		public static byte[] Pkcs5PasswordToBytes(char[] password)
		{
			return Encoding.ASCII.GetBytes(password);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x000594DA File Offset: 0x000584DA
		[Obsolete("Use version taking 'char[]' instead")]
		public static byte[] Pkcs5PasswordToBytes(string password)
		{
			return Encoding.ASCII.GetBytes(password);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000594E7 File Offset: 0x000584E7
		public static byte[] Pkcs5PasswordToUtf8Bytes(char[] password)
		{
			return Encoding.UTF8.GetBytes(password);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000594F4 File Offset: 0x000584F4
		[Obsolete("Use version taking 'char[]' instead")]
		public static byte[] Pkcs5PasswordToUtf8Bytes(string password)
		{
			return Encoding.UTF8.GetBytes(password);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00059501 File Offset: 0x00058501
		public static byte[] Pkcs12PasswordToBytes(char[] password)
		{
			return PbeParametersGenerator.Pkcs12PasswordToBytes(password, false);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0005950C File Offset: 0x0005850C
		public static byte[] Pkcs12PasswordToBytes(char[] password, bool wrongPkcs12Zero)
		{
			if (password.Length < 1)
			{
				return new byte[wrongPkcs12Zero ? 2 : 0];
			}
			byte[] array = new byte[(password.Length + 1) * 2];
			Encoding.BigEndianUnicode.GetBytes(password, 0, password.Length, array, 0);
			return array;
		}

		// Token: 0x04000B3D RID: 2877
		protected byte[] mPassword;

		// Token: 0x04000B3E RID: 2878
		protected byte[] mSalt;

		// Token: 0x04000B3F RID: 2879
		protected int mIterationCount;
	}
}
