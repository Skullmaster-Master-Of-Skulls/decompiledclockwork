using System;
using System.Xml;
using Oracle.ManagedDataAccess.Types;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001C1 RID: 449
	internal class OraXmlTypeData
	{
		// Token: 0x06001153 RID: 4435 RVA: 0x000BF624 File Offset: 0x000BD824
		internal OraXmlTypeData()
		{
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000BF62C File Offset: 0x000BD82C
		internal OraXmlTypeData(TypeOfXmlData typeOfXmlData, object value)
		{
			this.m_typeOfXmlData = typeOfXmlData;
			TypeOfXmlData typeOfXmlData2 = this.m_typeOfXmlData;
			if (typeOfXmlData2 <= TypeOfXmlData.Chars)
			{
				switch (typeOfXmlData2)
				{
				case TypeOfXmlData.String:
					this.m_xmlStr = (string)value;
					this.m_typeOfXmlData = TypeOfXmlData.String;
					return;
				case (TypeOfXmlData)3:
					break;
				case TypeOfXmlData.Clob:
					this.m_xmlClob = (OracleClob)((OracleClob)value).Clone();
					this.m_typeOfXmlData = TypeOfXmlData.Clob;
					return;
				default:
					if (typeOfXmlData2 != TypeOfXmlData.Chars)
					{
						return;
					}
					this.m_xmlChars = (char[])value;
					this.m_typeOfXmlData = TypeOfXmlData.Chars;
					return;
				}
			}
			else
			{
				if (typeOfXmlData2 == TypeOfXmlData.XmlDoc)
				{
					this.m_xmlStr = ((XmlDocument)value).OuterXml;
					this.m_typeOfXmlData = TypeOfXmlData.String;
					return;
				}
				if (typeOfXmlData2 == TypeOfXmlData.BlobWithText)
				{
					this.m_xmlBlobText = ((OraXmlBlobWithText)value).Clone();
					this.m_typeOfXmlData = TypeOfXmlData.BlobWithText;
					return;
				}
				if (typeOfXmlData2 != TypeOfXmlData.BlobCSX)
				{
					return;
				}
				this.m_xmlBlobCSX = (OracleBlob)((OracleBlob)value).Clone();
				this.m_typeOfXmlData = TypeOfXmlData.BlobCSX;
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000BF71C File Offset: 0x000BD91C
		internal void Dispose()
		{
			TypeOfXmlData typeOfXmlData = this.m_typeOfXmlData;
			if (typeOfXmlData <= TypeOfXmlData.Chars)
			{
				switch (typeOfXmlData)
				{
				case TypeOfXmlData.String:
					break;
				case (TypeOfXmlData)3:
				case (TypeOfXmlData)5:
					goto IL_E7;
				case TypeOfXmlData.Clob:
				case TypeOfXmlData.ClobAndString:
					this.m_xmlClob.Dispose();
					this.m_xmlClob = null;
					this.m_xmlStr = null;
					goto IL_E7;
				default:
					if (typeOfXmlData != TypeOfXmlData.Chars)
					{
						goto IL_E7;
					}
					this.m_xmlChars = null;
					goto IL_E7;
				}
			}
			else
			{
				switch (typeOfXmlData)
				{
				case TypeOfXmlData.XmlDoc:
					this.m_xmlDocInternal = null;
					goto IL_E7;
				case (TypeOfXmlData)33:
					goto IL_E7;
				case TypeOfXmlData.StringAndXmlDoc:
					break;
				default:
					switch (typeOfXmlData)
					{
					case TypeOfXmlData.BlobWithText:
					case TypeOfXmlData.BlobWithTextAndString:
						this.m_xmlBlobText.Dispose();
						this.m_xmlBlobText = null;
						this.m_xmlStr = null;
						goto IL_E7;
					case (TypeOfXmlData)65:
						goto IL_E7;
					default:
						switch (typeOfXmlData)
						{
						case TypeOfXmlData.BlobCSX:
						case TypeOfXmlData.BlobCSXAndString:
							this.m_xmlBlobCSX.Dispose();
							this.m_xmlBlobCSX = null;
							this.m_xmlStr = null;
							goto IL_E7;
						case (TypeOfXmlData)129:
							goto IL_E7;
						default:
							goto IL_E7;
						}
						break;
					}
					break;
				}
			}
			this.m_xmlStr = null;
			this.m_xmlDocInternal = null;
			IL_E7:
			this.m_typeOfXmlData = TypeOfXmlData.NoData;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000BF818 File Offset: 0x000BDA18
		internal OraXmlTypeData Clone()
		{
			OraXmlTypeData oraXmlTypeData = new OraXmlTypeData();
			TypeOfXmlData typeOfXmlData = this.m_typeOfXmlData;
			if (typeOfXmlData <= TypeOfXmlData.Chars)
			{
				switch (typeOfXmlData)
				{
				case TypeOfXmlData.String:
					break;
				case (TypeOfXmlData)3:
				case (TypeOfXmlData)5:
					return oraXmlTypeData;
				case TypeOfXmlData.Clob:
					oraXmlTypeData.m_xmlClob = (OracleClob)this.m_xmlClob.Clone();
					oraXmlTypeData.m_typeOfXmlData = TypeOfXmlData.Clob;
					return oraXmlTypeData;
				case TypeOfXmlData.ClobAndString:
					oraXmlTypeData.m_xmlClob = (OracleClob)this.m_xmlClob.Clone();
					oraXmlTypeData.m_xmlStr = this.m_xmlStr;
					oraXmlTypeData.m_typeOfXmlData = TypeOfXmlData.ClobAndString;
					return oraXmlTypeData;
				default:
					if (typeOfXmlData != TypeOfXmlData.Chars)
					{
						return oraXmlTypeData;
					}
					oraXmlTypeData.m_xmlChars = this.m_xmlChars;
					oraXmlTypeData.m_typeOfXmlData = TypeOfXmlData.Chars;
					return oraXmlTypeData;
				}
			}
			else
			{
				switch (typeOfXmlData)
				{
				case TypeOfXmlData.XmlDoc:
					oraXmlTypeData.m_xmlStr = this.m_xmlDocInternal.OuterXml;
					oraXmlTypeData.m_typeOfXmlData = TypeOfXmlData.String;
					return oraXmlTypeData;
				case (TypeOfXmlData)33:
					return oraXmlTypeData;
				case TypeOfXmlData.StringAndXmlDoc:
					break;
				default:
					switch (typeOfXmlData)
					{
					case TypeOfXmlData.BlobWithText:
						oraXmlTypeData.m_xmlBlobText = this.m_xmlBlobText.Clone();
						oraXmlTypeData.m_typeOfXmlData = TypeOfXmlData.BlobWithText;
						return oraXmlTypeData;
					case (TypeOfXmlData)65:
						return oraXmlTypeData;
					case TypeOfXmlData.BlobWithTextAndString:
						oraXmlTypeData.m_xmlBlobText = this.m_xmlBlobText.Clone();
						oraXmlTypeData.m_xmlStr = this.m_xmlStr;
						oraXmlTypeData.m_typeOfXmlData = TypeOfXmlData.BlobWithTextAndString;
						return oraXmlTypeData;
					default:
						switch (typeOfXmlData)
						{
						case TypeOfXmlData.BlobCSX:
							oraXmlTypeData.m_xmlBlobCSX = (OracleBlob)this.m_xmlBlobCSX.Clone();
							oraXmlTypeData.m_typeOfXmlData = TypeOfXmlData.BlobCSX;
							return oraXmlTypeData;
						case (TypeOfXmlData)129:
							return oraXmlTypeData;
						case TypeOfXmlData.BlobCSXAndString:
							oraXmlTypeData.m_xmlBlobCSX = (OracleBlob)this.m_xmlBlobCSX.Clone();
							oraXmlTypeData.m_xmlStr = this.m_xmlStr;
							oraXmlTypeData.m_typeOfXmlData = TypeOfXmlData.BlobCSXAndString;
							return oraXmlTypeData;
						default:
							return oraXmlTypeData;
						}
						break;
					}
					break;
				}
			}
			oraXmlTypeData.m_xmlStr = this.m_xmlStr;
			oraXmlTypeData.m_typeOfXmlData = TypeOfXmlData.String;
			return oraXmlTypeData;
		}

		// Token: 0x040013A3 RID: 5027
		internal char[] m_xmlChars;

		// Token: 0x040013A4 RID: 5028
		internal string m_xmlStr;

		// Token: 0x040013A5 RID: 5029
		internal OracleClob m_xmlClob;

		// Token: 0x040013A6 RID: 5030
		internal XmlDocument m_xmlDocInternal;

		// Token: 0x040013A7 RID: 5031
		internal OraXmlBlobWithText m_xmlBlobText;

		// Token: 0x040013A8 RID: 5032
		internal int m_csid;

		// Token: 0x040013A9 RID: 5033
		internal byte[] m_xmlLobLocator;

		// Token: 0x040013AA RID: 5034
		internal OracleBlob m_xmlBlobCSX;

		// Token: 0x040013AB RID: 5035
		internal TypeOfXmlData m_typeOfXmlData;
	}
}
