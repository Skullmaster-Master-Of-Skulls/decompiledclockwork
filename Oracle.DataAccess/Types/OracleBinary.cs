using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x020000D5 RID: 213
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleBinary : IComparable, INullable, IXmlSerializable
	{
		// Token: 0x060007F2 RID: 2034 RVA: 0x0004F6E0 File Offset: 0x0004E6E0
		static OracleBinary()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0004F6EE File Offset: 0x0004E6EE
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0004F6FF File Offset: 0x0004E6FF
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0004F704 File Offset: 0x0004E704
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute == null || !XmlConvert.ToBoolean(attribute))
			{
				this.m_value = Convert.FromBase64String(reader.ReadElementString());
				this.m_bNotNull = true;
				return;
			}
			this.m_value = null;
			this.m_bNotNull = false;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0004F754 File Offset: 0x0004E754
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(Convert.ToBase64String(this.m_value));
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0004F78A File Offset: 0x0004E78A
		public OracleBinary(byte[] data)
		{
			if (data != null)
			{
				this.m_bNotNull = true;
				this.m_value = new byte[data.Length];
				data.CopyTo(this.m_value, 0);
				return;
			}
			this.m_bNotNull = false;
			this.m_value = null;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0004F7C0 File Offset: 0x0004E7C0
		internal OracleBinary(byte[] data, int index, int length)
		{
			if (data != null)
			{
				this.m_bNotNull = true;
				this.m_value = new byte[length];
				Array.Copy(data, index, this.m_value, 0, length);
				return;
			}
			this.m_bNotNull = false;
			this.m_value = null;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0004F7F8 File Offset: 0x0004E7F8
		internal OracleBinary(byte[] data, bool bCopy)
		{
			if (data == null)
			{
				this.m_bNotNull = false;
				this.m_value = null;
				return;
			}
			this.m_bNotNull = true;
			if (bCopy)
			{
				this.m_value = new byte[data.Length];
				data.CopyTo(this.m_value, 0);
				return;
			}
			this.m_value = data;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0004F844 File Offset: 0x0004E844
		public static OracleBinary Concat(OracleBinary value1, OracleBinary value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBinary::appConcat()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum != CompareNullEnum.BothNotNull)
			{
				return OracleBinary.Null;
			}
			byte[] value3 = value1.m_value;
			byte[] value4 = value2.m_value;
			byte[] array = new byte[value3.Length + value4.Length];
			Array.Copy(value3, array, value3.Length);
			Array.Copy(value4, 0, array, value3.Length, value4.Length);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBinary::Concat()\n"
				});
			}
			return new OracleBinary(array);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0004F8F4 File Offset: 0x0004E8F4
		public static bool Equals(OracleBinary value1, OracleBinary value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBinary::Equals(1)\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::Equals(1)\n"
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
						" (EXIT)  OracleBinary::Equals(1)\n"
					});
				}
				return false;
			}
			byte[] value3 = value1.m_value;
			byte[] value4 = value2.m_value;
			if (value3.Length != value4.Length)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::Equals(1)\n"
					});
				}
				return false;
			}
			for (int i = 0; i < value3.Length; i++)
			{
				if (value3[i] != value4[i])
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleBinary::Equals(1)\n"
						});
					}
					return false;
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBinary::Equals(1)\n"
				});
			}
			return true;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0004FA1C File Offset: 0x0004EA1C
		public static bool GreaterThan(OracleBinary value1, OracleBinary value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBinary::GreaterThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::GreaterThan()\n"
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
						" (EXIT)  OracleBinary::GreaterThan()\n"
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
						" (EXIT)  OracleBinary::GreaterThan()\n"
					});
				}
				return true;
			}
			byte[] value3 = value1.m_value;
			byte[] value4 = value2.m_value;
			int num;
			if (value3.Length <= value4.Length)
			{
				num = value3.Length;
			}
			else
			{
				num = value4.Length;
			}
			int i = 0;
			while (i < num)
			{
				if (value3[i] != value4[i])
				{
					if (value3[i] > value4[i])
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (EXIT)  OracleBinary::GreaterThan()\n"
							});
						}
						return true;
					}
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleBinary::GreaterThan()\n"
						});
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBinary::GreaterThan()\n"
				});
			}
			return value3.Length > value4.Length;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0004FB8C File Offset: 0x0004EB8C
		public static bool GreaterThanOrEqual(OracleBinary value1, OracleBinary value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBinary::GreaterThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleBinary::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleBinary::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			byte[] value3 = value1.m_value;
			byte[] value4 = value2.m_value;
			int num;
			if (value3.Length <= value4.Length)
			{
				num = value3.Length;
			}
			else
			{
				num = value4.Length;
			}
			int i = 0;
			while (i < num)
			{
				if (value3[i] != value4[i])
				{
					if (value3[i] > value4[i])
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (EXIT)  OracleBinary::GreaterThanOrEqual()\n"
							});
						}
						return true;
					}
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleBinary::GreaterThanOrEqual()\n"
						});
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBinary::GreaterThanOrEqual()\n"
				});
			}
			return value3.Length >= value4.Length;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0004FCFC File Offset: 0x0004ECFC
		public static bool LessThan(OracleBinary value1, OracleBinary value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBinary::LessThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::LessThan()\n"
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
						" (EXIT)  OracleBinary::LessThan()\n"
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
						" (EXIT)  OracleBinary::LessThan()\n"
					});
				}
				return false;
			}
			byte[] value3 = value1.m_value;
			byte[] value4 = value2.m_value;
			int num;
			if (value3.Length <= value4.Length)
			{
				num = value3.Length;
			}
			else
			{
				num = value4.Length;
			}
			int i = 0;
			while (i < num)
			{
				if (value3[i] != value4[i])
				{
					if (value3[i] < value4[i])
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (EXIT)  OracleBinary::LessThan()\n"
							});
						}
						return true;
					}
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleBinary::LessThan()\n"
						});
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBinary::LessThan()\n"
				});
			}
			return value3.Length < value4.Length;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0004FE6C File Offset: 0x0004EE6C
		public static bool LessThanOrEqual(OracleBinary value1, OracleBinary value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBinary::LessThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::LessThanOrEqual()\n"
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
						" (EXIT)  OracleBinary::LessThanOrEqual()\n"
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
						" (EXIT)  OracleBinary::LessThanOrEqual()\n"
					});
				}
				return false;
			}
			byte[] value3 = value1.m_value;
			byte[] value4 = value2.m_value;
			int num;
			if (value3.Length <= value4.Length)
			{
				num = value3.Length;
			}
			else
			{
				num = value4.Length;
			}
			int i = 0;
			while (i < num)
			{
				if (value3[i] != value4[i])
				{
					if (value3[i] < value4[i])
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (EXIT)  OracleBinary::LessThanOrEqual()\n"
							});
						}
						return true;
					}
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleBinary::LessThanOrEqual()\n"
						});
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBinary::LessThanOrEqual()\n"
				});
			}
			return value3.Length <= value4.Length;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0004FFDC File Offset: 0x0004EFDC
		public static bool NotEquals(OracleBinary value1, OracleBinary value2)
		{
			return !OracleBinary.Equals(value1, value2);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0004FFE8 File Offset: 0x0004EFE8
		public static bool operator ==(OracleBinary value1, OracleBinary value2)
		{
			return OracleBinary.Equals(value1, value2);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0004FFF1 File Offset: 0x0004EFF1
		public static bool operator >(OracleBinary value1, OracleBinary value2)
		{
			return OracleBinary.GreaterThan(value1, value2);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0004FFFA File Offset: 0x0004EFFA
		public static bool operator >=(OracleBinary value1, OracleBinary value2)
		{
			return OracleBinary.GreaterThanOrEqual(value1, value2);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00050003 File Offset: 0x0004F003
		public static bool operator <(OracleBinary value1, OracleBinary value2)
		{
			return OracleBinary.LessThan(value1, value2);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0005000C File Offset: 0x0004F00C
		public static bool operator <=(OracleBinary value1, OracleBinary value2)
		{
			return OracleBinary.LessThanOrEqual(value1, value2);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00050015 File Offset: 0x0004F015
		public static bool operator !=(OracleBinary value1, OracleBinary value2)
		{
			return OracleBinary.NotEquals(value1, value2);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0005001E File Offset: 0x0004F01E
		public static OracleBinary operator +(OracleBinary value1, OracleBinary value2)
		{
			return OracleBinary.Concat(value1, value2);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00050027 File Offset: 0x0004F027
		public static explicit operator byte[](OracleBinary value1)
		{
			if (value1.m_bNotNull)
			{
				return value1.m_value;
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0005003F File Offset: 0x0004F03F
		public static implicit operator OracleBinary(byte[] value1)
		{
			return new OracleBinary(value1);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00050047 File Offset: 0x0004F047
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00050052 File Offset: 0x0004F052
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

		// Token: 0x1700013E RID: 318
		public byte this[int index]
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_value[index];
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00050084 File Offset: 0x0004F084
		public byte[] Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					byte[] array = new byte[this.m_value.Length];
					this.m_value.CopyTo(array, 0);
					return array;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000500BC File Offset: 0x0004F0BC
		public int CompareTo(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBinary::CompareTo()\n"
				});
			}
			if (obj.GetType() != typeof(OracleBinary))
			{
				throw new ArgumentException();
			}
			OracleBinary oracleBinary = (OracleBinary)obj;
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleBinary.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::CompareTo()\n"
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
						" (EXIT)  OracleBinary::CompareTo()\n"
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
						" (EXIT)  OracleBinary::CompareTo()\n"
					});
				}
				return 1;
			}
			byte[] value = this.m_value;
			byte[] value2 = oracleBinary.m_value;
			int num;
			if (value.Length <= value2.Length)
			{
				num = value.Length;
			}
			else
			{
				num = value2.Length;
			}
			int i = 0;
			while (i < num)
			{
				if (value[i] != value2[i])
				{
					if (value[i] < value2[i])
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (EXIT)  OracleBinary::CompareTo()\n"
							});
						}
						return -1;
					}
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleBinary::CompareTo()\n"
						});
					}
					return 1;
				}
				else
				{
					i++;
				}
			}
			if (value.Length == value2.Length)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::CompareTo()\n"
					});
				}
				return 0;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBinary::CompareTo()\n"
				});
			}
			if (value.Length < value2.Length)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0005027C File Offset: 0x0004F27C
		public override bool Equals(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleBinary::Equals(2)\n"
				});
			}
			if (obj == null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::Equals(2)\n"
					});
				}
				return false;
			}
			if (obj.GetType() != typeof(OracleBinary))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleBinary::Equals(2)\n"
					});
				}
				return false;
			}
			OracleBinary value = (OracleBinary)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleBinary::Equals(2)\n"
				});
			}
			return OracleBinary.Equals(this, value);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00050331 File Offset: 0x0004F331
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00050348 File Offset: 0x0004F348
		public override string ToString()
		{
			if (this.m_bNotNull)
			{
				return string.Concat(new object[]
				{
					base.GetType(),
					"(",
					this.m_value.Length,
					")"
				});
			}
			return "null";
		}

		// Token: 0x0400067D RID: 1661
		public static readonly OracleBinary Null;

		// Token: 0x0400067E RID: 1662
		internal byte[] m_value;

		// Token: 0x0400067F RID: 1663
		private bool m_bNotNull;
	}
}
