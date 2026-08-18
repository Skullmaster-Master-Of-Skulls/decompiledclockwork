using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

namespace EmailClassLibrary
{
	// Token: 0x02000008 RID: 8
	public class MAPI
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002A37 File Offset: 0x00001A37
		public bool AddRecipientTo(string email)
		{
			return this.AddRecipient(email, MAPI.HowTo.MAPI_TO);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A41 File Offset: 0x00001A41
		public bool AddRecipientCC(string email)
		{
			return this.AddRecipient(email, MAPI.HowTo.MAPI_CC);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A4B File Offset: 0x00001A4B
		public bool AddRecipientBCC(string email)
		{
			return this.AddRecipient(email, MAPI.HowTo.MAPI_BCC);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A55 File Offset: 0x00001A55
		public void AddAttachment(string strAttachmentFileName)
		{
			this.m_attachments.Add(strAttachmentFileName);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A64 File Offset: 0x00001A64
		public int SendMailPopup(string strSubject, string strBody, out string errmsg)
		{
			return this.SendMail(strSubject, strBody, 9, out errmsg);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A71 File Offset: 0x00001A71
		public int SendMailDirect(string strSubject, string strBody, out string errmsg)
		{
			return this.SendMail(strSubject, strBody, 1, out errmsg);
		}

		// Token: 0x0600003C RID: 60
		[DllImport("MAPI32.DLL")]
		private static extern int MAPISendMail(IntPtr sess, IntPtr hwnd, MapiMessage message, int flg, int rsv);

		// Token: 0x0600003D RID: 61 RVA: 0x00002A80 File Offset: 0x00001A80
		private int SendMail(string strSubject, string strBody, int how, out string errmsg)
		{
			MapiMessage mapiMessage = new MapiMessage();
			mapiMessage.subject = strSubject;
			mapiMessage.noteText = strBody;
			mapiMessage.recips = this.GetRecipients(out mapiMessage.recipCount);
			mapiMessage.files = this.GetAttachments(out mapiMessage.fileCount);
			this.m_lastError = MAPI.MAPISendMail(new IntPtr(0), new IntPtr(0), mapiMessage, how, 0);
			if (this.m_lastError > 1)
			{
				errmsg = "MAPISendMail failed! " + this.GetLastError();
			}
			else
			{
				errmsg = "";
			}
			this.Cleanup(ref mapiMessage);
			return this.m_lastError;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B14 File Offset: 0x00001B14
		private bool AddRecipient(string email, MAPI.HowTo howTo)
		{
			MapiRecipDesc mapiRecipDesc = new MapiRecipDesc();
			mapiRecipDesc.recipClass = (int)howTo;
			mapiRecipDesc.name = email;
			this.m_recipients.Add(mapiRecipDesc);
			return true;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B44 File Offset: 0x00001B44
		private IntPtr GetRecipients(out int recipCount)
		{
			recipCount = 0;
			if (this.m_recipients.Count == 0)
			{
				return IntPtr.Zero;
			}
			int num = Marshal.SizeOf(typeof(MapiRecipDesc));
			IntPtr intPtr = Marshal.AllocHGlobal(this.m_recipients.Count * num);
			int num2 = (int)intPtr;
			foreach (object obj in this.m_recipients)
			{
				MapiRecipDesc structure = (MapiRecipDesc)obj;
				Marshal.StructureToPtr<MapiRecipDesc>(structure, (IntPtr)num2, false);
				num2 += num;
			}
			recipCount = this.m_recipients.Count;
			return intPtr;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BFC File Offset: 0x00001BFC
		private IntPtr GetAttachments(out int fileCount)
		{
			fileCount = 0;
			if (this.m_attachments == null)
			{
				return IntPtr.Zero;
			}
			if (this.m_attachments.Count <= 0 || this.m_attachments.Count > 20)
			{
				return IntPtr.Zero;
			}
			int num = Marshal.SizeOf(typeof(MapiFileDesc));
			IntPtr intPtr = Marshal.AllocHGlobal(this.m_attachments.Count * num);
			MapiFileDesc mapiFileDesc = new MapiFileDesc();
			mapiFileDesc.position = -1;
			int num2 = (int)intPtr;
			foreach (object obj in this.m_attachments)
			{
				string path = (string)obj;
				mapiFileDesc.name = Path.GetFileName(path);
				mapiFileDesc.path = path;
				Marshal.StructureToPtr<MapiFileDesc>(mapiFileDesc, (IntPtr)num2, false);
				num2 += num;
			}
			fileCount = this.m_attachments.Count;
			return intPtr;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CF8 File Offset: 0x00001CF8
		private void Cleanup(ref MapiMessage msg)
		{
			int num = Marshal.SizeOf(typeof(MapiRecipDesc));
			if (msg.recips != IntPtr.Zero)
			{
				int num2 = (int)msg.recips;
				for (int i = 0; i < msg.recipCount; i++)
				{
					Marshal.DestroyStructure((IntPtr)num2, typeof(MapiRecipDesc));
					num2 += num;
				}
				Marshal.FreeHGlobal(msg.recips);
			}
			if (msg.files != IntPtr.Zero)
			{
				num = Marshal.SizeOf(typeof(MapiFileDesc));
				int num2 = (int)msg.files;
				for (int j = 0; j < msg.fileCount; j++)
				{
					Marshal.DestroyStructure((IntPtr)num2, typeof(MapiFileDesc));
					num2 += num;
				}
				Marshal.FreeHGlobal(msg.files);
			}
			this.m_recipients.Clear();
			this.m_attachments.Clear();
			this.m_lastError = 0;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002DF2 File Offset: 0x00001DF2
		public string GetLastError()
		{
			if (this.m_lastError <= 26)
			{
				return this.errors[this.m_lastError];
			}
			return "MAPI error [" + this.m_lastError.ToString() + "]";
		}

		// Token: 0x04000018 RID: 24
		private readonly string[] errors = new string[]
		{
			"OK [0]",
			"User abort [1]",
			"General MAPI failure [2]",
			"MAPI login failure [3]",
			"Disk full [4]",
			"Insufficient memory [5]",
			"Access denied [6]",
			"-unknown- [7]",
			"Too many sessions [8]",
			"Too many files were specified [9]",
			"Too many recipients were specified [10]",
			"A specified attachment was not found [11]",
			"Attachment open failure [12]",
			"Attachment write failure [13]",
			"Unknown recipient [14]",
			"Bad recipient type [15]",
			"No messages [16]",
			"Invalid message [17]",
			"Text too large [18]",
			"Invalid session [19]",
			"Type not supported [20]",
			"A recipient was specified ambiguously [21]",
			"Message in use [22]",
			"Network failure [23]",
			"Invalid edit fields [24]",
			"Invalid recipients [25]",
			"Not supported [26]"
		};

		// Token: 0x04000019 RID: 25
		private ArrayList m_recipients = new ArrayList();

		// Token: 0x0400001A RID: 26
		private ArrayList m_attachments = new ArrayList();

		// Token: 0x0400001B RID: 27
		private int m_lastError;

		// Token: 0x0400001C RID: 28
		private const int MAPI_LOGON_UI = 1;

		// Token: 0x0400001D RID: 29
		private const int MAPI_DIALOG = 8;

		// Token: 0x0400001E RID: 30
		private const int maxAttachments = 20;

		// Token: 0x0200000E RID: 14
		private enum HowTo
		{
			// Token: 0x04000040 RID: 64
			MAPI_ORIG,
			// Token: 0x04000041 RID: 65
			MAPI_TO,
			// Token: 0x04000042 RID: 66
			MAPI_CC,
			// Token: 0x04000043 RID: 67
			MAPI_BCC
		}
	}
}
