using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000084 RID: 132
	public class CrunchEnumerator
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x000252E6 File Offset: 0x000234E6
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x000252ED File Offset: 0x000234ED
		public static string FirstLetters
		{
			get
			{
				return CrunchEnumerator.s_varFirstLetters;
			}
			set
			{
				CrunchEnumerator.s_varFirstLetters = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x000252F5 File Offset: 0x000234F5
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00025305 File Offset: 0x00023505
		public static string PartLetters
		{
			get
			{
				return CrunchEnumerator.s_varPartLetters ?? CrunchEnumerator.s_varFirstLetters;
			}
			set
			{
				CrunchEnumerator.s_varPartLetters = value;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002530D File Offset: 0x0002350D
		internal CrunchEnumerator(IEnumerable<string> avoidNames)
		{
			this.m_skipNames = new HashSet<string>(avoidNames);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00025328 File Offset: 0x00023528
		internal string NextName()
		{
			string currentName;
			do
			{
				this.m_currentName++;
				currentName = this.CurrentName;
			}
			while (this.m_skipNames.Contains(currentName) || JSScanner.IsKeyword(currentName, true));
			return currentName;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00025362 File Offset: 0x00023562
		private string CurrentName
		{
			get
			{
				return CrunchEnumerator.GenerateNameFromNumber(this.m_currentName);
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00025370 File Offset: 0x00023570
		public static string CrunchedLabel(int nestLevel)
		{
			string text = null;
			if (nestLevel >= 0)
			{
				text = CrunchEnumerator.GenerateNameFromNumber(nestLevel);
				if (JSScanner.IsKeyword(text, true))
				{
					text = '_' + text;
				}
			}
			return text;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000253A4 File Offset: 0x000235A4
		public static string GenerateNameFromNumber(int index)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (index >= 0)
			{
				stringBuilder.Append(CrunchEnumerator.s_varFirstLetters[index % CrunchEnumerator.s_varFirstLetters.Length]);
				index /= CrunchEnumerator.s_varFirstLetters.Length;
				while (--index >= 0)
				{
					stringBuilder.Append(CrunchEnumerator.s_varPartLetters[index % CrunchEnumerator.s_varPartLetters.Length]);
					index /= CrunchEnumerator.s_varPartLetters.Length;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400030C RID: 780
		private HashSet<string> m_skipNames;

		// Token: 0x0400030D RID: 781
		private int m_currentName = -1;

		// Token: 0x0400030E RID: 782
		private static string s_varFirstLetters = "ntirufeoshclavypwbkdg";

		// Token: 0x0400030F RID: 783
		private static string s_varPartLetters = "tirufeoshclavypwbkdgn";
	}
}
