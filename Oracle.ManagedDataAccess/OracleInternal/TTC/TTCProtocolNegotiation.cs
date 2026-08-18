using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x0200022F RID: 559
	internal class TTCProtocolNegotiation : TTCMessage
	{
		// Token: 0x06001489 RID: 5257 RVA: 0x000DD034 File Offset: 0x000DB234
		internal TTCProtocolNegotiation(MarshallingEngine marshallingEngine) : base(marshallingEngine, 1)
		{
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x000DD048 File Offset: 0x000DB248
		internal override void ReInit(MarshallingEngine marshallingEngine)
		{
			base.ReInit(marshallingEngine);
			this.m_serverCharSet = 0;
			this.m_serverCharSetElem = 0;
			this.m_serverFlags = 0;
			this.m_serverNCharSet = 0;
			this.m_protocolServerString = null;
			this.m_protocolServerVersion = 0;
			this.m_oVersion = -1;
			this.m_serverCompiletimeCapabilities = null;
			this.m_serverRuntimeCapabilities = null;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x000DD09C File Offset: 0x000DB29C
		internal void WriteMessage()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteTTCCode();
				this.m_marshallingEngine.MarshalB1Array(TTCProtocolNegotiation.m_protocolClientVersion);
				this.m_marshallingEngine.MarshalB1Array(TTCProtocolNegotiation.m_protocolClientString);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x000DD130 File Offset: 0x000DB330
		internal short ServerCharacterSet
		{
			get
			{
				return this.m_serverCharSet;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x000DD138 File Offset: 0x000DB338
		internal short ServerNCharacterSet
		{
			get
			{
				return this.m_serverNCharSet;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x000DD140 File Offset: 0x000DB340
		internal byte ServerFlags
		{
			get
			{
				return this.m_serverFlags;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x000DD148 File Offset: 0x000DB348
		internal byte[] ServerCompileTimeCapabilities
		{
			get
			{
				return this.m_serverCompiletimeCapabilities;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000DD150 File Offset: 0x000DB350
		internal byte[] ServerRunTimeCapabilities
		{
			get
			{
				return this.m_serverRuntimeCapabilities;
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x000DD158 File Offset: 0x000DB358
		internal void ReadResponse()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				if ((byte)this.m_marshallingEngine.UnmarshalUB1(false) != 1)
				{
					throw new Exception("TTC Protocol Error");
				}
				this.m_protocolServerVersion = (byte)this.m_marshallingEngine.UnmarshalUB1(false);
				switch (this.m_protocolServerVersion)
				{
				case 4:
					this.m_oVersion = TTCProtocolNegotiation.MIN_OVERSION_SUPPORTED;
					break;
				case 5:
					this.m_oVersion = TTCProtocolNegotiation.ORACLE8_PROD_VERSION;
					break;
				case 6:
					this.m_oVersion = TTCProtocolNegotiation.ORACLE81_PROD_VERSION;
					break;
				default:
					throw new Exception("TTC Protocol Error");
				}
				this.m_marshallingEngine.UnmarshalUB1(false);
				this.m_protocolServerString = this.m_marshallingEngine.UnmarshalTEXT(50);
				this.m_serverCharSet = (short)this.m_marshallingEngine.UnmarshalUB2(false);
				this.m_serverFlags = (byte)this.m_marshallingEngine.UnmarshalUB1(false);
				if ((this.m_serverCharSetElem = (short)this.m_marshallingEngine.UnmarshalUB2(false)) > 0)
				{
					this.m_marshallingEngine.UnmarshalNBytes((int)(this.m_serverCharSetElem * 5));
				}
				byte[] representationArray = this.m_marshallingEngine.m_typeRepresentation.m_representationArray;
				byte b = representationArray[1];
				representationArray[1] = 0;
				int length = this.m_marshallingEngine.UnmarshalUB2(false);
				representationArray[1] = b;
				byte[] array = this.m_marshallingEngine.UnmarshalNBytes(length);
				int num = (int)(6 + (array[5] & byte.MaxValue) + (array[6] & byte.MaxValue));
				this.m_serverNCharSet = (short)((array[num + 3] & byte.MaxValue) << 8);
				this.m_serverNCharSet |= (short)(array[num + 4] & byte.MaxValue);
				int num2 = (int)this.m_marshallingEngine.UnmarshalUB1(false);
				this.m_serverCompiletimeCapabilities = new byte[num2];
				for (int i = 0; i < num2; i++)
				{
					this.m_serverCompiletimeCapabilities[i] = (byte)this.m_marshallingEngine.UnmarshalUB1(false);
				}
				num2 = (int)this.m_marshallingEngine.UnmarshalUB1(false);
				if (num2 > 0)
				{
					this.m_serverRuntimeCapabilities = new byte[num2];
					for (int j = 0; j < num2; j++)
					{
						this.m_serverRuntimeCapabilities[j] = (byte)this.m_marshallingEngine.UnmarshalUB1(false);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x000DD3DC File Offset: 0x000DB5DC
		// Note: this type is marked as 'beforefieldinit'.
		static TTCProtocolNegotiation()
		{
			byte[] array = new byte[2];
			array[0] = 6;
			TTCProtocolNegotiation.m_protocolClientVersion = array;
			TTCProtocolNegotiation.m_protocolClientString = new byte[]
			{
				79,
				68,
				80,
				46,
				78,
				69,
				84,
				95,
				77,
				97,
				110,
				97,
				103,
				101,
				100,
				0
			};
		}

		// Token: 0x040018D2 RID: 6354
		internal const byte MIN_TTCVER_SUPPORTED = 4;

		// Token: 0x040018D3 RID: 6355
		internal const byte V8_TTCVER_SUPPORTED = 5;

		// Token: 0x040018D4 RID: 6356
		internal const byte MAX_TTCVER_SUPPORTED = 6;

		// Token: 0x040018D5 RID: 6357
		internal static short ORACLE8_PROD_VERSION = 8030;

		// Token: 0x040018D6 RID: 6358
		internal static short ORACLE81_PROD_VERSION = 8100;

		// Token: 0x040018D7 RID: 6359
		internal static short MIN_OVERSION_SUPPORTED = 7230;

		// Token: 0x040018D8 RID: 6360
		private short m_serverCharSet;

		// Token: 0x040018D9 RID: 6361
		private short m_serverCharSetElem;

		// Token: 0x040018DA RID: 6362
		private byte m_serverFlags;

		// Token: 0x040018DB RID: 6363
		private short m_serverNCharSet;

		// Token: 0x040018DC RID: 6364
		private byte[] m_protocolServerString;

		// Token: 0x040018DD RID: 6365
		private byte m_protocolServerVersion;

		// Token: 0x040018DE RID: 6366
		private short m_oVersion = -1;

		// Token: 0x040018DF RID: 6367
		private static byte[] m_protocolClientVersion;

		// Token: 0x040018E0 RID: 6368
		private static byte[] m_protocolClientString;

		// Token: 0x040018E1 RID: 6369
		internal byte[] m_serverCompiletimeCapabilities;

		// Token: 0x040018E2 RID: 6370
		internal byte[] m_serverRuntimeCapabilities;
	}
}
