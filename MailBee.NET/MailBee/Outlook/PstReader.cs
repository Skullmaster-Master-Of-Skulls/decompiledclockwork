using System;
using System.IO;
using a;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x020005B8 RID: 1464
	public class PstReader
	{
		// Token: 0x0600311C RID: 12572 RVA: 0x000E678E File Offset: 0x000E578E
		public PstReader(string pstFilePath) : this(pstFilePath, null)
		{
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000E6798 File Offset: 0x000E5798
		public PstReader(string pstFilePath, string licenseKey)
		{
			PstReader.a(licenseKey);
			this.a = new bs(pstFilePath);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000E67B2 File Offset: 0x000E57B2
		public PstReader(Stream pstStream) : this(pstStream, null)
		{
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000E67BC File Offset: 0x000E57BC
		public PstReader(Stream pstStream, string licenseKey)
		{
			PstReader.a(licenseKey);
			this.a = new bs(pstStream);
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000E67D6 File Offset: 0x000E57D6
		public void Close()
		{
			this.a.c();
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000E67E3 File Offset: 0x000E57E3
		public PstFolderCollection GetPstRootFolders(bool includeSubFolders)
		{
			return this.GetPstRootFolders(includeSubFolders, "\\");
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000E67F1 File Offset: 0x000E57F1
		public PstFolderCollection GetPstRootFolders(bool includeSubFolders, string delimiter)
		{
			return new PstFolder(this.a.b()).GetPstSubFolders(includeSubFolders, delimiter);
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000E680C File Offset: 0x000E580C
		public PstFolder GetFolderByID(int pstID)
		{
			PstFolder result;
			try
			{
				result = new PstFolder((global::a.b.bj)ii.a(this.a, (long)pstID));
			}
			catch (MailBeePstParsingException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000E684C File Offset: 0x000E584C
		public PstItem GetItemByID(int pstID)
		{
			ii ii;
			try
			{
				ii = ii.a(this.a, (long)pstID);
			}
			catch (MailBeePstParsingException)
			{
				return null;
			}
			string a_ = ii.gr();
			uint num = global::b.a(a_);
			if (num <= 1485052734U)
			{
				if (num != 588408751U)
				{
					if (num != 1174848134U)
					{
						if (num == 1485052734U)
						{
							if (a_ == "IPM.Task")
							{
								return new PstTask((cv)ii);
							}
						}
					}
					else if (a_ == "IPM.Activity")
					{
						return new PstActivity((fm)ii);
					}
				}
				else if (a_ == "IPM.DistList")
				{
					return new PstDistList((el)ii);
				}
			}
			else if (num <= 2731351271U)
			{
				if (num != 1779978289U)
				{
					if (num == 2731351271U)
					{
						if (a_ == "IPM.Note")
						{
							return new PstMessage((co)ii);
						}
					}
				}
				else if (a_ == "IPM.Contact")
				{
					return new PstContact((fo)ii);
				}
			}
			else if (num != 2985546876U)
			{
				if (num == 3546511779U)
				{
					if (a_ == "IPM.Post.Rss")
					{
						return new PstRss((h5)ii);
					}
				}
			}
			else if (a_ == "IPM.Appointment")
			{
				return new PstAppointment((by)ii);
			}
			if (ii is co)
			{
				return new PstMessage((co)ii);
			}
			return new PstItem(ii);
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x000E69D4 File Offset: 0x000E59D4
		// (set) Token: 0x06003126 RID: 12582 RVA: 0x000E69E0 File Offset: 0x000E59E0
		[Obsolete("This property is obsolete. Use MailBee.Global.LicenseKey instead.")]
		public static string LicenseKey
		{
			get
			{
				return Resources.Instance.LicenseKeyIsWriteOnlyWarning;
			}
			set
			{
				Global.u = global::a.bn.a(value, typeof(PstReader));
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x000E69F7 File Offset: 0x000E59F7
		internal static global::a.bm License
		{
			get
			{
				return Global.u;
			}
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000E6A00 File Offset: 0x000E5A00
		internal static void a(string A_0)
		{
			try
			{
				Global.a(typeof(PstReader), A_0);
			}
			catch (MailBeeLicenseException ex)
			{
				try
				{
					Global.a(typeof(MsgConvert), A_0);
				}
				catch (MailBeeLicenseException)
				{
					throw ex;
				}
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000E6A54 File Offset: 0x000E5A54
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x0400204A RID: 8266
		private bs a;
	}
}
