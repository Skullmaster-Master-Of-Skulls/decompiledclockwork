using System;
using System.Collections;
using System.Text;
using a;
using a.f;

namespace MailBee.ImapMail
{
	// Token: 0x02000175 RID: 373
	public class Folder
	{
		// Token: 0x06000CC7 RID: 3271 RVA: 0x000329F5 File Offset: 0x000319F5
		internal Folder()
		{
			this.a = null;
			this.b = null;
			this.c = null;
			this.d = FolderFlags.None;
			this.e = false;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00032A20 File Offset: 0x00031A20
		private Folder(string A_0, string A_1, string A_2, FolderFlags A_3)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = true;
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00032A4C File Offset: 0x00031A4C
		public string Delimiter
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00032A54 File Offset: 0x00031A54
		public string Name
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00032A5C File Offset: 0x00031A5C
		public string RawName
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00032A64 File Offset: 0x00031A64
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00032A6C File Offset: 0x00031A6C
		internal string RawNameInternal
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00032A78 File Offset: 0x00031A78
		public string ShortName
		{
			get
			{
				if (this.a == null)
				{
					return this.b;
				}
				int num = this.b.LastIndexOf(this.a);
				if (num > -1)
				{
					return this.b.Substring(num + this.a.Length);
				}
				return this.b;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00032ACC File Offset: 0x00031ACC
		public int NestingLevel
		{
			get
			{
				if (this.a == null)
				{
					return 0;
				}
				int num = 0;
				int num2 = 0;
				while ((num2 = this.b.IndexOf(this.a, num2)) > -1)
				{
					num++;
					num2 += this.a.Length;
					if (num2 >= this.b.Length)
					{
						return num;
					}
				}
				return num;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00032B23 File Offset: 0x00031B23
		public FolderFlags Flags
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00032B2B File Offset: 0x00031B2B
		public bool IsValid
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00032B34 File Offset: 0x00031B34
		internal static Folder b(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null || A_0.Count < 3)
			{
				return null;
			}
			ArrayList arrayList = A_0[0] as ArrayList;
			if (arrayList == null)
			{
				return null;
			}
			string a_ = null;
			if (A_0[1] != null)
			{
				try
				{
					a_ = ((ao)A_0[1]).a(A_1);
				}
				catch
				{
					return null;
				}
			}
			string text = null;
			if (A_0[2] == null)
			{
				return null;
			}
			try
			{
				text = ((ao)A_0[2]).a(A_1);
			}
			catch
			{
				return null;
			}
			return new Folder(a_, global::a.f.f.a(text), text, Folder.a(arrayList, A_1));
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00032BE0 File Offset: 0x00031BE0
		private static FolderFlags a(ArrayList A_0, Encoding A_1)
		{
			FolderFlags folderFlags = FolderFlags.None;
			for (int i = 0; i < A_0.Count; i++)
			{
				if (A_0[i] != null)
				{
					string text = null;
					try
					{
						text = ((ao)A_0[i]).a(A_1);
					}
					catch
					{
						return FolderFlags.None;
					}
					text = text.ToUpper();
					uint num = global::b.a(text);
					if (num <= 1448075807U)
					{
						if (num <= 446503330U)
						{
							if (num <= 117193548U)
							{
								if (num != 42809869U)
								{
									if (num == 117193548U)
									{
										if (text == "\\UNMARKED")
										{
											folderFlags |= FolderFlags.Unmarked;
										}
									}
								}
								else if (text == "\\SENT")
								{
									folderFlags |= FolderFlags.Sent;
								}
							}
							else if (num != 290545591U)
							{
								if (num == 446503330U)
								{
									if (text == "\\SPAM")
									{
										folderFlags |= FolderFlags.Spam;
									}
								}
							}
							else if (text == "\\FLAGGED")
							{
								folderFlags |= FolderFlags.Starred;
							}
						}
						else if (num <= 1143089091U)
						{
							if (num != 831942903U)
							{
								if (num == 1143089091U)
								{
									if (text == "\\NOINFERIORS")
									{
										folderFlags |= FolderFlags.Noinferiors;
									}
								}
							}
							else if (text == "\\TRASH")
							{
								folderFlags |= FolderFlags.Trash;
							}
						}
						else if (num != 1184174750U)
						{
							if (num == 1448075807U)
							{
								if (text == "\\DRAFTS")
								{
									folderFlags |= FolderFlags.Drafts;
								}
							}
						}
						else if (text == "\\HASCHILDREN")
						{
							folderFlags |= FolderFlags.HasChildren;
						}
					}
					else if (num <= 2501544111U)
					{
						if (num <= 2259397630U)
						{
							if (num != 1923776158U)
							{
								if (num == 2259397630U)
								{
									if (text == "\\ALL")
									{
										folderFlags |= FolderFlags.AllMail;
									}
								}
							}
							else if (text == "\\STARRED")
							{
								folderFlags |= FolderFlags.Starred;
							}
						}
						else if (num != 2262964531U)
						{
							if (num == 2501544111U)
							{
								if (text == "\\MARKED")
								{
									folderFlags |= FolderFlags.Marked;
								}
							}
						}
						else if (text == "\\IMPORTANT")
						{
							folderFlags |= FolderFlags.Important;
						}
					}
					else if (num <= 3202671585U)
					{
						if (num != 2591251619U)
						{
							if (num == 3202671585U)
							{
								if (text == "\\INBOX")
								{
									folderFlags |= FolderFlags.Inbox;
								}
							}
						}
						else if (text == "\\ARCHIVE")
						{
							folderFlags |= FolderFlags.Archive;
						}
					}
					else if (num != 3340766897U)
					{
						if (num != 3729549928U)
						{
							if (num == 4189098973U)
							{
								if (text == "\\JUNK")
								{
									folderFlags |= FolderFlags.Spam;
								}
							}
						}
						else if (text == "\\NOSELECT")
						{
							folderFlags |= FolderFlags.Noselect;
						}
					}
					else if (text == "\\HASNOCHILDREN")
					{
						folderFlags |= FolderFlags.HasNoChildren;
					}
				}
			}
			return folderFlags;
		}

		// Token: 0x040008C3 RID: 2243
		private string a;

		// Token: 0x040008C4 RID: 2244
		private string b;

		// Token: 0x040008C5 RID: 2245
		private string c;

		// Token: 0x040008C6 RID: 2246
		private FolderFlags d;

		// Token: 0x040008C7 RID: 2247
		private bool e;
	}
}
