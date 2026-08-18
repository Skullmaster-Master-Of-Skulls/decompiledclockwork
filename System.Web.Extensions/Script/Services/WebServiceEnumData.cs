using System;
using System.Globalization;

namespace System.Web.Script.Services
{
	// Token: 0x020000F9 RID: 249
	internal class WebServiceEnumData : WebServiceTypeData
	{
		// Token: 0x06000D35 RID: 3381 RVA: 0x0002CB7A File Offset: 0x0002AD7A
		internal WebServiceEnumData(string typeName, string typeNamespace, string[] names, long[] values, bool isULong) : base(typeName, typeNamespace)
		{
			this.InitWebServiceEnumData(names, values, isULong);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002CB8F File Offset: 0x0002AD8F
		internal WebServiceEnumData(string typeName, string typeNamespace, Type t, string[] names, long[] values, bool isULong) : base(typeName, typeNamespace, t)
		{
			this.InitWebServiceEnumData(names, values, isULong);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0002CBA6 File Offset: 0x0002ADA6
		internal WebServiceEnumData(string typeName, string typeNamespace, string[] names, Array values, bool isULong) : base(typeName, typeNamespace)
		{
			this.InitWebServiceEnumData(names, values, isULong);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0002CBBB File Offset: 0x0002ADBB
		internal WebServiceEnumData(string typeName, string typeNamespace, Type t, string[] names, Array values, bool isULong) : base(typeName, typeNamespace)
		{
			this.InitWebServiceEnumData(names, values, isULong);
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0002CBD1 File Offset: 0x0002ADD1
		internal bool IsULong
		{
			get
			{
				return this.isULong;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0002CBD9 File Offset: 0x0002ADD9
		internal string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0002CBE1 File Offset: 0x0002ADE1
		internal long[] Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0002CBE9 File Offset: 0x0002ADE9
		private void InitWebServiceEnumData(string[] names, long[] values, bool isULong)
		{
			this.names = names;
			this.values = values;
			this.isULong = isULong;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002CC00 File Offset: 0x0002AE00
		private void InitWebServiceEnumData(string[] names, Array values, bool isULong)
		{
			this.names = names;
			this.values = new long[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				object value = values.GetValue(i);
				if (isULong)
				{
					this.values[i] = (long)((IConvertible)value).ToUInt64(CultureInfo.InvariantCulture);
				}
				else
				{
					this.values[i] = ((IConvertible)value).ToInt64(CultureInfo.InvariantCulture);
				}
			}
			this.isULong = isULong;
		}

		// Token: 0x040003A9 RID: 937
		private bool isULong;

		// Token: 0x040003AA RID: 938
		private string[] names;

		// Token: 0x040003AB RID: 939
		private long[] values;
	}
}
