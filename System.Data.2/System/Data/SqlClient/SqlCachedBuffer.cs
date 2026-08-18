using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001AD RID: 429
	internal sealed class SqlCachedBuffer : INullable
	{
		// Token: 0x0600191C RID: 6428 RVA: 0x000B1AD4 File Offset: 0x000B0ED4
		private SqlCachedBuffer()
		{
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000B1AE8 File Offset: 0x000B0EE8
		private SqlCachedBuffer(List<byte[]> cachedBytes)
		{
			this._cachedBytes = cachedBytes;
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x000B1B04 File Offset: 0x000B0F04
		internal List<byte[]> CachedBytes
		{
			get
			{
				return this._cachedBytes;
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000B1B18 File Offset: 0x000B0F18
		internal static bool TryCreate(SqlMetaDataPriv metadata, TdsParser parser, TdsParserStateObject stateObj, out SqlCachedBuffer buffer)
		{
			int num = 0;
			List<byte[]> list = new List<byte[]>();
			buffer = null;
			ulong num2;
			if (!parser.TryPlpBytesLeft(stateObj, out num2))
			{
				return false;
			}
			while (num2 != 0UL)
			{
				do
				{
					num = ((num2 > 2048UL) ? 2048 : ((int)num2));
					byte[] array = new byte[num];
					if (!stateObj.TryReadPlpBytes(ref array, 0, num, out num))
					{
						return false;
					}
					if (list.Count == 0)
					{
						SqlCachedBuffer.AddByteOrderMark(array, list);
					}
					list.Add(array);
					num2 -= (ulong)((long)num);
				}
				while (num2 > 0UL);
				if (!parser.TryPlpBytesLeft(stateObj, out num2))
				{
					return false;
				}
				if (num2 <= 0UL)
				{
					break;
				}
			}
			buffer = new SqlCachedBuffer(list);
			return true;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000B1BA8 File Offset: 0x000B0FA8
		private static void AddByteOrderMark(byte[] byteArr, List<byte[]> cachedBytes)
		{
			if (byteArr.Length < 2 || byteArr[0] != 223 || byteArr[1] != 255)
			{
				cachedBytes.Add(TdsEnums.XMLUNICODEBOMBYTES);
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x000B1BDC File Offset: 0x000B0FDC
		internal Stream ToStream()
		{
			return new SqlCachedStream(this);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000B1BF0 File Offset: 0x000B0FF0
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
			SqlXml sqlXml = new SqlXml(this.ToStream());
			return sqlXml.Value;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000B1C30 File Offset: 0x000B1030
		internal SqlString ToSqlString()
		{
			if (this.IsNull)
			{
				return SqlString.Null;
			}
			string data = this.ToString();
			return new SqlString(data);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000B1C58 File Offset: 0x000B1058
		internal SqlXml ToSqlXml()
		{
			return new SqlXml(this.ToStream());
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000B1C74 File Offset: 0x000B1074
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal XmlReader ToXmlReader()
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
			MethodInfo method = typeof(XmlReader).GetMethod("CreateSqlReader", BindingFlags.Static | BindingFlags.NonPublic);
			object[] array = new object[3];
			array[0] = this.ToStream();
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

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x000B1CF8 File Offset: 0x000B10F8
		public bool IsNull
		{
			get
			{
				return this._cachedBytes == null;
			}
		}

		// Token: 0x04000EF9 RID: 3833
		public static readonly SqlCachedBuffer Null = new SqlCachedBuffer();

		// Token: 0x04000EFA RID: 3834
		private const int _maxChunkSize = 2048;

		// Token: 0x04000EFB RID: 3835
		private List<byte[]> _cachedBytes;
	}
}
