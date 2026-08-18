using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x02000002 RID: 2
	public class AESEncryption : IEncryption
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public EncryptionType Name
		{
			get
			{
				return EncryptionType.AES_256bit;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002053 File Offset: 0x00000253
		public Encoding Encoder
		{
			get
			{
				return Encoding.Unicode;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		public AESEncryption()
		{
			string password = "]sBnQM)jj$K<wd]Ry_Ep'Uf_`FG~z{7u.ZmhG:rqMPt$%7FTaFjb^=t(^sRtTC?Y!Yb<:^HY?(bt[A~[55cwZTCG>YDE/7NMJ7Xr;ctS%/.N3=nTs'*$<x2,R_Mm>d^']Xm`{w~@>8WE^F8En&%5LN<={7[7#.PCb(%XjW)!;!^dFQ}57XNJ}-\\-gvZ\\pef;9N+}`{6Ytfz3GsQkw5gXJB]_$Xz$,6T? rs?U8#;E-GD>R4;<k=rx!/3hj-VVBY8E";
			this.key = EncryptionFactory.GetBytes(password, 32);
			this.iv = EncryptionFactory.GetBytes(password, 16);
			this.KeySize = this.key.Length * 8;
			this.BlockSize = this.iv.Length * 8;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B3 File Offset: 0x000002B3
		public AESEncryption(byte[] key, byte[] iv)
		{
			this.key = key;
			this.iv = iv;
			this.KeySize = key.Length * 8;
			this.BlockSize = iv.Length * 8;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E4 File Offset: 0x000002E4
		public byte[] Encrypt(string plainText)
		{
			Aes aesCryptoProvider = this.GetAesCryptoProvider();
			byte[] bytes = this.Encoder.GetBytes(plainText);
			byte[] result;
			using (ICryptoTransform cryptoTransform = aesCryptoProvider.CreateEncryptor())
			{
				result = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002138 File Offset: 0x00000338
		public string EncryptToString(string plainText)
		{
			Aes aesCryptoProvider = this.GetAesCryptoProvider();
			byte[] bytes = this.Encoder.GetBytes(plainText);
			string result;
			using (ICryptoTransform cryptoTransform = aesCryptoProvider.CreateEncryptor())
			{
				byte[] inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
				result = Convert.ToBase64String(inArray);
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002198 File Offset: 0x00000398
		public string Decrypt(string encryptedText)
		{
			Aes aesCryptoProvider = this.GetAesCryptoProvider();
			byte[] array = Convert.FromBase64String(encryptedText);
			string @string;
			using (ICryptoTransform cryptoTransform = aesCryptoProvider.CreateDecryptor())
			{
				byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
				@string = this.Encoder.GetString(bytes);
			}
			return @string;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F8 File Offset: 0x000003F8
		public string Decrypt(byte[] encryptedData)
		{
			Aes aesCryptoProvider = this.GetAesCryptoProvider();
			string @string;
			using (ICryptoTransform cryptoTransform = aesCryptoProvider.CreateDecryptor())
			{
				byte[] bytes = cryptoTransform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
				@string = this.Encoder.GetString(bytes);
			}
			return @string;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000224C File Offset: 0x0000044C
		public IBatchDecryptor GetBatchDecryptor()
		{
			Aes aesCryptoProvider = this.GetAesCryptoProvider();
			return new BatchEncryption(aesCryptoProvider.CreateDecryptor(), this.Encoder);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002278 File Offset: 0x00000478
		public IBatchEncryptor GetBatchEncryptor()
		{
			Aes aesCryptoProvider = this.GetAesCryptoProvider();
			return new BatchEncryption(aesCryptoProvider.CreateEncryptor(), this.Encoder);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022A4 File Offset: 0x000004A4
		public DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription)
		{
			return this.DecryptNameDataTableBatch(tSource, includeStudentNumberInNameDescription, false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022C0 File Offset: 0x000004C0
		public void DecryptDataTableBatchDynamicData(DataTable tSource, string colSaysWhetherToEncryptOrNot, string colEncrypted, string colTextToPlaceDecryptedText)
		{
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
			using (IBatchDecryptor batchDecryptor = this.GetBatchDecryptor())
			{
				foreach (object obj2 in tSource.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					bool flag2 = dataRow2[colSaysWhetherToEncryptOrNot] != DBNull.Value && Convert.ToBoolean(dataRow2[colSaysWhetherToEncryptOrNot]) && dataRow2[colEncrypted] != DBNull.Value;
					bool flag3 = flag2;
					if (flag3)
					{
						byte[] array = (byte[])dataRow2[colEncrypted];
						bool flag4 = array != null && array.Length != 0;
						if (flag4)
						{
							dataRow2[colTextToPlaceDecryptedText] = batchDecryptor.Decrypt(array);
						}
					}
				}
			}
			tSource.Columns.Remove(colEncrypted);
			tSource.Columns.Remove(colSaysWhetherToEncryptOrNot);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024BC File Offset: 0x000006BC
		public DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable tSource, params string[] colNamesToEncryptOrDecryptInLowerCase)
		{
			return this.EncryptOrDecryptNameDataTableBatch(encrypt, tSource, true, colNamesToEncryptOrDecryptInLowerCase);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024D8 File Offset: 0x000006D8
		public DataTable EncryptColumns(DataTable tSource, params string[] colNames)
		{
			return this.EncryptOrDecryptNameDataTableBatch(true, tSource, false, colNames);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024F4 File Offset: 0x000006F4
		public DataTable DecryptColumns(DataTable tSource, params string[] colNames)
		{
			return this.EncryptOrDecryptNameDataTableBatch(false, tSource, false, colNames);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002510 File Offset: 0x00000710
		public DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription, bool firstNameThenLastName)
		{
			Type type = Type.GetType("System.Int32");
			Type type2 = Type.GetType("System.String");
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
			using (IBatchDecryptor batchDecryptor = this.GetBatchDecryptor())
			{
				foreach (object obj in tSource.Rows)
				{
					DataRow dataRow2 = (DataRow)obj;
					bool flag = dataRow2[0] != DBNull.Value;
					if (flag)
					{
						int num3 = (int)dataRow2[0];
						byte[] data = (dataRow2[columnIndex] != DBNull.Value) ? ((byte[])dataRow2[columnIndex]) : null;
						byte[] data2 = (dataRow2[columnIndex2] != DBNull.Value) ? ((byte[])dataRow2[columnIndex2]) : null;
						byte[] data3 = (dataRow2[columnIndex3] != DBNull.Value) ? ((byte[])dataRow2[columnIndex3]) : null;
						bool flag2 = num >= 0;
						byte[] data4;
						if (flag2)
						{
							data4 = ((dataRow2[num] != DBNull.Value) ? ((byte[])dataRow2[num]) : null);
						}
						else
						{
							data4 = null;
						}
						int num4 = (num2 >= 0) ? ((int)dataRow2[num2]) : 0;
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3[0] = num3;
						string text = batchDecryptor.Decrypt(data);
						dataRow3[2] = text;
						string value = batchDecryptor.Decrypt(data2);
						dataRow3[3] = value;
						string value2 = batchDecryptor.Decrypt(data3);
						string text2 = batchDecryptor.Decrypt(data4);
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
			}
			return new DataTable[]
			{
				dataTable,
				dataTable2
			};
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A14 File Offset: 0x00000C14
		public object[] EncryptBatch(out byte[] encryptedBytes, string stringToEncrypt, object[] oo)
		{
			bool flag = oo == null;
			if (flag)
			{
				oo = new object[]
				{
					this.GetBatchEncryptor()
				};
			}
			IBatchEncryptor batchEncryptor = (IBatchEncryptor)oo[0];
			encryptedBytes = batchEncryptor.Encrypt(stringToEncrypt);
			return oo;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002A54 File Offset: 0x00000C54
		private Aes GetAesCryptoProvider()
		{
			return new AesManaged
			{
				BlockSize = this.BlockSize,
				KeySize = this.KeySize,
				IV = this.iv,
				Key = this.key,
				Mode = CipherMode.CBC,
				Padding = PaddingMode.PKCS7
			};
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002AB0 File Offset: 0x00000CB0
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
					BatchEncryption batchEncryption = encrypt ? ((BatchEncryption)this.GetBatchEncryptor()) : ((BatchEncryption)this.GetBatchDecryptor());
					using (batchEncryption)
					{
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
										dataRow[customPair2.Item1] = batchEncryption.Encrypt(text);
									}
								}
								else
								{
									bool flag3 = dataRow[customPair2.Item2] != DBNull.Value;
									if (flag3)
									{
										byte[] data = (byte[])dataRow[customPair2.Item2];
										dataRow[customPair2.Item1] = batchEncryption.Decrypt(data);
									}
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

		// Token: 0x06000014 RID: 20 RVA: 0x00002F38 File Offset: 0x00001138
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

		// Token: 0x04000001 RID: 1
		protected byte[] key;

		// Token: 0x04000002 RID: 2
		protected byte[] iv;

		// Token: 0x04000003 RID: 3
		protected readonly int KeySize;

		// Token: 0x04000004 RID: 4
		protected readonly int BlockSize;
	}
}
