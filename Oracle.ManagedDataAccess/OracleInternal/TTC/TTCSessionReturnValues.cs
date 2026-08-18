using System;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000235 RID: 565
	internal class TTCSessionReturnValues : TTCFunction
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x000DE424 File Offset: 0x000DC624
		internal TTCSessionReturnValues(MarshallingEngine mEngine) : base(mEngine, 4, 0)
		{
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000DE430 File Offset: 0x000DC630
		internal void Receive()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_marshallingEngine.UnmarshalUB1(true);
				int num = this.m_marshallingEngine.UnmarshalUB2(false);
				if (num > 0)
				{
					this.m_marshallingEngine.UnmarshalUB1(true);
					this.m_sessProperties = new TTCKeywordValuePair[num];
					for (int i = 0; i < num; i++)
					{
						this.m_sessProperties[i] = TTCKeywordValuePair.Unmarshal(this.m_marshallingEngine);
						if (163 == this.m_sessProperties[i].m_keyword)
						{
							this.m_marshallingEngine.m_connImplReference.m_sessionTimeZone = new OracleIntervalDS(this.m_sessProperties[i].m_binaryValue);
							this.m_marshallingEngine.m_connImplReference.m_sessionTimeZone.initialZoneId = 0;
						}
					}
				}
				this.m_marshallingEngine.m_connImplReference.UpdateSessionAttributes(this.m_sessProperties);
				this.m_s2cSessionGetflags = this.m_marshallingEngine.UnmarshalUB4(false);
				this.m_newSesionId = this.m_marshallingEngine.UnmarshalUB4(false);
				this.m_newSerialNumber = (long)this.m_marshallingEngine.UnmarshalUB2(false);
				if (this.m_marshallingEngine.m_connImplReference.SessionId != (int)this.m_newSesionId || this.m_marshallingEngine.m_connImplReference.SerialNumber != (int)this.m_newSerialNumber || (this.m_s2cSessionGetflags & 8L) == 8L || (this.m_s2cSessionGetflags & 4L) == 4L)
				{
					this.m_marshallingEngine.m_connImplReference.SessionId = (int)this.m_newSesionId;
					this.m_marshallingEngine.m_connImplReference.SerialNumber = (int)this.m_newSerialNumber;
					this.m_marshallingEngine.m_connImplReference.NewDRCPSessionAttached();
				}
				this.m_marshallingEngine.m_bDRCPSessionAttached = true;
				this.m_marshallingEngine.m_connImplReference.bDRCPServerProcessAttached = true;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x040018FD RID: 6397
		private long m_s2cSessionGetflags;

		// Token: 0x040018FE RID: 6398
		private long m_newSesionId;

		// Token: 0x040018FF RID: 6399
		private long m_newSerialNumber;

		// Token: 0x04001900 RID: 6400
		private TTCKeywordValuePair[] m_sessProperties;
	}
}
