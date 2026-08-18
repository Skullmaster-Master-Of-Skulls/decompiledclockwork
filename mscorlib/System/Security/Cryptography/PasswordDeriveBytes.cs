using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
	// Token: 0x02000892 RID: 2194
	[ComVisible(true)]
	public class PasswordDeriveBytes : DeriveBytes
	{
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x001158B0 File Offset: 0x001148B0
		private SafeProvHandle ProvHandle
		{
			get
			{
				if (this._safeProvHandle == null)
				{
					lock (this)
					{
						if (this._safeProvHandle == null)
						{
							SafeProvHandle safeProvHandle = Utils.AcquireProvHandle(this._cspParams);
							Thread.MemoryBarrier();
							this._safeProvHandle = safeProvHandle;
						}
					}
				}
				return this._safeProvHandle;
			}
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x0011590C File Offset: 0x0011490C
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt) : this(strPassword, rgbSalt, new CspParameters())
		{
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0011591B File Offset: 0x0011491B
		public PasswordDeriveBytes(byte[] password, byte[] salt) : this(password, salt, new CspParameters())
		{
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0011592A File Offset: 0x0011492A
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations) : this(strPassword, rgbSalt, strHashName, iterations, new CspParameters())
		{
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x0011593C File Offset: 0x0011493C
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations) : this(password, salt, hashName, iterations, new CspParameters())
		{
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x0011594E File Offset: 0x0011494E
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, CspParameters cspParams) : this(strPassword, rgbSalt, "SHA1", 100, cspParams)
		{
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x00115960 File Offset: 0x00114960
		public PasswordDeriveBytes(byte[] password, byte[] salt, CspParameters cspParams) : this(password, salt, "SHA1", 100, cspParams)
		{
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x00115972 File Offset: 0x00114972
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations, CspParameters cspParams) : this(new UTF8Encoding(false).GetBytes(strPassword), rgbSalt, strHashName, iterations, cspParams)
		{
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0011598C File Offset: 0x0011498C
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations, CspParameters cspParams)
		{
			this.IterationCount = iterations;
			this.Salt = salt;
			this.HashName = hashName;
			this._password = password;
			this._cspParams = cspParams;
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06004FD7 RID: 20439 RVA: 0x001159B9 File Offset: 0x001149B9
		// (set) Token: 0x06004FD8 RID: 20440 RVA: 0x001159C4 File Offset: 0x001149C4
		public string HashName
		{
			get
			{
				return this._hashName;
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed"), new object[]
					{
						"HashName"
					}));
				}
				this._hashName = value;
				this._hash = (HashAlgorithm)CryptoConfig.CreateFromName(this._hashName);
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06004FD9 RID: 20441 RVA: 0x00115A20 File Offset: 0x00114A20
		// (set) Token: 0x06004FDA RID: 20442 RVA: 0x00115A28 File Offset: 0x00114A28
		public int IterationCount
		{
			get
			{
				return this._iterations;
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed"), new object[]
					{
						"IterationCount"
					}));
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				this._iterations = value;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06004FDB RID: 20443 RVA: 0x00115A87 File Offset: 0x00114A87
		// (set) Token: 0x06004FDC RID: 20444 RVA: 0x00115AA4 File Offset: 0x00114AA4
		public byte[] Salt
		{
			get
			{
				if (this._salt == null)
				{
					return null;
				}
				return (byte[])this._salt.Clone();
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed"), new object[]
					{
						"Salt"
					}));
				}
				if (value == null)
				{
					this._salt = null;
					return;
				}
				this._salt = (byte[])value.Clone();
			}
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x00115B00 File Offset: 0x00114B00
		[Obsolete("Rfc2898DeriveBytes replaces PasswordDeriveBytes for deriving key material from a password and is preferred in new applications.")]
		public override byte[] GetBytes(int cb)
		{
			int num = 0;
			byte[] array = new byte[cb];
			if (this._baseValue == null)
			{
				this.ComputeBaseValue();
			}
			else if (this._extra != null)
			{
				num = this._extra.Length - this._extraCount;
				if (num >= cb)
				{
					Buffer.InternalBlockCopy(this._extra, this._extraCount, array, 0, cb);
					if (num > cb)
					{
						this._extraCount += cb;
					}
					else
					{
						this._extra = null;
					}
					return array;
				}
				Buffer.InternalBlockCopy(this._extra, num, array, 0, num);
				this._extra = null;
			}
			byte[] array2 = this.ComputeBytes(cb - num);
			Buffer.InternalBlockCopy(array2, 0, array, num, cb - num);
			if (array2.Length + num > cb)
			{
				this._extra = array2;
				this._extraCount = cb - num;
			}
			return array;
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x00115BB9 File Offset: 0x00114BB9
		public override void Reset()
		{
			this._prefix = 0;
			this._extra = null;
			this._baseValue = null;
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x00115BD0 File Offset: 0x00114BD0
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			if (keySize < 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			int num = X509Utils.OidToAlgId(alghashname);
			if (num == 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
			}
			int num2 = X509Utils.OidToAlgId(algname);
			if (num2 == 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
			}
			if (rgbIV == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidIV"));
			}
			return Utils._CryptDeriveKey(this.ProvHandle, num2, num, this._password, keySize << 16, rgbIV);
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x00115C54 File Offset: 0x00114C54
		private byte[] ComputeBaseValue()
		{
			this._hash.Initialize();
			this._hash.TransformBlock(this._password, 0, this._password.Length, this._password, 0);
			if (this._salt != null)
			{
				this._hash.TransformBlock(this._salt, 0, this._salt.Length, this._salt, 0);
			}
			this._hash.TransformFinalBlock(new byte[0], 0, 0);
			this._baseValue = this._hash.Hash;
			this._hash.Initialize();
			for (int i = 1; i < this._iterations - 1; i++)
			{
				this._hash.ComputeHash(this._baseValue);
				this._baseValue = this._hash.Hash;
			}
			return this._baseValue;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x00115D24 File Offset: 0x00114D24
		private byte[] ComputeBytes(int cb)
		{
			int num = 0;
			this._hash.Initialize();
			int num2 = this._hash.HashSize / 8;
			byte[] array = new byte[(cb + num2 - 1) / num2 * num2];
			CryptoStream cryptoStream = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write);
			this.HashPrefix(cryptoStream);
			cryptoStream.Write(this._baseValue, 0, this._baseValue.Length);
			cryptoStream.Close();
			Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
			num += num2;
			while (cb > num)
			{
				this._hash.Initialize();
				cryptoStream = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write);
				this.HashPrefix(cryptoStream);
				cryptoStream.Write(this._baseValue, 0, this._baseValue.Length);
				cryptoStream.Close();
				Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
				num += num2;
			}
			return array;
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x00115E0C File Offset: 0x00114E0C
		private void HashPrefix(CryptoStream cs)
		{
			int num = 0;
			byte[] array = new byte[]
			{
				48,
				48,
				48
			};
			if (this._prefix > 999)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_TooManyBytes"));
			}
			if (this._prefix >= 100)
			{
				byte[] array2 = array;
				int num2 = 0;
				array2[num2] += (byte)(this._prefix / 100);
				num++;
			}
			if (this._prefix >= 10)
			{
				byte[] array3 = array;
				int num3 = num;
				array3[num3] += (byte)(this._prefix % 100 / 10);
				num++;
			}
			if (this._prefix > 0)
			{
				byte[] array4 = array;
				int num4 = num;
				array4[num4] += (byte)(this._prefix % 10);
				num++;
				cs.Write(array, 0, num);
			}
			this._prefix++;
		}

		// Token: 0x04002919 RID: 10521
		private int _extraCount;

		// Token: 0x0400291A RID: 10522
		private int _prefix;

		// Token: 0x0400291B RID: 10523
		private int _iterations;

		// Token: 0x0400291C RID: 10524
		private byte[] _baseValue;

		// Token: 0x0400291D RID: 10525
		private byte[] _extra;

		// Token: 0x0400291E RID: 10526
		private byte[] _salt;

		// Token: 0x0400291F RID: 10527
		private string _hashName;

		// Token: 0x04002920 RID: 10528
		private byte[] _password;

		// Token: 0x04002921 RID: 10529
		private HashAlgorithm _hash;

		// Token: 0x04002922 RID: 10530
		private CspParameters _cspParams;

		// Token: 0x04002923 RID: 10531
		private SafeProvHandle _safeProvHandle;
	}
}
