using System;

namespace ImportExportClassLibrary
{
	// Token: 0x02000033 RID: 51
	public class ImportProblem
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000C864 File Offset: 0x0000B864
		public ImportProblem(string _ProblemDescription, ProblemType _ProblemType, ProblemSolution[] _ProblemSolutions)
		{
			this._problemType = _ProblemType;
			this._problemSolutions = _ProblemSolutions;
			this._problemDescription = _ProblemDescription;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000C881 File Offset: 0x0000B881
		public ImportProblem()
		{
			this._problemType = ProblemType.None;
			this._problemSolutions = null;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000C898 File Offset: 0x0000B898
		public override string ToString()
		{
			string text = this._problemDescription;
			if (this._problemSolutions != null)
			{
				text = text + Environment.NewLine + "Available Solutions:";
				foreach (ProblemSolution problemSolution in this._problemSolutions)
				{
					text += Environment.NewLine;
					text += ImportProblem.ProblemSolutionDescriptions[(int)problemSolution];
				}
			}
			return text;
		}

		// Token: 0x040000E3 RID: 227
		public static string[] ProblemSolutionDescriptions = new string[]
		{
			"Add this student to ClockWork",
			"Add this person to ClockWork (no group)",
			"Add this lookup course to ClockWork",
			"Choose another course",
			"Add this course for this student",
			"Unkown",
			"NA",
			"Discard this item.",
			"Ignore this item.",
			"Book Appointment"
		};

		// Token: 0x040000E4 RID: 228
		public ProblemType _problemType;

		// Token: 0x040000E5 RID: 229
		public ProblemSolution[] _problemSolutions;

		// Token: 0x040000E6 RID: 230
		public string _problemDescription;
	}
}
