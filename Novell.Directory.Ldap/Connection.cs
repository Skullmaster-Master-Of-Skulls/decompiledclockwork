using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000003 RID: 3
	internal sealed class Connection
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00001050
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002074 File Offset: 0x00001074
		public event CertificateValidationCallback OnCertificateValidation;

		// Token: 0x06000007 RID: 7 RVA: 0x00002098 File Offset: 0x00001098
		private static string GetProblemMessage(Connection.CertificateProblem Problem)
		{
			string text = "";
			string text2 = Enum.GetName(typeof(Connection.CertificateProblem), Problem);
			if (text2 != null)
			{
				text += text2;
			}
			else
			{
				text = "Unknown Certificate Problem";
			}
			return text;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020DC File Offset: 0x000010DC
		private void InitBlock()
		{
			this.writeSemaphore = new object();
			this.encoder = new LBEREncoder();
			this.decoder = new LBERDecoder();
			this.stopReaderMessageID = -99;
			this.messages = new MessageVector(5, 5);
			this.unsolicitedListeners = new ArrayList(3);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000112C
		internal bool Cloned
		{
			get
			{
				return this.cloneCount > 0;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002148 File Offset: 0x00001148
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002160 File Offset: 0x00001160
		internal bool Ssl
		{
			get
			{
				return this.ssl;
			}
			set
			{
				this.ssl = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002174 File Offset: 0x00001174
		internal string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000218C File Offset: 0x0000118C
		internal int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021A4 File Offset: 0x000011A4
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021BC File Offset: 0x000011BC
		internal int BindSemId
		{
			get
			{
				return this.bindSemaphoreId;
			}
			set
			{
				this.bindSemaphoreId = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021D4 File Offset: 0x000011D4
		internal bool BindSemIdClear
		{
			get
			{
				return this.bindSemaphoreId == 0;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021F4 File Offset: 0x000011F4
		internal bool Bound
		{
			get
			{
				return this.bindProperties != null && !this.bindProperties.Anonymous;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002220 File Offset: 0x00001220
		internal bool Connected
		{
			get
			{
				return this.in_Renamed != null;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002240 File Offset: 0x00001240
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002258 File Offset: 0x00001258
		internal BindProperties BindProperties
		{
			get
			{
				return this.bindProperties;
			}
			set
			{
				this.bindProperties = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002270 File Offset: 0x00001270
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002288 File Offset: 0x00001288
		internal ReferralInfo ActiveReferral
		{
			get
			{
				return this.activeReferral;
			}
			set
			{
				this.activeReferral = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022A0 File Offset: 0x000012A0
		internal string ConnectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022B8 File Offset: 0x000012B8
		internal Connection()
		{
			this.InitBlock();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002378 File Offset: 0x00001378
		internal object copy()
		{
			Connection connection = new Connection();
			connection.host = this.host;
			connection.port = this.port;
			Connection.protocol = Connection.protocol;
			return connection;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023B4 File Offset: 0x000013B4
		internal int acquireWriteSemaphore()
		{
			return this.acquireWriteSemaphore(0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023CC File Offset: 0x000013CC
		internal int acquireWriteSemaphore(int msgId)
		{
			int num = msgId;
			lock (this.writeSemaphore)
			{
				if (num == 0)
				{
					this.ephemeralId = ((this.ephemeralId == int.MinValue) ? (this.ephemeralId = -1) : (--this.ephemeralId));
					num = this.ephemeralId;
				}
				while (this.writeSemaphoreOwner != 0)
				{
					if (this.writeSemaphoreOwner == num)
					{
						IL_7E:
						this.writeSemaphoreCount++;
						return num;
					}
					try
					{
						Monitor.Wait(this.writeSemaphore);
					}
					catch (ThreadInterruptedException ex)
					{
					}
				}
				this.writeSemaphoreOwner = num;
				goto IL_7E;
			}
			return num;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024A8 File Offset: 0x000014A8
		internal void freeWriteSemaphore(int msgId)
		{
			lock (this.writeSemaphore)
			{
				if (this.writeSemaphoreOwner == 0)
				{
					throw new SystemException("Connection.freeWriteSemaphore(" + msgId + "): semaphore not owned by any thread");
				}
				if (this.writeSemaphoreOwner != msgId)
				{
					throw new SystemException(string.Concat(new object[]
					{
						"Connection.freeWriteSemaphore(",
						msgId,
						"): thread does not own the semaphore, owned by ",
						this.writeSemaphoreOwner
					}));
				}
				if (--this.writeSemaphoreCount == 0)
				{
					this.writeSemaphoreOwner = 0;
					Monitor.Pulse(this.writeSemaphore);
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002574 File Offset: 0x00001574
		private void waitForReader(Thread thread)
		{
			Thread objA;
			if (this.reader != null)
			{
				objA = this.reader;
			}
			else
			{
				objA = null;
			}
			Thread objB;
			if (thread != null)
			{
				objB = thread;
			}
			else
			{
				objB = null;
			}
			while (!object.Equals(objA, objB))
			{
				try
				{
					if (thread == this.deadReader)
					{
						if (thread == null)
						{
							return;
						}
						IOException rootException = this.deadReaderException;
						this.deadReaderException = null;
						this.deadReader = null;
						throw new LdapException("CONNECTION_READER", 91, null, rootException);
					}
					else
					{
						lock (this)
						{
							Monitor.Wait(this, TimeSpan.FromMilliseconds(5.0));
						}
					}
				}
				catch (ThreadInterruptedException ex)
				{
				}
				if (this.reader != null)
				{
					objA = this.reader;
				}
				else
				{
					objA = null;
				}
				if (thread != null)
				{
					objB = thread;
					continue;
				}
				objB = null;
			}
			this.deadReaderException = null;
			this.deadReader = null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002670 File Offset: 0x00001670
		internal void connect(string host, int port)
		{
			this.connect(host, port, 0);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002688 File Offset: 0x00001688
		public bool ServerCertificateValidation(X509Certificate certificate, int[] certificateErrors)
		{
			bool result;
			if (this.OnCertificateValidation != null)
			{
				result = this.OnCertificateValidation(certificate, certificateErrors);
			}
			else
			{
				result = this.DefaultCertificateValidationHandler(certificate, certificateErrors);
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026BC File Offset: 0x000016BC
		public bool DefaultCertificateValidationHandler(X509Certificate certificate, int[] certificateErrors)
		{
			bool result;
			if (certificateErrors != null && certificateErrors.Length > 0)
			{
				if (certificateErrors.Length == 1 && certificateErrors[0] == -2146762481)
				{
					result = true;
				}
				else
				{
					Console.WriteLine("Detected errors in the Server Certificate:");
					for (int i = 0; i < certificateErrors.Length; i++)
					{
						this.handshakeProblemsEncountered.Add((Connection.CertificateProblem)((ulong)certificateErrors[i]));
						Console.WriteLine(certificateErrors[i]);
					}
					result = false;
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002728 File Offset: 0x00001728
		private void connect(string host, int port, int semaphoreId)
		{
			this.waitForReader(null);
			this.unsolSvrShutDnNotification = false;
			int msgId = this.acquireWriteSemaphore(semaphoreId);
			try
			{
				if (port == 0)
				{
					port = 389;
				}
				try
				{
					if (this.in_Renamed == null || this.out_Renamed == null)
					{
						if (this.Ssl)
						{
							this.host = host;
							this.port = port;
							this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
							IPAddress address = Dns.Resolve(host).AddressList[0];
							IPEndPoint remoteEP = new IPEndPoint(address, port);
							this.sock.Connect(remoteEP);
							NetworkStream networkStream = new NetworkStream(this.sock, true);
							Assembly assembly;
							try
							{
								assembly = Assembly.LoadWithPartialName("Mono.Security");
							}
							catch (FileNotFoundException)
							{
								throw new LdapException("SSL_PROVIDER_MISSING", 114, null);
							}
							Type type = assembly.GetType("Mono.Security.Protocol.Tls.SslClientStream");
							object[] array = new object[4];
							array[0] = networkStream;
							array[1] = host;
							array[2] = false;
							Type type2 = assembly.GetType("Mono.Security.Protocol.Tls.SecurityProtocolType");
							Enum @enum = (Enum)Activator.CreateInstance(type2);
							int num = (int)Enum.Parse(type2, "Ssl3");
							int num2 = (int)Enum.Parse(type2, "Tls");
							array[3] = Enum.ToObject(type2, num | num2);
							object obj = Activator.CreateInstance(type, array);
							PropertyInfo property = type.GetProperty("ServerCertValidationDelegate");
							property.SetValue(obj, Delegate.CreateDelegate(property.PropertyType, this, "ServerCertificateValidation"), null);
							this.in_Renamed = (Stream)obj;
							this.out_Renamed = (Stream)obj;
						}
						else
						{
							this.socket = new TcpClient(host, port);
							this.in_Renamed = this.socket.GetStream();
							this.out_Renamed = this.socket.GetStream();
						}
					}
					else
					{
						Console.WriteLine("connect input/out Stream specified");
					}
				}
				catch (SocketException rootException)
				{
					this.freeWriteSemaphore(msgId);
					this.sock = null;
					this.socket = null;
					throw new LdapException("CONNECTION_ERROR", new object[]
					{
						host,
						port
					}, 91, null, rootException);
				}
				catch (IOException rootException2)
				{
					this.freeWriteSemaphore(msgId);
					this.sock = null;
					this.socket = null;
					throw new LdapException("CONNECTION_ERROR", new object[]
					{
						host,
						port
					}, 91, null, rootException2);
				}
				this.host = host;
				this.port = port;
				this.startReader();
				this.clientActive = true;
			}
			finally
			{
				this.freeWriteSemaphore(msgId);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000029FC File Offset: 0x000019FC
		internal void incrCloneCount()
		{
			lock (this)
			{
				this.cloneCount++;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A44 File Offset: 0x00001A44
		internal Connection destroyClone(bool apiCall)
		{
			Connection result;
			lock (this)
			{
				Connection connection = this;
				if (this.cloneCount > 0)
				{
					this.cloneCount--;
					if (apiCall)
					{
						connection = (Connection)this.copy();
					}
					else
					{
						connection = null;
					}
				}
				else if (this.in_Renamed != null)
				{
					InterThreadException notifyUser = new InterThreadException(apiCall ? "CONNECTION_CLOSED" : "CONNECTION_FINALIZED", null, 91, null, null);
					this.shutdown("destroy clone", 0, notifyUser);
				}
				result = connection;
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AE0 File Offset: 0x00001AE0
		internal void clearBindSemId()
		{
			this.bindSemaphoreId = 0;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AF8 File Offset: 0x00001AF8
		internal void writeMessage(Message info)
		{
			ExceptionMessages exceptionMessages = new ExceptionMessages();
			object[][] contents = exceptionMessages.getContents();
			this.messages.Add(info);
			if (info.BindRequest && !this.Connected && this.host != null)
			{
				this.connect(this.host, this.port, info.MessageID);
			}
			if (this.Connected)
			{
				LdapMessage request = info.Request;
				this.writeMessage(request);
				return;
			}
			int i;
			for (i = 0; i < contents.Length; i++)
			{
				if (contents[i][0] == "CONNECTION_CLOSED")
				{
					break;
				}
			}
			throw new LdapException("CONNECTION_CLOSED", new object[]
			{
				this.host,
				this.port
			}, 91, (string)contents[i][1]);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002BC0 File Offset: 0x00001BC0
		internal void writeMessage(LdapMessage msg)
		{
			int messageID;
			if (this.bindSemaphoreId == 0)
			{
				messageID = msg.MessageID;
			}
			else
			{
				messageID = this.bindSemaphoreId;
			}
			Stream stream = this.out_Renamed;
			this.acquireWriteSemaphore(messageID);
			try
			{
				if (stream == null)
				{
					throw new IOException("Output stream not initialized");
				}
				if (stream.CanWrite)
				{
					sbyte[] encoding = msg.Asn1Object.getEncoding(this.encoder);
					stream.Write(SupportClass.ToByteArray(encoding), 0, encoding.Length);
					stream.Flush();
				}
			}
			catch (IOException rootException)
			{
				if (msg.Type == 0 && this.ssl)
				{
					string text = "Following problem(s) occurred while establishing SSL based Connection : ";
					if (this.handshakeProblemsEncountered.Count > 0)
					{
						text += Connection.GetProblemMessage((Connection.CertificateProblem)this.handshakeProblemsEncountered[0]);
						for (int i = 1; i < this.handshakeProblemsEncountered.Count; i++)
						{
							text = text + ", " + Connection.GetProblemMessage((Connection.CertificateProblem)this.handshakeProblemsEncountered[i]);
						}
					}
					else
					{
						text += "Unknown Certificate Problem";
					}
					throw new LdapException(text, new object[]
					{
						this.host,
						this.port
					}, 113, null, rootException);
				}
				if (this.clientActive)
				{
					if (this.unsolSvrShutDnNotification)
					{
						throw new LdapException("SERVER_SHUTDOWN_REQ", new object[]
						{
							this.host,
							this.port
						}, 91, null, rootException);
					}
					throw new LdapException("IO_EXCEPTION", new object[]
					{
						this.host,
						this.port
					}, 91, null, rootException);
				}
			}
			finally
			{
				this.freeWriteSemaphore(messageID);
				this.handshakeProblemsEncountered.Clear();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002DC8 File Offset: 0x00001DC8
		internal MessageAgent getMessageAgent(int msgId)
		{
			Message message = this.messages.findMessageById(msgId);
			return message.MessageAgent;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002DEC File Offset: 0x00001DEC
		internal void removeMessage(Message info)
		{
			bool flag = SupportClass.VectorRemoveElement(this.messages, info);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002E08 File Offset: 0x00001E08
		~Connection()
		{
			this.shutdown("Finalize", 0, null);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E48 File Offset: 0x00001E48
		private void shutdown(string reason, int semaphoreId, InterThreadException notifyUser)
		{
			Message message = null;
			if (this.clientActive)
			{
				this.clientActive = false;
				for (;;)
				{
					try
					{
						object obj = this.messages[0];
						this.messages.RemoveAt(0);
						message = (Message)obj;
					}
					catch (ArgumentOutOfRangeException ex)
					{
						break;
					}
					message.Abandon(null, notifyUser);
				}
				int msgId = this.acquireWriteSemaphore(semaphoreId);
				if (this.bindProperties != null && this.out_Renamed != null && this.out_Renamed.CanWrite && !this.bindProperties.Anonymous)
				{
					try
					{
						LdapMessage ldapMessage = new LdapUnbindRequest(null);
						sbyte[] encoding = ldapMessage.Asn1Object.getEncoding(this.encoder);
						this.out_Renamed.Write(SupportClass.ToByteArray(encoding), 0, encoding.Length);
						this.out_Renamed.Flush();
						this.out_Renamed.Close();
					}
					catch (Exception ex2)
					{
					}
				}
				this.bindProperties = null;
				if (this.socket != null || this.sock != null)
				{
					if (this.reader != null && reason != "reader: thread stopping")
					{
						this.reader.Abort();
					}
					try
					{
						if (this.Ssl)
						{
							this.sock.Shutdown(SocketShutdown.Both);
							this.sock.Close();
						}
						else
						{
							if (this.in_Renamed != null)
							{
								this.in_Renamed.Close();
							}
							this.socket.Close();
						}
					}
					catch (IOException ex3)
					{
					}
					this.socket = null;
					this.sock = null;
					this.in_Renamed = null;
					this.out_Renamed = null;
				}
				this.freeWriteSemaphore(msgId);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003010 File Offset: 0x00002010
		internal bool areMessagesComplete()
		{
			object[] objectArray = this.messages.ObjectArray;
			int num = objectArray.Length;
			bool result;
			if (this.bindSemaphoreId != 0)
			{
				result = false;
			}
			else if (num == 0)
			{
				result = true;
			}
			else
			{
				for (int i = 0; i < num; i++)
				{
					if (!((Message)objectArray[i]).Complete)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003064 File Offset: 0x00002064
		internal void stopReaderOnReply(int messageID)
		{
			this.stopReaderMessageID = messageID;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000307C File Offset: 0x0000207C
		internal void startReader()
		{
			Thread thread = new Thread(new ThreadStart(new Connection.ReaderThread(this).Run));
			thread.IsBackground = true;
			thread.Start();
			this.waitForReader(thread);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000030B8 File Offset: 0x000020B8
		internal bool TLS
		{
			get
			{
				return this.nonTLSBackup != null;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000030D8 File Offset: 0x000020D8
		internal void startTLS()
		{
			try
			{
				this.waitForReader(null);
				this.nonTLSBackup = this.socket;
				Assembly assembly = null;
				try
				{
					assembly = Assembly.LoadWithPartialName("Mono.Security");
				}
				catch (FileNotFoundException)
				{
					throw new LdapException("SSL_PROVIDER_MISSING", 114, null);
				}
				Type type = assembly.GetType("Mono.Security.Protocol.Tls.SslClientStream");
				object[] array = new object[4];
				array[0] = this.socket.GetStream();
				array[1] = this.host;
				array[2] = false;
				Type type2 = assembly.GetType("Mono.Security.Protocol.Tls.SecurityProtocolType");
				Enum @enum = (Enum)Activator.CreateInstance(type2);
				int num = (int)Enum.Parse(type2, "Ssl3");
				int num2 = (int)Enum.Parse(type2, "Tls");
				array[3] = Enum.ToObject(type2, num | num2);
				object obj = Activator.CreateInstance(type, array);
				EventInfo @event = type.GetEvent("ServerCertValidationDelegate");
				@event.AddEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, "ServerCertificateValidation"));
				this.in_Renamed = (Stream)obj;
				this.out_Renamed = (Stream)obj;
			}
			catch (IOException rootException)
			{
				this.nonTLSBackup = null;
				throw new LdapException("Could not negotiate a secure connection", 91, null, rootException);
			}
			catch (Exception rootException2)
			{
				this.nonTLSBackup = null;
				throw new LdapException("The host is unknown", 91, null, rootException2);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000326C File Offset: 0x0000226C
		internal void stopTLS()
		{
			try
			{
				this.stopReaderMessageID = -98;
				this.out_Renamed.Close();
				this.in_Renamed.Close();
				this.waitForReader(null);
				this.socket = this.nonTLSBackup;
				this.in_Renamed = this.socket.GetStream();
				this.out_Renamed = this.socket.GetStream();
				this.stopReaderMessageID = -99;
			}
			catch (IOException rootException)
			{
				throw new LdapException("STOPTLS_ERROR", 91, null, rootException);
			}
			finally
			{
				this.nonTLSBackup = null;
				this.startReader();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000332C File Offset: 0x0000232C
		internal void AddUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			this.unsolicitedListeners.Add(listener);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003348 File Offset: 0x00002348
		internal void RemoveUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			SupportClass.VectorRemoveElement(this.unsolicitedListeners, listener);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003364 File Offset: 0x00002364
		private void notifyAllUnsolicitedListeners(RfcLdapMessage message)
		{
			LdapMessage ldapMessage = new LdapExtendedResponse(message);
			string id = ((LdapExtendedResponse)ldapMessage).ID;
			if (id.Equals("1.3.6.1.4.1.1466.20036"))
			{
				this.unsolSvrShutDnNotification = true;
			}
			int count = this.unsolicitedListeners.Count;
			for (int i = 0; i < count; i++)
			{
				LdapUnsolicitedNotificationListener l = (LdapUnsolicitedNotificationListener)this.unsolicitedListeners[i];
				LdapExtendedResponse m = new LdapExtendedResponse(message);
				Connection.UnsolicitedListenerThread unsolicitedListenerThread = new Connection.UnsolicitedListenerThread(this, l, m);
				unsolicitedListenerThread.Start();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000033E0 File Offset: 0x000023E0
		static Connection()
		{
			Connection.nameLock = new object();
			Connection.sdk = new StringBuilder("2.1.10").ToString();
			Connection.protocol = 3;
		}

		// Token: 0x04000001 RID: 1
		private const int CONTINUE_READING = -99;

		// Token: 0x04000002 RID: 2
		private const int STOP_READING = -98;

		// Token: 0x04000004 RID: 4
		private ArrayList handshakeProblemsEncountered = new ArrayList();

		// Token: 0x04000005 RID: 5
		private object writeSemaphore;

		// Token: 0x04000006 RID: 6
		private int writeSemaphoreOwner = 0;

		// Token: 0x04000007 RID: 7
		private int writeSemaphoreCount = 0;

		// Token: 0x04000008 RID: 8
		private int ephemeralId = -1;

		// Token: 0x04000009 RID: 9
		private BindProperties bindProperties = null;

		// Token: 0x0400000A RID: 10
		private int bindSemaphoreId = 0;

		// Token: 0x0400000B RID: 11
		private Thread reader = null;

		// Token: 0x0400000C RID: 12
		private Thread deadReader = null;

		// Token: 0x0400000D RID: 13
		private IOException deadReaderException = null;

		// Token: 0x0400000E RID: 14
		private LBEREncoder encoder;

		// Token: 0x0400000F RID: 15
		private LBERDecoder decoder;

		// Token: 0x04000010 RID: 16
		private Socket sock = null;

		// Token: 0x04000011 RID: 17
		private TcpClient socket = null;

		// Token: 0x04000012 RID: 18
		private TcpClient nonTLSBackup = null;

		// Token: 0x04000013 RID: 19
		private Stream in_Renamed = null;

		// Token: 0x04000014 RID: 20
		private Stream out_Renamed = null;

		// Token: 0x04000015 RID: 21
		private bool clientActive = true;

		// Token: 0x04000016 RID: 22
		private bool ssl = false;

		// Token: 0x04000017 RID: 23
		private bool unsolSvrShutDnNotification = false;

		// Token: 0x04000018 RID: 24
		private int stopReaderMessageID;

		// Token: 0x04000019 RID: 25
		private MessageVector messages;

		// Token: 0x0400001A RID: 26
		private ReferralInfo activeReferral = null;

		// Token: 0x0400001B RID: 27
		private ArrayList unsolicitedListeners;

		// Token: 0x0400001C RID: 28
		private string host = null;

		// Token: 0x0400001D RID: 29
		private int port = 0;

		// Token: 0x0400001E RID: 30
		private int cloneCount = 0;

		// Token: 0x0400001F RID: 31
		private string name = "";

		// Token: 0x04000020 RID: 32
		private static object nameLock;

		// Token: 0x04000021 RID: 33
		private static int connNum = 0;

		// Token: 0x04000022 RID: 34
		internal static string sdk;

		// Token: 0x04000023 RID: 35
		internal static int protocol;

		// Token: 0x04000024 RID: 36
		internal static string security = "simple";

		// Token: 0x02000004 RID: 4
		public enum CertificateProblem : long
		{
			// Token: 0x04000026 RID: 38
			CertEXPIRED = 2148204801L,
			// Token: 0x04000027 RID: 39
			CertVALIDITYPERIODNESTING,
			// Token: 0x04000028 RID: 40
			CertROLE,
			// Token: 0x04000029 RID: 41
			CertPATHLENCONST,
			// Token: 0x0400002A RID: 42
			CertCRITICAL,
			// Token: 0x0400002B RID: 43
			CertPURPOSE,
			// Token: 0x0400002C RID: 44
			CertISSUERCHAINING,
			// Token: 0x0400002D RID: 45
			CertMALFORMED,
			// Token: 0x0400002E RID: 46
			CertUNTRUSTEDROOT,
			// Token: 0x0400002F RID: 47
			CertCHAINING,
			// Token: 0x04000030 RID: 48
			CertREVOKED = 2148204812L,
			// Token: 0x04000031 RID: 49
			CertUNTRUSTEDTESTROOT,
			// Token: 0x04000032 RID: 50
			CertREVOCATION_FAILURE,
			// Token: 0x04000033 RID: 51
			CertCN_NO_MATCH,
			// Token: 0x04000034 RID: 52
			CertWRONG_USAGE,
			// Token: 0x04000035 RID: 53
			CertUNTRUSTEDCA = 2148204818L
		}

		// Token: 0x02000005 RID: 5
		public class ReaderThread
		{
			// Token: 0x06000035 RID: 53 RVA: 0x00003424 File Offset: 0x00002424
			private void InitBlock(Connection enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000036 RID: 54 RVA: 0x00003438 File Offset: 0x00002438
			public Connection Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00003450 File Offset: 0x00002450
			public ReaderThread(Connection enclosingInstance)
			{
				this.InitBlock(enclosingInstance);
			}

			// Token: 0x06000038 RID: 56 RVA: 0x0000346C File Offset: 0x0000246C
			public virtual void Run()
			{
				string reason = "reader: thread stopping";
				InterThreadException ex = null;
				Message message = null;
				IOException deadReaderException = null;
				this.enclosingInstance.reader = Thread.CurrentThread;
				try
				{
					try
					{
						for (;;)
						{
							Stream in_Renamed = this.enclosingInstance.in_Renamed;
							if (in_Renamed == null)
							{
								break;
							}
							Asn1Identifier asn1Identifier = new Asn1Identifier(in_Renamed);
							int tag = asn1Identifier.Tag;
							if (asn1Identifier.Tag == 16)
							{
								Asn1Length asn1Length = new Asn1Length(in_Renamed);
								RfcLdapMessage rfcLdapMessage = new RfcLdapMessage(this.enclosingInstance.decoder, in_Renamed, asn1Length.Length);
								int messageID = rfcLdapMessage.MessageID;
								try
								{
									message = this.enclosingInstance.messages.findMessageById(messageID);
									message.putReply(rfcLdapMessage);
								}
								catch (FieldAccessException ex2)
								{
									if (messageID == 0)
									{
										this.enclosingInstance.notifyAllUnsolicitedListeners(rfcLdapMessage);
										if (this.enclosingInstance.unsolSvrShutDnNotification)
										{
											ex = new InterThreadException("SERVER_SHUTDOWN_REQ", new object[]
											{
												this.enclosingInstance.host,
												this.enclosingInstance.port
											}, 91, null, null);
											return;
										}
									}
								}
								if (this.enclosingInstance.stopReaderMessageID == messageID || this.enclosingInstance.stopReaderMessageID == -98)
								{
									goto IL_126;
								}
							}
						}
						goto IL_1B1;
						IL_126:
						return;
					}
					catch (ThreadAbortException ex3)
					{
						return;
					}
					catch (IOException ex4)
					{
						deadReaderException = ex4;
						if (this.enclosingInstance.stopReaderMessageID != -98 && this.enclosingInstance.clientActive)
						{
							ex = new InterThreadException("CONNECTION_WAIT", new object[]
							{
								this.enclosingInstance.host,
								this.enclosingInstance.port
							}, 91, ex4, message);
						}
						this.enclosingInstance.in_Renamed = null;
						this.enclosingInstance.out_Renamed = null;
					}
					IL_1B1:;
				}
				finally
				{
					if (!this.enclosingInstance.clientActive || ex != null)
					{
						this.enclosingInstance.shutdown(reason, 0, ex);
					}
					else
					{
						this.enclosingInstance.stopReaderMessageID = -99;
					}
				}
				this.enclosingInstance.deadReaderException = deadReaderException;
				this.enclosingInstance.deadReader = this.enclosingInstance.reader;
				this.enclosingInstance.reader = null;
			}

			// Token: 0x04000036 RID: 54
			private Connection enclosingInstance;
		}

		// Token: 0x02000016 RID: 22
		private class UnsolicitedListenerThread : SupportClass.ThreadClass
		{
			// Token: 0x060000D7 RID: 215 RVA: 0x000053BC File Offset: 0x000043BC
			private void InitBlock(Connection enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000D8 RID: 216 RVA: 0x000053D0 File Offset: 0x000043D0
			public Connection Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x000053E8 File Offset: 0x000043E8
			internal UnsolicitedListenerThread(Connection enclosingInstance, LdapUnsolicitedNotificationListener l, LdapExtendedResponse m)
			{
				this.InitBlock(enclosingInstance);
				this.listenerObj = l;
				this.unsolicitedMsg = m;
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00005414 File Offset: 0x00004414
			public override void Run()
			{
				this.listenerObj.messageReceived(this.unsolicitedMsg);
			}

			// Token: 0x04000044 RID: 68
			private Connection enclosingInstance;

			// Token: 0x04000045 RID: 69
			private LdapUnsolicitedNotificationListener listenerObj;

			// Token: 0x04000046 RID: 70
			private LdapExtendedResponse unsolicitedMsg;
		}
	}
}
