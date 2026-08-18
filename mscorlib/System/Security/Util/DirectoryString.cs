using System;
using System.Collections;

namespace System.Security.Util
{
	// Token: 0x0200048B RID: 1163
	[Serializable]
	internal class DirectoryString : SiteString
	{
		// Token: 0x06002E46 RID: 11846 RVA: 0x0009BD94 File Offset: 0x0009AD94
		public DirectoryString()
		{
			this.m_site = "";
			this.m_separatedSite = new ArrayList();
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x0009BDB2 File Offset: 0x0009ADB2
		public DirectoryString(string directory, bool checkForIllegalChars)
		{
			this.m_site = directory;
			this.m_checkForIllegalChars = checkForIllegalChars;
			this.m_separatedSite = this.CreateSeparatedString(directory);
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x0009BDD8 File Offset: 0x0009ADD8
		private ArrayList CreateSeparatedString(string directory)
		{
			ArrayList arrayList = new ArrayList();
			if (directory == null || directory.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
			}
			string[] array = directory.Split(DirectoryString.m_separators);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && !array[i].Equals(""))
				{
					if (array[i].Equals("*"))
					{
						if (i != array.Length - 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
						}
						arrayList.Add(array[i]);
					}
					else
					{
						if (this.m_checkForIllegalChars && array[i].IndexOfAny(DirectoryString.m_illegalDirectoryCharacters) != -1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectoryOnUrl"));
						}
						arrayList.Add(array[i]);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x0009BE9D File Offset: 0x0009AE9D
		public virtual bool IsSubsetOf(DirectoryString operand)
		{
			return this.IsSubsetOf(operand, true);
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x0009BEA8 File Offset: 0x0009AEA8
		public virtual bool IsSubsetOf(DirectoryString operand, bool ignoreCase)
		{
			if (operand == null)
			{
				return false;
			}
			if (operand.m_separatedSite.Count == 0)
			{
				return this.m_separatedSite.Count == 0 || (this.m_separatedSite.Count > 0 && string.Compare((string)this.m_separatedSite[0], "*", StringComparison.Ordinal) == 0);
			}
			if (this.m_separatedSite.Count == 0)
			{
				return string.Compare((string)operand.m_separatedSite[0], "*", StringComparison.Ordinal) == 0;
			}
			return base.IsSubsetOf(operand, ignoreCase);
		}

		// Token: 0x040017C5 RID: 6085
		private bool m_checkForIllegalChars;

		// Token: 0x040017C6 RID: 6086
		private new static char[] m_separators = new char[]
		{
			'/'
		};

		// Token: 0x040017C7 RID: 6087
		protected static char[] m_illegalDirectoryCharacters = new char[]
		{
			'\\',
			':',
			'*',
			'?',
			'"',
			'<',
			'>',
			'|'
		};
	}
}
