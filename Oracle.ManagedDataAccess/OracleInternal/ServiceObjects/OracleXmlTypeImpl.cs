using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.BinXml;
using OracleInternal.Common;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001BC RID: 444
	internal class OracleXmlTypeImpl
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x000BE02C File Offset: 0x000BC22C
		internal static int GetKpsnpLen(OracleConnectionImpl connImpl)
		{
			if (!connImpl.IsServerUsingBigSCN)
			{
				return 24;
			}
			return 34;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000BE03C File Offset: 0x000BC23C
		internal OracleXmlTypeImpl()
		{
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000BE058 File Offset: 0x000BC258
		internal OracleXmlTypeImpl(OracleConnectionImpl connImpl)
		{
			this.m_kpsnpLen = OracleXmlTypeImpl.GetKpsnpLen(connImpl);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000BE080 File Offset: 0x000BC280
		internal OracleXmlTypeImpl(OracleConnectionImpl connImpl, TypeOfXmlType typeOfXmlType, TypeOfXmlData typeOfXmlData, object xmlValue) : this(connImpl)
		{
			this.m_connImpl = connImpl;
			this.m_typeOfXmlType = typeOfXmlType;
			this.m_xmlTypeData = new OraXmlTypeData(typeOfXmlData, xmlValue);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000BE0A8 File Offset: 0x000BC2A8
		internal OracleXmlTypeImpl(OracleConnectionImpl connImpl, TTCXmlTypeAccessor XmlTypeAccessor, DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex) : this(connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[0]);
			}
			try
			{
				this.m_connImpl = connImpl;
				OraXmlTypeHeader xmlTypeHeader = new OraXmlTypeHeader();
				XmlTypeAccessor.UnpickleXmlType(connImpl, dataUnmarshaller, currentRow, columnIndex, xmlTypeHeader, out this.m_xmlTypeData);
				this.PopulateXmlHeader(xmlTypeHeader);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777728, new string[0]);
				}
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000BE144 File Offset: 0x000BC344
		internal OracleXmlTypeImpl(OracleConnectionImpl connImpl, OraXmlTypeHeader xmlTypeHeader, OraXmlTypeData xmlTypeData) : this(connImpl)
		{
			try
			{
				this.Set(connImpl, xmlTypeHeader, xmlTypeData);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000BE184 File Offset: 0x000BC384
		internal void PopulateXmlHeader(OraXmlTypeHeader xmlTypeHeader)
		{
			this.m_typeOfXmlType = xmlTypeHeader.m_typeOfXmlType;
			this.m_xmlFlag = xmlTypeHeader.m_xmlFlag;
			if (xmlTypeHeader.HasSchema())
			{
				this.m_schemaID = xmlTypeHeader.m_schoid;
				this.m_schElem = xmlTypeHeader.m_schElem;
			}
			this.m_bGotSchemaInfo = true;
			this.m_bIsFragment = xmlTypeHeader.IsFragment();
			this.m_bGotFragmentProp = true;
			this.m_snapshot = xmlTypeHeader.m_snapshot;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000BE1F0 File Offset: 0x000BC3F0
		internal void Set(OracleConnectionImpl connImpl, OraXmlTypeHeader xmlTypeHeader, OraXmlTypeData xmlTypeData)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[0]);
			}
			try
			{
				this.m_connImpl = connImpl;
				this.m_xmlTypeData = xmlTypeData;
				this.PopulateXmlHeader(xmlTypeHeader);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777728, new string[0]);
				}
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000BE274 File Offset: 0x000BC474
		internal void Initialize(OracleConnection conn)
		{
			if (TypeOfXmlData.Clob == this.m_xmlTypeData.m_typeOfXmlData && this.m_xmlTypeData.m_xmlClob == null && this.m_xmlTypeData.m_xmlLobLocator != null)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[]
					{
						"CLOB_OBJECT_STREAM_RECEIVED"
					});
				}
				this.m_xmlTypeData.m_xmlClob = new OracleClob(conn, this.m_xmlTypeData.m_xmlLobLocator, false, false);
				this.m_xmlTypeData.m_xmlLobLocator = null;
				return;
			}
			if (TypeOfXmlData.BlobWithText == this.m_xmlTypeData.m_typeOfXmlData && this.m_xmlTypeData.m_xmlBlobText == null && this.m_xmlTypeData.m_xmlLobLocator != null)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[]
					{
						"BLOB_TEXT_STREAM_RECEIVED"
					});
				}
				this.m_xmlTypeData.m_xmlBlobText = new OraXmlBlobWithText(conn, this.m_xmlTypeData.m_xmlLobLocator, this.m_xmlTypeData.m_csid);
				this.m_xmlTypeData.m_xmlLobLocator = null;
				return;
			}
			if (TypeOfXmlData.BlobCSX == this.m_xmlTypeData.m_typeOfXmlData && this.m_xmlTypeData.m_xmlBlobCSX == null && this.m_xmlTypeData.m_xmlLobLocator != null)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[]
					{
						"BINARY_XML_ENCODED_STREAM_RECEIVED"
					});
				}
				this.m_xmlTypeData.m_xmlBlobCSX = new OracleBlob(conn, this.m_xmlTypeData.m_xmlLobLocator);
				this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.BlobCSX);
				this.m_xmlTypeData.m_xmlLobLocator = null;
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000BE40C File Offset: 0x000BC60C
		internal void Dispose()
		{
			this.m_schemaNSFromXmlDoc = null;
			this.m_schemaUrlFromXmlDoc = null;
			this.m_namespacesFromXmlDoc = null;
			this.m_schemaID = null;
			this.m_schElem = null;
			this.m_snapshot = null;
			this.m_xmlFlag = 0U;
			this.m_typeOfXmlType = TypeOfXmlType.Null;
			lock (this.m_syncLock)
			{
				this.m_xmlTypeData.Dispose();
			}
			this.m_connImpl = null;
			this.m_xmlTypeData = null;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000BE498 File Offset: 0x000BC698
		internal OracleXmlTypeImpl Clone()
		{
			OracleXmlTypeImpl oracleXmlTypeImpl = new OracleXmlTypeImpl();
			oracleXmlTypeImpl.m_typeOfXmlType = this.m_typeOfXmlType;
			lock (this.m_syncLock)
			{
				oracleXmlTypeImpl.m_xmlTypeData = this.m_xmlTypeData.Clone();
			}
			oracleXmlTypeImpl.m_schElem = this.m_schElem;
			oracleXmlTypeImpl.m_schemaID = this.m_schemaID;
			oracleXmlTypeImpl.m_snapshot = this.m_snapshot;
			oracleXmlTypeImpl.m_xmlFlag = this.m_xmlFlag;
			return oracleXmlTypeImpl;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000BE528 File Offset: 0x000BC728
		internal bool IsEmptyXmlTypeData()
		{
			lock (this.m_syncLock)
			{
				TypeOfXmlData typeOfXmlData = this.m_xmlTypeData.m_typeOfXmlData;
				if (typeOfXmlData <= TypeOfXmlData.Chars)
				{
					switch (typeOfXmlData)
					{
					case TypeOfXmlData.NoData:
						return true;
					case (TypeOfXmlData)1:
					case (TypeOfXmlData)3:
					case (TypeOfXmlData)5:
						goto IL_117;
					case TypeOfXmlData.String:
						break;
					case TypeOfXmlData.Clob:
					case TypeOfXmlData.ClobAndString:
						return this.m_xmlTypeData.m_xmlClob.IsEmpty;
					default:
						if (typeOfXmlData != TypeOfXmlData.Chars)
						{
							goto IL_117;
						}
						return this.m_xmlTypeData.m_xmlChars.Length == 0;
					}
				}
				else
				{
					switch (typeOfXmlData)
					{
					case TypeOfXmlData.XmlDoc:
						return string.IsNullOrEmpty(this.m_xmlTypeData.m_xmlDocInternal.OuterXml);
					case (TypeOfXmlData)33:
						goto IL_117;
					case TypeOfXmlData.StringAndXmlDoc:
						break;
					default:
						switch (typeOfXmlData)
						{
						case TypeOfXmlData.BlobWithText:
						case TypeOfXmlData.BlobWithTextAndString:
							return this.m_xmlTypeData.m_xmlBlobText.IsEmpty;
						case (TypeOfXmlData)65:
							goto IL_117;
						default:
							switch (typeOfXmlData)
							{
							case TypeOfXmlData.BlobCSX:
								return false;
							case (TypeOfXmlData)129:
								goto IL_117;
							case TypeOfXmlData.BlobCSXAndString:
								return string.IsNullOrEmpty(this.m_xmlTypeData.m_xmlStr);
							default:
								goto IL_117;
							}
							break;
						}
						break;
					}
				}
				return string.IsNullOrEmpty(this.m_xmlTypeData.m_xmlStr);
				IL_117:;
			}
			return true;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000BE678 File Offset: 0x000BC878
		internal void GetXmlDataForPickling(out TypeOfXmlData typeOfXmlData, out object xmlData)
		{
			lock (this.m_syncLock)
			{
				TypeOfXmlData typeOfXmlData2 = this.m_xmlTypeData.m_typeOfXmlData;
				if (typeOfXmlData2 <= TypeOfXmlData.Chars)
				{
					switch (typeOfXmlData2)
					{
					case TypeOfXmlData.String:
						break;
					case (TypeOfXmlData)3:
					case (TypeOfXmlData)5:
						goto IL_139;
					case TypeOfXmlData.Clob:
					case TypeOfXmlData.ClobAndString:
						xmlData = this.m_xmlTypeData.m_xmlClob;
						typeOfXmlData = TypeOfXmlData.Clob;
						goto IL_13F;
					default:
						if (typeOfXmlData2 != TypeOfXmlData.Chars)
						{
							goto IL_139;
						}
						xmlData = this.m_xmlTypeData.m_xmlChars;
						typeOfXmlData = TypeOfXmlData.Chars;
						goto IL_13F;
					}
				}
				else
				{
					switch (typeOfXmlData2)
					{
					case TypeOfXmlData.XmlDoc:
						if (this.m_xmlDocFragmentInternal != null && this.m_bIsFragment)
						{
							xmlData = this.m_xmlTypeData.m_xmlDocInternal.DocumentElement.InnerXml;
						}
						else
						{
							xmlData = this.m_xmlTypeData.m_xmlDocInternal.OuterXml;
						}
						typeOfXmlData = TypeOfXmlData.String;
						goto IL_13F;
					case (TypeOfXmlData)33:
						goto IL_139;
					case TypeOfXmlData.StringAndXmlDoc:
						break;
					default:
						switch (typeOfXmlData2)
						{
						case TypeOfXmlData.BlobWithText:
							xmlData = this.m_xmlTypeData.m_xmlBlobText.Value;
							typeOfXmlData = TypeOfXmlData.String;
							goto IL_13F;
						case (TypeOfXmlData)65:
							goto IL_139;
						case TypeOfXmlData.BlobWithTextAndString:
							break;
						default:
							switch (typeOfXmlData2)
							{
							case TypeOfXmlData.BlobCSX:
							case TypeOfXmlData.BlobCSXAndString:
								xmlData = this.m_xmlTypeData.m_xmlBlobCSX;
								typeOfXmlData = TypeOfXmlData.BlobCSX;
								goto IL_13F;
							case (TypeOfXmlData)129:
								goto IL_139;
							default:
								goto IL_139;
							}
							break;
						}
						break;
					}
				}
				xmlData = this.m_xmlTypeData.m_xmlStr;
				typeOfXmlData = TypeOfXmlData.String;
				goto IL_13F;
				IL_139:
				typeOfXmlData = TypeOfXmlData.NoData;
				xmlData = null;
				IL_13F:;
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000BE7EC File Offset: 0x000BC9EC
		internal string GetBinXmlDecodedString(OracleBlob blobCSX)
		{
			string result;
			try
			{
				ObxmlDecodeStream decodeStream = this.m_connImpl.GetDecodeStream(blobCSX.Connection, blobCSX);
				string text = decodeStream.DecodeBlob();
				this.m_connImpl.CloseDecodeStream(decodeStream);
				result = ((text == null) ? string.Empty : text);
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000BE844 File Offset: 0x000BCA44
		internal StringBuilder GetBinXmlDecodedStringBuilder(OracleBlob blobCSX)
		{
			StringBuilder result;
			try
			{
				ObxmlDecodeStream decodeStream = this.m_connImpl.GetDecodeStream(blobCSX.Connection, blobCSX);
				StringBuilder stringBuilder = decodeStream.DecodeBlobForXmlStream();
				this.m_connImpl.CloseDecodeStream(decodeStream);
				result = stringBuilder;
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000BE890 File Offset: 0x000BCA90
		internal string GetString()
		{
			string empty;
			lock (this.m_syncLock)
			{
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
						goto IL_249;
					case TypeOfXmlData.Clob:
						this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlClob.Value;
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
						return this.m_xmlTypeData.m_xmlStr;
					default:
						if (typeOfXmlData != TypeOfXmlData.Chars)
						{
							goto IL_249;
						}
						this.m_xmlTypeData.m_xmlStr = new string(this.m_xmlTypeData.m_xmlChars);
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData & (TypeOfXmlData)(-17));
						this.m_xmlTypeData.m_xmlChars = null;
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
						return this.m_xmlTypeData.m_xmlStr;
					}
				}
				else
				{
					switch (typeOfXmlData)
					{
					case TypeOfXmlData.XmlDoc:
						if (this.m_xmlDocFragmentInternal != null && this.m_bIsFragment)
						{
							this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlDocInternal.DocumentElement.InnerXml;
						}
						else
						{
							this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlDocInternal.OuterXml;
						}
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
						return this.m_xmlTypeData.m_xmlStr;
					case (TypeOfXmlData)33:
						goto IL_249;
					case TypeOfXmlData.StringAndXmlDoc:
						break;
					default:
						switch (typeOfXmlData)
						{
						case TypeOfXmlData.BlobWithText:
							this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlBlobText.Value;
							this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
							return this.m_xmlTypeData.m_xmlStr;
						case (TypeOfXmlData)65:
							goto IL_249;
						case TypeOfXmlData.BlobWithTextAndString:
							break;
						default:
							switch (typeOfXmlData)
							{
							case TypeOfXmlData.BlobCSX:
								this.m_xmlTypeData.m_xmlStr = this.GetBinXmlDecodedString(this.m_xmlTypeData.m_xmlBlobCSX);
								this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
								return this.m_xmlTypeData.m_xmlStr;
							case (TypeOfXmlData)129:
								goto IL_249;
							case TypeOfXmlData.BlobCSXAndString:
								break;
							default:
								goto IL_249;
							}
							break;
						}
						break;
					}
				}
				return this.m_xmlTypeData.m_xmlStr;
				IL_249:
				empty = string.Empty;
			}
			return empty;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000BEB18 File Offset: 0x000BCD18
		internal void GetXmlSchemaProp(out string schemaURL, out byte[] schemaID, out bool bHasTargetNamespace)
		{
			schemaURL = this.m_schemaUrlFromXmlDoc;
			schemaID = this.m_schemaID;
			bHasTargetNamespace = this.m_bHasTargetNamespaceProp;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000BEB34 File Offset: 0x000BCD34
		internal XmlDocument GetXmlDocument(bool bInternalUse, bool bThrowException)
		{
			XmlDocument xmlDocument = null;
			lock (this.m_syncLock)
			{
				if (TypeOfXmlData.XmlDoc == (this.m_xmlTypeData.m_typeOfXmlData & TypeOfXmlData.XmlDoc) && bInternalUse)
				{
					return this.m_xmlTypeData.m_xmlDocInternal;
				}
				TypeOfXmlData typeOfXmlData = this.m_xmlTypeData.m_typeOfXmlData;
				XmlTypeReader xmlTypeReader;
				if (typeOfXmlData <= TypeOfXmlData.Chars)
				{
					switch (typeOfXmlData)
					{
					case TypeOfXmlData.String:
					case TypeOfXmlData.ClobAndString:
						break;
					case (TypeOfXmlData)3:
					case (TypeOfXmlData)5:
						goto IL_327;
					case TypeOfXmlData.Clob:
						this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlClob.Value;
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData & (TypeOfXmlData)(-5));
						this.m_xmlTypeData.m_xmlClob.Dispose();
						this.m_xmlTypeData.m_xmlClob = null;
						xmlTypeReader = new XmlTypeReader(new StringReader(this.m_xmlTypeData.m_xmlStr));
						goto IL_337;
					default:
						if (typeOfXmlData != TypeOfXmlData.Chars)
						{
							goto IL_327;
						}
						this.m_xmlTypeData.m_xmlStr = new string(this.m_xmlTypeData.m_xmlChars);
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData & (TypeOfXmlData)(-17));
						this.m_xmlTypeData.m_xmlChars = null;
						xmlTypeReader = new XmlTypeReader(new StringReader(this.m_xmlTypeData.m_xmlStr));
						goto IL_337;
					}
				}
				else
				{
					switch (typeOfXmlData)
					{
					case TypeOfXmlData.XmlDoc:
						this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlDocInternal.OuterXml;
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
						xmlTypeReader = new XmlTypeReader(new StringReader(this.m_xmlTypeData.m_xmlStr));
						goto IL_337;
					case (TypeOfXmlData)33:
						goto IL_327;
					case TypeOfXmlData.StringAndXmlDoc:
						break;
					default:
						switch (typeOfXmlData)
						{
						case TypeOfXmlData.BlobWithText:
							this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlBlobText.Value;
							this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
							this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData & (TypeOfXmlData)(-65));
							this.m_xmlTypeData.m_xmlBlobText.Dispose();
							this.m_xmlTypeData.m_xmlBlobText = null;
							xmlTypeReader = new XmlTypeReader(new StringReader(this.m_xmlTypeData.m_xmlStr));
							goto IL_337;
						case (TypeOfXmlData)65:
							goto IL_327;
						case TypeOfXmlData.BlobWithTextAndString:
							break;
						default:
							switch (typeOfXmlData)
							{
							case TypeOfXmlData.BlobCSX:
								this.m_xmlTypeData.m_xmlStr = this.GetBinXmlDecodedString(this.m_xmlTypeData.m_xmlBlobCSX);
								this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
								this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData & (TypeOfXmlData)(-129));
								this.m_xmlTypeData.m_xmlBlobCSX.Dispose();
								this.m_xmlTypeData.m_xmlBlobCSX = null;
								xmlTypeReader = new XmlTypeReader(new StringReader(this.m_xmlTypeData.m_xmlStr));
								goto IL_337;
							case (TypeOfXmlData)129:
								goto IL_327;
							case TypeOfXmlData.BlobCSXAndString:
								break;
							default:
								goto IL_327;
							}
							break;
						}
						break;
					}
				}
				xmlTypeReader = new XmlTypeReader(new StringReader(this.m_xmlTypeData.m_xmlStr));
				goto IL_337;
				IL_327:
				xmlTypeReader = new XmlTypeReader(new StringReader(""));
				IL_337:
				xmlDocument = DotNetXmlImpl.GetXmlDocument(xmlTypeReader, this.m_xmlTypeData.m_xmlStr, out this.m_xmlDocFragmentInternal, out this.m_bIsFragment, bThrowException);
				if (this.m_bIsFragment)
				{
					this.m_xmlTypeData.m_typeOfXmlData = TypeOfXmlData.XmlDoc;
				}
				this.m_bGotFragmentProp = true;
				this.m_bGotSchemaInfo = true;
				this.m_namespacesFromXmlDoc = xmlTypeReader.m_namespaces;
				this.m_schemaNSFromXmlDoc = xmlTypeReader.m_schemaXmlns;
				this.m_schemaUrlFromXmlDoc = xmlTypeReader.m_schemaURL;
				this.m_bHasTargetNamespaceProp = xmlTypeReader.m_bHasTargetNamespace;
				if (bInternalUse)
				{
					this.m_xmlTypeData.m_xmlDocInternal = xmlDocument;
					this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.XmlDoc);
				}
			}
			return xmlDocument;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000BEF40 File Offset: 0x000BD140
		internal XmlReader GetXmlReader(XmlReaderSettings readerSettings)
		{
			XmlReader result;
			lock (this.m_syncLock)
			{
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
						goto IL_28E;
					case TypeOfXmlData.Clob:
						this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlClob.Value;
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
						return XmlReader.Create(new StringReader(this.m_xmlTypeData.m_xmlStr), readerSettings);
					default:
						if (typeOfXmlData != TypeOfXmlData.Chars)
						{
							goto IL_28E;
						}
						this.m_xmlTypeData.m_xmlStr = new string(this.m_xmlTypeData.m_xmlChars);
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData & (TypeOfXmlData)(-17));
						this.m_xmlTypeData.m_xmlChars = null;
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
						return XmlReader.Create(new StringReader(this.m_xmlTypeData.m_xmlStr), readerSettings);
					}
				}
				else
				{
					switch (typeOfXmlData)
					{
					case TypeOfXmlData.XmlDoc:
						if (this.m_xmlDocFragmentInternal != null && this.m_bIsFragment)
						{
							this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlDocInternal.DocumentElement.InnerXml;
						}
						else
						{
							this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlDocInternal.OuterXml;
						}
						this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
						return XmlReader.Create(new StringReader(this.m_xmlTypeData.m_xmlStr), readerSettings);
					case (TypeOfXmlData)33:
						goto IL_28E;
					case TypeOfXmlData.StringAndXmlDoc:
						break;
					default:
						switch (typeOfXmlData)
						{
						case TypeOfXmlData.BlobWithText:
							this.m_xmlTypeData.m_xmlStr = this.m_xmlTypeData.m_xmlBlobText.Value;
							this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
							return XmlReader.Create(new StringReader(this.m_xmlTypeData.m_xmlStr), readerSettings);
						case (TypeOfXmlData)65:
							goto IL_28E;
						case TypeOfXmlData.BlobWithTextAndString:
							break;
						default:
							switch (typeOfXmlData)
							{
							case TypeOfXmlData.BlobCSX:
								this.m_xmlTypeData.m_xmlStr = this.GetBinXmlDecodedString(this.m_xmlTypeData.m_xmlBlobCSX);
								this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData | TypeOfXmlData.String);
								return XmlReader.Create(new StringReader(this.m_xmlTypeData.m_xmlStr), readerSettings);
							case (TypeOfXmlData)129:
								goto IL_28E;
							case TypeOfXmlData.BlobCSXAndString:
								break;
							default:
								goto IL_28E;
							}
							break;
						}
						break;
					}
				}
				return XmlReader.Create(new StringReader(this.m_xmlTypeData.m_xmlStr), readerSettings);
				IL_28E:
				result = XmlReader.Create(new StringReader(""));
			}
			return result;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000BF214 File Offset: 0x000BD414
		internal void Invalidate(bool bOriginalFragmentValue, bool bValueIsFragment, ref bool bGotRootElement)
		{
			lock (this.m_syncLock)
			{
				if (this.m_typeOfXmlType == TypeOfXmlType.Clob || this.m_typeOfXmlType == (TypeOfXmlType)2147483649U)
				{
					this.m_typeOfXmlType = TypeOfXmlType.String;
				}
				this.m_xmlTypeData.m_typeOfXmlData = (this.m_xmlTypeData.m_typeOfXmlData & (TypeOfXmlData)(-3));
				this.m_xmlTypeData.m_xmlStr = null;
				if (this.m_xmlTypeData.m_xmlClob != null)
				{
					this.m_xmlTypeData.m_xmlClob.Dispose();
					this.m_xmlTypeData.m_xmlClob = null;
					this.m_xmlTypeData.m_typeOfXmlData = TypeOfXmlData.XmlDoc;
				}
				else if (this.m_xmlTypeData.m_xmlBlobText != null)
				{
					this.m_xmlTypeData.m_xmlBlobText.Dispose();
					this.m_xmlTypeData.m_xmlBlobText = null;
					this.m_xmlTypeData.m_typeOfXmlData = TypeOfXmlData.XmlDoc;
				}
				if (bOriginalFragmentValue != bValueIsFragment)
				{
					bGotRootElement = false;
					if (bValueIsFragment)
					{
						this.m_xmlDocFragmentInternal = this.m_xmlTypeData.m_xmlDocInternal.CreateDocumentFragment();
						this.m_xmlDocFragmentInternal.InnerXml = this.m_xmlTypeData.m_xmlDocInternal.DocumentElement.InnerXml;
						this.m_xmlTypeData.m_typeOfXmlData = TypeOfXmlData.XmlDoc;
						this.m_bIsFragment = true;
						this.m_bGotFragmentProp = true;
					}
					else
					{
						this.m_xmlDocFragmentInternal = null;
						this.m_xmlTypeData.m_typeOfXmlData = TypeOfXmlData.XmlDoc;
						this.m_bIsFragment = false;
						this.m_bGotFragmentProp = false;
					}
				}
			}
		}

		// Token: 0x0400136F RID: 4975
		internal object m_syncLock = new object();

		// Token: 0x04001370 RID: 4976
		internal OracleConnectionImpl m_connImpl;

		// Token: 0x04001371 RID: 4977
		internal OraXmlTypeData m_xmlTypeData;

		// Token: 0x04001372 RID: 4978
		internal byte[] m_schemaID;

		// Token: 0x04001373 RID: 4979
		internal byte[] m_schElem;

		// Token: 0x04001374 RID: 4980
		internal byte[] m_snapshot;

		// Token: 0x04001375 RID: 4981
		internal TypeOfXmlType m_typeOfXmlType;

		// Token: 0x04001376 RID: 4982
		internal uint m_xmlFlag;

		// Token: 0x04001377 RID: 4983
		internal Hashtable m_namespacesFromXmlDoc;

		// Token: 0x04001378 RID: 4984
		internal string m_schemaUrlFromXmlDoc;

		// Token: 0x04001379 RID: 4985
		internal string m_schemaNSFromXmlDoc;

		// Token: 0x0400137A RID: 4986
		internal bool m_bIsFragment;

		// Token: 0x0400137B RID: 4987
		internal bool m_bGotFragmentProp;

		// Token: 0x0400137C RID: 4988
		internal bool m_bGotSchemaInfo;

		// Token: 0x0400137D RID: 4989
		internal XmlDocumentFragment m_xmlDocFragmentInternal;

		// Token: 0x0400137E RID: 4990
		internal bool m_bHasTargetNamespaceProp;

		// Token: 0x0400137F RID: 4991
		internal int m_kpsnpLen = 34;
	}
}
