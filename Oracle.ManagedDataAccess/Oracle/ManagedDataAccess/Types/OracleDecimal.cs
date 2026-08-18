using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Core;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000249 RID: 585
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleDecimal : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x060015FE RID: 5630 RVA: 0x000EB808 File Offset: 0x000E9A08
		public OracleDecimal(byte[] numBytes)
		{
			this = new OracleDecimal(numBytes, true);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000EB814 File Offset: 0x000E9A14
		public OracleDecimal(int intX)
		{
			this = new OracleDecimal((long)intX);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000EB820 File Offset: 0x000E9A20
		public OracleDecimal(long longX)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_bNotNull = true;
				this.m_format = null;
				this.m_numberType = 1;
				this.m_bPositive = (longX > 0L);
				this.m_bZero = (longX == 0L);
				this.m_byteRepresentation = OracleNumberCore.lnxmin(longX);
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

		// Token: 0x06001601 RID: 5633 RVA: 0x000EB8C8 File Offset: 0x000E9AC8
		public OracleDecimal(float floatX)
		{
			this = new OracleDecimal(double.Parse(floatX.ToString()));
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000EB8DC File Offset: 0x000E9ADC
		public OracleDecimal(double doubleX)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (double.IsNaN(doubleX))
				{
					this.m_byteRepresentation = OracleNumberCore.NANREPD;
					this.m_bNotNull = true;
					this.m_numberType = 5;
					this.m_bPositive = true;
					this.m_bZero = false;
					this.m_format = null;
				}
				else if (double.IsPositiveInfinity(doubleX))
				{
					this.m_byteRepresentation = OracleNumberCore.GetPositiveInfinityByteRep();
					this.m_bNotNull = true;
					this.m_numberType = 3;
					this.m_bPositive = true;
					this.m_bZero = false;
					this.m_format = null;
				}
				else if (double.IsNegativeInfinity(doubleX))
				{
					this.m_byteRepresentation = OracleNumberCore.GetNegativeInfinityByteRep();
					this.m_bNotNull = true;
					this.m_numberType = 4;
					this.m_bPositive = false;
					this.m_bZero = false;
					this.m_format = null;
				}
				else if (doubleX == 0.0)
				{
					this.m_byteRepresentation = OracleNumberCore.GetZeroByteRep();
					this.m_bNotNull = true;
					this.m_numberType = 1;
					this.m_bPositive = false;
					this.m_bZero = true;
					this.m_format = null;
				}
				else
				{
					this.m_bNotNull = true;
					this.m_numberType = 2;
					this.m_bPositive = (doubleX > 0.0);
					this.m_bZero = false;
					this.m_format = null;
					this.m_byteRepresentation = OracleNumberCore.lnxren(doubleX);
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
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x000EBA88 File Offset: 0x000E9C88
		public OracleDecimal(decimal decimalX)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				byte[] array = null;
				DecimalConv.GetBytes(decimalX, out array);
				this.m_byteRepresentation = new byte[(int)array[0]];
				Array.Copy(array, 1, this.m_byteRepresentation, 0, (int)array[0]);
				byte[] bytes = BitConverter.GetBytes(decimal.GetBits(decimalX)[3]);
				if (bytes[2] == 0)
				{
					this.m_numberType = 1;
				}
				else
				{
					this.m_numberType = 2;
				}
				if (decimalX > 0m)
				{
					this.m_bPositive = true;
				}
				else
				{
					this.m_bPositive = false;
				}
				if (decimalX == 0m)
				{
					this.m_bZero = true;
				}
				else
				{
					this.m_bZero = false;
				}
				this.m_bNotNull = true;
				this.m_format = null;
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

		// Token: 0x06001604 RID: 5636 RVA: 0x000EBB8C File Offset: 0x000E9D8C
		public OracleDecimal(string numStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (numStr == null)
				{
					throw new ArgumentNullException("numStr");
				}
				if (numStr == "")
				{
					throw new FormatException();
				}
				OracleDecimal.ToBytes(numStr, out this.m_byteRepresentation, out this.m_numberType, out this.m_bPositive, out this.m_bZero);
				this.m_bNotNull = true;
				this.m_format = null;
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

		// Token: 0x06001605 RID: 5637 RVA: 0x000EBC48 File Offset: 0x000E9E48
		internal OracleDecimal(byte[] numBytes, bool bContainsLength)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (numBytes == null)
				{
					throw new ArgumentNullException();
				}
				if (bContainsLength && numBytes.Length != 22)
				{
					throw new ArgumentException();
				}
				if (bContainsLength)
				{
					int num = (int)numBytes[0];
					this.m_byteRepresentation = new byte[num];
					Array.Copy(numBytes, 1, this.m_byteRepresentation, 0, num);
				}
				else
				{
					int num = numBytes.Length;
					if (num == 22 && numBytes[num - 1] == 102)
					{
						num--;
						this.m_byteRepresentation = new byte[num];
						Array.Copy(numBytes, 0, this.m_byteRepresentation, 0, num);
					}
					else
					{
						this.m_byteRepresentation = numBytes;
					}
				}
				if (!OracleNumberCore.isValid(this.m_byteRepresentation))
				{
					this.m_byteRepresentation = null;
					throw new ArgumentException();
				}
				if (OracleNumberCore.IsPositive(this.m_byteRepresentation))
				{
					this.m_bPositive = true;
				}
				else
				{
					this.m_bPositive = false;
				}
				if (OracleNumberCore.IsNaN(this.m_byteRepresentation, 0, 0))
				{
					this.m_numberType = 5;
				}
				else if (OracleNumberCore.IsInt(this.m_byteRepresentation))
				{
					this.m_numberType = 1;
				}
				else if (OracleNumberCore.IsPositiveInfinity(this.m_byteRepresentation))
				{
					this.m_numberType = 3;
				}
				else if (OracleNumberCore.IsNegativeInfinity(this.m_byteRepresentation))
				{
					this.m_numberType = 4;
				}
				else
				{
					this.m_numberType = 2;
				}
				if (OracleNumberCore.IsZero(this.m_byteRepresentation))
				{
					this.m_bZero = true;
				}
				else
				{
					this.m_bZero = false;
				}
				this.m_bNotNull = true;
				this.m_format = null;
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
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000EBE0C File Offset: 0x000EA00C
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000EBE20 File Offset: 0x000EA020
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000EBE24 File Offset: 0x000EA024
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (text == null || !XmlConvert.ToBoolean(text))
			{
				text = reader.ReadElementString();
				OracleDecimal.ToBytes(text, out this.m_byteRepresentation, out this.m_numberType, out this.m_bPositive, out this.m_bZero);
				this.m_bNotNull = true;
				return;
			}
			this.m_bNotNull = false;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000EBE84 File Offset: 0x000EA084
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(this.ToString());
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000EBEBC File Offset: 0x000EA0BC
		public static bool Equals(OracleDecimal value1, OracleDecimal value2)
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
					result = (OracleDecimal.Compare(value1, value2) == 0);
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

		// Token: 0x0600160B RID: 5643 RVA: 0x000EBF60 File Offset: 0x000EA160
		public static bool GreaterThan(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = (OracleDecimal.Compare(value1, value2) > 0);
				}
				else if (value1.m_numberType == 5 && value2.m_numberType != 5)
				{
					result = true;
				}
				else
				{
					result = false;
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

		// Token: 0x0600160C RID: 5644 RVA: 0x000EBFEC File Offset: 0x000EA1EC
		public static bool GreaterThanOrEqual(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = (OracleDecimal.Compare(value1, value2) >= 0);
				}
				else if (value1.m_numberType != 5 && value2.m_numberType == 5)
				{
					result = false;
				}
				else
				{
					result = true;
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

		// Token: 0x0600160D RID: 5645 RVA: 0x000EC07C File Offset: 0x000EA27C
		public static bool LessThan(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = (OracleDecimal.Compare(value1, value2) < 0);
				}
				else if (value1.m_numberType != 5 && value2.m_numberType == 5)
				{
					result = true;
				}
				else
				{
					result = false;
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

		// Token: 0x0600160E RID: 5646 RVA: 0x000EC108 File Offset: 0x000EA308
		public static bool LessThanOrEqual(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = (OracleDecimal.Compare(value1, value2) <= 0);
				}
				else if (value1.m_numberType == 5 && value2.m_numberType != 5)
				{
					result = false;
				}
				else
				{
					result = true;
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

		// Token: 0x0600160F RID: 5647 RVA: 0x000EC198 File Offset: 0x000EA398
		public static bool NotEquals(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (OracleDecimal.Compare(value1, value2) != 0);
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

		// Token: 0x06001610 RID: 5648 RVA: 0x000EC1F8 File Offset: 0x000EA3F8
		public static OracleDecimal Max(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = ((OracleDecimal.Compare(value1, value2) >= 0) ? value1 : value2);
				}
				else
				{
					result = OracleDecimal.NaN;
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

		// Token: 0x06001611 RID: 5649 RVA: 0x000EC290 File Offset: 0x000EA490
		public static OracleDecimal Min(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = ((OracleDecimal.Compare(value1, value2) <= 0) ? value1 : value2);
				}
				else if (value1.m_numberType == 5)
				{
					result = value2;
				}
				else
				{
					result = value1;
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

		// Token: 0x06001612 RID: 5650 RVA: 0x000EC334 File Offset: 0x000EA534
		public static bool operator ==(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (OracleDecimal.Compare(value1, value2) == 0);
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

		// Token: 0x06001613 RID: 5651 RVA: 0x000EC390 File Offset: 0x000EA590
		public static bool operator >(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = (OracleDecimal.Compare(value1, value2) > 0);
				}
				else if (value1.m_numberType == 5 && value2.m_numberType != 5)
				{
					result = true;
				}
				else
				{
					result = false;
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

		// Token: 0x06001614 RID: 5652 RVA: 0x000EC41C File Offset: 0x000EA61C
		public static bool operator >=(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = (OracleDecimal.Compare(value1, value2) >= 0);
				}
				else if (value1.m_numberType != 5 && value2.m_numberType == 5)
				{
					result = false;
				}
				else
				{
					result = true;
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

		// Token: 0x06001615 RID: 5653 RVA: 0x000EC4AC File Offset: 0x000EA6AC
		public static bool operator <(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = (OracleDecimal.Compare(value1, value2) < 0);
				}
				else if (value1.m_numberType != 5 && value2.m_numberType == 5)
				{
					result = true;
				}
				else
				{
					result = false;
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

		// Token: 0x06001616 RID: 5654 RVA: 0x000EC538 File Offset: 0x000EA738
		public static bool operator <=(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (value1.m_numberType != 5 && value2.m_numberType != 5)
				{
					result = (OracleDecimal.Compare(value1, value2) <= 0);
				}
				else if (value1.m_numberType == 5 && value2.m_numberType != 5)
				{
					result = false;
				}
				else
				{
					result = true;
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

		// Token: 0x06001617 RID: 5655 RVA: 0x000EC5C8 File Offset: 0x000EA7C8
		public static bool operator !=(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (OracleDecimal.Compare(value1, value2) != 0);
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

		// Token: 0x06001618 RID: 5656 RVA: 0x000EC628 File Offset: 0x000EA828
		public static OracleDecimal operator +(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = OracleDecimal.Add(value1, value2);
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

		// Token: 0x06001619 RID: 5657 RVA: 0x000EC6A0 File Offset: 0x000EA8A0
		public static OracleDecimal operator -(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = OracleDecimal.Subtract(value1, value2);
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

		// Token: 0x0600161A RID: 5658 RVA: 0x000EC718 File Offset: 0x000EA918
		public static OracleDecimal operator -(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = OracleDecimal.Negate(value1);
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

		// Token: 0x0600161B RID: 5659 RVA: 0x000EC78C File Offset: 0x000EA98C
		public static OracleDecimal operator *(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = OracleDecimal.Multiply(value1, value2);
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

		// Token: 0x0600161C RID: 5660 RVA: 0x000EC804 File Offset: 0x000EAA04
		public static OracleDecimal operator /(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = OracleDecimal.Divide(value1, value2);
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

		// Token: 0x0600161D RID: 5661 RVA: 0x000EC87C File Offset: 0x000EAA7C
		public static OracleDecimal operator %(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = OracleDecimal.Mod(value1, value2);
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

		// Token: 0x0600161E RID: 5662 RVA: 0x000EC8F4 File Offset: 0x000EAAF4
		public static explicit operator OracleDecimal(string numStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = new OracleDecimal(numStr);
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

		// Token: 0x0600161F RID: 5663 RVA: 0x000EC968 File Offset: 0x000EAB68
		public static explicit operator byte(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			byte result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (value1.m_numberType == 5 || value1.m_numberType == 4 || value1.m_numberType == 3)
				{
					throw new OverflowException();
				}
				result = Convert.ToByte(OracleNumberCore.lnxsni(value1.m_byteRepresentation));
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

		// Token: 0x06001620 RID: 5664 RVA: 0x000ECA1C File Offset: 0x000EAC1C
		public static explicit operator short(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			short result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (value1.m_numberType == 5 || value1.m_numberType == 4 || value1.m_numberType == 3)
				{
					throw new OverflowException();
				}
				result = Convert.ToInt16(OracleNumberCore.lnxsni(value1.m_byteRepresentation));
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

		// Token: 0x06001621 RID: 5665 RVA: 0x000ECAD0 File Offset: 0x000EACD0
		public static explicit operator int(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (value1.m_numberType == 5 || value1.m_numberType == 4 || value1.m_numberType == 3)
				{
					throw new OverflowException();
				}
				result = Convert.ToInt32(OracleNumberCore.lnxsni(value1.m_byteRepresentation));
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

		// Token: 0x06001622 RID: 5666 RVA: 0x000ECB84 File Offset: 0x000EAD84
		public static explicit operator long(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (value1.m_numberType == 5 || value1.m_numberType == 4 || value1.m_numberType == 3)
				{
					throw new OverflowException();
				}
				result = OracleNumberCore.lnxsni(value1.m_byteRepresentation);
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

		// Token: 0x06001623 RID: 5667 RVA: 0x000ECC34 File Offset: 0x000EAE34
		public static explicit operator float(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			float result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = Convert.ToSingle(OracleNumberCore.lnxnur(value1.m_byteRepresentation));
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

		// Token: 0x06001624 RID: 5668 RVA: 0x000ECCC4 File Offset: 0x000EAEC4
		public static explicit operator double(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			double result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleNumberCore.lnxnur(value1.m_byteRepresentation);
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

		// Token: 0x06001625 RID: 5669 RVA: 0x000ECD50 File Offset: 0x000EAF50
		public static explicit operator decimal(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			decimal @decimal;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (value1.m_numberType == 5 || value1.m_numberType == 4 || value1.m_numberType == 3)
				{
					throw new OverflowException();
				}
				@decimal = DecimalConv.GetDecimal(value1.m_byteRepresentation, 0, value1.m_byteRepresentation.Length);
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
			return @decimal;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000ECE08 File Offset: 0x000EB008
		public static explicit operator OracleDecimal(double value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = new OracleDecimal(value1);
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

		// Token: 0x06001627 RID: 5671 RVA: 0x000ECE7C File Offset: 0x000EB07C
		public static implicit operator OracleDecimal(int value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = new OracleDecimal(value1);
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

		// Token: 0x06001628 RID: 5672 RVA: 0x000ECEF0 File Offset: 0x000EB0F0
		public static implicit operator OracleDecimal(long value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = new OracleDecimal(value1);
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

		// Token: 0x06001629 RID: 5673 RVA: 0x000ECF64 File Offset: 0x000EB164
		public static implicit operator OracleDecimal(decimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = new OracleDecimal(value1);
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

		// Token: 0x0600162A RID: 5674 RVA: 0x000ECFD8 File Offset: 0x000EB1D8
		public static OracleDecimal Abs(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					byte[] numBytes = OracleNumberCore.lnxabs(value1.m_byteRepresentation);
					result = new OracleDecimal(numBytes, false);
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600162B RID: 5675 RVA: 0x000ED06C File Offset: 0x000EB26C
		public static OracleDecimal Add(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull && value2.m_bNotNull)
				{
					if (value1.m_numberType != 5 && value2.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxadd(value1.m_byteRepresentation, value2.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
					if (compareNullEnum == CompareNullEnum.BothNull)
					{
						result = OracleDecimal.Null;
					}
					else if (compareNullEnum == CompareNullEnum.FirstNullOnly)
					{
						result = new OracleDecimal(value2.m_byteRepresentation, false);
					}
					else
					{
						result = new OracleDecimal(value1.m_byteRepresentation, false);
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

		// Token: 0x0600162C RID: 5676 RVA: 0x000ED16C File Offset: 0x000EB36C
		public static OracleDecimal AdjustScale(OracleDecimal value1, int digits, bool fRound)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (fRound)
				{
					result = OracleDecimal.Round(value1, digits);
				}
				else
				{
					result = OracleDecimal.Truncate(value1, digits);
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

		// Token: 0x0600162D RID: 5677 RVA: 0x000ED1F0 File Offset: 0x000EB3F0
		public static OracleDecimal Ceiling(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					byte[] numBytes = OracleNumberCore.lnxceil(value1.m_byteRepresentation);
					result = new OracleDecimal(numBytes, false);
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600162E RID: 5678 RVA: 0x000ED284 File Offset: 0x000EB484
		public static OracleDecimal ConvertToPrecScale(OracleDecimal value1, int precision, int scale)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5 && value1.m_numberType != 4 && value1.m_numberType != 3)
					{
						if (scale > (int)OracleDecimal.MaxScale || scale < OracleDecimal.MinScale)
						{
							throw new OracleTypeException(ResourceStringConstants.TYP_ERR_INVALID_SCALE, new object[0]);
						}
						if (precision > (int)OracleDecimal.MaxPrecision || precision < 1)
						{
							throw new OracleTypeException(ResourceStringConstants.TYP_ERR_INVALID_PREC, new object[0]);
						}
						OracleDecimal oracleDecimal = OracleDecimal.Round(value1, scale);
						string text = oracleDecimal.ToString();
						string text2 = text.TrimStart(new char[]
						{
							'-'
						});
						text2 = text.TrimStart(new char[]
						{
							'0',
							'.'
						});
						text2 = text2.TrimEnd(new char[]
						{
							'0'
						});
						int num = 1;
						int num2 = text2.IndexOf(".");
						if (num2 < 0)
						{
							num = 0;
						}
						if (precision + num < text2.Length)
						{
							throw new OracleTruncateException();
						}
						result = oracleDecimal;
					}
					else if (value1.m_numberType == 4)
					{
						result = OracleDecimal.NegativeInfinity;
					}
					else if (value1.m_numberType == 3)
					{
						result = OracleDecimal.PositiveInfinity;
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600162F RID: 5679 RVA: 0x000ED440 File Offset: 0x000EB640
		public static OracleDecimal Divide(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull && value2.m_bNotNull)
				{
					if (value1.m_bZero && value2.m_bZero)
					{
						result = OracleDecimal.NaN;
					}
					else if (value1.IsInfinity && value2.IsInfinity)
					{
						result = OracleDecimal.NaN;
					}
					else if (value1.m_numberType != 5 && value2.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxdiv(value1.m_byteRepresentation, value2.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001630 RID: 5680 RVA: 0x000ED538 File Offset: 0x000EB738
		public static OracleDecimal Floor(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					byte[] numBytes = OracleNumberCore.lnxflo(value1.m_byteRepresentation);
					result = new OracleDecimal(numBytes, false);
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001631 RID: 5681 RVA: 0x000ED5CC File Offset: 0x000EB7CC
		public static OracleDecimal Mod(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull && value2.m_bNotNull)
				{
					if (value1.m_numberType != 5 && value2.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxmod(value1.m_byteRepresentation, value2.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001632 RID: 5682 RVA: 0x000ED68C File Offset: 0x000EB88C
		public static OracleDecimal Multiply(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull && value2.m_bNotNull)
				{
					if ((value1.IsInfinity && value2.m_bZero) || (value1.m_bZero && value2.IsInfinity))
					{
						result = OracleDecimal.NaN;
					}
					else if (value1.m_numberType != 5 && value2.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxmul(value1.m_byteRepresentation, value2.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001633 RID: 5683 RVA: 0x000ED778 File Offset: 0x000EB978
		public static OracleDecimal Negate(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxneg(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001634 RID: 5684 RVA: 0x000ED820 File Offset: 0x000EBA20
		public static OracleDecimal Parse(string numStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = new OracleDecimal(numStr);
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

		// Token: 0x06001635 RID: 5685 RVA: 0x000ED894 File Offset: 0x000EBA94
		public static OracleDecimal SetPrecision(OracleDecimal value1, int precision)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5)
					{
						if (precision > (int)OracleDecimal.MaxPrecision || precision < 1)
						{
							throw new OracleTypeException(ResourceStringConstants.TYP_ERR_INVALID_PREC, new object[0]);
						}
						byte[] n = OracleNumberCore.lnxfpr(value1.m_byteRepresentation, precision);
						byte[] numBytes = OracleNumberCore.lnxrou(n, precision);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001636 RID: 5686 RVA: 0x000ED960 File Offset: 0x000EBB60
		internal static OracleDecimal SetPrecisionNoRound(OracleDecimal value1, int precision)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5)
					{
						if (precision > (int)OracleDecimal.MaxPrecision || precision < 1)
						{
							throw new OracleTypeException(ResourceStringConstants.TYP_ERR_INVALID_PREC, new object[0]);
						}
						byte[] numBytes = OracleNumberCore.lnxfpr(value1.m_byteRepresentation, precision);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001637 RID: 5687 RVA: 0x000EDA24 File Offset: 0x000EBC24
		public static OracleDecimal Round(OracleDecimal value1, int decplace)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					byte[] numBytes = OracleNumberCore.lnxrou(value1.m_byteRepresentation, decplace);
					result = new OracleDecimal(numBytes, false);
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001638 RID: 5688 RVA: 0x000EDAB8 File Offset: 0x000EBCB8
		public static OracleDecimal Shift(OracleDecimal value1, int decplace)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxshift(value1.m_byteRepresentation, decplace);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001639 RID: 5689 RVA: 0x000EDB60 File Offset: 0x000EBD60
		public static int Sign(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (OracleNumberCore.IsZero(value1.m_byteRepresentation))
				{
					result = 0;
				}
				else
				{
					result = (OracleNumberCore.IsPositive(value1.m_byteRepresentation) ? 1 : -1);
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

		// Token: 0x0600163A RID: 5690 RVA: 0x000EDC04 File Offset: 0x000EBE04
		public static OracleDecimal Sqrt(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5 && value1.m_numberType != 4 && value1.m_numberType != 3)
					{
						if (!value1.m_bPositive && !value1.m_bZero)
						{
							throw new ArgumentOutOfRangeException("value1");
						}
						byte[] numBytes = OracleNumberCore.lnxsqr(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600163B RID: 5691 RVA: 0x000EDCDC File Offset: 0x000EBEDC
		public static OracleDecimal Subtract(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull && value2.m_bNotNull)
				{
					if (value1.IsPositiveInfinity && value2.IsPositiveInfinity)
					{
						result = OracleDecimal.NaN;
					}
					else if (value1.m_numberType != 5 && value2.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxsub(value1.m_byteRepresentation, value2.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
					if (compareNullEnum == CompareNullEnum.BothNull)
					{
						result = OracleDecimal.Null;
					}
					else if (compareNullEnum == CompareNullEnum.FirstNullOnly)
					{
						result = OracleDecimal.Negate(value2);
					}
					else
					{
						result = new OracleDecimal(value1.m_byteRepresentation, false);
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

		// Token: 0x0600163C RID: 5692 RVA: 0x000EDDF4 File Offset: 0x000EBFF4
		public static OracleDecimal Truncate(OracleDecimal value1, int position)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					byte[] numBytes = OracleNumberCore.lnxtru(value1.m_byteRepresentation, position);
					result = new OracleDecimal(numBytes, false);
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600163D RID: 5693 RVA: 0x000EDE88 File Offset: 0x000EC088
		public static OracleDecimal Exp(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxexp(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600163E RID: 5694 RVA: 0x000EDF30 File Offset: 0x000EC130
		public static OracleDecimal Pow(OracleDecimal value1, int power)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxpow(value1.m_byteRepresentation, power);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600163F RID: 5695 RVA: 0x000EDFD8 File Offset: 0x000EC1D8
		public static OracleDecimal Pow(OracleDecimal value1, OracleDecimal power)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull && power.m_bNotNull)
				{
					if (value1.IsZero && !power.IsPositive && !power.IsZero)
					{
						result = OracleDecimal.PositiveInfinity;
					}
					else if (value1.m_numberType != 5 && power.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxbex(value1.m_byteRepresentation, power.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001640 RID: 5696 RVA: 0x000EE0BC File Offset: 0x000EC2BC
		public static OracleDecimal Log(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType == 4 || value1.m_numberType == 5)
					{
						result = OracleDecimal.NaN;
					}
					else if (value1.m_numberType == 3)
					{
						result = OracleDecimal.PositiveInfinity;
					}
					else
					{
						if (!value1.IsPositive && !value1.IsZero)
						{
							throw new ArgumentOutOfRangeException("value1");
						}
						byte[] numBytes = OracleNumberCore.lnxln(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001641 RID: 5697 RVA: 0x000EE19C File Offset: 0x000EC39C
		public static OracleDecimal Log(OracleDecimal value1, int logBase)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType == 4 || value1.m_numberType == 5)
					{
						result = OracleDecimal.NaN;
					}
					else if (value1.m_numberType == 3)
					{
						result = OracleDecimal.PositiveInfinity;
					}
					else
					{
						if (!value1.IsPositive && !value1.IsZero)
						{
							throw new ArgumentOutOfRangeException("value1");
						}
						if (logBase <= 0)
						{
							throw new ArgumentOutOfRangeException("logBase");
						}
						if (value1.IsZero && logBase == 0)
						{
							result = OracleDecimal.NaN;
						}
						else if (value1.IsPositive && logBase == 0)
						{
							result = new OracleDecimal(0);
						}
						else
						{
							OracleDecimal oracleDecimal = logBase;
							byte[] numBytes = OracleNumberCore.lnxlog(value1.m_byteRepresentation, oracleDecimal.m_byteRepresentation);
							result = new OracleDecimal(numBytes, false);
						}
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001642 RID: 5698 RVA: 0x000EE2CC File Offset: 0x000EC4CC
		public static OracleDecimal Log(OracleDecimal value1, OracleDecimal logBase)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull && logBase.m_bNotNull)
				{
					if (value1.m_numberType == 4 || value1.m_numberType == 5 || logBase.m_numberType == 4 || logBase.m_numberType == 5)
					{
						result = OracleDecimal.NaN;
					}
					else if (value1.m_numberType == 3)
					{
						result = OracleDecimal.PositiveInfinity;
					}
					else if (logBase.m_numberType == 3)
					{
						result = OracleDecimal.Zero;
					}
					else
					{
						if (!value1.IsPositive && !value1.IsZero)
						{
							throw new ArgumentOutOfRangeException("value1");
						}
						if (!logBase.IsPositive && !logBase.IsZero)
						{
							throw new ArgumentOutOfRangeException("logBase");
						}
						if (value1.IsZero && logBase.IsZero)
						{
							result = OracleDecimal.NaN;
						}
						else if (value1.IsPositive && logBase.IsZero)
						{
							result = new OracleDecimal(0);
						}
						else
						{
							byte[] numBytes = OracleNumberCore.lnxlog(value1.m_byteRepresentation, logBase.m_byteRepresentation);
							result = new OracleDecimal(numBytes, false);
						}
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001643 RID: 5699 RVA: 0x000EE45C File Offset: 0x000EC65C
		public static OracleDecimal Acos(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5 && value1.m_numberType != 4 && value1.m_numberType != 3)
					{
						byte[] numBytes = OracleNumberCore.lnxacos(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001644 RID: 5700 RVA: 0x000EE518 File Offset: 0x000EC718
		public static OracleDecimal Asin(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5 && value1.m_numberType != 4 && value1.m_numberType != 3)
					{
						byte[] numBytes = OracleNumberCore.lnxasin(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001645 RID: 5701 RVA: 0x000EE5D4 File Offset: 0x000EC7D4
		public static OracleDecimal Atan(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5)
					{
						byte[] numBytes = OracleNumberCore.lnxatan(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001646 RID: 5702 RVA: 0x000EE67C File Offset: 0x000EC87C
		public static OracleDecimal Atan2(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull && value2.m_bNotNull)
				{
					if (value1.m_numberType == 5 || value2.m_numberType == 5)
					{
						result = OracleDecimal.NaN;
					}
					else
					{
						byte[] numBytes = OracleNumberCore.lnxatan2(value1.m_byteRepresentation, value2.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001647 RID: 5703 RVA: 0x000EE73C File Offset: 0x000EC93C
		public static OracleDecimal Cos(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5 && value1.m_numberType != 4 && value1.m_numberType != 3)
					{
						byte[] numBytes = OracleNumberCore.lnxcos(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001648 RID: 5704 RVA: 0x000EE7F8 File Offset: 0x000EC9F8
		public static OracleDecimal Sin(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5 && value1.m_numberType != 4 && value1.m_numberType != 3)
					{
						byte[] numBytes = OracleNumberCore.lnxsin(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x06001649 RID: 5705 RVA: 0x000EE8B4 File Offset: 0x000ECAB4
		public static OracleDecimal Tan(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType != 5 && value1.m_numberType != 4 && value1.m_numberType != 3)
					{
						byte[] numBytes = OracleNumberCore.lnxtan(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
					else
					{
						result = OracleDecimal.NaN;
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600164A RID: 5706 RVA: 0x000EE970 File Offset: 0x000ECB70
		public static OracleDecimal Cosh(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType == 4 || value1.m_numberType == 3)
					{
						result = OracleDecimal.PositiveInfinity;
					}
					else if (value1.m_numberType == 5)
					{
						result = OracleDecimal.NaN;
					}
					else
					{
						byte[] numBytes = OracleNumberCore.lnxcsh(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600164B RID: 5707 RVA: 0x000EEA34 File Offset: 0x000ECC34
		public static OracleDecimal Sinh(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType == 4)
					{
						result = OracleDecimal.NegativeInfinity;
					}
					else if (value1.m_numberType == 3)
					{
						result = OracleDecimal.PositiveInfinity;
					}
					else if (value1.m_numberType == 5)
					{
						result = OracleDecimal.NaN;
					}
					else
					{
						byte[] numBytes = OracleNumberCore.lnxsnh(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x0600164C RID: 5708 RVA: 0x000EEB00 File Offset: 0x000ECD00
		public static OracleDecimal Tanh(OracleDecimal value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (value1.m_bNotNull)
				{
					if (value1.m_numberType == 5)
					{
						result = OracleDecimal.NaN;
					}
					else if (value1.IsPositiveInfinity)
					{
						result = new OracleDecimal(1);
					}
					else if (value1.IsNegativeInfinity)
					{
						result = new OracleDecimal(-1);
					}
					else
					{
						byte[] numBytes = OracleNumberCore.lnxtnh(value1.m_byteRepresentation);
						result = new OracleDecimal(numBytes, false);
					}
				}
				else
				{
					result = OracleDecimal.Null;
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

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x000EEBCC File Offset: 0x000ECDCC
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x000EEBD8 File Offset: 0x000ECDD8
		public byte[] BinData
		{
			get
			{
				byte[] array = new byte[22];
				array[0] = (byte)this.m_byteRepresentation.Length;
				Array.Copy(this.m_byteRepresentation, 0, array, 1, this.m_byteRepresentation.Length);
				return array;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x000EEC10 File Offset: 0x000ECE10
		public bool IsInt
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleNumberCore.IsInt(this.m_byteRepresentation);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x000EEC2C File Offset: 0x000ECE2C
		public bool IsPositive
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleNumberCore.IsPositive(this.m_byteRepresentation);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000EEC48 File Offset: 0x000ECE48
		public bool IsZero
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleNumberCore.IsZero(this.m_byteRepresentation);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x000EEC64 File Offset: 0x000ECE64
		internal bool IsInfinity
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleNumberCore.IsInfinity(this.m_byteRepresentation);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x000EEC80 File Offset: 0x000ECE80
		internal bool IsPositiveInfinity
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleNumberCore.IsPositiveInfinity(this.m_byteRepresentation);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x000EEC9C File Offset: 0x000ECE9C
		internal bool IsNegativeInfinity
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleNumberCore.IsNegativeInfinity(this.m_byteRepresentation);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x000EECB8 File Offset: 0x000ECEB8
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x000EECD0 File Offset: 0x000ECED0
		public string Format
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_format;
				}
				throw new OracleNullValueException();
			}
			set
			{
				if (this.m_bNotNull)
				{
					this.m_format = value;
					return;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x000EECE8 File Offset: 0x000ECEE8
		public decimal Value
		{
			get
			{
				if (this == OracleDecimal.Pi)
				{
					return OracleDecimal.Pivalue;
				}
				if (this.m_bNotNull)
				{
					return DecimalConv.GetDecimal(this.m_byteRepresentation, 0, this.m_byteRepresentation.Length);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000EED24 File Offset: 0x000ECF24
		public byte ToByte()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			byte result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_numberType == 5 || this.m_numberType == 4 || this.m_numberType == 3)
				{
					throw new OverflowException();
				}
				result = Convert.ToByte(OracleNumberCore.lnxsni(this.m_byteRepresentation));
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

		// Token: 0x06001659 RID: 5721 RVA: 0x000EEDD4 File Offset: 0x000ECFD4
		public short ToInt16()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			short result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_numberType == 5 || this.m_numberType == 4 || this.m_numberType == 3)
				{
					throw new OverflowException();
				}
				result = Convert.ToInt16(OracleNumberCore.lnxsni(this.m_byteRepresentation));
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

		// Token: 0x0600165A RID: 5722 RVA: 0x000EEE84 File Offset: 0x000ED084
		public int ToInt32()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_numberType == 5 || this.m_numberType == 4 || this.m_numberType == 3)
				{
					throw new OverflowException();
				}
				result = Convert.ToInt32(OracleNumberCore.lnxsni(this.m_byteRepresentation));
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

		// Token: 0x0600165B RID: 5723 RVA: 0x000EEF34 File Offset: 0x000ED134
		public long ToInt64()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this.m_numberType == 5 || this.m_numberType == 4 || this.m_numberType == 3)
				{
					throw new OverflowException();
				}
				result = OracleNumberCore.lnxsni(this.m_byteRepresentation);
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

		// Token: 0x0600165C RID: 5724 RVA: 0x000EEFDC File Offset: 0x000ED1DC
		public float ToSingle()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			float result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = Convert.ToSingle(OracleNumberCore.lnxnur(this.m_byteRepresentation));
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

		// Token: 0x0600165D RID: 5725 RVA: 0x000EF068 File Offset: 0x000ED268
		public double ToDouble()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			double result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleNumberCore.lnxnur(this.m_byteRepresentation);
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

		// Token: 0x0600165E RID: 5726 RVA: 0x000EF0F0 File Offset: 0x000ED2F0
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
				if (obj.GetType() != typeof(OracleDecimal))
				{
					throw new ArgumentException("obj");
				}
				result = OracleDecimal.Compare(this, (OracleDecimal)obj);
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

		// Token: 0x0600165F RID: 5727 RVA: 0x000EF1A0 File Offset: 0x000ED3A0
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null || obj.GetType() != typeof(OracleDecimal))
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

		// Token: 0x06001660 RID: 5728 RVA: 0x000EF238 File Offset: 0x000ED438
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
					result = this.m_byteRepresentation.GetHashCode();
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

		// Token: 0x06001661 RID: 5729 RVA: 0x000EF2A4 File Offset: 0x000ED4A4
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
					if (this.IsPositiveInfinity)
					{
						result = "~";
					}
					else if (this.IsNegativeInfinity)
					{
						result = "-~";
					}
					else if (this.IsZero)
					{
						result = "0";
					}
					else if (this.m_numberType == 5)
					{
						result = "NaN";
					}
					else
					{
						result = DecimalConv.ToString(this.m_byteRepresentation);
					}
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

		// Token: 0x06001662 RID: 5730 RVA: 0x000EF370 File Offset: 0x000ED570
		internal static OracleDecimal SetPi()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				result = new OracleDecimal(OracleNumberCore.PI, false);
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

		// Token: 0x06001663 RID: 5731 RVA: 0x000EF3EC File Offset: 0x000ED5EC
		internal static OracleDecimal GetMaxValue()
		{
			byte[] numBytes = new byte[]
			{
				byte.MaxValue,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100,
				100
			};
			return new OracleDecimal(numBytes, false);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000EF414 File Offset: 0x000ED614
		internal static OracleDecimal GetMinValue()
		{
			byte[] numBytes = new byte[]
			{
				0,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				102
			};
			return new OracleDecimal(numBytes, false);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000EF43C File Offset: 0x000ED63C
		internal static OracleDecimal GetPosInfinity()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				byte[] positiveInfinityByteRep = OracleNumberCore.GetPositiveInfinityByteRep();
				result = new OracleDecimal(positiveInfinityByteRep, false);
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

		// Token: 0x06001666 RID: 5734 RVA: 0x000EF4B8 File Offset: 0x000ED6B8
		internal static OracleDecimal GetNegInfinity()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				byte[] negativeInfinityByteRep = OracleNumberCore.GetNegativeInfinityByteRep();
				result = new OracleDecimal(negativeInfinityByteRep, false);
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

		// Token: 0x06001667 RID: 5735 RVA: 0x000EF534 File Offset: 0x000ED734
		internal static OracleDecimal GetNaN()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				byte[] nanrepd = OracleNumberCore.NANREPD;
				result = new OracleDecimal(nanrepd, false);
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

		// Token: 0x06001668 RID: 5736 RVA: 0x000EF5B0 File Offset: 0x000ED7B0
		internal static int Compare(OracleDecimal value1, OracleDecimal value2)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
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
					result = OracleNumberCore.compareBytes(value1.m_byteRepresentation, value2.m_byteRepresentation);
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

		// Token: 0x06001669 RID: 5737 RVA: 0x000EF664 File Offset: 0x000ED864
		internal static void ToBytes(string numStr, out byte[] decimalByteRep, out int numberType, out bool bPositive, out bool bZero)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (numStr == null)
				{
					throw new ArgumentNullException();
				}
				if (numStr == "~" || string.Compare(numStr, "Inf", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(numStr, "+Inf", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(numStr, "Infinity", StringComparison.InvariantCultureIgnoreCase) == 0 || numStr == "∞")
				{
					decimalByteRep = OracleNumberCore.GetPositiveInfinityByteRep();
					numberType = 3;
					bPositive = true;
					bZero = false;
				}
				else if (numStr == "-~" || string.Compare(numStr, "-Inf", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(numStr, "-Infinity", StringComparison.InvariantCultureIgnoreCase) == 0 || numStr == "-∞")
				{
					decimalByteRep = OracleNumberCore.GetNegativeInfinityByteRep();
					numberType = 4;
					bPositive = false;
					bZero = false;
				}
				else if (string.Compare(numStr, "NaN", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					decimalByteRep = OracleNumberCore.NANREPD;
					numberType = 5;
					bPositive = true;
					bZero = false;
				}
				else
				{
					bool flag;
					bool flag2;
					decimalByteRep = DecimalConv.FromString(numStr, out bPositive, out bZero, out flag, out flag2);
					if (decimalByteRep == null)
					{
						throw new FormatException();
					}
					numberType = (flag2 ? 2 : 1);
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
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x000EF7DC File Offset: 0x000ED9DC
		internal byte[] InternalByteRepresentation
		{
			get
			{
				return this.m_byteRepresentation;
			}
		}

		// Token: 0x040019D4 RID: 6612
		internal const byte MaxArrSize = 22;

		// Token: 0x040019D5 RID: 6613
		internal const byte MinPrecision = 1;

		// Token: 0x040019D6 RID: 6614
		internal const int ScaleFactor = 3;

		// Token: 0x040019D7 RID: 6615
		internal const int NumberTypeIndex = 2;

		// Token: 0x040019D8 RID: 6616
		private byte[] m_byteRepresentation;

		// Token: 0x040019D9 RID: 6617
		private bool m_bPositive;

		// Token: 0x040019DA RID: 6618
		private bool m_bZero;

		// Token: 0x040019DB RID: 6619
		private string m_format;

		// Token: 0x040019DC RID: 6620
		private bool m_bNotNull;

		// Token: 0x040019DD RID: 6621
		private int m_numberType;

		// Token: 0x040019DE RID: 6622
		internal static readonly OracleDecimal PositiveInfinity = OracleDecimal.GetPosInfinity();

		// Token: 0x040019DF RID: 6623
		internal static readonly OracleDecimal NegativeInfinity = OracleDecimal.GetNegInfinity();

		// Token: 0x040019E0 RID: 6624
		internal static readonly OracleDecimal NaN = OracleDecimal.GetNaN();

		// Token: 0x040019E1 RID: 6625
		public static readonly byte MaxPrecision = 38;

		// Token: 0x040019E2 RID: 6626
		public static readonly byte MaxScale = 127;

		// Token: 0x040019E3 RID: 6627
		public static readonly OracleDecimal MaxValue = OracleDecimal.GetMaxValue();

		// Token: 0x040019E4 RID: 6628
		public static readonly int MinScale = -84;

		// Token: 0x040019E5 RID: 6629
		public static readonly OracleDecimal MinValue = OracleDecimal.GetMinValue();

		// Token: 0x040019E6 RID: 6630
		public static readonly OracleDecimal NegativeOne = new OracleDecimal(-1);

		// Token: 0x040019E7 RID: 6631
		public static readonly OracleDecimal Null;

		// Token: 0x040019E8 RID: 6632
		public static readonly OracleDecimal One = new OracleDecimal(1);

		// Token: 0x040019E9 RID: 6633
		private static readonly decimal Pivalue = 3.1415926535897932384626433832m;

		// Token: 0x040019EA RID: 6634
		public static readonly OracleDecimal Pi = OracleDecimal.SetPi();

		// Token: 0x040019EB RID: 6635
		public static readonly OracleDecimal Zero = new OracleDecimal(0);
	}
}
