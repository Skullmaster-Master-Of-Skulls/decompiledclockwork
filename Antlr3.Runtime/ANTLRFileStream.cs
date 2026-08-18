using System;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace Antlr.Runtime
{
	// Token: 0x02000005 RID: 5
	[FileIOPermission(SecurityAction.Demand, Unrestricted = true)]
	[Serializable]
	public class ANTLRFileStream : ANTLRStringStream
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000240A File Offset: 0x0000060A
		public ANTLRFileStream(string fileName) : this(fileName, null)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002414 File Offset: 0x00000614
		public ANTLRFileStream(string fileName, Encoding encoding)
		{
			this.fileName = fileName;
			this.Load(fileName, encoding);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000242C File Offset: 0x0000062C
		public virtual void Load(string fileName, Encoding encoding)
		{
			if (fileName == null)
			{
				return;
			}
			string text;
			if (encoding == null)
			{
				text = File.ReadAllText(fileName);
			}
			else
			{
				text = File.ReadAllText(fileName, encoding);
			}
			this.data = text.ToCharArray();
			this.n = this.data.Length;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000246B File Offset: 0x0000066B
		public override string SourceName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x0400000A RID: 10
		protected string fileName;
	}
}
