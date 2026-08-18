using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000239 RID: 569
	internal class EnclaveDelegate
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x000F31E8 File Offset: 0x000F25E8
		public static EnclaveDelegate Instance
		{
			get
			{
				return EnclaveDelegate._EnclaveDelegate;
			}
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000F31FC File Offset: 0x000F25FC
		private EnclaveDelegate()
		{
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000F321C File Offset: 0x000F261C
		internal EnclaveDelegate.EnclavePackage GenerateEnclavePackage(Dictionary<int, SqlTceCipherInfoEntry> keysTobeSentToEnclave, string queryText, string enclaveType, string serverName, string enclaveAttestationUrl)
		{
			SqlEnclaveSession sqlEnclaveSession = null;
			long enclaveSessionCounter;
			try
			{
				this.GetEnclaveSession(enclaveType, serverName, enclaveAttestationUrl, out sqlEnclaveSession, out enclaveSessionCounter, true);
			}
			catch (Exception ex)
			{
				throw new EnclaveDelegate.RetriableEnclaveQueryExecutionException(ex.Message, ex);
			}
			List<ColumnEncryptionKeyInfo> decryptedKeysToBeSentToEnclave = this.GetDecryptedKeysToBeSentToEnclave(keysTobeSentToEnclave, serverName);
			byte[] queryStringHashBytes = this.ComputeQueryStringHash(queryText);
			byte[] bytePackage = this.GenerateBytePackageForKeys(enclaveSessionCounter, queryStringHashBytes, decryptedKeysToBeSentToEnclave);
			byte[] sessionKey = sqlEnclaveSession.GetSessionKey();
			byte[] array = this.EncryptBytePackage(bytePackage, sessionKey, serverName);
			byte[] bytes = BitConverter.GetBytes(sqlEnclaveSession.SessionId);
			byte[] enclavePackageBytes = this.CombineByteArrays(new byte[][]
			{
				bytes,
				array
			});
			return new EnclaveDelegate.EnclavePackage(enclavePackageBytes, sqlEnclaveSession);
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000F32CC File Offset: 0x000F26CC
		internal void InvalidateEnclaveSession(string enclaveType, string serverName, string EnclaveAttestationUrl, SqlEnclaveSession enclaveSession)
		{
			SqlColumnEncryptionEnclaveProvider enclaveProvider = this.GetEnclaveProvider(enclaveType);
			enclaveProvider.InvalidateEnclaveSession(serverName, EnclaveAttestationUrl, enclaveSession);
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000F32EC File Offset: 0x000F26EC
		internal void GetEnclaveSession(string enclaveType, string serverName, string enclaveAttestationUrl, out SqlEnclaveSession sqlEnclaveSession)
		{
			long num;
			this.GetEnclaveSession(enclaveType, serverName, enclaveAttestationUrl, out sqlEnclaveSession, out num, false);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000F3308 File Offset: 0x000F2708
		private void GetEnclaveSession(string enclaveType, string serverName, string enclaveAttestationUrl, out SqlEnclaveSession sqlEnclaveSession, out long counter, bool throwIfNull)
		{
			SqlColumnEncryptionEnclaveProvider enclaveProvider = this.GetEnclaveProvider(enclaveType);
			enclaveProvider.GetEnclaveSession(serverName, enclaveAttestationUrl, out sqlEnclaveSession, out counter);
			if (throwIfNull && sqlEnclaveSession == null)
			{
				throw SQL.NullEnclaveSessionDuringQueryExecution(enclaveType, enclaveAttestationUrl);
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000F333C File Offset: 0x000F273C
		internal SqlEnclaveAttestationParameters GetAttestationParameters(string enclaveType, string serverName, string enclaveAttestationUrl)
		{
			SqlColumnEncryptionEnclaveProvider enclaveProvider = this.GetEnclaveProvider(enclaveType);
			return enclaveProvider.GetAttestationParameters();
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000F3358 File Offset: 0x000F2758
		internal byte[] GetSerializedAttestationParameters(SqlEnclaveAttestationParameters sqlEnclaveAttestationParameters, string enclaveType)
		{
			int protocol = sqlEnclaveAttestationParameters.Protocol;
			byte[] uintBytes = this.GetUintBytes(enclaveType, protocol, "attestationProtocol");
			if (uintBytes == null)
			{
				throw SQL.NullArgumentInternal("attestationProtocolBytes", EnclaveDelegate.ClassName, EnclaveDelegate.GetSerializedAttestationParametersName);
			}
			byte[] input = sqlEnclaveAttestationParameters.GetInput();
			byte[] uintBytes2 = this.GetUintBytes(enclaveType, input.Length, "attestationProtocolInputLength");
			if (uintBytes2 == null)
			{
				throw SQL.NullArgumentInternal("attestationProtocolInputLengthBytes", EnclaveDelegate.ClassName, EnclaveDelegate.GetSerializedAttestationParametersName);
			}
			byte[] array = sqlEnclaveAttestationParameters.ClientDiffieHellmanKey.Key.Export(CngKeyBlobFormat.EccPublicBlob);
			byte[] uintBytes3 = this.GetUintBytes(enclaveType, array.Length, "clientDHPublicKeyLength");
			if (uintBytes3 == null)
			{
				throw SQL.NullArgumentInternal("clientDHPublicKeyLengthBytes", EnclaveDelegate.ClassName, EnclaveDelegate.GetSerializedAttestationParametersName);
			}
			return this.CombineByteArrays(new byte[][]
			{
				uintBytes,
				uintBytes2,
				input,
				uintBytes3,
				array
			});
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000F342C File Offset: 0x000F282C
		private byte[] GetUintBytes(string enclaveType, int intValue, string variableName)
		{
			byte[] bytes;
			try
			{
				uint value = Convert.ToUInt32(intValue);
				bytes = BitConverter.GetBytes(value);
			}
			catch (Exception innerException)
			{
				throw SQL.InvalidAttestationParameterUnableToConvertToUnsignedInt(variableName, intValue, enclaveType, innerException);
			}
			return bytes;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000F3474 File Offset: 0x000F2874
		internal void CreateEnclaveSession(string enclaveType, string serverName, string attestationUrl, byte[] attestationInfo, SqlEnclaveAttestationParameters attestationParameters)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				SqlColumnEncryptionEnclaveProvider enclaveProvider = this.GetEnclaveProvider(enclaveType);
				SqlEnclaveSession sqlEnclaveSession = null;
				long num;
				enclaveProvider.GetEnclaveSession(serverName, attestationUrl, out sqlEnclaveSession, out num);
				if (sqlEnclaveSession == null)
				{
					enclaveProvider.CreateEnclaveSession(attestationInfo, attestationParameters.ClientDiffieHellmanKey, attestationUrl, serverName, out sqlEnclaveSession, out num);
					if (sqlEnclaveSession == null)
					{
						throw SQL.NullEnclaveSessionReturnedFromProvider(enclaveType, attestationUrl);
					}
				}
			}
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000F34F8 File Offset: 0x000F28F8
		private SqlColumnEncryptionEnclaveProvider GetEnclaveProvider(string enclaveType)
		{
			if (SqlConnection.sqlColumnEncryptionEnclaveProviderConfigurationManager == null)
			{
				throw SQL.EnclaveProvidersNotConfiguredForEnclaveBasedQuery();
			}
			SqlColumnEncryptionEnclaveProvider sqlColumnEncryptionEnclaveProvider = SqlConnection.sqlColumnEncryptionEnclaveProviderConfigurationManager.GetSqlColumnEncryptionEnclaveProvider(enclaveType);
			if (sqlColumnEncryptionEnclaveProvider == null)
			{
				throw SQL.EnclaveProviderNotFound(enclaveType);
			}
			return sqlColumnEncryptionEnclaveProvider;
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000F352C File Offset: 0x000F292C
		private List<ColumnEncryptionKeyInfo> GetDecryptedKeysToBeSentToEnclave(Dictionary<int, SqlTceCipherInfoEntry> keysTobeSentToEnclave, string serverName)
		{
			List<ColumnEncryptionKeyInfo> list = new List<ColumnEncryptionKeyInfo>();
			foreach (SqlTceCipherInfoEntry value in keysTobeSentToEnclave.Values)
			{
				SqlClientSymmetricKey sqlClientSymmetricKey = null;
				SqlEncryptionKeyInfo? sqlEncryptionKeyInfo = null;
				SqlSecurityUtility.DecryptSymmetricKey(new SqlTceCipherInfoEntry?(value), serverName, out sqlClientSymmetricKey, out sqlEncryptionKeyInfo);
				if (sqlClientSymmetricKey == null)
				{
					throw SQL.NullArgumentInternal("sqlClientSymmetricKey", EnclaveDelegate.ClassName, EnclaveDelegate.GetDecryptedKeysToBeSentToEnclaveName);
				}
				if (value.ColumnEncryptionKeyValues == null)
				{
					throw SQL.NullArgumentInternal("ColumnEncryptionKeyValues", EnclaveDelegate.ClassName, EnclaveDelegate.GetDecryptedKeysToBeSentToEnclaveName);
				}
				if (value.ColumnEncryptionKeyValues.Count <= 0)
				{
					throw SQL.ColumnEncryptionKeysNotFound();
				}
				list.Add(new ColumnEncryptionKeyInfo(sqlClientSymmetricKey.RootKey, value.ColumnEncryptionKeyValues[0].databaseId, value.ColumnEncryptionKeyValues[0].cekMdVersion, value.ColumnEncryptionKeyValues[0].cekId));
			}
			return list;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000F3640 File Offset: 0x000F2A40
		private byte[] GenerateBytePackageForKeys(long enclaveSessionCounter, byte[] queryStringHashBytes, List<ColumnEncryptionKeyInfo> keys)
		{
			byte[] array = Guid.NewGuid().ToByteArray();
			byte[] bytes = BitConverter.GetBytes(enclaveSessionCounter);
			int num = array.Length;
			num += bytes.Length;
			num += queryStringHashBytes.Length;
			foreach (ColumnEncryptionKeyInfo columnEncryptionKeyInfo in keys)
			{
				num += columnEncryptionKeyInfo.GetLengthForSerialization();
			}
			byte[] array2 = new byte[num];
			int num2 = 0;
			Buffer.BlockCopy(array, 0, array2, num2, array.Length);
			num2 += array.Length;
			Buffer.BlockCopy(bytes, 0, array2, num2, bytes.Length);
			num2 += bytes.Length;
			Buffer.BlockCopy(queryStringHashBytes, 0, array2, num2, queryStringHashBytes.Length);
			num2 += queryStringHashBytes.Length;
			foreach (ColumnEncryptionKeyInfo columnEncryptionKeyInfo2 in keys)
			{
				num2 = columnEncryptionKeyInfo2.SerializeToBuffer(array2, num2);
			}
			return array2;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000F3758 File Offset: 0x000F2B58
		private byte[] EncryptBytePackage(byte[] bytePackage, byte[] sessionKey, string serverName)
		{
			if (sessionKey == null)
			{
				throw SQL.NullArgumentInternal("sessionKey", EnclaveDelegate.ClassName, "EncryptBytePackage");
			}
			if (sessionKey.Length == 0)
			{
				throw SQL.EmptyArgumentInternal("sessionKey", EnclaveDelegate.ClassName, "EncryptBytePackage");
			}
			byte[] result;
			try
			{
				SqlClientSymmetricKey encryptionKey = new SqlClientSymmetricKey(sessionKey);
				SqlClientEncryptionAlgorithm sqlClientEncryptionAlgorithm = EnclaveDelegate.SqlAeadAes256CbcHmac256Factory.Create(encryptionKey, SqlClientEncryptionType.Randomized, "AEAD_AES_256_CBC_HMAC_SHA256");
				result = sqlClientEncryptionAlgorithm.EncryptData(bytePackage);
			}
			catch (Exception innerExeption)
			{
				throw SQL.FailedToEncryptRegisterRulesBytePackage(innerExeption);
			}
			return result;
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000F37E4 File Offset: 0x000F2BE4
		private byte[] CombineByteArrays(byte[][] byteArraysToCombine)
		{
			byte[] array = new byte[byteArraysToCombine.Sum((byte[] ba) => ba.Length)];
			int num = 0;
			foreach (byte[] array2 in byteArraysToCombine)
			{
				Buffer.BlockCopy(array2, 0, array, num, array2.Length);
				num += array2.Length;
			}
			return array;
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000F3848 File Offset: 0x000F2C48
		private byte[] ComputeQueryStringHash(string queryString)
		{
			if (!string.IsNullOrWhiteSpace(queryString))
			{
				byte[] bytes = Encoding.Unicode.GetBytes(queryString);
				byte[] hash;
				using (SHA256Cng sha256Cng = new SHA256Cng())
				{
					sha256Cng.TransformFinalBlock(bytes, 0, bytes.Length);
					hash = sha256Cng.Hash;
				}
				return hash;
			}
			string argumentName = "queryString";
			if (queryString == null)
			{
				throw SQL.NullArgumentInternal(argumentName, EnclaveDelegate.ClassName, EnclaveDelegate.ComputeQueryStringHashName);
			}
			throw SQL.EmptyArgumentInternal(argumentName, EnclaveDelegate.ClassName, EnclaveDelegate.ComputeQueryStringHashName);
		}

		// Token: 0x04001553 RID: 5459
		private static readonly SqlAeadAes256CbcHmac256Factory SqlAeadAes256CbcHmac256Factory = new SqlAeadAes256CbcHmac256Factory();

		// Token: 0x04001554 RID: 5460
		private static readonly string GetAttestationInfoQueryString = string.Format("Select GetTrustedModuleIdentityAndAttestationInfo({0}) as attestationInfo", 0);

		// Token: 0x04001555 RID: 5461
		private static readonly EnclaveDelegate _EnclaveDelegate = new EnclaveDelegate();

		// Token: 0x04001556 RID: 5462
		private static readonly string ClassName = "EnclaveDelegate";

		// Token: 0x04001557 RID: 5463
		private static readonly string GetDecryptedKeysToBeSentToEnclaveName = "GetDecryptedKeysToBeSentToEnclave";

		// Token: 0x04001558 RID: 5464
		private static readonly string GetSerializedAttestationParametersName = "GetSerializedAttestationParameters";

		// Token: 0x04001559 RID: 5465
		private static readonly string ComputeQueryStringHashName = "ComputeQueryStringHash";

		// Token: 0x0400155A RID: 5466
		private readonly object _lock = new object();

		// Token: 0x020003FB RID: 1019
		internal class RetriableEnclaveQueryExecutionException : Exception
		{
			// Token: 0x060035BF RID: 13759 RVA: 0x00146E1C File Offset: 0x0014621C
			internal RetriableEnclaveQueryExecutionException(string message, Exception innerException) : base(message, innerException)
			{
			}
		}

		// Token: 0x020003FC RID: 1020
		internal class EnclavePackage
		{
			// Token: 0x17000863 RID: 2147
			// (get) Token: 0x060035C0 RID: 13760 RVA: 0x00146E34 File Offset: 0x00146234
			public SqlEnclaveSession EnclaveSession { get; }

			// Token: 0x17000864 RID: 2148
			// (get) Token: 0x060035C1 RID: 13761 RVA: 0x00146E48 File Offset: 0x00146248
			public byte[] EnclavePackageBytes { get; }

			// Token: 0x060035C2 RID: 13762 RVA: 0x00146E5C File Offset: 0x0014625C
			internal EnclavePackage(byte[] enclavePackageBytes, SqlEnclaveSession enclaveSession)
			{
				this.EnclavePackageBytes = enclavePackageBytes;
				this.EnclaveSession = enclaveSession;
			}
		}
	}
}
