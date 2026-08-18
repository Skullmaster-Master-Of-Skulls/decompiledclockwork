using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;

namespace OracleInternal.BinXml
{
	// Token: 0x0200001D RID: 29
	internal class ObxmlMetaDataRepository : IDisposable
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public void Dispose()
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000AFE8 File Offset: 0x000091E8
		internal ObxmlMetaDataRepository()
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000AFF0 File Offset: 0x000091F0
		internal bool Validate(ObxmlDecodeContext decodeContextForCurrentCall)
		{
			return decodeContextForCurrentCall != null && decodeContextForCurrentCall.DbConnection != null && decodeContextForCurrentCall.TokenMap != null;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B01C File Offset: 0x0000921C
		internal OracleXmlType GetTokenSet(ObxmlDecodeContext decodeContext, ulong tokenId, TokenTypes tokenType, string nameSpaceId, byte[] guid = null, bool bPopulateTokenMap = false)
		{
			if (ConfigBaseClass.m_XMLTypeOpcodeDump && ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[]
				{
					"(BinXMLOpcodeDump) ***** +++++Fetching Token Set+++++ ******"
				});
			}
			if (!this.Validate(decodeContext))
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.MetaDataRepositoryInvalid, null, ObxmlOpcode.OpcodeIds.None));
			}
			OracleXmlType result;
			try
			{
				OracleCommand oracleCommand = new OracleCommand("xdb.dbms_csx_int.GETVOCABULARYNOTXN", decodeContext.DbConnection);
				oracleCommand.CommandType = CommandType.StoredProcedure;
				oracleCommand.Parameters.Add("Result", OracleDbType.XmlType, 1);
				oracleCommand.Parameters["Result"].Direction = ParameterDirection.ReturnValue;
				oracleCommand.Parameters.Add("VOCABID", OracleDbType.Long).Value = tokenId;
				oracleCommand.Parameters.Add("VOCABTYPE", OracleDbType.Long).Value = ((tokenType == TokenTypes.NamespaceToken) ? 0 : 1);
				if (guid != null)
				{
					oracleCommand.Parameters.Add("RGUID", OracleDbType.Clob).Value = guid;
				}
				new OracleDataAdapter(oracleCommand);
				oracleCommand.ExecuteNonQuery();
				OracleXmlType oracleXmlType = (OracleXmlType)oracleCommand.Parameters["Result"].Value;
				if (bPopulateTokenMap)
				{
					this.PopulateTokenSet(decodeContext, oracleXmlType, TokenTypes.NamespaceToken == tokenType);
				}
				result = oracleXmlType;
			}
			catch (Exception)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.TokenRetrievalFailed, null, ObxmlOpcode.OpcodeIds.None));
			}
			return result;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B18C File Offset: 0x0000938C
		internal string GetResource(ObxmlDecodeContext decodeContext, string absPath)
		{
			if (!this.Validate(decodeContext))
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.MetaDataRepositoryInvalid, null, ObxmlOpcode.OpcodeIds.None));
			}
			string cmdText = "declare\n  res DBMS_XDBResource.XDBResource ;\nbegin\n  res := xdb.DBMS_XDB.getResource( abspath => :1 ) ;\n  :2 := DBMS_XDBResource.getContentVarchar2( res ) ;\nend ;\n";
			string result;
			try
			{
				OracleParameter param = new OracleParameter("1", OracleDbType.Varchar2, absPath, ParameterDirection.Input);
				OracleParameter param2 = new OracleParameter("2", OracleDbType.Varchar2, ParameterDirection.Output);
				OracleCommand oracleCommand = new OracleCommand(cmdText, decodeContext.DbConnection);
				oracleCommand.CommandType = CommandType.Text;
				oracleCommand.BindByName = false;
				oracleCommand.Parameters.Add(param);
				oracleCommand.Parameters.Add(param2);
				new OracleDataAdapter(oracleCommand);
				oracleCommand.ExecuteNonQuery();
				result = (string)oracleCommand.Parameters["2"].Value;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return result;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000B274 File Offset: 0x00009474
		internal void PopulateTokenSet(ObxmlDecodeContext decodeContext, OracleXmlType tokenSet, bool isNameSpaceTokenSet)
		{
			ObxmlDecoder obxmlDecoder = (ObxmlDecoder)decodeContext;
			obxmlDecoder.DecodeState.ProcessingTokenSet = true;
			if (!this.Validate(decodeContext))
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.MetaDataRepositoryInvalid, null, ObxmlOpcode.OpcodeIds.None));
			}
			if (ConfigBaseClass.m_XMLTypeOpcodeDump && ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BinXML, new string[]
				{
					"(BinXMLOpcodeDump) ***** +++++Processing Token Set+++++ ******"
				});
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BinXML, new string[]
				{
					isNameSpaceTokenSet ? "(BinXMLOpcodeDump) ++++++++++++++++++++++NameSpace++++++++++++++++++++++" : "(BinXMLOpcodeDump) ++++++++++++++++++++++Element/Attribute++++++++++++++++++++++"
				});
			}
			string value = tokenSet.Value;
			obxmlDecoder.DecodeState.ProcessingTokenSet = false;
			if (ConfigBaseClass.m_XMLTypeOpcodeDump && ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BinXML, new string[]
				{
					"(BinXMLOpcodeDump) ***** -----Processing Token Set Over----- ******"
				});
			}
		}
	}
}
