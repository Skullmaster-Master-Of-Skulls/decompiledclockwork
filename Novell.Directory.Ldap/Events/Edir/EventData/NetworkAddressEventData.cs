using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x02000076 RID: 118
	public class NetworkAddressEventData : BaseEdirEventData
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00014094 File Offset: 0x00013094
		public int ValueType
		{
			get
			{
				return this.nType;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000140AC File Offset: 0x000130AC
		public string Data
		{
			get
			{
				return this.strData;
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000140C4 File Offset: 0x000130C4
		public NetworkAddressEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.nType = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.strData = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001412C File Offset: 0x0001312C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[NetworkAddress");
			stringBuilder.AppendFormat("(type={0})", this.nType);
			stringBuilder.AppendFormat("(Data={0})", this.strData);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000203 RID: 515
		protected int nType;

		// Token: 0x04000204 RID: 516
		protected string strData;
	}
}
