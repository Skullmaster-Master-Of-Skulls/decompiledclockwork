using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ImportExportClassLibrary
{
	// Token: 0x02000007 RID: 7
	public class FtpClient
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003488 File Offset: 0x00002488
		public FtpClient()
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000034F0 File Offset: 0x000024F0
		public FtpClient(string server, string username, string password)
		{
			this.server = server;
			this.username = username;
			this.password = password;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000356C File Offset: 0x0000256C
		public FtpClient(string server, string username, string password, int timeoutSeconds, int port)
		{
			this.server = server;
			this.username = username;
			this.password = password;
			this.timeoutSeconds = timeoutSeconds;
			this.port = port;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000035F7 File Offset: 0x000025F7
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000035FF File Offset: 0x000025FF
		public bool VerboseDebugging
		{
			get
			{
				return this.verboseDebugging;
			}
			set
			{
				this.verboseDebugging = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003608 File Offset: 0x00002608
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00003610 File Offset: 0x00002610
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003619 File Offset: 0x00002619
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00003621 File Offset: 0x00002621
		public int Timeout
		{
			get
			{
				return this.timeoutSeconds;
			}
			set
			{
				this.timeoutSeconds = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000362A File Offset: 0x0000262A
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00003632 File Offset: 0x00002632
		public string Server
		{
			get
			{
				return this.server;
			}
			set
			{
				this.server = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000363B File Offset: 0x0000263B
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00003643 File Offset: 0x00002643
		public int RemotePort
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000364C File Offset: 0x0000264C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00003654 File Offset: 0x00002654
		public string RemotePath
		{
			get
			{
				return this.remotePath;
			}
			set
			{
				this.remotePath = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000365D File Offset: 0x0000265D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00003665 File Offset: 0x00002665
		public string Username
		{
			get
			{
				return this.username;
			}
			set
			{
				this.username = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000366E File Offset: 0x0000266E
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00003676 File Offset: 0x00002676
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000367F File Offset: 0x0000267F
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00003688 File Offset: 0x00002688
		public bool BinaryMode
		{
			get
			{
				return this.binMode;
			}
			set
			{
				if (this.binMode == value)
				{
					return;
				}
				if (value)
				{
					this.sendCommand("TYPE I");
				}
				else
				{
					this.sendCommand("TYPE A");
				}
				if (this.resultCode != 200)
				{
					throw new FtpClient.FtpException(this.result.Substring(4));
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000036DC File Offset: 0x000026DC
		public string Login()
		{
			string str = "";
			if (this.loggedin)
			{
				this.Close();
			}
			str = "Opening connection to " + this.server;
			try
			{
				this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IPAddress address = Dns.Resolve(this.server).AddressList[0];
				IPEndPoint remoteEP = new IPEndPoint(address, this.port);
				this.clientSocket.Connect(remoteEP);
			}
			catch (Exception ex)
			{
				str = str + Environment.NewLine + ex.ToString();
				if (this.clientSocket != null && this.clientSocket.Connected)
				{
					this.clientSocket.Close();
				}
				throw new FtpClient.FtpException("Couldn't connect to remote server", ex);
			}
			this.readResponse();
			if (this.resultCode != 220)
			{
				str = str + Environment.NewLine + "resultcode != 220";
				this.Close();
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
			this.sendCommand("USER " + this.username);
			if (this.resultCode != 331 && this.resultCode != 230)
			{
				this.cleanup();
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
			if (this.resultCode != 230)
			{
				this.sendCommand("PASS " + this.password);
				if (this.resultCode != 230 && this.resultCode != 202)
				{
					this.cleanup();
					throw new FtpClient.FtpException(this.result.Substring(4));
				}
			}
			this.loggedin = true;
			str = str + Environment.NewLine + "Connected to " + this.server;
			this.ChangeDir(this.remotePath);
			return str;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000038A4 File Offset: 0x000028A4
		public void Close()
		{
			if (this.clientSocket != null)
			{
				this.sendCommand("QUIT");
			}
			this.cleanup();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000038BF File Offset: 0x000028BF
		public string[] GetFileList()
		{
			return this.GetFileList("*.*");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000038CC File Offset: 0x000028CC
		public string[] GetFileList(string mask)
		{
			if (!this.loggedin)
			{
				this.Login();
			}
			Socket socket = this.createDataSocket();
			this.sendCommand("NLST " + mask);
			if (this.resultCode != 150 && this.resultCode != 125)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
			this.message = "";
			DateTime t = DateTime.Now.AddSeconds((double)this.timeoutSeconds);
			while (t > DateTime.Now)
			{
				int num = socket.Receive(this.buffer, this.buffer.Length, SocketFlags.None);
				this.message += FtpClient.ASCII.GetString(this.buffer, 0, num);
				if (num < this.buffer.Length)
				{
					break;
				}
			}
			string[] array = this.message.Replace("\r", "").Split(new char[]
			{
				'\n'
			});
			socket.Close();
			if (this.message.IndexOf("No such file or directory") != -1)
			{
				array = new string[0];
			}
			this.readResponse();
			if (this.resultCode != 226)
			{
				array = new string[0];
			}
			return array;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003A04 File Offset: 0x00002A04
		public long GetFileSize(string fileName)
		{
			if (!this.loggedin)
			{
				this.Login();
			}
			this.sendCommand("SIZE " + fileName);
			if (this.resultCode == 213)
			{
				return long.Parse(this.result.Substring(4));
			}
			throw new FtpClient.FtpException(this.result.Substring(4));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003A68 File Offset: 0x00002A68
		public void Download(string remFileName)
		{
			this.Download(remFileName, "", false);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003A77 File Offset: 0x00002A77
		public void Download(string remFileName, bool resume)
		{
			this.Download(remFileName, "", resume);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003A86 File Offset: 0x00002A86
		public void Download(string remFileName, string locFileName)
		{
			this.Download(remFileName, locFileName, false);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003A94 File Offset: 0x00002A94
		public void Download(string remFileName, string locFileName, bool resume)
		{
			if (!this.loggedin)
			{
				this.Login();
			}
			this.BinaryMode = true;
			if (locFileName.Equals(""))
			{
				locFileName = remFileName;
			}
			FileStream fileStream;
			if (!File.Exists(locFileName))
			{
				fileStream = File.Create(locFileName);
			}
			else
			{
				fileStream = new FileStream(locFileName, FileMode.Open);
			}
			Socket socket = this.createDataSocket();
			if (resume)
			{
				long length = fileStream.Length;
				if (length > 0L)
				{
					this.sendCommand("REST " + length);
					if (this.resultCode == 350)
					{
						fileStream.Seek(length, SeekOrigin.Begin);
					}
				}
			}
			this.sendCommand("RETR " + remFileName);
			if (this.resultCode != 150 && this.resultCode != 125)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
			DateTime t = DateTime.Now.AddSeconds((double)this.timeoutSeconds);
			while (t > DateTime.Now)
			{
				this.bytes = socket.Receive(this.buffer, this.buffer.Length, SocketFlags.None);
				fileStream.Write(this.buffer, 0, this.bytes);
				if (this.bytes <= 0)
				{
					break;
				}
			}
			fileStream.Close();
			if (socket.Connected)
			{
				socket.Close();
			}
			this.readResponse();
			if (this.resultCode != 226 && this.resultCode != 250)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003C08 File Offset: 0x00002C08
		public void Upload(string sourceFileName, string destFileName)
		{
			this.Upload(sourceFileName, destFileName, false);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003C14 File Offset: 0x00002C14
		public void Upload(string sourceFileName, string destFileName, bool resume)
		{
			if (!this.loggedin)
			{
				this.Login();
			}
			FileStream fileStream = new FileStream(sourceFileName, FileMode.Open);
			Socket socket = this.createDataSocket();
			this.sendCommand("TYPE I");
			string fileName = Path.GetFileName(destFileName);
			this.sendCommand("STOR " + fileName);
			if (this.resultCode != 125 && this.resultCode != 150)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
			while ((this.bytes = fileStream.Read(this.buffer, 0, this.buffer.Length)) > 0)
			{
				socket.Send(this.buffer, this.bytes, SocketFlags.None);
			}
			fileStream.Close();
			if (socket.Connected)
			{
				socket.Close();
			}
			this.readResponse();
			if (this.resultCode != 226 && this.resultCode != 250)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003D08 File Offset: 0x00002D08
		public void UploadDirectory(string path, bool recurse)
		{
			this.UploadDirectory(path, recurse, "*.*");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003D18 File Offset: 0x00002D18
		public void UploadDirectory(string path, bool recurse, string mask)
		{
			string[] array = path.Replace("/", "\\").Split(new char[]
			{
				'\\'
			});
			string text = array[array.Length - 1];
			if (this.GetFileList(text).Length < 1)
			{
				this.MakeDir(text);
			}
			this.ChangeDir(text);
			foreach (string text2 in Directory.GetFiles(path, mask))
			{
				this.Upload(text2, Path.GetFileName(text2), true);
			}
			if (recurse)
			{
				foreach (string path2 in Directory.GetDirectories(path))
				{
					this.UploadDirectory(path2, recurse, mask);
				}
			}
			this.ChangeDir("..");
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003DD8 File Offset: 0x00002DD8
		public void DeleteFile(string fileName)
		{
			if (!this.loggedin)
			{
				this.Login();
			}
			this.sendCommand("DELE " + fileName);
			if (this.resultCode != 250)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003E24 File Offset: 0x00002E24
		public void RenameFile(string oldFileName, string newFileName, bool overwrite)
		{
			if (!this.loggedin)
			{
				this.Login();
			}
			this.sendCommand("RNFR " + oldFileName);
			if (this.resultCode != 350)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
			if (!overwrite && this.GetFileList(newFileName).Length > 0)
			{
				throw new FtpClient.FtpException("File already exists");
			}
			this.sendCommand("RNTO " + newFileName);
			if (this.resultCode != 250)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003EBC File Offset: 0x00002EBC
		public void MakeDir(string dirName)
		{
			if (!this.loggedin)
			{
				this.Login();
			}
			this.sendCommand("MKD " + dirName);
			if (this.resultCode != 250 && this.resultCode != 257)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003F18 File Offset: 0x00002F18
		public void RemoveDir(string dirName)
		{
			if (!this.loggedin)
			{
				this.Login();
			}
			this.sendCommand("RMD " + dirName);
			if (this.resultCode != 250)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003F64 File Offset: 0x00002F64
		public string ChangeDir(string dirName)
		{
			string str = null;
			if (dirName == null || dirName.Equals(".") || dirName.Length == 0)
			{
				return "missing directory name!";
			}
			if (!this.loggedin)
			{
				str = this.Login();
			}
			this.sendCommand("CWD " + dirName);
			if (this.resultCode != 250)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
			this.sendCommand("PWD");
			if (this.resultCode != 257)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
			this.remotePath = this.message.Split(new char[]
			{
				'"'
			})[1];
			return str + Environment.NewLine + "Current directory is " + this.remotePath;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004034 File Offset: 0x00003034
		public int ChangeDir2(string dirName)
		{
			if (dirName == null || dirName.Equals(".") || dirName.Length == 0)
			{
				return -1;
			}
			if (!this.loggedin)
			{
				this.Login();
			}
			this.sendCommand("CWD " + dirName);
			int num = this.resultCode;
			this.sendCommand("PWD");
			this.remotePath = this.message.Split(new char[]
			{
				'"'
			})[1];
			return num;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000040B0 File Offset: 0x000030B0
		private void readResponse()
		{
			this.message = "";
			this.result = this.readLine();
			if (this.result.Length > 3)
			{
				this.resultCode = int.Parse(this.result.Substring(0, 3));
				return;
			}
			this.result = null;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004104 File Offset: 0x00003104
		private string readLine()
		{
			do
			{
				this.bytes = this.clientSocket.Receive(this.buffer, this.buffer.Length, SocketFlags.None);
				this.message += FtpClient.ASCII.GetString(this.buffer, 0, this.bytes);
			}
			while (this.bytes >= this.buffer.Length);
			string[] array = this.message.Split(new char[]
			{
				'\n'
			});
			if (this.message.Length > 2)
			{
				this.message = array[array.Length - 2];
			}
			else
			{
				this.message = array[0];
			}
			if (this.message.Length > 4 && !this.message.Substring(3, 1).Equals(" "))
			{
				return this.readLine();
			}
			if (this.verboseDebugging)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
				}
			}
			return this.message;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000041F4 File Offset: 0x000031F4
		private void sendCommand(string command)
		{
			bool flag = this.verboseDebugging;
			byte[] array = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
			this.clientSocket.Send(array, array.Length, SocketFlags.None);
			this.readResponse();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000423C File Offset: 0x0000323C
		private Socket createDataSocket()
		{
			this.sendCommand("PASV");
			if (this.resultCode != 227)
			{
				throw new FtpClient.FtpException(this.result.Substring(4));
			}
			int num = this.result.IndexOf('(');
			int num2 = this.result.IndexOf(')');
			string text = this.result.Substring(num + 1, num2 - num - 1);
			int[] array = new int[6];
			int length = text.Length;
			int num3 = 0;
			string text2 = "";
			int num4 = 0;
			while (num4 < length && num3 <= 6)
			{
				char c = char.Parse(text.Substring(num4, 1));
				if (char.IsDigit(c))
				{
					text2 += c;
				}
				else if (c != ',')
				{
					throw new FtpClient.FtpException("Malformed PASV result: " + this.result);
				}
				if (c == ',')
				{
					goto IL_D0;
				}
				if (num4 + 1 == length)
				{
					goto Block_5;
				}
				IL_103:
				num4++;
				continue;
				Block_5:
				try
				{
					IL_D0:
					array[num3++] = int.Parse(text2);
					text2 = "";
				}
				catch (Exception innerException)
				{
					throw new FtpClient.FtpException("Malformed PASV result (not supported?): " + this.result, innerException);
				}
				goto IL_103;
			}
			string hostName = string.Concat(new object[]
			{
				array[0],
				".",
				array[1],
				".",
				array[2],
				".",
				array[3]
			});
			int num5 = (array[4] << 8) + array[5];
			Socket socket = null;
			try
			{
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IPEndPoint remoteEP = new IPEndPoint(Dns.Resolve(hostName).AddressList[0], num5);
				socket.Connect(remoteEP);
			}
			catch (Exception innerException2)
			{
				if (socket != null && socket.Connected)
				{
					socket.Close();
				}
				throw new FtpClient.FtpException("Can't connect to remote server", innerException2);
			}
			return socket;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000443C File Offset: 0x0000343C
		private void cleanup()
		{
			if (this.clientSocket != null)
			{
				this.clientSocket.Close();
				this.clientSocket = null;
			}
			this.loggedin = false;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004460 File Offset: 0x00003460
		~FtpClient()
		{
			this.cleanup();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000448C File Offset: 0x0000348C
		public IAsyncResult BeginLogin(AsyncCallback callback)
		{
			FtpClient.LoginCallback loginCallback = new FtpClient.LoginCallback(this.Login);
			return loginCallback.BeginInvoke(callback, null);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000044B0 File Offset: 0x000034B0
		public IAsyncResult BeginClose(AsyncCallback callback)
		{
			FtpClient.CloseCallback closeCallback = new FtpClient.CloseCallback(this.Close);
			return closeCallback.BeginInvoke(callback, null);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000044D4 File Offset: 0x000034D4
		public IAsyncResult BeginGetFileList(AsyncCallback callback)
		{
			FtpClient.GetFileListCallback getFileListCallback = new FtpClient.GetFileListCallback(this.GetFileList);
			return getFileListCallback.BeginInvoke(callback, null);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000044F8 File Offset: 0x000034F8
		public IAsyncResult BeginGetFileList(string mask, AsyncCallback callback)
		{
			FtpClient.GetFileListMaskCallback getFileListMaskCallback = new FtpClient.GetFileListMaskCallback(this.GetFileList);
			return getFileListMaskCallback.BeginInvoke(mask, callback, null);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000451C File Offset: 0x0000351C
		public IAsyncResult BeginGetFileSize(string fileName, AsyncCallback callback)
		{
			FtpClient.GetFileSizeCallback getFileSizeCallback = new FtpClient.GetFileSizeCallback(this.GetFileSize);
			return getFileSizeCallback.BeginInvoke(fileName, callback, null);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004540 File Offset: 0x00003540
		public IAsyncResult BeginDownload(string remFileName, AsyncCallback callback)
		{
			FtpClient.DownloadCallback downloadCallback = new FtpClient.DownloadCallback(this.Download);
			return downloadCallback.BeginInvoke(remFileName, callback, null);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004564 File Offset: 0x00003564
		public IAsyncResult BeginDownload(string remFileName, bool resume, AsyncCallback callback)
		{
			FtpClient.DownloadFileNameResumeCallback downloadFileNameResumeCallback = new FtpClient.DownloadFileNameResumeCallback(this.Download);
			return downloadFileNameResumeCallback.BeginInvoke(remFileName, resume, callback, null);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004588 File Offset: 0x00003588
		public IAsyncResult BeginDownload(string remFileName, string locFileName, AsyncCallback callback)
		{
			FtpClient.DownloadFileNameFileNameCallback downloadFileNameFileNameCallback = new FtpClient.DownloadFileNameFileNameCallback(this.Download);
			return downloadFileNameFileNameCallback.BeginInvoke(remFileName, locFileName, callback, null);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000045AC File Offset: 0x000035AC
		public IAsyncResult BeginDownload(string remFileName, string locFileName, bool resume, AsyncCallback callback)
		{
			FtpClient.DownloadFileNameFileNameResumeCallback downloadFileNameFileNameResumeCallback = new FtpClient.DownloadFileNameFileNameResumeCallback(this.Download);
			return downloadFileNameFileNameResumeCallback.BeginInvoke(remFileName, locFileName, resume, callback, null);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000045D4 File Offset: 0x000035D4
		public IAsyncResult BeginUpload(string sourceFileName, string destFileName, AsyncCallback callback)
		{
			FtpClient.UploadCallback uploadCallback = new FtpClient.UploadCallback(this.Upload);
			return uploadCallback.BeginInvoke(sourceFileName, destFileName, callback, null);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000045F8 File Offset: 0x000035F8
		public IAsyncResult BeginUpload(string sourceFileName, string destFileName, bool resume, AsyncCallback callback)
		{
			FtpClient.UploadFileNameResumeCallback uploadFileNameResumeCallback = new FtpClient.UploadFileNameResumeCallback(this.Upload);
			return uploadFileNameResumeCallback.BeginInvoke(sourceFileName, destFileName, resume, callback, null);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004620 File Offset: 0x00003620
		public IAsyncResult BeginUploadDirectory(string path, bool recurse, AsyncCallback callback)
		{
			FtpClient.UploadDirectoryCallback uploadDirectoryCallback = new FtpClient.UploadDirectoryCallback(this.UploadDirectory);
			return uploadDirectoryCallback.BeginInvoke(path, recurse, callback, null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004644 File Offset: 0x00003644
		public IAsyncResult BeginUploadDirectory(string path, bool recurse, string mask, AsyncCallback callback)
		{
			FtpClient.UploadDirectoryPathRecurseMaskCallback uploadDirectoryPathRecurseMaskCallback = new FtpClient.UploadDirectoryPathRecurseMaskCallback(this.UploadDirectory);
			return uploadDirectoryPathRecurseMaskCallback.BeginInvoke(path, recurse, mask, callback, null);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000466C File Offset: 0x0000366C
		public IAsyncResult BeginDeleteFile(string fileName, AsyncCallback callback)
		{
			FtpClient.DeleteFileCallback deleteFileCallback = new FtpClient.DeleteFileCallback(this.DeleteFile);
			return deleteFileCallback.BeginInvoke(fileName, callback, null);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004690 File Offset: 0x00003690
		public IAsyncResult BeginRenameFile(string oldFileName, string newFileName, bool overwrite, AsyncCallback callback)
		{
			FtpClient.RenameFileCallback renameFileCallback = new FtpClient.RenameFileCallback(this.RenameFile);
			return renameFileCallback.BeginInvoke(oldFileName, newFileName, overwrite, callback, null);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000046B8 File Offset: 0x000036B8
		public IAsyncResult BeginMakeDir(string dirName, AsyncCallback callback)
		{
			FtpClient.MakeDirCallback makeDirCallback = new FtpClient.MakeDirCallback(this.MakeDir);
			return makeDirCallback.BeginInvoke(dirName, callback, null);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000046DC File Offset: 0x000036DC
		public IAsyncResult BeginRemoveDir(string dirName, AsyncCallback callback)
		{
			FtpClient.RemoveDirCallback removeDirCallback = new FtpClient.RemoveDirCallback(this.RemoveDir);
			return removeDirCallback.BeginInvoke(dirName, callback, null);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004700 File Offset: 0x00003700
		public IAsyncResult BeginChangeDir(string dirName, AsyncCallback callback)
		{
			FtpClient.ChangeDirCallback changeDirCallback = new FtpClient.ChangeDirCallback(this.ChangeDir);
			return changeDirCallback.BeginInvoke(dirName, callback, null);
		}

		// Token: 0x04000017 RID: 23
		private static int BUFFER_SIZE = 512;

		// Token: 0x04000018 RID: 24
		private static Encoding ASCII = Encoding.ASCII;

		// Token: 0x04000019 RID: 25
		private bool verboseDebugging;

		// Token: 0x0400001A RID: 26
		private string server = "localhost";

		// Token: 0x0400001B RID: 27
		private string remotePath = ".";

		// Token: 0x0400001C RID: 28
		private string username = "anonymous";

		// Token: 0x0400001D RID: 29
		private string password = "anonymous@anonymous.net";

		// Token: 0x0400001E RID: 30
		private string message;

		// Token: 0x0400001F RID: 31
		private string result;

		// Token: 0x04000020 RID: 32
		private int port = 21;

		// Token: 0x04000021 RID: 33
		private int bytes;

		// Token: 0x04000022 RID: 34
		private int resultCode;

		// Token: 0x04000023 RID: 35
		private bool loggedin;

		// Token: 0x04000024 RID: 36
		private bool binMode = true;

		// Token: 0x04000025 RID: 37
		private byte[] buffer = new byte[FtpClient.BUFFER_SIZE];

		// Token: 0x04000026 RID: 38
		private Socket clientSocket;

		// Token: 0x04000027 RID: 39
		private int timeoutSeconds = 10;

		// Token: 0x02000008 RID: 8
		public class FtpException : Exception
		{
			// Token: 0x06000077 RID: 119 RVA: 0x00004739 File Offset: 0x00003739
			public FtpException(string message) : base(message)
			{
			}

			// Token: 0x06000078 RID: 120 RVA: 0x00004742 File Offset: 0x00003742
			public FtpException(string message, Exception innerException) : base(message, innerException)
			{
			}
		}

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x0600007A RID: 122
		private delegate string LoginCallback();

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x0600007E RID: 126
		private delegate void CloseCallback();

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000082 RID: 130
		private delegate string[] GetFileListCallback();

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x06000086 RID: 134
		private delegate string[] GetFileListMaskCallback(string mask);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x0600008A RID: 138
		private delegate long GetFileSizeCallback(string fileName);

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x0600008E RID: 142
		private delegate void DownloadCallback(string remFileName);

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x06000092 RID: 146
		private delegate void DownloadFileNameResumeCallback(string remFileName, bool resume);

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x06000096 RID: 150
		private delegate void DownloadFileNameFileNameCallback(string remFileName, string locFileName);

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x0600009A RID: 154
		private delegate void DownloadFileNameFileNameResumeCallback(string remFileName, string locFileName, bool resume);

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x0600009E RID: 158
		private delegate void UploadCallback(string sourceFileName, string destFileName);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x060000A2 RID: 162
		private delegate void UploadFileNameResumeCallback(string sourceFileName, string destFileName, bool resume);

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x060000A6 RID: 166
		private delegate void UploadDirectoryCallback(string path, bool recurse);

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x060000AA RID: 170
		private delegate void UploadDirectoryPathRecurseMaskCallback(string path, bool recurse, string mask);

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x060000AE RID: 174
		private delegate void DeleteFileCallback(string fileName);

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x060000B2 RID: 178
		private delegate void RenameFileCallback(string oldFileName, string newFileName, bool overwrite);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x060000B6 RID: 182
		private delegate void MakeDirCallback(string dirName);

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x060000BA RID: 186
		private delegate void RemoveDirCallback(string dirName);

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x060000BE RID: 190
		private delegate string ChangeDirCallback(string dirName);
	}
}
