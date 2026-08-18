using System;
using a.l;
using Microsoft.Exchange.WebServices.Data;

namespace MailBee.EwsMail
{
	// Token: 0x02000524 RID: 1316
	public class EwsFolder
	{
		// Token: 0x06002B44 RID: 11076 RVA: 0x000CC56E File Offset: 0x000CB56E
		internal EwsFolder(Folder A_0, global::a.l.b<char> A_1)
		{
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x000CC584 File Offset: 0x000CB584
		public string ShortName
		{
			get
			{
				return this.a.DisplayName;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000CC591 File Offset: 0x000CB591
		public FolderId Id
		{
			get
			{
				return this.a.Id;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000CC59E File Offset: 0x000CB59E
		public Folder NativeFolder
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x000CC5A8 File Offset: 0x000CB5A8
		public int UnreadCount
		{
			get
			{
				int result;
				try
				{
					result = this.a.UnreadCount;
				}
				catch (ServiceObjectPropertyException)
				{
					result = -1;
				}
				return result;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000CC5DC File Offset: 0x000CB5DC
		public int TotalCount
		{
			get
			{
				int result;
				try
				{
					result = this.a.TotalCount;
				}
				catch (ServiceObjectPropertyException)
				{
					result = -1;
				}
				return result;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x000CC610 File Offset: 0x000CB610
		public long Size
		{
			get
			{
				long result;
				if (this.a.TryGetProperty<long>(d.a, out result))
				{
					return result;
				}
				return -1L;
			}
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000CC638 File Offset: 0x000CB638
		private string a(string A_0)
		{
			if (string.IsNullOrEmpty(A_0))
			{
				return A_0;
			}
			char oldChar = A_0[0];
			return A_0.Replace(oldChar, this.b.a()).TrimStart(new char[]
			{
				this.b.a()
			});
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x000CC684 File Offset: 0x000CB684
		public string FullName
		{
			get
			{
				string a_ = null;
				if (this.a.TryGetProperty<string>(d.d, out a_))
				{
					return this.a(a_);
				}
				return null;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x000CC6B0 File Offset: 0x000CB6B0
		internal string FullNameSafe
		{
			get
			{
				string fullName = this.FullName;
				if (fullName == null)
				{
					return string.Empty;
				}
				return fullName;
			}
		}

		// Token: 0x04001DDC RID: 7644
		private Folder a;

		// Token: 0x04001DDD RID: 7645
		private global::a.l.b<char> b;
	}
}
