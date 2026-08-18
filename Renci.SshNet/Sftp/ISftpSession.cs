using System;
using System.Collections.Generic;
using System.Threading;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp
{
	// Token: 0x02000031 RID: 49
	internal interface ISftpSession : ISubsystemSession, IDisposable
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003CA RID: 970
		uint ProtocolVersion { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003CB RID: 971
		string WorkingDirectory { get; }

		// Token: 0x060003CC RID: 972
		void ChangeDirectory(string path);

		// Token: 0x060003CD RID: 973
		string GetCanonicalPath(string path);

		// Token: 0x060003CE RID: 974
		SftpFileAttributes RequestFStat(byte[] handle);

		// Token: 0x060003CF RID: 975
		SftpFileAttributes RequestLStat(string path);

		// Token: 0x060003D0 RID: 976
		void RequestMkDir(string path);

		// Token: 0x060003D1 RID: 977
		byte[] RequestOpen(string path, Flags flags, bool nullOnError = false);

		// Token: 0x060003D2 RID: 978
		byte[] RequestOpenDir(string path, bool nullOnError = false);

		// Token: 0x060003D3 RID: 979
		void RequestPosixRename(string oldPath, string newPath);

		// Token: 0x060003D4 RID: 980
		byte[] RequestRead(byte[] handle, ulong offset, uint length);

		// Token: 0x060003D5 RID: 981
		KeyValuePair<string, SftpFileAttributes>[] RequestReadDir(byte[] handle);

		// Token: 0x060003D6 RID: 982
		void RequestRemove(string path);

		// Token: 0x060003D7 RID: 983
		void RequestRename(string oldPath, string newPath);

		// Token: 0x060003D8 RID: 984
		void RequestRmDir(string path);

		// Token: 0x060003D9 RID: 985
		void RequestSetStat(string path, SftpFileAttributes attributes);

		// Token: 0x060003DA RID: 986
		SftpFileSytemInformation RequestStatVfs(string path, bool nullOnError = false);

		// Token: 0x060003DB RID: 987
		void RequestSymLink(string linkpath, string targetpath);

		// Token: 0x060003DC RID: 988
		void RequestFSetStat(byte[] handle, SftpFileAttributes attributes);

		// Token: 0x060003DD RID: 989
		void RequestWrite(byte[] handle, ulong offset, byte[] data, int length, AutoResetEvent wait, Action<SftpStatusResponse> writeCompleted = null);

		// Token: 0x060003DE RID: 990
		void RequestClose(byte[] handle);

		// Token: 0x060003DF RID: 991
		uint CalculateOptimalReadLength(uint bufferSize);

		// Token: 0x060003E0 RID: 992
		uint CalculateOptimalWriteLength(uint bufferSize, byte[] handle);
	}
}
