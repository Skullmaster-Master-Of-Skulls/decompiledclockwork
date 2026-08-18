using System;
using System.Globalization;

namespace System.Data.OleDb
{
	// Token: 0x02000264 RID: 612
	public sealed class OleDbSchemaGuid
	{
		// Token: 0x06002667 RID: 9831 RVA: 0x00103E9C File Offset: 0x0010329C
		internal static string GetTextFromValue(Guid guid)
		{
			if (guid == OleDbSchemaGuid.Primary_Keys)
			{
				return "Primary_Keys";
			}
			if (guid == OleDbSchemaGuid.Indexes)
			{
				return "Indexes";
			}
			if (guid == OleDbSchemaGuid.Procedure_Parameters)
			{
				return "Procedure_Parameters";
			}
			if (guid == OleDbSchemaGuid.Procedures)
			{
				return "Procedures";
			}
			if (guid == OleDbSchemaGuid.Tables_Info)
			{
				return "Tables_Info";
			}
			if (guid == OleDbSchemaGuid.Trustee)
			{
				return "Trustee";
			}
			if (guid == OleDbSchemaGuid.Assertions)
			{
				return "Assertions";
			}
			if (guid == OleDbSchemaGuid.Catalogs)
			{
				return "Catalogs";
			}
			if (guid == OleDbSchemaGuid.Character_Sets)
			{
				return "Character_Sets";
			}
			if (guid == OleDbSchemaGuid.Collations)
			{
				return "Collations";
			}
			if (guid == OleDbSchemaGuid.Columns)
			{
				return "Columns";
			}
			if (guid == OleDbSchemaGuid.Check_Constraints)
			{
				return "Check_Constraints";
			}
			if (guid == OleDbSchemaGuid.Constraint_Column_Usage)
			{
				return "Constraint_Column_Usage";
			}
			if (guid == OleDbSchemaGuid.Constraint_Table_Usage)
			{
				return "Constraint_Table_Usage";
			}
			if (guid == OleDbSchemaGuid.Key_Column_Usage)
			{
				return "Key_Column_Usage";
			}
			if (guid == OleDbSchemaGuid.Referential_Constraints)
			{
				return "Referential_Constraints";
			}
			if (guid == OleDbSchemaGuid.Table_Constraints)
			{
				return "Table_Constraints";
			}
			if (guid == OleDbSchemaGuid.Column_Domain_Usage)
			{
				return "Column_Domain_Usage";
			}
			if (guid == OleDbSchemaGuid.Column_Privileges)
			{
				return "Column_Privileges";
			}
			if (guid == OleDbSchemaGuid.Table_Privileges)
			{
				return "Table_Privileges";
			}
			if (guid == OleDbSchemaGuid.Usage_Privileges)
			{
				return "Usage_Privileges";
			}
			if (guid == OleDbSchemaGuid.Schemata)
			{
				return "Schemata";
			}
			if (guid == OleDbSchemaGuid.Sql_Languages)
			{
				return "Sql_Languages";
			}
			if (guid == OleDbSchemaGuid.Statistics)
			{
				return "Statistics";
			}
			if (guid == OleDbSchemaGuid.Tables)
			{
				return "Tables";
			}
			if (guid == OleDbSchemaGuid.Translations)
			{
				return "Translations";
			}
			if (guid == OleDbSchemaGuid.Provider_Types)
			{
				return "Provider_Types";
			}
			if (guid == OleDbSchemaGuid.Views)
			{
				return "Views";
			}
			if (guid == OleDbSchemaGuid.View_Column_Usage)
			{
				return "View_Column_Usage";
			}
			if (guid == OleDbSchemaGuid.View_Table_Usage)
			{
				return "View_Table_Usage";
			}
			if (guid == OleDbSchemaGuid.Foreign_Keys)
			{
				return "Foreign_Keys";
			}
			if (guid == OleDbSchemaGuid.Procedure_Columns)
			{
				return "Procedure_Columns";
			}
			if (guid == OleDbSchemaGuid.Table_Statistics)
			{
				return "Table_Statistics";
			}
			if (guid == OleDbSchemaGuid.Check_Constraints_By_Table)
			{
				return "Check_Constraints_By_Table";
			}
			return "{" + guid.ToString("D", CultureInfo.InvariantCulture) + ")";
		}

		// Token: 0x04001795 RID: 6037
		public static readonly Guid Tables_Info = new Guid(3367314144U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001796 RID: 6038
		public static readonly Guid Trustee = new Guid(3367314159U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001797 RID: 6039
		public static readonly Guid Assertions = new Guid(3367313936U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001798 RID: 6040
		public static readonly Guid Catalogs = new Guid(3367313937U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001799 RID: 6041
		public static readonly Guid Character_Sets = new Guid(3367313938U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400179A RID: 6042
		public static readonly Guid Collations = new Guid(3367313939U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400179B RID: 6043
		public static readonly Guid Columns = new Guid(3367313940U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400179C RID: 6044
		public static readonly Guid Check_Constraints = new Guid(3367313941U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400179D RID: 6045
		public static readonly Guid Constraint_Column_Usage = new Guid(3367313942U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400179E RID: 6046
		public static readonly Guid Constraint_Table_Usage = new Guid(3367313943U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400179F RID: 6047
		public static readonly Guid Key_Column_Usage = new Guid(3367313944U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A0 RID: 6048
		public static readonly Guid Referential_Constraints = new Guid(3367313945U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A1 RID: 6049
		public static readonly Guid Table_Constraints = new Guid(3367313946U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A2 RID: 6050
		public static readonly Guid Column_Domain_Usage = new Guid(3367313947U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A3 RID: 6051
		public static readonly Guid Indexes = new Guid(3367313950U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A4 RID: 6052
		public static readonly Guid Column_Privileges = new Guid(3367313953U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A5 RID: 6053
		public static readonly Guid Table_Privileges = new Guid(3367313954U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A6 RID: 6054
		public static readonly Guid Usage_Privileges = new Guid(3367313955U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A7 RID: 6055
		public static readonly Guid Procedures = new Guid(3367313956U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A8 RID: 6056
		public static readonly Guid Schemata = new Guid(3367313957U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017A9 RID: 6057
		public static readonly Guid Sql_Languages = new Guid(3367313958U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017AA RID: 6058
		public static readonly Guid Statistics = new Guid(3367313959U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017AB RID: 6059
		public static readonly Guid Tables = new Guid(3367313961U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017AC RID: 6060
		public static readonly Guid Translations = new Guid(3367313962U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017AD RID: 6061
		public static readonly Guid Provider_Types = new Guid(3367313964U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017AE RID: 6062
		public static readonly Guid Views = new Guid(3367313965U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017AF RID: 6063
		public static readonly Guid View_Column_Usage = new Guid(3367313966U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017B0 RID: 6064
		public static readonly Guid View_Table_Usage = new Guid(3367313967U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017B1 RID: 6065
		public static readonly Guid Procedure_Parameters = new Guid(3367314104U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017B2 RID: 6066
		public static readonly Guid Foreign_Keys = new Guid(3367314116U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017B3 RID: 6067
		public static readonly Guid Primary_Keys = new Guid(3367314117U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017B4 RID: 6068
		public static readonly Guid Procedure_Columns = new Guid(3367314121U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017B5 RID: 6069
		public static readonly Guid Table_Statistics = new Guid(3367314175U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017B6 RID: 6070
		public static readonly Guid Check_Constraints_By_Table = new Guid(3367314177U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x040017B7 RID: 6071
		public static readonly Guid SchemaGuids = new Guid(4079373467U, 6240, 19966, 183, 27, 41, 97, 178, 234, 145, 189);

		// Token: 0x040017B8 RID: 6072
		public static readonly Guid DbInfoKeywords = new Guid(4079373468U, 6240, 19966, 183, 27, 41, 97, 178, 234, 145, 189);

		// Token: 0x040017B9 RID: 6073
		public static readonly Guid DbInfoLiterals = new Guid(4079373469U, 6240, 19966, 183, 27, 41, 97, 178, 234, 145, 189);
	}
}
