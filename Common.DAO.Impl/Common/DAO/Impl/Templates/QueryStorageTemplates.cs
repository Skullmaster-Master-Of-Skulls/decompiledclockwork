using System;

namespace TechnoPro.Common.DAO.Impl.Templates
{
	// Token: 0x02000036 RID: 54
	public class QueryStorageTemplates
	{
		// Token: 0x0400007E RID: 126
		internal const string QS_TEMPLATE_BY_ID = "DECLARE @cd char(1)\r\nSET @cd='_'\r\n\r\nSELECT et.templateid,et.efrom,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0\r\nTHEN\r\nSUBSTRING( efrom,1,CHARINDEX(@cd,efrom,1)-1)\r\nELSE '' END AS grp,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0 THEN\r\nSUBSTRING( efrom,CHARINDEX(@cd,efrom,1)+1,LEN(efrom)-CHARINDEX(@cd,efrom,1))\r\nELSE efrom END AS title,\r\nCAST(ebody AS varchar(max)) AS EmailBehindTemplate,\r\nebodypdf AS filename,\r\nebt.EmailTemplate,COALESCE(ebt.IsEmailTemplate,CAST(0 AS bit)) AS IsEmailTemplate,\r\nCASE WHEN NOT emisc IS NULL AND NOT CAST(emisc AS varchar(max))='' THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS HasContent,\r\nCASE WHEN @includefilecontents=1 AND NOT emisc IS NULL AND NOT CAST(emisc AS varchar(max))='' \r\nTHEN CAST('<data>' + CAST(emisc AS varchar(max)) + '</data>' AS xml).value('(data)[1]', 'varbinary(max)')\r\nELSE NULL END AS BinaryContent,\r\nblankreplacements,datecreated,\r\neto,ecc,ebcc,eattachments,ebody,warningifmissingcodes,bodytype,messagedeliverymethod,errorifmissingcodes\r\nFROM emailtemplates et LEFT JOIN EmailBasedTemplates ebt ON ebt.templateid=et.templateid\r\nWHERE et.templateid=@tid\r\nORDER BY grp,title";

		// Token: 0x0400007F RID: 127
		internal const string QS_TEMPLATES_BY_GROUP = "DECLARE @cd char(1)\r\nSET @cd='_'\r\n\r\nSELECT y.* \r\nFROM\r\n(\r\nSELECT et.templateid,et.efrom,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0\r\nTHEN\r\nSUBSTRING( efrom,1,CHARINDEX(@cd,efrom,1)-1)\r\nELSE '' END AS grp,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0 THEN\r\nSUBSTRING( efrom,CHARINDEX(@cd,efrom,1)+1,LEN(efrom)-CHARINDEX(@cd,efrom,1))\r\nELSE efrom END AS title,\r\nCAST(ebody AS varchar(max)) AS EmailBehindTemplate,\r\nebodypdf AS filename,\r\nebt.EmailTemplate,COALESCE(ebt.IsEmailTemplate,CAST(0 AS bit)) AS IsEmailTemplate,\r\nCASE WHEN NOT emisc IS NULL AND NOT CAST(emisc AS varchar(max))='' THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS HasContent,\r\nCASE WHEN @includefilecontents=1 AND NOT emisc IS NULL AND NOT CAST(emisc AS varchar(max))='' \r\nTHEN CAST('<data>' + CAST(emisc AS varchar(max)) + '</data>' AS xml).value('(data)[1]', 'varbinary(max)')\r\nELSE NULL END AS BinaryContent,\r\nblankreplacements,datecreated,\r\neto,ecc,ebcc,eattachments,ebody,warningifmissingcodes,bodytype,messagedeliverymethod,errorifmissingcodes\r\nFROM emailtemplates et LEFT JOIN EmailBasedTemplates ebt ON ebt.templateid=et.templateid\r\nWHERE et.isactive=1\r\n) y WHERE y.grp=@groupid\r\nORDER BY y.grp,y.title";

		// Token: 0x04000080 RID: 128
		internal const string QS_ALL_TEMPLATE_GROUPS = "DECLARE @cd char(1)\r\nSET @cd='_'\r\n\r\nSELECT DISTINCT x.* FROM\r\n(\r\nSELECT CASE WHEN CHARINDEX(@cd,efrom,1) > 0\r\nTHEN\r\nSUBSTRING( efrom,1,CHARINDEX(@cd,efrom,1)-1)\r\nELSE '' END AS grp,'' AS grptitle\r\nFROM emailtemplates et \r\nWHERE et.isactive=1\r\n\r\nUNION\r\n\r\nSELECT templategroupname AS grp,templategrouptitle AS grptitle \r\nFROM emailtemplategroups \r\nWHERE isactive=1\r\n) x\r\n\r\nORDER BY x.grp";

		// Token: 0x04000081 RID: 129
		internal const string QS_LOAD_ALL_TEMPLATES = "DECLARE @cd char(1)\r\nSET @cd='_'\r\n\r\nSELECT y.* \r\nFROM\r\n(\r\nSELECT et.templateid,et.efrom,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0\r\nTHEN\r\nSUBSTRING( efrom,1,CHARINDEX(@cd,efrom,1)-1)\r\nELSE '' END AS grp,\r\nCASE WHEN CHARINDEX(@cd,efrom,1) > 0 THEN\r\nSUBSTRING( efrom,CHARINDEX(@cd,efrom,1)+1,LEN(efrom)-CHARINDEX(@cd,efrom,1))\r\nELSE efrom END AS title,\r\nCAST(ebody AS varchar(max)) AS EmailBehindTemplate,\r\nebodypdf AS filename,\r\nebt.EmailTemplate,COALESCE(ebt.IsEmailTemplate,CAST(0 AS bit)) AS IsEmailTemplate,\r\nCASE WHEN NOT emisc IS NULL AND NOT CAST(emisc AS nvarchar(max))='' THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS HasContent,\r\nCASE WHEN @includefilecontents=1 AND NOT emisc IS NULL AND NOT CAST(emisc AS nvarchar(max))='' \r\nTHEN CAST('<data>' + CAST(emisc AS nvarchar(max)) + '</data>' AS xml).value('(data)[1]', 'varbinary(max)')\r\nELSE NULL END AS BinaryContent,\r\nblankreplacements,datecreated,\r\neto,ecc,ebcc,eattachments,ebody,warningifmissingcodes,bodytype,messagedeliverymethod,errorifmissingcodes\r\nFROM emailtemplates et LEFT JOIN EmailBasedTemplates ebt ON ebt.templateid=et.templateid\r\nWHERE et.isactive=1\r\n) y \r\nORDER BY y.grp,y.title";

		// Token: 0x04000082 RID: 130
		internal const string QI_TEMPLATE = "IF NOT EXISTS(SELECT templateid FROM emailtemplates WHERE efrom=@title)\r\nBEGIN\r\n    INSERT INTO emailtemplates (efrom,eto,ecc,ebcc,eattachments,ebody,emisc,blankreplacements,errorifmissingcodes) VALUES (@title,'','','','','','','',@fieldmappings)\r\n    SET @templateid = CAST(SCOPE_IDENTITY() as int)\r\nEND\r\nELSE\r\nBEGIN\r\n    SET @templateid = (SELECT TOP 1 templateid FROM emailtemplates WHERE efrom=@title)\r\nEND";

		// Token: 0x04000083 RID: 131
		internal const string QI_TEMPLATE_GROUP = "IF NOT @gid IS NULL AND NOT @gid='' AND NOT EXISTS(SELECT templategroupname FROM emailtemplategroups WHERE templategroupname=@gid)\r\nBEGIN\r\n    INSERT INTO emailtemplategroups (templategroupname,TemplateGroupTitle,isactive) VALUES (@gid,@title,1);\r\nEND\r\n\r\nSET @newgid=@gid";

		// Token: 0x04000084 RID: 132
		internal const string QU_TEMPLATE_TITLE_AND_GROUP = "UPDATE emailtemplates SET efrom=@title,errorifmissingcodes=@fieldmappings WHERE templateid=@tid";

		// Token: 0x04000085 RID: 133
		internal const string QU_TEMPLATE_DOCUMENT = "UPDATE emailtemplates SET emisc=@bb,ebodypdf=@filename WHERE templateid=@tid";

		// Token: 0x04000086 RID: 134
		internal const string QU_TEMPLATE_EMAIL_BEHIND_DOCUMENT = "UPDATE emailtemplates SET eto=@eto,ecc=@ecc,ebcc=@ebcc,warningifmissingcodes=@eattach,eattachments=@esubject,ebody=@ebody,bodytype=@bodytype,blankreplacements=@blankreplacements,MessageDeliveryMethod=@deliverymethod WHERE templateid=@tid";

		// Token: 0x04000087 RID: 135
		internal const string QD_TEMPLATE_DOCUMENT = "UPDATE emailtemplates SET ebodypdf='',emisc='' WHERE templateid=@tid";

		// Token: 0x04000088 RID: 136
		internal const string QD_TEMPLATE_EMAIL = "UPDATE emailtemplates SET ebodypdf='',emisc='' WHERE templateid=@tid";

		// Token: 0x04000089 RID: 137
		internal const string QD_TEMPLATE_EMAIL_BEHIND_DOCUMENT = "UPDATE emailtemplates SET eto='',ecc='',ebcc='',eattachments='',ebody='',warningifmissingcodes='',bodytype=0,blankreplacements='' WHERE templateid=@tid";

		// Token: 0x0400008A RID: 138
		internal const string QD_TEMPLATE = "DELETE FROM emailtemplates WHERE templateid=@tid";

		// Token: 0x0400008B RID: 139
		internal const string QD_TEMPLATE_GROUP = "DELETE FROM emailtemplategroups WHERE templategroupname=@gid";
	}
}
