using System;
using System.Text;
using Oracle.ManagedDataAccess.Types;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001BB RID: 443
	internal class OracleXmlStreamImpl
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x000BD8EC File Offset: 0x000BBAEC
		private OracleXmlStreamImpl()
		{
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000BD918 File Offset: 0x000BBB18
		internal OracleXmlStreamImpl(OracleConnectionImpl connImpl, OracleXmlTypeImpl xmlTypeImplObj)
		{
			this.m_connImpl = connImpl;
			this.m_xmlTypeImplObj = xmlTypeImplObj;
			this.m_xmlTypeData = xmlTypeImplObj.m_xmlTypeData;
			if (!this.m_bInitialized)
			{
				this.Initialize();
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000BD974 File Offset: 0x000BBB74
		private void Initialize()
		{
			this.m_bInitialized = true;
			TypeOfXmlData typeOfXmlData = this.m_xmlTypeData.m_typeOfXmlData;
			if (typeOfXmlData <= TypeOfXmlData.Chars)
			{
				switch (typeOfXmlData)
				{
				case TypeOfXmlData.String:
				case TypeOfXmlData.ClobAndString:
					break;
				case (TypeOfXmlData)3:
				case (TypeOfXmlData)5:
					return;
				case TypeOfXmlData.Clob:
					this.m_xmlClob = (OracleClob)this.m_xmlTypeData.m_xmlClob.Clone();
					this.m_xmlStreamType = TypeOfXmlData.Clob;
					return;
				default:
					if (typeOfXmlData != TypeOfXmlData.Chars)
					{
						return;
					}
					this.m_xmlChars = this.m_xmlTypeData.m_xmlChars;
					this.m_xmlStreamType = TypeOfXmlData.Chars;
					return;
				}
			}
			else
			{
				switch (typeOfXmlData)
				{
				case TypeOfXmlData.XmlDoc:
				case TypeOfXmlData.StringAndXmlDoc:
					break;
				case (TypeOfXmlData)33:
					return;
				default:
					switch (typeOfXmlData)
					{
					case TypeOfXmlData.BlobCSX:
					case TypeOfXmlData.BlobCSXAndString:
						this.m_xmlBlobText = this.m_xmlTypeImplObj.GetBinXmlDecodedStringBuilder(this.m_xmlTypeData.m_xmlBlobCSX);
						this.m_xmlStreamType = TypeOfXmlData.BlobWithText;
						return;
					case (TypeOfXmlData)129:
						return;
					default:
						return;
					}
					break;
				}
			}
			this.m_xmlStr = this.m_xmlTypeData.m_xmlStr;
			this.m_xmlStreamType = TypeOfXmlData.String;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000BDA64 File Offset: 0x000BBC64
		internal long GetLength()
		{
			if (this.m_streamDataLength < 0L)
			{
				if (this.m_xmlStreamType == TypeOfXmlData.BlobWithText)
				{
					if (this.m_xmlBlobText != null)
					{
						this.m_streamDataLength = (long)this.m_xmlBlobText.Length;
					}
					else
					{
						this.m_streamDataLength = 0L;
					}
				}
				else if (this.m_xmlStreamType == TypeOfXmlData.Clob)
				{
					this.m_streamDataLength = this.m_xmlClob.Length;
				}
				else if (this.m_xmlStreamType == TypeOfXmlData.Chars)
				{
					this.m_streamDataLength = (long)(this.m_xmlChars.Length * 2);
				}
				else
				{
					this.m_streamDataLength = (long)(this.m_xmlStr.Length * 2);
				}
			}
			return this.m_streamDataLength;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000BDB00 File Offset: 0x000BBD00
		internal string GetValue()
		{
			string result;
			if (this.m_xmlStreamType == TypeOfXmlData.BlobWithText)
			{
				if (this.m_xmlBlobText != null && this.m_streamDataLength < 1073741823L)
				{
					result = this.m_xmlBlobText.ToString();
				}
				else
				{
					result = this.m_xmlBlobText.ToString(0, 1073741822);
				}
			}
			else if (this.m_xmlStreamType == TypeOfXmlData.Clob)
			{
				result = this.m_xmlClob.Value;
			}
			else if (this.m_xmlStreamType == TypeOfXmlData.Chars)
			{
				result = new string(this.m_xmlChars);
			}
			else
			{
				result = this.m_xmlStr;
			}
			return result;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000BDB88 File Offset: 0x000BBD88
		internal int Read(char[] buffer, int offset, int count, ref long position)
		{
			long num;
			if (this.m_xmlStreamType == TypeOfXmlData.Clob)
			{
				this.m_xmlClob.Position = position;
				num = (long)this.m_xmlClob.Read(buffer, offset, count);
				position = this.m_xmlClob.Position;
			}
			else
			{
				long num2;
				if (position <= 0L)
				{
					num2 = 0L;
				}
				else
				{
					num2 = position / 2L;
				}
				if (count + offset <= buffer.Length)
				{
					num = (long)count;
				}
				else
				{
					num = (long)(buffer.Length - offset);
				}
				long length = this.GetLength();
				long num3 = num * 2L;
				long num4 = position + num3;
				if (num4 > length)
				{
					num = (length - position) / 2L;
				}
				if (num == 0L)
				{
					return 0;
				}
				if (this.m_xmlStreamType == TypeOfXmlData.Chars)
				{
					Array.Copy(this.m_xmlChars, num2, buffer, (long)offset, num);
				}
				else if (this.m_xmlStreamType == TypeOfXmlData.BlobWithText)
				{
					string text = this.m_xmlBlobText.ToString((int)num2, (int)num);
					char[] sourceArray = text.ToCharArray();
					Array.Copy(sourceArray, 0L, buffer, (long)offset, num);
				}
				else
				{
					string text2 = this.m_xmlStr.Substring((int)num2, (int)num);
					char[] sourceArray2 = text2.ToCharArray();
					Array.Copy(sourceArray2, 0L, buffer, (long)offset, num);
				}
				position += num * 2L;
			}
			return (int)num;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000BDCA8 File Offset: 0x000BBEA8
		internal int Read(byte[] buffer, int offset, int count, ref long position)
		{
			int result;
			if (this.m_xmlStreamType == TypeOfXmlData.Clob)
			{
				this.m_xmlClob.Position = position;
				result = this.m_xmlClob.Read(buffer, offset, count);
				position = this.m_xmlClob.Position;
			}
			else if (this.m_xmlStreamType == TypeOfXmlData.BlobWithText)
			{
				result = this.ReadFromStringBuilder(buffer, offset, count, ref position);
			}
			else
			{
				result = this.ReadFromStringValue(buffer, offset, count, ref position);
			}
			return result;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000BDD14 File Offset: 0x000BBF14
		private int ReadFromStringBuilder(byte[] buffer, int offset, int count, ref long position)
		{
			new UnicodeEncoding();
			if (position + (long)count > (long)(this.m_xmlBlobText.Length * 2))
			{
				count = this.m_xmlBlobText.Length * 2 - (int)position;
			}
			if (offset + count > buffer.Length)
			{
				count = buffer.Length - offset;
			}
			if (count <= 0 || offset < 0 || position < 0L)
			{
				throw new ArgumentOutOfRangeException("offset or count");
			}
			bool flag = count % 2 > 0;
			bool flag2 = position % 2L > 0L;
			int srcOffset;
			int startIndex;
			int length;
			if (flag2)
			{
				srcOffset = 1;
				startIndex = ((int)position - 1) / 2;
				if (flag)
				{
					length = (count + 1) / 2;
				}
				else
				{
					length = count / 2;
				}
			}
			else
			{
				srcOffset = 0;
				startIndex = (int)position / 2;
				if (flag)
				{
					length = (count + 1) / 2;
				}
				else
				{
					length = count / 2;
				}
			}
			string text = this.m_xmlBlobText.ToString(startIndex, length);
			char[] src = text.ToCharArray();
			Buffer.BlockCopy(src, srcOffset, buffer, offset, count);
			position += (long)count;
			return count;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000BDE04 File Offset: 0x000BC004
		private int ReadFromStringValue(byte[] buffer, int offset, int count, ref long position)
		{
			int num = count;
			int num2 = offset;
			long num3;
			if (position < 0L)
			{
				num3 = 0L;
			}
			else
			{
				num3 = position / 2L;
			}
			bool flag = false;
			if (position > 0L && position % 2L != 0L)
			{
				flag = true;
			}
			if (flag)
			{
				if (this.m_oddByteValue >= 0)
				{
					buffer[num2] = (byte)this.m_oddByteValue;
					num2++;
					position += 1L;
				}
				else
				{
					this.ReadByte((int)num3);
					buffer[num2] = (byte)this.m_oddByteValue;
					num2++;
					position += 1L;
				}
				num--;
				num3 += 1L;
				if (num == 0)
				{
					return 1;
				}
			}
			long length = this.GetLength();
			long num5;
			long num6;
			if (num == 1)
			{
				if (position < length)
				{
					int num4 = this.ReadByte((int)num3);
					buffer[num2] = (byte)num4;
					num2++;
					position += 1L;
					return 1;
				}
				num5 = 0L;
			}
			else
			{
				if (num + num2 <= buffer.Length)
				{
					num5 = (long)(num / 2);
				}
				else
				{
					num5 = (long)((buffer.Length - num2) / 2);
				}
				num6 = num5 * 2L;
				long num7 = position + num6;
				if (num7 > length)
				{
					num5 = (length - position) / 2L;
				}
			}
			if (num5 == 0L)
			{
				return 0;
			}
			Encoding unicode = Encoding.Unicode;
			if (this.m_xmlStreamType == TypeOfXmlData.Chars)
			{
				unicode.GetBytes(this.m_xmlChars, (int)num3, (int)num5, buffer, num2);
			}
			else
			{
				unicode.GetBytes(this.m_xmlStr, (int)num3, (int)num5, buffer, num2);
			}
			num6 = num5 * 2L;
			position += num6;
			if (flag)
			{
				num6 += 1L;
			}
			return (int)num6;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000BDF6C File Offset: 0x000BC16C
		private int ReadByte(int charOffset)
		{
			byte[] array = new byte[2];
			Encoding unicode = Encoding.Unicode;
			if (this.m_xmlStreamType == TypeOfXmlData.Chars)
			{
				unicode.GetBytes(this.m_xmlChars, charOffset, 1, array, 0);
			}
			else
			{
				unicode.GetBytes(this.m_xmlStr, charOffset, 1, array, 0);
			}
			this.m_oddByteValue = (int)array[1];
			return (int)array[0];
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000BDFC0 File Offset: 0x000BC1C0
		internal void Dispose()
		{
			this.m_xmlStr = null;
			this.m_xmlChars = null;
			if (this.m_xmlBlobText != null)
			{
				this.m_xmlBlobText.Clear();
				this.m_xmlBlobText = null;
			}
			if (this.m_xmlClob != null)
			{
				this.m_xmlClob.Dispose();
			}
			this.m_oddByteValue = -1;
			this.m_connImpl = null;
			this.m_xmlTypeData = null;
			this.m_xmlStreamType = TypeOfXmlData.String;
			this.m_bInitialized = false;
		}

		// Token: 0x04001363 RID: 4963
		internal object m_syncLock = new object();

		// Token: 0x04001364 RID: 4964
		internal OracleConnectionImpl m_connImpl;

		// Token: 0x04001365 RID: 4965
		internal OraXmlTypeData m_xmlTypeData;

		// Token: 0x04001366 RID: 4966
		internal OracleXmlTypeImpl m_xmlTypeImplObj;

		// Token: 0x04001367 RID: 4967
		private long m_streamDataLength = -1L;

		// Token: 0x04001368 RID: 4968
		internal string m_xmlStr;

		// Token: 0x04001369 RID: 4969
		internal char[] m_xmlChars;

		// Token: 0x0400136A RID: 4970
		internal OracleClob m_xmlClob;

		// Token: 0x0400136B RID: 4971
		internal StringBuilder m_xmlBlobText;

		// Token: 0x0400136C RID: 4972
		internal int m_oddByteValue = -1;

		// Token: 0x0400136D RID: 4973
		internal TypeOfXmlData m_xmlStreamType = TypeOfXmlData.String;

		// Token: 0x0400136E RID: 4974
		private bool m_bInitialized;
	}
}
