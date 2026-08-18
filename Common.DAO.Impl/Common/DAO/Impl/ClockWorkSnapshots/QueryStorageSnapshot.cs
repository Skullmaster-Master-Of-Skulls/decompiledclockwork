using System;

namespace TechnoPro.Common.DAO.Impl.ClockWorkSnapshots
{
	// Token: 0x0200010E RID: 270
	public class QueryStorageSnapshot
	{
		// Token: 0x04000477 RID: 1143
		internal const string QS_TABLE_CONTENTS_BY_TABLE_NAME = "DECLARE @q varchar(256)\r\nSET @q = \r\n'IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N''[' + @tablename + ']'') AND OBJECTPROPERTY(id, N''IsUserTable'') = 1)\r\nSELECT * FROM ' + @tablename + '\r\nELSE\r\nSELECT ''notexists'' AS errmsg WHERE 1=0'\r\n\r\nEXEC (@q)";

		// Token: 0x04000478 RID: 1144
		internal const string QS_USER_COUNT = "SELECT COUNT(*) FROM people";

		// Token: 0x04000479 RID: 1145
		internal const string QS_TABLES_AND_COLUMNS = "select TABLE_NAME, COLUMN_NAME\r\nfrom INFORMATION_SCHEMA.COLUMNS\r\norder by TABLE_NAME, ORDINAL_POSITION";

		// Token: 0x0400047A RID: 1146
		internal const string QS_TABLES_ORDERED_FOR_COPYING = "with Fkeys as (\r\n    select distinct\r\n         OnTable       = OnTable.name\r\n        ,AgainstTable  = AgainstTable.name \r\n    from \r\n        sysforeignkeys fk\r\n        inner join sysobjects onTable \r\n            on fk.fkeyid = onTable.id\r\n        inner join sysobjects againstTable  \r\n            on fk.rkeyid = againstTable.id\r\n    where 1=1\r\n        AND AgainstTable.TYPE = 'U'\r\n        AND OnTable.TYPE = 'U'\r\n        -- ignore self joins; they cause an infinite recursion\r\n        and OnTable.Name <> AgainstTable.Name\r\n    )\r\n,MyData as (\r\n    select \r\n         OnTable = o.name\r\n        ,AgainstTable = FKeys.againstTable\r\n    from \r\n        sys.objects o\r\n        left join FKeys\r\n            on  o.name = FKeys.onTable\r\n    where 1=1\r\n        and o.type = 'U'\r\n        and o.name not like 'sys%'\r\n    )\r\n,MyRecursion as (\r\n    -- base case\r\n    select  \r\n         TableName    = OnTable\r\n        ,Lvl        = 1\r\n    from\r\n        MyData\r\n    where 1=1\r\n        and AgainstTable is null\r\n\r\n    -- recursive case\r\n    union all select\r\n         TableName    = OnTable\r\n        ,Lvl        = r.Lvl + 1\r\n    from \r\n        MyData d\r\n        inner join MyRecursion r\r\n            on d.AgainstTable = r.TableName\r\n)\r\nselect\r\n    Lvl = max(Lvl)\r\n    ,TableName\r\n    ,strSql = 'delete from [' + tablename + ']'\r\nfrom \r\n    MyRecursion\r\ngroup by\r\n    TableName\r\norder by \r\n     1 \r\n    ,2 ";

		// Token: 0x0400047B RID: 1147
		internal const string QD_TRUNCATE_TABLE = "DECLARE @s varchar(256)\r\nSET @s = 'IF EXISTS(SELECT TOP 1 * FROM ' + @tablename + ') TRUNCATE TABLE ' + @tablename \r\nEXEC (@s)";

		// Token: 0x0400047C RID: 1148
		internal const string QD_DELETE_ALL_TABLE_ROWS = "DECLARE @s varchar(256)\r\nSET @s = 'IF EXISTS(SELECT TOP 1 * FROM ' + @tablename + ') DELETE FROM ' + @tablename\r\nEXEC (@s)";
	}
}
