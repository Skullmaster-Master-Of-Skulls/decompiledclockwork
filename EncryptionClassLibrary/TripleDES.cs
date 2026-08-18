using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class TripleDES : IEncryption
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public EncryptionType Name
		{
			get
			{
				return (this.key.Length == 24) ? EncryptionType.TripleDES_192bit : EncryptionType.TripleDES_128bit;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003CCA File Offset: 0x00001ECA
		public Encoding Encoder
		{
			get
			{
				return new UTF8Encoding();
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003CD1 File Offset: 0x00001ED1
		public TripleDES()
		{
			this.key = new byte[]
			{
				253,
				112,
				200,
				23,
				1,
				145,
				221,
				98,
				89,
				12,
				14,
				56,
				19,
				230,
				101,
				71,
				201,
				231,
				4,
				32,
				154,
				245,
				123,
				1
			};
			this.iv = new byte[]
			{
				1,
				59,
				131,
				251,
				210,
				187,
				26,
				59
			};
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003D0A File Offset: 0x00001F0A
		public TripleDES(byte[] _key, byte[] _iv)
		{
			this.key = _key;
			this.iv = _iv;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003D24 File Offset: 0x00001F24
		public TripleDESEncryptionClass GetTripleDesEncryptionClass()
		{
			return new TripleDESEncryptionClass(EncryptionType.TripleDES_192bit, this.key, this.iv);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003D48 File Offset: 0x00001F48
		public byte[] Encrypt(string plainText)
		{
			byte[] bytes = this.Encoder.GetBytes(plainText);
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			ICryptoTransform transform = tripleDESCryptoServiceProvider.CreateEncryptor(this.key, this.iv);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			memoryStream.Position = 0L;
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Read(array, 0, (int)memoryStream.Length);
			cryptoStream.Close();
			return array;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003DDC File Offset: 0x00001FDC
		public string EncryptToString(string plainText)
		{
			byte[] inArray = this.Encrypt(plainText);
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003DFC File Offset: 0x00001FFC
		public string Decrypt(string inputString)
		{
			byte[] inputInBytes = Convert.FromBase64String(inputString);
			return this.Decrypt(inputInBytes);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003E1C File Offset: 0x0000201C
		public string Decrypt(byte[] inputInBytes)
		{
			return this.Decrypt(inputInBytes, true);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003E38 File Offset: 0x00002038
		private string Decrypt(byte[] inputInBytes, bool tryRepairBadData)
		{
			string result;
			try
			{
				bool flag = inputInBytes == null;
				if (flag)
				{
					result = "";
				}
				else
				{
					TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
					ICryptoTransform transform = tripleDESCryptoServiceProvider.CreateDecryptor(this.key, this.iv);
					MemoryStream memoryStream = new MemoryStream();
					CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
					cryptoStream.Write(inputInBytes, 0, inputInBytes.Length);
					cryptoStream.FlushFinalBlock();
					memoryStream.Position = 0L;
					result = this.Encoder.GetString(memoryStream.ToArray());
				}
			}
			catch
			{
				try
				{
					result = this.TryDecryptNoPadding(inputInBytes);
				}
				catch
				{
					string text = tryRepairBadData ? this.TryRepairBadData(inputInBytes, (byte[] f) => this.Decrypt(f, false)) : null;
					result = (text ?? ".?.");
				}
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003F10 File Offset: 0x00002110
		private string TryRepairBadData(byte[] inputInBytes, Func<byte[], string> normalDecryptFunction)
		{
			bool flag = inputInBytes == null || inputInBytes.Length < 1 || inputInBytes.Length % 8 != 7;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					byte[] array = new byte[inputInBytes.Length + 1];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = inputInBytes[i];
					}
					result = normalDecryptFunction(array);
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003F88 File Offset: 0x00002188
		public void DecryptDataTableBatchDynamicData(DataTable tSource, string colSaysWhetherToEncryptOrNot, string colEncrypted, string colTextToPlaceDecryptedText)
		{
			byte[] array = new byte[0];
			Type type = array.GetType();
			bool flag = !tSource.Columns.Contains(colTextToPlaceDecryptedText);
			if (flag)
			{
				tSource.Columns.Add(colTextToPlaceDecryptedText);
			}
			bool readOnly = tSource.Columns[colTextToPlaceDecryptedText].ReadOnly;
			if (readOnly)
			{
				string text = tSource.Columns[colTextToPlaceDecryptedText].ColumnName + "2x";
				tSource.Columns[colTextToPlaceDecryptedText].ColumnName = text;
				tSource.Columns.Add(colTextToPlaceDecryptedText);
				foreach (object obj in tSource.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataRow[colTextToPlaceDecryptedText] = dataRow[text];
				}
				tSource.Columns.Remove(text);
			}
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor(this.key, this.iv);
			foreach (object obj2 in tSource.Rows)
			{
				DataRow dataRow2 = (DataRow)obj2;
				bool flag2 = dataRow2[colSaysWhetherToEncryptOrNot] != DBNull.Value && Convert.ToBoolean(dataRow2[colSaysWhetherToEncryptOrNot]) && dataRow2[colEncrypted] != DBNull.Value;
				bool flag3 = flag2;
				if (flag3)
				{
					byte[] array2 = (byte[])dataRow2[colEncrypted];
					bool flag4 = array2 != null && array2.Length != 0;
					if (flag4)
					{
						dataRow2[colTextToPlaceDecryptedText] = this.DecryptMini(array2, this.Encoder, tripleDESCryptoServiceProvider, cryptoTransform);
					}
				}
			}
			tSource.Columns.Remove(colEncrypted);
			tSource.Columns.Remove(colSaysWhetherToEncryptOrNot);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004198 File Offset: 0x00002398
		public IBatchDecryptor GetBatchDecryptor()
		{
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor(this.key, this.iv);
			return new BatchEncryption(cryptoTransform, this.Encoder);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000041D0 File Offset: 0x000023D0
		public IBatchEncryptor GetBatchEncryptor()
		{
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor(this.key, this.iv);
			return new BatchEncryption(cryptoTransform, this.Encoder);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004208 File Offset: 0x00002408
		private string GetUniqueColName(DataTable t, ref IList<string> invalidColNames, string proposedColName)
		{
			string item = proposedColName.ToLower();
			bool flag = !t.Columns.Contains(proposedColName) && !invalidColNames.Contains(item);
			string result;
			if (flag)
			{
				invalidColNames.Add(item);
				result = proposedColName;
			}
			else
			{
				for (int i = 0; i < 100000; i++)
				{
					string text = proposedColName + "_" + i.ToString();
					item = text.ToLower();
					bool flag2 = t.Columns.Contains(text) || invalidColNames.Contains(item);
					if (!flag2)
					{
						invalidColNames.Add(item);
						return text;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000042B4 File Offset: 0x000024B4
		public DataTable DecryptColumns(DataTable t, params string[] colNames)
		{
			return this.EncryptOrDecryptNameDataTableBatch(false, t, false, colNames);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000042D0 File Offset: 0x000024D0
		public DataTable EncryptColumns(DataTable t, params string[] colNames)
		{
			return this.EncryptOrDecryptNameDataTableBatch(true, t, false, colNames);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000042EC File Offset: 0x000024EC
		public DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable tSource, params string[] colNamesToEncryptOrDecryptInLowerCase)
		{
			return this.EncryptOrDecryptNameDataTableBatch(encrypt, tSource, true, colNamesToEncryptOrDecryptInLowerCase);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004308 File Offset: 0x00002508
		private DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable tSource, bool returnCopyOfOriginalTable, params string[] colNamesToEncryptOrDecryptInLowerCase)
		{
			tSource.BeginLoadData();
			DataTable result;
			try
			{
				Type byteArrayType = typeof(byte[]);
				Type typeFromHandle = typeof(string);
				List<string> list = (from g in colNamesToEncryptOrDecryptInLowerCase
				where tSource.Columns.Contains(g) && ((encrypt && tSource.Columns[g].DataType != byteArrayType) || (!encrypt && tSource.Columns[g].DataType == byteArrayType))
				select g into h
				select tSource.Columns[h].ColumnName).ToList<string>();
				bool flag = list.Count < 1;
				if (flag)
				{
					result = (returnCopyOfOriginalTable ? tSource.Copy() : tSource);
				}
				else
				{
					IList<string> usedNewColNames = new List<string>();
					List<CustomPair<string, string>> list2 = (from g in list
					select new CustomPair<string, string>(g, this.GetUniqueColName(tSource, ref usedNewColNames, g + "_original")) into h
					where !string.IsNullOrEmpty(h.Item2)
					select h).ToList<CustomPair<string, string>>();
					foreach (CustomPair<string, string> customPair in list2)
					{
						DataColumn dataColumn = tSource.Columns[customPair.Item1];
						int ordinal = tSource.Columns.IndexOf(dataColumn);
						dataColumn.ColumnName = customPair.Item2;
						DataColumn dataColumn2 = tSource.Columns.Add(customPair.Item1, encrypt ? byteArrayType : typeFromHandle);
						int ordinal2 = tSource.Columns.IndexOf(dataColumn2);
						try
						{
							dataColumn.SetOrdinal(ordinal2);
							dataColumn2.SetOrdinal(ordinal);
						}
						catch (Exception ex)
						{
						}
					}
					TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
					ICryptoTransform cryptoTransform = encrypt ? tripleDESCryptoServiceProvider.CreateEncryptor(this.key, this.iv) : tripleDESCryptoServiceProvider.CreateDecryptor(this.key, this.iv);
					foreach (object obj in tSource.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						foreach (CustomPair<string, string> customPair2 in list2)
						{
							bool encrypt2 = encrypt;
							if (encrypt2)
							{
								string text = dataRow[customPair2.Item2].ToString();
								bool flag2 = text.Length > 0;
								if (flag2)
								{
									dataRow[customPair2.Item1] = this.EncryptMini(text, this.Encoder, tripleDESCryptoServiceProvider, cryptoTransform);
								}
							}
							else
							{
								bool flag3 = dataRow[customPair2.Item2] != DBNull.Value;
								if (flag3)
								{
									byte[] inputInBytes = (byte[])dataRow[customPair2.Item2];
									dataRow[customPair2.Item1] = this.DecryptMini(inputInBytes, this.Encoder, tripleDESCryptoServiceProvider, cryptoTransform);
								}
							}
						}
					}
					foreach (CustomPair<string, string> customPair3 in list2)
					{
						tSource.Columns.Remove(customPair3.Item2);
					}
					result = (returnCopyOfOriginalTable ? tSource.Copy() : tSource);
				}
			}
			finally
			{
				tSource.EndLoadData();
			}
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000478C File Offset: 0x0000298C
		public object[] EncryptBatch(out byte[] encryptedBytes, string stringToEncrypt, object[] oo)
		{
			bool flag = oo == null;
			if (flag)
			{
				oo = new object[3];
				oo[0] = this.Encoder;
				oo[1] = new TripleDESCryptoServiceProvider();
				oo[2] = ((TripleDESCryptoServiceProvider)oo[1]).CreateEncryptor(this.key, this.iv);
			}
			Encoding encoding = (Encoding)oo[0];
			TripleDESCryptoServiceProvider tdesProvider = (TripleDESCryptoServiceProvider)oo[1];
			ICryptoTransform cryptoTransform = (ICryptoTransform)oo[2];
			encryptedBytes = this.EncryptMini(stringToEncrypt, encoding, tdesProvider, cryptoTransform);
			return oo;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004808 File Offset: 0x00002A08
		public DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription)
		{
			return this.DecryptNameDataTableBatch(tSource, includeStudentNumberInNameDescription, false);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004823 File Offset: 0x00002A23
		public void BeginBatchTransaction()
		{
			this.batchUtf8encoder = this.Encoder;
			this.batchTdesProvider = new TripleDESCryptoServiceProvider();
			this.batchCryptoTransform = this.batchTdesProvider.CreateDecryptor(this.key, this.iv);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000485A File Offset: 0x00002A5A
		public void EndBatchTransaction()
		{
			this.batchUtf8encoder = null;
			this.batchTdesProvider = null;
			this.batchCryptoTransform.Dispose();
			this.batchCryptoTransform = null;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004880 File Offset: 0x00002A80
		public string BatchDecrypt(byte[] bytes)
		{
			bool flag = bytes == null || bytes.Length < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = this.DecryptMini(bytes, this.batchUtf8encoder, this.batchTdesProvider, this.batchCryptoTransform);
			}
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000048C4 File Offset: 0x00002AC4
		public DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription, bool firstNameThenLastName)
		{
			Type type = Type.GetType("System.Int32");
			Type type2 = Type.GetType("System.String");
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor(this.key, this.iv);
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("personid", type);
			dataTable.Columns.Add("lastfirstname", type2);
			dataTable.Columns.Add("firstname", type2);
			dataTable.Columns.Add("lastname", type2);
			dataTable.Columns.Add("middlename", type2);
			DataTable dataTable2 = new DataTable();
			dataTable2.Columns.Add("personid", type);
			dataTable2.Columns.Add("student_no", type2);
			dataTable.Columns.Add("activated", type);
			dataTable2.Columns.Add("activated", type);
			int columnIndex = tSource.Columns.IndexOf("firstname");
			int columnIndex2 = tSource.Columns.IndexOf("lastname");
			int columnIndex3 = tSource.Columns.IndexOf("student_no");
			int num = tSource.Columns.IndexOf("middlename");
			int num2 = tSource.Columns.IndexOf("isactivatedcurrentyear");
			DataRow dataRow = dataTable.NewRow();
			dataRow[0] = -1;
			dataRow[1] = "";
			dataRow[2] = "";
			dataRow[3] = "";
			dataRow[4] = "";
			dataRow[5] = 0;
			dataTable.Rows.Add(dataRow);
			dataRow = dataTable2.NewRow();
			dataRow[0] = -1;
			dataRow[1] = "";
			dataRow[2] = 0;
			dataTable2.Rows.Add(dataRow);
			dataTable.Columns.Add("student_no", type2);
			foreach (object obj in tSource.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				bool flag = dataRow2[0] != DBNull.Value;
				if (flag)
				{
					int num3 = (int)dataRow2[0];
					byte[] inputInBytes = (dataRow2[columnIndex] != DBNull.Value) ? ((byte[])dataRow2[columnIndex]) : null;
					byte[] inputInBytes2 = (dataRow2[columnIndex2] != DBNull.Value) ? ((byte[])dataRow2[columnIndex2]) : null;
					byte[] inputInBytes3 = (dataRow2[columnIndex3] != DBNull.Value) ? ((byte[])dataRow2[columnIndex3]) : null;
					bool flag2 = num >= 0;
					byte[] inputInBytes4;
					if (flag2)
					{
						inputInBytes4 = ((dataRow2[num] != DBNull.Value) ? ((byte[])dataRow2[num]) : null);
					}
					else
					{
						inputInBytes4 = null;
					}
					int num4 = (num2 >= 0) ? ((int)dataRow2[num2]) : 0;
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3[0] = num3;
					string text = this.DecryptMini(inputInBytes, this.Encoder, tripleDESCryptoServiceProvider, cryptoTransform);
					dataRow3[2] = text;
					string value = this.DecryptMini(inputInBytes2, this.Encoder, tripleDESCryptoServiceProvider, cryptoTransform);
					dataRow3[3] = value;
					string value2 = this.DecryptMini(inputInBytes3, this.Encoder, tripleDESCryptoServiceProvider, cryptoTransform);
					string text2 = this.DecryptMini(inputInBytes4, this.Encoder, tripleDESCryptoServiceProvider, cryptoTransform);
					dataRow3[4] = text2;
					dataRow3[5] = num4;
					dataRow3["student_no"] = value2;
					StringBuilder stringBuilder;
					if (firstNameThenLastName)
					{
						stringBuilder = new StringBuilder(text);
						stringBuilder.Append(" ");
						stringBuilder.Append(value);
					}
					else
					{
						stringBuilder = new StringBuilder(value);
						bool flag3 = text.Length > 0;
						if (flag3)
						{
							stringBuilder.Append(", ");
							stringBuilder.Append(text);
						}
						bool flag4 = text2.Length > 0;
						if (flag4)
						{
							stringBuilder.Append(" ");
							stringBuilder.Append(text2);
						}
						if (includeStudentNumberInNameDescription)
						{
							stringBuilder.Append(" . ");
							stringBuilder.Append(value2);
						}
					}
					dataRow3[1] = stringBuilder.ToString();
					dataTable.Rows.Add(dataRow3);
					dataRow3 = dataTable2.NewRow();
					dataRow3[0] = num3;
					dataRow3[1] = value2;
					dataRow3[2] = num4;
					dataTable2.Rows.Add(dataRow3);
				}
			}
			return new DataTable[]
			{
				dataTable,
				dataTable2
			};
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004DE0 File Offset: 0x00002FE0
		private string DecryptMini(byte[] inputInBytes, Encoding encoding, TripleDESCryptoServiceProvider tdesProvider, ICryptoTransform cryptoTransform)
		{
			return this.DecryptMini(inputInBytes, encoding, tdesProvider, cryptoTransform, true);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004E00 File Offset: 0x00003000
		private string DecryptMini(byte[] inputInBytes, Encoding encoding, TripleDESCryptoServiceProvider tdesProvider, ICryptoTransform cryptoTransform, bool tryRepairBadData)
		{
			string result;
			try
			{
				bool flag = inputInBytes == null;
				if (flag)
				{
					result = "";
				}
				else
				{
					MemoryStream memoryStream = new MemoryStream();
					CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
					cryptoStream.Write(inputInBytes, 0, inputInBytes.Length);
					cryptoStream.FlushFinalBlock();
					memoryStream.Position = 0L;
					result = encoding.GetString(memoryStream.ToArray());
				}
			}
			catch
			{
				try
				{
					result = this.TryDecryptNoPadding(inputInBytes);
				}
				catch
				{
					string text = tryRepairBadData ? this.TryRepairBadData(inputInBytes, (byte[] g) => this.DecryptMini(g, encoding, tdesProvider, cryptoTransform, false)) : null;
					result = (text ?? ".?.");
				}
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004EE4 File Offset: 0x000030E4
		private string TryDecryptNoPadding(byte[] inputInBytes)
		{
			bool flag = inputInBytes == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				ICryptoTransform transform = new TripleDESCryptoServiceProvider
				{
					Padding = PaddingMode.None
				}.CreateDecryptor(this.key, this.iv);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				cryptoStream.Write(inputInBytes, 0, inputInBytes.Length);
				cryptoStream.FlushFinalBlock();
				memoryStream.Position = 0L;
				result = this.Encoder.GetString(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004F68 File Offset: 0x00003168
		private byte[] EncryptMini(string inputString, Encoding encoding, TripleDESCryptoServiceProvider tdesProvider, ICryptoTransform cryptoTransform)
		{
			bool flag = inputString == null || inputString.Trim().Length < 1;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] bytes = encoding.GetBytes(inputString);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				memoryStream.Position = 0L;
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Read(array, 0, (int)memoryStream.Length);
				cryptoStream.Close();
				result = array;
			}
			return result;
		}

		// Token: 0x04000020 RID: 32
		protected byte[] key;

		// Token: 0x04000021 RID: 33
		protected byte[] iv;

		// Token: 0x04000022 RID: 34
		private Encoding batchUtf8encoder;

		// Token: 0x04000023 RID: 35
		private TripleDESCryptoServiceProvider batchTdesProvider;

		// Token: 0x04000024 RID: 36
		private ICryptoTransform batchCryptoTransform;
	}
}
