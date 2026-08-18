using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using OracleInternal.Common;
using OracleInternal.Secure.Network;

namespace OracleInternal.Network
{
	// Token: 0x02000175 RID: 373
	internal class TcpsTransportAdapter : TcpTransportAdapter
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x00097F4C File Offset: 0x0009614C
		internal TcpsTransportAdapter(NameValueCollection socketOptions) : base(socketOptions)
		{
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00097F5C File Offset: 0x0009615C
		internal TcpsTransportAdapter(ConnectionOption conOption, TcpClient tcpClient) : base(conOption, tcpClient)
		{
			this.m_sslstream = new SslStream(this.m_client.GetStream());
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00097F84 File Offset: 0x00096184
		public override void Renegotiate(ConnectionOption conOption)
		{
			this.Negotiate(conOption);
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00097F90 File Offset: 0x00096190
		public override bool NeedReneg
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00097F94 File Offset: 0x00096194
		public override void Connect(ConnectionOption conOption)
		{
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Entry, new string[0]);
			}
			base.Connect(conOption);
			this.Negotiate(conOption);
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00097FD4 File Offset: 0x000961D4
		public override ITransportAdapter Answer(ConnectionOption conOption)
		{
			if (this.m_listener == null)
			{
				return null;
			}
			return new TcpsTransportAdapter(conOption, this.m_listener.AcceptTcpClient());
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00097FF4 File Offset: 0x000961F4
		public override void Disconnect()
		{
			lock (this.m_discLock)
			{
				if (this.m_sslstream != null)
				{
					this.m_sslstream.Close();
					this.m_sslstream = null;
				}
			}
			base.Disconnect();
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00098050 File Offset: 0x00096250
		public override Stream GetStream()
		{
			return this.m_sslstream;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00098058 File Offset: 0x00096258
		public override Socket GetSocket()
		{
			return null;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0009805C File Offset: 0x0009625C
		public override void Send(OraBuf OB)
		{
			OraArraySegment[] the_ByteSegments = OB.the_ByteSegments;
			try
			{
				if (OB.the_ByteSegments_Count == 2)
				{
					this.m_sslstream.Write(OB.m_buf, 0, OB.m_curlen);
				}
				else
				{
					for (int i = 0; i < OB.the_ByteSegments_Count; i++)
					{
						OraArraySegment oraArraySegment = the_ByteSegments[i];
						this.m_sslstream.Write(oraArraySegment.Array, oraArraySegment.Offset, oraArraySegment.Count);
					}
				}
			}
			catch (Exception inner)
			{
				throw new NetworkException(12571, inner);
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000980E4 File Offset: 0x000962E4
		private void Negotiate(ConnectionOption conOption)
		{
			SqlNetOraConfig sqlNetOraConfig = new SqlNetOraConfig();
			X509CertificateCollection x509CertificateCollection = null;
			SslProtocols sslProtocols = (SslProtocols)(240 | (TcpsTransportAdapter.SSL_VERSION_TLS11 | TcpsTransportAdapter.SSL_VERSION_TLS12));
			string text = null;
			string password = null;
			string text2 = "";
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Entry, new string[0]);
			}
			string sslversion = SqlNetOraConfig.SSLVersion;
			string key;
			if (!string.IsNullOrEmpty(sslversion) && !sslversion.Equals("0") && (key = sslversion) != null)
			{
				if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x6000dae-1 == null)
				{
					<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x6000dae-1 = new Dictionary<string, int>(18)
					{
						{
							"3.0",
							0
						},
						{
							"1.0",
							1
						},
						{
							"1.1",
							2
						},
						{
							"1.2",
							3
						},
						{
							"3.0 or 1.0",
							4
						},
						{
							"1.0 or 3.0",
							5
						},
						{
							"1 or 3",
							6
						},
						{
							"3 or 1",
							7
						},
						{
							"1.0 or 1.1",
							8
						},
						{
							"1.1 or 1.0",
							9
						},
						{
							"1.2 or 1.1",
							10
						},
						{
							"1.1 or 1.2",
							11
						},
						{
							"1.0 or 1.1 or 1.2",
							12
						},
						{
							"1.2 or 1.1 or 1.0",
							13
						},
						{
							"1.2 or 1.0 or 1.1",
							14
						},
						{
							"1.1 or 1.2 or 1.0",
							15
						},
						{
							"1.1 or 1.0 or 1.2",
							16
						},
						{
							"1.0 or 1.2 or 1.1",
							17
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x6000dae-1.TryGetValue(key, out num))
				{
					switch (num)
					{
					case 0:
						sslProtocols = SslProtocols.Ssl3;
						break;
					case 1:
						sslProtocols = SslProtocols.Tls;
						break;
					case 2:
						sslProtocols = (SslProtocols)TcpsTransportAdapter.SSL_VERSION_TLS11;
						break;
					case 3:
						sslProtocols = (SslProtocols)TcpsTransportAdapter.SSL_VERSION_TLS12;
						break;
					case 4:
					case 5:
					case 6:
					case 7:
						sslProtocols = SslProtocols.Default;
						break;
					case 8:
					case 9:
						sslProtocols = (SslProtocols)(TcpsTransportAdapter.SSL_VERSION_TLS11 | TcpsTransportAdapter.SSL_VERSION_TLS10);
						break;
					case 10:
					case 11:
						sslProtocols = (SslProtocols)(TcpsTransportAdapter.SSL_VERSION_TLS12 | TcpsTransportAdapter.SSL_VERSION_TLS11);
						break;
					case 12:
					case 13:
					case 14:
					case 15:
					case 16:
					case 17:
						sslProtocols = (SslProtocols)(TcpsTransportAdapter.SSL_VERSION_TLS10 | TcpsTransportAdapter.SSL_VERSION_TLS11 | TcpsTransportAdapter.SSL_VERSION_TLS12);
						break;
					}
				}
			}
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
				{
					string.Concat(new object[]
					{
						"SSLVersion = ",
						sslversion,
						". SSLProtocol = ",
						sslProtocols,
						"."
					})
				});
			}
			try
			{
				if (!string.IsNullOrEmpty(conOption.SSL_WALLET_DIRECTORY))
				{
					if (ProviderConfig.m_bTraceLevelNetwork)
					{
						Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
						{
							"MY_WALLET_DIRECTORY = " + conOption.SSL_WALLET_DIRECTORY
						});
					}
					text = conOption.SSL_WALLET_DIRECTORY;
					text2 = "FILE";
				}
				else
				{
					Hashtable walletLocation = SqlNetOraConfig.WalletLocation;
					if (walletLocation != null)
					{
						text2 = ((string)walletLocation["METHOD"]).ToUpperInvariant();
						if (text2 != null && text2 == "FILE")
						{
							text = (string)walletLocation["DIRECTORY"];
						}
					}
				}
				if (text2 == "FILE")
				{
					if (text == null)
					{
						throw new NetworkException(-6400);
					}
					text = ConfigBaseClass.GetResolvedFileLocation(text);
					if (sqlNetOraConfig != null)
					{
						password = sqlNetOraConfig["WALLET_PASSWORD"];
					}
					byte[] rawData = WalletReader.ReadWallet(text, ref password);
					X509Certificate2 x509Certificate = new X509Certificate2(rawData, password, X509KeyStorageFlags.DefaultKeySet);
					x509CertificateCollection = new X509CertificateCollection(new X509Certificate[]
					{
						x509Certificate
					});
				}
				else if (text2 == "MCS")
				{
					X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
					x509Store.Open(OpenFlags.ReadOnly);
					x509CertificateCollection = x509Store.Certificates;
				}
				if (x509CertificateCollection == null || x509CertificateCollection.Count == 0)
				{
					this.Disconnect();
					throw new NetworkException(-6400);
				}
			}
			catch (NetworkException)
			{
				throw;
			}
			catch (Exception inner)
			{
				this.Disconnect();
				throw new NetworkException(-6400, inner);
			}
			try
			{
				string text3 = (!string.IsNullOrEmpty(conOption.SSLServerDN)) ? conOption.SSLServerDN : ((!string.IsNullOrEmpty(conOption.ServiceName)) ? conOption.ServiceName : this.m_host);
				if (ProviderConfig.m_bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
					{
						"serverDN = " + text3
					});
				}
				this.m_sslstream = new SslStream(this.m_client.GetStream(), false, new RemoteCertificateValidationCallback(this.ValidateRemoteCertificate), null);
				this.m_sslstream.AuthenticateAsClient(text3, x509CertificateCollection, sslProtocols, false);
			}
			catch (NetworkException)
			{
				throw;
			}
			catch (AuthenticationException inner2)
			{
				this.Disconnect();
				if (!this.m_DNMatched)
				{
					throw new NetworkException(29003, inner2);
				}
				throw new NetworkException(542, inner2);
			}
			catch (Exception inner3)
			{
				this.Disconnect();
				throw new NetworkException(542, inner3);
			}
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00098630 File Offset: 0x00096830
		private bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chn, SslPolicyErrors pErrs)
		{
			if (ProviderConfig.m_bTraceLevelNetwork)
			{
				Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (TcpsTransportAdapter.DNMatching)
				{
					this.m_DNMatched = false;
					if (this.m_conOption != null && cert != null)
					{
						string text = this.m_conOption.SSLServerDN;
						string text2 = cert.Subject;
						if (!string.IsNullOrEmpty(text))
						{
							if (string.IsNullOrEmpty(text2))
							{
								return false;
							}
							text = string.Join("", text.Split(null, StringSplitOptions.RemoveEmptyEntries));
							text2 = string.Join("", text2.Split(null, StringSplitOptions.RemoveEmptyEntries));
							text = Regex.Replace(text, "ST=", "S=", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
							this.m_DNMatched = text.Equals(text2, StringComparison.InvariantCultureIgnoreCase);
							if (ProviderConfig.m_bTraceLevelNetwork)
							{
								Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
								{
									string.Concat(new string[]
									{
										"DN1 = \"",
										text.ToUpperInvariant(),
										"\" DN2 = \"",
										text2.ToUpperInvariant(),
										"\""
									})
								});
							}
						}
						else
						{
							if (ProviderConfig.m_bTraceLevelNetwork)
							{
								Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
								{
									"Service_Name matching."
								});
							}
							if (this.m_conOption.ServiceName != null)
							{
								this.m_DNMatched = ((pErrs & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.None);
							}
						}
					}
					if (ProviderConfig.m_bTraceLevelNetwork)
					{
						Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
						{
							"DN " + (this.m_DNMatched ? "matched." : "mismatched.")
						});
					}
					result = this.m_DNMatched;
				}
				else
				{
					result = true;
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00098820 File Offset: 0x00096A20
		public override void BeginAsyncReceives(OraBuf.AsyncReceiveCallback Callback, int AsyncBufferSize)
		{
			this.m_OraBufSize = AsyncBufferSize;
			this.m_AsyncRecvCB = Callback;
		}

		// Token: 0x040010D3 RID: 4307
		private static int SSL_VERSION_SSL_2 = 12;

		// Token: 0x040010D4 RID: 4308
		private static int SSL_VERSION_SSL_3 = 48;

		// Token: 0x040010D5 RID: 4309
		private static int SSL_VERSION_TLS10 = 192;

		// Token: 0x040010D6 RID: 4310
		private static int SSL_VERSION_TLS11 = 768;

		// Token: 0x040010D7 RID: 4311
		private static int SSL_VERSION_TLS12 = 3072;

		// Token: 0x040010D8 RID: 4312
		private static int SSL_VERSION_DEF = 240;

		// Token: 0x040010D9 RID: 4313
		protected static bool DNMatching = SqlNetOraConfig.SSLServerDNMatch;

		// Token: 0x040010DA RID: 4314
		protected SslStream m_sslstream;

		// Token: 0x040010DB RID: 4315
		protected bool m_DNMatched = true;
	}
}
