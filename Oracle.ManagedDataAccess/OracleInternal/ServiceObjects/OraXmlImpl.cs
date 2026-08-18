using System;
using System.Data;
using System.Xml;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001C3 RID: 451
	internal class OraXmlImpl
	{
		// Token: 0x0600115A RID: 4442 RVA: 0x000BFA04 File Offset: 0x000BDC04
		internal static string GetRootElement(OracleCommand cmd, OracleXmlType xmlType)
		{
			string result;
			try
			{
				string text = string.Empty;
				cmd.CommandText = "xmlType.getRootElement";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(":returnVal", OracleDbType.Varchar2, ParameterDirection.ReturnValue);
				cmd.Parameters[0].DbType = DbType.String;
				cmd.Parameters[0].Size = 1024;
				cmd.Parameters.Add(":self", OracleDbType.XmlType, xmlType, ParameterDirection.Input);
				cmd.ExecuteNonQuery();
				if (cmd.Parameters[0].Value != null || cmd.Parameters[0].Value != DBNull.Value)
				{
					text = (string)cmd.Parameters[0].Value;
				}
				result = text;
			}
			finally
			{
				cmd.Parameters.Clear();
			}
			return result;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000BFAE8 File Offset: 0x000BDCE8
		internal static OracleXmlType Extract(OracleCommand cmd, OracleXmlType xmlType, string xpathExpr, string nsMap)
		{
			OracleXmlType result;
			try
			{
				cmd.CommandText = "xmlType.extract";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(":returnVal", OracleDbType.XmlType, ParameterDirection.ReturnValue);
				cmd.Parameters.Add(":self", OracleDbType.XmlType, xmlType, ParameterDirection.Input);
				cmd.Parameters.Add(":path", OracleDbType.Varchar2, xpathExpr, ParameterDirection.Input);
				if (!string.IsNullOrEmpty(nsMap))
				{
					cmd.Parameters.Add(":nsmap", OracleDbType.Varchar2, nsMap, ParameterDirection.Input);
				}
				cmd.ExecuteNonQuery();
				if (cmd.Parameters[0].Value == null || cmd.Parameters[0].Value != DBNull.Value)
				{
					result = OracleXmlType.Null;
				}
				else
				{
					result = (OracleXmlType)cmd.Parameters[0].Value;
				}
			}
			finally
			{
				cmd.Parameters.Clear();
			}
			return result;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000BFBD4 File Offset: 0x000BDDD4
		internal static OracleXmlType Extract(OracleCommand cmd, OracleXmlType xmlType, string xpathExpr, XmlNamespaceManager nsMgr)
		{
			string nsMap = DotNetXmlImpl.NsMgrToString(nsMgr);
			return OraXmlImpl.Extract(cmd, xmlType, xpathExpr, nsMap);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000BFBF4 File Offset: 0x000BDDF4
		internal static bool IsExists(OracleCommand cmd, OracleXmlType xmlType, string xpathExpr, string nsMap)
		{
			bool result;
			try
			{
				cmd.CommandText = "xmlType.existsNode";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(":returnVal", OracleDbType.Int32, ParameterDirection.ReturnValue);
				cmd.Parameters[0].DbType = DbType.Int32;
				cmd.Parameters.Add(":self", OracleDbType.XmlType, xmlType, ParameterDirection.Input);
				cmd.Parameters.Add(":path", OracleDbType.Varchar2, xpathExpr, ParameterDirection.Input);
				if (!string.IsNullOrEmpty(nsMap))
				{
					cmd.Parameters.Add(":nsmap", OracleDbType.Varchar2, nsMap, ParameterDirection.Input);
				}
				cmd.ExecuteNonQuery();
				int num = (int)cmd.Parameters[0].Value;
				result = (num > 0);
			}
			finally
			{
				cmd.Parameters.Clear();
			}
			return result;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000BFCC8 File Offset: 0x000BDEC8
		internal static bool IsExists(OracleCommand cmd, OracleXmlType xmlType, string xpathExpr, XmlNamespaceManager nsMgr)
		{
			string nsMap = DotNetXmlImpl.NsMgrToString(nsMgr);
			return OraXmlImpl.IsExists(cmd, xmlType, xpathExpr, nsMap);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000BFCE8 File Offset: 0x000BDEE8
		internal static bool IsFragment(OracleCommand cmd, OracleXmlType xmlType)
		{
			bool result;
			try
			{
				cmd.CommandText = "xmlType.IsFragment";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(":returnVal", OracleDbType.Int32, ParameterDirection.ReturnValue);
				cmd.Parameters[0].DbType = DbType.Int32;
				cmd.Parameters.Add(":self", OracleDbType.XmlType, xmlType, ParameterDirection.Input);
				cmd.ExecuteNonQuery();
				int num = (int)cmd.Parameters[0].Value;
				result = (num > 0);
			}
			finally
			{
				cmd.Parameters.Clear();
			}
			return result;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x000BFD8C File Offset: 0x000BDF8C
		internal static OracleXmlType Transform(OracleCommand cmd, OracleXmlType xmlType, OracleXmlType xslDoc, string paramMap)
		{
			OracleXmlType result;
			try
			{
				cmd.CommandText = "xmlType.transform";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(":returnVal", OracleDbType.XmlType, ParameterDirection.ReturnValue);
				cmd.Parameters.Add(":self", OracleDbType.XmlType, xmlType, ParameterDirection.Input);
				cmd.Parameters.Add(":xsldoc", OracleDbType.XmlType, xslDoc, ParameterDirection.Input);
				if (!string.IsNullOrEmpty(paramMap))
				{
					cmd.Parameters.Add(":parammap", OracleDbType.Varchar2, paramMap, ParameterDirection.Input);
				}
				cmd.ExecuteNonQuery();
				if (cmd.Parameters[0].Value != null || cmd.Parameters[0].Value != DBNull.Value)
				{
					result = (OracleXmlType)cmd.Parameters[0].Value;
				}
				else
				{
					result = OracleXmlType.Null;
				}
			}
			finally
			{
				cmd.Parameters.Clear();
			}
			return result;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x000BFE78 File Offset: 0x000BE078
		internal static void Update(OracleCommand cmd, OracleXmlType xmlType, string xpathExpr, string nsMap, string val)
		{
			try
			{
				cmd.CommandText = "UpdateXml";
				cmd.CommandType = CommandType.StoredProcedure;
				OracleParameter param = new OracleParameter(":XMLType_instance", OracleDbType.XmlType, xmlType, ParameterDirection.Input);
				cmd.Parameters.Add(param);
				OracleParameter param2 = new OracleParameter(":XPath_string", OracleDbType.Varchar2, xpathExpr, ParameterDirection.Input);
				cmd.Parameters.Add(param2);
				OracleParameter param3 = new OracleParameter(":value_expr", OracleDbType.Varchar2, val, ParameterDirection.Input);
				cmd.Parameters.Add(param3);
				if (!string.IsNullOrEmpty(nsMap))
				{
					OracleParameter param4 = new OracleParameter(":namespace_string", OracleDbType.Varchar2, nsMap, ParameterDirection.Input);
					cmd.Parameters.Add(param4);
				}
				cmd.ExecuteNonQuery();
			}
			finally
			{
				cmd.Parameters.Clear();
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000BFF38 File Offset: 0x000BE138
		internal static void Update(OracleCommand cmd, OracleXmlType xmlType, string xpathExpr, XmlNamespaceManager nsMgr, string val)
		{
			string nsMap = DotNetXmlImpl.NsMgrToString(nsMgr);
			OraXmlImpl.Update(cmd, xmlType, xpathExpr, nsMap, val);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000BFF58 File Offset: 0x000BE158
		internal static void Update(OracleCommand cmd, OracleXmlType xmlType, string xpathExpr, string nsMap, OracleXmlType newXmlTypeVal)
		{
			try
			{
				cmd.CommandText = "UpdateXml";
				cmd.CommandType = CommandType.StoredProcedure;
				OracleParameter param = new OracleParameter(":XMLType_instance", OracleDbType.XmlType, xmlType, ParameterDirection.Input);
				cmd.Parameters.Add(param);
				OracleParameter param2 = new OracleParameter(":XPath_string", OracleDbType.Varchar2, xpathExpr, ParameterDirection.Input);
				cmd.Parameters.Add(param2);
				OracleParameter param3 = new OracleParameter(":value_expr", OracleDbType.XmlType, newXmlTypeVal, ParameterDirection.Input);
				cmd.Parameters.Add(param3);
				if (!string.IsNullOrEmpty(nsMap))
				{
					OracleParameter param4 = new OracleParameter(":namespace_string", OracleDbType.Varchar2, nsMap, ParameterDirection.Input);
					cmd.Parameters.Add(param4);
				}
				cmd.ExecuteNonQuery();
			}
			finally
			{
				cmd.Parameters.Clear();
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000C0018 File Offset: 0x000BE218
		internal static void Update(OracleCommand cmd, OracleXmlType xmlType, string xpathExpr, XmlNamespaceManager nsMgr, OracleXmlType newXmlTypeVal)
		{
			string nsMap = DotNetXmlImpl.NsMgrToString(nsMgr);
			OraXmlImpl.Update(cmd, xmlType, xpathExpr, nsMap, newXmlTypeVal);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000C0038 File Offset: 0x000BE238
		internal static bool Validate(OracleCommand cmd, OracleXmlType xmlType, string schemaUrl)
		{
			bool result;
			try
			{
				cmd.CommandText = "xmlType.isSchemaValid";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(":returnVal", OracleDbType.Int32, ParameterDirection.ReturnValue);
				cmd.Parameters[0].DbType = DbType.Int32;
				cmd.Parameters.Add(":self", OracleDbType.XmlType, xmlType, ParameterDirection.Input);
				cmd.Parameters.Add(":schurl", OracleDbType.Varchar2, schemaUrl, ParameterDirection.Input);
				cmd.ExecuteNonQuery();
				int num = (int)cmd.Parameters[0].Value;
				result = (num > 0);
			}
			finally
			{
				cmd.Parameters.Clear();
			}
			return result;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000C00F0 File Offset: 0x000BE2F0
		internal static string GetSchemaURL(OracleCommand cmd, OracleXmlType xmlType)
		{
			string result;
			try
			{
				cmd.CommandText = "xmlType.getSchemaURL";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(":returnVal", OracleDbType.Varchar2, ParameterDirection.ReturnValue);
				cmd.Parameters[0].Size = 1024;
				cmd.Parameters[0].DbType = DbType.String;
				cmd.Parameters.Add(":self", OracleDbType.XmlType, xmlType, ParameterDirection.Input);
				cmd.ExecuteNonQuery();
				if (cmd.Parameters[0].Value == null || cmd.Parameters[0].Value == DBNull.Value)
				{
					result = string.Empty;
				}
				else
				{
					result = (string)cmd.Parameters[0].Value;
				}
			}
			finally
			{
				cmd.Parameters.Clear();
			}
			return result;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000C01D4 File Offset: 0x000BE3D4
		internal static void GetSchema(OracleCommand cmd, OracleXmlType xmlType, string schemaUrl, out OracleClob schemaInfo, out byte[] schemaId)
		{
			try
			{
				schemaId = null;
				schemaInfo = null;
				cmd.CommandText = "select c.schema_id, c.schema.getclobval() from user_xml_schemas c where schema_url=:x";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(":x", OracleDbType.Varchar2, schemaUrl, ParameterDirection.Input);
				OracleDataReader oracleDataReader = cmd.ExecuteReader();
				if (oracleDataReader.Read() && !oracleDataReader.IsDBNull(0))
				{
					schemaId = new byte[16];
					oracleDataReader.GetBytes(0, 0L, schemaId, 0, 16);
					schemaInfo = oracleDataReader.GetOracleClob(1);
				}
			}
			finally
			{
				cmd.Parameters.Clear();
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000C0268 File Offset: 0x000BE468
		internal static void GetSchema(OracleCommand cmd, OracleXmlType xmlType, byte[] schemaId, out OracleClob schemaInfo, out string schemaUrl)
		{
			try
			{
				schemaUrl = null;
				schemaInfo = null;
				cmd.CommandText = "select c.schema_url, c.schema.getclobval() from user_xml_schemas c where schema_id=:x";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add(":x", OracleDbType.Raw, schemaId, ParameterDirection.Input);
				OracleDataReader oracleDataReader = cmd.ExecuteReader();
				if (oracleDataReader.Read() && !oracleDataReader.IsDBNull(0))
				{
					schemaUrl = oracleDataReader.GetString(0);
					schemaInfo = oracleDataReader.GetOracleClob(1);
				}
			}
			finally
			{
				cmd.Parameters.Clear();
			}
		}
	}
}
