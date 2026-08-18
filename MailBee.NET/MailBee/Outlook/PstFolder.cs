using System;
using System.IO;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x020005B4 RID: 1460
	public class PstFolder
	{
		// Token: 0x06003108 RID: 12552 RVA: 0x000E637C File Offset: 0x000E537C
		internal PstFolder(bj A_0)
		{
			this.a = A_0;
			this.d = this.a(A_0.kn()) + "_" + this.PstID;
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000E63EC File Offset: 0x000E53EC
		internal PstFolder(bj A_0, string A_1, string A_2, string A_3)
		{
			if (A_3 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.a = A_0;
			this.e = A_3;
			if (A_1 != string.Empty)
			{
				this.b = A_1 + this.e;
			}
			this.b += A_0.kn();
			if (A_2 != string.Empty)
			{
				this.c = A_2 + this.e;
			}
			this.d = this.a(A_0.kn()) + "_" + this.PstID;
			this.c += this.d;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000E64DC File Offset: 0x000E54DC
		private string a(string A_0)
		{
			foreach (char c in Path.GetInvalidFileNameChars())
			{
				if (A_0.IndexOf(c) != -1)
				{
					A_0 = A_0.Replace(c, '_');
				}
			}
			return A_0;
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000E6517 File Offset: 0x000E5517
		public int PstID
		{
			get
			{
				return this.a.fd().a;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000E6529 File Offset: 0x000E5529
		public string ShortName
		{
			get
			{
				return this.a.kn();
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x000E6536 File Offset: 0x000E5536
		public string SafeShortName
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000E653E File Offset: 0x000E553E
		public string Name
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000E6546 File Offset: 0x000E5546
		public string SafeName
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000E654E File Offset: 0x000E554E
		public PstItemCollection Items
		{
			get
			{
				return new PstItemCollection(this.a);
			}
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000E655B File Offset: 0x000E555B
		public PstFolderCollection GetPstSubFolders(bool includeSubFolders)
		{
			return this.GetPstSubFolders(includeSubFolders, this.e);
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000E656C File Offset: 0x000E556C
		public PstFolderCollection GetPstSubFolders(bool includeSubFolders, string delimiter)
		{
			PstFolderCollection pstFolderCollection = new PstFolderCollection();
			PstFolder.a(includeSubFolders, this, pstFolderCollection, delimiter);
			return pstFolderCollection;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000E658C File Offset: 0x000E558C
		internal static void a(bool A_0, PstFolder A_1, PstFolderCollection A_2, string A_3)
		{
			foreach (object obj in A_1.a.j())
			{
				bj bj = (bj)obj;
				PstFolder pstFolder = new PstFolder(bj, A_1.Name, A_1.SafeName, A_3);
				A_2.a(pstFolder);
				if (A_0 && bj.g())
				{
					PstFolder.a(A_0, pstFolder, A_2, A_3);
				}
			}
		}

		// Token: 0x0400203B RID: 8251
		private bj a;

		// Token: 0x0400203C RID: 8252
		private string b = string.Empty;

		// Token: 0x0400203D RID: 8253
		private string c = string.Empty;

		// Token: 0x0400203E RID: 8254
		private string d = string.Empty;

		// Token: 0x0400203F RID: 8255
		private string e = "\\";
	}
}
