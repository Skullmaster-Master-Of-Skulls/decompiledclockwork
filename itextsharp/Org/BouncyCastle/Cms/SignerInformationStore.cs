using System;
using System.Collections;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020001FB RID: 507
	public class SignerInformationStore
	{
		// Token: 0x060013A9 RID: 5033 RVA: 0x00071C3C File Offset: 0x00070C3C
		public SignerInformationStore(ICollection signerInfos)
		{
			foreach (object obj in signerInfos)
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				SignerID signerID = signerInformation.SignerID;
				ArrayList arrayList = (ArrayList)this.table[signerID];
				if (arrayList == null)
				{
					arrayList = (this.table[signerID] = new ArrayList(1));
				}
				arrayList.Add(signerInformation);
			}
			this.all = new ArrayList(signerInfos);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00071CE4 File Offset: 0x00070CE4
		public SignerInformation GetFirstSigner(SignerID selector)
		{
			ArrayList arrayList = (ArrayList)this.table[selector];
			if (arrayList != null)
			{
				return (SignerInformation)arrayList[0];
			}
			return null;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00071D14 File Offset: 0x00070D14
		public int Count
		{
			get
			{
				return this.all.Count;
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00071D21 File Offset: 0x00070D21
		public ICollection GetSigners()
		{
			return new ArrayList(this.all);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00071D30 File Offset: 0x00070D30
		public ICollection GetSigners(SignerID selector)
		{
			ArrayList arrayList = (ArrayList)this.table[selector];
			if (arrayList != null)
			{
				return new ArrayList(arrayList);
			}
			return new ArrayList();
		}

		// Token: 0x04000DAE RID: 3502
		private readonly ArrayList all;

		// Token: 0x04000DAF RID: 3503
		private readonly Hashtable table = new Hashtable();
	}
}
