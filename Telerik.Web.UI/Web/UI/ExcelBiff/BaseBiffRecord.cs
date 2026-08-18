using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A5A RID: 2650
	internal class BaseBiffRecord
	{
		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x00180E5D File Offset: 0x0017F05D
		public ushort RecordType
		{
			get
			{
				return this.recordType;
			}
		}

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x060066DB RID: 26331 RVA: 0x00180E65 File Offset: 0x0017F065
		// (set) Token: 0x060066DC RID: 26332 RVA: 0x00180E6D File Offset: 0x0017F06D
		public ushort Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x00180E76 File Offset: 0x0017F076
		public BaseBiffRecord(ushort recordType)
		{
			this.recordType = recordType;
		}

		// Token: 0x060066DE RID: 26334 RVA: 0x00180E88 File Offset: 0x0017F088
		public byte[] GetBaseData()
		{
			int num = 0;
			byte[] array = new byte[4];
			byte[] bytes = BitConverter.GetBytes(this.RecordType);
			bytes.CopyTo(array, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.Length);
			bytes.CopyTo(array, num);
			return array;
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x00180ED0 File Offset: 0x0017F0D0
		public byte[] GetData(out int currentPos)
		{
			int num = 0;
			byte[] array = new byte[(int)(4 + this.Length)];
			byte[] baseData = this.GetBaseData();
			baseData.CopyTo(array, num);
			num += baseData.Length;
			currentPos = num;
			return array;
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x00180F08 File Offset: 0x0017F108
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("recordType=0x{0:x4};", this.RecordType);
			stringBuilder.AppendFormat("length={0};", this.Length);
			return stringBuilder.ToString();
		}

		// Token: 0x04001905 RID: 6405
		public const ushort HeaderLength = 4;

		// Token: 0x04001906 RID: 6406
		private ushort recordType;

		// Token: 0x04001907 RID: 6407
		private ushort length;
	}
}
