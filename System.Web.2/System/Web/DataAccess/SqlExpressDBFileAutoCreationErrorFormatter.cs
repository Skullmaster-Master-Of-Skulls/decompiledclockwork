using System;

namespace System.Web.DataAccess
{
	// Token: 0x020001AE RID: 430
	internal sealed class SqlExpressDBFileAutoCreationErrorFormatter : UnhandledErrorFormatter
	{
		// Token: 0x06001658 RID: 5720 RVA: 0x00009727 File Offset: 0x00007927
		internal SqlExpressDBFileAutoCreationErrorFormatter(Exception exception) : base(exception)
		{
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00046B4B File Offset: 0x00044D4B
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("SqlExpress_MDF_File_Auto_Creation_MiscSectionTitle");
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00046B57 File Offset: 0x00044D57
		protected override string MiscSectionContent
		{
			get
			{
				return SqlExpressDBFileAutoCreationErrorFormatter.CustomErrorMessage;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00046B60 File Offset: 0x00044D60
		internal static string CustomErrorMessage
		{
			get
			{
				if (SqlExpressDBFileAutoCreationErrorFormatter.s_errMessage == null)
				{
					object obj = SqlExpressDBFileAutoCreationErrorFormatter.s_Lock;
					lock (obj)
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

		// Token: 0x04001697 RID: 5783
		private static string s_errMessage = null;

		// Token: 0x04001698 RID: 5784
		private static object s_Lock = new object();
	}
}
