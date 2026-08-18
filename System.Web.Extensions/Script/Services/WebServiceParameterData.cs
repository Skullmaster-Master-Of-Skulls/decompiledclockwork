using System;
using System.Reflection;

namespace System.Web.Script.Services
{
	// Token: 0x020000FB RID: 251
	internal class WebServiceParameterData
	{
		// Token: 0x06000D50 RID: 3408 RVA: 0x0002CFDC File Offset: 0x0002B1DC
		internal WebServiceParameterData(ParameterInfo param, int index)
		{
			this._param = param;
			this._index = index;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002CFF2 File Offset: 0x0002B1F2
		internal WebServiceParameterData(string paramName, Type paramType, int index)
		{
			this._paramName = paramName;
			this._paramType = paramType;
			this._index = index;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0002D00F File Offset: 0x0002B20F
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0002D017 File Offset: 0x0002B217
		internal ParameterInfo ParameterInfo
		{
			get
			{
				return this._param;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002D01F File Offset: 0x0002B21F
		internal string ParameterName
		{
			get
			{
				if (this._param != null)
				{
					return this._param.Name;
				}
				return this._paramName;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0002D03B File Offset: 0x0002B23B
		internal Type ParameterType
		{
			get
			{
				if (this._param != null)
				{
					return this._param.ParameterType;
				}
				return this._paramType;
			}
		}

		// Token: 0x040003B3 RID: 947
		private ParameterInfo _param;

		// Token: 0x040003B4 RID: 948
		private int _index;

		// Token: 0x040003B5 RID: 949
		private string _paramName;

		// Token: 0x040003B6 RID: 950
		private Type _paramType;
	}
}
