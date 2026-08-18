using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x020000D3 RID: 211
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleString : IComparable, INullable, IXmlSerializable
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x0004B9EA File Offset: 0x0004A9EA
		static OracleString()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0004B9F8 File Offset: 0x0004A9F8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0004BA09 File Offset: 0x0004AA09
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0004BA0C File Offset: 0x0004AA0C
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

		// Token: 0x0600079A RID: 1946 RVA: 0x0004BA5F File Offset: 0x0004AA5F
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(this.m_value);
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0004BA90 File Offset: 0x0004AA90
		public OracleString(string data)
		{
			this = new OracleString(data, true);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0004BA9C File Offset: 0x0004AA9C
		public OracleString(string data, bool isCaseIgnored)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::OracleString(1)\n"
				});
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
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleString::OracleString(1)\n"
				});
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0004BB0B File Offset: 0x0004AB0B
		public OracleString(byte[] bytes, bool isUnicode)
		{
			this = new OracleString(bytes, isUnicode, true);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0004BB18 File Offset: 0x0004AB18
		public OracleString(byte[] bytes, bool isUnicode, bool isCaseIgnored)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::OracleString(2)\n"
				});
			}
			this.m_value = null;
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = 0;
			int srcLen = bytes.Length;
			if (!isUnicode)
			{
				GCHandle gchandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
				try
				{
					try
					{
						num = OpsStr.BytesToUnicode(gchandle.AddrOfPinnedObject(), srcLen, 0, -1, out this.m_value);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					goto IL_D2;
				}
				finally
				{
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
					if (num != 0)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			Decoder decoder = Encoding.Unicode.GetDecoder();
			int num2 = decoder.GetCharCount(bytes, 0, bytes.Length);
			char[] array = new char[num2];
			num2 = decoder.GetChars(bytes, 0, bytes.Length, array, 0);
			this.m_value = new string(array);
			IL_D2:
			this.m_bCaseIgnored = isCaseIgnored;
			this.m_bNotNull = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleString::OracleString(2)\n"
				});
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0004BC44 File Offset: 0x0004AC44
		public OracleString(byte[] bytes, int index, int count, bool isUnicode)
		{
			this = new OracleString(bytes, index, count, isUnicode, true);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0004BC54 File Offset: 0x0004AC54
		public OracleString(byte[] bytes, int index, int count, bool isUnicode, bool isCaseIgnored)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::OracleString(3)\n"
				});
			}
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
				throw new IndexOutOfRangeException();
			}
			int num = 0;
			int srcLen = bytes.Length;
			if (!isUnicode)
			{
				GCHandle gchandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
				try
				{
					try
					{
						num = OpsStr.BytesToUnicode(gchandle.AddrOfPinnedObject(), srcLen, index, count, out this.m_value);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					goto IL_107;
				}
				finally
				{
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
					if (num != 0)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			if (bytes.Length < count * 2)
			{
				count = bytes.Length / 2;
			}
			Decoder decoder = Encoding.Unicode.GetDecoder();
			int num2 = decoder.GetCharCount(bytes, index, count);
			char[] array = new char[num2];
			num2 = decoder.GetChars(bytes, index, count, array, 0);
			if (count < num2)
			{
				array[count] = '\0';
			}
			this.m_value = new string(array);
			IL_107:
			this.m_bCaseIgnored = isCaseIgnored;
			this.m_bNotNull = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleString::OracleString(3)\n"
				});
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0004BDB4 File Offset: 0x0004ADB4
		public static OracleString Concat(OracleString value1, OracleString value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::Concat()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum != CompareNullEnum.BothNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::Concat()\n"
					});
				}
				return OracleString.Null;
			}
			if (value1.m_bCaseIgnored == value2.m_bCaseIgnored)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::Concat()\n"
					});
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder = stringBuilder.Append(value1.m_value);
				stringBuilder = stringBuilder.Append(value2.m_value);
				return new OracleString(stringBuilder.ToString(), value1.m_bCaseIgnored);
			}
			throw new OracleTypeException(ErrRes.TYP_COMPARE_COLLATION, new object[0]);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0004BE98 File Offset: 0x0004AE98
		public static bool Equals(OracleString value1, OracleString value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::Equals(1)\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::Equals()\n"
					});
				}
				return true;
			}
			if (compareNullEnum != CompareNullEnum.BothNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::Equals()\n"
					});
				}
				return false;
			}
			if (value1.m_bCaseIgnored == value2.m_bCaseIgnored)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::Equals()\n"
					});
				}
				return OracleString.StringCompare(value1, value2, value1.m_bCaseIgnored) == 0;
			}
			throw new OracleTypeException(ErrRes.TYP_COMPARE_COLLATION, new object[0]);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0004BF78 File Offset: 0x0004AF78
		public static bool GreaterThan(OracleString value1, OracleString value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::GreaterThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::GreaterThan()\n"
					});
				}
				return false;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::GreaterThan()\n"
					});
				}
				return false;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::GreaterThan()\n"
					});
				}
				return true;
			}
			if (value1.m_bCaseIgnored == value2.m_bCaseIgnored)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::GreaterThan()\n"
					});
				}
				return OracleString.StringCompare(value1, value2, value1.m_bCaseIgnored) > 0;
			}
			throw new OracleTypeException(ErrRes.TYP_COMPARE_COLLATION, new object[0]);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0004C080 File Offset: 0x0004B080
		public static bool GreaterThanOrEqual(OracleString value1, OracleString value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::GreaterThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleString::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleString::GreaterThanOrEqual()\n"
					});
				}
				return false;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleString::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			if (value1.m_bCaseIgnored == value2.m_bCaseIgnored)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleString::GreaterThanOrEqual()\n"
					});
				}
				return OracleString.StringCompare(value1, value2, value1.m_bCaseIgnored) >= 0;
			}
			throw new OracleTypeException(ErrRes.TYP_COMPARE_COLLATION, new object[0]);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0004C188 File Offset: 0x0004B188
		public static bool LessThan(OracleString value1, OracleString value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::LessThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::LessThan()\n"
					});
				}
				return false;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::LessThan()\n"
					});
				}
				return true;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::LessThan()\n"
					});
				}
				return false;
			}
			if (value1.m_bCaseIgnored == value2.m_bCaseIgnored)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::LessThan()\n"
					});
				}
				return OracleString.StringCompare(value1, value2, value1.m_bCaseIgnored) < 0;
			}
			throw new OracleTypeException(ErrRes.TYP_COMPARE_COLLATION, new object[0]);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0004C290 File Offset: 0x0004B290
		public static bool LessThanOrEqual(OracleString value1, OracleString value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::LessThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::LessThanOrEqual()\n"
					});
				}
				return true;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::LessThanOrEqual()\n"
					});
				}
				return true;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::LessThanOrEqual()\n"
					});
				}
				return false;
			}
			if (value1.m_bCaseIgnored == value2.m_bCaseIgnored)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::LessThanOrEqual()\n"
					});
				}
				return OracleString.StringCompare(value1, value2, value1.m_bCaseIgnored) <= 0;
			}
			throw new OracleTypeException(ErrRes.TYP_COMPARE_COLLATION, new object[0]);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0004C395 File Offset: 0x0004B395
		public static bool NotEquals(OracleString value1, OracleString value2)
		{
			return !OracleString.Equals(value1, value2);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0004C3A1 File Offset: 0x0004B3A1
		public static bool operator ==(OracleString value1, OracleString value2)
		{
			return OracleString.Equals(value1, value2);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0004C3AA File Offset: 0x0004B3AA
		public static bool operator >(OracleString value1, OracleString value2)
		{
			return OracleString.GreaterThan(value1, value2);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0004C3B3 File Offset: 0x0004B3B3
		public static bool operator >=(OracleString value1, OracleString value2)
		{
			return OracleString.GreaterThanOrEqual(value1, value2);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0004C3BC File Offset: 0x0004B3BC
		public static bool operator <(OracleString value1, OracleString value2)
		{
			return OracleString.LessThan(value1, value2);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0004C3C5 File Offset: 0x0004B3C5
		public static bool operator <=(OracleString value1, OracleString value2)
		{
			return OracleString.LessThanOrEqual(value1, value2);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0004C3CE File Offset: 0x0004B3CE
		public static bool operator !=(OracleString value1, OracleString value2)
		{
			return OracleString.NotEquals(value1, value2);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0004C3D7 File Offset: 0x0004B3D7
		public static OracleString operator +(OracleString value1, OracleString value2)
		{
			return OracleString.Concat(value1, value2);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0004C3E0 File Offset: 0x0004B3E0
		public static explicit operator string(OracleString value1)
		{
			if (value1.m_bNotNull)
			{
				return value1.Value;
			}
			throw new OracleNullValueException();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0004C3F8 File Offset: 0x0004B3F8
		public static implicit operator OracleString(string value1)
		{
			return new OracleString(value1);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0004C400 File Offset: 0x0004B400
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0004C40B File Offset: 0x0004B40B
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0004C413 File Offset: 0x0004B413
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0004C41C File Offset: 0x0004B41C
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

		// Token: 0x1700012D RID: 301
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0004C454 File Offset: 0x0004B454
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

		// Token: 0x060007B7 RID: 1975 RVA: 0x0004C46C File Offset: 0x0004B46C
		public OracleString Clone()
		{
			OracleString result = new OracleString(this.m_value, this.m_bCaseIgnored);
			return result;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0004C490 File Offset: 0x0004B490
		public int CompareTo(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::CompareTo()\n"
				});
			}
			if (obj.GetType() != typeof(OracleString))
			{
				throw new ArgumentException();
			}
			OracleString oraStr = (OracleString)obj;
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oraStr.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::CompareTo()\n"
					});
				}
				return 0;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::CompareTo()\n"
					});
				}
				return -1;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::CompareTo()\n"
					});
				}
				return 1;
			}
			if (this.m_bCaseIgnored == oraStr.m_bCaseIgnored)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::CompareTo()\n"
					});
				}
				return OracleString.StringCompare(this, oraStr, this.m_bCaseIgnored);
			}
			throw new OracleTypeException(ErrRes.TYP_COMPARE_COLLATION, new object[0]);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0004C5B8 File Offset: 0x0004B5B8
		public override bool Equals(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::Equals(1)\n"
				});
			}
			if (obj == null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::Equals(2)\n"
					});
				}
				return false;
			}
			if (obj.GetType() != typeof(OracleString))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::Equals(2)\n"
					});
				}
				return false;
			}
			OracleString value = (OracleString)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleString::Equals(2)\n"
				});
			}
			return OracleString.Equals(this, value);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0004C66D File Offset: 0x0004B66D
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0004C684 File Offset: 0x0004B684
		public byte[] GetNonUnicodeBytes()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::GetNonUnicodeBytes()\n"
				});
			}
			if (this.m_bNotNull)
			{
				int num = 0;
				IntPtr zero = IntPtr.Zero;
				uint num2 = 0U;
				GCHandle gchandle = GCHandle.Alloc(this.m_value, GCHandleType.Pinned);
				try
				{
					num = OpsStr.UnicodeToBytes(gchandle.AddrOfPinnedObject(), this.m_value.Length, out zero, out num2);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
					if (num != 0)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
				byte[] array = new byte[num2];
				Marshal.Copy(zero, array, 0, (int)num2);
				Marshal.FreeCoTaskMem(zero);
				return array;
			}
			throw new OracleNullValueException();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0004C764 File Offset: 0x0004B764
		public byte[] GetUnicodeBytes()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleString::GetUnicodeBytes()\n"
				});
			}
			if (this.m_bNotNull)
			{
				Encoder encoder = Encoding.Unicode.GetEncoder();
				char[] array = this.m_value.ToCharArray();
				int num = encoder.GetByteCount(array, 0, array.Length, true);
				byte[] array2 = new byte[num];
				num = encoder.GetBytes(array, 0, array.Length, array2, 0, true);
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleString::GetUnicodeBytes()\n"
					});
				}
				return array2;
			}
			throw new OracleNullValueException();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0004C7FA File Offset: 0x0004B7FA
		public override string ToString()
		{
			if (this.m_bNotNull)
			{
				return this.m_value;
			}
			return "null";
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0004C810 File Offset: 0x0004B810
		internal OracleString(IntPtr data, int count, bool isUnicode)
		{
			if (data != IntPtr.Zero)
			{
				if (isUnicode)
				{
					this.m_value = Marshal.PtrToStringUni(data, count);
				}
				else
				{
					int num = 0;
					string value;
					try
					{
						num = OpsStr.BytesToUnicode(data, count, 0, count, out value);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					if (num != 0)
					{
						throw new OracleTypeException(num, new object[0]);
					}
					this.m_value = value;
				}
				this.m_bCaseIgnored = true;
				this.m_bNotNull = true;
				return;
			}
			this.m_bNotNull = false;
			this.m_bCaseIgnored = true;
			this.m_value = null;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0004C8A8 File Offset: 0x0004B8A8
		internal static int StringCompare(OracleString oraStr1, OracleString oraStr2, bool fCaseInsensitive)
		{
			int isCaseInsensitive;
			if (fCaseInsensitive)
			{
				isCaseInsensitive = 1;
			}
			else
			{
				isCaseInsensitive = 0;
			}
			int result = 0;
			int num = 0;
			GCHandle gchandle = GCHandle.Alloc(oraStr1.m_value, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(oraStr2.m_value, GCHandleType.Pinned);
			try
			{
				num = OpsStr.StrCompare(gchandle.AddrOfPinnedObject(), oraStr1.m_value.Length, gchandle2.AddrOfPinnedObject(), oraStr2.m_value.Length, isCaseInsensitive, out result);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (gchandle2.IsAllocated)
				{
					gchandle2.Free();
				}
				if (num != 0)
				{
					throw new OracleTypeException(num, new object[0]);
				}
			}
			return result;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0004C974 File Offset: 0x0004B974
		internal static string GetValue(IntPtr data, int count, bool isUnicode)
		{
			if (!(data != IntPtr.Zero))
			{
				return null;
			}
			if (isUnicode)
			{
				return Marshal.PtrToStringUni(data, count);
			}
			int num = 0;
			string result;
			try
			{
				num = OpsStr.BytesToUnicode(data, count, 0, count, out result);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			return result;
		}

		// Token: 0x04000665 RID: 1637
		public static readonly OracleString Null;

		// Token: 0x04000666 RID: 1638
		private string m_value;

		// Token: 0x04000667 RID: 1639
		private bool m_bNotNull;

		// Token: 0x04000668 RID: 1640
		private bool m_bCaseIgnored;
	}
}
