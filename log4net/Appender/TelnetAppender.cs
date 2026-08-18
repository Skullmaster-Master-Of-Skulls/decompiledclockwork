using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000044 RID: 68
	public class TelnetAppender : AppenderSkeleton
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000877B File Offset: 0x0000697B
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00008784 File Offset: 0x00006984
		public int Port
		{
			get
			{
				return this.m_listeningPort;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw SystemInfo.CreateArgumentOutOfRangeException("value", value, string.Concat(new string[]
					{
						"The value specified for Port is less than ",
						0.ToString(NumberFormatInfo.InvariantInfo),
						" or greater than ",
						65535.ToString(NumberFormatInfo.InvariantInfo),
						"."
					}));
				}
				this.m_listeningPort = value;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008800 File Offset: 0x00006A00
		protected override void OnClose()
		{
			base.OnClose();
			if (this.m_handler != null)
			{
				this.m_handler.Dispose();
				this.m_handler = null;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00008822 File Offset: 0x00006A22
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008828 File Offset: 0x00006A28
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			try
			{
				LogLog.Debug(TelnetAppender.declaringType, "Creating SocketHandler to listen on port [" + this.m_listeningPort + "]");
				this.m_handler = new TelnetAppender.SocketHandler(this.m_listeningPort);
			}
			catch (Exception exception)
			{
				LogLog.Error(TelnetAppender.declaringType, "Failed to create SocketHandler", exception);
				throw;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008898 File Offset: 0x00006A98
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_handler != null && this.m_handler.HasConnections)
			{
				this.m_handler.Send(base.RenderLoggingEvent(loggingEvent));
			}
		}

		// Token: 0x0400013A RID: 314
		private TelnetAppender.SocketHandler m_handler;

		// Token: 0x0400013B RID: 315
		private int m_listeningPort = 23;

		// Token: 0x0400013C RID: 316
		private static readonly Type declaringType = typeof(TelnetAppender);

		// Token: 0x02000045 RID: 69
		protected class SocketHandler : IDisposable
		{
			// Token: 0x06000271 RID: 625 RVA: 0x000088D4 File Offset: 0x00006AD4
			public SocketHandler(int port)
			{
				this.m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				this.m_serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
				this.m_serverSocket.Listen(5);
				this.AcceptConnection();
			}

			// Token: 0x06000272 RID: 626 RVA: 0x00008928 File Offset: 0x00006B28
			private void AcceptConnection()
			{
				this.m_serverSocket.BeginAccept(new AsyncCallback(this.OnConnect), null);
			}

			// Token: 0x06000273 RID: 627 RVA: 0x00008944 File Offset: 0x00006B44
			public void Send(string message)
			{
				ArrayList clients = this.m_clients;
				foreach (object obj in clients)
				{
					TelnetAppender.SocketHandler.SocketClient socketClient = (TelnetAppender.SocketHandler.SocketClient)obj;
					try
					{
						socketClient.Send(message);
					}
					catch (Exception)
					{
						socketClient.Dispose();
						this.RemoveClient(socketClient);
					}
				}
			}

			// Token: 0x06000274 RID: 628 RVA: 0x000089C0 File Offset: 0x00006BC0
			private void AddClient(TelnetAppender.SocketHandler.SocketClient client)
			{
				lock (this)
				{
					ArrayList arrayList = (ArrayList)this.m_clients.Clone();
					arrayList.Add(client);
					this.m_clients = arrayList;
				}
			}

			// Token: 0x06000275 RID: 629 RVA: 0x00008A18 File Offset: 0x00006C18
			private void RemoveClient(TelnetAppender.SocketHandler.SocketClient client)
			{
				lock (this)
				{
					ArrayList arrayList = (ArrayList)this.m_clients.Clone();
					arrayList.Remove(client);
					this.m_clients = arrayList;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x06000276 RID: 630 RVA: 0x00008A6C File Offset: 0x00006C6C
			public bool HasConnections
			{
				get
				{
					ArrayList clients = this.m_clients;
					return clients != null && clients.Count > 0;
				}
			}

			// Token: 0x06000277 RID: 631 RVA: 0x00008A90 File Offset: 0x00006C90
			private void OnConnect(IAsyncResult asyncResult)
			{
				try
				{
					Socket socket = this.m_serverSocket.EndAccept(asyncResult);
					LogLog.Debug(TelnetAppender.declaringType, "Accepting connection from [" + socket.RemoteEndPoint.ToString() + "]");
					TelnetAppender.SocketHandler.SocketClient socketClient = new TelnetAppender.SocketHandler.SocketClient(socket);
					int count = this.m_clients.Count;
					if (count < 20)
					{
						try
						{
							socketClient.Send("TelnetAppender v1.0 (" + (count + 1) + " active connections)\r\n\r\n");
							this.AddClient(socketClient);
							goto IL_89;
						}
						catch
						{
							socketClient.Dispose();
							goto IL_89;
						}
					}
					socketClient.Send("Sorry - Too many connections.\r\n");
					socketClient.Dispose();
					IL_89:;
				}
				catch
				{
				}
				finally
				{
					if (this.m_serverSocket != null)
					{
						this.AcceptConnection();
					}
				}
			}

			// Token: 0x06000278 RID: 632 RVA: 0x00008B64 File Offset: 0x00006D64
			public void Dispose()
			{
				ArrayList clients = this.m_clients;
				foreach (object obj in clients)
				{
					TelnetAppender.SocketHandler.SocketClient socketClient = (TelnetAppender.SocketHandler.SocketClient)obj;
					socketClient.Dispose();
				}
				this.m_clients.Clear();
				Socket serverSocket = this.m_serverSocket;
				this.m_serverSocket = null;
				try
				{
					serverSocket.Shutdown(SocketShutdown.Both);
				}
				catch
				{
				}
				try
				{
					serverSocket.Close();
				}
				catch
				{
				}
			}

			// Token: 0x0400013D RID: 317
			private const int MAX_CONNECTIONS = 20;

			// Token: 0x0400013E RID: 318
			private Socket m_serverSocket;

			// Token: 0x0400013F RID: 319
			private ArrayList m_clients = new ArrayList();

			// Token: 0x02000046 RID: 70
			protected class SocketClient : IDisposable
			{
				// Token: 0x06000279 RID: 633 RVA: 0x00008C0C File Offset: 0x00006E0C
				public SocketClient(Socket socket)
				{
					this.m_socket = socket;
					try
					{
						this.m_writer = new StreamWriter(new NetworkStream(socket));
					}
					catch
					{
						this.Dispose();
						throw;
					}
				}

				// Token: 0x0600027A RID: 634 RVA: 0x00008C54 File Offset: 0x00006E54
				public void Send(string message)
				{
					this.m_writer.Write(message);
					this.m_writer.Flush();
				}

				// Token: 0x0600027B RID: 635 RVA: 0x00008C70 File Offset: 0x00006E70
				public void Dispose()
				{
					try
					{
						if (this.m_writer != null)
						{
							this.m_writer.Close();
							this.m_writer = null;
						}
					}
					catch
					{
					}
					if (this.m_socket != null)
					{
						try
						{
							this.m_socket.Shutdown(SocketShutdown.Both);
						}
						catch
						{
						}
						try
						{
							this.m_socket.Close();
						}
						catch
						{
						}
						this.m_socket = null;
					}
				}

				// Token: 0x04000140 RID: 320
				private Socket m_socket;

				// Token: 0x04000141 RID: 321
				private StreamWriter m_writer;
			}
		}
	}
}
