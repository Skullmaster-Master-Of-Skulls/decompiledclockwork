using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000072 RID: 114
	public sealed class OracleParameterCollection : DbParameterCollection
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x00036E24 File Offset: 0x00035024
		internal OracleParameterCollection()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_array = new ArrayList();
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00036E88 File Offset: 0x00035088
		internal OracleParameterCollection(ArrayList array)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_array = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x17000194 RID: 404
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
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRMCOL_ALREADY_ADDED, new string[0]));
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

		// Token: 0x17000195 RID: 405
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
				throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRMCOL_ALREADY_ADDED, new string[0]));
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00036FEC File Offset: 0x000351EC
		public override bool Contains(string parameterName)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (this.FindParamByName(parameterName) != -1);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00037068 File Offset: 0x00035268
		public override int IndexOf(string parameterName)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				result = this.FindParamByName(parameterName);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000370E0 File Offset: 0x000352E0
		public override void RemoveAt(string parameterName)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int num = this.FindParamByName(parameterName);
				if (num < 0)
				{
					throw new ArgumentException();
				}
				((OracleParameter)this.m_array[num]).m_collRef = null;
				this.m_array.RemoveAt(num);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00037188 File Offset: 0x00035388
		public override bool IsFixedSize
		{
			get
			{
				return this.m_array.IsFixedSize;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00037198 File Offset: 0x00035398
		public override bool IsReadOnly
		{
			get
			{
				return this.m_array.IsReadOnly;
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000371A8 File Offset: 0x000353A8
		public override int Add(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (obj == null)
				{
					throw new ArgumentNullException();
				}
				OracleParameter oracleParameter = (OracleParameter)obj;
				if (oracleParameter.m_collRef != null)
				{
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRMCOL_ALREADY_ADDED, new string[0]));
				}
				int num = this.m_array.Add(oracleParameter);
				oracleParameter.m_collRef = this;
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0003725C File Offset: 0x0003545C
		public OracleParameter Add(OracleParameter param)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (param == null)
				{
					throw new ArgumentNullException("param");
				}
				if (param.m_collRef != null)
				{
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRMCOL_ALREADY_ADDED, new string[0]));
				}
				this.m_array.Add(param);
				param.m_collRef = this;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return param;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0003730C File Offset: 0x0003550C
		public OracleParameter Add(string name, object val)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				result = this.Add(new OracleParameter(name, val));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00037388 File Offset: 0x00035588
		public OracleParameter Add(string name, OracleDbType dbType)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				result = this.Add(new OracleParameter(name, dbType));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00037404 File Offset: 0x00035604
		public OracleParameter Add(string name, OracleDbType dbType, ParameterDirection direction)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				result = this.Add(new OracleParameter(name, dbType, direction));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00037480 File Offset: 0x00035680
		public OracleParameter Add(string name, OracleDbType dbType, object val, ParameterDirection dir)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				result = this.Add(new OracleParameter(name, dbType, val, dir));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00037500 File Offset: 0x00035700
		public OracleParameter Add(string name, OracleDbType dbType, int size, object val, ParameterDirection dir)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				OracleParameter oracleParameter = this.Add(new OracleParameter(name, dbType, size, val, dir));
				result = oracleParameter;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00037584 File Offset: 0x00035784
		public OracleParameter Add(string name, OracleDbType dbType, int size)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				OracleParameter oracleParameter = this.Add(new OracleParameter(name, dbType, size));
				result = oracleParameter;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00037604 File Offset: 0x00035804
		public OracleParameter Add(string name, OracleDbType dbType, int size, string srcColumn)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				OracleParameter oracleParameter = this.Add(new OracleParameter(name, dbType, size, srcColumn));
				result = oracleParameter;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00037684 File Offset: 0x00035884
		public OracleParameter Add(string name, OracleDbType dbType, int size, ParameterDirection dir, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion version, object val)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleParameter result;
			try
			{
				OracleParameter oracleParameter = this.Add(new OracleParameter(name, dbType, size, dir, isNullable, precision, scale, srcColumn, version, val));
				result = oracleParameter;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00037710 File Offset: 0x00035910
		public override void Clear()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				for (int i = 0; i < this.m_array.Count; i++)
				{
					((OracleParameter)this.m_array[i]).m_collRef = null;
				}
				this.m_array.Clear();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000377B8 File Offset: 0x000359B8
		public override bool Contains(object item)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = this.m_array.Contains((OracleParameter)item);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00037838 File Offset: 0x00035A38
		public override int IndexOf(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				result = this.m_array.IndexOf((OracleParameter)obj);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000378B8 File Offset: 0x00035AB8
		public override void Insert(int index, object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (obj == null)
				{
					throw new ArgumentNullException();
				}
				OracleParameter oracleParameter = (OracleParameter)obj;
				if (oracleParameter.m_collRef != null)
				{
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRMCOL_ALREADY_ADDED, new string[0]));
				}
				this.m_array.Insert(index, oracleParameter);
				oracleParameter.m_collRef = this;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00037968 File Offset: 0x00035B68
		public override void Remove(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				int num = this.m_array.IndexOf((OracleParameter)obj);
				if (num < 0)
				{
					throw new ArgumentException();
				}
				((OracleParameter)this.m_array[num]).m_collRef = null;
				this.m_array.RemoveAt(num);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00037A28 File Offset: 0x00035C28
		public override void RemoveAt(int index)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				((OracleParameter)this.m_array[index]).m_collRef = null;
				this.m_array.RemoveAt(index);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00037ABC File Offset: 0x00035CBC
		public override int Count
		{
			get
			{
				return this.m_array.Count;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00037ACC File Offset: 0x00035CCC
		public override bool IsSynchronized
		{
			get
			{
				return this.m_array.IsSynchronized;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00037ADC File Offset: 0x00035CDC
		public override object SyncRoot
		{
			get
			{
				return this.m_array.SyncRoot;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00037AEC File Offset: 0x00035CEC
		public override void CopyTo(Array array, int index)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_array.CopyTo(array, index);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00037B68 File Offset: 0x00035D68
		public override IEnumerator GetEnumerator()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			IEnumerator enumerator;
			try
			{
				enumerator = this.m_array.GetEnumerator();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return enumerator;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00037BE4 File Offset: 0x00035DE4
		public override void AddRange(Array paramArray)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
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
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00037CE8 File Offset: 0x00035EE8
		protected override DbParameter GetParameter(int index)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DbParameter result;
			try
			{
				result = (this.m_array[index] as DbParameter);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00037D68 File Offset: 0x00035F68
		protected override DbParameter GetParameter(string parameterName)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DbParameter result;
			try
			{
				int index;
				if ((index = this.FindParamByName(parameterName)) != -1)
				{
					result = this[index];
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00037DF0 File Offset: 0x00035FF0
		protected override void SetParameter(int index, DbParameter value)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_array[index] = (value as OracleParameter);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00037E70 File Offset: 0x00036070
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int num = this.FindParamByName(parameterName);
				if (num < 0 || num >= this.m_array.Count)
				{
					throw new ArgumentException("parameterName");
				}
				this.m_array[num] = value;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00037F10 File Offset: 0x00036110
		private int FindParamByName(string name)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
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
				result = -1;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00038020 File Offset: 0x00036220
		internal int FindLastParamByName(string name)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int count = this.m_array.Count;
				for (int i = count - 1; i >= 0; i--)
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
				result = -1;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x04000699 RID: 1689
		internal ArrayList m_array;
	}
}
