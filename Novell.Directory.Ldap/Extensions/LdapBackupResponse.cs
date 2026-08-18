using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A4 RID: 164
	public class LdapBackupResponse : LdapExtendedResponse
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x000166EC File Offset: 0x000156EC
		public LdapBackupResponse(RfcLdapMessage rfcMessage) : base(rfcMessage)
		{
			if (this.ID == null || !this.ID.Equals("2.16.840.1.113719.1.27.100.97"))
			{
				throw new IOException("LDAP Extended Operation not supported");
			}
			if (this.ResultCode == 0)
			{
				byte[] array = SupportClass.ToByteArray(this.Value);
				if (array == null)
				{
					throw new Exception("LDAP Operations error. No returned value.");
				}
				LBERDecoder lberdecoder = new LBERDecoder();
				if (lberdecoder == null)
				{
					throw new Exception("Decoding error");
				}
				MemoryStream in_Renamed = new MemoryStream(array);
				Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer == null)
				{
					throw new IOException("Decoding error");
				}
				this.bufferLength = asn1Integer.intValue();
				Asn1Integer asn1Integer2 = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer2 == null)
				{
					throw new IOException("Decoding error");
				}
				int num = asn1Integer2.intValue();
				Asn1Integer asn1Integer3 = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer3 == null)
				{
					throw new IOException("Decoding error");
				}
				int num2 = asn1Integer3.intValue();
				this.stateInfo = num + "+" + num2;
				Asn1OctetString asn1OctetString = (Asn1OctetString)lberdecoder.decode(in_Renamed);
				if (asn1OctetString == null)
				{
					throw new IOException("Decoding error");
				}
				this.returnedBuffer = SupportClass.ToByteArray(asn1OctetString.byteValue());
				Asn1Sequence asn1Sequence = (Asn1Sequence)lberdecoder.decode(in_Renamed);
				if (asn1Sequence == null)
				{
					throw new IOException("Decoding error");
				}
				int num3 = ((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
				int[] array2 = new int[num3];
				Asn1Set asn1Set = (Asn1Set)asn1Sequence.get_Renamed(1);
				for (int i = 0; i < num3; i++)
				{
					Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Set.get_Renamed(i);
					array2[i] = ((Asn1Integer)asn1Sequence2.get_Renamed(0)).intValue();
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(num3);
				stringBuilder.Append(";");
				int j;
				for (j = 0; j < num3 - 1; j++)
				{
					stringBuilder.Append(array2[j]);
					stringBuilder.Append(";");
				}
				stringBuilder.Append(array2[j]);
				this.chunkSizesString = stringBuilder.ToString();
			}
			else
			{
				this.bufferLength = 0;
				this.stateInfo = null;
				this.chunkSizesString = null;
				this.returnedBuffer = null;
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00016938 File Offset: 0x00015938
		public int getBufferLength()
		{
			return this.bufferLength;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00016950 File Offset: 0x00015950
		public string getStatusInfo()
		{
			return this.stateInfo;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00016968 File Offset: 0x00015968
		public string getChunkSizesString()
		{
			return this.chunkSizesString;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00016980 File Offset: 0x00015980
		public byte[] getReturnedBuffer()
		{
			return this.returnedBuffer;
		}

		// Token: 0x04000356 RID: 854
		private int bufferLength;

		// Token: 0x04000357 RID: 855
		private string stateInfo;

		// Token: 0x04000358 RID: 856
		private string chunkSizesString;

		// Token: 0x04000359 RID: 857
		private byte[] returnedBuffer;
	}
}
