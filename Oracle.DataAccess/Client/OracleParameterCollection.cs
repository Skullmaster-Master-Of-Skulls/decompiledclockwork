using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200004E RID: 78
	public sealed class OracleParameterCollection : DbParameterCollection
	{
		// Token: 0x06000354 RID: 852 RVA: 0x000287FA File Offset: 0x000277FA
		static OracleParameterCollection()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00028808 File Offset: 0x00027808
		internal OracleParameterCollection()
		{
			this.m_array = new ArrayList();
		}

		// Token: 0x1700007E RID: 126
		public OracleParameter this[string name]
		{
			get
			{
				int index;
				if ((index = this.FindParamByName(name)) != -1)
				{
					return this[index];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.m_collRef != null)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRMCOL_ALREADY_ADDED, new string[0]));
				}
				int num = this.FindParamByName(name);
				if (num >= 0)
				{
					this.m_array[num] = value;
					value.m_collRef = this;
					return;
				}
				throw new ArgumentException("name");
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000288A4 File Offset: 0x000278A4
		private int FindParamByName(string name)
		{
			int count = this.m_array.Count;
			for (int i = 0; i < count; i++)
			{
				if (((OracleParameter)this.m_array[i]).ParameterName.Length >= 1 && ((OracleParameter)this.m_array[i]).ParameterName[0] == '"')
				{
					if (((OracleParameter)this.m_array[i]).ParameterName == name)
					{
						return i;
					}
				}
				else if (string.Compare(((OracleParameter)this.m_array[i]).ParameterName, name, true) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0002894F File Offset: 0x0002794F
		public override bool Contains(string parameterName)
		{
			return this.FindParamByName(parameterName) != -1;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0002895E File Offset: 0x0002795E
		public override int IndexOf(string parameterName)
		{
			return this.FindParamByName(parameterName);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00028968 File Offset: 0x00027968
		public override void RemoveAt(string parameterName)
		{
			int num = this.FindParamByName(parameterName);
			if (num >= 0)
			{
				((OracleParameter)this.m_array[num]).m_collRef = null;
				this.m_array.RemoveAt(num);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000289AA File Offset: 0x000279AA
		public override bool IsFixedSize
		{
			get
			{
				return this.m_array.IsFixedSize;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000289B7 File Offset: 0x000279B7
		public override bool IsReadOnly
		{
			get
			{
				return this.m_array.IsReadOnly;
			}
		}

		// Token: 0x17000081 RID: 129
		public OracleParameter this[int index]
		{
			get
			{
				return this.m_array[index] as OracleParameter;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (value.m_collRef == null)
				{
					this.m_array[index] = value;
					value.m_collRef = this;
					return;
				}
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRMCOL_ALREADY_ADDED, new string[0]));
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00028A24 File Offset: 0x00027A24
		public override int Add(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(1)\n"
				});
			}
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			OracleParameter oracleParameter = (OracleParameter)obj;
			if (oracleParameter.m_collRef != null)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRMCOL_ALREADY_ADDED, new string[0]));
			}
			int result = this.m_array.Add(oracleParameter);
			oracleParameter.m_collRef = this;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(1)\n"
				});
			}
			return result;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00028AB0 File Offset: 0x00027AB0
		public OracleParameter Add(OracleParameter param)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(2)\n"
				});
			}
			if (param == null)
			{
				throw new ArgumentNullException("param");
			}
			if (param.m_collRef != null)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRMCOL_ALREADY_ADDED, new string[0]));
			}
			this.m_array.Add(param);
			param.m_collRef = this;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(2)\n"
				});
			}
			return param;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00028B38 File Offset: 0x00027B38
		public OracleParameter Add(string name, object val)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(3)\n"
				});
			}
			OracleParameter result = this.Add(new OracleParameter(name, val));
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(3)\n"
				});
			}
			return result;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00028B90 File Offset: 0x00027B90
		public OracleParameter Add(string name, OracleDbType dbType)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(4)\n"
				});
			}
			OracleParameter result = this.Add(new OracleParameter(name, dbType));
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(4)\n"
				});
			}
			return result;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00028BE8 File Offset: 0x00027BE8
		public OracleParameter Add(string name, OracleDbType dbType, ParameterDirection direction)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(5)\n"
				});
			}
			OracleParameter result = this.Add(new OracleParameter(name, dbType, direction));
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(5)\n"
				});
			}
			return result;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00028C40 File Offset: 0x00027C40
		public OracleParameter Add(string name, OracleDbType dbType, object val, ParameterDirection dir)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(6)\n"
				});
			}
			OracleParameter result = this.Add(new OracleParameter(name, dbType, val, dir));
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(6)\n"
				});
			}
			return result;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00028C9C File Offset: 0x00027C9C
		public OracleParameter Add(string name, OracleDbType dbType, int size, object val, ParameterDirection dir)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(7)\n"
				});
			}
			OracleParameter result = this.Add(new OracleParameter(name, dbType, size, val, dir));
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(7)\n"
				});
			}
			return result;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00028CF8 File Offset: 0x00027CF8
		public OracleParameter Add(string name, OracleDbType dbType, int size)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(8)\n"
				});
			}
			OracleParameter result = this.Add(new OracleParameter(name, dbType, size));
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(8)\n"
				});
			}
			return result;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00028D50 File Offset: 0x00027D50
		public OracleParameter Add(string name, OracleDbType dbType, int size, string srcColumn)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(9)\n"
				});
			}
			OracleParameter result = this.Add(new OracleParameter(name, dbType, size, srcColumn));
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(9)\n"
				});
			}
			return result;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00028DAC File Offset: 0x00027DAC
		public OracleParameter Add(string name, OracleDbType dbType, int size, ParameterDirection dir, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion version, object val)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Add(10)\n"
				});
			}
			OracleParameter result = this.Add(new OracleParameter(name, dbType, size, dir, isNullable, precision, scale, srcColumn, version, val));
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Add(10)\n"
				});
			}
			return result;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00028E14 File Offset: 0x00027E14
		public override void Clear()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameterCollection::Clear()\n"
				});
			}
			for (int i = 0; i < this.m_array.Count; i++)
			{
				((OracleParameter)this.m_array[i]).m_collRef = null;
			}
			this.m_array.Clear();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameterCollection::Clear()\n"
				});
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00028E93 File Offset: 0x00027E93
		public override bool Contains(object item)
		{
			return this.m_array.Contains((OracleParameter)item);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00028EA6 File Offset: 0x00027EA6
		public override int IndexOf(object obj)
		{
			return this.m_array.IndexOf((OracleParameter)obj);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00028EBC File Offset: 0x00027EBC
		public override void Insert(int index, object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			OracleParameter oracleParameter = (OracleParameter)obj;
			if (oracleParameter.m_collRef != null)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRMCOL_ALREADY_ADDED, new string[0]));
			}
			this.m_array.Insert(index, oracleParameter);
			oracleParameter.m_collRef = this;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00028F0C File Offset: 0x00027F0C
		public override void Remove(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			int num = this.m_array.IndexOf((OracleParameter)obj);
			if (num >= 0)
			{
				((OracleParameter)this.m_array[num]).m_collRef = null;
				this.m_array.RemoveAt(num);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00028F66 File Offset: 0x00027F66
		public override void RemoveAt(int index)
		{
			((OracleParameter)this.m_array[index]).m_collRef = null;
			this.m_array.RemoveAt(index);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00028F8B File Offset: 0x00027F8B
		public override int Count
		{
			get
			{
				return this.m_array.Count;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00028F98 File Offset: 0x00027F98
		public override bool IsSynchronized
		{
			get
			{
				return this.m_array.IsSynchronized;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00028FA5 File Offset: 0x00027FA5
		public override object SyncRoot
		{
			get
			{
				return this.m_array.SyncRoot;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00028FB2 File Offset: 0x00027FB2
		public override void CopyTo(Array array, int index)
		{
			this.m_array.CopyTo(array, index);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00028FC1 File Offset: 0x00027FC1
		public override IEnumerator GetEnumerator()
		{
			return this.m_array.GetEnumerator();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00028FD0 File Offset: 0x00027FD0
		public override void AddRange(Array paramArray)
		{
			if (paramArray == null)
			{
				throw new ArgumentNullException();
			}
			foreach (object obj in paramArray)
			{
				OracleParameter oracleParameter = (OracleParameter)obj;
			}
			foreach (object obj2 in paramArray)
			{
				OracleParameter value = (OracleParameter)obj2;
				this.m_array.Add(value);
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00029074 File Offset: 0x00028074
		protected override DbParameter GetParameter(int index)
		{
			return this.m_array[index] as DbParameter;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00029088 File Offset: 0x00028088
		protected override DbParameter GetParameter(string parameterName)
		{
			int index;
			if ((index = this.FindParamByName(parameterName)) != -1)
			{
				return this[index];
			}
			return null;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000290AC File Offset: 0x000280AC
		protected override void SetParameter(int index, DbParameter value)
		{
			this.m_array[index] = (value as OracleParameter);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000290C0 File Offset: 0x000280C0
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			int num = this.FindParamByName(parameterName);
			if (num < 0 || num >= this.m_array.Count)
			{
				throw new ArgumentException("parameterName");
			}
			this.m_array[num] = value;
		}

		// Token: 0x04000265 RID: 613
		private ArrayList m_array;
	}
}
