using System;
using System.Net.Sockets;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x0200016C RID: 364
	internal class OracleCommunication
	{
		// Token: 0x06000E33 RID: 3635 RVA: 0x0009598C File Offset: 0x00093B8C
		internal OracleCommunication(ConOraBufPool oraBufPool)
		{
			this.m_oraBufPool = oraBufPool;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0009599C File Offset: 0x00093B9C
		internal OracleCommunication(OracleCommunication lsnrEP, ConOraBufPool oraBufPool)
		{
			this.m_connOption = lsnrEP.m_connOption;
			if (this.m_connOption == null)
			{
				throw new NetworkException(-6002);
			}
			this.m_sessionCtx = new SessionContext(this.m_connOption.SessionDataUnitSize, this.m_connOption.TransportDataUnitSize);
			this.m_sessionCtx.m_transportAdapter = lsnrEP.m_sessionCtx.m_transportAdapter.Answer(this.m_connOption);
			this.m_sessionCtx.m_socketStream = this.m_sessionCtx.m_transportAdapter.GetStream();
			this.m_sessionCtx.isNTConnected = true;
			this.m_oraBufPool = oraBufPool;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00095A40 File Offset: 0x00093C40
		internal static void GetSEPSUserIDandPW(string ConnectString, out string U, out string PW, out string WP, out string WF)
		{
			SEPS.GetSEPSUandP(ConnectString, out U, out PW, out WP, out WF);
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00095A50 File Offset: 0x00093C50
		internal string ConnectDescriptor
		{
			get
			{
				if (this.m_sessionCtx.isNTConnected)
				{
					return this.m_sessionCtx.m_connectData;
				}
				return null;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00095A6C File Offset: 0x00093C6C
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x00095A74 File Offset: 0x00093C74
		internal ConOraBufPool OraBufPool
		{
			get
			{
				return this.m_oraBufPool;
			}
			set
			{
				this.m_oraBufPool = value;
				this.m_sessionCtx.m_transportAdapter.OraBufPool = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00095A90 File Offset: 0x00093C90
		internal ConnectionOption ConnectionOption
		{
			get
			{
				return this.m_connOption;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00095A98 File Offset: 0x00093C98
		internal bool InBreakResetMode()
		{
			return this.m_sessionCtx.m_onBreakReset;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00095AA8 File Offset: 0x00093CA8
		internal int SDU
		{
			get
			{
				return this.m_sessionCtx.m_sessionDataUnit;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00095AB8 File Offset: 0x00093CB8
		internal string Server
		{
			get
			{
				if (this.m_connOption == null)
				{
					return null;
				}
				return this.m_connOption.Server;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x00095AD0 File Offset: 0x00093CD0
		internal bool TransportAlive
		{
			get
			{
				bool result;
				try
				{
					if (!this.m_sessionCtx.isNTConnected || !this.m_sessionCtx.m_transportAdapter.Connected)
					{
						result = false;
					}
					else
					{
						if (this.m_DataPacket == null)
						{
							this.m_DataPacket = new DataPacket(this.m_sessionCtx, Packet.NSPOVR_SZ);
						}
						if (this.m_sessionCtx.m_socket != null && this.m_sessionCtx.m_socket.Poll(0, SelectMode.SelectWrite))
						{
							this.m_DataPacket.Send(DataPacket.NSPDAFZER);
						}
						if (this.m_sessionCtx.m_transportAdapter == null || !this.m_sessionCtx.m_transportAdapter.Connected)
						{
							result = false;
						}
						else
						{
							result = true;
						}
					}
				}
				catch (Exception)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00095B8C File Offset: 0x00093D8C
		private void InitConOption(ConnectionOption connOption)
		{
			if (connOption == null)
			{
				throw new NetworkException(-6001);
			}
			this.m_connOption = connOption;
			connOption.AsyncBufferPool = this.m_oraBufPool;
			connOption.AsyncBufferInitArg = this;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00095BB8 File Offset: 0x00093DB8
		private void ConnectViaCO(ConnectionOption connOption, AddressResolution addrRes)
		{
			this.m_sessionCtx = new SessionContext(this.m_connOption.SessionDataUnitSize, this.m_connOption.TransportDataUnitSize);
			this.m_sessionCtx.m_connectData = this.m_connOption.ConnectData;
			this.m_sessionCtx.m_transportAdapter = this.GetTransportAdapter(this.m_connOption.Protocol);
			this.m_sessionCtx.m_transportAdapter.Connect(this.m_connOption);
			this.m_sessionCtx.m_socketStream = this.m_sessionCtx.m_transportAdapter.GetStream();
			this.m_sessionCtx.m_socket = this.m_sessionCtx.m_transportAdapter.GetSocket();
			if (this.m_sessionCtx.m_socketStream == null)
			{
				throw new NetworkException(12614);
			}
			this.m_sessionCtx.m_readerStream = new ReaderStream(this.m_sessionCtx);
			this.m_sessionCtx.m_writerStream = new WriterStream(this.m_sessionCtx);
			this.m_sessionCtx.isNTConnected = true;
			if (this.m_NAHandshake)
			{
				try
				{
					this.m_anoObject = new Ano();
					this.m_anoObject.Initialize(this.m_sessionCtx);
					this.m_sessionCtx.m_bAnoEnabled = true;
				}
				catch (Exception)
				{
					this.m_sessionCtx.m_bAnoEnabled = false;
				}
			}
			this.SendConnectPacketAndProcessResponse(addrRes);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00095D0C File Offset: 0x00093F0C
		internal void Connect(string tnsDescriptor)
		{
			this.m_NAHandshake = true;
			this.DoConnect(tnsDescriptor);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00095D1C File Offset: 0x00093F1C
		internal void Connect(string tnsDescriptor, bool doNAHandshake)
		{
			this.m_NAHandshake = doNAHandshake;
			this.DoConnect(tnsDescriptor);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00095D2C File Offset: 0x00093F2C
		internal void Connect(string tnsDescriptor, bool doNAHandshake = false, string IName = null)
		{
			this.m_NAHandshake = doNAHandshake;
			this.m_InstanceName = IName;
			this.DoConnect(tnsDescriptor);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00095D44 File Offset: 0x00093F44
		private void DoConnect(string tnsDescriptor)
		{
			this.m_cltEP = true;
			Exception ex = null;
			AddressResolution addressResolution = new AddressResolution(tnsDescriptor, this.m_InstanceName);
			foreach (object obj in addressResolution)
			{
				ConnectionOption connOption = (ConnectionOption)obj;
				try
				{
					this.InitConOption(connOption);
					this.ConnectViaCO(connOption, addressResolution);
					this.m_sessionCtx.m_transportAdapter.BeginAsyncReceives(null, this.m_sessionCtx.m_sessionDataUnit);
					this.m_sessionCtx.m_usingAsyncReceives = true;
					ex = null;
					break;
				}
				catch (NetworkException ex2)
				{
					ex = ex2;
				}
				catch (Exception inner)
				{
					ex = new NetworkException(-6001, inner);
				}
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00095E1C File Offset: 0x0009401C
		internal string Answer()
		{
			Packet packet = new ConnectPacket(this.m_sessionCtx, 0);
			packet.Receive();
			this.m_sessionCtx.m_bAnoEnabled = false;
			return null;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00095E4C File Offset: 0x0009404C
		internal void Accept(string AcceptData)
		{
			Packet packet = new AcceptPacket(this.m_sessionCtx, AcceptData);
			packet.Send();
			this.m_sessionCtx.m_readerStream = new ReaderStream(this.m_sessionCtx);
			this.m_sessionCtx.m_writerStream = new WriterStream(this.m_sessionCtx);
			this.m_sessionCtx.m_transportAdapter.BeginAsyncReceives(null, this.m_sessionCtx.m_sessionDataUnit);
			this.m_sessionCtx.m_usingAsyncReceives = true;
			this.m_NSHandshakeComplete = true;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00095EC8 File Offset: 0x000940C8
		internal void Listen(string tnsDescriptor, bool inAddr_Any)
		{
			AddressResolution addressResolution = new AddressResolution(tnsDescriptor, null);
			this.InitConOption(addressResolution.ResolveConnectionString());
			this.m_sessionCtx = new SessionContext(this.m_connOption.SessionDataUnitSize, this.m_connOption.TransportDataUnitSize);
			this.m_sessionCtx.m_connectData = this.m_connOption.ConnectData;
			this.m_connOption.inAddr_Any = inAddr_Any;
			this.m_sessionCtx.m_transportAdapter = this.GetTransportAdapter(this.m_connOption.Protocol);
			this.m_sessionCtx.m_transportAdapter.Listen(this.m_connOption);
			this.m_sessionCtx.isNTConnected = true;
			this.m_lsnEP = true;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00095F74 File Offset: 0x00094174
		internal void Listen(string tnsDescriptor)
		{
			this.Listen(tnsDescriptor, false);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00095F80 File Offset: 0x00094180
		internal void Disconnect()
		{
			if (this.m_sessionCtx.isNTConnected)
			{
				this.m_sessionCtx.m_transportAdapter.Disconnect();
				this.m_sessionCtx.m_socketStream.Close();
				this.m_sessionCtx.m_socketStream.Dispose();
				this.m_sessionCtx.m_socketStream = null;
				this.m_sessionCtx.m_transportAdapter = null;
				this.m_sessionCtx.isNTConnected = false;
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00095FF0 File Offset: 0x000941F0
		internal static string Resolve(string tnsAlias)
		{
			ConnectionOption connectionOption;
			return AddressResolution.Resolve(tnsAlias, out connectionOption, null);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00096008 File Offset: 0x00094208
		internal bool IsNAEInUse()
		{
			return this.m_sessionCtx != null && this.m_sessionCtx.m_ano != null && this.m_sessionCtx.cryptoNeeded;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0009602C File Offset: 0x0009422C
		internal void SendMarker(int markerType)
		{
			if (!this.m_sessionCtx.isNTConnected)
			{
				throw new NetworkException(12614);
			}
			MarkerPacket markerPacket = new MarkerPacket(this.m_sessionCtx, markerType);
			markerPacket.Send();
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00096064 File Offset: 0x00094264
		internal void Break()
		{
			if (!this.m_sessionCtx.m_onBreakReset)
			{
				this.m_sessionCtx.m_onBreakReset = true;
				if ((this.m_sessionCtx.m_negotiatedOptions & (int)TNSPacketOffsets.NSGRECVATTN) > 0)
				{
					this.m_sessionCtx.m_transportAdapter.SendUrgent(new byte[]
					{
						33
					}, 0, 1);
					return;
				}
				this.SendMarker((int)MarkerPacket.NIQBMARK);
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000960CC File Offset: 0x000942CC
		internal void Reset()
		{
			if (!this.m_sessionCtx.m_onBreakReset)
			{
				throw new NetworkException(-6000);
			}
			this.m_sessionCtx.m_writerStream.DiscardData();
			this.SendMarker((int)MarkerPacket.NIQRMARK);
			while (!this.m_sessionCtx.m_gotReset)
			{
				if (this.m_sessionCtx.m_usingAsyncReceives)
				{
					this.m_sessionCtx.m_readerStream.WaitForReset();
				}
				else
				{
					Packet packet = new Packet(this.m_sessionCtx, this.m_sessionCtx.m_sessionDataUnit);
					packet.Receive();
					if (packet.m_type == TNSPacketType.MARKER)
					{
						MarkerPacket markerPacket = new MarkerPacket(packet);
						if (markerPacket.m_isResetMarker)
						{
							this.m_sessionCtx.m_gotReset = true;
						}
					}
				}
			}
			this.m_sessionCtx.m_onBreakReset = (this.m_sessionCtx.m_gotReset = false);
			if (this.m_sessionCtx.m_ano != null)
			{
				if (this.m_sessionCtx.m_ano.dataIntegrityAlg != null)
				{
					this.m_sessionCtx.m_ano.dataIntegrityAlg.renew();
				}
				if (this.m_sessionCtx.encryptionAlg != null)
				{
					this.m_sessionCtx.encryptionAlg.setSessionKey(null, null);
				}
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000961EC File Offset: 0x000943EC
		private void SendConnectPacketAndProcessResponse(AddressResolution addrRes)
		{
			int errorCode = 0;
			bool flag = false;
			ConnectPacket connectPacket = new ConnectPacket(this.m_sessionCtx);
			while (!flag)
			{
				connectPacket.Send();
				Packet packet = new Packet(this.m_sessionCtx, this.m_sessionCtx.m_sessionDataUnit);
				packet.Receive();
				TNSPacketType type = packet.m_type;
				switch (type)
				{
				case TNSPacketType.ACCEPT:
					new AcceptPacket(packet);
					this.m_sessionCtx.m_readerStream = new ReaderStream(this.m_sessionCtx);
					this.m_sessionCtx.m_writerStream = new WriterStream(this.m_sessionCtx);
					this.m_NSHandshakeComplete = true;
					if (this.m_NAHandshake && this.m_sessionCtx.m_ano != null)
					{
						if ((this.m_sessionCtx.m_ACFL0 & 1) != 0 && (this.m_sessionCtx.m_ACFL0 & 4) == 0 && (this.m_sessionCtx.m_ACFL1 & 8) == 0)
						{
							this.m_sessionCtx.m_ano.StartNegotiation();
						}
						else
						{
							this.m_sessionCtx.m_bAnoEnabled = false;
							this.m_sessionCtx.m_ano = null;
						}
					}
					flag = true;
					if (ProviderConfig.m_bTraceLevelNetwork)
					{
						Trace.Write(OracleTraceLevel.Network, OracleTraceTag.None, new string[]
						{
							"NS Handshake completed successfully"
						});
						Trace.Write(OracleTraceLevel.Network, OracleTraceTag.None, new string[]
						{
							"Negotiated SDU size = " + this.m_sessionCtx.m_sessionDataUnit
						});
						continue;
					}
					continue;
				case TNSPacketType.ACK:
					break;
				case TNSPacketType.REFUSE:
				{
					RefusePacket refusePacket = new RefusePacket(packet);
					try
					{
						NVPair nvpair = NVNavigator.FindNVPairRecurse(NVFactory.CreateNVPair(refusePacket.Data), "ERROR");
						NVPair nvpair2;
						if (nvpair != null)
						{
							nvpair2 = NVNavigator.FindNVPairRecurse(nvpair, "CODE");
						}
						else
						{
							nvpair2 = NVNavigator.FindNVPairRecurse(NVFactory.CreateNVPair(refusePacket.Data), "ERR");
						}
						if (nvpair2 != null)
						{
							errorCode = int.Parse(nvpair2.ValueToString());
						}
					}
					catch (Exception)
					{
					}
					throw new NetworkException(errorCode);
				}
				case TNSPacketType.REDIRECT:
				{
					RedirectPacket redirectPacket = new RedirectPacket(packet);
					this.Disconnect();
					addrRes.BuildCO_Redirect(redirectPacket.redirectAddress, ref this.m_connOption);
					if (redirectPacket.redirectConnectData != null)
					{
						this.m_connOption.ConnectData = redirectPacket.redirectConnectData;
					}
					this.ConnectViaCO(this.m_connOption, addrRes);
					flag = true;
					continue;
				}
				default:
					if (type == TNSPacketType.RESEND)
					{
						if (!this.m_sessionCtx.m_transportAdapter.NeedReneg)
						{
							continue;
						}
						this.m_sessionCtx.m_transportAdapter.Renegotiate(this.m_connOption);
						this.m_sessionCtx.m_socketStream = this.m_sessionCtx.m_transportAdapter.GetStream();
						this.m_sessionCtx.m_socket = this.m_sessionCtx.m_transportAdapter.GetSocket();
						if (this.m_sessionCtx.m_socketStream == null)
						{
							throw new NetworkException(12614);
						}
						continue;
					}
					break;
				}
				this.m_sessionCtx.m_transportAdapter.Disconnect();
				throw new NetworkException(12566);
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000964CC File Offset: 0x000946CC
		internal void SetFoldInKey(byte[] Key)
		{
			Ano ano;
			byte[] skey;
			if (Key != null && this.m_sessionCtx != null && (ano = this.m_sessionCtx.m_ano) != null && this.m_sessionCtx.cryptoNeeded && (skey = ano.skey) != null)
			{
				int num = Math.Min(Key.Length, skey.Length);
				while (num-- != 0)
				{
					byte[] array = skey;
					int num2 = num;
					array[num2] ^= Key[num];
				}
				if (this.m_sessionCtx.encryptionAlg != null)
				{
					this.m_sessionCtx.encryptionAlg.setSessionKey(skey, ano.getInitializationVector());
					ano.foldedinkey = true;
				}
				if (this.m_sessionCtx.m_ano.dataIntegrityAlg != null)
				{
					this.m_sessionCtx.m_ano.dataIntegrityAlg.takeSessionKey(skey, ano.getInitializationVector());
					ano.foldedinkey = true;
				}
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000965A8 File Offset: 0x000947A8
		internal ITransportAdapter GetTransportAdapter(string protocol)
		{
			if (string.Equals(protocol, "TCPS", StringComparison.InvariantCultureIgnoreCase))
			{
				return new TcpsTransportAdapter(null);
			}
			if (string.Equals(protocol, "TCP", StringComparison.InvariantCultureIgnoreCase))
			{
				return new TcpTransportAdapter(null);
			}
			throw new NetworkException(12538);
		}

		// Token: 0x04001032 RID: 4146
		private ConnectionOption m_connOption;

		// Token: 0x04001033 RID: 4147
		internal SessionContext m_sessionCtx;

		// Token: 0x04001034 RID: 4148
		private bool m_lsnEP;

		// Token: 0x04001035 RID: 4149
		private bool m_cltEP;

		// Token: 0x04001036 RID: 4150
		private bool m_NSHandshakeComplete;

		// Token: 0x04001037 RID: 4151
		internal Ano m_anoObject;

		// Token: 0x04001038 RID: 4152
		private bool m_NAHandshake;

		// Token: 0x04001039 RID: 4153
		private string m_InstanceName;

		// Token: 0x0400103A RID: 4154
		private ConOraBufPool m_oraBufPool;

		// Token: 0x0400103B RID: 4155
		private DataPacket m_DataPacket;
	}
}
