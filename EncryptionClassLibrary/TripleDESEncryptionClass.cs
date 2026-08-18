using System;
using System.Data;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class TripleDESEncryptionClass : IEncryption
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00005454 File Offset: 0x00003654
		public TripleDESEncryptionClass()
		{
			this.encryptionType = EncryptionType.TripleDES_192bit;
			this.SetupEncryption(null, null);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005454 File Offset: 0x00003654
		public TripleDESEncryptionClass(IEncryption encryption)
		{
			this.encryptionType = EncryptionType.TripleDES_192bit;
			this.SetupEncryption(null, null);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000546E File Offset: 0x0000366E
		public TripleDESEncryptionClass(EncryptionType _encryptionType)
		{
			this.encryptionType = _encryptionType;
			this.SetupEncryption(null, null);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005488 File Offset: 0x00003688
		public TripleDESEncryptionClass(byte[] _key, byte[] _iv)
		{
			this.encryptionType = ((_key.Length == 24) ? EncryptionType.TripleDES_192bit : EncryptionType.TripleDES_128bit);
			this.SetupEncryption(_key, _iv);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000054AC File Offset: 0x000036AC
		public TripleDESEncryptionClass(EncryptionType _encryptionType, byte[] _key, byte[] _iv)
		{
			this.encryptionType = _encryptionType;
			this.SetupEncryption(_key, _iv);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000054C8 File Offset: 0x000036C8
		public TripleDESEncryptionClass(EncryptionType encryptionType, string password)
		{
			this.encryptionType = encryptionType;
			byte[][] bytes = TripleDESEncryptionClass.GetBytes(encryptionType > EncryptionType.TripleDES_128bit, password);
			this.SetupEncryption(bytes[0], bytes[1]);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000054FC File Offset: 0x000036FC
		private void SetupEncryption(byte[] _key, byte[] _iv)
		{
			EncryptionType encryptionType = this.encryptionType;
			EncryptionType encryptionType2 = encryptionType;
			if (encryptionType2 > EncryptionType.TripleDES_192bit)
			{
				if (encryptionType2 != EncryptionType.TripleDES_192bit_RandomIv)
				{
					this.encryption = null;
				}
				else
				{
					this.encryption = ((_key == null) ? new TripleDESRandomIv() : new TripleDESRandomIv(_key));
				}
			}
			else
			{
				this.encryption = ((_key == null) ? new TripleDES() : new TripleDES(_key, _iv));
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00005559 File Offset: 0x00003759
		public EncryptionType Name
		{
			get
			{
				return this.encryptionType;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005561 File Offset: 0x00003761
		public Encoding Encoder
		{
			get
			{
				return this.encryption.Encoder;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005570 File Offset: 0x00003770
		public byte[] Encrypt(string plainText)
		{
			return this.encryption.Encrypt(plainText ?? "");
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005598 File Offset: 0x00003798
		public string EncryptToString(string plainText)
		{
			byte[] inArray = this.Encrypt(plainText);
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000055B8 File Offset: 0x000037B8
		public IBatchDecryptor GetBatchDecryptor()
		{
			return this.encryption.GetBatchDecryptor();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000055D8 File Offset: 0x000037D8
		public IBatchEncryptor GetBatchEncryptor()
		{
			return this.encryption.GetBatchEncryptor();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000055F8 File Offset: 0x000037F8
		public string Decrypt(string inputString)
		{
			return this.encryption.Decrypt(inputString);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005618 File Offset: 0x00003818
		public string Decrypt(byte[] inputInBytes)
		{
			return (inputInBytes.Length == 0) ? string.Empty : this.encryption.Decrypt(inputInBytes);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005644 File Offset: 0x00003844
		public string Decrypt(object drItem)
		{
			bool flag = drItem == DBNull.Value;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = drItem is byte[];
				if (flag2)
				{
					result = ((((byte[])drItem).Length == 0) ? string.Empty : this.Decrypt((byte[])drItem));
				}
				else
				{
					result = "";
				}
			}
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000569C File Offset: 0x0000389C
		private static byte[][] GetBytes(bool use192bit, string password)
		{
			return EncryptionFactory.GetBytesLegacy(password, use192bit ? 24 : 16, 8);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000056C0 File Offset: 0x000038C0
		public DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription)
		{
			return this.encryption.DecryptNameDataTableBatch(tSource, includeStudentNumberInNameDescription, false);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000056E0 File Offset: 0x000038E0
		public void DecryptDataTableBatchDynamicData(DataTable tSource, string colSaysWhetherToEncryptOrNot, string colEncrypted, string colTextToPlaceDecryptedText)
		{
			this.encryption.DecryptDataTableBatchDynamicData(tSource, colSaysWhetherToEncryptOrNot, colEncrypted, colTextToPlaceDecryptedText);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000056F4 File Offset: 0x000038F4
		public DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable tSource, params string[] colNamesToEncryptOrDecryptInLowerCase)
		{
			return this.encryption.EncryptOrDecryptNameDataTableBatch(encrypt, tSource, colNamesToEncryptOrDecryptInLowerCase);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005714 File Offset: 0x00003914
		public DataTable EncryptColumns(DataTable tSource, string[] colNames)
		{
			return this.encryption.EncryptColumns(tSource, colNames);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005734 File Offset: 0x00003934
		public DataTable DecryptColumns(DataTable tSource, string[] colNames)
		{
			return this.encryption.DecryptColumns(tSource, colNames);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005754 File Offset: 0x00003954
		public virtual DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription, bool firstNameThenLastName)
		{
			return this.encryption.DecryptNameDataTableBatch(tSource, includeStudentNumberInNameDescription, firstNameThenLastName);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005774 File Offset: 0x00003974
		public object[] EncryptBatch(out byte[] encryptedBytes, string stringToEncrypt, object[] oo)
		{
			return this.encryption.EncryptBatch(out encryptedBytes, stringToEncrypt, oo);
		}

		// Token: 0x0400002F RID: 47
		public EncryptionType encryptionType;

		// Token: 0x04000030 RID: 48
		private IEncryption encryption;
	}
}
