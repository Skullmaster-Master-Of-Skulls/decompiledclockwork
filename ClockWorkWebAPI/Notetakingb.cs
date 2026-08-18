using System;
using System.Data;
using System.IO;
using System.Text;
using Databases;

namespace ClockWorkWebAPI
{
	// Token: 0x02000021 RID: 33
	public class Notetakingb
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000E008 File Offset: 0x0000C208
		private static string GetNotesFilename(string originalFilename, string course, DateTime lectureDate, bool isSampleNotes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("notes_");
			stringBuilder.Append(course);
			stringBuilder.Append(".");
			stringBuilder.Append(lectureDate.ToString("yyyy_MM-dd"));
			stringBuilder.Append(Path.GetExtension(originalFilename.ToString()));
			return stringBuilder.ToString();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000E06C File Offset: 0x0000C26C
		public static string GetNotesFilename(DataRow dr)
		{
			DateTime lectureDate = (dr["lecturedate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["lecturedate"]);
			string course = dr["course"].ToString();
			string originalFilename = dr["docname"].ToString();
			bool isSampleNotes = dr["issamplenotes"] != DBNull.Value && (bool)dr["issamplenotes"];
			return Notetakingb.GetNotesFilename(originalFilename, course, lectureDate, isSampleNotes);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		public static int AutomaticallyCreatePeerNotetakingServiceProviderRequestsForAStudent(int pid, int notetakingAccommodationCid, int courseExceptionsCid, int expiryDateForAllAccommodationsCid, int autoSetExpiryDateForAllAccommodationsIfMissingMonth)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			return 0;
		}
	}
}
