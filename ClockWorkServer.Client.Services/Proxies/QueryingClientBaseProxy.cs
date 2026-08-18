using System;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000117 RID: 279
	internal class QueryingClientBaseProxy : ClientBase<IQuerying>, IQuerying, IService
	{
		// Token: 0x06000ADE RID: 2782 RVA: 0x0001B863 File Offset: 0x00019A63
		public QueryingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001B86E File Offset: 0x00019A6E
		public QueryingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001B87C File Offset: 0x00019A7C
		public DataTable ExecuteQuery(string query)
		{
			return base.Channel.ExecuteQuery(query);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001B89C File Offset: 0x00019A9C
		public DataTable ExecuteQuery(string query, CWDbParameter[] parameters)
		{
			return base.Channel.ExecuteQuery(query, parameters);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001B8BC File Offset: 0x00019ABC
		public int ExecuteNonQuery(string query)
		{
			return base.Channel.ExecuteNonQuery(query);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001B8DC File Offset: 0x00019ADC
		public int ExecuteNonQuery(string query, CWDbParameter[] parameters)
		{
			return base.Channel.ExecuteNonQuery(query, parameters);
		}
	}
}
