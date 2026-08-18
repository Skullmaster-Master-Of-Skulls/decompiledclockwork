using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x02000023 RID: 35
	public class ScpClient : BaseClient
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000061A4 File Offset: 0x000043A4
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000061AC File Offset: 0x000043AC
		public TimeSpan OperationTimeout { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000061B5 File Offset: 0x000043B5
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000061BD File Offset: 0x000043BD
		public uint BufferSize { get; set; }

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060001A9 RID: 425 RVA: 0x000061C8 File Offset: 0x000043C8
		// (remove) Token: 0x060001AA RID: 426 RVA: 0x00006200 File Offset: 0x00004400
		public event EventHandler<ScpDownloadEventArgs> Downloading;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060001AB RID: 427 RVA: 0x00006238 File Offset: 0x00004438
		// (remove) Token: 0x060001AC RID: 428 RVA: 0x00006270 File Offset: 0x00004470
		public event EventHandler<ScpUploadEventArgs> Uploading;

		// Token: 0x060001AD RID: 429 RVA: 0x000062A5 File Offset: 0x000044A5
		public ScpClient(ConnectionInfo connectionInfo) : this(connectionInfo, false)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000062AF File Offset: 0x000044AF
		public ScpClient(string host, int port, string username, string password) : this(new PasswordConnectionInfo(host, port, username, password), true)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000062C2 File Offset: 0x000044C2
		public ScpClient(string host, string username, string password) : this(host, ConnectionInfo.DefaultPort, username, password)
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000062D2 File Offset: 0x000044D2
		public ScpClient(string host, int port, string username, params PrivateKeyFile[] keyFiles) : this(new PrivateKeyConnectionInfo(host, port, username, keyFiles), true)
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000062E5 File Offset: 0x000044E5
		public ScpClient(string host, string username, params PrivateKeyFile[] keyFiles) : this(host, ConnectionInfo.DefaultPort, username, keyFiles)
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000062F5 File Offset: 0x000044F5
		private ScpClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo) : this(connectionInfo, ownsConnectionInfo, new ServiceFactory())
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006304 File Offset: 0x00004504
		internal ScpClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo, IServiceFactory serviceFactory) : base(connectionInfo, ownsConnectionInfo, serviceFactory)
		{
			this.OperationTimeout = Renci.SshNet.Session.InfiniteTimeSpan;
			this.BufferSize = 16384U;
			if (ScpClient._byteToChar == null)
			{
				ScpClient._byteToChar = new char[128];
				char c = '\0';
				for (int i = 0; i < 128; i++)
				{
					char[] byteToChar = ScpClient._byteToChar;
					int num = i;
					char c2 = c;
					c = c2 + '\u0001';
					byteToChar[num] = c2;
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006368 File Offset: 0x00004568
		public void Upload(Stream source, string path)
		{
			using (PipeStream input = base.ServiceFactory.CreatePipeStream())
			{
				using (IChannelSession channelSession = base.Session.CreateChannelSession())
				{
					channelSession.DataReceived += delegate(object sender, ChannelDataEventArgs e)
					{
						input.Write(e.Data, 0, e.Data.Length);
					};
					channelSession.Open();
					int num = path.LastIndexOfAny(new char[]
					{
						'\\',
						'/'
					});
					if (num != -1)
					{
						string arg = path.Substring(0, num);
						string text = path.Substring(num + 1);
						channelSession.SendExecRequest(string.Format("scp -t \"{0}\"", arg));
						ScpClient.CheckReturnCode(input);
						path = text;
					}
					this.InternalUpload(channelSession, input, source, path);
					channelSession.Close();
				}
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006450 File Offset: 0x00004650
		public void Download(string filename, Stream destination)
		{
			if (filename.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("filename");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			using (PipeStream input = base.ServiceFactory.CreatePipeStream())
			{
				using (IChannelSession channelSession = base.Session.CreateChannelSession())
				{
					channelSession.DataReceived += delegate(object sender, ChannelDataEventArgs e)
					{
						input.Write(e.Data, 0, e.Data.Length);
					};
					channelSession.Open();
					channelSession.SendExecRequest(string.Format("scp -f \"{0}\"", filename));
					ScpClient.SendConfirmation(channelSession);
					string text = ScpClient.ReadString(input);
					Match match = ScpClient.FileInfoRe.Match(text);
					if (match.Success)
					{
						ScpClient.SendConfirmation(channelSession);
						match.Result("${mode}");
						long length = long.Parse(match.Result("${length}"));
						string filename2 = match.Result("${filename}");
						this.InternalDownload(channelSession, input, destination, filename2, length);
					}
					else
					{
						ScpClient.SendConfirmation(channelSession, 1, string.Format("\"{0}\" is not valid protocol message.", text));
					}
					channelSession.Close();
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000658C File Offset: 0x0000478C
		private static void InternalSetTimestamp(IChannelSession channel, Stream input, DateTime lastWriteTime, DateTime lastAccessime)
		{
			DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			long num = (long)(lastWriteTime - d).TotalSeconds;
			long num2 = (long)(lastAccessime - d).TotalSeconds;
			ScpClient.SendData(channel, string.Format("T{0} 0 {1} 0\n", num, num2));
			ScpClient.CheckReturnCode(input);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000065F0 File Offset: 0x000047F0
		private void InternalUpload(IChannelSession channel, Stream input, Stream source, string filename)
		{
			long length = source.Length;
			ScpClient.SendData(channel, string.Format("C0644 {0} {1}\n", length, Path.GetFileName(filename)));
			ScpClient.CheckReturnCode(input);
			byte[] array = new byte[this.BufferSize];
			int i = source.Read(array, 0, array.Length);
			long num = 0L;
			while (i > 0)
			{
				ScpClient.SendData(channel, array, i);
				num += (long)i;
				this.RaiseUploadingEvent(filename, length, num);
				i = source.Read(array, 0, array.Length);
			}
			ScpClient.SendConfirmation(channel);
			ScpClient.CheckReturnCode(input);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006678 File Offset: 0x00004878
		private void InternalDownload(IChannel channel, Stream input, Stream output, string filename, long length)
		{
			byte[] buffer = new byte[Math.Min(length, (long)((ulong)this.BufferSize))];
			long num = length;
			do
			{
				int num2 = input.Read(buffer, 0, (int)Math.Min(num, (long)((ulong)this.BufferSize)));
				output.Write(buffer, 0, num2);
				this.RaiseDownloadingEvent(filename, length, length - num);
				num -= (long)num2;
			}
			while (num > 0L);
			output.Flush();
			this.RaiseDownloadingEvent(filename, length, length - num);
			ScpClient.SendConfirmation(channel);
			ScpClient.CheckReturnCode(input);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000066F5 File Offset: 0x000048F5
		private void RaiseDownloadingEvent(string filename, long size, long downloaded)
		{
			if (this.Downloading != null)
			{
				this.Downloading(this, new ScpDownloadEventArgs(filename, size, downloaded));
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006713 File Offset: 0x00004913
		private void RaiseUploadingEvent(string filename, long size, long uploaded)
		{
			if (this.Uploading != null)
			{
				this.Uploading(this, new ScpUploadEventArgs(filename, size, uploaded));
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006731 File Offset: 0x00004931
		private static void SendConfirmation(IChannel channel)
		{
			ScpClient.SendData(channel, new byte[1]);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000673F File Offset: 0x0000493F
		private static void SendConfirmation(IChannel channel, byte errorCode, string message)
		{
			ScpClient.SendData(channel, new byte[]
			{
				errorCode
			});
			ScpClient.SendData(channel, string.Format("{0}\n", message));
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006762 File Offset: 0x00004962
		private static void CheckReturnCode(Stream input)
		{
			if (ScpClient.ReadByte(input) > 0)
			{
				throw new ScpException(ScpClient.ReadString(input));
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006779 File Offset: 0x00004979
		private static void SendData(IChannel channel, string command)
		{
			channel.SendData(SshData.Utf8.GetBytes(command));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000678C File Offset: 0x0000498C
		private static void SendData(IChannel channel, byte[] buffer, int length)
		{
			channel.SendData(buffer, 0, length);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006797 File Offset: 0x00004997
		private static void SendData(IChannel channel, byte[] buffer)
		{
			channel.SendData(buffer);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000067A0 File Offset: 0x000049A0
		private static int ReadByte(Stream stream)
		{
			int num = stream.ReadByte();
			if (num == -1)
			{
				throw new SshException("Stream has been closed.");
			}
			return num;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000067B8 File Offset: 0x000049B8
		private static string ReadString(Stream stream)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int num = ScpClient.ReadByte(stream);
			if (num == 1 || num == 2)
			{
				flag = true;
				num = ScpClient.ReadByte(stream);
			}
			for (char c = ScpClient._byteToChar[num]; c != '\n'; c = ScpClient._byteToChar[num])
			{
				stringBuilder.Append(c);
				num = ScpClient.ReadByte(stream);
			}
			if (flag)
			{
				throw new ScpException(stringBuilder.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006820 File Offset: 0x00004A20
		public void Upload(FileInfo fileInfo, string path)
		{
			if (fileInfo == null)
			{
				throw new ArgumentNullException("fileInfo");
			}
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException("path");
			}
			using (PipeStream input = base.ServiceFactory.CreatePipeStream())
			{
				using (IChannelSession channelSession = base.Session.CreateChannelSession())
				{
					channelSession.DataReceived += delegate(object sender, ChannelDataEventArgs e)
					{
						input.Write(e.Data, 0, e.Data.Length);
					};
					channelSession.Open();
					if (!channelSession.SendExecRequest(string.Format("scp -t \"{0}\"", path)))
					{
						throw new SshException("Secure copy execution request was rejected by the server. Please consult the server logs.");
					}
					ScpClient.CheckReturnCode(input);
					this.InternalUpload(channelSession, input, fileInfo, fileInfo.Name);
					channelSession.Close();
				}
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006908 File Offset: 0x00004B08
		public void Upload(DirectoryInfo directoryInfo, string path)
		{
			if (directoryInfo == null)
			{
				throw new ArgumentNullException("directoryInfo");
			}
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException("path");
			}
			using (PipeStream input = base.ServiceFactory.CreatePipeStream())
			{
				using (IChannelSession channelSession = base.Session.CreateChannelSession())
				{
					channelSession.DataReceived += delegate(object sender, ChannelDataEventArgs e)
					{
						input.Write(e.Data, 0, e.Data.Length);
					};
					channelSession.Open();
					channelSession.SendExecRequest(string.Format("scp -rt \"{0}\"", path));
					ScpClient.CheckReturnCode(input);
					ScpClient.InternalSetTimestamp(channelSession, input, directoryInfo.LastWriteTimeUtc, directoryInfo.LastAccessTimeUtc);
					ScpClient.SendData(channelSession, string.Format("D0755 0 {0}\n", Path.GetFileName(path)));
					ScpClient.CheckReturnCode(input);
					this.InternalUpload(channelSession, input, directoryInfo);
					ScpClient.SendData(channelSession, "E\n");
					ScpClient.CheckReturnCode(input);
					channelSession.Close();
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006A30 File Offset: 0x00004C30
		public void Download(string filename, FileInfo fileInfo)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentException("filename");
			}
			if (fileInfo == null)
			{
				throw new ArgumentNullException("fileInfo");
			}
			using (PipeStream input = base.ServiceFactory.CreatePipeStream())
			{
				using (IChannelSession channelSession = base.Session.CreateChannelSession())
				{
					channelSession.DataReceived += delegate(object sender, ChannelDataEventArgs e)
					{
						input.Write(e.Data, 0, e.Data.Length);
					};
					channelSession.Open();
					channelSession.SendExecRequest(string.Format("scp -pf \"{0}\"", filename));
					ScpClient.SendConfirmation(channelSession);
					this.InternalDownload(channelSession, input, fileInfo);
					channelSession.Close();
				}
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006B04 File Offset: 0x00004D04
		public void Download(string directoryName, DirectoryInfo directoryInfo)
		{
			if (string.IsNullOrEmpty(directoryName))
			{
				throw new ArgumentException("directoryName");
			}
			if (directoryInfo == null)
			{
				throw new ArgumentNullException("directoryInfo");
			}
			using (PipeStream input = base.ServiceFactory.CreatePipeStream())
			{
				using (IChannelSession channelSession = base.Session.CreateChannelSession())
				{
					channelSession.DataReceived += delegate(object sender, ChannelDataEventArgs e)
					{
						input.Write(e.Data, 0, e.Data.Length);
					};
					channelSession.Open();
					channelSession.SendExecRequest(string.Format("scp -prf \"{0}\"", directoryName));
					ScpClient.SendConfirmation(channelSession);
					this.InternalDownload(channelSession, input, directoryInfo);
					channelSession.Close();
				}
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006BD8 File Offset: 0x00004DD8
		private void InternalUpload(IChannelSession channel, Stream input, FileInfo fileInfo, string filename)
		{
			ScpClient.InternalSetTimestamp(channel, input, fileInfo.LastWriteTimeUtc, fileInfo.LastAccessTimeUtc);
			using (FileStream fileStream = fileInfo.OpenRead())
			{
				this.InternalUpload(channel, input, fileStream, filename);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006C28 File Offset: 0x00004E28
		private void InternalUpload(IChannelSession channel, Stream input, DirectoryInfo directoryInfo)
		{
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				this.InternalUpload(channel, input, fileInfo, fileInfo.Name);
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				ScpClient.InternalSetTimestamp(channel, input, directoryInfo.LastWriteTimeUtc, directoryInfo.LastAccessTimeUtc);
				ScpClient.SendData(channel, string.Format("D0755 0 {0}\n", directoryInfo2.Name));
				ScpClient.CheckReturnCode(input);
				this.InternalUpload(channel, input, directoryInfo2);
				ScpClient.SendData(channel, "E\n");
				ScpClient.CheckReturnCode(input);
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006CC4 File Offset: 0x00004EC4
		private void InternalDownload(IChannelSession channel, Stream input, FileSystemInfo fileSystemInfo)
		{
			DateTime lastWriteTime = DateTime.Now;
			DateTime lastAccessTime = DateTime.Now;
			string fullName = fileSystemInfo.FullName;
			int num = 0;
			for (;;)
			{
				string text = ScpClient.ReadString(input);
				if (text == "E")
				{
					ScpClient.SendConfirmation(channel);
					num--;
					fullName = new DirectoryInfo(fullName).Parent.FullName;
					if (num == 0)
					{
						break;
					}
				}
				else
				{
					Match match = ScpClient.DirectoryInfoRe.Match(text);
					if (match.Success)
					{
						ScpClient.SendConfirmation(channel);
						long.Parse(match.Result("${mode}"));
						string arg = match.Result("${filename}");
						DirectoryInfo directoryInfo;
						if (num > 0)
						{
							directoryInfo = Directory.CreateDirectory(string.Format("{0}{1}{2}", fullName, Path.DirectorySeparatorChar, arg));
							directoryInfo.LastAccessTime = lastAccessTime;
							directoryInfo.LastWriteTime = lastWriteTime;
						}
						else
						{
							directoryInfo = (fileSystemInfo as DirectoryInfo);
						}
						num++;
						fullName = directoryInfo.FullName;
					}
					else
					{
						match = ScpClient.FileInfoRe.Match(text);
						if (match.Success)
						{
							ScpClient.SendConfirmation(channel);
							match.Result("${mode}");
							long length = long.Parse(match.Result("${length}"));
							string text2 = match.Result("${filename}");
							FileInfo fileInfo = fileSystemInfo as FileInfo;
							if (fileInfo == null)
							{
								fileInfo = new FileInfo(string.Format("{0}{1}{2}", fullName, Path.DirectorySeparatorChar, text2));
							}
							using (FileStream fileStream = fileInfo.OpenWrite())
							{
								this.InternalDownload(channel, input, fileStream, text2, length);
							}
							fileInfo.LastAccessTime = lastAccessTime;
							fileInfo.LastWriteTime = lastWriteTime;
							if (num == 0)
							{
								return;
							}
						}
						else
						{
							match = ScpClient.TimestampRe.Match(text);
							if (match.Success)
							{
								ScpClient.SendConfirmation(channel);
								long num2 = long.Parse(match.Result("${mtime}"));
								long num3 = long.Parse(match.Result("${atime}"));
								DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
								lastWriteTime = dateTime.AddSeconds((double)num2);
								lastAccessTime = dateTime.AddSeconds((double)num3);
							}
							else
							{
								ScpClient.SendConfirmation(channel, 1, string.Format("\"{0}\" is not valid protocol message.", text));
							}
						}
					}
				}
			}
		}

		// Token: 0x04000078 RID: 120
		private static readonly Regex FileInfoRe = new Regex("C(?<mode>\\d{4}) (?<length>\\d+) (?<filename>.+)");

		// Token: 0x04000079 RID: 121
		private static char[] _byteToChar;

		// Token: 0x0400007E RID: 126
		private static readonly Regex DirectoryInfoRe = new Regex("D(?<mode>\\d{4}) (?<length>\\d+) (?<filename>.+)");

		// Token: 0x0400007F RID: 127
		private static readonly Regex TimestampRe = new Regex("T(?<mtime>\\d+) 0 (?<atime>\\d+) 0");
	}
}
