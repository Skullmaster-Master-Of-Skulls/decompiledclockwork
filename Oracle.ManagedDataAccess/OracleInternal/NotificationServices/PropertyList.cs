using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x0200018C RID: 396
	internal class PropertyList
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x0009DF9C File Offset: 0x0009C19C
		internal PropertyList(int n, InputBuffer ibuf)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.numelems = 0;
				this.head = null;
				for (int i = 0; i < n; i++)
				{
					string nextString = ibuf.NextString;
					int num = nextString.IndexOf(':');
					string name = nextString.Substring(0, num);
					string value_Renamed = nextString.Substring(num + 2);
					this.put(name, value_Renamed);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0009E05C File Offset: 0x0009C25C
		protected internal virtual void put(string name, string value_Renamed)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				PropertyElement propertyElement = new PropertyElement(name, value_Renamed);
				if (this.head == null)
				{
					this.head = propertyElement;
				}
				else
				{
					propertyElement.next = this.head;
					this.head = propertyElement;
				}
				this.numelems++;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0009E104 File Offset: 0x0009C304
		internal virtual void write(OutputBuffer obuf)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				for (PropertyElement next = this.head; next != null; next = next.next)
				{
					obuf.putString(next.name);
					obuf.putBytes(Notification.headerseparator, 2);
					obuf.putString(next.value_Renamed);
					obuf.putBytes(Notification.crlf, 2);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0009E1B8 File Offset: 0x0009C3B8
		internal virtual int num()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
			return this.numelems;
		}

		// Token: 0x040011C3 RID: 4547
		internal PropertyElement head;

		// Token: 0x040011C4 RID: 4548
		internal int numelems;
	}
}
