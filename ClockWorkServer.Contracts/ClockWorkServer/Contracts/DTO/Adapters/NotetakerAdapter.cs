using System;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C88 RID: 3208
	public static class NotetakerAdapter
	{
		// Token: 0x060042DA RID: 17114 RVA: 0x00022610 File Offset: 0x00020810
		public static string GetName(this NotetakerBaseDTO notetakerBase, bool showStudentNumber = false)
		{
			bool flag = notetakerBase == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = string.Join(" ", (from g in new string[]
				{
					(notetakerBase.FirstName ?? "").Trim(),
					(notetakerBase.LastName ?? "").Trim()
				}
				where g.Length > 0
				select g).ToArray<string>());
				bool flag2 = !showStudentNumber || (notetakerBase.Student_no ?? "").Trim().Length < 1;
				if (flag2)
				{
					result = text;
				}
				else
				{
					result = text + " (" + notetakerBase.Student_no + ")";
				}
			}
			return result;
		}
	}
}
