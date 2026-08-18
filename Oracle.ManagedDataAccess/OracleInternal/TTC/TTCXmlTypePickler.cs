using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.I18N;
using OracleInternal.ServiceObjects;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.TTC
{
	// Token: 0x0200023C RID: 572
	internal class TTCXmlTypePickler
	{
		// Token: 0x060014CE RID: 5326 RVA: 0x000DF800 File Offset: 0x000DDA00
		internal static long ReadIntInNativeFormat(byte[] buf)
		{
			long num = 0L;
			num += (long)((long)buf[0] << 24);
			num += (long)((long)buf[1] << 16);
			num += (long)((long)buf[2] << 8);
			return num + (long)(buf[3] & byte.MaxValue);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x000DF83C File Offset: 0x000DDA3C
		internal static int ReadShortInNativeFormat(byte[] buf)
		{
			int num = 0;
			num += (int)buf[0] << 8;
			return num + (int)(buf[1] & byte.MaxValue);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x000DF860 File Offset: 0x000DDA60
		internal static int WriteInt2Bytes(byte[] bytes, int index, int val)
		{
			bytes[index++] = (byte)(val >> 24);
			val &= 16777215;
			bytes[index++] = (byte)(val >> 16);
			val &= 65535;
			bytes[index++] = (byte)(val >> 8);
			val &= 255;
			bytes[index++] = (byte)val;
			return 4;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x000DF8BC File Offset: 0x000DDABC
		internal static int WriteShort2Bytes(byte[] bytes, int index, int val)
		{
			bytes[index++] = (byte)(val >> 8);
			val &= 255;
			bytes[index++] = (byte)val;
			return 2;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000DF8E0 File Offset: 0x000DDAE0
		private static int WriteLength(byte[] bytes, int index, long dataLength)
		{
			if (dataLength <= 245L)
			{
				bytes[index] = (byte)dataLength;
				return 1;
			}
			bytes[index++] = 254;
			bytes[index++] = (byte)(dataLength >> 24);
			dataLength &= 16777215L;
			bytes[index++] = (byte)(dataLength >> 16);
			dataLength &= 65535L;
			bytes[index++] = (byte)(dataLength >> 8);
			dataLength &= 255L;
			bytes[index++] = (byte)dataLength;
			return 5;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x000DF95C File Offset: 0x000DDB5C
		private static int WriteBuffer(byte[] destBytes, byte[] srcBytes, int destIndex, int srcIndex, long dataLength)
		{
			int num = (int)dataLength;
			if (srcBytes != null && (long)srcBytes.Length < dataLength)
			{
				num = srcBytes.Length;
			}
			for (int i = 0; i < num; i++)
			{
				destBytes[destIndex++] = srcBytes[srcIndex++];
			}
			return num;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x000DF99C File Offset: 0x000DDB9C
		internal static void GetXmlHeaderLength(OracleXmlTypeImpl xmlTypeImpl, out int length)
		{
			length = 5;
			if ((xmlTypeImpl.m_xmlFlag & 4096U) == 4096U && xmlTypeImpl.m_snapshot != null)
			{
				length += xmlTypeImpl.m_kpsnpLen;
			}
			if ((xmlTypeImpl.m_xmlFlag & 8U) == 8U && xmlTypeImpl.m_schemaID != null && xmlTypeImpl.m_schElem != null)
			{
				length += 20;
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000DF9F4 File Offset: 0x000DDBF4
		internal static int ReadNamedTypeHeader(List<ArraySegment<byte>> dataSegments, byte[] buf, ref int pos, OraXmlTypeHeader xmlTypeHeader)
		{
			int num = 0;
			xmlTypeHeader.m_dataLength = 0L;
			pos++;
			num++;
			pos++;
			num++;
			Accessor.CopyDataToUserBuffer(dataSegments, pos, buf, 0, 1);
			xmlTypeHeader.m_dataLength = (long)(buf[0] & byte.MaxValue);
			pos++;
			num++;
			if (xmlTypeHeader.m_dataLength > 245L)
			{
				Accessor.CopyDataToUserBuffer(dataSegments, pos, buf, 0, 4);
				xmlTypeHeader.m_dataLength = (long)((((int)(buf[0] & byte.MaxValue) * 256 + (int)(buf[1] & byte.MaxValue)) * 256 + (int)(buf[2] & byte.MaxValue)) * 256 + (int)(buf[3] & byte.MaxValue));
				pos += 4;
				num += 4;
			}
			return num;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x000DFAAC File Offset: 0x000DDCAC
		internal static bool ReadXmlHeader(OracleConnectionImpl connImpl, List<ArraySegment<byte>> dataSegments, OraXmlTypeHeader xmlTypeHeader)
		{
			byte[] array = new byte[64];
			int num = 0;
			int num2 = TTCXmlTypePickler.ReadNamedTypeHeader(dataSegments, array, ref num, xmlTypeHeader);
			num++;
			num2++;
			Accessor.CopyDataToUserBuffer(dataSegments, num, array, 0, 4);
			uint num3 = (uint)TTCXmlTypePickler.ReadIntInNativeFormat(array);
			num += 4;
			num2 += 4;
			if ((num3 & 1048576U) == 1048576U)
			{
				num += 4;
				num2 += 4;
			}
			if ((num3 & 4096U) == 4096U)
			{
				int kpsnpLen = OracleXmlTypeImpl.GetKpsnpLen(connImpl);
				xmlTypeHeader.m_snapshot = new byte[kpsnpLen];
				Accessor.CopyDataToUserBuffer(dataSegments, num, xmlTypeHeader.m_snapshot, 0, kpsnpLen);
				num += kpsnpLen;
				num2 += kpsnpLen;
				xmlTypeHeader.m_xmlFlag |= 4096U;
			}
			if ((num3 & 8U) == 8U)
			{
				xmlTypeHeader.m_schoid = new byte[16];
				Accessor.CopyDataToUserBuffer(dataSegments, num, xmlTypeHeader.m_schoid, 0, 16);
				num += 16;
				num2 += 16;
				xmlTypeHeader.m_schElem = new byte[4];
				Accessor.CopyDataToUserBuffer(dataSegments, num, xmlTypeHeader.m_schElem, 0, 4);
				num += 4;
				num2 += 4;
				xmlTypeHeader.m_xmlFlag |= 8U;
			}
			if ((num3 & 536870912U) == 536870912U)
			{
				num += 16;
				num2 += 16;
				xmlTypeHeader.m_xmlFlag |= 536870912U;
			}
			if ((num3 & 524288U) != 524288U)
			{
				if ((num3 & 1U) == 1U)
				{
					if ((num3 & 2147483648U) == 2147483648U)
					{
						xmlTypeHeader.m_typeOfXmlType = (TypeOfXmlType)2147483649U;
					}
					else if ((num3 & 16777216U) == 16777216U)
					{
						xmlTypeHeader.m_typeOfXmlType = TypeOfXmlType.BlobCSX;
					}
					else
					{
						xmlTypeHeader.m_typeOfXmlType = TypeOfXmlType.Clob;
					}
				}
				else if ((num3 & 4U) == 4U)
				{
					xmlTypeHeader.m_typeOfXmlType = TypeOfXmlType.String;
				}
				else if ((num3 & 2U) == 2U)
				{
					xmlTypeHeader.m_typeOfXmlType = TypeOfXmlType.Object;
				}
			}
			xmlTypeHeader.m_headerLength = num2;
			if (xmlTypeHeader.m_dataLength > 0L)
			{
				xmlTypeHeader.m_dataLength -= (long)xmlTypeHeader.m_headerLength;
			}
			return true;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x000DFC78 File Offset: 0x000DDE78
		internal static long GetLengthAndFlag(Conv dbCharSetConv, OracleXmlTypeImpl xmlTypeImpl, TypeOfXmlData typeOfXmlData, object xmlData, ref uint xmlFlag)
		{
			int num = 0;
			long num2 = 0L;
			long num3 = 0L;
			xmlFlag = (xmlTypeImpl.m_xmlFlag | (uint)xmlTypeImpl.m_typeOfXmlType);
			TTCXmlTypePickler.GetXmlHeaderLength(xmlTypeImpl, out num);
			if (typeOfXmlData <= TypeOfXmlData.Chars)
			{
				switch (typeOfXmlData)
				{
				case TypeOfXmlData.String:
				{
					string text = (string)xmlData;
					num3 = (long)dbCharSetConv.GetBytesLength(text, 0, text.Length);
					break;
				}
				case (TypeOfXmlData)3:
					break;
				case TypeOfXmlData.Clob:
				{
					OracleClob oracleClob = (OracleClob)xmlData;
					num3 = (long)oracleClob.m_clobImpl.m_lobLocator.Length;
					break;
				}
				default:
					if (typeOfXmlData == TypeOfXmlData.Chars)
					{
						char[] array = (char[])xmlData;
						num3 = (long)dbCharSetConv.GetBytesLength(array, 0, array.Length);
					}
					break;
				}
			}
			else if (typeOfXmlData != TypeOfXmlData.BlobWithText)
			{
				if (typeOfXmlData == TypeOfXmlData.BlobCSX)
				{
					OracleBlob oracleBlob = (OracleBlob)xmlData;
					num3 = (long)oracleBlob.m_blobImpl.m_lobLocator.Length;
				}
			}
			else
			{
				OracleBlob xmlBlob = ((OraXmlBlobWithText)xmlData).m_xmlBlob;
				num3 = (long)(xmlBlob.m_blobImpl.m_lobLocator.Length + 2);
			}
			num2 += 2L;
			num2 += (long)num;
			num2 += num3;
			if (num2 <= 244L)
			{
				num2 += 1L;
			}
			else
			{
				num2 += 5L;
			}
			return num2;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x000DFD8C File Offset: 0x000DDF8C
		internal static long GetLengthAndFlag(Conv dbCharSetConv, TypeOfXmlType typeOfXmlType, TypeOfXmlData typeOfXmlData, int offset, int size, object xmlData, ref uint xmlFlag)
		{
			int num = 5;
			long num2 = 0L;
			long num3 = 0L;
			xmlFlag = (uint)typeOfXmlType;
			if (typeOfXmlData <= TypeOfXmlData.Chars)
			{
				switch (typeOfXmlData)
				{
				case TypeOfXmlData.String:
				{
					string str = (string)xmlData;
					num3 = (long)dbCharSetConv.GetBytesLength(str, offset, size);
					break;
				}
				case (TypeOfXmlData)3:
					break;
				case TypeOfXmlData.Clob:
				{
					OracleClob oracleClob = (OracleClob)xmlData;
					num3 = (long)oracleClob.m_clobImpl.m_lobLocator.Length;
					break;
				}
				default:
					if (typeOfXmlData == TypeOfXmlData.Chars)
					{
						char[] chars = (char[])xmlData;
						num3 = (long)dbCharSetConv.GetBytesLength(chars, offset, size);
					}
					break;
				}
			}
			else if (typeOfXmlData != TypeOfXmlData.BlobWithText)
			{
				if (typeOfXmlData == TypeOfXmlData.BlobCSX)
				{
					OracleBlob oracleBlob = (OracleBlob)xmlData;
					num3 = (long)oracleBlob.m_blobImpl.m_lobLocator.Length;
				}
			}
			else
			{
				OracleBlob xmlBlob = ((OraXmlBlobWithText)xmlData).m_xmlBlob;
				num3 = (long)(xmlBlob.m_blobImpl.m_lobLocator.Length + 2);
			}
			num2 += 2L;
			num2 += (long)num;
			num2 += num3;
			if (num2 <= 244L)
			{
				num2 += 1L;
			}
			else
			{
				num2 += 5L;
			}
			return num2;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x000DFE8C File Offset: 0x000DE08C
		internal static byte[] Pickle(Conv dbCharSetConv, OracleXmlTypeImpl xmlTypeImpl)
		{
			uint num = 0U;
			int num2 = 0;
			TypeOfXmlData typeOfXmlData = TypeOfXmlData.NoData;
			object obj = null;
			xmlTypeImpl.GetXmlDataForPickling(out typeOfXmlData, out obj);
			long num3 = TTCXmlTypePickler.GetLengthAndFlag(dbCharSetConv, xmlTypeImpl, typeOfXmlData, obj, ref num);
			byte[] array = new byte[num3];
			array[num2++] = 133;
			array[num2++] = 1;
			num2 += TTCXmlTypePickler.WriteLength(array, num2, num3);
			array[num2++] = 1;
			num2 += TTCXmlTypePickler.WriteInt2Bytes(array, num2, (int)num);
			if ((num & 4096U) == 4096U && xmlTypeImpl.m_snapshot != null)
			{
				TTCXmlTypePickler.WriteBuffer(array, xmlTypeImpl.m_snapshot, num2, 0, (long)xmlTypeImpl.m_kpsnpLen);
				num2 += xmlTypeImpl.m_kpsnpLen;
			}
			if ((num & 8U) == 8U && xmlTypeImpl.m_schemaID != null && xmlTypeImpl.m_schElem != null)
			{
				TTCXmlTypePickler.WriteBuffer(array, xmlTypeImpl.m_schemaID, num2, 0, 16L);
				num2 += 16;
				TTCXmlTypePickler.WriteBuffer(array, xmlTypeImpl.m_schElem, num2, 0, 4L);
				num2 += 4;
			}
			TypeOfXmlData typeOfXmlData2 = typeOfXmlData;
			if (typeOfXmlData2 <= TypeOfXmlData.Chars)
			{
				switch (typeOfXmlData2)
				{
				case TypeOfXmlData.String:
				{
					int num4 = (int)num3;
					string text = (string)obj;
					dbCharSetConv.ConvertStringToBytes(text, 0, text.Length, array, num2, ref num4, true);
					break;
				}
				case (TypeOfXmlData)3:
					break;
				case TypeOfXmlData.Clob:
				{
					OracleClob oracleClob = (OracleClob)obj;
					num3 = (long)oracleClob.m_clobImpl.m_lobLocator.Length;
					TTCXmlTypePickler.WriteBuffer(array, oracleClob.m_clobImpl.m_lobLocator, num2, 0, num3);
					break;
				}
				default:
					if (typeOfXmlData2 == TypeOfXmlData.Chars)
					{
						int num5 = (int)num3;
						char[] array2 = (char[])obj;
						dbCharSetConv.ConvertCharsToBytes(array2, 0, array2.Length, array, num2, ref num5, true);
					}
					break;
				}
			}
			else if (typeOfXmlData2 != TypeOfXmlData.BlobWithText)
			{
				if (typeOfXmlData2 == TypeOfXmlData.BlobCSX)
				{
					OracleBlob oracleBlob = (OracleBlob)obj;
					num3 = (long)oracleBlob.m_blobImpl.m_lobLocator.Length;
					TTCXmlTypePickler.WriteBuffer(array, oracleBlob.m_blobImpl.m_lobLocator, num2, 0, num3);
				}
			}
			else
			{
				OracleBlob xmlBlob = ((OraXmlBlobWithText)obj).m_xmlBlob;
				TTCXmlTypePickler.WriteBuffer(array, xmlBlob.m_blobImpl.m_lobLocator, num2, 0, (long)xmlBlob.m_blobImpl.m_lobLocator.Length);
				num2 += xmlBlob.m_blobImpl.m_lobLocator.Length;
				TTCXmlTypePickler.WriteShort2Bytes(array, num2, ((OraXmlBlobWithText)obj).m_csid);
			}
			return array;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x000E00C4 File Offset: 0x000DE2C4
		internal static byte[] Pickle(Conv dbCharSetConv, TypeOfXmlType typeOfXmlType, TypeOfXmlData typeOfXmlData, int offset, int size, object xmlData)
		{
			uint val = 0U;
			int num = 0;
			long num2 = TTCXmlTypePickler.GetLengthAndFlag(dbCharSetConv, typeOfXmlType, typeOfXmlData, offset, size, xmlData, ref val);
			byte[] array = new byte[num2];
			array[num++] = 133;
			array[num++] = 1;
			num += TTCXmlTypePickler.WriteLength(array, num, num2);
			array[num++] = 1;
			num += TTCXmlTypePickler.WriteInt2Bytes(array, num, (int)val);
			if (typeOfXmlData <= TypeOfXmlData.Chars)
			{
				switch (typeOfXmlData)
				{
				case TypeOfXmlData.String:
				{
					int num3 = (int)num2;
					string str = (string)xmlData;
					dbCharSetConv.ConvertStringToBytes(str, offset, size, array, num, ref num3, true);
					break;
				}
				case (TypeOfXmlData)3:
					break;
				case TypeOfXmlData.Clob:
				{
					OracleClob oracleClob = (OracleClob)xmlData;
					int num4 = oracleClob.m_clobImpl.m_lobLocator.Length;
					TTCXmlTypePickler.WriteBuffer(array, oracleClob.m_clobImpl.m_lobLocator, num, 0, (long)num4);
					break;
				}
				default:
					if (typeOfXmlData == TypeOfXmlData.Chars)
					{
						int num5 = (int)num2;
						char[] chars = (char[])xmlData;
						dbCharSetConv.ConvertCharsToBytes(chars, offset, size, array, num, ref num5, true);
					}
					break;
				}
			}
			else if (typeOfXmlData != TypeOfXmlData.BlobWithText)
			{
				if (typeOfXmlData == TypeOfXmlData.BlobCSX)
				{
					OracleBlob oracleBlob = (OracleBlob)xmlData;
					num2 = (long)oracleBlob.m_blobImpl.m_lobLocator.Length;
					TTCXmlTypePickler.WriteBuffer(array, oracleBlob.m_blobImpl.m_lobLocator, num, 0, num2);
				}
			}
			else
			{
				OracleBlob xmlBlob = ((OraXmlBlobWithText)xmlData).m_xmlBlob;
				TTCXmlTypePickler.WriteBuffer(array, xmlBlob.m_blobImpl.m_lobLocator, num, 0, (long)xmlBlob.m_blobImpl.m_lobLocator.Length);
				num += xmlBlob.m_blobImpl.m_lobLocator.Length;
				TTCXmlTypePickler.WriteShort2Bytes(array, num, ((OraXmlBlobWithText)xmlData).m_csid);
			}
			return array;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x000E0274 File Offset: 0x000DE474
		// Note: this type is marked as 'beforefieldinit'.
		static TTCXmlTypePickler()
		{
			byte[] array = new byte[16];
			array[13] = 2;
			array[14] = 1;
			TTCXmlTypePickler.TOID = array;
		}

		// Token: 0x04001938 RID: 6456
		internal const string TypeName = "SYS.XMLTYPE";

		// Token: 0x04001939 RID: 6457
		internal const int XMLTYPE_VERSION = 1;

		// Token: 0x0400193A RID: 6458
		internal const int XMLTYPE_LOB = 1;

		// Token: 0x0400193B RID: 6459
		internal const int XMLTYPE_OBJECT = 2;

		// Token: 0x0400193C RID: 6460
		internal const int XMLTYPE_STR = 4;

		// Token: 0x0400193D RID: 6461
		internal const int XMLTYPE_PTR = 8;

		// Token: 0x0400193E RID: 6462
		internal const int XMLTYPE_CSX = 16777216;

		// Token: 0x0400193F RID: 6463
		internal const int XMLTYPE_LOB_CSX = 16777217;

		// Token: 0x04001940 RID: 6464
		internal const int XMLTYPE_XQ_SEQ_DMFMT = 524288;

		// Token: 0x04001941 RID: 6465
		internal const uint XMLTYPE_FLAG_SKIP_NEXT_4 = 1048576U;

		// Token: 0x04001942 RID: 6466
		internal const uint XMLTYPE_FLAG_SNAPSHOT = 4096U;

		// Token: 0x04001943 RID: 6467
		internal const uint XMLTYPE_FLAG_SCHMOID = 8U;

		// Token: 0x04001944 RID: 6468
		internal const uint XMLTYPE_FLAG_NO_DOC_WRAP = 4194304U;

		// Token: 0x04001945 RID: 6469
		internal const uint XMLTYPE_FLAG_FRAGMENT = 32U;

		// Token: 0x04001946 RID: 6470
		internal const uint XMLTYPE_FLAG_NOXMLHDR = 1024U;

		// Token: 0x04001947 RID: 6471
		internal const uint XMLTYPE_FLAG_GUID = 536870912U;

		// Token: 0x04001948 RID: 6472
		internal const uint XMLTYPE_FLAG_CSID = 2147483648U;

		// Token: 0x04001949 RID: 6473
		internal const int XMLTYPE_SCHMOIDLEN = 16;

		// Token: 0x0400194A RID: 6474
		internal const int XMLTYPE_ELEMNUMLEN = 4;

		// Token: 0x0400194B RID: 6475
		internal const int XMLTYPE_KOSNPLEN = 34;

		// Token: 0x0400194C RID: 6476
		internal const int XMLTYPE_KOSNPLEN_OLD = 24;

		// Token: 0x0400194D RID: 6477
		internal const int XMLTYPE_GUIDLEN = 16;

		// Token: 0x0400194E RID: 6478
		internal const int XMLTYPE_CSID_SIZE = 2;

		// Token: 0x0400194F RID: 6479
		internal const short KOPI20_IF_IS81 = 128;

		// Token: 0x04001950 RID: 6480
		internal const short KOPI20_IF_CMSB = 64;

		// Token: 0x04001951 RID: 6481
		internal const short KOPI20_IF_CLSB = 32;

		// Token: 0x04001952 RID: 6482
		internal const short KOPI20_IF_DEGN = 16;

		// Token: 0x04001953 RID: 6483
		internal const short KOPI20_IF_COLL = 8;

		// Token: 0x04001954 RID: 6484
		internal const short KOPI20_IF_NOPS = 4;

		// Token: 0x04001955 RID: 6485
		internal const short KOPI20_IF_ANY = 2;

		// Token: 0x04001956 RID: 6486
		internal const short KOPI20_IF_NONL = 1;

		// Token: 0x04001957 RID: 6487
		internal const short KOPI20_VERSION = 1;

		// Token: 0x04001958 RID: 6488
		internal const short KOPI20_LN_MAXV = 245;

		// Token: 0x04001959 RID: 6489
		internal const short KOPI20_LN_5BLN = 254;

		// Token: 0x0400195A RID: 6490
		internal static byte[] TOID;
	}
}
