using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020002BB RID: 699
	internal sealed class SqlCachedBuffer : INullable
	{
		// Token: 0x0600234E RID: 9038 RVA: 0x00290778 File Offset: 0x0028FB78
		internal SqlCachedBuffer(SqlMetaDataPriv metadata, TdsParser parser, TdsParserStateObject stateObj)
		{
			this._cachedBytes = new ArrayList();
			ulong num = parser.PlpBytesLeft(stateObj);
			while (num != 0UL)
			{
				do
				{
					int num2 = (num > 2048UL) ? 2048 : ((int)num);
					byte[] array = new byte[num2];
					num2 = stateObj.ReadPlpBytes(ref array, 0, num2);
					if (this._cachedBytes.Count == 0)
					{
						this.AddByteOrderMark(array);
					}
					this._cachedBytes.Add(array);
					num -= (ulong)((long)num2);
				}
				while (num > 0UL);
				num = parser.PlpBytesLeft(stateObj);
				if (num <= 0UL)
				{
					return;
				}
			}
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x00290808 File Offset: 0x0028FC08
		internal SqlCachedBuffer(SqlDataReader dataRdr, int columnOrdinal, long startPosition)
		{
			this._cachedBytes = new ArrayList();
			long num = startPosition;
			int num2;
			do
			{
				byte[] array = new byte[2048];
				num2 = (int)dataRdr.GetBytesInternal(columnOrdinal, num, array, 0, 2048);
				num += (long)num2;
				if (this._cachedBytes.Count == 0)
				{
					this.AddByteOrderMark(array, num2);
				}
				if (0 < num2)
				{
					if (num2 < array.Length)
					{
						byte[] array2 = new byte[num2];
						Buffer.BlockCopy(array, 0, array2, 0, num2);
						array = array2;
					}
					this._cachedBytes.Add(array);
				}
			}
			while (0 < num2);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x00290898 File Offset: 0x0028FC98
		private void AddByteOrderMark(byte[] byteArr)
		{
			this.AddByteOrderMark(byteArr, byteArr.Length);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x002908B8 File Offset: 0x0028FCB8
		private void AddByteOrderMark(byte[] byteArr, int length)
		{
			int num = 65279;
			if (length >= 2 && byteArr[0] == 223 && byteArr[1] == 255)
			{
				num = 0;
			}
			if (num != 0)
			{
				byte[] array = new byte[2];
				array[0] = (byte)num;
				num >>= 8;
				array[1] = (byte)num;
				this._cachedBytes.Add(array);
			}
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x00290918 File Offset: 0x0028FD18
		private SqlCachedBuffer()
		{
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x00290938 File Offset: 0x0028FD38
		internal ArrayList CachedBytes
		{
			get
			{
				return this._cachedBytes;
			}
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x00290958 File Offset: 0x0028FD58
		public override string ToString()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (this._cachedBytes.Count == 0)
			{
				return string.Empty;
			}
			SqlCachedStream value = new SqlCachedStream(this);
			SqlXml sqlXml = new SqlXml(value);
			return sqlXml.Value;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x002909A8 File Offset: 0x0028FDA8
		internal SqlString ToSqlString()
		{
			if (this.IsNull)
			{
				return SqlString.Null;
			}
			string data = this.ToString();
			return new SqlString(data);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x002909D8 File Offset: 0x0028FDD8
		internal SqlXml ToSqlXml()
		{
			SqlCachedStream value = new SqlCachedStream(this);
			return new SqlXml(value);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x002909F8 File Offset: 0x0028FDF8
		internal XmlReader ToXmlReader()
		{
			SqlCachedStream sqlCachedStream = new SqlCachedStream(this);
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
			MethodInfo method = typeof(XmlReader).GetMethod("CreateSqlReader", BindingFlags.Static | BindingFlags.NonPublic);
			object[] array = new object[3];
			array[0] = sqlCachedStream;
			array[1] = xmlReaderSettings;
			object[] parameters = array;
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			XmlReader result;
			try
			{
				result = (XmlReader)method.Invoke(null, parameters);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x00290A88 File Offset: 0x0028FE88
		public bool IsNull
		{
			get
			{
				return this._cachedBytes == null;
			}
		}

		// Token: 0x0400170A RID: 5898
		private const int _maxChunkSize = 2048;

		// Token: 0x0400170B RID: 5899
		private ArrayList _cachedBytes;

		// Token: 0x0400170C RID: 5900
		public static readonly SqlCachedBuffer Null = new SqlCachedBuffer();
	}
}
