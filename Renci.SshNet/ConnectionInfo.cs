using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Authentication;
using Renci.SshNet.Messages.Connection;
using Renci.SshNet.Security;
using Renci.SshNet.Security.Cryptography.Ciphers;
using Renci.SshNet.Security.Cryptography.Ciphers.Modes;

namespace Renci.SshNet
{
	// Token: 0x0200000B RID: 11
	public class ConnectionInfo : IConnectionInfoInternal, IConnectionInfo
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000306B File Offset: 0x0000126B
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003073 File Offset: 0x00001273
		public IDictionary<string, Type> KeyExchangeAlgorithms { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000307C File Offset: 0x0000127C
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003084 File Offset: 0x00001284
		public IDictionary<string, CipherInfo> Encryptions { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000308D File Offset: 0x0000128D
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003095 File Offset: 0x00001295
		public IDictionary<string, HashInfo> HmacAlgorithms { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000309E File Offset: 0x0000129E
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000030A6 File Offset: 0x000012A6
		public IDictionary<string, Func<byte[], KeyHostAlgorithm>> HostKeyAlgorithms { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000030AF File Offset: 0x000012AF
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000030B7 File Offset: 0x000012B7
		public IList<AuthenticationMethod> AuthenticationMethods { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000030C0 File Offset: 0x000012C0
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000030C8 File Offset: 0x000012C8
		public IDictionary<string, Type> CompressionAlgorithms { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000030D1 File Offset: 0x000012D1
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000030D9 File Offset: 0x000012D9
		public IDictionary<string, RequestInfo> ChannelRequests { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000030E2 File Offset: 0x000012E2
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000030EA File Offset: 0x000012EA
		public bool IsAuthenticated { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000030F3 File Offset: 0x000012F3
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000030FB File Offset: 0x000012FB
		public string Host { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003104 File Offset: 0x00001304
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000310C File Offset: 0x0000130C
		public int Port { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003115 File Offset: 0x00001315
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000311D File Offset: 0x0000131D
		public string Username { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003126 File Offset: 0x00001326
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000312E File Offset: 0x0000132E
		public ProxyTypes ProxyType { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003137 File Offset: 0x00001337
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000313F File Offset: 0x0000133F
		public string ProxyHost { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003148 File Offset: 0x00001348
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003150 File Offset: 0x00001350
		public int ProxyPort { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003159 File Offset: 0x00001359
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003161 File Offset: 0x00001361
		public string ProxyUsername { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000316A File Offset: 0x0000136A
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003172 File Offset: 0x00001372
		public string ProxyPassword { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000317B File Offset: 0x0000137B
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003183 File Offset: 0x00001383
		public TimeSpan Timeout { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000318C File Offset: 0x0000138C
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003194 File Offset: 0x00001394
		public Encoding Encoding { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000319D File Offset: 0x0000139D
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000031A5 File Offset: 0x000013A5
		public int RetryAttempts { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000031AE File Offset: 0x000013AE
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000031B6 File Offset: 0x000013B6
		public int MaxSessions { get; set; }

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600008B RID: 139 RVA: 0x000031C0 File Offset: 0x000013C0
		// (remove) Token: 0x0600008C RID: 140 RVA: 0x000031F8 File Offset: 0x000013F8
		public event EventHandler<AuthenticationBannerEventArgs> AuthenticationBanner;

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000322D File Offset: 0x0000142D
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003235 File Offset: 0x00001435
		public string CurrentKeyExchangeAlgorithm { get; internal set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000323E File Offset: 0x0000143E
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003246 File Offset: 0x00001446
		public string CurrentServerEncryption { get; internal set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000324F File Offset: 0x0000144F
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003257 File Offset: 0x00001457
		public string CurrentClientEncryption { get; internal set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003260 File Offset: 0x00001460
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003268 File Offset: 0x00001468
		public string CurrentServerHmacAlgorithm { get; internal set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003271 File Offset: 0x00001471
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003279 File Offset: 0x00001479
		public string CurrentClientHmacAlgorithm { get; internal set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003282 File Offset: 0x00001482
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000328A File Offset: 0x0000148A
		public string CurrentHostKeyAlgorithm { get; internal set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003293 File Offset: 0x00001493
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000329B File Offset: 0x0000149B
		public string CurrentServerCompressionAlgorithm { get; internal set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000032A4 File Offset: 0x000014A4
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000032AC File Offset: 0x000014AC
		public string ServerVersion { get; internal set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000032B5 File Offset: 0x000014B5
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000032BD File Offset: 0x000014BD
		public string ClientVersion { get; internal set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000032C6 File Offset: 0x000014C6
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000032CE File Offset: 0x000014CE
		public string CurrentClientCompressionAlgorithm { get; internal set; }

		// Token: 0x060000A1 RID: 161 RVA: 0x000032D8 File Offset: 0x000014D8
		public ConnectionInfo(string host, string username, params AuthenticationMethod[] authenticationMethods) : this(host, ConnectionInfo.DefaultPort, username, ProxyTypes.None, null, 0, null, null, authenticationMethods)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000032F8 File Offset: 0x000014F8
		public ConnectionInfo(string host, int port, string username, params AuthenticationMethod[] authenticationMethods) : this(host, port, username, ProxyTypes.None, null, 0, null, null, authenticationMethods)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003318 File Offset: 0x00001518
		public ConnectionInfo(string host, int port, string username, ProxyTypes proxyType, string proxyHost, int proxyPort, string proxyUsername, string proxyPassword, params AuthenticationMethod[] authenticationMethods)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			port.ValidatePort("port");
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			if (username.All(new Func<char, bool>(char.IsWhiteSpace)))
			{
				throw new ArgumentException("Cannot be empty or contain only whitespace.", "username");
			}
			if (proxyType != ProxyTypes.None)
			{
				if (proxyHost == null)
				{
					throw new ArgumentNullException("proxyHost");
				}
				proxyPort.ValidatePort("proxyPort");
			}
			if (authenticationMethods == null)
			{
				throw new ArgumentNullException("authenticationMethods");
			}
			if (authenticationMethods.Length == 0)
			{
				throw new ArgumentException("At least one authentication method should be specified.", "authenticationMethods");
			}
			this.Timeout = TimeSpan.FromSeconds(30.0);
			this.RetryAttempts = 10;
			this.MaxSessions = 10;
			this.Encoding = Encoding.UTF8;
			this.KeyExchangeAlgorithms = new Dictionary<string, Type>
			{
				{
					"diffie-hellman-group-exchange-sha256",
					typeof(KeyExchangeDiffieHellmanGroupExchangeSha256)
				},
				{
					"diffie-hellman-group-exchange-sha1",
					typeof(KeyExchangeDiffieHellmanGroupExchangeSha1)
				},
				{
					"diffie-hellman-group14-sha1",
					typeof(KeyExchangeDiffieHellmanGroup14Sha1)
				},
				{
					"diffie-hellman-group1-sha1",
					typeof(KeyExchangeDiffieHellmanGroup1Sha1)
				}
			};
			Dictionary<string, CipherInfo> dictionary = new Dictionary<string, CipherInfo>();
			dictionary.Add("aes256-ctr", new CipherInfo(256, (byte[] key, byte[] iv) => new AesCipher(key, new CtrCipherMode(iv), null)));
			dictionary.Add("3des-cbc", new CipherInfo(192, (byte[] key, byte[] iv) => new TripleDesCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("aes128-cbc", new CipherInfo(128, (byte[] key, byte[] iv) => new AesCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("aes192-cbc", new CipherInfo(192, (byte[] key, byte[] iv) => new AesCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("aes256-cbc", new CipherInfo(256, (byte[] key, byte[] iv) => new AesCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("blowfish-cbc", new CipherInfo(128, (byte[] key, byte[] iv) => new BlowfishCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("twofish-cbc", new CipherInfo(256, (byte[] key, byte[] iv) => new TwofishCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("twofish192-cbc", new CipherInfo(192, (byte[] key, byte[] iv) => new TwofishCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("twofish128-cbc", new CipherInfo(128, (byte[] key, byte[] iv) => new TwofishCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("twofish256-cbc", new CipherInfo(256, (byte[] key, byte[] iv) => new TwofishCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("arcfour", new CipherInfo(128, (byte[] key, byte[] iv) => new Arc4Cipher(key, false)));
			dictionary.Add("arcfour128", new CipherInfo(128, (byte[] key, byte[] iv) => new Arc4Cipher(key, true)));
			dictionary.Add("arcfour256", new CipherInfo(256, (byte[] key, byte[] iv) => new Arc4Cipher(key, true)));
			dictionary.Add("cast128-cbc", new CipherInfo(128, (byte[] key, byte[] iv) => new CastCipher(key, new CbcCipherMode(iv), null)));
			dictionary.Add("aes128-ctr", new CipherInfo(128, (byte[] key, byte[] iv) => new AesCipher(key, new CtrCipherMode(iv), null)));
			dictionary.Add("aes192-ctr", new CipherInfo(192, (byte[] key, byte[] iv) => new AesCipher(key, new CtrCipherMode(iv), null)));
			this.Encryptions = dictionary;
			Dictionary<string, HashInfo> dictionary2 = new Dictionary<string, HashInfo>();
			dictionary2.Add("hmac-md5", new HashInfo(128, new Func<byte[], HashAlgorithm>(CryptoAbstraction.CreateHMACMD5)));
			dictionary2.Add("hmac-md5-96", new HashInfo(128, (byte[] key) => CryptoAbstraction.CreateHMACMD5(key, 96)));
			dictionary2.Add("hmac-sha1", new HashInfo(160, new Func<byte[], HashAlgorithm>(CryptoAbstraction.CreateHMACSHA1)));
			dictionary2.Add("hmac-sha1-96", new HashInfo(160, (byte[] key) => CryptoAbstraction.CreateHMACSHA1(key, 96)));
			dictionary2.Add("hmac-sha2-256", new HashInfo(256, new Func<byte[], HashAlgorithm>(CryptoAbstraction.CreateHMACSHA256)));
			dictionary2.Add("hmac-sha2-256-96", new HashInfo(256, (byte[] key) => CryptoAbstraction.CreateHMACSHA256(key, 96)));
			dictionary2.Add("hmac-sha2-512", new HashInfo(512, new Func<byte[], HashAlgorithm>(CryptoAbstraction.CreateHMACSHA512)));
			dictionary2.Add("hmac-sha2-512-96", new HashInfo(512, (byte[] key) => CryptoAbstraction.CreateHMACSHA512(key, 96)));
			dictionary2.Add("hmac-ripemd160", new HashInfo(160, new Func<byte[], HashAlgorithm>(CryptoAbstraction.CreateHMACRIPEMD160)));
			dictionary2.Add("hmac-ripemd160@openssh.com", new HashInfo(160, new Func<byte[], HashAlgorithm>(CryptoAbstraction.CreateHMACRIPEMD160)));
			this.HmacAlgorithms = dictionary2;
			Dictionary<string, Func<byte[], KeyHostAlgorithm>> dictionary3 = new Dictionary<string, Func<byte[], KeyHostAlgorithm>>();
			dictionary3.Add("ssh-rsa", (byte[] data) => new KeyHostAlgorithm("ssh-rsa", new RsaKey(), data));
			dictionary3.Add("ssh-dss", (byte[] data) => new KeyHostAlgorithm("ssh-dss", new DsaKey(), data));
			this.HostKeyAlgorithms = dictionary3;
			this.CompressionAlgorithms = new Dictionary<string, Type>
			{
				{
					"none",
					null
				}
			};
			this.ChannelRequests = new Dictionary<string, RequestInfo>
			{
				{
					"env",
					new EnvironmentVariableRequestInfo()
				},
				{
					"exec",
					new ExecRequestInfo()
				},
				{
					"exit-signal",
					new ExitSignalRequestInfo()
				},
				{
					"exit-status",
					new ExitStatusRequestInfo()
				},
				{
					"pty-req",
					new PseudoTerminalRequestInfo()
				},
				{
					"shell",
					new ShellRequestInfo()
				},
				{
					"signal",
					new SignalRequestInfo()
				},
				{
					"subsystem",
					new SubsystemRequestInfo()
				},
				{
					"window-change",
					new WindowChangeRequestInfo()
				},
				{
					"x11-req",
					new X11ForwardingRequestInfo()
				},
				{
					"xon-xoff",
					new XonXoffRequestInfo()
				},
				{
					"eow@openssh.com",
					new EndOfWriteRequestInfo()
				},
				{
					"keepalive@openssh.com",
					new KeepAliveRequestInfo()
				}
			};
			this.Host = host;
			this.Port = port;
			this.Username = username;
			this.ProxyType = proxyType;
			this.ProxyHost = proxyHost;
			this.ProxyPort = proxyPort;
			this.ProxyUsername = proxyUsername;
			this.ProxyPassword = proxyPassword;
			this.AuthenticationMethods = authenticationMethods;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003AC9 File Offset: 0x00001CC9
		internal void Authenticate(ISession session, IServiceFactory serviceFactory)
		{
			if (serviceFactory == null)
			{
				throw new ArgumentNullException("serviceFactory");
			}
			this.IsAuthenticated = false;
			serviceFactory.CreateClientAuthentication().Authenticate(this, session);
			this.IsAuthenticated = true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003AF4 File Offset: 0x00001CF4
		void IConnectionInfoInternal.UserAuthenticationBannerReceived(object sender, MessageEventArgs<BannerMessage> e)
		{
			EventHandler<AuthenticationBannerEventArgs> authenticationBanner = this.AuthenticationBanner;
			if (authenticationBanner != null)
			{
				authenticationBanner(this, new AuthenticationBannerEventArgs(this.Username, e.Message.Message, e.Message.Language));
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B33 File Offset: 0x00001D33
		IAuthenticationMethod IConnectionInfoInternal.CreateNoneAuthenticationMethod()
		{
			return new NoneAuthenticationMethod(this.Username);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003B40 File Offset: 0x00001D40
		IList<IAuthenticationMethod> IConnectionInfoInternal.AuthenticationMethods
		{
			get
			{
				return this.AuthenticationMethods.Cast<IAuthenticationMethod>().ToList<IAuthenticationMethod>();
			}
		}

		// Token: 0x0400001E RID: 30
		internal static int DefaultPort = 22;
	}
}
