using System;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000116 RID: 278
	public class QueryingReusableClientProxy : WCFTokenBasedReusableClientProxy<IQuerying>, IQuerying, IService
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001B75A File Offset: 0x0001995A
		public QueryingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001B765 File Offset: 0x00019965
		public QueryingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001B774 File Offset: 0x00019974
		public DataTable ExecuteQuery(string query)
		{
			return this.WrapServiceMethod<DataTable>(() => this.Proxy.ExecuteQuery(query));
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0001B7AC File Offset: 0x000199AC
		public DataTable ExecuteQuery(string query, CWDbParameter[] parameters)
		{
			return this.WrapServiceMethod<DataTable>(() => this.Proxy.ExecuteQuery(query, parameters));
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001B7EC File Offset: 0x000199EC
		public int ExecuteNonQuery(string query)
		{
			return this.WrapServiceMethod<int>(() => this.Proxy.ExecuteNonQuery(query));
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001B824 File Offset: 0x00019A24
		public int ExecuteNonQuery(string query, CWDbParameter[] parameters)
		{
			return this.WrapServiceMethod<int>(() => this.Proxy.ExecuteNonQuery(query, parameters));
		}
	}
}
