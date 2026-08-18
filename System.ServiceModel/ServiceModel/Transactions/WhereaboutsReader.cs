using System;
using System.IO;
using System.Text;
using Microsoft.Transactions.Wsat.Protocol;
using Microsoft.Transactions.Wsat.Recovery;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B6 RID: 438
	internal class WhereaboutsReader
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x000334CC File Offset: 0x000316CC
		public WhereaboutsReader(byte[] whereabouts)
		{
			MemoryStream mem = new MemoryStream(whereabouts, 0, whereabouts.Length, false, true);
			this.DeserializeWhereabouts(mem);
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x000334F3 File Offset: 0x000316F3
		public string HostName
		{
			get
			{
				return this.hostName;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000334FB File Offset: 0x000316FB
		public ProtocolInformationReader ProtocolInformation
		{
			get
			{
				return this.protocolInfo;
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00033504 File Offset: 0x00031704
		private void DeserializeWhereabouts(MemoryStream mem)
		{
			Guid a = SerializationUtils.ReadGuid(mem);
			if (a != WhereaboutsReader.GuidWhereaboutsInfo)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("WhereaboutsSignatureMissing")));
			}
			uint num = SerializationUtils.ReadUInt(mem);
			if ((ulong)num * 8UL > (ulong)(mem.Length - mem.Position))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("WhereaboutsImplausibleProtocolCount")));
			}
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				this.DeserializeWhereaboutsProtocol(mem);
			}
			if (string.IsNullOrEmpty(this.hostName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("WhereaboutsNoHostName")));
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000335B0 File Offset: 0x000317B0
		private void DeserializeWhereaboutsProtocol(MemoryStream mem)
		{
			WhereaboutsReader.TmProtocol tmProtocol = (WhereaboutsReader.TmProtocol)SerializationUtils.ReadInt(mem);
			uint num = SerializationUtils.ReadUInt(mem);
			if (tmProtocol != WhereaboutsReader.TmProtocol.TmProtocolMsdtcV2)
			{
				if (tmProtocol != WhereaboutsReader.TmProtocol.TmProtocolExtended)
				{
					SerializationUtils.IncrementPosition(mem, (long)((ulong)num));
				}
				else
				{
					this.ReadExtendedProtocol(mem, num);
				}
			}
			else
			{
				this.ReadMsdtcV2Protocol(mem, num);
			}
			SerializationUtils.AlignPosition(mem, 4);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x000335F8 File Offset: 0x000317F8
		private void ReadMsdtcV2Protocol(MemoryStream mem, uint cbTmProtocolData)
		{
			if (cbTmProtocolData > 32U)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("WhereaboutsImplausibleHostNameByteCount")));
			}
			byte[] array = SerializationUtils.ReadBytes(mem, (int)cbTmProtocolData);
			int num = 0;
			while ((long)num < (long)((ulong)(cbTmProtocolData - 1U)) && (array[num] != 0 || array[num + 1] != 0))
			{
				num += 2;
			}
			if (num == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("WhereaboutsInvalidHostName")));
			}
			try
			{
				this.hostName = Encoding.Unicode.GetString(array, 0, num);
			}
			catch (ArgumentException e)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("WhereaboutsInvalidHostName"), e));
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000336A8 File Offset: 0x000318A8
		private void ReadExtendedProtocol(MemoryStream mem, uint cbTmProtocolData)
		{
			Guid a = SerializationUtils.ReadGuid(mem);
			if (a == PluggableProtocol10.ProtocolGuid || a == PluggableProtocol11.ProtocolGuid)
			{
				this.protocolInfo = new ProtocolInformationReader(mem);
				return;
			}
			SerializationUtils.IncrementPosition(mem, (long)((ulong)(cbTmProtocolData - 16U)));
		}

		// Token: 0x04001748 RID: 5960
		private string hostName;

		// Token: 0x04001749 RID: 5961
		private ProtocolInformationReader protocolInfo;

		// Token: 0x0400174A RID: 5962
		private static Guid GuidWhereaboutsInfo = new Guid("{2adb4462-bd41-11d0-b12e-00c04fc2f3ef}");

		// Token: 0x0400174B RID: 5963
		private const long STmToTmProtocolSize = 8L;

		// Token: 0x02000AFC RID: 2812
		private enum TmProtocol
		{
			// Token: 0x04003F61 RID: 16225
			TmProtocolNone,
			// Token: 0x04003F62 RID: 16226
			TmProtocolTip,
			// Token: 0x04003F63 RID: 16227
			TmProtocolMsdtcV1,
			// Token: 0x04003F64 RID: 16228
			TmProtocolMsdtcV2,
			// Token: 0x04003F65 RID: 16229
			TmProtocolExtended
		}
	}
}
