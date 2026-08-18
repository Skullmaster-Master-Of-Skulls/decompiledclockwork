using System;

namespace System.Web.DataAccess
{
	// Token: 0x02000279 RID: 633
	internal sealed class SqlExpressDBFileAutoCreationErrorFormatter : UnhandledErrorFormatter
	{
		// Token: 0x060020D3 RID: 8403 RVA: 0x0008EB8C File Offset: 0x0008DB8C
		internal SqlExpressDBFileAutoCreationErrorFormatter(Exception exception) : base(exception)
		{
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0008EB95 File Offset: 0x0008DB95
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("SqlExpress_MDF_File_Auto_Creation_MiscSectionTitle");
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x0008EBA1 File Offset: 0x0008DBA1
		protected override string MiscSectionContent
		{
			get
			{
				return SqlExpressDBFileAutoCreationErrorFormatter.CustomErrorMessage;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x0008EBA8 File Offset: 0x0008DBA8
		internal static string CustomErrorMessage
		{
			get
			{
				if (SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage == null)
				{
					lock (SqlExpressDBFileAutoCreationErrorFormatter.s_Lock)
					{
						if (SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage == null)
						{
							string @string = SR.GetString("SqlExpress_MDF_File_Auto_Creation");
							SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage = SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage + "<br><br><p>" + @string + "<br></p>\n";
							SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage += "<ol>\n";
							@string = SR.GetString("SqlExpress_MDF_File_Auto_Creation_1");
							SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage = SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage + "<li>" + @string + "</li>\n";
							@string = SR.GetString("SqlExpress_MDF_File_Auto_Creation_2");
							SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage = SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage + "<li>" + @string + "</li>\n";
							@string = SR.GetString("SqlExpress_MDF_File_Auto_Creation_3");
							SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage = SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage + "<li>" + @string + "</li>\n";
							@string = SR.GetString("SqlExpress_MDF_File_Auto_Creation_4");
							SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage = SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage + "<li>" + @string + "</li>\n";
							SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage += "</ol>\n";
						}
					}
				}
				return SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage;
			}
		}

		// Token: 0x04001ACE RID: 6862
		private static string s_errMessage = null;

		// Token: 0x04001ACF RID: 6863
		private static object s_Lock = new object();
	}
}
