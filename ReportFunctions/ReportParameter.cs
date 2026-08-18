using System;

namespace ReportFunctions
{
	// Token: 0x02000041 RID: 65
	public class ReportParameter
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00044708 File Offset: 0x00043708
		public int SearchInfoId
		{
			get
			{
				return this.searchInfoId;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00044720 File Offset: 0x00043720
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00044738 File Offset: 0x00043738
		public object ParamValue
		{
			get
			{
				return this.paramValue;
			}
			set
			{
				this.paramValue = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00044744 File Offset: 0x00043744
		public string ParamName
		{
			get
			{
				return this.paramName;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0004475C File Offset: 0x0004375C
		public Type ParamValueType
		{
			get
			{
				return this.paramValueType;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00044774 File Offset: 0x00043774
		public ReportParameter(int searchInfoId, string paramName, object paramValue, Type paramValueType)
		{
			this.searchInfoId = searchInfoId;
			this.paramName = paramName;
			this.paramValue = paramValue;
			this.paramValueType = paramValueType;
		}

		// Token: 0x040001EC RID: 492
		private int searchInfoId;

		// Token: 0x040001ED RID: 493
		private object paramValue;

		// Token: 0x040001EE RID: 494
		private string paramName;

		// Token: 0x040001EF RID: 495
		private Type paramValueType;
	}
}
