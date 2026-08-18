using System;
using System.Data;
using log4net.Core;
using log4net.Layout;

namespace log4net.Appender
{
	// Token: 0x02000009 RID: 9
	public class AdoNetAppenderParameter
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003059 File Offset: 0x00001259
		public AdoNetAppenderParameter()
		{
			this.Precision = 0;
			this.Scale = 0;
			this.Size = 0;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000307D File Offset: 0x0000127D
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003085 File Offset: 0x00001285
		public string ParameterName
		{
			get
			{
				return this.m_parameterName;
			}
			set
			{
				this.m_parameterName = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000308E File Offset: 0x0000128E
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003096 File Offset: 0x00001296
		public DbType DbType
		{
			get
			{
				return this.m_dbType;
			}
			set
			{
				this.m_dbType = value;
				this.m_inferType = false;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000030A6 File Offset: 0x000012A6
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000030AE File Offset: 0x000012AE
		public byte Precision
		{
			get
			{
				return this.m_precision;
			}
			set
			{
				this.m_precision = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000030B7 File Offset: 0x000012B7
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000030BF File Offset: 0x000012BF
		public byte Scale
		{
			get
			{
				return this.m_scale;
			}
			set
			{
				this.m_scale = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000030C8 File Offset: 0x000012C8
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000030D0 File Offset: 0x000012D0
		public int Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000030D9 File Offset: 0x000012D9
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000030E1 File Offset: 0x000012E1
		public IRawLayout Layout
		{
			get
			{
				return this.m_layout;
			}
			set
			{
				this.m_layout = value;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030EC File Offset: 0x000012EC
		public virtual void Prepare(IDbCommand command)
		{
			IDbDataParameter dbDataParameter = command.CreateParameter();
			dbDataParameter.ParameterName = this.ParameterName;
			if (!this.m_inferType)
			{
				dbDataParameter.DbType = this.DbType;
			}
			if (this.Precision != 0)
			{
				dbDataParameter.Precision = this.Precision;
			}
			if (this.Scale != 0)
			{
				dbDataParameter.Scale = this.Scale;
			}
			if (this.Size != 0)
			{
				dbDataParameter.Size = this.Size;
			}
			command.Parameters.Add(dbDataParameter);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000316C File Offset: 0x0000136C
		public virtual void FormatValue(IDbCommand command, LoggingEvent loggingEvent)
		{
			IDbDataParameter dbDataParameter = (IDbDataParameter)command.Parameters[this.ParameterName];
			object obj = this.Layout.Format(loggingEvent);
			if (obj == null)
			{
				obj = DBNull.Value;
			}
			dbDataParameter.Value = obj;
		}

		// Token: 0x04000021 RID: 33
		private string m_parameterName;

		// Token: 0x04000022 RID: 34
		private DbType m_dbType;

		// Token: 0x04000023 RID: 35
		private bool m_inferType = true;

		// Token: 0x04000024 RID: 36
		private byte m_precision;

		// Token: 0x04000025 RID: 37
		private byte m_scale;

		// Token: 0x04000026 RID: 38
		private int m_size;

		// Token: 0x04000027 RID: 39
		private IRawLayout m_layout;
	}
}
