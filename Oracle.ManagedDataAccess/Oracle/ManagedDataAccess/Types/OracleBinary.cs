using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000244 RID: 580
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleBinary : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x0600151A RID: 5402 RVA: 0x000E3EAC File Offset: 0x000E20AC
		public OracleBinary(byte[] data)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (data != null)
			{
				this.m_bNotNull = true;
				this.m_value = new byte[data.Length];
				data.CopyTo(this.m_value, 0);
			}
			else
			{
				this.m_bNotNull = false;
				this.m_value = null;
			}
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x000E3F20 File Offset: 0x000E2120
		internal OracleBinary(byte[] data, bool bCopy)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (data != null)
			{
				this.m_bNotNull = true;
				if (bCopy)
				{
					this.m_value = new byte[data.Length];
					data.CopyTo(this.m_value, 0);
				}
				else
				{
					this.m_value = data;
				}
			}
			else
			{
				this.m_bNotNull = false;
				this.m_value = null;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x000E3FA0 File Offset: 0x000E21A0
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x000E3FAC File Offset: 0x000E21AC
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

		// Token: 0x17000356 RID: 854
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

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x000E3FDC File Offset: 0x000E21DC
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

		// Token: 0x06001520 RID: 5408 RVA: 0x000E4014 File Offset: 0x000E2214
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000E4028 File Offset: 0x000E2228
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000E402C File Offset: 0x000E222C
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

		// Token: 0x06001523 RID: 5411 RVA: 0x000E407C File Offset: 0x000E227C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(Convert.ToBase64String(this.m_value));
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x000E40B4 File Offset: 0x000E22B4
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null || obj.GetType() != typeof(OracleBinary))
				{
					result = false;
				}
				else
				{
					result = (this.CompareTo(obj) == 0);
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

		// Token: 0x06001525 RID: 5413 RVA: 0x000E414C File Offset: 0x000E234C
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
				if (obj.GetType() != typeof(OracleBinary))
				{
					throw new ArgumentException("obj");
				}
				OracleBinary oracleBinary = (OracleBinary)obj;
				CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleBinary.m_bNotNull);
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
					byte[] value = this.m_value;
					byte[] value2 = oracleBinary.m_value;
					int num = Math.Min(value.Length, value2.Length);
					int i = 0;
					while (i < num)
					{
						if (value[i] != value2[i])
						{
							if (value[i] < value2[i])
							{
								return -1;
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
						result = 0;
					}
					else if (value.Length < value2.Length)
					{
						result = -1;
					}
					else
					{
						result = 1;
					}
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

		// Token: 0x06001526 RID: 5414 RVA: 0x000E4298 File Offset: 0x000E2498
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

		// Token: 0x06001527 RID: 5415 RVA: 0x000E4304 File Offset: 0x000E2504
		public override string ToString()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				if (this.m_bNotNull)
				{
					result = string.Concat(new object[]
					{
						base.GetType(),
						"(",
						this.m_value.Length,
						")"
					});
				}
				else
				{
					result = "null";
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

		// Token: 0x06001528 RID: 5416 RVA: 0x000E43C4 File Offset: 0x000E25C4
		public static OracleBinary Concat(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleBinary result;
			try
			{
				CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
				if (compareNullEnum != CompareNullEnum.BothNotNull)
				{
					result = OracleBinary.Null;
				}
				else
				{
					byte[] value3 = value1.m_value;
					byte[] value4 = value2.m_value;
					byte[] array = new byte[value3.Length + value4.Length];
					Array.Copy(value3, array, value3.Length);
					Array.Copy(value4, 0, array, value3.Length, value4.Length);
					result = new OracleBinary(array);
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

		// Token: 0x06001529 RID: 5417 RVA: 0x000E449C File Offset: 0x000E269C
		public static bool Equals(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
				if (compareNullEnum == CompareNullEnum.BothNull)
				{
					result = true;
				}
				else if (compareNullEnum != CompareNullEnum.BothNotNull)
				{
					result = false;
				}
				else
				{
					result = (value1.CompareTo(value2) == 0);
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

		// Token: 0x0600152A RID: 5418 RVA: 0x000E4544 File Offset: 0x000E2744
		public static bool GreaterThan(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) > 0);
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

		// Token: 0x0600152B RID: 5419 RVA: 0x000E45C4 File Offset: 0x000E27C4
		public static bool GreaterThanOrEqual(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) >= 0);
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

		// Token: 0x0600152C RID: 5420 RVA: 0x000E4648 File Offset: 0x000E2848
		public static bool LessThan(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) < 0);
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

		// Token: 0x0600152D RID: 5421 RVA: 0x000E46C8 File Offset: 0x000E28C8
		public static bool LessThanOrEqual(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) <= 0);
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

		// Token: 0x0600152E RID: 5422 RVA: 0x000E474C File Offset: 0x000E294C
		public static bool NotEquals(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) != 0);
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

		// Token: 0x0600152F RID: 5423 RVA: 0x000E47D0 File Offset: 0x000E29D0
		public static bool operator ==(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleBinary.Equals(value1, value2);
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

		// Token: 0x06001530 RID: 5424 RVA: 0x000E4848 File Offset: 0x000E2A48
		public static bool operator >(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleBinary.GreaterThan(value1, value2);
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

		// Token: 0x06001531 RID: 5425 RVA: 0x000E48C0 File Offset: 0x000E2AC0
		public static bool operator >=(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleBinary.GreaterThanOrEqual(value1, value2);
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

		// Token: 0x06001532 RID: 5426 RVA: 0x000E4938 File Offset: 0x000E2B38
		public static bool operator <(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleBinary.LessThan(value1, value2);
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

		// Token: 0x06001533 RID: 5427 RVA: 0x000E49B0 File Offset: 0x000E2BB0
		public static bool operator <=(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleBinary.LessThanOrEqual(value1, value2);
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

		// Token: 0x06001534 RID: 5428 RVA: 0x000E4A28 File Offset: 0x000E2C28
		public static bool operator !=(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleBinary.NotEquals(value1, value2);
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

		// Token: 0x06001535 RID: 5429 RVA: 0x000E4AA0 File Offset: 0x000E2CA0
		public static OracleBinary operator +(OracleBinary value1, OracleBinary value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleBinary result;
			try
			{
				result = OracleBinary.Concat(value1, value2);
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

		// Token: 0x06001536 RID: 5430 RVA: 0x000E4B18 File Offset: 0x000E2D18
		public static explicit operator byte[](OracleBinary value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			byte[] value2;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				value2 = value1.m_value;
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

		// Token: 0x06001537 RID: 5431 RVA: 0x000E4B9C File Offset: 0x000E2D9C
		public static implicit operator OracleBinary(byte[] value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleBinary result;
			try
			{
				result = new OracleBinary(value1);
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

		// Token: 0x0400199F RID: 6559
		internal byte[] m_value;

		// Token: 0x040019A0 RID: 6560
		private bool m_bNotNull;

		// Token: 0x040019A1 RID: 6561
		public static readonly OracleBinary Null;
	}
}
