using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000AA RID: 170
	public class LectureNoteWrapper
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x0000AF9E File Offset: 0x0000919E
		public LectureNoteWrapper()
		{
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00026994 File Offset: 0x00024B94
		public LectureNoteWrapper(int id, int lucid, string notes, DateTime lectureDate, DateTime dateUploaded, string filename)
		{
			this.Id = id;
			this.LuCourseId = lucid;
			this.Notes = SecurityElement.Escape((notes ?? "").Trim());
			bool flag = this.Notes.Length < 1;
			if (flag)
			{
				this.NotesPreview = "";
				this.NotesMore = "";
			}
			else
			{
				string[] array = LectureNoteWrapper.GetLines(this.Notes, false).ToArray<string>();
				bool flag2 = array.Length < 2;
				if (flag2)
				{
					this.NotesPreview = this.Notes;
					this.NotesMore = "";
				}
				else
				{
					this.NotesPreview = array[0];
					this.NotesMore = string.Join("<br />", array.Skip(1).ToArray<string>());
				}
			}
			this.Filename = filename;
			this.LectureDate = this.DateToJavascriptDateString(lectureDate);
			this.DateUploaded = this.DateToJavascriptDateString(dateUploaded);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00026A8D File Offset: 0x00024C8D
		private static IEnumerable<string> GetLines(string str, bool removeEmptyLines = false)
		{
			using (StringReader sr = new StringReader(str))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					bool flag = removeEmptyLines && string.IsNullOrWhiteSpace(line);
					if (!flag)
					{
						yield return line;
					}
				}
				line = null;
			}
			StringReader sr = null;
			yield break;
			yield break;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00026AA4 File Offset: 0x00024CA4
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x00026AAC File Offset: 0x00024CAC
		public int Id { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00026AB5 File Offset: 0x00024CB5
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x00026ABD File Offset: 0x00024CBD
		public int LuCourseId { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00026AC6 File Offset: 0x00024CC6
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00026ACE File Offset: 0x00024CCE
		public string Notes { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00026AD7 File Offset: 0x00024CD7
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00026ADF File Offset: 0x00024CDF
		public string NotesPreview { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00026AE8 File Offset: 0x00024CE8
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x00026AF0 File Offset: 0x00024CF0
		public string NotesMore { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00026AF9 File Offset: 0x00024CF9
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x00026B01 File Offset: 0x00024D01
		public string LectureDate { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00026B0A File Offset: 0x00024D0A
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00026B12 File Offset: 0x00024D12
		public string DateUploaded { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00026B1B File Offset: 0x00024D1B
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00026B23 File Offset: 0x00024D23
		public string Filename { get; set; }

		// Token: 0x06000556 RID: 1366 RVA: 0x00026B2C File Offset: 0x00024D2C
		private string DateToJavascriptDateString(DateTime dt)
		{
			return dt.ToString("o");
		}
	}
}
