using System;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000250 RID: 592
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleString : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x060016F1 RID: 5873 RVA: 0x000F4544 File Offset: 0x000F2744
		public OracleString(string data)
		{
			this = new OracleString(data, true);
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x000F4550 File Offset: 0x000F2750
		public OracleString(string data, bool isCaseIgnored)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (data != null)
			{
				this.m_bNotNull = true;
				this.m_value = data;
			}
			else
			{
				this.m_bNotNull = false;
				this.m_value = null;
			}
			this.m_bCaseIgnored = isCaseIgnored;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x000F45B8 File Offset: 0x000F27B8
		public OracleString(byte[] bytes, bool isUnicode)
		{
			this = new OracleString(bytes, isUnicode, true);
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000F45C4 File Offset: 0x000F27C4
		public OracleString(byte[] bytes, bool isUnicode, bool isCaseIgnored)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_value = null;
				if (bytes == null)
				{
					throw new ArgumentNullException("bytes");
				}
				if (!isUnicode)
				{
					CultureInfo currentCulture = CultureInfo.CurrentCulture;
					this.m_value = Encoding.GetEncoding(currentCulture.TextInfo.ANSICodePage).GetString(bytes);
				}
				else
				{
					Decoder decoder = Encoding.Unicode.GetDecoder();
					int num = decoder.GetCharCount(bytes, 0, bytes.Length);
					char[] array = new char[num];
					num = decoder.GetChars(bytes, 0, bytes.Length, array, 0);
					this.m_value = new string(array);
				}
				this.m_bCaseIgnored = isCaseIgnored;
				this.m_bNotNull = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000F46B8 File Offset: 0x000F28B8
		public OracleString(byte[] bytes, int index, int count, bool isUnicode)
		{
			this = new OracleString(bytes, index, count, isUnicode, true);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000F46C8 File Offset: 0x000F28C8
		public OracleString(byte[] bytes, int index, int count, bool isUnicode, bool isCaseIgnored)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_value = null;
				if (bytes == null)
				{
					throw new ArgumentNullException("bytes");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (index < 0 || index >= bytes.Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (!isUnicode)
				{
					CultureInfo currentCulture = CultureInfo.CurrentCulture;
					if (bytes.Length - index < count)
					{
						this.m_value = Encoding.GetEncoding(currentCulture.TextInfo.ANSICodePage).GetString(bytes, index, bytes.Length - index);
					}
					else
					{
						this.m_value = Encoding.GetEncoding(currentCulture.TextInfo.ANSICodePage).GetString(bytes, index, count);
					}
				}
				else
				{
					if (bytes.Length < count * 2)
					{
						count = bytes.Length / 2;
					}
					Decoder decoder = Encoding.Unicode.GetDecoder();
					int num = decoder.GetCharCount(bytes, index, count);
					char[] array = new char[num];
					num = decoder.GetChars(bytes, index, count, array, 0);
					if (count < num)
					{
						array[count] = '\0';
					}
					this.m_value = new string(array);
				}
				this.m_bCaseIgnored = isCaseIgnored;
				this.m_bNotNull = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x000F4824 File Offset: 0x000F2A24
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000F4838 File Offset: 0x000F2A38
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x000F483C File Offset: 0x000F2A3C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute == null || !XmlConvert.ToBoolean(attribute))
			{
				this.m_value = reader.ReadElementString();
				this.m_bNotNull = true;
			}
			else
			{
				this.m_value = null;
				this.m_bNotNull = false;
			}
			this.m_bCaseIgnored = true;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000F4890 File Offset: 0x000F2A90
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(this.m_value);
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x000F48C4 File Offset: 0x000F2AC4
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170003A5 RID: 933
		public char this[int index]
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_value.ToCharArray()[index];
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x000F48F0 File Offset: 0x000F2AF0
		public int Length
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_value.Length;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x000F490C File Offset: 0x000F2B0C
		public string Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_value;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x000F4924 File Offset: 0x000F2B24
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x000F492C File Offset: 0x000F2B2C
		public bool IsCaseIgnored
		{
			get
			{
				return this.m_bCaseIgnored;
			}
			set
			{
				this.m_bCaseIgnored = value;
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000F4938 File Offset: 0x000F2B38
		public OracleString Clone()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleString result;
			try
			{
				result = new OracleString(this.m_value, this.m_bCaseIgnored);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000F49B8 File Offset: 0x000F2BB8
		public int CompareTo(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (obj.GetType() != typeof(OracleString))
				{
					throw new ArgumentException("obj");
				}
				result = OracleString.StringCompare(this, (OracleString)obj);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000F4A68 File Offset: 0x000F2C68
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null || obj.GetType() != typeof(OracleString))
				{
					result = false;
				}
				else
				{
					result = OracleString.Equals(this, (OracleString)obj);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000F4B08 File Offset: 0x000F2D08
		public override int GetHashCode()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (this.m_bNotNull)
				{
					result = this.m_value.GetHashCode();
				}
				else
				{
					result = 0;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000F4B74 File Offset: 0x000F2D74
		public byte[] GetNonUnicodeBytes()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			byte[] bytes;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				bytes = Encoding.GetEncoding(currentCulture.TextInfo.ANSICodePage).GetBytes(this.m_value);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return bytes;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000F4C14 File Offset: 0x000F2E14
		public byte[] GetUnicodeBytes()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				Encoder encoder = Encoding.Unicode.GetEncoder();
				char[] array = this.m_value.ToCharArray();
				int num = encoder.GetByteCount(array, 0, array.Length, true);
				byte[] array2 = new byte[num];
				num = encoder.GetBytes(array, 0, array.Length, array2, 0, true);
				result = array2;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000F4CD0 File Offset: 0x000F2ED0
		public override string ToString()
		{
			if (this.m_bNotNull)
			{
				return this.m_value;
			}
			return "null";
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000F4CE8 File Offset: 0x000F2EE8
		public static OracleString Concat(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleString result;
			try
			{
				CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
				if (compareNullEnum == CompareNullEnum.BothNotNull)
				{
					if (value1.m_bCaseIgnored != value2.m_bCaseIgnored)
					{
						throw new OracleTypeException(ResourceStringConstants.TYP_COMPARE_COLLATION, new object[0]);
					}
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder = stringBuilder.Append(value1.m_value);
					stringBuilder = stringBuilder.Append(value2.m_value);
					result = new OracleString(stringBuilder.ToString(), value1.m_bCaseIgnored);
				}
				else
				{
					result = OracleString.Null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x000F4DD4 File Offset: 0x000F2FD4
		public static bool Equals(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (OracleString.StringCompare(value1, value2) == 0);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000F4E30 File Offset: 0x000F3030
		public static bool GreaterThan(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (OracleString.StringCompare(value1, value2) > 0);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x000F4E8C File Offset: 0x000F308C
		public static bool GreaterThanOrEqual(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (OracleString.StringCompare(value1, value2) >= 0);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000F4EEC File Offset: 0x000F30EC
		public static bool LessThan(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (OracleString.StringCompare(value1, value2) < 0);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x000F4F48 File Offset: 0x000F3148
		public static bool LessThanOrEqual(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (OracleString.StringCompare(value1, value2) <= 0);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x000F4FA8 File Offset: 0x000F31A8
		public static bool NotEquals(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = !OracleString.Equals(value1, value2);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000F5004 File Offset: 0x000F3204
		public static bool operator ==(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleString.Equals(value1, value2);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000F5060 File Offset: 0x000F3260
		public static bool operator >(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleString.GreaterThan(value1, value2);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000F50BC File Offset: 0x000F32BC
		public static bool operator >=(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleString.GreaterThanOrEqual(value1, value2);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000F5118 File Offset: 0x000F3318
		public static bool operator <(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleString.LessThan(value1, value2);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000F5174 File Offset: 0x000F3374
		public static bool operator <=(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleString.LessThanOrEqual(value1, value2);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x000F51D0 File Offset: 0x000F33D0
		public static bool operator !=(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleString.NotEquals(value1, value2);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000F522C File Offset: 0x000F342C
		public static OracleString operator +(OracleString value1, OracleString value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleString result;
			try
			{
				result = OracleString.Concat(value1, value2);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x000F52A4 File Offset: 0x000F34A4
		public static explicit operator string(OracleString value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			string value2;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				value2 = value1.Value;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return value2;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000F5328 File Offset: 0x000F3528
		public static implicit operator OracleString(string value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleString result;
			try
			{
				result = new OracleString(value1);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000F539C File Offset: 0x000F359C
		internal static int StringCompare(OracleString oraStr1, OracleString oraStr2)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!oraStr1.m_bNotNull, !oraStr2.m_bNotNull);
				if (compareNullEnum == CompareNullEnum.BothNull)
				{
					result = 0;
				}
				else if (compareNullEnum == CompareNullEnum.FirstNullOnly)
				{
					result = -1;
				}
				else if (compareNullEnum == CompareNullEnum.SecondNullOnly)
				{
					result = 1;
				}
				else
				{
					if (oraStr1.m_bCaseIgnored != oraStr2.m_bCaseIgnored)
					{
						throw new OracleTypeException(ResourceStringConstants.TYP_COMPARE_COLLATION, new object[0]);
					}
					result = string.Compare(oraStr1.m_value, oraStr2.m_value, oraStr1.m_bCaseIgnored);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x04001A2C RID: 6700
		private string m_value;

		// Token: 0x04001A2D RID: 6701
		private bool m_bNotNull;

		// Token: 0x04001A2E RID: 6702
		private bool m_bCaseIgnored;

		// Token: 0x04001A2F RID: 6703
		public static readonly OracleString Null;
	}
}
