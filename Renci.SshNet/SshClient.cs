using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x0200002D RID: 45
	public class SshClient : BaseClient
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		public IEnumerable<ForwardedPort> ForwardedPorts
		{
			get
			{
				return this._forwardedPorts.AsReadOnly();
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000D2FD File Offset: 0x0000B4FD
		public SshClient(ConnectionInfo connectionInfo) : this(connectionInfo, false)
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000D307 File Offset: 0x0000B507
		public SshClient(string host, int port, string username, string password) : this(new PasswordConnectionInfo(host, port, username, password), true)
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000D31A File Offset: 0x0000B51A
		public SshClient(string host, string username, string password) : this(host, ConnectionInfo.DefaultPort, username, password)
		{
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000D32A File Offset: 0x0000B52A
		public SshClient(string host, int port, string username, params PrivateKeyFile[] keyFiles) : this(new PrivateKeyConnectionInfo(host, port, username, keyFiles), true)
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000D33D File Offset: 0x0000B53D
		public SshClient(string host, string username, params PrivateKeyFile[] keyFiles) : this(host, ConnectionInfo.DefaultPort, username, keyFiles)
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000D34D File Offset: 0x0000B54D
		private SshClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo) : this(connectionInfo, ownsConnectionInfo, new ServiceFactory())
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000D35C File Offset: 0x0000B55C
		internal SshClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo, IServiceFactory serviceFactory) : base(connectionInfo, ownsConnectionInfo, serviceFactory)
		{
			this._forwardedPorts = new List<ForwardedPort>();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000D374 File Offset: 0x0000B574
		protected override void OnDisconnecting()
		{
			base.OnDisconnecting();
			foreach (ForwardedPort forwardedPort in this._forwardedPorts)
			{
				forwardedPort.Stop();
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000D3CC File Offset: 0x0000B5CC
		public void AddForwardedPort(ForwardedPort port)
		{
			if (port == null)
			{
				throw new ArgumentNullException("port");
			}
			this.EnsureSessionIsOpen();
			this.AttachForwardedPort(port);
			this._forwardedPorts.Add(port);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000D3F5 File Offset: 0x0000B5F5
		public void RemoveForwardedPort(ForwardedPort port)
		{
			if (port == null)
			{
				throw new ArgumentNullException("port");
			}
			port.Stop();
			SshClient.DetachForwardedPort(port);
			this._forwardedPorts.Remove(port);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000D41E File Offset: 0x0000B61E
		private void AttachForwardedPort(ForwardedPort port)
		{
			if (port.Session != null && port.Session != base.Session)
			{
				throw new InvalidOperationException("Forwarded port is already added to a different client.");
			}
			port.Session = base.Session;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000D44D File Offset: 0x0000B64D
		private static void DetachForwardedPort(ForwardedPort port)
		{
			port.Session = null;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000D456 File Offset: 0x0000B656
		public SshCommand CreateCommand(string commandText)
		{
			return this.CreateCommand(commandText, base.ConnectionInfo.Encoding);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000D46A File Offset: 0x0000B66A
		public SshCommand CreateCommand(string commandText, Encoding encoding)
		{
			this.EnsureSessionIsOpen();
			base.ConnectionInfo.Encoding = encoding;
			return new SshCommand(base.Session, commandText, encoding);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000D48B File Offset: 0x0000B68B
		public SshCommand RunCommand(string commandText)
		{
			SshCommand sshCommand = this.CreateCommand(commandText);
			sshCommand.Execute();
			return sshCommand;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000D49C File Offset: 0x0000B69C
		public Shell CreateShell(Stream input, Stream output, Stream extendedOutput, string terminalName, uint columns, uint rows, uint width, uint height, IDictionary<TerminalModes, uint> terminalModes, int bufferSize)
		{
			this.EnsureSessionIsOpen();
			return new Shell(base.Session, input, output, extendedOutput, terminalName, columns, rows, width, height, terminalModes, bufferSize);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000D4CC File Offset: 0x0000B6CC
		public Shell CreateShell(Stream input, Stream output, Stream extendedOutput, string terminalName, uint columns, uint rows, uint width, uint height, IDictionary<TerminalModes, uint> terminalModes)
		{
			return this.CreateShell(input, output, extendedOutput, terminalName, columns, rows, width, height, terminalModes, 1024);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		public Shell CreateShell(Stream input, Stream output, Stream extendedOutput)
		{
			return this.CreateShell(input, output, extendedOutput, string.Empty, 0U, 0U, 0U, 0U, null, 1024);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000D51C File Offset: 0x0000B71C
		public Shell CreateShell(Encoding encoding, string input, Stream output, Stream extendedOutput, string terminalName, uint columns, uint rows, uint width, uint height, IDictionary<TerminalModes, uint> terminalModes, int bufferSize)
		{
			this._inputStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(this._inputStream, encoding);
			streamWriter.Write(input);
			streamWriter.Flush();
			this._inputStream.Seek(0L, SeekOrigin.Begin);
			return this.CreateShell(this._inputStream, output, extendedOutput, terminalName, columns, rows, width, height, terminalModes, bufferSize);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000D578 File Offset: 0x0000B778
		public Shell CreateShell(Encoding encoding, string input, Stream output, Stream extendedOutput, string terminalName, uint columns, uint rows, uint width, uint height, IDictionary<TerminalModes, uint> terminalModes)
		{
			return this.CreateShell(encoding, input, output, extendedOutput, terminalName, columns, rows, width, height, terminalModes, 1024);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		public Shell CreateShell(Encoding encoding, string input, Stream output, Stream extendedOutput)
		{
			return this.CreateShell(encoding, input, output, extendedOutput, string.Empty, 0U, 0U, 0U, 0U, null, 1024);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000D5CB File Offset: 0x0000B7CB
		public ShellStream CreateShellStream(string terminalName, uint columns, uint rows, uint width, uint height, int bufferSize)
		{
			return this.CreateShellStream(terminalName, columns, rows, width, height, bufferSize, null);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000D5DD File Offset: 0x0000B7DD
		public ShellStream CreateShellStream(string terminalName, uint columns, uint rows, uint width, uint height, int bufferSize, IDictionary<TerminalModes, uint> terminalModeValues)
		{
			this.EnsureSessionIsOpen();
			return new ShellStream(base.Session, terminalName, columns, rows, width, height, terminalModeValues);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		protected override void OnDisconnected()
		{
			base.OnDisconnected();
			for (int i = this._forwardedPorts.Count - 1; i >= 0; i--)
			{
				SshClient.DetachForwardedPort(this._forwardedPorts[i]);
				this._forwardedPorts.RemoveAt(i);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000D644 File Offset: 0x0000B844
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				if (this._inputStream != null)
				{
					this._inputStream.Dispose();
					this._inputStream = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000D67A File Offset: 0x0000B87A
		private void EnsureSessionIsOpen()
		{
			if (base.Session == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
		}

		// Token: 0x04000100 RID: 256
		private readonly List<ForwardedPort> _forwardedPorts;

		// Token: 0x04000101 RID: 257
		private bool _isDisposed;

		// Token: 0x04000102 RID: 258
		private Stream _inputStream;
	}
}
