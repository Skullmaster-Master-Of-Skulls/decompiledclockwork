using System;
using System.IO;
using System.Security.Cryptography;

namespace Ionic.Zip
{
	// Token: 0x02000027 RID: 39
	internal class WinZipAesCrypto
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004A09 File Offset: 0x00002C09
		private WinZipAesCrypto(string password, int KeyStrengthInBits)
		{
			this._Password = password;
			this._KeyStrengthInBits = KeyStrengthInBits;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004A2C File Offset: 0x00002C2C
		public static WinZipAesCrypto Generate(string password, int KeyStrengthInBits)
		{
			WinZipAesCrypto winZipAesCrypto = new WinZipAesCrypto(password, KeyStrengthInBits);
			int num = winZipAesCrypto._KeyStrengthInBytes / 2;
			winZipAesCrypto._Salt = new byte[num];
			Random random = new Random();
			random.NextBytes(winZipAesCrypto._Salt);
			return winZipAesCrypto;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004A6C File Offset: 0x00002C6C
		public static WinZipAesCrypto ReadFromStream(string password, int KeyStrengthInBits, Stream s)
		{
			WinZipAesCrypto winZipAesCrypto = new WinZipAesCrypto(password, KeyStrengthInBits);
			int num = winZipAesCrypto._KeyStrengthInBytes / 2;
			winZipAesCrypto._Salt = new byte[num];
			winZipAesCrypto._providedPv = new byte[2];
			s.Read(winZipAesCrypto._Salt, 0, winZipAesCrypto._Salt.Length);
			s.Read(winZipAesCrypto._providedPv, 0, winZipAesCrypto._providedPv.Length);
			winZipAesCrypto.PasswordVerificationStored = (short)((int)winZipAesCrypto._providedPv[0] + (int)winZipAesCrypto._providedPv[1] * 256);
			if (password != null)
			{
				winZipAesCrypto.PasswordVerificationGenerated = (short)((int)winZipAesCrypto.GeneratedPV[0] + (int)winZipAesCrypto.GeneratedPV[1] * 256);
				if (winZipAesCrypto.PasswordVerificationGenerated != winZipAesCrypto.PasswordVerificationStored)
				{
					throw new BadPasswordException("bad password");
				}
			}
			return winZipAesCrypto;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004B27 File Offset: 0x00002D27
		public byte[] GeneratedPV
		{
			get
			{
				if (!this._cryptoGenerated)
				{
					this._GenerateCryptoBytes();
				}
				return this._generatedPv;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004B3D File Offset: 0x00002D3D
		public byte[] Salt
		{
			get
			{
				return this._Salt;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004B45 File Offset: 0x00002D45
		private int _KeyStrengthInBytes
		{
			get
			{
				return this._KeyStrengthInBits / 8;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004B4F File Offset: 0x00002D4F
		public int SizeOfEncryptionMetadata
		{
			get
			{
				return this._KeyStrengthInBytes / 2 + 10 + 2;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004BAE File Offset: 0x00002DAE
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004B60 File Offset: 0x00002D60
		public string Password
		{
			private get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				if (this._Password != null)
				{
					this.PasswordVerificationGenerated = (short)((int)this.GeneratedPV[0] + (int)this.GeneratedPV[1] * 256);
					if (this.PasswordVerificationGenerated != this.PasswordVerificationStored)
					{
						throw new BadPasswordException();
					}
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004BB8 File Offset: 0x00002DB8
		private void _GenerateCryptoBytes()
		{
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(this._Password, this.Salt, this.Rfc2898KeygenIterations);
			this._keyBytes = rfc2898DeriveBytes.GetBytes(this._KeyStrengthInBytes);
			this._MacInitializationVector = rfc2898DeriveBytes.GetBytes(this._KeyStrengthInBytes);
			this._generatedPv = rfc2898DeriveBytes.GetBytes(2);
			this._cryptoGenerated = true;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004C15 File Offset: 0x00002E15
		public byte[] KeyBytes
		{
			get
			{
				if (!this._cryptoGenerated)
				{
					this._GenerateCryptoBytes();
				}
				return this._keyBytes;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004C2B File Offset: 0x00002E2B
		public byte[] MacIv
		{
			get
			{
				if (!this._cryptoGenerated)
				{
					this._GenerateCryptoBytes();
				}
				return this._MacInitializationVector;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004C44 File Offset: 0x00002E44
		public void ReadAndVerifyMac(Stream s)
		{
			bool flag = false;
			this._StoredMac = new byte[10];
			s.Read(this._StoredMac, 0, this._StoredMac.Length);
			if (this._StoredMac.Length != this.CalculatedMac.Length)
			{
				flag = true;
			}
			if (!flag)
			{
				for (int i = 0; i < this._StoredMac.Length; i++)
				{
					if (this._StoredMac[i] != this.CalculatedMac[i])
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				throw new BadStateException("The MAC does not match.");
			}
		}

		// Token: 0x04000066 RID: 102
		internal byte[] _Salt;

		// Token: 0x04000067 RID: 103
		internal byte[] _providedPv;

		// Token: 0x04000068 RID: 104
		internal byte[] _generatedPv;

		// Token: 0x04000069 RID: 105
		internal int _KeyStrengthInBits;

		// Token: 0x0400006A RID: 106
		private byte[] _MacInitializationVector;

		// Token: 0x0400006B RID: 107
		private byte[] _StoredMac;

		// Token: 0x0400006C RID: 108
		private byte[] _keyBytes;

		// Token: 0x0400006D RID: 109
		private short PasswordVerificationStored;

		// Token: 0x0400006E RID: 110
		private short PasswordVerificationGenerated;

		// Token: 0x0400006F RID: 111
		private int Rfc2898KeygenIterations = 1000;

		// Token: 0x04000070 RID: 112
		private string _Password;

		// Token: 0x04000071 RID: 113
		private bool _cryptoGenerated;

		// Token: 0x04000072 RID: 114
		public byte[] CalculatedMac;
	}
}
