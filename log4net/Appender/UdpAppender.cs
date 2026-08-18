using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000034 RID: 52
	public class UdpAppender : AppenderSkeleton
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000062FB File Offset: 0x000044FB
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00006303 File Offset: 0x00004503
		public IPAddress RemoteAddress
		{
			get
			{
				return this.m_remoteAddress;
			}
			set
			{
				this.m_remoteAddress = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000630C File Offset: 0x0000450C
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00006314 File Offset: 0x00004514
		public int RemotePort
		{
			get
			{
				return this.m_remotePort;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw SystemInfo.CreateArgumentOutOfRangeException("value", value, string.Concat(new string[]
					{
						"The value specified is less than ",
						0.ToString(NumberFormatInfo.InvariantInfo),
						" or greater than ",
						65535.ToString(NumberFormatInfo.InvariantInfo),
						"."
					}));
				}
				this.m_remotePort = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00006390 File Offset: 0x00004590
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00006398 File Offset: 0x00004598
		public int LocalPort
		{
			get
			{
				return this.m_localPort;
			}
			set
			{
				if (value != 0 && (value < 0 || value > 65535))
				{
					throw SystemInfo.CreateArgumentOutOfRangeException("value", value, string.Concat(new string[]
					{
						"The value specified is less than ",
						0.ToString(NumberFormatInfo.InvariantInfo),
						" or greater than ",
						65535.ToString(NumberFormatInfo.InvariantInfo),
						"."
					}));
				}
				this.m_localPort = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006417 File Offset: 0x00004617
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000641F File Offset: 0x0000461F
		public Encoding Encoding
		{
			get
			{
				return this.m_encoding;
			}
			set
			{
				this.m_encoding = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00006428 File Offset: 0x00004628
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00006430 File Offset: 0x00004630
		protected UdpClient Client
		{
			get
			{
				return this.m_client;
			}
			set
			{
				this.m_client = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00006439 File Offset: 0x00004639
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00006441 File Offset: 0x00004641
		protected IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.m_remoteEndPoint;
			}
			set
			{
				this.m_remoteEndPoint = value;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000644C File Offset: 0x0000464C
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.RemoteAddress == null)
			{
				throw new ArgumentNullException("The required property 'Address' was not specified.");
			}
			if (this.RemotePort < 0 || this.RemotePort > 65535)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("this.RemotePort", this.RemotePort, string.Concat(new string[]
				{
					"The RemotePort is less than ",
					0.ToString(NumberFormatInfo.InvariantInfo),
					" or greater than ",
					65535.ToString(NumberFormatInfo.InvariantInfo),
					"."
				}));
			}
			if (this.LocalPort != 0 && (this.LocalPort < 0 || this.LocalPort > 65535))
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("this.LocalPort", this.LocalPort, string.Concat(new string[]
				{
					"The LocalPort is less than ",
					0.ToString(NumberFormatInfo.InvariantInfo),
					" or greater than ",
					65535.ToString(NumberFormatInfo.InvariantInfo),
					"."
				}));
			}
			this.RemoteEndPoint = new IPEndPoint(this.RemoteAddress, this.RemotePort);
			this.InitializeClientConnection();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006588 File Offset: 0x00004788
		protected override void Append(LoggingEvent loggingEvent)
		{
			try
			{
				byte[] bytes = this.m_encoding.GetBytes(base.RenderLoggingEvent(loggingEvent).ToCharArray());
				this.Client.Send(bytes, bytes.Length, this.RemoteEndPoint);
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error(string.Concat(new object[]
				{
					"Unable to send logging event to remote host ",
					this.RemoteAddress.ToString(),
					" on port ",
					this.RemotePort,
					"."
				}), e, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00006628 File Offset: 0x00004828
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000662B File Offset: 0x0000482B
		protected override void OnClose()
		{
			base.OnClose();
			if (this.Client != null)
			{
				this.Client.Close();
				this.Client = null;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006650 File Offset: 0x00004850
		protected virtual void InitializeClientConnection()
		{
			try
			{
				if (this.LocalPort == 0)
				{
					this.Client = new UdpClient(this.RemoteAddress.AddressFamily);
				}
				else
				{
					this.Client = new UdpClient(this.LocalPort, this.RemoteAddress.AddressFamily);
				}
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error("Could not initialize the UdpClient connection on port " + this.LocalPort.ToString(NumberFormatInfo.InvariantInfo) + ".", e, ErrorCode.GenericFailure);
				this.Client = null;
			}
		}

		// Token: 0x040000CD RID: 205
		private IPAddress m_remoteAddress;

		// Token: 0x040000CE RID: 206
		private int m_remotePort;

		// Token: 0x040000CF RID: 207
		private IPEndPoint m_remoteEndPoint;

		// Token: 0x040000D0 RID: 208
		private int m_localPort;

		// Token: 0x040000D1 RID: 209
		private UdpClient m_client;

		// Token: 0x040000D2 RID: 210
		private Encoding m_encoding = Encoding.Default;
	}
}
