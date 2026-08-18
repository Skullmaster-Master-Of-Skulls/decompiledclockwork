using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000028 RID: 40
	public class LdapConnection : ICloneable
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00007DB8 File Offset: 0x00006DB8
		private void InitBlock()
		{
			this.defSearchCons = new LdapSearchConstraints();
			this.responseCtlSemaphore = new object();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00007DDC File Offset: 0x00006DDC
		public virtual int ProtocolVersion
		{
			get
			{
				BindProperties bindProperties = this.conn.BindProperties;
				int result;
				if (bindProperties == null)
				{
					result = 3;
				}
				else
				{
					result = bindProperties.ProtocolVersion;
				}
				return result;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007E08 File Offset: 0x00006E08
		public virtual string AuthenticationDN
		{
			get
			{
				BindProperties bindProperties = this.conn.BindProperties;
				string result;
				if (bindProperties == null)
				{
					result = null;
				}
				else if (bindProperties.Anonymous)
				{
					result = null;
				}
				else
				{
					result = bindProperties.AuthenticationDN;
				}
				return result;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00007E40 File Offset: 0x00006E40
		public virtual string AuthenticationMethod
		{
			get
			{
				string result;
				if (this.conn.BindProperties == null)
				{
					result = "simple";
				}
				else
				{
					result = this.conn.BindProperties.AuthenticationMethod;
				}
				return result;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007E78 File Offset: 0x00006E78
		public virtual IDictionary SaslBindProperties
		{
			get
			{
				IDictionary result;
				if (this.conn.BindProperties == null)
				{
					result = null;
				}
				else
				{
					result = this.conn.BindProperties.SaslBindProperties;
				}
				return result;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00007EAC File Offset: 0x00006EAC
		public virtual object SaslBindCallbackHandler
		{
			get
			{
				object result;
				if (this.conn.BindProperties == null)
				{
					result = null;
				}
				else
				{
					result = this.conn.BindProperties.SaslCallbackHandler;
				}
				return result;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007EE0 File Offset: 0x00006EE0
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00007F04 File Offset: 0x00006F04
		public virtual LdapConstraints Constraints
		{
			get
			{
				return (LdapConstraints)this.defSearchCons.Clone();
			}
			set
			{
				if (value is LdapSearchConstraints)
				{
					this.defSearchCons = (LdapSearchConstraints)value.Clone();
				}
				else
				{
					LdapSearchConstraints ldapSearchConstraints = (LdapSearchConstraints)this.defSearchCons.Clone();
					ldapSearchConstraints.HopLimit = value.HopLimit;
					ldapSearchConstraints.TimeLimit = value.TimeLimit;
					ldapSearchConstraints.setReferralHandler(value.getReferralHandler());
					ldapSearchConstraints.ReferralFollowing = value.ReferralFollowing;
					LdapControl[] controls = value.getControls();
					if (controls != null)
					{
						ldapSearchConstraints.setControls(controls);
					}
					Hashtable properties = ldapSearchConstraints.Properties;
					if (properties != null)
					{
						ldapSearchConstraints.Properties = properties;
					}
					this.defSearchCons = ldapSearchConstraints;
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007F98 File Offset: 0x00006F98
		public virtual string Host
		{
			get
			{
				return this.conn.Host;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00007FB4 File Offset: 0x00006FB4
		public virtual int Port
		{
			get
			{
				return this.conn.Port;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007FD0 File Offset: 0x00006FD0
		public virtual LdapSearchConstraints SearchConstraints
		{
			get
			{
				return (LdapSearchConstraints)this.defSearchCons.Clone();
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00007FF4 File Offset: 0x00006FF4
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00008010 File Offset: 0x00007010
		public bool SecureSocketLayer
		{
			get
			{
				return this.conn.Ssl;
			}
			set
			{
				this.conn.Ssl = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000802C File Offset: 0x0000702C
		public virtual bool Bound
		{
			get
			{
				return this.conn.Bound;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00008048 File Offset: 0x00007048
		public virtual bool Connected
		{
			get
			{
				return this.conn.Connected;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00008064 File Offset: 0x00007064
		public virtual bool TLS
		{
			get
			{
				return this.conn.TLS;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00008080 File Offset: 0x00007080
		public virtual LdapControl[] ResponseControls
		{
			get
			{
				LdapControl[] result;
				if (this.responseCtls == null)
				{
					result = null;
				}
				else
				{
					LdapControl[] array = new LdapControl[this.responseCtls.Length];
					lock (this.responseCtlSemaphore)
					{
						for (int i = 0; i < this.responseCtls.Length; i++)
						{
							array[i] = (LdapControl)this.responseCtls[i].Clone();
						}
					}
					result = array;
				}
				return result;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00008108 File Offset: 0x00007108
		internal virtual Connection Connection
		{
			get
			{
				return this.conn;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00008120 File Offset: 0x00007120
		internal virtual string ConnectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600017F RID: 383 RVA: 0x00008138 File Offset: 0x00007138
		// (remove) Token: 0x06000180 RID: 384 RVA: 0x00008154 File Offset: 0x00007154
		public event CertificateValidationCallback UserDefinedServerCertValidationDelegate
		{
			add
			{
				this.conn.OnCertificateValidation += value;
			}
			remove
			{
				this.conn.OnCertificateValidation -= value;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008170 File Offset: 0x00007170
		public LdapConnection()
		{
			this.InitBlock();
			this.conn = new Connection();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000081A4 File Offset: 0x000071A4
		public object Clone()
		{
			object obj;
			LdapConnection ldapConnection;
			try
			{
				obj = base.MemberwiseClone();
				ldapConnection = (LdapConnection)obj;
			}
			catch (Exception ex)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			ldapConnection.conn = this.conn;
			if (this.defSearchCons != null)
			{
				ldapConnection.defSearchCons = (LdapSearchConstraints)this.defSearchCons.Clone();
			}
			else
			{
				ldapConnection.defSearchCons = null;
			}
			if (this.responseCtls != null)
			{
				ldapConnection.responseCtls = new LdapControl[this.responseCtls.Length];
				for (int i = 0; i < this.responseCtls.Length; i++)
				{
					ldapConnection.responseCtls[i] = (LdapControl)this.responseCtls[i].Clone();
				}
			}
			else
			{
				ldapConnection.responseCtls = null;
			}
			this.conn.incrCloneCount();
			return obj;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008280 File Offset: 0x00007280
		~LdapConnection()
		{
			this.Disconnect(this.defSearchCons, false);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000082C0 File Offset: 0x000072C0
		public virtual object getProperty(string name)
		{
			object result;
			if (name.ToUpper().Equals("version.sdk".ToUpper()))
			{
				result = Connection.sdk;
			}
			else if (name.ToUpper().Equals("version.protocol".ToUpper()))
			{
				result = Connection.protocol;
			}
			else if (name.ToUpper().Equals("version.security".ToUpper()))
			{
				result = Connection.security;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00008334 File Offset: 0x00007334
		public virtual void AddUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			if (listener != null)
			{
				this.conn.AddUnsolicitedNotificationListener(listener);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008350 File Offset: 0x00007350
		public virtual void RemoveUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			if (listener != null)
			{
				this.conn.RemoveUnsolicitedNotificationListener(listener);
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000836C File Offset: 0x0000736C
		public virtual void startTLS()
		{
			LdapMessage ldapMessage = this.MakeExtendedOperation(new LdapExtendedOperation("1.3.6.1.4.1.1466.20037", null), null);
			int messageID = ldapMessage.MessageID;
			this.conn.acquireWriteSemaphore(messageID);
			try
			{
				if (!this.conn.areMessagesComplete())
				{
					throw new LdapLocalException("OUTSTANDING_OPERATIONS", 1);
				}
				this.conn.stopReaderOnReply(messageID);
				LdapResponseQueue ldapResponseQueue = this.SendRequestToServer(ldapMessage, this.defSearchCons.TimeLimit, null, null);
				LdapExtendedResponse ldapExtendedResponse = (LdapExtendedResponse)ldapResponseQueue.getResponse();
				ldapExtendedResponse.chkResultCode();
				this.conn.startTLS();
			}
			finally
			{
				this.conn.startReader();
				this.conn.freeWriteSemaphore(messageID);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008430 File Offset: 0x00007430
		public virtual void stopTLS()
		{
			if (!this.TLS)
			{
				throw new LdapLocalException("NO_STARTTLS", 1);
			}
			int msgId = this.conn.acquireWriteSemaphore();
			try
			{
				if (!this.conn.areMessagesComplete())
				{
					throw new LdapLocalException("OUTSTANDING_OPERATIONS", 1);
				}
				this.conn.stopTLS();
			}
			finally
			{
				this.conn.freeWriteSemaphore(msgId);
				this.Connect(this.Host, this.Port);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000084C0 File Offset: 0x000074C0
		public virtual void Abandon(LdapSearchResults results)
		{
			this.Abandon(results, this.defSearchCons);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000084DC File Offset: 0x000074DC
		public virtual void Abandon(LdapSearchResults results, LdapConstraints cons)
		{
			results.Abandon();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000084F4 File Offset: 0x000074F4
		public virtual void Abandon(int id)
		{
			this.Abandon(id, this.defSearchCons);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008510 File Offset: 0x00007510
		public virtual void Abandon(int id, LdapConstraints cons)
		{
			try
			{
				MessageAgent messageAgent = this.conn.getMessageAgent(id);
				messageAgent.Abandon(id, cons);
			}
			catch (FieldAccessException ex)
			{
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008554 File Offset: 0x00007554
		public virtual void Abandon(LdapMessageQueue queue)
		{
			this.Abandon(queue, this.defSearchCons);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008570 File Offset: 0x00007570
		public virtual void Abandon(LdapMessageQueue queue, LdapConstraints cons)
		{
			if (queue != null)
			{
				MessageAgent messageAgent;
				if (queue is LdapSearchQueue)
				{
					messageAgent = queue.MessageAgent;
				}
				else
				{
					messageAgent = queue.MessageAgent;
				}
				int[] messageIDs = messageAgent.MessageIDs;
				for (int i = 0; i < messageIDs.Length; i++)
				{
					messageAgent.Abandon(messageIDs[i], cons);
				}
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000085BC File Offset: 0x000075BC
		public virtual void Add(LdapEntry entry)
		{
			this.Add(entry, this.defSearchCons);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000085D8 File Offset: 0x000075D8
		public virtual void Add(LdapEntry entry, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Add(entry, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			lock (this.responseCtlSemaphore)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008644 File Offset: 0x00007644
		public virtual LdapResponseQueue Add(LdapEntry entry, LdapResponseQueue queue)
		{
			return this.Add(entry, queue, this.defSearchCons);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008664 File Offset: 0x00007664
		public virtual LdapResponseQueue Add(LdapEntry entry, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			if (entry == null)
			{
				throw new ArgumentException("The LdapEntry parameter cannot be null");
			}
			if (entry.DN == null)
			{
				throw new ArgumentException("The DN value must be present in the LdapEntry object");
			}
			LdapMessage msg = new LdapAddRequest(entry, cons.getControls());
			return this.SendRequestToServer(msg, cons.TimeLimit, queue, null);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000086C0 File Offset: 0x000076C0
		public virtual void Bind(string dn, string passwd)
		{
			this.Bind(3, dn, passwd, this.defSearchCons);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000086E0 File Offset: 0x000076E0
		public virtual void Bind(int version, string dn, string passwd)
		{
			this.Bind(version, dn, passwd, this.defSearchCons);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008700 File Offset: 0x00007700
		public virtual void Bind(string dn, string passwd, LdapConstraints cons)
		{
			this.Bind(3, dn, passwd, cons);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000871C File Offset: 0x0000771C
		public virtual void Bind(int version, string dn, string passwd, LdapConstraints cons)
		{
			sbyte[] passwd2 = null;
			if (passwd != null)
			{
				try
				{
					Encoding encoding = Encoding.GetEncoding("utf-8");
					byte[] bytes = encoding.GetBytes(passwd);
					passwd2 = SupportClass.ToSByteArray(bytes);
					passwd = null;
				}
				catch (IOException ex)
				{
					passwd = null;
					throw new SystemException(ex.ToString());
				}
			}
			this.Bind(version, dn, passwd2, cons);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008788 File Offset: 0x00007788
		[CLSCompliant(false)]
		public virtual void Bind(int version, string dn, sbyte[] passwd)
		{
			this.Bind(version, dn, passwd, this.defSearchCons);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000087A8 File Offset: 0x000077A8
		[CLSCompliant(false)]
		public virtual void Bind(int version, string dn, sbyte[] passwd, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Bind(version, dn, passwd, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			if (ldapResponse != null)
			{
				lock (this.responseCtlSemaphore)
				{
					this.responseCtls = ldapResponse.Controls;
				}
				this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000881C File Offset: 0x0000781C
		[CLSCompliant(false)]
		public virtual LdapResponseQueue Bind(int version, string dn, sbyte[] passwd, LdapResponseQueue queue)
		{
			return this.Bind(version, dn, passwd, queue, this.defSearchCons);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008840 File Offset: 0x00007840
		[CLSCompliant(false)]
		public virtual LdapResponseQueue Bind(int version, string dn, sbyte[] passwd, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			if (dn == null)
			{
				dn = "";
			}
			else
			{
				dn = dn.Trim();
			}
			if (passwd == null)
			{
				sbyte[] array = new sbyte[0];
				passwd = array;
			}
			bool anonymous = false;
			if (passwd.Length == 0)
			{
				anonymous = true;
				dn = "";
			}
			LdapMessage ldapMessage = new LdapBindRequest(version, dn, passwd, cons.getControls());
			int messageID = ldapMessage.MessageID;
			BindProperties bindProps = new BindProperties(version, dn, "simple", anonymous, null, null);
			if (!this.conn.Connected)
			{
				if (this.conn.Host == null)
				{
					throw new LdapException("CONNECTION_IMPOSSIBLE", 91, null);
				}
				this.conn.connect(this.conn.Host, this.conn.Port);
			}
			this.conn.acquireWriteSemaphore(messageID);
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, bindProps);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008924 File Offset: 0x00007924
		public virtual bool Compare(string dn, LdapAttribute attr)
		{
			return this.Compare(dn, attr, this.defSearchCons);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008944 File Offset: 0x00007944
		public virtual bool Compare(string dn, LdapAttribute attr, LdapConstraints cons)
		{
			bool result = false;
			LdapResponseQueue ldapResponseQueue = this.Compare(dn, attr, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			lock (this.responseCtlSemaphore)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			if (ldapResponse.ResultCode == 6)
			{
				result = true;
			}
			else if (ldapResponse.ResultCode == 5)
			{
				result = false;
			}
			else
			{
				this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
			}
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000089D4 File Offset: 0x000079D4
		public virtual LdapResponseQueue Compare(string dn, LdapAttribute attr, LdapResponseQueue queue)
		{
			return this.Compare(dn, attr, queue, this.defSearchCons);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000089F4 File Offset: 0x000079F4
		public virtual LdapResponseQueue Compare(string dn, LdapAttribute attr, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (attr.size() != 1)
			{
				throw new ArgumentException("compare: Exactly one value must be present in the LdapAttribute");
			}
			if (dn == null)
			{
				throw new ArgumentException("compare: DN cannot be null");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage msg = new LdapCompareRequest(dn, attr.Name, attr.ByteValue, cons.getControls());
			return this.SendRequestToServer(msg, cons.TimeLimit, queue, null);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008A60 File Offset: 0x00007A60
		public virtual void Connect(string host, int port)
		{
			SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(host, " ");
			string text = null;
			while (tokenizer.HasMoreTokens())
			{
				try
				{
					int port2 = port;
					text = tokenizer.NextToken();
					int num = text.IndexOf(':');
					if (num != -1 && num + 1 != text.Length)
					{
						try
						{
							port2 = int.Parse(text.Substring(num + 1));
							text = text.Substring(0, num);
						}
						catch (Exception ex)
						{
							throw new ArgumentException("INVALID_ADDRESS");
						}
					}
					this.conn = this.conn.destroyClone(true);
					this.conn.connect(text, port2);
					break;
				}
				catch (LdapException ex2)
				{
					if (!tokenizer.HasMoreTokens())
					{
						throw ex2;
					}
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008B34 File Offset: 0x00007B34
		public virtual void Delete(string dn)
		{
			this.Delete(dn, this.defSearchCons);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008B50 File Offset: 0x00007B50
		public virtual void Delete(string dn, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Delete(dn, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			lock (this.responseCtlSemaphore)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008BBC File Offset: 0x00007BBC
		public virtual LdapResponseQueue Delete(string dn, LdapResponseQueue queue)
		{
			return this.Delete(dn, queue, this.defSearchCons);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008BDC File Offset: 0x00007BDC
		public virtual LdapResponseQueue Delete(string dn, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (dn == null)
			{
				throw new ArgumentException("DN_PARAM_ERROR");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage msg = new LdapDeleteRequest(dn, cons.getControls());
			return this.SendRequestToServer(msg, cons.TimeLimit, queue, null);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008C24 File Offset: 0x00007C24
		public virtual void Disconnect()
		{
			this.Disconnect(this.defSearchCons, true);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008C40 File Offset: 0x00007C40
		public virtual void Disconnect(LdapConstraints cons)
		{
			this.Disconnect(cons, true);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008C58 File Offset: 0x00007C58
		private void Disconnect(LdapConstraints cons, bool how)
		{
			this.conn = this.conn.destroyClone(how);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008C7C File Offset: 0x00007C7C
		public virtual LdapExtendedResponse ExtendedOperation(LdapExtendedOperation op)
		{
			return this.ExtendedOperation(op, this.defSearchCons);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008C9C File Offset: 0x00007C9C
		public virtual LdapExtendedResponse ExtendedOperation(LdapExtendedOperation op, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.ExtendedOperation(op, cons, null);
			LdapExtendedResponse ldapExtendedResponse = (LdapExtendedResponse)ldapResponseQueue.getResponse();
			lock (this.responseCtlSemaphore)
			{
				this.responseCtls = ldapExtendedResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapExtendedResponse);
			return ldapExtendedResponse;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008D0C File Offset: 0x00007D0C
		public virtual LdapResponseQueue ExtendedOperation(LdapExtendedOperation op, LdapResponseQueue queue)
		{
			return this.ExtendedOperation(op, this.defSearchCons, queue);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008D2C File Offset: 0x00007D2C
		public virtual LdapResponseQueue ExtendedOperation(LdapExtendedOperation op, LdapConstraints cons, LdapResponseQueue queue)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage msg = this.MakeExtendedOperation(op, cons);
			return this.SendRequestToServer(msg, cons.TimeLimit, queue, null);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008D60 File Offset: 0x00007D60
		protected internal virtual LdapMessage MakeExtendedOperation(LdapExtendedOperation op, LdapConstraints cons)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			if (op.getID() == null)
			{
				throw new ArgumentException("OP_PARAM_ERROR");
			}
			return new LdapExtendedRequest(op, cons.getControls());
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008D9C File Offset: 0x00007D9C
		public virtual void Modify(string dn, LdapModification mod)
		{
			this.Modify(dn, mod, this.defSearchCons);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008DBC File Offset: 0x00007DBC
		public virtual void Modify(string dn, LdapModification mod, LdapConstraints cons)
		{
			this.Modify(dn, new LdapModification[]
			{
				mod
			}, cons);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008DE0 File Offset: 0x00007DE0
		public virtual void Modify(string dn, LdapModification[] mods)
		{
			this.Modify(dn, mods, this.defSearchCons);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008E00 File Offset: 0x00007E00
		public virtual void Modify(string dn, LdapModification[] mods, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Modify(dn, mods, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			lock (this.responseCtlSemaphore)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008E70 File Offset: 0x00007E70
		public virtual LdapResponseQueue Modify(string dn, LdapModification mod, LdapResponseQueue queue)
		{
			return this.Modify(dn, mod, queue, this.defSearchCons);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008E90 File Offset: 0x00007E90
		public virtual LdapResponseQueue Modify(string dn, LdapModification mod, LdapResponseQueue queue, LdapConstraints cons)
		{
			return this.Modify(dn, new LdapModification[]
			{
				mod
			}, queue, cons);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008EB8 File Offset: 0x00007EB8
		public virtual LdapResponseQueue Modify(string dn, LdapModification[] mods, LdapResponseQueue queue)
		{
			return this.Modify(dn, mods, queue, this.defSearchCons);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008ED8 File Offset: 0x00007ED8
		public virtual LdapResponseQueue Modify(string dn, LdapModification[] mods, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (dn == null)
			{
				throw new ArgumentException("DN_PARAM_ERROR");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage msg = new LdapModifyRequest(dn, mods, cons.getControls());
			return this.SendRequestToServer(msg, cons.TimeLimit, queue, null);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008F24 File Offset: 0x00007F24
		public virtual LdapEntry Read(string dn)
		{
			return this.Read(dn, this.defSearchCons);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008F44 File Offset: 0x00007F44
		public virtual LdapEntry Read(string dn, LdapSearchConstraints cons)
		{
			return this.Read(dn, null, cons);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008F60 File Offset: 0x00007F60
		public virtual LdapEntry Read(string dn, string[] attrs)
		{
			return this.Read(dn, attrs, this.defSearchCons);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008F80 File Offset: 0x00007F80
		public virtual LdapEntry Read(string dn, string[] attrs, LdapSearchConstraints cons)
		{
			LdapSearchResults ldapSearchResults = this.Search(dn, 0, null, attrs, false, cons);
			LdapEntry result = null;
			if (ldapSearchResults.hasMore())
			{
				result = ldapSearchResults.next();
				if (ldapSearchResults.hasMore())
				{
					throw new LdapLocalException("READ_MULTIPLE", 101);
				}
			}
			return result;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008FC8 File Offset: 0x00007FC8
		public static LdapEntry Read(LdapUrl toGet)
		{
			LdapConnection ldapConnection = new LdapConnection();
			ldapConnection.Connect(toGet.Host, toGet.Port);
			LdapEntry result = ldapConnection.Read(toGet.getDN(), toGet.AttributeArray);
			ldapConnection.Disconnect();
			return result;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000900C File Offset: 0x0000800C
		public static LdapEntry Read(LdapUrl toGet, LdapSearchConstraints cons)
		{
			LdapConnection ldapConnection = new LdapConnection();
			ldapConnection.Connect(toGet.Host, toGet.Port);
			LdapEntry result = ldapConnection.Read(toGet.getDN(), toGet.AttributeArray, cons);
			ldapConnection.Disconnect();
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009050 File Offset: 0x00008050
		public virtual void Rename(string dn, string newRdn, bool deleteOldRdn)
		{
			this.Rename(dn, newRdn, deleteOldRdn, this.defSearchCons);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009070 File Offset: 0x00008070
		public virtual void Rename(string dn, string newRdn, bool deleteOldRdn, LdapConstraints cons)
		{
			this.Rename(dn, newRdn, null, deleteOldRdn, cons);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000908C File Offset: 0x0000808C
		public virtual void Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn)
		{
			this.Rename(dn, newRdn, newParentdn, deleteOldRdn, this.defSearchCons);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000090AC File Offset: 0x000080AC
		public virtual void Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Rename(dn, newRdn, newParentdn, deleteOldRdn, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			lock (this.responseCtlSemaphore)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009120 File Offset: 0x00008120
		public virtual LdapResponseQueue Rename(string dn, string newRdn, bool deleteOldRdn, LdapResponseQueue queue)
		{
			return this.Rename(dn, newRdn, deleteOldRdn, queue, this.defSearchCons);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009144 File Offset: 0x00008144
		public virtual LdapResponseQueue Rename(string dn, string newRdn, bool deleteOldRdn, LdapResponseQueue queue, LdapConstraints cons)
		{
			return this.Rename(dn, newRdn, null, deleteOldRdn, queue, cons);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009164 File Offset: 0x00008164
		public virtual LdapResponseQueue Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapResponseQueue queue)
		{
			return this.Rename(dn, newRdn, newParentdn, deleteOldRdn, queue, this.defSearchCons);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009188 File Offset: 0x00008188
		public virtual LdapResponseQueue Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (dn == null || newRdn == null)
			{
				throw new ArgumentException("RDN_PARAM_ERROR");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage msg = new LdapModifyDNRequest(dn, newRdn, newParentdn, deleteOldRdn, cons.getControls());
			return this.SendRequestToServer(msg, cons.TimeLimit, queue, null);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000091DC File Offset: 0x000081DC
		public virtual LdapSearchResults Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly)
		{
			return this.Search(base_Renamed, scope, filter, attrs, typesOnly, this.defSearchCons);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009200 File Offset: 0x00008200
		public virtual LdapSearchResults Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchConstraints cons)
		{
			LdapSearchQueue queue = this.Search(base_Renamed, scope, filter, attrs, typesOnly, null, cons);
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			return new LdapSearchResults(this, queue, cons);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009238 File Offset: 0x00008238
		public virtual LdapSearchQueue Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchQueue queue)
		{
			return this.Search(base_Renamed, scope, filter, attrs, typesOnly, queue, this.defSearchCons);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009260 File Offset: 0x00008260
		public virtual LdapSearchQueue Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchQueue queue, LdapSearchConstraints cons)
		{
			if (filter == null)
			{
				filter = "objectclass=*";
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage msg = new LdapSearchRequest(base_Renamed, scope, filter, attrs, cons.Dereference, cons.MaxResults, cons.ServerTimeLimit, typesOnly, cons.getControls());
			LdapSearchQueue ldapSearchQueue = queue;
			MessageAgent messageAgent;
			if (ldapSearchQueue == null)
			{
				messageAgent = new MessageAgent();
				ldapSearchQueue = new LdapSearchQueue(messageAgent);
			}
			else
			{
				messageAgent = queue.MessageAgent;
			}
			try
			{
				messageAgent.sendMessage(this.conn, msg, cons.TimeLimit, ldapSearchQueue, null);
			}
			catch (LdapException ex)
			{
				throw ex;
			}
			return ldapSearchQueue;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009308 File Offset: 0x00008308
		public static LdapSearchResults Search(LdapUrl toGet)
		{
			return LdapConnection.Search(toGet, null);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009320 File Offset: 0x00008320
		public static LdapSearchResults Search(LdapUrl toGet, LdapSearchConstraints cons)
		{
			LdapConnection ldapConnection = new LdapConnection();
			ldapConnection.Connect(toGet.Host, toGet.Port);
			if (cons == null)
			{
				cons = ldapConnection.SearchConstraints;
			}
			else
			{
				cons = (LdapSearchConstraints)cons.Clone();
			}
			cons.BatchSize = 0;
			LdapSearchResults result = ldapConnection.Search(toGet.getDN(), toGet.Scope, toGet.Filter, toGet.AttributeArray, false, cons);
			ldapConnection.Disconnect();
			return result;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009394 File Offset: 0x00008394
		public virtual LdapMessageQueue SendRequest(LdapMessage request, LdapMessageQueue queue)
		{
			return this.SendRequest(request, queue, null);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000093B0 File Offset: 0x000083B0
		public virtual LdapMessageQueue SendRequest(LdapMessage request, LdapMessageQueue queue, LdapConstraints cons)
		{
			if (!request.Request)
			{
				throw new SystemException("Object is not a request message");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessageQueue ldapMessageQueue = queue;
			MessageAgent messageAgent;
			if (ldapMessageQueue == null)
			{
				messageAgent = new MessageAgent();
				if (request.Type == 3)
				{
					ldapMessageQueue = new LdapSearchQueue(messageAgent);
				}
				else
				{
					ldapMessageQueue = new LdapResponseQueue(messageAgent);
				}
			}
			else if (request.Type == 3)
			{
				messageAgent = queue.MessageAgent;
			}
			else
			{
				messageAgent = queue.MessageAgent;
			}
			try
			{
				messageAgent.sendMessage(this.conn, request, cons.TimeLimit, ldapMessageQueue, null);
			}
			catch (LdapException ex)
			{
				throw ex;
			}
			return ldapMessageQueue;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009458 File Offset: 0x00008458
		private LdapResponseQueue SendRequestToServer(LdapMessage msg, int timeout, LdapResponseQueue queue, BindProperties bindProps)
		{
			MessageAgent messageAgent;
			if (queue == null)
			{
				messageAgent = new MessageAgent();
				queue = new LdapResponseQueue(messageAgent);
			}
			else
			{
				messageAgent = queue.MessageAgent;
			}
			messageAgent.sendMessage(this.conn, msg, timeout, queue, bindProps);
			return queue;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009498 File Offset: 0x00008498
		private ReferralInfo getReferralConnection(string[] referrals)
		{
			ReferralInfo referralInfo = null;
			Exception ex = null;
			LdapConnection ldapConnection = null;
			LdapReferralHandler referralHandler = this.defSearchCons.getReferralHandler();
			int i = 0;
			if (referralHandler == null || referralHandler is LdapAuthHandler)
			{
				for (i = 0; i < referrals.Length; i++)
				{
					string dn = null;
					sbyte[] passwd = null;
					try
					{
						ldapConnection = new LdapConnection();
						ldapConnection.Constraints = this.defSearchCons;
						LdapUrl ldapUrl = new LdapUrl(referrals[i]);
						ldapConnection.Connect(ldapUrl.Host, ldapUrl.Port);
						if (referralHandler != null && referralHandler is LdapAuthHandler)
						{
							LdapAuthProvider authProvider = ((LdapAuthHandler)referralHandler).getAuthProvider(ldapUrl.Host, ldapUrl.Port);
							dn = authProvider.DN;
							passwd = authProvider.Password;
						}
						ldapConnection.Bind(3, dn, passwd);
						ex = null;
						referralInfo = new ReferralInfo(ldapConnection, referrals, ldapUrl);
						ldapConnection.Connection.ActiveReferral = referralInfo;
						break;
					}
					catch (Exception ex2)
					{
						if (ldapConnection != null)
						{
							try
							{
								ldapConnection.Disconnect();
								ldapConnection = null;
								ex = ex2;
							}
							catch (LdapException ex3)
							{
							}
						}
					}
				}
			}
			else
			{
				try
				{
					ldapConnection = ((LdapBindHandler)referralHandler).Bind(referrals, this);
					if (ldapConnection == null)
					{
						LdapReferralException ex4 = new LdapReferralException("REFERRAL_ERROR");
						ex4.setReferrals(referrals);
						throw ex4;
					}
					for (int j = 0; j < referrals.Length; j++)
					{
						try
						{
							LdapUrl ldapUrl2 = new LdapUrl(referrals[j]);
							if (ldapUrl2.Host.ToUpper().Equals(ldapConnection.Host.ToUpper()) && ldapUrl2.Port == ldapConnection.Port)
							{
								referralInfo = new ReferralInfo(ldapConnection, referrals, ldapUrl2);
								break;
							}
						}
						catch (Exception ex5)
						{
						}
					}
					if (referralInfo == null)
					{
						ex = new LdapLocalException("REFERRAL_BIND_MATCH", 91);
					}
				}
				catch (Exception ex6)
				{
					ldapConnection = null;
					ex = ex6;
				}
			}
			if (ex == null)
			{
				return referralInfo;
			}
			if (ex is LdapReferralException)
			{
				throw (LdapReferralException)ex;
			}
			LdapException rootException;
			if (ex is LdapException)
			{
				rootException = (LdapException)ex;
			}
			else
			{
				rootException = new LdapLocalException("SERVER_CONNECT_ERROR", new object[]
				{
					this.conn.Host
				}, 91, ex);
			}
			LdapReferralException ex7 = new LdapReferralException("REFERRAL_ERROR", rootException);
			ex7.setReferrals(referrals);
			ex7.FailedReferral = referrals[referrals.Length - 1];
			throw ex7;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009710 File Offset: 0x00008710
		private void chkResultCode(LdapMessageQueue queue, LdapConstraints cons, LdapResponse response)
		{
			if (response.ResultCode == 10 && cons.ReferralFollowing)
			{
				ArrayList list = null;
				try
				{
					this.chaseReferral(queue, cons, response, response.Referrals, 0, false, null);
				}
				finally
				{
					this.releaseReferralConnections(list);
				}
			}
			else
			{
				response.chkResultCode();
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009774 File Offset: 0x00008774
		internal virtual ArrayList chaseReferral(LdapMessageQueue queue, LdapConstraints cons, LdapMessage msg, string[] initialReferrals, int hopCount, bool searchReference, ArrayList connectionList)
		{
			ArrayList arrayList = connectionList;
			LdapConnection ldapConnection = null;
			ReferralInfo referralInfo = null;
			if (arrayList == null)
			{
				arrayList = new ArrayList(cons.HopLimit);
			}
			string[] array;
			LdapMessage requestingMessage;
			if (initialReferrals != null)
			{
				array = initialReferrals;
				requestingMessage = msg.RequestingMessage;
			}
			else
			{
				LdapResponse ldapResponse = (LdapResponse)queue.getResponse();
				if (ldapResponse.ResultCode != 10)
				{
					ldapResponse.chkResultCode();
					return arrayList;
				}
				array = ldapResponse.Referrals;
				requestingMessage = ldapResponse.RequestingMessage;
			}
			try
			{
				if (hopCount++ > cons.HopLimit)
				{
					throw new LdapLocalException("Max hops exceeded", 97);
				}
				referralInfo = this.getReferralConnection(array);
				ldapConnection = referralInfo.ReferralConnection;
				LdapUrl referralUrl = referralInfo.ReferralUrl;
				arrayList.Add(ldapConnection);
				LdapMessage msg2 = this.rebuildRequest(requestingMessage, referralUrl, searchReference);
				try
				{
					MessageAgent messageAgent;
					if (queue is LdapResponseQueue)
					{
						messageAgent = queue.MessageAgent;
					}
					else
					{
						messageAgent = queue.MessageAgent;
					}
					messageAgent.sendMessage(ldapConnection.Connection, msg2, this.defSearchCons.TimeLimit, queue, null);
				}
				catch (InterThreadException rootException)
				{
					LdapReferralException ex = new LdapReferralException("REFERRAL_SEND", 91, null, rootException);
					ex.setReferrals(initialReferrals);
					ReferralInfo activeReferral = ldapConnection.Connection.ActiveReferral;
					ex.FailedReferral = activeReferral.ReferralUrl.ToString();
					throw ex;
				}
				if (initialReferrals != null)
				{
					return arrayList;
				}
				arrayList = this.chaseReferral(queue, cons, null, null, hopCount, false, arrayList);
			}
			catch (Exception ex2)
			{
				if (ex2 is LdapReferralException)
				{
					throw (LdapReferralException)ex2;
				}
				LdapReferralException ex3 = new LdapReferralException("REFERRAL_ERROR", ex2);
				ex3.setReferrals(array);
				if (referralInfo != null)
				{
					ex3.FailedReferral = referralInfo.ReferralUrl.ToString();
				}
				else
				{
					ex3.FailedReferral = array[array.Length - 1];
				}
				throw ex3;
			}
			return arrayList;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000994C File Offset: 0x0000894C
		private LdapMessage rebuildRequest(LdapMessage msg, LdapUrl url, bool reference)
		{
			string dn = url.getDN();
			string filter = null;
			int type = msg.Type;
			switch (type)
			{
			case 0:
			case 6:
			case 8:
			case 10:
			case 12:
			case 14:
				break;
			case 1:
			case 2:
			case 4:
			case 5:
			case 7:
			case 9:
			case 11:
			case 13:
			case 15:
			case 16:
				goto IL_6F;
			case 3:
				if (reference)
				{
					filter = url.Filter;
				}
				break;
			default:
				if (type != 23)
				{
					goto IL_6F;
				}
				break;
			}
			goto IL_95;
			IL_6F:
			throw new LdapLocalException("IMPROPER_REFERRAL", new object[]
			{
				msg.Type
			}, 82);
			IL_95:
			return msg.Clone(dn, filter, reference);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000099FC File Offset: 0x000089FC
		internal virtual void releaseReferralConnections(ArrayList list)
		{
			if (list != null)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					try
					{
						LdapConnection ldapConnection = (LdapConnection)list[i];
						list.RemoveAt(i);
						ldapConnection.Disconnect();
					}
					catch (IndexOutOfRangeException ex)
					{
					}
					catch (LdapException ex2)
					{
					}
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009A7C File Offset: 0x00008A7C
		public virtual LdapSchema FetchSchema(string schemaDN)
		{
			LdapEntry ent = this.Read(schemaDN, LdapSchema.schemaTypeNames);
			return new LdapSchema(ent);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009AA0 File Offset: 0x00008AA0
		public virtual string GetSchemaDN()
		{
			return this.GetSchemaDN("");
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009ABC File Offset: 0x00008ABC
		public virtual string GetSchemaDN(string dn)
		{
			string[] array = new string[]
			{
				"subschemaSubentry"
			};
			LdapEntry ldapEntry = this.Read(dn, array);
			LdapAttribute attribute = ldapEntry.getAttribute(array[0]);
			string[] stringValueArray = attribute.StringValueArray;
			if (stringValueArray == null || stringValueArray.Length < 1)
			{
				throw new LdapLocalException("NO_SCHEMA", new object[]
				{
					dn
				}, 94);
			}
			if (stringValueArray.Length > 1)
			{
				throw new LdapLocalException("MULTIPLE_SCHEMA", new object[]
				{
					dn
				}, 19);
			}
			return stringValueArray[0];
		}

		// Token: 0x040000C6 RID: 198
		public const int SCOPE_BASE = 0;

		// Token: 0x040000C7 RID: 199
		public const int SCOPE_ONE = 1;

		// Token: 0x040000C8 RID: 200
		public const int SCOPE_SUB = 2;

		// Token: 0x040000C9 RID: 201
		public const string NO_ATTRS = "1.1";

		// Token: 0x040000CA RID: 202
		public const string ALL_USER_ATTRS = "*";

		// Token: 0x040000CB RID: 203
		public const int Ldap_V3 = 3;

		// Token: 0x040000CC RID: 204
		public const int DEFAULT_PORT = 389;

		// Token: 0x040000CD RID: 205
		public const int DEFAULT_SSL_PORT = 636;

		// Token: 0x040000CE RID: 206
		public const string Ldap_PROPERTY_SDK = "version.sdk";

		// Token: 0x040000CF RID: 207
		public const string Ldap_PROPERTY_PROTOCOL = "version.protocol";

		// Token: 0x040000D0 RID: 208
		public const string Ldap_PROPERTY_SECURITY = "version.security";

		// Token: 0x040000D1 RID: 209
		public const string SERVER_SHUTDOWN_OID = "1.3.6.1.4.1.1466.20036";

		// Token: 0x040000D2 RID: 210
		private const string START_TLS_OID = "1.3.6.1.4.1.1466.20037";

		// Token: 0x040000D3 RID: 211
		private LdapSearchConstraints defSearchCons;

		// Token: 0x040000D4 RID: 212
		private LdapControl[] responseCtls = null;

		// Token: 0x040000D5 RID: 213
		private object responseCtlSemaphore;

		// Token: 0x040000D6 RID: 214
		private Connection conn = null;

		// Token: 0x040000D7 RID: 215
		private static object nameLock;

		// Token: 0x040000D8 RID: 216
		private static int lConnNum = 0;

		// Token: 0x040000D9 RID: 217
		private string name;
	}
}
