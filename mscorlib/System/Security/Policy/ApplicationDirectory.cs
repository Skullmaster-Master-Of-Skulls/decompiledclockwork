using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000495 RID: 1173
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectory : IBuiltInEvidence
	{
		// Token: 0x06002E77 RID: 11895 RVA: 0x0009CBF8 File Offset: 0x0009BBF8
		internal ApplicationDirectory()
		{
			this.m_appDirectory = null;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x0009CC07 File Offset: 0x0009BC07
		public ApplicationDirectory(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_appDirectory = new URLString(name);
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x0009CC29 File Offset: 0x0009BC29
		public string Directory
		{
			get
			{
				return this.m_appDirectory.ToString();
			}
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x0009CC38 File Offset: 0x0009BC38
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			if (!(o is ApplicationDirectory))
			{
				return false;
			}
			ApplicationDirectory applicationDirectory = (ApplicationDirectory)o;
			if (this.m_appDirectory == null)
			{
				return applicationDirectory.m_appDirectory == null;
			}
			return applicationDirectory.m_appDirectory != null && this.m_appDirectory.IsSubsetOf(applicationDirectory.m_appDirectory) && applicationDirectory.m_appDirectory.IsSubsetOf(this.m_appDirectory);
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x0009CC9D File Offset: 0x0009BC9D
		public override int GetHashCode()
		{
			return this.Directory.GetHashCode();
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x0009CCAC File Offset: 0x0009BCAC
		public object Copy()
		{
			return new ApplicationDirectory
			{
				m_appDirectory = this.m_appDirectory
			};
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x0009CCCC File Offset: 0x0009BCCC
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.ApplicationDirectory");
			securityElement.AddAttribute("version", "1");
			if (this.m_appDirectory != null)
			{
				securityElement.AddChild(new SecurityElement("Directory", this.m_appDirectory.ToString()));
			}
			return securityElement;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x0009CD18 File Offset: 0x0009BD18
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position++] = '\0';
			string directory = this.Directory;
			int length = directory.Length;
			if (verbose)
			{
				BuiltInEvidenceHelper.CopyIntToCharArray(length, buffer, position);
				position += 2;
			}
			directory.CopyTo(0, buffer, position, length);
			return length + position;
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x0009CD5C File Offset: 0x0009BD5C
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			int intFromCharArray = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position);
			position += 2;
			this.m_appDirectory = new URLString(new string(buffer, position, intFromCharArray));
			return position + intFromCharArray;
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x0009CD8C File Offset: 0x0009BD8C
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			if (verbose)
			{
				return this.Directory.Length + 3;
			}
			return this.Directory.Length + 1;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x0009CDAC File Offset: 0x0009BDAC
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x040017CE RID: 6094
		private URLString m_appDirectory;
	}
}
